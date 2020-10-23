using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using log4net;
using System.Net.Mail;//MailMessage

namespace SyncCyberPlan_lib
{

    /// <summary>
    /// class per scrivere/caricare setting dal file di configurazione
    /// </summary>
    public class Settings
    {
        static public string _NOMEFILECONFIG = "Config.json";
        static public string _NOMEFILECONFIG_EXAMPLE = "ConfigExample.json";

        private string _applicationName;
        private string _mailto_IT;
        private string _mailto_anagrafica;
        private string _mailto_pianificazione;
        private string _mailfrom;
        private string _serverSmtp;
        private bool _hostInfo;

        public string ApplicationName { get { return _applicationName; } set { _applicationName = value; } }
        public string Mailto_IT { get { return _mailto_IT; } set { _mailto_IT = value; } }
        public string Mailto_anagrafica { get { return _mailto_anagrafica; } set { _mailto_anagrafica = value; } }
        public string Mailto_pianificazione { get { return _mailto_pianificazione; } set { _mailto_pianificazione = value; } }
        public string Mailfrom { get { return _mailfrom; } set { _mailfrom = value; } }
        public string ServerSmtp { get { return _serverSmtp; } set { _serverSmtp = value; } }
        public bool HostInfo { get { return _hostInfo; } set { _hostInfo = value; } }


        public void LoadConfig()
        {

        }
        public void WriteConfig()
        {
            File.WriteAllText(_NOMEFILECONFIG, JsonConvert.SerializeObject(this, new JsonSerializerSettings { Formatting = Newtonsoft.Json.Formatting.Indented }));
        }
        static public void WriteExampleConfig()
        {
            Settings file = new Settings()
            {
                ApplicationName = "NomeApplicativo",
                Mailfrom = "it@sauro.net",
                Mailto_IT = "umberto.baratto@sauro.net",
                Mailto_anagrafica = "luca.biasio@sauro.net,alessandro.andrian@sauro.net",
                Mailto_pianificazione = "leonardo.macabri@sauro.net,cristian.scarso",
                ServerSmtp = "mail.sauro.net",
                HostInfo = false,
            };

            File.WriteAllText(_NOMEFILECONFIG_EXAMPLE, JsonConvert.SerializeObject(file, new JsonSerializerSettings { Formatting = Newtonsoft.Json.Formatting.Indented }));
        }
        static public Settings GetSettings()
        {
            ILog _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name);
            Settings ret = null;
            //leggo file di configurazione
            try
            {
                ret = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(_NOMEFILECONFIG));
            }
            catch (Exception ex)
            {
                _logger.Error("File di configurazione errato");
                _logger.Error(ex.Message);
            }
            return ret;
        }
    }
}
