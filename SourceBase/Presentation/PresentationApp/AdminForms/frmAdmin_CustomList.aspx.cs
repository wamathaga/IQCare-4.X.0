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

using Application.Common;
using Application.Presentation;
using Interface.Administration;


public partial class frmAdmin_CustomList : LogPage
{
    /////////////////////////////////////////////////////////////////////
    // Code Written By   : Sanjay Rana
    // Written Date      : 23th Aug 2006
    // Modification Date : 05th Sept 2006
    // Description       : Custom Master List
    //
    /// /////////////////////////////////////////////////////////////////
    

    #region "User Functions"

    private void Bind_Grid()
    {
        BoundField theCol1 = new BoundField();
        theCol1.HeaderText = "Priority";
        theCol1.ItemStyle.CssClass = "textstyle";
        theCol1.DataField = "SRNO";
        theCol1.SortExpression = "SRNO";
        theCol1.ItemStyle.Font.Underline = true;
        theCol1.ReadOnly = true;

        BoundField theCol2 = new BoundField();
        theCol2.HeaderText = lblHeader.Text; 
        theCol2.ItemStyle.CssClass = "textstyle";
        theCol2.DataField = "Name";
        theCol2.SortExpression = "Name";
        theCol2.ReadOnly = true;

        BoundField theCol3 = new BoundField();
        theCol3.HeaderText = "Status";
        theCol3.DataField = "Status";
        theCol3.SortExpression = "Status";
        theCol3.ItemStyle.CssClass = "textstyle";
        theCol3.ReadOnly = true;

        BoundField theCol4 = new BoundField();
        theCol4.HeaderText = "ID";
        theCol4.DataField = "ID";
        theCol4.SortExpression = "ID";
        theCol4.ItemStyle.CssClass = "textstylehidden";
        theCol4.ReadOnly = true;
        BoundField theCol5 = new BoundField();
        if (ViewState["TableName"].ToString().ToLower() == "HIVDisease".ToLower() || ViewState["TableName"].ToString().ToLower() == "Symptom".ToLower())
        {
            if (Convert.ToInt32(ViewState["CategoryId"]) == 31 || Convert.ToInt32(ViewState["CategoryId"]) == 32)
            {
                theCol5.HeaderText = "ICDCodeName";
                theCol5.DataField = "ICDCodeName";
                theCol5.SortExpression = "ICDCodeName";
                theCol5.ReadOnly = true;
            }
        }
        ButtonField theBtn = new ButtonField();
        theBtn.ButtonType = ButtonType.Link;
        theBtn.CommandName = "Select";
        theBtn.HeaderStyle.CssClass = "textstylehidden";
        theBtn.ItemStyle.CssClass = "textstylehidden";

        grdCustom.Columns.Add(theCol1);
        grdCustom.Columns.Add(theCol2);
        grdCustom.Columns.Add(theCol3);
        grdCustom.Columns.Add(theCol4);
        if (ViewState["TableName"].ToString().ToLower() == "HIVDisease".ToLower() || ViewState["TableName"].ToString().ToLower() == "Symptom".ToLower())
        {
            if (Convert.ToInt32(ViewState["CategoryId"]) == 31 || Convert.ToInt32(ViewState["CategoryId"]) == 32)
            {
                grdCustom.Columns.Add(theCol5);
            }
        }
        grdCustom.Columns.Add(theBtn);

        grdCustom.DataBind();
        grdCustom.Columns[3].Visible = false;
    }


