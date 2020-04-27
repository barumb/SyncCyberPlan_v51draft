using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.Data;
using System.Data.Common;

namespace SyncCyberPlan_lib
{
    public class ExpCorder : ExportItem
    {
        protected string CORD_C_CODE;
        protected int CORD_task_number;
        protected DateTime? CORD_C_PROMISE_DUEDATE;

        public ExpCorder():base("ODVMRP", X3WSUtils.TipoImport.DATAODV)
        {

        }
        public override void Init(object[] row)
        {
            CORD_C_CODE = getDBV<string>(row[0], nameof(CORD_C_CODE));
            CORD_task_number = getDBV<int>(row[1], nameof(CORD_task_number));            
            CORD_C_PROMISE_DUEDATE = getSqlDate(row[2], nameof(CORD_C_PROMISE_DUEDATE));
        }

        public override string getSageImportString()
        {
            //IT001;IT001-ORE1710936;00022000;CVF080R3;0;0;0;7200;4019;20190923;N;ORE;17;10936;22
            return
                 "IT001"
                + __SEP + CORD_C_CODE
                + __SEP + "line"
                + __SEP + string.Format("{0:ddMMyy}", CORD_C_PROMISE_DUEDATE)
                ;
                
        }

        protected override string GetSelectQuery(int TaskNumber)
        {
            return
            @" SELECT [C_CODE]
            ,[TASK_NUMBER]
            ,[C_PROMISE_DUEDATE]            
            FROM[CyberPlanFrontiera].[dbo].[EXP_CORDER]  where TASK_NUMBER = " + TaskNumber + " " + WhereCondition();
        }
        protected override string WhereCondition()
        {
            return "";
        }

        public override void DeleteTaskNumber(int taskNumberToDelete)
        {
            string qry = @"DELETE FROM [CyberPlanFrontiera].[dbo].[EXP_CORDER] WHERE [TASK_NUMBER] = " + taskNumberToDelete;
            DBHelper2.EseguiSuDBCyberPlan(ref _db, qry);
            _logger.Info("task number deleted =" + taskNumberToDelete);

        }
    }
}
