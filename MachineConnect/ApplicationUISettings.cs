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
    public partial class ApplicationUISettings : UserControl
    {
        string GraphType;

        public ApplicationUISettings()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(radioButton1.Checked) GraphType = "Bar";
            else if (radioButton2.Checked) GraphType = "Line";

            DatabaseAccess.SetApplicationUISettings("AutoRefreshInterval", cmbSpindle.Text.ToString());
            DatabaseAccess.SetApplicationUISettings("GraphType", GraphType);
            DatabaseAccess.SetApplicationUISettings("AlarmsFolderPath", txtAlarmsFolderPath.Text.ToString());
            DatabaseAccess.SetApplicationUISettings("ProgramsPath", txtProgramsFolderPath.Text.ToString());
            DatabaseAccess.SetApplicationUISettings("DowntimeThreshold", cmbStoppages.Text.ToString());

            Settings.AutoRefreshInterval = Convert.ToInt32(cmbSpindle.Text);
            Settings.StoppagesThreshold = cmbStoppages.Text;
            CNC_PT.SettingsPT.Program_path = txtProgramsFolderPath.Text;


             ServiceSettings_Load(null, EventArgs.Empty);
             CustomDialogBox cmb = new CustomDialogBox("Information Message","Service Data Updated Succesfully.");
             cmb.ShowDialog();
        }

        private void ServiceSettings_Load(object sender, EventArgs e)
        {
            ApplicationSettingsVals val = DatabaseAccess.GetApplicationUISettings();
            if (val != null)
            {
                cmbSpindle.Text = val.AutoRefreshInterval;
                if (val.GraphTypeVal.Equals("Bar"))
                {
                    radioButton1.Checked = true;
                }
                else
                {
                    radioButton2.Checked = true;
                }

                txtAlarmsFolderPath.Text = val.AlarmsFolderPath;
                txtProgramsFolderPath.Text = val.ProgramsPath;
                cmbStoppages.Text = val.StoppagesThreshold;
            }
        }

        private void btnBrowsePath_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtAlarmsFolderPath.Text = (folderBrowserDialog1.SelectedPath);
            }
        }

        private void btnBrowProgFolder_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtProgramsFolderPath.Text = (folderBrowserDialog1.SelectedPath);
            }
        }

    
    }
}
