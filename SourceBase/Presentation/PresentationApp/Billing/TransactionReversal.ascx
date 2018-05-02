<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TransactionReversal.ascx.cs"
    Inherits="IQCare.Web.Billing.TransactionReversal" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<link href="../Style/bootstrap-3.3.6-dist/css/bootstrap.css" rel="stylesheet" type="text/css" />
<link href="../Style/bootstrap-3.3.6-dist/css/bootstrap-theme.css" rel="stylesheet"
    type="text/css" />
    <style type="text/css">
.modalBackground
{
  background-color:#F5F5F5;
  filter: alpha(opacity=80);
  opacity: 0.80;  
}
</style>
<script src="../Style/bootstrap-3.3.6-dist/html5Shiv/html5shiv.js" type="text/javascript"></script>
<asp:UpdatePanel ID="panelFilter" runat="server" ChildrenAsTriggers="true">
    <ContentTemplate>
    <div class="row" style="width:97%">
 <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Filter By:</label>
             </div>
             <div class="col-md-10 col-sm-12 col-xs-12 form-group">
             <asp:RadioButtonList ID="radioShowOptions" runat="server" RepeatDirection="Horizontal"
                            OnSelectedIndexChanged="radioShowOptions_SelectedIndexChanged" AutoPostBack="true" CssClass="radioButtonList">
                            <asp:ListItem Value="PENDING" Text="Pending" Selected="True" />
                            <asp:ListItem Value="APPROVED" Text="Approved" />
                            <asp:ListItem Value="REJECTED" Text="Rejected" />
                            <asp:ListItem Value="ALL" Text="All Requests"></asp:ListItem>
                        </asp:RadioButtonList>
             </div>
            
 </div>
       
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdatePanel ID="divComponent" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:HiddenField ID="HTranID" runat="server" />
        <asp:HiddenField ID="HCanRefund" runat="server" />
        <asp:HiddenField ID="HApprovalMode" runat="server" />
        <asp:HiddenField ID="HReversalId" runat="server" />
        <asp:HiddenField ID="HBillId" runat="server" />
        <asp:HiddenField ID="HPatientID" runat="server" />
        <asp:HiddenField ID="HLocationID" runat="server" />
        <asp:HiddenField ID="HPrintMethodName" runat="server" />
        <asp:HiddenField ID="HPrintURL" runat="server" />
        <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <div id="divReversal" class="grid">
                            <%--<div class="rounded">--%>
                                <div class="mid-outer">
                                    <div class="mid-inner">
                                        <div class="mid" style="height: 280px; overflow: auto">
                                            <div id="div-reversalGridview" class="GridView whitebg">
                                                <asp:HiddenField ID="ReversalOpenItem" runat="server" />
                                                <asp:GridView ID="gridReversal" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                    BorderColor="White" BorderWidth="1px" CellPadding="0" GridLines="None" CssClass="table table-bordered table-hover"
                                                    ShowHeaderWhenEmpty="True" Width="100%" DataKeyNames="transactionreversalid,transactionid,patientid,billid"
                                                    OnRowCommand="gridReversal_RowCommand" OnRowCreated="gridReversal_RowCreated"
                                                    OnRowDataBound="gridReversal_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="ReceiptNumber" HeaderText="Receipt #" />
                                                        <asp:TemplateField HeaderText="Bill #" InsertVisible="False">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="buttonBillDetails" runat="server" CausesValidation="false" CommandName="BillDetails"
                                                                    Text='<%# DataBinder.Eval(Container.DataItem, "BillNumber")%>' CommandArgument="<%# Container.DataItemIndex %>"
                                                                    Font-Bold="false" Font-Size="X-Small"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="RequestDate" HeaderText="Date Requested" DataFormatString="{0:dd-MMM-yyyy HH:mm}" />
                                                        <asp:BoundField DataField="TransactionAmount" HeaderText="Amount" />
                                                        <asp:BoundField DataField="RequestedBy" HeaderText="Requested By" />
                                                        <asp:TemplateField HeaderText="Reversal Reason" InsertVisible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="labelReversalReason" runat="server"></asp:Label>
                                                                <span style='display: <%# ShowResonNotes(Eval("ReversalReason")) %>; white-space: nowrap'>
                                                                    <asp:LinkButton ID="linkReversal" runat="server" CausesValidation="false" CommandName="ViewNotes"
                                                                        Text="..." CommandArgument="<%# Container.DataItemIndex %>" Font-Bold="True"></asp:LinkButton></span>
                                                                <ajaxToolkit:ModalPopupExtender ID="mpeViewReversalNotes" runat="server" PopupControlID="pnlPopupReversalNotes"
                                                                    TargetControlID="linkReversal" CancelControlID="btnReversalClose" BackgroundCssClass="modalBackground">
                                                                </ajaxToolkit:ModalPopupExtender>
                                                                <asp:Panel ID="pnlPopupReversalNotes" runat="server" Style="display: none; background-color: #FFFFFF;
                                                                    width: 300px; border: 3px solid #479ADA;">
                                                                    <div style="background-color: #479ADA; height: 30px; color: White; line-height: 30px;
                                                                        text-align: center; font-weight: bold;">
                                                                        Reversal Reason
                                                                        <br />
                                                                    </div>
                                                                    <div style="min-height: 50px; line-height: 30px; text-align: left; font-weight: bold;
                                                                        padding: 5px;">
                                                                        <%# DataBinder.Eval(Container.DataItem, "ReversalReason")%>
                                                                        on
                                                                        <%# DataBinder.Eval(Container.DataItem, "RequestDate", "{0:dd-MMM-yyyy HH:mm}")%>&nbsp;by
                                                                        <%# DataBinder.Eval(Container.DataItem, "RequestedBy")%>.&nbsp;Transaction Amount:&nbsp;<b>
                                                                            <%# DataBinder.Eval(Container.DataItem, "TransactionAmount")%>
                                                                        </b>
                                                                    </div>
                                                                    <div style="padding: 5px;" align="center">
                                                                        <asp:Button ID="btnReversalClose" runat="server" Text="Close" Style="margin-left: 10px" /></div>
                                                                </asp:Panel>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status" InsertVisible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="labelApprovalStatus" runat="server" Text='<%# Bind("ApprovalStatus") %>'></asp:Label>
                                                                <span style='display: <%# ShowReversalNotes(Eval("ApprovalStatus")) %>; white-space: nowrap'>
                                                                    <asp:LinkButton ID="buttonViewNotes" runat="server" CausesValidation="false" CommandName="ViewNotes"
                                                                        Text="..." CommandArgument="<%# Container.DataItemIndex %>" Font-Bold="True"></asp:LinkButton></span>
                                                                <ajaxToolkit:ModalPopupExtender ID="mpeViewNotes" runat="server" PopupControlID="pnlPopupNotes"
                                                                    TargetControlID="buttonViewNotes" CancelControlID="btnClose" BackgroundCssClass="modalBackground">
                                                                </ajaxToolkit:ModalPopupExtender>
                                                                <asp:Panel ID="pnlPopupNotes" runat="server" Style="display: none; background-color: #FFFFFF;
                                                                    width: 300px; border: 3px solid #479ADA;">
                                                                    <div style="background-color: #479ADA; height: 30px; color: White; line-height: 30px;
                                                                        text-align: center; font-weight: bold;">
                                                                        <br />
                                                                    </div>
                                                                    <div style="min-height: 50px; line-height: 30px; text-align: center; font-weight: bold;">
                                                                        <%# DataBinder.Eval(Container.DataItem, "ApprovalStatus")%>
                                                                        on &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "ApprovalDate", "{0:dd-MMM-yyyy HH:mm}")%>&nbsp;&nbsp;by
                                                                        <%# DataBinder.Eval(Container.DataItem, "ApprovedBy")%>&nbsp;&nbsp; with comments
                                                                        <b>
                                                                            <%# DataBinder.Eval(Container.DataItem, "ApprovalNotes")%>
                                                                        </b>
                                                                    </div>
                                                                    <div style="padding: 3px;" align="right">
                                                                        <asp:Button ID="btnClose" runat="server" Text="Close" Style="margin-left: 10px" /></div>
                                                                </asp:Panel>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField InsertVisible="False" HeaderText="Refunded?">
                                                            <ItemTemplate>
                                                                <%# (Boolean.Parse(DataBinder.Eval(Container.DataItem, "[Refunded]").ToString())) ? "Yes" : "No"%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="RefundDate" DataFormatString="{0:dd-MMM-yyyy HH:mm}" HeaderText="Refund Date" />
                                                        <asp:TemplateField InsertVisible="False" ShowHeader="False">
                                                            <ItemTemplate>
                                                                <div style="text-align: center; padding: 10px; white-space: nowrap; display: <%# ShowApproval(Eval("ApprovalStatus")) %>">
                                                                    <asp:Button ID="btnApprove" runat="server" Text="Approve" Height="30px" Style="margin-right: 10px"
                                                                        CssClass="btn btn-primary" ForeColor="White" CommandName="Approve" CommandArgument="<%# Container.DataItemIndex %>" />
                                                                    <asp:Button ID="btnReject" runat="server" Text="Reject" Height="30px" CssClass="btn btn-primary"
                                                                        ForeColor="White" CommandName="Reject" CommandArgument="<%# Container.DataItemIndex %>" />
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField InsertVisible="False" ShowHeader="False">
                                                            <ItemTemplate>
                                                                <div style="white-space: nowrap">
                                                                    <span style='display: <%# ShowHideRefund(Eval("ApprovalStatus"),Eval("Refunded")) %>;
                                                                        white-space: nowrap'>
                                                                        <asp:Button ID="buttonRefund" runat="server" CausesValidation="false" Height="30px"
                                                                            CssClass="btn btn-primary" CommandName="Refund" Text="Refund" CommandArgument="<%# Container.DataItemIndex %>">
                                                                        </asp:Button></span>
                                                                    <ajaxToolkit:ConfirmButtonExtender ID="cbeRefundPay" runat="server" DisplayModalPopupID="mpeRefundPay"
                                                                        TargetControlID="buttonRefund"></ajaxToolkit:ConfirmButtonExtender>
                                                                    <ajaxToolkit:ModalPopupExtender ID="mpeRefundPay" runat="server" PopupControlID="pnlRefundPopup"
                                                                        TargetControlID="buttonRefund" OkControlID="btnRefundYes" CancelControlID="btnRefundNo"
                                                                        BackgroundCssClass="modalBackground">
                                                                    </ajaxToolkit:ModalPopupExtender>
                                                                    <asp:Panel ID="pnlRefundPopup" runat="server" Style="display: none; background-color: #fff;
                                                                        width: 350px; border: 0px solid #3c8dbc; height:140px">
                                                                       
   
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">Refund the cash</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
	<div class="row" align="center">
         <br />           
        </div>
        <div class="row">
 <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <img src="../Images/mb_question.gif" alt="" width="32" height="32" />
             </div>
              <div class="col-md-10 col-sm-12 col-xs-12 form-group">
             <label class="control-label" style="margin-left:10px;"> Confirm Refund Cash ?</label>
             </div>
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Button ID="btnRefundYes" runat="server" Text="Refund" CssClass="btn btn-primary" Height="30px" Width="25%" Style="text-align:left;"  />
 <label class="fa fa-undo" style="margin-left: -5.5%; vertical-align: sub; color: #fff;margin-right: 2%;""></label>
 <asp:Button ID="btnRefundNo" runat="server" Text="Cancel" CssClass="btn btn-primary" Height="30px" Width="25%" Style="text-align:left;"  />
 <label class="glyphicon glyphicon-remove" style="margin-left: -5.5%; vertical-align: sub; color: #fff;margin-right: 2%;""></label>
             </div>
 </div>

	 <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>
     
    
     
                                                                    </asp:Panel>
                                                                    <span style='display: <%# ShowPrintRefund(Eval("Refunded")) %>; white-space: nowrap'>
                                                                        <asp:Button ID="refundPrint" runat="server" CausesValidation="false" CommandName="PrintRefund"
                                                                            CssClass="btn btn-primary" Height="30px" Text="Print Refund" CommandArgument="<%# Container.DataItemIndex %>" />
                                                                    </span>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="bottom-outer">
                                    <div class="bottom-inner">
                                        <div class="bottom">
                                        </div>
                                    </div>
                                </div>
                           <%-- </div>--%>
                        </div>
             </div>
            
 </div>
       
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="radioShowOptions" EventName="SelectedIndexChanged" />
        <asp:AsyncPostBackTrigger ControlID="btnApprove" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="btnReject" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="gridReversal" EventName="RowCommand" />
    </Triggers>
