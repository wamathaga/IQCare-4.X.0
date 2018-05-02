var chkStillActive;
var activePage;
var currCIF;
var rowData;
var arrRiskScore = [];
var arrSTIScore = [];
var arrTBScore = [];

$(document).ready(function () {
    $.hivce.loader('show');
    //debugger;
    GetCIFData();
});


function Init_Controls(response) {

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

    $("#txtDOV").datepicker({ autoclose: true });
    $("#TxtchkdDate").datepicker({ autoclose: true });
    $("#txtTestedDate").datepicker({ autoclose: true });
    $("#txtRequestdDate").datepicker({ autoclose: true });
    
    CheckDatenAssign(response.DOV, "txtDOV", false);
    CheckDatenAssign(response.CheckedDate, "TxtchkdDate", false);
    CheckDatenAssign(response.TestedDate, "txtTestedDate", false);
    CheckDatenAssign(response.RequestedDate, "txtRequestdDate", false);

    $("#txtClientCode").val(response.PatientCode);
    $("#txtchildLess5").val(response.NoOfChildLessThan5);
    $("#txtNoOfWives").val(response.NoOfWives);
    $("#txtClientIndexID").val(response.IndexClientID);
    $("#txtComments").val(response.Comments);

    $("#cmbReferredFrom").select2().val(response.ReferFrom).trigger("change");
    $("#cmdSetting").select2().val(response.Setting).trigger("change");
    $("#cmbSOR").select2().val(response.PatientState).trigger("change");
    $("#cmbLGAOR").select2().val(response.PatientLga).trigger("change");
    $("#cmbMaritalStatus").select2().val(response.PatientMaritalStat).trigger("change");
    $("#cmbRequestedBy").select2().val(response.RequestedBy).trigger("change");
    $("#cmbTestedBy").select2().val(response.TestedBy).trigger("change");
    $("#cmbChkdBy").select2().val(response.CheckedBy).trigger("change");
    
    $("#chkFTV").bootstrapSwitch('state', ConvertValueBool(response.FirstTimeVisit));
    $("#chkTypeOfSession").bootstrapSwitch('state', ConvertValueBool(response.TypeOfSession));
    $("#chkIndexTesting").bootstrapSwitch('state', ConvertValueBool(response.IsClientIsIndex));
    $("#chkIfYesIndex").bootstrapSwitch('state', ConvertValueBool(response.IndexTestingType));

    $("#chkPrevHIVTested").bootstrapSwitch('state', ConvertValueBool(response.PrevTestNeg));
    $("#chkClientProg").bootstrapSwitch('state', ConvertValueBool(response.ClientPreg));
    $("#chkHIVTransRoutes").bootstrapSwitch('state', ConvertValueBool(response.InformHivTransRoute));
    $("#chkHIVRiskFactor").bootstrapSwitch('state', ConvertValueBool(response.InformHivTransRiskFactor));
    $("#chkHIVTransmPreven").bootstrapSwitch('state', ConvertValueBool(response.InformHivTransMethod));
    $("#chkPossibleTestResults").bootstrapSwitch('state', ConvertValueBool(response.InformHivPossiResults));
    $("#chkHIVConsent").bootstrapSwitch('state', ConvertValueBool(response.InformConsentHIVTesting));

    $("#chkSexualIntercourse").bootstrapSwitch('state', ConvertValueBool(response.EvrHadSex));
    $("#chkSexualIntercourse").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateRiskScore("si", GetSwitchValue("chkSexualIntercourse"));
    });
    $("#chkBloodTransFusion").bootstrapSwitch('state', ConvertValueBool(response.BlodTransIn3Mnth));
    $("#chkBloodTransFusion").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateRiskScore("btf", GetSwitchValue("chkBloodTransFusion"));
    });
    $("#chkSexinLast3Months").bootstrapSwitch('state', ConvertValueBool(response.SexWithCasPartnr));
    $("#chkSexinLast3Months").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateRiskScore("slm", GetSwitchValue("chkSexinLast3Months"));
    });
    $("#chkRegSexPartner").bootstrapSwitch('state', ConvertValueBool(response.SexWithRegPartnr));
    $("#chkRegSexPartner").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateRiskScore("rsp", GetSwitchValue("chkRegSexPartner"));
    });
    $("#chkSTI").bootstrapSwitch('state', ConvertValueBool(response.LastStiDone));
    $("#chkSTI").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateRiskScore("sti", GetSwitchValue("chkSTI"));
    });
    $("#chkMorethanOneSexPrtnr").bootstrapSwitch('state', ConvertValueBool(response.OnceAMonthSex));
    $("#chkMorethanOneSexPrtnr").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateRiskScore("sxp", GetSwitchValue("chkMorethanOneSexPrtnr"));
    });

    CalculateRiskScore("si", GetSwitchValue("chkSexualIntercourse"));
    CalculateRiskScore("btf", GetSwitchValue("chkBloodTransFusion"));
    CalculateRiskScore("slm", GetSwitchValue("chkSexinLast3Months"));
    CalculateRiskScore("rsp", GetSwitchValue("chkRegSexPartner"));
    CalculateRiskScore("sti", GetSwitchValue("chkSTI"));
    CalculateRiskScore("sxp", GetSwitchValue("chkMorethanOneSexPrtnr"));

    $("#chkCurrentCough").bootstrapSwitch('state', ConvertValueBool(response.CurrentCough));
    $("#chkCurrentCough").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateTBScore("cc", GetSwitchValue("chkCurrentCough"));
    });
    $("#chkWeightLoss").bootstrapSwitch('state', ConvertValueBool(response.WeightLoss));
    $("#chkWeightLoss").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateTBScore("wl", GetSwitchValue("chkWeightLoss"));
    });
    $("#chkFever").bootstrapSwitch('state', ConvertValueBool(response.Fever));
    $("#chkFever").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateTBScore("fv", GetSwitchValue("chkFever"));
    });
    $("#chkNightSweats").bootstrapSwitch('state', ConvertValueBool(response.NightSweats));
    $("#chkNightSweats").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateTBScore("ns", GetSwitchValue("chkNightSweats"));
    });

    CalculateTBScore("cc", GetSwitchValue("chkCurrentCough"));
    CalculateTBScore("wl", GetSwitchValue("chkWeightLoss"));
    CalculateTBScore("fv", GetSwitchValue("chkFever"));
    CalculateTBScore("ns", GetSwitchValue("chkNightSweats"));

    $("#chkSTIUrination").bootstrapSwitch('state', ConvertValueBool(response.FemaleVaginalDischarge));
    $("#chkSTIUrination").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateSTIScore("uri", GetSwitchValue("chkSTIUrination"));
    });
    $("#chkVaginalPain").bootstrapSwitch('state', ConvertValueBool(response.FemaleAbdominalPain));
    $("#chkVaginalPain").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateSTIScore("vp", GetSwitchValue("chkVaginalPain"));
    });
    $("#chkScrotalSwePain").bootstrapSwitch('state', ConvertValueBool(response.MaleScrotalSweling));
    $("#chkScrotalSwePain").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateSTIScore("sp", GetSwitchValue("chkScrotalSwePain"));
    });
    $("#chkMaleUrinationIssue").bootstrapSwitch('state', ConvertValueBool(response.MaleUrethalDischarge));
    $("#chkMaleUrinationIssue").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateSTIScore("mi", GetSwitchValue("chkMaleUrinationIssue"));
    });
    $("#chkGenitalSore").bootstrapSwitch('state', ConvertValueBool(response.GenitalSore));
    $("#chkGenitalSore").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateSTIScore("gs", GetSwitchValue("chkGenitalSore"));
    });
    //Fill array for stored values
    CalculateSTIScore("uri", GetSwitchValue("chkSTIUrination"));
    CalculateSTIScore("vp", GetSwitchValue("chkVaginalPain"));
    CalculateSTIScore("sp", GetSwitchValue("chkScrotalSwePain"));
    CalculateSTIScore("mi", GetSwitchValue("chkMaleUrinationIssue"));
    CalculateSTIScore("gs", GetSwitchValue("chkGenitalSore"));

    $("#chkInitialHIVTest").bootstrapSwitch('state', ConvertValueBool(response.InitialHIVTest));
    $("#chkConfirmatoryHIVTest").bootstrapSwitch('state', ConvertValueBool(response.ConfirmatoryHIVTest));
    $("#chkSyphilis").bootstrapSwitch('state', ConvertValueBool(response.SyphilisTest));
    $("#chkHepatitisB").bootstrapSwitch('state', ConvertValueBool(response.HepatitisB));
    $("#chkHepatitisC").bootstrapSwitch('state', ConvertValueBool(response.HepatitisC));

    $("#chkHivTestResult").bootstrapSwitch('state', ConvertValueBool(response.HIVTestResult));
    $("#chkNotTestedHIVThisYear").bootstrapSwitch('state', ConvertValueBool(response.NotPrevTested));
    $("#chkPrevTestedNeg").bootstrapSwitch('state', ConvertValueBool(response.PrevTestNeg));
    $("#chkPrevTestdPosInHIVCare").bootstrapSwitch('state', ConvertValueBool(response.PrevTestPosInHivCare));
    $("#chkPrevTestdPosNOTInHIVCare").bootstrapSwitch('state', ConvertValueBool(response.PrevTestPosNotInHivCare));

    $("#chkHIVResuSigndByTester").bootstrapSwitch('state', ConvertValueBool(response.HIVReqResFormSigned));
    $("#chkFromDTIF").bootstrapSwitch('state', ConvertValueBool(response.HIVReqResFormWithCI));

    $("#chkClientRcvdTestResults").bootstrapSwitch('state', ConvertValueBool(response.ClientRecHivTestResult));
    $("#chkPostTestConsuling").bootstrapSwitch('state', ConvertValueBool(response.PostTestConslDone));
    $("#chkRiskDevPlan").bootstrapSwitch('state', ConvertValueBool(response.RiskReducPlan));
    $("#chkPostDsiclosrPlan").bootstrapSwitch('state', ConvertValueBool(response.PostTestDiscloPlan));
    $("#chkPartnerForHIVTesting").bootstrapSwitch('state', ConvertValueBool(response.BringPartForHIVTest));
    $("#chkBringChild").bootstrapSwitch('state', ConvertValueBool(response.BrngChildForHIVTest));

    $("#chkDualContra").bootstrapSwitch('state', ConvertValueBool(response.FPAndDualConcInfo));
    $("#chkFPMethodUsed").bootstrapSwitch('state', ConvertValueBool(response.PartnerUseFPMethd));
    $("#chkCondomasFP").bootstrapSwitch('state', ConvertValueBool(response.PrntUseCondomAsFPM));
    $("#chkCondomUseDemonstrated").bootstrapSwitch('state', ConvertValueBool(response.CondomUseDemo));
    $("#chkCondomProvided").bootstrapSwitch('state', ConvertValueBool(response.CondomsProvided));

    $("#chkClientReferredToOther").bootstrapSwitch('state', ConvertValueBool(response.ClientRefferedToOther));
    $("#chkHivTestResult2").bootstrapSwitch('state', ConvertValueBool(response.HIVTestResult));
    $("#chkSyphilisTestResult").bootstrapSwitch('state', ConvertValueBool(response.SyphilisTest));
    $("#chkHepatitisBTest").bootstrapSwitch('state', ConvertValueBool(response.HepatitisB));
    $("#chkHepatitisC").bootstrapSwitch('state', ConvertValueBool(response.HepatitisC));
    $.hivce.loader('hide');
}

