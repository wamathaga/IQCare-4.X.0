using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Interface.Clinical;
using Interface.Security;
using Application.Common;
using Application.Interface;
using Application.Presentation;
using Interface.Administration;
using System.Text;

public partial class frmAddTechnicalArea : BasePage
{
    string ObjFactoryParameter = "BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical";
    Hashtable GetValuefromHT;
    int flag = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
        {
            IQCareMsgBox.Show("SessionExpired", this);
            Response.Redirect("~/frmlogin.aspx", true);
        }

        (Master.FindControl("levelTwoNavigationUserControl1").FindControl("PanelPatiInfo") as Panel).Visible = false;
        //appDateimg2.Attributes.Add("onclick", "w_displayDatePicker('" + txtenrollmentDate.ClientID + "');");
        //imgDtReEnroll.Attributes.Add("onclick", "w_displayDatePicker('" + txtReEnrollmentDate.ClientID + "');");
        //txtenrollmentDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
        //txtenrollmentDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3'); isCheckValidDate('" + Application["AppCurrentDate"] + "', '" + txtenrollmentDate.ClientID + "', '" + txtenrollmentDate.ClientID + "');");
        // txtReEnrollmentDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
        //txtReEnrollmentDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3'); isCheckValidDate('" + Application["AppCurrentDate"] + "', '" + txtReEnrollmentDate.ClientID + "', '" + txtReEnrollmentDate.ClientID + "');");

