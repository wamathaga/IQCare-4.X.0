<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    CodeBehind="frmPatientConsumables.aspx.cs" Inherits="IQCare.Web.ClinicalForms.PatientConsumables" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <%--    <asp:ScriptManager ID="scriptManager" runat="server" EnablePageMethods="true" />--%>
    <script language="javascript" type="text/javascript">

        function WindowPrint() {
            window.print();
        }
        function WindowPrintAll() {
            window.print();
        }

        function ace1_itemSelected(sender, e) {
            var hdItemName = $get('<%= HItemName.ClientID %>');
            hdItemName.value = e.get_value();
            //alert(hdCustID.value);
        }
        function onClientPopulated(sender, e) {
            var propertyPeople = sender.get_completionList().childNodes;
            for (var i = 0; i < propertyPeople.length; i++) {
                var div = document.createElement("span");
                var results = eval('(' + propertyPeople[i]._value + ')');
                //div.innerHTML = "<span style=' float:right; font-weight:bold;margin-right: 5px;'> " + results.AvlQty + "</span>";
                //div.innerHTML = results.AvlQty;
                // propertyPeople[i].appendChild(div);

            }

        }
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
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
    </style>
     <div class="content-wrapper">
     <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
       <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">Patient Consumables</h3>
       </div>
        <!-- /.box-header -->
        <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <div class="row">
              <asp:UpdatePanel ID="divComponent" runat="server">
                <ContentTemplate>
                    <%--<asp:Panel ID="divError" runat="server" Style="padding: 5px" CssClass="background-color: #FFFFC0; border: solid 1px #C00000"
                        HorizontalAlign="Left" Visible="false">
                        <asp:Label ID="lblError" runat="server" Style="font-weight: bold; color: #800000"
                            Text=""></asp:Label>
                        
                        <br />
                    </asp:Panel>--%>
                    <asp:HiddenField ID="HSelectedDate" runat="server" />
                    <asp:Panel ID="panelBilling" runat="server" Visible="true" Style="width: 100%;">
                        <table width="100%" class="table-condensed">
                            <tr>
                                <td class="form">
                                    <table width="100%" class="table-condensed">
                                        <tbody>
                                            <tr>
                                                <td align="left" valign="middle" width="20%">
                                                    <asp:Button ID="btnFind" MaxLength="20" runat="server" Text="Find Patient" Height="30px"
                                                        Enabled="true" OnClick="btnFind_Click" CssClass="btn btn-primary" Width="62%" />
                                                    <span class="glyphicon glyphicon-search" style="margin-left: -11%; vertical-align: sub;
                                                        color: #fff; text-align:left;"></span>
                                                </td>
                                                <td class="form" width="90%">
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="left">
                                                                Patient Name:
                                                            </td>
                                                            <td align="left">
                                                                <asp:Label ID="lblname" runat="server"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                Sex:
                                                            </td>
                                                            <td align="left">
                                                                <asp:Label ID="lblsex" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                Age:
                                                            </td>
                                                            <td align="left">
                                                                <asp:Label ID="lbldob" runat="server"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                Patient Facility ID:
                                                            </td>
                                                            <td align="left">
                                                                <asp:Label ID="lblFacilityID" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <h5 class='forms' align='left'>
                                                        Select Service Area:
                                                    </h5>
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlCostCenter" CssClass="form-control" runat="server" Style="z-index: 2">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                    </table>
                    </asp:Panel>
                    <asp:Panel ID="panelCalender" runat="server" Visible="true" Style="width: 100%;" class="panel panel-body">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Calendar ID="calendarConsumables" Width="100%" PrevMonthText="<< Previous Month"
                                    NextMonthText="Next Month >>" DayHeaderStyle-Font-Name="Verdana" Height="16px"
                                    TodayDayStyle-ForeColor="Black" DayStyle-BorderWidth="1" DayStyle-BorderStyle="Solid"
                                    OtherMonthDayStyle-ForeColor="#C0C0C0" SelectedDayStyle-ForeColor="#000000" SelectedDayStyle-BackColor="#faebd7"
                                    runat="server" CellSpacing="2" CellPadding="2" BorderColor="SteelBlue" BackColor="white"
                                    TitleStyle-Font-Size="12" TitleStyle-Font-Name="Verdana" TitleStyle-Font-Bold="False"
                                    SelectionMode="Day" DayStyle-Font-Size="12" DayStyle-Font-Name="Arial" DayStyle-VerticalAlign="Top"
                                    DayStyle-HorizontalAlign="Left" DayStyle-Width="15" DayStyle-Height="16" Font-Bold="True">
                                    <TodayDayStyle ForeColor="Black"></TodayDayStyle>
                                    <DayStyle Font-Size="12pt" Font-Names="verdana" HorizontalAlign="Center" Height="20px"
                                        BorderWidth="1px" BorderStyle="Solid" Width="75px" VerticalAlign="Middle"></DayStyle>
                                    <DayHeaderStyle Font-Names="Verdana" ForeColor="Desktop"></DayHeaderStyle>
                                    <SelectedDayStyle ForeColor="#800000" BackColor="#FFFFC0" Font-Bold="True"></SelectedDayStyle>
                                    <TitleStyle Font-Size="12pt" Font-Names="Verdana"></TitleStyle>
                                    <OtherMonthDayStyle ForeColor="Silver"></OtherMonthDayStyle>
                                </asp:Calendar>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                    <br />
                    <br />
                    <div id="divConsumables" class="grid" style="width: 100%;">
                        <div class="rounded">
                            <div class="mid-outer">
                                <div class="mid-inner">
                                    <div class="mid" style="height: 200px; overflow: auto;margin-left:1%;">
                                        <div id="div-gridview" class="GridView whitebg">
                                            <asp:HiddenField ID="HItemName" runat="server" />
                                            <asp:HiddenField ID="HItemID" runat="server" />
                                            <asp:HiddenField ID="HItemTypeID" runat="server" />
                                            <asp:GridView ID="gridConsumables" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                BorderColor="White" BorderWidth="1px" CellPadding="0" CssClass="table table-bordered table-hover" PageIndex="1"
                                                ShowFooter="True" ShowHeaderWhenEmpty="True" Width="98%" GridLines="None" DataKeyNames="PatientItemID,item_pk,issuedate,ItemType"
                                                EmptyDataText="No Consumables for the selected date">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Description">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="labelItemName" runat="server" Text='<%# Bind("itemname") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txtNewDescription" runat="server" AutoPostBack="true" OnTextChanged="txtNewDescription_textChanged"
                                                                Width="99%" Font-Names="Courier New" CssClass="form-control"></asp:TextBox>
                                                            <div id="divwidth" runat="server" style="text-align: left">
                                                            </div>
                                                            <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionInterval="30"
                                                                CompletionListCssClass="AutoExtender" CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                                                CompletionListItemCssClass="AutoExtenderList" CompletionSetCount="10" EnableCaching="false"
                                                                FirstRowSelected="false" MinimumPrefixLength="3" OnClientItemSelected="ace1_itemSelected"
                                                                ServiceMethod="SearchConsumableItems" TargetControlID="txtNewDescription" CompletionListElementID="divwidth">
                                                            </ajaxToolkit:AutoCompleteExtender>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="labelItemName" runat="server" Text='<%# Bind("itemname") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="35%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date Issued">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblEditDate" runat="server" Font-Bold="True" Text='<%# Bind("issuedate", "{0:dd-MMM-yyyy}") %>'
                                                                Width="99%"></asp:Label>
                                                        </EditItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblNewDate" runat="server" Width="99%"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="labelDateIssued" runat="server" Text='<%# Bind("issuedate", "{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="15%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit Price" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="labelPrice" runat="server" Text='<%# Bind("unitsellingprice") %>'
                                                                Width="99%"></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblEditUnitPrice" runat="server" Text='<%# Bind("unitsellingprice") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblNewUnitPrice" runat="server" Width="90%">0</asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quantity">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtEditQuantity" runat="server" onkeypress="return isNumber(event)"
                                                                Text='<%# Bind("quantity") %>' Width="60%" Wrap="False" MaxLength="6" CssClass="form-control"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txtNewQuantity" runat="server" onkeypress="return isNumber(event)"
                                                                Width="60%" OnTextChanged="txtNewQuantity_TextChanged" MaxLength="6" CssClass="form-control">1</asp:TextBox>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="labelQuantity" runat="server" Text='<%# Bind("quantity") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="7%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="labelAmount" runat="server" Text='<%# Bind("amount") %>' Width="99%"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblNewAmountPrice" runat="server" Width="90%"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ModuleName" HeaderText="Issued In">
                                                        <ItemStyle Width="10%" Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="issuedby" HeaderText="Issued By">
                                                        <ItemStyle Width="10%" Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField ShowHeader="False">
                                                        <FooterTemplate>
                                                            <asp:LinkButton ID="btnNewAdd" runat="server" CommandName="AddItem">Add</asp:LinkButton>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                                                Text="Delete"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="5%" />
                                                    </asp:TemplateField>
                                                    <%-- <asp:CommandField ShowDeleteButton="True" />--%>
                                                </Columns>
                                                <FooterStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                                <HeaderStyle CssClass="searchresultfixedheader" Height="20px" VerticalAlign="Middle"
                                                HorizontalAlign="Left"></HeaderStyle>                                          
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
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="calendarConsumables" EventName="SelectionChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlCostCenter" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
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
             </div>

        </div>
       </div>
      </div>
      </div>
     </div>
     </div>


    <div class="container-fluid">
        <h5 class="forms" align="left">
            Consumables</h5>
        <div class="border center formbg">
           
        </div>
    </div>
</asp:Content>
