using System.Collections.Generic;
using System;
using log4net;
using System.Data;
using System.Data.Common;

namespace SyncCyberPlan_lib
{
    public class OrdiniAcq_OPR_As400_FIRSTIMPORT 
    {
        protected static readonly ILog _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name);

        public string  MFHTORD;
        public decimal MFHAORD;
        public decimal MFHPORD;
        public string  MFHTCOM;
        public decimal MFHACOM; // anno ODV
        public decimal MFHPCOM; // progressivo ODV
        public decimal MFHSCOM; // riga ODV
        public string  MFHCART;
        public decimal MFHQTRC;
        public decimal MFHDCRE; // DATA
        public string  MFHSTAT;
        public decimal MFVDINI; // DATA
        public decimal MFVDEND; // DATA
        public string  MFVSTAV; // stato riga  MFVSTAV  se =ST ordine in corso, se vuoto ordine in attesa
        public decimal MFHQTPR; // qta prodotta
        public decimal MFVQTSC; // qta scartata

        public string  MFVUTLM; // unita di misura tempo per un pezzo/via  1=ORE  2=100MI-HR   3 Minuti 4 giorni  5 settimane
        public decimal MFVAMPT; // tempo per un pezzo/via
        public string  MFVUTSE; // unita di misura tempo di setup
        public decimal MFVASET; // tempo di setup

        public string MFVWRKC;  // centro di lavoro (Interno/esterno in as400)

        public string SOHNUM;
        public string SOHLIN;

        public string MFVMACN;
        public string MFVCSTM;

        public string FLVPZ; //flag Vie pezzi dell'attrezzatura
        public int NRVIE;   //numero vie totali articolo

        public string ARTUNMI;

        public OrdiniAcq_OPR_As400_FIRSTIMPORT(/*string YPOHTYP*/): base()
        {
            //__YPOHTYP_filter = YPOHTYP;
        }
        T getDBV<T>(object obj, string nomecampo)
        {
            T ret;
            if (!DBNull.Value.Equals(obj))
            {
                ret = (T)obj;
                if (typeof(T) == typeof(String))
                {
                    string tmp = (string)obj;
                    if (tmp.Length > 1) //se = 1 potrebbe essere un solo carattere (spazio) di un campo char(1), e non va cancellato
                    {
                        ret = (T)(object)tmp.Trim();
                    }
                }
            }
            else
            {
                ret = default(T);
                //_logger.Debug(GetID() + " campo " + typeof(T).ToString() + " is DBnull ");
                _logger.Debug(" campo " + nomecampo + " (" + typeof(T).Name + ") is DBnull ");
            }
            return ret;
        }
        DateTime? dateTime_fromDataAs400(decimal data_as400)
        {
            DateTime? ret = null;
            if (data_as400 > 10000)
            {
                ret = new DateTime((int)data_as400 / 10000, (int)data_as400 / 100 % 100, (int)data_as400 % 100);
            }
            return ret;
        }
        string EscapeSQL(string field, int strlen)
        {
            string ret = "?";
            if (field != null)
            {
                ret = field.Replace("'", "''");  //escape del singolo apice ' : si mette due volte ''
                ret = ret.Replace("  ", " ");

                if (ret.Length > strlen)
                {
                    ret = ret.Substring(0, strlen);
                }
            }
            return ret;
        }


