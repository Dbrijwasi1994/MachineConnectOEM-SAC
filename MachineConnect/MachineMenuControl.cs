using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using AxAcroPDFLib;
using MachineConnectOEM;

namespace MachineConnectApplication
{
    public partial class MachineMenuControl : UserControl
    {
        string appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static string headerValue = "MANUALS AND CALCULATORS";
        public MachineManual userControl = null;
        string manualsFolderPath = string.Empty;
        string manualsMTBPath = string.Empty;
        string manualsProductPath = string.Empty;
        string machineModel = string.Empty;
        public MachineMenuControl(UserControl usrControl)
        {
            InitializeComponent();
            userControl = usrControl as MachineManual;
            btnCalCulation.Visible = false;
            if (DatabaseAccess.GetMTB(HomeScreen.selectedMachine) == "MGTL")
            {
                btnCalCulation.Visible = true;
            }

        }

        private void btnDuty_Click(object sender, EventArgs e)
        {
            try
            {
                //headerValue = "NAVIGATION MANUAL";
                headerValue = "ELECTRICAL MANUAL";

                manualsFolderPath = DatabaseAccess.GetGenericMachineConnectFolderPath();
                manualsMTBPath = DatabaseAccess.GetAlarmsMTBPath(HomeScreen.selectedMachine, out machineModel);
                manualsProductPath = DatabaseAccess.GetProductPathForMTB(HomeScreen.selectedMachine, manualsMTBPath);

                if (string.IsNullOrEmpty(manualsFolderPath))
                {
                    // For Navigation Manual
                    //manualsFolderPath = System.IO.Path.Combine(Settings.APP_PATH, "AlarmsAndDocs", manualsMTBPath, "Navigation Manual");
                    manualsFolderPath = Path.Combine(Settings.APP_PATH, "AlarmsAndDocs", manualsMTBPath, "Manuals", "Electrical Manual");
                }

                string doc = Path.Combine(manualsFolderPath);
                if (!Directory.Exists(doc)) return;
                else Process.Start(doc);
                //var file = Directory.GetFiles(doc);
                //if (file.Count() > 0)
                //{
                //    if (userControl != null)
                //    {
                //        this.userControl.btnBack.Visible = true;
                //        this.userControl.lblHeader.Text = headerValue;
                //    }
                //    this.btnElectrical.ForeColor = Color.Orange;
                //    DisposePanelControls();
                //    pnlContainer.Controls.Clear();

                //    AxAcroPDFLib.AxAcroPDF pdf = new AxAcroPDFLib.AxAcroPDF();
                //    pdf.Dock = System.Windows.Forms.DockStyle.Fill;
                //    pdf.Enabled = true;
                //    pdf.Location = new System.Drawing.Point(0, 0);
                //    pdf.Name = "pdfReader";
                //    pdf.TabIndex = 1;
                //    pnlContainer.Controls.Add(pdf);
                //    pdf.LoadFile(file[0].ToString());
                //    pdf.setZoom(110.0F);
                //    Application.DoEvents();
                //    pdf.Focus();
                //    Application.DoEvents();
                //    SendKeys.Send("^L");
                //    Application.DoEvents();
                //    pnlContainer.Focus();

                //}
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("Loading PDF File..!! \n Error - " + ex.Message);
                CustomDialogBox frm = new CustomDialogBox("Error Message", ex.Message);
                frm.ShowDialog();
            }
        }

        private void ShowImplementationMessage()
        {
            CustomDialogBox cmb = new CustomDialogBox("Information Message", "Implementation under Progress");
            cmb.ShowDialog();
        }