function getPageName() {
    var url = window.location.pathname;
    var index = url.lastIndexOf("/") + 1;
    var filenameWithExtension = url.substr(index);
    var filename = filenameWithExtension.split(".")[0]; // <-- added this line
    return filename;                                    // <-- added this line
}

function stripTrailingSlash(str) {
    if (str.substr(-1) == '/') {
        return str.substr(0, str.length - 1);
    }
    return str;
}

function CalculateRiskScore(ref, val) {
    
    var chkData;
    //if (jQuery.isEmptyObject(arrRiskScore) == false) {
        chkData = $.grep(arrRiskScore, function(e) { return e.Ref.toLowerCase() == ref.toLowerCase(); });

        if (jQuery.isEmptyObject(chkData) == true) {
            arrRiskScore.push({ Ref: ref, Val: val });
        } else {
            $.each(arrRiskScore,
                function(index, arrD) {
                    if (jQuery.isEmptyObject(arrD) == false) {
                        if (arrD.Ref.toLowerCase() == ref.toLowerCase()) {
                            arrD.Val = val;
                        }
                    }
                });
        }
    
    CalculateMMSImpl();
   
    //}
}

function CalculateTBScore(ref, val) {
    debugger;
    var chkData;
    //if (jQuery.isEmptyObject(arrTBScore) == true) {
        chkData = $.grep(arrTBScore, function(e) { return e.Ref.toLowerCase() == ref.toLowerCase(); });

        if (jQuery.isEmptyObject(chkData) == true) {
            arrTBScore.push({ Ref: ref, Val: val });
        } else {
            $.each(arrTBScore,
                function(index, arrD) {
                    if (jQuery.isEmptyObject(arrD) == false) {
                        if (arrD.Ref.toLowerCase() == ref.toLowerCase()) {
                            arrD.Val = val;
                        }
                    }
                });
        }
   // }
    CalculateMMSImpl();
    
}

