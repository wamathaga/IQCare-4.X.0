using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HIVCE.BusinessLayer;
using HIVCE.Common;
using System.Configuration;

namespace HIVCE.Presentation
{
    public partial class ClinicalEncounter : System.Web.UI.Page
    {
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
                if (ConfigurationManager.AppSettings["IsDevelopment"].ToString() == "false")
                {
                    if (Session["AppLocationId"] == null)
                    {
                        Response.Redirect("~/frmlogin.aspx", true);
                    }

                    if (PatientId == 0)
                    {
                        Response.Redirect("~/ClinicalForms/frmPatient_Home.aspx", true);
                    }
                }
                if (!object.Equals(Session["PatientId"], null))
                {
                    PatientId = Convert.ToInt32(Session["PatientId"]);
                }
                if (!object.Equals(Session["PatientVisitId"], null))
                {
                    visitPK = Convert.ToInt32(Session["PatientVisitId"]);
                }
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

                if (!object.Equals(Request.QueryString["add"], null))
                {
                    if (Request.QueryString["add"].ToString() == "0")
                    {
                        visitPK = 0;
                    }
                }
                if (!object.Equals(Request.QueryString["data"], null))
                {
                    string response = string.Empty;
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


                }
            }
        }

        private string GetClinicalEnounter(int ptn_pk, int visitPK, int locationId)
        {
            string result = string.Empty;
            try
            {
                BLClinicalEncounter bl = new BLClinicalEncounter();
                HIVCE.Common.Entities.ClinicalEncounter obj = bl.GetData(ptn_pk, visitPK, locationId);
                result = SerializerUtil.ConverToJson<HIVCE.Common.Entities.ClinicalEncounter>(obj);
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

        private string GetPresentingComplaints(int ptn_pk, int visitPK, int locationId)
        {
            string result = string.Empty;
            try
            {
                BLClinicalEncounter bl = new BLClinicalEncounter();
                HIVCE.Common.Entities.PresentingComplaint obj = bl.GetPresentingComplaints(ptn_pk, visitPK, locationId);
                result = SerializerUtil.ConverToJson<HIVCE.Common.Entities.PresentingComplaint>(obj);
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
                BLClinicalEncounter bl = new BLClinicalEncounter();
                HIVCE.Common.Entities.AddtionalHx obj = bl.GetAddtionalHx(ptn_pk, visitPK, locationId);
                result = SerializerUtil.ConverToJson<HIVCE.Common.Entities.AddtionalHx>(obj);
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

        private string SaveTriageData(string nodeJson, int ptn_pk, int visitPK, int locationId, int userId)
        {
            string result = string.Empty;
            ResponseType ObjResponse = new ResponseType();
            try
            {
                HIVCE.Common.Entities.Triage triage = SerializerUtil.ConverToObject<HIVCE.Common.Entities.Triage>(nodeJson);
                BLClinicalEncounter blTP = new BLClinicalEncounter();
                triage.Ptn_pk = ptn_pk;
                triage.Visit_Id = visitPK;
                triage.LocationId = locationId;


                bool flag = blTP.SaveUpdateTriage(triage, userId);
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

        private string SavePresentingComplaintsData(string nodeJson, int ptn_pk, int visitPK, int locationId, int userId)
        {
            string result = string.Empty;
            ResponseType ObjResponse = new ResponseType();
            try
            {
                HIVCE.Common.Entities.PresentingComplaint obj = SerializerUtil.ConverToObject<HIVCE.Common.Entities.PresentingComplaint>(nodeJson);
                BLClinicalEncounter blTP = new BLClinicalEncounter();
                obj.Ptn_pk = ptn_pk;
                obj.Visit_Id = visitPK;
                obj.LocationId = locationId;


                bool flag = blTP.SaveUpdatePresentingComplaintsData(obj, userId, locationId);
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