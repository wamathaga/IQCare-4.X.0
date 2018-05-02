using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.IO;
using ChartDirector;
using Application.Common;
using Application.Presentation;
using Interface.Security;
using Interface.Reports;
using graph = Microsoft.Office.Interop.Owc11;
using System.Text;

public partial class frmFacilityHome : LogPage
{
    #region "Variable Declaration"
    double theGraphLowCD4, theGraphRecentCD4, ChartChartTypeEnum, theGraphHb = 0.0, theGraphHct = 0.0, theGraphAST = 0.0, theGraphCr = 0.0, Graph = 0.0;
    string month1, month2, month3, month4, month5, month6;
    System.Data.DataSet theFacilityDS;
    System.Data.DataSet theDS;
    System.Data.DataSet theDS1;
    System.Data.DataSet theDS2;
    StringBuilder stringFacility = new StringBuilder();
    
    #endregion

    private void ShowReport(DataTable theTable, string FileName)
    {
        IQWebUtils theUtils = new IQWebUtils();
        theUtils.ExporttoExcel(theTable,Response);
    }

    private void Fill_Dropdown()
    {
        IUser theLocationManager;
        theLocationManager = (IUser)ObjectFactory.CreateInstance("BusinessProcess.Security.BUser, BusinessProcess.Security");
        DataTable theDT = theLocationManager.GetFacilityList();
        ViewState["Facility"] = theDT;
        IQCareUtils theUtils = new IQCareUtils();
        DataView theDV = new DataView(theDT);
        theDV.Sort = "FacilityID";
        DataRowView[] drv = theDV.FindRows(Session["AppLocationId"]);
        if (drv[0].Row.ItemArray[2].ToString() == "1")
        {
            theDV.RowFilter = "Preferred = 1";
            DataTable theDT1 = (DataTable)theUtils.CreateTableFromDataView(theDV);
            DataRow theDR = theDT1.NewRow();
            theDR["FacilityName"] = "All";
            theDR["FacilityId"] = 9999;
            theDR["Preferred"] = 1;
            theDT1.Rows.InsertAt(theDR, 0);
            ddFacility.DataSource = theDT1;
            ddFacility.DataTextField = "FacilityName";
            ddFacility.DataValueField = "FacilityId";
            ddFacility.DataBind();
           
        }
        else
        {
            DataTable theDT1 = (DataTable)theUtils.CreateTableFromDataView(theDV);
            DataRow theDR = theDT1.NewRow();
            theDR["FacilityName"] = "All";
            theDR["FacilityId"] = 9999;
            theDR["Preferred"] = 0;
            theDT1.Rows.InsertAt(theDR, 0);
            ddFacility.DataSource = theDT1;
            ddFacility.DataTextField = "FacilityName";
            ddFacility.DataValueField = "FacilityId";
            ddFacility.DataBind();

            
        }
        ddFacility.SelectedValue = Session["AppLocationId"].ToString();
    }
    private void Init_Form()
    {
      
        IFacility FacilityManager;
        Double thePercent, theResultPercent, theTotalPateint, theTotalPMTCTPatient;
        FacilityManager = (IFacility)ObjectFactory.CreateInstance("BusinessProcess.Security.BFacility, BusinessProcess.Security");
        theDS = FacilityManager.GetFacilityStats(Convert.ToInt32(ddFacility.SelectedValue));
        tblPMTCTCare.Visible = false;
        tblExpInfant.Visible = false;
        tblHIVCare.Visible = false;
        ViewState["theDS"] = theDS;
        FacilityManager = null;
        DataTable dttecareas = new DataTable();
        dttecareas = theDS.Tables[10];

        lblTotalActivePatients.Text = theDS.Tables[0].Rows.Count.ToString();
        lblActiveNonARTPatients.Text = theDS.Tables[1].Rows.Count.ToString();
        lblActiveARTPatients.Text = theDS.Tables[2].Rows.Count.ToString();
        lblCurrentMotherPMTCT.Text = theDS.Tables[3].Rows.Count.ToString();
        lblANC.Text = theDS.Tables[4].Rows.Count.ToString();
        lblLD.Text = theDS.Tables[5].Rows.Count.ToString();
        lblPostnatal.Text = theDS.Tables[6].Rows.Count.ToString();
        lblCurrentTotalExposedInfants.Text = theDS.Tables[7].Rows.Count.ToString();
        lblCurrentPMTCTInfants.Text = theDS.Tables[8].Rows.Count.ToString();
        lblCurrentHIVCareInfants.Text = theDS.Tables[9].Rows.Count.ToString();

        Session["Facility"] = ddFacility.SelectedValue.ToString();
        string FacName = ddFacility.SelectedItem.Text;
        string[] theFacility = FacName.Split(new char[] { '-' });
        
        //GridView objdView = new GridView();
        //objdView.Columns.Clear();
        //objdView.AutoGenerateColumns = true;
        
       

        if (theFacility[0] == "All")
        {
            Session["FacilityName"] = "All Facilities";

        }
        else
        {
            Session["FacilityName"] = Convert.ToString(theDS.Tables[12].Rows[0]["FacilityName"]);
        }

        pnl_FacTexhAreas.Controls.Clear();        
        ////if (ddFacility.SelectedValue != "9999")
        ////{
            int m = 2;
            for (int k = 0; k <= dttecareas.Rows.Count - 1; k++)
            {
                
                //pnl_FacTexhAreas.Controls.Add(new LiteralControl("<table width='100%'><tr>"));
                //pnl_FacTexhAreas.Controls.Add(new LiteralControl("<td align='left'>"));
                //Label theLabelTitle = new Label();
                //theLabelTitle.ID = "Lbl_" + dttecareas.Rows[k]["ModuleName"].ToString() + dttecareas.Rows[k]["FacilityID"].ToString();
                //theLabelTitle.Text = dttecareas.Rows[k]["ModuleName"].ToString();
                //theLabelTitle.Font.Size = 9;
                //theLabelTitle.Font.Bold = true;
                //pnl_FacTexhAreas.Controls.Add(theLabelTitle);
                //pnl_FacTexhAreas.Controls.Add(new LiteralControl("</td></tr>"));
                
                DataRow[] theDR = theDS.Tables[11].Select("ModuleID=" + dttecareas.Rows[k]["ModuleId"].ToString());
                if (theDR.Length > 0)
                {

                    pnl_FacTexhAreas.Controls.Add(new LiteralControl("<table width='97%'><tr>"));
                    pnl_FacTexhAreas.Controls.Add(new LiteralControl("<td align='left'>"));
                    Label theLabelTitle = new Label();
                    theLabelTitle.ID = "Lbl_" + dttecareas.Rows[k]["ModuleName"].ToString() + dttecareas.Rows[k]["FacilityID"].ToString();
                    theLabelTitle.Text = dttecareas.Rows[k]["ModuleName"].ToString();
                    theLabelTitle.Visible = true;
                    theLabelTitle.Font.Size = 9;
                    theLabelTitle.Font.Bold = true;
                    pnl_FacTexhAreas.Controls.Add(theLabelTitle);
                    pnl_FacTexhAreas.Controls.Add(new LiteralControl("</td></tr>"));
                    
                  
                    //int m = 2;
                    for (int j = 0; j <= theDR.Length-1; j++)
                    {                        
                        pnl_FacTexhAreas.Controls.Add(new LiteralControl("<tr><td style='width: 43%; height: 25px;' align='left'>"));
                        Label theLabel = new Label();
                        theLabel.ID = "Lbl_" + (theDR[j]["IndicatorName"].ToString() + dttecareas.Rows[k]["ModuleId"].ToString() + dttecareas.Rows[k]["FacilityID"].ToString());
                        theLabel.Text = theDR[j]["IndicatorName"].ToString();
                        //theLabel.CssClass = "bold pad18";
                        theLabel.Width = 200;
                        theLabel.Font.Size = 9;
                        theLabel.Font.Bold = true;
                        //theLabel.Font.Underline = true;
                        theLabel.ForeColor = System.Drawing.Color.Blue;
                        pnl_FacTexhAreas.Controls.Add(theLabel);
                        pnl_FacTexhAreas.Controls.Add(new LiteralControl("</td><td align='left'>"));




                        if ((theDS.Tables.Count > (13 + m)) && (theDS.Tables[13 + m].Columns.Count > 1 || theDS.Tables[13 + m].Rows.Count > 1))
                        {
                            DataGrid objdView = new DataGrid();
                           
                            objdView.ID = "Dview_" + (theDR[j]["IndicatorName"].ToString() + dttecareas.Rows[k]["ModuleId"].ToString() + dttecareas.Rows[k]["FacilityID"].ToString()) + "_Val";
                            objdView.AutoGenerateColumns = true;
                            objdView.HeaderStyle.Font.Bold=true;
                            objdView.DataSource = theDS.Tables[13+m];
                            pnl_FacTexhAreas.Controls.Add(new LiteralControl("<table width='100%'; height:'25px';>"));
                            pnl_FacTexhAreas.Controls.Add(new LiteralControl("<tr>"));
                            pnl_FacTexhAreas.Controls.Add(new LiteralControl("<td>"));
                            pnl_FacTexhAreas.Controls.Add(new LiteralControl("<div class='gridviewFaclity whitebg';>"));
                            pnl_FacTexhAreas.Controls.Add(objdView);
                            pnl_FacTexhAreas.Controls.Add(new LiteralControl("</div>"));
                            pnl_FacTexhAreas.Controls.Add(new LiteralControl("</td>"));
                            pnl_FacTexhAreas.Controls.Add(new LiteralControl("</tr>"));
                            pnl_FacTexhAreas.Controls.Add(new LiteralControl("</table>"));
                            objdView.Visible = true;
                            objdView.DataBind();

                            

                        }
                        else
                        {
                            Label theValueLabel = new Label();
                            theValueLabel.ID = "Lbl_" + (theDR[j]["IndicatorName"].ToString() + dttecareas.Rows[k]["ModuleId"].ToString() + dttecareas.Rows[k]["FacilityID"].ToString()) + "_Val";
                            //if (theDS.Tables.Count > (13 + m))
                            if (((theDS.Tables.Count > (13 + m)) && (theDS.Tables[13 + m].Rows.Count > 0)))
                            {
                                theValueLabel.Text = theDS.Tables[13 + m].Rows[0][0].ToString();
                            }

                            theValueLabel.CssClass = "rightalign blue";
                            theValueLabel.Width = 50;
                            pnl_FacTexhAreas.Controls.Add(theValueLabel);

                        }
                        m = m + 2;
                    }
            
                    pnl_FacTexhAreas.Controls.Add(new LiteralControl("</td></tr>"));
                    pnl_FacTexhAreas.Controls.Add(new LiteralControl("<br/>"));
                    pnl_FacTexhAreas.Controls.Add(new LiteralControl("</tr></table>"));
                }
            }
            DataView theDV = new DataView(theDS.Tables[13]);
            theDV.RowFilter = "ModuleID=1";
            if (theDV.Count > 0)
            {
                tblPMTCTCare.Visible = true;
                tblExpInfant.Visible = true;
            }
            theDV.RowFilter = "ModuleID=2";
            if (theDV.Count > 0)
            {
                tblHIVCare.Visible = true;
            }
        ////}

        //if (ddFacility.SelectedValue != "9999")
        //{
        //    btnART.Visible = false;
        //    btnPMTCT.Visible = false;
        //}
        //else
        //{
        //    btnART.Visible = true;
        //    btnPMTCT.Visible = true;
        //    btnExposedChildren.Visible = true;
        //}
        

        #region "Fill Details"

        
        
         //theTotalPateint = Convert.ToDouble(theDS.Tables[0].Rows.Count);
       
        ///**********************Caluclate Female Patients *************/
        //Double theTotFemale = Convert.ToDouble(theDS.Tables[1].Rows.Count);
        //thePercent = (theTotFemale / theTotalPateint) * 100;
        //if (theTotalPateint != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}       
        //lblfemalepatient.Text = theDS.Tables[1].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///**********************Calculate Male Patients ***************/
        //Double theTotMale = Convert.ToDouble(theDS.Tables[2].Rows.Count);
        //thePercent = (theTotMale / theTotalPateint) * 100;
        //if (theTotalPateint != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}        
        //lblmalepatient.Text = theDS.Tables[2].Rows.Count + " " + "(" + theResultPercent + "%)";
        //lblTotalPatient.Text = theDS.Tables[0].Rows.Count.ToString();
        //lblActivePatient.Text = theDS.Tables[3].Rows.Count.ToString();

        ///*********************Calculate ARTMortality %  ****************/
        //Double theARTMortality = Convert.ToDouble(theDS.Tables[6].Rows.Count);
        //Double theTotalARTPatient = Convert.ToDouble(theDS.Tables[5].Rows.Count);
        //thePercent = (theARTMortality / theTotalARTPatient) * 100;
        //if (theTotalARTPatient != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lblARTMortality.Text = theDS.Tables[6].Rows.Count + " " + "(" + theResultPercent + "%)";


        ///*********************Calculate ART AND Pateint % ****************/
        //Double theActivePatient = Convert.ToDouble(theDS.Tables[3].Rows.Count);
        //Double theTotArtPatient = Convert.ToDouble(theDS.Tables[5].Rows.Count);

        //thePercent = (theTotArtPatient / theActivePatient) * 100;
        //if (theActivePatient != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lblArtPatients.Text = theDS.Tables[5].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///*********************Calculate NonART AND Pateint % ****************/
        //Double theTotNonArtPatient = Convert.ToDouble(theDS.Tables[4].Rows.Count);
        //thePercent = (theTotNonArtPatient / theActivePatient) * 100;
        //if (theActivePatient != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lblNonARTPatient.Text = theDS.Tables[4].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************ Calculate Non-ART Male Patient % By Age and Sex *************************/
        ///*** Non-ART Male Patient 0-1 *****/
        //Double theNonARTPtn;
        //theNonARTPtn = Convert.ToDouble(theDS.Tables[7].Rows.Count);
        //thePercent = (theNonARTPtn / theTotNonArtPatient) * 100;
        //if (theTotNonArtPatient != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lblNonARTMUpto2.Text = theDS.Tables[7].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///*** Non-ART Male Patient 2-4 *****/
        //theNonARTPtn = Convert.ToDouble(theDS.Tables[8].Rows.Count);
        //thePercent = (theNonARTPtn / theTotNonArtPatient) * 100;
        //if (theTotNonArtPatient != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lblNonARTMUpto4.Text = theDS.Tables[8].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///*** Non-ART Male Patient 5-14 *****/
        //theNonARTPtn = Convert.ToDouble(theDS.Tables[9].Rows.Count);
        //thePercent = (theNonARTPtn / theTotNonArtPatient) * 100;
        //if (theTotNonArtPatient != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lblNonARTMUpto14.Text = theDS.Tables[9].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///*** Non-ART Male Patient 15+ *****/
        //theNonARTPtn = Convert.ToDouble(theDS.Tables[10].Rows.Count);
        //thePercent = (theNonARTPtn / theTotNonArtPatient) * 100;
        //if (theTotNonArtPatient != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lblNonARTMAbove15.Text = theDS.Tables[10].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************ Calculate Non-ART Female Patient % By Age and Sex *************************/
        ///*** Non-ART Female Patient 0-1 *****/
        //theNonARTPtn = Convert.ToDouble(theDS.Tables[11].Rows.Count);
        //thePercent = (theNonARTPtn / theTotNonArtPatient) * 100;
        //if (theTotNonArtPatient != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lblNonARTFUpto2.Text = theDS.Tables[11].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///*** Non-ART Female Patient 2-4 *****/
        //theNonARTPtn = Convert.ToDouble(theDS.Tables[12].Rows.Count);
        //thePercent = (theNonARTPtn / theTotNonArtPatient) * 100;
        //if (theTotNonArtPatient != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lblNonARTFUpto4.Text = theDS.Tables[12].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///*** Non-ART Female Patient 5-14 *****/
        //theNonARTPtn = Convert.ToDouble(theDS.Tables[13].Rows.Count);
        //thePercent = (theNonARTPtn / theTotNonArtPatient) * 100;
        //if (theTotNonArtPatient != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}

        //lblNonARTFUpto14.Text = theDS.Tables[13].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///*** Non-ART Female Patient 15+ *****/
        //theNonARTPtn = Convert.ToDouble(theDS.Tables[14].Rows.Count);
        //thePercent = (theNonARTPtn / theTotNonArtPatient) * 100;
        //if (theTotNonArtPatient != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lblNonARTFAbove15.Text = theDS.Tables[14].Rows.Count + " " + "(" + theResultPercent + "%)";



        ///************************theTotArtPatient Calculate ART Pateint % By Age and Sex *************************/

        //Double theARTPtn, TotARTFMUpto2, TotARTFMUpto5, TotARTFMUpto15, TotARTFMAbove15;

        ///*** ART Male Patient 0-1 *****/
        //theARTPtn = Convert.ToDouble(theDS.Tables[15].Rows.Count);

        //TotARTFMUpto2 = theARTPtn; //For Graph
        //thePercent = (theARTPtn / theTotArtPatient) * 100;
        //if (theTotArtPatient != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lblARTMUpto2.Text = theDS.Tables[15].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///*** ART Male Patient 2-4 *****/
        //theARTPtn = Convert.ToDouble(theDS.Tables[16].Rows.Count);
        //TotARTFMUpto5 = theARTPtn;//For Graph
        //thePercent = (theARTPtn / theTotArtPatient) * 100;
        //if (theTotArtPatient != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lblARTMUpto4.Text = theDS.Tables[16].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///*** ART Male Patient 5-14 *****/
        //theARTPtn = Convert.ToDouble(theDS.Tables[17].Rows.Count);
        //TotARTFMUpto15 = theARTPtn;//For Graph
        //thePercent = (theARTPtn / theTotArtPatient) * 100;
        //if (theTotArtPatient != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lblARTMUpto14.Text = theDS.Tables[17].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///*** ART Male Patient 15+ *****/
        //theARTPtn = Convert.ToDouble(theDS.Tables[18].Rows.Count);
        //TotARTFMAbove15 = theARTPtn; //For Graph
        //thePercent = (theARTPtn / theTotArtPatient) * 100;
        //if (theTotArtPatient != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lblARTMAbove15.Text = theDS.Tables[18].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///*** ART Female Patient 0-1 *****/
        //theARTPtn = Convert.ToDouble(theDS.Tables[19].Rows.Count);
        //TotARTFMUpto2 = TotARTFMUpto2 + theARTPtn;//For Graph
        //thePercent = (theARTPtn / theTotArtPatient) * 100;
        //if (theTotArtPatient != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lblARTFUpto2.Text = theDS.Tables[19].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///*** ART Female Patient 2-4 *****/
        //theARTPtn = Convert.ToDouble(theDS.Tables[20].Rows.Count);
        //TotARTFMUpto5 = TotARTFMUpto5 + theARTPtn;//For Graph
        //thePercent = (theARTPtn / theTotArtPatient) * 100;
        //if (theTotArtPatient != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lblARTFUpto4.Text = theDS.Tables[20].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///*** ART Female Patient 5-14 *****/
        //theARTPtn = Convert.ToDouble(theDS.Tables[21].Rows.Count);
        //TotARTFMUpto15 = TotARTFMUpto15 + theARTPtn;
        //thePercent = (theARTPtn / theTotArtPatient) * 100;
        //if (theTotArtPatient != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lblARTFUpto14.Text = theDS.Tables[21].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///*** ART Female Patient 15+ *****/
        //theARTPtn = Convert.ToDouble(theDS.Tables[22].Rows.Count);
        //TotARTFMAbove15 = TotARTFMAbove15 + theARTPtn;

        //thePercent = (theARTPtn / theTotArtPatient) * 100;
        //if (theTotArtPatient != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lblARTFAbove15.Text = theDS.Tables[22].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Preffered*************************/

        //if (theDS.Tables[23].Rows.Count > 0)
        //{
        //    if (theDS.Tables[23].Rows[0]["Preferred"].ToString() == "1")
        //    {
        //        chkpreferred.Checked = true;
        //    }
        //    else { chkpreferred.Checked = false; }
        //}

        //#region "PMTCT"

        ///************************Cumulative Mothers Ever in PMTCT*************************/
        //lblMothersEverEnroll.Text = theDS.Tables[25].Rows.Count.ToString();

        ///************************Current Mothers in PMTCT*************************/
        //Double theCurrentPMTCTMothers = Convert.ToDouble(theDS.Tables[25].Rows.Count);
        //lblCurrentMothers.Text = theDS.Tables[26].Rows.Count.ToString();

        ///************************Current Number of Women on ARV Prophylaxis *************************/
        ///***ANC***/       
        //lblProANC.Text = theDS.Tables[27].Rows.Count.ToString();
        
        ///***L&D***/
        //lblProLD.Text = theDS.Tables[28].Rows.Count.ToString();
        ///***PN***/
        //lblProPN.Text = theDS.Tables[29].Rows.Count.ToString();

        ///************************Current ANC Mothers ************************************************/
        //Double theCurrentANCMothers = Convert.ToDouble(theDS.Tables[30].Rows.Count);
        //thePercent = (theCurrentANCMothers / theCurrentPMTCTMothers) * 100;
        //if (theCurrentPMTCTMothers != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lblCurrentANCMothers.Text = theDS.Tables[30].Rows.Count + " " + "(" + theResultPercent + "%)";       

        ///************************Current ANC HIV+ Mothers ******************************************/
        //Double theCurrentANCHIVPosMothers = Convert.ToDouble(theDS.Tables[31].Rows.Count);
        //thePercent = (theCurrentANCHIVPosMothers / theCurrentANCMothers) * 100;
        //if (theCurrentANCMothers != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lblANCHIVPosMothers.Text = theDS.Tables[31].Rows.Count + " " + "(" + theResultPercent + "%)";
        
        ///************************Current ANC HIV+ Mothers HIV+ Partner******************************/
        //Double theANCPosMotherPosPartner = Convert.ToDouble(theDS.Tables[32].Rows.Count);
        //thePercent = (theANCPosMotherPosPartner / theCurrentANCHIVPosMothers) * 100;
        //if (theCurrentANCHIVPosMothers != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkPosMotherPosPartner.Text = theDS.Tables[32].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Current ANC HIV+ Mothers HIV- Partner*****************************/
        //Double theANCPosMotherNegPartner = Convert.ToDouble(theDS.Tables[33].Rows.Count);
        //thePercent = (theANCPosMotherNegPartner / theCurrentANCHIVPosMothers) * 100;
        //if (theCurrentANCHIVPosMothers != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkPosMotherNegPartner.Text = theDS.Tables[33].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Current ANC HIV+ Mothers Unknown Partner**************************/
        //Double theANCPosMotherUnknownPartner = Convert.ToDouble(theDS.Tables[34].Rows.Count);
        //thePercent = (theANCPosMotherUnknownPartner / theCurrentANCHIVPosMothers) * 100;
        //if (theCurrentANCHIVPosMothers != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkPosMotherUnknownPartner.Text = theDS.Tables[34].Rows.Count + " " + "(" + theResultPercent + "%)";
        ///************************Current ANC HIV- Mothers ******************************************/
        //Double theCurrentANCHIVNegMothers = Convert.ToDouble(theDS.Tables[35].Rows.Count);
        //thePercent = (theCurrentANCHIVNegMothers / theCurrentANCMothers) * 100;
        //if (theCurrentANCMothers != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lblHIVNegMothers.Text = theDS.Tables[35].Rows.Count + " " + "(" + theResultPercent + "%)";
        ///************************Current ANC HIV- Mothers HIV+ Partner******************************/
        //Double theANCHIVNegMotherPosPartner = Convert.ToDouble(theDS.Tables[36].Rows.Count);
        //thePercent = (theANCHIVNegMotherPosPartner / theCurrentANCHIVNegMothers) * 100;
        //if (theCurrentANCHIVNegMothers != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkNegMotherPosPartner.Text = theDS.Tables[36].Rows.Count + " " + "(" + theResultPercent + "%)";
        ///************************Current ANC HIV- Mothers HIV- Partner*****************************/
        //Double theANCHIVNegMotherNegPartner = Convert.ToDouble(theDS.Tables[37].Rows.Count);
        //thePercent = (theANCHIVNegMotherNegPartner / theCurrentANCHIVNegMothers) * 100;
        //if (theCurrentANCHIVNegMothers != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkNegMotherNegPartner.Text = theDS.Tables[37].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Current ANC HIV- Mothers Unknown Partner**************************/
        //Double theANCHIVNegMotherUnknownPartner = Convert.ToDouble(theDS.Tables[38].Rows.Count);
        //thePercent = (theANCHIVNegMotherUnknownPartner / theCurrentANCHIVNegMothers) * 100;
        //if (theCurrentANCHIVNegMothers != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkNegMotherUnknownPartner.Text = theDS.Tables[38].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Current L&D Mothers*********************************************/
        //Double theCurrentLDMothers = Convert.ToDouble(theDS.Tables[39].Rows.Count);
        //thePercent = (theCurrentLDMothers / theCurrentPMTCTMothers) * 100;
        //if (theCurrentPMTCTMothers != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lblCurrentLDMothers.Text = theDS.Tables[39].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Current L&D HIV+ Mothers ****************************************/
        //Double theCurrentLDHIVPosMothers = Convert.ToDouble(theDS.Tables[40].Rows.Count);
        //thePercent = (theCurrentLDHIVPosMothers / theCurrentLDMothers) * 100;
        //if (theCurrentLDMothers != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lblLDHIVPosMothers.Text = theDS.Tables[40].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Current L&D HIV+ Mothers HIV+ Partner***************************/
        //Double theCurrentLDPosMotherPosPartner = Convert.ToDouble(theDS.Tables[41].Rows.Count);
        //thePercent = (theCurrentLDPosMotherPosPartner / theCurrentLDHIVPosMothers) * 100;
        //if (theCurrentLDHIVPosMothers != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkLDPosMotherPosPartner.Text = theDS.Tables[41].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Current L&D HIV+ Mothers HIV- Partner***************************/
        //Double theCurrentLDPosMotherNegPartner = Convert.ToDouble(theDS.Tables[42].Rows.Count);
        //thePercent = (theCurrentLDPosMotherNegPartner / theCurrentLDHIVPosMothers) * 100;
        //if (theCurrentLDHIVPosMothers != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}        
        //lnkLDPosMotherNegPartner.Text = theDS.Tables[42].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Current L&D HIV+ Mothers Unknown Partner*************************/
        //Double theCurrentLDPosMotherUnknownPartner = Convert.ToDouble(theDS.Tables[43].Rows.Count);
        //thePercent = (theCurrentLDPosMotherUnknownPartner / theCurrentLDHIVPosMothers) * 100;
        //if (theCurrentLDHIVPosMothers != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkLDPosMotherUnknownPartner.Text = theDS.Tables[43].Rows.Count + " " + "(" + theResultPercent + "%)";        

        ///************************Current L&D HIV- Mothers ******************************************/
        //Double theCurrentLDHIVNegMothers = Convert.ToDouble(theDS.Tables[44].Rows.Count);
        //thePercent = (theCurrentLDHIVNegMothers / theCurrentLDMothers) * 100;
        //if (theCurrentLDMothers != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lblLDHIVNegMothers.Text = theDS.Tables[44].Rows.Count + " " + "(" + theResultPercent + "%)";        

        ///************************Current L&D HIV- Mothers HIV+ Partner******************************/
        //Double theCurrentLDNegMotherPosPartner = Convert.ToDouble(theDS.Tables[45].Rows.Count);
        //thePercent = (theCurrentLDNegMotherPosPartner / theCurrentLDHIVNegMothers) * 100;
        //if (theCurrentLDHIVNegMothers != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkLDNegMotherPosPartner.Text = theDS.Tables[45].Rows.Count + " " + "(" + theResultPercent + "%)";                

        ///************************Current L&D HIV- Mothers HIV- Partner*****************************/
        //Double theCurrentLDNegMotherNegPartner = Convert.ToDouble(theDS.Tables[46].Rows.Count);
        //thePercent = (theCurrentLDNegMotherNegPartner / theCurrentLDHIVNegMothers) * 100;
        //if (theCurrentLDHIVNegMothers != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkLDNegMotherNegPartner.Text = theDS.Tables[46].Rows.Count + " " + "(" + theResultPercent + "%)";                       

        ///************************Current L&D HIV- Mothers Unknown Partner**************************/
        //Double theCurrentLDNegMotherUnknownPartner = Convert.ToDouble(theDS.Tables[47].Rows.Count);
        //thePercent = (theCurrentLDNegMotherUnknownPartner / theCurrentLDHIVNegMothers) * 100;
        //if (theCurrentLDHIVNegMothers != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkLDNegMotherUnknownPartner.Text = theDS.Tables[47].Rows.Count + " " + "(" + theResultPercent + "%)";        

        ///************************Current PN Mothers *********************************************/
        //Double theCurrentPNMothers = Convert.ToDouble(theDS.Tables[48].Rows.Count);
        //thePercent = (theCurrentPNMothers / theCurrentPMTCTMothers) * 100;
        //if (theCurrentPMTCTMothers != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lblCurrentPNMothers.Text = theDS.Tables[48].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Current PN HIV+ Mothers ****************************************/
        //Double theCurrentPNHIVPosMothers = Convert.ToDouble(theDS.Tables[49].Rows.Count);
        //thePercent = (theCurrentPNHIVPosMothers / theCurrentPNMothers) * 100;
        //if (theCurrentPNMothers != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lblPNHIVPosMothers.Text = theDS.Tables[49].Rows.Count + " " + "(" + theResultPercent + "%)";        

        ///************************Current PN HIV+ Mothers HIV+ Partner***************************/
        //Double theCurrentPNPosMotherPosPartner = Convert.ToDouble(theDS.Tables[50].Rows.Count);
        //thePercent = (theCurrentPNPosMotherPosPartner / theCurrentPNHIVPosMothers) * 100;
        //if (theCurrentPNHIVPosMothers != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkPNPosMotherPosPartner.Text = theDS.Tables[50].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Current PN HIV+ Mothers HIV- Partner***************************/
        //Double theCurrentPNPosMotherNegPartner = Convert.ToDouble(theDS.Tables[51].Rows.Count);
        //thePercent = (theCurrentPNPosMotherNegPartner / theCurrentPNHIVPosMothers) * 100;
        //if (theCurrentPNHIVPosMothers != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkPNPosMotherNegPartner.Text = theDS.Tables[51].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Current PN HIV+ Mothers Unknown Partner*************************/
        //Double theCurrentPNPosMotherUnknownPartner = Convert.ToDouble(theDS.Tables[52].Rows.Count);
        //thePercent = (theCurrentPNPosMotherUnknownPartner / theCurrentPNHIVPosMothers) * 100;
        //if (theCurrentPNHIVPosMothers != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkPNPosMotherUnknownPartner.Text = theDS.Tables[52].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Current PN HIV- Mothers ****************************************/
        //Double theCurrentPNHIVNegMothers = Convert.ToDouble(theDS.Tables[53].Rows.Count);
        //thePercent = (theCurrentPNHIVNegMothers / theCurrentPNMothers) * 100;
        //if (theCurrentPNMothers != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lblPNHIVNegMothers.Text = theDS.Tables[53].Rows.Count + " " + "(" + theResultPercent + "%)";        

        ///************************Current PN HIV- Mothers HIV+ Partner***************************/
        //Double theCurrentPNNegMotherPosPartner = Convert.ToDouble(theDS.Tables[54].Rows.Count);
        //thePercent = (theCurrentPNNegMotherPosPartner / theCurrentPNHIVNegMothers) * 100;
        //if (theCurrentPNHIVNegMothers != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkPNNegMotherPosPartner.Text = theDS.Tables[54].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Current PN HIV- Mothers HIV- Partner***************************/
        //Double theCurrentPNNegMotherNegPartner = Convert.ToDouble(theDS.Tables[55].Rows.Count);
        //thePercent = (theCurrentPNNegMotherNegPartner / theCurrentPNHIVNegMothers) * 100;
        //if (theCurrentPNHIVNegMothers != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkPNNegMotherNegPartner.Text = theDS.Tables[55].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Current PN HIV- Mothers Unknown Partner*************************/
        //Double theCurrentPNNegMotherUnknownPartner = Convert.ToDouble(theDS.Tables[56].Rows.Count);
        //thePercent = (theCurrentPNNegMotherUnknownPartner / theCurrentPNHIVNegMothers) * 100;
        //if (theCurrentPNHIVNegMothers != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkPNNegMotherUnknownPartner.Text = theDS.Tables[56].Rows.Count + " " + "(" + theResultPercent + "%)";

        
        #endregion
        //#region "Exposed Infants"

        ///****Cumulative Exposed Infants*****************************************************/        
        //lblExposedInfants.Text = theDS.Tables[57].Rows.Count.ToString();

        ///****Current Total Exposed Infants*****************************************************/        
        //lblCurrentExposedInfants.Text = theDS.Tables[58].Rows.Count.ToString();
        
        ///****Current PMTCT Infants*****************************************************/
        //lblCurrentPMTCTInfants.Text = theDS.Tables[59].Rows.Count.ToString();

        ///****Current HIV Care Infants*****************************************************/
        //lblCurrentHIVCareInfants.Text = theDS.Tables[60].Rows.Count.ToString();

        ///************************Age < 2 Months (PCR) *************************/
        //Double thePCRLessthan2months = Convert.ToDouble(theDS.Tables[61].Rows.Count);
        //lblPCRLessthan2months.Text = theDS.Tables[61].Rows.Count.ToString();

        ///************************Age < 2 Months--Number/Percent Tested(PCR) *************************/
        //Double thePercentTestedResult = Convert.ToDouble(theDS.Tables[62].Rows.Count);
        //thePercent = (thePercentTestedResult / thePCRLessthan2months) * 100;
        //if (thePCRLessthan2months != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lblPercentTestedResult.Text = theDS.Tables[62].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Age < 2 Months --Number/Percent Tested (PCR)--HIV+ *************************/
        //Double theTotalHIVPos = Convert.ToDouble(theDS.Tables[63].Rows.Count);
        //thePercent = (theTotalHIVPos / thePercentTestedResult) * 100;
        //if (thePercentTestedResult != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lblTotalHIVPos.Text = theDS.Tables[63].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Age < 2 Months --Number/Percent Tested (PCR)--HIV- *************************/
        //Double theTotalHIVNeg = Convert.ToDouble(theDS.Tables[64].Rows.Count);
        //thePercent = (theTotalHIVNeg / thePercentTestedResult) * 100;
        //if (thePercentTestedResult != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lblTotalHIVNeg.Text = theDS.Tables[64].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Age < 2 Months --Number/Percent Tested (PCR)--Indeterminate *************************/
        //Double theIndeterminateTested = Convert.ToDouble(theDS.Tables[65].Rows.Count);
        //thePercent = (theIndeterminateTested / thePercentTestedResult) * 100;
        //if (thePercentTestedResult != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lblIndeterminateTested.Text = theDS.Tables[65].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Age < 2 Months --Number/Percent Tested (PCR)--HIV+(EBF)*************************/
        //Double theHIVPosEBFlessthan2 = Convert.ToDouble(theDS.Tables[66].Rows.Count);
        //thePercent = (theHIVPosEBFlessthan2 / theTotalHIVPos) * 100;
        //if (theTotalHIVPos != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkHIVPosEBFlessthan2.Text = theDS.Tables[66].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Age < 2 Months --Number/Percent Tested (PCR)--HIV+(EBMS)*************************/
        //Double theHIVPosRFlessthan2 = Convert.ToDouble(theDS.Tables[67].Rows.Count);
        //thePercent = (theHIVPosRFlessthan2 / theTotalHIVPos) * 100;
        //if (theTotalHIVPos != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}        
        //lnkHIVPosRFlessthan2.Text = theDS.Tables[67].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Age < 2 Months --Number/Percent Tested (PCR)--HIV+(MF)*************************/
        //Double theHIVPosMFlessthan2 = Convert.ToDouble(theDS.Tables[68].Rows.Count);
        //thePercent = (theHIVPosMFlessthan2 / theTotalHIVPos) * 100;
        //if (theTotalHIVPos != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkHIVPosMFlessthan2.Text = theDS.Tables[68].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Age < 2 Months --Number/Percent Tested (PCR)--HIV+(Other)*************************/
        //Double theHIVPosOtherlessthan2 = Convert.ToDouble(theDS.Tables[69].Rows.Count);
        //thePercent = (theHIVPosOtherlessthan2 / theTotalHIVPos) * 100;
        //if (theTotalHIVPos != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkHIVPosOtherlessthan2.Text = theDS.Tables[69].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Age < 2 Months --Number/Percent Tested (PCR)--HIV-(EBF)*************************/
        //Double theHIVNegEBFlessthan2 = Convert.ToDouble(theDS.Tables[70].Rows.Count);
        //thePercent = (theHIVNegEBFlessthan2 / theTotalHIVNeg) * 100;
        //if (theTotalHIVNeg != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkHIVNegEBFlessthan2.Text = theDS.Tables[70].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Age < 2 Months --Number/Percent Tested (PCR)--HIV-(EBMS)*************************/
        //Double theHIVNegRFlessthan2 = Convert.ToDouble(theDS.Tables[71].Rows.Count);
        //thePercent = (theHIVNegRFlessthan2 / theTotalHIVNeg) * 100;
        //if (theTotalHIVNeg != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkHIVNegRFlessthan2.Text = theDS.Tables[71].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Age < 2 Months --Number/Percent Tested (PCR)--HIV-(MF)*************************/
        //Double theHIVNegMFlessthan2 = Convert.ToDouble(theDS.Tables[72].Rows.Count);
        //thePercent = (theHIVNegMFlessthan2 / theTotalHIVNeg) * 100;
        //if (theTotalHIVNeg != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkHIVNegMFlessthan2.Text = theDS.Tables[72].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Age < 2 Months --Number/Percent Tested (PCR)--HIV-(Other)*************************/
        //Double theHIVNegOtherlessthan2 = Convert.ToDouble(theDS.Tables[73].Rows.Count);
        //thePercent = (theHIVNegOtherlessthan2 / theTotalHIVNeg) * 100;
        //if (theTotalHIVNeg != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkHIVNegOtherlessthan2.Text = theDS.Tables[73].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Age < 2 Months --Number/Percent Tested (PCR)--Indeterminate(EBF)*************************/
        //Double theIndeterminateEBFlessthan2 = Convert.ToDouble(theDS.Tables[74].Rows.Count);
        //thePercent = (theIndeterminateEBFlessthan2 / theIndeterminateTested) * 100;
        //if (theIndeterminateTested != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkIndeterminateEBFlessthan2.Text = theDS.Tables[74].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Age < 2 Months --Number/Percent Tested (PCR)--Indeterminate(EBMS)*************************/
        //Double theIndeterminateRFlessthan2 = Convert.ToDouble(theDS.Tables[75].Rows.Count);
        //thePercent = (theIndeterminateRFlessthan2 / theIndeterminateTested) * 100;
        //if (theIndeterminateTested != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkIndeterminateRFlessthan2.Text = theDS.Tables[75].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Age < 2 Months --Number/Percent Tested (PCR)--Indeterminate(RF)*************************/
        //Double theIndeterminateMFlessthan2 = Convert.ToDouble(theDS.Tables[76].Rows.Count);
        //thePercent = (theIndeterminateMFlessthan2 / theIndeterminateTested) * 100;
        //if (theIndeterminateTested != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkIndeterminateMFlessthan2.Text = theDS.Tables[76].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Age < 2 Months --Number/Percent Tested (PCR)--Indeterminate(Other)*************************/
        //Double theIndeterminateOtherlessthan2 = Convert.ToDouble(theDS.Tables[77].Rows.Count);
        //thePercent = (theIndeterminateOtherlessthan2 / theIndeterminateTested) * 100;
        //if (theIndeterminateTested != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkIndeterminateOtherlessthan2.Text = theDS.Tables[77].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Age >= 2 and Age < 12 Months (PCR) *************************/
        //Double thePCR2to12months = Convert.ToDouble(theDS.Tables[78].Rows.Count);
        //lblPCR2to12months.Text = theDS.Tables[78].Rows.Count.ToString();

        ///************************Age >= 2 and Age < 12 Months--Number/Percent Tested(PCR) *************************/
        //Double thePercentTested2to12PCR = Convert.ToDouble(theDS.Tables[79].Rows.Count);
        //thePercent = (thePercentTested2to12PCR / thePCR2to12months) * 100;
        //if (thePCR2to12months != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lblPercentTested2to12PCR.Text = theDS.Tables[79].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Age >= 2 and Age < 12 Months --Number/Percent Tested (PCR)--HIV+ *************************/
        //Double theHIVPos2to12 = Convert.ToDouble(theDS.Tables[80].Rows.Count);
        //thePercent = (theHIVPos2to12 / thePercentTested2to12PCR) * 100;
        //if (thePercentTested2to12PCR != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lblTotalHIVPos2to12.Text = theDS.Tables[80].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Age >= 2 and Age < 12 Months --Number/Percent Tested (PCR)--HIV- *************************/
        //Double theTotalHIVNeg2to12 = Convert.ToDouble(theDS.Tables[81].Rows.Count);
        //thePercent = (theTotalHIVNeg2to12 / thePercentTested2to12PCR) * 100;
        //if (thePercentTested2to12PCR != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lblTotalHIVNeg2to12.Text = theDS.Tables[81].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Age >= 2 and Age <12 Months --Number/Percent Tested (PCR)--Indeterminate *************************/
        //Double theIndeterminateTested2to12 = Convert.ToDouble(theDS.Tables[82].Rows.Count);
        //thePercent = (theIndeterminateTested2to12 / thePercentTested2to12PCR) * 100;
        //if (thePercentTested2to12PCR != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lblIndeterminateTested2to12.Text = theDS.Tables[82].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Age >= 2 and Age <12 Months --Number/Percent Tested (PCR)--HIV+(EBF)*************************/
        //Double theHIVPosEBF2to12 = Convert.ToDouble(theDS.Tables[83].Rows.Count);
        //thePercent = (theHIVPosEBF2to12 / theHIVPos2to12) * 100;
        //if (theHIVPos2to12 != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkHIVPosEBF2to12.Text = theDS.Tables[83].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Age >= 2 and Age <12 Months --Number/Percent Tested (PCR)--HIV+(EBMS)*************************/
        //Double theHIVPosRF2to12 = Convert.ToDouble(theDS.Tables[84].Rows.Count);
        //thePercent = (theHIVPosRF2to12 / theHIVPos2to12) * 100;
        //if (theHIVPos2to12 != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkHIVPosRF2to12.Text = theDS.Tables[84].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Age >= 2 and Age <12 Months --Number/Percent Tested (PCR)--HIV+(MF)*************************/
        //Double theHIVPosMF2to12 = Convert.ToDouble(theDS.Tables[85].Rows.Count);
        //thePercent = (theHIVPosMF2to12 / theHIVPos2to12) * 100;
        //if (theHIVPos2to12 != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkHIVPosMF2to12.Text = theDS.Tables[85].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Age >= 2 and Age <12 Months --Number/Percent Tested (PCR)--HIV+(Other)*************************/
        //Double theHIVPosOther2to12 = Convert.ToDouble(theDS.Tables[86].Rows.Count);
        //thePercent = (theHIVPosOther2to12 / theHIVPos2to12) * 100;
        //if (theHIVPos2to12 != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkHIVPosOther2to12.Text = theDS.Tables[86].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Age >= 2 and Age <12 Months  --Number/Percent Tested (PCR)--HIV-(EBF)*************************/
        //Double theHIVNegEBF2to12 = Convert.ToDouble(theDS.Tables[87].Rows.Count);
        //thePercent = (theHIVNegEBF2to12 / theTotalHIVNeg2to12) * 100;
        //if (theTotalHIVNeg2to12 != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkHIVNegEBF2to12.Text = theDS.Tables[87].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Age >= 2 and Age <12 Months  --Number/Percent Tested (PCR)--HIV-(EBMS)*************************/
        //Double theHIVNegRF2to12 = Convert.ToDouble(theDS.Tables[88].Rows.Count);
        //thePercent = (theHIVNegRF2to12 / theTotalHIVNeg2to12) * 100;
        //if (theTotalHIVNeg2to12 != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkHIVNegRF2to12.Text = theDS.Tables[88].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Age >= 2 and Age <12 Months  --Number/Percent Tested (PCR)--HIV-(MF)*************************/
        //Double theHIVNegMF2to12 = Convert.ToDouble(theDS.Tables[89].Rows.Count);
        //thePercent = (theHIVNegMF2to12 / theTotalHIVNeg2to12) * 100;
        //if (theTotalHIVNeg2to12 != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkHIVNegMF2to12.Text = theDS.Tables[89].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Age >= 2 and Age <12 Months --Number/Percent Tested (PCR)--HIV-(Other)*************************/
        //Double theHIVNegOther2to12 = Convert.ToDouble(theDS.Tables[90].Rows.Count);
        //thePercent = (theHIVNegOther2to12 / theTotalHIVNeg2to12) * 100;
        //if (theTotalHIVNeg2to12 != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkHIVNegOther2to12.Text = theDS.Tables[90].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Age >= 2 and Age <12 Months --Number/Percent Tested (PCR)--Indeterminate(EBF)*************************/
        //Double theIndeterminateEBF2to12 = Convert.ToDouble(theDS.Tables[91].Rows.Count);
        //thePercent = (theIndeterminateEBF2to12 / theIndeterminateTested2to12) * 100;
        //if (theIndeterminateTested2to12 != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkIndeterminateEBF2to12.Text = theDS.Tables[91].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Age >= 2 and Age <12 Months --Number/Percent Tested (PCR)--Indeterminate(EBMS)*************************/
        //Double theIndeterminateRF2to12 = Convert.ToDouble(theDS.Tables[92].Rows.Count);
        //thePercent = (theIndeterminateRF2to12 / theIndeterminateTested2to12) * 100;
        //if (theIndeterminateTested2to12 != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkIndeterminateRF2to12.Text = theDS.Tables[92].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Age >= 2 and Age <12 Months --Number/Percent Tested (PCR)--Indeterminate(MF)*************************/
        //Double theIndeterminateMF2to12 = Convert.ToDouble(theDS.Tables[93].Rows.Count);
        //thePercent = (theIndeterminateMF2to12 / theIndeterminateTested2to12) * 100;
        //if (theIndeterminateTested2to12 != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkIndeterminateMF2to12.Text = theDS.Tables[93].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Age >= 2 and Age <12 Months --Number/Percent Tested (PCR)--Indeterminate(Other)*************************/
        //Double theIndeterminateOther2to12 = Convert.ToDouble(theDS.Tables[94].Rows.Count);
        //thePercent = (theIndeterminateOther2to12 / theIndeterminateTested2to12) * 100;
        //if (theIndeterminateTested2to12 != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkIndeterminateOther2to12.Text = theDS.Tables[94].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Age >= 18 and Age < 24 Months (PCR) *************************/
        //Double the18to24MonthPCR = Convert.ToDouble(theDS.Tables[95].Rows.Count);
        //lbl18to24RConfirm.Text = theDS.Tables[95].Rows.Count.ToString();

        ///************************Age < 2 Months--Number/Percent Tested(PCR) *************************/
        //Double thePercentTested18to24months = Convert.ToDouble(theDS.Tables[96].Rows.Count);
        //thePercent = (thePercentTested18to24months / the18to24MonthPCR) * 100;
        //if (the18to24MonthPCR != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lblPercentTested18to24months.Text = theDS.Tables[96].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Age >= 18 and Age < 24 Months --Number/Percent Tested (PCR)--HIV+ *************************/
        //Double theTotalHIVPos18to24 = Convert.ToDouble(theDS.Tables[97].Rows.Count);
        //thePercent = (theHIVPos2to12 / thePercentTested18to24months) * 100;
        //if (thePercentTested18to24months != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lblTotalHIVPos18to24.Text = theDS.Tables[97].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Age >= 18 and Age < 24 Months --Number/Percent Tested (PCR)--HIV- *************************/
        //Double theTotalHIVNeg18to24 = Convert.ToDouble(theDS.Tables[98].Rows.Count);
        //thePercent = (theTotalHIVNeg18to24 / thePercentTested18to24months) * 100;
        //if (thePercentTested18to24months != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lblTotalHIVNeg18to24.Text = theDS.Tables[98].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Age >= 18 and Age < 24 Months Months --Number/Percent Tested (PCR)--Indeterminate *************************/
        //Double theIndeterminateTested18to24 = Convert.ToDouble(theDS.Tables[99].Rows.Count);
        //thePercent = (theIndeterminateTested18to24 / thePercentTested18to24months) * 100;
        //if (thePercentTested18to24months != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lblIndeterminateTested18to24.Text = theDS.Tables[99].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Age >= 18 and Age < 24 Months --Number/Percent Tested (PCR)--HIV+(EBF)*************************/
        //Double theHIVPosEBF18to24 = Convert.ToDouble(theDS.Tables[100].Rows.Count);
        //thePercent = (theHIVPosEBF18to24 / theTotalHIVPos18to24) * 100;
        //if (theTotalHIVPos18to24 != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkHIVPosEBF18to24.Text = theDS.Tables[100].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Age >= 18 and Age < 24 Months --Number/Percent Tested (PCR)--HIV+(EBMS)*************************/
        //Double theHIVPosRF18to24 = Convert.ToDouble(theDS.Tables[101].Rows.Count);
        //thePercent = (theHIVPosRF18to24 / theTotalHIVPos18to24) * 100;
        //if (theTotalHIVPos18to24 != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkHIVPosRF18to24.Text = theDS.Tables[101].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Age >= 18 and Age < 24 Months --Number/Percent Tested (PCR)--HIV+(MF)*************************/
        //Double theHIVPosMF18to24 = Convert.ToDouble(theDS.Tables[102].Rows.Count);
        //thePercent = (theHIVPosMF18to24 / theTotalHIVPos18to24) * 100;
        //if (theTotalHIVPos18to24 != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkHIVPosMF18to24.Text = theDS.Tables[102].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Age >= 18 and Age < 24 Months --Number/Percent Tested (PCR)--HIV+(Other)*************************/
        //Double theHIVPosOther18to24 = Convert.ToDouble(theDS.Tables[103].Rows.Count);
        //thePercent = (theHIVPosOther18to24 / theTotalHIVPos18to24) * 100;
        //if (theTotalHIVPos18to24 != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkHIVPosOther18to24.Text = theDS.Tables[103].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Age >= 18 and Age < 24 Months --Number/Percent Tested (PCR)--HIV-(EBF)*************************/
        //Double theHIVNegEBF18to24 = Convert.ToDouble(theDS.Tables[104].Rows.Count);
        //thePercent = (theHIVNegEBF18to24 / theTotalHIVNeg18to24) * 100;
        //if (theTotalHIVNeg18to24 != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkHIVNegEBF18to24.Text = theDS.Tables[104].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Age >= 18 and Age < 24 Months--Number/Percent Tested (PCR)--HIV-(EBMS)*************************/
        //Double theHIVNegRF18to24 = Convert.ToDouble(theDS.Tables[105].Rows.Count);
        //thePercent = (theHIVNegRF18to24 / theTotalHIVNeg18to24) * 100;
        //if (theTotalHIVNeg18to24 != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkHIVNegRF18to24.Text = theDS.Tables[105].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Age >= 18 and Age < 24 Months--Number/Percent Tested (PCR)--HIV-(MF)*************************/
        //Double theHIVNegMF18to24 = Convert.ToDouble(theDS.Tables[106].Rows.Count);
        //thePercent = (theHIVNegMF18to24 / theTotalHIVNeg18to24) * 100;
        //if (theTotalHIVNeg18to24 != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkHIVNegMF18to24.Text = theDS.Tables[106].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Age >= 18 and Age < 24 Months--Number/Percent Tested (PCR)--HIV-(Other)*************************/
        //Double theHIVNegOther18to24 = Convert.ToDouble(theDS.Tables[107].Rows.Count);
        //thePercent = (theHIVNegOther18to24 / theTotalHIVNeg18to24) * 100;
        //if (theTotalHIVNeg18to24 != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkHIVNegOther18to24.Text = theDS.Tables[107].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Age >= 18 and Age < 24 Months--Number/Percent Tested (PCR)--Indeterminate(EBF)*************************/
        //Double theIndeterminateEBF18to24 = Convert.ToDouble(theDS.Tables[108].Rows.Count);
        //thePercent = (theIndeterminateEBF18to24 / theIndeterminateTested18to24) * 100;
        //if (theIndeterminateTested18to24 != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkIndeterminateEBF18to24.Text = theDS.Tables[108].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Age >= 18 and Age < 24 Months--Number/Percent Tested (PCR)--Indeterminate(EBMS)*************************/
        //Double theIndeterminateRF18to24 = Convert.ToDouble(theDS.Tables[109].Rows.Count);
        //thePercent = (theIndeterminateRF2to12 / theIndeterminateRF18to24) * 100;
        //if (theIndeterminateTested18to24 != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkIndeterminateRF18to24.Text = theDS.Tables[109].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Age >= 18 and Age < 24 Months--Number/Percent Tested (PCR)--Indeterminate(MF)*************************/
        //Double theIndeterminateMF18to24 = Convert.ToDouble(theDS.Tables[110].Rows.Count);
        //thePercent = (theIndeterminateMF18to24 / theIndeterminateTested18to24) * 100;
        //if (theIndeterminateTested18to24 != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkIndeterminateMF18to24.Text = theDS.Tables[110].Rows.Count + " " + "(" + theResultPercent + "%)";

        ///************************Age >= 18 and Age < 24 Months--Number/Percent Tested (PCR)--Indeterminate(Other)*************************/
        //Double theIndeterminateOther18to24 = Convert.ToDouble(theDS.Tables[111].Rows.Count);
        //thePercent = (theIndeterminateOther18to24 / theIndeterminateTested18to24) * 100;
        //if (theIndeterminateTested18to24 != 0)
        //{
        //    theResultPercent = System.Math.Round(thePercent);
        //}
        //else
        //{
        //    theResultPercent = 0;
        //}
        //lnkIndeterminateOther18to24.Text = theDS.Tables[111].Rows.Count + " " + "(" + theResultPercent + "%)";
        
        //#endregion

        ///************************Cumulative ARV Prophylaxis *************************/        
        //lblInfantsARVProphylaxis.Text = theDS.Tables[112].Rows.Count.ToString();

        ///************************Current ARV Prophylaxis *************************/
        //lblInfantsCurrentProphylaxis.Text = theDS.Tables[113].Rows.Count.ToString();

        ///************************Cumulative ARV Treatment *************************/
        //lblInfantsCumulativeARV.Text = theDS.Tables[114].Rows.Count.ToString();

        ///************************Current ARV Treatment *************************/
        //lblInfantsCurrentARV.Text = theDS.Tables[115].Rows.Count.ToString();

        ///**********Cotrimoxizole Prophylaxis-Cumulative Started < 2 Months*************************/
        //lblContrimProCumulessthan2.Text = theDS.Tables[116].Rows.Count.ToString();

        ///*********Cotrimoxizole Prophylaxis-Current Started < 2 Months*************************/
        //lblContrimProCurrentlessthan2.Text = theDS.Tables[117].Rows.Count.ToString();

        ///********Cotrimoxizole Prophylaxis-Cumulative Started 2-24 Months*************************/
        //lblContrimProCumu2to24.Text = theDS.Tables[118].Rows.Count.ToString();

        ///*******Cotrimoxizole Prophylaxis-Current Started < 2-24 Months*************************/
        //lblContrimProCurrent2to24.Text = theDS.Tables[119].Rows.Count.ToString();
        
        
        //#endregion
        ///* PieChart by Jayanta Kr. Das */
       
        //Chart.setLicenseCode("DEVP-2AC2-336W-54FM-EAB2-F8E2");
       
        //PieChart(theTotFemale, theTotMale);
     
        //ArtNonArtPieChart(theTotArtPatient, theTotNonArtPatient);
     
        //PerArtAge(TotARTFMUpto2, TotARTFMUpto5, TotARTFMUpto15, TotARTFMAbove15, theTotArtPatient);
       
    }

