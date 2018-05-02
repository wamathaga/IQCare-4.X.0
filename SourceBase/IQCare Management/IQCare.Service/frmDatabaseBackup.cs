using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.ServiceProcess;
using System.ServiceProcess.Design;
using System.Diagnostics;
using System.Configuration;
using System.Collections.Specialized;
using System.Xml;
using System.Data.Common;
using Application.Presentation;
using Interface.Service;
using Application.Common;
using Interface.Security;

//using BusinessLayer;
namespace IQCare.Service
{
    public partial class frmDatabaseBackup : Form
    {
        Int32 LocationId = 0;

        public frmDatabaseBackup()
        {
            InitializeComponent();
        }

        private void frmDatabaseBackup_Load(object sender, EventArgs e)
        {
            this.Top = 1;
            this.Left = 1;
            //set css begin
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
            //set css end
            if (GblIQCare.HasFunctionRight(ApplicationAccess.Backupsetup, FunctionAccess.Add, GblIQCare.dtUserRight) == false && GblIQCare.HasFunctionRight(ApplicationAccess.Backupsetup, FunctionAccess.Update, GblIQCare.dtUserRight) == false)
                btnBackup.Enabled = false;

            if (GblIQCare.HasFunctionRight(ApplicationAccess.Backuprestore, FunctionAccess.Add, GblIQCare.dtUserRight) == false && GblIQCare.HasFunctionRight(ApplicationAccess.Backuprestore, FunctionAccess.Update, GblIQCare.dtUserRight) == false)
                btnRestoreDatabase.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            txtPath.Text = fbd.SelectedPath.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                IIQCareSystem DBManager;
                DBManager = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
                if (chkDeidentified.Checked)
                    DBManager.DataBaseBackup(txtPath.Text, Convert.ToInt32(GblIQCare.AppLocationId), 1);
                else
                    DBManager.DataBaseBackup(txtPath.Text, Convert.ToInt32(GblIQCare.AppLocationId), 0);

                MessageBox.Show("IQCare database backup is been created successfully at " + txtPath.Text.ToString(), "IQCare", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                txtPath.Text = "";
                Cursor.Current = Cursors.Default;
            }
            catch (System.IO.DirectoryNotFoundException exd)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = exd.Message.ToString();
                IQCareWindowMsgBox.ShowWindow("#C1", theBuilder, this);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please make sure you have adequate permissions on the selected folder. Please avoid taking backup on 'C' drive.", "IQCare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Title = "Browse back-up Files";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.DefaultExt = "txt";
            openFileDialog1.Filter = "Back-up files (*.bak)|*.bak";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.ReadOnlyChecked = true;
            openFileDialog1.ShowReadOnly = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtRestorePath.Text = openFileDialog1.FileName;
            }

        }

        private void btnRestoreDatabase_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string filePath = txtRestorePath.Text;
            if (!string.IsNullOrEmpty(filePath))
            {
                if (System.IO.File.Exists(filePath))
                {
                    IIQCareSystem DataManager;
                    DataManager = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
                    DataSet theDS = DataManager.GetBackupSets(filePath);

                    IIQCareSystem DBManager;
                    DBManager = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
                    DBManager.RestoreDataBase(filePath, Convert.ToInt32(theDS.Tables[0].Rows[0]["Position"].ToString()));
                    IQCareWindowMsgBox.ShowWindow("DataRestore", this);
                    txtRestorePath.Text = string.Empty;
                    Cursor.Current = Cursors.Default;
                }
                else
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["MessageText"] = "Invalid file. Please select a valid path.";
                    IQCareWindowMsgBox.ShowWindow("#C1", theBuilder, this);
                }
            }
            else
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = "Select option from the list.";
                IQCareWindowMsgBox.ShowWindow("#C1", theBuilder, this);
            }
        }
    }
}