using System;

using Application.Presentation;
using Interface.HIVCE;
using Application.Common;

using HIVCE.Common;

public partial class frmPatientCEOIChronicDisease : System.Web.UI.Page
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
            }
            if (!object.Equals(Session["AppLocationId"], null))
            {
                locationId = Convert.ToInt32(Session["AppLocationId"]);
            }
            if (!object.Equals(Session["AppUserId"], null))
            {
                userId = Convert.ToInt32(Session["AppUserId"]);
            }

            if (!object.Equals(Request.QueryString["data"], null))
            {
                string response = string.Empty;


                if (Request.QueryString["data"].ToString() == "getdata")
                {
                    response = GetOIChronicDiseaseData(Convert.ToInt32(PatientId), locationId);
                    SendResponse(response);
                }
            }
        }
    }

    private string GetOIChronicDiseaseData(int ptn_pk, int locationId)
    {
        string result = string.Empty;
        try
        {
            IClinicalEncounter clinicalencounter = (IClinicalEncounter)ObjectFactory.CreateInstance("HIVCE.BusinessLayer.BLClinicalEncounter, HIVCE.BusinessLayer");
            HIVCE.Common.Entities.OIChronicDiseaseData oiCD = clinicalencounter.GetOIChronicDiseaseData(ptn_pk, locationId);

            result = SerializerUtil.ConverToJson<HIVCE.Common.Entities.OIChronicDiseaseData>(oiCD);
        }
        catch (Exception ex)
        {
            CLogger.WriteLog(ELogLevel.ERROR, "frmPatientCEOIChronicDisease() exception: " + ex.ToString());
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
