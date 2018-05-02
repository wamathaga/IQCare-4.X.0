<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    CodeBehind="frmBilling_PayBillByItems.aspx.cs" Inherits="IQCare.Web.Billing.frmBilling_PayBillByItems" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <%--<asp:ScriptManager ID="mst" runat="server">
    </asp:ScriptManager>--%>
    <script language="javascript" type="text/jscript">

        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
        function openReportPage(path) {
            // window.location.href = './frmBilling_ClientBill.aspx';
            window.open(path, 'popupwindow', 'toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=yes,resizable=no,width=950,height=650,scrollbars=yes');
        }
        
    </script>
    <br />
    <div>
        <div style="padding-top: 18px;">
        </div>
        <table cellspacing="6" cellpadding="0" width="100%" border="0">
            <tbody>
                <tr>
                    <td class="pad5 formbg border">
                        <div class="grid" style="width: 100%;">
                            <div class="rounded">
                                <div class="mid-outer">
                                    <div class="mid-inner">
                                        <div class="mid">
                                            <label>
                                                Patient Name:
                                                <asp:Label ID="lblname" runat="server"></asp:Label>
                                            </label>
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <label>
                                                Sex:
                                                <asp:Label ID="lblsex" runat="server"></asp:Label>
                                            </label>
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <label>
                                                DOB:
                                                <asp:Label ID="lbldob" runat="server"></asp:Label>
                                            </label>
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <label>
                                                IQCare Reference Number:
                                                <asp:Label ID="lblIQno" runat="server"></asp:Label>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
        <h2 class="forms" align="left">
            Pay Bill</h2>
        <table cellspacing="6" cellpadding="0" width="100%" border="0">
            <tbody>
                <asp:Panel ID="divError" runat="server" Style="padding: 5px" CssClass="background-color: #FFFFC0; border: solid 1px #C00000"
                    HorizontalAlign="Left" Visible="false">
                    <asp:Label ID="lblError" runat="server" Style="font-weight: bold; color: #800000"
                        Text=""></asp:Label>
                </asp:Panel>
                <tr>
                    <td class="form" style="width: 60%" valign="top">
                        <asp:UpdatePanel ID="upItemsList" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <div id="div-payItems" class="GridView whitebg">
                                    <asp:GridView ID="grdPayItems" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                        BorderColor="White" BorderWidth="1px" CellPadding="0" CssClass="table table-bordered table-hover" GridLines="None"
                                        ShowHeaderWhenEmpty="True" Width="100%" DataKeyNames="billitemid,patientid,billitemdate,amount"
                                        CellSpacing="2">
                                        <Columns>
                                            <asp:TemplateField Visible="True">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkBxHeader" runat="server" AutoPostBack="true" OnCheckedChanged="chkBxHeader_CheckedChanged" />
                                                </HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="30px" Wrap="False" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="30px" Wrap="False" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkBxItem" runat="server" AutoPostBack="True" OnCheckedChanged="chkBxItem_CheckedChanged">
                                                    </asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPayItemDesc" runat="server" Text='<%# Bind("ItemName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="60%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quantity">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPayItemQuantity" runat="server" Text='<%# Bind("Quantity") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Price">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPayItemPrice" runat="server" Text='<%# Bind("sellingPrice") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPayItemAmount" runat="server" Text='<%# Bind("Amount") %>' Width="99%"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle Width="15%" HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle HorizontalAlign="Left" />                                       
                                    </asp:GridView>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="form" valign="top">
                        <asp:UpdatePanel ID="upPayPanel" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <table width="100%" ID=tblCompute runat="server" >
                                    <tr>
                                        <td align="right" style="padding-left: 10px; font-weight: bold">
                                            Payment Mode:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlPaymentMode" runat="server" Width="180px">
                                            </asp:DropDownList>
                                        </td>
                                        <td rowspan="3" align="center" colspan="2" width="25%">
                                            <asp:Button ID="buttonCompute" runat="server" Text="Compute Change" Height="70px"
                                                Style="white-space: nowrap" Font-Bold="True" ForeColor="InfoText" OnClick="buttonCompute_Click"
                                                Width="100%" Enabled="True" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="padding-left: 10px; font-weight: bold">
                                            Ref No:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="textReferenceNo" runat="server" Width="180px" AutoComplete="Off"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                TargetControlID="textReferenceNo" FilterType="Custom, Numbers, LowercaseLetters, UppercaseLetters"
                                                ValidChars=".-" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="padding-left: 10px; font-weight: bold; white-space: nowrap">
                                            Amount to pay:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="textAmountToPay" runat="server" Width="180px" AutoComplete="Off"
                                                ReadOnly="true"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="fteAmountToPay" runat="server" TargetControlID="textAmountToPay"
                                                FilterType="Numbers, Custom" ValidChars="." />
                                            <asp:RangeValidator ID="rgAmountToPay" runat="server" ControlToValidate="textAmountToPay"
                                                Type="Double" MinimumValue="0" MaximumValue="10" ErrorMessage="The value should be between 0 and  the outstanding amount"
                                                Display="Dynamic" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="padding-left: 10px; font-weight: bold; white-space: nowrap">
                                            Tendered Amount:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="textTenderedAmount" runat="server" Width="180px" AutoComplete="Off"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="fteTenderedAmount" runat="server" TargetControlID="textTenderedAmount"
                                                FilterType="Numbers" />
                                                 <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                                ControlToCompare="textAmountToPay" ControlToValidate="textTenderedAmount" 
                                                Display="Dynamic" 
                                                ErrorMessage="Value shuold be greater than or equal to Amount to pay" 
                                                Operator="GreaterThanEqual" Type="Double"></asp:CompareValidator>
                                        </td>
                                    </tr>
                                   
                                    <tr>
                                        <td colspan="4">
                                            <hr style="border-width: 1px" />
                                        </td>
                                    </tr>
                                     </table>
                                           <table width="100%" runat="server" id="tblFinish">
                                    <tr>
                                        <td colspan="3" align="right">
                                            <label style="padding-left: 10px" id="label1">
                                                Total Bill:</label>
                                            <label style="padding-left: 10px" id="lblTotalBill" runat="server">
                                                0:</label>
                                        </td>
                                        <td align="center" rowspan="4" width="20%">
                                            <asp:Button ID="btnFinish" MaxLength="20" runat="server" Text="Finish" OnClick="btnFinish_Click"
                                                Height="70px" Width="100%" Enabled="false" />
                                        </td>
                                    </tr>
                                       <tr>
                                        <td colspan="3" align="right">
                                            <span style="padding-left: 10px; font-weight: bold">Amount to pay:</span>
                                            <label style="padding-left: 10px" id="labelAmountTopay" runat="server">
                                                none</label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" align="right">
                                            <span style="padding-left: 10px; font-weight: bold">Payment Type:</span>
                                            <label style="padding-left: 10px" id="labelPaymentType" runat="server">
                                                none</label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" align="right">
                                            <label style="padding-left: 10px" id="label2">
                                                Amount Tendered:</label>
                                            <label style="padding-left: 10px" id="lblPaid" runat="server">
                                                0:</label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" align="right">
                                            <label style="padding-left: 10px" id="lblMoneyType" runat="server">
                                                Change:</label>
                                            <label style="padding-left: 10px" id="lblChange" runat="server">
                                                0:</label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" align="right">
                                            <span style="padding-left: 10px; font-weight: bold">Amount Due:</span>
                                            <label style="padding-left: 10px" id="labelAmountDue" runat="server">
                                                0:</label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" align="center" style="height: 30px">
                                        </td>
                                        <td align="center" width="20%" style="height: 30px">
                                            <asp:Button ID="btnCancel" MaxLength="20" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                                Width="100%" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <hr style="border-width: 1px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" align="center">
                                            <asp:CheckBox ID="ckbPrintReciept" MaxLength="20" runat="server" Text="Print Reciept"
                                                Checked="True" Visible="True" />
                                        </td>
                                    </tr>
                                </table>
                                  
                                <asp:Button ID="btn" runat="server" Style="display: none" /><asp:Panel ID="pnNotify"
                                    runat="server" Style="display: none; width: 460px; border: solid 1px #808080;
                                    background-color: #E0E0E0; z-index: 15000">
                                    <asp:Panel ID="pnPopup_Title" runat="server" Style="border: solid 1px #808080; margin: 0px 0px 0px 0px;
                                        cursor: move; height: 18px">
                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 18px">
                                            <tr>
                                                <td style="width: 5px; height: 19px;">
                                                </td>
                                                <td style="width: 100%; height: 19px;">
                                                    <span style="font-weight: bold; color: Black">
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
                                    PopupControlID="pnNotify" BackgroundCssClass="modalBackground" DropShadow="True"
                                    PopupDragHandleControlID="pnPopup_Title" Enabled="True" DynamicServicePath="">
                                </ajaxToolkit:ModalPopupExtender>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnFinish" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </tbody>
        </table>
        <table cellspacing="6" cellpadding="0" width="100%" border="0">
            <tbody>
                <tr>
                    <td class="pad5 formbg border">
                        <asp:HiddenField ID="HDBillAmount" runat="server" />
                        <asp:HiddenField ID="HDAmountDue" runat="server" />
                        <asp:UpdatePanel ID="upTransactions" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <div id="paidBills" class="grid" style="width: 100%;">
                                    <div class="rounded">
                                        <div class="mid-outer">
                                            <div class="mid-inner">
                                                <div class="mid" style="height: 180px; overflow: auto">
                                                    <div id="div-receiptgridview" class="GridView whitebg">
                                                        <asp:GridView ID="gridBillTransaction" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                            BorderColor="White" BorderWidth="1px" CellPadding="0" CssClass="table table-bordered table-hover" DataKeyNames="BillID,TransactionID,PatientID"
                                                            Enabled="true" EnableModelValidation="True" GridLines="None" HorizontalAlign="Left"
                                                            ShowFooter="True" ShowHeaderWhenEmpty="True" Width="100%" OnRowCommand="gridBillTransaction_RowCommand"
                                                            OnRowDataBound="gridBillTransaction_RowDataBound">
                                                            <Columns>
                                                                <asp:BoundField DataField="ReceiptNumber" HeaderText="Reference #" />
                                                                <asp:BoundField DataField="TransactionDate" DataFormatString="{0:dd-MMM-yyyy hh:mm:ss}"
                                                                    HeaderText="Transaction Date" />
                                                                <asp:BoundField DataField="TotalAmount" HeaderText="Amount" />
                                                                <asp:BoundField DataField="TransactionTypeName" HeaderText="Transaction Type" />
                                                                <asp:TemplateField HeaderText="Transaction Status">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="labelTransactionStatus" runat="server" Text='<%# Bind("TransactionStatus") %>'></asp:Label></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="CreatedBy" HeaderText="Transacted By" />
                                                                <asp:TemplateField InsertVisible="False" ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="receiptPrint" runat="server" CausesValidation="false" CommandName="PrintReceipt"
                                                                            Text="Print Receipt" CommandArgument="<%# Container.DataItemIndex %>"></asp:LinkButton></ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle ForeColor="#3399FF" HorizontalAlign="Left" />                                                           
                                                        </asp:GridView>
                                                    </div>
                                                    <asp:Button ID="btnRaiseReversal" runat="server" Style="display: none" />
                                            <ajaxToolkit:ModalPopupExtender ID="mpeReverse" runat="server" PopupControlID="panelReversalPopup"
                                                TargetControlID="btnRaiseReversal" CancelControlID="buttonCancelReversal" BackgroundCssClass="modalBackground">
                                            </ajaxToolkit:ModalPopupExtender>
                                            <asp:Panel ID="panelReversalPopup" runat="server" Style="display: none; background-color: #FFFFFF;
                                                width: 300px; border: 3px solid #0DA9D0;">
                                                <div style="background-color: #2FBDF1; height: 30px; color: White; line-height: 30px;
                                                    text-align: center; font-weight: bold;">
                                                    Request For Reversal
                                                    <asp:Label ID="labelReceipt" runat="server" /></div>
                                                <div style="min-height: 50px; line-height: 30px; text-align: center; font-weight: bold;">
                                                    Reason for reversal?<br />
                                                    <asp:TextBox runat="server" ID="textReason" Width="286px" TextMode="MultiLine" /><asp:HiddenField
                                                        ID="HTransactionID" runat="server" />
                                                </div>
                                                <div style="padding: 3px;" align="right">
                                                    <asp:Button ID="buttonRequestReversal" runat="server" Text="Send Request" ForeColor="DarkGreen"
                                                        OnClick="RequestReversal" />
                                                    <asp:Button ID="buttonCancelReversal" runat="server" Text="Cancel" ForeColor="DarkBlue" />
                                                </div>
                                            </asp:Panel>
                                            <asp:Button ID="btnDisplay" runat="server" Style="display: none" /><asp:Panel ID="panelReversalNotify"
                                                runat="server" Style="display: none; width: 460px; border: solid 1px #808080;
                                                background-color: #E0E0E0; z-index: 15000">
                                                <asp:Panel ID="panelReversalTitle" runat="server" Style="border: solid 1px #808080;
                                                    margin: 0px 0px 0px 0px; cursor: move; height: 18px">
                                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 18px">
                                                        <tr>
                                                            <td style="width: 5px; height: 19px;">
                                                            </td>
                                                            <td style="width: 100%; height: 19px;">
                                                                <span style="font-weight: bold; color: Black">
                                                                    <asp:Label ID="labelReverseTitle" runat="server">Reverse Bill</asp:Label></span>
                                                            </td>
                                                            <td style="width: 5px; height: 19px;">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <table border="0" cellpadding="15" cellspacing="0" style="width: 100%;">
                                                    <tr>
                                                        <td style="width: 48px" valign="middle" align="center">
                                                            <asp:Image ID="imageReversalNotify" runat="server" ImageUrl="~/images/mb_information.gif"
                                                                Height="32px" Width="32px" />
                                                        </td>
                                                        <td style="width: 100%;" valign="middle" align="center">
                                                            <asp:Label ID="labelReversalInfo" runat="server" Font-Bold="True"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <div style="background-color: #FFFFFF; border-top: solid 1px #808080; width: 100%;
                                                    text-align: center; padding-top: 5px; padding-bottom: 5px">
                                                    <asp:Button ID="buttonReversalOK" runat="server" Text="OK" Width="80px" Style="border: solid 1px #808080;"
                                                        OnClick="buttonReversalOK_Click" /></div>
                                            </asp:Panel>
                                            <ajaxToolkit:ModalPopupExtender ID="mpeReversalNotify" runat="server" TargetControlID="btnDisplay"
                                                PopupControlID="panelReversalNotify" BackgroundCssClass="modalBackground" DropShadow="True"
                                                PopupDragHandleControlID="panelReversalTitle" Enabled="True" DynamicServicePath="">
                                            </ajaxToolkit:ModalPopupExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="bottom-outer">
                                            <div class="bottom-inner">
                                                <div class="bottom">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="form pad5 center">
                        <br />
                        <asp:Button ID="buttonClose" runat="server" OnClick="btn_close_Click" Text="Close" />
                    </td>
                </tr>
            </tbody>
        </table>
        <asp:UpdateProgress ID="sProgress" runat="server" DisplayAfter="5">
            <ProgressTemplate>
                <div style="width: 100%; height: 100%; position: fixed; top: 0px; left: 0px; vertical-align: middle;">
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
    </div>
</asp:Content>
