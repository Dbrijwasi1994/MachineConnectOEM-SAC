using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading;
using System.Data;
using System.Windows.Forms;

namespace MachineConnectApplication
{
    public static class ConnectionManager
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["TPMConnectionString"].ToString();

        public static SqlConnection GetConnection()
        {
            bool writeDown = false;
            DateTime dt = DateTime.Now;
            SqlConnection conn = new SqlConnection(ConnectionString);
            do
            {
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    if (writeDown == false)
                    {
                        dt = DateTime.Now.AddSeconds(60);
                        Logger.WriteErrorLog(ex.Message);
                        writeDown = true;
                    }
                    if (dt < DateTime.Now)
                    {
                        Logger.WriteErrorLog(ex.Message);
                        CustomDialogBox frm = new CustomDialogBox("Warning Message", ex.Message);
                        frm.ShowDialog();              
                        break;
                    }
                    Thread.Sleep(1000);
                }

            } while (conn.State != ConnectionState.Open);
            return conn;
        }
    }
}
