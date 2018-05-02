<%@ Page Title="Facility Billing Reports" Language="C#" MasterPageFile="~/MasterPage/IQCare.master"
    AutoEventWireup="true" CodeBehind="frmBillingReportPage.aspx.cs" Inherits="IQCare.Web.Billing.frmBillingReportPage" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" %>
<asp:Content ID="ctReportPage" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
 <style type="text/css">
.modalBackground
{
  background-color:#F5F5F5;
  filter: alpha(opacity=80);
  opacity: 0.80;  
 }
</style>
    <script type="text/javascript">
        function printPage() {
            window.open("./frmCustomReportPrint.aspx");
            // div.innerHTML = '<iframe src="./frmCustomReportPrint.aspx" onload="this.contentWindow.print();"></iframe>';
        }
        function openReportPage(path) {
            window.open(path, 'popupwindow', 'toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=yes,resizable=no,width=950,height=650,scrollbars=yes');
        }
    </script>
    <script type="text/javascript">
        function RegisterJQuery() {
            $('#textDateFrom').datepicker({ autoclose: true });
            $('#textDateTo').datepicker({ autoclose: true });            
        }
        //Calling RegisterJQuery when document is ready (Page loaded first time)
        $(document).ready(RegisterJQuery);
        //Calling RegisterJQuery when the page is doing postback (asp.net)
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RegisterJQuery);
</script>
    <div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">Billing Reports</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
	<div class="row" align="center">
         <br />           
        </div>
        <div class="row" id="div-BillingReports">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
  <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" Width="100%" ActiveTabIndex="0" CssClass="ajax__myTab">
                <ajaxToolkit:TabPanel ID="tbpnlStaticreports" runat="server">
                    <HeaderTemplate>
                        Standard Reports
                    </HeaderTemplate>
                    <ContentTemplate>
                        <div class="grid">
                            <asp:Panel ID="divError" runat="server" Style="padding: 5px" CssClass="background-color: #FFFFC0; border: solid 1px #C00000"
                                HorizontalAlign="Left" Visible="False">
                                <asp:Label ID="lblError" runat="server" Style="font-weight: bold; color: #800000"></asp:Label>
                            </asp:Panel>
                            <div class="mid-outer">
                                <div class="mid-inner">
                                    <div class="mid" style="cursor: pointer; height: 280px; overflow: auto; border: 1px solid #666699;">
                                        <div id="div-gridview" class="whitebg" style="width: 950; overflow: auto; margin-top: 5px;
                                            margin-bottom: 5px; max-height: 480px; height: 280px">
                                            <asp:GridView ID="grdBillilingReports" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                BorderColor="White" BorderWidth="1px" CellPadding="0" CssClass="table table-bordered table-hover" GridLines="None"
                                                Width="100%" DataKeyNames="Code,fileName" OnRowCommand="grdBillilingReports_RowCommand">
                                                <Columns>
                                                    <asp:BoundField DataField="Name" HeaderText="Name">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPayItemDesc" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Width="40%" HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:ButtonField CommandName="Generate" Text="Generate" />
                                                </Columns>                                               
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:Button ID="btnRaisePopup" runat="server" Text="Save" Width="60px" Style="display: none" />
                        <asp:HiddenField ID="HReport_ID" runat="server" />
                        <asp:HiddenField ID="HFileName" runat="server" />
                        <asp:Panel ID="divParameters" runat="server" Style="display: none; width: 370px;
                            border: solid 0px #3c8dbc; background-color:#fff;">
                                  <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">
        <asp:Panel ID="divTitleCustomReport" runat="server" Style="cursor: move;">Specify the following values for filtering </asp:Panel>
        </h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
	<div class="row" align="center">
         <br />           
        </div>
          <div class="row">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">Start Date</label>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
              <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="textDateFrom" clientidmode="Static"
                                                       maxlength="11" size="11"  runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>

           
             </div>
            
 </div>
 <div class="row">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label"> End Date:</label>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="textDateTo" clientidmode="Static"
                                                       maxlength="11" size="11"  runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>           
             </div>
            
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Button ID="btnActionOK" runat="server" Text="Continue" CssClass="btn btn-primary"
                                    Height="30px" Width="28%" Style="text-align: left;" />
                                <label class="glyphicon glyphicon-circle-arrow-right" style="margin-left: -7%; margin-right: 3%;
                                    vertical-align: sub; color: #fff;">
                                </label>
                                <asp:Button ID="btnActionCancel" runat="server" Text="Cancel" CssClass="btn btn-primary"
                                    Height="30px" Width="24%" Style="text-align: left;" />
                                <label class="glyphicon glyphicon-remove-circle" style="margin-left: -7%; margin-right: 2%;
                                    vertical-align: sub; color: #fff;">
                                </label>
             </div>
            
 </div>
	 <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>
     </div>

                            
                          
                         
                            
                        </asp:Panel>
                        <ajaxToolkit:ModalPopupExtender ID="parameterPopup" runat="server" BehaviorID="programmaticModalPopupBehavior"
                            TargetControlID="btnRaisePopup" PopupControlID="divParameters" BackgroundCssClass="modalBackground"
                            CancelControlID="btnActionCancel" DropShadow="false" PopupDragHandleControlID="divTitleCustomReport"
                            DynamicServicePath="" Enabled="True" X="450" Y="1">
                        </ajaxToolkit:ModalPopupExtender>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="tbpnlCustomreports" runat="server">
                    <HeaderTemplate>
                        Custom reports
                    </HeaderTemplate>
                    <ContentTemplate>
                        <asp:Panel ID="divErrorCR" runat="server" Style="padding: 5px" CssClass="background-color: #FFFFC0; border: solid 1px #C00000"
                            HorizontalAlign="Left" Visible="False">
                            <asp:Label ID="lblErrorCR" runat="server" Style="font-weight: bold; color: #800000"></asp:Label>
                        </asp:Panel>
                        <div class="row">
 <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
             <label class="control-label">Select Report:</label>
             </div>
             <div class="col-md-8 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlReport" Width="98%" runat="server" CssClass="form-control"></asp:DropDownList>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:Button ID="btnGenerate" runat="server" Text="Generate" OnClick="btnGenerate_Click"
                                            CssClass="btn btn-primary" Height="30px" Width="66%" Style="text-align: left;" />
                                        <label class="glyphicon glyphicon-eye-open" style="margin-left: -12%; margin-right: 2%;
                                            vertical-align: sub; color: #fff;">
                                        </label>
             </div>
            
 </div>

                       
                        <asp:Panel ID="divErrorCustomReport" runat="server" Style="padding: 5px" CssClass="background-color: #FFFFC0; border: solid 1px #C00000"
                            HorizontalAlign="Left" Visible="False">
                            <asp:Label ID="lblErrorCustomReport" runat="server" Style="font-weight: bold; color: #800000"></asp:Label>
                            <asp:HiddenField ID="queryString" runat="server" />
                            <asp:HiddenField ID="thetableName" runat="server" />
                        </asp:Panel>
                        <asp:Panel ID="pnlNoData" runat="server" Visible="False">
                            <span style="color: #600000; font-weight: bold;">There is no data at the moment for
                                the selected report </span>
                        </asp:Panel>
                        <asp:Panel ID="panelResult" runat="server" UpdateMode="Conditional">
                            <div class="mid-outer">
                                <div class="mid-inner">
                                    <div class="mid" style="border: 1px solid #666699;">
                                        <div class="whitebg" id="div-gridview" style="width: 950; overflow: auto; margin-top: 5px;
                                            margin-bottom: 5px; max-height: 480px; height: 280px">
                                            <asp:GridView ID="gridResult" runat="server" CellPadding="0" BorderStyle="Solid"
                                                BorderWidth="1px" HorizontalAlign="Left" Width="100%" CssClass="table table-bordered table-hover" BorderColor="#99CCFF"
                                                EmptyDataText="The report returned no data">
                                                <HeaderStyle CssClass="searchresultfixedheader" Height="20px" HorizontalAlign="Left"
                                                    Wrap="False" />                                               
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <table width="100%">
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btnExport" runat="server" Text="Export To Excel" OnClick="btnExport_Click"
                                            Visible="False" CssClass="btn btn-primary" Height="30px" Width="12%" Style="text-align:left;"/>
                                            <label class="fa fa-file-excel-o" style="margin-left: -2.8%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
                                        <asp:Button ID="btnPrint" runat="server" Text="Print" Visible="False"
                                            OnClientClick="printPage()" CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align:left;"/>
                                            <label class="glyphicon glyphicon-print" style="margin-left: -3%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
                                        <asp:Button ID="btn_close" runat="server" OnClick="btn_close_Click" Text=" Close "
                                            CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                                        <label class="glyphicon glyphicon-remove-circle" style="margin-left: -3%; margin-right: 2%;
                                            vertical-align: sub; color: #fff;">
                                        </label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <div id="printerDiv" style="display: none">
                        </div>
                        <asp:Button ID="btnRaisePopupCustomReport" runat="server" Text="Save" Width="60px"
                            Style="display: none" />
                        <asp:Panel ID="divCustomReportParameters" runat="server" Style="display: none; width: 420px;
                            border: solid 0px #3c8dbc;background-color:#fff;">
                                  <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">
        <asp:Panel ID="divCustomReportTitle" runat="server" Style="cursor: move;">Specify the following values for filtering</asp:Panel>
        </h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
	<div class="row" align="center">
         <br />           
        </div>
        
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
<asp:GridView ID="gridParameter" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="False"
                                DataKeyNames="ParameterName" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="ParameterName" HeaderText="Name" ReadOnly="True" />
                                    <asp:BoundField DataField="DataType" HeaderText="Type" ReadOnly="True" />
                                    <asp:TemplateField HeaderText="Value">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="paramValue" CssClass="form-control" Width="50%"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender runat="server" TargetControlID="paramValue" Format="dd-MMM-yyyy"
                                                ID="ceParamValue" Enabled="True"></ajaxToolkit:CalendarExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
             </div>
             
 </div>
     
    <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
