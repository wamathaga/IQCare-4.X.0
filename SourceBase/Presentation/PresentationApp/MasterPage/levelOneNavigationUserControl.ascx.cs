using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Data;

using Application.Presentation;
using Interface.Security;
using Application.Common;

public partial class MasterPage_levelOneNavigationUserControl : System.Web.UI.UserControl
{
    AuthenticationManager Authentication = new AuthenticationManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["SystemId"] = null;
        //if (Session["SystemId"] == null)
        //    Response.Redirect("~/frmlogoff.aspx");

        if (Session["SystemId"] !=null && Convert.ToString(Session["SystemId"]) == "3")
        {
            AuthenticatePASDP();
        }
        
        AuthenticateRights();
        CreatePlugInMenu();
    }

    #region "Hide menu item by value"
    public void RemoveMenuItemByValue(MenuItemCollection items, String value)
    {
        List<MenuItem> rmvMenuItem = new List<MenuItem>();

        //Breadth first, look in the collection
        foreach (MenuItem item in items)
        {
            if (item.Value == value)
            {
                rmvMenuItem.Add(item);
            }
        }

        if (rmvMenuItem.ToArray().Length != 0)
        {
            for (int j = 0; j < rmvMenuItem.ToArray().Length; j++)
            {
                items.Remove(rmvMenuItem[j]);
            }
        }

        //Search children
        foreach (MenuItem item in items)
        {
            RemoveMenuItemByValue(item.ChildItems, value);
        }
    }
    #endregion

    #region "Assign URL by value"
    public void AssignUrl(MenuItemCollection items, String value, String url)
    {
        foreach (MenuItem item in items)
        {
            if (item.Value == value)
            {
                item.NavigateUrl = url;
            }
        }

        foreach (MenuItem item in items)
        {
            AssignUrl(item.ChildItems, value, url);
        }
    }
    #endregion

    #region "Hiding Links for PASDP"

    private void AuthenticatePASDP()
    {
        foreach (MenuItem item in mainMenu.Items)
        {
            if (item.Value == "Find/Add Patient")
            {
                item.Enabled = false;
            }
            else if (item.Value == "Reports")
            {
                item.Enabled = false;
            }
            else if (item.Value == "Scheduler")
            {
                item.Enabled = false;
            }
            else if (item.Value == "Facility Home")
            {
                item.Enabled = false;
            }

            else if (item.Value == "Administration")
            {
                foreach (MenuItem items in item.ChildItems)
                {
                    if (items.Value == "Delete Patient")
                    {
                        items.Enabled = false;
                    }
                    else if (items.Value == "Configure Custom Fields")
                    {
                        items.Enabled = false;
                    }
                    else if (items.Value == "Export")
                    {
                        items.Enabled = false;
                    }
                }
            }
        }
    }


    #endregion

    #region "User Functions ReportHeader footer master"
    private void AuthenticateRights()
    {

        if (!String.IsNullOrEmpty(Session["UserRight"].ToString()))
        {
            if (Authentication.HasFeatureRight(ApplicationAccess.FacilitySetup, (DataTable)Session["UserRight"]) == false)
            {
                //mnuAdminFacility.Visible = false;
                RemoveMenuItemByValue(mainMenu.Items, "Facility Setup");
            }
            //if (Authentication.HasFeatureRight(ApplicationAccess.CustomiseDropDown, (DataTable)Session["UserRight"]) == false)
            //{
            //    mnuAdminCustom.Visible = false;
            //}
            if (Authentication.HasFeatureRight(ApplicationAccess.UserAdministration, (DataTable)Session["UserRight"]) == false)
            {
                //mnuAdminUser.Visible = false;
                RemoveMenuItemByValue(mainMenu.Items, "User Administration");
            }
            if (Authentication.HasFeatureRight(ApplicationAccess.UserGroupAdministration, (DataTable)Session["UserRight"]) == false)
            {
                //mnuAdminUserGroup.Visible = false;
                RemoveMenuItemByValue(mainMenu.Items, "User Group Administration");
            }
            if (Authentication.HasFeatureRight(ApplicationAccess.DeletePatient, (DataTable)Session["UserRight"]) == false)
            {
                //mnuAdminDeletePatient.Visible = false;
                RemoveMenuItemByValue(mainMenu.Items, "Delete Patient");
            }
            if (Authentication.HasFeatureRight(ApplicationAccess.CustomReports, (DataTable)Session["UserRight"]) == false)
            {
                //mnuAdminCustomReport.Visible = false;
                RemoveMenuItemByValue(mainMenu.Items, "Custom Reports");
            }
            if (Authentication.HasFeatureRight(ApplicationAccess.FacilityReports, (DataTable)Session["UserRight"]) == false)
            {
                //mnuAdminFacilityReport.Visible = false;
                RemoveMenuItemByValue(mainMenu.Items, "Facility Reports");
            }
            if (Authentication.HasFeatureRight(ApplicationAccess.DonorReports, (DataTable)Session["UserRight"]) == false)
            {
                //mnuAdminDonorReport.Visible = false;
                RemoveMenuItemByValue(mainMenu.Items, "Donor Reports");
            }
            if (Authentication.HasFeatureRight(ApplicationAccess.QueryBuilderReports, (DataTable)Session["UserRight"]) == false)
            {
                RemoveMenuItemByValue(mainMenu.Items, "Query Builder Reports");
            }
            if (Authentication.HasFeatureRight(ApplicationAccess.Schedular, (DataTable)Session["UserRight"]) == false)
            {
                //mnuSchedular.Visible = false;
                RemoveMenuItemByValue(mainMenu.Items, "Scheduler");
            }
            if (Authentication.HasFeatureRight(ApplicationAccess.ConfigureCustomFields, (DataTable)Session["UserRight"]) == false)
            {
                //mnuAdminCustomConfig.Visible = false;
                RemoveMenuItemByValue(mainMenu.Items, "Configure Custom Fields");
            }
            if (Authentication.HasFeatureRight(ApplicationAccess.IQToolsReports, (DataTable)Session["UserRight"]) == false)
            {
                //mnuAdminCustomConfig.Visible = false;
                RemoveMenuItemByValue(mainMenu.Items, "IQToolsReportsmain");
            }

            if (Authentication.HasFeatureRight(ApplicationAccess.AuditTrail, (DataTable)Session["UserRight"]) == false)
            {
                RemoveMenuItemByValue(mainMenu.Items, "AuditTrail");
            }
            if (Authentication.HasFeatureRight(ApplicationAccess.Export, (DataTable)Session["UserRight"]) == false)
            {
                RemoveMenuItemByValue(mainMenu.Items, "Export");
            }
            if (Authentication.HasFeatureRight(ApplicationAccess.Backupsetup, (DataTable)Session["UserRight"]) == false)
            {
                RemoveMenuItemByValue(mainMenu.Items, "Backup/Restore setup");
            }
            if (Authentication.HasFeatureRight(ApplicationAccess.Backuprestore, (DataTable)Session["UserRight"]) == false)
            {
                RemoveMenuItemByValue(mainMenu.Items, "Backup/Restore Database");
            }
            //TODO Added by Naveen 2015-02-04 for billing menu authentification
            if (Authentication.HasFeatureRight(ApplicationAccess.Billing, (DataTable)Session["UserRight"]) == false)
            {
                RemoveMenuItemByValue(mainMenu.Items, "Patient Bills");
            }
            if (Authentication.HasFeatureRight(ApplicationAccess.BillingReports, (DataTable)Session["UserRight"]) == false)
            {
                RemoveMenuItemByValue(mainMenu.Items, "Billing Reports");
            }
            if (Authentication.HasFeatureRight(ApplicationAccess.BillingReversal, (DataTable)Session["UserRight"]) == false)
            {
                RemoveMenuItemByValue(mainMenu.Items, "Reverse Billing");
            }

            if (Authentication.HasFeatureRight(ApplicationAccess.Consumables, (DataTable)Session["UserRight"]) == false)
            {
                RemoveMenuItemByValue(mainMenu.Items, "Consumables");
            }
            if (Convert.ToString(Session["Billing"]) != "1")
            {
                RemoveMenuItemByValue(mainMenu.Items, "Billing");
            }

        }
    }
    #endregion

    #region Create / configure Plug-in menu items

    private void CreatePlugInMenu() 
    {
        if (IsPostBack)
            return;

        List<MenuItem> rmvMenuItem = new List<MenuItem>();
        MenuItem mnuItem;

        try
        {
            IFacility MenuManager = (IFacility)ObjectFactory.CreateInstance("BusinessProcess.Security.BFacility, BusinessProcess.Security");
            DataSet dsMenu = MenuManager.GetPluginModuleAndFeaturesForFacility(Convert.ToInt32(Session["FacilityID"]));


            MenuItem pluginMenu, pluginSubMenu;
            
            string modName = String.Empty;
            string fetName = String.Empty;
            
            if (dsMenu != null && dsMenu.Tables.Count > 0)
            {
                //Create Module menu
                for (int i = 0; i < dsMenu.Tables[0].Rows.Count; i++)
                {
                    //Create parent menu first
                    pluginMenu = new MenuItem();
                    modName = Convert.ToString(dsMenu.Tables[0].Rows[i]["ModuleName"].ToString());
                    pluginMenu.Selectable = false;
                    pluginMenu.Text = modName;
                    pluginMenu.Value = modName + "|#" ;
                    pluginMenu.NavigateUrl = "#";
                    
                    //filter rows for parent menu
                    var tblFiltered = dsMenu.Tables[1].AsEnumerable().Where(row => row.Field<String>("ModuleName") == modName).OrderByDescending(row => row.Field<String>("ModuleName")).ToList();
                    var lstDT = tblFiltered.Any() ? tblFiltered.CopyToDataTable() : new DataTable();

                    //Create sub menu for parent menu (module)
                    foreach (DataRow drSMenu in lstDT.Rows)
                    {
                        //Create menu if permitted
                        if (Convert.ToBoolean(Authentication.HasFeatureRight(Convert.ToInt32(drSMenu["FeatureID"].ToString()), (DataTable)Session["UserRight"])))
                        {
                            pluginSubMenu = new MenuItem();
                            fetName = Convert.ToString(drSMenu["FeatureName"].ToString());
                            pluginSubMenu.Value = drSMenu["FeatureID"].ToString() + "|" + drSMenu["FeatureURL"].ToString();
                            pluginSubMenu.Text = fetName;
                            pluginMenu.ChildItems.Add(pluginSubMenu);
                        }
                    }
                    mainMenu.FindItem("plugin").ChildItems.Add(pluginMenu);
                }
                Session["PlugInMenuLoaded"] = true;
            }

            if(!(dsMenu.Tables[0].Rows.Count > 0))
                RemoveMenuItemByValue(mainMenu.Items, "plugin");
        }
        catch (Exception ex)
        {
            Response.Write("<script type='text/javascript'>alert('" + ex.Message + "')</script>");
            Response.Flush();
        }
    }


    #endregion

    protected void mainMenu_MenuItemClick(object sender, MenuEventArgs e)
    {
        string[] menuVal = e.Item.Value.ToString().Split('|');
        Session["SelectedFeatureID"] = menuVal[0];
        
        List<string> lst = menuVal.ToList<string>();
        if (lst.Count > 1)
            Response.Redirect(menuVal[1]);
        else
            return;
        
    }
}