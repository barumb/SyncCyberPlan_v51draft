using System.Collections.Generic;
using System;
using log4net;
using System.Data;
using System.Data.Common;

namespace SyncCyberPlan_lib
{
    public class DistintaBase_As400 : DistintaBase
    {
        static List<string> __lista_Articoli_RI_Sage = null;


        public string  SPRCART;
        public string  SPRCOMP;
        public string  SPRSEQU;
        public char    SPRFLPH;  //  S/N
        public decimal SPRQIMP;
        public string  SPRSTAT;

        
        public DistintaBase_As400(): base()
        {
        }

        public override void Init(object[] row)
        {
            SPRCART = getDBV<string> (row[0], "SPRCART");
            SPRCOMP = getDBV<string> (row[1], "SPRCOMP");
            SPRSEQU = getDBV<string> (row[2], "SPRSEQU");
            SPRFLPH = getDBV<string> (row[3], "SPRFLPH")[0];
            SPRQIMP = getDBV<decimal>(row[4], "SPRQIMP");
            SPRSTAT = getDBV<string> (row[5], "SPRSTAT");

            int tmp;
            if (!int.TryParse(SPRSEQU, out tmp))
            {
                SPRCART += " errore sequenza";
                SPRSEQU = "0";
            }



            C_BOM_CODE              = EscapeSQL(SPRCART, 30);   // varchar     30
            C_BOM_ALT               = EscapeSQL("ITS01", 20);   // varchar     20
            C_COMPONENT_CODE        = EscapeSQL(SPRCOMP, 50);   // varchar     50
            C_COMPONENT_PLANT       = EscapeSQL("ITS01", 20);   // varchar     20
            C_OPNUM                 = 0;                        // int           
            C_NSEQ                  = int.Parse(SPRSEQU);       // int           
            C_PHANTOM               = SPRFLPH == 'N' ? 0 : 1;   // bit           
            C_VAR_QTY               = (double)SPRQIMP;          // real           
            C_SCRAP_TYPE            = ' ';                      // char        1
            C_PCT_SCRAP             = 0;                        // real           
            C_WAREHOUSE_CODE        = __MAGAZZINO_INTERNO;      // varchar     20
            C_EFFECTIVE_DATE        = null;                     // datetime           
            C_EXPIRE_DATE           = null;                     // datetime           
            C_USER_STRING01         = "";                       // varchar     30
            C_USER_INT01            = 0;                        // int           
            C_USER_INT02            = 0;                        // int           
            C_USER_CHAR01           = ' ';                      // char        1
            C_USER_CHAR02           = ' ';                      // char        1
        }
        public override DataRow GetCyberRow()
        {
            if (!__lista_Articoli_RI_Sage.Contains(SPRCART))
            {
                //l'articolo non è rilasciato in sage
                //non esporto la distinta base
                return null;
            }
            //NON TOGLIERE ci sno più righe in distinta base con lo stesso articolo
            //__lista_Articoli_RI_Sage.Remove(SPRCART);
            

            return base.GetCyberRow();
        }
        public override string GetSelectQuery(bool mode, string libreria, string codice_like, string filtro)
        {
            if (__lista_Articoli_RI_Sage == null)
            {
                __lista_Articoli_RI_Sage = Get_Lista_Articoli_RI_Sage(mode, "SAURO", codice_like);
            }


            //string libreria = "MBM41LIB_M";  //libreria = "MBM41LIBMT"  TRAC;  //libreria = "MBM41LIB_M";  
            string __libreriaAs400 = libreria;


            string _tabSPR = __libreriaAs400 + ".SPR00PF";            

            string sage_query = "SELECT "     + "\n"
                + "  " + _tabSPR + ".SPRCART" + "\n"
                + ", " + _tabSPR + ".SPRCOMP" + "\n"
                + ", " + _tabSPR + ".SPRSEQU" + "\n"
                + ", " + _tabSPR + ".SPRFLPH" + "\n"
                + ", " + _tabSPR + ".SPRQIMP" + "\n"
                + ", " + _tabSPR + ".SPRSTAT" + "\n"
                    + " FROM " + _tabSPR + "\n"
                    + " WHERE " + _tabSPR + ".SPRSTAT ='RI' "
                    ;
            
            if (!string.IsNullOrWhiteSpace(codice_like))
            {
                sage_query += " and " + _tabSPR + ".SPRCART like '" + codice_like.Trim() + "'";
            }
            if (false)
            {
                string tmp_day = System.DateTime.Now.AddDays(-Item.NUMLASTDAYS).ToString("yyyyMMdd");
                //recupero solo i dati cambiati gli ultimi 3gg
                //return ret + " AND MBM41LIB_M.PFHD00F.UPDDT >= " + System.DateTime.Now.AddDays(-3).ToString("yyyyMMdd");
                sage_query += " AND (" 
                             + _tabSPR + ".SPRDAGG >= " + tmp_day
                    + " OR " + _tabSPR + ".SPRDCRE >= " + tmp_day
                    //+ " OR " + _tabSPR + ".SPRDTIN >= " + tmp_day
                    //+ " OR " + _tabSPR + ".SPRDTDL >= " + tmp_day
                    + ")";
            }
            sage_query +=
                      " order by "
                      + _tabSPR + ".SPRCART desc" + "\n";

            return sage_query;
        }
                
        static private List<string> Get_Lista_Articoli_RI_Sage(bool mode, string dossier, string codice_like)
        {
            List<string> _lista_articoli_rilasciati_in_sage = new List<string>(30000);

            //recupero totali accantonamenti per ogni articolo presente in ORR
            string query = Articolo.SelectQuery(true, dossier, codice_like, null);

            DBHelper2 db = DBHelper2.getSageDBHelper(dossier);
            DbDataReader dtr = db.GetReaderSelectCommand(query);
            object[] row = new object[dtr.FieldCount];

            while (dtr.Read())
            {
                dtr.GetValues(row);
                _lista_articoli_rilasciati_in_sage.Add(Item.GetDBV<string>(row[0],"articolo"));
            }
            return _lista_articoli_rilasciati_in_sage;
        }
    }
}
