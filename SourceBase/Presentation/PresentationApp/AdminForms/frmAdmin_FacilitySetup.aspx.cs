using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using Application.Common;
using Application.Presentation;
using Interface.Administration;
using Interface.Security;
using Interface.Service;
using System.Web.Configuration;



public partial class frmAdmin_FacilitySetup : LogPage
{
    //int FacilityId;
    DataSet theFacilityDS;
    //DataTable dtModule;
    string thePepFarDate = "";
    string theFName = "";
    int Preferred;
    int ProvinceId = 0;
    int DistrictId = 0;
    int CountryId = 0;
    Boolean flag, storeFlag;
    #region "User Functions"
    private void Init_Form()
    {
        //txtcountryno.Attributes.Add("onkeyup", "chkPostiveInteger('" + txtcountryno.ClientID + "')");
        //txtcountryno.Attributes.Add("onblur", "chkPostiveInteger('" + txtcountryno.ClientID + "')");

        txtLPTF.Attributes.Add("onkeyup", "chkPostiveInteger('" + txtLPTF.ClientID + "')");
        txtLPTF.Attributes.Add("onblur", "chkPostiveInteger('" + txtLPTF.ClientID + "')");

        txtSatelliteID.Attributes.Add("onkeyup", "chkPostiveInteger('" + txtSatelliteID.ClientID + "')");
        txtSatelliteID.Attributes.Add("onblur", "chkPostiveInteger('" + txtSatelliteID.ClientID + "')");
        //txtNationalId.Attributes.Add("onKeyup", "chkNumeric('" + txtNationalId.ClientID + "')");



        txtfacilityname.Text = "";
        //txtcountryno.Text = "";
        txtLPTF.Text = "";
        txtSatelliteID.Text = "";
        txtGrace.Text = "";
        txtPEPFAR_Fund.Value = "";
        cmbCurrency.SelectedValue = "0";
        if (Session["SystemId"].ToString() == "2")
        {
            paperless.Visible = false;
        }
        //BindCombo();
        //ddBackupTime.SelectedValue = "0";


        BindFunctions BindManager = new BindFunctions();
        IQCareUtils theUtils = new IQCareUtils();
        DataSet theDSXML = new DataSet();
        theDSXML.ReadXml(MapPath("..\\XMLFiles\\AllMasters.con"));

        DataView theDV;
        DataTable theDT = new DataTable();

        //theDV = new DataView(theDSXML.Tables["Mst_District"]);
        ICommonData oCommonData = (ICommonData)ObjectFactory.CreateInstance("BusinessProcess.Service.BCommonData,BusinessProcess.Service");

        theDT = (DataTable)oCommonData.getAllCountries();
        //theDV.RowFilter = "SystemID=" + Session["SystemId"] + "";
        if (theDT != null && theDT.Rows.Count > 0)
        {
            //theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            BindManager.BindCombo(ddCountry, theDT, "CountryName", "CountryID");
            //theDV.Dispose();
            theDT.Clear();
        }

        theDT = (DataTable)oCommonData.getAllProvince();
        //theDV.RowFilter = "SystemID=" + Session["SystemId"] + "";
        if (theDT != null && theDT.Rows.Count > 0)
        {
            //theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            BindManager.BindCombo(ddlprovince, theDT, "Name", "ID");
            //theDV.Dispose();
            theDT.Clear();
        }

        theDT = (DataTable)oCommonData.getAllDistrict();
        // theDV.RowFilter = "SystemID= " + Session["SystemId"] + "";
        if (theDT != null && theDT.Rows.Count > 0)
        {
            //theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            BindManager.BindCombo(ddldistrict, theDT, "Name", "ID");
            //theDV.Dispose();
            theDT.Clear();

        }



        /////////////////////////////////////////////////
        IFacilitySetup FacilityManager = (IFacilitySetup)ObjectFactory.CreateInstance("BusinessProcess.Administration.BFacility, BusinessProcess.Administration");
        DataSet theDSFacility = FacilityManager.GetModuleName();
        DataTable DT = theDSFacility.Tables[0];
        BindManager.BindCheckedList(cblPMTCT, DT, "modulename", "moduleid");

        // Binding Drug Stores
        DataSet theDSDrugStores = FacilityManager.GetDrugStores();
        BindManager.BindCheckedList(CBL_DrugStore, theDSDrugStores.Tables[0], "Name", "StoreId");
    }


