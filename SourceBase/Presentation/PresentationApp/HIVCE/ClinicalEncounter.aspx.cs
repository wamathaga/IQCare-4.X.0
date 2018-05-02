using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HIVCE.BusinessLayer;
using HIVCE.Common;
using System.Configuration;
using HIVCE.Common.Entities;
using Application.Presentation;
using System.Data;
using Interface.HIVCE;
using Interface.Service;
using Application.Common;
using Interface.Clinical;
using System.Web.UI.HtmlControls;
using System.Web.Services;

namespace HIVCE.Presentation
{
    public partial class ClinicalEncounter : System.Web.UI.Page
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
                    if (Request.QueryString["data"].ToString() == "getdata")
                    {
                        tabName = "triage";
                    }
                    if (Request.QueryString["data"].ToString() == "getpcdata")
                    {
                        tabName = "presentingcomplaints";
                    }
                    if (Request.QueryString["data"].ToString() == "getAddtionalHx")
                    {
                        tabName = "addtionalhx";
                    }
                    if (Request.QueryString["data"].ToString() == "getsystemicreview")
                    {
                        tabName = "systemicreview";

                    }
                    if (Request.QueryString["data"].ToString() == "getscreeningdata")
                    {
                        tabName = "screening";
                    }
                    if (Request.QueryString["data"].ToString() == "getmgtdata")
                    {
                        tabName = "management";
                    }

                    if (Request.QueryString["data"].ToString() == "getzscore")
                    {
                        tabName = "triage";
                    }

