using System.Collections.Generic;
using System;
using log4net;
using System.Data;

namespace SyncCyberPlan_lib
{
    public class Item_Routing : Item
    {
        public string    _ITMREF_0;
        public string    _YATTCOD_0;
        public short     _YPRI_0   ;
        public byte      _YENAFLG_0;
        public DateTime? _YDATRIA_0;
        public short     _YPLAIMP_0;//PLAS
        public byte      _YPLADIV_0;//PLAS

        public decimal _YCAD_0;    //per assemblaggio
        public int _YCADTEM_0; //per assemblaggio


        #region tabella output CYB_ITEM_ROUTING
        public string    C_ITEM_CODE;                       //varchar 50
        public string    C_ITEM_PLANT;                      //varchar 20
        public string    C_ROUTING_CODE;                    //varchar 51
        public string    C_ROUTING_ALT;                     //varchar 9
        public int       C_NSEQ;                            //int                                                 
        public DateTime? C_EFFECTIVE_DATE;                  //datetime                                                 
        public DateTime? C_EXPIRE_DATE;                     //datetime                                                 

        public byte C_USER_FLAG01; // bit	
        public byte C_USER_FLAG02; // bit	
        public string C_USER_STRING01; // varchar 29
        public string C_USER_STRING02; // varchar 29
        public string C_USER_STRING03; // varchar 29
        public string C_USER_STRING04; // varchar 29

        public decimal C_LOT_SIZE; //decimal  per assemblaggio
        public int C_RUN_TIME; //int      per assemblaggio
        #endregion


        public Item_Routing(): base("CYB_ITEM_ROUTING")
        {
        }
        public override void Init(object[] row)
        {
            _ITMREF_0   = getDBV<string>(row[0]);
            _YATTCOD_0  = getDBV<string>(row[1]);
            _YPRI_0     = getDBV<short>(row[2]);
            _YENAFLG_0  = getDBV<byte>(row[3]);
            _YDATRIA_0  = getSageDate((DateTime)row[4]);
            _YPLAIMP_0  = getDBV<short>(row[5]);
            _YPLADIV_0  = getDBV<byte>(row[6]);
            _YCAD_0     = getDBV<decimal>(row[7]);
            _YCADTEM_0  = getDBV<int>(row[8]);


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
        public override DataRow GetCyberRow()
        {
            DataRow _tablerow = _dataTable.NewRow();

            _tablerow[0] = C_ITEM_CODE;                       //varchar 50
            _tablerow[1] = C_ITEM_PLANT;                      //varchar 20
            _tablerow[2] = C_ROUTING_CODE;                    //varchar 51
            _tablerow[3] = C_ROUTING_ALT;                     //varchar 9
            _tablerow[4] = C_NSEQ;                            //int        
            _tablerow[5] = DateTime_toCyb(C_EFFECTIVE_DATE);
            _tablerow[6] = DateTime_toCyb(C_EXPIRE_DATE);     //datetime   
            _tablerow[7] = C_USER_FLAG01 ;
            _tablerow[8] = C_USER_FLAG02  ;
            _tablerow[9] = C_USER_STRING01;
            _tablerow[10] = C_USER_STRING02;
            _tablerow[11] = C_USER_STRING03;
            _tablerow[12] = C_USER_STRING04;
            _tablerow[13] = C_LOT_SIZE;
            _tablerow[14] = C_RUN_TIME;

            return _tablerow;
        }
        public override string GetSelectQuery(bool mode, string dossier, string codice_like, string tipo)
        {
            string db = "x3." + dossier;
            //string sage_query = "SELECT A.ITMREF_0, ITMSTA_0 from " + dossier + ".[ITMMASTER] A join " + dossier + ".[YITMINF] B on A.ITMREF_0=B.ITMREF_0" +
            //    " WHERE YLIVTRAS_0='" + tipo + "' and ITMSTA=1 ";

            string sage_query =
  @"select 
         ITMREF_0
        ,YATTCOD_0
        ,YPRI_0
        ,YENAFLG_0
        ,YDATRIA_0
        ,YPLAIMP_0
        ,YPLADIV_0  
        ,YCAD_0
        ,YCADTEM_0
        from SAURO.YPRDITM
        where YENAFLG_0=2 "
            ;

            if (!string.IsNullOrWhiteSpace(codice_like))
            {
                sage_query += " and ITMREF_0 like '" + codice_like.Trim() + "' \n";
            }

            sage_query += " ORDER BY ITMREF_0 ";
            return sage_query;
        }
        public override string GetID()
        {
            return C_ITEM_CODE + C_ITEM_PLANT + C_ROUTING_CODE + C_ROUTING_ALT + C_NSEQ;
        }

        public override void InitDataTable()
        {
            _dataTable.Columns.Add("C_ITEM_CODE"     , typeof(string));
            _dataTable.Columns.Add("C_ITEM_PLANT"    , typeof(string));
            _dataTable.Columns.Add("C_ROUTING_CODE"  , typeof(string));
            _dataTable.Columns.Add("C_ROUTING_ALT"   , typeof(string));
            _dataTable.Columns.Add("C_NSEQ"          , typeof(int));
            _dataTable.Columns.Add("C_EFFECTIVE_DATE", typeof(DateTime));
            _dataTable.Columns.Add("C_EXPIRE_DATE"   , typeof(DateTime));

            _dataTable.Columns.Add("C_USER_FLAG01 "   , typeof(byte	));
            _dataTable.Columns.Add("C_USER_FLAG02 "   , typeof(byte));
            _dataTable.Columns.Add("C_USER_STRING01 "   , typeof( string));
            _dataTable.Columns.Add("C_USER_STRING02 "   , typeof( string));
            _dataTable.Columns.Add("C_USER_STRING03 "   , typeof( string));
            _dataTable.Columns.Add("C_USER_STRING04 "   , typeof(string));

            _dataTable.Columns.Add("C_LOT_SIZE", typeof(decimal));
            _dataTable.Columns.Add("C_RUN_TIME", typeof(int));
        }
    }
}
