<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PatientWardAdmission.ascx.cs"
    Inherits="IQCare.Web.Admission.PatientWardAdmission" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" %>
<link href="../Style/bootstrap-3.3.6-dist/css/bootstrap.css" rel="stylesheet" type="text/css" />
<link href="../Style/bootstrap-3.3.6-dist/css/bootstrap-theme.css" rel="stylesheet"
    type="text/css" />
<script src="../Style/bootstrap-3.3.6-dist/html5Shiv/html5shiv.js" type="text/javascript"></script>
 <script type="text/javascript">
     function RegisterJQuery() {
         $('#textAdmissionDate').datepicker({ autoclose: true });
         $('#textExpectedDOD').datepicker({ autoclose: true });
     }
     //Calling MyFunction when document is ready (Page loaded first time)
     $(document).ready(RegisterJQuery);
     //Calling MyFunction when the page is doing postback (asp.net)
     Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RegisterJQuery);
    </script>
    <style type="text/css">
     .modalBackground 
{
  background-color:#F5F5F5;
  filter: alpha(opacity=80);
  opacity: 0.80; 
}      
    </style>
<asp:Panel ID="panelPopup" runat="server" Style="display: none; width: 580px; border: solid 0px #3c8dbc;
        background-color: #fff;" Width="680px" DefaultButton="buttonAdmit">
    
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title"> 
        <asp:Panel ID="divTitle" runat="server" Style="cursor: move;">            
                Admission Form           
        </asp:Panel>	
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
             <table style="width: 100%" class="table-condensed">
                        <tbody>
                            <tr>
                                <td style="width: 170px; font-weight: bold">
                                    Patient Name:
                                </td>
                                <td style="width: 170">
                                    <asp:Label ID="lblname" runat="server"></asp:Label>
                                </td>
                                <td style="width: 380; white-space: nowrap" colspan="2">
                                    <b>Age:</b>&nbsp;&nbsp;&nbsp;<asp:Label ID="lblAge" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                    <b>Sex:</b> &nbsp;&nbsp;&nbsp;<asp:Label ID="lblSex" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-weight: bold">
                                    Patient Facility ID:
                                </td>
                                <td>
                                    <asp:Label ID="lblFacilityID" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </tbody>
                    </table>
             </div>
          
 </div>  
   <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
   <asp:Panel ID="panelError" runat="server" Style="padding: 5px" CssClass="background-color: #FFFFC0; border: solid 1px #C00000"
                        HorizontalAlign="Left" Visible="true">
                        <asp:Label ID="errorLabel" runat="server" Style="font-weight: bold; color: #800000"
                            Text=""></asp:Label>
                        <asp:HiddenField ID="HPatientID" runat="server" />
                        <asp:HiddenField ID="HAdmissionID" runat="server" Value="-1" />
                        <asp:HiddenField ID="HMode" runat="server" Value="New" />
                        <asp:HiddenField ID="HLocationID" runat="server" />
                        <asp:HiddenField ID="HUserID" runat="server" />
                        <asp:HiddenField ID="HPatientGender" runat="server" />
                        <asp:HiddenField ID="HPatientAge" runat="server" />
                    </asp:Panel>
 </div>
 </div>
 <div class="row" align="center">
 <hr />
 </div>
 <div class="row" style="padding-right:90px;">
 <div class="row">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">Select Ward:</label>
             <asp:Label ID="labelWard" runat="server" Visible="false" Font-Bold="true" />
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
              <span style="display: <% = sVid %>">
                        <asp:UpdatePanel ID="divWardPanel" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlPatientWard" runat="server" Width="98%" CssClass="form-control" AutoPostBack="true"
                                    OnSelectedIndexChanged="SelectedWardChanged">
                                </asp:DropDownList>
                                <asp:Label runat="server" ID="labelAvailablity" Font-Bold="true" ForeColor="Red"
                                    Style="text-align: right; margin-bottom: 10px;" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </span>
             </div>
             

 </div>
 <div class="row">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group text-right">
  <label class="control-label">Referred From:</label> 
 </div>
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
  <span style="color: #FF0000; display: <% = sVid %>">
                        <asp:DropDownList ID="ddlReferral" runat="server" Width="98%" CssClass="form-control" AutoPostBack="false"
                            AppendDataBoundItems="true">
                        </asp:DropDownList>
                    </span>
                     <asp:Label ID="labelReferred" runat="server" Visible="false" Font-Bold="true" />
 </div>
 </div>
 <div class="row" id="trOtherSource" style="display: none">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group text-right">
  <label class="control-label">Other Source:</label> 
 </div>
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
 <asp:TextBox ID="textReferral" runat="server" MaxLength="50" Width="98%" CssClass="form-control"></asp:TextBox>
 </div>
 </div>
 <div class="row">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group text-right">
  <label class="control-label">Bed Number:</label> 
 </div>
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
 <span style="color: #FF0000; display: <% = sVid %>">
                        <asp:TextBox ID="textBedNumber" runat="server" MaxLength="10" Width="98%" CssClass="form-control"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                            FilterType="Numbers, UppercaseLetters, LowercaseLetters,Custom" TargetControlID="textBedNumber"
                            ValidChars="-/\"></ajaxToolkit:FilteredTextBoxExtender>
                    </span>
                    <asp:Label ID="labelBedNumber" runat="server" Visible="false" Font-Bold="true" />
 </div>
 </div>
 <div class="row">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group text-right">
  <label class="control-label">Admission Date:</label> 
 </div>
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
  <span style="color: #FF0000; display: <% = sVid %>">
   <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="textAdmissionDate" clientidmode="Static"
                                                       maxlength="11" size="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>

                        
                    </span>
                    <asp:Label ID="labelAdmissionDate" runat="server" Visible="false" Font-Bold="true" />
 </div>
 </div>
 <div class="row">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group text-right">
  <label class="control-label"> <span style="display: <% = sVid %>"></span>Expected Discharge:</label> 
 </div>
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
  <span style="color: #FF0000; display: <% = sVid %>">
  <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="textExpectedDOD" clientidmode="Static"
                                                       maxlength="11" size="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
                       
                    </span>
                    <asp:Label ID="labelExpectedDOD" runat="server" Visible="false" Font-Bold="true" />
 </div>
 </div>
 <div class="row">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group text-right">
  <label class="control-label">
   <span style="display: <% = sVid %>"></span>Admitted By:
  </label> 
 </div>
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
 <label class="control-label"> <asp:Label ID="labelAdmittedBy" runat="server" Visible="true" /></label>
 </div>
 </div>
 <div class="row">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group text-right">
  <label class="control-label">
  <span style="display: <% = sVid %>"></span>Discharge
  </label> 
 </div>
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
 <label class="control-label"><asp:Label ID="labelDischarge" runat="server" Visible="true" /></label>
 </div>
 </div>
  <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
  <div id="divAction" style="white-space: nowrap; border: 0; text-align: center;">
