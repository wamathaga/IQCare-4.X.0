<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmBillingAdmin_PaymentType.aspx.cs"
    Inherits="IQCare.Web.Billing.BillingAdmin_PaymentType" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
<style type="text/css">
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
        <h3 class="box-title"><asp:Label ID="lblHeader" runat="server" Text=""></asp:Label></h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
	
        <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Panel ID="divError" runat="server" Style="padding: 5px" CssClass="background-color: #FFFFC0; border: solid 1px #C00000"
                            HorizontalAlign="Left" Visible="true">
                            <tr>
                                <td>
                                    <asp:Label ID="lblError" runat="server" Style="font-weight: bold; color: #800000"
                                        Text=""></asp:Label>
                                </td>
                            </tr>
                        </asp:Panel>
             </div>
 </div>

                                        <asp:UpdatePanel runat="server" ID="updatePanelMasterItem">
                                            <ContentTemplate>
                                                <div class="mid-outer">
                                                    <div class="mid-inner">
                                                        <div class="mid">
                                                            <div id="grd_payment" class="GridView whitebg" style="cursor: pointer; height: 280px;
                                                                overflow: auto">
                                                                <asp:GridView ID="gridPaymentType" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                                                                    Width="100%" PageIndex="1" BorderWidth="0px" GridLines="None" CssClass="table table-bordered table-hover"
                                                                    CellPadding="0" OnRowDataBound="gridPaymentType_RowDataBound" OnSelectedIndexChanging="gridPaymentType_SelectedIndexChanging"
                                                                    OnSorting="gridPaymentType_Sorting" DataKeyNames="ID,Name">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Payment Type" SortExpression="Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="labelPayMethod" runat="server" Text='<%# Bind("Name") %>' ToolTip='<%# Bind("MethodDescription") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass="textstyle" />
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="MethodDescription" HeaderText="Description" ReadOnly="true"
                                                                            SortExpression="MethodDescription">
                                                                            <ItemStyle CssClass="textstyle" />
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:TemplateField HeaderText="Payment Plugin" SortExpression="Handler">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="labelHandler" runat="server" Text='<%# Bind("Handler") %>' ToolTip='<%# Bind("HandlerDescription") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass="textstyle" />
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Status" SortExpression="Active">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="labelStatus" runat="server" Text='<%# (Boolean.Parse(Eval("Active").ToString())) ? "Active" : "InActive" %>' />
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass="textstyle" />
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>                                                                   
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="buttonSubmit" EventName="Click" />
                                                <asp:AsyncPostBackTrigger ControlID="btnOkAction" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <div class="bottom-outer">
                                            <div class="bottom-inner">
                                                <div class="bottom">
                                                </div>
                                            </div>
                                        </div>
<div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
  <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" CssClass="btn btn-primary"
                                    Height="30px" Width="9%" Style="text-align: left;" />
                                <label class="glyphicon glyphicon-plus" style="margin-left: -3%; margin-right: 1%;
                                    vertical-align: sub; color: #fff;">
                                </label>
                                <asp:Button ID="btnCancel" runat="server" Text="Close" CssClass="btn btn-primary"
                                    Height="30px" Width="9%" Style="text-align: left; margin-left:2%;" />
                                <label class="glyphicon glyphicon-remove" style="margin-left: -3%; margin-right: 2%;
                                    vertical-align: sub; color: #fff;">
                                </label>          
             </div>
            
 </div>

                          
	 <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>
     </div>
        <div class="center" style="padding: 5px;">
           
            <asp:UpdatePanel ID="ItemPanel" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <asp:Button ID="btnShowItems" runat="server" Text="" Width="60px" Style="display: none" />
                    <asp:Panel ID="divItems" runat="server" Style="display: none; width: 580px; border: solid 0px #3c8dbc;
                        background-color: #fff" DefaultButton="buttonSubmit">
                            <div class="">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">
        <asp:Panel ID="divItemTitle" runat="server" Style="cursor: move;">
                            <asp:Label ID="labelItemTitle" runat="server">Payment Type Details</asp:Label>
                        </asp:Panel>
        </h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
	<div class="row" align="center">
         <br />           
        </div>
        <div class="row">
 <div class="col-md-11 col-sm-12 col-xs-12 form-group">
  <i style="color:Red;margin-left:10px;">All of the fields in this section are required.</i>             
             </div>             
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group"> 
  <asp:Panel ID="panelError" runat="server" Style="padding: 5px" CssClass="background-color: #FFFFC0; border: solid 1px #C00000"
                                HorizontalAlign="Left" Visible="true">
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="errorLabel" runat="server" Style="font-weight: bold; color: #800000"
                                            Text=""></asp:Label>
                                    </td>
                                </tr>
                            </asp:Panel>            
             </div>             
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">   
 <asp:Label ID="labelItemMainType" runat="server" Text=""></asp:Label>          
             </div>             
 </div>
 <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">Name :</label>
             </div>
             <div class="col-md-9 col-sm-12 col-xs-12 form-group">
  <asp:TextBox ID="textPaymentTypeName" runat="server" Width="99%" CssClass="form-control" AutoComplete="false"
                                        MaxLength="100"></asp:TextBox>
                                    <asp:HiddenField ID="prevPaymentName" runat="server" />
                                    <asp:HiddenField ID="currentID" runat="server" Value="-1" />
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                        FilterType="Numbers, UppercaseLetters, LowercaseLetters,Custom" TargetControlID="textPaymentTypeName"
                                        ValidChars="&-, " />
             </div>
            
 </div>
 <div class="row" style="display: none">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">Short Code :</label>
             </div>
             <div class="col-md-9 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="textPaymentTypeCode" runat="server" Width="99%" AutoComplete="false" CssClass="form-control"></asp:TextBox>
                                    <asp:HiddenField ID="prevTypeCode" runat="server" />
             </div>
            
 </div>
 <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">Description :</label>
             </div>
             <div class="col-md-9 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="textDescription" runat="server" Width="99%" AutoComplete="false"
                                        TextMode="MultiLine" Rows="2" MaxLength="255" CssClass="form-control" style="resize:none;"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                        FilterType="Numbers, UppercaseLetters, LowercaseLetters,Custom" TargetControlID="textDescription"
                                        ValidChars="&-, " />
             </div>
            
 </div>
 <div class="row" style="display: none">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">Priority :</label>
             </div>
             <div class="col-md-9 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtSeqNo" Width="50%" runat="server" CssClass="form-control"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                        TargetControlID="txtSeqNo" FilterType="Numbers" FilterMode="ValidChars" />
             </div>
            
 </div>
 <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">Handler Name :</label>
             </div>
             <div class="col-md-9 col-sm-12 col-xs-12 form-group">
