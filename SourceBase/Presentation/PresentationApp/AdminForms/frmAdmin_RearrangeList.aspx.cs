#region Namespace

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
using Application.Presentation;
using Application.Common;

#endregion 

public partial class AdminForms_frmAdmin_RearrangeList : LogPage
{
    DataTable theDT = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
        {
            IQCareMsgBox.Show("SessionExpired", this);
            Response.Redirect("~/frmlogin.aspx",true);
        }
        if (!IsPostBack)
        {
            FillDropDownFeatures();
        }
    }

    protected void FillDropDownFeatures()
    {
        ICustomFields CustomFields;
        try
        {
            DataTable theDTModule = (DataTable)Session["AppModule"];
            string theModList = "";
            foreach (DataRow theDR in theDTModule.Rows)
            {
                if (theModList == "")
                    theModList = theDR["ModuleId"].ToString();
                else
                    theModList = theModList + "," + theDR["ModuleId"].ToString();


            }

            if (theModList == "1,2")
            {
                theModList = "0";
            }
            else if (theModList == "1")
            {

                theModList = "1";

            }
            else
            {
                theModList = "2";
            }


            CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
            DataSet theDS = CustomFields.GetFeatures(Convert.ToInt32(Session["SystemId"]), theModList);
            BindFunctions BindManager = new BindFunctions();
            BindManager.BindCombo(ddlFormName, theDS.Tables[0], "FeatureName", "FeatureID");



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
            CustomFields = null;
        }
    }

    protected void ddlFormName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ICustomFields CustomFields;
        CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
        DataSet theDS = CustomFields.GetRearrangeCustomFields(Convert.ToInt32(Session["SystemId"].ToString()));
        DataView theDSView = new DataView();
        theDSView.Table = theDS.Tables[0];
        string[] strValue = ddlFormName.SelectedItem.Text.ToString().Split('-');
        theDSView.RowFilter = "FeatureName=" + "'" + strValue[0] + "'";
        theDT = theDSView.ToTable();
        BindFunctions BindManager = new BindFunctions();
        BindManager.BindList(lstRearrangeListItems, theDT, "Label", "CustomFieldId");
        ViewState["tempTable"] = theDT;
    }

    protected void btnDown_Click(object sender, EventArgs e)
    {

        int i = this.lstRearrangeListItems.SelectedIndex;
        object o = this.lstRearrangeListItems.SelectedItem;
        theDT = (DataTable)ViewState["tempTable"];
        if (i < this.lstRearrangeListItems.Items.Count - 1 && i >= 0)
        {
            //column value swapped Down
            theDT.Rows[i]["Label"] = theDT.Rows[i + 1]["Label"];
            theDT.Rows[i + 1]["Label"] = lstRearrangeListItems.Items[i];
            theDT.Rows[i]["CustomFieldID"] = theDT.Rows[i+1]["CustomFieldID"];
            theDT.Rows[i + 1]["CustomFieldID"] = lstRearrangeListItems.SelectedValue;
            ViewState["tempTable"] = theDT;
            lstRearrangeListItems.Items.Clear();
            BindFunctions BindManager = new BindFunctions();
            BindManager.BindList(lstRearrangeListItems, theDT, "Label", "CustomFieldId");
            lstRearrangeListItems.SelectedIndex = i + 1;
        }

    }
    
    protected void btnUp_Click(object sender, EventArgs e)
    {

        int i = this.lstRearrangeListItems.SelectedIndex;
        object o = this.lstRearrangeListItems.SelectedItem;
        theDT = (DataTable)ViewState["tempTable"];
        if (i > 0)
        {
            //column value swapped up
            theDT.Rows[i]["Label"] = theDT.Rows[i - 1]["Label"];
            theDT.Rows[i - 1]["Label"] = lstRearrangeListItems.Items[i];
            theDT.Rows[i]["CustomFieldID"] = theDT.Rows[i-1]["CustomFieldID"];
            theDT.Rows[i - 1]["CustomFieldID"] = lstRearrangeListItems.SelectedValue; ;
            ViewState["tempTable"] = theDT;
            lstRearrangeListItems.Items.Clear();
            BindFunctions BindManager = new BindFunctions();
            BindManager.BindList(lstRearrangeListItems, theDT, "Label", "CustomFieldId");
            lstRearrangeListItems.SelectedIndex = i-1;
        }



    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
       
        ICustomFields CustomFields;
        CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
        int CF = CustomFields.RearrangeCustomFields((DataTable)ViewState["tempTable"], Convert.ToInt32(Session["SystemId"].ToString()));
        string theScript;
        theScript = "<script language='javascript' id='DrgPopup'>\n";
        theScript += "window.close();\n";
        theScript += "</script>\n";
        RegisterStartupScript("DrgPopup", theScript);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string theScript;
        theScript = "<script language='javascript' id='DrgPopup'>\n";
        theScript += "window.close();\n";
        theScript += "</script>\n";
        RegisterStartupScript("DrgPopup", theScript);
    }
}
