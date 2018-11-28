using System.Collections.Generic;
using System;
using log4net;
using System.Data;

namespace SyncCyberPlan_lib
{
    public class ECYB_init 
    {
        protected static readonly ILog _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name);
        public ECYB_init()
        {

        }


        static public void Action()
        {
            DBHelper2 db = DBHelper2.getCyberDBHelper();
            string exec_store_precoderure = "EXECUTE [CyberPlanFrontiera].[dbo].[FILL_E_ROUTING]";
            _logger.Info("start execution");
            
            int i = DBHelper2.EseguiSuDBCyberPlan(ref db, exec_store_precoderure, 600);
            _logger.Info("end execution");
        }
    }
}
