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
    /// <summary>
    /// ordine di contolavoro
    /// </summary>
    public class ExpOrderOCL : ExpOrder
    {
        public ExpOrderOCL():base("OCL", X3WSUtils.TipoImport.DADEFINIRE)
        {
        }

        protected override string WhereCondition()
        {
            return " AND O.C_M_B='D' ";

            //C_M_B = M Make  produzione
            //C_M_B = B Buy acquisto
            //C_M_B = D Distribuited contolavoro
        }


        public override string getSageImportString()
        {
            throw new NotImplementedException();
        }
    }
}
