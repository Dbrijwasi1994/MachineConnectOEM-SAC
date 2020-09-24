using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MachineConnectApplication;
using System.Data.SqlClient;

namespace MachineConnectOEM
{
    class UpgradeDatabase
    {
        public static void RunScripts()
        {
            if (Properties.Settings.Default.FirstRun== true)
            {
                SqlConnection _sqlConn = ConnectionManager.GetConnection();
                SqlCommand cmd = new SqlCommand(@"IF NOT  EXISTS(
                                                SELECT *
                                                FROM sys.columns 
                                                WHERE Name      = N'ProgramFoldersEnabled'
                                                  AND Object_ID = Object_ID(N'machineinformation'))
                                            BEGIN
                                               Alter table machineinformation Add ProgramFoldersEnabled bit NOT NULL DEFAULT(0) 
                                            END", _sqlConn);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 120;
                try
                {
                    cmd.ExecuteNonQuery();
                    Properties.Settings.Default.FirstRun = false;
                    Properties.Settings.Default.Save();
                }
                catch (Exception exx)
                {
                    Logger.WriteErrorLog(exx.ToString());
                }
            }
        }
    }
}
