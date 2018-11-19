using System.Collections.Generic;
using System;
using log4net;
using System.Data;


namespace SyncCyberPlan_lib
{
    public abstract class Giacenze : Item
    {

        #region tabella output CYB_MATERIAL_LOT
        public string C_CODE;                                  //varchar  30
        public string C_ITEM_CODE;                             //varchar  50
        public string C_ITEM_PLANT;                            //varchar  20
        public string C_WAREHOUSE_CODE;                        //varchar  20
        public string C_DESCR;                                 //varchar  30
        public string C_CORDER_CODE;                           //varchar  30
        public decimal C_QTY;                                  //numeric  
        public decimal C_WDW_QTY;                              //numeric  
        public DateTime? C_EFFECTIVE_DATE;                     //datetime 
        public DateTime? C_EXPIRE_DATE;                        //datetime 
        public int C_USER_INT01;                               //int
        public int C_USER_INT02;                               //int
        public float C_USER_REAL01;                            //float
        public float C_USER_REAL02;                            //float
        public float C_USER_REAL03;                            //float
        public string C_USER_STRING01;                         //varchar  29
        public string C_USER_STRING02;                         //varchar  29
        public DateTime? C_USER_DATE01;                        //datetime
        public DateTime? C_USER_DATE02;                        //datetime
        public DateTime? C_USER_DATE03;                        //datetime
        #endregion


        public Giacenze(): base("CYB_MATERIAL_LOT")
        {
        }

        public override DataRow GetCyberRow()
        {
            DataRow _tablerow = _dataTable.NewRow();

            _tablerow[0] = C_CODE            ;                        //C_CODE           varchar  30
            _tablerow[1] = C_ITEM_CODE       ;                        //C_ITEM_CODE      varchar  50
            _tablerow[2] = C_ITEM_PLANT      ;                        //C_ITEM_PLANT     varchar  20
            _tablerow[3] = C_WAREHOUSE_CODE  ;                        //C_WAREHOUSE_CODE varchar  20
            _tablerow[4] = C_DESCR           ;                        //C_DESCR          varchar  30
            _tablerow[5] = C_CORDER_CODE     ;                        //C_CORDER_CODE    varchar  30
            _tablerow[6] = C_QTY             ;                        //C_QTY            numeric  
            _tablerow[7] = C_WDW_QTY         ;                        //C_WDW_QTY        numeric  
            _tablerow[8] = DateTime_toCyb(C_EFFECTIVE_DATE ) ;        //C_EFFECTIVE_DATE datetime 
            _tablerow[9] = DateTime_toCyb(C_EXPIRE_DATE);             //C_EXPIRE_DATE    datetime 
            _tablerow[10] = C_USER_INT01      ;                       //C_USER_INT01     int
            _tablerow[11] = C_USER_INT02      ;                       //C_USER_INT02     int
            _tablerow[12] = C_USER_REAL01     ;                       //C_USER_REAL01    float
            _tablerow[13] = C_USER_REAL02     ;                       //C_USER_REAL02    float
            _tablerow[14] = C_USER_REAL03     ;                       //C_USER_REAL03    float
            _tablerow[15] = C_USER_STRING01   ;                       //C_USER_STRING01  varchar  29
            _tablerow[16] = C_USER_STRING02   ;                       //C_USER_STRING02  varchar  29
            _tablerow[17] = DateTime_toCyb(C_USER_DATE01);            //C_USER_DATE01    datetime
            _tablerow[18] = DateTime_toCyb(C_USER_DATE02);            //C_USER_DATE02    datetime
            _tablerow[19] = DateTime_toCyb(C_USER_DATE03);            //C_USER_DATE03    datetime

            return _tablerow;
        }
        public override string GetID()
        {
            return C_CODE + "  " + C_ITEM_CODE;
        }

        public override void InitDataTable()
        {
            _dataTable.Columns.Add("C_CODE",                 typeof(string));                    //varchar  30
            _dataTable.Columns.Add("C_ITEM_CODE",            typeof(string));                    //varchar  50
            _dataTable.Columns.Add("C_ITEM_PLANT",           typeof(string));                    //varchar  20
            _dataTable.Columns.Add("C_WAREHOUSE_CODE",       typeof(string));                    //varchar  20
            _dataTable.Columns.Add("C_DESCR",                typeof(string));                    //varchar  30
            _dataTable.Columns.Add("C_CORDER_CODE",          typeof(string));                    //varchar  30
            _dataTable.Columns.Add("C_QTY",                  typeof(decimal));                   //numeric  
            _dataTable.Columns.Add("C_WDW_QTY",              typeof(decimal));                   //numeric  
            _dataTable.Columns.Add("C_EFFECTIVE_DATE",       typeof(DateTime));                  //datetime 
            _dataTable.Columns.Add("C_EXPIRE_DATE",          typeof(DateTime));                  //datetime 
            _dataTable.Columns.Add("C_USER_INT01",           typeof(int));                       //int
            _dataTable.Columns.Add("C_USER_INT02",           typeof(int));                       //int
            _dataTable.Columns.Add("C_USER_REAL01",          typeof(float));                     //float
            _dataTable.Columns.Add("C_USER_REAL02",          typeof(float));                     //float
            _dataTable.Columns.Add("C_USER_REAL03",          typeof(float));                     //float
            _dataTable.Columns.Add("C_USER_STRING01",        typeof(string));                    //varchar  29
            _dataTable.Columns.Add("C_USER_STRING02",        typeof(string));                    //varchar  29
            _dataTable.Columns.Add("C_USER_DATE01",          typeof(DateTime));                  //datetime
            _dataTable.Columns.Add("C_USER_DATE02",          typeof(DateTime));                  //datetime
            _dataTable.Columns.Add("C_USER_DATE03",          typeof(DateTime));                  //datetime
        }
    }
}
