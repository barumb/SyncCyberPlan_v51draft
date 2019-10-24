using System.Collections.Generic;
using System;
using log4net;
using System.Data;

namespace SyncCyberPlan_lib
{
    public class OrdiniVen_as400 : OrdiniVen
    {
        public string ORRTORD;
        public decimal ORRANNO;
        public decimal ORRPROG;
        public decimal ORRNRIG;
        public string ORRCART;
        public decimal ORRQTAO;
        public decimal ORRQTAS;
        public decimal ORRDCRE; //data
        public decimal ORRDTCO; //data
        public decimal ORRDTUC; //data
        public decimal ORVDTRF; //data
        public string ORRSTAT; //stato riga
        public string ORRFLO2;
        public string ORVOPER;
        public string ORVSTAT; //stato ordine
        public string BPSNUM;

        public OrdiniVen_as400(): base()
        {
        }

        public override void Init(object[] row)
        {
            ORRTORD = getDBV<string>(row[0], "ORRTORD");
            ORRANNO = getDBV<decimal>(row[1], "ORRANNO");
            ORRPROG = getDBV<decimal>(row[2], "ORRPROG");
            ORRNRIG = getDBV<decimal>(row[3], "ORRNRIG");
            ORRCART = getDBV<string>(row[4], "ORRCART");
            ORRQTAO = getDBV<decimal>(row[5], "ORRQTAO");
            ORRQTAS = getDBV<decimal>(row[6], "ORRQTAS");
            ORRDCRE = getDBV<decimal>(row[7], "ORRDCRE");  //DATA
            ORRDTCO = getDBV<decimal>(row[8], "ORRDTCO");  //DATA
            ORRDTUC = getDBV<decimal>(row[9], "ORRDTUC");  //DATA
            ORVDTRF = getDBV<decimal>(row[10], "ORVDTRF"); //DATA
            ORRSTAT = getDBV<string>(row[11], "ORRSTAT");
            ORRFLO2 = getDBV<string>(row[12], "ORRFLO2");
            ORVOPER = getDBV<string>(row[13], "ORVOPER");
            ORVSTAT = getDBV<string>(row[14], "ORVSTAT");
            BPSNUM = getDBV<string>(row[15], "BPSNUM");



            C_CODE                                   = EscapeSQL(ORRTORD + ORRANNO.ToString("00") + ORRPROG.ToString("000000") + ORRNRIG.ToString("0000"), 30);                                              //varchar  30
            C_DESCR                                  = EscapeSQL("", 30);                            //varchar  30
            C_ITEM_CODE                              = EscapeSQL(ORRCART, 50);                       //varchar  50
            C_ITEM_PLANT                             = EscapeSQL("ITS01", 20);                       //varchar  20
            C_QTY                                    = ORRQTAO;                                      //numeric  
            C_COMPL_QTY                              = ORRQTAS;                                      //numeric  
            C_INSERT_DATE                            = dateTime_fromDataAs400(ORRDCRE);              //datetime 
            C_DUEDATE                                = dateTime_fromDataAs400(ORRDTCO);              //datetime 
            C_REQUESTED_DUEDATE                      = dateTime_fromDataAs400(ORRDTUC);              //datetime 
            C_PROMISE_DUEDATE                        = dateTime_fromDataAs400(ORRDTCO);              //datetime 
            C_ORDERED_DATE                           = dateTime_fromDataAs400(ORVDTRF);              //datetime 
            C_HOST_DUEDATE                           = null;                                         //datetime 
            C_TYPE                                   = 'E';                                          //char     1
            C_HOST_TYPE                              = EscapeSQL("", 20);                            //varchar  20
            C_STATUS                                 = ORRSTAT.ToString()[0];                        //char     1
            C_HOST_STATUS                            = EscapeSQL(ORRTORD, 15);                       //varchar  15
            C_CORDER_HEADER_CODE                     = EscapeSQL("", 30);                            //varchar  30
            C_REF_CORDER                             = EscapeSQL("", 30);                            //varchar  30
            C_CO_GROUP                               = EscapeSQL("", 10);                            //varchar  10
            C_WAREHOUSE_CODE                         = EscapeSQL(__MAGAZZINO_INTERNO, 20);           //varchar  20
            C_VALUE                                  = 0;                                            //float    
            C_COST                                   = 0;                                            //float    
            C_MANAGER                                = EscapeSQL(ORVOPER, 20);                       //varchar  20
            C_CUSTOMER_CODE                          = EscapeSQL(BPSNUM, 30);                            //varchar  30
            C_USER_INT01                             = 0;                                            //int      
            C_USER_INT02                             = 0;                                            //int      
            C_USER_INT03                             = 0;                                            //int      
            C_USER_INT04                             = 0;                                            //int      
            C_USER_INT05                             = 0;                                            //int      
            C_USER_INT06                             = 0;                                            //int      
            C_USER_INT07                             = 0;                                            //int      
            C_USER_INT08                             = 0;                                            //int      
            C_USER_INT09                             = 0;                                            //int      
            C_USER_INT10                             = 0;                                            //int      
            C_USER_REAL01                            = 0;                                            //float     
            C_USER_REAL02                            = 0;                                            //float     
            C_USER_REAL03                            = 0;                                            //float     
            C_USER_REAL04                            = 0;                                            //float     
            C_USER_REAL05                            = 0;                                            //float     
            C_USER_CHAR01                            = ' ';                                          //char     1
            C_USER_CHAR02                            = ' ';                                          //char     1
            C_USER_CHAR03                            = ' ';                                          //char     1
            C_USER_CHAR04                            = ' ';                                          //char     1
            C_USER_CHAR05                            = ' ';                                          //char     1
            C_USER_FLAG01                            = ORRFLO2 == "C"?  1 : 0;                       //bit      RIGA CONFERMATA = 1
            C_USER_FLAG02                            = 0;                                            //bit      
            C_USER_STRING01                          = EscapeSQL("", 29);                            //varchar  29
            C_USER_STRING02                          = EscapeSQL("", 29);                            //varchar  29
            C_USER_STRING03                          = EscapeSQL("", 29);                            //varchar  29
            C_USER_STRING04                          = EscapeSQL("", 29);                            //varchar  29
            C_USER_STRING05                          = EscapeSQL("", 29);                            //varchar  29
            C_USER_STRING06                          = EscapeSQL("", 29);                            //varchar  29
            C_USER_STRING07                          = EscapeSQL("", 29);                            //varchar  29
            C_USER_STRING08                          = EscapeSQL("", 29);                            //varchar  29
            C_USER_STRING09                          = EscapeSQL("", 29);                            //varchar  29
            C_USER_STRING10                          = EscapeSQL("", 29);                            //varchar  29
            C_USER_NOTE01                            = EscapeSQL("", 29);                            //varchar  99
            C_USER_COLOR01                           = 0;                                            //int        
            C_USER_COLOR02                           = 0;                                            //int        
            C_USER_DATE01                            = null;                                         //datetime   
            C_USER_DATE02                            = null;                                         //datetime   
            C_USER_DATE03                            = null;                                         //datetime   
            C_USER_DATE04                            = null;                                         //datetime   
            C_USER_DATE05                            = null;                                         //datetime  
        }
        
