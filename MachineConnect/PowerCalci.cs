using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.IO;

namespace MachineConnectApplication
{
    public partial class PowerCalci : UserControl
    {
        string MachineModel = string.Empty;

        public PowerCalci()
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

        private bool ValidateFormFeilds()
        {
            string msg = string.Empty;

            if (string.IsNullOrEmpty(txtFeedVal.Text))
            {
                CustomDialogBox cmb = new CustomDialogBox("Error Message", "Please Enter feed of the component.");
                cmb.ShowDialog();
                txtFeedVal.Focus();
                return true;
            }

            if (string.IsNullOrEmpty(txtDepthOfCut.Text))
            {
                CustomDialogBox cmb = new CustomDialogBox("Error Message", "Please Enter Cutting Depth of the component.");
                cmb.ShowDialog();
                txtDepthOfCut.Focus();
                return true;
            }

            if (string.IsNullOrEmpty(txtDia.Text))
            {
                CustomDialogBox cmb = new CustomDialogBox("Error Message", "Please Enter Diameter of the component.");
                cmb.ShowDialog();              
                txtDia.Focus();
                return true;
            }

            if (string.IsNullOrEmpty(txtCuttingSpd.Text))
            {
                CustomDialogBox cmb = new CustomDialogBox("Error Message", "Please Enter Cutting Speed of the component.");
                cmb.ShowDialog();
                txtCuttingSpd.Focus();
                return true;
            }

            if (string.IsNullOrEmpty(txtSpecificCuttingForce.Text.Trim()))
            {
                CustomDialogBox cmb = new CustomDialogBox("Error Message", "Please Enter Specific Cutting Force of the component.");
                cmb.ShowDialog();
                txtSpecificCuttingForce.Focus();
                return true;
            }
           
            return false;
        }

        private void btnDefaults_Click(object sender, EventArgs e)
        {
            LoadDefaultValues();

            lblPowerReqStatus.Text = string.Empty;

            lblStandard.Text = "Standard";
            lblCalculated.Text = "Calculated";

            picBoxStatus.Image = null;
            lbl4.BackColor = Color.White;
        }

        private void LoadDefaultValues()
        {
            txtFeedVal.Text = "0.3";
            txtDepthOfCut.Text = "0.5";
            txtDia.Text = "50";
            txtCuttingSpd.Text = "100";
            txtSpecificCuttingForce.Text = "        " + "2500";

            ResetAllLabelText();
        }

