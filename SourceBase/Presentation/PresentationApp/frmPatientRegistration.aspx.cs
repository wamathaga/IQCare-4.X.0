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
using System.IO;
using Microsoft.VisualBasic;
using Interface.Clinical;
using Interface.Security;
using Application.Common;
using Application.Presentation;
using Interface.Administration;
using Interface.Service;

public partial class frmPatientRegistration : BasePage, IHttpHandler
{
    IPatientRegistration PatientManager;

    #region "Local Variables"
    string ObjFactoryParameter = "BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical";
    Hashtable htParameters;

    #endregion

    private Boolean FieldValidation()
    {
        IIQCareSystem IQCareSecurity;
        IQCareSecurity = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
        DateTime theCurrentDate = (DateTime)IQCareSecurity.SystemDate();
        IQCareUtils theUtils = new IQCareUtils();

        if (txtfirstName.Text.Trim() == "")
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "First Name";
            IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
            txtfirstName.Focus();
            return false;
        }
        else if (txtlastName.Text.Trim() == "")
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "Last Name";
            IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
            txtlastName.Focus();
            return false;
        }

        else if (txtRegDate.Value.Trim() == "")
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "Registration Date";
            IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
            txtlastName.Focus();
            return false;
        }

        DateTime theEnrolDate = Convert.ToDateTime(theUtils.MakeDate(txtRegDate.Value));
        if (theEnrolDate > theCurrentDate)
        {
            IQCareMsgBox.Show("EnrolDate", this);
            return false;
        }

        if (ddgender.SelectedValue.Trim() == "0")
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "Sex";
            IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
            ddgender.Focus();
            return false;
        }

        if (TxtDOB.Value.Trim() == "")
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "DOB";
            IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
            TxtDOB.Focus();
            return false;
        }
        DateTime theDOBDate = Convert.ToDateTime(theUtils.MakeDate(TxtDOB.Value));
        if (theDOBDate > theCurrentDate)
        {
            IQCareMsgBox.Show("DOBDate", this);
            TxtDOB.Focus();
            return false;
        }
        if (theDOBDate > theEnrolDate)
        {
            IQCareMsgBox.Show("DOB_EnrolDate", this);
            return false;
        }
        //if (Request.QueryString["name"] == "Edit" && ViewState["ARTStartDate"] != null)
        if (Convert.ToInt32(Session["PatientId"]) > 0 && ViewState["ARTStartDate"] != null)
        {
            DateTime theARTRegDate = Convert.ToDateTime(ViewState["ARTStartDate"].ToString());
            if (theEnrolDate > theARTRegDate)
            {
                IQCareMsgBox.Show("ARTRegDate", this);
                return false;
            }
        }
        return true;
    }

    private string DataQuality_Msg()
    {
        string strmsg = "Following values are required to complete the data quality check:</br>";

        if (txtfirstName.Text.Trim() == "")
        {
            string scriptFName = "<script language = 'javascript' defer ='defer' id = 'FName_ID'>\n";
            scriptFName += "To_Change_Color('lblPName');\n";
            scriptFName += "To_Change_Color('FName');\n";
            scriptFName += "</script>\n";
            RegisterStartupScript("FName_ID", scriptFName);
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = " -First Name";
            strmsg += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this);
            strmsg += "</br>";

        }
        if (txtlastName.Text.Trim() == "")
        {
            string scriptLName = "<script language = 'javascript' defer ='defer' id = 'LName_ID'>\n";
            scriptLName += "To_Change_Color('lblPName');\n";
            scriptLName += "To_Change_Color('LName');\n";
            scriptLName += "</script>\n";
            RegisterStartupScript("LName_ID", scriptLName);
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = " -Last Name";
            strmsg += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this);
            strmsg += "</br>";

        }
        if (txtRegDate.Value.Trim() == "")
        {
            string scriptEnrolDate = "<script language = 'javascript' defer ='defer' id = 'EnrolDate_ID'>\n";
            scriptEnrolDate += "To_Change_Color('lblenroldate');\n";
            scriptEnrolDate += "</script>\n";
            RegisterStartupScript("EnrolDate_ID", scriptEnrolDate);
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = " - Registration Date";
            strmsg += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this);
            strmsg += "</br>";

        }

        if (ddgender.SelectedValue.Trim() == "0")
        {
            string scriptGender = "<script language = 'javascript' defer ='defer' id = 'Gender_ID'>\n";
            scriptGender += "To_Change_Color('lblgender');\n";
            scriptGender += "</script>\n";
            RegisterStartupScript("Gender_ID", scriptGender);
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = " -Sex";
            strmsg += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this);
            strmsg += "</br>";
        }

        if (TxtDOB.Value.Trim() == "")
        {
            string scriptDOB = "<script language = 'javascript' defer ='defer' id = 'DOB_ID'>\n";
            scriptDOB += "To_Change_Color('lblDOB');\n";
            scriptDOB += "</script>\n";
            RegisterStartupScript("DOB_ID", scriptDOB);
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = " -DOB";
            strmsg += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this);
            strmsg += "</br>";
        }
        if (ddvillageName.SelectedValue == "0")
        {
            string scriptvillageName = "<script language = 'javascript' defer ='defer' id = 'villageName'>\n";
            scriptvillageName += "To_Change_Color('lblvillageName');\n";
            scriptvillageName += "</script>\n";
            RegisterStartupScript("villageName", scriptvillageName);
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = " -Village/Town/City Name";
            strmsg += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this);
            strmsg += "</br>";
        }
        if (dddistrictName.SelectedValue == "0")
        {
            string scriptdistrictName = "<script language = 'javascript' defer ='defer' id = 'districtName'>\n";
            scriptdistrictName += "To_Change_Color('lbldistrictName');\n";
            scriptdistrictName += "</script>\n";
            RegisterStartupScript("districtName", scriptdistrictName);
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = " -District/County/Ward";
            strmsg += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this);
            strmsg += "</br>";
        }
        if (String.IsNullOrEmpty(txtemergContactName.Text.Trim()))
        {
            string scriptemergContactName = "<script language = 'javascript' defer ='defer' id = 'emergContact'>\n";
            scriptemergContactName += "To_Change_Color('lblemergContactName');\n";
            scriptemergContactName += "</script>\n";
            RegisterStartupScript("emergContact", scriptemergContactName);
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = " -Emergency Contact Name";
            strmsg += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this);
            strmsg += "</br>";
        }


        return strmsg;
    }
    private Boolean FieldValidation_DQ()
    {
        IIQCareSystem IQCareSecurity;
        IQCareSecurity = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
        DateTime theCurrentDate = (DateTime)IQCareSecurity.SystemDate();
        IQCareUtils theUtils = new IQCareUtils();

        DateTime theEnrolDate = Convert.ToDateTime(theUtils.MakeDate(txtRegDate.Value));
        if (theEnrolDate > theCurrentDate)
        {
            IQCareMsgBox.Show("EnrolDate", this);
            txtRegDate.Focus();
            return false;
        }

        DateTime theDOBDate = Convert.ToDateTime(theUtils.MakeDate(TxtDOB.Value));
        if (theDOBDate > theCurrentDate)
        {
            IQCareMsgBox.Show("DOBDate", this);
            TxtDOB.Focus();
            return false;
        }
        if (theDOBDate > theEnrolDate)
        {
            IQCareMsgBox.Show("DOB_EnrolDate", this);
            TxtDOB.Focus();
            return false;
        }

        if (ddvillageName.SelectedValue == "0")
        {
            IQCareMsgBox.Show("VillageTownCityName", this);
            ddvillageName.Focus();
            return false;
        }
        if (dddistrictName.SelectedValue == "0")
        {
            IQCareMsgBox.Show("DistrictCountyWard", this);
            dddistrictName.Focus();
            return false;
        }
        if (String.IsNullOrEmpty(txtemergContactName.Text.Trim()))
        {
            IQCareMsgBox.Show("EmergencyContactName", this);
            txtemergContactName.Focus();
            return false;
        }
        return true;

    }
    private void HashTableParameter()
    {
        try
        {
            htParameters = new Hashtable();
            htParameters.Clear();
            htParameters.Add("FirstName", txtfirstName.Text.Trim());
            htParameters.Add("MiddleName", txtmiddleName.Text.Trim());
            htParameters.Add("LastName", txtlastName.Text.Trim());
            //if (Request.QueryString["name"].ToString() == "Add")
            if ((Convert.ToInt32(Session["PatientId"])) == 0)
                htParameters.Add("LocationID", Session["AppLocationID"].ToString());
            else
                htParameters.Add("LocationID", ViewState["LocationID"]);

            htParameters.Add("IQNumber", txtIQCareRef.Text);
            htParameters.Add("Gender", ddgender.SelectedValue);
            htParameters.Add("DOB", TxtDOB.Value);
            if (rbtndobPrecEstimated.Checked == true)
            {
                htParameters.Add("DOBPrecision", 1);
            }
            else if (rbtndobPrecExact.Checked == true)
            {
                htParameters.Add("DOBPrecision", 0);
            }
            else
            {
                htParameters.Add("DOBPrecision", 2);
            }
            //if (ImgfileUploader.PostedFile !=null)
            //{

            //    HttpPostedFile theFile = ImgfileUploader.PostedFile;
            //    theFile.SaveAs(Server.MapPath(string.Format("images//{0}", ImgfileUploader.PostedFile.FileName)));
            //    patient_image = ReadFile(Server.MapPath("~/images") + "//" + ImgfileUploader.PostedFile.FileName);
            //    str_Patimg = Convert.ToBase64String(patient_image);
            //    htParameters.Add("PatientImage", str_Patimg);
            //}

            htParameters.Add("RegistrationDate", txtRegDate.Value);
            htParameters.Add("LocalCouncil", txtlocalCouncil.Text.Trim());
            htParameters.Add("VillageName", ddvillageName.SelectedValue);
            htParameters.Add("DistrictName", dddistrictName.SelectedValue);
            htParameters.Add("Province", ddProvince.SelectedValue);
            htParameters.Add("Address", txtaddress.Text.Trim());
            htParameters.Add("Phone", txtphone.Text.Trim());
            htParameters.Add("MaritalStatus", ddmaritalStatus.SelectedValue);
            htParameters.Add("EmergencyContactName", txtemergContactName.Text.Trim());
            htParameters.Add("EmergencyContactRelation", ddEmergContactRelation.SelectedValue.ToString());
            htParameters.Add("EmergencyContactPhone", txtemergContactPhone.Text);
            htParameters.Add("EmergencyContactAddress", txtemergContactAddress.Text);
            htParameters.Add("UserID", 1);
            htParameters.Add("SatelliteID", Session["AppSatelliteId"].ToString());
            htParameters.Add("CountryID", Session["AppCountryId"].ToString());
            htParameters.Add("PosID", Session["AppPosID"].ToString());
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

        }
    }

    private void LoadPatientData()
    {
        //String Ptn_Pk = Request.QueryString["PatientId"];
        //String moduleID = Request.QueryString["moduleID"];
        String moduleID;
        String Ptn_Pk = Session["PatientId"].ToString();
        if (Session["CEModule"] != null)
            moduleID = Session["CEModule"].ToString();
        IQCareUtils theUtil = new IQCareUtils();
        try
        {
            //if (Request.QueryString["name"] == "Edit")
            if (Convert.ToInt32(Session["PatientId"]) > 0)
            {
                PatientManager = (IPatientRegistration)ObjectFactory.CreateInstance(ObjFactoryParameter);
                //DataSet theDS = PatientManager.GetPatientRegistration(Convert.ToInt32(Ptn_Pk), Convert.ToInt32(moduleID));
                DataSet theDS = PatientManager.GetPatientRegistration(Convert.ToInt32(Ptn_Pk), 12);

                ViewState["ptnid"] = Ptn_Pk;
                ViewState["VisitPk"] = theDS.Tables[4].Rows[0]["VisitId"].ToString();
                this.txtIQCareRef.Text = theDS.Tables[0].Rows[0]["IQNumber"].ToString();
                ViewState["IQNumber"] = txtIQCareRef.Text;
                imgbarcode.Src = "barcode.gif?code=" + ViewState["IQNumber"];
                imgpatient.Src = "~/PatientImages//" + ViewState["IQNumber"] + ".jpg";
                this.ddgender.SelectedValue = theDS.Tables[0].Rows[0]["RegSex"].ToString();
                this.txtRegDate.Value = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(theDS.Tables[0].Rows[0]["RegDate"]));
                //if (theDS.Tables[3].Rows.Count > 0)
                //{
                //    ViewState["ARTStartDate"] = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(theDS.Tables[3].Rows[0]["Startdate"]));
                //}
                if (theDS.Tables[2].Rows.Count > 0)
                {
                    if (theDS.Tables[2].Rows[0]["DataQuality"] != System.DBNull.Value && Convert.ToInt32(theDS.Tables[2].Rows[0]["DataQuality"]) == 1)
                    {
                        btncomplete.CssClass = "greenbutton";
                    }
                }
                this.txtageCurrentYears.Text = theDS.Tables[0].Rows[0]["Age"].ToString();
                this.txtageCurrentMonths.Text = theDS.Tables[0].Rows[0]["Age1"].ToString();
                this.txtlastName.Text = theDS.Tables[0].Rows[0]["LastName"].ToString();
                this.txtmiddleName.Text = theDS.Tables[0].Rows[0]["MiddleName"].ToString();
                this.txtfirstName.Text = theDS.Tables[0].Rows[0]["FirstName"].ToString();
                this.txtlocalCouncil.Text = theDS.Tables[0].Rows[0]["LocalCouncil"].ToString();
                this.TxtDOB.Value = ((DateTime)theDS.Tables[0].Rows[0]["RegDOB"]).ToString(Session["AppDateFormat"].ToString());
                this.txtphone.Text = theDS.Tables[0].Rows[0]["Phone"].ToString();
                if (Convert.ToInt32(theDS.Tables[0].Rows[0]["DobPrecision"]) == 1)
                {
                    this.rbtndobPrecEstimated.Checked = true;
                }
                else if (Convert.ToInt32(theDS.Tables[0].Rows[0]["DobPrecision"]) == 0)
                {
                    this.rbtndobPrecExact.Checked = true;
                }
                if (theDS.Tables[1].Rows.Count > 0)
                {
                    this.txtemergContactPhone.Text = theDS.Tables[1].Rows[0]["EmergContactPhone"].ToString();
                    this.txtemergContactName.Text = theDS.Tables[1].Rows[0]["EmergContactName"].ToString();
                    this.txtemergContactAddress.Text = theDS.Tables[1].Rows[0]["EmergContactAddress"].ToString();
                    this.ddEmergContactRelation.SelectedValue = theDS.Tables[1].Rows[0]["EmergContactRelation"].ToString();
                }
                this.ddmaritalStatus.SelectedValue = theDS.Tables[0].Rows[0]["MaritalStatus"].ToString();
                this.ddProvince.SelectedValue = theDS.Tables[0].Rows[0]["Province"].ToString();
                this.ddvillageName.SelectedValue = theDS.Tables[0].Rows[0]["VillageName"].ToString();
                this.dddistrictName.SelectedValue = theDS.Tables[0].Rows[0]["DistrictName"].ToString();
                this.txtaddress.Text = theDS.Tables[0].Rows[0]["Address"].ToString();

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

        }
    }


    void SelectedProvinceItemTypeChanged(object sender, EventArgs e)
    {
        if (ddProvince.SelectedValue != string.Empty && int.Parse(ddProvince.SelectedValue) > 0)
        {
            BindFunctions BindManager = new BindFunctions();
            int provinceID = int.Parse(ddProvince.SelectedValue);
            ICommonData oCommonData = (ICommonData)ObjectFactory.CreateInstance("BusinessProcess.Service.BCommonData,BusinessProcess.Service");
            DataTable theDT = (DataTable)oCommonData.getAllDistrict();
            DataView theDV = theDT.DefaultView;
            theDV.RowFilter = "ProvinceID = " + provinceID + "";
            if (theDV != null && theDV.Table.Rows.Count > 0)
            {
                BindManager.BindCombo(dddistrictName, theDV.ToTable(), "Name", "ID");
                theDT.Clear();
            }
        }
    }



    protected void Init_Form()
    {
        //(Master.FindControl("lblheaderfacility") as Label).Visible = true;
        //(Master.FindControl("lblheader") as Label).Text = "Patient Registration";
        //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Patient Registration";
        try
        {

            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();
            DataSet theDSXML = new DataSet();
            theDSXML.ReadXml(MapPath(".\\XMLFiles\\AllMasters.con"));

            //if (Request.QueryString["name"] == "Add")
            if ((Session["PatientId"] == null) || (Convert.ToInt32(Session["PatientId"]) == 0))
            {
                DataView theDV = new DataView();
                DataTable theDT = new DataTable();
                /*******/
                theDV = new DataView(theDSXML.Tables["Mst_District"]);
                theDV.RowFilter = "DeleteFlag=0 and SystemID= " + Session["SystemId"] + "";
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(dddistrictName, theDT, "Name", "ID");
                    theDV.Dispose();
                    theDT.Clear();

                }
                theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                theDV.RowFilter = "DeleteFlag=0 and CodeID=8 and (SystemID=0 or SystemID=" + Session["SystemId"] + ")";
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(ddEmergContactRelation, theDT, "Name", "ID");
                    theDV.Dispose();
                    theDT.Clear();
                }

                theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                theDV.RowFilter = "DeleteFlag=0 and CodeID=4";
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(ddgender, theDT, "Name", "ID");
                    theDV.Dispose();
                    theDT.Clear();
                }
                /*******/
                theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                theDV.RowFilter = "DeleteFlag=0 and CodeID=12 and SystemID=" + Session["SystemId"] + "";
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(ddmaritalStatus, theDT, "Name", "ID");
                    theDV.Dispose();
                    theDT.Clear();
                }
                theDV = new DataView(theDSXML.Tables["Mst_Province"]);
                theDV.RowFilter = "Deleteflag=0 and SystemID=" + Session["SystemId"] + "";
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(ddProvince, theDT, "Name", "ID");
                    theDV.Dispose();
                    theDT.Clear();
                }

                theDV = new DataView(theDSXML.Tables["mst_Village"]);
                theDV.RowFilter = "Deleteflag=0 and SystemID=" + Session["SystemId"] + "";
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(ddvillageName, theDT, "Name", "ID");
                    theDV.Dispose();
                    theDT.Clear();
                }

                if (!IsPostBack)
                {
                    //if (Request.QueryString["PatientID"] == null)
                    if ((Session["PatientId"] == null) || (Convert.ToInt32(Session["PatientId"]) == 0))
                    {
                        Hashtable theHT = (Hashtable)Session["EnrollParams"];
                        if (theHT["FirstName"].ToString() != null)
                            txtfirstName.Text = theHT["FirstName"].ToString();
                        if (theHT["LastName"].ToString() != null)
                            txtlastName.Text = theHT["LastName"].ToString();
                        if (theHT["Date of Birth"].ToString() == null)
                            TxtDOB.Value = theHT["Date of Birth"].ToString();
                        if (theHT["Sex"].ToString() == null)
                            ddgender.Text = theHT["Sex"].ToString();
                        Session.Remove("EnrollParams");
                    }
                    else
                    {
                        PatientManager = (IPatientRegistration)ObjectFactory.CreateInstance(ObjFactoryParameter);
                        //int patientID = Convert.ToInt32(Request.QueryString["PatientID"]);
                        int patientID = Convert.ToInt32(Session["PatientID"]);
                        ViewState["ptnid"] = patientID;
                        DataTable RecordDT = PatientManager.GetPatientRecord(patientID);
                        this.txtageCurrentYears.Text = RecordDT.Rows[0]["Age"].ToString();
                        this.txtageCurrentMonths.Text = RecordDT.Rows[0]["Age1"].ToString();
                        this.txtlastName.Text = RecordDT.Rows[0]["LastName"].ToString();
                        this.txtfirstName.Text = RecordDT.Rows[0]["FirstName"].ToString();
                        this.txtlocalCouncil.Text = RecordDT.Rows[0]["LocalCouncil"].ToString();
                        this.ddgender.SelectedValue = RecordDT.Rows[0]["Sex"].ToString();
                        this.TxtDOB.Value = ((DateTime)RecordDT.Rows[0]["DOB"]).ToString(Session["AppDateFormat"].ToString());
                    }

                }

            }
            //if (Request.QueryString["name"] == "Edit")
            if (Convert.ToInt32(Session["PatientId"]) > 0)
            {
                DataView theDV = new DataView();
                DataTable theDT = new DataTable();
                theDV = new DataView(theDSXML.Tables["Mst_District"]);
                theDV.RowFilter = "SystemID=" + Session["SystemId"] + "";
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(dddistrictName, theDT, "Name", "ID");
                    theDV.Dispose();
                    theDT.Clear();
                }

                theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                theDV.RowFilter = "CodeID=8 and (SystemID=0 or SystemID=" + Session["SystemId"] + ")";
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(ddEmergContactRelation, theDT, "Name", "ID");
                    theDV.Dispose();
                    theDT.Clear();
                }

                theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                theDV.RowFilter = "CodeID=4";
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(ddgender, theDT, "Name", "ID");
                    theDV.Dispose();
                    theDT.Clear();
                }

                theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                theDV.RowFilter = "CodeID=12 and SystemID=" + Session["SystemId"] + "";
                theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                BindManager.BindCombo(ddmaritalStatus, theDT, "Name", "ID");
                theDV.Dispose();
                theDT.Clear();

                theDV = new DataView(theDSXML.Tables["Mst_Province"]);
                theDV.RowFilter = "SystemID=" + Session["SystemId"] + "";
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(ddProvince, theDT, "Name", "ID");
                    theDV.Dispose();
                    theDT.Clear();
                }

                theDV = new DataView(theDSXML.Tables["mst_Village"]);
                theDV.RowFilter = "SystemID=" + Session["SystemId"] + "";
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(ddvillageName, theDT, "Name", "ID");
                    theDV.Dispose();
                    theDT.Clear();
                }


            }
            PatientManager = null;
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

        }

    }

    protected void Authenticate()
    {
        AuthenticationManager Authentication = new AuthenticationManager();

        if (Authentication.HasFunctionRight(ApplicationAccess.PatientRegistration, FunctionAccess.Print, (DataTable)Session["UserRight"]) == false)
        {
            btnPrint.Enabled = false;

        }
        //if (Request.QueryString["name"] == "Add")
        if (Session["PatientId"] == null || Convert.ToInt32(Session["PatientId"]) == 0)
        {
            if (Authentication.HasFunctionRight(ApplicationAccess.PatientRegistration, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
            {
                btnsave.Enabled = false;
                btncomplete.Enabled = false;
            }
        }
        else
        {
            if (Authentication.HasFunctionRight(ApplicationAccess.PatientRegistration, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
            {
                //int PatientID = Convert.ToInt32(Request.QueryString["PatientId"]);
                int PatientID = Convert.ToInt32(Session["PatientId"]);
                string theUrl = "";
                theUrl = string.Format("{0}", "./ClinicalForms/frmPatient_History.aspx");
                Response.Redirect(theUrl);
            }
            else if (Authentication.HasFunctionRight(ApplicationAccess.PatientRegistration, FunctionAccess.Update, (DataTable)Session["UserRight"]) == false)
            {
                btnsave.Enabled = false;
                btncomplete.Enabled = false;
            }
        }

    }

    protected void Page_Init(Object sender, EventArgs e)
    {
        if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
        {
            IQCareMsgBox.Show("SessionExpired", this);
            Response.Redirect("~/frmlogin.aspx", true);
        }
        if (!IsPostBack)
        {
            Init_Form();
            /***************** Check For User Rights ****************/
            Authenticate();
        }
    }
    private void HeaderLabel()
    {
        this.txtfirstName.Focus();
        this.txtageCurrentYears.Text = Request[this.txtageCurrentYears.UniqueID];
        this.txtageCurrentMonths.Text = Request[this.txtageCurrentMonths.UniqueID];
        //(Master.FindControl("lblformname") as Label).Text = "HIV Care Registration";
        //(Master.FindControl("lblRoot") as Label).Text = "Clinical Forms";
        //(Master.FindControl("lblheader") as Label).Text = "Registration";
        //(Master.FindControl("PanelPatiInfo") as Panel).Visible = false;
    }

    private void Attributes()
    {

        IIQCareSystem SystemManager = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
        DateTime theCurrentDate = SystemManager.SystemDate();
        SystemManager = null;
        txtSysDate.Text = theCurrentDate.ToString(Session["AppDateFormat"].ToString());
        txtemergContactPhone.Attributes.Add("onkeyup", "chkNumeric('" + txtemergContactPhone.ClientID + "')");
        txtphone.Attributes.Add("onkeyup", "chkNumeric('" + txtphone.ClientID + "')");
        txtlastName.Attributes.Add("onkeyup", "chkString('" + txtlastName.ClientID + "')");
        txtfirstName.Attributes.Add("onkeyup", "chkString('" + txtfirstName.ClientID + "')");
        TxtDOB.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
        txtRegDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
        txtRegDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3'); isCheckValidDate('" + Application["AppCurrentDate"] + "', '" + txtRegDate.ClientID + "', '" + txtRegDate.ClientID + "');");
        TxtDOB.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3'); CalcualteAge('" + txtageCurrentYears.ClientID + "','" + txtageCurrentMonths.ClientID + "','" + TxtDOB.ClientID + "','" + txtSysDate.ClientID + "');");
        txtphone.Attributes.Add("onkeyup", "chkNumeric('" + txtphone.ClientID + "')");
        txtemergContactPhone.Attributes.Add("onkeyup", "chkNumeric('" + txtemergContactPhone.ClientID + "')");
        txtNationalID.Attributes.Add("onkeyup", "chkNumeric('" + txtNationalID.ClientID + "')");
        txtageCurrentMonths.Attributes.Add("onkeyup", "chkNumeric('" + txtageCurrentMonths.ClientID + "')");
        txtageCurrentYears.Attributes.Add("onkeyup", "chkNumeric('" + txtageCurrentYears.ClientID + "')");
        
    }
    protected void Page_Load(Object sender, EventArgs e)
    {
        if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
        {
            IQCareMsgBox.Show("SessionExpired", this);
            Response.Redirect("~/frmlogin.aspx", true);
        }
        HeaderLabel();
        ddProvince.SelectedIndexChanged += new EventHandler(SelectedProvinceItemTypeChanged);
        Ajax.Utility.RegisterTypeForAjax(typeof(frmPatientRegistration));
        try
        {
            Attributes();
            if (!IsPostBack)
            {
                ViewState["ptnid"] = "";
                LoadPatientData();
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
    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public string GetDuplicateRecord(string strfname, string strmname, string strlname, string address, string Phone)
    {
        IPatientRegistration PatientManager;
        StringBuilder objBilder = new StringBuilder();
        PatientManager = (IPatientRegistration)ObjectFactory.CreateInstance(ObjFactoryParameter);
        DataSet dsPatient = new DataSet();
        dsPatient = PatientManager.GetDuplicatePatientSearchResults(strlname, strmname, strfname, address, Phone);

        if (dsPatient.Tables[0].Rows.Count > 0)
        {
            objBilder.Append("<table border='0'  width='100%'>");
            objBilder.Append("<tr style='background-color:#e1e1e1'>");
            //objBilder.Append("<td class='smallerlabel'>PatientID</td>");
            objBilder.Append("<td class='smallerlabel'>IQ Number</td>");
            objBilder.Append("<td class='smallerlabel'>F name</td>");
            objBilder.Append("<td class='smallerlabel'>L name</td>");
            objBilder.Append("<td class='smallerlabel'>Registration Date</td>");
            objBilder.Append("<td class='smallerlabel'>Existing Hosp/Clinic #</td>");
            objBilder.Append("<td class='smallerlabel'>Chief/Local Council</td>");
            objBilder.Append("<td class='smallerlabel'>Phone</td>");
            objBilder.Append("<td class='smallerlabel'>Facility</td>");
            objBilder.Append("</tr>");
            for (int i = 0; i < dsPatient.Tables[0].Rows.Count; i++)
            {
                objBilder.Append("<tr>");
                //objBilder.Append("<td class='smallerlabel'>" + dsPatient.Tables[0].Rows[i]["PatientRegistrationID"].ToString() + "</td>");
                objBilder.Append("<td class='smallerlabel'>" + dsPatient.Tables[0].Rows[i]["IQNumber"].ToString() + "</td>");
                objBilder.Append("<td class='smallerlabel'>" + dsPatient.Tables[0].Rows[i]["firstname"].ToString() + "</td>");
                objBilder.Append("<td class='smallerlabel'>" + dsPatient.Tables[0].Rows[i]["lastname"].ToString() + "</td>");
                objBilder.Append("<td class='smallerlabel'>" + dsPatient.Tables[0].Rows[i]["RegistrationDate"].ToString() + "</td>");
                objBilder.Append("<td class='smallerlabel'>" + dsPatient.Tables[0].Rows[i]["PatientClinicID"].ToString() + "</td>");
                objBilder.Append("<td class='smallerlabel'>" + dsPatient.Tables[0].Rows[i]["LocalCouncil"].ToString() + "</td>");
                objBilder.Append("<td class='smallerlabel'>" + dsPatient.Tables[0].Rows[i]["Phone"].ToString() + "</td>");
                objBilder.Append("<td class='smallerlabel'>" + dsPatient.Tables[0].Rows[i]["FacilityName"].ToString() + "</td>");
                objBilder.Append("</tr>");
            }
            objBilder.Append("</table>");
        }
        return objBilder.ToString();
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        //Save Validation
        if (FieldValidation() == false)
        {
            return;
        }
        //Save and Update in Normal Save
        try
        {
            HashTableParameter();
            IPatientRegistration PatientManager = (IPatientRegistration)ObjectFactory.CreateInstance(ObjFactoryParameter);
            //if (Request.QueryString["name"] == "Add")
            if (Convert.ToInt32(Session["PatientId"]) == 0)
            {
                if (Convert.ToString(ViewState["ptnid"]) == "")
                {
                    DataTable Patientds = PatientManager.SaveNewRegistration(htParameters, 0);
                    Session["PatientId"] = Patientds.Rows[0]["ptn_pk"].ToString();
                    ViewState["ptnid"] = Patientds.Rows[0]["ptn_pk"].ToString();
                    ViewState["visitPk"] = Patientds.Rows[0]["Visit_Id"].ToString();
                    ViewState["IQNumber"] = Patientds.Rows[0]["IQNumber"].ToString();
                    txtIQCareRef.Text = ViewState["IQNumber"].ToString();
                    if (ImgfileUploader.PostedFile != null)
                    {

                        HttpPostedFile theFile = ImgfileUploader.PostedFile;
                        theFile.SaveAs(Server.MapPath(string.Format("PatientImages//{0}", ViewState["IQNumber"] + Path.GetExtension(theFile.FileName))));


                    }
                    //if (barcodefileupload.PostedFile != null)
                    //{

                    //    HttpPostedFile thebarFile = barcodefileupload.PostedFile;
                    //    thebarFile.SaveAs(Server.MapPath(string.Format("PatientImages//{0}", "BAR" + ViewState["IQNumber"] + Path.GetExtension(thebarFile.FileName))));


                    //}
                    //imgbarcode.Src = "~/PatientImages//BAR" + ViewState["IQNumber"] + ".jpg";
                    imgbarcode.Src = "barcode.gif?code=" + ViewState["IQNumber"];
                    imgpatient.Src = "~/PatientImages//" + ViewState["IQNumber"] + ".jpg";
                    //Session["PRegistration"] = "Add";
                    SaveCancel();
                }

            }
            //else if (Request.QueryString["name"] == "Edit" && Session["PRegistration"].ToString() == "Add")
            //////else if (Convert.ToInt32(Session["PatientId"]) > 0 && Session["PRegistration"].ToString() == "Add")
            //////{

            //////    int PatientUpdated = PatientManager.UpdatePatientRegistration(htParameters, Convert.ToInt32(ViewState["ptnid"]), Convert.ToInt32(ViewState["VisitPk"]), 0);
            //////    SaveCancel();
            //////}
            //else if (Request.QueryString["name"] == "Edit" && Session["PRegistration"].ToString() == "Edit")
            else if (Convert.ToInt32(Session["PatientId"]) > 0)
            {

                int PatientUpdated = PatientManager.UpdatePatientRegistration(htParameters, Convert.ToInt32(ViewState["ptnid"]), Convert.ToInt32(ViewState["VisitPk"]), 0);
                if (ImgfileUploader.PostedFile != null)
                {
                    if (ImgfileUploader.HasFile)
                    {
                        HttpPostedFile theFile = ImgfileUploader.PostedFile;
                        theFile.SaveAs(Server.MapPath(string.Format("PatientImages//{0}", ViewState["IQNumber"] + Path.GetExtension(theFile.FileName))));
                    }

                }
                //if (barcodefileupload.PostedFile != null)
                //{

                //    HttpPostedFile thebarFile = barcodefileupload.PostedFile;
                //    thebarFile.SaveAs(Server.MapPath(string.Format("PatientImages//{0}", "BAR" + ViewState["IQNumber"] + Path.GetExtension(thebarFile.FileName))));


                //}
                UpdateCancel();
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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (txtIQCareRef.Text == "")
        {
            Response.Redirect("~/frmFindAddPatient.aspx");
        }
        else
            Response.Redirect("frmAddTechnicalArea.aspx");

        //(Master.FindControl("lblpntStatus") as Label).Text = Request.QueryString["sts"].ToString();
        //if ((Master.FindControl("lblpntStatus") as Label).Text == "1")
        //{
        //    if (Request.QueryString["name"] == "Add")
        //    {

        //        string theUrl;
        //        theUrl = string.Format("{0}?PatientId={1}", "./frmAddTechnicalArea.aspx?Name=Add", Request.QueryString["PatientId"].ToString());
        //        Response.Redirect(theUrl);
        //    }

        //    else if (Request.QueryString["name"] == "Edit")
        //    {
        //        string theUrl;
        //        theUrl = string.Format("{0}?PatientId={1}&sts={2}", "./frmAddTechnicalArea.aspx?Name=Add", Request.QueryString["PatientId"].ToString(), Request.QueryString["sts"].ToString());
        //        Response.Redirect(theUrl);
        //    }
        //}
        //else
        //{
        //    if (Request.QueryString["name"] == "Add")
        //    {

        //        if (ViewState["ptnid"].ToString() == "")
        //        {
        //            Response.Redirect("~/frmFindAddPatient.aspx");
        //        }
        //        else
        //        {
        //            string theUrl;
        //            theUrl = string.Format("{0}?PatientId={1}", "./frmAddTechnicalArea.aspx?Name=Add", ViewState["ptnid"].ToString());
        //            Response.Redirect(theUrl);
        //        }
        //    }
        //    else
        //    {
        //        string theUrl;
        //        theUrl = string.Format("{0}?PatientId={1}&sts={2}", "./frmAddTechnicalArea.aspx?Name=Add", Request.QueryString["PatientId"].ToString(), Request.QueryString["sts"].ToString());
        //        Response.Redirect(theUrl);
        //    }


        //}
    }

    private void SaveCancel()
    {
        string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
        script += "var ans;\n";
        // script += "ans=window.confirm('This IQNumber " + ViewState["IQNumber"].ToString() + " is generated\\n Registration Form Saved Successfully. Do you want to close?');\n";
        script += "ans=window.confirm('This Registration will be redirected to Service. Do you want to close?');\n";
        script += "if (ans==true)\n";
        script += "{\n";
        script += "window.location.href='./frmAddTechnicalArea.aspx';\n";
        script += "}\n";
        //script += "else \n";
        //script += "{\n";
        //script += "window.location.href('frmPatientRegistration.aspx');\n";
        //script += "}\n";
        script += "</script>\n";
        RegisterStartupScript("confirm", script);
    }

    private void DQCancel()
    {
        string script = "<script language = 'javascript' defer ='defer' id = 'confirm1'>\n";
        script += "var ans;\n";
        script += "ans=window.confirm('This IQNumber " + ViewState["IQNumber"].ToString() + " is generated \\nRegistration DQ Checked complete.Form Marked as DQ Checked.\\n Do you want to close?');\n";
        script += "if (ans==true)\n";
        script += "{\n";
        script += "window.location.href='./frmAddTechnicalArea.aspx';\n";
        script += "}\n";
        //script += "else \n";
        //script += "{\n";
        //script += "window.location.href('frmPatientRegistration.aspx');\n";
        //script += "}\n";
        script += "</script>\n";
        RegisterStartupScript("confirm1", script);
    }


    private void UpdateCancel()
    {
        string script = "<script language = 'javascript' defer ='defer' id = 'confirm2'>\n";
        script += "var ans;\n";
        script += "ans=window.confirm('Registration Form Update Successfully. Do you want to close?');\n";
        script += "if (ans==true)\n";
        script += "{\n";
        script += "window.location.href='./frmAddTechnicalArea.aspx';\n";
        script += "}\n";
        //script += "else \n";
        //script += "{\n";
        //script += "window.location.href('frmPatientRegistration.aspx');\n";
        //script += "}\n";
        script += "</script>\n";
        RegisterStartupScript("confirm2", script);
    }

    private void DQUpdateCancel()
    {
        string script = "<script language = 'javascript' defer ='defer' id = 'confirm3'>\n";
        script += "var ans;\n";
        script += "ans=window.confirm('Registration DQ Checked complete.Form Marked as DQ Checked. Do you want to close?');\n";
        script += "if (ans==true)\n";
        script += "{\n";
        script += "window.location.href='./frmAddTechnicalArea.aspx';\n";
        script += "}\n";
        //script += "else \n";
        //script += "{\n";
        //script += "window.location.href('frmPatientRegistration.aspx');\n";
        //script += "}\n";
        script += "</script>\n";
        RegisterStartupScript("confirm3", script);
    }


    protected void btncomplete_Click(object sender, EventArgs e)
    {
        ////Data Quality Validation
        //string msg = DataQuality_Msg();
        //if (msg.Length > 69)
        //{
        //    MsgBuilder theBuilder1 = new MsgBuilder();
        //    theBuilder1.DataElements["MessageText"] = msg;
        //    IQCareMsgBox.Show("#C1", theBuilder1, this);
        //    return;
        //}
        ////Field Validation

        //if (FieldValidation_DQ() == false)
        //{
        //    return;
        //}

        ////Save and Update in DataQuality

        //try
        //{
        //    HashTableParameter();
        //    IPatientRegistration PatientManager = (IPatientRegistration)ObjectFactory.CreateInstance(ObjFactoryParameter);
        //    //if (Request.QueryString["name"] == "Add")
        //    if(Convert.ToInt32(Session["PatientId"])==0)
        //    {
        //        if (Convert.ToString(ViewState["ptnid"]) == "")
        //        {
        //            DataTable Patientds = PatientManager.SaveNewRegistration(htParameters, 1);
        //            Session["PatientId"] = Patientds.Rows[0]["ptn_pk"].ToString();
        //            ViewState["ptnid"] = Patientds.Rows[0]["ptn_pk"].ToString();
        //            ViewState["visitPk"] = Patientds.Rows[0]["Visit_Id"].ToString();
        //            ViewState["IQNumber"] = Patientds.Rows[0]["IQNumber"].ToString();
        //            txtIQCareRef.Text = ViewState["IQNumber"].ToString();
        //            //Session to Redirect Technical Area with Add Mode even in Cancel 
        //            if (ImgfileUploader.PostedFile != null)
        //            {

        //                HttpPostedFile theFile = ImgfileUploader.PostedFile;
        //                theFile.SaveAs(Server.MapPath(string.Format("PatientImages//{0}", ViewState["IQNumber"] + Path.GetExtension(theFile.FileName))));


        //            }
        //            //if (barcodefileupload.PostedFile != null)
        //            //{

        //            //    HttpPostedFile thebarFile = barcodefileupload.PostedFile;
        //            //    thebarFile.SaveAs(Server.MapPath(string.Format("PatientImages//{0}", "BAR" + ViewState["IQNumber"] + Path.GetExtension(thebarFile.FileName))));


        //            //}
        //            //imgbarcode.Src = "~/PatientImages//BAR" + ViewState["IQNumber"] + ".jpg";
        //            imgbarcode.Src = "barcode.gif?code=" + ViewState["IQNumber"];
        //            imgpatient.Src = "~/PatientImages//" + ViewState["IQNumber"] + ".jpg";
        //            //Session["PRegistration"] = "Add";
        //            btncomplete.CssClass = "greenbutton";
        //            DQCancel();
        //        }

        //    }
        //    //else if (Request.QueryString["name"] == "Edit" && Session["PRegistration"].ToString() == "Add")
        //    //////else if (Convert.ToInt32(Session["PatientId"]) > 0 && Session["PRegistration"].ToString() == "Add")
        //    //////{

        //    //////    PatientManager.UpdatePatientRegistration(htParameters, Convert.ToInt32(ViewState["ptnid"]), Convert.ToInt32(ViewState["VisitPk"]), 1);
        //    //////    btncomplete.CssClass = "greenbutton";
        //    //////    DQCancel();
        //    //////}
        //    //else if (Request.QueryString["name"] == "Edit" && Session["PRegistration"].ToString() == "Edit")
        //    else if (Convert.ToInt32(Session["PatientId"]) > 0)
        //    {
        //        PatientManager.UpdatePatientRegistration(htParameters, Convert.ToInt32(ViewState["ptnid"]), Convert.ToInt32(ViewState["VisitPk"]), 1);
        //        if (ImgfileUploader.PostedFile != null)
        //        {
        //            if (ImgfileUploader.HasFile)
        //            {
        //                HttpPostedFile theFile = ImgfileUploader.PostedFile;
        //                theFile.SaveAs(Server.MapPath(string.Format("PatientImages//{0}", ViewState["IQNumber"] + Path.GetExtension(theFile.FileName))));
        //            }


        //        }
        //        //if (barcodefileupload.PostedFile != null)
        //        //{

        //        //    HttpPostedFile thebarFile = barcodefileupload.PostedFile;
        //        //    thebarFile.SaveAs(Server.MapPath(string.Format("PatientImages//{0}", "BAR" + ViewState["IQNumber"] + Path.GetExtension(thebarFile.FileName))));


        //        //}
        //        btncomplete.CssClass = "greenbutton";
        //        DQUpdateCancel();
        //    }
        //}
        //catch (Exception err)
        //{
        //    MsgBuilder theBuilder = new MsgBuilder();
        //    theBuilder.DataElements["MessageText"] = err.Message.ToString();
        //    IQCareMsgBox.Show("#C1", theBuilder, this);
        //    return;
        //}
        //finally
        //{
        //    PatientManager = null;
        //}
    }




    protected void btncontinue_Click(object sender, EventArgs e)
    {
        //Save Validation
        if (FieldValidation() == false)
        {
            return;
        }
        //Save and Update in Normal Save
        try
        {
            HashTableParameter();
            IPatientRegistration PatientManager = (IPatientRegistration)ObjectFactory.CreateInstance(ObjFactoryParameter);
            if (Convert.ToInt32(Session["PatientId"]) == 0)
            {
                if (Convert.ToString(ViewState["ptnid"]) == "")
                {
                    Session["htPtnRegParameter"] = htParameters;

                    //DataTable Patientds = PatientManager.SaveNewRegistration(htParameters, 0);
                    //Session["PatientId"] = Patientds.Rows[0]["ptn_pk"].ToString();
                    //ViewState["ptnid"] = Patientds.Rows[0]["ptn_pk"].ToString();
                    //ViewState["visitPk"] = Patientds.Rows[0]["Visit_Id"].ToString();
                    //ViewState["IQNumber"] = Patientds.Rows[0]["IQNumber"].ToString();
                    //txtIQCareRef.Text = ViewState["IQNumber"].ToString();
                    //if (ImgfileUploader.PostedFile != null)
                    //{

                    //    HttpPostedFile theFile = ImgfileUploader.PostedFile;
                    //    theFile.SaveAs(Server.MapPath(string.Format("PatientImages//{0}", ViewState["IQNumber"] + Path.GetExtension(theFile.FileName))));


                    //}
                    //imgbarcode.Src = "barcode.gif?code=" + ViewState["IQNumber"];
                    //imgpatient.Src = "~/PatientImages//" + ViewState["IQNumber"] + ".jpg";
                    SaveCancel();
                }

            }
            else if (Convert.ToInt32(Session["PatientId"]) > 0)
            {

                int PatientUpdated = PatientManager.UpdatePatientRegistration(htParameters, Convert.ToInt32(ViewState["ptnid"]), Convert.ToInt32(ViewState["VisitPk"]), 0);
                if (ImgfileUploader.PostedFile != null)
                {
                    if (ImgfileUploader.HasFile)
                    {
                        HttpPostedFile theFile = ImgfileUploader.PostedFile;
                        theFile.SaveAs(Server.MapPath(string.Format("PatientImages//{0}", ViewState["IQNumber"] + Path.GetExtension(theFile.FileName))));
                    }

                }
                UpdateCancel();
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

    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    protected void btncalculate_DOB_Click(object sender, EventArgs e)
    {
        if (txtageCurrentYears.Text.Trim() == string.Empty)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "Age (Years)";
            IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
            txtageCurrentYears.Focus();
            return;
        }
        
        if (txtageCurrentMonths.Text != "")
        {
            if ((Convert.ToInt32(txtageCurrentMonths.Text) < 0) || (Convert.ToInt32(txtageCurrentMonths.Text) > 11))
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["Control"] = "Age (Month)";
                IQCareMsgBox.Show("AgeMonthRange", theMsg, this);
                return;
            }
        }

        int age = 0;
        int months = 0;
        DateTime currentdate;
        age = Convert.ToInt32(txtageCurrentYears.Text);
        if (txtageCurrentMonths.Text != "")
        {
            currentdate = Convert.ToDateTime(Convert.ToDateTime(txtSysDate.Text).Month + "-01-" + Convert.ToDateTime(txtSysDate.Text).Year);
        }
        else
            currentdate = Convert.ToDateTime("06-15-" + Convert.ToDateTime(txtSysDate.Text).Year);

        DateTime birthdate = currentdate.AddYears(age * -1);
        if (txtageCurrentMonths.Text != "")
        {
            months = Convert.ToInt32(txtageCurrentMonths.Text);
            birthdate = birthdate.AddMonths(months * -1);
        }

        TxtDOB.Value = ((DateTime)birthdate).ToString(Session["AppDateFormat"].ToString());
        if (TxtDOB.Value != "")
        {
            rbtndobPrecEstimated.Checked = true;
        }
    }
}
