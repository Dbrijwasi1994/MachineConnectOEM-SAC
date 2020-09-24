using System;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Drawing;
using FocasLibrary;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using FocasGUI;
using DTO;
using MachineConnectApplication;
using System.Reflection;

namespace CNC_PT
{
    public partial class ProgramTransferControl : UserControl
    {
        bool isContract = false;
        public string ip = string.Empty;
        public ushort port = 8193;
        bool isProgramFoldersSupport = false;
        string _folderPath = "//CNC_MEM/USER/PATH1/";
        string GetPath = string.Empty;
        private static string appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        List<ProgramDTO> listProgramFromCNC = null;
        ToolTip toolTip1 = new ToolTip();
        ToolTip toolTip2 = new ToolTip();
        private string _programDownloadFolder = string.Empty;
        MenuItem myMenuItem = new MenuItem("Create Folder");
        ContextMenu mnu = new ContextMenu();

        public ProgramTransferControl()
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
            treeSystm.ContextMenuStrip = contextMenuStrip1;
            treeViewCNCFolder.ContextMenuStrip = contextMenuStripCNC;

        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                listsysPrograms.HorizontalScrollbar = true;
                dgvProgramDetails.AutoGenerateColumns = false;
                dgvProgramDetails.AllowUserToResizeColumns = true;
                PageLoadFunctions();
                //btnUpload.Enabled = false;
                CreateSubFolderDirectory(SettingsPT.Program_path, HomeScreen.selectedMachine);

                this.listsysPrograms.DragDrop += new System.Windows.Forms.DragEventHandler(this.listsysPrograms_DragDrop);
                this.listsysPrograms.DragEnter += new System.Windows.Forms.DragEventHandler(this.listsysPrograms_DragEnter);
                this.listsysPrograms.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listsysPrograms_MouseDown_1);

