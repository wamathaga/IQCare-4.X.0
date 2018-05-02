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
using Interface.Security;
using Interface.Reports;
using Interface.Scheduler;
using Interface.Clinical;
using Interface.Administration;
using Application.Presentation;
using Application.Common;

public partial class frmScheduler_AppointmentMain : LogPage
{
    //public frmScheduler_AppointmentMain()
    //{
    //    this.PreInit += new EventHandler(frmScheduler_AppointmentMain_PreInit);
    //}
    String currentDate;
    IAppointment FormManager;
    #region "User Functions"

    private void setFromToDateAndShowData()
    {
        DateTime currDateType;
        currentDate = string.Format("{0:dd/mm/yyyy}", Application["AppCurrentDate"].ToString());
        currDateType = Convert.ToDateTime(currentDate);

        fillDropDownList();
        txtFrom.Attributes.Add("onKeyup", "DateFormat(this,this.value,event,false,'3')");
        txtTo.Attributes.Add("onKeyup", "DateFormat(this,this.value,event,false,'3')");

        //btnNewAppointment.CssClass = "greenbutton";

        EventArgs evnt;
        evnt = new EventArgs();
        if (Request.QueryString["AppointmentStatus"] != null)
        {
            if (Request.QueryString["AppointmentStatus"] == "Pending")
            {
                ddAppointmentStatus.Attributes.Add("onchange", "ResetDateAndGrid(" + currDateType.Year + "," + currDateType.Month + "," + currDateType.Day + "," + "'FacilityHomePending'" + ")");
                //txtFrom.Value = currentDate.ToString();
                //txtTo.Value = currentDate.ToString();
                ddAppointmentStatus.SelectedValue = "12";
            }
            else if (Request.QueryString["AppointmentStatus"] == "Missed")
            {
                ddAppointmentStatus.Attributes.Add("onchange", "ResetDateAndGrid(" + currDateType.Year + "," + currDateType.Month + "," + currDateType.Day + "," + "'FacilityHomeMissed'" + ")");
                ddAppointmentStatus.SelectedValue = "13";
            }
            else
            {
                //txtFrom.Value = currentDate.ToString();
                //txtTo.Value = currentDate.ToString();
                ddAppointmentStatus.Attributes.Add("onchange", "ResetDateAndGrid(" + currDateType.Year + "," + currDateType.Month + "," + currDateType.Day + "," + "'FacilityHomeMissed'" + ")");
                ddAppointmentStatus.SelectedValue = "15";
            }
        }
        else
        {
            ddAppointmentStatus.Attributes.Add("onchange", "ResetDateAndGrid(" + currDateType.Year + "," + currDateType.Month + "," + currDateType.Day + "," + "'Scheduler'" + ")");
            ddAppointmentStatus.SelectedValue = "12";
        }
        //btnSubmit_Click(btnSubmit, evnt);
    }

    private void fillDropDownList()
    {
        IAppointment FormManager;
        DataSet theDtSet;

        //*******Get the patient details on the basis of Patient Enrollment Id and show the details.*******//
        FormManager = (IAppointment)ObjectFactory.CreateInstance("BusinessProcess.Scheduler.BAppointment, BusinessProcess.Scheduler");
        theDtSet = FormManager.GetAppointmentStatus();
        ddAppointmentStatus.DataSource = theDtSet.Tables[6];
        ddAppointmentStatus.DataTextField = "Name";
        ddAppointmentStatus.DataValueField = "Id";
        ddAppointmentStatus.SelectedIndex = 2;

        ddAppointmentStatus.DataBind();


    }

    private void getCurrentDate()
    {
        IInitialEval IEManager;
        DataSet dr;
        IEManager = (IInitialEval)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BInitialEval, BusinessProcess.Clinical");
        dr = IEManager.GetCurrentDate();
        DateTime dt = Convert.ToDateTime(dr.Tables[0].Rows[0]["CurrentDay"]);

        IQCareUtils theUtil = new IQCareUtils();
        dt = Convert.ToDateTime(dt.ToString(Session["AppDateFormat"].ToString()));

        currentDate = dt.ToString(Session["AppDateFormat"].ToString());

    }

