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
    public class ExpOperation : ExportItem
    {
        protected string    C_ORDER_CODE     ;
        protected int       C_OPNUM          ;
        //protected int       TASK_NUMBER      ;
        protected string    C_DESCR          ;
        protected Decimal   C_QTY            ;
        protected DateTime? C_STDATE         ;
        protected DateTime? C_DUEDATE        ;
        protected int       C_QUEUE_TIME     ;
        protected int       C_WAIT_TIME      ;
        protected int       C_SETUP_TIME     ;
        protected int       C_RUN_TIME       ;
        protected string    C_WORKCENTER_CODE;
        protected string    C_SUPPLIER_CODE  ;
        protected string    C_ATTREZZATURA   ;
        protected string    C_MACHINE        ;
        protected int       C_SEQUENCE       ;




        public ExpOperation():base("EXP_OPERATION","OPE")
        {

        }


        public override void Init(object[] row)
        {
            C_ORDER_CODE       = getDBV<string>(row[0]);
            C_OPNUM            = getDBV<int>   (row[1]);
            _task_number       = getDBV<int>   (row[2]);
            C_DESCR            = getDBV<string>(row[3]);
            C_QTY              = getDBV<Decimal>(row[4]);
            C_STDATE           = getSqlDate    (row[5]);
            C_DUEDATE          = getSqlDate    (row[6]);
            C_QUEUE_TIME       = getDBV<int>   (row[7]);
            C_WAIT_TIME        = getDBV<int>   (row[8]);
            C_SETUP_TIME       = getDBV<int>   (row[9]);
            C_RUN_TIME         = getDBV<int>   (row[10]);
            C_WORKCENTER_CODE  = getDBV<string>(row[11]);
            C_SUPPLIER_CODE    = getDBV<string>(row[12]);
            C_ATTREZZATURA     = getDBV<string>(row[13]);
            C_MACHINE          = getDBV<string>(row[14]);
            C_SEQUENCE         = getDBV<int>   (row[15]);
        }
        protected override string WhereCondition()
        {
            throw new NotImplementedException();
        }
        public override string getSageImportString()
        {
            throw new NotImplementedException(); 
        }

        protected override string GetSelectTaskNumberQuery()
        {
            return @"SELECT [C_ORDER_CODE]
      ,[C_OPNUM]
      ,[TASK_NUMBER]
      ,[C_DESCR]
      ,[C_QTY]
      ,[C_STDATE]
      ,[C_DUEDATE]
      ,[C_QUEUE_TIME]
      ,[C_WAIT_TIME]
      ,[C_SETUP_TIME]
      ,[C_RUN_TIME]
      ,[C_WORKCENTER_CODE]
      ,[C_SUPPLIER_CODE]
      ,[C_ATTREZZATURA]
      ,[C_MACHINE]
      ,[C_SEQUENCE]";
        }
    }
}
