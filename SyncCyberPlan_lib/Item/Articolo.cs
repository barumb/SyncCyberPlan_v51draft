using System.Collections.Generic;
using System;
using log4net;
using System.Data;
using System.Data.Common;


namespace SyncCyberPlan_lib
{
    public class Articolo: Item
    {
        protected string __TRASH__ = "TRASH";
        //tabella ITMMASTER
        public string ITMREF_0;  //codice articolo
        public string ITMDES1_0;
        public string STU_0;     //unità misura
        public string PUU_0;     //unità misura
        public string SAU_0;     //unità misura
        public string TCLCOD_0;  //categoria len 5
        public string WEU_0;     //deve essere "GR" per WP
        public decimal ITMWEI_0; //peso per PLASTICA WP
        public byte STDFLG_0; //Modalità gestione (stock=3 a commessa=4)
        public byte PHAFLG_0;  //flag fantasma, maschera ITM1 (nascosto); si trova in categoria 0PHA e anche in maschera ITF0 /articolo sito, nascosto)

        //tabella YITMINF
        public string YLIVTRAS_0;  //tipo articolo
        public string YFAMCOM_0;   //famiglia commerciale
        public int YNUMVIE_0;      //num vie totali
        public decimal YALTEZZA_0; //altezza Per MORSETTI
        public string YMRPTAG1_0;  //tag mrp: per WP deve contenere F se è il WP da far cambiare il meno possibile, il WP su cui basare il raggruppamento degli ordini 

        public decimal YCORPOLUN_0;       //lunghezza x WP (?)
        public decimal YCORPOPRO_0;       //profodità...
        public string YMAT_0;             //per capire se è sth PER WG (granulati->PLASTICA)
        public string YTAGOEM_0;          //Marchio
        public string YCOLORE_0;          //Colore
        public decimal YPESMAT_0;         //peso materozza/impronta
        public string YPASSOVIE_0;        //passo        
        public int   YCONFQTA_0;         //qta per confezione
        public int   YQTADECIMI_0;         //qta per confezione
        public string YSTAMPCOLORE_0;      //Color inchiostro stampigliatura
        public byte YPIEFLG_0;  //flag "Richiede piegatrice" per FILO



        //tabella ITMFACILIT
        public byte REOCOD_0;    //tipo proposta
        public decimal PRPLTI_0; //Lead Time Preparazione > C_PROTECTION_TIME
        public decimal SHR_0;    //% scarto

        public string[] DEFLOCTYP = new string[15]; // len 5
        public string[] DEFLOC= new string[15];     // len 10
        public decimal YQTAMIN_0;
        public decimal YQTAMAX_0;
        public decimal YQTAMUL_0;
        public decimal REOTSD_0;    // punto di riordino
        public decimal SAFSTO_0;    // stock sicurezza
        public decimal MAXSTO_0;    // stock massimo
        public decimal REOMINQTY_0; // lotto economico
        public decimal MFGLOTQTY_0; // lotto tecnico
        public decimal MFGLTI_0;    // Lead time di produzione
        public decimal QUALTI_0;    // Lead time controllo qualità
        public decimal OFS_0;       // Lead time Acquisto
        public int[] LOCNUM = new int[15]; // descrizione locazione
        public string YWCR_0;   //Reparto (centro di carico)
        public decimal YQTAPREANT_0;  //Qta prelievo anticipato, inserito apposta per mrp

        //tabella ITMBPS
        public string BPSNUM_0;

        //vista  FAMPEX
        public char FAMPEX;

        static protected Dictionary<string, Tuple<short, string, string>> datiContoLavoro = null;

        #region tabella output CYB_ITEM        
        public string C_CODE	; // varchar 50
        public string C_PLANT_CODE	; // varchar 20
        public string C_DESCR	; // varchar 50
        public char C_M_B	; // char 1
        public int C_PHANTOM	; // bit 
        public int C_FIXED_LEAD_TIME	; // int 
        public double C_VAR_LEAD_TIME	; // real 
        public double C_BUY_PREPROC_LT	; // real 
        public double C_BUY_PROC_LT	; // real 
        public double C_BUY_POSTPROC_LT	; // real 
        public double C_UNAVAIL_TIME	; // real 
        public double C_PROTECTION_TIME	; // real 
        public string C_UOM	; // varchar 3
        public string C_UOM2	; // varchar 3
        public double C_LENGTH	; // real 
        public double C_WIDTH	; // real 
        public char C_SCRAP_TYPE	; // char 1
        public double C_PROCESS_SCRAP	; // real 
        public string C_STD_WAREHOUSE_CODE	; // varchar 20
        public string C_ITEM_GROUP	; // varchar 8
        public string C_MANAGER	; // varchar 20
        public string C_HOST_CODE	; // varchar 50
        public string C_SUPPLIER_CODE	; // varchar 30
        public char C_ABC_CLASS	; // char 1
        public float C_VALUE	; // float	
        public float C_COST	; // float	
        public char C_MRP_TYPE	; // char 1
        public int C_POQ_DAYS	; // int	
        public int C_POQ_HOURS	; // int	
        public int C_DTF	; // int	
        public decimal C_LOT_SIZE	; // numeric	
        public decimal C_MIN_ORDER_QTY	; // numeric	
        public decimal C_MAX_ORDER_QTY	; // numeric	
        public decimal C_DEM_GROUPING_QTY	; // numeric	
        public decimal C_DEM_GROUPING_MAX_QTY	; // numeric	
        public decimal C_ROP_QTY	; // numeric	
        public decimal C_SS_QTY	; // numeric	
        public int C_USE_UNPEGGED_QTY	; // bit	
        public int C_USER_INT01	; // int	
        public int C_USER_INT02	; // int	
        public int C_USER_INT03	; // int	
        public int C_USER_INT04	; // int	
        public int C_USER_INT05	; // int	
        public int C_USER_INT06	; // int	
        public int C_USER_INT07	; // int	
        public int C_USER_INT08	; // int	
        public int C_USER_INT09	; // int	
        public int C_USER_INT10	; // int	
        public float C_USER_REAL01	; // float	   //per morsetti altezza??
        public float C_USER_REAL02	; // float	
        public float C_USER_REAL03	; // float	
        public float C_USER_REAL04	; // float	
        public float C_USER_REAL05	; // float	
        public char C_USER_CHAR01	; // char 1   //per morsetti CATEGORIA altezza??
        public char C_USER_CHAR02	; // char 1
        public char C_USER_CHAR03	; // char 1   //FAMPEX per PLASTICA
        public char C_USER_CHAR04	; // char 1
        public char C_USER_CHAR05	; // char 1
        public byte C_USER_FLAG01	; // bit	
        public byte C_USER_FLAG02	; // bit	
        public byte C_USER_FLAG03	; // bit	
        public string C_USER_STRING01	; // varchar 29
        public string C_USER_STRING02	; // varchar 29
        public string C_USER_STRING03	; // varchar 29
        public string C_USER_STRING04	; // varchar 29
        public string C_USER_STRING05	; // varchar 29
        public string C_USER_STRING06	; // varchar 29
        public string C_USER_STRING07	; // varchar 29
        public string C_USER_STRING08	; // varchar 29
        public string C_USER_STRING09	; // varchar 29
        public string C_USER_STRING10	; // varchar 29
        public string C_USER_NOTE01	; // varchar 99
        public int C_USER_COLOR01	; // int	
        public int C_USER_COLOR02	; // int	
        public DateTime? C_USER_DATE01	; // datetime	
        public DateTime? C_USER_DATE02	; // datetime	
        public DateTime? C_USER_DATE03	; // datetime	
        public DateTime? C_USER_DATE04	; // datetime	
        public DateTime? C_USER_DATE05	; // datetime	
        public float C_PROD_LOT_QTY;    //qta prelievo anticipato per overlap ordini
        #endregion


        public Articolo(): base("CYB_ITEM")
        {

        }