function CalculateSTIScore(ref, val) {
    var chkData;
    debugger;
   // if (jQuery.isEmptyObject(arrSTIScore) == true) {
        chkData = $.grep(arrSTIScore, function(e) { return e.Ref.toLowerCase() == ref.toLowerCase(); });

        if (jQuery.isEmptyObject(chkData) == true) {
            arrSTIScore.push({ Ref: ref, Val: val });
        } else {
            $.each(arrSTIScore,
                function(index, arrD) {
                    if (jQuery.isEmptyObject(arrD) == false) {
                        if (arrD.Ref.toLowerCase() == ref.toLowerCase()) {
                            arrD.Val = val;
                        }
                    }
                });
        }
   // }
    CalculateMMSImpl();
   
}

function CalculateMMSImpl() {
    var iRiskScore = 0.0;
    var iSTIScore = 0.0;
    var iTBScore = 0.0;

    $.each(arrRiskScore,
        function(index, arrD) {
            if (jQuery.isEmptyObject(arrD) == false) {
                iRiskScore += parseFloat(arrD.Val);
            }
        });
    
    $.each(arrTBScore,
        function (index, arrD) {
            if (jQuery.isEmptyObject(arrD) == false) {
                iTBScore += parseFloat(arrD.Val);
            }
        });

        $.each(arrSTIScore,
        function (index, arrD) {
            if (jQuery.isEmptyObject(arrD) == false) {
                iSTIScore += parseFloat(arrD.Val);
            }
        });

         $("#lblScore").val(iRiskScore);
         $("#lblSTISCreningScore").val(iSTIScore);
     $("#lblTBScreeningScore").val(iTBScore);
}

