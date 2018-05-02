$(document).ready(function () {
    $.hivce.loader('show');
    GetClinicalSummaryData();
    //
    $("#txtNumberofadherence").keydown(function (e) {
        // Allow: backspace, delete, tab, escape, enter and .
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
        // Allow: Ctrl+A, Command+A
            (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
        // Allow: home, end, left, right, down, up
            (e.keyCode >= 35 && e.keyCode <= 40)) {
            // let it happen, don't do anything
            return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });
    //
    $("#txtNoOfHomeVisit").keydown(function (e) {        
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||       
            (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||       
            (e.keyCode >= 35 && e.keyCode <= 40)) {           
            return;
        }        
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });
    //
    $("#txtNumberofDOTS").keydown(function (e) {
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
            (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            (e.keyCode >= 35 && e.keyCode <= 40)) {
            return;
        }
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });
    //
    $("#txtTel").keydown(function (e) {
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
            (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            (e.keyCode >= 35 && e.keyCode <= 40)) {
            return;
        }
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });
    //
});
//For Print
function printdiv(elem) {
    //debugger;
    //$("#txtNoOfHomeVisit").attr('readonly', true);
    //$("#txtNoOfHomeVisit").hide();
    //$.txtNoOfHomeVisit[$(this).val()].hide();
    var mywindow = window.open('', 'PRINT', 'height=700,width=1000');
    mywindow.document.write('<html><head><title>' + document.title + '</title>');
    //mywindow.document.write('<link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/themes/base/jquery-ui.css" rel="stylesheet" type="text/css"/>');
    //mywindow.document.write('<style type="text/css">.style1{width: 100%;}</style>');
    mywindow.document.write('</head><body >');
    mywindow.document.write('<h1>' + document.title + '</h1>');
    mywindow.document.write(document.getElementById(elem).innerHTML);
    mywindow.document.write('</body></html>');
    mywindow.document.close(); // necessary for IE >= 10
    mywindow.focus(); // necessary for IE >= 10*/
    mywindow.print();
    mywindow.close();
    //$("#txtNoOfHomeVisit").attr('readonly', false);
    return true;
}
function TestPrinting(elem) {
    function PrintDiv() {
        var contents = document.getElementById("div_print_Eval").innerHTML;
        var frame1 = document.createElement('iframe');
        frame1.name = "frame1";
        frame1.style.position = "absolute";
        frame1.style.top = "-1000000px";
        document.body.appendChild(frame1);
        var frameDoc = frame1.contentWindow ? frame1.contentWindow : frame1.contentDocument.document ? frame1.contentDocument.document : frame1.contentDocument;
        alert(frameDoc);
        frameDoc.document.open();
        frameDoc.document.write('<html><head><title>DIV Contents</title>');
        frameDoc.document.write('</head><body>');
        frameDoc.document.write(contents);
        frameDoc.document.write('</body></html>');
        frameDoc.document.close();
        setTimeout(function () {
            window.frames["frame1"].focus();
            window.frames["frame1"].print();
            document.body.removeChild(frame1);
        }, 500);
        return false;
    }
}
function WindowPrint() {
    window.print();
}
//
function EmailValidation() {
    var email = document.getElementById('txtEmail');
    if ((email.value).length > 0) {
        var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if (!filter.test(email.value)) {
            alert('Please provide a valid email address');
            email.focus;
            return false;
        }
    }
}
function GetClinicalSummaryData() {
    //debugger;
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
    //debugger;
    window.scrollTo(0, 0);    
    var currentDate = new Date();
    $("#dtFormFillingDate").datepicker({
        autoclose: true
    }).datepicker("setDate", "0");

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

    $('#dtlLabResults').dataTable().fnDestroy();
    ReInitialiseDatatable("ARTRegimen", response);

    $('#dtlRegimenChanges').dataTable().fnDestroy();
    ReInitialiseDatatable("RegimenChanges", response);

    $('#dtlOIS').dataTable().fnDestroy();
    ReInitialiseDatatable("OIS", response);

    $('#dtlCondition').dataTable().fnDestroy();
    ReInitialiseDatatable("Condition", response);

    $('#dtlAnthropoemetric').dataTable().fnDestroy();
    ReInitialiseDatatable("Anthropoemetric", response);

    BindControl(response);
    BindARTRegimen(response);
    //BindRegimenChanges(response);
}
function BindControl(response) {
    //debugger;
    var EvalForOtherBindVal = response.EvaluationForOtherVal;
    if (EvalForOtherBindVal == "1")
        $('#rdInadequate').prop('checked', true);
    else if (EvalForOtherBindVal == "2")
        $('#rdDruginteractions').prop('checked', true);
    else if (EvalForOtherBindVal == "3")
        $('#rdDrugFoodInteractions').prop('checked', true);
    else if (EvalForOtherBindVal == "4")
        $('#rdImpairedabsorption').prop('checked', true);


    if (response.MFLName == "0")
        $("#lblMFLName").text("");
    else
        $("#lblMFLName").text(response.MFLName);
    if (response.PatientEnrollmentID == "0")
        $("#lblPatientEnrollmentID").text("");
    else
        $("#lblPatientEnrollmentID").text(response.PatientEnrollmentID);
    if (response.DOB == "0")
        $("#lblDateofBirth").text("");
    else
        $("#lblDateofBirth").text(response.DOB);
    if (response.Sex == "0")
        $("#lblGender").text("");
    else
        $("#lblGender").text(response.Sex);
    if (response.CurrentWeight == "0")
        $("#lblCurrentWt").text("");
    else
        $("#lblCurrentWt").text(response.CurrentWeight);
    if (response.CurrentHeight == "0")
        $("#lblHeight").text("");
    else
        $("#lblHeight").text(response.CurrentHeight);
    if (response.CliniciansName == "0")
        $("#txtCliniciansName").val("");
    else
        $("#txtCliniciansName").val(response.CliniciansName);
    if (response.Telephone == "0")
        $("#txtTel").val("");
    else
        $("#txtTel").val(response.Telephone);
    if (response.Email == "0")
        $("#txtEmail").val("");
    else
        $("#txtEmail").val(response.Email);
    if (response.PrimaryReason == "0")
        $("#txtPrimaryReason").val("");
    else
        $("#txtPrimaryReason").val(response.PrimaryReason);
    if (response.ClinicalEvaluation == "0")
        $("#txtClinicalEvaluation").val("");
    else
        $("#txtClinicalEvaluation").val(response.ClinicalEvaluation);
    if (response.NumberOfAdherence == "0")
        $("#txtNumberofadherence").val("");
    else
        $("#txtNumberofadherence").val(response.NumberOfAdherence);
    if (response.NumberOfHomeVisit == "0")
        $("#txtNoOfHomeVisit").val("");
    else
        $("#txtNoOfHomeVisit").val(response.NumberOfHomeVisit);
    if (response.SupportStructures == "0")
        $("#txtSupportStructure").val("");
    else
        $("#txtSupportStructure").val(response.SupportStructures);
    if (response.EvidenceOfAdherence == "0")
        $("#txtEvidenceOfAdherence").val("");
    else
        $("#txtEvidenceOfAdherence").val(response.EvidenceOfAdherence);
    if (response.NumberOfDOTS == "0")
        $("#txtNumberofDOTS").val("");
    else
        $("#txtNumberofDOTS").val(response.NumberOfDOTS);
    if (response.PoorAdherence == "0")
        $("#txtLikelyroot").val("");
    else
        $("#txtLikelyroot").val(response.PoorAdherence);
    //debugger;
    if (response.CommentOnTreatment == "0") {
        $("#txtCommentOnTreatment").val("");
    }
    else {
        $("#txtCommentOnTreatment").val(response.CommentOnTreatment);
    }
    if (response.HasDrugResistance == "0")
        $("#txtHasDrug").val("");
    else
        $("#txtHasDrug").val(response.HasDrugResistance);
    if (response.HasFacilityMultidisciplinary == "0")
        $("#txtHasFacility").val("");
    else
        $("#txtHasFacility").val(response.HasFacilityMultidisciplinary);
    if (response.MDTMembers == "0")
        $("#txtMDTMembers").val("");
    else
        $("#txtMDTMembers").val(response.MDTMembers);
    if (response.EvaluationForOtherText == "0")
        $("#txtEvaluationCause").val("");
    else
        $("#txtEvaluationCause").val(response.EvaluationForOtherText);
 
    var reg = response.RegimenChanges.length;   
    if (reg > 0) {      
        $("#lblCurrentRegimen").text(response.RegimenChanges[0].Regimen);
    }
    //Note:
    //jQuery("label[for='lblPatientEnrollmentID']").html(response.PatientEnrollmentID); this is use for <label for="lblPatientEnrollmentID"></label>   
    $.hivce.loader('hide');
}
//


//----------------
function ReInitialiseDatatable(from, response) {
    //debugger;
    if (from == 'ARTRegimen') {
        table = $('#dtlLabResults').DataTable({
            paging: false,
            ordering: false,
            autoWidth: false,
            sortable: false,
            filter: false,
            info: false,
            //dom: 'Blfrtip',
            data: response.ARTRegimensLaboratory,
            columns: [
    { data: "Date" },
    { data: "CD4" },
    { data: "HB" },
    { data: "EGFR" },
    { data: "ViralLoad" }
  ]
        });
    }
    else if (from == 'RegimenChanges') {
        table = $('#dtlRegimenChanges').DataTable({
            paging: false,
            ordering: false,
            autoWidth: false,
            sortable: false,
            filter: false,
            info: false,
            //dom: 'Blfrtip',
            data: response.RegimenChanges,
            columns: [   
    { data: "Date" },   
    { data: "Regimen" },
    { data: "Switch" }
  ]
        });
    }
    else if (from == 'OIS') {
        table = $('#dtlOIS').DataTable({
            paging: false,
            ordering: false,
            autoWidth: false,
            sortable: false,
            filter: false,
            info: false,
            //dom: 'Blfrtip',
            data: response.OIS,
            columns: [
    { data: "Date" },   
    { data: "OIS" }
  ]
        });
    }

    else if (from == 'Condition') {
        table = $('#dtlCondition').DataTable({
            paging: false,
            ordering: false,
            autoWidth: false,
            sortable: false,
            filter: false,
            info: false,
            //dom: 'Blfrtip',
            data: response.ChronicConditions,
            columns: [
    { data: "Date" },
    { data: "Condition" }
  ]
        });
    }

    else if (from == 'Anthropoemetric') {
        table = $('#dtlAnthropoemetric').DataTable({
            paging: false,
            ordering: false,
            autoWidth: false,
            sortable: false,
            filter: false,
            info: false,
            //dom: 'Blfrtip',
            data: response.Anthropoemetric,
            columns: [
    { data: "Date" },
     { data: "Height" },
      { data: "Weight" },
    { data: "BMIZ" }
  ]
        });
    }
}
//


function BindARTRegimen(response) {
    debugger;
    arrARTRegimens = [];
    var responseData = response.ARTRegimensLaboratory;
    for (var key in responseData) {
        arrARTRegimens.push({ Date: responseData[key].Date, CD4: responseData[key].CD4, HB: responseData[key].HB, EGFR: responseData[key].EGFR, ViralLoad: responseData[key].ViralLoad, WeightZScore: responseData[key].WeightZScore, ARVRegimen: responseData[key].ARVRegimen, ReasonForSwitch: responseData[key].ReasonForSwitch, NewOI: responseData[key].NewOI });
    }    
    DrawDataTable("dtlLabResults", arrARTRegimens);
    //    
//    arrRegimenChanges = [];
//    var responseDataRC = response.RegimenChanges;
//    for (var key in responseDataRC) {
//        arrRegimenChanges.push({ Date: responseData[key].Date, Regimen: responseData[key].Regimen, Switch: responseData[key].Switch});
//    }
//    DrawDataTable("dtlRegimenChanges", arrRegimenChanges);


    $.hivce.loader('hide');
}



//SAVE DATA START
function SaveUpdateClinicalSummaryData(tabName) {
    debugger;
    $.hivce.loader('show');
    var data = PrepareClinicalSummaryData();
    PostClinicalSummaryData(data, false, tabName);
}
function PrepareClinicalSummaryData() {
    debugger;
    var EvalForOtherVal = "0";
    if ($('#rdInadequate').is(':checked'))
        EvalForOtherVal = 1;
    else if ($('#rdDruginteractions').is(':checked'))
        EvalForOtherVal = 2;
    else if ($('#rdDrugFoodInteractions').is(':checked'))
        EvalForOtherVal = 3;
    else if ($('#rdImpairedabsorption').is(':checked'))
        EvalForOtherVal = 4;

    rowData = [];
    rowData.push({
        Form_filling_date: $("#dtFormFillingDate").val(),
        CliniciansName: $("#txtCliniciansName").val(),
        Telephone: $("#txtTel").val(),
        Email: $("#txtEmail").val(),
        PrimaryReason: $("#txtPrimaryReason").val(),
        ClinicalEvaluation: $("#txtClinicalEvaluation").val(),
        NumberOfAdherence: $("#txtNumberofadherence").val(),
        NumberOfHomeVisit: $("#txtNoOfHomeVisit").val(),
        SupportStructures: $("#txtSupportStructure").val(),
        EvidenceOfAdherence: $("#txtEvidenceOfAdherence").val(),
        NumberOfDOTS: $("#txtNumberofDOTS").val(),
        PoorAdherence: $("#txtLikelyroot").val(),
        EvaluationForOtherText: $("#txtEvaluationCause").val(),
        EvaluationForOtherVal: EvalForOtherVal,
        CommentOnTreatment: $("#txtCommentOnTreatment").val(),
        HasDrugResistance: $("#txtHasDrug").val(),
        HasFacilityMultidisciplinary: $("#txtHasFacility").val(),
        MDTMembers: $("#txtMDTMembers").val()        
    });
    return rowData[0];
}
function PostClinicalSummaryData(rowData, isPartial, tabName) {
    debugger;
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
                var responseSuccess = response.Success;
                var msg = response.EM;
                if (responseSuccess == "true") {
                    customAlert("Clinical Summary " + dataSuccessMessage.toLowerCase());
                    if (isPartial == false) {
                        if (tabName.length > 0) {
                            GetNextPage(tabName);
                        }
                        else {
                            window.location.assign("../ClinicalForms/frmPatient_Home.aspx?&sts=0");
                        }
                        
                    } else {
                        if (tabName.length > 0) {
                            GetNextPage(tabName);
                        }
                    }
                }
                else {
                    if (responseSuccess == "false") {
                        customAlert(errorMessage);
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
        });
    }
}
///////
function GetNextPage(target) {
    $(".nav-tabs a[href=\"#" + target + "\"]").tab("show");
}