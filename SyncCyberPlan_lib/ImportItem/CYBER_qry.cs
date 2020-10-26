using System.Collections.Generic;
using System;
using log4net;
using System.Data;
using System.Data.Common;

namespace SyncCyberPlan_lib
{
    public static class CYBER_qry 
    {
        static readonly ILog _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name);
        public enum Status
        {
            Read, Running, Completed
        }        

        static public void SetStatus(string stato)
        {
            DBHelper2 db = DBHelper2.getCyberDBHelper();
            string command = "UPDATE [CyberPlanFrontiera].[dbo].[TRANSFER_STATUS] " 
                                          + " SET [STATUS] = '" + stato + "' ,"
                                          + " TIMESTAMP = '" + System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "'"
                                          + " WHERE ID='TR_STATUS'  ";
            _logger.Info("start execution");

            int i = DBHelper2.EseguiSuDBCyberPlan(ref db, command, 600);
            _logger.Info("end execution");
        }

        static public void Init()
        {
            DBHelper2 db = DBHelper2.getCyberDBHelper();
            string exec_store_procedure = "EXECUTE [CyberPlanFrontiera].[dbo].[FILL_E_ROUTING]";
            _logger.Info("start execution");
            
            int i = DBHelper2.EseguiSuDBCyberPlan(ref db, exec_store_procedure, 600);
            _logger.Info("end execution");
        }


