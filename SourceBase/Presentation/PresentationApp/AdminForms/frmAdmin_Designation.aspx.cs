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
using Application.Common;
using Application.Presentation;

/////////////////////////////////////////////////////////////////////
// Code Written By   : Pankaj Kumar
// Written Date      : 25th July 2006
// Modification Date : 
// Description       : Designation Add/Edit/Delete
//
/// /////////////////////////////////////////////////////////////////
/// /////////////////////////////////////////////////////////////////////
// Code Updated By   : Jayant Kumar Das
// Written Date      : 16th July 2014
// Modification Date : 
// Description       : Designation Add/Edit/Delete
//
/// /////////////////////////////////////////////////////////////////
public partial class AdminDesignation : LogPage
{
    int DesignationId;
    static string DesignationName;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
        {
            IQCareMsgBox.Show("SessionExpired", this);
            Response.Redirect("~/frmlogin.aspx",true);
        }
        //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Visible = false;
        //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Customise List";
        IDesignation DesignationManager;
        if (!IsPostBack)
        {
            try
            {
                ViewState["TableName"] = Request.QueryString["TableName"].ToString();
                ViewState["CategoryId"] = Request.QueryString["CategoryId"].ToString();
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

                if (Request.QueryString["Upd"] == "1")
                {
                    DesignationId = Convert.ToInt32(Request.QueryString["SelectedId"]);
                    DesignationManager = (IDesignation)ObjectFactory.CreateInstance("BusinessProcess.Administration.BDesignation, BusinessProcess.Administration");
                    DataSet theDS = DesignationManager.GetDesignationByID(DesignationId);
                    this.txtDesignationName.Text = theDS.Tables[0].Rows[0]["Designation_Name"].ToString();
                    DesignationName = theDS.Tables[0].Rows[0]["Designation_Name"].ToString();
                    if (theDS.Tables[0].Rows[0]["Status"].ToString() == "Active")
                    {
                        this.ddStatus.SelectedValue = "0";
                    }
                    else
                    {
                        this.ddStatus.SelectedValue = "1";
                    }
                    if (Convert.ToString(theDS.Tables[0].Rows[0]["DisplayMode"]) == "0")
                    {
                        this.rdouserName.Checked = true;
                    }
                    else if (Convert.ToString(theDS.Tables[0].Rows[0]["DisplayMode"]) == "1")
                    {
                        this.rdouserlist.Checked = true;
                    }
                    this.txtSeq.Text = theDS.Tables[0].Rows[0]["Sequence"].ToString();
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
                DesignationManager = null;
            }
        }
    } 
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string Url = string.Format("{0}?TableName={1}&CategoryId={2}&LstName={3}&Fid={4}&Upd={5}&CCID={6}&ModId={7}", "frmAdmin_CustomList.aspx", ViewState["TableName"].ToString(), ViewState["CategoryId"], ViewState["ListName"].ToString(), ViewState["FID"].ToString(), ViewState["Update"].ToString(), ViewState["CCID"].ToString(), ViewState["ModuleId"].ToString());
        Response.Redirect(Url);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (FieldValidation() == false)
        {
            return;
        }
        IDesignation DesignationManager;
        try
        {   //DisplayMode=0 is for Displaying userlist
            //DisplayMode=1 is for Displaying userlist
            int DisplayMode=0;
            if (rdouserName.Checked == true)
            {
                DisplayMode=0;
            }
            else if (rdouserlist.Checked == true){
                DisplayMode=1;
            }
            ///
            if (Request.QueryString["Upd"] == "0")
            {
                DesignationManager = (IDesignation)ObjectFactory.CreateInstance("BusinessProcess.Administration.BDesignation, BusinessProcess.Administration");
                int DesignationId = DesignationManager.SaveNewDesignation(txtDesignationName.Text, Convert.ToInt32(Session["AppUserId"]), Convert.ToInt32(txtSeq.Text), DisplayMode, Convert.ToInt32(Session["SystemId"]));
                string Url = string.Format("{0}?TableName={1}&CategoryId={2}&LstName={3}&Fid={4}&Upd={5}&CCID={6}&ModId={7}", "frmAdmin_CustomList.aspx", ViewState["TableName"].ToString(), ViewState["CategoryId"], ViewState["ListName"].ToString(), ViewState["FID"].ToString(), 1, ViewState["CCID"].ToString(), ViewState["ModuleId"].ToString());
                Response.Redirect(Url);
                //clear_fields();
            }
            else if (Request.QueryString["Upd"] == "1")
            {
                DesignationManager = (IDesignation)ObjectFactory.CreateInstance("BusinessProcess.Administration.BDesignation, BusinessProcess.Administration");
                if (DesignationName.ToString() != txtDesignationName.Text)
                {
                    int DesignationId = DesignationManager.UpdateDesignation(Convert.ToInt32(Request.QueryString["SelectedId"]), txtDesignationName.Text, Convert.ToInt32(Session["AppUserId"]), Convert.ToInt32(this.ddStatus.SelectedValue), Convert.ToInt32(txtSeq.Text), 1, DisplayMode, Convert.ToInt32(Session["SystemId"]));
                    IQCareMsgBox.Show("DesignationUpdate", this);
                }
                else 
                {
                    int DesignationId = DesignationManager.UpdateDesignation(Convert.ToInt32(Request.QueryString["SelectedId"]), txtDesignationName.Text, Convert.ToInt32(Session["AppUserId"]), Convert.ToInt32(this.ddStatus.SelectedValue), Convert.ToInt32(txtSeq.Text), 0, DisplayMode, Convert.ToInt32(Session["SystemId"]));
                    IQCareMsgBox.Show("DesignationUpdate", this);
                }
                string Url = string.Format("{0}?TableName={1}&CategoryId={2}&LstName={3}&Fid={4}&Upd={5}&CCID={6}&ModId={7}", "frmAdmin_CustomList.aspx", ViewState["TableName"].ToString(), ViewState["CategoryId"], ViewState["ListName"].ToString(), ViewState["FID"].ToString(), ViewState["Update"].ToString(), ViewState["CCID"].ToString(), ViewState["ModuleId"].ToString());
                Response.Redirect(Url);
            }
        }
        catch (Exception err)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1",theBuilder, this);
            return;
        }
        finally
        {
            DesignationManager = null;
        }

    }
    private void clear_fields()
    {
        txtDesignationName.Text = "";
        txtSeq.Text = "";
        txtDesignationName.Focus();
        ddStatus.ClearSelection();
        
    }
    #region "User functions"
    private Boolean FieldValidation()
    {
        if (txtDesignationName.Text.Trim() == "")
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "Designation Name";
            IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
            txtDesignationName.Focus();
            return false;
        }
        if (txtSeq.Text.Trim() == "")
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "Sequence No";
            IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
            txtSeq.Focus();
            return false;
        }
        return true;
    }
    #endregion
}
