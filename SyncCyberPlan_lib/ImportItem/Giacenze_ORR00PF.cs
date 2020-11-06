using System.Collections.Generic;
using System;
using log4net;
using System.Data;


namespace SyncCyberPlan_lib
{
    public class Giacenze_ORR00PF : Giacenze
    {
        public string  ORRTORD;
        public decimal ORRANNO;
        public decimal ORRPROG;
        public decimal ORRNRIG;
        public string  ORRCART;
        public string  ORRSTAT; //stato riga
        public decimal ORRQACA; //qta accantonata
        public string  ORVSTAT; //stato ordine


        public string X3ORDNR;  // Numero ordine + di riferimento in X3
        public int? X3ORDLN;   // Numero riga ordine di riferimento in X3
        public string X3ORDNRLN; // riferimento all'ordine di vendita (Nr ord + Riga ordine) utilizzato per gli OPR

        public Giacenze_ORR00PF(): base()
        {
        }

        public override void Init(object[] row)
        {
            ORRTORD = getDBV<string>(row[0], "ORRTORD");
            ORRANNO = (decimal)row[1];
            ORRPROG = (decimal)row[2];
            ORRNRIG = (decimal)row[3];
            ORRCART = getDBV<string>(row[4], "ORRCART");
            ORRSTAT = getDBV<string>(row[5], "ORRSTAT");
            ORRQACA = (decimal)row[6];
            ORVSTAT = getDBV<string>(row[7], "ORVSTAT");

            //20201104-it2adm>> Riferimento nrordine+nrriga di X3
            X3ORDNR = getDBV<string>(row[8], "X3ORDNR");
            X3ORDLN = getDBV<int>(row[9], "X3ORDLN");
            X3ORDNRLN = "000000000000";  //valore di default che indica ordini a fabbisogno

            if ((!String.IsNullOrEmpty(X3ORDNR)) && (X3ORDLN != null))
            {
                X3ORDNRLN = X3ORDNR + Convert.ToString(X3ORDLN).PadLeft(6, '0');
            }

            //rif ord as400
            //C_CODE = EscapeSQL(ORRTORD + ORRANNO.ToString("00") + ORRPROG.ToString("000000") + ORRNRIG.ToString("0000"), 30);                          //varchar  30
            // rif ord X3
            C_CODE = C_CORDER_CODE = EscapeSQL(X3ORDNRLN, 30);   //varchar 30  
            C_ITEM_CODE = EscapeSQL(ORRCART, 50);                         //varchar  50
            C_ITEM_PLANT = EscapeSQL("ITS01", 20);                        //varchar  20
            C_WAREHOUSE_CODE = EscapeSQL(__MAGAZZINO_INTERNO, 20);        //varchar  20
            C_DESCR = EscapeSQL("", 30);                                  //varchar  30
            C_CORDER_CODE = EscapeSQL(C_CODE, 30);                        //varchar  30
            C_QTY = ORRQACA;                                              //numeric  
            C_WDW_QTY = 0;                                                //numeric  
            C_EFFECTIVE_DATE = null;                                      //datetime 
            C_EXPIRE_DATE = null;                                         //datetime 
            C_USER_INT01 = 0;                                             //int
            C_USER_INT02 = 0;                                             //int
            C_USER_REAL01 = 0;                                            //float
            C_USER_REAL02 = 0;                                            //float
            C_USER_REAL03 = 0;                                            //float
            C_USER_STRING01 = EscapeSQL("", 29);                          //varchar  29
            C_USER_STRING02 = EscapeSQL("", 29);                          //varchar  29
            C_USER_DATE01 = null;                                         //datetime
            C_USER_DATE02 = null;                                         //datetime
            C_USER_DATE03 = null;                                         //datetime
        }        
        public override string GetSelectQuery(bool mode, string dossier, string codice_like, string filtro)
        {
            //string libreria = "MBM41LIB_M";  //libreria = "MBM41LIBMT"  TRAC;  //libreria = "MBM41LIB_M";  
            string __libreriaAs400 = dossier;


            string _tabORR = __libreriaAs400 + ".ORR00PF";
            string _tabORV = __libreriaAs400 + ".ORV00PF";

            string _tabVOO = __libreriaAs400 + ".VOOS00F";  // Tabella di transcodifica ordnine riga X3 -> Ordine riga As400

           // string sage_query = "SELECT "     + "\n"
           //     + "  " + _tabORR + ".ORRTORD" + "\n"
           //     + ", " + _tabORR + ".ORRANNO" + "\n"
           //     + ", " + _tabORR + ".ORRPROG" + "\n"
           //     + ", " + _tabORR + ".ORRNRIG" + "\n"
           //     + ", " + _tabORR + ".ORRCART" + "\n"
           //     + ", " + _tabORR + ".ORRSTAT" + "\n"
           //     + ", " + _tabORR + ".ORRQACA" + "\n"
           //     + ", " + _tabORV + ".ORVSTAT" + "\n"
           //         + " FROM " + _tabORV + "\n"
           //         + " INNER JOIN " + _tabORR + " ON " + "\n"
           //                       + _tabORV + ".ORVTORD = " + _tabORR + ".ORRTORD " + "\n"
           //             + " AND " + _tabORV + ".ORVANNO = " + _tabORR + ".ORRANNO " + "\n"
           //             + " AND " + _tabORV + ".ORVPROG = " + _tabORR + ".ORRPROG " + "\n"
           //         + " WHERE " + _tabORR + ".ORRSTAT ='RI' "
           //           //+ " and " + _tabORV + ".ORVSTAT ='RI' "
           //           //+ " and " + _tabORR + ".ORRTORD <> 'ORC' \n"
           //           + " and " + _tabORR + ".ORRQACA <> 0     \n"
           //           + " and " + _tabORR + ".ORRCART not like 'WU%'    \n"
           //         ;
           //


            string sage_query = "SELECT " + "\n"
            + "  ORR.ORRTORD" + "\n"
            + ", ORR.ORRANNO" + "\n"
            + ", ORR.ORRPROG" + "\n"
            + ", ORR.ORRNRIG" + "\n"
            + ", ORR.ORRCART" + "\n"
            + ", ORR.ORRSTAT" + "\n"
            + ", ORR.ORRQACA" + "\n"
            + ", ORV.ORVSTAT" + "\n"
            + ", trim(X3.SOHNR) AS X3ORDNR" + "\n"
            + ", int(X3.SOLNR) AS X3ORDLN" + "\n"
                + " FROM " + _tabORV + "  ORV \n"
                + " INNER JOIN " + _tabORR + " ORR ON " + "\n"
                    + "ORV.ORVTORD = ORR.ORRTORD " + "\n"
                    + " AND ORV.ORVANNO = ORR.ORRANNO " + "\n"
                    + " AND ORV.ORVPROG = ORR.ORRPROG " + "\n"
                + " LEFT JOIN " + _tabVOO + " X3 ON \n"
                + "ORR.ORRTORD = X3.TPOVE \n"
                    + "AND ORR.ORRANNO = X3.AAOVE \n"
                    + "AND ORR.ORRPROG = X3.NROVE \n"
                    + "AND ORR.ORRNRIG = X3.LNOVE \n"
                + " WHERE ORR.ORRSTAT ='RI' "
                  //+ " and ORV.ORVSTAT ='RI' "
                  //+ " and ORR.ORRTORD <> 'ORC' \n"
                  + " AND ORR.ORRQACA <> 0     \n"
                  + " AND ORR.ORRCART not like 'WU%'    \n"
                  + " AND not(X3.SOHNR is null)     \n"

                ;




            if (!string.IsNullOrWhiteSpace(codice_like))
            {
                sage_query += " and ORR.ORRCART like '" + codice_like.Trim() + "'";
            }
            sage_query +=
                      "  order by "
                      + "ORR.ORRTORD desc," + "\n"
                      + "ORR.ORRANNO desc," + "\n"
                      + "ORR.ORRPROG desc," + "\n"
                      + "ORR.ORRNRIG desc " + "\n";

            return sage_query;
        }

    }
}