</asp:UpdatePanel>
<asp:UpdatePanel ID="billItemPanel" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Button ID="btnShowItems" runat="server" Text="" Width="60px" Style="display: none" />
        <asp:Panel ID="divBillItems" runat="server" Style="display: none; width: 680px; border: solid 1px #3c8dbc;
            background-color: #fff; border-radius: 5px; position: fixed; left: 0px; top: 0px;  z-index: 15000 !important; position: relative;">
            <asp:Panel ID="divItemTitle" runat="server" Style="border: solid 1px #3c8dbc; margin: 0px 0px 0px 0px;
                background-color: #3c8dbc; cursor: move; height: 30px;color:#fff;">
                <table class="table-condensed" style="width: 100%; height: 18px">
                    <tr>
                        <td style="width: 5px; height: 19px;">
                        </td>
                        <td style="width: 100%; height: 19px;">
                            <span style="font-weight: bold;">
                                <asp:Label ID="labelItemTitle" runat="server">Bill Details & Items in the Bill</asp:Label></span>
                        </td>
                        <td style="width: 5px; height: 19px;">
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <table class="table-condensed" width="100%" border="0">
                <tbody>
                    <tr>
                        <td class="form">
                            <table width="100%">
                                <tr>
                                    <td style="font-weight: bold">
                                       <label  class="control-label"> Bill Number:</label>
                                    </td>
                                    <td>
                                        <asp:Label ID="labelBillNumber" runat="server"></asp:Label>
                                    </td>
                                    <td style="font-weight: bold">
                                        <label  class="control-label"> BillDate:</label>
                                    </td>
                                    <td>
                                        <asp:Label ID="labelBillDate" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold">
                                        <label  class="control-label"> Total Bill Amount:</label>
                                    </td>
                                    <td>
                                        <asp:Label ID="labelBillAmount" runat="server"></asp:Label>
                                    </td>
                                    <td style="font-weight: bold">
                                        <label  class="control-label"> Outstanding Amount:</label>
                                    </td>
                                    <td>
                                        <asp:Label ID="labelUnPaidAmount" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold">
                                       <label  class="control-label"> Billed By:</label>
                                    </td>
                                    <td>
                                        <asp:Label ID="labelBilledBy" runat="server"></asp:Label>
                                    </td>
                                    <td style="font-weight: bold">
                                        <label  class="control-label"> Bill Status:</label>
                                    </td>
                                    <td>
                                        <asp:Label ID="labelBillStatus" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="display: <% = NeedShowPatient %>">
                                    <td style="font-weight: bold">
                                        <label  class="control-label"> Patient Name:</label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblname" runat="server"></asp:Label>
                                    </td>
                                    <td style="font-weight: bold">
                                        <label  class="control-label"> Patient Number:</label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblFacilityID" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" class="table-condensed">
                                <tr>
                                    <td class="form" valign="top" style="width: 100%">
                                    <div class="row box-header">
         <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <h5 class="box-title">Items in the bill</h5>
         </div>   
          <hr />          
            </div>
                                      
                                        <div class="mid" style="height: 280px; overflow: auto">
                                            <div id="div-payItems" class="GridView whitebg">
                                                <asp:GridView ID="gridBillItems" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                    BorderColor="White" BorderWidth="1px" CellPadding="0" CssClass="table table-bordered table-hover" DataKeyNames="billItemID,billid"
                                                    Enabled="true" EnableModelValidation="True" GridLines="None" HorizontalAlign="Left"
                                                    ShowFooter="False" ShowHeaderWhenEmpty="True" Width="100%" CaptionAlign="Top"
                                                    Visible="true" BorderStyle="Solid">
                                                    <Columns>
                                                        <asp:BoundField DataField="BillItemDate" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                        <asp:BoundField DataField="ItemName" HeaderText="Item Description" />
                                                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                                        <asp:BoundField DataField="sellingprice" HeaderText="Price" DataFormatString="{0:N}" />
                                                        <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:N}" />
                                                    </Columns>
                                                   
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </tbody>
            </table>
            <div style="background-color: #FFFFFF; border-top: solid 1px #808080; width: 100%;
                text-align: center; padding-top: 5px; padding-bottom: 5px">
                <asp:Button ID="buttonCloseDetails" runat="server" Text="Close" CssClass="btn btn-primary" Height="30px" Width="12%" Style="text-align:left;" />
                <label class="glyphicon glyphicon-remove" style="margin-left: -5%; vertical-align: sub; color: #fff;margin-right: 2%;""></label>
            </div>
        </asp:Panel>
        <ajaxToolkit:ModalPopupExtender ID="billDetailsPopup" runat="server" BehaviorID="TRmpeBID01"
            TargetControlID="btnShowItems" PopupControlID="divBillItems" BackgroundCssClass="modalBackground"
            CancelControlID="buttonCloseDetails" DropShadow="true" PopupDragHandleControlID="divItemTitle" Y="1">
        </ajaxToolkit:ModalPopupExtender>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="gridReversal" EventName="RowCommand" />
    </Triggers>
