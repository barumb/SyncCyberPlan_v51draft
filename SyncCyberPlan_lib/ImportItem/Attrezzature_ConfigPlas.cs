using System.Collections.Generic;
using System;
using log4net;
using System.Data;

namespace SyncCyberPlan_lib
{
    public class Attrezzature_ConfigPlas: Item
    { 
        public string YATTCOD_0;                   //nvarchar](10) NOT NULL,
        public string YATPVER_0;                     //nvarchar](4) NOT NULL,
        public short YINDICE_0;                     //smallint] NOT NULL,
        public string YATPIMP_0;                     //nvarchar](1) NOT NULL,
        public decimal YATPLENMIN_0;                  //numeric](16, 5) NOT NULL,
        public decimal YATPLENMAX_0;                  //numeric](16, 5) NOT NULL,
        public short YATPVIEMIN_0;                  //smallint] NOT NULL,
        public short YATPVIEMAX_0;                  //smallint] NOT NULL,
        public byte YATPSTA_0;                  //tinyint] NOT NULL,
        public DateTime? YATPDATRIA_0;           //datetime] NOT NULL,


        #region tabella output [YPRDATTP]

        #endregion


        public Attrezzature_ConfigPlas(): base("YPRDATTP")
        {

        }

        public override void Init(object[] row)
        {
            YATTCOD_0        = getDBV<string>(row[0], "YATTCOD_0");
            YATPVER_0        = getDBV<string>(row[1], "YATPVER_0");
            YINDICE_0        = getDBV<short>(row[2], "YINDICE_0");
            YATPIMP_0        = getDBV<string>(row[3], "YATPIMP_0");
            YATPLENMIN_0     = getDBV<decimal>(row[4], "YATPLENMIN_0");
            YATPLENMAX_0     = getDBV<decimal>(row[5], "YATPLENMAX_0");
            YATPVIEMIN_0     = getDBV<short>(row[6], "YATPVIEMIN_0");
            YATPVIEMAX_0     = getDBV<short>(row[7], "YATPVIEMAX_0");
            YATPSTA_0        = getDBV<byte>(row[8], "YATPSTA_0");
            YATPDATRIA_0     = getSageDate(row[9], "YATPDATRIA_0");
        }
        public override DataRow GetCyberRow()
        {
            DataRow _tablerow = _dataTable.NewRow();

            _tablerow[0] = YATTCOD_0      ;
            _tablerow[1] = YATPVER_0      ;
            _tablerow[2] = YINDICE_0      ;
            _tablerow[3] = YATPIMP_0      ;
            _tablerow[4] = YATPLENMIN_0   ;
            _tablerow[5] = YATPLENMAX_0   ;
            _tablerow[6] = YATPVIEMIN_0   ;
            _tablerow[7] = YATPVIEMAX_0   ;
            _tablerow[8] = YATPSTA_0      ;
            _tablerow[9] = DateTime_toCyb(YATPDATRIA_0); 

            return _tablerow;
        }
        public override string GetSelectQuery(bool mode, string dossier, string codice_like, string tipo)
        {
            string db = "x3." + dossier;
            //string sage_query = "SELECT A.ITMREF_0, ITMSTA_0 from " + dossier + ".[ITMMASTER] A join " + dossier + ".[YITMINF] B on A.ITMREF_0=B.ITMREF_0" +
            //    " WHERE YLIVTRAS_0='" + tipo + "' and ITMSTA=1 ";

            string sage_query =
@"SELECT [YATTCOD_0]
      ,[YATPVER_0]
      ,[YINDICE_0]
      ,[YATPIMP_0]
      ,[YATPLENMIN_0]
      ,[YATPLENMAX_0]
      ,[YATPVIEMIN_0]
      ,[YATPVIEMAX_0]
      ,[YATPSTA_0]
      ,[YATPDATRIA_0]
  FROM " + db+ ".[YPRDATTP] "
  + " where YATPSTA_0<= 2 ";

            if (!string.IsNullOrWhiteSpace(codice_like))
            {
                sage_query += " and YATTCOD_0 like '" + codice_like.Trim() + "'";
            }
            sage_query += " ORDER BY YATTCOD_0 ";
            return sage_query;
        }
      
        public override string GetID()
        {
            return YATTCOD_0.PadRight(20) + YATPVER_0.PadRight(5) + YATPIMP_0.ToString().PadRight(5);
        }

        public override void InitDataTable()
        {         
           _dataTable.Columns.Add(" YATTCOD_0",     typeof(string));
           _dataTable.Columns.Add(" YATPVER_0",     typeof(string));
           _dataTable.Columns.Add(" YINDICE_0",     typeof(short ));
           _dataTable.Columns.Add(" YATPIMP_0",     typeof(string));
           _dataTable.Columns.Add(" YATPLENMIN_0",  typeof(decimal));
           _dataTable.Columns.Add(" YATPLENMAX_0",  typeof(decimal));
           _dataTable.Columns.Add(" YATPVIEMIN_0",  typeof(short));
           _dataTable.Columns.Add(" YATPVIEMAX_0",  typeof(short));
           _dataTable.Columns.Add(" YATPSTA_0",     typeof(byte));
           _dataTable.Columns.Add(" YATPDATRIA_0",  typeof(DateTime));
        }
    }
}
