using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Add
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.SqlClient;
using log4net;
using System.IO;


namespace SyncCyberPlan_lib
{
    public class AS400HelperTrigger
    {
        OdbcConnection _conn;
        OdbcCommand _cmdDB;
        OdbcDataReader _rd;
        string _sqlReadTrg;
        string _defTrigger;
        string _trgLibName;
        
        protected readonly string __SEP = ";"; //separatore
        
        protected static readonly ILog _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name);


        public AS400HelperTrigger(string pLibTrgAs400,
                                  string pConnString = @""
                                 )
        {
            _cmdDB = new OdbcCommand();
            _conn = new OdbcConnection();
            _trgLibName = pLibTrgAs400.ToUpper().Trim();
            _sqlReadTrg = @"Select CDKEY, DETRG FROM " + _trgLibName + ".XMLDTRG  ORDER BY DETRG DESC";

            if (pConnString.Trim() != "")
            {
                _conn.ConnectionString = pConnString;
            }
            else
            {
                //_conn.ConnectionString = @"Driver={Client Access ODBC Driver(32-bit)};System=isauro.sauro.dmn;Uid=DMZ;Pwd=6dmzadm;";
                //            _conn.ConnectionString = @"Driver={iSeries Access ODBC Driver};System=isauro.sauro.dmn;Uid=DMZ;Pwd=6dmzadm;";
                _conn.ConnectionString = @"Driver={iSeries Access ODBC Driver};System=isauro.sauro.dmn;Uid=DMZ;Pwd=6dmzadm;MGDSN=0;";
                //SSL CONNECTION
                //            _conn.ConnectionString = @"Driver={iSeries Access ODBC Driver};System=isauro.sauro.dmn;Uid=DMZ;Pwd=6dmzadm;SSL=1;";
                //CONNTYPE 1=READ/WRITE 2=READONLY  
                //            _conn.ConnectionString = @"Driver={iSeries Access ODBC Driver};System=isauro.sauro.dmn;Uid=DMZ;Pwd=6dmzadm;CONNTYPE=1;";
            }

            _conn.Open();
            _cmdDB = new OdbcCommand();
            _cmdDB.Connection = _conn;
            
            _cmdDB.CommandText = _defTrigger;
            _cmdDB.CommandType = System.Data.CommandType.Text;
        }

       

        public int ReadTrigger()
        {
            int result = -2;
            _logger.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name + " Lettura Tabella del Trigger");

            if (_conn.State != System.Data.ConnectionState.Open) OpenConnection();

            if (_conn.State != System.Data.ConnectionState.Open)
            {
                _logger.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name + "!!!! IMPOSSIBILE APRIRE CONNESSIONE SU AS400 !!!!");
                return -1;
            }

            
            _cmdDB.CommandText = _sqlReadTrg;
            
            _rd = _cmdDB.ExecuteReader();
            if (_rd.HasRows)
            {
                _rd.Read(); //mi basta leggere il primo record se la tabella trigger non è vuota
                            //recurero il valore della seconda colonna: DETRG

                if (!int.TryParse(_rd.GetValue(1).ToString().Trim(), out result))
                {   //NOn è stato possibile convertire in int. Imposto a 0 per reiniziare il conteggio 
                    return result = 0;
                }

            }
            else result = -1;

            // se return  -2=IMpossibile aprire la connessione -1:Tabella tRigger vuota  0=trovato carattere letterale o 0. quindi si reinizia il conteggio 0-9
            return result; 
           

        }

        

        public void OpenConnection()
        {
            _logger.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name + " ---start---");
            if (_conn == null)
            {
                throw new NullReferenceException("_connection è null");
            }
            if (_conn.State != ConnectionState.Open)
            {
                //_connection.ConnectionString = _connectionString;
                string connectionString = _conn.ConnectionString;
                int index_pwd = connectionString.IndexOf("Pwd=") + 4;
                if (index_pwd == 3)
                {
                    index_pwd = connectionString.IndexOf("Password=") + 9;
                }
                string public_cs = connectionString.Substring(0, index_pwd) + "XXXXXX;";
                _logger.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name + " _connection is not open: opening with cs = " + public_cs);
                _conn.Open();
            }
            _logger.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name + " ---end---");
        }

        public int ExecuteTrigger(int timeout = 10)
        {

            int res = -2;
            int _lastNrTrg = -2;
            _logger.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name + " Lettura Tabella del Trigger");

            if (_conn.State != System.Data.ConnectionState.Open) OpenConnection();

            if (_conn.State != System.Data.ConnectionState.Open)
            {
                _logger.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name + "!!!! IMPOSSIBILE APRIRE CONNESSIONE SU AS400 !!!!");
                //return -1;
            }

            _lastNrTrg = this.ReadTrigger();
            //Resetto il contatore. Mantengo 1 colonna
            if (_lastNrTrg == 9) _lastNrTrg = 0;

            _cmdDB = _conn.CreateCommand();
            _cmdDB.CommandType = CommandType.Text;

            if (_lastNrTrg == -1)
            {
                //Tabella trigger vuota. Inserisco il recordo
                _cmdDB.CommandText = @"INSERT INTO  " + _trgLibName + @".XMLDTRG  (CDKEY, DETRG) VALUES ('1','" + (_lastNrTrg).ToString() + @"') ";
            }
            else
            {
                //Tabella trigger con almeno un record. Vado ad aggionare
                _cmdDB.CommandText = @"UPDATE " + _trgLibName + @".XMLDTRG T  SET T.DETRG='" + (_lastNrTrg + 1).ToString() + @"' ";
            }
            if (timeout >= 0)
            {
                _cmdDB.CommandTimeout = timeout;
            }

            try
            {
                res = _cmdDB.ExecuteNonQuery();

                if (res == -1)
                {
                    //rollback
                    _logger.Error("rollback query \n" + _defTrigger);
                }
                else
                {
                    _logger.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name + " Eseguito Trigger su AS400");
                    //numero righe elaborate = res
                }
            }
            catch (Exception ex)
            {

                _logger.Error("", ex);
                throw ex;
            }
            _logger.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name + " end---");
            return res;


        }


    }
}