<span style="display: <% = sVid %>">
<asp:Button ID="buttonAdmit" runat="server" Text="Save Admission" OnClick="buttonAdmit_Click" CssClass="btn btn-primary" Height="30px" Width="30%" Style="text-align:left;"  />
<label class="glyphicon glyphicon-floppy-disk" style="margin-left: -6%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
</span>
<asp:Button ID="buttonCancelAddWard" runat="server" Text="Cancel" OnClick="buttonCancelAddWard_Click"  CssClass="btn btn-primary" Height="30px" Width="20%" Style="text-align:left;" />
<label class="glyphicon glyphicon-remove" style="margin-left: -6%; vertical-align: sub; color: #fff;margin-right: 2%;""></label>
                    </div>
 </div>
 </div>
 </div>
   
    <asp:Button ID="buttonRaiseItemPopup" runat="server" Style="display: none" />
    <ajaxToolkit:ModalPopupExtender ID="AdmissionDialog" runat="server" TargetControlID="buttonRaiseItemPopup"
        PopupControlID="panelPopup" BackgroundCssClass="modalBackground" DropShadow="false"
        BehaviorID="admisionx572" PopupDragHandleControlID="divTitle" Enabled="True"
        CancelControlID="buttonCancelAddWard" DynamicServicePath="" Y="20">
    </ajaxToolkit:ModalPopupExtender>
	 <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>

   
      </asp:Panel>

