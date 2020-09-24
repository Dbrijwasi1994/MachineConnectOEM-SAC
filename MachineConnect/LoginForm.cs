using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MachineConnectOEM;

namespace MachineConnectApplication
{
    public partial class LoginForm : Form
    {      
        //public static string selectedMachineId = string.Empty;
        public static string LoginUserName = string.Empty;
        public static string LoginPassword = string.Empty;
        public static string IsAdmin = string.Empty;
        public static bool FocasHasMachines = false;

        public LoginForm()
        {
            InitializeComponent();           
        }       

        private void validateFormFeilds()
        {
            string msg = string.Empty;
            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                msg = "Please Enter Employee Id.";
                CustomDialogBox frm = new CustomDialogBox("Error Message", msg);
                frm.ShowDialog();
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                msg = "Please Enter Password.";
                CustomDialogBox frm = new CustomDialogBox("Error Message", msg);
                frm.ShowDialog();
                txtPassword.Focus();
                return;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                LoginForm_Shown1(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;  
                Logger.WriteErrorLog(ex.ToString());
            }
            this.Cursor = Cursors.Default;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoginForm_Shown1(object sender, EventArgs e)
        {
            try
            {               
                List<string> machines = DatabaseAccess.GetAllMachines();
                if (machines.Count <= 0)
                {
                    validateFormFeilds();
                    if (!string.IsNullOrEmpty(txtUsername.Text) && !string.IsNullOrEmpty(txtPassword.Text))
                    {
                        this.Cursor = Cursors.WaitCursor;
                        if ((txtUsername.Text.Equals(Settings.SuperAdmin_UserName) && (txtPassword.Text.Equals(Settings.SuperAdmin_Password)))  ||
                            DatabaseAccess.CheckEmployeeDetail(txtUsername.Text, txtPassword.Text, out IsAdmin))
                        {
                            UpgradeDatabase.RunScripts();
                            MachineInfoForm frm = new MachineInfoForm(this);
                            frm.Show();
                            this.Hide();
                        }
                        else
                        {
                            CustomDialogBox frm = new CustomDialogBox("Error Message", "Please Enter Valid Credentials.");
                            frm.ShowDialog();
                        }
                        this.Cursor = Cursors.Default;
                    }
                }
                else
                {
                    validateFormFeilds();
                    if (!string.IsNullOrEmpty(txtUsername.Text) && !string.IsNullOrEmpty(txtPassword.Text))
                    {
                        this.Cursor = Cursors.WaitCursor;
                        if ((txtUsername.Text.Equals(Settings.SuperAdmin_UserName) && (txtPassword.Text.Equals(Settings.SuperAdmin_Password))) || 
                            DatabaseAccess.CheckEmployeeDetail(txtUsername.Text, txtPassword.Text, out IsAdmin))
                        {
                            FocasHasMachines = true;
                            UpgradeDatabase.RunScripts();
                            MainScreen mainForm = new MainScreen();
                            mainForm.Show();
                            this.Hide();
                        }
                        else
                        {
                            CustomDialogBox frm = new CustomDialogBox("Error Message", "Please Enter Valid Credentials.");
                            frm.ShowDialog();
                        }
                        this.Cursor = Cursors.Default;
                    }
                }


                if((txtUsername.Text.Equals(Settings.SuperAdmin_UserName) && (txtPassword.Text.Equals(Settings.SuperAdmin_Password))))
                {
                    IsAdmin = "1";
                }
                LoginUserName = txtUsername.Text;
                LoginPassword =  txtPassword.Text;

                ApplicationSettingsVals val = DatabaseAccess.GetApplicationUISettings();
                Settings.AutoRefreshInterval = Convert.ToInt32(val.AutoRefreshInterval);
                Settings.StoppagesThreshold = val.StoppagesThreshold;
                CNC_PT.SettingsPT.Program_path = val.ProgramsPath;
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
                CustomDialogBox frm = new CustomDialogBox("Error Message", ex.Message);
                frm.ShowDialog();
            }
        }

        private void LoginForm_Shown(object sender, EventArgs e)
        {
            txtUsername.Focus();
        }       

    }
}
