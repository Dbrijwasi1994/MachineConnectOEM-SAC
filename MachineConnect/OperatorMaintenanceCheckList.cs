using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MachineConnectApplication
{
    public partial class OperatorMaintenanceCheckList : UserControl
    {
        string manualsFolderPath = string.Empty;
        string manualsMTBPath = string.Empty;
        string manualsProductPath = string.Empty;
        string machineModel = string.Empty;

        public OperatorMaintenanceCheckList()
        {
            InitializeComponent();
        }

        private void OperatorMaintenanceCheckList_Load(object sender, EventArgs e)
        {
            DataTable dt = DatabaseAccess.GetOperatorMaintenanceCheckList();
            if (dt != null && dt.Rows.Count > 0)
            {
                dataGrid.DataSource = dt;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            manualsFolderPath = DatabaseAccess.GetGenericMachineConnectFolderPath();
            manualsMTBPath = DatabaseAccess.GetAlarmsMTBPath(HomeScreen.selectedMachine, out machineModel);
            manualsProductPath = DatabaseAccess.GetProductPathForMTB(HomeScreen.selectedMachine, manualsMTBPath);

            if (string.IsNullOrEmpty(manualsFolderPath))
            {
                manualsFolderPath = System.IO.Path.Combine(Settings.APP_PATH, "AlarmsAndDocs");
            }

            string doc = Path.Combine(manualsFolderPath, manualsMTBPath, "Daily CheckList");
            if (!Directory.Exists(doc)) return;
            var file = Directory.GetFiles(doc);
            if (file.Count() > 0)
            {
                System.Diagnostics.Process.Start(file[0].ToString());
            }
        }
    }
}
