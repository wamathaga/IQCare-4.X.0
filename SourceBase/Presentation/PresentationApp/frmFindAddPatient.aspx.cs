
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
using Interface.Administration;
using Interface.Clinical;
using Interface.Security;
using Application.Common;
using Application.Presentation;


public partial class frmFindAddPatient : BasePage
{
    private string theReportName = string.Empty;

    DataSet theFacilityDS;
    #region "User Functions"

    private void Init_Page()
    {
        if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
        {
            IQCareMsgBox.Show("SessionExpired", this);
            Response.Redirect("~/frmlogin.aspx", true);
        }
        AuthenticationManager Authentication = new AuthenticationManager();
        IFacilitySetup FacilityMaster = (IFacilitySetup)ObjectFactory.CreateInstance("BusinessProcess.Administration.BFacility, BusinessProcess.Administration");
        theFacilityDS = FacilityMaster.GetSystemBasedLabels(Convert.ToInt32(Session["SystemId"]), 999, 0);
        ViewState["FacilityDS"] = theFacilityDS;
        ViewState["grdDataSource"] = theFacilityDS.Tables[0];
        ViewState["SortDirection"] = "Asc";
        BindGrid();
        SetPageLabels();


        txtDOB.Value = "";
        txtfirstname.Text = "";
        txtmiddlename.Text = "";
        txtlastname.Text = "";
        txtidentificationno.Text = string.Empty;

        if (Request.QueryString["mnuClicked"] != null)
        {
            if (Request.QueryString["mnuClicked"] != "DeletePatient")
            {
                ddSex.SelectedValue = "0";

            }
        }
        if (Session["TechnicalAreaId"] == null) { Session["TechnicalAreaId"] = 0; }
        BindIdentifierDropdown(Session["TechnicalAreaId"].ToString());
        BindServiceDropdown();
        IUser theLocationManager;
        theLocationManager = (IUser)ObjectFactory.CreateInstance("BusinessProcess.Security.BUser, BusinessProcess.Security");
        DataTable theDT = theLocationManager.GetFacilityList();
        DataRow theDR = theDT.NewRow();
        theDR["FacilityName"] = "All";
        theDR["FacilityId"] = 9999;
        theDT.Rows.InsertAt(theDR, 0);
        BindFunctions theBindManger = new BindFunctions();
        theBindManger.BindCombo(ddFacility, theDT, "FacilityName", "FacilityId");
        ddFacility.SelectedValue = Convert.ToString(Session["AppLocationId"]);
    }

