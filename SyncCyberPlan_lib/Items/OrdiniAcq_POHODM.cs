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

        public override DataRow GetCyberRow()
        {
            DataRow _tablerow = base.GetCyberRow();

            C_M_B = 'B'; //char             1                       
            C_MRP_TYPE = 'C'; //char             1                       

            //DataRow _tablerow = _dataTable.Rows[_dataTable.Rows.Count - 1];
            _tablerow[4] = C_M_B;                              //C_M_B                        char             1                       
            _tablerow[5] = C_MRP_TYPE;                              //C_MRP_TYPE                   char             1                       

            return _tablerow;
        }
    }
}
