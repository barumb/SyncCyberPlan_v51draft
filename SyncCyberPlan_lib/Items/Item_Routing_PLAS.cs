using System.Collections.Generic;
using System;
using log4net;
using System.Data;

namespace SyncCyberPlan_lib
{
    public class Item_Routing_PLAS : Item_Routing
    {
        public Item_Routing_PLAS(): base()
        {
        }
        public override void Init(object[] row)
        {
            _ITMREF_0   = getDBV<string>(row[0]);
            _YATTCOD_0  = getDBV<string>(row[1]);
            _YPRI_0     = getDBV<short>(row[2]);
            _YENAFLG_0  = getDBV<byte>(row[3]);
            //_YDATRIA_0  = getSageDate(row[4]);
            _YPLAIMP_0  = getDBV<short>(row[5]);
            _YPLADIV_0  = getDBV<byte>(row[6]);
            _YCAD_0     = getDBV<decimal>(row[7]);
            _YCADTEM_0  = getDBV<int>(row[8]);
            _ITMSTA_0   = getDBV<byte>(row[9]);
            _YWCR_0     = getDBV<string>(row[10]);

            C_ITEM_CODE = EscapeSQL(_ITMREF_0, 50);           //varchar 50
            C_ITEM_PLANT = EscapeSQL("ITS01", 20);           //varchar 20
            C_ROUTING_CODE = EscapeSQL(_YATTCOD_0, 51);           //varchar 51
            C_ROUTING_ALT = EscapeSQL(_YPRI_0.ToString(), 9);            //varchar 9
            C_NSEQ = 0;                          //int        
            C_EFFECTIVE_DATE = _YDATRIA_0;                       //datetime   
            C_EXPIRE_DATE = null;                       //datetime   

            C_USER_FLAG01 = (byte)(_YPLADIV_0 == 2 ? 1 : 0);
            C_USER_FLAG02 = 0;
            C_USER_STRING01 = _YPLAIMP_0.ToString(); ;
            C_USER_STRING02 = "";
            C_USER_STRING03 = "";
            C_USER_STRING04 = "";

            C_LOT_SIZE = _YCAD_0;   //per assemblaggio        vie
            C_RUN_TIME = _YCADTEM_0;//per assemblaggio        al minuto (se qui =60)
        }
        public override string GetSelectQuery(bool mode, string dossier, string codice_like, string tipo)
        {
            string db = "x3." + dossier;
            //string sage_query = "SELECT A.ITMREF_0, ITMSTA_0 from " + dossier + ".[ITMMASTER] A join " + dossier + ".[YITMINF] B on A.ITMREF_0=B.ITMREF_0" +
            //    " WHERE YLIVTRAS_0='" + tipo + "' and ITMSTA=1 ";

            //NB: messo Join non left o full: 
            //left potrebbe portare dei null, ma qui servono solo dati completi (se per es. un'attrezzatura non esiste in YPRDATT non deve essere esportata la riga)
            //full non ha senso: parto dalla YPRDITM, se non c'è qui non ha senso esportare record
            
            string sage_query =
  @"SELECT   I.ITMREF_0
        ,I.YATTCOD_0
        ,I.YPRI_0
        ,I.YENAFLG_0
        ,'c era I.YDATRIA_0'
        ,I.YPLAIMP_0
        ,I.YPLADIV_0  
        ,I.YCAD_0
        ,I.YCADTEM_0
		,M.ITMSTA_0
		,F.YWCR_0
        from " + db + @".YPRDITM I
		join " + db + @".ITMMASTER M
			on I.ITMREF_0 = M.ITMREF_0
		join " + db + @".ITMFACILIT F
		    on I.ITMREF_0=F.ITMREF_0 and F.STOFCY_0='ITS01'
        join " + db + @".YPRDATT A 
            on A.YATTCOD_0= I.YATTCOD_0
        where I.YENAFLG_0=2 
        and A.YATTENAFLG_0=2
		and F.YWCR_0 = 'PLAS' "
            ;

            if (!string.IsNullOrWhiteSpace(codice_like))
            {
                sage_query += " and ITMREF_0 like '" + codice_like.Trim() + "' \n";
            }

            sage_query += " ORDER BY ITMREF_0 ";
            return sage_query;
        }
    }
}
