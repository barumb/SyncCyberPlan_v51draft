using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Add
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.SqlClient;
using log4net;
using System.IO;


namespace SyncCyberPlan_lib
{
    public class FIXAS4DesArt
    {



        SqlConnection _connIN;
        SqlCommand _cmdIN;
        SqlDataReader _rdIN;
        SqlDataAdapter _adpIN;
        DataTable _tbIN;

        OdbcConnection _connOUT;
        OdbcCommand _cmdOUT;
        
        string _sqlReader;
        string _sqlCmdUpd;
        string _As400LibName;
        string _x3Dossier;

        public FIXAS4DesArt(string pLibAs400,
                            string pDossierX3)
        {


            _connIN = new SqlConnection();
            _cmdIN = new SqlCommand();

            _connOUT = new OdbcConnection();
            _cmdOUT = new OdbcCommand();

            _As400LibName = pLibAs400.ToUpper().Trim();
            _x3Dossier = pDossierX3.ToUpper().Trim();

            _sqlReader = @"select X3.ITMREF_0, Rtrim(left(DLN.TEXTE_0,30)) AS LNGDES, Rtrim(left(DSH.TEXTE_0,12)) as SHODES
                           from SAURO.YITMINF  X3
                           left join " + _x3Dossier + @".ATEXTRA DLN
                           ON (X3.ITMREF_0=DLN.IDENT1_0 AND DLN.LANGUE_0='ITA' AND DLN.CODFIC_0 = 'YITMINF' AND DLN.ZONE_0 = 'YDESAUTAXX' )
                           left join " + _x3Dossier + @".ATEXTRA DSH
                           ON (X3.ITMREF_0=DSH.IDENT1_0 AND DSH.LANGUE_0='ITA' AND DSH.CODFIC_0 = 'YITMINF' AND DSH.ZONE_0 = 'YDESAUTAX3' )
                           left join (select * FROM P8DATA.S21C986V."+ _As400LibName + @".ART00PF) AS4
                           ON (X3.ITMREF_0=AS4.ARTCART)
                           WHERE X3.YOGG_0 IN ('054','064')";


             _connOUT.ConnectionString = @"Driver={iSeries Access ODBC Driver};System=isauro.sauro.dmn;Uid=DMZ;Pwd=6dmzadm;MGDSN=0;";
            _connIN.ConnectionString = @"Server=srvx3app1\SAGEX3;Database=x3;User Id=" + _x3Dossier + "; Password=tiger;";


            try
            {
                // per aggiornare Desc in AS400
                _connOUT.Open();
               
                _cmdOUT.Connection = _connOUT;
                _cmdOUT.CommandText = "";
                _cmdOUT.CommandType = System.Data.CommandType.Text;

                // Per recuperare Desc da Sage
                _connIN.Open();
                _cmdIN.Connection = _connIN;
                _cmdIN.CommandText = _sqlReader;
                _cmdIN.CommandType = System.Data.CommandType.Text;

                //_adpIN = new SqlDataAdapter();
                //_adpIN.SelectCommand = _cmdIN;
                //_tbIN = new DataTable();



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }


        public int UpdateAS400Desc() 
        {
            SqlDataReader _rdx3 =  _cmdIN.ExecuteReader();
            string _sqlUpdate;

            string longDesc;
            string shortDesc;

            long i = 0;

            if (_rdx3.HasRows == true)
            {
                
                while (_rdx3.Read())
                {
                    try
                    {
                        longDesc = _rdx3["LNGDES"].ToString();
                        shortDesc = _rdx3["SHODES"].ToString();
                        Console.WriteLine(i.ToString().ToString() + "\tART="+ _rdx3["ITMREF_0"].ToString() + "\t longdesc=" + longDesc + "\t\t schortdesc=" + shortDesc);

                        // UPD ART00PF
                        _sqlUpdate = @"UPDATE "+ _As400LibName+@".ART00PF "
                                  + @"SET  ARTDESC='" + longDesc + @"', ARTDESI='" + shortDesc + @"' "
                                  + @"WHERE  ARTCART='" + _rdx3["ITMREF_0"].ToString() + @"' " + Environment.NewLine;
                        _cmdOUT.CommandText = _sqlUpdate;
                        _cmdOUT.ExecuteNonQuery();


                        // UPD DADL00F (descrizione Inglese)
                        _sqlUpdate = @"UPDATE " + _As400LibName + @".DADL00F "
                                  + @"SET  DEART='" + longDesc + @"' "
                                  + @"WHERE  CDART='" + _rdx3["ITMREF_0"].ToString() + @"' " + Environment.NewLine;
                        _cmdOUT.CommandText = _sqlUpdate;
                        _cmdOUT.ExecuteNonQuery();
                    }
                    catch (Exception ex1)
                    {
                        Console.WriteLine(ex1.Message);
                    }
                    i++;
                }
                _rdx3.Close();
            }




            return 0;
        }

    }
}
