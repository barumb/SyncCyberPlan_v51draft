using System.Collections.Generic;
using System;
using log4net;
using System.Data;
using System.Data.Common;

namespace SyncCyberPlan_lib
{
    public class DistintaBase : Item
    {
        static List<string> __lista_Articoli_RI_Sage = null;


        public string  SPRCART;
        public string  SPRCOMP;
        public string  SPRSEQU;
        public char    SPRFLPH;  //  S/N
        public decimal SPRQIMP;
        public string  SPRSTAT;


        #region tabella output CYB_COMPONENT
        public string C_BOM_CODE;              // varchar     30
        public string C_BOM_ALT;               // varchar     20
        public string C_COMPONENT_CODE;        // varchar     50
        public string C_COMPONENT_PLANT;       // varchar     20
        public int C_OPNUM;                    // int           
        public int C_NSEQ;                     // int           
        public int C_PHANTOM;                  // bit           
        public double C_VAR_QTY;               // real           
        public char C_SCRAP_TYPE;              // char        1
        public float C_PCT_SCRAP;              // real           
        public string C_WAREHOUSE_CODE;        // varchar     20
        public DateTime? C_EFFECTIVE_DATE;     // datetime           
        public DateTime? C_EXPIRE_DATE;        // datetime           
        public string C_USER_STRING01;         // varchar     30
        public int C_USER_INT01;               // int           
        public int C_USER_INT02;               // int           
        public char C_USER_CHAR01;             // char        1
        public char C_USER_CHAR02;             // char        1

        #endregion


        public DistintaBase(): base("CYB_COMPONENT")
        {
        }

        public override void Init(object[] row)
        {
            SPRCART = getDBV<string>(row[0]);
            SPRCOMP = getDBV<string>(row[1]);
            SPRSEQU = getDBV<string>(row[2]);
            SPRFLPH = getDBV<string>(row[3])[0];
            SPRQIMP = getDBV<decimal>(row[4]);
            SPRSTAT = getDBV<string>(row[5]);

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
            C_WAREHOUSE_CODE        = "";                       // varchar     20
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


            DataRow _tablerow = _dataTable.NewRow();


            _tablerow[0]  = C_BOM_CODE       ;                   //C_BOM_CODE varchar     30
            _tablerow[1]  = C_BOM_ALT        ;                   //C_BOM_ALT varchar     20
            _tablerow[2]  = C_COMPONENT_CODE ;                   //C_COMPONENT_CODE varchar     50
            _tablerow[3]  = C_COMPONENT_PLANT;                   //C_COMPONENT_PLANT varchar     20
            _tablerow[4]  = C_OPNUM          ;                   //C_OPNUM int           
            _tablerow[5]  = C_NSEQ           ;                   //C_NSEQ int           
            _tablerow[6]  = C_PHANTOM        ;                   //C_PHANTOM bit           
            _tablerow[7]  = C_VAR_QTY        ;                   //C_VAR_QTY real           
            _tablerow[8]  = C_SCRAP_TYPE     ;                   //C_SCRAP_TYPE char        1
            _tablerow[9]  = C_PCT_SCRAP      ;                   //C_PCT_SCRAP real           
            _tablerow[10] = C_WAREHOUSE_CODE ;                   //C_WAREHOUSE_CODE varchar     20
            _tablerow[11] = DateTime_toCyb( C_EFFECTIVE_DATE);   //C_EFFECTIVE_DATE datetime           
            _tablerow[12] = DateTime_toCyb(C_EXPIRE_DATE);       //C_EXPIRE_DATE datetime           
            _tablerow[13] = C_USER_STRING01  ;                   //C_USER_STRING01 varchar     30
            _tablerow[14] = C_USER_INT01     ;                   //C_USER_INT01 int           
            _tablerow[15] = C_USER_INT02     ;                   //C_USER_INT02 int           
            _tablerow[16] = C_USER_CHAR01    ;                   //C_USER_CHAR01 char        1
            _tablerow[17] = C_USER_CHAR02    ;                   //C_USER_CHAR02 char        1 

            return _tablerow;
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
        public override string GetID()
        {
            return C_BOM_CODE.PadRight(30) + "   " + C_BOM_ALT + "  " + C_COMPONENT_CODE + "  " + C_OPNUM + "  " + C_NSEQ;
        }

        public override void InitDataTable()
        {
            _dataTable.Columns.Add("C_BOM_CODE",              typeof(string));                 // varchar 30
            _dataTable.Columns.Add("C_BOM_ALT",               typeof(string));                 // varchar 20
            _dataTable.Columns.Add("C_COMPONENT_CODE",        typeof(string));                 // varchar 50
            _dataTable.Columns.Add("C_COMPONENT_PLANT",       typeof(string));                 // varchar 20
            _dataTable.Columns.Add("C_OPNUM",                 typeof(int));                    // int
            _dataTable.Columns.Add("C_NSEQ",                  typeof(int));                    // int
            _dataTable.Columns.Add("C_PHANTOM",               typeof(int));                    // bit
            _dataTable.Columns.Add("C_VAR_QTY",               typeof(double));                 // real
            _dataTable.Columns.Add("C_SCRAP_TYPE",            typeof(char));                   // char    1
            _dataTable.Columns.Add("C_PCT_SCRAP",             typeof(double));                 // real
            _dataTable.Columns.Add("C_WAREHOUSE_CODE",        typeof(string));                 // varchar 20
            _dataTable.Columns.Add("C_EFFECTIVE_DATE",        typeof(DateTime));               // datetime
            _dataTable.Columns.Add("C_EXPIRE_DATE",           typeof(DateTime));               // datetime
            _dataTable.Columns.Add("C_USER_STRING01",         typeof(string));                 // varchar 30
            _dataTable.Columns.Add("C_USER_INT01",            typeof(int));                    // int	
            _dataTable.Columns.Add("C_USER_INT02",            typeof(int));                    // int	
            _dataTable.Columns.Add("C_USER_CHAR01",           typeof(char));                   // char 1
            _dataTable.Columns.Add("C_USER_CHAR02",           typeof(char));                   // char 1
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
                _lista_articoli_rilasciati_in_sage.Add(Item.GetDBV<string>(row[0]));
            }
            return _lista_articoli_rilasciati_in_sage;
        }
    }
}
