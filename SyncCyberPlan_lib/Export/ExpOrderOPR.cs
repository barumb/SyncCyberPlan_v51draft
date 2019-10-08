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
        public ExpOrderOPR()
        {
            _file_prefix = "OPR";
        }

        protected override string WhereCondition()
        {
            return " AND C_M_B='M' ";

            //C_M_B = M Make  produzione
            //C_M_B = B Buy acquisto
            //C_M_B = D Distribuited contolavoro
        }


        public override string getSageImportString()
        {
            //"ITS01",1,"ABF030LT-0V",270819,270819,1001,"","WWCICLO00","1","C","1"
            return
                "ITS01"
                + __SEP + "1"
                + __SEP + C_ITEM_CODE +
                __SEP + string.Format("{0:ddMMyy}", C_STDATE) +
                __SEP + string.Format("{0:ddMMyy}", C_DUEDATE) +
                __SEP + C_QTY +
                __SEP + "" +
                __SEP + "WWCICLO00" +
                __SEP + "1" +
                __SEP + "C" +
                __SEP + "1";
        }
    }
}