                this.treeSystm.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeSystm_NodeMouseClick);
                this.treeSystm.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeSystm_DragDrop);
                this.treeSystm.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeSystm_DragEnter);
                this.treeSystm.MouseMove += new System.Windows.Forms.MouseEventHandler(this.treeSystm_MouseMove);

                this.btnDownloadProgram.Click += new System.EventHandler(this.btnDonwnload_Click);
                this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
                this.dgvProgramDetails.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvProgramDetails_CellMouseDown);
                this.dgvProgramDetails.DragDrop += new System.Windows.Forms.DragEventHandler(this.dataGridView1_DragDrop);
                this.dgvProgramDetails.DragEnter += new System.Windows.Forms.DragEventHandler(this.dataGridView1_DragEnter);
                this.treeViewCNCFolder.NodeMouseDoubleClick += treeViewCNCFolder_NodeMouseDoubleClick;

                cmbMachine_SelectionChangeCommitted(null, null);
            }
            catch (Exception exx)
            {
                MachineConnectApplication.Logger.WriteErrorLog(exx.ToString());
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

        }

        private void CheckProgramFolderExistsForMac()
        {

            if (this.isProgramFoldersSupport)
            {
                btnExpandOrContract.Visible = true;
                tableLayoutPanel11.RowStyles[1].SizeType = SizeType.Absolute;
                tableLayoutPanel11.RowStyles[1].Height = 35;
                isContract = true;
                btnExpandOrContract_Click(null, EventArgs.Empty);
            }
            else
            {
                btnExpandOrContract.Visible = false;
                tableLayoutPanel11.RowStyles[1].SizeType = SizeType.Absolute;
                tableLayoutPanel11.RowStyles[1].Height = 0;
                isContract = false;
                btnExpandOrContract_Click(null, EventArgs.Empty);
            }

        }

        private void PageLoadFunctions()
        {
            //BindPlantId();         
            dgvProgramDetails.Rows.Clear();
            BindProgramTree();
        }

        private void cmbMachine_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {

                CreateSubFolderDirectory(SettingsPT.Program_path, HomeScreen.selectedMachine);
                dgvProgramDetails.Rows.Clear();
                listsysPrograms.Items.Clear();
                treeSystm.Nodes.Clear();
                Application.DoEvents();
                LoadFoldersInTreeView(ref treeSystm);
                GetIpPort();
                SettingsPT.IP = ip;
                SettingsPT.PORT = port;
                SettingsPT.IsProgramFoldersSupport = this.isProgramFoldersSupport;
                Application.DoEvents();
                CheckProgramFolderExistsForMac();
                if (this.isProgramFoldersSupport)
                {
                    LoadFoldersInTreeViewCNCFolders(ref treeViewCNCFolder);
                }
                BindProgramListGridview();
                if (this.isProgramFoldersSupport)
                {
                    if (treeViewCNCFolder.SelectedNode != null && !string.IsNullOrEmpty(treeViewCNCFolder.SelectedNode.FullPath.ToString()))
                    {
                        groupBox4.Text = "CNC Machine (" + treeViewCNCFolder.SelectedNode.FullPath.ToString() + ")";
                    }
                }
                treeSystm_NodeMouseClick(null, null);

            }
            catch (Exception ex)
            {
                FocasLibrary.Logger.WriteErrorLog("Error..!! ProgramTransfer : \n " + ex.ToString());
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            CreateSubFolderDirectory(SettingsPT.Program_path, HomeScreen.selectedMachine);
            _programDownloadFolder = Path.Combine(SettingsPT.Program_path, HomeScreen.selectedMachine); //TOD
            cmbMachine_SelectionChangeCommitted(HomeScreen.selectedMachine, EventArgs.Empty);
            BindProgramListGridview();
            treeSystm_NodeMouseClick(null, null);
            Cursor.Current = Cursors.Default;

            groupBox2.Text = "Computer (" + treeSystm.SelectedNode.FullPath.ToString() + ")";
            if (treeViewCNCFolder.SelectedNode != null && !string.IsNullOrEmpty(treeViewCNCFolder.SelectedNode.FullPath.ToString()))
            {
                groupBox4.Text = "CNC Machine (" + _folderPath + ")";
            }
        }




        void LoadFoldersInTreeViewCNCFolders(ref TreeView treeName)
        {
            try
            {
                //if (!ping_success())
                //{
                //    return;
                //}                        

                ushort focasLibHandle = ProgramTransferPT.ConnectToCNC(this.ip, this.port);
                if (focasLibHandle == 0) return;

                treeViewCNCFolder.BeginUpdate();
                treeViewCNCFolder.Nodes.Clear();
                //cnc_rdpdf_drive : Reads the information of Program memory drive.
                FocasLibBase.ODBPDFDRV driveName = new FocasLibBase.ODBPDFDRV();
                short ret = FocasLib.cnc_rdpdf_drive(focasLibHandle, driveName);
                var firstDriveName = driveName.drive1;
                TreeNode root = new TreeNode();
                root.Text = firstDriveName;
                root.Tag = "//" + firstDriveName + "/";
                root.Name = "//" + firstDriveName + "/";
                treeViewCNCFolder.Nodes.Add(root);

                GetCNCFolders(root.Tag.ToString(), root, focasLibHandle);
                treeViewCNCFolder.ExpandAll();

                TreeNode itemNode = null;
                foreach (TreeNode node in treeViewCNCFolder.Nodes)
                {
                    itemNode = FromID("PATH1", node);
                    if (itemNode != null) break;
                }
                treeViewCNCFolder.SelectedNode = itemNode != null ? itemNode : treeViewCNCFolder.Nodes[0];
                treeViewCNCFolder.SelectedNode.BackColor = Color.LightGreen;


                treeViewCNCFolder.EndUpdate();

                // _folderPath = treeViewCNCFolder.SelectedNode.Tag.ToString();
            }
            catch (Exception ex)
            {
                FocasLibrary.Logger.WriteErrorLog("Error..!! ProgramTransfer : \n " + ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

        private void GetCNCFolders(string dirInfo, TreeNode node, ushort focasLibHandle)
        {
            try
            {

                List<string> dInfo = ProgramTransferPT.GetCNCFolders(focasLibHandle, node.Tag.ToString());
                if (dInfo.Count > 0)
                {
                    toolTip2.SetToolTip(this.treeViewCNCFolder, "Please double click on folder to view programs");
                    foreach (string driSub in dInfo)
                    {
                        if ((node.Tag.ToString() + driSub).Equals("//CNC_MEM/SYSTEM", StringComparison.OrdinalIgnoreCase) ||
                            (node.Tag.ToString() + driSub).Equals("//CNC_MEM/MTB1", StringComparison.OrdinalIgnoreCase) ||
                            (node.Tag.ToString() + driSub).Equals("//CNC_MEM/MTB2", StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }
                        //treeNode.Text = driSub;
                        //treeNode.Tag = node.Name.ToString() + driSub + "/";
                        //treeNode = node.Nodes.Add(node.Name.ToString() + driSub + "/", driSub);//NB see return value//
                        //GetCNCFolders(driSub, treeNode, focasLibHandle);
                        TreeNode treeNode = new TreeNode();
                        treeNode.Text = driSub;
                        treeNode.Tag = node.Tag.ToString() + driSub + "/";
                        node.Nodes.Add(treeNode);//NB see return value//
                        GetCNCFolders(driSub, treeNode, focasLibHandle);
                    }
                }
            }
            catch (Exception ex)
            {
                FocasLibrary.Logger.WriteErrorLog("Error..!! ProgramTransfer : \n " + ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbPlant_SelectionChangeCommitted(object sender, EventArgs e)
        {
            BindProgramTree();
            cmbMachine_SelectionChangeCommitted(HomeScreen.selectedMachine, EventArgs.Empty);
        }

        private void treeSystm_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            listsysPrograms.Visible = true;
            listsysPrograms.Items.Clear();
            TreeNode node = e != null ? e.Node : treeSystm.SelectedNode;
            if (node == null) return;
            set_default_color();

            node.ForeColor = Color.Black;
            node.BackColor = Color.LightGreen;
            string folderPath = node.FullPath;
            groupBox2.Text = "Computer (" + folderPath + ")";
            //TODO is this required?
            //createSubFolderDirectory(SettingsPT.Program_path, cmbPlant.Text, folderPath);

            string completePath = Path.Combine(SettingsPT.Program_path, folderPath);

            SettingsPT.SYS_fldr_slctd = completePath;//savd for dwnload purpose//
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    BindProgramList(completePath);

                }
                catch (Exception ex)
                {
                    this.Cursor = Cursors.Default;
                    FocasLibrary.Logger.WriteErrorLog("Error..!! ProgramTransfer : \n " + ex.ToString());

                    CustomDialogBoxProgramTransfer cmb = new CustomDialogBoxProgramTransfer("Error Message", ex.Message);
                    cmb.ShowDialog();

                }
                this.Cursor = Cursors.Default;
            }
        }

        //added by Shwetha
        private void CreateSubFolderDirectory(string programPath, string machineFolder)
        {
            string createPlantFolder = Path.Combine(SettingsPT.Program_path, machineFolder);
            if (!Directory.Exists(createPlantFolder))
            {
                try
                {
                    DirectoryInfo di = Directory.CreateDirectory(createPlantFolder);
                }
                catch (Exception ex)
                {
                    FocasLibrary.Logger.WriteErrorLog("Error..!! ProgramTransfer : \n " + ex.ToString());
                }
            }
        }



        private void SetTreeStyle()
        {
            try
            {
                treeSystm.ImageList = imageList1;
                treeViewCNCFolder.ImageList = imageList1;
                foreach (TreeNode node in treeSystm.Nodes)
                {
                    foreach (TreeNode node2 in node.Nodes)
                    {
                        node2.ImageIndex = 0;
                        node2.SelectedImageIndex = 0;
                    }
                }

                foreach (TreeNode node in treeViewCNCFolder.Nodes)
                {
                    foreach (TreeNode node2 in node.Nodes)
                    {
                        node2.ImageIndex = 0;
                        node2.SelectedImageIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                FocasLibrary.Logger.WriteErrorLog("Error..!! ProgramTransfer : \n " + ex.ToString());
            }
        }
        private void GetIpPort()
        {
            DatabaseAccessPT.GetIpPort(HomeScreen.selectedMachine, out ip, out port, out isProgramFoldersSupport);
        }

        private void BindProgramList(string path)
        {
            try
            {
                listsysPrograms.Items.Clear();
                DirectoryInfo info = new DirectoryInfo(path);
                FileInfo[] fInfo = info.GetFiles();
                string fName;
                if (fInfo != null && fInfo.Length > 0)
                {
                    foreach (FileInfo fin in fInfo)
                    {
                        // f_name = Path.GetFileNameWithoutExtension(fin.Name);
                        //TODO - Satya - Read file for comment here if required
                        fName = Path.GetFileNameWithoutExtension(fin.Name);
                        //fName = "O" + fName;
                        listsysPrograms.Items.Add(fName);
                    }
                    listsysPrograms.SelectedIndex = -1;
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                FocasLibrary.Logger.WriteErrorLog("Error..!! ProgramTransfer : \n " + ex.ToString());
                CustomDialogBoxProgramTransfer cmb = new CustomDialogBoxProgramTransfer("Error Message", ex.Message);
                cmb.ShowDialog();
            }
        }

        private void CB_mcn_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                ((HandledMouseEventArgs)e).Handled = true;
            }
            catch (Exception ex)
            {
                FocasLibrary.Logger.WriteErrorLog("Error..!! ProgramTransfer  : \n " + ex.ToString());
                CustomDialogBoxProgramTransfer cmb = new CustomDialogBoxProgramTransfer("Error Message", ex.Message);
                cmb.ShowDialog();
            }
        }

        void LoadFoldersInTreeView(ref TreeView treeName)
        {
            try
            {
                treeName.Nodes.Clear();
                treeName.BeginUpdate();
                DirectoryInfo dirInfo = new DirectoryInfo(Path.Combine(SettingsPT.Program_path, HomeScreen.selectedMachine));
                CreateSubFolderDirectory(SettingsPT.Program_path, dirInfo.ToString());
                TreeNode node = new TreeNode();
                node.Text = (string)HomeScreen.selectedMachine;
                node.Tag = dirInfo.FullName;
                GetFolders(dirInfo, node);
                treeSystm.Nodes.Add(node);
                treeSystm.SelectedNode = treeSystm.Nodes[0];
                treeSystm.SelectedNode.Expand();
                treeSystm.EndUpdate();
            }
            catch (Exception ex)
            {
                FocasLibrary.Logger.WriteErrorLog("Error..!! ProgramTransfer : \n " + ex.ToString());
                CustomDialogBoxProgramTransfer cmb = new CustomDialogBoxProgramTransfer("Error Message", ex.Message);
                cmb.ShowDialog();
            }
        }

        void GetFolders(DirectoryInfo d, TreeNode node)
        {
            try
            {
                DirectoryInfo[] dInfo = d.GetDirectories();
                if (dInfo.Length > 0)
                {
                    TreeNode treeNode = new TreeNode();
                    foreach (DirectoryInfo driSub in dInfo)
                    {
                        if (driSub.Name.ToLower() == "temp") continue;
                        treeNode = node.Nodes.Add(driSub.Name);//NB see return value//
                        treeNode.Tag = driSub.FullName;
                        GetFolders(driSub, treeNode);
                    }
                }
            }
            catch (Exception ex)
            {
                FocasLibrary.Logger.WriteErrorLog("Error..!! ProgramTransfer : \n " + ex.ToString());
                CustomDialogBoxProgramTransfer cmb = new CustomDialogBoxProgramTransfer("Error Message", ex.Message);
                cmb.ShowDialog();
            }
        }

        private void BindProgramListGridview()
        {
            if (!ping_success())
            {
                return;
            }

            listProgramFromCNC = ProgramTransferPT.ReadAllPrograms(SettingsPT.IP, SettingsPT.PORT, 2, _folderPath, this.isProgramFoldersSupport);
            dgvProgramDetails.Rows.Clear();
            if (listProgramFromCNC != null)
            {
                var filenamesList = new BindingList<ProgramDTO>(listProgramFromCNC);
                dgvProgramDetails.DataSource = filenamesList;
                short RunningProgram;
                short mainProgram;
                RunningProgram = ProgramTransferPT.ReadRunningProgramNumber(this.ip, this.port, out mainProgram);
                HighLightProgramColor(RunningProgram.ToString());  //v1
            }
        }




        private void btnDonwnload_Click(object sender, EventArgs e)
        {

            if (dgvProgramDetails.Rows.Count <= 0) return;

            var progList = dgvProgramDetails.DataSource as BindingList<ProgramDTO>;
            if (progList != null)
            {
                string programsDownloaded = string.Empty;
                foreach (var item in progList.Where(item => item.Isselected == true).ToList())
                {
                    SettingsPT.pgm_selected_frm_grid = item.ProgramNo.ToString();
                    SettingsPT.pgm_Comment_selected_frm_grid = item.Comment;
                    if (!ping_success())
                    {
                        return;
                    }
                    try
                    {
                        string ip = SettingsPT.IP;
                        ushort port = SettingsPT.PORT;
                        if (SettingsPT.SYS_fldr_slctd == "")
                        {
                            FocasLibrary.Logger.WriteDebugLog("Select Destination folder.");

                            CustomDialogBoxProgramTransfer cmb = new CustomDialogBoxProgramTransfer("Error Message", @"Please select destination folder to save the file from ""Computer"" section.");
                            cmb.ShowDialog();
                            return;
                        }

                        string folder = SettingsPT.SYS_fldr_slctd;
                        string file = SettingsPT.pgm_selected_frm_grid;
                        if (file == null)
                        {
                            FocasLibrary.Logger.WriteDebugLog("Please Select a Program.");
                            CustomDialogBoxProgramTransfer cmb = new CustomDialogBoxProgramTransfer("Error Message", @"Please Select a Program to download from ""CNC Machine"" section.");
                            cmb.ShowDialog();
                            return;
                        }
                        if (treeSystm.SelectedNode.FullPath.Contains("AutoDownloadedPrograms")
                            || (!treeSystm.SelectedNode.Text.Equals("MasterPrograms", StringComparison.OrdinalIgnoreCase) &&
                                treeSystm.SelectedNode.FullPath.Contains("MasterPrograms")
                            ))
                        {
                            CustomDialogBoxProgramTransfer cmb = new CustomDialogBoxProgramTransfer("Error Message", "Program cann't be downloaded to \"AutoDownloadedPrograms\" folder. Please select some other folder and click download");
                            cmb.ShowDialog();
                            return;
                        }

                        this.Cursor = Cursors.WaitCursor;
                        if (treeSystm.SelectedNode.Text.Equals("MasterPrograms", StringComparison.OrdinalIgnoreCase))
                        {
                            if (progList.Where(item1 => item1.Isselected == true).Count() > 1)
                            {
                                CustomDialogBoxProgramTransfer cmb = new CustomDialogBoxProgramTransfer("Error Message", "Please select only one program at a time to create Master program.");
                                cmb.ShowDialog();
                                break;
                            }
                            DialogResult result = MessageBox.Show("Downloaded Program will be saved as the Master Program, Existing Mster Program will be overridden, if any. \r\n Do you want to Continue?", "MasterPrograms", MessageBoxButtons.OKCancel);
                            if (result == System.Windows.Forms.DialogResult.OK)
                            {
                                //Download the programs and sub programs as masterPrograms, if MasterProgram exists --> delete/overright it.
                                this.DownloadProgram(HomeScreen.selectedMachine, ip, port, file);
                            }
                            else
                            {
                                this.Cursor = Cursors.Default;
                                return;
                            }
                        }
                        else
                        {
                            var programDownloaded = ProgramTransferPT.DownloadProgram(ip, port, file, folder, false, SettingsPT.pgm_Comment_selected_frm_grid, false, _folderPath, isProgramFoldersSupport);
                            if (programDownloaded.Length > 10)
                            {
                                programsDownloaded = programsDownloaded + "," + Environment.NewLine + programDownloaded;
                            }
                            else
                            {
                                programsDownloaded = programsDownloaded + "," + programDownloaded;
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        FocasLibrary.Logger.WriteErrorLog(ex.ToString());
                        CustomDialogBoxProgramTransfer cmb = new CustomDialogBoxProgramTransfer("Error Message", ex.ToString());
                        cmb.ShowDialog();
                    }
                }
                this.Cursor = Cursors.Default;
                try
                {
                    BindProgramList(SettingsPT.SYS_fldr_slctd); //to refresh program list in system
                    if (!string.IsNullOrWhiteSpace(programsDownloaded))
                    {
                        CustomDialogBoxProgramTransfer cmb = new CustomDialogBoxProgramTransfer("Information Message", string.Format("Program(s) {0} " + Environment.NewLine + " saved successfully.", programsDownloaded.Trim(new char[] { ',' })));
                        cmb.ShowDialog();
                    }

                    foreach (var item in progList.Where(item => item.Isselected == true).ToList())
                    {
                        item.Isselected = false;
                    }
                    dgvProgramDetails.Refresh();

                }
                catch (Exception ex)
                {
                    FocasLibrary.Logger.WriteDebugLog(ex.ToString());
                    CustomDialogBoxProgramTransfer cmb = new CustomDialogBoxProgramTransfer("Error Message", ex.ToString());
                    cmb.ShowDialog();
                }

            }
        }

        //read comments-done, parse sub program-done, 
        //TODO - file naming in master file, file naming while saving to "AutoDownloadFolder"
        private int DownloadProgram(string machineId, string ipAddress, ushort port, string programNumber)
        {
            Hashtable hashSubPrograms = new Hashtable();
            bool result = false;
            int programNo = 0;
            int.TryParse(programNumber, out programNo);
            if (programNo == 0) return 0;
            string mainProgramMasterStr = string.Empty;
            string mainProgramCNCStr = ProgramTransferPT.DownloadProgram(ipAddress, port, programNo, out result, _folderPath, isProgramFoldersSupport);
            if (!result) return -1;
            //check if main program contains sub peogram(contains with M98P)
            string mainProgramCNCComment = FindProgramComment(mainProgramCNCStr);
            List<int> subProgramsCNC = FindSubPrograms(mainProgramCNCStr);
            List<int> subProgramsCNCTemp = new List<int>();
            if (subProgramsCNC.Count > 0)
            {
                //download sub programs starts with M98P
                foreach (var item in subProgramsCNC)
                {
                    string prgText = ProgramTransferPT.DownloadProgram(ipAddress, port, item, out result, _folderPath, isProgramFoldersSupport);
                    if (result)
                    {
                        hashSubPrograms.Add(item, prgText);
                        //find second level sub programs
                        subProgramsCNCTemp.AddRange(FindSubPrograms(prgText));
                    }
                    else
                        return -1;
                }
            }

            //download second level sub programs
            if (subProgramsCNCTemp.Count > 0)
            {
                //download sub programs starts with M98P
                foreach (var item in subProgramsCNCTemp.Distinct())
                {
                    string prgText = ProgramTransferPT.DownloadProgram(ipAddress, port, item, out result, _folderPath, isProgramFoldersSupport);
                    if (result)
                    {
                        if (!hashSubPrograms.ContainsKey(item))
                        {
                            hashSubPrograms.Add(item, prgText);
                        }
                    }
                    else
                        return -1;
                }
            }

            //O1234_yyyymmddhhmm.txt , O1234_567_yyyymmddhhmm.txt, O1234_678_yyyymmddhhmm.txt           
            CreateDirectory(_programDownloadFolder);

            //compaire the containt of main and sub program from "Master" folder under machine folder??
            string masterProgramFolderPath = Path.Combine(_programDownloadFolder, "MasterPrograms", "O" + programNumber + mainProgramCNCComment);
            string masterProgramPath = Path.Combine(masterProgramFolderPath, programNumber + mainProgramCNCComment + ".txt");

            //main program not exists, save all programs to master folder  
            CreateDirectory(masterProgramFolderPath);
            masterProgramPath = Path.Combine(masterProgramFolderPath, programNumber + mainProgramCNCComment + ".txt");
            WriteFileContent(masterProgramPath, mainProgramCNCStr);
            foreach (int item in hashSubPrograms.Keys)
            {
                string subProgramMasterFile = Path.Combine(masterProgramFolderPath, programNumber + mainProgramCNCComment + "_O" + item + ".txt");
                WriteFileContent(subProgramMasterFile, hashSubPrograms[item].ToString());
            }

            //refresh tree view
            string completePath = Path.Combine(SettingsPT.Program_path, treeSystm.Nodes[0].Tag.ToString(), "MasterPrograms");
            TreeNode treeNode = treeSystm.Nodes[0].Nodes.Cast<TreeNode>().Where(r => r.Text == "MasterPrograms").FirstOrDefault();
            if (treeNode != null)
            {
                treeNode.Nodes.Clear();
                GetFolders(new DirectoryInfo(completePath), treeNode);  //BindProgramList(completePath);
                var treeNode2 = treeNode.Nodes.Cast<TreeNode>().Where(r => r.Text == "O" + programNumber + mainProgramCNCComment).FirstOrDefault();
                if (treeNode2 != null)
                {
                    treeSystm.SelectedNode = treeNode2;
                    treeSystm_NodeMouseClick(null, null);
                }
            }

            CustomDialogBox cmb = new CustomDialogBox("Error Message", "Master Program Created");
            cmb.ShowDialog();

            return 0;
        }

        private static List<int> FindSubPrograms(string programText)
        {
            List<int> programs = new List<int>();
            if (programText.Contains("M98P"))
            {
                string[] lines = programText.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                //parse the file to findout sub-programs
                foreach (var line in lines)
                {
                    if (line.Contains("M98P"))
                    {
                        string prg = line.Remove(0, line.IndexOf("M98P") + 4);
                        Regex rgx = new Regex("[a-zA-Z ]"); //Regex.Replace(prg,"[^0-9 ]","");                       
                        prg = rgx.Replace(prg, "");
                        int p;
                        if (Int32.TryParse(prg, out p))
                        {
                            if (!programs.Contains(p))
                            {
                                programs.Add(p);
                            }
                        }
                    }
                }
            }
            return programs;
        }

        private static string FindProgramComment(string programText)
        {
            string comment = "(";
            string[] lines = programText.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines.ToList().Take(2))
            {
                if (line.Contains("(") && line.Contains(")"))
                {
                    comment += line.Substring(line.IndexOf("(") + 1, line.IndexOf(")") - line.IndexOf("(") - 1);
                    break;
                }
            }
            return SafeFileName(comment + ")");
        }


        //vas

        private static string FindProgramNumberAndComment(string programText, out string programNumber)
        {
            string comment = "(";
            programNumber = "";
            string[] lines = programText.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines.ToList().Take(2))
            {

                if (line.Contains("O"))
                {
                    if (SettingsPT.IsProgramFoldersSupport == false)
                    {
                        string prog = line;
                        if (line.Contains("("))
                        {
                            prog = prog.Substring(prog.IndexOf("O") + 1, prog.IndexOf("(") - 1);
                        }
                        else
                        {
                            Regex rgx = new Regex("[a-zA-Z() ]"); //Regex.Replace(prg,"[^0-9 ]","");                       
                            prog = rgx.Replace(prog, "");
                        }
                        int p;
                        if (SettingsPT.IsProgramFoldersSupport == false)
                        {
                            if (Int32.TryParse(prog, out p))
                            {
                                programNumber = p.ToString();
                            }
                        }
                        break;
                    }
                    else
                    {
                        string prog = line;
                        if (line.Contains("("))
                        {
                            prog = prog.Substring(0, prog.IndexOf("(") - 1);
                            programNumber = prog.ToString();
                        }
                        else
                        {
                            programNumber = prog;
                        }
                    }
                }

            }
            foreach (var line in lines.ToList().Take(2))
            {
                if (line.Contains("(") && line.Contains(")"))
                {
                    comment += line.Substring(line.IndexOf("(") + 1, line.IndexOf(")") - line.IndexOf("(") - 1);
                    break;
                }
            }
            return SafeFileName(comment + ")");
        }


        private static bool CompareContents(string str1, string str2)
        {
            if (str1.Equals(str2, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        }

        private static string ReadFileContent(string filePath)
        {
            try
            {
                return File.ReadAllText((filePath));
            }
            catch (Exception ex)
            {
                FocasLibrary.Logger.WriteErrorLog(ex.ToString());
            }

            return string.Empty;
        }

        private static bool WriteFileContent(string filePath, string str)
        {
            try
            {
                File.WriteAllText((filePath), str);
                return true;
            }
            catch (Exception ex)
            {
                FocasLibrary.Logger.WriteErrorLog(ex.ToString());
            }

            return false;
        }

        public static string SafeFileName(string name)
        {
            StringBuilder str = new StringBuilder(name);
            foreach (char c in System.IO.Path.GetInvalidFileNameChars())
            {
                str = str.Replace(c, '_');
            }
            return str.ToString();
        }

        public static string SafePathName(string name)
        {
            StringBuilder str = new StringBuilder(name);

            foreach (char c in System.IO.Path.GetInvalidPathChars())
            {
                str = str.Replace(c, '_');
            }
            return str.ToString();
        }

        public static bool CreateDirectory(string masterProgramFolderPath)
        {
            var safeMasterProgramFolderPath = SafePathName(masterProgramFolderPath);
            if (!Directory.Exists(safeMasterProgramFolderPath))
            {
                try
                {
                    Directory.CreateDirectory(safeMasterProgramFolderPath);
                }
                catch (Exception ex)
                {
                    FocasLibrary.Logger.WriteErrorLog(ex.ToString());
                    return false;
                }
            }
            return true;
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (listsysPrograms.Items.Count == 0 || dgvProgramDetails.Rows.Count == 0)
            {
                return;
            }


            if (listsysPrograms.SelectedItem == null || listsysPrograms.SelectedItem.ToString() == string.Empty)
            {
                CustomDialogBox cmb = new CustomDialogBox("Warning Message", @"Please select a program to upload from ""Computer"" section");
                cmb.ShowDialog();
                return;
            }

            if (!ping_success())
            {
                return;
            }
            this.Cursor = Cursors.WaitCursor;
            UploadProgramCSharp();
            this.Cursor = Cursors.Default;
        }

        private void UploadProgramCSharp()
        {
            if (listsysPrograms.SelectedItem == null || listsysPrograms.SelectedItem.ToString() == string.Empty) return;
            string selectedProgram = listsysPrograms.SelectedItem.ToString();
            string programNumber = string.Empty;
            bool status = false;
            //selectedProgram = selectedProgram.Substring(1);
            selectedProgram = Path.GetFileNameWithoutExtension(selectedProgram);
            selectedProgram = Path.GetFileNameWithoutExtension(selectedProgram);

            if (selectedProgram.IndexOfAny(new char[] { '(' }) >= 0)
            {
                selectedProgram = selectedProgram.Substring(0, selectedProgram.IndexOfAny(new char[] { '(' }));
            }
            status = ProgramTransferPT.CurrentlyRunning(ip, port, selectedProgram);
            if (status)
            {
                FocasLibrary.Logger.WriteDebugLog("selected program is currently running. we can not upload.");
                CustomDialogBox cmb = new CustomDialogBox("Information Message", @"selected program is currently running. we can not upload.");
                cmb.ShowDialog();
                return;
            }
            else
            {
                status = false;
                status = ProgramTransferPT.CheckProgramExistence(selectedProgram, listProgramFromCNC); // ls is global variable it conatains pograms present in cnc
                if (status)
                {
                    FocasLibrary.Logger.WriteDebugLog(String.Format("Program {0} already exists. Do you want to replace it.", selectedProgram));

                    DialogResult result = MessageBox.Show(String.Format("Program {0} already exists. Do you want to replace it ?", selectedProgram), "Delete Confirmation!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        string filePath = SettingsPT.SYS_fldr_slctd;
                        //filePath = Path.Combine(filePath, listsysPrograms.SelectedItem.ToString().Substring(1).ToString() + ".txt");
                        filePath = Path.Combine(filePath, listsysPrograms.SelectedItem.ToString().ToString() + ".txt");
                        if (!File.Exists(filePath)) return;
                        string tempProgram = File.ReadAllText(filePath);
                        status = false;
                        ProgramTransferPT.DeletePrograms(SettingsPT.IP, SettingsPT.PORT, selectedProgram, _folderPath, isProgramFoldersSupport);
                        status = ProgramTransferPT.UploadProgram(ip, port, tempProgram, _folderPath, isProgramFoldersSupport);
                        if (status)
                        {
                            FocasLibrary.Logger.WriteDebugLog("Program Uploaded Successfully.");
                            BindProgramListGridview();

                            CustomDialogBox cmb = new CustomDialogBox("Information Message", "Program Uploaded Successfully.");
                            cmb.ShowDialog();
                        }
                    }
                }
                else
                {
                    string filePath = SettingsPT.SYS_fldr_slctd;
                    filePath = Path.Combine(filePath, listsysPrograms.SelectedItem.ToString().Substring(1).ToString() + ".txt");
                    if (!File.Exists(filePath)) return;
                    string tempProgram = File.ReadAllText(filePath);
                    status = false;
                    status = ProgramTransferPT.UploadProgram(ip, port, tempProgram, _folderPath, isProgramFoldersSupport);
                    if (status)
                    {
                        FocasLibrary.Logger.WriteDebugLog("Program Uploaded Successfully.");
                        BindProgramListGridview();

                        CustomDialogBox cmb = new CustomDialogBox("Information Message", "Program Uploaded Successfully.");
                        cmb.ShowDialog();

                    }
                }
            }
        }






        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvProgramDetails.Rows.Count <= 0) return;
            SettingsPT.pgm_selected_frm_grid = (dgvProgramDetails.Rows[dgvProgramDetails.SelectedCells[0].RowIndex].Cells[1].Value.ToString());
            if (!ping_success())
            {
                return;
            }
            try
            {
                if (SettingsPT.pgm_selected_frm_grid == "")
                {
                    FocasLibrary.Logger.WriteDebugLog(@"Please Select The Program from ""CNC Machine"" section");
                    string exx = @"Please select a program to delete from CNC machine.";
                    CustomDialogBoxProgramTransfer cmb = new CustomDialogBoxProgramTransfer("Information Message", exx.ToString());
                    cmb.ShowDialog();
                    return;
                }
                string folder = SettingsPT.SYS_fldr_slctd;
                string ip = SettingsPT.IP;
                ushort port = SettingsPT.PORT;
                string file = SettingsPT.pgm_selected_frm_grid;
                DialogResult result = MessageBox.Show("Are you Sure to delete the program " + SettingsPT.pgm_selected_frm_grid + " from CNC machine?", "Delete Confirmation!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    return;
                }
                this.Cursor = Cursors.WaitCursor;
                bool rslt = DeleteProgramCSharp();
                this.Cursor = Cursors.Default;
                if (rslt)
                {
                    BindProgramListGridview();
                    CustomDialogBox cmb = new CustomDialogBox("Information Message", "Program Deleted Successfully.");
                    cmb.ShowDialog();
                }
                else
                {
                    CustomDialogBox cmb = new CustomDialogBox("Information Message", "Not able to delete the program. Plese try later.");
                    cmb.ShowDialog();
                }
                return;
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
        }

        private bool DeleteProgramCSharp()
        {
            bool status = false;
            status = ProgramTransferPT.CheckProgramExistence(SettingsPT.pgm_selected_frm_grid, listProgramFromCNC);
            if (!status)
            {
                FocasLibrary.Logger.WriteDebugLog("Program does not exist in cnc machine.");

                CustomDialogBox cmb = new CustomDialogBox("Information Message", "Program does not exist in cnc machine.");
                cmb.ShowDialog();
                return false;
            }
            else
            {
                status = ProgramTransferPT.CurrentlyRunning(ip, port, SettingsPT.pgm_selected_frm_grid);
                if (status)
                {
                    FocasLibrary.Logger.WriteDebugLog("selected program is currently running, so we can't delete this program.");
                    CustomDialogBox cmb = new CustomDialogBox("Information Message", "selected program is currently running, so we can't delete this program.");
                    cmb.ShowDialog();
                    return false;
                }

            }

            return ProgramTransferPT.DeletePrograms(ip, port, SettingsPT.pgm_selected_frm_grid, _folderPath, isProgramFoldersSupport);
        }

        private void dgvProgramDetails_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Clicks == 2)
            {
                if (e.RowIndex >= 0)
                {
                    btnViewCNCProgram_Click(null, EventArgs.Empty);
                    return;
                }
            }

            if (e.Button != System.Windows.Forms.MouseButtons.Left) return;
            try
            {
                DataGridView dgv = (DataGridView)sender;
                int ri = e.RowIndex;
                if (ri < 0)//if header raw?
                {
                    return;
                }
                if (e.ColumnIndex == 0)
                {
                    return;
                }
                SettingsPT.pgm_selected_frm_grid = (dgv.Rows[dgv.SelectedCells[0].RowIndex].Cells[1].Value.ToString());
                SettingsPT.pgm_Comment_selected_frm_grid = (dgv.Rows[dgv.SelectedCells[0].RowIndex].Cells[2].Value.ToString());
                DragDropDTO obj = new DragDropDTO { Comment = SettingsPT.pgm_Comment_selected_frm_grid, programNo = SettingsPT.pgm_selected_frm_grid };

                dgvProgramDetails.DoDragDrop(obj, DragDropEffects.Copy);

            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
        }

        private void dataGridView1_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = DragDropEffects.Copy;
            }
            catch (Exception ex)
            {
                FocasLibrary.Logger.WriteErrorLog("Error..!! ProgramTransfer : \n " + ex.ToString());
            }
        }

        private void dataGridView1_DragDrop(object sender, DragEventArgs e)
        {

            try
            {
                string folder_file = null;
                string programNumber = string.Empty;
                folder_file = (string)e.Data.GetData(typeof(string));
                if (string.IsNullOrWhiteSpace(folder_file) || folder_file.Contains("CNC"))//if a drop from form
                {
                    return;
                }
                else//if file drop?
                {
                    string[] files = null;
                    if ((string[])e.Data.GetData(DataFormats.FileDrop) != null)//file is there?
                    {
                        files = (string[])e.Data.GetData(DataFormats.FileDrop);
                        folder_file = files[0];
                    }
                }

                //NB dont change ping from here//
                if (!ping_success())
                {
                    return;
                }

                var selectedProgram = Path.GetFileNameWithoutExtension(folder_file);
                selectedProgram = Path.GetFileNameWithoutExtension(selectedProgram);
                if (selectedProgram.IndexOfAny(new char[] { '(' }) >= 0)
                {
                    selectedProgram = selectedProgram.Substring(0, selectedProgram.IndexOfAny(new char[] { '(' }));
                }
                bool status = false;
                status = ProgramTransferPT.CurrentlyRunning(ip, port, selectedProgram);
                if (status)
                {
                    FocasLibrary.Logger.WriteDebugLog("selected program is currently running. we can not upload.");

                    CustomDialogBoxProgramTransfer cmb = new CustomDialogBoxProgramTransfer("Information Message", "selected program is currently running, so we can't Upload this program.");
                    cmb.ShowDialog();
                    return;
                }
                else
                {
                    status = false;
                    status = ProgramTransferPT.CheckProgramExistence(selectedProgram, listProgramFromCNC); // ls is global variable it conatains pograms present in cnc
                    if (status)
                    {
                        FocasLibrary.Logger.WriteDebugLog(String.Format("Program {0} already exists. Do you want to replace it", selectedProgram));
                        //TODO - show yees/no, on Yes, delete the program and upload it. keep backup of the download program
                        //MessageBox.Show(String.Format("Program {0} Already exists. Do you want to replace it", selectedProgram));
                        DialogResult result = MessageBox.Show(String.Format("Program {0} already exists. Do you want to replace it ?", selectedProgram), "Delete Confirmation!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == System.Windows.Forms.DialogResult.Yes)
                        {
                            string filePath = SettingsPT.SYS_fldr_slctd;
                            filePath = Path.Combine(filePath, listsysPrograms.SelectedItem.ToString().Substring(1).ToString() + ".txt");
                            if (!File.Exists(filePath)) return;
                            string tempProgram = File.ReadAllText(filePath);
                            status = false;
                            this.UseWaitCursor = true;
                            dgvProgramDetails.UseWaitCursor = true;
                            Application.DoEvents();
                            ProgramTransferPT.DeletePrograms(SettingsPT.IP, SettingsPT.PORT, selectedProgram, _folderPath, isProgramFoldersSupport);
                            status = ProgramTransferPT.UploadProgram(ip, port, tempProgram, _folderPath, isProgramFoldersSupport);
                            if (status)
                            {
                                FocasLibrary.Logger.WriteDebugLog("Program Uploaded Successfully.");
                                //TODO - Satya - Refresh the program grid
                                BindProgramListGridview();
                                CustomDialogBox cmb = new CustomDialogBox("Information Message", "Program Uploaded Successfully.");
                                cmb.ShowDialog();

                            }
                            this.UseWaitCursor = false;
                            dgvProgramDetails.UseWaitCursor = false;
                        }
                    }
                    else
                    {
                        string filePath = SettingsPT.SYS_fldr_slctd;
                        filePath = Path.Combine(filePath, listsysPrograms.SelectedItem.ToString().Substring(1).ToString() + ".txt");
                        if (!File.Exists(filePath)) return;
                        string tempProgram = File.ReadAllText(filePath);
                        status = false;
                        this.UseWaitCursor = true;
                        dgvProgramDetails.UseWaitCursor = true;
                        Application.DoEvents();
                        status = ProgramTransferPT.UploadProgram(ip, port, tempProgram, _folderPath, isProgramFoldersSupport);
                        if (status)
                        {
                            FocasLibrary.Logger.WriteDebugLog("Program Uploaded Successfully.");
                            BindProgramListGridview();
                            CustomDialogBox cmb = new CustomDialogBox("Information Message", "Program Uploaded Successfully.");
                            cmb.ShowDialog();

                        }
                        this.UseWaitCursor = false;
                        dgvProgramDetails.UseWaitCursor = false;

                    }
                }
            }
            catch (Exception ex)//
            {
                FocasLibrary.Logger.WriteErrorLog("Error..!! ProgramTransfer : \n " + ex.ToString());
            }
        }

        private void listsysPrograms_MouseDown_1(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 2)
            {
                btnViewSystemProgram_Click(null, EventArgs.Empty);
                return;
            }
            if (e.Button != System.Windows.Forms.MouseButtons.Left) return;
            try
            {
                string folder = "";
                string file = "";
                if (SettingsPT.SYS_fldr_slctd != "" || listsysPrograms.SelectedItem != null)
                {
                    folder = SettingsPT.SYS_fldr_slctd;
                    file = listsysPrograms.SelectedItem.ToString();
                    if (file == "")
                    {
                        return;
                    }
                    file = file.Substring(1);//avoids "O"//                   
                    string path = Path.Combine(folder, file + ".txt");
                    listsysPrograms.DoDragDrop((string)path, DragDropEffects.Copy);
                }
            }
            catch (Exception ex)
            {
                FocasLibrary.Logger.WriteErrorLog("Error..!! ProgramTransfer : \n " + ex.ToString());
            }

        }

        private void listsysPrograms_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                DragDropDTO dto = e.Data.GetData(typeof(DragDropDTO)) as DragDropDTO;
                if (dto == null)
                {
                    //MessageBox.Show("File Drop is allowed from CNC list Only!!!!");
                    return;
                }

                string ip = SettingsPT.IP;
                ushort port = SettingsPT.PORT;
                if (SettingsPT.SYS_fldr_slctd == "")
                {
                    CustomDialogBox cmb = new CustomDialogBox("Information Message", "Please select the Destination folder.");
                    cmb.ShowDialog();
                    return;
                }
                string folder = SettingsPT.SYS_fldr_slctd; ;
                if (!ping_success())
                {
                    return;
                }
                this.UseWaitCursor = true;
                dgvProgramDetails.UseWaitCursor = true;
                Application.DoEvents();

                ProgramTransferPT.DownloadProgram(ip, port, dto.programNo, folder, false, dto.Comment, true, _folderPath, isProgramFoldersSupport);
                try
                {
                    BindProgramList(SettingsPT.SYS_fldr_slctd);
                }
                catch (Exception ex)
                {
                    FocasLibrary.Logger.WriteErrorLog("Error..!! ProgramTransfer : \n " + ex.ToString());
                    CustomDialogBoxProgramTransfer cmb = new CustomDialogBoxProgramTransfer("Information Message", ex.ToString());
                    cmb.ShowDialog();
                }
                this.UseWaitCursor = false;
                dgvProgramDetails.UseWaitCursor = false;
                Application.DoEvents();

            }
            catch (Exception ex)
            {
                CustomDialogBoxProgramTransfer cmb = new CustomDialogBoxProgramTransfer("Information Message", ex.ToString());
                cmb.ShowDialog();
                FocasLibrary.Logger.WriteErrorLog("Error..!! ProgramTransfer : \n " + ex.ToString());
            }

        }


        private void btnCompare_Click(object sender, EventArgs e)
        {
            if (dgvProgramDetails.Rows.Count <= 0) return;
            SettingsPT.pgm_selected_frm_grid = (dgvProgramDetails.Rows[dgvProgramDetails.SelectedCells[0].RowIndex].Cells[1].Value.ToString());
            SettingsPT.pgm_Comment_selected_frm_grid = (dgvProgramDetails.Rows[dgvProgramDetails.SelectedCells[0].RowIndex].Cells[2].Value.ToString());
            if (!ping_success())
            {
                return;
            }
            string file2 = null;
            file2 = GetSystemFile();
            if (string.IsNullOrEmpty(file2))
            {
                FocasLibrary.Logger.WriteDebugLog("Select a program from \"Computer\".");
                CustomDialogBox cmb = new CustomDialogBox("Information Message", "Select a program from \"Computer\".");
                cmb.ShowDialog();
                return;
            }
            string file1 = string.Empty;
            this.Cursor = Cursors.WaitCursor;


            file1 = ProgramTransferPT.DownloadProgram(ip, port, SettingsPT.pgm_selected_frm_grid, Path.Combine(SettingsPT.Program_path, HomeScreen.selectedMachine), true, SettingsPT.pgm_Comment_selected_frm_grid, true, _folderPath, this.isProgramFoldersSupport);
            this.Cursor = Cursors.Default;
            if (string.IsNullOrEmpty(file1))
            {
                FocasLibrary.Logger.WriteDebugLog(string.Format("Download failed for program : {0} ", SettingsPT.pgm_selected_frm_grid));

                CustomDialogBoxProgramTransfer cmb = new CustomDialogBoxProgramTransfer("Information Message", string.Format("Download failed for program : {0} ", SettingsPT.pgm_selected_frm_grid));
                cmb.ShowDialog();
                return;
            }
            call_winmerge(file1, file2);
        }

        private void call_winmerge(string file1, string file2)
        {
            try
            {
                SettingsPT.files_to_compare = file1 + "$$$" + file2;
                ThreadStart job = new ThreadStart(compare_files);
                Thread t = new Thread(job);
                t.Start();
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
        }

        private void delete_dwn_file(string file1)
        {
            try
            {
                File.Delete(file1);
            }
            catch (Exception ex)
            {
                FocasLibrary.Logger.WriteErrorLog("Error..!! ProgramTransfer : \n " + ex.ToString());
            }
        }

        private string GetSystemFile()
        {
            try
            {
                if (listsysPrograms.SelectedIndex == -1)
                {
                    return "";
                }
                string pgm = listsysPrograms.SelectedItem.ToString();

                // pgm = pgm.Substring(1);
                string folder = SettingsPT.SYS_fldr_slctd;//get from mouse click evnt of tree//
                string tempPath = Path.Combine(folder, pgm + ".txt");
                return tempPath;
            }
            catch (Exception ex)
            {
                FocasLibrary.Logger.WriteErrorLog("Error..!! ProgramTransfer : \n " + ex.ToString());
                CustomDialogBoxProgramTransfer cmb = new CustomDialogBoxProgramTransfer("Information Message", ex.ToString());
                cmb.ShowDialog();
                return null;
            }
        }

        public void compare_files()
        {
            try
            {
                string InputFile1, inputfile2;
                string files_to_cmpr = SettingsPT.files_to_compare;
                string[] file_arr = files_to_cmpr.Split(new string[] { "$$$" }, StringSplitOptions.RemoveEmptyEntries);
                InputFile1 = file_arr[0];
                inputfile2 = file_arr[1];

                InputFile1 = "\"" + InputFile1 + "\"";
                inputfile2 = "\"" + inputfile2 + "\"";
                ProcessStartInfo info = new ProcessStartInfo();
                info.FileName = "winmerge.exe";
                info.Arguments = InputFile1 + " " + inputfile2;
                try
                {
                    Process p = Process.Start(info);
                    p.WaitForExit();
                    return;
                }
                catch (Exception ex)
                {
                    FocasLibrary.Logger.WriteErrorLog("Error..!! ProgramTransfer : \n " + ex.ToString());
                }
            }
            catch (Exception ex)
            {
                FocasLibrary.Logger.WriteErrorLog("Error..!! ProgramTransfer : \n " + ex.ToString());
            }
        }

        private void show_file(string file1)
        {
            try
            {
                if (!File.Exists(file1)) return;
                SettingsPT.file_to_open = file1;
                ThreadStart job = new ThreadStart(OpenTextFile);
                Thread t = new Thread(job);
                t.Start();
            }
            catch (Exception ex)
            {
                FocasLibrary.Logger.WriteErrorLog("Error..!! ProgramTransfer : \n " + ex.ToString());
            }
        }

        private void OpenTextFile()
        {
            string InputFile1 = SettingsPT.file_to_open;
            ProcessStartInfo info = new ProcessStartInfo();
            info.CreateNoWindow = true;
            info.UseShellExecute = false;
            info.WindowStyle = ProcessWindowStyle.Hidden;
            info.FileName = "notepad.exe";
            info.Arguments = InputFile1;
            try
            {
                Process p = Process.Start(info);
                // p.WaitForExit();
            }
            catch (Exception ex)
            {
                FocasLibrary.Logger.WriteErrorLog("Error..!! ProgramTransfer : \n " + ex.ToString());
            }
        }

        private void btnViewSystemProgram_Click(object sender, EventArgs e)
        {
            if (listsysPrograms.Items.Count == 0) return;
            try
            {
                string file2 = null;
                file2 = GetSystemFile();
                if (file2 == null || file2 == "")
                {
                    string exx = "Please select a program from \"Computer\".";
                    CustomDialogBoxProgramTransfer cmb = new CustomDialogBoxProgramTransfer("Information Message", exx.ToString());
                    cmb.ShowDialog();
                    return;
                }
                SettingsPT.file_to_open = file2;
                OpenTextFile();
                return;
            }
            catch (Exception ex)
            {
                FocasLibrary.Logger.WriteErrorLog("Error..!! ProgramTransfer : \n " + ex.ToString());
            }
        }

        private void btnViewCNCProgram_Click(object sender, EventArgs e)
        {
            if (dgvProgramDetails.Rows.Count <= 0 || dgvProgramDetails.SelectedCells == null) return;
            SettingsPT.pgm_selected_frm_grid = (dgvProgramDetails.Rows[dgvProgramDetails.SelectedCells[0].RowIndex].Cells[1].Value.ToString());
            SettingsPT.pgm_Comment_selected_frm_grid = (dgvProgramDetails.Rows[dgvProgramDetails.SelectedCells[0].RowIndex].Cells[2].Value.ToString());
            if (!ping_success())
            {
                return;
            }
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string file1 = null;
                file1 = ProgramTransferPT.DownloadProgram(ip, port, SettingsPT.pgm_selected_frm_grid, SettingsPT.SYS_fldr_slctd, true, SettingsPT.pgm_Comment_selected_frm_grid, true, _folderPath, isProgramFoldersSupport);
                if (file1 == null || file1 == "")
                {
                    FocasLibrary.Logger.WriteDebugLog(string.Format("Download failed for program : {0}", SettingsPT.pgm_selected_frm_grid));
                    string exx = (string.Format("Download failed for program : {0}", SettingsPT.pgm_selected_frm_grid));
                    CustomDialogBoxProgramTransfer cmb = new CustomDialogBoxProgramTransfer("Information Message", exx.ToString());
                    cmb.ShowDialog();
                    return;
                }
                show_file(file1);
            }
            catch (Exception ex)
            {
                CustomDialogBoxProgramTransfer cmb = new CustomDialogBoxProgramTransfer("Information Message", ex.ToString());
                cmb.ShowDialog();
                FocasLibrary.Logger.WriteErrorLog("Error..!! ProgramTransfer : \n " + ex.ToString());
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        public bool ping_success()
        {

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Ping p = new Ping();
                PingReply reply = p.Send(SettingsPT.IP, 2000);
                if (reply.Status == IPStatus.Success)
                    return true;
                else
                {
                    Cursor.Current = Cursors.Default;
                    FocasLibrary.Logger.WriteDebugLog("Ping failure. Status = " + reply.Status.ToString());
                    string exx = ("Not able to Ping to the machine. Please check the network connection. Ping return status = " + reply.Status.ToString());
                    CustomDialogBoxProgramTransfer cmb = new CustomDialogBoxProgramTransfer("Information Message", exx.ToString());
                    cmb.ShowDialog();
                    return false;
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                FocasLibrary.Logger.WriteErrorLog("Error..!! ProgramTransfer : \n " + ex.ToString());
                CustomDialogBoxProgramTransfer cmb = new CustomDialogBoxProgramTransfer("Information Message", ex.ToString());
                cmb.ShowDialog();
                return false;
            }
            finally
            {

            }

        }

        private void BindProgramTree()
        {
            SetTreeStyle();
            treeSystm.Nodes.Clear();

            SetToolTip();
        }

        private void SetToolTip()
        {
            ToolTip toltip = new ToolTip();
            toltip.SetToolTip(btnDownloadProgram, "Download a program from CNC machine.");
            toltip.SetToolTip(btnUpload, "Upload a program to CNC from computer machine.");
        }

        private void treeSystm_DragDrop(object sender, DragEventArgs e)
        {

            try
            {
                string[] files_frm_explorer = null;
                files_frm_explorer = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files_frm_explorer != null && files_frm_explorer[0].Length > 0)
                {
                    FocasLibrary.Logger.WriteDebugLog("File Drop is allowed from CNC list Only.");
                    CustomDialogBoxProgramTransfer cmb = new CustomDialogBoxProgramTransfer("Information Message", "File Drop is allowed from CNC machine section Only.");
                    cmb.ShowDialog();
                    return;
                }
                string file = (string)e.Data.GetData(typeof(string));

                if (!file.Contains("CNC"))
                {
                    FocasLibrary.Logger.WriteDebugLog("Select program from CNC-Program list.");

                    CustomDialogBoxProgramTransfer cmb = new CustomDialogBoxProgramTransfer("Information Message", "Select program from CNC-Program list.");
                    cmb.ShowDialog();
                    return;//to avoid drop from system program list ie,self drop//
                }

                file = file.Substring(3);//avoids "CNC" part//
                string ip = SettingsPT.IP;
                ushort port = SettingsPT.PORT;
                if (SettingsPT.SYS_fldr_slctd == "")
                {
                    CustomDialogBoxProgramTransfer cmb = new CustomDialogBoxProgramTransfer("Information Message", "Select Destination Folder.");
                    cmb.ShowDialog();
                    return;
                }
                string folder = SettingsPT.SYS_fldr_slctd;
                if (!ping_success())
                {
                    return;
                }
                //TODO - satya
                ProgramTransferPT.DownloadProgram(ip, port, file, folder, false, "", true, _folderPath, isProgramFoldersSupport);
                try
                {
                    BindProgramList(SettingsPT.SYS_fldr_slctd);
                }
                catch (Exception ex)
                {
                    FocasLibrary.Logger.WriteErrorLog("Error..!! ProgramTransfer : \n " + ex.ToString());
                    CustomDialogBoxProgramTransfer cmb = new CustomDialogBoxProgramTransfer("Information Message", ex.ToString());
                    cmb.ShowDialog();

                }
            }
            catch (Exception ex)
            {
                FocasLibrary.Logger.WriteErrorLog("Error..!! ProgramTransfer : \n " + ex.ToString());
                CustomDialogBoxProgramTransfer cmb = new CustomDialogBoxProgramTransfer("Information Message", ex.ToString());
                cmb.ShowDialog();
            }

        }

        private void treeSystm_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void listsysPrograms_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void treeSystm_MouseMove(object sender, MouseEventArgs e)
        {
            // Get the node at the current mouse pointer location.
            TreeNode theNode = this.treeSystm.GetNodeAt(e.X, e.Y);

            // Set a ToolTip only if the mouse pointer is actually paused on a node.
            if ((theNode != null))
            {
                // Verify that the tag property is not "null".
                if (theNode.Tag != null)
                {
                    // Change the ToolTip only if the pointer moved to a new node.
                    if (theNode.Tag.ToString() != toolTip1.GetToolTip(this.treeSystm))
                    {
                        toolTip1.SetToolTip(this.treeSystm, theNode.Tag.ToString());
                    }
                }
                else
                {
                    toolTip1.SetToolTip(this.treeSystm, "");
                }
            }
            else     // Pointer is not over a node so clear the ToolTip.
            {
                toolTip1.SetToolTip(this.treeSystm, "");
            }
        }

        private void listView2_DragDrop(object sender, DragEventArgs e)
        {
            string programNumber = string.Empty;

            DragDropDTO dto = e.Data.GetData(typeof(DragDropDTO)) as DragDropDTO;
            if (dto != null)
            {
                string ip = SettingsPT.IP;
                ushort port = SettingsPT.PORT;
                string folder = string.Empty;
                if (!ping_success())
                {
                    return;
                }
                this.UseWaitCursor = true;
                Application.DoEvents();
                try
                {

                    var file1 = ProgramTransferPT.DownloadProgram(ip, port, dto.programNo, SettingsPT.SYS_fldr_slctd, true, dto.Comment, true, _folderPath, this.isProgramFoldersSupport);
                    var item = new listBoxItem { text = Path.GetFileNameWithoutExtension(file1), value = file1 };

                }
                catch (Exception ex)
                {
                    CustomDialogBoxProgramTransfer cmb = new CustomDialogBoxProgramTransfer("Information Message", ex.ToString());
                    cmb.ShowDialog();
                }
                this.UseWaitCursor = false;
                return;
            }

            //drop from Computer list box
            string file = (string)e.Data.GetData(typeof(string));
            if (!string.IsNullOrWhiteSpace(file))
            {
                var item = new listBoxItem { text = Path.GetFileNameWithoutExtension(file), value = file };
            }

        }

        private void listView2_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void listView1_DragDrop(object sender, DragEventArgs e)
        {

            string programNumber = string.Empty;

            DragDropDTO dto = e.Data.GetData(typeof(DragDropDTO)) as DragDropDTO;
            if (dto != null)
            {
                string ip = SettingsPT.IP;
                ushort port = SettingsPT.PORT;
                string folder = string.Empty;
                if (!ping_success())
                {
                    return;
                }
                this.UseWaitCursor = true;
                Application.DoEvents();
                try
                {

                    var file1 = ProgramTransferPT.DownloadProgram(ip, port, dto.programNo, SettingsPT.SYS_fldr_slctd, true, dto.Comment, true, _folderPath, this.isProgramFoldersSupport);
                    var item = new listBoxItem { text = Path.GetFileNameWithoutExtension(file1), value = file1 };

                }
                catch (Exception ex)
                {
                    CustomDialogBoxProgramTransfer cmb = new CustomDialogBoxProgramTransfer("Information Message", ex.ToString());
                    cmb.ShowDialog();
                }
                this.UseWaitCursor = false;
                return;
            }

            //drop from Computer list box
            string file = (string)e.Data.GetData(typeof(string));
            if (!string.IsNullOrWhiteSpace(file))
            {
                var item = new listBoxItem { text = Path.GetFileNameWithoutExtension(file), value = file };
            }

        }

        private void listView1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }



        private void btmMasterArea_Click(object sender, EventArgs e)
        {

            // if (rdoMasterAres.Checked)

            //Create Master
            try
            {
                string mainProgramNo = "";
                string mainProgramCNCComment = string.Empty;
                // if (listBox1.Items.Count > 0)
                //{
                this.Cursor = Cursors.WaitCursor;
                var file1 = "";//((listBoxItem)listBox1.Items[0]).value;
                string mainProgramMasterStr = string.Empty;
                string mainProgramCNCStr = File.ReadAllText(file1);
                mainProgramCNCComment = FindProgramNumberAndComment(mainProgramCNCStr, out mainProgramNo);

                //O1234_yyyymmddhhmm.txt , O1234_567_yyyymmddhhmm.txt, O1234_678_yyyymmddhhmm.txt           
                CreateDirectory(_programDownloadFolder);

                string masterProgramFolderPath = Path.Combine(_programDownloadFolder, "MasterPrograms", "O" + mainProgramNo + mainProgramCNCComment);
                string masterProgramPath = Path.Combine(masterProgramFolderPath, mainProgramNo + mainProgramCNCComment + ".txt");

                //main program not exists, save all programs to master folder  
                CreateDirectory(masterProgramFolderPath);
                masterProgramPath = Path.Combine(masterProgramFolderPath, mainProgramNo + mainProgramCNCComment + ".txt");
                WriteFileContent(masterProgramPath, mainProgramCNCStr);


                //if (listBox2.Items.Count > 0)
                {
                    //foreach (listBoxItem item in listBox2.Items)
                    //{
                    //    int subProgramNo = 0;
                    //    var Subfile1 = item.value;
                    //    string subProgramCNCStr = File.ReadAllText(Subfile1);
                    //    string subProgramCNCComment = FindProgramNumberAndComment(subProgramCNCStr, out subProgramNo);

                    //    //O1234_yyyymmddhhmm.txt , O1234_567_yyyymmddhhmm.txt, O1234_678_yyyymmddhhmm.txt           
                    //    CreateDirectory(_programDownloadFolder);

                    //    masterProgramFolderPath = Path.Combine(_programDownloadFolder, "MasterPrograms", "O" + mainProgramNo + mainProgramCNCComment);

                    //    //main program not exists, save all programs to master folder  
                    //    CreateDirectory(masterProgramFolderPath);
                    //    masterProgramFolderPath = Path.Combine(masterProgramFolderPath, mainProgramNo + mainProgramCNCComment + "_O" + subProgramNo + ".txt");
                    //    WriteFileContent(masterProgramFolderPath, subProgramCNCStr);
                    //}
                }

                //refresh tree view
                string completePath = Path.Combine(SettingsPT.Program_path, treeSystm.Nodes[0].Tag.ToString(), "MasterPrograms");
                TreeNode treeNode = treeSystm.Nodes[0].Nodes.Cast<TreeNode>().Where(r => r.Text == "MasterPrograms").FirstOrDefault();
                if (treeNode != null)
                {
                    treeNode.Nodes.Clear();
                    GetFolders(new DirectoryInfo(completePath), treeNode);  //BindProgramList(completePath);
                    var treeNode2 = treeNode.Nodes.Cast<TreeNode>().Where(r => r.Text == "O" + mainProgramNo + mainProgramCNCComment).FirstOrDefault();
                    if (treeNode2 != null)
                    {
                        treeSystm.SelectedNode = treeNode2;
                        treeSystm_NodeMouseClick(null, null);
                    }
                }


                CustomDialogBox cmb = new CustomDialogBox("Information Message", "Master Program created.");
                cmb.ShowDialog();
                this.Cursor = Cursors.Default;
            }
            catch (Exception exx)
            {
                CustomDialogBox cmb = new CustomDialogBox("Information Message", exx.ToString());
                cmb.ShowDialog();
                FocasLibrary.Logger.WriteErrorLog(exx.ToString());
            }

        }

        private void listsysPrograms_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnUpload.Enabled = true;
        }

        private void listsysPrograms_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            splitContainer2.SplitterDistance = splitContainer1.SplitterDistance;
            splitContainer2.SplitterDistance = splitContainer1.SplitterDistance;
        }

        private void btnExpandOrContract_Click(object sender, EventArgs e)
        {
            if (isContract)
            {
                isContract = false;
                tableLayoutPanel8.ColumnStyles[0].SizeType = SizeType.Absolute;
                tableLayoutPanel8.ColumnStyles[0].Width = 162;
                toolTip1.SetToolTip(this.btnExpandOrContract, "Menu Contract");
                btnExpandOrContract.BackgroundImage = Image.FromFile(Path.Combine(appPath, "Images\\b_pre.png"));
            }
            else
            {
                isContract = true;
                tableLayoutPanel8.ColumnStyles[0].SizeType = SizeType.Absolute;
                tableLayoutPanel8.ColumnStyles[0].Width = 0;
                toolTip1.SetToolTip(this.btnExpandOrContract, "Menu Expand");
                btnExpandOrContract.BackgroundImage = Image.FromFile(Path.Combine(appPath, "Images\\b_Nex.png"));
            }
        }

        private void createFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = GetForm("CreateFolder");
            if (form != null)
            {
                form.Show();
                form.BringToFront();
            }
            else
            {
                CreateFolder frm = new CreateFolder(GetPath);
                frm.ShowDialog();

                LoadFoldersInTreeView(ref treeSystm);
                string path = Path.GetFileName(frm.path);
                TreeNode itemNode = null;
                foreach (TreeNode node in treeSystm.Nodes)
                {
                    itemNode = FromID(path, node);
                    if (itemNode != null) break;
                }
                treeSystm.SelectedNode = itemNode != null ? itemNode : treeSystm.Nodes[0];
                treeSystm_NodeMouseClick(null, null);
            }
        }


        public TreeNode FromID(string itemId, TreeNode rootNode)
        {
            foreach (TreeNode node in rootNode.Nodes)
            {
                if (node.Text.Equals(itemId)) return node;
                TreeNode next = FromID(itemId, node);
                if (next != null) return next;
            }
            return null;
        }


        public static Form GetForm(string formName)
        {
            Form form = default(Form);
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name.Equals(formName, StringComparison.OrdinalIgnoreCase))
                {
                    form = f;
                    break;
                }
            }
            return form;
        }

        private void HighLightProgramColor(string Program)
        {
            foreach (DataGridViewRow Myrow in dgvProgramDetails.Rows)
            {
                if (Myrow.Cells[1].Value.ToString().TrimStart(new char[] { 'O' }) == Program)
                {
                    Myrow.DefaultCellStyle.BackColor = Color.GreenYellow;
                    break;
                }
            }
        }


        private void set_default_color()
        {
            foreach (TreeNode node in treeSystm.Nodes)
            {
                set_default_color2(node);

            }
        }


        private void set_default_color(TreeView treeView)
        {
            foreach (TreeNode node in treeView.Nodes)
            {
                set_default_color2(node);

            }
        }

        private void set_default_color2(TreeNode node)
        {
            node.ForeColor = Color.Black;
            node.BackColor = Color.White;
            foreach (TreeNode subnode in node.Nodes)
            {
                set_default_color2(subnode);

            }
        }


        private void treeViewCNCFolder_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode node = e != null ? e.Node : treeViewCNCFolder.SelectedNode;
            if (node == null) return;
            if (string.IsNullOrEmpty(node.Text.Trim())) return;
            if (!ping_success())
            {
                return;
            }
            set_default_color(treeViewCNCFolder);

            node.BackColor = Color.LightGreen;
            if (node.Tag != null)
            {
                _folderPath = node.Tag.ToString();
            }

            if (treeViewCNCFolder.SelectedNode != null && !string.IsNullOrEmpty(treeViewCNCFolder.SelectedNode.FullPath.ToString()))
            {
                groupBox4.Text = "CNC Machine (" + treeViewCNCFolder.SelectedNode.FullPath.ToString() + ")";
            }
            try
            {
                this.Cursor = Cursors.WaitCursor;
                BindProgramListGridview();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                FocasLibrary.Logger.WriteErrorLog("Error..!! ProgramTransfer : \n " + ex.ToString());
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }


        private void treeSystm_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Select the clicked node 
                treeSystm.SelectedNode = treeSystm.GetNodeAt(e.X, e.Y);
                GetPath = treeSystm.SelectedNode.Tag.ToString();
                if (treeSystm.SelectedNode != null)
                {
                    contextMenuStrip1.Show(treeSystm, e.Location);
                }
            }
        }
    }

    public class listBoxItem
    {
        public string text { get; set; }
        public string value { get; set; }

        public override string ToString()
        {
            return text;
        }
    }
}