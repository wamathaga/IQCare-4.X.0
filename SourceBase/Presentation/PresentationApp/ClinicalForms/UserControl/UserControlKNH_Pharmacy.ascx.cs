using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Interface.Clinical;
using System.Data;
using Application.Common;
using Application.Presentation;
using System.Collections;

namespace PresentationApp.ClinicalForms.UserControl
{
    public partial class UserControlKNH_Pharmacy : System.Web.UI.UserControl
    {
        IKNHStaticForms ExpressFormManager;
        DataView theDV, theDVCodeID;
        DataTable theDT;
        DataSet theDSXML = new DataSet();
        IQCareUtils theUtils = new IQCareUtils();
        BindFunctions BindManager = new BindFunctions();

        protected void Page_Load(object sender, EventArgs e)
        {
            ExpressFormManager = (IKNHStaticForms)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHStaticForms, BusinessProcess.Clinical");
            if (!IsPostBack)
            {
                
                GetLastregimenDispensed();
                fillDropDown();
            }

            
        }

        

        public void GetLastregimenDispensed()
        {
            DataSet lastRegimenDispensedDS = ExpressFormManager.GetLastRegimenDispensed(Convert.ToInt32(Session["PatientId"]));

            if (lastRegimenDispensedDS.Tables[1].Rows.Count > 0)
            {
                lblLastRegimenDispensed.Text = lastRegimenDispensedDS.Tables[1].Rows[0][0].ToString();
            }
            else
            {
                lblLastRegimenDispensed.Text = "";
            }
        }

        public void fillDropDown()
        {
            theDSXML.ReadXml(MapPath("..\\..\\XMLFiles\\AllMasters.con"));

            theDVCodeID = new DataView(theDSXML.Tables["Mst_Code"]);
            theDVCodeID.RowFilter = "Name='ARV Therapy Plan'";
            theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
            //theDV.RowFilter = "CodeID=146";
            theDV.RowFilter = "CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
            theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            theDT.DefaultView.ToTable(true, "Name");
            BindManager.BindCombo(ddlTreatmentplan, theDT, "Name", "ID");
            ddlTreatmentplan.Attributes.Add("OnChange", "getSelectedtableValue('divEligiblethrough','" + ddlTreatmentplan.ClientID + "','Start new treatment (naive patient)','DIVTreatmentplan');getSelectedtableValue('divARTchangecode','" + ddlTreatmentplan.ClientID + "','Change regimen','DIVTreatmentplan');getSelectedtableValue('divReasonforswitchto2ndlineregimen','" + ddlTreatmentplan.ClientID + "','Switch to second line','DIVTreatmentplan');getSelectedtableValue('divARTstopcode','" + ddlTreatmentplan.ClientID + "','Stop treatment','DIVTreatmentplan')");
            //ddlTreatmentplan.Attributes.Add("OnChange", "getSelectedtableValue('divEligiblethrough','" + ddlTreatmentplan.ClientID + "','Start ART','DIVTreatmentplan');getSelectedtableValue('divARTchangecode','" + ddlTreatmentplan.ClientID + "','Substitute regimen','DIVTreatmentplan');getSelectedtableValue('divReasonforswitchto2ndlineregimen','" + ddlTreatmentplan.ClientID + "','Switch to second lin','DIVTreatmentplan');getSelectedtableValue('divARTstopcode','" + ddlTreatmentplan.ClientID + "','Stop treatment','DIVTreatmentplan')");
            //ARTTreatmentPlan

            theDVCodeID = new DataView(theDSXML.Tables["Mst_Code"]);
            theDVCodeID.RowFilter = "Name='2ndLineRegimenSwitch'";
            theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
            theDV.RowFilter = "CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
            theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            BindManager.BindCombo(ddlReasonforswitchto2ndlineregimen, theDT, "Name", "ID");

            theDVCodeID = new DataView(theDSXML.Tables["Mst_Code"]);
            theDVCodeID.RowFilter = "Name='ARTEligibility'";
            theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
            theDV.RowFilter = "CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
            theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            BindManager.BindCheckedList(chklistEligiblethrough, theDT, "Name", "ID");

            theDVCodeID = new DataView(theDSXML.Tables["Mst_Code"]);
            theDVCodeID.RowFilter = "Name='ARTchangecode'";
            theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
            theDV.RowFilter = "CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
            theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            BindManager.BindCheckedList(chklistARTchangecode, theDT, "Name", "ID");

            theDVCodeID = new DataView(theDSXML.Tables["Mst_Code"]);
            theDVCodeID.RowFilter = "Name='ARTstopcode'";
            theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
            theDV.RowFilter = "CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
            theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            BindManager.BindCheckedList(chklistARTstopcode, theDT, "Name", "ID");

            chklistARTchangecode.Attributes.Add("OnClick", "CheckBoxHideUnhide('" + chklistARTchangecode.ClientID + "','divSpecifyotherARTchangereason','Other')");
            chklistEligiblethrough.Attributes.Add("OnClick", "CheckBoxHideUnhide('" + chklistEligiblethrough.ClientID + "','divOtherEligibility','Other')");
            chklistARTstopcode.Attributes.Add("OnClick", "CheckBoxHideUnhide('" + chklistARTstopcode.ClientID + "','divARTstopcodeother','Other patient decisi')");

            //BindDropdown(ddlTreatmentplan, "ARTTreatmentPlan");
            //BindChkboxlstControl(chklistEligiblethrough, "ARTEligibility");
            //BindChkboxlstControl(chklistARTchangecode, "ARTchangecode");
            //BindChkboxlstControl(chklistARTstopcode, "ARTstopcode");
            //BindDropdown(ddlReasonforswitchto2ndlineregimen, "2ndLineRegimenSwitch");
        }

