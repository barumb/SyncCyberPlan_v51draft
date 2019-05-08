using System.Collections.Generic;
using System;
using log4net;
using System.Data;

namespace SyncCyberPlan_lib
{
    public class Macchina: Item
    {
        public string _WST_0;     //macchina (CDL in sage)
        public string _TEXTE_0;   //
        public string _WCR_0  ;   //reparto (CDC in Sage)
        public string _YGRP_0 ;   //gruppo macchine
        public string _YBPS_0 ;   //fornitore
        public string _YMRPCDL_0;   //Cdl per Mrp cyberplan

        public byte    _YPLASTH_0;   //flah STH        PLAS
        public byte    _YPLAPA66_0;  //flag PA66       PLAS
        public byte    _YPLADIV_0;   //flag divisore   PLAS
        public decimal _YPLAGRMIN_0; //grammmatura  PLAS
        public decimal _YPLAGRMAX_0; //grammmatura  PLAS
        public int     _YQTASETCLF ;      //numero pezzi /settimana per Conto lavoro a capacita finita
        public byte    _WSTTYP_0; //tipo macchina  1 macchina  2= manuale  
        public string  _YMACLIN_0;  //macchina successiva(in linea)
        public byte _YPIEFLG_0; //flag piegatrice (per filo)

        #region tabella output CYB_MACHINE
        public string C_CODE         ;             //varchar](30) NOT NULL,
        public string C_DESCR        ;             //varchar](50) NULL,
        public int    C_EFFICIENCY   ;             //int] NULL,
        public char   C_USER_CHAR01  ;             //char](1) NULL,
        public char   C_USER_CHAR02  ;             //char](1) NULL,
        public byte   C_USER_FLAG01  ;             //bit] NULL,
        public byte   C_USER_FLAG02  ;             //bit] NULL,
        public int    C_USER_INT01   ;             //int] NULL,
        public int    C_USER_INT02   ;             //int] NULL,
        public float  C_USER_REAL01  ;             //float] NULL,
        public float  C_USER_REAL02  ;             //float] NULL,
        public string C_USER_STRING01;             //varchar](29) NULL,
        public string C_USER_STRING02;             //varchar](29) NULL,
        public string C_USER_STRING03;             //varchar](29) NULL,
        public string C_USER_STRING04;             //varchar](29) NULL,
        public string C_USER_STRING05;             //varchar](29) NULL,
        public DateTime? C_USER_TIME01  ;          //datetime] NULL,
        public DateTime? C_USER_TIME02  ;          //datetime] NULL,
        public float C_UTILIZATION  ;              //float] NULL,
        public float C_WAIT_TIME    ;              //float] NULL,
        public string C_WORKCENTER   ;             //varchar](30) NULL,
        #endregion


        public Macchina(): base("CYB_MACHINE")
        {

        }

