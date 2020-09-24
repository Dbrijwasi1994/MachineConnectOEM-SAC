using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MachineConnectApplication;

namespace MachineConnectApplication
{
    public partial class ProcessDoc : UserControl
    {
        public RPM userControl = null;
        public ProcessDoc()
        {
            InitializeComponent();
            dataGrid.AutoGenerateColumns = false;
        }

        public ProcessDoc(UserControl userControl)
        {
            InitializeComponent();
            userControl = userControl as RPM;               
        }


        private void Process_Load(object sender, EventArgs e)
        {
            int width = getSize();
            if (width < 1400)
            {
                sectionLTabelLayoutPnl.Margin.Left.Equals(70);
                sectionLTabelLayoutPnl.Margin.Right.Equals(70);
                sectionLTabelLayoutPnl.ColumnStyles[1].Width = 5;

            }
            else
            {
                sectionLTabelLayoutPnl.Margin.Left.Equals(200);
                sectionLTabelLayoutPnl.Margin.Right.Equals(200);
                sectionLTabelLayoutPnl.ColumnStyles[1].Width = 150;
            }
            dataGrid.AutoGenerateColumns = false;
            dataGridView1.AutoGenerateColumns = false;
            DataTable dt = new DataTable();
            DataTable dtoutput = new DataTable();
            dt = DatabaseAccess.GetSpindleProcessOutput("output");
            dtoutput = DatabaseAccess.GetSpindleProcessOutput("User defined");
            if (dt != null)
            {
                dataGrid.DataSource = dt;
            }

            if (dtoutput != null)
            {
                dataGridView1.DataSource = dtoutput;
            }
            
        }

        private void dataGrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < dataGrid.Rows.Count; i++)
            {
                dataGrid.Rows[i].Cells["CurrentValue"].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                dataGrid.Rows[i].Cells["CurrentValue"].Style.SelectionBackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                dataGrid.Rows[i].Cells["CurrentValue"].Style.SelectionForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
                dataGrid.Rows[i].Cells["CurrentValue"].Style.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
                dataGrid.Rows[i].Cells["DESCRIPTION"].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                dataGrid.Rows[i].Cells["DESCRIPTION"].Style.SelectionBackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                dataGrid.Rows[i].Cells["DESCRIPTION"].Style.SelectionForeColor = System.Drawing.ColorTranslator.FromHtml("#2A58A3");
                dataGrid.Rows[i].Cells["DESCRIPTION"].Style.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2A58A3");
                dataGrid.Rows[i].Cells["UNIT1"].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                dataGrid.Rows[i].Cells["UNIT1"].Style.SelectionBackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                dataGrid.Rows[i].Cells["UNIT1"].Style.SelectionForeColor = System.Drawing.ColorTranslator.FromHtml("#2A58A3");
                dataGrid.Rows[i].Cells["UNIT1"].Style.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2A58A3");
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells["Val"].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                dataGridView1.Rows[i].Cells["Val"].Style.SelectionBackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                dataGridView1.Rows[i].Cells["Val"].Style.SelectionForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
                dataGridView1.Rows[i].Cells["Val"].Style.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
                dataGridView1.Rows[i].Cells["DESC"].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                dataGridView1.Rows[i].Cells["DESC"].Style.SelectionBackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                dataGridView1.Rows[i].Cells["DESC"].Style.SelectionForeColor = System.Drawing.ColorTranslator.FromHtml("#2A58A3");
                dataGridView1.Rows[i].Cells["DESC"].Style.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2A58A3");
                dataGridView1.Rows[i].Cells["UNIT"].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                dataGridView1.Rows[i].Cells["UNIT"].Style.SelectionBackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                dataGridView1.Rows[i].Cells["UNIT"].Style.SelectionForeColor = System.Drawing.ColorTranslator.FromHtml("#2A58A3");
                dataGridView1.Rows[i].Cells["UNIT"].Style.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2A58A3");
              
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            DisposePanelControls();
            pnlContainer.Controls.Clear();
            RPM ctrl = new RPM(this);
            ctrl.userControl = this;
            ctrl.Dock = DockStyle.Fill;
            pnlContainer.Controls.Add(ctrl);

            this.Cursor = Cursors.Default;
        }

       

        private void DisposePanelControls()
        {
            foreach (Control p in pnlContainer.Controls)
            {
                p.Dispose();
            }
        }


        public int getSize()
        {
            System.Drawing.Rectangle workingRectangle = Screen.PrimaryScreen.WorkingArea;
            this.Size = new System.Drawing.Size(
                workingRectangle.Width - 10, workingRectangle.Height - 10);
            int Width = workingRectangle.Width;
           
            return Width;

        }
      
    }
}
