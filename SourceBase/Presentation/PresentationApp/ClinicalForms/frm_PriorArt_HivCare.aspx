<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master"
    EnableEventValidation="false"  AutoEventWireup="true" Inherits="ClinicalForms_frm_PriorArt_HivCare" Codebehind="frm_PriorArt_HivCare.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
<script type="text/javascript">
    function RegisterJQuery() {
        $('#txtPEPStartDate').datepicker({ autoclose: true });
        $('#txtPMTCTStartDate').datepicker({ autoclose: true });
        $('#txtHIVConfirmHIVPosDate').datepicker({ autoclose: true });
        $('#txtHIVEligibleDate').datepicker({ autoclose: true });
        $('#txtenrollmentDate').datepicker({ autoclose: true });
        $('#txtHIVReadyDate').datepicker({ autoclose: true });
        $('#txtDateAllergy').datepicker({ autoclose: true }); 
    }
    //Calling RegisterJQuery when document is ready (Page loaded first time)
    $(document).ready(RegisterJQuery);
    //Calling RegisterJQuery when the page is doing postback (asp.net)
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RegisterJQuery);
</script>
  
    <script language="Javascript" type="text/javascript">

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
      
    </script>

    <script language="javascript" type="text/javascript">

        function WindowPrint() {
            //            document.getElementById('divwhoto').className = '';
            //            document.getElementById('divPresenting').className = '';
            //            document.getElementById('divDrugAllergyMedicalAlr').className = '';
            //document.getElementById('divhivassociated').className='';
            window.print();
            //            document.getElementById('divwhoto').className = 'checkbox';
            //            document.getElementById('divPresenting').className = 'checkbox';
            //            document.getElementById('divDrugAllergyMedicalAlr').className = 'divBorder';
            //document.getElementById('divhivassociated').className='checkboxLeft';
        }


        function GetControl() {
            document.forms[0].submit();
        }

        function FocusValue() {
            window.scrollTo(0, 0);

            document.getElementById('<%=rdoDisclosureYes.ClientID%>').focus();
            //            alert('d');
            //            window.scrollTo(0, 0);
        }

        function ShowValue() {
            document.getElementById('Img1').disabled = true;
            document.getElementById('Img2').disabled = true;
            document.getElementById('Img6').disabled = true;
        }
        function CalEnbleDisble(a, b, c) {
            if (a == 0) {
                document.getElementById('Img1').disabled = true;
            }
            if (b == 0) {
                document.getElementById('Img2').disabled = true;
            }
            if (c == 0) {
                document.getElementById('Img6').disabled = true;
            }
        } 
    
    </script>

    <script language="javascript" type="text/javascript">
        function EnableDis(a) {
            var rdoVal = a;
            var ChkPEP = document.getElementById('<%=chkPEP.ClientID%>');
            var ChkPMTCT = document.getElementById('<%=chkPMTCTOnly.ClientID%>');
            var ChkEarlier = document.getElementById('<%=chkEarlierArvNotTransfer.ClientID%>');
            var PepWhere = document.getElementById('<%=txtPEPWhere.ClientID%>');
            var PepStartDt = document.getElementById('<%=txtPEPStartDate.ClientID%>');
            var Regimen = document.getElementById('<%=btnRegimen.ClientID%>');
            var PepARV = document.getElementById('<%=txtPEPARVs.ClientID%>');
            var PmtctWhere = document.getElementById('<%=txtPMTCTWhere.ClientID%>');
            var PmtctStartDt = document.getElementById('<%=txtPMTCTStartDate.ClientID%>');
            var Regimen1 = document.getElementById('<%=btnRegimen1.ClientID%>');
            var PmtctPepARV = document.getElementById('<%=txtPMTCTARVs.ClientID%>');
            var EarlierWhere = document.getElementById('<%=txtEarlierArvWhere.ClientID%>');
            var EarlierStartDt = document.getElementById('<%=txtEarlierArvStartDate.ClientID%>');
            var Regimen2 = document.getElementById('<%=btnRegimen2.ClientID%>');
            var EarlierARV = document.getElementById('<%=txtEarlierArvNotTransferArv.ClientID%>');
            if (rdoVal.value == "1") {
                ChkPEP.disabled = false;
                ChkPMTCT.disabled = false;
                ChkEarlier.disabled = false;
                PepWhere.value = '';
                PepARV.value = '';
                PepStartDt.value = '';
                PmtctWhere.value = '';
                PmtctStartDt.value = '';
                PmtctPepARV.value = '';
                EarlierWhere.value = '';
                EarlierStartDt.value = '';
                EarlierARV.value = '';
                PepStartDt.disabled = true;
                PepWhere.disabled = true;
                Regimen.disabled = true;
                PepARV.disabled = true;
                PmtctWhere.disabled = true;
                PmtctStartDt.disabled = true;
                EarlierStartDt.disabled = true;
                Regimen1.disabled = true;
                PmtctPepARV.disabled = true;
                EarlierWhere.disabled = true;
                Regimen2.disabled = true;
                EarlierARV.disabled = true;
                document.getElementById('Img1').disabled = true;
                document.getElementById('Img2').disabled = true;
                document.getElementById('Img6').disabled = true;
            }
            else {

                ChkPEP.checked = false;
                ChkPMTCT.checked = false;
                ChkEarlier.checked = false;
                document.getElementById('Img1').disabled = true;
                document.getElementById('Img2').disabled = true;
                document.getElementById('Img6').disabled = true;
                ChkPEP.disabled = true;
                ChkPMTCT.disabled = true;
                ChkEarlier.disabled = true;
                PepWhere.value = '';
                PepARV.value = '';
                PepStartDt.value = '';
                PmtctWhere.value = '';
                PmtctStartDt.value = '';
                PmtctPepARV.value = '';
                EarlierWhere.value = '';
                EarlierARV.value = '';
                EarlierStartDt.value = '';
                PepStartDt.disabled = true;
                PepWhere.disabled = true;
                Regimen.disabled = true;
                PepARV.disabled = true;
                PmtctWhere.disabled = true;
                PmtctStartDt.disabled = true;
                EarlierStartDt.disabled = true;
                Regimen1.disabled = true;
                PmtctPepARV.disabled = true;
                EarlierWhere.disabled = true;
                Regimen2.disabled = true;
                EarlierARV.disabled = true;
            }
        }
        function PriorArvPEP(id) {
            var PepWhere = document.getElementById('<%=txtPEPWhere.ClientID%>');
            var Regimen = document.getElementById('<%=btnRegimen.ClientID%>');
            var PepARV = document.getElementById('<%=txtPEPARVs.ClientID%>');
            var PepStartDt = document.getElementById('<%=txtPEPStartDate.ClientID%>');

            var pepChk = id;
            if (pepChk.checked == true) {
                PepStartDt.disabled = false;
                PepWhere.disabled = false;
                Regimen.disabled = false;
                PepARV.disabled = false;
                document.getElementById('Img1').disabled = false; 
            }
            else {
                PepStartDt.value = '';
                PepWhere.value = '';
                PepARV.value = '';
                PepStartDt.value = '';
                PepWhere.disabled = true;
                Regimen.disabled = true;
                PepARV.disabled = true;
                PepStartDt.disabled = true;
                document.getElementById('Img1').disabled = true;
            }
        }
        function PriorArvPMTCTOnly(id) {
            var PmtctWhere = document.getElementById('<%=txtPMTCTWhere.ClientID%>');
            var Regimen1 = document.getElementById('<%=btnRegimen1.ClientID%>');
            var PmtctPepARV = document.getElementById('<%=txtPMTCTARVs.ClientID%>');
            var PmtctStartDT = document.getElementById('<%=txtPMTCTStartDate.ClientID%>');
            var PmtctChk = id;
            if (PmtctChk.checked == true) {
                PmtctWhere.disabled = false;
                Regimen1.disabled = false;
                PmtctPepARV.disabled = false;
                PmtctStartDT.disabled = false;
                document.getElementById('Img2').disabled = false;
            }
            else {
                PmtctWhere.value = '';
                PmtctPepARV.value = '';
                PmtctStartDT.value = '';
                PmtctWhere.disabled = true;
                Regimen1.disabled = true;
                PmtctPepARV.disabled = true;
                PmtctStartDT.disabled = true;
                document.getElementById('Img2').disabled = true;
            }
        }
        function PriorArvEarlierArv(id) {
            var EarlierWhere = document.getElementById('<%=txtEarlierArvWhere.ClientID%>');
            var Regimen2 = document.getElementById('<%=btnRegimen2.ClientID%>');
            var EarlierARV = document.getElementById('<%=txtEarlierArvNotTransferArv.ClientID%>');
            var EarlierStartDT = document.getElementById('<%=txtEarlierArvStartDate.ClientID%>');
            var EarlierChk = id;
            if (EarlierChk.checked == true) {
                EarlierWhere.disabled = false;
                Regimen2.disabled = false;
                EarlierARV.disabled = false;
                EarlierStartDT.disabled = false;
                document.getElementById('Img6').disabled = false;
            }
            else {

                EarlierWhere.value = '';
                EarlierARV.value = '';
                EarlierStartDT.value = '';
                EarlierWhere.disabled = true;
                Regimen2.disabled = true;
                EarlierARV.disabled = true;
                EarlierStartDT.disabled = true;
                document.getElementById('Img6').disabled = true;
            }
        }
    </script>
    <div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">Prior ART/HIV Care</h3>
       </div>
        <!-- /.box-header -->
        <!-- DAL: using tables for form layout. Note that there are classes on labels and td. For custom fields, just use the 2 column layout, if there is an uneven number of cells, set last cell colspan="2" and align="center". Probably should talk through this -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
         <div class="row">
         <br />
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <h4>Prior ART</h4>
             </div>
                             
    </div>
    <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Prior ART:</label>
             </div>
              <div class="col-md-10 col-sm-12 col-xs-12 form-group">
            <label>
                        </label>
                    <input id="rdoDisclosureYes" onmouseup="up(this);" onfocus="up(this);" type="radio" value="1" name="disclosure"
                        onclick=" EnableDis(this)" runat="server" />&nbsp;
                    <label>
                        Yes</label>
                    <input id="rdoDisclosureNo" onmouseup="up(this);" onfocus="up(this);" type="radio" value="0" name="disclosure"
                        onclick="EnableDis(this)" runat="server" />
                    <label>
                        No</label>
             </div>
                             
    </div>
    <div class="row">
     <div class="col-xs-2 col-md-3">
     <%--left--%>
     <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <label>PEP:</label> <input type="checkbox" onclick="PriorArvPEP(this);" id="chkPEP" runat="server" />
 </div>
 
 </div>
 <div class="row">
 <br />
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <label> PMTCT Only:</label> <input type="checkbox" id="chkPMTCTOnly" onclick="PriorArvPMTCTOnly(this);" runat="server" />
 </div>

 </div>
 <div class="row">
 <br />
 <div class="col-md-12 col-sm-12 col-xs-12 form-group"> 
