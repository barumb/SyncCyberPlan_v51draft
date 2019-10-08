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
    public abstract class ExpOrder : ExportItem
    {
        protected string C_CODE;
        protected string C_CORDER_CODE;
        protected string C_ITEM_CODE;
        protected string C_ITEM_PLANT;
        protected string C_M_B;
        protected string C_MRP_TYPE;
        protected decimal C_QTY;
        protected decimal C_COMPL_QTY;
        protected DateTime? C_INSERT_DATE;
        protected string C_SHOP_FLOOR_CODE;
        protected int C_STATUS;
        protected string C_SUPPLIER_CODE;
        protected string C_WAREHOUSE_CODE;
        protected DateTime? C_STDATE;
        protected DateTime? C_DUEDATE;

        public ExpOrder():base("EXP_ORDER","")
        {

        }
        
        public override void Init(object[] row)
        {
            C_CODE                    = getDBV<string>(row[0]);
            _task_number              = getDBV<int>(row[1]);
            C_CORDER_CODE             = getDBV<string>(row[2]);
            C_ITEM_CODE               = getDBV<string>(row[3]);
            C_ITEM_PLANT              = getDBV<string>(row[4]);
            C_M_B                     = getDBV<string>(row[5]);
            C_MRP_TYPE                = getDBV<string>(row[6]);
            C_QTY                     = getDBV<decimal>(row[7]);
            C_COMPL_QTY               = getDBV<decimal>(row[8]);
            C_INSERT_DATE             = getSqlDate(row[9]);
            C_SHOP_FLOOR_CODE         = getDBV<string>(row[10]);
            C_STATUS                  = getDBV<int>(row[11]);
            C_SUPPLIER_CODE           = getDBV<string>(row[12]);
            C_WAREHOUSE_CODE          = getDBV<string>(row[13]);
            C_STDATE                  = getSqlDate(row[14]);
            C_DUEDATE                 = getSqlDate(row[15]);
        }

        protected override string GetSelectTaskNumberQuery()
        {
            return @"SELECT [C_CODE]
      ,[TASK_NUMBER]
      ,[C_CORDER_CODE]
      ,[C_ITEM_CODE]
      ,[C_ITEM_PLANT]
      ,[C_M_B]
      ,[C_MRP_TYPE]
      ,[C_QTY]
      ,[C_COMPL_QTY]
      ,[C_INSERT_DATE]
      ,[C_SHOP_FLOOR_CODE]
      ,[C_STATUS]
      ,[C_SUPPLIER_CODE]
      ,[C_WAREHOUSE_CODE]
      ,[C_STDATE]
      ,[C_DUEDATE]
";
        }
    }
}
