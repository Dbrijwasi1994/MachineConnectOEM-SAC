namespace MachineConnectOEM
{
    partial class PendingActivities
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PendingActivities));
            this.tblPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tblPanelSearch = new System.Windows.Forms.TableLayoutPanel();
            this.lblFromDate = new System.Windows.Forms.Label();
            this.dtFromDate = new System.Windows.Forms.DateTimePicker();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgvPendingActivities = new System.Windows.Forms.DataGridView();
            this.MachineID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Activity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DueDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Frequency = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tblPanelMain.SuspendLayout();
            this.tblPanelSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPendingActivities)).BeginInit();
            this.SuspendLayout();
            // 
            // tblPanelMain
            // 
            this.tblPanelMain.ColumnCount = 1;
            this.tblPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblPanelMain.Controls.Add(this.lblTitle, 0, 0);
            this.tblPanelMain.Controls.Add(this.tblPanelSearch, 0, 1);
            this.tblPanelMain.Controls.Add(this.dgvPendingActivities, 0, 2);
            this.tblPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tblPanelMain.Name = "tblPanelMain";
            this.tblPanelMain.RowCount = 3;
            this.tblPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblPanelMain.Size = new System.Drawing.Size(774, 501);
            this.tblPanelMain.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(18)))), ((int)(((byte)(53)))));
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft YaHei", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(774, 40);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "All Pending Activities List";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tblPanelSearch
            // 
            this.tblPanelSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(18)))), ((int)(((byte)(53)))));
            this.tblPanelSearch.ColumnCount = 4;
            this.tblPanelSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblPanelSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tblPanelSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tblPanelSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblPanelSearch.Controls.Add(this.lblFromDate, 0, 0);
            this.tblPanelSearch.Controls.Add(this.dtFromDate, 1, 0);
            this.tblPanelSearch.Controls.Add(this.btnRefresh, 2, 0);
            this.tblPanelSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblPanelSearch.Location = new System.Drawing.Point(0, 40);
            this.tblPanelSearch.Margin = new System.Windows.Forms.Padding(0);
            this.tblPanelSearch.Name = "tblPanelSearch";
            this.tblPanelSearch.RowCount = 1;
            this.tblPanelSearch.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblPanelSearch.Size = new System.Drawing.Size(774, 40);
            this.tblPanelSearch.TabIndex = 1;
            // 
            // lblFromDate
            // 
            this.lblFromDate.AutoSize = true;
            this.lblFromDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFromDate.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFromDate.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblFromDate.Location = new System.Drawing.Point(3, 0);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(94, 40);
            this.lblFromDate.TabIndex = 0;
            this.lblFromDate.Text = "From Date";
            this.lblFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtFromDate
            // 
            this.dtFromDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtFromDate.Location = new System.Drawing.Point(103, 10);
            this.dtFromDate.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.dtFromDate.Name = "dtFromDate";
            this.dtFromDate.Size = new System.Drawing.Size(154, 20);
            this.dtFromDate.TabIndex = 1;
            this.dtFromDate.TabStop = false;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Font = new System.Drawing.Font("Myanmar Text", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(263, 8);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(64, 23);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // dgvPendingActivities
            // 
            this.dgvPendingActivities.AllowUserToAddRows = false;
            this.dgvPendingActivities.AllowUserToDeleteRows = false;
            this.dgvPendingActivities.AllowUserToResizeColumns = false;
            this.dgvPendingActivities.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvPendingActivities.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPendingActivities.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPendingActivities.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPendingActivities.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPendingActivities.ColumnHeadersHeight = 50;
            this.dgvPendingActivities.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvPendingActivities.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MachineID,
            this.Activity,
            this.DueDate,
            this.Frequency});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Khaki;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPendingActivities.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvPendingActivities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPendingActivities.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvPendingActivities.EnableHeadersVisualStyles = false;
            this.dgvPendingActivities.Location = new System.Drawing.Point(3, 83);
            this.dgvPendingActivities.MultiSelect = false;
            this.dgvPendingActivities.Name = "dgvPendingActivities";
            this.dgvPendingActivities.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPendingActivities.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvPendingActivities.RowHeadersVisible = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.dgvPendingActivities.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvPendingActivities.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPendingActivities.Size = new System.Drawing.Size(768, 415);
            this.dgvPendingActivities.TabIndex = 2;
            // 
            // MachineID
            // 
            this.MachineID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.MachineID.DataPropertyName = "MachineID";
            this.MachineID.FillWeight = 2.269304F;
            this.MachineID.HeaderText = "Machine ID";
            this.MachineID.Name = "MachineID";
            this.MachineID.ReadOnly = true;
            this.MachineID.Width = 160;
            // 
            // Activity
            // 
            this.Activity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Activity.DataPropertyName = "Activity";
            this.Activity.FillWeight = 8.81544E-06F;
            this.Activity.HeaderText = "Activity";
            this.Activity.Name = "Activity";
            this.Activity.ReadOnly = true;
            // 
            // DueDate
            // 
            this.DueDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.DueDate.DataPropertyName = "DueDate";
            this.DueDate.FillWeight = 28.18754F;
            this.DueDate.HeaderText = "Activity Due Date";
            this.DueDate.Name = "DueDate";
            this.DueDate.ReadOnly = true;
            this.DueDate.Width = 280;
            // 
            // Frequency
            // 
            this.Frequency.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Frequency.DataPropertyName = "Frequency";
            this.Frequency.FillWeight = 369.5432F;
            this.Frequency.HeaderText = "Frequency";
            this.Frequency.Name = "Frequency";
            this.Frequency.ReadOnly = true;
            this.Frequency.Width = 200;
            // 
            // PendingActivities
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(18)))), ((int)(((byte)(53)))));
            this.ClientSize = new System.Drawing.Size(774, 501);
            this.Controls.Add(this.tblPanelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PendingActivities";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "All Pending Activities";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.PendingActivities_Load);
            this.tblPanelMain.ResumeLayout(false);
            this.tblPanelMain.PerformLayout();
            this.tblPanelSearch.ResumeLayout(false);
            this.tblPanelSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPendingActivities)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblPanelMain;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TableLayoutPanel tblPanelSearch;
        private System.Windows.Forms.Label lblFromDate;
        private System.Windows.Forms.DateTimePicker dtFromDate;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvPendingActivities;
        private System.Windows.Forms.DataGridViewTextBoxColumn MachineID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Activity;
        private System.Windows.Forms.DataGridViewTextBoxColumn DueDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Frequency;
    }
}