</asp:UpdatePanel>
<asp:UpdatePanel ID="notificationPanel" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Button ID="btnRaisePopup" runat="server" Text="" Width="60px" Style="display: none" />
        <asp:Panel ID="divParameters" runat="server" Style="display: none; width: 380px;
            border: solid 1px #808080; background-color: #F0F0F0">
            <asp:Panel ID="divTitle" runat="server" Style="border: solid 1px #3c8dbc; margin: 0px 0px 0px 0px;
                cursor: move; height: 22px; background-color: #3c8dbc; color:#fff;">
                <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <label class="control-label"><asp:Label ID="labelParamTitle" runat="server">Add Editing Item</asp:Label></label>
             </div>
 </div>

                
            </asp:Panel>
            <div class="row">
 <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <img src="../images/mb_question.gif" alt="" width="32" height="32" />
             </div>
             <div class="col-md-11 col-sm-12 col-xs-12 form-group">
             <label class="control-label required">*Please provide the reason</label>
             </div>
 </div>
 <div class="row">
 <div class="col-md-1 col-sm-12 col-xs-12 form-group">
            
             </div>
             <div class="col-md-11 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="textReason" Rows="2" Width="99%" runat="server" TextMode="MultiLine" CssClass="form-control" Style="resize:none;"></asp:TextBox>
             </div>
 </div>
 <div style="display: <%= sApproval %>">
 <div class="row">
 <div class="col-md-12 form-group">
             <label class="control-label"><asp:Panel ID="divCashRefund" runat="server" Style="padding: 5px; margin-top: 5px;
                                margin-bottom: 5px;" HorizontalAlign="left">
                                <asp:Label ID="labelCashRefund" runat="server" Style="color: #800000"
                                    Text="">Have you refunded the cash to the client?</asp:Label>
                            </asp:Panel></label>
             </div>
 </div>
 <div class="row">
 <div class="col-md-11 form-group" style="margin:5px;">
             <asp:RadioButtonList ID="rblOptionRefund" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="flow" CssClass="radioButtonList">
                                <asp:ListItem Value="No" Text="No, to be given by cashier" Selected="True" />
                                <asp:ListItem Value="Yes" Text="Yes" />
                            </asp:RadioButtonList>
             </div>
 </div>
