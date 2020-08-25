using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.Data;
using System.Data.Common;

namespace SyncCyberPlan_lib
{
    public class ExpOrderOPR : ExpOrder
    {
        static bool _CreaOprDaAs400;

        public ExpOrderOPR(bool creaOprDaAs400) :base("OPR", X3WSUtils.TipoImport.OPR)
        {
            _CreaOprDaAs400 = creaOprDaAs400;
        }

        protected override string WhereCondition()
        {
            return " AND O.C_M_B='M' ";

            //C_M_B = M Make  produzione
            //C_M_B = B Buy acquisto
            //C_M_B = D Distribuited contolavoro
        }

        /// <summary>
        /// Ritorna una riga per il Modello di Import YMFG
        /// </summary>
        /// <returns></returns>
        public override string getSageImportString()
        {
            //YMFG  creazione
            //A;ITS01;1;WP7467-MV;310719;010819;1000;;WWCICLO00;1;C;2;ODV,rigaODV
            //B;5;1000;STP122;P27;310719;310720;14400;3750
            //B;10;1000;STP123;P28;300919;310721;14400;3750
            //C;testcodbbb;tskNUM

            //YMFG modifica
            //A;ITS01;1;WP7467-MV;310719;010819;1000;ODP19ITS0100109;WWCICLO00;1;M;2;ODV,rigaODV
            //B;5;1000;STP133;P20;310725;310725;123;333
            //B;10;1000;STP144;P21;300925;310725;12300;344
            //C;testcodbbb;tskNUM


            //manca FATTIBIILITA': Savietto la può passare tramite il codice Esadecimale del colore

            return
                "A" +                                                              //

                __SEP + "ITS01" +                                                  //
                __SEP + "1" +                                                      //BOMALT
                                                                                   //__SEP + OPE_C_OPNUM +                                              //BOMOPE = openum
                __SEP + ORD_C_ITEM_CODE +                                          //
                __SEP + string.Format("{0:ddMMyy}", ORD_C_STDATE) +                //
                __SEP + string.Format("{0:ddMMyy}", ORD_C_DUEDATE) +               //
                __SEP + ORD_C_QTY +                                                //
                __SEP + getMFG_NumeroOrdine(ORD_C_CODE) +                          //numero ODP
                __SEP + getCICLO_UOM(C_UOM) +                                      //*1
                __SEP + "1" +                                                      //*2
                __SEP + getFlagCreaModifica(ORD_C_CODE) +                          //*3
                __SEP + getMFGSTA(ORD_C_CODE) +                                    //*4  //MFGSTA  2= Pianificato  1= Confermato
                __SEP + getOrdineCommessa(ORD_C_CORDER_CODE) +                     //
                __SEP + getRigaOrdineCommessa(ORD_C_CORDER_CODE) +                 //
                __SEP + ORD_task_number +                                          // by umb per avere YCYBTSK
                __SEP + getCyberCode(ORD_C_CODE) +                                 // by umb per avere YCYBCOD
                __SEP + "" +                                                       // by umb per avere YAS4COD
                "\nB" +                                                            //
                __SEP + OPE_C_OPNUM +                                              //OPENUM
                __SEP + "1" +                                                      //EXTWSTNBR, numero macchine, fisso 1
                __SEP + C_UOM +                                                    //OPEUOM Unita di misura operazione, fisso PZ per ora
                __SEP + "1" +                                                     //OPESTUCOE coeff. tra Unità di misura operazione e UM articolo, 
                __SEP + OPE_C_QTY +                                                //
                __SEP + OPE_C_ATTREZZATURA +                                       //
                __SEP + getMacchina(OPE_C_MACHINE, OPE_C_WORKCENTER_CODE) +        //
                __SEP + string.Format("{0:ddMMyy}", OPE_C_STDATE) +                //
                __SEP + string.Format("{0:ddMMyy}", OPE_C_DUEDATE) +               //
                __SEP + OPE_C_SETUP_TIME / 60 +   //da secondi a minuti            //
                __SEP + OPE_C_RUN_TIME / 60 +     //da secondi a minuti            //
                                                  // NON LO IMPORTA, NON C?é IN MASCHERA __SEP + getMFGSTA(ORD_C_CODE) +                                    //*4  //MFGSTA  2= Pianificato  1= Confermato (per le operazioni)
                __SEP + "1" +                                                      //  1= NO contolavoro 2= contolavoro strtturale  3= CL congiunturale
                __SEP + ORD_task_number +                                          // by umb per avere YCYBTSK
                __SEP + getCyberCode(ORD_C_CODE) +                                                       // by umb per avere YCYBCOD
                __SEP + "" +                                                       // by umb per avere YAS4COD
                "\nC" +                                                            //
                __SEP + ORD_task_number +                                          // by umb per avere YCYBTSK
                __SEP + getCyberCode(ORD_C_CODE) +                                 // by umb per avere YCYBCOD
                __SEP + "" ;                                                       // by umb per avere YAS4COD
               //MFGMAT non viene importata   "\nD" +
               //MFGMAT non viene importata   //__SEP + DEM_MFGLIN +
               //MFGMAT non viene importata   __SEP + DEM_C_NSEQ +
               //MFGMAT non viene importata   __SEP + DEM_C_ITEM_CODE +
               //MFGMAT non viene importata   __SEP + DEM_C_QTY +
               //MFGMAT non viene importata   __SEP + (DEM_C_QTY/ORD_C_QTY) +  //qta base = qta di legame                
                ;
        }
        string getCICLO_UOM(string ITEM_UOM)
        {
            return "WWCICLO00"+ ITEM_UOM;
        }

