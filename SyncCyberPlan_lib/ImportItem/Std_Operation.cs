using System.Collections.Generic;
using System;
using log4net;
using System.Data;

namespace SyncCyberPlan_lib
{
    public class Std_Operation : Item
    {
        public string   _COD_ATTREZZ;
        public string   _DESC_ATTREZZ;
        public int      _YSTAIMP_0   ;
        public decimal  _YSTASETUP_0 ;
        public decimal  _YSTACONFIG_0;
        public decimal  _YCONLOTSIZ_0;
        public string   _YGRP_0;     //GRUPPO macchine (CDL in sage)
        public int  _YCONCADTIM_0;
        public string   _YATTLOTUM_0;
        public string   _Yfornitore_BPS_0;
        public string   _YATTWCR_0;
        public decimal   _YCONCAD_0;  //CADENZA
        

        #region tabella output CYB_STD_OPERATION
        public string  C_ROUTING_CODE;                 //varchar    51
        public string  C_ROUTING_ALT;                  //varchar    9
        public int     C_OPNUM;                        //int     
        public string  C_ALT_OPNUM;                    //varchar    9
        public string  C_DESCR;                        //varchar    30
        public int     C_QUEUE_TIME;                   //int     
        public int     C_SETUP_TIME;                   //int     
        public int     C_RUN_TIME;                     //int     
        public int     C_WAIT_TIME;                    //int     
        public int     C_DURATION;                     //int     
        public decimal C_LOT_SIZE;                     //numeric    
        public char    C_SCRAP_TYPE;                   //char    1
        public double  C_SCRAP;                        //real    
        public string  C_OP_GROUP;                     //varchar    8
        public string  C_SETUP_GROUP_CODE;             //varchar    20
        public float   C_SETUP_TEAM_GROUP_QTY;         //float    
        public float   C_RUN_TEAM_GROUP_QTY;           //float    
        public string  C_WORKCENTER_CODE;              //varchar    20
        public string  C_HOST_WC;                      //varchar    20
        public string  C_SUPPLIER_CODE;                //varchar    30
        public char    C_HIERARCHICAL_POSITION;        //char    1
        public string  C_USER_NOTE01;                  //varchar    99
        public int     C_USER_INT01;                   //int     
        public double  C_USER_REAL01;                  //float    
        public string  C_USER_STRING01;                //varchar    29   //per Assemblaggio Flag Via/pezzo
        public string  C_USER_STRING02;                //varchar    29
        public int     C_USER_TIME01;                  //int     
        public int     C_USER_TIME02;                  //int     

        #endregion


        public Std_Operation(): base("CYB_STD_OPERATION")
        {
            throw new Exception("NON USATA"); // 31 ottobre 2018
        }

