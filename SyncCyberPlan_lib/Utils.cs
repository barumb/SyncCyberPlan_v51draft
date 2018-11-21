﻿using System;
using System.Net.Mail;
using System.Net; //IpAddress
using System.Net.Sockets;
using System.Diagnostics;


namespace SyncCyberPlan_lib
{
    public struct Utils
    {
        /// <summary>
        /// Sulla stringa restituita viene eseguito il TrimEnd()
        /// </summary>
        /// <param name="str"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        static public string MaxSubStringTrimmed(string str, int maxLength)
        {
            if (str != null)
            {
                if (str.Length <= maxLength)
                {
                    return str.TrimEnd();
                }
                else
                {
                    return str.Substring(0, maxLength).TrimEnd();
                }
            }
            return null;
        }
        static public string MaxSubString(string str, int maxLength)
        {
            if (str != null)
            {
                if (str.Length <= maxLength)
                {
                    return str;
                }
                else
                {
                    return str.Substring(0, maxLength);
                }
            }
            return null;
        }
        public static string ReplaceFirstOccurrence(string str, string Find, string Replace)
        {
            string result = str; 
            int index = str.IndexOf(Find);
            if (index >= 0)
            {
                result = str.Remove(index, Find.Length).Insert(index, Replace);
            }
            return result.Trim();
        }
        /// <summary>
        /// sostituisce la prima occorrenza di Find1 con Replace; nel caso la stringa Find1 non sia presente,
        /// sostituisce la prima occorrenza di Find2 con Replace
        /// </summary>
        /// <param name="str"></param>
        /// <param name="Find1"></param>
        /// <param name="Find2"></param>
        /// <param name="Replace"></param>
        /// <returns></returns>
        public static string ReplaceFirstOccurrence(string str, string Find1, string Find2, string Replace)
        {            
            string result = ReplaceFirstOccurrence(str, Find1, Replace);
            if (result == str)
            {
                result = ReplaceFirstOccurrence(str, Find2, Replace);
            }
            return result.Trim();
        }
        public static void SendMail(string MailFROM, string MailTO, string MailServerSMTP, string Msg)
        {
            IPAddress[] localAddress = new IPAddress[0];
            IPHostEntry hostInfo = new IPHostEntry();
            String strHostName = "";

//#if !DEBUG
            try
            {
                // Get the local computer info.
                strHostName = Dns.GetHostName();
                hostInfo = Dns.GetHostEntry(strHostName);
                localAddress = hostInfo.AddressList;
          
                //MailAddress from = new MailAddress(_MailFROM);
                //MailAddress to = new MailAddress(_MailTO);
                MailMessage Message = new MailMessage(MailFROM, MailTO);

                Message.Subject = "Avviso da sincronizzazione CyberPlan (" + System.DateTime.Now + " - " + hostInfo.HostName + ")";
                //((IPEndPoint)server.LocalEndpoint).Address.ToString() + " - " +  System.DateTime.Now + " - " + Msg;				
                Message.Body = 
                    //"Avviso da Sincronizzazione CyberPlan (" + System.DateTime.Now + " - " + hostInfo.HostName + ")"
                    //+ System.Environment.NewLine                      
                    //+ System.Environment.NewLine + "Host Name:             " + hostInfo.HostName
                    //+ System.Environment.NewLine + "\t NetBIOS Machine Name:  " + System.Environment.MachineName + "  "
                    //+ System.Environment.NewLine + "\t OS Version:            " + System.Environment.OSVersion + "  "
                    //+ System.Environment.NewLine + "\t Domain Name:           " + System.Environment.UserDomainName + "  "
                    //+ System.Environment.NewLine + "\t User Thread Name:      " + System.Environment.UserName + "  "
                    //+ System.Environment.NewLine + "\t CLR version:           " + System.Environment.Version + "  "
                    //+ System.Environment.NewLine + "\t Memoria fisica associata al contesto:  " + System.Environment.WorkingSet + "  "

                      System.Environment.NewLine + Msg
                    ;

                Message.Bcc.Add("francesco.chiminazzo@sauro.net");
                
                //for (int i = 0; i < localAddress.Length; i++)
                //{
                //    Message.Body += System.Environment.NewLine + "\t IP Address " + i + "-  " + localAddress[i].ToString();
                //}

                //Message.Priority = mp; //, System.Net.Mail.MailPriority mp


                //System.Net.Mail.SmtpClient 
                SmtpClient client = new SmtpClient(MailServerSMTP);
                client.Send(Message);
                //SmtpMail.SmtpServer = _MailServerSMTP;
                //SmtpMail.Send(Message);				
            }
            catch (Exception )
            {
                //ToLog(true, "MAIL", "---" + " Exception From:" + e.Source + " Message:" + e.Message + "\n Messaggio CAMM : " + Msg);//e.ToString()
                EventLog.WriteEntry("Exception Sync CyberPlan ", Msg);
            }
//#endif
        }
    }
}
