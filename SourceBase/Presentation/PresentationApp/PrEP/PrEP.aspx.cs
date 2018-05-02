using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HIVCE.BusinessLayer;
using HIVCE.Common;
using System.Configuration;
using Entities.Common;
using Application.Presentation;
using System.Data;
using Interface.HIVCE;
using Interface.Service;
using Application.Common;
using Interface.Clinical;
using System.Web.UI.HtmlControls;
using System.Web.Services;

namespace PresentationApp.PrEP
{
    public partial class PrEP : System.Web.UI.Page
    {
        ICommonData oCommonData = (ICommonData)ObjectFactory.CreateInstance("BusinessProcess.Service.BCommonData,BusinessProcess.Service");

        protected void Page_Load(object sender, EventArgs e)
        {
            //Session["PatientInformation"]
            //Session["PatientSex"] = "Female";
            //Session["PatientAge"] = "12";
            int PatientId = 0;
            int visitPK = 0;
            int locationId = 0;
            int userId = 0;

            if (!IsPostBack)
            {
                if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
                {
                    Response.Redirect("~/frmlogin.aspx", true);
                }

                if (!object.Equals(Session["PatientId"], null))
                {
                    PatientId = Convert.ToInt32(Session["PatientId"]);
                    if (PatientId == 0)
                    {
                        Response.Redirect("~/ClinicalForms/frmPatient_Home.aspx", true);
                    }
                    this.hidPID.Value = PatientId.ToString();
                }

                if (!object.Equals(Session["PatientVisitId"], null))
                {
                    if (!object.Equals(Request.QueryString["add"], null))
                    {
                        if (Request.QueryString["add"].ToString() == "0")
                        {
                            visitPK = 0;
                            Session["PatientVisitId"] = "0";
                        }
                    }
                    else
                    {
                        visitPK = Convert.ToInt32(Session["PatientVisitId"]);
                    }
                }
                else
                {
                    if (!object.Equals(Request.QueryString["add"], null))
                    {
                        if (Request.QueryString["add"].ToString() == "0")
                        {
                            visitPK = 0;
                        }
                    }
                }
                this.hidVId.Value = visitPK.ToString();

                if (!object.Equals(Session["AppLocationId"], null))
                {
                    locationId = Convert.ToInt32(Session["AppLocationId"]);
                }
                if (!object.Equals(Session["AppUserId"], null))
                {
                    userId = Convert.ToInt32(Session["AppUserId"]);
                }
                if (!object.Equals(Session["PatientSex"], null))
                {
                    this.hidGender.Value = Session["PatientSex"].ToString();
                }
                if (!object.Equals(Session["PatientAge"], null))
                {
                    this.hidDOB.Value = Session["PatientAge"].ToString();
                }
                if (!object.Equals(Session["patientageinyearmonth"], null))
                {
                    this.hidPAYM.Value = Session["patientageinyearmonth"].ToString();
                }
                if (!object.Equals(Session["TechnicalAreaName"], null))
                {
                    this.hidsrvNm.Value = Session["TechnicalAreaName"].ToString();
                }
                if (!object.Equals(Session["TechnicalAreaId"], null))
                {
                    this.hidMOD.Value = Session["TechnicalAreaId"].ToString();
                }

                string tabName = "triage";// ((HtmlInputHidden)this.hidTabName).Value.ToString();
                if (!object.Equals(Request.QueryString["data"], null))
                {
                    if (Request.QueryString["data"].ToString() == "gettriagedata")
                    {
                        tabName = "triage";
                    }
                    if (Request.QueryString["data"].ToString() == "getassessmentdata")
                    {
                        tabName = "assessment";
                    }
                    if (Request.QueryString["data"].ToString() == "getinitiationdata")
                    {
                        tabName = "initiation";
                    }

                    if (Request.QueryString["data"].ToString() == "getzscore")
                    {
                        tabName = "triage";
                    }
                }

                Authenticate();

                if (!object.Equals(Request.QueryString["data"], null))
                {
                    string response = string.Empty;


                    if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
                    {
                        CLogger.WriteLog(ELogLevel.ERROR, "Session expired!!");

                        ResponseType responsetype = new ResponseType() { Success = EnumUtil.GetEnumDescription(Success.False), ErrorMessage = "Session expired" };
                        response = SerializerUtil.ConverToJson<ResponseType>(responsetype);
                        SendResponse(response);
                    }

                    if (Request.QueryString["data"].ToString() == "gettriagedata")
                    {
                        response = GetPrEPTriage(Convert.ToInt32(PatientId), visitPK, locationId);
                        SendResponse(response);
                    }
                    if (Request.QueryString["data"].ToString() == "getassessmentdata")
                    {
                        response = GetPrEPAssessment(Convert.ToInt32(PatientId), visitPK, locationId);
                        SendResponse(response);
                    }
                    if (Request.QueryString["data"].ToString() == "getinitiationdata")
                    {
                        response = GetPrEPInitiation(Convert.ToInt32(PatientId), visitPK, locationId);
                        SendResponse(response);
                    }
                    if (Request.QueryString["data"].ToString() == "getfacility")
                    {
                        response = GetFacilities(Convert.ToInt32(PatientId), visitPK, locationId);
                        SendResponse(response);
                    }
                    if (Request.QueryString["data"].ToString() == "getzscore")
                    {
                        response = GetZScoreDetails(Convert.ToInt32(PatientId), visitPK, locationId);
                        SendResponse(response);
                    }

                    if (Request.QueryString["data"].ToString() == "savetriage")
                    {

                        System.IO.StreamReader sr = new System.IO.StreamReader(Request.InputStream);
                        string jsonString = "";
                        jsonString = sr.ReadToEnd();

                        response = SavePrEPTriage(jsonString, PatientId, visitPK, locationId, userId);
                        SendResponse(response);
                    }

                    if (Request.QueryString["data"].ToString() == "saveassessment")
                    {

                        System.IO.StreamReader sr = new System.IO.StreamReader(Request.InputStream);
                        string jsonString = "";
                        jsonString = sr.ReadToEnd();

                        //response = SaveAssessmentData(jsonString, PatientId, visitPK, locationId, userId);
                        SendResponse(response);
                    }
                    if (Request.QueryString["data"].ToString() == "saveinitiation")
                    {
                        System.IO.StreamReader sr = new System.IO.StreamReader(Request.InputStream);
                        string jsonString = "";
                        jsonString = sr.ReadToEnd();

                        response = SaveInitiationData(jsonString, PatientId, visitPK, locationId, userId);
                        SendResponse(response);
                    }

                    if (Request.QueryString["data"].ToString() == "deleteform")
                    {
                        response = DeleteForm();
                        SendResponse(response);
                    }

                }
            }
        }

