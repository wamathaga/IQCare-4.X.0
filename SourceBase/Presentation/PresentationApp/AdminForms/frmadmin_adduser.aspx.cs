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
using System.Linq;
using Interface.Administration;
using Application.Common;
using Application.Presentation;
using Interface.Security;



public partial class frmadmin_adduser : LogPage
{
    /////////////////////////////////////////////////////////////////////
    // Code Written By   : Sanjay Rana
    // Written Date      : 25th July 2006
    // Modification Date : 
    // Description       : User Master
    //
    /// /////////////////////////////////////////////////////////////////

    public Hashtable htGroups;
    public Hashtable htStore;
    int theUserId;  /// Variable for Passing UserId///

    #region "User Functions"

    private void clear_fields()
    {
        txtfirstname.Text = "";
        txtlastname.Text = "";
        txtusername.Text = "";
        txtPassword.Text = "";
        txtConfirmpassword.Text = "";
        txtPassword.Attributes.Remove("Value");
        txtConfirmpassword.Attributes.Remove("Value");
        lstUsergroup.ClearSelection();
        txtEmail.Text = "";
        txtPhone.Text = "";
        if (theUserId != 0)
        {
            btnSave.Text = "Update";
            //GetUserRecords();
        }
        // txtlastname.Focus();
        chkIsEmployee.Checked = false;
        chkIsEmployee.Focus();
    }