        public override void Init(object[] row)
        {
            ITMREF_0 = getDBV<string>(row[0]);

            if (ITMREF_0.StartsWith("WP"))
            {
                ;
            }


            ITMDES1_0 = getDBV<string>(row[1]);
            STU_0 = getDBV<string>(row[2]);
            PUU_0 = getDBV<string>(row[3]);
            SAU_0 = getDBV<string>(row[4]);
            TCLCOD_0 = getDBV<string>(row[5]);
            YLIVTRAS_0 = getDBV<string>(row[6]);
            YFAMCOM_0 = getDBV<string>(row[7]);
            //YPRDAPP_0 = getDBV_char(row[8]);  campo che non eiste più
            REOCOD_0 = getDBV<byte>(row[9]);             //tipo proposta
            PRPLTI_0 = getDBV<decimal>(row[10]);
            SHR_0 = getDBV<decimal>(row[11]);            //% scarto
            DEFLOCTYP[0] = getDBV<string>(row[12]);
            DEFLOCTYP[1] = getDBV<string>(row[13]);
            DEFLOCTYP[2] = getDBV<string>(row[14]);
            DEFLOCTYP[3] = getDBV<string>(row[15]);
            DEFLOCTYP[4] = getDBV<string>(row[16]);
            DEFLOCTYP[5] = getDBV<string>(row[17]);
            DEFLOCTYP[6] = getDBV<string>(row[18]);
            DEFLOCTYP[7] = getDBV<string>(row[19]);
            DEFLOCTYP[8] = getDBV<string>(row[20]);
            DEFLOCTYP[9] = getDBV<string>(row[21]);
            DEFLOCTYP[10] = getDBV<string>(row[22]);
            DEFLOCTYP[11] = getDBV<string>(row[23]);
            DEFLOCTYP[12] = getDBV<string>(row[24]);
            DEFLOCTYP[13] = getDBV<string>(row[25]);
            DEFLOCTYP[14] = getDBV<string>(row[26]);
            DEFLOC[0] = getDBV<string>(row[27]);
            DEFLOC[1] = getDBV<string>(row[28]);
            DEFLOC[2] = getDBV<string>(row[29]);
            DEFLOC[3] = getDBV<string>(row[30]);
            DEFLOC[4] = getDBV<string>(row[31]);
            DEFLOC[5] = getDBV<string>(row[32]);
            DEFLOC[6] = getDBV<string>(row[33]);
            DEFLOC[7] = getDBV<string>(row[34]);
            DEFLOC[8] = getDBV<string>(row[35]);
            DEFLOC[9] = getDBV<string>(row[36]);
            DEFLOC[10] = getDBV<string>(row[37]);
            DEFLOC[11] = getDBV<string>(row[38]);
            DEFLOC[12] = getDBV<string>(row[39]);
            DEFLOC[13] = getDBV<string>(row[40]);
            DEFLOC[14] = getDBV<string>(row[41]);
            YQTAMIN_0 = getDBV<decimal>(row[42]);
            YQTAMAX_0 = getDBV<decimal>(row[43]);
            YQTAMUL_0 = getDBV<decimal>(row[44]);
            REOTSD_0 = getDBV<decimal>(row[45]);
            SAFSTO_0 = getDBV<decimal>(row[46]);
            MAXSTO_0 = getDBV<decimal>(row[47]);
            REOMINQTY_0 = getDBV<decimal>(row[48]); //lotto economico
            MFGLOTQTY_0 = getDBV<decimal>(row[49]);  //lotto tecnico
            MFGLTI_0 = getDBV<decimal>(row[50]);  // Lead time di produzione
            QUALTI_0 = getDBV<decimal>(row[51]);  // Lead time controllo qualità
            OFS_0 = getDBV<decimal>(row[52]); // Lead time Acquisto
            LOCNUM[0] = getDBV<byte>(row[53]);
            LOCNUM[1] = getDBV<byte>(row[54]);
            LOCNUM[2] = getDBV<byte>(row[55]);
            LOCNUM[3] = getDBV<byte>(row[56]);
            LOCNUM[4] = getDBV<byte>(row[57]);
            LOCNUM[5] = getDBV<byte>(row[58]);
            LOCNUM[6] = getDBV<byte>(row[59]);
            LOCNUM[7] = getDBV<byte>(row[60]);
            LOCNUM[8] = getDBV<byte>(row[61]);
            LOCNUM[9] = getDBV<byte>(row[62]);
            LOCNUM[10] = getDBV<byte>(row[63]);
            LOCNUM[11] = getDBV<byte>(row[64]);
            LOCNUM[12] = getDBV<byte>(row[65]);
            LOCNUM[13] = getDBV<byte>(row[66]);
            LOCNUM[14] = getDBV<byte>(row[67]);
            YNUMVIE_0  = getDBV<short>(row[68]);
            YALTEZZA_0 = getDBV<decimal>(row[69]);
            YMRPTAG1_0 = getDBV<string>(row[70]);

            YCORPOLUN_0  = getDBV<decimal>(row[71]);        //lunghezza x WP (?)
            YCORPOPRO_0  = getDBV<decimal>(row[72]);        //profodità...
            YMAT_0       = getDBV<string>(row[73]);         //per capire se è sth il WG
            YTAGOEM_0    = getDBV<string>(row[74]);         //MArchio
            WEU_0        = getDBV<string>(row[75]);         //deve essere GR per WP
            ITMWEI_0     = getDBV<decimal>(row[76]);        //peso WP
            YCOLORE_0    = getDBV<string>(row[77]);
            //BPSNUM_0     = getDBV<string>(row[78]);
            BPSNUM_0 = "";
            YPESMAT_0    = getDBV<decimal>(row[79]);        //peso materozza/impronta
            YPASSOVIE_0  = getDBV<string>(row[80]);
            STDFLG_0     = getDBV<byte>(row[81]);            //gestione a stock/commessa
            YCONFQTA_0   = getDBV<int>(row[82]);
            YQTADECIMI_0 = getDBV<int>(row[83]);
            YWCR_0       = getDBV<string>(row[84]);   //reparto (CdC)
            YQTAPREANT_0 = getDBV<decimal>(row[85]);
            FAMPEX       = getDBV_char(row[86]);
            YSTAMPCOLORE_0 = getDBV<string>(row[87]);
            YPIEFLG_0    = getDBV<byte>(row[88]);
            PHAFLG_0    = getDBV<byte>(row[89]);

            C_CODE                               = EscapeSQL(ITMREF_0, 50);          // varchar 50
            C_PLANT_CODE                         = "ITS01";                          // varchar 20
            C_DESCR                              = EscapeSQL(ITMDES1_0, 50);         // varchar 50
            C_M_B                                = getTipoProposta(REOCOD_0);        // char 1  o APPROVIGIONAMENTO?  MAKE or BUY
            C_PHANTOM                            = PHAFLG_0 == 2 ? 1 : 0;           // bit 
            C_FIXED_LEAD_TIME                    = (C_M_B=='M'?(int)MFGLTI_0: (int)OFS_0) ;                    // int  lead time di produzione o di acquisto in base al Tipo proposta
            C_VAR_LEAD_TIME                      = -1;                               // real 
            C_BUY_PREPROC_LT                     = -1;                               // real 
            C_BUY_PROC_LT                        = -1;                       // real 
            C_BUY_POSTPROC_LT                    = -1;                               // real 
            C_UNAVAIL_TIME                       = (int)PRPLTI_0;                    // real 
            C_PROTECTION_TIME                    = 0;                                // real 
            C_UOM                                = EscapeSQL(STU_0, 3);              // varchar 3
            C_UOM2                               = EscapeSQL(PUU_0, 3);              // varchar 3
            C_LENGTH                             = (double)YCORPOLUN_0;              // real 
            C_WIDTH                              = (double)YCORPOPRO_0;              // real 
            C_SCRAP_TYPE                         = ' ';                              // char 1
            C_PROCESS_SCRAP                      = (double)SHR_0;                    // real 
            C_STD_WAREHOUSE_CODE                 = get_MagazzinoRicevimento();       // varchar 20
            C_ITEM_GROUP                         = EscapeSQL(TCLCOD_0, 8);           // varchar 8
            C_MANAGER                            = "";                               // varchar 20
            C_HOST_CODE                          = "";                               // varchar 50
            C_SUPPLIER_CODE                      = EscapeSQL("", 30);          // varchar 30
            C_ABC_CLASS                          = ' ';                              // char 1
            C_VALUE                              = 0;                                // float	
            C_COST                               = 0;                                // float	
            C_MRP_TYPE                           = getMrpType(ITMREF_0, TCLCOD_0, STDFLG_0);      // char 1
            C_POQ_DAYS                           = 0;                                // int	
            C_POQ_HOURS                          = 0;                                // int	
            C_DTF                                = 0;                                // int	
            C_LOT_SIZE                           = getQtaLOT(ITMREF_0, TCLCOD_0, YLIVTRAS_0,YQTAMUL_0);                        // numeric	
            C_MIN_ORDER_QTY                      = getQtaMIN(ITMREF_0, TCLCOD_0, YLIVTRAS_0,YQTAMIN_0);                        // numeric	
            C_MAX_ORDER_QTY                      = getQtaMAX(ITMREF_0, TCLCOD_0, YLIVTRAS_0,YQTAMAX_0);                        // numeric	
            C_DEM_GROUPING_QTY                   = 0;                                // numeric	
            C_DEM_GROUPING_MAX_QTY               = 0;                                // numeric	
            C_ROP_QTY                            = REOTSD_0;                         // numeric	
            C_SS_QTY                             = SAFSTO_0;                         // numeric	
            C_USE_UNPEGGED_QTY                   = 0;                                // bit	
            C_USER_INT01                         = YNUMVIE_0;                        // int	
            C_USER_INT02                         = getMarchio(YTAGOEM_0);            // int	
            C_USER_INT03                         = YCONFQTA_0;                       // int	Confez QTA
            C_USER_INT04                         = YQTADECIMI_0;                     // int	Decimo di confezione
            C_USER_INT05                         = 0;                                // int	
            C_USER_INT06                         = 0;                                // int	
            C_USER_INT07                         = 0;                                // int	
            C_USER_INT08                         = 0;                                // int	
            C_USER_INT09                         = 0;                                // int	
            C_USER_INT10                         = 0;                                // int	
            C_USER_REAL01                        = getPeso(YWCR_0, ITMREF_0, ITMWEI_0, WEU_0);                  // float	
            C_USER_REAL02                        = GetPesoMaterozza(ITMREF_0, YWCR_0, YPESMAT_0);                 // float	
            C_USER_REAL03                        = 0;                                // float	
            C_USER_REAL04                        = 0;                                // float	
            C_USER_REAL05                        = 0;                                // float	
            C_USER_CHAR01                        = (char)EscapeSQL(YMRPTAG1_0,1)[0]; // char 1  Categoria pe morsetti / Frontalino Per WP
            C_USER_CHAR02                        = YPIEFLG_0==2?'P':' ';             // char 1  Piega o non piega per FILO
            C_USER_CHAR03                        = FAMPEX;   //' '; //aggiornata successivamente da Articolo_caratteristiche() =getFAMP(ITMREF_0, ITMDES1_0);     // char 1
            C_USER_CHAR04                        = getSTH(TCLCOD_0,YMAT_0);          // char 1
            C_USER_CHAR05                        = ' ';                              // char 1
            C_USER_FLAG01                        = 0;                                // bit	
            C_USER_FLAG02                        = 0;                                // bit	
            C_USER_FLAG03                        = 0;                                // bit	
            C_USER_STRING01                      = getReparto(YWCR_0);             // varchar 29
            C_USER_STRING02                      = EscapeSQL(YCOLORE_0, 29);         // varchar 29
            C_USER_STRING03                      = EscapeSQL("", 29);                // varchar 29  SEZIONE per FILO
            C_USER_STRING04                      = EscapeSQL(YPASSOVIE_0, 29);       // varchar 29  passo
            C_USER_STRING05                      = EscapeSQL(YSTAMPCOLORE_0, 29);    // varchar 29
            C_USER_STRING06                      = EscapeSQL("", 29);                // varchar 29
            C_USER_STRING07                      = EscapeSQL("", 29);                // varchar 29
            C_USER_STRING08                      = EscapeSQL("", 29);                // varchar 29
            C_USER_STRING09                      = EscapeSQL("", 29);                // varchar 29
            C_USER_STRING10                      = EscapeSQL("", 29);                // varchar 29
            C_USER_NOTE01                        = "";                               // varchar 99
            C_USER_COLOR01                       = 0;                                // int	
            C_USER_COLOR02                       = 0;                                // int	
            C_USER_DATE01                        = null;                             // datetime	
            C_USER_DATE02                        = null;                             // datetime	
            C_USER_DATE05                        = null;                             // datetime	
            C_USER_DATE03                        = null;                             // datetime	
            C_USER_DATE04                        = null;                             // datetime	
            C_PROD_LOT_QTY                       = (float)YQTAPREANT_0;


           // if (datiContoLavoro.ContainsKey(ITMREF_0))
           // {
           //     //fatto da CyberPlan if (datiContoLavoro[ITMREF_0].Item1 != 0)
           //     //fatto da CyberPlan {
           //     //fatto da CyberPlan     //modifico solo se diverso da zero; così è possibile mettere 0 sull'attrezzatura e fissare articolo per articolo il LeadTime
           //     //fatto da CyberPlan     C_FIXED_LEAD_TIME = datiContoLavoro[ITMREF_0].Item1;
           //     //fatto da CyberPlan }
           //     //fatto da CyberPlan C_SUPPLIER_CODE = datiContoLavoro[ITMREF_0].Item2;
           //     //controllo
           //     if (C_M_B != 'D')
           //     {
           //         __bulk_message += Utils.NewLineMail() + " articolo " + ITMREF_0 + " assegnato ad attrezzatura di contolavoro, ma ha tipo proposta " + REOCOD_0;
           //     }
           // }
           // else
           // {
           //     if (C_M_B == 'D')
           //     {
           //         __bulk_message += Utils.NewLineMail() + " articolo " + ITMREF_0 + " non assegnato ad attrezzatura di contolavoro, ma ha tipo proposta Contolavoro";
           //     }
           // }


            if (
                (YLIVTRAS_0 == "SF" && PHAFLG_0 != 2)
                ||
                (YLIVTRAS_0 != "SF" && PHAFLG_0 == 2)
                )
            {
                __bulk_message += Utils.NewLineMail() + " Articolo " + ITMREF_0 + " ha liv.trasformazione " + YLIVTRAS_0 + " ma flag PHAFLG pari a " + PHAFLG_0;
            }
        }
        public override DataRow GetCyberRow()
        {
            if (REOCOD_0 == 0 && C_ITEM_GROUP != Attrezzature.__TOOL)
            {
                if (ITMREF_0 != "WI0000-00")
                {
                    //se REOCOD_0==0 (Tipo proposta dell'articolo sito, significa che NON c'è l'articolo sito ITALIA
                    __bulk_message += Utils.NewLineMail() + "articolo " + ITMREF_0 + " senza Articolo-sito ITALIA";
                }
                return null;
            }
            DataRow _tablerow = _dataTable.NewRow();

            _tablerow[0]  = C_CODE              ;              // varchar 50		 C_CODE 
            _tablerow[1]  = C_PLANT_CODE        ;              // varchar 20		 C_PLANT_CODE 
            _tablerow[2]  = C_DESCR             ;              // varchar 50		 C_DESCR 
            _tablerow[3]  = C_M_B               ;              // char 1  o APPROVIGIONAMENTO?		 C_M_B 
            _tablerow[4]  = C_PHANTOM           ;              // bit 		 C_PHANTOM = YLIVTRAS_0 =
            _tablerow[5]  = C_FIXED_LEAD_TIME   ;              // int 		 C_FIXED_LEAD_TIME 
            _tablerow[6]  = C_VAR_LEAD_TIME     ;              // real 		 C_VAR_LEAD_TIME
            _tablerow[7]  = C_BUY_PREPROC_LT    ;              // real 		 C_BUY_PREPROC_LT 
            _tablerow[8]  = C_BUY_PROC_LT       ;              // real 		 C_BUY_PROC_LT 
            _tablerow[9]  = C_BUY_POSTPROC_LT   ;              // real 		 C_BUY_POSTPROC_LT
            _tablerow[10] = C_UNAVAIL_TIME      ;             // real 		 C_UNAVAIL_TIME 
            _tablerow[11] = C_PROTECTION_TIME   ;             // real 		 C_PROTECTION_TIME 
            _tablerow[12] = C_UOM               ;             // varchar 3		 C_UOM
            _tablerow[13] = C_UOM2              ;             // varchar 3		 C_UOM2
            _tablerow[14] = C_LENGTH            ;             // real 		 C_LENGTH
            _tablerow[15] = C_WIDTH             ;             // real 		 C_WIDTH
            _tablerow[16] = C_SCRAP_TYPE        ;             // char 1		 C_SCRAP_TYPE 
            _tablerow[17] = C_PROCESS_SCRAP     ;             // real 		 C_PROCESS_SCRAP
            _tablerow[18] = C_STD_WAREHOUSE_CODE;             // varchar 20		 C_STD_WAREHOUSE_CODE 
            _tablerow[19] = C_ITEM_GROUP        ;             // varchar 8		 C_ITEM_GROUP 
            _tablerow[20] = C_MANAGER               ;         // varchar 20		 C_MANAGER
            _tablerow[21] = C_HOST_CODE             ;         // varchar 50		 C_HOST_CODE
            _tablerow[22] = C_SUPPLIER_CODE         ;         // varchar 30		 C_SUPPLIER_CODE
            _tablerow[23] = C_ABC_CLASS             ;         // char 1		 C_ABC_CLASS 
            _tablerow[24] = C_VALUE                 ;         // float			 C_VALUE 
            _tablerow[25] = C_COST                  ;         // float			 C_COST 
            _tablerow[26] = C_MRP_TYPE              ;         // char 1		 C_MRP_TYPE 
            _tablerow[27] = C_POQ_DAYS              ;         // int			 C_POQ_DAYS
            _tablerow[28] = C_POQ_HOURS             ;         // int			 C_POQ_HOURS
            _tablerow[29] = C_DTF                   ;         // int			 C_DTF 
            _tablerow[30] = C_LOT_SIZE              ;         // numeric			 C_LOT_SIZE 
            _tablerow[31] = C_MIN_ORDER_QTY         ;         // numeric			 C_MIN_ORDER_QTY 
            _tablerow[32] = C_MAX_ORDER_QTY         ;         // numeric			 C_MAX_ORDER_QTY 
            _tablerow[33] = C_DEM_GROUPING_QTY      ;         // numeric			 C_DEM_GROUPING_QTY
            _tablerow[34] = C_DEM_GROUPING_MAX_QTY  ;         // numeric			 C_DEM_GROUPING_MAX_QTY
            _tablerow[35] = C_ROP_QTY               ;         // numeric			 C_ROP_QTY 
            _tablerow[36] = C_SS_QTY                ;         // numeric			 C_SS_QTY 
            _tablerow[37] = C_USE_UNPEGGED_QTY      ;         // bit			 C_USE_UNPEGGED_QTY 
            _tablerow[38] = C_PROD_LOT_QTY;          //float Qta prelievo anticipato
            _tablerow[39] = C_USER_INT01            ;         // int			 C_USER_INT01 
            _tablerow[40] = C_USER_INT02            ;         // int			 C_USER_INT02 
            _tablerow[41] = C_USER_INT03            ;         // int			 C_USER_INT03 
            _tablerow[42] = C_USER_INT04            ;         // int			 C_USER_INT04 
            _tablerow[43] = C_USER_INT05            ;         // int			 C_USER_INT05 
            _tablerow[44] = C_USER_INT06            ;         // int			 C_USER_INT06 
            _tablerow[45] = C_USER_INT07            ;         // int			 C_USER_INT07 
            _tablerow[46] = C_USER_INT08            ;         // int			 C_USER_INT08 
            _tablerow[47] = C_USER_INT09            ;         // int			 C_USER_INT09 
            _tablerow[48] = C_USER_INT10            ;         // int			 C_USER_INT10 
            _tablerow[49] = C_USER_REAL01           ;         // float			 C_USER_REAL01 
            _tablerow[50] = C_USER_REAL02           ;         // float			 C_USER_REAL02 
            _tablerow[51] = C_USER_REAL03           ;         // float			 C_USER_REAL03 
            _tablerow[52] = C_USER_REAL04           ;         // float			 C_USER_REAL04 
            _tablerow[53] = C_USER_REAL05           ;         // float			 C_USER_REAL05 
            _tablerow[54] = C_USER_CHAR01           ;         // char 1		 C_USER_CHAR01 
            _tablerow[55] = C_USER_CHAR02           ;         // char 1		 C_USER_CHAR02 
            _tablerow[56] = C_USER_CHAR03           ;         // char 1		 C_USER_CHAR03 
            _tablerow[57] = C_USER_CHAR04           ;         // char 1		 C_USER_CHAR04 
            _tablerow[58] = C_USER_CHAR05           ;         // char 1		 C_USER_CHAR05 
            _tablerow[59] = C_USER_FLAG01           ;         // bit			 C_USER_FLAG01 
            _tablerow[60] = C_USER_FLAG02           ;         // bit			 C_USER_FLAG02 
            _tablerow[61] = C_USER_FLAG03           ;         // bit			 C_USER_FLAG03 
            _tablerow[62] = C_USER_STRING01         ;         // varchar 29		 C_USER_STRING01 
            _tablerow[63] = C_USER_STRING02         ;         // varchar 29		 C_USER_STRING02 
            _tablerow[64] = C_USER_STRING03         ;         // varchar 29		 C_USER_STRING03 
            _tablerow[65] = C_USER_STRING04         ;         // varchar 29		 C_USER_STRING04 
            _tablerow[66] = C_USER_STRING05         ;         // varchar 29		 C_USER_STRING05 
            _tablerow[67] = C_USER_STRING06         ;         // varchar 29		 C_USER_STRING06 
            _tablerow[68] = C_USER_STRING07         ;         // varchar 29		 C_USER_STRING07 
            _tablerow[69] = C_USER_STRING08         ;         // varchar 29		 C_USER_STRING08 
            _tablerow[70] = C_USER_STRING09         ;         // varchar 29		 C_USER_STRING09 
            _tablerow[71] = C_USER_STRING10         ;         // varchar 29		 C_USER_STRING10 
            _tablerow[72] = C_USER_NOTE01           ;         // varchar 99		 C_USER_NOTE01 
            _tablerow[73] = C_USER_COLOR01          ;         // int			 C_USER_COLOR01 
            _tablerow[74] = C_USER_COLOR02          ;         // int			 C_USER_COLOR02 
            _tablerow[75] = DateTime_toCyb(C_USER_DATE01);    // datetime			 C_USER_DATE01 
            _tablerow[76] = DateTime_toCyb(C_USER_DATE02);    // datetime			 C_USER_DATE02 
            _tablerow[77] = DateTime_toCyb(C_USER_DATE03);    // datetime			 C_USER_DATE03 
            _tablerow[78] = DateTime_toCyb(C_USER_DATE04);    // datetime			 C_USER_DATE04 
            _tablerow[79] = DateTime_toCyb(C_USER_DATE05);    // datetime			 C_USER_DATE05 
            

            return _tablerow;
        }

