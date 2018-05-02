<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    CodeBehind="frmBilling_ClientBill.aspx.cs" Inherits="IQCare.Web.Billing.frmBilling_ClientBill"
    MaintainScrollPositionOnPostback="true" %>
<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="act" Namespace="AjaxControlToolkit" %>
<%@ Register Src="TransactionReversal.ascx" TagName="TransactionReversal" TagPrefix="uc1" %>
<%@ Register Src="PatientDeposits.ascx" TagName="PatientDeposit" TagPrefix="pd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <script language="javascript" type="text/javascript">

        function WindowPrint() {
            window.print();
        }
        function WindowPrintAll() {
            window.print();
        }
        function PrintGrid(strid) {

            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'letf=0,top=0,width=400,height=400,toolbar=0,scrollbars=0,status=0');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        }


        function openReportPage(path) {
            window.open(path, 'popupwindow', 'toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=yes,resizable=no,width=950,height=650,scrollbars=yes');
        }
        function openReceiptPage(path) {
            window.open(path, 'ReceiptPage', 'toolbars=no,location=no,directories=no,dependent=yes,top=100,left=30,maximize=no,resize=no,width=1000,height=800,scrollbars=yes');
        }
        function ace1_itemSelected(sender, e) {
            var hdCustID = $get('<%= hdCustID.ClientID %>');
            hdCustID.value = e.get_value();
            //alert(hdCustID.value);
        }

        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
        function CheckAll(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById('<%=grdUnBilledItems.ClientID%>');
            for (i = 1; i < GridVwHeaderChckbox.rows.length - 1; i++) {
                if (!(GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("input")[0]).disabled) {
                    GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("input")[0].checked = Checkbox.checked;
                }
            }


        }
        function OnChecked(Checkbox) {

            if (!Checkbox.checked) {
                var grdUnBilledItems = document.getElementById('<%=grdUnBilledItems.ClientID%>');
                grdUnBilledItems.rows[0].cells[0].getElementsByTagName("input")[0].checked = false;
            }

        }

        function resetPosition(object, args) {
            var tb = object._element;
            var tbposition = findPositionWithScrolling(tb);
            var xposition = tbposition[0] - 75;
            var yposition = tbposition[1] + 10; // 22 textbox height 
            var ex = object._completionListElement;
            if (ex)
                $common.setLocation(ex, new Sys.UI.Point(xposition, yposition));
        }
        function findPositionWithScrolling(oElement) {
            if (typeof (oElement.offsetParent) != 'undefined') {
                var originalElement = oElement;
                for (var posX = 0, posY = 0; oElement; oElement = oElement.offsetParent) {
                    posX += oElement.offsetLeft;
                    posY += oElement.offsetTop;
                    if (oElement != originalElement && oElement != document.body && oElement != document.documentElement) {
                        posX -= oElement.scrollLeft;
                        posY -= oElement.scrollTop;
                    }
                }
                return [posX, posY];
            } else {
                return [oElement.x, oElement.y];
            }
        }
    </script>
    <style>
        .AutoExtender
        {
            font-family: Courier New, Arial, sans-serif;
            font-size: 11px;
            font-weight: 100;
            border: solid 1px #006699;
            line-height: 15px;
            padding: 0px;
            background-color: White;
            margin-left: 0px;
            width: 800px;
        }
        .AutoExtenderList
        {
            cursor: pointer;
            color: black;
            z-index: 2147483647 !important;
        }
        .AutoExtenderHighlight
        {
            color: White;
            background-color: #006699;
            cursor: pointer;
        }
        #divwidth
        {
            width: 800px !important;
        }
        #divwidth div
        {
            width: 800px !important;
        }
        #divwidthFooter
        {
            width: 800px !important;
        }
        #divwidthFooter div
        {
            width: 800px !important;
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
      <div class="box box-primary box-solid" style="z-index: 1;">
       <div class="box-header">
        <h3 class="box-title">Patient Bill</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
	<div class="row" align="center">
         <br />           
        </div>
        <div class="row">
 <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
             <label class="control-label">Patient Name:</label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
             <asp:Label ID="lblname" runat="server"></asp:Label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
             <label class="control-label">Sex:</label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
            <asp:Label ID="lblsex" runat="server"></asp:Label>
             </div> 
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
             <label  class="control-label">DOB:</label>
             </div> 
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
              <asp:Label ID="lbldob" runat="server"></asp:Label>
             </div> 
              <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
             <label  class="control-label">Facility ID:</label>
             </div> 
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
             <asp:Label ID="lblFacilityID" runat="server"></asp:Label>
             </div> 
              <div class="col-md-2 col-sm-12 col-xs-12 form-group text-nowrap">
             <label  class="control-label">IQCare Reference Number:</label>
             </div>
              <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
              <asp:Label ID="lblIQno" runat="server"></asp:Label>
              </div>
             
 </div>
 <div class="row">
 <div class="col-md-12">
  <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <act:TabContainer ID="TabContainer1" runat="server" Width="100%" ActiveTabIndex="0"
                    OnDemand="true" AutoPostBack="true" OnActiveTabChanged="TabContainer1_ActiveTabChanged" CssClass="ajax__myTab">
                    <act:TabPanel ID="tabCurrentBill" runat="server" HeaderText="Unbilled Items">
                        <ContentTemplate>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <br />
 <div id="divBills" class="grid" style="width: 100%;">
                                                      
                                                            <div class="mid-outer">
                                                                <div class="mid-inner">
                                                                    <div class="mid" style="height: 280px; overflow: auto">
                                                                        <div id="div-gridview" class="GridView whitebg">
                                                                            <asp:DropDownList ID="ddlCostCenter" runat="server" Style="z-index: 2" AutoPostBack="false"
                                                                                Visible="false">
                                                                            </asp:DropDownList>
                                                                            <asp:GridView ID="grdUnBilledItems" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                                BorderColor="White" BorderWidth="1px" CellPadding="0" CssClass="table table-bordered table-hover" DataKeyNames="billItemID,PaymentType,ItemType,ItemID"
                                                                                GridLines="None" OnRowCancelingEdit="grdUnBilledItems_RowCancelingEdit" OnRowCommand="grdUnBilledItems_RowCommand"
                                                                                OnRowDataBound="grdUnBilledItems_RowDataBound" OnRowDeleting="grdUnBilledItems_RowDeleting"
                                                                                OnRowEditing="grdUnBilledItems_RowEditing" OnRowUpdating="grdUnBilledItems_RowUpdating"
                                                                                PageIndex="1" ShowFooter="True" ShowHeaderWhenEmpty="True" Width="100%">
                                                                                <Columns>
                                                                                    <asp:TemplateField>
                                                                                        <HeaderTemplate>
                                                                                            <asp:CheckBox ID="chkBxHeader" runat="server" AutoPostBack="false" onclick="CheckAll(this);" /></HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkBxItem" runat="server" AutoPostBack="false" onclick="OnChecked(this);">
                                                                                            </asp:CheckBox></ItemTemplate>
                                                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="5px" Wrap="False" />
                                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="5px" Wrap="False" />
                                                                                        <ItemStyle Width="5%" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Left">
                                                                                        <EditItemTemplate>
                                                                                            <asp:Label ID="lblEditDate" runat="server" Font-Bold="True" Text='<%# Bind("BillItemDate","{0:dd-MMM-yyyy}") %>'
                                                                                                Width="99%"></asp:Label></EditItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="lblNewDate" runat="server" Width="99%"></asp:Label></FooterTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("BillItemDate","{0:dd-MMM-yyyy}") %>'></asp:Label></ItemTemplate>
                                                                                        <ItemStyle Width="10%" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Description" HeaderStyle-HorizontalAlign="Left">
                                                                                        <EditItemTemplate>
                                                                                            <asp:TextBox ID="txtEditDescription" runat="server" AutoPostBack="true" OnTextChanged="txtEditDescription_textChanged"
                                                                                                Text='<%# Bind("ItemName") %>' Width="99%" CssClass="form-control" Font-Names="Courier New"></asp:TextBox>
                                                                                            <asp:Panel ID="divwidth" runat="server" ScrollBars="Vertical" Height="150px" />
                                                                                            <%--<div id="divwidth" runat="server" style="height:150px; overflow:scroll;">--%>
                                                                                            </div>
                                                                                            <act:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" CompletionInterval="30"
                                                                                                CompletionListCssClass="AutoExtender" CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                                                                                CompletionListItemCssClass="AutoExtenderList" CompletionSetCount="10" BehaviorID="AutoCompleteEx"
                                                                                                EnableCaching="false" FirstRowSelected="false" MinimumPrefixLength="2" OnClientItemSelected="ace1_itemSelected"
                                                                                                ServiceMethod="SearchItems" TargetControlID="txtEditDescription" CompletionListElementID="divwidth">
                                                                                            </act:AutoCompleteExtender>
                                                                                        </EditItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:TextBox ID="txtNewDescription" runat="server" AutoPostBack="true" OnTextChanged="txtNewDescription_textChanged"
                                                                                                Width="99%" CssClass="form-control" Font-Names="Courier New"></asp:TextBox>
                                                                                            <asp:Panel ID="divwidthfooter" runat="server" ScrollBars="Vertical" Height="150px" />
                                                                                            <%--<div id="divwidthfooter" runat="server" />--%>
                                                                                            <act:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionInterval="30"
                                                                                                CompletionListCssClass="AutoExtender" CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                                                                                CompletionListItemCssClass="AutoExtenderList" CompletionSetCount="10" BehaviorID="AutoCompleteExFooter"
                                                                                                EnableCaching="false" FirstRowSelected="false" MinimumPrefixLength="2" OnClientItemSelected="ace1_itemSelected"
                                                                                                ServiceMethod="SearchItems" TargetControlID="txtNewDescription" CompletionListElementID="divwidthfooter">
                                                                                            </act:AutoCompleteExtender>
                                                                                        </FooterTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("ItemName") %>'></asp:Label></ItemTemplate>
                                                                                        <ItemStyle Width="35%" />
                                                                                    </asp:TemplateField>
                                                                                    <%--<asp:BoundField HeaderText="Item Type" DataField="ItemTypeName" Visible ="false" /><asp:TemplateField HeaderText="itemId" Visible="False">
                                                                                <EditItemTemplate>
                                                                                    <asp:TextBox ID="txtEdititemId" runat="server" Text='<%# Bind("itemId") %>' Width="99%"></asp:TextBox></EditItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:TextBox ID="txtNewitemId" runat="server" Width="99%"></asp:TextBox></FooterTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblitemId" runat="server" Text='<%# Bind("itemId") %>'></asp:Label></ItemTemplate>
                                                                                <ItemStyle Width="2px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="ItemType" Visible="False">
                                                                                <EditItemTemplate>
                                                                                    <asp:TextBox ID="txtEditItemType" runat="server" Text='<%# Bind("itemType") %>' Width="99%"></asp:TextBox></EditItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:TextBox ID="txtNewItemType" runat="server" Width="99%"></asp:TextBox></FooterTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblItemType" runat="server" Text='<%# Bind("itemType") %>'></asp:Label></ItemTemplate>
                                                                                <ItemStyle Width="2px" />
                                                                            </asp:TemplateField>--%>
                                                                                    <asp:TemplateField HeaderText="Department" Visible="True" HeaderStyle-HorizontalAlign="Left">
                                                                                        <EditItemTemplate>
                                                                                            <div style="white-space: nowrap">
                                                                                                <%-- <span style='display: <%# ShowEdit(Eval("ItemTypeName")) %>; white-space: nowrap'>--%>
                                                                                                <asp:DropDownList ID="ddlItemCostCenter" runat="server" Visible="false" Width="99%" CssClass="form-control">
                                                                                                </asp:DropDownList>
                                                                                                <%--</span><span style='display: <%# HideEdit(Eval("ItemTypeName")) %>; white-space: nowrap'>--%>
                                                                                                <asp:Label ID="lblCostCenter" runat="server" Text='<%# Bind("CostCenterName") %>'
                                                                                                    Visible="false"></asp:Label><%--</span>--%>
                                                                                            </div>
                                                                                        </EditItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <div style="white-space: nowrap">
                                                                                                <%--  <span style='display: <%# ShowEdit(Eval("ItemTypeName")) %>; white-space: nowrap'>--%>
                                                                                                <asp:DropDownList ID="ddlItemCostCenter" runat="server" Width="99%" Visible="false" CssClass="form-control">
                                                                                                </asp:DropDownList>
                                                                                                <%--  </span><span style='display: <%# HideEdit(Eval("ItemTypeName")) %>; white-space: nowrap'>--%>
                                                                                                <asp:Label ID="lblCostCenter" runat="server" Text='<%# Bind("CostCenterName") %>'
                                                                                                    Visible="false"></asp:Label>
                                                                                                <%--  </span>--%>
                                                                                            </div>
                                                                                        </FooterTemplate>
                                                                                        <ItemTemplate>
                                                                                            <div style="white-space: nowrap">
                                                                                                <asp:Label ID="lblCostCenter" runat="server" Text='<%# Bind("CostCenterName") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="10%" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Qty" HeaderStyle-HorizontalAlign="Left">
                                                                                        <EditItemTemplate>
                                                                                            <asp:TextBox ID="txtEditQuantity" runat="server" onkeypress="return isNumber(event)"
                                                                                                Text='<%# Bind("Quantity") %>' Width="99%" CssClass="form-control" Wrap="False" MaxLength="6"></asp:TextBox></EditItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:TextBox ID="txtNewQuantity" runat="server" onkeypress="return isNumber(event)"
                                                                                               Width="99%" CssClass="form-control" Text="1" MaxLength="6"></asp:TextBox></FooterTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("Quantity") %>'></asp:Label></ItemTemplate>
                                                                                        <ItemStyle Width="5%" />
                                                                                    </asp:TemplateField>
                                                                                    <%-- <asp:TemplateField HeaderText="Payment Mode" Visible="False">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("PaymentName") %>'></asp:Label></ItemTemplate>
                                                                                <ItemStyle Width="15%" />
                                                                            </asp:TemplateField>--%>
                                                                                    <asp:TemplateField HeaderText="Price" HeaderStyle-HorizontalAlign="Left">
                                                                                        <EditItemTemplate>
                                                                                            <asp:Label ID="lblEditUnitPrice" runat="server" Text='<%# Bind("sellingPrice") %>'></asp:Label></EditItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="lblNewUnitPrice" runat="server" Width="90%">0</asp:Label></FooterTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("sellingPrice") %>' Width="99%"></asp:Label></ItemTemplate>
                                                                                        <ItemStyle Width="8%" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Left">
                                                                                        <EditItemTemplate>
                                                                                            <asp:Label ID="lblEditAmount" runat="server" Width="99%"></asp:Label></EditItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="lblNewAmountPrice" runat="server" Width="90%"></asp:Label></FooterTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label7" runat="server" Text='<%# Bind("Amount") %>' Width="99%"></asp:Label></ItemTemplate>
                                                                                        <ItemStyle Width="10%" />
                                                                                    </asp:TemplateField>
                                                                                    <%--<asp:TemplateField HeaderText="Pay Bill Binded" Visible="False">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPayBill" runat="server" Text='<%# Bind("PayItem") %>' Width="99%"></asp:Label></ItemTemplate>
                                                                                <ItemStyle Width="5%" />
                                                                            </asp:TemplateField>--%>
                                                                                    <asp:TemplateField ShowHeader="False">
                                                                                        <EditItemTemplate>
                                                                                            <asp:LinkButton ID="buttonUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                                                                                Text="Update"></asp:LinkButton>&#160;<asp:LinkButton ID="buttonCancelEdit" runat="server"
                                                                                                    CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton></EditItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:LinkButton ID="btnNewAdd" runat="server" CommandName="AddItem">Add</asp:LinkButton></FooterTemplate>
                                                                                        <ItemTemplate>
                                                                                            <div style="white-space: nowrap">
                                                                                                <asp:LinkButton ID="buttonEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                                                                                    Text="Edit"></asp:LinkButton>&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="buttonDelete"
                                                                                                        runat="server" CausesValidation="False" CommandName="Delete" Text="Delete"></asp:LinkButton></div>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                                    </asp:TemplateField>
                                                                                    <%--<asp:TemplateField ShowHeader="False">
                                                                                <ItemTemplate>
                                                                                    </ItemTemplate>
                                                                            </asp:TemplateField>--%>
                                                                                </Columns>
                                                                               
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </div>
                                                                    <asp:HiddenField ID="hdCustID" runat="server" />
                                                                    <asp:HiddenField ID="HItemTypeID" runat="server" />
                                                                    <asp:HiddenField ID="HItemTypeName" runat="server" />
                                                                    <asp:HiddenField ID="HItemID" runat="server" />
                                                                    <asp:HiddenField ID="hConsumableItemTypeID" runat="server" Value="-1" />
                                                                    <br />                                                                
                                                                </div>
                                                            </div>
                                                           
                                                    </div>
             </div>
            
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group text-right">
 <label class="control-label"><asp:Label ID="lbl_total" runat="server" Text="Total:" Font-Bold="True"></asp:Label></label>
             </div>
            
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Button ID="btn_saveBill" runat="server" OnClick="btn_saveBill_Click" Text="Save"
                                                        CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                                                    <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; margin-right: 2%;
                                                        vertical-align: sub; color: #fff;">
                                                    </label>
                                                    <asp:Button ID="buttonGenerateBill" runat="server" Text="Generate Bill" OnClick="GenerateBill_Click"
                                                        OnPreRender="buttonGenerateBill_PreRender" CssClass="btn btn-primary" Height="30px"
                                                        Width="14%" /><label class="glyphicon glyphicon-list" style="margin-left: -3%;
                                                            margin-right: 2%; vertical-align: sub; color: #fff;">
                                                        </label>
                                                    <asp:Button ID="btn_print1" runat="server" OnClick="btn_print_Click" OnClientClick="PrintGrid('div-gridview')"
                                                        Text="Print" Style="margin-left: 5px;" CssClass="btn btn-primary" Height="30px"
                                                        Width="9%" />
                                                    <label class="glyphicon glyphicon-print" style="margin-left: -3%; margin-right: 2%;
                                                        vertical-align: sub; color: #fff;">
                                                    </label>
             </div>
            
 </div>
                                    
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="TabContainer1" EventName="ActiveTabChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="btnOkAction" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </ContentTemplate>
                    </act:TabPanel>
                    <act:TabPanel ID="tabPendingBill" runat="server" HeaderText="Open Bills">
                        <ContentTemplate>
                            <asp:UpdatePanel ID="upPendingBills" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:HiddenField ID="pendingBillsOpenITem" runat="server" />
                                                    <div id="divPendingBills" class="grid" style="width: 100%;">
                                                      
                                                            <div class="mid-outer">
                                                                <div class="mid-inner">
                                                                    <div class="mid" style="height: 280px; overflow: auto">
                                                                        <div id="div-pendingBillsGridview" class="GridView whitebg">
                                                                            <asp:GridView ID="gridPendingBills" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                                BorderColor="White" BorderWidth="1px" CellPadding="0" GridLines="None" CssClass="table table-bordered table-hover"
                                                                                ShowHeaderWhenEmpty="True" Width="100%" DataKeyNames="billID,PatientID,HasTransaction"
                                                                                BorderStyle="Solid" OnRowCommand="gridPendingBills_RowCommand" OnRowDataBound="gridPendingBills_RowDataBound"
                                                                                OnRowCreated="gridPendingBills_RowCreated">
                                                                                <Columns>
                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="ExpandGridButton" runat="server" CommandName="Expand" ImageUrl="~/Images/plus.png"
                                                                                                CommandArgument="<%# Container.DataItemIndex %>" /></ItemTemplate>
                                                                                        <ItemStyle Width="20px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:BoundField DataField="BillNumber" HeaderText="Invoice #" />
                                                                                    <asp:BoundField DataField="BillDate" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="Invoice Date" />
                                                                                    <asp:BoundField DataField="BillAmount" HeaderText="Amount" />
                                                                                    <asp:BoundField DataField="UnpaidAmount" HeaderText="Amount Oustanding" />
                                                                                    <asp:BoundField DataField="CreatedBy" HeaderText="Invoiced By" />
                                                                                    <asp:TemplateField HeaderText="Status">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="labelBillStatus" runat="server" Text='<%# Bind("BillStatus") %>'></asp:Label></ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField InsertVisible="False" ShowHeader="False">
                                                                                        <ItemTemplate>
                                                                                            <div style="white-space: nowrap">
                                                                                                <span style='display: <%# ShowPay(Eval("BillStatus"),Eval("HasTransaction")) %>;
                                                                                                    white-space: nowrap'>
                                                                                                    <asp:Button ID="buttonPayRedirect" runat="server" CausesValidation="false" CommandName="PayBill"
                                                                                                        Text="Pay Bill" CommandArgument="<%# Container.DataItemIndex %>" Visible="false"
                                                                                                        CssClass="btn btn-primary" Height="30px" Width="25%" Style="text-align: left;" />
                                                                                                         <label class="fa fa-inr" style="margin-left: -4%; margin-right:2%; vertical-align: sub; color: #fff;"></label>
                                                                                                    <asp:HiddenField runat="server" ID="HPayMode" />
                                                                                                </span><span style='display: <%# ShowCancel(Eval("BillStatus"),Eval("HasTransaction")) %>;
                                                                                                    white-space: nowrap'>
                                                                                                    <asp:Button ID="buttonCancel" runat="server" CausesValidation="false" CommandName="CancelBill"
                                                                                                        Text="Cancel Bill" CommandArgument="<%# Container.DataItemIndex %>"  CssClass="btn btn-primary" Height="30px" Width="25%" Style="text-align: left;">
                                                                                                    </asp:Button>
                                                                                                    <label class="glyphicon glyphicon-remove-circle" style="margin-left: -4%; margin-right:2%; vertical-align: sub; color: #fff;"></label>
                                                                                                    </span>
                                                                                                <!-- Cancel Bill -->
                                                                                                <act:ConfirmButtonExtender ID="cbeBillCancel" runat="server" DisplayModalPopupID="mpeBillCancel"
                                                                                                    TargetControlID="buttonCancel"></act:ConfirmButtonExtender>
                                                                                                <act:ModalPopupExtender ID="mpeBillCancel" runat="server" PopupControlID="pnlPopup"
                                                                                                    TargetControlID="buttonCancel" OkControlID="btnYes" CancelControlID="btnNo" BackgroundCssClass="modalBackground">
                                                                                                </act:ModalPopupExtender>
                                                                                                <asp:Panel ID="pnlPopup" runat="server" Style="display: none; background-color: #FFFFFF;
                                                                                                    width: 300px; border: 3px solid #479ADA;">
                                                                                                    <div style="background-color: #479ADA; height: 30px; color: White; line-height: 30px;
                                                                                                        text-align: center; font-weight: bold;">
                                                                                                        Confirmation
                                                                                                    </div>
                                                                                                    <div style="min-height: 50px; line-height: 30px; text-align: center; font-weight: bold;">
                                                                                                        This action cannot be reversed.<br />
                                                                                                        Are you sure you want to Cancel this bill?
                                                                                                    </div>
                                                                                                    <div style="padding: 3px;" align="right">
                                                                                                        <asp:Button ID="btnYes" runat="server" Text="Yes" CssClass="btn btn-primary" Height="30px" Width="20%" style="text-align:left;"/>
                                                                                                        <label class="fa fa-check" style="margin-left: -6%; margin-right:2%; vertical-align: sub; color: #fff;"></label>
                                                                                                        <asp:Button ID="btnNo" runat="server" Text="No" CssClass="btn btn-primary" Height="30px" Width="20%" style="text-align:left;"/>
                                                                                                        <label class="fa fa-close" style="margin-left: -6%; margin-right:2%; vertical-align: sub; color: #fff;"></label>
                                                                                                        </div>
                                                                                                </asp:Panel>
                                                                                                <span style='display: <%# ShowWriteOff(Eval("BillStatus"),Eval("HasTransaction")) %>;
                                                                                                    white-space: nowrap'>
                                                                                                    <asp:Button ID="buttonWriteOff" runat="server" CausesValidation="false" CommandName="WriteOffBill"
                                                                                                        Text="Write Off Bill" CommandArgument="<%# Container.DataItemIndex %>"  CssClass="btn btn-primary" Height="30px" Width="25%" Style="text-align: left;">
                                                                                                    </asp:Button>
                                                                                                    <label class="fa fa-pencil-square-o" style="margin-left: -4%; margin-right:2%; vertical-align: sub; color: #fff;"></label>
                                                                                                    </span>
                                                                                                <!-- Write off Bill -->
                                                                                                <act:ConfirmButtonExtender ID="cbeWriteOff" runat="server" DisplayModalPopupID="mpeWriteOff"
                                                                                                    TargetControlID="buttonWriteOff"></act:ConfirmButtonExtender>
                                                                                                <act:ModalPopupExtender ID="mpeWriteOff" runat="server" PopupControlID="panelWriteOff"
                                                                                                    TargetControlID="buttonWriteOff" OkControlID="WriteOffYes" CancelControlID="WriteOffCancel"
                                                                                                    BackgroundCssClass="modalBackground">
                                                                                                </act:ModalPopupExtender>
                                                                                                <asp:Panel ID="panelWriteOff" runat="server" Style="display: none; background-color: #FFFFFF;
                                                                                                    width: 300px; border: 3px solid #479ADA;">
                                                                                                    <div style="background-color: #479ADA; height: 30px; color: White; line-height: 30px;
                                                                                                        text-align: center; font-weight: bold;">
                                                                                                        Confirmation
                                                                                                    </div>
                                                                                                    <div style="min-height: 50px; line-height: 30px; text-align: center; font-weight: bold;
                                                                                                        white-space: normal">
                                                                                                        Are you sure you want to Write Off?<br />
                                                                                                        The outstanding amount is
                                                                                                        <%# Eval("UnpaidAmount") %>
                                                                                                    </div>
                                                                                                    <div style="padding: 3px;" align="right">
                                                                                                        <asp:Button ID="WriteOffYes" runat="server" Text="Yes" CssClass="btn btn-primary" Height="30px" Width="20%" style="text-align:left;" />
                                                                                                          <label class="fa fa-check" style="margin-left: -6%; margin-right:2%; vertical-align: sub; color: #fff;"></label>
                                                                                                        <asp:Button ID="WriteOffCancel" runat="server" Text="No" CssClass="btn btn-primary" Height="30px" Width="20%" style="text-align:left;" />
                                                                                                          <label class="fa fa-close" style="margin-left: -6%; margin-right:2%; vertical-align: sub; color: #fff;"></label>
                                                                                                        </div>
                                                                                                </asp:Panel>
                                                                                                <asp:Button ID="buttonInvoice" runat="server" CausesValidation="false" CommandName="PrintInvoice"
                                                                                                    Text="Print Invoice" CommandArgument="<%# Container.DataItemIndex %>"  CssClass="btn btn-primary" Height="30px" Width="25%" Style="text-align: left;">
                                                                                                </asp:Button>
                                                                                                <label class="glyphicon glyphicon-print" style="margin-left: -4%; margin-right:2%; vertical-align: sub; color: #fff;"></label>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>
                                                                                            <asp:HiddenField ID="HdnTransaction" runat="server" Value='<%# Bind("HasTransaction") %>' />
                                                                                            </td></tr>
                                                                                            <tr>
                                                                                                <td colspan="100%">
                                                                                                    <asp:Panel ID="ContainerDiv" runat="server" Style="display: none; position: relative;
                                                                                                        left: 5px;">
                                                                                                        <asp:GridView ID="gridBillItemList" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                                                            BorderColor="White" BorderWidth="1px" CellPadding="0" CssClass="table table-bordered table-hover" DataKeyNames="billItemID,BillID,PatientID"
                                                                                                            Enabled="true" EnableModelValidation="True" GridLines="None" HorizontalAlign="Left"
                                                                                                            ShowFooter="True" ShowHeaderWhenEmpty="True" Width="100%">
                                                                                                            <Columns>
                                                                                                                <asp:BoundField DataField="BillItemDate" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="Date" />
                                                                                                                <asp:BoundField DataField="ItemName" HeaderText="Item Description" />
                                                                                                                <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                                                                                                <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                                                                                                <asp:TemplateField InsertVisible="False" ShowHeader="False" Visible="false">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Button ID="buttonRemove" runat="server" CausesValidation="false" CommandName="RemoveItem"
                                                                                                                            Text="Remove" CommandArgument="<%# Container.DataItemIndex %>" ForeColor="Black">
                                                                                                                        </asp:Button></ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                            </Columns>
                                                                                                         
                                                                                                        </asp:GridView>
                                                                                                    </asp:Panel>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                               
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            
                                                    </div>
             </div>
            
 </div>
                                    
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="TabContainer1" EventName="ActiveTabChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </ContentTemplate>
                    </act:TabPanel>
                    <act:TabPanel ID="tabPaidBills" runat="server" HeaderText="Closed Bills">
                        <ContentTemplate>
                            <asp:UpdatePanel ID="upClearedBills" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
  <asp:HiddenField ID="OpenedDivsHiddenField" runat="server" />
                                                    <div id="paidBills" class="grid" style="width: 100%;">
                                                        <div class="rounded">
                                                            <div class="mid-outer">
                                                                <div class="mid-inner">
                                                                    <div class="mid" style="height: 280px; overflow: auto">
                                                                        <div id="div-receiptgridview" class="GridView whitebg">
                                                                            <asp:GridView ID="gridClosedBills" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                                BorderColor="White" BorderWidth="1px" CellPadding="0" GridLines="None" CssClass="table table-bordered table-hover"
                                                                                ShowHeaderWhenEmpty="True" Width="100%" DataKeyNames="billID,PatientID" OnRowCommand="gridClosedBills_RowCommand"
                                                                                OnRowDataBound="gridClosedBills_RowDataBound" BorderStyle="Solid" OnRowCreated="gridClosedBills_RowCreated">
                                                                                <Columns>
                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="ExpandGridButton" runat="server" CommandName="Expand" ImageUrl="~/Images/plus.png"
                                                                                                CommandArgument="<%# Container.DataItemIndex %>" /></ItemTemplate>
                                                                                        <ItemStyle Width="20px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:BoundField DataField="BillNumber" HeaderText="Invoice #" />
                                                                                    <asp:BoundField DataField="BillDate" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="Invoice Date" />
                                                                                    <asp:BoundField DataField="BillAmount" HeaderText="Amount" />
                                                                                    <asp:BoundField DataField="SettledAmount" HeaderText="Amount Paid" />
                                                                                    <asp:BoundField DataField="CreatedBy" HeaderText="Invoiced By" />
                                                                                    <asp:TemplateField HeaderText="Status">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="labelBillStatus" runat="server" Text='<%# Bind("BillStatus") %>'></asp:Label></ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField InsertVisible="False" ShowHeader="False">
                                                                                        <ItemTemplate>
                                                                                            <asp:Button ID="invoicePrint" runat="server" CausesValidation="false" CommandName="PrintInvoice"
                                                                                                Text="Print Invoice" CommandArgument="<%# Container.DataItemIndex %>" CssClass="btn btn-primary" Height="30px" Width="60%" style="text-align:left;" />
                                                                                                 <label class="glyphicon glyphicon-print" style="margin-left: -10%; vertical-align: sub; color: #fff;"></label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>
                                                                                            </td></tr><tr>
                                                                                                <td colspan="100%">
                                                                                                    <asp:Panel ID="ContainerDiv" runat="server" Style="display: none; position: relative;
                                                                                                        left: 5px;">
                                                                                                        <asp:GridView ID="gridBillTransaction" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                                                            BorderColor="White" BorderWidth="1px" CellPadding="0" CssClass="table table-bordered table-hover" DataKeyNames="TransactionID,PatientID"
                                                                                                            Enabled="true" EnableModelValidation="True" GridLines="None" HorizontalAlign="Left"
                                                                                                            ShowFooter="True" ShowHeaderWhenEmpty="True" Width="100%" Caption="Payment Transactions">
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
                                                                                                                <asp:TemplateField InsertVisible="False" ShowHeader="False">
                                                                                                                    <ItemTemplate>
                                                                                                                        <div style='text-align: center; padding: 10px; white-space: nowrap; display: <%# IsReversible(Eval("TransactionStatus"),Eval("Reversible")) %>'>
                                                                                                                            <asp:Button ID="buttonReverse" runat="server" CausesValidation="false" CommandName="Reverse"
                                                                                                                                Text="Reverse" ToolTip="Request for reversal" CommandArgument="<%# Container.DataItemIndex %>"
                                                                                                                                CssClass="btn btn-primary" Height="30px" Width="20%" Style="text-align:left;" />
                                                                                                                                <label class="glyphicon glyphicon-print" style="margin-left: -3%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
                                                                                                                            <asp:Button ID="receiptPrint" runat="server" CausesValidation="false" CommandName="PrintReceipt"
                                                                                                                                Text="Print Receipt" CommandArgument="<%# Container.DataItemIndex %>" CssClass="btn btn-primary" Height="30px" Width="22%" Style="text-align:left;" />
                                                                                                                                <label class="glyphicon glyphicon-print" style="margin-left: -3%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
                                                                                                                        </div>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                            </Columns>                                                                                                          
                                                                                                        </asp:GridView>
                                                                                                    </asp:Panel>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>                                                                               
                                                                            </asp:GridView>
                                                                        </div>
                                                                        <asp:Button ID="btnRaiseReversal" runat="server" Style="display: none" /><act:ModalPopupExtender
                                                                            ID="mpeReverse" runat="server" PopupControlID="panelReversalPopup" TargetControlID="btnRaiseReversal"
                                                                            CancelControlID="buttonCancelReversal" BackgroundCssClass="modalBackground">
                                                                        </act:ModalPopupExtender>
                                                                        <asp:Panel ID="panelReversalPopup" runat="server" Style="display: none; background-color: #FFFFFF;
                                                                            width: 300px; border: 3px solid #479ADA;">
                                                                            <div style="background-color: #479ADA; height: 30px; color: White; line-height: 30px;
                                                                                text-align: center; font-weight: bold;">
                                                                                Request For Reversal
                                                                                <asp:Label ID="labelReceipt" runat="server" /></div>
                                                                            <div style="min-height: 50px; line-height: 30px; text-align: center; font-weight: bold;">
                                                                                Reason for reversal?<br />
                                                                                <asp:TextBox runat="server" ID="textReason" Width="286px" TextMode="MultiLine" /><asp:HiddenField
                                                                                    ID="HTransactionID" runat="server" />
                                                                            </div>
                                                                            <div style="padding: 3px;" align="right">
                                                                                <asp:Button ID="buttonRequestReversal" runat="server" Text="Send Request" ForeColor="Black"
                                                                                    OnClick="RequestReversal" /><asp:Button ID="buttonCancelReversal" runat="server"
                                                                                        Text="Cancel" ForeColor="Black" /></div>
                                                                        </asp:Panel>
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
             </div>
            
 </div>
                                  
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="TabContainer1" EventName="ActiveTabChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </ContentTemplate>
                    </act:TabPanel>
                    <act:TabPanel ID="tabReversals" runat="server" HeaderText="Reversals">
                        <ContentTemplate>
                            <asp:UpdatePanel ID="divReversalComponent" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <uc1:TransactionReversal ID="ReverseTransaction" runat="server" PrintReceiptJSMethod="openReceiptPage"
                                        PrintReceiptURL="./frmBilling_Reciept.aspx" CanRefund="True" IsApprovalMode="False" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="TabContainer1" EventName="ActiveTabChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </ContentTemplate>
                    </act:TabPanel>
                    <act:TabPanel ID="tabDeposits" runat="server" HeaderText="Cash Deposits">
                        <ContentTemplate>
                            <asp:UpdatePanel ID="divDepositComponent" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <pd:PatientDeposit ID="PDControl" runat="server" CanRefund="True" PrintReceiptJSMethod="openReceiptPage"
                                        PrintReceiptURL="./frmBilling_Reciept.aspx" />
                                    <asp:Button ID="buttonHidden" runat="server" Width="80px" Style="border: solid 1px #808080;
                                        display: none" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="TabContainer1" EventName="ActiveTabChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="buttonHidden" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="PDControl" EventName="ErrorOccurred" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </ContentTemplate>
                    </act:TabPanel>
                </act:TabContainer>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="TabContainer1" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="notificationPanel1" runat="server">
            <ContentTemplate>
                <asp:Panel ID="panelNotify" runat="server" Style="display: none; width: 460px; border: solid 0px #3c8dbc;
                    background-color: #fff; z-index: 15000; height:130px;">
                       
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">
        <asp:Panel ID="panelPopup_Title" runat="server" Style="cursor: move;">
                        <asp:Label ID="lblNotice" runat="server">Add Editing Item</asp:Label>
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
<asp:Image ID="imgNotice" runat="server" ImageUrl="~/Images/mb_information.gif" Height="32px" Width="32px" />
             </div>
             <div class="col-md-11 col-sm-12 col-xs-12 form-group text-center">
             <label class="control-label"><asp:Label ID="lblNoticeInfo" runat="server"></asp:Label></label>
             </div>
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Button ID="btnOkAction" runat="server" Text="OK" CssClass="btn btn-primary" Height="30px" Width="15%" style="text-align:left;" />
 <label class="glyphicon glyphicon-ok" style="margin-left: -5%; margin-right: 2%; vertical-align: sub; color: #fff;">
                                    </label>
             </div>
</div>
	 <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>
      </asp:Panel>
                <asp:Button ID="btn" runat="server" Style="display: none" />
                <act:ModalPopupExtender ID="notifyPopupExtender" runat="server" TargetControlID="btn"
                    PopupControlID="panelNotify" BackgroundCssClass="modalBackground" DropShadow="false"
                    PopupDragHandleControlID="panelPopup_Title" Enabled="True" DynamicServicePath="" Y="20">
                </act:ModalPopupExtender>
            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>
        <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <asp:Button ID="Button2" runat="server" OnClick="btn_close_Click" Text="Close" CssClass="btn btn-primary"
                Height="30px" Width="8%" Style="text-align: left;" />
            <label class="glyphicon glyphicon-remove-circle" style="margin-left: -3%; margin-right: 2%;
                vertical-align: sub; color: #fff;" id="lblclientbillclose" runat="server">
            </label>
             </div>
              
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
             </div>
 </div>
	 <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>
     </div>
   
</asp:Content>
