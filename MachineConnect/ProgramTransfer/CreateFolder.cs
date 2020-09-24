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
using DTO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using FocasGUI;
using System.Reflection;

namespace CNC_PT
{
    public partial class CreateFolder : Form
    {
        public static bool EnableQualityInspection = false;
        public string path = string.Empty;
        private string GetPath;
        public CreateFolder()
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
            

         //   this.BackColor = ColorTranslator.FromHtml(Settings.FORM_BACKCOLOR); 
            
        }

        public CreateFolder(string GetPath)
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


            //this.BackColor = ColorTranslator.FromHtml(Settings.FORM_BACKCOLOR);
       //     tableLayoutPanel1.BackColor = ColorTranslator.FromHtml(Settings.FORM_BACKCOLOR); 
            // TODO: Complete member initialization
            this.GetPath = GetPath;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtFolderName.Text.ToString()))
                {
                    MessageBox.Show("Folder name cannot be empty!!", "Error Message");
                    return;
                }
                path = Path.Combine(GetPath, txtFolderName.Text.ToString());
                if (Directory.Exists(path))
                {
                    MessageBox.Show("That path exists already.");
                   
                    return;
                }

                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(path);
                MessageBox.Show("Folder Created Successfully","Information Message");
                this.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
              
            }

        }

        private void CreateFolder_Shown(object sender, EventArgs e)
        {
            txtFolderName.Focus();
        }       
       
    }
}
