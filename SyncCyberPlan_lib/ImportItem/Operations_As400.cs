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
    public class Operations_As400 : Operations
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

        //20201113
        public string X3ORDNR;  // Numero ordine + di riferimento in X3
        public int? X3ORDLN;   // Numero riga ordine di riferimento in X3

        public string X3ORDNRLN; // riferimento all'ordine di vendita (Nr ord + Riga ordine) utilizzato per gli OPR



        public Operations_As400(): base()
        {
        }

        public override void Init(object[] row)
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
            MFVMACN = getDBV<string>(row[16], "MFVMACN");
            MFVCSTM = getDBV<string>(row[17], "MFVCSTM");
            MFVWRKC = getDBV<string>(row[18], "MFVWRKC");
            MFVWKCT = getDBV<string>(row[19], "MFVWKCT");

            MFVUTLM = getDBV<string>(row[20], "MFVUTLM");   //unita di misura tempo per un pezzo/via  1=ORE  2=100MI-HR   3 Minuti 4 giorni  5 settimane
            MFVAMPT = getDBV<decimal>(row[21], "MFVAMPT");  //tempo per un pezzo/via
            MFVUTSE = getDBV<string>(row[22], "MFVUTSE");   //unita di misura tempo di setup
            MFVASET = getDBV<decimal>(row[23], "MFVASET");  //tempo di setup

            FLVPZ = getDBV<string>(row[24], "FLVPZ");  //flag vie pezzi dell'attrezzatura

            NRVIE = (int)getDBV<decimal>(row[25], "NRVIE");     //numero vie totali dell'articolo


            C_ORDER_CODE              = EscapeSQL(MFHTORD + MFHAORD.ToString("00") + MFHPORD.ToString("000000"), 30);
            // debug
            if ((MFHPORD== 62457) || (MFHPORD == 62418));
            {
                int cccc = 0;
            }
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




        public override string GetSelectQuery(bool mode, string dossier, string codice_like, string filtro)
        {
            string __libreriaAs400 = dossier;

            string _tabMFH = __libreriaAs400 + ".MFH00PF";
            string _tabMFV = __libreriaAs400 + ".MFV00PF";

            string _tabRSH = __libreriaAs400 + ".RSHD00F";
            string _tabPFH = __libreriaAs400 + ".PFHD00F";

            //   string query = "SELECT " + "\n"
            //       + "  " + _tabMFH + ".MFHTORD" + "\n"
            //       + ",  " + _tabMFH + ".MFHAORD" + "\n"
            //       + ",  " + _tabMFH + ".MFHPORD" + "\n"
            //       + ",  " + _tabMFH + ".MFHTCOM" + "\n"
            //       + ",  " + _tabMFH + ".MFHACOM" + "\n"
            //       + ",  " + _tabMFH + ".MFHPCOM" + "\n"
            //       + ",  " + _tabMFH + ".MFHSCOM" + "\n"
            //       + ",  " + _tabMFH + ".MFHCART" + "\n"
            //       + ",  " + _tabMFH + ".MFHQTRC" + "\n"
            //       + ",  " + _tabMFH + ".MFHDCRE" + "\n"
            //       + ",  " + _tabMFH + ".MFHSTAT" + "\n"
            //       + ",  " + _tabMFV + ".MFVDINI" + "\n"
            //       + ",  " + _tabMFV + ".MFVDEND" + "\n"
            //       + ",  " + _tabMFV + ".MFVSTAV" + "\n"
            //       + ",  " + _tabMFH + ".MFHQTPR" + "\n"
            //       + ",  " + _tabMFV + ".MFVQTSC" + "\n"
            //       + ",  " + _tabMFV + ".MFVMACN" + "\n"
            //       + ",  " + _tabMFV + ".MFVCSTM" + "\n"
            //       + ",  " + _tabMFV + ".MFVWRKC" + "\n"
            //       + ",  " + _tabMFV + ".MFVWKCT" + "\n"
            //
            //       + ",  " + _tabMFV + ".MFVUTLM" + "\n"
            //       + ",  " + _tabMFV + ".MFVAMPT" + "\n"
            //       + ",  " + _tabMFV + ".MFVUTSE" + "\n"
            //       + ",  " + _tabMFV + ".MFVASET" + "\n"
            //
            //       + ",  " + _tabRSH + ".FLVPZ" + "\n"
            //
            //       + ",  " + _tabPFH + ".NRVIE" + "\n"
            //
            //       + " FROM " + _tabMFH + "\n"
            //       + " INNER JOIN " + _tabMFV + " ON " + "\n"
            //                        + _tabMFH + ".MFHTORD = " + _tabMFV + ".MFVTORD " + "\n"
            //              + " AND " + _tabMFH + ".MFHAORD = " + _tabMFV + ".MFVAORD " + "\n"
            //              + " AND " + _tabMFH + ".MFHPORD = " + _tabMFV + ".MFVPORD " + "\n"
            //       + " INNER JOIN " + _tabRSH + " ON " + "\n"
            //                        + _tabRSH + ".CDSTM = " + _tabMFV + ".MFVCSTM " + "\n"
            //       + " INNER JOIN " + _tabPFH + " ON " + "\n"
            //                        + _tabPFH + ".CDART = " + _tabMFH + ".MFHCART " + "\n"
            //
            //       + " WHERE " + _tabMFH + ".MFHSTAT = 'RI' " + "\n"     //sempre RI
            //       + " and   " + _tabMFV + ".MFVSTAT = 'RI' " + "\n"     //sempre RI
            //       + " and   " + _tabMFV + ".MFVSTAV <> 'CH' " + "\n"    //questo indica se la riga è chiusa
            //       ;
            //
            //   if (!string.IsNullOrWhiteSpace(codice_like))
            //   {
            //       query += " and " + _tabMFH + ".MFHCART like '" + codice_like.Trim() + "'";
            //   }
            //
            //   query += " ORDER BY "
            //       + "  " + _tabMFH + ".MFHTORD," + "\n"
            //       + "  " + _tabMFH + ".MFHAORD," + "\n"
            //       + "  " + _tabMFH + ".MFHPORD" + "\n"
            //       ;


            // Versione con i riferimenti X3 dell'ordine nelle operation 

            // string query = "SELECT " + "\n"
            //    + "   MFG.MFHTORD" + "\n"
            //    + ",  MFG.MFHAORD" + "\n"
            //    + ",  MFG.MFHPORD" + "\n"
            //    + ",  MFG.MFHTCOM" + "\n"
            //    + ",  MFG.MFHACOM" + "\n"
            //    + ",  MFG.MFHPCOM" + "\n"
            //    + ",  MFG.MFHSCOM" + "\n"
            //    + ",  MFG.MFHCART" + "\n"
            //    + ",  MFG.MFHQTRC" + "\n"
            //    + ",  MFG.MFHDCRE" + "\n"
            //    + ",  MFG.MFHSTAT" + "\n"
            //    + ",  MFV.MFVDINI" + "\n"
            //    + ",  MFV.MFVDEND" + "\n"
            //    + ",  MFV.MFVSTAV" + "\n"
            //    + ",  MFH.MFHQTPR" + "\n"
            //    + ",  MFV.MFVQTSC" + "\n"
            //    + ",  MFV.MFVMACN" + "\n"
            //    + ",  MFV.MFVCSTM" + "\n"
            //    + ",  MFV.MFVWRKC" + "\n"
            //    + ",  MFV.MFVWKCT" + "\n"
            //    + ",  MFV.MFVUTLM" + "\n"
            //    + ",  MFV.MFVAMPT" + "\n"
            //    + ",  MFV.MFVUTSE" + "\n"
            //    + ",  MFV.MFVASET" + "\n"
            //    
            //    + ",  " + _tabRSH + "RSH.FLVPZ" + "\n"
            //    
            //    + ",  " + _tabPFH + "PFH.NRVIE" + "\n"
            //    
            //    + " FROM " + _tabMFH + " MFH \n"
            //    + " INNER JOIN " + _tabMFV + " MFV ON " + "\n"
            //           + "     MFH.MFHTORD = MFV.MFVTORD " + "\n"
            //           + " AND MFH.MFHAORD = MFV.MFVAORD " + "\n"
            //           + " AND MFH.MFHPORD = MFV.MFVPORD " + "\n"
            //    + " INNER JOIN " + _tabRSH + " RSH ON " + "\n"
            //                     + "RSH.CDSTM = MFV.MFVCSTM " + "\n"
            //    + " INNER JOIN " + _tabPFH + " PFH ON " + "\n"
            //                     + "PFH.CDART = MFH.MFHCART " + "\n"
            //    
            //    + " WHERE " 
            //    +        "MFH.MFHSTAT = 'RI' " + "\n"     //sempre RI
            //    + " and   MFV.MFVSTAT = 'RI' " + "\n"     //sempre RI
            //    + " and   MFV.MFVSTAV <> 'CH' " + "\n"    //questo indica se la riga è chiusa
            //    ;
            //
            //    if (!string.IsNullOrWhiteSpace(codice_like))
            //    {
            //        query += " and MFH.MFHCART like '" + codice_like.Trim() + "'";
            //    }
            //
            //    query += " ORDER BY "
            //        + "  MFH.MFHTORD," + "\n"
            //        + "  MFH.MFHAORD," + "\n"
            //        + "  MFH.MFHPORD" + "\n"
            //        ;


            // Versione con i riferimenti X3 dell'ordine nelle operation e con codice attrezz X3. Si assumene che codice macchina AS e X3 COINCIDANO!!!!!!!!


            //  versione NON parametrica
            //string query = " SELECT          "
            //            + "    MFH.MFHTORD "
            //            + "   ,MFH.MFHAORD "
            //            + "   ,MFH.MFHPORD "
            //            + "   ,MFH.MFHTCOM "
            //            + "   ,MFH.MFHACOM "
            //            + "   ,MFH.MFHPCOM "
            //            + "   ,MFH.MFHSCOM "
            //            + "   ,MFH.MFHCART "
            //            + "   ,MFH.MFHQTRC "
            //            + "   ,MFH.MFHDCRE "
            //            + "   ,MFH.MFHSTAT "
            //            + "   ,MFV.MFVDINI "
            //            + "   ,MFV.MFVDEND "
            //            + "   ,MFV.MFVSTAV "
            //            + "   ,MFH.MFHQTPR "
            //            + "   ,MFV.MFVQTSC "
            //            //+ "   --,MFV.MFVMACN-- Macchina As400 da Transcodifcare in X3 \n"
            //            + "   ,ITF.YPRDMAC_0 AS MFVMACN "
            //            //+ "   --,MFV.MFVCSTM-- Attrezz As400 da Transcodifcare in X3 \n"
            //            + "   ,ITF.YPRDSTA_0 AS MFVCSTM "
            //            + "   ,MFV.MFVWRKC   "
            //            + "   ,MFV.MFVWKCT   "
            //            + "   ,MFV.MFVUTLM   "
            //            + "   ,MFV.MFVAMPT   "
            //            + "   ,MFV.MFVUTSE   "
            //            + "   ,MFV.MFVASET   "
            //            + "   ,RSH.FLVPZ     "
            //            + "   ,PFH.NRVIE     "
            //            + " FROM [P8DATA].[S21C986V].MBM41LIB_M.MFH00PF MFH \n"
            //            + "   INNER JOIN [P8DATA].[S21C986V].[MBM41LIB_M].MFV00PF MFV ON \n"
            //            + "                    MFH.MFHTORD = MFV.MFVTORD              \n"
            //            + "                AND MFH.MFHAORD = MFV.MFVAORD              \n"
            //            + "                AND MFH.MFHPORD = MFV.MFVPORD              \n"
            //            + "   INNER JOIN [P8DATA].[S21C986V].MBM41LIB_M.RSHD00F  RSH ON \n"
            //            + "                    RSH.CDSTM = MFV.MFVCSTM                \n"
            //            + "   INNER JOIN [P8DATA].[S21C986V].MBM41LIB_M.PFHD00F PFH ON \n"
            //            + "                    PFH.CDART = MFH.MFHCART                \n"
            //            + "   INNER JOIN x3.SAURO.YPRDITM IPR  on                        \n"
            //            + "                    PFH.CDART = IPR.ITMREF_0               \n"
            //            + "   INNER JOIN x3.SAURO.YPRDATT ATX3 on                        \n"
            //            + "                    IPR.YATTCOD_0 = ATX3.YATTCOD_0         \n"
            //            + "                AND ATX3.YATTENAFLG_0 = 2                  \n"
            //            + "   INNER JOIN x3.SAURO.ITMFACILIT ITF on                      \n"
            //            + "                    IPR.ITMREF_0 = ITF.ITMREF_0            \n"
            //            + "                AND ITF.STOFCY_0 = 'ITS01'                 \n"
            //            + " WHERE                                                     \n"
            //            + "       MFH.MFHSTAT =  'RI'                                 \n"   //--sempre RI
            //            + "   and MFV.MFVSTAT =  'RI'                                 \n"   //--sempre RI
            //            + "   and MFV.MFVSTAV <> 'CH'                                 \n"   //--questo indica se la riga è chiusa
            //            + " ";
            //
            //if (!string.IsNullOrWhiteSpace(codice_like))
            //{
            //    query += " and MFH.MFHCART like '" + codice_like.Trim() + "'";
            //}
            //
            //
            //query += " ORDER BY               "
            //            + "    MFH.MFHTORD    "
            //            + "   ,MFH.MFHAORD    "
            //            + "   ,MFH.MFHPORD    "
            //            + " ";
            //



            //  versione di DEBUG
            #region OBSOLEQRY
         //   string query = " SELECT  DISTINCT        "
         //               + "    MFH.MFHTORD "
         //               + "   ,MFH.MFHAORD "
         //               + "   ,MFH.MFHPORD "
         //               + "   ,MFH.MFHTCOM "
         //               + "   ,MFH.MFHACOM "
         //               + "   ,MFH.MFHPCOM "
         //               + "   ,MFH.MFHSCOM "
         //               + "   ,MFH.MFHCART "
         //               + "   ,MFH.MFHQTRC "
         //               + "   ,MFH.MFHDCRE "
         //               + "   ,MFH.MFHSTAT "
         //               + "   ,MFV.MFVDINI "
         //               + "   ,MFV.MFVDEND "
         //               + "   ,MFV.MFVSTAV "
         //               + "   ,MFH.MFHQTPR "
         //               + "   ,MFV.MFVQTSC "
         //               //+ "   ,MFV.MFVMACN -- Macchina As400 da Transcodifcare in X3 \n"
         //               + "   ,ITF.YPRDMAC_0 AS MFVMACN "
         //               //+ "   ,MFV.MFVCSTM  -- Attrezz As400 da Transcodifcare in X3 \n"
         //               + "   ,IIF(left(MFH.MFHCART,2) <>'WP',IPR.YATTCOD_0 ,left(MFV.MFVCSTM,6)) AS MFVCSTM "
         //               + "   ,MFV.MFVWRKC   "
         //               + "   ,MFV.MFVWKCT   "
         //               + "   ,MFV.MFVUTLM   "
         //               + "   ,MFV.MFVAMPT   "
         //               + "   ,MFV.MFVUTSE   "
         //               + "   ,MFV.MFVASET   "
         //               + "   ,RSH.FLVPZ     "
         //               + "   ,PFH.NRVIE     "
         //               + " FROM [P8DATA].[S21C986V].MBM41LIB_M.MFH00PF MFH \n"
         //               + "   INNER JOIN [P8DATA].[S21C986V].[MBM41LIB_M].MFV00PF MFV ON \n"
         //               + "                    MFH.MFHTORD = MFV.MFVTORD                 \n"
         //               + "                AND MFH.MFHAORD = MFV.MFVAORD                 \n"
         //               + "                AND MFH.MFHPORD = MFV.MFVPORD                 \n"
         //               + "   INNER JOIN [P8DATA].[S21C986V].MBM41LIB_M.RSHD00F  RSH ON  \n"
         //               + "                    RSH.CDSTM = MFV.MFVCSTM                   \n"
         //               + "   INNER JOIN [P8DATA].[S21C986V].MBM41LIB_M.PFHD00F PFH ON   \n"
         //               + "                    PFH.CDART = MFH.MFHCART                   \n"
         //               + "   INNER JOIN x3.SAURO.YPRDITM IPR  on                        \n"
         //               + "                    PFH.CDART = IPR.ITMREF_0                  \n"
         //               + "                    AND IPR.YENAFLG_0=2                       \n"      // 1 articolo (TRANNE WP) ha 1 sola attrezzatura attiva !!!! 
         //               + "   INNER JOIN x3.SAURO.YPRDATT ATX3 on                        \n"
         //               + "                    IPR.YATTCOD_0 = ATX3.YATTCOD_0            \n"
         //               + "                AND ATX3.YATTENAFLG_0 = 2                     \n"
         //               + "   INNER JOIN x3.SAURO.ITMFACILIT ITF on                      \n"
         //               + "                    IPR.ITMREF_0 = ITF.ITMREF_0               \n"
         //               + "                AND ITF.STOFCY_0 = 'ITS01'                    \n"
         //               + " WHERE                                                        \n"
         //               + "       MFH.MFHSTAT =  'RI'                                    \n"   //--sempre RI
         //               + "   and MFV.MFVSTAT =  'RI'                                    \n"   //--sempre RI
         //               + "   and MFV.MFVSTAV <> 'CH'                                    \n"   //--questo indica se la riga è chiusa
         //               + " ";
            #endregion

            #region FIX_QRYOPR
            string query = "SELECT  DISTINCT       "
                         + " 	 MFH.MFHTORD    "
                         + " 	,MFH.MFHAORD    "
                         + " 	,MFH.MFHPORD    "
                         + " 	,MFH.MFHTCOM    "
                         + " 	,MFH.MFHACOM    "
                         + " 	,MFH.MFHPCOM    "
                         + " 	,MFH.MFHSCOM    "
                         + " 	,MFH.MFHCART    "
                         + " 	,MFH.MFHQTRC    "
                         + " 	,MFH.MFHDCRE    "
                         + " 	,MFH.MFHSTAT    "
                         + " 	,MFV.MFVDINI    "
                         + " 	,MFV.MFVDEND    "
                         + " 	,MFV.MFVSTAV    "
                         + " 	,MFH.MFHQTPR    "
                         + " 	,MFV.MFVQTSC    "
                         /*
                         + " 	--,ITF.YPRDMAC_0 AS MFVMACN   "
                         + " 	--,IIF(left(MFH.MFHCART,2) <>'WP',IPR.YATTCOD_0 ,left(MFV.MFVCSTM,6)) AS MFVCSTM  "
                         + " 	-- sostituite da                                                                  "
                         */
                         + " 	,IIF(left(MFH.MFHCART,2) <>'WP',ITF.YPRDMAC_0 ,left(MFR.MFRMACN,6)) AS MFVMACN    "
                         + " 	,IIF(left(MFH.MFHCART,2) <>'WP',IPR.YATTCOD_0 ,left(MFR.MFRCSTM,6)) AS MFVCSTM    "
                         + " 	,MFV.MFVWRKC  "
                         + " 	,MFV.MFVWKCT  "
                         + " 	,MFV.MFVUTLM  "
                         + " 	,MFV.MFVAMPT  "
                         + " 	,MFV.MFVUTSE  "
                         + " 	,MFV.MFVASET  "
                         + " 	,RSH.FLVPZ    "
                         + " 	,PFH.NRVIE    \n"
                         + " FROM [P8DATA].[S21C986V].MBM41LIB_M.MFH00PF MFH                   \n"
                         + "    INNER JOIN [P8DATA].[S21C986V].[MBM41LIB_M].MFV00PF MFV ON     \n"
                         + "                     MFH.MFHTORD = MFV.MFVTORD                     \n"
                         + "                 AND MFH.MFHAORD = MFV.MFVAORD                     \n"
                         + "                 AND MFH.MFHPORD = MFV.MFVPORD                     \n"
                         + "    INNER JOIN  [P8DATA].[S21C986V].MBM41LIB_M.MFR00PF MFR ON      \n"
                         + "                     MFH.MFHTORD = MFR.MFRTORD                     \n"
                         + "                 AND MFH.MFHAORD = MFR.MFRAORD                     \n"
                         + "                 AND MFH.MFHPORD = MFR.MFRPORD                     \n"
                         + "    INNER JOIN [P8DATA].[S21C986V].MBM41LIB_M.RSHD00F  RSH ON      \n"
                         + "                     RSH.CDSTM = MFV.MFVCSTM                       \n"
                         + "    INNER JOIN [P8DATA].[S21C986V].MBM41LIB_M.PFHD00F PFH ON       \n"
                         + "                     PFH.CDART = MFH.MFHCART                       \n"
                         + "    INNER JOIN x3.SAURO.YPRDITM IPR  on                            \n"
                         + "                     PFH.CDART = IPR.ITMREF_0                      \n"
                         + "                     AND IPR.YENAFLG_0=2                           \n"
                         + "    INNER JOIN x3.SAURO.YPRDATT ATX3 on                            \n"
                         + "                     IPR.YATTCOD_0 = ATX3.YATTCOD_0                \n"
                         + "                 AND ATX3.YATTENAFLG_0 = 2                         \n"
                         + "    INNER JOIN x3.SAURO.ITMFACILIT ITF on                          \n"
                         + "                     IPR.ITMREF_0 = ITF.ITMREF_0                   \n"
                         + "                 AND ITF.STOFCY_0 = 'ITS01'                        \n"
                         + "  WHERE                                                            \n"
                         + "        MFH.MFHSTAT =  'RI'                                        \n"
                         + "    and MFV.MFVSTAT =  'RI'                                        \n"
                         + "    and MFV.MFVSTAV <> 'CH'                                        \n"
                         + " ";
            #endregion

            if (!string.IsNullOrWhiteSpace(codice_like))
            {
                query += " and MFH.MFHCART like '" + codice_like.Trim() + "'";
            }
            
            // SELEZIONE degli 
            
            query += " ORDER BY               "
                        + "    MFH.MFHTORD    "
                        + "   ,MFH.MFHAORD    "
                        + "   ,MFH.MFHPORD    "
                        + " ";

            return query;
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
                    __bulk_message += commento + Utils.NewLineMail() + " ha unità tempo pari a <2>"+ Utils.NewLineMail();
                    break;
                case "3": ret = tmp_tempo ; break;           //3 = minuti
                case "4": ret = tmp_tempo * 60*24; break;    //4 = giorni
                case "5": ret = tmp_tempo * 60*24*7; break;  //5 = settimane
                default:
                    __bulk_message += commento + Utils.NewLineMail() + " ha unità di misura non prevista: < " + unita_di_misura + " >" + Utils.NewLineMail();
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
                //se l'attrezzatura va a vie moltiplico per il numero di vie
                tmp_tempo = tmp_tempo * NumVieTotali;
            }
            return getSetupTime(commento, unita_di_misura, tmp_tempo);
        }
    }
}
