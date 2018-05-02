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
using Interface.Clinical;
using Application.Common;
using Application.Presentation;
using Interface.Security;

public partial class frmScheduler_Appointmenthistory : LogPage
{
    #region "User Functions"
    int patientId;
    string PatientEnrollmentID;
    int locationId;
    int PatientStatus;

    private void fillPatientAppointmntDetailsInGrid()
    {  
        //////Session["PatientId"]
        //////Session["AppLocationId"]
        //if (Request.QueryString["Patientid"] != null)
        if (Session["PatientId"] != null)
        {
            //patientId = Convert.ToInt32(Request.QueryString["Patientid"]);
            patientId = Convert.ToInt32(Session["PatientId"]);
            if (Request.QueryString["FormName"] != null)
            {
                if (Request.QueryString["FormName"].ToString() == "PatientHome")
                {
                    locationId = 0;
                }
            }
            else
            {
                //locationId = Convert.ToInt32(Request.QueryString["Locationid"]);
                locationId = Convert.ToInt32(Session["AppLocationId"]);
            }
            DataTable DtCareEnd = (DataTable)Session["CEndedStatus"];
            DataSet theDs;
            IAppointment FormManager;
            IQCareUtils theUtil = new IQCareUtils();
            FormManager = (IAppointment)ObjectFactory.CreateInstance("BusinessProcess.Scheduler.BAppointment, BusinessProcess.Scheduler");
            theDs = FormManager.GetPatientppointmentDetails(patientId, locationId, 0);

            if (theDs.Tables[0].Rows.Count > 0)
            {
                //(Master.FindControl("lblformname") as Label).Text = "Appointment History For " + theDs.Tables[0].Rows[0]["Name"].ToString();
                PatientEnrollmentID = theDs.Tables[0].Rows[0]["PatientEnrollmentId"].ToString();
                //PatientStatus = Convert.ToInt32(theDs.Tables[0].Rows[0]["Status"]);
                if (DtCareEnd.Rows.Count > 0)
                {
                    PatientStatus = Convert.ToInt32(DtCareEnd.Rows[0]["CareEnded"]);
                }
                else
                {
                    PatientStatus = 0;
                }
                Session["patientappdtl"] = "0";
               
            }
            grdSearchResult.Columns.Clear();
            grdSearchResult.DataSource = theDs.Tables[1];

            if (!IsPostBack)
            {
                ViewState["GrdData"] = theDs.Tables[1];
                ViewState["SortDirection"] = "Asc";
            }
            //
           
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
        theCol3.HeaderText = "Purpose";
        theCol3.ItemStyle.CssClass = "Purpose";
        theCol3.DataField = "Purpose";
        theCol3.SortExpression = "Purpose";
        theCol3.ReadOnly = true;


        BoundField theCol4 = new BoundField();
        theCol4.HeaderText = "Appdate";
        theCol4.ItemStyle.CssClass = "textstyle";
        theCol4.DataField = "Appdate";
        theCol4.SortExpression = "Appdate";
        theCol4.ReadOnly = true;

        BoundField theCol5 = new BoundField();
        theCol5.HeaderText = "LocationName";
        theCol5.ItemStyle.CssClass = "textstyle";
        theCol5.DataField = "LocationName";
        theCol5.SortExpression = "LocationName";
        theCol5.ReadOnly = true;

        ButtonField theBtn = new ButtonField();
        theBtn.ButtonType = ButtonType.Link;
        theBtn.CommandName = "Select";
        theBtn.HeaderStyle.CssClass = "textstylehidden";
        theBtn.ItemStyle.CssClass = "textstylehidden";

        
        grdSearchResult.Columns.Add(theCol0);
        grdSearchResult.Columns.Add(theCol1);
        grdSearchResult.Columns.Add(theCol2);
        grdSearchResult.Columns.Add(theCol3);
        grdSearchResult.Columns.Add(theCol4);
        grdSearchResult.Columns.Add(theCol5);
        grdSearchResult.Columns.Add(theBtn);

        grdSearchResult.DataBind();
        grdSearchResult.Columns[4].Visible = false;
        
    }
   

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
        {
            IQCareMsgBox.Show("SessionExpired", this);
            Response.Redirect("~/frmlogin.aspx",true);
        }
        if (!IsPostBack)
        {
            if (Session["PatientStatus"]!= null)
            {
                (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text = Convert.ToString(Session["PatientStatus"]);
            }
        }
        GblIQCare.Scheduler = 1;
        
        //(Master.FindControl("lblRoot") as Label).Text = "Clinical Forms >>";
        //(Master.FindControl("lblMark") as Label).Text = "»";
        //(Master.FindControl("lblMark") as Label).Visible = false;
        //(Master.FindControl("lblheader") as Label).Text = "Schedule Appointment";
        //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Clinical Forms >> ";
        //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Schedule Appointment";
        //btnNewAppointment.CssClass = "greenbutton";
        //*******Update appointment status priviously missed, missed, careended and met from pending*******//
        IUser LoginManager;
        LoginManager = (IUser)ObjectFactory.CreateInstance("BusinessProcess.Security.BUser, BusinessProcess.Security");
        int theAffectedRows = LoginManager.UpdateAppointmentStatus(Convert.ToString(Application["AppCurrentDate"]), Convert.ToInt16(Session["AppLocationId"]));

        fillPatientAppointmntDetailsInGrid();

        if (patientId == 0)
        {
            btnNewAppointment.Enabled = false;
        }

        if (PatientStatus == 1)
        {
            btnNewAppointment.Enabled = false;
        }
        Form.EnableViewState = true;
        
    }

    protected void btnNewAppointment_Click(object sender, EventArgs e)
    {
        string theUrl;

        Session["PatientEnrollmentID"] = PatientEnrollmentID.ToString();
        theUrl = string.Format("{0}&FormName={1}", "./frmScheduler_AppointmentNewHistory.aspx?name=Add", "Appointment History New");
        Response.Redirect(theUrl);
        //Server.Transfer(theUrl);
    }

    protected void grdSearchResult_RowDataBound(object sender, GridViewRowEventArgs e)
    {       
        string theUrl = string.Empty;
        if (patientId != 0)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[2].ToString() != "Met") && (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[2].ToString() != "CareEnded") && (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[2].ToString() != "Missed") && (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[2].ToString() != "Previously Missed"))
                {
                    e.Row.Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.BackColor='#666699';");
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");

                    theUrl = string.Format("{0}&FormName={1}", "./frmScheduler_AppointmentNewHistory.aspx?name=Edit", "Appointment History");

                    if (e.Row.Cells[2].Text == "Pending")
                    {
                        ////e.Row.Attributes.Add("onclick", "window.location.href=('" + theUrl + "')");   ////Page.ClientScript.GetPostBackEventReference(grdSearchResult, "Select$" + e.Row.RowIndex.ToString()));
                        e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(grdSearchResult, "Select$" + e.Row.RowIndex.ToString()));
                        Session["patientappdtl"]="1";

                    }
                    else
                    {
                        e.Row.Attributes.Add("onclick", "window.location.href=('" + theUrl + "')");   ////Page.ClientScript.GetPostBackEventReference(grdSearchResult, "Select$" + e.Row.RowIndex.ToString()));
                    }

                    //Session["PatientAppDetails"] = (GridViewRow)grdSearchResult.Rows[e.NewSelectedIndex];  //e.Row.Cells[e.Row.RowIndex].Text;

                }

            }
        }
    }
    protected void grdSearchResult_Sorting(object sender, GridViewSortEventArgs e)
    {
        IQCareUtils clsUtil = new IQCareUtils();
        DataView theDV;

        if (e.SortExpression == "Appointment Date" )
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

    protected void grdSearchResult_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int intRows = e.NewSelectedIndex;
        //GridViewRow row = grdSearchResult.Rows[e.NewSelectedIndex];
        Session["AppPatDtl"] = intRows;
        string theUrl;
        theUrl = string.Format("{0}&FormName={1}", "./frmScheduler_AppointmentNewHistory.aspx?name=Edit", "Appointment History");
        Response.Redirect(theUrl);
    }
   

    protected void btnBack_Click(object sender, EventArgs e)
    {
        string theUrl;
        theUrl = string.Format("{0}", "../ClinicalForms/frmPatient_Home.aspx?name=Add");
        Response.Redirect(theUrl);
        //Server.Transfer(theUrl);

    }
}
