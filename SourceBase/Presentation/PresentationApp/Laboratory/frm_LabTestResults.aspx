<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    CodeBehind="frm_LabTestResults.aspx.cs" Inherits="PresentationApp.Laboratory.frm_LabTestResults" %>

<%@ Register TagPrefix="UcLabDetails" TagName="Uc1" Src="~/Laboratory/UserControl/UserControl_LabOrderDetails.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="act" Namespace="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <script type="text/javascript">
        var isTimeSelected = false;

        function DateSelected(sender, args) {

            if (!isTimeSelected)

                sender.get_timeView().setTime(8, 0, 0, 0);

            isTimeSelected = true;
        }
        function ClientTimeSelected(sender, args) {
            isTimeSelected = true;
        }
        
        function clientActiveTabChanged(sender, args) {

            //PageMethods.SaveCurrentTab(sender.get_id(), sender.get_activeTabIndex());
        }
        function Close() {
            var curl = document.URL;
            var w = window.opener;
            var rurl = '..';
            if (curl.toLowerCase().indexOf("iqcare") != -1) {
                rurl = rurl + '/IQCare';
            }

            if (w == null || w.closed) {
                window.location = rurl + "../ClinicalForms/frmPatient_Home.aspx";
            }
            else {
                window.close();
            }
            return false;
        }
        function WindowPrint() {

            window.print();
        }
        function SetActiveTab(index) {
            var tabContainer = $get('<%=tabControl.ClientID%>');
            setTimeout(function () {
            }, 2000); 
            tabContainer.control.set_activeTabIndex(index);
        }
        //DropDown list
        function SetEnableDisable(DDText, str, disableCntrl) {
            var e = document.getElementById(DDText);
            var text = e.options[e.selectedIndex].innerText;
            var YN = "";
            if (text == str) {
                $("#" + disableCntrl).prop("disabled", false);                
            }
            else {
                $("[id$='" + disableCntrl + "']").val("0");
                //$("#" + disableCntrl).attr('selectedIndex', 0);
                $("#" + disableCntrl).prop("disabled", true);
                
            }
        }

        function fnCustSpecihide(e, div) {            
            if ($(e).is(':checked') == true) {
                $(div).show();                
            }
            else {
                $(div).hide();                
            }
        }
        function fnConfirmEnable(e, disableCntrl) {
            if ($(e).is(':checked') == true) {                
                $(disableCntrl).prop("disabled", false);
            }
            else {                
                $(disableCntrl).prop("disabled", true);
            }
        }
    </script>
    <style type="text/css">
        .RadGrid .item-style td
        {
            padding-top: 0;
            padding-bottom: 0;
            height: 25px;
            vertical-align: middle;
        }
        fieldset.scheduler-border {
            border: 1px groove #eee !important;
            padding: 0 0.4em 0.4em 0.4em !important;
            margin: 0 0 0.5em 0 !important;
            -webkit-box-shadow:  0px 0px 0px 0px #000;
                    box-shadow:  0px 0px 0px 0px #000;
        }

        legend.scheduler-border {
            font-size: 1.1em !important;
            font-weight: bold !important;
            text-align: left !important;
            width:auto; /* Or auto */
            padding:0 5px; /* To give a bit of padding on the left and right */
            border-bottom:none;

        }
    </style>
    <%--<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>         
            <telerik:AjaxSetting AjaxControlID="TabTestResult">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridLabTest" />                  
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadGridLabTest">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridLabResult" />                    
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadGridLabResult">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlList" />
                    <telerik:AjaxUpdatedControl ControlID="RadGridArvMutation" />                    
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadGridArvMutation">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rcbFooterArvType" />
                    <%--<telerik:AjaxUpdatedControl ControlID="rcbFooterMutation" />  --%>                   
               <%-- </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rcbFooterArvType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rcbFooterMutation" />
                    
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rcbFooterMutation">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rcbFooterCulture" />                    
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>     
        
    </telerik:RadAjaxManager>--%>
    
    <div class="container-fluid">
        <div class="border formbg">
            <br />            
            <act:TabContainer ID="tabControl" runat="server" ActiveTabIndex="0" AutoPostBack="true"
                Width="100%" OnActiveTabChanged="tabControl_ActiveTabChanged" CssClass="ajax__myTab">
                <act:TabPanel ID="TabPnlPendLabs"  runat="server" HeaderText="Existing Pending Lab Orders"
                    TabIndex="0">
                    <ContentTemplate>
                    
                        <div class="border center formbg">
                            <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td class="form" align="center" colspan="2">
                                        <div class="treeview">
                                            <h1>
                                                <asp:TreeView ID="TreeViewExisForm" ForeColor="#000000" runat="server" Width="100%"
                                                    OnSelectedNodeChanged="TreeViewExisForm_SelectedNodeChanged">
                                                </asp:TreeView>
                                            </h1>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <br />
                        
                    </ContentTemplate>
                </act:TabPanel>
                <act:TabPanel ID="TabSpecReceipt" runat="server" HeaderText="Specimen Receipt"
                    TabIndex="1">
                    <ContentTemplate>                       
                                <div class="border center formbg pad5">
                                    <div class="border whitebg pad5">
                                        <table class="table-condensed" width="100%">
                                            <tr class="border">
                                                <td align="center" valign="middle" width="80%">
                                                    <UcLabDetails:Uc1 ID="UCSpecLabDetails" runat="server" />
                                                </td>
                                                <td align="right" valign="middle" width="20%">
                                                <asp:Button ID="btnPendingOrders" runat="server" CssClass="btn btn-primary" Font-Bold="True" Text="View Pending Orders"
                                                Style="height: 30px; width: 100%; text-align: center;" />
                                                <asp:Label ID="Label5" CssClass="glyphicon glyphicon-download" Style="margin-left: -3%;
                                                            margin-right: 2%; vertical-align: sub; color: #fff;" runat="server"></asp:Label>
                                            <act:ModalPopupExtender ID="btnPendingOrders_ModalPopupExtender" runat="server" TargetControlID="btnPendingOrders"
                                                PopupControlID="tblPendingOrders" BackgroundCssClass="modalBackground" CancelControlID="btnPendingOrdersClose">
                                            </act:ModalPopupExtender>
                                                </td>
                                            </tr>
                                        </table>
                                        <div class="border">
                                            <table class="table-condensed" width="100%">
                                                <tr>
                                                    <td style="width: 100%">
                                                        <table width="100%" border="0">
                                                            <tr>
                                                                <td style="width: 14%;" align="right">
                                                                    <label id="lblparity" class="required" runat="server">
                                                                        Specimen Type:
                                                                    </label>
                                                                </td>
                                                                <td style="width: 16%;" align="left">
                                                                    <asp:DropDownList ID="ddlspecimentype" runat="server" Width="140px">
                                                                        <asp:ListItem Text="Select Specimen Type" Value="0" Selected="True"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="width: 14%;" align="right">
                                                                    <label id="lblGravidae" class="required" runat="server">
                                                                        Source:
                                                                    </label>
                                                                </td>
                                                                <td style="width: 18%;" align="left">
                                                                    <asp:DropDownList ID="ddlSpecSource" runat="server" Width="100%">
                                                                        <asp:ListItem Text="Select Source" Value="0" Selected="True"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="width: 14%;" align="right">
                                                                    <label id="Label7" runat="server">
                                                                        Other Source:
                                                                    </label>
                                                                </td>
                                                                <td style="width: 23%;" align="left">
                                                                    <asp:TextBox ID="txtOtherSource" runat="server" Width="100%"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="100%">
                                                        <table width="100%" border="0">
                                                            <tr>
                                                                <td style="width: 17%;" align="right">
                                                                    <label id="Label1" class="required" runat="server">
                                                                        Specimen Received DateTime:
                                                                    </label>
                                                                </td>
                                                                <td style="width: 24%;" align="left">
                                                                    <telerik:RadDateTimePicker ID="txtSpecRecdt" runat="server" Skin="Office2007" Width="200px"
                                                                        Culture="en-US" >
                                                                        <ClientEvents OnDateSelected="DateSelected" />
                                                                        <TimeView StartTime="08:00:00" Interval="00:15:00" EndTime="20:15:00" Columns="7"
                                                                            OnClientTimeSelecting="ClientTimeSelected" CellSpacing="-1">
                                                                        </TimeView>
                                                                        <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                                                        <Calendar EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" UseColumnHeadersAsSelectors="False"
                                                                            UseRowHeadersAsSelectors="False">
                                                                        </Calendar>
                                                                        <DateInput DateFormat="dd-MMM-yyyy hh:mm tt" DisplayDateFormat="dd-MMM-yyyy hh:mm tt"
                                                                            LabelWidth="64px" Width="">
                                                                            <EmptyMessageStyle Resize="None" />
                                                                            <ReadOnlyStyle Resize="None" />
                                                                            <FocusedStyle Resize="None" />
                                                                            <DisabledStyle Resize="None" />
                                                                            <InvalidStyle Resize="None" />
                                                                            <HoveredStyle Resize="None" />
                                                                            <EnabledStyle Resize="None" />
                                                                        </DateInput>
                                                                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" /> 
                                                                    </telerik:RadDateTimePicker>
                                                                </td>
                                                                <td style="width: 17%;" align="right">
                                                                    <label id="Label8" class="required" runat="server">
                                                                        From:
                                                                    </label>
                                                                </td>
                                                                <td style="width: 17%;" align="left">
                                                                    <asp:DropDownList ID="ddlfromfacility" runat="server" Width="100%">
                                                                        <asp:ListItem Text="Select Facility" Value="0" Selected="True"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="100%">
                                                        <table width="100%" border="0">
                                                            <tr>
                                                                <td style="width: 16%;" align="right">
                                                                    <label id="Label3" class="required" runat="server">
                                                                        Number of specimen samples:
                                                                    </label>
                                                                </td>
                                                                <td style="width: 24%;" align="left">
                                                                    <asp:DropDownList ID="ddlspecno" runat="server" Width="75px">
                                                                        <asp:ListItem Text="Number" Value="0" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="width: 17%;" align="right">
                                                                    <label id="Label9" class="required" runat="server">
                                                                        Received by:
                                                                    </label>
                                                                </td>
                                                                <td style="width: 17%;" align="left">
                                                                    <asp:DropDownList ID="ddlrecivedby" runat="server" Width="100%">
                                                                        <asp:ListItem Text="Select Received by" Value="0" Selected="True"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table class="table-condensed" width="100%" id="Table3">
                                                <tr id="Tr3" runat="server" align="center">
                                                    <td id="Td3" runat="server">
                                                        <asp:Button ID="btnSpecSubmit" runat="server" Text="Submit" OnClick="btnSpecSubmit_Click"
                                                            CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align: left;" />
                                                        <asp:Label ID="Label19" CssClass="glyphicon glyphicon-download" Style="margin-left: -3%;
                                                            margin-right: 2%; vertical-align: sub; color: #fff;" runat="server"></asp:Label>
                                                        <asp:Button ID="btnSpecClear" runat="server" Text="Clear" OnClick="btnSpecSubmit_Click"
                                                            CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                                                        <asp:Label ID="Label20" CssClass="glyphicon glyphicon-refresh" Style="margin-left: -3%;
                                                            margin-right: 2%; vertical-align: sub; color: #fff;" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table class="table-condensed" width="100%">
                                                <tbody>
                                                    <tr id="Tr7">
                                                        <td align="left">
                                                            <telerik:RadGrid AutoGenerateColumns="False" ID="gridSpecList" runat="server" Width="100%"
                                                                CellPadding="0" Skin="Office2010Silver" Culture="(Default)" GroupPanelPosition="Top"
                                                                ResolvedRenderMode="Classic" OnItemDataBound="gridSpecList_ItemDataBound" OnDeleteCommand="gridSpecList_DeleteCommand">
                                                                <MasterTableView NoMasterRecordsText="No Records Found" DataKeyNames="ID" CellSpacing="0"
                                                                    CellPadding="0">
                                                                    <Columns>
                                                                        <telerik:GridTemplateColumn HeaderText="Specimen Type" FilterControlAltText="Filter TemplateColumn column"
                                                                            UniqueName="TemplateColumn">
                                                                            <HeaderStyle Font-Size="12px" Wrap="False" Width="10%" Font-Bold="True" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblLabTestUom" Font-Size="12px" Wrap="True" runat="server" Text='<%# Eval("SpecimenType") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn HeaderText="Source" FilterControlAltText="Filter TemplateColumn1 column"
                                                                            UniqueName="TemplateColumn1">
                                                                            <HeaderStyle Font-Size="12px" Wrap="False" Width="10%" Font-Bold="True" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblLabTestType" Font-Size="12px" Wrap="True" runat="server" Text='<%# Eval("SpecimenSource") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn HeaderText="Other Source" FilterControlAltText="Filter TemplateColumn2 column"
                                                                            UniqueName="TemplateColumn2">
                                                                            <HeaderStyle Font-Size="12px" Wrap="False" Width="20%" Font-Bold="True" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblOtherSource" runat="server" Font-Size="12px" Wrap="True" Text='<%# Eval("SpecimenOtherSource") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridDateTimeColumn DataField="SpecimenDate" DataFormatString="{0:dd-MMM-yyyy hh:mm tt}"
                                                                            UniqueName="SpecimenDate" HeaderText="Date/Time" FilterControlAltText="Filter SpecimenDate column">
                                                                            <HeaderStyle Font-Size="12px" Wrap="False" Width="20%" Font-Bold="True" />
                                                                        </telerik:GridDateTimeColumn>
                                                                        <telerik:GridTemplateColumn HeaderText="No of Sample" FilterControlAltText="Filter TemplateColumn4 column"
                                                                            UniqueName="TemplateColumn4">
                                                                            <HeaderStyle Font-Size="12px" Wrap="False" Width="5%" Font-Bold="True" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblsampleNo" runat="server" Font-Size="12px" Wrap="True" Text='<%# Eval("Specimennumbers") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn HeaderText="Specimen No" FilterControlAltText="Filter TemplateColumn5 column"
                                                                            UniqueName="TemplateColumn5">
                                                                            <HeaderStyle Font-Size="12px" Wrap="False" Width="20%" Font-Bold="True" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblspecNo" runat="server" Font-Size="12px" Wrap="True" Text='<%# Eval("SpecCustomNumber") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn HeaderText="Received by" FilterControlAltText="Filter TemplateColumn6 column"
                                                                            UniqueName="TemplateColumn6">
                                                                            <HeaderStyle Font-Size="12px" Wrap="False" Width="25%" Font-Bold="True" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblReceivedby" runat="server" Font-Size="12px" Wrap="True" Text='<%# Eval("SpecimenRecvdby") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn Visible="False" FilterControlAltText="Filter TemplateColumn3 column"
                                                                            UniqueName="TemplateColumn3">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblFlag" runat="server" Text='<%# Eval("Flag") %>'></asp:Label>
                                                                                <asp:Label ID="lblSpecID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridButtonColumn ConfirmText="Are you sure you want to delete this record?"
                                                                            ConfirmDialogType="RadWindow" ConfirmTitle="Delete" ButtonType="ImageButton"
                                                                            ImageUrl="~/Images/del.gif" CommandName="Delete" UniqueName="Delete" FilterControlAltText="Filter Delete column">
                                                                        </telerik:GridButtonColumn>
                                                                    </Columns>
                                                                </MasterTableView>
                                                            </telerik:RadGrid>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="border center formbg">
                                    <table class="table-condensed" width="100%">
                                        <tr id="Tr4" align="center">
                                            <td id="Td4" class="form">
                                                <asp:Button ID="btnSpecimenSave" runat="server" Text="Save" OnClick="btnSpecimenSave_Click"
                                                    CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                                                <asp:Label ID="lblSave" CssClass="glyphicon glyphicon-floppy-disk" Style="margin-left: -3%;
                                                    margin-right: 2%; vertical-align: sub; color: #fff;" runat="server"></asp:Label>
                                                <asp:Button ID="btncloseSpecimen" runat="server" Text="Close" OnClick="btncloseSpecimen_Click"
                                                    CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                                                <label class="glyphicon glyphicon-remove-circle" style="margin-left: -3%; margin-right: 2%;
                                                    vertical-align: sub; color: #fff;">
                                                </label>
                                                <asp:Button ID="btnSpecimenPrint" runat="server" OnClientClick="WindowPrint()"
                                                    Text="Print" CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                                                <label class="glyphicon glyphicon-print" style="margin-left: -3%; vertical-align: sub;
                                                    color: #fff;">
                                                </label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>                            
                    </ContentTemplate>
                </act:TabPanel>
                
                <act:TabPanel ID="TabTestResult" runat="server" HeaderText="Test Results"
                    TabIndex="3">
                    <ContentTemplate>
                    
                        <div class="border center formbg  pad5">
                            <div class="border whitebg pad5">
                                <table class="table-condensed">
                                    <tbody>
                                        <tr>
                                            <td align="center" valign="middle" width="100%">
                                                <UcLabDetails:Uc1 ID="UcResLabDetails" runat="server" />
                                            </td>
                                        </tr>
                                        <tr id="Tr6">
                                            <td align="left">
                                                <telerik:RadGrid AutoGenerateColumns="False" ID="RadGridLabTest" runat="server" Width="100%"
                                                    PageSize="5" ShowFooter="True" CellPadding="0" Skin="Office2010Silver" OnItemCreated="RadGridLabTest_ItemCreated"
                                                    OnNeedDataSource="RadGridLabTest_NeedDataSource" OnItemCommand="RadGridLabTest_ItemCommand"
                                                    OnDeleteCommand="RadGridLabTest_DeleteCommand" Culture="(Default)" GroupPanelPosition="Top"
                                                    ResolvedRenderMode="Classic">
                                                    <ClientSettings>
                                                        <Selecting AllowRowSelect="True"></Selecting>
                                                        <Resizing AllowColumnResize="True" EnableRealTimeResize="True"></Resizing>
                                                    </ClientSettings>
                                                    <MasterTableView NoMasterRecordsText="No Records Found" DataKeyNames="LabTestID"
                                                        CellSpacing="0" CellPadding="0">
                                                        <Columns>
                                                            <telerik:GridTemplateColumn HeaderText="Laboratory Test Name" FilterControlAltText="Filter TemplateColumn column"
                                                                UniqueName="TemplateColumn">
                                                                <HeaderStyle Font-Size="12px" Wrap="False" Width="40%" Font-Bold="True" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLabSubTestName" runat="server" Font-Size="12px" Wrap="True" Text='<%# Eval("SubTestName") %>'></asp:Label>
                                                                    <asp:Label ID="lblLabSubTestID" runat="server" Font-Size="12px" Wrap="True" Visible="false"
                                                                        Text='<%# Eval("SubTestId") %>'></asp:Label>
                                                                    <asp:Label ID="lblLabTestID" runat="server" Font-Size="12px" Wrap="True" Visible="false"
                                                                        Text='<%# Eval("LabTestId") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Units" FilterControlAltText="Filter TemplateColumn1 column"
                                                                UniqueName="TemplateColumn1">
                                                                <HeaderStyle Font-Size="12px" Wrap="False" Width="10%" Font-Bold="True" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLabTestUom" runat="server" Text='<%# Eval("UnitName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Test Type" FilterControlAltText="Filter TemplateColumn2 column"
                                                                UniqueName="TemplateColumn2">
                                                                <HeaderStyle Font-Size="12px" Wrap="False" Width="20%" Font-Bold="True" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLabTestType" Font-Size="12px" Wrap="True" runat="server" Text='<%# Eval("LabDepartmentName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                        </Columns>
                                                        <NestedViewSettings>
                                                            <ParentTableRelation>
                                                                <telerik:GridRelationFields DetailKeyField="SubTestId" MasterKeyField="SubTestId" />
                                                            </ParentTableRelation>
                                                        </NestedViewSettings>                                                        
                                                        <NestedViewTemplate>                                                        
                                                            <telerik:RadGrid AutoGenerateColumns="false" ID="RadGridLabResult" runat="server"
                                                                Width="100%" PageSize="5" AllowPaging="true" AllowMultiRowSelection="false" ClientSettings-Selecting-AllowRowSelect="false"
                                                                ClientSettings-Resizing-AllowColumnResize="false" ShowFooter="false" ClientSettings-Resizing-EnableRealTimeResize="false"
                                                                CellPadding="0" ShowHeader="false" Skin="Metro" HierarchyLoadMode="ServerOnDemand"
                                                                OnItemDataBound="RadGridResut_ItemDataBound" DataKeyNames="LabTestID" OnNeedDataSource="RadGridLabResult_NeedDataSource">
                                                                <MasterTableView Name="ChildGrid">
                                                                    <Columns>
                                                                        <telerik:GridTemplateColumn HeaderText="Lab Test Name" HeaderStyle-Font-Bold="true"
                                                                            UniqueName="SubTestId">
                                                                            <ItemTemplate>
                                                                            <fieldset class="scheduler-border">
                                                                                <legend class="scheduler-border">Specimen</legend>
                                                                            <asp:Panel runat="server" ID="pnl_spec" style="margin-top:-18px;">
                                                                                <label id="Label10"  runat="server">Select Specimen:</label>
                                                                                <asp:DropDownList ID="ddlselectspecimen" runat="server" Width="300px"></asp:DropDownList>
                                                                                <asp:CheckBox ID="chkCustSpec" runat="server" Text="Custom Specimen" TextAlign="Right" />
                                                                            </asp:Panel>
                                                                            <div id="divctx" runat="server" style="display: none;padding-top:2px;padding-bottom:2px">
                                                                                <label id="Label16" runat="server">
                                                                                    Custom Specimen No:
                                                                                </label>
                                                                                <asp:TextBox ID="txtCustSpecNo" runat="server" Width="300px" TextMode="SingleLine"></asp:TextBox>
                                                                            </div>
                                                                                <asp:Panel runat="server" ID="Panel1"  style="border-top:1px solid #eee;padding-top:5px;padding-bottom:2px" class="control-group">
                                                                                <label id="Label12"  runat="server">State:</label>
                                                                                <asp:DropDownList ID="ddlState" runat="server" Width="100px">
                                                                                 <asp:ListItem Text="Select State" Value="0" Selected="True"></asp:ListItem>
                                                                                 </asp:DropDownList>
                                                                                 &emsp;
                                                                                 <label id="Label14"  runat="server">Status:</label>
                                                                                 <asp:DropDownList ID="ddlTestStatus" runat="server" Width="100px">
                                                                                    <asp:ListItem Text="Select Status" Value="0" Selected="True"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                &emsp;
                                                                                <label id="Label15" runat="server">Reject Reason:</label>
                                                                                <asp:DropDownList ID="ddlrejreason" runat="server" Width="100px">
                                                                                    <asp:ListItem Text="Select Reason" Value="0" Selected="True"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                &emsp;
                                                                                <label id="Label13" runat="server">Other Reason:</label>
                                                                                <asp:TextBox ID="txtrejreason" runat="server" Width="100px"></asp:TextBox>
                                                                                </asp:Panel>
                                                                                </fieldset>
                                                                                <span>
                                                                                <label id="Label4" runat="server" style="padding-left: 10px;">
                                                                                   Result:
                                                                                </label>
                                                                                <telerik:RadComboBox ID="ddlList" runat="server" AutoPostBack="true" Skin="Office2010Silver"
                                                                                    EnableLoadOnDemand="true" OnSelectedIndexChanged="ddllist_SelectedIndexChanged">
                                                                                </telerik:RadComboBox>
                                                                                <telerik:RadNumericTextBox ID="txtRadValue" runat="server" Text='<%# Eval("TestResults") %>'
                                                                                    Skin="Office2010Silver">
                                                                                </telerik:RadNumericTextBox>
                                                                                <telerik:RadTextBox ID="txtAlphaRadValue" runat="server" Text='<%# Eval("TestResults1") %>'
                                                                                    Skin="Office2010Silver">
                                                                                </telerik:RadTextBox>
                                                                                <asp:RadioButtonList ID="btnRadRadiolist" runat="server" RepeatDirection="Horizontal">
                                                                                </asp:RadioButtonList>
                                                                                <asp:CheckBoxList ID="chkBoxList" runat="server" RepeatDirection="Vertical">
                                                                                </asp:CheckBoxList>
                                                                                <%-- <asp:DropDownList ID="ddlList" runat="server"></asp:DropDownList>--%>
                                                                                <asp:Label ID="lblUnitName" runat="server" Text='<%# Eval("UnitName") %>'></asp:Label>
                                                                                <asp:Label ID="lblControlType" runat="server" Text='<%# Eval("Control_type") %>'
                                                                                    Visible="false"></asp:Label>
                                                                                <asp:Label ID="lblundetectable" runat="server" Text='<%# Eval("undetectable") %>'
                                                                                    Visible="false"></asp:Label>
                                                                                <asp:Label ID="lblLabSubTestId" runat="server" Visible="false" Text='<%# Eval("SubTestID") %>'></asp:Label>
                                                                                <asp:Label ID="lblLabTestName" runat="server" Visible="false" Text='<%# Eval("SubTestName") %>'></asp:Label>
                                                                                <asp:Label ID="lblMinBoundaryVal" runat="server" Visible="false" Text='<%# Eval("MinBoundaryValue") %>'></asp:Label>
                                                                                <asp:Label ID="lblMaxBoundaryVal" runat="server" Visible="false" Text='<%# Eval("MaxBoundaryValue") %>'></asp:Label>
                                                                                <asp:Label ID="lblTestResultId" runat="server" Visible="false" Text='<%# Eval("TestResultId") %>'></asp:Label>
                                                                                <asp:Label ID="lblTestResults" runat="server" Visible="false" Text='<%# Eval("TestResults") %>'></asp:Label>
                                                                                <asp:Label ID="lblResultDate" runat="server" Visible="false" Text='<%# Eval("ResultReportDate") %>'></asp:Label>
                                                                                <asp:Label ID="lblResultby" runat="server" Visible="false" Text='<%# Eval("ResultReportBy") %>'></asp:Label>

                                                                                <asp:Label ID="lblSpecimenID" runat="server" Visible="false" Text='<%# Eval("SpecimenID") %>'></asp:Label>
                                                                                <asp:Label ID="lblCustomSpecimenName" runat="server" Visible="false" Text='<%# Eval("CustomSpecimenName") %>'></asp:Label>
                                                                                <asp:Label ID="lblStateId" runat="server" Visible="false" Text='<%# Eval("StateId") %>'></asp:Label>
                                                                                <asp:Label ID="lblStatusId" runat="server" Visible="false" Text='<%# Eval("StatusId") %>'></asp:Label>
                                                                                <asp:Label ID="lblRejectedReasonId" runat="server" Visible="false" Text='<%# Eval("RejectedReasonId") %>'></asp:Label>
                                                                                <asp:Label ID="lblOtherReason" runat="server" Visible="false" Text='<%# Eval("OtherReason") %>'></asp:Label>

                                                                                <asp:Label ID="lblConfirmed" runat="server" Visible="false" Text='<%# Eval("Confirmed") %>'></asp:Label>
                                                                                <asp:Label ID="lblConfirmedby" runat="server" Visible="false" Text='<%# Eval("Confirmedby") %>'></asp:Label>
                                                                                </span>
                                                                                <telerik:RadGrid AutoGenerateColumns="false" ID="RadGridArvMutation" runat="server"
                                                                                    Width="100%" PageSize="5" AllowSorting="true" AllowPaging="true" AllowMultiRowSelection="false"
                                                                                    ClientSettings-Selecting-AllowRowSelect="true" ClientSettings-Resizing-AllowColumnResize="true"
                                                                                    ShowFooter="true" ClientSettings-Resizing-EnableRealTimeResize="true" OnItemDataBound="RadGridArvMutation_ItemDataBound"
                                                                                    OnItemCommand="RadGridArvMutation_ItemCommand" OnDeleteCommand="RadGridArvMutation_DeleteCommand"
                                                                                    CellPadding="0" Skin="Office2010Silver" OnItemCreated="RadGridArvMutation_ItemCreated">
                                                                                    <PagerStyle Mode="NextPrevAndNumeric" />
                                                                                    <ClientSettings>
                                                                                        <Selecting AllowRowSelect="True"></Selecting>
                                                                                        <Resizing AllowColumnResize="True" EnableRealTimeResize="True"></Resizing>
                                                                                    </ClientSettings>
                                                                                    <MasterTableView AutoGenerateColumns="False" DataKeyNames="ID" CellSpacing="0" CellPadding="0">
                                                                                        <Columns>
                                                                                            <telerik:GridTemplateColumn HeaderText="ARV Type" HeaderStyle-Font-Bold="true">
                                                                                                <HeaderStyle Font-Size="12px" Wrap="False" Width="180px" />
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Eval("ID") %>'></asp:Label>
                                                                                                    <asp:Label ID="lblArvType" runat="server" Text='<%# Eval("ArvType") %>'></asp:Label>
                                                                                                    <asp:Label ID="lblArvTypeID" runat="server" Visible="false" Text='<%# Eval("ArvTypeID") %>'></asp:Label>
                                                                                                    <asp:Label ID="lblDeleteFlag" runat="server" Visible="false" Text='<%# Eval("DeleteFlag") %>'></asp:Label>
                                                                                                    <asp:Label ID="lblMutationID" runat="server" Visible="false" Text='<%# Eval("ArvMutationID") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <EditItemTemplate>
                                                                                                    <telerik:RadComboBox ID="rcbEditArvType" runat="server" Text="aSomeTest" AutoPostBack="false"
                                                                                                        Skin="Office2010Silver" CheckedItemsTexts="FitInInput">
                                                                                                    </telerik:RadComboBox>
                                                                                                </EditItemTemplate>
                                                                                                <FooterTemplate>
                                                                                                    <telerik:RadComboBox ID="rcbFooterArvType" runat="server" Text="--Select--" AutoPostBack="true"
                                                                                                        Skin="Office2010Silver" CheckedItemsTexts="FitInInput" EnableLoadOnDemand="true"
                                                                                                        OnSelectedIndexChanged="rcbFooterArvType_SelectedIndexChanged">
                                                                                                    </telerik:RadComboBox>
                                                                                                </FooterTemplate>
                                                                                                <HeaderStyle Font-Bold="True"></HeaderStyle>
                                                                                            </telerik:GridTemplateColumn>
                                                                                            <telerik:GridTemplateColumn HeaderText="Mutation" HeaderStyle-Font-Bold="true">
                                                                                                <HeaderStyle Font-Size="12px" Wrap="False" Width="200px" />
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblMutation" runat="server" Text='<%# Eval("ArvMutation") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <EditItemTemplate>
                                                                                                    <telerik:RadComboBox ID="rcbEditMutation" runat="server" Text="--Select--" AutoPostBack="false"
                                                                                                        Skin="Office2010Silver" CheckedItemsTexts="FitInInput" EnableLoadOnDemand="true">
                                                                                                    </telerik:RadComboBox>
                                                                                                </EditItemTemplate>
                                                                                                <FooterTemplate>
                                                                                                    <telerik:RadComboBox ID="rcbFooterMutation" runat="server" Text="--Select--" AutoPostBack="true"
                                                                                                        Skin="Office2010Silver" CheckedItemsTexts="FitInInput" EnableLoadOnDemand="true">
                                                                                                    </telerik:RadComboBox>
                                                                                                </FooterTemplate>
                                                                                                <HeaderStyle Font-Bold="True"></HeaderStyle>
                                                                                            </telerik:GridTemplateColumn>
                                                                                            <telerik:GridTemplateColumn HeaderText="Culture/Sensitivity" HeaderStyle-Font-Bold="true">
                                                                                                <HeaderStyle Font-Size="12px" Wrap="False" Width="200px" />
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblCulture" runat="server" Text='<%# Eval("ArvMutationOther") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <EditItemTemplate>
                                                                                                    <telerik:RadComboBox ID="rcbEditCulture" runat="server" Text="--Select--" AutoPostBack="false"
                                                                                                        Skin="Office2010Silver" CheckedItemsTexts="FitInInput" EnableLoadOnDemand="true">
                                                                                                    </telerik:RadComboBox>
                                                                                                </EditItemTemplate>
                                                                                                <FooterTemplate>
                                                                                                    <telerik:RadComboBox ID="rcbFooterCulture" runat="server" Text="--Select--" AutoPostBack="false"
                                                                                                        Skin="Office2010Silver" CheckedItemsTexts="FitInInput" EnableLoadOnDemand="true">
                                                                                                    </telerik:RadComboBox>
                                                                                                </FooterTemplate>
                                                                                                <HeaderStyle Font-Bold="True"></HeaderStyle>
                                                                                            </telerik:GridTemplateColumn>
                                                                                            <telerik:GridTemplateColumn HeaderText="Other Mutation" HeaderStyle-Font-Bold="true">
                                                                                                <HeaderStyle Font-Size="12px" Wrap="False" Width="150px" />
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblOtherMutation" Skin="Office2010Silver" runat="server" Text='<%# Eval("ArvMutationOther") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <EditItemTemplate>
                                                                                                    <telerik:RadTextBox ID="txtOtherEditMutation" Skin="Office2010Silver" runat="server"
                                                                                                        Text='<%# Eval("ArvMutationOther") %>'>
                                                                                                    </telerik:RadTextBox>
                                                                                                </EditItemTemplate>
                                                                                                <FooterTemplate>
                                                                                                    <telerik:RadTextBox ID="txtOtherFooterMutation" Skin="Office2010Silver" runat="server"
                                                                                                        Text='<%# Eval("ArvMutationOther") %>' Width="120px" Wrap="true">
                                                                                                    </telerik:RadTextBox>
                                                                                                </FooterTemplate>
                                                                                                <HeaderStyle Font-Bold="True"></HeaderStyle>
                                                                                            </telerik:GridTemplateColumn>
                                                                                            <telerik:GridTemplateColumn HeaderStyle-Font-Bold="true">
                                                                                                <HeaderStyle Font-Size="12px" Wrap="False" Width="90px" />
                                                                                                <ItemTemplate>
                                                                                                    <telerik:RadButton ID="btnRemove" runat="server" Skin="Office2010Silver" Text="Remove"
                                                                                                        ForeColor="Blue" CommandName="Delete" ButtonType="LinkButton">
                                                                                                    </telerik:RadButton>
                                                                                                </ItemTemplate>
                                                                                                <FooterTemplate>
                                                                                                    <telerik:RadButton ID="btnFooterAdd" runat="server" Skin="Office2010Silver" Text="Add"
                                                                                                        CommandName="Insert">
                                                                                                    </telerik:RadButton>
                                                                                                </FooterTemplate>
                                                                                                <HeaderStyle Font-Bold="True"></HeaderStyle>
                                                                                            </telerik:GridTemplateColumn>
                                                                                        </Columns>
                                                                                    </MasterTableView>
                                                                                </telerik:RadGrid>
                                                                                
                                                                                <asp:Panel runat="server" ID="pnl_rep" style="padding-left: 10px;border-top:1px solid #eee;padding-top:10px;padding-bottom:5px">
                                                                                    <asp:Label ID="Label17" Visible="true" CssClass="required" runat="server" Text="Reported by:"></asp:Label>
                                                                                    <asp:DropDownList ID="ddlRadReportedby" runat="server" Skin="Office2010Silver" Width="150px">
                                                                                    </asp:DropDownList>
                                                                                    &emsp;
                                                                                    <asp:Label ID="LblDatetime" Visible="true" runat="server" Text="Date Time:"></asp:Label>
                                                                                    <telerik:RadDateTimePicker ID="txtReportLabDate" runat="server" Skin="Office2007"
                                                                                        Width="200px">
                                                                                        <ClientEvents OnDateSelected="DateSelected" />
                                                                                        <TimeView ID="TimeViewRadReportedby" Skin="Default" ShowHeader="true" StartTime="08:00:00"
                                                                                            Interval="00:15:00" runat="server" EndTime="20:15:00" Columns="7" OnClientTimeSelecting="ClientTimeSelected">
                                                                                        </TimeView>
                                                                                        <DateInput ID="DateInputRadReportedby" DateFormat="dd-MMM-yyyy hh:mm tt" runat="server">
                                                                                        </DateInput>
                                                                                    </telerik:RadDateTimePicker>
                                                                                </asp:Panel>
                                                                                
                                                                                <asp:Panel runat="server" ID="pnlConfirm" >
                                                                                <fieldset class="scheduler-border">
                                                                                <legend class="scheduler-border">Confirm Result</legend>                                                                                    
                                                                                     <asp:CheckBox ID="chkConfirm" runat="server" Text="Confirm" TextAlign="Right" />
                                                                                    &emsp;
                                                                                    <asp:Label ID="Label2" Visible="true" CssClass="required" runat="server" Font-Bold="true" Text="Confirmed by:"></asp:Label>
                                                                                    <asp:DropDownList ID="ddlconfirmedby" runat="server" Skin="Office2010Silver" Width="150px">
                                                                                    </asp:DropDownList>
                                                                                    </fieldset>
                                                                                </asp:Panel>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                    </Columns>
                                                                </MasterTableView>
                                                            </telerik:RadGrid>
                                                        </NestedViewTemplate>
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <br />
                        <div class="border center formbg">
                            <table cellspacing="6" cellpadding="0" width="100%" border="0" id="Table4" runat="server">
                                <tr id="Tr2" runat="server" align="center">
                                    <td id="Td2" runat="server" class="form">
                                        <asp:Button ID="BtnSaveLabResults" runat="server" Text="Save" OnClick="BtnSaveLabResults_Click"
                                            CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                                        <asp:Label ID="Label18" CssClass="glyphicon glyphicon-floppy-disk" Style="margin-left: -3%;
                                            margin-right: 2%; vertical-align: sub; color: #fff;" runat="server"></asp:Label>
                                        <asp:Button ID="BtnCloseLabResults" runat="server" Text="Close" CssClass="btn btn-primary"
                                            Height="30px" Width="8%" Style="text-align: left;" OnClick="BtnCloseLabResults_Click" />
                                        <label class="glyphicon glyphicon-remove-circle" style="margin-left: -3%; margin-right: 2%;
                                            vertical-align: sub; color: #fff;">
                                        </label>
                                        <asp:Button ID="BtnPrintLabResults" runat="server" OnClientClick="WindowPrint()"
                                            Text="Print" CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                                        <label class="glyphicon glyphicon-print" style="margin-left: -3%; vertical-align: sub;
                                            color: #fff;">
                                        </label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <%--</ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="tabControl" EventName="ActiveTabChanged" />
                                <asp:AsyncPostBackTrigger ControlID="BtnSaveLabResults" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="BtnCloseLabResults" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>--%>
                    </ContentTemplate>
                </act:TabPanel>
            </act:TabContainer>
        </div>
    </div>
    <asp:HiddenField ID="txthdnfield" runat="server" />    
    <asp:Table ID="tblPendingOrders" runat="server" Width="400px" Height="300px" BackColor="White"
            CssClass="table-condensed">
            <asp:TableRow>
                <asp:TableCell Height="20px" BackColor="#205E8D">
                    <asp:Table ID="Table11" runat="server" Width="100%">
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="White"
                                Font-Bold="true">
                                Pending Orders
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Right" VerticalAlign="Middle">
                                <asp:ImageButton ID="btnPendingOrdersClose" runat="server" ImageUrl="~/Images/closeButton1.png"
                                    Height="20px" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Height="100%">
                    <div class="grid" id="divBills" style="width: 100%;">
                        <div class="rounded">
                            <div class="mid-outer">
                                <div class="mid-inner">
                                    <div class="mid" style="height: 400px; overflow: auto">
                                        <div id="div1" class="GridView whitebg">
                                            <asp:GridView ID="gvLaborders" runat="server" AutoGenerateColumns="False"
                                                Width="100%" BorderColor="white" PageIndex="1" BorderWidth="1" GridLines="None"
                                                CssClass="table table-bordered table-hover" CellPadding="0" CellSpacing="0">
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>                                                                                  
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
                        </div>
                    </div>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    <asp:UpdateProgress ID="sProgress" runat="server" DisplayAfter="5">
        <ProgressTemplate>
            <div style="width: 100%; height: 100%; position: fixed; top: 0px; left: 0px; vertical-align: middle;">
                <table style="position: relative; top: 45%; left: 45%; border: solid 1px #808080;
                    background-color: #FFFFC0; width: 150px; height: 24px;" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="right" valign="middle" style="width: 30px; height: 22px;">
                            <img src="../Images/loading.gif" height="16px" width="16px" alt="" />
                        </td>
                        <td align="left" valign="middle" style="font-weight: bold; color: #808080; width: 100px;
                            height: 22px; padding-left: 5px">
                            Processing....
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
