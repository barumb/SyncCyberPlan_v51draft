using System.Collections.Generic;
using System;
using log4net;
using System.Data;
using System.Data.Common;

namespace SyncCyberPlan_lib
{
    public class OrdiniVen : Item
    {
        public string SOHNUM_0;
        public int SOPLIN_0;
        public string BPCORD_0;
        public string ITMREF_0;
        public string SALFCY_0;
        public decimal QTY_0;
        public decimal DLVQTY_0;
        public DateTime ORDDAT_0;
        public DateTime YMRPDATAC_0;
        public DateTime DEMDLVDAT_0;        
        public byte SOQSTA_0;
        public byte YMRPFLGC_0; //flag data confermata
        public string CREUSR_0;        
        public byte ORDSTA_0;


        //      C_INSERT_DATE         Data creazione ordine(inutile per MRP)
        //      C_DUEDATE             Data obiettivo dell’MRP  = COALESCE(C_PROMISE_DATE,C_REQUESTED_DATE)
        //      C_REQUESTED_DUEDATE   Data di consegna richiesta dal cliente della riga d’ordine
        //      C_PROMISE_DUEDATE     Data di consegna confermata al cliente della riga d’ordine
        //      C_ORDERED_DATE        Data riga ordine(inutile per MRP)
        //
        #region tabella output CYB_CORDER
        public string C_CODE; //varchar 30    
        public string C_DESCR; //varchar 30
        public string C_ITEM_CODE; //varchar 50
        public string C_ITEM_PLANT; //varchar 20
        public decimal C_QTY; //numeric 
        public decimal C_COMPL_QTY; //numeric 
        public DateTime? C_INSERT_DATE; //datetime 
        public DateTime? C_DUEDATE; //datetime 
        public DateTime? C_REQUESTED_DUEDATE; //datetime 
        public DateTime? C_PROMISE_DUEDATE; //datetime                   
        public DateTime? C_ORDERED_DATE; //datetime 
        public DateTime? C_HOST_DUEDATE; //datetime 
        public char C_TYPE; //char 1
        public string C_HOST_TYPE; //varchar 20
        public char C_STATUS; //char 1
        public string C_HOST_STATUS; //varchar 15
        public string C_CORDER_HEADER_CODE; //varchar 30
        public string C_REF_CORDER; //varchar 30
        public string C_CO_GROUP; //varchar 10
        public string C_WAREHOUSE_CODE; //varchar 20
        public float C_VALUE; //float 
        public float C_COST; //float 
        public string C_MANAGER; //varchar 20
        public string C_CUSTOMER_CODE; //varchar 30
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
        public char C_USER_CHAR01; //char 1
        public char C_USER_CHAR02; //char 1
        public char C_USER_CHAR03; //char 1
        public char C_USER_CHAR04; //char 1
        public char C_USER_CHAR05; //char 1
        public int C_USER_FLAG01; //bit 
        public int C_USER_FLAG02; //bit 
        public string C_USER_STRING01; //varchar 29
        public string C_USER_STRING02; //varchar 29
        public string C_USER_STRING03; //varchar 29
        public string C_USER_STRING04; //varchar 29
        public string C_USER_STRING05; //varchar 29
        public string C_USER_STRING06; //varchar 29
        public string C_USER_STRING07; //varchar 29
        public string C_USER_STRING08; //varchar 29
        public string C_USER_STRING09; //varchar 29
        public string C_USER_STRING10; //varchar 29
        public string C_USER_NOTE01; //varchar 99
        public int C_USER_COLOR01; //int 
        public int C_USER_COLOR02; //int 
        public DateTime? C_USER_DATE01; //datetime 
        public DateTime? C_USER_DATE02; //datetime 
        public DateTime? C_USER_DATE03; //datetime 
        public DateTime? C_USER_DATE04; //datetime 
        public DateTime? C_USER_DATE05; //
        #endregion


        public OrdiniVen(): base("CYB_CORDER")
        {
        }

