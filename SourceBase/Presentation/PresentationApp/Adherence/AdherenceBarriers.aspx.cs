using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HIVCE.Common;
using Application.Common;
using Interface.Clinical;
using Application.Presentation;
using Entities.Common;
using System.Data;
using System.Web.UI.HtmlControls;

namespace PresentationApp.Adherence
{
    public partial class AdherenceBarriers : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
                //if (!object.Equals(Session["PatientSex"], null))
                //{
                //    this.hidGender.Value = Session["PatientSex"].ToString();
                //}
                //if (!object.Equals(Session["PatientAge"], null))
                //{
                //    this.hidDOB.Value = Session["PatientAge"].ToString();
                //}
                //if (!object.Equals(Session["patientageinyearmonth"], null))
                //{
                //    this.hidPAYM.Value = Session["patientageinyearmonth"].ToString();
                //}
                if (!object.Equals(Session["TechnicalAreaName"], null))
                {
                    this.hidsrvNm.Value = Session["TechnicalAreaName"].ToString();
                }
                if (!object.Equals(Session["TechnicalAreaId"], null))
                {
                    this.hidMOD.Value = Session["TechnicalAreaId"].ToString();
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
                        response = GetPatientAdherenceBarriers(Convert.ToInt32(PatientId), visitPK, locationId);
                        SendResponse(response);
                    }


                    if (Request.QueryString["data"].ToString() == "savedata")
                    {

                        System.IO.StreamReader sr = new System.IO.StreamReader(Request.InputStream);
                        string jsonString = "";
                        jsonString = sr.ReadToEnd();

                        response = SavePatientAdherenceBarriersData(jsonString, PatientId, visitPK, locationId, userId);
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
            HtmlButton btnS1Next = (HtmlButton)this.ucBarriersSection11.FindControl("btnS1Next");

            HtmlButton btnS2Previous = (HtmlButton)this.ucBarriersSection21.FindControl("btnS2Previous");
            HtmlButton btnS2Next = (HtmlButton)this.ucBarriersSection21.FindControl("btnS2Next");

            HtmlButton btnS3Previous = (HtmlButton)this.ucBarriersSection31.FindControl("btnS3Previous");
            HtmlButton btnS3Next = (HtmlButton)this.ucBarriersSection31.FindControl("btnS3Next");

            HtmlButton btnS4Previous = (HtmlButton)this.ucBarriersSection41.FindControl("btnS4Previous");
            HtmlButton btnSaveForm = (HtmlButton)this.ucBarriersSection41.FindControl("btnSaveForm");

            AuthenticationManager authMgr = new AuthenticationManager();

            /********** getting the data from XML file *******************************/
            //int featureId = 299;
            int featureId = ApplicationAccess.AdherenceBarriers;
            bool enable = true;
            int visitId = Convert.ToInt32(Session["PatientVisitId"]);
            if (Request.QueryString["name"] == "Delete")
            {
                btnSaveForm.Visible = false;
                btnDeleteForm.Visible = true;
            }
            else
            {
                btnSaveForm.Attributes["disabled"] = "true";
                btnDeleteForm.Visible = false;
            }

            if (visitId == 0)
            {
                enable = authMgr.HasFunctionRight(featureId, FunctionAccess.Add, (DataTable)System.Web.HttpContext.Current.Session["UserRight"]);
            }

            if (visitId > 0)
            {
                enable = authMgr.HasFunctionRight(featureId, FunctionAccess.Update, (DataTable)System.Web.HttpContext.Current.Session["UserRight"]);
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

            if (enable)
            {
                btnSaveForm.Attributes.Remove("disabled");
            }

            if (Request.QueryString["name"] == "Delete")
            {
                btnSaveForm.Attributes["disabled"] = "true";
                btnSaveForm.Attributes["disabled"] = "true";
            }
        }

        private string GetPatientAdherenceBarriers(int ptn_pk, int visitPK, int locationId)
        {
            string result = string.Empty;
            try
            {
                IPatientAdherence ipatientadherence;
                ipatientadherence = (IPatientAdherence)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientAdherence, BusinessProcess.Clinical");
                PatientAdherenceBarrier patientadherencebarrier = ipatientadherence.GetPatientAdherenceBarriers(ptn_pk, visitPK, locationId);

                result = SerializerUtil.ConverToJson<PatientAdherenceBarrier>(patientadherencebarrier);
            }
            catch (Exception ex)
            {
                string str = "ptn_pk: " + ptn_pk.ToString() + ",visitPK: " + visitPK.ToString() + ",locationId:" + locationId.ToString();
                CLogger.WriteLog(ELogLevel.ERROR, "GetPatientAdherenceBarriers() exception: " + str + "-" + ex.ToString());
                ResponseType response = new ResponseType() { Success = EnumUtil.GetEnumDescription(Success.False) };
                result = SerializerUtil.ConverToJson<ResponseType>(response);
            }
            finally
            {

            }
            return result;
        }

        private string SavePatientAdherenceBarriersData(string nodeJson, int ptn_pk, int visitPK, int locationId, int userId)
        {
            string result = string.Empty;
            ResponseType ObjResponse = new ResponseType();
            try
            {
                PatientAdherenceBarrier patientadherencebarrier = SerializerUtil.ConverToObject<PatientAdherenceBarrier>(nodeJson);
                IPatientAdherence ipatientadherence;
                ipatientadherence = (IPatientAdherence)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientAdherence, BusinessProcess.Clinical");
                patientadherencebarrier.Ptn_Pk = ptn_pk;
                patientadherencebarrier.Visit_Id = visitPK;
                patientadherencebarrier.LocationId = locationId;

                //result = SerializerUtil.ConverToJson<Triage>(triage);
                bool flag = true;
                int visitId;
                flag = ipatientadherence.SaveUpdatePatientAdherenceBarriers(patientadherencebarrier, userId, out visitId);


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

        public string DeleteForm()
        {
            string result = string.Empty;
            try
            {
                IinitialFollowupVisit InitialFollowupvisit;
                int theResultRow, OrderNo;
                string FormName;
                OrderNo = Convert.ToInt32(Session["PatientVisitId"].ToString());
                FormName = "Adherence Barriers";

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