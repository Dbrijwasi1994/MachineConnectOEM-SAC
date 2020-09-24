using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MachineConnectApplication
{
    public partial class MachineInfoForm : Form
    {
        LoginForm formVal = null;

        public MachineInfoForm(Form frmVal)
        {
            InitializeComponent();
            this.formVal = frmVal as LoginForm;

            //if (MachineInformation.NoOfRows > 0)
            //{
            //    btnProceed.Visible = true;
            //}
            //else
            //{
            //    btnProceed.Visible = false;
            //}
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            //Application.Exit();
            this.Close();         
            formVal.Show();
        }

        private void btnProceed_Click(object sender, EventArgs e)
        {
            MainScreen frm = new MainScreen();
            frm.Show();
            this.Hide();
        }
    }
}
