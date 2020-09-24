namespace MachineConnectApplication
{
    partial class PredictiveMaintanance
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tblStopageGrid = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridStoppage = new System.Windows.Forms.DataGridView();
            this.lblStoppage = new System.Windows.Forms.Label();
            this.CncAlaram = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CncMessage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HoursLeft = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Due = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tblStopageGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridStoppage)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(955, 534);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tblStopageGrid, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(471, 528);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // tblStopageGrid
            // 
            this.tblStopageGrid.ColumnCount = 1;
            this.tblStopageGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblStopageGrid.Controls.Add(this.dataGridStoppage, 0, 1);
            this.tblStopageGrid.Controls.Add(this.lblStoppage, 0, 0);
            this.tblStopageGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblStopageGrid.Location = new System.Drawing.Point(0, 0);
            this.tblStopageGrid.Margin = new System.Windows.Forms.Padding(0);
            this.tblStopageGrid.Name = "tblStopageGrid";
            this.tblStopageGrid.RowCount = 2;
            this.tblStopageGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tblStopageGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblStopageGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblStopageGrid.Size = new System.Drawing.Size(471, 528);
            this.tblStopageGrid.TabIndex = 65;
            // 
            // dataGridStoppage
            // 
            this.dataGridStoppage.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(208)))), ((int)(((byte)(160)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridStoppage.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridStoppage.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.dataGridStoppage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(88)))), ((int)(((byte)(163)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridStoppage.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridStoppage.ColumnHeadersHeight = 40;
            this.dataGridStoppage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridStoppage.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CncAlaram,
            this.CncMessage,
            this.Time,
            this.HoursLeft,
            this.Due});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(208)))), ((int)(((byte)(160)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridStoppage.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridStoppage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridStoppage.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridStoppage.EnableHeadersVisualStyles = false;
            this.dataGridStoppage.GridColor = System.Drawing.Color.Silver;
            this.dataGridStoppage.Location = new System.Drawing.Point(1, 19);
            this.dataGridStoppage.Margin = new System.Windows.Forms.Padding(1);
            this.dataGridStoppage.Name = "dataGridStoppage";
            this.dataGridStoppage.ReadOnly = true;
            this.dataGridStoppage.RowHeadersVisible = false;
            this.dataGridStoppage.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.dataGridStoppage.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dataGridStoppage.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(208)))), ((int)(((byte)(160)))));
            this.dataGridStoppage.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridStoppage.RowTemplate.Height = 30;
            this.dataGridStoppage.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridStoppage.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridStoppage.Size = new System.Drawing.Size(469, 508);
            this.dataGridStoppage.TabIndex = 13;
            // 
            // lblStoppage
            // 
            this.lblStoppage.AutoSize = true;
            this.lblStoppage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(88)))), ((int)(((byte)(163)))));
            this.lblStoppage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStoppage.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStoppage.ForeColor = System.Drawing.Color.White;
            this.lblStoppage.Location = new System.Drawing.Point(1, 0);
            this.lblStoppage.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.lblStoppage.Name = "lblStoppage";
            this.lblStoppage.Size = new System.Drawing.Size(469, 18);
            this.lblStoppage.TabIndex = 15;
            this.lblStoppage.Text = "Predictive Maintanance";
            this.lblStoppage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CncAlaram
            // 
            this.CncAlaram.DataPropertyName = "Alaram";
            this.CncAlaram.HeaderText = "Alaram";
            this.CncAlaram.Name = "CncAlaram";
            this.CncAlaram.ReadOnly = true;
            // 
            // CncMessage
            // 
            this.CncMessage.HeaderText = "Message";
            this.CncMessage.Name = "CncMessage";
            this.CncMessage.ReadOnly = true;
            // 
            // Time
            // 
            this.Time.DataPropertyName = "Time";
            this.Time.HeaderText = "Time";
            this.Time.Name = "Time";
            this.Time.ReadOnly = true;
            // 
            // HoursLeft
            // 
            this.HoursLeft.HeaderText = "Hours Left";
            this.HoursLeft.Name = "HoursLeft";
            this.HoursLeft.ReadOnly = true;
            // 
            // Due
            // 
            this.Due.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Due.HeaderText = "Due";
            this.Due.Name = "Due";
            this.Due.ReadOnly = true;
            // 
            // PredictiveMaintanance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PredictiveMaintanance";
            this.Size = new System.Drawing.Size(955, 534);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tblStopageGrid.ResumeLayout(false);
            this.tblStopageGrid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridStoppage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tblStopageGrid;
        private System.Windows.Forms.DataGridView dataGridStoppage;
        private System.Windows.Forms.Label lblStoppage;
        private System.Windows.Forms.DataGridViewTextBoxColumn CncAlaram;
        private System.Windows.Forms.DataGridViewTextBoxColumn CncMessage;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn HoursLeft;
        private System.Windows.Forms.DataGridViewTextBoxColumn Due;
    }
}
