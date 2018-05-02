using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Application.Presentation;
using Application.Common;

public partial class AdminForms_frmAdmin_LaboratorySelectList : LogPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
        {
            IQCareMsgBox.Show("SessionExpired", this);
            Response.Redirect("~/frmlogin.aspx",true);
        }
        if (!this.IsPostBack)
        {
            if (Session["LaboratorySelectList"] != null)
            {
                //ViewState["FID"] = Request.QueryString["Fid"].ToString();
                ViewState["FID"] = 44;
                lstselectList.DataSource = (DataTable)Session["LaboratorySelectList"];
                lstselectList.DataValueField = "selectlist";
                lstselectList.DataTextField = "selectlist";
                lstselectList.DataBind();
               // Session.Remove("LaboratorySelectList");
                AuthenticationManager Authentication = new AuthenticationManager();
                if (Authentication.HasFunctionRight(Convert.ToInt32(ViewState["FID"]), FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
                {
                    btnAdd.Enabled = false;
                }
            }
        }
        btnAdd.Attributes.Add("onClick", "return txtAdd('txtselect')");
        btnSubmit.Attributes.Add("onClick", "return listBox_hasItem('lstselectList')");
        btnRemove.Attributes.Add("onClick", "return listBox_selected('lstselectList')");
        
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if(!string.IsNullOrEmpty( txtselect.Text.Trim()))
        {
            lstselectList.Items.Add(txtselect.Text.Trim());
            txtselect.Text = "";
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
       
        DataTable theDT = CreateSelectedTable();
        DataRow theDR;
        for (int i = 0; i < lstselectList.Items.Count; i++)
        {
          
            theDR = theDT.NewRow();
            theDR["selectlist"] = lstselectList.Items[i].Text.ToString().Trim();
            theDT.Rows.Add(theDR);
        }
        Session["LaboratorySelectList"] = theDT;
        ClientScript.RegisterStartupScript(this.GetType(), "btnSubmit_Click", "<script>closeMe();</script>");

    }
    protected void btnRemove_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(lstselectList.SelectedValue))
        {
            lstselectList.Items.Remove(lstselectList.SelectedValue);
        }
    }
    private DataTable CreateSelectedTable()
    {
        DataTable theDT = new DataTable();
        theDT.Columns.Add("selectlist", System.Type.GetType("System.String")); 
        return theDT;
    }
}
