using System.Collections.Generic;
using System;
using log4net;
using System.Data;

namespace SyncCyberPlan_lib
{
    public class Demand_OPR_RIGHE : Item
    {
        public string  _MFCTORD;
        public decimal _MFCAORD;
        public decimal _MFCPORD;
        public string  _MFCCART;
        public string  _MFCCOMP;
        public decimal _MFCQTRC;
        public string  _MFCSTAT;
        
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

        public Demand_OPR_RIGHE(): base("CYB_DEMAND")
        {
        }

        public override string GetSelectQuery(bool mode, string dossier, string codice_like, string filtro)
        {
            string __libreriaAs400 = dossier;

            string _tabMFC = __libreriaAs400 + ".MFC00PF";

            string query = "SELECT " + "\n"
                + "   " + _tabMFC + ".MFCTORD" + "\n"
                + ",  " + _tabMFC + ".MFCAORD" + "\n"
                + ",  " + _tabMFC + ".MFCPORD" + "\n"
                + ",  " + _tabMFC + ".MFCCART" + "\n"
                + ",  " + _tabMFC + ".MFCCOMP" + "\n"
                + ",  " + _tabMFC + ".MFCQTRC" + "\n"
                + ",  " + _tabMFC + ".MFCSTAT" + "\n"
                + " FROM " + _tabMFC + "\n"
                + " WHERE " + _tabMFC + ".MFCSTAT = 'RI' " + "\n"
                ;

            if (!string.IsNullOrWhiteSpace(codice_like))
            {
                query += " and " + _tabMFC + ".MFCCART like '" + codice_like.Trim() + "'";
            }

            query += " ORDER BY " 
                + "  " + _tabMFC + ".MFCTORD," + "\n"
                + "  " + _tabMFC + ".MFCAORD," + "\n"
                + "  " + _tabMFC + ".MFCPORD" + "\n"
                ;
            return query;
        }

        public override void Init(object[] row)
        {
            _MFCTORD = getDBV<string>(row[0]);
            _MFCAORD = getDBV<decimal>(row[1]);
            _MFCPORD = getDBV<decimal>(row[2]);
            _MFCCART = getDBV<string>(row[3]);
            _MFCCOMP = getDBV<string>(row[4]);
            _MFCQTRC = getDBV<decimal>(row[5]);
            _MFCSTAT = getDBV<string>(row[6]);



            C_CORDER_CODE = EscapeSQL("", 30); 
            C_ORDER_CODE = EscapeSQL(_MFCTORD + _MFCAORD.ToString("00") + _MFCPORD.ToString("000000"),30);                   //string
            C_ITEM_CODE = EscapeSQL(_MFCCOMP, 50);                     //string 
            C_ITEM_PLANT = "ITS01";                   //string 
            C_OPNUM = 0;                         //int 
            C_NSEQ = 0;                         //int 
            C_QTY = _MFCQTRC;                     //decimal 
            C_WDW_QTY = 0;                       //decimal 
            C_M_B = ' ';                         //char 
            C_MRP_TYPE = ' ';                    //char 
            C_STATUS = 0;                        //int 
            C_REF_CORDER_CODE = "";              //string 
            C_DUEDATE = null;                    //DateTime 
            C_WAREHOUSE_CODE = __MAGAZZINO_INTERNO;       //string 
            C_USER_NOTE01 = EscapeSQL("Articolo da produrre: " +_MFCCART, 99);                  //string 
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
            return C_ORDER_CODE + C_ITEM_CODE + C_ITEM_PLANT + C_OPNUM + C_NSEQ;
    }
    }
}
