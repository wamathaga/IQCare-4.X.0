<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    CodeBehind="frmIQToolsQuery.aspx.cs" Inherits="IQCare.Web.Reports.frmIQToolsQuery" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
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
        <h3 class="box-title">IQ Tools Query</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
         <div class="row box-header">
         <br />
         <div class="col-md-2 col-sm-12 col-xs-12 form-group">
         <h5 class="box-title">Clincal Indicators:</h5>
         </div>  
         <div class="col-md-10 col-sm-12 col-xs-12 form-group">
         <asp:Button ID="btrRefresh" runat="server" Text="Refresh Indicators" OnClick="btrRefresh_Click" CssClass="btn btn-primary" Height="30px" Width="17%" Style="text-align:left;"/>
         <label class="glyphicon glyphicon-refresh" style="margin-left: -3%; vertical-align: sub; color: #fff; margin-right: 1%;"></label>
         </div>           
            </div>
         
            <asp:UpdatePanel ID="panelFilter" runat="server" ChildrenAsTriggers="true">
                <ContentTemplate>
                <div class="row">
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
             <label class="control-label">Select Category:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlCategory" Width="99%" runat="server" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"
                                        AutoPostBack="True" CssClass="form-control">
                                    </asp:DropDownList>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
             <label class="control-label">Select Query:</label>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlReport" Width="99%" runat="server" CssClass="form-control"></asp:DropDownList>
             </div> 
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             
             <asp:Button ID="btnGenerate" runat="server" Text="View" OnClick="btnGenerate_Click" CssClass="btn btn-primary" Height="30px" Width="50%" Style="text-align:left;"/>             
             <label class="glyphicon glyphicon-eye-open" style="margin-left: -20%; vertical-align: sub; color: #fff; margin-right: 1%;"></label>
             </div>
 </div>
 <div class="row">
 <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Start Date</label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="textDateFrom" clientidmode="Static"
                                                       maxlength="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
                                            </div>
             
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <label  class="control-label">End Date:</label>
             </div> 
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="textDateTo" clientidmode="Static"
                                                       maxlength="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
             </div> 
             
             <div class="col-md-2 col-sm-12 col-xs-12 form-group text-nowrap text-right">
             <label  class="control-label">CD4 Cutoff For ART:</label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="textCD4" runat="server" Text="350" Width="80px" CssClass="form-control"/>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="fteCD4" runat="server" FilterType="Numbers"
                                        TargetControlID="textCD4" />
                                    <asp:HiddenField ID="hCutOffCD4" runat="server" Value="350" />
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            
             </div>

 </div>
        <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
              <asp:Panel ID="divError" runat="server" Style="padding: 5px" CssClass="background-color: #FFFFC0; border: solid 1px #C00000"
                        HorizontalAlign="Left" Visible="false">
                        <asp:Label ID="lblError" runat="server" Style="font-weight: bold; color: #800000"
                            Text=""></asp:Label>
                        <asp:HiddenField ID="queryString" runat="server" />
                        <asp:HiddenField ID="thetableName" runat="server" />
                    </asp:Panel>
             </div>
           
 </div>            
       <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
  <asp:Panel ID="pnlNoData" runat="server" Visible="false">
                        <span style="color: #600000; font-weight: bold;">There is no data at the moment for
                            the selected report </span>
                    </asp:Panel>
 </div>
 </div>                 
                   
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="panelResult" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="mid-outer">
                        <div class="mid-inner">
                            <div class="mid" style="border: 1px solid #666699;">
                                <div class="whitebg" id="div-gridview" style="width: 950; overflow: auto; margin-top: 5px;
                                    margin-bottom: 5px; max-height: 480px; height: 280px">
                                    <asp:GridView ID="gridResult" runat="server" CellPadding="0" CellSpacing="0" EnableModelValidation="True"
                                        BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" Width="100%" UseAccessibleHeader="true"
                                        AllowSorting="false" GridLines="Both" CssClass="table table-bordered table-hover" BorderColor="#99CCFF"
                                        EmptyDataText="The report returned no data">
                                        <HeaderStyle CssClass="searchresultfixedheader" Height="20px" HorizontalAlign="Left" Wrap="False" />
                                       
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <table width="100%">
                        <tr>
                            <td align="center" colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnExport" runat="server" Text="Export To Excel" OnClick="btnExport_Click" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnGenerate" EventName="Click" />
                    <asp:PostBackTrigger ControlID="btnExport" />
                </Triggers>
            </asp:UpdatePanel>
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
          
	    <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>
     </div>
     </div>

    <div style="padding-left: 5px; padding-right: 5px; padding-top: 0px; width: 950">
        
        <div class="border center formbg" style="width: 950">
            
        </div>
    </div>
</asp:Content>
