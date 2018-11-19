using System.Collections.Generic;
using System;
using log4net;
using System.Data;

namespace SyncCyberPlan_lib
{
    public abstract class OrdiniAcq : Item
    {
        public string __YPOHTYP_filter;
        
        #region tabella output CYB_ORDER
        public string C_CODE; //varchar	30
        public string C_CORDER_CODE; //varchar	30
        public string C_ITEM_CODE; //varchar	50
        public string C_ITEM_PLANT; //varchar	20
        public char C_M_B; //char	1
        public char C_MRP_TYPE; //char	1
        public decimal C_QTY; //numeric	
        public decimal C_COMPL_QTY; //numeric	
        public decimal C_SCRAP_QTY; //numeric	
        public decimal C_HOST_QTY; //numeric	
        public DateTime? C_INSERT_DATE; //datetime	
        public DateTime? C_HOST_STDATE; //datetime	
        public DateTime? C_HOST_DUEDATE; //datetime	
        public DateTime? C_PROMISE_DATE; //datetime	
        public DateTime? C_ACT_STDATE; //datetime	
        public DateTime? C_ACT_DUEDATE; //datetime	
        public string C_SHOP_FLOOR_CODE; //varchar	20
        public int C_STATUS; //int	
        public string C_HOST_STATUS; //varchar	15
        public string C_HOST_CODE; //varchar	30
        public string C_ROUTING_CODE; //varchar	51
        public string C_ROUTING_ALT; //varchar	9
        public string C_BOM_CODE; //varchar	30
        public string C_BOM_ALT; //varchar	20
        public string C_ALT_PROD; //varchar	20
        public string C_HOST_ALT_PROD; //varchar	20
        public string C_HOST_ALT_ROUTING; //varchar	8
        public DateTime? C_HOST_LAST_UPDATE; //datetime	
        public float C_COST; //float	
        public float C_VALUE; //float	
        public string C_ORD_GROUP; //varchar	10
        public string C_MANAGER; //varchar	20
        public string C_SUPPLIER_CODE; //varchar	30
        public string C_WAREHOUSE_CODE; //varchar	20
        public int C_USER_INT01; //int	
        public int C_USER_INT02; //int	
        public int C_USER_INT03; //int	
        public int C_USER_INT04; //int	
        public int C_USER_INT05; //int	
        public int C_USER_INT06; //int	
        public int C_USER_INT07; //int	
        public int C_USER_INT08; //int	
        public int C_USER_INT09; //int	
        public int C_USER_INT10; //int	
        public float C_USER_REAL01; //float	
        public float C_USER_REAL02; //float	
        public float C_USER_REAL03; //float	
        public float C_USER_REAL04; //float	
        public float C_USER_REAL05; //float	
        public char C_USER_CHAR01; //char	1
        public char C_USER_CHAR02; //char	1
        public char C_USER_CHAR03; //char	1
        public char C_USER_CHAR04; //char	1
        public char C_USER_CHAR05; //char	1
        public int C_USER_FLAG01; //bit	
        public int C_USER_FLAG02; //bit	
        public string C_USER_STRING01; //varchar	29
        public string C_USER_STRING02; //varchar	29
        public string C_USER_STRING03; //varchar	29
        public string C_USER_STRING04; //varchar	29
        public string C_USER_STRING05; //varchar	29
        public string C_USER_STRING06; //varchar	29
        public string C_USER_STRING07; //varchar	29
        public string C_USER_STRING08; //varchar	29
        public string C_USER_STRING09; //varchar	29
        public string C_USER_STRING10; //varchar	29
        public string C_USER_NOTE01; //varchar	99
        public int C_USER_COLOR01; //int	
        public int C_USER_COLOR02; //int	
        public DateTime? C_USER_DATE01; //datetime	
        public DateTime? C_USER_DATE02; //datetime	
        public DateTime? C_USER_DATE03; //datetime	
        public DateTime? C_USER_DATE04; //datetime	
        public DateTime? C_USER_DATE05; //datetime	