        private char getTipoProposta(byte REOCOD_0)
        {
            char ret = ' '; //M= produzione B= acquisto D= contolavoro
            switch (REOCOD_0)
            {
                case 1: ret = ' '; break;
                case 2: ret = 'B'; break;
                case 3: ret = 'M'; break;
                case 4: ret = '?'; break; //acquisto intersito
                case 5: ret = 'D'; break;//contolavoro
                default: ret = ' '; break;
            }
            if (ret == ' ' || ret == '?')
            {
                //mandare mail
                __bulk_message += Utils.NewLineMail() + "articolo " + ITMREF_0 + " con tipo Proposta non prevista in Articolo-sito ITALIA";
            }
            return ret;
        }

        private string get_MagazzinoRicevimento()
        {
            if (C_CODE == "SRC3500000-G" || C_CODE == "BKI" || C_CODE == "WB0021" || C_CODE == "WM0539")
            {
                ;
            }
            string ret = "";
            for (int i = 0; i < DEFLOC.Length; i++)
            {
                if (LOCNUM[i] == 1) //LOCNUM 1=Ricevimento 2=Magazzino 3=Picking 6=Spedizione
                {
                    ret = DEFLOC[i];
                    if (ret == "MAG01")
                        ret = __MAGAZZINO_INTERNO;
                    break;
                }
            }
            if (ret.Trim() == "")
            {
                if (TCLCOD_0 != "0PHA")
                {
                    //per adesso forziamo sempre il magazzino interno, a meno che non siano phantom
                    ret = __MAGAZZINO_INTERNO;
                }
                else
                {
                    ret = __MAGAZZINO_INTERNO;
                    // __bulk_message += "Articolo " + C_CODE + " non ha magazzino di ricevimento (vedi articolo sito)" + System.Environment.NewLine;
                }
            }
            return ret.Trim();
        }

