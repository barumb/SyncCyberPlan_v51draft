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
    public class ExpDemand : ExportItem
    {
        protected string C_ORDER_CODE;
        protected string C_CORDER_CODE;
        protected string C_ITEM_CODE;
        protected string C_ITEM_PLANT;
        protected int C_OPNUM;
        protected int C_NSEQ;
        protected decimal C_QTY;
        protected decimal C_WDW_QTY;
        protected string C_MRP_TYPE;
        protected int C_STATUS;
        protected string C_REF_CORDER_CODE;
        protected DateTime? C_DUEDATE;

        public ExpDemand():base("EXP_DEMAND","DEM")
        {

        }
        protected override string WhereCondition()
        {
            throw new NotImplementedException();
        }

        public override void Init(object[] row)
        {
            C_ORDER_CODE                =getDBV<string> (row[0]);            
            C_CORDER_CODE               =getDBV<string> (row[1]);
            C_ITEM_CODE                 =getDBV<string> (row[2]);
            C_ITEM_PLANT                =getDBV<string> (row[3]);
            C_OPNUM                     =getDBV<int>    (row[4]);
            C_NSEQ                      =getDBV<int>    (row[5]);
            _task_number                =getDBV<int>    (row[6]);
            C_QTY                       =getDBV<decimal>(row[7]);
            C_WDW_QTY                   =getDBV<decimal>(row[8]);
            C_MRP_TYPE                  =getDBV<string> (row[9]);
            C_STATUS                    =getDBV<int>    (row[10]);
            C_REF_CORDER_CODE           =getDBV<string> (row[11]);
            C_DUEDATE                   =getSqlDate     (row[12]);
        }

        public override string getSageImportString()
        {
            throw new NotImplementedException();
        }

        protected override string GetSelectTaskNumberQuery()
        {
            return @"SELECT [C_CORDER_CODE]
      ,[C_ORDER_CODE]
      ,[C_ITEM_CODE]
      ,[C_ITEM_PLANT]
      ,[C_OPNUM]
      ,[C_NSEQ]
      ,[TASK_NUMBER]
      ,[C_QTY]
      ,[C_WDW_QTY]
      ,[C_MRP_TYPE]
      ,[C_STATUS]
      ,[C_REF_CORDER_CODE]
      ,[C_DUEDATE]

";
        }
    }
}
