using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace MachineConnectApplication
{
    public partial class MachineManual : UserControl
    {

        string appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public MachineManual()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            MachineManual_Load(null, EventArgs.Empty);
        }

        private void DisposePanelControls()
        {
            foreach (Control p in pnlContainer.Controls)
            {
                p.Dispose();
            }
        }        

        private void MachineManual_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;           
            DisposePanelControls();
            pnlContainer.Controls.Clear();
            lblHeader.Text = " MANUALS ";
            btnBack.Visible = false;
            MachineMenuControl ctrl = new MachineMenuControl(this);
            ctrl.userControl = this;
            ctrl.Dock = DockStyle.Fill;
            pnlContainer.Controls.Add(ctrl);
            this.Cursor = Cursors.Default;
        }

       
    }
}