        public void Init(object[] row)
        {
            MFHTORD = getDBV<string>(row[0], "MFHTORD");
            MFHAORD = getDBV<decimal>(row[1], "MFHAORD");
            MFHPORD = getDBV<decimal>(row[2], "MFHPORD");
            MFHTCOM = getDBV<string>(row[3], "MFHTCOM");
            MFHACOM = getDBV<decimal>(row[4], "MFHACOM");
            MFHPCOM = getDBV<decimal>(row[5], "MFHPCOM");
            MFHSCOM = getDBV<decimal>(row[6], "MFHSCOM");
            MFHCART = getDBV<string>(row[7], "MFHCART");
            MFHQTRC = getDBV<decimal>(row[8], "MFHQTRC");
            MFHDCRE = getDBV<decimal>(row[9], "MFHDCRE");
            MFHSTAT = getDBV<string>(row[10], "MFHSTAT");
            MFVDINI = getDBV<decimal>(row[11], "MFVDINI");
            MFVDEND = getDBV<decimal>(row[12], "MFVDEND");
            MFVSTAV = getDBV<string>(row[13], "MFVSTAV");
            MFHQTPR = getDBV<decimal>(row[14], "MFHQTPR");
            MFVQTSC = getDBV<decimal>(row[15], "MFVQTSC");

            MFVUTLM = getDBV<string>(row[16], "MFVUTLM");
            MFVAMPT = getDBV<decimal>(row[17], "MFVAMPT");
            MFVUTSE = getDBV<string>(row[18], "MFVUTSE");
            MFVASET = getDBV<decimal>(row[19], "MFVASET");

            MFVWRKC = getDBV<string>(row[20], "MFVWRKC");

            SOHNUM = getDBV<string>(row[21], "SOHNUM");
            SOHLIN = getDBV<string>(row[22], "SOHLIN");

            MFVMACN = getDBV<string>(row[23], "MFVMACN");
            MFVCSTM = getDBV<string>(row[24], "MFVCSTM");


            FLVPZ = getDBV<string>(row[25], "FLVPZ");  //flag vie pezzi dell'attrezzatura
            NRVIE = (int)getDBV<decimal>(row[26], "NRVIE");     //numero vie totali dell'articolo

            ARTUNMI = getDBV<string>(row[27], "ARTUNMI");

        }
        int getSetupTime(string commento, string unita_di_misura, decimal tempo)
        {
            int tmp_tempo = (int)tempo;
            int ret = -1;
            switch (unita_di_misura.Trim())
            {
                case "1": ret = tmp_tempo * 60; break;       //1=ORE
                case "2":
                    _logger.Info(commento + Utils.NewLineMail() + " ha unità tempo pari a <2>" + Utils.NewLineMail());
                    break;
                case "3": ret = tmp_tempo; break;           //3 = minuti
                case "4": ret = tmp_tempo * 60 * 24; break;    //4 = giorni
                case "5": ret = tmp_tempo * 60 * 24 * 7; break;  //5 = settimane
                default:
                    _logger.Info(commento + Utils.NewLineMail() + " ha unità di misura non prevista: < " + unita_di_misura + " >" + Utils.NewLineMail());
                    break;
            }
            if (ret == -1)
            {
                ;
            }
            return ret;
        }
        int getTotTime(string commento, string unita_di_misura, decimal tempo_per_pezzo_via, decimal pezzi_richiesti, string FlagViePezzi, int NumVieTotali)
        {
            decimal tmp_tempo = pezzi_richiesti * tempo_per_pezzo_via;
            if (FlagViePezzi.Trim().ToUpper() == "V")
            {
                //se l'attrezzatura va a vie moltiplico per il numero di vie
                tmp_tempo = tmp_tempo * NumVieTotali;
            }
            return getSetupTime(commento, unita_di_misura, tmp_tempo);
        }
        public string getSageImportString()
        {
            string __SEP = ";"; //separatore
            //YMFG  creazione
            //A;ITS01;1;WP7467-MV;310719;010819;1000;;WWCICLO00;1;C;2;ODV,rigaODV
            //B;5;1000;STP122;P27;310719;310720;14400;3750
            //B;10;1000;STP123;P28;300919;310721;14400;3750
            //C;testcodbbb;tskNUM

            //YMFG modifica
            //A;ITS01;1;WP7467-MV;310719;010819;1000;ODP19ITS0100109;WWCICLO00;1;M;2;ODV,rigaODV
            //B;5;1000;STP133;P20;310725;310725;123;333
            //B;10;1000;STP144;P21;300925;310725;12300;344
            //C;testcodbbb;tskNUM


            //manca FATTIBIILITA': Savietto la può passare tramite il codice Esadecimale del colore
            String oprcode = EscapeSQL(MFHTORD + MFHAORD.ToString("00") + MFHPORD.ToString("000000"), 30);

            return
                "A" +                                                              //
                __SEP + "ITS01" +                                                  //
                __SEP + "1" +                                                      //BOMALT
                                                                                   //__SEP + OPE_C_OPNUM +                                              //BOMOPE = openum
                __SEP + MFHCART +                                                  //
                __SEP + string.Format("{0:ddMMyy}", dateTime_fromDataAs400(MFVDINI)) +                          //
                __SEP + string.Format("{0:ddMMyy}", dateTime_fromDataAs400(MFVDEND)) +                          //
                __SEP + MFHQTRC +                                                  //
                __SEP + "" +                  //oprcode +                          //numero ODP
                __SEP + "WWCICLO00" +                                              //*1
                __SEP + "1" +                                                      //*2
                __SEP + "C" +                                                      //*3 sempre CREAZIONE (PRIMO IMPORT)
                __SEP + "1" +                                                      //*4  //MFGSTA  2= Pianificato  1= Confermato
                __SEP + EscapeSQL(SOHNUM, 30) +                                    //odv
                __SEP + SOHLIN +                                                   //RIGA odv
                "\nB" +                                                            //
                __SEP + "10" +                                                    //OPENUM
                __SEP + "1" +                                                      //EXTWSTNBR, numero macchine, fisso 1
                __SEP + ARTUNMI +                                                  //OPEUOM Unita di misura operazione
                __SEP + "1" +                                                      //OPESTUCOE coeff. tra Unità di misura operazione e UM articolo, 
                __SEP + MFHQTRC +                                                  //
                __SEP + MFVCSTM +                                                  //ATTREZZATURA
                __SEP + MFVMACN +                                                  //MACCHINA
                __SEP + string.Format("{0:ddMMyy}", dateTime_fromDataAs400(MFVDINI)) +                          //
                __SEP + string.Format("{0:ddMMyy}", dateTime_fromDataAs400(MFVDEND)) +                          //
                __SEP + getSetupTime(oprcode + " " + MFHCART + " Setup Time: MFVUTSE, MFVASET", MFVUTSE, MFVASET) + //minuti
                __SEP + getTotTime(oprcode + " " + MFHCART + " Tempo per pezzo/via MFVUTLM, MFVAMPT", MFVUTLM, MFVAMPT, MFHQTRC, FLVPZ, NRVIE) +     //minuti            
                            
            // NON LO IMPORTA, NON C?é IN MASCHERA __SEP + getMFGSTA(ORD_C_CODE) +                                    //*4  //MFGSTA  2= Pianificato  1= Confermato (per le operazioni)
            __SEP + "1" +                                                      //  1= NO contolavoro 2= contolavoro strtturale  3= CL congiunturale
                "\nC" +                                                            //
                __SEP + "" +                                                       //
                __SEP + "" +                                                       //
                __SEP + oprcode                                                   // COD As400

                                                                                   //MFGMAT non viene importata   "\nD" +
                                                                                   //MFGMAT non viene importata   //__SEP + DEM_MFGLIN +
                                                                                   //MFGMAT non viene importata   __SEP + DEM_C_NSEQ +
                                                                                   //MFGMAT non viene importata   __SEP + DEM_C_ITEM_CODE +
                                                                                   //MFGMAT non viene importata   __SEP + DEM_C_QTY +
                                                                                   //MFGMAT non viene importata   __SEP + (DEM_C_QTY/ORD_C_QTY) +  //qta base = qta di legame                
                +"\n"; //non usare fs.WriteLine: aggineg \r\n, non solo \n
        }
        static public string GetSelectQuery_opr_CON_ODV(string libreriaAs400)
        {            
            string _tabMFH = libreriaAs400 + ".MFH00PF";
            string _tabMFV = libreriaAs400 + ".MFV00PF";
            string _tabVOO = libreriaAs400 + ".VOOS00F";
            string _tabRSH = libreriaAs400 + ".RSHD00F";
            string _tabPFH = libreriaAs400 + ".PFHD00F";
            string _tabART = libreriaAs400 + ".ART00PF";

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

                + ",  " + _tabMFV + ".MFVUTLM" + "\n"
                + ",  " + _tabMFV + ".MFVAMPT" + "\n"
                + ",  " + _tabMFV + ".MFVUTSE" + "\n"
                + ",  " + _tabMFV + ".MFVASET" + "\n"
                + ",  " + _tabMFV + ".MFVWRKC" + "\n"
                + ",  " + _tabVOO + ".SOHNR" + "\n"
                + ",  " + _tabVOO + ".SOLNR" + "\n"

                + ",  " + _tabMFV + ".MFVMACN" + "\n" 
                + ",  " + _tabMFV + ".MFVCSTM" + "\n"

                + ",  " + _tabRSH + ".FLVPZ" + "\n"
                + ",  " + _tabPFH + ".NRVIE" + "\n"

                + ",  " + _tabART + ".ARTUNMI" + "\n"

                + " FROM " + _tabMFH + "\n"
                + " INNER JOIN " + _tabMFV + " ON " + "\n"
                                 + _tabMFH + ".MFHTORD = " + _tabMFV + ".MFVTORD " + "\n"
                       + " AND " + _tabMFH + ".MFHAORD = " + _tabMFV + ".MFVAORD " + "\n"
                       + " AND " + _tabMFH + ".MFHPORD = " + _tabMFV + ".MFVPORD " + "\n"
                + "left JOIN " + _tabVOO + " ON " + "\n"
                                 + _tabVOO + ".TPOVE = " + _tabMFH + ".MFHTCOM " + "\n"
                       + " AND " + _tabVOO + ".AAOVE = " + _tabMFH + ".MFHACOM " + "\n"
                       + " AND " + _tabVOO + ".NROVE = " + _tabMFH + ".MFHPCOM " + "\n"
                       + " AND " + _tabVOO + ".LNOVE = " + _tabMFH + ".MFHSCOM " + "\n"
                + " INNER JOIN " + _tabRSH + " ON " + "\n"
                                 + _tabRSH + ".CDSTM = " + _tabMFV + ".MFVCSTM " + "\n"
                + " INNER JOIN " + _tabPFH + " ON " + "\n"
                                 + _tabPFH + ".CDART = " + _tabMFH + ".MFHCART " + "\n"
                + " INNER JOIN " + _tabART + " ON " + "\n"
                                 + _tabART + ".ARTCART = " + _tabMFH + ".MFHCART " + "\n"

                + " WHERE " + _tabMFH + ".MFHSTAT = 'RI' " + "\n"     //sempre RI
                + " and   " + _tabMFV + ".MFVSTAT = 'RI' " + "\n"     //sempre RI
                + " and   " + _tabMFV + ".MFVSTAV <> 'CH' " + "\n"    //questo indica se la riga è chiusa
                + " AND " + _tabMFH + ".MFHTCOM<>'' and " + _tabVOO + ".SOLNR <> '' "
                ;

            //if (!string.IsNullOrWhiteSpace(codice_like))
            //{
            //    query += " and " + _tabMFH + ".MFHCART like '" + codice_like.Trim() + "'";
            //}

            query += " ORDER BY " 
                + "  " + _tabMFH + ".MFHTORD," + "\n"
                + "  " + _tabMFH + ".MFHAORD," + "\n"
                + "  " + _tabMFH + ".MFHPORD" + "\n"
                ;
            return query;
        }
        static public string GetSelectQuery_opr_SENZA_ODV(string libreriaAs400)
        {
            string _tabMFH = libreriaAs400 + ".MFH00PF";
            string _tabMFV = libreriaAs400 + ".MFV00PF";
            string _tabVOO = libreriaAs400 + ".VOOS00F";
            string _tabRSH = libreriaAs400 + ".RSHD00F";
            string _tabPFH = libreriaAs400 + ".PFHD00F";
            string _tabART = libreriaAs400 + ".ART00PF";

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

                + ",  " + _tabMFV + ".MFVUTLM" + "\n"
                + ",  " + _tabMFV + ".MFVAMPT" + "\n"
                + ",  " + _tabMFV + ".MFVUTSE" + "\n"
                + ",  " + _tabMFV + ".MFVASET" + "\n"
                + ",  " + _tabMFV + ".MFVWRKC" + "\n"
                + ", '' \n" //+ ",  " + _tabVOO + ".SOHNR" + "\n"
                + ", '' \n" //+ ",  " + _tabVOO + ".SOLNR" + "\n"

                + ",  " + _tabMFV + ".MFVMACN" + "\n"
                + ",  " + _tabMFV + ".MFVCSTM" + "\n"

                + ",  " + _tabRSH + ".FLVPZ" + "\n"
                + ",  " + _tabPFH + ".NRVIE" + "\n"

                + ",  " + _tabART + ".ARTUNMI" + "\n"

                + " FROM " + _tabMFH + "\n"
                + " INNER JOIN " + _tabMFV + " ON " + "\n"
                                 + _tabMFH + ".MFHTORD = " + _tabMFV + ".MFVTORD " + "\n"
                       + " AND " + _tabMFH + ".MFHAORD = " + _tabMFV + ".MFVAORD " + "\n"
                       + " AND " + _tabMFH + ".MFHPORD = " + _tabMFV + ".MFVPORD " + "\n"
//                + "left JOIN " + _tabVOO + " ON " + "\n"
//                                 + _tabVOO + ".TPOVE = " + _tabMFH + ".MFHTCOM " + "\n"
//                       + " AND " + _tabVOO + ".AAOVE = " + _tabMFH + ".MFHACOM " + "\n"
//                       + " AND " + _tabVOO + ".NROVE = " + _tabMFH + ".MFHPCOM " + "\n"
//                       + " AND " + _tabVOO + ".LNOVE = " + _tabMFH + ".MFHSCOM " + "\n"
                + " INNER JOIN " + _tabRSH + " ON " + "\n"
                                 + _tabRSH + ".CDSTM = " + _tabMFV + ".MFVCSTM " + "\n"
                + " INNER JOIN " + _tabPFH + " ON " + "\n"
                                 + _tabPFH + ".CDART = " + _tabMFH + ".MFHCART " + "\n"
                + " INNER JOIN " + _tabART + " ON " + "\n"
                                 + _tabART + ".ARTCART = " + _tabMFH + ".MFHCART " + "\n"

                + " WHERE " + _tabMFH + ".MFHSTAT = 'RI' " + "\n"     //sempre RI
                + " and   " + _tabMFV + ".MFVSTAT = 'RI' " + "\n"     //sempre RI
                + " and   " + _tabMFV + ".MFVSTAV <> 'CH' " + "\n"    //questo indica se la riga è chiusa
                + " AND " + _tabMFH + ".MFHTCOM='' "
                ;

            //if (!string.IsNullOrWhiteSpace(codice_like))
            //{
            //    query += " and " + _tabMFH + ".MFHCART like '" + codice_like.Trim() + "'";
            //}

            query += " ORDER BY "
                + "  " + _tabMFH + ".MFHTORD," + "\n"
                + "  " + _tabMFH + ".MFHAORD," + "\n"
                + "  " + _tabMFH + ".MFHPORD" + "\n"
                ;
            return query;
        }

