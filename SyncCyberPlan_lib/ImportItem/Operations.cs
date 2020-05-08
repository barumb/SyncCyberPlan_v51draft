using System;
using System.Collections.Generic;
using log4net;
using System.Data;
using System.Threading;

namespace SyncCyberPlan_lib
{
    /// <summary>
    /// 1 a 1 con OPR
    /// </summary>
    public class Operations : Item
    {
        public string  MFGNUM_0;
        public decimal EXTQTY_0; //qta richiesta
        
        public DateTime? OPESTR_0;  //DATA
        public DateTime? OPEEND_0;  //DATA
        
        public decimal CPLQTY_0; // qta prodotta
        public decimal REJCPLQTY_0; //qta scartata
        public string  EXTWST_0;  //macchina
        public string  YATTCOD_0;  //attrezzature
        public decimal EXTSETTIM_0; //setup time
        public decimal EXTOPETIM_0; //run time
        public byte    MFGSTA_0;

        #region tabella output CYB_OPERATION
        public string C_ORDER_CODE;
        public int C_OPNUM;
        public string C_DESCR;
        public decimal C_QTY;
        public decimal C_SCRAP_QTY;
        public decimal C_COMPL_QTY;
        public decimal C_COMPL_SCRAP_QTY;
        public int C_STATUS;
        public DateTime? C_STDATE;
        public DateTime? C_DUEDATE;
        public DateTime? C_HOST_DUEDATE;
        public DateTime? C_ACT_STDATE;
        public DateTime? C_ACT_DUEDATE;
        public int C_QUEUE_TIME;
        public int C_WAIT_TIME;
        public int C_SETUP_TIME;
        public int C_RUN_TIME;
        public string C_SETUP_GROUP_CODE;
        public string C_SETUP_TEAM_GROUP_CODE;
        public float C_SETUP_TEAM_GROUP_QTY;
        public string C_RUN_TEAM_GROUP_CODE;
        public string C_WORKCENTER_CODE;
        public string C_HOST_WC;
        public string C_SUPPLIER_CODE;
        public char C_HIERARCHICAL_POSITION;
        public int C_USER_INT02;
        public int C_USER_INT03;
        public float C_USER_REAL01;
        public float C_USER_REAL02;
        public float C_USER_REAL03;
        public char C_USER_CHAR01;
        public char C_USER_CHAR02;
        public char C_USER_CHAR03;
        public byte C_USER_FLAG01;
        public byte C_USER_FLAG02;
        public string C_USER_STRING01;
        public string C_USER_STRING02;
        public string C_USER_STRING03;
        public int C_USER_COLOR01;
        public int C_USER_COLOR02;
        public DateTime? C_USER_DATE01;
        public DateTime? C_USER_DATE02;
        public DateTime? C_USER_DATE03;
        public int C_USER_TIME01;
        public int C_USER_TIME02;
        public int C_USER_TIME03;
        public int C_USER_TIME04;

        #endregion


        public Operations(): base("CYB_OPERATION")
        {
        }

