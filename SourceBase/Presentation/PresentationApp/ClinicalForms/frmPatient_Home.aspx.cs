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
using System.Drawing;
using System.Drawing.Imaging;
using ChartDirector;
using Application.Presentation;
using Application.Common;
using Interface.Clinical;
using Interface.Security;
using Interface.Administration;
using Interface.Reports;
using Graph = Microsoft.Office.Interop.Owc11;
using System.Text;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;


public partial class frmPatient_Home : BasePage
{

    #region "Variable Declaration"
    double theGraphLowCD4, theGraphRecentCD4, theGraphHb = 0.0, theGraphHct = 0.0, theGraphAST = 0.0, theGraphCr = 0.0;
    string month1, month2, month3, month4, month5, month6, stat = "";
    System.Data.DataSet theFacilityDS;
    //AjaxControlToolkit.TabContainer tbcDynamic; 
    DataView dvModule = new DataView();
    DataTable dtModule = new DataTable();
    string theCommandName = "";
    #endregion

    #region "UserFunctions"
    private void Init_Form()
    {
        if (object.Equals(Session["TechnicalAreaId"], null))
        {
            IQCareMsgBox.Show("Please select the Service / Technical Area to proceed.", "!", "", this);
            Response.Redirect("~/frmAddTechnicalArea.aspx", true);
        }
        if (Request.QueryString["Func"] == "Delete")
        {
            FormDelete();
        }

        DateTime theTmpDate;
        string strPatientEnrollmentId = string.Empty;
        IPatientHome PatientManager;
        PatientManager = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
        System.Data.DataSet theDS = PatientManager.GetPatientDetails(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["SystemId"]), Convert.ToInt32(Session["TechnicalAreaId"]));
        PatientManager = null;

        #region "Patient Data"
        Session["PatientInformation"] = theDS.Tables[0];
        #endregion
        if (theDS.Tables[41].Rows.Count > 0)
        {
            //DataView theDV = new DataView(theDS.Tables[3]);
            dtModule = theDS.Tables[41];

            DataView dv = theDS.Tables[41].DefaultView;
            dv.RowFilter = "ModuleID=" + Convert.ToInt32(Session["TechnicalAreaId"]);
            DataTable dtRegs = dv.ToTable();
            Session["RegDate"] = dtRegs.Rows[0]["StartDate"].ToString();
        }
        if (Session["LastAreaVisited"] != null)
            lblTechVisited.Text = Session["LastAreaVisited"].ToString();

        Session["ARTEndedStatus"] = theDS.Tables[43].Rows[0]["ARTEndStatus"].ToString();
        #region "Fill Details"
        if (theDS.Tables[0].Rows.Count > 0)
        {
            string theAddress = "";
            if (Convert.ToInt32(Session["SystemId"]) == 1)
            {
                lblpatientname.Text = theDS.Tables[0].Rows[0]["FullName"].ToString() ;
                //lblpatientname.Text = theDS.Tables[0].Rows[0]["LastName"].ToString() + ", " + theDS.Tables[0].Rows[0]["FirstName"].ToString();
                lbldob.Text = Convert.ToDateTime(theDS.Tables[0].Rows[0]["DOB"]).ToString("dd-MMM-yyyy");
                Session["DOB"] = lbldob.Text;
                if (theDS.Tables[0].Rows[0]["Address"].ToString() != "")//|| theDS.Tables[0].Rows[0]["VillageNM"].ToString() != "" || theDS.Tables[0].Rows[0]["ProvinceNM"].ToString() != "")
                {
                    theAddress = theDS.Tables[0].Rows[0]["Address"].ToString() + "/";
                    Session["Address"] = theDS.Tables[0].Rows[0]["Address"].ToString();
                }

                if (theDS.Tables[0].Rows[0]["VillageNM"].ToString() != "")
                {
                    theAddress = theAddress + theDS.Tables[0].Rows[0]["VillageNM"].ToString() + "/";
                    Session["Village"] = theDS.Tables[0].Rows[0]["VillageNM"].ToString();
                }
                else
                {
                    //theAddress = theAddress + " /";
                    Session["Village"] = "/";

                }

                if (theDS.Tables[0].Rows[0]["ProvinceNM"].ToString() != "")
                {
                    theAddress = theAddress + theDS.Tables[0].Rows[0]["ProvinceNM"].ToString();
                }

                if (theDS.Tables[0].Rows[0]["EmergContactName"].ToString() != "")
                {
                    lblemergencycontact.Text = theDS.Tables[0].Rows[0]["EmergContactName"].ToString();
                }

                if (theDS.Tables[0].Rows[0]["EmergContactPhone"].ToString() != "")
                {
                    lblemgphone.Text = theDS.Tables[0].Rows[0]["EmergContactPhone"].ToString();

                }

            }
            else
            {
                lblpatientname.Text = theDS.Tables[0].Rows[0]["LastName"].ToString() + ", " + theDS.Tables[0].Rows[0]["MiddleName"].ToString() + " , " + theDS.Tables[0].Rows[0]["FirstName"].ToString();
                lbldob.Text = Convert.ToDateTime(theDS.Tables[0].Rows[0]["DOB"]).ToString("dd-MMM-yyyy");
                Session["DOB"] = lbldob.Text;
                if (theDS.Tables[0].Rows[0]["Address"].ToString() != "")//|| theDS.Tables[0].Rows[0]["VillageNM"].ToString() != "" || theDS.Tables[0].Rows[0]["ProvinceNM"].ToString() != "")
                {
                    theAddress = theDS.Tables[0].Rows[0]["Address"].ToString() + "/";
                    Session["Address"] = theDS.Tables[0].Rows[0]["Address"].ToString();
                }

                if (theDS.Tables[0].Rows[0]["VillageNM"].ToString() != "")
                {
                    theAddress = theAddress + theDS.Tables[0].Rows[0]["VillageNM"].ToString() + "/";
                    Session["Village"] = theDS.Tables[0].Rows[0]["VillageNM"].ToString();
                }
                else
                {
                    theAddress = theAddress + " /";
                    Session["Village"] = "/";

                }
                if (theDS.Tables[0].Rows[0]["ProvinceNM"].ToString() != "")
                {
                    theAddress = theAddress + theDS.Tables[0].Rows[0]["ProvinceNM"].ToString();
                }

                if (theDS.Tables[0].Rows[0]["TenCellLeader"].ToString() != "")
                {
                    lblemergencycontact.Text = theDS.Tables[0].Rows[0]["TenCellLeader"].ToString();
                }

                if (theDS.Tables[0].Rows[0]["TenCellLeaderAddress"].ToString() != "")
                {
                    lblemgphone.Text = theDS.Tables[0].Rows[0]["TenCellLeaderAddress"].ToString();

                }

            }
            hpIQNumber.Value = theDS.Tables[0].Rows[0]["IQNumber"].ToString();
            Session["EmerPhNo"] = theDS.Tables[0].Rows[0]["phone"].ToString();
            Session["District"] = theDS.Tables[0].Rows[0]["District"].ToString();
            Session["DistrictID"] = theDS.Tables[0].Rows[0]["DistrictId"].ToString();
            Session["VillageID"] = theDS.Tables[0].Rows[0]["VillageId"].ToString();
            Session["PatientName"] = lblpatientname.Text;
            Session["PatientSex"] = theDS.Tables[0].Rows[0]["SexNM"].ToString();
            Session["PatientAge"] = theDS.Tables[0].Rows[0]["AGE"].ToString() + "." + theDS.Tables[0].Rows[0]["AgeInMonths"].ToString();
            Session["patientageinyearmonth"] = theDS.Tables[0].Rows[0]["AGEINYEARMONTH"].ToString();
            lbladdress.Text = theAddress;
            lblptnenrollment.Text = theDS.Tables[0].Rows[0]["PatientEnrollmentId"].ToString();
            lblexistingid.Text = theDS.Tables[0].Rows[0]["PatientClinicID"].ToString();
            lblancno.Text = theDS.Tables[0].Rows[0]["ANCNumber"].ToString();
            lblpmtctno.Text = theDS.Tables[0].Rows[0]["PMTCTNumber"].ToString();
            lbladmissionno.Text = theDS.Tables[0].Rows[0]["AdmissionNumber"].ToString();
            lbloutpatientno.Text = theDS.Tables[0].Rows[0]["OutpatientNumber"].ToString();
            lblage.Text = theDS.Tables[0].Rows[0]["AGE"].ToString(); //+ "." + theDS.Tables[0].Rows[0]["AgeInMonths"].ToString();
            //lblage.Text = theDS.Tables[0].Rows[0]["AGEINYEARMONTH"].ToString();
            lblgender.Text = theDS.Tables[0].Rows[0]["SexNM"].ToString();

            if ((theDS.Tables[0].Rows[0]["HIVStatus_Child"] != System.DBNull.Value) && (theDS.Tables[0].Rows[0]["HIVStatus_Child"].ToString() == "1"))
            {
                lblhivpositivemother.Visible = true;
            }
            strPatientEnrollmentId = Convert.ToString(theDS.Tables[0].Rows[0]["PatientEnrollmentID"]);
        }

        string theUrl;
        theUrl = string.Format("{0}&PatientEnrollmentID={1}&FormName={2}", "../Scheduler/frmScheduler_AppointmentNew.aspx?name=Add", strPatientEnrollmentId, "PatientHome");
        // lnkSchedule.HRef = theUrl;

