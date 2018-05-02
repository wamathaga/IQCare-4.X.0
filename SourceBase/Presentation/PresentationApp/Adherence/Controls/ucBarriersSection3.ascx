<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucBarriersSection3.ascx.cs"
    Inherits="PresentationApp.Adherence.Controls.ucBarriersSection3" %>
<!-- Daily Routine -->
<div class="box box-default box-solid" id="divHIVCare">
    <div class="box-header with-border">
        <h3 class="box-title">
            D. Psychosocial Circumstances</h3>
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
                            Who do you live with?</h3>
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
                                <textarea class="form-control" rows="6" placeholder="Notes"
                                    id="txtLiveWith" maxlength="1000" style="resize: none;"></textarea>
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
                            Who is aware of your HIV status? Are there people in your life with whom you’ve
                            discussed your HIV status and ART use?</h3>
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
                                <textarea class="form-control" rows="6" placeholder="Notes"
                                    id="txtHIVStatusArtDiscussion" maxlength="1000" style="resize: none;"></textarea>
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
                            What is your support system?</h3>
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
                                <div id="divSupportSystem">
                                    <div class="row">
                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group" style="margin-bottom: 5px;">
                                            <input type="radio" id="rdTreatmentbuddy" name="supportSystem" class="flat-red" value="1" />
                                            &nbsp;<label for="minimal-radio-1">
                                                Treatment buddy</label>
                                        </div>
                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group" style="margin-bottom: 5px;">
                                            <input type="radio" id="rdSupportSystem" name="supportSystem" class="flat-red" value="1" />
                                            &nbsp;<label for="minimal-radio-1">
                                                Support group</label>
                                        </div>
                                        <div class="col-md-8 col-sm-12 col-xs-12 form-group" style="margin-bottom: 5px;">
                                            &nbsp;</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                <textarea class="form-control" rows="6" placeholder="Notes"
                                    id="txtSupportSystemDetails" maxlength="1000" style="resize: none;"></textarea>
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
                            Are there changes in relationships with family members/friends?</h3>
                        <div class="box-tools pull-right">
                            <input id="chkChangesInRelationship" name="switch-size" type="checkbox" checked data-size="small"
                                data-on-text="Yes" data-off-text="No" />
                            <%--<button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i id="I3" class="fa fa-minus"></i>
                            </button>--%>
                        </div>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                <textarea class="form-control" rows="6" placeholder="Notes"
                                    id="txtChangesInRelationship" maxlength="1000" style="resize: none;"></textarea>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                <div class="box box-primary" id="div5">
                    <div class="box-header with-border">
                        <h3 class="box-title">
                            Does it bother you people might find out about your HIV status?</h3>
                        <div class="box-tools pull-right">
                            <input id="chkHIVStatusBotherDetails" name="switch-size" type="checkbox" checked
                                data-size="small" data-on-text="Yes" data-off-text="No" />
                            <%--<button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i id="I4" class="fa fa-minus"></i>
                            </button>--%>
                        </div>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                <textarea class="form-control" rows="6" placeholder="Notes"
                                    id="txtHIVStatusBotherDetails" maxlength="1000" style="resize: none;"></textarea>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                <div class="box box-primary" id="div6">
                    <div class="box-header with-border">
                        <h3 class="box-title">
                            Do you feel that people treat you differently when they know your HIV status?</h3>
                        <div class="box-tools pull-right">
                            <input id="chkPeopleTreatmentDetails" name="switch-size" type="checkbox" checked
                                data-size="small" data-on-text="Yes" data-off-text="No" />
                           <%-- <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i id="I5" class="fa fa-minus"></i>
                            </button>--%>
                        </div>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                <textarea class="form-control" rows="6" placeholder="Notes"
                                    id="txtPeopleTreatmentDetails" maxlength="1000" style="resize: none;"></textarea>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                <div class="box box-primary" id="div7">
                    <div class="box-header with-border">
                        <h3 class="box-title">
                            Is stigma interfering with taking medication on time or with keeping clinic appointments?</h3>
                        <div class="box-tools pull-right">
                            <input id="chkStigmaInterfering" name="switch-size" type="checkbox" checked data-size="small"
                                data-on-text="Yes" data-off-text="No" />
                            <%--<button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i id="I6" class="fa fa-minus"></i>
                            </button>--%>
                        </div>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                <textarea class="form-control" rows="6" placeholder="Notes"
                                    id="txtStigmaInterfering" maxlength="1000" style="resize: none;"></textarea>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                <div class="box box-primary" id="div8">
                    <div class="box-header with-border">
                        <h3 class="box-title">
                            Have you ever stopped using medication because of religious beliefs?</h3>
                        <div class="box-tools pull-right">
                            <input id="chkReligiousBeliefs" name="switch-size" type="checkbox" checked data-size="small"
                                data-on-text="Yes" data-off-text="No" />
                            <%--<button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i id="I7" class="fa fa-minus"></i>
                            </button>--%>
                        </div>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                <textarea class="form-control" rows="6" placeholder="Notes"
                                    id="txtReligiousBeliefs" maxlength="1000" style="resize: none;"></textarea>
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
            <button type="button" class="btn btn-primary" onclick="GetPreviousPage('tab_2');" runat="server"
                    id="btnS3Previous" clientidmode="Static">
                Previous<span class="glyphicon glyphicon-floppy-disk" style="padding-left: 5px;"></span></button>
            <button type="button" class="btn btn-primary" onclick="PartialSavePatientAdherenceBarriers('tab_4');" runat="server"
                    id="btnS3Next" clientidmode="Static">
                Next<span class="glyphicon glyphicon-floppy-disk" style="padding-left: 5px;"></span></button>
        </div>
    </div>
</div>
</div>