        static public void GetFileImport_YMFG_Sage()
        {
            string libreriaAs400 = "MBM41LIB_M";
            DBHelper2 sage = DBHelper2.getAs400DBHelper(libreriaAs400);
            _logger.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name + " start...");

            string qry = GetSelectQuery_opr_CON_ODV(libreriaAs400);
            DbDataReader dtr = sage.GetReaderSelectCommand(qry);
            object[] row = new object[dtr.FieldCount];
            OrdiniAcq_OPR_As400_FIRSTIMPORT opr = null;

            string pathfile = @"\\srvx3app1\S$\Sage\SAGEX3\folders\SAURO\YSAURO\IMPEXP\Import_Iniziale_OPR_Da_As400_CON_ODV.txt";
            pathfile = @"Import_Iniziale_OPR_Da_As400_CON_ODV.txt";
            using (System.IO.StreamWriter fs = new System.IO.StreamWriter(pathfile, false))
            {
                while (dtr.Read())
                {
                    opr = new OrdiniAcq_OPR_As400_FIRSTIMPORT();
                    dtr.GetValues(row);
                    opr.Init(row);
                    fs.Write(opr.getSageImportString());
                }                
            }
            dtr.Close();


            qry = GetSelectQuery_opr_SENZA_ODV(libreriaAs400);
            dtr = sage.GetReaderSelectCommand(qry);
            row = new object[dtr.FieldCount];
            opr = null;

