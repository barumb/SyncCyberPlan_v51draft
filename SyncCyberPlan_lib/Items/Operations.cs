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
        public string  MFHTORD;
        public decimal MFHAORD;
        public decimal MFHPORD;
        public string  MFHTCOM;
        public decimal MFHACOM;
        public decimal MFHPCOM;
        public decimal MFHSCOM;
        public string  MFHCART;
        public decimal MFHQTRC; //qta richiesta
        public decimal MFHDCRE; //DATA
        public string  MFHSTAT;
        public decimal MFVDINI;  //DATA
        public decimal MFVDEND;  //DATA
        public string  MFVSTAV; // stato riga  MFVSTAV  se =ST ordine in corso, se vuoto ordine in attesa
        public decimal MFHQTPR; // qta prodotta
        public decimal MFVQTSC; //qta scartata
        public string  MFVMACN;  //macchina
        public string  MFVCSTM;  //attrezzature
        public string  MFVWRKC;  //centro di lavoro
        public string  MFVWKCT;  //tipo centro di lavoro (I=interno  E= esterno)

        public string  MFVUTLM;  //unita di misura tempo per un pezzo/via  1=ORE  2=100MI-HR   3 Minuti 4 giorni  5 settimane
        public decimal MFVAMPT;  //tempo per un pezzo/via
        public string  MFVUTSE;  //unita di misura tempo di setup
        public decimal MFVASET;  //tempo di setup

        public string FLVPZ; //flag Vie pezzi dell'attrezzatura

        public int NRVIE;   //numero vie totali articolo



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
            MFHTORD = getDBV<string>(row[0]);
            MFHAORD = getDBV<decimal>(row[1]);
            MFHPORD = getDBV<decimal>(row[2]);
            MFHTCOM = getDBV<string>(row[3]);
            MFHACOM = getDBV<decimal>(row[4]);
            MFHPCOM = getDBV<decimal>(row[5]);
            MFHSCOM = getDBV<decimal>(row[6]);
            MFHCART = getDBV<string>(row[7]);
            MFHQTRC = getDBV<decimal>(row[8]);
            MFHDCRE = getDBV<decimal>(row[9]);
            MFHSTAT = getDBV<string>(row[10]);
            MFVDINI = getDBV<decimal>(row[11]);
            MFVDEND = getDBV<decimal>(row[12]);
            MFVSTAV = getDBV<string>(row[13]);
            MFHQTPR = getDBV<decimal>(row[14]);
            MFVQTSC = getDBV<decimal>(row[15]);
            MFVMACN = getDBV<string>(row[16]);
            MFVCSTM = getDBV<string>(row[17]);
            MFVWRKC = getDBV<string>(row[18]);
            MFVWKCT = getDBV<string>(row[19]);

            MFVUTLM = getDBV<string>(row[20]);   //unita di misura tempo per un pezzo/via  1=ORE  2=100MI-HR   3 Minuti 4 giorni  5 settimane
            MFVAMPT = getDBV<decimal>(row[21]);  //tempo per un pezzo/via
            MFVUTSE = getDBV<string>(row[22]);   //unita di misura tempo di setup
            MFVASET = getDBV<decimal>(row[23]);  //tempo di setup

            FLVPZ = getDBV<string>(row[24]);  //flag vie pezzi dell'attrezzatura

            NRVIE = getDBV<int>(row[25]);     //numero vie totali dell'articolo


            C_ORDER_CODE              = EscapeSQL(MFHTORD + MFHAORD.ToString("00") + MFHPORD.ToString("000000"), 30); 
            C_OPNUM                   = 10;
            C_DESCR                   = EscapeSQL("",30);
            C_QTY                     = MFHQTRC;
            C_SCRAP_QTY               = MFVQTSC;
            C_COMPL_QTY               = MFHQTPR;
            C_COMPL_SCRAP_QTY         = 0;
            C_STATUS                  = -1;
            C_STDATE                  = dateTime_fromDataAs400(MFVDINI);
            C_DUEDATE                 = dateTime_fromDataAs400(MFVDEND); 
            C_HOST_DUEDATE            = dateTime_fromDataAs400(MFVDEND); 
            C_ACT_STDATE              = null;
            C_ACT_DUEDATE             = null;
            C_QUEUE_TIME              = -1;
            C_WAIT_TIME               = -1;
            C_SETUP_TIME              = getSetupTime(C_ORDER_CODE+" " + MFHCART + " Setup Time: MFVUTSE, MFVASET", MFVUTSE, MFVASET);
            C_RUN_TIME                = getTotTime(C_ORDER_CODE + " " + MFHCART + " Tempo per pezzo/via MFVUTLM, MFVAMPT", MFVUTLM, MFVAMPT, C_QTY, FLVPZ, NRVIE);
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
            C_USER_STRING01           = EscapeSQL(MFVCSTM, 29); //attrezzatura
            C_USER_STRING02           = EscapeSQL(MFVMACN, 29); //macchina
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

   


            //set_Attrezzatura_ASS_VieMinuto();
            //set_Attrezzatura_ASS_PzOra();
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
            string __libreriaAs400 = dossier;

            string _tabMFH = __libreriaAs400 + ".MFH00PF";
            string _tabMFV = __libreriaAs400 + ".MFV00PF";

            string _tabRSH = __libreriaAs400 + ".RSHD00F";
            string _tabPFH = __libreriaAs400 + ".PFHD00F";

            string query = "SELECT " + "\n"
                + "  " + _tabMFH + ".MFHTORD" + "\n"
                + ",  " + _tabMFH + ".MFHAORD" + "\n"
                + ",  " + _tabMFH + ".MFHPORD" + "\n"
                + ",  " + _tabMFH + ".MFHTCOM" + "\n"
                + ",  " + _tabMFH + ".MFHACOM" + "\n"
                + ",  " + _tabMFH + ".MFHPCOM" + "\n"
                + ",  " + _tabMFH + ".MFHSCOM" + "\n"
                + ",  " + _tabMFH + ".MFHCART" + "\n"
                + ",  " + _tabMFH + ".MFHQTRC" + "\n"
                + ",  " + _tabMFH + ".MFHDCRE" + "\n"
                + ",  " + _tabMFH + ".MFHSTAT" + "\n"
                + ",  " + _tabMFV + ".MFVDINI" + "\n"
                + ",  " + _tabMFV + ".MFVDEND" + "\n"
                + ",  " + _tabMFV + ".MFVSTAV" + "\n"
                + ",  " + _tabMFH + ".MFHQTPR" + "\n"
                + ",  " + _tabMFV + ".MFVQTSC" + "\n"
                + ",  " + _tabMFV + ".MFVMACN" + "\n"
                + ",  " + _tabMFV + ".MFVCSTM" + "\n"
                + ",  " + _tabMFV + ".MFVWRKC" + "\n"
                + ",  " + _tabMFV + ".MFVWKCT" + "\n"

                + ",  " + _tabMFV + ".MFVUTLM" + "\n"
                + ",  " + _tabMFV + ".MFVAMPT" + "\n"
                + ",  " + _tabMFV + ".MFVUTSE" + "\n"
                + ",  " + _tabMFV + ".MFVASET" + "\n"

                + ",  " + _tabRSH + ".FLVPZ" + "\n"

                + ",  " + _tabPFH + ".NRVIE" + "\n"

                + " FROM " + _tabMFH + "\n"
                + " INNER JOIN " + _tabMFV + " ON " + "\n"
                                 + _tabMFH + ".MFHTORD = " + _tabMFV + ".MFVTORD " + "\n"
                       + " AND " + _tabMFH + ".MFHAORD = " + _tabMFV + ".MFVAORD " + "\n"
                       + " AND " + _tabMFH + ".MFHPORD = " + _tabMFV + ".MFVPORD " + "\n"
                + " INNER JOIN " + _tabRSH + " ON " + "\n"
                                 + _tabRSH + ".CDSTM = " + _tabMFV + ".MFVCSTM " + "\n"
                + " INNER JOIN " + _tabPFH + " ON " + "\n"
                                 + _tabPFH + ".CDART = " + _tabMFH + ".MFHCART " + "\n"

                + " WHERE " + _tabMFH + ".MFHSTAT = 'RI' " + "\n"     //sempre RI
                + " and   " + _tabMFV + ".MFVSTAT = 'RI' " + "\n"     //sempre RI
                + " and   " + _tabMFV + ".MFVSTAV <> 'CH' " + "\n"    //questo indica se la riga è chiusa
                ;

            if (!string.IsNullOrWhiteSpace(codice_like))
            {
                query += " and " + _tabMFH + ".MFHCART like '" + codice_like.Trim() + "'";
            }

            query += " ORDER BY "
                + "  " + _tabMFH + ".MFHTORD," + "\n"
                + "  " + _tabMFH + ".MFHAORD," + "\n"
                + "  " + _tabMFH + ".MFHPORD" + "\n"
                ;
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

        /// <summary>
        /// restituisce valore in minuti
        /// </summary>
        /// <param name="unita_di_misura"></param>
        /// <param name="tempo"></param>
        /// <returns></returns>
        int getSetupTime(string commento, string unita_di_misura, decimal tempo)
        {
            int tmp_tempo = (int)tempo;            
            int ret = -1;
            switch (unita_di_misura.Trim())
            {
                case "1": ret = tmp_tempo * 60; break;       //1=ORE
                case "2":
                    __bulk_message += System.Environment.NewLine + commento + System.Environment.NewLine + " ha unità tempo pari a 2";
                    //Utils.SendMail("it@sauro.net", "francesco.chiminazzo@sauro.net", "mail.sauro.net", commento + System.Environment.NewLine + " ha unità tempo pari a 2");
                    break;
                case "3": ret = tmp_tempo ; break;           //3 = minuti
                case "4": ret = tmp_tempo * 60*24; break;    //4 = giorni
                case "5": ret = tmp_tempo * 60*24*7; break;  //5 = settimane
                default:
                    __bulk_message += System.Environment.NewLine + commento + System.Environment.NewLine + " ha unità di misura non prevista: " + unita_di_misura;
                    //Utils.SendMail("it@sauro.net", "francesco.chiminazzo@sauro.net", "mail.sauro.net", commento + System.Environment.NewLine + " ha unità di misura non prevista: " + unita_di_misura);
                    break;
            }
            if (ret == -1)
            {
                ;
            }
            return ret;
        }
        int getTotTime(string commento, string unita_di_misura, decimal tempo_per_pezzo_via, decimal pezzi_richiesti, string FlagViePezzi,int NumVieTotali)
        {
            decimal tmp_tempo = pezzi_richiesti * tempo_per_pezzo_via;
            if (FlagViePezzi.Trim().ToUpper() == "V")
            {
                //se l'attrezzatura va a vie moltiplico per il nuemro di vie
                tmp_tempo = tmp_tempo * NumVieTotali;
            }
            return getSetupTime(commento, unita_di_misura, tmp_tempo);
        }

        public override void LastAction(ref DBHelper2 cm)
        {
            if (!string.IsNullOrWhiteSpace(__bulk_message))
            { 
                //ThreadPool.QueueUserWorkItem(sendmail);
                Utils.SendMail("it@sauro.net", "francesco.chiminazzo@sauro.net", "mail.sauro.net", __bulk_message);
            }
        }
        //void sendmail(Object threadContext)
        //{
        //    Utils.SendMail("it@sauro.net", "francesco.chiminazzo@sauro.net", "mail.sauro.net", __bulk_message);
        //}
    }
}