    /// <summary>
    /// Handles the SelectedIndexChanged event of the ddlMainItem control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    /// <exception cref="System.NotImplementedException"></exception>
    void SelectedCountryItemTypeChanged(object sender, EventArgs e)
    {
        if (ddCountry.SelectedValue != string.Empty && int.Parse(ddCountry.SelectedValue) > 0)
        {
            BindFunctions BindManager = new BindFunctions();
            int countryID = int.Parse(ddCountry.SelectedValue);
            //if (ddCountry.SelectedItem.ToString() == "Kenya")
            //{
            //    Session["SystemId"] = "1";
            //    var configuration = WebConfigurationManager.OpenWebConfiguration("~");
            //    configuration.AppSettings.Settings["SystemId"].Value = Session["SystemId"].ToString();
            //    configuration.Save();
            //}
            //else if (ddCountry.SelectedItem.ToString() == "Tanzania")
            //{
            //    Session["SystemId"] = "2";
            //    var configuration = WebConfigurationManager.OpenWebConfiguration("~");
            //    configuration.AppSettings.Settings["SystemId"].Value = Session["SystemId"].ToString();
            //    configuration.Save();
            //}
            //else if (ddCountry.SelectedItem.ToString() == "Niger" || ddCountry.SelectedItem.ToString() == "Nigeria")
            //{
            //    Session["SystemId"] = "3";
            //    var configuration = WebConfigurationManager.OpenWebConfiguration("~");
            //    configuration.AppSettings.Settings["SystemId"].Value = Session["SystemId"].ToString();
            //    configuration.Save();
            //}
            //else
            //{
            //    Session["SystemId"] = "1";
            //    var configuration = WebConfigurationManager.OpenWebConfiguration("~");
            //    configuration.AppSettings.Settings["SystemId"].Value = Session["SystemId"].ToString();
            //    configuration.Save();
            //}
            ICommonData oCommonData = (ICommonData)ObjectFactory.CreateInstance("BusinessProcess.Service.BCommonData,BusinessProcess.Service");
            DataTable theDT = (DataTable)oCommonData.getAllProvince();
            DataView theDV = theDT.DefaultView;
            theDV.RowFilter = "CountryID = " + countryID + "";
            if (theDV != null && theDV.Table.Rows.Count > 0)
            {
                BindManager.BindCombo(ddlprovince, theDV.ToTable(), "Name", "ID");
                theDT.Clear();
            }
        }
    }

    void SelectedProvinceItemTypeChanged(object sender, EventArgs e)
    {
        if (ddlprovince.SelectedValue != string.Empty && int.Parse(ddlprovince.SelectedValue) > 0)
        {
            BindFunctions BindManager = new BindFunctions();
            int provinceID = int.Parse(ddlprovince.SelectedValue);
            ICommonData oCommonData = (ICommonData)ObjectFactory.CreateInstance("BusinessProcess.Service.BCommonData,BusinessProcess.Service");
            DataTable theDT = (DataTable)oCommonData.getAllDistrict();
            DataView theDV = theDT.DefaultView;
            theDV.RowFilter = "ProvinceID = " + provinceID + "";
            if (theDV != null && theDV.Table.Rows.Count > 0)
            {
                BindManager.BindCombo(ddldistrict, theDV.ToTable(), "Name", "ID");
                theDT.Clear();
            }
        }
    }


    //private void BindCombo()
    //{
    //    IQCareUtils theUtil = new IQCareUtils();
    //    DataTable theDT = theUtil.CreateTimeTable(15);
    //    DataRow theDR = theDT.NewRow();
    //    theDR[0] = "0";
    //    theDR[1] = "Select";
    //    theDT.Rows.InsertAt(theDR, 0);
    //    ddBackupTime.DataSource = theDT;
    //    ddBackupTime.DataTextField = "Time";
    //    ddBackupTime.DataValueField = "Id";
    //    ddBackupTime.DataBind();
    //}