    private DataSet fillDataSet()
    {

        DataSet theDtSet;
        IQCareUtils clsUtil = new IQCareUtils();

        FormManager = (IAppointment)ObjectFactory.CreateInstance("BusinessProcess.Scheduler.BAppointment, BusinessProcess.Scheduler");
        IQCareUtils theUtil = new IQCareUtils();

        theDtSet = new DataSet();
        //******Selected value is pending and missed(All)*******//
        if ((ddAppointmentStatus.SelectedValue == "12") || (ddAppointmentStatus.SelectedValue == "13"))//******Selected value is pending or Missed*******//
        {
            if ((txtFrom.Value.Trim() == "") && (txtTo.Value.Trim() == ""))
            {
                if (ddAppointmentStatus.SelectedValue == "12")
                {
                    theDtSet = FormManager.GetAppointmentGrid(1, "01-Jan-1900", "01-Jan-1900", Convert.ToInt32(Session["AppLocationId"]));

                }
                else
                {
                    theDtSet = FormManager.GetAppointmentGrid(2, "01-Jan-1900", "01-Jan-1900", Convert.ToInt32(Session["AppLocationId"]));
                }

            }
            else
            {
                if (checkDate())
                {
                    if (ddAppointmentStatus.SelectedValue == "12")
                    {
                        theDtSet = FormManager.GetAppointmentGrid(1, Convert.ToDateTime(txtFrom.Value).ToString("dd-MMM-yyy"), Convert.ToDateTime(txtTo.Value).ToString("dd-MMM-yyy"), Convert.ToInt32(Session["AppLocationId"]));
                    }
                    else
                    {
                        theDtSet = FormManager.GetAppointmentGrid(2, Convert.ToDateTime(txtFrom.Value).ToString("dd-MMM-yyy"), Convert.ToDateTime(txtTo.Value).ToString("dd-MMM-yyy"), Convert.ToInt32(Session["AppLocationId"]));
                    }

                }
            }

        }
        else
        {
            if (checkDate())
            {
                theDtSet = FormManager.GetAppointmentGrid(3, Convert.ToDateTime(txtFrom.Value).ToString("dd-MMM-yyy"), Convert.ToDateTime(txtTo.Value).ToString("dd-MMM-yyy"), Convert.ToInt32(Session["AppLocationId"]));

            }

        }
        grdSearchResult.Columns.Clear();
        grdSearchResult.DataSource = null;

        return theDtSet;
    }
    private bool checkDate()
    {
        try
        {
            if ((txtFrom.Value.ToString() == "") || (txtTo.Value.ToString() == ""))
            {
                if ((ddAppointmentStatus.SelectedValue == "12") || (ddAppointmentStatus.SelectedValue == "13"))//******Selected value is pending or Missed*******//
                {
                    IQCareMsgBox.Show("CompleteDateRange", this);
                    return false;
                }
                else
                {
                    IQCareMsgBox.Show("DatesRequired", this);
                    return false;
                }

            }
            else
            {
                if (Convert.ToDateTime(txtFrom.Value) > Convert.ToDateTime(txtTo.Value))
                {
                    IQCareMsgBox.Show("StartEndDate", this);
                    txtTo.Value = txtFrom.Value;
                    return false;
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
    private void BindGrid()
    {
        BoundField theCol = new BoundField();
        theCol.HeaderText = "First Name";
        theCol.DataField = "firstname";
        theCol.ItemStyle.CssClass = "textstyle";
        theCol.SortExpression = "firstname";
        theCol.ItemStyle.Font.Underline = true;
        theCol.ReadOnly = true;

        BoundField theCol0 = new BoundField();
        theCol0.HeaderText = "Middle Name";
        theCol0.DataField = "MiddleName";
        theCol0.ItemStyle.CssClass = "textstyle";
        theCol0.SortExpression = "MiddleName";
        theCol0.ItemStyle.Font.Underline = true;
        theCol0.ReadOnly = true;

        BoundField theCol1 = new BoundField();
        theCol1.HeaderText = "Last Name";
        theCol1.DataField = "lastname";
        theCol1.SortExpression = "lastname";
        theCol1.ItemStyle.CssClass = " textstyle";
        theCol1.ReadOnly = true;

        BoundField theCol2 = new BoundField();
        theCol2.HeaderText = "Patientid";
        theCol2.DataField = "Ptn_Pk";
        theCol2.ItemStyle.CssClass = "textstyle";
        theCol2.ReadOnly = true;

        BoundField theCol3 = new BoundField();
        theCol3.HeaderText = "Purpose";
        theCol3.DataField = "Purpose";
        theCol3.ItemStyle.CssClass = "textstyle";
        theCol3.SortExpression = "Purpose";
        theCol3.ReadOnly = true;

        BoundField theCol4 = new BoundField();
        theCol4.HeaderText = "Enrollment No.";
        theCol4.ItemStyle.CssClass = "textstyle";
        theCol4.DataField = "PatientEnrollmentID";
        theCol4.SortExpression = "PatientEnrollmentID";
        theCol4.ReadOnly = true;

        BoundField theCol5 = new BoundField();
        theCol5.HeaderText = "Appointment Date";
        theCol5.DataField = "Appointment_Date";
        theCol5.ItemStyle.CssClass = "textstyle";
        theCol5.SortExpression = "app_date";
        theCol5.ReadOnly = true;

        BoundField theCol6 = new BoundField();
        theCol6.HeaderText = "locationid";
        theCol6.DataField = "locationid";
        theCol6.ItemStyle.CssClass = "textstyle";
        theCol6.ReadOnly = true;

        BoundField theCol7 = new BoundField();
        theCol7.HeaderText = "visit_pk";
        theCol7.DataField = "visit_pk";
        theCol7.ItemStyle.CssClass = "textstyle";
        theCol7.ReadOnly = true;

        BoundField theCol8 = new BoundField();
        theCol8.HeaderText = "Appointment Status";
        theCol8.DataField = "AppointmentStatus";
        theCol8.ItemStyle.CssClass = "textstyle";
        theCol8.SortExpression = "AppointmentStatus";
        theCol8.ReadOnly = true;


        BoundField theCol9 = new BoundField();
        if (((DataTable)ViewState["grdDataSource"]).Rows.Count > 0)
        {
            theCol9.HeaderText = ((DataTable)ViewState["grdDataSource"]).Rows[1][0].ToString().Trim();
        }
        //theCol8.HeaderText = "File Reference";
        theCol9.ItemStyle.CssClass = "textstyle";
        theCol9.DataField = "PatientClinicID";
        theCol9.SortExpression = "PatientClinicID";
        theCol9.ReadOnly = true;

        BoundField theCol10 = new BoundField();
        theCol10.HeaderText = "Program";
        theCol10.ItemStyle.CssClass = "textstyle";
        theCol10.DataField = "Program";
        theCol10.SortExpression = "Program";
        theCol10.ReadOnly = true;

        grdSearchResult.Columns.Add(theCol);
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

        if (Convert.ToInt32(Session["SystemId"]) == 1)
        {
            grdSearchResult.Columns[1].Visible = false;

        }

        else
        {
            grdSearchResult.Columns[1].Visible = true;

        }

        grdSearchResult.Columns[3].Visible = false;
        grdSearchResult.Columns[7].Visible = false;
        grdSearchResult.Columns[8].Visible = false;
        grdSearchResult.Columns[9].Visible = false;


        grdSearchResult.DataBind();


    }
    #endregion

    protected void Page_Init(object sender, EventArgs e)
    {
        if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
        {
            IQCareMsgBox.Show("SessionExpired", this);
            Response.Redirect("~/frmlogin.aspx",true);
        }
        //RTyagi..17April.07
        /***************** Check For User Rights ****************/
        AuthenticationManager Authentiaction = new AuthenticationManager();

        if (Authentiaction.HasFunctionRight(ApplicationAccess.Schedular, FunctionAccess.Update, (DataTable)Session["UserRight"]) == false)
        {
            btnSubmit.Enabled = false;
            btnNewAppointment.Enabled = false;
        }
        if (Authentiaction.HasFunctionRight(ApplicationAccess.Schedular, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
        {
            btnSubmit.Enabled = false;
            btnNewAppointment.Enabled = false;
        }
        Form.EnableViewState = true;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
        {
            IQCareMsgBox.Show("SessionExpired", this);
            Response.Redirect("~/frmlogin.aspx",true);
        }

        //(Master.FindControl("lblRoot") as Label).Text = "Scheduler";
        //(Master.FindControl("lblMark") as Label).Text = "";
        //(Master.FindControl("lblMark") as Label).Visible = false;
        //(Master.FindControl("PanelPatiInfo") as Panel).Visible = false;
        //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Visible = false;
        //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Scheduler";
        (Master.FindControl("levelTwoNavigationUserControl1").FindControl("PanelPatiInfo") as Panel).Visible = false;
        IQWebUtils Utils = new IQWebUtils();
        Utils.HidePatientLevelheader();
        

        #region "Modified20June07(3)"
        txtTo.Attributes.Add("onkeyup", "CheckDate(this)");
        txtTo.Attributes.Add("OnBlur", "CheckDate(this)");

        txtFrom.Attributes.Add("onkeyup", "CheckDate(this)");
        txtFrom.Attributes.Add("OnBlur", "CheckDate(this)");
        txtTo.Attributes.Add("onkeyup", "DateFormat(this, this.value, event, false, '3')");
        txtFrom.Attributes.Add("onkeyup", "DateFormat(this, this.value, event, false, '3')");
        txtFrom.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");
        txtTo.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");
        #endregion

        if (Session["PatientStatus"] != null)
        {
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text = Convert.ToString(Session["PatientStatus"]);
        }
        BindHeader();

        if (!IsPostBack)
        {

            //*******Update appointment status priviously missed, missed, careended and met from pending*******//
            IUser LoginManager;
            LoginManager = (IUser)ObjectFactory.CreateInstance("BusinessProcess.Security.BUser, BusinessProcess.Security");
            int theAffectedRows = LoginManager.UpdateAppointmentStatus(Convert.ToString(Application["AppCurrentDate"]), Convert.ToInt16(Session["AppLocationId"]));

            //******Fill the dropdown list, set attribultes, set from and todate and accordingly show the data in grid******//
            setFromToDateAndShowData();

        }

    }
    private void BindHeader()
    {
        DataSet theFacilityDS = new DataSet();
        AuthenticationManager Authentication = new AuthenticationManager();
        IFacilitySetup FacilityMaster = (IFacilitySetup)ObjectFactory.CreateInstance("BusinessProcess.Administration.BFacility, BusinessProcess.Administration");

        theFacilityDS = FacilityMaster.GetSystemBasedLabels(Convert.ToInt32(Session["SystemId"]), ApplicationAccess.SchedularAppointment, 0);

        DataTable theDT = theFacilityDS.Tables[0];

        ViewState["grdDataSource"] = theFacilityDS.Tables[0];


    }
    protected void grdSearchResult_Sorting(object sender, GridViewSortEventArgs e)
    {
        IQCareUtils clsUtil = new IQCareUtils();

        SortAndSetDataInGrid(e.SortExpression);
        if (ViewState["SortDirection"].ToString() == "Asc")
        {
            ViewState["SortDirection"] = "Desc";
        }
        else
        {
            ViewState["SortDirection"] = "Asc";
        }

    }

    protected void grdSearchResult_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string theUrl = string.Empty;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[4].ToString() != "Met") && (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[4].ToString() != "CareEnded") && (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[4].ToString() != "Missed") && (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[4].ToString() != "Previously Missed"))
            {
                e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer';this.style.BackColor='#666699';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");


                theUrl = string.Format("{0}&PatientEnrollmentID={1}&LocationId={2}&PatientVisitID={3}&FormName={4}", "./frmScheduler_AppointmentNew.aspx?name=Edit", ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[5], ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[7], ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[8], "EditDelete");
                e.Row.Attributes.Add("onclick", "window.location.href=('" + theUrl + "')");   ////Page.ClientScript.GetPostBackEventReference(grdSearchResult, "Select$" + e.Row.RowIndex.ToString()));

            }

        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //*******Fill grdSearchResult*******//
        IAppointment FormManager;
        DataSet theDtSet;
        IQCareUtils clsUtil = new IQCareUtils();
        try
        {

            FormManager = (IAppointment)ObjectFactory.CreateInstance("BusinessProcess.Scheduler.BAppointment, BusinessProcess.Scheduler");
            IQCareUtils theUtil = new IQCareUtils();
            hidappointment.Value = "";
            //get the data in dataset on the basis of slected status and appointment dates
            theDtSet = fillDataSet();

            if (theDtSet.Tables.Count == 0)
            {
                return;
            }

            ViewState["GrdData"] = theDtSet.Tables[0];
            ViewState["SortDirection"] = "Asc";
            SortAndSetDataInGrid("appdate");
            ViewState["SortDirection"] = "Desc";
            if (theDtSet.Tables[0].Rows.Count == 0)
            {
                hidappointment.Value = "No";
                IQCareMsgBox.Show("NoAppointmentRecordExists", this);
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
    private void SortAndSetDataInGrid(String SortExpression)
    {
        IQCareUtils clsUtil = new IQCareUtils();
        DataView theDV;

        if (SortExpression == "app_date")
        {
            SortExpression = "appdate";
        }
        theDV = clsUtil.GridSort((DataTable)ViewState["GrdData"], SortExpression, ViewState["SortDirection"].ToString());

        //grdSearchResult.DataSource = null;
        //grdSearchResult.Columns.Clear();

        grdSearchResult.DataSource = theDV;
        BindGrid();

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        string theUrl;
        theUrl = string.Format("../frmFacilityHome.aspx");
        Response.Redirect(theUrl);
        //Server.Transfer(theUrl);

    }

    protected void btnNewAppointment_Click1(object sender, EventArgs e)
    {
        string theUrl;
        theUrl = string.Format("{0}?FormName={1}&mnuClicked={2}", "..//frmFindAddPatient.aspx", "AppointmentMain", "AppointmentMain");
        Response.Redirect(theUrl);



    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {

        FormManager = (IAppointment)ObjectFactory.CreateInstance("BusinessProcess.Scheduler.BAppointment, BusinessProcess.Scheduler");
        IQCareUtils theUtil = new IQCareUtils();
        int theAppStatus = 0;
        if (ddAppointmentStatus.SelectedValue == "12")
            theAppStatus = 1;
        else if (ddAppointmentStatus.SelectedValue == "13")
            theAppStatus = 2;
        else if (ddAppointmentStatus.SelectedValue == "15")
            theAppStatus = 3;

        if (string.IsNullOrWhiteSpace(txtFrom.Value))
        {
            txtFrom.Value = "1-1-1900";

        }
        //if (txtTo.Value == " ")
        if (string.IsNullOrWhiteSpace(txtTo.Value))
        {

            txtTo.Value = "1-1-1900";
        }
        DataTable dtAppointment = (DataTable)FormManager.GetAppointmentGrid(theAppStatus, Convert.ToDateTime(theUtil.MakeDate(txtFrom.Value)).ToString("dd-MMM-yyy"), Convert.ToDateTime(theUtil.MakeDate(txtTo.Value)).ToString("dd-MMM-yyy"), Convert.ToInt32(Session["AppLocationId"])).Tables[0];

        DataTable theDT = dtAppointment.Copy();
        theDT.Columns.Remove("ptn_pk");
        theDT.Columns.Remove("LocationId");
        theDT.Columns.Remove("Visit_Pk");
        theDT.Columns.Remove("appdate");
        IQWebUtils theUtils = new IQWebUtils();
        theUtils.ExporttoExcel(theDT, Response);

        /* Begin 25-03-2010
        DataTable theDT = new DataTable();
        theDT.Columns.Add("FirstName", System.Type.GetType("System.String"));
        if (Convert.ToInt32(Session["SystemId"]) == 2)
        {
            theDT.Columns.Add("MiddleName", System.Type.GetType("System.String"));
        }
        theDT.Columns.Add("LastName", System.Type.GetType("System.String"));
        theDT.Columns.Add("Appointment Status", System.Type.GetType("System.String"));
        theDT.Columns.Add("Purpose", System.Type.GetType("System.String"));
        theDT.Columns.Add("Enrollment No", System.Type.GetType("System.String"));
        theDT.Columns.Add("Appointment Date", System.Type.GetType("System.String"));
         
            theDT.Columns.Add(((DataTable)ViewState["grdDataSource"]).Rows[1][0].ToString().Trim(), System.Type.GetType("System.String"));
            theDT.Columns.Add("Program", System.Type.GetType("System.String"));
       
        for (int i = 0; i < dtAppointment.Rows.Count; i++)
        {
            DataRow theDR = theDT.NewRow();

            if (Convert.ToInt32(Session["SystemId"]) == 1)
            {
                theDR[0] = dtAppointment.Rows[i]["FirstName"].ToString();
                theDR[1] = dtAppointment.Rows[i]["LastName"].ToString();
                theDR[2] = dtAppointment.Rows[i]["AppointmentStatus"];
                theDR[3] = dtAppointment.Rows[i]["Purpose"];
                theDR[4] = dtAppointment.Rows[i]["patientenrollmentid"];
                theDR[5] = dtAppointment.Rows[i]["App_Date"];
                theDR[6] = dtAppointment.Rows[i]["PatientClinicID"];
                theDR[7] = dtAppointment.Rows[i]["Program"];
                theDT.Rows.InsertAt(theDR, i);

            }

            else
            {
                theDR[0] = dtAppointment.Rows[i]["FirstName"].ToString();
                theDR[1] = dtAppointment.Rows[i]["MiddleName"].ToString();
                theDR[2] = dtAppointment.Rows[i]["LastName"].ToString();
                theDR[3] = dtAppointment.Rows[i]["AppointmentStatus"];
                theDR[4] = dtAppointment.Rows[i]["Purpose"];
                theDR[5] = dtAppointment.Rows[i]["patientenrollmentid"];
                theDR[6] = dtAppointment.Rows[i]["App_Date"];
                theDR[7] = dtAppointment.Rows[i]["PatientClinicID"];
                theDR[8] = dtAppointment.Rows[i]["Program"];
                theDT.Rows.InsertAt(theDR, i);

            }

        }         
        Session["Appointment"] = theDT;
        string FName = "AppointmentList";
        
        string thePath = Server.MapPath("..\\ExcelFiles\\" + FName + ".xls");
        string theTemplatePath = Server.MapPath("..\\ExcelFiles\\IQCareTemplate.xls");
        theUtil.ExportToExcel((DataTable)Session["Appointment"], thePath, theTemplatePath); 
        
        Response.Redirect("..\\ExcelFiles\\" + FName + ".xls");
        */
    }
}
