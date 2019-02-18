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

        public override void LastAction(ref DBHelper2 cm)
        {
            LastAction_CheckCoerenzaDB(ref cm);
            return;
            LastAction_RiferimentiFornitori(ref cm);
        }
        protected void LastAction_CheckCoerenzaDB(ref DBHelper2 cm)
        {
            string testo_mail = "";

            //verifico che tutti i componenti in distinta base siano presenti in anagrafica
            string chk_query =
                @"SELECT [C_BOM_CODE], [C_COMPONENT_CODE]
  FROM [CyberPlanFrontiera].[dbo].[CYB_COMPONENT] C
  left join [CyberPlanFrontiera].[dbo].[CYB_ITEM] I
  on C.C_COMPONENT_CODE=I.[C_CODE ] and [C_ITEM_GROUP ]<> '__TOOL__'
  where I.C_CODE is null ";

            DbDataReader dtr = cm.GetReaderSelectCommand(chk_query);
            object[] row = new object[dtr.FieldCount];

            while (dtr.Read())
            {
                dtr.GetValues(row);
                testo_mail += "codice =" + getDBV<string>(row[0]) + "  componente=" + getDBV<string>(row[1]) + ";  il componente non è presente in anagrafica o non è rilasciato" + Utils.NewLineMail();
            }

            //--verifico che tutti i codici con distinta base siano presenti in anagrafica
            chk_query = @"SELECT [C_BOM_CODE],[C_COMPONENT_CODE]
  FROM [CyberPlanFrontiera].[dbo].[CYB_COMPONENT] C
  left join [CyberPlanFrontiera].[dbo].[CYB_ITEM] I
  on C.[C_BOM_CODE]=I.[C_CODE ] and [C_ITEM_GROUP ]<> '__TOOL__'  
  where I.C_CODE is null";
            dtr.Close();

            dtr = cm.GetReaderSelectCommand(chk_query);
            row = new object[dtr.FieldCount];

            while (dtr.Read())
            {
                dtr.GetValues(row);
                testo_mail += "codice =" + getDBV<string>(row[0]) + " ha distinta base ma non è presente in anagrafica o non è rilasciato" + Utils.NewLineMail();
            }




            //  --verifico che tutti i codici in anagrafica siano presenti con distinta base 
            //NB: and [C_M_B ] ='M' --make, prodotti in Sauro
            chk_query = @"SELECT I.[C_CODE ],[C_BOM_CODE],[C_COMPONENT_CODE]
  FROM [CyberPlanFrontiera].[dbo].[CYB_COMPONENT] C
  right join [CyberPlanFrontiera].[dbo].[CYB_ITEM] I
  on C.[C_BOM_CODE]=I.[C_CODE ]   
  where C.[C_BOM_CODE] is null 
  and [C_ITEM_GROUP ]<> '__TOOL__'
  and [C_M_B ] ='M' 
  order by I.[C_CODE ]";

            dtr.Close();
            dtr = cm.GetReaderSelectCommand(chk_query);
            row = new object[dtr.FieldCount];

            while (dtr.Read())
            {
                dtr.GetValues(row);
                testo_mail += "codice =" + getDBV<string>(row[0]) + " ha come in Sage 'tipo proposta'=Produzione ma non ha distinta base" + Utils.NewLineMail();
            }

            if (testo_mail != "")
            {
                Utils.SendMail("it@sauro.net", "codifica@sauro.net", "mail.sauro.net", testo_mail);
            }
        }

        protected void LastAction_RiferimentiFornitori(ref DBHelper2 cm)
        {
            _logger.Info("Inizio inserimento distinte base fornitori");
            Dictionary<string, string> lista = Get_Lista_Articoli_Fornitori_Sage("SAURO");

            //per ogni fornitore/articolo inserisco una nuova distinta base con magazzino diverso

            string query = @" INSERT INTO CYB_COMPONENT  (
 c2.C_BOM_CODE        
,c2.C_BOM_ALT         
,c2.C_COMPONENT_CODE  
,c2.C_COMPONENT_PLANT 
,c2.C_OPNUM           
,c2.C_NSEQ            
,c2.C_PHANTOM         
,c2.C_VAR_QTY         
,c2.C_SCRAP_TYPE      
,c2.C_PCT_SCRAP       
,c2.C_WAREHOUSE_CODE
,c2.C_EFFECTIVE_DATE  
,c2.C_EXPIRE_DATE     
,c2.C_USER_STRING01   
,c2.C_USER_INT01      
,c2.C_USER_INT02      
,c2.C_USER_CHAR01     
,c2.C_USER_CHAR02     
)
SELECT 
C_BOM_CODE
,C_BOM_ALT         
,C_COMPONENT_CODE  
,C_COMPONENT_PLANT 
,C_OPNUM           
,C_NSEQ            
,C_PHANTOM         
,C_VAR_QTY         
,C_SCRAP_TYPE      
,C_PCT_SCRAP       
,'%%1%%'  as C_WAREHOUSE_CODE
,C_EFFECTIVE_DATE  
,C_EXPIRE_DATE     
,C_USER_STRING01   
,C_USER_INT01      
,C_USER_INT02      
,C_USER_CHAR01     
,C_USER_CHAR02     
FROM CYB_COMPONENT c2
WHERE 
C_WAREHOUSE_CODE = '" + __MAGAZZINO_INTERNO + @"' and C_BOM_CODE ='%%2%%' ";
            foreach (KeyValuePair<string, string> itm in lista)
            {
                string ins_query = query.Replace("%%1%%", itm.Value);  //fornitore
                ins_query = ins_query.Replace("%%2%%", itm.Key);    //articolo

                int i = DBHelper2.EseguiSuDBCyberPlan(ref cm, ins_query);

            }

            _logger.Info("Fine inserimento distinte base fornitori");

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
        static private Dictionary<string, string> Get_Lista_Articoli_Fornitori_Sage(string dossier)
        {
            string db = "x3." + dossier;

            Dictionary<string, string> _lista_articoli_fornitori_sage = new Dictionary<string, string>(1500);

            //questa query va bene anche se non filtro gli articoli RILASCIATI
            //perchè poi duplico solo le distinte base già filtrate per articoli rilasciati
            string query = "select ITMREF_0, BPSNUM_0 from " + db + ".ITMBPS where BPSNUM_0 <> 'A000818' ";

            DBHelper2 dbh = DBHelper2.getSageDBHelper(dossier);
            DbDataReader dtr = dbh.GetReaderSelectCommand(query);
            object[] row = new object[dtr.FieldCount];

            while (dtr.Read())
            {
                dtr.GetValues(row);
                _lista_articoli_fornitori_sage.Add(Item.GetDBV<string>(row[0]), Item.GetDBV<string>(row[1]));
            }
            return _lista_articoli_fornitori_sage;
        }

    }
}
