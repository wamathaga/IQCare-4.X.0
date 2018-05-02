<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmAdmin_AdmissionWards.aspx.cs" Inherits="IQCare.Web.AdminForms.frmAdmin_AdmissionWards" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
<style type="text/css">
.modalBackground 
{
  background-color:#F5F5F5;
  filter: alpha(opacity=80);
  opacity: 0.80; 
  z-index: 2000;
}
</style>
<div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">Wards</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
         <div class="row" align="center">
<br />             
 </div>
  <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group text-nowrap">
             <asp:UpdatePanel runat="server" ID="divErrorUp" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:Panel ID="divError" runat="server" Style="padding: 5px" CssClass="background-color: #FFFFC0; border: solid 1px #C00000"
                                        HorizontalAlign="Left" Visible="true">
                                        <asp:Label ID="lblError" runat="server" Style="font-weight: bold; color: #800000"
                                            Text=""></asp:Label>
                                        <asp:HiddenField ID="HSelectedID" runat="server" />
                                        <asp:HiddenField ID="HActionType" runat="server" Value="VIEW" />
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
             </div>            
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group text-nowrap">
           <asp:UpdatePanel runat="server" ID="divComponent" UpdateMode="Conditional">
                <ContentTemplate>
                <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
            <div id="div1" class="grid" style="padding-right:5px;">
                                        <div class="mid-outer">
                                            <div class="mid-inner">
                                                <div class="mid" style="cursor: pointer; height: 280px; max-height: 480px; overflow: auto;
                                                    border: 1px solid #666699;">
                                                    <div id="div2" class="whitebg">
                                                        <asp:GridView ID="gridWardList" CssClass="table table-bordered table-hover" CellPadding="0" runat="server"
                                                            AutoGenerateColumns="False" PageSize="1" BorderWidth="0px" GridLines="None" DataKeyNames="WardID"
                                                            EmptyDataText="No Data to display" Width="100%" ShowHeaderWhenEmpty="True" OnRowCommand="gridWardList_RowCommand"
                                                            OnRowDataBound="gridWardList_RowDataBound">
                                                            <Columns>
                                                                <asp:BoundField DataField="WardName" HeaderText="Ward Name" SortExpression="WardName">
                                                                    <ItemStyle CssClass="textstyle" />
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="PatientCategory" HeaderText="Patient Category" SortExpression="PatientCategory">
                                                                    <ItemStyle CssClass="textstyle" />
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Capacity" HeaderText="Capacity" SortExpression="Capacity">
                                                                    <ItemStyle CssClass="textstyle" />
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Active" HeaderText="Status" SortExpression="Status">
                                                                    <ItemStyle CssClass="textstyle" />
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="searchresultfixedheader" Height="20px" HorizontalAlign="Left">
                                                            </HeaderStyle>
                                                           
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
             </div>
             
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <div id="divAction" style="white-space: nowrap; text-align: center">
                                        <asp:Button ID="buttonAddWard" runat="server" Text="Add Ward" OnClick="AddNewWard"
                                            CssClass="btn btn-primary" Height="30px" Width="11%" Style="text-align: left;" />
                                        <label class="glyphicon glyphicon-plus" style="margin-left: -3%; margin-right: 2%;
                                            vertical-align: sub; color: #fff;">
                                        </label>
                                        <asp:Button ID="buttonCancelAddWard" runat="server" Text="Cancel" Style="display: none;
                                            text-align: left;" CssClass="btn btn-primary" Height="30px" Width="8%" />
                                        <label class="glyphicon glyphicon-remove-circle" style="display: none; margin-left: -3%;
                                            margin-right: 2%; vertical-align: sub; color: #fff;">
                                        </label>
                                        <asp:Button ID="btnCloseMain" runat="server" OnClick="btnCloseMain_Click" Text="Close"
                                            CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                                        <label class="glyphicon glyphicon-remove-circle" style="margin-left: -3%; margin-right: 2%;
                                            vertical-align: sub; color: #fff;">
                                        </label>
                                    </div>
             </div>
             
 </div>
                    
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="buttonSubmitWard" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnOkAction" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="divDetailsCompnent" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:Button ID="buttonRaiseItemPopup" runat="server" Style="display: none" />
                    <asp:Panel ID="divData" runat="server" class="pad5 formbg border" Style="z-index: 9999!important;
                        overflow: auto; width:100%">
                        <div id="divWardList" class="grid">
                            <div class="mid-outer">
                                <div class="mid-inner">
                                    <div class="mid" style="cursor: pointer;  overflow: auto;
                                        border: 1px solid #666699;">
                                        <div id="div-gridview">
                                            <asp:Panel ID="divTitle" runat="server" Style="cursor: move;overflow:hidden;">
                                            
                                              <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header text-left">
        <h3 class="box-title"> Add, Modify Wards</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
         <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
           <asp:Panel ID="panelError" runat="server" Style="padding: 5px" CssClass="background-color: #FFFFC0; border: solid 1px #C00000"
                                                    HorizontalAlign="Left" Visible="true">
                                                     <asp:Label ID="errorLabel" runat="server" Style="font-weight: bold; color: #800000"
                                                                Text=""></asp:Label>
                                                </asp:Panel>
             </div>
             
 </div>
 <div class="row">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label class="control-label required">Ward Name:</label>
             </div>
             <div class="col-md-8 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="textWardName" runat="server" Width="99%" CssClass="form-control" MaxLength="50" AutoComplete="false"></asp:TextBox>
             </div>
             </div>
             <div class="row">
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label class="control-label required">Patient Category:</label>
             </div>
             <div class="col-md-8 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlPatientCategory" runat="server" Width="99%" CssClass="form-control" AutoPostBack="false">
                                                            <asp:ListItem Text="Select" Value="" />
                                                            <asp:ListItem Text="Female" Value="Female" />
                                                            <asp:ListItem Text="Male" Value="Male" />
                                                        </asp:DropDownList>
             </div> 
 </div>
 <div class="row">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label class="control-label required">Bed Capacity:</label>
             </div>
             <div class="col-md-8 col-sm-12 col-xs-12 form-group">
            <asp:TextBox ID="textCapacity" runat="server" Width="99%" MaxLength="5" AutoComplete="false" CssClass="form-control"></asp:TextBox>
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="fteCapacity" FilterType="Numbers" TargetControlID="textCapacity"
                                                            runat="server" />
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="filTxt" runat="server" TargetControlID="textCapacity"
                                                            FilterMode="ValidChars" FilterType="Numbers"></ajaxToolkit:FilteredTextBoxExtender>
             </div>
             </div>
             <div class="row">
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label class="control-label"> Status:</label>
             </div>
             <div class="col-md-8 col-sm-12 col-xs-12 form-group">
             <asp:RadioButtonList ID="rblStatus" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
                                                            <asp:ListItem Value="0">InActive</asp:ListItem>
                                                        </asp:RadioButtonList>
             </div> 
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <asp:Button ID="buttonSubmitWard" runat="server" Text="Save" OnClick="SaveWard" CssClass="btn btn-primary"
                                                            Height="30px" Width="30%" Style="text-align: left;" />
                                                        <label class="glyphicon glyphicon-plus" style="margin-left: -7%; margin-right: 2%;
                                                            vertical-align: sub; color: #fff;">
                                                        </label>
                                                        <asp:Button ID="buttonClose" runat="server" Text="Close" 
                                                            CssClass="btn btn-primary" Height="30px" Width="30%" Style="text-align: left;" />
                                                        <label class="glyphicon glyphicon-remove-circle" style="margin-left: -7%; margin-right: 2%;
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
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:ModalPopupExtender ID="mpeWardPopup" runat="server" TargetControlID="buttonRaiseItemPopup"
                        PopupControlID="divData" BackgroundCssClass="modalBackground" DropShadow="True"
                        BehaviorID="wardpopup" PopupDragHandleControlID="divTitle" Enabled="True" DynamicServicePath=""
                        CancelControlID="buttonClose" X="450" Y="0">
                    </ajaxToolkit:ModalPopupExtender>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="buttonAddWard" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="gridWardList" EventName="RowCommand" />
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
                                    <td style="width: 100%; height: 19px;">
                                        <span style="font-weight: bold; color: Black">
                                            <asp:Label ID="lblNotice" runat="server">Add Edit Wards</asp:Label></span>
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
                            <asp:Button ID="btnOkAction" runat="server" Text="OK" Width="80px" Style="border: solid 1px #808080;" />
                        </div>
                    </asp:Panel>
                    <asp:Button ID="btn" runat="server" Style="display: none" />
                    <ajaxToolkit:ModalPopupExtender ID="notifyPopupExtender" runat="server" TargetControlID="btn"
                        PopupControlID="pnNotify" BackgroundCssClass="modalBackground" DropShadow="True"
                        PopupDragHandleControlID="pnPopup_Title" Enabled="True" DynamicServicePath=""
                        CancelControlID="btnOkAction">
                    </ajaxToolkit:ModalPopupExtender>
                </ContentTemplate>
                <Triggers>
                </Triggers>
            </asp:UpdatePanel>
             </div>            
 </div>
	 <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>
     </div>
     </div>


    
    <asp:UpdateProgress ID="sProgress" runat="server" DisplayAfter="5">
        <ProgressTemplate>
            <div style="width: 100%; height: 100%; position: fixed; top: 0px; left: 0px; z-index: 300000;
                vertical-align: middle;">
                <table style="position: relative; top: 45%; left: 45%; border: solid 1px #808080;
                    background-color: #FFFFC0; width: 110px; height: 24px;" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="right" valign="middle" style="width: 30px; height: 22px;">
                            <img src="../Images/loading.gif" height="16px" width="16px" alt="" />
                        </td>
                        <td align="left" valign="middle" style="font-weight: bold; color: #808080; width: 80px;
                            height: 22px; padding-left: 5px">
                            Processing....
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
