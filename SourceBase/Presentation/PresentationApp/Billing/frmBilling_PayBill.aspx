<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    CodeBehind="frmBilling_PayBill.aspx.cs" Inherits="IQCare.Web.Billing.frmBilling_PayBill" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    
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
            window.location.href = './frmBilling_ClientBill.aspx';
            window.open(path, 'popupwindow', 'toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=yes,resizable=no,width=950,height=650,scrollbars=yes');
        }
      
        function calculateSum() {
            var totalPrice = 0.0;
            var grdPayItems = document.getElementById('<%=grdPayItems.ClientID%>');
            for (i = 1; i < grdPayItems.rows.length - 1; i++) {
                if (grdPayItems.rows[i].cells[0].getElementsByTagName("input")[0].checked) {
                    totalPrice += parseFloat(grdPayItems.rows[i].cells[4].getElementsByTagName("span")[0].innerText.replace(/,/g, ''));
                }

            }
            $('#<%=lblTotal.ClientID%>').text("" + totalPrice.toFixed(2));

        }

        function CheckAll(Checkbox) {
            var grdPayItems = document.getElementById('<%=grdPayItems.ClientID%>');
            for (i = 1; i < grdPayItems.rows.length - 1; i++) {
                if (!(grdPayItems.rows[i].cells[0].getElementsByTagName("input")[0]).disabled) {
                    grdPayItems.rows[i].cells[0].getElementsByTagName("input")[0].checked = Checkbox.checked;
                    if (Checkbox.checked)
                 {
                 grdPayItems.rows[i].style.backgroundColor = "#EBEBEB";
                 }
                 else
                 {
                  grdPayItems.rows[i].style.backgroundColor = "white";
                 }
                }

            }

            calculateSum();
        }

        function OnChecked() {
            var grdPayItems = document.getElementById('<%=grdPayItems.ClientID%>');
            for (i = 1; i < grdPayItems.rows.length - 1; i++) {
                if (grdPayItems.rows[i].cells[0].getElementsByTagName("input")[0].checked) {
                    grdPayItems.rows[i].style.backgroundColor = "#EBEBEB";
                } else {
                    grdPayItems.rows[i].style.backgroundColor = "white";
                    grdPayItems.rows[0].cells[0].getElementsByTagName("input")[0].checked = false;

                }

            }
            calculateSum();

        }
    
    </script>   
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">Pay Bill</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
	<div class="row" align="center">
         <br />           
        </div>
        <div class="row">
 <div class="col-md-1 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">Patient Name:</label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <asp:Label ID="lblname" runat="server"></asp:Label>
                                                <asp:HiddenField runat="server" ID="HLocationID" />
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">Sex:</label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <asp:Label ID="lblsex" runat="server"></asp:Label>
             </div> 
              <div class="col-md-1 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">DOB:</label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
            <asp:Label ID="lbldob" runat="server"></asp:Label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">Facility ID:</label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <asp:Label ID="lblFacilityID" runat="server"></asp:Label>
             </div> 
              <div class="col-md-2 col-sm-12 col-xs-12 form-group text-right">
             <label  class="control-label">IQCare Reference Number:</label>
             </div> 
              <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            <asp:Label ID="lblIQno" runat="server"></asp:Label>
             </div> 
 </div>
    <asp:UpdatePanel runat="server" ID="upBill" UpdateMode="Conditional">
            <ContentTemplate>
            <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <label class="control-label">
              <asp:Panel ID="divError" runat="server" Style="padding: 5px" CssClass="background-color: #FFFFC0; border: solid 1px #C00000"
                            HorizontalAlign="Left" Visible="false">
                            <asp:Label ID="lblError" runat="server" Style="color: #800000" Text=""></asp:Label>
                        </asp:Panel>
             
             </label>
             </div>
            
 </div>
            <div class="row box-header">
         <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <h5 class="box-title"> Bill Number: <asp:Label ID="labelBillNumber" runat="server" Font-Bold="true" ForeColor="Black"></asp:Label></h5>
         </div>             
            </div>
            <div class="row">
 <div class="col-md-8">
 <%--left--%>
 <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:HiddenField ID="HDBillAmount" runat="server" />
                                <asp:HiddenField ID="HDAmountDue" runat="server" />
                                <asp:HiddenField ID="HPayMode" runat="server" />
                                <asp:HiddenField ID="HDBillNumber" runat="server" />
                                <asp:HiddenField ID="HTran" runat="server" />
                                <asp:HiddenField ID="HDisplay" runat="server" Value="ITEMS" />
                                <div id="div-payItems" class="GridView whitebg">
                                    <asp:GridView ID="grdPayItems" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                        BorderColor="White" BorderWidth="1px" CellPadding="0" CssClass="table table-bordered table-hover" GridLines="None"
                                        ShowHeaderWhenEmpty="True" Width="100%" DataKeyNames="billitemid,patientid,billitemdate,amount,ItemId"
                                        CellSpacing="2" ShowFooter="True" OnRowDataBound="grdPayItems_RowDataBound" OnPreRender="grdPayItems_PreRender">
                                        <Columns>
                                            <asp:TemplateField Visible="True">
                                                <HeaderTemplate>
                                                    <span style="display: <% = AllowSelect %>; white-space: nowrap">
                                                        <asp:CheckBox ID="chkBxHeader" runat="server" AutoPostBack="False" 
                                                         onclick="CheckAll(this);" />
                                                    </span>
                                                </HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="30px" Wrap="False" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="30px" Wrap="False" />
                                                <ItemTemplate>
                                                    <span style='display: <% = AllowSelect %>; white-space: nowrap'>
                                                        <asp:CheckBox ID="chkBxItem" runat="server" AutoPostBack="False"   onclick="OnChecked();"
                                                        oncheckedchanged="chkBxItem_CheckedChanged"></asp:CheckBox></span>                                                        
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Description" DataField="ItemName" ItemStyle-Width="60%"
                                                HeaderStyle-HorizontalAlign="left">
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle Width="60%"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Quantity" DataField="Quantity" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign="Center">
                                                <HeaderStyle HorizontalAlign="Center" Wrap="false"></HeaderStyle>
                                                <ItemStyle Width="8%" HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Price" DataField="SellingPrice" DataFormatString="{0:N}"
                                                ItemStyle-Width="8%" HeaderStyle-HorizontalAlign="Center">
                                                <HeaderStyle HorizontalAlign="Center" Wrap="false"></HeaderStyle>
                                                <ItemStyle Width="8%" HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:TemplateField FooterText="Total:" HeaderText="Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="labelItemCost" runat="server" Text='<%# Bind("Amount", "{0:N}") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" Wrap="false" />
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" Width="20%" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle HorizontalAlign="Left" />                                       
                                    </asp:GridView>
                                </div>
             </div>
            
 </div>
 </div>
 <div class="col-md-4">
 <%--right--%>
 <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Panel ID="panelSummarry" runat="server" Style="display: block">
 <div class="row" id="divBillAmount" style="white-space: nowrap; vertical-align: baseline">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">
             <asp:Label ID="labelAmountDueCaption" runat="server" Height="30px" Style="white-space: nowrap; vertical-align: middle; font-weight: bold">Total Amount Due:</asp:Label>
             </label>
             </div>
              <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <label class="control-label"><asp:Label ID="labelAmountDue" runat="server" Height="30px" Style="white-space: nowrap; vertical-align: middle; font-weight: bold"></asp:Label></label>
             </div>
            
 </div>
 <div class="row" style="display: <% = AllowSelect %>">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label"> <asp:Label ID="lblTotalCaption" runat="server" Height="30px" Style="white-space: nowrap; vertical-align: middle; font-weight: bold">Total Amount (selected):</asp:Label></label>
             </div>
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <label class="control-label"><asp:Label ID="lblTotal" runat="server" Height="30px" Style="white-space: nowrap; vertical-align: middle;"></asp:Label></label>
             </div>
            
 </div>
 <div class="row">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">Payment Mode:</label>
             </div>
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
 <asp:DropDownList ID="ddlPaymentMode" runat="server" Width="99%" CssClass="form-control" ViewStateMode="Enabled"> </asp:DropDownList>
             </div>
            
 </div>
 <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group text-center">
 <asp:Button ID="buttonProceed" runat="server" Text="Proceed to pay" CssClass="btn btn-primary" Height="30px" Width="35%" style="text-align:left;" OnClick="buttonProceed_Click" Enabled="True" />
 <label class="fa fa-sign-in" style="margin-left: -5%; margin-right:2%; vertical-align: sub; color: #fff;"></label>
             </div>
 </div>
                                    
                                </asp:Panel>
                                <asp:Panel ID="panelPayment" runat="server" Style="display: block">
                                    <asp:PlaceHolder ID="phPayMethod" runat="server"></asp:PlaceHolder>
                                </asp:Panel>
             </div>
            
 </div>
 </div>
 </div>
                
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="buttonProceed" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <div class="row box-header">
         <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <h5 class="box-title">Bill Payment Transactions</h5>
         </div>             
            </div>
            <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
<asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div id="paidBills" class="grid" style="width: 100%;">                                  
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
                                                                        <div style='text-align: center; padding: 10px; white-space: nowrap; display: <%# IsReversible(Eval("TransactionStatus"),Eval("Reversible")) %>'>
                                                                            <asp:Button ID="buttonReverse" runat="server" CausesValidation="false" CommandName="Reverse"
                                                                                Text="Reverse" CommandArgument="<%# Container.DataItemIndex %>" CssClass="btn btn-primary" Height="30px" Width="35%" Style="text-align:left;">
                                                                            </asp:Button>
                                                                            <label class="fa fa-recycle" style="margin-left: -6%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
                                                                            <asp:Button ID="receiptPrint" runat="server" CausesValidation="false" CommandName="PrintReceipt"
                                                                                Text="Print Receipt" CommandArgument="<%# Container.DataItemIndex %>" CssClass="btn btn-primary" Height="30px" Width="40%" Style="text-align:left;">
                                                                            </asp:Button>
                                                                            <label class="glyphicon glyphicon-print" style="margin-left: -6%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
                                                                        </div>
                                                                    </ItemTemplate>
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
                                                        width: 300px; border: 1px solid #3c8dbc;">
                                                        <div style="background-color: #3c8dbc; height: 30px; color: Black; line-height: 30px;
                                                            text-align: center; font-weight: bold; color:#fff;">
                                                            Request For Reversal
                                                            <asp:Label ID="labelReceipt" runat="server" /></div>
                                                        <div style="min-height: 50px; line-height: 30px; text-align: center; font-weight: bold;margin:5px;">
                                                            Reason for reversal?<br />
                                                            <asp:TextBox runat="server" ID="textReason" Width="286px" CssClass="form-control" TextMode="MultiLine" Style="resize:none"/>
                                                            <asp:HiddenField ID="HTransactionID" runat="server" />
                                                        </div>
                                                        <div style="padding: 3px;" align="center">
                                                            <asp:Button ID="buttonRequestReversal" runat="server" Text="Send Request" CssClass="btn btn-primary" Height="30px" Width="45%" Style="text-align:left;" OnClick="RequestReversal" />
                                                            <label class="fa fa-paper-plane" style="margin-left: -8%; vertical-align: sub; color: #fff;margin-right: 2%;""></label>
                                                            <asp:Button ID="buttonCancelReversal" runat="server" Text="Cancel" CssClass="btn btn-primary" Height="30px" Width="28%" Style="text-align:left;" />
                                                            <label class="glyphicon glyphicon-remove" style="margin-left: -7%; vertical-align: sub; color: #fff;margin-right: 2%;""></label>

                                                        </div>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </div>                                       
                                </div>
                            </ContentTemplate>
                            <Triggers>
                            </Triggers>
                        </asp:UpdatePanel>
             </div>             
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Button ID="buttonClose" runat="server" OnClick="btn_close_Click" Text="Close" CssClass="btn btn-primary" Height="30px" Width="8%" style="text-align:left;"/>
 <label class="glyphicon glyphicon-remove-circle" style="margin-left: -3%; margin-right:2%; vertical-align: sub; color: #fff;"></label>
             </div>             
 </div>
        
        <asp:UpdatePanel ID="notificationPanel" runat="server">
            <ContentTemplate>
                <asp:Button ID="btn" runat="server" Style="display: none" />
                <asp:Panel ID="pnNotify" runat="server" Style="display: none; width: 460px; border: solid 0px #3c8dbc;
                    background-color: #fff; z-index: 15000; height:130px;">
                          <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">
        <asp:Panel ID="pnPopup_Title" runat="server" Style="cursor: move;">
                        <asp:Label ID="lblNotice" runat="server">Pay Bill</asp:Label>
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
 <div class="col-md-1 col-sm-12 col-xs-12 form-group">
 <asp:Image ID="imgNotice" runat="server" ImageUrl="~/images/mb_information.gif" Height="32px" Width="32px" />
             </div>
             <div class="col-md-11 col-sm-12 col-xs-12 form-group">
             <label class="control-label"><asp:Label ID="lblNoticeInfo" runat="server" ></asp:Label></label>
             </div>
              
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
<asp:Button ID="btnOkAction" runat="server" Text="OK" CssClass="btn btn-primary" Height="30px" Width="8%" style="text-align:left;" OnClick="btnOkAction_Click" />
<label class="glyphicon glyphicon-ok" style="margin-left: -3%; margin-right:2%; vertical-align: sub; color: #fff;"></label>
             </div>
             
 </div>
	 <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>
     </div>
                </asp:Panel>
                <ajaxToolkit:ModalPopupExtender ID="notifyPopupExtender" runat="server" TargetControlID="btn"
                    PopupControlID="pnNotify" BackgroundCssClass="modalBackground" DropShadow="false"   
                    PopupDragHandleControlID="pnPopup_Title" Enabled="True" DynamicServicePath="">
                </ajaxToolkit:ModalPopupExtender>
            </ContentTemplate>
        </asp:UpdatePanel>
	 <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>
     </div>

    <div>
      
     
    </div>
</asp:Content>
