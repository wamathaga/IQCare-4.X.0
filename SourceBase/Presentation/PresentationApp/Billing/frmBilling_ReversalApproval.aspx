<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    CodeBehind="frmBilling_ReversalApproval.aspx.cs" Inherits="IQCare.Web.Billing.frmBilling_ReversalApproval" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="TransactionReversal.ascx" TagName="TransactionReversal" TagPrefix="uc1" %>
<asp:Content ID="ctMain" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <script language="javascript" type="text/javascript">
        function openReceiptPage(path) {
            window.open(path, 'ReceiptPage', 'toolbars=no,location=no,directories=no,dependent=yes,top=100,left=30,maximize=no,resize=no,width=1000,height=800,scrollbars=yes');
        }
    </script>
    <div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">Bill Reversal Approval</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
	<div class="row" align="center">
         <br />           
        </div>
       
        
            <asp:UpdatePanel ID="panelFilter" runat="server" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <asp:Panel ID="divError" runat="server" Style="padding: 5px" CssClass="background-color: #FFFFC0; border: solid 1px #C00000"
                        HorizontalAlign="Left" Visible="false">
                        <asp:Label ID="lblError" runat="server" Style="font-weight: bold; color: #800000"
                            Text=""></asp:Label>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
      
        <asp:UpdatePanel ID="divComponent" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
            <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
            <uc1:TransactionReversal ID="ReverseTransaction" runat="server" IsApproval="True"
                    PrintReceiptJSMethod="openReceiptPage" PrintReceiptURL="./frmBilling_Reciept.aspx" />
             </div>
            
 </div>
               
            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="notificationPanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <!-- Confirmation Popup -->
                <asp:Button ID="btn" runat="server" Style="display: none" />
                <asp:Panel ID="pnNotify" runat="server" Style="display: none; width: 460px; border: solid 1px #808080;
                    background-color: #E0E0E0; z-index: 15000">
                    <asp:Panel ID="pnPopup_Title" runat="server" Style="border: solid 1px #808080; margin: 0px 0px 0px 0px;
                        cursor: move; height: 18px">
                        <table class="table-condensed" style="width: 100%; height: 18px">
                            <tr>
                                <td style="width: 5px; height: 19px;">
                                </td>
                                <td style="width: 100%; height: 19px;">
                                    <span style="font-weight: bold; color: Black">
                                        <asp:Label ID="lblNotice" runat="server">Add Editing Item</asp:Label></span>
                                </td>
                                <td style="width: 5px; height: 19px;">
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <table class="table-condensed" style="width: 100%;">
                        <tr>
                            <td style="width: 48px" valign="middle" align="center">
                                <asp:Image ID="imgNotice" runat="server" ImageUrl="~/images/mb_information.gif" Height="32px"
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
                <ajaxToolkit:ModalPopupExtender ID="notifyPopupExtender" runat="server" TargetControlID="btn"
                    PopupControlID="pnNotify" BackgroundCssClass="modalBackground" DropShadow="True"
                    PopupDragHandleControlID="pnPopup_Title" Enabled="True" DynamicServicePath="">
                </ajaxToolkit:ModalPopupExtender>
            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>
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
        
    
	 <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>
     </div>
     </div>
  
  
</asp:Content>
