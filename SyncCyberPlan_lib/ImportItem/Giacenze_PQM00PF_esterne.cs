using System.Collections.Generic;
using System;
using log4net;
using System.Data;
using System.Data.Common;

namespace SyncCyberPlan_lib
{
    public class Giacenze_PQM00PF_esterne : Giacenze
    {
        public string  PQMCART; //Articolo                        
        public decimal PQMQGIA;  //qta giacenza
        public string PQMLOCZ;  //locazione per Esterno in As400 = fornitore
        public string PQMSUBL;  //sublocazione (vuota per esterni in as400)
        public string PQMTDLO;  //lotto
        
        //public decimal PQMTIFI; //<>'S'
        //public decimal PQMTILO; // S o I -> interno   E -> Esterno
        //public decimal PQMQGIA; //giacenza
        //PQMSUBL  sub locazione
        //PQMTDLO  lotto

       
        public Giacenze_PQM00PF_esterne(): base()
        {
        }

        public override void Init(object[] row)
        {
            PQMCART = ((string)row[0]).Trim().ToUpper();
            PQMQGIA = (decimal)row[1];
            PQMLOCZ = ((string)row[2]).Trim().ToUpper();
            PQMSUBL = ((string)row[3]).Trim().ToUpper();
            PQMTDLO = ((string)row[4]).Trim().ToUpper();

            C_CODE = EscapeSQL(PQMTDLO, 30);                //varchar  30     //LOTTO
            C_ITEM_CODE = EscapeSQL(PQMCART, 50);               //varchar  50
            C_ITEM_PLANT = EscapeSQL("ITS01", 20);              //varchar  20
            C_WAREHOUSE_CODE = EscapeSQL(PQMLOCZ, 20);      //varchar  20  //CODICE TERZO
            C_DESCR = EscapeSQL("", 30);                        //varchar  30
            C_CORDER_CODE = EscapeSQL("", 30);                  //varchar  30
            C_QTY = GetQTY(PQMCART, PQMQGIA, "E " + C_WAREHOUSE_CODE+"-"+C_CODE);  //numeric  
            C_WDW_QTY = 0;                                      //numeric  
            C_EFFECTIVE_DATE = null;                            //datetime 
            C_EXPIRE_DATE = null;                               //datetime 
            C_USER_INT01 = 0;                                   //int
            C_USER_INT02 = 0;                                   //int
            C_USER_REAL01 = 0;                                  //float
            C_USER_REAL02 = 0;                                  //float
            C_USER_REAL03 = 0;                                  //float
            C_USER_STRING01 = EscapeSQL(__MAGAZZINO_ESTERNO, 29);                //varchar  29
            C_USER_STRING02 = EscapeSQL("", 29);                //varchar  29
            C_USER_DATE01 = null;                               //datetime
            C_USER_DATE02 = null;                               //datetime
            C_USER_DATE03 = null;                               //datetime
        }
        protected decimal GetQTY(string ART, decimal PQMQGIA, string magazzino)
        {
            decimal ret;
            decimal tmp = PQMQGIA;

            if (tmp < 0)
                ret = 0;
            else
                ret = tmp;


            //gestione avvisi mail


            if (PQMQGIA < 0)
            {
                int soglia;
                string message_error = "";
//                string destinatari = "leonardo.macabri@sauro.net,cristian.scarso@sauro.net,francesco.chiminazzo@sauro.net";
//#if DEBUG
//                destinatari = "francesco.chiminazzo@sauro.net";
//#endif
                switch (ART.Substring(0,2))
                {
                    case "WP": soglia = -300; break;
                    case "WM": soglia = -300; break;
                    case "WN": soglia = -300; break;
                    default: soglia = -500; break;
                }

                if ( PQMQGIA < soglia)
                {
                    message_error = ART + " ha giacenza totale negativa (magazzino " + magazzino + "): " + PQMQGIA + Utils.NewLineMail();
                    __bulk_message +=  message_error;
                    //Utils.SendMail("it@sauro.net", destinatari, message_error);

                }                
            } 
            return ret;
        }
        public override string GetSelectQuery(bool mode, string dossier, string codice_like, string filtro)
        {
            //string libreria = "MBM41LIB_M";  //libreria = "MBM41LIBMT"  TRAC;  //libreria = "MBM41LIB_M";  
            string __libreriaAs400 = dossier;


            string _tabPQM = __libreriaAs400 + ".PQM00PF";
            /*
             * SELECT PQMCART, PQMQGIA, IIF(PQMTILO='E', 'E', 'I' ) AS LOC
            FROM[MBM41LIB_M_PQM00PF giacenze]

WHERE PQMTIFI<>'S'
order by PQMCART
*/
            string sage_query = 
                      " SELECT " + "\n"
                    + "  " + _tabPQM + ".PQMCART" + "\n"                    
                    + ", " + _tabPQM + ".PQMQGIA"  + "\n"
                    + ", " + _tabPQM + ".PQMLOCZ" + "\n"
                    + ", " + _tabPQM + ".PQMSUBL" + "\n"
                    + ", " + _tabPQM + ".PQMTDLO" + "\n"
                    + " FROM " + _tabPQM + "\n"
                    + " WHERE " + _tabPQM + ".PQMTIFI<>'S' and " + _tabPQM + ".PQMTILO='E' \n"
                    + " and " + _tabPQM + ".PQMCART not like 'WU%'    \n"
                    + " and " + _tabPQM + ".PQMCART not like 'DAI%'   \n"
                    + " and " + _tabPQM + ".PQMCART not like 'DPI%'   \n"                    
                    ;

            
            if (!string.IsNullOrWhiteSpace(codice_like))
            {
                sage_query += " and " + _tabPQM + ".PQMCART like '" + codice_like.Trim() + "'" + "\n";
            }
            // non è possibile ordinare con l'identificativo della colonna PQMCART
            // sage_query +=
            //           " order by "
            //           + _tabPQM + ".PQMCART desc " + "\n"
            //           ;
            return sage_query;
        }
        static private Dictionary<string, decimal> Init_List_AccantonamentiRigheORV(string libreria)
        {
            Dictionary<string, decimal> ret = new Dictionary<string, decimal>(1500);

            string __libreriaAs400 = libreria;
            string _tabORR = __libreriaAs400 + ".ORR00PF";
            string _tabORV = __libreriaAs400 + ".ORV00PF";

            //recupero totali accantonamenti per ogni articolo presente in ORR
            string query = "SELECT " 
                + _tabORR + ".ORRCART, Sum( " + _tabORR + ".ORRQACA) AS QTAALLOC " 
                + " FROM "  + _tabORR 
                + " WHERE " + _tabORR + ".ORRSTAT ='RI' " 
                //+ " and " + _tabORR + ".ORRTORD<>'ORC' "
                + " and " + _tabORR + ".ORRCART not like 'WU%'    \n"
                + " GROUP BY ORRCART "
                + " HAVING Sum(" + _tabORR + ".ORRQACA)<>0 "
                + " order by  ORRCART ";

            DBHelper2 db = DBHelper2.getAs400DBHelper(libreria);
            DbDataReader dtr = db.GetReaderSelectCommand(query);
            object[] row = new object[dtr.FieldCount];
            
            while (dtr.Read())
            {
                dtr.GetValues(row);
                ret.Add(Item.GetDBV<string>(row[0], "ORRCART"), Item.GetDBV<decimal>(row[1], "sum(ORRQACA)"));
            }
            return ret;
        }

        public override void LastAction(ref DBHelper2 cm, DBHelper2 sage)
        {
            Utils.SendMail_Plan(Settings.GetSettings(), __bulk_message,  sage.LibreriaDossier.ToUpper() + "=>" +"giacenze As400 esterne");
        }

    }
}
