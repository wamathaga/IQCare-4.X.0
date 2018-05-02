$(document).ready(function () {
    $.hivce.loader('show');
    GetPatientAdherenceBarriers();
});

function GetPatientAdherenceBarriers() {
    activePage = getPageName() + '.aspx';
    var postData;
    $.ajax({
        type: "POST",
        url: activePage + "?data=getdata",
        //data: JSON.stringify(nodeId),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,
        cache: false,
        beforeSend: function () {
        },
        success: function (response) {
            console.log(response);
            var responseSuccess = response.Success;
            if (responseSuccess != null) {
                if (responseSuccess == "false") {
                    customAlert(errorMessage);
                }
            }
            else {
                InitControls(response);
            }
        },
        error: function (msg) {
        }
    });
}

function InitControls(response) {
    window.scrollTo(0, 0);

    $("textarea").maxlength({
        //alwaysShow: true,
        threshold: 50,
        warningClass: "labelTextBox label label-success",
        limitReachedClass: "labelTextBox label label-danger",
        separator: ' out of ',
        preText: 'You write ',
        postText: ' characters.',
        validate: true,
        placement: 'top'
    });

    //Date picker
    $('input[type="checkbox"]').on('switchChange.bootstrapSwitch', function (event, state) {
        DisableAdherenceBarriersDiv(this);
    });

    $('#divSupportSystem input[type="radio"]').iCheck({
        checkboxClass: 'icheckbox_flat-blue',
        radioClass: 'iradio_flat-blue'
    });

    // Section 1
    $("#chkAcceptedHIVStatus").bootstrapSwitch('state', false);
    $("#chkIsAgeAppropriate").bootstrapSwitch('state', false);

    $("#chkRiskOfTransmission").bootstrapSwitch('state', false);
    $("#chkUnderstandART").bootstrapSwitch('state', false);

    $("#chkARTSideEffect").bootstrapSwitch('state', false);
    $("#chkAdherenceBenefits").bootstrapSwitch('state', false);

    $("#chkConsequencesNonAdherence").bootstrapSwitch('state', false);

    // Section 2
    /*
    txtTypicalDay
    txtMedicinesIntake
    txtTravelVisitDescription
    txtPrimartCareGiver
    */

    // Section 3
    //txtLiveWith
    //txtHIVStatusArtDiscussion
    //rdTreatmentbuddy / supportSystem
    //rdSupportSystem / supportSystem
    //txtSupportSystemDetails
    $("#chkChangesInRelationship").bootstrapSwitch('state', false);
    //txtChangesInRelationship
    $("#chkHIVStatusBotherDetails").bootstrapSwitch('state', false);
    //txtHIVStatusBotherDetails
    $("#chkPeopleTreatmentDetails").bootstrapSwitch('state', false);
    //txtPeopleTreatmentDetails
    $("#chkStigmaInterfering").bootstrapSwitch('state', false);
    //txtStigmaInterfering
    $("#chkReligiousBeliefs").bootstrapSwitch('state', false);


    // Section 4
    //txtTotal
    //txtDepressionSeverity
    //txtRecommendedManagement
    $("#chkReferredOtherServices").bootstrapSwitch('state', false);
    $("#chkAppointmentsAttend").bootstrapSwitch('state', false);
    //txtExperienceAppointReorganized
    BindControl(response);
}

