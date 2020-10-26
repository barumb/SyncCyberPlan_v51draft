using System.Collections.Generic;
using System;
using log4net;
using System.Data;
using System.Data.Common;

namespace SyncCyberPlan_lib
{
    public class OrdiniAcq_OPR_As400 : OrdiniAcq_OPR
    {
        public string  MFHTORD;
        public decimal MFHAORD;
        public decimal MFHPORD;
        public string  MFHTCOM;
        public decimal MFHACOM; // anno ODV
        public decimal MFHPCOM; // progressivo ODV
        public decimal MFHSCOM; // riga ODV
        public string  MFHCART;
        public decimal MFHQTRC;
        public decimal MFHDCRE; // DATA
        public string  MFHSTAT;
        public decimal MFVDINI; // DATA
        public decimal MFVDEND; // DATA
        public string  MFVSTAV; // stato riga  MFVSTAV  se =ST ordine in corso, se vuoto ordine in attesa
        public decimal MFHQTPR; // qta prodotta
        public decimal MFVQTSC; // qta scartata

        public string  MFVUTLM; // unita di misura tempo per un pezzo/via  1=ORE  2=100MI-HR   3 Minuti 4 giorni  5 settimane
        public decimal MFVAMPT; // tempo per un pezzo/via
        public string  MFVUTSE; // unita di misura tempo di setup
        public decimal MFVASET; // tempo di setup

        public string MFVWRKC;  // centro di lavoro (Interno/esterno in as400)
        
        public string X3ORDNR;  // Numero ordine + di riferimento in X3
        public int? X3ORDLN;   // Numero riga ordine di riferimento in X3

        public string X3ORDNRLN; // riferimento all'ordine di vendita (Nr ord + Riga ordine) utilizzato per gli OPR

        public OrdiniAcq_OPR_As400(/*string YPOHTYP*/): base()
        {
            //__YPOHTYP_filter = YPOHTYP;
        }

