using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Schema;
using Application.Common;
using Application.Presentation;
using Interface.Clinical;
using Interface.Reports;

public partial class Reports_frmClinical_PatientSummary : LogPage
{
    private ReportDocument rptDocument;
    private string theReportSource = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        setReport();
    }

    private void setReport()
    {
        rptDocument = new ReportDocument();
        IQCareUtils theUtil = new IQCareUtils();
        IPatientHome ReportDetails = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome,BusinessProcess.Clinical");
        DataSet theDS = (DataSet)ReportDetails.GetPatientSummaryInformation(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["TechnicalAreaId"]));
       
        Session["dsPatientClinicalsummary"] = theDS;
        theDS.WriteXmlSchema(Server.MapPath("..\\XMLFiles\\PatientClinicalSummary.xml"));
        ReportDetails = null;

        theReportSource = "rptPatientClinicalSummary.rpt";
        rptDocument.Load(Server.MapPath(theReportSource));
        rptDocument.SetDataSource(theDS);
        crViewer.ReportSource = rptDocument;
        crViewer.DataBind();
        crViewer.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None;
    }
}