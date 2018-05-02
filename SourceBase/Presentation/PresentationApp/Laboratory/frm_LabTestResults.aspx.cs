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
using System.Linq;

//Third party Libs
using Telerik.Web.UI;
using System.Data;
using System.IO;

namespace PresentationApp.Laboratory
{
    public partial class frm_LabTestResults : BasePage
    {
        public string PId, PtnSts, DQ;
        int LocationID;

        #region"Old Lab Variables"
        
        static DataSet dtPrevLabOrder;
        DataView theDV = new DataView();
        DataTable DTtestDate = new DataTable();
        IIQCareSystem IQCareSecurity;
        DateTime theCurrentDate;
        #endregion
        
        protected void Page_Init(object sender, EventArgs e)
        {
            BindList(Session["AppUserId"].ToString());
            txtSpecRecdt.SelectedDate = DateTime.Now;
            //txtTestInitdt.SelectedDate = DateTime.Now;
            
            if (!IsPostBack)
            {
                if (Session["LabNumber"] != null)
                    fillLabDetails();
                else
                    tabControl.ActiveTabIndex = 0;
            }
        }
        private void BindGrid(DataTable tbl)
        {
            BoundField theCol0 = new BoundField();
            theCol0.HeaderText = "ParameterID";
            theCol0.DataField = "ParameterID";
            theCol0.ItemStyle.Width = Unit.Percentage(7);           
            theCol0.ReadOnly = true;
            theCol0.Visible = false;

            BoundField theCol1 = new BoundField();
            theCol1.HeaderText = "Lab Test Name";
            theCol1.DataField = "SubTestName";
            theCol1.ItemStyle.Width = Unit.Percentage(60);
            theCol1.ReadOnly = true;

            BoundField theCol3 = new BoundField();
            theCol3.HeaderText = "Department Name";
            theCol3.DataField = "LabDepartmentName";
            theCol3.ItemStyle.Width = Unit.Percentage(30);
            theCol3.ReadOnly = true;

            gvLaborders.Columns.Add(theCol0);
            gvLaborders.Columns.Add(theCol1);
            gvLaborders.Columns.Add(theCol3);
            gvLaborders.DataSource = tbl;
            gvLaborders.DataBind();

        }
        private void BindRejectList(string name, DropDownList ddl, DropDownList ddldisable)
        {
            DataSet theDSXML = new DataSet();
            theDSXML.ReadXml(MapPath("..\\XMLFiles\\ALLMasters.con"));
            BindFunctions BindManager = new BindFunctions();

            if (theDSXML.Tables["Mst_Decode"] != null)
            {
                BindComboXML(theDSXML, name, ddl);
                if (name == "SpecimenStatus")
                {
                    ddl.Attributes.Add("OnChange", "SetEnableDisable('" + ddl.ClientID + "','Reject','" + ddldisable.ClientID + "');");
                }
            }
        }