        private void btnPowerCalci_Click(object sender, EventArgs e)
        {
            // FOR POWER CALCULATOR
            //headerValue = "SIMPLE POWER CALCULATOR";
            ////ShowImplementationMessage();
            //if (userControl != null)
            //{
            //    this.userControl.lblHeader.Text = headerValue;
            //    this.userControl.btnBack.Visible = true;
            //}
            //this.btnElectrical.ForeColor = Color.Orange;
            //DisposePanelControls();
            //pnlContainer.Controls.Clear();

            //PowerCalci ctrl = new PowerCalci();
            //ctrl.Dock = DockStyle.Fill;
            //pnlContainer.Controls.Add(ctrl);
            try
            {
                headerValue = "APPLICATION MANUAL";

                manualsFolderPath = DatabaseAccess.GetGenericMachineConnectFolderPath();
                manualsMTBPath = DatabaseAccess.GetAlarmsMTBPath(HomeScreen.selectedMachine, out machineModel);
                manualsProductPath = DatabaseAccess.GetProductPathForMTB(HomeScreen.selectedMachine, manualsMTBPath);

                if (string.IsNullOrEmpty(manualsFolderPath))
                {
                    //manualsFolderPath = System.IO.Path.Combine(Settings.APP_PATH, "AlarmsAndDocs");
                    manualsFolderPath = Path.Combine(Settings.APP_PATH, "AlarmsAndDocs", manualsMTBPath, "Manuals", "Application Manual");
                }

                string doc = Path.Combine(manualsFolderPath);
                if (!Directory.Exists(doc)) return;
                else Process.Start(doc);
                //var file = Directory.GetFiles(doc);
                //if (file.Count() > 0)
                //{
                //    if (userControl != null)
                //    {
                //        this.userControl.btnBack.Visible = true;
                //        this.userControl.lblHeader.Text = headerValue;
                //    }
                //    this.btnElectrical.ForeColor = Color.Orange;
                //    DisposePanelControls();
                //    pnlContainer.Controls.Clear();

                //    AxAcroPDFLib.AxAcroPDF pdf = new AxAcroPDFLib.AxAcroPDF();
                //    pdf.Dock = System.Windows.Forms.DockStyle.Fill;
                //    pdf.Enabled = true;
                //    pdf.Location = new System.Drawing.Point(0, 0);
                //    pdf.Name = "pdfReader";
                //    pdf.TabIndex = 1;
                //    pnlContainer.Controls.Add(pdf);
                //    pdf.LoadFile(file[0].ToString());
                //    pdf.setZoom(110.0F);
                //    Application.DoEvents();
                //    pdf.Focus();
                //    Application.DoEvents();
                //    SendKeys.Send("^L");
                //    Application.DoEvents();
                //    pnlContainer.Focus();

                //}
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("Loading PDF File..!! \n Error - " + ex.Message);
                CustomDialogBox frm = new CustomDialogBox("Error Message", ex.Message);
                frm.ShowDialog();
            }

        }

        private void DisposePanelControls()
        {
            foreach (Control p in pnlContainer.Controls)
            {
                p.Dispose();
            }
        }

        private void btnElectrical_Click(object sender, EventArgs e)
        {
            try
            {
                headerValue = "WARRANTY CARD";

                manualsFolderPath = DatabaseAccess.GetGenericMachineConnectFolderPath();
                manualsMTBPath = DatabaseAccess.GetAlarmsMTBPath(HomeScreen.selectedMachine, out machineModel);
                manualsProductPath = DatabaseAccess.GetProductPathForMTB(HomeScreen.selectedMachine, manualsMTBPath);

                if (string.IsNullOrEmpty(manualsFolderPath))
                {
                    //manualsFolderPath = System.IO.Path.Combine(Settings.APP_PATH, "AlarmsAndDocs");
                    manualsFolderPath = Path.Combine(Settings.APP_PATH, "AlarmsAndDocs", manualsMTBPath, "Manuals", "Warranty Card");
                }

                string doc = Path.Combine(manualsFolderPath);
                if (!Directory.Exists(doc)) return;
                else Process.Start(doc);
                //var file = Directory.GetFiles(doc);
                //if (file.Count() > 0)
                //{
                //    if (userControl != null)
                //    {
                //        this.userControl.btnBack.Visible = true;
                //        this.userControl.lblHeader.Text = headerValue;
                //    }
                //    this.btnElectrical.ForeColor = Color.Orange;
                //    DisposePanelControls();
                //    pnlContainer.Controls.Clear();

                //    AxAcroPDFLib.AxAcroPDF pdf = new AxAcroPDFLib.AxAcroPDF();
                //    pdf.Dock = System.Windows.Forms.DockStyle.Fill;
                //    pdf.Enabled = true;
                //    pdf.Location = new System.Drawing.Point(0, 0);
                //    pdf.Name = "pdfReader";
                //    pdf.TabIndex = 1;
                //    pnlContainer.Controls.Add(pdf);
                //    pdf.LoadFile(file[0].ToString());
                //    pdf.setZoom(110.0F);
                //    Application.DoEvents();
                //    pdf.Focus();
                //    Application.DoEvents();
                //    SendKeys.Send("^L");
                //    Application.DoEvents();
                //    pnlContainer.Focus();

                //}
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("Loading PDF File..!! \n Error - " + ex.Message);
                CustomDialogBox frm = new CustomDialogBox("Error Message", ex.Message);
                frm.ShowDialog();
            }
        }

