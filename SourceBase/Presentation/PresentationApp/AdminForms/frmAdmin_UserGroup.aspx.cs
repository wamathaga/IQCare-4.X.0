using System;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using Application.Common;
using Application.Presentation;
using Interface.Administration;


/////////////////////////////////////////////////////////////////////
// Code Written By   : Rakhi Tyagi
// Written Date      : 1 Sept 2006
// Modification Date : 30 Oct 2006
// Description       : Add/Edit UserGroup  
//  Modification Date : 16 Feb 2007
/// /////////////////////////////////////////////////////////////////
namespace PresentationApp.AdminForms
{
    public partial class frmAdmin_UserGroup : LogPage
    {
        int GroupId;
        #region User Function
        private Boolean FieldValidations()
        {
            if (txtusergroupname.Text == "")
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["Control"] = "GroupName";
                IQCareMsgBox.Show("BlankTextBox", theMsg, this);
                return false;
            }
            if (TreeViewUserGroupForms.CheckedNodes.Count == 0)
            {

                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = "Please check whether parent node is checked";
                IQCareMsgBox.Show("#C1", theBuilder, this);
                return false;
            }

            return true;
        }

        private void AunthenticationFunction()
        {
            //RTyagi..19Feb 07..
            /***************** Check For User Rights ****************/
            AuthenticationManager Authentiaction = new AuthenticationManager();
            if (Request.QueryString["name"] == "Add")
            {
                if (Authentiaction.HasFunctionRight(ApplicationAccess.UserGroupAdministration, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
                {
                    btnsave.Enabled = false;
                }
            }
            else if (Request.QueryString["name"] == "Edit")
            {
                if (Authentiaction.HasFunctionRight(ApplicationAccess.UserGroupAdministration, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
                {
                    string theUrl = "frmAdmin_UserGroupList.aspx";
                    Response.Redirect(theUrl);
                }
                else if (Authentiaction.HasFunctionRight(ApplicationAccess.UserGroupAdministration, FunctionAccess.Update, (DataTable)Session["UserRight"]) == false)
                {
                    btnsave.Enabled = false;
                }
            }

        }


        public DataSet FacilityServiceUserGroupData(out int EditCareEnd, out int PIdentifiers)
        {
            /********** Declare Local Required Variables. **********/
            DataSet theDS = new DataSet();
            DataTable theUserGroupTbl = new DataTable();
            theUserGroupTbl.Columns.Add("FacilityID", typeof(string));
            theUserGroupTbl.Columns.Add("ModuleID", typeof(string));
            theUserGroupTbl.Columns.Add("FeatureID", typeof(string));
            theUserGroupTbl.Columns.Add("FeatureName", typeof(string));
            theUserGroupTbl.Columns.Add("TabID", typeof(string));
            theUserGroupTbl.Columns.Add("FunctionID", typeof(string));
            //Start For General Items
            string FacilityID = string.Empty;
            string FacilityName = string.Empty;
            string ModuleID = string.Empty;
            string ModuleName = string.Empty;

            foreach (RadTreeNode trFacility in TreeViewUserGroupForms.Nodes)
            {
                string FormID = string.Empty;
                string FormName = string.Empty;
                string TabID = string.Empty;
                string TabName = string.Empty;
                string FunctionID = string.Empty;
                string FunctionName = string.Empty;

                if (trFacility.Checked)
                {
                    FacilityID = trFacility.Value;
                    FacilityName = trFacility.Text;
                    if (trFacility.Nodes.Count > 0)
                    {
                        for (int i = 0; i < trFacility.Nodes.Count; i++)
                        {
                            if (trFacility.Nodes[i].Checked)
                            {
                                ModuleID = trFacility.Nodes[i].Value;
                                ModuleName = trFacility.Nodes[i].Text;

                                if (trFacility.Nodes[i].Nodes.Count > 0)
                                {
                                    for (int jFormType = 0; jFormType < trFacility.Nodes[i].Nodes.Count; jFormType++)
                                    {
                                        if (trFacility.Nodes[i].Nodes[jFormType].Checked)
                                        {
                                            FormID = trFacility.Nodes[i].Nodes[jFormType].Value;
                                            FormName = trFacility.Nodes[i].Nodes[jFormType].Text;
                                            if (trFacility.Nodes[i].Nodes[jFormType].Nodes.Count > 0)
                                            {
                                                for (int jTabType = 0; jTabType < trFacility.Nodes[i].Nodes[jFormType].Nodes.Count; jTabType++)
                                                {
                                                    if (trFacility.Nodes[i].Nodes[jFormType].Nodes[jTabType].Checked)
                                                    {
                                                        TabID = trFacility.Nodes[i].Nodes[jFormType].Nodes[jTabType].Value;
                                                        TabName = trFacility.Nodes[i].Nodes[jFormType].Nodes[jTabType].Text;
                                                        if (trFacility.Nodes[i].Nodes[jFormType].Nodes[jTabType].Nodes.Count > 0)
                                                        {
                                                            for (int jFunction = 0; jFunction < trFacility.Nodes[i].Nodes[jFormType].Nodes[jTabType].Nodes.Count; jFunction++)
                                                            {
                                                                if (trFacility.Nodes[i].Nodes[jFormType].Nodes[jTabType].Nodes[jFunction].Checked)
                                                                {
                                                                    FunctionID = trFacility.Nodes[i].Nodes[jFormType].Nodes[jTabType].Nodes[jFunction].Value;
                                                                    FunctionName = trFacility.Nodes[i].Nodes[jFormType].Nodes[jTabType].Nodes[jFunction].Text;
                                                                    theUserGroupTbl.Rows.Add(FacilityID, ModuleID, FormID, "", TabID, FunctionID);
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            FunctionID = trFacility.Nodes[i].Nodes[jFormType].Nodes[jTabType].Value;
                                                            FunctionName = trFacility.Nodes[i].Nodes[jFormType].Nodes[jTabType].Text;
                                                            theUserGroupTbl.Rows.Add(FacilityID, ModuleID, FormID, "", "", FunctionID);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else { theUserGroupTbl.Rows.Add(FacilityID, ModuleID, "0", "0", "0", "1"); }
                            }
                        }
                    }
                }
            }

            foreach (RadTreeNode trFacility in TreeViewUserGroupForms.Nodes)
            {
                if (trFacility.Checked)
                {
                    FacilityID = trFacility.Value;
                    //Common Items
                    foreach (RadTreeNode trFormsReports in TreeViewUserGroupCommonForms.Nodes)
                    {
                        string FormTypeID = string.Empty;
                        string FormTypeName = string.Empty;
                        string FormID = string.Empty;
                        string FormName = string.Empty;
                        string FunctionID = string.Empty;
                        string FunctionName = string.Empty;
                        string TabID = string.Empty;
                        string TabName = string.Empty;

                        if (trFormsReports.Checked)
                        {
                            FormTypeID = trFormsReports.Value;
                            FormTypeName = trFormsReports.Text;
                            if (trFormsReports.Nodes.Count > 0)
                            {
                                for (int i = 0; i < trFormsReports.Nodes.Count; i++)
                                {
                                    if (trFormsReports.Nodes[i].Checked)
                                    {
                                        FormID = trFormsReports.Nodes[i].Value;
                                        FormName = trFormsReports.Nodes[i].Text;

                                        if (trFormsReports.Nodes[i].Nodes.Count > 0)
                                        {

                                            for (int j = 0; j < trFormsReports.Nodes[i].Nodes.Count; j++)
                                            {
                                                if (trFormsReports.Nodes[i].Nodes[j].Nodes.Count > 0)
                                                {
                                                    ///////////////////////Tab///
                                                    TabID = trFormsReports.Nodes[i].Nodes[j].Value;
                                                    TabName = trFormsReports.Nodes[i].Nodes[j].Text;                                                    

                                                    for (int jFunction = 0; jFunction < trFormsReports.Nodes[i].Nodes[j].Nodes.Count; jFunction++)
                                                    {
                                                        if (trFormsReports.Nodes[i].Nodes[j].Nodes[jFunction].Checked)
                                                        {
                                                            FunctionID = trFormsReports.Nodes[i].Nodes[j].Nodes[jFunction].Value;
                                                            FunctionName = trFormsReports.Nodes[i].Nodes[j].Nodes[jFunction].Text;
                                                            theUserGroupTbl.Rows.Add(FacilityID, "0", FormID, "", TabID, FunctionID);
                                                        }
                                                    }

                                                }
                                                ////////////////////////////////////////
                                                else
                                                {
                                                    if (trFormsReports.Nodes[i].Nodes[j].Checked)
                                                    {
                                                        FunctionID = trFormsReports.Nodes[i].Nodes[j].Value;
                                                        FunctionName = trFormsReports.Nodes[i].Nodes[j].Text;
                                                        //theUserGroupTbl.Rows.Add("0", "0", FormID, "", "", FunctionID);
                                                        theUserGroupTbl.Rows.Add(FacilityID, "0", FormID, "", "", FunctionID);
                                                    }
                                                }
                                            }
                                        }
                                        
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //Customize List
            foreach (RadTreeNode trFormsCL in TreeViewUserGroupAdminForms.Nodes)
            {
                ModuleID = string.Empty;
                ModuleName = string.Empty;
                string FormID = string.Empty;
                string FormName = string.Empty;
                string TabID = string.Empty;
                string TabName = string.Empty;
                string FunctionID = string.Empty;
                string FunctionName = string.Empty;

                if (trFormsCL.Checked)
                {
                    ModuleID = trFormsCL.Value;
                    ModuleName = trFormsCL.Text;
                    if (trFormsCL.Nodes.Count > 0)
                    {
                        for (int i = 0; i < trFormsCL.Nodes.Count; i++)
                        {
                            if (trFormsCL.Nodes[i].Checked)
                            {
                                FormID = trFormsCL.Nodes[i].Value;
                                FormName = trFormsCL.Nodes[i].Text;
                                if (trFormsCL.Nodes[i].Nodes.Count > 0)
                                {
                                    for (int j = 0; j < trFormsCL.Nodes[i].Nodes.Count; j++)
                                    {
                                        if (trFormsCL.Nodes[i].Nodes[j].Checked)
                                        {
                                            FunctionID = trFormsCL.Nodes[i].Nodes[j].Value;
                                            FunctionName = trFormsCL.Nodes[i].Nodes[j].Text;
                                            theUserGroupTbl.Rows.Add("0", ModuleID, FormID, FormName, "", FunctionID);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            }
            theDS.Tables.Add(theUserGroupTbl);
            EditCareEnd = PIdentifiers = 0;
            foreach (RadTreeNode trFormsCL in TreeViewUserGroupSplPrivledges.Nodes)
            {
                if (trFormsCL.Checked)
                {
                    switch (trFormsCL.Value)
                    {
                        case "SPL001":
                            {
                                EditCareEnd = 1; // Set out parameter
                                break;
                            }
                        case "SPL002":
                            {
                                PIdentifiers = 1; // Set out parameter
                                break;
                            }

                    }

                }
            }

            return theDS;
        }
        #endregion
        private void GetFacilityServiceUserGroupData(DataSet theDS)
        {

            DataTable GetGeneralData = theDS.Tables[0];
            DataTable GetCLData = theDS.Tables[1];
            DataTable GetCommonData = theDS.Tables[2];
            DataTable GetSplData = theDS.Tables[3];
            if (GetGeneralData.Rows.Count > 0)
            {
                foreach (DataRow dr in GetGeneralData.Rows)
                {
                    string FacilityID = dr["FacilityId"].ToString();
                    string ModuleID = dr["ServiceId"].ToString();
                    string FeatureID = dr["FeatureID"].ToString();
                    string TabID = dr["TabId"].ToString();
                    string FunctionID = dr["FunctionID"].ToString();
                    foreach (RadTreeNode trFacility in TreeViewUserGroupForms.Nodes)
                    {
                        if (trFacility.Value == FacilityID)
                        {
                            //Facility Check
                            trFacility.Checked = true;
                            if (trFacility.Nodes.Count > 0)
                            {
                                for (int i = 0; i < trFacility.Nodes.Count; i++)
                                {
                                    if (trFacility.Nodes[i].Value == ModuleID)
                                    {
                                        //Service/ModuleChecked
                                        trFacility.Nodes[i].Checked = true;

                                        for (int jType = 0; jType < trFacility.Nodes[i].Nodes.Count; jType++)
                                        {
                                            if (trFacility.Nodes[i].Nodes[jType].Value == FeatureID)
                                            {
                                                //Form Checked
                                                trFacility.Nodes[i].Nodes[jType].Checked = true;
                                                for (int jTab = 0; jTab < trFacility.Nodes[i].Nodes[jType].Nodes.Count; jTab++)
                                                {
                                                    if (trFacility.Nodes[i].Nodes[jType].Nodes[jTab].Value == TabID)
                                                    {
                                                        //Tab Checked
                                                        trFacility.Nodes[i].Nodes[jType].Nodes[jTab].Checked = true;
                                                        trFacility.Nodes[i].Nodes[jType].Nodes[jTab].Expanded = true;

                                                        for (int jFun = 0; jFun < trFacility.Nodes[i].Nodes[jType].Nodes[jTab].Nodes.Count; jFun++)
                                                        {
                                                            if (trFacility.Nodes[i].Nodes[jType].Nodes[jTab].Nodes[jFun].Value == FunctionID)
                                                            {
                                                                trFacility.Nodes[i].Nodes[jType].Nodes[jTab].Nodes[jFun].Checked = true;
                                                                trFacility.Nodes[i].Nodes[jType].Nodes[jTab].Nodes[jFun].Expanded = true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (trFacility.Nodes[i].Nodes[jType].Nodes[jTab].Value == FunctionID)
                                                        {
                                                            trFacility.Nodes[i].Nodes[jType].Nodes[jTab].Checked = true;
                                                            trFacility.Nodes[i].Nodes[jType].Nodes[jTab].Expanded = true;
                                                        }

                                                    }
                                                }

                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
            }
            if (GetCLData.Rows.Count > 0)
            {
                foreach (DataRow dr in GetCLData.Rows)
                {
                    string FacilityID = dr["FacilityId"].ToString();
                    string ModuleID = dr["ServiceId"].ToString();
                    string FeatureID = dr["FeatureID"].ToString();
                    string FeatureName = dr["FeatureName"].ToString();
                    string TabID = dr["TabId"].ToString();
                    string FunctionID = dr["FunctionID"].ToString();
                    foreach (RadTreeNode trService in TreeViewUserGroupAdminForms.Nodes)
                    {
                        if (trService.Value == ModuleID)
                        {
                            //Service Check
                            trService.Checked = true;
                            if (trService.Nodes.Count > 0)
                            {
                                for (int m = 0; m < trService.Nodes.Count; m++)
                                {
                                    if (trService.Nodes[m].Value == FeatureID && trService.Nodes[m].Text == FeatureName)
                                    {
                                        trService.Nodes[m].Checked = true;
                                        for (int n = 0; n < trService.Nodes[m].Nodes.Count; n++)
                                        {
                                            if (trService.Nodes[m].Nodes[n].Value == FunctionID)
                                            {
                                                trService.Nodes[m].Nodes[n].Checked = true;
                                                trService.Nodes[m].Nodes[n].Expanded = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (GetCommonData.Rows.Count > 0)
            {
                foreach (DataRow dr in GetCommonData.Rows)
                {
                    string FormType = dr["FormType"].ToString();
                    string FacilityID = dr["FacilityId"].ToString();
                    string ModuleID = dr["ServiceId"].ToString();
                    string FeatureID = dr["FeatureID"].ToString();
                    string TabID = dr["TabId"].ToString();
                    string FunctionID = dr["FunctionID"].ToString();
                    foreach (RadTreeNode trCommonItem in TreeViewUserGroupCommonForms.Nodes)
                    {
                        if (trCommonItem.Value == FormType)
                        {
                            //Common Check
                            trCommonItem.Checked = true;
                            if (trCommonItem.Nodes.Count > 0)
                            {
                                for (int i = 0; i < trCommonItem.Nodes.Count; i++)
                                {
                                    if (trCommonItem.Nodes[i].Value == FeatureID)
                                    {
                                        trCommonItem.Nodes[i].Checked = true;
                                        for (int jType = 0; jType < trCommonItem.Nodes[i].Nodes.Count; jType++)
                                        {
                                            if (trCommonItem.Nodes[i].Nodes[jType].Nodes.Count > 0)
                                            {
                                                if (trCommonItem.Nodes[i].Nodes[jType].Value == TabID)
                                                {
                                                    //Tab Checked
                                                    trCommonItem.Nodes[i].Nodes[jType].Checked = true;
                                                    trCommonItem.Nodes[i].Nodes[jType].Expanded = true;

                                                    for (int jFun = 0; jFun < trCommonItem.Nodes[i].Nodes[jType].Nodes.Count; jFun++)
                                                    {
                                                        if (trCommonItem.Nodes[i].Nodes[jType].Nodes[jFun].Value == FunctionID)
                                                        {
                                                            trCommonItem.Nodes[i].Nodes[jType].Nodes[jFun].Checked = true;
                                                            trCommonItem.Nodes[i].Nodes[jType].Nodes[jFun].Expanded = true;
                                                        }
                                                    }
                                                }
                                            }
                                            else if (trCommonItem.Nodes[i].Nodes[jType].Value == FunctionID)
                                            {
                                                //Form Checked
                                                trCommonItem.Nodes[i].Nodes[jType].Checked = true;
                                                trCommonItem.Nodes[i].Nodes[jType].Expanded = true;
                                            }
                                        }
                                    }
                                    
                                }
                            }
                        }
                    }
                }
            }

            if (GetSplData.Rows.Count > 0)
            {
                foreach (DataRow dr in GetSplData.Rows)
                {
                    string CareEndFlag = dr["CareEndFlag"].ToString();
                    string IdentifierFlag = dr["IdentifierFlag"].ToString();
                    foreach (RadTreeNode trSplPriviledges in TreeViewUserGroupSplPrivledges.Nodes)
                    {
                        if (trSplPriviledges.Value == "SPL001" && CareEndFlag == "1")
                        {
                            trSplPriviledges.Checked = true;
                        }
                        else if (trSplPriviledges.Value == "SPL002" && IdentifierFlag == "1")
                        {
                            trSplPriviledges.Checked = true;
                        }

                    }
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
            {
                IQCareMsgBox.Show("SessionExpired", this);
                Response.Redirect("~/frmlogin.aspx", true);
            }
            //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Visible = false;
           // (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "User Group Administration";

            if (Request.QueryString["name"] != null)
            {
                lblh3.Text = Request.QueryString["name"];
            }
            try
            {
                if (Page.IsPostBack != true)
                {
                    if (Request.QueryString["name"] != null)
                    {
                        AunthenticationFunction();
                    }
                    if (lblh3.Text == "Add")
                    {
                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["Name"] = "User Group Roles";
                        IQCareMsgBox.ShowConfirm("UserGroupDetailSaveRecord", theBuilder, btnsave);
                        lblh3.Text = "Add User Group";
                    }
                    else
                    {
                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["Name"] = "User Group Roles";
                        IQCareMsgBox.ShowConfirm("UserGroupDetailUpdateRecord", theBuilder, btnsave);
                        lblh3.Text = "Edit User Group";
                    }

                    if (Request.QueryString["name"] != null && Request.QueryString["name"] == "Edit")
                    {
                        GroupId = Convert.ToInt32(Request.QueryString["GroupID"]);
                        IUserRole UserGroupManager;
                        UserGroupManager = (IUserRole)ObjectFactory.CreateInstance("BusinessProcess.Administration.BUserRole, BusinessProcess.Administration");
                        DataSet theDS = UserGroupManager.GetUserGroupFeatureList(Convert.ToInt32(Session["SystemId"]), 0);
                        BindFunctions BindManager = new BindFunctions();
                        DataView DVDD = new DataView(theDS.Tables[4]);
                        DVDD.RowFilter = "GroupID <> " + GroupId + "";
                        BindManager.BindCombo(ddGroupName, DVDD.ToTable(), "GroupName", "GroupID");
                        ViewState["DataPopulate"] = theDS;
                        GetGeneralDataforTreeView(theDS.Tables[1]);
                        GetModuleCustomListTreeView(theDS.Tables[3]);
                        GetCommonItemsinTreeView(theDS.Tables[0]);
                        GetSPlPriviledgesinTreeView(theDS.Tables[5]);
                        DataSet theOtherDS = UserGroupManager.GetUserGroupFeatureListByID(GroupId);
                        GetFacilityServiceUserGroupData(theOtherDS);
                        txtusergroupname.Text = Request.QueryString["Grpnm"].ToString();
                    }
                    else if (Request.QueryString["name"] != null && Request.QueryString["name"] == "Add")
                    {
                        IUserRole UserGroupManager;
                        UserGroupManager = (IUserRole)ObjectFactory.CreateInstance("BusinessProcess.Administration.BUserRole, BusinessProcess.Administration");
                        DataSet theDS = UserGroupManager.GetUserGroupFeatureList(Convert.ToInt32(Session["SystemId"]), 0);
                        BindFunctions BindManager = new BindFunctions();
                        DataView DVDD = new DataView(theDS.Tables[4]);
                        DVDD.RowFilter = "GroupID <> " + GroupId + "";
                        BindManager.BindCombo(ddGroupName, DVDD.ToTable(), "GroupName", "GroupID");
                        ViewState["DataPopulate"] = theDS;
                        GetGeneralDataforTreeView(theDS.Tables[1]);
                        GetModuleCustomListTreeView(theDS.Tables[3]);
                        GetCommonItemsinTreeView(theDS.Tables[0]);
                        GetSPlPriviledgesinTreeView(theDS.Tables[5]);
                    }
                }
            }
            catch (Exception err)
            {
                MsgBuilder theMsgBuilder = new MsgBuilder();
                theMsgBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theMsgBuilder, this);
                return;
            }
            finally
            {
            }
        }
        private void GetGeneralDataforTreeView(DataTable theDT)
        {
            try
            {
                DataTable theDTFacilityNames = theDT;
                DataView theDV = new DataView(theDT);
                theDV.Sort = "ModuleName ASC";
                DataTable theDTServiceNames = theDV.ToTable();
                theDV = new DataView(theDT);
                theDV.Sort = "FeatureName ASC";
                DataTable theDTForms = theDV.ToTable();
                theDV = new DataView(theDT);
                theDV.Sort = "TabName ASC";
                DataTable theDTTabs = theDV.ToTable();

                string FacilityID = string.Empty;
                string ModuleID = string.Empty;
                string FeatureID = string.Empty;
                foreach (DataRow dr in theDTFacilityNames.Rows)
                {
                    RadTreeNode tn = new RadTreeNode();
                    if (FacilityID != dr["facilityid"].ToString())
                    {
                        tn.Text = dr["FacilityName"].ToString();
                        tn.Value = dr["Facilityid"].ToString();
                        tn.ImageUrl = "~/Images/Treeview-grey-horizontal-line.png";
                        FacilityID = dr["facilityid"].ToString();
                        tn.HoveredImageUrl = "~/Images/Treeview-grey-horizontal-line.png";
                        TreeViewUserGroupForms.Nodes.Add(tn);
                        ModuleID = string.Empty;
                        //RadTreeNode tn00 = new RadTreeNode();
                        //tn00.Text = "Records";
                        //tn00.Value = "0";
                        //ModuleID = "0";
                        //tn00.ImageUrl = "~/Images/Treeview-grey-horizontal-line.png";
                        //tn00.Checked = true;
                        //tn00.Enabled = false;
                        //tn.Nodes.Add(tn00);
                        foreach (DataRow dr0 in theDTServiceNames.Rows)
                        {

                            if (FacilityID == dr0["facilityid"].ToString())
                            {
                                if (ModuleID != dr0["ModuleId"].ToString())
                                {
                                    RadTreeNode tn0 = new RadTreeNode();
                                    tn0.Text = dr0["ModuleName"].ToString();
                                    tn0.Value = dr0["ModuleId"].ToString();
                                    ModuleID = dr0["ModuleId"].ToString();
                                    tn0.ImageUrl = "~/Images/Treeview-grey-horizontal-line.png";
                                    tn.Nodes.Add(tn0);
                                    foreach (DataRow dr1 in theDTForms.Rows)
                                    {
                                        if (dr1["facilityid"].ToString() == FacilityID)
                                        {
                                            if (dr1["ModuleId"].ToString() == ModuleID)
                                            {
                                                if (FeatureID != dr1["FeatureID"].ToString())
                                                {
                                                    RadTreeNode tn1 = new RadTreeNode();
                                                    tn1.Text = dr1["FeatureName"].ToString();
                                                    tn1.Value = dr1["FeatureID"].ToString();
                                                    FeatureID = dr1["FeatureID"].ToString();
                                                    tn1.ImageUrl = "~/Images/Treeview-grey-horizontal-line.png";
                                                    if (tn1.Text != "")
                                                    {
                                                        tn0.Nodes.Add(tn1);
                                                        foreach (DataRow drTab in theDTTabs.Rows)
                                                        {
                                                            if (drTab["facilityid"].ToString() == FacilityID)
                                                            {
                                                                if (drTab["ModuleId"].ToString() == ModuleID)
                                                                {
                                                                    if (drTab["FeatureID"].ToString() == FeatureID)
                                                                    {
                                                                        if (drTab["TabID"].ToString() != "0")
                                                                        {
                                                                            RadTreeNode tnTab = new RadTreeNode();
                                                                            tnTab.Text = drTab["TabName"].ToString();
                                                                            tnTab.Value = drTab["TabId"].ToString();
                                                                            tnTab.ImageUrl = "~/Images/Treeview-grey-horizontal-line.png";
                                                                            tn1.Nodes.Add(tnTab);
                                                                            GetFunctionDetailsforTreeView(tnTab);
                                                                        }
                                                                        else
                                                                        {
                                                                            GetFunctionDetailsforTreeView(tn1);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }

                                                }

                                            }
                                        }
                                    }
                                }
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
            }
        }
        private void GetCommonItemsinTreeView(DataTable theDT)
        {
            DataTable dtName = theDT;
            DataView theDV = dtName.DefaultView;
            theDV.Sort = "FeatureName ASC";
            DataTable dtForm = theDV.ToTable();
            theDV = new DataView(theDT);
            theDV.Sort = "TabName ASC";
            DataTable theDTTabs = theDV.ToTable();

            string NameID = string.Empty;
            string FeatureID = string.Empty;
            foreach (DataRow dr in dtName.Rows)
            {
                RadTreeNode tn = new RadTreeNode();
                if (NameID != dr["FormID"].ToString())
                {
                    tn.Text = dr["NAME"].ToString();
                    tn.Value = dr["FormID"].ToString();
                    tn.ImageUrl = "~/Images/Treeview-grey-horizontal-line.png";
                    NameID = dr["FormID"].ToString();
                    tn.HoveredImageUrl = "~/Images/Treeview-grey-horizontal-line.png";
                    TreeViewUserGroupCommonForms.Nodes.Add(tn);
                    foreach (DataRow drForm in dtForm.Rows)
                    {
                        if (drForm["FormID"].ToString() == NameID)
                        {
                            if (FeatureID != drForm["FeatureID"].ToString())
                            {
                                RadTreeNode tnForm = new RadTreeNode();
                                tnForm.Selected = true;
                                tnForm.Text = drForm["FeatureName"].ToString();
                                tnForm.Value = drForm["FeatureID"].ToString();
                                FeatureID = drForm["FeatureID"].ToString();
                                tnForm.ImageUrl = "~/Images/Treeview-grey-horizontal-line.png";
                                tn.Nodes.Add(tnForm);
                                //if (NameID == "REP001")
                                //{
                                //    RadTreeNode tnFunc = new RadTreeNode();
                                //    tnFunc.Attributes.Add("flow", "horizontal");
                                //    tn.ImageUrl = "~/Images/Treeview-grey-horizontal-line.png";
                                //    tnFunc.ImageUrl = "~/Images/Treeview-grey-horizontal-line.png";
                                //    tnFunc.Text = "View";
                                //    tnFunc.Value = "1";
                                //    tnForm.Nodes.Add(tnFunc);
                                //}
                                foreach (DataRow drTab in theDTTabs.Rows)
                                {
                                    if (drTab["FeatureID"].ToString() == FeatureID)
                                    {
                                        if (drTab["TabID"].ToString() != "0")
                                        {
                                            RadTreeNode tnTab = new RadTreeNode();
                                            tnTab.Text = drTab["TabName"].ToString();
                                            tnTab.Value = drTab["TabId"].ToString();
                                            tnTab.ImageUrl = "~/Images/Treeview-grey-horizontal-line.png";
                                            tnForm.Nodes.Add(tnTab);
                                            GetFunctionDetailsforTreeView(tnTab);
                                        }
                                        else
                                        {
                                            GetFunctionDetailsforTreeView(tnForm);
                                        }
                                    }

                                }

                            }


                        }
                    }
                }
            }
        }
        private void GetSPlPriviledgesinTreeView(DataTable theDT)
        {
            DataTable dtName = theDT;
            DataView theDV = dtName.DefaultView;
            theDV.Sort = "FeatureName ASC";
            DataTable dtForm = theDV.ToTable();
            string NameID = string.Empty;
            string FeatureID = string.Empty;
            foreach (DataRow dr in dtName.Rows)
            {
                RadTreeNode tn = new RadTreeNode();
                if (NameID != dr["FormID"].ToString())
                {
                    tn.Text = dr["FeatureName"].ToString();
                    tn.Value = dr["FormID"].ToString();
                    tn.ImageUrl = "~/Images/Treeview-grey-horizontal-line.png";
                    NameID = dr["FormID"].ToString();
                    tn.HoveredImageUrl = "~/Images/Treeview-grey-horizontal-line.png";
                    TreeViewUserGroupSplPrivledges.Nodes.Add(tn);
                }

            }
        }
        private void GetModuleCustomListTreeView(DataTable theDT)
        {
            if (Convert.ToInt32(Session["SystemId"]) == 3)
            {
                DataTable dtName = theDT;
                string NameID = string.Empty;
                RadTreeNode tn = new RadTreeNode();
                tn.ImageUrl = "~/Images/Treeview-grey-horizontal-line.png";
                tn.Text = "Paediatric ART";
                tn.Value = "205";
                NameID = "205";
                TreeViewUserGroupAdminForms.Nodes.Add(tn);
                foreach (DataRow dr0 in dtName.Rows)
                {
                    if (NameID == dr0["ModuleId"].ToString())
                    {
                        RadTreeNode tn0 = new RadTreeNode();
                        tn0.ImageUrl = "~/Images/Treeview-grey-horizontal-line.png";
                        tn0.Text = dr0["FeatureNAME"].ToString();
                        tn0.Value = dr0["FeatureID"].ToString();
                        tn.Nodes.Add(tn0);
                        GetFunctionDetailsforCLTreeView(tn0);
                    }
                }



            }
            else
            {
                DataTable dtName = theDT;
                string NameID = string.Empty;
                foreach (DataRow dr in dtName.Rows)
                {
                    RadTreeNode tn = new RadTreeNode();
                    if (NameID != dr["ModuleId"].ToString())
                    {
                        tn.ImageUrl = "~/Images/Treeview-grey-horizontal-line.png";
                        tn.Text = dr["ModuleNAME"].ToString();
                        tn.Value = dr["ModuleID"].ToString();
                        NameID = dr["ModuleID"].ToString();
                        TreeViewUserGroupAdminForms.Nodes.Add(tn);
                        foreach (DataRow dr0 in dtName.Rows)
                        {
                            if (NameID == dr0["ModuleId"].ToString())
                            {
                                RadTreeNode tn0 = new RadTreeNode();
                                tn0.ImageUrl = "~/Images/Treeview-grey-horizontal-line.png";
                                tn0.Text = dr0["FeatureNAME"].ToString();
                                tn0.Value = dr0["FeatureID"].ToString();
                                tn.Nodes.Add(tn0);
                                GetFunctionDetailsforCLTreeView(tn0);
                            }
                        }

                    }
                }


            }
        }
        private void GetFunctionDetailsforTreeView(RadTreeNode trTabChildNode)
        {
            DataSet theDS = null;
            if (ViewState["DataPopulate"] != null)
            {
                theDS = (DataSet)ViewState["DataPopulate"];
            }
            DataTable dtTabArea = theDS.Tables[2];
            foreach (DataRow drType in dtTabArea.Rows)
            {
                RadTreeNode trTypeChild = new RadTreeNode();
                trTabChildNode.Attributes.Add("flow", "horizontal");
                trTabChildNode.ImageUrl = "~/Images/Treeview-grey-horizontal-line.png";
                trTypeChild.ImageUrl = "~/Images/Treeview-grey-horizontal-line.png";
                trTypeChild.Text = drType["FunctionName"].ToString();
                trTypeChild.Value = drType["FunctionID"].ToString();
                trTabChildNode.Nodes.Add(trTypeChild);
            }
        }
        private void GetFunctionDetailsforCLTreeView(RadTreeNode trTabChildNode)
        {
            DataSet theDS = null;
            if (ViewState["DataPopulate"] != null)
            {
                theDS = (DataSet)ViewState["DataPopulate"];
            }
            DataTable dtTabArea = theDS.Tables[2];
            foreach (DataRow drType in dtTabArea.Rows)
            {
                if (Convert.ToInt32(drType["FunctionID"]) == 2)
                {
                    RadTreeNode trTypeChild = new RadTreeNode();
                    trTabChildNode.Attributes.Add("flow", "horizontal");
                    trTabChildNode.ImageUrl = "~/Images/Treeview-grey-horizontal-line.png";
                    trTypeChild.ImageUrl = "~/Images/Treeview-grey-horizontal-line.png";
                    trTypeChild.Text = drType["FunctionName"].ToString();
                    trTypeChild.Value = drType["FunctionID"].ToString();
                    trTabChildNode.Nodes.Add(trTypeChild);
                }
                else if (Convert.ToInt32(drType["FunctionID"]) == 4)
                {
                    RadTreeNode trTypeChild = new RadTreeNode();
                    trTabChildNode.Attributes.Add("flow", "horizontal");
                    trTabChildNode.ImageUrl = "~/Images/Treeview-grey-horizontal-line.png";
                    trTypeChild.ImageUrl = "~/Images/Treeview-grey-horizontal-line.png";
                    trTypeChild.Text = drType["FunctionName"].ToString();
                    trTypeChild.Value = drType["FunctionID"].ToString();
                    trTabChildNode.Nodes.Add(trTypeChild);
                }
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            string theUrl;
            theUrl = "frmAdmin_UserGroupList.aspx";
            Response.Redirect(theUrl);
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (FieldValidations() == false)
            {
                return;
            }
            int UserGroupID = 0;
            int Flag = 0;
            int EditCareEnd = 0;
            int PIdentifiers = 0;
            DataSet theDS = new DataSet();
            IUserRole UserRoleManager = (IUserRole)ObjectFactory.CreateInstance("BusinessProcess.Administration.BUserRole, BusinessProcess.Administration");
            try
            {
                if (Request.QueryString["name"] == "Add")
                {
                    GroupId = 0;
                    int EnrollmentPrivilage = 1;
                    theDS = FacilityServiceUserGroupData(out EditCareEnd, out PIdentifiers);
                    int CareEndPrivilage = EditCareEnd;
                    int EditIdentifierPrivilage = PIdentifiers;
                    //if (theDS.Tables[0].Rows.Count <= 0 && CareEndPrivilage == 0 && EditIdentifierPrivilage == 0)
                    //{
                    //    IQCareMsgBox.Show("BlankRow", this);
                    //    return;
                    //}

                    UserGroupID = (int)UserRoleManager.SaveUserGroupDetail(GroupId, txtusergroupname.Text, theDS, Convert.ToInt32(Session["AppUserId"].ToString()), Flag, EnrollmentPrivilage, CareEndPrivilage, EditIdentifierPrivilage);
                    if (UserGroupID == 0)
                    {
                        IQCareMsgBox.Show("UserGroupDetailExists", this);
                        return;
                    }
                    else
                    {
                        IQCareMsgBox.Show("UserGroupDetailSave", this);
                    }
                }
                else if (Request.QueryString["name"] == "Edit")
                {
                    int EnrollmentPrivilage = 1;
                    GroupId = Convert.ToInt32(Request.QueryString["GroupID"]);
                    Flag = 1;
                    theDS = FacilityServiceUserGroupData(out EditCareEnd, out PIdentifiers);
                    int CareEndPrivilage = EditCareEnd;
                    int EditIdentifierPrivilage = PIdentifiers;
                    //if (theDS.Tables[0].Rows.Count <= 0 && CareEndPrivilage == 0 && EditIdentifierPrivilage == 0)
                    //{
                    //    IQCareMsgBox.Show("BlankRow", this);
                    //    return;
                    //}
                    UserRoleManager.UpdateUserGroup(GroupId, txtusergroupname.Text, theDS, Convert.ToInt32(Session["AppUserId"].ToString()), Flag, EnrollmentPrivilage, CareEndPrivilage, EditIdentifierPrivilage);
                    IQCareMsgBox.Show("UserGroupDetailUpdate", this);
                }
                string theUrl = "frmAdmin_UserGroupList.aspx";
                Response.Redirect(theUrl);

            }
            catch (Exception err)
            {
                err.ToString();
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = "Please check whether parent node is checked";
                IQCareMsgBox.Show("#C1", theBuilder, this);
                return;
            }
            finally
            {
                UserRoleManager = null;
            }
        }

        protected void btnback_Click(object sender, EventArgs e)
        {

        }

        protected void ddGroupName_SelectedIndexChanged(object sender, EventArgs e)
        {
            GroupId = Convert.ToInt32(ddGroupName.SelectedValue);
            IUserRole UserGroupManager;
            UserGroupManager = (IUserRole)ObjectFactory.CreateInstance("BusinessProcess.Administration.BUserRole, BusinessProcess.Administration");
            DataSet theOtherDS = UserGroupManager.GetUserGroupFeatureListByID(GroupId);
            GetFacilityServiceUserGroupData(theOtherDS);
        }


        protected void rgdrugmain_ItemCreated(object sender, GridItemEventArgs e)
        {



        }

    }
}
