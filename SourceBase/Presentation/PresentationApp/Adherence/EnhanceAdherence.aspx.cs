using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Application.Presentation;
using System.Data;
using Interface.Service;
using Application.Common;
using Entities.Common;
using HIVCE.Common;
using Interface.Clinical;
using System.Web.UI.HtmlControls;
using System.Web.Services;

namespace PresentationApp.Adherence
{
    public partial class EnhanceAdherence : BasePage
    {
        ICommonData oCommonData = (ICommonData)ObjectFactory.CreateInstance("BusinessProcess.Service.BCommonData,BusinessProcess.Service");

        protected void Page_Load(object sender, EventArgs e)
        {
            int PatientId = 0;
            int visitPK = 0;
            int locationId = 0;
            int userId = 0;
            int sessionId = 0;

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
                            this.hidCallFrom.Value = "new";
                        }
                    }
                    else
                    {
                        visitPK = Convert.ToInt32(Session["PatientVisitId"]);
                        this.hidCallFrom.Value = "existing";
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

                string tabName = "session1";
                ((HtmlInputHidden)this.hidTabName).Value.ToString();

                if (!object.Equals(Request.QueryString["data"], null))
                {
                    if (Request.QueryString["data"].ToString() == "getSession1Data")
                    {
                        tabName = "session1";
                    }
                    if (Request.QueryString["data"].ToString() == "getSession2Data")
                    {
                        tabName = "session2";
                    }
                    if (Request.QueryString["data"].ToString() == "getSession3Data")
                    {
                        tabName = "session3";
                    }
                    if (Request.QueryString["data"].ToString() == "getSession4Data")
                    {
                        tabName = "session4";
                    }
                    if (Request.QueryString["data"].ToString() == "getSessionVLData")
                    {
                        tabName = "sessionVL";
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

                    if (Request.QueryString["data"].ToString() == "getSession1Data")
                    {
                        response = GetEnhanceAdherence(Convert.ToInt32(PatientId), locationId, visitPK, 1);
                        SendResponse(response);
                    }
                    if (Request.QueryString["data"].ToString() == "getSession2Data")
                    {
                        response = GetEnhanceAdherence(Convert.ToInt32(PatientId), locationId, visitPK, 2);
                        SendResponse(response);
                    }
                    if (Request.QueryString["data"].ToString() == "getSession3Data")
                    {
                        response = GetEnhanceAdherence(Convert.ToInt32(PatientId), locationId, visitPK, 3);
                        SendResponse(response);
                    }
                    if (Request.QueryString["data"].ToString() == "getSession4Data")
                    {
                        response = GetEnhanceAdherence(Convert.ToInt32(PatientId), locationId, visitPK, 4);
                        SendResponse(response);
                    }
                    if (Request.QueryString["data"].ToString() == "getSessionVLData")
                    {
                        response = GetEnhanceAdherence(Convert.ToInt32(PatientId), locationId, visitPK, 5);
                        SendResponse(response);
                    }


                    if (Request.QueryString["data"].ToString() == "saveSession1")
                    {
                        System.IO.StreamReader sr = new System.IO.StreamReader(Request.InputStream);
                        string jsonString = ""; sessionId = 1;
                        jsonString = sr.ReadToEnd();
                        response = SaveSession(jsonString, PatientId, visitPK, locationId, userId, sessionId);
                        SendResponse(response);
                    }
                    if (Request.QueryString["data"].ToString() == "saveSession2")
                    {
                        System.IO.StreamReader sr = new System.IO.StreamReader(Request.InputStream);
                        string jsonString = ""; sessionId = 2;
                        jsonString = sr.ReadToEnd();
                        response = SaveSession(jsonString, PatientId, visitPK, locationId, userId, sessionId);
                        SendResponse(response);
                    }
                    if (Request.QueryString["data"].ToString() == "saveSession3")
                    {
                        System.IO.StreamReader sr = new System.IO.StreamReader(Request.InputStream);
                        string jsonString = ""; sessionId = 3;
                        jsonString = sr.ReadToEnd();
                        response = SaveSession(jsonString, PatientId, visitPK, locationId, userId, sessionId);
                        SendResponse(response);
                    }
                    if (Request.QueryString["data"].ToString() == "saveSession4")
                    {
                        System.IO.StreamReader sr = new System.IO.StreamReader(Request.InputStream);
                        string jsonString = ""; sessionId = 4;
                        jsonString = sr.ReadToEnd();
                        response = SaveSession(jsonString, PatientId, visitPK, locationId, userId, sessionId);
                        SendResponse(response);
                    }
                    if (Request.QueryString["data"].ToString() == "saveSessionVL")
                    {
                        System.IO.StreamReader sr = new System.IO.StreamReader(Request.InputStream);
                        string jsonString = ""; sessionId = 5;
                        jsonString = sr.ReadToEnd();
                        response = SaveSession(jsonString, PatientId, visitPK, locationId, userId, sessionId);
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

        public void Authenticate()
        {
            //string tabName = ((HtmlInputHidden)this.hidTabName).Value.ToString();
            AuthenticationManager authMgr = new AuthenticationManager();

            /********** getting the data from XML file *******************************/
            DataSet XMLDS = new DataSet();
            XMLDS.ReadXml(MapPath("..\\XMLFiles\\AllMasters.con"));
            DataView theDV = new DataView(XMLDS.Tables["Mst_FormBuilderTab"]);

            //int featureId = 298;
            int featureId = ApplicationAccess.EnhanceAdherenceCounselling;


            theDV.RowFilter = "FeatureId=" + featureId;
            IQCareUtils iQCareUtils = new IQCareUtils();
            DataTable dtTabId = (DataTable)iQCareUtils.CreateTableFromDataView(theDV);

            int tabId = 0;

            bool enable = true;
            int visitId = Convert.ToInt32(Session["PatientVisitId"]);

            HtmlButton btnSaveSession1 = (HtmlButton)this.ucSession1.FindControl("btnSaveSession1");
            //HtmlButton btnSaveSession2 = (HtmlButton)this.ucPresentingComplaints1.FindControl("btnSavePC");
            //HtmlButton btnSaveAddtionalHx = (HtmlButton)this.ucAddtionalHx1.FindControl("btnSaveAddtionalHx");
            //HtmlButton btnSaveScreening = (HtmlButton)this.ucScreening1.FindControl("btnSaveScreening");
            //HtmlButton btnSaveSystemicReview = (HtmlButton)this.ucSystemicReview1.FindControl("btnSaveSystemicReview");
            //HtmlButton btnSaveManagement = (HtmlButton)this.ucManagementx1.FindControl("btnSaveManagement");


            if (Request.QueryString["name"] == "Delete")
            {
                btnSaveSession1.Visible = false;
                btnDeleteForm.Visible = true;
            }
            else
            {
                btnSaveSession1.Attributes["disabled"] = "true";
                //btnSavePC.Attributes["disabled"] = "true";
                //btnSaveAddtionalHx.Attributes["disabled"] = "true";
                //btnSaveScreening.Attributes["disabled"] = "true";
                //btnSaveSystemicReview.Attributes["disabled"] = "true";
                //btnSaveManagement.Attributes["disabled"] = "true";

                btnDeleteForm.Visible = false;
            }


            if (dtTabId.Rows.Count > 0)
            {
                foreach (DataRow drTabsId in dtTabId.Rows)
                {
                    string tName = drTabsId["TabName"].ToString();
                    string tabName = string.Empty;
                    if (tName.ToLower().Contains("session1"))
                    {
                        tabName = "session1";
                    }
                    else
                    {
                        tabName = tName.ToLower();
                    }

                    if (tabName.ToLower().Contains("session1"))
                    {
                        if (drTabsId["TabName"].ToString().ToLower() == ("Session1").ToLower())
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

                    if (tabName == "session1")
                    {
                        if (enable)
                        {
                            btnSaveSession1.Attributes.Remove("disabled");
                            //btnSaveFindTriage.Attributes.Remove("disabled");
                            //if (!object.Equals(btnTSSaveManagement, null))
                            //{
                            //    btnTSSaveManagement.Attributes.Remove("disabled");
                            //}
                        }
                    }
                    else if (tabName == "session2")
                    {

                        if (enable)
                        {
                            //btnSavePC.Attributes.Remove("disabled");
                        }
                    }
                    else if (tabName == "session3")
                    {
                        if (enable)
                        {
                            //btnSaveAddtionalHx.Attributes.Remove("disabled");
                        }
                    }
                    else if (tabName == "session4")
                    {
                        if (enable)
                        {
                            //btnSaveScreening.Attributes.Remove("disabled");
                        }
                    }
                    else if (tabName == "viralload")
                    {
                        if (enable)
                        {
                            //btnSaveSystemicReview.Attributes.Remove("disabled");
                        }
                    }
                    //else if (tabName == "management")
                    //{

                    //    if (enable)
                    //    {
                    //        //btnSaveManagement.Attributes.Remove("disabled");
                    //    }
                    //}

                }

                if (Request.QueryString["name"] == "Delete")
                {
                    btnSaveSession1.Attributes["disabled"] = "true";
                    //btnSaveAddtionalHx.Attributes["disabled"] = "true";
                    //btnSaveScreening.Attributes["disabled"] = "true";
                    //btnSaveSystemicReview.Attributes["disabled"] = "true";
                    //btnSaveManagement.Attributes["disabled"] = "true";
                    //if (!object.Equals(btnTSSaveManagement, null))
                    //{
                    //    btnTSSaveManagement.Attributes["disabled"] = "true";
                    //}
                }

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
                FormName = "Enhance Adherence Counselling";

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

        private string GetEnhanceAdherence(int ptn_pk, int locationId, int visitPK, int sessionID)
        {
            string result = string.Empty;
            try
            {
                IPatientAdherence enchanceAdherence;
                enchanceAdherence = (IPatientAdherence)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientAdherence, BusinessProcess.Clinical");
                Entities.Common.PatientAdherenceCounseling PAC = enchanceAdherence.GetEnhanceAdherenceCounsellingDataByPatient(ptn_pk, locationId, visitPK, sessionID);

                IQCareUtils theUtils = new IQCareUtils();
                DataSet theDSXML = new DataSet();
                theDSXML.ReadXml(MapPath("..\\XMLFiles\\AllMasters.con"));
                double age = Convert.ToDouble(Session["patientageinyearmonth"].ToString());
                List<CodeDeCode> collections = new List<CodeDeCode>();
                DataView theCodeDV = new DataView(theDSXML.Tables["MST_CODE"]);
                theCodeDV.RowFilter = "DeleteFlag=0 and Name in ('RememberingMedications')";
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

                theCodeDV = new DataView(oCommonData.getAllEmployees());
                theCodeDT = (DataTable)theUtils.CreateTableFromDataView(theCodeDV);
                theDT = new DataTable();
                if (theCodeDT.Rows.Count > 0)
                {
                    foreach (DataRow row in theCodeDT.Rows)
                    {
                        Entities.Common.UserDesignation ud = new Entities.Common.UserDesignation();
                        ud.UserID = Convert.ToInt32(row["UserId"].ToString());
                        ud.UserName = row["UserName"].ToString();
                        ud.Designation = row["Designation"].ToString();
                        ud.DeleteFlag = Convert.ToInt32(row["DeleteFlag"].ToString());

                        PAC.Users.Add(ud);
                    }
                }

                PAC.DropdownValues.AddRange(collections);

                result = SerializerUtil.ConverToJson<PatientAdherenceCounseling>(PAC);
            }
            catch (Exception ex)
            {
                CLogger.WriteLog(ELogLevel.ERROR, "GetEnhanceAdherence() exception: " + ex.ToString());
                ResponseType response = new ResponseType() { Success = EnumUtil.GetEnumDescription(Success.False) };
                result = SerializerUtil.ConverToJson<ResponseType>(response);
            }
            finally
            {

            }
            return result;
        }

        private string SaveSession(string nodeJson, int ptn_pk, int visitPK, int locationId, int userId, int sessionId)
        {
            string result = string.Empty;
            ResponseType ObjResponse = new ResponseType();
            try
            {
                PatientAdherenceCounseling patientAdherenceCounselling = SerializerUtil.ConverToObject<PatientAdherenceCounseling>(nodeJson);
                IPatientAdherence ipatientadherence;
                ipatientadherence = (IPatientAdherence)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientAdherence,BusinessProcess.Clinical");
                patientAdherenceCounselling.Ptn_Pk = ptn_pk;
                patientAdherenceCounselling.Visit_Id = visitPK;
                patientAdherenceCounselling.LocationId = locationId;
                patientAdherenceCounselling.CurrentSession = sessionId;
                bool flag = true;
                int visitId = 0;

                visitId = ipatientadherence.SaveUpdateEnhanceAdherenceCounsellingSessions(patientAdherenceCounselling, userId);
               
                if (visitId > 0)
                    flag = true;
                else
                    flag = false;
                
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
                CLogger.WriteLog(ELogLevel.ERROR, "SavePatientAdherenceBarriersData() exception: " + ex.ToString());
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