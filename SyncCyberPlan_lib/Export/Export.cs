using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace SyncCyberPlan_lib
{
    public class Export
    {
        static readonly ILog _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name);

        public Export()
        {
        }
        /// <summary>
        /// TaskNumber è il numero con cui CyberPLan identifica gli export
        /// ogni export ha un TaskNumber che corrisponde a più righe in ciascuna tabella di export
        /// 
        /// E' importante che le modifiche esportate da CyberPLan siano portate su Sage nell'ordine corretto, per evitare disallineamenti
        /// 
        /// Per questo vanno esportati un TaskNumber alla volta, nel giusto ordine (nel caso che per qualche motivo ci siano più task number presenti)
        /// </summary>
        /// <param name="dossier"></param>
        public void ExportAllTaskNumber(string dossier, bool creaOprDaAs400)
        {
            X3WS service = null;
            string pool = "";

            switch (dossier)
            {
                case "SAURO": pool = X3WSUtils.X3WS_poolAlias+ "TOGLIMI_____BLOCCO_PER_SICUREZZA_FINO_A_RELEASE"; break;
                case "SAUROTEST": pool = X3WSUtils.X3WS_poolAliasTest; break;
                case "SAURODEV": pool = X3WSUtils.X3WS_poolAliasDev; break;
                default:
                    _logger.Info("Dossier non previsto per i WebServices\n");
                    return;
                    //break;
            }

            //tabelle di export di CyberPlan
            ExpCorder cord = new ExpCorder();
            ExpOrderOPR opr = new ExpOrderOPR(creaOprDaAs400);


            // si importano gli opr dal tasknumber più piccolo al più grande in seguenza fino all afine o al primo errore di importazione

            bool res= true;
           

            while (res)
            {
                res = false;

                bool flgImportOpr = true;
                bool flgImportCOrd = true;

                int? firstTaskNumber = ExportItem.GetMinTaskNumber(); 
                if (firstTaskNumber.HasValue)
                {
                    if (service == null)
                    {
                        service = new X3WS(pool); //inizializzo il Ws solo se necessario
                    }
                    //export Cyb vs Sage tramite WS
                    flgImportOpr = ExportTaskNumber(dossier, service, firstTaskNumber.Value, opr);
                    flgImportCOrd = ExportTaskNumber(dossier, service, firstTaskNumber.Value, cord);
                    
                    //export verso As400 dell'intero TaskNUmber  ???? MA HA SENSO?????????
                    //il Ws di export esporta tutto non solo il task corrente!!!!!! 
                    service.ExportMfgToAs400(firstTaskNumber.Value);

                    // se c'è stato un errore di importazione mi fermo
                    res = flgImportCOrd && flgImportOpr;
                }
                else
                {
                    _logger.Info("Nessun TaskNumber da esportare");
                }

                
            }
        }
        static protected bool ExportTaskNumber(string dossier, X3WS service, int taskNumberToExport, ExportItem expitm)
        {
            bool ret = false;
            string file = null;

            //crea file che il ws sage userà per l'import 
            file = expitm.WriteToFile(dossier, taskNumberToExport);
            if (file != null)
            {
                //import WS opr
                //ret = service.ImportOprFile("opr_aaaaa.txt", "201909315959");

                ret = service.ImportFile(expitm.Tipo, System.IO.Path.GetFileName(file));
                if (ret)
                {
                    expitm.DeleteTaskNumber(taskNumberToExport);
                    //expitm.DeleteTaskNumber(taskNumberToExport);   PER EVITARE DI CANCELLARE I DATI ho messo -100
                }
                else
                {
                    Settings s = Settings.GetSettings();
                    //il fallimento si basa sul ritorno del WS; quindi lascio al codice del WS la segnalazione eventuale via mail agli operatori
                    Utils.SendMail_Plan(s, "Errore Import da CyberPlan via WS, file " + file, "errore import");
                }
            }
            else
            {
                ret = true;//non c'è niente da esportare
            }
            return ret;
        }
    }
}
