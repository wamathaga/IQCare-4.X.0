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
using System.Text;
using Interface.Administration;
using Application.Common;
using Application.Presentation;
using Interface.Security;


public partial class AdminForms_frmAdmin_ChangePassword : LogPage

{
    private Boolean FieldValidation()
    {
        //Visit Date Validations
        if (txtNewPassword.Text.Trim() == "")
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "New Password";
            IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
            return false;
        }
        if (txtConfirmpassword.Text.Trim() == "")
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "Confirm Password";
            IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
            return false;
        }
        //if (txtNewPassword.Text != txtConfirmpassword.Text)
        //{
        //    IQCareMsgBox.Show("NewConfirmPasswordNotMatch", this);
        //    return false;
        //}
        return true;
    }

    private Boolean GetPassWord_Data()
    {
        IPassword PasswordMgr;
        try
        {

            PasswordMgr = (IPassword)ObjectFactory.CreateInstance("BusinessProcess.Administration.BPassword, BusinessProcess.Administration");
            DataSet DSGetPassData = PasswordMgr.GetUserData(Convert.ToInt32(Session["AppUserId"].ToString()));
            Utility theUtil = new Utility();
            string thePass = theUtil.Decrypt(DSGetPassData.Tables[0].Rows[0]["Password"].ToString());
            if (txtExisPassword.Text.Trim() != thePass)
            {
                IQCareMsgBox.Show("ExistPasswordNotMatch", this);
                return false;
            }

            if (Session["MandatoryChange"] != null)
            {
                if ((Session["MandatoryChange"].ToString() == "1") ||(Session["MandatoryChange"].ToString() == "2"))
                {
                    if (txtNewPassword.Text == thePass)
                    {
                        IQCareMsgBox.Show("PasswordMatchSame", this);
                        return false;
                    }
                }
            }
            if (txtNewPassword.Text == txtConfirmpassword.Text)
            {
                PasswordMgr.UpdatePassword(Convert.ToInt32(Session["AppUserId"].ToString()), txtNewPassword.Text);
            }
            else
            {
                IQCareMsgBox.Show("NewConfirmPasswordNotMatch", this);
                return false;
            }

        }
        catch (Exception err)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
        }
        finally
        {
            PasswordMgr = null;
        }
        return true;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
        {
            IQCareMsgBox.Show("SessionExpired", this);
            Response.Redirect("~/frmlogin.aspx",true);
        }
        //(Master.FindControl("lblheaderfacility") as Label).Text = "";
        //(Master.FindControl("lblMark") as Label).Text = "";
        //(Master.FindControl("lblheader") as Label).Text = "Password";
        //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Visible = false;
        //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Password";
        DataSet theXMLDS = new DataSet();
        theXMLDS.ReadXml(Server.MapPath("..\\XMLFiles\\AllMasters.con"));
        DataView theDV = new DataView(theXMLDS.Tables["Mst_Facility"]);
        theDV.RowFilter = "FacilityId=" + Convert.ToInt32(Session["AppLocationId"]);
        if (theDV[0]["StrongPassFlag"] != DBNull.Value)
        {
            if (Convert.ToInt32(theDV[0]["StrongPassFlag"]) == 1)
            {
                string[] Username = Session["AppUserName"].ToString().Split(' ');
                txtNewPassword.Attributes.Add("onblur", "if (validstrngpwd('" + txtNewPassword.ClientID + "','" + Username[0].ToString() + "')){\n NotifyMessage( document.getElementById('unique_id').innerHTML);\n document.getElementById('" + txtNewPassword.ClientID + "').value='';\n}");
            }
        }
        if (!IsPostBack)
        {
            ViewState["url"] = Application["PrvFrm"].ToString();
            Application.Remove("PrvFrm");
            txtUserName.Text = Session["AppUserName"].ToString();
            txtUserName.Enabled = false;
        }
        
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (FieldValidation() == false)
        {
            return;
        }

        if (GetPassWord_Data() == false)
        {
            return;
        }
        else
        {
            IQCareMsgBox.Show("UpdateMessage", this);
            
            Response.Redirect("~/frmlogin.aspx",true);

        }

    }
    protected void btnExit_ServerClick(object sender, EventArgs e)
    {
        if (Session["MandatoryChange"] != null)
        {
            if (Session["MandatoryChange"].ToString() == "1")
            {
                Response.Redirect(ViewState["url"].ToString());
            }
            else
                Response.Redirect("../frmFacilityHome.aspx");
        }
        else      
            Response.Redirect("../frmFacilityHome.aspx");
        
    }
}
