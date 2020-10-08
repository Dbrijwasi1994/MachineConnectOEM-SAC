namespace MachineConnectApplication
{
    partial class RPM
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RPM));
            this.mainTbl = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.winChartViewer3 = new ChartDirector.WinChartViewer();
            this.hScrollBar3 = new System.Windows.Forms.HScrollBar();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBoxSpindleLegend1 = new System.Windows.Forms.PictureBox();
            this.winChartViewer2 = new ChartDirector.WinChartViewer();
            this.hScrollBar2 = new System.Windows.Forms.HScrollBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBoxSpindleLegend = new System.Windows.Forms.PictureBox();
            this.winChartViewer1 = new ChartDirector.WinChartViewer();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCycleProfile = new System.Windows.Forms.Button();
            this.cmbDuration = new System.Windows.Forms.ComboBox();
            this.cmbDurationType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblParameter = new System.Windows.Forms.Label();
            this.cmbParameter = new System.Windows.Forms.ComboBox();
            this.lblScrollRange = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.mainTbl.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.winChartViewer3)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSpindleLegend1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.winChartViewer2)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSpindleLegend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.winChartViewer1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTbl
            // 
            this.mainTbl.BackColor = System.Drawing.SystemColors.Control;
            this.mainTbl.ColumnCount = 1;
            this.mainTbl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTbl.Controls.Add(this.panel3, 0, 3);
            this.mainTbl.Controls.Add(this.panel2, 0, 2);
            this.mainTbl.Controls.Add(this.panel1, 0, 1);
            this.mainTbl.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.mainTbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTbl.Location = new System.Drawing.Point(0, 0);
            this.mainTbl.Name = "mainTbl";
            this.mainTbl.RowCount = 4;
            this.mainTbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.mainTbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.mainTbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.mainTbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.mainTbl.Size = new System.Drawing.Size(1504, 704);
            this.mainTbl.TabIndex = 14;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.winChartViewer3);
            this.panel3.Controls.Add(this.hScrollBar3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 482);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1498, 219);
            this.panel3.TabIndex = 14;
            // 
            // winChartViewer3
            // 
            this.winChartViewer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winChartViewer3.HotSpotCursor = System.Windows.Forms.Cursors.Hand;
            this.winChartViewer3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.winChartViewer3.Location = new System.Drawing.Point(0, 0);
            this.winChartViewer3.Name = "winChartViewer3";
            this.winChartViewer3.ScrollDirection = ChartDirector.WinChartDirection.HorizontalVertical;
            this.winChartViewer3.Size = new System.Drawing.Size(1498, 205);
            this.winChartViewer3.TabIndex = 25;
            this.winChartViewer3.TabStop = false;
            this.winChartViewer3.ZoomDirection = ChartDirector.WinChartDirection.HorizontalVertical;
            this.winChartViewer3.ViewPortChanged += new ChartDirector.WinViewPortEventHandler(this.winChartViewer3_ViewPortChanged);
            this.winChartViewer3.MouseEnter += new System.EventHandler(this.winChartViewer3_MouseEnter);
            // 
            // hScrollBar3
            // 
            this.hScrollBar3.BackColor = System.Drawing.Color.White;
            this.hScrollBar3.Cursor = System.Windows.Forms.Cursors.Default;
            this.hScrollBar3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hScrollBar3.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.hScrollBar3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.hScrollBar3.Location = new System.Drawing.Point(0, 205);
            this.hScrollBar3.Maximum = 1000000000;
            this.hScrollBar3.Name = "hScrollBar3";
            this.hScrollBar3.Size = new System.Drawing.Size(1498, 14);
            this.hScrollBar3.TabIndex = 22;
            this.hScrollBar3.ValueChanged += new System.EventHandler(this.hScrollBar3_ValueChanged_1);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.pictureBoxSpindleLegend1);
            this.panel2.Controls.Add(this.winChartViewer2);
            this.panel2.Controls.Add(this.hScrollBar2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 258);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1498, 218);
            this.panel2.TabIndex = 13;
            // 
            // pictureBoxSpindleLegend1
            // 
            this.pictureBoxSpindleLegend1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxSpindleLegend1.Image")));
            this.pictureBoxSpindleLegend1.Location = new System.Drawing.Point(66, 3);
            this.pictureBoxSpindleLegend1.Name = "pictureBoxSpindleLegend1";
            this.pictureBoxSpindleLegend1.Size = new System.Drawing.Size(220, 21);
            this.pictureBoxSpindleLegend1.TabIndex = 27;
            this.pictureBoxSpindleLegend1.TabStop = false;
            // 
            // winChartViewer2
            // 
            this.winChartViewer2.BackColor = System.Drawing.Color.Transparent;
            this.winChartViewer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winChartViewer2.HotSpotCursor = System.Windows.Forms.Cursors.Hand;
            this.winChartViewer2.Location = new System.Drawing.Point(0, 0);
            this.winChartViewer2.Name = "winChartViewer2";
            this.winChartViewer2.ScrollDirection = ChartDirector.WinChartDirection.HorizontalVertical;
            this.winChartViewer2.Size = new System.Drawing.Size(1498, 208);
            this.winChartViewer2.TabIndex = 25;
            this.winChartViewer2.TabStop = false;
            this.winChartViewer2.ZoomDirection = ChartDirector.WinChartDirection.HorizontalVertical;
            this.winChartViewer2.ViewPortChanged += new ChartDirector.WinViewPortEventHandler(this.winChartViewer2_ViewPortChanged);
            this.winChartViewer2.MouseEnter += new System.EventHandler(this.winChartViewer2_MouseEnter);
            // 
            // hScrollBar2
            // 
            this.hScrollBar2.BackColor = System.Drawing.Color.White;
            this.hScrollBar2.Cursor = System.Windows.Forms.Cursors.Default;
            this.hScrollBar2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hScrollBar2.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.hScrollBar2.Location = new System.Drawing.Point(0, 208);
            this.hScrollBar2.Maximum = 1000000000;
            this.hScrollBar2.Name = "hScrollBar2";
            this.hScrollBar2.Size = new System.Drawing.Size(1498, 10);
            this.hScrollBar2.TabIndex = 22;
            this.hScrollBar2.Visible = false;
            this.hScrollBar2.ValueChanged += new System.EventHandler(this.hScrollBar2_ValueChanged_1);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.pictureBoxSpindleLegend);
            this.panel1.Controls.Add(this.winChartViewer1);
            this.panel1.Controls.Add(this.hScrollBar1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 37);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1498, 215);
            this.panel1.TabIndex = 3;
            // 
            // pictureBoxSpindleLegend
            // 
            this.pictureBoxSpindleLegend.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxSpindleLegend.Image")));
            this.pictureBoxSpindleLegend.Location = new System.Drawing.Point(66, 3);
            this.pictureBoxSpindleLegend.Name = "pictureBoxSpindleLegend";
            this.pictureBoxSpindleLegend.Size = new System.Drawing.Size(220, 21);
            this.pictureBoxSpindleLegend.TabIndex = 26;
            this.pictureBoxSpindleLegend.TabStop = false;
            // 
            // winChartViewer1
            // 
            this.winChartViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winChartViewer1.HotSpotCursor = System.Windows.Forms.Cursors.Hand;
            this.winChartViewer1.Location = new System.Drawing.Point(0, 0);
            this.winChartViewer1.Name = "winChartViewer1";
            this.winChartViewer1.ScrollDirection = ChartDirector.WinChartDirection.HorizontalVertical;
            this.winChartViewer1.Size = new System.Drawing.Size(1498, 205);
            this.winChartViewer1.TabIndex = 25;
            this.winChartViewer1.TabStop = false;
            this.winChartViewer1.ZoomDirection = ChartDirector.WinChartDirection.HorizontalVertical;
            this.winChartViewer1.ViewPortChanged += new ChartDirector.WinViewPortEventHandler(this.winChartViewer1_ViewPortChanged);
            this.winChartViewer1.MouseEnter += new System.EventHandler(this.winChartViewer1_MouseEnter);
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.BackColor = System.Drawing.Color.White;
            this.hScrollBar1.Cursor = System.Windows.Forms.Cursors.Default;
            this.hScrollBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hScrollBar1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.hScrollBar1.Location = new System.Drawing.Point(0, 205);
            this.hScrollBar1.Maximum = 1000000000;
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(1498, 10);
            this.hScrollBar1.TabIndex = 22;
            this.hScrollBar1.Visible = false;
            this.hScrollBar1.ValueChanged += new System.EventHandler(this.hScrollBar1_ValueChanged_1);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 9;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 190F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 76F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 104F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tableLayoutPanel1.Controls.Add(this.btnCycleProfile, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbDuration, 8, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbDurationType, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblParameter, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbParameter, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblScrollRange, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.dtpStartDate, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1504, 31);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // btnCycleProfile
            // 
            this.btnCycleProfile.BackColor = System.Drawing.SystemColors.Control;
            this.btnCycleProfile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCycleProfile.FlatAppearance.BorderColor = System.Drawing.Color.OliveDrab;
            this.btnCycleProfile.FlatAppearance.BorderSize = 2;
            this.btnCycleProfile.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnCycleProfile.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCycleProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCycleProfile.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCycleProfile.ForeColor = System.Drawing.Color.Navy;
            this.btnCycleProfile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCycleProfile.Location = new System.Drawing.Point(0, 0);
            this.btnCycleProfile.Margin = new System.Windows.Forms.Padding(0);
            this.btnCycleProfile.Name = "btnCycleProfile";
            this.btnCycleProfile.Size = new System.Drawing.Size(190, 31);
            this.btnCycleProfile.TabIndex = 59;
            this.btnCycleProfile.Text = "CYCLE PROFILE";
            this.btnCycleProfile.UseVisualStyleBackColor = false;
            this.btnCycleProfile.Click += new System.EventHandler(this.btnCycleProfile_Click);
            // 
            // cmbDuration
            // 
            this.cmbDuration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDuration.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDuration.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDuration.FormattingEnabled = true;
            this.cmbDuration.Location = new System.Drawing.Point(1442, 3);
            this.cmbDuration.Name = "cmbDuration";
            this.cmbDuration.Size = new System.Drawing.Size(59, 25);
            this.cmbDuration.TabIndex = 16;
            this.cmbDuration.SelectionChangeCommitted += new System.EventHandler(this.cmbDuration_SelectionChangeCommitted_1);
            // 
            // cmbDurationType
            // 
            this.cmbDurationType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDurationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDurationType.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDurationType.FormattingEnabled = true;
            this.cmbDurationType.Items.AddRange(new object[] {
            "Hour",
            "Minute",
            "Seconds"});
            this.cmbDurationType.Location = new System.Drawing.Point(1338, 3);
            this.cmbDurationType.Name = "cmbDurationType";
            this.cmbDurationType.Size = new System.Drawing.Size(98, 25);
            this.cmbDurationType.TabIndex = 15;
            this.cmbDurationType.SelectionChangeCommitted += new System.EventHandler(this.cmbDurationType_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(1262, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 20);
            this.label1.TabIndex = 17;
            this.label1.Text = "Duration";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblParameter
            // 
            this.lblParameter.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblParameter.AutoSize = true;
            this.lblParameter.BackColor = System.Drawing.Color.White;
            this.lblParameter.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblParameter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblParameter.Location = new System.Drawing.Point(1052, 5);
            this.lblParameter.Name = "lblParameter";
            this.lblParameter.Size = new System.Drawing.Size(80, 20);
            this.lblParameter.TabIndex = 18;
            this.lblParameter.Text = "Parameter";
            this.lblParameter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbParameter
            // 
            this.cmbParameter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbParameter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbParameter.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbParameter.FormattingEnabled = true;
            this.cmbParameter.Items.AddRange(new object[] {
            "Temperature",
            "Load",
            "FeedRate"});
            this.cmbParameter.Location = new System.Drawing.Point(1142, 3);
            this.cmbParameter.Name = "cmbParameter";
            this.cmbParameter.Size = new System.Drawing.Size(114, 25);
            this.cmbParameter.TabIndex = 19;
            this.cmbParameter.SelectionChangeCommitted += new System.EventHandler(this.cmbAxis_SelectionChangeCommitted);
            // 
            // lblScrollRange
            // 
            this.lblScrollRange.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblScrollRange.AutoSize = true;
            this.lblScrollRange.BackColor = System.Drawing.Color.White;
            this.lblScrollRange.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScrollRange.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblScrollRange.Location = new System.Drawing.Point(495, 5);
            this.lblScrollRange.Name = "lblScrollRange";
            this.lblScrollRange.Size = new System.Drawing.Size(48, 20);
            this.lblScrollRange.TabIndex = 16;
            this.lblScrollRange.Text = "label1";
            this.lblScrollRange.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CalendarFont = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStartDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpStartDate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(942, 3);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(104, 25);
            this.dtpStartDate.TabIndex = 61;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(852, 5);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 20);
            this.label2.TabIndex = 60;
            this.label2.Text = "From Date";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.mainTbl);
            this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(1504, 704);
            this.pnlContainer.TabIndex = 15;
            // 
            // RPM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlContainer);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "RPM";
            this.Size = new System.Drawing.Size(1504, 704);
            this.Load += new System.EventHandler(this.RPM_Load);
            this.mainTbl.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.winChartViewer3)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSpindleLegend1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.winChartViewer2)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSpindleLegend)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.winChartViewer1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.pnlContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainTbl;
        private System.Windows.Forms.Panel panel3;
        private ChartDirector.WinChartViewer winChartViewer3;
        private System.Windows.Forms.HScrollBar hScrollBar3;       
        private System.Windows.Forms.Panel panel2;
        private ChartDirector.WinChartViewer winChartViewer2;
        private System.Windows.Forms.HScrollBar hScrollBar2;
        private System.Windows.Forms.Panel panel1;
        private ChartDirector.WinChartViewer winChartViewer1;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblScrollRange;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ComboBox cmbDuration;
        private System.Windows.Forms.ComboBox cmbDurationType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblParameter;
        private System.Windows.Forms.ComboBox cmbParameter;
        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.PictureBox pictureBoxSpindleLegend;
        private System.Windows.Forms.PictureBox pictureBoxSpindleLegend1;
        private System.Windows.Forms.Button btnCycleProfile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
    }
}