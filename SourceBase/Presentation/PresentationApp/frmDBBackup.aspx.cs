using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.IO; 
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Interface.Security;
using Application.Presentation;
using Application.Common;

public partial class frmDBBackup : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
        {
            IQCareMsgBox.Show("SessionExpired", this);
            Response.Redirect("~/frmlogin.aspx",true);
        }

        Session["PatientId"] = 0;
        //(Master.FindControl("lblheaderfacility") as Label).Visible = false;
        //(Master.FindControl("lblheader") as Label).Text = "Back up/Restore";
        //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Visible = false;
        //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Back up/Restore";
        IQWebUtils Utils = new IQWebUtils();
        Utils.HidePatientLevelheader();
        if (!IsPostBack)
        {
            if (!String.IsNullOrEmpty(Session["BackupDrive"].ToString()))
                txtbakuppath.Text = Session["BackupDrive"].ToString() + "\\IQCareDBBackup";
        }
        if (Application["BackupSetFile"] != null)
        {
            txtRestore.Text = Application["BackupSetFile"].ToString();
            ViewState["BkpPosition"] = Application["Position"].ToString();
            //chkDeidentified.Checked = false;
            Application.Remove("BackupSetFile");
            Application.Remove("Position");
        }
    }

    protected void btnBackup_Click(object sender, EventArgs e)
    {
        try
        {

            IIQCareSystem DBManager;
            DBManager = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
            //if (chkDeidentified.Checked == true)
            //{
            //    DBManager.DataBaseBackup(txtbakuppath.Text, Convert.ToInt32(Session["AppLocationId"]), 1);
            //}
            //else
            //{
            //    DBManager.DataBaseBackup(txtbakuppath.Text, Convert.ToInt32(Session["AppLocationId"]), 0);
            //}
            DBManager.DataBaseBackup(txtbakuppath.Text, Convert.ToInt32(Session["AppLocationId"]), Convert.ToInt32(chkDeidentified.Checked));
            IQCareMsgBox.Show("DataBackup", this);
        }
        catch (System.IO.DirectoryNotFoundException exd)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = exd.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
        }
        catch (Exception err)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
        }
    }
    protected void btnBrowse_Click(object sender, EventArgs e)
    {
        string theScript;
        string thePath = txtbakuppath.Text; 
        thePath = thePath.Insert(3, "\\");
        theScript = "<script language='javascript' id='BkpPopup'>\n";
        theScript += "window.open('frmBackupset.aspx?drv="+ thePath +"','BackupDevice','toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=no,resize=no,width=700,height=550,scrollbars=yes');\n";
        theScript += "</script>\n";
        Page.RegisterClientScriptBlock("BkpPopup", theScript);                          
            
    }
    protected void btnRestore_Click(object sender, EventArgs e)
    {
        try
        {
            IIQCareSystem DBManager;
            DBManager = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
            DBManager.RestoreDataBase(txtRestore.Text.Trim(),Convert.ToInt32(ViewState["BkpPosition"]));
            IQCareMsgBox.Show("DataRestore", this);
        }
        catch (Exception err)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);

        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmFacilityHome.aspx");
    }
}
