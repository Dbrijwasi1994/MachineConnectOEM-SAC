using System.Data.SqlClient;
using System;
using System.Windows.Forms;
using FocasLibrary;
namespace CNC_PT
{
    public static class ConnectionManagerPT
    {
        public static SqlConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection(SettingsPT.connstr);
            try
            {
                
                conn.Open();
                return conn;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.WriteErrorLog(ex.ToString());
                return null;
            }
        }
    }
}
