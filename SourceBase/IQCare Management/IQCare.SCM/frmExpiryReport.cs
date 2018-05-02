using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Application.Common;
using Application.Presentation;
using Interface.SCM;

namespace IQCare.SCM
{
    public partial class frmExpiryReport : Form
    {
        DataSet theDs = new DataSet();
        public frmExpiryReport()
        {
            InitializeComponent();
        }

        private void frmExpiryReport_Load(object sender, EventArgs e)
        {
            this.dtpReportDate.Enabled = true;
            dtpReportDate.CustomFormat = "dd-MMM-yyyy";
            this.dtpReportDate.Text = GblIQCare.CurrentDate;
            this.dtpReportDate.Enabled = false;
            
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
            Init_Form();
            setExpiryDate();
        }
        private void Init_Form()
        {
            IMasterList objItemCommonlist = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");

            theDs = objItemCommonlist.GetStoreDetail();
            if (theDs.Tables[0].Rows.Count > 0)
            {
                BindFunctions theBindManager = new BindFunctions();
                theBindManager.Win_BindCombo(cmbStore, theDs.Tables[0], "Name", "Id");   
            }    
        }

      private void setExpiryDate()
          {
              //cmbdays.SelectedText = "select";
              cmbdays.Items.Add("Select");
              cmbdays.Items.Add(30);
              cmbdays.Items.Add(60);
              cmbdays.Items.Add(90);
              cmbdays.SelectedIndex = 0;
          }

      private void ShowGrid(DataTable theDT)
      {
          try
          {
              dgwExperyReport.Columns.Clear();
              dgwExperyReport.DataSource = null;
              DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
              col1.HeaderText = "Item Code";
              col1.DataPropertyName = "ItemCode";
              col1.Width = 120;
              col1.ReadOnly = true;
              col1.Visible = true;

              dgwExperyReport.DataSource = null;
              DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
              col2.HeaderText = "Item Description";
              col2.DataPropertyName = "ItemDescription";
              col2.Width = 200;
              col2.ReadOnly = true;
              col2.Visible = true;

              dgwExperyReport.DataSource = null;
              DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
              col3.HeaderText = "Batch No";
              col3.DataPropertyName = "BatchNo";
              col3.Width = 130;
              col3.ReadOnly = true;
              col3.Visible = true;

              dgwExperyReport.DataSource = null;
              DataGridViewTextBoxColumn col4 = new DataGridViewTextBoxColumn();
              col4.HeaderText = "Quantity";
              col4.DataPropertyName = "Quantityno";
              col4.Width = 120;
              col4.ReadOnly = true;
              col4.Visible = true;

              dgwExperyReport.DataSource = null;
              DataGridViewTextBoxColumn col5 = new DataGridViewTextBoxColumn();
              col5.HeaderText = "Dispensing Unit";
              col5.DataPropertyName = "DispensingUnit";
              col5.Width = 120;
              col5.ReadOnly = true;
              col5.Visible = true;

              dgwExperyReport.DataSource = null;
              DataGridViewTextBoxColumn col6 = new DataGridViewTextBoxColumn();
              col6.HeaderText = "Expiry Date";
              col6.DataPropertyName = "ExpiryDate";
              col6.Width = 120;
              col6.ReadOnly = true;
              col6.Visible = true;

              dgwExperyReport.DataSource = null;
              DataGridViewTextBoxColumn col7 = new DataGridViewTextBoxColumn();
              col7.HeaderText = "Total Purchase Price";
              col7.DataPropertyName = "TotalPurchasePrice";
              col7.Width = 133;
              col7.ReadOnly = true;
              col7.Visible = true;

              dgwExperyReport.Columns.Add(col1);
              dgwExperyReport.Columns.Add(col2);
              dgwExperyReport.Columns.Add(col3);
              dgwExperyReport.Columns.Add(col4);
              dgwExperyReport.Columns.Add(col5);
              dgwExperyReport.Columns.Add(col6);
              dgwExperyReport.Columns.Add(col7);

              dgwExperyReport.AutoGenerateColumns = false;
              dgwExperyReport.DataSource = theDT;
          }
          catch (Exception err)
          {
              MsgBuilder theBuilder = new MsgBuilder();
              theBuilder.DataElements["MessageText"] = err.Message.ToString();
              IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
          }
      }

      private void btnSubmit_Click(object sender, EventArgs e)
      {

          if (Validation_Form() != true)
          {

              return;
          
          }
          //IMasterList objItemCommonlist = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
          ISCMReport objItemCommonlist = (ISCMReport)ObjectFactory.CreateInstance("BusinessProcess.SCM.BSCMReport,BusinessProcess.SCM");

          DataTable theDT = new DataTable();
          
          //System.DateTime answer = Convert.ToDateTime(dtpReportDate.Text);
          System.DateTime answer = Convert.ToDateTime(GblIQCare.CurrentDate);

          System.DateTime expdate = answer.AddDays(Convert.ToInt32(cmbdays.Text));

          theDT = objItemCommonlist.GetExperyReport(Convert.ToInt32(cmbStore.SelectedValue.ToString()), expdate, Convert.ToDateTime(GblIQCare.CurrentDate));


          IQCareUtils theUtils = new IQCareUtils();
          DataView theDV = new DataView(theDT);
          theDV.RowFilter = "Quantityno > 0";
          theDT = theUtils.CreateTableFromDataView(theDV);


          if (theDT.Rows.Count > 0)
          {
              ShowGrid(theDT);
          
          }
      }

      private void button4_Click(object sender, EventArgs e)
      {
          this.Close();

      }
      private Boolean Validation_Form()
      {
          if (cmbStore.SelectedValue.ToString() == "0")
          {
              //IQCareWindowMsgBox.ShowWindow("SupplierName", this);
              MsgBuilder theBuilder = new MsgBuilder();
              IQCareWindowMsgBox.ShowWindow("StoreName", theBuilder, this);
              cmbStore.Focus();
              return false;
          }

          if (cmbdays.SelectedIndex.ToString() == "0")
          {
              //IQCareWindowMsgBox.ShowWindow("SupplierName", this);
              MsgBuilder theBuilder = new MsgBuilder();
              IQCareWindowMsgBox.ShowWindow("ExpiryDay", theBuilder, this);
              cmbdays.Focus();
              return false;
          }

          return true;
      
      }

      private void btnExport_Click(object sender, EventArgs e)
      {
          if (dgwExperyReport.RowCount > 0)
          {

              IQCareUtils theUtil = new IQCareUtils();
              DataTable theDT = (DataTable)dgwExperyReport.DataSource;
              string theFilePath = System.Configuration.ConfigurationSettings.AppSettings["ExcelFilesPath"];
            //  string theFilePath = System.IO.Directory.GetParent(System.Windows.Forms.Application.ExecutablePath).Parent.Parent.Parent.FullName + "\\IQCare Management\\ExcelFiles\\";
              theFilePath = theFilePath + "ExpiryReport.xls";
              theUtil.ExportToExcel_Windows(theDT, theFilePath, "");
              btnExport.Visible = false;
          }
          else
          {
              IQCareWindowMsgBox.ShowWindow("GridData", this);
              return;
          }
      }

      private void label5_Click(object sender, EventArgs e)
      {

      }

    }
    
}
