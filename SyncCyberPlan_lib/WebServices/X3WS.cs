using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using SyncCyberPlan_lib.SageX3WS;
using log4net;

namespace SyncCyberPlan_lib
{
    public class X3WS
    {
        protected static readonly ILog _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name);

        protected X3WS_BasicAuth _webService;
        protected SageX3WS.CAdxCallContext _context;
        public X3WS()
        {
            Init();
        }
        public X3WS(string poolAlias)
        {
            Init();
            ResetContext(poolAlias);
        }
        protected void Init()
        {
            _logger.Debug(this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "  start...");
            /*
             webservice referenziati con tecnlogia core .net 
             dotnet core NON gestisce l'autenticazione basic per cui non è utilizzabile con i WS sage V7PU9

            //Connected Services SageX3WS_old.CAdxWebServiceXmlCCClient _soapClient;

            // .net core
            //_soapClient = new SageX3WS_old.CAdxWebServiceXmlCCClient("CAdxWebServiceXmlCC", pSageHost.AbsoluteUri);
            */

            // .net 2.0
            _webService = new X3WS_BasicAuth();
            //_WebService.Url = pSageHost.AbsoluteUri;   // togliendo questa ha funzionato
            _webService.Credentials = X3WSUtils.GetCredential();
            _webService.PreAuthenticate = true;
            _logger.Debug(this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "  end");
        }
        public void ResetContext(string pPoolAlias)
        {
            _logger.Debug(this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "  start...");
            _context = new SageX3WS.CAdxCallContext();
            _context.codeLang = X3WSUtils.X3WS_codeLang;
            _context.poolAlias = pPoolAlias;// X3WSUtils.X3WS_poolAlias / X3WS_poolAliasTest
            _context.requestConfig = "";
            _logger.Debug(this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "  end");
        }
        /*public void WSTestQuery_NetCore()
        {

            SageX3WSCore.CAdxCallContext _context = new SageX3WSCore.CAdxCallContext();
            SageX3WSCore.CAdxParamKeyValue[] _params = new SageX3WSCore.CAdxParamKeyValue[1];
            SageX3WSCore.CAdxResultXml _results = new SageX3WSCore.CAdxResultXml();

            _context.codeLang = "ITA";
            _context.poolAlias = "WSSAURODEV";

            _params[0] = new SageX3WSCore.CAdxParamKeyValue();
            _params[0].key = "SOHNUM";
            _params[0].value = "IT001-ORE1800001";


            SageX3WSCore.queryRequest qryRequest = new SageX3WSCore.queryRequest(_context, "SOH", _params, 10);
            try
            {
                SageX3WSCore.queryResponse queryResponse = _soapClient.query(qryRequest);
            }
            catch (Exception)
            {

                throw;
            }

        }*/

        protected void GetDescription(string PublicName)
        {
            _logger.Debug(this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "  start...");
            string wsResponse = "";
            SageX3WS.CAdxResultXml _resultXML = new SageX3WS.CAdxResultXml();
            try
            {
                _webService.getDescription(_context, PublicName);
            }
            catch (Exception)
            {
                throw;
            }
            _logger.Debug(this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "  end");
        }
        protected CAdxResultXml QueryRequest(string pPublicName, SageX3WS.CAdxParamKeyValue[] _paramKeyValue, int pListSize = 1)
        {
            _logger.Debug(this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "  start...");
            SageX3WS.CAdxResultXml _resultXML = new SageX3WS.CAdxResultXml();
            try
            {
                _resultXML = _webService.query(_context, pPublicName, _paramKeyValue, pListSize);
            }
            catch (Exception ex)
            {
                throw;
            }
            _logger.Debug(this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "  end");
            return _resultXML;
        }
        protected string InsertOrderRequest(string pPublicName, SageX3WS.CAdxParamKeyValue[] _paramKeyValue, int pListSize = 1)
        {
            _logger.Debug(this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "  start...");
            string wsResponse = "";
            SageX3WS.CAdxResultXml _resultXML = new SageX3WS.CAdxResultXml();

            try
            {
                _resultXML = _webService.query(_context, pPublicName, _paramKeyValue, pListSize);
            }
            catch (Exception ex)
            {
                throw;
            }
            wsResponse = _resultXML.resultXml;
            _logger.Debug(this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "  end");
            return wsResponse;
        }
        protected CAdxResultXml ReadRequest(string pPublicName, SageX3WS.CAdxParamKeyValue[] _paramKeyValue)
        {
            _logger.Debug(this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "  start...");
            SageX3WS.CAdxResultXml _resultXML = new SageX3WS.CAdxResultXml();
            try
            {
                _resultXML = _webService.read(_context, pPublicName, _paramKeyValue);
            }
            catch (Exception ex)
            {

                throw;
            }
            _logger.Debug(this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "  end");
            return _resultXML;
        }
        protected CAdxResultXml SaveRequest(string pPublicName, string pXmlObject)
        {
            _logger.Debug(this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "  start...");
            SageX3WS.CAdxResultXml _resultXML = new SageX3WS.CAdxResultXml();
            try
            {
                _resultXML = _webService.save(_context, pPublicName, pXmlObject);
            }
            catch (Exception ex)
            {
                throw;
            }
            _logger.Debug(this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "  end");
            return _resultXML;
        }
        protected CAdxResultXml RunSubprog(string pPublicName, string pXmlObject)
        {
            _logger.Debug(this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "  start...");
            SageX3WS.CAdxResultXml _resultXML = new SageX3WS.CAdxResultXml();
            try
            {
                _resultXML = _webService.run(_context, pPublicName, pXmlObject);
            }
            catch (Exception ex)
            {
                throw;
            }
            _logger.Debug(this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "  end");
            return _resultXML;
        }


        public bool ImportOprFile(string nomeFile)
        {
            _logger.Debug(this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "  start...");
            CAdxResultXml XMLresult = new CAdxResultXml();

            List<string[]> KeyValue = new List<string[]>();
            KeyValue.Add(new string[2] { "IMPFILE",nomeFile });
            //KeyValue.Add(new string[2] { "TIMESTAMP", timestamp });
            //KeyValue.Add(new string[2] { "MSGERR", "000"});
            /*
<PARAM>
    <FLD NAME="IMPFILE">opr_aaaaa.txt</FLD>
    <FLD NAME="TIMESTAMP">20000909</FLD>
    --<FLD NAME="MSGERR">””</FLD>
</PARAM>
             */
            string XMLRequest = X3WSUtils.XMLBuildReq("PARAM", "FLD", KeyValue);
            XMLresult = RunSubprog(X3WSUtils.X3WS_PublicName_ImpOpr, XMLRequest);

            _logger.Info("XMLresult.status="+ XMLresult.status + "[0=errore, 1 ok]");
            if (XMLresult.messages != null && XMLresult.messages.Length > 0)
            {
                foreach (var msg in XMLresult.messages)
                {
                    _logger.Info(msg.message);
                }
            }

            //status
            //0 non esiste chiamata
            //1 chiamata eseguita
            int _chk = XMLresult.status;

            // Se arrivo qui vuol dire che è tutto OK
            string QryResponse = XMLresult.resultXml;

            //Dal flusso di ritorno estrapolo le info che mi servono
            // ...... GetOrdNum_ByRifOrdCli(SoapQryResponse, "FLD", "NRORD");

            string MSGERR = X3WSUtils.GetStringValueFromXML(QryResponse, "FLD", "MSGERR");
            _logger.Debug(this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "  ending");
            if (MSGERR == "Imported")
            {
                return true;
            }
            else
            {
                return false;
            }            
        }
    }
}
