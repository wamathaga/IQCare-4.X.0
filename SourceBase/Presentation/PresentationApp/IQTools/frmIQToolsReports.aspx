<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    Inherits="Reports_frmIQToolsReports" Title="Untitled Page" CodeBehind="frmIQToolsReports.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"
        EnablePageMethods="true" />--%>
    <script language="javascript" type="text/javascript">

        //**********************************************
        function PrintReport() {
            var url = "PrintReport.aspx";
            //frmIQToolsQuery.RunReport();
            OpenNewWindow(url, "PrintReport");
        }
        //**********************************************    
        function OpenNewWindow(pageurl, pgname) {

            var w = screen.width - 60;
            var h = screen.height - 60;
            var winprops = "location=no,scrollbars=yes,resizable=yes,status=no";
            var frmwin = window.open(pageurl, pgname, winprops);

            if (parseInt(navigator.appVersion) >= 4) {
                frmwin.window.focus();
            }
        }
        function ShowParameterPopUP() {
            var modalPopupBehaviorCtrl = $find('programmaticModalPopupBehavior');
            modalPopupBehaviorCtrl.show();
        }
    
    </script>

    <div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">IQTools Reports</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
         <div class="row">
         <br />
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
              <asp:Panel ID="divError" runat="server" Style="padding: 5px" CssClass="background-color: #FFFFC0; border: solid 1px #C00000"
                HorizontalAlign="Left" Visible="false">
                <asp:Label ID="lblError" runat="server" Style="font-weight: bold; color: #800000"
                    Text=""></asp:Label>
            </asp:Panel>
             </div>
            
 </div>
  <asp:UpdatePanel ID="panelResult" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <div class="mid-outer">
                        <div class="mid-inner">
                            <div class="mid" style="border: 1px solid #666699;width:99.5%">
                                <div class="whitebg" id="div-gridview" style="width: 100%; overflow: auto; margin-top: 5px;
                                    margin-bottom: 5px; max-height: 480px; height: 280px">
                                    <asp:GridView ID="gridResult" runat="server" CellPadding="0" EnableModelValidation="True"
                                        BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" Width="100%" CssClass="table table-bordered table-hover"
                                        BorderColor="#99CCFF" EmptyDataText="There are no reports at the moment" AutoGenerateColumns="False"
                                        DataKeyNames="ReportID,FullFileName" OnRowCommand="gridResult_RowCommand" OnSelectedIndexChanged="gridResult_SelectedIndexChanged"
                                        PageSize="15" OnRowDataBound="gridResult_RowDataBound">
                                        <PagerSettings Mode="Numeric" Visible="true" />
                                        <Columns>
                                            <asp:CommandField ShowSelectButton="True" />
                                            <asp:BoundField DataField="ReportName" HeaderText="Name" ReadOnly="True" />
                                            <asp:BoundField DataField="HasTemplate" HeaderText="Has Template" ReadOnly="True" />
                                            <asp:ButtonField CommandName="RUN" Text="Run The Report" />
                                        </Columns>
                                        <HeaderStyle CssClass="searchresultfixedheader" Height="20px" HorizontalAlign="Left"
                                            Wrap="False" />
                                        <PagerStyle HorizontalAlign="Right" VerticalAlign="Top" />                                       
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
             </div>
   <asp:Button ID="btnRaisePopup" runat="server" Text="Save" Width="60px" Style="display: none" />  
 </div>
                    <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Panel ID="divParameters" runat="server" Style="display: none; width: 320px;
                        border: solid 1px #808080; background-color: #F0F0F0">
                        <asp:Panel ID="divTitle" runat="server" Style="border: solid 1px #808080; margin: 0px 0px 0px 0px;
                            cursor: move; height: 18px">
                             <div class="row">
                             <div class="col-md-12">
                              <label class="control-label">Specify the following values for filtering</label>
                             </div>
                             </div>                                
                        </asp:Panel>
                        <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Start Date</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:TextBox runat="server" ID="textDateFrom" Width="120px" CssClass="form-control"/>
                                        <ajaxToolkit:CalendarExtender runat="server" TargetControlID="textDateFrom" Format="dd-MMM-yyyy"
                                            ID="ceStartDate">
                                        </ajaxToolkit:CalendarExtender>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">End Date:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:TextBox runat="server" ID="textDateTo" Width="120px" CssClass="form-control"/>
                                        <ajaxToolkit:CalendarExtender runat="server" TargetControlID="textDateTo" Format="dd-MMM-yyyy"
                                            ID="ceEndDate">
                                        </ajaxToolkit:CalendarExtender>
             </div> 
              <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">CD4 Cutoff For ART:</label>
             </div>
              <div class="col-md-2 col-sm-12 col-xs-12 form-group">
              <asp:TextBox runat="server" ID="textCD4" Width="80px" Text="350" CssClass="form-control"/>    
                                        <ajaxToolkit:FilteredTextBoxExtender ID="fteCD4" runat="server" TargetControlID="textCD4"
                                                FilterType="Numbers"  />      
                                        <asp:HiddenField ID="hCutOffCD4" runat="server" Value="350" />    
             </div>
 </div>
                        <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Button ID="btnActionOK" runat="server" Text="Continue" CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
 <label class="glyphicon glyphicon-ok" style="vertical-align: middle; margin-left: -2.5%; margin-right: 2%; color: #fff;"></label>  
 <asp:Button ID="btnActionCancel" runat="server" Text="Cancel" CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
 <label class="glyphicon glyphicon-remove-circle" style="vertical-align: middle; margin-left: -2.5%; margin-right: 2%; color: #fff;"></label>  
             </div>
              
 </div>
                        
                    </asp:Panel>
 </div>