function GetCIFData() {
    //debugger;
    //activePage = getPageName() + '.aspx';
   // var postData;
    $.ajax({
        type: "POST",
        url: "ClientIntakeForm.aspx?data=GET",
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
                if (response.CIFID != null) {
                    currCIF = response.CIFID;
                    debugger;
                    Init_Controls(response);
                    BindStateLGAData(response);
                    BindUserList(response);
                } else {
                    Init_Controls(response);
                    BindUserList(response);
                }
            }
        },
        error: function (msg) {
        }
    });
}

function ReadCIFormData() {

    rowData = [];
    debugger;
    rowData.push({
        DOV: GetDateData("txtDOV", true),
        TestedDate: GetDateData("txtTestedDate", true),
        CheckedDate: GetDateData("TxtchkdDate", true),
        RequestedDate: GetDateData("txtRequestdDate", true),
        ReferFrom: $("#cmbReferredFrom").val(),
        Setting: $("#cmdSetting").val(),
        PatientState: $("#cmbSOR").val(),
        PatientLga: $("#cmbLGAOR").val(),
        PatientMaritalStat: $("#cmbMaritalStatus").val(),
        PatientCode: ($("#txtClientCode").val() == 0) ? 0 : $("#txtClientCode").val(),
        NoOfChildLessThan5: ($("#txtchildLess5").val() == 0) ? 0 : $("#txtchildLess5").val(),
        NoOfWives: ($("#txtNoOfWives").val() == 0) ? 0 : $("#txtNoOfWives").val(),
        Comments: $("#txtComments").val(),
        FirstTimeVisit: GetSwitchValue("chkFTV"),
        TypeOfSession: GetSwitchValue("chkTypeOfSession"),
        IsClientIsIndex : GetSwitchValue("chkIndexTesting"),
        IndexTestingType: GetSwitchValue("chkIfYesIndex"),
        IndexClientID: ($("#txtClientIndexID").val().length == 0) ? 0 : $("#txtClientIndexID").val(),
        PrevTstHivNeg: GetSwitchValue("chkPrevHIVTested"),
        ClientPreg: GetSwitchValue("chkClientProg"),
        InformHivTransRoute: GetSwitchValue("chkHIVTransRoutes"),
        InformHivTransRiskFactor: GetSwitchValue("chkHIVRiskFactor"),
        InformHivTransMethod: GetSwitchValue("chkHIVTransmPreven"),
        InformHivPossiResults: GetSwitchValue("chkPossibleTestResults"),
        InformConsentHIVTesting: GetSwitchValue("chkHIVConsent"),
        EvrHadSex: GetSwitchValue("chkSexualIntercourse"),
        BlodTransIn3Mnth: GetSwitchValue("chkBloodTransFusion"),
        SexWithCasPartnr: GetSwitchValue("chkSexinLast3Months"),
        SexWithRegPartnr: GetSwitchValue("chkRegSexPartner"),
        LastStiDone: GetSwitchValue("chkSTI"),
        OnceAMonthSex: GetSwitchValue("chkMorethanOneSexPrtnr"),
        CurrentCough: GetSwitchValue("chkCurrentCough"),
        WeightLoss: GetSwitchValue("chkWeightLoss"),
        Fever: GetSwitchValue("chkFever"),
        NightSweats: GetSwitchValue("chkNightSweats"),
        FemaleVaginalDischarge: GetSwitchValue("chkSTIUrination"),
        FemaleAbdominalPain: GetSwitchValue("chkVaginalPain"),
        MaleUrethalDischarge: GetSwitchValue("chkMaleUrinationIssue"),
        MaleScrotalSweling: GetSwitchValue("chkScrotalSwePain"),
        InitialHIVTest: GetSwitchValue("chkInitialHIVTest"),
        GenitalSore: GetSwitchValue("chkGenitalSore"),
        ConfirmatoryHIVTest: GetSwitchValue("chkConfirmatoryHIVTest"),
        SyphilisTest: GetSwitchValue("chkSyphilis"),
        HIVTestResult: GetSwitchValue("chkHivTestResult"),
        NotPrevTested: GetSwitchValue("chkNotTestedHIVThisYear"),
        PrevTestNeg: GetSwitchValue("chkPrevTestedNeg"),
        PrevTestPosInHivCare: GetSwitchValue("chkPrevTestdPosInHIVCare"),
        PrevTestPosNotInHivCare: GetSwitchValue("chkPrevTestdPosNOTInHIVCare"),
        HIVReqResFormSigned: GetSwitchValue("chkHIVResuSigndByTester"),
        HIVReqResFormWithCI: GetSwitchValue("chkFromDTIF"),
        ClientRecHivTestResult: GetSwitchValue("chkClientRcvdTestResults"),
        PostTestConslDone: GetSwitchValue("chkPostTestConsuling"),
        RiskReducPlan: GetSwitchValue("chkRiskDevPlan"),
        PostTestDiscloPlan: GetSwitchValue("chkPostDsiclosrPlan"),
        BringPartForHIVTest: GetSwitchValue("chkPartnerForHIVTesting"),
        BrngChildForHIVTest: GetSwitchValue("chkPostTestConsuling"),
        FPAndDualConcInfo: GetSwitchValue("chkDualContra"),
        PartnerUseFPMethd: GetSwitchValue("chkFPMethodUsed"),
        PrntUseCondomAsFPM: GetSwitchValue("chkCondomasFP"),
        CondomUseDemo: GetSwitchValue("chkCondomUseDemonstrated"),
        CondomsProvided: GetSwitchValue("chkCondomProvided"),
        ClientRefferedToOther: GetSwitchValue("chkClientReferredToOther"),
        SyphilisTestResults: GetSwitchValue("chkSyphilisTestResult"),
        HepatitisB: GetSwitchValue("chkHepatitisBTest"),
        HepatitisC: GetSwitchValue("chkHepatitisC"),
        RequestedBy: $("#cmbRequestedBy").val(),
        TestedBy: $("#cmbTestedBy").val(),
        CheckedBy: $("#cmbChkdBy").val()
    });

    return rowData[0];
}