        private void btnManual_Click(object sender, EventArgs e)
        {
            manualsFolderPath = DatabaseAccess.GetGenericMachineConnectFolderPath();
            manualsMTBPath = DatabaseAccess.GetAlarmsMTBPath(HomeScreen.selectedMachine, out machineModel);
            manualsProductPath = DatabaseAccess.GetProductPathForMTB(HomeScreen.selectedMachine, manualsMTBPath);


            if (string.IsNullOrEmpty(manualsFolderPath))
            {
                //manualsFolderPath = System.IO.Path.Combine(Settings.APP_PATH, "AlarmsAndDocs", manualsMTBPath, "User Manual");
                manualsFolderPath = Path.Combine(Settings.APP_PATH, "AlarmsAndDocs", manualsMTBPath, "Manuals", "Machine Backup Manual");
            }

            string doc = Path.Combine(manualsFolderPath);
            if (!Directory.Exists(doc)) return;
            else Process.Start(doc);
            //var file = Directory.GetFiles(doc);
            //if (file.Count() > 0)
            //{
            //    try
            //    {
            //        if (userControl != null)
            //        {
            //            this.userControl.btnBack.Visible = true;
            //            this.userControl.lblHeader.Text = "Machine Backup Manual"; ;
            //        }
            //        this.btnElectrical.ForeColor = Color.Orange;
            //        DisposePanelControls();
            //        pnlContainer.Controls.Clear();

            //        AxAcroPDFLib.AxAcroPDF pdf = new AxAcroPDFLib.AxAcroPDF();

            //        pdf.Dock = System.Windows.Forms.DockStyle.Fill;
            //        pdf.Enabled = true;
            //        pdf.Location = new System.Drawing.Point(0, 0);
            //        pdf.Name = "pdfReader";
            //        pnlContainer.Controls.Add(pdf);
            //        pdf.LoadFile(file[0].ToString());
            //        pdf.Visible = true;
            //        pdf.BringToFront();
            //        pdf.setZoom(90.0F);
            //        Application.DoEvents();
            //        pdf.Focus();                 
            //        SendKeys.Send("^L");
            //        Application.DoEvents();
            //        pnlContainer.Focus();
            //    }                
            //    catch (Exception ex)
            //    {
            //        Logger.WriteErrorLog("Loading PDF File..!! \n Error - " + ex.Message);
            //        CustomDialogBox frm = new CustomDialogBox("Error Message", ex.Message);
            //        frm.ShowDialog();
            //    }

            //}
        }

        private void btnDailyCheckList_Click(object sender, EventArgs e)
        {
            if (DatabaseAccess.GetMTB(HomeScreen.selectedMachine) == "MGTL" || DatabaseAccess.GetMTB(HomeScreen.selectedMachine) == "AMS")
            {
                manualsFolderPath = DatabaseAccess.GetGenericMachineConnectFolderPath();
                manualsMTBPath = DatabaseAccess.GetAlarmsMTBPath(HomeScreen.selectedMachine, out machineModel);
                manualsProductPath = DatabaseAccess.GetProductPathForMTB(HomeScreen.selectedMachine, manualsMTBPath);


                if (string.IsNullOrEmpty(manualsFolderPath))
                {
                    // For Daily Checklist
                    //manualsFolderPath = System.IO.Path.Combine(Settings.APP_PATH, "AlarmsAndDocs", manualsMTBPath, "Daily CheckList");
                    manualsFolderPath = Path.Combine(Settings.APP_PATH, "AlarmsAndDocs", manualsMTBPath, "Manuals", "Mechanical Manual");
                }

                string doc = Path.Combine(manualsFolderPath);
                if (!Directory.Exists(doc)) return;
                else System.Diagnostics.Process.Start(doc);
                //var file = Directory.GetFiles(doc);
                //if (file.Count() > 0)
                //{
                //    try
                //    {
                //        if (userControl != null)
                //        {
                //            this.userControl.btnBack.Visible = true;
                //            this.userControl.lblHeader.Text = "Mechanical Manual"; ;
                //        }
                //        this.btnElectrical.ForeColor = Color.Orange;
                //        DisposePanelControls();
                //        pnlContainer.Controls.Clear();

                //        AxAcroPDFLib.AxAcroPDF pdf = new AxAcroPDFLib.AxAcroPDF();

                //        pdf.Dock = System.Windows.Forms.DockStyle.Fill;
                //        pdf.Enabled = true;
                //        pdf.Location = new System.Drawing.Point(0, 0);
                //        pdf.Name = "pdfReader";
                //        pnlContainer.Controls.Add(pdf);
                //        pdf.LoadFile(file[0].ToString());
                //        pdf.Visible = true;
                //        pdf.BringToFront();
                //        pdf.setZoom(90.0F);
                //        Application.DoEvents();
                //        pdf.Focus();
                //        SendKeys.Send("^L");
                //        Application.DoEvents();
                //        pnlContainer.Focus();
                //    }
                //    catch (Exception ex)
                //    {
                //        Logger.WriteErrorLog("Loading PDF File..!! \n Error - " + ex.Message);
                //        CustomDialogBox frm = new CustomDialogBox("Error Message", ex.Message);
                //        frm.ShowDialog();
                //    }

                //}
            }
            else
            {
                headerValue = "OPERATOR MAINTENENCE CHECKLIST";
                //ShowImplementationMessage();
                if (userControl != null)
                {
                    this.userControl.lblHeader.Text = headerValue;
                    this.userControl.btnBack.Visible = true;
                }
                this.btnElectrical.ForeColor = Color.Orange;
                DisposePanelControls();
                pnlContainer.Controls.Clear();

                OperatorMaintenanceCheckList ctrl = new OperatorMaintenanceCheckList();
                ctrl.Dock = DockStyle.Fill;
                pnlContainer.Controls.Add(ctrl);
            }
        }


