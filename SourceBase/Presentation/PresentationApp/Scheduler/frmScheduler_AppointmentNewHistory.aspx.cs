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
using Interface.Scheduler;
using Interface.Administration;
using Interface.Clinical;
using Application.Presentation;
using Application.Common;
using System.Collections.Generic;

public partial class frmScheduler_AppointmentNewHistory : LogPage
{
    #region "User Functions"
    //int currentDate;
    #region "Modified13June07(1)"
    private void fillDropDownList(int idPurpose, int idEmployee)
    {
        IAppointment FormManager;
        FormManager = (IAppointment)ObjectFactory.CreateInstance("BusinessProcess.Scheduler.BAppointment, BusinessProcess.Scheduler");
        DataSet theDtSet = FormManager.GetEmployees(idEmployee);
        DataSet theDtSetPurpose = FormManager.GetAppointmentReasons(idPurpose);
        BindFunctions appBind = new BindFunctions();
        IQCareUtils theUtils = new IQCareUtils();
        if (Request.QueryString["Name"] == "Add")
        {
            //if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
            //{
            DataView theDV = new DataView(theDtSet.Tables[0]);
            DataView TheDV = new DataView(theDtSetPurpose.Tables[0]);
            theDV.RowFilter = "DeleteFlag=0";
            TheDV.RowFilter = "DeleteFlag=0";
            if (theDV.Table != null)
            {
                DataTable DT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                if (Convert.ToInt32(Session["AppUserEmployeeId"]) > 0)
                {
                    theDV = new DataView(DT);

                    //theDV.RowFilter = "EmployeeId IN("+ EmployeeId +" ," + Session["AppUserEmployeeId"].ToString() + ")";
                    theDV.RowFilter = "EmployeeId =" + Session["AppUserEmployeeId"].ToString();
                    if (theDV.Count > 0)
                        DT = theUtils.CreateTableFromDataView(theDV);
                }
                DataTable TheDT = (DataTable)theUtils.CreateTableFromDataView(TheDV);

                //appBind.BindCombo(ddAppProvider, DT, "EmployeeName", "EmployeeId");
                //BindUserDropdown(ddAppProvider, string.Empty);
                BindUserDropdown(ddAppProvider, Session["AppUserId"].ToString());
                appBind.BindCombo(ddAppPurpose, TheDT, "Name", "Id");
                theDV.Dispose();
                TheDV.Dispose();
                DT.Clear();
                TheDT.Clear();


            }
            //}

        }
        if (Request.QueryString["name"] == "Edit" || Request.QueryString["name"] == "Delete")
        {



            BindDropdownOrderBy(theDtSet.Tables[0].Rows[0]["EmployeeName"].ToString());

            this.ddAppProvider.SelectedValue = theDtSet.Tables[0].Rows[0]["EmployeeName"].ToString();
            //appBind.BindCombo(ddAppProvider, theDtSet.Tables[0], "EmployeeName", "EmployeeId");
            appBind.BindCombo(ddAppPurpose, theDtSetPurpose.Tables[0], "Name", "Id");
        }

    }
    //{
    //    IAppointment FormManager;
    //    BindFunctions appBind;
    //    DataSet theDtSet;
    //    DataSet theDtSetPurpose;

    //    //*******Get the patient details on the basis of Patient Enrollment Id and show the details.*******//
    //    FormManager = (IAppointment)ObjectFactory.CreateInstance("BusinessProcess.Scheduler.BAppointment, BusinessProcess.Scheduler");
    //    theDtSet = FormManager.GetAppointmentReasons(idPurpose);

    //    appBind = new BindFunctions();
    //    appBind.BindCombo(ddAppPurpose, theDtSet.Tables[0], "Name", "Id");

    //    theDtSetPurpose = FormManager.GetEmployees(idEmployee);
    //    appBind = new BindFunctions();
    //    appBind.BindCombo(ddAppProvider, theDtSetPurpose.Tables[0], "EmployeeName", "EmployeeId");

    //}
    #endregion


