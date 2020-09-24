using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MachineConnectApplication;

namespace MachineConnectOEM
{
    public partial class ActivityInformation : UserControl
    {
        List<Tuple<string, string>> listFreq = new List<Tuple<string, string>>();
        int ActivityIdKey;
        public ActivityInformation()
        {
            InitializeComponent();
            BindFreq();
            BindActivityInformation("");
            dataGrid.AutoGenerateColumns = false;
        }

        #region "Bind Freq"
        private void BindFreq()
        {
            try
            {
                listFreq = DatabaseAccess.ListFreqData();
                cmbFreq.DataSource = listFreq;
                cmbFreq.ValueMember = "Item1";
                cmbFreq.DisplayMember = "Item2";
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
                CustomDialogBox frm = new CustomDialogBox("Error Message", ex.Message);
                frm.ShowDialog();
            }
        }
        #endregion

        #region "Bind Freq"
        private void BindActivityInformation(string freq)
        {
            try
            {

                //DataTable dt = DatabaseAccess.GetAllActivityForGrid(freq);
                //dataGrid.DataSource = dt;
                //ActivityIdKey = 0;               
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
                CustomDialogBox frm = new CustomDialogBox("Error Message", ex.Message);
                frm.ShowDialog();
            }
        }
        #endregion

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                BindActivityInformation(cmbFreq.Text.ToString());
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
                CustomDialogBox frm = new CustomDialogBox("Error Message", ex.Message);
                frm.ShowDialog();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int rowCount = 0;
                if (txtActivity.Text.ToString() == "")
                {
                    CustomDialogBox dialog = new CustomDialogBox("Information Message", "Please enter the txtActivity !!");
                    dialog.ShowDialog();
                    return;
                }
                DatabaseAccess.SaveActivityMasterData(ActivityIdKey, txtActivity.Text.ToString(), cmbFreq.SelectedValue.ToString(), out rowCount);
                if (rowCount > 0)
                {
                    CustomDialogBox frm = new CustomDialogBox("Information Message", "Details added / Updated successfully.");
                    frm.ShowDialog();
                    BindActivityInformation("");
                    this.Cursor = Cursors.Arrow;
                }
                else
                {
                    CustomDialogBox frm = new CustomDialogBox("Information Message", "Records not be Updated .");
                    frm.ShowDialog();
                    this.Cursor = Cursors.Arrow;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
                CustomDialogBox frm = new CustomDialogBox("Error Message", ex.Message);
                frm.ShowDialog();
            }
        }

        private void dataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!(e.RowIndex < 0))
            {
                try
                {
                    ActivityIdKey = Convert.ToInt32(dataGrid.Rows[e.RowIndex].Cells["ActivityID"].Value.ToString());
                    txtActivity.Text = dataGrid.Rows[e.RowIndex].Cells["Activity"].Value.ToString();
                    cmbFreq.Text = dataGrid.Rows[e.RowIndex].Cells["Frequency"].Value.ToString();                 
                }
                catch (Exception ex)
                {
                    Logger.WriteErrorLog(ex.Message);
                    CustomDialogBox frm = new CustomDialogBox("Error Message", ex.Message);
                    frm.ShowDialog();
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            BindFreq();
            BindActivityInformation("");
            txtActivity.Text = string.Empty;
        }
       
    }
}
