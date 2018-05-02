using System;
using System.Data;
using Application.Common;
using Application.Presentation;
using Entities.Common;
using HIVCE.Common;
using Interface.Clinical;
using Interface.Service;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web;

namespace PresentationApp.ClinicalForms
{
    public partial class frmClinicalSummary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int PatientId = 0;
            int visitPK = 0;
            int locationId = 0;
            int userId = 0;
            if (!IsPostBack)
            {
                //try
                //{
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
                    if (!object.Equals(Session["TechnicalAreaName"], null))
                    {
                        Label lblFacility = (Label)this.ucCaseSummary1.FindControl("lblFacility");
                        if (lblFacility != null)
                            lblFacility.Text = Session["TechnicalAreaName"].ToString();
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
                            response = GetClinicalSummaryData(Convert.ToInt32(PatientId), visitPK, locationId);
                            SendResponse(response);
                        }
                        if (Request.QueryString["data"].ToString() == "savedata")
                        {
                            System.IO.StreamReader sr = new System.IO.StreamReader(Request.InputStream);
                            string jsonString = "";
                            jsonString = sr.ReadToEnd();
                            response = SaveUpdateClinicalSummaryData(jsonString, PatientId, visitPK, locationId, userId);
                            SendResponse(response);
                        }

                    }
                //}
                //catch (Exception ex)
                //{
                //    //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),"err_msg","alert("+ex.Message+");",true);
                //    Response.Write("<script>alert(" + ex.Message + ");</script>");
                //}
            }
        }
        public void Authenticate()
        {
            HtmlButton btnSaveCS = (HtmlButton)this.ucCaseSummary1.FindControl("btnSave");
            HtmlButton btnPrintCS = (HtmlButton)this.ucCaseSummary1.FindControl("btnPrint");
            HtmlButton btnCloseCS = (HtmlButton)this.ucCaseSummary1.FindControl("btnClose");

            HtmlButton btnSaveEval = (HtmlButton)this.ucEvaluation1.FindControl("btnSave");
            HtmlButton btnPrintEval = (HtmlButton)this.ucEvaluation1.FindControl("btnPrint");
            HtmlButton btnCloseEval = (HtmlButton)this.ucEvaluation1.FindControl("btnClose");

            AuthenticationManager authMgr = new AuthenticationManager();

            /********** getting the data from XML file *******************************/
            //int featureId = 299;
            int featureId = ApplicationAccess.AdherenceBarriers;
            bool enable = true;
            int visitId = Convert.ToInt32(Session["PatientVisitId"]);
            //if (Request.QueryString["name"] == "Delete")
            //{
            //    btnSaveCS.Visible = false;
            //    btnPrintCS.Visible = true;
            //}
            //else
            //{
            //    btnSaveCS.Attributes["disabled"] = "true";
            //    btnPrintCS.Visible = false;
            //}

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
                    //btnDeleteForm.Visible = true;
                }
                else if (authMgr.HasFunctionRight(featureId, FunctionAccess.Delete, (DataTable)Session["UserRight"]) == false)
                {
                    //btnDeleteForm.Visible = true;
                    //btnDeleteForm.Attributes["disabled"] = "true";
                }
            }

            if (enable)
            {
                btnSaveCS.Attributes.Remove("disabled");
            }

            if (Request.QueryString["name"] == "Delete")
            {
                btnSaveCS.Attributes["disabled"] = "true";               
            }
        }
        private void SendResponse(string data)
        {
            //try
            //{
                Response.Clear();
                Response.ContentType = "application/json";
                Response.AddHeader("Content-type", "text/json");
                Response.AddHeader("Content-type", "application/json");
                Response.Write(data);
                Response.End();
                //HttpContext.Current.ApplicationInstance.CompleteRequest();
            //}
            //catch (Exception ex)
            //{
            //}
        }
        private string GetClinicalSummaryData(int ptn_pk, int visitPK, int locationId)
        {
            string result = string.Empty;
            try
            {
                IClinicalSummaryForm ipatientClinicalSummary;
                ipatientClinicalSummary = (IClinicalSummaryForm)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BClinicalSummary, BusinessProcess.Clinical");
                ClinicalSummary patientClinicalSummary = ipatientClinicalSummary.GetClinicalSummaryData(ptn_pk, visitPK, locationId);

                result = SerializerUtil.ConverToJson<ClinicalSummary>(patientClinicalSummary);
            }
            catch (Exception ex)
            {
                string str = "ptn_pk: " + ptn_pk.ToString() + ",visitPK: " + visitPK.ToString() + ",locationId:" + locationId.ToString();
                CLogger.WriteLog(ELogLevel.ERROR, "GetClinicalSummaryData() exception: " + str + "-" + ex.ToString());
                ResponseType response = new ResponseType() { Success = EnumUtil.GetEnumDescription(Success.False) };
                result = SerializerUtil.ConverToJson<ResponseType>(response);
            }
            finally
            {

            }
            return result;
        }
        private string SaveUpdateClinicalSummaryData(string nodeJson, int ptn_pk, int visitPK, int locationId, int userId)
        {
            string result = string.Empty;
            ResponseType ObjResponse = new ResponseType();
            try
            {
                ClinicalSummary clinicalsummarydata = SerializerUtil.ConverToObject<ClinicalSummary>(nodeJson);
                IClinicalSummaryForm iclinicalsummarydata;
                iclinicalsummarydata = (IClinicalSummaryForm)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BClinicalSummary, BusinessProcess.Clinical");

                clinicalsummarydata.Ptn_pk = ptn_pk;
                clinicalsummarydata.Visit_Id = visitPK;
                clinicalsummarydata.location_id = locationId;              
                int i = iclinicalsummarydata.SaveUpdateClinicalSummaryData(clinicalsummarydata, userId);


                //Session["PatientVisitId"] = visitId.ToString();
                //this.hidVId.Value = visitId.ToString();

                if (i>0)
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
    }
}