        private void ResetAllLabelText()
        {
            lbl1.Text = string.Empty;
            lbl2.Text = string.Empty;
            lbl3.Text = string.Empty;
            lbl4.Text = string.Empty;

            lbl5.Text = string.Empty;
            lbl6.Text = string.Empty;
            lbl7.Text = string.Empty;
            lbl8.Text = string.Empty;

            //lblContinuousPower1.Text = "Continuous Power   @  RPM";
            //lblContinuousPower2.Text = "30  Mins.  Power      @  RPM";

            //lblContinuousTorque1.Text = "Continuous Torque  @  RPM";
            //lblContinuousTorque2.Text = "30  Mins.  Torque     @  RPM";

            //lblStandard1.Text = "Continuous Power   @  RPM";
            //lblStandard2.Text = "30  Mins.  Power      @  RPM";
            //lblStandard3.Text = "Continuous Torque  @  RPM";
            //lblStandard4.Text = "30  Mins.  Torque     @  RPM";

            lblStandardVal1.Text = string.Empty;
            lblStandardVal2.Text = string.Empty;
            lblStandardVal3.Text = string.Empty;
            lblStandardVal4.Text = string.Empty;
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
          if (ValidateFormFeilds()) return;

          PowerCalculatorVals vals = DatabaseAccess.CalculatePowerForMachine(MachineModel, txtFeedVal.Text, txtDepthOfCut.Text, txtDia.Text, txtCuttingSpd.Text, txtSpecificCuttingForce.Text.Trim());
          if (vals != null)
          {
              lblPowerReqStatus.Text = string.Empty;

              lbl1.Text = vals.TangentialForce;
              lbl2.Text = vals.RPM;
              lbl3.Text = vals.Torque;
              lbl4.Text = vals.PowerRequired;

              lbl5.Text = vals.PAC;
              lbl6.Text = vals.PAS;
              lbl7.Text = vals.TAC;
              lbl8.Text = vals.TAS;

              if (Convert.ToDouble(vals.PowerRequired) <  Convert.ToDouble(vals.PAC))//pac
              {
                  picBoxStatus.Image = Image.FromFile(Path.Combine(Settings.APP_PATH, "Images", "Gif", "NetworkOk.png"));
                  lbl4.BackColor = ColorTranslator.FromHtml("#A0D0A0"); //Green    
                  lblPowerReqStatus.Text = "Power Required < " + "Continuous Power";
                  lblPowerReqStatus.ForeColor = Color.Green;
              }

              if (Convert.ToDouble(vals.PowerRequired) > Convert.ToDouble(vals.PAC))//pas
              {
                  picBoxStatus.Image = Image.FromFile(Path.Combine(Settings.APP_PATH, "Images", "Gif", "NetworkNotOk.png"));
                  lbl4.BackColor = ColorTranslator.FromHtml("#FF8080"); //Red
                  lblPowerReqStatus.Text = "Power Required > " + "Continuous Power";
                  lblPowerReqStatus.ForeColor = Color.Red;
              }

              if (Convert.ToDouble(vals.PowerRequired) >= Convert.ToDouble(vals.PAC) && Convert.ToDouble(vals.PowerRequired) <= Convert.ToDouble(vals.PAC))
              {
                  picBoxStatus.Image = Image.FromFile(Path.Combine(Settings.APP_PATH, "Images", "Gif", "NetworkOk.png"));
                  lbl4.BackColor = ColorTranslator.FromHtml("#FFF880"); //Yellow
                  lblPowerReqStatus.Text = " Power Required = Continuous Power";
                  lblPowerReqStatus.ForeColor = Color.Orange;
              }


              //lblStandard1.Text = "Continuous Power   @  " + vals.BaseSpeedOnMotor + " RPM";
              lblStandard2.Text = vals.StMin + "  Mins.  Power";//@ " + vals.StMin + " RPM";
              //lblStandard3.Text = "Continuous Torque  @  " + vals.BaseSpeedOnSpindle + " RPM";
              lblStandard4.Text = vals.StMin + "  Mins.  Torque";//@  " + vals.StMin + " RPM";

              lblStandardVal1.Text = vals.ContPower;
              lblStandardVal2.Text = vals.StMinPower;
              lblStandardVal3.Text = vals.ContTorque;
              lblStandardVal4.Text = vals.StMinTorque;

              lblStandard.Text = "Standard" + "\n @" + vals.BaseSpeedOnSpindle +" RPM";
              lblCalculated.Text = "Calculated" + "\n @" +  vals.RPM + " RPM";



          }
        }

        private void PowerCalci_Load(object sender, EventArgs e)
        {
           MachineModel = DatabaseAccess.GetModelForMachine(HomeScreen.selectedMachine);
           DataTable dt = DatabaseAccess.GetAllPowerCalciConstant();
           dataGridView.AutoGenerateColumns = false;
           if (dt != null && dt.Rows.Count > 0)
           {
               dataGridView.DataSource = dt;
           }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            int rowindex = dataGridView.CurrentCell.RowIndex;
            int columnindex = dataGridView.CurrentCell.ColumnIndex;

           txtSpecificCuttingForce.Text = "        "+dataGridView.Rows[rowindex].Cells[1].Value.ToString();
           tblSpecificCuttingForce.Visible = false;
        }

        private void btnGetSpecificCuttingForce_Click(object sender, EventArgs e)
        {
            if (tblSpecificCuttingForce.Visible)
            {
                tblSpecificCuttingForce.Visible = false;
            }
            else
            {
                tblSpecificCuttingForce.Visible = true;
            }
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView.CurrentCell == null) return;

            int rowindex = dataGridView.CurrentCell.RowIndex;
            int columnindex = dataGridView.CurrentCell.ColumnIndex;

            txtSpecificCuttingForce.Text = "        " + dataGridView.Rows[rowindex].Cells[1].Value.ToString();
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            txtFeedVal.Text = string.Empty;
            txtDepthOfCut.Text = string.Empty;
            txtDia.Text = string.Empty;
            txtCuttingSpd.Text = string.Empty;
            txtSpecificCuttingForce.Text = string.Empty;

            lbl1.Text = string.Empty;
            lbl2.Text = string.Empty;
            lbl3.Text = string.Empty;
            lbl4.Text = string.Empty;

            lbl5.Text = string.Empty;
            lbl6.Text = string.Empty;
            lbl7.Text = string.Empty;
            lbl8.Text = string.Empty;

            lblStandardVal1.Text = string.Empty;
            lblStandardVal2.Text = string.Empty;
            lblStandardVal3.Text = string.Empty;
            lblStandardVal4.Text = string.Empty;

            lblPowerReqStatus.Text = string.Empty;

            lblStandard.Text = "Standard" ;
            lblCalculated.Text = "Calculated" ;

            picBoxStatus.Image = null;
            lbl4.BackColor = Color.White;
        }
       
    }
}
