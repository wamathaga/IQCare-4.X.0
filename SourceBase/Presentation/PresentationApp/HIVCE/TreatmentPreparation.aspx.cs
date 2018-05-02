using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using HIVCE.BusinessLayer;
using HIVCE.Common;
using System.Configuration;
using Interface.HIVCE;
using Application.Presentation;

namespace HIVCE.Presentation
{
    public partial class TreatmentPreparation : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            
            int PatientId = 0;
            if (!IsPostBack)
            {
                if (ConfigurationManager.AppSettings["IsDevelopment"].ToString() == "false")
                {
                    if (Session["AppLocation"] == null)
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

                if (!object.Equals(Request.QueryString["data"], null))
                {
                    string response = string.Empty;
                    if (Request.QueryString["data"].ToString() == "gettp")
                    {
                        response = GetTreatmentPreparation(Convert.ToInt32(PatientId));
                        SendResponse(response);
                    }
                    if (Request.QueryString["data"].ToString() == "save")
                    {

                        System.IO.StreamReader sr = new System.IO.StreamReader(Request.InputStream);
                        string jsonString = "";
                        jsonString = sr.ReadToEnd();

                        response = SaveData(jsonString, PatientId);
                        SendResponse(response);
                    }
                }
            }
        }

        private string GetTreatmentPreparation(int ptn_pk)
        {
            string result = string.Empty;
            try
            {
                ITreatmentPreparation blObj = (ITreatmentPreparation)ObjectFactory.CreateInstance("HIVCE.BusinessLayer.BLTreatmentPreparation, HIVCE.BusinessLayer");
                HIVCE.Common.Entities.TreatmentPreparation objTP = blObj.GetTreatmentPreparation(ptn_pk);
                result = SerializerUtil.ConverToJson<HIVCE.Common.Entities.TreatmentPreparation>(objTP);
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

        private string SaveData(string nodeJson, int ptn_pk)
        {
            string result = string.Empty;
            ResponseType ObjResponse = new ResponseType();
            try
            {
                HIVCE.Common.Entities.TreatmentPreparation treatmentpreparation = SerializerUtil.ConverToObject<HIVCE.Common.Entities.TreatmentPreparation>(nodeJson);
                ITreatmentPreparation blObj = (ITreatmentPreparation)ObjectFactory.CreateInstance("HIVCE.BusinessLayer.BLTreatmentPreparation, HIVCE.BusinessLayer");
                treatmentpreparation.Ptn_pk = ptn_pk;

                bool flag = blObj.SaveUpdateTreatmentPreparation(treatmentpreparation);
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