        public override string GetSelectQuery(bool mode, string dossier, string codice_like, string filtro)
        {
            string __libreriaAs400 = dossier;

            string _tabMFH = __libreriaAs400 + ".MFH00PF";
            string _tabMFV = __libreriaAs400 + ".MFV00PF";
            string _tabVOO = __libreriaAs400 + ".VOOS00F";  // Tabella di transcodifica ordnine riga X3 -> Ordine riga As400


            /* CON questa Query popolo con i dati di AS400. Devo invece usare le coordinate ordine di X3  tramite la tabella VOOS00F 
            string query = "SELECT " + "\n"
                + "  " + _tabMFH + ".MFHTORD" + "\n"
                + ",  " + _tabMFH + ".MFHAORD" + "\n"
                + ",  " + _tabMFH + ".MFHPORD" + "\n"
                + ",  " + _tabMFH + ".MFHTCOM" + "\n"
                + ",  " + _tabMFH + ".MFHACOM" + "\n"
                + ",  " + _tabMFH + ".MFHPCOM" + "\n"
                + ",  " + _tabMFH + ".MFHSCOM" + "\n"
                + ",  " + _tabMFH + ".MFHCART" + "\n"
                + ",  " + _tabMFH + ".MFHQTRC" + "\n"
                + ",  " + _tabMFH + ".MFHDCRE" + "\n"
                + ",  " + _tabMFH + ".MFHSTAT" + "\n"
                + ",  " + _tabMFV + ".MFVDINI" + "\n"
                + ",  " + _tabMFV + ".MFVDEND" + "\n"
                + ",  " + _tabMFV + ".MFVSTAV" + "\n"
                + ",  " + _tabMFH + ".MFHQTPR" + "\n"
                + ",  " + _tabMFV + ".MFVQTSC" + "\n"

                + ",  " + _tabMFV + ".MFVUTLM" + "\n"
                + ",  " + _tabMFV + ".MFVAMPT" + "\n"
                + ",  " + _tabMFV + ".MFVUTSE" + "\n"
                + ",  " + _tabMFV + ".MFVASET" + "\n"
                + ",  " + _tabMFV + ".MFVWRKC" + "\n"
                + " FROM " + _tabMFH + "\n"
                + " INNER JOIN " + _tabMFV + " ON " + "\n"
                                 + _tabMFH + ".MFHTORD = " + _tabMFV + ".MFVTORD " + "\n"
                       + " AND " + _tabMFH + ".MFHAORD = " + _tabMFV + ".MFVAORD " + "\n"
                       + " AND " + _tabMFH + ".MFHPORD = " + _tabMFV + ".MFVPORD " + "\n"
                + " WHERE " + _tabMFH + ".MFHSTAT = 'RI' " + "\n"     //sempre RI
                + " and   " + _tabMFV + ".MFVSTAT = 'RI' " + "\n"     //sempre RI
                + " and   " + _tabMFV + ".MFVSTAV <> 'CH' " + "\n"    //questo indica se la riga è chiusa
                ;
            */
            /* Nuova versione che recupera il NR ordine e relativa riga di X3 tramite la tabella di relazione VOOS00F */
            string query = "SELECT " + "\n"
                + "   MFH.MFHTORD" + "\n"
                + ",  MFH.MFHAORD" + "\n"
                + ",  MFH.MFHPORD" + "\n"
                + ",  MFH.MFHTCOM" + "\n"
                + ",  MFH.MFHACOM" + "\n"
                + ",  MFH.MFHPCOM" + "\n"
                + ",  MFH.MFHSCOM" + "\n"
                + ",  MFH.MFHCART" + "\n"
                + ",  MFH.MFHQTRC" + "\n"
                + ",  MFH.MFHDCRE" + "\n"
                + ",  MFH.MFHSTAT" + "\n"
                + ",  MFV.MFVDINI" + "\n"
                + ",  MFV.MFVDEND" + "\n"
                + ",  MFV.MFVSTAV" + "\n"
                + ",  MFH.MFHQTPR" + "\n"
                + ",  MFV.MFVQTSC" + "\n"

                + ",  MFV.MFVUTLM" + "\n"
                + ",  MFV.MFVAMPT" + "\n"
                + ",  MFV.MFVUTSE" + "\n"
                + ",  MFV.MFVASET" + "\n"
                + ",  MFV.MFVWRKC" + "\n"
                + ",  trim(X3.SOHNR) as X3ORDNR " + "\n"
                + ",  int(X3.SOLNR) AS X3ORDLN " + "\n"
               
                + " FROM " + _tabMFH + " MFH \n"
                + " INNER JOIN " + _tabMFV + " MFV ON " + "\n"
                       + "     MFH.MFHTORD = MFV.MFVTORD " + "\n"
                       + " AND MFH.MFHAORD = MFV.MFVAORD " + "\n"
                       + " AND MFH.MFHPORD = MFV.MFVPORD " + "\n"
                + " LEFT JOIN " + _tabVOO + " X3 ON " + "\n"
                       + "     MFH.MFHTCOM = X3.TPOVE " + "\n"
                       + " AND MFH.MFHACOM = X3.AAOVE " + "\n"
                       + " AND MFH.MFHPCOM = X3.NROVE " + "\n"
                       + " AND MFH.MFHSCOM = X3.LNOVE " + "\n"
                       + " AND MFH.MFHACOM > 0 \n"
                + " WHERE MFH.MFHSTAT = 'RI' " + "\n"     //sempre RI
                + " and   MFV.MFVSTAT = 'RI' " + "\n"     //sempre RI
                + " and   MFV.MFVSTAV <> 'CH' " + "\n"    //questo indica se la riga è chiusa
                ;


            if (!string.IsNullOrWhiteSpace(codice_like))
            {
                query += " and MFH.MFHCART like '" + codice_like.Trim() + "'";
            }

            query += " ORDER BY " 
                + " MFH.MFHTORD," + "\n"
                + " MFH.MFHAORD," + "\n"
                + " MFH.MFHPORD" + "\n"
                ;
            return query;
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

            MFVUTLM = getDBV<string>(row[16], "MFVUTLM");
            MFVAMPT = getDBV<decimal>(row[17], "MFVAMPT");
            MFVUTSE = getDBV<string>(row[18], "MFVUTSE");
            MFVASET = getDBV<decimal>(row[19], "MFVASET");

            MFVWRKC = getDBV<string>(row[20], "MFVWRKC");
            //20201023-it2adm>> Riferimento nrordine+nrriga di X3
            X3ORDNR = getDBV<string>(row[21], "X3ORDNR");
            X3ORDLN =getDBV<int>(row[22], "X3ORDLN");

            X3ORDNRLN = "000000000000";  //valore di default che indica ordini a fabbisogno

            if ((!String.IsNullOrEmpty(X3ORDNR)) && (X3ORDLN!=null))
            {
                X3ORDNRLN = X3ORDNR + Convert.ToString(X3ORDLN).PadLeft(6, '0');
            }



            C_CODE = EscapeSQL(MFHTORD + MFHAORD.ToString("00") + MFHPORD.ToString("000000"), 30);        //varchar         30                
            //ATTENZIONE il valore "000000000000" indica valore default per ordini a fabbisogno
            C_CORDER_CODE = EscapeSQL(X3ORDNRLN, 30);   //varchar 30  
            // Vecchia versione. Modificata da IT2adm 20201023
            //C_CORDER_CODE         = EscapeSQL(MFHTCOM + MFHACOM.ToString("00") + MFHPCOM.ToString("000000") + MFHSCOM.ToString("0000"), 30);   //varchar 30  
            C_ITEM_CODE           = EscapeSQL(MFHCART, 50);                                        //varchar         50                      
            C_ITEM_PLANT          = EscapeSQL("ITS01", 20);                                        //varchar         20                      
            C_M_B                 = get_C_M_B(MFVWRKC);//'M';                                                           //char             1     // B=buy D=decentrato M = make                
            C_MRP_TYPE            = getMRP_type(MFHTCOM);                                          //char             1     //F=MTS make to stock (a fabbisogno)  C= MTO make to order (a commessa)                     
            C_QTY                 = MFHQTRC;                                                       //numeric            
            C_COMPL_QTY           = MFHQTPR;                                                       //numeric            
            C_SCRAP_QTY           = MFVQTSC;                                                       //numeric            qta scartata
            C_HOST_QTY            = 0;                                                             //numeric            
            C_INSERT_DATE         = dateTime_fromDataAs400(MFHDCRE);                               //datetime           
            C_HOST_STDATE         = dateTime_fromDataAs400(MFVDINI);                               //datetime           //data inizio           
            C_HOST_DUEDATE        = MFVDINI> MFVDEND? dateTime_fromDataAs400(MFVDINI) : dateTime_fromDataAs400(MFVDEND);  //datetime  data fine      Qualche volta in As400 c'è data inizio maggiore di data fine; in tal caso le mettiamo uguali
            C_PROMISE_DATE        = null;                                                          //datetime           
            C_ACT_STDATE          = null;                                                          //datetime           
            C_ACT_DUEDATE         = null;                                                          //datetime           
            C_SHOP_FLOOR_CODE     = EscapeSQL("", 20);                                             //varchar         20                      
            C_STATUS              = MFVSTAV== "ST" ? 6 : 4;                                        //int  in Cyber 6 iniziato, 4 confermato e fattibile
            C_HOST_STATUS         = EscapeSQL("", 15);                                             //varchar         15                      
            C_HOST_CODE           = EscapeSQL("", 30);                                             //varchar         30                      
            C_ROUTING_CODE        = EscapeSQL("", 51);                                             //varchar         51                      
            C_ROUTING_ALT         = EscapeSQL("", 9);                                              //varchar         9                       
            C_BOM_CODE            = EscapeSQL("", 30);                                             //varchar         30                      
            C_BOM_ALT             = EscapeSQL("", 20);                                             //varchar         20                      
            C_ALT_PROD            = EscapeSQL("", 20);                                             //varchar         20                      
            C_HOST_ALT_PROD       = EscapeSQL("", 20);                                             //varchar         20                      
            C_HOST_ALT_ROUTING    = EscapeSQL("", 8);                                              //varchar         8                       
            C_HOST_LAST_UPDATE    = null;                                                          //datetime           
            C_COST                = 0;                                                             //float              
            C_VALUE               = -9999;                                                         //float              
            C_ORD_GROUP           = EscapeSQL("", 10);                                             //varchar         10                      
            C_MANAGER             = EscapeSQL("", 20);                                             //varchar         20                      
            C_SUPPLIER_CODE       = EscapeSQL("", 30);                                             //varchar         30                      
            C_WAREHOUSE_CODE      = EscapeSQL(__MAGAZZINO_INTERNO, 20);                            //varchar         20                      
            C_USER_INT01          = 0;                                                             //int               
            C_USER_INT02          = 0;                                                             //int               
            C_USER_INT03          = 0;                                                             //int               
            C_USER_INT04          = 0;                                                             //int               
            C_USER_INT05          = 0;                                                             //int               
            C_USER_INT06          = 0;                                                             //int               
            C_USER_INT07          = 0;                                                             //int               
            C_USER_INT08          = 0;                                                             //int               
            C_USER_INT09          = 0;                                                             //int               
            C_USER_INT10          = 0;                                                             //int               
            C_USER_REAL01         = 0;                                                             //float             
            C_USER_REAL02         = 0;                                                             //float             
            C_USER_REAL03         = 0;                                                             //float             
            C_USER_REAL04         = 0;                                                             //float             
            C_USER_REAL05         = 0;                                                             //float             
            C_USER_CHAR01         = ' ';                                                           //char            1                       
            C_USER_CHAR02         = ' ';                                                           //char            1                       
            C_USER_CHAR03         = ' ';                                                           //char            1                       
            C_USER_CHAR04         = ' ';                                                           //char            1                       
            C_USER_CHAR05         = ' ';                                                           //char            1                       
            C_USER_FLAG01         = 0;                                                             //bit               
            C_USER_FLAG02         = 0;                                                             //bit               
            C_USER_STRING01       = EscapeSQL("", 29);                                             //varchar         29                      
            C_USER_STRING02       = EscapeSQL("", 29);                                             //varchar         29                      
            C_USER_STRING03       = EscapeSQL("", 29);                                             //varchar         29                      
            C_USER_STRING04       = EscapeSQL("", 29);                                             //varchar         29                      
            C_USER_STRING05       = EscapeSQL("", 29);                                             //varchar         29                      
            C_USER_STRING06       = EscapeSQL("", 29);                                             //varchar         29                      
            C_USER_STRING07       = EscapeSQL("", 29);                                             //varchar         29                      
            C_USER_STRING08       = EscapeSQL("", 29);                                             //varchar         29                      
            C_USER_STRING09       = EscapeSQL("", 29);                                             //varchar         29                      
            C_USER_STRING10       = EscapeSQL("", 29);                                             //varchar         29                      
            C_USER_NOTE01         = EscapeSQL("", 99);                                             //varchar         99                      
            C_USER_COLOR01        = 0;                                                             //int                                
            C_USER_COLOR02        = 0;                                                             //int                                
            C_USER_DATE01         = null;                                                          //datetime                           
            C_USER_DATE02         = null;                                                          //datetime                           
            C_USER_DATE03         = null;                                                          //datetime                           
            C_USER_DATE04         = null;                                                          //datetime                           
            C_USER_DATE05         = null;                                                          //datetime 
        }
        protected char get_C_M_B(string CDL_As400)
        {
            if (CDL_As400 == "CDLEXT")
                return 'D';  //contolavoro
            else if (CDL_As400 == "CDLINT")
                return 'M';  //make
            else
            {
                Utils.SendMail_IT(Settings.GetSettings(), "Errore import OPR in CyberPlan: "+ C_CODE+" ha cdl in as400 diverso da CDLEXT/CDLINT","OPR As400");
                return '?';
            }
        }
        
        char getMRP_type(string MFHTCOM)
        {
            //F=MTS make to stock (a fabbisogno)  C= MTO make to order (a commessa)  
            if (MFHTCOM.Trim() !=  "")
            {
                return 'C'; //ordine a commessa
            }
            else
            {
                return 'F';//ordine MRP a fabbisogno
            }
        }
    }
}