        public override void Init(object[] row)
        {
            MFGNUM_0    = getDBV<string>(row[0], "MFGNUM_0");
                        
            EXTQTY_0    = getDBV<decimal>(row[1],     "EXTQTY_0");
            OPESTR_0    = getSageDate(row[2],        "OPESTR_0");
            OPEEND_0    = getSageDate(row[3],        "OPEEND_0");
            CPLQTY_0    = getDBV<decimal>(row[4],    "CPLQTY_0");
            REJCPLQTY_0 = getDBV<decimal>(row[5], "REJCPLQTY_0");
            EXTWST_0    = getDBV<string>(row[6],     "EXTWST_0");
            YATTCOD_0   = getDBV<string>(row[7],    "YATTCOD_0");

            EXTSETTIM_0 = getDBV<decimal>(row[8], "EXTSETTIM_0");
            EXTOPETIM_0 = getDBV<decimal>(row[9], "EXTOPETIM_0");
            MFGSTA_0    = getDBV<byte>(row[10], "MFGSTA_0");


            C_ORDER_CODE              = EscapeSQL(MFGNUM_0, 30); 
            C_OPNUM                   = 10;
            C_DESCR                   = EscapeSQL("",30);
            C_QTY                     = EXTQTY_0;
            C_SCRAP_QTY               = REJCPLQTY_0;
            C_COMPL_QTY               = CPLQTY_0;
            C_COMPL_SCRAP_QTY         = 0;
            C_STATUS                  = -1;
            C_STDATE                  = OPESTR_0;
            C_DUEDATE                 = OPEEND_0; 
            C_HOST_DUEDATE            = OPEEND_0; 
            C_ACT_STDATE              = null;
            C_ACT_DUEDATE             = null;
            C_QUEUE_TIME              = -1;
            C_WAIT_TIME               = -1;
            C_SETUP_TIME              = (int)EXTSETTIM_0; //getSetupTime(C_ORDER_CODE+" " + MFHCART + " Setup Time: MFVUTSE, MFVASET", MFVUTSE, MFVASET);
            C_RUN_TIME                = (int)EXTOPETIM_0; //getTotTime(C_ORDER_CODE + " " + MFHCART + " Tempo per pezzo/via MFVUTLM, MFVAMPT", MFVUTLM, MFVAMPT, C_QTY, FLVPZ, NRVIE);
            C_SETUP_GROUP_CODE        = "";
            C_SETUP_TEAM_GROUP_CODE   = "";
            C_SETUP_TEAM_GROUP_QTY    = -1;
            C_RUN_TEAM_GROUP_CODE     = "";
            C_WORKCENTER_CODE         = "";
            C_HOST_WC                 = "";
            C_SUPPLIER_CODE           = "";
            C_HIERARCHICAL_POSITION   = ' ';
            C_USER_INT02              = 0;
            C_USER_INT03              = 0;
            C_USER_REAL01             = 0;
            C_USER_REAL02             = 0;
            C_USER_REAL03             = 0;
            C_USER_CHAR01             = ' ';
            C_USER_CHAR02             = ' ';
            C_USER_CHAR03             = ' ';
            C_USER_FLAG01             = 0;
            C_USER_FLAG02             = 0;
            C_USER_STRING01           = EscapeSQL(YATTCOD_0, 29); //attrezzatura
            C_USER_STRING02           = EscapeSQL(EXTWST_0, 29); //macchina
            C_USER_STRING03           = "";
            C_USER_COLOR01            = 0;
            C_USER_COLOR02            = 0;
            C_USER_DATE01             = null;
            C_USER_DATE02             = null;
            C_USER_DATE03             = null;
            C_USER_TIME01             = 0;
            C_USER_TIME02             = 0;
            C_USER_TIME03             = 0;
            C_USER_TIME04             = 0;
        }
        public override DataRow GetCyberRow()
        {
            DataRow _tablerow = _dataTable.NewRow();
            _tablerow[0]  = C_ORDER_CODE;
            _tablerow[1]  = C_OPNUM;
            _tablerow[2]  = C_DESCR;
            _tablerow[3]  = C_QTY;
            _tablerow[4]  = C_SCRAP_QTY;
            _tablerow[5]  = C_COMPL_QTY;
            _tablerow[6]  = C_COMPL_SCRAP_QTY;
            _tablerow[7]  = C_STATUS;
            _tablerow[8]  = DateTime_toCyb(C_STDATE);
            _tablerow[9]  = DateTime_toCyb(C_DUEDATE);
            _tablerow[10] = DateTime_toCyb(C_HOST_DUEDATE);
            _tablerow[11] = DateTime_toCyb(C_ACT_STDATE);
            _tablerow[12] = DateTime_toCyb(C_ACT_DUEDATE);
            _tablerow[13] = C_QUEUE_TIME;
            _tablerow[14] = C_WAIT_TIME;
            _tablerow[15] = C_SETUP_TIME;
            _tablerow[16] = C_RUN_TIME;
            _tablerow[17] = C_SETUP_GROUP_CODE;
            _tablerow[18] = C_SETUP_TEAM_GROUP_CODE;
            _tablerow[19] = C_SETUP_TEAM_GROUP_QTY;
            _tablerow[20] = C_RUN_TEAM_GROUP_CODE;
            _tablerow[21] = C_WORKCENTER_CODE;
            _tablerow[22] = C_HOST_WC;
            _tablerow[23] = C_SUPPLIER_CODE;
            _tablerow[24] = C_HIERARCHICAL_POSITION;
            _tablerow[25] = C_USER_INT02;
            _tablerow[26] = C_USER_INT03;
            _tablerow[27] = C_USER_REAL01;
            _tablerow[28] = C_USER_REAL02;
            _tablerow[29] = C_USER_REAL03;
            _tablerow[30] = C_USER_CHAR01;
            _tablerow[31] = C_USER_CHAR02;
            _tablerow[32] = C_USER_CHAR03;
            _tablerow[33] = C_USER_FLAG01;
            _tablerow[34] = C_USER_FLAG02;
            _tablerow[35] = C_USER_STRING01;
            _tablerow[36] = C_USER_STRING02;
            _tablerow[37] = C_USER_STRING03;
            _tablerow[38] = C_USER_COLOR01;
            _tablerow[39] = C_USER_COLOR02;
            _tablerow[40] = DateTime_toCyb(C_USER_DATE01);
            _tablerow[41] = DateTime_toCyb(C_USER_DATE02);
            _tablerow[42] = DateTime_toCyb(C_USER_DATE03);
            _tablerow[43] = C_USER_TIME01;
            _tablerow[44] = C_USER_TIME02;
            _tablerow[45] = C_USER_TIME03;
            _tablerow[46] = C_USER_TIME04;

            return _tablerow;
        }

