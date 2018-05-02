using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Common;
using HIVCE.Common;
using Interface.HIVCE;
using HIVCE.Common;
using System.Configuration;
using HIVCE.Common.Entities;
using Application.Presentation;
using System.Data;
using Interface.HIVCE;
using Interface.Service;
using Application.Common;
using Interface.Clinical;

namespace HIVCE.Presentation
{
    public partial class RefillEncounterForm : System.Web.UI.Page
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


                    if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
                    {
                        CLogger.WriteLog(ELogLevel.ERROR, "Session expired!!");

                        ResponseType responsetype =
                            new ResponseType()
                            {
                                Success = EnumUtil.GetEnumDescription(Success.False),
                                ErrorMessage = "Session expired"
                            };
                        response = SerializerUtil.ConverToJson<ResponseType>(responsetype);
                        SendResponse(response);
                    }
                    if (Request.QueryString["data"].ToString() == "getdata")
                    {
                        response = GetRefillEncounter(Convert.ToInt32(PatientId), visitPK, locationId);
                        SendResponse(response);
                    }
                    if (Request.QueryString["data"].ToString() == "getzscore")
                    {
                        response = GetZScoreDetails(Convert.ToInt32(PatientId), visitPK, locationId);
                        SendResponse(response);
                    }
                    if (Request.QueryString["data"].ToString() == "saverefillencounter")
                    {

                        System.IO.StreamReader sr = new System.IO.StreamReader(Request.InputStream);
                        string jsonString = "";
                        jsonString = sr.ReadToEnd();

                        response = SaveRefillEncounterData(jsonString, PatientId, visitPK, locationId, userId);
                        SendResponse(response);
                    }
                }
            }
        }

        private string GetRefillEncounter(int ptn_pk, int visitPK, int locationId)
        {
            string result = string.Empty;
            try
            {
                IClinicalEncounter clinicalencounter = (IClinicalEncounter)ObjectFactory.CreateInstance("HIVCE.BusinessLayer.BLClinicalEncounter, HIVCE.BusinessLayer");
                HIVCE.Common.Entities.RefillEncounter cl = clinicalencounter.GetRefillEncounter(ptn_pk, visitPK, locationId);

                result = SerializerUtil.ConverToJson<HIVCE.Common.Entities.RefillEncounter>(cl);
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

        private string SaveRefillEncounterData(string nodeJson, int ptn_pk, int visitPK, int locationId, int userId)
        {
            string result = string.Empty;
            ResponseType ObjResponse = new ResponseType();
            try
            {

                HIVCE.Common.Entities.RefillEncounterDB obj = SerializerUtil.ConverToObject<HIVCE.Common.Entities.RefillEncounterDB>(nodeJson);
                IClinicalEncounter clinicalencounter = (IClinicalEncounter)ObjectFactory.CreateInstance("HIVCE.BusinessLayer.BLClinicalEncounter, HIVCE.BusinessLayer");
                obj.Ptn_pk = ptn_pk;
                obj.Visit_Id = visitPK;
                obj.LocationId = locationId;

                bool flag = clinicalencounter.SaveRefillEncounterData(obj, userId, locationId);
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