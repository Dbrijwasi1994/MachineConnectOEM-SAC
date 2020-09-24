namespace MachineConnectApplication
{
    partial class MachineInformation
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grpPump = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tblMainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.tblInputFeilds = new System.Windows.Forms.TableLayoutPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.chkEtherNet = new System.Windows.Forms.CheckBox();
            this.txtPartCountMacroLocation = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblPumpModel = new System.Windows.Forms.Label();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblInterfaceID = new System.Windows.Forms.Label();
            this.txtInterfaceId = new System.Windows.Forms.TextBox();
            this.cmbMachineId = new System.Windows.Forms.ComboBox();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbMTB = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbMachineType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbModel = new System.Windows.Forms.ComboBox();
            this.chkProgramFoldersEnabled = new System.Windows.Forms.CheckBox();
            this.chkPartCountByMacro = new System.Windows.Forms.CheckBox();
            this.txtSpindleAxisNumber = new System.Windows.Forms.TextBox();
            this.lblPumpInformation = new System.Windows.Forms.Label();
            this.btnPumpInfoClose = new System.Windows.Forms.Button();
            this.btnPumpInfoMin = new System.Windows.Forms.Button();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.Machineid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MachineMTB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MachineType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MachineModel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IPAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PortNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Interfaceid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EthernetEnabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ProgramFoldersEnabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SpindleAxisNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EnablePartCountByMacro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpPump.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tblMainLayout.SuspendLayout();
            this.tblInputFeilds.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpPump
            // 
            this.grpPump.BackColor = System.Drawing.Color.Transparent;
            this.grpPump.Controls.Add(this.tableLayoutPanel4);
            this.grpPump.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpPump.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpPump.ForeColor = System.Drawing.Color.Green;
            this.grpPump.Location = new System.Drawing.Point(5, 3);
            this.grpPump.Margin = new System.Windows.Forms.Padding(5, 3, 3, 3);
            this.grpPump.Name = "grpPump";
            this.grpPump.Size = new System.Drawing.Size(1213, 109);
            this.grpPump.TabIndex = 4;
            this.grpPump.TabStop = false;
            this.grpPump.Text = "Machine Information";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.tblMainLayout, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 23);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 63.47826F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1207, 83);
            this.tableLayoutPanel4.TabIndex = 32;
            // 
            // tblMainLayout
            // 
            this.tblMainLayout.ColumnCount = 1;
            this.tblMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblMainLayout.Controls.Add(this.tblInputFeilds, 0, 0);
            this.tblMainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMainLayout.Location = new System.Drawing.Point(3, 3);
            this.tblMainLayout.Name = "tblMainLayout";
            this.tblMainLayout.RowCount = 1;
            this.tblMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMainLayout.Size = new System.Drawing.Size(1201, 77);
            this.tblMainLayout.TabIndex = 32;
            // 
            // tblInputFeilds
            // 
            this.tblInputFeilds.ColumnCount = 13;
            this.tblInputFeilds.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 91F));
            this.tblInputFeilds.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 152F));
            this.tblInputFeilds.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tblInputFeilds.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 108F));
            this.tblInputFeilds.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblInputFeilds.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 142F));
            this.tblInputFeilds.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.tblInputFeilds.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.tblInputFeilds.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 177F));
            this.tblInputFeilds.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tblInputFeilds.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 96F));
            this.tblInputFeilds.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 209F));
            this.tblInputFeilds.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblInputFeilds.Controls.Add(this.btnSave, 10, 1);
            this.tblInputFeilds.Controls.Add(this.chkEtherNet, 10, 0);
            this.tblInputFeilds.Controls.Add(this.txtPartCountMacroLocation, 9, 0);
            this.tblInputFeilds.Controls.Add(this.label2, 8, 1);
            this.tblInputFeilds.Controls.Add(this.lblPumpModel, 0, 0);
            this.tblInputFeilds.Controls.Add(this.txtDesc, 7, 0);
            this.tblInputFeilds.Controls.Add(this.lblDescription, 6, 0);
            this.tblInputFeilds.Controls.Add(this.lblInterfaceID, 6, 1);
            this.tblInputFeilds.Controls.Add(this.txtInterfaceId, 7, 1);
            this.tblInputFeilds.Controls.Add(this.cmbMachineId, 1, 0);
            this.tblInputFeilds.Controls.Add(this.lblCustomer, 2, 0);
            this.tblInputFeilds.Controls.Add(this.txtIP, 3, 0);
            this.tblInputFeilds.Controls.Add(this.lblSpeed, 4, 0);
            this.tblInputFeilds.Controls.Add(this.txtPort, 5, 0);
            this.tblInputFeilds.Controls.Add(this.label1, 0, 1);
            this.tblInputFeilds.Controls.Add(this.cmbMTB, 1, 1);
            this.tblInputFeilds.Controls.Add(this.label5, 4, 1);
            this.tblInputFeilds.Controls.Add(this.cmbMachineType, 5, 1);
            this.tblInputFeilds.Controls.Add(this.label6, 2, 1);
            this.tblInputFeilds.Controls.Add(this.cmbModel, 3, 1);
            this.tblInputFeilds.Controls.Add(this.chkProgramFoldersEnabled, 11, 0);
            this.tblInputFeilds.Controls.Add(this.chkPartCountByMacro, 8, 0);
            this.tblInputFeilds.Controls.Add(this.txtSpindleAxisNumber, 9, 1);
            this.tblInputFeilds.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblInputFeilds.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tblInputFeilds.Location = new System.Drawing.Point(0, 0);
            this.tblInputFeilds.Margin = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.tblInputFeilds.Name = "tblInputFeilds";
            this.tblInputFeilds.RowCount = 2;
            this.tblInputFeilds.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblInputFeilds.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblInputFeilds.Size = new System.Drawing.Size(1198, 74);
            this.tblInputFeilds.TabIndex = 33;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkSlateBlue;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(946, 37);
            this.btnSave.Margin = new System.Windows.Forms.Padding(0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(88, 37);
            this.btnSave.TabIndex = 56;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // chkEtherNet
            // 
            this.chkEtherNet.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEtherNet.Location = new System.Drawing.Point(949, 3);
            this.chkEtherNet.Name = "chkEtherNet";
            this.chkEtherNet.Size = new System.Drawing.Size(90, 31);
            this.chkEtherNet.TabIndex = 37;
            this.chkEtherNet.Text = " Enabled";
            this.chkEtherNet.UseVisualStyleBackColor = true;
            // 
            // txtPartCountMacroLocation
            // 
            this.txtPartCountMacroLocation.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtPartCountMacroLocation.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPartCountMacroLocation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(88)))), ((int)(((byte)(163)))));
            this.txtPartCountMacroLocation.Location = new System.Drawing.Point(843, 6);
            this.txtPartCountMacroLocation.Name = "txtPartCountMacroLocation";
            this.txtPartCountMacroLocation.Size = new System.Drawing.Size(89, 25);
            this.txtPartCountMacroLocation.TabIndex = 34;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(663, 45);
            this.label2.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(164, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "  Spindle Axis Number";
            // 
            // lblPumpModel
            // 
            this.lblPumpModel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPumpModel.AutoSize = true;
            this.lblPumpModel.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPumpModel.Location = new System.Drawing.Point(0, 8);
            this.lblPumpModel.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.lblPumpModel.Name = "lblPumpModel";
            this.lblPumpModel.Size = new System.Drawing.Size(88, 20);
            this.lblPumpModel.TabIndex = 0;
            this.lblPumpModel.Text = "Machine ID";
            // 
            // txtDesc
            // 
            this.txtDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDesc.Location = new System.Drawing.Point(666, 5);
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(1, 27);
            this.txtDesc.TabIndex = 7;
            // 
            // lblDescription
            // 
            this.lblDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(663, 17);
            this.lblDescription.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(1, 20);
            this.lblDescription.TabIndex = 1;
            this.lblDescription.Text = "Description";
            // 
            // lblInterfaceID
            // 
            this.lblInterfaceID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblInterfaceID.AutoSize = true;
            this.lblInterfaceID.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInterfaceID.Location = new System.Drawing.Point(663, 54);
            this.lblInterfaceID.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.lblInterfaceID.Name = "lblInterfaceID";
            this.lblInterfaceID.Size = new System.Drawing.Size(1, 20);
            this.lblInterfaceID.TabIndex = 2;
            this.lblInterfaceID.Text = "Interface ID";
            // 
            // txtInterfaceId
            // 
            this.txtInterfaceId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInterfaceId.Location = new System.Drawing.Point(666, 42);
            this.txtInterfaceId.Name = "txtInterfaceId";
            this.txtInterfaceId.Size = new System.Drawing.Size(1, 27);
            this.txtInterfaceId.TabIndex = 10;
            this.txtInterfaceId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtInterfaceId_KeyPress);
            // 
            // cmbMachineId
            // 
            this.cmbMachineId.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbMachineId.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMachineId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(88)))), ((int)(((byte)(163)))));
            this.cmbMachineId.FormattingEnabled = true;
            this.cmbMachineId.Location = new System.Drawing.Point(94, 6);
            this.cmbMachineId.Name = "cmbMachineId";
            this.cmbMachineId.Size = new System.Drawing.Size(146, 25);
            this.cmbMachineId.TabIndex = 6;
            // 
            // lblCustomer
            // 
            this.lblCustomer.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomer.Location = new System.Drawing.Point(243, 8);
            this.lblCustomer.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(59, 20);
            this.lblCustomer.TabIndex = 3;
            this.lblCustomer.Text = "DNC IP";
            // 
            // txtIP
            // 
            this.txtIP.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtIP.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(88)))), ((int)(((byte)(163)))));
            this.txtIP.Location = new System.Drawing.Point(316, 6);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(102, 25);
            this.txtIP.TabIndex = 8;
            this.txtIP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIP_KeyPress);
            // 
            // lblSpeed
            // 
            this.lblSpeed.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSpeed.AutoSize = true;
            this.lblSpeed.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSpeed.Location = new System.Drawing.Point(421, 8);
            this.lblSpeed.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(93, 20);
            this.lblSpeed.TabIndex = 5;
            this.lblSpeed.Text = "DNC IP Port";
            // 
            // txtPort
            // 
            this.txtPort.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtPort.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(88)))), ((int)(((byte)(163)))));
            this.txtPort.Location = new System.Drawing.Point(524, 6);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(136, 25);
            this.txtPort.TabIndex = 9;
            this.txtPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPort_KeyPress);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 45);
            this.label1.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 20);
            this.label1.TabIndex = 11;
            this.label1.Text = "MTB";
            // 
            // cmbMTB
            // 
            this.cmbMTB.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbMTB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMTB.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMTB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(88)))), ((int)(((byte)(163)))));
            this.cmbMTB.FormattingEnabled = true;
            this.cmbMTB.Location = new System.Drawing.Point(94, 43);
            this.cmbMTB.Name = "cmbMTB";
            this.cmbMTB.Size = new System.Drawing.Size(146, 25);
            this.cmbMTB.TabIndex = 18;
            this.cmbMTB.SelectionChangeCommitted += new System.EventHandler(this.cmbMTB_SelectionChangeCommitted);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(421, 37);
            this.label5.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 37);
            this.label5.TabIndex = 15;
            this.label5.Text = "Type";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbMachineType
            // 
            this.cmbMachineType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbMachineType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMachineType.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMachineType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(88)))), ((int)(((byte)(163)))));
            this.cmbMachineType.FormattingEnabled = true;
            this.cmbMachineType.Location = new System.Drawing.Point(524, 43);
            this.cmbMachineType.Name = "cmbMachineType";
            this.cmbMachineType.Size = new System.Drawing.Size(136, 25);
            this.cmbMachineType.TabIndex = 19;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(243, 45);
            this.label6.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 20);
            this.label6.TabIndex = 16;
            this.label6.Text = "Model";
            // 
            // cmbModel
            // 
            this.cmbModel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbModel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbModel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(88)))), ((int)(((byte)(163)))));
            this.cmbModel.FormattingEnabled = true;
            this.cmbModel.Location = new System.Drawing.Point(316, 43);
            this.cmbModel.Name = "cmbModel";
            this.cmbModel.Size = new System.Drawing.Size(102, 25);
            this.cmbModel.TabIndex = 17;
            // 
            // chkProgramFoldersEnabled
            // 
            this.chkProgramFoldersEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkProgramFoldersEnabled.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkProgramFoldersEnabled.Location = new System.Drawing.Point(1042, 6);
            this.chkProgramFoldersEnabled.Margin = new System.Windows.Forms.Padding(0);
            this.chkProgramFoldersEnabled.Name = "chkProgramFoldersEnabled";
            this.chkProgramFoldersEnabled.Size = new System.Drawing.Size(209, 25);
            this.chkProgramFoldersEnabled.TabIndex = 57;
            this.chkProgramFoldersEnabled.Text = "Program Folder Enabled";
            this.chkProgramFoldersEnabled.UseVisualStyleBackColor = true;
            // 
            // chkPartCountByMacro
            // 
            this.chkPartCountByMacro.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkPartCountByMacro.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPartCountByMacro.Location = new System.Drawing.Point(663, 6);
            this.chkPartCountByMacro.Margin = new System.Windows.Forms.Padding(0);
            this.chkPartCountByMacro.Name = "chkPartCountByMacro";
            this.chkPartCountByMacro.Size = new System.Drawing.Size(177, 25);
            this.chkPartCountByMacro.TabIndex = 34;
            this.chkPartCountByMacro.Text = "Part Count By Macro";
            this.chkPartCountByMacro.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.chkPartCountByMacro.UseVisualStyleBackColor = true;
            this.chkPartCountByMacro.CheckStateChanged += new System.EventHandler(this.chkPartCountByMacro_CheckStateChanged);
            // 
            // txtSpindleAxisNumber
            // 
            this.txtSpindleAxisNumber.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtSpindleAxisNumber.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSpindleAxisNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(88)))), ((int)(((byte)(163)))));
            this.txtSpindleAxisNumber.Location = new System.Drawing.Point(843, 43);
            this.txtSpindleAxisNumber.Name = "txtSpindleAxisNumber";
            this.txtSpindleAxisNumber.Size = new System.Drawing.Size(89, 25);
            this.txtSpindleAxisNumber.TabIndex = 35;
            // 
            // lblPumpInformation
            // 
            this.lblPumpInformation.AutoSize = true;
            this.lblPumpInformation.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPumpInformation.ForeColor = System.Drawing.Color.White;
            this.lblPumpInformation.Location = new System.Drawing.Point(254, 5);
            this.lblPumpInformation.Name = "lblPumpInformation";
            this.lblPumpInformation.Size = new System.Drawing.Size(220, 25);
            this.lblPumpInformation.TabIndex = 3;
            this.lblPumpInformation.Text = "Test Bench Information";
            // 
            // btnPumpInfoClose
            // 
            this.btnPumpInfoClose.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPumpInfoClose.BackColor = System.Drawing.Color.Firebrick;
            this.btnPumpInfoClose.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnPumpInfoClose.FlatAppearance.BorderSize = 2;
            this.btnPumpInfoClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPumpInfoClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPumpInfoClose.ForeColor = System.Drawing.Color.White;
            this.btnPumpInfoClose.Location = new System.Drawing.Point(948, 5);
            this.btnPumpInfoClose.Name = "btnPumpInfoClose";
            this.btnPumpInfoClose.Size = new System.Drawing.Size(25, 25);
            this.btnPumpInfoClose.TabIndex = 18;
            this.btnPumpInfoClose.Text = "X";
            this.btnPumpInfoClose.UseVisualStyleBackColor = false;
            // 
            // btnPumpInfoMin
            // 
            this.btnPumpInfoMin.BackColor = System.Drawing.Color.Transparent;
            this.btnPumpInfoMin.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnPumpInfoMin.FlatAppearance.BorderSize = 2;
            this.btnPumpInfoMin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPumpInfoMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPumpInfoMin.ForeColor = System.Drawing.Color.White;
            this.btnPumpInfoMin.Location = new System.Drawing.Point(678, 5);
            this.btnPumpInfoMin.Name = "btnPumpInfoMin";
            this.btnPumpInfoMin.Size = new System.Drawing.Size(25, 25);
            this.btnPumpInfoMin.TabIndex = 19;
            this.btnPumpInfoMin.Text = "-";
            this.btnPumpInfoMin.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPumpInfoMin.UseVisualStyleBackColor = false;
            // 
            // dataGrid
            // 
            this.dataGrid.AllowUserToAddRows = false;
            this.dataGrid.AllowUserToDeleteRows = false;
            this.dataGrid.AllowUserToOrderColumns = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGrid.BackgroundColor = System.Drawing.Color.White;
            this.dataGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.White;
            this.dataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGrid.ColumnHeadersHeight = 40;
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Machineid,
            this.Description,
            this.MachineMTB,
            this.MachineType,
            this.MachineModel,
            this.IPAddress,
            this.PortNo,
            this.Interfaceid,
            this.EthernetEnabled,
            this.ProgramFoldersEnabled,
            this.SpindleAxisNumber,
            this.EnablePartCountByMacro});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGrid.DefaultCellStyle = dataGridViewCellStyle9;
            this.dataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid.EnableHeadersVisualStyles = false;
            this.dataGrid.GridColor = System.Drawing.SystemColors.ControlLight;
            this.dataGrid.Location = new System.Drawing.Point(0, 115);
            this.dataGrid.Margin = new System.Windows.Forms.Padding(0);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.ReadOnly = true;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dataGrid.RowHeadersVisible = false;
            this.dataGrid.RowHeadersWidth = 40;
            this.dataGrid.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGrid.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGrid.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dataGrid.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dataGrid.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGrid.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGrid.RowTemplate.Height = 35;
            this.dataGrid.RowTemplate.ReadOnly = true;
            this.dataGrid.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGrid.Size = new System.Drawing.Size(1221, 494);
            this.dataGrid.TabIndex = 20;
            this.dataGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_CellDoubleClick);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.dataGrid, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.grpPump, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 115F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1221, 609);
            this.tableLayoutPanel3.TabIndex = 21;
            // 
            // Machineid
            // 
            this.Machineid.DataPropertyName = "Machineid";
            this.Machineid.HeaderText = "Machine ID";
            this.Machineid.MinimumWidth = 120;
            this.Machineid.Name = "Machineid";
            this.Machineid.ReadOnly = true;
            this.Machineid.Width = 120;
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "Description";
            this.Description.MinimumWidth = 120;
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Visible = false;
            this.Description.Width = 120;
            // 
            // MachineMTB
            // 
            this.MachineMTB.DataPropertyName = "MachineMTB";
            this.MachineMTB.HeaderText = "MTB";
            this.MachineMTB.MinimumWidth = 120;
            this.MachineMTB.Name = "MachineMTB";
            this.MachineMTB.ReadOnly = true;
            this.MachineMTB.Width = 120;
            // 
            // MachineType
            // 
            this.MachineType.DataPropertyName = "MachineType";
            this.MachineType.HeaderText = "Machine Type";
            this.MachineType.MinimumWidth = 130;
            this.MachineType.Name = "MachineType";
            this.MachineType.ReadOnly = true;
            this.MachineType.Width = 130;
            // 
            // MachineModel
            // 
            this.MachineModel.DataPropertyName = "MachineModel";
            this.MachineModel.HeaderText = "Model";
            this.MachineModel.MinimumWidth = 130;
            this.MachineModel.Name = "MachineModel";
            this.MachineModel.ReadOnly = true;
            this.MachineModel.Width = 130;
            // 
            // IPAddress
            // 
            this.IPAddress.DataPropertyName = "dncip";
            this.IPAddress.HeaderText = "DNC IP";
            this.IPAddress.MinimumWidth = 120;
            this.IPAddress.Name = "IPAddress";
            this.IPAddress.ReadOnly = true;
            this.IPAddress.Width = 120;
            // 
            // PortNo
            // 
            this.PortNo.DataPropertyName = "dncipportno";
            this.PortNo.HeaderText = "DNC IP Port";
            this.PortNo.MinimumWidth = 120;
            this.PortNo.Name = "PortNo";
            this.PortNo.ReadOnly = true;
            this.PortNo.Width = 120;
            // 
            // Interfaceid
            // 
            this.Interfaceid.DataPropertyName = "Interfaceid";
            this.Interfaceid.HeaderText = "Interface ID";
            this.Interfaceid.MinimumWidth = 2;
            this.Interfaceid.Name = "Interfaceid";
            this.Interfaceid.ReadOnly = true;
            this.Interfaceid.Visible = false;
            this.Interfaceid.Width = 116;
            // 
            // EthernetEnabled
            // 
            this.EthernetEnabled.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.EthernetEnabled.DataPropertyName = "EthernetEnabled";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.NullValue = false;
            this.EthernetEnabled.DefaultCellStyle = dataGridViewCellStyle8;
            this.EthernetEnabled.HeaderText = "Enabled";
            this.EthernetEnabled.MinimumWidth = 120;
            this.EthernetEnabled.Name = "EthernetEnabled";
            this.EthernetEnabled.ReadOnly = true;
            this.EthernetEnabled.Width = 120;
            // 
            // ProgramFoldersEnabled
            // 
            this.ProgramFoldersEnabled.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.ProgramFoldersEnabled.DataPropertyName = "ProgramFoldersEnabled";
            this.ProgramFoldersEnabled.HeaderText = "Program Folders Enabled";
            this.ProgramFoldersEnabled.MinimumWidth = 200;
            this.ProgramFoldersEnabled.Name = "ProgramFoldersEnabled";
            this.ProgramFoldersEnabled.ReadOnly = true;
            this.ProgramFoldersEnabled.Width = 200;
            // 
            // SpindleAxisNumber
            // 
            this.SpindleAxisNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.SpindleAxisNumber.DataPropertyName = "SpindleAxisNumber";
            this.SpindleAxisNumber.HeaderText = "Spindle Axis Number";
            this.SpindleAxisNumber.Name = "SpindleAxisNumber";
            this.SpindleAxisNumber.ReadOnly = true;
            this.SpindleAxisNumber.Visible = false;
            this.SpindleAxisNumber.Width = 180;
            // 
            // EnablePartCountByMacro
            // 
            this.EnablePartCountByMacro.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.EnablePartCountByMacro.DataPropertyName = "EnablePartCountByMacro";
            this.EnablePartCountByMacro.HeaderText = "Macro Location";
            this.EnablePartCountByMacro.MinimumWidth = 100;
            this.EnablePartCountByMacro.Name = "EnablePartCountByMacro";
            this.EnablePartCountByMacro.ReadOnly = true;
            this.EnablePartCountByMacro.Visible = false;
            // 
            // MachineInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.btnPumpInfoClose);
            this.Controls.Add(this.btnPumpInfoMin);
            this.Controls.Add(this.lblPumpInformation);
            this.Name = "MachineInformation";
            this.Size = new System.Drawing.Size(1221, 609);
            this.Load += new System.EventHandler(this.TestBenchInformation_Load);
            this.grpPump.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tblMainLayout.ResumeLayout(false);
            this.tblInputFeilds.ResumeLayout(false);
            this.tblInputFeilds.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpPump;
        private System.Windows.Forms.Label lblPumpInformation;
        private System.Windows.Forms.Button btnPumpInfoClose;
        private System.Windows.Forms.Button btnPumpInfoMin;
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tblMainLayout;
        private System.Windows.Forms.TableLayoutPanel tblInputFeilds;
        private System.Windows.Forms.TextBox txtInterfaceId;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Label lblPumpModel;
        private System.Windows.Forms.Label lblInterfaceID;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.ComboBox cmbMachineId;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.ComboBox cmbMTB;
        private System.Windows.Forms.ComboBox cmbModel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbMachineType;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox chkEtherNet;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPartCountMacroLocation;
        private System.Windows.Forms.CheckBox chkPartCountByMacro;
        private System.Windows.Forms.TextBox txtSpindleAxisNumber;
        private System.Windows.Forms.CheckBox chkProgramFoldersEnabled;
        private System.Windows.Forms.DataGridViewTextBoxColumn Machineid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn MachineMTB;
        private System.Windows.Forms.DataGridViewTextBoxColumn MachineType;
        private System.Windows.Forms.DataGridViewTextBoxColumn MachineModel;
        private System.Windows.Forms.DataGridViewTextBoxColumn IPAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn PortNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Interfaceid;
        private System.Windows.Forms.DataGridViewCheckBoxColumn EthernetEnabled;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ProgramFoldersEnabled;
        private System.Windows.Forms.DataGridViewTextBoxColumn SpindleAxisNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn EnablePartCountByMacro;
    }
}