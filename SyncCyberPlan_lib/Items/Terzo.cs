using System.Collections.Generic;
using System;
using log4net;
using System.Data;

namespace SyncCyberPlan_lib
{
    public class Terzo: Item
    {
        public string BPRNUM_0; //codice terzo
        public string BPRNAM_0; //ragione sociale
        public byte BPCFLG_0;  //is customer if 2
        public byte BPSFLG_0;  //is supplier if 2
        public string BPAADDLIG_0; //righe indirizzo
        public string BPAADDLIG_1; //righe indirizzo
        public string BPAADDLIG_2; //righe indirizzo

        #region tabella output CYB_COMPANY
        public string C_CODE;
        public string C_DESCR;
        public string C_ADDRESS;
        public int C_CUSTOMER; //1=si
        public int C_SUPPLIER; //1=si

        public string C_USER_STRING01;
        public string C_USER_STRING02;
        public string C_USER_STRING03;
        public string C_USER_STRING04;
        public string C_USER_STRING05;
        #endregion


        public Terzo(): base("CYB_COMPANY")
        {

        }

        public override void Init(object[] row)
        {
            BPRNUM_0 = getDBV<string>(row[0]); //codice terzo
            BPRNAM_0 = getDBV<string>(row[1]); //ragione sociale
            BPCFLG_0 = getDBV<byte>(row[2]);  //is customer if 2
            BPSFLG_0 = getDBV<byte>(row[3]);  //is supplier if 2
            BPAADDLIG_0 = getDBV<string>(row[4]); //righe indirizzo
            BPAADDLIG_1 = getDBV<string>(row[5]); //righe indirizzo
            BPAADDLIG_2 = getDBV<string>(row[6]);



            C_CODE = EscapeSQL(BPRNUM_0, 30);
            C_DESCR = EscapeSQL(BPRNAM_0, 30);
            C_ADDRESS = ((BPAADDLIG_0 + " " + BPAADDLIG_1).Trim() + " " + BPAADDLIG_2).Trim();
            C_ADDRESS = EscapeSQL(C_ADDRESS, 35);
            C_CUSTOMER = (BPCFLG_0 == 2) ? 1 : 0; //1=si
            C_SUPPLIER = (BPSFLG_0 == 2) ? 1 : 0; ; //1=si

            C_USER_STRING01 = EscapeSQL("", 29);
            C_USER_STRING02 = EscapeSQL("", 29);
            C_USER_STRING03 = EscapeSQL("", 29);
            C_USER_STRING04 = EscapeSQL("", 29);
            C_USER_STRING05 = EscapeSQL("", 29);
        }
        public override DataRow GetCyberRow()
        {
            DataRow _tablerow = _dataTable.NewRow();

            _tablerow[0] = C_CODE;                         //C_CODE
            _tablerow[1] = C_DESCR;                        //C_DESCR            
            _tablerow[2] = C_ADDRESS;                      //C_ADDRESS
            _tablerow[3] = C_CUSTOMER;                     //C_CUSTOMER //1=si
            _tablerow[4] = C_SUPPLIER;                     //C_SUPPLIER //1=si
            _tablerow[5] = C_USER_STRING01;                //C_USER_STRING01
            _tablerow[6] = C_USER_STRING02;                //C_USER_STRING02
            _tablerow[7] = C_USER_STRING03;                //C_USER_STRING03
            _tablerow[8] = C_USER_STRING04;                //C_USER_STRING04
            _tablerow[9] = C_USER_STRING05;                //C_USER_STRING05

            return _tablerow;
        }
        public override string GetSelectQuery(bool mode, string dossier, string codice_like, string tipo)
        {
            string db = "x3." + dossier;
            //string sage_query = "SELECT A.ITMREF_0, ITMSTA_0 from " + dossier + ".[ITMMASTER] A join " + dossier + ".[YITMINF] B on A.ITMREF_0=B.ITMREF_0" +
            //    " WHERE YLIVTRAS_0='" + tipo + "' and ITMSTA=1 ";

            string sage_query =
@"SELECT 
 B.BPRNUM_0, 
 B.BPRNAM_0, 
 B.BPCFLG_0, 
 B.BPSFLG_0, 
 A.BPAADDLIG_0, 
 A.BPAADDLIG_1, 
 A.BPAADDLIG_2 
 from " + db + ".BPARTNER B " +
                " join " + db + ".BPADDRESS A on B.BPRNUM_0 = A.BPANUM_0 and (B.BPCFLG_0=2 or  B.BPSFLG_0=2)" +
                " WHERE B.ENAFLG_0=2 and A.BPAADD_0='SL0' ";

            if (!string.IsNullOrWhiteSpace(codice_like))
            {
                sage_query += " and B.BPRNUM_0 like '" + codice_like.Trim() + "'";
            }
            sage_query += " ORDER BY B.BPRNUM_0 ";
            return sage_query;
        }
        /*public override string To_CYBERDB()
        {
            string ret = @"
                INSERT INTO [CyberPlanFrontiera].[dbo].[CYB_COMPANY]
             ([C_CODE]
             ,[C_DESCR]
             ,[C_ADDRESS]
             ,[C_CUSTOMER]
             ,[C_SUPPLIER]
             ,[C_USER_STRING01]
             ,[C_USER_STRING02]
             ,[C_USER_STRING03]
             ,[C_USER_STRING04]
             ,[C_USER_STRING05])
             VALUES
             (" +       CYB_COMPANY_C_CODE
              + " , " + CYB_COMPANY_C_DESCR
              + " , " + CYB_COMPANY_C_ADDRESS
              + " , " + CYB_COMPANY_C_CUSTOMER
              + " , " + CYB_COMPANY_C_SUPPLIER
              + " , " + CYB_COMPANY_C_USER_STRING01
              + " , " + CYB_COMPANY_C_USER_STRING02
              + " , " + CYB_COMPANY_C_USER_STRING03
              + " , " + CYB_COMPANY_C_USER_STRING04
              + " , " + C_USER_STRING05
              + " ) ";
             
            return ret;
        }*/

        public override string GetID()
        {
            return C_CODE;
        }

        public override void InitDataTable()
        {
            _dataTable.Columns.Add("C_CODE",            typeof(string));   
            _dataTable.Columns.Add("C_DESCR",           typeof(string));   
            _dataTable.Columns.Add("C_ADDRESS",         typeof(string));   
            _dataTable.Columns.Add("C_CUSTOMER",        typeof(int));    //1=si
            _dataTable.Columns.Add("C_SUPPLIER",        typeof(int));    //1=si
            _dataTable.Columns.Add("C_USER_STRING01",   typeof(string));   
            _dataTable.Columns.Add("C_USER_STRING02",   typeof(string));   
            _dataTable.Columns.Add("C_USER_STRING03",   typeof(string));   
            _dataTable.Columns.Add("C_USER_STRING04",   typeof(string));   
            _dataTable.Columns.Add("C_USER_STRING05",   typeof(string));   
        }
    }
}