function SaveCIF() {
    debugger;
    $.hivce.loader('show');
    var session1Data = ReadCIFormData();
    PostCIFormData(session1Data);
    $.hivce.loader('hide');
}

function PostCIFormData(rowData) {

    //var action = "savedata";
    var rowData1 = rowData;
    var errorMsg = ''; 
    activePage = getPageName() + '.aspx';

    if (isNaN(errorMsg)) {
        customAlert(errorMsg);
        return;
    }
    else {
        //$.hivce.loader('show');
        
        $.ajax({
            type: "POST",
            url: "ClientIntakeForm.aspx?data=SAVE",
            //url: activePage + "?data=" + action,
            data: JSON.stringify(rowData),
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            async: true,
            cache: false,
            beforeSend: function () {
            },
            success: function (response) {
                debugger;
                //console.log(response);
                var responseSuccess = response.Success;
                var msg = response.EM;
                if (responseSuccess == "True") {
                    //GetManagementData();
                    customAlert("Client Intake Form saved successfully.");
                   // window.location.assign("../ClinicalForms/frmPatient_History.aspx?&sts=0");
                }
                else {
                    if (responseSuccess == "False") {
                        customAlert(responseSuccess);
                    }
                }
                $.hivce.loader('hide');
            },
            error: function (xhr, textStatus, errorThrown) {
                ShowErrorMessage("There is an issue while saving data. Please contact admin.");
            }
            //error: function (msg) {
            //    ShowErrorMessage(msg);
            //}
        });
    }
}

