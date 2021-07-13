using System.Collections.Generic;
using System;
using log4net;
using System.Data;
using System.Data.Common;

namespace SyncCyberPlan_lib
{
    public class OrdiniAcq_OPR : OrdiniAcq
    {
        public string  H_MFGNUM_0;
        public string  H_MFGFCY_0;
        public string  H_YCYBCOD_0;
        public int     H_YCYBTSK_0;
        public string  I_ITMREF_0;
        public string  I_MFGFCY_0;
        public string  I_VCRNUMORI_0;  //-- valori di As400 da modificare con i legami in fase di import in Sage
        public int     I_VCRLINORI_0;  //-- valori di As400 da modificare con i legami in fase di import in Sage
        public DateTime?  H_CREDAT_0;  
        public byte      H_MFGSTA_0;     //--MFGSTA 1=Confermato  2= Pianificato  3=Suggerito 4=Chiuso
        public DateTime? H_STRDAT_0;  
        public DateTime? H_ENDDAT_0;  
        public byte    I_ITMSTA_0;      //-- 1= in attesa  2= in corso 3= saldato  4= Escluso
        public byte    I_MFITRKFLG_0;   //-- 1= in attesa  4= in corso 5= saldato
        public decimal I_EXTQTY_0;      //--qta prevista
        public decimal I_CPLQTY_0;      //--qta prodotta
        public decimal I_REJCPLQTY_0;   //-- qta scartata        
        public string  I_TCLCOD_0; 
        public int     I_MFGLIN_0;  
        public string  M_ITMREF_0;  
        public decimal M_RETQTY_0;       //--qta necessaria BOM
        public byte    O_SCOCOD_0;

        public string X3ORDNRLN;
        //-- public short   M_BOMSEQ_0;  
        //-- public decimal M_BOMQTY_0;  
        //public string ;                 //-- Buy Make o Decentrato


        public OrdiniAcq_OPR(/*string YPOHTYP*/): base("CYB_ORDER")
        {
            //__YPOHTYP_filter = YPOHTYP;
        }

        public override string GetSelectQuery(bool mode, string dossier, string codice_like, string filtro)
        {
            string _db= "x3." + dossier;
            
            string query = @"select 
  H.MFGNUM_0
, H.MFGFCY_0
, H.YCYBCOD_0
, H.YCYBTSK_0
, I.ITMREF_0
, I.MFGFCY_0
, I.VCRNUMORI_0 -- valori di As400 da modificare con i legami in fase di import in Sage
, I.VCRLINORI_0 -- valori di As400 da modificare con i legami in fase di import in Sage
, H.CREDAT_0
, H.MFGSTA_0   --MFGSTA 1=Confermato  2= Pianificato  3=Suggerito 4=Chiuso
, H.STRDAT_0
, H.ENDDAT_0
, I.ITMSTA_0    -- 1= in attesa  2= in corso 3= saldato  4= Escluso
, I.MFITRKFLG_0 -- 1= in attesa  4= in corso 5= saldato
, I.EXTQTY_0  --qta prevista
, I.CPLQTY_0  --qta prodotta
, I.REJCPLQTY_0 -- qta scartata
, I.TCLCOD_0
, I.MFGLIN_0
, M.ITMREF_0
, M.RETQTY_0 --qta necessaria
, O.SCOCOD_0 --flag contolavoro 1=no 2= strutturale 3= congiunturale
--, M.BOMSEQ_0
--, M.BOMQTY_0
-- Buy Make o Decentrato
      from " + _db + @".MFGHEAD H 
inner join " + _db + @".MFGITM I on H.MFGNUM_0 = I.MFGNUM_0
inner join " + _db + @".MFGMAT M on H.MFGNUM_0 = M.MFGNUM_0 and I.MFGLIN_0 = M.MFGLIN_0
inner join " + _db + @".MFGOPE O on H.MFGNUM_0 = O.MFGNUM_0
where
M.MFGSTA_0 = 1 and M.MFGSTA_0 = 1 and I.MFGSTA_0 = 1 "
                ;

            if (!string.IsNullOrWhiteSpace(codice_like))
            {
                query += " and I.ITMREF_0 like '" + codice_like.Trim() + "'";
            }

            query += " order by H.MFGNUM_0, I.ITMREF_0, M.BOMSEQ_0 ";
            return query;
        }

