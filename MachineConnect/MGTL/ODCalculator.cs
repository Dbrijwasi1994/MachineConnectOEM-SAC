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
    public partial class ODCalculator : UserControl
    {
        public string CuttingTimeOd
        {
            get {
                return txtCuttingTime.Text;
            }
            
        }
        public string RapidForwardReturnText
        {get;set;
            //get {
            //    return txtRapidForwardReturn.Text;
            //}
        }
        public string DressingOD 
        {
            get {
                return txtDressing.Text;
            }
        }
        public string TotalDressingTime
        {
            get;
            set;
           
        }
        public ODCalculator()
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
           
            if (CheckValidString())
            {
                if (CheckDigit())
                {
                    if ((txtCuttingSpeedVw.Text != string.Empty) && (txtWheelDia.Text != string.Empty) && (txtCuttingSpeedVj.Text != string.Empty) && (txtWheelDia.Text != string.Empty) && (txtStock.Text != string.Empty) && (txtCoarseMrr.Text != string.Empty)  && (txtFineMrr.Text != string.Empty) && (txtFineMrr.Text != string.Empty) && (txtWidth2.Text != string.Empty))
                    {
                        txtRapidForwardReturn.Text = RapidForwardReturnText;
                        txtDressingTime.Text = TotalDressingTime;

                        double x = (Convert.ToDouble(txtCuttingSpeedVw.Text) * 60000) / (Convert.ToDouble(txtWheelDia.Text) * 3.14);
                        Int32 rpmx = Convert.ToInt32(x);
                        txtRpmWheelHead.Text = rpmx.ToString();

                        double y = (Convert.ToDouble(txtCuttingSpeedVj.Text) * 1000) / (Convert.ToDouble(txtWorkHead.Text) * 3.14);
                        Int32 rpm = Convert.ToInt32(y);
                        txtRPMWorkHead.Text = rpm.ToString();

                        double z = (Convert.ToDouble(txtStock.Text) - 0.05) / (Convert.ToDouble(txtCoarseMrr.Text) * (Convert.ToDouble(txtRPMWorkHead.Text) / 60));
                        z = System.Math.Round(z, 2);
                        txtCoarseCutting.Text = z.ToString();
                        double w = (0.05 / (Convert.ToDouble(txtFineMrr.Text) * (Convert.ToDouble(txtRPMWorkHead.Text) / 60)));
                        w = System.Math.Round(w, 2);
                        txtFineCutting.Text = w.ToString();
                        if (RapidForwardReturnText != string.Empty)
                        {
                            double a = Convert.ToDouble(txtCoarseCutting.Text) + Convert.ToDouble(txtFineCutting.Text) + Convert.ToDouble(txtSparkOut.Text) + Convert.ToDouble(txtRapidForwardReturn.Text) + Convert.ToDouble(txtGuageRetraction.Text);
                            a = System.Math.Round(a, 2);
                            txtCycleTime.Text = a.ToString();
                        }
                        if (txtDressingTime.Text != string.Empty)
                        {
                            double b = Convert.ToDouble(txtDressingTime.Text) / Convert.ToDouble(txtDressingFrequency.Text);
                            b = System.Math.Round(b, 3);
                            txtDressing.Text = b.ToString();
                        }
                        if ((txtCycleTime.Text != string.Empty) && (txtDressing.Text != string.Empty))
                        {
                            double c = Convert.ToDouble(txtCycleTime.Text) + Convert.ToDouble(txtDressing.Text) + Convert.ToDouble(txtLoadingUnloading.Text);
                            c = System.Math.Round(c, 3);
                            txtFloor2floorTime.Text = c.ToString();
                        }
                        double d = Convert.ToDouble(txtCoarseCutting.Text) * Convert.ToDouble(txtWidth1.Text);
                        d = System.Math.Round(d, 2);
                        lblTime1.Text = d.ToString();

                        double f = Convert.ToDouble(txtFineCutting.Text) * Convert.ToDouble(txtWidth2.Text);
                        f = System.Math.Round(f, 2);
                        lbltime2.Text = f.ToString();
                        double g = Convert.ToDouble(txtRPMWorkHead.Text) / 60;
                        Int32 h = Convert.ToInt32(g);
                        txtWorkHeadRPS.Text = h.ToString();
                        txtCuttingTime.Text = (z + w + Convert.ToDouble(txtSparkOut.Text)).ToString();
                        txtFaceCutting.Text = "0";
                    }
                }
            }
        }


        

        private bool CheckDigit()
        {
           // button1_Click(null, null);
            double i;
            //if (!double.TryParse(txtRapidForwardReturn.Text, out i))
            //{
            //    MessageBox.Show("* Enter only digits");
            //    return false;
            //}


             if (!double.TryParse(txtSparkOut.Text, out i))
            {
                MessageBox.Show("* Enter only digits");
                return false;
            }
            else if (!double.TryParse(txtStock.Text, out i))
            {
                MessageBox.Show("* Enter only digits");
                return false;
            }
            else if (!double.TryParse(txtWheelDia.Text, out i))
            {
                MessageBox.Show("* Enter only digits");
                return false;
            }
            else if (!double.TryParse(txtWidth1.Text, out i))
            {
                MessageBox.Show("* Enter only digits");
                return false;
            }
            else if (!double.TryParse(txtWidth2.Text, out i))
            {
                MessageBox.Show("* Enter only digits");
                return false;
            }
            else if (!double.TryParse(txtWorkHead.Text, out i))
            {
                MessageBox.Show("* Enter only digits");
                return false;
            }
            //else if (!double.TryParse(txtCoarseCutting.Text, out i))
            //{
            //    MessageBox.Show("* Enter only digits");
            //    return false;
            //}
            else if (!double.TryParse(txtCoarseMrr.Text, out i))
            {
                MessageBox.Show("* Enter only digits");
                return false;
            }
            else if (!double.TryParse(txtCuttingSpeedVj.Text, out i))
            {
                MessageBox.Show("* Enter only digits");
                return false;
            }
            else if (!double.TryParse(txtCuttingSpeedVw.Text, out i))
            {
                MessageBox.Show("* Enter only digits");
                return false;
            }
            //else if (!double.TryParse(txtCycleTime.Text, out i))
            //{
            //    MessageBox.Show("* Enter only digits");
            //    return false;
            //}
            //else if (!double.TryParse(txtCuttingTime.Text, out i))
            //{
            //    MessageBox.Show("* Enter only digits");
            //    return false;
            //}
            //else if (!double.TryParse(txtDressing.Text, out i))
            //{
            //    MessageBox.Show("* Enter only digits");
            //    return false;
            //}
            else if (!double.TryParse(txtDressingFrequency.Text, out i))
            {
                MessageBox.Show("* Enter only digits");
                return false;
            }

            //else if (!double.TryParse(txtFineCutting.Text, out i))
            //{
            //    MessageBox.Show("* Enter only digits");
            //    return false;
            //}

            else if (!double.TryParse(txtFineMrr.Text, out i))
            {
                MessageBox.Show("* Enter only digits");
                return false;
            }
            else if (!double.TryParse(txtGuageRetraction.Text, out i))
            {
                MessageBox.Show("* Enter only digits");
                return false;
            }
            else if (!double.TryParse(txtGuageRetraction.Text, out i))
            {
                MessageBox.Show("* Enter only digits");
                return false;
            }

            return true;
        }

        private bool CheckValidString()
        {
            //if (txtRapidForwardReturn.Text == string.Empty) return false;
            if (txtDressingTime.Text == string.Empty) return false;
            //if (txtRapidForwardReturn.Text == string.Empty)
            //{
               
            //    return false;
            //}
           
               

               else if (txtSparkOut.Text == string.Empty)
               {

                   return false;
               }
               else if (txtStock.Text == string.Empty)
               {

                   return false;
               }
               else if (txtWheelDia.Text == string.Empty)
               {

                   return false;
               }
               else if (txtWidth1.Text == string.Empty)
               {

                   return false;
               }
               else if (txtWidth2.Text == string.Empty)
               {

                   return false;
               }
               else if (txtWorkHead.Text == string.Empty)
               {

                   return false;
               }
               //else if (txtCoarseCutting.Text == string.Empty)
               //{

               //    return false;
               //}
               else if (txtCoarseMrr.Text == string.Empty)
               {

                   return false;
               }
               else if (txtCuttingSpeedVj.Text == string.Empty)
               {

                   return false;
               }
               else if (txtCuttingSpeedVw.Text == string.Empty)
               {

                   return false;
               }
               //else if (txtCuttingTime.Text == string.Empty)
               //{

               //    return false;
               //}
               //else if (txtCycleTime.Text == string.Empty)
               //{

               //    return false;
               //}
               //else if (txtDressing.Text == string.Empty)
               //{

               //    return false;
               //}
               else if (txtDressingFrequency.Text == string.Empty)
               {

                   return false;
               }
               else if (txtDressingFrequency.Text == string.Empty)
               {

                   return false;
               }

               //else if (txtFineCutting.Text == string.Empty)
               //{

               //    return false;
               //}
               else if (txtFineMrr.Text == string.Empty)
               {

                   return false;
               }
               //else if (txtFloor2floorTime.Text == string.Empty)
               //{

               //    return false;
               //}
               else if (txtGuageRetraction.Text == string.Empty)
               {

                   return false;
               }
               else if (txtGuageRetraction.Text == string.Empty)
               {

                   return false;
               } 

               else return true;
        }

        private void ODCalculator_Load(object sender, EventArgs e)
        {
            button1_Click(null, null);
            btnCalculate_Click(null, null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtRapidForwardReturn.Text = string.Empty;
            txtGuageRetraction.Text = string.Empty;
            txtFloor2floorTime.Text = string.Empty;
            txtFineMrr.Text = string.Empty;
            txtFineCutting.Text = string.Empty;

            txtDressingFrequency.Text = string.Empty;
            txtDressing.Text = string.Empty;
            txtWidth2.Text = string.Empty;
            txtWidth1.Text = string.Empty;

            txtCycleTime.Text = string.Empty;
            txtCuttingTime.Text = string.Empty;
            txtCuttingSpeedVj.Text = string.Empty;
            txtCoarseMrr.Text = string.Empty;

            txtCoarseCutting.Text = string.Empty;
            txtWorkHead.Text = string.Empty;
            txtStock.Text = string.Empty;
            txtWheelDia.Text = string.Empty;

            txtSparkOut.Text = string.Empty;

            txtFlagging.Text = string.Empty;
            txtDressingTime.Text = string.Empty;
            txtLoadingUnloading.Text = string.Empty;
            txtWorkHead.Text = string.Empty;
            txtRpmWheelHead.Text = string.Empty;
            txtRPMWorkHead.Text = string.Empty;
            txtWorkHeadRPS.Text = string.Empty;
            txtFaceCutting.Text = string.Empty;
            lblTime1.Text = string.Empty;
            lbltime2.Text = string.Empty;
            txtCuttingSpeedVw.Text = string.Empty;

        }

        private void button1_Click(object sender, EventArgs e)
        {
           // txtRapidForwardReturn.Text = "8";
            txtGuageRetraction.Text = "1";
          //  txtFloor2floorTime.Text ="63.5";
            txtFineMrr.Text = "0.003";
         //   txtFineCutting.Text = "5.99";

            txtDressingFrequency.Text = "40";
            //txtDressing.Text = "3.55";
            txtWidth2.Text = "1";
            txtWidth1.Text = "1";

           // txtCycleTime.Text = "44.95";
           // txtCuttingTime.Text = "36";
            txtCuttingSpeedVj.Text = "20";
            txtCoarseMrr.Text ="0.006";

          //  txtCoarseCutting.Text ="20.96";
            
            txtStock.Text = "0.4";
            txtWheelDia.Text = "610";

            txtSparkOut.Text = "9";

            txtFlagging.Text = "0";
            txtDressingTime.Text = "142";
            txtLoadingUnloading.Text = "15";
            txtWorkHead.Text ="38.176";
         //  txtRpmWheelHead.Text ="1410";
         //   txtRPMWorkHead.Text = "167";
           // txtWorkHeadRPS.Text = "3";
           // txtFaceCutting.Text = "0";
          //  lblTime1.Text = "20.96";
          //  lbltime2.Text ="5.99";
            txtCuttingSpeedVw.Text ="45";
        }

       
    }
}