</div>

                   
                    
                    <ajaxToolkit:ModalPopupExtender ID="parameterPopup" runat="server" BehaviorID="programmaticModalPopupBehavior"
                        TargetControlID="btnRaisePopup" PopupControlID="divParameters" BackgroundCssClass="modalBackground"
                        CancelControlID="btnActionCancel" DropShadow="true" PopupDragHandleControlID="divTitle">
                    </ajaxToolkit:ModalPopupExtender>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="updReportTemplates" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="divData" runat="server" Style="width: 460px;" HorizontalAlign="Left"
                        Wrap="False" Visible="false">
                        <div class="row box-header">
         <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <h5 class="box-title">
         <asp:Label ID="lblTitle" runat="server" Text="Report Information"></asp:Label>
                                    <asp:HiddenField ID="HReport_ID" EnableViewState="true" runat="server" Value="" />
                                    <asp:HiddenField ID="HDateParams" EnableViewState="true" runat="server" Value="" />
         </h5>
         </div>             
            </div>
            <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Name:</label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
            <asp:TextBox ID="txtreportName" runat="server" Width="120" CssClass="form-control"></asp:TextBox>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             
             </div> 
 </div>
 <div class="row">
 <div class="col-md-12">
 <asp:Panel ID="pnlFileUpload" runat="server" Visible="false">
 <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Available Template:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:Button ID="DocumentTemplateLink" runat="server" Visible="false" CssClass="btn btn-primary" onclick="DocumentTemplateLink_Click" /> 
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label"><asp:Label ID="lblNoTemplateFile" runat="server">No template file available</asp:Label></label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
              <asp:Panel ID="panelAllowPrint" runat="server" Visible="false">
                                                        <br />
                                                        <input type="button" id="cmdPrint" value="Run The Report" onclick="ShowParameterPopUP();"
                                                            runat="server" style="cursor: pointer; border: solid 1px #FFFFFF; background-repeat: no-repeat;
                                                            padding-left: 25px; width: 120px; height: 30px; margin-left: 10px;" class="btn btn-primary" /></asp:Panel>
             </div> 
 </div>
 <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Upload the Template: only(xsl)</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:FileUpload ID="ctrlFileUpload" Width="280px" Height="19px" runat="server" />
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <asp:Button ID="cmdUpload" Width="140px" CssClass="btn btn-primary" Height="30px" runat="server" Text=" Save &amp; Upload"  OnClick="cmdUpload_Click" />
             </div>
             <div class="col-md-5 col-sm-12 col-xs-12 form-group">
             <asp:Button ID="buttonReportXML" runat="server" Text="Get ReportXML" onclick="buttonReportXML_Click" CssClass="btn btn-primary" Height="30px" Width="8%"/>
             <asp:Button ID="cmdTFileDelete" runat="server" Text="Remove Template File"  CssClass="btn btn-primary" Height="30px" Width="12%"/>
              <asp:Button ID="buttonClose" runat="server" Text="Close" onclick="buttonClose_Click"  CssClass="btn btn-primary" Height="30px" Width="8%"/>
             </div> 
 </div>
    <hr />
                                    </asp:Panel>
 </div>
 </div>
                        
                    </asp:Panel>
                    <!-- Template File Delete Confirmation Popup -->
                    <asp:Panel ID="divTFPopup" runat="server" Style="display: none; width: 320px; border: solid 1px #808080;
                        background-color: #E0E0E0;">
                        <asp:Panel ID="divTFPopup_Title" runat="server" Style="border: solid 1px #808080;
                            margin: 0px 0px 0px 0px; cursor: move; height: 18px">
                            <div class="row box-header">
         <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <h5 class="box-title">Remove Template File</h5>
         </div>             
            </div>

                            
                        </asp:Panel>
                        <div class="row">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group text-right">
             <img src="../Images/caution.png" alt="" width="32" height="32" />
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
            <label class="control-label">Would you really like to remove this template?</label>
             </div>
            
 </div>
                        <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
<asp:Button ID="cmdTFConfirmDelete" runat="server" Text="Delete" OnClick="cmdTFConfirmDelete_Click" CssClass="btn btn-primary" Height="30px" Width="8%"/>
<asp:Button ID="cmdTFConfirmCancel" runat="server" Text="Cancel" CssClass="btn btn-primary" Height="30px" Width="8%"/>
             </div>
             
             
              
 </div>
                        
                    </asp:Panel>
                    <ajaxToolkit:ModalPopupExtender ID="deleteTFModalPopupExtender" runat="server" TargetControlID="cmdTFileDelete"
                        PopupControlID="divTFPopup" BackgroundCssClass="modalBackground" CancelControlID="cmdTFConfirmCancel"
                        DropShadow="true" PopupDragHandleControlID="divTFPopup_Title" Enabled="True">
                    </ajaxToolkit:ModalPopupExtender>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="cmdUpload" />
                    <asp:PostBackTrigger ControlID="buttonReportXML" />
                    <asp:PostBackTrigger ControlID="DocumentTemplateLink" />
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

    
</asp:Content>
