<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true" Inherits="frmClinical_HIVCareARTCardEncounter"
    Title="Create HIV Care ART Card Encounter Form" Codebehind="frmClinical_HIVCareARTCardEncounter.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
<script type="text/javascript">
    function RegisterJQuery() {
        $('#txtVisitDate').datepicker({ autoclose: true });
        $('#txtFollowUpDate').datepicker({ autoclose: true });
        $('#txtTBRxStart').datepicker({ autoclose: true });
        $('#txtTBRxStop').datepicker({ autoclose: true });
        $('#txtARTEndeddate').datepicker({ autoclose: true }); 
    }
    //Calling RegisterJQuery when document is ready (Page loaded first time)
    $(document).ready(RegisterJQuery);
    //Calling RegisterJQuery when the page is doing postback (asp.net)
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RegisterJQuery);
</script>
    <br />
    <div style="padding-left: 8px; padding-right: 8px;">
        <script language="javascript" type="text/javascript">
            function WindowPrint() {
                window.print(); 
            }

            function WindowHistory() { 
                history.go(-1);
                return false;
            }
            function fnPageOpen(pageopen) {
                if (pageopen == "Pharmacy") {
                    window.open('../Pharmacy/frmPharmacyform.aspx?opento=ArtForm');
                }
                else if (pageopen == "Labratory") {
                    //                    window.open('../Laboratory/frmLabOrder.aspx?opento=ArtForm');
                    window.open('../Laboratory/frm_Laboratory.aspx?opento=ArtForm');
                }
                else if (pageopen == "LabTest") {
                    //                    window.open('../Laboratory/LabOrderForm.aspx?opento=ArtForm');
                    window.open('../Laboratory/frm_Laboratory.aspx?opento=ArtForm');
                }
            }
        </script>

        <div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">HIV Care/ART Encounter Form</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
         <div class="row">
         <br />
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <label class="required left" id="lblVisitDate" runat="server">Visit Date:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtVisitDate" clientidmode="Static"
                                                        runat="server" maxlength="11" Columns="8" data-date-format="dd-M-yyyy" style="width:110px;"/>
                                                </div>
                                            </div>
              
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
              <label>If Scheduled:</label><input id="chkifschedule" type="checkbox" runat="server" />
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label  class="control-label">Alternate Pick up Name:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtTreatmentSupporterName" Columns="23" MaxLength="23" runat="server" Width="250" CssClass="form-control"></asp:TextBox>
             </div>                        
    </div>
    <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Alternate Pick up Contact:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtTreatmentSupporterContact" Columns="23" MaxLength="23" runat="server" CssClass="form-control"></asp:TextBox>
             </div>
              <div class="col-md-1 col-sm-12 col-xs-12 form-group">
              </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label id="lblFollowUpDate" class="control-label" runat="server">Follow up Date:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtFollowUpDate" clientidmode="Static"
                                                       MaxLength="11" Columns="8"  runat="server" data-date-format="dd-M-yyyy" style="width:110px;"/>
                                                </div>
                                            </div>
            
             </div>                      
    </div>
    <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Duration Since:</label>
             </div>
              <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <label class="control-label">ART Start</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <div style="width: 100%; margin: 0px auto;">
<div style="float: left;margin-right:5px;"">
<asp:TextBox ID="txtARTStart" CssClass="form-control" Width="80" MaxLength="3" Columns="2" ReadOnly="true" runat="server"></asp:TextBox></div>
<div style="float: left"><span class="smalllabel">Months</span></div>
</div>
             </div>
           <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Starting Current Regimen</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <div style="width: 100%; margin: 0px auto;">
