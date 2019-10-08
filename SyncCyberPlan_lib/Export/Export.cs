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

        //tabelle di export di CyberPlan
        ExpCorder cord;
        ExpDemand dem;
        ExpOrderOPR opr;
        ExpOperation ope;

        public Export()
        {
            cord = new ExpCorder();
            dem = new ExpDemand();
            opr = new ExpOrderOPR();
            ope = new ExpOperation();

            //taskNumber_toExport = GetFirstTaskNumberAllTable();
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

            bool res= true;
            while (res)
            {
                res = false;
                int? firstTaskNumber = GetFirstTaskNumberAllTable();
                if (firstTaskNumber.HasValue)
                {
                    //inizializzo il Ws solo la prima volta
                    if (service == null)
                    {
                        service = new X3WS(pool);
                    }
                    res = ExportTaskNumber(dossier, service, firstTaskNumber.Value);
                }
                else
                {
                    _logger.Info("Nessun TaskNumber da esportare");
                }
            }
        }
        /// <summary>
        /// Esporta tutti i dati relativi ad un singolo taskNumber verso Sage
        /// </summary>
        /// <param name="dossier"></param>
        /// <param name="service"></param>
        /// <param name="taskNumberToExport"></param>
        /// <returns></returns>
        protected bool ExportTaskNumber(string dossier, X3WS service, int taskNumberToExport)
        {
            bool result = true;

            //result &= ExportTaskNumber(dossier, service, taskNumberToExport, cord);
            //result &= ExportTaskNumber(dossier, service, taskNumberToExport, dem);
            result &= ExportTaskNumber(dossier, service, taskNumberToExport, opr);
            //result &= ExportTaskNumber(dossier, service, taskNumberToExport, ope);

            return result;
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
                    expitm.DeleteTaskNumber(taskNumberToExport-100);
                    //expitm.DeleteTaskNumber(taskNumberToExport);   PER EVITARE DI CANCELLARE I DATI ho messo -100
                }
                else
                {
                    Settings s = Settings.GetSettings();
                    //il fallimento si basa sul ritorno del WS; quindi lascio al codice del WS la segnalazione eventuale via mail agli operatori
                    Utils.SendMail_Anag(s, "Errore Import da CyberPlan via WS");
                }
            }
            return ret;
        }
        int? GetFirstTaskNumberAllTable()
        {
            int? taskNumberAllTable = null;

            cord.RefreshFirstTaskNumber();
            dem.RefreshFirstTaskNumber();
            opr.RefreshFirstTaskNumber();
            ope.RefreshFirstTaskNumber();

            GetMinTaskNumber(ref taskNumberAllTable, cord.TaskNumber);
            GetMinTaskNumber(ref taskNumberAllTable, dem.TaskNumber);
            GetMinTaskNumber(ref taskNumberAllTable, opr.TaskNumber);
            GetMinTaskNumber(ref taskNumberAllTable, ope.TaskNumber);

            return taskNumberAllTable;
        }

        private void GetMinTaskNumber(ref int? tn, int? newtn)
        {
            if (tn.HasValue)
            {
                if (newtn.HasValue && newtn.Value < tn) tn = newtn.Value;
            }
            else if(newtn.HasValue)
            {
                tn = newtn;
            }
        }
    }
}