    private void SetPageLabels()
    {   //--------------27thAug2009
        //DataTable theDT = ((DataSet)ViewState["FacilityDS"]).Tables[1];
        //lblpatientenrolno.InnerHtml = theDT.Rows[0]["Label"].ToString() + ":";
        //lblHospclinicno.InnerHtml = theDT.Rows[1]["Label"].ToString()+":";
        if (Convert.ToString(Session["SystemId"]) == "1")
        {
            txtmiddlename.Visible = false;
            lblmiddlename.Visible = false;
        }

    }
    private void BindGrid()
    {

        BoundField theCol0 = new BoundField();
        theCol0.HeaderText = "Patientid";
        theCol0.DataField = "Ptn_Pk";
        theCol0.ItemStyle.Width = Unit.Percentage(3);
        //theCol0.ItemStyle.CssClass = "textstyle";
        theCol0.ReadOnly = true;

        BoundField theCol1 = new BoundField();
        theCol1.HeaderText = "Last Name";
        theCol1.DataField = "lastname";
        theCol1.SortExpression = "lastname";
        //theCol1.ItemStyle.CssClass = "textstyle";
        theCol1.ItemStyle.Width = Unit.Percentage(5);
        theCol1.ReadOnly = true;

        BoundField theCol2 = new BoundField();
        theCol2.HeaderText = "Middle Name";
        theCol2.DataField = "middlename";
        theCol2.SortExpression = "middlename";
        //theCol2.ItemStyle.CssClass = "textstyle";
        theCol2.ItemStyle.Width = Unit.Percentage(5);

        theCol2.ReadOnly = true;

        BoundField theCol3 = new BoundField();
        theCol3.HeaderText = "First Name";
        theCol3.DataField = "firstname";
        //theCol3.ItemStyle.CssClass = "textstyle";
        theCol3.SortExpression = "firstname";
        theCol3.ItemStyle.Font.Underline = true;
        theCol3.ItemStyle.Width = Unit.Percentage(5);
        theCol3.ReadOnly = true;

        BoundField theCol4 = new BoundField();
        theCol4.HeaderText = "IQ Number";
        //theCol4.ItemStyle.CssClass = "textstyle";
        theCol4.DataField = "PatientID";
        theCol4.SortExpression = "PatientID";
        theCol4.ItemStyle.Width = Unit.Percentage(7);
        theCol4.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
        theCol4.ItemStyle.VerticalAlign = VerticalAlign.Top;
        theCol4.ReadOnly = true;

        BoundField theCol5 = new BoundField();
        theCol5.HeaderText = "Services";
        //theCol5.ItemStyle.CssClass = "textstyle";
        theCol5.DataField = "PatientIDType";
        theCol5.SortExpression = "PatientIDType";
        theCol5.ItemStyle.Width = Unit.Percentage(32);
        theCol5.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
        theCol5.ItemStyle.VerticalAlign = VerticalAlign.Top;
        theCol5.ItemStyle.Wrap = true;
        theCol5.ReadOnly = true;

        BoundField theCol6 = new BoundField();
        if (((DataSet)ViewState["FacilityDS"]).Tables[0].Rows.Count > 0)
        {
            theCol6.HeaderText = ((DataSet)ViewState["FacilityDS"]).Tables[0].Rows[1][0].ToString();
        }
        //theCol4.HeaderText = "Existing Hosp/Clinic";
        //theCol6.ItemStyle.CssClass = "textstyle";
        theCol6.DataField = "PatientClinicID";
        theCol6.SortExpression = "PatientClinicID";
        theCol5.ItemStyle.Width = Unit.Percentage(7);
        theCol6.ReadOnly = true;

        BoundField theCol7 = new BoundField();
        theCol7.HeaderText = "Sex";
        //theCol7.ItemStyle.CssClass = "textstyle";
        theCol7.DataField = "Name";
        theCol7.SortExpression = "Name";
        theCol7.ItemStyle.Width = Unit.Percentage(2);
        theCol7.ReadOnly = true;

        BoundField theCol8 = new BoundField();
        theCol8.HeaderText = "DOB";
        theCol8.DataField = "dob";
        //theCol8.ItemStyle.CssClass = "textstyle";
        theCol8.SortExpression = "dob";
        theCol8.ItemStyle.Width = Unit.Percentage(3);
        theCol8.ReadOnly = true;

        ////BoundField theCol8 = new BoundField();
        ////theCol8.HeaderText = "Village";
        ////theCol8.DataField = "VillageName";
        ////theCol8.ItemStyle.CssClass = "textstyle";
        ////theCol8.SortExpression = "VillageName";
        ////theCol8.ReadOnly = true;

        BoundField theCol9 = new BoundField();
        theCol9.HeaderText = "Status";
        theCol9.DataField = "status";
        //theCol9.ItemStyle.CssClass = "textstyle";
        theCol9.SortExpression = "status";
        theCol9.ItemStyle.Width = Unit.Percentage(2);
        theCol9.ReadOnly = true;

        BoundField theCol10 = new BoundField();
        theCol10.HeaderText = "Patient Location";
        //theCol10.ItemStyle.CssClass = "textstyle";
        theCol10.DataField = "FacilityName";
        theCol10.SortExpression = "FacilityName";
        theCol10.ItemStyle.Width = Unit.Percentage(6);
        theCol10.ItemStyle.Wrap = true;
        theCol10.ReadOnly = true;

        BoundField theCol11 = new BoundField();
        theCol11.HeaderText = "LocationId";
        theCol11.DataField = "locationID";
        theCol11.ItemStyle.Width = Unit.Percentage(2);
        //theCol11.ItemStyle.CssClass = "textstyle";
        theCol11.ReadOnly = true;

        //ButtonField theBtn = new ButtonField();
        //theBtn.ButtonType = ButtonType.Link;
        //theBtn.CommandName = "Select";
        //theBtn.HeaderStyle.CssClass = "textstylehidden";
        //theBtn.ItemStyle.CssClass = "textstylehidden";

        //grdSearchResult.Columns.Add(theCol);
        grdSearchResult.Columns.Add(theCol0);
        grdSearchResult.Columns.Add(theCol1);
        grdSearchResult.Columns.Add(theCol2);
        grdSearchResult.Columns.Add(theCol3);
        grdSearchResult.Columns.Add(theCol4);
        grdSearchResult.Columns.Add(theCol5);
        grdSearchResult.Columns.Add(theCol6);
        grdSearchResult.Columns.Add(theCol7);
        grdSearchResult.Columns.Add(theCol8);
        grdSearchResult.Columns.Add(theCol9);
        grdSearchResult.Columns.Add(theCol10);
        grdSearchResult.Columns.Add(theCol11);
        //grdSearchResult.Columns.Add(theBtn);

        grdSearchResult.DataBind();
        grdSearchResult.Columns[0].Visible = false;
        grdSearchResult.Columns[6].Visible = false;
        grdSearchResult.Columns[11].Visible = false;
        if (Convert.ToString(Session["SystemId"]) == "1")
        {
            grdSearchResult.Columns[2].Visible = false;
        }
    }
    #region "Modified 13th june 2007(2) "
    private void SortAndSetDataInGrid(String SortExpression)
    {
        IQCareUtils clsUtil = new IQCareUtils();
        DataView theDV;

        if (SortExpression == "dob")
        {
            SortExpression = "dobPatient";
        }

        theDV = clsUtil.GridSort((DataTable)Session["GrdData"], SortExpression, Session["SortDirection"].ToString());

        grdSearchResult.DataSource = null;
        grdSearchResult.Columns.Clear();

        grdSearchResult.DataSource = theDV;
        BindGrid();

    }
    #endregion
    private Hashtable EnrollParams()
    {
        Hashtable theHT = new Hashtable();
        theHT.Add("FirstName", txtfirstname.Text);
        theHT.Add("LastName", txtlastname.Text);
        theHT.Add("MiddleName", txtmiddlename.Text);
        theHT.Add("EnrollmentNo", txtidentificationno.Text);
        //theHT.Add("EnrollmentNo", txtpatientenrolno.Text);
        //theHT.Add("ClinicNo", txtHospclinicno.Text);
        theHT.Add("Date of Birth", txtDOB.Value);
        theHT.Add("Sex", ddSex.SelectedValue);
        return theHT;
    }

