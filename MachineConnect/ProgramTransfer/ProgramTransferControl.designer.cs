namespace CNC_PT
{
    partial class ProgramTransferControl
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Programs folder");
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgramTransferControl));
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("SYSYEM");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("MTB1");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("MTB2");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("LIBRARY");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("PATH1");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("PATH2");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("USER", new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode6,
            treeNode7});
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("CNC_MEM", new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode8});
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.listsysPrograms = new System.Windows.Forms.ListBox();
            this.treeSystm = new System.Windows.Forms.TreeView();
            this.dgvProgramDetails = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Program = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Modifiedon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel13 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.treeViewCNCFolder = new System.Windows.Forms.TreeView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.createFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripCNC = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.createFolderToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnDownloadProgram = new System.Windows.Forms.Button();
            this.btnExpandOrContract = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProgramDetails)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tableLayoutPanel11.SuspendLayout();
            this.tableLayoutPanel13.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStripCNC.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // listsysPrograms
            // 
            this.listsysPrograms.AllowDrop = true;
            this.listsysPrograms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listsysPrograms.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listsysPrograms.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(88)))), ((int)(((byte)(163)))));
            this.listsysPrograms.FormattingEnabled = true;
            this.listsysPrograms.IntegralHeight = false;
            this.listsysPrograms.ItemHeight = 20;
            this.listsysPrograms.Location = new System.Drawing.Point(0, 0);
            this.listsysPrograms.Margin = new System.Windows.Forms.Padding(0);
            this.listsysPrograms.Name = "listsysPrograms";
            this.listsysPrograms.Size = new System.Drawing.Size(205, 653);
            this.listsysPrograms.TabIndex = 0;
            this.listsysPrograms.Click += new System.EventHandler(this.listsysPrograms_Click);
            // 
            // treeSystm
            // 
            this.treeSystm.AllowDrop = true;
            this.treeSystm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeSystm.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeSystm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(88)))), ((int)(((byte)(163)))));
            this.treeSystm.Location = new System.Drawing.Point(0, 0);
            this.treeSystm.Margin = new System.Windows.Forms.Padding(0);
            this.treeSystm.Name = "treeSystm";
            treeNode1.Name = "Node0";
            treeNode1.Text = "Programs folder";
            this.treeSystm.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.treeSystm.ShowRootLines = false;
            this.treeSystm.Size = new System.Drawing.Size(151, 653);
            this.treeSystm.TabIndex = 1;
            this.treeSystm.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeSystm_MouseUp);
            // 
            // dgvProgramDetails
            // 
            this.dgvProgramDetails.AllowDrop = true;
            this.dgvProgramDetails.AllowUserToAddRows = false;
            this.dgvProgramDetails.AllowUserToDeleteRows = false;
            this.dgvProgramDetails.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProgramDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvProgramDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProgramDetails.BackgroundColor = System.Drawing.Color.White;
            this.dgvProgramDetails.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(88)))), ((int)(((byte)(163)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProgramDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvProgramDetails.ColumnHeadersHeight = 37;
            this.dgvProgramDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Program,
            this.Comment,
            this.Modifiedon});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(88)))), ((int)(((byte)(163)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvProgramDetails.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvProgramDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProgramDetails.EnableHeadersVisualStyles = false;
            this.dgvProgramDetails.GridColor = System.Drawing.SystemColors.ControlLight;
            this.dgvProgramDetails.Location = new System.Drawing.Point(173, 3);
            this.dgvProgramDetails.MultiSelect = false;
            this.dgvProgramDetails.Name = "dgvProgramDetails";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProgramDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvProgramDetails.RowHeadersVisible = false;
            this.dgvProgramDetails.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvProgramDetails.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvProgramDetails.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvProgramDetails.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgvProgramDetails.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvProgramDetails.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProgramDetails.RowTemplate.Height = 39;
            this.dgvProgramDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProgramDetails.Size = new System.Drawing.Size(389, 649);
            this.dgvProgramDetails.TabIndex = 2;
            // 
            // Id
            // 
            this.Id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Id.DataPropertyName = "Isselected";
            this.Id.FalseValue = "False";
            this.Id.HeaderText = "Select";
            this.Id.MinimumWidth = 50;
            this.Id.Name = "Id";
            this.Id.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Id.TrueValue = "True";
            this.Id.Width = 75;
            // 
            // Program
            // 
            this.Program.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Program.DataPropertyName = "ProgramNo";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Program.DefaultCellStyle = dataGridViewCellStyle3;
            this.Program.HeaderText = "Program";
            this.Program.MinimumWidth = 70;
            this.Program.Name = "Program";
            this.Program.ReadOnly = true;
            this.Program.Width = 95;
            // 
            // Comment
            // 
            this.Comment.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Comment.DataPropertyName = "Comment";
            this.Comment.HeaderText = "Comment";
            this.Comment.MinimumWidth = 230;
            this.Comment.Name = "Comment";
            this.Comment.ReadOnly = true;
            // 
            // Modifiedon
            // 
            this.Modifiedon.DataPropertyName = "ModifiedDate";
            this.Modifiedon.HeaderText = "Modified on";
            this.Modifiedon.MinimumWidth = 170;
            this.Modifiedon.Name = "Modifiedon";
            this.Modifiedon.ReadOnly = true;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Blank-Folder-icon.png");
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.tableLayoutPanel4);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(88)))), ((int)(((byte)(163)))));
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(372, 722);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = " Computer";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.splitContainer2, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 25);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(366, 694);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 38);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeSystm);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listsysPrograms);
            this.splitContainer1.Size = new System.Drawing.Size(360, 653);
            this.splitContainer1.SplitterDistance = 151;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(88)))), ((int)(((byte)(163)))));
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(88)))), ((int)(((byte)(163)))));
            this.splitContainer2.Panel2.Controls.Add(this.tableLayoutPanel5);
            this.splitContainer2.Size = new System.Drawing.Size(366, 35);
            this.splitContainer2.SplitterDistance = 154;
            this.splitContainer2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(37, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "Machine";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(208, 35);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(202, 35);
            this.label2.TabIndex = 2;
            this.label2.Text = "Programs";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.Transparent;
            this.groupBox4.Controls.Add(this.tableLayoutPanel11);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(88)))), ((int)(((byte)(163)))));
            this.groupBox4.Location = new System.Drawing.Point(568, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.groupBox4.Size = new System.Drawing.Size(571, 728);
            this.groupBox4.TabIndex = 13;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = " CNC Machine";
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.ColumnCount = 1;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel11.Controls.Add(this.tableLayoutPanel13, 0, 1);
            this.tableLayoutPanel11.Controls.Add(this.tableLayoutPanel8, 0, 0);
            this.tableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel11.Location = new System.Drawing.Point(0, 25);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 2;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanel11.Size = new System.Drawing.Size(571, 703);
            this.tableLayoutPanel11.TabIndex = 0;
            // 
            // tableLayoutPanel13
            // 
            this.tableLayoutPanel13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tableLayoutPanel13.ColumnCount = 2;
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.07692F));
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 76.92308F));
            this.tableLayoutPanel13.Controls.Add(this.btnExpandOrContract, 0, 0);
            this.tableLayoutPanel13.Location = new System.Drawing.Point(3, 664);
            this.tableLayoutPanel13.Name = "tableLayoutPanel13";
            this.tableLayoutPanel13.RowCount = 1;
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel13.Size = new System.Drawing.Size(156, 36);
            this.tableLayoutPanel13.TabIndex = 6;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 2;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 170F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Controls.Add(this.treeViewCNCFolder, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.dgvProgramDetails, 1, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 1;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(565, 655);
            this.tableLayoutPanel8.TabIndex = 0;
            // 
            // treeViewCNCFolder
            // 
            this.treeViewCNCFolder.AllowDrop = true;
            this.treeViewCNCFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewCNCFolder.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeViewCNCFolder.ForeColor = System.Drawing.Color.Black;
            this.treeViewCNCFolder.Location = new System.Drawing.Point(3, 3);
            this.treeViewCNCFolder.Name = "treeViewCNCFolder";
            treeNode2.Name = "Node5";
            treeNode2.Text = "SYSYEM";
            treeNode3.Name = "Node6";
            treeNode3.Text = "MTB1";
            treeNode4.Name = "Node7";
            treeNode4.Text = "MTB2";
            treeNode5.Name = "Node8";
            treeNode5.Text = "LIBRARY";
            treeNode6.Checked = true;
            treeNode6.Name = "Node3";
            treeNode6.Text = "PATH1";
            treeNode7.Name = "Node4";
            treeNode7.Text = "PATH2";
            treeNode8.Name = "Node2";
            treeNode8.Text = "USER";
            treeNode9.Name = "CNC_MEM";
            treeNode9.Text = "CNC_MEM";
            this.treeViewCNCFolder.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode9});
            this.treeViewCNCFolder.ShowRootLines = false;
            this.treeViewCNCFolder.Size = new System.Drawing.Size(164, 649);
            this.treeViewCNCFolder.TabIndex = 4;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1148, 740);
            this.tableLayoutPanel1.TabIndex = 14;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel6, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 734F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1142, 734);
            this.tableLayoutPanel2.TabIndex = 25;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 3;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 181F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel7, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.groupBox4, 2, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(1142, 734);
            this.tableLayoutPanel6.TabIndex = 16;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 1;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(378, 728);
            this.tableLayoutPanel7.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.btnUpload, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.btnDownloadProgram, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(384, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 83F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(181, 734);
            this.tableLayoutPanel3.TabIndex = 13;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(3, 653);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(104, 78);
            this.panel1.TabIndex = 2;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createFolderToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(145, 26);
            // 
            // createFolderToolStripMenuItem
            // 
            this.createFolderToolStripMenuItem.Name = "createFolderToolStripMenuItem";
            this.createFolderToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.createFolderToolStripMenuItem.Text = "Create Folder";
            this.createFolderToolStripMenuItem.Click += new System.EventHandler(this.createFolderToolStripMenuItem_Click);
            // 
            // contextMenuStripCNC
            // 
            this.contextMenuStripCNC.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createFolderToolStripMenuItem1});
            this.contextMenuStripCNC.Name = "contextMenuStripCNC";
            this.contextMenuStripCNC.Size = new System.Drawing.Size(145, 26);
            // 
            // createFolderToolStripMenuItem1
            // 
            this.createFolderToolStripMenuItem1.Name = "createFolderToolStripMenuItem1";
            this.createFolderToolStripMenuItem1.Size = new System.Drawing.Size(144, 22);
            this.createFolderToolStripMenuItem1.Text = "Create Folder";
            // 
            // btnUpload
            // 
            this.btnUpload.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnUpload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(88)))), ((int)(((byte)(163)))));
            this.btnUpload.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnUpload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpload.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpload.ForeColor = System.Drawing.Color.White;
            this.btnUpload.Image = global::MachineConnectOEM.Properties.Resources.btnUpload;
            this.btnUpload.Location = new System.Drawing.Point(0, 328);
            this.btnUpload.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(176, 77);
            this.btnUpload.TabIndex = 0;
            this.btnUpload.Text = "    ";
            this.btnUpload.UseVisualStyleBackColor = false;
            // 
            // btnDownloadProgram
            // 
            this.btnDownloadProgram.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnDownloadProgram.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(88)))), ((int)(((byte)(163)))));
            this.btnDownloadProgram.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnDownloadProgram.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownloadProgram.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownloadProgram.ForeColor = System.Drawing.Color.White;
            this.btnDownloadProgram.Image = global::MachineConnectOEM.Properties.Resources.btnDownload;
            this.btnDownloadProgram.Location = new System.Drawing.Point(0, 249);
            this.btnDownloadProgram.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.btnDownloadProgram.Name = "btnDownloadProgram";
            this.btnDownloadProgram.Size = new System.Drawing.Size(176, 73);
            this.btnDownloadProgram.TabIndex = 1;
            this.btnDownloadProgram.UseVisualStyleBackColor = false;
            // 
            // btnExpandOrContract
            // 
            this.btnExpandOrContract.BackColor = System.Drawing.Color.Transparent;
            this.btnExpandOrContract.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExpandOrContract.BackgroundImage")));
            this.btnExpandOrContract.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExpandOrContract.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExpandOrContract.FlatAppearance.BorderSize = 0;
            this.btnExpandOrContract.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnExpandOrContract.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnExpandOrContract.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExpandOrContract.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExpandOrContract.ForeColor = System.Drawing.Color.White;
            this.btnExpandOrContract.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExpandOrContract.Location = new System.Drawing.Point(2, 0);
            this.btnExpandOrContract.Margin = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.btnExpandOrContract.Name = "btnExpandOrContract";
            this.btnExpandOrContract.Size = new System.Drawing.Size(33, 36);
            this.btnExpandOrContract.TabIndex = 46;
            this.btnExpandOrContract.UseVisualStyleBackColor = false;
            this.btnExpandOrContract.Click += new System.EventHandler(this.btnExpandOrContract_Click);
            // 
            // ProgramTransferControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Name = "ProgramTransferControl";
            this.Size = new System.Drawing.Size(1148, 740);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProgramDetails)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.tableLayoutPanel11.ResumeLayout(false);
            this.tableLayoutPanel13.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStripCNC.ResumeLayout(false);
            this.ResumeLayout(false);

        }

      

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;//1
        private System.Windows.Forms.Button btnDownloadProgram;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ListBox listsysPrograms;
        private System.Windows.Forms.TreeView treeSystm;
        private System.Windows.Forms.DataGridView dgvProgramDetails;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel11;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TreeView treeViewCNCFolder;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel13;
        private System.Windows.Forms.Button btnExpandOrContract;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem createFolderToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripCNC;
        private System.Windows.Forms.ToolStripMenuItem createFolderToolStripMenuItem1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Program;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comment;
        private System.Windows.Forms.DataGridViewTextBoxColumn Modifiedon;
        private System.Windows.Forms.Label label1;
      //  private System.Windows.Forms.TreeView tree_systm;
    }
}

