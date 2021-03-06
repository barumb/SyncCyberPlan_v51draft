﻿using System.Collections.Generic;
using System;
using log4net;
using System.Data;

namespace SyncCyberPlan_lib
{
    /// <summary>
    /// usato in particolare per FAMP
    /// </summary>
    public class Articolo_Caratteristiche : Item
    {
        public static long _ID_COUNTER=0;
        #region tabella output YITMCAR
        public long _ID;
        public string _ITMREF;
        public string _VALCOD;
        public decimal _VALVAL;
        public string _VALTXT;
        public int _PRIORITA;
        #endregion


        public Articolo_Caratteristiche(): base("YITMCAR")
        {
            throw new NotSupportedException();
        }

        public override void Init(object[] row)
        {
            _ID_COUNTER++;
            _ID = _ID_COUNTER;
            _ITMREF    = getDBV<string> (row[0],"ITMREF  "); 
            _VALCOD    = getDBV<string> (row[1],"VALCOD  "); 
            _VALVAL    = getDBV<decimal>(row[2],"VALVAL  "); 
            _VALTXT    = getDBV<string> (row[3],"VALTXT  "); 
            _PRIORITA  = getDBV<int>    (row[4],"PRIORITA"); 
        }
        public override DataRow GetCyberRow()
        {
            DataRow _tablerow = _dataTable.NewRow();
            _tablerow[0] = _ID;
            _tablerow[1] = _ITMREF;
            _tablerow[2] = _VALCOD;
            _tablerow[3] = _VALVAL;  
            _tablerow[4] = _VALTXT;
            _tablerow[5] = _PRIORITA;
            return _tablerow;
        }
        public override string GetSelectQuery(bool mode, string dossier, string codice_like, string tipo)
        {
            string db = "x3." + dossier;
            db = dossier;
            //string sage_query = "SELECT A.ITMREF_0, ITMSTA_0 from " + dossier + ".[ITMMASTER] A join " + dossier + ".[YITMINF] B on A.ITMREF_0=B.ITMREF_0" +
            //    " WHERE YLIVTRAS_0='" + tipo + "' and ITMSTA=1 ";

            //recupero caratteristiche codice e 
            //caratteristiche famiglia versione, con 2 priorità diverse 
            string sage_query = @"SELECT ITMREF, VALCOD, VALVAL, VALTXT, PRIORITA FROM
                        (
	                        select M.ITMREF_0 as ITMREF, YFVVALCOD_0 AS VALCOD, YFVVALVAL_0 AS VALVAL, YFVVALTXT_0 AS VALTXT, 50 AS PRIORITA
	                        from " + db + @".ITMMASTER M
	                        join " + db + @".YITMINF F on M.ITMREF_0 = F.ITMREF_0
	                        join " + db + @".YFAMVERD D on F.YFAMVER_0 = D.YFVFAMVER_0 
                        UNION
	                        select ITMREF_0 as ITMREF, YVALCOD_0 AS VALCOD, YVALVAL_0 AS VALVAL, YVALTXT_0 AS VALTXT, 100 AS PRIORITA
	                        From " + db + @".ITMMASTER M
	                        join " + db + @".YITMVAL Y on Y.YVALITMREF_0 = M.ITMREF_0  
                        ) U
            ";

            if (!string.IsNullOrWhiteSpace(codice_like))
            {
                sage_query += " and ITMREF like '" + codice_like.Trim() + "'";
            }
            sage_query += " ORDER BY ITMREF ";
            return sage_query;
        }

        public override string GetID()
        {
            return _ITMREF + _VALCOD;
        }

        public override void InitDataTable()
        {
            _dataTable.Columns.Add("ID", typeof(long));         //bigint con  identity
            _dataTable.Columns.Add("ITMREF", typeof(string));   //nvarchar(20)
            _dataTable.Columns.Add("VALCOD", typeof(string));   //nvarchar(20)
            _dataTable.Columns.Add("VALVAL", typeof(decimal));  //decimal(9,2)
            _dataTable.Columns.Add("VALTXT", typeof(string));   //nvarchar(20)
            _dataTable.Columns.Add("PRIORITA", typeof(int));   //smallint         
        }

