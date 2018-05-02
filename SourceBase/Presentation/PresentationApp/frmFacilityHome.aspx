<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    CodeBehind="frmFacilityHome.aspx.cs" Inherits="IQCare.Web.frmFacilityHome2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <div class="container-fluid">
        <div class="row">
            <span class="text-capitalize pull-left glyphicon-text-size= fa-2x" id="tHeading"
                runat="server"><i class="fa fa-cubes fa-3x" aria-hidden="true"></i><span class="text-info">
                    Select Service</span></span>
        </div>
        <hr />
    </div>
    <div  class="container-fluid" id="maindiv" runat="server">
         <div class="row tilerow" runat="server" id="servicerow">
            <asp:Repeater ID="RepeaterseriveAreas" runat="server" 
                 OnItemCommand="RepeaterseriveAreas_ItemCommand">
                <ItemTemplate>
                <asp:HiddenField ID="hdnModuleId" runat="server" Value='<%# Bind("ModuleId") %>' />
                <asp:HiddenField ID="hdnModuleName" runat="server" Value='<%# Bind("ModuleName") %>' />
                <asp:HiddenField ID="hdnModuleURL" runat="server" Value='<%# Bind("ModuleURL") %>' />
                    <div class="col-md-3 titletext" id="servicebutton" runat="server" style='padding-bottom: 20px;'>
                        <asp:LinkButton ID="ButtonService" runat="server" CssClass="btn btn-primary" Height="100" 
                            Width="100%" CommandName="LoadServiceCommand" CommandArgument='<%# Eval("ModuleId") %>'
                            Text='<%# showIcon(Eval("Icon").ToString()) + "  " + Eval("ModuleName") %>' style='<%# "display:inline-block;height:100px;width:100%;font-size:20px;padding-top:30px;padding-left:5px;background-color:" + DataBinder.Eval(Container.DataItem, "BackColor") + ";"%>'>
                        </asp:LinkButton>
                        <br />
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <%-- <asp:UpdatePanel ID="divDetailsCompnent" UpdateMode="Conditional" runat="server">--%>
        <asp:Button ID="btnShowItems" runat="server" Text="Test popup" Width="120px" Style="display: none;" />
         <asp:Panel ID="divItems" CssClass="container-fluid" runat="server" Style="display: none; width: 185%; border: solid 1px #808080; background-color: #205E8D;">
           <asp:Panel ID="divItemTitle" runat="server" Style="border: solid 0px #808080; margin: 0px 0px 0px 0px;
                cursor: move; height: 30px;margin-bottom:10px;">
                <table class="table-condensed">
                    <tr>
                        <td>
                            <span>
                                <asp:Label ID="labelItemTitle" runat="server"><h5 style="color:#fff;">Select Option</h5></asp:Label></span>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <table class="table-condensed" width="100%" style="background-color: #e1e1e1; margin-bottom: 10px;
                height: 150px;">
                <tr>
                    <td valign="top" colspan="1" style="font-weight: bold; padding: 3px" align="left">
                        <asp:Label ID="labelItemMainType" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btnPatientBill" runat="server" Width="30%" Text="Patient Bill" OnClientClick="window.location = './Billing/frmBillingFindAddBill.aspx'; return false;"
                            Visible="False" Font-Bold="True" CssClass="btn btn-danger" Height="80px"></asp:Button>
                        <asp:Button ID="btnBillReports" runat="server" Width="30%" Text="Billing Reports"
                            OnClientClick="window.location = './Billing/frmBillingReportPage.aspx'; return false;"
                            Visible="False" Font-Bold="True" CssClass="btn btn-warning" Height="80px"></asp:Button>
                        <asp:Button ID="btnBillReversal" runat="server" Width="30%" Text="Bill Reversals"
                            OnClientClick="window.location = './Billing/frmBilling_ReversalApproval.aspx'; return false;"
                            Visible="False" Font-Bold="True" CssClass="btn btn-primary" Height="80px"></asp:Button>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btnConsumables" runat="server" Width="30%" Text="Consumables" OnClientClick="window.location = './Billing/frmFindPatient.aspx?FormName=Consumables&mnuClicked=Consumables'; return false;"
                            Visible="False" Font-Bold="True" CssClass="btn btn-primary" Height="80px"></asp:Button>
                        <asp:Button ID="btnBillSettings" runat="server" Width="30%" Text="Payment Methods"
                            OnClientClick="window.location = './Billing/frmBillingAdmin_PaymentType.aspx'; return false;"
                            Visible="False" Font-Bold="True" CssClass="btn btn-primary" Height="80px"></asp:Button>
                        <asp:Button ID="btnPriceList" runat="server" Width="30%" Text="Price List" OnClientClick="window.location = './Billing/frmBilling_PriceList.aspx'; return false;"
                            Visible="False" Font-Bold="True" CssClass="btn btn-danger" Height="80px"></asp:Button>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btnCancel" runat="server" Text="Close" CssClass="btn btn-primary" Height="30px" Width="11%" Style="text-align:left;"></asp:Button>
                            <label class="glyphicon glyphicon-remove" style="margin-left: -3%; vertical-align: sub; color: #fff;margin-right: 2%;""></label>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <ajaxToolkit:ModalPopupExtender ID="billingOptionsPopup" runat="server" BehaviorID="ptpBehavior"
            PopupControlID="divItems" BackgroundCssClass="modalBackground" CancelControlID="btnCancel"
            X="350" Y="250" DropShadow="true" Enabled="true" PopupDragHandleControlID="divItemTitle"
            TargetControlID="btnShowItems">
        </ajaxToolkit:ModalPopupExtender>
        <%--</asp:UpdatePanel>--%>
    </div>
</asp:Content>
