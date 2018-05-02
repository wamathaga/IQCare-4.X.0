using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Application.Presentation;
using Interface.Security;
using Application.Common;

public partial class Scheduler_SchedulerHeaderFooter : System.Web.UI.MasterPage
{
#region "User Functions"
    string PMTCTNos = "";
    string ARTNos = "";
    private void AuthenticateRights()
    {
        DataView theDV = new DataView((DataTable)Session["UserRight"]);
        DataTable theDTModule = (DataTable)Session["AppModule"];
        string ModuleId = "";
        foreach (DataRow theDR in theDTModule.Rows)
        {
            if (ModuleId == "")
                ModuleId = theDR["ModuleId"].ToString();
            else
                ModuleId = ModuleId + "," + theDR["ModuleId"].ToString();
        }
        theDV.RowFilter = "ModuleId in (0," + ModuleId + ")";
        DataTable theDT = new DataTable();
        theDT = theDV.ToTable();

        if (ARTNos != null && ARTNos == "")
        {
            //tdART.Visible = false;
            trARTNo.Visible = false;
        }
        if (PMTCTNos != null && PMTCTNos == "")
        {
            //tdPMTCT.Visible = false;
            trPMTCTNo.Visible = false;
        }
        AuthenticationManager Authentication = new AuthenticationManager();

        if (Authentication.HasFeatureRight(ApplicationAccess.FacilitySetup,   theDT) == false)
        {
            mnuAdminFacility.Visible = false;
        }
        //if (Authentication.HasFeatureRight(ApplicationAccess.CustomiseDropDown, (DataTable)Session["UserRight"]) == false)
        //{
        //    mnuAdminCustom.Visible = false;
        //}
        if (Authentication.HasFeatureRight(ApplicationAccess.UserAdministration,   theDT) == false)
        {
            mnuAdminUser.Visible = false;
        }
        if (Authentication.HasFeatureRight(ApplicationAccess.UserGroupAdministration,   theDT) == false)
        {
            mnuAdminUserGroup.Visible = false;
        }
        if (Authentication.HasFeatureRight(ApplicationAccess.DeletePatient,   theDT) == false)
        {
            mnuAdminDeletePatient.Visible = false;
        }
        if (Authentication.HasFeatureRight(ApplicationAccess.CustomReports,   theDT) == false)
        {
            mnuAdminCustomReport.Visible = false;
        }
        if (Authentication.HasFeatureRight(ApplicationAccess.FacilityReports,   theDT) == false)
        {
            mnuAdminFacilityReport.Visible = false;
        }
        if (Authentication.HasFeatureRight(ApplicationAccess.DonorReports,   theDT) == false)
        {
            mnuAdminDonorReport.Visible = false;
        }
        if (Authentication.HasFeatureRight(ApplicationAccess.ConfigureCustomFields,   theDT) == false)
        {
            mnuAdminCustomConfig.Visible = false;
        }
        if (Authentication.HasFeatureRight(ApplicationAccess.Schedular,   theDT) == false)
        {
            mnuScheduler.Visible = false;
        }
    }
#endregion

    protected void Page_Init(object sender, EventArgs e) {
        this.Page.Error += new EventHandler(Page_Error);
    }

    void Page_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();
        CLogger.WriteLog(ELogLevel.ERROR, ex.ToString());
        Response.Write("<script>alert('Application has some issue, we are re-directing you to Login page.') ; location.href='../frmLogin.aspx'</script>");
        Server.ClearError();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
        {
            IQCareMsgBox.Show("SessionExpired", this);
            Response.Redirect("~/frmlogin.aspx",true);
        }
        try
        {
            lblTitle.InnerText = "International Quality Care Patient Management and Monitoring System [" + Session["AppLocation"].ToString() + "]";
            string url = Request.RawUrl.ToString();
            Application["PrvFrm"] = url;
            if (Session["AppUserID"].ToString() == "")
            {
                IQCareMsgBox.Show("SessionExpired", this);
                Response.Redirect("~/frmlogin.aspx",true);
            }
            string UserID = Session["AppUserId"].ToString();
            lblUserName.Text = Session["AppUserName"].ToString();
            if (Session["AppLocation"] != null)
            {
                lblLocation.Text = Session["AppLocation"].ToString();
            }
            IIQCareSystem AdminManager;
            AdminManager = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
            if (Session["AppDateFormat"] != null)
            {
                lblDate.Text = AdminManager.SystemDate().ToString(Session["AppDateFormat"].ToString());
            }
            //####### Delete Patient #############
            string theUrl;
            theUrl = string.Format("{0}?mnuClicked={1}", "../frmFindAddPatient.aspx", "DeletePatient");
            mnuAdminDeletePatient.HRef = theUrl;

            lblversion.Text = AuthenticationManager.AppVersion;
            lblrelDate.Text = AuthenticationManager.ReleaseDate;
            DataTable dtPatientInfo = (DataTable)Session["PatientInformation"];
            if (dtPatientInfo != null)
            {
                if (Session["SystemId"].ToString() == "1")
                    lblpatientname.Text = dtPatientInfo.Rows[0]["LastName"].ToString() + ", " + dtPatientInfo.Rows[0]["FirstName"].ToString();
                else
                    lblpatientname.Text = dtPatientInfo.Rows[0]["LastName"].ToString() + ", " + dtPatientInfo.Rows[0]["MiddleName"].ToString() + " , " + dtPatientInfo.Rows[0]["FirstName"].ToString();

                lblptnenrollment.Text = dtPatientInfo.Rows[0]["PatientEnrollmentID"].ToString();
                lblexistingid.Text = dtPatientInfo.Rows[0]["PatientClinicID"].ToString();
                lblancno.Text = dtPatientInfo.Rows[0]["ANCNumber"].ToString();
                lblpmtctno.Text = dtPatientInfo.Rows[0]["PMTCTNumber"].ToString();
                lbladmissionno.Text = dtPatientInfo.Rows[0]["AdmissionNumber"].ToString();
                lbloutpatientno.Text = dtPatientInfo.Rows[0]["OutpatientNumber"].ToString();
                PMTCTNos = dtPatientInfo.Rows[0]["ANCNumber"].ToString() + dtPatientInfo.Rows[0]["PMTCTNumber"].ToString() + dtPatientInfo.Rows[0]["AdmissionNumber"].ToString() + dtPatientInfo.Rows[0]["OutpatientNumber"].ToString();
                ARTNos = dtPatientInfo.Rows[0]["PatientEnrollmentId"].ToString();
            }
            else
            {
                PanelPatiInfo.Visible = false;
            }
            DataTable dtLabels = (DataTable)Session["DynamicLabels"];
            if (dtLabels != null)
            {
                lblenroll.Text = dtLabels.Rows[4]["Label"].ToString();
                lblClinicNo.Text = dtLabels.Rows[3]["Label"].ToString();
            }
            AuthenticateRights();
            LnkPwd_OnClick(sender, e);
            ////////////////////////

        }
        catch (Exception err)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
        }
    }

    protected void LnkPwd_OnClick(object sender, EventArgs e)
    {

    }
}
