using System.Collections.Generic;
using System;
using log4net;
using System.Data;

namespace SyncCyberPlan_lib
{
    public class Attrezzature_ASSE : Attrezzature
    {

        public Attrezzature_ASSE( )//: base("CYB_TOOL")
        {

        }

        
        public override string GetSelectQuery(bool mode, string dossier, string codice_like, string tipo)
        {
            string db = "x3." + dossier;
            string sage_query = @"SELECT
                      C.YCONATT_0+'-'+C.YCONCDL_0 as YATTCOD_0
					 ,A.YATTDES_0
					 ,A.YATTQTY_0
					 ,A.YATTVIN_0

					 ,A.YATTMAT_0
					 ,A.YATTFLGPA66_0
					 ,A.YATTFLGSTH_0
					 ,A.YATTMARSAU_0
					 ,A.YATTMARTYC_0
					 ,A.YATTMARHAR_0
					 ,A.YATTQUA_0
					 ,A.YATTTYP_0

					 ,A.YATTDATSAU_0
					 ,A.YATTDATTYC_0
					 ,A.YATTDATHAR_0
					 ,A.YATTVP_0					 
					 
                      from " + db + @".YPRDCONF C
					 left join " + db + @".YPRDATT A on A.YATTCOD_0= C.YCONATT_0
                     
                      where YCONCDL_0 <> ''
					  
                      and A.YATTWCR_0 ='ASSE'
					  and A.YATTENAFLG_0=2
                      order by YCONATT_0, YATTWCR_0 desc ";

            //join " + db + @".WORKSTATIO W  on C.YCONGRP_0 = W.YGRP_0 and C.YCONCDL_0 = W.WST_0
            //and S.YATTWCR_0 <>'ASSE' dovrebbe essere ininfluente

            //if (!string.IsNullOrWhiteSpace(codice_like))
            //{
            //    sage_query += " and B.BPRNUM_0 like '" + codice_like.Trim() + "'";
            //}
            return sage_query;
        }
    }
}
