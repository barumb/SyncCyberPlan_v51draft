using System.Collections.Generic;
using System;
using log4net;
using System.Data;
using System.Data.Common;

namespace SyncCyberPlan_lib
{
    public static class CYBER_utils 
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


        static public void FinalCheck()
        {   
            FinalCheck_PLAS_senza_cicli();
            FinalCheck_Ordini_di_Articoli_senza_cicli();
        }
        static private void FinalCheck_Ordini_di_Articoli_senza_cicli()
        {
            DBHelper2 db = DBHelper2.getCyberDBHelper();
            string command = @"  Select C_CODE,C_CORDER_CODE,C_ITEM_CODE,C_M_B  --, ope.C_USER_STRING01, ope.C_USER_STRING02
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
                if (C_M_B != "D")  //se è di contolavoro non lo segnalo, non ha ciclo (? in attesa di mail Savietto)
                {
                    if (articolo != prec_articolo) 
                    {
                        prec_articolo = articolo;
                        testo_mail += Utils.NewLineMail() + " codice =" + articolo + "  non ha ciclo ma ha degli ordini di produzione " + Utils.NewLineMail();
                    }
                    testo_mail += (string)row[0] + " " + Utils.NewLineMail();
                }
            }


            Utils.SendMail("it@sauro.net", "luca.biasio@sauro.net,alessandro.andrian@sauro.net", testo_mail);
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


            Utils.SendMail("it@sauro.net", "luca.biasio@sauro.net,alessandro.andrian@sauro.net", testo_mail);
            _logger.Info("end execution");
        }
    }
}
