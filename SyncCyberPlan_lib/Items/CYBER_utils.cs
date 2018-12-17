using System.Collections.Generic;
using System;
using log4net;
using System.Data;

namespace SyncCyberPlan_lib
{
    public static class CYBER_utils 
    {
        static readonly ILog _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name);
        public enum Status
        {
            Read, Running, Completed
        }        

        static public void SetStatus(string stato)
        {
            DBHelper2 db = DBHelper2.getCyberDBHelper();
            string command = "UPDATE [CyberPlanFrontiera].[dbo].[TRANSFER_STATUS] " 
                                          + " SET [STATUS] = '" + stato + "'" 
                                          + " WHERE ID='TR_STATUS'  ";
            _logger.Info("start execution");

            int i = DBHelper2.EseguiSuDBCyberPlan(ref db, command, 600);
            _logger.Info("end execution");
        }

        static public void Init()
        {
            DBHelper2 db = DBHelper2.getCyberDBHelper();
            string exec_store_procedure = "EXECUTE [CyberPlanFrontiera].[dbo].[FILL_E_ROUTING]";
            _logger.Info("start execution");
            
            int i = DBHelper2.EseguiSuDBCyberPlan(ref db, exec_store_procedure, 600);
            _logger.Info("end execution");
        }
    }
}
