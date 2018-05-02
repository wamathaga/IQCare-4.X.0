<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmBilling_PriceList.aspx.cs" Inherits="IQCare.Web.Billing.frmBilling_PriceList" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="ctMain" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <script language="javascript" type="text/javascript">
        function PrintReport() {

            var _priceDate = $('#<%=textPriceListDate.ClientID%>').val();

            var url = "PrintPriceList.aspx?print=true";
            OpenNewWindow(url, "PrintPriceList");
        }
        function OpenNewWindow(pageurl, pgname) {
            var w = screen.width - 60;
            var h = screen.height - 60;
            var winprops = "location=no,scrollbars=yes,resizable=yes,status=no";
            var frmwin = window.open(pageurl, pgname, winprops);
            if (parseInt(navigator.appVersion) >= 4) {
                frmwin.window.focus();
            }
        }
             
    </script>
    <style>
        div.ajax__calendar_container, div.ajax__calendar_body
        {
            width: 225px;
        }
        
        .ajax__calendar_days td
        {
            padding: 2px 4px;
        }
        .modalBackground 
{
  background-color:#F5F5F5;
  filter: alpha(opacity=80);
  opacity: 0.80; 
}
    </style>
    
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">Price List</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
	<div class="row" align="center">
         <br />           
        </div>
        <asp:UpdatePanel runat="server" ID="divFilterComponent" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="panelFilter" runat="server" DefaultButton="btnFilter">
                <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
  <asp:Button ID="btnPrint" runat="server" Text="Print Price List" 
                                       OnClick="btnPrint_Click" CssClass="btn btn-primary" Height="30px"
                                        Width="70%" Style="display: block; text-align: left;" />
                                    <label class="glyphicon glyphicon-print" style="margin-left: 61%; margin-right: 2%;
                                        vertical-align: sub; color: #fff;  margin-top: -20px">
                                    </label>
                                    <ajaxToolkit:ConfirmButtonExtender ID="cbePrint" runat="server" DisplayModalPopupID="mpePrintPriceList"
                                        TargetControlID="btnPrint"></ajaxToolkit:ConfirmButtonExtender>
                                    <ajaxToolkit:ModalPopupExtender ID="mpePrintPriceList" runat="server" PopupControlID="pnlPopup"
                                        TargetControlID="btnPrint" OkControlID="btnYes" CancelControlID="btnNo" BackgroundCssClass="modalBackground">
                                    </ajaxToolkit:ModalPopupExtender>
                                    <asp:Panel ID="pnlPopup" runat="server" Style="display: none; background-color: #fff;
                                        width: 350px; border: 1px solid #3c8dbc;">
                                        <div style="background-color: #3c8dbc; height: 30px; color: White; line-height: 30px;
                                            text-align: center; font-weight: bold;">
                                            Confirmation
                                        </div>
                                        <div style="min-height: 30px; line-height: 20px; text-align: center; font-weight: bold;">
                                            The full price list will be printed.<br />
                                            Only items whose price has been set will be printed.
                                        </div>
                                        <div style="min-height: 30px; line-height: 20px; text-align: center; font-weight: bold;">
                                            Effective Date:&nbsp;&nbsp;
                                            <asp:TextBox ID="textPriceListDate" runat="server" Text="" MaxLength="11" Width="80px"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="textPriceListDate"
                                                ErrorMessage="*" Display="None" ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$"></asp:RegularExpressionValidator><br />
                                            <ajaxToolkit:CalendarExtender ID="calendarButtonExtender" runat="server" TargetControlID="textPriceListDate"
                                                Format="dd-MMM-yyyy" />
                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="textPriceListDate"
                                                Enabled="True" UserDateFormat="DayMonthYear" CultureDateFormat="dd-MMM-yyyy"
                                                ClearMaskOnLostFocus="False" CultureName="en-GB" Mask="99-LLL-9999"></ajaxToolkit:MaskedEditExtender>
                                        </div>
                                        <div style="min-height: 30px; line-height: 20px; text-align: center; font-weight: bold;">
                                            Do you want to proceed?
                                        </div>
                                        <div style="padding: 5px;" align="right">
                                            <asp:Button ID="btnYes" runat="server" Text="OK" CssClass="btn btn-primary" Height="30px" Width="22%" Style="text-align:left;"/>
                                            <label class="glyphicon glyphicon-ok" style="margin-left: -5%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
                                            <asp:Button ID="btnNo" runat="server" Text="Cancel" CssClass="btn btn-primary" Height="30px" Width="22%" Style="text-align:left;" />
                                            <label class="glyphicon glyphicon-remove" style="margin-left: -5%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
                                            </div>
                                    </asp:Panel>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">Item Type:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlItemType" runat="server" AutoPostBack="false" Width="99%" CssClass="form-control"></asp:DropDownList>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-right">
             <label  class="control-label">Item Name:</label>
             </div> 
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="textSearchText" runat="server" MaxLength="25" Width="99%" CssClass="form-control"></asp:TextBox>
              <ajaxToolkit:FilteredTextBoxExtender ID="fteSearchText" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,Custom"
                                        TargetControlID="textSearchText" ValidChars="-/\* "></ajaxToolkit:FilteredTextBoxExtender>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group text-right">
             <label  class="control-label">Show priced only:</label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
             <asp:RadioButtonList ID="rblOption" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="radioButtonList">
                                        <asp:ListItem Selected="True" Value="No">No </asp:ListItem>
                                        <asp:ListItem Value="Yes">Yes </asp:ListItem>
                                    </asp:RadioButtonList>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
            <asp:Button ID="btnFilter" runat="server" Text="View" OnClick="btnFilter_Click"
                                        CausesValidation="false"  CssClass="btn btn-primary" Height="30px" Width="90%" Style="text-align: left;" />
                                    <label class="glyphicon glyphicon-eye-open" style="margin-left: -28%; margin-right: 2%;
                                        vertical-align: sub; color: #fff;">
                                    </label>
             </div>
 </div>

                </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnFilter" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnPrint" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdatePanel runat="server" ID="divErrorCompenent" UpdateMode="Always">
            <ContentTemplate>
            <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
