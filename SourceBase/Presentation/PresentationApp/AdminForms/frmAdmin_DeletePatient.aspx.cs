using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Interface.Administration;
using Application.Common;
using Application.Presentation;

public partial class frmAdmin_DeletePatient : LogPage
{
    int PatientId;
    string  theEnrollID;
    string PMTCTNos = "";
    string ARTNos = "";

    protected void Page_Init(object sender, EventArgs e)
    {
        if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
        {
            IQCareMsgBox.Show("SessionExpired", this);
            Response.Redirect("~/frmlogin.aspx",true);
        }
        ////RTyagi..19Feb 07..
        ///***************** Check For User Rights ****************/
        //AuthenticationManager Authentiaction = new AuthenticationManager();
        //if (Request.QueryString["name"] == "Delete")
        //{
        //    if (Authentiaction.HasFunctionRight(ApplicationAccess.DeletePatient, FunctionAccess.Delete, (DataTable)Session["UserRight"]) == false)
        //    {
        //        btndelete.Enabled = false;
        //    }
        //}
       
       
    }
     protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
        {
            IQCareMsgBox.Show("SessionExpired", this);
            Response.Redirect("~/frmlogin.aspx",true);
        }

        //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Administration >> ";
        //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Delete Patient";
        if (!IsPostBack)
        {
            if (Session["AppUserId"] != null)
            {
                ViewState["UserID"] = Session["AppUserId"].ToString();
            }
           
    
            MsgBuilder theMsgBuilder = new MsgBuilder();
            theMsgBuilder.DataElements["FormName"] = "Delete Patient";
            //IQCareMsgBox.ShowConfirm("DeletePatient", theMsgBuilder, btndelete);
            IQCareMsgBox.NotifyAction("Click DELETE button to Proceed Else BACK button to Undo!", "Patient Delete", false, this);
            //return;
        }

        if (Session["PatientID"] != null)
        {
            getPatientDetails(Convert.ToInt32(Session["PatientID"]));
        }
         
