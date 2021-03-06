﻿using System.Collections.Generic;
using System;
using log4net;
using System.Data;

namespace SyncCyberPlan_lib
{
    public class Attrezzature : Articolo
    {
        static public string __TOOL = "__TOOL__";

        public string _YATTCOD_0; 
        public string _YATTDES_0;        
        public short    _QTY;  //numero di attrezzature (stesso codice) presenti
        public string _YATTVIN_0;

        public decimal _YATTMAT_0   ;        //peso materozzza (PLAS)
        public byte _YATTFLGPA66_0;       //flag pa66 (PLAS)
        public byte _YATTFLGSTH_0;        //flag STH (PLAS)
        public byte _YATTMARSAU_0;        //Flag Marchio (PLAS)
        public byte _YATTMARTYC_0;        //Flag Marchio (PLAS)
        public byte _YATTMARHAR_0;        //Flag Marchio (PLAS)
        public short _YATTQUA_0;         //livello qualità (PLAS numero stampo)
        public string _YATTTYP;        //tipo attrezzatura
        public DateTime? _YATTDATSAU_0;  //data riattivazione MArchio Sauro
        public DateTime? _YATTDATTYC_0;  //data riattivazione MArchio Tyco
        public DateTime? _YATTDATHAR_0;  //data riattivazione MArchio Harting
        public char _YATTLOTUM_0;  //flag vie/pezzi
        public short _YATTLEATIM_0; //lead time, usato per contolavoro
        

        public Attrezzature( )//: base("CYB_TOOL")
        {

        }