        public override string GetSelectQuery(bool mode, string dossier, string codice_like, string tipo)
        {
            //if (datiContoLavoro == null)
            //{
            //    datiContoLavoro = DatiArticoliContolavoro(dossier);
            //}
            //metodo statico creato per poter essere richiamato senza modificare tabella statica _dataTable
            return SelectQuery(mode, dossier, codice_like, tipo);
        }
        
        public override string GetID()
        {
            return  ITMREF_0;
        }

        public override void InitDataTable()
        {
            _dataTable.Columns.Add("C_CODE",  typeof(string));             // varchar 50
            _dataTable.Columns.Add("C_PLANT_CODE",  typeof(string));       // varchar 20
            _dataTable.Columns.Add("C_DESCR",  typeof(string));            // varchar 50
            _dataTable.Columns.Add("C_M_B",  typeof(char));                // char 1  o APPROVVIGIONAMENTO?
            _dataTable.Columns.Add("C_PHANTOM",  typeof(int));             // bit 
            _dataTable.Columns.Add("C_FIXED_LEAD_TIME",  typeof(int));     // int 
            _dataTable.Columns.Add("C_VAR_LEAD_TIME",  typeof(double));    // real 
            _dataTable.Columns.Add("C_BUY_PREPROC_LT",  typeof(double));   // real 
            _dataTable.Columns.Add("C_BUY_PROC_LT",  typeof(double));      // real 
            _dataTable.Columns.Add("C_BUY_POSTPROC_LT",  typeof(double));  // real 
            _dataTable.Columns.Add("C_UNAVAIL_TIME",  typeof(double));     // real 
            _dataTable.Columns.Add("C_PROTECTION_TIME",  typeof(double));  // real 
            _dataTable.Columns.Add("C_UOM",  typeof(string));              // varchar 3
            _dataTable.Columns.Add("C_UOM2",  typeof(string));             // varchar 3
            _dataTable.Columns.Add("C_LENGTH",  typeof(double));           // real 
            _dataTable.Columns.Add("C_WIDTH",  typeof(double));            // real 
            _dataTable.Columns.Add("C_SCRAP_TYPE",  typeof(char));         // char 1
            _dataTable.Columns.Add("C_PROCESS_SCRAP",  typeof(double));    // real 
            _dataTable.Columns.Add("C_STD_WAREHOUSE_CODE",  typeof(string));  // varchar 20
            _dataTable.Columns.Add("C_ITEM_GROUP",  typeof(string));    // varchar 8
            _dataTable.Columns.Add("C_MANAGER",  typeof(string));             // varchar 20
            _dataTable.Columns.Add("C_HOST_CODE",  typeof(string));            // varchar 50
            _dataTable.Columns.Add("C_SUPPLIER_CODE",  typeof(string));        // varchar 30
            _dataTable.Columns.Add("C_ABC_CLASS",  typeof(char));         // char 1
            _dataTable.Columns.Add("C_VALUE",  typeof(float));            // float	
            _dataTable.Columns.Add("C_COST",  typeof(float));             // float	
            _dataTable.Columns.Add("C_MRP_TYPE",  typeof(char));             // char 1
            _dataTable.Columns.Add("C_POQ_DAYS",  typeof(int));             // int	
            _dataTable.Columns.Add("C_POQ_HOURS",  typeof(int));            // int	
            _dataTable.Columns.Add("C_DTF",  typeof(int));             // int	
            _dataTable.Columns.Add("C_LOT_SIZE",  typeof(decimal));            //YQTAMUL_0; // numeric	
            _dataTable.Columns.Add("C_MIN_ORDER_QTY",  typeof(decimal));            // numeric	
            _dataTable.Columns.Add("C_MAX_ORDER_QTY",  typeof(decimal));            // numeric	
            _dataTable.Columns.Add("C_DEM_GROUPING_QTY",  typeof(decimal));            // numeric	
            _dataTable.Columns.Add("C_DEM_GROUPING_MAX_QTY",  typeof(decimal));        // numeric	
            _dataTable.Columns.Add("C_ROP_QTY",  typeof(decimal));           // numeric	
            _dataTable.Columns.Add("C_SS_QTY",  typeof(decimal));            // numeric	
            _dataTable.Columns.Add("C_USE_UNPEGGED_QTY",  typeof(byte));       // bit	
            _dataTable.Columns.Add("C_PROD_LOT_QTY", typeof(float));             // qta prelievo anticipato per overlap ordini	
            _dataTable.Columns.Add("C_USER_INT01",  typeof(int));             // int	
            _dataTable.Columns.Add("C_USER_INT02",  typeof(int));             // int	
            _dataTable.Columns.Add("C_USER_INT03",  typeof(int));             // int	
            _dataTable.Columns.Add("C_USER_INT04",  typeof(int));             // int	
            _dataTable.Columns.Add("C_USER_INT05",  typeof(int));             // int	
            _dataTable.Columns.Add("C_USER_INT06",  typeof(int));             // int	
            _dataTable.Columns.Add("C_USER_INT07",  typeof(int));             // int	
            _dataTable.Columns.Add("C_USER_INT08",  typeof(int));             // int	
            _dataTable.Columns.Add("C_USER_INT09",  typeof(int));             // int	
            _dataTable.Columns.Add("C_USER_INT10",  typeof(int));             // int	
            _dataTable.Columns.Add("C_USER_REAL01",  typeof(float));           // float	
            _dataTable.Columns.Add("C_USER_REAL02",  typeof(float));           // float	
            _dataTable.Columns.Add("C_USER_REAL03",  typeof(float));           // float	
            _dataTable.Columns.Add("C_USER_REAL04",  typeof(float));           // float	
            _dataTable.Columns.Add("C_USER_REAL05",  typeof(float));           // float	
            _dataTable.Columns.Add("C_USER_CHAR01",  typeof(char));             // char 1
            _dataTable.Columns.Add("C_USER_CHAR02",  typeof(char));             // char 1
            _dataTable.Columns.Add("C_USER_CHAR03",  typeof(char));             // char 1
            _dataTable.Columns.Add("C_USER_CHAR04",  typeof(char));             // char 1
            _dataTable.Columns.Add("C_USER_CHAR05",  typeof(char));             // char 1
            _dataTable.Columns.Add("C_USER_FLAG01",  typeof(byte));            // bit	
            _dataTable.Columns.Add("C_USER_FLAG02",  typeof(byte));            // bit	
            _dataTable.Columns.Add("C_USER_FLAG03",  typeof(byte));            // bit	
            _dataTable.Columns.Add("C_USER_STRING01",  typeof(string));          // varchar 29
            _dataTable.Columns.Add("C_USER_STRING02",  typeof(string));          // varchar 29
            _dataTable.Columns.Add("C_USER_STRING03",  typeof(string));          // varchar 29
            _dataTable.Columns.Add("C_USER_STRING04",  typeof(string));          // varchar 29
            _dataTable.Columns.Add("C_USER_STRING05",  typeof(string));          // varchar 29
            _dataTable.Columns.Add("C_USER_STRING06",  typeof(string));          // varchar 29
            _dataTable.Columns.Add("C_USER_STRING07",  typeof(string));          // varchar 29
            _dataTable.Columns.Add("C_USER_STRING08",  typeof(string));          // varchar 29
            _dataTable.Columns.Add("C_USER_STRING09",  typeof(string));          // varchar 29
            _dataTable.Columns.Add("C_USER_STRING10",  typeof(string));          // varchar 29
            _dataTable.Columns.Add("C_USER_NOTE01",  typeof(string));            // varchar 99
            _dataTable.Columns.Add("C_USER_COLOR01",  typeof(int));              // int	
            _dataTable.Columns.Add("C_USER_COLOR02",  typeof(int));              // int	
            _dataTable.Columns.Add("C_USER_DATE01",  typeof(DateTime));          // datetime	
            _dataTable.Columns.Add("C_USER_DATE02",  typeof(DateTime));          // datetime	
            _dataTable.Columns.Add("C_USER_DATE03",  typeof(DateTime));          // datetime	
            _dataTable.Columns.Add("C_USER_DATE04",  typeof(DateTime));          // datetime	
            _dataTable.Columns.Add("C_USER_DATE05",  typeof(DateTime));          // datetime	            
        }

