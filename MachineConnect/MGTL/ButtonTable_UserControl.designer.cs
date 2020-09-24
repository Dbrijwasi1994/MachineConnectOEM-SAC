namespace MachineConnectOEM
{
    partial class ButtonTable_UserControl
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnOutPut = new System.Windows.Forms.Button();
            this.btnOd = new System.Windows.Forms.Button();
            this.btnIDcycleTime = new System.Windows.Forms.Button();
            this.btnDressingTime = new System.Windows.Forms.Button();
            this.pnlForAllButtons = new System.Windows.Forms.Panel();
            this.outPut_UserControl1 = new MachineConnectOEM.OutPut_UserControl();
            this.odCalculator1 = new MachineConnectOEM.ODCalculator();
            this.idcycleTimeUserControl1 = new MachineConnectOEM.IDCYCLETimeUserControl();
            this.dressingTime_UserControl1 = new MachineConnectOEM.DressingTime_UserControl();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.pnlForAllButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pnlForAllButtons, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1177, 720);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel2.ColumnCount = 5;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 575F));
            this.tableLayoutPanel2.Controls.Add(this.btnOutPut, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnOd, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnIDcycleTime, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnDressingTime, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 660);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1177, 60);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // btnOutPut
            // 
            this.btnOutPut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOutPut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnOutPut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOutPut.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOutPut.ForeColor = System.Drawing.Color.White;
            this.btnOutPut.Location = new System.Drawing.Point(453, 3);
            this.btnOutPut.Name = "btnOutPut";
            this.btnOutPut.Size = new System.Drawing.Size(144, 54);
            this.btnOutPut.TabIndex = 3;
            this.btnOutPut.Text = "Summary";
            this.btnOutPut.UseVisualStyleBackColor = false;
            this.btnOutPut.Click += new System.EventHandler(this.btnOutPut_Click);
            // 
            // btnOd
            // 
            this.btnOd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnOd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOd.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOd.ForeColor = System.Drawing.Color.White;
            this.btnOd.Location = new System.Drawing.Point(303, 3);
            this.btnOd.Name = "btnOd";
            this.btnOd.Size = new System.Drawing.Size(144, 54);
            this.btnOd.TabIndex = 2;
            this.btnOd.Text = "OD Cycle Time";
            this.btnOd.UseVisualStyleBackColor = false;
            this.btnOd.Click += new System.EventHandler(this.btnOd_Click);
            // 
            // btnIDcycleTime
            // 
            this.btnIDcycleTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnIDcycleTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnIDcycleTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIDcycleTime.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIDcycleTime.ForeColor = System.Drawing.Color.White;
            this.btnIDcycleTime.Location = new System.Drawing.Point(153, 3);
            this.btnIDcycleTime.Name = "btnIDcycleTime";
            this.btnIDcycleTime.Size = new System.Drawing.Size(144, 54);
            this.btnIDcycleTime.TabIndex = 1;
            this.btnIDcycleTime.Text = "ID Cycle Time";
            this.btnIDcycleTime.UseVisualStyleBackColor = false;
            this.btnIDcycleTime.Click += new System.EventHandler(this.btnIDcycleTime_Click);
            // 
            // btnDressingTime
            // 
            this.btnDressingTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDressingTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnDressingTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDressingTime.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDressingTime.ForeColor = System.Drawing.Color.White;
            this.btnDressingTime.Location = new System.Drawing.Point(3, 3);
            this.btnDressingTime.Name = "btnDressingTime";
            this.btnDressingTime.Size = new System.Drawing.Size(144, 54);
            this.btnDressingTime.TabIndex = 0;
            this.btnDressingTime.Text = "Dressing Time";
            this.btnDressingTime.UseVisualStyleBackColor = false;
            this.btnDressingTime.Click += new System.EventHandler(this.btnDressingTime_Click);
            // 
            // pnlForAllButtons
            // 
            this.pnlForAllButtons.Controls.Add(this.outPut_UserControl1);
            this.pnlForAllButtons.Controls.Add(this.odCalculator1);
            this.pnlForAllButtons.Controls.Add(this.idcycleTimeUserControl1);
            this.pnlForAllButtons.Controls.Add(this.dressingTime_UserControl1);
            this.pnlForAllButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlForAllButtons.Location = new System.Drawing.Point(3, 3);
            this.pnlForAllButtons.Name = "pnlForAllButtons";
            this.pnlForAllButtons.Size = new System.Drawing.Size(1171, 654);
            this.pnlForAllButtons.TabIndex = 1;
            // 
            // outPut_UserControl1
            // 
            this.outPut_UserControl1.DressingOD = null;
            this.outPut_UserControl1.dressingTimeID = null;
            this.outPut_UserControl1.Location = new System.Drawing.Point(870, 102);
            this.outPut_UserControl1.Name = "outPut_UserControl1";
            this.outPut_UserControl1.RapidApproachID = null;
            this.outPut_UserControl1.RapidForwardReturn = null;
            this.outPut_UserControl1.Size = new System.Drawing.Size(371, 570);
            this.outPut_UserControl1.TabIndex = 3;
            this.outPut_UserControl1.TotalCuttingTimeID = null;
            this.outPut_UserControl1.TotalCuttingTimeOD = null;
            this.outPut_UserControl1.Visible = false;
            // 
            // odCalculator1
            // 
            this.odCalculator1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.odCalculator1.Location = new System.Drawing.Point(0, 0);
            this.odCalculator1.Name = "odCalculator1";
            this.odCalculator1.RapidForwardReturnText = null;
            this.odCalculator1.Size = new System.Drawing.Size(1171, 654);
            this.odCalculator1.TabIndex = 2;
            this.odCalculator1.TotalDressingTime = null;
            this.odCalculator1.Visible = false;
            // 
            // idcycleTimeUserControl1
            // 
            this.idcycleTimeUserControl1.dressingTime = null;
            this.idcycleTimeUserControl1.Location = new System.Drawing.Point(24, 21);
            this.idcycleTimeUserControl1.Name = "idcycleTimeUserControl1";
            this.idcycleTimeUserControl1.Rappidapproach = null;
            this.idcycleTimeUserControl1.Size = new System.Drawing.Size(270, 720);
            this.idcycleTimeUserControl1.TabIndex = 1;
            this.idcycleTimeUserControl1.Visible = false;
            // 
            // dressingTime_UserControl1
            // 
            this.dressingTime_UserControl1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dressingTime_UserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dressingTime_UserControl1.Location = new System.Drawing.Point(0, 0);
            this.dressingTime_UserControl1.Name = "dressingTime_UserControl1";
            this.dressingTime_UserControl1.Size = new System.Drawing.Size(1171, 654);
            this.dressingTime_UserControl1.TabIndex = 0;
            // 
            // ButtonTable_UserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ButtonTable_UserControl";
            this.Size = new System.Drawing.Size(1177, 720);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.pnlForAllButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnOutPut;
        private System.Windows.Forms.Button btnOd;
        private System.Windows.Forms.Button btnIDcycleTime;
        private System.Windows.Forms.Button btnDressingTime;
        private System.Windows.Forms.Panel pnlForAllButtons;
        private DressingTime_UserControl dressingTime_UserControl1;
        private IDCYCLETimeUserControl idcycleTimeUserControl1;
        private ODCalculator odCalculator1;
        private OutPut_UserControl outPut_UserControl1;
    }
}
