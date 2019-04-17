﻿///Programma per creazione file di import di Sage con articoli di As400
///Gestisce TUTTI gli articoli che non siano Prodotti Finiti (no PF)
///
///
using System;
using log4net;
using SyncCyberPlan_lib;
using System.Collections.Generic;

namespace Console
{
    public class Program
    {
        // inizializzo logger di questa classe
        //protected static readonly ILog _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name);
        protected static readonly ILog _logger = LogManager.GetLogger("");

        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
#if DEBUG
             //EseguiTutto();
            
            Esegui("SAURO MBM41LIB_M DELETE MAC".Split(' '));
            Esegui("SAURO MBM41LIB_M ALLTIME MAC".Split(' '));
            return;


            Esegui("SAURO MBM41LIB_M DELETE POH-OFA".Split(' '));
            //Esegui("SAURO MBM41LIB_M ALLTIME POH-ODM COD=IT001-ODM180062%".Split(' '));
            //Esegui("SAURO MBM41LIB_M ALLTIME POH-OFA COD=IT001-OFA180063%".Split(' '));
            Esegui("SAURO MBM41LIB_M ALLTIME POH-ODM ".Split(' '));
            Esegui("SAURO MBM41LIB_M ALLTIME POH-OFA ".Split(' '));
            return;
            
            Esegui("SAURO MBM41LIB_M DELETE CIC".Split(' '));
            Esegui("SAURO MBM41LIB_M ALLTIME CIC".Split(' '));
            Esegui("SAURO MBM41LIB_M DELETE DISBAS".Split(' '));
            //Esegui("SAURO MBM41LIB_M ALLTIME DISBAS COD=MSB02005-0%".Split(' '));
            Esegui("SAURO MBM41LIB_M LAST=20 DISBAS".Split(' '));
            
#else
            Esegui(args);
#endif     
        }

