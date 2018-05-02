<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucBarriersSection1.ascx.cs"
    Inherits="PresentationApp.Adherence.Controls.ucBarriersSection1" %>
<!-- HIV status -->
<div class="box box-default box-solid" id="divHIVCare">
    <div class="box-header with-border">
        <h3 class="box-title">
            A. Awareness of HIV status</h3>
        <div class="box-tools pull-right">
            <%--<button type="button" class="btn btn-box-tool" data-widget="collapse">
                <i id="divHIVCareI" class="fa fa-minus"></i>
            </button>--%>
        </div>
    </div>
    <!-- /.box-header -->
    <div class="box-body">
        <table class="table" style="padding-left: 10px; width: 80%;">
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
        </table>
    </div>
    <!-- /.box-body -->
</div>
<!-- HIV status -->
<div class="box box-default box-solid" id="div1">
    <div class="box-header with-border">
        <h3 class="box-title">
            B. Understanding of HIV infection and ART</h3>
        <div class="box-tools pull-right">
            <%--<button type="button" class="btn btn-box-tool" data-widget="collapse">
                <i id="divHIVCareI" class="fa fa-minus"></i>
            </button>--%>
        </div>
    </div>
    <!-- /.box-header -->
    <div class="box-body">
        <table class="table" style="padding-left: 10px; width: 80%;">
            <tr>
                <td style="border-top: 0px; width: 90%">
                    1. Understands how HIV affects the body and risk of transmission to sexual partners
                    and children during pregnancy and breastfeeding
                </td>
                <td style="border-top: 0px; width: 10%">
                    <input id="chkRiskOfTransmission" name="switch-size" type="checkbox" checked data-size="small"
                        data-on-text="Yes" data-off-text="No" />
                </td>
            </tr>
            <tr>
                <td style="border-top: 0px; width: 90%">
                    2. Understands ART and how it works.
                </td>
                <td style="border-top: 0px; width: 10%">
                    <input id="chkUnderstandART" name="switch-size" type="checkbox" checked data-size="small"
                        data-on-text="Yes" data-off-text="No" />
                </td>
            </tr>
            <tr>
                <td style="border-top: 0px; width: 90%">
                    3 Understands side effects and what to do in case of side effects
                </td>
                <td style="border-top: 0px; width: 10%">
                    <input id="chkARTSideEffect" name="switch-size" type="checkbox" checked data-size="small"
                        data-on-text="Yes" data-off-text="No" />
                </td>
            </tr>
            <tr>
                <td style="border-top: 0px; width: 90%">
                    4. Understands benefits of adherence
                </td>
                <td style="border-top: 0px; width: 10%">
                    <input id="chkAdherenceBenefits" name="switch-size" type="checkbox" checked data-size="small"
                        data-on-text="Yes" data-off-text="No" />
                </td>
            </tr>
            <tr>
                <td style="border-top: 0px; width: 90%">
                    5. Understands consequences of non-adherence including drug resistance and treatment
                    failure
                </td>
                <td style="border-top: 0px; width: 10%">
                    <input id="chkConsequencesNonAdherence" name="switch-size" type="checkbox" checked data-size="small"
                        data-on-text="Yes" data-off-text="No" />
                </td>
            </tr>
        </table>
    </div>
    <!-- /.box-body -->
    <!-- Button Footer -->
    <div class="row no-print" align="center" style="margin-bottom: 10px;">
        <div class="form-group">
            <div class="col-md-12" align="center">
                <button type="button" class="btn btn-primary" onclick="PartialSavePatientAdherenceBarriers('tab_2');" runat="server"
                        id="btnS1Next" clientidmode="Static">
                    Next<span class="glyphicon glyphicon-floppy-disk" style="padding-left: 5px;"></span></button>
            </div>
        </div>
    </div>
</div>
