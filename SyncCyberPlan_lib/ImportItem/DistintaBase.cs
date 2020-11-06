using System.Collections.Generic;
using System;
using log4net;
using System.Data;
using System.Data.Common;

namespace SyncCyberPlan_lib
{
    public class DistintaBase : Item
    {
        public string ITMREF_0;
        public string CPNITMREF_0;
        public short BOMSEQ_0;
        public byte YPHAFLG_0;
        public decimal BOMQTY_0;
        public DateTime? BOMSTRDAT_0;
        public DateTime? BOMENDDAT_0;
        public int USESTA_0;
        public byte ITMSTA_0;

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
            ITMREF_0    = getDBV<string>(row[0], "ITMREF_0");
            CPNITMREF_0 = getDBV<string>(row[1], "CPNITMREF_0");
            BOMSEQ_0    = (short)row[2];
            YPHAFLG_0   = (byte)row[3];
            BOMQTY_0    = getDBV<decimal>(row[4], "BOMQTY_0"); //impiego
            BOMSTRDAT_0 = getSageDate(row[5], "BOMSTRDAT_0");
            BOMENDDAT_0 = getSageDate(row[6], "BOMENDDAT_0");
            USESTA_0    = (byte)row[7];
            ITMSTA_0    = (byte)row[8];


            C_BOM_CODE              = EscapeSQL(ITMREF_0, 30);      // varchar     30
            C_BOM_ALT               = EscapeSQL("ITS01", 20);       // varchar     20
            C_COMPONENT_CODE        = EscapeSQL(CPNITMREF_0, 50);   // varchar     50
            C_COMPONENT_PLANT       = EscapeSQL("ITS01", 20);       // varchar     20
            C_OPNUM                 = 0;                            // int           
            C_NSEQ                  = BOMSEQ_0;                     // int           
            C_PHANTOM               = YPHAFLG_0 == 'N' ? 0 : 1;     // bit           
            C_VAR_QTY               = (double)BOMQTY_0;             // real           
            C_SCRAP_TYPE            = ' ';                          // char        1
            C_PCT_SCRAP             = 0;                            // real           
            C_WAREHOUSE_CODE        = __MAGAZZINO_INTERNO;          // varchar     20
            C_EFFECTIVE_DATE        = BOMSTRDAT_0;                  // datetime           
            C_EXPIRE_DATE           = BOMENDDAT_0;                  // datetime           
            C_USER_STRING01         = "";                           // varchar     30
            C_USER_INT01            = 0;                            // int           
            C_USER_INT02            = 0;                            // int           
            C_USER_CHAR01           = ' ';                          // char        1
            C_USER_CHAR02           = ' ';                          // char        1
        }
        public override DataRow GetCyberRow()
        {
            //NON TOGLIERE ci sono più righe in distinta base con lo stesso articolo
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
        public override string GetSelectQuery(bool mode, string dossier, string codice_like, string filtro)
        {
            string db = "x3." + dossier;

            string sage_query = @"select 
  B.ITMREF_0
, D.CPNITMREF_0
, D.BOMSEQ_0
, D.YPHAFLG_0
, D.BOMQTY_0
, D.BOMSTRDAT_0
, D.BOMENDDAT_0
, B.USESTA_0
, M.ITMSTA_0
from " + db + @".BOMD D
join " + db + @".BOM B on B.ITMREF_0=D.ITMREF_0 and B.BOMALT_0=D.BOMALT_0
join " + db + @".ITMMASTER M on M.ITMREF_0=B.ITMREF_0
where 
M.ITMSTA_0=1 and
B.USESTA_0=2 and
(BOMENDDAT_0 ='1753-01-01 00:00:00.000'  or BOMENDDAT_0 >= GETDATE())
and BOMSTRDAT_0 <=GETDATE() "
;
            
            if (!string.IsNullOrWhiteSpace(codice_like))
            {
                sage_query += " and B.ITMREF_0 like '" + codice_like.Trim() + "'";
            }
            sage_query += " order by B.ITMREF_0 desc" + "\n";

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

        public override void LastAction(ref DBHelper2 cm, DBHelper2 sage)
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
                testo_mail += "codice =" + getDBV<string>(row[0], "C_BOM_CODE") + "  componente =" + getDBV<string>(row[1], "C_COMPONENT_CODE") + ";  il componente non è presente in anagrafica o non è rilasciato" + Utils.NewLineMail();
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
                testo_mail += "codice =" + getDBV<string>(row[0], "C_BOM_CODE") + " ha distinta base ma non è presente in anagrafica o non è rilasciato" + Utils.NewLineMail();
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
                string articolo = getDBV<string>(row[0], "C_CODE");
                if (!articolo.StartsWith("WR000"))
                {
                    testo_mail += "codice =" + articolo + " ha come in Sage 'tipo proposta'=Produzione ma non ha distinta base" + Utils.NewLineMail();
                }
            }
            
            Utils.SendMail_Anag(Settings.GetSettings(), testo_mail, "BOM su Cyber");
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
                _lista_articoli_fornitori_sage.Add(Item.GetDBV<string>(row[0], "ITMREF_0"), Item.GetDBV<string>(row[1], "BPSNUM_0"));
            }
            return _lista_articoli_fornitori_sage;
        }

    }
}
