using System.Collections.Generic;
using System;
using log4net;
using System.Data;

namespace SyncCyberPlan_lib
{
    public class Demand_OPR_righe : Item
    {
        public string MFGNUM_0;
        public string MFGFCY_0;
        //public string YCYBCOD_0;
        //public int YCYBTSK_0;
        public string ITMREF_0;
        //public string TCLCOD_0;
        public int MFGLIN_0;
        public decimal RETQTY_0;
        public string ITMREF_0_componente;
        //public short BOMSEQ_0;
        //public decimal BOMQTY_0;


        #region tabella output [CYB_DEMAND]
        public string   C_CORDER_CODE;                  //string
        public string   C_ORDER_CODE;                   //string
        public string   C_ITEM_CODE;                    //string 
        public string   C_ITEM_PLANT;                   //string 
        public int      C_OPNUM;                        //int 
        public int      C_NSEQ;                         //int 
        public decimal  C_QTY;                          //decimal 
        public decimal  C_WDW_QTY;                      //decimal 
        public char     C_M_B;                          //char 
        public char     C_MRP_TYPE;                     //char 
        public int      C_STATUS;                       //int 
        public string   C_REF_CORDER_CODE;              //string 
        public DateTime? C_DUEDATE;                      //DateTime 
        public string   C_WAREHOUSE_CODE;               //string 
        public string   C_USER_NOTE01;                  //string 
        public int      C_USER_INT01;                   //int 
        public int      C_USER_INT02;                   //int 
        public float    C_USER_REAL01;                  //float 
        public float    C_USER_REAL02;                  //float 
        public char     C_USER_CHAR01;                  //char 
        public char     C_USER_CHAR02;                  //char 
        public byte     C_USER_FLAG01;                  //bit 
        public byte     C_USER_FLAG02;                  //bit 
        public DateTime? C_USER_DATE01;                  //DateTime 
        public DateTime? C_USER_DATE02;                  //DateTime 
        public int      C_USER_COLOR01;                 //int 
        #endregion

        public Demand_OPR_righe(): base("CYB_DEMAND")
        {
        }

        public override string GetSelectQuery(bool mode, string dossier, string codice_like, string filtro)
        {
            /* per test
             SELECT  [MBM41LIB_M_MFC00PF righe OPR].MFCTORD
                 ,[MBM41LIB_M_MFC00PF righe OPR].MFCAORD 
                 ,[MBM41LIB_M_MFC00PF righe OPR].MFCPORD 
                 ,[MBM41LIB_M_MFC00PF righe OPR].MFCCART 
                 ,[MBM41LIB_M_MFC00PF righe OPR].MFCCOMP 
                 ,[MBM41LIB_M_MFC00PF righe OPR].MFCQTRC 
                 ,[MBM41LIB_M_MFC00PF righe OPR].MFCSTAT 
                 ,[MBM41LIB_M_MFV00PF OPR data].MFVWKCT 
                FROM [MBM41LIB_M_MFC00PF righe OPR] 
                
                INNER JOIN [MBM41LIB_M_MFV00PF OPR data] on 
                     [MBM41LIB_M_MFV00PF OPR data].MFVTORD = [MBM41LIB_M_MFC00PF righe OPR].MFCTORD 
                and  [MBM41LIB_M_MFV00PF OPR data].MFVAORD = [MBM41LIB_M_MFC00PF righe OPR].MFCAORD 
                and  [MBM41LIB_M_MFV00PF OPR data].MFVPORD = [MBM41LIB_M_MFC00PF righe OPR].MFCPORD 
                WHERE [MBM41LIB_M_MFC00PF righe OPR].MFCSTAT = 'RI' 
                and  [MBM41LIB_M_MFV00PF OPR data].MFVSTAT = 'RI'  
             * */
            string _db = "x3." + dossier;


            string query = @"select 
  H.MFGNUM_0
, H.MFGFCY_0
, H.YCYBCOD_0
, H.YCYBTSK_0
, I.ITMREF_0
, I.TCLCOD_0
, I.MFGLIN_0
, M.RETQTY_0
, M.ITMREF_0
, M.BOMSEQ_0
, M.BOMQTY_0

from " + _db + @".MFGHEAD H
join " + _db + @".MFGITM I on H.MFGNUM_0 = I.MFGNUM_0
join " + _db + @".MFGMAT M on H.MFGNUM_0 = M.MFGNUM_0 and I.MFGLIN_0 = M.MFGLIN_0
where
M.MFGSTA_0 = 1 and 
M.MFGSTA_0 = 1 and 
I.MFGSTA_0 = 1 "
//and M.MFGFCY_0 = 'ITS01' "
;
//--MFGSTA 1 = Confermato  2 = Pianificato  3 = Suggerito 4 = Chiuso"


            if (!string.IsNullOrWhiteSpace(codice_like))
            {
                //, I.ITMREF_0 composto
                //, M.ITMREF_0 componente
                //query += " and " + _tabMFC + ".MFCCART like '" + codice_like.Trim() + "'";
            }

            query += " order by H.MFGNUM_0, I.ITMREF_0, M.BOMSEQ_0 ";
            return query;
        }

