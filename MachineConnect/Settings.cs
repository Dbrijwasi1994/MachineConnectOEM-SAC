using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Configuration;
using System.Diagnostics;

namespace MachineConnectApplication
{
    class Settings
    {
        public static string APP_PATH = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public static int AutoRefreshInterval = 2;
        public static string StoppagesThreshold = "5";
        public static int StatusUpdateTimerIntervalInSec = Convert.ToInt32(ConfigurationManager.AppSettings["StatusUpdateTimerIntervalInSec"].ToString());
        public static int ANDONFlipInterval = Convert.ToInt32(ConfigurationManager.AppSettings["ANDONFlipInterval"].ToString());
        public static int ParamRefreshInterval = Convert.ToInt32(ConfigurationManager.AppSettings["ParamRefreshInterval"].ToString());
        public static int CmbDurationSelectedIndex = 3;
        public static int CmbDurationTypeSelectedIndex = 0;

        public static string ERROR_MSG = "Error Message";
        public static string INFO_MSG = "Information Message";

        public static string ServerAndDbs = ConfigurationManager.AppSettings["ServerDbs"];
        //public static int AutoListRefreshInterval = 0;

        public static string SuperAdmin_UserName = "amit";
        public static string SuperAdmin_Password = "Gogreen$Earth706";

        public static bool InAndonMode { get; set; }
        public static bool AccesAllowed = false;

        internal static void OpenExe(string path)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "\"" + path + "\"";
            Process.Start(startInfo);
        }   

        public static Form GetFormInstance(string ScreenName)
        {
            Form form = null;
            List<Form> openForms = new List<Form>();
            foreach (Form f in Application.OpenForms)
                openForms.Add(f);

            foreach (Form f in openForms)
            {
                if (f.Name.Equals(ScreenName))
                {                   
                    if (ScreenName.Equals("MainScreen"))
                    {
                        MainScreen frm = f as MainScreen;
                        if (frm != null)
                        {
                            return form = frm;
                        }                      
                    }
                }               

            }
            return form;
        }

        public static void WriteErrorMsg(string errorVal)
        {
            Logger.WriteErrorLog("Error - \n " + errorVal);
        }

        public static Image CheckForPath(string imagePath)
        {
            Image image = null;
            if (File.Exists(imagePath))
            {
                image = Image.FromFile(imagePath);
            }
            else
            {
                CustomDialogBox cb = new CustomDialogBox("Error Message", "File not found - " + imagePath);
                cb.ShowDialog();
                //MessageBox.Show("File not found - " + imagePath);
            }
            return image;
        } 

        public static void ShowErrorMsg(string errorVal)
        {
            WriteErrorMsg(errorVal);
            CustomDialogBox cmb = new CustomDialogBox(ERROR_MSG, errorVal);
            cmb.ShowDialog();
        }
      
        public static void ShowInfoMsg(string msg = "Message")
        {
            if (msg.Equals("Message"))
            {
                CustomDialogBox cmb = new CustomDialogBox(INFO_MSG, "Details Updated Successfully.");
                cmb.ShowDialog();
            }
            else
            {
                CustomDialogBox cmb = new CustomDialogBox(INFO_MSG, msg);
                cmb.ShowDialog();
            }
        }     
      
    }
}
