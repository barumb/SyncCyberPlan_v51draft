using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.Data;
using System.Data.Common;
using System.IO;

namespace SyncCyberPlan_lib
{
    public abstract class ExportItem
    {
        protected readonly string __SEP = ","; //separatore
        protected static readonly ILog _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name);

        protected string _CyberPLan_tabella;
        protected string _file_prefix;
        protected string _dossier;
        protected int? _task_number = null;
        protected DBHelper2 _db = null;

        public int? TaskNumber { get { return _task_number; } }

        public ExportItem(string CyberPLan_tabella, string file_prefix)
        {
            _CyberPLan_tabella = CyberPLan_tabella;
            _file_prefix = file_prefix;
            _db = DBHelper2.getCyberDBHelper();
        }

        public string GetQuery()
        {
            if (_task_number.HasValue)
            {
                return GetSelectTaskNumberQuery()
                    + "FROM [CyberPlanFrontiera].[dbo].[" + _CyberPLan_tabella + "] " +
                    " where TASK_NUMBER=" + _task_number + " " + WhereCondition();
            }
            else
            {
                throw new NullReferenceException("TaskNumber is null");
            }
        }
        protected abstract string GetSelectTaskNumberQuery();
        protected abstract string WhereCondition();
        public abstract void Init(object[] row);
        public abstract string getSageImportString();
       

        public void RefreshFirstTaskNumber()
        {
            string qry = @"SELECT top 1 [TASK_NUMBER] FROM [CyberPlanFrontiera].[dbo].[" + _CyberPLan_tabella + "] order by TASK_NUMBER asc";
            //DBHelper2.EseguiSuDBCyberPlan(ref _db, qry);
            DbDataReader dtr = _db.GetReaderSelectCommand(qry);
            object[] row = new object[dtr.FieldCount];

            if (dtr.HasRows)
            {
                while (dtr.Read())
                {
                    dtr.GetValues(row);
                    _task_number = (int)row[0];
                    _logger.Debug("Refresh task number=" + _task_number);
                    break;
                }
            }
            else
            {
                _logger.Debug("task number = null");
            }
            dtr.Close();
        }
        public string WriteToFile(string dossier, int taskNumberToExport) //where T : ExportItem, new()
        {
            _logger.Debug(this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "  start...");
            string pathfile = null;
            string startedAt = DateTime.Now.ToString();
            if (_task_number.HasValue && _task_number.Value== taskNumberToExport)
            {
                _logger.Info("Oggetto: " + this.GetType().ToString() + " su tabella " + _CyberPLan_tabella);

                string qry = GetQuery();
                DbDataReader dtr = _db.GetReaderSelectCommand(qry);
                object[] row = new object[dtr.FieldCount];

                int i = 0;

                if (dtr.HasRows)
                {
                    string timing = string.Format("{0:yyMMdd_hhmmss}", System.DateTime.Now);
                    //pathfile = @"S:\Sage\SAGEX3\folders\" + dossier + @"\YSAURO\IMPEXP\MRP\" + _file_prefix + "_task" + _task_number + "_" + timing + ".txt";
                    pathfile = @"\\srvx3app1\S$\Sage\SAGEX3\folders\" + dossier + @"\YSAURO\IMPEXP\MRP\" + _file_prefix + "_task" + _task_number + "_" + timing + ".txt";
                    using (System.IO.StreamWriter fs = new System.IO.StreamWriter(pathfile, false))
                    {
                        while (dtr.Read())
                        {
                            i++;
                            dtr.GetValues(row);
                            Init(row);
                            fs.WriteLine(getSageImportString());
                            //_logger.Info(i + " items... [" + tmp.GetID() + "]");
                        }
                    }
                }
                dtr.Close();


                //if (thereIsMessage)
                //{
                //    Utils.SendMail("it@sauro.net", "francesco.chiminazzo@sauro.net", message_error);
                //    //Utils.SendMail("it@sauro.net", "francesco.chiminazzo@sauro.net,enrico.lidacci@sauro.net", message_error);
                //}
                _logger.Info("esportato file " + pathfile);
            }
            else
            {
                _logger.Debug("Export vuoto; taskNumberToExport=" + taskNumberToExport);
            }
            _logger.Debug(this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "  end");
            return pathfile;
        }
        /// <summary>
        /// Cancella tutte le righe della tabella con TaskNumber corrente 
        /// per sicurezza si fa passare il TaskNumber e controlla che coincida con il corrente
        /// </summary>
        /// <param name="taskNumberToDelete"></param>
        public void DeleteTaskNumber(int taskNumberToDelete)
        {
            if (_task_number.HasValue && _task_number == taskNumberToDelete)
            {
                string qry = @"DELETE FROM [CyberPlanFrontiera].[dbo].[" + _CyberPLan_tabella + "] WHERE [TASK_NUMBER] = " + _task_number;
                DBHelper2.EseguiSuDBCyberPlan(ref _db, qry);
                _logger.Info("task number deleted =" + _task_number);
            }
        }

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
                _logger.Debug("un valore di " + typeof(T).ToString() + " is DBnull ");
            }
            return ret;
        }
        protected  DateTime? getSqlDate(object data)
        {
            if (data is System.DBNull)
            {
                return null;
            }
            DateTime tmp = (DateTime)data;
            if (tmp == System.Data.SqlTypes.SqlDateTime.MinValue)
            {
                return null;
            }
            else return tmp;
        }

    }
}
