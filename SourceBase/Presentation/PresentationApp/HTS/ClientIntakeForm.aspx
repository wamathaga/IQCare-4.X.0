<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/IQCare.master"
    CodeBehind="ClientIntakeForm.aspx.cs" Inherits="PresentationApp.HTS.ClientIntakeForm" %>

<asp:Content ID="Data" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <script src="<%=ResolveUrl("../incl/Common.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>"
        type="text/javascript"></script>
    <script src="<%=ResolveUrl("../incl/Constants.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>"
        type="text/javascript"></script>
    <script src="<%=ResolveUrl("Scripts/clientIntakeForm.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>"
        type="text/javascript"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-maxlength/1.7.0/bootstrap-maxlength.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () { $("[name='switch-size']").bootstrapSwitch(); });
    </script>
    <div class="content-wrapper">
        <!-- Main content -->
        <div class="content">
            <div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="box box-primary box-solid">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Client Intake Form</h3>
                                </div>
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                <div class="box-body">
                                <div class="col-md-12 col-sm-12 col-xs-12" style="padding-left: 7px; padding-right: 7px;">
                                    <div class="tab-content">
                                        <div class="tab-pane active" id="tab_1">
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                <div id="divPageGroup1" class="box box-default box-solid">
                                            <div class="box-body">
                                                <div class="box box-default box-solid">
                                                    <div class="box-body">
                                                        <div class="row">
                                                            <div class="col-md-2 col-sm-12 col-xs-12">
                                                                <label for="lblState" class="control-label">
                                                                    State:</label>
                                                                <label for="lblState" class="control-label">
                                                                    Kogumu</label>
                                                            </div>
                                                            <div class="col-md-2 col-sm-12 col-xs-12">
                                                                <label for="lblLGA" width="100px" class="control-label">
                                                                    LGA:</label>
                                                                <label for="lblLGA" class="control-label">
                                                                    Misusiri</label>
                                                            </div>
                                                            <div class="col-md-2 col-sm-12 col-xs-12 ">
                                                                <label for="lblFacility" class="control-label">
                                                                    Facility Name:</label>
                                                                <label for="lblFacilityName" class="control-label">
                                                                    Nigeria Care</label>
                                                            </div>
                                                            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                                <label for="lblReferredFrom" class="control-label">
                                                                    Referred:
                                                                </label>
                                                                <select id="cmbReferredFrom" class="form-control">
                                                                    <option value="0" selected="selected">Select and option</option>
                                                                    <option value="1">Self</option>
                                                                    <option value="2">TB</option>
                                                                    <option value="3">STI</option>
                                                                    <option value="4">FB</option>
                                                                    <option value="5">OPD</option>
                                                                    <option value="6">Ward</option>
                                                                    <option value="7">Blood Bank</option>
                                                                    <option value="8">ANC</option>
                                                                    <option value="9">Maternity</option>
                                                                    <option value="10">Immunization and Community</option>
                                                                    <option value="11">Others</option>

                                                                </select>
                                                            </div>
                                                            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                                <label for="lblSetting" class="control-label">
                                                                    Setting:
                                                                </label>
                                                                <select id="cmdSetting" class="form-control">
                                                                    <option value="0" selected="selected">Select and option</option>
                                                                    <option value="1">CT</option>
                                                                    <option value="2">TB</option>
                                                                    <option value="3">STI</option>
                                                                    <option value="4">OPD</option>
                                                                    <option value="5">Ward</option>
                                                                    <option value="6">Outreach</option>
                                                                    <option value="7">Standalone HTS</option>
                                                                    <option value="8">Others</option>
                                                                </select>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="box-body box-footer">
                                                        <div class="row">
                                                            <div class="col-md-3 col-sm-12 col-xs-12 ">
                                                                <label for="lblDOV1" class="control-label">
                                                                    Date Of Visit:</label>
                                                                <input type="text" class="form-control" id="txtDOV" placeholder="Date Of Visit" data-date-format="dd-M-yyyy">
                                                            </div>
                                                           <%-- <div class="col-md-3 col-sm-12 col-xs-12 ">
                                                                <label for="lblTelephone" class="control-label">
                                                                    Telephone:</label>
                                                                <input type="text" class="form-control" id="txtTelephine" placeholder="Telephone Number"
                                                                    data-number-format="xxxx-xxx-xxx">
                                                            </div>
                                                            <div class="col-md-4 col-sm-12 col-xs-12 ">
                                                                <label for="lblAddress" class="control-label">
                                                                    Descriptive Address:</label>
                                                                <textarea class="form-control" id="txtAddress" placeholder="Full Address" cols="4"
                                                                    rows="2"></textarea>
                                                            </div>--%>
                                                        </div>
                                                        <div class="row" style="margin-top: 1%">
                                                            <div class="col-md-3 col-sm-12 col-xs-12 ">
                                                                <label for="lblClientCode" class="control-label">
                                                                    Client Code:</label>
                                                                <input type="text" class="form-control" id="txtClientCode" placeholder="Client code">
                                                            </div>
                                                            <div class="col-md-3 col-sm-12 col-xs-12 ">
                                                                <label for="lblFTV" class="control-label">
                                                                    First Time Visit:</label>
                                                                <input id="chkFTV" name="switch-size" type="checkbox" checked data-size="small" data-on-text="Yes"
                                                                    data-off-text="No">
                                                            </div>
                                                            <div class="col-md-3 col-sm-12 col-xs-12 ">
                                                                <label for="lblSOR" class="control-label">
                                                                    State of Residence:
                                                                </label>
                                                                <select id="cmbSOR" class="form-control" >
                                                                    
                                                                </select>
                                                            </div>
                                                            <div class="col-md-3 col-sm-12 col-xs-12 ">
                                                                <label for="lblLGAOR" class="control-label">
                                                                    LGA of Residence:
                                                                </label>
                                                                <select id="cmbLGAOR" class="form-control" >
                                                                    
                                                                </select>
                                                            </div>
                                                        </div>
                                                        <div class="row" style="margin-top: 1%; vertical-align: text-bottom">
                                                            <div class="col-md-3 col-sm-12 col-xs-12 ">
                                                                <label for="lblMaritalStatus" class="control-label">
                                                                    Marital Status:</label>
                                                                <select id="cmbMaritalStatus" class="form-control">
                                                                    <option value="0" selected="selected">Select a option</option>
                                                                    <option value="1">Married</option>
                                                                    <option value="2">Single</option>
                                                                    <option value="3">Widow</option>
                                                                </select>
                                                            </div>
                                                            <div class="col-md-3 col-sm-12 col-xs-12 ">
                                                                <label for="lblNoOWC" class="control-label">
                                                                    No. of own children < 5:</label>
                                                                <input type="text" class="form-control" id="txtchildLess5" placeholder="Number of child < 5"
                                                                    data-number-format="x">
                                                            </div>
                                                            <div class="col-md-3 col-sm-12 col-xs-12 ">
                                                                <label for="lblNoOW" class="control-label">
                                                                    (if Married) No. of Wives/Co-Wives:</label>
                                                                <input type="text" class="form-control" id="txtNoOfWives" placeholder="Number of Wives / Co-Wives"
                                                                    data-number-format="x">
                                                            </div>
                                                            <div class="col-md-2 col-sm-12 col-xs-12 ">
                                                                <label for="lblTypeOfSession" class="control-label">
                                                                    Type of Session:</label>
                                                                <input id="chkTypeOfSession" name="switch-size" type="checkbox" checked data-size="small"
                                                                    data-on-text="Individual" data-off-text="Couple">
                                                            </div>
                                                        </div>
                                                        <div class="row" style="margin-top: 1%; vertical-align: text-bottom">
                                                            <div class="col-md-3 col-sm-12 col-xs-12 ">
                                                                <label for="lblIndexTesting" class="control-label">
                                                                    Index Testing: Is client identified from an index client</label>
                                                                <input id="chkIndexTesting" name="switch-size" type="checkbox" checked data-size="small"
                                                                    data-on-text="Yes" data-off-text="No">
                                                            </div>
                                                            <div class="col-md-3 col-sm-12 col-xs-12 ">
                                                                <label for="lblIfYes" class="control-label">
                                                                    if Yes</label>
                                                                <input id="chkIfYesIndex" name="switch-size" type="checkbox" checked data-size="small"
                                                                    data-on-text="Biological" data-off-text="Sexual">
                                                            </div>
                                                            <div class="col-md-3 col-sm-12 col-xs-12 ">
                                                                <label for="lblClientIndexID" class="control-label">
                                                                    Indicate index client ID:</label>
                                                                <input type="text" class="form-control" id="txtClientIndexID" placeholder="Client Index ID">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                            <!--PRE TEST INFORMATOIN-->
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        <div id="divPreTest" class="box box-default box-solid">
                                            <div class="box-header with-border">
                                                <h3 class="box-title">
                                                    Pre-Test Information</h3>
                                                <div class="box-tools pull-right">
                                                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                                        <i class="fa fa-minus"></i>
                                                    </button>
                                                </div>
                                            </div>
                                            <div class="box-body">
                                                <div class="box box-default box-solid">
                                                    <div class="box-body">
                                                        <div class="row">
                                                            <!-- Knowledge assessment-->
                                                            <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                                                                <table class="table" style="padding-left: 10px; margin-bottom: 0px;">
                                                                    <tr>
                                                                        <td colspan="2" style="border-top: 0px; padding: 2px;">
                                                                            <div class="box box-default box-solid" style="margin-bottom: 5px;">
                                                                                <div class="box-header">
                                                                                    <h5 class="box-title">
                                                                                        Knowledge Assessment</h5>
                                                                                </div>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="border-top: 0px; width: 90%">
                                                                            Previously tested HIV negative
                                                                        </td>
                                                                        <td style="border-top: 0px; width: 10%">
                                                                            <input id="chkPrevHIVTested" name="switch-size" type="checkbox" checked data-size="small"
                                                                                data-on-text="Yes" data-off-text="No">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="border-top: 0px; width: 90%">
                                                                            Client pregnant (test and ensure linkage to PMTCT program)
                                                                        </td>
                                                                        <td style="border-top: 0px; width: 10%">
                                                                            <input id="chkClientProg" name="switch-size" type="checkbox" checked data-size="small"
                                                                                data-on-text="Yes" data-off-text="No">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="border-top: 0px; width: 90%">
                                                                            Client informed about HIV transmission routes
                                                                        </td>
                                                                        <td style="border-top: 0px; width: 10%">
                                                                            <input id="chkHIVTransRoutes" name="switch-size" type="checkbox" checked data-size="small"
                                                                                data-on-text="Yes" data-off-text="No">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="border-top: 0px; width: 90%">
                                                                            Client informed about risk factors for HIV transmission
                                                                        </td>
                                                                        <td style="border-top: 0px; width: 10%">
                                                                            <input id="chkHIVRiskFactor" name="switch-size" type="checkbox" checked data-size="small"
                                                                                data-on-text="Yes" data-off-text="No">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="border-top: 0px; width: 90%">
                                                                            Client informed on preventing HIV transmission methods
                                                                        </td>
                                                                        <td style="border-top: 0px; width: 10%">
                                                                            <input id="chkHIVTransmPreven" name="switch-size" type="checkbox" checked data-size="small"
                                                                                data-on-text="Yes" data-off-text="No">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="border-top: 0px; width: 90%">
                                                                            Client informed about possible test results
                                                                        </td>
                                                                        <td style="border-top: 0px; width: 10%">
                                                                            <input id="chkPossibleTestResults" name="switch-size" type="checkbox" checked data-size="small"
                                                                                data-on-text="Yes" data-off-text="No">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="border-top: 0px; width: 90%">
                                                                            Informed consent for HIV testing given
                                                                        </td>
                                                                        <td style="border-top: 0px; width: 10%">
                                                                            <input id="chkHIVConsent" name="switch-size" type="checkbox" checked data-size="small"
                                                                                data-on-text="Yes" data-off-text="No">
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                            <!-- Risk assessment-->
                                                            <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                                                                <table class="table" style="padding-left: 10px; margin-bottom: 0px;">
                                                                    <tr>
                                                                        <td colspan="2" style="border-top: 0px; padding: 2px;">
                                                                            <div class="box box-default box-solid" style="margin-bottom: 5px;">
                                                                                <div class="box-header">
                                                                                    <h5 class="box-title">
                                                                                        HIV Risk Assessment</h5>
                                                                                </div>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="border-top: 0px; width: 90%">
                                                                            Ever had sexual intercourse
                                                                        </td>
                                                                        <td style="border-top: 0px; width: 10%">
                                                                            <input id="chkSexualIntercourse" name="switch-size" type="checkbox" checked data-size="small"
                                                                                data-on-text="Yes" data-off-text="No">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="border-top: 0px; width: 90%">
                                                                            Blood transfusion in last 3 months
                                                                        </td>
                                                                        <td style="border-top: 0px; width: 10%">
                                                                            <input id="chkBloodTransFusion" name="switch-size" type="checkbox" checked data-size="small"
                                                                                data-on-text="Yes" data-off-text="No">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="border-top: 0px; width: 90%">
                                                                            Unprotected sex with casual partner in last 3 months
                                                                        </td>
                                                                        <td style="border-top: 0px; width: 10%">
                                                                            <input id="chkSexinLast3Months" name="switch-size" type="checkbox" checked data-size="small"
                                                                                data-on-text="Yes" data-off-text="No">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="border-top: 0px; width: 90%">
                                                                            Unprotected sex with regular partner in last 3 months
                                                                        </td>
                                                                        <td style="border-top: 0px; width: 10%">
                                                                            <input id="chkRegSexPartner" name="switch-size" type="checkbox" checked data-size="small"
                                                                                data-on-text="Yes" data-off-text="No">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="border-top: 0px; width: 90%">
                                                                            STI in last 3 months
                                                                        </td>
                                                                        <td style="border-top: 0px; width: 10%">
                                                                            <input id="chkSTI" name="switch-size" type="checkbox" checked data-size="small" data-on-text="Yes"
                                                                                data-off-text="No">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="border-top: 0px; width: 90%">
                                                                            More than 1 sex partner during last 3 months
                                                                        </td>
                                                                        <td style="border-top: 0px; width: 10%">
                                                                            <input id="chkMorethanOneSexPrtnr" name="switch-size" type="checkbox" checked data-size="small"
                                                                                data-on-text="Yes" data-off-text="No">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="border-top: 0px; width: 90%">
                                                                            <i>(calculate the sum of 6 answers above)</i><b> Risk Score:</b>
                                                                        </td>
                                                                        <td style="border-top: 0px; width: 10%">
                                                                            <input type="text" disabled='disabled' id="lblScore" class="form-control" value="0" />
                                                                                
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <!-- Clinical TB Screening -->
                                                            <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                                                                <table class="table" style="padding-left: 10px; margin-bottom: 0px;">
                                                                    <tr>
                                                                        <td colspan="2" style="border-top: 0px; padding: 2px;">
                                                                            <div class="box box-default box-solid" style="margin-bottom: 5px;">
                                                                                <div class="box-header">
                                                                                    <h5 class="box-title">
                                                                                        Clinical TB Screening</h5>
                                                                                </div>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="border-top: 0px; width: 90%">
                                                                            Current cough
                                                                        </td>
                                                                        <td style="border-top: 0px; width: 10%">
                                                                            <input id="chkCurrentCough" name="switch-size" type="checkbox" checked data-size="small"
                                                                                data-on-text="Yes" data-off-text="No">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="border-top: 0px; width: 90%">
                                                                            Weight Loss
                                                                        </td>
                                                                        <td style="border-top: 0px; width: 10%">
                                                                            <input id="chkWeightLoss" name="switch-size" type="checkbox" checked data-size="small"
                                                                                data-on-text="Yes" data-off-text="No">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="border-top: 0px; width: 90%">
                                                                            Fever
                                                                        </td>
                                                                        <td style="border-top: 0px; width: 10%">
                                                                            <input id="chkFever" name="switch-size" type="checkbox" checked data-size="small"
                                                                                data-on-text="Yes" data-off-text="No">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="border-top: 0px; width: 90%">
                                                                            Night sweats
                                                                        </td>
                                                                        <td style="border-top: 0px; width: 10%">
                                                                            <input id="chkNightSweats" name="switch-size" type="checkbox" checked data-size="small"
                                                                                data-on-text="Yes" data-off-text="No">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="border-top: 0px; width: 90%">
                                                                            <i>(calculate the sum of 4 answers above)</i><b> TB Screening Score:</b><br />
                                                                            <i>if score>=1, test for Sputum AFB or refer to TB Service</i>
                                                                        </td>
                                                                        <td style="border-top: 0px; width: 10%">
                                                                            <input type="text" disabled='disabled' id="lblTBScreeningScore" class="form-control" value="0"/>
                                                                                
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                            <!-- Syndromic STI Screening -->
                                                            <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                                                                <table class="table" style="padding-left: 10px; margin-bottom: 0px;">
                                                                    <tr>
                                                                        <td colspan="2" style="border-top: 0px; padding: 2px;">
                                                                            <div class="box box-default box-solid" style="margin-bottom: 5px;">
                                                                                <div class="box-header">
                                                                                    <h5 class="box-title">
                                                                                        Syndromic STI Screening</h5>
                                                                                </div>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="border-top: 0px; width: 90%">
                                                                            <i>Female</i>: Complaints of vaginal discharge or burning when urinating?
                                                                        </td>
                                                                        <td style="border-top: 0px; width: 10%">
                                                                            <input id="chkSTIUrination" name="switch-size" type="checkbox" checked data-size="small"
                                                                                data-on-text="Yes" data-off-text="No">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="border-top: 0px; width: 90%">
                                                                            <i>Female</i> Complaints of lower abdominal pains with or without vaginal discharge?
                                                                        </td>
                                                                        <td style="border-top: 0px; width: 10%">
                                                                            <input id="chkVaginalPain" name="switch-size" type="checkbox" checked data-size="small"
                                                                                data-on-text="Yes" data-off-text="No">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="border-top: 0px; width: 90%">
                                                                            <i>Male</i> Complaints of urethral discharge or burning when urinating?
                                                                        </td>
                                                                        <td style="border-top: 0px; width: 10%">
                                                                            <input id="chkMaleUrinationIssue" name="switch-size" type="checkbox" checked data-size="small"
                                                                                data-on-text="Yes" data-off-text="No">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="border-top: 0px; width: 90%">
                                                                            <i>Male</i> Complaints of scrotal swelling and pain
                                                                        </td>
                                                                        <td style="border-top: 0px; width: 10%">
                                                                            <input id="chkScrotalSwePain" name="switch-size" type="checkbox" checked data-size="small"
                                                                                data-on-text="Yes" data-off-text="No">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="border-top: 0px; width: 90%">
                                                                            Complaints of genital sore(s) or swollen inguinal lymph nodes with without pains?
                                                                        </td>
                                                                        <td style="border-top: 0px; width: 10%">
                                                                            <input id="chkGenitalSore" name="switch-size" type="checkbox" checked data-size="small"
                                                                                data-on-text="Yes" data-off-text="No">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="border-top: 0px; width: 90%">
                                                                            <i>(calculate the sum of 3 answers above)</i><b> STI Screening Score:</b><br />
                                                                            <i>if score>=1, follow syndromic STI management guideline or refer</i>
                                                                        </td>
                                                                        <td style="border-top: 0px; width: 10%">
                                                                            <input type="text" disabled='disabled'  id="lblSTISCreningScore" class="form-control" value="0"/>
                                                                                
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            </div>
                                            
                                            <!--SEROLOGY REQUEST-->
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            <div id="divSerology" class="box box-default box-solid">
                                            <div class="box-header with-border">
                                                <h3 class="box-title">
                                                    Serology Request</h3>
                                                <div class="box-tools pull-right">
                                                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                                        <i class="fa fa-minus"></i>
                                                    </button>
                                                </div>
                                            </div>
                                            <div class="box-body">
                                                <div class="box box-default box-solid">
                                                    <div class="box-body">
                                                        <div class="row">
                                                            <!-- Knowledge assessment-->
                                                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                                <table class="table" style="padding-left: 10px; margin-bottom: 0px;">
                                                                    <tr>
                                                                        <td style="border-top: 0px; width: 90%">
                                                                            Initial HIV Test:
                                                                        </td>
                                                                        <td style="border-top: 0px; width: 10%">
                                                                            <input id="chkInitialHIVTest" name="switch-size" type="checkbox" checked data-size="small"
                                                                                data-on-text="Positive" data-off-text="Negative">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="border-top: 0px; width: 90%">
                                                                            Confirmatory HIV Test (Positive Results Only):
                                                                        </td>
                                                                        <td style="border-top: 0px; width: 10%">
                                                                            <input id="chkConfirmatoryHIVTest" name="switch-size" type="checkbox" checked data-size="small"
                                                                                data-on-text="Positive" data-off-text="Negative">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="border-top: 0px; width: 90%">
                                                                            Syphilis Test:
                                                                        </td>
                                                                        <td style="border-top: 0px; width: 10%">
                                                                            <input id="chkSyphilis" name="switch-size" type="checkbox" checked data-size="small"
                                                                                data-on-text="Reactive" data-off-text="Non reactive">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="border-top: 0px; width: 90%">
                                                                            Hepatitis B Test:
                                                                        </td>
                                                                        <td style="border-top: 0px; width: 10%">
                                                                            <input id="chkHepatitisB" name="switch-size" type="checkbox" checked data-size="small"
                                                                                data-on-text="Positive" data-off-text="Negative">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="border-top: 0px; width: 90%">
                                                                            Hepatitis C Test:
                                                                        </td>
                                                                        <td style="border-top: 0px; width: 10%">
                                                                            <input id="chkHepatitisC" name="switch-size" type="checkbox" checked data-size="small"
                                                                                data-on-text="Positive" data-off-text="Negative">
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </div>
                                                       
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                                
                                            <!--POST TEST COUNSELLING-->
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    <div id="div2" class="box box-default box-solid">
                                            <div class="box-header with-border">
                                                <h3 class="box-title">
                                                    POST TEST COUNSELLING</h3>
                                                <div class="box-tools pull-right">
                                                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                                        <i class="fa fa-minus"></i>
                                                    </button>
                                                </div>
                                            </div>
                                            <div class="box-body">
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                <div class="box box-default box-solid">
                                            <div class="box-body">
                                                    <div class="row">
                                        <!-- HIV Test results-->
                                        <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                                            <table class="table" style="padding-left: 10px; margin-bottom: 0px;">
                                                        
                                                <tr>
                                                    <td style="border-top: 0px; width: 90%">
                                                        HIV Test Result
                                                    </td>
                                                    <td style="border-top: 0px; width: 10%">
                                                        <input id="chkHivTestResult" name="switch-size" type="checkbox" checked data-size="small"
                                                                data-on-text="Positive" data-off-text="Negative">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="border-top: 0px; width: 90%">
                                                        Not previously tested (within a year)?
                                                    </td>
                                                    <td style="border-top: 0px; width: 10%">
                                                        <input id="chkNotTestedHIVThisYear" name="switch-size" type="checkbox" checked data-size="small"
                                                                data-on-text="Positive" data-off-text="Negative">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="border-top: 0px; width: 90%">
                                                        Previously tested positive (within a year)?
                                                    </td>
                                                    <td style="border-top: 0px; width: 10%">
                                                        <input id="chkPrevTestedNeg" name="switch-size" type="checkbox" checked data-size="small"
                                                                data-on-text="Yes" data-off-text="No">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="border-top: 0px; width: 90%">
                                                        Previously tested positive in HIV Care (within a year)?
                                                    </td>
                                                    <td style="border-top: 0px; width: 10%">
                                                        <input id="chkPrevTestdPosInHIVCare" name="switch-size" type="checkbox" checked data-size="small"
                                                               data-on-text="Yes" data-off-text="No">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="border-top: 0px; width: 90%">
                                                        Previously tested positive not in HIV Care (within a year)?
                                                    </td>
                                                    <td style="border-top: 0px; width: 10%">
                                                        <input id="chkPrevTestdPosNOTInHIVCare" name="switch-size" type="checkbox" checked data-size="small"
                                                               data-on-text="Yes" data-off-text="No">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="border-top: 0px; width: 90%">
                                                        HIV Request and Result form signed by tester(s)
                                                    </td>
                                                    <td style="border-top: 0px; width: 10%">
                                                        <input id="chkHIVResuSigndByTester" name="switch-size" type="checkbox" checked data-size="small"
                                                                data-on-text="Yes" data-off-text="No">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="border-top: 0px; width: 90%">
                                                        HIV Request and Result form filed with CT Intake Form
                                                    </td>
                                                    <td style="border-top: 0px; width: 10%">
                                                        <input id="chkFromDTIF" name="switch-size" type="checkbox" checked data-size="small"
                                                                data-on-text="Yes" data-off-text="No">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="border-top: 0px; width: 90%">
                                                        Client recieved HIV test results
                                                    </td>
                                                    <td style="border-top: 0px; width: 10%">
                                                        <input id="chkClientRcvdTestResults" name="switch-size" type="checkbox" checked data-size="small"
                                                                data-on-text="Yes" data-off-text="No">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="border-top: 0px; width: 90%">
                                                        Post test counseling done
                                                    </td>
                                                    <td style="border-top: 0px; width: 10%">
                                                        <input id="chkPostTestConsuling" name="switch-size" type="checkbox" checked data-size="small"
                                                                data-on-text="Yes" data-off-text="No">
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                               
                                        <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                                            <table class="table" style="padding-left: 10px; margin-bottom: 0px;">
                                                <tr>
                                                    <td style="border-top: 0px; width: 90%">
                                                        Risk resolution plan developed
                                                    </td>
                                                    <td style="border-top: 0px; width: 10%">
                                                        <input id="chkRiskDevPlan" name="switch-size" type="checkbox" checked data-size="small"
                                                                data-on-text="Yes" data-off-text="No">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="border-top: 0px; width: 90%">
                                                        Post test disclosure plan developed
                                                    </td>
                                                    <td style="border-top: 0px; width: 10%">
                                                        <input id="chkPostDsiclosrPlan" name="switch-size" type="checkbox" checked data-size="small"
                                                                data-on-text="Yes" data-off-text="No">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="border-top: 0px; width: 90%">
                                                        Will bring partner(s) for HIV Testing
                                                    </td>
                                                    <td style="border-top: 0px; width: 10%">
                                                        <input id="chkPartnerForHIVTesting" name="switch-size" type="checkbox" checked data-size="small"
                                                                data-on-text="Yes" data-off-text="No">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="border-top: 0px; width: 90%">
                                                        Will bring own children &lt;5 years for HIV testing
                                                    </td>
                                                    <td style="border-top: 0px; width: 10%">
                                                        <input id="chkBringChild" name="switch-size" type="checkbox" checked data-size="small"
                                                                data-on-text="Yes" data-off-text="No">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="border-top: 0px; width: 90%">
                                                        Provided with information on FP and dual contraception
                                                    </td>
                                                    <td style="border-top: 0px; width: 10%">
                                                        <input id="chkDualContra" name="switch-size" type="checkbox" checked data-size="small"
                                                                data-on-text="Yes" data-off-text="No">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="border-top: 0px; width: 90%">
                                                        Client/Partner use FP methods (other than condom)
                                                    </td>
                                                    <td style="border-top: 0px; width: 10%">
                                                        <input id="chkFPMethodUsed" name="switch-size" type="checkbox" checked data-size="small"
                                                                data-on-text="Yes" data-off-text="No">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="border-top: 0px; width: 90%">
                                                        Client/Partner use condoms as (one) FP method
                                                    </td>
                                                    <td style="border-top: 0px; width: 10%">
                                                        <input id="chkCondomasFP" name="switch-size" type="checkbox" checked data-size="small"
                                                                data-on-text="Yes" data-off-text="No">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="border-top: 0px; width: 90%">
                                                        Correct condom use demonstrated
                                                    </td>
                                                    <td style="border-top: 0px; width: 10%">
                                                        <input id="chkCondomUseDemonstrated" name="switch-size" type="checkbox" checked data-size="small"
                                                                data-on-text="Yes" data-off-text="No">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="border-top: 0px; width: 90%">
                                                        Condoms provided to client
                                                    </td>
                                                    <td style="border-top: 0px; width: 10%">
                                                        <input id="chkCondomProvided" name="switch-size" type="checkbox" checked data-size="small"
                                                                data-on-text="Yes" data-off-text="No">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="border-top: 0px; width: 90%">
                                                        Client referred to other services
                                                    </td>
                                                    <td style="border-top: 0px; width: 10%">
                                                        <input id="chkClientReferredToOther" name="switch-size" type="checkbox" checked data-size="small"
                                                                data-on-text="Yes" data-off-text="No">
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                            <i>If client test HIV negative, has an HIV Risk Assessment Score of 1 and above, or there is evidence of an STISyndrome, recommended tre-testing after 3 months.</i>
                                        </div>
                                    </div>
                                                    <div class="row">
                                        <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                                            <table class="table" style="padding-left: 10px; margin-bottom: 0px;">
                                                <tr>
                                                    <td style="border-top: 0px; width: 90%">
                                                        HIV Test Result
                                                    </td>
                                                    <td style="border-top: 0px; width: 10%">
                                                        <input id="chkHivTestResult2" name="switch-size" type="checkbox" checked data-size="small"
                                                                data-on-text="Positive" data-off-text="Negative">
                                                    </td>
                                                </tr>
                                            </table>    
                                        </div>
                                    </div>
                                            </div>
                                        </div>
                                        </div>
                                            </div>  
                                                      
                                            <%--SYPHILIS & HEPATITIS TESTING--%>
                                                                                                                                                                                                                                                                                        <div id="divSypHepTesting" class="box box-default box-solid">
                                                <div class="box-header with-border">
                                                    <h3 class="box-title">Syphilis & Hepatitis Tesing</h3>
                                                <div class="box-tools pull-right">
                                                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                                        <i class="fa fa-minus"></i>
                                                    </button>
                                                </div>
                                            </div>
                                             <div class="box-body">
                                                <div class="box box-default box-solid">
                                                    <div class="box-body">
                                                        <div class="row">
                                                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                            <table class="table" style="padding-left: 10px; margin-bottom: 0px;">
                                                        
                                                                <tr>
                                                                    <td style="border-top: 0px; width: 90%">
                                                                        SYPHILIS TEST RESULTS
                                                                    </td>
                                                                    <td style="border-top: 0px; width: 10%">
                                                                        <input id="chkSyphilisTestResult" name="switch-size" type="checkbox" checked data-size="small"
                                                                               data-on-text="Positive" data-off-text="Negative"/>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="border-top: 0px; width: 90%">
                                                                        Hepatitis B Virus Test Result
                                                                    </td>
                                                                    <td style="border-top: 0px; width: 10%">
                                                                        <input id="chkHepatitisBTest" name="switch-size" type="checkbox" checked data-size="small"
                                                                               data-on-text="Positive" data-off-text="Negative"/>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="border-top: 0px; width: 90%">
                                                                        Hepatitis C Virus Test Result
                                                                    </td>
                                                                    <td style="border-top: 0px; width: 10%">
                                                                        <input id="chkHepatitisC" name="switch-size" type="checkbox" checked data-size="small"
                                                                               data-on-text="Yes" data-off-text="No"/>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="border-top: 0px; width: 100%">
                                                                        Comments
                                                                    
                                                                        <textarea id="txtComments" rows="2" cols="4" class="form-control"  maxlength="1000"></textarea>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            
                        <div class="box-body">
                            <div class="box box-default box-solid">
                                <div class="box-body">
                                            
                        <div class="row">
                            <!-- Clinical TB Screening -->
                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                <div class="col-md-3 col-sm-12 col-xs-12 ">
                                    <label for="lblRequestedBy" class="control-label">
                                        Requested By:</label>
                                    <select id="cmbRequestedBy" class="form-control">
                                                                           
                                    </select>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 ">
                                    <label for="lblRequestedDate" class="control-label">
                                        Requested Date:</label>
                                    <input type="text" class="form-control" id="txtRequestdDate" placeholder="Date" data-date-format="dd-M-yyyy">
                                </div>
                                                                        
                                <div class="col-md-3 col-sm-12 col-xs-12 ">
                                    <label for="lblTestedBy" class="control-label">
                                        Tested By:</label>
                                    <select id="cmbTestedBy" class="form-control">
                                                                       
                                    </select>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 ">
                                    <label for="lblTestedDate" class="control-label">
                                        Tested Date:</label>
                                    <input type="text" class="form-control" id="txtTestedDate" placeholder="Date" data-date-format="dd-M-yyyy">
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <!-- Clinical TB Screening -->
                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                <div class="col-md-3 col-sm-12 col-xs-12 ">
                                    <label for="lblCheckdBy" class="control-label">
                                        Checked By:</label>
                                    <select id="cmbChkdBy" class="form-control">
                                                                      
                                    </select>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 ">
                                    <label for="lblChekdByDate" class="control-label">
                                        Checked Date:</label>
                                    <input type="text" class="form-control" id="TxtchkdDate" placeholder="Date" data-date-format="dd-M-yyyy">
                                </div>
                            </div>
                        </div>
                                </div>         
                            </div>
                        </div>
                                        </div>         
                                            </div>
                                        </div>
                                    </div>
                                 
<div class="row no-print" align="center">
<div class="form-group">
<div class="col-md-12" align="center">
<button type="button" class="btn btn-primary" onclick="closeform();">
Close<span class="glyphicon glyphicon-remove-circle" style="padding-left: 5px;"></span></button>
<button type="button" class="btn btn-primary" onclick="SaveCIF();" id="btnSaveCIFSave" clientidmode="Static" runat="server">
Save<span class="glyphicon glyphicon-floppy-disk" style="padding-left: 5px;"></span></button>
</div>
</div>
</div>
                                </div>
                            </div>   
                        </div>
                    </div>
                </div>
            </div>
          
        </div>
    </div>
</asp:Content>
