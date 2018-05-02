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
// Description       : Disease Add/Edit/Delete
//
/// /////////////////////////////////////////////////////////////////
    
public partial class Add_Edit_Disease : LogPage
{
    int DiseaseId;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
        {
            IQCareMsgBox.Show("SessionExpired", this);
            Response.Redirect("~/frmlogin.aspx",true);
        }
        //(Master.FindControl("lblheader") as Label).Text = "Customise List";
        //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Visible = false;
        //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Customise List";
        lblH2.Text = Request.QueryString["name"];
        
        if (lblH2.Text == "Add")
        {
            ddStatus.Visible = false;
            lblStatus.Visible = false;
            lblH2.Text = "Add Disease";
        }


        else if (lblH2.Text == "Edit")
        {
            lblH2.Text = "Edit Disease";
        }
        
        IDiseases DiseaseManager;
            if (!IsPostBack)
            {
                if (Request.QueryString["name"] == "Edit")
                {

                    try
                    {

                        int DiseaseId;
                        DiseaseId = Convert.ToInt32(Request.QueryString["DiseaseId"]);

                        DiseaseManager = (IDiseases)ObjectFactory.CreateInstance("BusinessProcess.Administration.BDiseases, BusinessProcess.Administration");
                        DataSet theDS = DiseaseManager.GetDiseasesByID(DiseaseId);
                        this.txtDiseaseName.Text = theDS.Tables[0].Rows[0]["DiseaseName"].ToString();
                        if (theDS.Tables[0].Rows[0]["DeleteFlag"].ToString() == "True")
                        {
                            this.ddStatus.SelectedValue = "1";
                        }
                        else
                        {
                            this.ddStatus.SelectedValue = "0";
                        }
                        this.txtSeq.Text = theDS.Tables[0].Rows[0]["Sequence"].ToString();
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
                            DiseaseManager = null;
                        }       
                
                }
             }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string url;
        IDiseases DiseaseManager;
        try
        {
            DiseaseManager = (IDiseases)ObjectFactory.CreateInstance("BusinessProcess.Administration.BDiseases, BusinessProcess.Administration");
            DataSet theDS = DiseaseManager.DeleteDisease(Convert.ToInt32(DiseaseId.ToString()));
            url = "frmAdmin_Diseaselist.aspx";
            Response.Redirect(url);
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
            DiseaseManager = null;
        }       
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string url = "frmAdmin_Diseaselist.aspx";
        Response.Redirect(url);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (FieldValidation() == false)
        {
            return;
        }
         IDiseases DiseaseManager;
        
        try
        {

            if (Request.QueryString["name"] == "Add")
            {

                DiseaseManager = (IDiseases)ObjectFactory.CreateInstance("BusinessProcess.Administration.BDiseases, BusinessProcess.Administration");
                int DiseaseId = DiseaseManager.SaveNewDisease(txtDiseaseName.Text, 1,Convert.ToInt32(txtSeq.Text));
                if (DiseaseId == 0)
                {
                    IQCareMsgBox.Show("DiseaseExists", this);
                    return;
                }
                else
                {
                    IQCareMsgBox.Show("DiseaseSave", this);
                   clear_fields();
                }
            }
            else if (Request.QueryString["name"] == "Edit")
            {
                DiseaseManager = (IDiseases)ObjectFactory.CreateInstance("BusinessProcess.Administration.BDiseases, BusinessProcess.Administration");
                int DiseaseId = DiseaseManager.UpdateDisease(Convert.ToInt32(Request.QueryString["diseaseid"]), txtDiseaseName.Text.ToUpper(), 1, Convert.ToInt32(this.ddStatus.SelectedValue), Convert.ToInt32(txtSeq.Text));
                IQCareMsgBox.Show("DiseaseUpdate", this);
                
            }
        }
        catch (Exception err)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", this);
            return;
        }
        finally
        {
            DiseaseManager = null;
        }

    }
 
#region User functions
private void clear_fields()
    {
        txtDiseaseName.Text = "";
        txtSeq.Text = "";
        txtDiseaseName.Focus();
        ddStatus.ClearSelection();
        
    }

    private Boolean FieldValidation()
    {
        if (txtDiseaseName.Text.Trim() == "")
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "Disease Name";
            IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
            txtDiseaseName.Focus();
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