function BindControl(response) {

    // Section 1
    $("#chkAcceptedHIVStatus").bootstrapSwitch('state', ConvertValueBool(response.AHIVS));
    $("#chkIsAgeAppropriate").bootstrapSwitch('state', ConvertValueBool(response.ISAA));
    $("#chkRiskOfTransmission").bootstrapSwitch('state', ConvertValueBool(response.ROT));
    $("#chkUnderstandART").bootstrapSwitch('state', ConvertValueBool(response.UART));
    $("#chkARTSideEffect").bootstrapSwitch('state', ConvertValueBool(response.ARTSE));
    $("#chkAdherenceBenefits").bootstrapSwitch('state', ConvertValueBool(response.AB));
    $("#chkConsequencesNonAdherence").bootstrapSwitch('state', ConvertValueBool(response.CNA));

    // Section 2
    $("#txtTypicalDay").val(response.TD);
    $("#txtMedicinesIntake").val(response.MIT);
    $("#txtTravelVisitDescription").val(response.TVD);
    $("#txtPrimartCareGiver").val(response.PCG);


    // Section 3
    $("#txtLiveWith").val(response.LW);
    $("#txtHIVStatusArtDiscussion").val(response.HIVSAD);
    $("input:radio[name=supportSystem][value=" + response.SS + "]").attr('checked', 'checked');
    $("input:radio[name=supportSystem]").iCheck('update');
    $("#txtSupportSystemDetails").val(response.SSD);

    if (jQuery.isEmptyObject(response.CINR) == false) {
        if (response.CINR.length > 0) {
            $("#chkChangesInRelationship").bootstrapSwitch('state', true);
        }
    }
    $("#txtChangesInRelationship").val(response.CINR);

    if (jQuery.isEmptyObject(response.HIVBD) == false) {
        if (response.HIVBD.length > 0) {
            $("#chkHIVStatusBotherDetails").bootstrapSwitch('state', true);
        }
    }
    $("#txtHIVStatusBotherDetails").val(response.HIVBD);

    if (jQuery.isEmptyObject(response.PTD) == false) {
        if (response.PTD.length > 0) {
            $("#chkPeopleTreatmentDetails").bootstrapSwitch('state', true);
        }
    }
    $("#txtPeopleTreatmentDetails").val(response.PTD);

    if (jQuery.isEmptyObject(response.SI) == false) {
        if (response.SI.length > 0) {
            $("#chkStigmaInterfering").bootstrapSwitch('state', true);
        }
    }
    $("#txtStigmaInterfering").val(response.SI);

    if (jQuery.isEmptyObject(response.RB) == false) {
        if (response.RB.length > 0) {
            $("#chkReligiousBeliefs").bootstrapSwitch('state', true);
        }
    }
    $("#txtReligiousBeliefs").val(response.RB);

    // Section 4
    $("#txtTotal").val(response.Total);
    $("#txtDepressionSeverity").val(response.DS);
    $("#txtRecommendedManagement").val(response.Rec);
    $("#chkReferredOtherServices").bootstrapSwitch('state', ConvertValueBool(response.ROS));
    $("#chkAppointmentsAttend").bootstrapSwitch('state', ConvertValueBool(response.AA));
    $("#txtExperienceAppointReorganized").val(response.EAR);

    $.hivce.loader('hide');
}

function GetPreviousPage(target) {
    $(".nav-tabs a[href=\"#" + target + "\"]").tab("show");
}

function GetNextPage(target) {
    $(".nav-tabs a[href=\"#" + target + "\"]").tab("show");
}

function PartialSavePatientAdherenceBarriers(tabName) {
    $.hivce.loader('show');

    var data = PreparePatientAdherenceBarriersData();

    PostPatientAdherenceBarriersData(data, true, tabName);
}

