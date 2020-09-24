using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace MachineConnectOEM.SAC
{
    public partial class DefinePMRules_Win : UserControl
    {
        public DefinePMRules_Win()
        {
            InitializeComponent();
            ElementHost host = new ElementHost();
            host.AutoSize = true;
            host.MaximumSize = new System.Drawing.Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height - 200);
            //host.Size = new Size(1000, 600);
            //host.Size = new Size(this.Parent.Width, this.Parent.Height);
            //host.Location = new Point(100, 100);

            DefinePMRules pmrules = new DefinePMRules();
            host.Child = pmrules;

            this.Controls.Add(host);
        }
    }
}
