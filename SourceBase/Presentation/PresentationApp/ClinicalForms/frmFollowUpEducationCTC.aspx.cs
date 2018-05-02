
using System;
using System.Data;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.VisualBasic;
using Interface.Clinical;
using Interface.Security;
using Application.Common;
using Interface.Administration;
using Application.Presentation;

public partial class ClinicalForms_frmFollowUpEducationCTC : BasePage
{
    IFollowupEducation FollowupEducation;
    AuthenticationManager Authentication = new AuthenticationManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
            {
                IQCareMsgBox.Show("SessionExpired", this);
                Response.Redirect("~/frmlogin.aspx",true);
            }
        if (!IsPostBack)
        {
            
            IFacilitySetup FacilityMaster = (IFacilitySetup)ObjectFactory.CreateInstance("BusinessProcess.Administration.BFacility, BusinessProcess.Administration");
            DataSet objDs = new DataSet();
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text = Session["lblpntstatus"].ToString();
            //(Master.FindControl("lblRoot") as Label).Text = "";
            //(Master.FindControl("lblMark") as Label).Visible = false;
            //(Master.FindControl("lblheader") as Label).Text = "Follow-up Education";
            //(Master.FindControl("lblformname") as Label).Text = "Follow-Up Education";
            //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Visible = false;
            //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Follow-up Education";
            //(Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Follow-up Education";
            if (Authentication.HasFunctionRight(ApplicationAccess.FollowupEducation, FunctionAccess.Print, (DataTable)Session["UserRight"]) == false)
            {
                btnPrint.Enabled = false;

            }  
            if (Request.QueryString["name"] == "Add")
            {
                Session["PtnRedirect"] = Convert.ToInt32(Session["PatientId"]);
                Boolean blnRightfind = false;
                if (Authentication.HasFunctionRight(ApplicationAccess.FollowupEducation, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
                {
                    string theUrl = string.Empty;
                    theUrl = string.Format("../ClinicalForms/frmPatient_Home.aspx?");
                    Response.Redirect(theUrl);
                }
                else if (Authentication.HasFunctionRight(ApplicationAccess.FollowupEducation, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
                {
                    blnRightfind = true;
                    btnaddtopic.Enabled = false;
                    btnsave.Enabled = false;

                }
                else if (Authentication.HasFunctionRight(ApplicationAccess.FollowupEducation, FunctionAccess.Update, (DataTable)Session["UserRight"]) == false)
                {
                    if (!blnRightfind)
                    {
                        btnsave.Enabled = true;
                        btnaddtopic.Enabled = true;
                    }
                }

            }


            txtvisitdate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            txtvisitdate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3')");


            Session["SaveFlag"] = "Add"; // "Edit"
            Session["SelectedId"] = "";
            Session["SelectedRow"] = -1;// index of row selected for editing from grid
            Session["RemoveFlag"] = "False";
            Session["Ptn_Pk"] = Convert.ToInt32(Session["PatientId"]);
            BindCouncellingType();
            GetAllData();

            if (Session["lblpntstatus"].ToString() == "1")
            {
                btnaddtopic.Enabled = false;
                btnsave.Enabled = false;
            }
            else
            {
                btnaddtopic.Enabled = true;

            }
        }
    }

    public void BindCouncellingType()
    {
        BindFunctions BindManager = new BindFunctions();
        FollowupEducation = (IFollowupEducation)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BFollowupEducation, BusinessProcess.Clinical");
        DataSet dsFollowupEducation = FollowupEducation.GetCouncellingType();
        BindManager.BindCombo(ddlcouncellingtype, dsFollowupEducation.Tables[0], "Name", "Id");
    }
    private void GetAllData()
    {

        FollowupEducation = (IFollowupEducation)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BFollowupEducation, BusinessProcess.Clinical");

        if (Session["Ptn_Pk"].ToString() != "0")
        {
            DataSet theDS = FollowupEducation.GetAllFollowupEducationData(Convert.ToInt32(Session["Ptn_Pk"]));
            Session["GridData"] = theDS.Tables[0];
            grdFollowupEducation.DataSource = (DataTable)Session["GridData"];
            BindGrid();
        }

    }
    private void SaveCancel()
    {
        string strSession = Session["Ptn_Pk"].ToString();
        string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
        script += "var ans;\n";
        script += "ans=window.confirm('Follow-up Education saved successfully. Do you want to close?');\n";
        script += "if (ans==true)\n";
        script += "{\n";
        script += "Redirect(" + strSession + ");\n";
        script += "}\n";
        script += "else \n";
        script += "{\n";
        script += "window.location.href='../ClinicalForms/frmFollowUpEducationCTC.aspx?name=Edit';\n";
        script += "}\n";
        script += "</script>\n";
        RegisterStartupScript("confirm", script);
    }
    private void Refresh()
    {
        Session["SaveFlag"] = "Add";
        Session["SelectedId"] = "";
        ddlcouncellingtype.SelectedIndex = -1;
        ddlcouncellingtopic.SelectedIndex = -1;
        txtotherctopic.Text = "";
        txtvisitdate.Value = "";
        txtcomments.Text = "";
        Session["SelectedRow"] = -1;

    }
    private void ClearSession()
    {
        Session["SaveFlag"] = null; // "Edit"
        Session["SelectedId"] = null;
        Session["SelectedRow"] = null;// index of row selected for editing from grid

    }
    private void BindGrid()
    {
        BoundField theCol0 = new BoundField();
        theCol0.HeaderText = "Id";
        theCol0.DataField = "Id";
        theCol0.ItemStyle.CssClass = "textstyle";
        grdFollowupEducation.Columns.Add(theCol0);


        BoundField theCol1 = new BoundField();
        theCol1.HeaderText = "Patientid";
        theCol1.DataField = "Ptn_pk";
        theCol1.ItemStyle.CssClass = "textstyle";
        grdFollowupEducation.Columns.Add(theCol1);

        BoundField theCol2 = new BoundField();
        theCol2.HeaderText = "Visit Date";
        theCol2.DataField = "VisitDate";
        theCol2.SortExpression = "VisitDate";
        theCol2.ItemStyle.CssClass = "textstyle";
        theCol2.ReadOnly = true;
        grdFollowupEducation.Columns.Add(theCol2);

        BoundField theCol3 = new BoundField();
        theCol3.HeaderText = "Councelling Type";
        theCol3.DataField = "CouncellingTypeId";
        theCol3.ItemStyle.CssClass = "textstyle";
        theCol3.SortExpression = "CouncellingTypeId";
        theCol3.ReadOnly = true;
        grdFollowupEducation.Columns.Add(theCol3);

        BoundField theCol4 = new BoundField();
        theCol4.HeaderText = "Councelling Type";
        theCol4.DataField = "CouncellingType";
        theCol4.ItemStyle.CssClass = "textstyle";
        theCol4.SortExpression = "CouncellingType";
        theCol4.ReadOnly = true;
        grdFollowupEducation.Columns.Add(theCol4);


        BoundField theCol5 = new BoundField();
        theCol5.HeaderText = "Councelling Topic";
        theCol5.ItemStyle.CssClass = "textstyle";
        theCol5.DataField = "CouncellingTopicId";
        theCol5.SortExpression = "CouncellingTopicId";
        theCol5.ReadOnly = true;
        grdFollowupEducation.Columns.Add(theCol5);

        BoundField theCol6 = new BoundField();
        theCol6.HeaderText = "Councelling Topic";
        theCol6.ItemStyle.CssClass = "textstyle";
        theCol6.DataField = "CouncellingTopic";
        theCol6.SortExpression = "CouncellingTopic";
        theCol6.ReadOnly = true;
        grdFollowupEducation.Columns.Add(theCol6);

        BoundField theCol7 = new BoundField();
        theCol7.HeaderText = "Other Councelling Topic";
        theCol7.ItemStyle.CssClass = "textstyle";
        theCol7.DataField = "OtherDetail";
        theCol7.SortExpression = "OtherDetail";
        theCol7.ReadOnly = true;
        grdFollowupEducation.Columns.Add(theCol7);



        BoundField theCol8 = new BoundField();
        theCol8.HeaderText = "Comments";
        theCol8.ItemStyle.CssClass = "textstyle";
        theCol8.DataField = "Comments";
        theCol8.SortExpression = "Comments";
        theCol8.ReadOnly = true;
        grdFollowupEducation.Columns.Add(theCol8);

        BoundField theCol9 = new BoundField();
        theCol9.HeaderText = "Visit_pk";
        theCol9.DataField = "Visit_pk";
        theCol9.SortExpression = "Visit_pk";
        theCol9.ItemStyle.CssClass = "textstyle";
        theCol9.ReadOnly = true;
        grdFollowupEducation.Columns.Add(theCol9);

        if (Authentication.HasFunctionRight(ApplicationAccess.FollowupEducation, FunctionAccess.Delete, (DataTable)Session["UserRight"]) == true && Session["lblpntstatus"].ToString() != "1")
        {
            CommandField objfield = new CommandField();
            objfield.ButtonType = ButtonType.Link;
            objfield.DeleteText = "<img src='../Images/del.gif' alt='Delete' border='0' />";
            objfield.ShowDeleteButton = true;
            grdFollowupEducation.Columns.Add(objfield);
        }

        grdFollowupEducation.DataBind();
        grdFollowupEducation.Columns[0].Visible = false;
        grdFollowupEducation.Columns[1].Visible = false;
        grdFollowupEducation.Columns[3].Visible = false;
        grdFollowupEducation.Columns[5].Visible = false;
        grdFollowupEducation.Columns[9].Visible = false;

    }
    protected void ddlcouncellingtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcouncellingtype.SelectedItem.Value.ToString() != "" && ddlcouncellingtype.SelectedItem.Value.ToString() != "0")
        {
            BindFunctions BindManager = new BindFunctions();
            FollowupEducation = (IFollowupEducation)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BFollowupEducation, BusinessProcess.Clinical");
            DataSet dsFollowupEducation = FollowupEducation.GetCouncellingTopic(Convert.ToInt32(ddlcouncellingtype.SelectedItem.Value.ToString()));
            BindManager.BindCombo(ddlcouncellingtopic, dsFollowupEducation.Tables[0], "Name", "Id");
        }
    }

    protected void grdFollowupEducation_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        if (Authentication.HasFunctionRight(ApplicationAccess.FollowupEducation, FunctionAccess.Update, (DataTable)Session["UserRight"]) == true)
        {
            btnsave.Enabled = true;
            btnaddtopic.Enabled = true;
        }
        if (Session["lblpntstatus"].ToString() == "1")
        {
            btnaddtopic.Enabled = false;
            btnsave.Enabled = false;
        }
        else
        {
            btnaddtopic.Enabled = true;

        }

        int thePage = grdFollowupEducation.PageIndex;
        int thePageSize = grdFollowupEducation.PageSize;


        GridViewRow theRow = grdFollowupEducation.Rows[e.NewSelectedIndex];
        int theIndex = thePageSize * thePage + theRow.RowIndex;

        System.Data.DataTable theDT = new System.Data.DataTable();
        theDT = ((DataTable)Session["GridData"]);

        int r = theIndex;


        // Fill data in Textboxes from grid
        //Edit the selected row
        if (theDT.Rows.Count > 0)
        {
            txtvisitdate.Value = theDT.Rows[r]["VisitDate"].ToString();
            ddlcouncellingtype.SelectedValue = theDT.Rows[r]["CouncellingTypeId"].ToString();
            ddlcouncellingtype_SelectedIndexChanged(sender, e);
            ddlcouncellingtopic.SelectedValue = theDT.Rows[r]["CouncellingTopicId"].ToString();
            txtotherctopic.Text = theDT.Rows[r]["OtherDetail"].ToString();
            txtcomments.Text = theDT.Rows[r]["Comments"].ToString();
            if (theDT.Rows[r]["CouncellingTopicId"].ToString() == "35")
            {
                tdotherctopic.Visible = true;

            }
            else
            {
                tdotherctopic.Visible = false;
            }

            Session["SelectedRow"] = theIndex;
            Session["SaveFlag"] = "Edit";
            btnaddtopic.Text = "Update Topic";


        }

    }
    protected void grdFollowupEducation_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        System.Data.DataTable theDT = new System.Data.DataTable();
        theDT = ((DataTable)Session["GridData"]);
        int theRow = Convert.ToInt32(e.RowIndex);

