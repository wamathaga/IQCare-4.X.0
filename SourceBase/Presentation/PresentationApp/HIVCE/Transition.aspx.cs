using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HIVCE.BusinessLayer;
using HIVCE.Common;
using System.Configuration;
using Application.Presentation;
using Interface.HIVCE;

namespace HIVCE.Presentation
{
    public partial class Transition : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int PatientId = 0;
            int visitPK = 0;
            DateTime dob = System.DateTime.Now;

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
                else
                {
                    if (object.Equals(Session["DOB"], null))
                    {
                        Session["DOB"] = "22-Mar-2012";
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

                if (!object.Equals(Request.QueryString["add"], null))
                {
                    if (Request.QueryString["add"].ToString() == "0")
                    {
                        visitPK = 0;
                    }
                }
                Common.Age age = new Age(DateTime.Now);
                if (!object.Equals(Session["DOB"], null))
                {
                    dob = Convert.ToDateTime(Session["DOB"]);
                    age = new Age(new DateTime(dob.Year, dob.Month, dob.Day), DateTime.Now);

                }
                this.hidDOB.Value = age.Years.ToString();

                if (!object.Equals(Request.QueryString["data"], null))
                {
                    string response = string.Empty;
                    if (Request.QueryString["data"].ToString() == "gettp")
                    {
                        response = GetTransition(Convert.ToInt32(PatientId), visitPK);
                        SendResponse(response);
                    }
                    if (Request.QueryString["data"].ToString() == "save")
                    {

                        System.IO.StreamReader sr = new System.IO.StreamReader(Request.InputStream);
                        string jsonString = "";
                        jsonString = sr.ReadToEnd();

                        response = SaveData(jsonString, PatientId, visitPK);
                        SendResponse(response);
                    }
                }
            }
        }

        private string GetTransition(int ptn_pk, int visitPK)
        {
            string result = string.Empty;
            try
            {
                BLTransition bl = new BLTransition();
                ITransition blObj = (ITransition)ObjectFactory.CreateInstance("HIVCE.BusinessLayer.BLTransition, HIVCE.BusinessLayer");
                HIVCE.Common.Entities.Transition objTP = blObj.GetTransition(ptn_pk, visitPK);
                result = SerializerUtil.ConverToJson<HIVCE.Common.Entities.Transition>(objTP);
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

        private string SaveData(string nodeJson, int ptn_pk, int visitPK)
        {
            string result = string.Empty;
            ResponseType ObjResponse = new ResponseType();
            try
            {
                HIVCE.Common.Entities.Transition treatmentpreparation = SerializerUtil.ConverToObject<HIVCE.Common.Entities.Transition>(nodeJson);
                ITransition blObj = (ITransition)ObjectFactory.CreateInstance("HIVCE.BusinessLayer.BLTransition, HIVCE.BusinessLayer");
                treatmentpreparation.Ptn_pk = ptn_pk;
                treatmentpreparation.Visit_Id = visitPK;

                bool flag = blObj.SaveUpdateTransition(treatmentpreparation);
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
                ObjResponse.ErrorMessage = ex.Message;
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