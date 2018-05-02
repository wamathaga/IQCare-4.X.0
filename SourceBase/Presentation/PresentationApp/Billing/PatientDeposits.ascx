<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PatientDeposits.ascx.cs"
    Inherits="IQCare.Web.Billing.PatientDeposits" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:UpdatePanel ID="notificationPanel" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <button type="button" id="btnRaisePopup" runat="server" style="margin-bottom: 5px;
            text-align: left; height: 30px;" class="btn btn-primary" clientidmode="Static">
            New Deposit<span class="glyphicon glyphicon-new-window" style="padding-left: 10px;
                color: #fff;"></span></button>
        <%--<asp:Button ID="" runat="server" Text="New Deposit" Style="margin-bottom: 5px;
            text-align: left;" CssClass="btn btn-primary" Height="30px" Width="11%" />
        <label class="glyphicon glyphicon-new-window" style="margin-left: -2%; margin-right: 2%;
            vertical-align: sub; color: #fff;">
        </label>--%>
        <asp:Panel ID="divParameters" runat="server" Style="display: none; width: 380px;
            border: solid 0px #3c8dbc; background-color: #fff; z-index: 15000; height: 100px;"
            DefaultButton="btnNewDeposit">
            <div class="row">
                <div class="col-xs-12">
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            <h3 class="box-title">
                                &nbsp;<asp:Panel ID="divTitle" runat="server" Style="cursor: move;">
                                    <label class="control-label">
                                        New Cash Deposit</label>
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
                                <h3>
                                </h3>
                                <h3>
                                </h3>
                            </h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body table-responsive no-padding" style="overflow: hidden; margin-left: 5px;">
                            <%--Main Content Start--%>
                            <div class="row" align="center">
                                <br />
                            </div>
                            <div class="row">
                                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                    <img src="../images/mb_question.gif" alt="" width="32" height="32" />
                                </div>
                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                    <label class="control-label">
                                        <asp:Label ID="lblDepositAmount" runat="server" Text="Deposit Amount"></asp:Label></label>
                                </div>
                                <div class="col-md-7 col-sm-12 col-xs-12 form-group">
                                    <asp:TextBox ID="textAmount" runat="server" AutoCompleteType="None" AutoComplete="off"
                                        MaxLength="7" CssClass="form-control" Width="99%"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="fteAmountToPay" runat="server" TargetControlID="textAmount"
                                        FilterType="Numbers" />
                                </div>
                            </div>
                            <asp:Panel ID="divApproval" runat="server" Visible="true" Style="text-align: center;
                                padding: 10px; width: 360px">
                                <div class="row" align="center">
                                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                        <asp:Button ID="btnNewDeposit" runat="server" Text="Save" CssClass="btn btn-primary"
                                            Height="30px" Width="23%" Style="text-align: left;" OnClick="btnNewDeposit_Click" />
                                        <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -5.5%; vertical-align: sub;
                                            color: #fff; margin-right: 2%;">
                                        </label>
                                        <asp:Button ID="btnActionCancel" runat="server" Text="Cancel" CssClass="btn btn-primary"
                                            Height="30px" Width="23%" Style="text-align: left;" />
                                        <label class="glyphicon glyphicon-remove" style="margin-left: -5.5%; vertical-align: sub;
                                            color: #fff; margin-right: 2%;">
                                        </label>
                                    </div>
                                </div>
                            </asp:Panel>
                            <%--Main Content End--%>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <ajaxToolkit:ModalPopupExtender ID="parameterPopup" runat="server" BehaviorID="PDmpeBID01"
            TargetControlID="btnRaisePopup" PopupControlID="divParameters" BackgroundCssClass="modalBackground"
            CancelControlID="btnActionCancel" DropShadow="false" PopupDragHandleControlID="divTitle"
            Y="20">
        </ajaxToolkit:ModalPopupExtender>
        <!-- Confirmation Popup -->
        <asp:Button ID="btn" runat="server" Style="display: none" />
        <asp:Panel ID="pnNotify" runat="server" Style="display: none; width: 460px; border: solid 0px #3c8dbc;
            background-color: #fff; z-index: 15000; height: 100px;">
            <div class="row">
                <div class="col-xs-12">
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            <h3 class="box-title">
                                &nbsp;<asp:Panel ID="pnPopup_Title" runat="server" Style="cursor: move;">
                                    <asp:Label ID="lblNotice" runat="server">Add Editing Item</asp:Label>
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
                                <h3>
                                </h3>
                                <h3>
                                </h3>
                            </h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body table-responsive no-padding" style="overflow: hidden; margin-left: 5px;">
                            <%--Main Content Start--%>
                            <div class="row" align="center">
                                <br />
                            </div>
                            <div class="row">
                                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                    <label class="control-label">
                                        <asp:Image ID="imgNotice" runat="server" ImageUrl="~/images/mb_information.gif" Height="32px"
                                            Width="32px" /></label>
                                </div>
                                <div class="col-md-11 col-sm-12 col-xs-12 form-group text-center">
                                    <label class="control-label">
                                        <asp:Label ID="lblNoticeInfo" runat="server"></asp:Label></label>
                                </div>
                            </div>
                            <div class="row" align="center">
                                <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                    <asp:Button ID="btnOkAction" runat="server" Text="OK" CssClass="btn btn-primary"
                                        Height="30px" Width="15%" Style="text-align: left;" />
                                    <label class="glyphicon glyphicon-ok" style="margin-left: -4%; vertical-align: sub;
                                        color: #fff; margin-right: 2%;">
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
            BehaviorID="PDmpeBID02" PopupDragHandleControlID="pnPopup_Title" Enabled="True"
            DynamicServicePath="" Y="20">
        </ajaxToolkit:ModalPopupExtender>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnNewDeposit" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="btnRefundYes" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="buttonRefund" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>
