﻿using System.Collections.Generic;
using System;
using log4net;
using System.Data;
using System.Data.Common;

namespace SyncCyberPlan_lib
{
    public class OrdiniAcq_As400_OPR : OrdiniAcq
    {
        public string  MFHTORD;
        public decimal MFHAORD;
        public decimal MFHPORD;
        public string  MFHTCOM;
        public decimal MFHACOM;//anno ODV
        public decimal MFHPCOM;//progressivo ODV
        public decimal MFHSCOM; //riga ODV
        public string  MFHCART;
        public decimal MFHQTRC;
        public decimal MFHDCRE; //DATA
        public string  MFHSTAT;
        public decimal MFVDINI; // DATA
        public decimal MFVDEND; // DATA
        public string  MFVSTAV; // stato riga  MFVSTAV  se =ST ordine in corso, se vuoto ordine in attesa
        public decimal MFHQTPR; // qta prodotta
        public decimal MFVQTSC; //qta scartata

        public string  MFVUTLM;  //unita di misura tempo per un pezzo/via  1=ORE  2=100MI-HR   3 Minuti 4 giorni  5 settimane
        public decimal MFVAMPT;  //tempo per un pezzo/via
        public string  MFVUTSE;  //unita di misura tempo di setup
        public decimal MFVASET;  //tempo di setup

        public string MFVWRKC;  //centro di lavoro (Interno/esterno in as400)


        public OrdiniAcq_As400_OPR(/*string YPOHTYP*/): base("CYB_ORDER")
        {
            //__YPOHTYP_filter = YPOHTYP;
        }

        public override string GetSelectQuery(bool mode, string dossier, string codice_like, string filtro)
        {
            string __libreriaAs400 = dossier;

            string _tabMFH = __libreriaAs400 + ".MFH00PF";
            string _tabMFV = __libreriaAs400 + ".MFV00PF";

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

            if (!string.IsNullOrWhiteSpace(codice_like))
            {
                query += " and " + _tabMFH + ".MFHCART like '" + codice_like.Trim() + "'";
            }

            query += " ORDER BY " 
                + "  " + _tabMFH + ".MFHTORD," + "\n"
                + "  " + _tabMFH + ".MFHAORD," + "\n"
                + "  " + _tabMFH + ".MFHPORD" + "\n"
                ;
            return query;
        }

        public override void Init(object[] row)
        {
            MFHTORD = getDBV<string>(row[0]);
            MFHAORD = getDBV<decimal>(row[1]);
            MFHPORD = getDBV<decimal>(row[2]);
            MFHTCOM = getDBV<string>(row[3]);
            MFHACOM = getDBV<decimal>(row[4]);
            MFHPCOM = getDBV<decimal>(row[5]);
            MFHSCOM = getDBV<decimal>(row[6]);
            MFHCART = getDBV<string>(row[7]);
            MFHQTRC = getDBV<decimal>(row[8]);
            MFHDCRE = getDBV<decimal>(row[9]);
            MFHSTAT = getDBV<string>(row[10]);   
            MFVDINI = getDBV<decimal>(row[11]);
            MFVDEND = getDBV<decimal>(row[12]);
            MFVSTAV = getDBV<string>(row[13]);
            MFHQTPR = getDBV<decimal>(row[14]);
            MFVQTSC = getDBV<decimal>(row[15]);

            MFVUTLM = getDBV<string>(row[16]);
            MFVAMPT = getDBV<decimal>(row[17]);
            MFVUTSE = getDBV<string>(row[18]);
            MFVASET = getDBV<decimal>(row[19]);

            MFVWRKC = getDBV<string>(row[20]);




            C_CODE                = EscapeSQL(MFHTORD + MFHAORD.ToString("00") + MFHPORD.ToString("000000"), 30);        //varchar         30                
            //ATTENZIONE il valore "000000000000" indica valore default per ordini a fabbisogno
            C_CORDER_CODE         = EscapeSQL(MFHTCOM + MFHACOM.ToString("00") + MFHPCOM.ToString("000000") + MFHSCOM.ToString("0000"), 30);   //varchar 30  
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
                Utils.SendMail("it@sauro.net", "it@sauro.net", "Errore import OPR in CyberPlan: "+ C_CODE+" ha cdl in as400 diverso da CDLEXT/CDLINT");
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
  where OPR.C_CODE like 'OPR%' and OPR.C_CORDER_CODE not like '000000000000' and SOH.C_CODE is null ";

            string testo_mail = "";
            DbDataReader dtr = cm.GetReaderSelectCommand(chk_query);
            object[] row = new object[dtr.FieldCount];

            while (dtr.Read())
            {
                dtr.GetValues(row);
                testo_mail += "OPR =" + getDBV<string>(row[0]) + "  ODV=" + getDBV<string>(row[1]) + "  articolo " + getDBV<string>(row[2]) + "; OPR aperto con ordine di vendita non presente (forse già chiuso)" + Utils.NewLineMail();
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
                testo_mail += "OPR =" + getDBV<string>(row[0]) + "  ODV=" + getDBV<string>(row[1]) + "  articolo " + getDBV<string>(row[2]) + "; OPR aperto con articolo non rilasciato" + Utils.NewLineMail();
            }










            //
            //invio mail
            //

            if (testo_mail != "")
            {
                Utils.SendMail("it@sauro.net", "leonardo.macabri@sauro.net,cristian.scarso@sauro.net", testo_mail);
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