        int Id = -1;
        if (theDT.Rows.Count > 0)
        {
            if ((theDT.Rows[theRow]["Id"] != null) && (theDT.Rows[theRow]["Id"] != DBNull.Value))
            {
                if (theDT.Rows[theRow]["Id"].ToString() != "")
                {
                    Id = Convert.ToInt32(theDT.Rows[theRow]["Id"]);
                    FollowupEducation = (IFollowupEducation)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BFollowupEducation, BusinessProcess.Clinical");
                    FollowupEducation.DeleteFollowupEducation(Id, Convert.ToInt32(Session["Ptn_pk"]));
                }
            }

            theDT.Rows[theRow].Delete();
            theDT.AcceptChanges();
            Session["GridData"] = theDT;
            grdFollowupEducation.Columns.Clear();
            grdFollowupEducation.DataSource = (DataTable)Session["GridData"];
            BindGrid();
            IQCareMsgBox.Show("DeleteSuccess", this);
            Refresh();
        }
        else
        {
            grdFollowupEducation.Visible = false;
            IQCareMsgBox.Show("DeleteSuccess", this);
            //Refresh();
        }
    }
    protected void grdFollowupEducation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < 10; i++)
            {
                if (Authentication.HasFunctionRight(ApplicationAccess.FollowupEducation, FunctionAccess.Update, (DataTable)Session["UserRight"]) == true && Session["lblpntstatus"].ToString() != "1")
                {
                    e.Row.Cells[i].Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.BackColor='#666699';");
                    e.Row.Cells[i].Attributes.Add("onmouseout", "this.style.backgroundColor='';");
                    e.Row.Cells[i].Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(grdFollowupEducation, "Select$" + e.Row.RowIndex.ToString()));
                }
            }
            if (Authentication.HasFunctionRight(ApplicationAccess.FollowupEducation, FunctionAccess.Delete, (DataTable)Session["UserRight"]) == true && Session["lblpntstatus"].ToString() != "1")
            {
                LinkButton objlink = (LinkButton)e.Row.Cells[10].Controls[0];
                objlink.OnClientClick = "if(!confirm('Are you sure you want to delete this record ?')) return false;";
                e.Row.Cells[10].ID = e.Row.RowIndex.ToString();
            }
        }
    }
    protected void grdFollowupEducation_Sorting(object sender, GridViewSortEventArgs e)
    {
        IQCareUtils clsUtil = new IQCareUtils();
        DataView theDV;

        if (Session["SortDirection"].ToString() == "Asc")
        {
            theDV = clsUtil.GridSort((DataTable)Session["GridData"], e.SortExpression, Session["SortDirection"].ToString());
            Session["SortDirection"] = "Desc";
        }
        else
        {
            theDV = clsUtil.GridSort((DataTable)Session["GridData"], e.SortExpression, Session["SortDirection"].ToString());
            Session["SortDirection"] = "Asc";
        }
        grdFollowupEducation.Columns.Clear();
        grdFollowupEducation.DataSource = theDV;
        BindGrid();

    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        DataTable dtPatientInfo = (DataTable)Session["PatientInformation"];
        DateTime RegistrationDate = Convert.ToDateTime(dtPatientInfo.Rows[0]["RegistrationDate"].ToString());
        if (Authentication.HasFunctionRight(ApplicationAccess.FollowupEducation, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
        {

            btnsave.Enabled = false;
            btnaddtopic.Enabled = false;
        }

        if (Convert.ToInt32(ddlcouncellingtype.SelectedValue) < 1)
        {
            IQCareMsgBox.Show("CouncellingNotSelected", this);
            return;
        }
        if (Convert.ToInt32(ddlcouncellingtopic.SelectedValue) < 1)
        {
            IQCareMsgBox.Show("CouncellingTopicNotSelected", this);
            return;
        }
        if (txtvisitdate.Value == "")
        {
            IQCareMsgBox.Show("MissingVisitDate", this);
            return;
        }
        IQCareUtils theUtil = new IQCareUtils();
        if (Convert.ToDateTime(Application["AppCurrentDate"]) < Convert.ToDateTime(theUtil.MakeDate(txtvisitdate.Value)))
        {
            IQCareMsgBox.Show("CompareDate5", this);
            txtvisitdate.Focus();
            return;
        }
        if ( RegistrationDate > Convert.ToDateTime(theUtil.MakeDate(txtvisitdate.Value)))
        {
            IQCareMsgBox.Show("VisitDateFollowup", this);
            txtvisitdate.Focus();
            return;
        }
        DataTable theDT = (DataTable)Session["GridData"];
        DataView theDV = new DataView(theDT);
        theDV.Sort = "VisitDate Desc";
        if (btnaddtopic.Text != "Update Topic")
        {
            if (theDV.Count > 0 && theDV[0]["CouncellingTopicId"].ToString() == ddlcouncellingtopic.SelectedValue)
            {
                IQCareMsgBox.Show("DuplicateFollowupEducation", this);
                ddlcouncellingtopic.Focus();
                return;
            }

            //if (theDV.Count > 0 && Convert.ToDateTime(theDV[0]["VisitDate"]) > Convert.ToDateTime(theUtil.MakeDate(txtvisitdate.Value)))
            //{
            //    IQCareMsgBox.Show("FollowupEducationDateinPast", this);
            //    ddlcouncellingtype.Focus();
            //    return;
            //}
        }
        //DataTable theDT = new DataTable();
        //theDT = (DataTable)Session["GridData"];
        if (Session["SaveFlag"].ToString() == "Add")
        {
            IQCareUtils theUtils = new IQCareUtils();

            //Add mode - a new member is be added and he is not already in the grid
            theDT = (DataTable)Session["GridData"];
            DataRow theDR = theDT.NewRow();
            theDR["Ptn_Pk"] = Session["Ptn_Pk"];
            theDR["VisitDate"] = txtvisitdate.Value;
            theDR["CouncellingTypeId"] = ddlcouncellingtype.SelectedItem.Value;
            theDR["CouncellingType"] = ddlcouncellingtype.SelectedItem.Text;
            theDR["CouncellingTopicId"] = ddlcouncellingtopic.SelectedItem.Value;
            theDR["CouncellingTopic"] = ddlcouncellingtopic.SelectedItem.Text;
            theDR["OtherDetail"] = txtotherctopic.Text;
            theDR["Comments"] = txtcomments.Text;
            theDR["Visit_pk"] = 0;
            theDT.Rows.Add(theDR);
            Session["GridData"] = theDT;
            grdFollowupEducation.Columns.Clear();
            grdFollowupEducation.DataSource = (DataTable)Session["GridData"];
        }
        else if (Session["SaveFlag"].ToString() == "Edit")
        {

            if (((DataTable)Session["GridData"]).Rows.Count < 1)
            {
                IQCareMsgBox.Show("PharmacyNoData", this);
                Refresh();
                return;
            }

            int r = Convert.ToInt32(Session["SelectedRow"]);            
            //theDT.Rows[r]["CouncellingTypeId"] = ddlcouncellingtype.SelectedValue;
            //theDT.Rows[r]["CouncellingType"] = ddlcouncellingtype.SelectedItem.Text;
            //theDT.Rows[r]["councellingTopicId"] = ddlcouncellingtopic.SelectedValue;
            //theDT.Rows[r]["CouncellingTopic"] = ddlcouncellingtopic.SelectedItem.Text;            
            //if (ddlcouncellingtype.SelectedValue == "5")
            //{
            //    theDT.Rows[r]["OtherDetail"] = txtotherctopic.Text;
            //}
            //else
            //{
            //    theDT.Rows[r]["OtherDetail"] = "";
            //}
            theDT.Rows[r]["VisitDate"] = txtvisitdate.Value;
            theDT.Rows[r]["Comments"] = txtcomments.Text;

            Session["GridData"] = theDT;
            grdFollowupEducation.Columns.Clear();
            grdFollowupEducation.DataSource = (DataTable)Session["GridData"];
        }
        BindGrid();
        Refresh();
        btnaddtopic.Text = "Add Topic";

    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (((DataTable)Session["GridData"]).Rows.Count < 1)
        {
            IQCareMsgBox.Show("PharmacyNoData", this);
            return;
        }

        int Id, Ptn_pk = 0, CouncellingTypeId, CouncellingTopicId, UserId, DeleteFlag, Visit_pk, LocationID;
        string Comments, OtherDetail;
        DateTime VisitDate;

        FollowupEducation = (IFollowupEducation)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BFollowupEducation, BusinessProcess.Clinical");
        DataTable theDT = (DataTable)Session["GridData"];

        foreach (DataRow theDR in theDT.Rows)
        {
            if (theDR["Id"] == DBNull.Value)
                Id = -1;
            else
                Id = Convert.ToInt32(theDR["Id"]);
            IQCareUtils theUtils = new IQCareUtils();
            Ptn_pk = Convert.ToInt32(Session["Ptn_Pk"]);
            CouncellingTypeId = Convert.ToInt32(theDR["CouncellingTypeId"]);
            CouncellingTopicId = Convert.ToInt32(theDR["CouncellingTopicId"]);
            LocationID = Convert.ToInt32(Session["AppLocationId"]);
            UserId = Convert.ToInt32(Session["AppUserId"]);
            DeleteFlag = 0;
            Comments = theDR["Comments"].ToString();
            OtherDetail = theDR["OtherDetail"].ToString();
            VisitDate = Convert.ToDateTime(theDR["VisitDate"]);
            Visit_pk = Convert.ToInt32(theDR["Visit_pk"]);

            FollowupEducation.SaveFollowupEducation(Id, Ptn_pk, CouncellingTypeId, CouncellingTopicId, Visit_pk, LocationID, VisitDate.ToString("dd-MMM-yyyy"), Comments, OtherDetail, UserId, DeleteFlag);
        }

        ClearSession();
        SaveCancel();
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        string theUrl = string.Empty;
        theUrl = string.Format("../ClinicalForms/frmPatient_Home.aspx");
        Response.Redirect(theUrl);
    }
    protected void ddlcouncellingtopic_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcouncellingtopic.SelectedValue == "35")
        {
            tdotherctopic.Visible = true;
            txtotherctopic.Focus();
        }
        else
        {
            tdotherctopic.Visible = false;
        }
    }
}
