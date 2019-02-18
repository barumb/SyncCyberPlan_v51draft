using System.Collections.Generic;
using System;
using log4net;
using System.Data;

namespace SyncCyberPlan_lib
{
    public abstract class Item_Routing : Item
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

        public byte _ITMSTA_0; //stato articolo
        public string _YWCR_0;   //reparto articolo

        public string _YCONCDL_0;   //Macchina
        //public int _YCONCAD_0;   //cadenza
        //public int _YCONCADTIM_0;//tempo cadenza

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
     
        public override string GetID()
        {
            return C_ITEM_CODE + C_ITEM_PLANT + C_ROUTING_CODE + C_ROUTING_ALT + " " + C_NSEQ;
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
