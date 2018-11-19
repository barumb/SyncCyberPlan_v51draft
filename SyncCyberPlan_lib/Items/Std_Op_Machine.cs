using System.Collections.Generic;
using System;
using log4net;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace SyncCyberPlan_lib
{
    public class Std_Op_Machine : Item
    {
        static protected DataTable __dataTable_Conf_Attr_Macchine;

        public string  _YCONATT_0;
        //public string  _YCONCDL_0   ;
        public string _WST_0;
        public string  _YCONGRP_0   ;
        public decimal _YCONLOTSIZ_0;
        public string  _YCONLOTUM_0 ;
        public decimal _YCONCAD_0   ;
        public decimal _YCONCADTIM_0;
        public DateTime? _YCONDATRIA_0;
        public byte _YCONENAFLG_0;
        public string _WCR_0;   //flusso  (PLAS, MORS...reparto)



        #region tabella output CYB_STD_OP_MACHINE
        public string C_ROUTING_CODE; //[varchar] (51) NOT NULL,
        public string C_ROUTING_ALT;  //[varchar] (9) NOT NULL,
        public int C_OPNUM;        //[int] NOT NULL,
        public int C_ALT_OPNUM;    //[int] NOT NULL,
        public string MACHINE_CODE;          // varchar 30 NOT NULL
        public DateTime? C_EFFECTIVE_DATE;
        public DateTime? C_EXPIRE_DATE;

        public decimal LOT_SIZE;              // numeric      
        public int PREFERENCE;            // int      
        public int RUN_TIME;              // int      
        public int SETUP_TIME;            // int      
        public int WAIT_TIME;             // int      
        public byte ACTIVE;
        #endregion


        public Std_Op_Machine(): base("CYB_STD_OP_MACHINE")
        {
        }
        public override void Init(object[] row)
        {
            _YCONATT_0        = getDBV<string>(row[0]);
            _YCONGRP_0        = getDBV<string>(row[1]);
            _WST_0            = getDBV<string>(row[2]);
            _YCONLOTSIZ_0     = getDBV<decimal>(row[3]);
            _YCONLOTUM_0      = getDBV<string>(row[4]);
            _YCONCAD_0        = getDBV<decimal>(row[5]);
            _YCONCADTIM_0     = getDBV<decimal>(row[6]);
            _YCONENAFLG_0     = getDBV<byte>(row[7]);
            _YCONDATRIA_0     = getSageDate((DateTime)row[8]);
            _WCR_0            = getDBV<string>(row[9]);


            C_ROUTING_CODE = EscapeSQL(_YCONATT_0, 51);      // varchar 51  
            C_ROUTING_ALT = "0";
            C_OPNUM = 10;
            C_ALT_OPNUM = 0;

            MACHINE_CODE = EscapeSQL(_WST_0, 30);      // varchar 30  //MACCHINA
            C_EFFECTIVE_DATE = _YCONDATRIA_0;
            C_EXPIRE_DATE = null;

            LOT_SIZE = getLotSize(_WCR_0, _YCONLOTSIZ_0, _YCONCAD_0);                     // numeric      
            PREFERENCE = 0;                     // int      
            RUN_TIME = (int)_YCONCADTIM_0;                     // int      
            SETUP_TIME = 0;                     // int      
            WAIT_TIME = 0;                     // int      
            ACTIVE = (byte)(_YCONENAFLG_0 == 2 ? 1 : 0);
        }
        protected decimal getLotSize(string flusso,decimal LotSize, decimal Cadenza)
        {
            if (flusso == "PLAS")
            {
                return Cadenza; //per la plastica il lotsize è il numero di impronte
            }
            return LotSize * Cadenza;
        }
        public override DataRow GetCyberRow()
        {
            DataRow row_spec = checkRigaSpecifica(GetCyberRow(_dataTable));
            return row_spec;        
        }
        protected DataRow GetCyberRow(DataTable origine)
        {
            DataRow _tablerow = origine.NewRow();

            _tablerow[0] = C_ROUTING_CODE; //C_ROUTING_CODE; //[varchar] (51) NOT NULL,
            _tablerow[1] = C_ROUTING_ALT;  //C_ROUTING_ALT;  //[varchar] (9) NOT NULL,
            _tablerow[2] = C_OPNUM;        //[int] NOT NULL,
            _tablerow[3] = C_ALT_OPNUM;    //[int] NOT NULL,
            _tablerow[4] = MACHINE_CODE;   //MACHINE_CODE     varchar 30
            _tablerow[5] = DateTime_toCyb(C_EFFECTIVE_DATE);
            _tablerow[6] = DateTime_toCyb(C_EXPIRE_DATE);

            _tablerow[7] = LOT_SIZE;     // numeric      
            _tablerow[8] = PREFERENCE;   //    int      
            _tablerow[9] = RUN_TIME;     //    int      
            _tablerow[10] = SETUP_TIME;  //    int      
            _tablerow[11] = WAIT_TIME;   //    int         
            _tablerow[12] = ACTIVE;      //    bool

            return _tablerow;
        }
        protected DataRow checkRigaSpecifica(DataRow rigacorrente)
        {
            //se la riga che sto elaborando (riga dell'esploso delle configurazioni ATTREZZATURE/GRUPPO - senza macchina)
            //è presente nella tabella __dataTable_Conf_Attr_Macchine (con configurazioni attrezz/macchina specifiche)
            // allora va sostituita la riga con quella specifica
            DataRow ret = null;
            DataRow[] arrayrows= __dataTable_Conf_Attr_Macchine.Select("C_ROUTING_CODE = '" + _YCONATT_0 + "' and MACHINE_CODE = '" + _WST_0 +"'");
            if (arrayrows.Length == 1)
            {
                //
                //c'è una riga di configurazione specifica per attrezz/macchina
                //
                ret = _dataTable.NewRow(); //tabella dell'oggetto 
                ret.ItemArray = arrayrows[0].ItemArray.Clone() as object[];

                byte attivo = (byte)ret["ACTIVE"];
                var datariattivazione = ret["C_EFFECTIVE_DATE"];
                if (attivo == 0)
                {
                    //la riga è stata disattivata
                    //se c'è la data di riattivazione va passata
                    //altrimento la configurazione non è valida
                    if (datariattivazione == DBNull.Value)
                    {
                        //la data di riattivazione con c'è, va cancellata anche la riga corrente
                        ret = null;
                    }
                    else
                    {
                        //va passata la riga specifica
                    }
                }
                __dataTable_Conf_Attr_Macchine.Rows.Remove(arrayrows[0]);
            }
            else if (arrayrows.Length == 0)
            {
                //non c'è nessuna riga specifica, lascio la riga corrente
                ret = rigacorrente;
            }
            else
            {
                string msg = "Configurazione Attrezattura/Macchina: più di una riga presente " + _YCONATT_0 + "/" + _WST_0;
                Utils.SendMail("it@sauro.net", "francesco.chiminazzo@sauro.net", "mail.sauro.net", msg);
                var tmp = _dataTable.NewRow();
                tmp["C_ROUTING_CODE"] = _YCONATT_0;
                tmp["MACHINE_CODE"]= _WST_0;
                tmp["LOT_SIZE"] = -999;
                ret = tmp;
            }
            return ret;
        }
        public override List<DataRow> GetLastCyberRows()
        {
            return __dataTable_Conf_Attr_Macchine.AsEnumerable().ToList<DataRow>();
        }
        public override string GetSelectQuery(bool mode, string dossier, string codice_like, string tipo)
        {
            if (__dataTable_Conf_Attr_Macchine == null)
            {
                __dataTable_Conf_Attr_Macchine = InitTable_ConfigAttrezzMacchine(dossier);
            }

            string db = "x3." + dossier;

            // string sage_query = @"SELECT
            //           C.YCONATT_0
            //          ,C.YCONCDL_0
            //          ,C.YCONGRP_0
            //          ,C.YCONLOTSIZ_0
            //          ,C.YCONLOTUM_0
            //          ,C.YCONCAD_0
            //          ,C.YCONCADTIM_0
            //          ,C.YCONDATRIA_0
            //          ,C.YCONENAFLG_0
            //           from " + db + ".YPRDCONF C \n" +
            //           " where "+ //C.YCONENAFLG_0=2 and " +
            //           " C.YCONCDL_0<>'' " ;


            //31 ottobre 2018: unica tabella (non si usa più CYB_STD_OPERATION)
            //RECUPERO qui le configurazioni attrezz/GRUPPO (senza macchina)
            //da cui ricavo tramite join
            //tutte le combinazioni GRUPPO/MACCHINA possibili
            string sage_query = @" SELECT
                      C.YCONATT_0
                     ,C.YCONGRP_0
                     ,W.WST_0
                     ,C.YCONLOTSIZ_0
                     ,C.YCONLOTUM_0
                     ,C.YCONCAD_0
                     ,C.YCONCADTIM_0
                     ,C.YCONENAFLG_0
                     ,C.YCONDATRIA_0
                     ,W.WCR_0
                      from x3.SAURO.YPRDCONF C
                     join SAURO.WORKSTATIO W  on C.YCONGRP_0 = W.YGRP_0
                      where YCONCDL_0 = ''
                      order by YCONATT_0, WST_0 desc";


            //C.YCONENAFLG_0=2 and 
            //C.YCONCDL_0 <> '';
            //and YCONATT_0 like 'STP014%'



            //if (!string.IsNullOrWhiteSpace(codice_like))
            //{
            //    sage_query += " and B.BPRNUM_0 like '" + codice_like.Trim() + "'";
            //}
            return sage_query;
        }
        public override string GetID()
        {
            return MACHINE_CODE;
        }

        public override void InitDataTable()
        {
            init_data_table(_dataTable);
        }

        static void init_data_table(DataTable table)
        {
            table.Columns.Add("C_ROUTING_CODE", typeof(string));
            table.Columns.Add("C_ROUTING_ALT", typeof(string));
            table.Columns.Add("C_OPNUM", typeof(int));
            table.Columns.Add("C_ALT_OPNUM", typeof(int));
            table.Columns.Add("MACHINE_CODE", typeof(string));
            table.Columns.Add("C_EFFECTIVE_DATE", typeof(DateTime));
            table.Columns.Add("C_EXPIRE_DATE", typeof(DateTime));

            table.Columns.Add("LOT_SIZE", typeof(decimal));
            table.Columns.Add("PREFERENCE", typeof(int));
            table.Columns.Add("RUN_TIME", typeof(int));
            table.Columns.Add("SETUP_TIME", typeof(int));
            table.Columns.Add("WAIT_TIME", typeof(int));
            table.Columns.Add("ACTIVE", typeof(byte));

        }
        static protected DataTable InitTable_ConfigAttrezzMacchine(string dossier)
        {
            //recupero dati Configurazione Attrezzature Macchine dove c'è la macchina, non solo il gruppo
            DataTable ret = new DataTable();
            init_data_table(ret);

            string query = @"SELECT
                      C.YCONATT_0
                     ,C.YCONGRP_0
                     ,C.YCONCDL_0 as WST_0
                     ,C.YCONLOTSIZ_0
                     ,C.YCONLOTUM_0
                     ,C.YCONCAD_0
                     ,C.YCONCADTIM_0
                     ,C.YCONENAFLG_0
                     ,C.YCONDATRIA_0
                     ,W.WCR_0
                      from SAURO.YPRDCONF C
                     join SAURO.WORKSTATIO W  on C.YCONCDL_0 = W.WST_0
                      where YCONCDL_0 <> '' 
                      order by YCONATT_0, YCONCDL_0 desc";

            DBHelper2 db = DBHelper2.getSageDBHelper(dossier);
            DbDataReader dtr = db.GetReaderSelectCommand(query);
            object[] row = new object[dtr.FieldCount];

            //MEGA accrocchio per velocità: uso lo stesso oggetto 
            Std_Op_Machine tmp = null;
            while (dtr.Read())
            {
                dtr.GetValues(row);
                tmp = new Std_Op_Machine();
                tmp.Init(row);
                
                ret.Rows.Add(tmp.GetCyberRow(ret));//accrocchio per evitare che la riga derivi dall'altra tabella 
            }
            return ret;
        }
    }
}
