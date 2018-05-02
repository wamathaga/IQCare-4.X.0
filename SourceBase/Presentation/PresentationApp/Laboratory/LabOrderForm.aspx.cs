using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Interface.Laboratory;
using Application.Common;
using System.Collections.Generic;
using System.Text;
using Application.Presentation;
using Interface.Administration;
using AjaxControlToolkit;
using System.Web.Script.Serialization;
using Interface.Security;
using Interface.Clinical;

public partial class Laboratory_LabOrderForm : LogPage
{


    static DataTable dtLabResult;
    static DataTable dtLabTest;
    DataView theDV = new DataView();
    DataTable DTtestDate = new DataTable();
    IIQCareSystem IQCareSecurity;
    DateTime theVisitDate, theCurrentDate;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
        {
            IQCareMsgBox.Show("SessionExpired", this);
            Response.Redirect("~/frmlogin.aspx",true);
        }

        Ajax.Utility.RegisterTypeForAjax(typeof(Laboratory_LabOrderForm));
        txtlaborderedbydate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
        txtlaborderedbydate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");

        txtLabtobeDone.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
        txtLabtobeDone.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");

        DataTable dtPatientInfo = (DataTable)Session["PatientInformation"];
        if (dtPatientInfo != null && dtPatientInfo.Rows.Count > 0)
        {
            if (Session["SystemId"].ToString() == "1")
                lblpatientname.Text = dtPatientInfo.Rows[0]["LastName"].ToString() + ", " + dtPatientInfo.Rows[0]["FirstName"].ToString();
            else
                lblpatientname.Text = dtPatientInfo.Rows[0]["LastName"].ToString() + ", " + dtPatientInfo.Rows[0]["MiddleName"].ToString() + " , " + dtPatientInfo.Rows[0]["FirstName"].ToString();
            lblIQnumber.Text = dtPatientInfo.Rows[0]["IQNumber"].ToString();


            
        }
        TechnicalAreaIdentifier();

