#region Import namespace
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
using Interface.Reports;
using Application.Common;
using Application.Presentation;
using Interface.Clinical;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Owc11;

#endregion

public partial class Reports_frmReportViewerARV : LogPage
{
    #region "Export Variables"
    DataTable theExcelDT;
    public DataTable theCountry;
    //string FName;
    #endregion

    #region Comments
    /////////////////////////////////////////////////////////////////////
    // Code Written By   : Ashok Kr. Gupta
    // Code Modified By  : Deepika Sain
    // Written Date      : 06th Oct 2006
    // Modification Date : 23th Nov 2007
    // Description       : Report View Form
    //
    ////////////////////////////////////////////////////////////////////
    #endregion

    # region  Variables Declaration

    private string theReportName = string.Empty;
    private string theDType = string.Empty;
    private string theReportSource = string.Empty;
    //private string theStartDate = string.Empty;
    //private string theEndDate = string.Empty;
    private string theQuarter = string.Empty;
    private string theYear = string.Empty;
    private string theReportQuery = string.Empty;
    private string theReportTitle = string.Empty;

    private int thePatientId = 0;
    private int theReportId = 0;
    private int theReportColumnCount = 0;
    private DataSet dsReportsPatient;
    private DataSet dsCDCReport;
    private DataSet dsCustomReport;
    private ReportDocument rptDocument;
   
    //IReports ReportDetails;

    #endregion

    #region "User Defined Functions"
   
    private void Init_Page()
    {

        try
        {
         
            // For other Reports like Donor , Clinical etc

            //dsReportsPatient = new DataSet();
            //dsReportsPatient.ReadXml(Server.MapPath("dsReports.xsd"));
            
            //// For CDC Report only

            //dsCDCReport = new DataSet();
            //dsCDCReport.ReadXml(Server.MapPath("dsCDCReports.xsd"));
           
            //// For Custom Report only

            //dsCustomReport = new DataSet();
            //dsCustomReport.ReadXml(Server.MapPath("dsCustomReport.xsd"));
         
            //ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");


            string theReportHeading = string.Empty;

            if (Request.QueryString["ReportId"] != null)
            {
                theReportId = Convert.ToInt32(Request.QueryString["ReportId"]);

            }
            if (Request.QueryString["ReportName"] != null)
            {
                theReportName = Request.QueryString["ReportName"];
            }

            if (Request.QueryString["StartDate"] != null)
            {
                ViewState["theStartDate"] = Request.QueryString["StartDate"];
            }
            if (Request.QueryString["EndDate"] != null)
            {
                ViewState["theEndDate"] = Request.QueryString["EndDate"];
            }
            if (Session["PatientId"] != null && Session["PatientId"].ToString() != "")
            {
                thePatientId = Convert.ToInt32(Session["PatientId"]);
            }
            else
            {
                thePatientId = 0;
            }

            // For CDC report     
            if ((Request.QueryString["QuarterId"] != null && Request.QueryString["QuarterId"].ToString() != ""))
            {
                theQuarter = Request.QueryString["QuarterId"].ToString();
            }
            if ((Request.QueryString["Year"] != null && Request.QueryString["Year"].ToString() != ""))
            {
                theYear = Request.QueryString["Year"].ToString();
            }

            //

            if (Request.QueryString["DType"] != null)
            {
                theDType = Request.QueryString["DType"];
            }

            if (theReportName != "")
            {
                if (theReportName == "UpARVPickup")
                {
                    theReportHeading = "Upcoming ARV Pickup Report";
                    theReportSource = "rptUpcomingARVPickup.rpt";

                }
                else if (theReportName == "MisARVPickup")
                {
                    theReportHeading = "Missed ARV Pickup Report";
                    theReportSource = "rptMisARVPickup.rpt";

                    //(Master.FindControl("lblRoot") as Label).Text = "Reports >>";
                    //(Master.FindControl("lblMark") as Label).Text = "»";
                    //(Master.FindControl("lblMark") as Label).Visible = false;
                    //(Master.FindControl("lblheader") as Label).Text = "Missed ARV Pickup";
                    //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Reports >> ";
                    //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Missed ARV Pickup";

                     btnExcel.Visible = true;
                }
                else if (theReportName == "NewPatients")
                {
                    theReportHeading = "New Patients Report";
                    theReportSource = "rptUpcomingARVPickup.rpt";

                }
                else if (theReportName == "PregnantFU")
                {
                    theReportHeading = "PregnantFU Report";
                    theReportSource = "rptUpcomingARVPickup.rpt";

                }
                else if (theReportName == "SinglePatientARVPickup")
                {
                    theReportHeading = "Patient ARV Pickup Report";
                    theReportSource = "rptPatientARVPickup.rpt";
                    //(Master.FindControl("lblRoot") as Label).Text = "Reports";
                    //(Master.FindControl("lblMark") as Label).Text = "»";
                    //(Master.FindControl("lblheader") as Label).Text = "Single Patient ARV Pickup";
                    //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Reports >> ";
                    //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Single Patient ARV Pickup";

                     btnExcel.Visible = true;
                }
                else if (theReportName == "AllPatientARVPickup")
                {
                    theReportHeading = "All Patient ARV Pickup Report";
                    theReportSource = "rptAllPatientARVPickup.rpt";

                    //(Master.FindControl("lblRoot") as Label).Text = "Reports";
                    //(Master.FindControl("lblMark") as Label).Text = "»";
                    //(Master.FindControl("lblheader") as Label).Text = "All Patients ARV Pickup";
                    //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Reports >> ";
                    //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "All Patients ARV Pickup";

                     btnExcel.Visible = true;

                }
                else if (theReportName == "PatientEnrollmentMonth")
                {
                    theReportHeading = "Patient Enrollment Month";
                    theReportSource = "rptPatiEnrollMonth.rpt";
                    //(Master.FindControl("lblRoot") as Label).Text = "Reports";
                    //(Master.FindControl("lblMark") as Label).Text = "»";
                    //(Master.FindControl("lblheader") as Label).Text = "Enrolled by Month";
                    //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Reports >> ";
                    //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Enrolled by Month";
                    btnExcel.Visible = false;
                }



                else if (theReportName == "ARVAdherence")
                {
                    //(Master.FindControl("lblRoot") as Label).Text = "Facility Report";
                    //(Master.FindControl("lblheader") as Label).Text = "ARV Pick up Report";
                    //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Facility Report >> ";
                    //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "ARV Pick up Report";
                    theReportHeading = "Adherence to ARV Collection Report";
                    theReportSource = "rptAdARVCollectionClients.rpt";
                }
               
                else if (theReportName == "MisARVAppointment")
                {
                    //(Master.FindControl("lblRoot") as Label).Text = "Facility Report";
                    //(Master.FindControl("lblheader") as Label).Text = "ARV Pick up Report";
                    //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Facility Report >> ";
                    //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "ARV Pick up Report";
                    theReportHeading = "Missing ARV Appointment Report";
                    theReportSource = "rptMisARVAppointClients.rpt";
                }
                else if (theReportName == "CDCReport")
                {
                    //theReportHeading = "CDC Report";
                    theReportHeading = "CDC Report Track1.0 Facility-Based Quarterly Report";
                    theReportSource = "CDCReport.rpt";
                    btnExcel.Visible = true;

                    //(Master.FindControl("lblRoot") as Label).Text = "Reports";
                    //(Master.FindControl("lblMark") as Label).Text = "»";
                    //(Master.FindControl("lblheader") as Label).Text = " Donor Reports » Track 1.0 Quarterly Report";
                    //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Reports >> ";
                    //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Donor Reports >> Track 1.0 Quarterly Report";

                }
                else if (theReportName == "Nigeria-Monthly NACA Report (IQCare data)")
                {
                    theReportHeading = "Nigeria-Monthly NACA Report ";
                    theReportSource = "rptNigeriaMonthlyNACAReport.rpt";

                    //(Master.FindControl("lblRoot") as Label).Text = "Reports";
                    //(Master.FindControl("lblMark") as Label).Text = "»";
                    //(Master.FindControl("lblheader") as Label).Text = "Donor Reports »Nigeria-Monthly NACA Report ";

                    //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Reports >> ";
                    //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Donor Reports »Nigeria-Monthly NACA Report";

                }
               

            }

            hBar.InnerText = theReportHeading;

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

        }

    }

