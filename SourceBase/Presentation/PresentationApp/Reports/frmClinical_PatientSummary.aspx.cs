using System;
using System.Data;

using CrystalDecisions.CrystalReports.Engine;

using Application.Presentation;
using Interface.Clinical;
using Interface.Reports;

public partial class Reports_frmClinical_PatientSummary : LogPage
{
    private ReportDocument rptDocument;
    private string theReportSource = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack != true)
        {
            setReport();
        }
        else
        {
            crViewer.ReportSource = (ReportDocument)Session["dsPatientClinicalsummary"];
            crViewer.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None;
        }
       
    }

    private void setReport()
    {
        rptDocument = new ReportDocument();

        IPatientHome ReportDetails = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome,BusinessProcess.Clinical");
        DataSet dsReportsPatientProfile = (DataSet)ReportDetails.GetPatientSummaryInformation(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["TechnicalAreaId"]));
        Session["dsPatientClinicalsummary"] = dsReportsPatientProfile;
        dsReportsPatientProfile.WriteXmlSchema(Server.MapPath("..\\XMLFiles\\PatientClinicalSummary.xml"));
        

        ReportDetails = null;

        //theReportSource = "rptPatientClinicalSummary.rpt";
        //theReportSource = "cr_PatientProfileSummary_New.rpt";
        theReportSource = "rptPatientProfile.rpt";
        rptDocument.Load(Server.MapPath(theReportSource));

        rptDocument.SetDataSource(dsReportsPatientProfile);
        rptDocument.SetParameterValue("SatelliteId", Session["AppSatelliteId"].ToString());
        crViewer.ReportSource = rptDocument;
        crViewer.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None;

    }
}