<asp:Panel ID="divError" runat="server" Style="padding: 5px" CssClass="background-color: #FFFFC0; border: solid 1px #C00000"
                                    HorizontalAlign="Left" Visible="true">
                                    <asp:Label ID="lblError" runat="server" Style="font-weight: bold; color: #800000"
                                        Text=""></asp:Label>
                                </asp:Panel>
                                <asp:HiddenField ID="HPageIndex" runat="server" />
                                <asp:HiddenField ID="HSearchText" runat="server" />
                                <asp:HiddenField ID="HItemType" runat="server" />
                                <asp:HiddenField ID="HPerm" runat="server" />
                                <asp:HiddenField ID="HPages" runat="server" />
                                <asp:HiddenField ID="HPriced" runat="server" />
             </div>
 </div>
              
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="divGridComponent" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table cellspacing="6" cellpadding="0" width="100%" border="0">
                    <tbody>
                        <tr>
                            <td class="pad5 formbg border">
                                <div id="divPriceList" class="grid" style="width: 100%;">
                                    <div class="rounded">
                                        <div class="top-outer">
                                            <div class="top-inner">
                                                <div class="top">
                                                    <h2>
                                                        <asp:Label runat="server" ID="labelNote"></asp:Label></h2>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="mid-outer">
                                            <div class="mid-inner">
                                                <div class="mid" style="height: 335px; overflow: auto">
                                                    <div id="div-PricelistGridview" class="GridView whitebg">
                                                        <asp:GridView ID="gridPriceList" runat="server" AllowSorting="True" AutoGenerateColumns="False" 
                                                            BorderColor="White" BorderWidth="1px" CellPadding="0" GridLines="None" CssClass="table table-bordered table-hover"
                                                            EmptyDataText="No Data to display" ShowHeaderWhenEmpty="True" Width="100%" BorderStyle="Solid"
                                                            DataKeyNames="ItemID,ItemTypeID,VersionStamp" OnRowCommand="gridPriceList_RowCommand"
                                                            OnRowDataBound="gridPriceList_RowDataBound" PageSize="50" OnRowCreated="gridPriceList_RowCreated"
                                                            AllowPaging="false" Visible="true">
                                                            <Columns>
                                                                <asp:BoundField DataField="ItemName" HeaderText="Item Name" ItemStyle-Width="40%">
                                                                    <ItemStyle Width="40%" />
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Cash Price">
                                                                    <ItemTemplate>
                                                                        <span style='display: <%# HideEdit() %>; white-space: nowrap'>
                                                                            <asp:Label ID="labelPrice" runat="server" Text='<%# Bind("SellingPrice", "{0:N}") %>'></asp:Label>
                                                                        </span><span style='display: <%# ShowInEdit() %>; white-space: nowrap'>
                                                                            <asp:TextBox ID="textPrice" runat="server" Text='<%# Bind("SellingPrice", "{0:N}") %>'
                                                                                MaxLength="11" Width="100px"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="ftePrice" runat="server" TargetControlID="textPrice"
                                                                                FilterType="Numbers, Custom" ValidChars="." />
                                                                        </span>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="10%" />
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Insurance Price">
                                                                    <ItemTemplate>
                                                                        <span style='display: <%# HideEdit() %>; white-space: nowrap'>
                                                                            <asp:Label ID="labelInsPrice" runat="server" Text='<%# Bind("SellingPriceIns", "{0:N}") %>'></asp:Label>
                                                                        </span><span style='display: <%# ShowInEdit() %>; white-space: nowrap'>
                                                                            <asp:TextBox ID="textInsPrice" runat="server" Text='<%# Bind("SellingPriceIns", "{0:N}") %>'
                                                                                MaxLength="11" Width="100px"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="fteInsPrice" runat="server" TargetControlID="textInsPrice"
                                                                                FilterType="Numbers, Custom" ValidChars="." />
                                                                        </span>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="10%" />
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Pricing Type">
                                                                    <ItemTemplate>
                                                                        <span style='display: <%# HideEdit() %>; white-space: nowrap'>
                                                                            <asp:Label ID="labelPriceType" runat="server"></asp:Label>
                                                                        </span><span style='display: <%# ShowInEdit() %>; white-space: nowrap'>
                                                                            <asp:DropDownList runat="server" ID="ddlPriceType" AutoPostBack="false">
                                                                                <asp:ListItem Value="Item">Item</asp:ListItem>
                                                                                <asp:ListItem Value="Dose">Dose</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </span>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="10%" />
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Effective Date">
                                                                    <ItemTemplate>
                                                                        <span style='display: <%# HideEdit() %>; white-space: nowrap'>
                                                                            <asp:Label ID="labelPriceDate" runat="server" Text='<%# Bind("PriceDate", "{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                                        </span>
                                                                        <div style='display: <%# ShowInEdit() %>; white-space: nowrap'>
                                                                            <asp:TextBox ID="textPriceDate" runat="server" Text='<%# Bind("PriceDate", "{0:dd-MMM-yyyy}") %>'
                                                                                MaxLength="11" Width="90px"></asp:TextBox>
                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="textPriceDate"
                                                                                ErrorMessage="*" Display="None" ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$"></asp:RegularExpressionValidator><br />
                                                                            <ajaxToolkit:CalendarExtender ID="calendarButtonExtender" runat="server" TargetControlID="textPriceDate"
                                                                                Format="dd-MMM-yyyy" OnClientDateSelectionChanged="disable_past_dates" />
                                                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="textPriceDate"
                                                                                Enabled="True" UserDateFormat="DayMonthYear" CultureDateFormat="dd-MMM-yyyy"
                                                                                ClearMaskOnLostFocus="False" CultureName="en-GB" Mask="99-LLL-9999"></ajaxToolkit:MaskedEditExtender>
                                                                        </div>
                                                                        <asp:HiddenField ID="hdVersionStamp" runat="server" Value="" />
                                                                        <asp:HiddenField ID="HFlag" runat="server" Value="" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="15%" />
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Delete">
                                                                    <ItemTemplate>
                                                                        <span style='display: <%# ShowInEdit() %>; white-space: nowrap'>
                                                                            <asp:CheckBox runat="server" ID="chkDelete" AutoPostBack="false" TextAlign="Left" />
                                                                        </span>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="10%" />
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="bottom-outer">
                                    <div class="bottom-inner">
                                        <div class="bottom" style="text-align: center">
                                            <br />
                                            <div id="divAction" style="white-space: nowrap; text-align: center;">
                                                <asp:Button ID="buttonSave" runat="server" Text="Save" OnClick="buttonSave_Click"
                                                    CausesValidation="false" CssClass="btn btn-primary" Height="30px" Width="8%"
                                                    Style="text-align: left;" />
                                                <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; margin-right: 2%;
                                                    vertical-align: sub; color: #fff;">
                                                </label>
                                                <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-primary"
                                                    Height="30px" Width="8%" Style="text-align: left;" />
                                                <label class="glyphicon glyphicon-remove-circle" style="margin-left: -3%; margin-right: 2%;
                                                    vertical-align: sub; color: #fff;">
                                                </label>
                                            </div>
                                            <br />
                                            <asp:Repeater ID="rptPager" runat="server" Visible="true">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                        Enabled='<%# Eval("Enabled") %>' OnClick="Page_Changed"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="form pad5 center" style="white-space: nowrap; text-align: center">
                            </td>
                        </tr>
                    </tbody>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnOkAction" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnFilter" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="notificationPanel" runat="server">
            <ContentTemplate>
                <asp:Button ID="btn" runat="server" Style="display: none" />
                <asp:Panel ID="pnNotify" runat="server" Style="display: none; width: 460px; border: solid 0px #3c8dbc;
                    background-color: #fff; z-index: 15000; height:130px;">
                       
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">
            &nbsp;<asp:Panel ID="pnPopup_Title" runat="server" Style="cursor: move;">
                        <asp:Label ID="lblNotice" runat="server">Price</asp:Label>                        
                    </asp:Panel>
            <h3>
            </h3>
            <h3>
            </h3>
            <h3>
            </h3>
            <h3>
            </h3>
            <h3>
            </h3>
        </h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
	<div class="row" align="center">
         <br />           
        </div>
        <div class="row">
 <div class="col-md-1 col-sm-12 col-xs-12 form-group">
