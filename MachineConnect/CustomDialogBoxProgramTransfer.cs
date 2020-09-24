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
    public partial class CustomDialogBoxProgramTransfer : Form
    {
        public CustomDialogBoxProgramTransfer()
        {
            InitializeComponent();
        }

        public CustomDialogBoxProgramTransfer(string headerMsg, string msg)
        {
            InitializeComponent();
            this.lblHeaderText.Text = headerMsg;
            this.lblText.Text = msg;          
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblText_TextChanged(object sender, EventArgs e)
        {
            Size textBoxRect = TextRenderer.MeasureText(this.lblText.Text, this.lblText.Font, new Size(this.lblText.Width, int.MaxValue),TextFormatFlags.WordBreak | TextFormatFlags.TextBoxControl);
            try
            {
                this.lblText.ScrollBars = textBoxRect.Height > this.lblText.Height ? ScrollBars.Vertical : ScrollBars.None;
            }
            catch (System.ComponentModel.Win32Exception)
            {
                
            }

        }

       
    }
}
