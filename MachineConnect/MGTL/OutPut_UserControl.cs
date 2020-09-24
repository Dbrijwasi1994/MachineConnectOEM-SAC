using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MachineConnectOEM
{
    public partial class OutPut_UserControl : UserControl
    {
        public string TotalCuttingTimeID { get; set; }
        public string TotalCuttingTimeOD { get; set; }
        public string RapidForwardReturn { get; set; }
        public string RapidApproachID { get; set; }
        public string dressingTimeID { get; set; }
        public string DressingOD { get; set; }
        public OutPut_UserControl()
        {
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.UserPaint |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.ResizeRedraw |
                          ControlStyles.ContainerControl |
                          ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.SupportsTransparentBackColor
                          , true);
            InitializeComponent();
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            
           
                txtTotalCuttingTimeID.Text = TotalCuttingTimeID;
                
                txtTotalCuttingTimeFace.Text = TotalCuttingTimeOD;


                txtRapidApproachID.Text = RapidApproachID;

             
                txtRapidApproachFace.Text = RapidForwardReturn;
                txtDressingTimeID.Text = dressingTimeID;
                txtDressingTimeFace.Text = DressingOD;
                if ((txtTotalCuttingTimeID.Text != string.Empty) && (txtRapidApproachID.Text != string.Empty) && (txtDressingTimeID.Text!=string.Empty))
                {
                    double y = Convert.ToDouble(txtTotalCuttingTimeID.Text) + Convert.ToDouble(txtRapidApproachID.Text) + Convert.ToDouble(txtDressingTimeID.Text);
                    y = System.Math.Round(y, 2);
                    txtTotalCycleTimeID.Text = y.ToString();
                }
                if ((txtTotalCuttingTimeFace.Text != string.Empty) && (txtDressingTimeFace.Text!=string.Empty))
                {
                    double z = Convert.ToDouble(txtTotalCuttingTimeFace.Text) + Convert.ToDouble(txtRapidApproachFace.Text) + Convert.ToDouble(txtDressingTimeFace.Text);
                    z = System.Math.Round(z, 2);
                    txtTotalCycleTimeFace.Text = z.ToString();
                }
                if ((txtTotalCycleTimeID.Text != string.Empty) && (txtTotalCycleTimeFace.Text != string.Empty))
                {
                    double w = Convert.ToDouble(txtTotalCycleTimeID.Text) + Convert.ToDouble(txtTotalCycleTimeFace.Text);
                    w = System.Math.Round(w, 2);
                    txtOverallCycleTime.Text = w.ToString();
                }
        }

        //private bool checkvalidity()
        //{
        //    double i;
        //    if (!double.TryParse(txtTotalCuttingTimeID.Text, out i))
        //    {
        //        MessageBox.Show("* Enter only digits");
        //        return false;
        //    }


        //    else if (!double.TryParse(txtTotalCuttingTimeFace.Text, out i))
        //    {
        //        MessageBox.Show("* Enter only digits");
        //        return false;
        //    }
        //    else if (!double.TryParse(txtTotalCycleTimeID.Text, out i))
        //    {
        //        MessageBox.Show("* Enter only digits");
        //        return false;
        //    }
        //    else if (!double.TryParse(txtRapidApproachID.Text, out i))
        //    {
        //        MessageBox.Show("* Enter only digits");
        //        return false;
        //    }
        //    return true;

        //}

        private void OutPut_UserControl_Load(object sender, EventArgs e)
        {
            
            btnCalculate_Click(null, null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtDressingTimeFace.Text = string.Empty;
            txtDressingTimeID.Text = string.Empty;
            txtOverallCycleTime.Text = string.Empty;
            txtRapidApproachFace.Text = string.Empty;
            txtRapidApproachID.Text = string.Empty;
            txtTotalCuttingTimeFace.Text = string.Empty;
            txtTotalCuttingTimeID.Text = string.Empty;
            txtTotalCycleTimeFace.Text = string.Empty;
            txtTotalCycleTimeID.Text = string.Empty;
            
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    txtDressingTimeFace.Text = "3.55";
        //    txtDressingTimeID.Text = "7.33"; 
        //    txtOverallCycleTime.Text = "316.88";
        //    txtRapidApproachFace.Text = "8";
        //    txtRapidApproachID.Text = "10";
        //    txtTotalCuttingTimeFace.Text = "36";
        //    txtTotalCuttingTimeID.Text = "252";
        //    txtTotalCycleTimeFace.Text = "47.55";
        //    txtTotalCycleTimeID.Text = "269.33";
            
        //}
    }
}
