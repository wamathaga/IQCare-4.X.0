var arrMMAS4Score4 = [];
var arrMMAS8Score4 = [];

function Init_UC_SessionVL_Controls(response) {
    debugger;
    window.scrollTo(0, 0);

    $("#chkCaseManager5").bootstrapSwitch('state', false);
    if (jQuery.isEmptyObject(response.ACM) == false) {
        if (response.ACM > 0) {
            $("#chkCaseManager5").bootstrapSwitch('state', true);
        }
    }
   
    $("#txtAreaExperience5").val(response.EAR);

    $("#chkBenefitHomeVisit5").bootstrapSwitch('state', false);
    if (jQuery.isEmptyObject(response.BHV) == false) {
        if (response.BHV > 0) {
            $("#chkBenefitHomeVisit5").bootstrapSwitch('state', true);
        }
    }

    $.hivce.loader('hide');
}

function SaveSessionVL() {
    debugger;
    $.hivce.loader('show');
    var sessionVLData = PrepareSessionVLData();
    PostSessionVLData(sessionVLData);
}

function PrepareSessionVLData() {
    debugger;
    rowSessionVLData = [];

    var chkCaseManager = GetSwitchValue("chkCaseManager5");
    var txtAreaExperience = $("#txtAreaExperience5").val();
    var chkBenefitHomeVisit = GetSwitchValue("chkBenefitHomeVisit5");
    
    rowSessionVLData.push({
        Ptn_Pk: 0,
        LId: 0,
        VId: 0,
        ACM: chkCaseManager,
        CS: 5,
        EAR: txtAreaExperience,
        BHV: chkBenefitHomeVisit,
    });

    return rowSessionVLData[0];
}

function PostSessionVLData(rowData) {
    debugger;
    var action = "saveSessionVL";
    var rowData1 = rowData;
    var errorMsg = CheckMXBlankValues();

    if (isNaN(errorMsg)) {
        customAlert(errorMsg);
        return;
    }
    else {
        // $.hivce.loader('show');
        var responseObject = null;
        $.ajax({
            type: "POST",
            url: "EnhanceAdherence.aspx?data=" + action,
            data: JSON.stringify(rowData1),
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            async: true,
            cache: false,
            beforeSend: function () {
            },
            success: function (response) {
                debugger;
                console.log(response);
                var responseSuccess = response.Success;
                var msg = response.EM;
                if (responseSuccess == "true") {
                    //GetSession4Data();
                    customAlert("Session VL tab " + dataSuccessMessage.toLowerCase());
                     window.location.assign("../ClinicalForms/frmPatient_History.aspx?&sts=0");
                }
                else {
                    if (responseSuccess == "false") {
                        customAlert(msg);
                    }
                    else {
                        customAlert(responseSuccess);
                    }
                }
                $.hivce.loader('hide');
            },
            error: function (xhr, textStatus, errorThrown) {
                //ShowErrorMessage(msg);
                console.log(xhr);
                console.log(textStatus);
                console.log(errorThrown);
            }
            //            error: function (msg) {
            //                ShowErrorMessage(msg);
            //            }
        });
    }
}

function CheckMXBlankValues() {
    errorMsg = '';
    //    var errorField = '';
    //    var ddlCTXAdhere = $("#ddlCTXAdhere").select2("val");
    //    var ddlARVAdhere = $("#ddlARVAdhere").select2("val");
    //    var ddlPWP = $("#ddlPWP").select2("val");

    //    var visitType = $("#ddlVisitType").select2("val");
    //    var visitTypeData = $("#ddlVisitType").select2('data')[0];


    //    if (visitType != null) {
    //        if (visitTypeData.text.toUpperCase().indexOf("SUPPORTER") == -1) {

    //            if (ddlCTXAdhere == null) {
    //                errorField += 'CTX/Dapson: Adherence ';
    //            }

    //            var hidARTStartDate = $("#hidARTStartDate").val();
    //            if (ddlARVAdhere == null && hidARTStartDate.length > 0) {
    //                if (errorField.length > 1) {
    //                    errorField += ', ';
    //                }
    //                errorField += 'ARV: Adherence ';
    //            }
    //        }
    //    }

    //    var ddlsubsituationInterruption = $("#ddlsubsituationInterruption").select2("val");
    //    if (ddlsubsituationInterruption == null) {
    //        if (errorField.length > 1) {
    //            errorField += ', ';
    //        }
    //        errorField += 'Substitutions/Interruptions ';
    //    }

    //    if (ddlsubsituationInterruption == 96 || ddlsubsituationInterruption == 97 || ddlsubsituationInterruption == 98) {
    //        var ddlRegimenCode = $("#ddlRegimenCode").select2("val");
    //        if (ddlRegimenCode == null) {
    //            if (errorField.length > 1) {
    //                errorField += ', ';
    //            }
    //            errorField += 'Regimen Code ';
    //        }
    //    }

    //    var age = $("#hidDOB").val();
    //    if (age > 14) {

    //        if (ddlPWP == null) {
    //            if (errorField.length > 1) {
    //                errorField += ', ';
    //            }
    //            errorField += 'Prevention with positives ';
    //        }
    //    }

    //    var chkKeyPopulation = GetSwitchValue("chkKeyPopulation");

    //    var rbgKP = $("input:radio[name=rbgKP]:checked").val();
    //    var urbgKP = (rbgKP === undefined) ? 0 : rbgKP;
    //    if (chkKeyPopulation > 0) {
    //        if (urbgKP == 0) {
    //            if (errorField.length > 1) {
    //                errorField += ', ';
    //            }
    //            errorField += 'Key Population ';
    //        }
    //    }

    //    //    if ($("#dtNextAppointmentDate").val().length == 0) {
    //    //        if (errorField.length > 1) {
    //    //            errorField += ', ';
    //    //        }
    //    //        errorField += 'Next Appointment date ';
    //    //    }


    //    if (isNaN(errorField)) {
    //        errorMsg = errorField + 'cannot be blank.';
    //    }
    return errorMsg;
}