    private void Fill_Details()
    {
        string script;
        /*-------------------Dynamic Label Settings----------------------*/
        //DataTable theDT = ((DataSet)ViewState["FacilityDS"]).Tables[1];
        theFacilityDS = new DataSet();
        AuthenticationManager Authentication = new AuthenticationManager();
        IFacilitySetup FacilityMaster = (IFacilitySetup)ObjectFactory.CreateInstance("BusinessProcess.Administration.BFacility, BusinessProcess.Administration");

        theFacilityDS = FacilityMaster.GetFacilityList(Convert.ToInt32(Session["SystemId"]), ApplicationAccess.FacilitySetup, 0);

        DataTable theDT = theFacilityDS.Tables[1];
        //lblCountry.InnerText = theDT.Rows[0]["Label"].ToString() + ":";
        //lblPOS.InnerText = theDT.Rows[1]["Label"].ToString() + ":";
        //Comment above Line By Rahmat 16Feb18 for Facility set up - MFL Code
        lblSatellite.InnerText = theDT.Rows[2]["Label"].ToString() + ":";
        /*---------------------------------------------------------------*/

        if (Convert.ToInt32(ViewState["FacilityId"]) > 0)
        {
            DataView theDV = new DataView(((DataSet)ViewState["FacilityDS"]).Tables[0]);
            theDV.RowFilter = "FacilityId=" + ViewState["FacilityId"].ToString();
            txtfacilityname.Text = theDV[0]["FacilityName"].ToString();

            txtLPTF.Text = theDV[0]["PosId"].ToString();
            txtSatelliteID.Text = theDV[0]["SatelliteId"].ToString();
            //txtNationalId.Text = theDV[0]["NationalId"].ToString();
            txtGrace.Text = theDV[0]["AppGracePeriod"].ToString();
            ddCountry.SelectedValue = theDV[0]["CountryId"].ToString();
            //txtcountryno.Text = theDV[0]["CountryId"].ToString();
            ddlprovince.SelectedValue = theDV[0]["provinceId"].ToString();
            ddldistrict.SelectedValue = theDV[0]["DistrictId"].ToString();

            if (theDV[0]["PepFarStartDate"].ToString() != "")
                txtPEPFAR_Fund.Value = ((DateTime)theDV[0]["PepFarStartDate"]).ToString(Session["AppDateFormat"].ToString());
            cmbCurrency.SelectedValue = theDV[0]["Currency"].ToString();
            if (theDV[0]["Status"].ToString() == "Active")
                ddStatus.SelectedValue = "0";
            else if (theDV[0]["Status"].ToString() == "In-Active")
                ddStatus.SelectedValue = "1";
            if (theDV[0]["Preferred"].ToString() == "Yes")
                chkPreferred.Checked = true;
            else
                chkPreferred.Checked = false;

            if (theDV[0]["Paperless"].ToString() == "Yes")
            {
                chkPaperlessclinic.Checked = true;
                ViewState["Paperless"] = "1";
            }
            else
            {
                chkPaperlessclinic.Checked = false;
                ViewState["Paperless"] = "0";
            }
            if (theDV[0]["StrongPassFlag"].ToString() == "1")
            {
                chkStrongPwd.Checked = true;

            }
            else
            {
                chkStrongPwd.Checked = false;

            }
            if (theDV[0]["ExpPwdFlag"].ToString() == "1")
            {
                chkexpPwd.Checked = true;
                script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'ExpNoofDays_0'>\n";
                script += "show('divnoofdays');\n";
                script += "</script>\n";
                RegisterStartupScript("ExpNoofDays_0", script);
                txtnoofdays.Text = theDV[0]["ExpPwdDays"].ToString();

            }
            else
            {
                chkexpPwd.Checked = false;
                script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'ExpNoofDays_1'>\n";
                script += "hide('divnoofdays');\n";
                script += "</script>\n";
                RegisterStartupScript("ExpNoofDays_1", script);
                txtnoofdays.Text = "";

            }
            //deepika
            ddCountry.Enabled = true;
            //txtcountryno.Enabled = true;
            txtLPTF.Enabled = true;
            txtSatelliteID.Enabled = true;
            //Facility Address
            txtFacAddress.Text = theDV[0]["FacilityAddress"].ToString();
            txtFacCell.Text = theDV[0]["FacilityCell"].ToString();
            txtFacEmail.Text = theDV[0]["FacilityEmail"].ToString();
            txtFacFax.Text = theDV[0]["FacilityFax"].ToString();
            txtFactele.Text = theDV[0]["FacilityTel"].ToString();
            txtFacURL.Text = theDV[0]["FacilityURL"].ToString();
            txtpharmfoottext.Text = theDV[0]["FacilityFooter"].ToString();

            if (theDV[0]["FacilityTemplate"].ToString() == "0")
            {
                Radio1.Checked = true;

            }
            else if (theDV[0]["FacilityTemplate"].ToString() == "1")
            {
                Radio2.Checked = true;
            }

            if (theDV[0]["DateConstraint"].ToString() == "1")
            {
                chkdateconstraint.Checked = true;

            }
            else
            {
                chkdateconstraint.Checked = false;

            }
            //Billing
            if (theDV[0]["Billing"].ToString() == "1")
            {
                chkbilling.Checked = true;

            }
            else
            {
                chkbilling.Checked = false;

            }
            //PMSCM
            if (theDV[0]["PMSCM"].ToString() == "1")
            {
                chkpmscm.Checked = true;

            }
            else
            {
                chkpmscm.Checked = false;

            }
            //Wards
            chkwards.Checked = (theDV[0]["Wards"].ToString() == "1");
            chkLMIS.Checked = (theDV[0]["LMIS"].ToString() == "1");

            //PMTCT Binding
            string strExpr;
            DataRow[] foundRows;
            DataView theDVmdl = new DataView(((DataSet)ViewState["FacilityDS"]).Tables[2]);
            DataView dvStore = new DataView(((DataSet)ViewState["FacilityDS"]).Tables[3]);
            strExpr = "FacilityId=" + ViewState["FacilityId"].ToString();
            foundRows = theDVmdl.Table.Select(strExpr);
            if (foundRows.GetUpperBound(0) != -1)
            {
                foreach (DataRow dr in foundRows)
                {
                    for (int i = 0; i < cblPMTCT.Items.Count; i++)
                    {
                        if (cblPMTCT.Items[i].Value == dr[1].ToString())
                        {
                            cblPMTCT.Items[i].Selected = true;
                        }

                    }
                }
            }
            // Already link stores with this facility
            foundRows = null;
            foundRows = dvStore.Table.Select(strExpr);
            if (foundRows.GetUpperBound(0) != -1)
            {
                foreach (DataRow dr in foundRows)
                {
                    for (int i = 0; i < CBL_DrugStore.Items.Count; i++)
                    {
                        if (CBL_DrugStore.Items[i].Value == dr["StoreID"].ToString())
                        {
                            CBL_DrugStore.Items[i].Selected = true;
                        }

                    }
                }
            }
        }

        //IFacilitySetup FacilityManager;
        //FacilityManager = (IFacilitySetup)ObjectFactory.CreateInstance("BusinessProcess.Administration.BFacility, BusinessProcess.Administration");
        //DataSet theDS = FacilityManager.GetFacility();
        //if (theDS.Tables[0].Rows.Count > 0)
        //{
        //    FacilityId = Convert.ToInt32(theDS.Tables[0].Rows[0]["FacilityID"]);
        //    txtfacilityname.Text = theDS.Tables[0].Rows[0]["FacilityName"].ToString();
        //    txtcountryno.Text = theDS.Tables[0].Rows[0]["CountryID"].ToString();
        //    txtLPTF.Text = theDS.Tables[0].Rows[0]["PosID"].ToString();
        //    txtSatelliteID.Text = theDS.Tables[0].Rows[0]["SatelliteID"].ToString();
        //   // ddsatelliteloc.SelectedValue = theDS.Tables[0].Rows[0]["SatelliteID"].ToString();
        //    txtGrace.Text = theDS.Tables[0].Rows[0]["AppGracePeriod"].ToString();
        //    cmbCurrency.SelectedValue = theDS.Tables[0].Rows[0]["Currency"].ToString();
        //    DateTime Pepfardate;
        //    if (theDS.Tables[0].Rows[0]["PepFarStartDate"] != System.DBNull.Value)
        //    {
        //        Pepfardate = Convert.ToDateTime(theDS.Tables[0].Rows[0]["PepFarStartDate"]);
        //        txtPEPFAR_Fund.Value = Pepfardate.ToString(Session["AppDateFormat"].ToString());
        //    }

        //    ddBackupDrive.SelectedValue = theDS.Tables[0].Rows[0]["BackupDrive"].ToString();
        //    //if (theDS.Tables[0].Rows[0]["BackupTime"].ToString() == "00:00:00")
        //    if (theDS.Tables[0].Rows[0]["BackupTime"].ToString() == "")
        //    {
        //        ddBackupTime.SelectedValue = "Select";
        //    }
        //    else { ddBackupTime.SelectedValue = theDS.Tables[0].Rows[0]["BackupTime"].ToString(); }
        //    //FileLoad.Value = theDS.Tables[0].Rows[0]["Image"].ToString();
        //    ViewState["LoginImage"] = theDS.Tables[0].Rows[0]["Image"].ToString();
        //}
        //else
        //{
        //    Session["AppUserId"] = "1";
        //}
    }
    public bool IsValidEmailAddress(string s)
    {
        if (string.IsNullOrEmpty(s))
            return false;
        else
        {
            var regex = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            return regex.IsMatch(s) && !s.EndsWith(".");
        }
    }
    private Boolean FieldValidation()
    {
        if (txtfacilityname.Text.Trim() == "")
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "Facility Name";
            IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
            txtfacilityname.Focus();
            return false;
        }