    protected void BindServiceDropdown()
    {
        BindFunctions BindManager = new BindFunctions();
        IPatientRegistration ptnMgr = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
        DataSet DSModules = ptnMgr.GetModuleNames(Convert.ToInt32(Session["AppLocationId"]), Convert.ToInt32(Session["AppUserId"]));

        DataTable theModDT = new DataTable();

        //if Admin user load all service areas
        if (Convert.ToString(Session["AppUserId"]) == "1")
            theModDT = DSModules.Tables[0];
        else
            theModDT = DSModules.Tables[2];

        DataView theModDV = new DataView(theModDT);
        theModDV.RowFilter = "ModuleId NOT IN(206,207,7)";
        IQCareUtils theModUtils = new IQCareUtils();
        DataTable theDT = new DataTable();
        theDT = theModUtils.CreateTableFromDataView(theModDV);
        if (theDT.Rows.Count > 0)
        {
            BindManager.BindCombo(ddlServices, theDT, "ModuleName", "ModuleID");
            ptnMgr = null;
        }
    }


    void BindIdentifierDropdown(string strServiceArea)
    {

        IPatientRegistration identifierManager;
        identifierManager = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
        DataTable theDT = identifierManager.GetIdentifierListByServiceName(strServiceArea);
        DataRow theDR = theDT.NewRow();
        theDR["IdentifierName"] = "All";
        theDR["IdentifierID"] = 9999;
        theDT.Rows.InsertAt(theDR, 0);
        theDR = theDT.NewRow();
        theDR["IdentifierName"] = "IQNumber";
        theDR["IdentifierID"] = 10000;
        theDT.Rows.InsertAt(theDR, 1);
        BindFunctions theBindManger = new BindFunctions();
        theBindManger.BindCombo(ddlIdentifier, theDT, "IdentifierName", "IdentifierId");
        ddFacility.SelectedValue = "0";
        txtidentificationno.Text = string.Empty;
        txtidentificationno.Enabled = false;
    }
    private void SetEnrollmentCombo()
    {
        if (Request.QueryString["mnuClicked"] == null)
        {
            string[,] ComboOptions = new string[3, 2];
            Hashtable theHT = EnrollParams();
            Session.Add("EnrollParams", theHT);
            string url;
            //Response.Redirect(url);

            if (Convert.ToString(Session["SystemId"]) == "1")
            {
                ////url = string.Format("{0}&sts={1}", "./ClinicalForms/frmClinical_Enrolment.aspx?name=Add", 0);
                url = string.Format("{0}", "./ClinicalForms/frmClinical_Enrolment.aspx");
            }
            else
            {
                url = string.Format("{0}&sts={1}", "./ClinicalForms/frmClinical_PatientRegistrationCTC.aspx?name=Add", 0);
            }
            ////////string pmtcturl;
            ////////////pmtcturl = string.Format("{0}&sts={1}", "./ClinicalForms/frmClinical_EnrolmentPMTCT.aspx?name=Add", 0);
            ////////pmtcturl = string.Format("{0}", "./ClinicalForms/frmClinical_EnrolmentPMTCT.aspx");
            //////////HiddenURL.Value = url;
            //////////HiddenPMTCTURL.Value = pmtcturl;
            ////////string theScript = "<script language='javascript' id='RegCmb' type='text/javascript'>";
            ////////theScript += "var arrItems=new Array();";
            //////////-----Module Setting-----
            ////////DataTable theDT = (DataTable)Session["AppModule"];
            ////////DataView theDV = new DataView(theDT);
            ////////theDV.RowFilter = "ModuleId= 2";
            ////////if (theDV.Count > 0)
            ////////{
            ////////    theScript += "arrItems.push({Label:'HIV Care',URL:'" + url + "'});";
            ////////}
            ////////theDV.RowFilter = "ModuleId= 1";
            ////////if (theDV.Count > 0)
            ////////{
            ////////    theScript += "arrItems.push({Label:'PMTCT',URL:'" + pmtcturl + "'});";
            ////////}
            ////////theScript += "createComboBox('cmb','Add Patient',arrItems);";
            ////////theScript += "</script>";
            ////////RegisterStartupScript("Regcmb", theScript);
            //////////btnView.Attributes.Add("onClick", "createComboBox('cmb','Add Patient',arrItems);"); 
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
        {
            IQCareMsgBox.Show("SessionExpired", this);
            Response.Redirect("~/frmlogin.aspx", true);
        }
        SetEnrollmentCombo();
        Ajax.Utility.RegisterTypeForAjax(typeof(frmFindAddPatient));
        if (!IsPostBack)
        {
            Session["HIVPatientStatus"] = 0;
            Session["PMTCTPatientStatus"] = 0;
            //SetEnrollmentCombo();
            Session["PatientId"] = 0;
            //(Master.FindControl("lblheaderfacility") as Label).Visible = false;
            //(Master.FindControl("lblheader") as Label).Text = "Find Add Patient";
            //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Find Add Patient";
            //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Visible = false;

            txtDOB.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3'); isCheckValidDate('" + Application["AppCurrentDate"] + "', '" + txtDOB.ClientID + "', '" + txtDOB.ClientID + "');");
            txtDOB.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            //      txtDOB.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3')");
            //txtpatientenrolno.Attributes.Add("onkeyup", "chkInteger('" + txtpatientenrolno.ClientID + "')");

            if (Request.QueryString["loc"] != null)
            {
                if (Request.QueryString["loc"] == "w")
                {
                    Utility theUtil = new Utility();
                    txtidentificationno.Text = theUtil.DecodeFrom64(Request.QueryString["iqnum"].ToString());

                    /*
                     * Calling generate cache from common location
                     * Update By: Gaurav 
                     * Update Date: 8 July 2014
                     */
                    IQCareUtils.GenerateCache(false);

                    windowlogin(theUtil.DecodeFrom64(Request.QueryString["AppName"]), Convert.ToInt32(theUtil.DecodeFrom64(Request.QueryString["apploc"].ToString())), Convert.ToInt32(theUtil.DecodeFrom64(Request.QueryString["sysid"].ToString())));
                }
            }
            if (Request.QueryString["ReportName"] != null)
            {
                theReportName = Request.QueryString["ReportName"];
                tHeading.InnerText = "Find Patient";
                btnAdd.Visible = false;
                spanBtnAdd.Visible = false;
            }
            else
            {
                theReportName = null;
            }
            if (Request.QueryString["mnuClicked"] != null)
            {
                if (Request.QueryString["mnuClicked"] == "DeletePatient")
                {
                    //add.Visible = false;
                    //RTyagi..19Feb 07..
                    /***************** Check For User Rights ****************/
                    AuthenticationManager Authentiaction = new AuthenticationManager();
                    //if (Request.QueryString["name"] == "Delete")
                    //{
                    if (Authentiaction.HasFunctionRight(ApplicationAccess.DeletePatient, FunctionAccess.Delete, (DataTable)Session["UserRight"]) == false)
                    {
                        string theUrl = "./frmFacilityHome.aspx";
                        Response.Redirect(theUrl);
                    }
                    //}

                    tHeading.InnerText = "Delete Patient";
                    //(Master.FindControl("lblheader") as Label).Text = "Delete Patient";
                    //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Delete Patient";
                    IQWebUtils Utils = new IQWebUtils();
                    Utils.HidePatientLevelheader();
                    btnAdd.Visible = false;
                    spanBtnAdd.Visible = false;
                    tdPatientOtherDetails.Visible = false;
                }
            }

            Session.Remove("GrdData");
            Session.Remove("SortDirection");
            if (Request.QueryString["FormName"] != null)
            {
                if (Request.QueryString["FormName"].ToString() == "AppointmentMain")
                {
                    //add.Visible = false;
                    btnAdd.Visible = false;
                    spanBtnAdd.Visible = false;

                }
            }
            if (Request.QueryString["FormName"] != null)
            {
                if (Request.QueryString["FormName"].ToString() == "FamilyInfo")
                {
                    btnAdd.Visible = false;
                    spanBtnAdd.Visible = false;
                }
            }
            Init_Page();

        }

    }

    public void windowlogin(string name, int locationid, int systemid)
    {
        IUser LoginManager;
        LoginManager = (IUser)ObjectFactory.CreateInstance("BusinessProcess.Security.BUser, BusinessProcess.Security");
        DataSet theDS = LoginManager.GetUserCredentials(name, locationid, systemid);
        if (theDS.Tables.Count > 0)
        {
            Session["AppUserId"] = Convert.ToString(theDS.Tables[0].Rows[0]["UserId"]);
            Session["AppUserName"] = Convert.ToString(theDS.Tables[0].Rows[0]["UserFirstName"]) + " " + Convert.ToString(theDS.Tables[0].Rows[0]["UserLastName"]);
            Session["EnrollFlag"] = theDS.Tables[1].Rows[0]["EnrollmentFlag"].ToString();
            Session["CareEndFlag"] = theDS.Tables[1].Rows[0]["CareEndFlag"].ToString();
            Session["IdentifierFlag"] = theDS.Tables[1].Rows[0]["IdentifierFlag"].ToString();
            Session["UserRight"] = theDS.Tables[1];
            DataTable theDT = theDS.Tables[2];
            Session["AppLocationId"] = theDT.Rows[0]["FacilityID"].ToString();
            Session["AppLocation"] = theDT.Rows[0]["FacilityName"].ToString();
            Session["AppCountryId"] = theDT.Rows[0]["CountryID"].ToString();
            Session["AppPosID"] = theDT.Rows[0]["PosID"].ToString();
            Session["AppSatelliteId"] = theDT.Rows[0]["SatelliteID"].ToString();
            Session["GracePeriod"] = theDT.Rows[0]["AppGracePeriod"].ToString();
            Session["AppDateFormat"] = theDT.Rows[0]["DateFormat"].ToString();
            Session["BackupDrive"] = theDT.Rows[0]["BackupDrive"].ToString();
            Session["SystemId"] = theDT.Rows[0]["SystemId"].ToString();
            Session["AppCurrency"] = theDT.Rows[0]["Currency"].ToString();
            Session["AppUserEmployeeId"] = theDS.Tables[0].Rows[0]["EmployeeId"].ToString();

            Session["AppModule"] = theDS.Tables[3];
            DataView theSCMDV = new DataView(theDS.Tables[3]);
            theSCMDV.RowFilter = "ModuleId=201";
            if (theSCMDV.Count > 0)
                Session["SCMModule"] = theSCMDV[0]["ModuleName"];

            Session["Paperless"] = theDT.Rows[0]["Paperless"].ToString();
            Session["Program"] = "";
            /*
            * Commented by Gaurav & Suggested by Joseph 
            * Purpose: Everytime not to update appoinments
            * Date: 23 Sept 2014
            */
            /////////////// Appointment Updates//////////////////
            //UpdateAppointment();
            /////////////////////////////////////////////////////

        }
    }
    private void UpdateAppointment()
    {
        //*******Update appointment status priviously missed, missed, careended and met from pending*******//                LoginManager = (IUser)ObjectFactory.CreateInstance("BusinessProcess.Security.BUser, BusinessProcess.Security");
        IUser LoginManager;
        LoginManager = (IUser)ObjectFactory.CreateInstance("BusinessProcess.Security.BUser, BusinessProcess.Security");
        int theAffectedRows = LoginManager.UpdateAppointmentStatus(Convert.ToString(Application["AppCurrentDate"]), Convert.ToInt16(Session["AppLocationId"]));
    }

    #region Commented Old Code for GenerateCache
    //private void GenerateCache()
    //{
    //    IIQCareSystem DateManager;
    //    DateManager = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
    //    DateTime theDTime = DateManager.SystemDate();

    //    System.IO.FileInfo theFileInfo1 = new System.IO.FileInfo(Server.MapPath(".\\XMLFiles\\AllMasters.con").ToString());
    //    System.IO.FileInfo theFileInfo2 = new System.IO.FileInfo(Server.MapPath(".\\XMLFiles\\DrugMasters.con").ToString());
    //    System.IO.FileInfo theFileInfo3 = new System.IO.FileInfo(Server.MapPath(".\\XMLFiles\\LabMasters.con").ToString());

    //    if (theFileInfo1.LastWriteTime.Date != theDTime.Date || theFileInfo2.LastWriteTime.Date != theDTime.Date || theFileInfo3.LastWriteTime.Date != theDTime.Date)
    //    {
    //        theFileInfo1.Delete();
    //        theFileInfo2.Delete();
    //        theFileInfo3.Delete();
    //        IIQCareSystem theCacheManager = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem,BusinessProcess.Security");
    //        DataSet theMainDS = theCacheManager.GetSystemCache();
    //        DataSet WriteXMLDS = new DataSet();

    //        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Provider"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Ward"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Division"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_District"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Reason"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Education"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Designation"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Employee"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Occupation"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Province"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Village"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Code"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_HIVAIDSCareTypes"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_ARTSponsor"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_HivDisease"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Assessment"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Symptom"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Decode"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Feature"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Function"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_HivDisclosure"].Copy());
    //        //WriteXMLDS.Tables.Add(theMainDS.Tables["mst_Satellite"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_LPTF"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_StoppedReason"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["mst_facility"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_HIVCareStatus"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_RelationshipType"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_TBStatus"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_ARVStatus"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_LostFollowreason"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Regimen"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_CouncellingType"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_CouncellingTopic"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_ReferredFrom"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_pmtctDeCode"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Module"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_ModDecode"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_ARVSideEffects"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_ModCode"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Country"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Town"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["VWDiseaseSymptom"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["VW_ICDList"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["mst_RegimenLine"].Copy());
    //        WriteXMLDS.WriteXml(Server.MapPath(".\\XMLFiles\\").ToString() + "AllMasters.con", XmlWriteMode.WriteSchema);

    //        WriteXMLDS.Tables.Clear();
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Strength"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_FrequencyUnits"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Drug"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Generic"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_DrugType"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Frequency"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_DrugSchedule"].Copy());
    //        WriteXMLDS.WriteXml(Server.MapPath(".\\XMLFiles\\").ToString() + "DrugMasters.con", XmlWriteMode.WriteSchema);

    //        WriteXMLDS.Tables.Clear();
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_LabTest"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["Lnk_TestParameter"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["Lnk_LabValue"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["Lnk_ParameterResult"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["LabTestOrder"].Copy());
    //        WriteXMLDS.Tables.Add(theMainDS.Tables["mst_PatientLabPeriod"].Copy());
    //        WriteXMLDS.WriteXml(Server.MapPath(".\\XMLFiles\\").ToString() + "LabMasters.con", XmlWriteMode.WriteSchema);
    //    }
    //}
    #endregion

    #region "Modified 13th june 2007(1) "
    protected void grdSearchResult_Sorting(object sender, GridViewSortEventArgs e)
    {
        IQCareUtils clsUtil = new IQCareUtils();
        DataView theDV;

        SortAndSetDataInGrid(e.SortExpression);

        if (Session["SortDirection"].ToString() == "Asc")
        {
            Session["SortDirection"] = "Desc";
        }
        else
        {
            Session["SortDirection"] = "Asc";
        }

    }
    #endregion

    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public string SetPatientId_Session(string url)
    {
        string str = url;
        string strUrl = string.Empty;
        int patientID = 0;
        strUrl = str.Substring(0, str.IndexOf("?"));
        if (strUrl == "./Scheduler/frmScheduler_AppointmentNew.aspx")
        {
            patientID = Convert.ToInt32(str.Substring(str.LastIndexOf("=") + 1));
        }
        else
        {
            patientID = Convert.ToInt32(str.Substring(str.IndexOf("=") + 1));
        }
        Session["PatientId"] = patientID;


        if (strUrl == "./Scheduler/frmScheduler_AppointmentNew.aspx")
        {
            strUrl = "./Scheduler/frmScheduler_AppointmentNew.aspx?name=Add";
        }

        #region "Refresh Patient Records"
        IPatientHome PManager;
        PManager = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
        System.Data.DataSet thePDS = PManager.GetPatientDetails(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["SystemId"]), Convert.ToInt32(Session["TechnicalAreaId"]));
        //System.Data.DataSet thePDS = PManager.GetPatientDetails(Convert.ToInt32(Request.QueryString["PatientId"]), Convert.ToInt32(Session["SystemId"]));

        Session["PatientInformation"] = thePDS.Tables[0];
        #endregion

        return strUrl;
    }
    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public string SetPatientIdFamily_Session(string url)
    {
        string str = url;

        int refID = Convert.ToInt32(str.Substring(str.IndexOf("RefId") + 6, str.IndexOf("&&") - str.IndexOf("RefId") - 6));
        int patientID = Convert.ToInt32(str.Substring(str.LastIndexOf("=") + 1));
        Session["PatientId"] = patientID;
        string strUrl = string.Empty;
        strUrl = str.Substring(0, str.IndexOf("?"));
        return url;
    }
    protected void grdSearchResult_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    e.Row.BackColor = System.Drawing.Color.White;
        //    e.Row.Attributes.Add("onmouseover", "this.style.cursor='hand';");
        //    e.Row.Attributes.Add("onclick",GetPostBackClientHyperlink(this.grdSearchResult, "Select$" + e.Row.DataItemIndex));
        //}