        AuthenticationManager Authentication = new AuthenticationManager();
        if (Session["LabId"] != null)
        {
            Session["Lab_ID"] = Session["LabId"];
            ViewState["LabTestID"] = Session["LabTestID"];
            Session["LabId"] = null;
            Session["LabTestID"] = null;
        }
        if (Application["AppCurrentDate"] != null)
        {
            hdappcurrentdate.Value = Application["AppCurrentDate"].ToString();
        }
        if (!IsPostBack)
        {
            ILabFunctions LabResultManager = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
            DataSet theDSPatient = LabResultManager.GetPatientInfo(Session["PatientId"].ToString());
            if (theDSPatient.Tables[1].Rows.Count > 0)
            {
                
                ViewState["IEVisitDate"] = theDSPatient.Tables[1].Rows[0]["VisitDate"];
            
            }
            dtLabTest = new DataTable();
            dtLabTest.Rows.Clear();
            hdControlExists.Value = "";
            if (dtLabTest.Columns.Count == 0)
            {
               
                dtLabTest.Columns.Add("TestId", System.Type.GetType("System.Int32"));
                dtLabTest.Columns.Add("TestName", System.Type.GetType("System.String"));
                dtLabTest.Columns.Add("Department", System.Type.GetType("System.String"));
            }
            GetLaborderdate();
            BindControls();
        }

        
    }

    private void GetLaborderdate()
    {
        int PatientID = Convert.ToInt32(Session["PatientId"]);
        int LocationID = Convert.ToInt32(Session["AppLocationId"]);
        ILabFunctions LabTestsMgrDate;
        if (Session["Lab_ID"] != null)
        {
            LabTestsMgrDate = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
            DTtestDate = (DataTable)LabTestsMgrDate.GetLaborderdate(PatientID, LocationID, Convert.ToInt32(Session["Lab_ID"]));
            ViewState["TestDate"] = DTtestDate;
        }
    }
    private void TechnicalAreaIdentifier()
    {
        int intmoduleID = Convert.ToInt32(Session["TechnicalAreaId"]);
        IPatientHome PatientHome = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
        System.Data.DataSet DSTab = PatientHome.GetTechnicalAreaIdentifierFuture(intmoduleID, Convert.ToInt32(Session["PatientId"]));

        if (DSTab.Tables[0].Rows.Count > 0)
        {
            if (DSTab.Tables[0].Rows.Count > 0)
            {


                //thePnlIdent.Controls.Add(new LiteralControl("<td class='bold pad18' style='width: 25%'>"));
                Label theLabelIdentifier1 = new Label();
                theLabelIdentifier1.ID = "Lbl_" + DSTab.Tables[0].Rows[0][0].ToString();
                int i = 0;
                foreach (DataRow DRLabel in DSTab.Tables[0].Rows)
                {
                    foreach (DataRow DRLabel1 in DSTab.Tables[1].Rows)
                    {
                        theLabelIdentifier1.Text = theLabelIdentifier1.Text + "    " + DRLabel[0].ToString() + " : " + DRLabel1[i].ToString();
                    }
                    i++;
                }

                
                thePnlIdent.Controls.Add(theLabelIdentifier1);
                

            }

            
        }


    }

    private void BindControls()
    {
        BindFunctions theBindManager = new BindFunctions();
        IQCareUtils theUtils = new IQCareUtils();
        DataSet theDSXML = new DataSet();
        DataTable theDT = new DataTable();
        theDSXML.ReadXml(Server.MapPath("..\\XMLFiles\\AllMasters.con"));
        if (theDSXML.Tables["Mst_Employee"] != null)
        {
            theDV = new DataView(theDSXML.Tables["Mst_Employee"]);
            theDV.RowFilter = "DeleteFlag=0";
            if (theDV.Table != null)
            {
                theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                if (Convert.ToInt32(Session["AppUserEmployeeId"]) > 0)
                {
                    theDV = new DataView(theDT);
                    theDV.RowFilter = "EmployeeId =" + Session["AppUserEmployeeId"].ToString();
                    if (theDV.Count > 0)
                        theDT = theUtils.CreateTableFromDataView(theDV);
                }

                theBindManager.BindCombo(ddlaborderedbyname, theDT, "EmployeeName", "EmployeeId");
                theDV.Dispose();
                theDT.Clear();
            }
            
        }
        if (Convert.ToInt32(Session["Lab_ID"]) > 0)
        {

            BindDropdownOrderBy(DTtestDate.Rows[0]["OrderedbyName"].ToString());
            ddlaborderedbyname.SelectedValue = DTtestDate.Rows[0]["OrderedbyName"].ToString();
            txtlaborderedbydate.Text = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(DTtestDate.Rows[0]["OrderedbyDate"]));
            if (DTtestDate.Rows[0].IsNull("PreClinicLabDate") == false)
                if (((DateTime)DTtestDate.Rows[0]["PreClinicLabDate"]).ToString(Session["AppDateFormat"].ToString()) != "01-Jan-1900")
                    this.txtLabtobeDone.Text = (((DateTime)DTtestDate.Rows[0]["PreClinicLabDate"]).ToString(Session["AppDateFormat"].ToString())).ToString();

            if (this.txtLabtobeDone.Text != "")
            {
                this.preclinicLabs.Checked = true;
            }


            dtLabTest = (DataTable)ViewState["LabTestID"];
            LoadLabResult((DataTable)ViewState["LabTestID"]);
        }

        
    }
    private void BindDropdownOrderBy(String EmployeeId)
    {
        DataSet theDS = new DataSet();
        theDS.ReadXml(MapPath("..\\XMLFiles\\ALLMasters.con"));
        BindFunctions BindManager = new BindFunctions();
        IQCareUtils theUtils = new IQCareUtils();
        if (theDS.Tables["Mst_Employee"] != null)
        {
            DataView theDV = new DataView(theDS.Tables["Mst_Employee"]);
            if (theDV.Table != null)
            {
                DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                if (Convert.ToInt32(Session["AppUserEmployeeId"]) > 0)
                {
                    theDV = new DataView(theDT);
                    theDV.RowFilter = "EmployeeId IN(" + Session["AppUserEmployeeId"].ToString() + "," + EmployeeId + ")";
                    if (theDV.Count > 0)
                        theDT = theUtils.CreateTableFromDataView(theDV);
                }
                BindManager.BindCombo(ddlaborderedbyname, theDT, "EmployeeName", "EmployeeId");

            }
        }

    }
    public Control FindControlRecursive(Control container, string name)
    {
        if (container.ID == name)
            return container;

        foreach (Control ctrl in container.Controls)
        {
            string id = ctrl.ClientID;
            Control foundCtrl = FindControlRecursive(ctrl, name);
            if (foundCtrl != null)
                return foundCtrl;
        }
        return null;
    }
    #region "AutoComplete Method"
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod(EnableSession = true)]
    public static List<string> Searchlab(string prefixText, int count)
    {
        List<string> Labdetail = new List<string>();
        List<Labs> lstDrugsDetail = GetLab(prefixText, count);
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        foreach (Labs c in lstDrugsDetail)
        {
            Labdetail.Add(AutoCompleteExtender.CreateAutoCompleteItem(c.SubTestName, serializer.Serialize(c)));
        }

        return Labdetail;

    }

    #region "Class for Lab"
    public class Labs
    {
        protected int _SubTestId;
        public int SubTestId
        {
            get { return _SubTestId; }
            set { _SubTestId = value; }
        }

        protected int _LabDepartmentId;
        public int LabDepartmentId
        {
            get { return _LabDepartmentId; }
            set { _LabDepartmentId = value; }
        }

        protected string _LabDepartmentName;
        public string LabDepartmentName
        {
            get { return _LabDepartmentName; }
            set { _LabDepartmentName = value; }
        }
        protected string _SubTestName;
        public string SubTestName
        {
            get { return _SubTestName; }
            set { _SubTestName = value; }
        }


    }
    #endregion

    public static List<Labs> GetLab(string prefixText, int count)
    {
        List<Labs> items = new List<Labs>();
        ILabFunctions LabTestsMgrDate;
        LabTestsMgrDate = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
        string sqlQuery;
        //creating Sql Query

        //sqlQuery = "select l.LabTestID,l.LabName,l.LabDepartmentID,d.labdepartmentname from mst_labtest l";
        //sqlQuery += " inner join mst_labdepartment d on l.LabDepartmentID=d.LabDepartmentID where labname like '%" + prefixText + "%' group by l.labtestid,l.labname,l.LabDepartmentID,d.labdepartmentname";
         
        sqlQuery = "select a.SubTestId[LabTestID], a.SubTestName[LabName], b.LabDepartmentID, c.labdepartmentname";
        sqlQuery += " from lnk_testparameter a inner join mst_labtest b on a.TestId=b.LabTestId";
        sqlQuery += " inner join mst_labdepartment c on c.LabDepartmentId=b.LabDepartmentID";
        sqlQuery += " where a.SubTestName like '%" + prefixText + "%' group by a.SubTestId, a.SubTestName, b.LabDepartmentID, c.labdepartmentname";


        //filling data from database
          dtLabResult = (DataTable)LabTestsMgrDate.ReturnLabQuery(sqlQuery);

          if (dtLabResult.Rows.Count > 0)
            {
                foreach (DataRow row in dtLabResult.Rows)
                {
                    try
                    {
                        Labs item = new Labs();
                        item.SubTestId = (int)row["LabTestID"];
                        item.SubTestName = (string)row["LabName"];
                        item.LabDepartmentId = (int)row["LabDepartmentID"];
                        item.LabDepartmentName = (string)row["labdepartmentname"];
                        items.Add(item);
                    }
                    catch (Exception ex)
                    {

                    }
                }

            }
               

        return items;
    }

    #endregion
    protected void txtautoTestName_TextChanged(object sender, EventArgs e)
    {

        txtautoTestName.Text = "";
        if (hdCustID.Value != "" && dtLabResult.Rows.Count>0)
        {
            if ((Convert.ToInt32(hdCustID.Value) != 0))
            {
                DataTable dt= dtLabResult;
                DataView dv = new DataView(dt);
                dv.RowFilter = "LabTestID=" + hdCustID.Value + "";
                DataTable dtfilter = dv.ToTable();

                var foundlabid = dtLabTest.Select("TestId = '" + hdCustID.Value + "'");
                if (foundlabid.Length == 0)
                {
                    hdControlExists.Value = dtfilter.Rows[0]["LabTestID"].ToString();
                    DataRow drLabTest;
                    drLabTest = dtLabTest.NewRow();
                    drLabTest["TestId"] = Convert.ToInt32(dtfilter.Rows[0]["LabTestID"].ToString());
                    drLabTest["TestName"] = dtfilter.Rows[0]["LabName"].ToString();
                    drLabTest["Department"] = dtfilter.Rows[0]["labdepartmentname"].ToString();
                    dtLabTest.Rows.Add(drLabTest);
                }
                
                 LoadLabResult(dtLabTest);
               
            }

        }
    }
    public void LoadLabResult(DataTable dtresult)
    {
       
        if (dtresult.Rows.Count > 0)
        {
            pnllabtest.Visible = true;
        }
        string count = string.Empty;
        Hashtable ht = new Hashtable();
        Panel thelblPnl = new Panel();
        thelblPnl.ID = "pnlheading" + pnlselectlab.ID;
        thelblPnl.Height = 20;
        thelblPnl.Width = 100;
        thelblPnl.Controls.Clear();

        Label theLabel = new Label();
        theLabel.ID = "lblheading" + pnlselectlab.ID;
        theLabel.Text = "Lab Test";
        theLabel.Font.Bold = true;
        thelblPnl.Controls.Add(theLabel);
        pnlselectlab.Controls.Add(thelblPnl);

        Label lblStSp = new Label();
        lblStSp.Width = 5;
        lblStSp.Text = "";
        lblStSp.Height = 20;
        pnlselectlab.Controls.Add(lblStSp);

        foreach (DataRow r in dtresult.Rows)
        {
            
            if (!(ht.ContainsKey("lblheading" + r["Department"].ToString())))
            {
                var rowcount = dtresult.Select("Department = '" + r["Department"].ToString() + "'");
                
                count= rowcount.Length.ToString();
                Panel thepnl = new Panel();
                thepnl.ID = "pnlsubheading" + r["Department"].ToString();
                thepnl.Height = 40 * rowcount.Length;
                thepnl.Width = 800;
              
                Label thesubheading = new Label();
                thesubheading.ID = "lblheading" + r["Department"].ToString();
                thesubheading.Text = r["Department"].ToString();
                thesubheading.Font.Bold = true;
                thesubheading.ForeColor = System.Drawing.Color.Blue;
                thepnl.Controls.Add(thesubheading);
                pnlselectlab.Controls.Add(thepnl);

                                
                ht.Add("lblheading" + r["Department"].ToString(), "lblheading" + r["Department"].ToString());


            }

                     
            
        }
        BindChildLabTest(dtresult, count);
    }
    public void BindChildLabTest(DataTable dtchild,string rowcount)
    {
        foreach (DataRow r in dtchild.Rows)
        {
            hdControlExists.Value = r["TestId"].ToString();
            Panel pnlctrl = (Panel)pnlselectlab.FindControl("pnlsubheading" + r["Department"].ToString());
            Panel pnlchildlab = new Panel();
            pnlchildlab.ID = "pnlchild" + r["TestId"].ToString();
            pnlchildlab.Height = 20;
            pnlchildlab.Width = 800;
            pnlchildlab.BorderWidth = 0;

            Label theSpace8 = new Label();
            theSpace8.ID = "theSpace8" + r["TestId"].ToString();
            theSpace8.Width = 10;
            theSpace8.Text = "";
            pnlchildlab.Controls.Add(theSpace8);

            Label thesubheading = new Label();
            thesubheading.ID = "lblheading" + r["TestId"].ToString();
            thesubheading.Text = r["TestName"].ToString();
            thesubheading.Width = 400;
            thesubheading.Font.Bold = true;
            pnlchildlab.Controls.Add(thesubheading);

            Label theSpace9 = new Label();
            theSpace9.ID = "theSpace9" + r["TestId"].ToString();
            theSpace9.Width = 100;
            theSpace9.Text = "";
            pnlchildlab.Controls.Add(theSpace9);

           

            LinkButton thelnkRemove = new LinkButton();
            thelnkRemove.ID = "lnkrmv%" + pnlchildlab.ID + "^" + r["TestId"].ToString();
            thelnkRemove.Width = 20;
            thelnkRemove.Text = "Remove";
            
            thelnkRemove.OnClientClick = "return removelink('" + pnlctrl.ClientID + "','" + pnlchildlab.ClientID + "','" + r["TestId"].ToString() + "','" + rowcount + "')";
            pnlchildlab.Controls.Add(thelnkRemove);

            pnlctrl.Controls.Add(pnlchildlab);

            
           
        }
        
    }
    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
      public string UpdateTable(string id)
      {
          string rowcount;
          if (dtLabTest.Rows.Count > 0)
          {
              for (int i = 0; i < dtLabTest.Rows.Count;i++ )
              {
                  if (dtLabTest.Rows[i]["TestId"].ToString() == id)
                  {
                      dtLabTest.Rows[i].Delete();
                  }
              }
              
          }
          dtLabTest.AcceptChanges();
          ViewState["LabTestID"] = dtLabTest;
          rowcount = dtLabTest.Rows.Count.ToString();
          return rowcount;
    
      }

    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public string  Save(string labtobedone,string laborder,string laborderdate,string appcurrdate)
    {
            string strresult = string.Empty;
            if (FieldValidation(labtobedone, laborderdate, laborder, appcurrdate) == false)
            {
                return "false";
            }
            
            DataTable LabTestIDs = LabTest();
            IQCareUtils theUtils = new IQCareUtils();
            ILabFunctions LabTestsManager;
            if (Convert.ToInt32(Session["Lab_ID"]) == 0)
            {
                int PatientID = Convert.ToInt32(Session["PatientId"]);
                int LocationID = Convert.ToInt32(Session["AppLocationId"]);
                LabTestsManager = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
                DataSet DSsavedLabID = (DataSet)LabTestsManager.SaveLabOrderTests(PatientID, LocationID, LabTestIDs, Convert.ToInt32(Session["AppUserId"]), Convert.ToInt32(laborder), laborderdate, Convert.ToString(Session["Lab_ID"]), 88, labtobedone);
                Session["Lab_ID"] = DSsavedLabID.Tables[0].Rows[0]["LabID"].ToString();
                ViewState["LabOther"] = DSsavedLabID.Tables[0];
                ViewState["LabTestID"] = DSsavedLabID.Tables[1];
                ViewState["TestDatetxt"] = laborderdate;

            }
            else
            {
                int PatientID = Convert.ToInt32(Session["PatientId"]);
                int LocationID = Convert.ToInt32(Session["ServiceLocationId"]);

                if (Convert.ToInt32(Session["Lab_ID"]) > 0)
                {
                    LabTestsManager = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
                    DataSet DSsavedLabID = (DataSet)LabTestsManager.SaveLabOrderTests(PatientID, LocationID, LabTestIDs, Convert.ToInt32(Session["AppUserId"]), Convert.ToInt32(laborder), laborderdate, Convert.ToString(Session["Lab_ID"]), 99, labtobedone);
                    Session["Lab_ID"] = DSsavedLabID.Tables[0].Rows[0]["LabID"].ToString();
                    ViewState["LabOther"] = DSsavedLabID.Tables[0];
                    ViewState["LabTestID"] = DSsavedLabID.Tables[1];
                    ViewState["TestDatetxt"] = laborderdate;

                }
                else
                {
                    LocationID = Convert.ToInt32(Session["AppLocationId"]);
                    LabTestsManager = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
                    DataSet DSsavedLabID = (DataSet)LabTestsManager.SaveLabOrderTests(PatientID, LocationID, LabTestIDs, Convert.ToInt32(Session["AppUserId"]), Convert.ToInt32(laborder), laborderdate, Convert.ToString(Session["Lab_ID"]), 88, labtobedone);
                    Session["Lab_ID"] = DSsavedLabID.Tables[0].Rows[0]["LabID"].ToString();
                    ViewState["LabOther"] = DSsavedLabID.Tables[0];
                    ViewState["LabTestID"] = DSsavedLabID.Tables[1];
                    ViewState["TestDatetxt"] = laborderdate;

                }
            }
            strresult = Session["Lab_ID"].ToString();
            return strresult;
    }
    private Boolean FieldValidation(string labtobedone,string orderbydate,string orderby,string appcurrentdate)
    {

        IQCareSecurity = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
        theCurrentDate = (DateTime)IQCareSecurity.SystemDate();
        IQCareUtils theUtils = new IQCareUtils();

        
        if (orderby == "0")
        {
            MsgBuilder theMsg = new MsgBuilder();
            theMsg.DataElements["Control"] = "Ordered By";
            IQCareMsgBox.Show("BlankDropDown", theMsg, this);
            return false;
        }
        else if (orderbydate == "")
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "Ordered By Date";
            IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
            return false;
        }
        else if (Convert.ToDateTime(orderbydate) > Convert.ToDateTime(appcurrentdate))
        {
            IQCareMsgBox.Show("OrderedToDate", this);
            return false;
        }
        //----------



        if (labtobedone != "")
        {
            theVisitDate = Convert.ToDateTime(theUtils.MakeDate(labtobedone));
            if (ViewState["IEVisitDate"] != null)
            {
                DateTime theIEVisitDate = Convert.ToDateTime(ViewState["IEVisitDate"].ToString());
                if (theIEVisitDate > theVisitDate)
                {
                    IQCareMsgBox.Show("CompareLabTobeDoneDate", this);
                    return false;
                }
                
            }
        }

        return true;
    }
    private DataTable LabTest()
    {
        DataTable dtLabTestFinal = new DataTable();
        dtLabTestFinal.Columns.Add("TestId", System.Type.GetType("System.Int32"));

        DataRow drLabTestfinal;

        for (int i = 0; i < dtLabTest.Rows.Count; i++)
        {
            drLabTestfinal = dtLabTestFinal.NewRow();
            drLabTestfinal["TestId"] = Convert.ToInt32(dtLabTest.Rows[i]["TestId"].ToString());
            dtLabTestFinal.Rows.Add(drLabTestfinal);


        }
        return dtLabTestFinal;

    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
        Page.RegisterClientScriptBlock("onclick", "<script language='javascript' type='text/javascript'>window.close();</script>");
    }

    protected void txtLabtobeDone_TextChanged(object sender, EventArgs e)
    {
        if (this.txtLabtobeDone.Text != "")
        {
            this.preclinicLabs.Checked = true;
        }
    }
}