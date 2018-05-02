<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    CodeBehind="frmAdmissionHome.aspx.cs" EnableEventValidation="false" Inherits="IQCare.Web.Admission.frmAdmissionHome" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="PatientWardAdmission.ascx" TagName="PatientWardAdmission" TagPrefix="paf" %>
<asp:Content ID="ctMain" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <%-- <script src="../Incl/quicksearch.js" type="text/javascript" defer="defer"></script>
    <script type="text/javascript">
        $(function () {
            $('.search_textbox').each(function (i) {
                $(this).quicksearch("[id*=gridAdmission] tr:not(:has(th))", {
                    'testQuery': function (query, txt, row) {
                        return $(row).children(":eq(" + i + ")").text().toLowerCase().indexOf(query[0].toLowerCase()) != -1;
                    }
                });
            });
        });

    </script>--%>
     <script type="text/javascript">
         function RegisterJQuery() {
             $('#textDischargeDate').datepicker({ autoclose: true });             
         }
         //Calling MyFunction when document is ready (Page loaded first time)
         $(document).ready(RegisterJQuery);
         //Calling MyFunction when the page is doing postback (asp.net)
         Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RegisterJQuery);
    </script>
   
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">Wards Admission</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
	<div class="row" align="center">
         <br />           
        </div>
         <asp:UpdatePanel runat="server" ID="divWardComponent">
            <ContentTemplate>
            
<div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group" align="center">
             <asp:Button ID="btnNewAdmission" runat="server" Text="New Admission" CssClass="btn btn-primary"
                                        Height="30px" Width="80%" Style="text-align: left;" />
                                    <label class="glyphicon glyphicon-plus-sign" style="margin-left: -15%; vertical-align: -webkit-baseline-middle;
                                        color: #fff;">
                                    </label>
             </div>
              <asp:Panel ID="panelFilter" runat="server" DefaultButton="btnFilter">
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">Wards:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlPatientWard" runat="server" Width="99%" CssClass="form-control" AutoPostBack="false"
                                        OnSelectedIndexChanged="SelectedWardChanged">
                                    </asp:DropDownList>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group text-nowrap text-right">
             <label  class="control-label">Show Discharged:</label>
             </div> 
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
             <asp:RadioButtonList
                                        ID="rblOption" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="radioButtonList">
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:RadioButtonList>
             </div> 
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            <asp:Button ID="btnFilter" runat="server" Text="View" OnClick="btnFilter_Click"
                                        CssClass="btn btn-primary" Height="30px" Width="50%" Style="text-align: left;" />
                                    <label class="fa fa-eye" style="margin-left: -15%; vertical-align: -webkit-baseline-middle;
                                        color: #fff;">
                                    </label>
             </div> 
             </asp:Panel>
 </div>
                
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="buttonDischarge" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        
<div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <asp:UpdatePanel runat="server" ID="divErrorCompenent" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:Panel ID="divError" runat="server" Style="padding: 5px" CssClass="background-color: #FFFFC0; border: solid 1px #C00000"
                                    HorizontalAlign="Left" Visible="true">
                                    <asp:Label ID="lblError" runat="server" Style="font-weight: bold; color: #800000"
                                        Text=""></asp:Label>
                                </asp:Panel>
                                <asp:HiddenField ID="HSelectedID" runat="server" />
                                <asp:HiddenField ID="HDate" runat="server" />
                                <asp:HiddenField ID="HStatus" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
             </div>            
 </div>
        
        <asp:UpdatePanel ID="divGridComponent" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
            <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
  <div id="divAdmissions" class="grid" style="width: 100%; padding-right:5px;">
                                    <div class="rounded">
                                       
                                            <div class="mid-inner">
                                                <div class="mid" style="height: 280px; overflow: auto">
                                                    <div id="div-AdmissionsGridview" class="GridView whitebg">
                                                        <asp:GridView ID="gridAdmission" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                            BorderColor="White" BorderWidth="1px" CellPadding="0" GridLines="None" CssClass="table table-bordered table-hover"
                                                            EmptyDataText="No Data to display" ShowHeaderWhenEmpty="True" Width="100%" BorderStyle="Solid"
                                                            DataKeyNames="AdmissionID,PatientID,WardID" OnRowCommand="gridAdmission_RowCommand"
                                                            OnRowDataBound="gridAdmission_RowDataBound" OnDataBound="gridAdmission_DataBound">
                                                            <Columns>
                                                                <asp:BoundField DataField="PatientName" HeaderText="Patient" />
                                                                <asp:BoundField DataField="PatientNumber" HeaderText="Patient #" />
                                                                <asp:BoundField DataField="WardName" HeaderText="Ward" />
                                                                <asp:BoundField DataField="BedNumber" HeaderText="Bed #" />
                                                                <asp:BoundField DataField="AdmissionDate" HeaderText="Date Admitted" DataFormatString="{0:dd-MMM-yyyy}" />
                                                                <asp:BoundField DataField="ReferredFrom" HeaderText="Referred From" />
                                                                <asp:BoundField HeaderText="Discharged?" DataField="Discharged" InsertVisible="False">
                                                                </asp:BoundField>
                                                                <asp:TemplateField InsertVisible="False" ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <div style="white-space: nowrap">
                                                                            <asp:Button ID="buttonDetails" runat="server" CausesValidation="false" CommandName="ViewEdit"
                                                                                Text="View/Edit" CommandArgument="<%# Container.DataItemIndex %>" CssClass="btn btn-primary" Height="30px" Width="35%" Style="text-align:left;"></asp:Button>
                                                                                 <label class="fa fa-eye" style="margin-left: -8%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
                                                                            <span style='display: <%# ShowDischarge(Eval("Discharged")) %>; white-space: nowrap'>
                                                                           
                                                                                <asp:Button ID="buttonDischarge" runat="server" CausesValidation="false" CommandName="Discharge"
                                                                                    Text="Discharge" CommandArgument="<%# Container.DataItemIndex %>" Visible="true"
                                                                                    CssClass="btn btn-primary" Height="30px" Width="35%" Style="text-align:left;" />
                                                                                    <label class="fa fa-sign-out" style="margin-left: -8%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
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
                <asp:AsyncPostBackTrigger ControlID="ddlPatientWard" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="gridAdmission" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="btnOkAction" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnFilter" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="buttonDischarge" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="divAdmitComponent" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <paf:PatientWardAdmission ID="AdmitPatient" runat="server" OpenMode="VIEW" />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="gridAdmission" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="btnOkAction" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="divDischargeComponent" runat="server">
            <ContentTemplate>
                <asp:Button ID="btnCal" runat="server" Style="display: none" />
                <asp:Panel ID="panelDischarge" runat="server" Style="display: none; width: 350px;
                    background-color: #fff" DefaultButton="buttonDischarge">
                   
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">
            &nbsp;<asp:Panel ID="divTitle" runat="server" Style="cursor: move;">Discharge Patient Form</asp:Panel>
            <h3>
            </h3>
        </h3>
          
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
	<div class="row" align="center">
         <br />           
        </div>
        <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group" align="right">
             <asp:Image ID="Image2" runat="server" ImageUrl="~/images/mb_information.gif" Height="32px"
                                    Width="32px" />
             </div>
             <div class="col-md-10 col-sm-12 col-xs-12 form-group">
              <div style="line-height: 30px; text-align: center; font-weight: bold;">
                                    Please note that this action cannot be reversed.
                                </div>
             </div>           
 </div>
 