<asp:UpdatePanel ID="divComponent" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:HiddenField ID="HCanReceive" runat="server" />
        <asp:HiddenField ID="HCanRefund" runat="server" />
        <asp:HiddenField ID="HPatientID" runat="server" />
        <asp:HiddenField ID="HLocationID" runat="server" />
        <asp:HiddenField ID="HPrintMethodName" runat="server" />
        <asp:HiddenField ID="HPrintURL" runat="server" />
        <div class="row">
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <label class="control-label">
                    Date of Last Deposit:</label>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <label class="control-label">
                    <asp:Label ID="labelDepositDate" runat="server"></asp:Label></label>
            </div>
            <div class="col-md-6 col-sm-12 col-xs-12 form-group text-right">
                <asp:Button ID="buttonViewTransactions" runat="server" Text="View Transactions" Enabled="True"
                    OnClick="buttonViewTransactions_Click" CssClass="btn btn-primary" Height="30px"
                    Width="28%" Style="text-align: left;" />
                <label class="glyphicon glyphicon-eye-open" style="margin-left: -6%; margin-right: 2%;
                    vertical-align: sub; color: #fff;">
                </label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <label class="control-label">
                    Last Amount Deposited:</label>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <label class="control-label">
                    <asp:Label ID="labelDepositedAmount" runat="server"></asp:Label></label>
            </div>
            <div class="col-md-6 col-sm-12 col-xs-12 form-group">
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <label class="control-label">
                    Received By:</label>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <label class="control-label">
                    <asp:Label ID="labelReceivedBy" runat="server" Height="30px"></asp:Label></label>
            </div>
            <div class="col-md-6 col-sm-12 col-xs-12 form-group">
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <label class="control-label">
                    Total Amount Available:</label>
            </div>
            <div class="col-md-9 col-sm-12 col-xs-12 form-group">
                <div style="white-space: nowrap; vertical-align: middle; text-align: left">
                    <asp:Label ID="labelTotalAvailable" runat="server" Height="30px" Text="0.0"></asp:Label>&nbsp;&nbsp;
                    <span style='display: <%# ShowHideRefund() %>; white-space: nowrap'>
                        <asp:Button ID="buttonRefund" runat="server" CausesValidation="false" OnClick="btnRefundYes_Click"
                            CommandName="Refund" Text="Return" ToolTip="Return the available deposit to the client"
                            CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align: left;">
                        </asp:Button>
                        <label class="fa fa-undo" style="margin-left: -2%; vertical-align: sub; color: #fff;
                            margin-right: 2%;">
                        </label>
                    </span>
                    <ajaxToolkit:ConfirmButtonExtender ID="cbeRefundPay" runat="server" DisplayModalPopupID="mpeRefundPay"
                        TargetControlID="buttonRefund"></ajaxToolkit:ConfirmButtonExtender>
                    <ajaxToolkit:ModalPopupExtender ID="mpeRefundPay" runat="server" PopupControlID="pnlRefundPopup"
                        TargetControlID="buttonRefund" OkControlID="btnRefundYes" CancelControlID="btnRefundNo"
                        BackgroundCssClass="modalBackground">
                    </ajaxToolkit:ModalPopupExtender>
                    <asp:Panel ID="pnlRefundPopup" runat="server" Style="display: none; background-color: #FFFFFF;
                        width: 300px; border: 3px solid #479ADA;">
                        <div style="background-color: #479ADA; height: 30px; color: White; line-height: 30px;
                            text-align: center; font-weight: bold;">
                            Return the entire available deposit in cash.
                        </div>
                        <div style="min-height: 50px; line-height: 30px; text-align: center; font-weight: bold;">
                            <table border="0" cellpadding="15" cellspacing="0" style="width: 100%; height: 18px">
                                <tr>
                                    <td style="width: 48px" valign="middle" align="center">
                                        <img src="../Images/mb_question.gif" alt="" width="32" height="32" />
                                    </td>
                                    <td style="width: 100%; padding-left: 20px" valign="middle" align="left">
                                        Confirm deposit return ?<br />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="padding: 3px;" align="center">
                            <asp:Button ID="btnRefundYes" runat="server" Text="Return" CssClass="btn btn-primary"
                                Height="30px" Width="25%" Style="text-align: left;" />
                            <label class="fa fa-undo" style="margin-left: -5%; vertical-align: sub; color: #fff;
                                margin-right: 2%;">
                            </label>
                            <asp:Button ID="btnRefundNo" runat="server" Text="Cancel" CssClass="btn btn-primary"
                                Height="30px" Width="25%" Style="text-align: left;" />
                            <label class="glyphicon glyphicon-remove" style="margin-left: -5%; vertical-align: sub;
                                color: #fff; margin-right: 2%;">
                            </label>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnNewDeposit" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="btnRefundYes" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>