    private void GetUserRecords()
    {
        try
        {
            Iuser UserManager;
            UserManager = (Iuser)ObjectFactory.CreateInstance("BusinessProcess.Administration.BUser, BusinessProcess.Administration");
            DataSet theDS = UserManager.GetUserRecord(theUserId);
            chkIsEmployee.Checked = (theDS.Tables[0].Rows[0]["IsEmployee"] == DBNull.Value) ? false : Convert.ToBoolean(theDS.Tables[0].Rows[0]["IsEmployee"]);

            if (chkIsEmployee.Checked)
            {
                lblPassword.Attributes.Add("class", "right required");
                lblConfirmPassword.Attributes.Add("class", "right required");
                lblUserGroup.Attributes.Add("class", "right required");
            }
            txtfirstname.Text = theDS.Tables[0].Rows[0]["UserFirstName"].ToString();
            txtlastname.Text = theDS.Tables[0].Rows[0]["UserLastName"].ToString();
            txtEmail.Text = theDS.Tables[0].Rows[0]["Email"].ToString();
            txtPhone.Text = theDS.Tables[0].Rows[0]["Phone"].ToString();
            dddesignation.SelectedValue = theDS.Tables[0].Rows[0]["Designation"].ToString();
            if (theUserId != 0 && txtusername.Text == "")
            {
                txtusername.Text = theDS.Tables[0].Rows[0]["UserName"].ToString();
            }
            Utility theUtil = new Utility();
            string thePass = theUtil.Decrypt(theDS.Tables[0].Rows[0]["Password"].ToString());
            txtPassword.Attributes.Add("Value", thePass);
            txtConfirmpassword.Attributes.Add("Value", thePass);
            //ddEmployee.SelectedValue = theDS.Tables[0].Rows[0]["EmployeeId"].ToString();
            //txtusername.ReadOnly = true;

            foreach (DataRow theDR in theDS.Tables[1].Rows)
            {
                int i = 0;
                for (i = 0; i < lstUsergroup.Items.Count; i++)
                {
                    if (lstUsergroup.Items[i].Value == theDR["groupid"].ToString())
                    {
                        lstUsergroup.Items[i].Selected = true;

                        if (lstUsergroup.Items[i].Text == "System Admin")
                        {
                            lstUsergroup.Items[i].Enabled = false;
                            txtfirstname.ReadOnly = true;
                            txtlastname.ReadOnly = true;
                            txtusername.ReadOnly = true;
                            btnDelete.Enabled = false;
                        }

                    }
                }
            }

            foreach (DataRow theDR in theDS.Tables[2].Rows)
            {
                int i = 0;

                for (i = 0; i < chklistStores.Items.Count; i++)
                {
                    if (chklistStores.Items[i].Value == theDR["StoreId"].ToString())
                    {
                        chklistStores.Items[i].Selected = true;
                    }
                }
            }

        }
        catch (Exception err)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
        }

    }

    private void fill_dropdowns()
    {
        try
        {
            Iuser CmbManager;
            CmbManager = (Iuser)ObjectFactory.CreateInstance("BusinessProcess.Administration.BUser,BusinessProcess.Administration");
            DataSet theDS = CmbManager.FillDropDowns();
            CmbManager = null;

            //// User Groups List
            BindFunctions GblCls = new BindFunctions();
            GblCls.BindCheckedList(lstUsergroup, theDS.Tables[0], "groupname", "groupid");

            //Stores list
            IQCareUtils theUtils = new IQCareUtils();
            DataTable dt = new DataTable();
            DataSet theXMLDS = new DataSet();
            theXMLDS.ReadXml(Server.MapPath("..\\XMLFiles\\AllMasters.con"));
            DataView theDV = new DataView(theXMLDS.Tables["mst_Store"]);
            theDV.RowFilter = "DeleteFlag=0";
            if (theDV.Table != null)
            {
                dt = theUtils.CreateTableFromDataView(theDV);
            }
            GblCls.BindCheckedList(chklistStores, dt, "Name", "Id");
            //Designation
            DataTable dtdesig = new DataTable();
            theDV = new DataView(theXMLDS.Tables["Mst_Designation"]);
            theDV.RowFilter = "DeleteFlag=0";
            if (theDV.Table != null)
            {
                dt = theUtils.CreateTableFromDataView(theDV);
            }
            GblCls.BindCombo(dddesignation, dt, "Name", "Id");
            //GblCls.BindCombo(ddEmployee,dt,"EmployeeName","EmployeeId");
        }
        catch (Exception err)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
        }
    }

    private Boolean FieldValidation()
    {
        if (txtlastname.Text.Trim() == "")
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "Last Name";
            IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
            txtlastname.Focus();
            return false;
        }
        if (txtfirstname.Text.Trim() == "")
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "First Name";
            IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
            txtfirstname.Focus();
            return false;
        }
        if (txtusername.Text.Trim() == "")
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "User Name";
            IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
            txtusername.Focus();
            return false;
        }
        if (dddesignation.SelectedIndex == 0)
        {
            MsgBuilder theMsg = new MsgBuilder();
            theMsg.DataElements["Control"] = "Designation";
            IQCareMsgBox.Show("BlankDropDown", theMsg, this);
            return false;
        }

        if (chkIsEmployee.Checked)
        {
            if (txtPassword.Text.Trim() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Password";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtPassword.Focus();
                return false;
            }
            if (txtPassword.Text.Trim() != txtConfirmpassword.Text.Trim())
            {
                IQCareMsgBox.Show("PasswordNotMatch", this);
                txtPassword.Text = "";
                txtConfirmpassword.Text = "";
                txtPassword.Focus();
                return false;
            }
            if (htGroups.Count < 1)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "User Group";
                IQCareMsgBox.Show("BlankList", theBuilder, this);
                lstUsergroup.Focus();
                return false;
            }
            DataSet theXMLDS = new DataSet();
            theXMLDS.ReadXml(Server.MapPath("..\\XMLFiles\\AllMasters.con"));
            DataView theDV = new DataView(theXMLDS.Tables["Mst_Facility"]);
            theDV.RowFilter = "FacilityId=" + Convert.ToInt32(Session["AppLocationId"]);
            if (theDV[0]["StrongPassFlag"] != DBNull.Value)
            {
                if (Convert.ToInt32(theDV[0]["StrongPassFlag"]) == 1)
                {
                    if (!IsStrongPassword(txtPassword.Text.Trim(), txtfirstname.Text, txtlastname.Text, txtusername.Text))
                    {
                        IQCareMsgBox.Show("StrongPassword", this);
                        return false;
                    }
                }
            }
        }
        return true;
    }

    private void CreateGroupTable()
    {
        htGroups = new Hashtable();
        htGroups.Clear();
        int i = 0;
        int j = 1;
        for (i = 0; i < lstUsergroup.Items.Count; i++)
        {
            if (Convert.ToInt32(lstUsergroup.Items[i].Selected) == 1)
            {
                htGroups.Add(j, lstUsergroup.Items[i].Value);
                j = j + 1;
            }
        }

        htStore = new Hashtable();
        htStore.Clear();
        int k = 0;
        int l = 1;
        for (k = 0; k < chklistStores.Items.Count; k++)
        {
            if (Convert.ToInt32(chklistStores.Items[k].Selected) == 1)
            {
                htStore.Add(l, chklistStores.Items[k].Value);
                l = l + 1;
            }
        }

    }

    #endregion

    protected void Page_Init(object sender, EventArgs e)
    {
        if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
        {
            IQCareMsgBox.Show("SessionExpired", this);
            Response.Redirect("~/frmlogin.aspx", true);
        }
        //(Master.FindControl("lblheader") as Label).Text = "User Administration"; 
        //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Visible = false;
        //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "User Administration";
        theUserId = Convert.ToInt32(Request.QueryString["SelectedUserId"]);
        fill_dropdowns();
        clear_fields();
        AuthenticationManager Authentication = new AuthenticationManager();

        if (theUserId == 0)
        {
            if (Authentication.HasFunctionRight(ApplicationAccess.UserAdministration, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
            {
                btnSave.Enabled = false;
            }
            IQCareMsgBox.ShowConfirm("UserSaveRecord", btnSave);
            lblh2.Text = "Add User";
            btnDelete.Visible = false;
        }
        else
        {
            if (Authentication.HasFunctionRight(ApplicationAccess.UserAdministration, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
            {
                btnSave.Enabled = false;
            }

            if (Authentication.HasFunctionRight(ApplicationAccess.UserAdministration, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
            {
                btnDelete.Enabled = false;
            }
            IQCareMsgBox.ShowConfirm("UpdateUserRecord", btnSave);
            IQCareMsgBox.ShowConfirm("RemoveUserRecord", btnDelete);
            lblh2.Text = "Edit/Remove User";
            btnDelete.Visible = true;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
        {
            IQCareMsgBox.Show("SessionExpired", this);
            Response.Redirect("~/frmlogin.aspx", true);
        }
        DataSet theXMLDS = new DataSet();
        theXMLDS.ReadXml(Server.MapPath("..\\XMLFiles\\AllMasters.con"));
        DataView theDV = new DataView(theXMLDS.Tables["Mst_Facility"]);
        theDV.RowFilter = "FacilityId=" + Convert.ToInt32(Session["AppLocationId"]);
        Panel pnl = IQCareMsgBox.FindControlFromMaster<Panel>("divError", this.Master);
        Label lblerror = IQCareMsgBox.FindControlFromMaster<Label>("lblError", this.Master);
        txtPhone.Attributes.Add("onblur", "phonenumber('" + lblerror.ClientID + "')");
        txtEmail.Attributes.Add("onblur", "ValidateEmail(" + txtEmail.ClientID + ",'" + lblerror.ClientID + "')");


        //txtusername.Attributes.Add("readonly", "true");
        //txtfirstname.Attributes.Add("onkeypress", "GetUserFirstName('" + txtfirstname.ClientID + "','" + txtlastname.ClientID + "','" + txtusername.ClientID + "')");
        //txtlastname.Attributes.Add("onkeyup", "GetUserLastName('" + txtlastname.ClientID + "','" + txtfirstname.ClientID + "','" + txtusername.ClientID + "')");

        //txtConfirmpassword.Attributes.Add("onblur", "return validateStrongPassword('" + txtConfirmpassword.ClientID + "', {length:[6, Infinity],lower:1,	upper:1,numeric:1,alpha:1,badWords:['password'],badSequenceLength: 4})");
        if (theDV[0]["StrongPassFlag"] != DBNull.Value)
        {
            if (Convert.ToInt32(theDV[0]["StrongPassFlag"]) == 1)
            {
                txtPassword.Attributes.Remove("onblur");
                txtPassword.Attributes.Add("onblur", "if (validnewuserstrngpwd()){\n NotifyMessage(document.getElementById('unique_id').innerHTML); \n document.getElementById('" + txtPassword.ClientID + "').value='' \n}");

            }
        }

        if (!chkIsEmployee.Checked)
        {
            lblPassword.Attributes.Add("class", "right");
            lblConfirmPassword.Attributes.Add("class", "right");
            lblUserGroup.Attributes.Add("class", "right");
        }

        if (!IsPostBack)
        {
            if (theUserId != 0)
            {
                GetUserRecords();
                if (theDV[0]["StrongPassFlag"] != DBNull.Value)
                {
                    if (Convert.ToInt32(theDV[0]["StrongPassFlag"]) == 1)
                    {
                        txtPassword.Attributes.Remove("onblur");
                        txtPassword.Attributes.Add("onblur", "if (validstrngpwd('" + txtfirstname.Text + "','" + txtlastname.Text + "','" + txtusername.Text + "')){\n NotifyMessage( document.getElementById('unique_id').innerHTML);\n document.getElementById('" + txtPassword.ClientID + "').value='' \n}");

                    }
                }
            }
            else
            {
                if (theDV[0]["StrongPassFlag"] != DBNull.Value)
                {
                    if (Convert.ToInt32(theDV[0]["StrongPassFlag"]) == 1)
                    {
                        txtPassword.Attributes.Remove("onblur");
                        txtPassword.Attributes.Add("onblur", "if (validnewuserstrngpwd()){\n NotifyMessage( document.getElementById('unique_id').innerHTML);\n document.getElementById('" + txtPassword.ClientID + "').value='' \n}");

                    }
                }
            }



        }
        else
        {
            if (chkIsEmployee.Checked)
            {
                lblPassword.Attributes.Add("class", "right required");
                lblConfirmPassword.Attributes.Add("class", "right required");
                lblUserGroup.Attributes.Add("class", "right required");
            }

        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Page_Init(sender, e);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        CreateGroupTable();

        if (FieldValidation() == false)
        {
            return;
        }

        Iuser UserManager;
        try
        {
            UserManager = (Iuser)ObjectFactory.CreateInstance("BusinessProcess.Administration.BUser,BusinessProcess.Administration");
            if (btnSave.Text == "Save")
            {
                int UserId = UserManager.SaveNewUser(chkIsEmployee.Checked, txtfirstname.Text, txtlastname.Text, txtusername.Text, txtPassword.Text, txtEmail.Text, txtPhone.Text, Convert.ToInt32(Session["AppUserId"]), Convert.ToInt32(dddesignation.SelectedValue), htGroups, htStore);
                if (UserId == 0)
                {
                    IQCareMsgBox.Show("UserExists", this);
                    txtusername.Focus();
                    return;
                }
                else
                {
                    IQCareMsgBox.Show("UserSave", this);
                    //Page_Init(sender, e);
                }
            }
            else
            {
                UserManager.UpdateUserRecord(chkIsEmployee.Checked, txtfirstname.Text, txtlastname.Text, txtusername.Text, txtPassword.Text, txtEmail.Text, txtPhone.Text, theUserId, Convert.ToInt32(Session["AppUserId"]), Convert.ToInt32(dddesignation.SelectedValue), htGroups, htStore);
                IQCareMsgBox.Show("UserUpdate", this);
                //Page_Init(sender, e); 
            }
            btnExit_Click(sender, e);
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
            UserManager = null;
        }
    }

    public static bool IsStrongPassword(string password, string fname, string lname, string uname)
    {
        // Minimum and Maximum Length of field - 6 to 12 Characters
        if (password.Length < 6)
            return false;

        // Special Characters - Not Allowed
        // Spaces - Not Allowed
        //if (!(password.All(c => char.IsLetter(c) || char.IsDigit(c))))
        //    return false;

        // Numeric Character - At least one character
        if (!password.Any(c => char.IsDigit(c)))
            return false;

        // At least one Capital Letter
        if (!password.Any(c => char.IsUpper(c)))
            return false;


        // Repetitive Characters - Allowed only two repetitive characters
        //var repeatCount = 0;
        //var lastChar = '\0';
        //foreach (var c in password)
        //{
        //    if (c == lastChar)
        //        repeatCount++;
        //    else
        //        repeatCount = 0;
        //    if (repeatCount == 3)
        //        return false;
        //    lastChar = c;
        //}

        //const string lookup = "ABCDEFGHIJKLMNOPQSTUVabcdefghijklmnopqrstuvwxyz0123456789";

        //for (var i = 0; i < (password.Length - 2); i++)
        //{
        //    var char1 = password.Substring(i, 1);
        //    var char2 = password.Substring(i + 1, 1);
        //    var char3 = password.Substring(i + 2, 1);
        //    var char4 = password.Substring(i + 3, 1);
        //    var char5 = password.Substring(i + 4, 1);
        //    if (lookup.Contains(char1))
        //    {
        //        if ((char1 == char2) && (char1 == char3) && (char1 == char4) && (char1 == char5))
        //        {
        //            return false;
        //        }
        //    }
        //}


        string[] Badwords = { "password", Convert.ToString(fname), Convert.ToString(lname), Convert.ToString(uname) };

        if (Badwords.Any(b => password.ToLower().Contains(b.ToLower())))
        {
            return false;
        }

        return true;
    }

    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmAdmin_UserList.aspx");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Iuser UserManager;
        int theAffectedRow = 0;
        string theUrl;
        try
        {
            if (theUserId != 0)
            {
                UserManager = (Iuser)ObjectFactory.CreateInstance("BusinessProcess.Administration.BUser, BusinessProcess.Administration");
                theAffectedRow = (int)UserManager.DeleteUserRecord(theUserId);
                //if (theAffectedRow != 0)
                //{
                //    theUrl = "frmAdmin_UserList.aspx";
                //    Response.Redirect(theUrl);
                //}
                //IQCareMsgBox.Show("UserDelete", this);
                //theUserId = 0;
                //clear_fields();
            }
            if (theAffectedRow != 0)
            {
                theUrl = "frmAdmin_UserList.aspx";
                Response.Redirect(theUrl);
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
            UserManager = null;
        }
    }

}