        public override void Init(object[] row)
        {
            MFGNUM_0  = getDBV<string>(row[0], "MFGNUM_0");
            MFGFCY_0  = getDBV<string>(row[1], "MFGFCY_0");
            //YCYBCOD_0 = getDBV<string>(row[2], "YCYBCOD_0");
            //YCYBTSK_0 = getDBV<int>(row[3], "YCYBTSK_0");
            ITMREF_0  = getDBV<string>(row[4], "ITMREF_0");
            //TCLCOD_0  = getDBV<string>(row[5], "TCLCOD_0");
            MFGLIN_0  = (int)row[6];
            RETQTY_0 = (decimal)row[7];

            ITMREF_0_componente = getDBV<string>(row[8], "ITMREF_0_componente");
            //BOMSEQ_0 = (short)row[9];
            //BOMQTY_0 = (decimal)row[10];



            C_CORDER_CODE = EscapeSQL("", 30);
            C_ORDER_CODE = EscapeSQL(MFGNUM_0 + MFGLIN_0.ToString("000000"),30);                   //string
            C_ITEM_CODE = EscapeSQL(ITMREF_0_componente, 50);                     //string 
            C_ITEM_PLANT = MFGFCY_0;                   //string 
            C_OPNUM = 0;                         //int 
            C_NSEQ = 0;                         //int 
            C_QTY = RETQTY_0;                     //decimal 
            C_WDW_QTY = 0;                       //decimal 
            C_M_B = ' ';                         //char 
            C_MRP_TYPE = ' ';                    //char 
            C_STATUS = 0;                        //int 
            C_REF_CORDER_CODE = "";              //string 
            C_DUEDATE = null;                    //DateTime 
            C_WAREHOUSE_CODE = __MAGAZZINO_INTERNO;       //string 
            C_USER_NOTE01 = EscapeSQL("Articolo da produrre: " + ITMREF_0, 99);                  //string 
            C_USER_INT01 = 0;                    //int 
            C_USER_INT02 = 0;                    //int 
            C_USER_REAL01 = 0;                   //float 
            C_USER_REAL02 = 0;                   //float 
            C_USER_CHAR01 = ' ';                  //char 
            C_USER_CHAR02 = ' ';                  //char 
            C_USER_FLAG01 = 0;                  //bit 
            C_USER_FLAG02 = 0;                  //bit 
            C_USER_DATE01 = null;                  //DateTime 
            C_USER_DATE02 = null;                  //DateTime 
            C_USER_COLOR01 = 0;                 //int 
        }
        public override DataRow GetCyberRow()
        {
            DataRow _tablerow = _dataTable.NewRow();

            _tablerow[0] = C_CORDER_CODE;                  //string
            _tablerow[1] = C_ORDER_CODE;                   //string
            _tablerow[2] = C_ITEM_CODE;                    //string 
            _tablerow[3] = C_ITEM_PLANT;                   //string 
            _tablerow[4] = C_OPNUM;                        //int 
            _tablerow[5] = C_NSEQ;                         //int 
            _tablerow[6] = C_QTY;                          //decimal 
            _tablerow[7] = C_WDW_QTY;                      //decimal 
            _tablerow[8] = C_M_B;                          //char 
            _tablerow[9] = C_MRP_TYPE;                     //char 
            _tablerow[10] = C_STATUS;                       //int 
            _tablerow[11] = C_REF_CORDER_CODE;              //string 
            _tablerow[12] = DateTime_toCyb(C_DUEDATE);                      //DateTime             
            _tablerow[13] = C_WAREHOUSE_CODE;               //string 
            _tablerow[14] = C_USER_NOTE01;                  //string 
            _tablerow[15] = C_USER_INT01;                   //int 
            _tablerow[16] = C_USER_INT02;                   //int 
            _tablerow[17] = C_USER_REAL01;                  //float 
            _tablerow[18] = C_USER_REAL02;                  //float 
            _tablerow[19] = C_USER_CHAR01;                  //char 
            _tablerow[20] = C_USER_CHAR02;                  //char 
            _tablerow[21] = C_USER_FLAG01;                  //bit 
            _tablerow[22] = C_USER_FLAG02;                  //bit 
            _tablerow[23] = DateTime_toCyb(C_USER_DATE01);                  //DateTime 
            _tablerow[24] = DateTime_toCyb(C_USER_DATE02);                  //DateTime 
            _tablerow[25] = C_USER_COLOR01;                 //int            

            return _tablerow;
        }