        string theUrl = string.Empty;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.BackColor = System.Drawing.Color.White;
            e.Row.Attributes.Add("onmouseover", "this.style.cursor='hand';");

            if (Request.QueryString["FormName"] != null && Request.QueryString["FormName"].ToString() == "FamilyInfo")
            {
                theUrl = string.Format("{0}?RefId={1}&&PatientId={2}", "./ClinicalForms/frmFamilyInformation.aspx", e.Row.Cells[0].Text, Session["PtnRedirect"].ToString());
                //e.Row.Attributes.Add("onclick", "window.location.href=('" + theUrl + "')");  
                e.Row.Attributes.Add("onclick", "fnSetSessionfamily('" + theUrl + "')");
            }

            else if (Convert.ToInt32(e.Row.Cells[11].Text) == Convert.ToInt32(Session["AppLocationId"]))
            {
                if (Request.QueryString["FormName"] != null && Request.QueryString["FormName"].ToString() == "AppointmentMain")
                {

                    //theUrl = string.Format("{0}&PatientId={1}&PatientEnrollmentID={2}&Locationid={3}", "./Scheduler/frmScheduler_AppointmentNew.aspx?name=Add", e.Row.Cells[0].Text, e.Row.Cells[5].Text, e.Row.Cells[12].Text);
                    theUrl = string.Format("{0}&PatientId={1}", "./Scheduler/frmScheduler_AppointmentNew.aspx?name=Add", e.Row.Cells[0].Text);
                }
                else if (Request.QueryString["mnuClicked"] != null && Request.QueryString["mnuClicked"] == "DeletePatient")
                {

                    theUrl = string.Format("{0}?PatientID={1}", "./AdminForms/frmAdmin_DeletePatient.aspx", e.Row.Cells[0].Text);
                }
                else if (theReportName != "")
                {
                    if (theReportName == "ARVAdherence")
                    {
                        theUrl = string.Format("{0}ReportName={1}&PatientId={2}", "./Reports/frmReportViewer.aspx?", theReportName, e.Row.Cells[0].Text);
                    }
                    else
                    {
                        theUrl = string.Format("{0}?PatientId={1}", "./frmAddTechnicalArea.aspx", e.Row.Cells[0].Text);
                    }
                }
                else
                {
                    theUrl = string.Format("{0}?PatientId={1}", "./frmAddTechnicalArea.aspx", e.Row.Cells[0].Text);
                    //theUrl = string.Format("{0}?Values={1}", "./ClinicalForms/frmPatient_Home.aspx", e.Row.Cells[0].Text);
                }

                //e.Row.Attributes.Add("onclick", "window.location.href=('" + theUrl + "')");
                e.Row.Attributes.Add("onclick", "fnSetSession('" + theUrl + "')");
            }
            else
            {
                //string theScript = "alert('This Patient belongs to different Location. Please Log-in from " + e.Row.Cells[8].Text + ".');";
                string theScript = "alert('This Patient belongs to a different Location. Please log-in with the patient\\'s location.');";
                e.Row.Attributes.Add("onclick", theScript);
            }


        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {

        //Hashtable theParams = new Hashtable();
        //theParams.Clear();
        //fillHashTable(theParams);
        //string url = string.Format("{0}&sts={1}&hashTable={2}", "./ClinicalForms/frmClinical_Enrolment.aspx?name=Add", 0, theParams);
        ///////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////Added by Sanjay on 27th Nov. 07 for Passing parameters to EnrollmentForm///////////
        Hashtable theHT = EnrollParams();
        Session.Add("EnrollParams", theHT);
        string url;
        ////////////////////Updated by Archana on 10th May 2010 for Redirecting to Module Selection Page///////////
        //if (Session["SystemId"].ToString() == "1")
        //{
        //    url = string.Format("{0}&sts={1}", "./ClinicalForms/frmClinical_Enrolment.aspx?name=Add", 0);
        //}
        //else { url = string.Format("{0}&sts={1}", "./ClinicalForms/frmClinical_PatientRegistrationCTC.aspx?name=Add", 0); }

        //url = string.Format("{0}&sts={1}", "./frmPatientRegistration.aspx?name=Add", 0);
        url = string.Format("{0}&sts={1}", "./frmPatientCustomRegistration.aspx?name=Add", 0);
        Response.Redirect(url);
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        // ScriptManager.RegisterClientScriptBlock(btnView, typeof(Button), "Regcmb_Ajax", "createComboBox('cmb','Add Patient',arrItems);", true); 
        //System.Threading.Thread.Sleep(3000);
        IPatientRegistration PatientManager;
        try
        {
            IQCareUtils theUtil = new IQCareUtils();
            string theDOB = "";
            //hidMessage.Value = "";
            if (txtDOB.Value == "")
            {
                theDOB = "01-01-1900";
            }
            else
            {
                theDOB = txtDOB.Value;
            }
            theDOB = theUtil.MakeDate(theDOB);


            if (ddlServices.SelectedIndex <= 0)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = "Please select service to search.";
                IQCareMsgBox.Show("#C1", theBuilder, this);
                return;
            }

            Session["TechnicalAreaName"] = ddlServices.Items[ddlServices.SelectedIndex].Text;

            string strname;
            strname = txtlastname.Text;
            txtlastname.Text = strname.Replace("'", "''").ToString();
            string sname;
            sname = txtmiddlename.Text;
            txtmiddlename.Text = sname.Replace("'", "''").ToString();
            string stname;
            stname = txtfirstname.Text;
            txtfirstname.Text = stname.Replace("'", "''").ToString();

            grdSearchResult.DataSource = null;
            grdSearchResult.Columns.Clear();
            PatientManager = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
            DataSet dsPatient = new DataSet();

            dsPatient = PatientManager.GetPatientSearchResults(Convert.ToInt32(ddFacility.SelectedValue), txtlastname.Text, txtmiddlename.Text, txtfirstname.Text, ddlIdentifier.SelectedValue, txtidentificationno.Text, ddSex.SelectedValue, Convert.ToDateTime(theDOB).ToString("dd-MMM-yyyy"), ddCareEndedStatus.SelectedValue, Convert.ToInt32(ddlServices.SelectedValue));

            this.grdSearchResult.DataSource = dsPatient.Tables[0];
            Session["GrdData"] = dsPatient.Tables[0];
            Session["SortDirection"] = "Asc";
            BindGrid();

            // Comment by deepak for fixed header
            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + grdSearchResult.ClientID + "', 400, 900 , 40 ,false); </script>", false);

