<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PayBillCashAndDeposit.ascx.cs"
    Inherits="IQCare.Web.Billing.PayBillCashAndDeposit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:UpdatePanel ID="notificationPanel" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Button ID="btn" runat="server" Style="display: none" />
        <asp:Panel ID="pnNotify" runat="server" Style="display: none; width: 460px; border: solid 1px #808080;
            background-color: #E0E0E0; z-index: 15000">
            <asp:Panel ID="pnPopup_Title" runat="server" Style="border: solid 1px #808080; margin: 0px 0px 0px 0px;
                cursor: move; height: 18px">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 18px">
                    <tr>
                        <td style="width: 5px; height: 19px;">
                        </td>
                        <td style="width: 100%; height: 19px;">
                            <span style="font-weight: bold; color: White">
                                <asp:Label ID="lblNotice" runat="server">Pay Bill</asp:Label></span>
                        </td>
                        <td style="width: 5px; height: 19px;">
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <table border="0" cellpadding="15" cellspacing="0" style="width: 100%;">
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
                <asp:Button ID="btnOkAction" runat="server" Text="OK" Width="80px" Style="border: solid 1px #808080;"
                    OnClick="btnOkAction_Click" /></div>
        </asp:Panel>
        <ajaxToolkit:ModalPopupExtender ID="notifyPopupExtender" runat="server" TargetControlID="btn"
            PopupControlID="pnNotify" BackgroundCssClass="modalBackground" DropShadow="false" BehaviorID="PBCmpeBID01"
            PopupDragHandleControlID="pnPopup_Title" Enabled="True" DynamicServicePath="" Y="20">
        </ajaxToolkit:ModalPopupExtender>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdatePanel runat="server" ID="upPayPanel" UpdateMode="Conditional" ChildrenAsTriggers="true">
    <ContentTemplate>
     <asp:HiddenField ID="HTran" runat="server" />
        <asp:HiddenField ID="HBillAmount" runat="server" />
        <asp:HiddenField ID="HBillID" runat="server" />
        <asp:HiddenField ID="HPatientID" runat="server" />
        <asp:HiddenField ID="HUserID" runat="server" />
        <asp:HiddenField ID="HLocationID" runat="server" />
        <asp:HiddenField ID="HDAmountDue" runat="server" />
        <asp:HiddenField ID="HDAmountToPay" runat="server" />
        <asp:HiddenField ID="HPayMethodName" runat="server" />
        <asp:HiddenField ID="HPayMethodID" runat="server" />
         <asp:HiddenField ID="HPayMode" runat="server" />
        <asp:Panel ID="panelCompute" runat="server" Style="display: block; width: 480px;" DefaultButton="buttonCompute">
        <div class="row">
 <div class="col-md-5 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Payment Mode:</label>
             </div>
             <div class="col-md-7 col-sm-12 col-xs-12 form-group">
             <asp:Label Style="padding-left: 10px" ID="labelPaymentMode" runat="server" />
             </div>
</div>
<div class="row">
 <div class="col-md-5 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Ref No:</label>
             </div>
             <div class="col-md-5 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="textReferenceNo" runat="server"  Width="95%" CssClass="form-control" AutoComplete="Off" MaxLength="50"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                        TargetControlID="textReferenceNo" FilterType="Custom, Numbers, LowercaseLetters, UppercaseLetters"
                                        ValidChars=".-" />
             </div>
</div>
<asp:Panel runat="server" ID="panelDeposit">
<div class="row">
 <div class="col-md-5 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Available Deposit:</label>
             </div>
             <div class="col-md-7 col-sm-12 col-xs-12 form-group">
              <asp:Label Style="padding-left: 10px" ID="labelAvailableDeposit" runat="server" />
             </div>
</div>
</asp:Panel>
<div class="row">
 <div class="col-md-5 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Outstanding Amount:</label>
             </div>
             <div class="col-md-7 col-sm-12 col-xs-12 form-group">
            <asp:Label Style="padding-left: 10px" ID="labelAmountOutstanding" runat="server" />
             </div>
</div>
<div class="row">
 <div class="col-md-5 col-sm-12 col-xs-12 form-group">
 <label class="control-label">Amount to pay:</label>
 </div>
 <div class="col-md-7 col-sm-12 col-xs-12 form-group" title="The amount of the bill to pay">
 <asp:TextBox ID="textAmountToPay" runat="server" Width="95%" CssClass="form-control" AutoComplete="Off"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="fteAmountToPay" runat="server" TargetControlID="textAmountToPay"
                                        FilterType="Numbers, Custom" ValidChars="." />
                                    <asp:RangeValidator ID="rgAmountToPay" runat="server" ControlToValidate="textAmountToPay"
                                        Type="Double" MinimumValue="0" MaximumValue="10" ErrorMessage="The value should be between 0 and  the outstanding amount"
                                        Display="Dynamic" Enabled="True" />
                                    <asp:Label Style="padding-left: 10px; font-weight: bold;" ID="lblAmountToPay" runat="server"
                                        Visible="false" />
 </div>
 </div>
