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

using Interface.Clinical;
using Interface.Security;
using Application.Presentation;
using Application.Common;
using Application.Interface;
using Interface.Administration;
using System.Text;

public partial class ClinicalForms_frmClinical_ARVTherapy : LogPage
{
    int PatientID, LocationID, visitPK = 0;
    AuthenticationManager Authentiaction = new AuthenticationManager();
    Hashtable htARVTherapyParameters;
    // Function to Pass HashTable Parameters
    int PId;
    string strmultiselect;
    StringBuilder sbValues;
    ArrayList arl;
    int PatID;
    StringBuilder sbParameter;
    String TableName;
    int icount;
    private Hashtable ARVTherapyParameters()
    {
        htARVTherapyParameters = new Hashtable();

             

        htARVTherapyParameters.Add("DateEligible", txtdateEligible.Value);
        htARVTherapyParameters.Add("EligibleThru", ddleligibleThru.SelectedValue);
        htARVTherapyParameters.Add("WHOStage", lstClinicalStage.SelectedValue);
        htARVTherapyParameters.Add("CD4", txtCD4.Text);
        htARVTherapyParameters.Add("CD4Percent", txtCD4percent.Text);
        htARVTherapyParameters.Add("CohortMonth", txtcohortmnth.Value);
        htARVTherapyParameters.Add("CohortYear", txtcohortyear.Value);
        htARVTherapyParameters.Add("AnotherRegimenStartDate", txtanotherRegimendate.Value);
        htARVTherapyParameters.Add("AnotherRegimen", txtanotherregimen.Value);
        htARVTherapyParameters.Add("AnotherWeight", txtanotherwght.Value);
        htARVTherapyParameters.Add("AnotherHeight", txtanotherheight.Value);
        htARVTherapyParameters.Add("AnotherClinicalStage", lstanotherClinicalStage.SelectedValue);

        htARVTherapyParameters.Add("visitdate", System.DateTime.Now.ToString("dd-MMM-yyyy"));
        return htARVTherapyParameters;
   
    }

    protected void btnRegimen_Click(object sender, EventArgs e)
    {
        string theScript;
        Application.Add("MasterData", ViewState["MasterData"]);
        Application.Add("SelectedDrug", (DataTable)ViewState["SelectedData"]);
        ViewState.Remove("ARVMasterData");
        theScript = "<script language='javascript' id='DrgPopup'>\n";
        theScript += "window.open('../Pharmacy/frmDrugSelector.aspx?DrugType=37&btnreg=btnRegimen' ,'DrugSelection','toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=no,resize=no,width=700,height=350,scrollbars=yes');\n";
        theScript += "</script>\n";
        Page.RegisterStartupScript("DrgPopup", theScript);
    }
    protected void btnTransRegimen_Click(object sender, EventArgs e)
    {
        string theScript;
        Application.Add("MasterData", Session["ARVMasterData"]);
        Application.Add("SelectedDrug", (DataTable)ViewState["TransSelectedData"]);
        //Session.Remove("ARVMasterData");
        theScript = "<script language='javascript' id='DrgPopup'>\n";
        theScript += "window.open('../Pharmacy/frmDrugSelector.aspx?DrugType=37&btnreg=btnRegimen1' ,'DrugSelection','toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=no,resize=no,width=700,height=350,scrollbars=yes');\n";
        theScript += "</script>\n";
        Page.RegisterStartupScript("DrgPopup", theScript);
    }
    /******** Fill Old Regimen List **********/
    private DataTable OldRegimenList(string[] str, DataView theDV)
    {
        DataTable theDT = CreateSelectedTable();
        foreach (string reg in str)
        {
            theDV.RowFilter = "Abbr Like '" + reg + "%'";
            if (theDV.Count > 0)
            {
                DataRow theDR = theDT.NewRow();
                theDR[0] = theDV[0][0];
                theDR[1] = theDV[0][1];
                theDR[2] = theDV[0][2];
                theDR[3] = theDV[0][3];
                theDR[4] = theDV[0][4];

                DataRow theTempeDR;
                theTempeDR = theDT.Rows.Find(theDV[0][0]);
                if (theTempeDR == null)
                {
                    theDT.Rows.Add(theDR);
                }
            }
        }
        return theDT;

    }

    private DataTable CreateSelectedTable()
    {
        DataTable theDT = new DataTable();
        theDT.Columns.Add("DrugId", System.Type.GetType("System.Int32"));
        theDT.Columns.Add("DrugName", System.Type.GetType("System.String"));
        theDT.Columns.Add("Generic", System.Type.GetType("System.Int32"));
        theDT.Columns.Add("DrugTypeID", System.Type.GetType("System.Int32"));
        theDT.Columns.Add("Abbr", System.Type.GetType("System.String"));
        theDT.PrimaryKey = new DataColumn[] { theDT.Columns[0] };
        return theDT;
    }
    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
        {
            IQCareMsgBox.Show("SessionExpired", this);
            Response.Redirect("~/frmlogin.aspx",true);
        }
        //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Clinical Forms >> ";
        //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "ART Therapy";
        //(Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "ART Therapy";

        PutCustomControl();


