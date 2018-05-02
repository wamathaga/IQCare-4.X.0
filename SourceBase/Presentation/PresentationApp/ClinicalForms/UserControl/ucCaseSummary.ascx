<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucCaseSummary.ascx.cs" Inherits="PresentationApp.ClinicalForms.UserControl.ucCaseSummary" %>
<script type="text/javascript" language="javascript">
    function BacktoHome() {
        window.location.href = "../../ClinicalForms/frmPatient_Home.aspx";
        return false;
    }
</script>
<div class="box box-default box-solid" id="divHIVCare">
    <div class="box-header with-border">
        <h3 class="box-title">
            Case summary form</h3>
        <div class="box-tools pull-right">
            <%--<button type="button" class="btn btn-box-tool" data-widget="collapse">
                <i id="divHIVCareI" class="fa fa-minus"></i>
            </button>--%>
        </div>
    </div>
    <!-- /.box-header -->
    <div class="box-body">
    <div id="div_print">
    <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Name of Facility :</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                 <asp:Label ID="lblFacility" runat="server" Text="Label"></asp:Label>
             </div>
              <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">MFL Code :</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
               <label id="lblMFLName"></label>            
             </div>

    </div>
    <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Patient CCC no. :</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <%--<label for="lblPatientEnrollmentID"></label>--%>
           <label id="lblPatientEnrollmentID"></label>
             </div>
              <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Date</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
           <div class="input-group date">
            <div class="input-group-addon">
                <i class="fa fa-calendar"></i>
            </div>
            <input type="text" class="form-control" id="dtFormFillingDate" data-date-format="dd-M-yyyy"
                style="padding: 13px 12px;">
        </div>
             </div>

    </div>
    <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Patient Details :</label>
             </div>
             <div class="col-md-10 col-sm-12 col-xs-12 form-group">
            <%--Sub Row start--%>
            <div class="row">
            <div class="col-md-4 col-sm-12 col-xs-12">
            <label class="control-label">Date of Birth (Enrolment Date) :</label>
            </div>
            <div class="col-md-8 col-sm-12 col-xs-12">
            <label id="lblDateofBirth"></label>
            </div>
            </div>
             <div class="row">
              <div class="col-md-2 col-sm-12 col-xs-12">
             <label class="control-label">Gender</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12">
              <label id="lblGender"></label>
             </div>
               <div class="col-md-3 col-sm-12 col-xs-12">
             <label class="control-label">Current Weight (Kg) :</label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12">
             <label id="lblCurrentWt"></label>
             </div>
               <div class="col-md-3 col-sm-12 col-xs-12">
             <label class="control-label">Height (cm) :</label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12">
             <label id="lblHeight"></label>
             </div>

             </div>
             <%--Sub Row end--%>
             </div>
              
    </div>
    <div class="row">
    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Clinician’s Name :</label>
             </div>
             <div class="col-md-9 col-sm-12 col-xs-12 form-group">
            <%--<label id="lblCliniciansName"></label>--%>
             <input type="text" class="form-control" id="txtCliniciansName" placeholder="Free Text">
             </div>
    </div>
    <div class="row">
    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Facility Contacts :</label>
             </div>
             <div class="col-md-9 col-sm-12 col-xs-12 form-group">
             <%--Sub Row start--%>
             <div class="row">
              <div class="col-md-1 col-sm-12 col-xs-12">
             <label class="control-label">Tel :</label>
             </div>
              <div class="col-md-3 col-sm-12 col-xs-12">
              <input type="text" class="form-control" id="txtTel" placeholder="Free Text">
              <%--<label id="lblTel"></label>--%>
              </div>
               <div class="col-md-1 col-sm-12 col-xs-12">
             <label class="control-label">Email :</label>
             </div>
              <div class="col-md-4 col-sm-12 col-xs-12">
               <input type="text" class="form-control" id="txtEmail" placeholder="Free Text" onblur="EmailValidation()">
             <%-- <label id="lblEmail"></label>--%>
              </div>
             </div>
              <%--Sub Row end--%>
             </div>
    </div>
    <div class="row">
     <div class="col-md-12 col-sm-12 col-xs-12 form-group">
      <label class="control-label">What is the primary reason for this consultation :</label>
     </div>
    </div>
     <div class="row">
     <div class="col-md-12 col-sm-12 col-xs-12 form-group">
     <textarea class="form-control" rows="4" placeholder="Free Text"
                                    id="txtPrimaryReason" maxlength="1000" style="resize: none;"></textarea>
     </div>
    </div>
     <div class="row">
     <div class="col-md-12 col-sm-12 col-xs-12 form-group">
      <label class="control-label">Clinical Evaluation: history, physical, diagnostics, working diagnosis (excluding the information in the table below) :</label>
     </div>
    </div>
     <div class="row">
     <div class="col-md-12 col-sm-12 col-xs-12 form-group">
      <textarea class="form-control" rows="4" placeholder="Free Text"
                                    id="txtClinicalEvaluation" maxlength="1000" style="resize: none;"></textarea>
     </div>
    </div>
    <div class="row">
     <div class="col-md-12 col-sm-12 col-xs-12 form-group">
      <label class="control-label">Complete the table below chronologically, including all ART regimens and laboratory results (and any previous history available for transfer-in patients) :</label>
     </div>
    </div>
     
     <div class="row">
     <div class="col-md-12 col-sm-12 col-xs-12 form-group">   
     <h4>Lab Results</h4>
     <table id="dtlLabResults" class="table table-bordered table-striped">       
                                <thead>
                                    <tr>
                                        <th style="width: 20%;">
                                            Date
                                        </th>
                                        <th style="width: 20%;">
                                            CD4
                                        </th>
                                        <th style="width: 20%;">
                                            HB
                                        </th>
                                        <th style="width: 20%;">
                                            CrCl / eGFR
                                        </th>
                                        <th style="width: 20%;">
                                        Viral Load
                                        </th>
                                    </tr>
                                </thead>
                            </table>
                                                                           
     </div>
    </div>
    <div class="row">
     <div class="col-md-12 col-sm-12 col-xs-12 form-group">   
     <h4>Regimen Changes </h4>
      <div class="row">
     <div class="col-md-12 col-sm-12 col-xs-12 form-group" align="center">  
      <b>Current Regimen</b> : <label id="lblCurrentRegimen"></label>
     </div>
     </div>
     <table id="dtlRegimenChanges" class="table table-bordered table-striped">       
                                <thead>
                                    <tr>
                                        <th style="width: 20%;">
                                            Date
                                        </th>
                                        <th style="width: 40%;">
                                            Regimen
                                        </th>
                                        <th style="width: 40%;">
                                            Switch
                                        </th>
                                       
                                    </tr>
                                </thead>
                            </table>
                                                                           
     </div>
    </div>
    <div class="row">
     <div class="col-md-6 col-sm-12 col-xs-12 form-group">   
     <h4>OIS</h4>
     <table id="dtlOIS" class="table table-bordered table-striped">       
                                <thead>
                                    <tr>
                                        <th style="width: 50%;">
                                            Date
                                        </th>
                                        <th style="width: 50%;">
                                            OIS
                                        </th>
                                       
                                       
                                    </tr>
                                </thead>
                            </table>
                                                                           
     </div>
     <div class="col-md-6 col-sm-12 col-xs-12 form-group">   
    <h4>Condition</h4>
     <table id="dtlCondition" class="table table-bordered table-striped">       
                                <thead>
                                    <tr>
                                        <th style="width: 50%;">
                                            Date
                                        </th>
                                        <th style="width: 50%;">
                                            Condition
                                        </th>
                                       
                                       
                                    </tr>
                                </thead>
                            </table>
                                                                           
     </div>
    </div>
    <div class="row">
     <div class="col-md-12 col-sm-12 col-xs-12 form-group">   
     <h4>Anthropoemetric Measurements</h4>
     <table id="dtlAnthropoemetric" class="table table-bordered table-striped">       
                                <thead>
                                    <tr>
                                        <th style="width: 25%;">
                                            Date
                                        </th>
                                        <th style="width: 25%;">
                                            Weight
                                        </th>
                                        <th style="width: 25%;">
                                            Height
                                        </th>
                                        <th style="width: 25%;">
                                            BMI/BMI Z Score
                                        </th>
                                        
                                    </tr>
                                </thead>
                            </table>
                                                                           
     </div>
    </div>
   </div>
    <div class="row no-print" align="center" style="margin-bottom: 10px;">
        <div class="form-group">
            <div class="col-md-12" align="center">
                <button type="button" class="btn btn-primary" runat="server"
                        id="btnSave" clientidmode="Static" onclick="SaveUpdateClinicalSummaryData('tab_2');">
                    Save<span class="glyphicon glyphicon-floppy-disk" style="padding-left: 5px;"></span></button>

                    <button type="button" class="btn btn-primary" runat="server"
                        id="btnPrint" clientidmode="Static" onclick="WindowPrint()">
                    Print<span class="glyphicon glyphicon-print" style="padding-left: 5px;"></span></button>

                    <button type="button" class="btn btn-primary" runat="server"
                        id="btnClose" clientidmode="Static" onclick="BacktoHome()">
                    Close<span class="glyphicon glyphicon-remove" style="padding-left: 5px;"></span></button>
            </div>
        </div>
    </div>
   <%--printdiv('div_print');--%>
        <%--<table class="table" style="padding-left: 10px; width: 80%;">
            <tr>
                <td style="border-top: 0px; width: 90%">
                    1. Has the patient/caregiver accepted HIV status?
                </td>
                <td style="border-top: 0px; width: 10%">
                    <input id="chkAcceptedHIVStatus" name="switch-size" type="checkbox" checked data-size="small"
                        data-on-text="Yes" data-off-text="No" />
                </td>
            </tr>
            <tr>
                <td style="border-top: 0px; width: 90%">
                    2. For children/adolescents: is age-appropriate disclosure underway/complete?
                </td>
                <td style="border-top: 0px; width: 10%">
                    <input id="chkIsAgeAppropriate" name="switch-size" type="checkbox" checked data-size="small"
                        data-on-text="Yes" data-off-text="No" />
                </td>
            </tr>
        </table>--%>
    </div>
    <!-- /.box-body -->
</div>