    private void Set_PatientReports()
   {
       try
       {
           string theEnrollmentID = string.Empty;

           rptDocument = new ReportDocument();
           rptDocument.Load(Server.MapPath(theReportSource));


           //if (theReportName != "ARVAdherence" && theReportName != "PatientProfile" && theReportName != "MisARVAppointment" && theReportName != "CDCReport" && theReportName != "PatientARVPickup" && theReportName != "PCustomReport" && theReportName != "LCustomReport")

           if (theReportName != "ARVAdherence" && theReportName != "MisARVAppointment" && theReportName != "CDCReport" && theReportName != "SinglePatientARVPickup" && theReportName != "AllPatientARVPickup" && theReportName != "PCustomReport" && theReportName != "LCustomReport" && theReportName != "MisARVPickup" && theReportName != "PatientEnrollmentMonth" && theReportName != "Nigeria-Monthly NACA Report (IQCare data)")
           {
               IReports ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
               DataTable dtReportsPatient = (DataTable)ReportDetails.GetPatientDetails(thePatientId, Convert.ToDateTime(ViewState["theStartDate"]), Convert.ToDateTime(ViewState["theEndDate"])).Tables[0];
               ReportDetails = null;
               rptDocument.SetDataSource(dtReportsPatient);
               rptDocument.SetParameterValue("StartDate", ViewState["theStartDate"]);
               rptDocument.SetParameterValue("EndDate", ViewState["theEndDate"]);
           }
           else if (theReportName == "SinglePatientARVPickup")
           {
               DataTable dtDrugARVPickup = (DataTable)Session["dtDrugARVPickup"];
               ExportARVToExcel(dtDrugARVPickup);
               ViewState["FName"] = "SinglePatientARVPickup";

               rptDocument.SetDataSource(dtDrugARVPickup);
               IQCareUtils theUtil = new IQCareUtils();

               rptDocument.SetParameterValue("StartDate", theUtil.MakeDate("01-01-1900"));
               rptDocument.SetParameterValue("EndDate", theUtil.MakeDate("01-01-1900"));
               if (Session["SystemId"].ToString() == "1")
               {
                   rptDocument.SetParameterValue("Patient_FileId", "Existing Patient ClinicId:");
               }
               else
               {
                   rptDocument.SetParameterValue("Patient_FileId", "File Reference:");
               }
           }

           else if (theReportName == "AllPatientARVPickup")
           {
               DataTable dtDrugARVPickup = (DataTable)Session["dtDrugARVPickup"];
               ExportARVToExcel(dtDrugARVPickup);               
               ViewState["FName"] = "AllPatientARVPickup";               
               rptDocument.SetDataSource(dtDrugARVPickup);
           }



          //Ajay
           else if (theReportName == "MisARVPickup")
           {
               DataTable dtDrugARVPickup = (DataTable)Session["dtDrugARVPickup"];

                #region "Excel Export"
               theExcelDT = new DataTable();
               theExcelDT.Columns.Add("Enroll#", System.Type.GetType("System.String"));
               theExcelDT.Columns.Add("Name/Address", System.Type.GetType("System.String"));
               theExcelDT.Columns.Add("Phone/Emergency Phone", System.Type.GetType("System.String"));
               theExcelDT.Columns.Add("Last Regimen", System.Type.GetType("System.String"));
               theExcelDT.Columns.Add("Age", System.Type.GetType("System.Int32"));
               theExcelDT.Columns.Add("Dispensed By Date", System.Type.GetType("System.String"));
               theExcelDT.Columns.Add("ARV for (x) Days", System.Type.GetType("System.Int32"));
               theExcelDT.Columns.Add("Expected Return", System.Type.GetType("System.String"));
               theExcelDT.Columns.Add("OverDue", System.Type.GetType("System.Int32"));
               theExcelDT.Columns.Add("Termination Status", System.Type.GetType("System.String"));

               int i = 0;

               for (i = 0; i < dtDrugARVPickup.Rows.Count; i++)
               {
                   DataRow theDR = theExcelDT.NewRow();
                   theDR[0] = dtDrugARVPickup.Rows[i]["PatientEnrollmentID"];
                   theDR[1] = dtDrugARVPickup.Rows[i]["Name"].ToString() + "/" + dtDrugARVPickup.Rows[i]["Address"].ToString();
                   theDR[2] = dtDrugARVPickup.Rows[i]["Phone"].ToString() + "/" + dtDrugARVPickup.Rows[i]["EmergContactPhone"].ToString();
                   theDR[3] = dtDrugARVPickup.Rows[i]["PatientLastRegimen"].ToString();
                   theDR[4] = dtDrugARVPickup.Rows[i]["Age"];
                   theDR[5] = ((DateTime)dtDrugARVPickup.Rows[i]["DispensedByDate"]).ToString(Session["AppDateFormat"].ToString());
                   theDR[6] = dtDrugARVPickup.Rows[i]["LongestDaysLate"];
                   theDR[7] = ((DateTime)dtDrugARVPickup.Rows[i]["NextOrder"]).ToString(Session["AppDateFormat"].ToString());
                   theDR[8] = dtDrugARVPickup.Rows[i]["DaysLateOrEarly"];
                   theDR[9] = "Active";
                   theExcelDT.Rows.InsertAt(theDR, i);
               }
               ViewState["RptData"] = theExcelDT;
               ViewState["FName"] = "MisARVPickup";
               #endregion

               rptDocument.SetDataSource(dtDrugARVPickup);
               rptDocument.SetParameterValue("StartDate", ViewState["theStartDate"]);
               //rptDocument.SetParameterValue("EndDate", theEndDate);
           }
           else if (theReportName == "PatientEnrollmentMonth")
           {
               DataSet dtPatiEnrollMonth = (DataSet)Session["dtPatiEnrollMonth"];
               rptDocument.SetDataSource(dtPatiEnrollMonth);
           }
           else if (theReportName == "ARVAdherence")
           {
               IReports ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
               DataTable dtReportsClinical = (DataTable)ReportDetails.GetARVCollectionClients(thePatientId).Tables[0];
               rptDocument.SetDataSource(dtReportsClinical);
               ReportDetails = null;
           }
           else if (theReportName == "MisARVAppointment")
           {
               IQCareUtils theUtil = new IQCareUtils();
               IReports ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
               DataTable dtReportsClinical = (DataTable)ReportDetails.GetMisARVAppointClients(theDType, Convert.ToDateTime(theUtil.MakeDate(ViewState["theStartDate"].ToString()))).Tables[0];
               ReportDetails = null;
               rptDocument.SetDataSource(dtReportsClinical);
               rptDocument.SetParameterValue("DateCategory", theDType);
           }


           //else if (theReportName == "CDCReport")
           //{
               
           //    IQCareUtils theUtilsCF = new IQCareUtils();
           //    IReports ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
           //    DataSet dsCDCRep = (DataSet)ReportDetails.GetCDSReportData(Convert.ToDateTime(ViewState["theStartDate"]), Convert.ToDateTime(ViewState["theEndDate"]), Convert.ToInt16(Session["AppLocationId"]));
           //    ReportDetails = null;
           //    //dsCDCRep.WriteXmlSchema(Server.MapPath("..//XMLFiles//CDCRep.xml"));
           //    DataSet theDSXML = new DataSet();
           //    string xmlFilePath = MapPath("..\\XMLFiles\\Currency.xml");
           //    theDSXML.ReadXml(xmlFilePath);
           //    DataView theDV = new DataView(theDSXML.Tables[0]);
           //    string countryid = dsCDCRep.Tables[8].Rows[0]["countryid"].ToString();
           //    theDV.RowFilter = "id ="+countryid+"";
           //    theCountry = (DataTable)theUtilsCF.CreateTableFromDataView(theDV);
           //    //ViewState["CntryName"] = theCountry.Rows[0]["Name"].ToString();
           //    string s = theCountry.Rows[0]["Name"].ToString();
           //    string[] theCount = s.Split(new char[] {','});
           //    ViewState["CntryName"] = theCount[0].ToString();
           //    rptDocument.SetDataSource(dsCDCRep);
           //    rptDocument.SetParameterValue("countryname", ViewState["CntryName"].ToString());
           //   //string strxml = dsCDCRep.GetXml();
           //    ViewState["RptData"] = dsCDCRep;
           //} 

           else if (theReportName == "Nigeria-Monthly NACA Report (IQCare data)")
           {
               ViewState["FName"] = "Nigeria-Monthly NACA Report (IQCare data)";
               DataTable dtMonthlyNACAReportData = (DataTable)Session["dtMonthlyNACAReportData"];               
               rptDocument.SetDataSource(dtMonthlyNACAReportData);
               //ExportNACAReportToExcel(dtMonthlyNACAReportData);
               rptDocument.SetParameterValue("StartDate", ViewState["theStartDate"]);
               rptDocument.SetParameterValue("EndDate",  ViewState["theEndDate"]);
               rptDocument.SetParameterValue("CurrentDate", System.DateTime.Today);
               
              
           }
           //Deepika
           //Int32 columnCouter;
           //columnCouter = 0;
           if ((theReportName != "CDCReport") && (theReportName != "PCustomReport") && (theReportName != "LCustomReport") && (theReportName != "PatientEnrollmentMonth") 
               && (theReportName != "Nigeria-Monthly NACA Report (IQCare data)"))
           {

               theEnrollmentID = Session["AppCountryId"].ToString() + "-" + Session["AppPosID"].ToString() + "-" + Session["AppSatelliteId"].ToString() + "-";
               rptDocument.SetParameterValue("EnrollmentID", theEnrollmentID);
               crViewer.EnableParameterPrompt = false;

             
           
           }
           
           rptDocument.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath("..\\ExcelFiles\\PView.pdf"));
           ViewState["RepName"] = theReportName;
           crViewer.ReportSource = rptDocument;
           crViewer.DataBind();
           crViewer.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None;
           Session["Report"] = rptDocument;
           
           
           //-----------------------------------------
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

        }

}
    
    private void ExportARVToExcel(DataTable dtDrugARVPickup)
    {
        theExcelDT = new DataTable();
        theExcelDT.Columns.Add("Patient Name", System.Type.GetType("System.String"));
        theExcelDT.Columns.Add("Enrollment No", System.Type.GetType("System.String"));
        theExcelDT.Columns.Add("Exiting Patient ID", System.Type.GetType("System.String"));
        theExcelDT.Columns.Add("Date Dispensed", System.Type.GetType("System.String"));
        theExcelDT.Columns.Add("Longest Duration", System.Type.GetType("System.Int32"));
        theExcelDT.Columns.Add("Next Collection", System.Type.GetType("System.String"));
        theExcelDT.Columns.Add("Days", System.Type.GetType("System.Int32"));
        theExcelDT.Columns.Add("Late/Early", System.Type.GetType("System.String"));
        

        int i = 0;
        DateTime orderedByDate;
        TimeSpan ts;
        Int32 dateDiff;
        for (i = 0; i < dtDrugARVPickup.Rows.Count; i++)
        {
            DataRow theDR = theExcelDT.NewRow();

            theDR[0] = dtDrugARVPickup.Rows[i]["Name"].ToString();
            theDR[1] = dtDrugARVPickup.Rows[i]["PatientEnrollmentID"];
            theDR[2] = dtDrugARVPickup.Rows[i]["PatientClinicID"].ToString().Trim()  ;
            

            if (dtDrugARVPickup.Rows[i].IsNull("DispensedByDate") == true)
            {
                theDR[3] = dtDrugARVPickup.Rows[i]["DispensedByDate"];
            }
            else
            {
                theDR[3] = ((DateTime)dtDrugARVPickup.Rows[i]["DispensedByDate"]).ToString(Session["AppDateFormat"].ToString());
            }

            theDR[4] = dtDrugARVPickup.Rows[i]["LongestDaysLate"];

            orderedByDate = Convert.ToDateTime(dtDrugARVPickup.Rows[i]["DispensedByDate"]);
            ts = (Convert.ToDateTime(Application["AppCurrentDate"]) - orderedByDate);
            dateDiff = ts.Days;
            if (dtDrugARVPickup.Rows[i]["LongestDaysLate"].ToString() !="")
            {
                if ((dtDrugARVPickup.Rows[i].IsNull("DateArrived") == true))
                {
                    if (dtDrugARVPickup.Rows[i]["LongestDaysLate"] == System.DBNull.Value)
                    {theDR[5] = "Patient Not Due Yet";}
                    else if(dateDiff <= Convert.ToInt32(dtDrugARVPickup.Rows[i]["LongestDaysLate"])) 
                    {theDR[5] = "Patient Not Due Yet";}

                }

                else
                {
                    if (dtDrugARVPickup.Rows[i].IsNull("DateArrived") == false)
                    {
                        theDR[5] = ((DateTime)dtDrugARVPickup.Rows[i]["DateArrived"]).ToString(Session["AppDateFormat"].ToString());

                    }
                    else
                    {
                        theDR[5] = "Patient Late";
                    }
                }

            }
            else
            {
                if (dtDrugARVPickup.Rows[i].IsNull("DateArrived") == false)
                {
                    theDR[5] = ((DateTime)dtDrugARVPickup.Rows[i]["DateArrived"]).ToString(Session["AppDateFormat"].ToString());

                }
                else
                {
                    theDR[5] = "Patient Late";
                }
            }
            if (dtDrugARVPickup.Rows[i].IsNull("DaysLateOrEarly") == true)
            {
                theDR[6] = 0;
                theDR[7] = "";
            }
            else
            {
                theDR[6] = dtDrugARVPickup.Rows[i]["DaysLateOrEarly"];
                if (Convert.ToInt32(dtDrugARVPickup.Rows[i]["DaysLateOrEarly"]) < 0)
                {
                    theDR[7] = "Early";
                }
                else
                {
                    theDR[7] = "Late";
                }
            }
             theExcelDT.Rows.InsertAt(theDR, i);
        }
        ViewState["RptData"] = theExcelDT;
    }


    private int isDate(string columnValue)
    {
        DateTime dt;
        try
        {
            dt = Convert.ToDateTime(columnValue);
            return 1;
        }
        catch(Exception ex)
        {
            return 0;
        }
    }

   

    #region Patient Prorile Function for fill Datasource
    private void PatientProfile(ref DataSet dsReportsPatient)
    {
        IReports ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
        DataSet dsReportsPatientProfile = (DataSet)ReportDetails.GetPatientProfileAndHistory(thePatientId);
        ReportDetails = null;
        //=====================================================================
        // Filling data set object
        foreach (DataRow dr in dsReportsPatientProfile.Tables[0].Rows)
        {
            dsReportsPatient.Tables["dtPatientProfile"].ImportRow(dr);
        }
        foreach (DataRow dr in dsReportsPatientProfile.Tables[1].Rows)
        {
            dsReportsPatient.Tables["dtARVTreatmentHistory"].ImportRow(dr);
        }
        //==================================================================
        //History of AIDS Defining Illness

        for (int i = 0; i < dsReportsPatientProfile.Tables[2].Rows.Count; i++)
        {
            DataRow dr = dsReportsPatientProfile.Tables[2].Rows[i];
            DataRow PrevRow = null;
            DataRow drAIDSHistory;

            if (i > 0)
            {
                PrevRow = dsReportsPatientProfile.Tables[2].Rows[i - 1];
            }
            if (PrevRow == null)
            {
                drAIDSHistory = dsReportsPatient.Tables["dtAIDSDefiningHistory"].NewRow();
                drAIDSHistory["Name"] = dr["Name"];
                drAIDSHistory["DateOfDisease"] = dr["DateOfDisease"];
                if (dr["VisitDate"] != DBNull.Value)
                {
                    drAIDSHistory["VisitDate"] = Convert.ToDateTime(dr["VisitDate"]).ToShortDateString();
                    drAIDSHistory["VisitDates"] = Convert.ToDateTime(dr["VisitDate"]).ToShortDateString();
                }
                drAIDSHistory["Ptn_Pk"] = dr["Ptn_Pk"];
                dsReportsPatient.Tables["dtAIDSDefiningHistory"].Rows.Add(drAIDSHistory);
            }
            else
            {
                if (dr["Name"].ToString() == PrevRow["Name"].ToString())
                {
                    string prevDates = dsReportsPatient.Tables["dtAIDSDefiningHistory"].Rows[dsReportsPatient.Tables["dtAIDSDefiningHistory"].Rows.Count - 1]["VisitDates"].ToString().Trim();
                    dsReportsPatient.Tables["dtAIDSDefiningHistory"].Rows[dsReportsPatient.Tables["dtAIDSDefiningHistory"].Rows.Count - 1]["VisitDates"] = prevDates + ";  " + Convert.ToDateTime(dr["VisitDate"]).ToShortDateString();
                }
                else
                {
                    drAIDSHistory = dsReportsPatient.Tables["dtAIDSDefiningHistory"].NewRow();
                    drAIDSHistory["Name"] = dr["Name"];
                    drAIDSHistory["DateOfDisease"] = dr["DateOfDisease"];
                    if (dr["VisitDate"] != DBNull.Value)
                    {
                        drAIDSHistory["VisitDate"] = Convert.ToDateTime(dr["VisitDate"]).ToShortDateString();
                        drAIDSHistory["VisitDates"] = Convert.ToDateTime(dr["VisitDate"]).ToShortDateString();
                    }

                    drAIDSHistory["Ptn_Pk"] = dr["Ptn_Pk"];
                    dsReportsPatient.Tables["dtAIDSDefiningHistory"].Rows.Add(drAIDSHistory);
                }
            }
        }
        //==================================================================
        //Customizing Lab History Table According To the Report
        string PrevTest = "";
        for (int i = 0; i < dsReportsPatientProfile.Tables[3].Rows.Count; i++)
        {
            DataRow dr = dsReportsPatientProfile.Tables[3].Rows[i];
            DataRow PrevRow = null;
            DataRow drLabHistory;
            if (i > 0)
            {
                PrevRow = dsReportsPatientProfile.Tables[3].Rows[i - 1];
            }
            if (PrevRow == null)
            {
                drLabHistory = dsReportsPatient.Tables["dtLabHistory"].NewRow();
                drLabHistory["OrderedByDate"] = dr["OrderedByDate"];
                drLabHistory["Weight"] = dr["Weight"];
                if (dr["TestId"].ToString() == "1")
                {
                    drLabHistory["CD4"] = dr["TestResults"];
                }
                else
                {
                    drLabHistory["OtherLabResult"] = dr["SubTestName"] + " " + dr["TestResults"];
                    PrevTest = drLabHistory["OtherLabResult"].ToString();
                }
                dsReportsPatient.Tables["dtLabHistory"].Rows.Add(drLabHistory);
            }
            else
            {
                if (dr["OrderedByDate"].ToString() == PrevRow["OrderedByDate"].ToString())
                {
                    if (dr["TestId"].ToString() != "1")
                    {
                        string PrevTests = dsReportsPatient.Tables["dtLabHistory"].Rows[dsReportsPatient.Tables["dtLabHistory"].Rows.Count - 1]["OtherLabResult"].ToString();
                        dsReportsPatient.Tables["dtLabHistory"].Rows[dsReportsPatient.Tables["dtLabHistory"].Rows.Count - 1]["OtherLabResult"] = PrevTests + ";  " + dr["SubTestName"] + " " + dr["TestResults"];
                    }
                    else
                    {
                        dsReportsPatient.Tables["dtLabHistory"].Rows[dsReportsPatient.Tables["dtLabHistory"].Rows.Count - 1]["CD4"] = dr["TestResults"];
                    }
                }
                else
                {
                    drLabHistory = dsReportsPatient.Tables["dtLabHistory"].NewRow();
                    drLabHistory["OrderedByDate"] = dr["OrderedByDate"];
                    drLabHistory["Weight"] = dr["Weight"];
                    if (dr["TestId"].ToString() == "1")
                    {
                        drLabHistory["CD4"] = dr["TestResults"];
                    }
                    else
                    {
                        drLabHistory["OtherLabResult"] = dr["SubTestName"] + " " + dr["TestResults"];
                        PrevTest = drLabHistory["OtherLabResult"].ToString();
                    }
                    dsReportsPatient.Tables["dtLabHistory"].Rows.Add(drLabHistory);
                }
            }

        }
        //=====================================================================
    }
    #endregion
    
    
    private void CDCReport(ref DataSet dsCDCReport)
    {

        ////////////foreach (DataRow dr in dsCDCRep.Tables[0].Rows)
        ////////////{
        ////////////    dsCDCReport.Tables["dtHIVPaliative"].ImportRow(dr);
        ////////////}
        ////////////foreach (DataRow dr in dsCDCRep.Tables[1].Rows)
        ////////////{
        ////////////    dsCDCReport.Tables["dtPediatricHIVPaliative"].ImportRow(dr);
        ////////////}
        ////////////foreach (DataRow dr in dsCDCRep.Tables[2].Rows)
        ////////////{
        ////////////    dsCDCReport.Tables["dtARTCare"].ImportRow(dr);
        ////////////}
        ////////////foreach (DataRow dr in dsCDCRep.Tables[3].Rows)
        ////////////{
        ////////////    dsCDCReport.Tables["dtPediatricARTCare"].ImportRow(dr);
        ////////////}

        ////////////foreach (DataRow dr in dsCDCRep.Tables[4].Rows)
        ////////////{
        ////////////    dsCDCReport.Tables["dtChangeInCD4HalfYearly"].ImportRow(dr);
        ////////////}
        ////////////foreach (DataRow dr in dsCDCRep.Tables[5].Rows)
        ////////////{
        ////////////    dsCDCReport.Tables["dtChangeInCD4Yearly"].ImportRow(dr);
        ////////////}

        ////////////foreach (DataRow dr in dsCDCRep.Tables[6].Rows)
        ////////////{
        ////////////    dsCDCReport.Tables["dtARTCareFollowUp"].ImportRow(dr);
        ////////////}

        ////////////foreach (DataRow dr in dsCDCRep.Tables[7].Rows)
        ////////////{
        ////////////    dsCDCReport.Tables["dtPediatricARTCareFollowUp"].ImportRow(dr);
        ////////////}
        ////////////foreach (DataRow dr in dsCDCRep.Tables[8].Rows)
        ////////////{
        ////////////    dsCDCReport.Tables["dtFacilitySetUp"].ImportRow(dr);
        ////////////}

    }
    #endregion

    # region System Generated Code

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack != true)
        {
            Init_Page();
            Set_PatientReports();
        }
        
            //crViewer.ReportSource = (ReportDocument)ViewState["Report"];
            crViewer.ReportSource = (ReportDocument)Session["Report"];
              
        
    }

    #endregion

    protected void btnBack_Click(object sender, EventArgs e)
    {
        ViewState.Remove("RptData");
        ViewState.Remove("RptName");
        if (ViewState["RepName"].ToString() == "AllPatientARVPickup" || ViewState["RepName"].ToString() == "SinglePatientARVPickup" || ViewState["RepName"].ToString() == "MisARVPickup" || ViewState["RepName"].ToString() == "PatientEnrollmentMonth")
        {
            //string theUrl = string.Format("{0}", "frmReport_PatientARVPickup.aspx?Patient=");
            Response.Redirect("frmReportFacilityJump.aspx?");
        }
        //else if (ViewState["RepName"].ToString() == "CDCReport")
        //{
        //    Response.Redirect("frmReportCDC.aspx");
        //}

        else if (ViewState["RepName"].ToString() == "Nigeria-Monthly NACA Report (IQCare data)")
        {
            Response.Redirect("frmReport_NigeriaMonthlyNACAReport.aspx");
        }
        

        if (Request.QueryString["ReportId"] != null && Request.QueryString["ReportId"].ToString() != "")
        {
            if (Convert.ToInt32(Request.QueryString["ReportId"]) > 0)
            {
                Response.Redirect("frmReportCustomNew.aspx?ReportId=" + Request.QueryString["ReportId"].ToString(), true);
            }
        }
        else
        {
            Page.RegisterStartupScript("Back", "<script> history.back(); </script>");
        }

        Session.Remove("Report");
        Session.Remove("dtMonthlyNACAReportData");
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        IQCareUtils theUtils = new IQCareUtils();

        if (ViewState["RepName"].ToString() != "")
        {
            //if (ViewState["RepName"].ToString() == "CDCReport")//direct Excel Sheet Meetu Rahul 11-May-2009--
            //{
            //    //string thePath = Server.MapPath("..\\ExcelFiles\\CDC.xls");
            //    ExportCDCToExcel();
            //    //Response.Redirect("..\\ExcelFiles\\CDC.xls");
            //}
            //else
            //{
                string FName = ViewState["FName"].ToString();
                string thePath = Server.MapPath("..\\ExcelFiles\\" + FName + ".xls");
                string theTemplatePath = Server.MapPath("..\\ExcelFiles\\IQCareTemplate.xls");
                theUtils.ExportToExcel((DataTable)ViewState["RptData"], thePath, theTemplatePath);
                //theUtils.OpenExcelFile(thePath, Response);
               Response.Redirect("..\\ExcelFiles\\" + FName + ".xls");


          // }

        }

        //string script = "<script language = 'javascript' defer ='defer' id = 'excelfileopen'>\n";
        //script += "window.open('" + thePath + "');\n";
        //script += "</script>\n";
        //RegisterStartupScript("excelfileopen", script);

        //Response.Redirect(thePath);
        /////////////////////////////////////////////////////////////////////////////
    }

    //private void ExportCDCToExcel()
    //{
    //    //Get the template file from excel folder 
    //    try
    //    {
    //        Excel.SpreadsheetClass theApp = new Microsoft.Office.Interop.Owc11.SpreadsheetClass();
    //        string theFilePath = Server.MapPath("..\\ExcelFiles\\Templates\\CDC.xml");
    //        theApp.XMLURL = theFilePath;
    //        writeCellWiseInExcel(theApp);
    //        theApp.Export(Server.MapPath("..\\ExcelFiles\\CDC.xls"), Microsoft.Office.Interop.Owc11.SheetExportActionEnum.ssExportActionNone, Microsoft.Office.Interop.Owc11.SheetExportFormat.ssExportXMLSpreadsheet);
    //        Response.Redirect("..\\ExcelFiles\\CDC.xls"); 
            
    //    }
    //    catch (Exception err)
    //    {
    //        MsgBuilder theBuilder = new MsgBuilder();
    //        theBuilder.DataElements["MessageText"] = err.Message.ToString();
    //        IQCareMsgBox.Show("#C1", theBuilder, this);
    //        return;
    //    }
    //    finally
    //    {

    //    }
    //}

    //private void writeCellWiseInExcel(Excel.SpreadsheetClass theSheet)
    //{
    //    try
    //    {
    //        //For "dtHIVPaliative" table 
    //        writeInCell(theSheet, "E12", "Table", "VCountMLess15EverEnrolled");
    //        writeInCell(theSheet, "E13", "Table", "VCountMAbove15EverEnrolled");
    //        writeInCell(theSheet, "E14", "Table", "VCountFLess15EverEnrolled");
    //        writeInCell(theSheet, "E15", "Table", "VCountFAbove15EverEnrolled");

    //        writeInCell(theSheet, "G12", "Table", "VCountMLess15NewEnrollee");
    //        writeInCell(theSheet, "G13", "Table", "VCountMAbove15NewEnrollee");
    //        writeInCell(theSheet, "G14", "Table", "VCountFLess15NewEnrollee");
    //        writeInCell(theSheet, "G15", "Table", "VCountFAbove15NewEnrollee");

    //        writeInCell(theSheet, "Q12", "Table", "RHIVCareMLess15");
    //        writeInCell(theSheet, "Q13", "Table", "RHIVCareMAbove15");
    //        writeInCell(theSheet, "Q14", "Table", "RHIVCareFLess15");
    //        writeInCell(theSheet, "Q15", "Table", "RHIVCareFAbove15");

    //        writeInCell(theSheet, "Q18", "Table", "RHIVCareART");

    //        //For "PediatricHIVPalliative" table 
    //        writeInCell(theSheet, "E21", "Table1", "VCountM0To1EverEnrolled");
    //        writeInCell(theSheet, "E22", "Table1", "VCountMLess2To4EverEnrolled");
    //        writeInCell(theSheet, "E23", "Table1", "VCountMLess5To14EverEnrolled");
    //        writeInCell(theSheet, "E24", "Table1", "VCountF0To1EverEnrolled");
    //        writeInCell(theSheet, "E25", "Table1", "VCountF2To4EverEnrolled");
    //        writeInCell(theSheet, "E26", "Table1", "VCountF5To15EverEnrolled");

    //        writeInCell(theSheet, "G21", "Table1", "VCountM0To1NewEnrollee");
    //        writeInCell(theSheet, "G22", "Table1", "VCountM2To4NewEnrollee");
    //        writeInCell(theSheet, "G23", "Table1", "VCountM5To14NewEnrollee");
    //        writeInCell(theSheet, "G24", "Table1", "VCountF0To1NewEnrollee");
    //        writeInCell(theSheet, "G25", "Table1", "VCountF2To4NewEnrollee");
    //        writeInCell(theSheet, "G26", "Table1", "VCountF5To14NewEnrollee");

    //        writeInCell(theSheet, "Q21", "Table1", "RHIVCareM0To1");
    //        writeInCell(theSheet, "Q22", "Table1", "RHIVCareM2To4");
    //        writeInCell(theSheet, "Q23", "Table1", "RHIVCareM5To14");
    //        writeInCell(theSheet, "Q24", "Table1", "RHIVCareF0To1");
    //        writeInCell(theSheet, "Q25", "Table1", "RHIVCareF2To4");
    //        writeInCell(theSheet, "Q26", "Table1", "RHIVCareF5To14");

    //        //For "ARTCare" table 
    //        writeInCell(theSheet, "E31", "Table2", "ARTCareMale0to14A");
    //        writeInCell(theSheet, "E32", "Table2", "ARTCareMale15B");
    //        writeInCell(theSheet, "E33", "Table2", "ARTCareFemale0to14C");
    //        writeInCell(theSheet, "E34", "Table2", "ARTCareFemale15D");

    //        writeInCell(theSheet, "E37", "Table2", "ARTCarePregnantFemaleF");

    //        writeInCell(theSheet, "G31", "Table2", "ARTCareMale0to14G");
    //        writeInCell(theSheet, "G32", "Table2", "ARTCareMale15H");
    //        writeInCell(theSheet, "G33", "Table2", "ARTCareFemale0to14I");
    //        writeInCell(theSheet, "G34", "Table2", "ARTCareFemale15J");
    //        writeInCell(theSheet, "G37", "Table2", "ARTCarePregnantFemaleL");

    //        writeInCell(theSheet, "L31", "Table2", "ARTCareMaleNewEnroleeAA");
    //        writeInCell(theSheet, "L32", "Table2", "ARTCareMaleNewEnroleeBB");
    //        writeInCell(theSheet, "L33", "Table2", "ARTCareFemaleNewEnroleeCC");
    //        writeInCell(theSheet, "L34", "Table2", "ARTCareFemaleNewEnroleeDD");
    //        writeInCell(theSheet, "L37", "Table2", "ARTCarePregnantFemaleNewEnroleeFF");

    //        writeInCell(theSheet, "N31", "Table2", "ARTCareMaleTransferInGG");
    //        writeInCell(theSheet, "N32", "Table2", "ARTCareMaleTransferInhh");
    //        writeInCell(theSheet, "N33", "Table2", "ARTCareFemaleTransferInII");
    //        writeInCell(theSheet, "N34", "Table2", "ARTCareFemaleTransferInJJ");
    //        writeInCell(theSheet, "N37", "Table2", "ARTCarePregnantFemaleTransferInLL");

    //        writeInCell(theSheet, "Q31", "Table2", "ARTCareMaleCurrentMM");
    //        writeInCell(theSheet, "Q32", "Table2", "ARTCareMaleCurrentNN");
    //        writeInCell(theSheet, "Q33", "Table2", "ARTCareFemaleCurrentOO");
    //        writeInCell(theSheet, "Q34", "Table2", "ARTCareFemaleCurrentPP");
    //        writeInCell(theSheet, "Q37", "Table2", "ARTCarePregnantFemaleCurrentRR");
            

    //        //For "PediatricARTCare" table 
    //        writeInCell(theSheet, "E44", "Table3", "PediatricARTCareMale0To1A");
    //        writeInCell(theSheet, "E45", "Table3", "PediatricARTCareMale2To4B");
    //        writeInCell(theSheet, "E46", "Table3", "PediatricARTCareMale5To14C");
    //        writeInCell(theSheet, "E47", "Table3", "PediatricARTCareFemale0To1D");
    //        writeInCell(theSheet, "E48", "Table3", "PediatricARTCareFemale2To4E");
    //        writeInCell(theSheet, "E49", "Table3", "PediatricARTCareFemale5To14F");

    //        writeInCell(theSheet, "G44", "Table3", "PediatricARTCareMale0To1G");
    //        writeInCell(theSheet, "G45", "Table3", "PediatricARTCareMale2To4H");
    //        writeInCell(theSheet, "G46", "Table3", "PediatricARTCareMale5To14I");
    //        writeInCell(theSheet, "G47", "Table3", "PediatricARTCareFemale0To1J");
    //        writeInCell(theSheet, "G48", "Table3", "PediatricARTCareFemale2To4k");
    //        writeInCell(theSheet, "G49", "Table3", "PediatricARTCareFemale5To14L");

    //        writeInCell(theSheet, "L44", "Table3", "PediatricARTCareMaleNewEnrolee0to1S");
    //        writeInCell(theSheet, "L45", "Table3", "PediatricARTCareMaleNewEnrolee2to4t");
    //        writeInCell(theSheet, "L46", "Table3", "PediatricARTCareMaleNewEnrolee5to14u");
    //        writeInCell(theSheet, "L47", "Table3", "PediatricARTCareFemaleNewEnrolee0to1V");
    //        writeInCell(theSheet, "L48", "Table3", "PediatricARTCareFemaleNewEnrolee2to4w");
    //        writeInCell(theSheet, "L49", "Table3", "PediatricARTCareFemaleNewEnrolee5to14X");

    //        writeInCell(theSheet, "N44", "Table3", "TransfersMalesLessThan2");
    //        writeInCell(theSheet, "N45", "Table3", "TransfersMalesBetween2And4");
    //        writeInCell(theSheet, "N46", "Table3", "TransfersMalesBetween5And14");
    //        writeInCell(theSheet, "N47", "Table3", "TransfersFemalesLessThan2");
    //        writeInCell(theSheet, "N48", "Table3", "TransfersFemalesBetween2And4");
    //        writeInCell(theSheet, "N49", "Table3", "TransfersFemalesBetween5And14");

    //        writeInCell(theSheet, "Q44", "Table3", "TotalMalesLessThan2");
    //        writeInCell(theSheet, "Q45", "Table3", "TotalMalesBetween2And4");
    //        writeInCell(theSheet, "Q46", "Table3", "TotalMalesBetween5And14");
    //        writeInCell(theSheet, "Q47", "Table3", "TotalFemalesLessThan2");
    //        writeInCell(theSheet, "Q48", "Table3", "TotalFemalesBetween2And4");
    //        writeInCell(theSheet, "Q49", "Table3", "TotalFemalesBetween5And14");

    //        //For "ChangeInCD4HalfYearly" table 
    //        writeInCell(theSheet, "E61", "Table4", "cohortmonth");
    //        writeInCell(theSheet, "E62", "Table4", "CohortBaseline");
    //        writeInCell(theSheet, "E63", "Table4", "CohortCd4Count");
            

    //        writeInCell(theSheet, "G62", "Table4", "cohort6months");
    //        writeInCell(theSheet, "G63", "Table4", "CD4Cohort6months");
    //        writeInCell(theSheet, "G65", "Table4", "CD4Cohort6outof6");

    //        //For "ChangeInCD4Yearly" table 

    //        writeInCell(theSheet, "N62", "Table5", "CohortBaseline");
    //        writeInCell(theSheet, "N63", "Table5", "CohortCd4Count");

    //        writeInCell(theSheet, "Q62", "Table5", "cohort12months");
    //        writeInCell(theSheet, "Q63", "Table5", "CD4Cohort12months");
    //        writeInCell(theSheet, "Q65", "Table5", "CD4Cohort12outof12");

    //        //For "ARTCareFollowUp" table
    //        writeInCell(theSheet, "E78", "Table6", "StoppedARTMalesLess15");
    //        writeInCell(theSheet, "E79", "Table6", "StoppedARTMalesGreater15");
    //        writeInCell(theSheet, "E80", "Table6", "StoppedARTFemalesLess15");
    //        writeInCell(theSheet, "E81", "Table6", "StoppedARTFemalesGreater15");

    //        writeInCell(theSheet, "G78", "Table6", "MaleTransferredOutLess15");
    //        writeInCell(theSheet, "G79", "Table6", "MaleTransferredOutGreater15");
    //        writeInCell(theSheet, "G81", "Table6", "FemaleTransferredOutLess15");
    //        writeInCell(theSheet, "G81", "Table6", "FemaleTransferredOutGreater15");

    //        writeInCell(theSheet, "I78", "Table6", "MaleDeathLess15");
    //        writeInCell(theSheet, "I79", "Table6", "MaleDeathGreater15");
    //        writeInCell(theSheet, "I80", "Table6", "FemaleDeathLess15");
    //        writeInCell(theSheet, "I81", "Table6", "FemaleDeathGreater15");

    //        writeInCell(theSheet, "L78", "Table6", "MaleLostFollowupLess15");
    //        writeInCell(theSheet, "L79", "Table6", "MaleLostFollowupGreater15");
    //        writeInCell(theSheet, "L80", "Table6", "FemaleLostFollowupLess15");
    //        writeInCell(theSheet, "L81", "Table6", "FemaleLostFollowupGreater15");

    //        writeInCell(theSheet, "N78", "Table6", "MaleUnknownLess15");
    //        writeInCell(theSheet, "N79", "Table6", "MaleUnknownGreater15");
    //        writeInCell(theSheet, "N80", "Table6", "FemaleUnknownLess15");
    //        writeInCell(theSheet, "N81", "Table6", "FemaleUnknownGreater15");

    //        //For "PediatricARTCareFollowUp" table
    //        writeInCell(theSheet, "E85", "Table7", "StoppedARTMalesLess2");
    //        writeInCell(theSheet, "E86", "Table7", "StoppedARTMalesBetween2And4");
    //        writeInCell(theSheet, "E87", "Table7", "StoppedARTMalesBetween5And14");
    //        writeInCell(theSheet, "E88", "Table7", "StoppedARTFemalesLess2");
    //        writeInCell(theSheet, "E89", "Table7", "StoppedARTFemalesBetween2And4");
    //        writeInCell(theSheet, "E90", "Table7", "StoppedARTFemalesBetween5And14");

    //        writeInCell(theSheet, "G85", "Table7", "TransferredOutMalesLess2");
    //        writeInCell(theSheet, "G86", "Table7", "TransferredOutMalesBetween2And4");
    //        writeInCell(theSheet, "G87", "Table7", "TransferredOutMalesBetween5And14");
    //        writeInCell(theSheet, "G88", "Table7", "TransferredOutFemalesLess2");
    //        writeInCell(theSheet, "G89", "Table7", "TransferredOutFemalesBetween2And4");
    //        writeInCell(theSheet, "G90", "Table7", "TransferredOutFemalesBetween5And14");

    //        writeInCell(theSheet, "I85", "Table7", "DeathARTMalesLess2");
    //        writeInCell(theSheet, "I86", "Table7", "DeathARTMalesBetween2And4");
    //        writeInCell(theSheet, "I87", "Table7", "DeathARTMalesBetween5And14");
    //        writeInCell(theSheet, "I88", "Table7", "DeathARTFemalesLess2");
    //        writeInCell(theSheet, "I89", "Table7", "DeathARTFemalesBetween2And4");
    //        writeInCell(theSheet, "I90", "Table7", "DeathARTFemalesBetween5And14");

    //        writeInCell(theSheet, "L85", "Table7", "LostFollowupARTMalesLess2");
    //        writeInCell(theSheet, "L86", "Table7", "LostFollowupARTMalesBetween2And4");
    //        writeInCell(theSheet, "L87", "Table7", "LostFollowupARTMalesBetween5And14");
    //        writeInCell(theSheet, "L88", "Table7", "LostFollowupARTFemalesLess2");
    //        writeInCell(theSheet, "L89", "Table7", "LostFollowupARTFemalesBetween2And4");
    //        writeInCell(theSheet, "L90", "Table7", "LostFollowupARTFemalesBetween5And14");

    //        writeInCell(theSheet, "N85", "Table7", "UnknownARTMalesLess2");
    //        writeInCell(theSheet, "N86", "Table7", "UnknownARTMalesBetween2And4");
    //        writeInCell(theSheet, "N87", "Table7", "UnknownARTMalesBetween5And14");
    //        writeInCell(theSheet, "N88", "Table7", "UnknownARTFemalesLess2");
    //        writeInCell(theSheet, "N89", "Table7", "UnknownARTFemalesBetween2And4");
    //        writeInCell(theSheet, "N90", "Table7", "UnknownARTFemalesBetween5And14");


    //        writeInCell(theSheet, "D4", "", Convert.ToDateTime(((DataSet)ViewState["RptData"]).Tables["Table8"].Rows[0]["PepFarStartDate"]).ToString(Session["AppDateFormat"].ToString()));
    //        writeInCell(theSheet, "D5", "", Convert.ToDateTime(((DataSet)ViewState["RptData"]).Tables["Table8"].Rows[0]["QtrStartDate"]).ToString(Session["AppDateFormat"].ToString()));
    //        writeInCell(theSheet, "D6", "Table8", "grantee");
    //        writeInCell(theSheet, "L5", "", Convert.ToDateTime(((DataSet)ViewState["RptData"]).Tables["Table8"].Rows[0]["QtrEndDate"]).ToString(Session["AppDateFormat"].ToString()));
    //        writeInCell(theSheet, "L6", "Table8", "FacilityName");
    //        writeInCell(theSheet, "L7", "", "");
    //    }
    //    catch (Exception err)
    //    {
    //        MsgBuilder theBuilder = new MsgBuilder();
    //        theBuilder.DataElements["MessageText"] = err.Message.ToString();
    //        IQCareMsgBox.Show("#C1", theBuilder, this);
    //        return;
    //    }
    //    finally
    //    {

    //    }
    //}
    private void writeInCell(Excel.SpreadsheetClass theSheet, string cell, string tablename, string column)
    {
        try
        {
            Excel.Range theRange = theSheet.Cells.get_Range(cell, cell);
            //theRange.WrapText = true;
            if (tablename != "")
                theRange.Value2 = ((DataSet)ViewState["RptData"]).Tables[tablename].Rows[0][column].ToString();
            else if(column!="")
                theRange.Value2 = column.ToString();
            else 
                theRange.Value2 = ViewState["CntryName"].ToString();
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

        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "btnPrint_Click", "fnPrient()", true);
        Response.Redirect("..\\ExcelFiles\\PView.pdf");
        
    }
}
