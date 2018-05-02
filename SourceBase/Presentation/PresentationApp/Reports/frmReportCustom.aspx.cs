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
using System.Data.SqlClient;
using System.IO;
using Application.Common;
using Interface.Reports;
using Interface.Security;
using Application.Presentation;
using System.Threading;


public partial class Reports_frmReportCustom : LogPage
{


    protected void Page_Load(object sender, EventArgs e)
    {
        Session["PatientId"] = 0;
        //(Master.FindControl("lblRoot") as Label).Text = "Reports >>";
        ////(Master.FindControl("lblMark") as Label).Text = "»";
        //(Master.FindControl("lblMark") as Label).Visible = false; 
        //(Master.FindControl("lblheader") as Label).Text = "Custom Reports";
        //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Reports >> ";
        //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Custom Reports";
        IQWebUtils Utils = new IQWebUtils();
        Utils.HidePatientLevelheader();
        this.btnExportReport.Attributes.Add("onclick", "javascript:return CheckReport('" + this.ddTitle.ClientID + "')");
        this.btnRun.Attributes.Add("onclick", "javascript:return CheckReport('" + this.ddTitle.ClientID + "')");
        this.btnEdit.Attributes.Add("onclick", "javascript:return CheckReport('" + this.ddTitle.ClientID + "')");
        IQCareMsgBox.ShowConfirm("CustomRepRefresh", btnRefresh);

        //RTyagi..20-Feb-07.
        if (Page.IsPostBack != true)
        {
            AuthenticationManager Authentiaction = new AuthenticationManager();
            if (Authentiaction.HasFunctionRight(ApplicationAccess.CustomReports, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
            {
                btnEdit.Enabled = false;
                btnRun.Enabled = false;
                btnExportReport.Enabled = false;
                btnEditImport.Enabled = false;
            }
            //Thread theRptTable = new Thread(RefreshReportingTables);
            //theRptTable.Priority = ThreadPriority.Highest; 
            //theRptTable.Start();

            DataSet dsCategory;
            IReports CustomReport;
            CustomReport = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
            dsCategory = CustomReport.GetAllCategory();
            BindFunctions theBind = new BindFunctions();
            theBind.BindCombo(ddCategory, dsCategory.Tables[0], "CategoryName", "CategoryId");
            //ddCategory.Items.Insert(0, new ListItem("Select Category", ""));
            ddTitle.Items.Insert(0, new ListItem("Select Title", ""));

            //////
            string str = string.Empty;
            if (Request.QueryString["r"] != null)
            {
                str = Request.QueryString["r"].ToString();
            }
            if (str != "no")
            {

                string thescript = "<script language = 'javascript' defer ='defer' id = 'refreshCache'>\n";
                thescript += "document.getElementById('" + btnRefresh.ClientID + "').click();\n";
                thescript += "</script>\n";
                RegisterStartupScript("refreshCache", thescript);
            }
            //return;
        }

    }


    //protected void lnkNewReport_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("frmReportCustomNew.aspx", true);
    //}

    protected void btnExportReport_Click(object sender, EventArgs e)
    {
        if (this.ddTitle.SelectedValue.ToString() != "")
        {
            DataSet dsExistingReport, dsCustomReport;
            IReports CustomReport;
            CustomReport = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
            dsExistingReport = CustomReport.GetCustomReportData(Convert.ToInt32(this.ddTitle.SelectedValue));
            Stream stream = new MemoryStream();
            dsExistingReport.WriteXml(stream, XmlWriteMode.WriteSchema);
            byte[] Buffer;

            Buffer = new byte[stream.Length];
            stream.Position = 0;
            stream.Read(Buffer, 0, (int)stream.Length);
            stream.Close();

            Response.Clear();

            Response.ContentType = "application/xml";
            Response.AddHeader("content-disposition", "attachment; filename=Report.xml");
            Response.BinaryWrite(Buffer);

            Response.End();

            //DataTable dtReportField = null;
            //DataTable dtReportFilter = null;

            //if (dsExistingReport.Tables.Count > 1 && dsExistingReport.Tables[1] != null)
            //{
            //    dtReportField = dsExistingReport.Tables[1];
            //}
            //if (dsExistingReport.Tables.Count > 2 && dsExistingReport.Tables[2] != null)
            //{
            //    dtReportFilter = dsExistingReport.Tables[2];
            //}

            //DataRow drMstReport;
            //dsCustomReport = CreateDataSet();
            //drMstReport = dsCustomReport.Tables["dtMstReport"].NewRow();

            //drMstReport["ReportName"] = dsExistingReport.Tables[0].Rows[0]["ReportName"].ToString();
            //drMstReport["Description"] = dsExistingReport.Tables[0].Rows[0]["Description"].ToString();
            //drMstReport["Condition"] = dsExistingReport.Tables[0].Rows[0]["Condition"].ToString();
            //drMstReport["CategoryId"] = dsExistingReport.Tables[0].Rows[0]["CategoryId"].ToString();
            //drMstReport["RptType"] = dsExistingReport.Tables[0].Rows[0]["RptType"].ToString();
            //dsCustomReport.Tables["dtMstReport"].Rows.Add(drMstReport);

            //DataRow drReportFields;
            //DataRow drReportFilter;
            //DataView dvFilter;
            //if (drMstReport["RptType"].ToString() == "DYNAMIC")
            //{
            //    if (dtReportField != null)
            //    {
            //        foreach (DataRow dr in dtReportField.Rows)
            //        {
            //            drReportFields = dsCustomReport.Tables["dtlReportFields"].NewRow();

            //            drReportFields["GroupId"] = Convert.ToInt32(dr["GroupId"]);
            //            drReportFields["FieldId"] = Convert.ToInt32(dr["FieldId"]);

            //            drReportFields["FieldLabel"] = dr["FieldName"].ToString();
            //            drReportFields["AggregateFunction"] = dr["AggregateFunction"].ToString();
            //            drReportFields["IsDisplay"] = dr["IsDisplay"];
            //            drReportFields["Sequence"] = dr["Sequence"];
            //            drReportFields["Sort"] = dr["Sort"];
            //            drReportFields["ViewName"] = dr["ViewName"];
            //            dsCustomReport.Tables["dtlReportFields"].Rows.Add(drReportFields);
            //            //===============================================================================
            //            dvFilter = new DataView(dtReportFilter, "ReportFieldId=" + dr["ReportFieldId"], "Sequence", DataViewRowState.CurrentRows);
            //            if (dvFilter.Count > 0)
            //            {
            //                for (int i = 0; i < dvFilter.Count; i++)
            //                {
            //                    drReportFilter = dsCustomReport.Tables["dtlReportFilter"].NewRow();
            //                    drReportFilter["LinkFieldId"] = Convert.ToInt32(dr["FieldId"]); //Convert.ToInt32(dr["Sequence"]);
            //                    drReportFilter["Operator"] = dvFilter[i]["Operator"].ToString();
            //                    drReportFilter["FilterValue"] = dvFilter[i]["FilterValue"].ToString();
            //                    drReportFilter["AndOr"] = dvFilter[i]["AndOr"].ToString();
            //                    drReportFilter["Sequence"] = dvFilter[i]["Sequence"].ToString();

            //                    drReportFilter["Operator1"] = dvFilter[i]["Operator1"].ToString();
            //                    drReportFilter["FilterValue1"] = dvFilter[i]["FilterValue1"].ToString();
            //                    drReportFilter["PanelId"] = Convert.ToInt32(dr["Sequence"]);
            //                    drReportFilter["AndOr1"] = dvFilter[i]["AndOr1"].ToString(); // labtest changed
            //                    dsCustomReport.Tables["dtlReportFilter"].Rows.Add(drReportFilter);
            //                }
            //            }
            //            //===============================================================================
            //        }
            //    }
            //}
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (this.ddTitle.SelectedValue != "")
        {
            Response.Redirect("frmReportCustomNew.aspx?ReportId=" + this.ddTitle.SelectedValue.ToString(), true);
        }
    }

    protected void ddCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddCategory.SelectedIndex == 0)
        {
            ddTitle.Items.Clear();
            ddTitle.Items.Insert(0, new ListItem("Select Title", ""));
        }
        else if ((ddCategory.SelectedValue != "") && (ddCategory.SelectedIndex != 0))
        {
            DataSet dsReports;
            IReports CustomReport;
            CustomReport = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
            dsReports = CustomReport.GetReportList(Convert.ToInt32(ddCategory.SelectedValue));
            ddTitle.DataSource = dsReports.Tables[0];
            ddTitle.DataTextField = "ReportName";
            ddTitle.DataValueField = "ReportId";
            ddTitle.DataBind();
            ddTitle.Items.Insert(0, new ListItem("Select Title", ""));
        }
    }

    protected void btnEditImport_Click(object sender, EventArgs e)
    {
        try
        {
            if (inptReport.HasFile)
            {
                //inptReport.SaveAs(@"C:\" + inptReport.FileName);
                //if (File.Exists(inptReport.PostedFile.InputStream.ToString()))
                //{
                    
                    //DataSet theDS = CreateDataSet();
                    DataSet theDS = new DataSet();
                    theDS.ReadXml(inptReport.PostedFile.InputStream);

                    //theDS.ReadXml(@"C:\" + inptReport.FileName);()
                    Session["CustomReportDS"] = theDS;
                    Response.Redirect("frmReportCustomNew.aspx?Import=Yes", true);
                    //IReports CustomReport;
                    //CustomReport = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
                    //int theReportId;
                    //theReportId = CustomReport.SaveCustomReport(theDS, 0);
                    //29Jul08
                    //Response.Redirect("frmReportCustomNew.aspx?ReportId=" + theReportId.ToString() + "&ReportImpMode=RIEdit",false);
                    //Response.Redirect("frmReportCustomNew.aspx?ReportId=" + theReportId.ToString());
                    //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "btnEditImport_Click", "fnRedirect('" + theReportId.ToString() + "')", true);
                //}
            }
        }
        catch (Exception err)
        {
            //29Jul08
            //IQCareMsgBox.Show("InvalidFile", this);
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
        }
    }

    private DataSet CreateDataSet()
    {
        DataSet dsCustomReports = new DataSet();

        DataTable dtMstReport = new DataTable("dtMstReport");
        DataTable dtlReportFields = new DataTable("dtlReportFields");
        DataTable dtlReportFilter = new DataTable("dtlReportFilter");

        //============= adding columns to MstReport DataTable ================
        dtMstReport.Columns.Add(new DataColumn("ColumnNo", typeof(int)));
        dtMstReport.Columns.Add(new DataColumn("CategoryId", typeof(int)));
        dtMstReport.Columns.Add(new DataColumn("CategoryName", typeof(string)));
        dtMstReport.Columns.Add(new DataColumn("ReportName", typeof(string)));
        dtMstReport.Columns.Add(new DataColumn("Description", typeof(string)));
        dtMstReport.Columns.Add(new DataColumn("Condition", typeof(string)));
        dtMstReport.Columns.Add(new DataColumn("ReportId", typeof(int)));
        dtMstReport.Columns.Add(new DataColumn("RptType", typeof(string)));

        //============= adding columns to Report's Field DataTable ================
        dtlReportFields.Columns.Add(new DataColumn("GroupId", typeof(int)));
        dtlReportFields.Columns.Add(new DataColumn("FieldId", typeof(int)));
        dtlReportFields.Columns.Add(new DataColumn("FieldLabel", typeof(string)));
        dtlReportFields.Columns.Add(new DataColumn("AggregateFunction", typeof(string)));
        dtlReportFields.Columns.Add(new DataColumn("IsDisplay", typeof(bool)));
        dtlReportFields.Columns.Add(new DataColumn("Sequence", typeof(int)));
        dtlReportFields.Columns.Add(new DataColumn("Sort", typeof(string)));
        dtlReportFields.Columns.Add(new DataColumn("ViewName", typeof(string)));

        //============= adding columns to Report's Filter DataTable ================
        dtlReportFilter.Columns.Add(new DataColumn("LinkFieldId", typeof(int)));
        dtlReportFilter.Columns.Add(new DataColumn("Operator", typeof(string)));
        dtlReportFilter.Columns.Add(new DataColumn("FilterValue", typeof(string)));
        dtlReportFilter.Columns.Add(new DataColumn("AndOr", typeof(string)));
        dtlReportFilter.Columns.Add(new DataColumn("Sequence", typeof(int)));

        dtlReportFilter.Columns.Add(new DataColumn("Operator1", typeof(string)));
        dtlReportFilter.Columns.Add(new DataColumn("FilterValue1", typeof(string)));
        dtlReportFilter.Columns.Add(new DataColumn("PanelId", typeof(int)));
        dtlReportFilter.Columns.Add(new DataColumn("AndOr1", typeof(string)));


        dsCustomReports.Tables.Add(dtMstReport);
        dsCustomReports.Tables.Add(dtlReportFields);
        dsCustomReports.Tables.Add(dtlReportFilter);

        return dsCustomReports;
    }

    protected void btnRun_Click(object sender, EventArgs e)
    {
        if (this.ddTitle.SelectedValue != "")
        {
            IReports CustomReport = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
            DataSet DSCustomReport = CustomReport.GetCustomReport(Convert.ToInt32(ddTitle.SelectedValue));
            string theRptName = "";
            if (Convert.ToInt32(DSCustomReport.Tables[4].Rows[0][0]) > 0 && Convert.ToInt32(DSCustomReport.Tables[4].Rows[0][0]) < 8)
            {
                theRptName = "rptPotrait";
            }
            else
            {
                theRptName = "rptLandscape";
            }
            Session.Add("ReportData", DSCustomReport);
            Response.Redirect("frmReportViewer.aspx?ReportId=" + ddTitle.SelectedValue.ToString() + "&rptType=" + theRptName);
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("../frmFacilityHome.aspx", true);
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        IIQCareSystem theRptTables = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem,BusinessProcess.Security");
        theRptTables.RefreshReportingTables(1);

    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmReportCustomNew.aspx", true);
    }
}