<label>Earlier ARV Not Transfer:</label>
<input type="checkbox" id="chkEarlierArvNotTransfer" onclick="PriorArvEarlierArv(this);" runat="server" />

</div>
 </div>
     </div>
       <div class="col-xs-10 col-md-9">
       <%--right--%>
       <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Start Date:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtPEPStartDate" clientidmode="Static"
                                                        maxlength="11" size="11" name="pharmOrderedbyDate" runat="server" disabled="disabled" data-date-format="dd-M-yyyy" style="width:110px;"/>
                                                </div>
                                            </div>

             
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <div style="width: 100%; margin: 0px auto;">
<div style="float: left;margin-right:5px;"><label class="control-label">Where:</label></div>
<div style="float: left"><asp:TextBox ID="txtPEPWhere" Enabled="false" runat="server" MaxLength="50" CssClass="form-control" Width="210"></asp:TextBox></div>

</div>
             
             
             </div>
            
              <div class="col-md-3 col-sm-12 col-xs-12 form-group">
              <div style="width: 100%; margin: 0px auto;">
<div style="float: left;margin-right:5px;"><label class="control-label">ARVs:</label></div>
<div style="float: left;margin-right:5px;">
<input id="txtPEPARVs" size="16" name="currentART" readonly="readonly" runat="server" disabled="disabled" class="form-control" style="width:110px;" /></div>
<div style="float: left"><asp:Button ID="btnRegimen" Enabled="false" runat="server" Text="..." OnClick="btnRegimen_Click" CssClass="btn btn-primary" Width="40" /></div>
</div>
             
             </div>
             
                              
    </div>
    <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Start Date:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtPMTCTStartDate" clientidmode="Static"
                                                     maxlength="11" size="11" name="pharmOrderedbyDate" runat="server" disabled="disabled" data-date-format="dd-M-yyyy" style="width:110px;"/>
                                                </div>
                                            </div>

             
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <div style="width: 100%; margin: 0px auto;">
<div style="float: left;margin-right:5px;">
<label class="control-label">Where:</label></div>
<div style="float: left">
<asp:TextBox ID="txtPMTCTWhere" Enabled="false" MaxLength="50" runat="server" CssClass="form-control" Width="210"></asp:TextBox>
</div>