    private void Bind_GridDiseaseSymptom()
    {
        BoundField theCol1 = new BoundField();
        theCol1.HeaderText = "Priority";
        theCol1.ItemStyle.CssClass = "textstyle";
        theCol1.DataField = "SRNO";
        theCol1.SortExpression = "SRNO";
        theCol1.ItemStyle.Font.Underline = true;
        theCol1.ReadOnly = true;

        BoundField theCol2 = new BoundField();
        theCol2.HeaderText = lblHeader.Text; 
        theCol2.ItemStyle.CssClass = "textstyle";
        theCol2.DataField = "Name";
        theCol2.SortExpression = "Name";
        theCol2.ReadOnly = true;

        BoundField theCol3 = new BoundField();
        theCol3.HeaderText = "ICDCodeName";
        theCol3.DataField = "ICDCodeName";
        theCol3.SortExpression = "ICDCodeName";
        theCol3.ReadOnly = true;

        BoundField theCol4 = new BoundField();
        theCol4.HeaderText = "Status";
        theCol4.DataField = "Status";
        theCol4.SortExpression = "Status";
        theCol4.ItemStyle.CssClass = "textstyle";
        theCol4.ReadOnly = true;

        BoundField theCol5 = new BoundField();
        theCol5.HeaderText = "ID";
        theCol5.DataField = "ID";
        theCol5.SortExpression = "ID";
        theCol5.ItemStyle.CssClass = "textstylehidden";
        theCol5.ReadOnly = true;
     
        ButtonField theBtn = new ButtonField();
        theBtn.ButtonType = ButtonType.Link;
        theBtn.CommandName = "Select";
        theBtn.HeaderStyle.CssClass = "textstylehidden";
        theBtn.ItemStyle.CssClass = "textstylehidden";

        grdCustom.Columns.Add(theCol1);
        grdCustom.Columns.Add(theCol2);
        grdCustom.Columns.Add(theCol3);
        grdCustom.Columns.Add(theCol4);
        grdCustom.Columns.Add(theCol5);
        grdCustom.Columns.Add(theBtn);

        grdCustom.DataBind();
        grdCustom.Columns[4].Visible = false;
    }


    private void Init_Form()
    {
        if (ViewState["TableName"].ToString() != "HivDisease")
        {
            lblHeader.Text = ViewState["ListName"].ToString();
            ////////// Done by Sanjay on 19th Sept 2006  ////////////////////////////////////////
            ////////// For all the Custom List the ListName field of XML file will be Used //////
            ////////if (Convert.ToInt32(ViewState["CategoryId"]) > 0)
            //////// lblHeader.Text = ViewState["ListName"].ToString();
            /////////////////////////////////////////////////////////////////////////////////////
        }
        else
        {
            lblHeader.Text = "OIs or AIDS Defining Illnesses";
        }
        DataTable theDTModule = (DataTable)Session["AppModule"];
        string theModList = "";
        foreach (DataRow theDR in theDTModule.Rows)
        {
            if (theModList == "")
                theModList = theDR["ModuleId"].ToString();
            else
                theModList = theModList + "," + theDR["ModuleId"].ToString();
        }
        ICustomList CustomManager = (ICustomList)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomList, BusinessProcess.Administration");
        DataTable theDT = CustomManager.GetCustomList(ViewState["TableName"].ToString(), Convert.ToInt32(ViewState["CategoryId"]), Convert.ToInt32(Session["SystemId"]));
        if (ViewState["gridSource"] == null)
        {
            ViewState["gridSource"] = theDT;
            ViewState["gridSortDirection"] = "Desc";
        }

        grdCustom.DataSource = theDT;
        if (ViewState["TableName"].ToString().ToLower() == "HIVDisease".ToLower() || ViewState["TableName"].ToString().ToLower() == "Symptom".ToLower())
        {
            Bind_GridDiseaseSymptom();
        }
        else
        {
            Bind_Grid();
        }
    }
    #region "AccessRight"
    ///************ Check Access Rights For User ****************/
    //private void CheckAccessRight()
    //{
    //    int theChk = 0;
    //    DataTable theCheckDT = (DataTable)Session["UserRight"];
    //    IQCareUtils theUtils = new IQCareUtils();
    //    DataTable theDT;
    //    if (theCheckDT != null)
    //    {
    //        DataView theDV = new DataView(theCheckDT);
    //        theDV.RowFilter = "FeatureID = 6";
    //        if (theDV.Count > 0)
    //        {
    //            theDT = theUtils.CreateTableFromDataView(theDV);