        public override void Init(object[] row)
        {
            SOHNUM_0    = getDBV<string>(row[0], "SOHNUM_0");
            SOPLIN_0    = getDBV<int>(row[1], "SOPLIN_0");
            BPCORD_0    = getDBV<string>(row[2], "BPCORD_0");
            ITMREF_0    = getDBV<string>(row[3], "ITMREF_0");
            SALFCY_0    = getDBV<string>(row[4], "SALFCY_0");
            QTY_0       = getDBV<decimal>(row[5], "QTY_0");
            DLVQTY_0    = getDBV<decimal>(row[6], "DLVQTY_0");
            ORDDAT_0    = (DateTime)row[7];
            YMRPDATAC_0 = (DateTime)row[8];
            DEMDLVDAT_0 = (DateTime)row[9];
            SOQSTA_0    = (byte)row[10];
            YMRPFLGC_0  = (byte)row[11];            
            CREUSR_0    = getDBV<string>(row[12], "CREUSR_0");
            ORDSTA_0    = (byte)row[13];



            C_CODE                     = EscapeSQL(SOHNUM_0 + SOPLIN_0.ToString("000000"), 30);  //varchar  30
            C_DESCR                    = EscapeSQL("", 30);                                      //varchar  30
            C_ITEM_CODE                = EscapeSQL(ITMREF_0, 50);                                //varchar  50
            C_ITEM_PLANT               = EscapeSQL(SALFCY_0, 20);                                //varchar  20
            C_QTY                      = QTY_0;                                                  //numeric  
            C_COMPL_QTY                = DLVQTY_0;                                               //numeric  
            C_INSERT_DATE              = ORDDAT_0;                                               //datetime 
            C_DUEDATE                  = YMRPDATAC_0;                                            //datetime 
            C_REQUESTED_DUEDATE        = YMRPDATAC_0;//DEMDLVDAT_0;                              //datetime 
            C_PROMISE_DUEDATE          = (YMRPFLGC_0 == 2) ? (DateTime?)YMRPDATAC_0 : null ;     //datetime 
            //C_ORDERED_DATE             = YORDDAT_0;                                            //datetime 
            C_HOST_DUEDATE             = null;                                                   //datetime 
            C_TYPE                     = 'E';                                                    //char     1
            C_HOST_TYPE                = EscapeSQL("", 20);                                      //varchar  20
            C_STATUS                   = SOQSTA_0.ToString()[0];                                 //char     1
            C_HOST_STATUS              = EscapeSQL("", 15);                                      //varchar  15
            C_CORDER_HEADER_CODE       = EscapeSQL("", 30);                                      //varchar  30
            C_REF_CORDER               = EscapeSQL("", 30);                                      //varchar  30
            C_CO_GROUP                 = EscapeSQL("", 10);                                      //varchar  10
            C_WAREHOUSE_CODE           = EscapeSQL(__MAGAZZINO_INTERNO, 20);                     //varchar  20
            C_VALUE                    = 0;                                                      //float    
            C_COST                     = 0;                                                      //float    
            C_MANAGER                  = EscapeSQL(CREUSR_0, 20);                                //varchar  20
            C_CUSTOMER_CODE            = EscapeSQL(BPCORD_0, 30);                                //varchar  30
            C_USER_INT01               = 0;                                                      //int      
            C_USER_INT02               = 0;                                                      //int      
            C_USER_INT03               = 0;                                                      //int      
            C_USER_INT04               = 0;                                                      //int      
            C_USER_INT05               = 0;                                                      //int      
            C_USER_INT06               = 0;                                                      //int      
            C_USER_INT07               = 0;                                                      //int      
            C_USER_INT08               = 0;                                                      //int      
            C_USER_INT09               = 0;                                                      //int      
            C_USER_INT10               = 0;                                                      //int      
            C_USER_REAL01              = 0;                                                      //float     
            C_USER_REAL02              = 0;                                                      //float     
            C_USER_REAL03              = 0;                                                      //float     
            C_USER_REAL04              = 0;                                                      //float     
            C_USER_REAL05              = 0;                                                      //float     
            C_USER_CHAR01              = ' ';                                                    //char     1
            C_USER_CHAR02              = ' ';                                                    //char     1
            C_USER_CHAR03              = ' ';                                                    //char     1
            C_USER_CHAR04              = ' ';                                                    //char     1
            C_USER_CHAR05              = ' ';                                                    //char     1
            C_USER_FLAG01              = 0;                                                      //bit      
            C_USER_FLAG02              = 0;                                                      //bit      
            C_USER_STRING01            = EscapeSQL("", 29);                                      //varchar  29
            C_USER_STRING02            = EscapeSQL("", 29);                                      //varchar  29
            C_USER_STRING03            = EscapeSQL("", 29);                                      //varchar  29
            C_USER_STRING04            = EscapeSQL("", 29);                                      //varchar  29
            C_USER_STRING05            = EscapeSQL("", 29);                                      //varchar  29
            C_USER_STRING06            = EscapeSQL("", 29);                                      //varchar  29
            C_USER_STRING07            = EscapeSQL("", 29);                                      //varchar  29
            C_USER_STRING08            = EscapeSQL("", 29);                                      //varchar  29
            C_USER_STRING09            = EscapeSQL("", 29);                                      //varchar  29
            C_USER_STRING10            = EscapeSQL("", 29);                                      //varchar  29
            C_USER_NOTE01              = EscapeSQL("", 29);                                      //varchar  99
            C_USER_COLOR01             = 0;                                                      //int        
            C_USER_COLOR02             = 0;                                                      //int        
            C_USER_DATE01              = null;                                                   //datetime   
            C_USER_DATE02              = null;                                                   //datetime   
            C_USER_DATE03              = null;                                                   //datetime   
            C_USER_DATE04              = null;                                                   //datetime   
            C_USER_DATE05              = null;                                                   //datetime 

        }        
        public override DataRow GetCyberRow()
        {
            DataRow _tablerow = _dataTable.NewRow();

            _tablerow[0] = C_CODE;                                  //C_CODE                                    varchar  30
            _tablerow[1] = C_DESCR;                                 //C_DESCR                                   varchar  30
            _tablerow[2] = C_ITEM_CODE;                             //C_ITEM_CODE                               varchar  50
            _tablerow[3] = C_ITEM_PLANT;                            //C_ITEM_PLANT                              varchar  20
            _tablerow[4] = C_QTY;                                   //C_QTY                                     numeric  
            _tablerow[5] = C_COMPL_QTY;                             //C_COMPL_QTY                               numeric  
            _tablerow[6] =  DateTime_toCyb( C_INSERT_DATE      );   //C_INSERT_DATE                             datetime 
            _tablerow[7] =  DateTime_toCyb( C_DUEDATE          );   //C_DUEDATE                                 datetime 
            _tablerow[8] =  DateTime_toCyb( C_REQUESTED_DUEDATE);   //C_REQUESTED_DUEDATE                       datetime 
            _tablerow[9] =  DateTime_toCyb( C_PROMISE_DUEDATE  );   //C_PROMISE_DUEDATE                         datetime 
            _tablerow[10] = DateTime_toCyb(C_ORDERED_DATE     );    //C_ORDERED_DATE                            datetime 
            _tablerow[11] = DateTime_toCyb(C_HOST_DUEDATE);         //C_HOST_DUEDATE                            datetime 
            _tablerow[12] = C_TYPE;                                 //C_TYPE                                    char     1
            _tablerow[13] = C_HOST_TYPE            ;                //C_HOST_TYPE                               varchar  20
            _tablerow[14] = C_STATUS               ;                //C_STATUS                                  char     1
            _tablerow[15] = C_HOST_STATUS          ;                //C_HOST_STATUS                             varchar  15
            _tablerow[16] = C_CORDER_HEADER_CODE   ;                //C_CORDER_HEADER_CODE                      varchar  30
            _tablerow[17] = C_REF_CORDER;                           //C_REF_CORDER                              varchar  30
            _tablerow[18] = C_CO_GROUP;                             //C_CO_GROUP                                varchar  10
            _tablerow[19] = C_WAREHOUSE_CODE;                       //C_WAREHOUSE_CODE                          varchar  20
            _tablerow[20] = C_VALUE;                                //C_VALUE                                   float    
            _tablerow[21] = C_COST;                                 //C_COST                                    float    
            _tablerow[22] = C_MANAGER;                              //C_MANAGER                                 varchar  20
            _tablerow[23] = C_CUSTOMER_CODE;                        //C_CUSTOMER_CODE                           varchar  30
            _tablerow[24] = C_USER_INT01      ;                     //C_USER_INT01                              int      
            _tablerow[25] = C_USER_INT02      ;                     //C_USER_INT02                              int      
            _tablerow[26] = C_USER_INT03      ;                     //C_USER_INT03                              int      
            _tablerow[27] = C_USER_INT04      ;                     //C_USER_INT04                              int      
            _tablerow[28] = C_USER_INT05      ;                     //C_USER_INT05                              int      
            _tablerow[29] = C_USER_INT06      ;                     //C_USER_INT06                              int      
            _tablerow[30] = C_USER_INT07      ;                     //C_USER_INT07                              int      
            _tablerow[31] = C_USER_INT08      ;                     //C_USER_INT08                              int      
            _tablerow[32] = C_USER_INT09      ;                     //C_USER_INT09                              int      
            _tablerow[33] = C_USER_INT10      ;                     //C_USER_INT10                              int      
            _tablerow[34] = C_USER_REAL01     ;                     //C_USER_REAL01                             float     
            _tablerow[35] = C_USER_REAL02     ;                     //C_USER_REAL02                             float     
            _tablerow[36] = C_USER_REAL03     ;                     //C_USER_REAL03                             float     
            _tablerow[37] = C_USER_REAL04     ;                     //C_USER_REAL04                             float     
            _tablerow[38] = C_USER_REAL05     ;                     //C_USER_REAL05                             float     
            _tablerow[39] = C_USER_CHAR01     ;                     //C_USER_CHAR01                             char     1
            _tablerow[40] = C_USER_CHAR02     ;                     //C_USER_CHAR02                             char     1
            _tablerow[41] = C_USER_CHAR03     ;                     //C_USER_CHAR03                             char     1
            _tablerow[42] = C_USER_CHAR04     ;                     //C_USER_CHAR04                             char     1
            _tablerow[43] = C_USER_CHAR05     ;                     //C_USER_CHAR05                             char     1
            _tablerow[44] = C_USER_FLAG01     ;                     //C_USER_FLAG01                             bit      
            _tablerow[45] = C_USER_FLAG02     ;                     //C_USER_FLAG02                             bit      
            _tablerow[46] = C_USER_STRING01   ;                     //C_USER_STRING01                           varchar  29
            _tablerow[47] = C_USER_STRING02   ;                     //C_USER_STRING02                           varchar  29
            _tablerow[48] = C_USER_STRING03   ;                     //C_USER_STRING03                           varchar  29
            _tablerow[49] = C_USER_STRING04   ;                     //C_USER_STRING04                           varchar  29
            _tablerow[50] = C_USER_STRING05   ;                     //C_USER_STRING05                           varchar  29
            _tablerow[51] = C_USER_STRING06   ;                     //C_USER_STRING06                           varchar  29
            _tablerow[52] = C_USER_STRING07   ;                     //C_USER_STRING07                           varchar  29
            _tablerow[53] = C_USER_STRING08   ;                     //C_USER_STRING08                           varchar  29
            _tablerow[54] = C_USER_STRING09   ;                     //C_USER_STRING09                           varchar  29
            _tablerow[55] = C_USER_STRING10   ;                     //C_USER_STRING10                           varchar  29
            _tablerow[56] = C_USER_NOTE01     ;                     //C_USER_NOTE01                             varchar  99
            _tablerow[57] = C_USER_COLOR01    ;                     //C_USER_COLOR01                            int        
            _tablerow[58] = C_USER_COLOR02    ;                     //C_USER_COLOR02                            int        
            _tablerow[59] = DateTime_toCyb( C_USER_DATE01);         //C_USER_DATE01                             datetime   
            _tablerow[60] = DateTime_toCyb( C_USER_DATE02);         //C_USER_DATE02                             datetime   
            _tablerow[61] = DateTime_toCyb( C_USER_DATE03);         //C_USER_DATE03                             datetime   
            _tablerow[62] = DateTime_toCyb( C_USER_DATE04);         //C_USER_DATE04                             datetime   
            _tablerow[63] = DateTime_toCyb(C_USER_DATE05);          //C_USER_DATE05                             datetime   
            return _tablerow;
        }
        public override string GetSelectQuery(bool mode, string dossier, string codice_like, string filtro)
        {
            string db = "x3." + dossier;
            //string sage_query = "SELECT A.ITMREF_0, ITMSTA_0 from " + dossier + ".[ITMMASTER] A join " + dossier + ".[YITMINF] B on A.ITMREF_0=B.ITMREF_0" +
            //    " WHERE YLIVTRAS_0='" + tipo + "' and ITMSTA=1 ";

            string sage_query = @"SELECT 
S.SOHNUM_0 
,Q.SOPLIN_0 
,S.BPCORD_0
,Q.ITMREF_0 
,S.SALFCY_0
,Q.QTY_0 
,Q.DLVQTY_0 
,Q.ORDDAT_0 
,Q.YMRPDATAC_0  --(CHIEDERE A BIASIN X SHIDAT)  (9)
,Q.DEMDLVDAT_0 
--,(11) ??
,Q.SOQSTA_0   
,Q.YMRPFLGC_0 
,Q.CREUSR_0  
,S.ORDSTA_0 
,Q.YMRPFLGC_0
--(16) non penso serva



from " + db + @".SORDER S  
Inner join " + db + @".SORDERQ Q on Q.SOHNUM_0 = S.SOHNUM_0
Inner join " + db + @".SORDERP P on Q.SOHNUM_0 = P.SOHNUM_0
and Q.SOPLIN_0 = P.SOPLIN_0
and Q.SOQSEQ_0 = P.SOPSEQ_0

WHERE
Q.SOQSTA_0 <> 3
AND P.YPRODBLK_0 < 2 --0 non valorizzato   1 = non bloccato  2 = bloccato (non va schedulato)
AND S.ORDSTA_0 = 1
and S.SALFCY_0 = 'ITS01'
order by S.SOHNUM_0
";

            if (!string.IsNullOrWhiteSpace(codice_like))
            {
                sage_query += " and S.SOHNUM_0 like '" + codice_like.Trim() + "'";
            }
            return sage_query;
        }
        public override string GetID()
        {
            return C_CODE + "   " + C_ITEM_CODE;
        }