<asp:Button ID="btnActionOKCustomReport" runat="server" Text="Continue" OnClick="btnActionOKCustomReport_Click"
                                    CssClass="btn btn-primary" Height="30px" Width="22%" Style="text-align: left;" />
                                <label class="glyphicon glyphicon-circle-arrow-right" style="margin-left: -4%; margin-right: 2%;
                                    vertical-align: sub; color: #fff;">
                                </label>
                                <asp:Button ID="btnActionCancelCustomReport" runat="server" Text="Cancel" CssClass="btn btn-primary"
                                    Height="30px" Width="18%" Style="text-align: left;" />
                                <label class="glyphicon glyphicon-remove-circle" style="margin-left: -5%; margin-right: 2%;
                                    vertical-align: sub; color: #fff;">
                                </label>
             </div>
             
 </div>                       
	 <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>
     </div>
             </asp:Panel>
                        <ajaxToolkit:ModalPopupExtender ID="parameterPopupCustomR" runat="server" TargetControlID="btnRaisePopupCustomReport"
                            PopupControlID="divCustomReportParameters" BackgroundCssClass="modalBackground"
                            CancelControlID="btnActionCancelCustomReport" DropShadow="false" PopupDragHandleControlID="divCustomReportTitle"
                            DynamicServicePath="" Enabled="True" Y="7">
                        </ajaxToolkit:ModalPopupExtender>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnGenerate" EventName="Click" />
                    <asp:PostBackTrigger ControlID="btnExport" />
                </triggers>
            </ajaxToolkit:TabContainer>
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
            
 </div>
	 <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>
     </div>
     </div>


    
</asp:Content>
