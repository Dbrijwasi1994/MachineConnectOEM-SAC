using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FocasGUI
{
    public partial class DialogBox : Form
    {
        public static bool IsCloseApplication = false;
        string lblMessage = string.Empty;
        public static bool IsToResetAppSettings;

        public DialogBox(string lblMessage)
        {
            InitializeComponent();
            this.lblMessage = lblMessage;
        }
    
        private void btnOk_Click(object sender, EventArgs e)
        {
            IsCloseApplication = true;
            IsToResetAppSettings = true;
            this.Close();
        }

        private void DialogBox_Load(object sender, EventArgs e)
        {
            lblText.Text = lblMessage;           
        }
     
        private void CustomDialogBox_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            btnClose_Click(null, EventArgs.Empty);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            IsCloseApplication = false;
            this.Close();
        }
    }
}