        protected decimal getQtaMIN(string articolo, string categoria, string YLIVTRAS_0, decimal QTAMIN)
        {
            if (getMrpType(ITMREF_0, TCLCOD_0, STDFLG_0) == 'C')
                return 0;   //se l'articolo è a commessa va lasciato a zero

            decimal ret = QTAMIN;
            if (YLIVTRAS_0 == "PF")
            {
                ret = 0;
            }
            return ret;
        }
        protected decimal getQtaMAX(string articolo, string categoria, string YLIVTRAS_0, decimal QTAMAX)
        {
            if (getMrpType(ITMREF_0, TCLCOD_0, STDFLG_0) == 'C')
                return 0;   //se l'articolo è a commessa va lasciato a zero

            decimal ret = QTAMAX;
            if (YLIVTRAS_0 == "PF")
            {
                ret = 0;
            }
            return ret;
        }
        protected decimal getQtaLOT(string articolo, string categoria, string YLIVTRAS_0, decimal QTALOT)
        {
            if (getMrpType(ITMREF_0, TCLCOD_0, STDFLG_0)=='C')
                return 0;   //se l'articolo è a commessa va lasciato a zero

            decimal ret = QTALOT;
            if (YLIVTRAS_0 == "PF")
            {
                ret = 1;
            }
            return ret;
        }
        protected char getMrpType(string articolo,string categoria, int GestioneSage)
        {
            char ret = 'F'; //a fabbisogno
            if (GestioneSage == 4) //a commessa
            {
                ret = 'C'; // a Commessa
            }
            return ret;
        }
        protected float getPeso(string reparto,string articolo, decimal peso, string udm)
        {
            if (reparto == "PLAS")
            {
                if (peso == 0)
                {
                    //CE NE SONO TROPPI__bulk_message += Utils.NewLineMail()+ articolo + " (corpo plastico) non ha il peso";
                }

                if (udm == "GR")
                {
                    return (float)peso;
                }
                else
                {
                    __bulk_message += Utils.NewLineMail() + articolo + " (corpo plastico) non ha il peso espresso in grammi; sistema anagrafica in sage";
                    return 0;
                }
            }
            else
            {
                if (udm == "GR")
                {
                    return (float)peso;
                }
                else if (udm == "KG")
                {
                    return (float)peso*1000;
                }
            }
            return (float)peso;
        }
        protected int getMarchio(string tagoem)
        {
            int ret = -1;

            //1 Sauro
            //2 Tyco
            //3 Harting
            if (tagoem != null)
            {
                string tmp = tagoem.Trim();

                switch (tagoem)
                {
                    case "1": ret = 1; break;
                    case "2": ret = 2; break;
                    case "3": ret = 3; break;
                    default: ret = -1; break;
                }
            }
            else
            {
                __bulk_message += Utils.NewLineMail() + " articolo " + ITMREF_0 + " Forse manca record corrispondente in YITMINF!";
            }
            return ret;
        }
        protected char getFAMP_OLD(string cod_Articolo, string temp_desc_da_togliere)
        {
            if (temp_desc_da_togliere.Contains(" AF "))
            {
                return 'A';
            }
            else if (temp_desc_da_togliere.Contains(" POL "))
            {
                return 'P';
            }
            else if (temp_desc_da_togliere.Contains(" FL "))
            {
                return 'F';
            }
            else if (temp_desc_da_togliere.Contains(" MOD "))
            {
                return 'M';
            }
            return ' ';
        }
        protected char getSTH(string categoria,string Cod_materiale)
        {
            char ret = ' ';
            if (categoria.Trim() == "1GRA" || categoria.Trim() == "3PLA" || categoria.Trim() == "WP")
            {
                if (Cod_materiale!= null)
                {
                    if (Cod_materiale.Trim() == "104" || Cod_materiale.Trim() == "105" || Cod_materiale.Trim() == "106")
                        ret = 'S';  //sth
                    else ret = 'P';  //Pa66 o ABS
                }
            }
            return ret;
        }
        protected string getReparto(string YWCR)
        {
            if (string.IsNullOrWhiteSpace(YWCR))
            {
                __bulk_message += Utils.NewLineMail() + "articolo "+ ITMREF_0 + " senza reparto";
                return __TRASH__;
            }
            else
            {
                if(
                    !(YWCR_0 == "ASSE" || YWCR_0 == "PLAS" || YWCR_0 == "CL" || YWCR_0 == "CTAPE" || 
                    YWCR_0 == "FILO" || YWCR_0 == "MORS" || YWCR_0 == "TERM" || YWCR_0 == "VITI" || YWCR_0 == "NOGES")
                    )
                __bulk_message += Utils.NewLineMail() + "articolo " + ITMREF_0 + " con reparto non previsto: " + YWCR_0;
                return YWCR;
            }

            //int i;
            //if (categoria.Trim()=="3PLA" || categoria.Trim() == "WP")
            //    return "PLAS";
            //else if (categoria.Trim() == "3MET" || categoria.Trim() == "WM")
            //    return "MORS";
            //else if (categoria.Trim() == "3SEM")
            //    return "ASSE";
            //else if(!int.TryParse(categoria.Substring(0,1), out i))  //se non inizia con un numero
            //    return "ASSE";

            //return categoria.Substring(0,4);
        }
        protected float GetPesoMaterozza(string articolo, string reparto, decimal pesomaterozza)
        {
            if (reparto == "PLAS" && pesomaterozza == 0)
            {
                //CE NE SONO TROPPI __bulk_message += Utils.NewLineMail() + "articolo " + articolo + " non ha peso materozza";
            }
            return (float)YPESMAT_0;
        }

