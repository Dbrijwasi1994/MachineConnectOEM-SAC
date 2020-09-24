using MachineConnectApplication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MachineConnectOEM
{
    public partial class IRSLogin : Form
    {
        public IRSLogin()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.btnLogin.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            validateFormFields();
            try
            {
                if (!string.IsNullOrEmpty(txtUsername.Text) && !string.IsNullOrEmpty(txtPassword.Text))
                {
                    if (DatabaseAccess.IRSListLogin(txtUsername.Text, txtPassword.Text))
                    {
                        Settings.AccesAllowed = true;
                        Close();
                    }
                    else
                    {
                        CustomDialogBox popup = new CustomDialogBox("Invalid Credentials", "The Username or Password you'd entered is incorrect. Please try again.");
                        popup.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                CustomDialogBox frm = new CustomDialogBox("Information Message", ex.Message.ToString());
                frm.ShowDialog();
            }
        }

        private void validateFormFields()
        {
            string msg = string.Empty;
            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                msg = "Please Enter User Name.";
                CustomDialogBox popup = new CustomDialogBox("Error Message", msg);
                popup.ShowDialog();
                txtUsername.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                msg = "Please Enter Password.";
                CustomDialogBox popup = new CustomDialogBox("Error Message", msg);
                popup.ShowDialog();
                txtPassword.Focus();
                return;
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }
    }
}