</div>
             
             
             </div>
            
              <div class="col-md-3 col-sm-12 col-xs-12 form-group">
              <div style="width: 100%; margin: 0px auto;">
<div style="float: left;margin-right:5px;"><label class="control-label">ARVs:</label></div>
<div style="float: left;margin-right:5px;">
<input id="txtPMTCTARVs" size="15" name="prevARVRegimen1Name" readonly="readonly" disabled="disabled" runat="server" class="form-control" style="width:110px;" />
</div>
<div style="float: left">
<asp:Button ID="btnRegimen1" Enabled="false" runat="server" Text="..." OnClick="btnRegimen1_Click" CssClass="btn btn-primary" Width="40" />
</div>
</div>
             
             </div>
             
                              
    </div>
    <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Start Date:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtEarlierArvStartDate" clientidmode="Static"
                                                     maxlength="11" size="11" name="pharmOrderedbyDate" runat="server" disabled="disabled" data-date-format="dd-M-yyyy" style="width:110px;"/>                                                     
                                                </div>
                                            </div>

             
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <div style="width: 100%; margin: 0px auto;">
<div style="float: left;margin-right:5px;">
<label class="control-label">Where:</label></div>
<div style="float: left">
<asp:TextBox ID="txtEarlierArvWhere" Enabled="false" MaxLength="50" runat="server" CssClass="form-control" Width="210"></asp:TextBox>
</div>