        public override string GetSelectQuery(bool mode, string dossier, string codice_like, string filtro)
        {
            string _db = "x3." + dossier;

            string query = @"select 
 O.MFGNUM_0
,O.EXTQTY_0
,O.OPESTR_0
,O.OPEEND_0
,O.CPLQTY_0
,O.REJCPLQTY_0
,O.EXTWST_0
,O.YATTCOD_0
,O.EXTSETTIM_0
,O.EXTOPETIM_0
,H.MFGSTA_0       --MFGSTA 1=Confermato  2= Pianificato  3=Suggerito 4=Chiuso
from " + _db + @".MFGHEAD H 
inner join " + _db + @".MFGOPE O on H.MFGNUM_0= O.MFGNUM_0
where 
 H.MFGSTA_0<=2 ";  

            if (!string.IsNullOrWhiteSpace(codice_like))
            {
             
            }

        query += " order by O.MFGNUM_0, O.OPENUM_0";
            return query;
        }
        public override string GetID()
        {
            return C_ORDER_CODE + C_OPNUM;
        }

        public override void InitDataTable()
        {
            _dataTable.Columns.Add("C_ORDER_CODE",            typeof(string));
            _dataTable.Columns.Add("C_OPNUM",                 typeof(int));
            _dataTable.Columns.Add("C_DESCR",                 typeof(string));
            _dataTable.Columns.Add("C_QTY",                   typeof(decimal));
            _dataTable.Columns.Add("C_SCRAP_QTY",             typeof(decimal));
            _dataTable.Columns.Add("C_COMPL_QTY",             typeof(decimal));
            _dataTable.Columns.Add("C_COMPL_SCRAP_QTY",       typeof(decimal));
            _dataTable.Columns.Add("C_STATUS",                typeof(int));
            _dataTable.Columns.Add("C_STDATE",                typeof(DateTime));
            _dataTable.Columns.Add("C_DUEDATE",               typeof(DateTime));
            _dataTable.Columns.Add("C_HOST_DUEDATE",          typeof(DateTime));
            _dataTable.Columns.Add("C_ACT_STDATE",            typeof(DateTime));
            _dataTable.Columns.Add("C_ACT_DUEDATE",           typeof(DateTime));
            _dataTable.Columns.Add("C_QUEUE_TIME",            typeof(int));
            _dataTable.Columns.Add("C_WAIT_TIME",             typeof(int));
            _dataTable.Columns.Add("C_SETUP_TIME",            typeof(int));
            _dataTable.Columns.Add("C_RUN_TIME",              typeof(int));
            _dataTable.Columns.Add("C_SETUP_GROUP_CODE",      typeof(string));
            _dataTable.Columns.Add("C_SETUP_TEAM_GROUP_CODE", typeof(string));
            _dataTable.Columns.Add("C_SETUP_TEAM_GROUP_QTY",  typeof(float));
            _dataTable.Columns.Add("C_RUN_TEAM_GROUP_CODE",   typeof(string));
            _dataTable.Columns.Add("C_WORKCENTER_CODE",       typeof(string));
            _dataTable.Columns.Add("C_HOST_WC",               typeof(string));
            _dataTable.Columns.Add("C_SUPPLIER_CODE",         typeof(string));
            _dataTable.Columns.Add("C_HIERARCHICAL_POSITION", typeof(char));
            _dataTable.Columns.Add("C_USER_INT02",            typeof(int));
            _dataTable.Columns.Add("C_USER_INT03",            typeof(int));
            _dataTable.Columns.Add("C_USER_REAL01",           typeof(float));
            _dataTable.Columns.Add("C_USER_REAL02",           typeof(float));
            _dataTable.Columns.Add("C_USER_REAL03",           typeof(float));
            _dataTable.Columns.Add("C_USER_CHAR01",           typeof(char));
            _dataTable.Columns.Add("C_USER_CHAR02",           typeof(char));
            _dataTable.Columns.Add("C_USER_CHAR03",           typeof(char));
            _dataTable.Columns.Add("C_USER_FLAG01",           typeof(byte));
            _dataTable.Columns.Add("C_USER_FLAG02",           typeof(byte));
            _dataTable.Columns.Add("C_USER_STRING01",         typeof(string));
            _dataTable.Columns.Add("C_USER_STRING02",         typeof(string));
            _dataTable.Columns.Add("C_USER_STRING03",         typeof(string));
            _dataTable.Columns.Add("C_USER_COLOR01",          typeof(int));
            _dataTable.Columns.Add("C_USER_COLOR02",          typeof(int));
            _dataTable.Columns.Add("C_USER_DATE01",           typeof(DateTime));
            _dataTable.Columns.Add("C_USER_DATE02",           typeof(DateTime));
            _dataTable.Columns.Add("C_USER_DATE03",           typeof(DateTime));
            _dataTable.Columns.Add("C_USER_TIME01",           typeof(int));
            _dataTable.Columns.Add("C_USER_TIME02",           typeof(int));
            _dataTable.Columns.Add("C_USER_TIME03",           typeof(int));
            _dataTable.Columns.Add("C_USER_TIME04",           typeof(int));
        }

        public override void LastAction(ref DBHelper2 cm, DBHelper2 sage)
        {
            //ThreadPool.QueueUserWorkItem(sendmail);
            Utils.SendMail_Plan(Settings.GetSettings(), __bulk_message, "operations");
        }
        //void sendmail(Object threadContext)
        //{
        //    Utils.SendMail("it@sauro.net", "francesco.chiminazzo@sauro.net", __bulk_message);
        //}
    }
}
