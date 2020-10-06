using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChartDirector;
using System.IO;
using System.Threading.Tasks;
using AxAcroPDFLib;
using CNC_PT;
using FocasGUI;
using MachineConnectOEM;
using MachineConnectOEM.SAC;

namespace MachineConnectApplication
{
    public partial class MainScreen : Form
    {
        public bool HideCNCPreventiveAlaramViewer;
        string CNCAlarmCount = string.Empty;
        string PreventiveAlarmCount = string.Empty;
        public static string CURRENT_DATE_TIME = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public DateTime NEXT_REFRESH_DATE_TIME = DateTime.Now.Date.AddDays(1);
        bool isHomeScreenView = false;
        bool formLoad = true;

        public static bool RunChartDateTime = true;
        Task backgroundTask = null;
        TaskScheduler uiThreadScheduler = null;
        string machineStatus = string.Empty;
        private string MachineStatusAsImage;
        private string LastDateTime;
        public static string LOGICAL_DAY_END;
        List<string> AllMachines = null;
        public MainScreen()
        {
            //Commented by satya on 15 may 2016 to solve the WPF control disappear issue
            //this.DoubleBuffered = true;
            //this.SetStyle(ControlStyles.UserPaint |
            //              ControlStyles.AllPaintingInWmPaint |
            //              ControlStyles.ResizeRedraw |
            //              ControlStyles.ContainerControl |
            //              ControlStyles.OptimizedDoubleBuffer |
            //              ControlStyles.SupportsTransparentBackColor
            //              , true);

            InitializeComponent();
            //var companyLogo = Directory.GetFiles(Path.Combine(Settings.APP_PATH, @"CompanyLogo"));
            //btnLogo.BackgroundImage = (Image.FromFile(companyLogo[0].ToString()));             
            //// companyLogo = Directory.GetFiles(Path.Combine(Settings.APP_PATH, @"CompanyLogo"));
            //btnLogo.BackgroundImage = (Image.FromFile(companyLogo[0].ToString()));
            
            this.WindowState = FormWindowState.Maximized;
            string MTB = DatabaseAccess.GetMTB(HomeScreen.selectedMachine);
            var companyLogo = Path.Combine(Settings.APP_PATH, @"CompanyLogo", MTB + ".png");
            if (File.Exists(companyLogo))
            {
                btnLogo.BackgroundImage = Image.FromFile(companyLogo);
            }
            else
            {
                //var companyLogo = Path.Combine(Settings.APP_PATH, @"CompanyLogo", "CompanyLogo.png");
                btnLogo.BackgroundImage = Image.FromFile(Path.Combine(Settings.APP_PATH, @"CompanyLogo", "CompanyLogo.png"));
            }

            lblDateTime.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt");
            btnPredectiveAlerts.Text = "PRODUCTION ALERTS"; //"  CNC" + Environment.NewLine + "   ALERTS";
            btnProgramTransfer.Text = "MACHINE "+ Environment.NewLine +" PARAMETERS"; //" PROGRAM " + Environment.NewLine + " TRANSFER";
            //btnMachineManual.Text = "CNC ALARMS";//"MANUALS AND " + Environment.NewLine + "  CALCULATORS ";
            //btnSpindleParameters.Text = "AXIS AND " + Environment.NewLine + " SPINDLE";//"   MACHINE  " + Environment.NewLine + "  PARAMETERS ";
            //btnMaintenanceSchedule.UseMnemonic = false;
            //btnMaintenanceSchedule.Text = "PREDICTIVE & "+ Environment.NewLine +" PREVENTIVE";//"   PREDICTIVE MAINTENANCE " + Environment.NewLine + "  SCHEDULE ";
            btnPRGP7.Text = "PROGRAM " + Environment.NewLine + " TRANSFER";

            CURRENT_DATE_TIME = (DatabaseAccess.GetShiftStartEndTimeForDay(1, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))).ToString("yyyy-MM-dd HH:mm:ss");
            LOGICAL_DAY_END = (DatabaseAccess.GetShiftStartEndTimeForDay(0, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))).ToString("yyyy-MM-dd HH:mm:ss");
        }

        public MainScreen(string ViewType)
        {
            //Commented by satya on 15 may 2016 to solve the WPF control disappear issue
            //this.DoubleBuffered = true;
            //this.SetStyle(ControlStyles.UserPaint |
            //              ControlStyles.AllPaintingInWmPaint |
            //              ControlStyles.ResizeRedraw |
            //              ControlStyles.ContainerControl |
            //              ControlStyles.OptimizedDoubleBuffer |
            //              ControlStyles.SupportsTransparentBackColor
            //              , true);

            InitializeComponent();
            btnSettings_Click(null, EventArgs.Empty);
        }

        //Commented by satya on 15 may 2016 to solve the WPF control disappear issue
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= 0x02000000;
        //        return cp;
        //    }
        //}

        private void lblHeader_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer1.Dispose();
            DisposePanelControls();
            Application.Exit();
        }

        private void MainScreen_Shown(object sender, EventArgs e)
        {
            btnCurent.Text = DateTime.Now.ToString("dd MMM");
            btnCurent.BackgroundImage = Image.FromFile(Path.Combine(Settings.APP_PATH, "Images", "SelectedDate.png"));
            btnCurent.ForeColor = Color.White;

            btnPrev1.Text = DateTime.Now.AddDays(-1).ToString("dd MMM");

            btnPrev2.Text = DateTime.Now.AddDays(-2).ToString("dd MMM");

            if ((LoginForm.LoginUserName.Equals("admin", StringComparison.InvariantCultureIgnoreCase) &&
                LoginForm.LoginPassword.Equals("admin$4321", StringComparison.InvariantCultureIgnoreCase)) ||
                (LoginForm.LoginUserName.Equals(Settings.SuperAdmin_UserName) && LoginForm.LoginPassword.Equals(Settings.SuperAdmin_Password)))
            {
                btnSettings.Visible = true;
            }
            else
            {
                btnSettings.Visible = true;
            }


            AllMachines = DatabaseAccess.GetAllMachines();
            List<string> plants = DatabaseAccess.GetAllPlants1();

            cmbPlant.DataSource = plants;
            if (cmbPlant.Items.Count > 1)
            {
                cmbPlant.SelectedIndex = 1;
                lblPlant.Text = cmbPlant.SelectedItem.ToString();
                cmbMachines.DataSource = DatabaseAccess.GetMachinesByPlant1(cmbPlant.SelectedItem.ToString());
            }
            else
            {
                cmbMachines.DataSource = AllMachines;
            }
            HomeScreen.selectedMachine = cmbMachines.SelectedItem.ToString();


            uiThreadScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            btnPreventiveAlarmCount.Location = new System.Drawing.Point(pnlCNCPreventiveAlarm.Width - 43, 2);
            cmbMachines.Text = HomeScreen.selectedMachine;
            //cmbMachines.BringToFront();
            timer1_Tick(this, EventArgs.Empty);
            //btnCurent_Click(null, EventArgs.Empty);
            LoadHomeScreen();


            string MTB = DatabaseAccess.GetMTB(HomeScreen.selectedMachine);
            var companyLogo = Path.Combine(Settings.APP_PATH, @"CompanyLogo", MTB + ".png");
            if (File.Exists(companyLogo))
            {
                btnLogo.BackgroundImage = Image.FromFile(companyLogo);
            }
            else
            {
                //var companyLogo = Path.Combine(Settings.APP_PATH, @"CompanyLogo", "CompanyLogo.png");
                btnLogo.BackgroundImage = Image.FromFile(Path.Combine(Settings.APP_PATH, @"CompanyLogo", "CompanyLogo.png"));
            }
            timer1.Interval = Settings.StatusUpdateTimerIntervalInSec < 10 ? (int)TimeSpan.FromSeconds(10).TotalMilliseconds : (int)TimeSpan.FromSeconds(Settings.StatusUpdateTimerIntervalInSec).TotalMilliseconds;
            timerANDON.Interval = Settings.ANDONFlipInterval < 10 ? (int)TimeSpan.FromSeconds(10).TotalMilliseconds : (int)TimeSpan.FromSeconds(Settings.ANDONFlipInterval).TotalMilliseconds;
            timer1.Enabled = false;
            timerANDON.Enabled = false;

            formLoad = false;
        }

        private void LoadHomeScreen()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Dashboard_Sac_Win ctrl = new Dashboard_Sac_Win();
                ctrl.Dock = DockStyle.Fill;
                if (Settings.InAndonMode)
                {
                    //DisposePanelControls();
                    //pnlContainer.Controls.Clear();
                    pnlContainer.Controls.Add(ctrl);
                    if (pnlContainer.Controls.Count > 1)
                    {
                        var ctrl1 = pnlContainer.Controls[0];
                        ctrl1.Dispose();
                        //pnlContainer.Controls.RemoveAt(0);
                    }
                }
                else
                {
                    DisposePanelControls();
                    pnlContainer.Controls.Clear();
                    pnlContainer.Controls.Add(ctrl);
                }
               

                lblMacName.Text = cmbMachines.SelectedItem.ToString();
                lblMacName.BringToFront();
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            SetButonsDefaultForeColor();
            isHomeScreenView = false;
            SetButonsDefaultForeColor();

            DisposePanelControls();
            pnlContainer.Controls.Clear();
            Dashboard_Sac_Win ctrl = new Dashboard_Sac_Win();
            ctrl.Dock = DockStyle.Fill;
            pnlContainer.Controls.Add(ctrl);
            this.Cursor = Cursors.Default;
        }

        private void DisposePanelControls()
        {
            //pnlContainer.Visible = false;

            foreach (Control p in pnlContainer.Controls)
            {
                p.Dispose();
            }
            //pnlContainer.Visible = true;
        }

        private void SetButonsDefaultForeColor()
        {
            btnUpgrade.ForeColor = Color.Black;
            btnUpgrade.BackColor = Color.Transparent;

            btnPredectiveAlerts.ForeColor = Color.White;
            btnPredectiveAlerts.BackColor = ColorTranslator.FromHtml("0x1F497D");

            btnMaintenanceSchedule.ForeColor = Color.White;
            btnMaintenanceSchedule.BackColor = ColorTranslator.FromHtml("0x1F497D");

            btnCNCAlarmCount.BackColor = ColorTranslator.FromHtml("0x1F497D");
            btnPredectiveAlerts.BackColor = ColorTranslator.FromHtml("0x1F497D");

            btnSpindleParameters.ForeColor = Color.White;
            btnSpindleParameters.BackColor = ColorTranslator.FromHtml("0x1F497D");

            btnProgramTransfer.ForeColor = Color.White;
            btnProgramTransfer.BackColor = ColorTranslator.FromHtml("0x1F497D");

            btnMachineManual.ForeColor = Color.White;
            btnMachineManual.BackColor = ColorTranslator.FromHtml("0x1F497D");

            btnMenuIcon.ForeColor = Color.White;
            //btnMenuIcon.BackColor = ColorTranslator.FromHtml("0x1F497D");
            btnPRGP7.ForeColor = Color.White;
            btnPRGP7.BackColor = ColorTranslator.FromHtml("0x1F497D");
            
        }

        private void btnPredectiveAlerts_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            isHomeScreenView = false;
            SetButonsDefaultForeColor();
            btnPredectiveAlerts.BackColor = ColorTranslator.FromHtml("0x4040A0");
            btnPredectiveAlerts.ForeColor = Color.Orange;
            DisposePanelControls();
            pnlContainer.Controls.Clear();
            HomeScreen ctrl = new HomeScreen(this);
            ctrl.Dock = DockStyle.Fill;
            pnlContainer.Controls.Add(ctrl);
            this.Cursor = Cursors.Default;
            lblDateTime.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt");
        }

        private void btnSpindleParameters_Click(object sender, EventArgs e)
        {
            SetButonsDefaultForeColor();
            isHomeScreenView = false;
            SetButonsDefaultForeColor();
            btnSpindleParameters.ForeColor = Color.Orange;
            btnSpindleParameters.BackColor = ColorTranslator.FromHtml("0x4040A0");
            DisposePanelControls();
            pnlContainer.Controls.Clear();
            IRSchedule_Win ctrl = new IRSchedule_Win();
            ctrl.Dock = DockStyle.Fill;
            pnlContainer.Controls.Add(ctrl);
            this.Cursor = Cursors.Default;
        }

        private void btnMachineManual_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            isHomeScreenView = false;
            SetButonsDefaultForeColor();
            btnMachineManual.BackColor = ColorTranslator.FromHtml("0x4040A0");
            btnMachineManual.ForeColor = Color.Orange;
            DisposePanelControls();
            pnlContainer.Controls.Clear();
            RPM ctrl = new RPM();
            ctrl.Dock = DockStyle.Fill;
            pnlContainer.Controls.Add(ctrl);
            this.Cursor = Cursors.Default;
            lblDateTime.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt");
        }

        private void LoadMachineManualControl()
        {
            this.Cursor = Cursors.WaitCursor;

            SetButonsDefaultForeColor();
            btnMachineManual.BackColor = ColorTranslator.FromHtml("0x4040A0");
            btnMachineManual.ForeColor = Color.Orange;

            DisposePanelControls();
            pnlContainer.Controls.Clear();

            MachineManual ctrl = new MachineManual();
            ctrl.Dock = DockStyle.Fill;
            pnlContainer.Controls.Add(ctrl);
            this.Cursor = Cursors.Default;

            lblMachineStatus.Width = cmbMachines.Width;
            lblMacName.Width = cmbMachines.Width;
            lblPlant.Width = cmbPlant.Width;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            if (DateTime.Now > NEXT_REFRESH_DATE_TIME) //Doubt Date Time.Now????
            {
                btnCurent.Text = DateTime.Now.ToString("dd MMM");
                btnCurent.ForeColor = Color.White;

                btnPrev1.Text = DateTime.Now.AddDays(-1).ToString("dd MMM");

                btnPrev2.Text = DateTime.Now.AddDays(-2).ToString("dd MMM");

                NEXT_REFRESH_DATE_TIME.Date.AddDays(1);
            }

            if (backgroundTask != null && backgroundTask.Status == TaskStatus.Running) return;
            lblDateTime.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt");

            LoadDataForTheSelectedDate();

        }

        private void LoadDataForTheSelectedDate()
        {
            string selectedMachine = HomeScreen.selectedMachine;

            var stopageBackgroundTask = new Task(() =>
            {
                CNCAlarmCount = DatabaseAccess.GetCNCOrPreventiveAlaramCount(selectedMachine, CURRENT_DATE_TIME, "Alarmcount");
                PreventiveAlarmCount = DatabaseAccess.GetCNCOrPreventiveAlaramCount(selectedMachine, CURRENT_DATE_TIME, "PreventiveAlarmcount");
                machineStatus = DatabaseAccess.GetMachineStatus(selectedMachine, out MachineStatusAsImage, out LastDateTime);
            });

            var stoppageTask = stopageBackgroundTask.ContinueWith(t =>
            {
                timer1.Enabled = true;
                if (t.IsFaulted)
                {
                    Logger.WriteErrorLog(t.Exception.ToString());
                    this.UseWaitCursor = false;
                    return;
                }

                btnCNCAlarmCount.Text = CNCAlarmCount;
                btnPreventiveAlarmCount.Text = PreventiveAlarmCount;
                lblMachineStatus.Text = "[ " + machineStatus + " ]";

                if (MachineStatusAsImage.Equals("Stopped"))
                {
                    picBxMacStatus.Image = Image.FromFile(Path.Combine(Settings.APP_PATH, "Images", "Gif", "NetworkNotOk.png"));
                    this.toolTip1.SetToolTip(this.picBxMacStatus, "Connection Status - NOT-CONNECTED" + " | " + LastDateTime);
                }
                else
                {
                    picBxMacStatus.Image = Image.FromFile(Path.Combine(Settings.APP_PATH, "Images", "Gif", "NetworkOk.png"));
                    this.toolTip1.SetToolTip(this.picBxMacStatus, "Connection Status - CONNECTED" + " | " + LastDateTime);
                }

            }, uiThreadScheduler);
            stopageBackgroundTask.Start();
        }

        private void btnMaintenanceSchedule_Click(object sender, EventArgs e)
        {
            isHomeScreenView = false;
            this.Cursor = Cursors.WaitCursor;
            SetButonsDefaultForeColor();
            btnMaintenanceSchedule.BackColor = ColorTranslator.FromHtml("0x4040A0");
            btnMaintenanceSchedule.ForeColor = Color.Orange;
            DisposePanelControls();
            pnlContainer.Controls.Clear();
            CNCAndPredictiveAlarmControl ctrl = new CNCAndPredictiveAlarmControl();
            ctrl.Dock = DockStyle.Fill;
            pnlContainer.Controls.Add(ctrl);
            this.Cursor = Cursors.Default;
            lblDateTime.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt");
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            DisposePanelControls();
            pnlContainer.Controls.Clear();
            SetButonsDefaultForeColor();
            FocasAppSettingz ctrl = new FocasAppSettingz();
            ctrl.Dock = DockStyle.Fill;
            pnlContainer.Controls.Add(ctrl);
        }

        private void cmbMachines_SelectionChangeCommitted(object sender, EventArgs e)
        {
            HomeScreen.selectedMachine = cmbMachines.SelectedItem.ToString();
            this.Invalidate();  
            Application.DoEvents(); Application.DoEvents();
            if (pnlContainer.Controls.Count > 0)
            {
                timer1_Tick(null, EventArgs.Empty);
                LoadRespectiveUserControl();

                lblMacName.BringToFront();
                lblMacName.Text = HomeScreen.selectedMachine;
            }

            string MTB = DatabaseAccess.GetMTB(HomeScreen.selectedMachine);
            var companyLogo = Path.Combine(Settings.APP_PATH, @"CompanyLogo", MTB + ".png");
            if (File.Exists(companyLogo))
            {
                btnLogo.BackgroundImage = Image.FromFile(companyLogo);
            }
            else
            {
                //var companyLogo = Path.Combine(Settings.APP_PATH, @"CompanyLogo", "CompanyLogo.png");
                btnLogo.BackgroundImage = Image.FromFile(Path.Combine(Settings.APP_PATH, @"CompanyLogo", "CompanyLogo.png"));
            }
        }

        

        private void LoadRespectiveUserControl()
        {
            if (pnlContainer.Controls.Count == 0) return;
            var type = pnlContainer.Controls[0].Name;
            var tagName = pnlContainer.Controls[0].Tag;            
            if (!string.IsNullOrEmpty(type))
            {   
                if (type.Equals("Dashboard_Sac_Win", StringComparison.OrdinalIgnoreCase))
                {
                    btnHome_Click(null, EventArgs.Empty);
                }
                else if (type.Equals("HomeScreen", StringComparison.OrdinalIgnoreCase))
                {
                    btnPredectiveAlerts_Click(null, EventArgs.Empty);
                }
                else if (type.Equals("CNCAndPredictiveAlarmControl", StringComparison.OrdinalIgnoreCase))
                {
                    Application.DoEvents(); Application.DoEvents(); Application.DoEvents(); Application.DoEvents(); Application.DoEvents();
                    btnMaintenanceSchedule_Click(null, EventArgs.Empty);
                }
                else if (type.Equals("IRSchedule_Win", StringComparison.OrdinalIgnoreCase))
                {
                    Application.DoEvents(); Application.DoEvents(); Application.DoEvents(); Application.DoEvents(); Application.DoEvents();
                    btnSpindleParameters_Click(null, EventArgs.Empty);
                }
                else if (type.Equals("RPM", StringComparison.OrdinalIgnoreCase))
                {
                    
                    Application.DoEvents(); Application.DoEvents(); Application.DoEvents(); Application.DoEvents(); Application.DoEvents();
                    btnMachineManual_Click(null, EventArgs.Empty);
                }
                else if (type.Equals("ProcessDoc", StringComparison.OrdinalIgnoreCase))
                {
                    Application.DoEvents(); Application.DoEvents(); Application.DoEvents(); Application.DoEvents(); Application.DoEvents();
                    btnProgramTransfer_Click(null, null);
                }
                else if (type.Equals("ProgramTransferControl", StringComparison.OrdinalIgnoreCase))
                {
                    btnPRGP7_Click(null, EventArgs.Empty);
                }
                else if (type.Equals("UpgradeOEM", StringComparison.OrdinalIgnoreCase))
                {
                    btnUpgrade_Click(null, EventArgs.Empty);
                }
                else if (type.Equals("FocasAppSettingz", StringComparison.OrdinalIgnoreCase))
                {
                    btnSettings_Click(null, EventArgs.Empty);
                }
                else { }
            }
            else
            {
                if (tagName.Equals("MaintenanceSchedule"))
                {
                    btnMaintenanceSchedule_Click(null, EventArgs.Empty);
                }
                if (tagName.Equals("MaintenenceChecklist"))
                {
                    btnProgramTransfer_Click(null, EventArgs.Empty);
                }
            }
            Application.DoEvents();
            pnlContainer.Refresh();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            DisposePanelControls();
            pnlContainer.Controls.Clear();

            SetButonsDefaultForeColor();

            WebPageViewerControl ctrl = new WebPageViewerControl();
            ctrl.Dock = DockStyle.Fill;
            pnlContainer.Controls.Add(ctrl);

        }

        private void cmbMachines_MouseEnter(object sender, EventArgs e)
        {
            cmbMachines.BringToFront();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            cmbMachines.BringToFront();
            cmbMachines.DropDownStyle = ComboBoxStyle.DropDownList;
            //pnlContainer.Refresh();
            //Application.DoEvents(); Application.DoEvents(); Application.DoEvents(); Application.DoEvents(); Application.DoEvents();
        }

        private void cmbMachines_MouseLeave(object sender, EventArgs e)
        {
            lblMacName.BringToFront();
            lblMacName.Text = cmbMachines.Text.ToString();
        }

        private void btnProgramTransfer_Click(object sender, EventArgs e)
        {
            SetButonsDefaultForeColor();
            isHomeScreenView = false;
            SetButonsDefaultForeColor();
            btnProgramTransfer.ForeColor = Color.Orange;
            btnProgramTransfer.BackColor = ColorTranslator.FromHtml("0x4040A0");

            DisposePanelControls();
            pnlContainer.Controls.Clear();
            ProcessDoc ctrl = new ProcessDoc();
            ctrl.Dock = DockStyle.Fill;
            pnlContainer.Controls.Add(ctrl);
            this.Cursor = Cursors.Default;
        }

        private void cmbMachines_Click(object sender, EventArgs e)
        {
            pnlContainer.Refresh();
        }

        private void lblMacName_MouseEnter(object sender, EventArgs e)
        {
            cmbMachines.BringToFront();
        }

        private void tableLayoutPanel2_MouseLeave(object sender, EventArgs e)
        {
            lblMacName.BringToFront();

        }

        private void cmbMachines_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void headerTblLayout_MouseEnter(object sender, EventArgs e)
        {
            lblMacName.BringToFront();
        }

        private void btnCurent_Click(object sender, EventArgs e)
        {
            RunChartDateTime = true;
            SetBackOldButtonStyles();
            CURRENT_DATE_TIME = (DatabaseAccess.GetShiftStartEndTimeForDay(1, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))).ToString("yyyy-MM-dd HH:mm:ss");
            LOGICAL_DAY_END = (DatabaseAccess.GetShiftStartEndTimeForDay(0, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))).ToString("yyyy-MM-dd HH:mm:ss");
            //g: test
            //CURRENT_DATE_TIME = "2018-12-04 06:00:00";
            //LOGICAL_DAY_END = "2018-12-05 06:00:00";
            btnCurent.BackgroundImage = Image.FromFile(Path.Combine(Settings.APP_PATH, "Images", "SelectedDate.png"));
            btnCurent.ForeColor = Color.White;

            timer1.Enabled = false;
            LoadDataForTheSelectedDate();
            LoadRespectiveUserControl();
            timer1.Enabled = true;
        }

        private void btnPrev1_Click(object sender, EventArgs e)
        {
            RunChartDateTime = false;
            SetBackOldButtonStyles();
            string dateVal = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss");
            CURRENT_DATE_TIME = (DatabaseAccess.GetShiftStartEndTimeForDay(1, dateVal)).ToString("yyyy-MM-dd HH:mm:ss");
            LOGICAL_DAY_END = (DatabaseAccess.GetShiftStartEndTimeForDay(0, dateVal)).ToString("yyyy-MM-dd HH:mm:ss");
            btnPrev1.BackgroundImage = Image.FromFile(Path.Combine(Settings.APP_PATH, "Images", "SelectedDate.png"));
            btnPrev1.ForeColor = Color.White;

            timer1.Enabled = false;
            LoadDataForTheSelectedDate();
            LoadRespectiveUserControl();
            timer1.Enabled = true;
        }

        private void btnPrev2_Click(object sender, EventArgs e)
        {
            RunChartDateTime = false;
            SetBackOldButtonStyles();
            string dateVal = DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd HH:mm:ss");
            CURRENT_DATE_TIME = (DatabaseAccess.GetShiftStartEndTimeForDay(1, dateVal)).ToString("yyyy-MM-dd HH:mm:ss");
            LOGICAL_DAY_END = (DatabaseAccess.GetShiftStartEndTimeForDay(0, dateVal)).ToString("yyyy-MM-dd HH:mm:ss");
            btnPrev2.BackgroundImage = Image.FromFile(Path.Combine(Settings.APP_PATH, "Images", "SelectedDate.png"));
            btnPrev2.ForeColor = Color.White;

            timer1.Enabled = false;
            LoadDataForTheSelectedDate();
            LoadRespectiveUserControl();
            timer1.Enabled = true;
        }

        private void SetBackOldButtonStyles()
        {
            btnPrev2.BackgroundImage = Image.FromFile(Path.Combine(Settings.APP_PATH, "Images", "DateBtn.png"));
            btnPrev2.ForeColor = Color.Black;

            btnPrev1.BackgroundImage = Image.FromFile(Path.Combine(Settings.APP_PATH, "Images", "DateBtn.png"));
            btnPrev1.ForeColor = Color.Black;

            btnCurent.BackgroundImage = Image.FromFile(Path.Combine(Settings.APP_PATH, "Images", "DateBtn.png"));
            btnCurent.ForeColor = Color.Black;
        }

        private void btnUpgrade_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            SetButonsDefaultForeColor();
            btnUpgrade.BackColor = Color.Black;
            btnUpgrade.ForeColor = Color.Orange;

            DisposePanelControls();
            pnlContainer.Controls.Clear();

            UpgradeOEM ctrl = new UpgradeOEM();
            ctrl.Dock = DockStyle.Fill;
            pnlContainer.Controls.Add(ctrl);
            this.Cursor = Cursors.Default;
        }

        private void btnAndonMode_Click(object sender, EventArgs e)
        {
            if (btnAndonMode.Text.Contains("Desktop"))
            {
                Settings.InAndonMode = true;
                btnAndonMode.Text = "ANDON Mode";
                this.toolTip1.SetToolTip(this.btnAndonMode, "Switch to Desktop mode");
                timerANDON_Tick(null, EventArgs.Empty);
            }
            else
            {
                Settings.InAndonMode = false;
                btnAndonMode.Text = "Desktop Mode";
                this.toolTip1.SetToolTip(this.btnAndonMode, "Switch to ANDON mode");
                timerANDON.Enabled = false;
            }
        }

        private void timerANDON_Tick(object sender, EventArgs e)
        {
            timerANDON.Enabled = false;
            try
            {
                int machineCount = cmbMachines.Items.Count;
                cmbMachines.SelectedIndex = (cmbMachines.SelectedIndex + 1 >= machineCount) ? 0 : cmbMachines.SelectedIndex + 1;
                cmbMachines_SelectionChangeCommitted(null, EventArgs.Empty);
            }
            finally
            {
                timerANDON.Enabled = true;
            }
        }

        private void cmbPlant_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmbPlant_MouseEnter(object sender, EventArgs e)
        {
            cmbPlant.BringToFront();
        }

        private void cmbPlant_MouseLeave(object sender, EventArgs e)
        {
            lblPlant.BringToFront();
            lblPlant.Text = cmbPlant.Text.ToString();
        }

        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            lblPlant.BringToFront();
        }

        private void lblPlant_Click(object sender, EventArgs e)
        {
            cmbPlant.BringToFront();
            cmbPlant.DropDownStyle = ComboBoxStyle.DropDownList;
            //pnlContainer.Refresh();
            //Application.DoEvents(); Application.DoEvents(); Application.DoEvents(); Application.DoEvents(); Application.DoEvents();
        }

        private void lblPlant_MouseEnter(object sender, EventArgs e)
        {
            cmbPlant.BringToFront();
        }

        private void cmbPlant_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbPlant.SelectedItem.ToString().Contains("All") == false)
            {
                cmbMachines.DataSource = DatabaseAccess.GetMachinesByPlant1(cmbPlant.SelectedItem.ToString());
            }
            else
            {
                cmbMachines.DataSource = AllMachines;
            }
            cmbMachines_SelectionChangeCommitted(null, EventArgs.Empty);
            lblPlant.BringToFront();
            lblPlant.Text = cmbPlant.SelectedItem.ToString();
        }

        private void cmbPlant_Click(object sender, EventArgs e)
        {
            pnlContainer.Refresh();
        }

        private void btnMenuIcon_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            isHomeScreenView = false;
            SetButonsDefaultForeColor();
            //btnMenuIcon.BackColor = ColorTranslator.FromHtml("0x4040A0");
            //btnMenuIcon.ForeColor = Color.Orange;

            DisposePanelControls();
            pnlContainer.Controls.Clear();

            MachineManual mmanual = new MachineManual();
            MachineMenuControl ctrl = new MachineMenuControl(mmanual);
            ctrl.Dock = DockStyle.Fill;
            pnlContainer.Controls.Add(ctrl);
            this.Cursor = Cursors.Default;
            lblDateTime.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt");
            lblMachineStatus.Width = cmbMachines.Width;
            lblMacName.Width = cmbMachines.Width;
            lblPlant.Width = cmbPlant.Width;
        }

        private void btnPRGP7_Click(object sender, EventArgs e)
        {
            SetButonsDefaultForeColor();
            isHomeScreenView = false;
            SetButonsDefaultForeColor();
            btnPRGP7.ForeColor = Color.Orange;
            btnPRGP7.BackColor = ColorTranslator.FromHtml("0x4040A0");

            DisposePanelControls();
            pnlContainer.Controls.Clear();
            ProgramTransferControl ctrl = new ProgramTransferControl();
            ctrl.Dock = DockStyle.Fill;
            pnlContainer.Controls.Add(ctrl);
            this.Cursor = Cursors.Default;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetButonsDefaultForeColor();
            isHomeScreenView = false;
            SetButonsDefaultForeColor();

            DisposePanelControls();
            pnlContainer.Controls.Clear();
            IRSchedule_Win ctrl = new IRSchedule_Win();
            ctrl.Dock = DockStyle.Fill;
            pnlContainer.Controls.Add(ctrl);
            this.Cursor = Cursors.Default;
        }

       
    }
}