        public override void InitDataTable()
        {
                    _dataTable.Columns.Add("C_CORDER_CODE",               typeof(string));
                    _dataTable.Columns.Add("C_ORDER_CODE",               typeof(string));
                    _dataTable.Columns.Add("C_ITEM_CODE",               typeof(string) );
                    _dataTable.Columns.Add("C_ITEM_PLANT",               typeof(string) );
                    _dataTable.Columns.Add("C_OPNUM",               typeof(int ));
                    _dataTable.Columns.Add("C_NSEQ",               typeof(int ));
                    _dataTable.Columns.Add("C_QTY",               typeof(decimal) );
                    _dataTable.Columns.Add("C_WDW_QTY",               typeof(decimal) );
                    _dataTable.Columns.Add("C_M_B",               typeof(char) );
                    _dataTable.Columns.Add("C_MRP_TYPE",               typeof(char) );
                    _dataTable.Columns.Add("C_STATUS",               typeof(int ));
                    _dataTable.Columns.Add("C_REF_CORDER_CODE",               typeof(string) );
                    _dataTable.Columns.Add("C_DUEDATE",               typeof(DateTime ));
                    _dataTable.Columns.Add("C_WAREHOUSE_CODE",               typeof(string) );
                    _dataTable.Columns.Add("C_USER_NOTE01",               typeof(string) );
                    _dataTable.Columns.Add("C_USER_INT01",               typeof(int ));
                    _dataTable.Columns.Add("C_USER_INT02",               typeof(int ));
                    _dataTable.Columns.Add("C_USER_REAL01",               typeof(float ));
                    _dataTable.Columns.Add("C_USER_REAL02",               typeof(float ));
                    _dataTable.Columns.Add("C_USER_CHAR01",               typeof(char) );
                    _dataTable.Columns.Add("C_USER_CHAR02",               typeof(char) );
                    _dataTable.Columns.Add("C_USER_FLAG01",               typeof(byte ));
                    _dataTable.Columns.Add("C_USER_FLAG02",               typeof(byte));
                    _dataTable.Columns.Add("C_USER_DATE01",               typeof(DateTime ));
                    _dataTable.Columns.Add("C_USER_DATE02",               typeof(DateTime ));
                    _dataTable.Columns.Add("C_USER_COLOR01",               typeof(int ));

        }

        public override string GetID()
        {
            return C_ORDER_CODE + "_" + C_ITEM_CODE + "_" + C_ITEM_PLANT + "_" + C_OPNUM + "_" + C_NSEQ;
    }
    }
}
