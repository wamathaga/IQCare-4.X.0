using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using HIVCE.Common;
using Interface.Clinical;
using Interface.HIVCE;
using System.Web.UI.HtmlControls;

namespace PresentationApp.ClinicalForms
{
    public partial class frmClinical_ANC : System.Web.UI.Page
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



                if (!object.Equals(Request.QueryString["data"], null))
                {
                    string response = string.Empty;

                    HtmlButton btnDeleteForm = (HtmlButton)this.ucANCProfile1.FindControl("btnDeleteForm");
                    btnDeleteForm.Style.Add("visibility", " hidden");


                    if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
                    {
                        CLogger.WriteLog(ELogLevel.ERROR, "Session expired!!");

                        ResponseType responsetype = new ResponseType() { Success = EnumUtil.GetEnumDescription(Success.False), ErrorMessage = "Session expired" };
                        response = SerializerUtil.ConverToJson<ResponseType>(responsetype);
                        SendResponse(response);
                    }

                    if (Request.QueryString["data"].ToString() == "getprofiledata")
                    {
                        response = GetProfileData(Convert.ToInt32(PatientId), visitPK, locationId);
                        SendResponse(response);
                    }

                    if (Request.QueryString["data"].ToString() == "getscreeningtests")
                    {
                        response = GetScreeningAndTestsData(Convert.ToInt32(PatientId), visitPK, locationId);
                        SendResponse(response);
                    }

                    if (Request.QueryString["data"].ToString() == "saveprofile")
                    {

                        System.IO.StreamReader sr = new System.IO.StreamReader(Request.InputStream);
                        string jsonString = "";
                        jsonString = sr.ReadToEnd();

                        response = SaveProfileData(jsonString, PatientId, visitPK, locationId, userId);
                        SendResponse(response);
                    }

                    if (Request.QueryString["data"].ToString() == "savescreeningtestdata")
                    {

                        System.IO.StreamReader sr = new System.IO.StreamReader(Request.InputStream);
                        string jsonString = "";
                        jsonString = sr.ReadToEnd();

                        response = SaveScreeningAndTestsData(jsonString, PatientId, visitPK, locationId, userId);
                        SendResponse(response);
                    }

                    if (Request.QueryString["data"].ToString() == "getvisitdetails")
                    {
                        response = GetVisitDetailsData(Convert.ToInt32(PatientId), visitPK, locationId);
                        SendResponse(response);
                    }

                    if (Request.QueryString["data"].ToString() == "savevisitdetailsdata")
                    {

                        System.IO.StreamReader sr = new System.IO.StreamReader(Request.InputStream);
                        string jsonString = "";
                        jsonString = sr.ReadToEnd();

                        response = SaveVisitDetailsData(jsonString, PatientId, visitPK, locationId, userId);
                        SendResponse(response);
                    }
                    if (Request.QueryString["data"].ToString() == "deleteform")
                    {
                        btnDeleteForm.Style.Add("visibility", " visible");
                        response = DeleteForm();
                        SendResponse(response);
                    }
                }
            }
        }

        private string GetProfileData(int ptn_pk, int visitPK, int locationId)
        {
            string result = string.Empty;
            try
            {
                IKNHMEI iANC = (IKNHMEI)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHMEI, BusinessProcess.Clinical");
                Entities.Common.ANC anc = iANC.GetANCData(ptn_pk, visitPK, locationId);

                result = SerializerUtil.ConverToJson<Entities.Common.ANC>(anc);
            }
            catch (Exception ex)
            {
                CLogger.WriteLog(ELogLevel.ERROR, "GetANCProfileData() exception: " + ex.ToString());
                ResponseType response = new ResponseType() { Success = EnumUtil.GetEnumDescription(Success.False) };
                result = SerializerUtil.ConverToJson<ResponseType>(response);
            }
            finally
            {

            }
            return result;
        }

        private string GetScreeningAndTestsData(int ptn_pk, int visitPK, int locationId)
        {
            string result = string.Empty;
            try
            {
                IKNHMEI iANC = (IKNHMEI)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHMEI, BusinessProcess.Clinical");
                Entities.Common.ANCScreeningTests anc = iANC.GetANCScreeningTestsData(ptn_pk, locationId, visitPK);

                result = SerializerUtil.ConverToJson<Entities.Common.ANCScreeningTests>(anc);
            }
            catch (Exception ex)
            {
                CLogger.WriteLog(ELogLevel.ERROR, "GetScreeningAndTestsData() exception: " + ex.ToString());
                ResponseType response = new ResponseType() { Success = EnumUtil.GetEnumDescription(Success.False) };
                result = SerializerUtil.ConverToJson<ResponseType>(response);
            }
            finally
            {

            }
            return result;
        }

        private string SaveProfileData(string nodeJson, int ptn_pk, int visitPK, int locationId, int userId)
        {
            string result = string.Empty;
            ResponseType ObjResponse = new ResponseType();
            try
            {
                Entities.Common.ANC anc = SerializerUtil.ConverToObject<Entities.Common.ANC>(nodeJson);
                IKNHMEI iknhmei = (IKNHMEI)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHMEI, BusinessProcess.Clinical");
                anc.Ptn_pk = ptn_pk;
                anc.Visit_Id = visitPK;
                anc.LocationId = locationId;

                //result = SerializerUtil.ConverToJson<Triage>(triage);
                bool flag = true;
                int visitId;
                flag = iknhmei.SaveUpdateProfile(anc, userId, locationId, out visitId);

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

        private string SaveScreeningAndTestsData(string nodeJson, int ptn_pk, int visitPK, int locationId, int userId)
        {
            string result = string.Empty;
            ResponseType ObjResponse = new ResponseType();
            try
            {
                Entities.Common.ANCScreeningTests anc = SerializerUtil.ConverToObject<Entities.Common.ANCScreeningTests>(nodeJson);
                IKNHMEI iknhmei = (IKNHMEI)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHMEI, BusinessProcess.Clinical");
                anc.Ptn_pk = ptn_pk;
                anc.Visit_Id = visitPK;
                anc.LocationId = locationId;

                bool flag = true;
                int visitId;
                flag = iknhmei.SaveUpdateScreeningTestsData(anc, userId, locationId);
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


        private string GetVisitDetailsData(int ptn_pk, int visitPK, int locationId)
        {
            string result = string.Empty;
            try
            {
                IKNHMEI iANC = (IKNHMEI)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHMEI, BusinessProcess.Clinical");
                Entities.Common.ANCVisitDetails anc = iANC.GetANCVisitDetailsData(ptn_pk, locationId, visitPK);

                result = SerializerUtil.ConverToJson<Entities.Common.ANCVisitDetails>(anc);
            }
            catch (Exception ex)
            {
                CLogger.WriteLog(ELogLevel.ERROR, "GetVisitDetailsData() exception: " + ex.ToString());
                ResponseType response = new ResponseType() { Success = EnumUtil.GetEnumDescription(Success.False) };
                result = SerializerUtil.ConverToJson<ResponseType>(response);
            }
            finally
            {

            }
            return result;
        }

        private string SaveVisitDetailsData(string nodeJson, int ptn_pk, int visitPK, int locationId, int userId)
        {
            string result = string.Empty;
            ResponseType ObjResponse = new ResponseType();
            try
            {
                Entities.Common.ANCVisitDetails anc = SerializerUtil.ConverToObject<Entities.Common.ANCVisitDetails>(nodeJson);
                IKNHMEI iknhmei = (IKNHMEI)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHMEI, BusinessProcess.Clinical");
                anc.Ptn_pk = ptn_pk;
                anc.Visit_Id = visitPK;
                anc.LocationId = locationId;

                bool flag = true;
                int visitId;
                flag = iknhmei.SaveUpdateVisitDetailsData(anc, userId, locationId);
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

        public string DeleteForm()
        {
            string result = string.Empty;
            try
            {
                IinitialFollowupVisit InitialFollowupvisit;
                int theResultRow, OrderNo;
                string FormName;
                OrderNo = Convert.ToInt32(Session["PatientVisitId"].ToString());
                FormName = "ANC";

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