    private void BindDropdownOrderBy(String EmployeeId)
    {


        IAppointment FormManager;
        FormManager = (IAppointment)ObjectFactory.CreateInstance("BusinessProcess.Scheduler.BAppointment, BusinessProcess.Scheduler");

        DataSet theDtSet = FormManager.GetEmployees(0);


        IQCareUtils theUtils = new IQCareUtils();


        DataView theDV = new DataView(theDtSet.Tables[0]);

        theDV.RowFilter = "DeleteFlag=0";

        if (theDV.Table != null)
        {
            DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);

            if (Convert.ToInt32(Session["AppUserEmployeeId"]) > 0)
            {



                string theModList = "";
                foreach (DataRow theDR in theDT.Rows)
                {
                    if (theModList == "")
                        theModList = theDR["EmployeeId"].ToString();
                    else
                        theModList = theModList + "," + theDR["EmployeeId"].ToString();
                }



                theDV.RowFilter = "EmployeeId IN(" + Session["AppUserEmployeeId"].ToString() + "," + theModList + ")";
                if (theDV.Count > 0)
                    theDT = theUtils.CreateTableFromDataView(theDV);
            }
            BindFunctions BindManager = new BindFunctions();
            //BindManager.BindCombo(ddAppProvider, theDT, "EmployeeName", "EmployeeId");
            //BindUserDropdown(ddAppProvider, string.Empty);
            BindUserDropdown(ddAppProvider, Session["AppUserId"].ToString());


        }


    }

    private void BindUserDropdown(DropDownList DropDownID, String userId)
    {
        Dictionary<int, string> userList = new Dictionary<int, string>();
        CustomFieldClinical.BindUserDropDown(DropDownID, out userList);
        if (!string.IsNullOrEmpty(userId))
        {
            if (userList.ContainsKey(Convert.ToInt32(userId)))
            {
                DropDownID.SelectedValue = userId;
                //SecurityPerTabSignature = userId;
            }
        }
    }
    private Boolean checkEntriesShowMessages()
    {
        //*******Show a message to fill the essential details *******//
        IQCareUtils theUtil = new IQCareUtils();
        MsgBuilder theMsg = new MsgBuilder();
        if (txtAppDate.Text == "")
        {
            theMsg.DataElements["Control"] = "Appointment Date";
            IQCareMsgBox.Show("BlankTextBox", theMsg, this);
            return false;
        }
        if (ddAppProvider.SelectedValue == "0")
        {

            theMsg.DataElements["Control"] = "Provider";
            IQCareMsgBox.Show("BlankDropDown", theMsg, this);
            return false;
        }

        if (ddAppPurpose.SelectedValue == "0")
        {
            theMsg.DataElements["Control"] = "Purpose";
            IQCareMsgBox.Show("BlankDropDown", theMsg, this);
            return false;
        }

        return true;
    }

    private bool AppointmentExist()
    {
        int visitid = 0;
        DataTable theDt;
        IAppointment FormManager;
        IQCareUtils theUtils = new IQCareUtils();
        DateTime temp = Convert.ToDateTime(theUtils.MakeDate(txtAppDate.Text));

        FormManager = (IAppointment)ObjectFactory.CreateInstance("BusinessProcess.Scheduler.BAppointment, BusinessProcess.Scheduler");

        //*******Check if user is editing and existing appointment then also send the visit id of the appointment*******//
        //if (Request.QueryString["PatientVisitID"] != null)
        //{
        //    visitid = Convert.ToInt32(Request.QueryString["PatientVisitID"]);
        //}


        if (Session["PatientVisitId"] != null)
        {
            visitid = Convert.ToInt32(Session["PatientVisitId"]);
        }
        //updated by-Nidhi
        //Descr-Changing datetime to string
        //theDt = (DataTable)FormManager.CheckAppointmentExistance(Convert.ToInt32(patientId), Convert.ToInt32(Session["AppLocationId"]), temp, Convert.ToInt32(ddAppPurpose.SelectedValue), visitid);
        theDt = (DataTable)FormManager.CheckAppointmentExistance(Convert.ToInt32(patientId), Convert.ToInt32(Session["AppLocationId"]), txtAppDate.Text, Convert.ToInt32(ddAppPurpose.SelectedValue), visitid);
        if (Convert.ToInt32(theDt.Rows[0][0]) > 0)
        {
            return true;
        }


        return false;
    }

    private bool checkDate()
    {
        DateTime temp;
        IInitialEval IEManager;

        try
        {
            IQCareUtils theUtils = new IQCareUtils();
            temp = Convert.ToDateTime(theUtils.MakeDate(txtAppDate.Text));

            IEManager = (IInitialEval)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BInitialEval, BusinessProcess.Clinical");

            DataSet DSEnrolment = IEManager.GetClinicalDate(Convert.ToInt32(patientId), 0);
            if (Convert.ToInt32(DSEnrolment.Tables[0].Rows[0]["Existflag"]) == 1)
            {
                DateTime EnrolDate = Convert.ToDateTime(DSEnrolment.Tables[1].Rows[0]["VisitDate"]);
                EnrolDate = Convert.ToDateTime(EnrolDate.ToString(Session["AppDateFormat"].ToString()));
                if (EnrolDate.ToString() != "" && txtAppDate.Text != "")
                {
                    //if (Convert.ToDateTime(txtAppDate.Text) < EnrolDate)
                    if (temp < EnrolDate)
                    {
                        IQCareMsgBox.Show("EnrolmentScheduler_Date", this);
                        return false;

                    }

                }
            }
            return true;
        }
        catch
        {
            IQCareMsgBox.Show("InvalidDate", this);
            return false;
        }
    }
    #region "Modified13June07(2)"
    #region "Modified20June07(7)"
    private void fillThePatientAppointmentDetails()
    {
        DataSet theDs;
        IAppointment FormManager;
        IQCareUtils theUtil = new IQCareUtils();
        string selectValue1, selectValue2;
        FormManager = (IAppointment)ObjectFactory.CreateInstance("BusinessProcess.Scheduler.BAppointment, BusinessProcess.Scheduler");
        //theDs = FormManager.GetPatientppointmentDetails(patientId, Convert.ToInt32(Request.QueryString["LocationID"]), Convert.ToInt32(Request.QueryString["PatientVisitID"]));
        theDs = FormManager.GetPatientppointmentDetails(patientId, Convert.ToInt32(Session["AppLocationId"]), Convert.ToInt32(Session["PatientVisitId"]));

        if (theDs.Tables[0].Rows.Count > 0)
        {
            if (Convert.ToInt32(Session["AppLocationId"]) == 0 && Convert.ToInt32(Session["PatientVisitId"]) == 0)
            {
                if (theDs.Tables[0].Rows[0]["EmployeeId"].ToString() == "")
                {
                    theDs.Tables[0].Rows[0]["EmployeeId"] = "0";
                }
                txtAppDate.Text = String.Format("{0:dd-MMM-yyyy}", theDs.Tables[0].Rows[0]["AppDate"]);
                if (theDs.Tables[0].Rows[0]["AppReason"].ToString() == "")
                {
                    ddAppPurpose.SelectedValue = "0";
                    selectValue1 = "0";
                }
                else
                {
                    ddAppPurpose.SelectedValue = theDs.Tables[0].Rows[0]["AppReason"].ToString();
                    selectValue1 = theDs.Tables[0].Rows[0]["AppReason"].ToString();
                }
                if (theDs.Tables[0].Rows[0]["EmployeeId"].ToString() == "")
                {
                    ddAppProvider.SelectedValue = "0";
                    selectValue2 = "0";
                }
                else
                {
                    ddAppProvider.SelectedValue = theDs.Tables[0].Rows[0]["EmployeeId"].ToString();
                    selectValue2 = theDs.Tables[0].Rows[0]["EmployeeId"].ToString();
                }

            }
            //if ((ddAppPurpose.SelectedValue == "0") || (ddAppProvider.SelectedValue == "0"))
            //{

            //    fillDropDownList(Convert.ToInt32(selectValue1), Convert.ToInt32(selectValue2));
            //    ddAppPurpose.SelectedValue = selectValue1;
            //    ddAppProvider.SelectedValue = selectValue2;
            //}

        }
    }
    #endregion
    #endregion
    private void getPatientDetails(int patientId)
    {
        IDeletePatient FormManager;
        //*******Get the patient details on the basis of Patient Enrollment Id and show the details.*******//
        FormManager = (IDeletePatient)ObjectFactory.CreateInstance("BusinessProcess.Administration.BDeletePatient, BusinessProcess.Administration");
        //DataTable theDT = FormManager.GetPatientDetailsByEnrollment(theEnrollID);
        DataTable theDT = FormManager.GetPatientDetails(patientId);
        if (theDT.Rows.Count != 0)
        {
            //lblPatientNameValue.Text = theDT.Rows[0]["Name"].ToString();
            //lblHospitalNoValue.Text = theDT.Rows[0]["PatientClinicID"].ToString();
            //theEnrollID = theDT.Rows[0]["PatientEnrollmentID"].ToString();
            //lblPatientEnrollmentNoValue.Text = Session["AppCountryId"].ToString() + "-" + Session["AppPosID"].ToString() + "-" + Session["AppSatelliteId"].ToString() + "-" + theEnrollID.ToString();
            //patientId = Convert.ToInt32(theDT.Rows[0]["ptn_pk"].ToString());
            //if (theDT.Rows[0]["Sex"].ToString() == "16")
            //{
            //    lblPatientGenderValue.Text = "Male";
            //}
            //else
            //{
            //    lblPatientGenderValue.Text = "Female";
            //}
            //*******Check whether the patient is inactive*******//
            //*******Inactive patient show message and disable  delete*******//
            if (theDT.Rows[0]["status"].ToString() == "1")
            {
                string script = "<script language = 'javascript' defer ='defer' id = 'confirm1'>\n";
                script += "var ans=true;\n";
                script += "alert('Patient is Inactive');\n";
                script += "</script>\n";
                RegisterClientScriptBlock("confirm1", script);
                btndelete.Enabled = false;
                btnSubmit.Enabled = false;
            }
            else
            {
                btndelete.Enabled = true;
            }
        }
    }
    private void deleteAppointment()
    {
        //*******Delete the patient on the basis of patient id ********//
        IAppointment FormManager;
        FormManager = (IAppointment)ObjectFactory.CreateInstance("BusinessProcess.Scheduler.BAppointment, BusinessProcess.Scheduler");
        //int theResultRow = FormManager.DeletePatientAppointmentDetails(Convert.ToInt32(Request.QueryString["PatientId"]), Convert.ToInt32(Request.QueryString["LocationID"]), Convert.ToInt32(Request.QueryString["PatientVisitID"]));
        int theResultRow = FormManager.DeletePatientAppointmentDetails(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["AppLocationId"]), Convert.ToInt32(Session["PatientVisitId"]));

        if (theResultRow == 0)
        {
            IQCareMsgBox.Show("DeletePatientError", this);
            return;
        }
        else
        {
            string theUrl;
            theUrl = string.Format("{0}", "./frmScheduler_AppointmentMain.aspx");
            if (Request.QueryString["FormName"] == "Appointment History")
            {
                theUrl = string.Format("{0}?FormName={1}", "../Scheduler/frmScheduler_AppointmentHistory.aspx?name=Add", "PatientHome");

            }
            Response.Redirect(theUrl);
            //Server.Transfer(theUrl);
        }
    }
    private DateTime getCurrentDate()
    {
        IInitialEval IEManager;
        DataSet dr;
        IEManager = (IInitialEval)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BInitialEval, BusinessProcess.Clinical");
        dr = IEManager.GetCurrentDate();
        DateTime dt = Convert.ToDateTime(dr.Tables[0].Rows[0]["CurrentDay"]);

        IQCareUtils theUtil = new IQCareUtils();
        dt = Convert.ToDateTime(dt.ToString(Session["AppDateFormat"].ToString()));


        return dt;
    }
    private void fillPatientAppointmntDetailsInGrid()
    {
        int locationId;
        //if (Request.QueryString["Patientid"] != null)
        if (Session["PatientId"] != null)
        {
            patientId = Convert.ToInt32(Session["PatientId"]);
            locationId = Convert.ToInt32(Session["AppLocationId"]);

            DataSet theDs;
            IAppointment FormManager;
            IQCareUtils theUtil = new IQCareUtils();
            FormManager = (IAppointment)ObjectFactory.CreateInstance("BusinessProcess.Scheduler.BAppointment, BusinessProcess.Scheduler");
            theDs = FormManager.GetPatientppointmentDetails(patientId, Convert.ToInt32(locationId), 0);


            grdSearchResult.Columns.Clear();
            grdSearchResult.DataSource = theDs.Tables[1];

            if (!IsPostBack)
            {
                ViewState["GrdData"] = theDs.Tables[1];
                ViewState["SortDirection"] = "Asc";

            }

            BindGrid();
        }
    }

    private void BindGrid()
    {
        BoundField theCol0 = new BoundField();
        theCol0.HeaderText = "Appointment Date";
        theCol0.DataField = "Appointment Date";
        theCol0.SortExpression = "Appointment Date";
        theCol0.ItemStyle.CssClass = "textstyle";
        theCol0.ReadOnly = true;

        BoundField theCol1 = new BoundField();
        theCol1.HeaderText = "Met Date";
        theCol1.DataField = "Met Date";
        theCol1.SortExpression = "Met Date";
        theCol1.ItemStyle.CssClass = " textstyle";
        theCol1.ReadOnly = true;

        BoundField theCol2 = new BoundField();
        theCol2.HeaderText = "Status";
        theCol2.DataField = "Status";
        theCol2.ItemStyle.CssClass = "textstyle";
        theCol2.SortExpression = "Status";
        theCol2.ItemStyle.Font.Underline = true;
        theCol2.ReadOnly = true;

        BoundField theCol3 = new BoundField();
        theCol3.HeaderText = "PurPose";
        theCol3.ItemStyle.CssClass = "Purpose";
        theCol3.DataField = "PurPose";
        theCol3.SortExpression = "PurPose";
        theCol3.ReadOnly = true;

        BoundField theCol4 = new BoundField();
        theCol4.HeaderText = "Appdate";
        theCol4.ItemStyle.CssClass = "textstyle";
        theCol4.DataField = "Appdate";
        theCol4.SortExpression = "Appdate";
        theCol4.ReadOnly = true;

        ButtonField theBtn = new ButtonField();
        theBtn.ButtonType = ButtonType.Link;
        theBtn.CommandName = "Select";
        theBtn.HeaderStyle.CssClass = "textstylehidden";
        theBtn.ItemStyle.CssClass = "textstylehidden";

        BoundField theCol6 = new BoundField();
        theCol6.HeaderText = "ProviderID";
        theCol6.ItemStyle.CssClass = "textstylehidden";
        theCol6.HeaderStyle.CssClass = "textstylehidden";
        theCol6.DataField = "ProviderID";
        theCol6.SortExpression = "ProviderID";
        theCol6.ReadOnly = true;

        BoundField theCol7 = new BoundField();
        theCol7.HeaderText = "PurPose";
        theCol7.ItemStyle.CssClass = "Purpose";
        theCol7.DataField = "PurPoseID";
        theCol7.SortExpression = "PurPoseID";
        theCol7.ReadOnly = true;

        BoundField theCol8 = new BoundField();
        theCol8.HeaderText = "Visit_pk";
        theCol8.ItemStyle.CssClass = "textstylehidden";
        theCol8.DataField = "Visit_pk";
        theCol8.SortExpression = "Visit_pk";
        theCol8.ReadOnly = true;


        grdSearchResult.Columns.Add(theCol0);
        grdSearchResult.Columns.Add(theCol1);
        grdSearchResult.Columns.Add(theCol2);
        grdSearchResult.Columns.Add(theCol3);
        grdSearchResult.Columns.Add(theCol4);

        grdSearchResult.Columns.Add(theBtn);
        grdSearchResult.Columns.Add(theCol6);
        grdSearchResult.Columns.Add(theCol7);
        grdSearchResult.Columns.Add(theCol8);

        grdSearchResult.DataBind();
        grdSearchResult.Columns[4].Visible = false;
        grdSearchResult.Columns[6].Visible = false;
        grdSearchResult.Columns[7].Visible = false;
        grdSearchResult.Columns[8].Visible = false;
    }

    #endregion

    int patientId;
    string theEnrollID;

    protected void Page_PreInit(object sender, EventArgs e)
    {

    }
    #region "Modified13June07(3)"
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
        {
            IQCareMsgBox.Show("SessionExpired", this);
            Response.Redirect("~/frmlogin.aspx", true);
        }
        GblIQCare.Scheduler = 1;
        ////theEnrollID = Request.QueryString["PatientEnrollmentID"].ToString();
        if (Session["PatientEnrollmentID"] != null)
            theEnrollID = Session["PatientEnrollmentID"].ToString();

        //getPatientDetails(theEnrollID);
        //Session["PatientId"]
        //patientId = Convert.ToInt32(Request.QueryString["PatientId"]);
        patientId = Convert.ToInt32(Session["PatientId"]);
        getPatientDetails(patientId);
        txtAppDate.Attributes.Add("onKeyup", "DateFormat(this,this.value,event,false,'3')");
        txtAppDate.Attributes.Add("onBlur", "CheckDate(this);");
        txtNoOfDays.Attributes.Add("OnBlur", "noOfDays('" + txtNoOfDays.ClientID + "','" + txtAppDate.ClientID + "');");

        //(Master.FindControl("lblRoot") as Label).Text = "Clinical Forms >>";
        //(Master.FindControl("lblMark") as Label).Text = "»";
        //(Master.FindControl("lblMark") as Label).Visible = false;
        //(Master.FindControl("lblheader") as Label).Text = "New Appointment";
        //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Clinical Forms >> ";
        //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "New Appointment";
        //BindHeader();

        //Session["PatientStatus"].ToString = Convert.ToInt32(theValue[3]);

        //if (Request.QueryString["sts"] != null)
        if (Session["PatientStatus"] != null)
        {
            //(Master.FindControl("lblpntStatus") as Label).Text = Request.QueryString["sts"].ToString();
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text = Convert.ToString(Session["PatientStatus"]);

        }


        if (Request.QueryString["FormName"] != null)
        {
            if ((Request.QueryString["FormName"] == "EditDelete") || (Request.QueryString["FormName"] == "Appointment History"))
            {
                //(Master.FindControl("lblheader") as Label).Text = "Edit Appointment";
                //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Edit Appointment";
                header.InnerText = "Edit Appointment";
                btnSubmit.Text = "Update";
                btndelete.Visible = true;
                gly_btndelete.Visible = true;
            }

        }


        if (!IsPostBack)
        {
            //*******Fill purpose and provider dropdownlists*******//
            fillDropDownList(0, 0);
            fillPatientAppointmntDetailsInGrid();
            if (Request.QueryString["FormName"] != null)
            {
                if ((Request.QueryString["FormName"] == "EditDelete") || (Request.QueryString["FormName"] == "Appointment History"))
                {
                    //*******Get all the appointment details of selected patient and show the information in the form*******//
                    fillThePatientAppointmentDetails();

                }

            }


            //FillAppDetails((GridViewRow)Session["PatientAppDetails"]);
            //GridViewRow row = grdSearchResult.Rows[e.NewSelectedIndex];
            //GridViewRow row =(GridViewRow) Session["AppPatDtl"];
            if (IsPostBack)
            {
                FillAppDetails();
            }

        }
        Form.EnableViewState = true;

        if (Request.QueryString["opento"] == "popup")
        {
            ifPopUp();
        }
    }

    #endregion

    public void ifPopUp()
    {
        Master.FindControl("levelOneNavigationUserControl1").Visible = false;
        Master.FindControl("levelTwoNavigationUserControl1").Visible = false;
        //Master.FindControl("lnkHelp").Visible = false;
        Master.FindControl("lnkPassword").Visible = false;
        Master.FindControl("lnkReportDefect").Visible = false;
        Master.FindControl("lnkLogOut").Visible = false;
        //Master.FindControl("username1").Visible = false;
        //Master.FindControl("currentdate1").Visible = false;
        //Master.FindControl("facilityName").Visible = false;
        //Master.FindControl("imageFlipLevel2").Visible = false;
        //Master.FindControl("lblSeperator1").Visible = false;
        //Master.FindControl("lblSeperator2").Visible = false;
        //Master.FindControl("lblSeperator3").Visible = false;
        //lblFormName.Visible = false;
        //Updatepanel.FindControl("patientInfoTop").Visible = false;
        btnBack.Visible = false;
    }
    //private void BindHeader()
    //{
    //    DataSet theFacilityDS = new DataSet();
    //    AuthenticationManager Authentication = new AuthenticationManager();
    //    IFacilitySetup FacilityMaster = (IFacilitySetup)ObjectFactory.CreateInstance("BusinessProcess.Administration.BFacility, BusinessProcess.Administration");

    //    theFacilityDS = FacilityMaster.GetSystemBasedLabels(Convert.ToInt32(Session["SystemId"]), ApplicationAccess.SchedularAppointment,0);

    //    DataTable theDT = theFacilityDS.Tables[0];
    //    lblpatientenrol.InnerHtml = theDT.Rows[0]["Label"].ToString() + ":";
    //    lblExisclinicID.InnerHtml = theDT.Rows[1]["Label"].ToString() + ":";


    //}
    protected void grdSearchResult_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[2].Text == "Pending")
            {
                for (int i = 0; i < 4; i++)
                {
                    e.Row.Cells[i].Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.BackColor='#666699';");
                    e.Row.Cells[i].Attributes.Add("onmouseout", "this.style.backgroundColor='';");
                    e.Row.Cells[i].Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(grdSearchResult, "Select$" + e.Row.RowIndex.ToString()));
                }

                if ((Session["patientappdtl"] == "1") && (btnSubmit.Text == "Update"))
                {
                    FillAppDetails();
                }
            }
            //DataRow dr;
            //dr = (DataRow)Session["PatientAppDetails"];
            //FillAppDetails(dr);
        }
    }
    protected void grdSearchResult_Sorting(object sender, GridViewSortEventArgs e)
    {
        IQCareUtils clsUtil = new IQCareUtils();
        DataView theDV;

        if (e.SortExpression == "Appointment Date")
        {
            e.SortExpression = "Appdate";
        }
        if (ViewState["SortDirection"].ToString() == "Asc")
        {
            theDV = clsUtil.GridSort((DataTable)ViewState["GrdData"], e.SortExpression, ViewState["SortDirection"].ToString());
            ViewState["SortDirection"] = "Desc";
        }
        else
        {
            theDV = clsUtil.GridSort((DataTable)ViewState["GrdData"], e.SortExpression, ViewState["SortDirection"].ToString());
            ViewState["SortDirection"] = "Asc";
        }
        grdSearchResult.Columns.Clear();
        grdSearchResult.DataSource = theDV;
        BindGrid();

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        IAppointment FormManager;
        int theResultRow = 0;
        DateTime currentDate;
        Boolean editData = false;
        DateTime enteredDate;
        try
        {
            //*******First check whether the appointment date, purpose and provider is being selected*******//
            IQCareUtils theUtil = new IQCareUtils();
            if (checkEntriesShowMessages())
            {
                //*******Save all the details and create a new appointment*******//
                if (checkDate())
                {
                    if (AppointmentExist())
                    {
                        IQCareMsgBox.Show("AppointmentExist", this);
                        return;
                    }
                    else
                    {
                        //First check that appdate must be with in a year not greter then that
                        currentDate = getCurrentDate();
                        //enteredDate = Convert.ToDateTime(txtAppDate.Text);


                        enteredDate = Convert.ToDateTime(theUtil.MakeDate(txtAppDate.Text));

                        if (currentDate.AddYears(1) < enteredDate)
                        {
                            IQCareMsgBox.Show("AppointmentWithInAYear", this);
                            return;

                        }
                        if (Request.QueryString["FormName"] != null)
                        {
                            if ((Request.QueryString["FormName"] == "EditDelete") || (Request.QueryString["FormName"] == "Appointment History"))
                            {
                                //*******if redirected from appointment edit/delete then update the data*******//
                                FormManager = (IAppointment)ObjectFactory.CreateInstance("BusinessProcess.Scheduler.BAppointment, BusinessProcess.Scheduler");

                                //theResultRow = FormManager.UpdatePatientppointmentDetails(Convert.ToInt32(patientId), Convert.ToInt32(Request.QueryString["LocationID"]), Convert.ToInt32(Request.QueryString["PatientVisitID"]), Convert.ToDateTime(txtAppDate.Text), Convert.ToInt32(ddAppPurpose.SelectedValue),Convert.ToInt32(Session["AppUserId"]), Convert.ToInt32(ddAppProvider.SelectedValue), currentDate);
                                //update by-nidhi
                                //Desc- changing datetime to string
                                //theResultRow = FormManager.UpdatePatientppointmentDetails(Convert.ToInt32(patientId), Convert.ToInt32(Session["AppLocationId"]), Convert.ToInt32(Session["PatientVisitId"]), enteredDate, Convert.ToInt32(ddAppPurpose.SelectedValue), Convert.ToInt32(Session["AppUserId"]), Convert.ToInt32(ddAppProvider.SelectedValue), currentDate);
                                theResultRow = FormManager.UpdatePatientppointmentDetails(Convert.ToInt32(patientId), Convert.ToInt32(Session["AppLocationId"]), Convert.ToInt32(Session["PatientVisitId"]), txtAppDate.Text, Convert.ToInt32(ddAppPurpose.SelectedValue), Convert.ToInt32(Session["AppUserId"]), Convert.ToInt32(ddAppProvider.SelectedValue), currentDate.ToString("dd-MMM-yyyy"));
                                editData = true;

                            }
                        }
                        if (!editData)
                        {
                            //*******if redirected from appointment main then save the data*******//
                            FormManager = (IAppointment)ObjectFactory.CreateInstance("BusinessProcess.Scheduler.BAppointment, BusinessProcess.Scheduler");
                            theResultRow = FormManager.SaveAppointment(Convert.ToInt32(patientId), Convert.ToInt32(Session["AppLocationId"]), txtAppDate.Text, Convert.ToInt32(ddAppPurpose.SelectedValue), Convert.ToInt32(ddAppProvider.SelectedValue), Convert.ToInt32(Session["AppUserId"]), Application["AppCurrentDate"].ToString());

                        }
                        if (theResultRow == 0)
                        {
                            IQCareMsgBox.Show("AppointmentCreationError", this);
                            return;
                        }
                        else
                        {
                            if (Request.QueryString["opento"] == "popup")
                            {
                                selfClose();
                            }
                            else
                            {
                                string theUrl;
                                theUrl = string.Format("{0}", "./frmScheduler_AppointmentMain.aspx?name=Add");

                                if (Request.QueryString["FormName"] != null)
                                {
                                    if (Request.QueryString["FormName"] == "PatientHome")
                                    {
                                        theUrl = string.Format("{0}", "../ClinicalForms/frmPatient_Home.aspx?name=Add");

                                    }

                                    if ((Request.QueryString["FormName"] == "Appointment History") || (Request.QueryString["FormName"] == "Appointment History New"))
                                    {
                                        //Session["PatientId"]
                                        //theUrl = string.Format("{0}&PatientId={1}&LocationId={2}&FormName={3}&sts={4}", "../Scheduler/frmScheduler_AppointmentHistory.aspx?name=Add", Convert.ToInt32(Request.QueryString["PatientId"]), Session["AppLocationId"].ToString(), "PatientHome", Request.QueryString["sts"].ToString());
                                        theUrl = string.Format("{0}&FormName={1}", "../Scheduler/frmScheduler_AppointmentHistory.aspx?name=Add", "PatientHome");

                                    }
                                }
                                Response.Redirect(theUrl);
                                //Server.Transfer(theUrl);
                            }
                        }
                    }
                }
            }
            else
            {
                return;
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
            FormManager = null;
        }

    }

    public void selfClose()
    {
        if (Request.QueryString["opento"] == "popup")
        {
            string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
            script += "alert('Appointment saved successfully.');\n";
            script += "self.close();\n";
            script += "</script>\n";
            RegisterStartupScript("confirm", script);
        }
    }
    protected void btndelete_Click(object sender, EventArgs e)
    {
        //*******Ask for confirmation and then delete the patient appointment*******//

        string script = "<script language = 'javascript' defer ='defer' id = 'aftersavefunction'>\n";
        script += "var ans;\n";
        script += "ans=window.confirm('Are you sure you want to delete this patient appointment ?');\n";
        script += "if (ans==true)\n";
        script += "{\n";
        script += "document.getElementById('" + theBtn.ClientID + "').click();\n";
        script += "}\n";
        script += "</script>\n";
        RegisterStartupScript("aftersavefunction", script);
        return;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        string theUrl = "";

        theUrl = string.Format("{0}", "./frmScheduler_AppointmentMain.aspx?name=Add");

        if (Request.QueryString["FormName"] != null)
        {
            if (Request.QueryString["FormName"] == "PatientHome")
            {
                theUrl = string.Format("../ClinicalForms/frmPatient_Home.aspx");

            }

            if ((Request.QueryString["FormName"] == "Appointment History") || (Request.QueryString["FormName"] == "Appointment History New"))
            {
                theUrl = string.Format("{0}&FormName={1}", "../Scheduler/frmScheduler_AppointmentHistory.aspx?name=Add", "PatientHome");

            }
        }
        Response.Redirect(theUrl);
        //Server.Transfer(theUrl);

    }
    protected void theBtn_Click(object sender, EventArgs e)
    {
        deleteAppointment();
    }

    protected void grdSearchResult_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        GridViewRow row = grdSearchResult.Rows[e.NewSelectedIndex];
        txtAppDate.Text = row.Cells[0].Text;
        if (row.Cells[7].Text != "&nbsp;")
        {
            ddAppPurpose.SelectedValue = row.Cells[7].Text;
        }
        Session["PatientVisitId"] = row.Cells[8].Text;
        if (row.Cells[6].Text != "&nbsp;")
        {
            ddAppProvider.SelectedValue = row.Cells[6].Text;
        }
        //ViewState["VisitID"] = row.Cells[7].Text;
        btnSubmit.Text = "Update";

    }

    protected void FillAppDetails()
    {
        //GridViewRow row = grdSearchResult.Rows[e.NewSelectedIndex];
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["GrdData"];
        int rowno = Convert.ToInt32(Session["AppPatDtl"]);
        if (dt.Rows.Count > 0)
        {
            if (rowno >= 0)
            {
                txtAppDate.Text = dt.Rows[rowno][0].ToString();

                //ddAppPurpose.SelectedItem.Text = dt.Rows[rowno][8].ToString(); 
                ddAppPurpose.SelectedValue = dt.Rows[rowno][10].ToString();
                Session["PatientVisitId"] = dt.Rows[rowno][6].ToString();

                ddAppProvider.SelectedValue = dt.Rows[rowno][11].ToString();
                //ViewState["VisitID"] = row.Cells[7].Text;
                btnSubmit.Text = "Update";
            }

        }


    }
    public void clearControl()
    {
        txtAppDate.Text = "";
        ddAppPurpose.SelectedIndex = 0;
        ddAppProvider.SelectedIndex = 0;
        btnSubmit.Text = "Save";


    }


}
