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
using Application.Common;
using Application.Presentation;
using Interface.Security;
using Interface.Reports;
using ChartDirector;
using graph = Microsoft.Office.Interop.Owc11;



public partial class frmHIVCareFacilityStatistics : BasePage
{
    #region "Variable Declaration"
    double theGraphLowCD4, theGraphRecentCD4, ChartChartTypeEnum, theGraphHb = 0.0, theGraphHct = 0.0, theGraphAST = 0.0, theGraphCr = 0.0, Graph = 0.0;
    string month1, month2, month3, month4, month5, month6;
    System.Data.DataSet theFacilityDS;
    System.Data.DataSet theDS;

    public double noofmale;
    public double nooffemale;
    public double noofart;
    public double noofNonart;
   // public double noofArtbelow1;
    public double noofArtupto2;
    public double noofArtupto4;
    public double noofArtupto15;
    public double noofArtabove15;

    #endregion
    private void ShowReport(DataTable theTable, string FileName)
    {
        IQWebUtils theUtils = new IQWebUtils();
        theUtils.ExporttoExcel(theTable, Response);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
        {
            IQCareMsgBox.Show("SessionExpired", this);
            Response.Redirect("~/frmlogin.aspx",true);
        }
        try
        {

            lblLocation.Text = Session["FacilityName"].ToString();
            IFacility HIVCareFacManager;
            Double thePercent, theResultPercent, theTotalPateint, theTotalPMTCTPatient;
            HIVCareFacManager = (IFacility)ObjectFactory.CreateInstance("BusinessProcess.Security.BFacility, BusinessProcess.Security");
            theDS = HIVCareFacManager.GetHIVCareFacilityStats(Convert.ToInt16(Session["Facility"].ToString()));
            ViewState["theDS"] = theDS;
            
            lblTotalPatient.Text = theDS.Tables[0].Rows.Count.ToString();

            theTotalPateint = Convert.ToDouble(theDS.Tables[0].Rows.Count);

            /**********************Caluclate Female Patients *************/
            Double theTotFemale = Convert.ToDouble(theDS.Tables[1].Rows.Count);
            thePercent = (theTotFemale / theTotalPateint) * 100;
            if (theTotalPateint != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lblFemalePatient.Text = theDS.Tables[1].Rows.Count + " " + "(" + theResultPercent + "%)";

            ///**********************Calculate Male Patients ***************/
            Double theTotMale = Convert.ToDouble(theDS.Tables[2].Rows.Count);
            thePercent = (theTotMale / theTotalPateint) * 100;
            if (theTotalPateint != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lblMalePatient.Text = theDS.Tables[2].Rows.Count + " " + "(" + theResultPercent + "%)";
            lblTotalActivePatients.Text = theDS.Tables[3].Rows.Count.ToString();

            ///*********************Calculate NonART AND Pateint % ****************/
            Double theActivePatient = Convert.ToDouble(theDS.Tables[3].Rows.Count);
            Double theTotNonArtPatient = Convert.ToDouble(theDS.Tables[4].Rows.Count);
            thePercent = (theTotNonArtPatient / theActivePatient) * 100;
            if (theActivePatient != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lblActiveNonARTPatients.Text = theDS.Tables[4].Rows.Count + " " + "(" + theResultPercent + "%)";

            ///*********************Calculate ART AND Pateint % ****************/

            Double theTotArtPatient = Convert.ToDouble(theDS.Tables[5].Rows.Count);

            thePercent = (theTotArtPatient / theActivePatient) * 100;
            if (theActivePatient != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lblActiveARTPatients.Text = theDS.Tables[5].Rows.Count + " " + "(" + theResultPercent + "%)";

            ///*********************Calculate ARTMortality %  ****************/

            Double theARTMortality = Convert.ToDouble(theDS.Tables[6].Rows.Count);
            Double theTotalARTPatient = Convert.ToDouble(theDS.Tables[5].Rows.Count);
            thePercent = (theARTMortality / theTotalARTPatient) * 100;
            if (theTotalARTPatient != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lblARTMortality.Text = Convert.ToDouble(theDS.Tables[6].Rows.Count).ToString();
           // lblARTMortality.Text = theDS.Tables[6].Rows.Count + " " + "(" + theResultPercent + "%)";



            /************************ Calculate Non-ART Male Patient % By Age and Sex *************************/
            /*** Non-ART Male Patient 0-1 *****/
            Double theNonARTPtn;
            theNonARTPtn = Convert.ToDouble(theDS.Tables[7].Rows.Count);
            thePercent = (theNonARTPtn / theTotNonArtPatient) * 100;
            if (theTotNonArtPatient != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lblNonARTMUpto2.Text = theDS.Tables[7].Rows.Count + " " + "(" + theResultPercent + "%)";

            /*** Non-ART Male Patient 2-4 *****/
            theNonARTPtn = Convert.ToDouble(theDS.Tables[8].Rows.Count);
            thePercent = (theNonARTPtn / theTotNonArtPatient) * 100;
            if (theTotNonArtPatient != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lblNonARTMUpto4.Text = theDS.Tables[8].Rows.Count + " " + "(" + theResultPercent + "%)";

            /*** Non-ART Male Patient 5-14 *****/
            theNonARTPtn = Convert.ToDouble(theDS.Tables[9].Rows.Count);
            thePercent = (theNonARTPtn / theTotNonArtPatient) * 100;
            if (theTotNonArtPatient != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lblNonARTMUpto14.Text = theDS.Tables[9].Rows.Count + " " + "(" + theResultPercent + "%)";

            /*** Non-ART Male Patient 15+ *****/
            theNonARTPtn = Convert.ToDouble(theDS.Tables[10].Rows.Count);
            thePercent = (theNonARTPtn / theTotNonArtPatient) * 100;
            if (theTotNonArtPatient != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lblNonARTMAbove15.Text = theDS.Tables[10].Rows.Count + " " + "(" + theResultPercent + "%)";

            /************************ Calculate Non-ART Female Patient % By Age and Sex *************************/
            /*** Non-ART Female Patient 0-1 *****/
            theNonARTPtn = Convert.ToDouble(theDS.Tables[11].Rows.Count);
            thePercent = (theNonARTPtn / theTotNonArtPatient) * 100;
            if (theTotNonArtPatient != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lblNonARTFupto2.Text = theDS.Tables[11].Rows.Count + " " + "(" + theResultPercent + "%)";

            /*** Non-ART Female Patient 2-4 *****/
            theNonARTPtn = Convert.ToDouble(theDS.Tables[12].Rows.Count);
            thePercent = (theNonARTPtn / theTotNonArtPatient) * 100;
            if (theTotNonArtPatient != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lblNonARTFupto4.Text = theDS.Tables[12].Rows.Count + " " + "(" + theResultPercent + "%)";

            /*** Non-ART Female Patient 5-14 *****/
            theNonARTPtn = Convert.ToDouble(theDS.Tables[13].Rows.Count);
            thePercent = (theNonARTPtn / theTotNonArtPatient) * 100;
            if (theTotNonArtPatient != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }

            lblNonARTFUpto14.Text = theDS.Tables[13].Rows.Count + " " + "(" + theResultPercent + "%)";

            /*** Non-ART Female Patient 15+ *****/
            theNonARTPtn = Convert.ToDouble(theDS.Tables[14].Rows.Count);
            thePercent = (theNonARTPtn / theTotNonArtPatient) * 100;
            if (theTotNonArtPatient != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lblNonARTFAbove15.Text = theDS.Tables[14].Rows.Count + " " + "(" + theResultPercent + "%)";



            /************************theTotArtPatient Calculate ART Pateint % By Age and Sex *************************/

            Double theARTPtn, TotARTFMUpto2, TotARTFMUpto5, TotARTFMUpto15, TotARTFMAbove15;

            /*** ART Male Patient 0-1 *****/
            theARTPtn = Convert.ToDouble(theDS.Tables[15].Rows.Count);

            TotARTFMUpto2 = theARTPtn; //For Graph
            thePercent = (theARTPtn / theTotArtPatient) * 100;
            if (theTotArtPatient != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lblARTMUpto2.Text = theDS.Tables[15].Rows.Count + " " + "(" + theResultPercent + "%)";

            /*** ART Male Patient 2-4 *****/
            theARTPtn = Convert.ToDouble(theDS.Tables[16].Rows.Count);
            TotARTFMUpto5 = theARTPtn;//For Graph
            thePercent = (theARTPtn / theTotArtPatient) * 100;
            if (theTotArtPatient != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lblARTMUpto4.Text = theDS.Tables[16].Rows.Count + " " + "(" + theResultPercent + "%)";

            /*** ART Male Patient 5-14 *****/
            theARTPtn = Convert.ToDouble(theDS.Tables[17].Rows.Count);
            TotARTFMUpto15 = theARTPtn;//For Graph
            thePercent = (theARTPtn / theTotArtPatient) * 100;
            if (theTotArtPatient != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lblARTMUpto14.Text = theDS.Tables[17].Rows.Count + " " + "(" + theResultPercent + "%)";

            /*** ART Male Patient 15+ *****/
            theARTPtn = Convert.ToDouble(theDS.Tables[18].Rows.Count);
            TotARTFMAbove15 = theARTPtn; //For Graph
            thePercent = (theARTPtn / theTotArtPatient) * 100;
            if (theTotArtPatient != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lblARTMAbove15.Text = theDS.Tables[18].Rows.Count + " " + "(" + theResultPercent + "%)";

            /*** ART Female Patient 0-1 *****/
            theARTPtn = Convert.ToDouble(theDS.Tables[19].Rows.Count);
            TotARTFMUpto2 = TotARTFMUpto2 + theARTPtn;//For Graph
            thePercent = (theARTPtn / theTotArtPatient) * 100;
            if (theTotArtPatient != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lblARTFUpto2.Text = theDS.Tables[19].Rows.Count + " " + "(" + theResultPercent + "%)";

            /*** ART Female Patient 2-4 *****/
            theARTPtn = Convert.ToDouble(theDS.Tables[20].Rows.Count);
            TotARTFMUpto5 = TotARTFMUpto5 + theARTPtn;//For Graph
            thePercent = (theARTPtn / theTotArtPatient) * 100;
            if (theTotArtPatient != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lblARTFUpto4.Text = theDS.Tables[20].Rows.Count + " " + "(" + theResultPercent + "%)";

            /*** ART Female Patient 5-14 *****/
            theARTPtn = Convert.ToDouble(theDS.Tables[21].Rows.Count);
            TotARTFMUpto15 = TotARTFMUpto15 + theARTPtn;
            thePercent = (theARTPtn / theTotArtPatient) * 100;
            if (theTotArtPatient != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lblARTFUpto14.Text = theDS.Tables[21].Rows.Count + " " + "(" + theResultPercent + "%)";

            /*** ART Female Patient 15+ *****/
            theARTPtn = Convert.ToDouble(theDS.Tables[22].Rows.Count);
            TotARTFMAbove15 = TotARTFMAbove15 + theARTPtn;

            thePercent = (theARTPtn / theTotArtPatient) * 100;
            if (theTotArtPatient != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lblARTFAbove15.Text = theDS.Tables[22].Rows.Count + " " + "(" + theResultPercent + "%)";
            PieChart(theTotFemale, theTotMale);
            ArtNonArtPieChart(theTotArtPatient, theTotNonArtPatient);
            PerArtAge(TotARTFMUpto2,TotARTFMUpto5,TotARTFMUpto15,TotARTFMAbove15,theTotArtPatient);

        }
        catch (Exception err)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
        }
        
    }
    void PieChart(double TotFemale, double TotMale)
    {
        noofmale = TotMale;
        nooffemale = TotFemale;
        //////////if (Convert.ToInt32(TotFemale) < 1 || Convert.ToInt32(TotMale) < 1)
        //////////    return;
       
        //////////graph.ChartSpace objCSpace = new graph.ChartSpaceClass();
        //////////graph.ChChart objChart = objCSpace.Charts.Add(0);
        //////////objChart.Type = graph.ChartChartTypeEnum.chChartTypeDoughnutExploded;
        //////////objCSpace.Charts[0].ChartDepth = 125;
        //////////objCSpace.Charts[0].AspectRatio = 80;
        ////////////objCSpace.Charts[0].SeriesCollection[0].Interior.Color = "Green";  
        //////////objChart.HasLegend = false;
        //////////objCSpace.Charts[0].SeriesCollection.Add(0);
        //////////objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection.Add();
        //////////objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection[0].HasPercentage = true;
        //////////objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection[0].HasValue = false;
        //////////objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection[0].HasCategoryName = true;
        //////////objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection[0].Separator = "\n";
        //////////objCSpace.Charts[0].SeriesCollection[0].Caption = String.Empty;
        //////////objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection[0].Font.Name = "verdana";
        //////////objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection[0].Font.Size = 7;
        //////////objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection[0].Font.Bold = false;
        //////////objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection[0].Font.Color = "Black";
        //////////objChart.HasTitle = true;
        //////////objChart.Title.Caption = "";
        //////////objChart.HasLegend = true;
        ////////////objChart.Axes[0].HasTitle = true;
        ////////////objChart.Axes[0].Title.Caption = "Male Axis Caption";
        ////////////objChart.Axes[1].HasTitle = true;
        ////////////objChart.Axes[1].Title.Caption = "Female Axis Caption";
        //////////string strSeriesName = "Person 1";
        //////////double Total = TotFemale + TotMale;
        //////////string strValue = Math.Round(((TotFemale / Total) * 100)).ToString() + '\t' + Math.Round(((TotMale / Total) * 100)).ToString();
        //////////string strCategory = "Female" + '\t' + "Male";
        //////////objChart.SeriesCollection[0].SetData(graph.ChartDimensionsEnum.chDimSeriesNames, (int)graph.ChartSpecialDataSourcesEnum.chDataLiteral, strSeriesName);
        //////////objChart.SeriesCollection[0].SetData(graph.ChartDimensionsEnum.chDimCategories, (int)graph.ChartSpecialDataSourcesEnum.chDataLiteral, strCategory);
        //////////objChart.SeriesCollection[0].SetData(graph.ChartDimensionsEnum.chDimValues, (int)graph.ChartSpecialDataSourcesEnum.chDataLiteral, strValue);

        //////////objChart.Border.Color = "#33CC33";
        //////////objChart.SeriesCollection[0].Points[0].Interior.Color = "dda0dd";
        //////////objChart.SeriesCollection[0].Points[1].Interior.Color = "eee8aa";
        //////////string strAbsolutePath = (Server.MapPath(".")) + "\\Images\\Piechart.GIF";
        //////////objCSpace.ExportPicture(strAbsolutePath, "GIF", 300, 200);
        //////////string strRelativePath = ".\\Images\\Piechart.gif";
        //////////ChartMaleFemale.ImageUrl = strRelativePath;
    }
    void ArtNonArtPieChart(double TotArtPatient, double TotNonArtPatient)
    {
        noofart = TotArtPatient;
        noofNonart = TotNonArtPatient;

        //if (Convert.ToInt32(TotArtPatient) < 1 || Convert.ToInt32(TotNonArtPatient) < 1)
        //    return;

        //////////////graph.ChartSpace objCSpace = new graph.ChartSpaceClass();
        //////////////graph.ChChart objChart = objCSpace.Charts.Add(0);
        //////////////objChart.Type = graph.ChartChartTypeEnum.chChartTypeDoughnutExploded;
        //////////////objCSpace.Charts[0].ChartDepth = 125;
        //////////////objCSpace.Charts[0].AspectRatio = 80;
        //////////////objChart.HasLegend = false;
        //////////////objCSpace.Charts[0].SeriesCollection.Add(0);
        //////////////objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection.Add();
        //////////////objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection[0].HasPercentage = true;
        //////////////objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection[0].HasValue = false;
        //////////////objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection[0].HasCategoryName = true;
        //////////////objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection[0].Separator = "\n";
        //////////////objCSpace.Charts[0].SeriesCollection[0].Caption = String.Empty;
        //////////////objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection[0].Font.Name = "verdana";
        //////////////objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection[0].Font.Size = 7;
        //////////////objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection[0].Font.Bold = false;
        //////////////objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection[0].Font.Color = "Black";
        //////////////objChart.HasTitle = true;
        //////////////objChart.Title.Caption = "";
        //////////////objChart.HasLegend = true;
        ////////////////objChart.Axes[0].HasTitle = true;
        ////////////////objChart.Axes[0].Title.Caption = "Male Axis Caption";
        ////////////////objChart.Axes[1].HasTitle = true;
        ////////////////objChart.Axes[1].Title.Caption = "Female Axis Caption";
        //////////////string strSeriesName = "Person 1";
        //////////////double Total = TotArtPatient + TotNonArtPatient;

        //////////////string strValue = Math.Round(((TotArtPatient / Total) * 100)).ToString() + '\t' + Math.Round(((TotNonArtPatient / Total) * 100)).ToString();
        //////////////string strCategory = "ART Patient" + '\t' + "Non ART Patient";

        //////////////objChart.SeriesCollection[0].SetData(graph.ChartDimensionsEnum.chDimSeriesNames, (int)graph.ChartSpecialDataSourcesEnum.chDataLiteral, strSeriesName);
        //////////////objChart.SeriesCollection[0].SetData(graph.ChartDimensionsEnum.chDimCategories, (int)graph.ChartSpecialDataSourcesEnum.chDataLiteral, strCategory);
        //////////////objChart.SeriesCollection[0].SetData(graph.ChartDimensionsEnum.chDimValues, (int)graph.ChartSpecialDataSourcesEnum.chDataLiteral, strValue);
        //////////////objChart.Border.Color = "#33CC33";
        //////////////objChart.SeriesCollection[0].Points[0].Interior.Color = "f4a460";
        //////////////objChart.SeriesCollection[0].Points[1].Interior.Color = "ffd700";
        //////////////string strAbsolutePath = (Server.MapPath(".")) + ".\\Images\\Piechart1.GIF";
        //////////////objCSpace.ExportPicture(strAbsolutePath, "GIF", 300, 200);
        //////////////string strRelativePath = ".\\Images\\Piechart1.gif";
        //////////////ChartARTNonART.ImageUrl = strRelativePath;
        
    }
    void PerArtAge(double TotARTFMUpto2, double TotARTFMUpto5, double TotARTFMUpto15, double TotARTFMAbove15, double theTotArtPatient)
    {
        //if (Convert.ToInt32(TotARTFMUpto2) < 1 || Convert.ToInt32(TotARTFMUpto5) < 1 || Convert.ToInt32(TotARTFMUpto15) < 1 || Convert.ToInt32(TotARTFMAbove15) < 1 || Convert.ToInt32(theTotArtPatient) < 1)
        //    return;

      
         //noofArtbelow1=theTotArtPatient;
         noofArtupto2=TotARTFMUpto2;
         noofArtupto4=TotARTFMUpto5;
         noofArtupto15=TotARTFMUpto15;
         noofArtabove15=TotARTFMAbove15;


         ////graph.ChartSpace objCSpace = new graph.ChartSpaceClass();
         ////graph.ChChart objChart = objCSpace.Charts.Add(0);
         ////objChart.Type = graph.ChartChartTypeEnum.chChartTypeDoughnutExploded;
         ////objCSpace.Charts[0].ChartDepth = 125;
         ////objCSpace.Charts[0].AspectRatio = 80;
         ////objChart.HasLegend = false;
         ////objCSpace.Charts[0].SeriesCollection.Add(0);
         ////objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection.Add();
         ////objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection[0].HasPercentage = true;
         ////objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection[0].HasValue = false;
         ////objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection[0].HasCategoryName = true;
         ////objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection[0].Separator = "\n";
         ////objCSpace.Charts[0].SeriesCollection[0].Caption = String.Empty;
         ////objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection[0].Font.Name = "verdana";
         ////objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection[0].Font.Size = 7;
         ////objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection[0].Font.Bold = false;
         ////objCSpace.Charts[0].SeriesCollection[0].DataLabelsCollection[0].Font.Color = "Black";
         ////objChart.HasTitle = true;
         ////objChart.Title.Caption = "";
         ////objChart.HasLegend = true;
         //////objChart.Axes[0].HasTitle = true;
         //////objChart.Axes[0].Title.Caption = "Male Axis Caption";
         //////objChart.Axes[1].HasTitle = true;
         //////objChart.Axes[1].Title.Caption = "Female Axis Caption";
         ////string strSeriesName = "Person 1";
         ////string strValue = Math.Round(((TotARTFMUpto2 / theTotArtPatient) * 100)).ToString() + '\t' + Math.Round(((TotARTFMUpto5 / theTotArtPatient) * 100)).ToString() + '\t' + Math.Round(((TotARTFMUpto15 / theTotArtPatient) * 100)).ToString() + '\t' + Math.Round(((TotARTFMAbove15 / theTotArtPatient) * 100)).ToString();
         ////string strCategory = "0-1" + '\t' + "2-4" + '\t' + "5-14" + '\t' + "above 15";
         ////objChart.SeriesCollection[0].SetData(graph.ChartDimensionsEnum.chDimSeriesNames, (int)graph.ChartSpecialDataSourcesEnum.chDataLiteral, strSeriesName);
         ////objChart.SeriesCollection[0].SetData(graph.ChartDimensionsEnum.chDimCategories, (int)graph.ChartSpecialDataSourcesEnum.chDataLiteral, strCategory);
         ////objChart.SeriesCollection[0].SetData(graph.ChartDimensionsEnum.chDimValues, (int)graph.ChartSpecialDataSourcesEnum.chDataLiteral, strValue);
         ////objChart.Border.Color = "#33CC33";
         ////objChart.SeriesCollection[0].Points[0].Interior.Color = "e9967a";
         ////objChart.SeriesCollection[0].Points[1].Interior.Color = "9acd32";
         ////string strAbsolutePath = (Server.MapPath(".")) + ".\\Images\\Piechart2.GIF";
         ////objCSpace.ExportPicture(strAbsolutePath, "GIF", 300, 200);
         ////string strRelativePath = ".\\Images\\Piechart2.gif";
         ////ChartPerARTbyAGE.ImageUrl = strRelativePath;
       
    }
    protected void hlEverEnrolledPatients_Click(object sender, EventArgs e)
    {
        ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[0], "EverEnrolledPatient");
    }
    protected void hlFemalesEnrolled_Click(object sender, EventArgs e)
    {
        ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[1], "FemalesEnroll");
    }
    protected void hlMalesEnrolled_Click(object sender, EventArgs e)
    {
        ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[2], "MalesEnroll");

    }
    protected void hlTotalActivePatients_Click(object sender, EventArgs e)
    {
        ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[3], "ActivePatients");

    }
    protected void hlActiveNonARTPatients_Click(object sender, EventArgs e)
    {
        ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[4], "ActiveNonARTPatients");

    }
    protected void hlActiveARTPatients_Click(object sender, EventArgs e)
    {
        ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[5], "ActiveARTPatients");

    }
    protected void hlARTMortality_Click(object sender, EventArgs e)
    {
        ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[6], "ARTMortality");

    }
    protected void hlLosttoFollowUp_Click(object sender, EventArgs e)
    {
        IReports ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports, BusinessProcess.Reports");
        DataTable dtLosttoFollowupPatientReport = (DataTable)ReportDetails.GetLosttoFollowupPatientReport(Convert.ToInt32(Session["AppLocationId"]), Session["SystemId"].ToString()).Tables[0];
        string FName = "LstFollowup";
        IQWebUtils theUtils = new IQWebUtils();
        string thePath = Server.MapPath(".\\ExcelFiles\\" + FName + ".xls");
        string theTemplatePath = Server.MapPath(".\\ExcelFiles\\IQCareTemplate.xls");
        theUtils.ExporttoExcel(dtLosttoFollowupPatientReport, Response);
        Response.Redirect(".\\ExcelFiles\\" + FName + ".xls");

    }
    protected void hlDueforTermination_Click(object sender, EventArgs e)
    {
        IReports ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports, BusinessProcess.Reports");
        System.Data.DataSet dsARTUnknown = ReportDetails.GetPtnotvisitedrecentlyUnknown(Application["AppCurrentDate"].ToString(), Application["AppCurrentDate"].ToString(), Convert.ToInt32(Session["AppLocationId"]));
        string FName = "ARTunknown";
        IQWebUtils theUtils = new IQWebUtils();
        string thePath = Server.MapPath(".\\ExcelFiles\\" + FName + ".xls");
        string theTemplatePath = Server.MapPath(".\\ExcelFiles\\IQCareTemplate.xls");
        theUtils.ExporttoExcel(dsARTUnknown.Tables[0], Response);
        Response.Redirect(".\\ExcelFiles\\" + FName + ".xls");

    }
    protected void hlNonARTMUpto2_Click(object sender, EventArgs e)
    {
        ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[7], "NonARTMale0-1");

    }
    protected void hlNonARTMUpto4_Click(object sender, EventArgs e)
    {
        ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[8], "NonARTMale2-4");

    }
    protected void hlNonARTMUpto14_Click(object sender, EventArgs e)
    {
        ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[9], "NonARTMale5-14");
    }
    protected void hlNonARTMAbove15_Click(object sender, EventArgs e)
    {
        ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[10], "NonARTMaleabove15");
    }
    protected void hlNonARTFUpto2_Click(object sender, EventArgs e)
    {
        ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[11], "NonARTFemale0-1");

    }
    protected void hlNonARTFupto2_Click(object sender, EventArgs e)
    {
        ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[12], "NonARTFemale2-4");

    }
    protected void hlNonARTFUpto14_Click(object sender, EventArgs e)
    {
        ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[13], "NonARTFemale5-14");
    }
    protected void hlNonARTFAbove15_Click(object sender, EventArgs e)
    {
        ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[14], "NonARTFemaleabove15");
    }
    protected void hlARTMUpto2_Click(object sender, EventArgs e)
    {
        ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[15], "ARTMale0-1");
    }
    protected void hlARTMUpto4_Click(object sender, EventArgs e)
    {
        ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[16], "ARTMale2-4");

    }
    protected void hlARTMUpto14_Click(object sender, EventArgs e)
    {
        ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[17], "ARTMale5-14");

    }
    protected void hlARTMAbove15_Click(object sender, EventArgs e)
    {
        ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[18], "ARTMaleabove15");

    }
    protected void hlARTFUpto2_Click(object sender, EventArgs e)
    {
        ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[19], "ARTFemale0-1");

    }
    protected void hlARTFUpto4_Click(object sender, EventArgs e)
    {
        ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[20], "ARTFemale2-4");
    }
    protected void hlARTFUpto14_Click(object sender, EventArgs e)
    {
        ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[21], "ARTFemale5-14");

    }
    protected void hlARTFAbove15_Click(object sender, EventArgs e)
    {
        ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[22], "ARTFemaleabove15");

    }
}
