using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.IO;
using AxAcroPDFLib;
using MachineConnectOEM;

namespace MachineConnectApplication
{
    public partial class FocasAppSettingz : UserControl
    {    
        string CURRENT_DATE_TIME = Convert.ToDateTime(MainScreen.CURRENT_DATE_TIME).ToString("yyyy-MM-dd HH:mm:ss");
        
        public FocasAppSettingz()
        {
            InitializeComponent();
            if (btnMachineInformation.Visible)
            {
                btnMachineInformation_Click(null,EventArgs.Empty);
            }
            else if (btnServiceSettings.Visible)
            {
                btnServiceSettings_Click(null, EventArgs.Empty);
            }
            else if (btnApplicationSettings.Visible)
            {
                btnApplicationSettings_Click(null, EventArgs.Empty);
            }         
        }

        private void DisposePanelControls()
        {
            foreach (Control p in pnlContainer.Controls)
            {
                p.Dispose();
            }
        }       
       
        private void btnMachineInformation_Click(object sender, EventArgs e)
        {
            DisposePanelControls();           
            pnlContainer.Controls.Clear();
            SetButonsDefaultForeColor();

            this.btnMachineInformation.BackColor = ColorTranslator.FromHtml("0x4040A0");
            btnMachineInformation.ForeColor = Color.Orange;

            MachineInformation ctrl = new MachineInformation();
            ctrl.Dock = DockStyle.Fill;
            pnlContainer.Controls.Add(ctrl);
        }

        private void SetButonsDefaultForeColor()
        {
            btnMachineInformation.ForeColor = Color.White;
            btnMachineInformation.BackColor = ColorTranslator.FromHtml("0x1F497D");

            btnServiceSettings.ForeColor = Color.White;
            btnServiceSettings.BackColor = ColorTranslator.FromHtml("0x1F497D");

            btnApplicationSettings.ForeColor = Color.White;
            btnApplicationSettings.BackColor = ColorTranslator.FromHtml("0x1F497D");

            btnShiftSettings.ForeColor = Color.White;
            btnShiftSettings.BackColor = ColorTranslator.FromHtml("0x1F497D");

            BtnActivityMaster.ForeColor = Color.White;
            BtnActivityMaster.BackColor = ColorTranslator.FromHtml("0x1F497D");

            btnProcessParamSettings.ForeColor = Color.White;
            btnProcessParamSettings.BackColor = ColorTranslator.FromHtml("0x1F497D");

            btnActivityTimings.ForeColor = Color.White;
            btnActivityTimings.BackColor = ColorTranslator.FromHtml("0x1F497D");
        }

        private void btnServiceSettings_Click(object sender, EventArgs e)
        {
            DisposePanelControls();
            pnlContainer.Controls.Clear();
            SetButonsDefaultForeColor();

            this.btnServiceSettings.BackColor = ColorTranslator.FromHtml("0x4040A0");
            btnServiceSettings.ForeColor = Color.Orange;

            ServiceSettings ctrl = new ServiceSettings();
            ctrl.Dock = DockStyle.Fill;
            pnlContainer.Controls.Add(ctrl);
        }

        private void btnApplicationSettings_Click(object sender, EventArgs e)
        {
            DisposePanelControls();
            pnlContainer.Controls.Clear();        
            SetButonsDefaultForeColor();

            this.btnApplicationSettings.BackColor = ColorTranslator.FromHtml("0x4040A0");
            btnApplicationSettings.ForeColor = Color.Orange;

            ApplicationUISettings ctrl = new ApplicationUISettings();
            ctrl.Dock = DockStyle.Fill;
            pnlContainer.Controls.Add(ctrl);
        }

        private void btnAlerts_Click(object sender, EventArgs e)
        {
            //if (LoginForm.FocasHasMachines)
            //{
            //    Application.Exit();
            //}
            //else  
        }

        private void FocasAppSettingz_Load(object sender, EventArgs e)
        {
            if (LoginForm.IsAdmin.Equals("1"))
            {
                btnMachineInformation.Visible = true;
                //if (LoginForm.LoginUserName.Equals(Settings.SuperAdmin_UserName) && (LoginForm.LoginPassword.Equals(Settings.SuperAdmin_Password)))
                //{
                //    btnServiceSettings.Visible = true;
                //}
            }
            else
            { 
            
            }
            // reorder buttons
            tableLayoutPanel3.Controls.Clear();
            tableLayoutPanel3.Controls.Add(btnMachineInformation, 0, 0);
            tableLayoutPanel3.Controls.Add(BtnActivityMaster, 1, 0);
            tableLayoutPanel3.Controls.Add(btnActivityTimings, 2, 0);
            tableLayoutPanel3.Controls.Add(btnProcessParamSettings, 3, 0);
            tableLayoutPanel3.Controls.Add(btnShiftSettings, 4, 0);
            tableLayoutPanel3.Controls.Add(btnServiceSettings, 5, 0);
            tableLayoutPanel3.Controls.Add(btnApplicationSettings, 6, 0);
        }

        private void btnShiftSettings_Click(object sender, EventArgs e)
        {
            DisposePanelControls();
            pnlContainer.Controls.Clear();
            SetButonsDefaultForeColor();

            this.btnShiftSettings.BackColor = ColorTranslator.FromHtml("0x4040A0");
            btnShiftSettings.ForeColor = Color.Orange;

            ShiftDetailz ctrl = new ShiftDetailz();
            ctrl.Dock = DockStyle.Fill;
            pnlContainer.Controls.Add(ctrl);
        }

        private void BtnActivityMaster_Click(object sender, EventArgs e)
        {
            DisposePanelControls();
            pnlContainer.Controls.Clear();
            SetButonsDefaultForeColor();

            this.BtnActivityMaster.BackColor = ColorTranslator.FromHtml("0x4040A0");
            BtnActivityMaster.ForeColor = Color.Orange;

            ActivityInfoUserCnrl ctrl = new ActivityInfoUserCnrl();
            //ActivityInformation ctrl = new ActivityInformation();
            ctrl.Dock = DockStyle.Fill;
            pnlContainer.Controls.Add(ctrl);
        }

        private void btnProcessParamSettings_Click(object sender, EventArgs e)
        {
            DisposePanelControls();
            pnlContainer.Controls.Clear();
            SetButonsDefaultForeColor();
            this.btnProcessParamSettings.BackColor = ColorTranslator.FromHtml("0x4040A0");
            btnProcessParamSettings.ForeColor = Color.Orange;
            PPMSettingsParent ctrl = new PPMSettingsParent();
            ctrl.Dock = DockStyle.Fill;
            pnlContainer.Controls.Add(ctrl);
        }

        private void btnActivityTimings_Click(object sender, EventArgs e)
        {
            DisposePanelControls();
            pnlContainer.Controls.Clear();
            SetButonsDefaultForeColor();
            btnActivityTimings.BackColor = ColorTranslator.FromHtml("0x4040A0");
            btnActivityTimings.ForeColor = Color.Orange;
            MachineConnectOEM.SAC.DefinePMRules_Win ctrl = new MachineConnectOEM.SAC.DefinePMRules_Win();
            ctrl.Dock = DockStyle.Fill;
            //ctrl.Size = new Size(pnlContainer.Width, pnlContainer.Height);
            //pnlContainer.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            pnlContainer.Controls.Add(ctrl);
            
        }
    }
}
