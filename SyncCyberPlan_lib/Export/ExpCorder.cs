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
        protected string C_CODE;
        protected DateTime? C_PROMISE_DUEDATE;

        public ExpCorder():base("EXP_CORDER","ODVMRP")
        {

        }
        public override void Init(object[] row)
        {
            C_CODE = getDBV<string>(row[0]);
            _task_number = getDBV<int>(row[1]);            
            C_PROMISE_DUEDATE = getSqlDate(row[2]);
        }

        public override string getSageImportString()
        {
            throw new NotImplementedException();
        }

        protected override string GetSelectTaskNumberQuery()
        {
            return 
            @" SELECT [C_CODE]
            ,[TASK_NUMBER]
            ,[C_PROMISE_DUEDATE]
            ";
        }
        protected override string WhereCondition()
        {
            return "";
        }
    }
}