    //            if (Request.QueryString["name"] == "Add")
    //            {
    //                theDV = new DataView(theDT);
    //                theDV.RowFilter = "FunctionID = 4";
    //                if (theDV.Count <= 0)
    //                {
    //                    theChk = 4;
    //                    Check_AccessRight(theChk);
    //                }
    //            }
    //            else if (Request.QueryString["name"] == "Edit")
    //            {
    //                theDV = new DataView(theDT);
    //                theDV.RowFilter = "FunctionID = 2";
    //                if (theDV.Count <= 0)
    //                {
    //                    btnAdd.Enabled = false;
    //                    theDV.RowFilter = "FunctionID = 1";
    //                    if (theDV.Count <= 0)
    //                    {
    //                        theChk = 1;
    //                        Check_AccessRight(theChk);
    //                    }
    //                }
    //            }
    //        }
    //        else
    //        {
    //            Check_AccessRight(theChk);
    //        }
    //    }
    //    else
    //    {
    //        Check_AccessRight(theChk);
    //    }
    //}

    //protected void Check_AccessRight(int thChk)
    //{

    //    int PatientId = Convert.ToInt32(Request.QueryString["PatientId"]);
    //    int sts = Convert.ToInt32(Request.QueryString["sts"].ToString());
    //    string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
    //    script += "var ans=true;\n";
    //    if (thChk == 0)
    //    {
    //        script += "alert('You have no right to access Non-ART Follow-UP');\n";
    //    }
    //    else if (thChk == 1)
    //    {
    //        script += "alert('You have no right to View Non-ART Follow-UP');\n";
    //    }
    //    else if (thChk == 4)
    //    {
    //        script += "alert('You have no right to do Patient Non-ART Follow-UP');\n";
    //    }
    //    script += "if (ans==true)\n";

    //    script += "{\n";
    //    script += "window.location.href('frmPatient_Home.aspx?PatientId=" + PatientId + "');\n";
    //    script += "}\n";

    //    script += "</script>\n";
    //    RegisterClientScriptBlock("confirm", script);

    //}

