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

namespace PresentationApp.GreenCard
{
    public partial class frm_GreenCard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Authenticate();
                FillDropDown();
                Init_ARTHistory();
                GetGreenCardData();
                PatientDetails();
                BindFamilyGrid();
                BindSubsSwitchs();


            }
            else
            {
                Init_ARTHistory();
                if ((DataTable)Application["AddRegimen"] != null)
                {
                    DataTable theDT = (DataTable)Application["AddRegimen"];
                    ViewState["SelectedData"] = theDT;
                    string theStr = FillRegimen(theDT);
                    txtRegimen.Text = theStr;
                    Application.Remove("AddRegimen");
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

                txtVisitDate.Text = String.Format("{0:dd-MMM-yyyy}", theDSARTHistory.Tables[0].Rows[0]["VisitDate"]);
                txtTransferInDate.Text = String.Format("{0:dd-MMM-yyyy}", theDSARTHistory.Tables[0].Rows[0]["TransferInDate"]);
                ddlvisittype.SelectedValue = Convert.ToString(theDSARTHistory.Tables[0].Rows[0]["VisitTypeId"]);
                ddltransferfrom.SelectedValue = Convert.ToString(theDSARTHistory.Tables[0].Rows[0]["TransferInFrom"]);
                dddistrict.SelectedValue = Convert.ToString(theDSARTHistory.Tables[0].Rows[0]["TransferInDistrictId"]);
                ddfacility.SelectedValue = Convert.ToString(theDSARTHistory.Tables[0].Rows[0]["TransferInFacilityId"]);
                txtMFLCode.Text = theDSARTHistory.Tables[0].Rows[0]["TransferMFLCode"].ToString();
                ddlCountryFrom.SelectedValue = Convert.ToString(theDSARTHistory.Tables[0].Rows[0]["TransferInCountryId"]);
                ddlbaseWHO.SelectedValue = Convert.ToString(theDSARTHistory.Tables[0].Rows[0]["BaseWHOStage"]);
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
                txtbaseviraldate.Text = String.Format("{0:dd-MMM-yyyy}", theDSARTHistory.Tables[0].Rows[0]["BaseViralLoadDate"]);
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

                txtdeathdate.Text = String.Format("{0:dd-MMM-yyyy}", theDSARTHistory.Tables[0].Rows[0]["DeathDate"]);
                txtsexDisDate.Text = String.Format("{0:dd-MMM-yyyy}", theDSARTHistory.Tables[0].Rows[0]["PartnerDisclosureDate"]);
                partDisDate.Text = String.Format("{0:dd-MMM-yyyy}", theDSARTHistory.Tables[0].Rows[0]["AdolPartialDisclosureDate"]);                
                txtFullDisDate.Text = String.Format("{0:dd-MMM-yyyy}", theDSARTHistory.Tables[0].Rows[0]["AdolFullDisclosureDate"]);
                txtINHStartDate.Text = String.Format("{0:dd-MMM-yyyy}", theDSARTHistory.Tables[0].Rows[0]["INHStartDate"]);
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

           

        }        

            #endregion
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
        protected void btnRegimen_Click(object sender, EventArgs e)
        {
            string theScript;
            Application.Add("MasterData", ViewState["MasterARVData"]);
            Session["DrugData"] = ViewState["MasterARVData"];
            Application.Add("SelectedDrug", (DataTable)ViewState["SelectedData"]);
            theScript = "<script language='javascript' id='DrgSelPopup'>\n";
            theScript += "window.open('../Pharmacy/frmDrugSelector.aspx?DrugType=37&btnreg=" + btnRegimen.ID + "' ,'DrugSelection','toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=no,resize=no,width=700,height=350,scrollbars=yes');\n";
            theScript += "</script>\n";
            Page.RegisterStartupScript("DrgPopup", theScript);
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
                IQCareMsgBox.Show("RegLastUsed", this);
                txtLastUsed.Focus();
                return false;
            }
            if (Convert.ToDateTime(theUtil.MakeDate(txtLastUsed.Text)) > Convert.ToDateTime(theUtil.MakeDate(Application["AppCurrentDate"].ToString())))
            {
                IQCareMsgBox.Show("CmpLastUsed", this);
                txtLastUsed.Focus();
                return false;
            }
            else if (ddlpurpose.SelectedValue == "0")
            {
                IQCareMsgBox.Show("ChkPurpose", this);
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
            }
            if (theDS.Tables["Mst_PMTCTDecode"] != null)
            {
                //Field VisitType
                DataView theDVDecode = new DataView(theDS.Tables["Mst_PMTCTDecode"]);
                theDVDecode.RowFilter = "CodeName='FieldVisitType' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    DataTable theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    theDTCode = theDTCode.Select("Name <> 'ANC PMTCT'").CopyToDataTable();
                    theBndMgr.BindCombo(ddlvisittype, theDTCode, "Name", "Id");

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
                IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
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
                theHT.Add("VisitTypeId",ddlvisittype.SelectedValue);
	            theHT.Add("TransferInFrom",ddltransferfrom.SelectedValue);
	            theHT.Add("TransferInDate",txtTransferInDate.Text);
	            theHT.Add("TransferInDistrictId",dddistrict.SelectedValue);
	            theHT.Add("TransferInFacilityId",ddfacility.SelectedValue);
	            theHT.Add("TransferMFLCode",txtMFLCode.Text);
	            theHT.Add("TransferInCountryId",ddlCountryFrom.SelectedValue);
	            theHT.Add("ARTStartDate",txtDateARTStarted.Text);
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
                IQCareMsgBox.Show("#C1", theMsg, this);
            }
            return theHT;

        }
        
    }
}