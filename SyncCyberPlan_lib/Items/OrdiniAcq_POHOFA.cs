using System.Collections.Generic;
using System;
using log4net;
using System.Data;

namespace SyncCyberPlan_lib
{
    public class OrdiniAcq_OFA : OrdiniAcq_POH
    {
        public OrdiniAcq_OFA():base("OFA")
        {

        }
        public override void Init(object[] row)
        {
            base.Init(row);

            C_M_B = 'D'; //char             1                       //D=decentrato
            C_MRP_TYPE = 'C'; //char             1                  //F=MTS make to stock (a fabbisogno)  C= MTO make to order (a commessa)
        }
    }
}
