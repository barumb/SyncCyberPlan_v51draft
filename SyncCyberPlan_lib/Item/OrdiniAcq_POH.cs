using System.Collections.Generic;
using System;
using log4net;
using System.Data;

namespace SyncCyberPlan_lib
{
    public class OrdiniAcq_POH : OrdiniAcq
    {
        public string POHNUM_0;
        public int POPLIN_0;
        public int POHTYP_0;
        public string ITMREF_0;
        public string POHFCY_0;
        public decimal QTYSTU_0;
        public decimal RCPQTYSTU_0;
        public DateTime? CREDAT_0;
        public DateTime? ORDDAT_0;
        public DateTime? EXTRCPDAT_0;
        public int LINCLEFLG_0;
        public float LINAMT_0;
        public string BPSNUM_0;
        public string YPOHTYP_0;
        
        public OrdiniAcq_POH(string YPOHTYP): base("CYB_ORDER")
        {
            __YPOHTYP_filter = YPOHTYP;
        }

        public override void Init(object[] row)
        {
            POHNUM_0 = getDBV<string>(row[0], "POHNUM_0");
            POPLIN_0 = getDBV<int>(row[1], "POPLIN_0");
            POHTYP_0 = getDBV<byte>(row[2], "POHTYP_0");
            ITMREF_0 = getDBV<string>(row[3], "ITMREF_0");
            POHFCY_0 = getDBV<string>(row[4], "POHFCY_0");
            QTYSTU_0 = getDBV<decimal>(row[5], "QTYSTU_0");
            RCPQTYSTU_0 = getDBV<decimal>(row[6], "RCPQTYSTU_0");
            CREDAT_0 = getSageDate(row[7], "CREDAT_0");
            ORDDAT_0 = getSageDate(row[8], "ORDDAT_0");
            EXTRCPDAT_0 = getSageDate(row[9], "EXTRCPDAT_0");
            LINCLEFLG_0 = getDBV<byte>(row[10], "LINCLEFLG_0");
            LINAMT_0 = (float)getDBV<decimal>(row[11], "LINAMT_0");
            BPSNUM_0 = getDBV<string>(row[12], "BPSNUM_0");
            YPOHTYP_0 = getDBV<string>(row[13], "YPOHTYP_0");



            C_CODE                   =EscapeSQL(POHNUM_0 + POPLIN_0.ToString("000000"), 30); //varchar         30                      
            C_CORDER_CODE            =EscapeSQL("", 30); //varchar         30                      
            C_ITEM_CODE              =EscapeSQL(ITMREF_0, 50); //varchar         50                      
            C_ITEM_PLANT             =EscapeSQL(POHFCY_0, 20); //varchar         20                      
            C_M_B                    ='B'; //char             1                       
            C_MRP_TYPE               ='F'; //char             1                       
            C_QTY                    =QTYSTU_0; //numeric            
            C_COMPL_QTY              =RCPQTYSTU_0; //numeric            
            C_SCRAP_QTY              =0; //numeric            
            C_HOST_QTY               =0; //numeric            
            C_INSERT_DATE            =CREDAT_0; //datetime           
            C_HOST_STDATE            =ORDDAT_0; //datetime           
            C_HOST_DUEDATE           =EXTRCPDAT_0; //datetime           
            C_PROMISE_DATE           =null; //datetime           
            C_ACT_STDATE             =null; //datetime           
            C_ACT_DUEDATE            =null; //datetime           
            C_SHOP_FLOOR_CODE        =EscapeSQL("", 20); //varchar         20                      
            C_STATUS                 =6;            //int  6 se ordine di acquisto(già emesso), 3 se richiesta di acquisto

            C_HOST_STATUS            =EscapeSQL("", 15); //varchar         15                      
            C_HOST_CODE              =EscapeSQL("", 30); //varchar         30                      
            C_ROUTING_CODE           =EscapeSQL("", 51); //varchar         51                      
            C_ROUTING_ALT            =EscapeSQL("", 9); //varchar         9                       
            C_BOM_CODE               =EscapeSQL("", 30); //varchar         30                      
            C_BOM_ALT                =EscapeSQL("", 20); //varchar         20                      
            C_ALT_PROD               =EscapeSQL("", 20); //varchar         20                      
            C_HOST_ALT_PROD          =EscapeSQL("", 20); //varchar         20                      
            C_HOST_ALT_ROUTING       =EscapeSQL("", 8); //varchar         8                       
            C_HOST_LAST_UPDATE       =null; //datetime           
            C_COST                   =0; //float              
            C_VALUE                  =LINAMT_0; //float              
            C_ORD_GROUP              =EscapeSQL("", 10); //varchar         10                      
            C_MANAGER                =EscapeSQL("", 20); //varchar         20                      
            C_SUPPLIER_CODE          =EscapeSQL(BPSNUM_0, 30); //varchar         30                      
            C_WAREHOUSE_CODE         =EscapeSQL(__MAGAZZINO_INTERNO, 20); //varchar         20                      
            C_USER_INT01             =0; //int               
            C_USER_INT02             =0; //int               
            C_USER_INT03             =0; //int               
            C_USER_INT04             =0; //int               
            C_USER_INT05             =0; //int               
            C_USER_INT06             =0; //int               
            C_USER_INT07             =0; //int               
            C_USER_INT08             =0; //int               
            C_USER_INT09             =0; //int               
            C_USER_INT10             =0; //int               
            C_USER_REAL01            =0; //float             
            C_USER_REAL02            =0; //float             
            C_USER_REAL03            =0; //float             
            C_USER_REAL04            =0; //float             
            C_USER_REAL05            =0; //float             
            C_USER_CHAR01            =' '; //char            1                       
            C_USER_CHAR02            =' '; //char            1                       
            C_USER_CHAR03            =' '; //char            1                       
            C_USER_CHAR04            =' '; //char            1                       
            C_USER_CHAR05            =' '; //char            1                       
            C_USER_FLAG01            =0; //bit               
            C_USER_FLAG02            =0; //bit               
            C_USER_STRING01          =EscapeSQL("", 29); //varchar         29                      
            C_USER_STRING02          =EscapeSQL("", 29); //varchar         29                      
            C_USER_STRING03          =EscapeSQL("", 29); //varchar         29                      
            C_USER_STRING04          =EscapeSQL("", 29); //varchar         29                      
            C_USER_STRING05          =EscapeSQL("", 29); //varchar         29                      
            C_USER_STRING06          =EscapeSQL("", 29); //varchar         29                      
            C_USER_STRING07          =EscapeSQL("", 29); //varchar         29                      
            C_USER_STRING08          =EscapeSQL("", 29); //varchar         29                      
            C_USER_STRING09          =EscapeSQL("", 29); //varchar         29                      
            C_USER_STRING10          =EscapeSQL("", 29); //varchar         29                      
            C_USER_NOTE01            =EscapeSQL("", 99); //varchar         99                      
            C_USER_COLOR01           =0; //int                                
            C_USER_COLOR02           =0; //int                                
            C_USER_DATE01            =null; //datetime                           
            C_USER_DATE02            =null; //datetime                           
            C_USER_DATE03            =null; //datetime                           
            C_USER_DATE04            =null; //datetime                           
            C_USER_DATE05            =null; //datetime                   

        }        
        public override string GetSelectQuery(bool mode, string dossier, string codice_like, string filtro)
        {
            string db = "x3." + dossier;
            //string sage_query = "SELECT A.ITMREF_0, ITMSTA_0 from " + dossier + ".[ITMMASTER] A join " + dossier + ".[YITMINF] B on A.ITMREF_0=B.ITMREF_0" +
            //    " WHERE YLIVTRAS_0='" + tipo + "' and ITMSTA=1 ";

            string sage_query =
                @"SELECT 
                   Q.POHNUM_0
                 , Q.POPLIN_0
                 , Q.POHTYP_0
                 , Q.ITMREF_0
                 , Q.POHFCY_0
                 , Q.QTYSTU_0
                 , Q.RCPQTYSTU_0
                 , Q.CREDAT_0
                 , Q.ORDDAT_0
                 , Q.EXTRCPDAT_0
                 , Q.LINCLEFLG_0
                 , Q.LINAMT_0
                 , Q.BPSNUM_0
                 , P.YPOHTYP_0
                 from " + db + ".PORDERQ Q " +
                " join " + db + ".PORDER P on P.POHNUM_0 = Q.POHNUM_0 " +
                " WHERE P.YPOHTYP_0='" + __YPOHTYP_filter + "' "  +
                " and Q.ITMREF_0 not like 'WWACQ%' " +
                " and Q.LINCLEFLG_0=1 "
                ;

            if (!string.IsNullOrWhiteSpace(codice_like))
            {
                sage_query += " and Q.POHNUM_0 like '" + codice_like.Trim() + "'";
            }

            sage_query += " ORDER BY Q.POHNUM_0, Q.POPLIN_0 ";
            return sage_query;
        }
    }
}