        public override void LastAction(ref DBHelper2 cm, DBHelper2 sage)
        {
            __bulk_message += CheckArticoliContolavoro(sage);
            Utils.SendMail_Plan(Settings.GetSettings(), __bulk_message);
        }

        static public string SelectQuery(bool mode, string dossier, string codice_like, string tipo)
        {
            string db = "x3." + dossier;
            //string sage_query = "SELECT A.ITMREF_0, ITMSTA_0 from " + dossier + ".[ITMMASTER] A join " + dossier + ".[YITMINF] B on A.ITMREF_0=B.ITMREF_0" +
            //    " WHERE YLIVTRAS_0='" + tipo + "' and ITMSTA=1 ";

            string sage_query =
  @"SELECT 
  I.ITMREF_0
 ,I.ITMDES1_0
 ,I.STU_0
 ,I.PUU_0
 ,I.SAU_0
 ,I.TCLCOD_0
 ,Y.YLIVTRAS_0
 ,Y.YFAMCOM_0
 ,'ex F.YPRDAPP_0' as ggg
 ,F.REOCOD_0
 ,F.PRPLTI_0
 ,F.SHR_0
 ,F.DEFLOCTYP_0 
 ,F.DEFLOCTYP_1 
 ,F.DEFLOCTYP_2 
 ,F.DEFLOCTYP_3 
 ,F.DEFLOCTYP_4 
 ,F.DEFLOCTYP_5 
 ,F.DEFLOCTYP_6 
 ,F.DEFLOCTYP_7 
 ,F.DEFLOCTYP_8 
 ,F.DEFLOCTYP_9 
 ,F.DEFLOCTYP_10 
 ,F.DEFLOCTYP_11 
 ,F.DEFLOCTYP_12 
 ,F.DEFLOCTYP_13 
 ,F.DEFLOCTYP_14 
 ,F.DEFLOC_0  
 ,F.DEFLOC_1  
 ,F.DEFLOC_2  
 ,F.DEFLOC_3  
 ,F.DEFLOC_4  
 ,F.DEFLOC_5  
 ,F.DEFLOC_6  
 ,F.DEFLOC_7  
 ,F.DEFLOC_8  
 ,F.DEFLOC_9  
 ,F.DEFLOC_10 
 ,F.DEFLOC_11 
 ,F.DEFLOC_12 
 ,F.DEFLOC_13 
 ,F.DEFLOC_14 
 ,F.YQTAMIN_0
 ,F.YQTAMAX_0
 ,F.YQTAMUL_0
 ,F.REOTSD_0
 ,F.SAFSTO_0
 ,F.MAXSTO_0
 ,F.REOMINQTY_0
 ,F.MFGLOTQTY_0
 ,F.MFGLTI_0   
 ,F.QUALTI_0   
 ,F.OFS_0      
 ,F.LOCNUM_0  
 ,F.LOCNUM_1  
 ,F.LOCNUM_2  
 ,F.LOCNUM_3  
 ,F.LOCNUM_4  
 ,F.LOCNUM_5  
 ,F.LOCNUM_6  
 ,F.LOCNUM_7  
 ,F.LOCNUM_8  
 ,F.LOCNUM_9  
 ,F.LOCNUM_10 
 ,F.LOCNUM_11 
 ,F.LOCNUM_12 
 ,F.LOCNUM_13 
 ,F.LOCNUM_14 
 ,Y.YNUMVIE_0
 ,Y.YCORPOALT_0
 ,Y.YMRPTAG1_0

 ,Y.YCORPOLUN_0
 ,Y.YCORPOPRO_0
 ,Y.YMAT_0           
 ,Y.YTAGOEM_0
 ,I.WEU_0
 ,I.ITMWEI_0
 ,Y.YCOLORE_0
 ,'era F.YBPSNUM_0'
 ,Y.YPESMAT_0
 ,Y.YPASSOVIE_0
 ,I.STDFLG_0
 ,Y.YCONFQTA_0
 ,Y.YQTADECIMI_0
 ,F.YWCR_0
 ,F.YQTAPREANT_0
 ,FAMPEX.FAMPEX_0
 ,Y.YSTAMPCOLORE_0
 ,Y.YPIEFLG_0
, I.PHAFLG_0
  from " + db + ".ITMMASTER I \n" +
                " left join " + db + ".YITMINF Y on I.ITMREF_0 = Y.ITMREF_0 \n" +
                " left join " + db + ".ITMFACILIT F on I.ITMREF_0 = F.ITMREF_0 and F.STOFCY_0 = 'ITS01' \n" +
                " left join " + db + ".YITMVP FAMPEX on FAMPEX.ITMREF_0 = I.ITMREF_0 \n" +
                ////" left join " +
                ////" ( " +
                ////"     select ITMREF_0, BPSNUM_0 from SAURO.ITMBPS where ROWID in " +
                ////"         (select  min(ROWID)from SAURO.ITMBPS where BPSNUM_0 <> 'A000818' group by ITMREF_0) " +
                ////"  ) B " +
                ////"  on B.ITMREF_0 = I.ITMREF_0 " +


 //               " LEFT JOIN " +
 //               " ( \n" +
 //               " select ITMREF_0,                         \n" +
 //               " case YVALCOD_0                           \n" +
 //               " when '028' then 'F'                      \n" +
 //               " when '006' then 'A'                      \n" +
 //               " when '038' then 'M'                      \n" +
 //               " when '051' then 'P'                      \n" +
 //               " end AS FAMP                              \n" +
 //               " from " + db + ".ITMMASTER M              \n" +
 //               " join " + db + ".YITMVAL Y                \n" +
 //               " on Y.YVALITMREF_0 = M.ITMREF_0           \n" +
 //               " where                                    \n" +
 //               "     Y.YVALCOD_0 = '028' or               \n" +
 //               "     Y.YVALCOD_0 = '006' or               \n" +
 //               "     Y.YVALCOD_0 = '038' or               \n" +
 //               "     Y.YVALCOD_0 = '051'                  \n" +
 //               " ) AS FAMP ON FAMP.ITMREF_0 = I.ITMREF_0  \n" +




            " WHERE I.ITMSTA_0=1 \n" +
                " and I.ITMREF_0 not like 'WU%' \n" +
                //" and I.ITMREF_0 not like 'WWACQ%' \n" +
                //" and I.ITMREF_0 not like 'WWVEN%' \n" +
                //" and I.ITMREF_0 not like 'WWDPI%' \n" +
                " and I.ITMREF_0 not like 'WW%' \n"
            ;

            if (!string.IsNullOrWhiteSpace(codice_like))
            {
                sage_query += " and I.ITMREF_0 like '" + codice_like.Trim() + "' \n";
            }

            sage_query += " ORDER BY I.ITMREF_0 ";
            return sage_query;
        }

