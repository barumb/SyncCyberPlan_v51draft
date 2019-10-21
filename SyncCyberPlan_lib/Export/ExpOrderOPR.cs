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
        public ExpOrderOPR():base("OPR")
        {
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
            //A;ITS01;1;WP7467-MV;310719;010819;1000;;WWCICLO00;1;C;2
            //B;5;1000;P27;310719;310720;14400;3750
            //B;10;1000;P28;300919;310721;14400;3750
            //C;testcodbbb

            //YMFG modifica
            //A;ITS01;1;WP7467-MV;310719;010819;1000;ODP19ITS0100109;WWCICLO00;1;M;2
            //B;5;1000;P20;310725;310725;123;333
            //B;10;1000;P21;300925;310725;12300;344
            //C;testcodbbb


            return
                "A" +
                __SEP + "ITS01" +
                __SEP + "1" +
                __SEP + ORD_C_ITEM_CODE +
                __SEP + string.Format("{0:ddMMyy}", ORD_C_STDATE) +
                __SEP + string.Format("{0:ddMMyy}", ORD_C_DUEDATE) +
                __SEP + ORD_C_QTY +
                __SEP + getMFG_NumeroOrdine(ORD_C_CODE) +
                __SEP + "WWCICLO00" +
                __SEP + "1" +
                __SEP + getFlagCreaModifica(ORD_C_CODE) +
                __SEP + "2" +   //MFGSTA  2= Pianificato  1= Confermato
                "\nB" +
                __SEP + OPE_C_OPNUM +
                __SEP + OPE_C_QTY +
                __SEP + OPE_C_MACHINE +
                __SEP + string.Format("{0:ddMMyy}", OPE_C_STDATE) +
                __SEP + string.Format("{0:ddMMyy}", OPE_C_DUEDATE) +
                __SEP + OPE_C_SETUP_TIME +
                __SEP + OPE_C_RUN_TIME +
                "\nC" +
                __SEP + getCyberCode(ORD_C_CODE);
                //MFGMAT non viene importata   "\nD" +
                //MFGMAT non viene importata   //__SEP + DEM_MFGLIN +
                //MFGMAT non viene importata   __SEP + DEM_C_NSEQ +
                //MFGMAT non viene importata   __SEP + DEM_C_ITEM_CODE +
                //MFGMAT non viene importata   __SEP + DEM_C_QTY +
                //MFGMAT non viene importata   __SEP + (DEM_C_QTY/ORD_C_QTY) +  //qta base = qta di legame                
                ;
        }
        string getMFG_NumeroOrdine(string ORD_C_CODE)
        {
            if (ORD_C_CODE.ToUpper().StartsWith("OPR"))
            {
                return ORD_C_CODE; //OPR già esistente, simao in modifica
            }
            else
            {
                return ""; //Proposta nuova, non esistente, simao in creazione
            }
        }
        string getFlagCreaModifica(string ORD_C_CODE)
        {
            if (ORD_C_CODE.ToUpper().StartsWith("OPR"))
            {
                return "M"; //flag di modifica
            }
            else
            {
                return "C"; //flag di creazione
            }
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
            ret += " " + string.Format("{0:ddMMyy_hhmm}", System.DateTime.Now.Date); //flag di modifica
            return ret;
        }
    }
}