        public override void Init(object[] row)
        {
            _COD_ATTREZZ          = getDBV<string>(row[0], "COD_ATTREZZ");
            _DESC_ATTREZZ         = getDBV<string>(row[1], "DESC_ATTREZZ");
            _YSTAIMP_0            = (int)getDBV<short>(row[2], "YSTAIMP_0");
            _YSTASETUP_0          = getDBV<decimal>(row[3], "YSTASETUP_0");
            _YSTACONFIG_0         = getDBV<decimal>(row[4], "YSTACONFIG_0");
            _YCONLOTSIZ_0         = getDBV<decimal>(row[5], "YCONLOTSIZ_0");
            _YGRP_0               = getDBV<string>(row[6], "YGRP_0");      // Centro di lavoro
            _YCONCADTIM_0         = getDBV<int>(row[7], "YCONCADTIM_0");          // tempo della CADENZA 
            _YATTLOTUM_0          = getDBV<string>(row[8], "YATTLOTUM_0");      // Vie/pezzo
            _Yfornitore_BPS_0     = getDBV<string>(row[9], "Yfornitore_BPS_0");      // fornitore
            _YATTWCR_0           = getDBV<string>(row[10], "YATTWCR_0");     // reparto 
            _YCONCAD_0            = getDBV<decimal>(row[11], "YCONCAD_0");   //  CADENZA          

            C_ROUTING_CODE = EscapeSQL(_COD_ATTREZZ, 51);                    //varchar    51
            C_ROUTING_ALT = EscapeSQL("0", 9);                              //varchar    9
            C_OPNUM = 10;                                             //int     
            C_ALT_OPNUM = EscapeSQL("0", 9);                              //varchar    9
            C_DESCR = EscapeSQL(_YATTWCR_0, 30);                     //varchar    30
            C_QUEUE_TIME = 0;                                              //int     
            C_SETUP_TIME = (int)_YSTASETUP_0;                              //int     
            C_RUN_TIME = _YCONCADTIM_0;                                 //int     
            C_WAIT_TIME = 0;                                              //int     
            C_DURATION = 0;                                              //int     
            C_LOT_SIZE = _YCONLOTSIZ_0 * _YCONCAD_0;                                    //numeric    
            C_SCRAP_TYPE = ' ';                                            //char    1
            C_SCRAP = 0;                                              //real    
            C_OP_GROUP = EscapeSQL("", 8);                               //varchar    8
            C_SETUP_GROUP_CODE = EscapeSQL("", 20);                              //varchar    20
            C_SETUP_TEAM_GROUP_QTY = 0;                                              //float    
            C_RUN_TEAM_GROUP_QTY = 0;                                              //float    
            C_WORKCENTER_CODE = EscapeSQL("", 20);                     //varchar    20
            C_HOST_WC = EscapeSQL("", 20);                              //varchar    20
            C_SUPPLIER_CODE = EscapeSQL(_Yfornitore_BPS_0, 30);                     //varchar    30
            C_HIERARCHICAL_POSITION = ' ';                                            //char    1
            C_USER_NOTE01 = EscapeSQL("", 99);                              //varchar    99
            C_USER_INT01 = 0;                                              //int     
            C_USER_REAL01 = 0;                                              //float    
            C_USER_STRING01 = EscapeSQL(_YATTLOTUM_0, 29);                      //varchar    29    //per Assemblaggio Flag Via/pezzo
            C_USER_STRING02 = EscapeSQL(_YGRP_0, 29);           //varchar    29  Gruppo inserita nella  PRIMARY KEY
            C_USER_TIME01 = 0;                                              //int     
            C_USER_TIME02 = 0;                                              //int     


            //set_Attrezzatura_ASS_VieMinuto();
            //set_Attrezzatura_ASS_PzOra();
        }
        public override DataRow GetCyberRow()
        {
            DataRow _tablerow = _dataTable.NewRow();
            _tablerow[0] = C_ROUTING_CODE;
            _tablerow[1] = C_ROUTING_ALT;
            _tablerow[2] = C_OPNUM;
            _tablerow[3] = C_ALT_OPNUM;
            _tablerow[4] = C_DESCR;
            _tablerow[5] = C_QUEUE_TIME;
            _tablerow[6] = C_SETUP_TIME;
            _tablerow[7] = C_RUN_TIME;
            _tablerow[8] = C_WAIT_TIME;
            _tablerow[9] = C_DURATION;
            _tablerow[10] = C_LOT_SIZE;
            _tablerow[11] = C_SCRAP_TYPE;
            _tablerow[12] = C_SCRAP;
            _tablerow[13] = C_OP_GROUP;
            _tablerow[14] = C_SETUP_GROUP_CODE;
            _tablerow[15] = C_SETUP_TEAM_GROUP_QTY;
            _tablerow[16] = C_RUN_TEAM_GROUP_QTY;
            _tablerow[17] = C_WORKCENTER_CODE;
            _tablerow[18] = C_HOST_WC;
            _tablerow[19] = C_SUPPLIER_CODE;
            _tablerow[20] = C_HIERARCHICAL_POSITION;
            _tablerow[21] = C_USER_NOTE01;
            _tablerow[22] = C_USER_INT01;
            _tablerow[23] = C_USER_REAL01;
            _tablerow[24] = C_USER_STRING01;
            _tablerow[25] = C_USER_STRING02;
            _tablerow[26] = C_USER_TIME01;
            _tablerow[27] = C_USER_TIME02;


            return _tablerow;
        }