        static public string CheckArticoliContolavoro(DBHelper2 sage)
        {
            /*Recupero articoli contolavoro solo per check coerenza*/
            bool first = true;
            string ret1 = "";
            //DBHelper2 sage = DBHelper2.getSageDBHelper(dossier);
            _logger.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name + " start...");
            string db = "x3." + sage.LibreriaDossier;
            string qry = @"select 
  P.ITMREF_0
, F.REOCOD_0
, P.YATTCOD_0
, A.YATTLEATIM_0
, AM.MAC_0
, AM.MACREP_0
, AM.MACMRPCDL_0
, AM.BPS_0
, AM.CONFGRUPPO_0
--, YCONCAD_0, YCONCADTIM_0,YCONENAFLG_0, MAC_0, MACREP_0, MACMRPCDL_0, BPS_0, MACOBSO_0 
from " + db + @".YPRDITM P
join " + db + @".ITMMASTER M
	on M.ITMREF_0 = P.ITMREF_0
join " + db + @".YPRDATT A 
	on P.YATTCOD_0 = A.YATTCOD_0
join " + db + @".YPRDAM AM on AM.YCONATT_0= A.YATTCOD_0 
join " + db + @".ITMFACILIT F on F.ITMREF_0=P.ITMREF_0   ";

            string where= @"where M.ITMSTA_0=1 and P.YENAFLG_0=2 and A.YATTENAFLG_0=2 and AM.YCONENAFLG_0=2 and
( 	AM.MACMRPCDL_0 like 'CL%' or AM.MACMRPCDL_0 like 'ASSE%' or AM.MACMRPCDL_0 like 'PLAE%' or AM.MACMRPCDL_0 like 'CTAE%' )
and F.STOFCY_0 ='ITS01' and REOCOD_0<>5";



