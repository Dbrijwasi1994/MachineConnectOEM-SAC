namespace MachineConnectOEM
{
    partial class CycleProfile
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.dgvCycleDetails = new System.Windows.Forms.DataGridView();
            this.SelectCycle = new System.Windows.Forms.DataGridViewButtonColumn();
            this.MachineModel = new System.Windows.Forms.DataGridViewLinkColumn();
            this.IPAddress = new System.Windows.Forms.DataGridViewLinkColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cmbParameter = new System.Windows.Forms.ComboBox();
            this.lblParameter = new System.Windows.Forms.Label();
            this.cmbMachineId = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.lblAxis = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.btnView = new System.Windows.Forms.Button();
            this.chkShowMarkers = new System.Windows.Forms.CheckBox();
            this.chkShowValues = new System.Windows.Forms.CheckBox();
            this.ChartsPanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanelCharts = new System.Windows.Forms.TableLayoutPanel();
            this.chartViewer6 = new ChartDirector.WinChartViewer();
            this.chartViewer5 = new ChartDirector.WinChartViewer();
            this.chartViewer4 = new ChartDirector.WinChartViewer();
            this.chartViewer3 = new ChartDirector.WinChartViewer();
            this.chartViewer2 = new ChartDirector.WinChartViewer();
            this.chartViewer1 = new ChartDirector.WinChartViewer();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.cmbInterval = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCycleDetails)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.ChartsPanel.SuspendLayout();
            this.tableLayoutPanelCharts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartViewer6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartViewer5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartViewer4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartViewer3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartViewer2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartViewer1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 1;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Controls.Add(this.dgvCycleDetails, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.ChartsPanel, 0, 2);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Margin = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 3;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(1607, 1234);
            this.tableLayoutPanelMain.TabIndex = 0;
            // 
            // dgvCycleDetails
            // 
            this.dgvCycleDetails.AllowUserToAddRows = false;
            this.dgvCycleDetails.AllowUserToDeleteRows = false;
            this.dgvCycleDetails.AllowUserToOrderColumns = true;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvCycleDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvCycleDetails.BackgroundColor = System.Drawing.Color.White;
            this.dgvCycleDetails.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvCycleDetails.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.White;
            this.dgvCycleDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvCycleDetails.ColumnHeadersHeight = 40;
            this.dgvCycleDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvCycleDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SelectCycle,
            this.MachineModel,
            this.IPAddress});
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCycleDetails.DefaultCellStyle = dataGridViewCellStyle13;
            this.dgvCycleDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCycleDetails.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvCycleDetails.EnableHeadersVisualStyles = false;
            this.dgvCycleDetails.GridColor = System.Drawing.SystemColors.ControlLight;
            this.dgvCycleDetails.Location = new System.Drawing.Point(0, 34);
            this.dgvCycleDetails.Margin = new System.Windows.Forms.Padding(0);
            this.dgvCycleDetails.Name = "dgvCycleDetails";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCycleDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.dgvCycleDetails.RowHeadersVisible = false;
            this.dgvCycleDetails.RowHeadersWidth = 40;
            this.dgvCycleDetails.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dgvCycleDetails.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvCycleDetails.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvCycleDetails.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgvCycleDetails.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvCycleDetails.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCycleDetails.RowTemplate.Height = 35;
            this.dgvCycleDetails.RowTemplate.ReadOnly = true;
            this.dgvCycleDetails.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCycleDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCycleDetails.Size = new System.Drawing.Size(1607, 200);
            this.dgvCycleDetails.TabIndex = 21;
            this.dgvCycleDetails.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCycleDetails_CellContentDoubleClick);
            this.dgvCycleDetails.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvCycleDetails_DataBindingComplete);
            // 
            // SelectCycle
            // 
            this.SelectCycle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.Black;
            this.SelectCycle.DefaultCellStyle = dataGridViewCellStyle10;
            this.SelectCycle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SelectCycle.HeaderText = "Select";
            this.SelectCycle.MinimumWidth = 100;
            this.SelectCycle.Name = "SelectCycle";
            this.SelectCycle.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SelectCycle.Text = "Select";
            this.SelectCycle.ToolTipText = "Select Cycle";
            // 
            // MachineModel
            // 
            this.MachineModel.DataPropertyName = "CycleStart";
            dataGridViewCellStyle11.Format = "yyyy-MM-dd HH:mm:ss ";
            this.MachineModel.DefaultCellStyle = dataGridViewCellStyle11;
            this.MachineModel.HeaderText = "Cycle Start";
            this.MachineModel.MinimumWidth = 150;
            this.MachineModel.Name = "MachineModel";
            this.MachineModel.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.MachineModel.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.MachineModel.Width = 200;
            // 
            // IPAddress
            // 
            this.IPAddress.DataPropertyName = "CycleEnd";
            dataGridViewCellStyle12.Format = "yyyy-MM-dd HH:mm:ss ";
            this.IPAddress.DefaultCellStyle = dataGridViewCellStyle12;
            this.IPAddress.HeaderText = "Cycle End";
            this.IPAddress.MinimumWidth = 150;
            this.IPAddress.Name = "IPAddress";
            this.IPAddress.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IPAddress.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.IPAddress.Width = 200;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 14;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 104F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label3, 8, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbInterval, 10, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbParameter, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblParameter, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbMachineId, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.dtpToDate, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblAxis, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dtpFromDate, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnView, 8, 0);
            this.tableLayoutPanel1.Controls.Add(this.chkShowMarkers, 12, 0);
            this.tableLayoutPanel1.Controls.Add(this.chkShowValues, 13, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 1);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1605, 32);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // cmbParameter
            // 
            this.cmbParameter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbParameter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbParameter.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbParameter.FormattingEnabled = true;
            this.cmbParameter.Items.AddRange(new object[] {
            "Hour",
            "Minute",
            "Seconds"});
            this.cmbParameter.Location = new System.Drawing.Point(839, 3);
            this.cmbParameter.Name = "cmbParameter";
            this.cmbParameter.Size = new System.Drawing.Size(144, 25);
            this.cmbParameter.TabIndex = 29;
            // 
            // lblParameter
            // 
            this.lblParameter.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblParameter.AutoSize = true;
            this.lblParameter.BackColor = System.Drawing.Color.White;
            this.lblParameter.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblParameter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblParameter.Location = new System.Drawing.Point(739, 6);
            this.lblParameter.Name = "lblParameter";
            this.lblParameter.Size = new System.Drawing.Size(92, 20);
            this.lblParameter.TabIndex = 28;
            this.lblParameter.Text = "Parameter : ";
            this.lblParameter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbMachineId
            // 
            this.cmbMachineId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbMachineId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMachineId.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMachineId.FormattingEnabled = true;
            this.cmbMachineId.Items.AddRange(new object[] {
            "Hour",
            "Minute",
            "Seconds"});
            this.cmbMachineId.Location = new System.Drawing.Point(589, 3);
            this.cmbMachineId.Name = "cmbMachineId";
            this.cmbMachineId.Size = new System.Drawing.Size(144, 25);
            this.cmbMachineId.TabIndex = 24;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(483, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 20);
            this.label2.TabIndex = 23;
            this.label2.Text = "Machine ID : ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpToDate
            // 
            this.dtpToDate.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dtpToDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(333, 4);
            this.dtpToDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(144, 23);
            this.dtpToDate.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(253, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 20);
            this.label1.TabIndex = 21;
            this.label1.Text = "To Date : ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAxis
            // 
            this.lblAxis.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAxis.AutoSize = true;
            this.lblAxis.BackColor = System.Drawing.Color.White;
            this.lblAxis.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAxis.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblAxis.Location = new System.Drawing.Point(3, 6);
            this.lblAxis.Name = "lblAxis";
            this.lblAxis.Size = new System.Drawing.Size(93, 20);
            this.lblAxis.TabIndex = 19;
            this.lblAxis.Text = "From Date : ";
            this.lblAxis.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dtpFromDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(103, 5);
            this.dtpFromDate.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(144, 23);
            this.dtpFromDate.TabIndex = 20;
            // 
            // btnView
            // 
            this.btnView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnView.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnView.Location = new System.Drawing.Point(988, 2);
            this.btnView.Margin = new System.Windows.Forms.Padding(2);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(76, 28);
            this.btnView.TabIndex = 25;
            this.btnView.Text = "View";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // chkShowMarkers
            // 
            this.chkShowMarkers.AutoSize = true;
            this.chkShowMarkers.Checked = true;
            this.chkShowMarkers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowMarkers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkShowMarkers.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShowMarkers.Location = new System.Drawing.Point(1404, 3);
            this.chkShowMarkers.Name = "chkShowMarkers";
            this.chkShowMarkers.Size = new System.Drawing.Size(98, 26);
            this.chkShowMarkers.TabIndex = 26;
            this.chkShowMarkers.Text = "Show Markers";
            this.chkShowMarkers.UseVisualStyleBackColor = true;
            this.chkShowMarkers.CheckedChanged += new System.EventHandler(this.chkShowMarkers_CheckedChanged);
            // 
            // chkShowValues
            // 
            this.chkShowValues.AutoSize = true;
            this.chkShowValues.Checked = true;
            this.chkShowValues.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowValues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkShowValues.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShowValues.Location = new System.Drawing.Point(1508, 3);
            this.chkShowValues.Name = "chkShowValues";
            this.chkShowValues.Size = new System.Drawing.Size(94, 26);
            this.chkShowValues.TabIndex = 27;
            this.chkShowValues.Text = "Show Values";
            this.chkShowValues.UseVisualStyleBackColor = true;
            this.chkShowValues.CheckedChanged += new System.EventHandler(this.chkShowValues_CheckedChanged);
            // 
            // ChartsPanel
            // 
            this.ChartsPanel.AutoScroll = true;
            this.ChartsPanel.AutoSize = true;
            this.ChartsPanel.Controls.Add(this.tableLayoutPanelCharts);
            this.ChartsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChartsPanel.Location = new System.Drawing.Point(2, 236);
            this.ChartsPanel.Margin = new System.Windows.Forms.Padding(2);
            this.ChartsPanel.Name = "ChartsPanel";
            this.ChartsPanel.Size = new System.Drawing.Size(1603, 996);
            this.ChartsPanel.TabIndex = 22;
            // 
            // tableLayoutPanelCharts
            // 
            this.tableLayoutPanelCharts.AutoScroll = true;
            this.tableLayoutPanelCharts.AutoSize = true;
            this.tableLayoutPanelCharts.ColumnCount = 1;
            this.tableLayoutPanelCharts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelCharts.Controls.Add(this.chartViewer6, 0, 5);
            this.tableLayoutPanelCharts.Controls.Add(this.chartViewer5, 0, 4);
            this.tableLayoutPanelCharts.Controls.Add(this.chartViewer4, 0, 3);
            this.tableLayoutPanelCharts.Controls.Add(this.chartViewer3, 0, 2);
            this.tableLayoutPanelCharts.Controls.Add(this.chartViewer2, 0, 1);
            this.tableLayoutPanelCharts.Controls.Add(this.hScrollBar1, 0, 6);
            this.tableLayoutPanelCharts.Controls.Add(this.chartViewer1, 0, 0);
            this.tableLayoutPanelCharts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelCharts.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelCharts.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelCharts.MaximumSize = new System.Drawing.Size(1916, 1200);
            this.tableLayoutPanelCharts.Name = "tableLayoutPanelCharts";
            this.tableLayoutPanelCharts.RowCount = 7;
            this.tableLayoutPanelCharts.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanelCharts.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanelCharts.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanelCharts.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanelCharts.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanelCharts.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanelCharts.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelCharts.Size = new System.Drawing.Size(1603, 996);
            this.tableLayoutPanelCharts.TabIndex = 0;
            // 
            // chartViewer6
            // 
            this.chartViewer6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chartViewer6.HotSpotCursor = System.Windows.Forms.Cursors.Hand;
            this.chartViewer6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chartViewer6.Location = new System.Drawing.Point(3, 1003);
            this.chartViewer6.Name = "chartViewer6";
            this.chartViewer6.ScrollDirection = ChartDirector.WinChartDirection.HorizontalVertical;
            this.chartViewer6.Size = new System.Drawing.Size(1580, 194);
            this.chartViewer6.TabIndex = 31;
            this.chartViewer6.TabStop = false;
            this.chartViewer6.ZoomDirection = ChartDirector.WinChartDirection.HorizontalVertical;
            this.chartViewer6.ViewPortChanged += new ChartDirector.WinViewPortEventHandler(this.chartViewer1_ViewPortChanged);
            // 
            // chartViewer5
            // 
            this.chartViewer5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chartViewer5.HotSpotCursor = System.Windows.Forms.Cursors.Hand;
            this.chartViewer5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chartViewer5.Location = new System.Drawing.Point(3, 803);
            this.chartViewer5.Name = "chartViewer5";
            this.chartViewer5.ScrollDirection = ChartDirector.WinChartDirection.HorizontalVertical;
            this.chartViewer5.Size = new System.Drawing.Size(1580, 194);
            this.chartViewer5.TabIndex = 30;
            this.chartViewer5.TabStop = false;
            this.chartViewer5.ZoomDirection = ChartDirector.WinChartDirection.HorizontalVertical;
            this.chartViewer5.ViewPortChanged += new ChartDirector.WinViewPortEventHandler(this.chartViewer1_ViewPortChanged);
            // 
            // chartViewer4
            // 
            this.chartViewer4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chartViewer4.HotSpotCursor = System.Windows.Forms.Cursors.Hand;
            this.chartViewer4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chartViewer4.Location = new System.Drawing.Point(3, 603);
            this.chartViewer4.Name = "chartViewer4";
            this.chartViewer4.ScrollDirection = ChartDirector.WinChartDirection.HorizontalVertical;
            this.chartViewer4.Size = new System.Drawing.Size(1580, 194);
            this.chartViewer4.TabIndex = 29;
            this.chartViewer4.TabStop = false;
            this.chartViewer4.ZoomDirection = ChartDirector.WinChartDirection.HorizontalVertical;
            this.chartViewer4.ViewPortChanged += new ChartDirector.WinViewPortEventHandler(this.chartViewer1_ViewPortChanged);
            // 
            // chartViewer3
            // 
            this.chartViewer3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chartViewer3.HotSpotCursor = System.Windows.Forms.Cursors.Hand;
            this.chartViewer3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chartViewer3.Location = new System.Drawing.Point(3, 403);
            this.chartViewer3.Name = "chartViewer3";
            this.chartViewer3.ScrollDirection = ChartDirector.WinChartDirection.HorizontalVertical;
            this.chartViewer3.Size = new System.Drawing.Size(1580, 194);
            this.chartViewer3.TabIndex = 28;
            this.chartViewer3.TabStop = false;
            this.chartViewer3.ZoomDirection = ChartDirector.WinChartDirection.HorizontalVertical;
            this.chartViewer3.ViewPortChanged += new ChartDirector.WinViewPortEventHandler(this.chartViewer1_ViewPortChanged);
            // 
            // chartViewer2
            // 
            this.chartViewer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chartViewer2.HotSpotCursor = System.Windows.Forms.Cursors.Hand;
            this.chartViewer2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chartViewer2.Location = new System.Drawing.Point(3, 203);
            this.chartViewer2.Name = "chartViewer2";
            this.chartViewer2.ScrollDirection = ChartDirector.WinChartDirection.HorizontalVertical;
            this.chartViewer2.Size = new System.Drawing.Size(1580, 194);
            this.chartViewer2.TabIndex = 27;
            this.chartViewer2.TabStop = false;
            this.chartViewer2.ZoomDirection = ChartDirector.WinChartDirection.HorizontalVertical;
            this.chartViewer2.ViewPortChanged += new ChartDirector.WinViewPortEventHandler(this.chartViewer1_ViewPortChanged);
            // 
            // chartViewer1
            // 
            this.chartViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chartViewer1.HotSpotCursor = System.Windows.Forms.Cursors.Hand;
            this.chartViewer1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chartViewer1.Location = new System.Drawing.Point(3, 3);
            this.chartViewer1.Name = "chartViewer1";
            this.chartViewer1.ScrollDirection = ChartDirector.WinChartDirection.HorizontalVertical;
            this.chartViewer1.Size = new System.Drawing.Size(1580, 194);
            this.chartViewer1.TabIndex = 26;
            this.chartViewer1.TabStop = false;
            this.chartViewer1.ZoomDirection = ChartDirector.WinChartDirection.HorizontalVertical;
            this.chartViewer1.ViewPortChanged += new ChartDirector.WinViewPortEventHandler(this.chartViewer1_ViewPortChanged);
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hScrollBar1.Location = new System.Drawing.Point(0, 1213);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(1586, 17);
            this.hScrollBar1.TabIndex = 32;
            this.hScrollBar1.ValueChanged += new System.EventHandler(this.hScrollBar1_ValueChanged);
            // 
            // cmbInterval
            // 
            this.cmbInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInterval.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbInterval.FormattingEnabled = true;
            this.cmbInterval.Items.AddRange(new object[] {
            "2",
            "4",
            "10",
            "20",
            "30",
            "60",
            "120"});
            this.cmbInterval.Location = new System.Drawing.Point(1209, 3);
            this.cmbInterval.Name = "cmbInterval";
            this.cmbInterval.Size = new System.Drawing.Size(74, 25);
            this.cmbInterval.TabIndex = 30;
            this.cmbInterval.SelectedIndexChanged += new System.EventHandler(this.cmbInterval_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(1069, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 20);
            this.label3.TabIndex = 31;
            this.label3.Text = "Interval (Sec): ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CycleProfile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Name = "CycleProfile";
            this.Size = new System.Drawing.Size(1607, 1234);
            this.Load += new System.EventHandler(this.CycleProfile_Load);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCycleDetails)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ChartsPanel.ResumeLayout(false);
            this.ChartsPanel.PerformLayout();
            this.tableLayoutPanelCharts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartViewer6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartViewer5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartViewer4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartViewer3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartViewer2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartViewer1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblAxis;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.ComboBox cmbMachineId;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.DataGridView dgvCycleDetails;
        private System.Windows.Forms.CheckBox chkShowMarkers;
        private System.Windows.Forms.CheckBox chkShowValues;
        private System.Windows.Forms.Panel ChartsPanel;
        private System.Windows.Forms.ComboBox cmbParameter;
        private System.Windows.Forms.Label lblParameter;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelCharts;
        private ChartDirector.WinChartViewer chartViewer6;
        private ChartDirector.WinChartViewer chartViewer5;
        private ChartDirector.WinChartViewer chartViewer4;
        private ChartDirector.WinChartViewer chartViewer3;
        private ChartDirector.WinChartViewer chartViewer2;
        private ChartDirector.WinChartViewer chartViewer1;
        private System.Windows.Forms.DataGridViewButtonColumn SelectCycle;
        private System.Windows.Forms.DataGridViewLinkColumn MachineModel;
        private System.Windows.Forms.DataGridViewLinkColumn IPAddress;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbInterval;
    }
}
