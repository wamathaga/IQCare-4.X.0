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
using Interface.Clinical;
using Interface.SCM;
using Interface.FormBuilder;
using System.Diagnostics;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Collections;
using SHDocVw;

namespace IQCare.SCM
{
    public partial class frmPatientDrugDispense : Form
    {
        #region "Variables"
        DataSet XMLDS = new DataSet(); /// for All Masters ////
        DataSet XMLPharDS = new DataSet(); ///for Pharmacy Masters///                                       
        DataSet thePharmacyMaster = new DataSet();
        DataTable theExistingDrugs = new DataTable();
        Int32 IntProcess = 0;
        Int32 thePatientId = 0;
        Int32 theOrderId = 0;
        Int32 theReturnOrderId = 0;
        DateTime theDOB;
        /// <summary>
        /// Dispensing Variables
        /// </summary>
        Int32 theFunded = 0;
        Int32 theItemId = 0;
        Int32 theDispensingUnit = 0;
        Int32 theBatchId = 0;
        Int32 theAvailQty = 0;
        Int32 theStrength = 0;
        Int32 theItemType = 0;
        Int32 theProphylaxis = 0;
        decimal theCostPrice = 0;
        decimal theMargin = 0;
        decimal theBillAmt = 0;
        decimal theSellingPrice = 0;
        decimal theConfigSellingPrice = 0;
        string theDispensingUnitName = "";
        string theGenericAbb = "";
        String theOrderStatus = "";
        decimal thePrecribeAmt = 0;

        //john KNH
        double qtyAvailableInBatch = 0;
        string makeGridEditable = "";
        string lastdispensedARV = "";
        string ARVBeingDispensed = "";
        string ItemInstructions = "";
        static int lastRegimenLine = 0;
        int patientHeight;
        int HeightWeightPopUp = 0;
        //

        ////////////
        /// <summary>
        /// GridControls
        /// </summary>
        TextBox theReturnQty = new TextBox();
        Int32 theCurrentRow = 0;
        Int32 theDispCurrentRow = -1;
        ////////////////////////// 
        ImageList imageList1 = new ImageList();
        int theprevbatchId = 0;
        #endregion

        public frmPatientDrugDispense()
        {
            InitializeComponent();
            grdDrugDispense.CellPainting += new DataGridViewCellPaintingEventHandler(grdDrugDispense_CellPainting);
        }
        private void Init_Form()
        {
            imageList1.Images.Add("pic1", Image.FromFile(GblIQCare.GetPath() + "\\printer.jpg"));
            // NxtAppDate.CustomFormat = " "; 

            btnART.Enabled = false;
            grpBoxLastDispense.Visible = false;
            BindCombo();
            txtPatientIdentification.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            dtpDOB.CustomFormat = " ";
            cmbFacility.SelectedValue = GblIQCare.AppLocationId.ToString();
            dtDispensedDate.Text = GblIQCare.CurrentDate;
            dtpReturnDate.Text = GblIQCare.CurrentDate;
            cmbFacility.Enabled = false;
            lstSearch.Visible = false;
            cmdSave.Visible = false;
            cmdPrintPrescription.Visible = false;
            btnPrintLabel.Visible = false;
            grpExistingRec.Visible = false;
            //john
            dtRefillApp.CustomFormat = " ";
            makeGridEditable = "Yes";
            //john end
            //chkPrintPrescription.Checked = false;
            //txtPatientInstructions.Enabled = false;
            Authentication();
        }
        private void Authentication()
        {
            if (GblIQCare.HasFunctionRight(ApplicationAccess.DrugDispense, FunctionAccess.Add, GblIQCare.dtUserRight) == false)
            {
                btnSave.Enabled = false;
            }
            if (GblIQCare.HasFunctionRight(ApplicationAccess.DrugDispense, FunctionAccess.Update, GblIQCare.dtUserRight) == false)
            {
                btnSave.Enabled = false;
            }
        }

        private void BindSearchGrid(DataTable theDT)
        {
            grdResultView.DataSource = null;
            grdResultView.AutoGenerateColumns = false;
            grdResultView.Columns.Clear();

            DataGridViewTextBoxColumn theCol1 = new DataGridViewTextBoxColumn();
            theCol1.HeaderText = "Last Name";
            theCol1.DataPropertyName = "lastname";
            theCol1.Width = 200;

            DataGridViewTextBoxColumn theCol2 = new DataGridViewTextBoxColumn();
            theCol2.HeaderText = "First Name";
            theCol2.DataPropertyName = "firstname";
            theCol2.Width = 200;

            DataGridViewTextBoxColumn theCol3 = new DataGridViewTextBoxColumn();
            theCol3.HeaderText = "IQ-Number";
            theCol3.DataPropertyName = "PatientId";
            theCol3.Width = 150;

            DataGridViewTextBoxColumn theCol4 = new DataGridViewTextBoxColumn();
            theCol4.HeaderText = "Sex";
            theCol4.DataPropertyName = "Name";
            theCol4.Width = 50;

            DataGridViewTextBoxColumn theCol5 = new DataGridViewTextBoxColumn();
            theCol5.HeaderText = "Date of Birth";
            theCol5.DataPropertyName = "dob";
            theCol5.Width = 120;

            DataGridViewTextBoxColumn theCol6 = new DataGridViewTextBoxColumn();
            theCol6.HeaderText = "Status";
            theCol6.DataPropertyName = "status";
            theCol6.Width = 60;

            DataGridViewTextBoxColumn theCol7 = new DataGridViewTextBoxColumn();
            theCol7.HeaderText = "Patient Location";
            theCol7.DataPropertyName = "FacilityName";
            theCol7.Width = 130;

            DataGridViewTextBoxColumn theCol8 = new DataGridViewTextBoxColumn();
            theCol8.HeaderText = "Ptn_Pk";
            theCol8.DataPropertyName = "Ptn_Pk";
            theCol8.Width = 10;
            theCol8.Visible = false;

            grdResultView.Columns.Add(theCol1);
            grdResultView.Columns.Add(theCol2);
            grdResultView.Columns.Add(theCol3);
            grdResultView.Columns.Add(theCol4);
            grdResultView.Columns.Add(theCol5);
            grdResultView.Columns.Add(theCol6);
            grdResultView.Columns.Add(theCol7);
            grdResultView.Columns.Add(theCol8);
            //grdResultView.Columns.Add(theCol9);
            //grdResultView.Columns.Add(theCol10);

            grdResultView.DataSource = theDT;
        }

        private void BindCombo()
        {
            XMLDS.ReadXml(GblIQCare.GetXMLPath() + "\\AllMasters.con");
            XMLPharDS.ReadXml(GblIQCare.GetXMLPath() + "\\DrugMasters.con");
            BindFunctions theBindManager = new BindFunctions();

            DataView theDV = new DataView(XMLDS.Tables["mst_Facility"]);
            theDV.RowFilter = "(DeleteFlag =0 or DeleteFlag is null)";
            DataTable theFacilityDT = theDV.ToTable();
            theBindManager.Win_BindCombo(cmbFacility, theFacilityDT, "FacilityName", "FacilityId");

            theDV = new DataView(XMLDS.Tables["mst_Decode"]);
            theDV.RowFilter = "CodeId = 4 and (DeleteFlag =0 or DeleteFlag is null)";
            DataTable theGender = theDV.ToTable();
            theBindManager.Win_BindCombo(cmbSex, theGender, "Name", "Id");

            theDV.RowFilter = "CodeId = 33 and (DeleteFlag =0 or DeleteFlag is null)";
            DataTable thePharProg = theDV.ToTable();
            theBindManager.Win_BindCombo(cmbprogram, thePharProg, "Name", "Id");

            //theDV = new DataView(XMLPharDS.Tables["mst_Frequency"]);
            //theDV.RowFilter = "(DeleteFlag =0 or DeleteFlag is null)";
            //DataTable theDT = theDV.ToTable();
            //theBindManager.Win_BindCombo(cmbFrequency, theDT, "Name", "Id");


            theDV = new DataView(XMLDS.Tables["mst_Provider"]);
            theDV.RowFilter = "(DeleteFlag =0 or DeleteFlag is null)";
            DataTable theDTProvider = theDV.ToTable();
            theBindManager.Win_BindCombo(cmbProvider, theDTProvider, "Name", "Id");

            theDV = new DataView(XMLDS.Tables["mst_RegimenLine"]);
            theDV.RowFilter = "(DeleteFlag =0 or DeleteFlag is null)";
            DataTable theDTRegimenLine = theDV.ToTable();
            theBindManager.Win_BindCombo(cmbRegimenLine, theDTRegimenLine, "Name", "Id");

            theDV = new DataView(XMLDS.Tables["mst_Decode"]);
            theDV.RowFilter = "CodeId = 26 and (DeleteFlag =0 or DeleteFlag is null)";
            DataTable theDTReason = theDV.ToTable();
            theBindManager.Win_BindCombo(cmbReason, theDTReason, "Name", "Id");

            theDV = new DataView(XMLDS.Tables["mst_Decode"]);
            theDV.RowFilter = "CodeId = 31 and (DeleteFlag =0 or DeleteFlag is null) and id in(140,141,142)";
            DataTable thePeriodTaken = theDV.ToTable();
            theBindManager.Win_BindCombo(cmdPeriodTaken, thePeriodTaken, "Name", "Id");

            IViewAssociation objViewAssociation = (IViewAssociation)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BViewAssociation,BusinessProcess.FormBuilder");
            DataSet objDsViewAssociation = objViewAssociation.GetMoudleName();
            DataTable dt;
            dt = objDsViewAssociation.Tables[0];
            DataRow drAddSelect;
            drAddSelect = dt.NewRow();
            drAddSelect["ModuleName"] = "All";
            drAddSelect["ModuleID"] = 0;
            dt.Rows.InsertAt(drAddSelect, 0);
            BindFunctions theBind = new BindFunctions();
            theBind.Win_BindCombo(cmbService, dt, "ModuleName", "ModuleId");

        }

        private void frmPatientDrugDispense_Load(object sender, EventArgs e)
        {
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
            Init_Form();
            tabDispense.SelectedTab = tabDispense.TabPages[0];
            txtPatientIdentification.Select();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            btnART.Enabled = false;
            grpBoxLastDispense.Visible = false;
            //      clearPopup();
            string theSex = "";
            string theDOB = "";
            if (Convert.ToInt32(cmbSex.SelectedValue) > 0)
                theSex = cmbSex.SelectedValue.ToString();
            if (dtpDOB.CustomFormat == " ")
                theDOB = "01-01-1900";
            else
                theDOB = dtpDOB.Text;

            IPatientRegistration PatientManager = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
            DataSet dsPatient = PatientManager.GetPatientSearchResults(Convert.ToInt32(cmbFacility.SelectedValue), txtLastName.Text, "", txtFirstName.Text, "0", txtPatientIdentification.Text,
                theSex, Convert.ToDateTime(theDOB).ToString("dd-MMM-yyyy"), "0", Convert.ToInt32(cmbService.SelectedValue));
            BindSearchGrid(dsPatient.Tables[0]);
        }

        private void dtpDOB_Enter(object sender, EventArgs e)
        {
            dtpDOB.CustomFormat = "dd-MMM-yyyy";
        }

        private void tabDispense_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnART.Enabled = false;
            grpBoxLastDispense.Visible = false;
            if ((tabDispense.TabPages[1].Focus() == true || tabDispense.TabPages[2].Focus() == true) && thePatientId < 1)
            {
                IQCareWindowMsgBox.ShowWindow("PatientNotSelected", this);
                tabDispense.SelectedTab = tabDispense.TabPages[0];
                return;
            }
            if (tabDispense.TabPages[0].Focus() == true)
            {
                thePatientId = 0;
                this.Text = "Find Patient";
                theOrderId = 0;
                theOrderStatus = "";
            }
            if (tabDispense.TabPages[1].Focus() == true)
            {
                DataView theDV = new DataView(XMLDS.Tables["Mst_Store"]);
                theDV.RowFilter = "Id=" + GblIQCare.intStoreId.ToString();
                lblStoreName.Text = theDV[0]["Name"].ToString();
                cmdSave.Visible = true;
                cmdPrintPrescription.Visible = true;
                btnPrintLabel.Visible = true;
                theItemId = 0;
                theDispensingUnit = 0;
                theBatchId = 0;
                theCostPrice = 0;
                theMargin = 0;
                theBillAmt = 0;
                theDispensingUnitName = "";
                txtItemName.Text = "";
                txtBatchNo.Text = "";
                //cmbFrequency.SelectedValue = "0";
                //txtQtyDispensed.Text = "";
                txtExpirydate.Text = "";
                //txtSellingPrice.Text = "";
                //txtDose.Text = "";
                //txtDuration.Text = "";
                this.Text = "Dispense Drugs";
                if (theOrderId > 0 && (theOrderStatus == "Already Dispensed Order" || theOrderStatus == "Partial Dispense"))
                {
                    makeGridEditable = "No";
                    cmdSave.Enabled = false;
                }
                else
                {
                    cmdSave.Enabled = true;
                    makeGridEditable = "Yes";
                }
                //cmdSave.Enabled = true;

            }
            if (tabDispense.TabPages[2].Focus() == true)
            {
                DataView theDV = new DataView(XMLDS.Tables["Mst_Store"]);
                theDV.RowFilter = "Id=" + GblIQCare.intStoreId.ToString();
                lblReturnStoreName.Text = theDV[0]["Name"].ToString();
                cmdSave.Visible = true;
                cmdPrintPrescription.Visible = false;
                this.Text = "Return Drugs";
                lblReturnDispensedDate.Text = "";
                lblReturnProgram.Text = "";
                IDrug thePharmacyManager = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
                DataTable theDT = thePharmacyManager.GetPharmacyExistingRecord(thePatientId, GblIQCare.intStoreId);
                BindDrugReturnGrid(theDT);
                grdReturnDetail.DataSource = false;
                grdReturnDetail.Columns.Clear();
                cmdSave.Enabled = true;
            }
        }

        private void grdResultView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            clearPopup();

            thePatientId = Convert.ToInt32(grdResultView.Rows[grdResultView.CurrentRow.Index].Cells[7].Value);
            theDOB = Convert.ToDateTime(grdResultView.Rows[grdResultView.CurrentRow.Index].Cells[4].Value);
            tabDispense.SelectedTab = tabDispense.TabPages[1];
            lblPatientName.Text = grdResultView.Rows[grdResultView.CurrentRow.Index].Cells[0].Value.ToString() + ", " +
                grdResultView.Rows[grdResultView.CurrentRow.Index].Cells[1].Value.ToString();
            lblReturnPatName.Text = grdResultView.Rows[grdResultView.CurrentRow.Index].Cells[0].Value.ToString() + ", " +
                grdResultView.Rows[grdResultView.CurrentRow.Index].Cells[1].Value.ToString();
            lblIQNumber.BackColor = this.BackColor;
            lblIQNumber.Text = grdResultView.Rows[grdResultView.CurrentRow.Index].Cells[2].Value.ToString();
            lblReturnIQNumber.Text = grdResultView.Rows[grdResultView.CurrentRow.Index].Cells[2].Value.ToString();
            IDrug theDrugManager = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
            thePharmacyMaster = theDrugManager.GetPharmacyDispenseMasters(thePatientId, GblIQCare.intStoreId);
            if (thePharmacyMaster.Tables[0].Rows.Count > 0)
            {
                if (thePharmacyMaster.Tables[0].Rows[0].IsNull("LastDispense"))
                {
                    lblDispDate.Text = "";
                    lblReturnLstDispDate.Text = "";
                    lbllastDisdate.Text = "";
                    
                }
                else
                {
                    lblDispDate.Text = ((DateTime)thePharmacyMaster.Tables[0].Rows[0]["LastDispense"]).ToString(GblIQCare.AppDateFormat.ToString());
                    lblReturnLstDispDate.Text = ((DateTime)thePharmacyMaster.Tables[0].Rows[0]["LastDispense"]).ToString(GblIQCare.AppDateFormat.ToString());
                    lbllastDisdate.Text = ((DateTime)thePharmacyMaster.Tables[0].Rows[0]["LastDispense"]).ToString(GblIQCare.AppDateFormat.ToString());
                }
                if (thePharmacyMaster.Tables[0].Rows[0].IsNull("LastRegimen"))
                {
                    lblLstRegimen.Text = "";
                    lblReturnLstRegimen.Text = "";
                    lblDispenseLstRegimen.Text = "";
                    //john 21st Oct 2013
                    lastdispensedARV = "";
                    //
                }
                else
                {
                    lblLstRegimen.Text = removeRegimenDuplicates(thePharmacyMaster.Tables[0].Rows[0]["LastRegimen"].ToString());
                    lblReturnLstRegimen.Text = thePharmacyMaster.Tables[0].Rows[0]["LastRegimen"].ToString();
                    lblDispenseLstRegimen.Text = removeRegimenDuplicates(thePharmacyMaster.Tables[0].Rows[0]["LastRegimen"].ToString());
                    //john 21st Oct 2013
                    lastdispensedARV = removeRegimenDuplicates(thePharmacyMaster.Tables[0].Rows[0]["LastRegimen"].ToString());
                    //
                }

                if (thePharmacyMaster.Tables[0].Rows[0].IsNull("ProgID"))
                {
                    cmbprogram.SelectedValue = 0;
                }
                else
                {
                    cmbprogram.SelectedValue = thePharmacyMaster.Tables[0].Rows[0]["ProgID"].ToString();
                }

                if (thePharmacyMaster.Tables[0].Rows[0].IsNull("RegimenLine"))
                {
                    cmbRegimenLine.SelectedValue = 0;
                    lastRegimenLine = 0;

                }
                else
                {
                    cmbRegimenLine.SelectedValue = thePharmacyMaster.Tables[0].Rows[0]["RegimenLine"].ToString();
                    lastRegimenLine = int.Parse(thePharmacyMaster.Tables[0].Rows[0]["RegimenLine"].ToString());
                }

                if (thePharmacyMaster.Tables[0].Rows[0].IsNull("ProviderID"))
                {
                    cmbProvider.SelectedValue = 0;
                }
                else
                {
                    cmbProvider.SelectedValue = thePharmacyMaster.Tables[0].Rows[0]["ProviderID"].ToString();
                }



            }
            else
            {
                lblDispDate.Text = "";
                lblLstRegimen.Text = "";
                lblReturnLstRegimen.Text = "";
                lblDispenseLstRegimen.Text = "";
                lblReturnLstDispDate.Text = "";
                lbllastDisdate.Text = "";
                //john 21st Oct 2013
                lastdispensedARV = "";
                //
            }

