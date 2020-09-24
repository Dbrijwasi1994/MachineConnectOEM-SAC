using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Text.RegularExpressions;


namespace MachineConnectApplication
{
    public partial class MachineInformation : UserControl
    { 
        List<string> allModels = new List<string>();
        public static int NoOfRows = 0;

        public MachineInformation()
        {
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.UserPaint |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.ResizeRedraw |
                          ControlStyles.ContainerControl |
                          ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.SupportsTransparentBackColor
                          , true);

            InitializeComponent();
            dataGrid.AutoGenerateColumns = false;   
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  
                return cp;
            }
        }     

        private void txtKeypres(KeyPressEventArgs e)
        {            
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            if (e.KeyChar == Convert.ToChar(Keys.Back))
            {
                e.Handled = false;
            }
        }

        private void txtPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeypres(e);
        }

        private void txtInterfaceId_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeypres(e);
        }
    
        public Boolean CheckIPValid(String strIP)
        {
            //  Split string by ".", check that array length is 3
            char chrFullStop = '.';
            string[] arrOctets = strIP.Split(chrFullStop);
            if (arrOctets.Length != 4)
            {
                return false;
            }

            //  Check each substring checking that the int value is less than 255 and that is char[] length is !> 2
            Int16 MAXVALUE = 255;
            Int32 temp; // Parse returns Int32
            foreach (String strOctet in arrOctets)
            {
                if (strOctet.Length > 3)
                {
                    return false;
                }

                temp = int.Parse(strOctet);
                if (temp > MAXVALUE)
                {
                    return false;
                }
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                short EtherNetflag = 0;
                short ProgramFoldersflag = 0;
                if (chkEtherNet.Checked == true)
                {
                    EtherNetflag = 1;
                }
                else if (chkEtherNet.Checked == false)
                {
                    EtherNetflag = 0;
                }


                if (chkProgramFoldersEnabled.Checked == true)
                {
                    ProgramFoldersflag = 1;
                }
                else if (chkProgramFoldersEnabled.Checked == false)
                {
                    ProgramFoldersflag = 0;
                }
                if (DatabaseAccess.CheckMachine(cmbMachineId.Text.Trim()))
                {
                    if (ValidateAllFeilds())
                    {

                        if (CheckForUniqueIpInerface())
                        {
                            DatabaseAccess.UpdateMachineInfoForMachine(cmbMachineId.Text.Trim(), txtDesc.Text.Trim(),
                               cmbMTB.Text.ToString(), cmbMachineType.Text.ToString(), cmbModel.Text.ToString(), txtIP.Text.Trim(),
                               txtPort.Text, txtInterfaceId.Text.Trim(), EtherNetflag,ProgramFoldersflag, txtPartCountMacroLocation.Text, txtSpindleAxisNumber.Text);
                           
                           
                            CustomDialogBox cmb = new CustomDialogBox("Error Message", "Data updated successfully");
                            cmb.ShowDialog();
                            resetAllFeilds();  
                        }
                    }
                }

                else
                {
                    if (ValidateAllFeilds())
                    {
                        if (CheckForUniqueIpInerface())
                        {
                            if (CheckForIpAssignment() == false)
                            {
                               DatabaseAccess.InsertDataForMachine(cmbMachineId.Text.Trim(), txtDesc.Text.Trim(),
                               cmbMTB.Text.ToString(), cmbMachineType.Text.ToString(), cmbModel.Text.ToString(), txtIP.Text.Trim(),
                               txtPort.Text, txtInterfaceId.Text.Trim(), EtherNetflag,ProgramFoldersflag, txtPartCountMacroLocation.Text, txtSpindleAxisNumber.Text);
                               CustomDialogBox cmb = new CustomDialogBox("Error Message", "Details Added successfully");
                               cmb.ShowDialog();                       
                                resetAllFeilds();
                                cmbMachineId.DataSource = DatabaseAccess.GetMachineIdForMachineInfo();
                            }

                            else
                            {                                
                                CustomDialogBox cmb = new CustomDialogBox("Error Message", "IP address already exists, enter different IP address");
                                cmb.ShowDialog();
                                txtIP.Focus();
                            }
                        }

                    }
                }

                DataTable dt = DatabaseAccess.GetAllMachinesDataForGrid();
                dataGrid.DataSource = dt;
                NoOfRows = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
        }

        private void GetAllMachines()
        {
            throw new NotImplementedException();
        }

        private bool CheckForIpAssignment()
        {
            return false;
        }

        private bool CheckForUniqueMachine()
        {
            //Check For unique Machine
            List<string> checkmachine = DatabaseAccess.GetMachineIdForMachineInfo();
            for (int i = 0; i < checkmachine.Count; i++)
            {
                if (checkmachine[i] == cmbMachineId.Text.Trim())
                {
                    CustomDialogBox cmb = new CustomDialogBox("Error Message", "Machine ID already exists, enter different IP address");
                    cmb.ShowDialog();
                    return false;
                }
            }
            return true;
        }

        private bool CheckForUniqueIpInerface()
        {            
            var validIP = DatabaseAccess.CheckUniqueIPInterfacePort(cmbMachineId.Text.Trim(), "IP", txtIP.Text);
            if (validIP)
            {
                CustomDialogBox cmb = new CustomDialogBox("Error Message","IP address already exists, enter different IP address");
                cmb.ShowDialog();
                txtIP.Focus();
                return false;
            } 
            return true;
        }      

        private bool ValidateAllFeilds()
        {
            bool ipval = CheckIPValid(txtIP.Text.Trim());

            if (txtIP.Text.Trim() == string.Empty)
            {
                CustomDialogBox cmb = new CustomDialogBox("Error Message", "Please Enter IP address.");
                cmb.ShowDialog();
                txtIP.Focus();
                return false;
            }
            else if (ipval == false)
            {
                CustomDialogBox cmb = new CustomDialogBox("Error Message", "Invalid IP address format");
                cmb.ShowDialog();
                txtIP.Focus();
                return false;
            }
            //else if (txtInterfaceId.Text.Trim() == string.Empty)
            //{
            //    CustomDialogBox cmb = new CustomDialogBox("Error Message", "Please Enter Interface ID.");
            //    cmb.ShowDialog();
            //    txtInterfaceId.Focus();
            //    return false;
            //}
            //else if (txtDesc.Text.Trim() == string.Empty)
            //{
            //    CustomDialogBox cmb = new CustomDialogBox("Error Message", "Please Enter Description.");
            //    cmb.ShowDialog();              
            //    txtDesc.Focus();
            //    return false;
            //}
            else if (txtPort.Text.Trim() == string.Empty)
            {
                CustomDialogBox cmb = new CustomDialogBox("Error Message", "Please Enter Port No.");
                cmb.ShowDialog();              
                txtPort.Focus();
                return false;
            }

            return true;
        }           

        private void TestBenchInformation_Load(object sender, EventArgs e)
        {
            try
            {

                chkPartCountByMacro.Checked = false;
                txtPartCountMacroLocation.Enabled = false;


                var machine = DatabaseAccess.GetAllMachines();
                cmbMachineId.DataSource = machine;
                cmbMachineId.Text = string.Empty;

                cmbMTB.DataSource = DatabaseAccess.GetAllMachinesData("MTB");
                cmbMTB.Text = string.Empty;

                cmbMachineType.DataSource = DatabaseAccess.GetAllMachinesData("MachineType_MGTL");
                cmbMachineType.Text = string.Empty;

                allModels = DatabaseAccess.GetAllModels(); 
                cmbMTB_SelectionChangeCommitted(null, EventArgs.Empty);

                DataTable dt = DatabaseAccess.GetAllMachinesDataForGrid();
                dataGrid.DataSource = dt;

                SuperAdminSettings();

                NoOfRows = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
        }

        private void SuperAdminSettings()
        {
            if (LoginForm.LoginUserName.Equals(Settings.SuperAdmin_UserName) && (LoginForm.LoginPassword.Equals(Settings.SuperAdmin_Password)))
            {
                this.tblInputFeilds.ColumnStyles[9].SizeType = SizeType.Absolute;
                this.tblInputFeilds.ColumnStyles[9].Width = 117;

                this.tblInputFeilds.ColumnStyles[10].SizeType = SizeType.Absolute;
                this.tblInputFeilds.ColumnStyles[10].Width = 95;

                dataGrid.Columns[10].Visible = true;
                dataGrid.Columns[11].Visible = true;
            }
            else
            {
                //this.tblInputFeilds.ColumnStyles[7].SizeType = SizeType.Absolute;
                //this.tblInputFeilds.ColumnStyles[7].Width = 0;

                this.tblInputFeilds.ColumnStyles[8].SizeType = SizeType.Absolute;
                this.tblInputFeilds.ColumnStyles[8].Width = 0;

                this.tblInputFeilds.ColumnStyles[9].SizeType = SizeType.Absolute;
                this.tblInputFeilds.ColumnStyles[9].Width = 0;

                dataGrid.Columns[11].Visible = false;
                dataGrid.Columns[10].Visible = false;
            }
        }                     
   
        private void txtIP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if (e.KeyChar == Convert.ToChar(Keys.Back))
            {
                e.Handled = false;
            }
        }      
    
        private void btnNew_Cancel_Click(object sender, EventArgs e)
        {

        }

        private void resetAllFeilds()
        {
            cmbMachineId.Text = string.Empty;
            txtIP.Text = string.Empty;
            txtInterfaceId.Text = string.Empty;
            txtDesc.Text = string.Empty;
            txtPort.Text = string.Empty;  
            chkEtherNet.Checked = false;
            chkProgramFoldersEnabled.Checked = false;

            cmbMTB.Text = string.Empty;
            cmbMachineType.Text = string.Empty;
            cmbModel.Text = string.Empty;

            txtPartCountMacroLocation.Text = string.Empty;
            txtSpindleAxisNumber.Text = string.Empty;
            chkPartCountByMacro.Checked = false;
        }

        private void dataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!(e.RowIndex < 0))
            {
                try
                {
                    cmbMachineId.Text = dataGrid.Rows[e.RowIndex].Cells["Machineid"].Value.ToString();
                    txtDesc.Text = dataGrid.Rows[e.RowIndex].Cells["Description"].Value.ToString();
                    cmbMTB.Text = dataGrid.Rows[e.RowIndex].Cells["MachineMTB"].Value.ToString(); 
                    cmbMachineType.Text = dataGrid.Rows[e.RowIndex].Cells["MachineType"].Value.ToString();
                    cmbModel.Text = dataGrid.Rows[e.RowIndex].Cells["MachineModel"].Value.ToString(); 
                    txtIP.Text = dataGrid.Rows[e.RowIndex].Cells["IPAddress"].Value.ToString();
                    txtPort.Text = dataGrid.Rows[e.RowIndex].Cells["PortNo"].Value.ToString();
                    txtInterfaceId.Text = dataGrid.Rows[e.RowIndex].Cells["Interfaceid"].Value.ToString();
                    chkEtherNet.Checked = Convert.ToBoolean(dataGrid.Rows[e.RowIndex].Cells["EthernetEnabled"].Value);
                    chkProgramFoldersEnabled.Checked = Convert.ToBoolean(dataGrid.Rows[e.RowIndex].Cells["ProgramFoldersEnabled"].Value);

                    //Super Admin Values
                    if ((dataGrid.Rows[e.RowIndex].Cells["EnablePartCountByMacro"].Value.ToString()) != "0")
                    {
                        txtPartCountMacroLocation.Enabled = true;
                        txtPartCountMacroLocation.Text = dataGrid.Rows[e.RowIndex].Cells["EnablePartCountByMacro"].Value.ToString();
                        chkPartCountByMacro.Checked = true;
                        //chkPartCountByMacro.Checked = Convert.ToBoolean(dataGrid.Rows[e.RowIndex].Cells["EthernetEnabled"].Value);
                    }
                    else
                    {
                        txtPartCountMacroLocation.Enabled = false;
                        txtPartCountMacroLocation.Text = string.Empty;
                        chkPartCountByMacro.Checked = false;
                    }
                    txtSpindleAxisNumber.Text = dataGrid.Rows[e.RowIndex].Cells["SpindleAxisNumber"].Value.ToString();

                }
                catch (Exception ex)
                {
                    Settings.WriteErrorMsg(ex.ToString());
                }
            }
        }

        private void cmbMTB_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cmbModel.DataSource = DatabaseAccess.GetAllMachineModelData(cmbMTB.Text.ToString());
            cmbModel.Text = string.Empty;
        }

        private void chkPartCountByMacro_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkPartCountByMacro.Checked)
            {
                txtPartCountMacroLocation.Text = string.Empty;
                txtPartCountMacroLocation.Enabled = true;
            }
            else
            {
                txtPartCountMacroLocation.Enabled = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            
        }    
    }
}