        private string GetPrEPInitiation(int ptn_pk, int visitPK, int locationId)
        {
            string result = string.Empty;
            try
            {
                IPrEP ipatientPrEP;
                ipatientPrEP = (IPrEP)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPrEP, BusinessProcess.Clinical");
                Entities.Common.PrEP patientPrEP = ipatientPrEP.GetPrEPInitiationData(ptn_pk, visitPK, locationId);
                //PrEP patientPrEP = ipatientPrEP.GetPrEPInitiationData(ptn_pk, visitPK, locationId);

                result = SerializerUtil.ConverToJson<Entities.Common.PrEP>(patientPrEP);
            }
            catch (Exception ex)
            {
                string str = "ptn_pk: " + ptn_pk.ToString() + ",visitPK: " + visitPK.ToString() + ",locationId:" + locationId.ToString();
                CLogger.WriteLog(ELogLevel.ERROR, "GetPrEPInitiation() exception: " + str + "-" + ex.ToString());
                ResponseType response = new ResponseType() { Success = EnumUtil.GetEnumDescription(Success.False) };
                result = SerializerUtil.ConverToJson<ResponseType>(response);
            }
            finally
            {

            }
            return result;
        }
        private string SaveInitiationData(string nodeJson, int ptn_pk, int visitPK, int locationId, int userId)
        {
            string result = string.Empty;
            ResponseType ObjResponse = new ResponseType();
            try
            {
                Entities.Common.PrEPInitiation PrEPdata = SerializerUtil.ConverToObject<Entities.Common.PrEPInitiation>(nodeJson);
                IPrEP iPrEPdata;
                iPrEPdata = (IPrEP)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPrEP, BusinessProcess.Clinical");
                PrEPdata.Ptn_pk = ptn_pk;
                PrEPdata.Visit_Id = visitPK;
                PrEPdata.LocationId = locationId;
                int i = iPrEPdata.SaveUpdatePrEPInitiationData(PrEPdata, userId, visitPK, locationId);


                //Session["PatientVisitId"] = visitId.ToString();
                //this.hidVId.Value = visitId.ToString();

                if (i > 0)
                {
                    ObjResponse.Success = EnumUtil.GetEnumDescription(Success.True);
                }
                else
                {
                    ObjResponse.Success = EnumUtil.GetEnumDescription(Success.False);
                }

            }
            catch (Exception ex)
            {
                if (ex.Message.ToLower().Contains("visit already exists"))
                {
                    ObjResponse.ErrorMessage = ex.Message;
                }
                ObjResponse.Success = EnumUtil.GetEnumDescription(Success.False);
                CLogger.WriteLog(ELogLevel.ERROR, "SaveUpdatePrEPInitiationData() exception: " + ex.ToString());
            }
            finally
            {
            }

            result = SerializerUtil.ConverToJson<ResponseType>(ObjResponse);
            return result;
        }
        public void Authenticate()
        {
            //string tabName = ((HtmlInputHidden)this.hidTabName).Value.ToString();
            AuthenticationManager authMgr = new AuthenticationManager();

            /********** getting the data from XML file *******************************/
            DataSet XMLDS = new DataSet();
            XMLDS.ReadXml(MapPath("..\\XMLFiles\\AllMasters.con"));
            DataView theDV = new DataView(XMLDS.Tables["Mst_FormBuilderTab"]);

            //int featureId = 298;
            int featureId = ApplicationAccess.ClinicalEncounter;


            theDV.RowFilter = "FeatureId=" + featureId;
            IQCareUtils iQCareUtils = new IQCareUtils();
            DataTable dtTabId = (DataTable)iQCareUtils.CreateTableFromDataView(theDV);

            int tabId = 0;

            bool enable = true;
            int visitId = Convert.ToInt32(Session["PatientVisitId"]);

            //HtmlButton btnSaveTriage = (HtmlButton)this.ucPrEPTriage1.FindControl("btnSaveTriage");


            //HtmlButton btnSaveFindTriage = (HtmlButton)this.ucHIVCETriage1.FindControl("btnSaveFindTriage");
            //HtmlButton btnDeleteForm = (HtmlButton)this.ucHIVCETriage1.FindControl("btnDeleteForm");
            //HtmlButton btnSavePC = (HtmlButton)this.ucPresentingComplaints1.FindControl("btnSavePC");
            //HtmlButton btnSaveAddtionalHx = (HtmlButton)this.ucAddtionalHx1.FindControl("btnSaveAddtionalHx");
            //HtmlButton btnSaveScreening = (HtmlButton)this.ucScreening1.FindControl("btnSaveScreening");
            //HtmlButton btnSaveSystemicReview = (HtmlButton)this.ucSystemicReview1.FindControl("btnSaveSystemicReview");
            //HtmlButton btnSaveManagement = (HtmlButton)this.ucManagementx1.FindControl("btnSaveManagement");
            //HtmlButton btnTSSaveManagement = (HtmlButton)this.ucHIVCETriage1.FindControl("btnTSSaveManagement");

            if (Request.QueryString["name"] == "Delete")
            {
                //btnSaveTriage.Visible = false;
                //btnSaveFindTriage.Visible = false;
                //btnDeleteForm.Visible = true;
            }
            else
            {
                //btnSaveTriage.Attributes["disabled"] = "true";
                //btnSaveFindTriage.Attributes["disabled"] = "true";
                //btnDeleteForm.Visible = false;
            }
            //btnSavePC.Attributes["disabled"] = "true";
            //btnSaveAddtionalHx.Attributes["disabled"] = "true";
            //btnSaveScreening.Attributes["disabled"] = "true";
            //btnSaveSystemicReview.Attributes["disabled"] = "true";
            //btnSaveManagement.Attributes["disabled"] = "true";

            //if (!object.Equals(btnTSSaveManagement, null))
            //{
            //    btnTSSaveManagement.Attributes["disabled"] = "true";
            //}


            if (dtTabId.Rows.Count > 0)
            {
                foreach (DataRow drTabsId in dtTabId.Rows)
                {
                    string tName = drTabsId["TabName"].ToString();
                    string tabName = string.Empty;
                    if (tName.ToLower().Contains("triage"))
                    {
                        tabName = "triage";
                    }
                    else
                    {
                        tabName = tName.ToLower();
                    }

                    if (tabName.ToLower().Contains("triage"))
                    {
                        if (drTabsId["TabName"].ToString().ToLower() == ("HIVCETriage").ToLower())
                        {
                            tabId = Convert.ToInt32(drTabsId["TabId"].ToString());
                        }
                    }
                    else
                    {
                        if (drTabsId["TabName"].ToString().ToLower() == tabName.ToLower())
                        {
                            tabId = Convert.ToInt32(drTabsId["TabId"].ToString());
                        }
                    }


                    if (visitId == 0)
                    {
                        enable = authMgr.HasFunctionRight(featureId, tabId, FunctionAccess.Add, (DataTable)System.Web.HttpContext.Current.Session["UserRight"]);
                    }

                    if (visitId > 0)
                    {
                        enable = authMgr.HasFunctionRight(featureId, tabId, FunctionAccess.Update, (DataTable)System.Web.HttpContext.Current.Session["UserRight"]);
                    }
                    if (Request.QueryString["name"] == "Delete")
                    {
                        if (authMgr.HasFunctionRight(featureId, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
                        {

                            int PatientID = Convert.ToInt32(Session["PatientId"]);
                            string theUrl = "";
                            theUrl = string.Format("{0}", "~/ClinicalForms/frmClinical_DeleteForm.aspx");
                            Response.Redirect(theUrl);
                        }
                        else if (authMgr.HasFunctionRight(featureId, FunctionAccess.Delete, (DataTable)Session["UserRight"]) == true)
                        {
                            // btnDeleteForm.Visible = true;
                        }
                        else if (authMgr.HasFunctionRight(featureId, FunctionAccess.Delete, (DataTable)Session["UserRight"]) == false)
                        {
                            //btnDeleteForm.Visible = true;
                            //btnDeleteForm.Attributes["disabled"] = "true";
                        }
                    }

                    if (tabName == "triage")
                    {
                        if (enable)
                        {
                            //btnSaveTriage.Attributes.Remove("disabled");
                            //btnSaveFindTriage.Attributes.Remove("disabled");
                            //if (!object.Equals(btnTSSaveManagement, null))
                            //{
                            //    btnTSSaveManagement.Attributes.Remove("disabled");
                            //}
                        }
                    }
                    else if (tabName == "presentingcomplaints")
                    {

                        if (enable)
                        {
                            // btnSavePC.Attributes.Remove("disabled");
                        }
                    }
                    else if (tabName == "addtionalhx")
                    {
                        if (enable)
                        {
                            // btnSaveAddtionalHx.Attributes.Remove("disabled");
                        }
                    }
                    else if (tabName == "screening")
                    {
                        if (enable)
                        {
                            // btnSaveScreening.Attributes.Remove("disabled");
                        }
                    }
                    else if (tabName == "systemicreview")
                    {
                        if (enable)
                        {
                            //  btnSaveSystemicReview.Attributes.Remove("disabled");
                        }
                    }
                    else if (tabName == "management")
                    {

                        if (enable)
                        {
                            // btnSaveManagement.Attributes.Remove("disabled");
                        }
                    }

                }

                if (Request.QueryString["name"] == "Delete")
                {
                    //btnSavePC.Attributes["disabled"] = "true";
                    //btnSaveAddtionalHx.Attributes["disabled"] = "true";
                    //btnSaveScreening.Attributes["disabled"] = "true";
                    //btnSaveSystemicReview.Attributes["disabled"] = "true";
                    //btnSaveManagement.Attributes["disabled"] = "true";
                    //if (!object.Equals(btnTSSaveManagement, null))
                    //{
                    //    btnTSSaveManagement.Attributes["disabled"] = "true";
                    //}
                }


                //if (Request.QueryString["name"] == "Delete")
                //{

                //    //lblDelete.Attributes.Add("class", "glyphicon glyphicon-floppy-remove");
                //    //lblDelete.Attributes.Add("style", "margin-left: -3%; margin-right: 2%;vertical-align: sub; color: #fff;");
                //}

                //if (Authentication.HasFunctionRight(ApplicationAccess.HEIForm, FunctionAccess.Print, (DataTable)Session["UserRight"]) == false)
                //{
                //    btnTriageClose.Enabled = false;
                //    btnTriagePrint.Enabled = false;

                //}
                //if (Authentication.HasFunctionRight(ApplicationAccess.HEIForm, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
                //{
                //    btnTriageSave.Enabled = false;
                //    btnTriageClose.Enabled = false;
                //}
                //if (Authentication.HasFunctionRight(ApplicationAccess.HEIForm, FunctionAccess.Update, (DataTable)Session["UserRight"]) == false)
                //{
                //    btnTriageSave.Enabled = false;
                //    btnHTCSave.Enabled = false;
                //    btnProfileSave.Enabled = false;
                //    btnSaveClinicalReview.Enabled = false;
                //    btnSavePMTCT.Enabled = false;
                //}
                //else if (Request.QueryString["name"] == "Delete")
                //{
                //    if (Authentication.HasFunctionRight(ApplicationAccess.HEIForm, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
                //    {

                //        int PatientID = Convert.ToInt32(Session["PatientId"]);
                //        string theUrl = "";
                //        theUrl = string.Format("{0}", "frmClinical_DeleteForm.aspx");
                //        Response.Redirect(theUrl);
                //    }
                //    else if (Authentication.HasFunctionRight(ApplicationAccess.HEIForm, FunctionAccess.Delete, (DataTable)Session["UserRight"]) == false)
                //    {
                //        btnTriageSave.Text = "Delete";
                //        btnTriageSave.Enabled = false;
                //        btnHTCSave.Enabled = false;
                //        btnProfileSave.Enabled = false;
                //        btnSaveClinicalReview.Enabled = false;
                //        btnSavePMTCT.Enabled = false;
                //    }
                //}

            }
        }

        public string DeleteForm()
        {
            string result = string.Empty;
            try
            {
                IinitialFollowupVisit InitialFollowupvisit;
                int theResultRow, OrderNo;
                string FormName;
                OrderNo = Convert.ToInt32(Session["PatientVisitId"].ToString());
                FormName = "Clinical Encounter";

                InitialFollowupvisit = (IinitialFollowupVisit)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BInitialFollowupVisit, BusinessProcess.Clinical");
                theResultRow = (int)InitialFollowupvisit.DeleteInitialFollowupVisitForm(FormName, OrderNo, Convert.ToInt32(Session["PatientId"].ToString()), Convert.ToInt32(Session["AppUserId"].ToString()));
                if (theResultRow == 0)
                {
                    result = EnumUtil.GetEnumDescription(Success.False);
                }
                else
                {
                    //string theUrl;
                    //theUrl = string.Format("{0}", "frmPatient_Home.aspx?Func=Delete");
                    //Response.Redirect(theUrl);
                    result = EnumUtil.GetEnumDescription(Success.True);
                }

            }
            catch (Exception ex)
            {
                CLogger.WriteLog(ELogLevel.ERROR, "DeleteForm() exception: " + ex.ToString());
                result = EnumUtil.GetEnumDescription(Success.False);
            }
            return result;

        }

        private string GetPrEPTriage(int ptn_pk, int visitPK, int locationId)
        {
            string result = string.Empty;
            try
            {

                IQCareUtils theUtils = new IQCareUtils();
                DataSet theDSXML = new DataSet();
                theDSXML.ReadXml(MapPath("..\\XMLFiles\\AllMasters.con"));
                double age = Convert.ToDouble(Session["patientageinyearmonth"].ToString());
                List<CodeDeCode> collections = new List<CodeDeCode>();

                DataView theCodeDV = new DataView(theDSXML.Tables["MST_CODE"]);
                theCodeDV.RowFilter = "DeleteFlag=0 and Name in ('KeyPopulation')";
                DataTable theCodeDT = (DataTable)theUtils.CreateTableFromDataView(theCodeDV);
                DataTable theDT = new DataTable();
                if (theCodeDT.Rows.Count > 0)
                {
                    foreach (DataRow row in theCodeDT.Rows)
                    {

                        DataView theDV = new DataView(theDSXML.Tables["MST_DECODE"]);
                        theDV.RowFilter = "DeleteFlag=0 and CodeID=" + row["CodeId"] + "";

                        theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                        List<CodeDeCode> collections1 = new List<CodeDeCode>();
                        collections1 = (from dt in theDT.AsEnumerable()
                                        select new CodeDeCode()
                                        {
                                            CodeId = dt.Field<int>("codeid"),
                                            CodeName = dt.Field<string>("CodeName"),
                                            DeCodeId = dt.Field<int>("Id"),
                                            DeCodeName = dt.Field<string>("NAME")
                                        }).ToList();

                        collections.AddRange(collections1);
                    }
                }

                IPrEP iPrep = (IPrEP)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPrEP, BusinessProcess.Clinical");
                Entities.Common.PrEP prep = iPrep.GetPrEPTriageData(ptn_pk, visitPK, locationId);
                if (collections.Count > 0)
                {
                    prep.DropDownValues.AddRange(collections);
                }

                theCodeDV = new DataView(oCommonData.getAllEmployees());
                theCodeDT = (DataTable)theUtils.CreateTableFromDataView(theCodeDV);
                if (theCodeDT.Rows.Count > 0)
                {
                    foreach (DataRow row in theCodeDT.Rows)
                    {
                        UserDesignation ud = new UserDesignation();
                        ud.UserID = Convert.ToInt32(row["userid"].ToString());
                        ud.UserName = row["UserName"].ToString();
                        ud.Designation = row["Designation"].ToString();
                        ud.DeleteFlag = Convert.ToInt32(row["DeleteFlag"].ToString());

                        prep.UserDesignation.Add(ud);
                    }
                }

                string facilitylist = SerializerUtil.ConverToJson<List<Entities.Common.Facility>>(prep.FacilityList);
                Session["FacilityList"] = facilitylist;

                result = SerializerUtil.ConverToJson<Entities.Common.PrEP>(prep);
            }
            catch (Exception ex)
            {
                CLogger.WriteLog(ELogLevel.ERROR, "GetPrEPTriageData() exception: " + ex.ToString());
                ResponseType response = new ResponseType() { Success = EnumUtil.GetEnumDescription(Success.False) };
                result = SerializerUtil.ConverToJson<ResponseType>(response);
            }
            finally
            {

            }
            return result;
        }

        private string GetFacilities(int ptn_pk, int visitPK, int locationId)
        {
            string result = string.Empty;
            try
            {
                if (object.Equals(Session["FacilityList"], string.Empty))
                {

                    IPrEP iPrepFacility = (IPrEP)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPrEP, BusinessProcess.Clinical");
                    Entities.Common.PrEP pfl = iPrepFacility.GetPrEPTriageData(ptn_pk, visitPK, locationId);


                    string facilitylist = SerializerUtil.ConverToJson<List<Entities.Common.Facility>>(pfl.FacilityList);

                    Session["FacilityList"] = facilitylist;
                    result = facilitylist;
                }
                else
                {
                    result = Session["FacilityList"].ToString();
                }
            }
            catch (Exception ex)
            {
                CLogger.WriteLog(ELogLevel.ERROR, "GetPrEPFacility() exception: " + ex.ToString());
                ResponseType response = new ResponseType() { Success = EnumUtil.GetEnumDescription(Success.False) };
                result = SerializerUtil.ConverToJson<ResponseType>(response);
            }
            finally
            {

            }
            return result;
        }

        private string GetPrEPAssessment(int ptn_pk, int visitPK, int locationId)
        {
            string result = string.Empty;
            try
            {
                IPrEP iPrep = (IPrEP)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPrEP, BusinessProcess.Clinical");
                Entities.Common.PrEP assessment = iPrep.GetPrEPAssessmentData(ptn_pk, visitPK, locationId);
                result = SerializerUtil.ConverToJson<Entities.Common.PrEP>(assessment);

                //if (visitPK > 0)
                //{

                //    IPrEP iPrep = (IPrEP)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPrEP, BusinessProcess.Clinical");
                //    Entities.Common.PrEP assessment = iPrep.GetPrEPAssessmentData(ptn_pk, visitPK, locationId);
                //    result = SerializerUtil.ConverToJson<Entities.Common.PrEP>(assessment);
                    
                //}
                //else
                //{
                //    ResponseType response = new ResponseType() { Success = EnumUtil.GetEnumDescription(Success.False) };
                //    result = SerializerUtil.ConverToJson<ResponseType>(response);
                //}
            }
            catch (Exception ex)
            {
                ///CLogger.WriteLog(ELogLevel.ERROR, "GetAnnouncements() exception: " + ex.ToString());
                ResponseType response = new ResponseType() { Success = EnumUtil.GetEnumDescription(Success.False) };
                result = SerializerUtil.ConverToJson<ResponseType>(response);
            }
            finally
            {

            }
            return result;
        }

        private string SavePrEPTriage(string nodeJson, int ptn_pk, int visitPK, int locationId, int userId)
        {
            string result = string.Empty;
            ResponseType ObjResponse = new ResponseType();
            try
            {
                Entities.Common.PrEPTriage triage = SerializerUtil.ConverToObject<Entities.Common.PrEPTriage>(nodeJson);
                IPrEP iPrep = (IPrEP)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPrEP, BusinessProcess.Clinical");

                triage.Ptn_pk = ptn_pk;
                triage.LocationId = locationId;
                triage.Visit_Id = visitPK;


                //result = SerializerUtil.ConverToJson<Triage>(triage);
                int flag = 0;
                int visitId;
                flag = iPrep.SaveUpdatePrEPTriage(triage, visitPK, locationId, userId);

                Session["PatientVisitId"] = visitPK.ToString();
                this.hidVId.Value = visitPK.ToString();

                if (flag != 0)
                {
                    ObjResponse.Success = EnumUtil.GetEnumDescription(Success.True);
                }
                else
                {
                    ObjResponse.Success = EnumUtil.GetEnumDescription(Success.False);
                }

            }
            catch (Exception ex)
            {
                if (ex.Message.ToLower().Contains("visit already exists"))
                {
                    ObjResponse.ErrorMessage = ex.Message;
                }
                ObjResponse.Success = EnumUtil.GetEnumDescription(Success.False);
            }
            finally
            {
            }

            result = SerializerUtil.ConverToJson<ResponseType>(ObjResponse);
            return result;
        }

        private string SavePresentingComplaintsData(string nodeJson, int ptn_pk, int visitPK, int locationId, int userId)
        {
            string result = string.Empty;
            ResponseType ObjResponse = new ResponseType();
            //try
            //{
            //    HIVCE.Common.Entities.PresentingComplaint obj = SerializerUtil.ConverToObject<HIVCE.Common.Entities.PresentingComplaint>(nodeJson);
            //    IClinicalEncounter clinicalencounter = (IClinicalEncounter)ObjectFactory.CreateInstance("HIVCE.BusinessLayer.BLClinicalEncounter, HIVCE.BusinessLayer");
            //    obj.Ptn_pk = ptn_pk;
            //    obj.Visit_Id = visitPK;
            //    obj.LocationId = locationId;

            //    //result = SerializerUtil.ConverToJson<PresentingComplaint>(obj);

            //    bool flag = clinicalencounter.SaveUpdatePresentingComplaintsData(obj, userId, locationId);
            //    if (flag)
            //    {
            //        ObjResponse.Success = EnumUtil.GetEnumDescription(Success.True);
            //    }
            //    else
            //    {
            //        ObjResponse.Success = EnumUtil.GetEnumDescription(Success.False);
            //    }

            //}
            //catch (Exception ex)
            //{

            //    ObjResponse.Success = EnumUtil.GetEnumDescription(Success.False);
            //}
            //finally
            //{
            //}

            result = SerializerUtil.ConverToJson<ResponseType>(ObjResponse);
            return result;
        }

        private string GetZScoreDetails(int ptn_pk, int visitPK, int locationId)
        {
            string result = string.Empty;
            try
            {
                IPrEP iPrepFacility = (IPrEP)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPrEP, BusinessProcess.Clinical");
                Entities.Common.PrEP pz = iPrepFacility.GetPrEPTriageData(ptn_pk, visitPK, locationId);

                DataSet zscoreds = new DataSet();
                IKNHStaticForms knhs = (IKNHStaticForms)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHStaticForms, BusinessProcess.Clinical");
                string height = string.IsNullOrEmpty(pz.PatientVitals.Height) == true ? "0" : pz.PatientVitals.Height;
                zscoreds = knhs.GetZScoreNewImplementation(ptn_pk, Session["patientsex"].ToString(), height.ToString());

                ZScoreDetails zs = new ZScoreDetails();
                zs.WFA = new ZScore();
                zs.WFH = new ZScore();
                zs.BMIz = new ZScore();
                if (zscoreds.Tables[0].Rows.Count > 0)
                {
                    DataColumnCollection columns = zscoreds.Tables[0].Columns;
                    if (columns.Contains("L"))
                    {
                        zs.WFA.L = Convert.ToDouble(zscoreds.Tables[0].Rows[0]["L"].ToString());
                        zs.WFA.M = Convert.ToDouble(zscoreds.Tables[0].Rows[0]["M"].ToString());
                        zs.WFA.S = Convert.ToDouble(zscoreds.Tables[0].Rows[0]["S"].ToString());
                    }
                }

                if (zscoreds.Tables[1].Rows.Count > 0)
                {
                    DataColumnCollection columns = zscoreds.Tables[1].Columns;
                    if (columns.Contains("L"))
                    {
                        zs.WFH.L = Convert.ToDouble(zscoreds.Tables[1].Rows[0]["L"].ToString());
                        zs.WFH.M = Convert.ToDouble(zscoreds.Tables[1].Rows[0]["M"].ToString());
                        zs.WFH.S = Convert.ToDouble(zscoreds.Tables[1].Rows[0]["S"].ToString());
                    }
                }

                if (zscoreds.Tables[2].Rows.Count > 0)
                {
                    DataColumnCollection columns = zscoreds.Tables[2].Columns;
                    if (columns.Contains("L"))
                    {
                        zs.BMIz.L = Convert.ToDouble(zscoreds.Tables[2].Rows[0]["L"].ToString());
                        zs.BMIz.M = Convert.ToDouble(zscoreds.Tables[2].Rows[0]["M"].ToString());
                        zs.BMIz.S = Convert.ToDouble(zscoreds.Tables[2].Rows[0]["S"].ToString());
                    }
                }

                zscoreds.Dispose();

                result = SerializerUtil.ConverToJson<Entities.Common.ZScoreDetails>(zs);
            }
            catch (Exception ex)
            {
                CLogger.WriteLog(ELogLevel.ERROR, "getzscoredetails() exception: " + ex.ToString());
                ResponseType response = new ResponseType() { Success = EnumUtil.GetEnumDescription(Success.False) };
                result = SerializerUtil.ConverToJson<ResponseType>(response);
            }
            finally
            {

            }
            return result;
        }

        private void SendResponse(string data)
        {
            Response.Clear();
            Response.ContentType = "application/json";
            Response.AddHeader("Content-type", "text/json");
            Response.AddHeader("Content-type", "application/json");
            Response.Write(data);
            Response.End();
        }
    }
}