    void objdView_PreRender(object sender, EventArgs e)
    {
        GridView theGView = (GridView)sender;
        theGView.DataBind();
    }

    private void AuditTrailNotificationonfailure()
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
        RNot.Text = "Audit Trail information is not transfering to MYSQL. please check connection";
        pnlnotification.Controls.Add(RNot);
    }

    private void AuditTrailNotificationbackup()
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
        RNot.Text = "Audit Trail information is backedup";
        pnlnotification.Controls.Add(RNot);
    }

    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
        {
            IQCareMsgBox.Show("SessionExpired", this);
            Response.Redirect("~/frmlogin.aspx",true);
        }
        GetSQLAuditData();
        //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Facility Home";
        //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Visible = false;
        IQWebUtils Utils = new IQWebUtils();
        Utils.HidePatientLevelheader();
        //(Master.FindControl("lblheaderfacility") as Label).Visible = false;
        //(Master.FindControl("lblheader") as Label).Text = "Facility Home";
        try
        {
            
            if (IsPostBack != true)
            {
                Session["PatientId"] = 0;
                ViewState["Facility"] = null;
                AuthenticationManager Authentiaction = new AuthenticationManager();
                if (Authentiaction.HasFunctionRight(ApplicationAccess.Schedular, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
                {
                    DirectScheduler.Visible = false;
                    MissedScheduler.Visible = false;
                }
                Fill_Dropdown();
                Init_Form();
                string theUrl;
                theUrl = string.Format("{0}&AppointmentStatus={1}", "./Scheduler/frmScheduler_AppointmentMain.aspx?name=Add", "All");
                DirectScheduler.HRef = theUrl;
                theUrl = string.Format("{0}&AppointmentStatus={1}", "./Scheduler/frmScheduler_AppointmentMain.aspx?name=Add", "Missed");
                MissedScheduler.HRef = theUrl;
            }

        }
        catch (Exception err)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
        }
    }
    protected void ddFacility_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Init_Form();
            
            
        }
        catch (Exception err)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
        }
    }

    
    //void PieChart(double TotFemale, double TotMale)
    //{
    //    if (Convert.ToInt32(TotFemale) < 1 || Convert.ToInt32(TotMale) < 1)
    //        return;

    //    graph.ChartSpace objCSpace = new graph.ChartSpaceClass();
    //    graph.ChChart objChart = objCSpace.Charts.Add(0);
    //    objChart.Type = graph.ChartChartTypeEnum.chChartTypeDoughnutExploded;
    //    objCSpace.Charts[0].ChartDepth = 125;
    //    objCSpace.Charts[0].AspectRatio = 80;
    //    //objCSpace.Charts[0].SeriesCollection[0].Interior.Color = "Green";  
    //    objChart.HasLegend = false;
    //    objCSpace.Charts[0].SeriesCollection.Add(0);
    //    objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection.Add();
    //    objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection[0].HasPercentage = true;
    //    objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection[0].HasValue = false;
    //    objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection[0].HasCategoryName = true;
    //    objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection[0].Separator = "\n";
    //    objCSpace.Charts[0].SeriesCollection[0].Caption = String.Empty;
    //    objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection[0].Font.Name = "verdana";
    //    objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection[0].Font.Size = 7;
    //    objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection[0].Font.Bold = false;
    //    objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection[0].Font.Color = "Black";
    //    objChart.HasTitle = true;
    //    objChart.Title.Caption = "";
    //    objChart.HasLegend = true;
    //    //objChart.Axes[0].HasTitle = true;
    //    //objChart.Axes[0].Title.Caption = "Male Axis Caption";
    //    //objChart.Axes[1].HasTitle = true;
    //    //objChart.Axes[1].Title.Caption = "Female Axis Caption";
    //    string strSeriesName = "Person 1";
    //    double Total = TotFemale + TotMale;
    //    string strValue = Math.Round(((TotFemale / Total) * 100)).ToString() + '\t' + Math.Round(((TotMale / Total) * 100)).ToString();
    //    string strCategory = "Female" + '\t' + "Male";
    //    objChart.SeriesCollection[0].SetData(graph.ChartDimensionsEnum.chDimSeriesNames, (int)graph.ChartSpecialDataSourcesEnum.chDataLiteral, strSeriesName);
    //    objChart.SeriesCollection[0].SetData(graph.ChartDimensionsEnum.chDimCategories, (int)graph.ChartSpecialDataSourcesEnum.chDataLiteral, strCategory);
    //    objChart.SeriesCollection[0].SetData(graph.ChartDimensionsEnum.chDimValues, (int)graph.ChartSpecialDataSourcesEnum.chDataLiteral, strValue);
        
    //    objChart.Border.Color = "#33CC33";
    //    objChart.SeriesCollection[0].Points[0].Interior.Color = "dda0dd";
    //    objChart.SeriesCollection[0].Points[1].Interior.Color = "eee8aa";
    //    string strAbsolutePath = (Server.MapPath(".")) + "\\Images\\Piechart.GIF";
    //    objCSpace.ExportPicture(strAbsolutePath, "GIF", 300, 200);
    //    string strRelativePath = ".\\Images\\Piechart.gif";
    //    string strImageTag = "<IMG SRC='" + strRelativePath + "'/>";
    //   ChartHolder.Controls.Add(new LiteralControl(strImageTag));
    //}
    //void ArtNonArtPieChart(double TotArtPatient, double TotNonArtPatient)
    //{

    //    if (Convert.ToInt32(TotArtPatient) < 1 || Convert.ToInt32(TotNonArtPatient) < 1)
    //        return;

    //    graph.ChartSpace objCSpace = new graph.ChartSpaceClass();
    //    graph.ChChart objChart = objCSpace.Charts.Add(0);
    //    objChart.Type = graph.ChartChartTypeEnum.chChartTypeDoughnutExploded;
    //    objCSpace.Charts[0].ChartDepth = 125;
    //    objCSpace.Charts[0].AspectRatio = 80;
    //    objChart.HasLegend = false;
    //    objCSpace.Charts[0].SeriesCollection.Add(0);
    //    objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection.Add();
    //    objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection[0].HasPercentage = true;
    //    objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection[0].HasValue = false;
    //    objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection[0].HasCategoryName = true;
    //    objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection[0].Separator = "\n";
    //    objCSpace.Charts[0].SeriesCollection[0].Caption = String.Empty;
    //    objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection[0].Font.Name = "verdana";
    //    objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection[0].Font.Size = 7;
    //    objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection[0].Font.Bold = false ;
    //    objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection[0].Font.Color = "Black";
    //    objChart.HasTitle = true;
    //    objChart.Title.Caption = "";
    //    objChart.HasLegend = true;
    //    //objChart.Axes[0].HasTitle = true;
    //    //objChart.Axes[0].Title.Caption = "Male Axis Caption";
    //    //objChart.Axes[1].HasTitle = true;
    //    //objChart.Axes[1].Title.Caption = "Female Axis Caption";
    //    string strSeriesName = "Person 1";
    //    double Total = TotArtPatient + TotNonArtPatient;

    //    string strValue = Math.Round(((TotArtPatient / Total) * 100)).ToString() + '\t' + Math.Round(((TotNonArtPatient / Total) * 100)).ToString();
    //    string strCategory = "ART Patient" + '\t' + "Non ART Patient";

    //    objChart.SeriesCollection[0].SetData(graph.ChartDimensionsEnum.chDimSeriesNames, (int)graph.ChartSpecialDataSourcesEnum.chDataLiteral, strSeriesName);
    //    objChart.SeriesCollection[0].SetData(graph.ChartDimensionsEnum.chDimCategories, (int)graph.ChartSpecialDataSourcesEnum.chDataLiteral, strCategory);
    //    objChart.SeriesCollection[0].SetData(graph.ChartDimensionsEnum.chDimValues, (int)graph.ChartSpecialDataSourcesEnum.chDataLiteral, strValue);
    //    objChart.Border.Color = "#33CC33";
    //    objChart.SeriesCollection[0].Points[0].Interior.Color = "f4a460";
    //    objChart.SeriesCollection[0].Points[1].Interior.Color = "ffd700";
    //    string strAbsolutePath = (Server.MapPath(".")) + ".\\Images\\Piechart1.GIF";
    //    objCSpace.ExportPicture(strAbsolutePath, "GIF", 300, 200);
    //    string strRelativePath = ".\\Images\\Piechart1.gif";
    //    string strImageTag = "<IMG SRC='" + strRelativePath + "'/>";
    //    ChartHolder1.Controls.Add(new LiteralControl(strImageTag));
    //}
    //void PerArtAge(double TotARTFMUpto2, double TotARTFMUpto5, double TotARTFMUpto15, double TotARTFMAbove15, double theTotArtPatient) 
    //{
    //    if (Convert.ToInt32(TotARTFMUpto2) < 1 || Convert.ToInt32(TotARTFMUpto5) < 1 || Convert.ToInt32(TotARTFMUpto15) < 1 || Convert.ToInt32(TotARTFMAbove15) < 1 || Convert.ToInt32(theTotArtPatient)<1)
    //        return;

    //    graph.ChartSpace objCSpace = new graph.ChartSpaceClass();
    //    graph.ChChart objChart = objCSpace.Charts.Add(0);
    //    objChart.Type = graph.ChartChartTypeEnum.chChartTypeDoughnutExploded;
    //    objCSpace.Charts[0].ChartDepth = 125;
    //    objCSpace.Charts[0].AspectRatio = 80;
    //    objChart.HasLegend = false;
    //    objCSpace.Charts[0].SeriesCollection.Add(0);
    //    objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection.Add();
    //    objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection[0].HasPercentage = true;
    //    objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection[0].HasValue = false;
    //    objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection[0].HasCategoryName = true;
    //    objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection[0].Separator = "\n";
    //    objCSpace.Charts[0].SeriesCollection[0].Caption = String.Empty;
    //    objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection[0].Font.Name = "verdana";
    //    objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection[0].Font.Size = 7;
    //    objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection[0].Font.Bold = false;
    //    objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection[0].Font.Color = "Black";
    //    objChart.HasTitle = true;
    //    objChart.Title.Caption = "";
    //    objChart.HasLegend = true;
    //    //objChart.Axes[0].HasTitle = true;
    //    //objChart.Axes[0].Title.Caption = "Male Axis Caption";
    //    //objChart.Axes[1].HasTitle = true;
    //    //objChart.Axes[1].Title.Caption = "Female Axis Caption";
    //    string strSeriesName = "Person 1";
    //    string strValue = Math.Round(((TotARTFMUpto2 / theTotArtPatient) * 100)).ToString() + '\t' + Math.Round(((TotARTFMUpto5 / theTotArtPatient) * 100)).ToString() + '\t' + Math.Round(((TotARTFMUpto15 / theTotArtPatient) * 100)).ToString() + '\t' + Math.Round(((TotARTFMAbove15 / theTotArtPatient) * 100)).ToString();
    //    string strCategory = "0-1" + '\t' + "2-4"+'\t' + "5-14" + '\t' + "above 15";
    //    objChart.SeriesCollection[0].SetData(graph.ChartDimensionsEnum.chDimSeriesNames, (int)graph.ChartSpecialDataSourcesEnum.chDataLiteral, strSeriesName);
    //    objChart.SeriesCollection[0].SetData(graph.ChartDimensionsEnum.chDimCategories, (int)graph.ChartSpecialDataSourcesEnum.chDataLiteral, strCategory);
    //    objChart.SeriesCollection[0].SetData(graph.ChartDimensionsEnum.chDimValues, (int)graph.ChartSpecialDataSourcesEnum.chDataLiteral, strValue);
    //    objChart.Border.Color = "#33CC33";
    //    objChart.SeriesCollection[0].Points[0].Interior.Color = "e9967a";
    //    objChart.SeriesCollection[0].Points[1].Interior.Color = "9acd32";
    //   string strAbsolutePath = (Server.MapPath(".")) + ".\\Images\\Piechart2.GIF";
    //    objCSpace.ExportPicture(strAbsolutePath, "GIF", 300, 200);
    //    string strRelativePath = ".\\Images\\Piechart2.gif";
    //    string strImageTag = "<IMG SRC='" + strRelativePath + "'/>";
    //    ChartHolder2.Controls.Add(new LiteralControl(strImageTag));
    //}

    protected void chkpreferred_CheckedChanged(object sender, EventArgs e)
    {
        IQCareUtils theUtils = new IQCareUtils();
        DataView theDV = new DataView((DataTable)ViewState["Facility"]);
        theDV.Sort = "FacilityID";
        if (chkpreferred.Checked == true)
        {
            theDV.RowFilter = "Preferred = 1";
            DataTable theDT1 = (DataTable)theUtils.CreateTableFromDataView(theDV);
            DataRow theDR = theDT1.NewRow();
            theDR["FacilityName"] = "All";
            theDR["FacilityId"] = 9999;
            theDR["Preferred"] = 1;
            theDT1.Rows.InsertAt(theDR, 0);
            ddFacility.DataSource = theDT1;
            ddFacility.DataTextField = "FacilityName";
            ddFacility.DataValueField = "FacilityId";
            ddFacility.DataBind();
            //ddFacility.SelectedValue = Session["AppLocationId"].ToString();
            //Init_Form();

        }
        else
        {
            DataTable theDT1 = (DataTable)theUtils.CreateTableFromDataView(theDV);
            DataRow theDR = theDT1.NewRow();
            theDR["FacilityName"] = "All";
            theDR["FacilityId"] = 9999;
            theDR["Preferred"] = 0;
            theDT1.Rows.InsertAt(theDR, 0);
            ddFacility.DataSource = theDT1;
            ddFacility.DataTextField = "FacilityName";
            ddFacility.DataValueField = "FacilityId";
            ddFacility.DataBind();
            //ddFacility.SelectedValue = Session["AppLocationId"].ToString();
           
        }
       Init_Form();
        //Page_Load(sender, e);
    }
    //#region "Ever Enrolled Patients List"
    //protected void hlEverEnrolledPatients_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[0], "EverEnrolledPatient");
    //}
    //#endregion

    //#region "Total Enrolled Female Patients List"
    //protected void hlFemalesEnrolled_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[1], "FemalesEnroll");
    //}
    //#endregion

    //#region "Total Enrolled Male Patients List"
    //protected void hlMalesEnrolled_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[2], "MalesEnroll");
    //}
    //#endregion

    //#region "Total Active Patients List"
    //protected void hlActivePatients_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[3], "ActivePatients");
    //}
    //#endregion

    //#region "Total Active Non-ART Patients List"
    //protected void hlActiveNonARTPatients_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[4], "ActiveNonARTPatients");
    //}
    //#endregion

    //#region "Total Active ART Patients List"
    //protected void hlActiveARTPatients_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[5], "ActiveARTPatients");
    //}
    //#endregion

    //#region "ART Mortality"
    //protected void hlARTMortality_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[6], "ARTMortality");
    //}
    //#endregion

    #region "Lost to Follow-up Patient List"
    protected void hlLosttoFollowUp_Click(object sender, EventArgs e)
    {
        IReports ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports, BusinessProcess.Reports");
        DataTable dtLosttoFollowupPatientReport = (DataTable)ReportDetails.GetLosttoFollowupPatientReport(Convert.ToInt32(ddFacility.SelectedValue), Session["SystemId"].ToString()).Tables[0];
        string FName = "LstFollowup";
        IQWebUtils theUtils = new IQWebUtils();
        string thePath = Server.MapPath(".\\ExcelFiles\\" + FName + ".xls");
        string theTemplatePath = Server.MapPath(".\\ExcelFiles\\IQCareTemplate.xls");
        theUtils.ExporttoExcel(dtLosttoFollowupPatientReport,Response);
        Response.Redirect(".\\ExcelFiles\\" + FName + ".xls");
        //theUtils.OpenExcelFile(thePath, Response);

    }
    #endregion


    #region "ART Unknown Patient List"
    protected void hlartunknown_Click(object sender, EventArgs e)
    {

        IReports ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports, BusinessProcess.Reports");
        System.Data.DataSet dsARTUnknown = ReportDetails.GetPtnotvisitedrecentlyUnknown(Application["AppCurrentDate"].ToString(), Application["AppCurrentDate"].ToString(), Convert.ToInt32(ddFacility.SelectedValue));
        string FName = "ARTunknown";
        IQWebUtils theUtils = new IQWebUtils();
        string thePath = Server.MapPath(".\\ExcelFiles\\" + FName + ".xls");
        string theTemplatePath = Server.MapPath(".\\ExcelFiles\\IQCareTemplate.xls");
        theUtils.ExporttoExcel(dsARTUnknown.Tables[0], Response);
        Response.Redirect(".\\ExcelFiles\\" + FName + ".xls");

    }
    #endregion

    //#region "0-1 Year Non-ART Male Patients List"
    //protected void hlNonARTMUpto2_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[7], "NonARTMale0-1");

    //}
    //#endregion

    //#region "2-4 Year Non-ART Male Patients List"
    //protected void hlNonARTMUpto4_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[8], "NonARTMale2-4");

    //}
    //#endregion

    //#region "5-14 Year Non-ART Male Patients List"
    //protected void hlNonARTMUpto14_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[9], "NonARTMale5-14");

    //}
    //#endregion

    //#region "15+ Year Non-ART Male Patients List"
    //protected void hlNonARTMAbove15_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[10], "NonARTMaleabove15");

    //}
    //#endregion

    //#region "0-1 Year Non-ART Female Patients List"
    //protected void hlNonARTFUpto2_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[11], "NonARTFemale0-1");

    //}
    //#endregion

    //#region "2-4 Year Non-ART Female Patients List"
    //protected void hlNonARTFUpto4_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[12], "NonARTFemale2-4");

    //}
    //#endregion

    //#region "5-14 Year Non-ART Female Patients List"
    //protected void hlNonARTFUpto14_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[13], "NonARTFemale5-14");

    //}
    //#endregion

    //#region "15+ Year Non-ART Female Patients List"
    //protected void hlNonARTFAbove15_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[14], "NonARTFemaleabove15");

    //}
    //#endregion

    //#region "0-1 Year ART Male Patients List"
    //protected void hlARTMUpto2_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[15], "ARTMale0-1");

    //}
    //#endregion

    //#region "2-4 Year ART Male Patients List"
    //protected void hlARTMUpto4_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[16], "ARTMale2-4");

    //}
    //#endregion

    //#region "5-14 Year ART Male Patients List"
    //protected void hlARTMUpto14_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[17], "ARTMale5-14");

    //}
    //#endregion

    //#region "15+ Year ART Male Patients List"
    //protected void hlARTMAbove15_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[18], "ARTMaleabove15");

    //}
    //#endregion

    //#region "0-1 Year ART Female Patients List"
    //protected void hlARTFUpto2_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[19], "ARTFemale0-1");

    //}
    //#endregion

    //#region "2-4 Year ART Female Patients List"
    //protected void hlARTFUpto4_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[20], "ARTFemale2-4");
    //}
    //#endregion

    //#region "5-14 Year ART Female Patients List"
    //protected void hlARTFUpto14_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[21], "ARTFemale5-14");
    //}
    //#endregion

    //#region "15+ Year ART Female Patients List"
    //protected void hlARTFAbove15_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[22], "ARTFemaleabove15");
    //}
    //#endregion
    
    //protected void hlMothersEverEnroll_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[25], "PMTCTEverEnrollMothers");
    //}
    //protected void hlCurrentMothers_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[26], "PMTCTCurrentMothers");
    //}
    //protected void hlProANC_Click(object sender, EventArgs e)
    //{ 
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[27], "ProANCMothers");
    
    //}
    //protected void hlProLD_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[28], "ProLDMothers");
    //}
    //protected void hlProPN_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[29], "ProPNMothers");
    //}
    //protected void hlPosMotherPosPartner_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[32], "ANCPosMotherPosPartner");
    //}
    //protected void hlPosMotherNegPartner_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[33], "ANCPosMotherNegPartner");

    //}
    //protected void hlPosMotherUnknownPartner_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[34], "ANCPosMotherUnknownPartner"); 
    //}
    //protected void hlNegMotherPosPartner_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[36], "ANCNegMotherPosPartner"); 

    //}
    //protected void hlNegMotherNegPartner_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[37], "ANCNegMotherNegPartner"); 

    //}
    //protected void hlNegMotherUnknownPartner_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[38], "ANCNegMotherUnknownPartner"); 

    //}
    //protected void hlLDPosMotherPosPartner_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[41], "LDPosMotherPosPartner"); 

    //}
    //protected void hlLDPosMotherNegPartner_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[42], "LDPosMotherNegPartner"); 

    //}
    //protected void hlLDPosMotherUnknownPartner_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[43], "LDPosMotherUnknownPartner"); 

    //}
    //protected void hlLDNegMotherPosPartner_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[45], "LDNegMotherPosPartner"); 

    //}
    //protected void hlLDNegMotherNegPartner_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[46], "LDNegMotherNegPartner"); 

    //}
    //protected void hlLDNegMotherUnknownPartner_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[47], "LDNegMotherUnknownPartner"); 

    //}
    //protected void hlPNPosMotherPosPartner_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[50], "PNPosMotherPosPartner"); 

    //}
    //protected void hlPNPosMotherNegPartner_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[51], "PNPosMotherNegPartner"); 

    //}
    //protected void hlPNPosMotherUnknownPartner_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[52], "PNPosMotherUnknownPartner"); 

    //}
    //protected void hlPNNegMotherPosPartner_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[54], "PNNegMotherPosPartner"); 

    //}
    //protected void hlPNNegMotherNegPartner_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[55], "PNNegMotherNegPartner");
    //}
    //protected void hlPNNegMotherUnknownPartner_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[56], "PNNegMotherUnknownPartner");
    //}
    //protected void hlExposedInfants_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[57], "CumulativeExposedInfants");
    //}
    //protected void hlCurrentExposedInfants_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[58], "CurrentExposedInfants");
   
    //}
    //protected void hlCurrentPMTCTInfants_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[59], "CurrentPMTCTInfants");

    //}
    //protected void hlCurrentHIVCareInfants_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[60], "CurrentHIVCareInfants");

    //}
    
    //protected void hlHIVPosEBFlessthan2_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[66], "AgeLessthan2HIVPosEBF");
    //}
    //protected void hlHIVPosRFlessthan2_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[67], "AgeLessthan2HIVPosRF");
    //}
    //protected void hlHIVPosMFlessthan2_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[68], "AgeLessthan2HIVPosMF");
    //}
    //protected void hlHIVPosOtherlessthan2_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[69], "AgeLessthan2HIVPosOther");

    //}   
    //protected void hlHIVNegEBFlessthan2_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[70], "AgeLessthan2HIVNegEBF");
    //}
    //protected void hlHIVNegRFlessthan2_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[71], "AgeLessthan2HIVNegRF");
    //}
    //protected void hlHIVNegMFlessthan2_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[72], "AgeLessthan2HIVNegMF");
    //}
    //protected void hlHIVNegOtherlessthan2_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[73], "AgeLessthan2HIVNegOther");
    //}   
    //protected void hlIndeterminateEBFlessthan2_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[74], "AgeLessthan2IndeterminateEBF");

    //}
    //protected void hlIndeterminateRFlessthan2_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[75], "AgeLessthan2IndeterminateRF");
    //}
    //protected void hlIndeterminateMFlessthan2_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[76], "AgeLessthan2IndeterminateMF");
    //}
    //protected void hlIndeterminateOtherlessthan2_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[77], "AgeLessthan2IndeterminateOther");
    //}    
    //protected void hlHIVPosEBF2to12_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[83], "HIVPos2to12EBF");
    //}
    //protected void hlHIVPosRF2to12_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[84], "HIVPos2to12RF");
    //}
    //protected void hlHIVPosMF2to12_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[85], "HIVPos2to12MF");
    //}
    //protected void hlHIVPosOther2to12_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[86], "HIVPos2to12Other");
    //}  
    //protected void hlHIVNegEBF2to12_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[87], "HIVNeg2to12EBF");
    //}
    //protected void hlHIVNegRF2to12_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[88], "HIVNeg2to12RF");
    //}
    //protected void hlHIVNegMF2to12_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[89], "HIVNeg2to12MF");
    //}
    //protected void hlHIVNegOther2to12_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[90], "HIVNeg2to12Other");
    //}   
    //protected void hlIndeterminateEBF2to12_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[91], "Indeterminate2to12EBF");
    //}
    //protected void hlIndeterminateRF2to12_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[92], "Indeterminate2to12RF");
    //}
    //protected void hlIndeterminateMF2to12_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[93], "Indeterminate2to12MF");
    //}
    //protected void hlIndeterminateOther2to12_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[94], "Indeterminate2to12Other");
    //}   
    //protected void hlHIVPosEBF18to24_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[100], "HIVPos18to24EBF");
    //}
    //protected void hlHIVPosRF18to24_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[101], "HIVPos18to24RF");
    //}
    //protected void hlHIVPosMF18to24_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[102], "HIVPos18to24MF");
    //}
    //protected void hlHIVPosOther18to24_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[103], "HIVPos18to24Other");
    //}  
    //protected void hlHIVNegEBF18to24_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[104], "HIVNeg18to24EBF");
    //}
    //protected void hlHIVNegRF18to24_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[105], "HIVNeg18to24RF");
    //}
    //protected void hlHIVNegMF18to24_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[106], "HIVNeg18to24MF");
    //}
    //protected void hlHIVNegOther18to24_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[107], "HIVNeg18to24Other");
    //}   
    //protected void hlIndeterminateEBF18to24_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[108], "Indeterminate18to24EBF");
    //}
    //protected void hlIndeterminateRF18to24_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[109], "Indeterminate18to24RF");
    //}
    //protected void hlIndeterminateMF18to24_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[110], "Indeterminate18to24MF");
    //}
    //protected void hlIndeterminateOther18to24_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[111], "Indeterminate18to24Other");
    //}
    //protected void hlInfantsARVPro_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[112], "InfantsCumulativeARVProphylaxis");
    //}
    //protected void hlInfantsCurrentProphylaxis_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[113], "InfantsCurrentARVProphylaxis");
    //}
    //protected void hlInfantsCumulativeARV_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[114], "InfantsCumulativeARVTreatment");
    //}
    //protected void hlInfantsCurrentARV_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[115], "InfantsCurrentARVTreatment");
    //}
    //protected void hlContrimProCumulessthan2_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[116], "ContrimProCumulessthan2");
    
    //}
    //protected void hlContrimProCurrentlessthan2_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[117], "ContrimProCurrentlessthan2");
    //}
    //protected void hlContrimProCumu2to24_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[118], "ContrimProCumu2to24");
    //}
    //protected void hlContrimProCurrent2to24_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[119], "ContrimProCurrent2to24");
    //}
    //protected void hlInfantsnotonContrim_Click(object sender, EventArgs e)
    //{
    //    ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[120], "InfantsnotonContrim");
    //}
    protected void hlTotalActivePatients_Click(object sender, EventArgs e)
    {
        ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[0], "ActivePatients");
    }

    protected void  hlActiveNonARTPatients_Click(object sender, EventArgs e)
    {
        ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[1], "ActiveNonARTPatients");
    }

    protected void hlActiveARTPatient_Click(object sender, EventArgs e)
    {
        ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[2], "ActiveARTPatients");
    }
    protected void hllnkCurrentMotherPMTCT_Click(object sender, EventArgs e)
    {
        ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[3], "PMTCTCurrentMothers");
    }

    protected void hllnkANC_Click(object sender, EventArgs e)
    {
        ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[4], "ProANCMothers");
    }
    protected void hllnkLD_Click(object sender, EventArgs e)
    {
        ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[5], "ProLDMothers");
    }
    protected void hllnkPostnatal_Click(object sender, EventArgs e)
    {
        ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[6], "ProLDMothers");
    }
    protected void hllnkCurrentTotalExposedInfants_Click(object sender, EventArgs e)
    {
        ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[7], "CurrentTotalExposedInfants");
    }  
    protected void hllnkCurrentPMTCTInfants_Click(object sender, EventArgs e)
    {
        ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[8], "CurrentPMTCTInfants");
    }
    protected void hllnkCurrentHIVCareInfants_Click(object sender, EventArgs e)
    {
        ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[9], "CurrentHIVCareInfants");
    }
    protected void more_Click(object sender, EventArgs e)
    {
        
        string theScript;
        theScript = "<script language='javascript' id='Popup'>\n";
        theScript += "window.open('frmHIVCareFacilityStatistics.aspx','popupwindow','toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=yes,resizable=no,width=950,height=650,scrollbars=yes');\n";
        theScript += "</script>\n";
        Page.RegisterClientScriptBlock("Popup", theScript);

    }
    protected void hlmore1_Click(object sender, EventArgs e)
    {
       
        string theScript;
        theScript = "<script language='javascript' id='pmtctpopup'>\n";
        theScript += "window.open('frmPMTCTFacilityStatistics.aspx','popupwindow','toolbars=no,location=no, directories=no,dependent=yes,top=10,left=30.maximize=yes, resizable=no,width=900,height=630,scrollbars=yes');\n";
        theScript += "</script>\n";
        Page.RegisterClientScriptBlock("pmtctpopup",theScript);

    }
    protected void hlmore2_Click(object sender, EventArgs e)
    {
       
        string theScript;
        theScript = "<script language='javascript' id='pmtctpopup'>\n";
        theScript += "window.open('frmExposedFacilityStatistics.aspx','popupwindow','toolbars=no,location=no, directories=no,dependent=yes,top=10,left=30.maximize=yes, resizable=no,width=900,height=630,scrollbars=yes');\n";
        theScript += "</script>\n";
        Page.RegisterClientScriptBlock("pmtctpopup", theScript);
    }

    protected void GetSQLAuditData()
    {
        IUser LoginManager = (IUser)ObjectFactory.CreateInstance("BusinessProcess.Security.BUser, BusinessProcess.Security");
        System.Data.DataSet theDS = LoginManager.GetAuditData();

        if (theDS.Tables[0].Rows[0]["Days"] != System.DBNull.Value)
        {
            if (Convert.ToInt32(theDS.Tables[0].Rows[0]["Days"].ToString()) >= 1 && Convert.ToInt32(theDS.Tables[0].Rows[0]["Days"].ToString()) < 7)
            {
                AuditTrailNotificationonfailure();
            }
            if (Convert.ToInt32(theDS.Tables[0].Rows[0]["Days"].ToString()) == 7)
            {
                string xmlFilesPath = string.Empty;
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["XMLAuditTrail"].ToString()))
                {
                    xmlFilesPath = ConfigurationManager.AppSettings["XMLAuditTrail"].ToString();
                    string TodayDatetime = DateTime.Today.ToShortDateString().Replace("/", "-");
                    string AuditTrail = xmlFilesPath + "_" + TodayDatetime + ".XML";
                    System.IO.FileInfo theFileInfo1 = new System.IO.FileInfo(AuditTrail);
                    System.Data.DataSet WriteXMLDS = new System.Data.DataSet();
                    WriteXMLDS.Tables.Add(theDS.Tables[1].Copy());
                    WriteXMLDS.WriteXml(AuditTrail, XmlWriteMode.WriteSchema);
                    AuditTrailNotificationbackup();
                }
            }
        }
    }
    
}

