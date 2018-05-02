<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucCounsellingSessionVL.ascx.cs"
    Inherits="PresentationApp.Adherence.Controls.ucCounsellingSessionVL" %>
<div id="divPageGroup1" class="box box-default box-solid">
    <div class="box-header with-border">
        <h3 class="box-title">
            Enhance Adherence Counselling Sessions</h3>
        <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                <i class="fa fa-minus"></i>
            </button>
        </div>
    </div>
</div>
<div id="divPageGroup2" class="box box-default box-solid">
    <div class="box-header with-border">
        <h3 class="box-title">
            Viral Load</h3>
        <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                <i class="fa fa-minus"></i>
            </button>
        </div>
    </div>
    <div class="box-body">
        <div class="col-md-10 col-sm-12 col-xs-12 form-group">
            Discuss result of the second V/L tst?</div>
        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            <input id="chkCaseManager5" name="switch-size" type="checkbox" checked data-size="small"
                data-on-text="Yes" data-off-text="No" />
        </div>
        <div class="box box-default box-solid">
            <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                    Way forward (VL<1000 continue current regiment, VL>1000 Prep for regimen change)
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                    <textarea class="form-control" rows="5"  cols="1" id="txtAreaExperience5" maxlength="1000"></textarea>
                </div>
            </div>
            <div class="row">
                <div class="col-md-10 col-sm-12 col-xs-12 form-group">
                    <label for="inputEmail3" class="control-label">
                        Check this box if all required sessions(1,2,3/4) have been completed
                    </label>
                </div>
                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                    <input id="chkBenefitHomeVisit5" name="switch-size" type="checkbox" checked data-size="small"
                        data-on-text="Yes" data-off-text="No" />
                </div>
            </div>
    </div>
</div>
</div>
<!-- Button Footer -->
<div class="row no-print" align="center">
    <div class="form-group">
        <div class="col-md-12" align="center">
            <button type="button" class="btn btn-primary" onclick="GetPreviousPage('tab_4');">
                Previous<span class="glyphicon glyphicon-remove-circle" style="padding-left: 5px;"></span></button>
            <button type="button" class="btn btn-primary" onclick="SaveSessionVL();" runat="server"
                id="btnSaveCounsellingSessionVL" clientidmode="Static">
                Save<span class="glyphicon glyphicon-floppy-disk" style="padding-left: 5px;"></span></button>
        </div>
    </div>
</div>
