namespace FocasGUI
{
    partial class DialogBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogBox));
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lblText = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.tblHeader = new System.Windows.Forms.TableLayoutPanel();
            this.lblHeaderText = new System.Windows.Forms.Label();
            this.btnMsgIndication = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.tblMainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tblHeader.SuspendLayout();
            this.tblMainLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 74.91166F));
            this.tableLayoutPanel3.Controls.Add(this.lblText, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel1, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(15, 42);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(15, 15, 15, 10);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 73.3871F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 26.6129F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(349, 124);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // lblText
            // 
            this.lblText.AutoSize = true;
            this.lblText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblText.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblText.ForeColor = System.Drawing.Color.White;
            this.lblText.Location = new System.Drawing.Point(3, 0);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(343, 91);
            this.lblText.TabIndex = 16;
            this.lblText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.btnCancel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnOk, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(74, 94);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(200, 27);
            this.tableLayoutPanel1.TabIndex = 43;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCancel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(112, 0);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 27);
            this.btnCancel.TabIndex = 43;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.lblClose_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnOk.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOk.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnOk.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.Location = new System.Drawing.Point(15, 0);
            this.btnOk.Margin = new System.Windows.Forms.Padding(0);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(70, 27);
            this.btnOk.TabIndex = 42;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // tblHeader
            // 
            this.tblHeader.BackColor = System.Drawing.Color.White;
            this.tblHeader.ColumnCount = 3;
            this.tblHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.5903F));
            this.tblHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 88.40971F));
            this.tblHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tblHeader.Controls.Add(this.lblHeaderText, 1, 0);
            this.tblHeader.Controls.Add(this.btnMsgIndication, 0, 0);
            this.tblHeader.Controls.Add(this.btnClose, 2, 0);
            this.tblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblHeader.Location = new System.Drawing.Point(0, 0);
            this.tblHeader.Margin = new System.Windows.Forms.Padding(0);
            this.tblHeader.Name = "tblHeader";
            this.tblHeader.RowCount = 1;
            this.tblHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblHeader.Size = new System.Drawing.Size(379, 27);
            this.tblHeader.TabIndex = 0;
            // 
            // lblHeaderText
            // 
            this.lblHeaderText.AutoSize = true;
            this.lblHeaderText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeaderText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeaderText.ForeColor = System.Drawing.Color.Navy;
            this.lblHeaderText.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblHeaderText.Location = new System.Drawing.Point(40, 1);
            this.lblHeaderText.Margin = new System.Windows.Forms.Padding(0, 1, 1, 1);
            this.lblHeaderText.Name = "lblHeaderText";
            this.lblHeaderText.Size = new System.Drawing.Size(304, 25);
            this.lblHeaderText.TabIndex = 14;
            this.lblHeaderText.Text = "Confirmation";
            this.lblHeaderText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnMsgIndication
            // 
            this.btnMsgIndication.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnMsgIndication.BackgroundImage")));
            this.btnMsgIndication.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnMsgIndication.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMsgIndication.Enabled = false;
            this.btnMsgIndication.FlatAppearance.BorderSize = 0;
            this.btnMsgIndication.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMsgIndication.ForeColor = System.Drawing.Color.White;
            this.btnMsgIndication.Location = new System.Drawing.Point(1, 1);
            this.btnMsgIndication.Margin = new System.Windows.Forms.Padding(1, 1, 0, 1);
            this.btnMsgIndication.Name = "btnMsgIndication";
            this.btnMsgIndication.Size = new System.Drawing.Size(39, 25);
            this.btnMsgIndication.TabIndex = 15;
            this.btnMsgIndication.UseVisualStyleBackColor = false;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClose.BackgroundImage")));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(346, 1);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(31, 25);
            this.btnClose.TabIndex = 63;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tblMainLayout
            // 
            this.tblMainLayout.BackColor = System.Drawing.Color.Cyan;
            this.tblMainLayout.ColumnCount = 1;
            this.tblMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMainLayout.Controls.Add(this.tblHeader, 0, 0);
            this.tblMainLayout.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tblMainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMainLayout.Location = new System.Drawing.Point(2, 2);
            this.tblMainLayout.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.tblMainLayout.Name = "tblMainLayout";
            this.tblMainLayout.RowCount = 2;
            this.tblMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tblMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblMainLayout.Size = new System.Drawing.Size(379, 176);
            this.tblMainLayout.TabIndex = 1;
            // 
            // DialogBox
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(75)))), ((int)(((byte)(122)))));
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(383, 180);
            this.Controls.Add(this.tblMainLayout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DialogBox";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MessageBox";
            this.Load += new System.EventHandler(this.DialogBox_Load);
            this.SizeChanged += new System.EventHandler(this.CustomDialogBox_SizeChanged);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tblHeader.ResumeLayout(false);
            this.tblHeader.PerformLayout();
            this.tblMainLayout.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblText;
        private System.Windows.Forms.TableLayoutPanel tblHeader;
        private System.Windows.Forms.Label lblHeaderText;
        private System.Windows.Forms.Button btnMsgIndication;
        private System.Windows.Forms.TableLayoutPanel tblMainLayout;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClose;

    }
}