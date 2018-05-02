using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Interface.Clinical;
using Application.Common;
using Application.Presentation;
using Interface.Pharmacy;
using System.Collections.Generic;
using AjaxControlToolkit;
using System.Web.Script.Serialization;

using Interface.Security;


public partial class ClinicalForms_frmNAFDAC : LogPage
{
    IAllergyInfo PatientManager;
    AuthenticationManager Authentiaction = new AuthenticationManager();

    DataSet theDSDrug;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
        {
            IQCareMsgBox.Show("SessionExpired", this);
            Response.Redirect("~/frmlogin.aspx");
        }

        if (Session["PatientStatus"] != null)
        {
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text = Session["PatientStatus"].ToString();
        }

        if (Authentiaction.HasFunctionRight(ApplicationAccess.NAFDAC, FunctionAccess.Print, (DataTable)Session["UserRight"]) == false)
        {
            btnPrint.Enabled = false;
        }

        if (!IsPostBack)
        {
            if (Request.QueryString["name"] == "Add")
            {
                Session["PtnRedirect"] = Convert.ToInt32(Session["PatientId"]);
                Boolean blnRightfind = false;
                if (Authentiaction.HasFunctionRight(ApplicationAccess.NAFDAC, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
                {
                    string theUrl = string.Empty;
                    theUrl = string.Format("../ClinicalForms/frmPatient_Home.aspx");
                    Response.Redirect(theUrl);
                }
                else if (Authentiaction.HasFunctionRight(ApplicationAccess.NAFDAC, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
                {
                    blnRightfind = true;
                    btnSubmit.Enabled = false;
                    btnadd.Enabled = false;
                }
                else if (Authentiaction.HasFunctionRight(ApplicationAccess.NAFDAC, FunctionAccess.Update, (DataTable)Session["UserRight"]) == false)
                {
                    if (!blnRightfind)
                    {
                        btnSubmit.Enabled = false;
                        btnadd.Enabled = false;
                    }
                }
                ClearSession();
            }

            Session["PtnRedirect"] = Convert.ToInt32(Session["PatientId"]);
            Session["SaveFlag"] = "Add";
            Session["SelectedId"] = "";
            Session["SelectedRow"] = -1;
            Session["RemoveFlag"] = "False";

            Session["Ptn_Pk"] = Convert.ToInt32(Session["PatientId"]);
            if (Session["PtnRedirect"] == null)
            {
                Session["PtnRedirect"] = Convert.ToInt32(Session["PatientId"]);
            }

            Session["ReferenceId"] = "";
            Session["RegistrationNo"] = "";
            GetDrugGenericDetails(Convert.ToInt32(Session["PatientId"]));
            BindUserDropdown(ddlSignature, Session["AppUserId"].ToString());
            GetAllData();

        }

        txtAllergyDateStart.Attributes.Add("onblur", "isCheckValidDate('" + Application["AppCurrentDate"] + "', '" + txtAllergyDateStart.ClientID + "', '" + txtAllergyDateStart.ClientID + "');");
        txtAllergyDateEnd.Attributes.Add("onblur", "isCheckValidDate('" + Application["AppCurrentDate"] + "', '" + txtAllergyDateEnd.ClientID + "', '" + txtAllergyDateStart.ClientID + "');");
        txtDrugStartDate.Attributes.Add("onblur", "isCheckValidDate('" + Application["AppCurrentDate"] + "', '" + txtDrugStartDate.ClientID + "', '" + txtDrugStartDate.ClientID + "');");
        txtDrugStopDate.Attributes.Add("onblur", "isCheckValidDate('" + Application["AppCurrentDate"] + "', '" + txtDrugStopDate.ClientID + "', '" + txtDrugStopDate.ClientID + "');");
        txtCMDateStart.Attributes.Add("onblur", "isCheckValidDate('" + Application["AppCurrentDate"] + "', '" + txtCMDateStart.ClientID + "', '" + txtCMDateStart.ClientID + "');");
        txtCMDateStop.Attributes.Add("onblur", "isCheckValidDate('" + Application["AppCurrentDate"] + "', '" + txtCMDateStop.ClientID + "', '" + txtCMDateStop.ClientID + "');");
        txtDuration.Attributes.Add("onkeyup", "chkInteger('" + txtDuration.ClientID + "')");
    }

    private void BindUserDropdown(DropDownList DropDownID, String userId)
    {
        Dictionary<int, string> userList = new Dictionary<int, string>();
        CustomFieldClinical.BindUserDropDown(DropDownID, out userList);
        if (!string.IsNullOrEmpty(userId))
        {
            if (userList.ContainsKey(Convert.ToInt32(userId)))
            {
                DropDownID.SelectedValue = userId;
                //SecurityPerTabSignature = userId;
            }
        }
    }



    private void GetAllData()
    {
        if (Request.QueryString["back"] != null)
        {
            grdAllergy.Columns.Clear();
            grdCM.Columns.Clear();
            BindGrid("SD");
            BindGrid("CM");

            grdAllergy.DataSource = (DataTable)Session["GridDataADR"];
            grdAllergy.DataBind();
            grdCM.DataSource = (DataTable)Session["GridDataCM"];
            grdCM.DataBind();
        }
        else
        {

            PatientManager = (IAllergyInfo)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BAllergyInfo, BusinessProcess.Clinical");
            if (Session["Ptn_Pk"].ToString() != "0")
            {
                grdAllergy.Columns.Clear();
                grdCM.Columns.Clear();

                BindGrid("SD");
                BindGrid("CM");

                DataSet theDS = PatientManager.GetNAFDACDataByPatient(Convert.ToInt32(Session["Ptn_Pk"]));

                if (theDS.Tables[0].Rows.Count > 0)
                {
                    Session["GridDataADR"] = theDS.Tables[0];
                    grdAllergy.DataSource = (DataTable)Session["GridDataADR"];
                    grdAllergy.DataBind();
                }
                //else { Session["GridDataADR"] = grdAllergy.DataSource; }

                if (theDS.Tables[1].Rows.Count > 0)
                {
                    Session["GridDataCM"] = theDS.Tables[1];
                    grdCM.DataSource = (DataTable)Session["GridDataCM"];
                    grdCM.DataBind();
                }
                //else { Session["GridDataCM"] = grdCM.DataSource; }

                if (theDS.Tables[2].Rows.Count > 0)
                {
                    txtDescription.Text = theDS.Tables[2].Rows[0]["DrugReactionDescription"].ToString();
                    txtAllergyDateStart.Value = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(theDS.Tables[2].Rows[0]["DateDrugReactionStart"]));
                    txtAllergyDateEnd.Value = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(theDS.Tables[2].Rows[0]["DateDrugReactionStop"].ToString()));
                    rblOutcome.SelectedValue = theDS.Tables[2].Rows[0]["OutcomeOfReaction"].ToString();
                    txtORDesc.Text = theDS.Tables[2].Rows[0]["OutcomeOfReactionReason"].ToString();
                    rblReason.SelectedValue = theDS.Tables[2].Rows[0]["PatientAdmittedDueToADR"].ToString();
                    rblProlonged.SelectedValue = theDS.Tables[2].Rows[0]["WasitProlongedDueToADR"].ToString();
                    txtDuration.Text = theDS.Tables[2].Rows[0]["DurationOfAdmission"].ToString();
                    txtReaction.Text = theDS.Tables[2].Rows[0]["TreatmentOfReaction"].ToString();
                    ddlSignature.SelectedValue = theDS.Tables[2].Rows[0]["Signature"].ToString();
                }
            }
        }
    }

    private void BindGrid(string grdName)
    {
        if (grdName == "SD")
        {
            BoundField theCol1 = new BoundField();
            theCol1.HeaderText = "ptn_pk";
            theCol1.DataField = "ptn_pk";
            theCol1.Visible = false;
            theCol1.ItemStyle.CssClass = "textstyle";
            grdAllergy.Columns.Add(theCol1);

            BoundField theCol2 = new BoundField();
            theCol2.HeaderText = "Drug_pk";
            theCol2.DataField = "Drug_pk";
            theCol2.Visible = false;
            theCol2.ItemStyle.CssClass = "textstyle";
            grdAllergy.Columns.Add(theCol2);

            BoundField theCol3 = new BoundField();
            theCol3.HeaderText = "Drug Name";
            theCol3.DataField = "DrugName";
            theCol3.ItemStyle.CssClass = "textstyle";
            theCol3.ReadOnly = true;
            theCol3.ItemStyle.Width = 200;
            grdAllergy.Columns.Add(theCol3);

            BoundField theCol4 = new BoundField();
            theCol4.HeaderText = "Manufacturer";
            theCol4.DataField = "Manufacturer";
            theCol4.ItemStyle.CssClass = "textstyle";
            grdAllergy.Columns.Add(theCol4);

            BoundField theCol5 = new BoundField();
            theCol5.HeaderText = "Generic Name";
            theCol5.DataField = "GenericName";
            theCol5.ItemStyle.CssClass = "textstyle";
            theCol5.ReadOnly = true;
            grdAllergy.Columns.Add(theCol5);

            BoundField theCol14 = new BoundField();
            theCol14.HeaderText = "GenericID";
            theCol14.DataField = "Generic_ID";
            theCol14.ItemStyle.CssClass = "textstyle";
            theCol14.ReadOnly = true;
            theCol14.Visible = false;
            grdAllergy.Columns.Add(theCol14);

            BoundField theCol15 = new BoundField();
            theCol15.HeaderText = "BatchID";
            theCol15.DataField = "Batch_ID";
            theCol15.ItemStyle.CssClass = "textstyle";
            theCol15.ReadOnly = true;
            theCol15.Visible = false;
            grdAllergy.Columns.Add(theCol15);

            BoundField theCol6 = new BoundField();
            theCol6.HeaderText = "Batch No";
            theCol6.DataField = "BatchNo";
            theCol6.ItemStyle.CssClass = "textstyle";
            theCol6.ReadOnly = true;
            grdAllergy.Columns.Add(theCol6);

            BoundField theCol7 = new BoundField();
            theCol7.HeaderText = "Expiry Date";
            theCol7.DataField = "ExpiryDate";
            theCol7.ItemStyle.CssClass = "textstyle";
            theCol7.DataFormatString = "{0:dd-MMM-yyyy}";
            grdAllergy.Columns.Add(theCol7);

            BoundField theCol8 = new BoundField();
            theCol8.HeaderText = "Indication For Use";
            theCol8.DataField = "IndicationForUse";
            theCol8.ItemStyle.CssClass = "textstyle";
            theCol8.ReadOnly = true;
            theCol8.ItemStyle.Width = 200;
            grdAllergy.Columns.Add(theCol8);

            BoundField theCol9 = new BoundField();
            theCol9.HeaderText = "Dosage";
            theCol9.DataField = "Dosage";
            theCol9.ItemStyle.CssClass = "textstyle";
            grdAllergy.Columns.Add(theCol9);

            BoundField theCol10 = new BoundField();
            theCol10.HeaderText = "Route";
            theCol10.DataField = "Route";
            theCol10.ItemStyle.CssClass = "textstyle";
            theCol10.ReadOnly = true;
            grdAllergy.Columns.Add(theCol10);

            BoundField theCol11 = new BoundField();
            theCol11.HeaderText = "Date Started";
            theCol11.DataField = "DateStarted";
            theCol11.ItemStyle.CssClass = "textstyle";
            theCol11.DataFormatString = "{0:dd-MMM-yyyy}";
            grdAllergy.Columns.Add(theCol11);

            BoundField theCol12 = new BoundField();
            theCol12.HeaderText = "Date Stopped";
            theCol12.DataField = "DateStopped";
            theCol12.ItemStyle.CssClass = "textstyle";
            theCol12.DataFormatString = "{0:dd-MMM-yyyy}";
            grdAllergy.Columns.Add(theCol12);


            BoundField theCol13 = new BoundField();
            theCol13.HeaderText = "NAFDAC #";
            theCol13.DataField = "NAFDACNo";
            theCol13.ItemStyle.CssClass = "textstyle";
            theCol13.ReadOnly = true;
            grdAllergy.Columns.Add(theCol13);


            CommandField objfield = new CommandField();
            objfield.ButtonType = ButtonType.Link;
            objfield.DeleteText = "<img src='../Images/del.gif' alt='Delete this' border='0' />";
            objfield.ShowDeleteButton = true;
            grdAllergy.Columns.Add(objfield);
            grdAllergy.DataBind();
        }
        else if (grdName == "CM")
        {
            BoundField theCol1 = new BoundField();
            theCol1.HeaderText = "ptn_pk";
            theCol1.DataField = "ptn_pk";
            theCol1.Visible = false;
            theCol1.ItemStyle.CssClass = "textstyle";
            grdCM.Columns.Add(theCol1);

            BoundField theCol2 = new BoundField();
            theCol2.HeaderText = "Drug_pk";
            theCol2.DataField = "Drug_pk";
            theCol2.Visible = false;
            theCol2.ItemStyle.CssClass = "textstyle";
            grdCM.Columns.Add(theCol2);

            BoundField theCol14 = new BoundField();
            theCol14.HeaderText = "GenericID";
            theCol14.DataField = "Generic_ID";
            theCol14.ItemStyle.CssClass = "textstyle";
            theCol14.ReadOnly = true;
            theCol14.Visible = false;
            grdCM.Columns.Add(theCol14);

            BoundField theCol15 = new BoundField();
            theCol15.HeaderText = "BatchID";
            theCol15.DataField = "Batch_ID";
            theCol15.ItemStyle.CssClass = "textstyle";
            theCol15.ReadOnly = true;
            theCol15.Visible = false;
            grdCM.Columns.Add(theCol15);

            BoundField theCol3 = new BoundField();
            theCol3.HeaderText = "Drug Name";
            theCol3.DataField = "DrugName";
            theCol3.ItemStyle.CssClass = "textstyle";
            theCol3.ReadOnly = true;
            theCol3.ItemStyle.Width = 200;
            grdCM.Columns.Add(theCol3);

            BoundField theCol9 = new BoundField();
            theCol9.HeaderText = "Dosage";
            theCol9.DataField = "Dosage";
            theCol9.ItemStyle.CssClass = "textstyle";
            grdCM.Columns.Add(theCol9);

            BoundField theCol10 = new BoundField();
            theCol10.HeaderText = "Route";
            theCol10.DataField = "Route";
            theCol10.ItemStyle.CssClass = "textstyle";
            theCol10.ReadOnly = true;
            grdCM.Columns.Add(theCol10);

            BoundField theCol11 = new BoundField();
            theCol11.HeaderText = "Date Started";
            theCol11.DataField = "DateStarted";
            theCol11.ItemStyle.CssClass = "textstyle";
            theCol11.DataFormatString = "{0:dd-MMM-yyyy}";
            grdCM.Columns.Add(theCol11);

            BoundField theCol12 = new BoundField();
            theCol12.HeaderText = "Date Stopped";
            theCol12.DataField = "DateStopped";
            theCol12.ItemStyle.CssClass = "textstyle";
            theCol12.DataFormatString = "{0:dd-MMM-yyyy}";
            grdCM.Columns.Add(theCol12);

            //BoundField theCol15 = new BoundField();
            //theCol15.HeaderText = "Concomitant Medicine";
            //theCol15.DataField = "ConcomitantMedicine";
            //theCol15.ItemStyle.CssClass = "textstyle";
            //theCol15.ReadOnly = true;
            //theCol15.Visible = false;
            //grdCM.Columns.Add(theCol15);

            BoundField theCol16 = new BoundField();
            theCol16.HeaderText = "Reason For Use";
            theCol16.DataField = "ReasonForUse";
            theCol16.ItemStyle.CssClass = "textstyle";
            theCol16.ReadOnly = true;
            theCol16.ItemStyle.Width = 200;
            grdCM.Columns.Add(theCol16);

            CommandField objfield = new CommandField();
            objfield.ButtonType = ButtonType.Link;
            objfield.DeleteText = "<img src='../Images/del.gif' alt='Delete this' border='0' />";
            objfield.ShowDeleteButton = true;
            grdCM.Columns.Add(objfield);

            grdCM.DataBind();
        }
    }

    protected void GetDrugGenericDetails(int PatientID)
    {
        IPediatric PediatricManager;
        try
        {
            PediatricManager = (IPediatric)ObjectFactory.CreateInstance("BusinessProcess.Pharmacy.BPediatric, BusinessProcess.Pharmacy");
            DataSet theDrugDS = PediatricManager.GetDrugDetails();
            theDSDrug = new DataSet();
            theDSDrug.Tables.Add(theDrugDS.Tables[0].Copy());
            ViewState["MasterData"] = theDSDrug;
        }
        catch (Exception er)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = er.Message.ToString();
            IQCareMsgBox.Show("C1#", theBuilder, this);
        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod(EnableSession = true)]
    public static List<string> SearchDrugs(string prefixText, int count)
    {
        List<string> Drugsdetail = new List<string>();
        List<Drugs> lstDrugsDetail = GetDrugs(prefixText, count);
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        foreach (Drugs c in lstDrugsDetail)
        {
            Drugsdetail.Add(AutoCompleteExtender.CreateAutoCompleteItem(c.DrugName, serializer.Serialize(c)));
        }
        return Drugsdetail;
    }

    public static List<Drugs> GetDrugs(string prefixText, int count)
    {
        List<Drugs> items = new List<Drugs>();
        IDrug objRptFields;
        objRptFields = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.Pharmacy.BDrug,BusinessProcess.Pharmacy");
        string sqlQuery;
        sqlQuery = string.Format("Select d.Drug_pk, d.DrugName, m.Name as Manufacturer, g.GenericName, b.Name as BatchNo, b.ExpiryDate " +
                                 "from Mst_Drug d Left Join lnk_druggeneric lnk on lnk.Drug_pk = d.Drug_pk Left Join mst_Generic g on g.GenericID = lnk.GenericID " +
                                "Left Join mst_batch b on b.ItemID = d.Drug_pk Left Join Mst_Manufacturer m on m.Id = d.Manufacturer " +
                                "Where g.GenericName LIKE '%{0}%'", prefixText);

        DataTable dataTable = objRptFields.ReturnDatatableQuery(sqlQuery);
        if (dataTable.Rows.Count > 0)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                try
                {
                    Drugs item = new Drugs();
                    item.DrugId = (int)row["Drug_pk"];
                    item.DrugName = (string)row["DrugName"];
                    item.Manufacturer = (string)row["Manufacturer"];
                    item.GenericName = (string)row["GenericName"];
                    if (!DBNull.Value.Equals(row["BatchNo"]))
                    {
                        item.BatchNo = (string)row["BatchNo"];
                    }
                    else
                    {
                        item.BatchNo = string.Empty;
                    }
                    if (!DBNull.Value.Equals(row["ExpiryDate"]))
                    {
                        item.ExpiryDate = (DateTime)row["ExpiryDate"];
                    }
                    else
                    {
                        item.ExpiryDate = null;
                    }

                    items.Add(item);
                }
                catch (Exception ex)
                {

                }
            }
        }
        return items;
    }

    protected void txtAllergen_TextChanged(object sender, EventArgs e)
    {
        IQCareUtils theUtils = new IQCareUtils();
        DataView theAutoDV;
        DataView theExistsDV;
        DataSet theAutoDS = (DataSet)ViewState["MasterData"];
        int DrugId;
        if (hdCustID.Value != "")
        {
            if ((Convert.ToInt32(hdCustID.Value) != 0))
            {
                DrugId = Convert.ToInt32(hdCustID.Value);
                theAutoDV = new DataView(theAutoDS.Tables[0]);
                theAutoDV.RowFilter = "Drug_Pk = " + DrugId;
                DataTable theAutoDT = (DataTable)theUtils.CreateTableFromDataView(theAutoDV);
                if (ViewState["AddARV"] == null)
                {
                    DataTable theDT = new DataTable();
                    theDT.Columns.Add("DrugId", System.Type.GetType("System.Int32"));
                    theDT.Columns.Add("GenericId", System.Type.GetType("System.Int32"));
                    theDT.Columns.Add("BatchId", System.Type.GetType("System.Int32"));
                    theDT.Columns.Add("DrugName", System.Type.GetType("System.String"));
                    theDT.Columns.Add("Manufacturer", System.Type.GetType("System.String"));
                    theDT.Columns.Add("GenericName", System.Type.GetType("System.String"));
                    theDT.Columns.Add("BatchNo", System.Type.GetType("System.String"));
                    theDT.Columns.Add("ExpiryDate", System.Type.GetType("System.DateTime"));

                    ViewState["AddARV"] = theDT;
                }
                DataTable ExistDT = (DataTable)ViewState["AddARV"];
                theExistsDV = new DataView(ExistDT);
                theExistsDV.RowFilter = "DrugId =" + theAutoDT.Rows[0]["Drug_pk"];
                DataTable theSelExistsDT = (DataTable)theUtils.CreateTableFromDataView(theExistsDV);
                if (theSelExistsDT.Rows.Count == 0)
                {
                    DataRow DR = ExistDT.NewRow();
                    DR[0] = theAutoDT.Rows[0]["Drug_pk"];
                    ViewState["Drug_pk"] = theAutoDT.Rows[0]["Drug_pk"];
                    DR[1] = theAutoDT.Rows[0]["GenericID"];
                    ViewState["GenericID"] = theAutoDT.Rows[0]["GenericID"];
                    DR[2] = theAutoDT.Rows[0]["ID"];
                    ViewState["BatchID"] = theAutoDT.Rows[0]["ID"];
                    DR[3] = theAutoDT.Rows[0]["DrugName"];
                    DR[4] = theAutoDT.Rows[0]["Manufacturer"];
                    lblManufacturer.Text = theAutoDT.Rows[0]["Manufacturer"].ToString();
                    DR[5] = theAutoDT.Rows[0]["GenericName"];
                    lblGeneric.Text = theAutoDT.Rows[0]["GenericName"].ToString();
                    DR[6] = theAutoDT.Rows[0]["BatchNo"];
                    lblBatchNo.Text = theAutoDT.Rows[0]["BatchNo"].ToString();
                    DR[7] = theAutoDT.Rows[0]["ExpiryDate"];
                    DateTime? ExpiryDate = theAutoDT.Rows[0]["ExpiryDate"] as DateTime?;
                    txtExpiryDate.Value = String.Format("{0:dd-MMM-yyyy}", ExpiryDate);

                    ExistDT.Rows.Add(DR);
                }
                else
                {
                    IQCareMsgBox.Show("DrugExists", this);
                    txtAllergen.Text = "";
                    return;
                }
                hdCustID.Value = "";
            }
        }
        else
        {
            txtAllergen.Text = "";
            hdCustID.Value = "";
        }
    }

    protected void txtAllergenCM_TextChanged(object sender, EventArgs e)
    {
        IQCareUtils theUtils = new IQCareUtils();
        DataView theAutoDV;
        DataView theExistsDV;
        DataSet theAutoDS = (DataSet)ViewState["MasterData"];
        int DrugId;
        if (hdCustIDCM.Value != "")
        {
            if ((Convert.ToInt32(hdCustIDCM.Value) != 0))
            {
                DrugId = Convert.ToInt32(hdCustIDCM.Value);
                theAutoDV = new DataView(theAutoDS.Tables[0]);
                theAutoDV.RowFilter = "Drug_Pk = " + DrugId;
                DataTable theAutoDT = (DataTable)theUtils.CreateTableFromDataView(theAutoDV);
                if (ViewState["AddARVCM"] == null)
                {
                    DataTable theDT = new DataTable();
                    theDT.Columns.Add("DrugId", System.Type.GetType("System.Int32"));
                    theDT.Columns.Add("GenericID", System.Type.GetType("System.Int32"));
                    theDT.Columns.Add("BatchID", System.Type.GetType("System.Int32"));
                    theDT.Columns.Add("DrugName", System.Type.GetType("System.String"));
                    ViewState["AddARVCM"] = theDT;
                }
                DataTable ExistDT = (DataTable)ViewState["AddARVCM"];
                theExistsDV = new DataView(ExistDT);
                theExistsDV.RowFilter = "DrugId =" + theAutoDT.Rows[0]["Drug_pk"];
                DataTable theSelExistsDT = (DataTable)theUtils.CreateTableFromDataView(theExistsDV);
                if (theSelExistsDT.Rows.Count == 0)
                {
                    DataRow DR = ExistDT.NewRow();
                    DR[0] = theAutoDT.Rows[0]["Drug_pk"];
                    ViewState["Drug_pk_CM"] = theAutoDT.Rows[0]["Drug_pk"];
                    DR[1] = theAutoDT.Rows[0]["GenericID"];
                    ViewState["GenericID_CM"] = theAutoDT.Rows[0]["GenericID"];
                    DR[2] = theAutoDT.Rows[0]["ID"];
                    ViewState["BatchID_CM"] = theAutoDT.Rows[0]["ID"];
                    DR[3] = theAutoDT.Rows[0]["DrugName"];
                    ExistDT.Rows.Add(DR);
                }
                else
                {
                    IQCareMsgBox.Show("DrugExists", this);
                    txtAllergenCM.Text = "";
                    return;
                }
                hdCustIDCM.Value = "";
            }
        }
        else
        {
            txtAllergenCM.Text = "";
            hdCustIDCM.Value = "";
        }
    }

    protected void btnAdd(object sender, EventArgs e)
    {
        try
        {
            if (Authentiaction.HasFunctionRight(ApplicationAccess.NAFDAC, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
            {
                btnSubmit.Enabled = false;
                btnadd.Enabled = false;
            }
            if (FieldValidation())
            {
                DataRow[] foundRows = null;
                grdAllergy.Visible = true;
                DataTable theDTCM = new DataTable();
                theDTCM = (DataTable)Session["GridDataCM"];
                if (theDTCM != null)
                {
                    foundRows = theDTCM.Select("DrugName='" + txtAllergen.Text + "'");
                }

                DataTable theDTADR = new DataTable();
                theDTADR = (DataTable)Session["GridDataADR"];
                if (theDTADR != null)
                {
                    if (txtAllergen.Text != "")
                    {
                        //if (foundRows.Length == 0){}
                        foundRows = theDTADR.Select("Drug_pk='" + ViewState["Drug_pk"] + "'");
                    }
                }

                if (Session["SaveFlag"] == null) { Session["SaveFlag"] = "Add"; }

                if (Session["SaveFlag"].ToString() == "Add")
                {
                    //if (foundRows == null)  // if (foundRows.Length < 1)
                    //{
                    //Add mode - a new member is be added and he is not already in the grid
                    theDTADR = (DataTable)Session["GridDataADR"];
                    if (theDTADR != null)
                    {
                        DataRow theDR = theDTADR.NewRow();
                        theDR["Ptn_Pk"] = Session["Ptn_Pk"];
                        theDR["Drug_pk"] = ViewState["Drug_pk"];
                        theDR["Generic_ID"] = ViewState["GenericID"];
                        theDR["Batch_ID"] = ViewState["BatchID"];
                        theDR["DrugName"] = txtAllergen.Text;
                        theDR["GenericName"] = lblGeneric.Text;
                        theDR["BatchNo"] = lblBatchNo.Text;
                        theDR["NAFDACNo"] = txtNafdac.Text;
                        theDR["Manufacturer"] = lblManufacturer.Text;
                        //theDR["SuspectedDrug"] = 1;
                        theDR["IndicationForUse"] = txtIndication.Text;
                        theDR["Dosage"] = txtDosage.Text;
                        theDR["Route"] = txtRoute.Text;

                        if (string.IsNullOrEmpty(txtExpiryDate.Value) == false)
                        {
                            theDR["ExpiryDate"] = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(txtExpiryDate.Value));
                        }
                        if (string.IsNullOrEmpty(txtDrugStartDate.Value) == false)
                        {
                            theDR["DateStarted"] = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(txtDrugStartDate.Value));
                        }
                        if (string.IsNullOrEmpty(txtDrugStopDate.Value) == false)
                        {
                            theDR["DateStopped"] = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(txtDrugStopDate.Value));
                        }

                        theDTADR.Rows.Add(theDR);
                        Session["GridDataADR"] = theDTADR;
                    }
                    else
                    {

                        DataTable tb = CreateTable();

                        DataRow theDR = tb.NewRow();
                        theDR["Ptn_Pk"] = Session["Ptn_Pk"];
                        theDR["Drug_pk"] = ViewState["Drug_pk"];
                        theDR["Generic_ID"] = ViewState["GenericID"];
                        theDR["Batch_ID"] = ViewState["BatchID"];
                        theDR["DrugName"] = txtAllergen.Text;
                        theDR["GenericName"] = lblGeneric.Text;
                        theDR["BatchNo"] = lblBatchNo.Text;
                        theDR["NAFDACNo"] = txtNafdac.Text;
                        theDR["Manufacturer"] = lblManufacturer.Text;
                        //theDR["SuspectedDrug"] = 1;
                        theDR["IndicationForUse"] = txtIndication.Text;
                        theDR["Dosage"] = txtDosage.Text;
                        theDR["Route"] = txtRoute.Text;

                        if (string.IsNullOrEmpty(txtExpiryDate.Value) == false)
                        {
                            theDR["ExpiryDate"] = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(txtExpiryDate.Value));
                        }
                        if (string.IsNullOrEmpty(txtDrugStartDate.Value) == false)
                        {
                            theDR["DateStarted"] = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(txtDrugStartDate.Value));
                        }
                        if (string.IsNullOrEmpty(txtDrugStopDate.Value) == false)
                        {
                            theDR["DateStopped"] = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(txtDrugStopDate.Value));
                        }

                        tb.Rows.Add(theDR);
                        Session["GridDataADR"] = tb;
                    }

                    //}
                    //else
                    //{
                    //    IQCareMsgBox.Show("AllergyMemberExists", this);
                    //    Refresh();
                    //    return;
                    //}
                }
                else if (Session["SaveFlag"].ToString() == "Edit")
                {
                    //Edit mode- ie member is selected from grid
                    int r = Convert.ToInt32(Session["SelectedRow"]);
                    theDTADR.Rows[r]["Generic_ID"] = ViewState["GenericID"];
                    theDTADR.Rows[r]["Batch_ID"] = ViewState["BatchID"];
                    theDTADR.Rows[r]["DrugName"] = txtAllergen.Text;
                    theDTADR.Rows[r]["GenericName"] = lblGeneric.Text;
                    theDTADR.Rows[r]["BatchNo"] = lblBatchNo.Text;
                    theDTADR.Rows[r]["NAFDAC"] = txtNafdac.Text;
                    theDTADR.Rows[r]["Manufacturer"] = lblManufacturer.Text;
                    theDTADR.Rows[r]["SuspectedDrug"] = 1;
                    theDTADR.Rows[r]["IndicationForUse"] = txtIndication.Text;
                    theDTADR.Rows[r]["Dosage"] = txtDosage.Text;
                    theDTADR.Rows[r]["Route"] = txtRoute.Text;

                    if (string.IsNullOrEmpty(txtExpiryDate.Value) == false)
                    {
                        theDTADR.Rows[r]["ExpiryDate"] = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(txtExpiryDate.Value));
                    }
                    if (string.IsNullOrEmpty(txtDrugStartDate.Value) == false)
                    {
                        theDTADR.Rows[r]["DateStarted"] = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(txtDrugStartDate.Value));
                    }
                    if (string.IsNullOrEmpty(txtDrugStopDate.Value) == false)
                    {
                        theDTADR.Rows[r]["DateStopped"] = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(txtDrugStopDate.Value));
                    }

                    Session["GridDataADR"] = theDTADR;
                }

                grdAllergy.Columns.Clear();
                BindGrid("SD");
                //DataView dv = new DataView((DataTable)Session["GridDataADR"]);
                //dv.RowFilter = "ConcomitantMedicine = False";
                grdAllergy.DataSource = (DataTable)Session["GridDataADR"];
                grdAllergy.DataBind();
                Refresh();
                ViewState["Drug_pk"] = string.Empty;
                ViewState["GenericID"] = string.Empty;
                ViewState["BatchID"] = string.Empty;
                btnadd.Text = "Add Suspected Drug";


            }
        }
        catch (Exception ex)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = ex.Message.ToString();
            IQCareMsgBox.Show("C1#", theBuilder, this);
        }

    }

    private static DataTable CreateTable()
    {
        DataTable tb = new DataTable();
        tb.Columns.Add("Ptn_Pk", typeof(int));
        tb.Columns.Add("Drug_pk", typeof(int));
        tb.Columns.Add("Generic_ID", typeof(int));
        tb.Columns.Add("Batch_ID", typeof(int));
        tb.Columns.Add("DrugName", typeof(string));
        tb.Columns.Add("GenericName", typeof(string));
        tb.Columns.Add("BatchNo", typeof(string));
        tb.Columns.Add("NAFDACNo", typeof(string));
        tb.Columns.Add("Manufacturer", typeof(string));
        tb.Columns.Add("IndicationForUse", typeof(string));
        tb.Columns.Add("Dosage", typeof(string));
        tb.Columns.Add("Route", typeof(string));
        tb.Columns.Add("ExpiryDate", typeof(string));
        tb.Columns.Add("DateStarted", typeof(string));
        tb.Columns.Add("DateStopped", typeof(string));
        return tb;
    }

    protected void btnAddCM(object sender, EventArgs e)
    {
        try
        {
            if (Authentiaction.HasFunctionRight(ApplicationAccess.NAFDAC, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
            {
                btnSubmit.Enabled = false;
                btnaddcm.Enabled = false;
            }
            if (FieldValidationCM())
            {
                DataRow[] foundRows = null;
                grdCM.Visible = true;

                DataTable theDTADR = new DataTable();
                theDTADR = (DataTable)Session["GridDataADR"];
                if (theDTADR != null)
                {
                    foundRows = theDTADR.Select("DrugName='" + txtAllergenCM.Text + "'");
                }

                DataTable theDTCM = new DataTable();
                theDTCM = (DataTable)Session["GridDataCM"];
                if (theDTCM != null)
                {
                    if (txtAllergenCM.Text != "")
                    {
                        //if (foundRows.Length == 0) {  }
                        foundRows = theDTCM.Select("Drug_pk='" + ViewState["Drug_pk_CM"] + "'");
                    }
                }


                if (Session["SaveFlag"] == null)
                {
                    Session["SaveFlag"] = "Add";
                }
                if (Session["SaveFlag"].ToString() == "Add")
                {
                    //if (foundRows == null)   //(foundRows.Length < 1)
                    //{
                    //Add mode - a new member is be added and he is not already in the grid
                    theDTCM = (DataTable)Session["GridDataCM"];
                    if (theDTCM != null)
                    {
                        DataRow theDR = theDTCM.NewRow();

                        theDR["Ptn_Pk"] = Session["Ptn_Pk"];
                        theDR["Drug_pk"] = ViewState["Drug_pk_CM"];
                        theDR["Generic_ID"] = ViewState["GenericID_CM"];
                        theDR["Batch_ID"] = ViewState["BatchID_CM"];
                        theDR["DrugName"] = txtAllergenCM.Text;
                        //theDR["ConcomitantMedicine"] = 1;
                        theDR["ReasonForUse"] = txtReasonForUse.Text;
                        theDR["Dosage"] = txtCMDosage.Text;
                        theDR["Route"] = txtCMRoute.Text;

                        if (string.IsNullOrEmpty(txtCMDateStart.Value) == false)
                        {
                            theDR["DateStarted"] = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(txtCMDateStart.Value));
                        }
                        if (string.IsNullOrEmpty(txtCMDateStop.Value) == false)
                        {
                            theDR["DateStopped"] = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(txtCMDateStop.Value));
                        }

                        theDTCM.Rows.Add(theDR);
                        Session["GridDataCM"] = theDTCM;
                    }

                    else
                    {

                        DataTable tb = CreateTableCM();

                        DataRow theDR = tb.NewRow();

                        theDR["Ptn_Pk"] = Session["Ptn_Pk"];
                        theDR["Drug_pk"] = ViewState["Drug_pk_CM"];
                        theDR["Generic_ID"] = ViewState["GenericID_CM"];
                        theDR["Batch_ID"] = ViewState["BatchID_CM"];
                        theDR["DrugName"] = txtAllergenCM.Text;
                        //theDR["ConcomitantMedicine"] = 1;
                        theDR["ReasonForUse"] = txtReasonForUse.Text;
                        theDR["Dosage"] = txtCMDosage.Text;
                        theDR["Route"] = txtCMRoute.Text;

                        if (string.IsNullOrEmpty(txtCMDateStart.Value) == false)
                        {
                            theDR["DateStarted"] = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(txtCMDateStart.Value));
                        }
                        if (string.IsNullOrEmpty(txtCMDateStop.Value) == false)
                        {
                            theDR["DateStopped"] = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(txtCMDateStop.Value));
                        }

                        tb.Rows.Add(theDR);
                        Session["GridDataCM"] = tb;
                    }
                    //}
                    //else
                    //{
                    //    IQCareMsgBox.Show("AllergyMemberExists", this);
                    //    Refresh();
                    //    return;
                    //}
                }
                else if (Session["SaveFlag"].ToString() == "Edit")
                {
                    //Edit mode- ie member is selected from grid
                    int r = Convert.ToInt32(Session["SelectedRow"]);
                    theDTCM.Rows[r]["Generic_ID"] = ViewState["GenericID_CM"];
                    theDTCM.Rows[r]["Batch_ID"] = ViewState["BatchID_CM"];
                    theDTCM.Rows[r]["DrugName"] = txtAllergenCM.Text;
                    theDTCM.Rows[r]["ConcomitantMedicine"] = 1;
                    theDTCM.Rows[r]["ReasonForUse"] = txtReasonForUse.Text;
                    theDTCM.Rows[r]["Dosage"] = txtDosage.Text;
                    theDTCM.Rows[r]["Route"] = txtRoute.Text;

                    if (string.IsNullOrEmpty(txtCMDateStart.Value) == false)
                    {
                        theDTCM.Rows[r]["DateStarted"] = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(txtCMDateStart.Value));
                    }
                    if (string.IsNullOrEmpty(txtCMDateStop.Value) == false)
                    {
                        theDTCM.Rows[r]["DateStopped"] = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(txtCMDateStop.Value));
                    }

                    Session["GridDataCM"] = theDTCM;

                }
                grdCM.Columns.Clear();
                BindGrid("CM");
                //DataView dv = new DataView((DataTable)Session["GridDataCM"]);
                //dv.RowFilter = "ConcomitantMedicine = True";
                grdCM.DataSource = (DataTable)Session["GridDataCM"];
                grdCM.DataBind();
                Refresh();
                ViewState["Drug_pk_CM"] = string.Empty;
                ViewState["GenericID_CM"] = string.Empty;
                ViewState["BatchID_CM"] = string.Empty;
                btnaddcm.Text = "Add Concomitant Medicine";
            }
        }
        catch (Exception ex)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = ex.Message.ToString();
            IQCareMsgBox.Show("C1#", theBuilder, this);
        }

    }

    private static DataTable CreateTableCM()
    {
        DataTable tb = new DataTable();
        tb.Columns.Add("Ptn_Pk", typeof(int));
        tb.Columns.Add("Drug_pk", typeof(int));
        tb.Columns.Add("Generic_ID", typeof(int));
        tb.Columns.Add("Batch_ID", typeof(int));
        tb.Columns.Add("DrugName", typeof(string));
        tb.Columns.Add("ReasonForUse", typeof(string));
        tb.Columns.Add("Dosage", typeof(string));
        tb.Columns.Add("Route", typeof(string));
        tb.Columns.Add("DateStarted", typeof(string));
        tb.Columns.Add("DateStopped", typeof(string));
        return tb;
    }


    private bool Validation()
    {
        if (txtDescription.Text == "")
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "Description";
            IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
            txtDescription.Focus();
            return false;
        }

        if (txtAllergyDateStart.Value == "")
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "Date Start";
            IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
            txtAllergyDateStart.Focus();
            return false;
        }

        if (txtAllergyDateEnd.Value == "")
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "Date End";
            IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
            txtAllergyDateEnd.Focus();
            return false;
        }

        if (Convert.ToInt32(rblOutcome.SelectedValue) < 1)
        {

            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "Outcome of Reaction";
            IQCareMsgBox.Show("UncheckedButton", theBuilder, this);
            rblOutcome.Focus();
            return false;

        }

        var result = DateTime.Compare(Convert.ToDateTime(txtAllergyDateStart.Value), Convert.ToDateTime(txtAllergyDateEnd.Value));
        if (result > 0)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "Date Start";
            IQCareMsgBox.Show("CmpDateNAFDAC", theBuilder, this);
            txtAllergyDateStart.Focus();
            return false;
        }

       return true;

    }
    private bool FieldValidation()
    {
        IIQCareSystem IQCareSecurity;
        IQCareSecurity = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
        DateTime theCurrentDate = (DateTime)IQCareSecurity.SystemDate();
        IQCareUtils theUtils = new IQCareUtils();
        PatientManager = (IAllergyInfo)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BAllergyInfo, BusinessProcess.Clinical");

        if (txtAllergen.Text == "")
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "Drug";
            IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
            txtAllergen.Focus();
            return false;
        }

        if (ddlAllergen.SelectedIndex == 0)
        {
            MsgBuilder theMsg = new MsgBuilder();
            theMsg.DataElements["Control"] = "Drug";
            IQCareMsgBox.Show("BlankDropDown", theMsg, this);
            return false;
        }

        var result = DateTime.Compare(Convert.ToDateTime(txtDrugStartDate.Value), Convert.ToDateTime(txtDrugStopDate.Value));
        if (result > 0)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "Date Start";
            IQCareMsgBox.Show("CmpDateNAFDAC", theBuilder, this);
            txtDrugStartDate.Focus();
            return false;
        }
        return true;
    }
    private bool  FieldValidationCM()
    {
        IIQCareSystem IQCareSecurity;
        IQCareSecurity = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
        DateTime theCurrentDate = (DateTime)IQCareSecurity.SystemDate();
        IQCareUtils theUtils = new IQCareUtils();
        PatientManager = (IAllergyInfo)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BAllergyInfo, BusinessProcess.Clinical");
        if (txtAllergenCM.Text == "")
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "Drug";
            IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
            txtAllergenCM.Focus();
            return false;
        }

        if (ddlAllergenCM.SelectedIndex == 0)
        {
            MsgBuilder theMsg = new MsgBuilder();
            theMsg.DataElements["Control"] = "Drug";
            IQCareMsgBox.Show("BlankDropDown", theMsg, this);
            return false;
        }
        
        var result = DateTime.Compare(Convert.ToDateTime(txtCMDateStart.Value), Convert.ToDateTime(txtCMDateStop.Value));
        if (result > 0)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "Date Start";
            IQCareMsgBox.Show("CmpDateNAFDAC", theBuilder, this);
            txtDrugStartDate.Focus();
            return false;
        }
        return true;
    }

    protected void grdAllergy_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        if (Authentiaction.HasFunctionRight(ApplicationAccess.NAFDAC, FunctionAccess.Update, (DataTable)Session["UserRight"]) == true)
        {
            btnSubmit.Enabled = true;
            btnadd.Enabled = true;
        }
        if (Session["lblpntstatus"].ToString() == "1")
        {
            btnadd.Enabled = false;
            btnSubmit.Enabled = false;
        }
        else
        {
            btnadd.Enabled = true;
        }
        int thePage = grdAllergy.PageIndex;
        int thePageSize = grdAllergy.PageSize;
        GridViewRow theRow = grdAllergy.Rows[e.NewSelectedIndex];
        int theIndex = thePageSize * thePage + theRow.RowIndex;
        System.Data.DataTable theDT = new System.Data.DataTable();
        theDT = ((DataTable)Session["GridDataADR"]);
        int r = theIndex;
        // Fill data in Textboxes from grid
        //Edit the selected row
        if (theDT.Rows.Count > 0)
        {
            txtAllergen.Text = "";
            ddlAllergen.Visible = true;
            ddlAllergen.SelectedValue = "0";
            GetDrugGenericDetails(Convert.ToInt32(Session["PatientId"]));
            txtAllergen.Text = theDT.Rows[r]["DrugName"].ToString();

            if (theDT.Rows[r]["txtDrugStartDate"].ToString() != "")
            {
                txtDrugStartDate.Value = String.Format("{0:dd-MMM-yyyy}", theDT.Rows[r]["txtDrugStartDate"].ToString());
            }
            else
            {
                txtDrugStartDate.Value = "";
            }
            Session["SelectedRow"] = theIndex;
            Session["SaveFlag"] = "Edit";
            btnadd.Text = "Update Suspected Drug";
        }
    }

    protected void grdAllergy_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //So that when the user clicks on the row - the corresponding row is edited
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < 15; i++)
            {
                if (e.Row.Cells[0].Text.ToString() != "0")
                {
                    if (Session["lblpntstatus"] != null)
                    {
                        if (Authentiaction.HasFunctionRight(ApplicationAccess.NAFDAC, FunctionAccess.Update, (DataTable)Session["UserRight"]) == true && Session["lblpntstatus"].ToString() != "1")
                        {
                            e.Row.Cells[i].Attributes.Add("onmouseover", "this.style.cursor='pointer';this.style.BackColor='#666699';");
                            e.Row.Cells[i].Attributes.Add("onmouseout", "this.style.backgroundColor='';");
                            e.Row.Cells[i].Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(grdAllergy, "Select$" + e.Row.RowIndex.ToString()));
                        }
                    }
                }
                if (e.Row.Cells[0].Text.ToString() == "0")
                {
                    e.Row.Cells[i].BackColor = System.Drawing.Color.LightGray;
                    e.Row.Cells[15].Visible = false;
                }
            }
            if (Session["lblpntstatus"] != null)
            {
                if (Authentiaction.HasFunctionRight(ApplicationAccess.NAFDAC, FunctionAccess.Delete, (DataTable)Session["UserRight"]) == true && Session["lblpntstatus"].ToString() != "1")
                {
                    LinkButton objlink = (LinkButton)e.Row.Cells[15].Controls[0];
                    objlink.OnClientClick = "if(!confirm('Are you sure you want to delete this?')) return false;";
                    e.Row.Cells[15].ID = e.Row.RowIndex.ToString();
                }
            }
            if (Session["CareEndFlag"].ToString() == "1")
            {
                e.Row.Cells[15].Visible = false;
            }
        }
    }

    protected void grdAllergy_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        System.Data.DataTable theDT = new System.Data.DataTable();
        theDT = ((DataTable)Session["GridDataADR"]);

        int r = Convert.ToInt32(e.RowIndex.ToString());
        int Id = -1;
        try
        {
            if (theDT.Rows.Count > 0)
            {
                if (theDT.Rows[r].HasErrors == false)
                {
                    if ((theDT.Rows[r]["Drug_pk"] != null) && (theDT.Rows[r]["Drug_pk"] != DBNull.Value))
                    {
                        if (theDT.Rows[r]["Drug_pk"].ToString() != "")
                        {
                            Id = Convert.ToInt32(theDT.Rows[r]["Drug_pk"]);
                            PatientManager = (IAllergyInfo)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BAllergyInfo, BusinessProcess.Clinical");
                            PatientManager.DeleteNAFDACDataByDrug(Id, Convert.ToInt32(Session["AppUserId"]));
                        }
                    }
                }
                theDT.Rows[r].Delete();
                theDT.AcceptChanges();
                Session["GridDataADR"] = theDT;
                grdAllergy.Columns.Clear();
                BindGrid("SD");
                //DataView dv = new DataView((DataTable)Session["GridData"]);
                //dv.RowFilter = "SuspectedDrug = 1";
                grdAllergy.DataSource = (DataTable)Session["GridDataADR"];
                grdAllergy.DataBind();

                System.Data.DataTable theDTV = new System.Data.DataTable();
                theDTV = ((DataTable)ViewState["AddARV"]);
                for (int i = theDTV.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = theDTV.Rows[i];
                    if (dr["DrugId"].ToString() == Id.ToString())
                        dr.Delete();
                    theDTV.AcceptChanges();
                    ViewState["AddARV"] = theDTV;
                }

                Refresh();
                IQCareMsgBox.Show("DeleteSuccess", this);

            }
            else
            {
                grdAllergy.Visible = false;
                IQCareMsgBox.Show("DeleteSuccess", this);
                Refresh();
            }
        }
        catch (Exception ex)
        {
            string str = ex.Message;
        }
    }

    protected void grdCM_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        if (Authentiaction.HasFunctionRight(ApplicationAccess.NAFDAC, FunctionAccess.Update, (DataTable)Session["UserRight"]) == true)
        {
            btnSubmit.Enabled = true;
            btnaddcm.Enabled = true;
        }
        if (Session["lblpntstatus"].ToString() == "1")
        {
            btnaddcm.Enabled = false;
            btnSubmit.Enabled = false;
        }
        else
        {
            btnaddcm.Enabled = true;
        }
        int thePage = grdCM.PageIndex;
        int thePageSize = grdCM.PageSize;
        GridViewRow theRow = grdCM.Rows[e.NewSelectedIndex];
        int theIndex = thePageSize * thePage + theRow.RowIndex;
        System.Data.DataTable theDT = new System.Data.DataTable();
        theDT = ((DataTable)Session["GridDataADR"]);
        int r = theIndex;
        // Fill data in Textboxes from grid
        //Edit the selected row
        if (theDT.Rows.Count > 0)
        {

            txtAllergenCM.Text = "";
            ddlAllergenCM.Visible = true;
            ddlAllergenCM.SelectedValue = "0";
            txtAllergenCM.Text = theDT.Rows[r]["DrugName"].ToString();
            GetDrugGenericDetails(Convert.ToInt32(Session["PatientId"]));
            txtAllergenCM.Visible = true;

            if (theDT.Rows[r]["DateStarted"].ToString() != "")
            {
                txtCMDateStart.Value = String.Format("{0:dd-MMM-yyyy}", theDT.Rows[r]["DateStarted"].ToString());
            }
            else
            {
                txtCMDateStart.Value = "";
            }


            Session["SelectedRow"] = theIndex;
            Session["SaveFlag"] = "Edit";
            btnaddcm.Text = "Update Concomitant Medicines";
        }
    }

    protected void grdCM_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //So that when the user clicks on the row - the corresponding row is edited
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < 10; i++)
            {
                if (e.Row.Cells[0].Text.ToString() != "0")
                {
                    if (Session["lblpntstatus"] != null)
                    {
                        if (Authentiaction.HasFunctionRight(ApplicationAccess.NAFDAC, FunctionAccess.Update, (DataTable)Session["UserRight"]) == true && Session["lblpntstatus"].ToString() != "1")
                        {
                            e.Row.Cells[i].Attributes.Add("onmouseover", "this.style.cursor='pointer';this.style.BackColor='#666699';");
                            e.Row.Cells[i].Attributes.Add("onmouseout", "this.style.backgroundColor='';");
                            e.Row.Cells[i].Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(grdCM, "Select$" + e.Row.RowIndex.ToString()));
                        }
                    }
                }
                if (e.Row.Cells[0].Text.ToString() == "0")
                {
                    e.Row.Cells[i].BackColor = System.Drawing.Color.LightGray;
                    e.Row.Cells[10].Visible = false;
                }
            }
            if (Session["lblpntstatus"] != null)
            {
                if (Authentiaction.HasFunctionRight(ApplicationAccess.NAFDAC, FunctionAccess.Delete, (DataTable)Session["UserRight"]) == true && Session["lblpntstatus"].ToString() != "1")
                {
                    LinkButton objlink = (LinkButton)e.Row.Cells[10].Controls[0];
                    objlink.OnClientClick = "if(!confirm('Are you sure you want to delete this?')) return false;";
                    e.Row.Cells[10].ID = e.Row.RowIndex.ToString();
                }
            }
            if (Session["CareEndFlag"].ToString() == "1")
            {
                e.Row.Cells[10].Visible = false;
            }
        }
    }

    protected void grdCM_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable theDT = new System.Data.DataTable();
        theDT = ((DataTable)Session["GridDataCM"]);
        int r = Convert.ToInt32(e.RowIndex.ToString());
        int Id = -1;
        try
        {
            if (theDT.Rows.Count > 0)
            {
                if (theDT.Rows[r].HasErrors == false)
                {
                    if ((theDT.Rows[r]["Drug_pk"] != null) && (theDT.Rows[r]["Drug_pk"] != DBNull.Value))
                    {
                        if (theDT.Rows[r]["Drug_pk"].ToString() != "")
                        {
                            Id = Convert.ToInt32(theDT.Rows[r]["Drug_pk"]);
                            PatientManager = (IAllergyInfo)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BAllergyInfo, BusinessProcess.Clinical");
                            PatientManager.DeleteNAFDACDataByDrug(Id, Convert.ToInt32(Session["AppUserId"]));
                        }
                    }
                }
                theDT.Rows[r].Delete();
                theDT.AcceptChanges();
                Session["GridDataCM"] = theDT;
                grdCM.Columns.Clear();
                BindGrid("CM");
                //DataView dv = new DataView((DataTable)Session["GridData"]);
                //dv.RowFilter = "ConcomitantMedicine = 1";
                grdCM.DataSource = (DataTable)Session["GridDataCM"];
                grdCM.DataBind();


                DataTable theDTV = new System.Data.DataTable();
                theDTV = ((DataTable)ViewState["AddARVCM"]);
                for (int i = theDTV.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = theDTV.Rows[i];
                    if (dr["DrugId"].ToString() == Id.ToString())
                        dr.Delete();
                    theDTV.AcceptChanges();
                    ViewState["AddARVCM"] = theDTV;
                }

                Refresh();
                IQCareMsgBox.Show("DeleteSuccess", this);
            }
            else
            {
                grdCM.Visible = false;
                IQCareMsgBox.Show("DeleteSuccess", this);
                Refresh();
            }
        }
        catch (Exception ex)
        {
            string str = ex.Message;
        }
    }

    private void Refresh()
    {
        txtAllergen.Text = string.Empty;
        ddlAllergen.SelectedIndex = -1;
        lblGeneric.Text = string.Empty;
        lblBatchNo.Text = string.Empty;
        lblManufacturer.Text = string.Empty;
        txtExpiryDate.Value = string.Empty;
        txtNafdac.Text = string.Empty;
        txtIndication.Text = string.Empty;
        txtDosage.Text = string.Empty;
        txtRoute.Text = string.Empty;
        txtDrugStartDate.Value = string.Empty;
        txtDrugStopDate.Value = string.Empty;

        txtAllergenCM.Text = string.Empty;
        ddlAllergenCM.SelectedIndex = -1;
        txtReasonForUse.Text = string.Empty;
        txtCMDateStart.Value = string.Empty;
        txtCMDateStop.Value = string.Empty;
        txtCMDosage.Text = string.Empty;
        txtCMRoute.Text = string.Empty;

        Session["SaveFlag"] = "Add";
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        ClearSession();
        string theUrl = string.Empty;
        if (Session["CEForm"] == null)
        {
            if (Request.QueryString["name"] != null)
            {
                if (Request.QueryString["name"] == "Edit")
                {
                    theUrl = string.Format("../ClinicalForms/frmPatient_Home.aspx");
                }
                else
                {
                    if (Session["SystemId"].ToString() == "1")
                    {
                        if (Session["fmLocationID"] != null && Session["fmsts"] != null)
                        {
                            theUrl = string.Format("../ClinicalForms/frmPatient_Home.aspx");
                        }
                        else
                        {
                            theUrl = string.Format("../ClinicalForms/frmPatient_Home.aspx");
                            Response.Redirect(theUrl);
                        }
                    }
                    else
                    {
                        if (Session["fmLocationID"] != null && Session["fmsts"] != null)
                        {
                            theUrl = string.Format("../ClinicalForms/frmClinical_PatientRegistrationCTC.aspx?name=Edit&locationid=" + Session["fmLocationID"].ToString() + "&sts=" + Session["fmsts"].ToString() + "");
                        }
                        else
                        {
                            theUrl = string.Format("../ClinicalForms/frmPatient_Home.aspx");
                            Response.Redirect(theUrl);
                        }
                    }
                }
                Response.Redirect(theUrl);
            }
            else
            {
                if (Request.QueryString["Refid"] != null)
                {
                    theUrl = string.Format("../ClinicalForms/frmPatient_Home.aspx");
                    Response.Redirect(theUrl);
                }
                else
                {
                    if (Session["PtnRedirect"] != null && Session["fmLocationID"] != null)
                    {
                        if (Session["SystemId"].ToString() == "1")
                        {
                            theUrl = string.Format("../ClinicalForms/frmClinical_Enrolment.aspx?name=Edit&locationid=" + Session["fmLocationID"].ToString() + "&sts=" + Session["fmsts"].ToString() + "");
                        }
                        else
                        {
                            theUrl = string.Format("../ClinicalForms/frmClinical_PatientRegistrationCTC.aspx?name=Edit&locationid=" + Session["fmLocationID"].ToString() + "&sts=" + Session["fmsts"].ToString() + "");
                        }

                        Response.Redirect(theUrl);
                    }
                    else
                    {
                        theUrl = string.Format("../frmFindAddPatient.aspx?FormName=FamilyInfo");
                        Response.Redirect(theUrl);
                    }
                }
            }
        }
        else
        {
            theUrl = string.Format("../ClinicalForms/frmClinical_Enrolment.aspx?name=Edit&locationid=" + Session["fmLocationID"].ToString() + "&sts=" + Session["fmsts"].ToString() + "");
            Response.Redirect(theUrl);
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            bool allMandatoryData = true;
            DataTable theDTADR = (DataTable)Session["GridDataADR"];
            DataTable theDTCM = (DataTable)Session["GridDataCM"];
            if (theDTCM == null || theDTCM.Rows.Count < 1) { allMandatoryData = FieldValidationCM(); }
            if (theDTADR == null || theDTADR.Rows.Count < 1) { allMandatoryData = FieldValidation(); }
            if (Validation() == false) { allMandatoryData = false; }

            if (allMandatoryData)
            {
                int Ptn_Pk = 0, userID = 0, deleteFlag = 0, signature = 0, drugPk = 0, suspectedDrug = 0, concomitantMedicine = 0, genericID = 0, batchID = 0;
                string description = string.Empty, dateReactionStart = string.Empty, dateReactionStop = string.Empty, outcomeReaction = string.Empty,
                    admitDuetoADR = string.Empty, prolongedADR = string.Empty, durationOfAdmission = string.Empty, reactionTreatment = string.Empty,
                    indicationForUse = string.Empty, dosage = string.Empty, route = string.Empty, dateStarted = string.Empty,
                    dateStopped = string.Empty, reasonForUse = string.Empty, nafdacNo = string.Empty, outcomeReactionDesc = string.Empty;

                PatientManager = (IAllergyInfo)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BAllergyInfo, BusinessProcess.Clinical");

                Ptn_Pk = Convert.ToInt32(Session["Ptn_Pk"]);
                description = txtDescription.Text;
                dateReactionStart = txtAllergyDateStart.Value;
                dateReactionStop = txtAllergyDateEnd.Value;
                outcomeReaction = rblOutcome.SelectedItem.Value;
                outcomeReactionDesc = txtORDesc.Text;
                if (rblReason.SelectedItem != null) { admitDuetoADR = rblReason.SelectedItem.Value; }
                if (rblProlonged.SelectedItem != null) { prolongedADR = rblProlonged.SelectedItem.Value; }
                durationOfAdmission = txtDuration.Text;
                reactionTreatment = txtReaction.Text;
                signature = Convert.ToInt16(ddlSignature.SelectedItem.Value);
                userID = Convert.ToInt32(Session["AppUserId"]);
                deleteFlag = 0;


                if (theDTADR != null && theDTADR.Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDTADR.Rows)
                    {
                        suspectedDrug = 1;
                        if (!string.IsNullOrEmpty(theDR["Drug_pk"].ToString()))
                        {
                            drugPk = Convert.ToInt32(theDR["Drug_pk"]);
                        }
                        if (!string.IsNullOrEmpty(theDR["Generic_ID"].ToString()))
                        {
                            genericID = Convert.ToInt32(theDR["Generic_ID"]);
                        }
                        if (!string.IsNullOrEmpty(theDR["Batch_ID"].ToString()))
                        {
                            batchID = Convert.ToInt32(theDR["Batch_ID"]);
                        }
                        if (!string.IsNullOrEmpty(theDR["NafdacNo"].ToString()))
                        {
                            nafdacNo = Convert.ToString(theDR["NafdacNo"]);
                        }
                        if (!string.IsNullOrEmpty(theDR["IndicationForUse"].ToString()))
                        {
                            indicationForUse = Convert.ToString(theDR["IndicationForUse"]);
                        }
                        if (!string.IsNullOrEmpty(theDR["Dosage"].ToString()))
                        {
                            dosage = Convert.ToString(theDR["Dosage"]);
                        }
                        if (!string.IsNullOrEmpty(theDR["Route"].ToString()))
                        {
                            route = Convert.ToString(theDR["Route"]);
                        }
                        if (!string.IsNullOrEmpty(theDR["DateStarted"].ToString()))
                        {
                            dateStarted = Convert.ToString(theDR["DateStarted"]);
                        }
                        if (!string.IsNullOrEmpty(theDR["DateStopped"].ToString()))
                        {
                            dateStopped = Convert.ToString(theDR["DateStopped"]);
                        }

                        PatientManager.SaveNAFDACData(Ptn_Pk, description, dateReactionStart, dateReactionStop, outcomeReaction, admitDuetoADR, prolongedADR, durationOfAdmission,
                            reactionTreatment, signature, drugPk, suspectedDrug, indicationForUse, dosage, route, dateStarted, dateStopped, concomitantMedicine, reasonForUse,
                            userID, deleteFlag, nafdacNo, genericID, batchID, outcomeReactionDesc);
                    }
                }

                if (theDTCM != null && theDTCM.Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDTCM.Rows)
                    {
                        concomitantMedicine = 1;
                        if (!string.IsNullOrEmpty(theDR["Drug_pk"].ToString()))
                        {
                            drugPk = Convert.ToInt32(theDR["Drug_pk"]);
                        }
                        if (!string.IsNullOrEmpty(theDR["Generic_ID"].ToString()))
                        {
                            genericID = Convert.ToInt32(theDR["Generic_ID"]);
                        }
                        if (!string.IsNullOrEmpty(theDR["Batch_ID"].ToString()))
                        {
                            batchID = Convert.ToInt32(theDR["Batch_ID"]);
                        }
                        if (!string.IsNullOrEmpty(theDR["Dosage"].ToString()))
                        {
                            dosage = Convert.ToString(theDR["Dosage"]);
                        }
                        if (!string.IsNullOrEmpty(theDR["Route"].ToString()))
                        {
                            route = Convert.ToString(theDR["Route"]);
                        }
                        if (!string.IsNullOrEmpty(theDR["DateStarted"].ToString()))
                        {
                            dateStarted = Convert.ToString(theDR["DateStarted"]);
                        }
                        if (!string.IsNullOrEmpty(theDR["DateStopped"].ToString()))
                        {
                            dateStopped = Convert.ToString(theDR["DateStopped"]);
                        }
                        if (!string.IsNullOrEmpty(theDR["ReasonForUse"].ToString()))
                        {
                            reasonForUse = Convert.ToString(theDR["ReasonForUse"]);
                        }
                        PatientManager.SaveNAFDACData(Ptn_Pk, description, dateReactionStart, dateReactionStop, outcomeReaction, admitDuetoADR, prolongedADR, durationOfAdmission,
                            reactionTreatment, signature, drugPk, suspectedDrug, indicationForUse, dosage, route, dateStarted, dateStopped, concomitantMedicine, reasonForUse,
                            userID, deleteFlag, nafdacNo, genericID, batchID, outcomeReactionDesc);
                    }
                }
                ClearSession();
                btnSubmit.Enabled = false;
                SaveCancel();
            }


        }
        catch (Exception ex)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = ex.Message.ToString();
            IQCareMsgBox.Show("C1#", theBuilder, this);
        }
    }
    private void ClearSession()
    {
        Session["SaveFlag"] = null;
        Session["SelectedId"] = null;
        Session["SelectedRow"] = null;
        Session["ReferenceId"] = null;
        Session["RegistrationNo"] = null;
        Session["GridDataADR"] = null;
        Session["GridDataCM"] = null;
    }
    private void SaveCancel()
    {
        string scriptString = "<script language='javascript'> " +
         "window.opener.__doPostBack('Search_OnClick',''); </script>";

        if (!Page.ClientScript.IsClientScriptBlockRegistered(scriptString))
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(),
                                               "script", scriptString);
        }
        string strSession = Session["Ptn_Pk"].ToString();
        IQCareMsgBox.NotifyAction("NAFDAC Information saved successfully.Do you want to close?", "NAFDAC", false, this, "window.location.href='frmPatient_History.aspx?sts=" + 0 + "';");
        string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
        //script += "var ans;\n";
        //script += "ans=window.confirm('Allergy Information saved successfully. Do you want to close?');\n";
        //script += "if (ans==true)\n";
        //script += "{\n";
        //if (Request.QueryString["opento"] == "popup")
        //{
        //    script += "self.close();\n";
        //}
        //else
        //{
        //    script += "window.location.href='frmPatient_Home.aspx';\n";
        //}
        //script += "}\n";
        //script += "else \n";
        //script += "{\n";
        if (Request.QueryString["opento"] == "popup")
        {

            script += "self.close();\n";
        }

        //else
        //{
        //    script += "window.location.href('../ClinicalForms/frmAllergy.aspx?name=Edit');\n";
        //}

        //script += "}\n";
        script += "</script>\n";
        ClientScript.RegisterStartupScript(typeof(string), "confirm", script);



        //if (Request.QueryString["opento"] == "ArtForm")
        //{
        //    string script;
        //    script = "";
        //    script = "<script language = 'javascript' defer ='defer' id = 'closeself_00'>\n";
        //    script += "self.close();\n";
        //    script += "</script>\n";
        //    RegisterStartupScript("closeself_00", script);
        //    return;
        //}


    }
    private void MessageBox(string msg)
    {
        Page.Controls.Add(new LiteralControl("<script language='javascript'> window.alert('" + msg + "')</script>"));
    }

    public class Drugs
    {
        protected int _DrugId;
        public int DrugId
        {
            get { return _DrugId; }
            set { _DrugId = value; }
        }

        protected string _drugName;
        public string DrugName
        {
            get { return _drugName; }
            set { _drugName = value; }
        }

        protected string _manu;
        public string Manufacturer
        {
            get { return _manu; }
            set { _manu = value; }
        }

        protected string _gene;
        public string GenericName
        {
            get { return _gene; }
            set { _gene = value; }
        }

        protected string _batch;
        public string BatchNo
        {
            get { return _batch; }
            set { _batch = value; }
        }

        protected DateTime? _expdt;
        public DateTime? ExpiryDate
        {
            get { return _expdt; }
            set { _expdt = value; }
        }

    }

}







