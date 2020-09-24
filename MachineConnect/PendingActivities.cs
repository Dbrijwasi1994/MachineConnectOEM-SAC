using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MachineConnectOEM.SAC;
using FocasGUI;
using MachineConnectApplication;

namespace MachineConnectOEM
{
    public partial class PendingActivities : Form
    {
        private ObservableCollection<NotificationData> allPendingList;
        private string selectedMachine;
        private DateTime PendingActStartDate;

        public PendingActivities()
        {
            InitializeComponent();
        }

        public PendingActivities(ObservableCollection<NotificationData> allPendingList, string selectedMachine, DateTime pendingActStartDate)
        {
            this.allPendingList = allPendingList;
            this.selectedMachine = selectedMachine;
            this.PendingActStartDate = pendingActStartDate;
            InitializeComponent();
        }

        private void PendingActivities_Load(object sender, EventArgs e)
        {
            dgvPendingActivities.AutoGenerateColumns = false;
            dtFromDate.Value = PendingActStartDate;
            if (allPendingList != null && allPendingList.Count > 0)
            {
                dgvPendingActivities.DataSource = allPendingList;
            }
            else
            {
                dgvPendingActivities.DataSource = new ObservableCollection<NotificationData>();
                CustomDialogBox dlgError = new CustomDialogBox("Information Message", "No data available for selected date.");
                dlgError.ShowDialog();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            allPendingList = DataBaseAccess_SAC.GetAllPendingActivities(selectedMachine, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "Weekly", dtFromDate.Value.ToString("yyyy-MM-dd hh:mm:ss"));
            if (allPendingList != null && allPendingList.Count > 0)
            {
                dgvPendingActivities.DataSource = allPendingList;
            }
            else
            {
                dgvPendingActivities.DataSource = new ObservableCollection<NotificationData>();
                CustomDialogBox dlgError = new CustomDialogBox("Information Message", "No data available for selected date.");
                dlgError.ShowDialog();
            }
        }
    }
}