        #endregion


        public OrdiniAcq(string YPOHTYP): base("CYB_ORDER")
        {
            __YPOHTYP_filter = YPOHTYP;
        }
        public override DataRow GetCyberRow()
        {
            //if (C_COMPL_QTY >= C_QTY)
            //{
            //    //se la quantità completata è maggiore della qta richiesta la riga non va esportata in CyberPlan
            //    return null;
            //}

            DataRow _tablerow = _dataTable.NewRow();
            
            _tablerow[0] = C_CODE;                                         //C_CODE                       varchar         30                      
            _tablerow[1] = C_CORDER_CODE;                                  //C_CORDER_CODE                varchar         30                      
            _tablerow[2] = C_ITEM_CODE;                                    //C_ITEM_CODE                  varchar         50                      
            _tablerow[3] = C_ITEM_PLANT;                                   //C_ITEM_PLANT                 varchar         20                      
            _tablerow[4] = C_M_B;                                          //C_M_B                        char             1                       
            _tablerow[5] = C_MRP_TYPE;                                     //C_MRP_TYPE                   char             1                       
            _tablerow[6] = C_QTY;                                          //C_QTY                        numeric            
            _tablerow[7] = C_COMPL_QTY;                                    //C_COMPL_QTY                  numeric            
            _tablerow[8] = 0;                                              //C_SCRAP_QTY                  numeric            
            _tablerow[9] = 0;                                              //C_HOST_QTY                   numeric            
            _tablerow[10] = C_INSERT_DATE ;                                //C_INSERT_DATE                datetime           
            _tablerow[11] = C_HOST_STDATE ;                                //C_HOST_STDATE                datetime           
            _tablerow[12] = C_HOST_DUEDATE;                                //C_HOST_DUEDATE               datetime           
            _tablerow[13] = DateTime_toCyb(C_PROMISE_DATE);                //C_PROMISE_DATE               datetime           
            _tablerow[14] = DateTime_toCyb(C_ACT_STDATE );                 //C_ACT_STDATE                 datetime           
            _tablerow[15] = DateTime_toCyb(C_ACT_DUEDATE );                //C_ACT_DUEDATE                datetime           
            _tablerow[16] = C_SHOP_FLOOR_CODE;                             //C_SHOP_FLOOR_CODE            varchar         20                      
            _tablerow[17] = C_STATUS;                                      //C_STATUS                     int  in Cyber 1 aperta 2 chiusa (contrario di sage)
            _tablerow[18] = C_HOST_STATUS;                                 //C_HOST_STATUS                varchar         15                      
            _tablerow[19] = C_HOST_CODE       ;                            //C_HOST_CODE                  varchar         30                      
            _tablerow[20] = C_ROUTING_CODE    ;                            //C_ROUTING_CODE               varchar         51                      
            _tablerow[21] = C_ROUTING_ALT     ;                            //C_ROUTING_ALT                varchar         9                       
            _tablerow[22] = C_BOM_CODE        ;                            //C_BOM_CODE                   varchar         30                      
            _tablerow[23] = C_BOM_ALT         ;                            //C_BOM_ALT                    varchar         20                      
            _tablerow[24] = C_ALT_PROD        ;                            //C_ALT_PROD                   varchar         20                      
            _tablerow[25] = C_HOST_ALT_PROD   ;                            //C_HOST_ALT_PROD              varchar         20                      
            _tablerow[26] = C_HOST_ALT_ROUTING;                            //C_HOST_ALT_ROUTING           varchar         8                       
            _tablerow[27] = DateTime_toCyb(C_HOST_LAST_UPDATE);            //C_HOST_LAST_UPDATE           datetime           
            _tablerow[28] = C_COST;                                        //C_COST                       float              
            _tablerow[29] = C_VALUE           ;                            //C_VALUE                      float              
            _tablerow[30] = C_ORD_GROUP       ;                            //C_ORD_GROUP                  varchar         10                      
            _tablerow[31] = C_MANAGER         ;                            //C_MANAGER                    varchar         20                      
            _tablerow[32] = C_SUPPLIER_CODE   ;                            //C_SUPPLIER_CODE              varchar         30                      
            _tablerow[33] = C_WAREHOUSE_CODE  ;                            //C_WAREHOUSE_CODE             varchar         20                      
            _tablerow[34] = C_USER_INT01;                                  //C_USER_INT01                 int               
            _tablerow[35] = C_USER_INT02;                                  //C_USER_INT02                 int               
            _tablerow[36] = C_USER_INT03;                                  //C_USER_INT03                 int               
            _tablerow[37] = C_USER_INT04;                                  //C_USER_INT04                 int               
            _tablerow[38] = C_USER_INT05;                                  //C_USER_INT05                 int               
            _tablerow[39] = C_USER_INT06;                                  //C_USER_INT06                 int               
            _tablerow[40] = C_USER_INT07;                                  //C_USER_INT07                 int               
            _tablerow[41] = C_USER_INT08;                                  //C_USER_INT08                 int               
            _tablerow[42] = C_USER_INT09;                                  //C_USER_INT09                 int               
            _tablerow[43] = C_USER_INT10;                                  //C_USER_INT10                 int               
            _tablerow[44] = C_USER_REAL01;                                 //C_USER_REAL01                float             
            _tablerow[45] = C_USER_REAL02;                                 //C_USER_REAL02                float             
            _tablerow[46] = C_USER_REAL03;                                 //C_USER_REAL03                float             
            _tablerow[47] = C_USER_REAL04;                                 //C_USER_REAL04                float             
            _tablerow[48] = C_USER_REAL05;                                 //C_USER_REAL05                float             
            _tablerow[49] = C_USER_CHAR01;                                 //C_USER_CHAR01                char            1                       
            _tablerow[50] = C_USER_CHAR02;                                 //C_USER_CHAR02                char            1                       
            _tablerow[51] = C_USER_CHAR03;                                 //C_USER_CHAR03                char            1                       
            _tablerow[52] = C_USER_CHAR04;                                 //C_USER_CHAR04                char            1                       
            _tablerow[53] = C_USER_CHAR05;                                 //C_USER_CHAR05                char            1                       
            _tablerow[54] = C_USER_FLAG01;                                 //C_USER_FLAG01                bit               
            _tablerow[55] = C_USER_FLAG02;                                 //C_USER_FLAG02                bit               
            _tablerow[56] = C_USER_STRING01;                               //C_USER_STRING01              varchar         29                      
            _tablerow[57] = C_USER_STRING02;                               //C_USER_STRING02              varchar         29                      
            _tablerow[58] = C_USER_STRING03;                               //C_USER_STRING03              varchar         29                      
            _tablerow[59] = C_USER_STRING04;                               //C_USER_STRING04              varchar         29                      
            _tablerow[60] = C_USER_STRING05;                               //C_USER_STRING05              varchar         29                      
            _tablerow[61] = C_USER_STRING06;                               //C_USER_STRING06              varchar         29                      
            _tablerow[62] = C_USER_STRING07;                               //C_USER_STRING07              varchar         29                      
            _tablerow[63] = C_USER_STRING08;                               //C_USER_STRING08              varchar         29                      
            _tablerow[64] = C_USER_STRING09;                               //C_USER_STRING09              varchar         29                      
            _tablerow[65] = C_USER_STRING10;                               //C_USER_STRING10              varchar         29                      
            _tablerow[66] = C_USER_NOTE01   ;                              //C_USER_NOTE01                varchar         99                      
            _tablerow[67] = C_USER_COLOR01  ;                              //C_USER_COLOR01               int                                
            _tablerow[68] = C_USER_COLOR02;                                //C_USER_COLOR02               int                                
            _tablerow[69] = DateTime_toCyb( C_USER_DATE01);                //C_USER_DATE01                datetime                           
            _tablerow[70] = DateTime_toCyb(C_USER_DATE02 );                //C_USER_DATE02                datetime                           
            _tablerow[71] = DateTime_toCyb(C_USER_DATE03 );                //C_USER_DATE03                datetime                           
            _tablerow[72] = DateTime_toCyb(C_USER_DATE04 );                //C_USER_DATE04                datetime                           
            _tablerow[73] = DateTime_toCyb(C_USER_DATE05);                 //C_USER_DATE05                datetime            

            return _tablerow;
        }
        public override string GetID()
        {
            return C_CODE + "  " + C_CODE;
        }