        static void Esegui(string[] args)
        {
            ///
            //imposto il punto come separatore dei decimali, per accontentare Sql server (cyber plan frontiera)
            ///
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            ///
            ///

            bool _mode_all = false;
            bool _delete = false;

            //_logger.Info("START at " + DateTime.Now.ToString() + " ----------------argomenti: " + string.Join(" ", args) + " ------------------");
            _logger.Info("START - argomenti: " + string.Join(" ", args) + " ------------------");
            if (args[0].ToUpper() == "INIT_CYB")
            {   
                    CYBER_utils.Init();
                    return;   
            }
            else if (args[0].ToUpper() == "START")
            {
                CYBER_utils.SetStatus("Running");
                return;
            }
            else if (args[0].ToUpper() == "STOP")
            {
                CYBER_utils.SetStatus("Completed");
                return;
            }
            if (args.Length < 4)
            {
                _logger.Info("\n\n\nSintassi:\n" +
                    "SyncCyberPlan DOSSIER LIBRERIAAS400 DELETE|ALLTIME|LAST=N OGG [COD=CODICELIKE]\n\n" +   //parametri opzionali alla fine
                    "Comandi disponibili: \n" +

                    "DELETE per azzerare la tabella di frontiera di CyberPlane \n" +
                    "        (per gli oggetti XXX-YYY le prime lettere XXX identificano la tabella)\n" +
                    "ALLTIME  per ottenere tutti i valori\n" +
                    "LAST=N   per ottenere solo i valori degli ultimi N giorni\n" +
                    "OGG= \n" +
                    "      BPR terzi                           da sage a cyb\n" +
                    "      POH-ODM ordini di acquisto          da sage a cyb\n" +
                    //"      POH-OFA ordini di acquisto          da sage a cyb\n" +
                    "      ITM articolo                        da sage a cyb\n" +
                    "      MAC macchine                        da sage a cyb\n" +
                    "      ATT attrezzature                    da sage a cyb\n" +
                    "      CIC cicli da ITM                    da sage a cyb\n" +
                    "\n" +
                    "      POH-OPR ordini di produzione        da as400 a cyb\n" +
                    "      SOH ordini di vendita               da as400 a cyb\n" +
                    "      LOC locazioni magazzini             da as400 a cyb\n" +
                    "      GIAC giacenze magazzino PQM00PF     da as400 a cyb\n" +
                    "      GIAC-ALL giacenze allocate ORR00PF  da as400 a cyb\n" +
                    "      DISBAS distinta base SPR00PF        da as400 a cyb\n" +
                    "      DEM Fabbisogni OPR in corso MFC00PF da as400 a cyb\n" +
                    
                    "\n" +
                    "SyncCyberPlan START     prima di iniziare sync tabelle CyberPlan\n" +
                    "SyncCyberPlan STOP      alla fine del     sync tabelle CyberPlan\n" +
                    "SyncCyberPlan INIT_CYB  per inizializzare tabelle CyberPlan\n" +
                    "SyncCyberPlan INIT_CYB  per inizializzare tabelle CyberPlan\n" +

                    "COD = WP%  per ottenere un filtro sui codici\n"
                );
            }
            else
            {
                int indexargs = 0;
                string codicelike = "";
                string oggetto = "";
                string nomefile = "";


                ///////////////////ARG index 0-1
                string dossier = args[indexargs].ToUpper();
                indexargs++;
                string libreriaas400 = args[indexargs].ToUpper();

                ///////////////////ARG index 2
                indexargs++;
                string _cur_arg = args[indexargs].ToUpper();
                string nomefileTMP = "SyncCyberPlan_" + _cur_arg + "_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
                nomefileTMP = nomefileTMP.Replace("=", "");
                if (_cur_arg == "DELETE")
                {
                    _logger.Debug(_cur_arg);
                    _delete = true;

                }
                else if (_cur_arg == "ALLTIME")
                {
                    _logger.Debug(_cur_arg);
                    _mode_all = true;

                }
                else if (_cur_arg.StartsWith("LAST="))
                {
                    _logger.Debug(_cur_arg);
                    _mode_all = false;
                    if (!SetLASTDAYS(_cur_arg.Replace("LAST=", "")))
                    {
                        _logger.Error("parametro LAST non corretto");
                        return;
                    }
                }
                else
                {
                    _logger.Error("comando con parametri errati: " + string.Join(" ", args));
                    return;
                }

                indexargs++;
                oggetto = args[indexargs].ToUpper();
                if (oggetto == "ITM"
                    || oggetto == "LOC"
                    || oggetto == "MAC"
                    || oggetto == "ATT"
                    || oggetto == "CIC"
                    || oggetto == "BPR" 
                    || oggetto == "SOH" 
                    || oggetto == "POH-ODM" 
                    //|| oggetto == "POH-OFA"
                    || oggetto == "OPR"
                    || oggetto == "GIAC" 
                    || oggetto == "DISBAS"
                    || oggetto == "DEM"
                    )
                {

                }
                else
                {
                    _logger.Error("comando con parametro oggetto errato: " + string.Join(" ", args));
                    return;
                }

                ///////////////////ARG index >=N  parametri opzionali
                indexargs++;
                for (; indexargs < args.Length; indexargs++)
                {
                    _cur_arg = args[indexargs].ToUpper();
                    if (_cur_arg.StartsWith("COD=") && codicelike == "")
                    {
                        codicelike = _cur_arg.Substring(_cur_arg.IndexOf('=') + 1);
                        //_logger.Info("codice like = " + codicelike);
                    }
                    else if (nomefile == "")
                    {
                        _cur_arg = args[indexargs];//per non avere il ToUpper
                        nomefile = _cur_arg;
                    }
                    else
                    {
                        _logger.Error("Parametri errati");
                        return;
                    }
                }
                if (nomefile == "") nomefile = nomefileTMP;
                _logger.Debug(" nome file = " + nomefile);
                ///////////////////FINE ARG 


                DBHelper2 as400 = DBHelper2.getAs400DBHelper(libreriaas400);
                DBHelper2 sage = DBHelper2.getSageDBHelper(dossier);
                DBHelper2 cyber = DBHelper2.getCyberDBHelper();

                //SageTable_Manager sm = new SageTable_Manager(dossier);
                //As400Table_Manager am = new As400Table_Manager();
#if !DEBUG
                try
                {
#endif
                switch (oggetto)
                    {
                        //da sage
                        case "ITM":
                            sage.WriteToCyberPlan<Articolo>(_mode_all, codicelike, "", _delete, "");
                            //aggiunto vista FAMPEX    sm.WriteToCyberPlan<Articolo_Caratteristiche>(_mode_all, codicelike, "", _delete, "");
                            break;
                        case "CIC":
                            //ho aggiunto questo if per timore che quelli di CyberPlan non svuotino le tabelle
                            //sm.WriteToCyberPlan<Cicli_Routing_Header>(_mode_all, codicelike, "", _delete, "");     
                            if(_delete)  DBHelper2.EseguiSuDBCyberPlan(ref cyber, "DELETE FROM [CyberPlanFrontiera].[dbo].[CYB_STD_OPERATION] where 1=1 ");
                            //sm.WriteToCyberPlan<Std_Operation>(_mode_all, codicelike, "", _delete, "");      

                            sage.WriteToCyberPlan<Std_Op_Machine>(_mode_all, codicelike, "", _delete, "");
                            //sm.WriteToCyberPlan<Std_Op_Machine_ASSE>(_mode_all, codicelike, "", _delete, "");
                            sage.WriteToCyberPlan<Item_Routing>(_mode_all, codicelike, "", _delete, "");
                            //sm.WriteToCyberPlan<Item_Routing_ASSE>(_mode_all, codicelike, "", _delete, "");
                            //sm.WriteToCyberPlan<Item_Routing_PLAS>(_mode_all, codicelike, "", _delete, "");
                            break;
                        case "BPR": sage.WriteToCyberPlan<Terzo>(_mode_all, codicelike, "", _delete, ""); break;
                        case "POH-ODM": sage.WriteToCyberPlan<OrdiniAcq_ODM>(_mode_all, codicelike, "", _delete, ""); break;
                        //case "POH-OFA": sm.WriteToCyberPlan<OrdiniAcq_OFA>(_mode_all, codicelike, "", _delete, ""); break;
                        case "MAC": sage.WriteToCyberPlan<Macchina>(_mode_all, codicelike, "", _delete, ""); break;
                        case "ATT": //sm.WriteToCyberPlan<Attrezzature_ASSE>(_mode_all, codicelike, "", _delete, "");
                                    sage.WriteToCyberPlan<Attrezzature>(_mode_all, codicelike, "", _delete, "");
                                    sage.WriteToCyberPlan<Attrezzature_ConfigPlas>(_mode_all, codicelike, "", _delete, ""); 
                        break;


                        //da as400                    
                        case "LOC": as400.WriteToCyberPlan<Locazione>(_mode_all, codicelike, "", _delete, ""); break;
                        case "SOH": as400.WriteToCyberPlan<OrdiniVen_as400>(_mode_all, codicelike, "", _delete, ""); break;
                        case "OPR":
                            as400.WriteToCyberPlan<OrdiniAcq_As400_OPR>(_mode_all, codicelike, "", _delete, "");
                            as400.WriteToCyberPlan<Operations>(_mode_all, codicelike, "", _delete, "");
                        break;
                        case "GIAC": as400.WriteToCyberPlan<Giacenze_ORR00PF>(_mode_all, codicelike, "", _delete, ""); 
                                     as400.WriteToCyberPlan<Giacenze_PQM00PF>(_mode_all, codicelike, "", _delete, ""); //interne
                                     as400.WriteToCyberPlan<Giacenze_PQM00PF_esterne>(_mode_all, codicelike, "", _delete, "");
                        break;
                        case "DISBAS": as400.WriteToCyberPlan<DistintaBase>(_mode_all, codicelike, "", _delete, ""); break;
                        case "DEM": as400.WriteToCyberPlan<Demand_OPR_RIGHE>(_mode_all, codicelike, "", _delete, ""); break;


                    default: _logger.Error(_cur_arg + ": tipo articolo non previsto"); return;
                    }
#if !DEBUG
                }
                catch (Exception ex)
                {

                    _logger.Error(string.Join(" ",args) + "\n" + ex.ToString());
                    Utils.SendMail("it@sauro.net", "francesco.chiminazzo@sauro.net", "mail.sauro.net", string.Join(" ", args) + "\n\n\n" + ex.ToString(), true);
                }
#endif
            }
            //_logger.Info("END at   " + DateTime.Now.ToString() + " ----------------");
            _logger.Info("END ---------------------------------");
        }
        static void EseguiTutto()
        {
            Esegui("SAURO MBM41LIB_M DELETE LOC".Split(' '));
            Esegui("SAURO MBM41LIB_M DELETE MAC".Split(' '));
            Esegui("SAURO MBM41LIB_M DELETE ATT".Split(' '));
            Esegui("SAURO MBM41LIB_M DELETE ITM ".Split(' '));
            Esegui("SAURO MBM41LIB_M DELETE BPR".Split(' '));
            Esegui("SAURO MBM41LIB_M DELETE POH-ODM ".Split(' '));
            Esegui("SAURO MBM41LIB_M DELETE OPR ".Split(' '));
            Esegui("SAURO MBM41LIB_M DELETE SOH".Split(' '));
            Esegui("SAURO MBM41LIB_M DELETE GIAC".Split(' '));
            Esegui("SAURO MBM41LIB_M DELETE DISBAS".Split(' '));
            Esegui("SAURO MBM41LIB_M DELETE DEM".Split(' '));
            Esegui("SAURO MBM41LIB_M DELETE CIC".Split(' '));


            Esegui("SAURO MBM41LIB_M ALLTIME LOC".Split(' '));
            Esegui("SAURO MBM41LIB_M ALLTIME MAC".Split(' '));
            Esegui("SAURO MBM41LIB_M ALLTIME ATT".Split(' '));
            Esegui("SAURO MBM41LIB_M ALLTIME ITM ".Split(' '));
            Esegui("SAURO MBM41LIB_M ALLTIME BPR".Split(' '));
            Esegui("SAURO MBM41LIB_M ALLTIME POH-ODM ".Split(' '));
            Esegui("SAURO MBM41LIB_M ALLTIME OPR ".Split(' '));
            Esegui("SAURO MBM41LIB_M ALLTIME SOH".Split(' '));
            Esegui("SAURO MBM41LIB_M ALLTIME GIAC".Split(' '));
            Esegui("SAURO MBM41LIB_M ALLTIME DISBAS".Split(' '));
            Esegui("SAURO MBM41LIB_M ALLTIME DEM".Split(' '));
            Esegui("SAURO MBM41LIB_M ALLTIME CIC".Split(' '));
        }

        static public bool SetLASTDAYS(string numstr)
        {
            Item.SetLastDays(-1);
            int num = 0;
            if (int.TryParse(numstr, out num))
            {
                if (num > 0)
                {
                    Item.SetLastDays(num);
                    return true;
                }
            }
            return false;
        }
    }
}
