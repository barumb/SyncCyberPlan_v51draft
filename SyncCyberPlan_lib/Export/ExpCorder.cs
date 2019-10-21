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

        public ExpCorder():base("ODVMRP")
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
            throw new NotImplementedException();
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
