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
using Interface.Service;

/////////////////////////////////////////////////////////////////////
// Code Written By   : Pankaj Kumar
// Written Date      : 7th September 2006
// Modify By         : Rakhi Tyagi
// Modification Date : 21 Feb 2007
// Description       : Employee Add/Edit/Delete
//
/// /////////////////////////////////////////////////////////////////
/// 
public partial class AdminForms_frmAdmin_EmployeeMaster : LogPage
{
    #region "Variable Declaration"
    int theEmpID, theUserID,theDeleteFlag,theEmployeeID;
    ICommonData oCommonData = (ICommonData)ObjectFactory.CreateInstance("BusinessProcess.Service.BCommonData,BusinessProcess.Service");
    #endregion

    #region "User functions"

    private Boolean FieldValidation()
    {
        MsgBuilder theBuilder = new MsgBuilder();
        if (txtFirstName.Text.Trim() == "")
        {
            theBuilder.DataElements["Control"] = "First Name";
            IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
            txtFirstName.Focus();
            return false;
        }
        if (txtLastName.Text.Trim() == "")
        {
            theBuilder.DataElements["Control"] = "Last Name";
            IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
            txtLastName.Focus();
            return false;
        }
        if (ddDesignation.SelectedIndex == 0)
        {
            theBuilder.DataElements["Control"] = "Designation";
            IQCareMsgBox.Show("BlankDropDown", theBuilder, this);
            ddDesignation.Focus();
            return false;
        }
       
        return true;
    }

    protected void FillDropDowns()
    {

        BindFunctions BindManager = new BindFunctions();
        IQCareUtils theUtils = new IQCareUtils();

        DataTable theDT = new DataTable();

        //DataSet theDSXML = new DataSet();
        //theDSXML.ReadXml(MapPath("..\\XMLFiles\\AllMasters.con"));
        //DataView theDV = new DataView(theDSXML.Tables["mst_Designation"]);
        DataView theDV = new DataView(oCommonData.getAllDesignation());
        theDV.RowFilter = "DeleteFlag=0";
        if (theDV.Table != null)
        {
            theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            BindManager.BindCombo(ddDesignation, theDT, "Name", "ID");
            theDV.Dispose();
            theDT.Clear();
        }

             
    }
    
    private void clear_fields()
    {
        //Clear all form fields
        txtFirstName.Text = "";
        txtLastName.Text = "";
        ddDesignation.ClearSelection();
        ddStatus.ClearSelection();
        txtFirstName.Focus();
    }
    #endregion

    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
        {
            IQCareMsgBox.Show("SessionExpired", this);
            Response.Redirect("~/frmlogin.aspx",true);
        }
   
        //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Customize Lists >> ";
        //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Employee";

        lblH2.Text = Request.QueryString["name"];

        if (lblH2.Text == "Add")
        {
            lblactive.Visible = false;
            ddStatus.Visible=false;
            lblH2.Text = "Add Employee Name";
        }
        else if (lblH2.Text == "Edit")
        {
            lblH2.Text = "Edit Employee Name";
            btnsave.Text = "Update";
        }
        IEmployeeMst EmployeeManager;
        try
        {
            EmployeeManager = (IEmployeeMst)ObjectFactory.CreateInstance("BusinessProcess.Administration.BEmployeeMst, BusinessProcess.Administration");
            if (IsPostBack != true)
            {
                ViewState["FID"] = Request.QueryString["Fid"].ToString();
                FillDropDowns();

                if (Request.QueryString["name"] == "Edit")
                {
                    //int EmployeeId;
                    theEmpID = Convert.ToInt32(Request.QueryString["EmployeeId"]);
                    DataSet theDS = EmployeeManager.GetEmployeeForID(theEmpID);

                    this.txtFirstName.Text = theDS.Tables[0].Rows[0]["FirstName"].ToString();
                    this.txtLastName.Text = theDS.Tables[0].Rows[0]["LastName"].ToString();
                    this.ddDesignation.SelectedValue = theDS.Tables[0].Rows[0]["DesignationID"].ToString();
                    if (theDS.Tables[0].Rows[0]["DeleteFlag"].ToString() == "1")
                    {
                        this.ddStatus.SelectedValue = "1";
                    }
                    else
                    {
                        this.ddStatus.SelectedValue = "0";
                    } 
                }
            }
            AuthenticationManager Authentication = new AuthenticationManager();
            if (Authentication.HasFunctionRight(Convert.ToInt32(ViewState["FID"]), FunctionAccess.Update, (DataTable)Session["UserRight"]) == false)
            {
                btnsave.Enabled = false;
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
            EmployeeManager = null;
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (FieldValidation() == false)
        {
            return;
        }
        IEmployeeMst EmployeeManager;
        DataTable theResultDT;
        try
        {
            EmployeeManager = (IEmployeeMst)ObjectFactory.CreateInstance("BusinessProcess.Administration.BEmployeeMst, BusinessProcess.Administration");
            theUserID = Convert.ToInt32(Session["AppUserId"].ToString());
            if (Request.QueryString["name"] == "Add")
            {
                theEmployeeID = 0;
                theDeleteFlag = 0;
                theResultDT = (DataTable)EmployeeManager.SaveNewEmployee(txtFirstName.Text, txtLastName.Text, Convert.ToInt32(ddDesignation.SelectedValue), theEmployeeID, theDeleteFlag, theUserID);
                if (theResultDT.Rows[0][0].ToString() == "0")
                {
                    IQCareMsgBox.Show("EmployeeExists", this);
                    return;
                }
            }
            else if (Request.QueryString["name"] == "Edit")
            {
                theEmployeeID = Convert.ToInt32(Request.QueryString["EmployeeID"]);
                if (ddStatus.SelectedValue.ToString() == "1")
                {
                    theDeleteFlag = 1;
                }
                else
                {
                    theDeleteFlag = 0;
                }
                theResultDT = (DataTable)EmployeeManager.SaveNewEmployee(txtFirstName.Text, txtLastName.Text, Convert.ToInt32(ddDesignation.SelectedValue), theEmployeeID, theDeleteFlag, theUserID);
            
                if (theResultDT.Rows[0][0].ToString() == "0")
                {
                    IQCareMsgBox.Show("UpdateEmployee", this);
                    return;
                }
            }
            string url = "frmAdmin_EmployeeMasterlist.aspx?TableName=Employee&CategoryId=0&LstName=" + Request.QueryString["LstName"].ToString() + "&Fid=" + Request.QueryString["Fid"].ToString() + "&Upd=" + theUserID + "&CCID=" + Request.QueryString["Fid"].ToString();
            Response.Redirect(url);

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
            EmployeeManager = null;
        }


    }
    

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string url = "frmAdmin_EmployeeMasterlist.aspx?TableName=Employee&CategoryId=0&LstName=" + Request.QueryString["LstName"].ToString() + "&Fid=" + Request.QueryString["Fid"].ToString() + "&Upd=" + theUserID + "&CCID=" + Request.QueryString["Fid"].ToString();
        Response.Redirect(url);
    }
    protected void btnreset_Click(object sender, EventArgs e)
    {
        clear_fields();
    }
}