        //   txtenrollmentDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3');");
        Ajax.Utility.RegisterTypeForAjax(typeof(frmAddTechnicalArea));
        //appDateimg2.Visible = true;
        txtenrollmentDate.Disabled = false;
        trReEnroll.Visible = false;
        btnReEnollPatient.Visible = false;
        if (!IsPostBack)
        {


            BindDropdown();
            LoadPatientDetail();

            ClientScript.RegisterStartupScript(this.GetType(), "Changing", "<script>fnChange();</script>");

            if (Session["TechnicalAreaName"] != null)
                ddlTecharea.Items.FindByText(Session["TechnicalAreaName"].ToString()).Selected = true;

            //Automatically sets the technical area that you are in and cannot be changed
            string moduleId = String.Empty;

            if (String.IsNullOrEmpty(Convert.ToString(Request.QueryString["mod"])))
                moduleId = ddlTecharea.SelectedValue.ToString();
            else
                moduleId = Convert.ToString(Request.QueryString["mod"]);

            if (!String.IsNullOrEmpty(moduleId))
            {
                tHeading.InnerText = "Service Enrollment";
                btnContinue.Visible = false;
                lblContinue.Visible = false;
                ddlTecharea.SelectedValue = moduleId;
                ddlTecharea.Enabled = true;
                Session["TechnicalAreaName"] = Request.QueryString["srvNm"];
                Session["TechnicalAreaId"] = moduleId;

                flag = 1;
                LoadModuleIdentifiers(Convert.ToInt32(moduleId));

            }
        }
        else
        {

            int moduleId = 0;
            if (object.Equals(Request.QueryString["mod"], null) == false && Convert.ToInt32(Request.QueryString["mod"]) > 0)
            {
                moduleId = Convert.ToInt32(Request.QueryString["mod"]);
            }
            else if (Convert.ToInt32(ddlTecharea.SelectedValue) > 0)
            {
                moduleId = Convert.ToInt32(ddlTecharea.SelectedValue);
            }
            if (moduleId > 0)
                LoadModuleIdentifiers(Convert.ToInt32(moduleId));
        }
    }
    private void LoadPatientDetail()
    {
        ViewState["AutoPopulated"] = "False";
        int patientID = Convert.ToInt32(Session["PatientId"]);

        GetValuefromHT = (Hashtable)Session["htPtnRegParameter"];
        if (patientID == 0)
        {
            lblname.Text = GetValuefromHT["FirstName"].ToString() + ' ' + GetValuefromHT["MiddleName"].ToString() + ' ' + GetValuefromHT["LastName"].ToString();
            string Gender = GetValuefromHT["Gender"].ToString() == "16" ? "Male" : "Female";
            Session["PatientSex"] = Gender;
            lblsex.Text = Gender;
            lbldob.Text = GetValuefromHT["DOB"].ToString();

            //DateTime today = DateTime.Today;
            //int age = today.Year - Convert.ToDateTime(GetValuefromHT["DOB"].ToString()).Year;
            //Session["PatientAge"] = age;
            //int ageMonth = today.Month - Convert.ToDateTime(GetValuefromHT["DOB"].ToString()).Month;
            //Session["PatientAgeMonths"] = ageMonth;
            CalculateYourAge(Convert.ToDateTime(GetValuefromHT["DOB"].ToString()));

            ViewState["RegistrationDate"] = GetValuefromHT["RegistrationDate"].ToString();
            //lblIQno.Text = theDS.Tables[0].Rows[0]["IQNumber"].ToString();
            btnContinue.Visible = false;
            lblContinue.Visible = false;
        }


        IPatientRegistration ptnMgr = (IPatientRegistration)ObjectFactory.CreateInstance(ObjFactoryParameter);

        DataSet theDS = ptnMgr.GetPatientRegistration(patientID, 12);
        if (theDS.Tables[0].Rows.Count > 0)
        {
            lblname.Text = theDS.Tables[0].Rows[0]["Firstname"].ToString() + ' ' + theDS.Tables[0].Rows[0]["Middlename"].ToString() + ' ' + theDS.Tables[0].Rows[0]["Lastname"].ToString();
            lblsex.Text = theDS.Tables[0].Rows[0]["sex"].ToString();
            lbldob.Text = theDS.Tables[0].Rows[0]["dob"].ToString();
            lblIQno.Text = theDS.Tables[0].Rows[0]["IQNumber"].ToString();
            Session["PatientSex"] = theDS.Tables[0].Rows[0]["sex"].ToString();
            Session["PatientAge"] = theDS.Tables[0].Rows[0]["AGE"].ToString(); // +"." + theDS.Tables[0].Rows[0]["AgeInMonths"].ToString();
            if (Convert.ToInt16(theDS.Tables[0].Rows[0]["PatientTypeId"]) > 0)
            {
                ddlPatientType.Items.FindByValue(theDS.Tables[0].Rows[0]["PatientTypeId"].ToString()).Selected = true;
            }
        }
        if (theDS.Tables[2].Rows.Count > 0)
        {
            if (Convert.ToInt32(Session["PatientId"]) > 0)
            {
                ViewState["RegistrationDate"] = theDS.Tables[2].Rows[0]["VisitDate"].ToString();
            }
            else
            {
                ViewState["RegistrationDate"] = GetValuefromHT["RegistrationDate"].ToString();
            }

        }
    }
    //Calculate Year and Month by Rahmat(06-Feb-2018)
    private void CalculateYourAge(DateTime Dob)
    {
        DateTime Now = DateTime.Now;
        int Years = new DateTime(DateTime.Now.Subtract(Dob).Ticks).Year - 1;
        DateTime PastYearDate = Dob.AddYears(Years);
        int Months = 0;
        for (int i = 1; i <= 12; i++)
        {
            if (PastYearDate.AddMonths(i) == Now)
            {
                Months = i;
                break;
            }
            else if (PastYearDate.AddMonths(i) >= Now)
            {
                Months = i - 1;
                break;
            }
        }
        Session["PatientAge"] = Years;
        Session["PatientAgeMonths"] = Months;
    }
    protected void BindDropdown()
    {
        BindFunctions BindManager = new BindFunctions();
        IPatientRegistration ptnMgr = (IPatientRegistration)ObjectFactory.CreateInstance(ObjFactoryParameter);
        DataSet DSModules = new DataSet();
        DataTable theDT = new DataTable();
        DataTable dtPatientType = new DataTable();
        if (Convert.ToInt32(Session["AppUserId"]) == 1)
        {
            DSModules = ptnMgr.GetModuleNames(Convert.ToInt32(Session["AppLocationId"]), Convert.ToInt32(Session["AppUserId"]));
            theDT = DSModules.Tables[0];
            dtPatientType = DSModules.Tables[4];
            DataRow[] toBeDeleted;
            toBeDeleted = theDT.Select("ModuleName = 'RECORDS'");
            if (toBeDeleted.Length > 0)
            {
                foreach (DataRow dr in toBeDeleted)
                {
                    theDT.Rows.Remove(dr);
                    theDT.AcceptChanges();
                }
            }
        }
        else if (Convert.ToInt32(Session["AppUserId"]) > 1)
        {
            DSModules = ptnMgr.GetModuleNames(Convert.ToInt32(Session["AppLocationId"]), Convert.ToInt32(Session["AppUserId"]));
            theDT = DSModules.Tables[2];
            dtPatientType = DSModules.Tables[4];
            DataRow[] toBeDeleted;
            toBeDeleted = theDT.Select("ModuleName = 'RECORDS'");
            if (toBeDeleted.Length > 0)
            {
                foreach (DataRow dr in toBeDeleted)
                {
                    theDT.Rows.Remove(dr);
                    theDT.AcceptChanges();
                }
            }
        }
        if (theDT.Rows.Count > 0)
        {
            BindManager.BindCombo(ddlTecharea, BindModuleByBusinessRules(theDT, DSModules.Tables[1]), "ModuleName", "ModuleID");
            ptnMgr = null;
        }
        if (dtPatientType.Rows.Count > 0)
        {
            BindManager.BindCombo(ddlPatientType, dtPatientType, "Name", "Id");

        }
        ptnMgr = null;
    }
    public DataTable BindModuleByBusinessRules(DataTable dt, DataTable dtbusinessrules)
    {

        DataTable btable = new DataTable();
        btable.Columns.Add("ModuleName", typeof(string));
        btable.Columns.Add("ModuleID", typeof(string));

        foreach (DataRow r in dt.Rows)
        {


            DataView dv = new DataView(dtbusinessrules);
            dv.RowFilter = "Moduleid=" + r["ModuleID"].ToString() + "";
            DataTable dtfilter = dv.ToTable();


            Hashtable htrecord = new Hashtable();
            if (dtfilter.Rows.Count > 0)
            {
                DataRow[] resultset1 = dtfilter.Select("SetType=1");
                DataRow[] resultset2 = dtfilter.Select("SetType=2");
                int set1 = resultset1.Length;
                DataRow[] set1rulesAge = dtfilter.Select("SetType=1 and BusRuleId=16");
                DataRow[] set1rulesmale = dtfilter.Select("SetType=1 and BusRuleId=14");
                DataRow[] set1rulesfemale = dtfilter.Select("SetType=1 and BusRuleId=15");

                int set2 = resultset2.Length;
                DataRow[] set2rulesAge = dtfilter.Select("SetType=2 and BusRuleId=16");
                DataRow[] set2rulesmale = dtfilter.Select("SetType=2 and BusRuleId=14");
                DataRow[] set2rulesfemale = dtfilter.Select("SetType=2 and BusRuleId=15");


                if (set1 > 0)
                {
                    if (set1 == 3)
                    {
                        foreach (DataRow DR in set1rulesAge)
                        {
                            if (Convert.ToString(DR["BusRuleId"]) == "16" && (DR["Value"] != System.DBNull.Value) && (DR["Value1"] != System.DBNull.Value))
                            {
                                if ((Convert.ToDecimal(Session["PatientAge"]) >= Convert.ToDecimal(DR["Value"]) && Convert.ToDecimal(Session["PatientAge"]) <= Convert.ToDecimal(DR["Value1"])) && ((Session["PatientSex"].ToString() == "Male") || Session["PatientSex"].ToString() == "Female"))
                                {
                                    if (!(htrecord.Contains(r["ModuleName"].ToString())))
                                    {
                                        DataRow theDR = btable.NewRow();
                                        theDR["ModuleName"] = r["ModuleName"].ToString();
                                        theDR["ModuleID"] = r["ModuleID"].ToString();
                                        btable.Rows.Add(theDR);
                                        htrecord.Add(r["ModuleName"].ToString(), r["ModuleName"].ToString());
                                    }
                                }

                            }
                        }
                    }
                    if (set1 == 2)
                    {
                        foreach (DataRow DR in set1rulesAge)
                        {
                            if (Convert.ToString(DR["BusRuleId"]) == "16" && (DR["Value"] != System.DBNull.Value) && (DR["Value1"] != System.DBNull.Value))
                            {
                                if ((Convert.ToDecimal(Session["PatientAge"]) >= Convert.ToDecimal(DR["Value"]) && Convert.ToDecimal(Session["PatientAge"]) <= Convert.ToDecimal(DR["Value1"])) && Session["PatientSex"].ToString() == "Male")
                                {
                                    foreach (DataRow DR1 in set1rulesmale)
                                    {
                                        if (Convert.ToString(DR1["BusRuleId"]) == "14" && Session["PatientSex"].ToString() == "Male")
                                        {

                                            if (!(htrecord.Contains(r["ModuleName"].ToString())))
                                            {
                                                DataRow theDR = btable.NewRow();
                                                theDR["ModuleName"] = r["ModuleName"].ToString();
                                                theDR["ModuleID"] = r["ModuleID"].ToString();
                                                btable.Rows.Add(theDR);
                                                htrecord.Add(r["ModuleName"].ToString(), r["ModuleName"].ToString());
                                            }
                                        }
                                    }
                                }
                                else if ((Convert.ToDecimal(Session["PatientAge"]) >= Convert.ToDecimal(DR["Value"]) && Convert.ToDecimal(Session["PatientAge"]) <= Convert.ToDecimal(DR["Value1"])) && Session["PatientSex"].ToString() == "Female")
                                {
                                    foreach (DataRow DR1 in set1rulesfemale)
                                    {
                                        if (Convert.ToString(DR1["BusRuleId"]) == "15" && Session["PatientSex"].ToString() == "Female")
                                        {
                                            if (!(htrecord.Contains(r["ModuleName"].ToString())))
                                            {
                                                DataRow theDR = btable.NewRow();
                                                theDR["ModuleName"] = r["ModuleName"].ToString();
                                                theDR["ModuleID"] = r["ModuleID"].ToString();
                                                btable.Rows.Add(theDR);
                                                htrecord.Add(r["ModuleName"].ToString(), r["ModuleName"].ToString());
                                            }
                                        }
                                    }

                                }

                            }
                        }
                        if (set1rulesAge.Length == 0)
                        {
                            foreach (DataRow DR in set1rulesmale)
                            {
                                if (Convert.ToString(DR["BusRuleId"]) == "14" && Session["PatientSex"].ToString() == "Male")
                                {
                                    if (!(htrecord.Contains(r["ModuleName"].ToString())))
                                    {
                                        DataRow theDR = btable.NewRow();
                                        theDR["ModuleName"] = r["ModuleName"].ToString();
                                        theDR["ModuleID"] = r["ModuleID"].ToString();
                                        btable.Rows.Add(theDR);
                                        htrecord.Add(r["ModuleName"].ToString(), r["ModuleName"].ToString());
                                    }
                                }

                            }
                            foreach (DataRow DR in set1rulesfemale)
                            {
                                if (Convert.ToString(DR["BusRuleId"]) == "15" && Session["PatientSex"].ToString() == "Female")
                                {
                                    if (!(htrecord.Contains(r["ModuleName"].ToString())))
                                    {
                                        DataRow theDR = btable.NewRow();
                                        theDR["ModuleName"] = r["ModuleName"].ToString();
                                        theDR["ModuleID"] = r["ModuleID"].ToString();
                                        btable.Rows.Add(theDR);
                                        htrecord.Add(r["ModuleName"].ToString(), r["ModuleName"].ToString());
                                    }
                                }
                            }
                        }
                    }
                    if (set1 == 1)
                    {
                        foreach (DataRow DR in set1rulesAge)
                        {
                            if (Convert.ToString(DR["BusRuleId"]) == "16" && (DR["Value"] != System.DBNull.Value) && (DR["Value1"] != System.DBNull.Value))
                            {
                                if (Convert.ToDecimal(Session["PatientAge"]) >= Convert.ToDecimal(DR["Value"]) && Convert.ToDecimal(Session["PatientAge"]) <= Convert.ToDecimal(DR["Value1"]))
                                {
                                    if (!(htrecord.Contains(r["ModuleName"].ToString())))
                                    {
                                        DataRow theDR = btable.NewRow();
                                        theDR["ModuleName"] = r["ModuleName"].ToString();
                                        theDR["ModuleID"] = r["ModuleID"].ToString();
                                        btable.Rows.Add(theDR);
                                        htrecord.Add(r["ModuleName"].ToString(), r["ModuleName"].ToString());
                                    }
                                }


                            }
                        }
                    }
                    if (set1 == 1)
                    {
                        foreach (DataRow DR in set1rulesmale)
                        {
                            if (Convert.ToString(DR["BusRuleId"]) == "14" && Session["PatientSex"].ToString() == "Male")
                            {
                                if (!(htrecord.Contains(r["ModuleName"].ToString())))
                                {
                                    DataRow theDR = btable.NewRow();
                                    theDR["ModuleName"] = r["ModuleName"].ToString();
                                    theDR["ModuleID"] = r["ModuleID"].ToString();
                                    btable.Rows.Add(theDR);
                                    htrecord.Add(r["ModuleName"].ToString(), r["ModuleName"].ToString());
                                }
                            }

                        }
                    }
                    if (set1 == 1)
                    {
                        foreach (DataRow DR in set1rulesfemale)
                        {
                            if (Convert.ToString(DR["BusRuleId"]) == "15" && Session["PatientSex"].ToString() == "Female")
                            {
                                if (!(htrecord.Contains(r["ModuleName"].ToString())))
                                {
                                    DataRow theDR = btable.NewRow();
                                    theDR["ModuleName"] = r["ModuleName"].ToString();
                                    theDR["ModuleID"] = r["ModuleID"].ToString();
                                    btable.Rows.Add(theDR);
                                    htrecord.Add(r["ModuleName"].ToString(), r["ModuleName"].ToString());
                                }
                            }
                        }
                    }
                }
                //set type 2

                if (set2 > 0)
                {
                    if (set2 == 3)
                    {
                        foreach (DataRow DR in set2rulesAge)
                        {
                            if (Convert.ToString(DR["BusRuleId"]) == "16" && (DR["Value"] != System.DBNull.Value) && (DR["Value1"] != System.DBNull.Value))
                            {
                                if ((Convert.ToDecimal(Session["PatientAge"]) >= Convert.ToDecimal(DR["Value"]) && Convert.ToDecimal(Session["PatientAge"]) <= Convert.ToDecimal(DR["Value1"])) && ((Session["PatientSex"].ToString() == "Male") || Session["PatientSex"].ToString() == "Female"))
                                {
                                    if (!(htrecord.Contains(r["ModuleName"].ToString())))
                                    {
                                        DataRow theDR = btable.NewRow();
                                        theDR["ModuleName"] = r["ModuleName"].ToString();
                                        theDR["ModuleID"] = r["ModuleID"].ToString();
                                        btable.Rows.Add(theDR);
                                        htrecord.Add(r["ModuleName"].ToString(), r["ModuleName"].ToString());
                                    }
                                }

                            }
                        }
                    }
                    if (set2 == 2)
                    {
                        foreach (DataRow DR in set2rulesAge)
                        {
                            if (Convert.ToString(DR["BusRuleId"]) == "16" && (DR["Value"] != System.DBNull.Value) && (DR["Value1"] != System.DBNull.Value))
                            {
                                if ((Convert.ToDecimal(Session["PatientAge"]) >= Convert.ToDecimal(DR["Value"]) && Convert.ToDecimal(Session["PatientAge"]) <= Convert.ToDecimal(DR["Value1"])) && Session["PatientSex"].ToString() == "Male")
                                {
                                    foreach (DataRow DR1 in set2rulesmale)
                                    {
                                        if (Convert.ToString(DR1["BusRuleId"]) == "14" && Session["PatientSex"].ToString() == "Male")
                                        {
                                            if (!(htrecord.Contains(r["ModuleName"].ToString())))
                                            {
                                                DataRow theDR = btable.NewRow();
                                                theDR["ModuleName"] = r["ModuleName"].ToString();
                                                theDR["ModuleID"] = r["ModuleID"].ToString();
                                                btable.Rows.Add(theDR);
                                                htrecord.Add(r["ModuleName"].ToString(), r["ModuleName"].ToString());
                                            }
                                        }

                                    }
                                }
                                else if ((Convert.ToDecimal(Session["PatientAge"]) >= Convert.ToDecimal(DR["Value"]) && Convert.ToDecimal(Session["PatientAge"]) <= Convert.ToDecimal(DR["Value1"])) && Session["PatientSex"].ToString() == "Female")
                                {
                                    foreach (DataRow DR1 in set2rulesfemale)
                                    {
                                        if (Convert.ToString(DR1["BusRuleId"]) == "15" && Session["PatientSex"].ToString() == "Female")
                                        {
                                            if (!(htrecord.Contains(r["ModuleName"].ToString())))
                                            {
                                                DataRow theDR = btable.NewRow();
                                                theDR["ModuleName"] = r["ModuleName"].ToString();
                                                theDR["ModuleID"] = r["ModuleID"].ToString();
                                                btable.Rows.Add(theDR);
                                                htrecord.Add(r["ModuleName"].ToString(), r["ModuleName"].ToString());
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (set2rulesAge.Length == 0)
                        {
                            foreach (DataRow DR in set2rulesmale)
                            {
                                if (Convert.ToString(DR["BusRuleId"]) == "14" && Session["PatientSex"].ToString() == "Male")
                                {
                                    if (!(htrecord.Contains(r["ModuleName"].ToString())))
                                    {
                                        DataRow theDR = btable.NewRow();
                                        theDR["ModuleName"] = r["ModuleName"].ToString();
                                        theDR["ModuleID"] = r["ModuleID"].ToString();
                                        btable.Rows.Add(theDR);
                                        htrecord.Add(r["ModuleName"].ToString(), r["ModuleName"].ToString());
                                    }
                                }

                            }
                            foreach (DataRow DR in set2rulesfemale)
                            {
                                if (Convert.ToString(DR["BusRuleId"]) == "15" && Session["PatientSex"].ToString() == "Female")
                                {
                                    if (!(htrecord.Contains(r["ModuleName"].ToString())))
                                    {
                                        DataRow theDR = btable.NewRow();
                                        theDR["ModuleName"] = r["ModuleName"].ToString();
                                        theDR["ModuleID"] = r["ModuleID"].ToString();
                                        btable.Rows.Add(theDR);
                                        htrecord.Add(r["ModuleName"].ToString(), r["ModuleName"].ToString());
                                    }
                                }
                            }
                        }
                    }
                    if (set2 == 1)
                    {
                        foreach (DataRow DR in set2rulesAge)
                        {
                            if (Convert.ToString(DR["BusRuleId"]) == "16" && (DR["Value"] != System.DBNull.Value) && (DR["Value1"] != System.DBNull.Value))
                            {
                                if (Convert.ToDecimal(Session["PatientAge"]) >= Convert.ToDecimal(DR["Value"]) && Convert.ToDecimal(Session["PatientAge"]) <= Convert.ToDecimal(DR["Value1"]))
                                {
                                    if (!(htrecord.Contains(r["ModuleName"].ToString())))
                                    {
                                        DataRow theDR = btable.NewRow();
                                        theDR["ModuleName"] = r["ModuleName"].ToString();
                                        theDR["ModuleID"] = r["ModuleID"].ToString();
                                        btable.Rows.Add(theDR);
                                        htrecord.Add(r["ModuleName"].ToString(), r["ModuleName"].ToString());
                                    }
                                }


                            }
                        }
                    }
                    if (set2 == 1)
                    {
                        foreach (DataRow DR in set2rulesmale)
                        {
                            if (Convert.ToString(DR["BusRuleId"]) == "14" && Session["PatientSex"].ToString() == "Male")
                            {
                                if (!(htrecord.Contains(r["ModuleName"].ToString())))
                                {
                                    DataRow theDR = btable.NewRow();
                                    theDR["ModuleName"] = r["ModuleName"].ToString();
                                    theDR["ModuleID"] = r["ModuleID"].ToString();
                                    btable.Rows.Add(theDR);
                                    htrecord.Add(r["ModuleName"].ToString(), r["ModuleName"].ToString());
                                }
                            }

                        }
                    }
                    if (set2 == 1)
                    {
                        foreach (DataRow DR in set2rulesfemale)
                        {
                            if (Convert.ToString(DR["BusRuleId"]) == "15" && Session["PatientSex"].ToString() == "Female")
                            {
                                if (!(htrecord.Contains(r["ModuleName"].ToString())))
                                {
                                    DataRow theDR = btable.NewRow();
                                    theDR["ModuleName"] = r["ModuleName"].ToString();
                                    theDR["ModuleID"] = r["ModuleID"].ToString();
                                    btable.Rows.Add(theDR);
                                    htrecord.Add(r["ModuleName"].ToString(), r["ModuleName"].ToString());
                                }
                            }
                        }
                    }
                }



            }
            else
            {

                DataRow theDR = btable.NewRow();
                theDR["ModuleName"] = r["ModuleName"].ToString();
                theDR["ModuleID"] = r["ModuleID"].ToString();
                btable.Rows.Add(theDR);


            }


        }
        return btable;
    }
    private Boolean FieldValidation()
    {
        IIQCareSystem IQCareSecurity = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
        DateTime theCurrentDate = (DateTime)IQCareSecurity.SystemDate();
        IQCareUtils theUtils = new IQCareUtils();
        //by Akhil
        DateTime RegistrationDate = DateTime.Today;
        if (!String.IsNullOrEmpty(ViewState["RegistrationDate"].ToString()))
            RegistrationDate = Convert.ToDateTime(ViewState["RegistrationDate"]);

        int textblankstatus = GetBlankTextboxesstatus(1);

        if (ddlTecharea.SelectedValue == "1" && lblsex.Text == "Male")
        {
            if (Convert.ToInt32(Session["PatientAge"]) > 2)
            {
                IQCareMsgBox.Show("PMTCTMaleRegister", this);
                return false;
            }
            else if ((Convert.ToInt32(Session["PatientAge"]) == 2) && (Convert.ToInt32(Session["PatientAgeMonths"]) != 0))
            {
                IQCareMsgBox.Show("PMTCTMaleRegister", this);
                return false;
            }
            return true;
        }


        if (txtenrollmentDate.Value == "")
        {
            //MsgBuilder theBuilder = new MsgBuilder();
            //theBuilder.DataElements["Control"] = "Enrollment Date";
            //IQCareMsgBox.Show("BlankTextBox", theBuilder, this);

            //string script = "<script language = 'javascript' defer ='defer' id = 'confirmenrolment'>\n";
            //script += "alert('Enrollment Date cannot be blank.');\n";
            //script += "document.getElementById('extruderLeftmain').style.display = 'inline';\n";
            //script += "</script>\n";
            //ClientScript.RegisterStartupScript(this.GetType(), "confirmenrolment", script);

            IQCareMsgBox.NotifyAction("Enrollment Date cannot be blank.", "Patient Enrollment", false, this, "");
            txtenrollmentDate.Focus();
            return false;
        }
        DateTime theEnrolDate = Convert.ToDateTime(theUtils.MakeDate(txtenrollmentDate.Value));
        DateTime theReEnrolDate = Convert.ToDateTime(theUtils.MakeDate(txtReEnrollmentDate.Value));
        if (theEnrolDate > theCurrentDate)
        {
            //IQCareMsgBox.Show("EnrolDate", this);

            //string script = "<script language = 'javascript' defer ='defer' id = 'confirmenrolment'>\n";
            //script += "alert('Enrollment date cannot be greater than the current date.');\n";
            //script += "document.getElementById('extruderLeftmain').style.display = 'inline';\n";
            //script += "</script>\n";
            //ClientScript.RegisterStartupScript(this.GetType(), "confirmenrolment", script);

            IQCareMsgBox.NotifyAction("Enrollment date cannot be greater than the current date.", "Patient Enrollment", false, this, "");

            txtenrollmentDate.Focus();
            return false;
        }
        if (theEnrolDate < RegistrationDate)
        {
            //IQCareMsgBox.Show("RegistrationDate", this);

            //string script = "<script language = 'javascript' defer ='defer' id = 'confirmenrolment'>\n";
            //script += "alert('Enrollment date cannot be Less than Registration date.');\n";
            //script += "document.getElementById('extruderLeftmain').style.display = 'inline';\n";
            //script += "</script>\n";
            //ClientScript.RegisterStartupScript(this.GetType(), "confirmenrolment", script);

            IQCareMsgBox.NotifyAction("Enrollment date cannot be Less than Registration date.", "Patient Enrollment", false, this, "");

            txtenrollmentDate.Focus();
            return false;
        }
        if (ViewState["CareEndedDate"] != null)
        {
            if (theReEnrolDate > theCurrentDate)
            {
                //IQCareMsgBox.Show("ReEnrolDate", this);

                //string script = "<script language = 'javascript' defer ='defer' id = 'confirmenrolment'>\n";
                //script += "alert('Re-Enrollment date cannot be greater than the current date.');\n";
                //script += "document.getElementById('extruderLeftmain').style.display = 'inline';\n";
                //script += "</script>\n";
                //ClientScript.RegisterStartupScript(this.GetType(), "confirmenrolment", script);

                IQCareMsgBox.NotifyAction("Re-Enrollment date cannot be greater than the current date.", "Patient Enrollment", false, this, "");

                txtReEnrollmentDate.Focus();
                return false;
            }

            if (theReEnrolDate < (DateTime)ViewState["CareEndedDate"])
            {
                //string script = "<script language = 'javascript' defer ='defer' id = 'confirmreenrolment'>\n";
                //script += "alert('Re-Enrollment date cannot be Less than Care Termination date');\n";
                //script += "document.getElementById('extruderLeftmain').style.display = 'inline';\n";
                //script += "</script>\n";
                //ClientScript.RegisterStartupScript(this.GetType(), "confirmreenrolment", script);

                IQCareMsgBox.NotifyAction("Re-Enrollment date cannot be Less than Care Termination date.", "Patient Enrollment", false, this, "");

                //IQCareMsgBox.Show("RegistrationCareEndDate", this);

                txtReEnrollmentDate.Focus();
                return false;
            }
        }

        if (textblankstatus.ToString() == "0")
        {
            //IQCareMsgBox.Show("IdentifierRequired", this);

            //string script = "<script language = 'javascript' defer ='defer' id = 'confirmenrolment'>\n";
            //script += "alert('Atleast one identifier field is required to register in service.');\n";
            //script += "document.getElementById('extruderLeftmain').style.display = 'inline';\n";
            //script += "</script>\n";
            //ClientScript.RegisterStartupScript(this.GetType(), "confirmenrolment", script);

            IQCareMsgBox.NotifyAction("Atleast one identifier field is required to register in service.", "Patient Enrollment", false, this, "");
            return false;
        }

        return true;
    }

    protected void btnSaveContinue_Click(object sender, EventArgs e)
    {
        if (FieldValidation() == false)
            return;


        SavePatientRegistration();
        ClientScript.RegisterStartupScript(this.GetType(), "Changingextruder", "<script language = 'javascript' defer ='defer' id = 'confirmextrufalse'>document.getElementById('extruderLeftmain').style.display = 'none';</script>");

        string msg = ValidationMessage();
        if (msg.Length > 52)
        {
            MsgBuilder theBuilder1 = new MsgBuilder();
            theBuilder1.DataElements["MessageText"] = msg;
            IQCareMsgBox.Show("#C1", theBuilder1, this);
            return;
        }
        if (InsertUpdateIdentifiers() == true)
        {
            SaveCancel();
        }
    }
    private void SaveCancel()
    {

        Session["status"] = "Add";
        int patientID = Convert.ToInt32(Session["PatientId"]);
        String theUrl;
        Session["TechnicalAreaId"] = ddlTecharea.SelectedValue.ToString();

        if (Session["TechnicalAreaName"] != null && Session["TechnicalAreaName"].ToString() == "Records")
            theUrl = String.Format("./frmFindAddCustom.aspx?srvNm={0}&mod={1}", "Records", 0);
        else
            theUrl = "./ClinicalForms/frmPatient_Home.aspx";

        string strSrvName = ddlTecharea.SelectedItem.Text;
        string moduleid = ddlTecharea.SelectedItem.Value;

        ClientScript.RegisterStartupScript(this.GetType(), "Changing", "<script>fnChange();</script>");
        Label lblNoticeInfo = IQCareMsgBox.FindControlFromMaster<Label>("lblNoticeInfo", this.Master);
        Label lblNotice = IQCareMsgBox.FindControlFromMaster<Label>("lblNotice", this.Master);
        Image imgNotice = IQCareMsgBox.FindControlFromMaster<Image>("imgNotice", this.Master);
        Button btnOkAction = IQCareMsgBox.FindControlFromMaster<Button>("btnOkAction", this.Master);
        Button btnCancel = IQCareMsgBox.FindControlFromMaster<Button>("btnCancel", this.Master);
        lblNoticeInfo.Text = "Service Registration Form saved successfully. Do you want to close?";
        lblNotice.Text = "Service Registration Form";
        lblNoticeInfo.ForeColor = (false) ? System.Drawing.Color.Black : System.Drawing.Color.Black;
        lblNoticeInfo.Font.Bold = true;
        imgNotice.ImageUrl = (false) ? "~/images/mb_hand.gif" : "~/images/mb_information.gif";
        btnOkAction.OnClientClick = "";
        btnOkAction.OnClientClick = "window.location.href='" + theUrl + "';";
        //btnCancel.OnClientClick = "";
        //btnCancel.OnClientClick = "window.location.href='" + theUrl + "';";
        AjaxControlToolkit.ModalPopupExtender mod = IQCareMsgBox.FindControlFromMaster<AjaxControlToolkit.ModalPopupExtender>("notifyPopupExtender", this.Master);
        mod.Show();
        if (Session["TechnicalAreaName"] != null && Session["TechnicalAreaName"].ToString() == "Records")
        {
            string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
            script += "window.open('./ClinicalForms/frmPatientWaitingList.aspx?srvNm=" + strSrvName + "&mod=" + moduleid + "&PID=" + patientID + "', 'popupwindow', 'toolbars=no,location=no,directories=no,dependent=yes,top=150,left=150,maximize=yes,resizable=no,width=800,height=500,scrollbars=yes');\n";
            script += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "confirm", script);
        }
    }

    protected void btnContinue_Click(object sender, EventArgs e)
    {
        if (ddlTecharea.SelectedIndex > 0)
        {
            Session["TechnicalAreaId"] = ddlTecharea.SelectedValue.ToString();
        }
        else
        {
            IQCareMsgBox.Show("SelectTechnicalArea", this);
            return;
        }

        if (txtenrollmentDate.Value == "")
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "Enrollment Date";
            IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
            txtenrollmentDate.Focus();
            return;
        }
        int textcontinueblankstatus = GetBlankTextboxesstatus(0);
        if (textcontinueblankstatus.ToString() == "0")
        {
            IQCareMsgBox.Show("IdentifierRequired", this);
            return;
        }
        if (ViewState["Enrolldate"].ToString() == "")
        {
            IQCareMsgBox.Show("TechAreaNotRegistered", this);
            return;
        }
        //VY added in case of records this would go back to find add patients
        if (Session["TechnicalAreaName"].ToString() == "Records" && Convert.ToInt32(Session["TechnicalAreaId"]) == 0)
        {

            int patientID = Convert.ToInt32(Session["PatientId"]);
            string strSrvName = ddlTecharea.SelectedItem.Text;
            string moduleid = ddlTecharea.SelectedItem.Value;
            String theUrl = String.Format("./frmFindAddCustom.aspx?srvNm={0}&mod={1}", "Records", 0);

            String theOrdScript;
            theOrdScript = "<script language='javascript' id='PrintReciept'>\n";
            theOrdScript += "window.location.href = '" + theUrl + "';\n";
            theOrdScript += "window.open('./ClinicalForms/frmPatientWaitingList.aspx?srvNm=" + strSrvName + "&mod=" + moduleid + "&PID=" + patientID + "', 'popupwindow', 'toolbars=no,location=no,directories=no,dependent=yes,top=150,left=150,maximize=yes,resizable=no,width=800,height=500,scrollbars=yes');\n";
            theOrdScript += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "waitingList", theOrdScript);


        }
        else
        {
            Response.Redirect("./ClinicalForms/frmPatient_Home.aspx");
        }


    }
    protected void ddlTecharea_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtenrollmentDate.Value = "";
        flag = 1;
        LoadModuleIdentifiers(Convert.ToInt32(ddlTecharea.SelectedValue.ToString()));
    }

    #region Added Naveen-28-Oct-2010
    private void LoadFieldTypeControl(string ControlID, string FieldName)
    {
        string fieldlabel = "";
        DataView theLabelDV = new DataView((DataTable)ViewState["ModuleIdentifiers"]);
        theLabelDV.RowFilter = "FieldName='" + FieldName + "'";
        fieldlabel = theLabelDV[0]["FieldLabel"].ToString();

        if (ControlID == "1") ///SingleLine Text Box
        {
            pnlIdentFields.Controls.Add(new LiteralControl("<div class='row'>"));
            //pnlIdentFields.Controls.Add(new LiteralControl("<tr>"));
            pnlIdentFields.Controls.Add(new LiteralControl("<div class='col-md-5 col-sm-12 col-xs-12 form-group text-right'>"));
            if (theLabelDV[0]["RequiredFlag"].ToString() == "1")
                pnlIdentFields.Controls.Add(new LiteralControl("<label class='control-label required'>" + fieldlabel + " :</label>"));
            else
                pnlIdentFields.Controls.Add(new LiteralControl("<label class='control-label'>" + fieldlabel + " :</label>"));

            pnlIdentFields.Controls.Add(new LiteralControl("</div>"));
            pnlIdentFields.Controls.Add(new LiteralControl("<div class='col-md-5 col-sm-12 col-xs-12 form-group'>"));
            TextBox theSingleText = new TextBox();
            theSingleText.ID = "txt" + FieldName;
            theSingleText.Width = 180;
            theSingleText.MaxLength = 50;
            theSingleText.CssClass = "form-control";
            //By Akhil
            if (!pnlIdentFields.Controls.Contains(theSingleText))
                pnlIdentFields.Controls.Add(theSingleText);
            BindTextboxes(FieldName);
            theSingleText.Attributes.Add("onKeyup", "chkAlphaNumericString('" + theSingleText.ClientID + "')");
            pnlIdentFields.Controls.Add(new LiteralControl("</div>"));
            pnlIdentFields.Controls.Add(new LiteralControl("</div>"));
            //pnlIdentFields.Controls.Add(new LiteralControl("</table>"));

        }
        else if (ControlID == "2") ///DecimalTextBox
        {

            //pnlIdentFields.Controls.Add(new LiteralControl("<table width='100%'>"));
            pnlIdentFields.Controls.Add(new LiteralControl("<div class='row'>"));
            pnlIdentFields.Controls.Add(new LiteralControl("<div class='col-md-5 col-sm-12 col-xs-12 form-group text-right'>"));
            if (theLabelDV[0]["RequiredFlag"].ToString() == "1")
                pnlIdentFields.Controls.Add(new LiteralControl("<label class='control-label required'>" + fieldlabel + " :</label>"));
            else
                pnlIdentFields.Controls.Add(new LiteralControl("<label class='control-label'>" + fieldlabel + " :</label>"));

            pnlIdentFields.Controls.Add(new LiteralControl("</div>"));
            pnlIdentFields.Controls.Add(new LiteralControl("<div class='col-md-5 col-sm-12 col-xs-12 form-group'>"));
            TextBox theSingleDecimalText = new TextBox();
            theSingleDecimalText.ID = "txt" + FieldName;
            theSingleDecimalText.Width = 180;
            theSingleDecimalText.MaxLength = 50;
            theSingleDecimalText.CssClass = "form-control";
            //By Akhil
            if (!pnlIdentFields.Controls.Contains(theSingleDecimalText))
                pnlIdentFields.Controls.Add(theSingleDecimalText);

            BindTextboxes(FieldName);
            theSingleDecimalText.Attributes.Add("onKeyup", "chkDecimal('" + theSingleDecimalText.ClientID + "')");
            pnlIdentFields.Controls.Add(new LiteralControl("</div>"));
            pnlIdentFields.Controls.Add(new LiteralControl("</div>"));
            //pnlIdentFields.Controls.Add(new LiteralControl("</table>"));

        }
        else if (ControlID == "3")   /// Numeric (Integer)
        {
            //pnlIdentFields.Controls.Add(new LiteralControl("<table width='100%'>"));
            pnlIdentFields.Controls.Add(new LiteralControl("<div class='row'>"));
            pnlIdentFields.Controls.Add(new LiteralControl("<div class='col-md-5 col-sm-12 col-xs-12 form-group text-right'>"));

            if (theLabelDV[0]["RequiredFlag"].ToString() == "1")
                pnlIdentFields.Controls.Add(new LiteralControl("<label class='control-label required'>" + fieldlabel + " :</label>"));
            else
                pnlIdentFields.Controls.Add(new LiteralControl("<label class='control-label'>" + fieldlabel + " :</label>"));

            pnlIdentFields.Controls.Add(new LiteralControl("</div>"));
            pnlIdentFields.Controls.Add(new LiteralControl("<div class='col-md-5 col-sm-12 col-xs-12 form-group'>"));
            TextBox theNumberText = new TextBox();
            theNumberText.ID = "txt" + FieldName;
            theNumberText.Width = 180;
            theNumberText.MaxLength = 50;
            theNumberText.CssClass = "form-control";
            //By Akhil
            if (!pnlIdentFields.Controls.Contains(theNumberText))
                pnlIdentFields.Controls.Add(theNumberText);

            BindTextboxes(FieldName);
            theNumberText.Attributes.Add("onKeyup", "chkNumeric('" + theNumberText.ClientID + "')");
            pnlIdentFields.Controls.Add(new LiteralControl("</div>"));
            pnlIdentFields.Controls.Add(new LiteralControl("</div>"));
            //pnlIdentFields.Controls.Add(new LiteralControl("</table>"));
        }
        else if (ControlID == "17") ///SingleLine Text Box
        {
            ViewState["AutoPopulated"] = "True";
            //pnlIdentFields.Controls.Add(new LiteralControl("<table  width='100%'>"));
            pnlIdentFields.Controls.Add(new LiteralControl("<div class='row'>"));
            pnlIdentFields.Controls.Add(new LiteralControl("<div class='col-md-5 col-sm-12 col-xs-12 form-group text-right'>"));

            if (theLabelDV[0]["RequiredFlag"].ToString() == "1")
                pnlIdentFields.Controls.Add(new LiteralControl("<label class='control-label required'>" + fieldlabel + " :</label>"));
            else
                pnlIdentFields.Controls.Add(new LiteralControl("<label class='control-label'>" + fieldlabel + " :</label>"));

            pnlIdentFields.Controls.Add(new LiteralControl("</div>"));
            pnlIdentFields.Controls.Add(new LiteralControl("<div class='col-md-5 col-sm-12 col-xs-12 form-group'>"));
            TextBox theSingleText = new TextBox();
            theSingleText.ID = "txt" + FieldName;
            theSingleText.ReadOnly = true;
            theSingleText.BackColor = System.Drawing.Color.LightGray;
            theSingleText.Width = 180;
            theSingleText.MaxLength = 50;
            theSingleText.CssClass = "form-control";
            //By Akhil
            if (!pnlIdentFields.Controls.Contains(theSingleText))
                pnlIdentFields.Controls.Add(theSingleText);

            BindTextboxes(FieldName);
            theSingleText.Attributes.Add("onKeyup", "chkAlphaNumericString('" + theSingleText.ClientID + "')");
            pnlIdentFields.Controls.Add(new LiteralControl("</div>"));
            pnlIdentFields.Controls.Add(new LiteralControl("</div>"));
            //pnlIdentFields.Controls.Add(new LiteralControl("</table>"));

        }
    }

    private void BindTextboxes(string fieldname)
    {
        DataTable DTPatientIdents = (DataTable)ViewState["PatientIdentdata"];
        foreach (Control x in pnlIdentFields.Controls)
        {
            if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
            {

                if ("txt" + fieldname.ToString() == ((TextBox)x).ID)
                {
                    if (DTPatientIdents.Rows.Count > 0)
                    {
                        //((TextBox)x).Text = "abc";
                        ((TextBox)x).Text = Convert.ToString(DTPatientIdents.Rows[0][fieldname]);
                        if (Convert.ToInt32(Session["IdentifierFlag"]) == 0 && ((TextBox)x).Text != "")
                        {
                            ((TextBox)x).Enabled = false;
                        }
                    }
                }
            }
        }
    }

    private int GetBlankTextboxesstatus(int flag)
    {
        //flag=0 for continue & flag=1 for save and continue 
        int Blankstatus = 0;
        DataRow DTPatientIndefiersRow = null;
        DataTable DTPatientIndefiers = (DataTable)ViewState["PatientIdentdata"];

        if (DTPatientIndefiers.Rows.Count > 0)
        {
            DTPatientIndefiersRow = DTPatientIndefiers.Rows[0];
        }
        DataTable DTModuleIdents = (DataTable)ViewState["ModuleIdentifiers"];

        if (ViewState["AutoPopulated"].ToString() == "True")
        {
            Blankstatus++;
        }
        for (int j = 0; j <= DTModuleIdents.Rows.Count - 1; j++)
        {
            foreach (Control x in pnlIdentFields.Controls)
            {
                if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                {

                    if (flag == 0)
                    {
                        if (DTPatientIndefiersRow != null)
                        {
                            if (((TextBox)x).Text != "" && DTPatientIndefiersRow[DTModuleIdents.Rows[j]["FieldName"].ToString()] != System.DBNull.Value)
                            {
                                Blankstatus++;
                            }
                        }
                    }
                    else
                    {
                        if (((TextBox)x).Text != "")
                        {
                            Blankstatus++;
                        }
                    }
                }
            }
        }
        return Blankstatus;
    }

    private void LoadModuleIdentifiers(int ModuleID)
    {
        ViewState["Enrolldate"] = "";
        pnlIdentFields.Controls.Clear();
        IPatientRegistration PatRegMgr = (IPatientRegistration)ObjectFactory.CreateInstance(ObjFactoryParameter);
        DataSet theDS = PatRegMgr.GetFieldNames(ModuleID, Convert.ToInt32(Session["PatientId"]));

        ViewState["PatientIdentdata"] = theDS.Tables[1];
        ViewState["ModuleIdentifiers"] = theDS.Tables[0];

        int AutoFieldValue = 1;

        if (Convert.ToInt32(Session["PatientId"]) != 0)
        {
            if (theDS.Tables.Count > 4)
            {
                if (theDS.Tables[4].Rows[0]["AutoField"].ToString() == "")
                {
                    if (String.IsNullOrEmpty(theDS.Tables[4].Rows[0]["AutoField"].ToString()))
                        AutoFieldValue = 0;
                }
            }
            else
            {
                AutoFieldValue = 0;
            }
        }
        if (Convert.ToInt32(Session["PatientId"]) == 0)
        {
            AutoFieldValue = 0;
        }

        lblEnrollment.InnerText = "Enrollment Date:";
        btnSaveContinue.Enabled = true;

        if (theDS.Tables[2].Rows.Count > 0 && theDS.Tables[2].Rows[0]["StartDate"].ToString() != "")
        {
            if (Convert.ToInt32(theDS.Tables[2].Rows[0]["ReEnrollCount"]) > 0)
                lblEnrollment.InnerText = "Re-Enrollment Date:";
            else
                lblEnrollment.InnerText = "Enrollment Date:";

            if (flag == 1)
            {
                txtenrollmentDate.Value = ((DateTime)theDS.Tables[2].Rows[0]["StartDate"]).ToString(Session["AppDateFormat"].ToString());
                if (Session["TechnicalAreaName"] != null && Session["TechnicalAreaName"].ToString() == "Records")
                    btnSaveContinue.Enabled = false;
                else
                    btnSaveContinue.Enabled = true;

            }

            if (theDS.Tables[2].Rows[0]["Enrolchk"].ToString() == "1")
                ViewState["Enrolldate"] = ((DateTime)theDS.Tables[2].Rows[0]["StartDate"]).ToString(Session["AppDateFormat"].ToString());
            else
                ViewState["Enrolldate"] = "";

            if (Convert.ToInt32(Session["IdentifierFlag"]) == 0 && txtenrollmentDate.Value != "")
                txtenrollmentDate.Disabled = true;

        }
        ////ReEnrollment////
        if (Convert.ToInt32(Session["PatientId"]) == 0)
        {
            GetValuefromHT = (Hashtable)Session["htPtnRegParameter"];
            txtenrollmentDate.Value = GetValuefromHT["RegistrationDate"].ToString();
        }
        else
        {
            if (theDS.Tables[3].Rows.Count > 0)
            {
                btnReEnollPatient.Visible = true;
                //appDateimg2.Visible = false;
                txtenrollmentDate.Disabled = true;
                ViewState["CareEndedDate"] = theDS.Tables[3].Rows[0]["CareEndedDate"];
            }
        }
        /////
        try
        {
            //For Loading Controls in the form

            DataTable DT = theDS.Tables[0];
            int Numtds = 2, td = 1;
            int countrow = theDS.Tables[0].Rows.Count;
            pnlIdentFields.Controls.Add(new LiteralControl("<table cellspacing='6' cellpadding='0' width='100%' border='0'>"));
            foreach (DataRow DRLnkTable in theDS.Tables[0].Rows)
            {
                if (td <= Numtds)
                {
                    if (td == 1)
                    {

                        pnlIdentFields.Controls.Add(new LiteralControl("<tr style='" + HideUnhideAutoPopulated(countrow, AutoFieldValue, DRLnkTable["AutoNumber"].ToString()) + "'>"));
                    }

                    pnlIdentFields.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' style='width: 50%;" + HideUnhideAutoPopulated(countrow, AutoFieldValue, DRLnkTable["AutoNumber"].ToString()) + "'>"));
                    LoadFieldTypeControl(DRLnkTable["FieldType"].ToString(), DRLnkTable["FieldName"].ToString());
                    if (td == 2)
                    {
                        pnlIdentFields.Controls.Add(new LiteralControl("</td>"));
                        pnlIdentFields.Controls.Add(new LiteralControl("</tr>"));
                        td = 1;
                    }
                    else
                    {
                        pnlIdentFields.Controls.Add(new LiteralControl("</td>"));
                        td++;
                    }

                }
            }
            if (td == 2)
            {
                pnlIdentFields.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' style='width: 50%'>"));
                pnlIdentFields.Controls.Add(new LiteralControl("</td>"));
                pnlIdentFields.Controls.Add(new LiteralControl("</tr>"));
            }
            td = 1;
            pnlIdentFields.Controls.Add(new LiteralControl("</table>"));
            pnlIdentFields.Controls.Add(new LiteralControl("</br>"));

        }
        catch (Exception err)
        {

            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
        }

    }
    //private string HideUnhideAutoPopulatedrow(int count,int finddata, string autofield)
    //{
    //    string strhide = string.Empty;
    //    if (autofield != "")
    //    {
    //        //int mod = count % 2;
    //        if (finddata==0)
    //        {
    //            strhide = "display:none";
    //        }
    //    }
    //    return strhide;
    //}
    private string HideUnhideAutoPopulated(int count, int finddata, string autofield)
    {
        string strhide = string.Empty;
        if (autofield != "0")
        {
            if (finddata == 0)
            {
                //strhide = "visibility:hidden";
            }
        }
        return strhide;
    }
    // Generating full DML Statement 
    private bool InsertUpdateIdentifiers()
    {
        ICustomFields CustomFields;
        try
        {
            bool theReEnroll = false;
            string sqlstr = string.Empty;
            IQCareUtils theUtils = new IQCareUtils();
            DateTime visitdate = Convert.ToDateTime(theUtils.MakeDate(txtenrollmentDate.Value));
            DateTime ReEnrollDate = Convert.ToDateTime(theUtils.MakeDate(txtReEnrollmentDate.Value));
            string sqlselect;
            string sqlstrset = "";
            string[] strarr = new string[3];

            StringBuilder Insertcbl;
            StringBuilder Insertcb2;
            IPatientRegistration ptnMgr = (IPatientRegistration)ObjectFactory.CreateInstance(ObjFactoryParameter);
            DataSet DSPatDetails = ptnMgr.GetPatientTechnicalAreaDetails(Convert.ToInt32(Session["PatientId"]), ddlTecharea.SelectedItem.ToString(), Convert.ToInt32(ddlTecharea.SelectedValue));
            int blankstatus = GetBlankTextboxesstatus(1);

            if (blankstatus > 0)
            {
                sqlselect = "UPDATE mst_Patient WITH(ROWLOCK) SET mst_Patient.Status=0, ";
                sqlselect = sqlselect + "PatientType = " + Convert.ToInt32(ddlPatientType.SelectedValue) + ",";

                DataTable DTModuleIdents = (DataTable)ViewState["ModuleIdentifiers"];

                for (int j = 0; j <= DTModuleIdents.Rows.Count - 1; j++)
                {
                    foreach (Control x in pnlIdentFields.Controls)
                    {
                        if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                        {
                            if ("txt" + DTModuleIdents.Rows[j]["FieldName"].ToString() == ((TextBox)x).ID)
                            {
                                DataTable dtduplicates = ptnMgr.CheckDuplicateIdentifiervaule(DTModuleIdents.Rows[j]["FieldName"].ToString(), ((TextBox)x).Text);
                                if (dtduplicates.Rows.Count == 0 || (dtduplicates.Rows.Count == 1 && dtduplicates.Rows[0]["Ptn_pk"].ToString() == Session["PatientId"].ToString()))
                                {
                                    if (DTModuleIdents.Rows[j]["AutoNumber"].ToString() == "")
                                    {
                                        if (((TextBox)x).Text == "")
                                        {
                                            sqlstrset += "mst_patient." + DTModuleIdents.Rows[j]["FieldName"] + "=NULL,";
                                        }
                                        else
                                        {
                                            sqlstrset += "mst_patient." + DTModuleIdents.Rows[j]["FieldName"] + "='" + ((TextBox)x).Text + "',";
                                        }
                                    }
                                    else
                                    {
                                        if (((TextBox)x).Text == "")
                                        {
                                            sqlstrset += "mst_patient." + DTModuleIdents.Rows[j]["FieldName"] + "='" + GetMaxAutoPopulate(Convert.ToInt32(DTModuleIdents.Rows[j]["AutoNumber"].ToString()), DTModuleIdents.Rows[j]["FieldName"].ToString(), Convert.ToInt32(DTModuleIdents.Rows[j]["Fieldtype"].ToString())) + "',";
                                        }
                                        else
                                        {
                                            sqlstrset += "mst_patient." + DTModuleIdents.Rows[j]["FieldName"] + "='" + ((TextBox)x).Text + "',";
                                        }
                                    }

                                }
                                else if (dtduplicates.Rows.Count > 1 || (dtduplicates.Rows[0]["Ptn_pk"].ToString() != Session["PatientId"].ToString() && dtduplicates.Rows[0][DTModuleIdents.Rows[j]["FieldName"].ToString()].ToString() == ((TextBox)x).Text))
                                {
                                    MsgBuilder theBuilder = new MsgBuilder();
                                    theBuilder.DataElements["Control"] = DTModuleIdents.Rows[j]["FieldName"].ToString();
                                    IQCareMsgBox.Show("DuplicateIndentifier", theBuilder, this);
                                    return false;

                                }

                                else
                                {
                                    MsgBuilder theBuilder = new MsgBuilder();
                                    theBuilder.DataElements["Control"] = DTModuleIdents.Rows[j]["FieldName"].ToString();
                                    IQCareMsgBox.Show("DuplicateIndentifier", theBuilder, this);
                                    return false;

                                }
                            }
                        }
                    }
                }

                sqlstrset = sqlstrset.Substring(0, sqlstrset.Length - 1);
                sqlstrset += " where Ptn_pk= " + Session["PatientId"].ToString();
                sqlstr = sqlselect + sqlstrset;
                strarr[0] = sqlstr.ToString();

                Insertcbl = new StringBuilder();
                if (DSPatDetails.Tables[0].Rows.Count > 0)
                {
                    Insertcbl.Append("UPDATE ord_visit WITH(ROWLOCK) SET  VisitDate='" + String.Format("{0:dd-MMM-yyyy}", visitdate) + "',UpdateDate=getdate()");
                    Insertcbl.Append(" where Ptn_pk= " + Session["PatientId"].ToString() + " and Visit_Id=" + DSPatDetails.Tables[0].Rows[0]["Visit_ID"].ToString());
                }
                else
                {
                    Insertcbl.Append("Insert into ord_visit(Ptn_Pk,LocationID,VisitDate,VisitType,DataQuality,DeleteFlag,UserID,CreateDate)");
                    Insertcbl.Append("values (" + Convert.ToInt32(Session["PatientId"]) + ", " + Session["AppLocationId"].ToString() + ",'" + String.Format("{0:dd-MMM-yyyy}", visitdate) + "'," + iif(String.IsNullOrEmpty(DSPatDetails.Tables[1].Rows[0]["VisitTypeID"].ToString()), 0, DSPatDetails.Tables[1].Rows[0]["VisitTypeID"].ToString()) + ",0,0,");
                    Insertcbl.Append("" + Session["AppUserId"].ToString() + ", Getdate())");
                }
                strarr[1] = Insertcbl.ToString();

                Insertcb2 = new StringBuilder();
                if (DSPatDetails.Tables[2].Rows.Count > 0)
                {
                    if (btnReEnollPatient.Visible == false)
                    {
                        Insertcb2.Append("Update lnk_patientprogramstart WITH(ROWLOCK) SET");
                        Insertcb2.Append(" UpdateDate=Getdate(),StartDate='" + String.Format("{0:dd-MMM-yyyy}", visitdate) + "' where Ptn_Pk=" + Convert.ToInt32(Session["PatientId"]) + " and ModuleId=" + Convert.ToInt32(ddlTecharea.SelectedValue));
                    }
                    else
                    {
                        Insertcb2.Append("Update lnk_patientprogramstart WITH(ROWLOCK) SET");
                        Insertcb2.Append(" UpdateDate=Getdate(),StartDate='" + String.Format("{0:dd-MMM-yyyy}", ReEnrollDate) + "' where Ptn_Pk=" + Convert.ToInt32(Session["PatientId"]) + " and ModuleId=" + Convert.ToInt32(ddlTecharea.SelectedValue));
                        Insertcb2.Append("; Insert into Lnk_PatientReEnrollment(Ptn_Pk,LocationId,ModuleId,ReEnrollDate,OldEnrollDate,UserId,CreateDate)");
                        Insertcb2.Append("Values(" + Convert.ToInt32(Session["PatientId"]) + ", " + Session["AppLocationId"].ToString() + "," + Convert.ToInt32(ddlTecharea.SelectedValue) + ",'" + String.Format("{0:dd-MMM-yyyy}", ReEnrollDate) + "','" + String.Format("{0:dd-MMM-yyyy}", visitdate) + "'," + Session["AppUserId"].ToString() + ", Getdate())");
                        theReEnroll = true;
                    }
                }
                else
                {
                    Insertcb2.Append("Insert into lnk_patientprogramstart(Ptn_Pk,ModuleID,StartDate,UserID,CreateDate)");
                    Insertcb2.Append("values (" + Convert.ToInt32(Session["PatientId"]) + ", " + Convert.ToInt32(ddlTecharea.SelectedValue) + ",");
                    Insertcb2.Append("'" + String.Format("{0:dd-MMM-yyyy}", visitdate) + "'," + Session["AppUserId"].ToString() + ", Getdate())");

                }
                strarr[2] = Insertcb2.ToString();
                CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
                int icountprog = CustomFields.SaveUpdateCustomFieldValues(strarr);
                if (theReEnroll == true)
                {
                    IPatientHome theReactivationManager = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
                    theReactivationManager.ReActivatePatient(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(ddlTecharea.SelectedValue));
                }
                theReEnroll = false;
                if (icountprog == 0)
                {
                    return false;
                }

                Session["TechnicalAreaId"] = ddlTecharea.SelectedValue.ToString();

            }
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
            return false;

        }
    }

    object iif(bool expression, object truePart, object falsePart)
    { return expression ? truePart : falsePart; }

    private string GetMaxAutoPopulate(int startingnumber, string columnname, int fieldtype)
    {
        string strmaxvalue = "";
        if (fieldtype == 17)
        {
            IPatientRegistration ptnMgr = (IPatientRegistration)ObjectFactory.CreateInstance(ObjFactoryParameter);
            DataSet DSPatDetails = ptnMgr.GetMaxAutoPopulateIdentifier(columnname);
            if (DSPatDetails.Tables[0].Rows[0][0].ToString() == "")
            {
                strmaxvalue = startingnumber.ToString();
            }
            else
            {
                int autoincrement = Convert.ToInt32(DSPatDetails.Tables[0].Rows[0][0].ToString()) + 1;
                strmaxvalue = Convert.ToString(autoincrement);

            }
        }
        return strmaxvalue;
    }
    private void SavePatientRegistration()
    {
        IPatientRegistration PatientManager = (IPatientRegistration)ObjectFactory.CreateInstance(ObjFactoryParameter);
        Hashtable GetValuefromHT = new Hashtable();
        try
        {
            if (Convert.ToInt32(Session["PatientId"]) == 0)
            {
                if (Convert.ToString(ViewState["ptnid"]) == "")
                {
                    GetValuefromHT = (Hashtable)Session["htPtnRegParameter"];
                    StringBuilder A = (StringBuilder)Session["CustomRegistration"];
                    DataSet Patientds = PatientManager.Common_GetSaveUpdateforCustomRegistrion(A.ToString());
                    Session["PatientId"] = Patientds.Tables[0].Rows[0]["ptn_pk"].ToString();
                    ViewState["ptnid"] = Patientds.Tables[0].Rows[0]["ptn_pk"].ToString();
                    ViewState["visitPk"] = Patientds.Tables[0].Rows[0]["Visit_Id"].ToString();
                    ViewState["IQNumber"] = Patientds.Tables[0].Rows[0]["IQNumber"].ToString();
                    //DataTable Patientds = PatientManager.SaveNewRegistration(GetValuefromHT, 0);
                    //Session["PatientId"] = Patientds.Rows[0]["ptn_pk"].ToString();
                    //ViewState["ptnid"] = Patientds.Rows[0]["ptn_pk"].ToString();
                    //ViewState["visitPk"] = Patientds.Rows[0]["Visit_Id"].ToString();
                    //ViewState["IQNumber"] = Patientds.Rows[0]["IQNumber"].ToString();
                    ////SaveCancel();
                    DataSet theDS = PatientManager.GetFieldNames(Convert.ToInt32(ddlTecharea.SelectedValue), Convert.ToInt32(Session["PatientId"]));
                    if (theDS.Tables[1].Rows.Count > 0)
                    {
                        ViewState["PatientIdentdata"] = theDS.Tables[1];
                    }
                    ViewState["ModuleIdentifiers"] = theDS.Tables[0];
                    if (theDS.Tables[2].Rows.Count > 0 && txtenrollmentDate.Value == "")
                    {
                        txtenrollmentDate.Value = ((DateTime)theDS.Tables[2].Rows[0]["StartDate"]).ToString(Session["AppDateFormat"].ToString());
                        ViewState["Enrolldate"] = ((DateTime)theDS.Tables[2].Rows[0]["StartDate"]).ToString(Session["AppDateFormat"].ToString());
                        if (Convert.ToInt32(Session["IdentifierFlag"]) == 0 && txtenrollmentDate.Value != "")
                        {
                            //appDateimg2.Visible = false;
                            txtenrollmentDate.Disabled = true;
                        }
                    }
                }

            }
        }
        catch (Exception err)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
            return;
        }
        finally
        {
            PatientManager = null;
        }
    }
    private String ValidationMessage()
    {
        string strmsg = "Following values are required to complete this:</br>";
        DataTable DTModuleIdents = (DataTable)ViewState["ModuleIdentifiers"];

        for (int j = 0; j <= DTModuleIdents.Rows.Count - 1; j++)
        {
            foreach (Control x in pnlIdentFields.Controls)
            {
                if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                {
                    if (("txt" + DTModuleIdents.Rows[j]["FieldName"].ToString() == ((TextBox)x).ID) && (DTModuleIdents.Rows[j]["RequiredFlag"].ToString() == "1"))
                    {
                        if (((TextBox)x).Text == "")
                        {
                            strmsg += DTModuleIdents.Rows[j]["FieldLabel"] + " is Required";
                            strmsg = strmsg + "</br>";
                        }
                    }
                }
            }
        }

        return strmsg;
    }
    protected void btnReEnollPatient_Click(object sender, EventArgs e)
    {
        trReEnroll.Visible = true;
        txtReEnrollmentDate.Focus();
    }
}

    #endregion

