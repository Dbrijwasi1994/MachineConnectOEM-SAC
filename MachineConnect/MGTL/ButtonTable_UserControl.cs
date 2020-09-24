using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MachineConnectApplication;

namespace MachineConnectOEM
{
    public partial class ButtonTable_UserControl : UserControl
    {
        public static string headerValue = "OTHER CALCULATORS";
        public MachineManual userControl2 = null;
        public ButtonTable_UserControl(UserControl userControl)
        {
            InitializeComponent();
            userControl2 = userControl as MachineManual;
            if (userControl != null)
            {
                headerValue = "Dressing Time";
                btnDressingTime.ForeColor = Color.Orange;
                btnIDcycleTime.ForeColor = Color.White;
                btnOd.ForeColor = Color.White;
                btnOutPut.ForeColor = Color.White;
                userControl2.lblHeader.Text = headerValue;
                userControl2.btnBack.Visible = true;
            }
        }

        private void btnDressingTime_Click(object sender, EventArgs e)
        {
            headerValue = "Dressing Time";
            btnDressingTime.ForeColor = Color.Orange;
            btnIDcycleTime.ForeColor = Color.White;
             btnOd.ForeColor = Color.White;
             btnOutPut.ForeColor = Color.White;
            if (userControl2 != null)
            {
                this.userControl2.lblHeader.Text = headerValue;
                this.userControl2.btnBack.Visible = true;
            }
            outPut_UserControl1.Visible = false;
            odCalculator1.Visible = false;
            dressingTime_UserControl1.Visible = true;
            this.idcycleTimeUserControl1.Visible = false;
            dressingTime_UserControl1.Dock = DockStyle.Fill;
        }

      

        private void btnIDcycleTime_Click(object sender, EventArgs e)
        {
            headerValue = "ID Cycle Time";

            btnDressingTime.ForeColor = Color.White;
            btnIDcycleTime.ForeColor = Color.Orange;
            btnOd.ForeColor = Color.White;
            btnOutPut.ForeColor = Color.White;

            if (userControl2 != null)
            {
                this.userControl2.lblHeader.Text = headerValue;
                this.userControl2.btnBack.Visible = true;
            }
           
            
            outPut_UserControl1.Visible = false;
            odCalculator1.Visible = false;
            this.dressingTime_UserControl1.Visible = false;
            idcycleTimeUserControl1.Visible = true;
            this.idcycleTimeUserControl1.dressingTime = dressingTime_UserControl1.DressingTime;
            this.idcycleTimeUserControl1.Rappidapproach = dressingTime_UserControl1.RapidApproach;
            idcycleTimeUserControl1.Dock = DockStyle.Fill;

        }

        private void btnOd_Click(object sender, EventArgs e)
        {
            headerValue = "OD Cycle Time";




            btnDressingTime.ForeColor = Color.White;
            btnIDcycleTime.ForeColor = Color.White;
            btnOd.ForeColor = Color.Orange;
            btnOutPut.ForeColor = Color.White;
       

            if (userControl2 != null)
            {
                this.userControl2.lblHeader.Text = headerValue;
                this.userControl2.btnBack.Visible = true;
            }
            odCalculator1.RapidForwardReturnText = dressingTime_UserControl1.RapidForwardreturnOd;
            odCalculator1.TotalDressingTime = dressingTime_UserControl1.DressingTimeOD;
            outPut_UserControl1.Visible = false;
            idcycleTimeUserControl1.Visible = false;
            dressingTime_UserControl1.Visible = false;
            odCalculator1.Visible = true;
            odCalculator1.Dock = DockStyle.Fill;
        }

        private void btnOutPut_Click(object sender, EventArgs e)
        {
            //OutPut_UserControl OutPut_UserControl1 = new OutPut_UserControl();
            headerValue = "Summary";
            btnDressingTime.ForeColor = Color.White;
            btnIDcycleTime.ForeColor = Color.White;
            btnOd.ForeColor = Color.White;
            btnOutPut.ForeColor = Color.Orange;

            if (userControl2 != null)
            {
                this.userControl2.lblHeader.Text = headerValue;
                this.userControl2.btnBack.Visible = true;
            }
           

            idcycleTimeUserControl1.Visible = false;
            dressingTime_UserControl1.Visible = false;
            odCalculator1.Visible = false;
            this.outPut_UserControl1.DressingOD = dressingTime_UserControl1.TotalDressingTimeOd;
            this.outPut_UserControl1.dressingTimeID = dressingTime_UserControl1.DressingTime;
            this.outPut_UserControl1.TotalCuttingTimeID = idcycleTimeUserControl1.CycleTime;
            this.outPut_UserControl1.TotalCuttingTimeOD = odCalculator1.CuttingTimeOd;
            this.outPut_UserControl1.RapidForwardReturn = odCalculator1.RapidForwardReturnText;
            this.outPut_UserControl1.RapidApproachID = dressingTime_UserControl1.RapidApproach;
            this.outPut_UserControl1.RapidForwardReturn = dressingTime_UserControl1.RapidForwardreturnOd;
           // this.outPut_UserControl1.DressingOD = odCalculator1.DressingOD;
            this.outPut_UserControl1.Dock = DockStyle.Fill;
            outPut_UserControl1.Visible = true;
           
        }

       
      
    }
}