        public override string GetSelectQuery(bool mode, string dossier, string codice_like, string tipo)
        {
            //string libreria = "MBM41LIB_M";  //libreria = "MBM41LIBMT"  TRAC;  //libreria = "MBM41LIB_M";  
            //string __libreriaAs400 = libreria;  
            //
            //
            //string _tabrsh = __libreriaAs400 + ".RSHD00F";
            //
            //string sage_query = "SELECT " 
            //               + _tabrsh + ".CDSTM " 
            //        + ", " + _tabrsh + ".DESTM " 
            //        + " FROM " + _tabrsh
            //        + " ORDER BY " + _tabrsh + ".CDSTM "
            //        ;

            string db = "x3." + dossier;

            //recupero configurazioni senza Macchina (solo con gruppo macchine)
            string sage_query = @"SELECT
                      C.YCONATT_0
                     ,S.YSTADES_0
                     ,S.YSTAIMP_0
                     ,S.YSTASETUP_0
                     ,S.YSTACONFIG_0
                     ,C.YCONLOTSIZ_0
                     ,C.YCONGRP_0
                     ,C.YCONCADTIM_0
                     ,S.YATTLOTUM_0
                     ,'fornitore'
                     ,S.YATTWCR_0 
                     ,C.YCONCAD_0                     
                      from " + db + ".YPRDCONF C \n" +
                      " join " + db + ".YPRDATT S on S.YATTCOD_0 = C.YCONATT_0 \n" +                      
                      " where S.YATTENAFLG_0=2 " +
                      " and C.YCONENAFLG_0=2 " +
                      " and C.YCONCDL_0='' "
              ;

            //if (!string.IsNullOrWhiteSpace(codice_like))
            //{
            //    sage_query += " and B.BPRNUM_0 like '" + codice_like.Trim() + "'";
            //}
            return sage_query;
        }
        public override string GetID()
        {
            return C_ROUTING_CODE + C_ROUTING_ALT + C_OPNUM + C_ALT_OPNUM;
        }

        public override void InitDataTable()
        {
            _dataTable.Columns.Add("C_ROUTING_CODE", typeof(string));
            _dataTable.Columns.Add("C_ROUTING_ALT", typeof(string));
            _dataTable.Columns.Add("C_OPNUM", typeof(int));
            _dataTable.Columns.Add("C_ALT_OPNUM", typeof(string));
            _dataTable.Columns.Add("C_DESCR", typeof(string));
            _dataTable.Columns.Add("C_QUEUE_TIME", typeof(int));
            _dataTable.Columns.Add("C_SETUP_TIME", typeof(int));
            _dataTable.Columns.Add("C_RUN_TIME", typeof(int));
            _dataTable.Columns.Add("C_WAIT_TIME", typeof(int));
            _dataTable.Columns.Add("C_DURATION", typeof(int));
            _dataTable.Columns.Add("C_LOT_SIZE", typeof(decimal));
            _dataTable.Columns.Add("C_SCRAP_TYPE", typeof(char));
            _dataTable.Columns.Add("C_SCRAP", typeof(double));
            _dataTable.Columns.Add("C_OP_GROUP", typeof(string));
            _dataTable.Columns.Add("C_SETUP_GROUP_CODE", typeof(string));
            _dataTable.Columns.Add("C_SETUP_TEAM_GROUP_QTY", typeof(float));
            _dataTable.Columns.Add("C_RUN_TEAM_GROUP_QTY", typeof(float));
            _dataTable.Columns.Add("C_WORKCENTER_CODE", typeof(string));
            _dataTable.Columns.Add("C_HOST_WC", typeof(string));
            _dataTable.Columns.Add("C_SUPPLIER_CODE", typeof(string));
            _dataTable.Columns.Add("C_HIERARCHICAL_POSITION", typeof(char));
            _dataTable.Columns.Add("C_USER_NOTE01", typeof(string));
            _dataTable.Columns.Add("C_USER_INT01", typeof(int));
            _dataTable.Columns.Add("C_USER_REAL01", typeof(double));
            _dataTable.Columns.Add("C_USER_STRING01", typeof(string));
            _dataTable.Columns.Add("C_USER_STRING02", typeof(string));
            _dataTable.Columns.Add("C_USER_TIME01", typeof(int));
            _dataTable.Columns.Add("C_USER_TIME02", typeof(int));
        }
    }
}
