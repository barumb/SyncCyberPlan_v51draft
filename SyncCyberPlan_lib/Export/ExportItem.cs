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
        protected readonly string __SEP = ";"; //separatore
        protected static readonly ILog _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name);

        public X3WSUtils.TipoImport Tipo;
        protected string _file_prefix;
        protected string _dossier;
        protected DBHelper2 _db = null;

        public ExportItem(string file_prefix, X3WSUtils.TipoImport tipo)
        {
            _file_prefix = file_prefix;
            Tipo = tipo;
            _db = DBHelper2.getCyberDBHelper();
        }

        protected abstract string GetSelectQuery(int TaskNumber);
        protected abstract string WhereCondition();
        public abstract void Init(object[] row);
        public abstract string getSageImportString();



        public string WriteToFile(string dossier, int taskNumberToExport) //where T : ExportItem, new()
        {
            _logger.Debug(this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name +  " dossier=" + dossier + " NrTask=" + taskNumberToExport.ToString() + "    start...");
            string pathfile = null;
            string startedAt = DateTime.Now.ToString();

            //_logger.Debug("Oggetto: " + this.GetType().ToString());

            string qry = GetSelectQuery(taskNumberToExport);
            DbDataReader dtr = _db.GetReaderSelectCommand(qry);
            object[] row = new object[dtr.FieldCount];

            int i = 0;

            if (dtr.HasRows)
            {
                string timing = string.Format("{0:yyMMdd_hhmmss}", System.DateTime.Now);
                //release
                pathfile = @"\\srvx3app1\" + dossier + @"\IMPEXP\MRP\" + _file_prefix + "_task" + taskNumberToExport + "_" + timing + ".txt";
                //debug
                //pathfile = @"\\srvx3app1\S$\Sage\SAGEX3\folders\" + dossier + @"\YSAURO\IMPEXP\MRP\" + _file_prefix + "_task" + taskNumberToExport + "_" + timing + ".txt";
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
            if (pathfile == null)
            {
                _logger.Info(this.GetType().Name + " TaskNumber=" + taskNumberToExport + " - nulla da esportare");
            }
            else
            {
                _logger.Info("TaskNumber=" + taskNumberToExport + " - esportato file " + pathfile);
            }

            _logger.Debug(this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "  end");
            return pathfile;
        }
        /// <summary>
        /// Cancella tutte le righe della tabella con TaskNumber corrente 
        /// per sicurezza si fa passare il TaskNumber e controlla che coincida con il corrente
        /// </summary>
        /// <param name="taskNumberToDelete"></param>
        public abstract void DeleteTaskNumber(int taskNumberToDelete);

        protected T getDBV<T>(object obj, string param_name)
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
                //_logger.Debug("valore di " + (param_name + "  " + typeof(T).ToString()).Trim() + " is DBnull ");
                _logger.Debug("valore DBnull di " + param_name );
            }
            return ret;
        }
        protected  DateTime? getSqlDate(object data, string param_name)
        {
            if (data is System.DBNull)
            {
                return null;
            }
            DateTime tmp = (DateTime)data;
            if (tmp == System.Data.SqlTypes.SqlDateTime.MinValue)
            {
                _logger.Debug("valore DBnull di " + param_name);
                return null;
            }
            else return tmp;
        }



        public static int? GetMinTaskNumber()
        {
            int? task_number = null;

            string qry =
@"      select * from (select top 1 TASK_NUMBER from [CyberPlanFrontiera].[dbo].EXP_CORDER order by TASK_NUMBER asc) A
union select * from (select top 1 TASK_NUMBER from [CyberPlanFrontiera].[dbo].EXP_ORDER order by TASK_NUMBER asc) B
union select * from (select top 1 TASK_NUMBER from [CyberPlanFrontiera].[dbo].EXP_OPERATION order by TASK_NUMBER asc) C
union select * from (select top 1 TASK_NUMBER from [CyberPlanFrontiera].[dbo].EXP_DEMAND order by TASK_NUMBER asc) D ";



            DBHelper2 db = DBHelper2.getCyberDBHelper();
            DbDataReader dtr = db.GetReaderSelectCommand(qry);
            object[] row = new object[dtr.FieldCount];

            if (dtr.HasRows)
            {
                while (dtr.Read())
                {
                    dtr.GetValues(row);
                    task_number = (int)row[0];
                    _logger.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name + " Refresh min task number=" + task_number);
                    break;
                }
            }
            else
            {
                _logger.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name + " task number = null");
            }
            dtr.Close();
            return task_number;
        }
    }
}