        if (Convert.ToInt32(ddCountry.SelectedValue) < 1)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "Country";
            IQCareMsgBox.Show("BlankDropDown", theBuilder, this);
            ddCountry.Focus();
            return false;
        }

        //if (txtcountryno.Text.Trim() == "")
        //{
        //    MsgBuilder theBuilder = new MsgBuilder();
        //    theBuilder.DataElements["Control"] = "Country No";
        //    IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
        //    txtcountryno.Focus();
        //    return false;
        //}

        if (txtLPTF.Text.Trim() == "")
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "Facility No";
            IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
            txtLPTF.Focus();
            return false;
        }
        if (txtSatelliteID.Text.Trim() == "")
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "Satelite No";
            IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
            txtSatelliteID.Focus();
            return false;
        }
        /* if (ddsatelliteloc.SelectedIndex < 1)
         {
             MsgBuilder theBuilder = new MsgBuilder();
             theBuilder.DataElements["Control"] = "Satelite No";
             IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
             txtcountryno.Focus();
             return false;
         }*/
        if (txtGrace.Text.Trim() == "")
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "Grace Period";
            IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
            txtGrace.Focus();
            return false;
        }
        if (Convert.ToInt32(txtGrace.Text) < 0)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "Grace Period";
            IQCareMsgBox.Show("GreatThanZero", theBuilder, this);
            txtGrace.Focus();
            return false;
        }
        if (Convert.ToInt32(cmbCurrency.SelectedValue) < 1)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "Currency";
            IQCareMsgBox.Show("BlankDropDown", theBuilder, this);
            cmbCurrency.Focus();
            return false;
        }

        if (txtFacAddress.Text.Trim() == "")
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "Facility Address";
            IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
            txtFacAddress.Focus();
            return false;
        }
        if (txtFactele.Text.Trim() == "")
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "Facility Telephone";
            IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
            txtFactele.Focus();
            return false;
        }
        if (txtFacEmail.Text.Trim() != "")
        {
            if (IsValidEmailAddress(txtFacEmail.Text) == false)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = "InValid Email";
                IQCareMsgBox.Show("#C1", theBuilder, this);
                txtFacEmail.Focus();
                return false;
            }


        }
        if (chkexpPwd.Checked && Convert.ToInt32(txtnoofdays.Text) < 0)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "Expire Password No of Days";
            IQCareMsgBox.Show("GreatThanZero", theBuilder, this);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "toggledivnoofdays", "toggle('divnoofdays');", true);
            //divexppwd.Style.Remove("display");
            //divexppwd.Style["display"] = "block";
            txtnoofdays.Focus();
            return false;
        }

        for (int i = 0; i < cblPMTCT.Items.Count; i++)
        {
            if (cblPMTCT.Items[i].Selected == true)
            {
                flag = true;
                return flag;

            }



        }

        if (flag == false)
        {
            MsgBuilder theMsg = new MsgBuilder();
            theMsg.DataElements["Control"] = "Select any one technical area";
            IQCareMsgBox.Show("UncheckedButton", theMsg, this);

            return false;

        }

        // Making mandatory field....
        //for (int i = 0; i < CBL_DrugStore.Items.Count; i++)
        //{
        //    if (cblPMTCT.Items[i].Selected == true)
        //    {
        //        storeFlag = true;
        //        return storeFlag;

        //    }
        //}
        //if (storeFlag == false)
        //{
        //    MsgBuilder theMsg = new MsgBuilder();
        //    theMsg.DataElements["Control"] = "Select any drug store";
        //    IQCareMsgBox.Show("UncheckedButton", theMsg, this);
        //    return false;
        //}

        return true;
    }
    public DataTable AddModule()
    {
        DataTable dtModule = new DataTable();
        DataColumn ModuleID = new DataColumn("ModuleID", System.Type.GetType("System.Int32"));
        //ModuleID.DataType = System.Type.GetType("System.Int32");
        dtModule.Columns.Add(ModuleID);
        DataRow drModule;
        for (int i = 0; i < cblPMTCT.Items.Count; i++)
        {
            if (cblPMTCT.Items[i].Selected)
            {
                drModule = dtModule.NewRow();
                drModule["ModuleID"] = Convert.ToInt32(cblPMTCT.Items[i].Value);
                dtModule.Rows.Add(drModule);

            }

        }

        if (chkpmscm.Checked == true)
        {
            drModule = dtModule.NewRow();
            drModule["ModuleID"] = 201;
            dtModule.Rows.Add(drModule);
        }

        return dtModule;
    }

    public DataTable AddStores()
    {
        DataTable dtStore = new DataTable(); ;
        DataColumn StoreID = new DataColumn("StoreID", System.Type.GetType("System.Int32"));
        dtStore.Columns.Add(StoreID);
        DataRow drStore;

        for (int i = 0; i < CBL_DrugStore.Items.Count; i++)
        {
            if (CBL_DrugStore.Items[i].Selected)
            {
                drStore = dtStore.NewRow();
                drStore["StoreID"] = Convert.ToInt32(CBL_DrugStore.Items[i].Value == "" ? "0" : CBL_DrugStore.Items[i].Value);
                dtStore.Rows.Add(drStore);
            }

        }

        return dtStore;
    }

    private void SaveYesNoforPaperless()
    {
        string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
        script += "ans=window.confirm('You are changing the Application either to Paperless or Non Paperless. This will have Serious implication. Would you like to change?');\n";
        script += "if (ans==true)\n";
        script += "{\n";
        script += "document.getElementById('" + btnOk.ClientID + "').click();\n";
        script += "}\n";
        script += "else \n";
        script += "{\n";
        script += "window.location.href='frmAdmin_FacilityList.aspx';\n";
        script += "}\n";
        script += "</script>\n";
        RegisterStartupScript("confirm", script);
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
        {
            IQCareMsgBox.Show("SessionExpired", this);
            Response.Redirect("~/frmlogin.aspx", true);
        }
        try
        {
            //Adding event for update panel to trigger without postback......
            ddCountry.SelectedIndexChanged += new EventHandler(SelectedCountryItemTypeChanged);
            ddlprovince.SelectedIndexChanged += new EventHandler(SelectedProvinceItemTypeChanged);

            if (Page.IsPostBack != true)
            {
                if (Session["AppUserName"] != "")
                {
                    AuthenticationManager Authentiaction = new AuthenticationManager();
                    if (Authentiaction.HasFunctionRight(ApplicationAccess.FacilitySetup, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false &&
                        Authentiaction.HasFunctionRight(ApplicationAccess.FacilitySetup, FunctionAccess.Update, (DataTable)Session["UserRight"]) == false)
                    {
                        btnSave.Enabled = false;
                        btnCancel.Enabled = false;
                    }
                }
                else
                {
                    btnExit.Enabled = false;
                }
                ViewState["FacilityId"] = Convert.ToInt32(Request.QueryString["FacilityId"]);
                if (Convert.ToInt32(ViewState["FacilityId"]) < 1)
                {
                    //(Master.FindControl("lblheader") as Label).Text = "Add Facility";
                    //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Visible = false;
                    //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Add Facility";
                    lblForm.Text = "Add Facility";
                }
                else
                {
                    //(Master.FindControl("lblheader") as Label).Text = "Edit Facility";
                    //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Visible = false;
                    //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Edit Facility";
                    lblForm.Text = "Edit Facility";
                }
                //(Master.FindControl("lblMark") as Label).Visible = false;
                ViewState["FacilityDS"] = (DataSet)Session["FacilityDS"];
                //Session.Remove("FacilityDS");

                txtPEPFAR_Fund.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
                txtPEPFAR_Fund.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3')");
                Init_Form();
                Fill_Details();
                if (Convert.ToInt32(ViewState["FacilityId"]) == 0)
                {
                    btnSave.Text = "Save";
                    ddStatus.Enabled = false;
                    IQCareMsgBox.ShowConfirm("FacilitySaveRecord", btnSave);
                }
                else
                {
                    btnSave.Text = "Update";
                    IQCareMsgBox.ShowConfirm("FacilityUpdateRecord", btnSave);
                }
            }

            Page.EnableViewState = true;

        }
        catch (Exception err)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
        }
    }
    private Hashtable getFacilityParameters()
    {
        Hashtable htFacilityParameters = new Hashtable();
        htFacilityParameters.Add("FacilityLogo", txtfacilityname.Text + ".jpg");
        htFacilityParameters.Add("FacilityAddress", txtFacAddress.Text);
        htFacilityParameters.Add("FacilityTel", txtFactele.Text);
        htFacilityParameters.Add("FacilityCell", txtFacCell.Text);
        htFacilityParameters.Add("FacilityFax", txtFacFax.Text);
        htFacilityParameters.Add("FacilityEmail", txtFacEmail.Text);
        htFacilityParameters.Add("FacilityFootertext", txtpharmfoottext.Text);
        htFacilityParameters.Add("FacilityURL", txtFacURL.Text);
        htFacilityParameters.Add("Facilitytemplate", this.Radio1.Checked == true ? Radio1.Value.ToString() : (this.Radio2.Checked == true ? Radio2.Value.ToString() : "0"));
        htFacilityParameters.Add("StrongPassword", this.chkStrongPwd.Checked == true ? 1 : 0);
        htFacilityParameters.Add("ExpirePaswordFlag", this.chkexpPwd.Checked == true ? 1 : 0);
        htFacilityParameters.Add("ExpirePaswordDays", this.chkexpPwd.Checked == true ? txtnoofdays.Text : "");
        htFacilityParameters.Add("DateConstraint", this.chkdateconstraint.Checked == true ? 1 : 0);
        htFacilityParameters.Add("Billing", this.chkbilling.Checked == true ? 1 : 0);
        htFacilityParameters.Add("PMSCM", this.chkpmscm.Checked == true ? 1 : 0);
        // htFacilityParameters.Add("Records", chkrecords.Checked == true ? 1 : 0);
        htFacilityParameters.Add("Wards", chkwards.Checked == true ? 1 : 0);
        htFacilityParameters.Add("LMIS", chkLMIS.Checked == true ? 1 : 0);
        return htFacilityParameters;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (FieldValidation() == false)
        {
            return;
        }

        IFacilitySetup FacilityManager;
        IQCareUtils theUtils = new IQCareUtils();
        if (Convert.ToString(ddlprovince.SelectedValue.Trim()) == "0" || Convert.ToString(ddlprovince.SelectedValue.Trim()) == "")
            ProvinceId = 0;
        else
            ProvinceId = Convert.ToInt32(ddlprovince.SelectedValue.Trim());

        if (Convert.ToString(ddldistrict.SelectedValue.Trim()) == "0" || Convert.ToString(ddldistrict.SelectedValue.Trim()) == "")
            DistrictId = 0;
        else
            DistrictId = Convert.ToInt32(ddldistrict.SelectedValue.Trim());

        if (Convert.ToString(ddCountry.SelectedValue.Trim()) == "0" || Convert.ToString(ddCountry.SelectedValue.Trim()) == "")
            CountryId = 0;
        else
            CountryId = Convert.ToInt32(ddCountry.SelectedValue.Trim());

        Session["AppCountryId"] = CountryId;

        if (txtPEPFAR_Fund.Value.Trim() == "")
        {
            txtPEPFAR_Fund.Value = "01-01-1900";
        }
        if (txtPEPFAR_Fund.Value.Trim() != "")
            thePepFarDate = theUtils.MakeDate(txtPEPFAR_Fund.Value);
        try
        {
            if (FileLoad.PostedFile != null)
            {
                if (FileLoad.PostedFile.FileName != "")
                {
                    HttpPostedFile theFile = FileLoad.PostedFile;
                    theFile.SaveAs(Server.MapPath(string.Format("..//images//{0}", "Login.jpg")));
                    theFName = "Login.jpg";
                }

            }
            else if (ViewState["LoginImage"] != null)
            {
                theFName = ViewState["LoginImage"].ToString();
            }
            else
            {
                theFName = "";
            }

            if (chkPreferred.Checked == true)
                Preferred = 1;
            else
                Preferred = 0;

            int Paperless;
            if (chkPaperlessclinic.Checked == true)
                Paperless = 1;
            else
                Paperless = 0;

            DataTable dtModule = AddModule();
            DataTable dtStore = AddStores();

            if (FileLoad.PostedFile != null)
            {
                if (Filelogo.PostedFile.FileName != "")
                {
                    HttpPostedFile theFilelogo = Filelogo.PostedFile;
                    theFilelogo.SaveAs(Server.MapPath(string.Format("..//images//{0}", txtfacilityname.Text + ".jpg")));

                }
            }
            Hashtable htFacilityParameters = getFacilityParameters();

            /////////////////////////////////
            if (btnSave.Text == "Save")
            {
                FacilityManager = (IFacilitySetup)ObjectFactory.CreateInstance("BusinessProcess.Administration.BFacility, BusinessProcess.Administration");

                int Rows = FacilityManager.SaveNewFacility(txtfacilityname.Text, CountryId.ToString(), txtLPTF.Text, txtSatelliteID.Text, "0", ProvinceId, DistrictId, theFName, Convert.ToInt32(cmbCurrency.SelectedValue), Convert.ToInt32(txtGrace.Text), "dd-MMM-yyyy", Convert.ToDateTime(thePepFarDate), Convert.ToInt32(Session["SystemId"]), Preferred, Paperless, Convert.ToInt32(Session["AppUserId"]), dtModule, htFacilityParameters, dtStore);
                if (Rows > 0)
                {
                    IQCareMsgBox.Show("FacilityMasterSave", this);
                    if (Session["AppUserName"].ToString() != string.Empty)
                    {
                        Response.Redirect("frmAdmin_FacilityList.aspx");

                    }
                    else
                        Response.Redirect("~/frmlogin.aspx", true);
                }
                else
                {
                    IQCareMsgBox.Show("FacilityMasterExists", this);
                    return;
                }
            }
            else
            {
                int Paperlessclinic = this.chkPaperlessclinic.Checked == true ? 1 : 0;
                //int PMTCTclinic = this.chkPMTCT.Checked == true ? 1 : 0;|| (Convert.ToInt32(ViewState["PMTCT"])!= PMTCTclinic)
                if ((Convert.ToInt32(ViewState["Paperless"]) != Paperlessclinic))
                {
                    SaveYesNoforPaperless();
                }
                else if (Convert.ToInt32(ViewState["Paperless"]) == Paperlessclinic)
                {

                    FacilityManager = (IFacilitySetup)ObjectFactory.CreateInstance("BusinessProcess.Administration.BFacility, BusinessProcess.Administration");
                    int Rows = FacilityManager.UpdateFacility(Convert.ToInt32(ViewState["FacilityId"]), txtfacilityname.Text, CountryId.ToString(), txtLPTF.Text, txtSatelliteID.Text, "0", ProvinceId, DistrictId, theFName, Convert.ToInt32(cmbCurrency.SelectedValue), Convert.ToInt32(txtGrace.Text), "dd-MMM-yyyy", Convert.ToDateTime(thePepFarDate), Convert.ToInt32(ddStatus.SelectedValue), Convert.ToInt32(Session["SystemId"]), Preferred, Paperless, Convert.ToInt32(Session["AppUserId"]), dtModule, htFacilityParameters, dtStore);
                    if (Rows > 0)
                    {
                        IQCareMsgBox.Show("FacilityMasterUpdate", this);
                        Response.Redirect("frmAdmin_FacilityList.aspx");
                    }

                }
                else
                {
                    IQCareMsgBox.Show("FacilityMasterExists", this);
                    return;
                }
            }
        }
        catch (Exception err)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
        }
        finally
        {
            FacilityManager = null;
        }
    }

    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmAdmin_FacilityList.aspx");
    }

    //private void SQLCreateJob()
    //{

    //    SQLDMO._SQLServer theServer = new SQLDMO.SQLServerClass();
    //    SQLDMO.Job theJob = new SQLDMO.Job();

    //    IIQCareSystem SQLManager;
    //    SQLManager = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
    //    string constr = SQLManager.GetServerInstance();
    //    string[] theConStr = constr.Split(';');

    //    theServer.Connect((theConStr[0].ToString().Split('='))[1].ToString().Trim(), (theConStr[1].ToString().Split('='))[1].ToString().Trim(), (theConStr[2].ToString().Split('='))[1].ToString().Trim());
    //    theJob.Name = "IQCare Auto-Backup";
    //    theJob.Description = "IQCare Auto-Backup";
    //    theJob.Category = "Database Maintenance";

    //    SQLDMO.JobStep theStep = new SQLDMO.JobStep();
    //    theStep.SubSystem = "TSQL";
    //    string theCmd = "declare @InstanceName varchar(1000) ";
    //    theCmd += " declare @dir varchar(500)";
    //    theCmd += " set @dir = md " + ddBackupDrive.Text + "IQCareDBBackup";
    //    theCmd += " exec xp_cmdshell @dir";
    //    theCmd += " set @FileName = " + ddBackupDrive.Text + "IQCareDBBackup" + "\\IQCare + convert(varchar,getdate(),106) +'.bak'";
    //    theCmd += " set @InstanceName = 'iqcare_backup_' + convert(varchar,getdate(),106)";
    //    theCmd += " BACKUP DATABASE [IQCare] TO  DISK = @FileName WITH NOFORMAT, NOINIT,  NAME = @InstanceName, SKIP, REWIND, NOUNLOAD, STATS = 10";

    //    theStep.Command = theCmd;
    //    theStep.OnSuccessAction = SQLDMO.SQLDMO_JOBSTEPACTION_TYPE.SQLDMOJobStepAction_QuitWithSuccess;
    //    theStep.OnFailAction = SQLDMO.SQLDMO_JOBSTEPACTION_TYPE.SQLDMOJobStepAction_QuitWithFailure;

    //    SQLDMO.JobSchedule theSchedule = new SQLDMO.JobSchedule();
    //    theSchedule.Schedule.FrequencyType = SQLDMO.SQLDMO_FREQUENCY_TYPE.SQLDMOFreq_Daily;
    //    theSchedule.Schedule.FrequencyInterval = 1;
    //    theSchedule.Schedule.ActiveStartDate = Convert.ToInt32(DateTime.Now.Date.ToString("yyyymmdd"));
    //    theSchedule.Schedule.ActiveStartTimeOfDay = Convert.ToInt32(DateTime.Now.ToString("hhmmss"));
    //    theSchedule.Schedule.ActiveEndDate = 99991231;
    //    theSchedule.Schedule.ActiveEndTimeOfDay = 235959;


    //    //theJob.Remove();

    //    theServer.JobServer.Jobs.Add(theJob);
    //    theJob.ApplyToTargetServer((theConStr[0].ToString().Split('='))[1].ToString().Trim());
    //    theJob.JobSteps.Add(theStep);
    //    theJob.BeginAlter();
    //    theStep.DoAlter();
    //    theJob.JobSchedules.Add(theSchedule);
    //    theJob.DoAlter();
    //}

    protected void btnOk_Click(object sender, EventArgs e)
    {
        int Paperlessclinic = this.chkPaperlessclinic.Checked == true ? 1 : 0;
        int Preferred = this.chkPreferred.Checked == true ? 1 : 0;
        DataTable dtModule = AddModule();
        DataTable dtStore = AddStores();
        //int PMTCTclinic = this.chkPMTCT.Checked == true ? 1 : 0;
        IFacilitySetup FacilityMgr;
        IQCareUtils theUtils = new IQCareUtils();
        if (Filelogo.PostedFile.FileName != "")
        {
            HttpPostedFile theFilelogo = Filelogo.PostedFile;
            theFilelogo.SaveAs(Server.MapPath(string.Format("..//images//{0}", txtfacilityname.Text + ".jpg")));

        }
        Hashtable htFacilityParameters = getFacilityParameters();

        if (txtPEPFAR_Fund.Value.Trim() == "")
        {
            txtPEPFAR_Fund.Value = "01-01-1900";
        }
        try
        {
            thePepFarDate = theUtils.MakeDate(txtPEPFAR_Fund.Value);
            FacilityMgr = (IFacilitySetup)ObjectFactory.CreateInstance("BusinessProcess.Administration.BFacility, BusinessProcess.Administration");
            int Rows = FacilityMgr.UpdateFacility(Convert.ToInt32(ViewState["FacilityId"]), txtfacilityname.Text, CountryId.ToString(), txtLPTF.Text, txtSatelliteID.Text, "0", Convert.ToInt32(ddlprovince.SelectedValue), Convert.ToInt32(ddldistrict.SelectedValue), theFName, Convert.ToInt32(cmbCurrency.SelectedValue), Convert.ToInt32(txtGrace.Text), "dd-MMM-yyyy", Convert.ToDateTime(thePepFarDate), Convert.ToInt32(ddStatus.SelectedValue), Convert.ToInt32(Session["SystemId"]), Preferred, Paperlessclinic, Convert.ToInt32(Session["AppUserId"]), dtModule, htFacilityParameters, dtStore);
            Response.Redirect(".././frmLogin.aspx");
        }
        catch (Exception err)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
        }

        finally { FacilityMgr = null; }

    }

}
