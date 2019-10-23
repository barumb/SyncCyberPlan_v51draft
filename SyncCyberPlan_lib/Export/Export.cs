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
        public void ExportAllTaskNumber(string dossier)
        {
            X3WS service = null;
            string pool = "";

            switch (dossier)
            {
                case "SAURO": pool = X3WSUtils.X3WS_poolAlias+ "_BLOCCO_PER_SICUREZZA_FINO_A_RELEASE"; break;
                case "SAUROTEST": pool = X3WSUtils.X3WS_poolAliasTest; break;
                case "SAURODEV": pool = X3WSUtils.X3WS_poolAliasDev; break;
                default:
                    _logger.Info("Dossier non previsto per i WebServices\n");
                    return;
                    //break;
            }

            //tabelle di export di CyberPlan
            ExpCorder cord = new ExpCorder();
            ExpOrderOPR opr = new ExpOrderOPR();

            service = new X3WS(pool); //inizializzo il Ws
            bool res= true;
            while (res)
            {
                int? firstTaskNumber = ExportItem.GetMinTaskNumber(); 
                if (firstTaskNumber.HasValue)
                {
                    res &= ExportTaskNumber(dossier, service, firstTaskNumber.Value, opr);
                    //res &= ExportTaskNumber(dossier, service, firstTaskNumber.Value, cord);                    
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

            //crea file OPR
            file = expitm.WriteToFile(dossier, taskNumberToExport);
            if (file != null)
            {
                //import WS opr
                //ret = service.ImportOprFile("opr_aaaaa.txt", "201909315959");
                
                ret = service.ImportOprFile(System.IO.Path.GetFileName(file));
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
            return ret;
        }
    }
}