        //if (Request.QueryString["PatientID"] != null)
        //{
        //    PatientId = Convert.ToInt32( Request.QueryString["PatientID"].ToString());
        //    //if (PatientId != 0)
        //    //{
        //    //    PatientDetailsShow.Visible = true;
        //    //}
        //    getPatientDetails(PatientId);
        //}
        //BindHeader();
        Page.EnableViewState = true;
        
    }

     
    //private void BindHeader()
    //{
    //   DataSet theFacilityDS = new DataSet();
    //    AuthenticationManager Authentication = new AuthenticationManager();
    //    IFacilitySetup FacilityMaster = (IFacilitySetup)ObjectFactory.CreateInstance("BusinessProcess.Administration.BFacility, BusinessProcess.Administration");

    //    theFacilityDS = FacilityMaster.GetSystemBasedLabels(Convert.ToInt32(Session["SystemId"]), ApplicationAccess.DeletePatient,0);

    //    DataTable theDT = theFacilityDS.Tables[0];
    //    lblpatientenrol.InnerHtml = theDT.Rows[0]["Label"].ToString();
    //    lblExisclinicID.InnerHtml = theDT.Rows[1]["Label"].ToString();


    //}

    protected void btndelete_Click(object sender, EventArgs e)
    {
        //*******Delete the patient(make deleteflag = 1 in mst_patient table) on the basis of patientid *******//
      
        deletePatient();
    }

    

    protected void btnBack_Click(object sender, EventArgs e)
    {
        string theUrl;
        theUrl = string.Format("{0}?mnuClicked={1}", "../frmFindAddPatient.aspx", "DeletePatient"); ;
        Response.Redirect(theUrl);
    }

    protected void theBtn_Click(object sender, EventArgs e)
    {
        ////*******Delete the patient(make deleteflag = 1 in mst_patient table) on the basis of patientid *******//
        //deletePatient();
    }

    private void getPatientDetails(int PatientID)
    {
        IDeletePatient FormManager;

        //*******Get the patient details on the basis of Patient Enrollment Id and show the details.*******//
        FormManager = (IDeletePatient)ObjectFactory.CreateInstance("BusinessProcess.Administration.BDeletePatient, BusinessProcess.Administration");
        DataTable theDT = FormManager.GetPatientDetails(PatientID);

        if (theDT.Rows.Count != 0 )
        {

            //DataTable dtPatientInfo = (DataTable)Session["PatientInformation"];
            
                if (Session["SystemId"].ToString() == "1")
                {
                    lblpatientname.Text = theDT.Rows[0]["LastName"].ToString() + ", " + theDT.Rows[0]["FirstName"].ToString();
                }
                else
                {
                    lblpatientname.Text = theDT.Rows[0]["LastName"].ToString() + ", " + theDT.Rows[0]["MiddleName"].ToString() + " , " + theDT.Rows[0]["FirstName"].ToString();
                }

                lblptnenrollment.Text = theDT.Rows[0]["PatientEnrollmentID"].ToString();
                lblexistingid.Text = theDT.Rows[0]["PatientClinicID"].ToString();
                lblancno.Text = theDT.Rows[0]["ANCNumber"].ToString();
                lblpmtctno.Text = theDT.Rows[0]["PMTCTNumber"].ToString();
                lbladmissionno.Text = theDT.Rows[0]["AdmissionNumber"].ToString();
                lbloutpatientno.Text = theDT.Rows[0]["OutpatientNumber"].ToString();
                PMTCTNos = theDT.Rows[0]["ANCNumber"].ToString() + theDT.Rows[0]["PMTCTNumber"].ToString() + theDT.Rows[0]["AdmissionNumber"].ToString() + theDT.Rows[0]["OutpatientNumber"].ToString();
                ARTNos = theDT.Rows[0]["PatientEnrollmentId"].ToString();
            

            //PatientDetailsShow.Visible = true;

          
            //lblPatientNameValue.Text = theDT.Rows[0]["Name"].ToString();
            //lblHospitalNoValue.Text = theDT.Rows[0]["PatientClinicID"].ToString();
            //theEnrollID = theDT.Rows[0]["PatientEnrollmentID"].ToString();
            //lblPatientEnrollmentNoValue.Text = Session["AppCountryId"].ToString() + "-" + Session["AppPosID"].ToString() + "-" + Session["AppSatelliteId"].ToString() + "-" + theEnrollID.ToString();
            PatientId = Convert.ToInt32( theDT.Rows[0]["ptn_pk"].ToString());
            
            //if (theDT.Rows[0]["Sex"] != System.DBNull.Value)
            //{
            //    if (theDT.Rows[0]["Sex"].ToString() == "16")
            //    {
            //        lblPatientGenderValue.Text = "Male";
            //    }
            //    else
            //    {
            //        lblPatientGenderValue.Text = "Female";
            //    }
            //}
            //*******Check whether the patient is inactive*******//
            //*******Inactive patient show message and disable  delete*******//
            if (theDT.Rows[0]["status"].ToString() == "1")
            {
                IQCareMsgBox.Show("PatientInactive", this);
                return;
            }
            else
            {
                btndelete.Enabled = true;
               
            }
        }


        DataTable dtLabels = (DataTable)Session["DynamicLabels"];
        if (dtLabels != null)
        {
            lblenroll.Text = dtLabels.Rows[4]["Label"].ToString();
            lblClinicNo.Text = dtLabels.Rows[3]["Label"].ToString();
        }

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
    }

    private void deletePatient()
    {
        //*******Delete the patient on the basis of patient id ********//
        IDeletePatient FormManager;
        FormManager = (IDeletePatient)ObjectFactory.CreateInstance("BusinessProcess.Administration.BDeletePatient, BusinessProcess.Administration");
        int theResultRow = FormManager.DeletePatient(Convert.ToInt32(PatientId), Convert.ToInt32( ViewState["UserID"]));

        if (theResultRow == 0)
        {
            IQCareMsgBox.Show("DeletePatientError", this);
            return;
        }
        else
        {
            string theUrl;
            theUrl = string.Format("{0}?mnuClicked={1}", "../frmFindAddPatient.aspx", "DeletePatient"); ;
            Response.Redirect(theUrl);
        }
    }
   
}