        if (theDS.Tables[19].Rows.Count > 0)
        {
            if (theDS.Tables[19].Rows[0]["ART/PalliativeCare"].ToString() == "Care Ended")
            {
                lblptnstatus.Text = "Care Ended";
                (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text = "1";
                btnReactivate.Visible = true;
                btnReactivate.CssClass = "btn btn-primary";
            }

            else if (theDS.Tables[19].Rows[0]["ART/PalliativeCare"].ToString() != "Care Ended")
            {
                lblptnstatus.Text = "Active";
                (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text = "0";
                btnReactivate.Visible = false;
            }
            Session["PtnPrgStatus"] = theDS.Tables[19];
            Session["CEndedStatus"] = theDS.Tables[40];
            DataTable dt = new DataTable();
            dt = theDS.Tables[42];
            if (theDS.Tables[42].Rows.Count > 0)
            {
                if (dt.Rows[0]["PatientExitReason"].ToString() == "93")
                {
                    lblptnstatus.Text = "Care Ended";
                    (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text = "1";
                    btnReactivate.Visible = true;
                    btnReactivate.CssClass = "greenbutton";

                }
            }



            ////if (((theDS.Tables[19].Rows[0]["PMTCTStatus"].ToString() == "PMTCT Care Ended") && (theDS.Tables[19].Rows[0]["ART/PalliativeCare"].ToString() == "Care Ended"))
            ////    || ((theDS.Tables[19].Rows[0]["PMTCTStatus"].ToString() == "") && (theDS.Tables[19].Rows[0]["ART/PalliativeCare"].ToString() == "Care Ended"))
            ////    || ((theDS.Tables[19].Rows[0]["PMTCTStatus"].ToString() == "PMTCT Care Ended") && (theDS.Tables[19].Rows[0]["ART/PalliativeCare"].ToString() == "")))
            ////{
            ////    lblptnstatus.Text = "Care Ended";
            ////    (Master.FindControl("lblpntStatus") as Label).Text = "1";
            ////}

            ////else if (((theDS.Tables[19].Rows[0]["PMTCTStatus"].ToString() != "PMTCT Care Ended") || (theDS.Tables[19].Rows[0]["ART/PalliativeCare"].ToString() != "Care Ended"))
            ////           || ((theDS.Tables[19].Rows[0]["PMTCTStatus"].ToString() == "PMTCT Care Ended") && (theDS.Tables[19].Rows[0]["ART/PalliativeCare"].ToString() != "Care Ended"))
            ////           || ((theDS.Tables[19].Rows[0]["PMTCTStatus"].ToString() != "PMTCT Care Ended") && (theDS.Tables[19].Rows[0]["ART/PalliativeCare"].ToString() == "Care Ended")))
            ////{
            ////    lblptnstatus.Text = "Active";
            ////    (Master.FindControl("lblpntStatus") as Label).Text = "0";

            ////}
            //if (Convert.ToInt32(theDS.Tables[19].Rows[0]["Status"]) == 0)
            //{
            //    lblptnstatus.Text = "Active";
            //    (Master.FindControl("lblpntStatus") as Label).Text = "0";

            //}
            //else
            //{
            //    lblptnstatus.Text = "Care Ended";
            //    (Master.FindControl("lblpntStatus") as Label).Text = "1";
            //}           

        }

        if (theDS.Tables[0].Rows.Count > 0)
        {

            if (theDS.Tables[0].Rows[0]["Phone"].ToString() != "")
            {
                lblpatientphone.Text = theDS.Tables[0].Rows[0]["Phone"].ToString();
            }
            else
            {
                lblpatientphone.Text = "";
            }

            if (Convert.ToInt32(theDS.Tables[0].Rows[0]["AgeInMonths"]) < 18)
            {
                int LessAgeMonth = (18 - Convert.ToInt32(theDS.Tables[0].Rows[0]["AgeInMonths"]));
                DateTime dt1 = new DateTime();
                dt1 = Convert.ToDateTime(Application["AppCurrentDate"]);
                dt1 = dt1.AddMonths(LessAgeMonth);
                lblnexthivscheck.Visible = true;
                lblnexthivstatuscheck.Visible = true;
                lblnexthivscheck.Text = dt1.ToString(Session["AppDateFormat"].ToString());
            }
        }

        if (theDS.Tables[1].Rows.Count > 0)
        {
            theTmpDate = Convert.ToDateTime(theDS.Tables[1].Rows[0]["VisitDate"]);
            lbllstvisit.Text = theTmpDate.ToString(Session["AppDateFormat"].ToString());

        }
        else
        {
            lbllstvisit.Text = "";
        }
        if (theDS.Tables[2].Rows.Count > 0)
        {
            if (theDS.Tables[2].Rows[0]["AppDate"] != System.DBNull.Value)
            {
                theTmpDate = Convert.ToDateTime(theDS.Tables[2].Rows[0]["AppDate"]);
                lblnextapp.Text = theTmpDate.ToString(Session["AppDateFormat"].ToString());
            }
            else
            {
                lblnextapp.Text = "";
            }
        }
        else
        {
            lblnextapp.Text = "";
        }

        if (theDS.Tables[3].Rows.Count > 0)
        {
            DataView theDV = new DataView(theDS.Tables[3]);
            theDV.RowFilter = "Parameterid = 5 AND TestResults is not null";
            if (theDV.Count > 0)
            {
                theDV.Sort = "OrderedByDate desc";
                if (theDV[0]["TestResults"] != System.DBNull.Value)
                {
                    lblHB.Text = theDV[0]["TestResults"].ToString() + "(" + ((DateTime)theDV[0]["OrderedByDate"]).ToString(Session["AppDateFormat"].ToString()) + ")";
                    if (lblHB.Text != "")
                    {
                        theGraphHb = Convert.ToDouble(theDV[0]["TestResults"].ToString());
                        month3 = string.Format("{0:MMM-yyyy}", theDV[0]["OrderedByDate"]);
                    }

                }
            }

            theDV.RowFilter = "Parameterid = 75 AND TestResults is not null";

            if (theDV.Count > 0)
            {
                theDV.Sort = "OrderedByDate desc";

                if (theDV[0]["TestResults"] != System.DBNull.Value)
                {
                    if (Convert.ToInt32(theDV[0]["TestResults"]) == 1)
                    {
                        lblSyphilis.Text = "Positive";


                    }
                    else if (Convert.ToInt32(theDV[0]["TestResults"]) == 0)
                    {

                        lblSyphilis.Text = "Negative";
                    }




                }
            }
            theDV.RowFilter = "Parameterid = 6 AND TestResults is not null";
            if (theDV.Count > 0)
            {
                if (theDV[0]["TestResults"] != System.DBNull.Value)
                {
                    lblHCT.Text = theDV[0]["TestResults"].ToString() + "(" + ((DateTime)theDV[0]["OrderedByDate"]).ToString(Session["AppDateFormat"].ToString()) + ")";
                    if (lblHCT.Text != "")
                    {
                        theGraphHct = Convert.ToDouble(theDV[0]["TestResults"].ToString());
                        month4 = string.Format("{0:MMM-yyyy}", theDV[0]["OrderedByDate"]);
                    }
                }
            }
            theDV.RowFilter = "Parameterid = 10 AND TestResults is not null";
            if (theDV.Count > 0)
            {
                if (theDV[0]["TestResults"] != System.DBNull.Value)
                {
                    lblAST.Text = theDV[0]["TestResults"].ToString() + "(" + ((DateTime)theDV[0]["OrderedByDate"]).ToString(Session["AppDateFormat"].ToString()) + ")";
                    if (lblAST.Text != "")
                    {
                        theGraphAST = Convert.ToDouble(theDV[0]["TestResults"].ToString());
                        month5 = string.Format("{0:MMM-yyyy}", theDV[0]["OrderedByDate"]);
                    }
                }
            }
            theDV.RowFilter = "Parameterid = 12 AND TestResults is not null";
            string theCRmgtxt = "";
            if (theDV.Count > 0)
            {
                if (theDV[0]["TestResults"] != System.DBNull.Value)
                {
                    theCRmgtxt = theDV[0]["TestResults"].ToString() + "(" + ((DateTime)theDV[0]["OrderedByDate"]).ToString(Session["AppDateFormat"].ToString()) + ")";
                    if (theCRmgtxt != "")
                    {
                        theGraphCr = Convert.ToDouble(theDV[0]["TestResults"].ToString());
                        month6 = string.Format("{0:MMM-yyyy}", theDV[0]["OrderedByDate"]);
                    }
                }
            }
            theDV.RowFilter = "Parameterid = 106 AND TestResults is not null";
            string theCRmmtxt = "";
            if (theDV.Count > 0)
            {
                if (theDV[0]["TestResults"] != System.DBNull.Value)
                {
                    theCRmmtxt = theDV[0]["TestResults"].ToString() + "(" + ((DateTime)theDV[0]["OrderedByDate"]).ToString(Session["AppDateFormat"].ToString()) + ")";
                }
            }

            if (theCRmgtxt == "")
            {
                lblCr.Text = theCRmmtxt;
            }
            else
            {
                lblCr.Text = theCRmgtxt;
            }

        }
        if (theDS.Tables[4].Rows.Count > 0)
        {
            if (theDS.Tables[4].Rows[0]["Current ARV StartDate"] != System.DBNull.Value)
            {
                lblarvstartdate.Text = Convert.ToDateTime(theDS.Tables[4].Rows[0]["Current ARV StartDate"]).ToString(Session["AppDateFormat"].ToString());

            }
            else
            {
                lblarvstartdate.Text = "";
            }

            if (theDS.Tables[4].Rows[0]["AidsRelief ARV StartDate"] != System.DBNull.Value)
            {
                lblaidsrstartdate.Text = Convert.ToDateTime(theDS.Tables[4].Rows[0]["AidsRelief ARV StartDate"]).ToString(Session["AppDateFormat"].ToString());
            }
            else
            {
                lblaidsrstartdate.Text = "";
            }
            if ((theDS.Tables[4].Rows[0]["Hist ARV StartDate"] != System.DBNull.Value) && (Session["SystemId"].ToString() == "1"))
            {
                lblhistoricalsdate.Text = Convert.ToDateTime(theDS.Tables[4].Rows[0]["Hist ARV StartDate"]).ToString(Session["AppDateFormat"].ToString());
            }

            else if ((theDS.Tables[4].Rows[0]["Hist ARV StartDateCTC"] != System.DBNull.Value) && (Session["SystemId"].ToString() == "2"))
            {
                lblhistoricalsdate.Text = Convert.ToDateTime(theDS.Tables[4].Rows[0]["Hist ARV StartDateCTC"]).ToString(Session["AppDateFormat"].ToString());
            }
            else
            {
                lblhistoricalsdate.Text = "";
            }
        }
        if (theDS.Tables[4].Rows.Count > 0)
        {
            if (theDS.Tables[4].Rows[0]["Current ARV Regimen"].ToString() != "0")
            {
                lblarvregimen.Text = theDS.Tables[4].Rows[0]["Current ARV Regimen"].ToString();
            }
            else
            {
                lblarvregimen.Text = "";
            }

        }
        else
        {
            lblarvregimen.Text = "";

        }

        /*CD4 and Viral Load Graph */
        double[] CD4 = new Double[theDS.Tables[6].Rows.Count];
        for (Int32 a = 0, l = CD4.Length; a < l; a++)
        {
            if (theDS.Tables[6].Rows[a]["TestResult"] != System.DBNull.Value)
            {
                CD4.SetValue(Convert.ToDouble(theDS.Tables[6].Rows[a]["TestResult"]), a);
            }
        }

        double[] ViralLoad = new Double[theDS.Tables[7].Rows.Count];
        for (Int32 a = 0, l = ViralLoad.Length; a < l; a++)
        {
            if (theDS.Tables[7].Rows[a]["TestResult"] != System.DBNull.Value)
            {
                ViralLoad.SetValue(Convert.ToDouble(theDS.Tables[7].Rows[a]["TestResult"]), a);
            }
        }

        DateTime[] YearCD4 = new DateTime[theDS.Tables[6].Rows.Count];
        for (Int32 a = 0, l = YearCD4.Length; a < l; a++)
        {
            YearCD4.SetValue((DateTime)theDS.Tables[6].Rows[a]["DATE"], a);
        }

        DateTime[] YearVL = new DateTime[theDS.Tables[7].Rows.Count];
        for (Int32 a = 0, l = YearVL.Length; a < l; a++)
        {
            YearVL.SetValue(theDS.Tables[7].Rows[a]["DATE"], a);
        }

        DateTime[] Year = new DateTime[theDS.Tables[8].Rows.Count];
        for (Int32 a = 0, l = Year.Length; a < l; a++)
        {
            Year.SetValue(theDS.Tables[8].Rows[a]["DATE"], a);
        }
        //18thAug2009 createChartCD4(CD4, ViralLoad, YearCD4, YearVL, Year);
        Chart.setLicenseCode("DEVP-2AC2-336W-54FM-EAB2-F8E2");
        createChartCD4(WebChartViewerCD4VL, CD4, ViralLoad, YearCD4, YearVL, Year);
        Session["CD4_Graph"] = CD4;
        Session["ViralLoad_Graph"] = ViralLoad;
        Session["YearCD4_Graph"] = YearCD4;
        Session["YearVL_Graph"] = YearVL;
        Session["Year_Graph"] = Year;


        double[] Height = new Double[theDS.Tables[5].Rows.Count];
        for (Int32 a = 0, l = Height.Length; a < l; a++)
        {
            Height.SetValue(Convert.ToDouble(theDS.Tables[5].Rows[a]["Height"]), a);
        }
        double[] Weight = new Double[theDS.Tables[5].Rows.Count];
        for (Int32 a = 0, l = Weight.Length; a < l; a++)
        {
            Weight.SetValue(Convert.ToDouble(theDS.Tables[5].Rows[a]["Weight"]), a);
        }

        double[] BMI = new Double[theDS.Tables[5].Rows.Count];
        for (Int32 a = 0, l = Weight.Length; a < l; a++)
        {
            if (theDS.Tables[5].Rows[a]["BMI"] != System.DBNull.Value)
            { BMI.SetValue(Convert.ToDouble(theDS.Tables[5].Rows[a]["BMI"]), a); }
        }

        DateTime[] YearWeightBMI = new DateTime[theDS.Tables[5].Rows.Count];
        for (Int32 a = 0, l = YearWeightBMI.Length; a < l; a++)
        {
            if (theDS.Tables[5].Rows[a]["Visit_OrderbyDate"] != System.DBNull.Value)
            { YearWeightBMI.SetValue(theDS.Tables[5].Rows[a]["Visit_OrderbyDate"], a);}
        }
        // 18thAug2009 createChartWeight( Weight, BMI, YearWeightBMI);
        createChartWeight(WebChartViewerWeight, Weight, BMI, YearWeightBMI);
        Session["Weight_graph"] = Weight;
        Session["BMI_graph"] = BMI;
        Session["YearWeightBMI_graph"] = YearWeightBMI;

        if (lblgender.Text == "Female")
        {
            if (theDS.Tables[9].Rows.Count != 0)
            {
                if (theDS.Tables[9].Rows[0]["Pregnant"] != System.DBNull.Value)
                {
                    if (theDS.Tables[9].Rows[0]["Pregnant"].ToString() != "0")
                    {
                        lblPregnant.Visible = false;
                        lblPregnancyTest.Text = Convert.ToString(theDS.Tables[9].Rows[0]["Pregnantvalue"]);
                    }
                }

            }
            if (Convert.ToInt32(Session["TechnicalAreaId"]) == 1)
            {
                if (lbladmissionno.Text != "" || lblanc.Text != "" || lblpmtct.Text != "" || lbloutpatientno.Text != "")
                {
                    btnAddChildren.Visible = true;
                }
                else
                {
                    btnAddChildren.Visible = false;
                }
            }
            else { btnAddChildren.Visible = false; }

        }
        else
        {
            btnAddChildren.Visible = false;
        }

        //------------WHO Stage--------------------------------------------------------------------------------------
        if (theDS.Tables[10].Rows.Count > 0)
        {
            if (Convert.ToInt32(theDS.Tables[10].Rows[0]["WHOStageFlag"]) == 1)
            {
                if (theDS.Tables[11].Rows[0]["WHOStage"] != System.DBNull.Value)
                {
                    lblWHOStage.Text = theDS.Tables[11].Rows[0]["WHOStage"].ToString();
                }
            }
        }
        //------Lowest CD4-------------------------------------------------------------------------------------------
        if (Convert.ToInt32(theDS.Tables[12].Rows[0]["LowestCD4Flag"].ToString()) != 0)
        {
            {
                DataView theDV = new DataView(theDS.Tables[13]);
                if (theDV.Count > 0)
                {
                    string theLowCD4 = "";
                    if (Session["SystemId"].ToString() == "1")
                    {
                        if (theDV[0].Row.ItemArray[0] != System.DBNull.Value)
                        {
                            theDV.RowFilter = "TestResults is not null";
                            theDV.Sort = "TestResults asc";
                            theLowCD4 = theDV[0]["TestResults"].ToString();
                            lblLowestCD4.Text = theLowCD4;
                            if (theDV[0].Row.IsNull("OrderedByDate") == false)
                            {
                                theLowCD4 = theLowCD4 + "(" + ((DateTime)theDV[0]["OrderedByDate"]).ToString(Session["AppDateFormat"].ToString()) + ")";
                                lblCD4Due.Text = theLowCD4;
                            }
                        }
                    }
                    else if (Session["SystemId"].ToString() == "2")
                    {
                        if (theDV[0].Row.ItemArray[1] != System.DBNull.Value)
                        {
                            theDV.RowFilter = "TestResultsCTC is not null";
                            theDV.Sort = "TestResultsCTC asc";
                            theLowCD4 = theDV[0]["TestResultsCTC"].ToString();
                            lblLowestCD4.Text = theLowCD4;
                        }
                    }
                }
            }
        }
        //-----Most Recent CD4 - AidsRelief-----------------------------------------------------------------------
        if ((Convert.ToInt32(theDS.Tables[14].Rows[0]["RecentCD4Flag"].ToString()) != 0) && (Session["SystemId"].ToString() == "1"))
        {
            if (theDS.Tables[15].Rows.Count > 0)
            {
                DataView theDV = new DataView(theDS.Tables[15]);
                if (theDV.Count > 0)
                {
                    string theRecentCD4 = "";
                    theRecentCD4 = theDV[0]["TestResults"].ToString();
                    lblRecentCD4.Text = theRecentCD4;
                    theGraphRecentCD4 = Convert.ToDouble(theDV[0]["TestResults"].ToString());
                    if (theDV[0].Row.IsNull("OrderedByDate") == false)
                        theRecentCD4 = theRecentCD4 + "(" + ((DateTime)theDV[0]["OrderedByDate"]).ToString(Session["AppDateFormat"].ToString()) + ")";
                    lblCD4Due.Text = theRecentCD4;
                }
            }
        }
        if (Convert.ToInt32(theDS.Tables[16].Rows[0]["RecentCD4Flag"].ToString()) != 0 && Session["SystemId"].ToString() == "1")
        {
            if (theDS.Tables[17].Rows[0][0].ToString() != "")
            {
                lblCD4Due.Text = ((DateTime)theDS.Tables[17].Rows[0][0]).ToString(Session["AppDateFormat"].ToString());
            }
            else
            {
                lblCD4Due.Text = "";
            }
        }
        if (theDS.Tables[18].Rows.Count > 0)
        {
            if (theDS.Tables[18].Rows[0]["WABStage"] != System.DBNull.Value)
            {
                lblWABStage.Text = theDS.Tables[18].Rows[0]["WABStage"].ToString();
            }

        }

        //-----ART Status-------------------------------------------------------------
        if (theDS.Tables[19].Rows.Count > 0)
        {
            lblprogram.Text = theDS.Tables[19].Rows[0]["ART/PalliativeCare"].ToString();
            Session["Program"] = lblprogram.Text;
        }
        //-----PMTCT Status-------------------------------------------------------------
        //if (theDS.Tables[19].Rows.Count > 0)
        //{
        //    lblpmtctstatus.Text = theDS.Tables[19].Rows[0]["PMTCTStatus"].ToString();
        //}
        //else
        //{
        //    lblpmtctstatus.Text = "";
        //} 

        if (theDS.Tables[20].Rows.Count > 0)
        {
            if (theDS.Tables[20].Rows[0]["FamilyCount"].ToString() != "0")
                lblfamilyEnrolled.Text = theDS.Tables[20].Rows[0]["FamilyCount"].ToString();
        }
        if (theDS.Tables[21].Rows.Count > 0)
        {
            if (theDS.Tables[21].Rows[0]["FamilyARTCount"].ToString() != "0")
                lblfamilyArt.Text = theDS.Tables[21].Rows[0]["FamilyARTCount"].ToString();
        }
        if (theDS.Tables[22].Rows[0]["FamilyAllCount"].ToString() != "0")
        {
            imgfamily.ImageUrl = "~/images/15px-Yes_check.svg.png";
        }
        else
        {
            imgfamily.ImageUrl = "~/images/No_16x.ico";
        }

        //----------------Dynamic Labels---------------
        Session["DynamicLabels"] = theDS.Tables[23];

        lblenroll.Text = theDS.Tables[23].Rows[4]["Label"].ToString();
        lblClinicNo.Text = theDS.Tables[23].Rows[3]["Label"].ToString();
        lblShowAddress.Text = theDS.Tables[23].Rows[0]["Label"].ToString();
        lblEmrContact.Text = theDS.Tables[23].Rows[1]["Label"].ToString();
        lblEmrPhone.Text = theDS.Tables[23].Rows[2]["Label"].ToString();

        //----Most Recent CD4 - CTC--------------------------------------------------------------------------------
        if (theDS.Tables[24].Rows.Count > 0 && Session["SystemId"].ToString() == "2")
        {
            string theRecentCD4 = "";
            theRecentCD4 = theDS.Tables[24].Rows[0]["TestResults"].ToString();
            lblRecentCD4.Text = theRecentCD4;
            if (theDS.Tables[24].Rows[0]["Dis_Date"].ToString() == "1")
            {
                theRecentCD4 = theRecentCD4 + "(" + ((DateTime)theDS.Tables[24].Rows[0]["OrderedByDate"]).ToString(Session["AppDateFormat"].ToString()) + ")";
                lblRecentCD4.Text = theRecentCD4;
                lblCD4Due.Text = ((DateTime)theDS.Tables[24].Rows[0]["OrderedByDueDate"]).ToString(Session["AppDateFormat"].ToString());
            }
        }
        //--------------Weight--------------------------------------------------------------------------------------
        if (theDS.Tables[25].Rows.Count > 0)
        {
            if (theDS.Tables[25].Rows[0]["weight"] != System.DBNull.Value)
            {
                lblweight.Text = theDS.Tables[25].Rows[0]["weight"].ToString();
            }
            else
            {
                lblweight.Text = "";
            }
        }

        if (theDS.Tables[26].Rows.Count > 0)
        {
            if (theDS.Tables[26].Rows[0].IsNull("CurrARTStock") != true && Convert.ToInt32(theDS.Tables[26].Rows[0]["CurrARTStock"]) <= 5 && Convert.ToInt32(theDS.Tables[26].Rows[0]["CurrARTStock"]) >= 0)
            {
                lbloutofstock.Text = "This patient ARVs supply is finishing in " + Convert.ToInt32(theDS.Tables[26].Rows[0]["CurrARTStock"]) + " days.";
                lbloutofstock.Visible = true;
            }

            if (theDS.Tables[26].Rows[0].IsNull("CurrARTStock") == true || Convert.ToInt32(theDS.Tables[26].Rows[0]["CurrARTStock"]) < 0)
            {
                lbloutofstock.Text = "This patient is out of ARVs.";
                lbloutofstock.Visible = true;
            }
        }

        //----AgeForPMTCTCareTracking Form----------------------------------------
        //if (theDS.Tables[27].Rows.Count > 0)
        //    Session["PatientAge"] = theDS.Tables[27].Rows[0]["PatientAge"].ToString();

        //-----Current ARV Prophylaxis Regimen and Current ARV Prophylaxis Regimen Start Date
        if (theDS.Tables[28].Rows.Count > 0)
        {
            if (theDS.Tables[28].Rows[0]["CurrentARVProphylaxisRegimen"] != "0")
            {
                lblARVProRegimen.Text = theDS.Tables[28].Rows[0]["CurrentARVProphylaxisRegimen"].ToString();
            }
            else
            {
                lblARVProRegimen.Text = "";
            }
            if (theDS.Tables[28].Rows[0]["CurrentProphylaxisRegimenStartDate"] != System.DBNull.Value)
            {
                lblARVProStartDate.Text = Convert.ToDateTime(theDS.Tables[28].Rows[0]["CurrentProphylaxisRegimenStartDate"]).ToString(Session["AppDateFormat"].ToString());

            }
            else
            {
                lblARVProStartDate.Text = "";
            }
        }
        //-----------Latest Child Delivered and Date--------------------------------------------------
        if (theDS.Tables[29].Rows.Count > 0)
        {
            if (theDS.Tables[29].Rows[0]["DeliveryDateTime"] != System.DBNull.Value)
            {
                //lblChildDelivered.Text = "Yes";
                lblDeliveryDate.Text = Convert.ToDateTime(theDS.Tables[29].Rows[0]["DeliveryDateTime"]).ToString(Session["AppDateFormat"].ToString());

            }
            else
            {
                //lblChildDelivered.Text = "";
                lblDeliveryDate.Text = "";
            }
        }
        //----- Feeding Option-------------------------------------------------------------------
        if (theDS.Tables[30].Rows.Count > 0)
        {
            lblFeedingOption.Text = theDS.Tables[30].Rows[0]["FeedingOption"].ToString();
        }
        else
        {
            lblFeedingOption.Text = "";
        }
        //------PMTCT Last Visit Date ------------------------------------------------------------------
        if (theDS.Tables[31].Rows.Count > 0)
        {
            lblLastVisit.Text = Convert.ToDateTime(theDS.Tables[31].Rows[0]["PMTCTVisitDate"]).ToString(Session["AppDateFormat"].ToString());
        }
        else
        {
            lblLastVisit.Text = "";
        }

        //------PMTCT Last Visit Date ------------------------------------------------------------------
        if (theDS.Tables[31].Rows.Count > 0)
        {
            lblLastVisit.Text = Convert.ToDateTime(theDS.Tables[31].Rows[0]["PMTCTVisitDate"]).ToString(Session["AppDateFormat"].ToString());
        }
        else
        {
            lblLastVisit.Text = "";
        }
        //------PMTCT Last Visit Date ------------------------------------------------------------------
        if (theDS.Tables[31].Rows.Count > 0)
        {
            lblLastVisit.Text = Convert.ToDateTime(theDS.Tables[31].Rows[0]["PMTCTVisitDate"]).ToString(Session["AppDateFormat"].ToString());
        }
        else
        {
            lblLastVisit.Text = "";
        }
        //------LMP From ANC ------------------------------------------------------------------
        if (theDS.Tables[33].Rows.Count > 0)
        {
            lblLMP.Text = Convert.ToDateTime(theDS.Tables[33].Rows[0]["LMP"]).ToString(Session["AppDateFormat"].ToString());
        }
        else
        {
            lblLMP.Text = "";
        }
        //------EDD From ANC  ------------------------------------------------------------------
        if (theDS.Tables[34].Rows.Count > 0)
        {
            lblEDD.Text = Convert.ToDateTime(theDS.Tables[34].Rows[0]["EDD"]).ToString(Session["AppDateFormat"].ToString());
        }
        else
        {
            lblEDD.Text = "";
        }
        //------TBStatus from ANC  ------------------------------------------------------------------
        if (theDS.Tables[35].Rows.Count > 0)
        {
            lblTBStatus.Text = theDS.Tables[35].Rows[0]["TBStatus"].ToString();
        }
        else
        {
            lblTBStatus.Text = "";
        }

        //------Partner HIV Status  ------------------------------------------------------------------
        if (theDS.Tables[36].Rows.Count > 0)
        {
            lblPartnerHIVStatus.Text = theDS.Tables[36].Rows[0]["Partner HIV Status"].ToString();
        }
        else
        {
            lblPartnerHIVStatus.Text = "";
        }

        if (theDS.Tables[37].Rows.Count > 0)
        {
            if (theDS.Tables[37].Rows[0]["Prophylaxis Regimen"] != System.DBNull.Value)
            {
                //lblInfantProphylaxisRegimen.Text = "Yes";

                lblInfantProphylaxisRegimen.Text = theDS.Tables[37].Rows[0]["Prophylaxis Regimen"].ToString();

            }
            else
            {
                lblInfantProphylaxisRegimen.Text = "";

            }
        }


        if (theDS.Tables[38].Rows.Count > 0)
        {
            StringBuilder strBuilder1 = new StringBuilder();
            strBuilder1.Append("<table border='0'  width='100%'>");
            strBuilder1.Append("<tr >");
            strBuilder1.Append("<td class='smallerlabel' style= 'font-weight: bold'>Test</td>");
            strBuilder1.Append("<td class='smallerlabel' style= 'font-weight: bold'>Date</td>");
            strBuilder1.Append("<td class='smallerlabel' style= 'font-weight: bold'>Age(Mnt)</td>");
            strBuilder1.Append("<td class='smallerlabel' style= 'font-weight: bold'>Result</td>");

            strBuilder1.Append("</tr>");

            for (int i = 0; i < theDS.Tables[38].Rows.Count; i++)
            {


                strBuilder1.Append("<tr>");
                strBuilder1.Append("<td  class='smallerlabel  '   width='25%'>" + theDS.Tables[38].Rows[i]["Test"].ToString() + "</td>");


                strBuilder1.Append("<td class='smallerlabel '  width='25%'>" + theDS.Tables[38].Rows[i]["Date"].ToString() + "</td>");

                //strBuilder1.Append("<td class='smallerlabel '  width='25%'>" + Convert.ToDateTime(theDS.Tables[38].Rows[i]["Age(Mnt)"]).ToString(Session["AppDateFormat"].ToString()) + "</td>");

                strBuilder1.Append("<td class='smallerlabel '  width='25%'>" + theDS.Tables[38].Rows[i]["Age(Mnt)"].ToString() + "</td>");

                if (theDS.Tables[38].Rows[i]["Test"].ToString() != "HIV Rapid (Confirmatory)")
                {
                    if (theDS.Tables[38].Rows[i]["Result"].ToString() == "1.00")
                    {
                        string strPos = String.Empty;
                        strPos = "Positive";
                        strBuilder1.Append("<td class='smallerlabel'  width='25%'>" + strPos + "</td>");
                    }


                    else if (theDS.Tables[38].Rows[i]["Result"].ToString() == "0.00")
                    {
                        string strNeg = String.Empty;
                        strNeg = "Negative";
                        strBuilder1.Append("<td class='smallerlabel' width='25%' >" + strNeg + "</td>");
                    }
                }
                else if (theDS.Tables[38].Rows[i]["Test"].ToString() == "HIV Rapid (Confirmatory)")
                {
                    if (theDS.Tables[38].Rows[i]["Result"].ToString() == "1.00")
                    {
                        string strSpace = String.Empty;

                        strBuilder1.Append("<td class='smallerlabel'  width='25%'>" + strSpace + "</td>");

                    }
                }

                strBuilder1.Append("</tr>");


            }
            strBuilder1.Append("</table>");
            Literal1.Text = strBuilder1.ToString();

        }
        if (theDS.Tables[39].Rows.Count > 0)
        {
            if (theDS.Tables[39].Rows[0]["Gestational Age"] != System.DBNull.Value)
            {
                lblGestAge.Text = theDS.Tables[39].Rows[0]["Gestational Age"].ToString();


            }
            else
            {
                lblGestAge.Text = "";

            }
        }

        //for (int i = 0; i < theDS.Tables[38].Rows.Count; i++)
        //{
        //    strBuilder2.Append("<table border='0'  width='100%'>");
        //    strBuilder2.Append("<tr style='background-color:#e1e1e1'>");
        //    strBuilder2.Append("<tr>");

        //    strBuilder2.Append("<td class='smallerlabel'>" + theDS.Tables[38].Rows[i]["OrderedbyDate"].ToString() + "</td>");
        //    strBuilder3.Append("<td class='smallerlabel'>" + theDS.Tables[38].Rows[i]["Age"].ToString() + "</td>");

        //    strBuilder2.Append("</tr>");
        //}
        //strBuilder2.Append("</table>");


        //Literal2.Text = strBuilder2.ToString();

        //for (int i = 0; i < theDS.Tables[38].Rows.Count; i++)
        //{
        //    strBuilder3.Append("<table border='0'  width='100%'>");
        //    strBuilder3.Append("<tr style='background-color:#e1e1e1'>");
        //    strBuilder3.Append("<tr>");


        //    strBuilder3.Append("<td class='smallerlabel'>" + theDS.Tables[38].Rows[i]["Age"].ToString() + "</td>");

        //    strBuilder3.Append("</tr>");
        //}
        //strBuilder3.Append("</table>");



        //Literal3.Text = strBuilder3.ToString();




        //------Child HIV Status---------------------------------------------------------------------
        //if (theDS.Tables[32].Rows.Count > 0)
        //{
        //    lblChildHIVStatus.Text = theDS.Tables[32].Rows[0]["ChildHIVStatus"].ToString();
        //} 
        #endregion
            DataTable theDTMod = (DataTable)Session["AppModule"];
            DataView theDVMod = new DataView(theDTMod);
            theDVMod.RowFilter = "ModuleId=" + Convert.ToInt32(Session["TechnicalAreaId"]);
            //VY added label for modulename 2014-10-14
            Session["TechnicalAreaName"] = theDVMod[0]["ModuleName"].ToString();
            lblServiceArea.InnerText = Session["TechnicalAreaName"].ToString();
            //tbpnldynamic.HeaderText = Session["TechnicalAreaName"].ToString();
            if (!object.Equals(Session["TechnicalAreaName"], null))
                lblDynamicHeader.Text = Session["TechnicalAreaName"].ToString();
            else
                lblDynamicHeader.Text = "";

            if (Convert.ToInt32(Session["TechnicalAreaId"]) == 2)
            {
                thePnl.Controls.Add((HtmlTable)tbhiv);
                PreviousEnrollmentDetails(2, Convert.ToInt32(Session["PatientId"]), DynControlsARV);
            }
            else if (Convert.ToInt32(Session["TechnicalAreaId"]) == 1)
            {
                thePnl.Controls.Add((HtmlTable)tblpmtct);
                PreviousEnrollmentDetails(1, Convert.ToInt32(Session["PatientId"]), DynControlPMTCT);
            }
            else
            {
                TechnicalAreaIndicators(Convert.ToInt32(theDVMod[0]["ModuleId"]), theDVMod[0]["ModuleName"].ToString());
            }
            theDVMod.RowFilter = "ModuleId=201";
            if (theDVMod.Count == 0)
            {
                //TabPanelPatientCosts.Visible = false;
                //Note: Comment by Rahmat for new Tab
            }
            if (Convert.ToString(Session["MotherPtnpk"]) != "")
            {
                btnShowMother.Visible = true;
            }
            if (Session["SCMModule"] == null)
            {
                grdarvadherence.DataSource = theDS.Tables[44];
                grdarvadherence.DataBind();

                grdCotriadherence.DataSource = theDS.Tables[46];
                grdCotriadherence.DataBind();
            }
            else if (Session["SCMModule"] != null)
            {
                grdarvadherence.DataSource = theDS.Tables[45];
                grdarvadherence.DataBind();
                grdCotriadherence.DataSource = theDS.Tables[47];
                grdCotriadherence.DataBind();
            }
            
    }

    private void createChartCD4(WebChartViewer viewer, Double[] CD4, Double[] ViralLoad, DateTime[] YearCD4, DateTime[] YearVL, DateTime[] Year)
    {
        XYChart c = new XYChart(300, 180, 0xddddff, 0x000000, 1);
        c.addLegend(90, 10, false, "Arial Bold", 7).setBackground(0xcccccc);
        c.setPlotArea(60, 60, 180, 45, 0xffffff).setGridColor(0xcccccc, 0xccccccc);
        c.xAxis().setTitle("Year");
        c.xAxis().setLabelStyle("Arial", 8, 1).setFontAngle(90);
        c.yAxis().setLinearScale(0, 1500, 500, 0);
        c.yAxis2().setLogScale(10, 10000, 10);

        LineLayer layer = c.addLineLayer2();

        layer.setLineWidth(2);
        layer.addDataSet(CD4, 0xff0000, "CD4").setDataSymbol(Chart.CircleShape, 5);
        layer.setXData(YearCD4);

        LineLayer layer1 = c.addLineLayer2();
        layer1.setLineWidth(2);
        layer1.setUseYAxis2();
        layer1.addDataSet(ViralLoad, 0x008800, "Viralload").setDataSymbol(Chart.CircleShape, 5);
        layer1.setXData(YearVL);

        // Output the chart
        viewer.Image = c.makeWebImage(Chart.PNG);
        viewer.ImageMap = c.getHTMLImageMap("", "",
            "title='{dataSetName} Count on {xLabel}={value}'");

    }

    //18thAug2009 private void createChartWeight( Double[] Weight, Double[] BMI, DateTime[] YearWeightBMI)
    private void createChartWeight(WebChartViewer Wviewer, Double[] Weight, Double[] BMI, DateTime[] YearWeightBMI)
    {
        XYChart c = new XYChart(300, 180, 0xddddff, 0x000000, 1);
        c.addLegend(90, 10, false, "Arial Bold", 7).setBackground(0xcccccc);
        c.setPlotArea(60, 60, 180, 45, 0xffffff).setGridColor(0xcccccc, 0xccccccc);
        c.xAxis().setTitle("Year");
        c.xAxis().setLabelStyle("Arial", 8, 1).setFontAngle(90);
        c.yAxis().setLinearScale(0, 200, 50, 0);
        c.yAxis2().setLogScale(0, 1000, 10);

        LineLayer layer = c.addLineLayer2();
        layer.setLineWidth(2);
        layer.addDataSet(Weight, 0xff0000, "Weight").setDataSymbol(Chart.CircleShape, 5);
        int count = YearWeightBMI.Length;
        layer.setXData(YearWeightBMI);

        LineLayer layer1 = c.addLineLayer2();
        layer1.setLineWidth(2);
        layer1.setUseYAxis2();
        layer1.addDataSet(BMI, 0x008800, "BMI").setDataSymbol(Chart.CircleShape, 5);
        layer1.setXData(YearWeightBMI);

        // Output the chart
        Wviewer.Image = c.makeWebImage(Chart.PNG);
        //Include tool tip for the chart
        Wviewer.ImageMap = c.getHTMLImageMap("", "",
          "title='{dataSetName} Count on {xLabel}={value}'");


    }



    private void TechnicalAreaIndicators(int ModuleId, string ModName)
    {
        try
        {
            TechnicalAreaIdentifier(ModuleId, ModName);

            IPatientHome PatientHome = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
            System.Data.DataSet DSTab = PatientHome.GetTechnicalAreaIndicators(ModuleId, Convert.ToInt32(Session["PatientId"]));

            if (DSTab.Tables.Count > 0)
            {
                if (DSTab.Tables[0].Rows.Count > 0)
                {
                    thePnl.Controls.Add(new LiteralControl("<tr>"));
                    thePnl.Controls.Add(new LiteralControl("<td colspan='2'>"));
                    thePnl.Controls.Add(new LiteralControl("<h5 class='forms' align='left'>" + DSTab.Tables[0].Rows[0]["Title"].ToString() + "</h5>"));
                    thePnl.Controls.Add(new LiteralControl("</td>"));
                    thePnl.Controls.Add(new LiteralControl("</tr>"));
                    int m = 2;
                    foreach (DataRow theDR in DSTab.Tables[0].Rows)
                    {
                        thePnl.Controls.Add(new LiteralControl("<tr align='left'>"));
                        thePnl.Controls.Add(new LiteralControl("<td class='bold pad18' style='width: 40%'>"));
                        Label theLabel = new Label();
                        theLabel.ID = "Lbl_" + (theDR["IndicatorName"].ToString());
                        theLabel.Text = theDR["IndicatorName"].ToString();
                        thePnl.Controls.Add(theLabel);
                        thePnl.Controls.Add(new LiteralControl("</td>"));



                        if ((DSTab.Tables.Count > (0 + m)) && (DSTab.Tables[0 + m].Columns.Count > 1 || DSTab.Tables[0 + m].Rows.Count > 1))
                        {
                            DataGrid objdView = new DataGrid();

                            objdView.ID = "Dview_" + (theDR["IndicatorName"].ToString()) + "_Val";
                            objdView.AutoGenerateColumns = true;
                            objdView.HeaderStyle.Font.Bold = true;
                            objdView.DataSource = DSTab.Tables[0 + m];
                            thePnl.Controls.Add(new LiteralControl("<td>"));
                            thePnl.Controls.Add(new LiteralControl("<table width='100%'; height:'25px';>"));
                            thePnl.Controls.Add(new LiteralControl("<tr>"));
                            thePnl.Controls.Add(new LiteralControl("<td>"));
                            thePnl.Controls.Add(new LiteralControl("<div class='gridviewFaclity whitebg';>"));
                            thePnl.Controls.Add(objdView);
                            thePnl.Controls.Add(new LiteralControl("</div>"));
                            thePnl.Controls.Add(new LiteralControl("</td>"));
                            thePnl.Controls.Add(new LiteralControl("</tr>"));
                            thePnl.Controls.Add(new LiteralControl("</table>"));
                            objdView.Visible = true;
                            objdView.DataBind();
                            thePnl.Controls.Add(new LiteralControl("</td>"));
                            thePnl.Controls.Add(new LiteralControl("</tr>"));
                        }
                        else
                        {
                            thePnl.Controls.Add(new LiteralControl("<td>"));
                            Label theLabelIndentifierValue1 = new Label();
                            Label theValueLabel = new Label();
                            theValueLabel.ID = "Lbl_" + (theDR["IndicatorName"].ToString()) + "_Val";
                            if (((DSTab.Tables.Count > (0 + m)) && (DSTab.Tables[0 + m].Rows.Count > 0)))
                            {
                                theValueLabel.Text = DSTab.Tables[0 + m].Rows[0][0].ToString();
                            }
                            //theValueLabel.Text = DSTab.Tables[0 + m].Rows[0][0].ToString();
                            thePnl.Controls.Add(theValueLabel); thePnl.Controls.Add(new LiteralControl("</td>"));
                            thePnl.Controls.Add(new LiteralControl("</tr>"));

                        }
                        m = m + 2;

                    }


                }
            }
            PreviousEnrollmentDetails(ModuleId, Convert.ToInt32(Session["PatientId"]), thePnl);
            thePnl.Controls.Add(new LiteralControl("</tbody>"));
            thePnl.Controls.Add(new LiteralControl("</table>"));

        }
        catch (Exception err)
        {
            //MsgBuilder theBuilder = new MsgBuilder();
            //theBuilder.DataElements["MessageText"] = "No records found";
            //IQCareMsgBox.Show("#C1", theBuilder, this);
        }
    }

    private void PreviousEnrollmentDetails(int theModuleId, int thePatientId, Panel thePanel)
    {
        string theSQL = "select OldEnrollDate from lnk_patientreenrollment where Ptn_pk =" + thePatientId.ToString() + " and ModuleId =" + theModuleId.ToString();
        theSQL = theSQL + " union select startdate [OldEnrollDate] from lnk_patientprogramstart where Ptn_pk =" + thePatientId.ToString() + " and ModuleId =" + theModuleId.ToString();
        theSQL = theSQL + " order by OldEnrollDate desc";
        IReports thePrevEnrollment = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports, BusinessProcess.Reports");
        System.Data.DataSet theDS = thePrevEnrollment.ReturnQueryResult(theSQL);

       
        thePanel.Controls.Add(new LiteralControl("<div class='row box-header'>"));
        thePanel.Controls.Add(new LiteralControl("<div class='col-md-10 col-sm-12 col-xs-12 form-group'>"));
        thePanel.Controls.Add(new LiteralControl("<h5 class='box-title'>Enrollment History</h5>"));
        thePanel.Controls.Add(new LiteralControl("</div>"));
        thePanel.Controls.Add(new LiteralControl("</div>"));

        if (theDS.Tables[0].Rows.Count < 1)
        {
            thePanel.Controls.Add(new LiteralControl("<div class='row voffset1'>"));
            thePanel.Controls.Add(new LiteralControl("<div class='col-md-6'>"));
            thePanel.Controls.Add(new LiteralControl("<label class='control-label lb-sm'>"));
            Label theLabel = new Label();
            theLabel.Text = "None";
            thePanel.Controls.Add(theLabel);
            thePanel.Controls.Add(new LiteralControl("</label>"));
            thePanel.Controls.Add(new LiteralControl("</div>"));
            thePanel.Controls.Add(new LiteralControl("</div>"));
        }
        else
        {
            int i = 0;
            foreach (DataRow theDR in theDS.Tables[0].Rows)
            {               
                thePanel.Controls.Add(new LiteralControl("<div class='row voffset1'>"));
                thePanel.Controls.Add(new LiteralControl("<div class='col-md-6'>"));
                thePanel.Controls.Add(new LiteralControl("<label class='control-label lb-sm'>"));
                Label theLabel = new Label();
                i = i + 1;
                theLabel.ID = "ReEnroll_Dt_" + i.ToString();
                if (i == theDS.Tables[0].Rows.Count)
                    theLabel.Text = ((DateTime)theDR[0]).ToString(Session["AppDateFormat"].ToString()) + " - Enrollment Date";
                else
                    theLabel.Text = ((DateTime)theDR[0]).ToString(Session["AppDateFormat"].ToString()) + " - ReEnrollment Date";
                thePanel.Controls.Add(theLabel);
                thePanel.Controls.Add(new LiteralControl("</label>"));
                thePanel.Controls.Add(new LiteralControl("</div>"));
                thePanel.Controls.Add(new LiteralControl("</div>"));
            }
        }
    }

    private void TechnicalAreaIdentifier(int moduleID, string ModName)
    {

        IPatientHome PatientHome = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
        System.Data.DataSet DSTab = PatientHome.GetTechnicalAreaIdentifierFuture(moduleID, Convert.ToInt32(Session["PatientId"]));

        thePnl.Controls.Add(new LiteralControl("<table border='0' cellpadding='0' cellspacing='0' width=100%>"));
        thePnl.Controls.Add(new LiteralControl("<tbody>"));
        thePnl.Controls.Add(new LiteralControl("<tr>"));
        thePnl.Controls.Add(new LiteralControl("<td colspan='2'>"));
        thePnl.Controls.Add(new LiteralControl("<h5 class='box-title' align='left'>" + ModName + " Identification Information</h5>"));
        thePnl.Controls.Add(new LiteralControl("</td>"));
        thePnl.Controls.Add(new LiteralControl("</tr>"));

        if (DSTab.Tables[0].Rows.Count > 0)
        {
            if (DSTab.Tables[0].Rows.Count > 0)
            {
                thePnl.Controls.Add(new LiteralControl("<div class='row voffset1'>"));
                thePnl.Controls.Add(new LiteralControl("<div class='col-md-2'>"));
                thePnl.Controls.Add(new LiteralControl("<label class='control-label lb-sm'>"));
                Label theLabelIdentifier1 = new Label();
                theLabelIdentifier1.ID = "Lbl_" + DSTab.Tables[0].Rows[0][0].ToString();
                theLabelIdentifier1.Text = DSTab.Tables[0].Rows[0][0].ToString() + " : ";
                
                thePnl.Controls.Add(theLabelIdentifier1);
                thePnl.Controls.Add(new LiteralControl("</label>"));
                thePnl.Controls.Add(new LiteralControl("</div>"));

                thePnl.Controls.Add(new LiteralControl("<div class='col-md-4'>"));
                Label theLabelIndentifierValue1 = new Label();
                theLabelIndentifierValue1.ID = "Lbl_" + DSTab.Tables[0].Rows[0][0].ToString() + "_Value";
                theLabelIndentifierValue1.Text = DSTab.Tables[1].Rows[0][0].ToString();
                theLabelIndentifierValue1.CssClass = "control-label aspxlb-sm";                
                thePnl.Controls.Add(theLabelIndentifierValue1);
                thePnl.Controls.Add(new LiteralControl("</div>"));
                thePnl.Controls.Add(new LiteralControl("</div>"));
                //theLabelIdentifier1.Font.Size = 5;
            }

            if (DSTab.Tables[0].Rows.Count > 1)
            {

                thePnl.Controls.Add(new LiteralControl("<div class='row voffset1'>"));
                thePnl.Controls.Add(new LiteralControl("<div class='col-md-2'>"));
                thePnl.Controls.Add(new LiteralControl("<label class='control-label lb-sm'>"));
                Label theLabelIdentifier2 = new Label();
                theLabelIdentifier2.ID = "Lbl_" + DSTab.Tables[0].Rows[1][0].ToString();
                theLabelIdentifier2.Text = DSTab.Tables[0].Rows[1][0].ToString() + " : ";
                thePnl.Controls.Add(theLabelIdentifier2);
                thePnl.Controls.Add(new LiteralControl("</label>"));
                thePnl.Controls.Add(new LiteralControl("</div>"));

                thePnl.Controls.Add(new LiteralControl("<div class='col-md-4'>"));
                Label theLabelIndentifierValue2 = new Label();
                theLabelIndentifierValue2.ID = "Lbl_" + DSTab.Tables[0].Rows[1][0].ToString() + "_Value";
                theLabelIndentifierValue2.Text = DSTab.Tables[1].Rows[0][1].ToString();
                theLabelIndentifierValue2.CssClass = "control-label aspxlb-sm";
                thePnl.Controls.Add(theLabelIndentifierValue2);
                thePnl.Controls.Add(new LiteralControl("</div>"));
                thePnl.Controls.Add(new LiteralControl("</div>"));

            }

            if (DSTab.Tables[0].Rows.Count > 2)
            {

                thePnl.Controls.Add(new LiteralControl("<div class='row voffset1'>"));
                thePnl.Controls.Add(new LiteralControl("<div class='col-md-2'>"));
                thePnl.Controls.Add(new LiteralControl("<label class='control-label lb-sm'>"));
                Label theLabelIdentifier3 = new Label();
                theLabelIdentifier3.ID = "Lbl_" + DSTab.Tables[0].Rows[2][0].ToString();
                theLabelIdentifier3.Text = DSTab.Tables[0].Rows[2][0].ToString() + " : ";
                thePnl.Controls.Add(theLabelIdentifier3);
                thePnl.Controls.Add(new LiteralControl("</label>"));
                thePnl.Controls.Add(new LiteralControl("</div>"));

                thePnl.Controls.Add(new LiteralControl("<div>"));
                Label theLabelIndentifierValue3 = new Label();
                theLabelIndentifierValue3.ID = "Lbl_" + DSTab.Tables[0].Rows[2][0].ToString() + "_Value";
                theLabelIndentifierValue3.Text = DSTab.Tables[1].Rows[0][2].ToString();
                theLabelIndentifierValue3.CssClass = "control-label aspxlb-sm";
                thePnl.Controls.Add(theLabelIndentifierValue3);
                thePnl.Controls.Add(new LiteralControl("</div>"));
                thePnl.Controls.Add(new LiteralControl("</div>"));

            }

            if (DSTab.Tables[0].Rows.Count > 3)
            {

                thePnl.Controls.Add(new LiteralControl("<div class='row voffset1'>"));
                thePnl.Controls.Add(new LiteralControl("<div class='col-md-2'>"));
                thePnl.Controls.Add(new LiteralControl("<label class='control-label lb-sm'>"));
                Label theLabelIdentifier4 = new Label();
                theLabelIdentifier4.ID = "Lbl_" + DSTab.Tables[0].Rows[3][0].ToString();
                theLabelIdentifier4.Text = DSTab.Tables[0].Rows[3][0].ToString() + " : ";
                thePnl.Controls.Add(theLabelIdentifier4);
                thePnl.Controls.Add(new LiteralControl("</label>"));
                thePnl.Controls.Add(new LiteralControl("</div>"));

                thePnl.Controls.Add(new LiteralControl("<div>"));
                Label theLabelIndentifierValue4 = new Label();
                theLabelIndentifierValue4.ID = "Lbl_" + DSTab.Tables[0].Rows[3][0].ToString() + "_Value";
                theLabelIndentifierValue4.Text = DSTab.Tables[1].Rows[0][3].ToString();
                theLabelIndentifierValue4.CssClass = "control-label aspxlb-sm";
                thePnl.Controls.Add(theLabelIndentifierValue4);
                thePnl.Controls.Add(new LiteralControl("</div>"));
                thePnl.Controls.Add(new LiteralControl("</div>"));

            }
        }
    }
    private void TechnicalAreaIdentifierOLD(int moduleID, string ModName)
    {

        IPatientHome PatientHome = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
        System.Data.DataSet DSTab = PatientHome.GetTechnicalAreaIdentifierFuture(moduleID, Convert.ToInt32(Session["PatientId"]));

        thePnl.Controls.Add(new LiteralControl("<table border='0' cellpadding='0' cellspacing='0' width=100%>"));
        thePnl.Controls.Add(new LiteralControl("<tbody>"));
        thePnl.Controls.Add(new LiteralControl("<tr>"));
        thePnl.Controls.Add(new LiteralControl("<td colspan='2'>"));
        thePnl.Controls.Add(new LiteralControl("<h5 class='forms' align='left'>" + ModName + " Identification Information</h5>"));
        thePnl.Controls.Add(new LiteralControl("</td>"));
        thePnl.Controls.Add(new LiteralControl("</tr>"));

        if (DSTab.Tables[0].Rows.Count > 0)
        {
            if (DSTab.Tables[0].Rows.Count > 0)
            {
                thePnl.Controls.Add(new LiteralControl("<tr align='left'>"));
                thePnl.Controls.Add(new LiteralControl("<td class='bold pad18' style='width: 40%'>"));
                Label theLabelIdentifier1 = new Label();
                theLabelIdentifier1.ID = "Lbl_" + DSTab.Tables[0].Rows[0][0].ToString();
                theLabelIdentifier1.Text = DSTab.Tables[0].Rows[0][0].ToString() + " : ";
                theLabelIdentifier1.CssClass = "control-label lb-sm";
                thePnl.Controls.Add(theLabelIdentifier1);
                thePnl.Controls.Add(new LiteralControl("</td>"));

                thePnl.Controls.Add(new LiteralControl("<td>"));
                Label theLabelIndentifierValue1 = new Label();
                theLabelIndentifierValue1.ID = "Lbl_" + DSTab.Tables[0].Rows[0][0].ToString() + "_Value";
                theLabelIndentifierValue1.Text = DSTab.Tables[1].Rows[0][0].ToString();
                thePnl.Controls.Add(theLabelIndentifierValue1);
                thePnl.Controls.Add(new LiteralControl("</td>"));
                thePnl.Controls.Add(new LiteralControl("</tr>"));
                //theLabelIdentifier1.Font.Size = 5;
            }

            if (DSTab.Tables[0].Rows.Count > 1)
            {

                thePnl.Controls.Add(new LiteralControl("<tr align='left'>"));
                thePnl.Controls.Add(new LiteralControl("<td class='bold pad18' style='width: 40%'>"));
                Label theLabelIdentifier2 = new Label();
                theLabelIdentifier2.ID = "Lbl_" + DSTab.Tables[0].Rows[1][0].ToString();
                theLabelIdentifier2.Text = DSTab.Tables[0].Rows[1][0].ToString() + " : ";
                thePnl.Controls.Add(theLabelIdentifier2);
                thePnl.Controls.Add(new LiteralControl("</td>"));

                thePnl.Controls.Add(new LiteralControl("<td>"));
                Label theLabelIndentifierValue2 = new Label();
                theLabelIndentifierValue2.ID = "Lbl_" + DSTab.Tables[0].Rows[1][0].ToString() + "_Value";
                theLabelIndentifierValue2.Text = DSTab.Tables[1].Rows[0][1].ToString();
                thePnl.Controls.Add(theLabelIndentifierValue2);
                thePnl.Controls.Add(new LiteralControl("</td>"));
                thePnl.Controls.Add(new LiteralControl("</tr>"));

            }

            if (DSTab.Tables[0].Rows.Count > 2)
            {

                thePnl.Controls.Add(new LiteralControl("<tr align='left'>"));
                thePnl.Controls.Add(new LiteralControl("<td class='bold pad18' style='width: 40%'>"));
                Label theLabelIdentifier3 = new Label();
                theLabelIdentifier3.ID = "Lbl_" + DSTab.Tables[0].Rows[2][0].ToString();
                theLabelIdentifier3.Text = DSTab.Tables[0].Rows[2][0].ToString() + " : ";
                thePnl.Controls.Add(theLabelIdentifier3);
                thePnl.Controls.Add(new LiteralControl("</td>"));

                thePnl.Controls.Add(new LiteralControl("<td>"));
                Label theLabelIndentifierValue3 = new Label();
                theLabelIndentifierValue3.ID = "Lbl_" + DSTab.Tables[0].Rows[2][0].ToString() + "_Value";
                theLabelIndentifierValue3.Text = DSTab.Tables[1].Rows[0][2].ToString();
                thePnl.Controls.Add(theLabelIndentifierValue3);
                thePnl.Controls.Add(new LiteralControl("</td>"));
                thePnl.Controls.Add(new LiteralControl("</tr>"));

            }

            if (DSTab.Tables[0].Rows.Count > 3)
            {

                thePnl.Controls.Add(new LiteralControl("<tr align='left'>"));
                thePnl.Controls.Add(new LiteralControl("<td class='bold pad18' style='width: 40%'>"));
                Label theLabelIdentifier4 = new Label();
                theLabelIdentifier4.ID = "Lbl_" + DSTab.Tables[0].Rows[3][0].ToString();
                theLabelIdentifier4.Text = DSTab.Tables[0].Rows[3][0].ToString() + " : ";
                thePnl.Controls.Add(theLabelIdentifier4);
                thePnl.Controls.Add(new LiteralControl("</td>"));

                thePnl.Controls.Add(new LiteralControl("<td>"));
                Label theLabelIndentifierValue4 = new Label();
                theLabelIndentifierValue4.ID = "Lbl_" + DSTab.Tables[0].Rows[3][0].ToString() + "_Value";
                theLabelIndentifierValue4.Text = DSTab.Tables[1].Rows[0][3].ToString();
                thePnl.Controls.Add(theLabelIndentifierValue4);
                thePnl.Controls.Add(new LiteralControl("</td>"));
                thePnl.Controls.Add(new LiteralControl("</tr>"));

            }
        }
    }
    #endregion

    # region Re Variables Declaration
    private string theReportName = string.Empty;
    private string theDType = string.Empty;
    private string theReportSource = string.Empty;
    private string theQuarter = string.Empty;
    private string theYear = string.Empty;
    private string theReportQuery = string.Empty;
    private string theReportTitle = string.Empty;
    private int thePatientId = 0;
    private int theReportId = 0;
    private ReportDocument rptDocument;
    string theReportHeading = string.Empty;
    string theCurrency = string.Empty;
    string theFaciltyName = string.Empty;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
        {
            IQCareMsgBox.Show("SessionExpired", this);
            Response.Redirect("~/frmlogin.aspx", true);
        }
        if (Page.IsPostBack != true)
        {
            #region "Session Variables"
            //Session["PatientId"] = 0;
            #endregion

        }
       // (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Clinical Forms >> ";
        //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Patient Home";
        (Master.FindControl("levelTwoNavigationUserControl1").FindControl("PanelPatiInfo") as Panel).Visible = false;
        Session.Remove("status");
        try
        {
            if (object.Equals(Session["TechnicalAreaId"], null))
                Response.Redirect("~/frmAddTechnicalArea.aspx", false);

            if (Session["PatientId"] != null)
            {
                Init_Form();
                Session["CEForm"] = null;
                Session["lblpntstatus"] = Convert.ToInt32((Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text);
                LoadSummaryGridView();
                getCurrency();

            }
        }
        catch (Exception err)
        {
            //string x = err.ToString();
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = "User is not registered for the selected Service Area.";
            IQCareMsgBox.Show("#C1", theBuilder, this);
            Response.Redirect("~/frmAddTechnicalArea.aspx", true);
        }

    }
    private void getCurrency()
    {
        DataTable dtCurrency = new DataTable();
        System.Data.DataSet theDS = new System.Data.DataSet();
        theDS.ReadXml(Server.MapPath("..\\XMLFiles\\Currency.xml"));
        DataView theCurrDV = new DataView(theDS.Tables[0]);
        theCurrDV.RowFilter = "Id=" + Convert.ToInt32(Session["AppCurrency"]);
        string thestringCurrency = theCurrDV[0]["Name"].ToString();
        theCurrency = thestringCurrency.Substring(thestringCurrency.LastIndexOf("(") + 1, 3);

    }
    protected void btnReactivate_Click(object sender, EventArgs e)
    {
        try
        {
            //IPatientHome ReactivatePtnMgr;
            //ReactivatePtnMgr = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
            //System.Data.DataSet theDS1 = ReactivatePtnMgr.ReActivatePatient(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["TechnicalAreaId"]));
            //Session["HIVPatientStatus"] = 0;

            //string Url = string.Format("{0}", "../ClinicalForms/frmPatient_Home.aspx");
            string Url = string.Format("{0}?mod={1}", "~/frmAddTechnicalArea.aspx", Session["TechnicalAreaId"].ToString());
            Response.Redirect(Url);
            //Server.Transfer(Url);

        }
        catch (Exception err)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
        }

    }

    protected void btnWeightChart_Click(object sender, EventArgs e)
    {
        Session["whichGraph"] = "weight_bmi";
        //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open( '../Reports/frmClinical_PatientSummary.aspx', null, 'height=950,width=650,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open( 'frmClinical_ViewGraph.aspx', 'popupwindow', 'toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=yes,resizable=no,width=950,height=650,scrollbars=yes' );", true);
    }

    protected void btnCD4Graph_Click(object sender, EventArgs e)
    {
        Session["whichGraph"] = "cd4";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open( 'frmClinical_ViewGraph.aspx', 'popupwindow', 'toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=yes,resizable=no,width=950,height=650,scrollbars=yes' );", true);

    }

    protected void btnAddChildren_Click(object sender, EventArgs e)
    {

        Session["PatientName"] = lblpatientname.Text;
        Session["DOB"] = lbldob.Text;
        //Session["AdmissionNo"] = lbladmissionno.Text;
        Session["AdmissionNo"] = hpIQNumber.Value;
        Session["PatientId"] = Convert.ToInt32(Session["PatientId"]);

        if (Session["PatientId"] != null)
        {
            Session["lblpntstatus"] = Convert.ToInt32((Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text);
            string Url = string.Format("{0}", "../ClinicalForms/frmChildEnrolment.aspx");
            Server.Transfer(Url);
        }


    }

    protected void Menu_MenuItemClick(object sender, MenuEventArgs e)
    {
        int theIndex = Int32.Parse(e.Item.Value);
        if (theIndex > 2)
        {
            TechnicalAreaIndicators(theIndex, e.Item.Text);
            TabView.ActiveViewIndex = 3;
        }
        else
        {
            TabView.ActiveViewIndex = theIndex;
        }

    }

    protected void btnTechChange_Click(object sender, EventArgs e)
    {
        DataTable theDT = (DataTable)Session["AppModule"];
        DataView theDV = new DataView(theDT);
        theDV.RowFilter = "ModuleId =" + Session["TechnicalAreaId"].ToString();
        if (theDV.Count > 0)
            Session["LastAreaVisited"] = theDV[0]["ModuleName"].ToString();
        //VY 2014-10-07 changed the link to point to find add for the specific technical area
        string theUrl = String.Format("../frmFindAddCustom.aspx?srvNm={0}&mod={1}", theDV[0]["ModuleName"], Session["TechnicalAreaId"]);
        Response.Redirect(theUrl);
    }
    #region "Patient Cost"
    private void LoadSummaryGridView()
    {
        IPatientHome PatientManager;
        PatientManager =
            (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
        DataTable dataTable = PatientManager.GetPatientDebitNoteSummary(Convert.ToInt32(Session["PatientId"]));
        GridViewSummary.DataSource = null;
        GridViewSummary.Columns.Clear();
        if (dataTable.Rows.Count > 0)
        {
            GridViewSummary.DataSource = dataTable;
            BindGrid();
            GridViewTran.DataSource = null;
            GridViewTran.DataBind();
        }
    }

    protected void GridViewSummary_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Int32 theRowIndex = Convert.ToInt32(e.CommandArgument);
        GridViewRow theRow = (sender as GridView).Rows[theRowIndex];
        Int32 theBillId = Convert.ToInt32(theRow.Cells[4].Text);
        if (e.CommandName == "Select")
            ShowDetails(theBillId);
        else
            PrintDebitNote(theBillId);

    }

    private void PrintDebitNote(int billid)
    {
        //theReportHeading = "Upcoming ARV Pickup Report";
        theReportSource = "../Reports/rptPatientDebitNote.rpt";
        IPatientHome PatientManager;
        PatientManager =
            (IPatientHome)
            ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
        System.Data.DataSet dataTable = PatientManager.GetPatientDebitNoteDetails(billid, Convert.ToInt32(Session["PatientId"]));
        dataTable.WriteXmlSchema(Server.MapPath("..\\XMLFiles\\PatientDebitNote.xml"));
        rptDocument = new ReportDocument();
        rptDocument.Load(Server.MapPath(theReportSource));
        rptDocument.SetDataSource(dataTable);
        rptDocument.SetParameterValue("BillId", billid.ToString());
        rptDocument.SetParameterValue("Currency", theCurrency.ToString());
        rptDocument.SetParameterValue("FacilityName", Session["AppLocation"].ToString());
        rptDocument.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath("..\\ExcelFiles\\DebitNote.pdf"));
        Response.Redirect("..//ExcelFiles//DebitNote.pdf");

    }

    private void ShowDetails(int billid)
    {
        IPatientHome PatientManager;
        PatientManager =
            (IPatientHome)
            ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
        System.Data.DataSet dataTable = PatientManager.GetPatientDebitNoteDetails(billid, Convert.ToInt32(Session["PatientId"]));
        GridViewTran.DataSource = dataTable.Tables[0];

        GridViewTran.DataBind();
    }

    private void BindGrid()
    {

        BoundField theCol0 = new BoundField();
        theCol0.HeaderText = "Billing Date";
        theCol0.DataField = "VisitDate";
        theCol0.ItemStyle.Width = Unit.Percentage(25);
        theCol0.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
        theCol0.ReadOnly = true;

        BoundField theCol1 = new BoundField();
        theCol1.HeaderText = "Total Cost";
        theCol1.DataField = "TotalCost";
        theCol1.ItemStyle.Width = Unit.Percentage(25);
        theCol1.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
        theCol1.ReadOnly = true;

        ButtonField theBtn = new ButtonField();
        theBtn.HeaderText = "Bill #";
        theBtn.DataTextField = "BillId";
        theBtn.CommandName = "Select";
        theBtn.DataTextFormatString = "{0:000000}";
        theBtn.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
        theBtn.ItemStyle.Width = Unit.Percentage(25);

        ButtonField theBtnprint = new ButtonField();
        theBtnprint.ButtonType = ButtonType.Link;
        theBtnprint.CommandName = "Print";
        theBtnprint.DataTextField = "Print";
        theBtnprint.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
        theBtnprint.Text = "Print";
        theBtnprint.ItemStyle.Width = Unit.Percentage(18);

        BoundField theCol3 = new BoundField();
        theCol3.DataField = "BillId";
        theCol3.ItemStyle.Width = Unit.Percentage(2);
        theCol0.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
        theCol3.ReadOnly = true;

        GridViewSummary.Columns.Add(theCol0);
        GridViewSummary.Columns.Add(theCol1);
        GridViewSummary.Columns.Add(theBtn);
        GridViewSummary.Columns.Add(theBtnprint);
        GridViewSummary.Columns.Add(theCol3);

        GridViewSummary.DataBind();
        GridViewSummary.Columns[4].Visible = false;

    }
    #endregion
    protected void btnShowMother_Click(object sender, EventArgs e)
    {
        Session["PatientId"] = Session["MotherPtnpk"];
        Session["MotherPtnpk"] = "";
        string Url = string.Format("{0}", "../ClinicalForms/frmPatient_Home.aspx");
        Response.Redirect(Url);
    }
    protected void lnkPharmacyNotes_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this.UpdateMasterLink, this.UpdateMasterLink.GetType(), "Popup", "window.open('frmPatient_PharmacyNotes.aspx','popupwindow','toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=yes,resizable=no,width=950,height=650,scrollbars=yes');", true);
    }

    protected void FormDelete()
    {
        Telerik.Web.UI.RadNotification RNot = new Telerik.Web.UI.RadNotification();
        RNot.ID = "RadNotification";
        RNot.VisibleOnPageLoad = true;
        RNot.Position = Telerik.Web.UI.NotificationPosition.Center;
        RNot.Width = 350;
        RNot.Height = 100;
        RNot.Animation = Telerik.Web.UI.NotificationAnimation.Fade;
        RNot.EnableRoundedCorners = true;
        RNot.EnableShadow = true;
        RNot.ForeColor = Color.Red;
        RNot.VisibleTitlebar = false;
        RNot.Text = "Form Deleted Successfully";
        pnlnotification.Controls.Add(RNot);

    }
    protected void grdarvadherence_OnRowDataBound(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < grdarvadherence.Rows.Count; i++)
            {
                double Total = 0.00;
                int j = Convert.ToInt32(grdarvadherence.Columns.Count - 1);
                GridViewRow row = grdarvadherence.Rows[i];
                if (grdarvadherence.Rows.Count >i + 1)
                {
                    GridViewRow NextRow = grdarvadherence.Rows[i + 1];
                    Label lbladherance = row.FindControl("lbladherance") as Label;
                    if (row.Cells[j].RowSpan == 0)
                    {
                        row.Cells[j].ColumnSpan = 5;
                        row.Cells[j].RowSpan = 2;
                    }
                    else
                    {
                        //row.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                    }
                    if ((row.Cells[3].Text != string.Empty) || (row.Cells[3].Text != "&nbsp;"))
                    {
                        Total = (Convert.ToDouble(NextRow.Cells[2].Text) - Convert.ToDouble(row.Cells[3].Text)) / Convert.ToDouble(NextRow.Cells[2].Text);

                        lbladherance.Text = String.Format("{0:P}", Total);
                    }
                    else
                    {
                        lbladherance.Text = "Pill Count not filled";
                    }

                    i = i + 1;
                    if (i >= grdarvadherence.Rows.Count)
                        break;
                }

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    
    }



    protected void grdCotriadherence_OnRowDataBound(object sender, EventArgs e)
    {        
        try
        {
            for (int i = 0; i < grdCotriadherence.Rows.Count; i++)
            {
                double Total = 0.00;
                int j = Convert.ToInt32(grdCotriadherence.Columns.Count - 1);
                GridViewRow row = grdCotriadherence.Rows[i];
                if (grdCotriadherence.Rows.Count>i + 1)
                {
                    GridViewRow NextRow = grdCotriadherence.Rows[i + 1];
                    if (row.Cells[j].RowSpan == 0)
                    {
                        row.Cells[j].RowSpan = 2;
                    }
                    else
                    {
                        row.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                    }
                    if ((NextRow.Cells[3].Text != string.Empty) || (NextRow.Cells[3].Text != "&nbsp;"))
                    {
                        Total = (Convert.ToDouble(row.Cells[2].Text) - Convert.ToDouble(NextRow.Cells[3].Text)) / Convert.ToDouble(row.Cells[2].Text) * 100;
                        grdCotriadherence.Rows[i].Cells[j].Text = Total.ToString("#0.00");
                    }
                    else
                    {
                        grdCotriadherence.Rows[i].Cells[j].Text = "Pill Count not filled";
                    }

                    i = i + 1;
                    if (i >= grdCotriadherence.Rows.Count)
                        break;
                }

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    protected void grdarvadherence_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.BackColor='#666699';");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");
            //e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(grdarvadherence, "Select$" + e.Row.RowIndex.ToString()));

        }
    }
       
}

