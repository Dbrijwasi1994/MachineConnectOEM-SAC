namespace MachineConnectApplication
{
    partial class HomeScreen
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HomeScreen));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.chartTblLayout = new System.Windows.Forms.TableLayoutPanel();
            this.topChartTblLayout = new System.Windows.Forms.TableLayoutPanel();
            this.pnlTimeAnalysis = new System.Windows.Forms.Panel();
            this.chartTimeAnalysis = new ChartDirector.WinChartViewer();
            this.tblPartsCountChart = new System.Windows.Forms.TableLayoutPanel();
            this.pnlPartsCountChart = new System.Windows.Forms.Panel();
            this.chartPartsCount = new ChartDirector.WinChartViewer();
            this.tblPartsCountPrev = new System.Windows.Forms.TableLayoutPanel();
            this.btnPartsCountPrev = new System.Windows.Forms.Button();
            this.tblPartsCountNext = new System.Windows.Forms.TableLayoutPanel();
            this.btnPartsCountNext = new System.Windows.Forms.Button();
            this.bottomChartTblLayout = new System.Windows.Forms.TableLayoutPanel();
            this.tblStopageGrid = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridStoppage = new System.Windows.Forms.DataGridView();
            this.lblStoppage = new System.Windows.Forms.Label();
            this.tblProductionAnalysis = new System.Windows.Forms.TableLayoutPanel();
            this.pnlProductionanalysis = new System.Windows.Forms.Panel();
            this.chartProductionanalysis = new ChartDirector.WinChartViewer();
            this.tblAnalysisNext = new System.Windows.Forms.TableLayoutPanel();
            this.btnAnalysisChartNext = new System.Windows.Forms.Button();
            this.tblAnalysisPrev = new System.Windows.Forms.TableLayoutPanel();
            this.btnAnalysisChartPrev = new System.Windows.Forms.Button();
            this.tblRunChart = new System.Windows.Forms.TableLayoutPanel();
            this.tblRunChartNext = new System.Windows.Forms.TableLayoutPanel();
            this.btnRunChartNext = new System.Windows.Forms.Button();
            this.tblRunChartPrev = new System.Windows.Forms.TableLayoutPanel();
            this.btnRunChartPrev = new System.Windows.Forms.Button();
            this.pnlRunchart = new System.Windows.Forms.Panel();
            this.analyticsPicturebox = new System.Windows.Forms.PictureBox();
            this.chartRunChart = new ChartDirector.WinChartViewer();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.BatchStart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BatchEnd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StopageTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chartTblLayout.SuspendLayout();
            this.topChartTblLayout.SuspendLayout();
            this.pnlTimeAnalysis.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartTimeAnalysis)).BeginInit();
            this.tblPartsCountChart.SuspendLayout();
            this.pnlPartsCountChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartPartsCount)).BeginInit();
            this.tblPartsCountPrev.SuspendLayout();
            this.tblPartsCountNext.SuspendLayout();
            this.bottomChartTblLayout.SuspendLayout();
            this.tblStopageGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridStoppage)).BeginInit();
            this.tblProductionAnalysis.SuspendLayout();
            this.pnlProductionanalysis.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartProductionanalysis)).BeginInit();
            this.tblAnalysisNext.SuspendLayout();
            this.tblAnalysisPrev.SuspendLayout();
            this.tblRunChart.SuspendLayout();
            this.tblRunChartNext.SuspendLayout();
            this.tblRunChartPrev.SuspendLayout();
            this.pnlRunchart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.analyticsPicturebox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartRunChart)).BeginInit();
            this.SuspendLayout();
            // 
            // chartTblLayout
            // 
            this.chartTblLayout.ColumnCount = 1;
            this.chartTblLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.chartTblLayout.Controls.Add(this.topChartTblLayout, 0, 0);
            this.chartTblLayout.Controls.Add(this.bottomChartTblLayout, 0, 2);
            this.chartTblLayout.Controls.Add(this.tblRunChart, 0, 1);
            this.chartTblLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartTblLayout.Location = new System.Drawing.Point(0, 0);
            this.chartTblLayout.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.chartTblLayout.Name = "chartTblLayout";
            this.chartTblLayout.RowCount = 3;
            this.chartTblLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.chartTblLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 136F));
            this.chartTblLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.chartTblLayout.Size = new System.Drawing.Size(1009, 550);
            this.chartTblLayout.TabIndex = 1;
            // 
            // topChartTblLayout
            // 
            this.topChartTblLayout.ColumnCount = 2;
            this.topChartTblLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.topChartTblLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.topChartTblLayout.Controls.Add(this.pnlTimeAnalysis, 0, 0);
            this.topChartTblLayout.Controls.Add(this.tblPartsCountChart, 0, 0);
            this.topChartTblLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topChartTblLayout.Location = new System.Drawing.Point(0, 0);
            this.topChartTblLayout.Margin = new System.Windows.Forms.Padding(0);
            this.topChartTblLayout.Name = "topChartTblLayout";
            this.topChartTblLayout.RowCount = 1;
            this.topChartTblLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.topChartTblLayout.Size = new System.Drawing.Size(1009, 207);
            this.topChartTblLayout.TabIndex = 0;
            // 
            // pnlTimeAnalysis
            // 
            this.pnlTimeAnalysis.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(208)))), ((int)(((byte)(223)))));
            this.pnlTimeAnalysis.Controls.Add(this.chartTimeAnalysis);
            this.pnlTimeAnalysis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTimeAnalysis.Location = new System.Drawing.Point(608, 3);
            this.pnlTimeAnalysis.Name = "pnlTimeAnalysis";
            this.pnlTimeAnalysis.Size = new System.Drawing.Size(398, 201);
            this.pnlTimeAnalysis.TabIndex = 65;
            // 
            // chartTimeAnalysis
            // 
            this.chartTimeAnalysis.BackColor = System.Drawing.Color.White;
            this.chartTimeAnalysis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartTimeAnalysis.Location = new System.Drawing.Point(0, 0);
            this.chartTimeAnalysis.Margin = new System.Windows.Forms.Padding(0);
            this.chartTimeAnalysis.MouseUsage = ChartDirector.WinChartMouseUsage.ScrollOnDrop;
            this.chartTimeAnalysis.Name = "chartTimeAnalysis";
            this.chartTimeAnalysis.Size = new System.Drawing.Size(398, 201);
            this.chartTimeAnalysis.TabIndex = 42;
            this.chartTimeAnalysis.TabStop = false;
            this.chartTimeAnalysis.MouseEnter += new System.EventHandler(this.chartTimeAnalysis_MouseEnter);
            // 
            // tblPartsCountChart
            // 
            this.tblPartsCountChart.ColumnCount = 3;
            this.tblPartsCountChart.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblPartsCountChart.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblPartsCountChart.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblPartsCountChart.Controls.Add(this.pnlPartsCountChart, 0, 0);
            this.tblPartsCountChart.Controls.Add(this.tblPartsCountPrev, 0, 0);
            this.tblPartsCountChart.Controls.Add(this.tblPartsCountNext, 2, 0);
            this.tblPartsCountChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblPartsCountChart.Location = new System.Drawing.Point(3, 3);
            this.tblPartsCountChart.Name = "tblPartsCountChart";
            this.tblPartsCountChart.RowCount = 1;
            this.tblPartsCountChart.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblPartsCountChart.Size = new System.Drawing.Size(599, 201);
            this.tblPartsCountChart.TabIndex = 0;
            // 
            // pnlPartsCountChart
            // 
            this.pnlPartsCountChart.Controls.Add(this.chartPartsCount);
            this.pnlPartsCountChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPartsCountChart.Location = new System.Drawing.Point(20, 0);
            this.pnlPartsCountChart.Margin = new System.Windows.Forms.Padding(0);
            this.pnlPartsCountChart.Name = "pnlPartsCountChart";
            this.pnlPartsCountChart.Size = new System.Drawing.Size(559, 201);
            this.pnlPartsCountChart.TabIndex = 65;
            // 
            // chartPartsCount
            // 
            this.chartPartsCount.BackColor = System.Drawing.Color.White;
            this.chartPartsCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartPartsCount.Location = new System.Drawing.Point(0, 0);
            this.chartPartsCount.Margin = new System.Windows.Forms.Padding(0);
            this.chartPartsCount.MouseUsage = ChartDirector.WinChartMouseUsage.ScrollOnDrop;
            this.chartPartsCount.Name = "chartPartsCount";
            this.chartPartsCount.Size = new System.Drawing.Size(559, 201);
            this.chartPartsCount.TabIndex = 42;
            this.chartPartsCount.TabStop = false;
            this.chartPartsCount.MouseEnter += new System.EventHandler(this.chartPartsCount_MouseHover);
            this.chartPartsCount.MouseLeave += new System.EventHandler(this.chartPartsCount_MouseLeave);
            // 
            // tblPartsCountPrev
            // 
            this.tblPartsCountPrev.BackColor = System.Drawing.Color.White;
            this.tblPartsCountPrev.ColumnCount = 1;
            this.tblPartsCountPrev.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblPartsCountPrev.Controls.Add(this.btnPartsCountPrev, 0, 0);
            this.tblPartsCountPrev.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblPartsCountPrev.Location = new System.Drawing.Point(0, 0);
            this.tblPartsCountPrev.Margin = new System.Windows.Forms.Padding(0);
            this.tblPartsCountPrev.Name = "tblPartsCountPrev";
            this.tblPartsCountPrev.RowCount = 1;
            this.tblPartsCountPrev.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblPartsCountPrev.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 201F));
            this.tblPartsCountPrev.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 201F));
            this.tblPartsCountPrev.Size = new System.Drawing.Size(20, 201);
            this.tblPartsCountPrev.TabIndex = 0;
            this.tblPartsCountPrev.MouseEnter += new System.EventHandler(this.chartPartsCount_MouseHover);
            // 
            // btnPartsCountPrev
            // 
            this.btnPartsCountPrev.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPartsCountPrev.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPartsCountPrev.BackgroundImage")));
            this.btnPartsCountPrev.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnPartsCountPrev.FlatAppearance.BorderSize = 0;
            this.btnPartsCountPrev.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnPartsCountPrev.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnPartsCountPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPartsCountPrev.Location = new System.Drawing.Point(0, 84);
            this.btnPartsCountPrev.Margin = new System.Windows.Forms.Padding(0);
            this.btnPartsCountPrev.Name = "btnPartsCountPrev";
            this.btnPartsCountPrev.Size = new System.Drawing.Size(20, 33);
            this.btnPartsCountPrev.TabIndex = 1;
            this.btnPartsCountPrev.UseVisualStyleBackColor = true;
            this.btnPartsCountPrev.Click += new System.EventHandler(this.btnPrev_Click);
            this.btnPartsCountPrev.MouseEnter += new System.EventHandler(this.chartPartsCount_MouseHover);
            // 
            // tblPartsCountNext
            // 
            this.tblPartsCountNext.BackColor = System.Drawing.Color.White;
            this.tblPartsCountNext.ColumnCount = 1;
            this.tblPartsCountNext.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblPartsCountNext.Controls.Add(this.btnPartsCountNext, 0, 0);
            this.tblPartsCountNext.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblPartsCountNext.Location = new System.Drawing.Point(579, 0);
            this.tblPartsCountNext.Margin = new System.Windows.Forms.Padding(0);
            this.tblPartsCountNext.Name = "tblPartsCountNext";
            this.tblPartsCountNext.RowCount = 1;
            this.tblPartsCountNext.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblPartsCountNext.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 201F));
            this.tblPartsCountNext.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 201F));
            this.tblPartsCountNext.Size = new System.Drawing.Size(20, 201);
            this.tblPartsCountNext.TabIndex = 1;
            this.tblPartsCountNext.MouseEnter += new System.EventHandler(this.chartPartsCount_MouseHover);
            // 
            // btnPartsCountNext
            // 
            this.btnPartsCountNext.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPartsCountNext.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPartsCountNext.BackgroundImage")));
            this.btnPartsCountNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPartsCountNext.FlatAppearance.BorderSize = 0;
            this.btnPartsCountNext.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnPartsCountNext.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnPartsCountNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPartsCountNext.Location = new System.Drawing.Point(0, 80);
            this.btnPartsCountNext.Margin = new System.Windows.Forms.Padding(0);
            this.btnPartsCountNext.Name = "btnPartsCountNext";
            this.btnPartsCountNext.Size = new System.Drawing.Size(20, 41);
            this.btnPartsCountNext.TabIndex = 0;
            this.btnPartsCountNext.UseVisualStyleBackColor = true;
            this.btnPartsCountNext.Click += new System.EventHandler(this.btnNext_Click);
            this.btnPartsCountNext.MouseEnter += new System.EventHandler(this.chartPartsCount_MouseHover);
            // 
            // bottomChartTblLayout
            // 
            this.bottomChartTblLayout.ColumnCount = 2;
            this.bottomChartTblLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.bottomChartTblLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.bottomChartTblLayout.Controls.Add(this.tblStopageGrid, 0, 0);
            this.bottomChartTblLayout.Controls.Add(this.tblProductionAnalysis, 0, 0);
            this.bottomChartTblLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottomChartTblLayout.Location = new System.Drawing.Point(0, 343);
            this.bottomChartTblLayout.Margin = new System.Windows.Forms.Padding(0);
            this.bottomChartTblLayout.Name = "bottomChartTblLayout";
            this.bottomChartTblLayout.RowCount = 1;
            this.bottomChartTblLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.bottomChartTblLayout.Size = new System.Drawing.Size(1009, 207);
            this.bottomChartTblLayout.TabIndex = 1;
            // 
            // tblStopageGrid
            // 
            this.tblStopageGrid.ColumnCount = 1;
            this.tblStopageGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblStopageGrid.Controls.Add(this.dataGridStoppage, 0, 1);
            this.tblStopageGrid.Controls.Add(this.lblStoppage, 0, 0);
            this.tblStopageGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblStopageGrid.Location = new System.Drawing.Point(608, 3);
            this.tblStopageGrid.Name = "tblStopageGrid";
            this.tblStopageGrid.RowCount = 2;
            this.tblStopageGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tblStopageGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblStopageGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblStopageGrid.Size = new System.Drawing.Size(398, 201);
            this.tblStopageGrid.TabIndex = 64;
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
            this.dataGridStoppage.BackgroundColor = System.Drawing.Color.White;
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
            this.BatchStart,
            this.BatchEnd,
            this.StopageTime});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(208)))), ((int)(((byte)(160)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridStoppage.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridStoppage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridStoppage.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridStoppage.EnableHeadersVisualStyles = false;
            this.dataGridStoppage.GridColor = System.Drawing.Color.Silver;
            this.dataGridStoppage.Location = new System.Drawing.Point(1, 27);
            this.dataGridStoppage.Margin = new System.Windows.Forms.Padding(1);
            this.dataGridStoppage.Name = "dataGridStoppage";
            this.dataGridStoppage.ReadOnly = true;
            this.dataGridStoppage.RowHeadersVisible = false;
            this.dataGridStoppage.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridStoppage.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dataGridStoppage.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(208)))), ((int)(((byte)(160)))));
            this.dataGridStoppage.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridStoppage.RowTemplate.Height = 30;
            this.dataGridStoppage.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridStoppage.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridStoppage.Size = new System.Drawing.Size(396, 173);
            this.dataGridStoppage.TabIndex = 13;
            this.dataGridStoppage.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridStoppage_DataBindingComplete);
            this.dataGridStoppage.SelectionChanged += new System.EventHandler(this.dataGridStoppage_SelectionChanged);
            // 
            // lblStoppage
            // 
            this.lblStoppage.AutoSize = true;
            this.lblStoppage.BackColor = System.Drawing.Color.White;
            this.lblStoppage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStoppage.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStoppage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(88)))), ((int)(((byte)(163)))));
            this.lblStoppage.Location = new System.Drawing.Point(1, 0);
            this.lblStoppage.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.lblStoppage.Name = "lblStoppage";
            this.lblStoppage.Size = new System.Drawing.Size(396, 26);
            this.lblStoppage.TabIndex = 15;
            this.lblStoppage.Text = "Stoppages";
            this.lblStoppage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tblProductionAnalysis
            // 
            this.tblProductionAnalysis.ColumnCount = 3;
            this.tblProductionAnalysis.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblProductionAnalysis.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblProductionAnalysis.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblProductionAnalysis.Controls.Add(this.pnlProductionanalysis, 1, 0);
            this.tblProductionAnalysis.Controls.Add(this.tblAnalysisNext, 2, 0);
            this.tblProductionAnalysis.Controls.Add(this.tblAnalysisPrev, 0, 0);
            this.tblProductionAnalysis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblProductionAnalysis.Location = new System.Drawing.Point(3, 3);
            this.tblProductionAnalysis.Margin = new System.Windows.Forms.Padding(3, 3, 2, 3);
            this.tblProductionAnalysis.Name = "tblProductionAnalysis";
            this.tblProductionAnalysis.RowCount = 1;
            this.tblProductionAnalysis.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblProductionAnalysis.Size = new System.Drawing.Size(600, 201);
            this.tblProductionAnalysis.TabIndex = 1;
            // 
            // pnlProductionanalysis
            // 
            this.pnlProductionanalysis.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(208)))), ((int)(((byte)(223)))));
            this.pnlProductionanalysis.Controls.Add(this.chartProductionanalysis);
            this.pnlProductionanalysis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlProductionanalysis.Location = new System.Drawing.Point(20, 0);
            this.pnlProductionanalysis.Margin = new System.Windows.Forms.Padding(0);
            this.pnlProductionanalysis.Name = "pnlProductionanalysis";
            this.pnlProductionanalysis.Size = new System.Drawing.Size(560, 201);
            this.pnlProductionanalysis.TabIndex = 65;
            // 
            // chartProductionanalysis
            // 
            this.chartProductionanalysis.BackColor = System.Drawing.Color.White;
            this.chartProductionanalysis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartProductionanalysis.Location = new System.Drawing.Point(0, 0);
            this.chartProductionanalysis.Margin = new System.Windows.Forms.Padding(0);
            this.chartProductionanalysis.MouseUsage = ChartDirector.WinChartMouseUsage.ScrollOnDrop;
            this.chartProductionanalysis.Name = "chartProductionanalysis";
            this.chartProductionanalysis.Size = new System.Drawing.Size(560, 201);
            this.chartProductionanalysis.TabIndex = 42;
            this.chartProductionanalysis.TabStop = false;
            this.chartProductionanalysis.MouseEnter += new System.EventHandler(this.chartProductionanalysis_MouseHover);
            this.chartProductionanalysis.MouseLeave += new System.EventHandler(this.chartProductionanalysis_MouseLeave);
            // 
            // tblAnalysisNext
            // 
            this.tblAnalysisNext.BackColor = System.Drawing.Color.White;
            this.tblAnalysisNext.ColumnCount = 1;
            this.tblAnalysisNext.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblAnalysisNext.Controls.Add(this.btnAnalysisChartNext, 0, 0);
            this.tblAnalysisNext.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblAnalysisNext.Location = new System.Drawing.Point(580, 0);
            this.tblAnalysisNext.Margin = new System.Windows.Forms.Padding(0);
            this.tblAnalysisNext.Name = "tblAnalysisNext";
            this.tblAnalysisNext.RowCount = 1;
            this.tblAnalysisNext.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblAnalysisNext.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 201F));
            this.tblAnalysisNext.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 201F));
            this.tblAnalysisNext.Size = new System.Drawing.Size(20, 201);
            this.tblAnalysisNext.TabIndex = 3;
            this.tblAnalysisNext.MouseEnter += new System.EventHandler(this.chartProductionanalysis_MouseHover);
            // 
            // btnAnalysisChartNext
            // 
            this.btnAnalysisChartNext.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAnalysisChartNext.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAnalysisChartNext.BackgroundImage")));
            this.btnAnalysisChartNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAnalysisChartNext.FlatAppearance.BorderSize = 0;
            this.btnAnalysisChartNext.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnAnalysisChartNext.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnAnalysisChartNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnalysisChartNext.Location = new System.Drawing.Point(0, 83);
            this.btnAnalysisChartNext.Margin = new System.Windows.Forms.Padding(0);
            this.btnAnalysisChartNext.Name = "btnAnalysisChartNext";
            this.btnAnalysisChartNext.Size = new System.Drawing.Size(20, 34);
            this.btnAnalysisChartNext.TabIndex = 1;
            this.btnAnalysisChartNext.UseVisualStyleBackColor = true;
            this.btnAnalysisChartNext.Click += new System.EventHandler(this.btnNext_Click);
            this.btnAnalysisChartNext.MouseEnter += new System.EventHandler(this.chartProductionanalysis_MouseHover);
            // 
            // tblAnalysisPrev
            // 
            this.tblAnalysisPrev.BackColor = System.Drawing.Color.White;
            this.tblAnalysisPrev.ColumnCount = 1;
            this.tblAnalysisPrev.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblAnalysisPrev.Controls.Add(this.btnAnalysisChartPrev, 0, 0);
            this.tblAnalysisPrev.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblAnalysisPrev.Location = new System.Drawing.Point(0, 0);
            this.tblAnalysisPrev.Margin = new System.Windows.Forms.Padding(0);
            this.tblAnalysisPrev.Name = "tblAnalysisPrev";
            this.tblAnalysisPrev.RowCount = 1;
            this.tblAnalysisPrev.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblAnalysisPrev.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 201F));
            this.tblAnalysisPrev.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 201F));
            this.tblAnalysisPrev.Size = new System.Drawing.Size(20, 201);
            this.tblAnalysisPrev.TabIndex = 2;
            this.tblAnalysisPrev.MouseEnter += new System.EventHandler(this.chartProductionanalysis_MouseHover);
            // 
            // btnAnalysisChartPrev
            // 
            this.btnAnalysisChartPrev.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAnalysisChartPrev.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAnalysisChartPrev.BackgroundImage")));
            this.btnAnalysisChartPrev.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnAnalysisChartPrev.FlatAppearance.BorderSize = 0;
            this.btnAnalysisChartPrev.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnAnalysisChartPrev.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnAnalysisChartPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnalysisChartPrev.Location = new System.Drawing.Point(0, 83);
            this.btnAnalysisChartPrev.Margin = new System.Windows.Forms.Padding(0);
            this.btnAnalysisChartPrev.Name = "btnAnalysisChartPrev";
            this.btnAnalysisChartPrev.Size = new System.Drawing.Size(20, 34);
            this.btnAnalysisChartPrev.TabIndex = 2;
            this.btnAnalysisChartPrev.UseVisualStyleBackColor = true;
            this.btnAnalysisChartPrev.Click += new System.EventHandler(this.btnPrev_Click);
            this.btnAnalysisChartPrev.MouseEnter += new System.EventHandler(this.chartProductionanalysis_MouseHover);
            // 
            // tblRunChart
            // 
            this.tblRunChart.ColumnCount = 3;
            this.tblRunChart.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tblRunChart.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblRunChart.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tblRunChart.Controls.Add(this.tblRunChartNext, 2, 0);
            this.tblRunChart.Controls.Add(this.tblRunChartPrev, 0, 0);
            this.tblRunChart.Controls.Add(this.pnlRunchart, 1, 0);
            this.tblRunChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblRunChart.Location = new System.Drawing.Point(3, 210);
            this.tblRunChart.Name = "tblRunChart";
            this.tblRunChart.RowCount = 1;
            this.tblRunChart.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblRunChart.Size = new System.Drawing.Size(1003, 130);
            this.tblRunChart.TabIndex = 2;
            // 
            // tblRunChartNext
            // 
            this.tblRunChartNext.BackColor = System.Drawing.Color.White;
            this.tblRunChartNext.ColumnCount = 1;
            this.tblRunChartNext.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblRunChartNext.Controls.Add(this.btnRunChartNext, 0, 1);
            this.tblRunChartNext.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblRunChartNext.Location = new System.Drawing.Point(981, 0);
            this.tblRunChartNext.Margin = new System.Windows.Forms.Padding(0);
            this.tblRunChartNext.Name = "tblRunChartNext";
            this.tblRunChartNext.RowCount = 3;
            this.tblRunChartNext.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblRunChartNext.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tblRunChartNext.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblRunChartNext.Size = new System.Drawing.Size(22, 130);
            this.tblRunChartNext.TabIndex = 68;
            this.tblRunChartNext.MouseEnter += new System.EventHandler(this.chartRunChart_MouseHover);
            // 
            // btnRunChartNext
            // 
            this.btnRunChartNext.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnRunChartNext.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRunChartNext.BackgroundImage")));
            this.btnRunChartNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnRunChartNext.FlatAppearance.BorderSize = 0;
            this.btnRunChartNext.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnRunChartNext.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnRunChartNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRunChartNext.Location = new System.Drawing.Point(1, 44);
            this.btnRunChartNext.Margin = new System.Windows.Forms.Padding(0);
            this.btnRunChartNext.Name = "btnRunChartNext";
            this.btnRunChartNext.Size = new System.Drawing.Size(20, 41);
            this.btnRunChartNext.TabIndex = 1;
            this.btnRunChartNext.UseVisualStyleBackColor = true;
            this.btnRunChartNext.Click += new System.EventHandler(this.btnRunChartNext_Click);
            // 
            // tblRunChartPrev
            // 
            this.tblRunChartPrev.BackColor = System.Drawing.Color.White;
            this.tblRunChartPrev.ColumnCount = 1;
            this.tblRunChartPrev.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblRunChartPrev.Controls.Add(this.btnRunChartPrev, 0, 1);
            this.tblRunChartPrev.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblRunChartPrev.Location = new System.Drawing.Point(0, 0);
            this.tblRunChartPrev.Margin = new System.Windows.Forms.Padding(0);
            this.tblRunChartPrev.Name = "tblRunChartPrev";
            this.tblRunChartPrev.RowCount = 3;
            this.tblRunChartPrev.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblRunChartPrev.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tblRunChartPrev.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblRunChartPrev.Size = new System.Drawing.Size(22, 130);
            this.tblRunChartPrev.TabIndex = 67;
            this.tblRunChartPrev.MouseEnter += new System.EventHandler(this.chartRunChart_MouseHover);
            // 
            // btnRunChartPrev
            // 
            this.btnRunChartPrev.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRunChartPrev.BackgroundImage")));
            this.btnRunChartPrev.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnRunChartPrev.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRunChartPrev.FlatAppearance.BorderSize = 0;
            this.btnRunChartPrev.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnRunChartPrev.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnRunChartPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRunChartPrev.Location = new System.Drawing.Point(0, 40);
            this.btnRunChartPrev.Margin = new System.Windows.Forms.Padding(0);
            this.btnRunChartPrev.Name = "btnRunChartPrev";
            this.btnRunChartPrev.Size = new System.Drawing.Size(22, 50);
            this.btnRunChartPrev.TabIndex = 2;
            this.btnRunChartPrev.UseVisualStyleBackColor = true;
            this.btnRunChartPrev.Click += new System.EventHandler(this.btnRunChartPrev_Click);
            // 
            // pnlRunchart
            // 
            this.pnlRunchart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(208)))), ((int)(((byte)(223)))));
            this.pnlRunchart.Controls.Add(this.analyticsPicturebox);
            this.pnlRunchart.Controls.Add(this.chartRunChart);
            this.pnlRunchart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRunchart.Location = new System.Drawing.Point(22, 0);
            this.pnlRunchart.Margin = new System.Windows.Forms.Padding(0);
            this.pnlRunchart.Name = "pnlRunchart";
            this.pnlRunchart.Size = new System.Drawing.Size(959, 130);
            this.pnlRunchart.TabIndex = 66;
            // 
            // analyticsPicturebox
            // 
            this.analyticsPicturebox.BackColor = System.Drawing.Color.White;
            this.analyticsPicturebox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("analyticsPicturebox.BackgroundImage")));
            this.analyticsPicturebox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.analyticsPicturebox.Location = new System.Drawing.Point(43, 0);
            this.analyticsPicturebox.Margin = new System.Windows.Forms.Padding(0);
            this.analyticsPicturebox.Name = "analyticsPicturebox";
            this.analyticsPicturebox.Size = new System.Drawing.Size(272, 22);
            this.analyticsPicturebox.TabIndex = 64;
            this.analyticsPicturebox.TabStop = false;
            // 
            // chartRunChart
            // 
            this.chartRunChart.BackColor = System.Drawing.Color.White;
            this.chartRunChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartRunChart.Location = new System.Drawing.Point(0, 0);
            this.chartRunChart.Margin = new System.Windows.Forms.Padding(0);
            this.chartRunChart.MouseUsage = ChartDirector.WinChartMouseUsage.ScrollOnDrop;
            this.chartRunChart.Name = "chartRunChart";
            this.chartRunChart.Size = new System.Drawing.Size(959, 130);
            this.chartRunChart.TabIndex = 42;
            this.chartRunChart.TabStop = false;
            this.chartRunChart.MouseEnter += new System.EventHandler(this.chartRunChart_MouseEnter);
            this.chartRunChart.MouseLeave += new System.EventHandler(this.chartRunChart_MouseLeave);
            this.chartRunChart.MouseHover += new System.EventHandler(this.chartRunChart_MouseHover);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // BatchStart
            // 
            this.BatchStart.DataPropertyName = "BatchStart";
            dataGridViewCellStyle3.Format = "dd-MM-yyyy hh:mm tt";
            this.BatchStart.DefaultCellStyle = dataGridViewCellStyle3;
            this.BatchStart.HeaderText = "Start Time";
            this.BatchStart.MinimumWidth = 180;
            this.BatchStart.Name = "BatchStart";
            this.BatchStart.ReadOnly = true;
            this.BatchStart.Width = 180;
            // 
            // BatchEnd
            // 
            this.BatchEnd.DataPropertyName = "BatchEnd";
            dataGridViewCellStyle4.Format = "dd-MM-yyyy hh:mm tt";
            this.BatchEnd.DefaultCellStyle = dataGridViewCellStyle4;
            this.BatchEnd.HeaderText = "End Time";
            this.BatchEnd.MinimumWidth = 180;
            this.BatchEnd.Name = "BatchEnd";
            this.BatchEnd.ReadOnly = true;
            this.BatchEnd.Width = 180;
            // 
            // StopageTime
            // 
            this.StopageTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.StopageTime.DataPropertyName = "StoppageTime";
            dataGridViewCellStyle5.Format = "hh:mm:ss";
            this.StopageTime.DefaultCellStyle = dataGridViewCellStyle5;
            this.StopageTime.HeaderText = "Duration (hh:mm:ss)";
            this.StopageTime.MinimumWidth = 50;
            this.StopageTime.Name = "StopageTime";
            this.StopageTime.ReadOnly = true;
            // 
            // HomeScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chartTblLayout);
            this.Name = "HomeScreen";
            this.Size = new System.Drawing.Size(1009, 550);
            this.Load += new System.EventHandler(this.HomeScreen_Load);
            this.chartTblLayout.ResumeLayout(false);
            this.topChartTblLayout.ResumeLayout(false);
            this.pnlTimeAnalysis.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartTimeAnalysis)).EndInit();
            this.tblPartsCountChart.ResumeLayout(false);
            this.pnlPartsCountChart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartPartsCount)).EndInit();
            this.tblPartsCountPrev.ResumeLayout(false);
            this.tblPartsCountNext.ResumeLayout(false);
            this.bottomChartTblLayout.ResumeLayout(false);
            this.tblStopageGrid.ResumeLayout(false);
            this.tblStopageGrid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridStoppage)).EndInit();
            this.tblProductionAnalysis.ResumeLayout(false);
            this.pnlProductionanalysis.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartProductionanalysis)).EndInit();
            this.tblAnalysisNext.ResumeLayout(false);
            this.tblAnalysisPrev.ResumeLayout(false);
            this.tblRunChart.ResumeLayout(false);
            this.tblRunChartNext.ResumeLayout(false);
            this.tblRunChartPrev.ResumeLayout(false);
            this.pnlRunchart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.analyticsPicturebox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartRunChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel chartTblLayout;
        private System.Windows.Forms.TableLayoutPanel topChartTblLayout;
        private System.Windows.Forms.TableLayoutPanel tblPartsCountChart;
        private System.Windows.Forms.Panel pnlPartsCountChart;
        private ChartDirector.WinChartViewer chartPartsCount;
        private System.Windows.Forms.TableLayoutPanel tblPartsCountPrev;
        private System.Windows.Forms.Button btnPartsCountPrev;
        private System.Windows.Forms.TableLayoutPanel tblPartsCountNext;
        private System.Windows.Forms.Button btnPartsCountNext;
        private System.Windows.Forms.TableLayoutPanel bottomChartTblLayout;
        private System.Windows.Forms.TableLayoutPanel tblProductionAnalysis;
        private System.Windows.Forms.Panel pnlProductionanalysis;
        private ChartDirector.WinChartViewer chartProductionanalysis;
        private System.Windows.Forms.TableLayoutPanel tblAnalysisNext;
        private System.Windows.Forms.Button btnAnalysisChartNext;
        private System.Windows.Forms.TableLayoutPanel tblAnalysisPrev;
        private System.Windows.Forms.Button btnAnalysisChartPrev;
        private System.Windows.Forms.TableLayoutPanel tblRunChart;
        private System.Windows.Forms.TableLayoutPanel tblRunChartPrev;
        private System.Windows.Forms.Button btnRunChartPrev;
        private System.Windows.Forms.Panel pnlRunchart;
        private ChartDirector.WinChartViewer chartRunChart;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TableLayoutPanel tblStopageGrid;
        private System.Windows.Forms.DataGridView dataGridStoppage;
        private System.Windows.Forms.Label lblStoppage;
        private System.Windows.Forms.Panel pnlTimeAnalysis;
        private System.Windows.Forms.PictureBox analyticsPicturebox;
        private System.Windows.Forms.TableLayoutPanel tblRunChartNext;
        private System.Windows.Forms.Button btnRunChartNext;
        private ChartDirector.WinChartViewer chartTimeAnalysis;
        private System.Windows.Forms.DataGridViewTextBoxColumn BatchStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn BatchEnd;
        private System.Windows.Forms.DataGridViewTextBoxColumn StopageTime;
    }
}