        protected void btnPrescribeDrugs_Click(object sender, EventArgs e)
        {
            Session["PharmacyUCPatientVisitId"] = Session["PatientVisitId"];
            Session["TherapyPlan"] = ddlTreatmentplan.SelectedValue;
            if (ddlTreatmentplan.SelectedValue == "0")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "selectTreatmentPlan", "alert('Please select the treatment plan.');", true);
            }
            else
            {
                Hashtable HT = PharmacyHT();
                
                ExpressFormManager.SaveUpdateARVTherapy(HT);
                IQCareUtils.Redirect("../PharmacyDispense/frmPharmacyDispense_PatientOrder.aspx?opento=ArtForm&LastRegimenDispensed=True", "_blank", "toolbars=no,location=no,directories=no,dependent=yes,top=100,left=30,maximize=no,resize=no,width=1000,height=800,scrollbars=yes");
            }
        }

        protected Hashtable PharmacyHT()
        {

            Hashtable theHT = new Hashtable();
            try
            {
                theHT.Add("patientID", Convert.ToInt32(Session["PatientId"]));
                theHT.Add("visitID", Convert.ToInt32(Session["PatientVisitId"]));
                theHT.Add("userID", Convert.ToInt32(Session["AppUserId"]));
                theHT.Add("locationID", Convert.ToInt32(Session["AppLocationID"]));
                theHT.Add("treatmentPlan", Convert.ToInt32(ddlTreatmentplan.SelectedValue));


                theHT.Add("noOfDrugsSubstituted", txtNoofdrugssubstituted.Text);
                theHT.Add("reasonForSwitchTo2ndLineRegimen", Convert.ToInt32(ddlReasonforswitchto2ndlineregimen.SelectedValue));
                theHT.Add("specifyOtherEligibility", txtSpecifyOtherEligibility.Text);
                theHT.Add("specifyotherARTchangereason", txtSpecifyotherARTchangereason.Text);
                theHT.Add("specifyOtherStopCode", txtSpecifyOtherStopCode.Text);
                
            }
            catch (Exception err)
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theMsg, this);
            }
            return theHT;

        }

        protected void ddlTreatmentplan_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}