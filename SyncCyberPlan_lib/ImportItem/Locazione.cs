using System.Collections.Generic;
using System;
using log4net;
using System.Data;

namespace SyncCyberPlan_lib
{
    public class Locazione: Item
    {
        public string _TABCTAB; // 'LOCZ'
        public string _TABSTAB; //nome locazione
        public string _TABDESC;
        public string _TABDESI;
        public string _TABDAGG;
        public string   _TABFLG1; // 'I' o 'E' (interno esterno) o 'S' (scarto)
        public string   _TABFLG2; // ' ' 'T' o '- '
        public string   _TABFLG3; // 'Y' 'N' o ' '
        public string   _TABFLG4; // '1' o '2' o ' '
                
        public string C_CODE;
        public string C_PLANTCODE; 
        public string C_DESCR;
        
        public Locazione( ): base("CYB_WAREHOUSE")
        {

        }

        public override void Init(object[] row)
        {
            _TABCTAB = getDBV<string>(row[0], "TABCTAB");
            _TABSTAB = getDBV<string>(row[1],"TABSTAB");
            _TABDESC = getDBV<string>(row[2],"TABDESC");
            //_TABDESI = getDBV<string>(row[3]);
            //_TABDAGG = getDBV<string>(row[4]);
            //_TABFLG1 = getDBV<string>(row[5]);
            //_TABFLG2 = getDBV<string>(row[6]);
            //_TABFLG3 = getDBV<string>(row[7]);
            //_TABFLG4 = getDBV<string>(row[8]);


            C_CODE = EscapeSQL(_TABSTAB, 20);
            C_PLANTCODE = EscapeSQL("ITS01", 20);
            C_DESCR = EscapeSQL(_TABDESC, 30);
        }
        public override DataRow GetCyberRow()
        {
            DataRow _tablerow = _dataTable.NewRow();

            _tablerow[0] = C_CODE     ;   //C_BOM_CODE varchar     20
            _tablerow[1] = C_PLANTCODE;   //C_BOM_CODE varchar     20
            _tablerow[2] = C_DESCR    ;   //C_BOM_CODE varchar     30

            return _tablerow;
        }
        public override string GetSelectQuery(bool mode, string libreria, string codice_like, string tipo)
        {
            //string libreria = "MBM41LIB_M";  //libreria = "MBM41LIBMT"  TRAC;  //libreria = "MBM41LIB_M";  
            string __libreriaAs400 = libreria;            
            string _tabPQM = __libreriaAs400 + ".PQM00PF";
            string _tabTAB = __libreriaAs400 + ".TAB00PF";

            string query = "SELECT " 
                           + _tabTAB + ".TABCTAB " 
                    + ", " + _tabTAB + ".TABSTAB " 
                    + ", " + _tabTAB + ".TABDESC "
                    + ", " + _tabTAB + ".TABDESI "
                    + ", " + _tabTAB + ".TABDAGG "
                    + ", " + _tabTAB + ".TABFLG1 "
                    + ", " + _tabTAB + ".TABFLG2 "
                    + ", " + _tabTAB + ".TABFLG3 "
                    + ", " + _tabTAB + ".TABFLG4 "
                    + " FROM " + _tabTAB
                    + " WHERE " + _tabTAB + ".TABCTAB ='LOCZ' "
                    + " ORDER BY " + _tabTAB + ".TABSTAB "
                    ;

            query = @" SELECT 'TABTAB' AS TABCTAB, '"+ __MAGAZZINO_INTERNO + @"' AS CODE, 'Magazzini interni' AS DESCR from " + _tabTAB 
                +  " UNION "
                + @" SELECT 'TABTAB' AS TABCTAB, '"+ __MAGAZZINO_ESTERNO + "' AS CODE, 'Magazzini esterni' AS DESCR from " + _tabTAB;


            query = @" SELECT 'TABTAB' AS TABCTAB, '" + __MAGAZZINO_INTERNO + @"' AS CODE, 'Magazzini interni' AS DESCR from " + _tabTAB

                + " UNION "

                + " SELECT DISTINCT " 
                + @" 'TABTAB' AS TABCTAB "
                + ", " + _tabPQM + ".PQMLOCZ AS CODE"
                + @", 'Magazzino esterno' AS DESCR " 
                + " FROM " + _tabPQM + "\n"
                + " WHERE " + _tabPQM + ".PQMTIFI<>'S' and " + _tabPQM + ".PQMTILO='E' \n"
                + " and " + _tabPQM + ".PQMCART not like 'WU%'    \n"
                + " and " + _tabPQM + ".PQMCART not like 'DAI%'   \n"
                + " and " + _tabPQM + ".PQMCART not like 'DPI%'   \n"
            ;

            //if (!string.IsNullOrWhiteSpace(codice_like))
            //{
            //    sage_query += " and B.BPRNUM_0 like '" + codice_like.Trim() + "'";
            //}
            return query;
        }
        public override string GetID()
        {
            return C_CODE;
        }

        public override void InitDataTable()
        {
            _dataTable.Columns.Add("C_CODE"          , typeof(string));
            _dataTable.Columns.Add("C_PLANTCODE"     , typeof(string));
            _dataTable.Columns.Add("C_DESCR"         , typeof(string));
        }
    }
}