                    if (Request.QueryString["data"].ToString() == "geticd10")
                    {
                        tabName = "management";
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

                    if (Request.QueryString["data"].ToString() == "getdata")
                    {
                        response = GetClinicalEnounter(Convert.ToInt32(PatientId), visitPK, locationId);
                        SendResponse(response);
                    }
                    if (Request.QueryString["data"].ToString() == "getpcdata")
                    {
                        response = GetPresentingComplaints(Convert.ToInt32(PatientId), visitPK, locationId);
                        SendResponse(response);
                    }
                    if (Request.QueryString["data"].ToString() == "getAddtionalHx")
                    {
                        response = GetAddtionalHx(Convert.ToInt32(PatientId), visitPK, locationId);
                        SendResponse(response);
                    }
                    if (Request.QueryString["data"].ToString() == "getsystemicreview")
                    {
                        response = GetSystemicReview(Convert.ToInt32(PatientId), visitPK, locationId);
                        SendResponse(response);
                    }
                    if (Request.QueryString["data"].ToString() == "getscreeningdata")
                    {
                        response = GetScreeningData(Convert.ToInt32(PatientId), visitPK, locationId);
                        SendResponse(response);
                    }
                    if (Request.QueryString["data"].ToString() == "getmgtdata")
                    {
                        Session["ICD10"] = string.Empty;
                        response = GetManagementData(Convert.ToInt32(PatientId), visitPK, locationId);
                        SendResponse(response);
                    }

                    if (Request.QueryString["data"].ToString() == "getzscore")
                    {
                        response = GetZScoreDetails(Convert.ToInt32(PatientId), visitPK, locationId);
                        SendResponse(response);
                    }

                    if (Request.QueryString["data"].ToString() == "geticd10")
                    {
                        response = GetICD10Values(Convert.ToInt32(PatientId), visitPK, locationId);
                        SendResponse(response);
                    }

                    if (Request.QueryString["data"].ToString() == "getfacility")
                    {
                        response = GetFacilities(Convert.ToInt32(PatientId), visitPK, locationId);
                        SendResponse(response);
                    }

                    if (Request.QueryString["data"].ToString() == "savetriage")
                    {

                        System.IO.StreamReader sr = new System.IO.StreamReader(Request.InputStream);
                        string jsonString = "";
                        jsonString = sr.ReadToEnd();

                        response = SaveTriageData(jsonString, PatientId, visitPK, locationId, userId);
                        SendResponse(response);
                    }

                    if (Request.QueryString["data"].ToString() == "savepc")
                    {

                        System.IO.StreamReader sr = new System.IO.StreamReader(Request.InputStream);
                        string jsonString = "";
                        jsonString = sr.ReadToEnd();

                        response = SavePresentingComplaintsData(jsonString, PatientId, visitPK, locationId, userId);
                        SendResponse(response);
                    }
                    if (Request.QueryString["data"].ToString() == "saveadx")
                    {

                        System.IO.StreamReader sr = new System.IO.StreamReader(Request.InputStream);
                        string jsonString = "";
                        jsonString = sr.ReadToEnd();

                        response = SaveAdditionalHx(jsonString, PatientId, visitPK, locationId, userId);
                        SendResponse(response);
                    }

                    if (Request.QueryString["data"].ToString() == "savescreening")
                    {

                        System.IO.StreamReader sr = new System.IO.StreamReader(Request.InputStream);
                        string jsonString = "";
                        jsonString = sr.ReadToEnd();

                        response = SaveScreeningData(jsonString, PatientId, visitPK, locationId, userId);
                        SendResponse(response);
                    }

                    if (Request.QueryString["data"].ToString() == "savesystemicreview")
                    {

                        System.IO.StreamReader sr = new System.IO.StreamReader(Request.InputStream);
                        string jsonString = "";
                        jsonString = sr.ReadToEnd();

                        response = SaveSystemicReviewData(jsonString, PatientId, visitPK, locationId, userId);
                        SendResponse(response);
                    }

                    if (Request.QueryString["data"].ToString() == "savemanagementx")
                    {

                        System.IO.StreamReader sr = new System.IO.StreamReader(Request.InputStream);
                        string jsonString = "";
                        jsonString = sr.ReadToEnd();

                        response = SaveManagementxData(jsonString, PatientId, visitPK, locationId, userId);
                        SendResponse(response);
                    }

                    if (Request.QueryString["data"].ToString() == "deleteform")
                    {
                        response = DeleteForm();
                        SendResponse(response);
                    }

                    //
                    if (Request.QueryString["data"].ToString() == "savetstriage")
                    {

                        System.IO.StreamReader sr = new System.IO.StreamReader(Request.InputStream);
                        string jsonString = "";
                        jsonString = sr.ReadToEnd();

                        response = SaveTSTriageData(jsonString, PatientId, visitPK, locationId, userId);
                        SendResponse(response);
                    }
                }
            }
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

            HtmlButton btnSaveTriage = (HtmlButton)this.ucHIVCETriage1.FindControl("btnSaveTriage");
            HtmlButton btnSaveFindTriage = (HtmlButton)this.ucHIVCETriage1.FindControl("btnSaveFindTriage");

            HtmlButton btnDeleteForm = (HtmlButton)this.ucHIVCETriage1.FindControl("btnDeleteForm");

            HtmlButton btnSavePC = (HtmlButton)this.ucPresentingComplaints1.FindControl("btnSavePC");
            HtmlButton btnSaveAddtionalHx = (HtmlButton)this.ucAddtionalHx1.FindControl("btnSaveAddtionalHx");
            HtmlButton btnSaveScreening = (HtmlButton)this.ucScreening1.FindControl("btnSaveScreening");
            HtmlButton btnSaveSystemicReview = (HtmlButton)this.ucSystemicReview1.FindControl("btnSaveSystemicReview");
            HtmlButton btnSaveManagement = (HtmlButton)this.ucManagementx1.FindControl("btnSaveManagement");
            HtmlButton btnTSSaveManagement = (HtmlButton)this.ucHIVCETriage1.FindControl("btnTSSaveManagement");

            if (Request.QueryString["name"] == "Delete")
            {
                btnSaveTriage.Visible = false;
                btnSaveFindTriage.Visible = false;
                btnDeleteForm.Visible = true;
            }
            else
            {
                btnSaveTriage.Attributes["disabled"] = "true";
                btnSaveFindTriage.Attributes["disabled"] = "true";
                btnDeleteForm.Visible = false;
            }
            btnSavePC.Attributes["disabled"] = "true";
            btnSaveAddtionalHx.Attributes["disabled"] = "true";
            btnSaveScreening.Attributes["disabled"] = "true";
            btnSaveSystemicReview.Attributes["disabled"] = "true";
            btnSaveManagement.Attributes["disabled"] = "true";

            if (!object.Equals(btnTSSaveManagement, null))
            {
                btnTSSaveManagement.Attributes["disabled"] = "true";
            }


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
                            btnDeleteForm.Visible = true;
                        }
                        else if (authMgr.HasFunctionRight(featureId, FunctionAccess.Delete, (DataTable)Session["UserRight"]) == false)
                        {
                            btnDeleteForm.Visible = true;
                            btnDeleteForm.Attributes["disabled"] = "true";
                        }
                    }

                    if (tabName == "triage")
                    {
                        if (enable)
                        {
                            btnSaveTriage.Attributes.Remove("disabled");
                            btnSaveFindTriage.Attributes.Remove("disabled");
                            if (!object.Equals(btnTSSaveManagement, null))
                            {
                                btnTSSaveManagement.Attributes.Remove("disabled");
                            }
                        }
                    }
                    else if (tabName == "presentingcomplaints")
                    {

                        if (enable)
                        {
                            btnSavePC.Attributes.Remove("disabled");
                        }
                    }
                    else if (tabName == "addtionalhx")
                    {
                        if (enable)
                        {
                            btnSaveAddtionalHx.Attributes.Remove("disabled");
                        }
                    }
                    else if (tabName == "screening")
                    {
                        if (enable)
                        {
                            btnSaveScreening.Attributes.Remove("disabled");
                        }
                    }
                    else if (tabName == "systemicreview")
                    {
                        if (enable)
                        {
                            btnSaveSystemicReview.Attributes.Remove("disabled");
                        }
                    }
                    else if (tabName == "management")
                    {

                        if (enable)
                        {
                            btnSaveManagement.Attributes.Remove("disabled");
                        }
                    }

                }

                if (Request.QueryString["name"] == "Delete")
                {
                    btnSavePC.Attributes["disabled"] = "true";
                    btnSaveAddtionalHx.Attributes["disabled"] = "true";
                    btnSaveScreening.Attributes["disabled"] = "true";
                    btnSaveSystemicReview.Attributes["disabled"] = "true";
                    btnSaveManagement.Attributes["disabled"] = "true";
                    if (!object.Equals(btnTSSaveManagement, null))
                    {
                        btnTSSaveManagement.Attributes["disabled"] = "true";
                    }
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

        private string GetClinicalEnounter(int ptn_pk, int visitPK, int locationId)
        {
            string result = string.Empty;
            try
            {
                IClinicalEncounter clinicalencounter = (IClinicalEncounter)ObjectFactory.CreateInstance("HIVCE.BusinessLayer.BLClinicalEncounter, HIVCE.BusinessLayer");
                HIVCE.Common.Entities.ClinicalEncounter cl = clinicalencounter.GetData(ptn_pk, visitPK, locationId);

                string facilitylist = SerializerUtil.ConverToJson<List<HIVCE.Common.Entities.Facility>>(cl.FacilityList);

                Session["FacilityList"] = facilitylist;

                result = SerializerUtil.ConverToJson<HIVCE.Common.Entities.ClinicalEncounter>(cl);
            }
            catch (Exception ex)
            {
                CLogger.WriteLog(ELogLevel.ERROR, "GetClinicalEnounter() exception: " + ex.ToString());
                ResponseType response = new ResponseType() { Success = EnumUtil.GetEnumDescription(Success.False) };
                result = SerializerUtil.ConverToJson<ResponseType>(response);
            }
            finally
            {

            }
            return result;
        }

        private string GetPresentingComplaints(int ptn_pk, int visitPK, int locationId)
        {
            string result = string.Empty;
            try
            {
                if (visitPK > 0)
                {
                    IClinicalEncounter clinicalencounter = (IClinicalEncounter)ObjectFactory.CreateInstance("HIVCE.BusinessLayer.BLClinicalEncounter, HIVCE.BusinessLayer");
                    HIVCE.Common.Entities.PresentingComplaint obj = clinicalencounter.GetPresentingComplaints(ptn_pk, visitPK, locationId);
                    result = SerializerUtil.ConverToJson<HIVCE.Common.Entities.PresentingComplaint>(obj);
                    //result = bl.GetPresentingComplaints(ptn_pk, visitPK, locationId);
                }
                else
                {
                    ResponseType response = new ResponseType() { Success = EnumUtil.GetEnumDescription(Success.False) };
                    result = SerializerUtil.ConverToJson<ResponseType>(response);
                }
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

        private string GetAddtionalHx(int ptn_pk, int visitPK, int locationId)
        {
            string result = string.Empty;
            try
            {
                if (visitPK > 0)
                {
                    IClinicalEncounter clinicalencounter = (IClinicalEncounter)ObjectFactory.CreateInstance("HIVCE.BusinessLayer.BLClinicalEncounter, HIVCE.BusinessLayer");
                    HIVCE.Common.Entities.AddtionalHx obj = clinicalencounter.GetAddtionalHx(ptn_pk, visitPK, locationId);
                    result = SerializerUtil.ConverToJson<HIVCE.Common.Entities.AddtionalHx>(obj);
                    //result = bl.GetAddtionalHx(ptn_pk, visitPK, locationId);
                }
                else
                {
                    ResponseType response = new ResponseType() { Success = EnumUtil.GetEnumDescription(Success.False) };
                    result = SerializerUtil.ConverToJson<ResponseType>(response);
                }

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

        private string GetSystemicReview(int ptn_pk, int visitPK, int locationId)
        {
            string result = string.Empty;
            try
            {
                if (visitPK > 0)
                {

                    IQCareUtils theUtils = new IQCareUtils();
                    //DataSet theDSXML = new DataSet();
                    //theDSXML.ReadXml(MapPath("..\\XMLFiles\\AllMasters.con"));
                    //DataView theCodeDV = new DataView(theDSXML.Tables["MST_CODE"]);
                    DataView theCodeDV = new DataView(oCommonData.getAllMSTCode());
                    theCodeDV.RowFilter = "DeleteFlag=0 and Name in ('WHOStageIConditions','WHOStageIIConditions','WHOStageIIICoditions','WHOStageIVConditions','WHO Stage','GeneralConditions','SkinConditions','ENTConditions','ChestLungsConditions','CardiovascularConditions','AbdomenConditions','CNSConditions','GenitalUrinaryConditions')";
                    DataTable theCodeDT = (DataTable)theUtils.CreateTableFromDataView(theCodeDV);
                    DataTable theDT = new DataTable();
                    if (theCodeDT.Rows.Count > 0)
                    {
                        List<CodeDeCodeTables> WhoStages = new List<CodeDeCodeTables>();
                        foreach (DataRow row in theCodeDT.Rows)
                        {

                            DataView theDV = new DataView(oCommonData.getMSTDecode(Convert.ToInt32(row["CodeId"])));
                            //theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and CodeID=" + row["CodeId"];

                            theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                            List<CodeDeCodeTables> WhoStages1 = new List<CodeDeCodeTables>();


                            WhoStages1 = (from dt in theDT.AsEnumerable()
                                          select new CodeDeCodeTables()
                                          {
                                              CodeId = Convert.ToInt32(row["CodeId"]),
                                              CodeName = Convert.ToString(row["Name"]),
                                              DeCodeId = dt.Field<int>("Id"),
                                              DeCodeName = dt.Field<string>("NAME")
                                          }).ToList();

                            WhoStages.AddRange(WhoStages1);
                        }

                        IClinicalEncounter clinicalencounter = (IClinicalEncounter)ObjectFactory.CreateInstance("HIVCE.BusinessLayer.BLClinicalEncounter, HIVCE.BusinessLayer");
                        HIVCE.Common.Entities.DBSystemicReviewData obj = clinicalencounter.GetSystemicReviewData(ptn_pk, visitPK, locationId);
                        obj.Collections = new List<CodeDeCodeTables>();
                        if (WhoStages.Count > 0)
                        {
                            obj.Collections = WhoStages;
                        }
                        result = SerializerUtil.ConverToJson<HIVCE.Common.Entities.DBSystemicReviewData>(obj);


                    }
                }
                else
                {
                    ResponseType response = new ResponseType() { Success = EnumUtil.GetEnumDescription(Success.False) };
                    result = SerializerUtil.ConverToJson<ResponseType>(response);
                }
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

        private string GetManagementData(int ptn_pk, int visitPK, int locationId)
        {
            string result = string.Empty;
            try
            {
                if (visitPK > 0)
                {
                    IQCareUtils theUtils = new IQCareUtils();
                    DataSet theDSXML = new DataSet();
                    theDSXML.ReadXml(MapPath("..\\XMLFiles\\AllMasters.con"));
                    double age = Convert.ToDouble(Session["patientageinyearmonth"].ToString());
                    List<CodeDeCodeTables> collections = new List<CodeDeCodeTables>();

                    DataView theCodeDV = new DataView(theDSXML.Tables["MST_CODE"]);
                    theCodeDV.RowFilter = "DeleteFlag=0 and Name in ('ShortTermEffects','LongTermEffects','RememberingMedications','KeyPopulation','ARV Eligibility')";
                    DataTable theCodeDT = (DataTable)theUtils.CreateTableFromDataView(theCodeDV);
                    DataTable theDT = new DataTable();
                    if (theCodeDT.Rows.Count > 0)
                    {
                        foreach (DataRow row in theCodeDT.Rows)
                        {

                            DataView theDV = new DataView(theDSXML.Tables["MST_DECODE"]);
                            theDV.RowFilter = "DeleteFlag=0 and CodeID=" + row["CodeId"] + "";

                            theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                            List<CodeDeCodeTables> collections1 = new List<CodeDeCodeTables>();


                            collections1 = (from dt in theDT.AsEnumerable()
                                            select new CodeDeCodeTables()
                                            {
                                                CodeId = dt.Field<int>("codeid"),
                                                CodeName = dt.Field<string>("CodeName"),
                                                DeCodeId = dt.Field<int>("Id"),
                                                DeCodeName = dt.Field<string>("NAME")
                                            }).ToList();

                            collections.AddRange(collections1);
                        }
                    }

                    IClinicalEncounter clinicalencounter = (IClinicalEncounter)ObjectFactory.CreateInstance("HIVCE.BusinessLayer.BLClinicalEncounter, HIVCE.BusinessLayer");
                    HIVCE.Common.Entities.Management obj = clinicalencounter.GetManagement(ptn_pk, visitPK, locationId);

                    if (obj.DropdownValues.Count > 0)
                    {
                        if (collections.Count > 0)
                        {
                            obj.DropdownValues.AddRange(collections);
                        }
                    }

                    theCodeDV = new DataView(oCommonData.getAllEmployees());
                    theCodeDT = (DataTable)theUtils.CreateTableFromDataView(theCodeDV);
                    theDT = new DataTable();
                    if (theCodeDT.Rows.Count > 0)
                    {
                        foreach (DataRow row in theCodeDT.Rows)
                        {
                            UserDesignation ud = new UserDesignation();
                            ud.UserID = Convert.ToInt32(row["userid"].ToString());
                            ud.UserName = row["UserName"].ToString();
                            ud.Designation = row["Designation"].ToString();
                            ud.DeleteFlag = Convert.ToInt32(row["DeleteFlag"].ToString());

                            obj.Signature.Add(ud);
                        }
                    }

                    string icd10 = SerializerUtil.ConverToJson<List<HIVCE.Common.Entities.ICD>>(obj.ICDDropdownValues);

                    Session["ICD10"] = icd10;
                    result = SerializerUtil.ConverToJson<HIVCE.Common.Entities.Management>(obj);
                }
                else
                {
                    ResponseType response = new ResponseType() { Success = EnumUtil.GetEnumDescription(Success.False) };
                    result = SerializerUtil.ConverToJson<ResponseType>(response);
                }
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

        private string GetScreeningData(int ptn_pk, int visitPK, int locationId)
        {
            string result = string.Empty;
            try
            {
                if (visitPK > 0)
                {
                    IQCareUtils theUtils = new IQCareUtils();
                    double age = Convert.ToDouble(Session["patientageinyearmonth"].ToString());
                    List<CodeDeCodeTables> collections = new List<CodeDeCodeTables>();
                    DataSet theDSXML = new DataSet();
                    theDSXML.ReadXml(MapPath("..\\XMLFiles\\AllMasters.con"));

                    DataView theCodeDV = new DataView(oCommonData.getAllMSTCode());
                    if (age <= 15)
                    {
                        theCodeDV.RowFilter = "DeleteFlag=0 and Name in ('TBICFPaeds','TBFindings','IPT','CXR','TBRegimen','SputumSmear','GeneExpert','SputumDST','IPTAdherence','IPTContraindication','IPTDiscontinued')";
                    }
                    else
                    {
                        theCodeDV.RowFilter = "DeleteFlag=0 and Name in ('TBAssessmentICF','TBFindings','IPT','CXR','TBRegimen','SputumSmear','GeneExpert','SputumDST','IPTAdherence','IPTContraindication','IPTDiscontinued')";
                    }
                    DataTable theCodeDT = (DataTable)theUtils.CreateTableFromDataView(theCodeDV);
                    DataTable theDT = new DataTable();
                    if (theCodeDT.Rows.Count > 0)
                    {
                        foreach (DataRow row in theCodeDT.Rows)
                        {

                            //DataView theDV = new DataView(theDSXML.Tables["MST_DECODE"]);
                            DataView theDV = new DataView(oCommonData.getMSTDecode(Convert.ToInt32(row["CodeId"])));
                            theDV.RowFilter = "DeleteFlag=0 and CodeID=" + row["CodeId"] + " and Name Not in ('Transferred out')";

                            theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                            List<CodeDeCodeTables> collections1 = new List<CodeDeCodeTables>();


                            collections1 = (from dt in theDT.AsEnumerable()
                                            select new CodeDeCodeTables()
                                            {
                                                CodeId = Convert.ToInt32(row["CodeId"]),
                                                CodeName = Convert.ToString(row["Name"]),
                                                DeCodeId = dt.Field<int>("Id"),
                                                DeCodeName = dt.Field<string>("NAME")
                                            }).ToList();

                            collections.AddRange(collections1);
                        }
                    }

                    theCodeDV = new DataView(theDSXML.Tables["MST_BLUECODE"]);
                    theCodeDV.RowFilter = "DeleteFlag=0 and Name in ('TB Status')";
                    theCodeDT = (DataTable)theUtils.CreateTableFromDataView(theCodeDV);
                    theDT = new DataTable();
                    if (theCodeDT.Rows.Count > 0)
                    {

                        foreach (DataRow row in theCodeDT.Rows)
                        {

                            DataView theDV = new DataView(theDSXML.Tables["MST_BLUEDECODE"]);
                            theDV.RowFilter = "DeleteFlag=0 and CodeID=" + row["CodeId"] + "";

                            theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                            List<CodeDeCodeTables> collections1 = new List<CodeDeCodeTables>();


                            collections1 = (from dt in theDT.AsEnumerable()
                                            select new CodeDeCodeTables()
                                            {
                                                CodeId = dt.Field<int>("codeid"),
                                                CodeName = "TBStatus",
                                                DeCodeId = dt.Field<int>("Id"),
                                                DeCodeName = GetTBStatusName(dt.Field<string>("NAME"))
                                            }).ToList();

                            collections.AddRange(collections1);
                        }
                    }

                    IClinicalEncounter clinicalencounter = (IClinicalEncounter)ObjectFactory.CreateInstance("HIVCE.BusinessLayer.BLClinicalEncounter, HIVCE.BusinessLayer");
                    HIVCE.Common.Entities.DBScreeningData obj = clinicalencounter.GetScreeningData(ptn_pk, visitPK, locationId);

                    obj.Collections = new List<CodeDeCodeTables>();

                    if (collections.Count > 0)
                    {
                        obj.Collections = collections;
                    }
                    result = SerializerUtil.ConverToJson<HIVCE.Common.Entities.DBScreeningData>(obj);

                }
                else
                {
                    ResponseType response = new ResponseType() { Success = EnumUtil.GetEnumDescription(Success.False) };
                    result = SerializerUtil.ConverToJson<ResponseType>(response);
                }

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

        private string GetTBStatusName(string name)
        {
            string strName;

            switch (name.Trim().ToLower())
            {
                case "no signs":
                    strName = "Negative TB Screen";
                    break;
                case "suspect":
                    strName = "Presumptive TB";
                    break;
                case "tb rx":
                    strName = "Client on TB Treatment";
                    break;
                default:
                    strName = name;
                    break;
            }

            return strName;
        }

        private string SaveTriageData(string nodeJson, int ptn_pk, int visitPK, int locationId, int userId)
        {
            string result = string.Empty;
            ResponseType ObjResponse = new ResponseType();
            try
            {
                HIVCE.Common.Entities.Triage triage = SerializerUtil.ConverToObject<HIVCE.Common.Entities.Triage>(nodeJson);
                IClinicalEncounter clinicalencounter = (IClinicalEncounter)ObjectFactory.CreateInstance("HIVCE.BusinessLayer.BLClinicalEncounter, HIVCE.BusinessLayer");
                triage.Ptn_pk = ptn_pk;
                triage.Visit_Id = visitPK;
                triage.LocationId = locationId;

                //result = SerializerUtil.ConverToJson<Triage>(triage);
                bool flag = true;
                int visitId;
                flag = clinicalencounter.SaveUpdateTriage(triage, userId, out visitId);

                Session["PatientVisitId"] = visitId.ToString();
                this.hidVId.Value = visitId.ToString();

                if (flag)
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
            try
            {
                HIVCE.Common.Entities.PresentingComplaint obj = SerializerUtil.ConverToObject<HIVCE.Common.Entities.PresentingComplaint>(nodeJson);
                IClinicalEncounter clinicalencounter = (IClinicalEncounter)ObjectFactory.CreateInstance("HIVCE.BusinessLayer.BLClinicalEncounter, HIVCE.BusinessLayer");
                obj.Ptn_pk = ptn_pk;
                obj.Visit_Id = visitPK;
                obj.LocationId = locationId;

                //result = SerializerUtil.ConverToJson<PresentingComplaint>(obj);

                bool flag = clinicalencounter.SaveUpdatePresentingComplaintsData(obj, userId, locationId);
                if (flag)
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

                ObjResponse.Success = EnumUtil.GetEnumDescription(Success.False);
            }
            finally
            {
            }

            result = SerializerUtil.ConverToJson<ResponseType>(ObjResponse);
            return result;
        }

        private string SaveAdditionalHx(string nodeJson, int ptn_pk, int visitPK, int locationId, int userId)
        {
            string result = string.Empty;
            ResponseType ObjResponse = new ResponseType();
            try
            {
                HIVCE.Common.Entities.DBAdditionalHx obj = SerializerUtil.ConverToObject<HIVCE.Common.Entities.DBAdditionalHx>(nodeJson);
                IClinicalEncounter clinicalencounter = (IClinicalEncounter)ObjectFactory.CreateInstance("HIVCE.BusinessLayer.BLClinicalEncounter, HIVCE.BusinessLayer");
                obj.Ptn_Pk = ptn_pk;
                obj.Visit_Id = visitPK;
                obj.LocationId = locationId;

                //result = SerializerUtil.ConverToJson<PresentingComplaint>(obj);

                bool flag = clinicalencounter.SaveUpdateAdditionalHxData(obj, userId, locationId);
                if (flag)
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

                ObjResponse.Success = EnumUtil.GetEnumDescription(Success.False);
            }
            finally
            {
            }

            result = SerializerUtil.ConverToJson<ResponseType>(ObjResponse);
            return result;
        }

        private string SaveScreeningData(string nodeJson, int ptn_pk, int visitPK, int locationId, int userId)
        {
            string result = string.Empty;
            ResponseType ObjResponse = new ResponseType();
            try
            {

                HIVCE.Common.Entities.DBScreeningData obj = SerializerUtil.ConverToObject<HIVCE.Common.Entities.DBScreeningData>(nodeJson);
                IClinicalEncounter clinicalencounter = (IClinicalEncounter)ObjectFactory.CreateInstance("HIVCE.BusinessLayer.BLClinicalEncounter, HIVCE.BusinessLayer");
                obj.Ptn_Pk = ptn_pk;
                obj.Visit_Id = visitPK;
                obj.LocationId = locationId;

                //result = SerializerUtil.ConverToJson<PresentingComplaint>(obj);

                bool flag = clinicalencounter.SaveUpdateScreeningData(obj, userId, locationId);
                if (flag)
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

                ObjResponse.Success = EnumUtil.GetEnumDescription(Success.False);
            }
            finally
            {
            }

            result = SerializerUtil.ConverToJson<ResponseType>(ObjResponse);
            return result;
        }

        private string SaveSystemicReviewData(string nodeJson, int ptn_pk, int visitPK, int locationId, int userId)
        {
            string result = string.Empty;
            ResponseType ObjResponse = new ResponseType();
            try
            {

                HIVCE.Common.Entities.DBSystemicReviewData obj = SerializerUtil.ConverToObject<HIVCE.Common.Entities.DBSystemicReviewData>(nodeJson);
                IClinicalEncounter clinicalencounter = (IClinicalEncounter)ObjectFactory.CreateInstance("HIVCE.BusinessLayer.BLClinicalEncounter, HIVCE.BusinessLayer");
                obj.Ptn_Pk = ptn_pk;
                obj.Visit_Id = visitPK;
                obj.LocationId = locationId;

                bool flag = clinicalencounter.SaveUpdateSystemicReviewData(obj, userId, locationId);
                if (flag)
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

                ObjResponse.Success = EnumUtil.GetEnumDescription(Success.False);
            }
            finally
            {
            }

            result = SerializerUtil.ConverToJson<ResponseType>(ObjResponse);
            return result;
        }

        private string SaveManagementxData(string nodeJson, int ptn_pk, int visitPK, int locationId, int userId)
        {
            string result = string.Empty;
            ResponseType ObjResponse = new ResponseType();
            try
            {

                HIVCE.Common.Entities.ManagementDB obj = SerializerUtil.ConverToObject<HIVCE.Common.Entities.ManagementDB>(nodeJson);
                IClinicalEncounter clinicalencounter = (IClinicalEncounter)ObjectFactory.CreateInstance("HIVCE.BusinessLayer.BLClinicalEncounter, HIVCE.BusinessLayer");
                obj.Ptn_pk = ptn_pk;
                obj.Visit_Id = visitPK;
                obj.LocationId = locationId;

                bool flag = clinicalencounter.SaveUpdateManagementxData(obj, userId, locationId);
                if (flag)
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

                ObjResponse.Success = EnumUtil.GetEnumDescription(Success.False);
            }
            finally
            {
            }

            result = SerializerUtil.ConverToJson<ResponseType>(ObjResponse);
            return result;
        }

        private string GetZScoreDetails(int ptn_pk, int visitPK, int locationId)
        {
            string result = string.Empty;
            try
            {
                IClinicalEncounter clinicalencounter = (IClinicalEncounter)ObjectFactory.CreateInstance("HIVCE.BusinessLayer.BLClinicalEncounter, HIVCE.BusinessLayer");
                HIVCE.Common.Entities.ClinicalEncounter cl = clinicalencounter.GetData(ptn_pk, visitPK, locationId);
                DataSet ZScoreDS = new DataSet();
                IKNHStaticForms KNHS = (IKNHStaticForms)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHStaticForms, BusinessProcess.Clinical");
                string height = string.IsNullOrEmpty(cl.PatientVitals.Height) == true ? "0" : cl.PatientVitals.Height;
                ZScoreDS = KNHS.GetZScoreNewImplementation(ptn_pk, Session["PatientSex"].ToString(), height.ToString());

                ZScoreDetails zs = new ZScoreDetails();
                zs.WFA = new ZScore();
                zs.WFH = new ZScore();
                zs.BMIz = new ZScore();
                if (ZScoreDS.Tables[0].Rows.Count > 0)
                {
                    DataColumnCollection columns = ZScoreDS.Tables[0].Columns;
                    if (columns.Contains("L"))
                    {
                        zs.WFA.L = Convert.ToDouble(ZScoreDS.Tables[0].Rows[0]["L"].ToString());
                        zs.WFA.M = Convert.ToDouble(ZScoreDS.Tables[0].Rows[0]["M"].ToString());
                        zs.WFA.S = Convert.ToDouble(ZScoreDS.Tables[0].Rows[0]["S"].ToString());
                    }
                }

                if (ZScoreDS.Tables[1].Rows.Count > 0)
                {
                    DataColumnCollection columns = ZScoreDS.Tables[1].Columns;
                    if (columns.Contains("L"))
                    {
                        zs.WFH.L = Convert.ToDouble(ZScoreDS.Tables[1].Rows[0]["L"].ToString());
                        zs.WFH.M = Convert.ToDouble(ZScoreDS.Tables[1].Rows[0]["M"].ToString());
                        zs.WFH.S = Convert.ToDouble(ZScoreDS.Tables[1].Rows[0]["S"].ToString());
                    }
                }

                if (ZScoreDS.Tables[2].Rows.Count > 0)
                {
                    DataColumnCollection columns = ZScoreDS.Tables[2].Columns;
                    if (columns.Contains("L"))
                    {
                        zs.BMIz.L = Convert.ToDouble(ZScoreDS.Tables[2].Rows[0]["L"].ToString());
                        zs.BMIz.M = Convert.ToDouble(ZScoreDS.Tables[2].Rows[0]["M"].ToString());
                        zs.BMIz.S = Convert.ToDouble(ZScoreDS.Tables[2].Rows[0]["S"].ToString());
                    }
                }

                ZScoreDS.Dispose();

                result = SerializerUtil.ConverToJson<HIVCE.Common.Entities.ZScoreDetails>(zs);
            }
            catch (Exception ex)
            {
                CLogger.WriteLog(ELogLevel.ERROR, "GetZScoreDetails() exception: " + ex.ToString());
                ResponseType response = new ResponseType() { Success = EnumUtil.GetEnumDescription(Success.False) };
                result = SerializerUtil.ConverToJson<ResponseType>(response);
            }
            finally
            {

            }
            return result;
        }

        private string GetICD10Values(int ptn_pk, int visitPK, int locationId)
        {
            string result = string.Empty;
            try
            {
                if (object.Equals(Session["ICD10"], string.Empty))
                {

                    IClinicalEncounter clinicalencounter = (IClinicalEncounter)ObjectFactory.CreateInstance("HIVCE.BusinessLayer.BLClinicalEncounter, HIVCE.BusinessLayer");
                    HIVCE.Common.Entities.Management obj = clinicalencounter.GetManagement(ptn_pk, visitPK, locationId);

                    string icd10 = SerializerUtil.ConverToJson<List<HIVCE.Common.Entities.ICD>>(obj.ICDDropdownValues);

                    Session["ICD10"] = icd10;
                    result = icd10;
                }
                else
                {
                    result = Session["ICD10"].ToString();
                }
            }
            catch (Exception ex)
            {
                CLogger.WriteLog(ELogLevel.ERROR, "GetICD10Values() exception: " + ex.ToString());
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

                    IClinicalEncounter clinicalencounter = (IClinicalEncounter)ObjectFactory.CreateInstance("HIVCE.BusinessLayer.BLClinicalEncounter, HIVCE.BusinessLayer");
                    HIVCE.Common.Entities.ClinicalEncounter cl = clinicalencounter.GetData(ptn_pk, visitPK, locationId);


                    string facilitylist = SerializerUtil.ConverToJson<List<HIVCE.Common.Entities.Facility>>(cl.FacilityList);

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
                CLogger.WriteLog(ELogLevel.ERROR, "GetICD10Values() exception: " + ex.ToString());
                ResponseType response = new ResponseType() { Success = EnumUtil.GetEnumDescription(Success.False) };
                result = SerializerUtil.ConverToJson<ResponseType>(response);
            }
            finally
            {

            }
            return result;
        }

        private string SaveTSTriageData(string nodeJson, int ptn_pk, int visitPK, int locationId, int userId)
        {
            string result = string.Empty;
            ResponseType ObjResponse = new ResponseType();
            try
            {
                HIVCE.Common.Entities.TSTriage triage = SerializerUtil.ConverToObject<HIVCE.Common.Entities.TSTriage>(nodeJson);
                IClinicalEncounter clinicalencounter = (IClinicalEncounter)ObjectFactory.CreateInstance("HIVCE.BusinessLayer.BLClinicalEncounter, HIVCE.BusinessLayer");
                triage.Ptn_pk = ptn_pk;
                triage.Visit_Id = visitPK;
                triage.LocationId = locationId;

                //result = SerializerUtil.ConverToJson<Triage>(triage);
                bool flag = true;
                int visitId;
                flag = clinicalencounter.SaveUpdateTSTriage(triage, userId, out visitId);

                Session["PatientVisitId"] = visitId.ToString();
                this.hidVId.Value = visitId.ToString();

                if (flag)
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