<div class="row">
 <div class="col-md-5 col-sm-12 col-xs-12 form-group" align="right">
             <label class="control-label">Discharge Date:</label>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="textDischargeDate" clientidmode="Static"
                                                       maxlength="11" size="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
            
             </div>
            
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <div id="divAction" style="white-space: nowrap">
<asp:Button ID="buttonDischarge" runat="server" Text="Discharge" OnClick="buttonDischarge_Click" CssClass="btn btn-primary" Height="30px" Width="32%" Style="text-align:left;" />
<label class="fa fa-sign-out" style="margin-left: -7%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
<asp:Button ID="buttonCancel" runat="server" Text="Cancel"  CssClass="btn btn-primary" Height="30px" Width="32%" Style="text-align:left;" />
<label class="glyphicon glyphicon-remove" style="margin-left: -7%; vertical-align: sub; color: #fff;margin-right: 2%;""></label>
                                </div>
 </div>
 </div>
	 <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>
       </asp:Panel>
                <ajaxToolkit:ModalPopupExtender ID="modalDischarge" runat="server" TargetControlID="btnCal"
                    PopupControlID="panelDischarge" BackgroundCssClass="modalBackground" DropShadow="false"
                    PopupDragHandleControlID="divTitle" Enabled="True" DynamicServicePath="" BehaviorID="mpeBID32803"
                    CancelControlID="buttonCancel" Y="20">
                </ajaxToolkit:ModalPopupExtender>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="notificationPanel" runat="server">
            <ContentTemplate>
                <asp:Button ID="btn" runat="server" Style="display: none" />
                <asp:Panel ID="pnNotify" runat="server" Style="display: none; width: 460px; border: solid 1px #808080;
                    background-color: #E0E0E0; z-index: 15000">
                    <asp:Panel ID="pnPopup_Title" runat="server" Style="border: solid 1px #808080; margin: 0px 0px 0px 0px;
                        cursor: move; height: 18px">
                        <table class="table-condensed" style="width: 100%; height: 18px; background-color: #6699FF">
                            <tr>
                                <td style="width: 5px; height: 19px;">
                                </td>
                                <td style="width: 100%; height: 19px;">
                                    <span style="font-weight: bold; color: Black">
                                        <asp:Label ID="lblNotice" runat="server">Admission</asp:Label></span>
                                </td>
                                <td style="width: 5px; height: 19px;">
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <table class="table-condensed" style="width: 100%;">
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
                        <asp:Button ID="btnOkAction" runat="server" Text="Close" Width="120px" Style="border: solid 1px #808080;"
                            OnClick="btnOkAction_Click" />&nbsp;&nbsp;&nbsp;<asp:Button ID="btnExit" runat="server"
                                Text="Close" Width="80px" Style="border: solid 1px #808080; display: none" /></div>
                </asp:Panel>
                <ajaxToolkit:ModalPopupExtender ID="notifyPopupExtender" runat="server" TargetControlID="btn"
                    PopupControlID="pnNotify" BackgroundCssClass="modalBackground" DropShadow="false"
                    PopupDragHandleControlID="pnPopup_Title" Enabled="True" DynamicServicePath=""
                    OkControlID="btnOkAction">
                </ajaxToolkit:ModalPopupExtender>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="sProgress" runat="server" DisplayAfter="5">
            <ProgressTemplate>
                <div style="width: 100%; height: 100%; position: fixed; top: 0px; left: 0px; vertical-align: middle;
                    background-color: Gray; filter: alpha(opacity=50); opacity: 0.7; z-index: 120;
                    moz-opacity: 0.5; khtml-opacity: .5">
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
    
    
</asp:Content>