            //SetEnrollmentCombo();
            if (dsPatient.Tables[0].Rows.Count == 0)
            {
                //hidMessage.Value = "No";
                IQCareMsgBox.Show("NoPatientExists", this);
                Init_Page();
            }
        }
        catch (Exception err)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
            return;
        }
        finally
        {
            PatientManager = null;
        }
    }

    protected void ddlIdentifier_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtidentificationno.Enabled = (ddlIdentifier.SelectedItem.ToString() == "Select") ? false : true;
        txtidentificationno.Text = (ddlIdentifier.SelectedItem.ToString() == "Select") ? string.Empty : txtidentificationno.Text;

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string theUrl = "";

        if (Request.QueryString["FormName"] != null)
        {
            //if (Request.QueryString["FormName"].ToString() == "DeletePatient")
            //{
            //    theUrl = string.Format("./AdminForms/frmAdmin_DeletePatient.aspx");
            //}

            if (Request.QueryString["FormName"].ToString() == "AppointmentMain")
            {
                theUrl = string.Format("./Scheduler/frmScheduler_AppointmentMain.aspx");
            }
            if (Request.QueryString["FormName"].ToString() == "FamilyInfo")
            {
                //theUrl = string.Format("./ClinicalForms/frmFamilyInformation.aspx");
                //theUrl = string.Format("{0}&PatientId={1}&sts={2}", "./ClinicalForms/frmFamilyInformation.aspx?name=Edit", Convert.ToInt32(Session["RefId"].ToString()), Convert.ToInt32(Session["lblpntstatus"].ToString()));
                theUrl = string.Format("{0}&back={1}&sts={2}&PatientId={3}", "./ClinicalForms/frmFamilyInformation.aspx?name=Edit", "Back", Convert.ToInt32(Session["lblpntstatus"].ToString()), Session["PtnRedirect"].ToString());
            }
        }
        else
        {
            theUrl = string.Format("{0}", "frmFacilityHome.aspx");
        }
        Response.Redirect(theUrl);
    }


    //protected void lnkAddPMTCT_Click(object sender, EventArgs e)
    //{
    //    string url;
    //    if (Convert.ToString(Session["SystemId"]) == "1")
    //    {
    //        url = string.Format("{0}&sts={1}", "./ClinicalForms/frmClinical_EnrolmentPMTCT.aspx?name=Add", 0);
    //    }
    //    else { url = string.Format("{0}&sts={1}", "./ClinicalForms/frmClinical_EnrolmentPMTCT.aspx?name=Add", 0); }
    //    Response.Redirect(url);
    //}
}