            if (thePharmacyMaster.Tables[2].Rows.Count > 0)
            {
                if (thePharmacyMaster.Tables[2].Rows[0].IsNull("height"))
                {
                    txtHeight.Text = "";
                }
                else
                {
                    //patientHeight = int.Parse(thePharmacyMaster.Tables[2].Rows[0]["height"].ToString());
                    txtHeight.Text = thePharmacyMaster.Tables[2].Rows[0]["height"].ToString();
                }
            }

            DataTable theDT = CreatePharmacyDispenseTable();
            BindPharmacyDispenseGrid(theDT);
            txtItemName.Select();
        }

        private DataTable CreatePharmacyDispenseTable()
        {
            DataTable theDT = new DataTable();
            theDT.Columns.Add("ItemId", Type.GetType("System.Int32"));
            theDT.Columns.Add("ItemName", Type.GetType("System.String"));
            theDT.Columns.Add("DispensingUnitId", Type.GetType("System.Int32"));
            theDT.Columns.Add("DispensingUnitName", Type.GetType("System.String"));
            theDT.Columns.Add("BatchId", Type.GetType("System.Int32"));
            theDT.Columns.Add("BatchNo", Type.GetType("System.String"));
            theDT.Columns.Add("StrengthId", Type.GetType("System.Int32"));
            theDT.Columns.Add("FrequencyId", Type.GetType("System.Int32"));
            theDT.Columns.Add("FrequencyName", Type.GetType("System.String"));
            //theDT.Columns.Add("ExpiryDate", Type.GetType("System.DateTime"));
            theDT.Columns.Add("ExpiryDate", Type.GetType("System.String"));
            theDT.Columns.Add("QtyDisp", Type.GetType("System.Int32"));
            theDT.Columns.Add("CostPrice", Type.GetType("System.Decimal"));
            theDT.Columns.Add("Margin", Type.GetType("System.Decimal"));
            theDT.Columns.Add("SellingPrice", Type.GetType("System.Decimal"));
            theDT.Columns.Add("BillAmount", Type.GetType("System.Decimal"));
            theDT.Columns.Add("Prophylaxis", Type.GetType("System.Int32"));
            theDT.Columns.Add("ItemType", Type.GetType("System.Int32"));
            theDT.Columns.Add("GenericAbb", Type.GetType("System.String"));

            //todo
            theDT.Columns.Add("OrderedQuantity", Type.GetType("System.String"));
            theDT.Columns.Add("DataStatus", Type.GetType("System.String"));

            theDT.Columns.Add("Dose", Type.GetType("System.String"));
            theDT.Columns.Add("Duration", Type.GetType("System.String"));

            theDT.Columns.Add("PrintPrescriptionStatus", Type.GetType("System.Int32"));
            theDT.Columns.Add("PatientInstructions", Type.GetType("System.String"));

            //john 13th August 2013
            theDT.Columns.Add("UnitSellingPrice", Type.GetType("System.Int32"));
            theDT.Columns.Add("freqMultiplier", Type.GetType("System.Int32"));
            //
           

            DataColumn[] thePkey = new DataColumn[3];
            thePkey[0] = theDT.Columns["ItemId"];
            thePkey[1] = theDT.Columns["BatchId"];
            thePkey[2] = theDT.Columns["ExpiryDate"];
            theDT.PrimaryKey = thePkey;
            return theDT;
        }

        private void BindPharmacyDispenseGrid(DataTable theDT)
        {
            try
            {

                grdDrugDispense.DataSource = null;
                grdDrugDispense.Columns.Clear();
                grdDrugDispense.AutoGenerateColumns = false;


                DataGridViewTextBoxColumn theCol1 = new DataGridViewTextBoxColumn();
                theCol1.HeaderText = "ItemId";
                theCol1.DataPropertyName = "ItemId";
                theCol1.Width = 10;
                theCol1.Visible = false;
                theCol1.ReadOnly = true;

                DataGridViewTextBoxColumn theCol2 = new DataGridViewTextBoxColumn();
                theCol2.HeaderText = "Drug Name";
                theCol2.DataPropertyName = "ItemName";
                theCol2.Width = 285;
                theCol2.ReadOnly = true;

                DataGridViewTextBoxColumn theCol3 = new DataGridViewTextBoxColumn();
                theCol3.HeaderText = "DispUnitId";
                theCol3.DataPropertyName = "DispensingUnitId";
                theCol3.Width = 10;
                theCol3.Visible = false;
                theCol3.ReadOnly = true;

                DataGridViewTextBoxColumn theCol4 = new DataGridViewTextBoxColumn();
                theCol4.HeaderText = "Dispensing Unit";
                theCol4.DataPropertyName = "DispensingUnitName";
                theCol4.Width = 80;
                theCol4.ReadOnly = true;

                DataGridViewTextBoxColumn theCol5 = new DataGridViewTextBoxColumn();
                theCol5.HeaderText = "BatchId";
                theCol5.DataPropertyName = "BatchId";
                theCol5.Width = 10;
                theCol5.Visible = false;
                theCol5.ReadOnly = true;

                DataGridViewTextBoxColumn theCol6 = new DataGridViewTextBoxColumn();
                theCol6.HeaderText = "Batch No";
                theCol6.DataPropertyName = "BatchNo";
                theCol6.Name = "BatchNo";
                theCol6.Width = 80;
                theCol6.ReadOnly = true;

                DataGridViewTextBoxColumn theCol7 = new DataGridViewTextBoxColumn();
                theCol7.HeaderText = "Expiry Date";
                theCol7.DataPropertyName = "ExpiryDate";
                theCol7.Name = "ExpiryDate";
                theCol7.Width = 80;
                theCol7.ReadOnly = true;

                DataGridViewTextBoxColumn theCol8 = new DataGridViewTextBoxColumn();
                theCol8.HeaderText = "Quantity Dispensed";
                theCol8.DataPropertyName = "QtyDisp";
                theCol8.Name = "QtyDisp";
                theCol8.Width = 60;
                if (makeGridEditable == "Yes")
                {

                }
                else
                {
                    theCol8.ReadOnly = true;
                }
                //theCol8.ReadOnly = true;

                DataGridViewTextBoxColumn theCol9 = new DataGridViewTextBoxColumn();
                theCol9.HeaderText = "CostPrice";
                theCol9.DataPropertyName = "CostPrice";
                theCol9.Width = 10;
                theCol9.Visible = false;
                theCol9.ReadOnly = true;

                DataGridViewTextBoxColumn theCol10 = new DataGridViewTextBoxColumn();
                theCol10.HeaderText = "Margin";
                theCol10.DataPropertyName = "Margin";
                theCol10.Width = 10;
                theCol10.Visible = false;
                theCol10.ReadOnly = true;

                DataGridViewTextBoxColumn theCol11 = new DataGridViewTextBoxColumn();
                theCol11.HeaderText = "Selling Price";
                theCol11.DataPropertyName = "SellingPrice";
                theCol11.Name = "SellingPrice";
                theCol11.Width = 60;
                theCol11.ReadOnly = true;

                DataGridViewTextBoxColumn theCol12 = new DataGridViewTextBoxColumn();
                theCol12.HeaderText = "Bill Amount";
                theCol12.DataPropertyName = "BillAmount";
                theCol12.Name = "BillAmount";
                theCol12.Width = 60;
                theCol12.ReadOnly = true;

                DataGridViewTextBoxColumn theCol13 = new DataGridViewTextBoxColumn();
                theCol13.HeaderText = "StrengthId";
                theCol13.DataPropertyName = "StrengthId";
                theCol13.Width = 10;
                theCol13.Visible = false;
                theCol13.ReadOnly = true;

                DataGridViewTextBoxColumn theCol14 = new DataGridViewTextBoxColumn();
                theCol14.HeaderText = "Frequency";
                theCol14.DataPropertyName = "FrequencyId";
                theCol14.Name = "FrequencyId";
                theCol14.Width = 10;
                theCol14.Visible = false;
                theCol14.ReadOnly = true;

                DataGridViewTextBoxColumn theCol15 = new DataGridViewTextBoxColumn();
                theCol15.HeaderText = "Frequency";
                theCol15.DataPropertyName = "FrequencyName";
                theCol15.Width = 60;
                theCol15.ReadOnly = true;

                //todo

                DataGridViewTextBoxColumn theCol16 = new DataGridViewTextBoxColumn();
                theCol16.HeaderText = "Quantity Prescribed";
                theCol16.DataPropertyName = "OrderedQuantity";
                theCol16.Name = "QtyPres";
                theCol16.Width = 60;
                theCol16.ReadOnly = true;

                DataGridViewTextBoxColumn theCol17 = new DataGridViewTextBoxColumn();
                theCol17.HeaderText = "DataStatus";
                theCol17.DataPropertyName = "DataStatus";
                theCol17.Width = 10;
                theCol17.Visible = false;
                theCol17.ReadOnly = true;


                DataGridViewTextBoxColumn theCol18 = new DataGridViewTextBoxColumn();
                theCol18.HeaderText = "Dose";
                theCol18.DataPropertyName = "Dose";
                theCol18.Name = "Dose";
                theCol18.Width = 38;
                if (makeGridEditable == "Yes")
                {

                }
                else
                {
                    theCol18.ReadOnly = true;
                }
                //theCol18.ReadOnly = true;

                DataGridViewTextBoxColumn theCol19 = new DataGridViewTextBoxColumn();
                theCol19.HeaderText = "Duration";
                theCol19.DataPropertyName = "Duration";
                theCol19.Name = "Duration";
                theCol19.Width = 60;
                if (makeGridEditable == "Yes")
                {
                    
                }
                else
                {
                    theCol19.ReadOnly = true;
                }
                //theCol19.ReadOnly = true;

                DataGridViewCheckBoxColumn theCol20 = new DataGridViewCheckBoxColumn();
                theCol20.HeaderText = "Print";
                theCol20.DataPropertyName = "PrintPrescriptionStatus";
                theCol20.Width = 25;




                DataGridViewTextBoxColumn theCol21 = new DataGridViewTextBoxColumn();
                theCol21.HeaderText = "Patient Instructions";
                theCol21.DataPropertyName = "PatientInstructions";
                theCol21.Name = "PatientInstructions";
                theCol21.Width = 10;
                theCol21.Visible = false;


                //john
                DataGridViewTextBoxColumn theCol22 = new DataGridViewTextBoxColumn();
                theCol22.HeaderText = "Unit Selling Price";
                theCol22.DataPropertyName = "UnitSellingPrice";
                theCol22.Name = "UnitSellingPrice";
                theCol22.Width = 10;
                theCol22.Visible = false;

                DataGridViewTextBoxColumn theCol23 = new DataGridViewTextBoxColumn();
                theCol23.HeaderText = "Frequency Multiplier";
                theCol23.DataPropertyName = "freqMultiplier";
                theCol23.Name = "freqMultiplier";
                theCol23.Width = 10;
                theCol23.Visible = false;

                DataGridViewTextBoxColumn theCol24 = new DataGridViewTextBoxColumn();
                theCol24.HeaderText = "ItemType";
                theCol24.DataPropertyName = "ItemType";
                theCol24.Width = 10;
                theCol24.Visible = false;
                theCol24.ReadOnly = true;

                //end

                //DataGridViewImageColumn theCol22 = new DataGridViewImageColumn();
                //theCol22.Width = 25;
                //theCol22.Image = Image.FromFile(GblIQCare.GetPath() + "\\No_16x.ico");



                grdDrugDispense.Columns.Add(theCol1);
                grdDrugDispense.Columns.Add(theCol2);
                grdDrugDispense.Columns.Add(theCol3);
                grdDrugDispense.Columns.Add(theCol4);
                grdDrugDispense.Columns.Add(theCol5);
                grdDrugDispense.Columns.Add(theCol6);
                grdDrugDispense.Columns.Add(theCol7);
                // Dose
                grdDrugDispense.Columns.Add(theCol18);

                grdDrugDispense.Columns.Add(theCol15);
                grdDrugDispense.Columns.Add(theCol19);

                grdDrugDispense.Columns.Add(theCol16);
                grdDrugDispense.Columns.Add(theCol8);
                // add
                grdDrugDispense.Columns.Add(theCol9);
                grdDrugDispense.Columns.Add(theCol10);
                grdDrugDispense.Columns.Add(theCol11);
                grdDrugDispense.Columns.Add(theCol12);
                grdDrugDispense.Columns.Add(theCol13);
                grdDrugDispense.Columns.Add(theCol14);

                //todo
                //  grdDrugDispense.Columns.Add(theCol16);
                grdDrugDispense.Columns.Add(theCol17);
                grdDrugDispense.Columns.Add(theCol20);
                grdDrugDispense.Columns.Add(theCol21);
                //grdDrugDispense.Columns.Add(theCol22);

                //john start
                grdDrugDispense.Columns.Add(theCol22);
                grdDrugDispense.Columns.Add(theCol23);
                grdDrugDispense.Columns.Add(theCol24);
                //end
                grdDrugDispense.DataSource = theDT;

            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        private int CheckARVDrugsInGrid()
        {
            //john
            int j = 0;
            for (int i = 0; i < grdDrugDispense.Rows.Count; i++)
            {
                if (grdDrugDispense.Rows[i].Cells[23].Value.ToString() == "37")
                {
                    j = 1;
                }

            }
            return j;
        }

        private void lstSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lstSearch_DoubleClick(sender, e);
            }
        }

        private void txtItemName_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtItemName.Text != "")
            {
                lstSearch.Visible = true;
                lstSearch.Width = 900;
                lstSearch.Left = txtItemName.Left - 80;
                lstSearch.Top = panel4.Top + txtItemName.Top + txtItemName.Height;
                lstSearch.Height = 300;

                DataView theDV = new DataView(thePharmacyMaster.Tables[1]);
                theDV.RowFilter = "DrugName like '%" + txtItemName.Text.Trim().ToString().Replace("%", "[%]") + "%'";
                if (theDV.Count > 0)
                {
                    DataTable theDT = theDV.ToTable();
                    BindFunctions theBindManager = new BindFunctions();
                    theBindManager.Win_BindListBox(lstSearch, theDT, "DisplayItem", "DisplayItemId");
                }
                else
                {
                    lstSearch.DataSource = null;
                }

            }
            else
            {
                lstSearch.Visible = false;
            }
            if (e.KeyCode == Keys.Down)
                lstSearch.Select();
        }

        private void lstSearch_DoubleClick(object sender, EventArgs e)
        {
            if (lstSearch.SelectedValue != "")
            {
                string[] theId = lstSearch.SelectedValue.ToString().Split('-');
                theItemId = Convert.ToInt32(theId.GetValue(0));
                DataView theDV = new DataView(thePharmacyMaster.Tables[1]);
                theDV.RowFilter = "Drug_Pk = " + theItemId.ToString() + " and BatchId = " + theId.GetValue(1).ToString() + " and ExpiryDate='" + theId.GetValue(2).ToString() + "'";
                txtItemName.Text = theDV[0]["DrugName"].ToString();
                txtBatchNo.Text = theDV[0]["BatchNo"].ToString();
                theBatchId = Convert.ToInt32(theDV[0]["BatchId"]);
                txtExpirydate.Text = ((DateTime)theDV[0]["ExpiryDate"]).ToString(GblIQCare.AppDateFormat);
                theSellingPrice = Convert.ToDecimal(theDV[0]["SellingPrice"]);
                theConfigSellingPrice = Convert.ToDecimal(theDV[0]["ConfigSellingPrice"]);
                theCostPrice = Convert.ToDecimal(theDV[0]["CostPrice"]);
                theMargin = Convert.ToDecimal(theDV[0]["DispensingMargin"]);
                theDispensingUnit = Convert.ToInt32(theDV[0]["DispensingId"]);
                theDispensingUnitName = theDV[0]["DispensingUnit"].ToString();
                theFunded = Convert.ToInt32(theDV[0]["Funded"]);
                theAvailQty = Convert.ToInt32(theDV[0]["AvailQty"]);
                theStrength = Convert.ToInt32(theDV[0]["StrengthId"]);
                theItemType = Convert.ToInt32(theDV[0]["DrugTypeId"]);
                ARVBeingDispensed = theDV[0]["GenericAbb"].ToString();
                ItemInstructions = theDV[0]["ItemInstructions"].ToString();
                if (theItemType == 37 && cmbprogram.SelectedValue.ToString() == "223")
                {
                    theProphylaxis = 1;

                }
                else
                {
                    theProphylaxis = 0;

                }

                if (theItemType == 37)
                {
                    if (btnART.Enabled == false)
                    {
                        btnART.Enabled = true;
                        if (lastdispensedARV != "")
                        {
                            grpBoxLastDispense.Visible = true;
                        }
                    }
                }
                else
                {
                    if (CheckARVDrugsInGrid() == 0)
                    {
                        if (btnART.Enabled == true)
                        {
                            btnART.Enabled = false;
                            //grpBoxLastDispense.Visible = false;
                        }
                    }
                }
                theGenericAbb = theDV[0]["GenericAbb"].ToString();
                lstSearch.Visible = false;
                //john
                //cmbFrequency.Select();

                if (theAvailQty == 0)
                {
                    IQCareWindowMsgBox.ShowWindowConfirm("NoAvailQty", this);
                    return;
                }

                if (ARVBeingDispensed != "" && lastdispensedARV != "")
                {
                    if (lastdispensedARV != ARVBeingDispensed)
                    {
                        IQCareWindowMsgBox.ShowWindow("RegimenChangeAlert", this);
                    }
                }

            }
        }

        private string removeRegimenDuplicates(string lastRegimen)
        {
            IEnumerable<string> res = lastRegimen.Split('/').Distinct();
            string result = res.Aggregate((current, next) => current + "/" + next);
            return result;
        }
        //john
        //private void cmbFrequency_KeyUp(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        txtQtyDispensed.Select();
        //    }
        //}

        //john
        //private void txtQtyDispensed_KeyUp(object sender, KeyEventArgs e)
        //{
        //    if (txtQtyDispensed.Text.Trim() != "")
        //    {
        //        string s = txtQtyDispensed.Text;
        //        if (s.IndexOf('.', 0) != -1)
        //        {
        //            s = s.Substring(0, s.IndexOf('.', 0));
        //        }
        //        txtQtyDispensed.Text = s;
        //        if (txtQtyDispensed.Text == "0")
        //        {
        //            txtQtyDispensed.Text = "";
        //            IQCareWindowMsgBox.ShowWindowConfirm("QuanitytData", this);
        //            return;
        //        }


        //        if (Convert.ToInt32(txtQtyDispensed.Text) > theAvailQty)
        //        {
        //            IQCareWindowMsgBox.ShowWindow("DrugDispenseQtyCompare", this);
        //            return;
        //        }
        //        CalculateSellingPrice();
        //    }
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        btnDispenseSubmit.Select();
        //    }
        //}

        //john
        //private void CalculateSellingPrice()
        //{
        //    Int32 theQtyDispense = 0;
        //    if (theConfigSellingPrice == 0)
        //    {
        //        IQCareWindowMsgBox.ShowWindowConfirm("MissingSellingPrice", this);
        //        return;
        //    }
        //    if (txtQtyDispensed.Text == "")
        //        theQtyDispense = 0;
        //    else
        //        theQtyDispense = Convert.ToInt32(txtQtyDispensed.Text);
        //    txtSellingPrice.Text = (theQtyDispense * theSellingPrice).ToString();
        //    if (theFunded == 0)
        //        theBillAmt = Convert.ToDecimal(txtSellingPrice.Text);
        //    else
        //        theBillAmt = Convert.ToDecimal("0");
        //}

        private void btnDispenseSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                IDrug thePharmacyManager = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
                //john 13th Aug 2013
                DataSet sellingPriceDS = new DataSet();
                IDrug theDrugManager = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
                sellingPriceDS = theDrugManager.GetPharmacyDispenseMasters(thePatientId, GblIQCare.intStoreId);
                DataView sellingPriceDV = new DataView(sellingPriceDS.Tables[1]);
                sellingPriceDV.RowFilter = "Drug_Pk = " + theItemId.ToString() + " and BatchId = " + theBatchId.ToString() + " and ExpiryDate='" + txtExpirydate.Text + "'";
                DataTable sellingPriceDT = sellingPriceDV.ToTable();

                if (sellingPriceDT.Rows[0]["AvailQty"].ToString() != null && !sellingPriceDT.Rows[0]["AvailQty"].ToString().Equals(""))
                    qtyAvailableInBatch = Convert.ToDouble(sellingPriceDT.Rows[0]["AvailQty"].ToString());
                else
                    qtyAvailableInBatch = 0;
                //
                
                if (ValidateDrugDispense() == false)
                    return;
                //john
                //if (txtQtyPrescribed.Text.Trim() != "" && txtQtyDispensed.Text.Trim() != "")
                //{
                //    decimal qtydis = Convert.ToDecimal(txtQtyDispensed.Text.Trim());
                //    decimal qtypre = Convert.ToDecimal(txtQtyPrescribed.Text.Trim());
                //    if (qtydis > qtypre)
                //    {
                //        if (MessageBox.Show("You have entered Dispensed Qty more than the Prescribed Qty" +
                //            "\nDo you want to Continue?", "IQCare Management", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel)
                //        {
                //            return;
                //        }
                //    }
                //}
                if (txtItemName.Enabled == false)
                {
                    DataTable theDT = (DataTable)grdDrugDispense.DataSource;
                    DataView theDV = new DataView(theDT);
                    theDV.RowFilter = "ItemId = " + theItemId.ToString() + " and BatchId = " + theBatchId.ToString() + " and ExpiryDate='" + txtExpirydate.Text + "'";
                    //john
                    //if (theDV.Count > 0)
                    //{
                    //    theDV[0]["QtyDisp"] = txtQtyDispensed.Text;
                    //    theDV[0]["FrequencyId"] = Convert.ToInt32(cmbFrequency.SelectedValue);
                    //    theDV[0]["FrequencyName"] = cmbFrequency.Text;
                    //    theDV[0]["SellingPrice"] = txtSellingPrice.Text;
                    //    if (chkPrintPrescription.Checked)
                    //    {
                    //        theDV[0]["PrintPrescriptionStatus"] = "1";
                    //    }
                    //    else
                    //    {
                    //        theDV[0]["PrintPrescriptionStatus"] = "0";
                    //    }

                    //    theDV[0]["PatientInstructions"] = txtPatientInstructions.Text;
                    //}
                    txtItemName.Enabled = true;
                    BindPharmacyDispenseGrid(theDT);
                }
                else
                {
                    DataTable theDT = (DataTable)grdDrugDispense.DataSource;
                    //DataTable theDT = CreatePharmacyDispenseTable();
                    //MessageBox.Show(theDT.Rows.Count.ToString());
                    //MessageBox.Show("Here");
                    

                    if (theOrderStatus == "New Order")
                    {
                        DataRow[] theRmDR = theDT.Select("ItemId = " + theItemId.ToString());
                        if (theRmDR.Length > 0)
                        {
                            if (Convert.ToString(theRmDR[0]["BatchId"]) != "" && theprevbatchId == 0)
                            {
                                if (theRmDR[0]["BatchId"].ToString() == theBatchId.ToString())
                                {
                                    IQCareWindowMsgBox.ShowWindow("BatchExists", this);
                                    return;

                                }
                                DataTable dtrow = theRmDR.CopyToDataTable();
                                DataRow[] theRmFillDR = dtrow.Select("BatchId = " + theBatchId.ToString());
                                if (theRmFillDR.Length > 0)
                                {
                                    theDT.Rows.Remove(theRmFillDR[0]);
                                }
                            }
                            else
                                theDT.Rows.Remove(theRmDR[0]);
                        }
                    }



                    // Check Total Quantity  (Available qty and enter Quantity)
                    //john
                    //if (thePrecribeAmt != 0)
                    //{
                    //    if (theOrderStatus == "Partial Dispense")
                    //    {
                    //        decimal totalQuantity = 0;
                    //        decimal totalItemQty = 0;
                    //        DataTable thePartialDT = theDT;
                    //        DataView dv = thePartialDT.DefaultView;
                    //        dv.RowFilter = "ItemId = " + theItemId.ToString();
                    //        DataTable dtnew = dv.ToTable();
                    //        foreach (DataRow dr in dtnew.Rows)
                    //        {
                    //            totalItemQty = totalItemQty + Convert.ToDecimal(dr[10].ToString());
                    //        }
                    //        if (dtnew.Rows.Count == 1 && totalItemQty == 0)
                    //        {
                    //            DataRow[] theRmDR = theDT.Select("ItemId = " + theItemId.ToString());
                    //            theDT.Rows.Remove(theRmDR[0]);
                    //            string s = txtQtyDispensed.Text;
                    //            if (s.IndexOf('.', 0) != -1)
                    //            {
                    //                s = s.Substring(0, s.IndexOf('.', 0));
                    //            }
                    //            txtQtyDispensed.Text = s;
                    //            //john
                    //            //if (txtQtyDispensed.Text == "0")
                    //            //{
                    //            //    txtQtyDispensed.Focus();
                    //            //    IQCareWindowMsgBox.ShowWindowConfirm("QuanitytData", this);
                    //            //    return;
                    //            //}
                    //        }
                    //        totalQuantity = totalItemQty + Convert.ToDecimal(txtQtyDispensed.Text);
                    //        //john
                    //        //if (totalQuantity > thePrecribeAmt)
                    //        //{
                    //        //    IQCareWindowMsgBox.ShowWindowConfirm("QuanitytDetails", this);
                    //        //    return;
                    //        //}
                    //    }
                    //}
                    if ((theDispCurrentRow > -1) && (theDT.Rows.Count > 0))
                    {
                        DataView theDV = new DataView(theDT);
                        theDV.RowFilter = "ItemId = " + theItemId.ToString() + " and BatchId = " + theBatchId.ToString() + " and ExpiryDate='" + txtExpirydate.Text.Replace("-", " ") + "'";
                        //theDV.RowFilter = "ItemId = " + theItemId.ToString() + " and BatchId = " + theBatchId.ToString() + "";
                        if (theDV.Count > 0)
                        {
                            //john
                            //theDV[0]["QtyDisp"] = txtQtyDispensed.Text;
                            //theDV[0]["FrequencyId"] = Convert.ToInt32(cmbFrequency.SelectedValue);
                            //theDV[0]["FrequencyName"] = cmbFrequency.Text;
                            //theDV[0]["SellingPrice"] = txtSellingPrice.Text;

                            //theDV[0]["OrderedQuantity"] = txtQtyPrescribed.Text.Trim();

                            //theDV[0]["FrequencyId"] = Convert.ToInt32(cmbFrequency.SelectedValue);
                            //theDV[0]["FrequencyName"] = cmbFrequency.Text;
                            theDV[0]["Prophylaxis"] = theProphylaxis;
                            theDV[0]["ItemType"] = theItemType;
                            theDV[0]["GenericAbb"] = theGenericAbb;
                            //theDV[0]["Dose"] = txtDose.Text;
                            //theDV[0]["Duration"] = txtDuration.Text;
                            theDV[0]["BillAmount"] = theBillAmt;
                            theDV[0]["CostPrice"] = theCostPrice;

                            //if (chkPrintPrescription.Checked)
                            //{
                            //    theDV[0]["PrintPrescriptionStatus"] = "1";
                            //}
                            //else
                            //{
                            //    theDV[0]["PrintPrescriptionStatus"] = "0";
                            //}

                            theDV[0]["PatientInstructions"] = ItemInstructions;

                            //lblPayAmount.Text = (Convert.ToDecimal(lblPayAmount.Text) + theBillAmt).ToString();
                        }
                        else
                        {
                            DataRow theDR = theDT.NewRow();
                            theDR["ItemId"] = theItemId;
                            theDR["ItemName"] = txtItemName.Text;
                            theDR["DispensingUnitId"] = theDispensingUnit;
                            theDR["DispensingUnitName"] = theDispensingUnitName;
                            theDR["BatchId"] = theBatchId;
                            theDR["BatchNo"] = txtBatchNo.Text;
                            theDR["ExpiryDate"] = txtExpirydate.Text;
                            //john
                            //theDR["QtyDisp"] = txtQtyDispensed.Text;
                            theDR["CostPrice"] = theCostPrice;
                            theDR["Margin"] = theMargin;
                            //theDR["SellingPrice"] = txtSellingPrice.Text;
                            theDR["BillAmount"] = theBillAmt;
                            theDR["StrengthId"] = theStrength;
                            //theDR["FrequencyId"] = Convert.ToInt32(cmbFrequency.SelectedValue);
                            //theDR["FrequencyName"] = cmbFrequency.Text;
                            theDR["Prophylaxis"] = theProphylaxis;
                            theDR["ItemType"] = theItemType;
                            theDR["GenericAbb"] = theGenericAbb;
                            //theDR["OrderedQuantity"] = txtQtyPrescribed.Text.Trim();
                            //john end

                            //if (thePrecribeAmt != 0)
                            //{
                            //    theDR["OrderedQuantity"] = thePrecribeAmt;// theDT1.Rows[0]["OrderedQuantity"].ToString();
                            //}
                            //else
                            //{
                            //    theDR["OrderedQuantity"] = txtQtyPrescribed.Text.Trim();
                            //}
                            //

                            //john start
                            //theDR["Dose"] = txtDose.Text;
                            //theDR["Duration"] = txtDuration.Text;

                            theDR["DataStatus"] = "1";

                            //john
                            //if (chkPrintPrescription.Checked)
                            //{
                            //    theDR["PrintPrescriptionStatus"] = "1";
                            //}
                            //else
                            //{
                            //    theDR["PrintPrescriptionStatus"] = "0";
                            //}

                            theDR["PatientInstructions"] = ItemInstructions;


                            //lblPayAmount.Text = (Convert.ToDecimal(lblPayAmount.Text) + theBillAmt).ToString();
                            theDT.Rows.Add(theDR);

                            DataTable theNewDT = CreatePharmacyDispenseTable();
                            theNewDT = theDT.Copy();

                            //   grdDrugDispense.DataSource = theNewDT;
                            BindPharmacyDispenseGrid(theNewDT);
                        }
                        theDT.AcceptChanges();
                        BindPharmacyDispenseGrid(theDT);
                        theDispCurrentRow = -1;
                    }
                    else
                    {
                        DataRow theDR = theDT.NewRow();
                        theDR["ItemId"] = theItemId;
                        theDR["ItemName"] = txtItemName.Text;
                        theDR["DispensingUnitId"] = theDispensingUnit;
                        theDR["DispensingUnitName"] = theDispensingUnitName;
                        theDR["BatchId"] = theBatchId;
                        theDR["BatchNo"] = txtBatchNo.Text;
                        theDR["ExpiryDate"] = txtExpirydate.Text;
                        theDR["QtyDisp"] = 0;
                        theDR["CostPrice"] = theCostPrice;
                        theDR["Margin"] = theMargin;
                        theDR["SellingPrice"] = 0;
                        theDR["BillAmount"] = theBillAmt;
                        theDR["StrengthId"] = theStrength;
                        theDR["FrequencyId"] = 0;
                        theDR["FrequencyName"] = "";
                        theDR["Prophylaxis"] = theProphylaxis;
                        theDR["ItemType"] = theItemType;
                        theDR["GenericAbb"] = theGenericAbb;
                        theDR["OrderedQuantity"] = 0;
                        //if (thePrecribeAmt != 0)
                        //{
                        //    theDR["OrderedQuantity"] = thePrecribeAmt;// theDT1.Rows[0]["OrderedQuantity"].ToString();
                        //}
                        //else
                        //{
                        //    theDR["OrderedQuantity"] = txtQtyPrescribed.Text.Trim();
                        //}
                        //

                        //john start
                        theDR["Dose"] = 0;
                        theDR["Duration"] = 0;
                        theDR["freqMultiplier"] = 0;
                        if (sellingPriceDT.Rows[0]["ConfigSellingPrice"].ToString() != null && !sellingPriceDT.Rows[0]["ConfigSellingPrice"].ToString().Equals(""))
                            theDR["UnitSellingPrice"] = Convert.ToInt32(sellingPriceDT.Rows[0]["ConfigSellingPrice"].ToString());
                        else
                            theDR["UnitSellingPrice"] =0;

                        theDR["DataStatus"] = "1";

                        
                        //if (chkPrintPrescription.Checked)
                        //{
                        //    theDR["PrintPrescriptionStatus"] = "1";
                        //}
                        //else
                        //{
                        //    theDR["PrintPrescriptionStatus"] = "0";
                        //}

                        theDR["PatientInstructions"] = ItemInstructions;


                        //lblPayAmount.Text = (Convert.ToDecimal(lblPayAmount.Text) + theBillAmt).ToString();
                        theDT.Rows.Add(theDR);

                        DataTable theNewDT = CreatePharmacyDispenseTable();
                        theNewDT = theDT.Copy();

                        //   grdDrugDispense.DataSource = theNewDT;
                        BindPharmacyDispenseGrid(theNewDT);
                    }
                }
                // DataSet theDS = thePharmacyManager.GetPharmacyExistingRecordDetails(theOrderId);
                //BindPharmacyDispenseGrid(theDS.Tables[0]);

                theItemId = 0;
                theDispensingUnit = 0;
                theBatchId = 0;
                theCostPrice = 0;
                theMargin = 0;
                theBillAmt = 0;
                theDispensingUnitName = "";
                txtItemName.Text = "";
                txtBatchNo.Text = "";
                //cmbFrequency.SelectedValue = "0";
                //txtQtyDispensed.Text = "";
                txtExpirydate.Text = "";
                //txtSellingPrice.Text = "";
                //txtDose.Text = "";
                //txtDuration.Text = "";
                txtItemName.Select();
                //txtQtyPrescribed.Text = "";
                //txtQtyPrescribed.Enabled = true;
                //chkPrintPrescription.Checked = false;
                //txtPatientInstructions.Text = "";
                theprevbatchId = 0;
                //txtWeight.Text = "";
                //txtHeight.Text = "";
                //txtBSA.Text = "";
                //cmbprogram.SelectedValue = "0";
                //cmdPeriodTaken.SelectedValue = "0";
                //cmbProvider.SelectedValue = "0";
                //cmbRegimenLine.SelectedValue = "0";
                //cmbReason.SelectedValue = "0";

            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = "Please select Drug";
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
                //throw err;
            }
        }

        private Boolean ValidateDrugDispense()
        {
            if (txtItemName.Text.Trim() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Drug Name";
                IQCareWindowMsgBox.ShowWindowConfirm("BlankTextBox", theBuilder, this);
                return false;
            }
            //john
            //if (txtDose.Text.Trim() == "")
            //{
            //    MsgBuilder theBuilder = new MsgBuilder();
            //    theBuilder.DataElements["Control"] = "Dose";
            //    IQCareWindowMsgBox.ShowWindowConfirm("BlankTextBox", theBuilder, this);
            //    return false;
            //}
            //if (Convert.ToInt32(cmbFrequency.SelectedValue) < 1)
            //{
            //    MsgBuilder theBuilder = new MsgBuilder();
            //    theBuilder.DataElements["Control"] = "Frequency";
            //    IQCareWindowMsgBox.ShowWindowConfirm("BlankDropDown", theBuilder, this);
            //    return false;
            //}
            //if (txtDuration.Text.Trim() == "")
            //{
            //    MsgBuilder theBuilder = new MsgBuilder();
            //    theBuilder.DataElements["Control"] = "Duration";
            //    IQCareWindowMsgBox.ShowWindowConfirm("BlankTextBox", theBuilder, this);
            //    return false;
            //}
            //if (txtQtyPrescribed.Text.Trim() == "")
            //{
            //    MsgBuilder theBuilder = new MsgBuilder();
            //    theBuilder.DataElements["Control"] = "Quantity Prescribed";
            //    IQCareWindowMsgBox.ShowWindowConfirm("BlankTextBox", theBuilder, this);
            //    return false;
            //}
            ////if (Convert.ToInt32(cmbprogram.SelectedValue) < 1)
            ////{
            ////    MsgBuilder theBuilder = new MsgBuilder();
            ////    theBuilder.DataElements["Control"] = "Program";
            ////    IQCareWindowMsgBox.ShowWindowConfirm("BlankDropDown", theBuilder, this);
            ////    return false;
            ////}
            //if (txtQtyDispensed.Text.Trim() == "")
            //{
            //    MsgBuilder theBuilder = new MsgBuilder();
            //    theBuilder.DataElements["Control"] = "Quantity Dispensed";
            //    IQCareWindowMsgBox.ShowWindowConfirm("BlankTextBox", theBuilder, this);
            //    return false;
            //}
            ////if (txtQtyPrescribed.Text.Trim() != "" && txtQtyDispensed.Text.Trim() != "")
            ////{
            ////    decimal qtydis = Convert.ToDecimal(txtQtyDispensed.Text.Trim());
            ////    decimal qtypre = Convert.ToDecimal(txtQtyPrescribed.Text.Trim());
            ////    if (qtydis > qtypre)
            ////    {
            ////        MsgBuilder theBuilder = new MsgBuilder();
            ////        theBuilder.DataElements["Control"] = "Quantity Dispensed";
            ////        IQCareWindowMsgBox.ShowWindowConfirm("DrugQtyCompare", theBuilder, this);
            ////        return false;
            ////    }
            ////}
            return true;
        }

        private void txtItemName_KeyPress(object sender, KeyPressEventArgs e)
        {
            BindFunctions theBindManager = new BindFunctions();
            theBindManager.Win_String(e);
        }

        private void txtQtyDispensed_KeyPress(object sender, KeyPressEventArgs e)
        {
            BindFunctions theBindManager = new BindFunctions();
            theBindManager.Win_Numeric(e);
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            clearPopup();
            this.Close();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            bool DispenseSaved = false;

            try
            {
                IDrug thePharmacyManager = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
                if (tabDispense.TabPages[1].Focus() == true)
                {

                    //DataSet thsdsval = thePharmacyManager.CheckDispencedDate(thePatientId, GblIQCare.AppLocationId, Convert.ToDateTime(dtDispensedDate.Text), theOrderId);
                    //if (thsdsval.Tables[0].Rows.Count > 0)
                    //{
                    //    string GetStatus = thsdsval.Tables[0].Rows[0]["Status"].ToString();
                    //    if (GetStatus == "1")
                    //    {
                    //        IQCareWindowMsgBox.ShowWindow("DispenceCheck", this);
                    //        return;
                    //    }
                    //}

                    DataTable theDT = (DataTable)grdDrugDispense.DataSource;
                    string thetmpRegimen = "";
                    string theRegimen = "";
                    Int32 theProgId = 0;
                    Int32 theAge = Convert.ToDateTime(dtDispensedDate.Text).Year - theDOB.Year;
                    if (theAge > 15)
                        theProgId = 116;
                    else
                        theProgId = 117;
                    DataTable theUniqueDT = theDT.Copy();
                    DataTable theDupFilteredDT = RemoveDuplicatesRecords(theUniqueDT);
                    foreach (DataRow theDR in theDupFilteredDT.Rows)
                    {
                        if (theDR["ItemType"].ToString() == "37" && theDR["Prophylaxis"].ToString() != "1")
                        {
                            if (thetmpRegimen == "" && theDR["GenericAbb"].ToString() != "")
                                thetmpRegimen = thetmpRegimen + theDR["GenericAbb"].ToString();
                            else if (thetmpRegimen != "" && theDR["GenericAbb"].ToString() != "")
                                if (!thetmpRegimen.Contains(theDR["GenericAbb"].ToString()))
                                    thetmpRegimen = thetmpRegimen + "/" + theDR["GenericAbb"].ToString();
                        }
                    }

                    foreach (string s in thetmpRegimen.Split('/'))
                    {
                        if (theRegimen == "" && s != "")
                            theRegimen = theRegimen + s;
                        else if (theRegimen != "" && s != "")
                            if (!theRegimen.Contains(s))
                                theRegimen = theRegimen + "/" + s;
                   }
                    string thepharmacyRefilldate = "";
                    if (dtRefillApp.CustomFormat == " ")
                        thepharmacyRefilldate = "01-01-1900";
                    else
                        thepharmacyRefilldate = dtRefillApp.Text;


                    //john commented
                    //DataTable dtPharmacyDetails = thePharmacyManager.SavePharmacyDispense(thePatientId, GblIQCare.AppLocationId, GblIQCare.intStoreId, GblIQCare.AppUserId,
                    //     Convert.ToDateTime(dtDispensedDate.Text), theProgId, Convert.ToInt32(cmbprogram.SelectedValue), theRegimen, theOrderId, theDT, Convert.ToDateTime(thepharmacyRefilldate));

                    if (btnART.Enabled)
                    {
                        //john commented
                        //int val = theOrderId;
                        //int ptnPharmacyPK = Convert.ToInt32(dtPharmacyDetails.Rows[0]["Ptn_Pharmacy_Pk"].ToString());
                        //SaveUpdateArt(ptnPharmacyPK);

                        //john 16th Oct 2013
                        //DataTable dtPaperlessClinic = new DataTable();
                        //dtPaperlessClinic = thePharmacyManager.CheckPaperlessClinic(GblIQCare.AppLocationId);
                        //if (dtPaperlessClinic.Rows[0][0].ToString() == "1")
                        //{
                        //    DataTable dtPharmacyDetails = thePharmacyManager.SavePharmacyDispense(thePatientId, GblIQCare.AppLocationId, GblIQCare.intStoreId, GblIQCare.AppUserId,
                        //        Convert.ToDateTime(dtDispensedDate.Text), theProgId, Convert.ToInt32(cmbprogram.SelectedValue), theRegimen, theOrderId, theDT, Convert.ToDateTime(thepharmacyRefilldate));
                        //    int val = theOrderId;
                        //    int ptnPharmacyPK = Convert.ToInt32(dtPharmacyDetails.Rows[0]["Ptn_Pharmacy_Pk"].ToString());

                        //    SaveUpdateArt(ptnPharmacyPK);
                        //    IQCareWindowMsgBox.ShowWindow("PharmacyDispenseSave", this);
                        //    DataTable theNewDT = CreatePharmacyDispenseTable();
                        //    BindPharmacyDispenseGrid(theNewDT);
                        //    grdDrugDispense.DataSource = theNewDT;
                        //    btnART.Enabled = false;
                        //}
                        //else
                        //{
                        if (HeightWeightPopUp == 0)
                        {
                            grpHivCareTrtPharmacyField.Visible = true;

                            DispenseSaved = false;
                        }
                        else if (cmbprogram.SelectedValue.ToString() == "0" || cmbRegimenLine.SelectedValue.ToString() == "0" || cmbProvider.SelectedValue.ToString() == "0")
                        {
                            MessageBox.Show("Program, Regimen Line and Drug Provider cannot be blank");
                            grpHivCareTrtPharmacyField.Visible = true;

                            DispenseSaved = false;
                        }
                        else
                        {
                            DataTable dtPharmacyDetails = thePharmacyManager.SavePharmacyDispense(thePatientId, GblIQCare.AppLocationId, GblIQCare.intStoreId, GblIQCare.AppUserId,
                                Convert.ToDateTime(dtDispensedDate.Text), theProgId, Convert.ToInt32(cmbprogram.SelectedValue), theRegimen, theOrderId, theDT, Convert.ToDateTime(thepharmacyRefilldate));
                            int val = theOrderId;
                            int ptnPharmacyPK = Convert.ToInt32(dtPharmacyDetails.Rows[0]["Ptn_Pharmacy_Pk"].ToString());
                            SaveUpdateArt(ptnPharmacyPK);
                            IQCareWindowMsgBox.ShowWindow("PharmacyDispenseSave", this);
                            DataTable theNewDT = CreatePharmacyDispenseTable();
                            BindPharmacyDispenseGrid(theNewDT);
                            grdDrugDispense.DataSource = theNewDT;
                            btnART.Enabled = false;
                            HeightWeightPopUp = 0;
                            if (int.Parse(cmbRegimenLine.SelectedValue.ToString()) > lastRegimenLine)
                            {
                                lastRegimenLine = int.Parse(cmbRegimenLine.SelectedValue.ToString());
                            }

                            DispenseSaved = true;
                        }
                        //}
                        //

                    }
                    else
                    {
                        DataTable dtPharmacyDetails = thePharmacyManager.SavePharmacyDispense(thePatientId, GblIQCare.AppLocationId, GblIQCare.intStoreId, GblIQCare.AppUserId,
                         Convert.ToDateTime(dtDispensedDate.Text), theProgId, Convert.ToInt32(cmbprogram.SelectedValue), theRegimen, theOrderId, theDT, Convert.ToDateTime(thepharmacyRefilldate));
                        IQCareWindowMsgBox.ShowWindow("PharmacyDispenseSave", this);
                        DataTable theNewDT = CreatePharmacyDispenseTable();
                        BindPharmacyDispenseGrid(theNewDT);
                        grdDrugDispense.DataSource = theNewDT;
                        btnART.Enabled = false;

                        DispenseSaved = true;
                    }

                    //john commented
                    //IQCareWindowMsgBox.ShowWindow("PharmacyDispenseSave", this);
                    //DataTable theNewDT = CreatePharmacyDispenseTable();
                    //BindPharmacyDispenseGrid(theNewDT);
                    //grdDrugDispense.DataSource = theNewDT;

                    //todo

                    if (DispenseSaved)  //Reset these values only if dispense has been saved
                    {

                        theOrderId = 0;
                        theOrderStatus = "";
                        lblPayAmount.Text = "0.0";
                    }
                    
                }
                else if (tabDispense.TabPages[2].Focus() == true)
                {

                    if (Convert.ToDateTime(dtpReturnDate.Text) < Convert.ToDateTime(dtDispensedDate.Text))
                    {
                        IQCareWindowMsgBox.ShowWindow("NoDrugReturnDate", this);
                        return;
                    }
                    if (grdReturnDetail.RowCount > 0)
                    {
                        DataTable theDT = (DataTable)grdReturnDetail.DataSource;
                        int theRowsforSave = 0;
                        foreach (DataRow theDR in theDT.Rows)
                        {
                            if (Convert.ToInt32(theDR["ReturnQty"]) > 0)
                            {
                                theRowsforSave = theRowsforSave + 1;
                                theDR["SellingPrice"] = Convert.ToDecimal("-" + theDR["UnitSellingPrice"].ToString()) * Convert.ToInt32(theDR["ReturnQty"]);
                                if (Convert.ToInt32(theDR["BillAmount"]) > 0)
                                {
                                    theDR["BillAmount"] = theDR["SellingPrice"];
                                }
                                else
                                {
                                    theDR["BillAmount"] = 0;
                                }
                            }
                        }

                        if (theRowsforSave == 0)
                        {
                            IQCareWindowMsgBox.ShowWindow("NoDrugReturn", this);
                            return;
                        }
                        thePharmacyManager.SavePharmacyReturn(thePatientId, GblIQCare.AppLocationId, GblIQCare.intStoreId, Convert.ToDateTime(dtpReturnDate.Text), GblIQCare.AppUserId, theReturnOrderId, theDT);
                        IQCareWindowMsgBox.ShowWindow("PharmacyReturnSave", this);
                        theDT = thePharmacyManager.GetPharmacyExistingRecord(thePatientId, GblIQCare.intStoreId);
                        BindDrugReturnGrid(theDT);
                        grdReturnDetail.DataSource = false;
                        grdReturnDetail.Columns.Clear();
                        btnART.Enabled = false;
                    }
                    else
                    {
                        IQCareWindowMsgBox.ShowWindow("NoDrugReturn", this);
                        return;
                    }
                    thePharmacyMaster = thePharmacyManager.GetPharmacyDispenseMasters(thePatientId, GblIQCare.intStoreId);

                    //btnART.Enabled = false;
                    grpBoxLastDispense.Visible = false;
                    chkPharmacyRefill.Checked = false;
                }
                
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                if (err.Message.ToString() == "Deleted row information cannot be accessed through the row.")
                {
                    theBuilder.DataElements["MessageText"] = "You cannot delete a prescribed drug.";
                }
                else
                {
                    theBuilder.DataElements["MessageText"] = err.Message.ToString();
                }
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }
        private DataTable RemoveDuplicatesRecords(DataTable dt)
        {
            var result = dt.AsEnumerable()
                 .GroupBy(r => r.Field<int>("ItemID"))
                 .Select(g => g.First())
                 .CopyToDataTable();

            return result;
        }
        private void btnView_Click(object sender, EventArgs e)
        {
            grpExistingRec.Visible = true;
            grpExistingRec.Left = 68;
            grpExistingRec.Top = 56;
            grpExistingRec.Width = 702;
            grpExistingRec.Height = 280;
            IDrug thePharmacyManager = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
            DataTable theDT = thePharmacyManager.GetPharmacyExistingRecord(thePatientId, GblIQCare.intStoreId);
            BindExitingGrid(theDT);
        }

        private void BindExitingGrid(DataTable theDT)
        {
            grdExitingPharDisp.DataSource = null;
            grdExitingPharDisp.Columns.Clear();
            grdExitingPharDisp.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn theCol1 = new DataGridViewTextBoxColumn();
            theCol1.HeaderText = "Transaction Date";
            theCol1.DataPropertyName = "TransactionDate";
            theCol1.Width = 200;

            DataGridViewTextBoxColumn theCol2 = new DataGridViewTextBoxColumn();
            theCol2.HeaderText = "Status";
            theCol2.DataPropertyName = "Status";
            theCol2.Width = 200;

            DataGridViewTextBoxColumn theCol3 = new DataGridViewTextBoxColumn();
            theCol3.HeaderText = "Id";
            theCol3.DataPropertyName = "Ptn_Pharmacy_Pk";
            theCol3.Width = 10;
            theCol3.Visible = false;

            grdExitingPharDisp.Columns.Add(theCol1);
            grdExitingPharDisp.Columns.Add(theCol2);
            grdExitingPharDisp.Columns.Add(theCol3);
            grdExitingPharDisp.DataSource = theDT;

        }

        private void BindDrugReturnGrid(DataTable theDT)
        {
            grdReturnOrder.DataSource = null;
            grdReturnOrder.Columns.Clear();
            grdReturnOrder.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn theCol1 = new DataGridViewTextBoxColumn();
            theCol1.HeaderText = "Transaction Date";
            theCol1.DataPropertyName = "TransactionDate";
            theCol1.Width = 200;

            DataGridViewTextBoxColumn theCol2 = new DataGridViewTextBoxColumn();
            theCol2.HeaderText = "Status";
            theCol2.DataPropertyName = "Status";
            theCol2.Width = 200;

            DataGridViewTextBoxColumn theCol3 = new DataGridViewTextBoxColumn();
            theCol3.HeaderText = "Id";
            theCol3.DataPropertyName = "Ptn_Pharmacy_Pk";
            theCol3.Width = 10;
            theCol3.Visible = false;

            grdReturnOrder.Columns.Add(theCol1);
            grdReturnOrder.Columns.Add(theCol2);
            grdReturnOrder.Columns.Add(theCol3);
            grdReturnOrder.DataSource = theDT;
        }

        private void btnExitingRecClose_Click(object sender, EventArgs e)
        {
            grpExistingRec.Visible = false;
        }

        private void grdExitingPharDisp_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            clearPopup();
            chkPharmacyRefill.Checked = false;

            theOrderId = Convert.ToInt32(grdExitingPharDisp.Rows[grdExitingPharDisp.CurrentRow.Index].Cells[2].Value);
            theOrderStatus = grdExitingPharDisp.Rows[grdExitingPharDisp.CurrentRow.Index].Cells[1].Value.ToString();
            IDrug thePharmacyManager = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
            DataSet theDS = thePharmacyManager.GetPharmacyExistingRecordDetails(theOrderId);
            if (theDS.Tables[1].Rows.Count > 0)
            {
                if (theDS.Tables[1].Rows[0].IsNull("DispensedByDate") == false)
                    dtDispensedDate.Text = theDS.Tables[1].Rows[0]["DispensedByDate"].ToString();
                if (theDS.Tables[1].Rows[0].IsNull("ProgId") == false)
                    cmbprogram.SelectedValue = theDS.Tables[1].Rows[0]["ProgId"].ToString();
            }
            decimal theBillAmount = 0;
            theExistingDrugs = theDS.Tables[0];
            foreach (DataRow theRow in theDS.Tables[0].Rows)
            {
                if (theRow.IsNull("BillAmount") == false)
                    theBillAmount = theBillAmount + Convert.ToDecimal(theRow["BillAmount"]);
                else
                    theBillAmount = theBillAmount + 0;
            }
            lblPayAmount.Text = theBillAmount.ToString();

            if (theOrderId > 0 && (theOrderStatus == "Already Dispensed Order" || theOrderStatus == "Partial Dispense"))
            {
                makeGridEditable = "No";
                dtRefillApp.CustomFormat = " ";
                //cmdSave.Enabled = false;
            }
            else
            {
                //cmdSave.Enabled = true;
                makeGridEditable = "Yes";
            }

            BindPharmacyDispenseGrid(theDS.Tables[0]);
            //MessageBox.Show(theDS.Tables[0].Rows.Count.ToString());

            btnART.Enabled = false;
            grpBoxLastDispense.Visible = false;


            if (theDS.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < theDS.Tables[0].Rows.Count; i++)
                {
                    Int32 ItemID = Convert.ToInt32(theDS.Tables[0].Rows[i]["ItemId"].ToString());
                    IDrug thePharmacyDetails = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
                    DataSet theDS5 = thePharmacyManager.GetDrugTypeID(ItemID);
                    if (theDS5.Tables[0].Rows.Count > 0)
                    {
                        string DrugID = theDS5.Tables[0].Rows[0]["DrugTypeID"].ToString();
                        if (DrugID == "37")
                        {
                            //john 21st Aug 2013
                            if (theDS.Tables[0].Rows[i]["ItemType"].ToString() == "37")
                                ARVBeingDispensed=theDS.Tables[0].Rows[i]["GenericAbb"].ToString();
                            //

                            int a = theOrderId;
                            btnART.Enabled = true;
                            if (lastdispensedARV != "")
                            {
                                grpBoxLastDispense.Visible = true;
                            }
                            clearPopup();
                            if (btnART.Enabled)
                            {
                                if (theOrderId > 0)
                                {
                                    IDrug thePharmacyManager1 = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
                                    DataSet theDS1 = thePharmacyManager1.GetPharmacyDetailsByDespenced(theOrderId);
                                    if (theDS1.Tables[0].Rows.Count > 0)
                                    {
                                        txtWeight.Text = theDS1.Tables[0].Rows[0]["Weight"].ToString();
                                        txtHeight.Text = theDS1.Tables[0].Rows[0]["Height"].ToString();
                                        if (txtWeight.Text != "" && txtHeight.Text != "")
                                        {
                                            Double calVal = (Convert.ToDouble(txtWeight.Text) * Convert.ToDouble(txtHeight.Text) / 3600);
                                            calVal = (Double)Math.Sqrt(Convert.ToDouble(calVal));
                                            txtBSA.Text = Math.Round(calVal, 2).ToString();
                                        }

                                        cmbprogram.SelectedValue = theDS1.Tables[0].Rows[0]["ProgID"].ToString();
                                        if (theDS1.Tables[0].Rows[0]["PharmacyPeriodTaken"].ToString() != "")
                                        {
                                            cmdPeriodTaken.SelectedValue = theDS1.Tables[0].Rows[0]["PharmacyPeriodTaken"].ToString();
                                        }
                                        if (theDS1.Tables[0].Rows[0]["ProviderID"].ToString() != "")
                                        {
                                            cmbProvider.SelectedValue = theDS1.Tables[0].Rows[0]["ProviderID"].ToString();
                                        }
                                        if (theDS1.Tables[0].Rows[0]["RegimenLine"].ToString() != "")
                                        {
                                            cmbRegimenLine.SelectedValue = theDS1.Tables[0].Rows[0]["RegimenLine"].ToString();
                                        }
                                        if (theDS1.Tables[0].Rows[0]["AppDate"].ToString() != "")
                                        {
                                            NxtAppDate.Format = DateTimePickerFormat.Custom;
                                            NxtAppDate.CustomFormat = "dd-MMM-yyyy";
                                            NxtAppDate.Text = theDS1.Tables[0].Rows[0]["AppDate"].ToString();

                                        }
                                        else
                                        {
                                            // NxtAppDate.Format = DateTimePickerFormat.Custom;
                                            NxtAppDate.CustomFormat = " ";


                                        }
                                        if (theDS1.Tables[0].Rows[0]["AppReason"].ToString() != "")
                                        {
                                            cmbReason.SelectedValue = theDS1.Tables[0].Rows[0]["AppReason"].ToString();
                                        }

                                        if (cmbprogram.SelectedValue.ToString() == "222")
                                        {
                                            int patientID = thePatientId;
                                            DataSet GettheDS = thePharmacyManager1.SaveArtData(patientID, Convert.ToDateTime(dtDispensedDate.Text));
                                        }


                                    }
                                }
                            }

                        }
                    }
                }
            }

            // todo
            //if (theDS.Tables[0].Rows[0]["OrderedQuantity"].ToString() != null && theDS.Tables[0].Rows[0]["OrderedQuantity"].ToString() != "")
            //{
            //    thePrecribeAmt = Convert.ToDecimal(theDS.Tables[0].Rows[0]["OrderedQuantity"]);
            //}
            //Already Dispensed Order
            if (theOrderId > 0 && (theOrderStatus == "Already Dispensed Order" || theOrderStatus == "Partial Dispense"))
            {
                makeGridEditable = "No";
                cmdSave.Enabled = false;
            }
            else
            {
                cmdSave.Enabled = true;
                makeGridEditable = "Yes";
            }

            //john 21st Aug 2013
            if (ARVBeingDispensed != "" && lastdispensedARV != "")
            {
                if (ARVBeingDispensed != lastdispensedARV)
                {
                    IQCareWindowMsgBox.ShowWindow("RegimenChangeAlert", this);
                    //MessageBox.Show("Change in regimen");
                }
            }
            //
            grpExistingRec.Visible = false;
        }

        private void clearPopup()
        {
            txtWeight.Text = "";
            txtHeight.Text = "";
            txtBSA.Text = "";
            cmdPeriodTaken.SelectedValue = "0";
            cmbProvider.SelectedValue = "0";
 
            cmbReason.SelectedValue = "0";
            cmbprogram.SelectedValue = "0";
            lastRegimenLine = 0;
            cmbRegimenLine.SelectedValue = "0";

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            thePrecribeAmt = 0;
            theOrderId = 0;
            DataTable theNewDT = CreatePharmacyDispenseTable();
            grdDrugDispense.DataSource = theNewDT;
            BindPharmacyDispenseGrid(theNewDT);
            dtDispensedDate.Text = GblIQCare.CurrentDate;
            cmbprogram.SelectedValue = "0";
            theItemId = 0;
            theDispensingUnit = 0;
            theBatchId = 0;
            theCostPrice = 0;
            theMargin = 0;
            theBillAmt = 0;
            theDispensingUnitName = "";
            txtItemName.Text = "";
            txtBatchNo.Text = "";
            //cmbFrequency.SelectedValue = "0";
            //txtQtyDispensed.Text = "";
            txtExpirydate.Text = "";
            //txtSellingPrice.Text = "";
            //txtDose.Text = "";
            //txtDuration.Text = "";
            txtItemName.Select();
            //txtQtyPrescribed.Text = "";
            //txtQtyPrescribed.Enabled = true;
            txtWeight.Text = "";
            txtHeight.Text = "";
            txtBSA.Text = "";
            //chkPrintPrescription.Checked = false;
            //txtPatientInstructions.Text = "";
            cmdPeriodTaken.SelectedValue = "0";
            cmbProvider.SelectedValue = "0";
            cmbRegimenLine.SelectedValue = "0";
            cmbReason.SelectedValue = "0";
            btnART.Enabled = false;
            grpBoxLastDispense.Visible = false;
            cmdSave.Enabled = true;
            //john cmbAutoGrid.Items.Clear();
            cmbGrdDrugDispense.Hide();
            cmbGrdDrugDispenseFreq.Hide();
            makeGridEditable = "Yes";
            dtRefillApp.CustomFormat = " ";
            chkPharmacyRefill.Checked = false;
            lblPayAmount.Text = "0.0";
            clearPopup();
        }

        //this.images is an ImageList with your bitmaps
        void grdDrugDispense_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == 19 && e.RowIndex == -1)
            {
                e.PaintBackground(e.ClipBounds, false);

                Point pt = e.CellBounds.Location;  // where you want the bitmap in the cell

                int offsetX = (e.CellBounds.Width - this.imageList1.ImageSize.Width) / 2;
                int offsetY = (e.CellBounds.Height - this.imageList1.ImageSize.Height) / 2;
                pt.X += offsetX;
                pt.Y += offsetY;
                this.imageList1.Draw(e.Graphics, pt, 0);
                e.Handled = true;
            }
        }

        private void grdDrugDispense_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //int PresdispenceQty = 0;
            //decimal totalOqdQty = 0;

            ////if (grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[10].Value.ToString() != "" && grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[10].Value.ToString() != "0")
            //if (grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[10].Value.ToString() != "")
            //{
            //    if (theOrderId > 0 && theOrderStatus == "Partial Dispense")
            //    {
            //        IQCareWindowMsgBox.ShowWindow("PharmacyCannotAlterRow", this);
            //        return;
            //    }
            //    else if (!Convert.ToInt32(grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[10].Value).Equals(System.DBNull.Value))
            //    {
            //        theDispCurrentRow = grdDrugDispense.CurrentRow.Index;
            //        PresdispenceQty = Convert.ToInt32(grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[10].Value);

            //        thePrecribeAmt = PresdispenceQty;

            //        string itemID = grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[0].Value.ToString();
            //        DataTable dtGrd = ((DataTable)(grdDrugDispense.DataSource)).Copy();
            //        DataView dv = dtGrd.DefaultView;
            //        dv.RowFilter = "ItemId=" + itemID;

            //        DataTable dt1 = dv.ToTable();// Table;

            //        foreach (DataRow dr in dt1.Rows)
            //        {
            //            totalOqdQty = totalOqdQty + Convert.ToDecimal(dr["QtyDisp"].ToString());
            //        }
            //        //if (PresdispenceQty <= totalOqdQty)
            //        //{
            //        //    IQCareWindowMsgBox.ShowWindow("PharmacyCannotAlterRow", this);
            //        //    return;
            //        //}
            //        //Already Dispensed Order
            //        if (theOrderId > 0 && theOrderStatus == "Already Dispensed Order")
            //        {
            //            IQCareWindowMsgBox.ShowWindow("PharmacyCannotAlterRow", this);
            //            return;
            //        }
            //    }
            //}
            //else if (theOrderId > 0 && theOrderStatus != "New Order")
            //{
            //    IQCareWindowMsgBox.ShowWindow("PharmacyCannotAlterRow", this);
            //    return;
            //}
            //theItemId = Convert.ToInt32(grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[0].Value);
            //DataView theDV = new DataView(thePharmacyMaster.Tables[1]);

            //// Add && theOrderStatus != "Partial Dispense"  below line
            //if (theOrderStatus != "New Order" && theOrderStatus != "Partial Dispense")
            //{
            //    theDV.RowFilter = "Drug_Pk = " + theItemId.ToString() + " and BatchId = " + grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[4].Value.ToString() +
            //        " and ExpiryDate='" + grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[6].Value.ToString() + "'";
            //}
            //else
            //{
            //    theDV.RowFilter = "Drug_Pk = " + theItemId.ToString();
            //}
            //if (theDV.Count < 1)
            //{
            //    IQCareWindowMsgBox.ShowWindow("NoAvailQty", this);
            //    return;
            //}

            //if ((theDV.ToTable().Rows.Count == 0) && (Convert.ToInt32(grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[9].Value) == 0))
            //{
            //    IQCareWindowMsgBox.ShowWindow("NoAvailQty", this);
            //    return;
            //}
            //else
            //{
            //    txtItemName.Text = theDV[0]["DrugName"].ToString();




            //    if (theOrderStatus == "New Order")
            //    {

            //        cmbFrequency.SelectedValue = grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[17].Value.ToString();
            //        txtSellingPrice.Text = "";
            //        txtBatchNo.Text = "";
            //        theBatchId = 0;
            //        txtExpirydate.Text = "";
            //        if (grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[7].Value.ToString() != "")
            //        {
            //            txtDose.Text = grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[7].Value.ToString();
            //        }
            //        else
            //        {
            //            txtDose.Text = "";
            //        }
            //        if (grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[9].Value.ToString() != "")
            //        {
            //            txtDuration.Text = grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[9].Value.ToString();
            //        }
            //        else
            //        {
            //            txtDuration.Text = "";
            //        }
            //        if (grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[10].Value.ToString() != "")
            //        {
            //            txtQtyPrescribed.Text = grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[10].Value.ToString();
            //            if (theExistingDrugs.Rows.Count > 0)
            //            {
            //                DataRow[] theexistrow = theExistingDrugs.Select("ItemId=" + theItemId.ToString());
            //                if (theexistrow.Length > 0)
            //                    txtQtyPrescribed.Enabled = false;
            //            }
            //        }
            //        else
            //        {
            //            txtQtyPrescribed.Text = "";
            //        }
            //        if (grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[11].Value.ToString() != "0.00")
            //        {
            //            txtQtyDispensed.Text = grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[11].Value.ToString();

            //        }
            //        else
            //        {
            //            txtQtyDispensed.Text = "";
            //        }
            //        if (grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[14].Value.ToString() != "")
            //        {
            //            txtSellingPrice.Text = grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[14].Value.ToString();
            //        }
            //        else
            //        {
            //            txtSellingPrice.Text = "";
            //        }
            //        //print prescription
            //        if (grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[19].Value.ToString() != "0")
            //        {
            //            chkPrintPrescription.Checked = true;
            //        }
            //        else
            //        {
            //            chkPrintPrescription.Checked = false;
            //        }
            //        if (grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[20].Value.ToString() != "")
            //        {
            //            txtPatientInstructions.Text = grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[20].Value.ToString();
            //        }
            //        else
            //        {
            //            txtPatientInstructions.Text = "";
            //        }

            //        theprevbatchId = 1;
            //        theSellingPrice = 0;
            //        theConfigSellingPrice = 0;
            //        theCostPrice = 0;
            //        theMargin = 0;
            //        theDispensingUnit = 0;
            //        theDispensingUnitName = "";
            //        theFunded = 0;
            //        theAvailQty = 0;
            //        theStrength = 0;
            //        theItemType = Convert.ToInt32(theDV[0]["DrugTypeId"]);
            //        txtItemName.Select();
            //        KeyEventArgs theArg = new KeyEventArgs(Keys.Enter);
            //        txtItemName_KeyUp(txtItemName, theArg);

            //    }
            //    else
            //    {
            //        KeyEventArgs theArg1 = new KeyEventArgs(Keys.Enter);
            //        txtItemName_KeyUp(txtItemName, theArg1);

            //        txtBatchNo.Text = theDV[0]["BatchNo"].ToString();
            //        theBatchId = Convert.ToInt32(theDV[0]["BatchId"]);
            //        txtExpirydate.Text = ((DateTime)theDV[0]["ExpiryDate"]).ToString(GblIQCare.AppDateFormat); 
            //        theSellingPrice = Convert.ToDecimal(theDV[0]["SellingPrice"]);
            //        theConfigSellingPrice = Convert.ToDecimal(theDV[0]["ConfigSellingPrice"]);
            //        theCostPrice = Convert.ToDecimal(theDV[0]["CostPrice"]);
            //        theMargin = Convert.ToDecimal(theDV[0]["DispensingMargin"]);
            //        theDispensingUnit = Convert.ToInt32(theDV[0]["DispensingId"]);
            //        theDispensingUnitName = theDV[0]["DispensingUnit"].ToString();
            //        theFunded = Convert.ToInt32(theDV[0]["Funded"]);
            //        theAvailQty = Convert.ToInt32(theDV[0]["AvailQty"]);
            //        theStrength = Convert.ToInt32(theDV[0]["StrengthId"]);
            //        theItemType = Convert.ToInt32(theDV[0]["DrugTypeId"]);

            //        if (grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[7].Value.ToString() != "")
            //        {
            //            if (!Convert.ToInt32(grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[7].Value).Equals(System.DBNull.Value))
            //            {
            //                txtDose.Text = Convert.ToDecimal(grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[7].Value).ToString();
            //            }
            //        }
            //        if (grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[9].Value.ToString() != "")
            //        {
            //            if (!Convert.ToInt32(grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[9].Value).Equals(System.DBNull.Value))
            //            {
            //                txtDuration.Text = Convert.ToDecimal(grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[9].Value).ToString();
            //            }
            //        }
            //        // txtDose.Text = Convert.ToDecimal(grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[7].Value).ToString();
            //        //  txtDuration.Text = Convert.ToDecimal(grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[9].Value).ToString();
            //        cmbFrequency.SelectedValue = grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[17].Value.ToString();

            //        if (grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[11].Value.ToString() != "0.00")
            //        {
            //            txtQtyDispensed.Text = Convert.ToDecimal(grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[11].Value).ToString();
            //        }

            //        txtQtyPrescribed.Text = Convert.ToDecimal(grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[10].Value).ToString();
            //        //if (txtQtyPrescribed.Text != "")
            //        //{
            //        //    txtQtyPrescribed.Enabled = false;
            //        //}


            //        if (grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[14].Value.ToString() != "")
            //        {
            //            if (!Convert.ToInt32(grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[14].Value).Equals(System.DBNull.Value))
            //            {
            //                txtSellingPrice.Text = Convert.ToDecimal(grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[14].Value).ToString();
            //            }
            //        }
            //        if (PresdispenceQty > totalOqdQty)
            //        {
            //            txtItemName.Enabled = true;
            //        }
            //        else
            //        {
            //            txtItemName.Enabled = false;
            //        }


            //        if (theItemType == 37 && cmbprogram.SelectedValue.ToString() == "223")
            //            theProphylaxis = 1;
            //        else
            //            theProphylaxis = 0;
            //        theGenericAbb = theDV[0]["GenericAbb"].ToString();

            //        if (grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[19].Value.ToString() != "0")
            //        {
            //            chkPrintPrescription.Checked = true;
            //        }
            //        else
            //        {
            //            chkPrintPrescription.Checked = false;
            //        }
            //        txtPatientInstructions.Text = grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[20].Value.ToString();
            //    }
            //}
        }

        private void grdReturnOrder_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grdReturnOrder.Rows[grdReturnOrder.CurrentRow.Index].Cells[1].Value.ToString() != "New Order")
            {
                theReturnOrderId = Convert.ToInt32(grdReturnOrder.Rows[grdReturnOrder.CurrentRow.Index].Cells[2].Value);
                IDrug thePharmacyManager = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
                DataSet theDS = thePharmacyManager.GetPharmacyExistingRecordDetails(theReturnOrderId);
                if (theDS.Tables[1].Rows.Count > 0)
                {
                    if (theDS.Tables[1].Rows[0]["DispensedByDate"].ToString() != "")
                    {
                        lblReturnDispensedDate.Text = ((DateTime)theDS.Tables[1].Rows[0]["DispensedByDate"]).ToString("dd-MMM-yyyy");
                    }

                    DataView theDV = new DataView(XMLDS.Tables["Mst_Decode"]);
                    theDV.RowFilter = "CodeId = 33 and (DeleteFlag = 0 or DeleteFlag is null) and Id = " + theDS.Tables[1].Rows[0]["ProgId"].ToString();
                    DataTable dtfilter = theDV.ToTable();
                    if (dtfilter.Rows.Count > 0)
                    {
                        lblReturnProgram.Text = theDV[0]["Name"].ToString();
                        //DataTable theReturnDT = theDS.Tables[0];

                    }
                    BindDrugRetunDetailGrid(theDS.Tables[0]);
                }
            }
        }

        private void BindDrugRetunDetailGrid(DataTable theDT)
        {
            grdReturnDetail.DataSource = null;
            grdReturnDetail.Columns.Clear();
            grdReturnDetail.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn theCol1 = new DataGridViewTextBoxColumn();
            theCol1.HeaderText = "ItemId";
            theCol1.DataPropertyName = "ItemId";
            theCol1.Width = 10;
            theCol1.Visible = false;

            DataGridViewTextBoxColumn theCol2 = new DataGridViewTextBoxColumn();
            theCol2.HeaderText = "Drug Name";
            theCol2.DataPropertyName = "ItemName";
            theCol2.Width = 200;
            theCol2.ReadOnly = true;

            DataGridViewTextBoxColumn theCol3 = new DataGridViewTextBoxColumn();
            theCol3.HeaderText = "DispUnitId";
            theCol3.DataPropertyName = "DispensingUnitId";
            theCol3.Width = 10;
            theCol3.Visible = false;

            DataGridViewTextBoxColumn theCol4 = new DataGridViewTextBoxColumn();
            theCol4.HeaderText = "Dispensing Unit";
            theCol4.DataPropertyName = "DispensingUnitName";
            theCol4.Width = 80;
            theCol4.ReadOnly = true;

            DataGridViewTextBoxColumn theCol5 = new DataGridViewTextBoxColumn();
            theCol5.HeaderText = "BatchId";
            theCol5.DataPropertyName = "BatchId";
            theCol5.Width = 10;
            theCol5.Visible = false;

            DataGridViewTextBoxColumn theCol6 = new DataGridViewTextBoxColumn();
            theCol6.HeaderText = "Batch No";
            theCol6.DataPropertyName = "BatchNo";
            theCol6.Width = 80;
            theCol6.ReadOnly = true;

            DataGridViewTextBoxColumn theCol7 = new DataGridViewTextBoxColumn();
            theCol7.HeaderText = "Expiry Date";
            theCol7.DataPropertyName = "ExpiryDate";
            theCol7.Width = 80;
            theCol7.ReadOnly = true;

            DataGridViewTextBoxColumn theCol8 = new DataGridViewTextBoxColumn();
            theCol8.HeaderText = "Quantity";
            theCol8.DataPropertyName = "QtyDisp";
            theCol8.Width = 80;
            theCol8.ReadOnly = true;

            DataGridViewTextBoxColumn theCol9 = new DataGridViewTextBoxColumn();
            theCol9.HeaderText = "CostPrice";
            theCol9.DataPropertyName = "CostPrice";
            theCol9.Width = 10;
            theCol9.Visible = false;

            DataGridViewTextBoxColumn theCol10 = new DataGridViewTextBoxColumn();
            theCol10.HeaderText = "Margin";
            theCol10.DataPropertyName = "Margin";
            theCol10.Width = 10;
            theCol10.Visible = false;

            DataGridViewTextBoxColumn theCol11 = new DataGridViewTextBoxColumn();
            theCol11.HeaderText = "Selling Price";
            theCol11.DataPropertyName = "SellingPrice";
            theCol11.Width = 80;
            theCol11.Visible = false;

            DataGridViewTextBoxColumn theCol12 = new DataGridViewTextBoxColumn();
            theCol12.HeaderText = "Bill Amount";
            theCol12.DataPropertyName = "BillAmount";
            theCol12.Width = 80;
            theCol12.Visible = false;

            DataGridViewTextBoxColumn theCol13 = new DataGridViewTextBoxColumn();
            theCol13.HeaderText = "StrengthId";
            theCol13.DataPropertyName = "StrengthId";
            theCol13.Width = 10;
            theCol13.Visible = false;

            DataGridViewTextBoxColumn theCol14 = new DataGridViewTextBoxColumn();
            theCol14.HeaderText = "Frequency";
            theCol14.DataPropertyName = "FrequencyId";
            theCol14.Width = 10;
            theCol14.Visible = false;

            DataGridViewTextBoxColumn theCol15 = new DataGridViewTextBoxColumn();
            theCol15.HeaderText = "Frequency";
            theCol15.DataPropertyName = "FrequencyName";
            theCol15.Width = 80;
            theCol15.Visible = false;

            DataGridViewTextBoxColumn theCol16 = new DataGridViewTextBoxColumn();
            theCol16.HeaderText = "Quantity Return";
            theCol16.DataPropertyName = "ReturnQty";
            theCol16.Width = 80;

            DataView theDV = new DataView(XMLDS.Tables["Mst_Decode"]);
            theDV.RowFilter = "CodeId = 204 and (DeleteFlag = 0 or DeleteFlag is null)";
            DataTable theReturnReasonDT = theDV.ToTable();
            DataGridViewComboBoxColumn theCol17 = new DataGridViewComboBoxColumn();
            theCol17.HeaderText = "Return Reason";
            theCol17.ValueMember = "Id";
            theCol17.DisplayMember = "Name";
            theCol17.DataSource = theReturnReasonDT;
            theCol17.DataPropertyName = "ReturnReason";
            theCol17.Width = 150;

            grdReturnDetail.Columns.Add(theCol1);
            grdReturnDetail.Columns.Add(theCol2);
            grdReturnDetail.Columns.Add(theCol3);
            grdReturnDetail.Columns.Add(theCol4);
            grdReturnDetail.Columns.Add(theCol5);
            grdReturnDetail.Columns.Add(theCol6);
            grdReturnDetail.Columns.Add(theCol7);
            grdReturnDetail.Columns.Add(theCol8);
            grdReturnDetail.Columns.Add(theCol15);
            grdReturnDetail.Columns.Add(theCol9);
            grdReturnDetail.Columns.Add(theCol10);
            grdReturnDetail.Columns.Add(theCol11);
            grdReturnDetail.Columns.Add(theCol12);
            grdReturnDetail.Columns.Add(theCol13);
            grdReturnDetail.Columns.Add(theCol14);
            grdReturnDetail.Columns.Add(theCol16);
            grdReturnDetail.Columns.Add(theCol17);

            grdReturnDetail.DataSource = theDT;

        }

        private void grdReturnDetail_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control.GetType().ToString() == "System.Windows.Forms.DataGridViewTextBoxEditingControl")
            {
                theCurrentRow = grdReturnDetail.CurrentRow.Index;
                theReturnQty = (TextBox)e.Control;
                theReturnQty.KeyUp += new KeyEventHandler(theReturnQty_KeyUp);
                theReturnQty.KeyPress += new KeyPressEventHandler(theReturnQty_KeyPress);
            }
        }

        void theReturnQty_KeyUp(object sender, KeyEventArgs e)
        {
            if (theReturnQty.Text != "")
            {
                if (Convert.ToInt32(grdReturnDetail.Rows[theCurrentRow].Cells[7].Value) < Convert.ToInt32(theReturnQty.Text))
                {
                    IQCareWindowMsgBox.ShowWindow("ReturnQtyGrtthanIssue", this);
                    theReturnQty.Text = "";
                    return;
                }
            }
        }

        void theReturnQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            BindFunctions theBindManager = new BindFunctions();
            theBindManager.Win_Numeric(e);
        }

        private void grdResultView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnHIVCareTrtPharFld_Click(object sender, EventArgs e)
        {
            HeightWeightPopUp = 1;
            grpHivCareTrtPharmacyField.Visible = false;
        }

        private void btnART_Click(object sender, EventArgs e)
        {
            grpHivCareTrtPharmacyField.Visible = true;

        }

        private void txtDose_KeyPress(object sender, KeyPressEventArgs e)
        {
            BindFunctions theBindManager = new BindFunctions();
            theBindManager.Win_Numeric(e);
        }

        private void txtDuration_KeyPress(object sender, KeyPressEventArgs e)
        {
            BindFunctions theBindManager = new BindFunctions();
            theBindManager.Win_Numeric(e);
        }

        private void txtQtyPrescribed_KeyPress(object sender, KeyPressEventArgs e)
        {
            BindFunctions theBindManager = new BindFunctions();
            theBindManager.Win_Numeric(e);
        }

        private void txtHeight_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtWeight.Text != "" && txtHeight.Text != "")
            {
                Double calVal = (Convert.ToDouble(txtWeight.Text) * Convert.ToDouble(txtHeight.Text) / 3600);
                calVal = (Double)Math.Sqrt(Convert.ToDouble(calVal));
                txtBSA.Text = Math.Round(calVal, 2).ToString();
            }
        }

        private void txtWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            BindFunctions theBindManager = new BindFunctions();
            theBindManager.Win_decimal(e);
        }

        private void txtHeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            BindFunctions theBindManager = new BindFunctions();
            theBindManager.Win_decimal(e);
        }

        private void txtWeight_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtWeight.Text != "" && txtHeight.Text != "")
            {
                Double calVal = (Convert.ToDouble(txtWeight.Text) * Convert.ToDouble(txtHeight.Text) / 3600);
                calVal = (Double)Math.Sqrt(Convert.ToDouble(calVal));
                txtBSA.Text = Math.Round(calVal, 2).ToString();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtItemName.Text = "";
            //txtDose.Text = "";
            //txtDuration.Text = "";
            //txtQtyPrescribed.Text = "";
            //txtQtyDispensed.Text = "";
            txtBatchNo.Text = "";
            txtExpirydate.Text = "";
            //txtSellingPrice.Text = "";
            //cmbFrequency.SelectedValue = "0";
            //chkPrintPrescription.Checked = false;
            //txtPatientInstructions.Text = "";
        }

        private void btnPatientClinicalSummary_Click(object sender, EventArgs e)
        {
            GblIQCare.patientID = thePatientId;
            Form theForm;
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmPatientClinicalSummary, IQCare.SCM"));
            //GblIQCare.theArea = "Dispense";
            theForm.Show();

            // frmPatientClinicalSummary frmPatientProfile = new frmPatientClinicalSummary();
            //frmPatientProfile.Show();
        }

        private void btnPharmacyNotes_Click(object sender, EventArgs e)
        {
            GblIQCare.patientID = thePatientId;
            Form theForm;
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmPharmacynotes, IQCare.SCM"));
            theForm.Show();
        }

        //john
        //private void txtDuration_Leave(object sender, EventArgs e)
        //{
        //    DataView theDV = new DataView();
        //    int intqtyprescribed = 0;
        //    string multiplier = string.Empty;
        //    theDV = new DataView(XMLPharDS.Tables["mst_Frequency"]);
        //    string strfrequency = cmbFrequency.SelectedValue.ToString();
        //    theDV.RowFilter = "ID='" + strfrequency.ToString() + "'";
        //    DataTable theDT = theDV.ToTable();
        //    if (theDT.Rows.Count > 0)
        //    {
        //        multiplier = theDT.Rows[0]["multiplier"].ToString();
        //        if (multiplier != "0" && txtDose.Text != "" && txtDuration.Text != "")
        //        {
        //            intqtyprescribed = Convert.ToInt32(txtDose.Text) * Convert.ToInt32(txtDuration.Text) * Convert.ToInt32(multiplier);
        //            txtQtyPrescribed.Text = intqtyprescribed.ToString();
        //        }
        //    }


        //}

        //private void btnHTPhaField_Click(object sender, EventArgs e)
        //{
        //    if (btnART.Enabled)
        //    {
        //        int val = theOrderId;
        //        if (theOrderId > 0)
        //        {
        //            SaveUpdateArt(theOrderId);
        //        }
        //    }
        //}
        public void SaveUpdateArt(int OrderID)
        {

            {
                IDrug thePharmacyManager1 = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
                string theDOB = "";
                string theWeight = "";
                string theHeight = "";
                if (NxtAppDate.CustomFormat == " ")
                    theDOB = "01-01-1900";
                else if (NxtAppDate.Text == "")
                {
                    theDOB = "01-01-1900";
                }
                else
                {
                    theDOB = NxtAppDate.Text;
                }

                if (txtWeight.Text == "")
                    theWeight = "0";
                else
                    theWeight = txtWeight.Text;

                if (txtHeight.Text == "")
                    theHeight = "0";
                else
                    theHeight = txtHeight.Text;

                DataSet theDS1 = thePharmacyManager1.SaveHivTreatementPharmacyField(OrderID, theWeight, theHeight, Convert.ToInt32(cmbprogram.SelectedValue), Convert.ToInt32(cmdPeriodTaken.SelectedValue), Convert.ToInt32(cmbProvider.SelectedValue), Convert.ToInt32(cmbRegimenLine.SelectedValue),0, Convert.ToDateTime(theDOB), Convert.ToInt32(cmbReason.SelectedValue));
                // IQCareWindowMsgBox.ShowWindow("ProgramSave", this); 
                return;
            }

        }

        public void SetPharmacyRefillApp()
        {
            
        }


        private void NxtAppDate_Enter(object sender, EventArgs e)
        {
            NxtAppDate.CustomFormat = "dd-MMM-yyyy";
        }

        private void btncopy_Click(object sender, EventArgs e)
        {
            Utility theUtil = new Utility();
            string urlpath = GblIQCare.weburl();
            ProcessStartInfo sInfo = new ProcessStartInfo(urlpath + "?loc=w&iqnum=" + theUtil.EncodeTo64(lblIQNumber.Text) + "&AppName=" + theUtil.EncodeTo64(GblIQCare.AppUName) + "&apploc=" + theUtil.EncodeTo64(GblIQCare.AppLocationId.ToString()) + "&sysid=" + theUtil.EncodeTo64(GblIQCare.SystemId.ToString()) + "");
            Process.Start(sInfo);

            //ShellWindows iExplorerInstances = new ShellWindows();
            //if (iExplorerInstances.Count > 0)
            //{
            //    IEnumerator enumerator = iExplorerInstances.GetEnumerator();
            //    enumerator.MoveNext();
            //    InternetExplorer iExplorer = (InternetExplorer)enumerator.Current;
            //    iExplorer.Navigate(urlpath + "?loc=w&iqnum=" + theUtil.EncodeTo64(lblIQNumber.Text) + "&AppName=" + theUtil.EncodeTo64(GblIQCare.AppUName) + "&apploc=" + theUtil.EncodeTo64(GblIQCare.AppLocationId.ToString()) + "&sysid=" + theUtil.EncodeTo64(GblIQCare.SystemId.ToString()) + "", 0x800); //0x800 means new tab
            //}
            //else
            //{
            //    ProcessStartInfo sInfo = new ProcessStartInfo(urlpath + "?loc=w&iqnum=" + theUtil.EncodeTo64(lblIQNumber.Text) + "&AppName=" + theUtil.EncodeTo64(GblIQCare.AppUName) + "&apploc=" + theUtil.EncodeTo64(GblIQCare.AppLocationId.ToString()) + "&sysid=" + theUtil.EncodeTo64(GblIQCare.SystemId.ToString()) + "");
            //    Process.Start(sInfo);
            //}

           

            
        }

        //john
        //private void chkPrintPrescription_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (chkPrintPrescription.Checked)
        //    {
        //        txtPatientInstructions.Enabled = true;
        //    }
        //    else
        //    {
        //        txtPatientInstructions.Text = "";
        //        txtPatientInstructions.Enabled = false;
        //    }
        //}

       

        private void cmdPrintPrescription_Click(object sender, EventArgs e)
        {
            try
            {
                //Cells[19].Value.ToString() != "0"
                int ptnPharmacyPK=0;
                DataSet theDS=new DataSet();
                IDrug thePharmacyManager = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
                DataTable theSrcDT = (DataTable)grdDrugDispense.DataSource;
                int PrintStatus = 0;
                foreach (DataRow theDR in theSrcDT.Rows)
                {

                    if (theDR["PrintPrescriptionStatus"].ToString() == "1")
                    {
                        PrintStatus = 1; 
                    }
                }
                if (PrintStatus == 0)
                {
                    IQCareWindowMsgBox.ShowWindow("PrintPresCheck", this);
                    return;
                }
                //if (theOrderStatus != "Already Dispensed Order")
                //{
                    if (tabDispense.TabPages[1].Focus() == true)
                    {

                        //DataSet thsdsval = thePharmacyManager.CheckDispencedDate(thePatientId, GblIQCare.AppLocationId, Convert.ToDateTime(dtDispensedDate.Text), theOrderId);
                        //if (thsdsval.Tables[0].Rows.Count > 0)
                        //{
                        //    string GetStatus = thsdsval.Tables[0].Rows[0]["Status"].ToString();
                        //    if (GetStatus == "1")
                        //    {
                        //        IQCareWindowMsgBox.ShowWindow("DispenceCheck", this);
                        //        return;
                        //    }
                        //}


                        //string theRegimen = "";
                        //Int32 theProgId = 0;
                        //Int32 theAge = Convert.ToDateTime(dtDispensedDate.Text).Year - theDOB.Year;
                        //if (theAge > 15)
                        //    theProgId = 116;
                        //else
                        //    theProgId = 117;

                        //foreach (DataRow theDR in theDT.Rows)
                        //{
                        //    if (theDR["ItemType"].ToString() == "37" && theDR["Prophylaxis"].ToString() != "1")
                        //    {
                        //        if (theRegimen == "" && theDR["GenericAbb"].ToString() != "")
                        //            theRegimen = theRegimen + theDR["GenericAbb"].ToString();
                        //        else if (theRegimen != "" && theDR["GenericAbb"].ToString() != "")
                        //            theRegimen = theRegimen + "/" + theDR["GenericAbb"].ToString();
                        //    }
                        //}
                        //DataTable dtPharmacyDetails = thePharmacyManager.SavePharmacyDispense(thePatientId, GblIQCare.AppLocationId, GblIQCare.intStoreId, GblIQCare.AppUserId,
                        //     Convert.ToDateTime(dtDispensedDate.Text), theProgId, Convert.ToInt32(cmbprogram.SelectedValue), theRegimen, theOrderId, theDT);
                        //ptnPharmacyPK = Convert.ToInt32(dtPharmacyDetails.Rows[0]["Ptn_Pharmacy_Pk"].ToString());
                        //if (btnART.Enabled)
                        //{
                        //    int val = theOrderId;
                        //    SaveUpdateArt(ptnPharmacyPK);

                        //}
                        /////IQCareWindowMsgBox.ShowWindow("PharmacyDispenseSave", this);
                        //DataTable theNewDT = CreatePharmacyDispenseTable();
                        //BindPharmacyDispenseGrid(theNewDT);
                        //grdDrugDispense.DataSource = theNewDT;

                    }
                    thePharmacyMaster = thePharmacyManager.GetPharmacyDispenseMasters(thePatientId, GblIQCare.intStoreId);

                    //btnART.Enabled = false;
                    grpBoxLastDispense.Visible = false;
                
               
             //// Add DataColumn in Existing Datatable////
            DataView theSrcDV = new DataView(theSrcDT);
            theSrcDV.RowFilter = "PrintPrescriptionStatus='1'";
            DataTable theDTs = theSrcDV.ToTable();
            theDTs.TableName = "Table";
            DataColumn ItemCodeColumn = new DataColumn("Item Code", typeof(System.String));
            ItemCodeColumn.DefaultValue = "";
            theDTs.Columns.Add(ItemCodeColumn);
            theDTs.AcceptChanges();
            theDS.Tables.Add(theDTs);
            //////    ///////////////////////////////////////////
            ptnPharmacyPK = theOrderId;
            XMLDS.ReadXml(GblIQCare.GetXMLPath() + "\\AllMasters.con");
            DataView theDV = new DataView(XMLDS.Tables["mst_Facility"]);
            theDV.RowFilter = "FacilityId=" + Convert.ToInt32(GblIQCare.AppLocationId);
            DataTable theDTMod = (DataTable)GblIQCare.dtModules;
            DataView theDVMod = new DataView(theDTMod);
            theDVMod.RowFilter = "ModuleId=" + Convert.ToInt32(GblIQCare.AppLocationId);
            DataSet thePharmDS = thePharmacyManager.GetPharmacyPrescriptionDetails(ptnPharmacyPK, Convert.ToInt32(thePatientId),0);
            for (int index = 0; index < thePharmDS.Tables.Count; index++)
            {
                ///Set the copy of source table in local instance
                DataTable tableToAdd = thePharmDS.Tables[index].Copy();
                int a = index + 1;
                tableToAdd.TableName = "Table" + a;
                ///Remove from source to avoid any exceptions
                //thePharmDS.Tables.RemoveAt(index);
                ///Add the copy to result set
                theDS.Tables.Add(tableToAdd);
                
            }
            //Image Streaming
                DataTable dtFacility = new DataTable();
                // object of data row
                DataRow drow = null;
                // add the column in table to store the image of Byte array type
                dtFacility.Columns.Add("FacilityImage", System.Type.GetType("System.Byte[]"));
                drow = dtFacility.NewRow();
                int ImageFlag = 0;
               
                // check the existance of image
                if (File.Exists(GblIQCare.PresentationImagePath() + ((theDS.Tables[3].Rows[0]["FacilityLogo"].ToString() !="")?theDS.Tables[3].Rows[0]["FacilityLogo"].ToString():"")))
                {
                    // define the filestream object to read the image
                    FileStream fs = default(FileStream);
                    // define te binary reader to read the bytes of image
                    BinaryReader br = default(BinaryReader);
                    // open image in file stream
                    fs = new FileStream(GblIQCare.PresentationImagePath() + theDS.Tables[3].Rows[0]["FacilityLogo"].ToString(), FileMode.Open);
                    
                    // initialise the binary reader from file streamobject
                    br = new BinaryReader(fs);
                    // define the byte array of filelength
                    byte[] imgbyte = new byte[fs.Length + 1];
                    // read the bytes from the binary reader
                    imgbyte = br.ReadBytes(Convert.ToInt32((fs.Length)));
                    drow[0] = imgbyte;
                    // add the image in bytearray
                    dtFacility.Rows.Add(drow);
                    ImageFlag = 1;
                    // add row into the datatable
                    br.Close();
                    // close the binary reader
                    fs.Close();
                    // close the file stream
                }
                
                theDS.Tables.Add(dtFacility);
                ////////////////////////////////////////

                //john 16th Oct 2013
                DataTable dtPersonDispensing = new DataTable();
                //Updated by -Nidhi 
                //Updated Date-15 Apr,2014
                //Desc- if the table is not null then exceute the inner loop else show the blank data in report  
                if (theDS.Tables[6] != null && theDS.Tables[6].Rows.Count > 0)
                {
                    //this is created by john 
                    //dtPersonDispensing = thePharmacyManager.GetPersonDispensingDrugs(thePharmDS.Tables[6].Rows[0][0].ToString());

                    //copied all the tables into theDs dataset object by manually created tables name like table1,table2 till table7
                    dtPersonDispensing = thePharmacyManager.GetPersonDispensingDrugs(theDS.Tables[6].Rows[0][0].ToString());
                    dtPersonDispensing.TableName = "PersonDispensingDrugs";
                    theDS.Tables.Add(dtPersonDispensing);
                }
                //
                
                theDS.WriteXmlSchema(GblIQCare.GetXMLPath() + "\\PatientPharmacyPrescription.xml");
                frmReportViewer theRepViewer = new frmReportViewer();
                theRepViewer.MdiParent = this.MdiParent;
                theRepViewer.Location = new Point(0, 0);
                if (theDS.Tables[3].Rows[0]["FacilityTemplate"].ToString() == "1")
                {

                    rptKNHPrescription rpt = new rptKNHPrescription();
                    rpt.SetDataSource(theDS);
                    rpt.SetParameterValue("EnrollmentID", "");
                    rpt.SetParameterValue("PharmacyID", ptnPharmacyPK.ToString());
                    rpt.SetParameterValue("ModuleName", "");
                    rpt.SetParameterValue("Currency", getCurrency().ToString());
                    rpt.SetParameterValue("FacilityName", GblIQCare.AppLocation.ToString());
                    rpt.SetParameterValue("Imageflag", ImageFlag.ToString());
                    theRepViewer.crViewer.ReportSource = rpt;
                    
                }
                else
                {

                    rptSimplePrescription rpt = new rptSimplePrescription();
                    rpt.SetDataSource(theDS);
                    rpt.SetParameterValue("EnrollmentID", "");
                    rpt.SetParameterValue("ModuleName", "");
                    rpt.SetParameterValue("Currency", getCurrency().ToString());
                    rpt.SetParameterValue("FacilityName", GblIQCare.AppLocation.ToString());
                    rpt.SetParameterValue("Imageflag", ImageFlag.ToString());
                    theRepViewer.crViewer.ReportSource = rpt;     
                    
                }
                if (theOrderId > 0)
                {
                    thePharmacyManager = null;
                    theRepViewer.Show();
                    //this.Close();
                }
                //--Jayant
                else
                {
                    IQCareWindowMsgBox.ShowWindow("DrugSaveCheck", this);
                    return; 
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }

        }

        private string getCurrency()
        {
            System.Data.DataSet theDS = new System.Data.DataSet();
            theDS.ReadXml(GblIQCare.GetXMLPath() + "\\Currency.xml");
            DataView theCurrDV = new DataView(theDS.Tables[0]);
            theCurrDV.RowFilter = "Id=" + Convert.ToInt32(GblIQCare.AppCountryId);
            string thestringCurrency = theCurrDV[0]["Name"].ToString();
            return thestringCurrency.Substring(thestringCurrency.LastIndexOf("(") + 1, 3);

        }

        private void label31_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        //john start
        private void cmbGrdDrugDispense_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            this.grdDrugDispense.CurrentCell.Value = cmbGrdDrugDispense.Text;
            this.cmbGrdDrugDispense.Hide();

            DataView theDV = thePharmacyMaster.Tables[1].DefaultView;
            theDV.RowFilter = "Drug_Pk = '" + grdDrugDispense.Rows[grdDrugDispense.SelectedCells[0].RowIndex].Cells[0].Value.ToString() + "' and BatchQty ='" + grdDrugDispense.Rows[grdDrugDispense.SelectedCells[0].RowIndex].Cells[5].Value.ToString() + "' and AvailQty is not null";
            DataTable qtyAvailableDT = theDV.ToTable();
            if (qtyAvailableDT.Rows.Count != 0)
            {
                qtyAvailableInBatch = Convert.ToDouble(qtyAvailableDT.Rows[0][9].ToString());
                //KNHsellingPrice = Convert.ToDouble(qtyAvailableDT.Rows[0][6].ToString());
                //KNHexpiryDate = ((DateTime)qtyAvailableDT.Rows[0][8]).ToString(GblIQCare.AppDateFormat);
                grdDrugDispense.Rows[grdDrugDispense.SelectedCells[0].RowIndex].Cells["ExpiryDate"].Value = ((DateTime)qtyAvailableDT.Rows[0][8]).ToString(GblIQCare.AppDateFormat); ;
                //MessageBox.Show(((DateTime)qtyAvailableDT.Rows[0][8]).ToString(GblIQCare.AppDateFormat));
                //txtExpirydate.Text = ((DateTime)theDV[0]["ExpiryDate"]).ToString(GblIQCare.AppDateFormat);
                //MessageBox.Show("Available " + qtyAvailable.ToString() + "sellingPrice " + sellingPrice.ToString() + "ExpiryDate " + expiryDate);
            }
            
        }

        private void cmbGrdDrugDispenseFreq_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            this.grdDrugDispense.CurrentCell.Value = cmbGrdDrugDispenseFreq.Text;
            this.cmbGrdDrugDispenseFreq.Hide();
            //MessageBox.Show(cmbGrdDrugDispenseFreq.SelectedValue.ToString());
            DataView theDVFreq = XMLPharDS.Tables["mst_Frequency"].DefaultView;
            string x = cmbGrdDrugDispenseFreq.SelectedValue.ToString();
            int value;
            if (int.TryParse(x, out value))
                theDVFreq.RowFilter = "Id = " + value;
            
            DataTable freqDT = theDVFreq.ToTable();
            if (freqDT.Rows.Count != 0)
            {
                grdDrugDispense.Rows[grdDrugDispense.SelectedCells[0].RowIndex].Cells["freqMultiplier"].Value = Convert.ToInt32(freqDT.Rows[0]["multiplier"].ToString());
                grdDrugDispense.Rows[grdDrugDispense.SelectedCells[0].RowIndex].Cells["FrequencyId"].Value = Convert.ToInt32(freqDT.Rows[0]["ID"].ToString());
                if (grdDrugDispense.Rows[grdDrugDispense.SelectedCells[0].RowIndex].Cells[7].Value.ToString() != "" && grdDrugDispense.Rows[grdDrugDispense.SelectedCells[0].RowIndex].Cells[22].Value.ToString() != "" && grdDrugDispense.Rows[grdDrugDispense.SelectedCells[0].RowIndex].Cells[9].Value.ToString() != "")
                {
                    grdDrugDispense.Rows[grdDrugDispense.SelectedCells[0].RowIndex].Cells[10].Value = Convert.ToDouble(grdDrugDispense.Rows[grdDrugDispense.SelectedCells[0].RowIndex].Cells[7].Value.ToString()) * Convert.ToDouble(grdDrugDispense.Rows[grdDrugDispense.SelectedCells[0].RowIndex].Cells[22].Value.ToString()) * Convert.ToDouble(grdDrugDispense.Rows[grdDrugDispense.SelectedCells[0].RowIndex].Cells[9].Value.ToString());
                }
                //MessageBox.Show(grdDrugDispense.Rows[grdDrugDispense.SelectedCells[0].RowIndex].Cells["FrequencyId"].Value.ToString());
            }
            if (grdDrugDispense.Rows[grdDrugDispense.SelectedCells[0].RowIndex].Cells["QtyDisp"].Value.ToString() != "" && grdDrugDispense.Rows[grdDrugDispense.SelectedCells[0].RowIndex].Cells["QtyPres"].Value.ToString() != "")
            {
                if (Convert.ToDouble(grdDrugDispense.Rows[grdDrugDispense.SelectedCells[0].RowIndex].Cells["QtyDisp"].Value.ToString()) > Convert.ToDouble(grdDrugDispense.Rows[grdDrugDispense.SelectedCells[0].RowIndex].Cells["QtyPres"].Value.ToString()))
                {
                    if (MessageBox.Show("You have entered Dispensed Qty more than the Prescribed Qty" +
                                "\nDo you want to Continue?", "IQCare Management", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel)
                    {
                        grdDrugDispense.Rows[grdDrugDispense.SelectedCells[0].RowIndex].Cells["QtyDisp"].Value = 0;
                        grdDrugDispense.Rows[grdDrugDispense.SelectedCells[0].RowIndex].Cells["QtyDisp"].Selected = true;
                        return;
                    }

                    //MessageBox.Show("Quantity Dispensed is greater than Quantity Prescribed!");
                    //grdDrugDispense.Rows[grdDrugDispense.SelectedCells[0].RowIndex].Cells["QtyDisp"].Value = 0;
                    //grdDrugDispense.Rows[grdDrugDispense.SelectedCells[0].RowIndex].Cells["QtyDisp"].Selected = true;
                }
            }
        }

        private void grdDrugDispense_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 11 || e.ColumnIndex == 9 || e.ColumnIndex == 7)
            {
                if (grdDrugDispense.Rows[e.RowIndex].Cells["BatchNo"].Value.ToString() != "")
                {
                    if (grdDrugDispense.Rows[e.RowIndex].Cells["Dose"].Value.ToString() != "" && grdDrugDispense.Rows[e.RowIndex].Cells["freqMultiplier"].Value.ToString() != "" && grdDrugDispense.Rows[e.RowIndex].Cells["Duration"].Value.ToString() != "")
                    {
                        grdDrugDispense.Rows[e.RowIndex].Cells["QtyPres"].Value = Convert.ToDouble(grdDrugDispense.Rows[e.RowIndex].Cells["Dose"].Value.ToString()) * Convert.ToDouble(grdDrugDispense.Rows[e.RowIndex].Cells["freqMultiplier"].Value.ToString()) * Convert.ToDouble(grdDrugDispense.Rows[e.RowIndex].Cells["Duration"].Value.ToString());
                    }
                    else
                    {
                        grdDrugDispense.Rows[e.RowIndex].Cells["QtyPres"].Value = 0;
                    }

                    if (grdDrugDispense.Rows[e.RowIndex].Cells["QtyDisp"].Value.ToString() != "" && grdDrugDispense.Rows[e.RowIndex].Cells["QtyPres"].Value.ToString() != "")
                    {
                        if (Convert.ToDouble(grdDrugDispense.Rows[e.RowIndex].Cells["QtyDisp"].Value.ToString()) > Convert.ToDouble(grdDrugDispense.Rows[e.RowIndex].Cells["QtyPres"].Value.ToString()))
                        {

                            if (MessageBox.Show("You have entered Dispensed Qty more than the Prescribed Qty" +
                                "\nDo you want to Continue?", "IQCare Management", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel)
                            {
                                grdDrugDispense.Rows[e.RowIndex].Cells["QtyDisp"].Value = 0;
                                grdDrugDispense.Rows[e.RowIndex].Cells["QtyDisp"].Selected = true;
                                return;
                            }
                            

                            //MessageBox.Show("Quantity Dispensed is greater than Quantity Prescribed!");
                            //grdDrugDispense.Rows[e.RowIndex].Cells["QtyDisp"].Value = 0;
                            //grdDrugDispense.Rows[e.RowIndex].Cells["QtyDisp"].Selected = true;
                        }
                        else if (Convert.ToDouble(grdDrugDispense.Rows[e.RowIndex].Cells["QtyDisp"].Value.ToString()) > qtyAvailableInBatch)
                        {
                            MessageBox.Show("You have entered Dispensed Qty more than the Qty available in Batch!");
                            grdDrugDispense.Rows[e.RowIndex].Cells["QtyDisp"].Value = qtyAvailableInBatch;
                            //grdDrugDispense.Rows[e.RowIndex].Cells["QtyDisp"].Selected = true;
                            return;
                        }
                        
                        double TotalsellingPrice = Convert.ToDouble(grdDrugDispense.Rows[e.RowIndex].Cells["UnitSellingPrice"].Value.ToString()) * Convert.ToDouble(grdDrugDispense.Rows[e.RowIndex].Cells["QtyDisp"].Value.ToString());
                        grdDrugDispense.Rows[e.RowIndex].Cells["SellingPrice"].Value = TotalsellingPrice.ToString();
                        if (theFunded == 0)
                        {
                            grdDrugDispense.Rows[e.RowIndex].Cells["BillAmount"].Value = TotalsellingPrice.ToString();
                            theBillAmt = Convert.ToDecimal(TotalsellingPrice);
                            lblPayAmount.Text = (Convert.ToDecimal(lblPayAmount.Text) + theBillAmt).ToString();
                        }
                        else
                        {
                            grdDrugDispense.Rows[e.RowIndex].Cells["BillAmount"].Value = "0";
                            theBillAmt = 0;
                            lblPayAmount.Text = (Convert.ToDecimal(lblPayAmount.Text) + theBillAmt).ToString();
                        }

                        //MessageBox.Show(grdDrugDispense.Rows[e.RowIndex].Cells["UnitSellingPrice"].Value.ToString() + " " + grdDrugDispense.Rows[e.RowIndex].Cells["QtyDisp"].Value.ToString());
                        
                    }
                }
                else
                {
                    MessageBox.Show("Batch Number cannot be empty!");
                    //grdDrugDispense.Rows[e.RowIndex].Cells["Dose"].Value = 0;
                    //grdDrugDispense.Rows[e.RowIndex].Cells["Dose"].Selected = true;
                    //grdDrugDispense.Rows[e.RowIndex].Cells["Duration"].Value = 0;
                    //grdDrugDispense.Rows[e.RowIndex].Cells["Duration"].Selected = true;
                }
            }
        }

        private void grdDrugDispense_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == 19)
            //{
            //    grdDrugDispense.EndEdit();
            //    GblIQCare.PatientInstructions = grdDrugDispense.Rows[e.RowIndex].Cells["PatientInstructions"].Value.ToString();
            //    if (grdDrugDispense.Rows[e.RowIndex].Cells[19].Value.ToString() == "1")
            //    {
                    
            //        frmPatientInstruction frm = new frmPatientInstruction();
            //        frm.ShowDialog();
            //        grdDrugDispense.Rows[e.RowIndex].Cells["PatientInstructions"].Value = GblIQCare.PatientInstructions;
            //    }
            //    else
            //    {
            //        grdDrugDispense.Rows[e.RowIndex].Cells["PatientInstructions"].Value = GblIQCare.PatientInstructions;
            //    }

            //}
        }

        private void grdDrugDispense_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.cmbGrdDrugDispense.Hide();
            this.cmbGrdDrugDispenseFreq.Hide();
            
            if (grdDrugDispense.SelectedCells[0].ColumnIndex == 5)
            {
                if (makeGridEditable == "Yes")
                {
                    DataView dv = thePharmacyMaster.Tables[1].DefaultView;
                    //MessageBox.Show(dv.ToTable().Rows[0][1].ToString());
                    dv.RowFilter = "Drug_Pk = '" + grdDrugDispense.Rows[grdDrugDispense.SelectedCells[0].RowIndex].Cells[0].Value.ToString() + "' and BatchNo <> ''";

                    cmbGrdDrugDispense.DataSource = dv.ToTable();
                    cmbGrdDrugDispense.DisplayMember = "BatchQty";
                    cmbGrdDrugDispense.ValueMember = "BatchId";
                    cmbGrdDrugDispense.Location = grdDrugDispense.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                    //cmbAutoGrid.SelectedValue = grdDrugDispense.CurrentCell.Value;
                    cmbGrdDrugDispense.Width = grdDrugDispense.CurrentCell.Size.Width;
                    cmbGrdDrugDispense.Show();
                    //clsGlobal.iStudentID = Convert.ToInt32(dgvStudents.Rows[dgvStudents.SelectedCells[0].RowIndex].Cells[7].Value);
                    //clsGlobal.sStudentName = dgvStudents.Rows[dgvStudents.SelectedCells[0].RowIndex].Cells[8].Value.ToString();
                    //clsGlobal.sAdmNo = dgvStudents.Rows[dgvStudents.SelectedCells[0].RowIndex].Cells[0].Value.ToString();
                }
            }

            if (grdDrugDispense.SelectedCells[0].ColumnIndex == 8)
            {
                if (makeGridEditable == "Yes")
                {
                    fill_DrugFreq();
                    cmbGrdDrugDispenseFreq.Location = grdDrugDispense.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                    cmbGrdDrugDispenseFreq.Width = grdDrugDispense.CurrentCell.Size.Width;
                    cmbGrdDrugDispenseFreq.Show();
                }
            }
        }

        public void fill_DrugFreq()
        {
            XMLPharDS.Clear();
            XMLPharDS.ReadXml(GblIQCare.GetXMLPath() + "\\DrugMasters.con");
            BindFunctions theBindManager1 = new BindFunctions();
            DataView theDV1 = new DataView(XMLPharDS.Tables["mst_Frequency"]);
            theDV1.RowFilter = "(DeleteFlag =0 or DeleteFlag is null)";
            DataTable theDT1 = theDV1.ToTable();
            theBindManager1.Win_BindCombo(cmbGrdDrugDispenseFreq, theDT1, "Name", "Id");
        }

        private void grdDrugDispense_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                e.Control.KeyPress += new KeyPressEventHandler(Control_KeyPress);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void dtRefillApp_Enter(object sender, EventArgs e)
        {
            dtRefillApp.CustomFormat = "dd-MMM-yyyy";
        }

        private void chkPharmacyRefill_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPharmacyRefill.Checked == false)
            {
                dtRefillApp.CustomFormat = " ";
                dtRefillApp.Enabled = false;
            }
            else
            {
                dtRefillApp.CustomFormat = "dd-MMM-yyyy";
                dtRefillApp.Enabled = true;
            }
        }
        private void btnPrintLabel_Click(object sender, EventArgs e)
        {
            GblIQCare.dtPrintLabel = (DataTable)grdDrugDispense.DataSource;
            GblIQCare.PatientName =lblPatientName.Text;
            GblIQCare.IQNumber = lblIQNumber.Text;
            GblIQCare.StoreName = lblStoreName.Text;
            if (GblIQCare.dtPrintLabel.Rows.Count > 0 || GblIQCare.dtPrintLabel  == null)
            {
                Form theForm;
                theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmPrintLabel, IQCare.SCM"));
                theForm.MdiParent = this.MdiParent;
                theForm.Left = 0;
                theForm.Top = 0;
                theForm.Focus();
                theForm.Show();
               
            }
            else
            {
                MessageBox.Show("Please select drugs");
            }
        }

        private void dtDispensedDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtDispensedDate.Value > DateTime.Today)
            {
                MessageBox.Show("Dispensing date cannot be greater than current date.");
                dtDispensedDate.Value = DateTime.Today;
            }
        }

        private void cmbRegimenLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lastRegimenLine != 0)
            {
                if (cmbRegimenLine.SelectedIndex < lastRegimenLine)
                {
                    cmbRegimenLine.SelectedIndex = lastRegimenLine;
                    //MessageBox.Show("Cannot select Regimen Line Lower the current Regimen Line.");
                }
            }
        }
        //john end
    }

}