        static public void FinalCheck(string dossier)
        {
            FinalCheck_ARTICOLI_con_reparto_non_coerente( dossier);
            FinalCheck_ATTR_senza_macchina(dossier);
            FinalCheck_PLAS_senza_cicli();
            FinalCheck_Ordini_di_Articoli_senza_cicli();
        }
        static private void FinalCheck_ARTICOLI_con_reparto_non_coerente( string dossier)
        {
//            DBHelper2 db = DBHelper2.getSageDBHelper("SAURO");
//            string command = @" select F.YWCR_0, F.ITMREF_0, AM.MACREP_0, AM.MAC_0 from SAURO.ITMFACILIT F
//join SAURO.ITMMASTER M on F.ITMREF_0=M.ITMREF_0
//left join SAURO.YPRDITM P on P.ITMREF_0=F.ITMREF_0 
//left join SAURO.YPRDAM AM on P.YATTCOD_0=AM.YCONATT_0
//where F.STOFCY_0='ITS01' and F.YWCR_0<>AM.MACREP_0 and MACREP_0<>'CL' and ITMSTA_0=1
//order by F.YWCR_0, AM.MACREP_0 ";

            DBHelper2 db = DBHelper2.getSageDBHelper(dossier);
            string command = @" select F.YWCR_0, F.ITMREF_0, AM.MACREP_0, AM.MAC_0 "
                            + " from " + dossier + ".ITMFACILIT F "
                            + " join " + dossier + ".ITMMASTER M on F.ITMREF_0=M.ITMREF_0 "
                            + " left join " + dossier + ".YPRDITM P on P.ITMREF_0=F.ITMREF_0 "
                            + " left join " + dossier + ".YPRDAM AM on P.YATTCOD_0=AM.YCONATT_0 "
                            + " where F.STOFCY_0='ITS01' and F.YWCR_0<>AM.MACREP_0 and MACREP_0<>'CL' and ITMSTA_0=1 "
                            + " order by F.YWCR_0, AM.MACREP_0 ";



            _logger.Info("start execution");

            string testo_mail = "";
            DbDataReader dtr = db.GetReaderSelectCommand(command);
            object[] row = new object[dtr.FieldCount];

            string reparto_articolo = "", articolo = "";
            string reparto_macchina="", macchina = "";
            while (dtr.Read())
            {
                dtr.GetValues(row);
                reparto_articolo = (string)row[0];
                articolo = (string)row[1];
                reparto_macchina= (string)row[2];
                macchina = (string)row[3];
                testo_mail += articolo.PadRight(40) + " ha reparto " + reparto_articolo.PadRight(10) + " ma la macchina " + macchina.PadRight(20) + " ha reparto " + reparto_macchina+ Utils.NewLineMail();
            }

            Utils.SendMail_Anag(Settings.GetSettings(), testo_mail, "Articoli ATTIVI con reparto non coerente con reparto macchina");
            _logger.Info("end execution");
        }
        static private void FinalCheck_ATTR_senza_macchina( string dossier)
        {
//            DBHelper2 db = DBHelper2.getSageDBHelper("SAURO");
//            string command = @" select YATTCOD_0, YATTDES_0 from SAURO.YPRDATT A
//left join SAURO.YPRDCONF C on A.YATTCOD_0=C.YCONATT_0 and C.YCONENAFLG_0=2
//where A.YATTENAFLG_0=2 and C.YCONATT_0 is null
//order by YSTACOD_0 ";

            DBHelper2 db = DBHelper2.getSageDBHelper(dossier);
            string command = @" select YATTCOD_0, YATTDES_0 from "
                             + dossier +".YPRDATT A "
                             + " left join " +dossier+".YPRDCONF C on A.YATTCOD_0=C.YCONATT_0 and C.YCONENAFLG_0=2 "
                             + " where A.YATTENAFLG_0=2 and C.YCONATT_0 is null "
                             + " order by YSTACOD_0 ";




            _logger.Info("start execution");

            string testo_mail = "";
            DbDataReader dtr = db.GetReaderSelectCommand(command);
            object[] row = new object[dtr.FieldCount];

            string attrezzatura = "", desc="";
            while (dtr.Read())
            {
                dtr.GetValues(row);
                attrezzatura = (string)row[0];
                desc = (string)row[1];
                testo_mail += "attrezzatura senza macchine associate: " + attrezzatura.PadRight(40) + desc + Utils.NewLineMail();
            }

            Utils.SendMail_Plan(Settings.GetSettings(), testo_mail, "Attrezzature ATTIVE senza macchine associate");
            _logger.Info("end execution");
        }
        static private void FinalCheck_Ordini_di_Articoli_senza_cicli()
        {
            DBHelper2 db = DBHelper2.getCyberDBHelper();
            string command = @"  Select C_CODE,C_CORDER_CODE,C_ITEM_CODE,C_M_B --,ope.C_OPNUM, ope.C_USER_STRING01 AS Attrezzatura, ope.C_USER_STRING02 as Macchina
  from [CyberPlanFrontiera].[dbo].[CYB_ORDER] od
  left join [CyberPlanFrontiera].[dbo].[CYB_OPERATION] ope
  on od.C_CODE = ope.C_ORDER_CODE
  where C_M_B in ('M','D') and ope.C_OPNUM is null 
  order by C_ITEM_CODE ";


            _logger.Info("start execution");

            string testo_mail = "";
            DBHelper2 cyber = DBHelper2.getCyberDBHelper();
            DbDataReader dtr = cyber.GetReaderSelectCommand(command);
            object[] row = new object[dtr.FieldCount];

            string prec_articolo = "";
            string articolo = "";
            while (dtr.Read())
            {
                dtr.GetValues(row);
                articolo = (string)row[2];

                string C_M_B = (string)row[3];                
                if (C_M_B != "D")  //se è di contolavoro non lo segnalo: devono avere il ciclo solo gli articolo di CL a capacità Finita, ma da qui non si riesce a suddividere in base al Cdl_MRP, quindi non riesco a fare il controllo
                {
                    if (articolo != prec_articolo)
                    {
                        prec_articolo = articolo;
                        testo_mail += Utils.NewLineMail() + " codice =" + articolo + "  non ha ciclo ma ha degli ordini di produzione " + Utils.NewLineMail();
                        if (articolo == "WM0662-03")
                        {
                            testo_mail += articolo+ ": questo articolo non ha attrezzatura in As400, quindi non viene creato il ciclo; OK" + Utils.NewLineMail(); 
                        }
                    }
                    testo_mail += (string)row[0] + " " + Utils.NewLineMail();
                }
            }

            Utils.SendMail_Anag(Settings.GetSettings(), testo_mail, "Ordini di articoli senza ciclo");
            _logger.Info("end execution");
        }
        static private void FinalCheck_PLAS_senza_cicli()
        {
            DBHelper2 db = DBHelper2.getCyberDBHelper();
            string command = @"SELECT
      distinct D.[C_ITEM_CODE]

      ,D.[C_ORDER_CODE]
      /*,D.[C_QTY]
      ,D.[C_WAREHOUSE_CODE]
      ,I.[C_DESCR ]
      ,I.[C_M_B ]
      ,I.[C_ITEM_GROUP ]
      ,I.[C_USER_STRING01 ]*/
      ,CICLI.C_CODE

  FROM[CyberPlanFrontiera].[dbo].[CYB_DEMAND] D
left join[CyberPlanFrontiera].[dbo].[CYB_ITEM] I on D.C_ITEM_CODE=I.[C_CODE]
left join
(
  --query indicata da Savietto per recuperare i cicli; qui serve per recuperare Articoli PLAS che non hanno cicli
  SELECT T.C_CODE FROM[CyberPlanFrontiera].[dbo].FiltroCicliPlastica T
                  LEFT JOIN [CyberPlanFrontiera].[dbo].CYB_STD_OP_MACHINE PO_MAC
                  ON T.ATTREZZATURA  = PO_MAC.C_ROUTING_CODE
                  left join [CyberPlanFrontiera].[dbo].cyb_machine mac on mac.c_code = PO_MAC.machine_code
                  where CHARINDEX(TIPO_P, mac.c_user_string04) > 0 -- verifico tipo plastica
                  and ((T.DIVISORE = 1 and mac.[C_USER_FLAG01] = 1) or T.DIVISORE = 0) -- Devo verificare: se il divisore è richiesto solo macchine con divisore, altrimenti tutte
                  and((T.PESO_MATER + T.PESO_ITEM) * (CASE WHEN cast(T.IMPRONTE_FORZ as int) = 0 THEN COALESCE(cast(t.IMPRONT_DEFAULT as int), 1) ELSE COALESCE(cast(t.IMPRONTE_FORZ as int), 1) END))  >= MAC.C_USER_REAL01
                 and((T.PESO_MATER + T.PESO_ITEM)*(CASE WHEN cast(T.IMPRONTE_FORZ as int) = 0 THEN COALESCE(cast(t.IMPRONT_DEFAULT as int), 1) ELSE COALESCE(cast(t.IMPRONTE_FORZ as int), 1) END))  <= MAC.C_USER_REAL02  -- Verifico il peso tra il min e il max della macchina
      GROUP BY T.C_CODE, T.C_PLANT_CODE, PO_MAC.C_OPNUM, PO_MAC.C_ALT_OPNUM, COALESCE(MAC.C_WORKCENTER,'TRASH')
  
  ) CICLI on D.C_ITEM_CODE= CICLI.C_CODE
  where I.[C_USER_STRING01]='PLAS' and CICLI.C_CODE is null 
order by D.C_ITEM_CODE ";


            _logger.Info("start execution");
            
            string testo_mail = "";
            DBHelper2 cyber = DBHelper2.getCyberDBHelper();
            DbDataReader dtr = cyber.GetReaderSelectCommand(command);
            object[] row = new object[dtr.FieldCount];

            string prec_articolo = "";
            string articolo = "";
            while (dtr.Read())
            {
                dtr.GetValues(row);
                articolo = (string)row[0];
                if (articolo != prec_articolo)
                {
                    prec_articolo = articolo;
                    testo_mail += Utils.NewLineMail() + " codice =" + articolo + "  non è producibile ma questi Ordine di produzione lo richiedono " + Utils.NewLineMail();
                }
                testo_mail += (string)row[1] + " " + Utils.NewLineMail();
            }


            Utils.SendMail_Anag(Settings.GetSettings(), testo_mail, "WP senza ciclo");
            _logger.Info("end execution");
        }
    }
}