<div style="float: left;margin-right:5px;""><asp:TextBox ID="txtStartingCurrentRegimen" CssClass="form-control" MaxLength="3" Columns="2" Width="80"  ReadOnly="true" runat="server"></asp:TextBox></div>
<div style="float: left"><span class="smalllabel">Months</span></div>
</div>
             </div>                      
    </div>
    <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <h4>Clinical Status</h4>
             </div>
                             
    </div>
    <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
  <label id="lblWeight" runat="server" class="control-label">Weight:</label>           
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <div style="width: 100%; margin: 0px auto;">
<div style="float: left"><asp:TextBox ID="txtPhysWeight" runat="server" MaxLength="5" Columns="4" CssClass="form-control" Width="80"></asp:TextBox></div>
<div style="float: left"> <span class="smallerlabel">kg</span></div>
</div>
              
                                   
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label id="lblHeight" class="control-label" runat="server">Height:</label>           
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <div style="width: 100%; margin: 0px auto;">
<div style="float: left"><asp:TextBox ID="txtPhysHeight" runat="server" MaxLength="3" Columns="4" CssClass="form-control" Width="80"></asp:TextBox></div>
<div style="float: left"><span class="smallerlabel">cm</span></div>
 
                                    
</div>
             </div>                      
    </div>
    <div class="row" id="tdFamilyPlanning" runat="server">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Family Planning:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlFamilyPanningStatus" runat="server" CssClass="form-control" Width="250"> </asp:DropDownList>
             </div>
             <div class="col-md-5 col-sm-12 col-xs-12 form-group">
               <div class="divborder checkbox" id="divFamilyPlanningMethod" style="display: none">                              
                                <asp:Panel ID="PnlFamilyPlanningMethod" runat="server">
                                </asp:Panel>
                            </div>
             </div>
                               <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                               <div class="row" id="tdOedema" style="width: 30%; display: none"  runat="server">
                                <label>
                                        Oedema:</label>
                                    <asp:RadioButton ID="rdoOedemaPlus" GroupName="Oedema" runat="server" />
                                    <label>
                                        +</label>
                                    <asp:RadioButton ID="rdoOedemaMinus" GroupName="Oedema" runat="server" />
                                    <label>
                                        -</label>
                               </div>
                               </div>
    </div>
    <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group" id="tdPregnant" runat="server">
          <table width="100%" border="0">
                                <tr align="center">
                                    <td align="center" style="width: 100%; display: inline-table" runat="server">
                                        <label id="lblPregnant" runat="server">
                                            Pregnant:</label>
                                        <input id="rdoPregnantYes" onmouseup="up(this);" onfocus="up(this);" onclick="down(this); show('spanEDD');hide('spanEDDNopregnant'); "
                                            type="radio" value="Y" name="pregnant" runat="server">
                                        <label>
                                            Yes</label>
                                        <input id="rdoPregnantNo" onmouseup="up(this);" onfocus="up(this);" onclick="down(this); hide('spanEDD');show('spanEDDNopregnant');"
                                            type="radio" value="N" name="pregnant" runat="server">
                                        <label>
                                            No</label>
                                        <span id="spanEDD" style="display: none">
                                            <label id="lblEDD" class="margin20" for="EDD">
                                                EDD:</label>
                                            <input id="txtEDD" runat="server" maxlength="11" size="11" class="form-control" style="width:110px;">
                                            <img id="imgEDD" onclick="w_displayDatePicker('<%=txtEDD.ClientID%>');" height="22"
                                                alt="Date Helper" hspace="3" src="../images/cal_icon.gif" width="22" border="0" style="vertical-align: bottom; margin-bottom: 2px;">
                                            <span class="smallerlabel">(DD-MMM-YYYY)</span>
                                            <label id="lblGestation" for="Gestation">
                                                Gestation:</label>
                                            <asp:TextBox ID="txtGestation" runat="server" MaxLength="3" Columns="2" CssClass="form-control" style="width:70px;"></asp:TextBox>
                                            <span class="smalllabel">Weeks</span>
                                            <label id="lblPMTCTPregnant" for="PMTCTPregnant" class="margin20">
                                                PMTCT?</label>
                                            <input id="rdoPMTCT" onclick="toggle('spanpmctcancno');" type="checkbox" value="Y"
                                                name="rdoPMTCT" runat="server" />
                                            <span style="display: none" id="spanpmctcancno">
                                                <label id="lblpmctanc" for="pmctcanc" class="margin20">
                                                    PMTCT/ANC No:</label>
                                                <asp:TextBox ID="txtPMTCTANCNumber" runat="server" MaxLength="4" Columns="13" CssClass="form-control" style="width:70px;"></asp:TextBox>
                                            </span></span><span id="spanEDDNopregnant" style="display: none">
                                                <label id="lblDelivery" class="right10" for="DeliveryPregnantNo">
                                                    Delivery Date:</label>
                                                <asp:TextBox ID="txtDeliveryDate" MaxLength="11" Columns="8" runat="server" CssClass="form-control" style="width:110px;"></asp:TextBox>
                                                <img height="22" alt="Date Helper" onclick="w_displayDatePicker('<%=txtDeliveryDate.ClientID%>');"
                                                    hspace="5" src="../images/cal_icon.gif" width="22" border="0" style="vertical-align: bottom; margin-bottom: 2px;">
                                                <span class="smallerlabel">(DD-MMM-YYYY)</span>
                                                <label id="lblPostPartum" class="right10" for="PostPartum">
                                                    Post Partum:</label>
                                                <asp:CheckBox ID="cbPostPartum" runat="server" />
                                            </span>
                                    </td>
                                </tr>
                            </table>
             </div>
                            
    </div>
     <div class="row" id="tdMUACAge" runat="server" style="display: none">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
  <label>MUAC:</label>
  </div>
  <div class="col-md-4 col-sm-12 col-xs-12 form-group">
  <div style="width: 100%; margin: 0px auto;">