        public override void Init(object[] row)
        {
            _YATTCOD_0    = getDBV<string>(row[0],"YATTCOD_0");
            _YATTDES_0    = getDBV<string>(row[1],"YATTDES_0");
            _QTY          = getDBV<short>( row[2],"QTY      ");
            _YATTVIN_0    = getDBV<string>(row[3],"YATTVIN_0");

            _YATTMAT_0     = getDBV<decimal>(row[4] , "YATTMAT_0    ");       //peso materozzza (PLAS)
            _YATTFLGPA66_0 = getDBV<byte>(   row[5] , "YATTFLGPA66_0");       //flag pa66 (PLAS)
            _YATTFLGSTH_0  = getDBV<byte>(   row[6] , "YATTFLGSTH_0 ");       //flag STH (PLAS)
            _YATTMARSAU_0  = getDBV<byte>(   row[7] , "YATTMARSAU_0 ");       //Flag Marchio (PLAS)
            _YATTMARTYC_0  = getDBV<byte>(   row[8] , "YATTMARTYC_0 ");       //Flag Marchio (PLAS)
            _YATTMARHAR_0  = getDBV<byte>(   row[9] , "YATTMARHAR_0 ");       //Flag Marchio (PLAS)
            _YATTQUA_0     = getDBV<short>(  row[10], "YATTQUA_0    ");        //livello qualità
            _YATTTYP       = getDBV<string>( row[11], "YATTTYP      ");       

            _YATTDATSAU_0 = getSageDate(row[12], "YATTDATSAU_0");
            _YATTDATTYC_0 = getSageDate(row[13], "YATTDATTYC_0");
            _YATTDATHAR_0 = getSageDate(row[14], "YATTDATHAR_0");
            _YATTLOTUM_0     = (row[15] == System.DBNull.Value) ? ' ' : getDBV<string>(row[15], "YATTLOTUM_0")[0];
            _YATTLEATIM_0 = getDBV<short>(row[16], "YATTLEATIM_0");


            C_CODE                               = EscapeSQL(_YATTCOD_0, 50);      // varchar 50
            C_PLANT_CODE                         = "ITS01";                          // varchar 20
            C_DESCR                              = EscapeSQL(_YATTDES_0, 50);     // varchar 50
            C_M_B                                = ' ';                              // char 1  o APPROVIGIONAMENTO?
            C_PHANTOM                            = 0;                                // bit 
            C_FIXED_LEAD_TIME                    = _YATTLEATIM_0;                     // int 
            C_VAR_LEAD_TIME                      = 0;                                // real 
            C_BUY_PREPROC_LT                     = 0;                                // real 
            C_BUY_PROC_LT                        = (int)0;                           // real 
            C_BUY_POSTPROC_LT                    = 0;                                // real 
            C_UNAVAIL_TIME                       = (int)0;                           // real 
            C_PROTECTION_TIME                    = 0;                                // real 
            C_UOM                                = EscapeSQL("", 3);                 // varchar 3
            C_UOM2                               = EscapeSQL("", 3);                 // varchar 3
            C_LENGTH                             = 0;                                // real 
            C_WIDTH                              = 0;                                // real 
            C_SCRAP_TYPE                         = ' ';                              // char 1
            C_PROCESS_SCRAP                      = (double)0;                        // real 
            C_STD_WAREHOUSE_CODE                 = EscapeSQL(__MAGAZZINO_INTERNO, 20);                // varchar 20
            C_ITEM_GROUP                         = __TOOL;                           // varchar 8
            C_MANAGER                            = "";                               // varchar 20
            C_HOST_CODE                          = "";                               // varchar 50
            C_SUPPLIER_CODE                      = EscapeSQL("", 30);                // varchar 30
            C_ABC_CLASS                          = ' ';                              // char 1
            C_VALUE                              = 0;                                // float	
            C_COST                               = 0;                                // float	
            C_MRP_TYPE                           = ' ';                              // char 1
            C_POQ_DAYS                           = 0;                                // int	
            C_POQ_HOURS                          = 0;                                // int	
            C_DTF                                = 0;                                // int	
            C_LOT_SIZE                           = 0;                                // numeric	
            C_MIN_ORDER_QTY                      = 0;                                // numeric	
            C_MAX_ORDER_QTY                      = 0;                                // numeric	
            C_DEM_GROUPING_QTY                   = 0;                                // numeric	
            C_DEM_GROUPING_MAX_QTY               = 0;                                // numeric	
            C_ROP_QTY                            = 0;                                // numeric	
            C_SS_QTY                             = 0;                                // numeric	
            C_USE_UNPEGGED_QTY                   = 0;                                // bit	
            C_USER_INT01                         = _QTY;                             // int	
            C_USER_INT02                         = _YATTQUA_0;                       // int	
            C_USER_INT03                         = 0;                                // int	
            C_USER_INT04                         = 0;                                // int	
            C_USER_INT05                         = 0;                                // int	
            C_USER_INT06                         = 0;                                // int	
            C_USER_INT07                         = 0;                                // int	
            C_USER_INT08                         = 0;                                // int	
            C_USER_INT09                         = 0;                                // int	
            C_USER_INT10                         = 0;                                // int	
            C_USER_REAL01                        = (float)_YATTMAT_0;                // float	//peso materozza
            C_USER_REAL02                        = 0;                                // float	
            C_USER_REAL03                        = 0;                                // float	
            C_USER_REAL04                        = 0;                                // float	
            C_USER_REAL05                        = 0;                                // float	
            C_USER_CHAR01                        = _YATTLOTUM_0;                        // char 1  Flag Vie/Pezzi       ///////mai fatto -> Categoria pe morsetti 
            C_USER_CHAR02                        = ' ';                              // char 1  ////////Piega o non pieag per FILO
            C_USER_CHAR03                        = ' ';                              // char 1
            C_USER_CHAR04                        = ' ';                              // char 1
            C_USER_CHAR05                        = ' ';                              // char 1
            C_USER_FLAG01                        = 0;                                // bit	
            C_USER_FLAG02                        = 0;                                // bit	
            C_USER_FLAG03                        = 0;                                // bit	
            C_USER_STRING01                      = EscapeSQL(_YATTVIN_0, 29);                // varchar 29
            C_USER_STRING02                      = getMarchi(_YATTTYP, _YATTMARSAU_0,_YATTMARTYC_0,_YATTMARHAR_0);                // varchar 29
            C_USER_STRING03                      = EscapeSQL("", 29);                // varchar 29  SEZIONE per FILO
            C_USER_STRING04                      = Attrezzature.GetTipoPLastica(_YATTTYP, _YATTFLGSTH_0,_YATTFLGPA66_0);                // varchar 29
            C_USER_STRING05                      = EscapeSQL("", 29);                // varchar 29
            C_USER_STRING06                      = EscapeSQL("", 29);                // varchar 29
            C_USER_STRING07                      = EscapeSQL("", 29);                // varchar 29
            C_USER_STRING08                      = EscapeSQL("", 29);                // varchar 29
            C_USER_STRING09                      = EscapeSQL("", 29);                // varchar 29
            C_USER_STRING10                      = EscapeSQL("", 29);                // varchar 29
            C_USER_NOTE01                        = "";                               // varchar 99
            C_USER_COLOR01                       = 0;                                // int	
            C_USER_COLOR02                       = 0;                                // int	
            C_USER_DATE01                        = _YATTDATSAU_0;                             // datetime	
            C_USER_DATE02                        = _YATTDATTYC_0;                             // datetime	
            C_USER_DATE05                        = _YATTDATHAR_0;                             // datetime	
            C_USER_DATE03                        = null;                             // datetime	
            C_USER_DATE04                        = null;                             // datetime	
            C_PROD_LOT_QTY                       = 0;
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
            string sage_query = @"SELECT
                      S.YATTCOD_0
                     ,S.YATTDES_0
                     ,S.YATTQTY_0
                     ,S.YATTVIN_0

                     ,S.YATTMAT_0
                     ,S.YATTFLGPA66_0
                     ,S.YATTFLGSTH_0
                     ,S.YATTMARSAU_0
                     ,S.YATTMARTYC_0
                     ,S.YATTMARHAR_0                     
                     ,S.YATTQUA_0     
                     ,S.YATTTYP_0

                     ,S.YATTDATSAU_0
                     ,S.YATTDATTYC_0
                     ,S.YATTDATHAR_0                     
                     ,S.YATTLOTUM_0
                     ,S.YATTLEATIM_0

                      from " + db + @".YPRDATT S 
                       where S.YATTENAFLG_0=2 "
                        ;
                       //and S.YATTTYP_0 <>'ASS' "
 


            //if (!string.IsNullOrWhiteSpace(codice_like))
            //{
            //    sage_query += " and B.BPRNUM_0 like '" + codice_like.Trim() + "'";
            //}
            return sage_query;
        }

        protected string getMarchi(string TipoAttrezzatura, short FlagSauro, short FlagTyco, short FlagHarting)
        {
            string ret = "";
            if (TipoAttrezzatura.ToUpper().StartsWith("PLA"))
            {
                if (FlagSauro == 2) ret += "1";
                if (FlagTyco == 2) ret += "2";
                if (FlagHarting == 2) ret += "3";

                if (ret == "")
                {
                    __bulk_message += Utils.NewLineMail() + "l'attrezzatura " + C_CODE + " ha tutti i marchi disabilitati";
                }
            }            
            return ret;
        }
        static public string GetTipoPLastica(string TipoAttrezzatura, short FlagSTH,short FlagPa66)
        {
            string ret = "";
            if (TipoAttrezzatura.ToUpper() == "PLA")
            {
                if (FlagSTH == 2) ret += "S";
                if (FlagPa66 == 2) ret += "P";
            }
            return ret;
        }
    }
}