function closeform() {
    window.location.href = "/ClinicalForms/frmPatient_Home.aspx";
    return true;
}

function BindStateLGAData(response) {
    debugger;
    var data;
    
    data = [];
    $.each(response.States, function (index, value) {
        data.push({ id: value.StateId, text: value.StateName });
    });

    $("#cmbSOR").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#cmbSOR").select2("val", "0");
    $("#cmbSOR").trigger('change.select2');

    if (jQuery.isEmptyObject(response.PatientState) == false) {
           $("#cmbSOR").select2().val(response.PatientState).trigger("change");
        
    }

    data = [];
    $.each(response.LGAs, function (index, value) {
        data.push({ id: value.LGAId, text: value.LGAName });
    });

    $("#cmbLGAOR").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#cmbLGAOR").select2("val", "0");
    $("#cmbLGAOR").trigger('change.select2');
    if (jQuery.isEmptyObject(response.PatientLga) == false) {
        $("#cmbLGAOR").select2().val(response.PatientLga).trigger("change");
    }
}

function BindUserList(response) {
    debugger;
    var data;

    data = [];
    $.each(response.UserList, function (index, value) {
        data.push({ id: value.U_Id, text: value.UN });
    });

    $("#cmbRequestedBy").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#cmbRequestedBy").select2("val", "0");
    $("#cmbRequestedBy").trigger('change.select2');

    if (jQuery.isEmptyObject(response.RequestedBy) == false) {
            $("#cmbRequestedBy").select2().val(response.RequestedBy).trigger("change");
    }

    $("#cmbTestedBy").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#cmbTestedBy").select2("val", "0");
    $("#cmbTestedBy").trigger('change.select2');

    if (jQuery.isEmptyObject(response.TestedBy) == false) {
            $("#cmbTestedBy").select2().val(response.TestedBy).trigger("change");
        }
    

    $("#cmbChkdBy").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    
    if (jQuery.isEmptyObject(response.CheckedBy) == false) {
            $("#cmbChkdBy").select2().val(response.CheckedBy).trigger("change");
    }
}

