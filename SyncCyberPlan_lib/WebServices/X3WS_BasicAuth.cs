using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace SyncCyberPlan_lib
{
    /// <summary>
    /// WebServices di X3 con BasicAuth
    /// </summary>
    public class X3WS_BasicAuth : SageX3WS.CAdxWebServiceXmlCCService
    {
        /*
        Metodi HTTP

        GET: Retrieve information associated with a specific URL resource
        HEAD: Retrieve header information linked with a URL resource
        POST: Send data to the web server – for example, form data
        PUT: Replace the data for a specific URL with new data transmitted by the client
        DELETE: Delete the data behind the respective URL

        */

        /* 
        Codice derivato dall'articolo https://www.rklesolutions.com/blog/x3-soap-web-services 

        The second change is the authentication method. In prior versions, the X3 authentication information (user name and password) was included in the context. 
        In Update 9, the user name and password are no longer included in the context, but they can be passed using basic authentication.

        Below is an example of adding basic authentication to the web service call.
        Create a class called BasicAuth which inherits the CAdxWebServiceXmlCCService class from the wsdl. 
        Method GetWebRequets in this class adds the basic authentication information to the web request header.
        */
        protected override WebRequest GetWebRequest(Uri uri)
        {
            HttpWebRequest _webRequest = (HttpWebRequest)base.GetWebRequest(uri);
            NetworkCredential credential = Credentials as NetworkCredential;
            if (credential != null)
            {
                string authInfo = "";
                if (credential.Domain != null && credential.Domain.Length > 0)
                {
                    authInfo = string.Format(@"{0}\{1}:{2}", credential.Domain, credential.UserName, credential.Password);
                }
                else
                {
                    authInfo = string.Format(@"{0}:{1}", credential.UserName, credential.Password);
                }
                authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
                _webRequest.Headers["Authorization"] = "Basic " + authInfo;
            }
            _webRequest.PreAuthenticate = true;

            return _webRequest;
        }
    }
}
