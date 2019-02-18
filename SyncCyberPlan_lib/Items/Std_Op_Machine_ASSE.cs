using System.Collections.Generic;
using System;
using log4net;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace SyncCyberPlan_lib
{
    public class Std_Op_Machine_ASSE: Std_Op_Machine
    {
        public Std_Op_Machine_ASSE(): base()
        {
        }
        public override void Init(object[] row)
        {
            _YCONATT_0        = getDBV<string>(row[0]);
            _YCONGRP_0        = getDBV<string>(row[1]);
            _WST_0            = getDBV<string>(row[2]);
            _YCONLOTSIZ_0     = getDBV<decimal>(row[3]);
            _YCONLOTUM_0      = getDBV<string>(row[4]);
            _YCONCAD_0        = getDBV<decimal>(row[5]);
            _YCONCADTIM_0     = getDBV<int>(row[6]);
            _YCONENAFLG_0     = getDBV<byte>(row[7]);
            _YCONDATRIA_0     = getSageDate(row[8]);
            _WCR_0            = getDBV<string>(row[9]);


            C_ROUTING_CODE = EscapeSQL(_YCONATT_0+"-"+_WST_0, 51);      // varchar 51    cif0001-cmf1
            C_ROUTING_ALT = "0";
            C_OPNUM = 10;
            C_ALT_OPNUM = 0;

            MACHINE_CODE = EscapeSQL(_WST_0, 30);      // varchar 30  //MACCHINA
            C_EFFECTIVE_DATE = _YCONDATRIA_0;
            C_EXPIRE_DATE = null;

            LOT_SIZE = getLotSize(_WCR_0, _YCONLOTSIZ_0, _YCONCAD_0);                     // numeric      
            PREFERENCE = 0;                     // int      
            RUN_TIME = _YCONCADTIM_0;                     // int      
            SETUP_TIME = 0;                     // int      
            WAIT_TIME = 0;                     // int      
            ACTIVE = (byte)(_YCONENAFLG_0 == 2 ? 1 : 0);
        }
        protected override decimal getLotSize(string flusso,decimal LotSize, decimal Cadenza)
        {
            //assemblaggio
            return LotSize * Cadenza;
        }

        public override List<DataRow> GetLastCyberRows()
        {
            return null;
        }
        public override string GetSelectQuery(bool mode, string dossier, string codice_like, string tipo)
        {
            string db = "x3." + dossier;

            //31 ottobre 2018: unica tabella (non si usa più CYB_STD_OPERATION)
            //RECUPERO qui le configurazioni attrezz/GRUPPO (senza macchina)
            //da cui ricavo tramite join
            //tutte le combinazioni GRUPPO/MACCHINA possibili


            //18 febbario 2019
            //per ASSEMBLAGGIO recupero configurazioni di Attrezzature ATTIVE
            //e CREO per CyberPlan attrezzatura-Macchina
            //dò per scontato che non ci sinao attrezzature associate a tutte le macchina: YCONCDL_0 <> ''
            string sage_query = @" SELECT
                      C.YCONATT_0
                     ,C.YCONGRP_0
                     ,W.WST_0
                     ,C.YCONLOTSIZ_0
                     ,C.YCONLOTUM_0
                     ,C.YCONCAD_0
                     ,C.YCONCADTIM_0
                     ,C.YCONENAFLG_0
                     ,C.YCONDATRIA_0
                     ,W.WCR_0
					 ,C.YCONCDL_0 
					 --,A.YATTWCR_0
                      from x3.SAURO.YPRDCONF C
					 left join SAURO.YPRDATT A on A.YATTCOD_0= C.YCONATT_0
                     join SAURO.WORKSTATIO W  on C.YCONGRP_0 = W.YGRP_0 and C.YCONCDL_0 = W.WST_0
                      where YCONCDL_0 <> ''
					  and W.WCR_0 = 'ASSE'
					  and A.YATTENAFLG_0=2
                      order by YCONATT_0, WST_0 desc ";


            //C.YCONENAFLG_0=2 and 
            //C.YCONCDL_0 <> '';
            //and YCONATT_0 like 'STP014%'
            return sage_query;
        }
   
    }
}
