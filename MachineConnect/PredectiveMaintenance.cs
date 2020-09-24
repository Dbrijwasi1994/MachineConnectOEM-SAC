using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MachineConnectApplication
{
    public partial class PredectiveMaintenance : UserControl
    {
        public PredectiveMaintenance()
        {
            //Commented by satya on 15 may 2016 to solve the WPF control disappear issue
            //this.DoubleBuffered = true;
            //this.SetStyle(ControlStyles.UserPaint |
            //              ControlStyles.AllPaintingInWmPaint |
            //              ControlStyles.ResizeRedraw |
            //              ControlStyles.ContainerControl |
            //              ControlStyles.OptimizedDoubleBuffer |
            //              ControlStyles.SupportsTransparentBackColor
            //              , true);


            InitializeComponent();
        }

        //Commented by satya on 15 may 2016 to solve the WPF control disappear issue
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= 0x02000000;
        //        return cp;
        //    }
        //}
    }
}