        public override string GetSelectQuery(bool mode, string dossier, string codice_like, string filtro)
        {
            //string libreria = "MBM41LIB_M";  //libreria = "MBM41LIBMT"  TRAC;  //libreria = "MBM41LIB_M";  
            string __libreriaAs400 = dossier;


            string _tabORR = __libreriaAs400 + ".ORR00PF";
            string _tabORV = __libreriaAs400 + ".ORV00PF";
            string _tabTAB = __libreriaAs400 + ".TAB00PF";
            

            string sage_query = "SELECT "     + "\n"
                + "  " + _tabORR + ".ORRTORD" + "\n"
                + ", " + _tabORR + ".ORRANNO" + "\n"
                + ", " + _tabORR + ".ORRPROG" + "\n"
                + ", " + _tabORR + ".ORRNRIG" + "\n"
                + ", " + _tabORR + ".ORRCART" + "\n"
                + ", " + _tabORR + ".ORRQTAO" + "\n"
                + ", " + _tabORR + ".ORRQTAS" + "\n"
                + ", " + _tabORR + ".ORRDCRE" + "\n"
                + ", " + _tabORR + ".ORRDTCO" + "\n"
                + ", " + _tabORR + ".ORRDTUC" + "\n"
                + ", " + _tabORV + ".ORVDTRF" + "\n"
                + ", " + _tabORR + ".ORRSTAT" + "\n"
                + ", " + _tabORR + ".ORRFLO2" + "\n"
                + ", " + _tabORV + ".ORVOPER" + "\n"
                + ", " + _tabORV + ".ORVSTAT" + "\n"
//                + ", " + _tabORR + ".ORRCCCO" + "\n"   //cod cliente di as400
                + ", " + _tabTAB + ".TABDESC" + "\n"   //cod cliente Sage X3
                + ", " + _tabORR + ".ORRFLO2" + "\n"

                    + " FROM " + _tabORV + "\n"
                    + " INNER JOIN " + _tabORR + " ON " + "\n"
                                  + _tabORV + ".ORVTORD = " + _tabORR + ".ORRTORD " + "\n"
                        + " AND " + _tabORV + ".ORVANNO = " + _tabORR + ".ORRANNO " + "\n"
                        + " AND " + _tabORV + ".ORVPROG = " + _tabORR + ".ORRPROG " + "\n"
                    + " INNER JOIN " + _tabTAB + " ON " + "\n"
                                  + _tabTAB + ".TABCTAB = 'CLX3' " 
                        + " and " + _tabTAB + ".TABSTAB = " + _tabORR + ".ORRCCCO "
            

            + " WHERE " + _tabORR + ".ORRSTAT ='RI' "
                      + " and " + _tabORR + ".ORRSWBL <> 'S' "             //se = 'S' la riga è bloccata e non deve girarci l'MRP
                      + " and " + _tabORV + ".ORVSTAT ='RI' "
                      //+ " and " + _tabORR + ".ORRTORD <> 'ORC' "                      
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
