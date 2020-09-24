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
    public partial class ServiceSettings : UserControl
    {
        public ServiceSettings()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
             DatabaseAccess.UpdateServiceSettings(cmbLive.SelectedItem.ToString(),"LiveDataInterval"); 
             DatabaseAccess.UpdateServiceSettings(cmbSpindle.SelectedItem.ToString(),"SpindleDataInterval");
             DatabaseAccess.UpdateServiceSettings(cmbAlarm.SelectedItem.ToString(), "AlarmDataInterval");

             ServiceSettings_Load(null, EventArgs.Empty);
             CustomDialogBox cmb = new CustomDialogBox("Information Message","Service Data Updated Succesfully.");
             cmb.ShowDialog();
        }

        private void ServiceSettings_Load(object sender, EventArgs e)
        {
            var serviceSettings = DatabaseAccess.GetAllServiceSettingsData();
            if (serviceSettings != null)
            {
                cmbLive.Text = serviceSettings.live;
                cmbAlarm.Text = serviceSettings.alarm;
                cmbSpindle.Text = serviceSettings.spindle;
            
            }
        }
    }
}
