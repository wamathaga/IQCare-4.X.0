<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPrEPInitiation.ascx.cs" Inherits="PresentationApp.PrEP.Controls.ucPrEPInitiation" %>
<script type="text/javascript">

</script>
<div>     
    <!-- Lab Investigations -->
    <div class="box box-primary" id="divClientDetails">
        <div class="box-header with-border">
            <h3 class="box-title">
                Lab Investigations</h3>
        </div>
        <div class="box-body">
           <div class="row">
     <div class="col-md-12 col-sm-12 col-xs-12 form-group">   
     
     <table id="dtlLabResults" class="table table-bordered table-striped">       
                                <thead>
                                    <tr>
                                        <th style="width: 15%;">
                                            LabTestID
                                        </th>
                                        <th style="width: 35%;">
                                            TestName
                                        </th>
                                        <th style="width: 20%;">
                                            Date
                                        </th>
                                        <th style="width: 20%;">
                                            Result
                                        </th>
                                        <th style="width: 10%;">
                                           Order Today
                                        </th>                                       
                                    </tr>
                                </thead>
                            </table>
                                                                           
     </div>
    </div>
     <div class="row">
     <div class="col-md-2 col-sm-12 col-xs-12 form-group">   
      <label for="inputEmail3" class="control-label">Creatinine Clearence</label>
     </div>
      <div class="col-md-2 col-sm-12 col-xs-12 form-group">   
      <input type="text" class="form-control" id="txtCreatinineClearence" disabled="disabled"/>
     </div>
     </div>
     <div class="row">
     <div class="col-md-2 col-sm-12 col-xs-12 form-group">   
      <label for="inputEmail3" class="control-label">HIV Test Results</label>
     </div>
      <div class="col-md-2 col-sm-12 col-xs-12 form-group">   
     <select class="form-control select2" data-placeholder="Select" id="ddlHIVTestResults"
                        style="width: 100%;" onchange="HIVTestResultsChange('ddlHIVTestResults','ifPositive')">
                    </select>
     </div>
     <div class="col-md-8 col-sm-12 col-xs-12 form-group" id="ifPositive" style="display:none;">
      <div class="row">
       <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                    <label for="inputEmail3" class="control-label">
                        Sample for Drug resistance colleted?</label>
                </div>
                <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                    <input id="chkDrugCollected" name="switch-size" type="checkbox" checked data-size="small"
                        data-on-text="Yes" data-off-text="No">
                </div>
      </div>
      <div class="row">
       <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                    <label for="inputEmail3" class="control-label">
                        Referred to CCC</label>
                </div>
                <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                    <input id="chkReferredToCCC" name="switch-size" type="checkbox" checked data-size="small"
                        data-on-text="Yes" data-off-text="No">
                </div>
      </div>
     </div>
     </div>
     <div class="row">
     <div class="col-md-2 col-sm-12 col-xs-12 form-group">   
      <label for="inputEmail3" class="control-label">Hepatitis B Vaccine initiated</label>
     </div>
      <div class="col-md-2 col-sm-12 col-xs-12 form-group">   
      <input id="chkHepatitisBVaccine" name="switch-size" type="checkbox" checked data-size="small"
                        data-on-text="Yes" data-off-text="No">
     </div>
     </div>
        </div>
    </div>

     <!-- PrEP initiation criteria -->
    <div class="box box-primary" id="divTransfer">
        <div class="box-header with-border">
            <h3 class="box-title">
                PrEP initiation criteria</h3>
            <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse">
                    <i class="fa fa-minus"></i>
                </button>
            </div>
        </div>
        <div class="box-body">            
            <div class="row">
                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                    <label for="inputEmail3" class="control-label">
                        Previous PrEP use</label>
                </div>
                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                    <input id="chkPreviousPrEPuse" name="switch-size" type="checkbox" checked data-size="small"
                        data-on-text="Yes" data-off-text="No">
                </div>
            </div>
            <div class="row">
                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                    <label for="inputEmail3" class="control-label">
                        Willing to start PrEP</label>
                </div>
                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                    <input id="chkWillingToStartPrEP" name="switch-size" type="checkbox" checked data-size="small"
                        data-on-text="Yes" data-off-text="No" onchange="WillingToStartPrEPChange('chkWillingToStartPrEP','dvNoPrep','dvYesPrep')"/>
                </div>
                <div class="col-md-6 col-sm-12 col-xs-12 form-group" style="display:none;" id="dvNoPrep">
                 <select class="form-control select2" data-placeholder="Select" id="ddlPrepStartNo"
                        style="width: 100%;">
                    </select>
               <%-- <label for="inputEmail3" class="control-label">
                None, Side Effects (ADR), Stigma, Pill burdent, Taking pills for a long time, Too many HIV Test
                </label>--%>
                </div>
                <div class="col-md-6 col-sm-12 col-xs-12 form-group" style="display:none;" id="dvYesPrep">
                <div class="row">
                <div class="col-md-8 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">Signs/symptoms of acute HIV</label>
                </div>
                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                <input id="chkSymptomsOfHIV" name="switch-size" type="checkbox" checked data-size="small"
                        data-on-text="Yes" data-off-text="No" onchange="WillingToStartPrEPChangeNo('chkSymptomsOfHIV','chkMedicallyToStartPrep','chkContraindications','dvEligibleforPrEP','dvNoEligibleforPrEP')"/>
                </div>
                </div>
                <div class="row">
                <div class="col-md-8 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">Medically ineligible to start PrEP</label>
                </div>
                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                 <input id="chkMedicallyToStartPrep" name="switch-size" type="checkbox" checked data-size="small"
                        data-on-text="Yes" data-off-text="No" onchange="WillingToStartPrEPChangeNo('chkSymptomsOfHIV','chkMedicallyToStartPrep','chkContraindications','dvEligibleforPrEP','dvNoEligibleforPrEP')"/>
                </div>
                </div>
                <div class="row">
                <div class="col-md-8 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">Contraindications for TDF-FTC /TDF-3TC/TDF</label>
                </div>
                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                <input id="chkContraindications" name="switch-size" type="checkbox" checked data-size="small"
                        data-on-text="Yes" data-off-text="No" onchange="WillingToStartPrEPChangeNo('chkSymptomsOfHIV','chkMedicallyToStartPrep','chkContraindications','dvEligibleforPrEP','dvNoEligibleforPrEP')"/>
                </div>
                </div>
                </div>
            </div>
            <div class="row text-center" id="dvEligibleforPrEP" style="display:none;">
             <label for="inputEmail3" class="control-label" id="lbloptions">Eligible for PrEP</label>
            </div>
            <div class="row text-center" id="dvNoEligibleforPrEP" style="display:none;">
             <label for="inputEmail3" class="control-label" id="Label1">Not eligible for PrEP</label>
            </div>
        </div>
    </div>

    <!-- PrEP continuation criteria -->
    <div class="box box-primary">
        <div class="box-header with-border">
            <h3 class="box-title">
                PrEP continuation criteria</h3>
        </div>
        <div class="box-body">
            <div class="row">
                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                    <label for="inputEmail3" class="control-label">
                        Signs/symptoms of acute HIV:</label>
                </div>
                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                     <input id="chkSigns_Symptoms" name="switch-size" type="checkbox" checked data-size="small"
                        data-on-text="Yes" data-off-text="No"/>
                </div>
               
            </div>
            <!-- /.row -->
           
        </div>
    </div>

    <!-- PrEP Adherence -->
    <div class="box box-primary">
        <div class="box-header with-border">
            <h3 class="box-title">
                PrEP Adherence</h3>
        </div>
        <div class="box-body">
            <div class="row">
                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                    <label id="lblBPMMHG" for="inputEmail3" class="control-label">
                        Self-assessment of adherence since last visit:</label>
                </div>
                <div class="col-md-2 col-sm-12 col-xs-12 form-group" style="display: inline-flex">
                    <select class="form-control select2" data-placeholder="Select" id="ddlSelfAssessment"
                        style="width: 100%;" onchange="WhyPoor('ddlSelfAssessment','dvWhyPoor')">
                    </select>
                </div>
                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                    <label for="inputEmail3" class="control-label">
                       Pill Count:
                    </label>
                </div>
                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                    <input type="number" class="form-control" id="txtPillCount" placeholder="Pill Count"
                       />
                </div>
                <div id="dvWhyPoor" style="display:none;">
                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                    <label for="inputEmail3" class="control-label">
                        Why Poor/Fair:</label>
                </div>
                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                     <select class="form-control select2" data-placeholder="Select" id="ddlWhyPoorFair"
                        style="width: 100%;">
                    </select>
                </div>
                </div>
            </div>
            <!-- /.row -->
            <div class="row">
                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                    <label for="inputEmail3" class="control-label">
                        PrEP status:</label>
                </div>
                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                     <select class="form-control select2" data-placeholder="Select" id="ddlPrepStatus"
                        style="width: 100%;" onchange="PrEPStatus('ddlSelfAssessment','dvDiscontinueReason','dvRegime')">
                    </select>
                </div>
                <div id="dvDiscontinueReason" style="display:none;">
                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                    <label for="inputEmail3" class="control-label">
                        Reason for discontinuation:</label>
                </div>
                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                    <select class="form-control select2" data-placeholder="Select" id="ddlReasonFordiscontinuation"
                        style="width: 100%;">
                    </select>
                </div>
                </div>
                <div id="dvRegime" style="display:none;">
                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                    <label for="inputEmail3" class="control-label">
                        Regimen:</label>
                </div>
                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                     <select class="form-control select2" data-placeholder="Select" id="ddlPreRegimen"
                        style="width: 100%;">
                    </select>
                </div>
                </div>
            </div>
            <!-- /.row -->
            <div class="row">
              <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                    <label for="inputEmail3" class="control-label">
                        Condoms Issed:</label>
                </div>
                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                       <input id="chkCondomsIssed" name="switch-size" type="checkbox" checked data-size="small"
                        data-on-text="Yes" data-off-text="No"/>              
                </div>  
                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                    <label for="inputEmail3" class="control-label">
                        Adherence counselling done:</label>
                </div>
                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                      <input id="chkAdherenceCounsellingDone" name="switch-size" type="checkbox" checked data-size="small"
                        data-on-text="Yes" data-off-text="No"/>               
                </div> 
                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                    <label for="inputEmail3" class="control-label">
                        Prescribed PrEP today:</label>
                </div>
                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                       <input id="chkPrescribedPrEPtoday" name="switch-size" type="checkbox" checked data-size="small"
                        data-on-text="Yes" data-off-text="No"/>              
                </div> 
            </div>
            <!-- /.row -->
        </div>
    </div>

     <!-- Reminders and workplan -->
    <div class="box box-primary">
        <div class="box-header with-border">
            <h3 class="box-title">
            Reminders and workplan
            </h3>
        </div>
        <div class="box-body">
            <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                    <textarea class="form-control" rows="5" placeholder="Reminders and workplan" id="txtRemindersWorkplan"
                        maxlength="1000"></textarea>
                </div>
            </div>
        </div>
    </div>

    <!-- Next Appointment -->
    <div class="box box-primary">
        <div class="box-header with-border">
            <h3 class="box-title">
            Next Appointment
            </h3>
        </div>
        <div class="box-body">
            <div class="row">
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label requiredFieldColor">
                            * Next Appointment Date</label>
                    </div>
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <div class="input-group date">
                            <div class="input-group-addon">
                                <i class="fa fa-calendar"></i>
                            </div>
                            <input type="text" class="form-control" id="dtNextAppointmentDate" data-date-format="dd-M-yyyy"
                                style="padding: 13px 12px;" />
                        </div>
                    </div>
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <input type="text" class="form-control" id="txtServiceArea" placeholder="PrEP"
                            disabled="disabled" value="PrEP" />
                    </div>
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <select class="form-control select2" data-placeholder="Select" id="ddlPurpose">
                        </select>
                    </div>
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <input type="text" class="form-control" id="txtNADDescription" placeholder="Description" />
                    </div>
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <button type="button" class="btn btn-block btn-primary btn-sm" style="width: 50px;"
                            id="btnAddNextAppointment" onclick="AddAppointment('0');">
                            Add</button>
                    </div>
                   
                </div>
                
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                        <table id="dtlNextAppointment" class="table table-bordered table-striped">
                            <thead>
                                <tr>
                                    <th style="width: 50px;">
                                        #
                                    </th>
                                    <th style="width: 150px;">
                                        Appintment Date
                                    </th>
                                    <th style="width: 100px;">
                                        Service Area
                                    </th>
                                    <th style="width: 100px;">
                                        Reason
                                    </th>
                                    <th style="width: 250px;">
                                        Description
                                    </th>
                                    <th style="width: 100px;">
                                        Status
                                    </th>
                                    <th style="width: 10px;">
                                        Edit
                                    </th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                </div>
        </div>
    </div>

    <div class="box-body">
        <!-- / sub box level 2 -->
        <div class="box box-default box-solid">
            <!-- /.box-header -->
            <div class="box-body">
                <!-- /.box-body -->
                <div class="row">
                    <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                        <button type="button" class="btn btn-primary" onclick="OpenCEPopup('<%= ResolveUrl("~/PharmacyDispense/frmPharmacyDispense_PatientOrder.aspx") %>');">
                            Prescription<span class="glyphicon glyphicon-pencil" style="padding-left: 5px;"></span></button>
                    </div>
                    <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                        <button type="button" class="btn btn-primary" onclick="OpenCEPopup('<%= ResolveUrl("~/Laboratory/frm_Laboratory.aspx") %>');">
                            Laboratory<span class="glyphicon glyphicon-tags" style="padding-left: 5px;"></span></button>
                    </div>
                </div>
            </div>
            <!-- /.box sub box level 2 -->
        </div>
        <!-- /.box-body -->
    </div>

    <!-- Button Footer -->
    <div class="row no-print" align="center">
        <div class="form-group">
            <div class="col-md-12" align="center">
                <button type="button" class="btn btn-primary" onclick="SaveUpdatePrEPInitiationData('tab_2');" runat="server"
                    id="btnSaveTriage" clientidmode="Static">
                    Save<span class="glyphicon glyphicon-floppy-disk" style="padding-left: 5px;"></span></button>
                <button type="button" class="btn btn-primary" runat="server" onclick="DeleteForm();"
                    id="btnDeleteForm" clientidmode="Static">
                    Delete<span class="glyphicon glyphicon-remove" style="padding-left: 5px;"></span></button>
            </div>
        </div>
    </div>
</div>
