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
// Modify Date       : Rakhi Tyagi 
// Modification Date : 22 Feb 2007
// Description       : Lab test List
//
/// /////////////////////////////////////////////////////////////////
public partial class AdminLaboratoryTest_Master : LogPage
{
    #region "Variable Declaration"
    int LabId;
    public int LabIdforselectList;
    ILabMst LabManager;
    string theURL;
    DataTable theResultDT;
    #endregion
    #region "User Functions"
   
    protected void FillDropDowns()
    {
        LabManager = (ILabMst)ObjectFactory.CreateInstance("BusinessProcess.Administration.BLabMst, BusinessProcess.Administration");
        DataSet theDS = LabManager.GetDropDowns();
        BindFunctions BindManager = new BindFunctions();
        BindManager.BindCombo(ddDepartment, theDS.Tables[0], "LabDepartmentName", "LabDepartmentID");
        BindManager.BindCombo(ddUnit, theDS.Tables[2], "Name", "UnitID");
    }

    private Boolean FieldValidation()
    {
        //Validate fields input values
        if (txtLabName.Text.Trim() == "")
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "Laboratory Test";
            IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
            txtLabName.Focus();
            return false;
        }
        if (ddDepartment.SelectedValue == "0")
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "Laboratory Test";
            IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
            ddDepartment.Focus();
            return false;
        }
        if (ddUnit.SelectedValue == "0")
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "Laboratory Test";
            IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
            ddUnit.Focus();
            return false;
        }
        if (txtSeq.Text.Trim() == "")
        {
            MsgBuilder theBuilder = new MsgBuilder();
            #region "20-jun-07 - 1"
                //theBuilder.DataElements["Control"] = "Sequence No";
                theBuilder.DataElements["Control"] = "Priority";
            #endregion
            IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
            txtSeq.Focus();
            return false;
        }
        if (ddlDataType.SelectedItem.Text == "Numeric")
        {
            if (txtLowerBoundary.Text == "" || txtUpperBoundary.Text == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Boundary Value";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                if(txtLowerBoundary.Text=="") 
                    txtLowerBoundary.Focus();
                else
                    txtUpperBoundary.Focus();

                return false;
            }
            if (Convert.ToDecimal(txtLowerBoundary.Text) > Convert.ToDecimal(txtUpperBoundary.Text))
            {
               
                IQCareMsgBox.Show("Boundary", this);
                txtUpperBoundary.Focus();
                return false;
            }
        }
        if ((ddlDataType.SelectedItem.Text == "Select List") && (Session["LaboratorySelectList"] == null))
        {
            IQCareMsgBox.Show("LabSelectList", this);
            return false;
        }
        if (Session["LaboratorySelectList"] != null)
        {
            DataTable dt = (DataTable)Session["LaboratorySelectList"];
            if (dt.Rows.Count == 0)
            {
                IQCareMsgBox.Show("LabSelectList", this);
                return false;
            }
        }
       

        return true;
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
        {
            IQCareMsgBox.Show("SessionExpired", this);
            Response.Redirect("~/frmlogin.aspx",true);
        }
      //  (Master.FindControl("lblheader") as Label).Text = "Customise List";
        LabIdforselectList = 0;
        
        lblH2.Text = Request.QueryString["name"];
        //(Master.FindControl("lblRoot") as Label).Text = " » Customize Lists";
        //(Master.FindControl("lblMark") as Label).Visible = false;
        //(Master.FindControl("lblheader") as Label).Text = "Laboratory";
        //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Customize Lists >> ";
        //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Laboratory";
        ViewState["FID"] = Request.QueryString["Fid"].ToString();
        if (lblH2.Text == "Add")
        {
            ddStatus.Visible = false;
            lblStatus1.Visible = false;
            lblH2.Text = "Add Laboratory Test";
            //tdDataType.ColSpan = 2;
        }
        else if (lblH2.Text == "Edit")
        {
            lblH2.Text = "Edit Laboratory Test";
            btnSave.Text = "Update";
            //txtLabName.Enabled = false;
        }
        try
        {
            if (!IsPostBack)
            {
                Session.Remove("LaboratorySelectList");
                ViewState["LabValueID"] = "";
                ddlDataType.Attributes.Add("OnChange", "JavaScript:ShowHideBoundary();");
                txtLowerBoundary.Attributes.Add("onkeyup", "chkDecimal('" + txtLowerBoundary.ClientID + "');"); ;
                txtUpperBoundary.Attributes.Add("onkeyup", "chkDecimal('" + txtUpperBoundary.ClientID + "');"); ;
                //txtCD4.Attributes.Add("onkeyup", "chkDecimal('" + txtCD4.ClientID + "');");
                FillDropDowns();
                ViewState["UserID"] = Session["AppUserId"].ToString();
                AuthenticationManager Authentication = new AuthenticationManager();
                if (Convert.ToInt32(ViewState["FID"]) != 0)
                {
                    if (Authentication.HasFunctionRight(Convert.ToInt32(ViewState["FID"]), FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
                    {
                        btnSave.Enabled = false;
                    }
                }
                if (Request.QueryString["name"] == "Edit")
                {
                    int LabId;
                    LabId = Convert.ToInt32(Request.QueryString["LabId"]);

                    

                    LabManager = (ILabMst)ObjectFactory.CreateInstance("BusinessProcess.Administration.BLabMst, BusinessProcess.Administration");
                    DataSet theDS = LabManager.GetLabByID(LabId);
                    this.txtLabName.Text = theDS.Tables[0].Rows[0]["LabName"].ToString();
                    this.ddDepartment.SelectedValue = theDS.Tables[0].Rows[0]["LabDepartmentID"].ToString();
                    // Change by Rahmat for set Lab Unit Value
                    if (ddUnit.Items.FindByValue(Convert.ToString(theDS.Tables[0].Rows[0]["UnitId"] == null ? "" : theDS.Tables[0].Rows[0]["UnitId"])) != null)
                    {
                        this.ddUnit.SelectedValue = theDS.Tables[0].Rows[0]["UnitId"].ToString();
                    }
                    if (theDS.Tables[0].Rows[0]["TestResults1"].ToString() == "0" && theDS.Tables[0].Rows[0]["TestResults"].ToString() == "0.00" && theDS.Tables[0].Rows[0]["TestResultId"].ToString() == "0")
                    {
                        this.txtLabName.Enabled = true;
                        ddDepartment.Enabled = true;
                        ddlDataType.Enabled = true;
                        txtLowerBoundary.Enabled = true;
                        txtUpperBoundary.Enabled = true;
                        btnselectList.Disabled = false;

                    }
                    else
                    {
                        this.txtLabName.Enabled = false;
                        ddDepartment.Enabled = false;
                        ddlDataType.Enabled = false;
                        txtLowerBoundary.Enabled = false;
                        txtUpperBoundary.Enabled = false;
                        //btnselectList.Disabled = true;
                    }
                    //if (theDS.Tables[0].Rows[0]["LabTypeId"].ToString() == "1")
                    //{
                    //    this.txtLabName.Enabled=true;
                    //    ddDepartment.Enabled = true;
                    //    ddlDataType.Enabled = true;
                    //    //ddUnit.Enabled = true;
                       
                    //}
                    //else
                    //{
                    //    this.txtLabName.Enabled = false;
                    //    ddDepartment.Enabled = false;
                    //    ddlDataType.Enabled = false;
                    //    //ddUnit.Enabled = false;
                    //}

                    if (theDS.Tables[0].Rows[0]["DeleteFlag"].ToString() == "1")
                    {
                        this.ddStatus.SelectedValue = "1";

                    }
                    else
                    {
                        this.ddStatus.SelectedValue = "0";
                    }
                    this.txtSeq.Text = (theDS.Tables[0].Rows[0]["Sequence"].ToString() == "") ? "1" : theDS.Tables[0].Rows[0]["Sequence"].ToString();
                    if (theDS.Tables[1].Rows.Count > 0)
                    {
                        if (theDS.Tables[1].Rows[0]["SubTestId"].ToString() != "" )
                        {
                            ddlDataType.SelectedIndex = 1; //Numeric
                            //pnlBoundary.Visible = true;
                            //pnlBoundary2.Visible = true;
                            RegisterClientScriptBlock("ShowHideBoundary", "");
                            txtLowerBoundary.Text = theDS.Tables[1].Rows[0]["MinBoundaryValue"].ToString();
                            txtUpperBoundary.Text = theDS.Tables[1].Rows[0]["MaxBoundaryValue"].ToString();
                            ViewState["LabValueID"] = theDS.Tables[1].Rows[0]["Id"].ToString(); //id from lnk_labValue
                        }
                        else
                        {
                          //  ddlDataType.SelectedIndex = 0; //Text
                            if (theDS.Tables[0].Rows[0]["DataType"].ToString() == "Numeric")
                            {
                                ddlDataType.SelectedIndex = 1; // Numeric
                            }
                            else if (theDS.Tables[0].Rows[0]["DataType"].ToString() == "Select List")
                            {
                                ddlDataType.SelectedIndex = 2; //select list
                                getselectlist(theDS.Tables[2]);
                                LabIdforselectList = Convert.ToInt32(Request.QueryString["LabId"]);
                            }
                            else
                            {
                                ddlDataType.SelectedIndex = 0; //Text
                            }
                            RegisterClientScriptBlock("ShowHideBoundary", "");
                            //pnlBoundary.Visible = false;
                            //pnlBoundary2.Visible = false;
                            txtLowerBoundary.Text = "";
                            txtUpperBoundary.Text = "";
                        }
                    }
                    else
                    {
                        if (theDS.Tables[0].Rows[0]["DataType"].ToString() == "Numeric")
                        {
                            ddlDataType.SelectedIndex = 1; // Numeric
                        }
                        else if (theDS.Tables[0].Rows[0]["DataType"].ToString() == "Select List")
                        {
                            ddlDataType.SelectedIndex = 2; //select list
                            getselectlist(theDS.Tables[2]);
                            LabIdforselectList = Convert.ToInt32(Request.QueryString["LabId"]);
                        }
                        else if (theDS.Tables[0].Rows[0]["DataType"].ToString() == "Group")
                        {
                            ddlDataType.SelectedIndex = 3; // Group
                        }
                        else
                        {
                            ddlDataType.SelectedIndex = 0; //Text
                        }
                        RegisterClientScriptBlock("ShowHideBoundary", "");
                        //pnlBoundary.Visible = false;
                        //pnlBoundary2.Visible = false;
                        txtLowerBoundary.Text = "";
                        txtUpperBoundary.Text = "";
                    }
                    
                }
                if (Authentication.HasFunctionRight(Convert.ToInt32(ViewState["FID"]), FunctionAccess.Update, (DataTable)Session["UserRight"]) == false)
                {
                    btnSave.Enabled = false;
                }
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
            LabManager = null;
        }
    }
    private void getselectlist(DataTable dtlab)
    {
        DataTable theDTselect = CreateSelectedTable();
        DataRow theDR;
        for (int i = 0; i < dtlab.Rows.Count; i++)
        {

            theDR = theDTselect.NewRow();
            theDR["selectlist"] = dtlab.Rows[i]["Result"].ToString().Trim();
            theDTselect.Rows.Add(theDR);
        }
        Session["LaboratorySelectList"] = theDTselect;
    }
    private DataTable CreateSelectedTable()
    {
        DataTable theDT = new DataTable();
        theDT.Columns.Add("selectlist", System.Type.GetType("System.String"));
        return theDT;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (FieldValidation() == false)
        {
            return;
        }
       
        try
        {
            if (Request.QueryString["name"] == "Add")
            {
               
                LabManager = (ILabMst)ObjectFactory.CreateInstance("BusinessProcess.Administration.BLabMst, BusinessProcess.Administration");
                //if ((ddlDataType.SelectedItem.Text == "Text")||(ddlDataType.SelectedItem.Text == "Select List") )
                if ((ddlDataType.SelectedItem.Text == "Text") || (ddlDataType.SelectedItem.Text == "Select List") || (ddlDataType.SelectedItem.Text == "Group"))
                    theResultDT = LabManager.SaveNewLab(txtLabName.Text, Convert.ToInt32(ddDepartment.SelectedValue), 1, Convert.ToInt32(ViewState["UserID"]), ddlDataType.SelectedItem.Text.ToString(), 0, 0, Convert.ToInt32(this.txtSeq.Text), Convert.ToInt32(ddUnit.SelectedValue));
                else
                    theResultDT = LabManager.SaveNewLab(txtLabName.Text, Convert.ToInt32(ddDepartment.SelectedValue), 1, Convert.ToInt32(ViewState["UserID"]), ddlDataType.SelectedItem.Text.ToString(), Convert.ToDecimal(txtUpperBoundary.Text), Convert.ToDecimal(txtLowerBoundary.Text), Convert.ToInt32(this.txtSeq.Text), Convert.ToInt32(ddUnit.SelectedValue));
                
                if (theResultDT.Rows[0][0].ToString() == "0")
                {
                    IQCareMsgBox.Show("LabExists", this);
                    return;
                }

                if (ddlDataType.SelectedItem.Text == "Select List")
                {
                    if (Session["LaboratorySelectList"] != null)
                    {
                        DataTable dt = (DataTable)Session["LaboratorySelectList"];
                        int rowaffected = LabManager.SaveNewLabselectList(Convert.ToInt32(theResultDT.Rows[0][0].ToString()), dt, Convert.ToInt32(ViewState["UserID"]));
                        Session.Remove("LaboratorySelectList");
                    }
                    else
                    {
                        IQCareMsgBox.Show("LabSelectList", this);
                    return;
                      
                    }
                }
            }
            else if (Request.QueryString["name"] == "Edit")
            {
               
                LabManager = (ILabMst)ObjectFactory.CreateInstance("BusinessProcess.Administration.BLabMst, BusinessProcess.Administration");
                if (ViewState["LabValueID"].ToString() == "")
                {
                    //if ((ddlDataType.SelectedItem.Text == "Text")||(ddlDataType.SelectedItem.Text == "Select List"))
                    if ((ddlDataType.SelectedItem.Text == "Text") || (ddlDataType.SelectedItem.Text == "Select List") || (ddlDataType.SelectedItem.Text == "Group"))
                        theResultDT = LabManager.UpdateLab(Convert.ToInt32(Request.QueryString["labid"]), txtLabName.Text, Convert.ToInt32(ddDepartment.SelectedValue), 1, Convert.ToInt32(ViewState["UserID"]), Convert.ToInt32(this.ddStatus.SelectedValue), ddlDataType.SelectedItem.Text.ToString(), 0, 0, 0, Convert.ToInt32(this.txtSeq.Text), Convert.ToInt32(ddUnit.SelectedValue));
                    else
                        theResultDT = LabManager.UpdateLab(Convert.ToInt32(Request.QueryString["labid"]), txtLabName.Text, Convert.ToInt32(ddDepartment.SelectedValue), 1, Convert.ToInt32(ViewState["UserID"]), Convert.ToInt32(this.ddStatus.SelectedValue), ddlDataType.SelectedItem.Text.ToString(), Convert.ToDecimal(txtUpperBoundary.Text), Convert.ToDecimal(txtLowerBoundary.Text), 0, Convert.ToInt32(this.txtSeq.Text), Convert.ToInt32(ddUnit.SelectedValue));
                }
                else
                {
                    if ((ddlDataType.SelectedItem.Text == "Text") || (ddlDataType.SelectedItem.Text == "Select List"))
                        theResultDT = LabManager.UpdateLab(Convert.ToInt32(Request.QueryString["labid"]), txtLabName.Text, Convert.ToInt32(ddDepartment.SelectedValue), 1, Convert.ToInt32(ViewState["UserID"]), Convert.ToInt32(this.ddStatus.SelectedValue), ddlDataType.SelectedItem.Text.ToString(), 0, 0, Convert.ToInt32(ViewState["LabValueID"]), Convert.ToInt32(this.txtSeq.Text), Convert.ToInt32(ddUnit.SelectedValue));
                    else
                        theResultDT = LabManager.UpdateLab(Convert.ToInt32(Request.QueryString["labid"]), txtLabName.Text, Convert.ToInt32(ddDepartment.SelectedValue), 1, Convert.ToInt32(ViewState["UserID"]), Convert.ToInt32(this.ddStatus.SelectedValue), ddlDataType.SelectedItem.Text.ToString(), Convert.ToDecimal(txtUpperBoundary.Text), Convert.ToDecimal(txtLowerBoundary.Text), Convert.ToInt32(ViewState["LabValueID"]), Convert.ToInt32(this.txtSeq.Text), Convert.ToInt32(ddUnit.SelectedValue));
                }

                if (theResultDT.Rows[0][0].ToString() == "0")
                {
                    IQCareMsgBox.Show("UpdateLab", this);
                    return;
                }
                if (ddlDataType.SelectedItem.Text == "Select List")
                {
                    if (Session["LaboratorySelectList"] != null)
                    {
                        DataTable dt = (DataTable)Session["LaboratorySelectList"];
                        int rowaffected = LabManager.SaveNewLabselectList(Convert.ToInt32(theResultDT.Rows[0][0].ToString()), dt, Convert.ToInt32(ViewState["UserID"]));
                        Session.Remove("LaboratorySelectList");
                    }
                    else
                    {
                        IQCareMsgBox.Show("LabSelectList", this);
                        return;

                    }

                }
            }
            //theURL = "frmAdmin_LabTestlist.aspx";
            string theUrl = string.Format("{0}?Fid={1}", "frmAdmin_LabTestlist.aspx", ViewState["FID"].ToString());
            Response.Redirect(theUrl);
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
           LabManager = null;
        }

    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        //theURL = "frmAdmin_LabTestlist.aspx";
        string theUrl = string.Format("{0}?Fid={1}", "frmAdmin_LabTestlist.aspx", ViewState["FID"].ToString());
        Response.Redirect(theUrl);

    }

   
}
