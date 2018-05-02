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
    public partial class UserControl_NigeriaTreatmentPlan : System.Web.UI.UserControl
    {
        DataView theDV, theDVCodeID;
        DataTable theDT;
        DataSet theDSXML = new DataSet();
        IQCareUtils theUtils = new IQCareUtils();
        BindFunctions BindManager = new BindFunctions();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                BindControl();
                fillDropDown();
            }
        }

        protected void btnPrescribeDrugs_Click(object sender, EventArgs e)
        {        
            Session["PharmacyUCPatientVisitId"] = Session["PatientVisitId"];
            IQCareUtils.Redirect("../PharmacyDispense/frmPharmacyDispense_PatientOrder.aspx?opento=ArtForm&LastRegimenDispensed=True", "_blank", "toolbars=no,location=no,directories=no,dependent=yes,top=100,left=30,maximize=no,resize=no,width=1000,height=800,scrollbars=yes");
                    
        }
        public void BindControl()
        {
            IQCareUtils iQCareUtils = new IQCareUtils();
            BindFunctions bindFunctions = new BindFunctions();
            DataTable dt = new DataTable();

            dt = iQCareUtils.GetDataTable("MST_CODE", "NigeriaListLabEvaluation");
                   
            if (dt.Rows.Count > 0)
            {
                gvtreatment.DataSource = dt;
                gvtreatment.DataBind();
            }
            dt = new DataTable();
            dt = iQCareUtils.GetDataTable("MST_CODE", "NigeriaListEnrollin");

            if (dt.Rows.Count > 0)
            {
                gvenrollin.DataSource = dt;
                gvenrollin.DataBind();
            }
            
        }
        protected void gvtreatment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl = (Label)e.Row.FindControl("lbltreatment");
                CheckBox Chktreatment = (CheckBox)e.Row.FindControl("Chktreatment");
                TextBox txtPresenting = (TextBox)e.Row.FindControl("txtother");
                if (Chktreatment.Text.ToUpper() == "OTHER REFERRALS")
                {
                    Chktreatment.Attributes.Add("onchange", "ShowHideDiv('DivTreatmentOther');");
                    
                }
            }
        }
        

        public void fillDropDown()
        {
            theDSXML.ReadXml(MapPath("..\\..\\XMLFiles\\AllMasters.con"));

            theDVCodeID = new DataView(theDSXML.Tables["Mst_Code"]);
            theDVCodeID.RowFilter = "Name='NigeriaARVTreamentPlan'";
            theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
            if (theDVCodeID.Table.Rows.Count > 0)
            {
                theDV.RowFilter = "CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
                theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                theDT.DefaultView.ToTable(true, "Name");
                BindManager.BindCombo(ddlTreatmentplan, theDT, "Name", "ID");
            }
            ddlTreatmentplan.Attributes.Add("OnChange", "getSelectedtableValue('divARTchangecode','" + ddlTreatmentplan.ClientID + "','Change Treatment','DIVTreatmentplan');");
            //ddlTreatmentplan.Attributes.Add("OnChange", "getSelectedtableValue('divEligiblethrough','" + ddlTreatmentplan.ClientID + "','Start ART','DIVTreatmentplan');getSelectedtableValue('divARTchangecode','" + ddlTreatmentplan.ClientID + "','Substitute regimen','DIVTreatmentplan');getSelectedtableValue('divReasonforswitchto2ndlineregimen','" + ddlTreatmentplan.ClientID + "','Switch to second lin','DIVTreatmentplan');getSelectedtableValue('divARTstopcode','" + ddlTreatmentplan.ClientID + "','Stop treatment','DIVTreatmentplan')");
            //ARTTreatmentPlan  
            if (theDVCodeID.Table.Rows.Count > 0)
            {
                theDVCodeID = new DataView(theDSXML.Tables["Mst_Code"]);
                theDVCodeID.RowFilter = "Name='NigeriaARVTreamentChangeReason'";
                theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                theDV.RowFilter = "CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
                theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                BindManager.BindCheckedList(chklistARTchangecode, theDT, "Name", "ID");
            }

            chklistARTchangecode.Attributes.Add("OnClick", "CheckBoxHideUnhide('" + chklistARTchangecode.ClientID + "','divSpecifyotherARTchangereason','Other(Specify)')");
            

        }
        protected void ddlTreatmentplan_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}