<asp:UpdatePanel ID="transactionPanel" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Button ID="btnShowTransactions" runat="server" Text="" Width="60px" Style="display: none" />
        <asp:Panel ID="divTransactions" runat="server" Style="display: none; width: 740px;
            border: solid 0px #3c8dbc; background-color: #fff; height: 300px; overflow: auto">
            <div class="row">
                <div class="col-xs-12">
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            <h3 class="box-title">
                                &nbsp;<asp:Panel ID="divItemTitle" runat="server" Style="cursor: move;">
                                    <asp:Label ID="labelItemTitle" runat="server" ForeColor="White">Patient Deposit Transactions</asp:Label>
                                </asp:Panel>
                                <h3>
                                </h3>
                                <h3>
                                </h3>
                            </h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body table-responsive no-padding" style="overflow: hidden; margin-left: 5px;">
                            <%--Main Content Start--%>
                            <div class="row" align="center">
                                <br />
                            </div>
                            <div class="row" align="center">
                                <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                    <div id="divDeposits" class="grid" style="width: 100%; margin-top: 8px;">
                                        <div class="rounded">
                                            <div class="mid-outer1">
                                                <div class="mid-inner">
                                                    <div class="mid" style="height: 220px; overflow: auto">
                                                        <div id="div-depositGridview" class="GridView whitebg">
                                                            <asp:GridView ID="gridTransaction" runat="server" AllowSorting="False" AutoGenerateColumns="False"
                                                                BorderColor="White" BorderWidth="1px" CellPadding="0" CssClass="table table-bordered table-hover"
                                                                DataKeyNames="transactionid" Enabled="true" EnableModelValidation="True" GridLines="None"
                                                                HorizontalAlign="Left" ShowFooter="True" ShowHeaderWhenEmpty="True" Width="100%"
                                                                BorderStyle="Solid">
                                                                <Columns>
                                                                    <asp:BoundField DataField="ReferenceNumber" HeaderText="Ref Number" />
                                                                    <asp:BoundField DataField="TransactionDate" HeaderText="Transaction Date" DataFormatString="{0:dd-MMM-yyyy HH:mm}" />
                                                                    <asp:TemplateField InsertVisible="False" ShowHeader="True" HeaderText="Amount">
                                                                        <ItemTemplate>
                                                                            <div style='text-align: <%# FormatAmount(Eval("TransactionType")) %>'>
                                                                                <%# FormatAmountDisplay(Eval("TransactionType"),DataBinder.Eval(Container.DataItem, "Amount", "{0:N}"))%>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="ReceivedBy" HeaderText="Received By" />
                                                                    <asp:BoundField DataField="TransactionDescription" HeaderText="Description" />
                                                                    <asp:TemplateField InsertVisible="False" ShowHeader="False" Visible="false">
                                                                        <ItemTemplate>
                                                                            <div style="white-space: nowrap">
                                                                                <span style='white-space: nowrap'>
                                                                                    <asp:Button ID="transactionPrint" runat="server" CausesValidation="false" CommandName="PrintTransaction"
                                                                                        Text="Print" CommandArgument="<%# Container.DataItemIndex %>" CssClass="btn btn-primary"
                                                                                        Height="30px" Width="9%" Style="text-align: left;" />
                                                                                    <label class="glyphicon glyphicon-print" style="margin-left: -3%; vertical-align: sub;
                                                                                        color: #fff; margin-right: 2%;">
                                                                                    </label>
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
                                            <%-- <div class="bottom-outer">
                                        <div class="bottom-inner">
                                            <div class="bottom">
                                            </div>
                                        </div>
                                    </div>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" align="center">
                                <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                    <asp:Button ID="buttonCloseDetails" runat="server" Text="Close" CssClass="btn btn-primary"
                                        Height="30px" Width="12%" Style="text-align: left;" />
                                    <label class="glyphicon glyphicon-remove" style="margin-left: -3%; vertical-align: sub;
                                        color: #fff; margin-right: 2%;">
                                    </label>
                                </div>
                            </div>
                            <%--Main Content End--%>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <ajaxToolkit:ModalPopupExtender ID="depositDetailsPopup" runat="server" BehaviorID="PDmpeBID03"
            TargetControlID="btnShowTransactions" PopupControlID="divTransactions" BackgroundCssClass="modalBackground"
            CancelControlID="buttonCloseDetails" DropShadow="false" PopupDragHandleControlID="divItemTitle"
            Y="10">
        </ajaxToolkit:ModalPopupExtender>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnNewDeposit" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="btnRefundYes" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="buttonViewTransactions" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>
