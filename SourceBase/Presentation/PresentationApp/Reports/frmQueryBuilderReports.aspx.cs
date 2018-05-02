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
using System.Data.SqlClient;
using Application.Common;
using Application.Presentation;
using Interface.Reports;
using System.IO;



public partial class Reports_frmQueryBuilderReports : LogPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["PatientId"] = 0;
        //(Master.FindControl("lblRoot") as Label).Text = "Reports >>";
        //(Master.FindControl("lblMark") as Label).Visible = false;
        //(Master.FindControl("lblheader") as Label).Text = "QueryBuilder Reports";
        //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Reports >> ";
        //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "QueryBuilder Reports";
        IQWebUtils Utils = new IQWebUtils();
        Utils.HidePatientLevelheader();
        if (!IsPostBack)
        {
            IReports theQBuilderReports = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports, BusinessProcess.Reports");
            DataTable theDT = theQBuilderReports.GetReportsCategory();
            BindFunctions theBind = new BindFunctions();
            theBind.BindCombo(ddlCategory, theDT, "CategoryName", "CategoryId");
        }
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCategory.SelectedValue == "0")
        {
            dyanamicRadiobutton.Visible = false;
            lblReportName.Visible = false;
            IQCareMsgBox.Show("QueryBuilder", this);
        }
        else
        {
            AddDynamicRadioButton();
            dyanamicRadiobutton.Visible = true;
            lblReportName.Visible = true;

        }
      
    }
  private void AddDynamicRadioButton()
    {
        IReports theQBuilderReports = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports, BusinessProcess.Reports");
        DataTable theDT = theQBuilderReports.GetCustomReports(Convert.ToInt32(ddlCategory.SelectedValue));
        rdButtonList.Items.Clear();
        foreach (DataRow theDR in theDT.Rows)
      {
          rdButtonList.Items.Add(new ListItem(theDR["ReportName"].ToString(), theDR["ReportQuery"].ToString()));
   
      } 
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
       if(rdButtonList.SelectedItem !=null)
       {

                    string theQuery = rdButtonList.SelectedItem.Value.ToString();
                    IReports theQBuilderReports = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports, BusinessProcess.Reports");
                    DataTable theDT = theQBuilderReports.ReturnQueryResult(theQuery).Tables[0];
                    if (theDT.Rows.Count > 0)
                    {
                        IQWebUtils theUtils = new IQWebUtils();
                        string ReportName1 = rdButtonList.SelectedItem.Text;
                        string thePath = Server.MapPath("..\\ExcelFiles\\" + ReportName1 + ".xls");
                        string theTemplatePath = Server.MapPath("..\\ExcelFiles\\IQCareTemplate.xls");
                        theUtils.ExporttoExcel(theDT, Response);
                    }
       }
        else
       {
           IQCareMsgBox.Show("SelectRadioButton", this);
       }
         

        }
        
    
  
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/frmFacilityHome.aspx");
    }

}





   