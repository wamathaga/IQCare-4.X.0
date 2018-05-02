using System;
using System.Data;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Text;
using System.Web.Security; 
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.VisualBasic;
using Interface.Clinical;
using Interface.Security;
using Application.Common;
using Interface.Administration;
using Application.Presentation; 
using Interface.Pharmacy;
using System.Collections.Generic;
using AjaxControlToolkit;
using System.Web.Script.Serialization;

public partial class ClinicalForms_frmAllergy : LogPage
{
    IAllergyInfo PatientManager;
    AuthenticationManager Authentiaction = new AuthenticationManager();
    string PMTCTNos = "";
    string ARTNos = "";
    DataSet theDSDrug;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["PatientId"] != null)
        {
            //Session["lblpntstatus"] = Convert.ToInt32((Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text);
        }
        if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
        {
            IQCareMsgBox.Show("SessionExpired", this);
            Response.Redirect("~/frmlogin.aspx");
        }

        if (Session["PatientStatus"] != null)
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text = Session["PatientStatus"].ToString();

        //(Master.FindControl("levelTwoNavigationUserControl1").FindControl("PanelPatiInfo") as Panel).Visible = false;
        //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Visible = false;
        //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Allergy Information";
        BindHeader();
        if (Authentiaction.HasFunctionRight(ApplicationAccess.Allergy, FunctionAccess.Print, (DataTable)Session["UserRight"]) == false)
        {
            btnPrint.Enabled = false;
        }
        if (!IsPostBack)
        {
            if (Request.QueryString["name"] == "Add")
            {                
                Session["PtnRedirect"] = Convert.ToInt32(Session["PatientId"]);
                Boolean blnRightfind = false;
                if (Authentiaction.HasFunctionRight(ApplicationAccess.Allergy, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
                {
                    string theUrl = string.Empty;
                    theUrl = string.Format("../ClinicalForms/frmPatient_Home.aspx");
                    Response.Redirect(theUrl);
                }
                else if (Authentiaction.HasFunctionRight(ApplicationAccess.Allergy, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
                {
                    blnRightfind = true;
                    btnSubmit.Enabled = false;
                    btnadd.Enabled = false;
                }
                else if (Authentiaction.HasFunctionRight(ApplicationAccess.Allergy, FunctionAccess.Update, (DataTable)Session["UserRight"]) == false)
                {
                    if (!blnRightfind)
                    {
                        btnSubmit.Enabled = false;
                        btnadd.Enabled = false;
                    }
                }
            }
            Session["PtnRedirect"] = Convert.ToInt32(Session["PatientId"]);

            if (Request.QueryString["RefId"] == null)
            {
                Session["SaveFlag"] = "Add";
                Session["SelectedId"] = "";
                Session["SelectedRow"] = -1;
                Session["RemoveFlag"] = "False";
                Session["Ptn_Pk"] = Convert.ToInt32(Session["PatientId"]);
                if (Session["PtnRedirect"] == null)
                {                   
                    Session["PtnRedirect"] = Convert.ToInt32(Session["PatientId"]);
                }
                if (Request.QueryString["strfy"] != null)
                {
                    Session["CEForm"] = Convert.ToInt32(Request.QueryString["strfy"]);                  
                    Session["CEPtnPk"] = Convert.ToInt32(Session["PatientId"]);
                }
                Session["ReferenceId"] = "";
                Session["RegistrationNo"] = "";
                FillDropDowns();
                GetAllData();
            }
            else
            {
                FillDropDowns();
            }
        }
        DataTable dtPatientInfo = (DataTable)Session["PatientInformation"];
        if (dtPatientInfo != null)
        {
            if (Session["SystemId"].ToString() == "1")
            {
                lblname.Text = dtPatientInfo.Rows[0]["LastName"].ToString() + ", " + dtPatientInfo.Rows[0]["FirstName"].ToString();
                lblpatientnamepmtct.Text = dtPatientInfo.Rows[0]["LastName"].ToString() + ", " + dtPatientInfo.Rows[0]["FirstName"].ToString();
            }
            else
            {
                lblname.Text = dtPatientInfo.Rows[0]["LastName"].ToString() + ", " + dtPatientInfo.Rows[0]["MiddleName"].ToString() + " , " + dtPatientInfo.Rows[0]["FirstName"].ToString();
                lblpatientnamepmtct.Text = dtPatientInfo.Rows[0]["LastName"].ToString() + ", " + dtPatientInfo.Rows[0]["MiddleName"].ToString() + " , " + dtPatientInfo.Rows[0]["FirstName"].ToString();

            }
            lblIQnumber.Text = dtPatientInfo.Rows[0]["IQNumber"].ToString();
            lblIQnumberpmtct.Text = dtPatientInfo.Rows[0]["IQNumber"].ToString();
            PMTCTNos = dtPatientInfo.Rows[0]["ANCNumber"].ToString() + dtPatientInfo.Rows[0]["PMTCTNumber"].ToString() + dtPatientInfo.Rows[0]["AdmissionNumber"].ToString() + dtPatientInfo.Rows[0]["OutpatientNumber"].ToString();
            ARTNos = dtPatientInfo.Rows[0]["PatientEnrollmentId"].ToString();
            if (PMTCTNos != null && PMTCTNos != "")
            {
                pmtct.Visible = true;
            }
            else
            {
                pmtct.Visible = false;
            }
            if (ARTNos == "")
            {
                divART.Visible = false;
            }
            else
            { 
                divART.Visible = true;
            }
            if (PMTCTNos != "" && ARTNos != "")
            {
                pmtctname.Visible = false;
            }
            if (PMTCTNos == "")
            {
                if (lblIQnumber.Text != "")
                {
                    divART.Visible = true;
                }
            }
        }

        //if (Session["lblpntstatus"].ToString() == "1")
        //{
        //    btnadd.Enabled = false;
        //    btnSubmit.Enabled = false;
        //}
        //else
        //{
        //    btnadd.Enabled = true;
        //}
        //if (Session["CareEndFlag"].ToString() == "1")
        //{
        //    btnadd.Enabled = true;
        //    btnSubmit.Enabled = true;
        //}

        if (Request.QueryString["opento"] == "popup")
        {
            ifPopUp();
        }

        txtAllergyDate.Attributes.Add("onblur", "isCheckValidDate('" + Application["AppCurrentDate"] + "', '" + txtAllergyDate.ClientID + "', '" + txtAllergyDate.ClientID + "');");
    }


    public void ifPopUp()
    {
        Master.FindControl("levelOneNavigationUserControl1").Visible = false;
        Master.FindControl("levelTwoNavigationUserControl1").Visible = false;
        //Master.FindControl("lnkHelp").Visible = false;
        Master.FindControl("lnkPassword").Visible = false;
        Master.FindControl("lnkReportDefect").Visible = false;
        Master.FindControl("lnkLogOut").Visible = false;
        //Master.FindControl("username1").Visible = false;
        //Master.FindControl("currentdate1").Visible = false;
        //Master.FindControl("facilityName").Visible = false;
        //Master.FindControl("imageFlipLevel2").Visible = false;
        //Master.FindControl("lblSeperator1").Visible = false;
        //Master.FindControl("lblSeperator2").Visible = false;
        //Master.FindControl("lblSeperator3").Visible = false;
        lblFormName.Visible = false;
        //Updatepanel.FindControl("patientInfoTop").Visible = false;
        btnBack.Visible = false;
        lblBtnBack.Visible = false;
    }
    /// <summary>
    /// Validation 
    /// </summary>
    /// <returns></returns>
    private Boolean FieldValidation()
    {
        IIQCareSystem IQCareSecurity;
        IQCareSecurity = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
        DateTime theCurrentDate = (DateTime)IQCareSecurity.SystemDate();
        IQCareUtils theUtils = new IQCareUtils();
        PatientManager = (IAllergyInfo)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BAllergyInfo, BusinessProcess.Clinical");
        if (ddlAllergyType.SelectedIndex == 0)
        {
            MsgBuilder theMsg = new MsgBuilder();
            theMsg.DataElements["Control"] = "Allergy Type";
            IQCareMsgBox.Show("BlankDropDown", theMsg, this);
            return false;
        }
        if (ddlAllergyType.SelectedItem.Value == "207")
        {
            if (txtAllergen.Text == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Allergen";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtAllergen.Focus();
                return false;
            }
        }
        else if (ddlAllergyType.SelectedItem.Value == "211")
        {
            if (txtAllergenOther.Text == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Allergen";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtAllergenOther.Focus();
                return false;
            }
        }
        else
        {
            if (ddlAllergen.SelectedIndex == 0)
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["Control"] = "Allergen";
                IQCareMsgBox.Show("BlankDropDown", theMsg, this);
                return false;
            }
        }
        return true;
    }
    /// <summary>
    /// Add/Edit field data to gridview
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd(object sender, EventArgs e)
    {
        try
        {
            if (Authentiaction.HasFunctionRight(ApplicationAccess.Allergy, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
            {
                btnSubmit.Enabled = false;
                btnadd.Enabled = false;
            }
            if (FieldValidation())
            {
                DataRow[] foundRows;
                grdAllergy.Visible = true;
                DataTable theDT = new DataTable();
                theDT = (DataTable)Session["GridData"];
                if (txtAllergen.Text != "")
                {
                    foundRows = theDT.Select("AllergyTypeDesc='" + ddlAllergyType.SelectedItem.Text + "' and AllergenDesc='" + txtAllergen.Text + "'");
                }
                else if (txtAllergenOther.Text != "")
                {
                    foundRows = theDT.Select("AllergyTypeDesc='" + ddlAllergyType.SelectedItem.Text + "' and AllergenDesc='" + txtAllergenOther.Text + "'");
                }
                else
                {
                    foundRows = theDT.Select("AllergyTypeDesc='" + ddlAllergyType.SelectedItem.Text + "' and AllergenDesc='" + ddlAllergen.SelectedItem.Text + "'");
                }
                if (Session["SaveFlag"] == null)
                {
                    Session["SaveFlag"] = "Add";
                }
                if (Session["SaveFlag"].ToString() == "Add")
                {
                    if (foundRows.Length < 1)
                    {
                        //Add mode - a new member is be added and he is not already in the grid
                        theDT = (DataTable)Session["GridData"];
                        DataRow theDR = theDT.NewRow();
                        theDR["Ptn_Pk"] = Session["Ptn_Pk"];
                        theDR["AllergyTypeID"] = ddlAllergyType.SelectedItem.Value;
                        if (ddlAllergyType.SelectedIndex > 0)
                        {
                            theDR["AllergyTypeDesc"] = ddlAllergyType.SelectedItem.Text;
                        }
                        if (ddlAllergyType.SelectedItem.Value == "207")
                        {
                            theDR["AllergenTypeID"] = txtAllergen.Text;
                            theDR["AllergenDesc"] = txtAllergen.Text;
                        }
                        else if (ddlAllergyType.SelectedItem.Value == "211")
                        {
                            theDR["AllergenTypeID"] = txtAllergenOther.Text;
                            theDR["AllergenDesc"] = txtAllergenOther.Text;
                        }
                        else
                        {
                            theDR["AllergenTypeID"] = ddlAllergen.SelectedItem.Value;
                            theDR["AllergenDesc"] = ddlAllergen.SelectedItem.Text;

                            if (ddlAllergen.SelectedItem.Text.ToLower() == "other")
                            {
                                if (txtOtherAllegian.Text != "")
                                {
                                    theDR["otherAllergen"] = txtOtherAllegian.Text;
                                }
                            }
                            else
                            {
                                theDR["otherAllergen"] = "";
                            }
                        }

                        theDR["typeReaction"] = txtTypeOfReaction.Text;
                        theDR["SevrityTypeID"] = ddlSeverity.SelectedItem.Value;
                        if (ddlSeverity.SelectedIndex > 0)
                        {
                            theDR["severityDesc"] = ddlSeverity.SelectedItem.Text;
                        }
                        if (txtAllergyDate.Value != "")
                        {
                            theDR["dateAllergy"] = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(txtAllergyDate.Value));
                        }
                        else
                        {
                            theDR["dateAllergy"] = "";
                        }
                        theDT.Rows.Add(theDR);
                        Session["GridData"] = theDT;
                        grdAllergy.Columns.Clear();
                        grdAllergy.DataSource = (DataTable)Session["GridData"];
                    }
                    else
                    {
                        IQCareMsgBox.Show("AllergyMemberExists", this);
                        return;
                    }
                }
                else if (Session["SaveFlag"].ToString() == "Edit")
                {
                    //Edit mode- ie member is selected from grid
                    int r = Convert.ToInt32(Session["SelectedRow"]);
                    theDT.Rows[r]["AllergyTypeID"] = ddlAllergyType.SelectedItem.Value;
                    if (ddlAllergyType.SelectedIndex > 0)
                    {
                        theDT.Rows[r]["AllergyTypeDesc"] = ddlAllergyType.SelectedItem.Text;
                    }
                    if (ddlAllergyType.SelectedItem.Value == "207")
                    {
                        theDT.Rows[r]["AllergenTypeID"] = txtAllergen.Text;
                        theDT.Rows[r]["AllergenDesc"] = txtAllergen.Text;
                    }
                    else if (ddlAllergyType.SelectedItem.Value == "211")
                    {
                        theDT.Rows[r]["AllergenTypeID"] = txtAllergenOther.Text;
                        theDT.Rows[r]["AllergenDesc"] = txtAllergenOther.Text;
                    }
                    else
                    {
                        theDT.Rows[r]["AllergenTypeID"] = ddlAllergen.SelectedItem.Value;
                        theDT.Rows[r]["AllergenDesc"] = ddlAllergen.SelectedItem.Text;

                        if (ddlAllergen.SelectedItem.Text.ToLower() == "other")
                        {
                            if (txtOtherAllegian.Text != "")
                            {
                                theDT.Rows[r]["otherAllergen"] = txtOtherAllegian.Text;
                            }
                            else
                            {
                                theDT.Rows[r]["otherAllergen"] = "";
                            }
                        }
                        else
                        {
                            theDT.Rows[r]["otherAllergen"] = "";
                        }
                    }

                    theDT.Rows[r]["typeReaction"] = txtTypeOfReaction.Text;
                    theDT.Rows[r]["SevrityTypeID"] = ddlSeverity.SelectedItem.Value;
                    if (ddlSeverity.SelectedIndex > 0)
                    {
                        theDT.Rows[r]["severityDesc"] = ddlSeverity.SelectedItem.Text;
                    }
                    if (txtAllergyDate.Value != "")
                    {
                        theDT.Rows[r]["dateAllergy"] = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(txtAllergyDate.Value));
                    }
                    else
                    {
                        theDT.Rows[r]["dateAllergy"] = "";
                    }
                    Session["GridData"] = theDT;
                    grdAllergy.Columns.Clear();
                    grdAllergy.DataSource = (DataTable)Session["GridData"];
                }
                //if (((DataTable)Session["GridData"]).Rows.Count == 0)
                //    btnSubmit.Enabled = false;
                //else
                //    btnSubmit.Enabled = true;
                BindGrid();
                Refresh();
                btnadd.Text = "Add Allergy";
                //btnSubmit.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = ex.Message.ToString();
            IQCareMsgBox.Show("C1#", theBuilder, this);
        }

    }
    /// <summary>
    ///  Bind Dropdown 
    /// </summary>
    private void FillDropDowns()
    {
        BindFunctions BindManager = new BindFunctions();
        IQCareUtils theUtils = new IQCareUtils();
        DataTable theDT = new DataTable();
        DataSet theDSXML = new DataSet();
        theDSXML.ReadXml(MapPath("..\\XMLFiles\\AllMasters.con"));
        DataView theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
        theDV.RowFilter = "DeleteFlag=0 and CodeID=210 and SystemId in(0,1)";
        if (theDV.Table != null)
        {
            theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            BindManager.BindCombo(ddlSeverity, theDT, "Name", "ID");
            theDV.Dispose();
            theDT.Clear();
        }

        theDV = new DataView(theDSXML.Tables["Mst_Code"]);
        theDV.RowFilter = "DeleteFlag=0 and CodeID in (207,208,209,211)";
        if (theDV.Table != null)
        {
            theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            BindManager.BindCombo(ddlAllergyType, theDT, "Name", "CodeID");
            theDV.Dispose();
            theDT.Clear();
        }
    }
    private void BindHeader()
    {
        DataSet theFacilityDS = new DataSet();
        AuthenticationManager Authentication = new AuthenticationManager();
        IFacilitySetup FacilityMaster = (IFacilitySetup)ObjectFactory.CreateInstance("BusinessProcess.Administration.BFacility, BusinessProcess.Administration");
        if (Session["SystemId"].ToString() == "1")
        {
            theFacilityDS = FacilityMaster.GetSystemBasedLabels(Convert.ToInt32(Session["SystemId"]), ApplicationAccess.Allergy, 0);
        }
        else if (Session["SystemId"].ToString() == "2")
        {
            theFacilityDS = FacilityMaster.GetSystemBasedLabels(Convert.ToInt32(Session["SystemId"]), ApplicationAccess.Allergy, 0);
        }
        ViewState["grdDataSource"] = theFacilityDS.Tables[0];
    }
    /// <summary>
    /// Bind Grid Data
    /// </summary>
    private void BindGrid()
    {
        BoundField theCol0 = new BoundField();
        theCol0.HeaderText = "Id";
        theCol0.DataField = "Id";
        theCol0.ItemStyle.CssClass = "textstyle";
        grdAllergy.Columns.Add(theCol0);

        BoundField theCol1 = new BoundField();
        theCol1.HeaderText = "Patientid";
        theCol1.DataField = "ptn_pk";
        theCol1.ItemStyle.CssClass = "textstyle";
        grdAllergy.Columns.Add(theCol1);

        BoundField theCol2 = new BoundField();
        theCol2.HeaderText = "AllergyTypeID";
        theCol2.DataField = "AllergyTypeID";
        theCol2.ItemStyle.CssClass = "textstyle";
        grdAllergy.Columns.Add(theCol2);

        BoundField theCol3 = new BoundField();
        theCol3.HeaderText = "Allergy Type";
        theCol3.DataField = "AllergyTypeDesc";        
        theCol3.ItemStyle.CssClass = "textstyle";
        theCol3.ReadOnly = true;
        grdAllergy.Columns.Add(theCol3);

        BoundField theCol4 = new BoundField();
        theCol4.HeaderText = "AllergenTypeID";
        theCol4.DataField = "AllergenTypeID";
        theCol4.ItemStyle.CssClass = "textstyle";
        grdAllergy.Columns.Add(theCol4);

        BoundField theCol5 = new BoundField();
        theCol5.HeaderText = "Allergen";
        theCol5.DataField = "AllergenDesc";
        theCol5.ItemStyle.CssClass = "textstyle";
        theCol5.ReadOnly = true;
        grdAllergy.Columns.Add(theCol5);


        BoundField theCol6 = new BoundField();
        theCol6.HeaderText = "Other Allergen";
        theCol6.ItemStyle.CssClass = "textstyle";
        theCol6.DataField = "otherAllergen";
        theCol6.ReadOnly = true;
        theCol6.ItemStyle.Width = 200;
        grdAllergy.Columns.Add(theCol6);

        BoundField theCol7 = new BoundField();
        theCol7.HeaderText = "Reaction Type";
        theCol7.ItemStyle.CssClass = "textstyle";
        theCol7.DataField = "typeReaction";
        theCol7.ReadOnly = true;
        theCol7.ItemStyle.Width = 200;
        grdAllergy.Columns.Add(theCol7);

        BoundField theCol8 = new BoundField(); // double
        theCol8.HeaderText = "SevrityTypeID";
        theCol8.DataField = "SevrityTypeID";
        theCol8.ItemStyle.CssClass = "textstyle";
        grdAllergy.Columns.Add(theCol8);

        BoundField theCol9 = new BoundField();
        theCol9.HeaderText = "Severity";
        theCol9.ItemStyle.CssClass = "textstyle";
        theCol9.DataField = "severityDesc";
        theCol9.ReadOnly = true;
        grdAllergy.Columns.Add(theCol9);

        BoundField theCol10 = new BoundField(); // double
        theCol10.HeaderText = "Date Allergy";
        theCol10.DataField = "dateAllergy";
        theCol10.ItemStyle.CssClass = "textstyle";
        theCol10.DataFormatString = "{0:dd-MMM-yyyy}";
        grdAllergy.Columns.Add(theCol10);

        CommandField objfield = new CommandField();
        objfield.ButtonType = ButtonType.Link;
        objfield.DeleteText = "<img src='../Images/del.gif' alt='Delete this' border='0' />";
        objfield.ShowDeleteButton = true;
        grdAllergy.Columns.Add(objfield);

        grdAllergy.DataBind();
        grdAllergy.Columns[0].Visible = false;
        grdAllergy.Columns[1].Visible = false; 
        grdAllergy.Columns[2].Visible = false;
        grdAllergy.Columns[4].Visible = false;
        grdAllergy.Columns[8].Visible = false;
    }
    private void GetAllData()
    {
        if (Request.QueryString["back"] != null)
        {
            //if (((DataTable)Session["GridData"]).Rows.Count == 0)
            //    btnSubmit.Enabled = false;
            //else
            //    btnSubmit.Enabled = true;
            grdAllergy.DataSource = (DataTable)Session["GridData"];
            grdAllergy.DataBind();
            BindGrid();
        }
        else
        {

            PatientManager = (IAllergyInfo)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BAllergyInfo, BusinessProcess.Clinical");
            if (Session["Ptn_Pk"].ToString() != "0")
            {
                DataSet theDS = PatientManager.GetAllAllergyData(Convert.ToInt32(Session["Ptn_Pk"]));
                //if (theDS.Tables[0].Rows.Count > 0)
                //{
                //    btnSubmit.Enabled = true;
                //}
                //else
                //{
                //    btnSubmit.Enabled = false;
                //}
                Session["GridData"] = theDS.Tables[0];
                grdAllergy.DataSource = (DataTable)Session["GridData"];
                BindGrid();
            }
        }
    }

    private void SearchFamilyInfo()
    {
      
        PatientManager = (IAllergyInfo)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BAllergyInfo, BusinessProcess.Clinical");
        Session["ReferenceId"] = Convert.ToInt32(Request.QueryString["RefId"]);
        //---- check whether the patient is twice selected OR the patient is selecting himself as family member
        if (Convert.ToInt32(Session["ReferenceId"]) == Convert.ToInt32(Session["Ptn_Pk"])) // selecting himself
        {
            grdAllergy.DataSource = (DataTable)Session["GridData"];
            grdAllergy.DataBind();
            IQCareMsgBox.Show("SelectHimself", this);          
        }
        foreach (DataRow theDR in ((DataTable)Session["GridData"]).Rows)
        {
            if (theDR["ReferenceId"] != DBNull.Value)
            {
                if (Convert.ToInt32(Session["ReferenceId"]) == Convert.ToInt32(theDR["ReferenceId"])) // patient already selected
                {
                    grdAllergy.DataSource = (DataTable)Session["GridData"];
                    grdAllergy.DataBind();
                    IQCareMsgBox.Show("DuplicateSelect", this);                
                    break;
                }
            }
        }        
        grdAllergy.DataSource = (DataTable)Session["GridData"];
        BindGrid();
    }

    protected void grdAllergy_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        if (Authentiaction.HasFunctionRight(ApplicationAccess.Allergy, FunctionAccess.Update, (DataTable)Session["UserRight"]) == true)
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
        theDT = ((DataTable)Session["GridData"]);
        int r = theIndex;
        // Fill data in Textboxes from grid
        //Edit the selected row
        if (theDT.Rows.Count > 0)
        {
            txtAllergen.Visible = false;
            txtAllergen.Text = "";
            txtAllergenOther.Visible = false;
            txtAllergenOther.Text = "";
            ddlAllergen.Visible = false;
            if (theDT.Rows[r].IsNull("AllergyTypeID") != true && theDT.Rows[r].IsNull("AllergyTypeID").ToString() != string.Empty)
            {
                ddlAllergyType.SelectedValue = theDT.Rows[r]["AllergyTypeID"].ToString();
            }
            else
            {
                ddlAllergyType.SelectedValue = "0";
            }
            if (ddlAllergyType.SelectedValue == "207")
            {
                txtAllergen.Text = theDT.Rows[r]["AllergenTypeID"].ToString();
                GetPediatricFields(Convert.ToInt32(Session["PatientId"]));
                txtAllergen.Visible = true;
            }
            else if (ddlAllergyType.SelectedValue == "211")
            {
                txtAllergenOther.Text = theDT.Rows[r]["AllergenTypeID"].ToString();
                txtAllergenOther.Visible = true;
            }
            else
            {
                txtAllergenOther.Visible = false;
                txtAllergen.Visible = false;
                ddlAllergen.Visible = true;
                fillSubDdlData();
                if (theDT.Rows[r].IsNull("AllergenTypeID") != true && theDT.Rows[r].IsNull("AllergenTypeID").ToString() != string.Empty)
                {
                    ddlAllergen.SelectedValue = theDT.Rows[r]["AllergenTypeID"].ToString();
                    if (ddlAllergen.SelectedItem.Text.ToLower() == "other")
                    {
                        lblOtherALlegian.Visible = true;
                        txtOtherAllegian.Visible = true;
                    }
                }
                else
                {
                    ddlAllergen.SelectedValue = "0";
                }
            }

            txtOtherAllegian.Text = theDT.Rows[r]["OtherAllergen"].ToString();
            if (txtOtherAllegian.Text != "")
            {
                lblOtherALlegian.Visible = true;
                txtOtherAllegian.Visible = true;
            }
            txtTypeOfReaction.Text = theDT.Rows[r]["TypeReaction"].ToString();
            if (theDT.Rows[r].IsNull("SevrityTypeID") != true && theDT.Rows[r].IsNull("SevrityTypeID").ToString() != string.Empty)
            {
                ddlSeverity.SelectedValue = theDT.Rows[r]["SevrityTypeID"].ToString();
            }
            else
            {
                ddlSeverity.SelectedValue = "0";
            }
            if (theDT.Rows[r]["DateAllergy"].ToString() != "")
            {
                txtAllergyDate.Value = String.Format("{0:dd-MMM-yyyy}", theDT.Rows[r]["DateAllergy"].ToString());
            }
            else
            {
                txtAllergyDate.Value = "";
            }
            Session["SelectedRow"] = theIndex;
            Session["SaveFlag"] = "Edit";
            btnadd.Text = "Update Allergy";
        }
    }
    /// <summary>
    /// Refresh Field
    /// </summary>
    private void Refresh()
    {
        Session["SaveFlag"] = "Add";      
        ddlAllergyType.SelectedIndex = -1;
        ddlAllergen.SelectedIndex = -1;
        ddlAllergen.Visible = false;
        txtAllergen.Visible = false;
        txtAllergen.Text = "";
        txtAllergenOther.Visible = true;
        txtAllergenOther.Text = "";
        lblOtherALlegian.Visible = false;
        txtOtherAllegian.Visible = false;
        txtOtherAllegian.Text = "";
        ddlSeverity.SelectedIndex = -1;
        txtAllergyDate.Value = "";
        txtTypeOfReaction.Text = "";
    }
    protected void grdAllergy_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //So that when the user clicks on the row - the corresponding row is edited
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < 11; i++)
            {
                if (e.Row.Cells[0].Text.ToString() != "0")
                {
                    if (Session["lblpntstatus"] != null)
                    {
                        if (Authentiaction.HasFunctionRight(ApplicationAccess.Allergy, FunctionAccess.Update, (DataTable)Session["UserRight"]) == true && Session["lblpntstatus"].ToString() != "1")
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
                    e.Row.Cells[11].Visible = false;
                }
            }
            if (Session["lblpntstatus"] != null)
            {
                if (Authentiaction.HasFunctionRight(ApplicationAccess.Allergy, FunctionAccess.Delete, (DataTable)Session["UserRight"]) == true && Session["lblpntstatus"].ToString() != "1")
                {
                    LinkButton objlink = (LinkButton)e.Row.Cells[11].Controls[0];
                    objlink.OnClientClick = "if(!confirm('Are you sure you want to delete this?')) return false;";
                    e.Row.Cells[11].ID = e.Row.RowIndex.ToString();
                }
            }
            if (Session["CareEndFlag"].ToString() == "1")
            {
                e.Row.Cells[11].Visible = false;
            }
        }
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
            int Id, Ptn_Pk = 0, UserID, DeleteFlag;
            string AllergyType, Allergen, otherAllergen, typeReaction, severity, dataAllergy;
            PatientManager = (IAllergyInfo)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BAllergyInfo, BusinessProcess.Clinical");
            DataTable theDT = (DataTable)Session["GridData"];
            foreach (DataRow theDR in theDT.Rows)
            {
                if (theDR["Id"] == DBNull.Value)
                    Id = -1;
                else
                    Id = Convert.ToInt32(theDR["Id"]);
                Ptn_Pk = Convert.ToInt32(Session["Ptn_Pk"]);
                if (!string.IsNullOrEmpty(theDR["AllergyTypeID"].ToString()))
                {
                    AllergyType = Convert.ToString(theDR["AllergyTypeID"]);
                }
                else
                {
                    AllergyType = "0";
                }
                if (theDR["AllergyTypeID"].ToString() != "207")
                {
                    Allergen = theDR["AllergenTypeID"].ToString();
                }
                else if (theDR["AllergyTypeID"].ToString() != "211")
                {
                    Allergen = theDR["AllergenTypeID"].ToString();
                }
                else
                {
                    Allergen = theDR["Allergen"].ToString();
                }
                if (theDR["otherAllergen"].ToString() != "")
                {
                    otherAllergen = theDR["otherAllergen"].ToString();
                }
                else
                {
                    otherAllergen = "";
                }
                typeReaction = theDR["typeReaction"].ToString();
                if (!string.IsNullOrEmpty(theDR["SevrityTypeID"].ToString()))
                {
                    severity = Convert.ToString(theDR["SevrityTypeID"]);
                }
                else
                {
                    severity = "0";
                }
                UserID = Convert.ToInt32(Session["AppUserId"]);
                DeleteFlag = 0;
                dataAllergy = theDR["dateAllergy"].ToString();
                if (dataAllergy == "")
                {
                    dataAllergy = "";
                }
                PatientManager.SaveAllergyInfo(Id, Ptn_Pk, AllergyType, Allergen, otherAllergen, severity, typeReaction, UserID, DeleteFlag, Convert.ToString(dataAllergy));
            }
            ClearSession();
            btnSubmit.Enabled = false;
            SaveCancel();
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
        IQCareMsgBox.NotifyAction("Allergy Information saved successfully.Do you want to close?", "Allergy Information", false, this, "window.location.href='frmPatient_History.aspx?sts=" + 0 + "';");
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
        RegisterStartupScript("confirm", script);



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

    

    protected void grdAllergy_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {           
        System.Data.DataTable theDT = new System.Data.DataTable();
        theDT = ((DataTable)Session["GridData"]);
        int r = Convert.ToInt32(e.RowIndex.ToString());
        int Id = -1;
        try
        {
            if (theDT.Rows.Count > 0)
            {
                if (theDT.Rows[r].HasErrors == false)
                {
                    if ((theDT.Rows[r]["Id"] != null) && (theDT.Rows[r]["Id"] != DBNull.Value))
                    {
                        if (theDT.Rows[r]["Id"].ToString() != "")
                        {
                            Id = Convert.ToInt32(theDT.Rows[r]["Id"]);
                            PatientManager = (IAllergyInfo)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BAllergyInfo, BusinessProcess.Clinical");
                            PatientManager.DeleteAllergyInfo(Id, Convert.ToInt32(Session["AppUserId"]));
                        }
                    }
                }
                theDT.Rows[r].Delete();
                theDT.AcceptChanges();
                Session["GridData"] = theDT;
                grdAllergy.Columns.Clear();
                grdAllergy.DataSource = (DataTable)Session["GridData"];
                BindGrid();               
                IQCareMsgBox.Show("DeleteSuccess", this);
                Refresh();
                //if (((DataTable)Session["GridData"]).Rows.Count == 0)
                //    btnSubmit.Enabled = false;
                //else
                //    btnSubmit.Enabled = true;
                Refresh();
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

    private void MessageBox(string msg)
    {
        Page.Controls.Add(new LiteralControl("<script language='javascript'> window.alert('" + msg + "')</script>"));
    }

    protected void ddlAllergyType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblOtherALlegian.Visible = false;
        txtOtherAllegian.Visible = false;
        txtOtherAllegian.Text = "";
        ddlAllergen.SelectedIndex = -1;
        ddlAllergen.Visible = false;
        txtAllergen.Visible = false;
        txtAllergen.Text = "";
        txtAllergenOther.Visible = false;
        txtAllergenOther.Text = "";
        fillSubDdlData();
    }
    /// <summary>
    ///  Bind Dropdown Data
    /// </summary>
    private void fillSubDdlData()
    {
        BindFunctions BindManager = new BindFunctions();
        IQCareUtils theUtils = new IQCareUtils();
        DataTable theDT = new DataTable();
        DataSet theDSXML = new DataSet();
        theDSXML.ReadXml(MapPath("..\\XMLFiles\\AllMasters.con"));
        DataView theDV;
        txtAllergenOther.Visible = true;
        txtAllergenOther.Text = "";
        if (ddlAllergyType.SelectedItem.Value != "207" && ddlAllergyType.SelectedItem.Value != "211" && ddlAllergyType.SelectedItem.Text != "Select")
        {
            ddlAllergen.Visible = true;
            txtAllergen.Visible = false;
            txtAllergen.Text = "";
            txtAllergenOther.Visible = false;
            txtAllergenOther.Text = "";
            if (ddlAllergyType.SelectedItem.Value == "208")
            {
                theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                theDV.RowFilter = "DeleteFlag=0 and CodeID=208";
                theDV.Sort = "Code ASC";
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(ddlAllergen, theDT, "Name", "ID");
                    theDV.Dispose();
                    theDT.Clear();
                }
            }
            else if (ddlAllergyType.SelectedItem.Value == "209")
            {
                theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                theDV.RowFilter = "DeleteFlag=0 and CodeID=209";
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(ddlAllergen, theDT, "Name", "ID");
                    theDV.Dispose();
                    theDT.Clear();
                }
            }
        }
        else if (ddlAllergyType.SelectedItem.Value == "207")
        {
            txtAllergen.Visible = true;
            txtAllergen.Text = "";
            txtAllergenOther.Visible = false;
            txtAllergenOther.Text = "";
            GetPediatricFields(Convert.ToInt32(Session["PatientId"]));
        }
    }
    protected void ddlAllergen_SelectedIndexChanged(object sender, EventArgs e)
    {       
        lblOtherALlegian.Visible = false;
        txtOtherAllegian.Visible = false;
        if (ddlAllergen.SelectedItem.Text.ToLower() == "other")
        {
            lblOtherALlegian.Visible = true;
            txtOtherAllegian.Visible = true;
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
                if (Session["AddARV"] == null)
                    {
                        DataTable theDT = new DataTable();
                        theDT.Columns.Add("DrugId", System.Type.GetType("System.Int32"));
                        theDT.Columns.Add("DrugName", System.Type.GetType("System.String"));
                        theDT.Columns.Add("Generic", System.Type.GetType("System.Int32"));
                        theDT.Columns.Add("DrugTypeId", System.Type.GetType("System.Int32"));
                        Session["AddARV"] = theDT;
                    }
                    DataTable ExistDT = (DataTable)Session["AddARV"];
                    theExistsDV = new DataView(ExistDT);
                    theExistsDV.RowFilter = "DrugId =" + theAutoDT.Rows[0]["Drug_pk"];
                    DataTable theSelExistsDT = (DataTable)theUtils.CreateTableFromDataView(theExistsDV);
                    if (theSelExistsDT.Rows.Count == 0)
                    {
                        DataRow DR = ExistDT.NewRow();
                        DR[0] = theAutoDT.Rows[0]["Drug_pk"];
                        DR[1] = theAutoDT.Rows[0]["DrugName"];
                        DR[2] = 0;
                        DR[3] = theAutoDT.Rows[0]["DrugTypeId"];
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
    public class Drugs
    {
        protected int _DrugId;
        public int DrugId
        {
            get { return _DrugId; }
            set { _DrugId = value; }
        }
        protected int _avlqty;
        public int AvlQty
        {
            get { return _avlqty; }
            set { _avlqty = value; }
        }
        protected string _drugName;
        public string DrugName
        {
            get { return _drugName; }
            set { _drugName = value; }
        }
    }
    protected void GetPediatricFields(int PatientID)
    {
        IPediatric PediatricManager;
        try
        {
            PediatricManager = (IPediatric)ObjectFactory.CreateInstance("BusinessProcess.Pharmacy.BPediatric, BusinessProcess.Pharmacy");
            DataSet theDrugDS = (DataSet)PediatricManager.GetDrugGenericDetails(PatientID);
            theDSDrug = new DataSet();
            theDSDrug.Tables.Add(theDrugDS.Tables[0].Copy());//--0--// performance generic abbrv
            ViewState["MasterData"] = theDSDrug;
        }
        catch (Exception er)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = er.Message.ToString();
            IQCareMsgBox.Show("C1#", theBuilder, this);
        }
    }

    public static List<Drugs> GetDrugs(string prefixText, int count)
    {
        List<Drugs> items = new List<Drugs>();
        IDrug objRptFields;
        objRptFields = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.Pharmacy.BDrug,BusinessProcess.Pharmacy");
        string sqlQuery;
        sqlQuery = string.Format("Select GenericID[drug_pk], GenericName[drugname] from mst_generic  WHERE DeleteFlag=0 and GenericName LIKE '%{0}%'", prefixText);
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
                    items.Add(item);
                }
                catch (Exception ex)
                {

                }
            }
        }
        return items;
    }
}