        if (!IsPostBack)
        {
            BMIAttributes();
            Authenticate();
            BindDropdowns();
            Init_ARTCare();
            if (ddleligibleThru.SelectedItem.Text == "Clinical")
            {
                lstClinicalStage.Enabled = true;
                txtCD4.Enabled = false;
                txtCD4percent.Enabled = false;
            }
            else if (ddleligibleThru.SelectedItem.Text == "CD4 count/percent")
            {
                lstClinicalStage.Enabled = false;
                txtCD4.Enabled = true;
                txtCD4percent.Enabled = true;
            }
            else
            {
                lstClinicalStage.Enabled = false;
                txtCD4.Enabled = false;
                txtCD4percent.Enabled = false;
            }


        }
        else
        {
            if ((DataTable)Application["AddRegimen"] != null)
            {
                ViewState["MasterData"] = (DataTable)Application["MasterData"];
                Application.Remove("MasterData");
                DataTable theDT = (DataTable)Application["AddRegimen"];
                ViewState["SelectedData"] = theDT;
                string theStr = FillRegimen(theDT);
                Application.Remove("AddRegimen");

            }
            else if ((DataTable)Application["AddRegimen1"] != null)
            {
                Session["ARVMasterData"] = (DataTable)Application["MasterData"];
                Application.Remove("MasterData");
                DataTable theDT = (DataTable)Application["AddRegimen1"];
                ViewState["TransSelectedData"] = theDT;
                string theStr = FillRegimen(theDT);
                txtanotherregimen.Value = theStr;
                Application.Remove("AddRegimen1");

            }



            if (ddleligibleThru.SelectedItem.Text == "Clinical")
            {
                lstClinicalStage.Enabled = true;
                txtCD4.Enabled = false;
                txtCD4percent.Enabled = false;
            }
            else if (ddleligibleThru.SelectedItem.Text == "CD4 count/percent")
            {
                lstClinicalStage.Enabled = false;
                txtCD4.Enabled = true;
                txtCD4percent.Enabled = true;
            }
            else
            {
                lstClinicalStage.Enabled = false;
                txtCD4.Enabled = false;
                txtCD4percent.Enabled = false;
            }


        }
        BMIAttributes();
        PId = Convert.ToInt32(Session["PatientId"]);
        FillOldData(PId);
    }
    private void Authenticate()
    {
        /***************** Check For User Rights ****************/
        if (Authentiaction.HasFunctionRight(ApplicationAccess.ARVTherapy, FunctionAccess.Print, (DataTable)Session["UserRight"]) == false)
        {
            btn_print.Enabled = false;
        }
        if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
        {
            if (Authentiaction.HasFunctionRight(ApplicationAccess.ARVTherapy, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
            {
                btn_save.Enabled = false;
                DQ_Check.Enabled = false;
            }
        }
        
        else
        {
            if (Authentiaction.HasFunctionRight(ApplicationAccess.ARVTherapy, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
            {
                string theUrl = "";
                theUrl = string.Format("{0}?sts={1}", "frmPatient_History.aspx", Session["HIVPatientStatus"].ToString());
                Response.Redirect(theUrl);
            }
            else if (Authentiaction.HasFunctionRight(ApplicationAccess.ARVTherapy, FunctionAccess.Update, (DataTable)Session["UserRight"]) == false)
            {
                btn_save.Enabled = false;
                DQ_Check.Enabled = false;
            }
            if (Convert.ToString(Session["HIVPatientStatus"]) == "1")
            {
                btn_save.Enabled = false;
                DQ_Check.Enabled = false;
            }
            //Privilages for Care End
            //if (Convert.ToString(Session["CareEndFlag"]) == "1" && Convert.ToString(Session["CareendedStatus"]) == "1")
            //{
            //    btn_save.Enabled = true;
            //    DQ_Check.Enabled = true;
            //}
        }
    }
    private void BindDropdowns()
    {
        DataSet theDS = new DataSet();
        theDS.ReadXml(MapPath("..\\XMLFiles\\ALLMasters.con"));
        BindFunctions BindManager = new BindFunctions();
        IQCareUtils theUtils = new IQCareUtils();
        
        if (theDS.Tables["Mst_Decode"] != null)
        {
            DataView theDVEligibility = new DataView(theDS.Tables["Mst_Decode"]);
            theDVEligibility.RowFilter = "CodeId=1007 and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
            theDVEligibility.Sort = "SRNo";
            if (theDVEligibility.Table != null)
            {
                DataTable theDTARVEligibility = (DataTable)theUtils.CreateTableFromDataView(theDVEligibility);

                BindManager.BindCombo(ddleligibleThru, theDTARVEligibility, "Name", "Id");
            }

            DataView theDVWHOStage = new DataView(theDS.Tables["Mst_Decode"]);
            theDVWHOStage.RowFilter = "CodeId=22 and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
            theDVWHOStage.Sort = "SRNo";
            if (theDVWHOStage.Table != null)
            {
                DataTable theDTWHOStage = (DataTable)theUtils.CreateTableFromDataView(theDVWHOStage);

                BindManager.BindCombo(lstanotherClinicalStage, theDTWHOStage, "Name", "Id");
                BindManager.BindCombo(lstthisClinicalStage, theDTWHOStage, "Name", "Id");
                BindManager.BindCombo(lstClinicalStage, theDTWHOStage, "Name", "Id");

            }
        }
        
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {

        if (Validate_Data_Quality() == false)
        {
            return;
        }
        IPatientARTCare ACManager;
        ACManager = (IPatientARTCare)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientARTCare, BusinessProcess.Clinical");
        LocationID = Convert.ToInt32(Session["AppLocationId"]);
        PatientID = Convert.ToInt32(Session["PatientId"]);

        if ((Convert.ToInt32(Session["PatientVisitId"]) > 0))
        {
            visitPK = Convert.ToInt32(Session["PatientVisitId"]);

        }

        //todo
        DataTable theCustomDataDT = null;
        if ((Convert.ToInt32(Session["PatientVisitId"]) > 0))
        {
            //  visitPK = Convert.ToInt32(Session["PatientVisitId"]);

            CustomFieldClinical theCustomManager = new CustomFieldClinical();
            theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Update", ApplicationAccess.ARVTherapy, (DataSet)ViewState["CustomFieldsDS"]);

        }
        else
        {
            CustomFieldClinical theCustomManager = new CustomFieldClinical();
            theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Insert", ApplicationAccess.ARVTherapy, (DataSet)ViewState["CustomFieldsDS"]);

        }


        Hashtable htparam = ARVTherapyParameters();
        ACManager.Save_Update_ARVTherapy(PatientID, visitPK, LocationID, htparam, Convert.ToInt32(Session["AppUserId"]), 0, theCustomDataDT);
        SaveCancel();

    }
    protected void DQ_Check_Click(object sender, EventArgs e)
    {
                
        if (Validate_Data_Quality() == false)
        {
            return;
        }
        string msg = DataQuality_Msg();
        if (msg.Length > 69)
        {
            MsgBuilder theBuilder1 = new MsgBuilder();
            theBuilder1.DataElements["MessageText"] = msg;
            IQCareMsgBox.Show("#C1", theBuilder1, this);
            return;
        }
        else
        {
            IPatientARTCare ACManager;
            ACManager = (IPatientARTCare)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientARTCare, BusinessProcess.Clinical");
            LocationID = Convert.ToInt32(Session["AppLocationId"]);
            PatientID = Convert.ToInt32(Session["PatientId"]);

            if ((Convert.ToInt32(Session["PatientVisitId"]) > 0))
            {
                visitPK = Convert.ToInt32(Session["PatientVisitId"]);

            }

            //todo
            DataTable theCustomDataDT = null;
            if ((Convert.ToInt32(Session["PatientVisitId"]) > 0))
            {
                //  visitPK = Convert.ToInt32(Session["PatientVisitId"]);

                CustomFieldClinical theCustomManager = new CustomFieldClinical();
                theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Update", ApplicationAccess.ARTHistory, (DataSet)ViewState["CustomFieldsDS"]);

            }
            else
            {
                CustomFieldClinical theCustomManager = new CustomFieldClinical();
                theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Insert", ApplicationAccess.ARTHistory, (DataSet)ViewState["CustomFieldsDS"]);

            }



            Hashtable htparam = ARVTherapyParameters();
            ACManager.Save_Update_ARVTherapy(PatientID, visitPK, LocationID, htparam, Convert.ToInt32(Session["AppUserId"]), 1, theCustomDataDT);
            SaveCancel();
        }
    }
    protected void btn_close_Click(object sender, EventArgs e)
    {
        string theUrl = "";
        if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
        {
            theUrl = string.Format("{0}", "frmPatient_Home.aspx");
        }
        else if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
        {
            theUrl = string.Format("{0}", "frmPatient_History.aspx");

        }
        
        Response.Redirect(theUrl);

    }
    protected void btn_print_Click(object sender, EventArgs e)
    {

    }
    protected void Init_ARTCare()
    {
        IPatientARTCare IEManager;
        try
        {
          
            //txtdateEligible.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3'), isCheckValidDate('" + Application["AppCurrentDate"] + "', '" + txtdateEligible.ClientID + "', '" + txtdateEligible.ClientID + "'), compareDates('" + Session["dob"] + "', '" + txtdateEligible.ClientID + "')");
            txtdateEligible.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            //txtanotherRegimendate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3'), isCheckValidDate('" + Session["AppCurrentDate"] + "', '" + txtanotherRegimendate.ClientID + "', '" + txtanotherRegimendate.ClientID + "'), compareDates('" + Session["dob"] + "', '" + txtanotherRegimendate.ClientID + "')");
            txtanotherRegimendate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            txtCD4.Attributes.Add("onKeyup", "chkNumeric('" + txtCD4.ClientID + "')");
            txtCD4percent.Attributes.Add("onKeyup", "chkNumeric('" + txtCD4percent.ClientID + "')");
            txtanotherwght.Attributes.Add("onKeyup", "chkNumeric('" + txtanotherwght.ClientID + "')");
            txtanotherheight.Attributes.Add("onKeyup", "chkNumeric('" + txtanotherheight.ClientID + "')");

            



            //Get BMI
            //IPatientHome PatientManager;
            //PatientManager = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
            //System.Data.DataSet theBMI = PatientManager.GetPatientDetails(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["SystemId"]), Convert.ToInt32(Session["TechnicalAreaId"]));
            //PatientManager = null;

            //Get ARV Therapy details
            IEManager = (IPatientARTCare)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientARTCare, BusinessProcess.Clinical");
            DataSet theDS = IEManager.GetPatientARVTherapy(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["AppLocationId"]));
            Session["ARVMasterData"] = theDS.Tables[1];
            ViewState["MasterData"] = theDS.Tables[1];
            ViewState["MasterARVData"] = theDS.Tables[1];
            if (theDS.Tables[15].Rows.Count > 0 && theDS.Tables[15].Rows[0]["DOB"] != System.DBNull.Value)
            {
                Session["dob"] = String.Format("{0:dd-MMM-yyyy}", theDS.Tables[15].Rows[0]["DOB"]);
            }
            else
            {
                Session["dob"] = "01-Jan-1900";
            }
            txtdateEligible.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3'), isCheckValidDate('" + Application["AppCurrentDate"] + "', '" + txtdateEligible.ClientID + "', '" + txtdateEligible.ClientID + "'), compareDates('" + Session["dob"] + "', '" + txtdateEligible.ClientID + "')");
            txtanotherRegimendate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3'), isCheckValidDate('" + Application["AppCurrentDate"] + "', '" + txtanotherRegimendate.ClientID + "', '" + txtanotherRegimendate.ClientID + "'), compareDates('" + Session["dob"] + "', '" + txtanotherRegimendate.ClientID + "')");
            //txtdateEligible.Attributes.Add("OnBlur", "compareDates('" + Session["dob"] + "', '" + txtdateEligible.ClientID + "')");
            //txtanotherRegimendate.Attributes.Add("OnBlur", "compareDates('" + Session["dob"] + "', '" + txtanotherRegimendate.ClientID + "')");

            if (theDS.Tables[4].Rows.Count>0 && theDS.Tables[4].Rows[0]["Visit_Id"] != System.DBNull.Value)
            {
                Session["PatientVisitId"] = theDS.Tables[4].Rows[0]["Visit_Id"].ToString();

               


            }
            else
                Session["PatientVisitId"] = 0;
            if (theDS.Tables[0].Rows.Count > 0)
            {
                if (theDS.Tables[0].Rows[0]["ARTStartDate"] != System.DBNull.Value)
                {
                    this.txtcohortmnth.Value = String.Format("{0:MMM}", theDS.Tables[0].Rows[0]["ARTStartDate"]).ToUpper();
                }
                if (theDS.Tables[0].Rows[0]["ARTStartDate"] != System.DBNull.Value)
                {
                    this.txtcohortyear.Value = String.Format("{0:yyyy}", theDS.Tables[0].Rows[0]["ARTStartDate"]).ToUpper();
                }

                // ART Start at This Facility Section
                if (theDS.Tables[0].Rows[0]["ARTStartDate"] != System.DBNull.Value)
                {
                    this.txtthisRegimendate.Value = String.Format("{0:dd-MMM-yyyy}", theDS.Tables[0].Rows[0]["ARTStartDate"]);

                    if (theDS.Tables[8].Rows.Count > 0 && theDS.Tables[8].Rows[0]["FirstLineRegimen"] != System.DBNull.Value)
                    {
                        txtthisregimen.Value = theDS.Tables[8].Rows[0]["FirstLineRegimen"].ToString();

                    }

                    if (theDS.Tables[3].Rows.Count > 0 && theDS.Tables[3].Rows[0]["weight"] != System.DBNull.Value)
                    {
                        this.txtthiswght.Value = theDS.Tables[3].Rows[0]["weight"].ToString();
                    }
                    if (theDS.Tables[2].Rows.Count > 0 && theDS.Tables[2].Rows[0]["height"] != System.DBNull.Value)
                    {
                        this.txtthisheight.Value = theDS.Tables[2].Rows[0]["height"].ToString();
                    }

                    if ((theDS.Tables[3].Rows.Count > 0 && theDS.Tables[3].Rows[0]["weight"] != System.DBNull.Value && theDS.Tables[3].Rows[0]["weight"].ToString() != "") || (theDS.Tables[2].Rows.Count > 0 && theDS.Tables[2].Rows[0]["height"] != System.DBNull.Value && theDS.Tables[2].Rows[0]["height"].ToString() != ""))
                    {
                        decimal thisWeight = Convert.ToDecimal(theDS.Tables[3].Rows[0]["weight"].ToString());
                        decimal thisHeight = Convert.ToDecimal(theDS.Tables[2].Rows[0]["height"].ToString());
                        if (thisWeight > 0 && thisHeight > 0)
                        {
                            decimal thisBMI = thisWeight / ((thisHeight / 100) * (thisHeight / 100));
                            thisBMI = Math.Round(thisBMI, 2);
                            txtthisbmi.Value = Convert.ToString(thisBMI);
                        }
                        else
                            txtthisbmi.Value = "";
                    }
                    //bmi
                    //if (theBMI.Tables[5].Rows.Count > 0 && theBMI.Tables[5].Rows[0]["BMI"] != System.DBNull.Value)
                    //{
                    //    this.txtthisbmi.Value = theBMI.Tables[5].Rows[0]["BMI"].ToString();
                    //}
                  
                    //this.txtthisbmi = calculateBMI(Convert.ToInt32(this.txtthiswght.Value), Convert.ToInt32(this.txtthisheight.Value));

                    if (theDS.Tables[9].Rows.Count > 0 && theDS.Tables[9].Rows[0]["whostage"] != System.DBNull.Value && theDS.Tables[9].Rows[0]["whostage"].ToString() != "")
                    {
                        this.lstthisClinicalStage.SelectedValue = theDS.Tables[9].Rows[0]["whostage"].ToString();
                    }
                }   
            }
            
            //////////////////////////////////////////////////////////////////////////////////////////////
            //Grid Substitution of ARVs
            grdSubsARVs.DataSource = theDS.Tables[12];
            grdSubsARVs.DataBind();

            /////////////////////////////////////////////////////////////////////////////////////////////
            
            //Grid Interuptions of ARVs
            grdInteruptions.DataSource = theDS.Tables[13];
            grdInteruptions.DataBind();
            
            
            //For Editing Records
            if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
            {
                if (theDS.Tables[11].Rows.Count > 0)
                {
                    // ART Start at another Facility Section
                    if ((theDS.Tables[11].Rows[0]["FirstLineRegStDate"] != System.DBNull.Value) || (theDS.Tables[11].Rows[0]["FirstLineRegStDate"] != "1900-01-01"))
                    {
                        string firsLineRegDate = String.Format("{0:dd-MMM-yyyy}", theDS.Tables[11].Rows[0]["FirstLineRegStDate"]);
                        if (firsLineRegDate.ToString() != "01-Jan-1900")
                        {
                            this.txtanotherRegimendate.Value = firsLineRegDate.ToString();
                        }
                    }
                    else if (theDS.Tables[16].Rows.Count > 0)
                    {
                        string firsanotherLineRegDate = String.Format("{0:dd-MMM-yyyy}", theDS.Tables[16].Rows[0]["ARTStartDate"]);
                        if (firsanotherLineRegDate.ToString() != "01-Jan-1900")
                        {
                            this.txtanotherRegimendate.Value = firsanotherLineRegDate.ToString();
                        }
                    }

                    if (txtanotherRegimendate.Value != string.Empty)
                    {
                        this.txtcohortmnth.Value = String.Format("{0:MMM}", Convert.ToDateTime(txtanotherRegimendate.Value)).ToUpper();
                    }
                    if (txtanotherRegimendate.Value != string.Empty)
                    {
                        this.txtcohortyear.Value = String.Format("{0:yyyy}", Convert.ToDateTime(txtanotherRegimendate.Value)).ToUpper();
                    }
                    if (theDS.Tables[11].Rows[0]["FirstLineReg"] != System.DBNull.Value || theDS.Tables[11].Rows[0]["FirstLineReg"] != "")
                    {
                        txtanotherregimen.Value = theDS.Tables[11].Rows[0]["FirstLineReg"].ToString();
                        string[] theStrRegimen = txtanotherregimen.Value.Split(new Char[] { '/' });
                        DataView theARVDV = new DataView(theDS.Tables[1]);
                        if (txtanotherregimen.Value != "")
                        {
                            ViewState["TransSelectedData"] = OldRegimenList(theStrRegimen, theARVDV);
                        }
                    }

                    if (theDS.Tables[11].Rows[0]["weight"] != System.DBNull.Value || theDS.Tables[11].Rows[0]["weight"] != "")
                    {
                        this.txtanotherwght.Value = theDS.Tables[11].Rows[0]["weight"].ToString();
                    }

                    if (theDS.Tables[11].Rows[0]["height"] != System.DBNull.Value || theDS.Tables[11].Rows[0]["height"] != "")
                    {
                        this.txtanotherheight.Value = theDS.Tables[11].Rows[0]["height"].ToString();
                    }
                    ///

                    if (((theDS.Tables[11].Rows[0]["Weight"].ToString() != "") && (theDS.Tables[11].Rows[0]["Weight"] != System.DBNull.Value)) || ((theDS.Tables[11].Rows[0]["Height"].ToString() != "") && (theDS.Tables[11].Rows[0]["Height"] != System.DBNull.Value)))
                    {
                        decimal anotherWeight = Convert.ToDecimal(theDS.Tables[11].Rows[0]["Weight"].ToString());
                        decimal anotherHeight = Convert.ToDecimal(theDS.Tables[11].Rows[0]["Height"].ToString());
                        if (anotherWeight > 0 && anotherHeight > 0)
                        {
                            decimal anotherBMI = anotherWeight / ((anotherHeight / 100) * (anotherHeight / 100));
                            anotherBMI = Math.Round(anotherBMI, 2);
                            txtanotherbmi.Value = Convert.ToString(anotherBMI);
                        }
                        else
                            txtanotherbmi.Value = "";
                    }


                    //bmi
                    //if (theBMI.Tables[5].Rows.Count > 0 && theBMI.Tables[5].Rows[0]["BMI"] != System.DBNull.Value)
                    //{
                    //    this.txtanotherbmi.Value = theBMI.Tables[5].Rows[0]["BMI"].ToString();
                    //}

                    if (theDS.Tables[11].Rows[0]["whostage"] != System.DBNull.Value)
                    {
                        this.lstanotherClinicalStage.SelectedValue = theDS.Tables[11].Rows[0]["whostage"].ToString();
                    }
                }


                if (theDS.Tables[14].Rows.Count > 0)
                {
                    // Medically Eligible
                    if ((theDS.Tables[14].Rows[0]["dateEligible"] != System.DBNull.Value) || (theDS.Tables[14].Rows[0]["dateEligible"] != "1900-01-01"))
                    {
                        string eligibleDate = String.Format("{0:dd-MMM-yyyy}", theDS.Tables[14].Rows[0]["dateEligible"]);
                        if (eligibleDate.ToString() != "01-Jan-1900")
                        {
                            this.txtdateEligible.Value = eligibleDate.ToString();
                        }
                    }

                    if (theDS.Tables[14].Rows[0]["eligibleThrough"] != System.DBNull.Value)
                    {
                        this.ddleligibleThru.SelectedValue = theDS.Tables[14].Rows[0]["eligibleThrough"].ToString();
                    }

                    if (theDS.Tables[14].Rows[0]["WHOStage"] != System.DBNull.Value)
                    {
                        this.lstClinicalStage.SelectedValue = theDS.Tables[14].Rows[0]["WHOStage"].ToString();
                    }

                    if (theDS.Tables[14].Rows[0]["CD4"] != System.DBNull.Value)
                    {
                        this.txtCD4.Text = theDS.Tables[14].Rows[0]["CD4"].ToString();
                    }
                    if (theDS.Tables[14].Rows[0]["CD4percent"] != System.DBNull.Value)
                    {
                        this.txtCD4percent.Text = theDS.Tables[14].Rows[0]["CD4percent"].ToString();
                    }
                }
                if (theDS.Tables[17].Rows.Count > 0)
                {
                    string ARTTransferDate = String.Format("{0:dd-MMM-yyyy}", theDS.Tables[17].Rows[0]["ARTTransferInDate"]);
                    if (ARTTransferDate.ToString() != "01-Jan-1900")
                    {
                        //this.txtanotherRegimendate.Value = firsanotherLineRegDate.ToString();
                        enableAnotherFacilityControls();
                    }
                    else
                    {
                        //DisableAnotherFacilityControls();
                    }
                }
                else
                {
                    //DisableAnotherFacilityControls();
                }
                //////////////////////////////////////////////////////////////////////////////////////////////
                //if (theDS1.Tables[0].Rows[0]["DataQuality"] != System.DBNull.Value && Convert.ToInt32(theDS1.Tables[0].Rows[0]["DataQuality"]) == 1)
                //{
                //    btncomplete.CssClass = "greenbutton";
                //}


            }
            else
            {
                if (theDS.Tables[16].Rows.Count > 0)
                {
                    string firsanotherLineRegDate = String.Format("{0:dd-MMM-yyyy}", theDS.Tables[16].Rows[0]["ARTStartDate"]);
                    if (firsanotherLineRegDate.ToString() != "01-Jan-1900")
                    {
                        this.txtanotherRegimendate.Value = firsanotherLineRegDate.ToString();
                    }
                }
                if (txtanotherRegimendate.Value != string.Empty)
                {
                    this.txtcohortmnth.Value = String.Format("{0:MMM}", Convert.ToDateTime(txtanotherRegimendate.Value)).ToUpper();
                }
                if (txtanotherRegimendate.Value != string.Empty)
                {
                    this.txtcohortyear.Value = String.Format("{0:yyyy}", Convert.ToDateTime(txtanotherRegimendate.Value)).ToUpper();
                }
                if (theDS.Tables[17].Rows.Count > 0)
                {
                    string ARTTransferDate = String.Format("{0:dd-MMM-yyyy}", theDS.Tables[17].Rows[0]["ARTTransferInDate"]);
                    if (ARTTransferDate.ToString() != "01-Jan-1900")
                    {
                        enableAnotherFacilityControls();
                    }
                    else
                    {
                       // DisableAnotherFacilityControls();
                    }
                }
                else
                {
                    //DisableAnotherFacilityControls();
                }
            }
            
        }
        // }
        catch (Exception err)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
            return;
        }
        finally
        {
            IEManager = null;
        }
    }
    private void enableAnotherFacilityControls()
    {
        txtanotherRegimendate.Disabled = false;
        txtanotherregimen.Disabled = false;
        txtanotherregimen.Disabled = false;
        btnanotherRegimen.Enabled = true;
        txtanotherwght.Disabled = false;
        txtanotherheight.Disabled = false;
        txtanotherbmi.Disabled = false;
        lstanotherClinicalStage.Enabled = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "imgdate", "document.getElementById('imgdate').style.visibility = 'visible';", true);
        

    }
    private void DisableAnotherFacilityControls()
    {
        txtanotherRegimendate.Disabled = true;
        txtanotherregimen.Disabled = true;
        txtanotherregimen.Disabled = true;
        btnanotherRegimen.Enabled = false;
        txtanotherwght.Disabled = true;
        txtanotherheight.Disabled = true;
        lstanotherClinicalStage.Enabled = false;
        txtanotherbmi.Disabled = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "imgdate", "document.getElementById('imgdate').style.visibility = 'hidden';", true);
        
    }

    private Boolean Validate_Data_Quality()
    {
        if (txtanotherwght.Value != "")
        {
            decimal weight = Convert.ToDecimal(txtanotherwght.Value);
            if (weight < 0 || weight > 250)
            {
                IQCareMsgBox.Show("chkWeight", this);
                txtanotherwght.Focus();
                return false;
            }
        }
        if (txtanotherheight.Value != "")
        {
            decimal height = Convert.ToDecimal(txtanotherheight.Value);

            if (height < 0 || height > 250)
            {
                IQCareMsgBox.Show("chkHeight", this);
                txtanotherheight.Focus();
                return false;
            }

        }
        if (txtanotherRegimendate.Value != "" && txtthisRegimendate.Value != "")
        {
            DateTime dtanother = Convert.ToDateTime(txtanotherRegimendate.Value.Trim()).Date;
            DateTime dtthis = Convert.ToDateTime(txtthisRegimendate.Value.Trim()).Date;
            if (dtanother >= dtthis)
            {
                IQCareMsgBox.Show("CmpARVTherapyDate", this);
                txtthisRegimendate.Focus();
                return false;
            }

        }


        return true;
    }
    #region "Function to meet the DataQuality Business rule"
    private string DataQuality_Msg()
    {
        string strmsg = "Following values are required to complete the data quality check:</br>";

        return strmsg;
    }
    #endregion

    private void SaveCancel()
    {
        int PatientID = Convert.ToInt32(Session["PatientId"]);

        IQCareMsgBox.NotifyAction("ART Therapy Form saved successfully. Do you want to close?", "ART Therapy Form", false, this, "window.location.href='frmPatient_History.aspx?sts=" + 0 + "';");
        ////string strPatientID = ViewState["PtnID"].ToString();
        //string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
        //script += "var ans;\n";
        //script += "ans=window.confirm('ART Therapy Form saved successfully. Do you want to close?');\n";
        //script += "if (ans==true)\n";
        //script += "{\n";
        //if (Session["Redirect"] == "0")
        //{
        //    script += "window.location.href='frmPatient_Home.aspx';\n";
        //}
        //else
        //{
        //    script += "window.location.href='frmPatient_History.aspx';\n";
        //}
        //script += "}\n";
        //script += "else \n";
        //script += "{\n";
        //script += "window.location.href='frmClinical_ARVTherapy.aspx'\n";
        //script += "}\n";
        //script += "</script>\n";
        //RegisterStartupScript("confirm", script);
    }
    private string FillRegimen(DataTable theDT)
    {
        string theRegimen = "";
        DataView theDV = new DataView();
        if (theDT.Rows.Count != 0)
        {
            for (int i = 0; i < theDT.Rows.Count; i++)
            {
                DataTable theFilDT = (DataTable)ViewState["MasterARVData"];
                if (theFilDT.Rows.Count > 0)
                {
                    theDV = new DataView(theFilDT);
                    theDV.RowFilter = "DrugId = " + theDT.Rows[i]["DrugId"]; //+ " and DrugTypeID = 37";
                    if (theDV.Count > 0)
                    {
                        for (int j = 0; j < theDV.Count; j++)
                        {
                            if (theRegimen == "")
                            {
                                theRegimen = theDV[j]["Abbr"].ToString();
                            }
                            else
                            {
                                theRegimen = theRegimen + "/" + theDV[j]["Abbr"].ToString();
                            }
                        }
                    }
                }
                theRegimen = theRegimen.Trim();

            }
        }
        return theRegimen;
    }

    protected void BMIAttributes()
    {
        txtanotherheight.Attributes.Add("OnBlur", "CalcualteBMI('" + txtanotherbmi.ClientID + "','" + txtanotherwght.ClientID + "','" + txtanotherheight.ClientID + "');");
        txtanotherwght.Attributes.Add("OnBlur", "CalcualteBMI('" + txtanotherbmi.ClientID + "','" + txtanotherwght.ClientID + "','" + txtanotherheight.ClientID + "');");

        //txtanotherheight.Attributes.Add("onchange", "CalcualteBMI('" + txtanotherbmi.ClientID + "','" + txtanotherwght.ClientID + "','" + txtanotherheight.ClientID + "');");
        //txtanotherwght.Attributes.Add("onchange", "CalcualteBMI('" + txtanotherbmi.ClientID + "','" + txtanotherwght.ClientID + "','" + txtanotherheight.ClientID + "');");

        txtthisheight.Attributes.Add("OnBlur", "CalcualteBMI('" + txtthisbmi.ClientID + "','" + txtthiswght.ClientID + "','" + txtthisheight.ClientID + "');");
        txtthiswght.Attributes.Add("OnBlur", "CalcualteBMI('" + txtthisbmi.ClientID + "','" + txtthiswght.ClientID + "','" + txtthisheight.ClientID + "');");
    }
    protected void ddleligibleThru_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddleligibleThru.SelectedItem.Text == "Clinical")
        {
            lstClinicalStage.Enabled = true;
            lstClinicalStage.SelectedIndex = 0;
            txtCD4.Text = "";
            txtCD4.Enabled = false;
            txtCD4percent.Text = "";
            txtCD4percent.Enabled = false;
        }
        else if (ddleligibleThru.SelectedItem.Text == "CD4 count/percent")
        {
            lstClinicalStage.Enabled = false;
            lstClinicalStage.SelectedIndex = 0;
            txtCD4.Text = "";
            txtCD4.Enabled = true;
            txtCD4percent.Text = "";
            txtCD4percent.Enabled = true;
        }
        else
        {
            lstClinicalStage.Enabled = false;
            lstClinicalStage.SelectedIndex = 0;
            txtCD4.Text = "";
            txtCD4.Enabled = false;
            txtCD4percent.Text = "";
            txtCD4percent.Enabled = false;
        }
    }

    private void PutCustomControl()
    {
        ICustomFields CustomFields;
        CustomFieldClinical theCustomField = new CustomFieldClinical();
        try
        {

            CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields,BusinessProcess.Administration");
            DataSet theDS = CustomFields.GetCustomFieldListforAForm(Convert.ToInt32(ApplicationAccess.ARVTherapy));
            if (theDS.Tables[0].Rows.Count != 0)
            {
                theCustomField.CreateCustomControlsForms(pnlCustomList, theDS, "ATherapy");
                //ViewState["CustomFieldsDS"] = theDS;
                //pnlCustomList.Visible = true;
            }
            //theCustomField.CreateCustomControlsForms(pnlCustomList, theDS, "PAHCare");
            ViewState["CustomFieldsDS"] = theDS;
            pnlCustomList.Visible = true;
        }
        catch (Exception err)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
        }
        finally
        {
            CustomFields = null;
        }

    }

    private void UpdateCustomFieldsValues()
    {
        GenerateCustomFieldsValues(pnlCustomList);
        string sqlstr = string.Empty;
        PatID = Convert.ToInt32(Session["PatientId"]);
        string sqlselect;
        string strdelete;
        Int32 visitID = 0;
        DateTime visitdate = System.DateTime.Now;
        ICustomFields CustomFields;
        //  if (txtvisitDate.Text.ToString() != "")

        visitdate = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MMM-yyyy"));

        if (ViewState["VisitID_add"] != null)
            visitID = Convert.ToInt32(ViewState["VisitID_add"]);

        if (sbValues.ToString().Trim() != "")
        {
            if (ViewState["CustomFieldsData"] != null)
            {
                sbValues = sbValues.Remove(0, 1);
                sqlstr = "UPDATE dtl_CustomField_" + TableName.ToString().Replace("-", "_") + " SET ";
                sqlstr += sbValues.ToString() + " where Ptn_pk= " + PatID.ToString() + " and Visit_pk=" + visitID.ToString();
            }
            else
            {
                sqlstr = "INSERT INTO dtl_CustomField_" + TableName.ToString().Replace("-", "_") + "(ptn_pk,LocationID,Visit_pk,Visit_Date " + sbParameter.ToString() + " )";
                sqlstr += " VALUES(" + PatID.ToString() + "," + Session["AppLocationID"] + "," + visitID + ",'" + visitdate + "'" + sbValues.ToString() + ")";
                ViewState["CustomFieldsData"] = 1;
            }


            try
            {
                CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
                icount = CustomFields.SaveCustomFieldValues(sqlstr.ToString());
                if (icount == -1)
                {
                    return;
                }
            }
            catch
            {
            }
            finally
            {
                CustomFields = null;
            }
        }
        if (strmultiselect.ToString() != "")
        {
            string[] FieldValues = strmultiselect.Split(new char[] { '^' });
            if (arl.Count != 0)
            {
                int p = 0;
                foreach (object obj in arl)
                {
                    sqlselect = "";
                    strdelete = "";
                    if (obj.ToString() != "")
                    {
                        try
                        {
                            CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
                            strdelete = "DELETE from [" + obj.ToString() + "] where ptn_pk= " + PatID.ToString() + " and LocationID=" + Session["AppLocationID"] + " and visit_pk=" + visitID;
                            icount = CustomFields.SaveCustomFieldValues(strdelete.ToString());

                            if (FieldValues[p].ToString() != "")
                            {
                                string[] mValues = FieldValues[p].Split(new char[] { ',' });

                                foreach (string str in mValues)
                                {
                                    if (str.ToString() != "")
                                    {
                                        string strtab = "dtl_CustomField_" + TableName.ToString().Replace("-", "_") + "_";
                                        Int32 ispos = Convert.ToInt32(strtab.Length);
                                        Int32 iepos = Convert.ToInt32(obj.ToString().Length) - ispos;

                                        sqlselect = "INSERT INTO [" + obj.ToString() + "](ptn_pk,LocationID,visit_pk,visit_Date, [" + obj.ToString().Substring(ispos, iepos) + "])";
                                        sqlselect += " VALUES (" + PatID.ToString() + "," + Session["AppLocationID"] + "," + visitID + ",'" + visitdate + "'," + str.ToString() + ")";



                                        icount = CustomFields.SaveCustomFieldValues(sqlselect.ToString());
                                        if (icount == -1)
                                        {
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                        catch
                        {
                        }
                        finally
                        {
                            CustomFields = null;
                        }
                    }
                    p += 1;
                }
            }
        }
    }

    private void InsertCustomFieldsValues()
    {
        GenerateCustomFieldsValues(pnlCustomList);
        string sqlstr = string.Empty;
        string sqlselect;
        Int32 visitID = 0;
        DateTime visitdate = System.DateTime.Now;
        PatID = Convert.ToInt32(Session["PatientId"]);
        ICustomFields CustomFields;
        if (ViewState["VisitID_add"] != null)
            visitID = Convert.ToInt32(ViewState["VisitID_add"]);
        //  if (txtvisitDate.Text.ToString() != "")
        visitdate = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MMM-yyyy"));

        if (sbValues.ToString().Trim() != "")
        {
            sqlstr = "INSERT INTO dtl_CustomField_" + TableName.ToString().Replace("-", "_") + "(ptn_pk,LocationID,Visit_pk,Visit_Date " + sbParameter.ToString() + " )";
            sqlstr += " VALUES(" + PatID.ToString() + "," + Session["AppLocationID"] + "," + visitID + ",'" + visitdate + "'" + sbValues.ToString() + ")";

            try
            {
                CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
                icount = CustomFields.SaveCustomFieldValues(sqlstr.ToString());
                if (icount == -1)
                {
                    return;
                }
            }
            catch
            {
            }
            finally
            {
                CustomFields = null;
            }
        }
        if (strmultiselect.ToString() != "")
        {
            string[] FieldValues = strmultiselect.Split(new char[] { '^' });
            if (arl.Count != 0)
            {
                int p = 0;
                foreach (object obj in arl)
                {
                    sqlselect = "";
                    if (obj.ToString() != "")
                    {
                        if (FieldValues[p].ToString() != "")
                        {
                            string[] mValues = FieldValues[p].Split(new char[] { ',' });
                            foreach (string str in mValues)
                            {
                                if (str.ToString() != "")
                                {
                                    string strtab = "dtl_CustomField_" + TableName.ToString().Replace("-", "_") + "_";
                                    Int32 ispos = Convert.ToInt32(strtab.Length);
                                    Int32 iepos = Convert.ToInt32(obj.ToString().Length) - ispos;
                                    sqlselect = "INSERT INTO [" + obj.ToString() + "](ptn_pk,LocationID,Visit_pk,Visit_Date, [" + obj.ToString().Substring(ispos, iepos) + "])";
                                    sqlselect += " VALUES (" + PatID.ToString() + "," + Session["AppLocationID"] + "," + visitID + ",'" + visitdate + "'," + str.ToString() + ")";
                                    try
                                    {
                                        CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
                                        icount = CustomFields.SaveCustomFieldValues(sqlselect.ToString());
                                        if (icount == -1)
                                        {
                                            return;
                                        }

                                    }
                                    catch
                                    {
                                    }
                                    finally
                                    {
                                        CustomFields = null;
                                    }
                                }
                            }
                        }
                    }
                    p += 1;
                }
            }
        }
    }

    private void GenerateCustomFieldsValues(Control Cntrl)
    {
        string pnlName = Cntrl.ID;
        sbValues = new StringBuilder();
        strmultiselect = string.Empty;
        string strfName = string.Empty;
        Boolean radioflag = false;

        Int32 stpos = 0;
        Int32 enpos = 0;
        if (ViewState["CustomFieldsData"] != null)
        {
            foreach (Control x in Cntrl.Controls)
            {
                if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                {
                    if (x.ID.Substring(0, 16).ToString().ToUpper() == pnlName.ToUpper() + "TXT")
                    {
                        strfName = pnlName.ToUpper() + "TXT";
                        stpos = strfName.Length;
                        enpos = x.ID.Length - stpos;
                        strfName = x.ID.Substring(stpos, enpos).ToString();
                        if (((TextBox)x).Text != "")
                        {
                            sbValues.Append(",[" + strfName + "] = '" + ((TextBox)x).Text.ToString() + "'");
                        }
                        else
                        {
                            sbValues.Append(",[" + strfName + "] = ' " + "'");
                        }
                    }
                    else if (x.ID.Substring(0, 16).ToString().ToUpper() == pnlName.ToUpper() + "NUM")
                    {
                        strfName = pnlName.ToUpper() + "NUM";
                        stpos = strfName.Length;
                        enpos = x.ID.Length - stpos;
                        strfName = x.ID.Substring(stpos, enpos).ToString();
                        if (((TextBox)x).Text != "")
                        {
                            sbValues.Append(",[" + strfName + "]=" + ((TextBox)x).Text.ToString());
                        }
                        else
                        {
                            sbValues.Append("," + strfName + "=Null");
                        }

                    }
                    else if (x.ID.Substring(0, 15).ToString().ToUpper() == pnlName.ToUpper() + "DT")
                    {
                        strfName = pnlName.ToUpper() + "DT";
                        stpos = strfName.Length;
                        enpos = x.ID.Length - stpos;
                        strfName = x.ID.Substring(stpos, enpos).ToString();
                        if (((TextBox)x).Text != "")
                        {
                            sbValues.Append(",[" + strfName + "]='" + Convert.ToDateTime(((TextBox)x).Text.ToString()) + "'");
                        }
                        else
                        {
                            sbValues.Append(",[" + strfName + "]=" + "Null");
                        }
                    }
                }
                if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputRadioButton))
                {
                    if (x.ID.Substring(0, 19).ToString().ToUpper() == pnlName.ToUpper() + "RADIO1")
                    {
                        radioflag = false;
                        strfName = pnlName.ToUpper() + "RADIO1";
                        stpos = strfName.Length;
                        enpos = x.ID.Length - stpos;
                        strfName = x.ID.Substring(stpos, enpos).ToString();
                        if (((HtmlInputRadioButton)x).Checked == true)
                        {
                            sbValues.Append(",[" + strfName + "]=" + "1");
                            radioflag = true;
                        }
                    }
                    if (x.ID.Substring(0, 19).ToString().ToUpper() == pnlName.ToUpper() + "RADIO2")
                    {
                        strfName = pnlName.ToUpper() + "RADIO2";
                        stpos = strfName.Length;
                        enpos = x.ID.Length - stpos;
                        strfName = x.ID.Substring(stpos, enpos).ToString();
                        if (((HtmlInputRadioButton)x).Checked == true)
                        {
                            sbValues.Append(",[" + strfName + "]=" + "0");
                            radioflag = true;
                        }
                        if (radioflag == false)
                        {
                            sbValues.Append(",[" + strfName + "]=" + "Null");
                        }
                    }

                }
                if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                {
                    if (x.ID.Substring(0, 23).ToString().ToUpper() == pnlName.ToUpper() + "SELECTLIST")
                    {
                        strfName = pnlName.ToUpper() + "SELECTLIST";
                        stpos = strfName.Length;
                        enpos = x.ID.Length - stpos;
                        strfName = x.ID.Substring(stpos, enpos).ToString();
                        if (((DropDownList)x).SelectedValue != "0")
                        {
                            sbValues.Append(",[" + strfName + "] = " + ((DropDownList)x).SelectedValue.ToString() + " ");
                        }
                        else
                        {
                            sbValues.Append(",[" + strfName + "] =  " + "0");
                        }

                    }
                }

            }
        }

        if (ViewState["CustomFieldsData"] == null)
        {
            foreach (Control x in Cntrl.Controls)
            {
                if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                {
                    if (x.ID.Substring(0, 16).ToString().ToUpper() == pnlName.ToUpper() + "TXT")
                    {
                        if (((TextBox)x).Text != "")
                        {
                            sbValues.Append(",'" + ((TextBox)x).Text.ToString() + "'");
                        }
                        else
                        {
                            sbValues.Append(",' " + "'");
                        }
                    }
                    else if (x.ID.Substring(0, 16).ToString().ToUpper() == pnlName.ToUpper() + "NUM")
                    {
                        if (((TextBox)x).Text != "")
                        {
                            sbValues.Append("," + ((TextBox)x).Text.ToString());
                        }
                        else
                        {
                            sbValues.Append(",0");
                        }

                    }
                    else if (x.ID.Substring(0, 15).ToString().ToUpper() == pnlName.ToUpper() + "DT")
                    {
                        if (((TextBox)x).Text != "")
                        {
                            sbValues.Append(",'" + Convert.ToDateTime(((TextBox)x).Text.ToString()) + "'");
                        }
                        else
                        {
                            sbValues.Append(",Null");
                        }
                    }
                }
                if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputRadioButton))
                {
                    if (x.ID.Substring(0, 19).ToString().ToUpper() == pnlName.ToUpper() + "RADIO1")
                    {
                        radioflag = false;
                        if (((HtmlInputRadioButton)x).Checked == true)
                        {
                            sbValues.Append(",1");
                            radioflag = true;
                        }
                    }
                    if (x.ID.Substring(0, 19).ToString().ToUpper() == pnlName.ToUpper() + "RADIO2")
                    {
                        if (((HtmlInputRadioButton)x).Checked == true)
                        {
                            sbValues.Append(",0");
                            radioflag = true;
                        }
                        if (radioflag == false)
                        {
                            sbValues.Append(",Null");
                        }
                    }
                }
                if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                {
                    if (x.ID.Substring(0, 23).ToString().ToUpper() == pnlName.ToUpper() + "SELECTLIST")
                    {
                        if (((DropDownList)x).SelectedValue != "0")
                        {
                            sbValues.Append("," + ((DropDownList)x).SelectedValue.ToString() + " ");
                        }
                        else
                        {
                            sbValues.Append(", " + "0");
                        }

                    }
                }
            }
        }
        if (ViewState["CustomFieldsMulti"] != null || ViewState["CustomFieldsMulti"] == null)
        {
            foreach (Control x in Cntrl.Controls)
            {
                if (x.GetType() == typeof(System.Web.UI.WebControls.CheckBoxList))
                {

                    if (x.ID.Substring(0, 28).ToString().ToUpper() == pnlName.ToUpper() + "MULTISELECTLIST")
                    {

                        foreach (ListItem li in ((CheckBoxList)x).Items)
                        {
                            if (Convert.ToInt32(li.Selected) == 1)
                            {
                                strmultiselect += " " + li.Value.ToString() + ",";
                            }
                        }
                        strmultiselect += "^";
                    }
                }
            }
        }
    }

    private void FillOldData(Int32 PatID)
    {
        DataSet dsvalues = null;
        ICustomFields CustomFields;

        try
        {
            DataSet theCustomFields = (DataSet)ViewState["CustomFieldsDS"];
            string theTblName = "";
            if (theCustomFields.Tables[0].Rows.Count > 0)
                theTblName = theCustomFields.Tables[0].Rows[0]["FeatureName"].ToString().Replace(" ", "_");
            string theColName = "";
            foreach (DataRow theDR in theCustomFields.Tables[0].Rows)
            {
                if (theDR["ControlId"].ToString() != "9")
                {
                    if (theColName == "")
                        theColName = theDR["Label"].ToString();
                    else
                        theColName = theColName + "," + theDR["Label"].ToString();
                }
            }

            CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");

            //todo AMit extra
            int visitPKval = Convert.ToInt32(Session["PatientVisitId"]);

            dsvalues = CustomFields.GetCustomFieldValues("dtl_CustomField_" + theTblName.ToString().Replace("-", "_"), theColName, Convert.ToInt32(PatID.ToString()), 0, visitPKval, 0, 0, Convert.ToInt32(ApplicationAccess.ARVTherapy));
            CustomFieldClinical theCustomManager = new CustomFieldClinical();
            theCustomManager.FillCustomFieldData(theCustomFields, dsvalues, pnlCustomList, "ATherapy");
        }
        catch (Exception err)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
        }
        finally
        {
            CustomFields = null;
        }
    }
}
