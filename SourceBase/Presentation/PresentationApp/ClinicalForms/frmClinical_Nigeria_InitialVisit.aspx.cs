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
using Interface.Clinical;
using Interface.Security;
using Application.Presentation;
using Application.Common;
using Application.Interface;
using System.Text;
using Interface.Administration;

namespace PresentationApp.ClinicalForms
{
    public partial class frmClinical_Nigeria_InitialVisit : BasePage
    {
        INigeriaARTCard NigAdultIE;
        
        protected void Page_Init(object sender, EventArgs e)
        {
            //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Clinical Forms >> ";
            //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Initial Visit";
            //(Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Initial Visit";
            txtConfHIVdate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3');");
            txtConfHIVdate.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'3')");
            txtMedElligDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3');");
            txtMedElligDate.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'3')");
            txtIntAdhCounsling.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3');");
            txtIntAdhCounsling.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'3')");
            txtTransferedIn.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3');");
            txtTransferedIn.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'3')");
            BindLists();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            NigAdultIE = (INigeriaARTCard)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BNigeriaARTCard, BusinessProcess.Clinical");
            DataTable DT = NigAdultIE.GetNigeriaInitialVisitId(Convert.ToInt32(Session["PatientId"].ToString()));
            if (DT.Rows.Count > 0)
            {
                Session["PatientVisitId"] = DT.Rows[0]["Visit_Id"].ToString();
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
                {
                    BindExistingData();
                    //ErrorLoad();
                }
                
            }
           
        }
        public void BindControl(Control cntrl, string fieldname)
        {
            DataTable thedeCodeDT = new DataTable();
            IQCareUtils iQCareUtils = new IQCareUtils();
            BindFunctions BindManager = new BindFunctions();
            DataSet theDSXML = new DataSet();
            theDSXML.ReadXml(MapPath("..\\XMLFiles\\AllMasters.con"));


            DataView theCodeDV = new DataView(theDSXML.Tables["MST_CODE"]);
            theCodeDV.RowFilter = "DeleteFlag=0 and Name='" + fieldname + "'";
            DataTable theCodeDT = (DataTable)iQCareUtils.CreateTableFromDataView(theCodeDV);
            DataView theDV = new DataView(theDSXML.Tables["MST_DECODE"]);
            if (fieldname.ToString() != "")
            {
                if (theCodeDT.Rows.Count > 0)
                {

                    theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and CodeID=" + theCodeDT.Rows[0]["CodeId"];
                    theDV.Sort = "SRNo ASC";
                    thedeCodeDT = (DataTable)iQCareUtils.CreateTableFromDataView(theDV);
                }
                if (cntrl is CheckBoxList)
                {
                    if (thedeCodeDT.Rows.Count > 0)
                    {
                        BindManager.BindCheckedList((CheckBoxList)cntrl, thedeCodeDT, "Name", "ID");
                    }
                }
                else if (cntrl is DropDownList)
                {
                    if (thedeCodeDT.Rows.Count > 0)
                    {
                        BindManager.BindCombo((DropDownList)cntrl, thedeCodeDT, "Name", "ID");
                    }
                }
                else if (cntrl is RadioButtonList)
                {
                    if (thedeCodeDT.Rows.Count > 0)
                    {
                        BindManager.RadioButtonList((RadioButtonList)cntrl, thedeCodeDT, "Name", "ID");
                    }
                }

            }
            else
            {
                theDV = new DataView(theDSXML.Tables["MST_DECODE"]);
                if (theDV.Table.Rows.Count > 0)
                {
                    theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and CodeID=17 and ModuleId=209";
                    theDV.Sort = "SRNo ASC";
                    thedeCodeDT = new DataTable();
                    thedeCodeDT = (DataTable)iQCareUtils.CreateTableFromDataView(theDV);
                }
                if (cntrl is CheckBoxList)
                {
                    if (thedeCodeDT.Rows.Count > 0)
                    {
                        BindManager.BindCheckedList((CheckBoxList)cntrl, thedeCodeDT, "Name", "ID");
                    }
                }
                else if (cntrl is DropDownList)
                {
                    if (thedeCodeDT.Rows.Count > 0)
                    {
                        BindManager.BindCombo((DropDownList)cntrl, thedeCodeDT, "Name", "ID");
                    }
                }
                else if (cntrl is RadioButtonList)
                {
                    if (thedeCodeDT.Rows.Count > 0)
                    {
                        BindManager.RadioButtonList((RadioButtonList)cntrl, thedeCodeDT, "Name", "ID");
                    }
                }
            }

        }
        private void BindLists()
        {
            BindControl(ddlCareEntryPoint, "");
            ddlCareEntryPoint.Attributes.Add("OnChange", "getSelectedValue('divCareEntryPointOther','" + ddlCareEntryPoint.ClientID + "','Other (specify)');");
            BindControl(ddlPriorART, "EntryType");
            BindControl(ddlwhyelligible, "WhyEligible ");
            BindControl(ddlModeHIVTest, "NigeriaHIVTestType");
            BindFacilitydropdwn();
            
        }
        private void BindFacilitydropdwn()
        {
            IUser theLocationManager;
            theLocationManager = (IUser)ObjectFactory.CreateInstance("BusinessProcess.Security.BUser, BusinessProcess.Security");
            DataTable theDT = theLocationManager.GetFacilityList();
            BindFunctions theBindManger = new BindFunctions();
            theBindManger.BindCombo(ddlFacTransfFrom, theDT, "FacilityName", "FacilityId");
            
        }

        public void Save(int dqchk)
        {            
            if (fieldValidation() == false)
            {
                //ErrorLoad();
                return;
            }
            Hashtable theHT = HtParameters();
            
            DataSet DsReturns = new DataSet();
            NigAdultIE = (INigeriaARTCard)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BNigeriaARTCard, BusinessProcess.Clinical");               
            DsReturns = NigAdultIE.SaveUpdateInitialVisitData(theHT, dqchk, Convert.ToInt32(Session["AppUserId"]));
                
            Session["Redirect"] = "0";
            if (Convert.ToInt32(DsReturns.Tables[0].Rows[0]["Visit_Id"]) > 0)
            {
                Session["PatientVisitId"] = Convert.ToInt32(DsReturns.Tables[0].Rows[0]["Visit_Id"]);
                SaveCancel();                
            }
        }
        private Hashtable HtParameters()
        {           
            Hashtable theHT = new Hashtable();
            try
            {
                theHT.Add("patientID", Session["PatientId"]);
                theHT.Add("visitID", Session["PatientVisitId"]);
                theHT.Add("locationID", Session["AppLocationId"]);                
                
                theHT.Add("CareEntry", ddlCareEntryPoint.SelectedValue);
                theHT.Add("OtherCareEntry", txtCareEntryOther.Text);
                //////////Prior HIV Care//////////////
                theHT.Add("ConfirmHIVDate", txtConfHIVdate.Value);
                theHT.Add("ModeHIVTest", ddlModeHIVTest.SelectedValue);
                theHT.Add("TestLocation", txtmodetestwhere.Text);
                theHT.Add("PriorART", ddlPriorART.SelectedValue);

                //////////ART Commencement//////////////
                theHT.Add("ElligibleDate", txtMedElligDate.Value);
                theHT.Add("WhyElligible", ddlwhyelligible.SelectedValue);
                theHT.Add("AdhCounslingDate", txtIntAdhCounsling.Value);
                theHT.Add("DateTransferedIn", txtTransferedIn.Value);
                theHT.Add("FacilityTransferFrom", ddlFacTransfFrom.SelectedValue);

                return theHT;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private Boolean fieldValidation()
        {
            return true;
        }
        public void BindExistingData()
        {
            string script = string.Empty;

            if (Convert.ToInt32(Session["PatientVisitId"].ToString()) > 0)
            {
                NigAdultIE = (INigeriaARTCard)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BNigeriaARTCard, BusinessProcess.Clinical");
                DataSet dsGet = NigAdultIE.GetNigeriaInitialVisitDetails(Convert.ToInt32(Session["PatientId"].ToString()), Convert.ToInt32(Session["PatientVisitId"].ToString()));
                if (dsGet.Tables[0].Rows.Count > 0)
                {                  

                    if (dsGet.Tables[0].Rows[0]["AdhCounsellingDate"] != DBNull.Value)
                    {
                        if((Convert.ToDateTime(dsGet.Tables[0].Rows[0]["AdhCounsellingDate"]).Year != 1900))
                            txtIntAdhCounsling.Value = String.Format("{0:dd-MMM-yyyy}", dsGet.Tables[0].Rows[0]["AdhCounsellingDate"]);
                    }
                    if (dsGet.Tables[0].Rows[0]["Careeentry"] != DBNull.Value)
                    {
                        ddlCareEntryPoint.SelectedValue = dsGet.Tables[0].Rows[0]["Careeentry"].ToString();
                        if (ddlCareEntryPoint.SelectedItem.Text.ToUpper() == "OTHER (SPECIFY)")
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Careeentry", "$('#" + ddlCareEntryPoint.ClientID + "').trigger('onchange');", true);
                        }
                    }
                    if (dsGet.Tables[0].Rows[0]["OtherCareEntry"] != DBNull.Value)
                        txtCareEntryOther.Text = dsGet.Tables[0].Rows[0]["OtherCareEntry"].ToString();
                }
                if (dsGet.Tables[1].Rows.Count > 0)
                {

                    if (dsGet.Tables[1].Rows[0]["ConfirmHIVPosDate"] != DBNull.Value)
                    {
                        if ((Convert.ToDateTime(dsGet.Tables[1].Rows[0]["ConfirmHIVPosDate"]).Year != 1900))
                            txtConfHIVdate.Value = String.Format("{0:dd-MMM-yyyy}", dsGet.Tables[1].Rows[0]["ConfirmHIVPosDate"]);
                    }
                    if (dsGet.Tables[1].Rows[0]["HIVTestType"] != DBNull.Value)
                        ddlModeHIVTest.SelectedValue = dsGet.Tables[1].Rows[0]["HIVTestType"].ToString();
                    if (dsGet.Tables[1].Rows[0]["TestLocationOther"] != DBNull.Value)
                        txtmodetestwhere.Text = dsGet.Tables[1].Rows[0]["TestLocationOther"].ToString();
                    if (dsGet.Tables[1].Rows[0]["PrevART"] != DBNull.Value)
                        ddlPriorART.SelectedValue = dsGet.Tables[1].Rows[0]["PrevART"].ToString();
                }
                if (dsGet.Tables[2].Rows.Count > 0)
                {

                    if (dsGet.Tables[2].Rows[0]["dateEligible"] != DBNull.Value)
                    {
                        if ((Convert.ToDateTime(dsGet.Tables[2].Rows[0]["dateEligible"]).Year != 1900))
                            txtMedElligDate.Value = String.Format("{0:dd-MMM-yyyy}", dsGet.Tables[2].Rows[0]["dateEligible"]);
                    }
                    if (dsGet.Tables[2].Rows[0]["eligibleThrough"] != DBNull.Value)
                        ddlwhyelligible.SelectedValue = dsGet.Tables[2].Rows[0]["eligibleThrough"].ToString();
                }
                if (dsGet.Tables[3].Rows.Count > 0)
                {

                    if (dsGet.Tables[3].Rows[0]["ARTTransferInDate"] != DBNull.Value)
                    {
                        if ((Convert.ToDateTime(dsGet.Tables[3].Rows[0]["ARTTransferInDate"]).Year != 1900))
                            txtTransferedIn.Value = String.Format("{0:dd-MMM-yyyy}", dsGet.Tables[3].Rows[0]["ARTTransferInDate"]);
                    }
                    if (dsGet.Tables[3].Rows[0]["ARTTransferInFrom"] != DBNull.Value)
                        ddlFacTransfFrom.SelectedValue = dsGet.Tables[3].Rows[0]["ARTTransferInFrom"].ToString();
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Save(0);
        }

        protected void btndataquality_Click(object sender, EventArgs e)
        {
            Save(1);
        }

        protected void btnclose_Click(object sender, EventArgs e)
        {

        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {

        }
        private void SaveCancel()
        {
            IQCareMsgBox.NotifyAction("Initial Visit Form saved successfully.", "Initial Visit Form", false, this, "window.location.href='frmPatient_History.aspx?sts=" + 0 + "';");
        }
    }
}