<asp:DropDownList ID="ddlHandler" runat="server" Width="99%" CssClass="form-control">
                                    </asp:DropDownList>
                                    <asp:HiddenField ID="prevhandlerName" runat="server" />
             </div>
            
 </div>
 <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">Status :</label>
             </div>
             <div class="col-md-9 col-sm-12 col-xs-12 form-group">
             <asp:RadioButtonList ID="rblStatus" runat="server" RepeatDirection="Horizontal" CssClass="radioButtonList">
                                        <asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
                                        <asp:ListItem Value="0">InActive</asp:ListItem>
                                    </asp:RadioButtonList>
             </div>
            
 </div>
 <div class="row">
 <hr style="height: 2px; color: #C0C0C0; margin: 1px; padding: 0px" />
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Button ID="buttonSubmit" runat="server" Text="Save" OnClick="buttonSubmit_Click"
                                        CssClass="btn btn-primary" Height="30px" Width="15%" Style="text-align: left;" />
                                    <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; margin-right: 1%;
                                        vertical-align: sub; color: #fff;">
                                    </label>
                                    <asp:Button ID="buttonClose" runat="server" Text="Close" CssClass="btn btn-primary"
                                        Height="30px" Width="15%" Style="text-align: left;" />
                                    <label class="glyphicon glyphicon-remove-circle" style="margin-left: -3%; margin-right: 2%;
                                        vertical-align: sub; color: #fff;">
                                    </label>
             </div>
 </div>
	 <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>
     </div>
</asp:Panel>
                    <ajaxToolkit:ModalPopupExtender ID="paymentTypePopup" runat="server" BehaviorID="ptpBehavior"
                        TargetControlID="btnShowItems" PopupControlID="divItems" BackgroundCssClass="modalBackground"
                        CancelControlID="buttonClose" DropShadow="false" PopupDragHandleControlID="divItemTitle">
                    </ajaxToolkit:ModalPopupExtender>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="gridPaymentType" EventName="RowCommand" />
                    <asp:AsyncPostBackTrigger ControlID="gridPaymentType" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnOkAction" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="notificationPanel" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pnNotify" runat="server" Style="display: none; width: 460px; border: solid 1px #808080;
                        background-color: #E0E0E0; z-index: 15000">
                        <asp:Panel ID="pnPopup_Title" runat="server" Style="border: solid 1px #808080; margin: 0px 0px 0px 0px;
                            cursor: move; height: 18px">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 18px">
                                <tr>
                                    <td style="width: 5px; height: 19px;">
                                    </td>
                                    <td style="width: 100%; height: 19px; background-color: #479ADA">
                                        <span style="font-weight: bold; color: Black">
                                            <asp:Label ID="lblNotice" runat="server">Add Editing Item</asp:Label></span>
                                    </td>
                                    <td style="width: 5px; height: 19px;">
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <table border="0" cellpadding="15" cellspacing="0" style="width: 100%;">
                            <tr>
                                <td style="width: 48px" valign="middle" align="center">
                                    <asp:Image ID="imgNotice" runat="server" ImageUrl="~/Images/mb_information.gif" Height="32px"
                                        Width="32px" />
                                </td>
                                <td style="width: 100%;" valign="middle" align="center">
                                    <asp:Label ID="lblNoticeInfo" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <div style="background-color: #FFFFFF; border-top: solid 1px #808080; width: 100%;
                            text-align: center; padding-top: 5px; padding-bottom: 5px">
                            <asp:Button ID="btnOkAction" runat="server" Text="OK" Width="80px" Style="border: solid 1px #808080;"
                                OnClick="btnOkAction_Click" />
                        </div>
                    </asp:Panel>
                    <asp:Button ID="btn" runat="server" Style="display: none" />
                    <ajaxToolkit:ModalPopupExtender ID="notifyPopupExtender" runat="server" TargetControlID="btn"
                        PopupControlID="pnNotify" BackgroundCssClass="modalBackground" DropShadow="True"
                        PopupDragHandleControlID="pnPopup_Title" Enabled="True" DynamicServicePath="">
                    </ajaxToolkit:ModalPopupExtender>
                </ContentTemplate>
                <Triggers>
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <asp:UpdateProgress ID="sProgress" runat="server" DisplayAfter="5">
            <ProgressTemplate>
                <div style="width: 100%; height: 100%; position: fixed; top: 0px; left: 0px; vertical-align: middle;">
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
