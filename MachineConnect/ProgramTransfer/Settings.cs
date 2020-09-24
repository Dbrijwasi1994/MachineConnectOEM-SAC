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

namespace CNC_PT
{   

    public static class SettingsPT
    {
        public static string shared_file = "";
        public static string Program_path = "";
        public static string connstr = "";        
        public static string IP;
        public static ushort PORT;
        public static string CPP_EXE ="";
        public static string file_to_open = "";
        public static string files_to_compare = "";
        public static bool IsProgramFoldersSupport;
        public static string SYS_fldr_slctd="";
        public static string SYS_pgm_slctd;
        public static string CNC_pgm_slctd;
        public static string CNC_fldr_slctd;
        public static Color sys_fldr_colr;
        public static TreeNode sys_node_selected;
        public static string pgm_selected_frm_grid;
        public static string pgm_Comment_selected_frm_grid;

        static SettingsPT()
        {
            Program_path = @"C:\TPMDNC\programs\";// ConfigurationManager.AppSettings["Program_path"];
            //connstr = ConfigurationManager.ConnectionStrings["TPMTrakConnString"].ConnectionString;

            //added
            //if (!Directory.Exists(Program_path))
            //{
            //    DirectoryInfo di = Directory.CreateDirectory(Program_path);
            //}
           
        }

        public static string SafeFileName(string name)
        {
            StringBuilder str = new StringBuilder(name);            
            foreach (char c in System.IO.Path.GetInvalidFileNameChars())
            {
                str = str.Replace(c, '_');
            }
            return str.ToString();
        }

    }

}