</div>

            
            <asp:Panel ID="divApproval" runat="server" Visible="true" Style="text-align: center;
                padding: 10px; width: 380px">
                <asp:Button ID="btnApprove" runat="server" Text="Approve" OnClick="btnApprove_Click" CssClass="btn btn-primary" Height="30px" Width="25%" Style="text-align:left;" />
                <label class="fa fa-check" style="margin-left: -6%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
                <asp:Button ID="btnReject" runat="server" Text="Reject" OnClick="btnReject_Click" CssClass="btn btn-primary" Height="30px" Width="25%" Style="text-align:left;" />
                <label class="glyphicon glyphicon-remove" style="margin-left: -5%; vertical-align: sub; color: #fff; margin-right: 2%;" id="iconReject" runat="server" visible="false"></label>
                <asp:Button ID="btnActionCancel" runat="server" Text="Cancel" CssClass="btn btn-primary" Height="30px" Width="25%" Style="text-align:left;" />
                <label class="glyphicon glyphicon-remove" style="margin-left: -5%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
                </asp:Panel>

        </asp:Panel>
        <ajaxToolkit:ModalPopupExtender ID="parameterPopup" runat="server" BehaviorID="TRmpeBID02"
            TargetControlID="btnRaisePopup" PopupControlID="divParameters" BackgroundCssClass="modalBackground"
            CancelControlID="btnActionCancel" DropShadow="true" PopupDragHandleControlID="divTitle" Y="10">
        </ajaxToolkit:ModalPopupExtender>
        <!-- Confirmation Popup -->
        <asp:Button ID="btn" runat="server" Style="display: none" />
        <asp:Panel ID="pnNotify" runat="server" Style="display: none; width: 460px; border: solid 1px #808080;
            background-color: #E0E0E0; z-index: 15000">
            <asp:Panel ID="pnPopup_Title" runat="server" Style="border: solid 1px #808080; margin: 0px 0px 0px 0px;
                cursor: move; height: 18px">
                <table class="table-condensed" style="width: 100%; height: 18px">
                    <tr style="width: 100%; background-color: #479ADA;">
                        <td style="width: 5px; height: 19px;">
                        </td>
                        <td style="width: 100%; height: 19px; background-color: #479ADA;">
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
        <asp:AsyncPostBackTrigger ControlID="gridReversal" EventName="RowCommand" />
    </Triggers>
</asp:UpdatePanel>
