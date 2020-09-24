using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using MachineConnectApplication;

namespace FocasSmartDataCollection
{
    
    public partial class AppConfigModifier : Form
    {
        System.Configuration.Install.InstallContext formContext;

        public AppConfigModifier()
        {
            InitializeComponent();
            chkBox.Checked = false;           
        }

        public AppConfigModifier(System.Configuration.Install.InstallContext context)
        {
            formContext = context;
            InitializeComponent();
            chkBox.Checked = false;          
            this.CenterToParent();

        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            if (validateFormFeilds()) return;
            if (chkBox.Checked == false)
            {
                if (validateCredentials()) return;
            }
            modifyTheConnectionString();
           
            //this.Close();
        }

        private void modifyTheConnectionString()
        {
            Configuration c = ConfigurationManager.OpenExeConfiguration(System.Reflection.Assembly.GetExecutingAssembly().Location); 
            ConnectionStringsSection section = (ConnectionStringsSection)c.GetSection("connectionStrings");
            ConnectionStringSettingsCollection settings = ConfigurationManager.ConnectionStrings;

            if (settings != null)
            {
                try
                {
                    var connection = section.ConnectionStrings["TPMConnectionString"].ConnectionString;
                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connection.ToString());
                    builder.DataSource = cmbServers.Text.ToString();
                    builder.InitialCatalog = txtInitialCatalog.Text.ToString();
                    if (chkBox.Checked == false)
                    {
                        builder.UserID = txtUserName.Text.ToString();
                        builder.Password = txtPassWord.Text.ToString();
                        builder.IntegratedSecurity = false;
                        section.ConnectionStrings["TPMConnectionString"].ConnectionString = builder.ConnectionString.ToString();
                    }
                    else
                    {
                        //Console.WriteLine(builder.ConnectionString);
                        builder.Remove("User ID");
                        builder.Remove("Password");
                        builder.IntegratedSecurity = true;
                        section.ConnectionStrings["TPMConnectionString"].ConnectionString = builder.ConnectionString;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());                 
                }

                c.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("connectionStrings");
                this.Close();
            }           

        }

        private bool validateCredentials()
        {
            string msg = string.Empty;

            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                msg = "Please provide the Input for the UserName.";
                MessageBox.Show(msg, "Important Note", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                txtUserName.Focus();
                return true;
            }

            if (string.IsNullOrEmpty(txtPassWord.Text))
            {
                msg = "Please provide the Input for the Password.";
                MessageBox.Show(msg, "Important Note", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                txtPassWord.Focus();
                return true;
            }          
                   
            return false;
        }

        private bool validateFormFeilds()
        {
            string msg = string.Empty;

            if (string.IsNullOrEmpty(cmbServers.Text))
            {
                msg = "Please provide the Input for the Datasource.";
                MessageBox.Show(msg, "Important Note", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                cmbServers.Focus();
                return true;
            }

            if (string.IsNullOrEmpty(txtInitialCatalog.Text))
            {
                msg = "Please provide the Input for the Initial catalog.";
                MessageBox.Show(msg, "Important Note", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                txtInitialCatalog.Focus();
                return true;
            }
          
            return false;
        }

        private void chkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBox.Checked == true)
            {
                // hide data source n initial catalog in UI
                lblUserName.Enabled = false;
                txtUserName.Enabled = false;

                lblPassword.Enabled = false;
                txtPassWord.Enabled = false;
            }
            else
            {
                lblUserName.Enabled = true;
                txtUserName.Enabled = true;

                lblPassword.Enabled = true;
                txtPassWord.Enabled = true;
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (validateFormFeilds()) return;
            if (chkBox.Checked == false)
            {
                if (validateCredentials()) return;
            }
            Configuration c = ConfigurationManager.OpenExeConfiguration(System.Reflection.Assembly.GetExecutingAssembly().Location);//file path
            ConnectionStringsSection section = (ConnectionStringsSection)c.GetSection("connectionStrings");
            var connection = section.ConnectionStrings["TPMConnectionString"].ConnectionString;

            //Constructing the string with user defined values.
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connection.ToString());
            builder.DataSource = cmbServers.Text;
            builder.InitialCatalog = txtInitialCatalog.Text;
            if (chkBox.Checked == false)
            {
                builder.UserID = txtUserName.Text;
                builder.Password = txtPassWord.Text;
                builder.IntegratedSecurity = false;
                section.ConnectionStrings["TPMConnectionString"].ConnectionString = builder.ConnectionString;
            }
            else
            {
                builder.Remove("User ID");
                builder.Remove("Password");
                builder.IntegratedSecurity = true;
                section.ConnectionStrings["TPMConnectionString"].ConnectionString = builder.ConnectionString;
            }
            c.Save();

            SqlConnection conn = new SqlConnection(builder.ConnectionString);
            try
            {
                this.Cursor = Cursors.WaitCursor;
                conn.Open();
                MessageBox.Show("Connection Successfull");
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Connection failed..!!!\n" + ex.Message);
            }
            this.Cursor = Cursors.Default;
        }

        private void ConnectionString_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                Configuration c = ConfigurationManager.OpenExeConfiguration(System.Reflection.Assembly.GetExecutingAssembly().Location);
                ConnectionStringsSection section = (ConnectionStringsSection)c.GetSection("connectionStrings");
                var sqlConn = section.ConnectionStrings["TPMConnectionString"].ConnectionString;
                DisplaySQLServerInstances();
                //SqlConnection sqlConn = ConnectionManager.GetConnection();
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(sqlConn.ToString());
                cmbServers.Text = (builder.DataSource).ToString();
                txtInitialCatalog.Text = (builder.InitialCatalog).ToString();
                txtUserName.Text = (builder.UserID).ToString();
                txtPassWord.Text = (builder.Password).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Cursor = Cursors.Default;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DisplaySQLServerInstances()
        {
            List<string> serversList = new List<string>();
            try
            {
                System.Data.Sql.SqlDataSourceEnumerator instance = System.Data.Sql.SqlDataSourceEnumerator.Instance;
                System.Data.DataTable servers = instance.GetDataSources();

                for (int i = 0; i < servers.Rows.Count; i++)
                {
                    if ((servers.Rows[i]["InstanceName"] as string) != null)
                        serversList.Add(servers.Rows[i]["ServerName"].ToString() + "\\" + servers.Rows[i]["InstanceName"].ToString());
                    else
                        serversList.Add(servers.Rows[i]["ServerName"].ToString());
                }
            }
            catch (Exception exx)
            {
                MessageBox.Show(exx.Message);
            }

            cmbServers.DataSource = serversList;


        }

    }
}