</div>
             
             
             </div>
            
              <div class="col-md-3 col-sm-12 col-xs-12 form-group">
              <div style="width: 100%; margin: 0px auto;">
<div style="float: left;margin-right:5px;"><label class="control-label">ARVs:</label></div>
<div style="float: left;margin-right:5px;">
<input id="txtEarlierArvNotTransferArv" size="15" name="prevARVRegimen2Name" readonly="readonly" disabled="disabled" runat="server" class="form-control" style="width:110px;" />
</div>
<div style="float: left">
<asp:Button ID="btnRegimen2" runat="server" Enabled="false" Text="..." OnClick="btnRegimen2_Click" CssClass="btn btn-primary" Width="40"/>
</div>
</div>
             
             </div>
             
                              
    </div>

     </div>
    </div>
    <div class="row">        
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <h4>HIV Care</h4>
             </div>
                             
    </div>
    <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <label class="control-label" id="lblHIVConfirmHIVPosDate" runat="server">Date Confirmed HIV Positive:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtHIVConfirmHIVPosDate" clientidmode="Static"
                                                        maxlength="11" size="11" name="pharmOrderedbyDate" runat="server" data-date-format="dd-M-yyyy" style="width:110px;"/>
                                                </div>
                                            </div>
             
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Test Type:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlHivTestType" runat="server" Width="110px" Enabled="false" CssClass="form-control" Style="z-index: 2">
                                    </asp:DropDownList>
             </div>  
              <div class="col-md-3 col-sm-12 col-xs-12 form-group">
              <div style="width: 100%; margin: 0px auto;">
