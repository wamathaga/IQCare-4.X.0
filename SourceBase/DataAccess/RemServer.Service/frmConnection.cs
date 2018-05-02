using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DataAccess.Entity;
using DataAccess.Common;
using Application.Common;
using System.IO;
using System.Xml;

namespace RemServer
{
    public partial class frmConnection : Form
    {
        public frmConnection()
        {
            InitializeComponent();
        }

        private void frmConnection_Load(object sender, EventArgs e)
        {
            init_fields();
        }

        #region "User Functions"
        private void init_fields()
        {
            txtServernm.Text = "";
            txtDBnm.Text = "";
            txtUsernm.Text = "";
            txtPassword.Text = "";
            txtconfirmpass.Text = "";
            txtServernm.Focus();
        }
        private int CheckData()
        {
            if (txtServernm.Text.Trim() == "")
            {
                MessageBox.Show("Enter a Valid Server Name.");
                txtServernm.Focus();
                return 0;
            }
            else if (txtDBnm.Text.Trim() == "")
            {
                MessageBox.Show("Enter a Valid DataBase Name.");
                txtDBnm.Focus();
                return 0;
            }
            else if (txtUsernm.Text.Trim() == "")
            {
                MessageBox.Show("Enter a Valid User Name.");
                txtUsernm.Focus();
                return 0;
            }
            else if (txtPassword.Text.Trim() == "")
            {
                MessageBox.Show("Enter a Valid Password.");
                txtPassword.Focus();
                return 0;
            }
            else if (txtconfirmpass.Text.Trim() != txtPassword.Text.Trim())
            {
                MessageBox.Show("Invalid Password. Reenter..");
                txtPassword.Text = "";
                txtconfirmpass.Text = "";
                txtPassword.Focus();
                return 0;
            }
            else
            {
                return 1;
            }
        }
        #endregion

        private void txtServernm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (txtServernm.Text.Trim() == "")
                {
                    MessageBox.Show("Enter a Valid Server Name.");
                    txtServernm.Focus();
                }
            }
        }

        private void txtDBnm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (txtDBnm.Text.Trim() == "")
                {
                    MessageBox.Show("Enter a Valid DataBase Name.");
                    txtDBnm.Focus();
                }
            }
        }

        private void txtUsernm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (txtUsernm.Text.Trim() == "")
                {
                    MessageBox.Show("Enter a Valid User Name.");
                    txtUsernm.Focus();

                }
            }
        }

        private void txtPassword_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (txtPassword.Text.Trim() == "")
                {
                    MessageBox.Show("Enter a Valid Password.");
                    txtPassword.Focus();
                }
            }
        }

        private void txtconfirmpass_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (txtPassword.Text.Trim() != txtconfirmpass.Text.Trim())
                {
                    MessageBox.Show("Invalid Password. Reenter..");
                    txtPassword.Text = "";
                    txtconfirmpass.Text = "";
                    txtPassword.Focus();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckData() == 1)
            {

                #region "TempConfigSettings"

                Utility objUtil = new Utility();
                //string theConString = objUtil.Encrypt(string.Format("data source = {0};uid = {1};pwd = {2};initial catalog = {3}", txtServernm.Text.Trim(), txtUsernm.Text.Trim(), txtPassword.Text.Trim(), txtDBnm.Text.Trim()));
                string theConString = objUtil.Encrypt(string.Format("Data Source={0};User ID={1};pwd={2};Initial Catalog={3}", txtServernm.Text.Trim(), txtUsernm.Text.Trim(), txtPassword.Text.Trim(), txtDBnm.Text.Trim()));
                ConfigurationSettings.AppSettings.Set("ConnectionString", theConString);
                string filepath = System.Windows.Forms.Application.StartupPath + @"\RemServer.exe.config";

                //////
                XmlDocument doc = new XmlDocument();
                doc.Load(filepath);
                XmlNode Node = doc.DocumentElement.ChildNodes.Item(1);
                Node = Node.ChildNodes.Item(0);
                Node.Attributes["value"].Value = theConString;
                doc.Save(filepath);

                #endregion

                ClsObject obj = new ClsObject();
                Hashtable htParams = new Hashtable();
                htParams.Clear();
                DataTable theDT = (DataTable)obj.ReturnObject(htParams, "select * from Sysobjects", ClsDBUtility.ObjectEnum.DataTable);
                if (theDT.Rows.Count > 0)
                {
                    MessageBox.Show("Connection Established.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Connection Failed. Try Again..");
                    ConfigurationSettings.AppSettings.Set("ConnectionString", "");
                    this.Close();
                }
            }
        }
        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}