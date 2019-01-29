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


        public Giacenze_ORR00PF(): base()
        {
        }

        public override void Init(object[] row)
        {
            ORRTORD = getDBV<string>(row[0]);
            ORRANNO = (decimal)row[1];
            ORRPROG = (decimal)row[2];
            ORRNRIG = (decimal)row[3];
            ORRCART = getDBV<string>(row[4]);
            ORRSTAT = getDBV<string>(row[5]);
            ORRQACA = (decimal)row[6];
            ORVSTAT = getDBV<string>(row[7]);


            C_CODE = EscapeSQL(ORRTORD + ORRANNO.ToString("00") + ORRPROG.ToString("000000") + ORRNRIG.ToString("0000"), 30);                          //varchar  30
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

            string sage_query = "SELECT "     + "\n"
                + "  " + _tabORR + ".ORRTORD" + "\n"
                + ", " + _tabORR + ".ORRANNO" + "\n"
                + ", " + _tabORR + ".ORRPROG" + "\n"
                + ", " + _tabORR + ".ORRNRIG" + "\n"
                + ", " + _tabORR + ".ORRCART" + "\n"
                + ", " + _tabORR + ".ORRSTAT" + "\n"
                + ", " + _tabORR + ".ORRQACA" + "\n"
                + ", " + _tabORV + ".ORVSTAT" + "\n"
                    + " FROM " + _tabORV + "\n"
                    + " INNER JOIN " + _tabORR + " ON " + "\n"
                                  + _tabORV + ".ORVTORD = " + _tabORR + ".ORRTORD " + "\n"
                        + " AND " + _tabORV + ".ORVANNO = " + _tabORR + ".ORRANNO " + "\n"
                        + " AND " + _tabORV + ".ORVPROG = " + _tabORR + ".ORRPROG " + "\n"
                    + " WHERE " + _tabORR + ".ORRSTAT ='RI' "
                      //+ " and " + _tabORV + ".ORVSTAT ='RI' "
                      //+ " and " + _tabORR + ".ORRTORD <> 'ORC' \n"
                      + " and " + _tabORR + ".ORRQACA <> 0     \n"
                      + " and " + _tabORR + ".ORRCART not like 'WU%'    \n"
                    ;
            
            if (!string.IsNullOrWhiteSpace(codice_like))
            {
                sage_query += " and " + _tabORR + ".ORRCART like '" + codice_like.Trim() + "'";
            }
            sage_query +=
                      " order by "
                      + _tabORR + ".ORRTORD desc," + "\n"
                      + _tabORR + ".ORRANNO desc," + "\n"
                      + _tabORR + ".ORRPROG desc," + "\n"
                      + _tabORR + ".ORRNRIG desc " + "\n";

            return sage_query;
        }

    }
}