    #endregion
    #endregion

 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
        {
            IQCareMsgBox.Show("SessionExpired", this);
            Response.Redirect("~/frmlogin.aspx",true);
        }
        try
        {
            if (Page.IsPostBack != true)
            {
                //RTyagi..
                //CheckAccessRight();

                ViewState["TableName"] = Request.QueryString["TableName"].ToString();
                if (Request.QueryString["CategoryId"].ToString() == "" || Request.QueryString["CategoryId"].ToString() == "0")
                {
                    ViewState["CategoryId"] = 0;
                }
                else{
                ViewState["CategoryId"] = Convert.ToInt32(Request.QueryString["CategoryId"]);}
                ViewState["ListName"] = Request.QueryString["LstName"].ToString();
                ViewState["FID"] = Request.QueryString["Fid"].ToString();
                ViewState["Update"] = Request.QueryString["Upd"].ToString();
                if (Request.QueryString["CCID"] != null)
                {
                    ViewState["CCID"] = Request.QueryString["CCID"].ToString();
                }
                if (Request.QueryString["ModId"].ToString() != "")
                {
                    ViewState["ModuleId"] = Convert.ToInt32(Request.QueryString["ModId"]);
                }
                //(Master.FindControl("lblMark") as Label).Visible = false;
                //(Master.FindControl("lblRoot") as Label).Text = " » Customize Lists"; 
                //(Master.FindControl("lblheader") as Label).Text = ViewState["ListName"].ToString();
                //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Customize Lists >> ";
                //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = ViewState["ListName"].ToString();

                if (ViewState["ListName"].ToString() == "Emergency Contact Relationship")
                {
                    //(Master.FindControl("lblheader") as Label).Text = "Emerg. Cont. Relationship";
                    //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Emerg. Cont. Relationship";
                }
                if (ViewState["ListName"].ToString() == "Scheduler - Appointment purpose")
                {
                    //(Master.FindControl("lblheader") as Label).Text = "Sched. Appoi. Purpose";
                    //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Sched. Appoi. Purpose";
                }
                Init_Form();
                AuthenticationManager Authentication = new AuthenticationManager();
                if (Authentication.HasFunctionRight(Convert.ToString(ViewState["ListName"]), FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
                {
                    btnAdd.Enabled = false;
                }
            }
        }
        catch(Exception err)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string theUrl = "";
        if (ViewState["TableName"].ToString().ToLower() == "HIVDisease".ToLower() || ViewState["TableName"].ToString().ToLower() == "Symptom".ToLower())
        {
            theUrl = string.Format("{0}?TableName={1}&CategoryId={2}&LstName={3}&Fid={4}&Upd={5}&CCID={6}&ModId={7}", "frmAdmin_OI_Symptoms.aspx", ViewState["TableName"].ToString(), ViewState["CategoryId"].ToString(), ViewState["ListName"].ToString(), ViewState["FID"].ToString(), ViewState["Update"].ToString(), ViewState["CCID"].ToString(), ViewState["ModuleId"].ToString());
        }
        else if ((ViewState["TableName"].ToString().ToLower() == "PreDefinedDruglist".ToLower()) || (ViewState["TableName"].ToString().ToLower() == "PreDefinedLablist".ToLower()))
        {
            theUrl = string.Format("{0}?TableName={1}&LstName={2}&ModId={3}", "frmAdmin_PredefineDrugList.aspx", ViewState["TableName"].ToString(), ViewState["ListName"].ToString(), ViewState["ModuleId"].ToString());
        }
        else if (ViewState["TableName"].ToString().ToLower() == "Designation".ToLower())
        {
            theUrl = string.Format("{0}?TableName={1}&CategoryId={2}&LstName={3}&Fid={4}&Upd={5}&CCID={6}&ModId={7}&Status={8}", "frmAdmin_Designation.aspx", ViewState["TableName"].ToString(), ViewState["CategoryId"].ToString(), ViewState["ListName"].ToString(), ViewState["FID"].ToString(), "0", ViewState["CCID"].ToString(), ViewState["ModuleId"].ToString(),"Add");
        }
        else
        {
            theUrl = string.Format("{0}?TableName={1}&CategoryId={2}&LstName={3}&Fid={4}&Upd={5}&CCID={6}&ModId={7}", "frmAdmin_CustomPage.aspx", ViewState["TableName"].ToString(), ViewState["CategoryId"].ToString(), ViewState["ListName"].ToString(), ViewState["FID"].ToString(), ViewState["Update"].ToString(), ViewState["CCID"].ToString(), ViewState["ModuleId"].ToString());
        }  
        Response.Redirect(theUrl);
    }

    protected void grdCustom_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.BackColor='#666699';");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");
            e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(grdCustom, "Select$" + e.Row.RowIndex.ToString()));
          
        }
    }

    protected void grdCustom_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int thePage = grdCustom.PageIndex;
        int thePageSize = grdCustom.PageSize;

        GridViewRow  theRow = grdCustom.Rows[e.NewSelectedIndex];

        //////// Done By Sanjay on 19th Sept 2006 /////////////////
        //////// Change in Business Rule. Inactive Records can be updated by the user /////////
        ////////if (theRow.Cells[2].Text.ToString() != "InActive")
        ////////{
        ////////    int Id = Convert.ToInt32(theRow.Cells[3].Text.ToString());
        ////////    string theUrl = string.Format("{0}?SelectedId={1}&TableName={2}&CategoryId={3}&LstName={4}", "frmAdmin_CustomPage.aspx", Id, ViewState["TableName"].ToString(), ViewState["CategoryId"].ToString(), ViewState["ListName"].ToString());
        ////////    Response.Redirect(theUrl);
        ////////}
        ////////else
        ////////{
        ////////    IQCareMsgBox.Show("CustomListInactiveSelect", this);
        ////////    return;
        ////////}
        ////////////////////////////////////////////////////////////////////////////////////////
        string theUrl = "";
        if (ViewState["TableName"].ToString().ToLower() == "HIVDisease".ToLower() || ViewState["TableName"].ToString().ToLower() == "Symptom".ToLower())
        {
            int Id = Convert.ToInt32(theRow.Cells[4].Text.ToString());
            theUrl = string.Format("{0}?SelectedId={1}&TableName={2}&CategoryId={3}&LstName={4}&Fid={5}&Upd={6}&CCID={7}&ModId={8}", "frmAdmin_OI_Symptoms.aspx", Id, ViewState["TableName"].ToString(), ViewState["CategoryId"].ToString(), ViewState["ListName"].ToString(), ViewState["FID"].ToString(), ViewState["Update"].ToString(), ViewState["CCID"].ToString(), ViewState["ModuleId"].ToString());
        }
        else if ((ViewState["TableName"].ToString().ToLower() == "PreDefinedDruglist".ToLower()) || (ViewState["TableName"].ToString().ToLower() == "PreDefinedLablist".ToLower()))
        {
            theUrl = string.Format("{0}?TableName={1}&LstName={2}", "frmAdmin_PredefineDrugList.aspx", ViewState["TableName"].ToString(), ViewState["ListName"].ToString());
        }
        else if (ViewState["TableName"].ToString().ToLower() == "Designation".ToLower())
        {
            int Id = Convert.ToInt32(theRow.Cells[3].Text.ToString());
            theUrl = string.Format("{0}?SelectedId={1}&TableName={2}&CategoryId={3}&LstName={4}&Fid={5}&Upd={6}&CCID={7}&ModId={8}", "frmAdmin_Designation.aspx", Id, ViewState["TableName"].ToString(), ViewState["CategoryId"].ToString(), ViewState["ListName"].ToString(), ViewState["FID"].ToString(), ViewState["Update"].ToString(), ViewState["CCID"].ToString(), ViewState["ModuleId"].ToString());
        }
        else
        {
            int Id = Convert.ToInt32(theRow.Cells[3].Text.ToString());
            //theUrl = string.Format("{0}?SelectedId={1}&TableName={2}&CategoryId={3}&LstName={4}&Fid={5}&Upd={6}&CCID={7}", "frmAdmin_CustomPage.aspx", Id, ViewState["TableName"].ToString(), ViewState["CategoryId"].ToString(), ViewState["ListName"].ToString(), ViewState["FID"].ToString(), ViewState["Update"].ToString(), ViewState["CCID"].ToString());
            theUrl = string.Format("{0}?SelectedId={1}&TableName={2}&CategoryId={3}&LstName={4}&Fid={5}&Upd={6}&CCID={7}&ModId={8}", "frmAdmin_CustomPage.aspx", Id, ViewState["TableName"].ToString(), ViewState["CategoryId"].ToString(), ViewState["ListName"].ToString(), ViewState["FID"].ToString(), ViewState["Update"].ToString(), ViewState["CCID"].ToString(), ViewState["ModuleId"].ToString());
        }
        if ((ViewState["TableName"].ToString().ToLower() != "PreDefinedDruglist".ToLower()))
            Response.Redirect(theUrl);
    }

    protected void grdCustom_Sorting(object sender, GridViewSortEventArgs e)
    {
        //Init_Form(); 
        GridView theGD = (GridView)sender;
        IQCareUtils SortManager = new IQCareUtils();
        DataView theDV;
        if (ViewState["gridSortDirection"].ToString() == "Asc")
        {
            theDV = SortManager.GridSort((DataTable)this.ViewState["gridSource"], e.SortExpression.ToString(), ViewState["gridSortDirection"].ToString());
            ViewState["gridSortDirection"] = "Desc";
        }
        else
        {
            theDV = SortManager.GridSort((DataTable)this.ViewState["gridSource"], e.SortExpression.ToString(), ViewState["gridSortDirection"].ToString());
            ViewState["gridSortDirection"] = "Asc";
        }

        theGD.Columns.Clear();
        theGD.DataSource = theDV;
        Bind_Grid();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string theUrl = "frmAdmin_PMTCT_CustomItems.aspx";
        Response.Redirect(theUrl);
    }

   
   
}