        string getMFG_NumeroOrdine(string ORD_C_CODE)
        {
            if (ORD_C_CODE.ToUpper().StartsWith("OPR"))
            {
                return ORD_C_CODE; //OPR già esistente, simao in modifica
            }
            else
            {
                return ""; //Proposta nuova, non esistente, siamo in creazione
            }
        }
        string getFlagCreaModifica(string ORD_C_CODE)
        {
            if (ORD_C_CODE.ToUpper().StartsWith("OPR"))
            {
                if (_CreaOprDaAs400)
                    return "C"; //creazione degli OPR che gà esistono in As400; per la fase iniziale
                else
                    return "M"; //flag di modifica
            }
            else
            {
                return "C"; //flag di creazione
            }
        }
        string getMFGSTA(string ORD_C_CODE)
        {
            if (ORD_C_CODE.ToUpper().StartsWith("OPR") || ORD_C_CODE.ToUpper().StartsWith("ODP") )
                return "1";
            else
                //MFGSTA  2= Pianificato  1= Confermato
                return "2";
        }
        string getCyberCode(string ORD_C_CODE)
        {
            string ret = "";
            if (ORD_C_CODE.ToUpper().StartsWith("OPR"))
            {
                ret=  "sync"; //flag di modifica
            }
            else
            {
                ret = ORD_C_CODE; //flag di creazione
            }
            //ret += " " + string.Format("{0:ddMMyy_hhmm}", System.DateTime.Now.Date); //flag di modifica
            ret += " " + string.Format("{0:ddMMyy}", System.DateTime.Now.Date); //flag di modifica
            return ret;
        }
        string getOrdineCommessa(string p_ORD_C_CORDER_CODE)
        {
            //ORC190001430001 = ORC19000143-0001
            if (p_ORD_C_CORDER_CODE == "Y0000" || p_ORD_C_CORDER_CODE == "X0000")
            {
                return "";
            }
            else
            {
                return  p_ORD_C_CORDER_CODE.Substring(0,11);
            }
        }
        string getRigaOrdineCommessa(string p_ORD_C_CORDER_CODE)
        {
            //ORC190001430001 = ORC19000143-0001
            if (p_ORD_C_CORDER_CODE == "Y0000" || p_ORD_C_CORDER_CODE == "X0000")
            {
                return "";
            }
            else
            {
                return p_ORD_C_CORDER_CODE.Substring(11, 4);
            }
        }
        string getMacchina(string OPE_C_MACHINE, string C_WORKCENTER_CODE)
        {
            //se la macchina è null va preso il CDL: è il caso delle macchine manuali alle quali come CDL CyberPlan mette NOMEMACCHINA_M
            if (string.IsNullOrWhiteSpace(OPE_C_MACHINE))
            {
                if (C_WORKCENTER_CODE.EndsWith("_M"))
                {
                    return C_WORKCENTER_CODE.Substring(0, C_WORKCENTER_CODE.Length - 2);
                }
                else return C_WORKCENTER_CODE;
            }
            else
            {
                return OPE_C_MACHINE;
            }
        }
    }
}
