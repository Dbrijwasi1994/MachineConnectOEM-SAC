using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using System.Configuration;
using System.Threading;
using FocasLibrary;
using MachineConnectApplication;
namespace CNC_PT
{
    public class DatabaseAccessPT
    {
        SqlConnection conn;
        public DatabaseAccessPT()
        {
        }
        public List<string> GetPlantList()
        {
            List<string> lst = null;
            conn = ConnectionManagerPT.GetConnection();
            SqlDataReader rdr = null;
            string qry = "select  distinct plantid from plantinformation";
            SqlCommand cmd = new SqlCommand(qry, conn);
            try
            {
                rdr = cmd.ExecuteReader();
                lst = (GetListOfData(rdr));

            }
            catch (Exception ex)
            {
                MachineConnectApplication.Logger.WriteErrorLog(ex.Message);
                // return null;
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                    rdr.Dispose();
                }
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                    conn = null;
                }
            }
            return lst;

        }
        public List<string> GetMachines(string PlantId)
        {
            List<string> Lst = null;
            conn = ConnectionManagerPT.GetConnection();
            SqlDataReader rdr = null;
            string qry;
           
            qry = @"select distinct machineid from machineinformation where ethernetenabled=1 and machineid
                  in (select distinct machineid from plantmachine where plantid= @pl_id)";            
            SqlCommand cmd = new SqlCommand(qry, conn);
            cmd.Parameters.Add("@pl_id", SqlDbType.NVarChar).Value = PlantId;
            try
            {
                rdr = cmd.ExecuteReader();
                Lst = (GetListOfData(rdr));
            }
            catch (Exception ex)
            {
                MachineConnectApplication.Logger.WriteErrorLog(ex.Message);
                // return null;
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                    rdr.Dispose();
                }
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                    conn = null;
                }
            }
            return Lst;
        }
        private List<string> GetListOfData(SqlDataReader rdr)
        {
            if (rdr.HasRows)
            {
                List<string> lst = new List<string>();
                try
                {
                    while (rdr.Read())
                    {
                        lst.Add(rdr[0].ToString());
                    }
                    return lst;
                }
                catch (Exception ex)
                {
                    MachineConnectApplication.Logger.WriteErrorLog(ex.Message);
                    return null;
                }
            }
            else
            {
                return null;
            }
        }


        public static void GetIpPort(string mid, out  string ip, out ushort port, out bool isProgramFoldersSupport)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            ip = " ";
            port = 9813;
            isProgramFoldersSupport = false;
            try
            {
                string qry = "select  DNCIP, DNCIPPortNo, ProgramFoldersEnabled from  machineinformation where machineid=@mid";
                conn = ConnectionManager.GetConnection();
                cmd = new SqlCommand(qry, conn);
                cmd.Parameters.Add("@mid", SqlDbType.NVarChar).Value = mid;
                rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    rdr.Read();
                    ip = (string)rdr[0];
                    ushort.TryParse((string)rdr[1], out port);
                    bool.TryParse(rdr[2].ToString(), out isProgramFoldersSupport);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                rdr.Close();
                rdr.Dispose();
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

    }
}
