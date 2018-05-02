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
using Interface.Laboratory;


public partial class ClinicalForms_frmExposedInfantEnrollment : LogPage //BasePage
{
    AuthenticationManager Authentiaction = new AuthenticationManager();
    string ObjFactoryParameter = "BusinessProcess.Clinical.BInfantEnrollment, BusinessProcess.Clinical";
    DataTable dtTemp, dtInfo;
    DataSet theDS;
    int icount;

    private Boolean FieldValidation()
    {
        IIQCareSystem IQCareSecurity = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
        DateTime theCurrentDate = (DateTime)IQCareSecurity.SystemDate();
        IQCareUtils theUtils = new IQCareUtils();

        if (DDFinalResult.SelectedItem.Value == "9")
        {
            spDeath.Visible = true;
        }

        if (TxtFirstName.Text.Trim() == "")
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "First Name";
            IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
            TxtFirstName.Focus();
            return false;
        }
        if (TxtLastName.Text.Trim() == "")
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "Last Name";
            IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
            TxtLastName.Focus();
            return false;
        }

        if (TxtInfantId.Text.Trim() == "")
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "Infant ID";
            IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
            TxtInfantId.Focus();
            return false;
        }

        try
        {
            Int64 i = Convert.ToInt64(TxtInfantId.Text);
        }
        catch
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "Infant ID";
            IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
            TxtDateOfBirth.Focus();
            return false;
        }

        if (TxtDateOfBirth.Value.Trim() == "")
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "DOB";
            IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
            TxtDateOfBirth.Focus();
            return false;
        }
        DateTime theDOBDate = Convert.ToDateTime(theUtils.MakeDate(TxtDateOfBirth.Value));
        if (theDOBDate > theCurrentDate)
        {
            IQCareMsgBox.Show("DOBDate", this);
            TxtDateOfBirth.Focus();
            return false;
        }

         int Age = GetAge(theDOBDate);
         if (TxtDateOfBirth.Value != "")
         {
             Decimal AgeinYear = Math.Round(Convert.ToDecimal(Session["PatientAge"]), 0);
             if (Convert.ToDecimal(Age) >= AgeinYear)
             {
                 //ScriptManager.RegisterStartupScript(this, GetType(), "PatientAge", "alert('Infant Age cannot be more than Parent Age');", true);
                 MsgBuilder theMsg = new MsgBuilder();
                 theMsg.DataElements["Control"] = "Infant Age cannot be more than Parent Age";
                 IQCareMsgBox.Show("PatientAge", theMsg, this);
                 TxtDateOfBirth.Focus();
                 return false;
             }
         }

        if (TxtDeathDate.Value != "")
        {
            spDeath.Visible = true;

            DateTime theDeathDate = Convert.ToDateTime(theUtils.MakeDate(TxtDeathDate.Value));
            if (theDeathDate > theCurrentDate)
            {
                IQCareMsgBox.Show("DeathDateCheck", this);
                TxtDeathDate.Focus();
                return false;
            }
            if (theDeathDate < theDOBDate)
            {
                IQCareMsgBox.Show("DeathDOBDate", this);
                TxtDeathDate.Focus();
                return false;
            }

        }
        


        //if (DDInfantFeedingPractice.SelectedItem.Value.Trim() == "0")
        //{
        //    IQCareMsgBox.Show("InfantFeed", this);
        //    DDInfantFeedingPractice.Focus();
        //    return false;
        //}
        //if (DDHIVTestType.SelectedItem.Value.Trim() == "0")
        //{
        //    IQCareMsgBox.Show("InfantHivTestType", this);
        //    DDHIVTestType.Focus();
        //    return false;
        //}
        //if (DDResult.SelectedItem.Value.Trim() == "0")
        //{
        //    IQCareMsgBox.Show("InfantResult", this);
        //    DDResult.Focus();
        //    return false;
        //}

        //if (DDFinalResult.SelectedItem.Value.Trim() == "0")
        //{
        //    IQCareMsgBox.Show("InfantFinalResult", this);
        //    DDFinalResult.Focus();
        //    return false;
        //}
        return true;
    }

    public Int32 GetAge(DateTime dateOfBirth)
    {
        var today = DateTime.Today;
        var a = (today.Year * 100 + today.Month) * 100 + today.Day;
        var b = (dateOfBirth.Year * 100 + dateOfBirth.Month) * 100 + dateOfBirth.Day;
        return (a - b) / 10000;
    }

    protected void AddAttributes()
    {
        TxtDateOfBirth.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3');");
        TxtDeathDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
        //TxtDateOfBirth.Attributes.Add("onkeyup", "DateFormat(this, this.value, event, false, '3')");
    }
    public void BindGrid(DataTable dt)
    {
        try
        {
            if (dt.Rows.Count > 0)
            {
                grdChildInfo.DataSource = null;
                grdChildInfo.DataSource = dt;
                grdChildInfo.DataBind();
            }
        }
        catch (Exception err)
        {

            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
        {
            IQCareMsgBox.Show("SessionExpired", this);
            Response.Redirect("~/frmlogin.aspx",true);
        }
        maintainviewState(Convert.ToString(DDFinalResult.SelectedItem));
        BusineesRuleFinalResult();

        if (!Page.IsPostBack)
        {

            AddAttributes();
            IInfantEnrollment ptnMgrPMTCT = (IInfantEnrollment)ObjectFactory.CreateInstance(ObjFactoryParameter);
            theDS = ptnMgrPMTCT.GetExposedInfantByParentId(Convert.ToInt32(Session["PatientId"].ToString()));
            dtTemp = theDS.Tables[0];
            //dtInfo = theDS.Tables[1];
            string strPatientName = Session["PatientName"].ToString();
            string[] strname = strPatientName.Split(',');
            ViewState["FName"] = strname[1].ToString();
            ViewState["LName"] = strname[0].ToString();
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text = Session["lblpntstatus"].ToString();
            if (Authentiaction.HasFunctionRight(ApplicationAccess.ChildEnrollment, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
            {
                string theUrl = string.Empty;
                theUrl = string.Format("../ClinicalForms/frmPatient_Home.aspx");
                Response.Redirect(theUrl);
            }
            else if (Authentiaction.HasFunctionRight(ApplicationAccess.ChildEnrollment, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
            {
                btnsave.Enabled = false;
                btnAdd.Enabled = false;
            }
            else if (Authentiaction.HasFunctionRight(ApplicationAccess.ChildEnrollment, FunctionAccess.Update, (DataTable)Session["UserRight"]) == false)
            {
                btnsave.Enabled = false;
                btnAdd.Enabled = false;
            }

            // Bind Grid

            //GetAdmissionNo();


            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text = Session["lblpntstatus"].ToString();
            //(Master.FindControl("lblRoot") as Label).Text = "";
            //(Master.FindControl("lblMark") as Label).Visible = false;
            //(Master.FindControl("lblheader") as Label).Text = "Exposed Infant Follow up";
            //(Master.FindControl("lblformname") as Label).Text = "Exposed Infant Follow up";
            //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Visible = false;
            //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Exposed Infant Follow up";
            //(Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Exposed Infant Follow up";
            //lblname.Text = strPatientName;
            //DataTable dtPatientInfo = (DataTable)Session["PatientInformation"];
            //if (dtPatientInfo != null)
            //{
            //    lblIQnumber.Text = dtPatientInfo.Rows[0]["IQNumber"].ToString();
            //}
            if (ViewState["DT"] == null)
            {
                ViewState["DT"] = dtTemp;
                //ViewState["iSerialNo"] = Convert.ToInt16(dtInfo.Rows[0][0]) + 1;
                ViewState["FirstName"] = "Baby of " + strname[1].ToString();
                ViewState["LastName"] = strname[0].ToString();
                //TxtFirstName.Text = ViewState["FirstName"].ToString();
                //TxtLastName.Text = ViewState["LastName"].ToString();
            }
            else
            {
                ViewState["FirstName"] = TxtFirstName.Text;
                ViewState["LastName"] = TxtLastName.Text;
                //TxtFirstName.Text = ViewState["FirstName"].ToString();
                //TxtLastName.Text = ViewState["LastName"].ToString();
            }

            Init_Form();
            BindGrid(dtTemp);
            if (dtTemp.Rows.Count == 0)
            {
                btnsave.Enabled = false;
            }
            else
            {
                btnsave.Enabled = true;
            }
        }
       

        
    }
    private void maintainviewState(string DDFinalResult)
    {

        if (DDFinalResult == "D=Dead")
        {
            ViewState["FinalResult"] = "1";
            // Page.ClientScript.RegisterStartupScript(Type.GetType("System.String"), "addScript", "spDeathCon(9)", true);
            // spDeath.Visible = true;
        }
    }
    private void BusineesRuleFinalResult()
    {
        if (ViewState["FinalResult"] == "1")
        {
            string scriptChangeRegimen = "<script language = 'javascript' defer ='defer' id = 'onChangeRegimen'>\n";
            scriptChangeRegimen += "spDeathCon(9); \n";
            scriptChangeRegimen += "</script>\n";
            RegisterStartupScript("onChangeRegimen", scriptChangeRegimen);
        }
          // Page.ClientScript.RegisterStartupScript(Type.GetType("System.String"), "addScript1", "abc()", true);
    }
    private void Init_Form()
    {
        BindFunctions BindManager = new BindFunctions();
        IQCareUtils theUtils = new IQCareUtils();
        ILabFunctions LabResultManager = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
        DataSet theDSLabs;
        theDSLabs = LabResultManager.GetLabValues();
        DataTable HivTestType = theDSLabs.Tables[2];
        DataView theDV1 = new DataView(HivTestType);
        theDV1.RowFilter = "SubTestId in (53,114)";
        HivTestType = theUtils.CreateTableFromDataView(theDV1);
        BindManager.BindCombo(DDHIVTestType, HivTestType, "SubTestName", "SubTestId");
        DataSet theDSXML = new DataSet();
        theDSXML.ReadXml(MapPath("..\\XMLFiles\\ALLMasters.con"));
        DataView theDV = new DataView();
        DataTable theDT = new DataTable();
        /*******/
        theDV = new DataView(theDSXML.Tables["Mst_ModDeCode"]);
        theDV.RowFilter = "DeleteFlag=0 and CodeID=5";
        if (theDV.Table != null)
        {
            theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            BindManager.BindCombo(DDFinalResult, theDT, "Name", "ID");
            theDV.Dispose();
            theDT.Clear();
        }
        theDV = new DataView(theDSXML.Tables["mst_PMTCTDecode"]);
        theDV.RowFilter = "DeleteFlag=0 and CodeID=5";
        if (theDV.Table != null)
        {
            theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            BindManager.BindCombo(DDResult, theDT, "Name", "ID");
            theDV.Dispose();
            theDT.Clear();
        }
        theDV = new DataView(theDSXML.Tables["mst_PMTCTDecode"]);
        theDV.RowFilter = "DeleteFlag=0 and CodeID=4";
        if (theDV.Table != null)
        {
            theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            BindManager.BindCombo(DDInfantFeedingPractice, theDT, "Name", "ID");
            theDV.Dispose();
            theDT.Clear();
        }
    }

    protected void grdChildInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int r = e.Row.Cells.Count;
            for (int i = 0; i < 10; i++)
            {
                if (Authentiaction.HasFunctionRight(ApplicationAccess.ChildEnrollment, FunctionAccess.Update, (DataTable)Session["UserRight"]) == true)
                {
                    e.Row.Cells[i].Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.BackColor='#666699';");
                    e.Row.Cells[i].Attributes.Add("onmouseout", "this.style.backgroundColor='';");
                    e.Row.Cells[i].Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(grdChildInfo, "Select$" + e.Row.RowIndex.ToString()));
                }
            }
            if (e.Row.Cells[5].Text == "1")
            {
                e.Row.Cells[5].Text = "Yes";
            }
            if (e.Row.Cells[5].Text == "0")
            {
                e.Row.Cells[5].Text = "No";
            }

            if (Authentiaction.HasFunctionRight(ApplicationAccess.ChildEnrollment, FunctionAccess.Delete, (DataTable)Session["UserRight"]) == true)
            {
                LinkButton objlink = (LinkButton)e.Row.Cells[10].Controls[0];
                objlink.OnClientClick = "if(!confirm('Are you sure you want to delete this?')) return false;";
                e.Row.Cells[10].ID = e.Row.RowIndex.ToString();

            }
        }
    }

    protected void grdChildInfo_Sorting(object sender, GridViewSortEventArgs e)
    {
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string theUrl;
        theUrl = string.Format("{0}", "frmPatient_Home.aspx");
        Response.Redirect(theUrl);
    }

    public void ClearControl()
    {
        TxtDeathDate.Value = "";
        TxtDateOfBirth.Value = "";
        DDFinalResult.SelectedIndex = 0;
        DDHIVTestType.SelectedIndex = 0;
        DDInfantFeedingPractice.SelectedIndex = 0;
        DDResult.SelectedIndex = 0;
        ChkCPTStarted.Checked = false;
        TxtInfantId.Text = "";
        TxtFirstName.Text = "";
        TxtLastName.Text = "";
        //TxtFirstName.Text = "Baby of " + ViewState["FName"].ToString();
        //TxtLastName.Text = ViewState["LName"].ToString();
    }
    protected void btnAdd_Click1(object sender, EventArgs e)
    {
        if (ViewState["DT"] != null)
        {
            dtTemp = (DataTable)ViewState["DT"];
        }
        if (btnAdd.Text == "Add Infant")
        {
            IInfantEnrollment ptnMgrPMTCT = (IInfantEnrollment)ObjectFactory.CreateInstance(ObjFactoryParameter);
            DataSet theDS = ptnMgrPMTCT.CheckIdentity(TxtInfantId.Text);
            if (theDS.Tables[0].Rows.Count > 0)
            {
                IQCareMsgBox.Show("InfantDataDBExist", this);
                TxtInfantId.Focus();
                return;
            }
            if (grdChildInfo.Rows.Count > 0)
            {
                for (int i = 0; i < grdChildInfo.Rows.Count; i++)
                {
                    if (grdChildInfo.Rows[i].Cells[2].Text == TxtInfantId.Text)
                    {
                        IQCareMsgBox.Show("InfantDataDBExist", this);
                        TxtInfantId.Focus();
                        return;
                    }
                }
            }
        }
        else
        {
            if (dtTemp.Rows.Count > 0)
            {
                int r = Convert.ToInt32(Session["SelectedRow"]);
                string InfID = dtTemp.Rows[r][2].ToString();
                if (InfID != TxtInfantId.Text)
                {
                    IInfantEnrollment ptnMgrPMTCT = (IInfantEnrollment)ObjectFactory.CreateInstance(ObjFactoryParameter);
                    DataSet theDS = ptnMgrPMTCT.CheckIdentity(TxtInfantId.Text);
                    if (theDS.Tables[0].Rows.Count > 0)
                    {
                        IQCareMsgBox.Show("InfantDataDBExist", this);
                        TxtInfantId.Focus();
                        return;
                    }
                }

                if (grdChildInfo.Rows.Count > 0)
                {
                    for (int i = 0; i < grdChildInfo.Rows.Count; i++)
                    {
                        if (grdChildInfo.Rows[i].Cells[2].Text == TxtInfantId.Text && i != r)
                        {
                            IQCareMsgBox.Show("InfantDataDBExist", this);
                            TxtInfantId.Focus();
                            return;
                        }
                    }

                }
            }
        }
        if (Authentiaction.HasFunctionRight(ApplicationAccess.ChildEnrollment, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
        {
            btnsave.Enabled = false;
            btnAdd.Enabled = false;
        }
        else
        {
            btnsave.Enabled = true;
            btnAdd.Enabled = true;
        }
       
        if (FieldValidation() == false)
        { return; }

        if (btnAdd.Text == "Add Infant")
        {
            icount = dtTemp.Rows.Count;
            DataRow row;
            row = dtTemp.NewRow();
            row["Id"] = "0";
            row["FirstName"] = ViewState["FirstName"].ToString();
            row["LastName"] = ViewState["LastName"].ToString();
            row["ExposedInfantId"] = TxtInfantId.Text;
            string strDob = TxtDateOfBirth.Value;
            DateTime dtDob = Convert.ToDateTime(strDob);
            strDob = dtDob.ToString("dd MMM yyyy");
            row["DOB"] = strDob;
            if (DDInfantFeedingPractice.SelectedItem.Text == "Select")
            {
                row["FeedingPractice3mos"] = string.Empty;
            }
            else
            {
                row["FeedingPractice3mos"] = string.IsNullOrEmpty(DDInfantFeedingPractice.SelectedValue) ?
                    string.Empty : DDInfantFeedingPractice.SelectedItem.Text;
            }
            row["CTX2mos"] = ChkCPTStarted.Checked ? "Yes" : "No";

            if (DDHIVTestType.SelectedItem.Text == "Select")
            {
                row["HIVTestType"] = string.Empty;
            }
            else
            {
                row["HIVTestType"] = string.IsNullOrEmpty(DDHIVTestType.SelectedValue) ?
                    string.Empty : DDHIVTestType.SelectedItem.Text;
            }
            if (DDResult.SelectedItem.Text == "Select")
            {
                row["HIVResult"] = string.Empty;
            }
            else
            {
                row["HIVResult"] = string.IsNullOrEmpty(DDResult.SelectedValue) ?
                    string.Empty : DDResult.SelectedItem.Text;
            }
            if (DDFinalResult.SelectedItem.Text == "Select")
            {
                row["FinalStatus"] = string.Empty;
            }
            else
            {
                row["FinalStatus"] = string.IsNullOrEmpty(DDFinalResult.SelectedValue) ?
                    string.Empty : DDFinalResult.SelectedItem.Text;
            }
            if (!TxtDeathDate.Value.Equals(string.Empty))
            {
                string strDeath = TxtDeathDate.Value;
                DateTime dtDeath = Convert.ToDateTime(strDeath);
                strDeath = dtDeath.ToString("dd MMM yyyy");
                row["DeathDate"] = strDeath;
            }
            if (TxtDeathDate.Value == "")
            {
            }
            // New
            row["HivResultId"] = string.IsNullOrEmpty(DDResult.SelectedItem.Value) ?
               string.Empty : DDResult.SelectedItem.Value;
            row["FeedID"] = string.IsNullOrEmpty(DDInfantFeedingPractice.SelectedItem.Value) ?
               string.Empty : DDInfantFeedingPractice.SelectedItem.Value;
            row["FinalStatusID"] = string.IsNullOrEmpty(DDFinalResult.SelectedItem.Value) ?
               string.Empty : DDFinalResult.SelectedItem.Value;
            row["HIVTestTypeID"] = string.IsNullOrEmpty(DDHIVTestType.SelectedItem.Value) ?
               string.Empty : DDHIVTestType.SelectedItem.Value;

            dtTemp.Rows.InsertAt(row, icount);
            ViewState["DT"] = dtTemp;
            ViewState["Count"] = dtTemp.Rows.Count + 1;
            BindGrid(dtTemp);
            ClearControl();
        }
        else if (btnAdd.Text == "Update Infant")
        {
            int r = Convert.ToInt32(Session["SelectedRow"]);
            dtTemp.Rows[r]["FirstName"] = TxtFirstName.Text;
            dtTemp.Rows[r]["LastName"] = TxtLastName.Text;
            dtTemp.Rows[r]["ExposedInfantId"] = TxtInfantId.Text;
            string strDob = TxtDateOfBirth.Value;
            DateTime dtDob = Convert.ToDateTime(strDob);
            strDob = dtDob.ToString("dd MMM yyyy");
            dtTemp.Rows[r]["DOB"] = strDob;

            if (DDInfantFeedingPractice.SelectedItem.Text == "Select")
            {
                dtTemp.Rows[r]["FeedingPractice3mos"] = string.Empty;
            }
            else
            {
                dtTemp.Rows[r]["FeedingPractice3mos"] = string.IsNullOrEmpty(DDInfantFeedingPractice.SelectedValue) ?
                    string.Empty : DDInfantFeedingPractice.SelectedItem.Text;
            }


            dtTemp.Rows[r]["CTX2mos"] = ChkCPTStarted.Checked ? "Yes" : "No";

            if (DDHIVTestType.SelectedItem.Text == "Select")
            {
                dtTemp.Rows[r]["HIVTestType"] = string.Empty;
            }
            else
            {
                dtTemp.Rows[r]["HIVTestType"] = string.IsNullOrEmpty(DDHIVTestType.SelectedValue) ?
                    string.Empty : DDHIVTestType.SelectedItem.Text;
            }

            if (DDResult.SelectedItem.Text == "Select")
            {
                dtTemp.Rows[r]["HIVResult"] = string.Empty;
            }
            else
            {
                dtTemp.Rows[r]["HIVResult"] = string.IsNullOrEmpty(DDResult.SelectedValue) ?
                    string.Empty : DDResult.SelectedItem.Text;

            }
            if (DDFinalResult.SelectedItem.Text == "Select")
            {
                dtTemp.Rows[r]["FinalStatus"] = string.Empty;
            }
            else
            {
                dtTemp.Rows[r]["FinalStatus"] = string.IsNullOrEmpty(DDFinalResult.SelectedValue) ?
                    string.Empty : DDFinalResult.SelectedItem.Text;
            }

            if (!TxtDeathDate.Value.Equals(string.Empty))
            {
                string strDeath = TxtDeathDate.Value;
                DateTime dtDeath = Convert.ToDateTime(strDeath);
                strDeath = dtDeath.ToString("dd MMM yyyy");
                dtTemp.Rows[r]["DeathDate"] = strDeath;
            }
            if (TxtDeathDate.Value == "")
            {
                dtTemp.Rows[r]["DeathDate"] = System.DBNull.Value;
            }
            dtTemp.Rows[r]["HivResultId"] = string.IsNullOrEmpty(DDResult.SelectedItem.Value) ?
            string.Empty : DDResult.SelectedItem.Value;
            dtTemp.Rows[r]["FeedID"] = string.IsNullOrEmpty(DDInfantFeedingPractice.SelectedItem.Value) ?
            string.Empty : DDInfantFeedingPractice.SelectedItem.Value;
            dtTemp.Rows[r]["FinalStatusID"] = string.IsNullOrEmpty(DDFinalResult.SelectedItem.Value) ?
            string.Empty : DDFinalResult.SelectedItem.Value;
            dtTemp.Rows[r]["HIVTestTypeID"] = string.IsNullOrEmpty(DDHIVTestType.SelectedItem.Value) ?
           string.Empty : DDHIVTestType.SelectedItem.Value;
            ViewState["DT"] = dtTemp;
            BindGrid(dtTemp);
            btnAdd.Text = "Add Infant";
            ClearControl();
        }
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        dtTemp = (DataTable)ViewState["DT"];
        for (int i = 0; i < dtTemp.Rows.Count; i++)
        {
            Hashtable theHT = AddUpdateData(i);
            IInfantEnrollment ptnMgrPMTCT = (IInfantEnrollment)ObjectFactory.CreateInstance(ObjFactoryParameter);
            //DataTable theCustomDataDT = new DataTable();
            /*DataTable*/
            DateTime ddeath = Convert.ToDateTime("01-01-1900");
            if (!theHT["deathdate"].Equals(string.Empty))
            {
                ddeath = Convert.ToDateTime(theHT["deathdate"]);
            }
            int ctx2Id = 0;
            if (theHT["ctx"].ToString() == "Yes")
            {
                ctx2Id = 1;
            }
            int theDS = ptnMgrPMTCT.SaveExposedInfant(Convert.ToInt32(theHT["id"]), Convert.ToInt32(theHT["ptn_pk"]),
                Convert.ToInt64(theHT["ExposedInfantId"]),
                theHT["fname"].ToString(), theHT["lname"].ToString(), Convert.ToDateTime(theHT["dob"]).ToString("dd-MMM-yyyy"),
                theHT["fp"].ToString(), ctx2Id.ToString(), theHT["type"].ToString(),
                theHT["result"].ToString(), theHT["final"].ToString(), ddeath.ToString("dd-MMM-yyyy"), Convert.ToInt32(Session["AppUserId"].ToString()));

            if (dtTemp.Rows[i][0].ToString() == "0")
            {
                ViewState["Status"] = "Add";
                //ViewState["visitPk"] = theDS.Rows[0]["Visit_ID"].ToString();
                //ViewState["PtnID"] = theDS.Rows[0]["PatientID"].ToString();
                //DataSet theDSInfantInfo = ptnMgrPMTCT.SaveInfantInfo(Convert.ToInt64(ViewState["PtnID"]), Convert.ToInt64(Session["AppLocationId"]), Convert.ToInt64(ViewState["visitPk"]), Convert.ToInt64(Session["PatientId"]), Convert.ToInt64(Session["AppUserId"]));
                BindGrid(dtTemp);
            }
            else
            {
                ViewState["Status"] = "Edit";
                //Hashtable theHT = AddUpdateData(i);
                //IPatientRegistration ptnMgrPMTCT = (IPatientRegistration)ObjectFactory.CreateInstance(ObjFactoryParameter);
                //DataTable theCustomDataDT = new DataTable();
                //DataTable theDS = ptnMgrPMTCT.SavePatientRegistrationPMTCT(theHT, theCustomDataDT);
                BindGrid(dtTemp);
            }
            SaveCancel();
        }
    }
    protected Hashtable AddUpdateData(int rowid)
    {
        Hashtable theHT = new Hashtable();
        theHT.Clear();
        /*if (ViewState["Status"].ToString() == "Edit")
        {
            theHT.Add("id", dtTemp.Rows[rowid][7].ToString());
        }
        else*/
        {
            theHT.Add("id", dtTemp.Rows[rowid][0].ToString());
        }
        theHT.Add("ptn_pk", Session["PatientId"].ToString());
        theHT.Add("ExposedInfantId", dtTemp.Rows[rowid][2].ToString());
        theHT.Add("fname", dtTemp.Rows[rowid][3].ToString());
        theHT.Add("lname", dtTemp.Rows[rowid][4].ToString());
        theHT.Add("dob", dtTemp.Rows[rowid][5].ToString());
        theHT.Add("fp", dtTemp.Rows[rowid][6].ToString());
        theHT.Add("ctx", dtTemp.Rows[rowid][7].ToString());
        theHT.Add("type", dtTemp.Rows[rowid][8].ToString());
        theHT.Add("result", dtTemp.Rows[rowid][9].ToString());
        theHT.Add("final", dtTemp.Rows[rowid][10].ToString());
        theHT.Add("deathdate", dtTemp.Rows[rowid][11].ToString());
        return theHT;
    }

    private void SaveCancel()
    {
        IQCareMsgBox.NotifyAction("Enrollment Form saved successfully. Do you want to close?", "Enrollment Form", false, this, "window.location.href='frmPatient_Home.aspx';");
        //string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
        //script += "var ans;\n";
        //script += "ans=window.confirm('Enrollment Form saved successfully. Do you want to close?');\n";
        //script += "if (ans==true)\n";
        //script += "{\n";
        //script += "window.location.href='frmPatient_Home.aspx';\n";
        //script += "}\n";
        //script += "else \n";
        //script += "{\n";
        //script += "window.location.href='frmExposedInfantEnrollment.aspx?name=Edit';\n";
        //script += "}\n";
        //script += "</script>\n";
        //RegisterStartupScript("confirm", script);
    }

    protected void TxtFirstName_TextChanged(object sender, EventArgs e)
    {
        ViewState["FirstName"] = TxtFirstName.Text;
    }
    protected void TxtLastName_TextChanged(object sender, EventArgs e)
    {
        ViewState["LastName"] = TxtLastName.Text;
    }
    protected void TxtInfantId_TextChanged(object sender, EventArgs e)
    {
        ViewState["InfantID"] = TxtLastName.Text;
    }
    protected void grdChildInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        // btnAdd.Text = "Add Child";
        int p = Convert.ToInt32(e.RowIndex);
        dtTemp = (DataTable)ViewState["DT"];
        //GetAdmissionNo();
        if (dtTemp.Rows[p][0].ToString() != "0")
        {
            IInfantEnrollment ptnMgrPMTCT = (IInfantEnrollment)ObjectFactory.CreateInstance(ObjFactoryParameter);
            ptnMgrPMTCT.DeleteExposedInfantById(Convert.ToInt32(dtTemp.Rows[p][0]));
        }
        dtTemp.Rows[p].Delete();
        dtTemp.AcceptChanges();
        ViewState["DT"] = dtTemp;
        BindGrid((DataTable)ViewState["DT"]);
        IQCareMsgBox.Show("DeleteSuccess", this);
        if (((DataTable)ViewState["DT"]).Rows.Count == 0)
        {
            btnsave.Enabled = false;
            grdChildInfo.DataSource = ViewState["DT"];
            grdChildInfo.DataBind();
        }
        else
        {
            btnsave.Enabled = true;
        }
    }
    //protected void DDFinalResult_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    TxtDeathDate.Value = "";
    //    if (DDFinalResult.SelectedValue.Equals("9"))
    //    {
    //        //  TxtDeathDate.Visible = true;
    //        //  imgDeath.Visible = true;
    //        // lblDeathDate.Visible = true;
    //        //  spanDeath.Visible = true;
    //       // a.Visible = true;
    //    }
    //    else
    //    {
    //       // a.Visible = false;
    //        //   TxtDeathDate.Visible = false;
    //        //    imgDeath.Visible = false;
    //        //    spanDeath.Visible = false;
    //        //   lblDeathDate.Visible = false;
    //    }
    //}
    protected void grdChildInfo_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        BindGrid((DataTable)(ViewState["DT"]));
        if (Authentiaction.HasFunctionRight(ApplicationAccess.ChildEnrollment, FunctionAccess.Update, (DataTable)Session["UserRight"]) == true)
        {
            btnsave.Enabled = true;
            btnAdd.Enabled = true;
        }
        else
        {
            btnsave.Enabled = false;
            btnAdd.Enabled = false;
        }
        int thePage = grdChildInfo.PageIndex;
        int thePageSize = grdChildInfo.PageSize;
        GridViewRow theRow = grdChildInfo.Rows[e.NewSelectedIndex];
        int theIndex = thePageSize * thePage + theRow.RowIndex;
        int rowindex = theIndex;
        TxtInfantId.Text = grdChildInfo.Rows[rowindex].Cells[2].Text.ToString();
        TxtFirstName.Text = grdChildInfo.Rows[rowindex].Cells[0].Text.ToString();
        TxtLastName.Text = grdChildInfo.Rows[rowindex].Cells[1].Text.ToString();
        string strDOB = grdChildInfo.Rows[rowindex].Cells[3].Text.ToString();
        DateTime dtDob = Convert.ToDateTime(strDOB);
        strDOB = dtDob.ToString("dd-MMM-yyyy");
        TxtDateOfBirth.Value = strDOB;
        if (grdChildInfo.Rows[rowindex].Cells[5].Text.ToString() == "Yes")
        {
            ChkCPTStarted.Checked = true;
        }
        else
        {
            ChkCPTStarted.Checked = false;
        }
        string str = grdChildInfo.Rows[rowindex].Cells[4].Text.ToString();
        ListItem li = DDInfantFeedingPractice.Items.FindByText(str);
        if (li != null)
        {
            DDInfantFeedingPractice.SelectedIndex = -1;
            li.Selected = true;
        }
        str = grdChildInfo.Rows[rowindex].Cells[6].Text.ToString();
        li = DDHIVTestType.Items.FindByText(str);
        if (li != null)
        {
            DDHIVTestType.SelectedIndex = -1;
            li.Selected = true;
        }
        str = grdChildInfo.Rows[rowindex].Cells[7].Text.ToString();
        li = DDResult.Items.FindByText(str);
        if (li != null)
        {
            DDResult.SelectedIndex = -1;
            li.Selected = true;
        }
        str = grdChildInfo.Rows[rowindex].Cells[8].Text.ToString();
        li = DDFinalResult.Items.FindByText(str);
        if (li != null)
        {
            DDFinalResult.SelectedIndex = -1;
            li.Selected = true;
        }
        if (str == "D=Dead")
        {
            string strDeath = grdChildInfo.Rows[rowindex].Cells[9].Text.ToString();
            try
            {
                DateTime dtDeath = Convert.ToDateTime(strDeath);
                strDeath = dtDeath.ToString("dd-MMM-yyyy");
                TxtDeathDate.Value = strDeath;
                Page.ClientScript.RegisterStartupScript(Type.GetType("System.String"), "addScript", "CalEnbleDisble()", true);
            }
            catch { }
        }
        Session["SelectedRow"] = rowindex;
        btnAdd.Text = "Update Infant";
    }
}
