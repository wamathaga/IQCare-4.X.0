<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucBarriersSection2.ascx.cs"
    Inherits="PresentationApp.Adherence.Controls.ucBarriersSection2" %>
<!-- Daily Routine -->
<div class="box box-default box-solid" id="divHIVCare">
    <div class="box-header with-border">
        <h3 class="box-title">
            C. Daily Routine</h3>
        <div class="box-tools pull-right">
        </div>
    </div>
    <!-- /.box-header -->
    <div class="box-body">
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                <div class="box box-primary" id="div1">
                    <div class="box-header with-border">
                        <h3 class="box-title">
                            Tell me about your typical day (Review the patient’s/caregiver’s daily routine)</h3>
                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i id="divHIVCareI" class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                <textarea class="form-control" rows="6" placeholder="Notes" id="txtTypicalDay" maxlength="1000"
                                    style="resize: none;"></textarea>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                <div class="box box-primary" id="div2">
                    <div class="box-header with-border">
                        <h3 class="box-title">
                            Please tell me how you take each of your medicines? (Review how the patient takes
                            medicine or how the caregiver administers it)</h3>
                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i id="I1" class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                <textarea class="form-control" rows="6" placeholder="Notes" id="txtMedicinesIntake"
                                    maxlength="1000" style="resize: none;"></textarea>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                <ul>
                                    <li>If the patient’s/caregiver’s daily routine conflicts with medication schedule, work
                                        with them to find a new medication schedule that will be more appropriate </li>
                                    <li>Remind the patient/caregiver to take/give missed or delayed doses as soon as he/she
                                        remembers (up to 12 hours late if on a once-daily regimen, or up to 6 hours late
                                        if on a twicedaily regimen). The next dose should be taken at the usual time
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                <div class="box box-primary" id="div3">
                    <div class="box-header with-border">
                        <h3 class="box-title">
                            What do you do in case of visits or travel?</h3>
                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i id="I2" class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                <textarea class="form-control" rows="6" placeholder="Notes" id="txtTravelVisitDescription"
                                    maxlength="1000" style="resize: none;"></textarea>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                <div class="box box-primary" id="div4">
                    <div class="box-header with-border">
                        <h3 class="box-title">
                            For Orphans/Children: Who is the primary care giver? Assess their level of commitment.</h3>
                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i id="I3" class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                <textarea class="form-control" rows="6" placeholder="Notes" id="txtPrimartCareGiver"
                                    maxlength="1000" style="resize: none;"></textarea>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- /.box-body -->
    <!-- Button Footer -->
    <div class="row no-print" align="center" style="margin-bottom: 10px;">
        <div class="form-group">
            <div class="col-md-12" align="center">
                <button type="button" class="btn btn-primary" onclick="GetPreviousPage('tab_1');"
                    runat="server" id="btnS2Previous" clientidmode="Static">
                    Previous<span class="glyphicon glyphicon-floppy-disk" style="padding-left: 5px;"></span></button>
                <button type="button" class="btn btn-primary" onclick="PartialSavePatientAdherenceBarriers('tab_3');" runat="server"
                    id="btnS2Next" clientidmode="Static">
                    Next<span class="glyphicon glyphicon-floppy-disk" style="padding-left: 5px;"></span></button>
            </div>
        </div>
    </div>
</div>