        public override void Init(object[] row)
        {
            _WST_0      = getDBV<string>(row[0]); //macchina (CDL in sage)
            _TEXTE_0    = getDBV<string>(row[1]); //
            _WCR_0      = getDBV<string>(row[2]); //reparto (CDC in Sage)
            _YGRP_0     = getDBV<string>(row[3]); //gruppo macchine
            _YBPS_0     = getDBV<string>(row[4]); //fornitore
            _YMRPCDL_0  = getDBV<string>(row[5]); //CDL per mrp

            _YPLASTH_0    = getDBV<byte>(row[6]);
            _YPLAPA66_0   = getDBV<byte>(row[7]);
            _YPLADIV_0    = getDBV<byte>(row[8]);
            _YPLAGRMIN_0  = getDBV<decimal>(row[9]);
            _YPLAGRMAX_0  = getDBV<decimal>(row[10]);
            _YQTASETCLF   = getDBV<int>(row[11]);
            _WSTTYP_0     = getDBV<byte>(row[12]);
            _YMACLIN_0    = getDBV<string>(row[13]);
            _YPIEFLG_0    = getDBV<byte>(row[14]);


            C_CODE = _WST_0;           //varchar](30) NOT NULL,
            C_DESCR = _TEXTE_0;         //varchar](50) NULL,
            C_EFFICIENCY = 0;             //int] NULL,
            C_USER_CHAR01 = _YPIEFLG_0 == 2 ? 'P' : ' ';         //char](1) NULL,
            C_USER_CHAR02 = ' ';         //char](1) NULL,
            C_USER_FLAG01 = (byte)(_YPLADIV_0 == 2 ? 1 : 0);  //bit] NULL,
            C_USER_FLAG02 = getFlagManuale(_YMRPCDL_0, _WSTTYP_0, _WST_0);    //bit] NULL,   se manuale (WSTTYP=2) metto true
            C_USER_INT01 = _YQTASETCLF;             //int] NULL,
            C_USER_INT02 = 0;          //int] NULL,
            C_USER_REAL01 = (float)_YPLAGRMIN_0;            //float] NULL,
            C_USER_REAL02 = (float)_YPLAGRMAX_0;            //float] NULL,
            C_USER_STRING01 = _WCR_0;   //varchar](29) NULL,
            C_USER_STRING02 = _YGRP_0;  //varchar](29) NULL,
            C_USER_STRING03 = _YBPS_0;   //varchar](29) NULL,
            C_USER_STRING04 = Attrezzature.GetTipoPLastica(_WCR_0, _YPLASTH_0, _YPLAPA66_0);       //varchar](29) NULL,
            C_USER_STRING05 = _YMACLIN_0;             //varchar](29) NULL,  //macchina successiva
            C_USER_TIME01 = null;         //datetime] NULL,
            C_USER_TIME02 = null;         //datetime] NULL,
            C_UTILIZATION = 0;           //float] NULL,
            C_WAIT_TIME = 0;              //float] NULL,
            C_WORKCENTER = getMrpCDL(_YMRPCDL_0, _WSTTYP_0, _WST_0);   //varchar](30) NULL,
        }
        public override DataRow GetCyberRow()
        {
            DataRow _tablerow = _dataTable.NewRow();

            _tablerow[0] = C_CODE;
            _tablerow[1] = C_DESCR;
            _tablerow[2] = C_EFFICIENCY;  
            _tablerow[3] = C_USER_CHAR01;
            _tablerow[4] = C_USER_CHAR02;
            _tablerow[5] = C_USER_FLAG01;
            _tablerow[6] = C_USER_FLAG02; 
            _tablerow[7] = C_USER_INT01;  
            _tablerow[8] = C_USER_INT02;  
            _tablerow[9] = C_USER_REAL01; 
            _tablerow[10] = C_USER_REAL02; 
            _tablerow[11] = C_USER_STRING01;
            _tablerow[12] = C_USER_STRING02;
            _tablerow[13] = C_USER_STRING03;
            _tablerow[14] = C_USER_STRING04;
            _tablerow[15] = C_USER_STRING05;
            _tablerow[16] = DateTime_toCyb(C_USER_TIME01); 
            _tablerow[17] = DateTime_toCyb(C_USER_TIME02);
            _tablerow[18] = C_UTILIZATION;
            _tablerow[19] = C_WAIT_TIME;  
            _tablerow[20] = C_WORKCENTER; 

            return _tablerow;
        }
        public override string GetSelectQuery(bool mode, string dossier, string codice_like, string tipo)
        {
            string db = "x3." + dossier;
            //string sage_query = "SELECT A.ITMREF_0, ITMSTA_0 from " + dossier + ".[ITMMASTER] A join " + dossier + ".[YITMINF] B on A.ITMREF_0=B.ITMREF_0" +
            //    " WHERE YLIVTRAS_0='" + tipo + "' and ITMSTA=1 ";

            string sage_query = @"select 
                  W.WST_0 
                , A.TEXTE_0 
                , W.WCR_0 
                , W.YGRP_0 
                , W.YBPS_0
                , W.YMRPCDL_0

                , W.YPLASTH_0
                , W.YPLAPA66_0
                , W.YPLADIV_0
                , W.YPLAGRMIN_0
                , W.YPLAGRMAX_0
                , W.YQTASETCLF_0 
                , W.WSTTYP_0
                , W.YMACLIN_0
                , W.YPIEFLG_0
                from x3.SAURO.WORKSTATIO W 
                join x3.SAURO.ATEXTRA A on A.CODFIC_0 = 'WORKSTATIO' 
                and A.ZONE_0 ='WSTDESAXX' and A.LANGUE_0='ITA' and A.IDENT1_0 = W.WST_0
                where W.YENAFLG_0=2
                ";

            if (!string.IsNullOrWhiteSpace(codice_like))
            {
                sage_query += " and W.WST_0 like '" + codice_like.Trim() + "'";
            }
            sage_query += " ORDER BY W.WST_0 ";
            return sage_query;
        }