<div style="float: left"><asp:TextBox ID="txtMUAC" runat="server" MaxLength="4" Columns="4" CssClass="form-control" Width="60"></asp:TextBox></div>
<div style="float: left"><span class="smallerlabel">cm</span></div>
</div>
   
                                        
  </div>
  <div class="col-md-2 col-sm-12 col-xs-12 form-group">
  <label>Age:</label>
  </div>
  <div class="col-md-4 col-sm-12 col-xs-12 form-group">
  <div style="width: 100%; margin: 0px auto;">
<div style="float: left"><asp:TextBox ID="txtAge" runat="server" ReadOnly="true" MaxLength="4" Columns="3" CssClass="form-control" Width="60"></asp:TextBox></div>
<div style="float: left"><span class="smallerlabel">Months</span></div>
</div>
  
                                        
  </div>
 

 </div>
 <div class="row">

             <div class="col-md-2 col-sm-12 col-xs-12 form-group" style="display: inline">
             <label id="lblTBStatus" runat="server">TB Status:</label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlTBStatus" AutoPostBack="false" runat="server" CssClass="form-control" Width="130"></asp:DropDownList>
             </div>
                         
    </div>
    <div class="row">
    <div id="divTBStatusTBRX" style="display: none">
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label  class="control-label">TB Rx Start:</label>
             </div>    
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtTBRxStart" clientidmode="Static"
                                                       maxlength="8" size="8" name="txtTBRxStart" runat="server" data-date-format="dd-M-yyyy" style="width:110px;"/>
                                                </div>
                                            </div>             
             </div>
            
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label>TB Rx Stop:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtTBRxStop" clientidmode="Static"
                                                       maxlength="8" size="8" name="txtTBRxStop" runat="server" data-date-format="dd-M-yyyy" style="width:110px;"/>
                                                </div>
                                            </div>
             
             </div> 
              <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label"><label>TB Reg #:</label></label>
             </div> 
              <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <input id="txtTBRegNumber" maxlength="8" size="8" name="txtTBRegNumber" runat="server" class="form-control" style="width:100px;" />
             </div>
                     </div>
    </div>
    <div class="row">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Potential Side Effects:</label>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <label class="control-label">New OIs, Other Problems:</label>
             </div>
                                
    </div>
     <div class="row">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
            <div id="divPotentialSideEffect" class="divborder margin20" style="margin-top: 10;
                                margin-bottom: 10">
                                <div style="font-weight: normal">
                                    <asp:Panel ID="PnlPotentialSideEffect" runat="server">
                                    </asp:Panel>
                                </div>
                            </div>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
              <div class="divborder margin20" style="margin-top: 10; margin-bottom: 10">
                                <%--<table id="tblNewOIsProblemsOther" cellpadding="0" cellspacing="1" runat="server">
                                </table>--%>
                                <asp:Panel ID="PnlNewOIsProblemsOther" runat="server">
                                </asp:Panel>
                            </div>
             </div>
                                
    </div>
    <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
              <label class="margin20" id="lblNutritionalProblems" style="visibility: hidden" runat="server"> Nutritional Problems:</label>
             </div>
             <div class="col-md-9 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlNutritionalProblems" Style="visibility: hidden" runat="server" CssClass="form-control"> </asp:DropDownList>
             </div>
                                
    </div>
    <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            <label id="lblWABStage" runat="server">WAB Stage:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlWABStage" runat="server" Width="150" CssClass="form-control"></asp:DropDownList>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label id="lblWHOStage" runat="server">WHO Stage:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
              <asp:DropDownList ID="ddlWHOStage" runat="server" Width="150" CssClass="form-control"></asp:DropDownList>
             </div>                      
    </div>
     <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <h4>Pharmacy</h4>
             </div>
                             
    </div>
    <div class="row">
 <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <label class="control-label">CPT:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Adhere:</label>
             </div>
             <div class="col-md-7 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlCPTAdhere" runat="server" CssClass="form-control" Width="130"></asp:DropDownList>
             </div>
                                
    </div>
    <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label" id="lblARVDrugsAdhere" runat="server">ARV Drugs:</label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <label class="control-label"> Adhere:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlARVDrugsAdhere" runat="server" CssClass="form-control" Width="130"></asp:DropDownList>
             </div>
              <span id="divARVDrugsPoorFair" class="right15">
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label  class="control-label">Why Poor/Fair</label>
             </div>   
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlReasonARVDrugsPoorFair" Enabled="false" runat="server" CssClass="form-control" Width="380"> </asp:DropDownList>
             </div>
             
                  </span>        
    </div>
    <div class="row">
 <div class="col-md-5 col-sm-12 col-xs-12 form-group">
           
             </div>

             <div id="divReasonARVDrugsother" style="display: none">
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Other Reason:</label>
             </div> 
              <div class="col-md-5 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtReasonARVDrugsPoorFairOther" runat="server" MaxLength="10" Columns="10" CssClass="form-control" Width="380"></asp:TextBox>
             </div> 
                  </div>
             
                                   
    </div>
    <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label required">Substitution/Interruption:</label>
             </div>
             <div class="col-md-10 col-sm-12 col-xs-12 form-group">
              <asp:DropDownList ID="lstclinPlanFU" runat="server" Width="400" CssClass="form-control"></asp:DropDownList>
             </div>
                                
    </div>
    <div class="row">
     <div id="arvTherapyChange" style="display: none">
                                           
                                            
                                            
                                                <label class="required margin10" for="arvTherapyChangeCodeOtherName">
                                                    </label>
                                               
                                        

 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label required">Change Regimen Reason:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlArvTherapyChangeCode" runat="server" CssClass="form-control"></asp:DropDownList>
             </div>
             <div id="otherarvTherapyChangeCode" style="display: none">
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label required">Specify:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
              <input id="txtarvTherapyChangeCodeOtherName" maxlength="20" size="10" class="form-control" style="width:200px;" name="arvTherapyChangeCodeOtherName" runat="server"/>
             </div>   
             </div>                   
   </div> 
   </div>
   <div class="row">
   <div id="arvTherapyStop" style="display: none">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Date ART Ended</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtARTEndeddate" clientidmode="Static"
                                                        maxlength="11" size="10" name="txtARTEndeddate" runat="server" data-date-format="dd-M-yyyy" style="width:110px;"/>
                                                </div>
                                            </div>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label required">Stop Regimen Reason:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlArvTherapyStopCode" runat="server" CssClass="form-control"> </asp:DropDownList>
             </div> 
              <div id="otherarvTherapyStopCode" style="display: none">
              <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label  class="control-label required">Specify:</label>
             </div> 
              <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <input id="txtarvTherapyStopCodeOtherName" maxlength="20" size="10" name="arvTherapyStopCodeOtherName" class="form-control" style="width:200px;" runat="server"/>
             </div>  
            </div> </div>                    
    </div>
    <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <div id="divAdultPharmacy" runat="server">
                                <asp:Button ID="btnpharmacy" Text="Prescribe Drugs" runat="server" OnClick="btnpharmacy_Click" CssClass="btn btn-primary">
                                </asp:Button>
                            </div>
             </div>
            
                              
    </div>
       <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <h4>Investigations</h4>
             </div>
                             
    </div>
    <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
  <div id="divLaboratory" runat="server">
                                <asp:Button ID="btnLabratory" Text="Laboratory" runat="server" OnClick="btnLabratory_Click" CssClass="btn btn-primary" />
                            </div>
                            <div id="divLabOrderTest" runat="server">
                                <asp:Button ID="btnOrderLabTest" Text="Lab Order Test" runat="server" OnClick="btnOrderLabTest_Click"  CssClass="btn btn-primary"/>
                            </div>
 </div>
 </div>
  <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <h4>Referrals and Consultations</h4>
             </div>
                             
    </div>
 <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Referred To:</label>
             </div>
                               
    </div>
    <div class="row">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
            <asp:Panel ID="PnlReferredTo" runat="server"></asp:Panel>
             </div>            
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label"># of Days Hospitalized:</label>
             </div>   
              <div class="col-md-4 col-sm-12 col-xs-12 form-group">
            <asp:TextBox ID="txtNumOfDaysHospitalized" runat="server" MaxLength="4" Columns="4" CssClass="form-control" Width="60"></asp:TextBox>
             </div>                    
    </div>

    <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Nutritional Support:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlNutritionalSupport" runat="server" CssClass="form-control"></asp:DropDownList>
             </div>
              <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Attending Clinician:</label>
             </div>
              <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlAttendingClinician" runat="server" CssClass="form-control" Width="350"></asp:DropDownList>
             </div>
                               
    </div>

    <div class="row">
             <div id="tdInfantFeedingPractice" style="display: none" runat="server">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Infant Feeding Practice:</label>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlInfantFeedingPractice" runat="server" CssClass="form-control"></asp:DropDownList>
             </div>    
             </div>                   



	     <%--Main Content End--%>
         </div>
         <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <asp:Panel ID="pnlCustomList" Visible="false" runat="server" Height="100%" Width="100%"
                                Wrap="true">
                            </asp:Panel>
             </div>
                                 
    </div>
      <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Button ID="btnSave" Text="Save" OnClick="btnSave_Click" runat="server" CssClass="btn btn-primary" Width="8%" Height="30px" />
  <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -2%; margin-right: 2%;
                                vertical-align: sub; color: #fff;"></label>
                            <asp:Button ID="btnDataQualityCheck" Text="Data quality check" OnClick="btnDataQualityCheck_Click"
                                runat="server" CssClass="btn btn-primary" Width="15%" Height="30px"/>
                                 <label class="glyphicon glyphicon-ok" style="margin-left: -2%; margin-right: 2%;
                                vertical-align: sub; color: #fff;"></label>
                            <asp:Button ID="btnClose" Text="Close" OnClick="btnClose_Click" runat="server"  CssClass="btn btn-primary" Width="8%" Height="30px"/>
                             <label class="glyphicon glyphicon-remove" style="margin-left: -2%; margin-right: 2%;
                                vertical-align: sub; color: #fff;"></label>
                            <asp:Button ID="btnPrint" Text="Print" runat="server" OnClientClick="WindowPrint()"  CssClass="btn btn-primary" Width="8%" Height="30px"/>
                             <label class="glyphicon glyphicon-print" style="margin-left: -2%; margin-right: 2%;
                                vertical-align: sub; color: #fff;"></label>
 </div>
 </div>
      </div>
      </div>
      </div>
     </div>
     </div>
       
        
        
    </div>
</asp:Content>