            DbDataReader dtr = sage.GetReaderSelectCommand(qry + where + " order by P.ITMREF_0");
            object[] row = new object[dtr.FieldCount];
            while (dtr.Read())
            {
                dtr.GetValues(row);
                string itmref = (string)row[0];
                string attr = (string)row[2];
                string macc = (string)row[4];
                ret1 += Utils.NewLineMail() + "articolo ATTIVO con attrezzatura di contolavoro (" + attr + " su " + macc + ") ma tipo proposta diversa da contolavoro: " + itmref;
            }










            where = @"where M.ITMSTA_0=1 and P.YENAFLG_0=2 and A.YATTENAFLG_0=2 and AM.YCONENAFLG_0=2 and NOT
( 	AM.MACMRPCDL_0 like 'CL%' or AM.MACMRPCDL_0 like 'ASSE%' or AM.MACMRPCDL_0 like 'PLAE%' or AM.MACMRPCDL_0 like 'CTAE%' )
and F.STOFCY_0 ='ITS01' and REOCOD_0=5";


            dtr = sage.GetReaderSelectCommand(qry + where + " order by P.ITMREF_0");
            row = new object[dtr.FieldCount];
            while (dtr.Read())
            {
                dtr.GetValues(row);
                string itmref = (string)row[0];
                string attr = (string)row[2];
                string macc = (string)row[4];
                if (first)
                {
                    ret1 += Utils.NewLineMail() + Utils.NewLineMail();
                    first = false;
                }
                ret1 += Utils.NewLineMail() + "articolo ATTIVO con attrezzatura NON di contolavoro (" + attr + " su " + macc + ") ma tipo proposta di contolavoro: " + itmref;
            }
            first = true;






            where = @"where M.ITMSTA_0=1 and P.YENAFLG_0 = 2 and A.YATTENAFLG_0 = 2 and AM.YCONENAFLG_0 = 2 and
      --not(AM.MACMRPCDL_0 like 'CL%' or AM.MACMRPCDL_0 like 'ASSE%' or AM.MACMRPCDL_0 like 'PLAE%' or AM.MACMRPCDL_0 like 'CTAE%')
F.STOFCY_0 = 'ITS01'
and REOCOD_0<>5
and REOCOD_0<>3 ";

            dtr = sage.GetReaderSelectCommand(qry + where + " order by P.ITMREF_0");
            row = new object[dtr.FieldCount];
            while (dtr.Read())
            {
                dtr.GetValues(row);
                string itmref = (string)row[0];
                string attr = (string)row[2];
                string macc = (string)row[4];
                if (first)
                {
                    ret1 += Utils.NewLineMail() + Utils.NewLineMail();
                    first = false;
                }
                ret1 += Utils.NewLineMail() + "articolo ATTIVO associato ad attrezzatura (" + attr + " su " + macc + ") ma ha come tipo proposta né Contolavoro né Produzione: " + itmref;
            }
            first = true;



            qry = @"select
  M.ITMREF_0
, F.REOCOD_0
, YLIVTRAS_0
, P.YATTCOD_0

from x3.SAUROTEST.ITMMASTER M
join x3.SAUROTEST.YITMINF Y on M.ITMREF_0 = Y.ITMREF_0
join x3.SAUROTEST.ITMFACILIT F  on M.ITMREF_0 = F.ITMREF_0
full join x3.SAUROTEST.YPRDITM P on F.ITMREF_0 = P.ITMREF_0 ";

            where = @" where F.STOFCY_0 = 'ITS01' and ITMSTA_0 = 1
 -- and YLIVTRAS_0<>'MP' and YLIVTRAS_0<> 'SF' and YLIVTRAS_0<> 'IM' and YLIVTRAS_0<> 'ME' and YLIVTRAS_0<> 'MC' and YLIVTRAS_0<> 'MK'
and(YLIVTRAS_0 = 'SL' or YLIVTRAS_0 = 'PF' or YLIVTRAS_0 = 'SC')
--P.YENAFLG_0 = 2 and
  and P.YATTCOD_0 is not null
and not (REOCOD_0 = 3 or REOCOD_0 = 5) ";

            dtr = sage.GetReaderSelectCommand(qry + where + " order by M.ITMREF_0");
            row = new object[dtr.FieldCount];
            while (dtr.Read())
            {
                dtr.GetValues(row);
                string itmref = (string)row[0];
                if (first)
                {
                    ret1 += Utils.NewLineMail() + Utils.NewLineMail();
                    first = false;
                }
                ret1 += Utils.NewLineMail() + "articolo ATTIVO associato ad attrezzatura ma NON ha come tipo proposta Contolavoro o Produzione: " + itmref;
            }
            first = true;


            /*  CONTROLLO GIà fatto in ITME ROUTING
             *  
            where = @" where F.STOFCY_0 = 'ITS01' and ITMSTA_0 = 1
 -- and YLIVTRAS_0<>'MP' and YLIVTRAS_0<> 'SF' and YLIVTRAS_0<> 'IM' and YLIVTRAS_0<> 'ME' and YLIVTRAS_0<> 'MC' and YLIVTRAS_0<> 'MK'
and(YLIVTRAS_0 = 'SL' or YLIVTRAS_0 = 'PF' or YLIVTRAS_0 = 'SC')
--P.YENAFLG_0 = 2 and
  and P.YATTCOD_0 is null
and (REOCOD_0 = 3 or REOCOD_0 = 5) ";

            dtr = sage.GetReaderSelectCommand(qry + where + " order by M.ITMREF_0");
            row = new object[dtr.FieldCount];
            while (dtr.Read())
            {
                dtr.GetValues(row);
                string itmref = (string)row[0];
                if (first)
                {
                    ret1 += Utils.NewLineMail() + Utils.NewLineMail();
                    first = false;
                }
                ret1 += Utils.NewLineMail() + "articolo ATTIVO NON è associato ad attrezzatura ma ha come tipo proposta Contolavoro o Produzione: " + itmref;
            }
            first = true;
            */

            _logger.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name + " end");

            return ret1;
        }
        static public Dictionary<string, Tuple<short, string, string>> DatiArticoliContolavoro_old(string dossier)
        {
/*Recupero articoli contolavoro solo per check coerenza*/

            DBHelper2 sage = DBHelper2.getSageDBHelper(dossier);
            _logger.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name + " start...");
            string db = "x3." + dossier;
            string qry =
@"select 
  P.ITMREF_0
, P.YATTCOD_0
, A.YATTLEATIM_0
, C.YCONCDL_0
, W.WCR_0
, W.YMRPCDL_0
, W.YBPS_0 
from " + db + @".YPRDITM P
join " + db + @".YPRDATT A 
	on P.YATTCOD_0 = A.YATTCOD_0
join " + db + @".YPRDCONF C 
	on C.YCONATT_0=A.YATTCOD_0
join " + db + @".WORKSTATIO W 
    on W.WST_0= C.YCONCDL_0
where P.YENAFLG_0=2 and A.YATTENAFLG_0=2 and C.YCONENAFLG_0=2 and
(
	W.YMRPCDL_0 like 'CL%' 	or W.YMRPCDL_0 like 'ASSE%' or W.YMRPCDL_0 like 'PLAE%' or W.YMRPCDL_0 like 'CTAE%'
)"
;
            DbDataReader dtr = sage.GetReaderSelectCommand(qry);
            object[] row = new object[dtr.FieldCount];
            Dictionary<string, Tuple<short, string, string>> itm_CL= new Dictionary<string, Tuple<short, string, string>>(100);  //Tupla: leadTime, fornitore, cdl_mrp
            while (dtr.Read())
            {
                dtr.GetValues(row);
                string itmref = (string)row[0];
                if (itm_CL.ContainsKey(itmref) )
                {
                    if ((string)row[5]!="PLAE")
                    {
                        __bulk_message += Utils.NewLineMail() + " articolo " + itmref + " ha configurazioni multiple con attrezzature e macchine";
                    }
                }
                else
                {
                    itm_CL.Add(itmref, new Tuple<short, string, string>((short)row[2], (string)row[6], (string)row[5])); //  Articolo + (leadTime, Fornitore, cdl_mrp)
                }
            }
            _logger.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name + " end");
            return itm_CL;
        }
    }
}