        public override void LastAction(ref DBHelper2 cm, DBHelper2 sage)
        {
            Update_FAMP(ref cm);
        }
        static void Update_FAMP(ref DBHelper2 cm)
        {
            //// caratteristiche 
            //FAMP ...
            //E estrattori
            //DKTHQ plurime
            // X tutto il resto


            /*
             * Query per avere le caratteristiche valide:
             * quelle dell'articolo hanno la precedenza su quelle della famiglia versione
             * quelle della famiglia vanno considerate solo se l'articolo non ha la stessa caratteristica impostata
             
SELECT a.itmref, a.valcod, a.valval, a.valtxt,a.PRIORITA
FROM [CyberPlanFrontiera].[dbo].[YITMCAR] a
INNER JOIN (
    SELECT itmref, valcod, MAX([PRIORITA]) PRIORITA
    FROM [CyberPlanFrontiera].[dbo].[YITMCAR]
    GROUP BY itmref,valcod
) b ON a.itmref = b.itmref and a.valcod=b.valcod AND a.priorita = b.priorita
order by a.itmref,a.valcod
    */

            /*
           QUERY per recuperare le caratteristiche FAMP  

SELECT a.itmref, 
--a.valcod, a.valval, a.valtxt,a.PRIORITA,
case a.VALCOD 
when '028' then 'F'
when '006' then 'A'
when '038' then 'M'
when '051' then 'P'
end AS FAMP
FROM [CyberPlanFrontiera].[dbo].[YITMCAR] a
INNER JOIN (
    SELECT itmref, valcod, MAX([PRIORITA]) PRIORITA
    FROM [CyberPlanFrontiera].[dbo].[YITMCAR]
    GROUP BY itmref,valcod
) b ON a.itmref = b.itmref and a.valcod=b.valcod AND a.priorita = b.priorita
where
(
a.valcod = '028' or              
a.valcod = '006' or               
a.valcod = '038' or               
a.valcod = '051'                  
)
order by a.itmref,a.valcod desc



             */

            //query che prende le caratt FAMP (articlo e famiglia versione) e aggiorna gli articoli
            //nei casi ci siano caratt multiple ne viene presa una a caso (l'ultima nell'ordine di sql)
            /*
             Con una query grop by seleziono per ogni articolo/caratteristica la MAX priorità
             Poi con un join recupero valore con priorità massima ed eventuale valore TESTO (per plurime)
             */
            string upd_query = @"update [CyberPlanFrontiera].dbo.CYB_ITEM 
                        set C_USER_CHAR03  = C.FAMP

                        from 
                        (
                        SELECT a.itmref, 
                        --a.valcod, a.valval, a.valtxt,a.PRIORITA,
                        case a.VALCOD 
                        when '028' then 'F'
                        when '006' then 'A'
                        when '038' then 'M'
                        when '051' then 'P'
                        when '057' then 'E'
                        when '150' then a.VALTXT
                        end AS FAMP
                        FROM [CyberPlanFrontiera].[dbo].[YITMCAR] a
                        INNER JOIN (
                            SELECT itmref, valcod, MAX([PRIORITA]) PRIORITA
                            FROM [CyberPlanFrontiera].[dbo].[YITMCAR]
                            GROUP BY itmref,valcod
                        ) b ON a.itmref = b.itmref and a.valcod=b.valcod AND a.priorita = b.priorita
                        where
                        a.valcod = '028' or              
                        a.valcod = '006' or               
                        a.valcod = '038' or               
                        a.valcod = '051' or 
                        a.valcod = '057' or 
                        a.valcod = '150'                 
                        --order by a.itmref,a.valcod desc
                        ) C

                        where C_CODE = C.ITMREF;



            update CyberPlanFrontiera.dbo.CYB_ITEM 
            set [C_USER_CHAR03 ]='X'
            where [C_USER_CHAR03 ] =' 'and  [C_USER_STRING01 ]  like 'PLAS'

";



            int i = DBHelper2.EseguiSuDBCyberPlan(ref cm, upd_query);
        }
    }
}
