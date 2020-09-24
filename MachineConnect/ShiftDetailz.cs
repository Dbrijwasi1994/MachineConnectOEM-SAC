using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MachineConnectApplication
{
    public partial class ShiftDetailz : UserControl
    {
        List<string> dayList;
        List<string> shiftId;

        public ShiftDetailz()
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
            dayList = new List<string>();
            shiftId = new List<string>();

            AddDays(dayList);
            AddshiftId(shiftId);

            CmbfromDay.DataSource = dayList.ToList();
            CmbToDay.DataSource = dayList.ToList();
            cmbShiftId.DataSource = shiftId;

            dataGridView.ReadOnly = true;

             //dataGridView.DefaultCellStyle.SelectionBackColor = Color.White;
             dataGridView.DefaultCellStyle.Font = new Font("Segoe UI", 11,FontStyle.Regular);
             dataGridView.DefaultCellStyle.ForeColor = Color.Black;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        private void AddshiftId(List<string> shiftId)
        {
            shiftId.Add("1");
            shiftId.Add("2");
            shiftId.Add("3");
        }

        private void ShiftDetails_Load(object sender, EventArgs e)
        {           
            dataGridView.AutoGenerateColumns = false;

            var res = DatabaseAccess.GetAllshiftDetails();
            dataGridView.DataSource = res;
            dataGridView.ReadOnly = false;

            setValuesForDataColumn();          

        }

        private void setValuesForDataColumn()
        {
            for (int i = 0; i < dataGridView.RowCount; i++)
            {
                //FromDay
                if ((dataGridView[2, i].Value.ToString()).Equals("0"))
                {
                    dataGridView[2, i].Value = "Today";
                }
                else if ((dataGridView[2, i].Value.ToString()).Equals("1"))
                {
                    dataGridView[2, i].Value = "Tomorrow";
                }
                else
                {
                    dataGridView[2, i].Value = "Yesterday";
                }

                //ToDay
                if ((dataGridView[4, i].Value.ToString()).Equals("0"))
                {
                    dataGridView[4, i].Value = "Today";
                }
                else if ((dataGridView[4, i].Value.ToString()).Equals("1"))
                {
                    dataGridView[4, i].Value = "Tomorrow";
                }
                else
                {
                    dataGridView[4, i].Value = "Yesterday";
                }
            }

        }

        private void AddDays(List<string> dayList)
        {
            dayList.Add("Today");
            dayList.Add("Tomorrow");
            dayList.Add("Yesterday");
        }

        private void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {           
            if (ValidateFormFeilds())
            {
                bool isValidEntry = DatabaseAccess.CheckShiftId(cmbShiftId.Text);
                if (isValidEntry)
                {                   
                        DatabaseAccess.CheckForTheTimeEntry(dTPFromTime.Value.ToString(), dtpToTime.Value.ToString());
                        DatabaseAccess.UpdateShiftDetails(cmbShiftId.Text, txtShiftName.Text, CmbfromDay.Text, CmbToDay.Text, dTPFromTime.Value, dtpToTime.Value);
                        ShiftDetails_Load(null, EventArgs.Empty);
                        MessageBox.Show("Details Updated Successfully.");                   
                }

                else
                {
                    if (!DatabaseAccess.CheckForShiftName(txtShiftName.Text,cmbShiftId.Text))
                    {                      
                        DatabaseAccess.InsertShiftDetails(cmbShiftId.Text, txtShiftName.Text, CmbfromDay.Text, CmbToDay.Text, dTPFromTime.Value, dtpToTime.Value);
                        ShiftDetails_Load(null, EventArgs.Empty);
                        MessageBox.Show("Details Added Successfully.");                        
                    }
                    else
                    {
                        MessageBox.Show("Shift Name Already Exsits,.!! \n Please Enter Different Shift Name.");
                    }
                }

                dtpToTime.Value = (dtpToTime.Value).AddDays(-1);
            }
        }

        private bool CheckTime(DateTime fromTime, DateTime toTime)
        {
            if (fromTime >= toTime)
            {
                Console.WriteLine("True");
                return false;
            }
            return true;
        }

      

        private bool ValidateFormFeilds()
        {

            TimeSpan time1 = DateTime.Parse(dTPFromTime.Value.ToString("hh:mm:ss tt")).TimeOfDay;
            TimeSpan time2 = DateTime.Parse(dtpToTime.Value.ToString("hh:mm:ss tt")).TimeOfDay;
          
            if (string.IsNullOrEmpty(cmbShiftId.Text))
            {
                MessageBox.Show("Please Enter Valid Shift ID.", "Important Note", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }

            if (string.IsNullOrEmpty(txtShiftName.Text))
            {
                MessageBox.Show("Shift Name cannot be blank.\n Please enter Shift Name.", "Important Note", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }
            
            if ( ((CmbfromDay.Text).Equals("Tomorrow") && (CmbToDay.Text).Equals("Today")) ||   ((CmbfromDay.Text).Equals("Today") && (CmbToDay.Text).Equals("Yesterday"))
               || ((CmbfromDay.Text).Equals("Yesterday") && (CmbToDay.Text).Equals("Tomorrow")) || ((CmbfromDay.Text).Equals("Tomorrow") && (CmbToDay.Text).Equals("Yesterday")) || ((CmbfromDay.Text).Equals("Tomorrow") && (CmbToDay.Text).Equals("Today")))
            {
                MessageBox.Show("Please enter Valid Days.", "Important Note", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }

            if (((CmbfromDay.Text).Equals("Today") && (CmbToDay.Text).Equals("Today")) && (time1 > time2))
            {
                MessageBox.Show("Please enter Valid Timings. From Time Cannot Be Greater than To Time", "Important Note", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }

            if ((dTPFromTime.Value.Equals(dtpToTime.Value)))
            {
                MessageBox.Show("Please enter Valid Timings. From Time Cannot Be Equal to the To Time", "Important Note", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }

            if (((CmbfromDay.Text).Equals("Today") && (CmbToDay.Text).Equals("Tomorrow")))
            {
                if (time2 > time1)
                {
                    MessageBox.Show("Please enter Valid Timings. Time Is Greater than 24 hrs. Please Check the End time. ", "Important Note", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return false;
                }
            }

            if (((CmbfromDay.Text).Equals("Today") && (CmbToDay.Text).Equals("Today")) && (time1 == time2) || ((CmbfromDay.Text).Equals("Tomorrow") && (CmbToDay.Text).Equals("Tomorrow")) && (time1 == time2) || ((CmbfromDay.Text).Equals("Yesterday") && (CmbToDay.Text).Equals("Yesterday")) && (time1 == time2))
            {
                MessageBox.Show("Please enter Valid Timings. From Time Cannot Be Equal To the To Time", "Important Note", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }

          
            return true;
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            if (dataGridView.DataSource != null)
            {
                DialogResult res = MessageBox.Show("This removes all of your shift details. Click OK to Proceed.", "Important", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    DatabaseAccess.RemoveAllShiftdata();
                    ShiftDetails_Load(null, EventArgs.Empty);
                    resetAllFeilds();
                }
            }
            else
            {
                MessageBox.Show("No Records To Delete.\n ", "Important", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            }
        }

        private void cmbShiftId_SelectionChangeCommitted(object sender, EventArgs e)
        {
            resetAllFeilds();
            getDetailsForShiftId(cmbShiftId.Text);
        }

        private void resetAllFeilds()
        {
            txtShiftName.Text = string.Empty;
            CmbfromDay.SelectedIndex = 0;
            CmbToDay.SelectedIndex = 0;

            //dTPFromTime.Value =
        }

        private void getDetailsForShiftId(string shiftId)
        {
            var details = DatabaseAccess.GetShiftDetails(shiftId);
            if (details != null)
            {
                if (details.FromDay.ToString().Equals("0")) CmbfromDay.SelectedIndex = 0;
                else if (details.FromDay.ToString().Equals("1")) CmbfromDay.SelectedIndex = 1;
                else if (details.FromDay.ToString().Equals("2")) CmbfromDay.SelectedIndex = 2;

                if (details.ToDay.ToString().Equals("0")) CmbToDay.SelectedIndex = 0;
                else if (details.ToDay.ToString().Equals("1")) CmbToDay.SelectedIndex = 1;
                else if (details.ToDay.ToString().Equals("2")) CmbToDay.SelectedIndex = 2;

                dTPFromTime.Text = details.FromTime.ToString();
                dtpToTime.Text = details.ToTime.ToString();
                txtShiftName.Text = details.ShiftName.ToString();
            }
        }

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!(e.RowIndex < 0))
            {
                try
                {
                    txtShiftName.Text = dataGridView.Rows[e.RowIndex].Cells["ShiftName"].Value.ToString();
                    string s = dataGridView.Rows[e.RowIndex].Cells["ShiftIdz"].Value.ToString();

                    if (s.Equals("1"))
                    {
                        cmbShiftId.SelectedIndex = 0;
                    }
                    else if (s.Equals("2"))
                    {
                        cmbShiftId.SelectedIndex = 1;
                    }
                    else if (s.Equals("3"))
                    {
                        cmbShiftId.SelectedIndex = 2;
                    }
                  
                    dTPFromTime.Text = dataGridView.Rows[e.RowIndex].Cells["FromTime"].Value.ToString();
                    dtpToTime.Text = dataGridView.Rows[e.RowIndex].Cells["ToTime"].Value.ToString();

                    if (dataGridView.Rows[e.RowIndex].Cells["Fromday"].Value.ToString().Equals("Tomorrow")) CmbfromDay.SelectedIndex = 1;
                    else if (dataGridView.Rows[e.RowIndex].Cells["Fromday"].Value.ToString().Equals("Yesterday")) CmbfromDay.SelectedIndex = 2;
                    else if (dataGridView.Rows[e.RowIndex].Cells["Fromday"].Value.ToString().Equals("Today")) CmbfromDay.SelectedIndex = 0;


                    if (dataGridView.Rows[e.RowIndex].Cells["ToDay"].Value.ToString().Equals("Tomorrow")) CmbToDay.SelectedIndex = 1;
                    else if (dataGridView.Rows[e.RowIndex].Cells["ToDay"].Value.ToString().Equals("Yesterday")) CmbToDay.SelectedIndex = 2;
                    else if (dataGridView.Rows[e.RowIndex].Cells["ToDay"].Value.ToString().Equals("Today")) CmbToDay.SelectedIndex = 0;



                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error,.!!" + ex.Message);
                }
            }
        }       
    }
}
