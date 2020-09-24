using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MachineConnectOEM
{
    public partial class CycleProfileWindow : Form
    {
        public CycleProfileWindow()
        {
            InitializeComponent();
        }

        private void CycleProfileWindow_Load(object sender, EventArgs e)
        {
            CycleProfile cycleProfile = new CycleProfile();
            cycleProfile.Dock = DockStyle.Fill;
            panelMain.Controls.Add(cycleProfile);
        }
    }
}
