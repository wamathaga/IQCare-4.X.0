<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    CodeBehind="frm_Laboratory.aspx.cs" Inherits="PresentationApp.Laboratory.frm_Laboratory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <script type="text/javascript">
        function RegisterJQuery() {
            $('#txtLabtobeDone').datepicker({ autoclose: true });
            $('#txtlaborderedbydate').datepicker({ autoclose: true });
            $('#txtrepordtedbydate').datepicker({ autoclose: true });
            $(".datePicker").datepicker({ autoclose: true });
        }
        //Calling RegisterJQuery when document is ready (Page loaded first time)
        $(document).ready(RegisterJQuery);
        //Calling RegisterJQuery when the page is doing postback (asp.net)
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RegisterJQuery);
    </script>
    <script type="text/javascript">
        function OnANCClose() {
            debugger;
            window.opener.LabPopUpClosed();
            window.close();
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
        function myFunction() {
            $('#dvMsg').show();
            setTimeout(function () { $("#dvMsg").fadeOut(1500); }, 8000)

        }

    </script>
    <style type="text/css">
        .RadAutoCompleteBox_Metro .ui .li
        {
            float: left !important;
            text-align: left;
        }
        .RadComboBoxDropDown .rcbItem, .RadComboBoxDropDown .rcbHovered, .RadComboBoxDropDown .rcbDisabled, .RadComboBoxDropDown .rcbLoading, .RadComboBoxDropDown .rcbCheckAllItems, .RadComboBoxDropDown .rcbCheckAllItemsHovered
        {
            min-height: 10px;
            font: 12px Arial, sans-serif;
        }
        
        .RadAutoCompleteBox_Metro .racItem, .RadAutoCompleteBox_Metro .racInput, .RadAutoCompleteBox_Metro .racHovered
        {
            float: left !important;
        }
        .RadGrid_Metro tr.Row50
        {
            height: 10px;
        }
        .RadComboBox_Metro, .RadComboBox_Metro .rcbInput, .RadComboBoxDropDown_Metro
        {
            font: 12px Arial, sans-serif;
            color: #333;
            text-align: left;
        }
        .RadAutoCompleteBoxPopup_Default, .RadAutoCompleteBoxPopup_Metro, .RadAutoCompleteBoxPopup .racList
        {
            max-height: 250px !important;
            text-align: left;
        }
        .notification
        {
            /*background-color: #006699;*/
            min-height: 100px;
            width: 30%;
            margin: 0 auto;
            text-align: center;
            line-height: 30px;
            margin-top: 20px;
            color: #fff;
            font-size: 12px;
            font-weight: bold;
            box-shadow: 10px 10px 5px #888888;
            position: absolute;
            top: -140px;
            right: 5px;
            border-radius: 4px;
            -moz-border-radius: 4px;
            -webkit-border-radius: 4px;
            -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=90)"; /* IE 8 */
            filter: alpha(opacity=90); /* IE 5-7 */
            -moz-opacity: 0.9; /* Netscape */
            -khtml-opacity: 0.9; /* Safari 1.x */
            opacity: 0.9; /* Good browsers */
            background: rgb(216, 119, 119); /* Old browsers */
            background: -moz-linear-gradient(top, rgba(216, 119, 119, 1) 1%, rgba(217, 87, 87, 1) 100%); /* FF3.6+ */
            background: -webkit-gradient(linear, left top, left bottom, color-stop(1%, rgba(216, 119, 119, 1)), color-stop(100%, rgba(217, 87, 87, 1))); /* Chrome,Safari4+ */
            background: -webkit-linear-gradient(top, rgba(216, 119, 119, 1) 1%, rgba(217, 87, 87, 1) 100%); /* Chrome10+,Safari5.1+ */
            background: -o-linear-gradient(top, rgba(216, 119, 119, 1) 1%, rgba(217, 87, 87, 1) 100%); /* Opera11.10+ */
            background: -ms-linear-gradient(top, rgba(216, 119, 119, 1) 1%, rgba(217, 87, 87, 1) 100%); /* IE10+ */
            border: 1px solid #b72224;
        }
        .close-image
        {
            display: block;
            float: left;
            position: relative;
            top: -10px;
            left: -10px;
            height: 20px;
        }
    </style>
    <asp:UpdatePanel ID="Updatepanel" runat="server">
        <ContentTemplate>
            <div class="content-wrapper">
                <div class="box-body">
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="box box-primary box-solid">
                                <div class="box-header">
                                    <h3 class="box-title">
                                        Order Labs</h3>
                                </div>
                                <!-- /.box-header -->
                                <div class="box-body table-responsive no-padding" style="overflow: hidden; margin-left: 5px;">
                                    <%--Main Content Start--%>
                                    <div class="row">
                                        <br />
                                        <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
                                            <label class="control-label">
                                                Preclinic Labs:</label>
                                        </div>
                                        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                            <asp:CheckBox ID="preclinicLabs" runat="server" Style="vertical-align: bottom; margin-bottom: -3px;
                                                display: inline-block" />
                                        </div>
                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group text-right">
                                            <label class="control-label">
                                                Lab to be done on:</label>
                                        </div>
                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                            <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtLabtobeDone" clientidmode="Static"
                                                        maxlength="11" runat="server" data-date-format="dd-M-yyyy" style="width: 110px;" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group text-nowrap text-right">
                                            <label class="control-label">
                                                Pre-Selected Labs:</label>
                                        </div>
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                            <telerik:radcombobox id="rcbPreSelectedLabTest" runat="server" text="aSomeTest" autopostback="true"
                                                skin="Metro" checkboxes="true" enableloadondemand="false" enablecheckallitemscheckbox="true"
                                                checkeditemstexts="FitInInput" width="100%" cssclass="form-control">
                                                </telerik:radcombobox>
                                        </div>
                                        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                            <telerik:radbutton id="btnAddDrug" text="Add" runat="server" width="65%" skin="Metro"
                                                onclick="BtnAddDrugClick" cssclass="btn btn-primary">
                                                </telerik:radbutton>
                                            <span class="glyphicon glyphicon-plus" style="vertical-align: sub; margin-left: -3%;
                                                margin-right: 2%; color: #fff"></span>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                            <label class="control-label">
                                                Select Lab:</label>
                                        </div>
                                        <div class="col-md-10 col-sm-12 col-xs-12 form-group">
                                            <telerik:radautocompletebox id="AutoselectLabTest" runat="server" skin="Metro" dropdownwidth="500px"
                                                minfilterlength="2" width="555" onentryadded="Autoselectdrug_EntryAdded" cssclass="form-control">
                                                    <WebServiceSettings Path="frm_Laboratory.aspx" Method="GetLabsNames" />
                                                </telerik:radautocompletebox>
                                        </div>
                                    </div>
                                    <div class="row" align="center">
                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group" id="Tr2" runat="server">
                                            <a id="sGrid" href="#sGrid"></a>
                                            <telerik:radgrid autogeneratecolumns="false" id="RadGridLabTest" runat="server" width="100%"
                                                pagesize="5" allowpaging="false" allowmultirowselection="false" clientsettings-selecting-allowrowselect="true"
                                                clientsettings-resizing-allowcolumnresize="true" showfooter="true" clientsettings-resizing-enablerealtimeresize="true"
                                                cellpadding="0" skin="Metro" onitemcreated="RadGridLabTest_ItemCreated" onneeddatasource="RadGridLabTest_NeedDataSource"
                                                onitemcommand="RadGridLabTest_ItemCommand" ondeletecommand="RadGridLabTest_DeleteCommand"
                                                onitemdatabound="RadGridLabTest_ItemDataBound" culture="(Default)">
                                        <PagerStyle Mode="NextPrevAndNumeric" />
                                        <ClientSettings>
                                            <Selecting AllowRowSelect="True"></Selecting>
                                            <Resizing AllowColumnResize="True" EnableRealTimeResize="True"></Resizing>
                                        </ClientSettings>
                                        <MasterTableView AutoGenerateColumns="False" NoMasterRecordsText="No Records Found"
                                            DataKeyNames="LabTestID" CellSpacing="0" CellPadding="0">
                                            <%--<NoRecordsTemplate>
                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td align="center">
                                                                No Records Found
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </NoRecordsTemplate>--%>
                                            <Columns>
                                                <telerik:GridTemplateColumn HeaderText="Laboratory Test Name" HeaderStyle-Font-Bold="true">
                                                    <HeaderStyle Font-Size="10px" Wrap="False" Width="25%" Font-Bold="True" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLabTestName" runat="server" Text='<%# Eval("SubTestName") %>'></asp:Label>
                                                        <asp:Label ID="lblLabSubTestID" runat="server" Visible="false" Text='<%# Eval("SubTestId") %>'></asp:Label>
                                                        <asp:Label ID="lblLabTestID" runat="server" Visible="false" Text='<%# Eval("LabTestId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                  
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Units" HeaderStyle-Font-Bold="true">
                                                    <HeaderStyle Font-Size="10px" Wrap="False" Width="10%" Font-Bold="True" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLabTestUom" runat="server" Text='<%# Eval("UnitName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                   
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Test Type" HeaderStyle-Font-Bold="true">
                                                    <HeaderStyle Font-Size="10px" Wrap="False" Width="18%" Font-Bold="True" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLabTestType" runat="server" Text='<%# Eval("LabDepartmentName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                  
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Urgent" HeaderStyle-Font-Bold="true">
                                                  <HeaderStyle Font-Size="10px" Wrap="False" Width="5%" Font-Bold="True" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox runat="server" ID="chkUrgent" Checked='<%# (Convert.ToBoolean(Eval("UrgentFlag")) == true)? true: false %>'></asp:CheckBox>
                                                    </ItemTemplate>
                                                     
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Justification" HeaderStyle-Font-Bold="true">
                                                <HeaderStyle Font-Size="10px" Wrap="False" Width="18%" Font-Bold="True" />
                                                <ItemTemplate>
                                                  

                                                    <telerik:RadComboBox ID="ddlJustification" runat="server" AutoPostBack="true" Skin="Windows7"
                                                                        EnableLoadOnDemand="true" OnSelectedIndexChanged="ddlJustification_SelectedIndexChanged">
                                                                    </telerik:RadComboBox>

                                                    <telerik:RadTextBox runat="server" ID="txtJustification" visible="false" Skin="Metro"  Text='<%# Eval("JustificationOther")%>'></telerik:RadTextBox>

                                                  
                                                </ItemTemplate>


                                                
                                            </telerik:GridTemplateColumn>
                                             
                                             
                                             <telerik:GridTemplateColumn HeaderText="Rejected/Repeat" HeaderStyle-Font-Bold="true">
                                               <HeaderStyle Font-Size="10px" Wrap="False" Width="10%" Font-Bold="True"/>
                                                  <ItemTemplate>
                                                        <asp:CheckBox runat="server" ID="chkReject" Checked='<%# (Convert.ToBoolean(Eval("RejectFlag")) == true)? true: false %>'></asp:CheckBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>


                                                <telerik:GridTemplateColumn HeaderText="" HeaderStyle-Font-Bold="true">
                                                    <ItemTemplate>
                                                        <telerik:RadButton ID="btnRemove" runat="server" Skin="Metro" Text="Remove" ForeColor="Blue"
                                                            CommandName="Delete" ButtonType="LinkButton">
                                                        </telerik:RadButton>
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
                                                    <NoRecordsTemplate>
                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td align="center">
                                                               Results not yet updated
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </NoRecordsTemplate>
                                                        <Columns>
                                                            <telerik:GridTemplateColumn HeaderText="Lab Test Name" HeaderStyle-Font-Bold="true"
                                                                UniqueName="SubTestId">
                                                                <ItemTemplate>
                                                                    <telerik:RadComboBox ID="ddlList" runat="server" AutoPostBack="true" Skin="Windows7"
                                                                        EnableLoadOnDemand="true" OnSelectedIndexChanged="ddllist_SelectedIndexChanged">
                                                                    </telerik:RadComboBox>
                                                                    <telerik:RadNumericTextBox ID="txtRadValue" runat="server" Text='<%# Eval("TestResults") %>'
                                                                        Skin="Metro">
                                                                    </telerik:RadNumericTextBox>
                                                                    <telerik:RadTextBox ID="txtAlphaRadValue" runat="server" Text='<%# Eval("TestResults1") %>'
                                                                        Skin="Metro">
                                                                    </telerik:RadTextBox>
                                                                    <asp:RadioButtonList ID="btnRadRadiolist" runat="server" RepeatDirection="Horizontal">
                                                                    </asp:RadioButtonList>
                                                                    <asp:CheckBoxList ID="chkBoxList" runat="server" RepeatDirection="Vertical">
                                                                    </asp:CheckBoxList>
                                                                    <%-- <asp:DropDownList ID="ddlList" runat="server"></asp:DropDownList>--%>
                                                                    <asp:Label ID="lblUnitName" runat="server" Text='<%# Eval("UnitName") %>'></asp:Label>
                                                                   
                                                                    <asp:Label ID="LblResultReportBy" CssClass="pad10" Font-Bold="true" runat="server" Text='<%# Eval("ReportedbyName") %>'></asp:Label>
                                                                    <asp:Label ID="LblResultReportDate" runat="server" Font-Bold="true" Text='<%# Eval("ResReportedbyDate") %>'></asp:Label>
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
                                                                    
                                                                    
                                                                    <telerik:RadGrid AutoGenerateColumns="false" ID="RadGridArvMutation" runat="server"
                                                                        Width="100%" PageSize="5" AllowSorting="true" AllowPaging="true" AllowMultiRowSelection="false"
                                                                        ClientSettings-Selecting-AllowRowSelect="true" ClientSettings-Resizing-AllowColumnResize="true"
                                                                        ShowFooter="true" ClientSettings-Resizing-EnableRealTimeResize="true" OnItemDataBound="RadGridArvMutation_ItemDataBound"
                                                                        OnItemCommand="RadGridArvMutation_ItemCommand" OnDeleteCommand="RadGridArvMutation_DeleteCommand"
                                                                        CellPadding="0" Skin="Metro">
                                                                        <PagerStyle Mode="NextPrevAndNumeric" />
                                                                        <ClientSettings>
                                                                            <Selecting AllowRowSelect="True"></Selecting>
                                                                            <Resizing AllowColumnResize="True" EnableRealTimeResize="True"></Resizing>
                                                                        </ClientSettings>
                                                                        <MasterTableView AutoGenerateColumns="False" DataKeyNames="ID" CellSpacing="0" CellPadding="0">
                                                                            <Columns>
                                                                                <telerik:GridTemplateColumn HeaderText="ARV Type" HeaderStyle-Font-Bold="true">
                                                                                    <HeaderStyle Font-Size="10px" Wrap="False" Width="180px" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Eval("ID") %>'></asp:Label>
                                                                                        <asp:Label ID="lblArvType" runat="server" Text='<%# Eval("ArvType") %>'></asp:Label>
                                                                                        <asp:Label ID="lblArvTypeID" runat="server" Visible="false" Text='<%# Eval("ArvTypeID") %>'></asp:Label>
                                                                                        <asp:Label ID="lblDeleteFlag" runat="server" Visible="false" Text='<%# Eval("DeleteFlag") %>'></asp:Label>
                                                                                        <asp:Label ID="lblMutationID" runat="server" Visible="false" Text='<%# Eval("ArvMutationID") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <%--<EditItemTemplate>
                                                                                        <telerik:RadComboBox ID="rcbEditArvType" runat="server" Text="aSomeTest" AutoPostBack="false"
                                                                                            Skin="Metro" CheckedItemsTexts="FitInInput">
                                                                                        </telerik:RadComboBox>
                                                                                    </EditItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <telerik:RadComboBox ID="rcbFooterArvType" runat="server" Text="--Select--" AutoPostBack="true"
                                                                                            Skin="Metro" CheckedItemsTexts="FitInInput" EnableLoadOnDemand="true" OnSelectedIndexChanged="rcbFooterArvType_SelectedIndexChanged">
                                                                                        </telerik:RadComboBox>
                                                                                    </FooterTemplate>--%>
                                                                                    <HeaderStyle Font-Bold="True"></HeaderStyle>
                                                                                </telerik:GridTemplateColumn>
                                                                                <telerik:GridTemplateColumn HeaderText="Mutation" HeaderStyle-Font-Bold="true">
                                                                                    <HeaderStyle Font-Size="10px" Wrap="False" Width="200px" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblMutation" runat="server" Text='<%# Eval("ArvMutation") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <%--<EditItemTemplate>
                                                                                        <telerik:RadComboBox ID="rcbEditMutation" runat="server" Text="--Select--" AutoPostBack="false"
                                                                                            Skin="Metro" CheckedItemsTexts="FitInInput" EnableLoadOnDemand="true">
                                                                                        </telerik:RadComboBox>
                                                                                    </EditItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <telerik:RadComboBox ID="rcbFooterMutation" runat="server" Text="--Select--" AutoPostBack="true"
                                                                                            Skin="Metro" CheckedItemsTexts="FitInInput" EnableLoadOnDemand="true" OnSelectedIndexChanged="rcbFooterMutation_SelectedIndexChanged">
                                                                                        </telerik:RadComboBox>
                                                                                    </FooterTemplate>--%>
                                                                                    <HeaderStyle Font-Bold="True"></HeaderStyle>
                                                                                </telerik:GridTemplateColumn>
                                                                                <telerik:GridTemplateColumn HeaderText="Culture/Sensitivity" HeaderStyle-Font-Bold="true">
                                                                                    <HeaderStyle Font-Size="10px" Wrap="False" Width="200px" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblCulture" runat="server" Text='<%# Eval("ArvMutationOther") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <%--<EditItemTemplate>
                                                                                        <telerik:RadComboBox ID="rcbEditCulture" runat="server" Text="--Select--" AutoPostBack="false"
                                                                                            Skin="Metro" CheckedItemsTexts="FitInInput" EnableLoadOnDemand="true">
                                                                                        </telerik:RadComboBox>
                                                                                    </EditItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <telerik:RadComboBox ID="rcbFooterCulture" runat="server" Text="--Select--" AutoPostBack="false"
                                                                                            Skin="Metro" CheckedItemsTexts="FitInInput" EnableLoadOnDemand="true">
                                                                                        </telerik:RadComboBox>
                                                                                    </FooterTemplate>--%>
                                                                                    <HeaderStyle Font-Bold="True"></HeaderStyle>
                                                                                </telerik:GridTemplateColumn>
                                                                                <telerik:GridTemplateColumn HeaderText="Other Mutation" HeaderStyle-Font-Bold="true">
                                                                                    <HeaderStyle Font-Size="10px" Wrap="False" Width="150px" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblOtherMutation" Skin="Metro" runat="server" Text='<%# Eval("ArvMutationOther") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <%--<EditItemTemplate>
                                                                                        <telerik:RadTextBox ID="txtOtherEditMutation" Skin="Metro" runat="server" Text='<%# Eval("ArvMutationOther") %>'>
                                                                                        </telerik:RadTextBox>
                                                                                    </EditItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <telerik:RadTextBox ID="txtOtherFooterMutation" Skin="Metro" runat="server" Text='<%# Eval("ArvMutationOther") %>'
                                                                                            Width="120px" Wrap="true">
                                                                                        </telerik:RadTextBox>
                                                                                    </FooterTemplate>--%>
                                                                                    <HeaderStyle Font-Bold="True"></HeaderStyle>
                                                                                </telerik:GridTemplateColumn>
                                                                                <telerik:GridTemplateColumn HeaderStyle-Font-Bold="true">
                                                                                    <HeaderStyle Font-Size="10px" Wrap="False" Width="90px" />
                                                                                    <ItemTemplate>
                                                                                        <telerik:RadButton ID="btnRemove" runat="server" Skin="Metro" Text="Remove" ForeColor="Blue"
                                                                                            CommandName="Delete" ButtonType="LinkButton">
                                                                                        </telerik:RadButton>
                                                                                    </ItemTemplate>
                                                                                    <%--<FooterTemplate>
                                                                                        <telerik:RadButton ID="btnFooterAdd" runat="server" Skin="Metro" Text="Add" CommandName="Insert">
                                                                                        </telerik:RadButton>
                                                                                    </FooterTemplate>--%>
                                                                                    <HeaderStyle Font-Bold="True"></HeaderStyle>
                                                                                </telerik:GridTemplateColumn>
                                                                                <%--  <telerik:GridEditCommandColumn ButtonType="PushButton"  ItemStyle-Font-Names="verdana" ItemStyle-Font-Size="10pt"  >
                                                            </telerik:GridEditCommandColumn>--%>
                                                                            </Columns>
                                                                        </MasterTableView>
                                                                        <%--<FooterStyle Font-Names="Verdana" Font-Size="10pt" HorizontalAlign="Left" />
                                                    <HeaderStyle Font-Names="Verdana" Font-Size="10pt" HorizontalAlign="Left" />--%>
                                                                    </telerik:RadGrid>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                            </NestedViewTemplate>
                                        </MasterTableView>
                                        <%--<FooterStyle Font-Names="Verdana" Font-Size="10pt" HorizontalAlign="Left" />
                                            <HeaderStyle Font-Names="Verdana" Font-Size="10pt" HorizontalAlign="Left" />--%>
                                    </telerik:radgrid>
                                        </div>
                                    </div>
                                    <div class="row" align="center">
                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                            <asp:Panel ID="pnlCustomList" Visible="false" runat="server" Height="100%" Width="100%"
                                                Wrap="true">
                                            </asp:Panel>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                            <label class="control-label" style="margin-left: 5px;">
                                                Ordered by:</label>
                                        </div>
                                        <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                            <asp:DropDownList ID="ddlaborderedbyname" runat="Server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group" align="right">
                                            <label class="control-label">
                                                Ordered By Date:</label>
                                        </div>
                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                            <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtlaborderedbydate" clientidmode="Static"
                                                        maxlength="12" size="11" runat="server" data-date-format="dd-M-yyyy" style="width: 110px;" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" id="trReported" runat="server">
                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                            <label class="control-label required">
                                                Reported by:</label>
                                        </div>
                                        <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                            <asp:DropDownList ID="ddlreportedby" runat="Server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group" align="right">
                                            <label class="control-label required">
                                                Reported By Date:</label>
                                        </div>
                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                            <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtrepordtedbydate" clientidmode="Static"
                                                        maxlength="12" size="11" runat="server" data-date-format="dd-M-yyyy" style="width: 110px;" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" id="btntr" runat="server" align="center">
                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                            <asp:Button ID="btnsave" runat="server" Width="8%" Height="30px" Text="Save" OnClick="BtnSaveClick"
                                                CssClass="btn btn-primary" Style="text-align: left;" />
                                            <span class="glyphicon glyphicon-floppy-disk" style="vertical-align: sub; margin-left: -3%;
                                                margin-right: 2%; color: #fff"></span>
                                            <asp:Button ID="btnclose" runat="server" Width="8%" Height="30px" Text="Close" OnClick="btnclose_Click"
                                                CssClass="btn btn-primary" Style="text-align: left;" />
                                            <span class="glyphicon glyphicon-remove" style="vertical-align: sub; margin-left: -3%;
                                                margin-right: 2%; color: #fff"></span>
                                        </div>
                                    </div>
                                    <%--Main Content End--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="hiddTestID" runat="server" />
                <asp:HiddenField ID="hiddTestAddTestID" runat="server" />
                <asp:HiddenField ID="hdnTestAddTestID" runat="server" />
            </div>
            <div id="Div1" style="position: relative">
                <div id="dvMsg" class="notification" style="display: none;">
                    <img id="closeimg" alt="close" class="close-image" src="../Incl/notification_center_plugin/images/growl-close.png"
                        style="cursor: pointer;" onclick="$(this).parent().hide();" />
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnclose"></asp:PostBackTrigger>
        </Triggers>
    </asp:UpdatePanel>
    <asp:HiddenField ID="txthdnfield" runat="server" />
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