function SavePatientAdherenceBarriers() {
    $.hivce.loader('show');

    var data = PreparePatientAdherenceBarriersData();

    PostPatientAdherenceBarriersData(data, false, null);
}
function PreparePatientAdherenceBarriersData() {

    rowData = [];
    var rbSupportSystem = $("input:radio[name=supportSystem]:checked").val();
    var urSupportSystem = (rbSupportSystem === undefined) ? 0 : rbSupportSystem;


    rowData.push({
        AA: GetSwitchValue("chkAppointmentsAttend"),
        EAR: $("#txtExperienceAppointReorganized").val(),
        HIVBD: $("#txtHIVStatusBotherDetails").val(),
        HIVSAD: $("#txtHIVStatusArtDiscussion").val(),
        PTD: $("#txtPeopleTreatmentDetails").val(),
        RB: $("#txtReligiousBeliefs").val(),
        ROS: GetSwitchValue("chkReferredOtherServices"),
        SI: $("#txtStigmaInterfering").val(),
        SS: urSupportSystem,
        SSD: $("#txtSupportSystemDetails").val(),
        TVD: $("#txtTravelVisitDescription").val(),
        PAM_Id: 0,
        Ptn_Pk: 0,
        VId: 0,
        LocationId: 0,
        AB: GetSwitchValue("chkAdherenceBenefits"),
        AHIVS: GetSwitchValue("chkAcceptedHIVStatus"),
        ARTSE: GetSwitchValue("chkARTSideEffect"),
        CINR: $("#txtChangesInRelationship").val(),
        CNA: GetSwitchValue("chkConsequencesNonAdherence"),
        ISAA: GetSwitchValue("chkIsAgeAppropriate"),
        LW: $("#txtLiveWith").val(),
        MIT: $("#txtMedicinesIntake").val(),
        PCG: $("#txtPrimartCareGiver").val(),
        ROT: GetSwitchValue("chkRiskOfTransmission"),
        TD: $("#txtTypicalDay").val(),
        UART: GetSwitchValue("chkUnderstandART")
    });

    return rowData[0];
}

function PostPatientAdherenceBarriersData(rowData, isPartial, tabName) {

    var action = "savedata";
    var rowData1 = rowData;
    var errorMsg = ''; //CheckMXBlankValues();
    activePage = getPageName() + '.aspx';

    if (isNaN(errorMsg)) {
        customAlert(errorMsg);
        return;
    }
    else {
        $.hivce.loader('show');
        var responseObject = null;
        $.ajax({
            type: "POST",
            //url: "ClinicalEncounter.aspx?data=" + action,
            url: activePage + "?data=" + action,
            data: JSON.stringify(rowData1),
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            async: true,
            cache: false,
            beforeSend: function () {
            },
            success: function (response) {
                //debugger;
                //console.log(response);
                var responseSuccess = response.Success;
                var msg = response.EM;
                if (responseSuccess == "true") {
                    //GetManagementData();
                    customAlert("Patient Adherence Barriers " + dataSuccessMessage.toLowerCase());
                    if (isPartial == false) {
                        window.location.assign("../ClinicalForms/frmPatient_History.aspx?&sts=0");
                    } else {
                        if (tabName.length > 0) {
                            GetNextPage(tabName);
                        }
                    }
                }
                else {
                    if (responseSuccess == "false") {
                        //customAlert(errorMessage);
                        if (msg != null) {
                            if (msg.toLowerCase().indexOf("visit already exists") >= 0) {
                                //string contains hello
                                customAlert(msg);
                            }
                            else {
                                customAlert(errorMessage);
                            }
                        }
                        else {
                            customAlert(errorMessage);
                        }
                    }
                    else {
                        customAlert(responseSuccess);
                    }
                }
                $.hivce.loader('hide');
            },
            error: function (xhr, textStatus, errorThrown) {
                ShowErrorMessage(msg);
            }
            //error: function (msg) {
            //    ShowErrorMessage(msg);
            //}
        });
    }
}


function DeleteForm() {
    //alert(GetParameterValues('name'));
    $.hivce.loader('show');
    var formName = '';
    activePage = getPageName() + '.aspx';

    var postData;
    $.ajax({
        type: "POST",
        //url: "ClinicalEncounter.aspx?data=getAddtionalHx",
        url: activePage + "?data=deleteform",
        //data: '{formName: "' + formName + '" }',
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,
        cache: false,
        beforeSend: function () {
        },
        success: function (response) {
            console.log(response);
            var responseSuccess = response.Success;
            if (responseSuccess != null) {
                if (responseSuccess == "false") {
                    customAlert(errorMessage);
                }
            }
            else {
                //string theUrl;
                //theUrl = string.Format("{0}", "frmPatient_Home.aspx?Func=Delete");
                //Response.Redirect(theUrl);
                window.open("../ClinicalForms/frmPatient_Home.aspx?Func=Delete", "_self");
            }
        },
        error: function (msg) {
        }
    });
}