        public override void Init(object[] row)
        {
            H_MFGNUM_0    = getDBV<string >(row[0],  "H_MFGNUM_0");
            H_MFGFCY_0    = getDBV<string >(row[1],  "H_MFGFCY_0");
            H_YCYBCOD_0   = getDBV<string >(row[2],  "H_YCYBCOD_0");
            H_YCYBTSK_0   = getDBV<int    >(row[3],  "H_YCYBTSK_0");
            I_ITMREF_0    = getDBV<string >(row[4],  "I_ITMREF_0");
            I_MFGFCY_0    = getDBV<string >(row[5],  "I_MFGFCY_0");
            I_VCRNUMORI_0 = getDBV<string >(row[6],  "I_VCRNUMORI_0");
            I_VCRLINORI_0 = getDBV<int    >(row[7],  "I_VCRLINORI_0");
            H_CREDAT_0    = getSageDate    (row[8],  "H_CREDAT_0");
            H_MFGSTA_0    = getDBV<byte   >(row[9],  "H_MFGSTA_0");
            H_STRDAT_0    = getSageDate    (row[10], "H_STRDAT_0");
            H_ENDDAT_0    = getSageDate    (row[11], "H_ENDDAT_0");
            I_ITMSTA_0    = getDBV<byte   >(row[12], "I_ITMSTA_0");
            I_MFITRKFLG_0 = getDBV<byte   >(row[13], "I_MFITRKFLG_0");
            I_EXTQTY_0    = getDBV<decimal>(row[14], "I_EXTQTY_0");
            I_CPLQTY_0    = getDBV<decimal>(row[15], "I_CPLQTY_0");
            I_REJCPLQTY_0 = getDBV<decimal>(row[16], "I_REJCPLQTY_0");
            I_TCLCOD_0    = getDBV<string >(row[17], "I_TCLCOD_0");
            I_MFGLIN_0    = getDBV<int    >(row[18], "I_MFGLIN_0");
            M_ITMREF_0    = getDBV<string >(row[19], "M_ITMREF_0");
            M_RETQTY_0    = getDBV<decimal>(row[20], "M_RETQTY_0");
            O_SCOCOD_0    = getDBV<byte>   (row[21], "O_SCOCOD_0");

            X3ORDNRLN = "";
            if ((!String.IsNullOrEmpty(I_VCRNUMORI_0)) && (I_VCRLINORI_0 != null))
            {
                X3ORDNRLN = I_VCRNUMORI_0 + Convert.ToString(I_VCRLINORI_0).PadLeft(6, '0');
            }


            C_CODE                = EscapeSQL(H_MFGNUM_0, 30);        //varchar         30                
            //ATTENZIONE il valore "000000000000" indica valore default per ordini a fabbisogno
            //C_CORDER_CODE         = EscapeSQL(string.IsNullOrWhiteSpace(I_VCRNUMORI_0)? "00000000000000000000": I_VCRNUMORI_0 + I_VCRLINORI_0 , 30);   //varchar 30  
            C_CORDER_CODE = EscapeSQL(string.IsNullOrWhiteSpace(I_VCRNUMORI_0) ? "00000000000000000000" : X3ORDNRLN, 30);   //varchar 30  
            C_ITEM_CODE           = EscapeSQL(I_ITMREF_0, 50);                                        //varchar         50                      
            C_ITEM_PLANT          = EscapeSQL("ITS01", 20);                                        //varchar         20                      
            C_M_B                 = get_C_M_B(O_SCOCOD_0);                                    //char             1     // B=buy D=decentrato M = make                
            C_MRP_TYPE            = getMRP_type(I_VCRNUMORI_0);                                          //char             1     //F=MTS make to stock (a fabbisogno)  C= MTO make to order (a commessa)                     
            C_QTY                 = I_EXTQTY_0;                                                       //numeric            
            C_COMPL_QTY           = I_CPLQTY_0;                                                       //numeric            
            C_SCRAP_QTY           = I_REJCPLQTY_0;                                                       //numeric            qta scartata
            C_HOST_QTY            = 0;                                                             //numeric            
            C_INSERT_DATE         = H_CREDAT_0;                                                   //datetime           
            C_HOST_STDATE         = H_STRDAT_0;                                                   //datetime           //data inizio           
            C_HOST_DUEDATE        = H_STRDAT_0 > H_ENDDAT_0? H_STRDAT_0 :H_ENDDAT_0;  //datetime  data fine      Qualche volta in As400 c'è data inizio maggiore di data fine; in tal caso le mettiamo uguali
            C_PROMISE_DATE        = null;                                                          //datetime           
            C_ACT_STDATE          = null;                                                          //datetime           
            C_ACT_DUEDATE         = null;                                                          //datetime           
            C_SHOP_FLOOR_CODE     = EscapeSQL("", 20);                                             //varchar         20                      
            C_STATUS              = I_MFITRKFLG_0 == 4 ? 6 : 4;                                        //int  in Cyber 6 iniziato, 4 confermato e fattibile  //IN SAGE  I_MFITRKFLG_0 -- 1= in attesa  4= in corso 5= saldato
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
        protected char get_C_M_B(byte O_SCOCOD_0)
        {
            if (O_SCOCOD_0 == 2 || O_SCOCOD_0==3)
                return 'D';  //contolavoro
            else if (O_SCOCOD_0 == 1)
                return 'M';  //make
            else
            {
                Utils.SendMail_IT(Settings.GetSettings(), "Errore import OPR in CyberPlan;  O_SCOCOD_0= " + O_SCOCOD_0 + " non previsto","Import OPR -> Cyberplan");
                return '?';
            }
        }
        public override void LastAction(ref DBHelper2 cm, DBHelper2 sage)
        {

            //
            //controllo che gli OPR siano associati a ODV aperti
            //

            string chk_query = @"SELECT OPR.[C_CODE]
      ,OPR.[C_CORDER_CODE]
      ,OPR.[C_ITEM_CODE]
	  ,SOH.C_CODE
  FROM [CyberPlanFrontiera].[dbo].[CYB_ORDER] OPR
  full join [CyberPlanFrontiera].[dbo].[CYB_CORDER] SOH
  on OPR.C_CORDER_CODE= SOH.C_CODE
  where (OPR.C_CODE like 'OPR%' or OPR.C_CODE like 'ODP%') and OPR.C_CORDER_CODE not like '000000000000' and SOH.C_CODE is null ";

            string testo_mail = "";
            DbDataReader dtr = cm.GetReaderSelectCommand(chk_query);
            object[] row = new object[dtr.FieldCount];

            while (dtr.Read())
            {
                dtr.GetValues(row);
                testo_mail += "OPR =" + getDBV<string>(row[0], "C_CODE") + "  ODV =" + getDBV<string>(row[1], "C_CORDER_CODE") + "  articolo " + getDBV<string>(row[2], "C_ITEM_CODE") + "; OPR aperto con ordine di vendita non presente (forse già chiuso)" + Utils.NewLineMail();
            }





            //
            //controllo che gli OPR abbiano codici presenti in anagrafica articoli
            //

            chk_query= @"SELECT OPR.[C_CODE]
              ,OPR.[C_CORDER_CODE]
              ,OPR.[C_ITEM_CODE]
	          ,ITM.[C_CODE]
              FROM[CyberPlanFrontiera].[dbo].[CYB_ORDER] OPR
              left join[CyberPlanFrontiera].[dbo].[CYB_ITEM] ITM
              on OPR.[C_ITEM_CODE]= ITM.C_CODE
              where OPR.C_CODE like 'OPR%' --and OPR.C_CORDER_CODE not like '000000000000' 
              and ITM.[C_ITEM_GROUP] not like '__TOOL__'
              and ITM.C_CODE is null ";

            dtr.Close();
            dtr = cm.GetReaderSelectCommand(chk_query);
            row = new object[dtr.FieldCount];

            while (dtr.Read())
            {
                dtr.GetValues(row);
                testo_mail += "OPR =" + getDBV<string>(row[0], "C_CODE") + "  ODV =" + getDBV<string>(row[1], "C_CORDER_CODE") + "  articolo " + getDBV<string>(row[2],"C_ITEM_CODE") + "; OPR aperto con articolo non rilasciato" + Utils.NewLineMail();
            }


            //
            //invio mail
            //

            Utils.SendMail_Plan(Settings.GetSettings(), testo_mail, sage.LibreriaDossier.ToUpper()+"=>" + "OPR");
        }

        char getMRP_type(string ordinecommessa)
        {
            //F=MTS make to stock (a fabbisogno)  C= MTO make to order (a commessa)  
            if (string.IsNullOrWhiteSpace(ordinecommessa))
            {
                return 'F';//ordine MRP a fabbisogno
            }
            else
            {
                return 'C'; //ordine a commessa
            }
        }
    }
}
