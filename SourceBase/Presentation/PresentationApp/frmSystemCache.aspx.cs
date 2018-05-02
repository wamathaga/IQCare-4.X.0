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
using Application.Common;
using Application.Presentation;
using Interface.Security;
using Interface.Reports;

public partial class frmSystemCache : LogPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
        {
            IQCareMsgBox.Show("SessionExpired", this);
            Response.Redirect("~/frmlogin.aspx", true);
        }
        #region Commented Old Code for GenerateCache
        //System.IO.FileInfo theFileInfo1 = new System.IO.FileInfo(Server.MapPath(".\\XMLFiles\\AllMasters.con").ToString());
        //System.IO.FileInfo theFileInfo2 = new System.IO.FileInfo(Server.MapPath(".\\XMLFiles\\DrugMasters.con").ToString());
        //System.IO.FileInfo theFileInfo3 = new System.IO.FileInfo(Server.MapPath(".\\XMLFiles\\LabMasters.con").ToString());
        //System.IO.FileInfo theFileInfo4 = new System.IO.FileInfo(Server.MapPath(".\\XMLFiles\\Frequency.xml").ToString());
        //theFileInfo1.Delete();
        //theFileInfo2.Delete();
        //theFileInfo3.Delete();
        //theFileInfo4.Delete();

        //IIQCareSystem theCacheManager = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem,BusinessProcess.Security");
        //DataSet theMainDS = theCacheManager.GetSystemCache();
        //DataSet WriteXMLDS = new DataSet();

        //WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_CouncellingType"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_CouncellingTopic"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Provider"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Division"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Ward"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_District"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Reason"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Education"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Designation"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Employee"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Occupation"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Province"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Village"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Code"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_HIVAIDSCareTypes"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_ARTSponsor"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_HivDisease"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Assessment"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Symptom"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Decode"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Feature"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Function"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_HivDisclosure"].Copy());
        ////WriteXMLDS.Tables.Add(theMainDS.Tables["mst_Satellite"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_LPTF"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["mst_StoppedReason"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["mst_facility"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_HIVCareStatus"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_RelationshipType"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_TBStatus"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_ARVStatus"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_LostFollowreason"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Regimen"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_pmtctDeCode"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Module"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_ModDecode"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_ARVSideEffects"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_ModCode"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Country"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Town"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["VWDiseaseSymptom"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["VW_ICDList"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["mst_RegimenLine"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["mst_Store"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["mst_BlueCode"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["mst_BlueDecode"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_FormBuilderTab"].Copy());
        //WriteXMLDS.WriteXml(Server.MapPath(".\\XMLFiles\\").ToString() + "AllMasters.con",XmlWriteMode.WriteSchema);

        //WriteXMLDS.Tables.Clear();
        //WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Strength"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_FrequencyUnits"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Drug"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Generic"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_DrugType"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Frequency"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_DrugSchedule"].Copy());
        //WriteXMLDS.WriteXml(Server.MapPath(".\\XMLFiles\\").ToString() + "DrugMasters.con",XmlWriteMode.WriteSchema);

        //WriteXMLDS.Tables.Clear();
        //WriteXMLDS.Tables.Clear();
        //WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_LabTest"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["Lnk_TestParameter"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["Lnk_LabValue"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["Lnk_ParameterResult"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["LabTestOrder"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["mst_PatientLabPeriod"].Copy());
        //WriteXMLDS.WriteXml(Server.MapPath(".\\XMLFiles\\").ToString() + "LabMasters.con",XmlWriteMode.WriteSchema);

        //WriteXMLDS.Tables.Clear();
        //WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Frequency"].Copy());
        //WriteXMLDS.WriteXml(Server.MapPath(".\\XMLFiles\\").ToString() + "Frequency.xml", XmlWriteMode.WriteSchema);
        #endregion
        /*
         * Calling generate cache from common location
         * Update By: Gaurav 
         * Update Date: 8 July 2014
         */
        string code = Request.QueryString["Code"].ToString();
        try
        {
            if ((string.IsNullOrEmpty(code)) || (code == "1"))
            {
                IQCareUtils.GenerateCache(true);
            }
            else if (code == "2")
            {
                try
                {
                    IReportIQTools IQToolsReport = (IReportIQTools)ObjectFactory.CreateInstance("BusinessProcess.Reports.IQToolsReport,BusinessProcess.Reports");
                    IQToolsReport.IQToolsRefresh();
                }
                catch (Exception exp)
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["MessageText"] = exp.Message.ToString();
                    IQCareMsgBox.Show("#C1", theBuilder, this);
                }
            }
            DataSet theDS = new DataSet();
            theDS.ReadXml(MapPath("XMLFiles\\ALLMasters.con"));
            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();
            if (theDS.Tables["Mst_Facility"] != null)
            {
                DataView theDV = new DataView(theDS.Tables["Mst_Facility"]);
                theDV.RowFilter = "DeleteFlag=0 and FacilityId=" + Convert.ToInt32(Session["AppLocationId"]);
                if (theDV.Table != null)
                {
                    DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    HttpContext.Current.Session["Billing"] = theDT.Rows[0]["Billing"].ToString();
                    HttpContext.Current.Session["Wards"] = theDT.Rows[0]["Wards"].ToString();
                    if (theDT.Rows[0]["PMSCM"].ToString() == "1")
                    {
                        Session["SCMModule"] = "PMSCM";
                    }
                    else { Session["SCMModule"] = null; }
                }
            }

        }
        catch (Exception err)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
        }
        finally
        {
            Response.Redirect("frmFacilityHome.aspx");
        }


    }
}
