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
    public partial class CustomDialogBox : Form
    {
        public CustomDialogBox()
        {
            InitializeComponent();
        }

        public CustomDialogBox(string headerMsg,string msg)
        {
            InitializeComponent();
            this.lblHeaderText.Text = headerMsg;
            this.lblText.Text = msg;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
