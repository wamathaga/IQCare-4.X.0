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

namespace PresentationApp.AdminForms
{
    public partial class frmAdmin_TreeUserGroup : LogPage
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
            foreach (RadTreeNode trFacility in TreeViewUserGroupForms.Nodes)
            {
                string FacilityID = string.Empty;
                string FacilityName = string.Empty;
                string ModuleID = string.Empty;
                string ModuleName = string.Empty;
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
                            }
                        }
                    }
                }
            }
            //Common Items
            foreach (RadTreeNode trFormsReports in TreeViewUserGroupCommonForms.Nodes)
            {
                string FormTypeID = string.Empty;
                string FormTypeName = string.Empty;
                string FormID = string.Empty;
                string FormName = string.Empty;
                string FunctionID = string.Empty;
                string FunctionName = string.Empty;

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
                                        if (trFormsReports.Nodes[i].Nodes[j].Checked)
                                        {
                                            FunctionID = trFormsReports.Nodes[i].Nodes[j].Value;
                                            FunctionName = trFormsReports.Nodes[i].Nodes[j].Text;
                                            theUserGroupTbl.Rows.Add("0", "0", FormID, "", "", FunctionID);
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
                string ModuleID = string.Empty;
                string ModuleName = string.Empty;
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
                        if (trService.Value == ModuleID && FacilityID == "0")
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
                        if (trCommonItem.Value == FormType && FacilityID == "0")
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
                                            if (trCommonItem.Nodes[i].Nodes[jType].Value == FunctionID)
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
                Response.Redirect("~/frmlogin.aspx",true);
            }
            //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Visible = false;
            //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "User Group Administration";

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
                        DataSet theDS = UserGroupManager.GetUserGroupFeatureList(1, 0);
                        BindFunctions BindManager = new BindFunctions();
                        DataView DVDD = new DataView(theDS.Tables[4]);
                        DVDD.RowFilter = "GroupID <> " + GroupId + "";
                        BindManager.BindCombo(ddGroupName, DVDD.ToTable(), "GroupName", "GroupID");
                        ViewState["DataPopulate"] = theDS;
                        //GetGeneralDataforTreeView(theDS.Tables[1]);
                        //GetModuleCustomListTreeView(theDS.Tables[3]);
                        //GetCommonItemsinTreeView(theDS.Tables[0]);
                        //GetSPlPriviledgesinTreeView(theDS.Tables[5]);
                        DataSet theOtherDS = UserGroupManager.GetUserGroupFeatureListByID(GroupId);
                        GetFacilityServiceUserGroupData(theOtherDS);
                        txtusergroupname.Text = Request.QueryString["Grpnm"].ToString();
                    }
                    else if (Request.QueryString["name"] != null && Request.QueryString["name"] == "Add")
                    {
                        IUserRole UserGroupManager;
                        UserGroupManager = (IUserRole)ObjectFactory.CreateInstance("BusinessProcess.Administration.BUserRole, BusinessProcess.Administration");
                        DataSet theDS = UserGroupManager.GetUserGroupFeatureList(1, 0);
                        BindFunctions BindManager = new BindFunctions();
                        DataView DVDD = new DataView(theDS.Tables[4]);
                        DVDD.RowFilter = "GroupID <> " + GroupId + "";
                        BindManager.BindCombo(ddGroupName, DVDD.ToTable(), "GroupName", "GroupID");
                        ViewState["DataPopulate"] = theDS;
                        //GetGeneralDataforTreeView(theDS.Tables[1]);
                        //GetModuleCustomListTreeView(theDS.Tables[3]);
                        //GetCommonItemsinTreeView(theDS.Tables[0]);
                        //GetSPlPriviledgesinTreeView(theDS.Tables[5]);
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

    }
}