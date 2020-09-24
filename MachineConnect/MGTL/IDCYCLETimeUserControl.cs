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
    public partial class IDCYCLETimeUserControl : UserControl
    {
        public string dressingTime { get; set; }
        public string Rappidapproach { get; set; }
        public string CycleTime
        {
            get
            {
                return txtTotalCuttingTimeOutPut.Text;
            }
        }
        public IDCYCLETimeUserControl()
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
        private void btnCalculateIDCYCle_Click(object sender, EventArgs e)
        {
            if (checkstring())
            
            {
                if (checkValidInput())
                {
                    txtDressingTimeOutput.Text = dressingTime;
                    double x = (1000 * 25) / (3.142 * Convert.ToDouble(txtCompDia.Text));
                    Int32 rpmx = Convert.ToInt32(x);
                    txtRpm.Text = rpmx.ToString();

                    double y = (60 * (Convert.ToDouble(TXTLengthtobeground.Text) - Convert.ToDouble(txtWheelwidth.Text))) / (Convert.ToDouble(txtTableTraverse.Text) )+ 0.5;
                    y = System.Math.Round(y, 3); 
                    txtTimePerStroke.Text = y.ToString();
                   
                    double z = Convert.ToDouble(TXTSlideMovementAirgap.Text) / Convert.ToDouble(TXTInfeedAirGap.Text);
                    z = System.Math.Round(z, 3); 
                    txtNosOfStrokAirGap.Text = z.ToString();
                    
                    double w = Convert.ToDouble(TXTStockRemovalRough.Text) / Convert.ToDouble(txtInfeedRough.Text);
                    w = System.Math.Round(w, 3); 
                    txtNoOfStrokeCoarse.Text = w.ToString();
                    
                    double s = Convert.ToDouble(TXTstockRemovalFine.Text) / Convert.ToDouble(TXTInfeedFine.Text);
                    s = System.Math.Round(s, 3); 
                    txtNoOfStrokeFine.Text = s.ToString();
                    
                    double p = Convert.ToDouble(TXTstockRemovalSparkOut.Text) / Convert.ToDouble(TXTInfeedSuperFinish.Text);
                    p = System.Math.Round(p, 3); 
                    txtNoOfStrokeSuperFine.Text = p.ToString();
                    
                    double d = Convert.ToDouble(txtNosOfStrokAirGap.Text) + Convert.ToDouble(txtNoOfStrokeCoarse.Text) + Convert.ToDouble(txtNoOfStrokeFine.Text) + Convert.ToDouble(txtNoOfStrokeSuperFine.Text) + Convert.ToDouble(lblNosOfStrokeSparkOut.Text);
                    d = System.Math.Round(d, 3); 
                    txtTotalNoOfStroke.Text = d.ToString();
                    
                    double f = Convert.ToDouble(txtNosOfStrokAirGap.Text) * Convert.ToDouble(txtTimePerStroke.Text);
                    f = System.Math.Round(f, 2); 
                    txtTimeAIrGap.Text = f.ToString();
                    
                    double g = Convert.ToDouble(txtNoOfStrokeCoarse.Text) * Convert.ToDouble(txtTimePerStroke.Text);
                    g = System.Math.Round(g, 2); 
                    txtTimeCoarse.Text = g.ToString();
                    
                    double h = Convert.ToDouble(txtNoOfStrokeFine.Text) * Convert.ToDouble(txtTimePerStroke.Text);
                    h = System.Math.Round(h, 2); 
                    txtTineFine.Text = h.ToString();
                    
                    double i = Convert.ToDouble(txtNoOfStrokeSuperFine.Text) * Convert.ToDouble(txtTimePerStroke.Text);
                    i = System.Math.Round(i, 2); 
                    txtTimeSuperFinish.Text = i.ToString();
                    
                    double j = Convert.ToDouble(lblNosOfStrokeSparkOut.Text) * Convert.ToDouble(txtTimePerStroke.Text);
                    j = System.Math.Round(j, 2); 
                    txtTimeSparkOut.Text = j.ToString();

                    double k = (Convert.ToDouble(txtTotalNoOfStroke.Text) * Convert.ToDouble(txtTimePerStroke.Text)) + Convert.ToDouble(lblTimeIdle.Text) + Convert.ToDouble(lblTimeDressing.Text);
                    k = System.Math.Round(k, 2);
                    txtTotalCuttingTimeOutPut.Text = k.ToString();
                    if (txtWheelSurfaceSpeed.Text != string.Empty)
                    {
                        double l = (60000 * Convert.ToDouble(txtWheelSurfaceSpeed.Text)) / (Math.PI * Convert.ToDouble(TxtWheelDiaMAx.Text));
                        Int64 rpm = Convert.ToInt64(l);
                        txtWheelRpmMaxDia.Text = rpm.ToString();

                        double m = (60000 * Convert.ToDouble(txtWheelSurfaceSpeed.Text)) / (Math.PI * Convert.ToDouble(txtWheelDiaMin.Text));
                        Int64 rpmm = Convert.ToInt64(m);
                        txtWheelRpmMinDia.Text = rpmm.ToString();
                    }
                   // txtTotalCuttingTimeOutPut.Text = txtTotalCycleTime.Text;

                    txtRapidApproachReturnOutput.Text = Rappidapproach;
                   
                    if (txtDressingTimeOutput.Text != string.Empty)
                    {
                        double n = Convert.ToDouble(txtTotalCuttingTimeOutPut.Text) + Convert.ToDouble(txtRapidApproachReturnOutput.Text) + Convert.ToDouble(txtDressingTimeOutput.Text);
                        n = System.Math.Round(n, 2);
                        txtTotalCycleTimeOutPut.Text = n.ToString();
                    }
                    //double o = 40 + Convert.ToDouble(txtTotalCycleTimeOutPut.Text);
                    //o = System.Math.Round(o, 2); 
                    //txtTotalCycleTimeIDFAceGrinding.Text = o.ToString();
                    
                    //double q = 60 + Convert.ToDouble(txtTotalCuttingTimeOutPut.Text);
                    //q = System.Math.Round(q, 2); 
                    //TxtTotalCuttingTimeIDFaceGrinding.Text = q.ToString();

                    
                }
            }

        }

        private bool checkstring()
        {

            
                   
          

            
            //if(txtNoOfStrokeSuperFine.Text == string.Empty)return false;
            //if(txtTotalNoOfStroke.Text == string.Empty)return false;
            //if(txtTimeAIrGap.Text == string.Empty)return false;

            //if(txtTimeCoarse.Text == string.Empty)return false;
            //if(txtTineFine.Text == string.Empty)return false;
            //if(txtTimeSuperFinish.Text == string.Empty)return false;
            //if(txtTimeSparkOut.Text == string.Empty)return false;

            //if(txtTotalCuttingTimeOutPut.Text == string.Empty)return false;
            //if(txtWheelRpmMaxDia.Text == string.Empty)return false;
            //if(txtWheelRpmMinDia.Text == string.Empty)return false;
            //if(txtRapidApproachReturnOutput.Text == string.Empty)return false;
            //if (txtWheelSurfaceSpeed.Text == string.Empty) return false;
                //if(txtTotalCycleTimeOutPut.Text == string.Empty)return false;
            if(TXTSlideMovementAirgap.Text == string.Empty)return false;
            if(TXTStockRemovalRough.Text == string.Empty)return false;
            if(TXTstockRemovalFine.Text == string.Empty)return false;
            if(TXTStockRemovalRough.Text == string.Empty)return false;
            if(TXTstockRemovalSparkOut.Text == string.Empty)return false;
            //if(txtWheelSurfaceSpeed.Text == string.Empty)return false;
            if(TXTInfeedAirGap.Text == string.Empty)return false;
            if(TXTInfeedFine.Text == string.Empty)return false;
            if(lblNosOfStrokeSparkOut.Text == string.Empty)return false;
            if(txtCompDia.Text == string.Empty)return false;
            if(txtWheelwidth.Text ==string.Empty)return false;
            if(txtWheelDiaMin.Text == string.Empty)return false;
            if(TxtWheelDiaMAx.Text == string.Empty)return false;
            if(TXTLengthtobeground.Text == string.Empty)return false;
            if(lblTimeIdle.Text == string.Empty)return false;
            if(lblTimeDressing.Text == string.Empty)return false;
            if(txtTableTraverse.Text == string.Empty)return false;
            if(txtInfeedRough.Text == string.Empty)return false;
            return true;
        }

        private bool checkValidInput()
        {
            double i;
            if (!double.TryParse(txtCompDia.Text, out i))
            {
                MessageBox.Show("* Enter only digits"); 
               
                return false;
            }
            else if (!double.TryParse(txtWheelwidth.Text, out i))
            {
                  MessageBox.Show( "* Enter only digits"); 
                return false;
            }
            else if (!double.TryParse(TXTstockRemovalSparkOut.Text, out i))
            {
                MessageBox.Show("* Enter only digits");
                return false;
            }
            else if (!double.TryParse(TXTstockRemovalFine.Text, out i))
            {
                MessageBox.Show("* Enter only digits");
                return false;
            }
            else if (!double.TryParse(TXTStockRemovalRough.Text, out i))
            {
                MessageBox.Show("* Enter only digits");
                return false;
            }
            else if (!double.TryParse(TXTInfeedAirGap.Text, out i))
            {
                MessageBox.Show("* Enter only digits");
                return false;
            }
            else if (!double.TryParse(TXTInfeedFine.Text, out i))
            {
                MessageBox.Show("* Enter only digits");
                return false;
            }
            else if (!double.TryParse(lblNosOfStrokeSparkOut.Text, out i))
            {
                MessageBox.Show("* Enter only digits");
                return false;
            }

            else if (!double.TryParse(txtWheelDiaMin.Text, out i))
            {
                MessageBox.Show("* Enter only digits");
                return false;
            }
            else if (!double.TryParse(TxtWheelDiaMAx.Text, out i))
            {
                MessageBox.Show("* Enter only digits");
                return false;
            }
            else if (!double.TryParse(TXTLengthtobeground.Text, out i))
            {
                MessageBox.Show("* Enter only digits");
                return false;
            }
            else if (!double.TryParse(lblTimeIdle.Text, out i))
            {
                MessageBox.Show("* Enter only digits");
                return false;
            }
            else if (!double.TryParse(lblTimeDressing.Text, out i))
            {
                MessageBox.Show("* Enter only digits");
                return false;
            }

            else if (!double.TryParse(txtTableTraverse.Text, out i))
            {
                MessageBox.Show("* Enter only digits");
                return false;
            }
            else if (!double.TryParse(txtInfeedRough.Text, out i))
            {
                MessageBox.Show("* Enter only digits");
                return false;
            }

            return true;   
        
        }

        private void IDCYCLETimeUserControl_Load(object sender, EventArgs e)
        {
            button1_Click(null, null);
            btnCalculateIDCYCle_Click(null, null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TXTInfeedSuperFinish.Text = string.Empty;
            txtDressingTimeOutput.Text = string.Empty;
            txtRpm.Text = string.Empty;
            txtTimePerStroke.Text = string.Empty;
            txtNosOfStrokAirGap.Text = string.Empty;
            txtNoOfStrokeCoarse.Text = string.Empty;

            txtNoOfStrokeFine.Text = string.Empty;
            txtNoOfStrokeSuperFine.Text = string.Empty;
            txtTotalNoOfStroke.Text = string.Empty;
            txtTimeAIrGap.Text = string.Empty;

            txtTimeCoarse.Text = string.Empty;
            txtTineFine.Text = string.Empty;
            txtTimeSuperFinish.Text = string.Empty;
            txtTimeSparkOut.Text = string.Empty;

            txtTotalCuttingTimeOutPut.Text = string.Empty;
            txtWheelRpmMaxDia.Text = string.Empty;
            txtWheelRpmMinDia.Text = string.Empty;
            txtRapidApproachReturnOutput.Text = string.Empty;
            txtWheelSurfaceSpeed.Text = string.Empty;
            txtTotalCycleTimeOutPut.Text = string.Empty;
            TXTSlideMovementAirgap.Text = string.Empty;
            TXTStockRemovalRough.Text = string.Empty;
            TXTstockRemovalFine.Text = string.Empty;
            TXTStockRemovalRough.Text = string.Empty;
            TXTstockRemovalSparkOut.Text= string.Empty;
            txtWheelSurfaceSpeed.Text = string.Empty;
            TXTInfeedAirGap.Text = string.Empty;
            TXTInfeedFine.Text = string.Empty;
            lblNosOfStrokeSparkOut.Text = string.Empty;
            txtCompDia.Text = string.Empty;
            txtWheelwidth.Text = string.Empty;
            txtWheelDiaMin.Text = string.Empty;
            TxtWheelDiaMAx.Text = string.Empty;
            TXTLengthtobeground.Text = string.Empty;
            lblTimeIdle.Text = string.Empty;
            lblTimeDressing.Text = string.Empty;
            txtTableTraverse.Text = string.Empty;
            txtInfeedRough.Text = string.Empty;
           

          
        }

        private void button1_Click(object sender, EventArgs e)
        {
           // txtDressingTimeOutput.Text = "7.33";
            //txtRpm.Text = "136"; ;
         //   txtTimePerStroke.Text = "3.5"; ;
        //    txtNosOfStrokAirGap.Text = "4"; 
          //  txtNoOfStrokeCoarse.Text = "28.571";

       //     txtNoOfStrokeFine.Text = "10"; 
          //  txtNoOfStrokeSuperFine.Text = "10"; ;
           // txtTotalNoOfStroke.Text = "60.571";
        //    txtTimeAIrGap.Text = "14";

            //txtTimeCoarse.Text = "100";
         //   txtTineFine.Text = "35";
           // txtTimeSuperFinish.Text = "35";
           // txtTimeSparkOut.Text = "28";

         //   txtTotalCuttingTimeOutPut.Text = "252";
         //   txtWheelRpmMaxDia.Text = "13890";
         //   txtWheelRpmMinDia.Text = "21827";
         //   txtRapidApproachReturnOutput.Text = "10";

           // txtTotalCycleTimeOutPut.Text = "40";
            TXTSlideMovementAirgap.Text = "0.1";
            TXTStockRemovalRough.Text = "0.2";
            TXTstockRemovalFine.Text = "0.03";
            TXTstockRemovalSparkOut.Text = "0.01";
            TXTInfeedSuperFinish.Text = "0.001";
            txtWheelSurfaceSpeed.Text = "40";
            TXTInfeedAirGap.Text = "0.025";
            TXTInfeedFine.Text = "0.003";
            lblNosOfStrokeSparkOut.Text = "8";
            txtCompDia.Text = "58.51";
            txtWheelwidth.Text = "50";
            txtWheelDiaMin.Text= "35";
            TxtWheelDiaMAx.Text = "55";
            TXTLengthtobeground.Text = "125";
            lblTimeIdle.Text = "5";
            lblTimeDressing.Text = "35";
            txtTableTraverse.Text ="1500";
            txtInfeedRough.Text = "0.007";
           // txtTotalCycleTimeOutPut.Text = "269.33";

        }

       

        
     

      




















       
    }
}
