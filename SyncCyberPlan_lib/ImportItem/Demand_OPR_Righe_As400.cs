using System.Collections.Generic;
using System;
using log4net;
using System.Data;

namespace SyncCyberPlan_lib
{
    public class Demand_OPR_righe_As400 : Demand_OPR_righe
    {
        public string  _MFCTORD;
        public decimal _MFCAORD;
        public decimal _MFCPORD;
        public string  _MFCCART;
        public string  _MFCCOMP;
        public decimal _MFCQTRC;
        public string  _MFCSTAT;
        public string  _MFVWKCT; // I interno E esterno (conto lavoro)
        
        public Demand_OPR_righe_As400(): base()
        {
        }

        public override string GetSelectQuery(bool mode, string dossier, string codice_like, string filtro)
        {
            string __libreriaAs400 = dossier;

            string _tabMFV = __libreriaAs400 + ".MFV00PF";
            string _tabMFC = __libreriaAs400 + ".MFC00PF";
            //TODO ???? da inserire la transcodifica Nr ordine as400->X3???
            string query = "SELECT " + "\n"
                + "   " + _tabMFC + ".MFCTORD" + "\n"
                + ",  " + _tabMFC + ".MFCAORD" + "\n"
                + ",  " + _tabMFC + ".MFCPORD" + "\n"
                + ",  " + _tabMFC + ".MFCCART" + "\n"
                + ",  " + _tabMFC + ".MFCCOMP" + "\n"
                + ",  " + _tabMFC + ".MFCQTRC" + "\n"
                + ",  " + _tabMFC + ".MFCSTAT" + "\n"
                + ",  " + _tabMFV + ".MFVWKCT" + "\n"
                + " FROM " + _tabMFC + "\n" 

                + " join " + _tabMFV + " on " 
                + "      " + _tabMFV + ".MFVTORD = " + _tabMFC + ".MFCTORD " + "\n" 
                + " and  " + _tabMFV + ".MFVAORD = " + _tabMFC + ".MFCAORD " + "\n" 
                + " and  " + _tabMFV + ".MFVPORD = " + _tabMFC + ".MFCPORD " + "\n"
                + " WHERE " + _tabMFC + ".MFCSTAT = 'RI' " + "\n"
                + " and  " + _tabMFV + ".MFVSTAT = 'RI' " + "\n"     //sempre RI testata OPR
                //+ " and MFCCOMP like  'WN0028-03' "
                ;

            if (!string.IsNullOrWhiteSpace(codice_like))
            {
                //query += " and " + _tabMFC + ".MFCCART like '" + codice_like.Trim() + "'";
            }

            query += " ORDER BY " 
                + "  " + _tabMFC + ".MFCTORD," + "\n"
                + "  " + _tabMFC + ".MFCAORD," + "\n"
                + "  " + _tabMFC + ".MFCPORD" + "\n"
                ;
            return query;
        }

        public override void Init(object[] row)
        {
            _MFCTORD = getDBV<string>(row[0], "MFCTORD");
            _MFCAORD = getDBV<decimal>(row[1], "MFCAORD");
            _MFCPORD = getDBV<decimal>(row[2], "MFCPORD");
            _MFCCART = getDBV<string>(row[3], "MFCCART");
            _MFCCOMP = getDBV<string>(row[4], "MFCCOMP");
            _MFCQTRC = getDBV<decimal>(row[5], "MFCQTRC");
            _MFCSTAT = getDBV<string>(row[6], "MFCSTAT");
            _MFVWKCT = getDBV<string>(row[7], "MFVWKCT");



            C_CORDER_CODE = EscapeSQL("", 30); 
            C_ORDER_CODE = EscapeSQL(_MFCTORD + _MFCAORD.ToString("00") + _MFCPORD.ToString("000000"),30);                   //string
            C_ITEM_CODE = EscapeSQL(_MFCCOMP, 50);                     //string 
            C_ITEM_PLANT = "ITS01";                   //string 
            C_OPNUM = 0;                         //int 
            C_NSEQ = 0;                         //int 
            C_QTY = _MFCQTRC;                     //decimal 
            C_WDW_QTY = 0;                       //decimal 
            C_M_B = ' ';                         //char 
            C_MRP_TYPE = ' ';                    //char 
            C_STATUS = 0;                        //int 
            C_REF_CORDER_CODE = "";              //string 
            C_DUEDATE = null;                    //DateTime 
            C_WAREHOUSE_CODE = __MAGAZZINO_INTERNO;       //string 
            C_USER_NOTE01 = EscapeSQL("Articolo da produrre: " +_MFCCART, 99);                  //string 
            C_USER_INT01 = 0;                    //int 
            C_USER_INT02 = 0;                    //int 
            C_USER_REAL01 = 0;                   //float 
            C_USER_REAL02 = 0;                   //float 
            C_USER_CHAR01 = ' ';                  //char 
            C_USER_CHAR02 = ' ';                  //char 
            C_USER_FLAG01 = 0;                  //bit 
            C_USER_FLAG02 = 0;                  //bit 
            C_USER_DATE01 = null;                  //DateTime 
            C_USER_DATE02 = null;                  //DateTime 
            C_USER_COLOR01 = 0;                 //int 
        }
 
    }
}