        private void MachineMenuControl_Load(object sender, EventArgs e)
        {
            //manualsFolderPath = DatabaseAccess.GetGenericMachineConnectFolderPath();
            //manualsMTBPath = DatabaseAccess.GetAlarmsMTBPath(HomeScreen.selectedMachine);
            //manualsProductPath = DatabaseAccess.GetProductPathForMTB(HomeScreen.selectedMachine, manualsMTBPath);
        }

        private void btnCalCulation_Click(object sender, EventArgs e)
        {
            // FOR OTHER CALCULATOR PAGE
            //headerValue = "OTHER CALCULATOR";
            ////ShowImplementationMessage();
            //if (userControl != null)
            //{
            //    this.userControl.lblHeader.Text = headerValue;
            //    this.userControl.btnBack.Visible = true;
            //}
            //this.btnCalCulation.ForeColor = Color.Orange;
            //DisposePanelControls();
            //pnlContainer.Controls.Clear();

            //ButtonTable_UserControl ctrl = new ButtonTable_UserControl(this.userControl);
            //ctrl.Dock = DockStyle.Fill;
            //pnlContainer.Controls.Add(ctrl);

            manualsFolderPath = DatabaseAccess.GetGenericMachineConnectFolderPath();
            manualsMTBPath = DatabaseAccess.GetAlarmsMTBPath(HomeScreen.selectedMachine, out machineModel);
            manualsProductPath = DatabaseAccess.GetProductPathForMTB(HomeScreen.selectedMachine, manualsMTBPath);


            if (string.IsNullOrEmpty(manualsFolderPath))
            {
                manualsFolderPath = Path.Combine(Settings.APP_PATH, "AlarmsAndDocs", manualsMTBPath, "Manuals", "Machine Checklist");
            }

            string doc = Path.Combine(manualsFolderPath);
            if (!Directory.Exists(doc)) return;
            var file = Directory.GetFiles(doc);
            if (file.Count() > 0)
            {
                try
                {
                    if (userControl != null)
                    {
                        this.userControl.btnBack.Visible = true;
                        this.userControl.lblHeader.Text = "Machine Checklist"; ;
                    }
                    this.btnElectrical.ForeColor = Color.Orange;
                    DisposePanelControls();
                    pnlContainer.Controls.Clear();

                    AxAcroPDFLib.AxAcroPDF pdf = new AxAcroPDFLib.AxAcroPDF();

                    pdf.Dock = System.Windows.Forms.DockStyle.Fill;
                    pdf.Enabled = true;
                    pdf.Location = new System.Drawing.Point(0, 0);
                    pdf.Name = "pdfReader";
                    pnlContainer.Controls.Add(pdf);
                    pdf.LoadFile(file[0].ToString());
                    pdf.Visible = true;
                    pdf.BringToFront();
                    pdf.setZoom(90.0F);
                    Application.DoEvents();
                    pdf.Focus();
                    SendKeys.Send("^L");
                    Application.DoEvents();
                    pnlContainer.Focus();
                }
                catch (Exception ex)
                {
                    Logger.WriteErrorLog("Loading PDF File..!! \n Error - " + ex.Message);
                    CustomDialogBox frm = new CustomDialogBox("Error Message", ex.Message);
                    frm.ShowDialog();
                }
            }
        }
    }
}



