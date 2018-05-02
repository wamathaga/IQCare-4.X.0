using System;
using System.Data;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.VisualBasic;
using Interface.Clinical;
using Interface.Security;
using Application.Common;
using Interface.Administration;
using Application.Presentation;
using Interface.Pharmacy;
using System.Collections.Generic;
using AjaxControlToolkit;
using System.Web.Script.Serialization;
using System.Drawing;

namespace PresentationApp.GreenCard
{
    public partial class frm_GreenCardFollowup : System.Web.UI.Page
    {
        IQCareUtils theUtils = new IQCareUtils();
        DataTable theDTGrid = new DataTable();
        DataView theDTView = new DataView();
        DataTable dtmuiltselect;
        DataTable DTCheckedIds;
        IPriorArtHivCare Iprior;
        //public string availableTBResults, chestXRay, tissueBiopsy, IPT, AdherenceAddressed, missedAnyDoses, ReferredForAdherence, ContactsScreenedForTB;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Authenticate();
                FillDropDown();
                if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
                {
                    DataSet theDSARTHistory = new DataSet();
                    IPriorArtHivCare ARTHistoryMgr = (IPriorArtHivCare)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPriorArtHivCare, BusinessProcess.Clinical");
                    theDSARTHistory = ARTHistoryMgr.GetGreenCardData(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["PatientVisitId"]), Convert.ToInt32(Session["AppLocationId"]));
                    if (Convert.ToInt32(theDSARTHistory.Tables[3].Rows[0]["TypeofVisit"]) > 0)
                    {
                        ddlvisittype.SelectedValue = theDSARTHistory.Tables[3].Rows[0]["TypeofVisit"].ToString();
                        if (ddlvisittype.SelectedValue.ToString() == "Initial only")
                        {
                            GetGreenCardData();
                            ScriptManager.RegisterStartupScript(this, GetType(), "DisableTabbyId", "DisableTabbyId('follow');", true);
                        }
                        else if (ddlvisittype.SelectedValue.ToString() == "Follow Up")
                        {
                            GreenCardFollowupData();
                            ScriptManager.RegisterStartupScript(this, GetType(), "DisableTabbyId", "DisableTabbyId('int');", true);
                        }
                    }


                }
                Init_ARTHistory();                
                PatientDetails();
                BindFamilyGrid();
                BindSubsSwitchs();
                BindrcbmedicalCondition();
                BindPresentingCompControl();
                if (theDTGrid != null && theDTGrid.Rows.Count > 0)
                {
                    theDTView = new DataView(theDTGrid);

                    theDTView.RowFilter = "Section = Immunization";
                    if (theDTView.Count > 0)
                    {
                        ViewState["GridImmunizationData"] = (DataTable)theUtils.CreateTableFromDataView(theDTView);
                    }
                    
                }
                else
                {                    
                    ViewState["GridImmunizationData"] = theDTGrid;                    
                }


            }
            else
            {
                Init_ARTHistory();
                //First ART Regimen
                if ((DataTable)Application["AddRegimen7"] != null)
                {
                    DataTable theDT = (DataTable)Application["AddRegimen7"];
                    ViewState["SelectedData"] = theDT;
                    string theStr = FillRegimen(theDT);
                    txtFirstARTRegimen.Text = theStr;
                    Application.Remove("AddRegimen7");
                }
                //Prior ART IE Regimen
                if ((DataTable)Application["AddRegimen6"] != null)
                {
                    DataTable theDT = (DataTable)Application["AddRegimen6"];
                    ViewState["SelectedData"] = theDT;
                    string theStr = FillRegimen(theDT);
                    txtRegimen.Text = theStr;
                    Application.Remove("AddRegimen6");
                }
                //Prior ART FWP Regimen
                if ((DataTable)Application["AddRegimen5"] != null)
                {
                    DataTable theDT = (DataTable)Application["AddRegimen5"];
                    ViewState["SelectedData"] = theDT;
                    string theStr = FillRegimen(theDT);
                    txtRegimenFWP.Text = theStr;
                    Application.Remove("AddRegimen5");
                }
            }
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "BMI", "CalcualteBMI('" + txtbaseweight.ClientID + "','" + txtbaseheight.ClientID + "','" + txtbaseBMI.ClientID + "');", true);
            txtbaseheight.Attributes.Add("OnBlur", "CalcualteBMI('" + txtbaseweight.ClientID + "','" + txtbaseheight.ClientID + "','" + txtbaseBMI.ClientID + "');");
            txtbaseweight.Attributes.Add("OnBlur", "CalcualteBMI('" + txtbaseweight.ClientID + "','" + txtbaseheight.ClientID + "','" + txtbaseBMI.ClientID + "');");


        }
        protected void GetGreenCardData()
        {
            DataSet theDSARTHistory = new DataSet();
            IPriorArtHivCare ARTHistoryMgr = (IPriorArtHivCare)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPriorArtHivCare, BusinessProcess.Clinical");
            theDSARTHistory = ARTHistoryMgr.GetGreenCardData(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["PatientVisitId"]), Convert.ToInt32(Session["AppLocationId"]));


            if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
            {
                if (theDSARTHistory.Tables[0].Rows[0]["VisitDate"].ToString() != "01-Jan-1900")
                    txtVisitDate.Text = String.Format("{0:dd-MMM-yyyy}", theDSARTHistory.Tables[0].Rows[0]["VisitDate"]);
                if (theDSARTHistory.Tables[0].Rows[0]["VisitDate"].ToString() != "01-Jan-1900")
                    txtTransferInDate.Text = String.Format("{0:dd-MMM-yyyy}", theDSARTHistory.Tables[0].Rows[0]["TransferInDate"]);
                ddlvisittype.SelectedValue = Convert.ToString(theDSARTHistory.Tables[0].Rows[0]["VisitTypeId"]);
                ddltransferfrom.SelectedValue = Convert.ToString(theDSARTHistory.Tables[0].Rows[0]["TransferInFrom"]);
                dddistrict.SelectedValue = Convert.ToString(theDSARTHistory.Tables[0].Rows[0]["TransferInDistrictId"]);
                ddfacility.SelectedValue = Convert.ToString(theDSARTHistory.Tables[0].Rows[0]["TransferInFacilityId"]);
                txtMFLCode.Text = theDSARTHistory.Tables[0].Rows[0]["TransferMFLCode"].ToString();
                ddlCountryFrom.SelectedValue = Convert.ToString(theDSARTHistory.Tables[0].Rows[0]["TransferInCountryId"]);
                ddlbaseWHO.SelectedValue = Convert.ToString(theDSARTHistory.Tables[0].Rows[0]["BaseWHOStage"]);
                if (theDSARTHistory.Tables[0].Rows[0]["ARTStartDate"].ToString() != "01-Jan-1900")
                    txtDateARTStarted.Text = String.Format("{0:dd-MMM-yyyy}", theDSARTHistory.Tables[0].Rows[0]["ARTStartDate"]);
                txtbaseCD4.Text = theDSARTHistory.Tables[0].Rows[0]["BaseCD4"].ToString();

                if (Convert.ToInt32(theDSARTHistory.Tables[0].Rows[0]["BaseHepB"]) == 1)
                {
                    rdoHepByes.Checked = true;
                }
                else if (Convert.ToInt32(theDSARTHistory.Tables[0].Rows[0]["BaseHepB"]) == 0)
                {
                    rdoHepBNo.Checked = true;
                }
                if (Convert.ToInt32(theDSARTHistory.Tables[0].Rows[0]["BasePregnancy"]) == 1)
                {
                    rdobasePrgencyYes.Checked = true;
                }
                else if (Convert.ToInt32(theDSARTHistory.Tables[0].Rows[0]["BasePregnancy"]) == 0)
                {
                    rdobasePrgencyNo.Checked = true;
                }
                if (Convert.ToInt32(theDSARTHistory.Tables[0].Rows[0]["BaseBreastFeeding"]) == 1)
                {
                    rdobasebrestFeedYes.Checked = true;
                }
                else if (Convert.ToInt32(theDSARTHistory.Tables[0].Rows[0]["BaseBreastFeeding"]) == 0)
                {
                    rdobasebrestFeedNo.Checked = true;
                }
                if (Convert.ToInt32(theDSARTHistory.Tables[0].Rows[0]["BaseTBHIV"]) == 1)
                {
                    rdobaseTBYes.Checked = true;
                }
                else if (Convert.ToInt32(theDSARTHistory.Tables[0].Rows[0]["BaseTBHIV"]) == 0)
                {
                    rdobaseTBNo.Checked = true;
                }
                if (Convert.ToInt32(theDSARTHistory.Tables[0].Rows[0]["BaseDiscordant"]) == 1)
                {
                    rdoDiscordantYes.Checked = true;
                }
                else if (Convert.ToInt32(theDSARTHistory.Tables[0].Rows[0]["BaseDiscordant"]) == 0)
                {
                    rdoDiscordantNo.Checked = true;
                }
                if (Convert.ToInt32(theDSARTHistory.Tables[0].Rows[0]["PartnerDisclosure"]) == 1)
                {
                    rdodiclosureyes.Checked = true;
                }
                else if (Convert.ToInt32(theDSARTHistory.Tables[0].Rows[0]["PartnerDisclosure"]) == 0)
                {
                    rdodiclosureno.Checked = true;

                }
                if (Convert.ToInt32(theDSARTHistory.Tables[0].Rows[0]["INHCompletion"]) == 1)
                {
                    Inhcompyes.Checked = true;
                }
                else if (Convert.ToInt32(theDSARTHistory.Tables[0].Rows[0]["INHCompletion"]) == 0)
                {
                    InhcompNo.Checked = true;
                }

                txtbaseweight.Text = theDSARTHistory.Tables[0].Rows[0]["BaseWeight"].ToString();
                txtbaseheight.Text = theDSARTHistory.Tables[0].Rows[0]["BaseHeight"].ToString();
                txtbaseMUAC.Text = theDSARTHistory.Tables[0].Rows[0]["BaseMUAC"].ToString();
                txtFirstARTRegimen.Text = theDSARTHistory.Tables[0].Rows[0]["ARTRegimen"].ToString();
                txtBaseViral.Text = theDSARTHistory.Tables[0].Rows[0]["BaseViralLoad"].ToString();
                if (theDSARTHistory.Tables[0].Rows[0]["BaseViralLoadDate"].ToString() != "01-Jan-1900")
                    txtbaseviraldate.Text = String.Format("{0:dd-MMM-yyyy}", theDSARTHistory.Tables[0].Rows[0]["BaseViralLoadDate"]);
                if (theDSARTHistory.Tables[0].Rows[0]["TransferOutDate"].ToString() != "01-Jan-1900")
                    txttransferdate.Text = String.Format("{0:dd-MMM-yyyy}", theDSARTHistory.Tables[0].Rows[0]["TransferOutDate"]);
                //theHT.Add("BCG", txtDiphtheria.Text);
                txtMeasles.Text = theDSARTHistory.Tables[0].Rows[0]["Measles"].ToString();
                txtIPV.Text = theDSARTHistory.Tables[0].Rows[0]["IPV"].ToString();
                txtPenta.Text = theDSARTHistory.Tables[0].Rows[0]["Penta"].ToString();
                txtPCV.Text = theDSARTHistory.Tables[0].Rows[0]["PCV"].ToString();
                txtBCG.Text = theDSARTHistory.Tables[0].Rows[0]["BCG"].ToString();
                txtHBV.Text = theDSARTHistory.Tables[0].Rows[0]["HBV"].ToString();


                txtFlu.Text = theDSARTHistory.Tables[0].Rows[0]["Flu"].ToString();
                txtHepatitis.Text = theDSARTHistory.Tables[0].Rows[0]["HepatitisB"].ToString();
                txtTetanus.Text = theDSARTHistory.Tables[0].Rows[0]["Tetanus"].ToString();
                txtPertussis.Text = theDSARTHistory.Tables[0].Rows[0]["Pertussis"].ToString();
                txtHaemophilus.Text = theDSARTHistory.Tables[0].Rows[0]["Haemophilus"].ToString();
                if (theDSARTHistory.Tables[0].Rows[0]["DeathDate"].ToString() != "01-Jan-1900")
                    txtdeathdate.Text = String.Format("{0:dd-MMM-yyyy}", theDSARTHistory.Tables[0].Rows[0]["DeathDate"]);
                if (theDSARTHistory.Tables[0].Rows[0]["PartnerDisclosureDate"].ToString() != "01-Jan-1900")
                    txtsexDisDate.Text = String.Format("{0:dd-MMM-yyyy}", theDSARTHistory.Tables[0].Rows[0]["PartnerDisclosureDate"]);
                if (theDSARTHistory.Tables[0].Rows[0]["AdolPartialDisclosureDate"].ToString() != "01-Jan-1900")
                    partDisDate.Text = String.Format("{0:dd-MMM-yyyy}", theDSARTHistory.Tables[0].Rows[0]["AdolPartialDisclosureDate"]);
                if (theDSARTHistory.Tables[0].Rows[0]["AdolFullDisclosureDate"].ToString() != "01-Jan-1900")
                    txtFullDisDate.Text = String.Format("{0:dd-MMM-yyyy}", theDSARTHistory.Tables[0].Rows[0]["AdolFullDisclosureDate"]);
                if (theDSARTHistory.Tables[0].Rows[0]["INHStartDate"].ToString() != "01-Jan-1900")
                    txtINHStartDate.Text = String.Format("{0:dd-MMM-yyyy}", theDSARTHistory.Tables[0].Rows[0]["INHStartDate"]);
                if (theDSARTHistory.Tables[0].Rows[0]["INHCompletionDate"].ToString() != "01-Jan-1900")
                    txtINHCompDate.Text = String.Format("{0:dd-MMM-yyyy}", theDSARTHistory.Tables[0].Rows[0]["INHCompletionDate"]);

                GrdPriorART.Columns.Clear();
                BindGrid();
                GrdPriorART.DataSource = theDSARTHistory.Tables[2];
                GrdPriorART.DataBind();
                DataTable theDTGRV = (DataTable)GrdPriorART.DataSource;
                ViewState["GridData"] = theDTGRV;
            }

        }
        private void BindFamilyGrid()
        {
            IFamilyInfo PatientFamilyInfo = (IFamilyInfo)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BFamilyInfo, BusinessProcess.Clinical");
            DataSet theDS = PatientFamilyInfo.GetAllFamilyData(Convert.ToInt32(Session["PatientId"]));

            //BoundField theCol0 = new BoundField();
            //theCol0.HeaderText = "Id";
            //theCol0.DataField = "Id";
            //theCol0.ItemStyle.CssClass = "textstyle";
            //ucFamily.grdFamily.Columns.Add(theCol0);

            //BoundField theCol1 = new BoundField();
            //theCol1.HeaderText = "Patientid";
            //theCol1.DataField = "ptn_pk";
            //theCol1.ItemStyle.CssClass = "textstyle";
            //ucFamily.grdFamily.Columns.Add(theCol1);

            //BoundField theCol2 = new BoundField();
            //theCol2.HeaderText = "Last Name";
            //theCol2.DataField = "RLastName";
            //theCol2.SortExpression = "RLastName";
            //theCol2.ItemStyle.CssClass = "textstyle";
            //theCol2.ReadOnly = true;
            //ucFamily.grdFamily.Columns.Add(theCol2);

            //BoundField theCol3 = new BoundField();
            //theCol3.HeaderText = "First Name";
            //theCol3.DataField = "RFirstName";
            //theCol3.ItemStyle.CssClass = "textstyle";
            //theCol3.SortExpression = "RFirstName";
            //theCol3.ReadOnly = true;
            //ucFamily.grdFamily.Columns.Add(theCol3);

            ////"Existing Hosp/Clinic #";//"Enrollment No.";
            //BoundField theCol4 = new BoundField();
            ////if (((DataTable)ViewState["grdDataSource"]).Rows.Count > 0)
            ////{
            ////    theCol4.HeaderText = ((DataTable)ViewState["grdDataSource"]).Rows[1][0].ToString().Trim();
            ////}
            //theCol4.HeaderText = "Registration No";
            //theCol4.ItemStyle.CssClass = "textstyle";
            //theCol4.DataField = "RegistrationNo";
            //theCol4.SortExpression = "RegistrationNo";
            //theCol4.ReadOnly = true;
            //ucFamily.grdFamily.Columns.Add(theCol4);

            //////"Existing Hosp/Clinic #";
            ////BoundField theCol5 = new BoundField();
            ////if (((DataTable)ViewState["grdDataSource"]).Rows.Count > 0)
            ////{
            ////    theCol5.HeaderText = ((DataTable)ViewState["grdDataSource"]).Rows[0][0].ToString().Trim();
            ////}
            ////theCol5.ItemStyle.CssClass = "textstyle";
            ////theCol5.DataField = "FileNo";
            ////theCol5.SortExpression = "FileNo";
            ////theCol5.ReadOnly = true;
            ////ucFamily.grdFamily.Columns.Add(theCol5);

            //BoundField theCol6 = new BoundField();
            //theCol6.HeaderText = "Registered?";
            //theCol6.ItemStyle.CssClass = "textstyle";
            //theCol6.DataField = "Registered?";
            //theCol6.SortExpression = "Registered?";
            //theCol6.ReadOnly = true;
            //ucFamily.grdFamily.Columns.Add(theCol6);

            //BoundField theCol7 = new BoundField(); // double
            //theCol7.HeaderText = "RelationshipTypeId";
            //theCol7.DataField = "RelationshipTypeId";
            //theCol7.ItemStyle.CssClass = "textstyle";
            //ucFamily.grdFamily.Columns.Add(theCol7);


            //BoundField theCol8 = new BoundField();
            //theCol8.HeaderText = "Relationship";
            //theCol8.DataField = "RelationshipTypeDesc";
            //theCol8.ItemStyle.CssClass = "textstyle";
            //theCol8.SortExpression = "RelationshipTypeDesc";
            //theCol8.ReadOnly = true;
            //ucFamily.grdFamily.Columns.Add(theCol8);


            //BoundField theCol8_1 = new BoundField();
            //theCol8_1.HeaderText = "Relationship Date";
            //theCol8_1.DataField = "RelationshipDate";
            ////theCol8_1.DataField = String.Format("{0:dd-MMM-yyyy}", theCol8_1.DataField);
            //theCol8_1.ItemStyle.CssClass = "textstyle";
            //theCol8_1.SortExpression = "RelationshipDate";
            //ucFamily.grdFamily.Columns.Add(theCol8_1);


            //BoundField theCol9 = new BoundField();
            //theCol9.HeaderText = "SexId";
            //theCol9.ItemStyle.CssClass = "textstyle";
            //theCol9.DataField = "SexId";
            //theCol9.ReadOnly = true;
            //ucFamily.grdFamily.Columns.Add(theCol9);

            //BoundField theCol10 = new BoundField();
            //theCol10.HeaderText = "Sex";
            //theCol10.DataField = "SexDesc";
            //theCol10.ItemStyle.CssClass = "textstyle";
            //theCol10.SortExpression = "SexDesc";
            //ucFamily.grdFamily.Columns.Add(theCol10);

            //BoundField theCol11 = new BoundField();
            //theCol11.HeaderText = "Age(yrs)";
            //theCol11.ItemStyle.CssClass = "textstyle";
            //theCol11.DataField = "AgeYear";
            //theCol11.SortExpression = "AgeYear";
            //theCol11.ReadOnly = true;
            //ucFamily.grdFamily.Columns.Add(theCol11);

            //BoundField theCol12 = new BoundField();
            //theCol12.HeaderText = "Age(mths)";
            //theCol12.ItemStyle.CssClass = "textstyle";
            //theCol12.DataField = "AgeMonth";
            //theCol12.SortExpression = "AgeMonth";
            //theCol12.ReadOnly = true;
            //ucFamily.grdFamily.Columns.Add(theCol12);

            //BoundField theCol13 = new BoundField();
            //theCol13.HeaderText = "HivStatusId";
            //theCol13.DataField = "HivStatusId";
            //theCol13.ItemStyle.CssClass = "textstyle";
            //ucFamily.grdFamily.Columns.Add(theCol13);

            //BoundField theCol14 = new BoundField();
            //theCol14.HeaderText = "HIVStatus";
            //theCol14.ItemStyle.CssClass = "textstyle";
            //theCol14.DataField = "HivStatusDesc";
            //theCol14.SortExpression = "HivStatusDesc";
            //theCol14.ReadOnly = true;
            //ucFamily.grdFamily.Columns.Add(theCol14);

            //BoundField theCol14_1 = new BoundField();
            //theCol14_1.HeaderText = "Last HIV Test Date";
            //theCol14_1.DataField = "LastHIVTestDate";
            ////theCol4_1.DataField = String.Format("{0:dd-MMM-yyyy}", theCol8_1.DataField);
            //theCol14_1.ItemStyle.CssClass = "textstyle";
            //theCol14_1.SortExpression = "LastHIVTestDate";
            //ucFamily.grdFamily.Columns.Add(theCol14_1);

            //BoundField theCol15 = new BoundField();
            //theCol15.HeaderText = "HIVCareStatusId";
            //theCol15.DataField = "HIVCareStatusId";
            //theCol15.ItemStyle.CssClass = "textstyle";
            //ucFamily.grdFamily.Columns.Add(theCol15);

            //BoundField theCol16 = new BoundField();
            //theCol16.HeaderText = "HIVCareStatus";
            //theCol16.DataField = "HivCareStatusDesc";
            //theCol16.ItemStyle.CssClass = "textstyle";
            //theCol16.SortExpression = "HivCareStatusDesc";
            //theCol16.ReadOnly = true;
            //ucFamily.grdFamily.Columns.Add(theCol16);

            ////ButtonField theBtn = new ButtonField();
            ////theBtn.ButtonType = ButtonType.Image;
            ////theBtn.HeaderText = "Delete";
            ////theBtn.CommandName = "Remove";
            ////theBtn.ImageUrl= "../Images/del.gif";
            //////theBtn.HeaderText = "Remove";
            ////ucFamily.grdFamily.Columns.Add(theBtn);

            ////if (Authentiaction.HasFunctionRight(ApplicationAccess.FamilyInfo, FunctionAccess.Delete, (DataTable)Session["UserRight"]) == true && Session["lblpntstatus"].ToString() != "1")
            ////{
            ////CommandField objfield = new CommandField();
            ////objfield.ButtonType = ButtonType.Link;
            ////objfield.DeleteText = "<img src='../Images/del.gif' alt='Delete this' border='0' />";
            ////objfield.ShowDeleteButton = true;
            ////ucFamily.grdFamily.Columns.Add(objfield);

            ////}

            ucFamily.grdFamily.DataSource = theDS.Tables[0];
            ucFamily.grdFamily.DataBind();
            ucFamily.grdFamily.Columns[0].Visible = false;
            ucFamily.grdFamily.Columns[1].Visible = false;
            ucFamily.grdFamily.Columns[7].Visible = false;
            ucFamily.grdFamily.Columns[10].Visible = false;
            ucFamily.grdFamily.Columns[14].Visible = false;
            ucFamily.grdFamily.Columns[17].Visible = false;
        }
        private void BindSubsSwitchs()
        {
            //Get ARV Therapy details
            IPatientARTCare IEManager = (IPatientARTCare)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientARTCare, BusinessProcess.Clinical");
            DataSet theDS = IEManager.GetPatientARVTherapy(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["AppLocationId"]));
            //Grid Substitution of ARVs
            ucDrug.grdSubsARVs.DataSource = theDS.Tables[12];
            ucDrug.grdSubsARVs.DataBind();
            //Grid Viral Load tracker
            ucViralTrack.grdviralload.DataSource = theDS.Tables[18];
            ucViralTrack.grdviralload.DataBind();
        }
        private void PatientDetails()
        {
            IPatientHome PatientManager;
            PatientManager = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
            System.Data.DataSet theDS = PatientManager.GetPatientDetails(Convert.ToInt32(Session["PatientId"]), 0, 0);
            PatientManager = null;
            #region "Fill Details"
            if (theDS.Tables[0].Rows.Count > 0)
            {
                string theAddress = "";

                lblPatName.Text = theDS.Tables[0].Rows[0]["FullName"].ToString();

                lblDOB.Text = Convert.ToDateTime(theDS.Tables[0].Rows[0]["DOB"]).ToString("dd-MMM-yyyy");
                if (theDS.Tables[0].Rows[0]["Address"].ToString() != "")
                {
                    theAddress = theDS.Tables[0].Rows[0]["Address"].ToString() + "/";
                    //Session["Address"] = theDS.Tables[0].Rows[0]["Address"].ToString();
                }

                if (theDS.Tables[0].Rows[0]["VillageNM"].ToString() != "")
                {
                    theAddress = theAddress + theDS.Tables[0].Rows[0]["VillageNM"].ToString() + "/";
                    //Session["Village"] = theDS.Tables[0].Rows[0]["VillageNM"].ToString();
                }
                //else
                //{
                //    //theAddress = theAddress + " /";
                //    //Session["Village"] = "/";

                //}
                lblmaritalstatus.Text = theDS.Tables[0].Rows[0]["MaritalStatus"].ToString();
                lbluniquename.Text = theDS.Tables[0].Rows[0]["IQNumber"].ToString();

                lblsex.Text = theDS.Tables[0].Rows[0]["SexNM"].ToString();
                //lblAge.Text = theDS.Tables[0].Rows[0]["AGE"].ToString() + " yrs" + theDS.Tables[0].Rows[0]["AgeInMonths"].ToString() + " months";
                lblAge.Text = theDS.Tables[0].Rows[0]["AGEINYEARMONTH"].ToString();

                if (theDS.Tables[0].Rows[0]["ProvinceNM"].ToString() != "")
                {
                    theAddress = theAddress + theDS.Tables[0].Rows[0]["ProvinceNM"].ToString();
                }
                txtpostaladdress.Text = theAddress;
                if (theDS.Tables[0].Rows[0]["NearestHC"].ToString() != "")
                {
                    txtNearHC.Text = theDS.Tables[0].Rows[0]["NearestHC"].ToString();
                }

                if (theDS.Tables[0].Rows[0]["SubCountry"].ToString() != "")
                {
                    txtpatSubCountry.Text = theDS.Tables[0].Rows[0]["SubCountry"].ToString();

                }
                if (theDS.Tables[0].Rows[0]["Landmark"].ToString() != "")
                {
                    txtPatLandmark.Text = theDS.Tables[0].Rows[0]["Landmark"].ToString();

                }
                if (theDS.Tables[0].Rows[0]["SubLocation"].ToString() != "0")
                {
                    txtPatSubLocation.Text = theDS.Tables[0].Rows[0]["SubLocation"].ToString();

                }
                if (theDS.Tables[0].Rows[0]["EmergContactName"].ToString() != "")
                {
                    txtGuardName.Text = theDS.Tables[0].Rows[0]["EmergContactName"].ToString();
                }

                if (theDS.Tables[0].Rows[0]["EmergContactPhone"].ToString() != "")
                {
                    txtGuardTel.Text = theDS.Tables[0].Rows[0]["EmergContactPhone"].ToString();

                }
                if (theDS.Tables[0].Rows[0]["EmergContactAddress"].ToString() != "")
                {
                    txtGuardAddress.Text = theDS.Tables[0].Rows[0]["EmergContactAddress"].ToString();

                }
                if (theDS.Tables[0].Rows[0]["EmergContactRelation"].ToString() != "0")
                {
                    ddlGuardRelationship.SelectedValue = theDS.Tables[0].Rows[0]["EmergContactRelation"].ToString();

                }


            }

            #endregion

        }
        private void BindWHOListData(DataTable theDT)
        {
            foreach (DataRow theDR in theDT.Rows)
            {

                if (Convert.ToString(theDR["FieldName"]) == "CurrentWHOStageIConditions")
                {
                    for (int i = 0; i < this.UCWHO.gvWHO1.Rows.Count; i++)
                    {
                        Label lblWHOId = (Label)UCWHO.gvWHO1.Rows[i].FindControl("lblwho1");
                        CheckBox chkWHOId = (CheckBox)UCWHO.gvWHO1.Rows[i].FindControl("Chkwho1");
                        HtmlInputText txtWHODate1 = (HtmlInputText)UCWHO.gvWHO1.Rows[i].FindControl("txtCurrentWho1Date");
                        HtmlInputText txtWHODate2 = (HtmlInputText)UCWHO.gvWHO1.Rows[i].FindControl("txtCurrentWho1Date1");
                        if (Convert.ToInt32(theDR["ValueId"]) == Convert.ToInt32(lblWHOId.Text))
                        {
                            chkWHOId.Checked = true;
                            if (theDR["DateField1"].ToString() != "")
                                txtWHODate1.Value = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(theDR["DateField1"].ToString()));
                            if (theDR["DateField2"].ToString() != "")
                                txtWHODate2.Value = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(theDR["DateField2"].ToString()));
                        }
                    }
                }

                else if (Convert.ToString(theDR["FieldName"]) == "CurrentWHOStageIIConditions")
                {
                    for (int i = 0; i < this.UCWHO.gvWHO2.Rows.Count; i++)
                    {
                        Label lblWHOId = (Label)UCWHO.gvWHO2.Rows[i].FindControl("lblwho2");
                        CheckBox chkWHOId = (CheckBox)UCWHO.gvWHO2.Rows[i].FindControl("Chkwho2");
                        HtmlInputText txtWHODate1 = (HtmlInputText)UCWHO.gvWHO2.Rows[i].FindControl("txtCurrentWho2Date");
                        HtmlInputText txtWHODate2 = (HtmlInputText)UCWHO.gvWHO2.Rows[i].FindControl("txtCurrentWho2Date1");
                        if (Convert.ToInt32(theDR["ValueId"]) == Convert.ToInt32(lblWHOId.Text))
                        {
                            chkWHOId.Checked = true;
                            if (theDR["DateField1"].ToString() != "")
                                txtWHODate1.Value = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(theDR["DateField1"].ToString()));
                            if (theDR["DateField2"].ToString() != "")
                                txtWHODate2.Value = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(theDR["DateField2"].ToString()));
                        }
                    }
                }

                else if (Convert.ToString(theDR["FieldName"]) == "CurrentWHOStageIIIConditions")
                {
                    for (int i = 0; i < this.UCWHO.gvWHO3.Rows.Count; i++)
                    {
                        Label lblWHOId = (Label)UCWHO.gvWHO3.Rows[i].FindControl("lblwho3");
                        CheckBox chkWHOId = (CheckBox)UCWHO.gvWHO3.Rows[i].FindControl("Chkwho3");
                        HtmlInputText txtWHODate1 = (HtmlInputText)UCWHO.gvWHO3.Rows[i].FindControl("txtCurrentWho3Date");
                        HtmlInputText txtWHODate2 = (HtmlInputText)UCWHO.gvWHO3.Rows[i].FindControl("txtCurrentWho3Date1");
                        if (Convert.ToInt32(theDR["ValueId"]) == Convert.ToInt32(lblWHOId.Text))
                        {
                            chkWHOId.Checked = true;
                            if (theDR["DateField1"].ToString() != "")
                                txtWHODate1.Value = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(theDR["DateField1"].ToString()));
                            if (theDR["DateField2"].ToString() != "")
                                txtWHODate2.Value = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(theDR["DateField2"].ToString()));
                        }
                    }
                }

                else if (Convert.ToString(theDR["FieldName"]) == "CurrentWHOStageIVConditions")
                {
                    for (int i = 0; i < this.UCWHO.gvWHO4.Rows.Count; i++)
                    {
                        Label lblWHOId = (Label)UCWHO.gvWHO4.Rows[i].FindControl("lblwho4");
                        CheckBox chkWHOId = (CheckBox)UCWHO.gvWHO4.Rows[i].FindControl("Chkwho4");
                        HtmlInputText txtWHODate1 = (HtmlInputText)UCWHO.gvWHO4.Rows[i].FindControl("txtCurrentWho4Date");
                        HtmlInputText txtWHODate2 = (HtmlInputText)UCWHO.gvWHO4.Rows[i].FindControl("txtCurrentWho4Date1");
                        if (Convert.ToInt32(theDR["ValueId"]) == Convert.ToInt32(lblWHOId.Text))
                        {
                            chkWHOId.Checked = true;
                            if (theDR["DateField1"].ToString() != "")
                                txtWHODate1.Value = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(theDR["DateField1"].ToString()));
                            if (theDR["DateField2"].ToString() != "")
                                txtWHODate2.Value = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(theDR["DateField2"].ToString()));
                        }
                    }
                }




            }

        }
        public void FillCheckboxlist(CheckBoxList chk, DataTable thedt, string name)
        {
            IQCareUtils iQCareUtils = new IQCareUtils();
            DataTable dt = new DataTable();
            string script = string.Empty;
            if (thedt.Rows.Count > 0)
            {
                DataView theDV = new DataView(thedt);
                theDV.RowFilter = "FieldName='" + name + "'";
                dt = (DataTable)iQCareUtils.CreateTableFromDataView(theDV);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < chk.Items.Count; j++)
                    {
                        if (chk.Items[j].Value == dt.Rows[i]["ValueID"].ToString())
                        {
                            chk.Items[j].Selected = true;
                            if (name == "ChronicCondition")
                            {
                                if (chk.Items[j].Text == "Other specify")
                                {
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'divothercondition'>\n";
                                    script += "ShowHide('divOtherChronicCondition','show');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("divothercondition", script);
                                }
                            }
                            if (name == "TBSideEffects")
                            {
                                if (chk.Items[j].Text == "Other Side effects (specify)")
                                {
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'divothertbshowhideyes'>\n";
                                    script += "ShowHide('divothertbshowhide','show');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("divothertbshowhideyes", script);
                                }
                            }
                            if (name == "DrugAllergiesToxicitiesPaeds")
                            {
                                if (chk.Items[j].Text == "ARV")
                                {
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'divspecifyarvallergyshowhideA'>\n";
                                    script += "ShowHide('divspecifyarvallergyshowhide','show');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("divspecifyarvallergyshowhideA", script);
                                }
                                if (chk.Items[j].Text == "Antibiotic")
                                {
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'divspecifyantibioticshowhideA'>\n";
                                    script += "ShowHide('divspecifyantibioticshowhide','show');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("divspecifyantibioticshowhideA", script);
                                }
                                if (chk.Items[j].Text == "Other")
                                {
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'divspecifyotherdrugshowhideA'>\n";
                                    script += "ShowHide('divspecifyotherdrugshowhide','show');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("divspecifyotherdrugshowhideA", script);
                                }
                            }
                            if (name == "LongTermEffects")
                            {
                                if (chk.Items[j].Text == "Other specify")
                                {
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'divspecityotherlogntermeffectsy'>\n";
                                    script += "ShowHide('divspecityotherlogntermeffects','show');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("divspecityotherlogntermeffectsy", script);
                                }
                            }
                            if (name == "ShortTermEffects")
                            {
                                if (chk.Items[j].Text == "Other Specify")
                                {
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'divothershorttermeffectsshowhidey'>\n";
                                    script += "ShowHide('divothershorttermeffectsshowhide','show');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("divothershorttermeffectsshowhidey", script);
                                }
                            }

                            if (name == "Counselling")
                            {
                                if (chk.Items[j].Text == "Other")
                                {
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'divothercounsellingshowhidey'>\n";
                                    script += "ShowHide('divothercounsellingshowhide','show');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("divothercounsellingshowhidey", script);
                                }
                            }
                            if (name == "RefferedToFUpF")
                            {
                                if (chk.Items[j].Text.ToLower() == "other specialist clinic")
                                {
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'TriagedivReferToSpecialistClinicyesno'>\n";
                                    script += "ShowHide('TriagedivReferToSpecialistClinic','show');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("TriagedivReferToSpecialistClinicyesno", script);
                                }
                                if (chk.Items[j].Text.ToLower() == "other (specify)")
                                {
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'TriagedivReferToOtheryesno'>\n";
                                    script += "ShowHide('TriagedivReferToOther','show');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("TriagedivReferToOtheryesno", script);
                                }
                            }
                        }
                    }
                }

            }
        }    
        protected void GreenCardFollowupData()
        {

            Iprior = (IPriorArtHivCare)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPriorArtHivCare, BusinessProcess.Clinical");
            try
            {
                DataSet theDS = Iprior.GetGreenCardFollowup(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["PatientVisitId"]));                

                if (theDS.Tables[0].Rows.Count > 0)
                {
                    if (theDS.Tables[0].Rows[0]["VisitDate"] != System.DBNull.Value)
                    {
                        this.txtVisitDate.Text = String.Format("{0:dd-MMM-yyyy}", theDS.Tables[0].Rows[0]["VisitDate"]).ToUpper();
                    }
                   
                }
                // Vital Sign
                if (theDS.Tables[2].Rows.Count > 0)
                {
                    if (theDS.Tables[2].Rows[0]["TEMP"] != System.DBNull.Value)
                    {
                        idVitalSign.txtTemp.Text = theDS.Tables[2].Rows[0]["TEMP"].ToString();
                    }
                    if (theDS.Tables[2].Rows[0]["RR"] != System.DBNull.Value)
                    {
                        idVitalSign.txtRR.Text = theDS.Tables[2].Rows[0]["RR"].ToString();
                    }
                    if (theDS.Tables[2].Rows[0]["HR"] != System.DBNull.Value)
                    {
                        idVitalSign.txtHR.Text = theDS.Tables[2].Rows[0]["HR"].ToString();
                    }
                    if (theDS.Tables[2].Rows[0]["Height"] != System.DBNull.Value)
                    {
                        idVitalSign.txtHeight.Text = theDS.Tables[2].Rows[0]["Height"].ToString();
                    }
                    if (theDS.Tables[2].Rows[0]["Weight"] != System.DBNull.Value)
                    {
                        idVitalSign.txtWeight.Text = theDS.Tables[2].Rows[0]["Weight"].ToString();
                    }
                    if (theDS.Tables[2].Rows[0]["BPSystolic"] != System.DBNull.Value)
                    {
                        idVitalSign.txtBPSystolic.Text = theDS.Tables[2].Rows[0]["BPSystolic"].ToString();
                    }
                    if (theDS.Tables[2].Rows[0]["BPDiastolic"] != System.DBNull.Value)
                    {
                        idVitalSign.txtBPDiastolic.Text = theDS.Tables[2].Rows[0]["BPDiastolic"].ToString();
                    }
                    if (theDS.Tables[2].Rows[0]["Headcircumference"] != System.DBNull.Value)
                    {
                        idVitalSign.txtheadcircumference.Text = theDS.Tables[2].Rows[0]["Headcircumference"].ToString();
                    }
                    if (theDS.Tables[2].Rows[0]["WeightForAge"] != System.DBNull.Value)
                    {
                        idVitalSign.lblWA.Text = theDS.Tables[2].Rows[0]["WeightForAge"].ToString();
                    }
                    if (theDS.Tables[2].Rows[0]["WeightForHeight"] != System.DBNull.Value)
                    {
                        idVitalSign.lblWH.Text = theDS.Tables[2].Rows[0]["WeightForHeight"].ToString();
                    }
                    if (theDS.Tables[2].Rows[0]["BMIz"] != System.DBNull.Value)
                    {
                        idVitalSign.lblBMIz.Text = theDS.Tables[2].Rows[0]["BMIz"].ToString();
                    }
                    if (theDS.Tables[2].Rows[0]["NurseComments"] != System.DBNull.Value)
                    {
                        idVitalSign.txtnursescomments.Text = theDS.Tables[2].Rows[0]["NurseComments"].ToString();
                    }
                }

                CheckBoxList chkPEGeneral = (CheckBoxList)this.UCPhyExam.FindControl("cblGeneralConditions");
                FillCheckboxlist(chkPEGeneral, theDS.Tables[4], "GeneralConditions");
                //Cardiovascular conditions
                CheckBoxList chkPECardiovascular = (CheckBoxList)this.UCPhyExam.FindControl("cblCardiovascularConditions");
                FillCheckboxlist(chkPECardiovascular, theDS.Tables[4], "CardiovascularConditions");
                //CNS 
                CheckBoxList chkPECNS = (CheckBoxList)this.UCPhyExam.FindControl("cblCNSConditions");
                FillCheckboxlist(chkPECNS, theDS.Tables[4], "CNSConditions");
                //Oral cavity
                CheckBoxList chkPEOralCavity = (CheckBoxList)this.UCPhyExam.FindControl("cblOralCavityConditions");
                FillCheckboxlist(chkPEOralCavity, theDS.Tables[4], "OralCavityConditions");
                //Chest Lungs
                CheckBoxList chkPEChestLungs = (CheckBoxList)this.UCPhyExam.FindControl("cblChestLungsConditions");
                FillCheckboxlist(chkPEChestLungs, theDS.Tables[4], "ChestLungsConditions");
                //Genitourinary 
                CheckBoxList chkPEGenitourinary = (CheckBoxList)this.UCPhyExam.FindControl("cblGenitalUrinaryConditions");
                FillCheckboxlist(chkPEGenitourinary, theDS.Tables[4], "GenitalUrinaryConditions");
                //Skin 
                CheckBoxList chkPESkin = (CheckBoxList)this.UCPhyExam.FindControl("cblSkinConditions");
                FillCheckboxlist(chkPESkin, theDS.Tables[4], "SkinConditions");
                //Abdomen conditions
                CheckBoxList chkPEabdomen = (CheckBoxList)this.UCPhyExam.FindControl("cblAbdomenConditions");
                FillCheckboxlist(chkPEabdomen, theDS.Tables[4], "AbdomenConditions");

                UCPhyExam.txtOtherGeneralConditions.Text = theDS.Tables[1].Rows[0]["OtherGeneralConditions"].ToString();
                UCPhyExam.txtOtherAbdomenConditions.Text = theDS.Tables[1].Rows[0]["OtherAbdomenConditions"].ToString();
                UCPhyExam.txtOtherCardiovascularConditions.Text = theDS.Tables[1].Rows[0]["OtherCardiovascularConditions"].ToString();
                UCPhyExam.txtOtherOralCavityConditions.Text = theDS.Tables[1].Rows[0]["OtherOralCavityConditions"].ToString();
                UCPhyExam.txtOtherGenitourinaryConditions.Text = theDS.Tables[1].Rows[0]["OtherGenitourinaryConditions"].ToString();
                UCPhyExam.txtOtherCNSConditions.Text = theDS.Tables[1].Rows[0]["OtherCNSConditions"].ToString();
                UCPhyExam.txtOtherChestLungsConditions.Text = theDS.Tables[1].Rows[0]["OtherChestLungsConditions"].ToString();
                UCPhyExam.txtOtherSkinConditions.Text = theDS.Tables[1].Rows[0]["OtherSkinConditions"].ToString();


                if (theDS.Tables[0].Rows[0]["OtherMedicalConditionNotes"] != DBNull.Value)
                    UCPhyExam.txtOtherMedicalConditionNotes.Text = theDS.Tables[1].Rows[0]["OtherMedicalConditionNotes"].ToString();
                ////////////WHO Stage///////////////////////////////////////////
                

                ////WHO Stage I                
                BindWHOListData(theDS.Tables[4]);

                if (theDS.Tables[5].Rows.Count > 0)
                {
                    DataView dv = new DataView(theDS.Tables[5]);
                    
                    // Immunization 
                    dv.RowFilter = "Section = 'Immunization'";
                    DataTable dt = (DataTable)theUtils.CreateTableFromDataView(dv);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        GrdImmunization.Columns.Clear();
                        BindGrid(GrdImmunization, "Immunization");
                        dt.TableName = "dtImmunization";
                        GrdImmunization.DataSource = dt;
                        GrdImmunization.DataBind();
                        ViewState["GridImmunizationData"] = dt;
                    }

                    
                    
                }

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
                Iprior = null;
            }
        }
        private void BindGrid()
        {
            BoundField theCol0 = new BoundField();
            theCol0.HeaderText = "VisitId";
            theCol0.DataField = "VisitId";
            theCol0.ItemStyle.CssClass = "textstyle";
            GrdPriorART.Columns.Add(theCol0);

            BoundField theCol1 = new BoundField();
            theCol1.HeaderText = "Patientid";
            theCol1.DataField = "ptn_pk";
            theCol1.ItemStyle.CssClass = "textstyle";
            GrdPriorART.Columns.Add(theCol1);

            BoundField theCol6 = new BoundField();
            theCol6.HeaderText = "PurposeId";
            theCol6.DataField = "PurposeId";
            GrdPriorART.Columns.Add(theCol6);

            BoundField theCol2 = new BoundField();
            theCol2.HeaderText = "Purpose";
            theCol2.DataField = "Purpose";
            theCol2.SortExpression = "Purpose";
            theCol2.ReadOnly = true;
            GrdPriorART.Columns.Add(theCol2);

            BoundField theCol3 = new BoundField();
            theCol3.HeaderText = "Regimen";
            theCol3.DataField = "Regimen";
            theCol3.SortExpression = "Regimen";
            theCol3.ReadOnly = true;
            GrdPriorART.Columns.Add(theCol3);

            BoundField theCol4 = new BoundField();
            theCol4.HeaderText = "Regimen Last Used";
            theCol4.DataField = "RegLastUsed";
            theCol4.DataFormatString = "{0:dd-MMM-yyyy}";
            theCol4.SortExpression = "RegLastUsed";
            theCol4.ReadOnly = true;
            GrdPriorART.Columns.Add(theCol4);

            CommandField theCol5 = new CommandField();
            theCol5.ButtonType = ButtonType.Link;
            theCol5.DeleteText = "<img src='../Images/del.gif' alt='Delete this' border='0' />";
            theCol5.ShowDeleteButton = true;
            GrdPriorART.Columns.Add(theCol5);

            GrdPriorART.Columns[0].Visible = false;
            GrdPriorART.Columns[1].Visible = false;
            GrdPriorART.Columns[2].Visible = false;
        }
        private void Refresh()
        {
            ddlpurpose.SelectedIndex = -1;
            txtRegimen.Text = "";
            txtLastUsed.Text = "";

        }
        protected void btnAddPriorART_Click(object sender, EventArgs e)
        {
            if (ValidateLastUsed() == false)
            {
                return;
            }
            int VisitId = Convert.ToInt32(Session["PatientVisitId"]) > 0 ? Convert.ToInt32(Session["PatientVisitId"]) : 0;
            DataTable theDT = new DataTable();
            if ((DataTable)ViewState["GridData"] == null)
            {
                theDT.Columns.Add("ptn_pk", typeof(Int32));
                theDT.Columns.Add("VisitId", typeof(Int32));
                theDT.Columns.Add("PurposeId", typeof(Int32));
                theDT.Columns.Add("Purpose", typeof(string));
                theDT.Columns.Add("Regimen", typeof(string));
                theDT.Columns.Add("RegLastUsed", typeof(string));
                DataRow theDR = theDT.NewRow();
                theDR["ptn_pk"] = Session["PatientId"];
                theDR["VisitId"] = VisitId;
                theDR["Purpose"] = ddlpurpose.SelectedItem.Text;
                theDR["Regimen"] = txtRegimen.Text;
                theDR["RegLastUsed"] = "" + txtLastUsed.Text + "";
                theDR["PurposeId"] = ddlpurpose.SelectedValue;
                theDT.Rows.Add(theDR);
                GrdPriorART.Columns.Clear();
                BindGrid();
                Refresh();
                GrdPriorART.DataSource = theDT;
                GrdPriorART.DataBind();
                ViewState["GridData"] = theDT;
            }
            else
            {

                theDT = (DataTable)ViewState["GridData"];
                if (Convert.ToInt32(ViewState["UpdateFlag"]) == 1)
                {
                    DataRow[] rows = theDT.Select("PurposeId=" + ViewState["SelectedPurposeId"] + " and Regimen='" + ViewState["SelectedRegimen"] + "' and RegLastUsed='" + ViewState["SelectedLastused"] + "'");
                    for (int i = 0; i < rows.Length; i++)
                    {
                        rows[i]["ptn_pk"] = Session["PatientId"];
                        rows[i]["VisitId"] = VisitId;
                        rows[i]["PurposeId"] = ddlpurpose.SelectedValue;
                        rows[i]["Purpose"] = ddlpurpose.SelectedItem.Text;
                        rows[i]["Regimen"] = txtRegimen.Text;
                        rows[i]["RegLastUsed"] = "" + txtLastUsed.Text + "";
                        theDT.AcceptChanges();
                    }
                    GrdPriorART.Columns.Clear();
                    BindGrid();
                    Refresh();
                    GrdPriorART.DataSource = theDT;
                    GrdPriorART.DataBind();
                    ViewState["GridData"] = theDT;
                    ViewState["UpdateFlag"] = "0";
                    DataTable theGVDT = (DataTable)GrdPriorART.DataSource;
                }
                else
                {
                    DataRow theDR = theDT.NewRow();
                    theDR["ptn_pk"] = Session["PatientId"];
                    theDR["VisitId"] = VisitId;
                    theDR["Purpose"] = ddlpurpose.SelectedItem.Text;
                    theDR["Regimen"] = txtRegimen.Text;
                    theDR["RegLastUsed"] = "" + txtLastUsed.Text + "";
                    theDR["PurposeId"] = ddlpurpose.SelectedValue;
                    theDT.Rows.Add(theDR);
                    GrdPriorART.Columns.Clear();
                    BindGrid();
                    Refresh();
                    GrdPriorART.DataSource = theDT;
                    GrdPriorART.DataBind();
                    ViewState["GridData"] = theDT;
                }
            }
        }
        protected void btnRegimenFA_Click(object sender, EventArgs e)
        {
            string theScript;
            Application.Add("MasterData", ViewState["MasterARVData"]);
            Session["DrugData"] = ViewState["MasterARVData"];
            Application.Add("SelectedDrugFA", (DataTable)ViewState["SelectedDataFA"]);
            theScript = "<script language='javascript' id='DrgPopupFA'>\n";
            theScript += "window.open('../Pharmacy/frmDrugSelector.aspx?DrugType=37&btnreg=btnRegimenGCIEFA' ,'DrugSelectionFA','toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=no,resize=no,width=700,height=350,scrollbars=yes');\n";
            theScript += "</script>\n";
            Page.RegisterStartupScript("DrgPopupFA", theScript);
        }
        protected void btnRegimen_Click(object sender, EventArgs e)
        {
            string theScript;
            Application.Add("MasterData", ViewState["MasterARVData"]);
            Session["DrugData"] = ViewState["MasterARVData"];
            Application.Add("SelectedDrug", (DataTable)ViewState["SelectedData"]);
            theScript = "<script language='javascript' id='DrgPopupIE'>\n";
            theScript += "window.open('../Pharmacy/frmDrugSelector.aspx?DrugType=37&btnreg=btnRegimenGCIE' ,'DrugSelection','toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=no,resize=no,width=700,height=350,scrollbars=yes');\n";
            theScript += "</script>\n";
            Page.RegisterStartupScript("DrgPopupIE", theScript);
        }
        protected void GrdPriorART_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    if (e.Row.Cells[0].Text.ToString() != "0")
                    {
                        e.Row.Cells[i].Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.BackColor='#666699';");
                        e.Row.Cells[i].Attributes.Add("onmouseout", "this.style.backgroundColor='';");
                        e.Row.Cells[i].Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(GrdPriorART, "Select$" + e.Row.RowIndex.ToString()));
                    }
                    if (e.Row.Cells[5].Text.ToString() == "01-Jan-1900")
                    {
                        e.Row.Cells[5].Text = "";
                    }

                }
            }
        }
        protected void GrdPriorART_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int thePage = GrdPriorART.PageIndex;
            int thePageSize = GrdPriorART.PageSize;
            GridViewRow theRow = GrdPriorART.Rows[e.NewSelectedIndex];
            int theIndex = thePageSize * thePage + theRow.RowIndex;
            System.Data.DataTable theDT = new System.Data.DataTable();
            theDT = ((DataTable)ViewState["GridData"]);
            int r = theIndex;
            if (theDT.Rows.Count > 0)
            {
                ViewState["UpdateFlag"] = 1;
                ddlpurpose.SelectedValue = theDT.Rows[r]["PurposeId"].ToString();
                ViewState["SelectedPurposeId"] = Convert.ToInt32(theDT.Rows[r]["PurposeId"]);
                txtRegimen.Text = theDT.Rows[r]["Regimen"].ToString();
                ViewState["SelectedRegimen"] = theDT.Rows[r]["Regimen"].ToString();

                txtLastUsed.Text = String.Format("{0:dd-MMM-yyyy}", theDT.Rows[r]["RegLastUsed"]);
                ViewState["SelectedLastused"] = theDT.Rows[r]["RegLastUsed"].ToString();

                DataTable dtMaster = (DataTable)ViewState["MasterARVData"];
                string[] thetransStrRegimen = txtRegimen.Text.Split(new Char[] { '/' });
                DataView theARVTransDV = new DataView(dtMaster);
                if (txtRegimen.Text != "")
                {
                    ViewState["SelectedData"] = OldRegimenList(thetransStrRegimen, theARVTransDV);
                }

            }
        }
        private DataTable OldRegimenList(string[] str, DataView theDV)
        {
            DataTable theDT = CreateSelectedTable();
            foreach (string reg in str)
            {
                theDV.RowFilter = "Abbr Like '" + reg + "%'";
                if (theDV.Count > 0)
                {
                    DataRow theDR = theDT.NewRow();
                    theDR[0] = theDV[0][0];
                    theDR[1] = theDV[0][1];
                    theDR[2] = theDV[0][2];
                    theDR[3] = theDV[0][3];
                    theDR[4] = theDV[0][4];

                    DataRow theTempeDR;
                    theTempeDR = theDT.Rows.Find(theDV[0][0]);
                    if (theTempeDR == null)
                    {
                        theDT.Rows.Add(theDR);
                    }
                }
            }
            return theDT;

        }
        private DataTable CreateSelectedTable()
        {
            DataTable theDT = new DataTable();
            theDT.Columns.Add("DrugId", System.Type.GetType("System.Int32"));
            theDT.Columns.Add("DrugName", System.Type.GetType("System.String"));
            theDT.Columns.Add("Generic", System.Type.GetType("System.Int32"));
            theDT.Columns.Add("DrugTypeID", System.Type.GetType("System.Int32"));
            theDT.Columns.Add("Abbr", System.Type.GetType("System.String"));
            theDT.PrimaryKey = new DataColumn[] { theDT.Columns[0] };
            return theDT;
        }
        private Boolean ValidateLastUsed()
        {
            IQCareUtils theUtil = new IQCareUtils();
            if (txtLastUsed.Text == "")
            {
                IQCareMsgBox.ShowfromPage("RegLastUsed", this);
                txtLastUsed.Focus();
                return false;
            }
            if (Convert.ToDateTime(theUtil.MakeDate(txtLastUsed.Text)) > Convert.ToDateTime(theUtil.MakeDate(Application["AppCurrentDate"].ToString())))
            {
                IQCareMsgBox.ShowfromPage("CmpLastUsed", this);
                txtLastUsed.Focus();
                return false;
            }
            else if (ddlpurpose.SelectedValue == "0")
            {
                IQCareMsgBox.ShowfromPage("ChkPurpose", this);
                txtLastUsed.Focus();
                return false;
            }
            return true;
        }
        protected void FillDropDown()
        {            
            BindFunctions theBndMgr = new BindFunctions();
            DataSet theDS = new DataSet();
            theDS.ReadXml(MapPath("..\\XMLFiles\\ALLMasters.con"));
            IQCareUtils theUtils = new IQCareUtils();
            
            
            if (theDS.Tables["Mst_District"] != null)
            {
                DataView theDV = new DataView(theDS.Tables["Mst_District"]);
                theDV.RowFilter = "DeleteFlag=0 and Systemid=1";
                if (theDV.Table != null)
                {
                    DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    theBndMgr.BindCombo(dddistrict, theDT, "Name", "ID");
                }
            }
            if (theDS.Tables["Mst_LPTF"] != null)
            {
                DataView theDV = new DataView(theDS.Tables["Mst_LPTF"]);
                theDV.RowFilter = "DeleteFlag=0";
                if (theDV.Table != null)
                {
                    DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    theBndMgr.BindCombo(ddfacility, theDT, "Name", "ID");
                }
            }
            if (theDS.Tables["Mst_Decode"] != null)
            {
                DataView theDV = new DataView(theDS.Tables["Mst_Decode"]);
                theDV.RowFilter = "DeleteFlag=0 and CodeId=33";
                if (theDV.Table != null)
                {
                    DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    theBndMgr.BindCombo(ddlpurpose, theDT, "Name", "ID");
                    theBndMgr.BindCombo(ddlPApurposeFWP, theDT, "Name", "ID");
                }
                theDV = new DataView(theDS.Tables["Mst_Decode"]);
                theDV.RowFilter = "DeleteFlag=0 and SystemId=2 and CodeId=17";
                theDV.Sort = "SRNo";
                if (theDV.Table != null)
                {
                    DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    theBndMgr.BindCombo(ddltransferfrom, theDT, "Name", "ID");
                }
            }

            if (theDS.Tables["Mst_Decode"] != null)
            {
                DataView theDV = new DataView(theDS.Tables["Mst_Decode"]);
                theDV.RowFilter = "(DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1) and CodeId=22";
                theDV.Sort = "SRNo";
                if (theDV.Table != null)
                {
                    DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    theBndMgr.BindCombo(ddlWHOStage, theDT, "Name", "ID");
                    theBndMgr.BindCombo(ddlbaseWHO, theDT, "Name", "ID");
                }
                theDV = new DataView(theDS.Tables["Mst_Decode"]);
                theDV.RowFilter = "(DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1) and CodeId=8";
                theDV.Sort = "SRNo";
                if (theDV.Table != null)
                {
                    DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    theBndMgr.BindCombo(ddlGuardRelationship, theDT, "Name", "ID");
                }
                theDV = new DataView(theDS.Tables["Mst_Decode"]);
                theDV.RowFilter = "(DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1) and CodeName='ShortTermEffects'";
                theDV.Sort = "SRNo";
                if (theDV.Table != null)
                {
                    DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    theBndMgr.BindCheckedList(cblshorttermeffects, theDT, "Name", "ID");
                }
                theDV = new DataView(theDS.Tables["Mst_Decode"]);
                theDV.RowFilter = "(DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1) and CodeName='LongTermEffects'";
                theDV.Sort = "SRNo";
                if (theDV.Table != null)
                {
                    DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    theBndMgr.BindCheckedList(cbllongtermeffects, theDT, "Name", "ID");
                }
                
            }
            if (theDS.Tables["Mst_PMTCTDecode"] != null)
            {
                DataTable theDTCode;
                //Field VisitType
                DataView theDVDecode = new DataView(theDS.Tables["Mst_PMTCTDecode"]);
                theDVDecode.RowFilter = "CodeName='FieldVisitType' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    theDTCode = theDTCode.Select("Name <> 'ANC PMTCT'").CopyToDataTable();
                    theBndMgr.BindCombo(ddlvisittype, theDTCode, "Name", "Id");

                }
                //Immunization Period
                theDVDecode = new DataView(theDS.Tables["mst_pmtctdecode"]);
                theDVDecode.RowFilter = "CodeName='Immunisationperiod' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    theBndMgr.BindCombo(ddlImmunisationPeriod, theDTCode, "Name", "Id");

                }


                //Immunization given
                theDVDecode = new DataView(theDS.Tables["mst_pmtctdecode"]);
                theDVDecode.RowFilter = "CodeName='Immunisationgiven' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    theBndMgr.BindCombo(ddImmunisationgiven, theDTCode, "Name", "Id");

                }
            }
            
            if (theDS.Tables["Mst_Countries"] != null)
            {
                //Field VisitType
                DataView theDVDecode = new DataView(theDS.Tables["Mst_Countries"]);
                theDVDecode.Sort = "CountryID";
                if (theDVDecode.Table != null)
                {
                    DataTable theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    theBndMgr.BindCombo(ddlCountryFrom, theDTCode, "CountryName", "CountryID");

                }
            }

        }
        protected void Init_ARTHistory()
        {

            IPatientARTCare ARTHistoryMgr = (IPatientARTCare)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientARTCare, BusinessProcess.Clinical");
            DataSet theDS = ARTHistoryMgr.GetPatientARTCare(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["AppLocationId"]));
            ViewState["MasterARVData"] = theDS.Tables[1];

        }
        private string FillRegimen(DataTable theDT)
        {
            string theRegimen = "";
            DataView theDV = new DataView();
            if (theDT.Rows.Count != 0)
            {
                for (int i = 0; i < theDT.Rows.Count; i++)
                {
                    DataTable theFilDT = (DataTable)ViewState["MasterARVData"];
                    if (theFilDT.Rows.Count > 0)
                    {
                        theDV = new DataView(theFilDT);
                        theDV.RowFilter = "DrugId = " + theDT.Rows[i]["DrugId"];
                        if (theDV.Count > 0)
                        {
                            for (int j = 0; j < theDV.Count; j++)
                            {
                                if (theRegimen == "")
                                {
                                    theRegimen = theDV[j]["Abbr"].ToString();
                                }
                                else
                                {
                                    theRegimen = theRegimen + "/" + theDV[j]["Abbr"].ToString();
                                }
                            }
                        }
                    }
                    theRegimen = theRegimen.Trim();

                }
            }
            return theRegimen;
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ClinicalForms/frmPatient_Home.aspx");
        }
        private Boolean fieldValidation()
        {
            IQCareUtils iQCareUtils = new IQCareUtils();
            string validateMessage = "Following values are required:</br>";
            bool validationCheck = true;
            if (txtVisitDate.Text.Trim() == "")
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -Visit Date";
                validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "</br>";
                txtVisitDate.Focus();
                validationCheck = false;
            }
            if (!validationCheck)
            {
                MsgBuilder totalMsgBuilder = new MsgBuilder();
                totalMsgBuilder.DataElements["MessageText"] = validateMessage;
                IQCareMsgBox.ShowfromPage("#C1", totalMsgBuilder, this);
            }
            return validationCheck;
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (fieldValidation())
            {
                Hashtable theHT = HT(0);
                DataTable theDT = new DataTable();
                if ((DataTable)ViewState["GridData"] == null)
                {
                    theDT.Columns.Add("ptn_pk", typeof(Int32));
                    theDT.Columns.Add("VisitId", typeof(Int32));
                    theDT.Columns.Add("PurposeId", typeof(Int32));
                    theDT.Columns.Add("Purpose", typeof(string));
                    theDT.Columns.Add("Regimen", typeof(string));
                    theDT.Columns.Add("RegLastUsed", typeof(string));
                }
                else { theDT = (DataTable)ViewState["GridData"]; }
                IPriorArtHivCare ARTHistoryMgr = (IPriorArtHivCare)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPriorArtHivCare, BusinessProcess.Clinical");
                DataTable DT = ARTHistoryMgr.Save_Update_GreenCardForm(theHT, theDT);
                if (Convert.ToInt32(DT.Rows[0]["Visit_Id"]) > 0)
                {
                    SaveCancel();
                }
            }
        }
        private void SaveCancel()
        {
            string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
            script += "var ans;\n";
            script += "ans=window.confirm('Green Card Form saved successfully. Do you want to close?');\n";
            script += "if (ans==true)\n";
            script += "{\n";
            script += "window.location.href='../ClinicalForms/frmPatient_Home.aspx';\n";
            script += "}\n";
            script += "else \n";
            script += "{\n";
            script += "window.location.href='../ClinicalForms/frmPatient_History.aspx';\n";
            script += "}\n";
            script += "</script>\n";
            RegisterStartupScript("confirm", script);

        }
        protected Hashtable HT(int qltyFlag)
        {

            Hashtable theHT = new Hashtable();
            try
            {
                theHT.Add("PatientId", Session["PatientId"]);
                theHT.Add("VisitId", Session["PatientVisitId"]);
                theHT.Add("LocationId", Session["AppLocationId"]);
                theHT.Add("UserID", Session["AppUserId"].ToString());
                //todo
                theHT.Add("visitdate", txtVisitDate.Text);
                theHT.Add("VisitTypeId", ddlvisittype.SelectedValue);
                theHT.Add("TransferInFrom", ddltransferfrom.SelectedValue);
                theHT.Add("TransferInDate", txtTransferInDate.Text);
                theHT.Add("TransferInDistrictId", dddistrict.SelectedValue);
                theHT.Add("TransferInFacilityId", ddfacility.SelectedValue);
                theHT.Add("TransferMFLCode", txtMFLCode.Text);
                theHT.Add("TransferInCountryId", ddlCountryFrom.SelectedValue);
                theHT.Add("ARTStartDate", txtDateARTStarted.Text);
                theHT.Add("BaseWHOStage", ddlbaseWHO.SelectedValue);
                theHT.Add("BaseCD4", String.IsNullOrWhiteSpace(Convert.ToString(txtbaseCD4.Text)) ? "0" : Convert.ToString(txtbaseCD4.Text));
                theHT.Add("BaseHepB", rdoHepByes.Checked ? 1 : rdoHepBNo.Checked ? 0 : 9);
                theHT.Add("BasePregnancy", rdobasePrgencyYes.Checked ? 1 : rdobasePrgencyNo.Checked ? 0 : 9);
                theHT.Add("BaseBreastFeeding", rdobasebrestFeedYes.Checked ? 1 : rdobasebrestFeedNo.Checked ? 0 : 9);
                theHT.Add("BaseTBHIV", rdobaseTBYes.Checked ? 1 : rdobaseTBNo.Checked ? 0 : 9);
                theHT.Add("BaseDiscordant", rdoDiscordantYes.Checked ? 1 : rdoDiscordantNo.Checked ? 0 : 9);
                theHT.Add("BaseWeight", txtbaseweight.Text);
                theHT.Add("BaseHeight", txtbaseheight.Text);
                theHT.Add("BaseMUAC", txtbaseMUAC.Text);
                theHT.Add("ARTRegimen", txtFirstARTRegimen.Text);
                theHT.Add("BaseViralLoad", String.IsNullOrWhiteSpace(Convert.ToString(txtBaseViral.Text)) ? "0" : Convert.ToString(txtBaseViral.Text));
                theHT.Add("BaseViralLoadDate", txtbaseviraldate.Text);
                theHT.Add("TransferOutDate", txttransferdate.Text);
                theHT.Add("DeathDate", txtdeathdate.Text);
                theHT.Add("PartnerDisclosure", rdodiclosureyes.Checked ? 1 : rdodiclosureno.Checked ? 0 : 9);
                theHT.Add("PartnerDisclosureDate", txtsexDisDate.Text);
                theHT.Add("AdolPartialDisclosureDate", partDisDate.Text);
                theHT.Add("AdolFullDisclosureDate", txtFullDisDate.Text);
                theHT.Add("INHStartDate", txtINHStartDate.Text);
                theHT.Add("INHCompletion", Inhcompyes.Checked ? 1 : InhcompNo.Checked ? 0 : 9);
                theHT.Add("INHCompletionDate", txtINHCompDate.Text);
                theHT.Add("Measles", txtMeasles.Text);
                theHT.Add("IPV", txtIPV.Text);
                theHT.Add("Penta", txtPenta.Text);
                theHT.Add("PCV", txtPCV.Text);
                theHT.Add("BCG", txtDiphtheria.Text);
                theHT.Add("HBV", txtHBV.Text);
                theHT.Add("Flu", txtFlu.Text);
                theHT.Add("HepatitisB", txtHepatitis.Text);
                theHT.Add("Tetanus", txtTetanus.Text);
                theHT.Add("Pertussis", txtPertussis.Text);
                theHT.Add("Haemophilus", txtHaemophilus.Text);


            }
            catch (Exception err)
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.ShowfromPage("#C1", theMsg, this);
            }
            return theHT;

        }
        protected void BindrcbmedicalCondition()
        {
            DataTable dt = GetDataTable("Mst_Code", "PresentingComplaints");
            dt.PrimaryKey = new DataColumn[] { dt.Columns["ID"] };
            if (Session["PCValue"] != null)
            {
                string[] pcRow = Session["PCValue"].ToString().Split('#');
                foreach (string val in pcRow)
                {
                    string[] pcCellvalue = val.Split(',');
                    if (dt.Rows.Find(pcCellvalue[0]) != null)
                    {
                        DataRow dr = dt.Rows.Find(pcCellvalue[0]);
                        dr["ChkVal"] = "1";
                        dr["ChkValText"] = pcCellvalue[2].ToString();
                    }
                }

            }


            gvPresentingComplaints.DataSource = dt;
            gvPresentingComplaints.DataBind();

        }
        protected DataTable GetDataTable(string flag, string fieldName)
        {
            BIQAdultIE objAdultIEFields = new BIQAdultIE();
            objAdultIEFields.Flag = flag;
            objAdultIEFields.PtnPk = 0;//Convert.ToInt32(Session["PatientID"].ToString());
            objAdultIEFields.LocationId = 0;// Int32.Parse(Session["AppLocationId"].ToString());
            objAdultIEFields.FieldName = fieldName;

            IKNHAdultIE theExpressManager;
            theExpressManager = (IKNHAdultIE)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BAdultIE, BusinessProcess.Clinical");
            DataTable dt = theExpressManager.GetKnhAdultIEData(objAdultIEFields);
            return dt;

        }

        protected void gvPresentingComplaints_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl = (Label)e.Row.FindControl("lblPresenting");
                CheckBox chkPresenting = (CheckBox)e.Row.FindControl("ChkPresenting");
                TextBox txtPresenting = (TextBox)e.Row.FindControl("txtPresenting");
                ((CheckBox)e.Row.FindControl("ChkPresenting")).Attributes.Add("onclick", "togglePC('" + chkPresenting.ClientID + "')");
                txtPresenting.Attributes.Add("onkeyup", "chkDecimal('" + txtPresenting.ClientID + "');");
                //txtPresenting.Attributes.Add("onkeydown", "chkInteger('" + chkPresenting.ClientID + "')");
                //txtPresenting.Visible = true;
                if (chkPresenting.Text == "Other")
                {
                    chkPresenting.Attributes.Add("onchange", "ShowHideDiv('DivOther');");
                }
                if (chkPresenting.Text.ToUpper() == "NONE")
                {
                    chkPresenting.Attributes.Add("class", "checkbox");
                    chkPresenting.Style.Add("margin-left", "5%");
                    //chkPresenting.Attributes.Add("OnChange", "DisableCheckbox(" + chkPresenting.ClientID + ");");
                }
                else
                    chkPresenting.Attributes.Add("class", "selectAll");

            }
        }
        public void BindPresentingCompControl()
        {
            IinitialFollowupVisit InitialFollowupVisit = (IinitialFollowupVisit)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BInitialFollowupVisit, BusinessProcess.Clinical");
            DataSet ds = InitialFollowupVisit.GetInitialFollowupVisitData(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["AppLocationId"]));
            
            IQCareUtils iQCareUtils = new IQCareUtils();
            BindFunctions bindFunctions = new BindFunctions();            
            DataView DvBindControl;
            DataTable DtBindControl;
            //DtBindControl = ds.Tables[0];
            //if (DtBindControl.Rows.Count != 0)
            //{
            //    if (DtBindControl.Rows[0]["ART"] != DBNull.Value)
            //    {
            //        ArtStartinMonth = Convert.ToDateTime(DtBindControl.Rows[0]["ART"]).Year * 12 + Convert.ToDateTime(DtBindControl.Rows[0]["ART"]).Month;
            //    }
            //    else
            //    {
            //        ArtStartinMonth = 0;
            //    }
            //    if (DtBindControl.Rows[0]["REGIMEN"] != DBNull.Value)
            //    {
            //        ArtRegimeninMonth = Convert.ToDateTime(DtBindControl.Rows[0]["REGIMEN"]).Year * 12 + Convert.ToDateTime(DtBindControl.Rows[0]["REGIMEN"]).Month;

            //    }
            //    else
            //    {
            //        ArtRegimeninMonth = 0;
            //    }
            //}
            
            ////Visit Type
            //DvBindControl = new DataView(ds.Tables[1]);
            //if (DvBindControl.Table != null)
            //{
            //    DtBindControl = (DataTable)iQCareUtils.CreateTableFromDataView(DvBindControl);
            //    bindFunctions.BindCombo(ddlvisittype, DtBindControl, "Name", "ID");
            //    DvBindControl.Dispose();
            //    DtBindControl.Clear();
            //}
            //Cotrimoxazole Adhere 
            DvBindControl = new DataView(ds.Tables[11]);
            if (DvBindControl.Table != null)
            {
                DtBindControl = (DataTable)iQCareUtils.CreateTableFromDataView(DvBindControl);
                bindFunctions.BindCombo(ddlCotrimoxazoleAdhere, DtBindControl, "Name", "ID");
                DvBindControl.Dispose();
                DtBindControl.Clear();
            }

            //ARV Drugs Adhere 
            DvBindControl = new DataView(ds.Tables[12]);
            if (DvBindControl.Table != null)
            {
                DtBindControl = (DataTable)iQCareUtils.CreateTableFromDataView(DvBindControl);
                bindFunctions.BindCombo(ddlarvdrugadhere, DtBindControl, "Name", "ID");
                DvBindControl.Dispose();
                DtBindControl.Clear();
            }

            //Why Poor/Fair 
            DvBindControl = new DataView(ds.Tables[13]);
            if (DvBindControl.Table != null)
            {
                DtBindControl = (DataTable)iQCareUtils.CreateTableFromDataView(DvBindControl);
                bindFunctions.BindCombo(ddlwhypoorfair, DtBindControl, "Name", "ID");
                DvBindControl.Dispose();
                DtBindControl.Clear();
            }

            //Subsituations/Interruption 
            DvBindControl = new DataView(ds.Tables[14]);
            if (DvBindControl.Table != null)
            {
                DtBindControl = (DataTable)iQCareUtils.CreateTableFromDataView(DvBindControl);
                bindFunctions.BindCombo(ddlsubsituationInterruption, DtBindControl, "Name", "ID");
                DvBindControl.Dispose();
                DtBindControl.Clear();
            }
            //Therapy Change Codes
            DvBindControl = new DataView(ds.Tables[22]);
            DvBindControl.RowFilter = "CodeID='6'";
            DvBindControl.Sort = "ID ASC";
            if (DvBindControl.Table != null)
            {
                DtBindControl = (DataTable)iQCareUtils.CreateTableFromDataView(DvBindControl);
                bindFunctions.BindCombo(ddlArvTherapyChangeCode, DtBindControl, "Name", "ID");
                DvBindControl.Dispose();
                DtBindControl.Clear();
            }
            //Therapy Stop Codes
            DvBindControl = new DataView(ds.Tables[22]);
            DvBindControl.RowFilter = "CodeID='5'";
            DvBindControl.Sort = "ID ASC";
            if (DvBindControl.Table != null)
            {
                DtBindControl = (DataTable)iQCareUtils.CreateTableFromDataView(DvBindControl);
                bindFunctions.BindCombo(ddlArvTherapyStopCode, DtBindControl, "Name", "ID");
                DvBindControl.Dispose();
                DtBindControl.Clear();
            }

            //Family planning status 
            DvBindControl = new DataView(ds.Tables[3]);
            if (DvBindControl.Table != null)
            {
                DtBindControl = (DataTable)iQCareUtils.CreateTableFromDataView(DvBindControl);
                bindFunctions.BindCombo(ddlFamilyPanningStatus, DtBindControl, "Name", "ID");
                DvBindControl.Dispose();
                DtBindControl.Clear();
            }

            //Family planning method 
            DvBindControl = new DataView(ds.Tables[4]);
            if (DvBindControl.Table != null)
            {
                DtBindControl = (DataTable)iQCareUtils.CreateTableFromDataView(DvBindControl);
                bindFunctions.CreateBlueCheckedList(PnlFamilyPlanningMethod, DtBindControl, "", "");
                DvBindControl.Dispose();
                DtBindControl.Clear();
            }

            //Reason Not on Family Planning
            DvBindControl = new DataView(ds.Tables[5]);
            if (DvBindControl.Table != null)
            {
                DtBindControl = (DataTable)iQCareUtils.CreateTableFromDataView(DvBindControl);
                bindFunctions.BindCombo(ddlnotfamilyplanning, DtBindControl, "Name", "ID");
                DvBindControl.Dispose();
                DtBindControl.Clear();
            }

            

            ////Nutritional Support 
            //DvBindControl = new DataView(ds.Tables[16]);
            //if (DvBindControl.Table != null)
            //{
            //    DtBindControl = (DataTable)iQCareUtils.CreateTableFromDataView(DvBindControl);
            //    bindFunctions.BindCombo(ddlNutritionalSupport, DtBindControl, "Name", "ID");
            //    DvBindControl.Dispose();
            //    DtBindControl.Clear();


            //}

        }
        protected void btnAddImmunization_Click(object sender, EventArgs e)
        {
            int VisitId = Convert.ToInt32(Session["PatientVisitId"]) > 0 ? Convert.ToInt32(Session["PatientVisitId"]) : 0;
            if (ddlImmunisationPeriod.SelectedItem.Text == "Select" || ddImmunisationgiven.SelectedItem.Text == "Select" || txtDateImmunised.Text == "")
            {
                IQCareMsgBox.ShowfromPage("NoRecordSelected", this);
                return;
            }

            if (((DataTable)ViewState["GridImmunizationData"]).Rows.Count == 0)
            {
                CreateGridColoumn();

                DataRow theDR = theDTGrid.NewRow();
                theDR["ptn_pk"] = Session["PatientId"];
                theDR["Visit_pk"] = VisitId;
                theDR["Section"] = "Immunization";
                theDR["TypeofTestId"] = ddlImmunisationPeriod.SelectedValue;
                theDR["TypeofTest"] = ddlImmunisationPeriod.SelectedItem.Text;
                theDR["ResultId"] = ddImmunisationgiven.SelectedValue;
                theDR["Result"] = ddImmunisationgiven.SelectedItem.Text;
                theDR["Date"] = "" + txtDateImmunised.Text + "";
                theDR["Comments"] = string.Empty;
                theDTGrid.Rows.Add(theDR);
                GrdImmunization.Columns.Clear();
                BindGrid(GrdImmunization, "Immunization");
                RefreshGrid("Immunization");
                GrdImmunization.DataSource = theDTGrid;
                GrdImmunization.DataBind();
                ViewState["GridImmunizationData"] = theDTGrid;
            }
            else
            {
                theDTGrid = (DataTable)ViewState["GridImmunizationData"];
                DataRow theDR = theDTGrid.NewRow();
                theDR["ptn_pk"] = Session["PatientId"];
                theDR["Visit_pk"] = VisitId;
                theDR["Section"] = "Immunization";
                theDR["TypeofTestId"] = ddlImmunisationPeriod.SelectedValue;
                theDR["TypeofTest"] = ddlImmunisationPeriod.SelectedItem.Text;
                theDR["ResultId"] = ddImmunisationgiven.SelectedValue;
                theDR["Result"] = ddImmunisationgiven.SelectedItem.Text;
                theDR["Date"] = "" + txtDateImmunised.Text + "";
                theDR["Comments"] = string.Empty;
                theDTGrid.Rows.Add(theDR);
                GrdImmunization.Columns.Clear();
                BindGrid(GrdImmunization, "Immunization");
                RefreshGrid("Immunization");
                GrdImmunization.DataSource = theDTGrid;
                GrdImmunization.DataBind();
                ViewState["GridImmunizationData"] = theDTGrid;
            }
        }
        protected void GrdImmunization_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            System.Data.DataTable theDT = new System.Data.DataTable();
            theDT = ((DataTable)ViewState["GridImmunizationData"]);
            int r = Convert.ToInt32(e.RowIndex.ToString());
            int Id = -1;
            try
            {
                if (theDT.Rows.Count > 0)
                {

                    if (theDT.Rows[r].HasErrors == false)
                    {
                        if ((theDT.Rows[r]["TypeofTestId"] != null) && (theDT.Rows[r]["TypeofTestId"] != DBNull.Value))
                        {
                            if (theDT.Rows[r]["TypeofTestId"].ToString() != "")
                            {
                                Id = Convert.ToInt32(theDT.Rows[r]["TypeofTestId"]);
                                theDT.Rows[r].Delete();
                                theDT.AcceptChanges();
                                ViewState["GridImmunizationData"] = theDT;
                                GrdImmunization.Columns.Clear();
                                BindGrid(GrdImmunization, "Immunization");
                                RefreshGrid("Immunization");
                                GrdImmunization.DataSource = (DataTable)ViewState["GridImmunizationData"];
                                GrdImmunization.DataBind();
                                IQCareMsgBox.ShowfromPage("DeleteSuccess", this);
                            }
                        }
                    }
                }
                else
                {
                    GrdImmunization.Visible = false;
                    RefreshGrid("Immunization");
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }
        private void BindGrid(System.Web.UI.WebControls.GridView gridView, string gridName)
        {
            BoundField theCol0 = new BoundField();
            theCol0.HeaderText = "VisitId";
            theCol0.DataField = "VisitId";
            theCol0.ItemStyle.CssClass = "textstyle";
            theCol0.Visible = false;
            gridView.Columns.Add(theCol0);

            BoundField theCol1 = new BoundField();
            theCol1.HeaderText = "Patientid";
            theCol1.DataField = "ptn_pk";
            theCol1.ItemStyle.CssClass = "textstyle";
            theCol1.Visible = false;
            gridView.Columns.Add(theCol1);

            BoundField theCol2 = new BoundField();
            theCol2.HeaderText = "TypeofTestId";
            theCol2.DataField = "TypeofTestId";
            theCol2.Visible = false;
            gridView.Columns.Add(theCol2);

            BoundField theCol3 = new BoundField();
            theCol3.HeaderText = "ResultId";
            theCol3.DataField = "ResultId";
            theCol3.Visible = false;
            gridView.Columns.Add(theCol3);

            BoundField theCol4 = new BoundField();
            if (gridName == "Immunization")
            {
                theCol4.HeaderText = "Immunization Period";
            }
            else if (gridName == "Milestone")
            {
                theCol4.HeaderText = "Duration";
            }
            else if (gridName == "TBAssessment")
            {
                theCol4.HeaderText = "Treatment";
            }
            else
            {
                theCol4.HeaderText = "Test Type";
            }
            theCol4.DataField = "TypeofTest";
            gridView.Columns.Add(theCol4);

            BoundField theCol5 = new BoundField();
            if (gridName == "Immunization")
            {
                theCol5.HeaderText = "Immunization Given";
            }
            else if (gridName == "Milestone")
            {
                theCol5.HeaderText = "Status";
            }
            else if (gridName == "TBAssessment")
            {
                theCol5.HeaderText = "Plan";
            }
            else
            {
                theCol5.HeaderText = "Result";
            }
            theCol5.DataField = "Result";
            theCol5.ReadOnly = true;
            gridView.Columns.Add(theCol5);

            BoundField theCol6 = new BoundField();
            theCol6.HeaderText = "Date";
            theCol6.DataField = "Date";
            theCol6.DataFormatString = "{0:dd-MMM-yyyy}";
            theCol6.ReadOnly = true;
            if (gridName == "Milestone")
            {
                theCol6.Visible = false;
            }
            else
            {
                theCol6.Visible = true;
            }
            gridView.Columns.Add(theCol6);

            BoundField theCol7 = new BoundField();
            theCol7.HeaderText = "Comments";
            theCol7.DataField = "Comments";
            theCol7.ReadOnly = true;
            if (gridName == "TBAssessment" || gridName == "Immunization")
            {
                theCol7.Visible = false;
            }
            else
            {
                theCol7.Visible = true;
            }
            gridView.Columns.Add(theCol7);

            CommandField theCol8 = new CommandField();
            theCol8.ButtonType = ButtonType.Link;
            theCol8.DeleteText = "<img src='../Images/del.gif' alt='Delete this' border='0' />";
            theCol8.ShowDeleteButton = true;
            gridView.Columns.Add(theCol8);

            BoundField theCol9 = new BoundField();
            theCol9.HeaderText = "Section";
            theCol9.DataField = "Section";
            theCol9.Visible = false;
            gridView.Columns.Add(theCol9);


        }
        private void RefreshGrid(string grdName)
        {            

            if (grdName == "Immunization")
            {
                ddlImmunisationPeriod.SelectedIndex = -1;
                ddImmunisationgiven.SelectedIndex = -1;
                txtDateImmunised.Text = "";
            }            
        }
        private void CreateGridColoumn()
        {
            theDTGrid = new DataTable();
            theDTGrid.Columns.Add("ptn_pk", typeof(Int32));
            theDTGrid.Columns.Add("Visit_pk", typeof(Int32));
            theDTGrid.Columns.Add("Section", typeof(string));
            theDTGrid.Columns.Add("TypeofTestId", typeof(Int32));
            theDTGrid.Columns.Add("TypeofTest", typeof(string));
            theDTGrid.Columns.Add("ResultId", typeof(Int32));
            theDTGrid.Columns.Add("Result", typeof(string));
            theDTGrid.Columns.Add("Comments", typeof(string));
            theDTGrid.Columns.Add("Date", typeof(string));
        }
        protected void btnAppointment_Click(object sender, EventArgs e)
        {
            IQCareUtils.Redirect("../Scheduler/frmScheduler_AppointmentNewHistory.aspx?name=Add&FormName=Appointment History New&opento=popup", "_blank", "toolbars=no,location=no,directories=no,dependent=yes,top=100,left=30,maximize=no,resize=no,width=1000,height=800,scrollbars=yes");
        }
        #region "Prior ART Followup"
        private void BindGridFWP()
        {
            BoundField theCol0 = new BoundField();
            theCol0.HeaderText = "VisitId";
            theCol0.DataField = "VisitId";
            theCol0.ItemStyle.CssClass = "textstyle";
            GrdPriorARTFWP.Columns.Add(theCol0);

            BoundField theCol1 = new BoundField();
            theCol1.HeaderText = "Patientid";
            theCol1.DataField = "ptn_pk";
            theCol1.ItemStyle.CssClass = "textstyle";
            GrdPriorARTFWP.Columns.Add(theCol1);

            BoundField theCol6 = new BoundField();
            theCol6.HeaderText = "PurposeId";
            theCol6.DataField = "PurposeId";
            GrdPriorARTFWP.Columns.Add(theCol6);

            BoundField theCol2 = new BoundField();
            theCol2.HeaderText = "Purpose";
            theCol2.DataField = "Purpose";
            theCol2.SortExpression = "Purpose";
            theCol2.ReadOnly = true;
            GrdPriorARTFWP.Columns.Add(theCol2);

            BoundField theCol3 = new BoundField();
            theCol3.HeaderText = "Regimen";
            theCol3.DataField = "Regimen";
            theCol3.SortExpression = "Regimen";
            theCol3.ReadOnly = true;
            GrdPriorARTFWP.Columns.Add(theCol3);

            BoundField theCol4 = new BoundField();
            theCol4.HeaderText = "Regimen Last Used";
            theCol4.DataField = "RegLastUsed";
            theCol4.DataFormatString = "{0:dd-MMM-yyyy}";
            theCol4.SortExpression = "RegLastUsed";
            theCol4.ReadOnly = true;
            GrdPriorARTFWP.Columns.Add(theCol4);

            CommandField theCol5 = new CommandField();
            theCol5.ButtonType = ButtonType.Link;
            theCol5.DeleteText = "<img src='../Images/del.gif' alt='Delete this' border='0' />";
            theCol5.ShowDeleteButton = true;
            GrdPriorARTFWP.Columns.Add(theCol5);

            GrdPriorARTFWP.Columns[0].Visible = false;
            GrdPriorARTFWP.Columns[1].Visible = false;
            GrdPriorARTFWP.Columns[2].Visible = false;
        }
        private void RefreshFWP()
        {
            ddlPApurposeFWP.SelectedIndex = -1;
            txtRegimenFWP.Text = "";
            txtdateLastusedFWP.Text = "";

        }
        protected void btnAddPriorARTFWP_Click(object sender, EventArgs e)
        {
            if (ValidateLastUsed() == false)
            {
                return;
            }
            int VisitId = Convert.ToInt32(Session["PatientVisitId"]) > 0 ? Convert.ToInt32(Session["PatientVisitId"]) : 0;
            DataTable theDT = new DataTable();
            if ((DataTable)ViewState["GridData"] == null)
            {
                theDT.Columns.Add("ptn_pk", typeof(Int32));
                theDT.Columns.Add("VisitId", typeof(Int32));
                theDT.Columns.Add("PurposeId", typeof(Int32));
                theDT.Columns.Add("Purpose", typeof(string));
                theDT.Columns.Add("Regimen", typeof(string));
                theDT.Columns.Add("RegLastUsed", typeof(string));
                DataRow theDR = theDT.NewRow();
                theDR["ptn_pk"] = Session["PatientId"];
                theDR["VisitId"] = VisitId;
                theDR["Purpose"] = ddlPApurposeFWP.SelectedItem.Text;
                theDR["Regimen"] = txtRegimenFWP.Text;
                theDR["RegLastUsed"] = "" + txtdateLastusedFWP.Text + "";
                theDR["PurposeId"] = ddlPApurposeFWP.SelectedValue;
                theDT.Rows.Add(theDR);
                GrdPriorARTFWP.Columns.Clear();
                BindGridFWP();
                RefreshFWP();
                GrdPriorARTFWP.DataSource = theDT;
                GrdPriorARTFWP.DataBind();
                ViewState["GridData"] = theDT;
            }
            else
            {

                theDT = (DataTable)ViewState["GridData"];
                if (Convert.ToInt32(ViewState["UpdateFlag"]) == 1)
                {
                    DataRow[] rows = theDT.Select("PurposeId=" + ViewState["SelectedPurposeId"] + " and Regimen='" + ViewState["SelectedRegimen"] + "' and RegLastUsed='" + ViewState["SelectedLastused"] + "'");
                    for (int i = 0; i < rows.Length; i++)
                    {
                        rows[i]["ptn_pk"] = Session["PatientId"];
                        rows[i]["VisitId"] = VisitId;
                        rows[i]["PurposeId"] = ddlPApurposeFWP.SelectedValue;
                        rows[i]["Purpose"] = ddlPApurposeFWP.SelectedItem.Text;
                        rows[i]["Regimen"] = txtRegimenFWP.Text;
                        rows[i]["RegLastUsed"] = "" + txtdateLastusedFWP.Text + "";
                        theDT.AcceptChanges();
                    }
                    GrdPriorARTFWP.Columns.Clear();
                    BindGridFWP();
                    RefreshFWP();
                    GrdPriorARTFWP.DataSource = theDT;
                    GrdPriorARTFWP.DataBind();
                    ViewState["GridData"] = theDT;
                    ViewState["UpdateFlag"] = "0";
                    DataTable theGVDT = (DataTable)GrdPriorARTFWP.DataSource;
                }
                else
                {
                    DataRow theDR = theDT.NewRow();
                    theDR["ptn_pk"] = Session["PatientId"];
                    theDR["VisitId"] = VisitId;
                    theDR["Purpose"] = ddlPApurposeFWP.SelectedItem.Text;
                    theDR["Regimen"] = txtRegimenFWP.Text;
                    theDR["RegLastUsed"] = "" + txtdateLastusedFWP.Text + "";
                    theDR["PurposeId"] = ddlPApurposeFWP.SelectedValue;
                    theDT.Rows.Add(theDR);
                    GrdPriorARTFWP.Columns.Clear();
                    BindGridFWP();
                    RefreshFWP();
                    GrdPriorARTFWP.DataSource = theDT;
                    GrdPriorARTFWP.DataBind();
                    ViewState["GridData"] = theDT;
                }
            }
        }
        protected void btnFWPRegimen_Click(object sender, EventArgs e)
        {            
            string theScript;
            Application.Add("MasterData", ViewState["MasterARVData"]);
            Session["DrugData"] = ViewState["MasterARVData"];
            Application.Add("SelectedDrug", (DataTable)ViewState["SelectedData"]);
            theScript = "<script language='javascript' id='DrgPopupFWP'>\n";
            theScript += "window.open('../Pharmacy/frmDrugSelector.aspx?DrugType=37&btnreg=btnRegimenGCFWP' ,'DrugSelection','toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=no,resize=no,width=700,height=350,scrollbars=yes');\n";
            theScript += "</script>\n";
            //Page.RegisterStartupScript("DrgPopupFWP", theScript);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "DrgPopupFWP", theScript, true);
        }
        protected void GrdPriorARTFWP_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    if (e.Row.Cells[0].Text.ToString() != "0")
                    {
                        e.Row.Cells[i].Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.BackColor='#666699';");
                        e.Row.Cells[i].Attributes.Add("onmouseout", "this.style.backgroundColor='';");
                        e.Row.Cells[i].Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(GrdPriorARTFWP, "Select$" + e.Row.RowIndex.ToString()));
                    }
                    if (e.Row.Cells[5].Text.ToString() == "01-Jan-1900")
                    {
                        e.Row.Cells[5].Text = "";
                    }

                }
            }
        }
        protected void GrdPriorARTFWP_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int thePage = GrdPriorARTFWP.PageIndex;
            int thePageSize = GrdPriorARTFWP.PageSize;
            GridViewRow theRow = GrdPriorARTFWP.Rows[e.NewSelectedIndex];
            int theIndex = thePageSize * thePage + theRow.RowIndex;
            System.Data.DataTable theDT = new System.Data.DataTable();
            theDT = ((DataTable)ViewState["GridData"]);
            int r = theIndex;
            if (theDT.Rows.Count > 0)
            {
                ViewState["UpdateFlag"] = 1;
                ddlPApurposeFWP.SelectedValue = theDT.Rows[r]["PurposeId"].ToString();
                ViewState["SelectedPurposeId"] = Convert.ToInt32(theDT.Rows[r]["PurposeId"]);
                txtRegimenFWP.Text = theDT.Rows[r]["Regimen"].ToString();
                ViewState["SelectedRegimen"] = theDT.Rows[r]["Regimen"].ToString();

                txtdateLastusedFWP.Text = String.Format("{0:dd-MMM-yyyy}", theDT.Rows[r]["RegLastUsed"]);
                ViewState["SelectedLastused"] = theDT.Rows[r]["RegLastUsed"].ToString();

                DataTable dtMaster = (DataTable)ViewState["MasterARVData"];
                string[] thetransStrRegimen = txtRegimenFWP.Text.Split(new Char[] { '/' });
                DataView theARVTransDV = new DataView(dtMaster);
                if (txtRegimenFWP.Text != "")
                {
                    ViewState["SelectedData"] = OldRegimenList(thetransStrRegimen, theARVTransDV);
                }

            }
        }        
        
        private Boolean ValidateFWPLastUsed()
        {
            IQCareUtils theUtil = new IQCareUtils();
            if (txtdateLastusedFWP.Text == "")
            {
                IQCareMsgBox.ShowfromPage("RegLastUsed", this);
                txtdateLastusedFWP.Focus();
                return false;
            }
            if (Convert.ToDateTime(theUtil.MakeDate(txtdateLastusedFWP.Text)) > Convert.ToDateTime(theUtil.MakeDate(Application["AppCurrentDate"].ToString())))
            {
                IQCareMsgBox.ShowfromPage("CmpLastUsed", this);
                txtdateLastusedFWP.Focus();
                return false;
            }
            else if (ddlPApurposeFWP.SelectedValue == "0")
            {
                IQCareMsgBox.ShowfromPage("ChkPurpose", this);
                txtdateLastusedFWP.Focus();
                return false;
            }
            return true;
        }
        #endregion        
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["name"] == "Delete")
            {
                int delete = theUtils.DeleteForm("Adult Initial Evaluation", Convert.ToInt32(Session["PatientVisitId"]), Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["AppUserId"]));

                if (delete == 0)
                {
                    IQCareMsgBox.ShowfromPage("RemoveFormError", this);
                    return;
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "deleteSuccessful", "alert('Form deleted successfully.');", true);
                    string theUrl;
                    theUrl = string.Format("frmPatient_Home.aspx");
                    Response.Redirect(theUrl);

                }
            }
        }

        public void Save(string tabname)
        {
            if (fieldValidation(tabname) == false)
            {
                //ErrorLoad();
                return;
            }
            Hashtable theHT = new Hashtable();
            DataTable dtmuiltselect = CreateTempTable();

            DataSet DsReturns = new DataSet();
            Iprior = (IPriorArtHivCare)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPriorArtHivCare, BusinessProcess.Clinical");
            if (tabname == "Triage")
            {
                //tabname = "Triage";
                theHT = HT(0, tabname);
                DsReturns = Iprior.SaveUpdateGreenCardFollowupData_TriageTab(theHT, 0, 0, Convert.ToInt32(Session["AppUserId"]));

                //tabindex = 1;

            }
            else if (tabname == "PresCompl")
            {
                dtmuiltselect = PresentingComplaints(dtmuiltselect, "PresentingComplaints");
                theHT = HT(0, tabname);
                DsReturns = Iprior.SaveUpdateGreenCardFollowupData_TriageTab(theHT, 0, 0, Convert.ToInt32(Session["AppUserId"]));
                
            }

            else if (tabname == "AddHx")
            {                
                DataTable theDT = new DataTable();
                if ((DataTable)ViewState["GridData"] == null)
                {
                    theDT.Columns.Add("ptn_pk", typeof(Int32));
                    theDT.Columns.Add("VisitId", typeof(Int32));
                    theDT.Columns.Add("PurposeId", typeof(Int32));
                    theDT.Columns.Add("Purpose", typeof(string));
                    theDT.Columns.Add("Regimen", typeof(string));
                    theDT.Columns.Add("RegLastUsed", typeof(string));
                }
                else { theDT = (DataTable)ViewState["GridData"]; }
                
                DsReturns = Iprior.SaveUpdateGreenCardFollowupData_AddHxTab(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["PatientVisitId"]), Convert.ToInt32(Session["AppLocationId"]), txtVisitDate.Text.ToString(), theDT, (DataTable)ViewState["GridImmunizationData"], 0, Convert.ToInt32(Session["AppUserId"]), Convert.ToInt32(Session["AppUserId"]));
                //tabindex = 4;

            }
            else if (tabname == "SystRew")
            {                
                dtmuiltselect = GetCheckBoxListValues(UCPhyExam.cblGeneralConditions, dtmuiltselect, "GeneralConditions");
                //Cardiovascular conditions
                dtmuiltselect = GetCheckBoxListValues(UCPhyExam.cblCardiovascularConditions, dtmuiltselect, "CardiovascularConditions");
                //CNS 
                dtmuiltselect = GetCheckBoxListValues(UCPhyExam.cblCNSConditions, dtmuiltselect, "CNSConditions");
                //Oral cavity
                dtmuiltselect = GetCheckBoxListValues(UCPhyExam.cblOralCavityConditions, dtmuiltselect, "OralCavityConditions");
                //Chest Lungs
                dtmuiltselect = GetCheckBoxListValues(UCPhyExam.cblChestLungsConditions, dtmuiltselect, "ChestLungsConditions");
                //Genitourinary 
                dtmuiltselect = GetCheckBoxListValues(UCPhyExam.cblGenitalUrinaryConditions, dtmuiltselect, "GenitalUrinaryConditions");
                //Skin 
                dtmuiltselect = GetCheckBoxListValues(UCPhyExam.cblSkinConditions, dtmuiltselect, "SkinConditions");
                //Abdomen conditions
                dtmuiltselect = GetCheckBoxListValues(UCPhyExam.cblAbdomenConditions, dtmuiltselect, "AbdomenConditions");
                //WHO Stage I
                //GridView gdviewwho1 = (GridView)UcWhostaging.FindControl("gvWHO1");
                dtmuiltselect = InsertMultiSelectList(UCWHO.gvWHO1, dtmuiltselect, "CurrentWHOStageIConditions");
                //// WHO Stage II
                dtmuiltselect = InsertMultiSelectList(UCWHO.gvWHO2, dtmuiltselect, "CurrentWHOStageIIConditions");
                //// WHO Stage III
                dtmuiltselect = InsertMultiSelectList(UCWHO.gvWHO3, dtmuiltselect, "CurrentWHOStageIIIConditions");
                //// WHO Stage IV
                dtmuiltselect = InsertMultiSelectList(UCWHO.gvWHO4, dtmuiltselect, "CurrentWHOStageIVConditions");
                IKNHStaticForms TBScreeningManager = (IKNHStaticForms)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHStaticForms, BusinessProcess.Clinical");
                TBScreeningManager.SaveUpdateTBScreening(TBHT(), getCheckBoxListItemValues(UCTBScreen.cblTBAssessmentICF), getCheckBoxListItemValues(UCTBScreen.cblStopTBReason), getCheckBoxListItemValues(UCTBScreen.cblReviewChecklist), getCheckBoxListItemValues(UCTBScreen.cblSignsOfHepatitis));
                theHT = HT(0, tabname);
                DsReturns = Iprior.SaveUpdateKNHPeadraticFollowupData_SysReviewTab(theHT, dtmuiltselect, 0, 0, Convert.ToInt32(Session["AppUserId"]));
                //tabindex = 5;
            }
            else if (tabname == "Mgmt")
            {
                dtmuiltselect = GetCheckBoxListValues(cblshorttermeffects, dtmuiltselect, "ShortTermEffects");
                dtmuiltselect = GetCheckBoxListValues(cbllongtermeffects, dtmuiltselect, "LongTermEffects");
                theHT = HT(0, tabname);
                DsReturns = Iprior.SaveUpdateGreenCardFollowupData_MgtTab(theHT, dtmuiltselect, 0, 0, Convert.ToInt32(Session["AppUserId"]));
            }

            Session["Redirect"] = "0";
            if (Convert.ToInt32(DsReturns.Tables[0].Rows[0]["Visit_Id"]) > 0)
            {
                Session["PatientVisitId"] = Convert.ToInt32(DsReturns.Tables[0].Rows[0]["Visit_Id"]);
                SaveCancel(tabname);
                ////BindExistingData();
                //checkIfPreviuosTabSaved();
                //tabControl.ActiveTabIndex = tabindex;
            }
        }
        private Boolean fieldValidation(string TabName)
        {
            IQCareUtils theUtil = new IQCareUtils();
            MsgBuilder totalMsgBuilder = new MsgBuilder();
            ////if ((Convert.ToInt32(Session["PatientVisitId"]) == 0) || (Session["PatientVisitId"] == null))
            ////{
            ////    KNHStatic = (IKNHStaticForms)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHStaticForms, BusinessProcess.Clinical");
            ////    DataSet dsValidate = KNHStatic.GetExistKNHStaticFormbydate(Convert.ToInt32(Session["PatientId"]), txtvisitDate.Value.ToString(), Convert.ToInt32(Session["AppLocationId"]), 24);

            ////    if (dsValidate.Tables[0].Rows.Count > 0)
            ////    {
            ////        totalMsgBuilder.DataElements["FormName"] = "Paediatric Follow up Form";
            ////        IQCareMsgBox.ShowfromPage("KNHFormRecordExist", totalMsgBuilder, this);
            ////        return false; ;

            ////    }
            ////}
            if (txtVisitDate.Text.Trim() == "")
            {

                totalMsgBuilder.DataElements["MessageText"] = "Enter Encounter Date";
                //lblError.Text = "Enter Encounter Date";
                //divError.Visible = true;
                IQCareMsgBox.ShowfromPage("#C1", totalMsgBuilder, this);
                return false;

            }

            int count = 0;
            if (TabName == "Triage" || TabName == "btnSubmitTriage")
            {                
                if (this.idVitalSign.txtHeight.Text == "")
                {

                    totalMsgBuilder.DataElements["MessageText"] = "Enter Height";
                    IQCareMsgBox.ShowfromPage("#C1", totalMsgBuilder, this);
                    idVitalSign.lblHeight.ForeColor = Color.Red;                    
                    return false;
                }
                else
                {
                    idVitalSign.lblHeight.ForeColor = Color.FromArgb(0, 0, 142);
                    //lblVitalSigns.ForeColor = Color.FromArgb(0, 0, 142);
                }
                if (this.idVitalSign.txtWeight.Text == "")
                {

                    totalMsgBuilder.DataElements["MessageText"] = "Enter Weight";
                    IQCareMsgBox.ShowfromPage("#C1", totalMsgBuilder, this);
                    idVitalSign.lblWeight.ForeColor = Color.Red;
                    //lblVitalSigns.ForeColor = Color.Red;
                    return false;
                }
                else
                {
                    idVitalSign.lblWeight.ForeColor = Color.FromArgb(0, 0, 142);
                    //lblVitalSigns.ForeColor = Color.FromArgb(0, 0, 142);
                }
                if (this.idVitalSign.txtBPSystolic.Text == "")
                {

                    totalMsgBuilder.DataElements["MessageText"] = "Enter BP Systolic";
                    IQCareMsgBox.ShowfromPage("#C1", totalMsgBuilder, this);
                    idVitalSign.lblBP.ForeColor = Color.Red;
                    //lblVitalSigns.ForeColor = Color.Red;
                    return false;
                }
                else
                {
                    idVitalSign.lblBP.ForeColor = Color.FromArgb(0, 0, 142);
                    //lblVitalSigns.ForeColor = Color.FromArgb(0, 0, 142);
                }
                if (this.idVitalSign.txtBPDiastolic.Text == "")
                {

                    totalMsgBuilder.DataElements["MessageText"] = "Enter BP Diastolic";
                    IQCareMsgBox.ShowfromPage("#C1", totalMsgBuilder, this);
                    idVitalSign.lblBP.ForeColor = Color.Red;
                    //lblVitalSigns.ForeColor = Color.Red;
                    return false;
                }
                else
                {
                    idVitalSign.lblBP.ForeColor = Color.FromArgb(0, 0, 142);
                    //lblVitalSigns.ForeColor = Color.FromArgb(0, 0, 142);
                }

            }
            ////else if (TabName == "Clinical History")
            ////{
            ////    count = 0;
            ////    GridView gvPresentingComplaints = (GridView)UCPresComp.FindControl("gvPresentingComplaints");
            ////    foreach (GridViewRow row in gvPresentingComplaints.Rows)
            ////    {
            ////        CheckBox chk = (CheckBox)row.FindControl("ChkPresenting");
            ////        if (chk.Checked == true)
            ////        {
            ////            count++;
            ////        }
            ////    }
            ////    if (count == 0)
            ////    {
            ////        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValCC", "alert('Select Presenting Complaints - Clinical History')", true);
            ////        totalMsgBuilder.DataElements["MessageText"] = "Select Presenting Complaints - Clinical History";
            ////        IQCareMsgBox.ShowfromPage("#C1", totalMsgBuilder, this);
            ////        UCPresComp.gvPresentingComplaints.HeaderStyle.ForeColor = Color.Red;
            ////        lblPresComp.ForeColor = Color.Red;
            ////        return false;
            ////    }
            ////    else
            ////    {
            ////        UCPresComp.gvPresentingComplaints.HeaderStyle.ForeColor = Color.FromArgb(0, 0, 142);
            ////        lblPresComp.ForeColor = Color.FromArgb(0, 0, 142);
            ////    }
            ////    //for (int i = 0; i < this.UcPhysExam.cblGeneralConditions.Items.Count; i++)
            ////    //{
            ////    //    if (this.UCPresComp.gvPresentingComplaints.Items[i].Selected == true)
            ////    //    {
            ////    //        count++;
            ////    //    }
            ////    //}
            ////    //if (count == 0)
            ////    //{
            ////    //    totalMsgBuilder.DataElements["MessageText"] = "Select General Condition";
            ////    //    IQCareMsgBox.ShowfromPage("#C1", totalMsgBuilder, this);
            ////    //    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValGC", "alert('Select General Condition')", true);
            ////    //    return false;
            ////    //}
            ////}

            ////else if (TabName == "Examination" || tabControl.ActiveTabIndex == 3)
            ////{
            ////    for (int i = 0; i < chkLongTermMedication.Items.Count; i++)
            ////    {
            ////        if (chkLongTermMedication.Items[i].Selected == true)
            ////        {
            ////            count++;
            ////        }
            ////    }
            ////    if (count == 0)
            ////    {
            ////        totalMsgBuilder.DataElements["MessageText"] = "Select Long Term Medication";
            ////        IQCareMsgBox.ShowfromPage("#C1", totalMsgBuilder, this);
            ////        lblchlongtermmed.ForeColor = Color.Red;
            ////        lblMedicalConditions.ForeColor = Color.Red;
            ////        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValLTM", "alert('Select Long Term Medication')", true);
            ////        return false;
            ////    }
            ////    else
            ////    {
            ////        lblchlongtermmed.ForeColor = Color.FromArgb(0, 0, 142);
            ////        lblMedicalConditions.ForeColor = Color.FromArgb(0, 0, 142);
            ////    }
            ////    count = 0;
            ////    for (int i = 0; i < this.UcPhysExam.cblGeneralConditions.Items.Count; i++)
            ////    {
            ////        if (this.UcPhysExam.cblGeneralConditions.Items[i].Selected == true)
            ////        {
            ////            count++;
            ////        }
            ////    }
            ////    if (count == 0)
            ////    {
            ////        totalMsgBuilder.DataElements["MessageText"] = "Select General Condition";
            ////        IQCareMsgBox.ShowfromPage("#C1", totalMsgBuilder, this);
            ////        UcPhysExam.lblGeneral.ForeColor = Color.Red;
            ////        lblMedicalConditions.ForeColor = Color.Red;
            ////        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValGC", "alert('Select General Condition')", true);
            ////        return false;
            ////    }
            ////    else
            ////    {
            ////        UcPhysExam.lblGeneral.ForeColor = Color.FromArgb(0, 0, 142);
            ////        lblMedicalConditions.ForeColor = Color.FromArgb(0, 0, 142);
            ////    }
            ////    count = 0;
            ////    for (int i = 0; i < this.UcPhysExam.cblCardiovascularConditions.Items.Count; i++)
            ////    {
            ////        if (this.UcPhysExam.cblCardiovascularConditions.Items[i].Selected == true)
            ////        {
            ////            count++;
            ////        }
            ////    }
            ////    if (count == 0)
            ////    {
            ////        totalMsgBuilder.DataElements["MessageText"] = "Select Cardiovascular Conditions";
            ////        IQCareMsgBox.ShowfromPage("#C1", totalMsgBuilder, this);
            ////        UcPhysExam.lblCardiovarscular.ForeColor = Color.Red;
            ////        lblMedicalConditions.ForeColor = Color.Red;
            ////        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValCC", "alert('Select Cardiovascular Conditions')", true);
            ////        return false;
            ////    }
            ////    else
            ////    {
            ////        UcPhysExam.lblCardiovarscular.ForeColor = Color.FromArgb(0, 0, 142);
            ////        lblMedicalConditions.ForeColor = Color.FromArgb(0, 0, 142);
            ////    }
            ////    count = 0;
            ////    for (int i = 0; i < this.UcPhysExam.cblOralCavityConditions.Items.Count; i++)
            ////    {
            ////        if (this.UcPhysExam.cblOralCavityConditions.Items[i].Selected == true)
            ////        {
            ////            count++;
            ////        }
            ////    }
            ////    if (count == 0)
            ////    {
            ////        totalMsgBuilder.DataElements["MessageText"] = "Select Oral Cavity Conditions";
            ////        IQCareMsgBox.ShowfromPage("#C1", totalMsgBuilder, this);
            ////        UcPhysExam.lblOralCavity.ForeColor = Color.Red;
            ////        lblMedicalConditions.ForeColor = Color.Red;
            ////        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValOC", "alert('Select Oral Cavity Conditions')", true);
            ////        return false;
            ////    }
            ////    else
            ////    {
            ////        UcPhysExam.lblOralCavity.ForeColor = Color.FromArgb(0, 0, 142);
            ////        lblMedicalConditions.ForeColor = Color.FromArgb(0, 0, 142);
            ////    }
            ////    count = 0;
            ////    for (int i = 0; i < this.UcPhysExam.cblGenitalUrinaryConditions.Items.Count; i++)
            ////    {
            ////        if (this.UcPhysExam.cblGenitalUrinaryConditions.Items[i].Selected == true)
            ////        {
            ////            count++;
            ////        }
            ////    }
            ////    if (count == 0)
            ////    {
            ////        totalMsgBuilder.DataElements["MessageText"] = "Select GenitalUrinary Conditions";
            ////        IQCareMsgBox.ShowfromPage("#C1", totalMsgBuilder, this);
            ////        UcPhysExam.lblGenitourinary.ForeColor = Color.Red;
            ////        lblMedicalConditions.ForeColor = Color.Red;
            ////        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValGU", "alert('Select GenitalUrinary Conditions')", true);
            ////        return false;
            ////    }
            ////    else
            ////    {
            ////        UcPhysExam.lblGenitourinary.ForeColor = Color.FromArgb(0, 0, 142);
            ////        lblMedicalConditions.ForeColor = Color.FromArgb(0, 0, 142);
            ////    }

            ////    count = 0;
            ////    for (int i = 0; i < this.UcPhysExam.cblCNSConditions.Items.Count; i++)
            ////    {
            ////        if (this.UcPhysExam.cblCNSConditions.Items[i].Selected == true)
            ////        {
            ////            count++;
            ////        }
            ////    }
            ////    if (count == 0)
            ////    {
            ////        totalMsgBuilder.DataElements["MessageText"] = "Select CNS Conditions";
            ////        IQCareMsgBox.ShowfromPage("#C1", totalMsgBuilder, this);
            ////        UcPhysExam.lblCNS.ForeColor = Color.Red;
            ////        lblMedicalConditions.ForeColor = Color.Red;
            ////        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValCNS", "alert('Select CNS Conditions')", true);
            ////        return false;
            ////    }
            ////    else
            ////    {
            ////        UcPhysExam.lblCNS.ForeColor = Color.FromArgb(0, 0, 142);
            ////        lblMedicalConditions.ForeColor = Color.FromArgb(0, 0, 142);
            ////    }

            ////    count = 0;
            ////    for (int i = 0; i < this.UcPhysExam.cblChestLungsConditions.Items.Count; i++)
            ////    {
            ////        if (this.UcPhysExam.cblChestLungsConditions.Items[i].Selected == true)
            ////        {
            ////            count++;
            ////        }
            ////    }
            ////    if (count == 0)
            ////    {
            ////        totalMsgBuilder.DataElements["MessageText"] = "Select ChestLung Conditions";
            ////        IQCareMsgBox.ShowfromPage("#C1", totalMsgBuilder, this);
            ////        UcPhysExam.lblChest.ForeColor = Color.Red;
            ////        lblMedicalConditions.ForeColor = Color.Red;
            ////        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValChest", "alert('Select ChestLung Conditions')", true);
            ////        return false;
            ////    }
            ////    else
            ////    {
            ////        UcPhysExam.lblChest.ForeColor = Color.FromArgb(0, 0, 142);
            ////        lblMedicalConditions.ForeColor = Color.FromArgb(0, 0, 142);
            ////    }

            ////    count = 0;
            ////    for (int i = 0; i < this.UcPhysExam.cblSkinConditions.Items.Count; i++)
            ////    {
            ////        if (this.UcPhysExam.cblSkinConditions.Items[i].Selected == true)
            ////        {
            ////            count++;
            ////        }
            ////    }
            ////    if (count == 0)
            ////    {
            ////        totalMsgBuilder.DataElements["MessageText"] = "Select Skin Conditions";
            ////        IQCareMsgBox.ShowfromPage("#C1", totalMsgBuilder, this);
            ////        UcPhysExam.lblSkin.ForeColor = Color.Red;
            ////        lblMedicalConditions.ForeColor = Color.Red;
            ////        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValSkin", "alert('Select Skin Conditions')", true);
            ////        return false;
            ////    }
            ////    else
            ////    {
            ////        UcPhysExam.lblSkin.ForeColor = Color.FromArgb(0, 0, 142);
            ////        lblMedicalConditions.ForeColor = Color.FromArgb(0, 0, 142);
            ////    }

            ////    count = 0;
            ////    for (int i = 0; i < this.UcPhysExam.cblAbdomenConditions.Items.Count; i++)
            ////    {
            ////        if (this.UcPhysExam.cblAbdomenConditions.Items[i].Selected == true)
            ////        {
            ////            count++;
            ////        }
            ////    }
            ////    if (count == 0)
            ////    {
            ////        totalMsgBuilder.DataElements["MessageText"] = "Select Abdomen Conditions";
            ////        IQCareMsgBox.ShowfromPage("#C1", totalMsgBuilder, this);
            ////        UcPhysExam.lblAbdomen.ForeColor = Color.Red;
            ////        lblMedicalConditions.ForeColor = Color.Red;
            ////        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValAbdomen", "alert('Select Abdomen Conditions')", true);
            ////        return false;
            ////    }
            ////    else
            ////    {
            ////        UcPhysExam.lblAbdomen.ForeColor = Color.FromArgb(0, 0, 142);
            ////        lblMedicalConditions.ForeColor = Color.FromArgb(0, 0, 142);
            ////    }
            ////    if (UCWHO.ddlwhostage1.SelectedIndex == 0)
            ////    {
            ////        totalMsgBuilder.DataElements["MessageText"] = "Select WHO Stage";
            ////        IQCareMsgBox.ShowfromPage("#C1", totalMsgBuilder, this);
            ////        UCWHO.lblWHOStage.ForeColor = Color.Red;
            ////        lblheadWHOStage.ForeColor = Color.Red;
            ////        return false;
            ////    }
            ////    else
            ////    {
            ////        UCWHO.lblWHOStage.ForeColor = Color.FromArgb(0, 0, 142);
            ////        lblheadWHOStage.ForeColor = Color.FromArgb(0, 0, 142);
            ////    }
            ////    if (UCWHO.ddlWABStage.SelectedIndex == 0)
            ////    {
            ////        totalMsgBuilder.DataElements["MessageText"] = "Select WAB Stage";
            ////        IQCareMsgBox.ShowfromPage("#C1", totalMsgBuilder, this);
            ////        UCWHO.lblWABStage.ForeColor = Color.Red;
            ////        lblheadWHOStage.ForeColor = Color.Red;
            ////        return false;
            ////    }
            ////    else
            ////    {
            ////        UCWHO.lblWABStage.ForeColor = Color.FromArgb(0, 0, 142);
            ////        lblheadWHOStage.ForeColor = Color.FromArgb(0, 0, 142);
            ////    }

            ////}
            ////else if (TabName == "Management" || tabControl.ActiveTabIndex == 4)
            ////{

            ////    if (this.UserControlKNH_Pharmacy1.ddlTreatmentplan.SelectedIndex == 0)
            ////    {

            ////        totalMsgBuilder.DataElements["MessageText"] = "Select Treatment Plan";
            ////        IQCareMsgBox.ShowfromPage("#C1", totalMsgBuilder, this);
            ////        UserControlKNH_Pharmacy1.lblTreatmentplan.ForeColor = Color.Red;
            ////        lblheadregimenpresc.ForeColor = Color.Red;
            ////        return false;
            ////    }
            ////    else
            ////    {
            ////        UserControlKNH_Pharmacy1.lblTreatmentplan.ForeColor = Color.FromArgb(0, 0, 142);
            ////        lblheadregimenpresc.ForeColor = Color.FromArgb(0, 0, 142);
            ////    }

            ////}

            return true;

        }
        protected Hashtable HT(int qltyFlag, string tabname)
        {
            dtmuiltselect = CreateTempTable();
            Hashtable theHT = new Hashtable();
            try
            {
                theHT.Add("patientID", Session["PatientId"]);
                theHT.Add("visitID", Session["PatientVisitId"]);
                theHT.Add("locationID", Session["AppLocationId"]);          
                theHT.Add("visitDate", txtVisitDate.Text);               
                if (tabname.ToString() == "Triage")
                {                    
                    #region "Vital Signs"
                    if (this.idVitalSign.txtTemp.Text != "")
                    {
                        theHT.Add("Temp", this.idVitalSign.txtTemp.Text.ToString());
                    }
                    else
                    {
                        theHT.Add("Temp", "0");
                    }
                    if (this.idVitalSign.txtRR.Text != "")
                    {
                        theHT.Add("RR", this.idVitalSign.txtRR.Text);
                    }
                    else
                    {
                        theHT.Add("RR", "0");
                    }
                    if (this.idVitalSign.txtHR.Text != "")
                    {
                        theHT.Add("HR", this.idVitalSign.txtHR.Text);
                    }
                    else
                    {
                        theHT.Add("HR", "0");
                    }
                    if (this.idVitalSign.txtHeight.Text != "")
                    {
                        theHT.Add("height", this.idVitalSign.txtHeight.Text);
                    }
                    else
                    {
                        theHT.Add("height", "0");
                    }
                    if (this.idVitalSign.txtWeight.Text != "")
                    {
                        theHT.Add("weight", this.idVitalSign.txtWeight.Text);
                    }
                    else
                    {
                        theHT.Add("weight", "0");
                    }
                    if (this.idVitalSign.txtBPDiastolic.Text != "")
                    {
                        theHT.Add("BPDiastolic", this.idVitalSign.txtBPDiastolic.Text);
                    }
                    else
                    {
                        theHT.Add("BPDiastolic", "0");
                    }
                    if (this.idVitalSign.txtBPSystolic.Text != "")
                    {
                        theHT.Add("BPSystolic", this.idVitalSign.txtBPSystolic.Text);
                    }
                    else
                    {
                        theHT.Add("BPSystolic", "0");
                    }
                    if (this.idVitalSign.txtheadcircumference.Text != "")
                    {
                        theHT.Add("HeadCircumference", this.idVitalSign.txtheadcircumference.Text);
                    }
                    else
                    {
                        theHT.Add("HeadCircumference", "0");
                    }

                    theHT.Add("WeightForAge", this.idVitalSign.lblWA.Text);


                    theHT.Add("WeightForHeight", this.idVitalSign.lblWH.Text);
                    theHT.Add("BMIz", this.idVitalSign.lblBMIz.Text);

                    theHT.Add("NursesComments", this.idVitalSign.txtnursescomments.Text);
                    #endregion
                }
                if (tabname.ToString() == "PresCompl")
                {                    
                    theHT.Add("PresentingComplaintsAdditionalNotes", txtOtherPresentingComplaints.Text);                    
                    theHT.Add("lmp", txtLMPdate.Text);
                    int Pregnant = this.rdopregnantYes.Checked == true ? 1 : (this.rdopregnantNo.Checked == true ? 0 : 9);
                    int Delivered = this.rdoDeliveredYes.Checked == true ? 1 : (this.rdoDeliveredNo.Checked == true ? 0 : 9);
                    theHT.Add("Delivered", Delivered);
                    if (Delivered == 1)
                    {
                        theHT.Add("Pregnant", 0);
                        theHT.Add("DelDate", txtDeliDate.Text);
                        theHT.Add("EDDDate", "");
                    }
                    else if (Delivered == 0)
                    {
                        theHT.Add("Pregnant", 1);
                        theHT.Add("EDDDate", txtEDDDate.Text);
                        theHT.Add("DelDate", "");
                    }
                    else
                    {
                        theHT.Add("Pregnant", Pregnant);
                        if (Pregnant == 1)
                        {
                            theHT.Add("DelDate", "");
                            theHT.Add("EDDDate", txtEDDDate.Text);
                        }
                        else
                        {
                            theHT.Add("DelDate", "");
                            theHT.Add("EDDDate", "");
                        }
                    }

                    //Family Planning
                    theHT.Add("familyPlanningStatus", ddlFamilyPanningStatus.SelectedValue);
                    //Table 0-Family Planning Methods
                    DataTable dataTableFamilyPlanningMethods = new DataTable();
                    if (ddlFamilyPanningStatus.SelectedItem.Text == "Currently on Family Planning" || ddlFamilyPanningStatus.SelectedItem.Text == "Wants Family Planning")
                    {
                        dataTableFamilyPlanningMethods = GetCheckBoxListcheckedIDs(PnlFamilyPlanningMethod, "familyPlanningMethodID", "", 0);
                    }
                    else if (ddlFamilyPanningStatus.SelectedItem.Text == "Not on Family Planning")
                    {
                        theHT.Add("NoFamilyPlanning", ddlnotfamilyplanning.SelectedValue);
                    }
                    int CervCancer = this.rdocervcancyes.Checked == true ? 1 : (this.rdocervcancNo.Checked == true ? 0 : 9);
                    theHT.Add("CervCancer", CervCancer);
                    theHT.Add("CervCancerDate", txtCervCancDate.Text);
                }


                else if (tabname.ToString() == "AddHx")
                {
                    

                }
                else if (tabname.ToString() == "SystRew")
                {
                    //////MultiSelect////
                    theHT.Add("OtherGeneralConditions", UCPhyExam.txtOtherGeneralConditions.Text);
                    //////MultiSelect////
                    theHT.Add("OtherAbdomenConditions", UCPhyExam.txtOtherAbdomenConditions.Text);
                    //////MultiSelect////
                    theHT.Add("OtherCardiovascularConditions", UCPhyExam.txtOtherCardiovascularConditions.Text);
                    //////MultiSelect////
                    theHT.Add("OtherOralCavityConditions", UCPhyExam.txtOtherOralCavityConditions.Text);
                    //////MultiSelect////
                    theHT.Add("OtherGenitourinaryConditions", UCPhyExam.txtOtherGenitourinaryConditions.Text);
                    //////MultiSelect////
                    theHT.Add("OtherCNSConditions", UCPhyExam.txtOtherCNSConditions.Text);
                    //////MultiSelect////
                    theHT.Add("OtherChestLungsConditions", UCPhyExam.txtOtherChestLungsConditions.Text);
                    //////MultiSelect////
                    theHT.Add("OtherSkinConditions", UCPhyExam.txtOtherSkinConditions.Text);
                    theHT.Add("OtherMedicalConditionNotes", UCPhyExam.txtOtherMedicalConditionNotes.Text);

                    
                    theHT.Add("WABStage", Convert.ToInt32(UCWHO.ddlWABStage.SelectedValue));
                    //////////Progression in WHO//////////////                    
                    theHT.Add("CurrentWHOStage", UCWHO.ddlwhostage1.SelectedValue);
                    int Menarche = UCWHO.radbtnMernarcheyes.Checked ? 1 : UCWHO.radbtnMernarcheno.Checked ? 0 : 9;
                    theHT.Add("Menarche", Convert.ToInt32(Menarche));
                    theHT.Add("MenarcheDate", UCWHO.txtmenarchedate.Value);
                    theHT.Add("TannerStaging", Convert.ToInt32(UCWHO.ddltannerstaging.SelectedValue));
                }
                else if (tabname.ToString() == "Mgmt")
                {
                    
                    //Pharmacy
                    theHT.Add("CotrimoxazoleAdhere", ddlCotrimoxazoleAdhere.SelectedValue);
                    theHT.Add("ARVDrugsAdhere", ddlarvdrugadhere.SelectedValue);
                    theHT.Add("WhyPooFair", ddlwhypoorfair.SelectedValue);
                    theHT.Add("reasonARVDrugsPoorFairOther", txtReasonARVDrugsPoorFairOther.Text);                   
                    ////////MultiSelect////Short term effects
                    theHT.Add("Specifyothershorttermeffects", txtspecityothershortterm.Text);
                    ////////MultiSelect////Long term effects
                    theHT.Add("OtherLongtermEffects", txtspecifyotherlongterm.Text);
                    ////////////Diagnosis//////////////
                    //theHT.Add("SpecifyLabEvaluation", UcLabEval.txtlabdiagnosticinput.Text);
                    //theHT.Add("Fluconazole", Convert.ToInt32(ddlfluconazole.SelectedValue));
                }

            }
            catch (Exception err)
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.ShowfromPage("#C1", theMsg, this);
            }
            return theHT;

        }
        private DataTable CreateTempTable()
        {
            DataTable dtprescompl = new DataTable();

            DataColumn theID = new DataColumn("ID");
            theID.DataType = System.Type.GetType("System.Int32");
            dtprescompl.Columns.Add(theID);

            DataColumn theFieldName = new DataColumn("FieldName");
            theFieldName.DataType = System.Type.GetType("System.String");
            dtprescompl.Columns.Add(theFieldName);

            DataColumn theDateValue1 = new DataColumn("DateField1");
            theDateValue1.DataType = System.Type.GetType("System.String");
            dtprescompl.Columns.Add(theDateValue1);

            DataColumn theDateValue2 = new DataColumn("DateField2");
            theDateValue2.DataType = System.Type.GetType("System.String");
            dtprescompl.Columns.Add(theDateValue2);

            DataColumn theValue1 = new DataColumn("NumericField");
            theValue1.DataType = System.Type.GetType("System.String");
            dtprescompl.Columns.Add(theValue1);

            DataColumn theOther = new DataColumn("Other_Notes");
            theOther.DataType = System.Type.GetType("System.String");
            dtprescompl.Columns.Add(theOther);
            return dtprescompl;
        }
        private void SaveCancel(string tabname)
        {            
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowConfirmYesNo", "ShowConfirmYesNo('" + tabname + "');", true);
            
        }
        private DataTable PresentingComplaints(DataTable dtprescompl, string name)
        {

            GridView gdview = (GridView)FindControl("gvPresentingComplaints");
            foreach (GridViewRow row in gdview.Rows)
            {
                DataRow dr = dtprescompl.NewRow();
                CheckBox chk = (CheckBox)row.FindControl("ChkPresenting");
                TextBox txt = (TextBox)row.FindControl("txtPresenting");
                Label lbl = (Label)row.FindControl("lblPresenting");
                if (chk.Checked)
                {
                    dr["ID"] = Convert.ToInt32(lbl.Text);
                    dr["FieldName"] = name;
                    if (txt.Text != "")
                    {
                        dr["Other_Notes"] = txt.Text;
                    }
                    dtprescompl.Rows.Add(dr);
                }


            }
            return dtprescompl;

        }
        protected Hashtable TBHT()
        {

            Hashtable theHT = new Hashtable();
            try
            {
                theHT.Add("patientID", Convert.ToInt32(Session["PatientId"]));
                theHT.Add("visitID", Convert.ToInt32(Session["PatientVisitId"]));
                theHT.Add("locationID", Convert.ToInt32(Session["AppLocationID"]));
                theHT.Add("userID", Convert.ToInt32(Session["AppUserId"]));
                theHT.Add("TBFindings", UCTBScreen.ddlTBFindings.SelectedValue);
                //theHT.Add("availableTBResults", UCTBScreen.rdoAvailab.Checked == true ? "1" : (UCTBScreen.rdopregnantNo.Checked == true ? "0" : "9"));
                theHT.Add("SputumSmear", Convert.ToInt32(UCTBScreen.ddlSputumSmear.SelectedValue));
                theHT.Add("GeneExpert", Convert.ToInt32(UCTBScreen.ddlGeneExpert.SelectedValue));
                theHT.Add("SputumDST", Convert.ToInt32(UCTBScreen.ddlSputumDST.SelectedValue));
                theHT.Add("chestXRay", UCTBScreen.rdoChestXrayYes.Checked == true ? "1" : (UCTBScreen.rdoChestXrayNo.Checked == true ? "0" : "9"));
                theHT.Add("tissueBiopsy", UCTBScreen.rdoTissueBiopsyYes.Checked == true ? "1" : (UCTBScreen.rdoTissueBiopsyNo.Checked == true ? "0" : "9"));
                theHT.Add("CXRResults", Convert.ToInt32(UCTBScreen.ddlCXRResults.SelectedValue));
                theHT.Add("OtherCXRResults", UCTBScreen.txtOtherCXRResults.Text);
                theHT.Add("TBClassification", Convert.ToInt32(UCTBScreen.ddlTBClassification.SelectedValue));
                theHT.Add("PatientClassification", Convert.ToInt32(UCTBScreen.ddlPatientClassification.SelectedValue));
                theHT.Add("TBPLan", Convert.ToInt32(UCTBScreen.ddlTBPLan.SelectedValue));
                theHT.Add("OtherTBPlan", UCTBScreen.txtOtherTBPlan.Text);
                theHT.Add("TBRegimen", Convert.ToInt32(UCTBScreen.ddlTBRegimen.SelectedValue));
                theHT.Add("OtherTBRegimen", UCTBScreen.txtOtherTBRegimen.Text);

                theHT.Add("sputumSmearDate", UCTBScreen.txtSputumSmearDate.Text.Trim());
                theHT.Add("GeneExpertDate", UCTBScreen.txtGeneExpertDate.Text);
                theHT.Add("SputumDSTDate", UCTBScreen.txtSputumDSTDate.Text);
                theHT.Add("chestXRayDate", UCTBScreen.txtChestXrayDate.Text);
                theHT.Add("TissueBiopsyDate", UCTBScreen.txtTissueBiopsyDate.Text);
                theHT.Add("TBRegimenStartDate", UCTBScreen.txtTBRegimenEndDate.Text);
                theHT.Add("TBRegimenEndDate", UCTBScreen.txtTBRegimenEndDate.Text);
                theHT.Add("INHStartDate", UCTBScreen.txtINHStartDate.Text);
                theHT.Add("INHEndDate", UCTBScreen.txtINHEndDate.Text);
                theHT.Add("PyridoxineStartDate", UCTBScreen.txtPyridoxineStartDate.Text);
                theHT.Add("PyridoxineEndDate", UCTBScreen.txtPyridoxineEndDate.Text);
                //As per john this col is not required
                //theHT.Add("INHStopDate",txtINHStopDate.Text);
                /////////////////////////////////////////              
                theHT.Add("TBTreatment", Convert.ToInt32(UCTBScreen.ddlTBTreatment.SelectedValue));
                theHT.Add("OtherTBTreatment", UCTBScreen.txtOtherTreatmentOutcome.Text);
                theHT.Add("IPT", UCTBScreen.rdoLstIPT.SelectedValue);
                theHT.Add("AdherenceAddressed", UCTBScreen.rdoAdherenceBeenAddressedYes.Checked == true ? "1" : (UCTBScreen.rdoAdherenceBeenAddressedNo.Checked == true ? "0" : "9"));
                theHT.Add("missedAnyDoses", UCTBScreen.rdoMissedAnyTBDosesYes.Checked == true ? "1" : (UCTBScreen.rdoMissedAnyTBDosesNo.Checked == true ? "0" : "9"));
                theHT.Add("ReferredForAdherence", UCTBScreen.rdoReferredForAdherenceYes.Checked == true ? "1" : (UCTBScreen.rdoReferredForAdherenceNo.Checked == true ? "0" : "9"));
                theHT.Add("ContactsScreenedForTB", UCTBScreen.rdoContactsScreenedForTBYes.Checked == true ? "1" : (UCTBScreen.rdoContactsScreenedForTBNo.Checked == true ? "0" : "9"));
                theHT.Add("SpecifyOtherTBSideEffects", UCTBScreen.txtSpecifyOtherTBSideEffects.Text);
                theHT.Add("SpecifyWhyContactNotScreenedForTB", UCTBScreen.txtSpecifyWhyContactNotScreenedForTB.Text);
                theHT.Add("age", Convert.ToDouble(Session["patientageinyearmonth"].ToString()));
                theHT.Add("FacilityPatientReferredTo", UCTBScreen.ddlPatientReferredForTreatment.SelectedValue);
                theHT.Add("ReasonDeclinedIPT", UCTBScreen.ddlReasonDeclinedIPT.SelectedValue);
                theHT.Add("OtherReasonDeclinedIPT", UCTBScreen.txtOtherReasonDeclinedIPT.Text);

            }
            catch (Exception err)
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.ShowfromPage("#C1", theMsg, this);
            }
            return theHT;

        }
        private DataTable getCheckBoxListItemValues(CheckBoxList cbl)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("value", typeof(int));
            if (cbl.Items.Count > 0)
            {
                for (int i = 0; i < cbl.Items.Count; i++)
                {
                    if (cbl.Items[i].Selected)
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] = cbl.Items[i].Value;
                        dt.Rows.Add(dr);
                    }
                }
            }

            return dt;
        }
        private DataTable InsertMultiSelectList(GridView gdview, DataTable dt, string fieldname)
        {
            foreach (GridViewRow row in gdview.Rows)
            {
                DataRow dr = dt.NewRow();

                if (fieldname == "CurrentWHOStageIConditions")
                {
                    CheckBox chk = (CheckBox)row.FindControl("Chkwho1");
                    System.Web.UI.HtmlControls.HtmlInputText txtdt1 = (HtmlInputText)row.FindControl("txtCurrentWho1Date");
                    System.Web.UI.HtmlControls.HtmlInputText txtdt2 = (HtmlInputText)row.FindControl("txtCurrentWho1Date1");
                    Label lbl = (Label)row.FindControl("lblwho1");

                    if (chk.Checked)
                    {
                        dr["ID"] = Convert.ToInt32(lbl.Text);
                        dr["FieldName"] = fieldname;
                        if (txtdt1.Value != "")
                        {
                            dr["DateField1"] = txtdt1.Value;
                        }
                        if (txtdt2.Value != "")
                        {
                            dr["DateField2"] = txtdt2.Value;
                        }
                        dt.Rows.Add(dr);
                    }
                }
                if (fieldname == "CurrentWHOStageIIConditions")
                {
                    CheckBox chk = (CheckBox)row.FindControl("Chkwho2");
                    System.Web.UI.HtmlControls.HtmlInputText txtdt1 = (HtmlInputText)row.FindControl("txtCurrentWho2Date");
                    System.Web.UI.HtmlControls.HtmlInputText txtdt2 = (HtmlInputText)row.FindControl("txtCurrentWho2Date1");
                    Label lbl = (Label)row.FindControl("lblwho2");

                    if (chk.Checked)
                    {
                        dr["ID"] = Convert.ToInt32(lbl.Text);
                        dr["FieldName"] = fieldname;
                        if (txtdt1.Value != "")
                        {
                            dr["DateField1"] = txtdt1.Value;
                        }
                        if (txtdt2.Value != "")
                        {
                            dr["DateField2"] = txtdt2.Value;
                        }
                        dt.Rows.Add(dr);
                    }
                }
                if (fieldname == "CurrentWHOStageIIIConditions")
                {
                    CheckBox chk = (CheckBox)row.FindControl("Chkwho3");
                    System.Web.UI.HtmlControls.HtmlInputText txtdt1 = (HtmlInputText)row.FindControl("txtCurrentWho3Date");
                    System.Web.UI.HtmlControls.HtmlInputText txtdt2 = (HtmlInputText)row.FindControl("txtCurrentWho3Date1");
                    Label lbl = (Label)row.FindControl("lblwho3");

                    if (chk.Checked)
                    {
                        dr["ID"] = Convert.ToInt32(lbl.Text);
                        dr["FieldName"] = fieldname;
                        if (txtdt1.Value != "")
                        {
                            dr["DateField1"] = txtdt1.Value;
                        }
                        if (txtdt2.Value != "")
                        {
                            dr["DateField2"] = txtdt2.Value;
                        }
                        dt.Rows.Add(dr);
                    }
                }
                if (fieldname == "CurrentWHOStageIVConditions")
                {
                    CheckBox chk = (CheckBox)row.FindControl("Chkwho4");
                    System.Web.UI.HtmlControls.HtmlInputText txtdt1 = (HtmlInputText)row.FindControl("txtCurrentWho4Date");
                    System.Web.UI.HtmlControls.HtmlInputText txtdt2 = (HtmlInputText)row.FindControl("txtCurrentWho4Date1");
                    Label lbl = (Label)row.FindControl("lblwho4");

                    if (chk.Checked)
                    {
                        dr["ID"] = Convert.ToInt32(lbl.Text);
                        dr["FieldName"] = fieldname;
                        if (txtdt1.Value != "")
                        {
                            dr["DateField1"] = txtdt1.Value;
                        }
                        if (txtdt2.Value != "")
                        {
                            dr["DateField2"] = txtdt2.Value;
                        }
                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;



        }
        private DataTable GetCheckBoxListValues(CheckBoxList chklist, DataTable dt, string name)
        {
            DataRow dr;
            for (int i = 0; i < chklist.Items.Count; i++)
            {
                if (chklist.Items[i].Selected)
                {
                    dr = dt.NewRow();
                    dr["ID"] = Convert.ToInt32(chklist.Items[i].Value);
                    dr["FieldName"] = name;
                    dt.Rows.Add(dr);
                }

            }
            return dt;
        }
        private DataTable GetCheckBoxListcheckedIDs(Panel thePnl, string FieldName, string thetxtFieldName, int Flag)
        {

            string chktrueother = "";
            int chktrueothervalue = 0;
            if (Flag == 0)
            {
                DTCheckedIds = new DataTable();

                if (DTCheckedIds.Columns.Contains(FieldName) == false && DTCheckedIds.Columns.Contains(FieldName) == false)
                {
                    DataColumn dataColumnPotentialSideEffect = new DataColumn(FieldName);
                    dataColumnPotentialSideEffect.DataType = System.Type.GetType("System.Int32");
                    DTCheckedIds.Columns.Add(dataColumnPotentialSideEffect);
                    if (thetxtFieldName != "")
                    {
                        DataColumn thepotentialSideEffect_Other = new DataColumn(thetxtFieldName);
                        thepotentialSideEffect_Other.DataType = System.Type.GetType("System.String");
                        DTCheckedIds.Columns.Add(thepotentialSideEffect_Other);
                    }

                }

            }
            DataRow theDR;
            foreach (Control y in thePnl.Controls)
            {
                if (y.GetType() == typeof(System.Web.UI.WebControls.Panel))
                    GetCheckBoxListcheckedIDs((System.Web.UI.WebControls.Panel)y, FieldName, thetxtFieldName, 1);

                else
                {

                    if (y.GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                    {
                        if (((CheckBox)y).Checked == true)
                        {

                            string[] theControlId = ((CheckBox)y).ID.ToString().Split('-');
                            //theDR[FieldName] = theControlId[1].ToString();
                            if (theControlId[2].ToString().Contains("Other") == true)
                            {
                                chktrueother = theControlId[2].ToString();
                                chktrueothervalue = Convert.ToInt32(theControlId[1].ToString());
                            }
                            else
                            {
                                theDR = DTCheckedIds.NewRow();
                                theDR[FieldName] = theControlId[1].ToString();
                                DTCheckedIds.Rows.Add(theDR);
                            }

                        }

                    }
                    if (y.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                    {
                        if (thetxtFieldName != "")
                        {
                            if (((System.Web.UI.WebControls.TextBox)y).ID.Contains("OtherTXT") == true)
                            {
                                theDR = DTCheckedIds.NewRow();
                                string[] theControlId = ((TextBox)y).ID.ToString().Split('-');
                                theDR[FieldName] = chktrueothervalue.ToString();
                                if (((TextBox)y).Text != "")
                                {
                                    theDR[thetxtFieldName] = ((TextBox)y).Text;
                                    DTCheckedIds.Rows.Add(theDR);
                                }

                            }
                            string script = "";
                            script = "<script language = 'javascript' defer ='defer' id = " + ((TextBox)y).ID + ">\n";
                            script += "show('txt" + chktrueothervalue.ToString() + "');\n";
                            script += "</script>\n";
                            RegisterStartupScript("" + ((TextBox)y).ID + "", script);
                        }

                    }
                    //DTCheckedIds.Rows.Add(theDR);
                }
            }
            return DTCheckedIds;

        }

        protected void btnSaveTriage_Click(object sender, EventArgs e)
        {
            Save("Triage");
        }
        protected void btncloseTriage_Click(object sender, EventArgs e)
        {
            string theUrl;
            theUrl = string.Format("frmPatient_Home.aspx");
            Response.Redirect(theUrl);
        }
        protected void btnSavePresCompl_Click(object sender, EventArgs e)
        {
            Save("PresCompl");
        }
        protected void btnClosePresCompl_Click(object sender, EventArgs e)
        {
            string theUrl;
            theUrl = string.Format("frmPatient_Home.aspx");
            Response.Redirect(theUrl);
        }
        protected void btnSaveAddHx_Click(object sender, EventArgs e)
        {
            Save("AddHx");
        }
        protected void btncloseAddHx_Click(object sender, EventArgs e)
        {
            string theUrl;
            theUrl = string.Format("frmPatient_Home.aspx");
            Response.Redirect(theUrl);
        }
        protected void btnSaveSystRew_Click(object sender, EventArgs e)
        {
            Save("SystRew");
        }
        protected void btncloseSystRew_Click(object sender, EventArgs e)
        {
            string theUrl;
            theUrl = string.Format("frmPatient_Home.aspx");
            Response.Redirect(theUrl);
        }
        protected void btnSaveMgmt_Click(object sender, EventArgs e)
        {
            Save("Mgmt");
        }
        protected void btncloseMgmt_Click(object sender, EventArgs e)
        {
            string theUrl;
            theUrl = string.Format("frmPatient_Home.aspx");
            Response.Redirect(theUrl);
        }

        protected void ddlvisittype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
            {
                DataSet theDSARTHistory = new DataSet();
                IPriorArtHivCare ARTHistoryMgr = (IPriorArtHivCare)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPriorArtHivCare, BusinessProcess.Clinical");
                theDSARTHistory = ARTHistoryMgr.GetGreenCardData(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["PatientVisitId"]), Convert.ToInt32(Session["AppLocationId"]));
                if (Convert.ToInt32(theDSARTHistory.Tables[3].Rows[0]["TypeofVisit"]) > 0)
                {
                    ddlvisittype.SelectedValue = theDSARTHistory.Tables[3].Rows[0]["TypeofVisit"].ToString();
                    if(ddlvisittype.SelectedValue.ToString()=="Initial only")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "DisableTabbyId", "DisableTabbyId('follow');", true);
                    }
                    else if (ddlvisittype.SelectedValue.ToString() == "Follow Up")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "DisableTabbyId", "DisableTabbyId('int');", true);
                    }
                }
                

            }
        }

    }
}