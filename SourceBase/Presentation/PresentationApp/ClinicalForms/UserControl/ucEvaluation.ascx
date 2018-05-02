<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucEvaluation.ascx.cs" Inherits="PresentationApp.ClinicalForms.UserControl.ucEvaluation" %>
<script type="text/javascript" language="javascript">
    function BacktoHome() {
        window.location.href = "../../ClinicalForms/frmPatient_Home.aspx";
        return false;
    }
</script>

<style type="text/css">
        .style1{width: 100%;}
        </style>
<div class="box box-default box-solid" id="divEvaluation">
    <div class="box-header with-border">
        <h3 class="box-title">
            Evaluation form</h3>
        <div class="box-tools pull-right">
            <%--<button type="button" class="btn btn-box-tool" data-widget="collapse">
                <i id="divHIVCareI" class="fa fa-minus"></i>
            </button>--%>
        </div>
    </div>
    <!-- /.box-header -->
    <div class="box-body">

        <div id="div_print_Eval">
            <asp:Panel ID="pnlPerson" runat="server">
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                        <label class="control-label">
                            Adherence and Treatment Failure Evaluation</label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-7 col-sm-12 col-xs-12 form-group">
                        <label class="control-label">
                            Number of adherence counselling/assessment sessions done in the last 3-6 months</label>
                    </div>
                    <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                        <input type="text" class="form-control" id="txtNumberofadherence" placeholder="Free Text">
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-7 col-sm-12 col-xs-12 form-group">
                        <label class="control-label">
                            Number of home visits conducted in last 3-6 months, and findings</label>
                    </div>
                    <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                        <input type="text" class="form-control" id="txtNoOfHomeVisit" placeholder="Free Text">
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-7 col-sm-12 col-xs-12 form-group">
                        <label class="control-label">
                            Support structures (e.g. treatment buddy, support group attendance, caregivers)
                            in place for this patient?</label>
                    </div>
                    <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                        <textarea class="form-control" rows="4" placeholder="Free Text" id="txtSupportStructure"
                            maxlength="1000" style="resize: none;"></textarea>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-7 col-sm-12 col-xs-12 form-group">
                        <label class="control-label">
                            Evidence of adherence concerns (e.g. missed appoint- ments, pill counts)</label>
                    </div>
                    <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                        <textarea class="form-control" rows="4" placeholder="Free Text" id="txtEvidenceOfAdherence"
                            maxlength="1000" style="resize: none;"></textarea>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-7 col-sm-12 col-xs-12 form-group">
                        <label class="control-label">
                            Number of DOTS done in last 3-6 months</label>
                    </div>
                    <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                        <input type="text" class="form-control" id="txtNumberofDOTS" placeholder="Free Text" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-7 col-sm-12 col-xs-12 form-group">
                        <label class="control-label">
                            Likely root cause/s of poor adherence for this patient (e.g. stigma, disclosure,
                            side effects, alcohol or other drugs, mental health issues, caregiver changes, religious
                            beliefs, inadequate preparation, etc.)</label>
                    </div>
                    <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                        <textarea class="form-control" rows="4" placeholder="Free Text" id="txtLikelyroot"
                            maxlength="1000" style="resize: none;"></textarea>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                        <label class="control-label">
                            Evaluation for other causes of treatment failure, e.g.:</label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-7 col-sm-12 col-xs-12 form-group">
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                <input type="radio" id="rdInadequate" name="supportSystem" class="flat-red" value="1" />
                                &nbsp;<label for="minimal-radio-1">
                                    Inadequate dosing/dose adjustments (particularly for children)</label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                <input type="radio" id="rdDruginteractions" name="supportSystem" class="flat-red"
                                    value="1" />
                                &nbsp;<label for="minimal-radio-1">
                                    Drug-drug interactions</label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                <input type="radio" id="rdDrugFoodInteractions" name="supportSystem" class="flat-red"
                                    value="1" />
                                &nbsp;<label for="minimal-radio-1">
                                    Drug-food interactions</label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                <input type="radio" id="rdImpairedabsorption" name="supportSystem" class="flat-red"
                                    value="1" />
                                &nbsp;<label for="minimal-radio-1">
                                    Impaired absorption (e.g. chronic severe diar- rhoea)</label>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                        <textarea class="form-control" rows="4" placeholder="Free Text" id="txtEvaluationCause"
                            maxlength="1000" style="resize: none;"></textarea>
                    </div>
                </div>
                <div class="box box-default box-solid" id="divART">
                    <div class="box-header with-border">
                        <h3 class="box-title">
                            Other Relevant ART History</h3>
                        <div class="box-tools pull-right">
                            <%--<button type="button" class="btn btn-box-tool" data-widget="collapse">
                <i id="divART" class="fa fa-minus"></i>
            </button>--%>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-7 col-sm-12 col-xs-12 form-group">
                        <label class="control-label">
                            Comment on treatment interruptions (if any)</label>
                    </div>
                    <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                        <textarea class="form-control" rows="4" placeholder="Free Text" id="txtCommentOnTreatment"
                            maxlength="1000" style="resize: none;"></textarea>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-7 col-sm-12 col-xs-12 form-group">
                        <label class="control-label">
                            Has Drug Resistance/Sensitivity Testing been done for this patient? If yes, state
                            date done and attach the de- tailed results</label>
                    </div>
                    <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                        <textarea class="form-control" rows="4" placeholder="Free Text" id="txtHasDrug" maxlength="1000"
                            style="resize: none;"></textarea>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-7 col-sm-12 col-xs-12 form-group">
                        <label class="control-label">
                            Has facility multidisciplinary team discussed the patient’s case? If yes, comment
                            on date, deliberations and recom- mendations (indicate how treatment failure was
                            estab- lished and confirmed, proposed regimen and dosage, current source of drugs
                            if patient already on 3rd line)</label>
                    </div>
                    <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                        <textarea class="form-control" rows="4" placeholder="Free Text" id="txtHasFacility"
                            maxlength="1000" style="resize: none;"></textarea>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-7 col-sm-12 col-xs-12 form-group">
                        <label class="control-label">
                            MDT members who participated in the case discussion (names and titles)</label>
                    </div>
                    <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                        <textarea class="form-control" rows="4" placeholder="Free Text" id="txtMDTMembers"
                            maxlength="1000" style="resize: none;"></textarea>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
    <div class="row no-print" align="center" style="margin-bottom: 10px;">
        <div class="form-group">
            <div class="col-md-12" align="center">
                <button type="button" class="btn btn-primary" runat="server" id="btnSave" clientidmode="Static"
                    onclick="SaveUpdateClinicalSummaryData('');">
                    Save<span class="glyphicon glyphicon-floppy-disk" style="padding-left: 5px;"></span></button>
                <button type="button" class="btn btn-primary" runat="server" id="btnPrint" clientidmode="Static"
                    onclick="WindowPrint()">
                    Print<span class="glyphicon glyphicon-print" style="padding-left: 5px;"></span></button>
                <button type="button" class="btn btn-primary" runat="server" id="btnClose" clientidmode="Static"
                    onclick="BacktoHome()">
                    Close<span class="glyphicon glyphicon-remove" style="padding-left: 5px;"></span></button>
                <%--<asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" OnClientClick="WindowPrint()" />--%>
                <%--onclick="printdiv('div_print_Eval');"--%>
            </div>
        </div>
    </div>
    <!-- /.box-body -->
</div>
