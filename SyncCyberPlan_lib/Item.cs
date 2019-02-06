using System.Collections.Generic;
using System;
using log4net;
using System.Data;

namespace SyncCyberPlan_lib
{
    public abstract class Item
    {
        // inizializzo logger di questa classe
        protected static readonly ILog _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name);
        protected static string __bulk_message="";

        public static string __MAGAZZINO_INTERNO = "Interno";
        public static string __MAGAZZINO_ESTERNO = "Esterno";
        public string _CP_tabella;

        static public int NUMLASTDAYS;
        static public DataTable _dataTable;
        static private string _table_created = "";
        public Item(string CyberPLan_tabella)
        {
            _CP_tabella = CyberPLan_tabella;
            InitTable();
        }
       
        void InitTable()
        {
            if (_table_created != _CP_tabella)
            {
                _dataTable = new DataTable(_CP_tabella);
                InitDataTable();
                _table_created = _CP_tabella;
            }
        }
        public abstract string GetID();        
        public abstract void Init(object[] row);
        public abstract DataRow GetCyberRow();
        /// <summary>
        /// serve per aggiungere eventuali righe residue alla fine
        /// </summary>
        /// <returns></returns>
        public virtual List<DataRow> GetLastCyberRows()
        {
            return null;
        }
        public void AddCyberRow()
        {
            DataRow row = GetCyberRow();
            if (row != null)
            {
                //_dataTable.Rows.Add(GetCyberRow());
                _dataTable.Rows.Add(row.ItemArray);
            }
            else {
                ;
            }
        }
        public int AddLastCyberRows()
        {
            List<DataRow> rows = GetLastCyberRows();
            int i = 0;
            if (rows != null)
            {
                foreach (DataRow row in rows)
                {
                    DataRow tmp = _dataTable.NewRow(); 
                    tmp.ItemArray = row.ItemArray.Clone() as object[];
                    _dataTable.Rows.Add(tmp);
                    i++;
                }
            }
            else
            {
                ;
            }
            return i;
        }
        public virtual void LastAction(ref DBHelper2 cm)
        {

        }
        public void DoLastAction(ref DBHelper2 cm)
        {
            LastAction(ref cm);
        }
        public abstract void InitDataTable();
        public abstract string GetSelectQuery(bool mode, string dossier, string codice_like, string tipo);        
        //public abstract string To_CYBERDB();
        //public abstract string To_CYBERDB_QueryHeader_OLD_non_più_usata();
        //public abstract string To_CYBERDB_QueryValues_OLD_non_più_usata();
        protected string EscapeSQL(string field, int strlen)
        {
            string ret = null;
            if (field != null)
            {
                ret = field.Replace("'", "''");  //escape del singolo apice ' : si mette due volte ''
                ret = ret.Replace("  ", " ");

                if (ret.Length > strlen)
                {
                    ret = ret.Substring(0, strlen);
                }
            }
            return ret;
        }

        protected string SqlDate_fromDateTime(DateTime? dt)
        {
            string ret = "";
            if (dt is null)
            {
                ret = "null";
            }
            else
            {
                ret = " '" + dt.Value.ToString("MM/dd/yyyy hh:mm:ss") + "' ";
            }
            return ret;
        }
        protected object DateTime_toCyb(DateTime? data)
        {
            return data == null ? (object)DBNull.Value : (object)data;
        }
        protected DateTime? dateTime_fromDataAs400(decimal data_as400)
        {
            DateTime? ret = null;
            if(data_as400>10000)
            {
                ret = new DateTime((int)data_as400/10000, (int)data_as400/100%100, (int)data_as400%100);
            }
            return ret;
        }
        protected object colTime_fromDataAs400(decimal data_as400)
        {
            DateTime? ret = null;
            if (data_as400 > 10000)
            {
                ret = new DateTime((int)data_as400 / 10000, (int)data_as400 / 100 % 100, (int)data_as400 % 100);
            }
            else {
                return DBNull.Value;
            }
            return ret;
        }
        public virtual string GetDeletedAllQuery()
        {
            return "DELETE FROM [CyberPlanFrontiera].[dbo].[" + _CP_tabella + "] where 1=1 ";
        }
        /// <summary>
        /// GetDBValue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected T getDBV<T>(object obj)
        {
            T ret;
            if (!DBNull.Value.Equals(obj))
            {
                ret = (T)obj;
                if (typeof(T) == typeof(String))
                {
                    string tmp = (string)obj;
                    if (tmp.Length > 1) //se = 1 potrebbe essere un solo carattere (spazio) di un campo char(1), e non va cancellato
                    {
                        ret = (T)(object)tmp.Trim();
                    }
                }
            }
            else
            {
                ret = default(T);
                _logger.Debug(GetID() + " campo " + typeof(T).ToString() + " is DBnull ");
            }
            return ret;
        }
        protected DateTime? getSageDate(DateTime data)
        {
            if (data == System.Data.SqlTypes.SqlDateTime.MinValue)
            {
                return null;
            }
            else return data;
        }
        static public T GetDBV<T>(object obj)
        {
            T ret ;
            if (!DBNull.Value.Equals(obj))
            {
                ret = (T)obj;
                if (typeof(T) == typeof(String))
                {
                    string tmp = (string)obj;
                    ret =(T)(object) tmp.Trim();
                }
            }
            else
            {
                ret = default(T);
                _logger.Debug(" campo " + typeof(T).ToString()+ " is DBnull ");
            }
            return ret;
        }
        public static void SetLastDays(int lastdays)
        {
            Item.NUMLASTDAYS = lastdays;
        }
        public static void ResetMailMessage()
        {
            __bulk_message = "";
        }
    }
}
