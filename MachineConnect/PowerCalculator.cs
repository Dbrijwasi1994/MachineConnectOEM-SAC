using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace MachineConnectApplication
{
    public partial class PowerCalculator : UserControl
    {
        string shiftStartTime = string.Empty;
        string shiftEndTime = string.Empty;
        string shiftName = string.Empty;
        GenParameter globalPara = new GenParameter();
        static string machineId = string.Empty;

        public PowerCalculator()
        {
            InitializeComponent();
            globalPara.MachineID = HomeScreen.selectedMachine;
            machineId = globalPara.MachineID;
        }

        private void PowerCalci_Load(object sender, EventArgs e)
        {
            DatabaseAccess.GetCurrentShiftDetails(out shiftStartTime, out shiftEndTime, out shiftName);           
            PowerCaliMachineInfo powerMIC = DatabaseAccess.GetPowerCalciMachineInfo(machineId, "BasicData");
            lblSpindleMotor.Text = powerMIC.SpindleType;
            lblPowerRating.Text = powerMIC.PowerRating.ToString();
            lbllblContinuousRating.Text = powerMIC.ContinuousPowerRating.ToString();
            lblConstTorqueRange.Text = powerMIC.TorqueRange.ToString();
            lblBaseSpeed1.Text =  powerMIC.BaseSpeed1.ToString();
            lblBaseSpeed2.Text = powerMIC.BaseSpeed2.ToString();
            lblShortTermPower.Text = powerMIC.BaseSpeedSrtTerm.ToString();
            lblMotorPulleyDia.Text =  powerMIC.MotorPulley.ToString();
            lblSpindlePulleyDia.Text = powerMIC.SpindlePulley.ToString();       
            cmbMarialUsed.DataSource = DatabaseAccess.GetPowerCalciConstants();

            if (cmbMarialUsed.DataSource != null)
            {
                lblMaterial1.Text = cmbMarialUsed.SelectedItem.ToString() ;
                lblMaterial2.Text = cmbMarialUsed.SelectedItem.ToString() ;
            }
            else
            {
                lblMaterial1.Text = "-----------";
                lblMaterial1.Text = "-----------";
            }

        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateFormFeilds()) return;

                Cursor.Current = Cursors.WaitCursor;
                PowerCaliMachineInfoGreen powerMICGreen = DatabaseAccess.GetPowerCalciMachineInfoGreen(machineId, txtComponentDia.Text, txtSpindleSpeed.Text, txtFeed.Text, txtGamaInDeg.Text, txtKInDegree.Text, txtDept.Text, cmbMarialUsed.SelectedItem.ToString(), "DerivedData");
                lblShortTermTorque.Text = powerMICGreen.ShortTermTorqueRange.ToString();
                lblCutting.Text = powerMICGreen.CuttingVal.ToString();
                lblPulleyRatio.Text = powerMICGreen.PulleyRatio.ToString();
                lblBaseSpeedN1.Text = powerMICGreen.BaseSpeedVal1.ToString();
                lblBaseSpeedN2.Text = powerMICGreen.BaseSpeedVal2.ToString();

                CalculatedValues CalVal = DatabaseAccess.PowerCalciCutConditions(machineId, txtComponentDia.Text, txtSpindleSpeed.Text, txtFeed.Text, txtGamaInDeg.Text, txtKInDegree.Text, txtDept.Text, cmbMarialUsed.SelectedItem.ToString(), "Power");
                lblKc.Text = CalVal.SpecificCuttingForce.ToString();
                lblChipThickness.Text = CalVal.ChipThickness.ToString();
                lblPowerReq.Text = CalVal.PowerRequired.ToString();
                lblPac.Text = CalVal.ContPowerRating.ToString();
                lblPas.Text = CalVal.ShortTermPowerRating.ToString();
                lblMc.Text = CalVal.curveRaise.ToString();
                lblKc1.Text = CalVal.SpecificCuttingForceForremoving.ToString();

                lblMaterial1.Text = cmbMarialUsed.SelectedItem.ToString();
                lblMaterial2.Text = cmbMarialUsed.SelectedItem.ToString();

                if (CalVal.PowerRequired < CalVal.ContPowerRating)//pac
                {
                    lblPowerReq.BackColor = Color.Green;
                }

                if (CalVal.PowerRequired > CalVal.ShortTermPowerRating)//pas
                {
                    lblPowerReq.BackColor = Color.Red;
                }

                if(CalVal.PowerRequired >= CalVal.ContPowerRating && CalVal.PowerRequired <= CalVal.ShortTermPowerRating)
                {
                    lblPowerReq.BackColor = Color.Yellow;
                }

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
                    
        }

        private bool ValidateFormFeilds()
        {
            string msg = string.Empty;

            if (string.IsNullOrEmpty(txtSpindleSpeed.Text))
            {
                msg = "Please Enter Spindle Speed.";
                MessageBox.Show(msg, "Important Note", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                txtSpindleSpeed.Focus();
                return true;
            }

            if (string.IsNullOrEmpty(txtComponentDia.Text))
            {
                msg = "Please Enter Diameter of the component..";
                MessageBox.Show(msg, "Important Note", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                txtComponentDia.Focus();
                return true;
            }

            if (string.IsNullOrEmpty(txtFeed.Text))
            {
                msg = "Please Enter Feed of the component.";
                MessageBox.Show(msg, "Important Note", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                txtFeed.Focus();
                return true;
            }

            if (string.IsNullOrEmpty(txtDept.Text))
            {
                msg = "Please Enter Depth of the component.";
                MessageBox.Show(msg, "Important Note", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                txtDept.Focus();
                return true;
            }

            if (string.IsNullOrEmpty(txtGamaInDeg.Text))
            {
                msg = "Please Enter  'ɣ' Degree.";
                MessageBox.Show(msg, "Important Note", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                txtGamaInDeg.Focus();
                return true;
            }

            if (string.IsNullOrEmpty(txtKInDegree.Text))
            {
                msg = "Please Enter 'k' Degree.";
                MessageBox.Show(msg, "Important Note", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                txtKInDegree.Focus();
                return true;
            }

            return false;
        }

        private void cmbMarialUsed_SelectionChangeCommitted(object sender, EventArgs e)
        {
            lblMaterial1.Text = cmbMarialUsed.SelectedItem.ToString();
            lblMaterial2.Text = cmbMarialUsed.SelectedItem.ToString();
        }

       
    }
}