<div style="float: left;margin-right:5px;"><label class="control-label">Where:</label></div>
<div style="float: left"><asp:TextBox ID="txtHIVCareWhere" MaxLength="50" runat="server" CssClass="form-control" Width="200"></asp:TextBox></div>
</div>
              
              
             </div> 
                              
    </div>
    <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Date Eligible for ART:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtHIVEligibleDate" clientidmode="Static"
                                                     maxlength="11" size="11" name="pharmOrderedbyDate" runat="server" disabled="disabled" data-date-format="dd-M-yyyy" style="width:110px;"/>                                                    
                                                </div>
                                            </div>

             
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Clinical Stage:</label>
             </div>
            
              <div class="col-md-2 col-sm-12 col-xs-12 form-group">
              <asp:DropDownList ID="ddlHIVClincialWHOStage" runat="server" Width="110px" Style="z-index: 2" CssClass="form-control"> </asp:DropDownList>
             
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <div style="width: 100%; margin: 0px auto;">
<div style="float: left;margin-right:5px;"> <label class="control-label"> CD4:</label></div>
<div style="float: left;margin-right:15px;">
<asp:TextBox ID="txtHIVPrevARVsCD4" onkeypress="return isNumberKey(event)" MaxLength="4" Width="60" runat="server" CssClass="form-control"></asp:TextBox></div>
<div style="float: left;margin-right:5px;">
 <label class="control-label">CD4 %:</label>
