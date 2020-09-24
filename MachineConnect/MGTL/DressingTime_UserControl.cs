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
    public partial class DressingTime_UserControl : UserControl
    {
        public string DressingTime
        {
            get
            {
                return txtDressingTimeComponentID.Text;
            }
        }
        public string DressingTimeOD
        {
            get
            {
                return txtTotalGrinding.Text;
            }
        }
        public string TotalDressingTimeOd
        {
            get
            {
                return txtDressingTimeComponent.Text;
            }
        }
        public string RapidApproach
        {
            get
            {
                return TxtRapidApproachID.Text;
            }
        }
        public string RapidForwardreturnOd
        { 
        get{
            return txtRapidApproach.Text;
        
        }
        }

        public DressingTime_UserControl()
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
            if (checkValidValue())
            {
                if (IsDigit())
                {
                    if ((txtWidth.Text != string.Empty) && (txtDressingtraverserate.Text != string.Empty)  && (txtRapidApproach.Text != string.Empty)  && (txtNoOFCOmponent.Text != string.Empty))
                    {
                        double x = (Convert.ToDouble(txtWidth.Text) + 20) / Convert.ToDouble(txtDressingtraverserate.Text);
                        x = System.Math.Round(x, 2);
                        txtDressingtime.Text = x.ToString();

                        double y = Convert.ToDouble(txtDressingtime.Text) * 60;
                        y = System.Math.Round(y, 2);
                        txtDressigCycletime.Text = y.ToString();

                        double z = Convert.ToDouble(txtDressigCycletime.Text) + Convert.ToDouble(txtRapidApproach.Text);
                        z = System.Math.Round(z, 3);
                        txtTotalGrinding.Text = z.ToString();

                        double w = Convert.ToDouble(txtTotalGrinding.Text) / Convert.ToDouble(txtNoOFCOmponent.Text);
                        w = System.Math.Round(w, 2);
                        txtDressingTimeComponent.Text = w.ToString();
                    }
                }
            }
            if (checkValidValueID())
            {
                if (IsDigit1())
                {
                    if ((txtWidthId.Text != string.Empty) && (txtDressingTraverserateId.Text != string.Empty) && (TxtRapidApproachID.Text != string.Empty) && (txtNoOfComponentInOneDressingID.Text != string.Empty))
                    {

                        double x = (Convert.ToDouble(txtWidthId.Text) + 10) / Convert.ToDouble(txtDressingTraverserateId.Text);
                        x = System.Math.Round(x, 2);
                        txtDressingTimeID.Text = x.ToString();
                        double y = Convert.ToDouble(txtDressingTimeID.Text) * 60;
                        y = System.Math.Round(y, 2);
                        txtDressingCycleTimeID.Text = y.ToString();
                        double z = Convert.ToDouble(txtDressingCycleTimeID.Text) + Convert.ToDouble(TxtRapidApproachID.Text);
                        z = System.Math.Round(z, 3);
                        txtTotalGrindingDressingID.Text = z.ToString();
                        double w = Convert.ToDouble(txtTotalGrindingDressingID.Text) / Convert.ToDouble(txtNoOfComponentInOneDressingID.Text);
                        w = System.Math.Round(w, 2);
                        txtDressingTimeComponentID.Text = w.ToString();
                    }
                }

            }
            
        }

        private bool IsDigit()
        {
            double i;
            if (!double.TryParse(txtWidth.Text, out i))
            {
                MessageBox.Show("* Enter only digits");
                return false;
            }
            else if (!double.TryParse(txtDressingtraverserate.Text, out i))
            {
                MessageBox.Show("* Enter only digits");
                return false;
            }
            else if (!double.TryParse(txtRapidApproach.Text, out i))
            {
                MessageBox.Show("* Enter only digits");
                return false;
            }
            else if (!double.TryParse(txtNoOFCOmponent.Text, out i))
            {
                MessageBox.Show("* Enter only digits");
                return false;
            }
           
            return true;
        }

        private bool checkValidValue()
        {
            if (txtWidth.Text == string.Empty)
                return false;
            else if (txtDressingtraverserate.Text == string.Empty)
                return false;
      
            else if (txtRapidApproach.Text == string.Empty)
                return false;
   
            else if (txtNoOFCOmponent.Text == string.Empty)
                return false;
            else return true;
        }

      

       
        private bool IsDigit1()
        {
            int i;
            if (!int.TryParse(txtWidthId.Text, out i))
            {
                MessageBox.Show("* Enter only digits");
                return false;
            }
            else if (!int.TryParse(txtDressingTraverserateId.Text, out i))
            {
                MessageBox.Show("* Enter only digits");
                return false;
            }
            else if (!int.TryParse(TxtRapidApproachID.Text, out i))
            {
                MessageBox.Show("* Enter only digits");
                return false;
            }
            else if (!int.TryParse(txtNoOfComponentInOneDressingID.Text, out i))
            {
               
                MessageBox.Show("* Enter only digits");
                return false;
            }

            return true;   
        }

        private bool checkValidValueID()
        {
            if (txtWidthId.Text == string.Empty)
                return false;
            else if (txtDressingTraverserateId.Text == string.Empty)
                return false;

            else if (TxtRapidApproachID.Text == string.Empty)
                return false;

            else if (txtNoOfComponentInOneDressingID.Text == string.Empty)
                return false;
            else return true;
        }

        private void DressingTime_UserControl_Load(object sender, EventArgs e)
        {
           button2_Click(null, null);
            btnCalculate_Click(null, null);
        }

        private void txtDressingTimeComponent_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtWidthId.Text = string.Empty;
            txtDressingTraverserateId.Text = string.Empty;
            TxtRapidApproachID.Text = string.Empty;
            txtNoOfComponentInOneDressingID.Text = string.Empty;
            txtNoOFCOmponent.Text = string.Empty;

            txtRapidApproach.Text = string.Empty;
            txtDressingtraverserate.Text = string.Empty;
            txtWidth.Text = string.Empty;
            txtDressigCycletime.Text = string.Empty;
            txtDressingCycleTimeID.Text = string.Empty;
            txtDressingtime.Text = string.Empty;
            txtDressingTimeID.Text = string.Empty;
            txtTotalGrinding.Text = string.Empty;
            txtTotalGrindingDressingID.Text = string.Empty;
            txtDressingTimeComponentID.Text = string.Empty;
            txtDressingTimeComponent.Text = string.Empty;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtWidthId.Text = "30";
            txtDressingTraverserateId.Text = "200";
            TxtRapidApproachID.Text = "10";
            txtNoOfComponentInOneDressingID.Text = "3";
            txtNoOFCOmponent.Text ="40";

            txtRapidApproach.Text = "10";
            txtDressingtraverserate.Text = "100";
            txtWidth.Text = "200";
            //txtDressigCycletime.Text = "132";
            //txtDressingCycleTimeID.Text ="12";
            //txtDressingtime.Text = "2.2";
          //  txtDressingTimeID.Text = "0.2";
            //txtTotalGrinding.Text = "142";
            //txtTotalGrindingDressingID.Text = "22";
            //txtDressingTimeComponentID.Text = "7.33";
            //txtDressingTimeComponent.Text = "3.55";
        }

        //private void buttonCalculate1_Click(object sender, EventArgs e)
        //{
        //    if (checkValidValueID())
        //    {
        //        if (IsDigit1())
        //        {
        //            double x = (Convert.ToDouble(txtWidthId.Text) + 10) / Convert.ToDouble(txtDressingTraverserateId.Text);
        //            x = System.Math.Round(x, 2);
        //            txtDressingTimeID.Text = x.ToString();
        //            double y = Convert.ToDouble(txtDressingTimeID.Text) * 60;
        //            y = System.Math.Round(y, 2);
        //            txtDressingCycleTimeID.Text = y.ToString();
        //            double z = Convert.ToDouble(txtDressingCycleTimeID.Text) + Convert.ToDouble(TxtRapidApproachID.Text);
        //            z = System.Math.Round(z, 3);
        //            txtTotalGrindingDressingID.Text = z.ToString();
        //            double w = Convert.ToDouble(txtTotalGrindingDressingID.Text) / Convert.ToDouble(txtNoOfComponentInOneDressingID.Text);
        //            w = System.Math.Round(w, 2);
        //            txtDressingTimeComponentID.Text = w.ToString();
        //        }

        //    }
        //}

      

     

        








    
    }
}