        public override void InitDataTable()
        {
            _dataTable.Columns.Add("C_CODE",                             typeof(string));           //varchar 30    
			_dataTable.Columns.Add("C_DESCR",                            typeof(string));           //varchar 30
			_dataTable.Columns.Add("C_ITEM_CODE",                        typeof(string));           //varchar 50
			_dataTable.Columns.Add("C_ITEM_PLANT",                       typeof(string));           //varchar 20
			_dataTable.Columns.Add("C_QTY",                              typeof(decimal));          //numeric 
			_dataTable.Columns.Add("C_COMPL_QTY",                        typeof(decimal));          //numeric 
			_dataTable.Columns.Add("C_INSERT_DATE",                      typeof(DateTime));        //datetime 
			_dataTable.Columns.Add("C_DUEDATE",                          typeof(DateTime));        //datetime 
			_dataTable.Columns.Add("C_REQUESTED_DUEDATE",                typeof(DateTime));        //datetime 
			_dataTable.Columns.Add("C_PROMISE_DUEDATE",                  typeof(DateTime));        //datetime                   
			_dataTable.Columns.Add("C_ORDERED_DATE",                     typeof(DateTime));        //datetime 
			_dataTable.Columns.Add("C_HOST_DUEDATE",                     typeof(DateTime));        //datetime 
			_dataTable.Columns.Add("C_TYPE",                             typeof(char));             //char 1
			_dataTable.Columns.Add("C_HOST_TYPE",                        typeof(string));           //varchar 20
			_dataTable.Columns.Add("C_STATUS",                           typeof(char));             //char 1
			_dataTable.Columns.Add("C_HOST_STATUS",                      typeof(string));           //varchar 15
			_dataTable.Columns.Add("C_CORDER_HEADER_CODE",               typeof(string));           //varchar 30
			_dataTable.Columns.Add("C_REF_CORDER",                       typeof(string));           //varchar 30
			_dataTable.Columns.Add("C_CO_GROUP",                         typeof(string));           //varchar 10
			_dataTable.Columns.Add("C_WAREHOUSE_CODE",                   typeof(string));           //varchar 20
			_dataTable.Columns.Add("C_VALUE",                            typeof(float));            //float 
			_dataTable.Columns.Add("C_COST",                             typeof(float));            //float 
			_dataTable.Columns.Add("C_MANAGER",                          typeof(string));           //varchar 20
			_dataTable.Columns.Add("C_CUSTOMER_CODE",                    typeof(string));           //varchar 30
			_dataTable.Columns.Add("C_USER_INT01",                       typeof(int));              //int 
			_dataTable.Columns.Add("C_USER_INT02",                       typeof(int));              //int 
			_dataTable.Columns.Add("C_USER_INT03",                       typeof(int));              //int 
			_dataTable.Columns.Add("C_USER_INT04",                       typeof(int));              //int 
			_dataTable.Columns.Add("C_USER_INT05",                       typeof(int));              //int 
			_dataTable.Columns.Add("C_USER_INT06",                       typeof(int));              //int 
			_dataTable.Columns.Add("C_USER_INT07",                       typeof(int));              //int 
			_dataTable.Columns.Add("C_USER_INT08",                       typeof(int));              //int 
			_dataTable.Columns.Add("C_USER_INT09",                       typeof(int));              //int 
			_dataTable.Columns.Add("C_USER_INT10",                       typeof(int));              //int 
			_dataTable.Columns.Add("C_USER_REAL01",                      typeof(float));            //float 
			_dataTable.Columns.Add("C_USER_REAL02",                      typeof(float));            //float 
			_dataTable.Columns.Add("C_USER_REAL03",                      typeof(float));            //float 
			_dataTable.Columns.Add("C_USER_REAL04",                      typeof(float));            //float 
			_dataTable.Columns.Add("C_USER_REAL05",                      typeof(float));            //float 
			_dataTable.Columns.Add("C_USER_CHAR01",                      typeof(char));             //char 1
			_dataTable.Columns.Add("C_USER_CHAR02",                      typeof(char));             //char 1
			_dataTable.Columns.Add("C_USER_CHAR03",                      typeof(char));             //char 1
			_dataTable.Columns.Add("C_USER_CHAR04",                      typeof(char));             //char 1
			_dataTable.Columns.Add("C_USER_CHAR05",                      typeof(char));             //char 1
			_dataTable.Columns.Add("C_USER_FLAG01",                      typeof(int));              //bit 
			_dataTable.Columns.Add("C_USER_FLAG02",                      typeof(int));              //bit 
			_dataTable.Columns.Add("C_USER_STRING01",                    typeof(string));           //varchar 29
			_dataTable.Columns.Add("C_USER_STRING02",                    typeof(string));           //varchar 29
			_dataTable.Columns.Add("C_USER_STRING03",                    typeof(string));           //varchar 29
			_dataTable.Columns.Add("C_USER_STRING04",                    typeof(string));           //varchar 29
			_dataTable.Columns.Add("C_USER_STRING05",                    typeof(string));           //varchar 29
			_dataTable.Columns.Add("C_USER_STRING06",                    typeof(string));           //varchar 29
			_dataTable.Columns.Add("C_USER_STRING07",                    typeof(string));           //varchar 29
			_dataTable.Columns.Add("C_USER_STRING08",                    typeof(string));           //varchar 29
			_dataTable.Columns.Add("C_USER_STRING09",                    typeof(string));           //varchar 29
			_dataTable.Columns.Add("C_USER_STRING10",                    typeof(string));           //varchar 29
			_dataTable.Columns.Add("C_USER_NOTE01",                      typeof(string));           //varchar 99
			_dataTable.Columns.Add("C_USER_COLOR01",                     typeof(int));              //int 
			_dataTable.Columns.Add("C_USER_COLOR02",                     typeof(int));              //int 
			_dataTable.Columns.Add("C_USER_DATE01",                      typeof(DateTime));         //datetime 
			_dataTable.Columns.Add("C_USER_DATE02",                      typeof(DateTime));         //datetime 
			_dataTable.Columns.Add("C_USER_DATE03",                      typeof(DateTime));         //datetime 
			_dataTable.Columns.Add("C_USER_DATE04",                      typeof(DateTime));         //datetime 
			_dataTable.Columns.Add("C_USER_DATE05",                      typeof(DateTime));        //
        }

        public override void LastAction(ref DBHelper2 cm, DBHelper2 sage)
        {
            //CONTROLLO che il terzo dell'ODV sia presente nell'anagrafica terzi
            string testo_mail = "";
            string chk_query = 
@"SELECT distinct O.C_CODE
, O.C_CUSTOMER_CODE
FROM [CyberPlanFrontiera].[dbo].[CYB_CORDER] O
left join CyberPlanFrontiera.dbo.CYB_COMPANY T on O.C_CUSTOMER_CODE=T.C_CODE
where T.C_CODE is null ";

            DbDataReader dtr = cm.GetReaderSelectCommand(chk_query);
            object[] row = new object[dtr.FieldCount];

            while (dtr.Read())
            {
                dtr.GetValues(row);
                testo_mail += "ODV =" + getDBV<string>(row[0], "C_CODE") + "  non caricato; manca codice cliente =" + getDBV<string>(row[1], "C_CUSTOMER_CODE") + Utils.NewLineMail();
            }

            Utils.SendMail_IT(Settings.GetSettings(), testo_mail, "ODV");
        }
    }
}