</div>
<div style="float: left">
<asp:TextBox ID="txtHIVPrevARVsCD4Percent" MaxLength="4" onkeypress="return isNumberKey(event)" Width="60" runat="server" CssClass="form-control"></asp:TextBox>
</div>
</div>
             </div>
                              
    </div>
    <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Date HIV Enrolled:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtenrollmentDate" clientidmode="Static"
                                                        maxlength="11" size="11" name="pharmReportedbyDate" runat="server" data-date-format="dd-M-yyyy" style="width:110px;"/>
                                                </div>
                                            </div>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label"> HIV care Transfer in From:</label>
             </div>
             <div class="col-md-5 col-sm-12 col-xs-12 form-group">
              <asp:DropDownList ID="ddlarttransferinfrom" runat="Server" CssClass="form-control" Width="430"> </asp:DropDownList> 
             </div> 
                                  
    </div>
    <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Eligible and Ready:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtHIVReadyDate" clientidmode="Static"
                                                       maxlength="11" size="11" name="pharmOrderedbyDate" runat="server" data-date-format="dd-M-yyyy" style="width:110px;"/>
                                                </div>
                                            </div>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Presumptive HIV diagnosis:</label><input type="checkbox" id="chkHIVPresumptiveDiagnosis" runat="server" />
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label  class="control-label">PCR-infant:</label><input type="checkbox" id="chkHIVPcrInfant" runat="server" />
             </div>                      
    </div>
    <div class="row">        
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <h4>Drug Allergies and Relevant Medical Conditions</h4>
             </div>
                             
    </div>
    <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label required">Drug Allergy:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtDrugAllergies" MaxLength="50" runat="server" CssClass="form-control" Width="220"></asp:TextBox>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label required">Type of Reaction:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtTypeOfReaction" MaxLength="50" runat="server" CssClass="form-control" Width="220"></asp:TextBox>
             </div>                      
    </div>
    <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Date of Allergy:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtDateAllergy" clientidmode="Static"
                                                        maxlength="11" size="11" name="pharmReportedbyDate" runat="server" data-date-format="dd-M-yyyy" style="width:110px;"/>
                                                </div>
                                            </div>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Relevant Medical Conditions:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtRelevantMedicalCondition" MaxLength="150" TextMode="MultiLine" CssClass="form-control" Width="250" runat="server" Style="resize:none;"></asp:TextBox>
             </div>                      
    </div>
     <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Button ID="btnAddAllergy" runat="server" Text="Add Allergy" OnClick="btnAddAllergy_Click" CssClass="btn btn-primary" Width="10%" Height="30px"  />
  <label class="glyphicon glyphicon-plus" style="margin-left: -2%; margin-right: 2%;
                                vertical-align: sub; color: #fff;"></label>
 </div>
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <div class="grid" id="divDrugAllergyMedicalAlr" style="width:100%;">
                            <div class="rounded" style="padding-right:5px;">
                              <div class="top-outer">
                                        <div class="top-inner">
                                            <div class="top">
                                                <h2>
                                                    <center>
                                                       </center>
                                                </h2>
                                            </div>
                                        </div>
                                    </div>
                                 <div class="mid-outer">
                                  <div class="mid-inner">
                                   <div class="mid" style="height: 100px; overflow: auto">
                                <div id="div-gridview" class="gridview whitebg" style="height: 100">
                                    <asp:GridView Height="50px" ID="gvDrugAllergiesMedicalCondition" runat="server" AutoGenerateColumns="False"
                                        Width="100%" AllowSorting="True" BorderWidth="0" GridLines="None" CssClass="table table-bordered table-hover"
                                        CellPadding="0" CellSpacing="0" OnSorting="gvDrugAllergiesMedicalCondition_Sorting"
                                        HeaderStyle-HorizontalAlign="Left" RowStyle-CssClass="row"
                                        OnRowDataBound="gvDrugAllergiesMedicalCondition_RowDataBound" OnRowDeleting="gvDrugAllergiesMedicalCondition_RowDeleting"
                                        OnSelectedIndexChanging="gvDrugAllergiesMedicalCondition_SelectedIndexChanging">
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
 </div>
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Panel ID="pnlCustomList" Visible="false" runat="server" Height="100%" Width="100%" Wrap="true">
                        </asp:Panel>
 </div>
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnsave_Click" CssClass="btn btn-primary" Width="8%" Height="30px" />
 <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -2%; margin-right: 2%;
                                vertical-align: sub; color: #fff;"></label>
                        <asp:Button ID="btncomplete" runat="server" Text="Data Quality Check" OnClick="btncomplete_Click" CssClass="btn btn-primary" Width="15%" Height="30px" />
                         <label class="glyphicon glyphicon-ok" style="margin-left: -2%; margin-right: 2%;
                                vertical-align: sub; color: #fff;"></label>
                        <asp:Button ID="btnback" runat="server" Text="Close" OnClick="btnback_Click" CssClass="btn btn-primary" Width="8%" Height="30px" />
                         <label class="glyphicon glyphicon-remove" style="margin-left: -2%; margin-right: 2%;
                                vertical-align: sub; color: #fff;"></label>
                        <asp:Button ID="btnPrint" Text="Print" runat="server" OnClientClick="WindowPrint()"
                            OnClick="btnPrint_Click" CssClass="btn btn-primary" Width="8%" Height="30px"/>
                             <label class="glyphicon glyphicon-print" style="margin-left: -2%; margin-right: 2%;
                                vertical-align: sub; color: #fff;"></label>
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
