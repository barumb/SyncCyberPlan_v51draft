using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Xml;
using System.IO;

namespace SyncCyberPlan_lib
{
    public static class X3WSUtils
    {
        public static string X3WS_poolAlias = "WSSAURO";
        public static string X3WS_poolAliasTest = "WSSAUROTEST";
        public static string X3WS_poolAliasDev = "WSSAURODEV";
        public static string X3WS_codeLang = "ITA";
        public static string X3WS_Endpoint = @"http://sage.sauro.dmn/soap-wsdl/syracuse/collaboration/syracuse/CAdxWebServiceXmlCC";
        public static string X3WS_usr = "X3WS";
        public static string X3WS_pwd = "wsusr1736";
        public static string X3WS_HeaderXmlFile = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
        public static string X3WS_NewLine = " \r\n";

        public static string X3WS_PublicName_ImpOpr = "YIMPOPR";

        static public NetworkCredential GetCredential()
        {
            NetworkCredential ret = new NetworkCredential(X3WSUtils.X3WS_usr, X3WSUtils.X3WS_pwd);
            return ret;
        }
        static public Uri GetX3Endpoint()
        {
            // URL=http://sage.sauro.dmn:8124/soap-wsdl/syracuse/collaboration/syracuse/CAdxWebServiceXmlCC
            // URL
            Uri x3Endpoint = new Uri(@"http://sage.sauro.dmn/soap-wsdl/syracuse/collaboration/syracuse/CAdxWebServiceXmlCC");
            return x3Endpoint;
        }


        static public string GetStringValueFromXML(string XMLbuffer, string XmlElementFieldName, string XmlAttributeName)
        {
            string _value = "";

            if ((XMLbuffer == "") || (XMLbuffer == null)) return _value;


            using (XmlReader reader = XmlReader.Create(new StringReader(XMLbuffer)))
            {
                reader.MoveToContent();
                // Parse the file and display each of the nodes.  
                while (reader.ReadToFollowing(XmlElementFieldName))
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            //_FieldValue = recordXml.ReadElementContentAsString();
                            reader.MoveToAttribute("NAME");
                            if (reader.Value == XmlAttributeName)
                            {
                                reader.MoveToElement();
                                _value = reader.ReadElementContentAsString();
                            }
                            //switch (reader.Value)
                            //{
                            //    case "NRORD":
                            //        reader.MoveToElement();
                            //        _OrdNum = reader.ReadElementContentAsString();
                            //        break;
                            //
                            //}
                            break;
                    }
                }
            }
            return _value;
        }

        static public string XMLBuildReq(string pXMLGrpName, string pXMLTagFldName, List<string[]> KeyValues)
        {
            string pHeaderXML = X3WSUtils.X3WS_HeaderXmlFile;
            string _nl = X3WSUtils.X3WS_NewLine;
            string _XML = pHeaderXML + _nl;

            _XML = _XML + "<" + pXMLGrpName + ">" + _nl;
            foreach (string[] _cur in KeyValues)
            {
                _XML = _XML + _AddField(pXMLTagFldName, _cur[0], _cur[1]);

            }
            _XML = _XML + "</" + pXMLGrpName + ">" + _nl;
            return _XML;
        }

        static private string _AddField(string pXMLTagFldName, string X3Key, string X3Value)
        {
            string _line = "<" + pXMLTagFldName + " NAME=\"" + X3Key.ToUpper() + @""">" + X3Value.ToUpper() + "</" + pXMLTagFldName + ">" + X3WSUtils.X3WS_NewLine;

            return _line;
        }
    }
}