<div class="row">
<hr />
</div>
<asp:Panel runat="server" ID="panelTenderedAmount">
<div class="row">
 <div class="col-md-5 col-sm-12 col-xs-12 form-group">
 <label class="control-label" id="labelAmountTendered" runat="server">Tendered Amount:</label>
 </div>
 <div class="col-md-7 col-sm-12 col-xs-12 form-group">
<asp:TextBox ID="textTenderedAmount" runat="server" Width="95%" CssClass="form-control" AutoComplete="Off">0.0</asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="fteTenderedAmount" runat="server" TargetControlID="textTenderedAmount"
                                            FilterType="Numbers, Custom" ValidChars="." />
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="textAmountToPay"
                                            ControlToValidate="textTenderedAmount" Display="Dynamic" ErrorMessage="Value should be greater than or equal to Amount to pay"
                                            Operator="GreaterThanEqual" Type="Double"></asp:CompareValidator>
 </div>
 </div>
 </asp:Panel>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
<asp:Button ID="buttonCompute" runat="server" Text="Compute" OnClick="buttonCompute_Click" Enabled="True" CssClass="btn btn-primary" Height="30px" Width="30%" style="text-align:left;"/>
<label class="fa fa-desktop" style="margin-left: -5%; margin-right:2%; vertical-align: sub; color: #fff;"></label>
<asp:Button ID="buttonStepBack" runat="server" Text="Cancel" Enabled="True" CausesValidation="False" onclick="buttonStepBack_Click" UseSubmitBehavior="False" 
 CssClass="btn btn-primary" Height="30px" Width="16%" style="text-align:left;"/>
 <label class="glyphicon glyphicon-remove-circle" style="margin-left: -4%; margin-right:2%; vertical-align: sub; color: #fff;"></label>
             </div>
</div>

        
 <div class="row">
 <hr style="border-width: 1px" />
 </div>
            
        </asp:Panel>
        <asp:Panel ID="panelFinish" runat="server" Style="width: 480px;" Visible="false" DefaultButton="btnFinish">
        <div class="row">
 <div class="col-md-5 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Total Bill:</label>
             </div>
             <div class="col-md-7 col-sm-12 col-xs-12 form-group">
             <label style="padding-left: 10px" id="lblTotalBill" runat="server"> 0:</label>
             </div>
            
 </div>
 <div class="row">
 <div class="col-md-5 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Amount to pay:</label>
             </div>
             <div class="col-md-7 col-sm-12 col-xs-12 form-group">
             <label style="padding-left: 10px" id="labelAmountTopay" runat="server">none</label>
             </div>
            
 </div>
 <div class="row">
 <div class="col-md-5 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Payment Type:</label>
             </div>
             <div class="col-md-7 col-sm-12 col-xs-12 form-group">
            <label style="padding-left: 10px" id="labelPaymentType" runat="server">none</label>
             </div>
            
 </div>
 <div class="row">
 <div class="col-md-5 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Amount Tendered:</label>
             </div>
             <div class="col-md-7 col-sm-12 col-xs-12 form-group">
            <label style="padding-left: 10px" id="lblPaid" runat="server">0:</label>
             </div>
            
 </div>
  <div class="row">
 <div class="col-md-5 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Change:</label>
             </div>
             <div class="col-md-7 col-sm-12 col-xs-12 form-group">
             <label style="padding-left: 10px" id="lblChange" runat="server">0:</label>
             </div>
            
 </div>
 <div class="row">
 <div class="col-md-5 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Amount Due:</label>
             </div>
             <div class="col-md-7 col-sm-12 col-xs-12 form-group">
            <label style="padding-left: 10px" id="labelAmountDue" runat="server">0:</label>
             </div>
            
 </div>
 <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <asp:CheckBox ID="ckbPrintReciept" MaxLength="20" runat="server" Text="Print Receipt" Checked="True" Visible="True" TextAlign="Left" CssClass="radioButtonList" />
             </div>
             
            
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
  <asp:Button ID="btnFinish" MaxLength="20" runat="server" Text="Finish" OnClick="btnFinish_Click" Enabled="false" CssClass="btn btn-primary" Height="30px" Width="18%" style="text-align:left;"/>          
  <label class="fa fa-flag-checkered" style="margin-left: -4%; margin-right:2%; vertical-align: sub; color: #fff;"></label>
  <asp:Button ID="btnCancel" MaxLength="20" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-primary" Height="30px" Width="18%" style="text-align:left;"/>
   <label class="glyphicon glyphicon-remove-circle" style="margin-left: -4%; margin-right:2%; vertical-align: sub; color: #fff;"></label>
             </div>
              </div>
            <table width="100%">
                <tr>
                    
                    <td align="center" width="25%" style="height: 100%">
                        <table width="100%" style="height: 100%">
                            <tr>
                                <td valign="middle">
                                    
                                </td>
                            </tr>
                            <tr>
                                <td align="center" valign="middle" width="20%">
                                    
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>               
            </table>
        </asp:Panel>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnFinish" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="buttonCompute" EventName="Click" />
        <%--  <asp:AsyncPostBackTrigger ControlID="ddlPaymentMode" EventName="SelectedIndexChanged" />--%>
    </Triggers>
</asp:UpdatePanel>
