<%@ Control Language="C#" AutoEventWireup="true" Inherits="MasterPage_levelOneNavigationUserControl"
    CodeBehind="levelOneNavigationUserControl.ascx.cs" %>
<div class="navbar navbar-default">
    <%--<div class="navbar-inner">--%>
    <div class="container-fluid" style="border-bottom: 1px solid #6CF">
        <div class="navbar-header">
            <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1"
                aria-expanded="False">
                <span class="sr-only">Toggle navigation</span> <span class="icon-bar"></span><span
                    class="icon-bar"></span><span class="icon-bar"></span>
            </button>
            <div class="navbar-brand" href="#" style="text-decoration: none">
                <span class="fa-stack fa-lg"><i class="fa fa-circle fa-stack-1x"></i><i class="fa fa-code-fork fa-stack-1x fa-inverse">
                </i></span><span class="text-muted small">IQCARE</span>
            </div>
        </div>
        <!-- .navbar-header -->
        <!-- Everything you want hidden at 940px or less, place within here -->
        <div class="collapse navbar-collapse">
            <asp:Menu ID="mainMenu" runat="server" EnableViewState="false" IncludeStyleBlock="false"
                Orientation="Horizontal" RenderingMode="List" CssClass="text-muted" StaticMenuStyle-CssClass="nav"
                StaticSelectedStyle-CssClass="active" DynamicMenuStyle-CssClass="dropdown-menu">
                <Items>
                    <asp:MenuItem Text="<i class='fa fa-cubes fa-1x text-muted' aria-hidden='true'></i> <span class='fa-1x text-muted'><strong>  Select Service</strong></span>"
                        NavigateUrl="~/frmFacilityHome.aspx"></asp:MenuItem>
                    <asp:MenuItem Text="<i class='fa fa-search fa-1x text-muted' aria-hidden='true'></i> <span class='fa-1x text-muted'><strong>  Find/Add Patient</strong></span>"
                        NavigateUrl="~/frmFindAddPatient.aspx"></asp:MenuItem>
                    <asp:MenuItem Text="<i class='fa fa-bar-chart fa-1x text-muted' aria-hidden='true'></i> <span class='fa-1x text-muted'> <strong>Reports</strong></span>"
                        Selectable="False">
                        <asp:MenuItem Text="Custom Reports" Value="Custom Reports" NavigateUrl="~/Reports/frmReportCustom.aspx">
                        </asp:MenuItem>
                        <asp:MenuItem Text="Query Builder Reports" Value="Query Builder Reports" NavigateUrl="~/Reports/frmQueryBuilderReports.aspx">
                        </asp:MenuItem>
                        <asp:MenuItem Text="IQTools Reports" Value="IQToolsReportsmain" Selectable="False">
                            <asp:MenuItem Text="IQTools Query" Value="IQToolsQuery" NavigateUrl="~/IQTools/frmIQToolsQuery.aspx">
                            </asp:MenuItem>
                            <asp:MenuItem Text="IQTools Reports" Value="IQToolsReports" NavigateUrl="~/IQTools/frmIQToolsReports.aspx">
                            </asp:MenuItem>
                            <asp:MenuItem Text="Refresh IQTools Cache" Value="Refresh IQTools Cache" NavigateUrl="~/frmSystemCache.aspx?Code=2">
                            </asp:MenuItem>
                        </asp:MenuItem>
                    </asp:MenuItem>
                    <asp:MenuItem Text="<i class='fa fa-calendar fa-1x text-muted' aria-hidden='true'></i> <span class='fa-1x text-muted'> <strong>Scheduler</strong></span>"
                        Value="Scheduler" NavigateUrl="~/Scheduler/frmScheduler_AppointmentMain.aspx" />
                    <asp:MenuItem Text="<i class='fa fa-cogs fa-1x text-muted'></i> <span class='fa-1x text-muted'><strong>Administration</strong></span>"
                        Value="Administration" Selectable="False" NavigateUrl="~/frmFacilityHome.aspx">
                        <asp:MenuItem Text="Facility Setup" Value="Facility Setup" NavigateUrl="~/AdminForms/frmAdmin_FacilityList.aspx">
                        </asp:MenuItem>
                        <asp:MenuItem Text="Customize Lists" Value="Customize Lists" NavigateUrl="~/AdminForms/frmAdmin_PMTCT_CustomItems.aspx">
                        </asp:MenuItem>
                        <asp:MenuItem Text="Item Management" Value="ItemMgt" NavigateUrl="~/AdminForms/frmAdmin_ItemManagement.aspx">
                        </asp:MenuItem>
                        <asp:MenuItem Text="User Administration" Value="User Administration" NavigateUrl="~/AdminForms/frmAdmin_UserList.aspx">
                        </asp:MenuItem>
                        <asp:MenuItem Text="User Group Administration" Value="User Group Administration"
                            NavigateUrl="~/AdminForms/frmAdmin_UserGroupList.aspx"></asp:MenuItem>
                        <asp:MenuItem Text="Delete Patient" Value="Delete Patient" NavigateUrl="~/frmFindAddPatient.aspx?mnuClicked=DeletePatient">
                        </asp:MenuItem>
                        <asp:MenuItem Text="Configure Custom Fields" Value="Configure Custom Fields" NavigateUrl="~/AdminForms/frmConfig_Customfields.aspx">
                        </asp:MenuItem>
                        <asp:MenuItem Text="Audit Trail" Value="AuditTrail" NavigateUrl="~/AdminForms/frmAdmin_AuditTrail.aspx">
                        </asp:MenuItem>
                        <asp:MenuItem Text="Refresh System Cache" Value="Refresh System Cache" NavigateUrl="~/frmSystemCache.aspx?Code=1">
                        </asp:MenuItem>
                    </asp:MenuItem>
                    <asp:MenuItem Text="<i class='fa fa-bar-chart fa-1x text-muted' aria-hidden='true'></i> <span class='fa-1x text-muted'> <strong>Plug-ins</strong></span>"
                        Value="plugin" Selectable="False"></asp:MenuItem>
                    <asp:MenuItem Text="<i class='fa fa-database fa-1x text-muted' aria-hidden='true'></i><span class='fa-1x text-muted'> <strong>Database Back Up</strong></span>"
                        Value="Back Up" Selectable="false" NavigateUrl="#">
                        <asp:MenuItem Text="System Back-Up/Restore" Value="System Back-Up/Restore" NavigateUrl="~/frmDBBackup.aspx">
                        </asp:MenuItem>
                        <asp:MenuItem Text="Backup/Restore Setup" Value="Backup/Restore Setup" NavigateUrl="~/frmDBBackupSetting.aspx">
                        </asp:MenuItem>
                    </asp:MenuItem>
                </Items>
            </asp:Menu>
        </div>
    </div>
    <!-- .nav-collapse collapse -->
</div>
