using System.Collections.Generic;
using System;
using log4net;
using System.Data;

namespace SyncCyberPlan_lib
{
    public class OrdiniAcq_ODM : OrdiniAcq_POH
    {
        public OrdiniAcq_ODM(): base("ODM")
        {

        }

        public override void Init(object[] row)
        {
            base.Init(row);

            C_M_B = 'B'; //char             1         B=buy              
            C_MRP_TYPE = 'C'; //char             1    //F=MTS make to stock (a fabbisogno)  C= MTO make to order (a commessa)                    
        }
    }
}
