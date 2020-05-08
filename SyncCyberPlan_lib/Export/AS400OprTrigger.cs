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
        string _libAS400;
        string _defTrigger;
        
        protected readonly string __SEP = ";"; //separatore
        
        protected static readonly ILog _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name);


        public AS400HelperTrigger(string pLibAs400,
                                  string pTriggerCmdSql,
                                  string pConnString = @""
                                 )
        {
            _cmdDB = new OdbcCommand();
            _conn = new OdbcConnection();
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
            
            _libAS400 = pLibAs400.Trim().ToUpper();
            _defTrigger = pTriggerCmdSql;
            _cmdDB.CommandText = _defTrigger;
            _cmdDB.CommandType = System.Data.CommandType.Text;
        }

        public void SetTrigger(string pDefTrigger)
        {
            _cmdDB.CommandText  = pDefTrigger;
        
        }

        public void ReadTrigger()
        {
            _logger.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name + " Lettura Tabella del Trigger");

            if (_conn.State != System.Data.ConnectionState.Open) OpenConnection();

            if (_conn.State != System.Data.ConnectionState.Open)
            {
                _logger.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name + "!!!! IMPOSSIBILE APRIRE CONNESSIONE SU AS400 !!!!");
                //return -1;
            }
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

        public int ExecuteTrigger( int timeout=10)
        {
            int res = -2;
            _logger.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name + " Attivazione trigger su AS400");
            
            if ( _conn.State!= System.Data.ConnectionState.Open) OpenConnection();

            if (_conn.State != System.Data.ConnectionState.Open)
            {
                _logger.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name + "!!!! IMPOSSIBILE APRIRE CONNESSIONE SU AS400 !!!!");
                return -1;
            }

            _cmdDB = _conn.CreateCommand( );
            if (timeout >= 0)
            {
                _cmdDB.CommandTimeout = timeout;
            }
            _cmdDB.CommandType = CommandType.Text;
            _cmdDB.CommandText = _defTrigger;
            _logger.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name + " Command created : " + _defTrigger);

            try
            {
                res = _cmdDB.ExecuteNonQuery();
                
                if (res == -1 )
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