<asp:Image ID="imgNotice" runat="server" ImageUrl="~/images/mb_information.gif" Height="32px" Width="32px" />
             </div>
             <div class="col-md-11 col-sm-12 col-xs-12 form-group">
             <label class="control-label"><asp:Label ID="lblNoticeInfo" runat="server"></asp:Label></label>
             </div>
            
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group"> 
 <asp:Button ID="btnOkAction" runat="server" Text="Close" OnClick="btnOkAction_Click"
                            CssClass="btn btn-primary" Height="30px" Width="20%" Style="text-align: left;" />
                        <label class="glyphicon glyphicon-remove-circle" style="margin-left: -5%; margin-right: 2%;
                            vertical-align: sub; color: #fff;">
                        </label>
                        <asp:Button ID="btnExit" runat="server" Text="Close" CssClass="btn btn-primary" Height="30px"
                            Width="20%" Style="text-align: left; display: none" />
                        <label class="glyphicon glyphicon-remove-circle" style="margin-left: -5%; margin-right: 2%;
                            vertical-align: sub; color: #fff;">
                        </label>            
             </div>             
 </div>
	 <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>
 </asp:Panel>
                <ajaxToolkit:ModalPopupExtender ID="notifyPopupExtender" runat="server" TargetControlID="btn"
                    PopupControlID="pnNotify" BackgroundCssClass="modalBackground" DropShadow="false"
                    PopupDragHandleControlID="pnPopup_Title" Enabled="True" DynamicServicePath=""
                    OkControlID="btnOkAction" Y="50">
                </ajaxToolkit:ModalPopupExtender>
            </ContentTemplate>
        </asp:UpdatePanel>
	 <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>
     </div>
     

        <asp:UpdateProgress ID="sProgress" runat="server" DisplayAfter="5">
            <ProgressTemplate>
                <div style="width: 100%; height: 100%; position: fixed; top: 0px; left: 0px; vertical-align: middle;
                    background-color: Gray; filter: alpha(opacity=50); opacity: 0.7; z-index: 120;
                    moz-opacity: 0.5; khtml-opacity: .5">
                    <table style="position: relative; top: 45%; left: 45%; border: solid 1px #808080;
                        background-color: #FFFFC0; width: 120px; height: 24px;" cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="right" valign="middle" style="width: 30px; height: 22px;">
                                <img src="../Images/loading.gif" height="16px" width="16px" alt="" />
                            </td>
                            <td align="left" valign="middle" style="font-weight: bold; color: #808080; width: 90px;
                                height: 22px; padding-left: 5px">
                                Processing....
                            </td>
                        </tr>
                    </table>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    
</asp:Content>