            pathfile = @"\\srvx3app1\S$\Sage\SAGEX3\folders\SAURO\YSAURO\IMPEXP\Import_Iniziale_OPR_Da_As400_SENZA_ODV.txt";
            pathfile = @"Import_Iniziale_OPR_Da_As400_SENZA_ODV.txt";
            using (System.IO.StreamWriter fs = new System.IO.StreamWriter(pathfile, false))
            {
                while (dtr.Read())
                {
                    opr = new OrdiniAcq_OPR_As400_FIRSTIMPORT();
                    dtr.GetValues(row);
                    opr.Init(row);
                    fs.Write(opr.getSageImportString());
                }
            }
            dtr.Close();

            _logger.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name + " end");
            return;
        }

        protected char get_C_M_B(string CDL_As400)
        {
            if (CDL_As400 == "CDLEXT")
                return 'D';  //contolavoro
            else if (CDL_As400 == "CDLINT")
                return 'M';  //make
            else
            {
                _logger.Info("Errore import OPR in CyberPlan: OPRxxx ha cdl in as400 diverso da CDLEXT/CDLINT");
                return '?';
            }
        }
        
        char getMRP_type(string MFHTCOM)
        {
            //F=MTS make to stock (a fabbisogno)  C= MTO make to order (a commessa)  
            if (MFHTCOM.Trim() !=  "")
            {
                return 'C'; //ordine a commessa
            }
            else
            {
                return 'F';//ordine MRP a fabbisogno
            }
        }
    }
}
