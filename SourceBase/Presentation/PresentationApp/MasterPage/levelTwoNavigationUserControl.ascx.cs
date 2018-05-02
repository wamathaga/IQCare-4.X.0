using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using Interface.Security;
using Application.Presentation;
using Application.Common;
using Interface.Clinical;
using System.Configuration;
using Interface.SCM;
using System.Web.Services;


public partial class MasterPage_levelTwoNavigationUserControl : System.Web.UI.UserControl
{
    string ObjFactoryParameter = "BusinessProcess.Clinical.BCustomForm, BusinessProcess.Clinical";
    string ModuleId = "";
    int PtnPMTCTStatus;
    int PtnARTStatus;
    string PMTCTNos = "";
    string ARTNos = "";
    public int PatientId = 0;


    protected void Page_Load(object sender, EventArgs e)
    {
        setSessionIds_Patient();
        try
        {
            //Ajax.Utility.RegisterTypeForAjax(typeof(MasterPage_levelTwoNavigationUserControl));
            string url = Request.RawUrl.ToString();
            Application["PrvFrm"] = url;
            lblpntStatus.Visible = false;

            if (!IsPostBack)
            {
                Init_Menu();
                Load_MenuRegistration();
                Load_MenuCreateNewForm();
                //RTyagi..19Feb 07..
                AuthenticationRights();
            }
            //Added by Akhil - 10/14-/2014
            //Extruder control inclusion for patient
            if (Session["SCMModule"] == null)
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuDebitNote");
            //patientLevelMenu.Attributes.Add("onClick", "alert('executed');");
            patientLevelMenu.MenuItemClick += patientLevelMenu_MenuItemClick1;

            lblpntStatus.Text = "";
            lblpntStatus.Visible = false;
        }
        catch (Exception err)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
        }
    }

    protected void patientLevelMenu_MenuItemClick(object sender, MenuEventArgs e)
    {

        //Response.Redirect(e.Item.Value);
    }

    #region "Hide menu item by value"
    public void RemoveMenuItemByValue(MenuItemCollection items, String value)
    {
        List<MenuItem> rmvMenuItem = new List<MenuItem>();

        //Breadth first, look in the collection
        foreach (MenuItem item in items)
        {
            if (item.Value == value)
            {
                rmvMenuItem.Add(item);
            }
        }

        if (rmvMenuItem.ToArray().Length != 0)
        {
            for (int j = 0; j < rmvMenuItem.ToArray().Length; j++)
            {
                items.Remove(rmvMenuItem[j]);
            }
        }

        //Search children
        foreach (MenuItem item in items)
        {
            RemoveMenuItemByValue(item.ChildItems, value);
        }
    }
    #endregion

    #region "Assign URL by value"
    public void AssignUrl(MenuItemCollection items, String value, String url)
    {
        foreach (MenuItem item in items)
        {
            if (item.Value == value)
            {
                item.NavigateUrl = url;
            }
        }

        foreach (MenuItem item in items)
        {
            AssignUrl(item.ChildItems, value, url);
        }
    }
    #endregion

    #region "Assign Attributes"
    //patientLevelMenu.Attributes.Add("onClick", "fnSetformID('" + theDR["FeatureID"].ToString() + "');");
    public void AssignAttribute(MenuItemCollection items, String value, String url)
    {
        foreach (MenuItem item in items)
        {
            if (item.Value == value)
            {
                //patientLevelMenu.Attributes.Add("onClick", "window.open('" + url + "','','toolbars=no,location=no,directories=no,dependent=yes,top=100,left=30,maximize=no,resize=no,width=1000,height=500,scrollbars=yes');return false;");
                Page.ClientScript.RegisterStartupScript(typeof(Page), "SymbolError", "<script type='text/javascript'>function openWin1() { window.open('" + url + "','','toolbars=no,location=no,directories=no,dependent=yes,top=100,left=30,maximize=no,resize=no,width=1000,height=700,scrollbars=yes');};</script>");

                //item.NavigateUrl = "javascript:window.open('" + url + "','','toolbars=no,location=no,directories=no,dependent=yes,top=100,left=30,maximize=no,resize=no,width=1000,height=500,scrollbars=yes');";
                item.NavigateUrl = "javascript:openWin1();";
            }
        }

        foreach (MenuItem item in items)
        {
            AssignAttribute(item.ChildItems, value, url);
        }
    }
    #endregion

    #region "Disable Menu Items"
    private void disableMenuItem()
    {
        //patientLevelMenu.Items[0].Enabled = false;
        //for (int i = 0; i < patientLevelMenu.Items[0].ChildItems.Count; i++)
        //{
        //    patientLevelMenu.Items[0].ChildItems[i].Selectable = false;
        //}
        patientLevelMenu.Items[1].Selectable = false;
        patientLevelMenu.FindItem("mnuClinicalDeleteForm").Selectable = false;
        patientLevelMenu.FindItem("mnuClinicalDeleteForm").Enabled = false;
        //patientLevelMenu.Items[2].Enabled = false;
        //patientLevelMenu.Items[3].Enabled = false;
        //patientLevelMenu.Items[4].Enabled = false;
        //for (int i = 0; i < patientLevelMenu.Items[1].ChildItems.Count; i++)
        //{
        //    patientLevelMenu.Items[1].ChildItems[i].Selectable = false;
        //}
        if (Convert.ToString(Session["CareEndFlag"]) == "1")
        {
            patientLevelMenu.Items[4].Enabled = true;
            for (int i = 0; i < patientLevelMenu.Items[1].ChildItems.Count; i++)
            {
                var values = new[] { "mnuPharmacy", "mnuLabOrderDynm", "mnuLabTest" };
                if (values.Any(patientLevelMenu.Items[1].ChildItems[i].Value.Contains))
                //if (patientLevelMenu.Items[1].ChildItems[i].Value.ContainsAny("mnuPharmacy") || patientLevelMenu.Items[1].ChildItems[i].Value.Equals("mnuLabOrderDynm"))
                {
                    patientLevelMenu.Items[1].ChildItems[i].Selectable = true;
                }
                else
                {
                    patientLevelMenu.Items[1].ChildItems[i].Selectable = false;
                }
            }
        }
        //patientLevelMenu.Items[5].Enabled = false;
        //patientLevelMenu.Items[7].Enabled = false;
        //VY Disable Waiting Lsit 2015-03-31
        //patientLevelMenu.FindItem("mnuWaitingList").Enabled = false;
    }
    #endregion

    //[Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public void SetPatientId_Session()
    {
        HttpContext.Current.Session["PatientVisitId"] = 0;
        HttpContext.Current.Session["ServiceLocationId"] = 0;
        HttpContext.Current.Session["LabId"] = 0;
        HttpContext.Current.Session["LabOrderID"] = null;
    }

    //Dynamic Forms
    //   [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public void SetDynamic_Session(string id)
    {
        HttpContext.Current.Session["PatientVisitId"] = 0;
        HttpContext.Current.Session["ServiceLocationId"] = 0;
        HttpContext.Current.Session["FeatureID"] = id;
        HttpContext.Current.Session["LabOrderID"] = null;
    }

    #region "Authentication Clinical Header"
    private void AuthenticationRights()
    {
        if (Session["TechnicalAreaId"] == null)
        {
        }
        else
        {
            string ModuleId;
            DataView theDV = new DataView((DataTable)Session["UserRight"]);
            if (Session["TechnicalAreaId"] != null || Session["TechnicalAreaId"].ToString() != "")
            {
                if (Convert.ToInt32(Session["TechnicalAreaId"].ToString()) != 0)
                {
                    ModuleId = "0," + Session["TechnicalAreaId"].ToString();
                }
                else
                    ModuleId = "0";

            }
            else
                ModuleId = "0";
            theDV.RowFilter = "ModuleId in (" + ModuleId + ")";
            DataTable theDT = new DataTable();
            theDT = theDV.ToTable();

            Session["FilteredUserRight"] = theDT;



            AuthenticationManager Authentication = new AuthenticationManager();

            if (!object.Equals(ConfigurationManager.AppSettings["EnablePharmacyStaticForm"], null))
            {
                if (Convert.ToBoolean(ConfigurationManager.AppSettings["EnablePharmacyStaticForm"].ToString()))
                {
                    //validate permission for pharmacy form
                    if (!Authentication.HasFeatureRight(ApplicationAccess.AdultPharmacy, theDT))
                        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuPharmacy");
                }
                else
                    RemoveMenuItemByValue(patientLevelMenu.Items, "mnuPharmacy");
            }
            if (Authentication.HasFeatureRight(ApplicationAccess.ARTFollowup, theDT) == false)
            {
                //mnuFollowupART.Visible = false;
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuFollowupART");
            }


            if (Authentication.HasFeatureRight(ApplicationAccess.HomeVisit, theDT) == false)
            {
                //mnuHomeVisit.Visible = false;
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuHomeVisit");
            }
            if (Authentication.HasFeatureRight(ApplicationAccess.InitialEvaluation, theDT) == false)
            {
                //mnuInitEval.Visible = false;
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuInitEval");
            }

            if (Authentication.HasFeatureRight(ApplicationAccess.Laboratory, theDT) == false)
            {
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuLabOrderDynm");
                //mnuLabOrder.Visible = false;
                //mnuLabOrderPMTCT.Visible = false;
            }
            if ((Authentication.HasFeatureRight(ApplicationAccess.OrderLabTest, theDT) == false) || (Session["LMIS"].ToString() != "1"))
            {
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuLabTest");
                //mnuLabOrder.Visible = false;
                //mnuLabOrderPMTCT.Visible = false;
            }

            if (Authentication.HasFeatureRight(ApplicationAccess.NonARTFollowup, theDT) == false)
            {
                //mnuNonARTFollowUp.Visible = false;
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuNonARTFollowUp");
            }



            if (Authentication.HasFeatureRight(ApplicationAccess.DeleteForm, theDT) == false)
            {
                //mnuClinicalDeleteForm.Visible = false;
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuClinicalDeleteForm");
            }
            if (Authentication.HasFeatureRight(ApplicationAccess.PatientARVPickup, theDT) == false)
            {
                //mnuPatientProfile.Visible = false;
                //mnuDrugPickUp.Visible = false;
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuPatientProfile");
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuDrugPickUp");
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuPatientBlueCard");
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuDebitNote");
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuClinicalSummary");
            }
            if (Authentication.HasFeatureRight(ApplicationAccess.Schedular, theDT) == false)
            {
                //mnuScheduleAppointment.Visible = false;
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuScheduleAppointment");
            }

            if (Authentication.HasFeatureRight(ApplicationAccess.SchedularAppointment, theDT) == false)
            {
                //mnuScheduleAppointment.Visible = false;
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuScheduleAppointment");
            }

            if (Authentication.HasFeatureRight(ApplicationAccess.FamilyInfo, theDT) == false)
            {
                //mnuFamilyInformation.Visible = false;
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuFamilyInformation");
            }

            if (Authentication.HasFeatureRight(ApplicationAccess.Allergy, theDT) == false)
            {
                //mnuFamilyInformation.Visible = false;
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuAllergyInformation");
            }

            if (Authentication.HasFeatureRight(ApplicationAccess.NAFDAC, theDT) == false)
            {
                //mnuFamilyInformation.Visible = false;
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuNAFDAC");
            }
            if (Authentication.HasFeatureRight(ApplicationAccess.ChildEnrollment, theDT) == false)
            {
                //mnuInfantFollowUp.Visible = false;
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuInfantFollowUp");
            }

            if (Authentication.HasFeatureRight(ApplicationAccess.PatientClassification, theDT) == false)
            {
                //mnuPatientClassification.Visible = false;
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuPatientClassification");
            }

            if (Authentication.HasFeatureRight(ApplicationAccess.ChildEnrollment, theDT) == false)
            {
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuExposedInfant");
            }

            if (Authentication.HasFeatureRight(ApplicationAccess.FollowupEducation, theDT) == false)
            {
                //mnuFollowupEducation.Visible = false;
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuFollowupEducation");
            }
            else
            {
                DataSet theDS = (DataSet)ViewState["AddForms"];
                DataView theFormDV = new DataView(theDS.Tables[1]);
                theFormDV.RowFilter = "FeatureId=" + ApplicationAccess.FollowupEducation.ToString();
                if (theFormDV.Count < 1)
                    //mnuFollowupEducation.Visible = false;
                    RemoveMenuItemByValue(patientLevelMenu.Items, "mnuFollowupEducation");
            }
            if (Authentication.HasFeatureRight(ApplicationAccess.PatientRecord, theDT) == false)
            {
                //mnuPatientRecord.Visible = false;

            }


            if (Authentication.HasFeatureRight(ApplicationAccess.Transfer, theDT) == false)
            {
                //mnuPatientTranfer.Visible = false;
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuPatientTransfer");
            }
            if (Authentication.HasFeatureRight(ApplicationAccess.Transfer, theDT) == false)
            {
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuPatientHome");
            }
            if (Authentication.HasFeatureRight(ApplicationAccess.OrderLabTest, theDT) == false)
            {
                //mnuOrderLabTest.Visible = false;
                //mnuOrderLabTestPMTCT.Visible = false;
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuOrderLabTest");
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuOrderLabTestPMTCT");
            }

            if (Authentication.HasFeatureRight(ApplicationAccess.KNHAdultInitialEvaluationForm, theDT) == false)
            {
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuknhadultIE");
            }
            if (Authentication.HasFeatureRight(ApplicationAccess.KNHAdultFollowupForm, theDT) == false)
            {
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuRvsdAdult");
            }
            if (Authentication.HasFeatureRight(ApplicationAccess.KNHPaediatricInitialEvaluationForm, theDT) == false)
            {
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuknhPaediatricIE");
            }
            if (Authentication.HasFeatureRight(ApplicationAccess.KNHPaediatricFollowupForm, theDT) == false)
            {
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuPaediatricFollowup");
            }
            if (Authentication.HasFeatureRight(ApplicationAccess.KNHExpress, theDT) == false)
            {
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuRvsdExpressForm");
            }

            if (Authentication.HasFeatureRight(ApplicationAccess.InitialFollowupVisits, theDT) == false)
            {
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuARTVisit");
            }
            if (Authentication.HasFeatureRight(ApplicationAccess.ARTHistory, theDT) == false)
            {
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuARTHistory");
            }
            if (Authentication.HasFeatureRight(ApplicationAccess.ARVTherapy, theDT) == false)
            {
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuARTTherapy");
            }

            if (Authentication.HasFeatureRight(ApplicationAccess.Dashboard, theDT) == false)
            {
                RemoveMenuItemByValue(PharmacyDispensingMenu.Items, "Dashboard");
            }
            if (Authentication.HasFeatureRight(ApplicationAccess.Dispense, theDT) == false)
            {
                RemoveMenuItemByValue(PharmacyDispensingMenu.Items, "Dispense");
            }
            if (Authentication.HasFeatureRight(ApplicationAccess.StockSummaryWeb, theDT) == false)
            {
                RemoveMenuItemByValue(PharmacyDispensingMenu.Items, "StockSummaryWeb");
            }
            if (Authentication.HasFeatureRight(ApplicationAccess.StockManagement, theDT) == false)
            {
                RemoveMenuItemByValue(PharmacyDispensingMenu.Items, "StockManagement");
            }
            //Consumables linked to billing 
            if (Session["Billing"].ToString() != "1")
            {
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuConsumablesIssuance");
            }

        }

    }
    #endregion

    #region "Load Menu"
    public void LoadCreateNewMenu(String url, int i)
    {
        if (!IsPostBack)
        {
            IPatientHome PatientHome = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
            int ModuleId = Convert.ToInt32(Session["TechnicalAreaId"]);
            DataSet theDS = PatientHome.GetTechnicalAreaandFormName(ModuleId);
            DataTable dtrules = new DataTable();
            if (theDS.Tables[1].Rows.Count > 0)
            {
                dtrules = BindFormBusinessRules(theDS.Tables[1], theDS.Tables[3]);
            }

            /*
             * Change for care end feature name suggested by Stacey 
             * (Please do not append the name  of the service area; please also add spaces to conform to the original name) 
             * Changes by: Gaurav Saxena
             * Changes Date: 18 Nov 2014
             */

            /*
             * Change for security implementation of forms 
             * Forms should display as per users accessibility
             * Changes by: Jayanta Das
             * Changes Date: 27 Nov 2014
             */
            DataTable UserFeatureName = ((DataTable)Session["UserFeatures"]).DefaultView.ToTable(true, "FeatureName");
            string featureName = dtrules.Rows[i]["FeatureName"].ToString();
            if (Convert.ToInt32(Session["AppUserId"]) > 1)
            {
                featureName = "";
            }
            foreach (DataRow theDR in UserFeatureName.Rows)
            {
                if (theDR["FeatureName"].ToString() == dtrules.Rows[i]["FeatureName"].ToString())
                {
                    featureName = dtrules.Rows[i]["FeatureName"].ToString();
                }
            }

            if (featureName != "")
            {

                if (featureName.ToLower().Contains("careend_"))
                {
                    featureName = "Care End";
                }
                MenuItem child = new MenuItem(featureName, url);
                if (Convert.ToInt32(dtrules.Rows[i]["FeatureId"]) == 5 && Session["PaperLess"].ToString() == "1")
                {
                    child = new MenuItem("Laboratory", url);
                    patientLevelMenu.Items[2].ChildItems.Add(child);
                }
                else
                    patientLevelMenu.Items[1].ChildItems.Add(child);
                DataTable theCEntedStatusDT = (DataTable)Session["CEndedStatus"];
                string CareEnded = string.Empty;
                if (theCEntedStatusDT != null)
                {
                    if (theCEntedStatusDT.Rows.Count > 0)
                    {

                        CareEnded = Convert.ToString(theCEntedStatusDT.Rows[0]["CareEnded"]);
                        if (CareEnded == "1")
                        {
                            Session["CareEndFlag"] = "1";
                            disableMenuItem();
                        }
                    }
                }
            }
            theDS.Dispose();
            PatientHome = null;
            featureName = "";
        }

    }
    #endregion

    #region "Load Partial Menu"
    public void LoadCreatePartialMenu(String url, int CountryId)
    {
        //for (int i = patientLevelMenu.Items[4].ChildItems.Count - 1; i >= 0; i--)
        //{
        //    patientLevelMenu.Items[4].ChildItems.RemoveAt(i);
        //}

        ICustomForm CustomFormMgr = (ICustomForm)ObjectFactory.CreateInstance(ObjFactoryParameter);
        DataSet theDS = CustomFormMgr.GetFormName(1, CountryId);
        foreach (DataRow dr in theDS.Tables[0].Rows)
        {
            MenuItem child = new MenuItem(dr["FeatureName"].ToString(), "mnu" + dr["FeatureID"].ToString(), "", url);
            patientLevelMenu.Items[4].ChildItems.Add(child);
        }
    }
    #endregion

    #region "Divs"
    private void EnrolmentARTPMTCT()
    {
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuEnrolment");
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuPMTCTEnrol");
    }

    private void AdditionalForms()
    {
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuAllergyInformation");
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuNAFDAC");
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuFamilyInformation");
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuPatientClassification");
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuFollowupEducation");
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuExposedInfant");
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuFreeText");
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuClinicalSummaryAdd");
    }

    private void DivDynModule()
    {
        //RemoveMenuItemByValue(patientLevelMenu.Items, "mnuLabOrderDynm");
    }

    private void ClinicID()
    {
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuInitEval");
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuFollowupART");
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuNonARTFollowUp");
        //RemoveMenuItemByValue(patientLevelMenu.Items, "mnuPharmacy");
        //RemoveMenuItemByValue(patientLevelMenu.Items, "mnuLabOrder");
        //RemoveMenuItemByValue(patientLevelMenu.Items, "mnuOrderLabTest");
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuHomeVisit");
        //RemoveMenuItemByValue(patientLevelMenu.Items, "mnuAdultPharmacy");
    }

    private void divPMTCT()
    {
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuPharmacyPMTCT");
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuLabOrderPMTCT");
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuOrderLabTestPMTCT");
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuHEIForm");
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuMEIForm");
    }

    private void divUgandaBlueCard()
    {
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuPriorARTHIVCare");
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuARTCare");
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuHIVCareARTEncounter");
    }

    private void divKenyaBlueCard()
    {
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuARTHistory");
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuARTTherapy");
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuARTVisit");
        //RemoveMenuItemByValue(patientLevelMenu.Items, "mnuGreenCard");

    }
    private void divPEP()
    {
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuknhpep");
        //RemoveMenuItemByValue(patientLevelMenu.Items, "mnuknhadultIE");
        //RemoveMenuItemByValue(patientLevelMenu.Items, "mnuknhPaediatricIE");
        //RemoveMenuItemByValue(patientLevelMenu.Items, "mnuRvsdAdult");
        //RemoveMenuItemByValue(patientLevelMenu.Items, "mnuPaediatricFollowup");
        //RemoveMenuItemByValue(patientLevelMenu.Items, "mnuRvsdExpressForm");
    }
    private void divKHN()
    {
        //RemoveMenuItemByValue(patientLevelMenu.Items, "mnuknhpep");
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuknhadultIE");
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuknhPaediatricIE");
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuRvsdAdult");
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuPaediatricFollowup");
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuRvsdExpressForm");
    }
    private void divNigARTCard()
    {
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnunigadultIE");
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnunigPaedIE");
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuARTCardVisitation");
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuARTCardSummary");
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnunigInitVisit");
    }

    private void divKNHDCC()
    {
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuknhDCCadultIE");
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuknhDCCadultFU");

    }
    private void divIEFV()
    {
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuARTVisit");
    }
    private void divHIVCE()
    {
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuHIVCE");
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuREF");
    }
    #endregion

    public DataTable BindFormBusinessRules(DataTable dtform, DataTable dtbusinessrules)
    {

        DataTable btable = new DataTable();
        btable.Columns.Add("FeatureID", typeof(string));
        btable.Columns.Add("FeatureName", typeof(string));
        foreach (DataRow r in dtform.Rows)
        {
            DataView dv = new DataView(dtbusinessrules);
            dv.RowFilter = "FeatureID=" + r["FeatureID"].ToString() + "";
            DataTable dtfilter = dv.ToTable();


            Hashtable htrecord = new Hashtable();
            if (dtfilter.Rows.Count > 0)
            {
                DataRow[] resultset1 = dtfilter.Select("SetType=1");
                DataRow[] resultset2 = dtfilter.Select("SetType=2");
                int set1 = resultset1.Length;
                DataRow[] set1rulesAge = dtfilter.Select("SetType=1 and BusRuleId=16");
                DataRow[] set1rulesmale = dtfilter.Select("SetType=1 and BusRuleId=14");
                DataRow[] set1rulesfemale = dtfilter.Select("SetType=1 and BusRuleId=15");

                int set2 = resultset2.Length;
                DataRow[] set2rulesAge = dtfilter.Select("SetType=2 and BusRuleId=16");
                DataRow[] set2rulesmale = dtfilter.Select("SetType=2 and BusRuleId=14");
                DataRow[] set2rulesfemale = dtfilter.Select("SetType=2 and BusRuleId=15");


                if (set1 > 0)
                {
                    if (set1 == 3)
                    {
                        foreach (DataRow DR in set1rulesAge)
                        {
                            if (Convert.ToString(DR["BusRuleId"]) == "16" && (DR["Value"] != System.DBNull.Value) && (DR["Value1"] != System.DBNull.Value))
                            {
                                if ((Convert.ToDecimal(Session["PatientAge"]) >= Convert.ToDecimal(DR["Value"]) && Convert.ToDecimal(Session["PatientAge"]) <= Convert.ToDecimal(DR["Value1"])) && ((Session["PatientSex"].ToString() == "Male") || Session["PatientSex"].ToString() == "Female"))
                                {
                                    if (!(htrecord.Contains(r["FeatureName"].ToString())))
                                    {
                                        DataRow theDR = btable.NewRow();
                                        theDR["FeatureName"] = r["FeatureName"].ToString();
                                        theDR["FeatureID"] = r["FeatureID"].ToString();
                                        btable.Rows.Add(theDR);
                                        htrecord.Add(r["FeatureName"].ToString(), r["FeatureName"].ToString());
                                    }
                                }

                            }
                        }
                    }
                    if (set1 == 2)
                    {
                        foreach (DataRow DR in set1rulesAge)
                        {
                            if (Convert.ToString(DR["BusRuleId"]) == "16" && (DR["Value"] != System.DBNull.Value) && (DR["Value1"] != System.DBNull.Value))
                            {
                                if ((Convert.ToDecimal(Session["PatientAge"]) >= Convert.ToDecimal(DR["Value"]) && Convert.ToDecimal(Session["PatientAge"]) <= Convert.ToDecimal(DR["Value1"])) && Session["PatientSex"].ToString() == "Male")
                                {
                                    foreach (DataRow DR1 in set1rulesmale)
                                    {
                                        if (Convert.ToString(DR1["BusRuleId"]) == "14" && Session["PatientSex"].ToString() == "Male")
                                        {

                                            if (!(htrecord.Contains(r["FeatureName"].ToString())))
                                            {
                                                DataRow theDR = btable.NewRow();
                                                theDR["FeatureName"] = r["FeatureName"].ToString();
                                                theDR["FeatureID"] = r["FeatureID"].ToString();
                                                btable.Rows.Add(theDR);
                                                htrecord.Add(r["FeatureName"].ToString(), r["FeatureName"].ToString());
                                            }
                                        }
                                    }
                                }
                                else if ((Convert.ToDecimal(Session["PatientAge"]) >= Convert.ToDecimal(DR["Value"]) && Convert.ToDecimal(Session["PatientAge"]) <= Convert.ToDecimal(DR["Value1"])) && Session["PatientSex"].ToString() == "Female")
                                {
                                    foreach (DataRow DR1 in set1rulesfemale)
                                    {
                                        if (Convert.ToString(DR1["BusRuleId"]) == "15" && Session["PatientSex"].ToString() == "Female")
                                        {
                                            if (!(htrecord.Contains(r["FeatureName"].ToString())))
                                            {
                                                DataRow theDR = btable.NewRow();
                                                theDR["FeatureName"] = r["FeatureName"].ToString();
                                                theDR["FeatureID"] = r["FeatureID"].ToString();
                                                btable.Rows.Add(theDR);
                                                htrecord.Add(r["FeatureName"].ToString(), r["FeatureName"].ToString());
                                            }
                                        }
                                    }

                                }

                            }
                        }
                        if (set1rulesAge.Length == 0)
                        {
                            foreach (DataRow DR in set1rulesmale)
                            {
                                if (Convert.ToString(DR["BusRuleId"]) == "14" && Session["PatientSex"].ToString() == "Male")
                                {
                                    if (!(htrecord.Contains(r["FeatureName"].ToString())))
                                    {
                                        DataRow theDR = btable.NewRow();
                                        theDR["FeatureName"] = r["FeatureName"].ToString();
                                        theDR["FeatureID"] = r["FeatureID"].ToString();
                                        btable.Rows.Add(theDR);
                                        htrecord.Add(r["FeatureName"].ToString(), r["FeatureName"].ToString());
                                    }
                                }

                            }
                            foreach (DataRow DR in set1rulesfemale)
                            {
                                if (Convert.ToString(DR["BusRuleId"]) == "15" && Session["PatientSex"].ToString() == "Female")
                                {
                                    if (!(htrecord.Contains(r["FeatureName"].ToString())))
                                    {
                                        DataRow theDR = btable.NewRow();
                                        theDR["FeatureName"] = r["FeatureName"].ToString();
                                        theDR["FeatureID"] = r["FeatureID"].ToString();
                                        btable.Rows.Add(theDR);
                                        htrecord.Add(r["FeatureName"].ToString(), r["FeatureName"].ToString());
                                    }
                                }
                            }
                        }
                    }
                    if (set1 == 1)
                    {
                        foreach (DataRow DR in set1rulesAge)
                        {
                            if (Convert.ToString(DR["BusRuleId"]) == "16" && (DR["Value"] != System.DBNull.Value) && (DR["Value1"] != System.DBNull.Value))
                            {
                                if (Convert.ToDecimal(Session["PatientAge"]) >= Convert.ToDecimal(DR["Value"]) && Convert.ToDecimal(Session["PatientAge"]) <= Convert.ToDecimal(DR["Value1"]))
                                {
                                    if (!(htrecord.Contains(r["FeatureName"].ToString())))
                                    {
                                        DataRow theDR = btable.NewRow();
                                        theDR["FeatureName"] = r["FeatureName"].ToString();
                                        theDR["FeatureID"] = r["FeatureID"].ToString();
                                        btable.Rows.Add(theDR);
                                        htrecord.Add(r["FeatureName"].ToString(), r["FeatureName"].ToString());
                                    }
                                }


                            }
                        }
                    }
                    if (set1 == 1)
                    {
                        foreach (DataRow DR in set1rulesmale)
                        {
                            if (Convert.ToString(DR["BusRuleId"]) == "14" && Session["PatientSex"].ToString() == "Male")
                            {
                                if (!(htrecord.Contains(r["FeatureName"].ToString())))
                                {
                                    DataRow theDR = btable.NewRow();
                                    theDR["FeatureName"] = r["FeatureName"].ToString();
                                    theDR["FeatureID"] = r["FeatureID"].ToString();
                                    btable.Rows.Add(theDR);
                                    htrecord.Add(r["FeatureName"].ToString(), r["FeatureName"].ToString());
                                }
                            }

                        }
                    }
                    if (set1 == 1)
                    {
                        foreach (DataRow DR in set1rulesfemale)
                        {
                            if (Convert.ToString(DR["BusRuleId"]) == "15" && Session["PatientSex"].ToString() == "Female")
                            {
                                if (!(htrecord.Contains(r["FeatureName"].ToString())))
                                {
                                    DataRow theDR = btable.NewRow();
                                    theDR["FeatureName"] = r["FeatureName"].ToString();
                                    theDR["FeatureID"] = r["FeatureID"].ToString();
                                    btable.Rows.Add(theDR);
                                    htrecord.Add(r["FeatureName"].ToString(), r["FeatureName"].ToString());
                                }
                            }
                        }
                    }
                }
                //set type 2

                if (set2 > 0)
                {
                    if (set2 == 3)
                    {
                        foreach (DataRow DR in set2rulesAge)
                        {
                            if (Convert.ToString(DR["BusRuleId"]) == "16" && (DR["Value"] != System.DBNull.Value) && (DR["Value1"] != System.DBNull.Value))
                            {
                                if ((Convert.ToDecimal(Session["PatientAge"]) >= Convert.ToDecimal(DR["Value"]) && Convert.ToDecimal(Session["PatientAge"]) <= Convert.ToDecimal(DR["Value1"])) && ((Session["PatientSex"].ToString() == "Male") || Session["PatientSex"].ToString() == "Female"))
                                {
                                    if (!(htrecord.Contains(r["FeatureName"].ToString())))
                                    {
                                        DataRow theDR = btable.NewRow();
                                        theDR["FeatureName"] = r["FeatureName"].ToString();
                                        theDR["FeatureID"] = r["FeatureID"].ToString();
                                        btable.Rows.Add(theDR);
                                        htrecord.Add(r["FeatureName"].ToString(), r["FeatureName"].ToString());
                                    }
                                }

                            }
                        }
                    }
                    if (set2 == 2)
                    {
                        foreach (DataRow DR in set2rulesAge)
                        {
                            if (Convert.ToString(DR["BusRuleId"]) == "16" && (DR["Value"] != System.DBNull.Value) && (DR["Value1"] != System.DBNull.Value))
                            {
                                if ((Convert.ToDecimal(Session["PatientAge"]) >= Convert.ToDecimal(DR["Value"]) && Convert.ToDecimal(Session["PatientAge"]) <= Convert.ToDecimal(DR["Value1"])) && Session["PatientSex"].ToString() == "Male")
                                {
                                    foreach (DataRow DR1 in set2rulesmale)
                                    {
                                        if (Convert.ToString(DR1["BusRuleId"]) == "14" && Session["PatientSex"].ToString() == "Male")
                                        {
                                            if (!(htrecord.Contains(r["FeatureName"].ToString())))
                                            {
                                                DataRow theDR = btable.NewRow();
                                                theDR["FeatureName"] = r["FeatureName"].ToString();
                                                theDR["FeatureID"] = r["FeatureID"].ToString();
                                                btable.Rows.Add(theDR);
                                                htrecord.Add(r["FeatureName"].ToString(), r["FeatureName"].ToString());
                                            }
                                        }

                                    }
                                }
                                else if ((Convert.ToDecimal(Session["PatientAge"]) >= Convert.ToDecimal(DR["Value"]) && Convert.ToDecimal(Session["PatientAge"]) <= Convert.ToDecimal(DR["Value1"])) && Session["PatientSex"].ToString() == "Female")
                                {
                                    foreach (DataRow DR1 in set2rulesfemale)
                                    {
                                        if (Convert.ToString(DR1["BusRuleId"]) == "15" && Session["PatientSex"].ToString() == "Female")
                                        {
                                            if (!(htrecord.Contains(r["FeatureName"].ToString())))
                                            {
                                                DataRow theDR = btable.NewRow();
                                                theDR["FeatureName"] = r["FeatureName"].ToString();
                                                theDR["FeatureID"] = r["FeatureID"].ToString();
                                                btable.Rows.Add(theDR);
                                                htrecord.Add(r["FeatureName"].ToString(), r["FeatureName"].ToString());
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (set2rulesAge.Length == 0)
                        {
                            foreach (DataRow DR in set2rulesmale)
                            {
                                if (Convert.ToString(DR["BusRuleId"]) == "14" && Session["PatientSex"].ToString() == "Male")
                                {
                                    if (!(htrecord.Contains(r["FeatureName"].ToString())))
                                    {
                                        DataRow theDR = btable.NewRow();
                                        theDR["FeatureName"] = r["FeatureName"].ToString();
                                        theDR["FeatureID"] = r["FeatureID"].ToString();
                                        btable.Rows.Add(theDR);
                                        htrecord.Add(r["FeatureName"].ToString(), r["FeatureName"].ToString());
                                    }
                                }

                            }
                            foreach (DataRow DR in set2rulesfemale)
                            {
                                if (Convert.ToString(DR["BusRuleId"]) == "15" && Session["PatientSex"].ToString() == "Female")
                                {
                                    if (!(htrecord.Contains(r["FeatureName"].ToString())))
                                    {
                                        DataRow theDR = btable.NewRow();
                                        theDR["FeatureName"] = r["FeatureName"].ToString();
                                        theDR["FeatureID"] = r["FeatureID"].ToString();
                                        btable.Rows.Add(theDR);
                                        htrecord.Add(r["FeatureName"].ToString(), r["FeatureName"].ToString());
                                    }
                                }
                            }
                        }
                    }
                    if (set2 == 1)
                    {
                        foreach (DataRow DR in set2rulesAge)
                        {
                            if (Convert.ToString(DR["BusRuleId"]) == "16" && (DR["Value"] != System.DBNull.Value) && (DR["Value1"] != System.DBNull.Value))
                            {
                                if (Convert.ToDecimal(Session["PatientAge"]) >= Convert.ToDecimal(DR["Value"]) && Convert.ToDecimal(Session["PatientAge"]) <= Convert.ToDecimal(DR["Value1"]))
                                {
                                    if (!(htrecord.Contains(r["FeatureName"].ToString())))
                                    {
                                        DataRow theDR = btable.NewRow();
                                        theDR["FeatureName"] = r["FeatureName"].ToString();
                                        theDR["FeatureID"] = r["FeatureID"].ToString();
                                        btable.Rows.Add(theDR);
                                        htrecord.Add(r["FeatureName"].ToString(), r["FeatureName"].ToString());
                                    }
                                }


                            }
                        }
                    }
                    if (set2 == 1)
                    {
                        foreach (DataRow DR in set2rulesmale)
                        {
                            if (Convert.ToString(DR["BusRuleId"]) == "14" && Session["PatientSex"].ToString() == "Male")
                            {
                                if (!(htrecord.Contains(r["FeatureName"].ToString())))
                                {
                                    DataRow theDR = btable.NewRow();
                                    theDR["FeatureName"] = r["FeatureName"].ToString();
                                    theDR["FeatureID"] = r["FeatureID"].ToString();
                                    btable.Rows.Add(theDR);
                                    htrecord.Add(r["FeatureName"].ToString(), r["FeatureName"].ToString());
                                }
                            }

                        }
                    }
                    if (set2 == 1)
                    {
                        foreach (DataRow DR in set2rulesfemale)
                        {
                            if (Convert.ToString(DR["BusRuleId"]) == "15" && Session["PatientSex"].ToString() == "Female")
                            {
                                if (!(htrecord.Contains(r["FeatureName"].ToString())))
                                {
                                    DataRow theDR = btable.NewRow();
                                    theDR["FeatureName"] = r["FeatureName"].ToString();
                                    theDR["FeatureID"] = r["FeatureID"].ToString();
                                    btable.Rows.Add(theDR);
                                    htrecord.Add(r["FeatureName"].ToString(), r["FeatureName"].ToString());
                                }
                            }
                        }
                    }
                }



            }
            else
            {

                DataRow theDR = btable.NewRow();
                theDR["FeatureName"] = r["FeatureName"].ToString();
                theDR["FeatureID"] = r["FeatureID"].ToString();
                btable.Rows.Add(theDR);


            }


        }
        return btable;
    }
    private void Load_MenuCreateNewForm()
    {
        int ModuleId = Convert.ToInt32(Session["TechnicalAreaId"]);
        DataSet theDS = (DataSet)ViewState["AddForms"];
        string moduleName = "";
        if (theDS.Tables[0].Rows.Count > 0)
        {
            if (!theDS.Tables[0].Rows[0].IsNull("ModuleName"))
            {
                moduleName = theDS.Tables[0].Rows[0]["ModuleName"].ToString();
            }
        }
        DataTable dtrules = new DataTable();
        if (theDS.Tables[1].Rows.Count > 0)
        {
            dtrules = BindFormBusinessRules(theDS.Tables[1], theDS.Tables[3]);
        }

        int rowNo = 0;
        foreach (DataRow theDR in dtrules.Rows)
        {
            if (Convert.ToInt32(theDR["Featureid"]) != 71)
            {
                string theURL = "", theLabTest = "";
                if (Convert.ToInt32(theDR["FeatureId"]) == 3) { theURL = string.Format("{0}", "../PharmacyDispense/frmPharmacyDispense_PatientOrder.aspx?Prog=''"); }
                //theURL = string.Format("{0}", "../Pharmacy/frmPharmacy_Custom.aspx?Prog=''");
                else if (Convert.ToInt32(theDR["FeatureId"]) == 4) { theURL = string.Format("{0}", "../PharmacyDispense/frmPharmacyDispense_PatientOrder.aspx?Prog=''"); }
                //theURL = string.Format("{0}", "../Pharmacy/frmPharmacy_Custom.aspx?Prog=''");
                else if (Convert.ToInt32(theDR["FeatureId"]) == 5 && Session["PaperLess"].ToString() == "0")
                //theURL = string.Format("{0}sts={1}", "../Laboratory/frmLabOrder.aspx?", lblpntStatus.Text);
                { //theURL = string.Format("{0}sts={1}", "../Laboratory/frm_Laboratory.aspx?", lblpntStatus.Text); 
                }
                else if (Convert.ToInt32(theDR["FeatureId"]) == 5 && Session["PaperLess"].ToString() == "1")
                {
                    //theURL = string.Format("{0}&sts={1}", "../Laboratory/LabOrderForm.aspx?name=Add", lblpntStatus.Text);
                    //theLabTest = string.Format("{0}&sts={1}", "../Laboratory/LabOrderForm.aspx?name=Add", lblpntStatus.Text);
                    //theURL = string.Format("{0}&sts={1}", "../Laboratory/frm_Laboratory.aspx?name=Add", lblpntStatus.Text);
                    //theLabTest = "";
                }
                else if (theDR["FeatureName"].ToString() == "Care Termination")
                    theURL = string.Format("{0}", "../Scheduler/frmScheduler_ContactCareTracking.aspx?");
                else if (theDR["FeatureName"].ToString() == "CareEnd_" + moduleName)
                    theURL = string.Format("{0}", "../Scheduler/frmScheduler_ContactCareTracking.aspx?");
                else if (theDR["FeatureName"].ToString() == "Pharmacy")
                    theURL = string.Format("{0}|{1}", "../PharmacyDispense/frmPharmacyDispense_PatientOrder.aspx?", theDR["FeatureId"].ToString());
                else
                    theURL = string.Format("{0}|{1}", "../ClinicalForms/frmClinical_CustomForm.aspx?", theDR["FeatureId"].ToString());

                if (ModuleId.ToString() == "1")
                {
                    DivDynModule(); divKenyaBlueCard(); divUgandaBlueCard(); divKHN(); divPEP(); ClinicID(); divNigARTCard(); divKNHDCC(); divIEFV();
                    LoadCreateNewMenu(theURL, rowNo);
                    if (lblpntStatus.Text == "1")
                        disableMenuItem();
                }
                else if (ModuleId.ToString() == "2")
                {
                    DivDynModule(); divKenyaBlueCard(); divUgandaBlueCard(); divKHN(); divPEP(); divPMTCT(); divNigARTCard(); divKNHDCC(); divHIVCE();
                    LoadCreateNewMenu(theURL, rowNo);
                    if (lblpntStatus.Text != "1")
                    {
                        //SetDynamic_Session(theDR["FeatureID"].ToString());
                        //ClinicID.Controls.Add(new LiteralControl("onClick=fnSetformID('" + theDR["FeatureID"].ToString() + "'); HRef=" + theURL + ""));
                    }
                }
                else if (ModuleId.ToString() == "202")
                {
                    DivDynModule(); divKenyaBlueCard(); ClinicID(); divKHN(); divPEP(); divPMTCT(); divNigARTCard(); divKNHDCC(); divHIVCE();
                    if (lblpntStatus.Text != "1")
                    {
                        if (theLabTest != "")
                        {
                            LoadCreateNewMenu(theLabTest, rowNo);
                        }
                        else
                        {
                            LoadCreateNewMenu(theURL, rowNo);
                        }
                    }
                }

                else if (ModuleId.ToString() == "203")
                {
                    DivDynModule(); divUgandaBlueCard(); ClinicID(); divKHN(); divPEP(); divPMTCT(); divNigARTCard(); divKNHDCC();
                    //divIEFV();
                    if (lblpntStatus.Text != "1")
                    {
                        //if (theLabTest != "")
                        //{

                        //    LoadCreateNewMenu(theLabTest, rowNo);
                        //}
                        //else
                        //{
                        //Clinical Encounter
                        LoadCreateNewMenu(theURL, rowNo);
                        //}
                    }
                }
                else if (ModuleId.ToString() == "204")
                {
                    DivDynModule(); ClinicID(); divPMTCT(); divPEP(); divNigARTCard(); divKNHDCC(); divHIVCE();
                    if (Convert.ToDecimal(Session["PatientAge"]) >= 15)
                    {
                        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuPaediatricFollowup");
                        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuknhPaediatricIE");
                    }
                    else
                    {
                        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuRvsdExpressForm");
                        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuknhadultIE");
                        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuRvsdAdult");
                    }
                    if (lblpntStatus.Text != "1")
                    {
                        if (theLabTest != "")
                        {

                            LoadCreateNewMenu(theLabTest, rowNo);
                        }
                        else
                        {

                            LoadCreateNewMenu(theURL, rowNo);
                        }
                    }
                }
                else if (ModuleId.ToString() == "6")
                {
                    DivDynModule(); ClinicID(); divPMTCT(); divKHN(); divNigARTCard(); divKNHDCC(); divHIVCE();
                    if (lblpntStatus.Text != "1")
                    {
                        if (theLabTest != "")
                        {

                            LoadCreateNewMenu(theLabTest, rowNo);
                        }
                        else
                        {

                            LoadCreateNewMenu(theURL, rowNo);
                        }
                    }
                }
                else if (ModuleId.ToString() == "209")
                {
                    DivDynModule(); ClinicID(); divPMTCT(); divPEP(); divKHN(); divUgandaBlueCard(); divKNHDCC(); divHIVCE();
                    if (Convert.ToDecimal(Session["PatientAge"]) >= 15)
                    {
                        RemoveMenuItemByValue(patientLevelMenu.Items, "mnunigPaedIE");
                    }
                    else
                    {
                        RemoveMenuItemByValue(patientLevelMenu.Items, "mnunigadultIE");

                    }
                    if (lblpntStatus.Text != "1")
                    {
                        if (theLabTest != "")
                        {

                            LoadCreateNewMenu(theLabTest, rowNo);
                        }
                        else
                        {

                            LoadCreateNewMenu(theURL, rowNo);
                        }
                    }
                }
                else
                {
                    if (Convert.ToInt32(theDR["FeatureId"]) == 5 && Session["PaperLess"].ToString() == "1")
                    {
                        divKenyaBlueCard(); divUgandaBlueCard(); ClinicID(); divKHN(); divPEP(); divPMTCT(); divNigARTCard(); divKNHDCC();
                        LoadCreateNewMenu(theURL, rowNo);
                    }
                    else
                    {
                        LoadCreateNewMenu(theURL, rowNo);
                        if (lblpntStatus.Text == "1")
                            disableMenuItem();
                    }
                }
            }
            rowNo++;
        }

        if (ModuleId.ToString() == "1")
        {
            DivDynModule();
            ClinicID();
            divUgandaBlueCard();
            //todo 
            divKenyaBlueCard();
            divKHN(); divPEP(); divNigARTCard(); divKNHDCC();
            divIEFV();

        }
        else if (ModuleId.ToString() == "2")
        {
            divPMTCT();
            DivDynModule();
            divUgandaBlueCard();
            //todo 
            divKenyaBlueCard();
            divKHN(); divNigARTCard(); divPEP(); divKNHDCC();
            divHIVCE();
        }
        else if (ModuleId.ToString() == "4")
        {
            divIEFV();
        }
        else if (ModuleId.ToString() == "202")
        {
            divPMTCT();
            DivDynModule();
            ClinicID();
            divKHN(); divNigARTCard(); divPEP();
            //todo 
            divKenyaBlueCard(); divKNHDCC();
            divHIVCE();
        }

        else if (ModuleId.ToString() == "203")
        {
            divPMTCT();
            DivDynModule();
            ClinicID();
            divKHN(); divNigARTCard();
            divUgandaBlueCard();
            divKNHDCC();
            //divIEFV();
        }
        else if (ModuleId.ToString() == "204")
        {
            divPMTCT();
            DivDynModule();
            ClinicID();
            divKenyaBlueCard();
            divUgandaBlueCard();
            divPEP();
            divNigARTCard();
            divKNHDCC();
            divHIVCE();
            if (Convert.ToDecimal(Session["PatientAge"]) >= 15)
            {
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuPaediatricFollowup");
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuknhPaediatricIE");
            }
            else
            {
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuRvsdExpressForm");
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuknhadultIE");
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuRvsdAdult");
            }

        }
        else if (ModuleId.ToString() == "6")
        {
            divPMTCT();
            DivDynModule();
            ClinicID();
            divKenyaBlueCard();
            divUgandaBlueCard();
            divKHN(); divNigARTCard();
            divKNHDCC();
            divHIVCE();
        }
        else if (ModuleId.ToString() == "209")
        {
            divPMTCT();
            DivDynModule();
            ClinicID();
            divKenyaBlueCard();
            divUgandaBlueCard();
            divKHN(); divPEP();
            divKNHDCC();
            divHIVCE();
            if (Convert.ToDecimal(Session["PatientAge"]) >= 15)
            {
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnunigPaedIE");
            }
            else
            {
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnunigadultIE");
            }
        }
        else if (ModuleId.ToString() == "210")
        {
            divPMTCT();
            DivDynModule();
            ClinicID();
            divKenyaBlueCard();
            divUgandaBlueCard();
            divKHN(); divPEP();
            divNigARTCard();
            divHIVCE();
        }

        else
        {
            //todo
            DivDynModule();
            divPMTCT();
            ClinicID();
            divUgandaBlueCard();
            divKenyaBlueCard();
            divKHN(); divNigARTCard(); divPEP(); divKNHDCC();
            divHIVCE();
        }

        if (Session["PaperLess"] != null)
        {
            if (Session["PaperLess"].ToString() == "0")
            {
                RemoveMenuItemByValue(patientLevelMenu.Items,
                    "mnuWaitingList"); //waiting list available only in paerless mode

            }
        }
    }

    private void Init_Menu()
    {
        IPatientHome PatientHome = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
        int ModuleId = Convert.ToInt32(Session["TechnicalAreaId"]);
        DataSet theDS = PatientHome.GetTechnicalAreaandFormName(ModuleId);
        ViewState["AddForms"] = theDS;

        if (Convert.ToInt32(Session["PatientId"]) != 0 && object.Equals(Session["PatientId"], null) == false)
            PatientId = Convert.ToInt32(Session["PatientId"]);

        if (Session["AppUserID"].ToString() == "")
        {
            IQCareMsgBox.Show("SessionExpired", this);
            Response.Redirect("~/frmlogin.aspx", true);
        }

        DataTable dtPatientInfo = (DataTable)Session["PatientInformation"];
        if (dtPatientInfo != null && dtPatientInfo.Rows.Count > 0)
        {
            lblpatientname.Text = dtPatientInfo.Rows[0]["FullName"].ToString();
            //if (Session["SystemId"].ToString() == "1")
            //    lblpatientname.Text = dtPatientInfo.Rows[0]["LastName"].ToString() + ", " + dtPatientInfo.Rows[0]["FirstName"].ToString();
            //else
            //    lblpatientname.Text = dtPatientInfo.Rows[0]["LastName"].ToString() + ", " + dtPatientInfo.Rows[0]["MiddleName"].ToString() + " , " + dtPatientInfo.Rows[0]["FirstName"].ToString();
            lblIQnumber.Text = dtPatientInfo.Rows[0]["IQNumber"].ToString();
            lblAge.Text = Convert.ToString(dtPatientInfo.Rows[0]["AGE"] == null ? "0" : dtPatientInfo.Rows[0]["AGE"]);
            lblGender.Text = dtPatientInfo.Rows[0]["SexNM"].ToString();


            PMTCTNos = dtPatientInfo.Rows[0]["ANCNumber"].ToString() + dtPatientInfo.Rows[0]["PMTCTNumber"].ToString() + dtPatientInfo.Rows[0]["AdmissionNumber"].ToString() + dtPatientInfo.Rows[0]["OutpatientNumber"].ToString();
            ARTNos = dtPatientInfo.Rows[0]["PatientEnrollmentId"].ToString();
        }
        else
        {
            PanelPatiInfo.Visible = false;
        }

        DataTable dtLabels = (DataTable)Session["DynamicLabels"];
        if (dtLabels != null)
        {
            if (GblIQCare.Scheduler == 0)
            {
                thePnlIdent.Visible = true;
                TechnicalAreaIdentifier();
            }

            else
            {
                thePnlIdent.Visible = false;
                GblIQCare.Scheduler = 0;
            }
        }
        if (Session["Billing"].ToString() == "1")
            setBillStaus();
        //################  Master Settings ###################
        string UserID = "";
        if (Session["AppUserID"].ToString() != null)
            UserID = Session["AppUserId"].ToString();

        IIQCareSystem AdminManager;
        AdminManager = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");

        //######################################################

        string theUrl;
        if (Session["PtnPrgStatus"] != null)
        {
            DataTable theStatusDT = (DataTable)Session["PtnPrgStatus"];
            DataTable theCEntedStatusDT = (DataTable)Session["CEndedStatus"];
            string PatientExitReason = string.Empty;
            string PMTCTCareEnded = string.Empty;
            string CareEnded = string.Empty;
            Session["CareendedStatus"] = 0;
            if (theCEntedStatusDT.Rows.Count > 0)
            {
                PatientExitReason = Convert.ToString(theCEntedStatusDT.Rows[0]["PatientExitReason"]);
                PMTCTCareEnded = Convert.ToString(theCEntedStatusDT.Rows[0]["PMTCTCareEnded"]);
                CareEnded = Convert.ToString(theCEntedStatusDT.Rows[0]["CareEnded"]);
                Session["CareendedStatus"] = CareEnded;
                if (CareEnded == "1")
                {
                    Session["CareEndFlag"] = "1";
                    disableMenuItem();
                }
            }


            if ((Convert.ToString(theStatusDT.Rows[0]["PMTCTStatus"]) == "PMTCT Care Ended") || (PatientExitReason == "93" && PMTCTCareEnded == "1"))
            {
                PtnPMTCTStatus = 1;
                Session["PMTCTPatientStatus"] = 1;
            }
            else
            {
                PtnPMTCTStatus = 0;
                Session["PMTCTPatientStatus"] = 0;
            }
            if ((Convert.ToString(theStatusDT.Rows[0]["ART/PalliativeCare"]) == "Care Ended") || (PatientExitReason == "93" && CareEnded == "1"))
            {
                PtnARTStatus = 1;
                Session["HIVPatientStatus"] = 1;
            }
            else
            {
                PtnARTStatus = 0;
                Session["HIVPatientStatus"] = 0;
            }
        }


        if (lblpntStatus.Text == "0" && (PtnARTStatus == 0 || PtnPMTCTStatus == 0))
        {
            if (PtnARTStatus == 0)
            {
                //########### Initial Evaluation ############
                theUrl = string.Format("{0}", "../ClinicalForms/frmClinical_InitialEvaluation.aspx");
                AssignUrl(patientLevelMenu.Items, "mnuInitEval", theUrl);
                //########### ART-FollowUp ############
                string theUrl18 = string.Format("{0}&sts={1}", "../ClinicalForms/frmClinical_ARTFollowup.aspx?", "0");
                AssignUrl(patientLevelMenu.Items, "mnuFollowupART", theUrl18);
                //########### Non-ART Follow-Up #########
                string theUrl1 = string.Format("{0}&sts={1}", "../ClinicalForms/frmClinical_NonARTFollowUp.aspx?", "0");
                Session.Remove("ExixstDS1");
                AssignUrl(patientLevelMenu.Items, "mnuNonARTFollowUp", theUrl1);
                //########### Patient Record ############ 
                theUrl = string.Format("{0}&sts={1}", "../ClinicalForms/frmClinical_PatientRecordCTC.aspx?name=Add", PtnARTStatus);
                //########### Adult Pharmacy ############
                //Added by Akhil - If "EnablePharmacyStaticForm" is set to TRUE then it will display the static pharmacy form.
                // and will 
                if (!object.Equals(Session["patientageinyearmonth"], null))
                {
                    try
                    {
                        if (Convert.ToBoolean(ConfigurationManager.AppSettings["EnablePharmacyStaticForm"].ToString()))
                        {
                            theUrl = string.Format("{0}&sts={1}", "../PharmacyDispense/frmPharmacyDispense_PatientOrder.aspx?Prog=ART", "0");
                            AssignUrl(patientLevelMenu.Items, "mnuPharmacy", theUrl);
                        }
                    }
                    catch { }
                }
                ////########### Pharmacy CTC###############
                theUrl = string.Format("{0}", "../Pharmacy/frmPharmacy_CTC.aspx?Prog=ART");
                //########### Laboratory ############
                //theUrl = string.Format("{0}sts={1}", "../Laboratory/frmLabOrder.aspx?", PtnARTStatus);
                //string theUrlLabOrder = string.Format("{0}&sts={1}", "../Laboratory/LabOrderForm.aspx?name=Add", PtnARTStatus);
                theUrl = string.Format("{0}sts={1}", "../Laboratory/frm_Laboratory.aspx?", PtnARTStatus);
                //string theUrlLabOrder = string.Format("{0}&sts={1}", "../Laboratory/frm_Laboratory.aspx?name=Add", PtnARTStatus);
                //AssignUrl(patientLevelMenu.Items, "mnuLabOrder", theUrl);
                //santosh
                //AssignAttribute(patientLevelMenu.Items, "mnuOrderLabTest", theUrlLabOrder);
                //########### Home Visit ############
                theUrl = string.Format("{0}", "../Scheduler/frmScheduler_HomeVisit.aspx");
                AssignUrl(patientLevelMenu.Items, "mnuHomeVisit", theUrl);
                //mnuHomeVisit.HRef = theUrl;
            }

            if (PtnPMTCTStatus == 0)
            {
                //########### Contact Tracking ############        
                theUrl = string.Format("{0}Module={1}", "../Scheduler/frmScheduler_ContactCareTracking.aspx?", "PMTCT");
                //####### Adult Pharmacy PMTCT ##########
                //theUrl = string.Format("{0}", "../Pharmacy/frmPharmacy_Custom.aspx?Prog=PMTCT");
                //AssignUrl(patientLevelMenu.Items, "mnuPharmacyPMTCT", theUrl);
                //########### Pharmacy PMTCT CTC###############
                theUrl = string.Format("{0}", "../Pharmacy/frmPharmacy_CTC.aspx?Prog=PMTCT");
                //########### Laboratory ############
                //string theUrlPMTCT = string.Format("{0}sts={1}", "../Laboratory/frmLabOrder.aspx?", PtnPMTCTStatus);
                //string theUrlPMTCTLabOrder = string.Format("{0}sts={1}", "../Laboratory/LabOrderForm.aspx?", PtnPMTCTStatus);
                string theUrlPMTCT = string.Format("{0}sts={1}", "../Laboratory/frm_Laboratory.aspx?", PtnPMTCTStatus);
                string theUrlPMTCTLabOrder = string.Format("{0}sts={1}", "../Laboratory/frm_Laboratory.aspx?", PtnPMTCTStatus);
                AssignUrl(patientLevelMenu.Items, "mnuLabOrderPMTCT", theUrlPMTCT);
                AssignUrl(patientLevelMenu.Items, "mnuOrderLabTestPMTCT", theUrlPMTCTLabOrder);
                //santosh change
                AssignAttribute(patientLevelMenu.Items, "mnuOrderLabTestPMTCT", theUrlPMTCTLabOrder);

            }
        }

        #region "Common Forms"
        theUrl = string.Format("{0}&mnuClicked={1}&sts={2}", "../AdminForms/frmAdmin_DeletePatient.aspx?name=Add", "DeletePatient", lblpntStatus.Text);
        AssignUrl(patientLevelMenu.Items, "mnuAdminDeletePatient", theUrl);

        //######## Meetu 08 Sep 2009 End########//
        //####### Delete Form #############
        theUrl = string.Format("{0}?sts={1}", "../ClinicalForms/frmClinical_DeleteForm.aspx", lblpntStatus.Text);
        AssignUrl(patientLevelMenu.Items, "mnuClinicalDeleteForm", theUrl);

        //####### Delete Patient  #############
        theUrl = string.Format("{0}?mnuClicked={1}&sts={2}", "../AdminForms/frmAdmin_DeletePatient.aspx?name=Add", "DeletePatient", lblpntStatus.Text);
        AssignUrl(patientLevelMenu.Items, "mnuAdminDeletePatient", theUrl);

        //##### Patient Transfer #######
        theUrl = string.Format("{0}&sts={1}", "../ClinicalForms/frmClinical_Transfer.aspx?name=Add", lblpntStatus.Text);
        AssignUrl(patientLevelMenu.Items, "mnuPatientTransfer", theUrl);

        //##### Patient Home #######
        //theUrl = string.Format("{0}&sts={1}", "../ClinicalForms/frmClinical_Transfer.aspx?name=Add", lblpntStatus.Text);
        theUrl = string.Format("../ClinicalForms/frmPatient_Home.aspx");//Fix bug by Rahmat on 06-Apr-2017 :Bug_Patient Home button pointing to wrong page
        AssignUrl(patientLevelMenu.Items, "mnuPatientHome", theUrl);

        //########### Existing Forms ############
        theUrl = string.Format("{0}&sts={1}", "../ClinicalForms/frmPatient_History.aspx?", lblpntStatus.Text);
        AssignUrl(patientLevelMenu.Items, "mnuExistingForms", theUrl);

        //########### ARV-Pickup Report ############
        theUrl = string.Format("{0}&SatelliteID={1}&CountryID={2}&PosID={3}&sts={4}", "../Reports/frmReport_PatientARVPickup.aspx?name=Add", Session["AppSatelliteId"], Session["AppCountryId"], Session["AppPosID"], lblpntStatus.Text);
        AssignUrl(patientLevelMenu.Items, "mnuDrugPickUp", theUrl);

        //########### PatientProfile ############
        theUrl = string.Format("{0}&ReportName={1}&sts={2}", "../Reports/frmReportViewer.aspx?name=Add", "PatientProfile", lblpntStatus.Text);
        AssignUrl(patientLevelMenu.Items, "mnuPatientProfile", theUrl);

        //########### PatientSummary ############
        theUrl = string.Format("{0}&ReportName={1}&sts={2}", "../Reports/frmReportViewer.aspx?name=Add", "PatientSummary", lblpntStatus.Text);
        AssignUrl(patientLevelMenu.Items, "mnuClinicalSummary", theUrl);

        //########### ARV-Pickup Report ############
        theUrl = string.Format("{0}&SatelliteID={1}&CountryID={2}&PosID={3}&sts={4}", "../Reports/frmReportDebitNote.aspx?name=Add", Session["AppSatelliteId"], Session["AppCountryId"], Session["AppPosID"], lblpntStatus.Text);
        AssignUrl(patientLevelMenu.Items, "mnuDebitNote", theUrl);

        //###### PatientHome #############
        theUrl = string.Format("{0}", "../ClinicalForms/frmPatient_Home.aspx");
        AssignUrl(patientLevelMenu.Items, "mnuPatienHome", theUrl);

        //###### Scheduler #############
        theUrl = string.Format("{0}&LocationId={1}&FormName={2}&sts={3}", "../Scheduler/frmScheduler_AppointmentHistory.aspx?name=Add", Session["AppLocationId"].ToString(), "PatientHome", lblpntStatus.Text);
        AssignUrl(patientLevelMenu.Items, "mnuScheduleAppointment", theUrl);

        //####### Additional Forms - Family Information #######
        theUrl = string.Format("{0}", "../ClinicalForms/frmFamilyInformation.aspx?name=Add");
        AssignUrl(patientLevelMenu.Items, "mnuFamilyInformation", theUrl);

        //####### Additional Forms - Allergy #######
        theUrl = string.Format("{0}", "../ClinicalForms/frmAllergy.aspx?name=Add");
        AssignUrl(patientLevelMenu.Items, "mnuAllergyInformation", theUrl);

        //####### Additional Forms - NAFDAC #######
        theUrl = string.Format("{0}", "../ClinicalForms/frmNAFDAC.aspx?name=Add");
        AssignUrl(patientLevelMenu.Items, "mnuNAFDAC", theUrl);

        //####### Patient Classification #######
        theUrl = string.Format("{0}", "../ClinicalForms/frmClinical_PatientClassificationCTC.aspx?name=Add");
        AssignUrl(patientLevelMenu.Items, "mnuPatientClassification", theUrl);

        //####### Follow-up Education #######
        theUrl = string.Format("{0}", "../ClinicalForms/frmFollowUpEducationCTC.aspx?name=Add");
        AssignUrl(patientLevelMenu.Items, "mnuFollowupEducation", theUrl);

        //####### Exposed Infant #############
        theUrl = string.Format("{0}", "../ClinicalForms/frmExposedInfantEnrollment.aspx");
        AssignUrl(patientLevelMenu.Items, "mnuExposedInfant", theUrl);
        #endregion

        theUrl = string.Format("{0}", "../ClinicalForms/frm_PriorArt_HivCare.aspx");
        AssignUrl(patientLevelMenu.Items, "mnuPriorARTHIVCare", theUrl);

        theUrl = string.Format("{0}", "../ClinicalForms/frmClinical_ARTCare.aspx");
        AssignUrl(patientLevelMenu.Items, "mnuARTCare", theUrl);

        //########### HIV Care/ART Encounter #########
        string theUrl2 = string.Format("{0}", "../ClinicalForms/frmClinical_HIVCareARTCardEncounter.aspx");
        AssignUrl(patientLevelMenu.Items, "mnuHIVCareARTEncounter", theUrl2);

        //########### Kenya Blue Card #########
        theUrl = string.Format("{0}", "../ClinicalForms/frmClinical_InitialFollowupVisit.aspx?status=0");
        AssignUrl(patientLevelMenu.Items, "mnuARTVisit", theUrl);

        theUrl = string.Format("{0}", "../ClinicalForms/frmClinical_RevisedAdultfollowup.aspx");
        AssignUrl(patientLevelMenu.Items, "mnuRvsdAdult", theUrl);

        theUrl = string.Format("{0}", "../ClinicalForms/frmClinical_KNH_PediatricFollowup.aspx");
        AssignUrl(patientLevelMenu.Items, "mnuPaediatricFollowup", theUrl);

        theUrl = string.Format("{0}", "../ClinicalForms/frmClinical_KNH_ExpressForm.aspx");
        AssignUrl(patientLevelMenu.Items, "mnuRvsdExpressForm", theUrl);

        theUrl = string.Format("{0}", "../ClinicalForms/frmClinical_ARVTherapy.aspx");
        AssignUrl(patientLevelMenu.Items, "mnuARTTherapy", theUrl);

        theUrl = string.Format("{0}", "../ClinicalForms/frmClinical_ARTHistory.aspx");
        AssignUrl(patientLevelMenu.Items, "mnuARTHistory", theUrl);

        theUrl = string.Format("{0}", "../ClinicalForms/frmClinical_KNH_PEP.aspx");
        AssignUrl(patientLevelMenu.Items, "mnuknhpep", theUrl);

        theUrl = string.Format("{0}", "../ClinicalForms/frmClinical_KNH_AdultIE.aspx");
        AssignUrl(patientLevelMenu.Items, "mnuknhadultIE", theUrl);

        theUrl = string.Format("{0}", "../ClinicalForms/frmClinical_KNH_Paediatric_IE.aspx");
        AssignUrl(patientLevelMenu.Items, "mnuknhPaediatricIE", theUrl);

        //####### Additional Forms - Free Text #######
        theUrl = string.Format("{0}", "../ClinicalForms/frmFreeText.aspx?name=Add");
        AssignUrl(patientLevelMenu.Items, "mnuFreeText", theUrl);

        //####### Additional Forms - Clinical Summary #######
        theUrl = string.Format("{0}", "../ClinicalForms/frmClinicalSummary.aspx?name=Add");
        AssignUrl(patientLevelMenu.Items, "mnuClinicalSummaryAdd", theUrl);

        //########### Patient Enrollment ############
        //Added - Jayanta Kr. Das - 16-02-07
        DataTable theDT = new DataTable();
        if (PatientId != 0)
        {
            //### Patient Enrolment ######
            string theUrl1 = "";
            if (ARTNos != null && ARTNos == "")
            {
                if (Session["SystemId"].ToString() == "1" && PtnARTStatus == 0)
                {
                    theUrl = string.Format("{0}", "../ClinicalForms/frmClinical_Enrolment.aspx");
                    AssignUrl(patientLevelMenu.Items, "mnuEnrolment", theUrl);
                }
                else if (PtnARTStatus == 0)
                {
                    theUrl = string.Format("{0}&locationid={1}&sts={2}", "../ClinicalForms/frmClinical_PatientRegistrationCTC.aspx?name=Add", Session["AppLocationId"].ToString(), PtnARTStatus);
                    AssignUrl(patientLevelMenu.Items, "mnuEnrolment", theUrl);
                }
                if (PtnPMTCTStatus == 0)
                {
                    theUrl1 = string.Format("{0}", "../frmPatientCustomRegistration.aspx");
                    AssignUrl(patientLevelMenu.Items, "mnuPMTCTEnrol", theUrl1);
                }
            }

            else if (PMTCTNos != null && PMTCTNos == "")
            {
                if (PtnPMTCTStatus == 0)
                {
                    theUrl1 = string.Format("{0}", "../frmPatientCustomRegistration.aspx");
                    AssignUrl(patientLevelMenu.Items, "mnuPMTCTEnrol", theUrl1);
                }

                if (Session["SystemId"].ToString() == "1" && PtnARTStatus == 0)
                {
                    theUrl = string.Format("{0}", "../ClinicalForms/frmClinical_Enrolment.aspx");
                    AssignUrl(patientLevelMenu.Items, "mnuEnrolment", theUrl);
                }
                else if (PtnARTStatus == 0)
                {
                    theUrl = string.Format("{0}&locationid={1}&sts={2}", "../ClinicalForms/frmClinical_PatientRegistrationCTC.aspx?name=Edit", Session["AppLocationId"].ToString(), PtnARTStatus);
                    AssignUrl(patientLevelMenu.Items, "mnuEnrolment", theUrl);
                }
            }
            else
            {
                theUrl1 = string.Format("{0}", "../frmPatientCustomRegistration.aspx");
                AssignUrl(patientLevelMenu.Items, "mnuPMTCTEnrol", theUrl1);

                if (Session["SystemId"].ToString() == "1" && PtnARTStatus == 0)
                {
                    theUrl = string.Format("{0}", "../ClinicalForms/frmClinical_Enrolment.aspx");
                    AssignUrl(patientLevelMenu.Items, "mnuEnrolment", theUrl);
                }
                else if (PtnARTStatus == 0)
                {
                    theUrl = string.Format("{0}&locationid={1}&sts={2}", "../ClinicalForms/frmClinical_PatientRegistrationCTC.aspx?name=Edit", Session["AppLocationId"].ToString(), PtnARTStatus);
                    AssignUrl(patientLevelMenu.Items, "mnuEnrolment", theUrl);
                }
            }
        }
    }

    private void Load_MenuRegistration()
    {
        int ModuleId = Convert.ToInt32(Session["TechnicalAreaId"]);
        IPatientHome PatientHome = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
        DataSet theDS = PatientHome.GetTechnicalAreaandFormName(ModuleId);
        if (ModuleId == 2)
        {
            //mnuPMTCTEnrol.Visible = true;
            //mnuEnrolment.Visible = true;
        }
        else if (ModuleId == 203)//CCC Patient Card MoH 257(Fix bug "Hide Legacy Forms From Menu" by Rahmat on 04-04-2017)
        {
            RemoveMenuItemByValue(patientLevelMenu.Items, "mnuARTTherapy");
        }
        else
        {
            RemoveMenuItemByValue(patientLevelMenu.Items, "mnuEnrolment");
        }

    }

    private void TechnicalAreaIdentifier()
    {
        int intmoduleID = Convert.ToInt32(Session["TechnicalAreaId"]);
        IPatientHome PatientHome = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
        System.Data.DataSet DSTab = PatientHome.GetTechnicalAreaIdentifierFuture(intmoduleID, Convert.ToInt32(Session["PatientId"]));

        if (DSTab.Tables[0].Rows.Count > 0)
        {
            if (DSTab.Tables[0].Rows.Count > 0)
            {
                Label theLabelIdentifier1 = new Label();
                theLabelIdentifier1.ID = "Lbl_" + DSTab.Tables[0].Rows[0][0].ToString();
                //theLabelIdentifier1.Font.Bold = true;
                int i = 0;
                foreach (DataRow DRLabel in DSTab.Tables[0].Rows)
                {
                    if (i > 0)
                    {
                        theLabelIdentifier1.Text = theLabelIdentifier1.Text + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                    }
                    foreach (DataRow DRLabel1 in DSTab.Tables[1].Rows)
                    {
                        theLabelIdentifier1.Text = theLabelIdentifier1.Text + "    " + "<label class='control-label'>" + DRLabel[0].ToString() + "</label>" + " : " + DRLabel1[i].ToString() + "           ";
                    }

                    i++;
                }
                thePnlIdent.Controls.Add(theLabelIdentifier1);
            }
        }


    }

    private void Load_MenuPartial(int PatientId, string Status, int CountryId)
    {
        ICustomForm CustomFormMgr = (ICustomForm)ObjectFactory.CreateInstance(ObjFactoryParameter);
        DataSet theDS = CustomFormMgr.GetFormName(1, CountryId);
        foreach (DataRow dr in theDS.Tables[0].Rows)
        {
            string theURL = string.Format("{0}", "../ClinicalForms/frmClinical_CustomForm.aspx?");

            if (Status == "0")
            {
                LoadCreatePartialMenu(theURL, CountryId);
                if (PtnARTStatus == 1)
                {
                    disableMenuItem();
                }
            }
            else
            {
                LoadCreatePartialMenu(theURL, CountryId);
                if (PtnARTStatus == 1)
                {
                    disableMenuItem();
                }
            }
        }
    }

    protected void patientLevelMenu_MenuItemClick1(object sender, MenuEventArgs e)
    {
        if (e.Item.Value.ToString().Contains('|'))
        {
            string[] urlParts = e.Item.Value.Split('|');
            SetPatientId_Session();
            SetDynamic_Session(urlParts[1]);
            Response.Redirect(urlParts[0]);
        }
        else
        {
            SetPatientId_Session();
            //if (e.Item.Value.Substring(0, 15).ToString().Equals("mnuOrderLabTest") || e.Item.Value.Equals("../Laboratory/LabOrderForm.aspx?name=Add&sts=0"))
            string strMenuValue = string.Empty;

            if (!String.IsNullOrEmpty(e.Item.Value.ToString()))
                strMenuValue = e.Item.Value.Substring(0, 15);

            if (strMenuValue.Equals("mnuOrderLabTest") || strMenuValue.Equals("mnuLabOrderDynm") || e.Item.Value.Equals("../Laboratory/frm_Laboratory.aspx?name=Add&sts=0"))
            {

                //Redirect("../Laboratory/LabOrderForm.aspx", "_blank", "toolbars=no,location=no,directories=no,dependent=yes,top=100,left=30,maximize=no,resize=no,width=1000,height=800,scrollbars=yes");
                //Redirect("../Laboratory/frm_Laboratory.aspx", "_blank", "toolbars=no,location=no,directories=no,dependent=yes,top=100,left=30,maximize=no,resize=no,width=1000,height=800,scrollbars=yes");
                Response.Redirect("../Laboratory/frm_Laboratory.aspx");
            }
            else
            {
                Response.Redirect(e.Item.Value.ToString());
            }


        }


    }

    public void setSessionIds_Patient()
    {
        if (!Page.ClientScript.IsStartupScriptRegistered("Menu Script"))
        {
            string script = "\n window.onload = function(){";
            script += "\n var menuTable = document.getElementById('" + patientLevelMenu.ClientID + "');";
            script += "\n var menuLinks = menuTable.getElementsByTagName('a');";
            script += "\n   for(i=0;i<menuLinks.length;i++)";
            script += "\n     {";
            script += "\n       menuLinks[i].onclick = function(){  MasterPage_levelTwoNavigationUserControl.SetPatientId_Session();}";
            script += "\n     }";
            script += "\n   setOnClickForNextLevelMenuItems(menuTable.nextSibling);";
            script += "\n }";
            script += "\n function setOnClickForNextLevelMenuItems(currentMenuItemsContainer){";
            script += "\n   var id = currentMenuItemsContainer.id;";
            script += "\n   var len = id.length;";
            script += "\n     if(id != null && typeof(id) != 'undefined' && id.substring(0,parseInt(len)-7) == '" + patientLevelMenu.ClientID + "' && id.substring(parseInt(len)-5,parseInt(len)) == 'Items')";
            script += "\n      {";
            script += "\n        var subMenuLinks = currentMenuItemsContainer.getElementsByTagName('a');";
            script += "\n        for(i=0;i<subMenuLinks.length;i++)";
            script += "\n          {";
            script += "\n            if(subMenuLinks[i] != 'javascript:openClinicalSummary();'){subMenuLinks[i].onclick = function(){ PageMethods.SetPatientId_Session('bb', CallSuccess, CallFailed);}}";
            script += "\n          }";
            script += "\n        setOnClickForNextLevelMenuItems(currentMenuItemsContainer.nextSibling);";
            script += "\n      }";
            script += "\n }";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Menu Script", script, true);
        }
    }

    public static void Redirect(string url, string target, string windowFeatures)
    {
        HttpContext context = HttpContext.Current;

        if ((String.IsNullOrEmpty(target) ||
            target.Equals("_self", StringComparison.OrdinalIgnoreCase)) &&
            String.IsNullOrEmpty(windowFeatures))
        {

            context.Response.Redirect(url);
        }
        else
        {
            Page page = (Page)context.Handler;
            if (page == null)
            {
                throw new InvalidOperationException(
                    "Cannot redirect to new window outside Page context.");
            }
            url = page.ResolveClientUrl(url);

            string script;
            if (!String.IsNullOrEmpty(windowFeatures))
            {
                script = @"window.open(""{0}"", ""{1}"", ""{2}"");";
            }
            else
            {
                script = @"window.open(""{0}"", ""{1}"");";
            }

            script = String.Format(script, url, target, windowFeatures);
            ScriptManager.RegisterStartupScript(page,
                typeof(Page),
                "Redirect",
                script,
                true);
        }
    }
    //TODO Done code for Bill 
    private void setBillStaus()
    {
        System.IO.FileInfo fileinfo = new System.IO.FileInfo(Request.Url.AbsolutePath);
        string pageName = fileinfo.Name;


        try
        {
            if (pageName.Equals("frmBilling_ClientBill.aspx") || pageName.Equals("frmBilling_PayBill.aspx"))
            {
                Tr1.Visible = false;
                Tr2.Visible = false;
                patientLevelMenu.Visible = false;
                return;

            }

            int visitTypeID = getVisitTypeID(pageName);
            if (visitTypeID == 99)
            {
                Tr2.Visible = false;
            }
            else if (visitTypeID == 6)//Lab order form show labs paid for
            {
                Tr2.Visible = true;
                Label theLabelBill;
                IBilling BillingManager = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.SCM.BBilling, BusinessProcess.SCM");
                DataTable theLabsDt = BillingManager.GetPaidLabs(int.Parse(Session["PatientId"].ToString()));
                String labs = "", labelText = "Attention:This service has not been paid for!!";
                foreach (DataRow row in theLabsDt.Rows)
                {
                    labs = String.Format("{0}{1}, ", labs, row[0]);
                }
                if (theLabsDt.Rows.Count == 0)
                {

                    labelText = "Attention:This service has not been paid for!!";
                    theLabelBill = new Label { ID = "Lbl_bill", ForeColor = System.Drawing.Color.Red, Text = labelText };
                }
                else
                {
                    labelText = "Patient is cleared to recieve the following lab tests: " + labs.ToUpper();
                    theLabelBill = new Label { ID = "Lbl_bill", ForeColor = System.Drawing.Color.Green, Text = labelText };

                }

                thePnlBill.Controls.Add(theLabelBill);
            }
            else if (visitTypeID == 4)//Pharmacy form
            {
                Tr2.Visible = true;
                Label theLabelBill;
                IBilling BillingManager = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.SCM.BBilling, BusinessProcess.SCM");
                DataTable theDrugsDt = BillingManager.GetPaidDrugs(int.Parse(Session["PatientId"].ToString()));
                String drugs = "", labelText = "Attention:This service has not been paid for!!";
                foreach (DataRow row in theDrugsDt.Rows)
                {
                    drugs = String.Format("{0}{1} {2},{3} ", drugs, row[1], row[0], Environment.NewLine);
                }
                if (theDrugsDt.Rows.Count == 0)
                {

                    labelText = "Attention:This service has not been paid for!!";
                    theLabelBill = new Label { ID = "Lbl_bill", ForeColor = System.Drawing.Color.Red, Text = labelText };
                }
                else
                {
                    labelText = "Patient is cleared to recieve the following Drugs: " + Environment.NewLine + drugs;
                    theLabelBill = new Label { ID = "Lbl_bill", ForeColor = System.Drawing.Color.Green, Text = labelText };

                }

                thePnlBill.Controls.Add(theLabelBill);
            }
            else
            {
                //check whether this is a form that has previously been filled if so it dosnt have to be paid for again
                if (Session["PatientVisitId"] != null && int.Parse(Session["PatientVisitId"].ToString()) != 0) return;
                IBilling BillingManager = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.SCM.BBilling, BusinessProcess.SCM");
                bool itemPaid;
                DataTable drItemStatus = BillingManager.isVisitTypePaid(visitTypeID, int.Parse(Session["PatientId"].ToString()));

                itemPaid = Int32.Parse(drItemStatus.Rows[0][0].ToString()) > 0 ? true : false;
                if (itemPaid == true)
                {
                    Tr2.Visible = true;
                    Label theLabelBill = new Label { ID = "Lbl_bill", ForeColor = System.Drawing.Color.Green, Text = "Patient is cleared to recieve this service" };
                    thePnlBill.Controls.Add(theLabelBill);

                }
                else
                {
                    //Check if the item has a price i.e it has been configured to be sold and set the visibility based on this
                    Tr2.Visible = Decimal.Parse(drItemStatus.Rows[0][1].ToString()) > 0 ? true : false;
                    Label theLabelBill = new Label { ID = "Lbl_bill", ForeColor = System.Drawing.Color.Red, Text = "Attention:This service has not been paid for!!" };
                    thePnlBill.Controls.Add(theLabelBill);

                }


            }
        }
        catch (Exception ex)
        {

            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = ex.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
            return;
        }
    }
    private int getVisitTypeID(String pageName)
    {
        try
        {
            switch (pageName.TrimEnd(".aspx".ToCharArray()))
            {
                case "frmClinical_ARTHistory":
                    return 18;
                case "frmClinical_ARVTherapy":
                    return 19;
                case "frmClinical_InitialFollowupVisit":
                    return 17;
                case "frmPharmacyForm":
                case "frmPharmacy_Custom":
                    return 4;
                case "frmLabOrder":
                case "frmDynamicLab":
                    return 6;
                case "frmClinical_CustomForm":
                    return int.Parse(HttpContext.Current.Session["FeatureID"].ToString());
                default:
                    return 99;
            }
        }
        catch (Exception ex)
        {

            return 99;
        }
    }
}
