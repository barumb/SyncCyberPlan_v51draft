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
        public static void SendMail_IT(Settings s, string Msg, bool errore)
        {
            SendMail(s.Mailfrom, s.Mailto_IT, "", s.ServerSmtp, "", Msg, errore);
        }        
        public static void SendMail_IT(Settings s, string Msg, string postTitolo)
        {
            SendMail(s.Mailfrom, s.Mailto_IT, "", s.ServerSmtp, postTitolo, Msg, false);
        }
        public static void SendMail_Plan(Settings s, string Msg, string postTitolo)
        {
            SendMail(s.Mailfrom, s.Mailto_pianificazione, s.Mailto_IT, s.ServerSmtp, postTitolo, Msg, false);
        }
        public static void SendMail_Anag(Settings s, string Msg, string postTitolo)
        {
            SendMail(s.Mailfrom, s.Mailto_anagrafica, s.Mailto_IT, s.ServerSmtp, postTitolo, Msg, false);
        }
        public static void SendMail(string MailFROM, string MailTO, string mailBCC, string MailServerSMTP, string postTitolo, string Msg, bool errore)
        {
            if (!string.IsNullOrWhiteSpace(Msg))
            {
                IPAddress[] localAddress = new IPAddress[0];
                IPHostEntry hostInfo = new IPHostEntry();
                String strHostName = "";
                if (postTitolo == null) postTitolo = "";


#if DEBUG
                MailTO = "francesco.chiminazzo@sauro.net";
#endif                
                try
                {
                    // Get the local computer info.
                    strHostName = Dns.GetHostName();
                    hostInfo = Dns.GetHostEntry(strHostName);
                    localAddress = hostInfo.AddressList;

                    //MailAddress from = new MailAddress(_MailFROM);
                    //MailAddress to = new MailAddress(_MailTO);
                    MailMessage Message = new MailMessage(MailFROM, MailTO);
                    //Message.IsBodyHtml = false;

                    if (errore)
                    {
                        Message.Subject = "[CyberPLan->Sage] ERRORE sincronizzazione (" + hostInfo.HostName + ")" + postTitolo;
                    }
                    else
                    {
                        Message.Subject = "[CyberPlan->Sage] problema da sistemare: " + postTitolo;
                    }

                    //((IPEndPoint)server.LocalEndpoint).Address.ToString() + " - " +  System.DateTime.Now + " - " + Msg;				
                    Message.Body =
                          "Avviso da Sincronizzazione CyberPlan (" + System.DateTime.Now + " - " + hostInfo.HostName + ")"
                          //+ System.Environment.NewLine                      
                          //+ System.Environment.NewLine + "Host Name:             " + hostInfo.HostName
                          //+ System.Environment.NewLine + "\t NetBIOS Machine Name:  " + System.Environment.MachineName + "  "
                          //+ System.Environment.NewLine + "\t OS Version:            " + System.Environment.OSVersion + "  "
                          //+ System.Environment.NewLine + "\t Domain Name:           " + System.Environment.UserDomainName + "  "
                          //+ System.Environment.NewLine + "\t User Thread Name:      " + System.Environment.UserName + "  "
                          //+ System.Environment.NewLine + "\t CLR version:           " + System.Environment.Version + "  "
                          //+ System.Environment.NewLine + "\t Memoria fisica associata al contesto:  " + System.Environment.WorkingSet + "  "

                          + Utils.NewLineMail() + Utils.NewLineMail() + Msg
                        ;
                    if (!string.IsNullOrWhiteSpace(mailBCC))
                    {
                        Message.Bcc.Add(mailBCC);
                    }
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
                catch (Exception ex)
                {
                    //ToLog(true, "MAIL", "---" + " Exception From:" + e.Source + " Message:" + e.Message + "\n Messaggio CAMM : " + Msg);//e.ToString()
                    EventLog.WriteEntry("Exception Sync CyberPlan ", Msg);
                }
                //#endif
            }
        }

        public static void SendMail_StatusMsg(Settings s ,  string postTitolo, string Msg , string Subject)
        {
            if (!string.IsNullOrWhiteSpace(Msg))
            {
                IPAddress[] localAddress = new IPAddress[0];
                IPHostEntry hostInfo = new IPHostEntry();
                String strHostName = "";
                if (postTitolo == null) postTitolo = "";


#if DEBUG
                s.Mailto_pianificazione = "umberto.baratto@sauro.net";
                s.Mailto_IT = "";
#endif                
                try
                {
                    // Get the local computer info.
                    strHostName = Dns.GetHostName();
                    hostInfo = Dns.GetHostEntry(strHostName);
                    localAddress = hostInfo.AddressList;

            
                    MailMessage Message = new MailMessage(s.Mailfrom, s.Mailto_pianificazione);
                    Message.Bcc.Add(s.Mailto_IT);

                    Message.Subject = Subject;
                    

                    
                    Message.Body =
                          "Avviso da Sincronizzazione CyberPlan (" + System.DateTime.Now + " - " + hostInfo.HostName + ")"                    
                          + Utils.NewLineMail() + Utils.NewLineMail() + Msg
                        ;
                                        
                    SmtpClient client = new SmtpClient(s.ServerSmtp);
                    client.Send(Message);
                    
                }
                catch (Exception ex)
                {
                    //ToLog(true, "MAIL", "---" + " Exception From:" + e.Source + " Message:" + e.Message + "\n Messaggio CAMM : " + Msg);//e.ToString()
                    EventLog.WriteEntry("Exception Sync CyberPlan ", Msg);
                }
                //#endif
            }
        }
        public static string NewLineMail()
        {
            //outlook se non ci sono spazi potrebbe tagliare gli a capo
            return "   " + System.Environment.NewLine;         
        }
      

    }
}