        public override void InitDataTable()
        {
            _dataTable.Columns.Add("C_CODE",                    typeof(string));         //varchar	30
            _dataTable.Columns.Add("C_CORDER_CODE",             typeof(string));         //varchar	30
            _dataTable.Columns.Add("C_ITEM_CODE",               typeof(string));         //varchar	50
            _dataTable.Columns.Add("C_ITEM_PLANT",              typeof(string));         //varchar	20
            _dataTable.Columns.Add("C_M_B",                     typeof(string));         //char	1
            _dataTable.Columns.Add("C_MRP_TYPE",                typeof(string));         //char	1
            _dataTable.Columns.Add("C_QTY",                     typeof(string));         //numeric	
            _dataTable.Columns.Add("C_COMPL_QTY",               typeof(string));         //numeric	
            _dataTable.Columns.Add("C_SCRAP_QTY",               typeof(string));         //numeric	
            _dataTable.Columns.Add("C_HOST_QTY",                typeof(string));         //numeric	
            _dataTable.Columns.Add("C_INSERT_DATE",             typeof(string));         //datetime	
            _dataTable.Columns.Add("C_HOST_STDATE",             typeof(string));         //datetime	
            _dataTable.Columns.Add("C_HOST_DUEDATE",            typeof(string));         //datetime	
            _dataTable.Columns.Add("C_PROMISE_DATE",            typeof(string));         //datetime	
            _dataTable.Columns.Add("C_ACT_STDATE",              typeof(string));         //datetime	
            _dataTable.Columns.Add("C_ACT_DUEDATE",             typeof(string));         //datetime	
            _dataTable.Columns.Add("C_SHOP_FLOOR_CODE",         typeof(string));         //varchar	20
            _dataTable.Columns.Add("C_STATUS",                  typeof(string));         //int	
            _dataTable.Columns.Add("C_HOST_STATUS",             typeof(string));         //varchar	15
            _dataTable.Columns.Add("C_HOST_CODE",               typeof(string));         //varchar	30
            _dataTable.Columns.Add("C_ROUTING_CODE",            typeof(string));         //varchar	51
            _dataTable.Columns.Add("C_ROUTING_ALT",             typeof(string));         //varchar	9
            _dataTable.Columns.Add("C_BOM_CODE",                typeof(string));         //varchar	30
            _dataTable.Columns.Add("C_BOM_ALT",                 typeof(string));         //varchar	20
            _dataTable.Columns.Add("C_ALT_PROD",                typeof(string));         //varchar	20
            _dataTable.Columns.Add("C_HOST_ALT_PROD",           typeof(string));         //varchar	20
            _dataTable.Columns.Add("C_HOST_ALT_ROUTING",        typeof(string));         //varchar	8
            _dataTable.Columns.Add("C_HOST_LAST_UPDATE",        typeof(DateTime));       //datetime	
            _dataTable.Columns.Add("C_COST",                    typeof(float));          //float	
            _dataTable.Columns.Add("C_VALUE",                   typeof(float));          //float	
            _dataTable.Columns.Add("C_ORD_GROUP",               typeof(string));         //varchar	10
            _dataTable.Columns.Add("C_MANAGER",                 typeof(string));         //varchar	20
            _dataTable.Columns.Add("C_SUPPLIER_CODE",           typeof(string));         //varchar	30
            _dataTable.Columns.Add("C_WAREHOUSE_CODE",          typeof(string));         //varchar	20
            _dataTable.Columns.Add("C_USER_INT01",              typeof(int));            //int	
            _dataTable.Columns.Add("C_USER_INT02",              typeof(int));            //int	
            _dataTable.Columns.Add("C_USER_INT03",              typeof(int));            //int	
            _dataTable.Columns.Add("C_USER_INT04",              typeof(int));            //int	
            _dataTable.Columns.Add("C_USER_INT05",              typeof(int));            //int	
            _dataTable.Columns.Add("C_USER_INT06",              typeof(int));            //int	
            _dataTable.Columns.Add("C_USER_INT07",              typeof(int));            //int	
            _dataTable.Columns.Add("C_USER_INT08",              typeof(int));            //int	
            _dataTable.Columns.Add("C_USER_INT09",              typeof(int));            //int	
            _dataTable.Columns.Add("C_USER_INT10",              typeof(int));            //int	
            _dataTable.Columns.Add("C_USER_REAL01",             typeof(float));          //float	
            _dataTable.Columns.Add("C_USER_REAL02",             typeof(float));          //float	
            _dataTable.Columns.Add("C_USER_REAL03",             typeof(float));          //float	
            _dataTable.Columns.Add("C_USER_REAL04",             typeof(float));          //float	
            _dataTable.Columns.Add("C_USER_REAL05",             typeof(float));          //float	
            _dataTable.Columns.Add("C_USER_CHAR01",             typeof(char));           //char	1
            _dataTable.Columns.Add("C_USER_CHAR02",             typeof(char));           //char	1
            _dataTable.Columns.Add("C_USER_CHAR03",             typeof(char));           //char	1
            _dataTable.Columns.Add("C_USER_CHAR04",             typeof(char));           //char	1
            _dataTable.Columns.Add("C_USER_CHAR05",             typeof(char));           //char	1
            _dataTable.Columns.Add("C_USER_FLAG01",             typeof(int));            //bit	
            _dataTable.Columns.Add("C_USER_FLAG02",             typeof(int));            //bit	
            _dataTable.Columns.Add("C_USER_STRING01",           typeof(string));         //varchar	29
            _dataTable.Columns.Add("C_USER_STRING02",           typeof(string));         //varchar	29
            _dataTable.Columns.Add("C_USER_STRING03",           typeof(string));         //varchar	29
            _dataTable.Columns.Add("C_USER_STRING04",           typeof(string));         //varchar	29
            _dataTable.Columns.Add("C_USER_STRING05",           typeof(string));         //varchar	29
            _dataTable.Columns.Add("C_USER_STRING06",           typeof(string));         //varchar	29
            _dataTable.Columns.Add("C_USER_STRING07",           typeof(string));         //varchar	29
            _dataTable.Columns.Add("C_USER_STRING08",           typeof(string));         //varchar	29
            _dataTable.Columns.Add("C_USER_STRING09",           typeof(string));         //varchar	29
            _dataTable.Columns.Add("C_USER_STRING10",           typeof(string));         //varchar	29
            _dataTable.Columns.Add("C_USER_NOTE01",             typeof(string));         //varchar	99
            _dataTable.Columns.Add("C_USER_COLOR01",            typeof(int));            //int	
            _dataTable.Columns.Add("C_USER_COLOR02",            typeof(int));            //int	
            _dataTable.Columns.Add("C_USER_DATE01",             typeof(DateTime));       //datetime	
            _dataTable.Columns.Add("C_USER_DATE02",             typeof(DateTime));       //datetime	
            _dataTable.Columns.Add("C_USER_DATE03",             typeof(DateTime));       //datetime	
            _dataTable.Columns.Add("C_USER_DATE04",             typeof(DateTime));       //datetime	
            _dataTable.Columns.Add("C_USER_DATE05",             typeof(DateTime));       //datetime	
        }
    }
}
