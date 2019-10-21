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
        protected string         ORD_C_CODE;
		protected int            ORD_task_number;
        protected string         ORD_C_CORDER_CODE;
        protected string         ORD_C_ITEM_CODE;
        protected string         ORD_C_ITEM_PLANT;
        protected string         ORD_C_M_B;
        protected string         ORD_C_MRP_TYPE;
        protected decimal        ORD_C_QTY;
        protected decimal        ORD_C_COMPL_QTY;
        protected DateTime?      ORD_C_INSERT_DATE;
        protected string         ORD_C_SHOP_FLOOR_CODE;
        protected int            ORD_C_STATUS;
        protected string         ORD_C_SUPPLIER_CODE;
        protected string         ORD_C_WAREHOUSE_CODE;
        protected DateTime?      ORD_C_STDATE;
        protected DateTime?      ORD_C_DUEDATE;
		
        protected string         OPE_C_ORDER_CODE     ;
        protected int            OPE_C_OPNUM          ;
        protected int            OPE_task_number      ;
        protected string         OPE_C_DESCR          ;
        protected Decimal        OPE_C_QTY            ;
        protected DateTime?      OPE_C_STDATE         ;
        protected DateTime?      OPE_C_DUEDATE        ;
        protected int            OPE_C_QUEUE_TIME     ;
        protected int            OPE_C_WAIT_TIME      ;
        protected int            OPE_C_SETUP_TIME     ;
        protected int            OPE_C_RUN_TIME       ;
        protected string         OPE_C_WORKCENTER_CODE;
        protected string         OPE_C_SUPPLIER_CODE  ;
        protected string         OPE_C_ATTREZZATURA   ;
        protected string         OPE_C_MACHINE        ;
        protected int            OPE_C_SEQUENCE       ;
		
        protected string         DEM_C_ORDER_CODE;
        protected string         DEM_C_CORDER_CODE;
        protected string         DEM_C_ITEM_CODE;
        protected string         DEM_C_ITEM_PLANT;
        protected int            DEM_C_OPNUM;
        protected int            DEM_C_NSEQ;
		protected int            DEM_task_number;
        protected decimal        DEM_C_QTY;
        protected decimal        DEM_C_WDW_QTY;
        protected string         DEM_C_MRP_TYPE;
        protected int            DEM_C_STATUS;
        protected string         DEM_C_REF_CORDER_CODE;
        protected DateTime?      DEM_C_DUEDATE;

        public ExpOrder(string file_prefix) : base(file_prefix)
        {

        }

        public override void Init(object[] row)
        {
            ORD_C_CODE             = getDBV<string>(row[0],   nameof(ORD_C_CODE));
            _logger.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name + " Ordine " + ORD_C_CODE);
            ORD_task_number        = getDBV<int>(row[1],      nameof(ORD_task_number));
            ORD_C_CORDER_CODE      = getDBV<string>(row[2],   nameof(ORD_C_CORDER_CODE));
            ORD_C_ITEM_CODE        = getDBV<string>(row[3],   nameof(ORD_C_ITEM_CODE));
            ORD_C_ITEM_PLANT       = getDBV<string>(row[4],   nameof(ORD_C_ITEM_PLANT));
            ORD_C_M_B              = getDBV<string>(row[5],   nameof(ORD_C_M_B));
            ORD_C_MRP_TYPE         = getDBV<string>(row[6],   nameof(ORD_C_MRP_TYPE));
            ORD_C_QTY              = getDBV<decimal>(row[7],  nameof(ORD_C_QTY));
            ORD_C_COMPL_QTY        = getDBV<decimal>(row[8],  nameof(ORD_C_COMPL_QTY));
            ORD_C_INSERT_DATE      = getSqlDate(row[9],       nameof(ORD_C_INSERT_DATE));
            ORD_C_SHOP_FLOOR_CODE  = getDBV<string>(row[10],  nameof(ORD_C_SHOP_FLOOR_CODE));
            ORD_C_STATUS           = getDBV<int>(row[11],     nameof(ORD_C_STATUS));
            ORD_C_SUPPLIER_CODE    = getDBV<string>(row[12],  nameof(ORD_C_SUPPLIER_CODE));
            ORD_C_WAREHOUSE_CODE   = getDBV<string>(row[13],  nameof(ORD_C_WAREHOUSE_CODE));
            ORD_C_STDATE           = getSqlDate(row[14],      nameof(ORD_C_STDATE));
            ORD_C_DUEDATE          = getSqlDate(row[15],      nameof(ORD_C_DUEDATE));

            OPE_C_ORDER_CODE       = getDBV<string>(row[16],  nameof(OPE_C_ORDER_CODE));
            OPE_C_OPNUM            = getDBV<int>   (row[17],  nameof(OPE_C_OPNUM));
            OPE_task_number        = getDBV<int>   (row[18],  nameof(OPE_task_number));
            OPE_C_DESCR            = getDBV<string>(row[19],  nameof(OPE_C_DESCR));
            OPE_C_QTY              = getDBV<Decimal>(row[20], nameof(OPE_C_QTY));
            OPE_C_STDATE           = getSqlDate    (row[21],  nameof(OPE_C_STDATE));
            OPE_C_DUEDATE          = getSqlDate    (row[22],  nameof(OPE_C_DUEDATE));
            OPE_C_QUEUE_TIME       = getDBV<int>   (row[23],  nameof(OPE_C_QUEUE_TIME));
            OPE_C_WAIT_TIME        = getDBV<int>   (row[24],  nameof(OPE_C_WAIT_TIME));
            OPE_C_SETUP_TIME       = getDBV<int>   (row[25],  nameof(OPE_C_SETUP_TIME));
            OPE_C_RUN_TIME         = getDBV<int>   (row[26],  nameof(OPE_C_RUN_TIME));
            OPE_C_WORKCENTER_CODE  = getDBV<string>(row[27],  nameof(OPE_C_WORKCENTER_CODE));
            OPE_C_SUPPLIER_CODE    = getDBV<string>(row[28],  nameof(OPE_C_SUPPLIER_CODE));
            OPE_C_ATTREZZATURA     = getDBV<string>(row[29],  nameof(OPE_C_ATTREZZATURA));
            OPE_C_MACHINE          = getDBV<string>(row[30],  nameof(OPE_C_MACHINE));
            OPE_C_SEQUENCE         = getDBV<int>   (row[31],  nameof(OPE_C_SEQUENCE));


            DEM_C_ORDER_CODE       = getDBV<string> (row[32], nameof(DEM_C_ORDER_CODE)); 
            DEM_C_CORDER_CODE      = getDBV<string> (row[33], nameof(DEM_C_CORDER_CODE));
            DEM_C_ITEM_CODE        = getDBV<string> (row[34], nameof(DEM_C_ITEM_CODE));
            DEM_C_ITEM_PLANT       = getDBV<string> (row[35], nameof(DEM_C_ITEM_PLANT));
            DEM_C_OPNUM            = getDBV<int>    (row[36], nameof(DEM_C_OPNUM));
            DEM_C_NSEQ             = getDBV<int>    (row[37], nameof(DEM_C_NSEQ));
            DEM_task_number        = getDBV<int>    (row[38], nameof(DEM_task_number));
            DEM_C_QTY              = getDBV<decimal>(row[39], nameof(DEM_C_QTY));
            DEM_C_WDW_QTY          = getDBV<decimal>(row[40], nameof(DEM_C_WDW_QTY));
            DEM_C_MRP_TYPE         = getDBV<string> (row[41], nameof(DEM_C_MRP_TYPE));
            DEM_C_STATUS           = getDBV<int>    (row[42], nameof(DEM_C_STATUS));
            DEM_C_REF_CORDER_CODE  = getDBV<string> (row[43], nameof(DEM_C_REF_CORDER_CODE));
            DEM_C_DUEDATE          = getSqlDate     (row[44], nameof(DEM_C_DUEDATE));
        }

        protected override string GetSelectQuery(int TaskNumber)
        {
            string ret = @"select 
       O.[C_CODE]
      ,O.[TASK_NUMBER]
      ,O.[C_CORDER_CODE]
      ,O.[C_ITEM_CODE]
      ,O.[C_ITEM_PLANT]
      ,O.[C_M_B]
      ,O.[C_MRP_TYPE]
      ,O.[C_QTY]
      ,O.[C_COMPL_QTY]
      ,O.[C_INSERT_DATE]
      ,O.[C_SHOP_FLOOR_CODE]
      ,O.[C_STATUS]
      ,O.[C_SUPPLIER_CODE]
      ,O.[C_WAREHOUSE_CODE]
      ,O.[C_STDATE]
      ,O.[C_DUEDATE]
      
      ,P.[C_ORDER_CODE]
      ,P.[C_OPNUM]
      ,P.[TASK_NUMBER]
      ,P.[C_DESCR]
      ,P.[C_QTY]
      ,P.[C_STDATE]
      ,P.[C_DUEDATE]
      ,P.[C_QUEUE_TIME]
      ,P.[C_WAIT_TIME]
      ,P.[C_SETUP_TIME]
      ,P.[C_RUN_TIME]
      ,P.[C_WORKCENTER_CODE]
      ,P.[C_SUPPLIER_CODE]
      ,P.[C_ATTREZZATURA]
      ,P.[C_MACHINE]
      ,P.[C_SEQUENCE]

      ,D.[C_CORDER_CODE]
      ,D.[C_ORDER_CODE]
      ,D.[C_ITEM_CODE]
      ,D.[C_ITEM_PLANT]
      ,D.[C_OPNUM]
      ,D.[C_NSEQ]
      ,D.[TASK_NUMBER]
      ,D.[C_QTY]
      ,D.[C_WDW_QTY]
      ,D.[C_MRP_TYPE]
      ,D.[C_STATUS]
      ,D.[C_REF_CORDER_CODE]
      ,D.[C_DUEDATE]

        from dbo.EXP_ORDER O
        join  dbo.EXP_OPERATION P on O.TASK_NUMBER=P.TASK_NUMBER and O.C_CODE = P.C_ORDER_CODE
        join dbo.EXP_DEMAND D on O.TASK_NUMBER=D.TASK_NUMBER and O.C_CODE = D.C_ORDER_CODE
        where O.TASK_NUMBER="+ TaskNumber + " " + WhereCondition() +
            " order by O.TASK_NUMBER asc ";

            return ret;
        }

        public override void DeleteTaskNumber(int taskNumberToDelete)
        {
            string qry = @" DELETE FROM [CyberPlanFrontiera].[dbo].[EXP_ORDER]     WHERE [TASK_NUMBER] = " + taskNumberToDelete + @" 
                            DELETE FROM [CyberPlanFrontiera].[dbo].[EXP_OPERATION] WHERE [TASK_NUMBER] = " + taskNumberToDelete + @" 
                            DELETE FROM [CyberPlanFrontiera].[dbo].[EXP_DEMAND]    WHERE [TASK_NUMBER] = " + taskNumberToDelete;
            DBHelper2.EseguiSuDBCyberPlan(ref _db, qry);
            _logger.Info("task number deleted =" + taskNumberToDelete);

        }
    }
}
