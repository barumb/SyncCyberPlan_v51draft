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

        public override DataRow GetCyberRow()
        {
            DataRow _tablerow = base.GetCyberRow();

            C_M_B = 'D'; //char             1                       //D=decentrato
            C_MRP_TYPE = 'C'; //char             1                  //F=MTS make to stock (a fabbisogno)  C= MTO make to order (a commessa)

            //DataRow _tablerow = _dataTable.Rows[_dataTable.Rows.Count - 1];
            _tablerow[4] = C_M_B;                              //C_M_B                        char             1                       
            _tablerow[5] = C_MRP_TYPE;                              //C_MRP_TYPE                   char             1                       

            return _tablerow;
        }
    }
}
