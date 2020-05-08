using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.SqlClient;
using log4net;
using System.IO;

namespace SyncCyberPlan_lib
{
    public class DBHelper2
    {
        // inizializzo logger di questa classe
        protected static readonly ILog _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name);

        //protected string _connectionString;		
        protected string _libreria_dossier;
        protected DbConnection _connection;
        protected DbCommand _cmdSql;

        public string LibreriaDossier { get { return _libreria_dossier; } }

        // Server=myServerName\myInstanceName;Database=myDataBase;User Id=myUsername; Password=myPassword;
        public DBHelper2(DbConnection conn)
		{
            if (conn == null)
            {
                throw new ArgumentNullException("conn", "parametro passato è null");
            }
            else
            {
                _connection = conn;
            }
			_cmdSql = null;
		}		
		public void OpenConnection()
		{
			_logger.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name + " ---start---");
			if (_connection == null)
			{
                throw new NullReferenceException("_connection è null");
			}						
			if (_connection.State != ConnectionState.Open)
			{
				//_connection.ConnectionString = _connectionString;
                string connectionString = _connection.ConnectionString;
				int index_pwd = connectionString.IndexOf("Pwd=") + 4;
				if (index_pwd == 3)
				{
					index_pwd = connectionString.IndexOf("Password=") + 9;
				}
				string public_cs = connectionString.Substring(0, index_pwd) + "XXXXXX;";
				_logger.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name + " _connection is not open: opening with cs = " + public_cs);
				_connection.Open();
			}
			_logger.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name + " ---end---");
		}
		
		public DbDataReader GetReaderSelectCommand(string command)
		{
			_logger.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name + " start---");
            _connection.Close();
			OpenConnection();

			_cmdSql = _connection.CreateCommand();
			_cmdSql.CommandType = CommandType.Text;
			_cmdSql.CommandText = command;
			_logger.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name + " Command created : " + command);

			try
			{
				return _cmdSql.ExecuteReader();
			}
			catch (Exception ex)
			{
				_logger.Error("", ex);
			}
			_logger.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name + " end---");
			return null;
		}
        public int ExecuteCommand(string command)
        {
            return ExecuteCommand(command, -1);
        }
        public int ExecuteCommand(string command, int timeout)
        {
            int res =-2;
            _logger.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name + " start---");
            OpenConnection();

            _cmdSql = _connection.CreateCommand();
            if (timeout >= 0)
            {
                _cmdSql.CommandTimeout = timeout;
            }
            _cmdSql.CommandType = CommandType.Text;
            _cmdSql.CommandText = command;
            _logger.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name + " Command created : " + command);

            try
            {
                res = _cmdSql.ExecuteNonQuery();
                if (res == -1 && (command.Contains("UPDATE ") || command.Contains("INSERT ") || command.Contains("DELETE ") ))
                {
                    //rollback
                    _logger.Error("rollback query \n" + command);
                }
                else
                {
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

        public void WriteToCyberPlan<T>(bool mode_all, string codice_like, string filtro, bool delete, string option) where T : Item, new()
        {
            Item.ResetMailMessage();
            string startedAt = DateTime.Now.ToString();
            string message_error = "Command started at " + startedAt + "  " +
                "\n Parameters: mode_all=" + mode_all + ", codice_like=" + codice_like + ", filtro=" + filtro + ", delete=" + delete + ", option=" + option + "\n\n";
            _logger.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name + " --- started at " + startedAt);
            T tmp = new T();
            _logger.Info("Oggetto: " + typeof(T).ToString().PadRight(60) + " su tabella " + tmp._CP_tabella);
            DBHelper2 cm = DBHelper2.getCyberDBHelper();
            if (delete)
            {
                DBHelper2.EseguiSuDBCyberPlan(ref cm, tmp.GetDeletedAllQuery());
                _logger.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name + " ---   deleting ended at " + DateTime.Now.ToString());
                return;
            }

            //if (string.IsNullOrWhiteSpace(codice_like))
            //{
            //    codice_like = "%";
            //}

            //string qry = tmp.GetSelectQuery(mode_all, _libreria_dossier+"TEST", codice_like, filtro);

            _logger.Info("Oggetto: " + typeof(T).ToString().PadRight(60) + " Disab Blocco chifra suffisso TEST a " + _libreria_dossier );
            string qry = tmp.GetSelectQuery(mode_all, _libreria_dossier , codice_like, filtro);


            DbDataReader dtr = GetReaderSelectCommand(qry);
            object[] row = new object[dtr.FieldCount];

            int i = 0;
            int j = 0;

            while (dtr.Read())
            {
                i++;
                j++;
                dtr.GetValues(row);
                //tmp.Init__AddRow(row);
                tmp.Init(row);
                tmp.AddCyberRow();//tmp.GetRow(row);

                if (j % 1000 == 0)
                {
                    //int res= DBHelper2.EseguiSuDBCyberPlan(ref cm, tmp_qry);
                    int res = DBHelper2.EseguiSuDBCyberPlan_Bulk(ref cm, tmp._CP_tabella, Item._dataTable);
                    _logger.Info(i + " items... [" + tmp.GetID() + "]");
                    j = 0;
                }
            }
            int added = tmp.AddLastCyberRows();
            if (j > 0 || added > 0)
            {
                //DBHelper2.EseguiSuDBCyberPlan(ref cm, tmp_qry);
                int res = DBHelper2.EseguiSuDBCyberPlan_Bulk(ref cm, tmp._CP_tabella, Item._dataTable);
                _logger.Info(i + added + " items [last= " + tmp.GetID() + "]");
            }
            tmp.DoLastAction(ref cm, this);
            dtr.Close();

            //if (thereIsMessage)
            //{
            //    Utils.SendMail("it@sauro.net", "francesco.chiminazzo@sauro.net", message_error);
            //    //Utils.SendMail("it@sauro.net", "francesco.chiminazzo@sauro.net,enrico.lidacci@sauro.net", message_error);
            //}

            _logger.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name + " ---   ended at " + DateTime.Now.ToString());
        }

        void scriviRiga_WL(StreamWriter sw, string str)
        {
            if (!string.IsNullOrWhiteSpace(str))
            {
                sw.WriteLine(str);
            }
        }
        void scriviRiga(StreamWriter sw, string str)
        {
            if (!string.IsNullOrWhiteSpace(str))
            {
                sw.Write(str);
            }
        }

        public static DBHelper2 getSageDBHelper(string dossier)
        {
            DBHelper2 ret = null;
            _logger.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name + " --- started");
            dossier = dossier.Trim().ToUpper();
            if (dossier == "SAURO" || dossier == "SAURODEV" || dossier == "SAUROTEST" || dossier == "SAUROINT")
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = @"Server=srvx3app1\SAGEX3;Database=x3;User Id=" + dossier + "; Password=tiger;";
                ret = new DBHelper2(conn);
                ret._libreria_dossier = dossier;
            }
            else
            {
                throw new ArgumentException("dossier non previsto: " + dossier);
            }

            _logger.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name + " ---   ended");
            return ret;
        }
        public static DBHelper2 getAs400DBHelper(string libreria)
        {
            _logger.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name + " --- started");
            DBHelper2 ret = null;
            libreria = libreria.Trim().ToUpper();
            if (libreria == "MBM41LIB_M" || libreria == "MBM41LIBMT")
            {
                OdbcConnection conn = new OdbcConnection();
                conn.ConnectionString = "Driver={Client Access ODBC Driver (32-bit)};System=isauro.sauro.dmn;Uid=DMZ;Pwd=6dmzadm;";
                ret = new DBHelper2(conn);
                ret._libreria_dossier = libreria;
            }
            else
            {
                throw new ArgumentException("libreria non previsto: " + libreria);
            }
            _logger.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name + " ---   ended");
            return ret;
        }
        public static DBHelper2 getCyberDBHelper()
        {
            DBHelper2 ret = null;
            _logger.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name + " --- started");

            SqlConnection conn = new SqlConnection();
                conn.ConnectionString = @"Server=srvsql1,1433\MSSQLSERVER;Database=CyberPlanFrontiera;User Id=Cyberplan; Password=C18£3r:okboo;";
                ret = new DBHelper2(conn);

            _logger.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name + " ---   ended");
            return ret;
        }
        static public int EseguiSuDBCyberPlan(ref DBHelper2 cm, string query)
        {
            return EseguiSuDBCyberPlan(ref cm, query, -1);
        }
        static public int EseguiSuDBCyberPlan(ref DBHelper2 cm, string query, int timeout)
        {
#if DEBUG
            //return 1;
#endif
            int ret = -99;
            if (!string.IsNullOrWhiteSpace(query))
            {
                if (cm._connection.State != ConnectionState.Open)
                {
                    cm._connection.Open();
                }
                ret = cm.ExecuteCommand(query, timeout);

                cm._connection.Close();
            }
            return ret;
        }
        static int EseguiSuDBCyberPlan_Bulk(ref DBHelper2 cm, string tableName, DataTable dataTable)
        {
#if DEBUG
            return 1;
#endif
            if (cm._connection.State != ConnectionState.Open)
            {
                cm._connection.Open();
            }
            SqlBulkCopy bulk = new SqlBulkCopy(
                        (SqlConnection)cm._connection,
                        SqlBulkCopyOptions.TableLock |
                        SqlBulkCopyOptions.FireTriggers |
                        SqlBulkCopyOptions.UseInternalTransaction,
                        null
                        );
            bulk.DestinationTableName = tableName;            
            bulk.WriteToServer(dataTable);

            cm._connection.Close();
            dataTable.Clear();
            return 1;
        }
    }
}