        protected string getMrpCDL(string mrpcdl, byte WSTTYP, string macchina)
        {
            //se il CDl_mrp è "ASSI" e la macchina è manuale ( WSTTYP=2)
            //come CDL va passata la macchina stessa
            //e non va passato il flag "manuale"
            if ((mrpcdl == "ASSI" || mrpcdl == "ASSE") && WSTTYP == 2)
            {
                return macchina;
            }
            else if (mrpcdl == "CLF")
            {
                return "CLF_"+macchina;
            }
            else
            {
                return mrpcdl;
            }
        }
        protected byte getFlagManuale(string mrpcdl, byte WSTTYP, string macchina)
        {
            //se il CDl_mrp è "ASSI" e la macchina è manuale ( WSTTYP=2)
            //come CDL va passata la macchina stessa
            //e non va passato il flag "manuale"
            if (mrpcdl == "ASSI" && WSTTYP == 2)
            {
                return 0;
            }
            else
            {
                return (byte)(_WSTTYP_0 == 2 ? 1 : 0);
            }
        }

        public override string GetID()
        {
            return C_CODE;
        }

        public override void InitDataTable()
        {
            _dataTable.Columns.Add("C_CODE", typeof(string));
            _dataTable.Columns.Add("C_DESCR", typeof(string));
            _dataTable.Columns.Add("C_EFFICIENCY", typeof(int));
            _dataTable.Columns.Add("C_USER_CHAR01", typeof(char));
            _dataTable.Columns.Add("C_USER_CHAR02", typeof(char));
            _dataTable.Columns.Add("C_USER_FLAG01", typeof(byte));
            _dataTable.Columns.Add("C_USER_FLAG02", typeof(byte));
            _dataTable.Columns.Add("C_USER_INT01", typeof(int));
            _dataTable.Columns.Add("C_USER_INT02", typeof(int));
            _dataTable.Columns.Add("C_USER_REAL01", typeof(float));
            _dataTable.Columns.Add("C_USER_REAL02", typeof(float));
            _dataTable.Columns.Add("C_USER_STRING01", typeof(string));
            _dataTable.Columns.Add("C_USER_STRING02", typeof(string));
            _dataTable.Columns.Add("C_USER_STRING03", typeof(string));
            _dataTable.Columns.Add("C_USER_STRING04", typeof(string));
            _dataTable.Columns.Add("C_USER_STRING05", typeof(string));
            _dataTable.Columns.Add("C_USER_TIME01", typeof(DateTime));
            _dataTable.Columns.Add("C_USER_TIME02", typeof(DateTime));
            _dataTable.Columns.Add("C_UTILIZATION", typeof(float));
            _dataTable.Columns.Add("C_WAIT_TIME", typeof(float));
            _dataTable.Columns.Add("C_WORKCENTER", typeof(string));
        }
    }
}