        public void BindComboXML(DataSet theDSXML, string FieldName, DropDownList ddl)
        {
            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();
            DataView theDVCodeID = new DataView(theDSXML.Tables["Mst_Code"]);
            theDVCodeID.RowFilter = "Name='" + FieldName + "'";
            DataView theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
            theDV.RowFilter = "CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
            DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            BindManager.BindCombo(ddl, theDT, "Name", "ID");
        }
        private void BindList(String EmployeeId)
        {
            DataSet theDSXML = new DataSet();
            theDSXML.ReadXml(MapPath("..\\XMLFiles\\ALLMasters.con"));
            BindFunctions BindManager = new BindFunctions();

            //ddlmthroncotrimoxazole
            if (theDSXML.Tables["Mst_Decode"] != null)
            {
                BindComboXML(theDSXML, "SpecimenType", ddlspecimentype);
                BindComboXML(theDSXML, "SpecimenSource", ddlSpecSource);
                //BindComboXML(theDSXML, "SpecimenState", ddlState);
                //BindComboXML(theDSXML, "SpecimenStatus", ddlTestStatus);
                //ddlTestStatus.Attributes.Add("OnChange", "SetEnableDisable('" + ddlTestStatus.ClientID + "','Reject','" + ddlrejreason.ClientID + "');");
                //BindComboXML(theDSXML, "SpecimenRejectReason", ddlrejreason);

            }
            //if (theDSXML.Tables["mst_LabDepartment"] != null)
            //{                
            //    BindManager.BindCombo(ddldepartment, theDSXML.Tables["mst_LabDepartment"], "LabDepartmentName", "LabDepartmentID");
            //}
            IUser UserManager = (IUser)ObjectFactory.CreateInstance("BusinessProcess.Security.BUser,BusinessProcess.Security");
            DataTable theFacDT = UserManager.GetFacilityList();
            BindManager.BindCombo(ddlfromfacility, theFacDT, "FacilityName", "FacilityId");
            //BindUserDropdown(ddlrecivedby, string.Empty);
            BindUserDropdown(ddlrecivedby, Session["AppUserId"].ToString());

            //BindUserDropdown(ddlTestPerfby, string.Empty);


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
                }
            }
        }
        private void BindUserDropdown(RadComboBox DropDownID, String userId)
        {
            Dictionary<int, string> userList = new Dictionary<int, string>();
            CustomFieldClinical.BindUserDropDown(DropDownID, out userList);
            if (!string.IsNullOrEmpty(userId))
            {
                if (userList.ContainsKey(Convert.ToInt32(userId)))
                {
                    DropDownID.SelectedValue = userId;
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Laboratory >> ";
            //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Laboratory Results";
            //(Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Laboratory Results";

            #region "Refresh Patient Records"
            IPatientHome PManager;
            PManager = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
            System.Data.DataSet thePDS = PManager.GetPatientDetails(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["SystemId"]), Convert.ToInt32(Session["TechnicalAreaId"]));

            Session["PatientInformation"] = thePDS.Tables[0];
            #endregion
            IPatientHome PatientManager;
            PatientManager = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");

            try
            {
                if (!IsPostBack)
                {
                    IFacilitySetup FacilityMaster = (IFacilitySetup)ObjectFactory.CreateInstance("BusinessProcess.Administration.BFacility, BusinessProcess.Administration");
                    if (Session["PatientId"] == null || Convert.ToString(Session["PatientId"]) == "0")
                    {
                        Session["PatientId"] = Request.QueryString["PatientId"];  //remove it after session of patient set on find add when patient selected from grid.
                    }

                    PId = Convert.ToString(Session["PatientId"]);
                    PtnSts = Convert.ToString(Session["PatientStatus"]); //Request.QueryString["sts"].ToString();
                    if (Session["PatientId"] != null && Convert.ToInt32(Session["PatientId"]) != 0)
                    {
                        PId = Session["PatientId"].ToString();
                    }
                    if (Session["PatientStatus"] != null)
                    {
                        PtnSts = Session["PatientStatus"].ToString();
                    }
                    DataSet theDS = PatientManager.GetPatientHistory(Convert.ToInt32(PId));
                    ViewState["theCFDT"] = theDS.Tables[3].Copy();
                    FormIQCare(theDS);


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
        protected void Page_PreRender(object sender, EventArgs e)
        {


        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static void SaveCurrentTab(string controlId, int currentTabIndex)
        {
            //if (ViewState["LabNumber"] != null)
            //    fillLabDetails();
            //else
            //    tabControl.ActiveTabIndex = 0;
        }
        protected void TreeViewExisForm_SelectedNodeChanged(object sender, EventArgs e)
        {
            try
            {
                if (TreeViewExisForm.SelectedNode.Value == "")
                    return;

                string[] theName = TreeViewExisForm.SelectedNode.Text.Split('(');
                string[] theValue = TreeViewExisForm.SelectedNode.Value.Split('%');

                Session["PatientId"] = Convert.ToInt32(theValue[0]);
                Session["PatientVisitId"] = Convert.ToInt32(theValue[1]);
                Session["ServiceLocationId"] = Convert.ToInt32(theValue[2]);
                Session["PatientStatus"] = Convert.ToInt32(theValue[3]);
                Session["CEModule"] = theValue[4].ToString();
                Session["LabNumber"] = theValue[6].ToString();
                Session["TranDate"] = theValue[7].ToString();
                Session["ResultVisitId"] = theValue[8].ToString();

                AuthenticationManager Authentication = new AuthenticationManager();

                DataTable filteresdDT = new DataTable();
                if (!object.Equals(Session["FilteredUserRight"], null))
                    filteresdDT = (DataTable)Session["FilteredUserRight"];


                switch (theName[0].Trim())
                {
                    case "Order Labs"://Change Laboratory to Order Labs by Rahmat as on 26Feb2018
                        //url = string.Format("{0}", "~/./Laboratory/frmLabOrder.aspx");
                        Session["LabOrderID"] = Session["PatientVisitId"];                        
                        #region "Old Lab Data"
                        if (Session["LabOrderID"] != null)
                        {
                            DataTable dt = GetDataTable("LAB_STATUS", "", Convert.ToInt32(Session["LabOrderID"].ToString()));
                            Session["LabOrderStatus"] = dt.Rows[0]["LabStatus"].ToString();                           
                            //BindMasterData();
                            LoadBlankGrid();                            
                            BindLabTestGrid();
                            BindLabOrderDetails();
                            tabControl.ActiveTabIndex = 1;
                            fillLabDetails();                            
                            
                        }
                        else
                        {
                            Session["LabOrderStatus"] = "";                            
                        }

                        #endregion

                        break;

                    //default: break;

                }

                //fillLabDetails();
                //TreeViewExisForm.SelectedNode.NavigateUrl = url;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void FormIQCare(DataSet theDS)
        {
            IPatientHome PatientManager;
            PatientManager = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
            int tmpYear = 0;
            int tmpMonth = 0;
            TreeNode root = new TreeNode();
            TreeNode theMRoot = new TreeNode();
            bool flagyear = true;

            int PtnARTStatus = 0;
            if (PtnSts == "0" || PtnSts == "")
            {
                if (Session["PtnPrgStatus"] != null)
                {
                    DataTable theStatusDT = (DataTable)Session["PtnPrgStatus"];
                    DataTable theCEntedStatusDT = (DataTable)Session["CEndedStatus"];
                    string PatientExitReason = string.Empty;
                    string PMTCTCareEnded = string.Empty;
                    string CareEnded = string.Empty;
                    if (theCEntedStatusDT.Rows.Count > 0)
                    {
                        PatientExitReason = Convert.ToString(theCEntedStatusDT.Rows[0]["PatientExitReason"]);
                        PMTCTCareEnded = Convert.ToString(theCEntedStatusDT.Rows[0]["PMTCTCareEnded"]);
                        CareEnded = Convert.ToString(theCEntedStatusDT.Rows[0]["CareEnded"]);
                    }

                }
            }
            else
            {

                PtnARTStatus = 1;
            }

            foreach (DataRow theDR in theDS.Tables[4].Rows)
            {

                if (((DateTime)theDR["TranDate"]).Year != 1900)
                {
                    DQ = "";
                    if (tmpYear != ((DateTime)theDR["TranDate"]).Year)
                    {
                        root = new TreeNode();
                        root.Text = ((DateTime)theDR["TranDate"]).Year.ToString();
                        root.Value = "";
                        if (flagyear)
                        {
                            root.Expand();
                            flagyear = false;
                        }
                        else
                        {
                            root.Collapse();
                        }
                        TreeViewExisForm.Nodes.Add(root);
                        tmpYear = ((DateTime)theDR["TranDate"]).Year;
                        tmpMonth = 0;
                    }

                    if (tmpYear == ((DateTime)theDR["TranDate"]).Year && tmpMonth != ((DateTime)theDR["TranDate"]).Month)
                    {
                        theMRoot = new TreeNode();
                        theMRoot.Text = ((DateTime)theDR["TranDate"]).ToString("MMMM");
                        theMRoot.Value = "";
                        root.ChildNodes.Add(theMRoot);
                        tmpMonth = ((DateTime)theDR["TranDate"]).Month;
                    }

                    if (theDR["DataQuality"].ToString() == "1")
                    {
                        DQ = "Data Quality Done";

                    }

                    if (tmpYear == ((DateTime)theDR["TranDate"]).Year && tmpMonth == ((DateTime)theDR["TranDate"]).Month)
                    {
                        this.LocationID = Convert.ToInt32(theDS.Tables[0].Rows[0]["LocationID"].ToString());
                        TreeNode theFrmRoot = new TreeNode();
                        theFrmRoot.Text = theDR["FormName"].ToString() + " ( " + ((DateTime)theDR["TranDate"]).ToString(Session["AppDateFormat"].ToString()) + " )";

                        if (Convert.ToString(Session["TechnicalAreaId"]) == Convert.ToString(theDR["Module"]) || (theDR["FormName"].ToString() == "Order Labs"))//Change Laboratory to Order Labs by Rahmat as on 26Feb2018
                        {
                            if ((theDR["FormName"].ToString() == "Order Labs"))// Change Laboratory to Order Labs by Rahmat as on 26Feb2018
                            {
                                if (theDR["URGENT"].ToString() == "URGENT")
                                {
                                    if (theDR["CAUTION"].ToString() == "1")
                                    {
                                        theFrmRoot.ImageUrl = "~/images/caution_urgent.png";
                                    }
                                    else if (theDR["CAUTION"].ToString() == "2")
                                    {
                                        theFrmRoot.ImageUrl = "~/images/partial.png";
                                    }
                                    else
                                        theFrmRoot.ImageUrl = "~/images/15px-Yes_check.svg.png";
                                }
                                else
                                {
                                    if (theDR["CAUTION"].ToString() == "1")
                                    {
                                        theFrmRoot.ImageUrl = "~/images/caution.png";
                                    }
                                    else if (theDR["CAUTION"].ToString() == "2")
                                    {
                                        theFrmRoot.ImageUrl = "~/images/partial.png";
                                    }
                                    else
                                        theFrmRoot.ImageUrl = "~/images/15px-Yes_check.svg.png";
                                }
                            }

                        }
                        else
                        {
                            theFrmRoot.ImageUrl = "~/Images/lock.jpg";
                            theFrmRoot.ImageToolTip = "You are Not Authorized to Access this Functionality";
                            theFrmRoot.SelectAction = TreeNodeSelectAction.None;
                        }

                        theFrmRoot.NavigateUrl = "";
                        theFrmRoot.Value = Convert.ToInt32(PId) + "%" + theDR["OrderNo"].ToString() + "%" + theDR["LocationID"].ToString() + "%" + PtnARTStatus + "%" + theDR["Module"].ToString() + "%" + theDR["FormName"].ToString() + "%" + theDR["LabNumber"].ToString() + "%" + theDR["TranDate"].ToString() + "%" + theDR["ID"].ToString();
                        theMRoot.ChildNodes.Add(theFrmRoot);
                    }
                }

            }

        }    



        protected void ddlVisitType_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        protected void GrdNNHistory_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void GrdNNHistory_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }

        protected void GrdNNHistory_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void btnMMother_Click(object sender, EventArgs e)
        {

        }

        protected void btAddNNatal_Click(object sender, EventArgs e)
        {

        }

        protected void GrdMMHistory_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void GrdMMHistory_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }

        protected void GrdMMHistory_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
        }
        #region "Old Laboratory Form"

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static AutoCompleteBoxData GetLabsNames(object context)
        {
            string searchString = ((Dictionary<string, object>)context)["Text"].ToString();
            DataTable data = GetDataTable("LabTestID", "", searchString);
            List<AutoCompleteBoxItemData> result = new List<AutoCompleteBoxItemData>();

            foreach (DataRow row in data.Rows)
            {
                AutoCompleteBoxItemData childNode = new AutoCompleteBoxItemData();
                childNode.Text = row["LabName"].ToString();
                childNode.Value = row["LabTestID"].ToString();
                result.Add(childNode);
            }

            AutoCompleteBoxData res = new AutoCompleteBoxData();
            res.Items = result.ToArray();

            return res;
        }
        private void BindControls()
        {
            BindFunctions theBindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();
            DataSet theDSXML = new DataSet();
            DataTable theDT = new DataTable();
            theDSXML.ReadXml(Server.MapPath("..\\XMLFiles\\AllMasters.con"));
            //if (theDSXML.Tables["Mst_Employee"] != null)
            //{
            //    theDV = new DataView(theDSXML.Tables["Mst_Employee"]);
            //    theDV.RowFilter = "DeleteFlag=0";
            //    if (theDV.Table != null)
            //    {
            //        theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            //        if (Convert.ToInt32(Session["AppUserEmployeeId"]) > 0)
            //        {
            //            theDV = new DataView(theDT);
            //            theDV.RowFilter = "EmployeeId =" + Session["AppUserEmployeeId"].ToString();
            //            if (theDV.Count > 0)
            //                theDT = theUtils.CreateTableFromDataView(theDV);
            //        }

            //        theDV.Dispose();
            //        theDT.Clear();
            //    }

            //}
            IUser UserManager = (IUser)ObjectFactory.CreateInstance("BusinessProcess.Security.BUser,BusinessProcess.Security");
            DataTable theFacDT = UserManager.GetFacilityList();
            theBindManager.BindCombo(ddlfromfacility, theFacDT, "FacilityName", "FacilityId");

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
                    /* Bugid 2707
                    BindManager.BindCombo(ddlaborderedbyname, theDT, "EmployeeName", "EmployeeId");
                    */
                }

            }

        }
        private void BindDropdownReportedBy(String EmployeeId)
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

                    //BindManager.BindCombo(ddlaborderedbyname, theDT, "EmployeeName", "EmployeeId");

                   
                }

            }

        }




        protected void GetPreviousLabOrderTest()
        {
            ILabFunctions theILabManager;
            theILabManager = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
            dtPrevLabOrder = new DataSet();
            dtPrevLabOrder = theILabManager.GetPreviousOrderedLabs(Convert.ToInt32(Session["PatientId"]));

        }

        protected static DataTable GetDataTable(string flag, string labIds, string labName)
        {
            BIQTouchLabFields objLabFields = new BIQTouchLabFields();
            objLabFields.Flag = flag;
            objLabFields.LabTestIDs = labIds;
            objLabFields.LabTestName = labName;
            ILabFunctions theILabManager;
            theILabManager = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
            DataSet Ds = theILabManager.GetPatientLabTestID(objLabFields);
            DataTable dt = Ds.Tables[0];
            return dt;
        }
        protected void BindLabTestGrid()
        {
            if (Session["LabOrderID"] != null)
            {
                DataTable dt = null;
                //DataTable dt = GetDataTable("LabSubTestID", "", Convert.ToInt32(Session["LabOrderID"].ToString()));
                if (ViewState["tableLabSubTestID"] != null) { dt = (DataTable)ViewState["tableLabSubTestID"]; }
                else { dt = GetDataTable("LabSubTestID", "", Convert.ToInt32(Session["LabOrderID"])); }
                ViewState["tableLabSubTestID"] = dt;
                string filterExp = "DeleteFlag = 'N'";
                //RadGridLabTest.DataSource = dt;
                RadGridLabTest.DataSource = dt.Select(filterExp);
                RadGridLabTest.DataBind();
            }
        }
        protected void BindLabOrderDetails()
        {
            if (Session["LabOrderID"] != null)
            {
                //btnSave.Enabled = true;
                if (Session["LabOrderStatus"].ToString() == "Completed" && Request.QueryString["name"] != "Delete")
                {
                    BtnSaveLabResults.Enabled = false;
                }
            }

        }
        private Boolean FieldValidation()
        {

            IQCareSecurity = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
            theCurrentDate = (DateTime)IQCareSecurity.SystemDate();
            IQCareUtils theUtils = new IQCareUtils();

            return true;
        }


        protected void LoadBlankGrid()
        {
            if (RadGridLabTest.Items.Count == 0)
            {
                RadGridLabTest.DataSource = new Object[0];
            }

        }
        protected void RadGridLabTest_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridDataItem dataItm = e.Item as GridDataItem;
            RadGrid radGridArvMutation = (RadGrid)sender;

            Label lblLabSubTestID = (Label)dataItm.FindControl("lblLabSubTestID");
            string id = lblLabSubTestID.Text;
            DataTable table = (DataTable)ViewState["tableLabSubTestID"];//(DataTable)ViewState["tblLabtestID"]; make commented as row is not selected on first click...

            if (table != null)
            {
                table.PrimaryKey = new DataColumn[] { table.Columns["SubTestId"] };

                if (table.Rows.Find(id) != null)
                {
                    DataRow dr = table.Rows.Find(id);
                    //if (Session["LabOrderID"] != null)
                    //{
                    dr["DeleteFlag"] = "Y";
                    table.AcceptChanges();


                    var filter = from lab in table.AsEnumerable()
                                 where lab.Field<string>("DeleteFlag") == "N"
                                 select new
                                 {
                                     LabTestId = lab["LabTestId"],
                                     LabName = lab["LabName"],
                                     SubTestId = lab["SubTestId"],
                                     SubTestName = lab["SubTestName"],
                                     DefaultUnit = lab["DefaultUnit"],
                                     id = lab["id"],
                                     unitid = lab["unitid"],
                                     codeid = lab["codeid"],
                                     UnitName = lab["UnitName"],
                                     labDepartmentName = lab["labDepartmentName"],
                                     DeleteFlag = lab["DeleteFlag"]
                                 };
                    radGridArvMutation.DataSource = filter;
                    ViewState["softDeleteRecordsLabTestGrid"] = table;

                    //}
                    //else
                    //{
                    //    dr.Delete();
                    //    table.AcceptChanges();
                    //    radGridArvMutation.DataSource = table;
                    //    ViewState["tableLabSubTestID"] = table;
                    //}
                    ViewState["tblLabtestID"] = table;
                    ////Bug Id=170,171
                    //string[] preLab = hdnTestAddTestID.Value.Split(',');
                    //preLab = preLab.Where(s => s != id).ToArray();
                    //hdnTestAddTestID.Value = string.Join(",", preLab);
                    //string[] selectLab = hiddTestAddTestID.Value.Split(',');
                    //selectLab = selectLab.Where(s => s != id).ToArray();
                    //hiddTestAddTestID.Value = string.Join(",", selectLab);
                    //Bug End
                }
                else
                {
                    radGridArvMutation.DataSource = (DataTable)ViewState["tableLabSubTestID"];

                }
                radGridArvMutation.DataBind();
            }
        }



        private bool gridhasvalues(RadGrid radGridLabResult)
        {
            //if (Session["LabOrderID"] != null)
            //{
            DataTable dtGenXpert = new DataTable();
            //For GenXpert
            List<BIQTouchLabFields> listArv = new List<BIQTouchLabFields>();
            dtGenXpert = CreateDtGenXpertTable();

            foreach (GridDataItem item in radGridLabResult.Items)
            {
                Label lblLabSubTestId = (Label)item.FindControl("lblLabSubTestId");
                Label lblLabSubTestName = (Label)item.FindControl("lblLabTestName");
                Label lblundetectable = (Label)item.FindControl("lblundetectable");
                Label lblControlType = (Label)item.FindControl("lblControlType");
                RadioButtonList btnradRadioButtonList = (RadioButtonList)item.FindControl("btnRadRadiolist");
                Telerik.Web.UI.RadNumericTextBox txtRadValue = (Telerik.Web.UI.RadNumericTextBox)item.FindControl("txtRadValue");
                Telerik.Web.UI.RadTextBox txtAlphaRadValue = (Telerik.Web.UI.RadTextBox)item.FindControl("txtAlphaRadValue");
                CheckBoxList chkBoxList = (CheckBoxList)item.FindControl("chkBoxList");
                Telerik.Web.UI.RadComboBox ddlList = (RadComboBox)item.FindControl("ddlList");

                string strResuts = "0";

                if (lblundetectable.Text == "1")
                {
                    if (ddlList.SelectedValue.ToString() != "0")
                    {
                        //if((ddlList.SelectedValue.ToString() != "9999") && (txtRadValue.Text !=""))
                        return true;
                    }



                }
                else
                {
                    if (lblControlType.Text == "Radio")
                    {
                        if (btnradRadioButtonList.SelectedValue != "")
                        {
                            return true;
                        }
                    }
                    else if (lblControlType.Text == "Combo Box")
                    {

                        if (ddlList.SelectedValue.ToString() != "")
                        {
                            return true;
                        }

                    }
                    else if (lblControlType.Text == "Check box")
                    {

                        if (chkBoxList.SelectedValue.ToString() != "")
                        {
                            return true;
                        }
                    }
                    else if (lblControlType.Text == "GridView")
                    {
                        if (lblLabSubTestName.Text.ToUpper().Equals("ARV MUTATIONS"))
                        {
                            listArv = ArvMutationData(lblLabSubTestId.Text.ToString());
                            if (listArv.Count > 0)
                            {
                                return true;
                            }
                        }
                        else
                        {
                            DataRow dR = dtGenXpert.NewRow();
                            RadGrid radSubGridItems = (RadGrid)item.FindControl("RadGridArvMutation");
                            GridFooterItem radSubFooterItems = (GridFooterItem)radSubGridItems.MasterTableView.GetItems(GridItemType.Footer)[0];
                            Telerik.Web.UI.RadComboBox rcbFooterArvType = (Telerik.Web.UI.RadComboBox)radSubFooterItems.FindControl("rcbFooterArvType");
                            Telerik.Web.UI.RadComboBox rcbFooterMutation = (Telerik.Web.UI.RadComboBox)radSubFooterItems.FindControl("rcbFooterMutation");
                            Telerik.Web.UI.RadComboBox rcbFooterCulture = (Telerik.Web.UI.RadComboBox)radSubFooterItems.FindControl("rcbFooterCulture");
                            dR["LabId"] = Convert.ToInt32(Session["LabOrderID"]);
                            if (rcbFooterArvType.SelectedValue != "")
                            {
                                dR["ABFID"] = Convert.ToInt32(rcbFooterArvType.SelectedValue);
                                dR["ABFText"] = rcbFooterArvType.SelectedItem.Text;
                            }
                            if (rcbFooterMutation.SelectedValue != "")
                            {
                                dR["GeneXpertID"] = Convert.ToInt32(rcbFooterMutation.SelectedValue);
                                dR["GeneXpertText"] = rcbFooterMutation.SelectedItem.Text;
                            }
                            if (rcbFooterCulture.SelectedValue != "")
                            {
                                dR["CultSens"] = Convert.ToInt32(rcbFooterCulture.SelectedValue);
                                dR["CultSensText"] = rcbFooterCulture.SelectedItem.Text;
                            }
                            dR["ParameterID"] = Convert.ToInt32(lblLabSubTestId.Text);
                            dtGenXpert.Rows.Add(dR);
                            return true;
                        }
                    }
                    else
                    {

                        if (txtRadValue.Text != "0" && txtRadValue.Text != "")
                        {
                            strResuts = txtRadValue.Text.ToString();
                            return true;
                        }
                        else if (txtAlphaRadValue.Text != "")
                        {
                            strResuts = txtAlphaRadValue.Text.ToString();
                            return true;
                        }
                    }
                }

            }

            //}
            return false;

        }
        protected void RadGridLabTest_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.ExpandCollapseCommandName && e.Item is GridDataItem)
            {
                if (Session["LabOrderStatus"].ToString() != "")
                {
                    RadGrid detsGrid = (RadGrid)((GridDataItem)e.Item).ChildItem.FindControl("RadGridLabResult");
                    detsGrid.Visible = !e.Item.Expanded;

                    if (!e.Item.Expanded)
                    {
                        if ((Convert.ToInt32(Session["LabOrderID"]) > 0) && (gridhasvalues(detsGrid) == false))
                            detsGrid.Rebind();
                    }
                }

            }

        }

        protected string SelectedLabTest()
        {

            string labIdstr = "";
            string commastr = "";
            DataTable dt = null;
            if (Session["LabOrderID"] != null)
            {
                if (ViewState["tableLabSubTestID"] != null)
                {
                    dt = (DataTable)(ViewState["tableLabSubTestID"]);
                }
                else
                {
                    dt = GetDataTable("LabSubTestID", "", Convert.ToInt32(Session["LabOrderID"].ToString()));
                }
                string strFilter = "DeleteFlag = 'N'";
                DataRow[] collection = dt.Select(strFilter);
                if (collection.Length > 0)
                {
                    foreach (DataRow item in collection)
                    {
                        labIdstr = labIdstr + commastr + item.ItemArray[2];
                        commastr = ",";
                    }
                }
            }


            return labIdstr;


        }

        protected void RadGridLabTest_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                string labIdstr = SelectedLabTest();
                //hiddTestID.Value = "";
                DataTable dt = null;
                if (labIdstr != "")
                {
                    //DataTable dt = GetDataTable("LabSubTestID", labIdstr, "");
                    if (ViewState["tableLabSubTestID"] != null)
                    {
                        dt = (DataTable)(ViewState["tableLabSubTestID"]);
                        bool NoNeedToAdd = false;
                        foreach (DataRow item in dt.Rows)
                        {
                            if (labIdstr == item.ItemArray[2].ToString())
                            {
                                NoNeedToAdd = true;
                                break;
                            }
                        }
                        if (!NoNeedToAdd) { dt = GetDataTable("LabSubTestID", labIdstr, ""); }
                    }
                    else
                    {
                        dt = GetDataTable("LabSubTestID", labIdstr, "");
                    }
                    string filterExp = "DeleteFlag = 'N'";
                    RadGridLabTest.DataSource = dt.Select(filterExp);
                    //hiddTestID.Value = RadGridLabTest.Items.Count.ToString();
                    ViewState["tblLabtestID"] = dt;
                    ViewState["tableLabSubTestID"] = dt;
                }
            }
            catch (Exception ex)
            {
                //lblerr.Text = ex.Message.ToString();


            }
        }

        protected void RadGridLabTest_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridNestedViewItem)
            {
                e.Item.FindControl("RadGridLabResult").Visible = ((GridNestedViewItem)e.Item).ParentItem.Expanded;
                (e.Item.FindControl("RadGridLabResult") as RadGrid).NeedDataSource += new GridNeedDataSourceEventHandler(RadGridLabResult_NeedDataSource);


                RadGrid radGridLabResult = (RadGrid)e.Item.FindControl("RadGridLabResult");
                radGridLabResult.ItemCreated += new GridItemEventHandler(radGridLabResult_ItemCreated);
                radGridLabResult.ItemDataBound += new GridItemEventHandler(RadGridResut_ItemDataBound);


                //radGridLabResult.DataBind();

            }
        }
        protected void btnclose_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["opento"] == "ArtForm")
            {
                if (Convert.ToInt32(Session["ArtEncounterPatientVisitId"]) > 0)
                {
                    Session["PatientVisitId"] = Session["ArtEncounterPatientVisitId"];
                }
                Page.RegisterClientScriptBlock("onclick", "<script language='javascript' type='text/javascript'>Close();</script>");

            }
            else
            {
                string theUrl = string.Format("{0}", "../ClinicalForms/frmPatient_Home.aspx");
                Response.Redirect(theUrl, true);

            }

        }
        DateTime DateGiven(string dateVal)
        {
            DateTime dt = Convert.ToDateTime("01/01/1900");
            if (!string.IsNullOrEmpty(dateVal))
            {
                dt = DateTime.Parse(dateVal);
            }
            return dt;
        }
        private void SaveCancel()
        {
            int PatientID = Convert.ToInt32(Session["PatientId"]);
            IQCareMsgBox.NotifyAction("Laboratory Form saved successfully. Do you want to close?", "Laboratory Form", false, this, "window.location.href='../ClinicalForms/frmPatient_History.aspx?sts=" + 0 + "';");

            //string script = "var ans;\n";
            //script += "ans=window.confirm('Laboratory Form saved successfully. Do you want to close?');\n";
            //script += "if (ans==true)\n";
            //script += "{\n";
            //script += "window.location.href='../ClinicalForms/frmPatient_History.aspx';\n";
            //script += "}\n";
            //script += "else \n";
            //script += "{\n";
            //script += "window.location.href='frm_Laboratory.aspx'\n";
            //script += "}\n";
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirm", script, true);
        }
        protected void SaveLabOrder()
        {
            if (LabResultFieldValidation())
            {
                //Default code for User Control Load 
                DataTable dtGenXpert = new DataTable();

                List<BIQTouchLabFields> list = new List<BIQTouchLabFields>();
                List<BIQTouchLabFields> listArv = new List<BIQTouchLabFields>();
                DataTable TestIntList = CreateTestInittable();
                try
                {
                    // Asigning value for saving
                    IQCareUtils theUtils = new IQCareUtils();
                    BIQTouchLabFields objLabFields = new BIQTouchLabFields();
                    objLabFields.Ptnpk = Convert.ToInt32(Session["PatientID"]);
                    objLabFields.LocationId = Convert.ToInt32(Session["AppLocationId"].ToString());
                    objLabFields.UserId = Convert.ToInt32(Session["AppUserId"].ToString());
                    objLabFields.OrderedByName = Convert.ToInt32(0);
                    objLabFields.OrderedByDate = DateGiven("");

                    objLabFields.LabTestID = 0;
                    objLabFields.ReportedByDate = DateGiven(Convert.ToDateTime(Application["AppCurrentDate"]).ToString());
                    objLabFields.ReportedByName = Convert.ToInt32(Session["AppUserId"].ToString());
                    objLabFields.TestResults = "";
                    objLabFields.TestResultId = 0;
                    objLabFields.LabOrderId = 0;
                    objLabFields.SystemId = Convert.ToInt32(Session["SystemId"]);

                    objLabFields.PreClinicLabDate = DateGiven("");

                    if (Session["LabOrderID"] != null)
                    {
                        objLabFields.LabOrderId = Convert.ToInt32(Session["LabOrderID"].ToString());
                        objLabFields.IntFlag = 2;
                        //For GenXpert

                        if (ViewState["TblGenXpert"] == null)
                        {
                            dtGenXpert = CreateDtGenXpertTable();
                        }
                       
                        foreach (GridNestedViewItem nestedView in RadGridLabTest.MasterTableView.GetItems(GridItemType.NestedView))
                        {
                            RadGrid radGridLabResult = (RadGrid)nestedView.FindControl("RadGridLabResult");

                            foreach (GridDataItem item in radGridLabResult.Items)
                            {
                                Boolean entryFlag = false;
                                Label lblLabSubTestId = (Label)item.FindControl("lblLabSubTestId");
                                Label lblLabSubTestName = (Label)item.FindControl("lblLabTestName");
                                //Label lblLabTestID = (Label)item.FindControl("lblLabTestID");

                                Label lblControlType = (Label)item.FindControl("lblControlType");
                                RadioButtonList btnradRadioButtonList = (RadioButtonList)item.FindControl("btnRadRadiolist");
                                Telerik.Web.UI.RadNumericTextBox txtRadValue = (Telerik.Web.UI.RadNumericTextBox)item.FindControl("txtRadValue");
                                Telerik.Web.UI.RadTextBox txtAlphaRadValue = (Telerik.Web.UI.RadTextBox)item.FindControl("txtAlphaRadValue");
                                CheckBoxList chkBoxList = (CheckBoxList)item.FindControl("chkBoxList");
                                RadComboBox ddlList = (RadComboBox)item.FindControl("ddlList");
                                DropDownList ddlRadReportedby = (DropDownList)item.FindControl("ddlRadReportedby");
                                RadDateTimePicker txtReportLabDate = (RadDateTimePicker)item.FindControl("txtReportLabDate");
                                BIQTouchLabFields objLabFields1 = new BIQTouchLabFields();
                                //string strResuts = "0"; commented by Jayant 30-03-2015
                                string strResuts = "";
                                int intRestutID = 0;
                                Label lblundetectable = (Label)item.FindControl("lblundetectable");
                                if (lblundetectable.Text == "1")
                                {
                                    entryFlag = true;
                                    if (ddlList.SelectedValue != string.Empty)
                                    {
                                        intRestutID = Convert.ToInt32(ddlList.SelectedValue.ToString());
                                    }
                                    if (txtRadValue.Text != "0" && txtRadValue.Text != "")
                                    {
                                        strResuts = txtRadValue.Text.ToString();
                                    }
                                }
                                else
                                {
                                    if (lblControlType.Text == "Radio")
                                    {
                                        if (btnradRadioButtonList.SelectedValue != "")
                                        {
                                            entryFlag = true;
                                            strResuts = lblLabSubTestId.Text;
                                            intRestutID = Convert.ToInt32(btnradRadioButtonList.SelectedValue.ToString());
                                        }
                                    }
                                    else if (lblControlType.Text == "Combo Box")
                                    {

                                        if (ddlList.SelectedValue.ToString() != "")
                                        {
                                            entryFlag = true;
                                            intRestutID = Convert.ToInt32(ddlList.SelectedValue.ToString());
                                            strResuts = ddlList.SelectedValue;
                                            //objLabFields1.TestResultId = Convert.ToInt32(ddlList.SelectedValue.ToString());
                                        }

                                    }
                                    else if (lblControlType.Text == "Check box")
                                    {

                                        if (chkBoxList.SelectedValue.ToString() != "")
                                        {
                                            entryFlag = true;
                                            intRestutID = Convert.ToInt32(chkBoxList.SelectedValue.ToString());
                                            strResuts = chkBoxList.SelectedValue;
                                            //objLabFields1.TestResultId = Convert.ToInt32(chkBoxList.SelectedValue.ToString());
                                        }
                                    }
                                    else if (lblControlType.Text == "GridView")
                                    {
                                        if (lblLabSubTestName.Text.ToUpper().Equals("ARV MUTATIONS"))
                                        {
                                            listArv = ArvMutationData(lblLabSubTestId.Text.ToString());// Code Here
                                            entryFlag = true;
                                        }
                                        else
                                        {
                                            DataRow dR = dtGenXpert.NewRow();
                                            RadGrid radSubGridItems = (RadGrid)item.FindControl("RadGridArvMutation");
                                            GridFooterItem radSubFooterItems = (GridFooterItem)radSubGridItems.MasterTableView.GetItems(GridItemType.Footer)[0];
                                            Telerik.Web.UI.RadComboBox rcbFooterArvType = (Telerik.Web.UI.RadComboBox)radSubFooterItems.FindControl("rcbFooterArvType");
                                            Telerik.Web.UI.RadComboBox rcbFooterMutation = (Telerik.Web.UI.RadComboBox)radSubFooterItems.FindControl("rcbFooterMutation");
                                            Telerik.Web.UI.RadComboBox rcbFooterCulture = (Telerik.Web.UI.RadComboBox)radSubFooterItems.FindControl("rcbFooterCulture");
                                            dR["LabId"] = Convert.ToInt32(Session["LabOrderID"].ToString());
                                            if (rcbFooterArvType.SelectedValue != "")
                                            {
                                                dR["ABFID"] = Convert.ToInt32(rcbFooterArvType.SelectedValue);
                                                dR["ABFText"] = rcbFooterArvType.SelectedItem.Text;
                                            }
                                            if (rcbFooterMutation.SelectedValue != "")
                                            {
                                                dR["GeneXpertID"] = Convert.ToInt32(rcbFooterMutation.SelectedValue);
                                                dR["GeneXpertText"] = rcbFooterMutation.SelectedItem.Text;
                                            }
                                            if (rcbFooterCulture.SelectedValue != "")
                                            {
                                                dR["CultSens"] = Convert.ToInt32(rcbFooterCulture.SelectedValue);
                                                dR["CultSensText"] = rcbFooterCulture.SelectedItem.Text;
                                            }
                                            dR["ParameterID"] = Convert.ToInt32(lblLabSubTestId.Text);
                                            dtGenXpert.Rows.Add(dR);
                                            ViewState["TblGenXpert"] = dtGenXpert;
                                        }
                                    }
                                    else
                                    {
                                        entryFlag = true;
                                        if (txtRadValue.Text != "")
                                        {
                                            strResuts = txtRadValue.Text.ToString();
                                        }
                                        else if (txtAlphaRadValue.Text != "")
                                        {
                                            strResuts = txtAlphaRadValue.Text.ToString();
                                        }
                                    }
                                }
                                objLabFields1.TestResults = strResuts;
                                objLabFields1.TestResultId = intRestutID;
                                objLabFields1.Ptnpk = Convert.ToInt32(Session["PatientID"]);
                                objLabFields1.LabTestIDs = "0";
                                objLabFields1.LabTestName = "";
                                objLabFields1.LocationId = Convert.ToInt32(Session["AppLocationId"].ToString());
                                objLabFields1.UserId = Convert.ToInt32(Session["AppUserId"].ToString());
                                objLabFields1.OrderedByName = Convert.ToInt32(0);
                                objLabFields1.OrderedByDate = DateGiven("");
                                objLabFields1.IntFlag = 3;
                                objLabFields1.LabTestID = 0;
                                objLabFields1.SubTestID = Convert.ToInt32(lblLabSubTestId.Text.ToString());
                                objLabFields1.PreClinicLabDate = DateGiven("");
                                objLabFields1.LabOrderId = Convert.ToInt32(Session["LabOrderID"].ToString());
                                objLabFields1.ReportedByDate = DateGiven(Convert.ToDateTime(Application["AppCurrentDate"]).ToString());
                                objLabFields1.ReportedByName = Convert.ToInt32(Session["AppUserId"].ToString());
                                if (txtReportLabDate.SelectedDate != null)
                                    objLabFields1.LabReportByDate = Convert.ToDateTime(txtReportLabDate.SelectedDate);
                                else
                                    objLabFields1.LabReportByDate = DateGiven("");
                                if (ddlRadReportedby.SelectedValue.ToString() != "")
                                    objLabFields1.LabReportByName = Convert.ToInt32(ddlRadReportedby.SelectedValue);
                                else
                                    objLabFields1.LabReportByName = 0;
                                objLabFields1.Flag = GetSubTestIDDeleteFlag(lblLabSubTestId.Text.ToString());
                                //}
                                if (entryFlag == true)
                                {
                                    list.Add(objLabFields1);
                                }
                                DropDownList ddlselectspecimen = (DropDownList)item.FindControl("ddlselectspecimen");
                                TextBox txtCustSpecNo = (TextBox)item.FindControl("txtCustSpecNo");
                                CheckBox chkCustSpec = (CheckBox)item.FindControl("chkCustSpec");                                
                                DropDownList ddlState = (DropDownList)item.FindControl("ddlState");                                
                                DropDownList ddlTestStatus = (DropDownList)item.FindControl("ddlTestStatus");
                                DropDownList ddlrejreason = (DropDownList)item.FindControl("ddlrejreason");
                                TextBox txtrejreason = (TextBox)item.FindControl("txtrejreason");
                                CheckBox chkConfirm = (CheckBox)item.FindControl("chkConfirm");
                                DropDownList ddlconfirmedby = (DropDownList)item.FindControl("ddlconfirmedby");
                                if (chkConfirm.Checked)
                                    objLabFields1.Confirmed = 1;
                                else
                                    objLabFields1.Confirmed = 0;
                                objLabFields1.Confirmedby = Convert.ToInt32(ddlconfirmedby.SelectedValue);
                                DataRow thetestinitdr = TestIntList.NewRow();                                
                                thetestinitdr["LabID"] = Convert.ToInt32(Session["LabOrderID"].ToString());
                                thetestinitdr["LabTestID"] = Convert.ToInt32(lblLabSubTestId.Text.ToString());
                                thetestinitdr["SpecimenID"] = Convert.ToInt32(ddlselectspecimen.SelectedValue);
                                
                                if(chkCustSpec.Checked)                               
                                    thetestinitdr["CustomSpecimenName"] = txtCustSpecNo.Text;
                                else
                                    thetestinitdr["CustomSpecimenName"] = "";                                
                                                                
                                thetestinitdr["StateId"] = Convert.ToInt32(ddlState.SelectedValue);                                
                                thetestinitdr["StatusId"] = Convert.ToInt32(ddlTestStatus.SelectedValue);                                
                                thetestinitdr["RejectedReasonId"] = Convert.ToInt32(ddlrejreason.SelectedValue);                                
                                thetestinitdr["OtherReason"] = txtrejreason.Text;                                
                                TestIntList.Rows.Add(thetestinitdr);
                                TestIntList.AcceptChanges();

                            }
                        }
                    }
                    ILabFunctions theILabManager;
                    theILabManager = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
                    //int count = 0;
                    //for (int i = 0; i < list.Count; i++)
                    //{
                    //    if ((!String.IsNullOrEmpty(list[i].TestResults)) || (list[i].TestResultId > 0))
                    //    {
                    //        count = count + 1;
                    //    }
                    //}


                    //    if (count == 0)
                    //    {
                    //        //IQCareMsgBox.ShowforUpdatePanel("FillLabResults", this);
                    //        MsgBuilder theBuilder = new MsgBuilder();
                    //        theBuilder.DataElements["MessageText"] = "Please Fill atleast one Lab Result.";
                    //        IQCareMsgBox.Show("#C1", theBuilder, this);                        
                    //        return;
                    //    }

                    DataTable theCustomDataDT = new DataTable();
                    int result = theILabManager.IQTouchSaveLabOrderTests(objLabFields, list, listArv, dtGenXpert, theCustomDataDT, TestIntList);
                    if (result > 0)
                    {
                        SaveResultCancel("Lab Result");
                    }

                }
                catch (Exception ex)
                {

                }
                finally
                {

                }
            }
        }

        protected string GetSubTestIDDeleteFlag(string subTestID)
        {
            DataTable table = (DataTable)ViewState["tableLabSubTestID"];
            table.PrimaryKey = new DataColumn[] { table.Columns["subTestID"] };
            string deleteFlag = "N";

            if (table.Rows.Find(subTestID) != null)
            {
                DataRow dr = table.Rows.Find(subTestID);
                deleteFlag = dr["DeleteFlag"].ToString();

            }
            return deleteFlag;


        }

        protected List<BIQTouchLabFields> ArvMutationData(string subtestID)
        {
            List<BIQTouchLabFields> listArv = new List<BIQTouchLabFields>();

            if (ViewState["TblArvMutation"] != null)
            {
                DataTable dt = (DataTable)ViewState["TblArvMutation"];
                foreach (DataRow dr in dt.Rows)
                {

                    BIQTouchLabFields obj1 = new BIQTouchLabFields();
                    obj1.UserId = Int32.Parse(Session["AppUserId"].ToString());
                    obj1.LabOrderId = Convert.ToInt32(Session["LabOrderID"]);
                    obj1.SubTestID = Convert.ToInt32(subtestID);
                    obj1.MutationID = Convert.ToInt32(dr["ArvMutationID"].ToString());
                    obj1.OtherMutation = dr["ArvMutationOther"].ToString();
                    obj1.ArvTypeID = Convert.ToInt32(dr["ArvTypeID"].ToString());

                    if (GetSubTestIDDeleteFlag(subtestID) == "Y")
                    {
                        obj1.Flag = "X";  // Removing Parameter ID from arvMutation Table when user deleting testID from parent grid
                    }
                    else
                    {
                        obj1.Flag = dr["DeleteFlag"].ToString();
                    }
                    listArv.Add(obj1);

                }
            }
            return listArv;

        }

        protected void RadGridArvMutation_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                RadComboBox combo = (RadComboBox)sender;
                GridFooterItem footeritem = (GridFooterItem)combo.NamingContainer;
                RadComboBox rcbFooterMutation = (RadComboBox)footeritem.FindControl("rcbFooterMutation");
                rcbFooterMutation.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(rcbFooterMutation_SelectedIndexChanged);
            }
           
        }
        protected void radGridLabResult_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {


            if (e.Item is GridDataItem)
            {
                //TextBox tb = new TextBox();
                //tb.ID = tbID;
                //tb.EnabeViewState = true;
                //gdi["column name"].controls.add(tb);




                //RadComboBox combo = ((RadComboBox)e.Item.FindControl("rdcmbfrequency"));
                //combo.DataSource = ((DataTable)Session["Frequency"]);
                //combo.DataValueField = "FrequencyId";
                //combo.DataTextField = "FrequencyName";
                //combo.DataBind();
            }
            //RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "JumpToGrid", "document.location = '#sGrid';", true);
        }
        protected DataTable GetDataTable(string flag, string labtestids, int LabOrderID)
        {

            BIQTouchLabFields objLabFields = new BIQTouchLabFields();
            objLabFields.Flag = flag;
            objLabFields.LabTestIDs = labtestids;
            objLabFields.LabOrderId = LabOrderID;
            objLabFields.LabTestName = "";

            ILabFunctions theILabManager;
            theILabManager = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
            DataSet Ds = theILabManager.GetPatientLabTestID(objLabFields);
            DataTable dt = Ds.Tables[0];
            return dt;



        }

        protected void RadGridLabResult_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

            try
            {
                GridDataItem parentItem = ((sender as RadGrid).NamingContainer as GridNestedViewItem).ParentItem as GridDataItem;

                Label lblID = (Label)parentItem.FindControl("lblLabSubTestID");
                //lblID
                DataTable dt = GetDataTable("QRY_CHILDGRID", lblID.Text, Convert.ToInt32(Session["LabOrderID"]));
                (sender as RadGrid as RadGrid).DataSource = dt;//new Object[0];


                //(sender as RadGrid as RadGrid).DataBind();
                //RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "JumpToGrid", "document.location = '#sGrid';", true);

            }
            catch (Exception ex)
            {
                //lblerr.Text = ex.Message.ToString();


            }
        }

        protected void BindRadioButtonList(RadioButtonList rbList, string labSubTestID)
        {
            DataTable dt = GetDataTable("QRY_CHILDRB", labSubTestID, 0);
            rbList.DataSource = dt;
            rbList.DataTextField = "Result";
            rbList.DataValueField = "ResultID";
            rbList.DataBind();


        }
        protected void BindDropdownist(RadComboBox rbList, string labSubTestID)
        {
            DataTable dt = GetDataTable("QRY_CHILDRB", labSubTestID, 0);
            rbList.DataSource = dt;
            rbList.DataTextField = "Result";
            rbList.DataValueField = "ResultID";
            rbList.DataBind();
            rbList.Items.Insert(0, new RadComboBoxItem("", "9997"));

        }
        protected void BindCheckBoxList(CheckBoxList rbList, string labSubTestID)
        {
            DataTable dt = GetDataTable("QRY_CHILDRB", labSubTestID, 0);
            rbList.DataSource = dt;
            rbList.DataTextField = "Result";
            rbList.DataValueField = "ResultID";
            rbList.DataBind();


        }
        protected DataTable GetArvMutationDataTable(string flag)
        {
            BIQTouchLabFields objLabFields = new BIQTouchLabFields();
            objLabFields.Flag = flag;
            ILabFunctions theILabManager;
            theILabManager = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
            DataSet Ds = theILabManager.IQTouchLaboratoryGetArvMutationMasterList(objLabFields);
            DataTable dt = Ds.Tables[0];
            return dt;
        }

        protected DataTable GetArvMutationGrid(string flag)
        {
            BIQTouchLabFields objLabFields = new BIQTouchLabFields();
            objLabFields.Flag = flag;
            objLabFields.LabOrderId = Convert.ToInt32(Session["LabOrderID"]);

            ILabFunctions theILabManager;
            theILabManager = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
            DataSet Ds = theILabManager.IQTouchLaboratoryGetArvMutationDetails(objLabFields);
            DataTable dt = Ds.Tables[0];
            return dt;
        }

        protected DataTable GetGenXpertGrid(string flag, int TestId)
        {

            BIQTouchLabFields objLabFields = new BIQTouchLabFields();
            objLabFields.Flag = flag;
            //if (Session["LabOrderID"] != null)
            objLabFields.LabOrderId = Convert.ToInt32(Session["LabOrderID"]);
            //else
            //    objLabFields.LabOrderId = 0;

            ILabFunctions theILabManager;
            theILabManager = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
            DataSet Ds = theILabManager.IQTouchLaboratoryGetGenXpertDetails(objLabFields, TestId);
            DataTable dt = Ds.Tables[0];
            return dt;
        }


        protected void BindArvType(RadComboBox rcb)
        {

            DataTable dt = GetArvMutationDataTable("ARV_TYPE");
            rcb.DataSource = dt;
            rcb.DataTextField = "ITEM_NAME";
            rcb.DataValueField = "ID";
            rcb.DataBind();




        }
        protected void BindArvMutation(RadComboBox rcb, Int32 ArvTypeID)
        {

            BIQTouchLabFields objLabFields = new BIQTouchLabFields();
            objLabFields.Flag = "ARV_MUTATION";
            objLabFields.ArvTypeID = ArvTypeID;
            ILabFunctions theILabManager;
            theILabManager = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
            DataSet Ds = theILabManager.IQTouchLaboratoryGetArvMutationMasterList(objLabFields);
            DataTable dt = Ds.Tables[0];
            rcb.DataSource = dt;
            rcb.DataTextField = "ITEM_NAME";
            rcb.DataValueField = "ID";
            rcb.DataBind();
        }
        protected void RadGridResut_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem && e.Item.OwnerTableView.Name == "ChildGrid")
            {
                GridDataItem item = (GridDataItem)e.Item;
                Label lblLabSubTestId = (Label)item.FindControl("lblLabSubTestId");
                Label lblLabSubTestName = (Label)item.FindControl("lblLabTestName");
                Label lblControlType = (Label)item.FindControl("lblControlType");
                Label lblundetectable = (Label)item.FindControl("lblundetectable");
                Label lblUnitName = (Label)item.FindControl("lblUnitName");
                Label lblMinBoundaryVal = (Label)item.FindControl("lblMinBoundaryVal");
                Label lblMaxBoundaryVal = (Label)item.FindControl("lblMaxBoundaryVal");
                Label lblTestResultId = (Label)item.FindControl("lblTestResultId");
                Label lblResultDate = (Label)item.FindControl("lblResultDate");
                Label lblResultby = (Label)item.FindControl("lblResultby");

                Label lblSpecimenID = (Label)item.FindControl("lblSpecimenID");
                Label lblCustomSpecimenName = (Label)item.FindControl("lblCustomSpecimenName");
                Label lblStateId = (Label)item.FindControl("lblStateId");
                Label lblStatusId = (Label)item.FindControl("lblStatusId");
                Label lblRejectedReasonId = (Label)item.FindControl("lblRejectedReasonId");
                Label lblOtherReason = (Label)item.FindControl("lblOtherReason");
                Label lblConfirmed = (Label)item.FindControl("lblConfirmed");
                Label lblConfirmedby = (Label)item.FindControl("lblConfirmedby");

                DropDownList ddlState = (DropDownList)item.FindControl("ddlState");
                BindRejectList("SpecimenState", ddlState, null);
                DropDownList ddlTestStatus = (DropDownList)item.FindControl("ddlTestStatus");
                DropDownList ddlrejreason = (DropDownList)item.FindControl("ddlrejreason");
                BindRejectList("SpecimenStatus", ddlTestStatus, ddlrejreason);
                BindRejectList("SpecimenRejectReason", ddlrejreason, null);
                DropDownList ddlconfirmedby = (DropDownList)item.FindControl("ddlconfirmedby");
                //BindUserDropdown(ddlconfirmedby, string.Empty);
                BindUserDropdown(ddlconfirmedby, Session["AppUserId"].ToString());
                if (Session["LabOrderID"] != null)
                {
                    ILabFunctions theILabManager = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");                   
                    DataSet theLabDS = theILabManager.GetOrderedLabs(Convert.ToInt32(Session["LabOrderID"]));
                    
                    foreach (DataRow theLabDR in theLabDS.Tables[4].Rows)
                    {
                        
                        if (lblLabSubTestId.Text == theLabDR["LabTestId"].ToString())
                        {
                            BindFunctions thebind = new BindFunctions();
                            DropDownList ddlselectspecimen = (DropDownList)item.FindControl("ddlselectspecimen");
                            thebind.BindCombo(ddlselectspecimen, theLabDS.Tables[2], "SpecimenName", "ID");
                            TextBox txtCustSpecNo = (TextBox)item.FindControl("txtCustSpecNo");
                            if (lblSpecimenID.Text != "" && lblSpecimenID.Text == theLabDR["SpecimenId"].ToString())
                            {
                                ddlselectspecimen.SelectedValue = lblSpecimenID.Text;
                            }
                            else
                                ddlselectspecimen.SelectedValue = "0";

                            HtmlGenericControl divFinal = (HtmlGenericControl)item.FindControl("divctx");
                            CheckBox chkCustSpec = (CheckBox)item.FindControl("chkCustSpec");
                            chkCustSpec.Attributes.Add("onchange", "fnCustSpecihide(" + chkCustSpec.ClientID + "," + divFinal.ClientID + ");");
                            if (lblCustomSpecimenName.Text != "")
                            {
                                chkCustSpec.Checked = true;
                                txtCustSpecNo.Text = lblCustomSpecimenName.Text;
                                ScriptManager.RegisterStartupScript(this, GetType(), "CustSpecihide", "fnCustSpecihide('" + chkCustSpec.ClientID + "','" + divFinal.ClientID + "');", true);
                            }
                            
                            if (lblStateId.Text != "")
                            {
                                ddlState.SelectedValue = lblStateId.Text;
                            }
                            
                            if (lblStatusId.Text != "")
                            {
                                ddlTestStatus.SelectedValue = lblStatusId.Text;
                            }
                            if (lblRejectedReasonId.Text != "")
                            {
                                ddlrejreason.SelectedValue = lblRejectedReasonId.Text;
                            }
                            CheckBox chkConfirm = (CheckBox)item.FindControl("chkConfirm");
                            
                            chkConfirm.Attributes.Add("onchange", "fnConfirmEnable(" + chkConfirm.ClientID + "," + ddlconfirmedby.ClientID + ");");
                            if (lblConfirmed.Text != "")
                            {
                                if (lblConfirmed.Text == "1")
                                {
                                    chkConfirm.Checked = true;
                                    if (lblConfirmedby.Text != "")
                                    {
                                        ddlconfirmedby.SelectedValue = lblConfirmedby.Text;
                                    }
                                }
                            }
                        }
                    }
                    
                }

                //Telerik.Web.UI.RadButton btnradRadioButtonList = (Telerik.Web.UI.RadButton)item.FindControl("btnRadRadiolist");
                RadioButtonList btnradRadioButtonList = (RadioButtonList)item.FindControl("btnRadRadiolist");
                Telerik.Web.UI.RadNumericTextBox txtRadValue = (Telerik.Web.UI.RadNumericTextBox)item.FindControl("txtRadValue");
                Telerik.Web.UI.RadTextBox txtAlphaRadValue = (Telerik.Web.UI.RadTextBox)item.FindControl("txtAlphaRadValue");
                Label lblresult = (Label)item.FindControl("lblTestResults");
                CheckBoxList chkBoxList = (CheckBoxList)item.FindControl("chkBoxList");
                Telerik.Web.UI.RadComboBox ddlList = (RadComboBox)item.FindControl("ddlList");
                RadGrid radGridArvMutation = (RadGrid)item.FindControl("RadGridArvMutation");
                DropDownList ddlRadReportedby = (DropDownList)item.FindControl("ddlRadReportedby");
                Telerik.Web.UI.RadDateTimePicker txtResultDate = (RadDateTimePicker)item.FindControl("txtReportLabDate");
                txtResultDate.SelectedDate = DateTime.Now;
                //BindUserDropdown(ddlRadReportedby, string.Empty);
                BindUserDropdown(ddlRadReportedby, Session["AppUserId"].ToString());
                if (lblResultby.Text != "")
                {
                    BindUserDropdown(ddlRadReportedby, lblResultby.Text);
                }
                if (lblResultDate.Text != "" && (Convert.ToDateTime(lblResultDate.Text).Year !=1900))
                {
                    txtResultDate.SelectedDate = Convert.ToDateTime(lblResultDate.Text);
                }
                lblUnitName.Visible = false;
                txtRadValue.Visible = false;
                txtAlphaRadValue.Visible = false;
                btnradRadioButtonList.Visible = false;
                chkBoxList.Visible = false;
                ddlList.Visible = false;
                radGridArvMutation.Visible = false;
                if (lblundetectable.Text == "1")
                {
                    //BindDropdownist(ddlList, lblLabSubTestId.Text);
                    ddlList.Items.Clear();
                    ddlList.Items.Add(new RadComboBoxItem("Select", "0"));
                    ddlList.Items.Add(new RadComboBoxItem("", "9997"));
                    ddlList.Items.Add(new RadComboBoxItem("Detectable", "9999"));
                    ddlList.Items.Add(new RadComboBoxItem("Undetectable", "9998"));

                    //ddlList.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(ddllist_SelectedIndexChanged);
                    if (Convert.ToInt32(lblTestResultId.Text.ToString()) > 0)
                    {
                        int index = ddlList.FindItemIndexByValue(lblTestResultId.Text);
                        ddlList.SelectedIndex = index;
                        //ddlList.Selected = lblTestResultId.Text;
                        if (lblTestResultId.Text == "9999")
                        {
                            txtRadValue.Text = lblresult.Text;
                            txtRadValue.Visible = true;
                            lblUnitName.Visible = true;
                        }
                    }

                    ddlList.Visible = true;

                }
                else
                {

                    if (lblControlType.Text == "Radio")
                    {
                        BindRadioButtonList(btnradRadioButtonList, lblLabSubTestId.Text);
                        if (Convert.ToInt32(lblTestResultId.Text.ToString()) > 0)
                        {
                            btnradRadioButtonList.SelectedValue = lblTestResultId.Text;
                        }

                        btnradRadioButtonList.Visible = true;

                    }
                    else if (lblControlType.Text == "Combo Box")
                    {
                        BindDropdownist(ddlList, lblLabSubTestId.Text);
                        if (Convert.ToInt32(lblTestResultId.Text.ToString()) > 0)
                        {
                            ddlList.SelectedValue = lblTestResultId.Text;
                        }

                        ddlList.Visible = true;
                    }
                    else if (lblControlType.Text == "Check box")
                    {
                        BindCheckBoxList(chkBoxList, lblLabSubTestId.Text);
                        if (Convert.ToInt32(lblTestResultId.Text.ToString()) > 0)
                        {
                            chkBoxList.SelectedValue = lblTestResultId.Text;
                        }
                        chkBoxList.Visible = true;
                    }
                    else if (lblControlType.Text == "GridView")
                    {
                        radGridArvMutation.Visible = true;
                        //if (lblLabSubTestId.Text.ToString() == "17" || lblLabSubTestId.Text.ToString() == "18" || lblLabSubTestId.Text.ToString() == "19" || lblLabSubTestId.Text.ToString() == "131")
                        //{
                        if (lblLabSubTestName.Text.ToString().ToUpper().Contains("SPUTUM AFB") || lblLabSubTestName.Text.ToString().ToUpper().Equals("GENEXPERT"))
                        {

                            txthdnfield.Value = lblLabSubTestName.Text;
                            radGridArvMutation.Columns[0].HeaderText = "ABF";
                            radGridArvMutation.Columns[1].HeaderText = "GeneXpert";
                            radGridArvMutation.Columns[3].Visible = false;
                            radGridArvMutation.Columns[4].Visible = false;
                            DataTable dt = GetGenXpertGrid("GENXPERT", Convert.ToInt32(lblLabSubTestId.Text));
                            if (dt.Rows.Count > 0)
                            {
                                //ViewState["TblArvMutation"] = dt;
                                radGridArvMutation.DataSource = dt;
                                radGridArvMutation.DataBind();
                            }
                            else
                            {
                                radGridArvMutation.DataSource = new Object[0];
                                radGridArvMutation.DataBind();
                            }
                        }
                        else if (lblLabSubTestName.Text.ToUpper().Equals("ARV MUTATIONS"))
                        {
                            txthdnfield.Value = lblLabSubTestName.Text;
                            radGridArvMutation.Columns[2].Visible = false;
                            //BindArvType
                            DataTable dt = GetArvMutationGrid("MUTATION_GRID");
                            if (dt.Rows.Count > 0)
                            {
                                ViewState["TblArvMutation"] = dt;
                                radGridArvMutation.DataSource = dt;
                                radGridArvMutation.DataBind();
                            }
                            else
                            {
                                radGridArvMutation.DataSource = new Object[0];
                                radGridArvMutation.DataBind();
                            }
                            // Bind GridView RadGridArvMutation
                        }
                        radGridArvMutation.ItemDataBound += new GridItemEventHandler(RadGridArvMutation_ItemDataBound);
                        radGridArvMutation.ItemCommand += new GridCommandEventHandler(RadGridArvMutation_ItemCommand);
                        radGridArvMutation.DeleteCommand += new GridCommandEventHandler(RadGridArvMutation_DeleteCommand);
                    }
                    else if (lblControlType.Text == "Single line text box")
                    {
                        lblUnitName.Visible = true;
                        txtAlphaRadValue.Visible = true;
                    }
                    else
                    {
                        lblUnitName.Visible = true;
                        txtRadValue.Visible = true;
                        if (Convert.ToDouble(lblMinBoundaryVal.Text.ToString()) == 0 && Convert.ToDouble(lblMaxBoundaryVal.Text.ToString()) == 0)
                        {
                            txtRadValue.MinValue = 0;
                            txtRadValue.MaxValue = 99999;
                        }
                        else
                        {
                            txtRadValue.Attributes.Add("OnBlur", "isBetween('" + txtRadValue.ClientID + "', '" + lblLabSubTestName.Text + "', '" + lblMinBoundaryVal.Text.ToString() + "', '" + lblMaxBoundaryVal.Text.ToString() + "')");
                            //txtRadValue.MinValue = Convert.ToDouble(lblMinBoundaryVal.Text.ToString());
                            //txtRadValue.MaxValue = Convert.ToDouble(lblMaxBoundaryVal.Text.ToString());
                        }
                        txtRadValue.Text = "";
                        txtRadValue.Text = lblresult.Text;

                    }
                }
            }

        }
        public DataTable CreateDtArvMutationTable()
        {
            DataTable dtlArvMutation = new DataTable();
            dtlArvMutation.Columns.Add("ID", typeof(string));
            dtlArvMutation.Columns.Add("ArvTypeID", typeof(string));
            dtlArvMutation.Columns.Add("ArvType", typeof(string));
            dtlArvMutation.Columns.Add("ArvMutationID", typeof(string));
            dtlArvMutation.Columns.Add("ArvMutation", typeof(string));
            dtlArvMutation.Columns.Add("ArvMutationOther", typeof(string));
            dtlArvMutation.Columns.Add("DeleteFlag", typeof(string));
            dtlArvMutation.Columns.Add("DeleteFlagdb", typeof(string));

            dtlArvMutation.PrimaryKey = new DataColumn[] { dtlArvMutation.Columns["ID"] };
            return dtlArvMutation;

        }

        public DataTable CreateDtGenXpertTable()
        {
            DataTable dtlGenXpert = new DataTable();
            dtlGenXpert.Columns.Add("LabId", typeof(int));
            dtlGenXpert.Columns.Add("ABFID", typeof(int));
            dtlGenXpert.Columns.Add("ABFText", typeof(string));
            dtlGenXpert.Columns.Add("GeneXpertID", typeof(int));
            dtlGenXpert.Columns.Add("GeneXpertText", typeof(string));
            dtlGenXpert.Columns.Add("CultSens", typeof(int));
            dtlGenXpert.Columns.Add("CultSensText", typeof(string));
            dtlGenXpert.Columns.Add("ParameterID", typeof(int));
            return dtlGenXpert;
        }



        protected void RadGridArvMutation_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            GridDataItem dataItm = e.Item as GridDataItem;
            RadGrid radGridArvMutation = (RadGrid)sender;

            Label lblID = (Label)dataItm.FindControl("lblID");
            string id = lblID.Text;
            DataTable table = (DataTable)ViewState["TblArvMutation"];
            table.PrimaryKey = new DataColumn[] { table.Columns["ID"] };

            if (table.Rows.Find(id) != null)
            {
                DataRow dr = table.Rows.Find(id);


                if (dr["DeleteFlagdb"].ToString() == "N")
                {
                    table.Rows.Find(id).Delete();
                }
                else
                {
                    dr["DeleteFlag"] = "Y";
                }


                // 
                table.AcceptChanges();
                var query = from inv in table.AsEnumerable()
                            where inv.Field<string>("DeleteFlag") == "N"
                            select new
                            {
                                ID = inv["ID"],
                                ArvTypeID = inv["ArvTypeID"],
                                ArvType = inv["ArvType"],
                                ArvMutationID = inv["ArvMutationID"],
                                ArvMutation = inv["ArvMutation"],
                                ArvMutationOther = inv["ArvMutationOther"],
                                DeleteFlag = inv["DeleteFlag"]
                            };


                ViewState["TblArvMutation"] = table;
                radGridArvMutation.DataSource = query;
                radGridArvMutation.DataBind();
            }
            else
            {
                radGridArvMutation.DataSource = (DataTable)ViewState["TblArvMutation"];
                radGridArvMutation.DataBind();

            }

        }

        protected void RadGridSputum_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            GridDataItem dataItm = e.Item as GridDataItem;
            RadGrid radGridArvMutation = (RadGrid)sender;

            Label lblID = (Label)dataItm.FindControl("lblID");
            string id = lblID.Text;
            DataTable table = (DataTable)ViewState["TblArvMutation"];
            table.PrimaryKey = new DataColumn[] { table.Columns["ID"] };

            if (table.Rows.Find(id) != null)
            {
                DataRow dr = table.Rows.Find(id);


                if (dr["DeleteFlagdb"].ToString() == "N")
                {
                    table.Rows.Find(id).Delete();
                }
                else
                {
                    dr["DeleteFlag"] = "Y";
                }


                // 
                table.AcceptChanges();
                var query = from inv in table.AsEnumerable()
                            where inv.Field<string>("DeleteFlag") == "N"
                            select new
                            {
                                ID = inv["ID"],
                                ArvTypeID = inv["ArvTypeID"],
                                ArvType = inv["ArvType"],
                                ArvMutationID = inv["ArvMutationID"],
                                ArvMutation = inv["ArvMutation"],
                                ArvMutationOther = inv["ArvMutationOther"],
                                DeleteFlag = inv["DeleteFlag"]
                            };


                ViewState["TblArvMutation"] = table;
                radGridArvMutation.DataSource = query;
                radGridArvMutation.DataBind();
            }
            else
            {
                radGridArvMutation.DataSource = (DataTable)ViewState["TblArvMutation"];
                radGridArvMutation.DataBind();

            }

        }
        protected void RadGridArvMutation_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            DataTable dtMutation;

            if (ViewState["TblArvMutation"] == null)
            {
                dtMutation = CreateDtArvMutationTable();
            }
            else
            {
                dtMutation = (DataTable)ViewState["TblArvMutation"];
            }
            DataRow dR = dtMutation.NewRow();

            int dtnextId = Convert.ToInt32(dtMutation.Rows.Count) + 1;
            RadGrid radGridArvMutation = (RadGrid)sender;


            if (e.CommandName == "Insert")
            {
                if (radGridArvMutation != null)
                {
                    // GridItem[] footerItems = RadOtherVaccine.MasterTableView.GetItems(GridItemType.Footer);
                    GridFooterItem footeritem = (GridFooterItem)radGridArvMutation.MasterTableView.GetItems(GridItemType.Footer)[0];
                    if (footeritem != null)
                    {
                        txthdnfield.Value = "ARV MUTATIONS";
                        Telerik.Web.UI.RadComboBox rcbFooterArvType = (Telerik.Web.UI.RadComboBox)footeritem.FindControl("rcbFooterArvType");
                        Telerik.Web.UI.RadComboBox rcbFooterMutation = (Telerik.Web.UI.RadComboBox)footeritem.FindControl("rcbFooterMutation");
                        Telerik.Web.UI.RadTextBox txtOtherFooterMutation = (Telerik.Web.UI.RadTextBox)footeritem.FindControl("txtOtherFooterMutation");
                        dR["ID"] = dtnextId.ToString();// +rcbFooterMutation.SelectedItem.Text;
                        dR["ArvTypeID"] = rcbFooterArvType.SelectedValue.ToString();
                        dR["ArvType"] = rcbFooterArvType.SelectedItem.Text.ToString();
                        dR["ArvMutationID"] = rcbFooterMutation.SelectedValue.ToString();
                        dR["ArvMutation"] = rcbFooterMutation.SelectedItem.Text.ToString();
                        dR["ArvMutationOther"] = txtOtherFooterMutation.Text.ToString();
                        dR["DeleteFlag"] = "N";
                        dR["DeleteFlagdb"] = "N";
                        dtMutation.Rows.Add(dR);
                        ViewState["TblArvMutation"] = dtMutation;

                        var query = from inv in dtMutation.AsEnumerable()
                                    where inv.Field<string>("DeleteFlag") == "N"
                                    select new
                                    {
                                        ID = inv["ID"],
                                        ArvTypeID = inv["ArvTypeID"],
                                        ArvType = inv["ArvType"],
                                        ArvMutationID = inv["ArvMutationID"],
                                        ArvMutation = inv["ArvMutation"],
                                        ArvMutationOther = inv["ArvMutationOther"],
                                        DeleteFlag = inv["DeleteFlag"]
                                    };


                        radGridArvMutation.DataSource = query;
                        radGridArvMutation.DataBind();
                        // RadGridArvMutation_ItemDataBound(sender, e);
                    }
                }

            }


        }

        protected void RadGridSputum_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            DataTable dtMutation;
            if (ViewState["TblArvMutation"] == null)
            {
                dtMutation = CreateDtArvMutationTable();
            }
            else
            {
                dtMutation = (DataTable)ViewState["TblArvMutation"];
            }
            DataRow dR = dtMutation.NewRow();

            int dtnextId = Convert.ToInt32(dtMutation.Rows.Count) + 1;
            RadGrid radGridArvMutation = (RadGrid)sender;
            if (e.CommandName == "Insert")
            {
                if (radGridArvMutation != null)
                {
                    // GridItem[] footerItems = RadOtherVaccine.MasterTableView.GetItems(GridItemType.Footer);
                    GridFooterItem footeritem = (GridFooterItem)radGridArvMutation.MasterTableView.GetItems(GridItemType.Footer)[0];
                    if (footeritem != null)
                    {
                        Telerik.Web.UI.RadComboBox rcbFooterArvType = (Telerik.Web.UI.RadComboBox)footeritem.FindControl("rcbFooterArvType");
                        Telerik.Web.UI.RadComboBox rcbFooterMutation = (Telerik.Web.UI.RadComboBox)footeritem.FindControl("rcbFooterMutation");
                        Telerik.Web.UI.RadTextBox txtOtherFooterMutation = (Telerik.Web.UI.RadTextBox)footeritem.FindControl("txtOtherFooterMutation");
                        dR["ID"] = dtnextId.ToString() + rcbFooterMutation.SelectedItem.Text;
                        dR["ArvTypeID"] = rcbFooterArvType.SelectedValue.ToString();
                        dR["ArvType"] = rcbFooterArvType.SelectedItem.Text.ToString();
                        dR["ArvMutationID"] = rcbFooterMutation.SelectedValue.ToString();
                        dR["ArvMutation"] = rcbFooterMutation.SelectedItem.Text.ToString();
                        dR["ArvMutationOther"] = txtOtherFooterMutation.Text.ToString();
                        dR["DeleteFlag"] = "N";
                        dR["DeleteFlagdb"] = "N";
                        dtMutation.Rows.Add(dR);
                        ViewState["TblArvMutation"] = dtMutation;

                        var query = from inv in dtMutation.AsEnumerable()
                                    where inv.Field<string>("DeleteFlag") == "N"
                                    select new
                                    {
                                        ID = inv["ID"],
                                        ArvTypeID = inv["ArvTypeID"],
                                        ArvType = inv["ArvType"],
                                        ArvMutationID = inv["ArvMutationID"],
                                        ArvMutation = inv["ArvMutation"],
                                        ArvMutationOther = inv["ArvMutationOther"],
                                        DeleteFlag = inv["DeleteFlag"]
                                    };


                        radGridArvMutation.DataSource = query;
                        radGridArvMutation.DataBind();
                        // RadGridArvMutation_ItemDataBound(sender, e);
                    }
                }

            }


        }
        protected void RadGridArvMutation_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            RadGrid RdGrd = (RadGrid)sender;
            if (e.Item is GridFooterItem)
            {
                GridFooterItem footeritem = (GridFooterItem)e.Item;
                RadComboBox rcbFooterArvType = (RadComboBox)footeritem.FindControl("rcbFooterArvType");
                if (txthdnfield.Value.ToString().ToUpper().Contains("SPUTUM AFB") || txthdnfield.Value.ToString().ToUpper().Equals("GENEXPERT"))
                {
                    rcbFooterArvType.Items.Clear();
                    rcbFooterArvType.Items.Add(new RadComboBoxItem("", "9997"));
                    rcbFooterArvType.Items.Add(new RadComboBoxItem("Positive", "1"));
                    rcbFooterArvType.Items.Add(new RadComboBoxItem("Negative", "0"));
                }
                else if (txthdnfield.Value.ToString().ToUpper().Contains("ARV MUTATIONS"))
                {
                    DataTable dt = GetArvMutationDataTable("ARV_TYPE");
                    rcbFooterArvType.DataSource = dt;
                    rcbFooterArvType.DataTextField = "ITEM_NAME";
                    rcbFooterArvType.DataValueField = "ID";
                    rcbFooterArvType.DataBind();
                }
                rcbFooterArvType.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(rcbFooterArvType_SelectedIndexChanged);
            }

            //if (e.Item is GridFooterItem)
            //{
            //    GridFooterItem footeritem = (GridFooterItem)e.Item;
            //    RadComboBox rcbFooterArvType = (RadComboBox)footeritem.FindControl("rcbFooterArvType");
            //    if (ViewState["Seputm"].ToString() == "Seputm")
            //    {
            //        rcbFooterArvType.Items.Add(new RadComboBoxItem("Positive", "1"));
            //        rcbFooterArvType.Items.Add(new RadComboBoxItem("Negative", "0"));
            //    }
            //    else
            //    {
            //        BindArvType(rcbFooterArvType);
            //    }
            //    rcbFooterArvType.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(rcbFooterArvType_SelectedIndexChanged);
            //}
        }
        protected void ddllist_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RadComboBox ddl = (RadComboBox)sender;
            GridTableCell item = (GridTableCell)ddl.Parent;
            Label lblundetectable = (Label)item.FindControl("lblundetectable");
            if (lblundetectable.Text == "1")
            {

                Label lblLabSubTestId = (Label)item.FindControl("lblLabSubTestId");
                Label lblLabSubTestName = (Label)item.FindControl("lblLabTestName");
                Label lblControlType = (Label)item.FindControl("lblControlType");

                Label lblUnitName = (Label)item.FindControl("lblUnitName");
                Label lblMinBoundaryVal = (Label)item.FindControl("lblMinBoundaryVal");
                Label lblMaxBoundaryVal = (Label)item.FindControl("lblMaxBoundaryVal");
                Label lblTestResultId = (Label)item.FindControl("lblTestResultId");

                Telerik.Web.UI.RadNumericTextBox txtRadValue = (Telerik.Web.UI.RadNumericTextBox)item.FindControl("txtRadValue");
                Telerik.Web.UI.RadTextBox txtAlphaRadValue = (Telerik.Web.UI.RadTextBox)item.FindControl("txtAlphaRadValue");
                Label lblresult = (Label)item.FindControl("lblTestResults");
                if (ddl.SelectedValue == "9999")
                {
                    lblUnitName.Visible = true;
                    txtRadValue.Visible = true;
                    if (Convert.ToDouble(lblMinBoundaryVal.Text.ToString()) == 0 && Convert.ToDouble(lblMaxBoundaryVal.Text.ToString()) == 0)
                    {
                        txtRadValue.MinValue = 0;
                        txtRadValue.MaxValue = 99999;
                    }
                    else
                    {
                        txtRadValue.Attributes.Add("OnBlur", "isBetween('" + txtRadValue.ClientID + "', '" + lblLabSubTestName.Text + "', '" + lblMinBoundaryVal.Text.ToString() + "', '" + lblMaxBoundaryVal.Text.ToString() + "')");
                        //txtRadValue.MinValue = Convert.ToDouble(lblMinBoundaryVal.Text.ToString());
                        //txtRadValue.MaxValue = Convert.ToDouble(lblMaxBoundaryVal.Text.ToString());
                    }
                    txtRadValue.Text = "";
                    txtRadValue.Text = lblresult.Text;
                }
                else
                {
                    lblUnitName.Visible = false;
                    txtRadValue.Visible = false;
                    if (Convert.ToDouble(lblMinBoundaryVal.Text.ToString()) == 0 && Convert.ToDouble(lblMaxBoundaryVal.Text.ToString()) == 0)
                    {
                        txtRadValue.MinValue = 0;
                        txtRadValue.MaxValue = 99999;
                    }
                    else
                    {
                        txtRadValue.Attributes.Add("OnBlur", "isBetween('" + txtRadValue.ClientID + "', '" + lblLabSubTestName.Text + "', '" + lblMinBoundaryVal.Text.ToString() + "', '" + lblMaxBoundaryVal.Text.ToString() + "')");
                        //txtRadValue.MinValue = Convert.ToDouble(lblMinBoundaryVal.Text.ToString());
                        //txtRadValue.MaxValue = Convert.ToDouble(lblMaxBoundaryVal.Text.ToString());
                    }
                    txtRadValue.Text = "";
                    txtRadValue.Text = lblresult.Text;
                }
            }
        }

        protected void rcbFooterArvType_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RadComboBox combo = (RadComboBox)sender;
            GridFooterItem footeritem = (GridFooterItem)combo.NamingContainer;
            RadComboBox rcbFooterMutation = (RadComboBox)footeritem.FindControl("rcbFooterMutation");
            //Jayant-Start 
            if (e.Value == "0" && e.Text == "Negative")
            {
                rcbFooterMutation.Items.Clear();
                rcbFooterMutation.Items.Add(new RadComboBoxItem("Positive-Resistant", "1"));
                rcbFooterMutation.Items.Add(new RadComboBoxItem("Positive-Sensitive", "2"));
                rcbFooterMutation.Items.Add(new RadComboBoxItem("Negative", "0"));
            }
            else if (e.Value == "1" && e.Text == "Positive")
            {
                rcbFooterMutation.Items.Clear();
            }
            else if (e.Value == "1" && e.Text == "NRTI")
            {
                rcbFooterMutation.Items.Clear();
                BindArvMutation(rcbFooterMutation, Convert.ToInt32(e.Value));
            }
            else if (e.Value == "2" && e.Text == "NNRTI")
            {
                rcbFooterMutation.Items.Clear();
                BindArvMutation(rcbFooterMutation, Convert.ToInt32(e.Value));
            }
            else if (e.Value == "3" && e.Text == "PI")
            {
                rcbFooterMutation.Items.Clear();
                BindArvMutation(rcbFooterMutation, Convert.ToInt32(e.Value));
            }
            else if (e.Value == "4" && e.Text == "INI")
            {
                rcbFooterMutation.Items.Clear();
                BindArvMutation(rcbFooterMutation, Convert.ToInt32(e.Value));
            }
            //rcbFooterMutation.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(rcbFooterMutation_SelectedIndexChanged);
            
            //RadComboBox combo = (RadComboBox)sender;
            //GridFooterItem footeritem = (GridFooterItem)combo.NamingContainer;
            //RadComboBox rcbFooterMutation = (RadComboBox)footeritem.FindControl("rcbFooterMutation");
            //if (ViewState["Seputm"].ToString() == "Seputm")
            //{
            //    if (e.Value == "0")
            //    {
            //        rcbFooterMutation.Items.Clear();
            //        rcbFooterMutation.Items.Add(new RadComboBoxItem("Positive-Resistant", "1"));
            //        rcbFooterMutation.Items.Add(new RadComboBoxItem("Positive-Sensitive", "2"));
            //        rcbFooterMutation.Items.Add(new RadComboBoxItem("Negative", "0"));
            //    }
            //    else if (e.Value == "1")
            //    {
            //        BindArvMutation(rcbFooterMutation, Convert.ToInt32(e.Value));
            //    }
            //    else if (e.Value == "2")
            //    {
            //        BindArvMutation(rcbFooterMutation, Convert.ToInt32(e.Value));
            //    }
            //    else if (e.Value == "3")
            //    {
            //        BindArvMutation(rcbFooterMutation, Convert.ToInt32(e.Value));
            //    }
            //    else if (e.Value == "4")
            //    {
            //        BindArvMutation(rcbFooterMutation, Convert.ToInt32(e.Value));
            //    }
            //    else
            //    {
            //        rcbFooterMutation.Items.Clear();
            //    }

            //}
            //else
            //{
            //    BindArvMutation(rcbFooterMutation, Convert.ToInt32(e.Value));
            //}
            //rcbFooterMutation.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(rcbFooterMutation_SelectedIndexChanged);

        }
        protected void rcbFooterMutation_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RadComboBox comboMut = (RadComboBox)sender;
            GridFooterItem footeritemMut = (GridFooterItem)comboMut.NamingContainer;
            RadComboBox rcbFooterCulture = (RadComboBox)footeritemMut.FindControl("rcbFooterCulture");
            if (e.Value == "1")
            {
                rcbFooterCulture.Items.Clear();
                rcbFooterCulture.Items.Add(new RadComboBoxItem("Confirmed-Resistant", "1"));
                rcbFooterCulture.Items.Add(new RadComboBoxItem("Not-Resistant", "2"));
            }
            else
            {
                rcbFooterCulture.Items.Clear();
            }            
        }
        protected void BindChildGridDdl(Telerik.Web.UI.RadComboBox rcbComobo, string flag, string dataTextName, string dataValueName)
        {

            //DataTable dt = GetDataTable(flag, 0, "");
            //rcbComobo.DataTextField = dataTextName;
            //rcbComobo.DataValueField = dataValueName;
            //rcbComobo.DataSource = dt;
            //rcbComobo.DataBind();
            //rcbComobo.SelectedValue = "";
        }
        protected void BtnSaveClick(object sender, EventArgs e)
        {
            if (Request.QueryString["name"] == "Delete")
            {
                DeleteForm();
            }
            else if (FieldValidation() == false)
            {
                return;
            }
            else
                SaveLabOrder();
            // BtnBack_Click(sender, e);
        }
        private void DeleteForm()
        {
            int theResultRow, OrderNo;
            string FormName;
            OrderNo = Convert.ToInt32(Session["PatientVisitId"]);
            FormName = "Order Labs";

            ILabFunctions LabResultManager = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
            theResultRow = (int)LabResultManager.DeleteLabForms(FormName, OrderNo, Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["AppUserId"].ToString()));

            if (theResultRow == 0)
            {
                IQCareMsgBox.Show("RemoveFormError", this);
                return;
            }
            else
            {
                string theUrl;
                theUrl = string.Format("../ClinicalForms/frmPatient_Home.aspx?Func=Delete");
                Response.Redirect(theUrl);

            }
        }
        private void PutCustomControl()
        {
            ICustomFields CustomFields;
            CustomFieldClinical theCustomField = new CustomFieldClinical();
            try
            {

                CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields,BusinessProcess.Administration");
                DataSet theDS = CustomFields.GetCustomFieldListforAForm(Convert.ToInt32(ApplicationAccess.Laboratory));

                if (theDS.Tables[0].Rows.Count != 0)
                {
                    //theCustomField.CreateCustomControlsForms(pnlCustomList, theDS, "PPharm");
                }
                ViewState["CustomFieldsDS"] = theDS;
                //pnlCustomList.Visible = true;
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
        private void FillOldCustomData(Int32 PatID)
        {
            DataSet dsvalues = null;
            ICustomFields CustomFields;
            Int32 LabId = 0;
            if (Session["PatientVisitId"] != null)
                LabId = Convert.ToInt32(Session["PatientVisitId"]);
            try
            {
                DataSet theCustomFields = (DataSet)ViewState["CustomFieldsDS"];
                string theTblName = string.Empty;
                if (theCustomFields.Tables[0].Rows.Count > 0)
                {
                    theTblName = theCustomFields.Tables[0].Rows[0]["FeatureName"].ToString().Replace(" ", "_");
                }
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
                dsvalues = CustomFields.GetCustomFieldValues("dtl_CustomField_" + theTblName.ToString().Replace("-", "_"), theColName, Convert.ToInt32(PatID.ToString()), 0, 0, Convert.ToInt32(LabId), 0, Convert.ToInt32(ApplicationAccess.Laboratory));
                CustomFieldClinical theCustomManager = new CustomFieldClinical();

                //theCustomManager.FillCustomFieldData(theCustomFields, dsvalues, pnlCustomList, "PPharm");
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

        #endregion

        protected void tabControl_ActiveTabChanged(object sender, EventArgs e)
        {
            if (Session["LabNumber"] != null)
                fillLabDetails();
            else
                tabControl.ActiveTabIndex = 0;

        }
        private void clearsession()
        {
            Session.Remove("TranDate");
            Session.Remove("LabNumber");
            Session.Remove("TestInitpDT");
            Session.Remove("SpecRecpDT");
            Session.Remove("LabOrderStatus");
            Session.Remove("LabOrderID");
        }
        private void fillLabDetails()
        {
            if (tabControl.ActiveTabIndex > 0)
            {
                IKNHStaticForms KNHStat = (IKNHStaticForms)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHStaticForms, BusinessProcess.Clinical");
                DataTable dttabid;
                
                UCSpecLabDetails.SetData(Session["LabNumber"].ToString(), Convert.ToDateTime(Session["TranDate"]));                
                UcResLabDetails.SetData(Session["LabNumber"].ToString(), Convert.ToDateTime(Session["TranDate"]));
                string LabNumber = UCSpecLabDetails.txtlabnumber.Text;
                string[] lab = LabNumber.Split('-');
                string LabFilNo = lab[1].TrimStart('0');
                ILabFunctions theILabManager = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
                DataSet theLabDS = theILabManager.GetOrderedLabs(Convert.ToInt32(LabFilNo));
                gridSpecList.DataSource = theLabDS.Tables[3];
                gridSpecList.DataBind();
                Session["SpecRecpDT"] = theLabDS.Tables[3];
                
                if(tabControl.ActiveTabIndex == 1)
                {
                    dttabid = KNHStat.GetTabID("TabLabSpecimen");
                    if (dttabid.Rows.Count > 0)
                    {
                        AuthenticationRight(83, Convert.ToInt32(dttabid.Rows[0]["TabID"]), btnSpecimenSave, btnSpecimenPrint);
                        //gvLaborders.DataSource = theLabDS.Tables[4];
                        BindGrid(theLabDS.Tables[0]);
                    }
                }
                
                if (tabControl.ActiveTabIndex == 2)
                {
                    dttabid = KNHStat.GetTabID("TabLabResults");
                    if (dttabid.Rows.Count > 0)
                        AuthenticationRight(83, Convert.ToInt32(dttabid.Rows[0]["TabID"]), BtnSaveLabResults, BtnPrintLabResults);
                    if (Session["LabOrderID"] != null)
                    {
                        if (Convert.ToInt32(Session["LabOrderID"].ToString()) > 0)
                        {
                            
                            foreach (GridDataItem griditem in RadGridLabTest.Items)
                            {
                                object sender = new object();
                                CommandEventArgs arg = new CommandEventArgs("ExpandCollapse", griditem);
                                GridCommandEventArgs evarg = new GridCommandEventArgs(griditem, null, arg);
                                RadGridLabTest_ItemCommand(sender, evarg);
                                griditem.Expanded = true;
                            }
                            foreach (GridNestedViewItem nestedView in RadGridLabTest.MasterTableView.GetItems(GridItemType.NestedView))
                            {
                                RadGrid radGridLabResult = (RadGrid)nestedView.FindControl("RadGridLabResult");                                
                                
                                foreach (GridDataItem item in radGridLabResult.Items)
                                {
                                    Label lblLabSubTestId = (Label)item.FindControl("lblLabSubTestId");
                                    Label lblSpecimenID = (Label)item.FindControl("lblSpecimenID");
                                    BindFunctions thebind = new BindFunctions();
                                    DropDownList ddlselectspecimen = (DropDownList)item.FindControl("ddlselectspecimen");
                                    thebind.BindCombo(ddlselectspecimen, theLabDS.Tables[2], "SpecimenName", "ID");
                                    if (Session["LabOrderID"] != null)
                                    {
                                        foreach (DataRow theLabDR in theLabDS.Tables[4].Rows)
                                        {
                                            if (lblLabSubTestId.Text == theLabDR["LabTestId"].ToString())
                                            {
                                                
                                                if (lblSpecimenID.Text != "")
                                                {
                                                    ddlselectspecimen.SelectedValue = lblSpecimenID.Text;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            
                        }
                    }
                }
                DataTable dt = GetDataTable("LAB_STATUS", "", Convert.ToInt32(LabFilNo));
                Session["LabOrderStatus"] = dt.Rows[0]["LabStatus"].ToString();
            }
        }

        protected void btnSpecSubmit_Click(object sender, EventArgs e)
        {
            if (SpecimenFieldValidation())
            {
                IQCareUtils theUtils = new IQCareUtils();
                string LabNumber = UCSpecLabDetails.txtlabnumber.Text;
                string[] lab = LabNumber.Split('-');
                string LabFilNo = lab[1].TrimStart('0');
                string CustomSpecNo = LabFilNo + "-" + Convert.ToInt32(ddlspecimentype.SelectedValue) + Convert.ToInt32(ddlSpecSource.SelectedValue);
                if (Session["SpecRecpDT"] == null)
                    Session["SpecRecpDT"] = CreateSpecimentable();
                DataTable SpecList = (DataTable)Session["SpecRecpDT"];
                //DataTable FilSpecList = RemoveSNofromDT(SpecList, "SpecCustomNumber");
                theDV = new DataView(SpecList);
                theDV.RowFilter = "SpecCustomNumber = '" + CustomSpecNo + "'";
                DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                if (theDT.Rows.Count > 0)
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["MessageText"] = "Specimen Type already exists.";
                    IQCareMsgBox.Show("#C1", theBuilder, this);
                    return;
                }
                int lastrow = 0;
                if (SpecList.Rows.Count > 0)
                {
                    DataRow thisRow = (DataRow)SpecList.Rows[SpecList.Rows.Count - 1];
                    lastrow = thisRow["ID"] != DBNull.Value ? Convert.ToInt32(thisRow["ID"]) : 0;
                }
                DataRow thespecdr = SpecList.NewRow();
                thespecdr["ID"] = lastrow + 1;
                thespecdr["SpecCustomNumber"] = LabFilNo + "-" + Convert.ToInt32(ddlspecimentype.SelectedValue) + Convert.ToInt32(ddlSpecSource.SelectedValue);
                thespecdr["LabID"] = LabFilNo;
                thespecdr["SpecimenTypeID"] = Convert.ToInt32(ddlspecimentype.SelectedValue);
                thespecdr["Specimentype"] = ddlspecimentype.SelectedItem.Text;
                thespecdr["SourceID"] = Convert.ToInt32(ddlSpecSource.SelectedValue);
                thespecdr["SpecimenSource"] = ddlSpecSource.SelectedItem.Text;
                thespecdr["SpecimenOtherSource"] = txtOtherSource.Text;
                thespecdr["SpecimenDate"] = Convert.ToDateTime(txtSpecRecdt.DbSelectedDate);
                thespecdr["FacilityID"] = Convert.ToInt32(ddlfromfacility.SelectedValue);
                thespecdr["FacilityName"] = ddlfromfacility.SelectedItem.Text;
                thespecdr["Specimennumbers"] = Convert.ToInt32(ddlspecno.SelectedValue);
                thespecdr["SpecimenRecvdbyId"] = Convert.ToInt32(ddlrecivedby.SelectedValue);
                thespecdr["SpecimenRecvdby"] = ddlrecivedby.SelectedItem.Text;
                thespecdr["Flag"] = "1";
                SpecList.Rows.Add(thespecdr);
                SpecList.AcceptChanges();
                //List<FillSpecimen> SpecList = new List<FillSpecimen>();
                //SpecList.Add(new FillSpecimen(Convert.ToInt32(ddlspecimentype.SelectedValue), Convert.ToInt32(ddlSpecSource.SelectedValue), Convert.ToInt32(ddlspecno.SelectedValue), Convert.ToInt32(ddlrecivedby.SelectedValue), ddlspecimentype.SelectedItem.Text, ddlSpecSource.SelectedItem.Text, ddlspecno.SelectedItem.Text, ddlrecivedby.SelectedItem.Text, Convert.ToDateTime(txtSpecRecdt.DbSelectedDate), txtOtherSource.Text));
                gridSpecList.DataSource = SpecList;
                gridSpecList.DataBind();
                Session["SpecRecpDT"] = SpecList;
                ClearSpecimen();
            }
        }
        private void ClearSpecimen()
        {
            ddlspecimentype.SelectedIndex = 0;
            ddlSpecSource.SelectedIndex = 0;
            ddlfromfacility.SelectedIndex = 0;
            ddlspecno.SelectedIndex = 0;
            ddlrecivedby.SelectedIndex = 0;
            txtOtherSource.Text = "";
            txtSpecRecdt.DbSelectedDate = "";

        }
        private void AuthenticationRight(int FeatureID,  int iTabID, Button btnsave, Button btnPrint)
        {
            AuthenticationManager Authentication = new AuthenticationManager();
            ICustomForm mgrUserValidate = (ICustomForm)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BCustomForm, BusinessProcess.Clinical");
            IKNHStaticForms isTabSaved = (IKNHStaticForms)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHStaticForms, BusinessProcess.Clinical");            
            bool bCanView = !Authentication.HasFunctionRight(FeatureID, iTabID, FunctionAccess.View, (DataTable)System.Web.HttpContext.Current.Session["UserRight"]);
            //if user have view permission
            btnsave.Enabled = bCanView;

            //first time  - new user form creation
            if (Convert.ToInt32(System.Web.HttpContext.Current.Session["ResultVisitId"]) == 0)
            {
                bool bCanAdd = Authentication.HasFunctionRight(FeatureID, iTabID, FunctionAccess.Add, (DataTable)System.Web.HttpContext.Current.Session["UserRight"]);
                btnsave.Enabled = bCanAdd;

            }
            else if (Convert.ToInt32(System.Web.HttpContext.Current.Session["ResultVisitId"]) > 0)
            {
                DataSet tabSaved = isTabSaved.CheckIfTabSaved(iTabID, Convert.ToInt32(System.Web.HttpContext.Current.Session["ResultVisitId"]));
                if (tabSaved.Tables[0].Rows.Count == 0)
                {
                    bool bCanAdd = Authentication.HasFunctionRight(FeatureID, iTabID, FunctionAccess.Add, (DataTable)System.Web.HttpContext.Current.Session["UserRight"]);
                    btnsave.Enabled = bCanAdd;
                }
                else
                {
                    bool bCanUpdate = Authentication.HasFunctionRight(FeatureID, iTabID, FunctionAccess.Update, (DataTable)System.Web.HttpContext.Current.Session["UserRight"]);
                    if (Convert.ToInt32(System.Web.HttpContext.Current.Session["AppUserID"]) == 1)
                        bCanUpdate = true;

                    btnsave.Enabled = bCanUpdate;
                }

            }
            btnPrint.Enabled = Authentication.HasFunctionRight(FeatureID, iTabID, FunctionAccess.Print, (DataTable)System.Web.HttpContext.Current.Session["UserRight"]);            
            
        }
        private DataTable RemoveSNofromDT(DataTable theDt, string ColName)
        {
            string ColValue = "";
            DataTable theNew = theDt.Clone();
            foreach (DataRow theDR in theDt.Rows)
            {
                theNew.NewRow();
                ColValue = theDR[ColName].ToString();
                string[] thenewstr = ColValue.Split('-');
                theDR[ColName] = thenewstr[0] + "-" + thenewstr[1];
            }
            return theDt;
        }

        protected void gridSpecList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            GridDataItem dataItem = e.Item as GridDataItem;

            if (dataItem != null)
            {
                Label lblFalg = (Label)dataItem.FindControl("lblFlag");
                if (lblFalg.Text == "1")
                {
                    (dataItem["Delete"].Controls[0] as ImageButton).ImageUrl = "~/Images/del.gif";
                }
                else
                {
                    dataItem["Delete"].Controls[0].Visible = false;
                }
            }

        }
        private DataTable CreateSpecimentable()
        {
            DataTable thespecdt = new DataTable();
            thespecdt.Columns.Add("ID", typeof(int));
            thespecdt.Columns.Add("LabID", typeof(Int32));
            thespecdt.Columns.Add("SpecCustomNumber", typeof(string));
            thespecdt.Columns.Add("SpecimenTypeID", typeof(Int32));
            thespecdt.Columns.Add("Specimentype", typeof(string));
            thespecdt.Columns.Add("SourceID", typeof(Int32));
            thespecdt.Columns.Add("SpecimenSource", typeof(string));
            thespecdt.Columns.Add("SpecimenOtherSource", typeof(string));
            thespecdt.Columns.Add("SpecimenDate", typeof(DateTime));
            thespecdt.Columns.Add("FacilityID", typeof(Int32));
            thespecdt.Columns.Add("FacilityName", typeof(string));
            thespecdt.Columns.Add("Specimennumbers", typeof(string));
            thespecdt.Columns.Add("SpecimenRecvdbyId", typeof(Int32));
            thespecdt.Columns.Add("SpecimenRecvdby", typeof(string));
            thespecdt.Columns.Add("Flag", typeof(int));
            return thespecdt;
        }
        private DataTable CreateTestInittable()
        {
            DataTable theTestInitdt = new DataTable();
            
            theTestInitdt.Columns.Add("LabID", typeof(int));
            theTestInitdt.Columns.Add("LabTestID", typeof(int));            
            theTestInitdt.Columns.Add("SpecimenID", typeof(int));            
            theTestInitdt.Columns.Add("CustomSpecimenName", typeof(string));            
            theTestInitdt.Columns.Add("StateId", typeof(int));            
            theTestInitdt.Columns.Add("StatusId", typeof(int));            
            theTestInitdt.Columns.Add("RejectedReasonId", typeof(int));            
            theTestInitdt.Columns.Add("OtherReason", typeof(string));
            
            return theTestInitdt;
        }

        private Boolean SpecimenFieldValidation()
        {

            IQCareSecurity = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
            theCurrentDate = (DateTime)IQCareSecurity.SystemDate();
            IQCareUtils theUtils = new IQCareUtils();
            if (ddlspecimentype.SelectedIndex == 0)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Specimen Type";
                IQCareMsgBox.Show("BlankDropDown", theBuilder, this);
                return false;
            }
            if (ddlSpecSource.SelectedIndex == 0)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Specimen Source";
                IQCareMsgBox.Show("BlankDropDown", theBuilder, this);
                return false;
            }
            if (txtSpecRecdt.DbSelectedDate.ToString() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Specimen Received Date";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                //txtvisitDate.Focus();
                return false;
            }
            if (Convert.ToDateTime(Application["AppCurrentDate"].ToString()).AddDays(1) < Convert.ToDateTime(txtSpecRecdt.DbSelectedDate))
            {
                IQCareMsgBox.Show("SpecimenDate", this);
                return false;
            }
            if (ddlfromfacility.SelectedIndex == 0)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Facility From";
                IQCareMsgBox.Show("BlankDropDown", theBuilder, this);
                return false;
            }
            if (ddlspecno.SelectedIndex == 0)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Number of Specimen";
                IQCareMsgBox.Show("BlankDropDown", theBuilder, this);
                return false;
            }
            if (ddlrecivedby.SelectedIndex == 0)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Received by";
                IQCareMsgBox.Show("BlankDropDown", theBuilder, this);
                return false;
            }
            IQCareMsgBox.HideMessage(this);
            return true;
        }
        private Boolean LabResultFieldValidation()
        {
            foreach (GridNestedViewItem nestedView in RadGridLabTest.MasterTableView.GetItems(GridItemType.NestedView))
            {
                RadGrid radGridLabResult = (RadGrid)nestedView.FindControl("RadGridLabResult");
                DataTable dtGenXpert = new DataTable();
                dtGenXpert = CreateDtGenXpertTable();
                foreach (GridDataItem item in radGridLabResult.Items)
                {
                    
                    List<BIQTouchLabFields> listArv = new List<BIQTouchLabFields>();
                    Label lblLabTestName = (Label)item.FindControl("lblLabTestName");
                    Label lblLabSubTestId = (Label)item.FindControl("lblLabSubTestId");
                    Label lblLabSubTestName = (Label)item.FindControl("lblLabTestName");
                    DropDownList ddlselectspecimen = (DropDownList)item.FindControl("ddlselectspecimen");                    
                    DropDownList ddlState = (DropDownList)item.FindControl("ddlState");                    
                    DropDownList ddlTestStatus = (DropDownList)item.FindControl("ddlTestStatus");
                    DropDownList ddlrejreason = (DropDownList)item.FindControl("ddlrejreason");
                    DropDownList ddlRadReportedby = (DropDownList)item.FindControl("ddlRadReportedby");
                    RadDateTimePicker txtReportLabDate = (RadDateTimePicker)item.FindControl("txtReportLabDate");
                    Label lblundetectable = (Label)item.FindControl("lblundetectable");
                    Label lblControlType = (Label)item.FindControl("lblControlType");
                    RadioButtonList btnradRadioButtonList = (RadioButtonList)item.FindControl("btnRadRadiolist");
                    Telerik.Web.UI.RadNumericTextBox txtRadValue = (Telerik.Web.UI.RadNumericTextBox)item.FindControl("txtRadValue");
                    Telerik.Web.UI.RadTextBox txtAlphaRadValue = (Telerik.Web.UI.RadTextBox)item.FindControl("txtAlphaRadValue");
                    CheckBoxList chkBoxList = (CheckBoxList)item.FindControl("chkBoxList");
                    Telerik.Web.UI.RadComboBox ddlList = (RadComboBox)item.FindControl("ddlList");

                    string strResuts = "0";
                    bool testfilled = false;
                    if (lblundetectable.Text == "1")
                    {
                        if (ddlList.SelectedValue.ToString() != "0")
                        {
                            //if((ddlList.SelectedValue.ToString() != "9999") && (txtRadValue.Text !=""))
                            testfilled = true;
                        }



                    }
                    else
                    {
                        if (lblControlType.Text == "Radio")
                        {
                            if (btnradRadioButtonList.SelectedValue != "")
                            {
                                testfilled = true;
                            }
                        }
                        else if (lblControlType.Text == "Combo Box")
                        {

                            if (ddlList.SelectedValue.ToString() != "")
                            {
                                testfilled = true;
                            }

                        }
                        else if (lblControlType.Text == "Check box")
                        {

                            if (chkBoxList.SelectedValue.ToString() != "")
                            {
                                testfilled = true;
                            }
                        }
                        else if (lblControlType.Text == "GridView")
                        {
                            if (lblLabSubTestName.Text.ToUpper().Equals("ARV MUTATIONS"))
                            {
                                listArv = ArvMutationData(lblLabSubTestId.Text.ToString());
                                if (listArv.Count > 0)
                                {
                                    testfilled = true;
                                }
                            }
                            else
                            {
                                dtGenXpert.Rows.Clear();
                                DataRow dR = dtGenXpert.NewRow();
                                RadGrid radSubGridItems = (RadGrid)item.FindControl("RadGridArvMutation");
                                GridFooterItem radSubFooterItems = (GridFooterItem)radSubGridItems.MasterTableView.GetItems(GridItemType.Footer)[0];
                                Telerik.Web.UI.RadComboBox rcbFooterArvType = (Telerik.Web.UI.RadComboBox)radSubFooterItems.FindControl("rcbFooterArvType");
                                Telerik.Web.UI.RadComboBox rcbFooterMutation = (Telerik.Web.UI.RadComboBox)radSubFooterItems.FindControl("rcbFooterMutation");
                                Telerik.Web.UI.RadComboBox rcbFooterCulture = (Telerik.Web.UI.RadComboBox)radSubFooterItems.FindControl("rcbFooterCulture");
                                dR["LabId"] = Convert.ToInt32(Session["LabOrderID"]);
                                if (rcbFooterArvType.SelectedValue != "")
                                {
                                    dR["ABFID"] = Convert.ToInt32(rcbFooterArvType.SelectedValue);
                                    dR["ABFText"] = rcbFooterArvType.SelectedItem.Text;
                                }
                                if (rcbFooterMutation.SelectedValue != "")
                                {
                                    dR["GeneXpertID"] = Convert.ToInt32(rcbFooterMutation.SelectedValue);
                                    dR["GeneXpertText"] = rcbFooterMutation.SelectedItem.Text;
                                }
                                if (rcbFooterCulture.SelectedValue != "")
                                {
                                    dR["CultSens"] = Convert.ToInt32(rcbFooterCulture.SelectedValue);
                                    dR["CultSensText"] = rcbFooterCulture.SelectedItem.Text;
                                }
                                dR["ParameterID"] = Convert.ToInt32(lblLabSubTestId.Text);
                                dtGenXpert.Rows.Add(dR);
                                if (dtGenXpert.Rows.Count > 0)
                                {
                                    testfilled = true;
                                }
                            }
                        }
                        else
                        {

                            if (txtRadValue.Text != "0" && txtRadValue.Text != "")
                            {
                                strResuts = txtRadValue.Text.ToString();
                                testfilled = true;
                            }
                            else if (txtAlphaRadValue.Text != "")
                            {
                                strResuts = txtAlphaRadValue.Text.ToString();
                                testfilled = true;
                            }
                        }
                    }
                    //if (ddlselectspecimen.SelectedIndex == 0)
                    //{
                    //    MsgBuilder theBuilder = new MsgBuilder();
                    //    theBuilder.DataElements["Control"] = "Specimen Name for " + lblLabTestName.Text;
                    //    IQCareMsgBox.Show("BlankDropDown", theBuilder, this);
                    //    return false;
                    //}
                    
                    //if (ddlState.SelectedIndex == 0)
                    //{
                    //    MsgBuilder theBuilder = new MsgBuilder();
                    //    theBuilder.DataElements["Control"] = "Specimen State for " + lblLabTestName.Text;
                    //    IQCareMsgBox.Show("BlankDropDown", theBuilder, this);
                    //    return false;
                    //}
                    //if (ddlTestStatus.SelectedIndex == 0)
                    //{
                    //    MsgBuilder theBuilder = new MsgBuilder();
                    //    theBuilder.DataElements["Control"] = "Specimen Status for " + lblLabTestName.Text;
                    //    IQCareMsgBox.Show("BlankDropDown", theBuilder, this);
                    //    return false;
                    //}
                    if (testfilled==true && ddlRadReportedby.SelectedIndex == 0)
                    {
                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["Control"] = "Reported by for " + lblLabTestName.Text;
                        IQCareMsgBox.Show("BlankDropDown", theBuilder, this);
                        return false;
                    }
                    if (testfilled == true && txtReportLabDate.DbSelectedDate.ToString() == "")
                    {
                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["Control"] = "Test Initiation Date for " + lblLabTestName.Text;
                        IQCareMsgBox.Show("BlankTextBox", theBuilder, this);                       
                        return false;
                    }
                    if (Convert.ToDateTime(Application["AppCurrentDate"].ToString()).AddDays(1) < Convert.ToDateTime(txtReportLabDate.DbSelectedDate))
                    {
                        IQCareMsgBox.Show("TestInitDate", this);
                        return false;
                    }
                }
            }            
            
            IQCareMsgBox.HideMessage(this);
            return true;
        }
        private Boolean TestInitFieldValidation()
        {

            IQCareSecurity = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
            theCurrentDate = (DateTime)IQCareSecurity.SystemDate();
            IQCareUtils theUtils = new IQCareUtils();
            //if (ddltestname.SelectedIndex == 0)
            //{
            //    MsgBuilder theBuilder = new MsgBuilder();
            //    theBuilder.DataElements["Control"] = "Lab Test";
            //    IQCareMsgBox.Show("BlankDropDown", theBuilder, this);
            //    return false;
            //}
            //if (ddlselectspecimen.SelectedIndex == 0)
            //{
            //    MsgBuilder theBuilder = new MsgBuilder();
            //    theBuilder.DataElements["Control"] = "Specimen Name";
            //    IQCareMsgBox.Show("BlankDropDown", theBuilder, this);
            //    return false;
            //}
            //if (txtTestInitdt.DbSelectedDate.ToString() == "")
            //{
            //    MsgBuilder theBuilder = new MsgBuilder();
            //    theBuilder.DataElements["Control"] = "Test Initiation Date";
            //    IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
            //    //txtvisitDate.Focus();
            //    return false;
            //}
            //if (Convert.ToDateTime(Application["AppCurrentDate"].ToString()).AddDays(1) < Convert.ToDateTime(txtTestInitdt.DbSelectedDate))
            //{
            //    IQCareMsgBox.Show("TestInitDate", this);
            //    return false;
            //}
            //if (ddlTestPerfby.SelectedIndex == 0)
            //{
            //    MsgBuilder theBuilder = new MsgBuilder();
            //    theBuilder.DataElements["Control"] = "Performed by";
            //    IQCareMsgBox.Show("BlankDropDown", theBuilder, this);
            //    return false;
            //}
            //if (ddlState.SelectedIndex == 0)
            //{
            //    MsgBuilder theBuilder = new MsgBuilder();
            //    theBuilder.DataElements["Control"] = "Specimen State";
            //    IQCareMsgBox.Show("BlankDropDown", theBuilder, this);
            //    return false;
            //}
            //if (ddlTestStatus.SelectedIndex == 0)
            //{
            //    MsgBuilder theBuilder = new MsgBuilder();
            //    theBuilder.DataElements["Control"] = "Specimen Status";
            //    IQCareMsgBox.Show("BlankDropDown", theBuilder, this);
            //    return false;
            //}
            IQCareMsgBox.HideMessage(this);
            return true;
        }
        protected void btnSpecimenSave_Click(object sender, EventArgs e)
        {
            SaveSpecimen();
        }

        protected void btncloseSpecimen_Click(object sender, EventArgs e)
        {
            clearsession();
            Response.Redirect(Request.RawUrl);
        }

        private void SaveSpecimen()
        {
            DataTable thesvspcDt = (DataTable)Session["SpecRecpDT"];
            if (thesvspcDt.Rows.Count > 0)
            {
                bool exists = thesvspcDt.AsEnumerable().Where(c => c.Field<string>("Flag").Equals("1")).Count() > 0;
                if (exists)
                {
                    DataTable tblFiltered = thesvspcDt.AsEnumerable()
                   .Where(row => row.Field<string>("Flag") == "1")
                   .CopyToDataTable();
                    thesvspcDt = new DataTable();
                    thesvspcDt = RemoveColumnsfromSpecimenTable(tblFiltered);
                    ILabFunctions theILabManager = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
                    int result = theILabManager.SaveUpdateSpecimenDetails(thesvspcDt, Convert.ToInt32(Session["AppUserId"]));
                    if (result > 0)
                    {
                        SaveCancel("Specimen Receipt");
                        tabControl.ActiveTabIndex = 2;
                        fillLabDetails();
                    }
                }
            }
        }
        private DataTable RemoveColumnsfromSpecimenTable(DataTable thedt)
        {
            thedt.Columns.Remove("ID");
            thedt.Columns.Remove("Specimentype");
            thedt.Columns.Remove("SpecimenSource");
            thedt.Columns.Remove("FacilityName");
            thedt.Columns.Remove("SpecimenRecvdby");
            thedt.Columns.Remove("Flag");
            return thedt;

        }
        private DataTable RemoveColumnsTestInitTable(DataTable thedt)
        {
            thedt.Columns.Remove("ID");
            thedt.Columns.Remove("TestName");
            thedt.Columns.Remove("SpecimenName");
            thedt.Columns.Remove("Performedby");
            thedt.Columns.Remove("StateName");
            thedt.Columns.Remove("StatusName");
            thedt.Columns.Remove("RejectedReason");
            thedt.Columns.Remove("Flag");
            return thedt;

        }
        private void SaveCancel(string tabname)
        {
            int PatientID = Convert.ToInt32(Session["PatientId"]);
            IQCareMsgBox.NotifyAction(tabname + " Tab saved successfully.", "Laboratory Results", false, this, "");
            
        }
        private void SaveResultCancel(string tabname)
        {           
            clearsession();
            IQCareMsgBox.NotifyAction(tabname + " Tab saved successfully.", "Laboratory Results", true, this,false, "window.location.href='frm_LabTestResults.aspx'");
            
        }

        
        private void ClearTestInit()
        {
            //ddltestname.SelectedIndex = 0;
            //ddlselectspecimen.SelectedIndex = 0;
            //ddlTestPerfby.SelectedIndex = 0;
            //ddlState.SelectedIndex = 0;
            //ddlTestStatus.SelectedIndex = 0;
            //ddlrejreason.SelectedIndex = 0;
            //ddldepartment.SelectedIndex = 0;
            //txtCustSpecNo.Text = "";
            ////txtTestInitdt.DbSelectedDate = "";
            //txtrejreason.Text = "";
        }

        //protected void btnTestInitClear_Click(object sender, EventArgs e)
        //{
        //    ClearTestInit();
        //}

        //protected void btnSaveTestInit_Click(object sender, EventArgs e)
        //{
        //    DataTable thesvspcDt = (DataTable)Session["TestInitpDT"];
        //    if (thesvspcDt.Rows.Count > 0)
        //    {
        //        bool exists = thesvspcDt.AsEnumerable().Where(c => c.Field<string>("Flag").Equals("1")).Count() > 0;
        //        if (exists)
        //        {
        //            DataTable tblFiltered = thesvspcDt.AsEnumerable()
        //           .Where(row => row.Field<string>("Flag") == "1")
        //           .CopyToDataTable();
        //            thesvspcDt = new DataTable();
        //            thesvspcDt = RemoveColumnsTestInitTable(tblFiltered);
        //            ILabFunctions theILabManager = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
        //            int result = theILabManager.SaveUpdateTestInitDetails(thesvspcDt, Convert.ToInt32(Session["AppUserId"]));
        //            if (result > 0)
        //            {
        //                SaveCancel("Test Inititiation");
        //                tabControl.ActiveTabIndex = 3;
        //            }
        //        }
        //    }

        //}

        //protected void btnCloseTestInit_Click(object sender, EventArgs e)
        //{
        //    clearsession();
        //    Response.Redirect(Request.RawUrl);
        //}

        //protected void grdTestInit_ItemDataBound(object sender, GridItemEventArgs e)
        //{
        //    GridDataItem dataItem = e.Item as GridDataItem;

        //    if (dataItem != null)
        //    {
        //        Label lblFalg = (Label)dataItem.FindControl("lblTestInitFlag");
        //        if (lblFalg.Text == "1")
        //        {
        //            (dataItem["Delete"].Controls[0] as ImageButton).ImageUrl = "~/Images/del.gif";
        //        }
        //        else
        //        {
        //            dataItem["Delete"].Controls[0].Visible = false;
        //        }
        //    }
        //}

        //protected void ddltestname_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //}


        //protected void grdTestInit_DeleteCommand(object sender, GridCommandEventArgs e)
        //{
        //    GridDataItem dataItm = e.Item as GridDataItem;
        //    RadGrid radTestInit = (RadGrid)sender;

        //    Label lblID = (Label)dataItm.FindControl("lblTestInitID");
        //    string id = lblID.Text;
        //    DataTable table = (DataTable)Session["TestInitpDT"];
        //    table.PrimaryKey = new DataColumn[] { table.Columns["ID"] };

        //    if (table.Rows.Find(id) != null)
        //    {
        //        DataRow dr = table.Rows.Find(id);
        //        table.Rows.Find(id).Delete();
        //        table.AcceptChanges();
        //        Session["TestInitpDT"] = table;
        //        grdTestInit.DataSource = table;
        //        grdTestInit.DataBind();
        //    }
        //    else
        //    {
        //        grdTestInit.DataSource = (DataTable)Session["TestInitpDT"];
        //        grdTestInit.DataBind();

        //    }
        //}

        protected void gridSpecList_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            GridDataItem dataItm = e.Item as GridDataItem;
            RadGrid radgridSpecList = (RadGrid)sender;

            Label lblID = (Label)dataItm.FindControl("lblSpecID");
            string id = lblID.Text;
            DataTable table = (DataTable)Session["SpecRecpDT"];
            table.PrimaryKey = new DataColumn[] { table.Columns["ID"] };

            if (table.Rows.Find(id) != null)
            {
                DataRow dr = table.Rows.Find(id);
                table.Rows.Find(id).Delete();
                table.AcceptChanges();
                Session["SpecRecpDT"] = table;
                gridSpecList.DataSource = table;
                gridSpecList.DataBind();
            }
            else
            {
                gridSpecList.DataSource = (DataTable)Session["SpecRecpDT"];
                gridSpecList.DataBind();

            }
        }

        protected void BtnSaveLabResults_Click(object sender, EventArgs e)
        {
            SaveLabOrder();
        }

        protected void btnSpecClear_Click(object sender, EventArgs e)
        {
            ClearSpecimen();
        }

        protected void BtnCloseLabResults_Click(object sender, EventArgs e)
        {
            clearsession();
            Response.Redirect(Request.RawUrl);
        }





    }
}