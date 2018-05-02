var arrChronicDisorders = [];
var chkStillActive;
var activePage;
var drugAllergen = [];
var envAllergen = [];
var foodAllergen = [];
var arrLabResults = [];
var arrVisitAnthropometric = [];
var arrPresentPregnancy = [];
var arrNxtAppointment = [];

$(document).ready(function () {
    $.hivce.loader('show');
    GetProfileData();
    $("#hidTabName").val("profile");

    $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
        var target = $(e.target).attr("href") // activated tab
        if (target.toLowerCase().indexOf("#wtab") == -1) {

            $.hivce.loader('show');
            if (target == "#tab_1") {
                GetProfileData();
                $("#hidTabName").val("profile");
            }
            else if (target == "#tab_2") {
                GetScreeningAndTestsData();
                $("#hidTabName").val("screeningandtests");
            }
            else if (target == "#tab_3") {
                GetVisitDetailsData();
                $("#hidTabName").val("visitdetails");
            }

        }
    });

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
});

function GetPreviousPage(target) {
    $.hivce.loader('show');
    $(".nav-tabs a[href=\"#" + target + "\"]").tab("show");
    if (target == "#tab_1") {
        GetProfileData();
        $("#hidTabName").val("profile");
    }
    else if (target == "#tab_2") {
        GetScreeningAndTestsData();
        $("#hidTabName").val("screeningandtests");
    }
    else if (target == "#tab_3") {
        GetVisitDetailsData();
        $("#hidTabName").val("visitdetails");
    }
}

function GetNextPage(target) {
    $(".nav-tabs a[href=\"#" + target + "\"]").tab("show");
    if (target == "#tab_1") {
        GetProfileData();
        $("#hidTabName").val("profile");
    }
    else if (target == "#tab_2") {
        GetScreeningAndTestsData();
        $("#hidTabName").val("screeningandtests");
    }
    else if (target == "#tab_3") {
        GetVisitDetailsData();
        $("#hidTabName").val("visitdetails");
    }
    $.hivce.loader('hide');
}

function OpenLabResultPopup() {

    var NWin = window.open('../Laboratory/frm_Laboratory.aspx?ref=anc', '', 'height=800,width=800');
    if (window.focus) {
        //        NWin.onbeforeunload = function () {
        //            FamilyPopUpClosed();
        //        }
        NWin.focus();
    }
    return false;
}

function DisableDiv(ctrlName) {
    var disDiv;
    var IsControlDisable = false;
    var ctrl, ctrl1;
    if (ctrlName.id == "chkChronicDisorders") {
        disDiv = "divChronicDisorders";
    }
    else if (ctrlName.id == "chkDrugAllergies") {
        disDiv = "divDrugAllergies";
    }
    else if (ctrlName.id == "chkHIVTestingToday") {
        disDiv = "divHIVTesting";
    }
    else if (ctrlName.id == "chkPatientaccPartner") {
        disDiv = "divPartnerDetails";
    }
    var ctrlVal = $("#" + ctrlName.id).bootstrapSwitch('state');
    if (IsControlDisable) {
        if (ctrlVal) {
            $("#" + ctrl).prop('disabled', false);
            if (ctrl1 != null) {
                $("#" + ctrl1).prop('disabled', false);
            }
        }
        else {
            $("#" + ctrl).prop('disabled', true);
            if (ctrl1 != null) {
                $("#" + ctrl1).prop('disabled', true);
            }
        }
    }
    else {
        $("#" + disDiv).removeAttr("style");
        if (ctrlVal) {
            $("#" + disDiv).css("visibility", "visible");
            $("#" + disDiv).css("display", "block");
        }
        else {
            $("#" + disDiv).css("visibility", "hidden");
            $("#" + disDiv).css("display", "none");
        }
    }
}


function ReInitialiseDatatable(from) {
    if (from == 'CD') {
        $('#dtlChronicDisorders').DataTable({
            "aaData": arrChronicDisorders,
            "bSort": false,
            "bPaginate": false,
            "bFilter": false,
            "bInfo": false,
            "autoWidth": false,
            "aoColumns": [
                { bSortable: false,
                    mRender: function (data, type, full) {
                        return full["PC"];
                    }
                },
                { bSortable: false,
                    mRender: function (data, type, full) {
                        return full["displayDate"];
                    }
                },
                { bSortable: false,
                    mRender: function (data, type, full) {
                        return full["IsAVal"];
                    }
                },
                {
                    bSortable: false,
                    mRender: function (data, type, full) {
                        if (full !== undefined) {
                            if (full["VId"] == $("#hidVId").val() || full["VId"] == "0") {
                                return "<a class=\"btn btn-app btn-app-small\" onclick=\"DeleteRow('" + full["PCID"] + "','" + full["VId"] + "','CD');\"><i class=\"glyphicon glyphicon-trash\"></i></a>";
                            }
                            else {
                                return "";
                            }
                        }
                        else {
                            return "<a class=\"btn btn-app btn-app-small\"><i class=\"glyphicon glyphicon-trash\"></i></a>";
                        }
                    }
                }
            ]
        });
    }
    else if (from == 'DA') {
        arrDrugAllergies = [];
        $('#dtlDrugAllergies').DataTable({
            "aaData": arrDrugAllergies,
            "bSort": false,
            "bPaginate": false,
            "bFilter": false,
            "bInfo": false,
            "autoWidth": false,
            "aoColumns": [
                { bSortable: false,
                    mRender: function (data, type, full) {
                        return full["ATD"];
                    }
                },
                { bSortable: false,
                    mRender: function (data, type, full) {
                        return full["Allergen"];
                    }
                },
                { bSortable: false,
                    mRender: function (data, type, full) {
                        return full["SDesc"];
                    }
                },
                { bSortable: false,
                    mRender: function (data, type, full) {
                        return full["displayDAller"];
                    }
                },
                { bSortable: false,
                    mRender: function (data, type, full) {
                        return full["TR"];
                    }
                },
                { bSortable: false,
                    mRender: function (data, type, full) {
                        return full["Comment"];
                    }
                },
                {
                    bSortable: false,
                    mRender: function (data, type, full) {
                        if (full !== undefined) {
                            return "<a class=\"btn btn-app btn-app-small\" onclick=\"DeleteRow('" + full["ATID"] + "',0,'DA');\"><i class=\"glyphicon glyphicon-trash\"></i></a>";
                        }
                        else {
                            return "<a class=\"btn btn-app btn-app-small\"><i class=\"glyphicon glyphicon-trash\"></i></a>";
                        }
                    }
                }
            ]
        });
    }
    else if (from == 'PP') {
        arrPreviousPregnancy = [];
        $('#dtlPreviousPregnancy').DataTable({
            "aaData": arrPreviousPregnancy,
            "bSort": false,
            "bPaginate": false,
            "bFilter": false,
            "bInfo": false,
            "autoWidth": false,
            "aoColumns": [
                { bSortable: false,
                    mRender: function (data, type, full) {
                        return full["YOB"];
                    }
                },
                { bSortable: false,
                    mRender: function (data, type, full) {
                        return full["POD"];
                    }
                },
                { bSortable: false,
                    mRender: function (data, type, full) {
                        return full["MName"];
                    }
                },
                { bSortable: false,
                    mRender: function (data, type, full) {
                        return full["LH"];
                    }
                },
                { bSortable: false,
                    mRender: function (data, type, full) {
                        return full["MOD"];
                    }
                },
                { bSortable: false,
                    mRender: function (data, type, full) {
                        return full["Gender"];
                    }
                },
                { bSortable: false,
                    mRender: function (data, type, full) {
                        return full["Fate"];
                    }
                },
                { bSortable: false,
                    mRender: function (data, type, full) {
                        /*Birth Weight*/
                        return full["BW"];
                    }
                },
                {
                    bSortable: false,
                    mRender: function (data, type, full) {
                        if (full !== undefined) {
                            return "<a class=\"btn btn-app btn-app-small\" onclick=\"DeletePPRow('" + full["YOB"] + "','" + full["POD"] + "','" + full["MID"] + "',0,'PP');" + "\"><i class=\"glyphicon glyphicon-trash\"></i></a>";
                        }
                        else {
                            return "<a class=\"btn btn-app btn-app-small\"><i class=\"glyphicon glyphicon-trash\"></i></a>";
                        }
                    }
                }
            ]
        });
    }
    else if (from == 'LR') {
        $('#dtlLabResults').DataTable({
            "aaData": arrLabResults,
            "bSort": false,
            "bPaginate": false,
            "bFilter": false,
            "bInfo": false,
            "autoWidth": false,
            "aoColumns": [
                { bSortable: false,
                    mRender: function (data, type, full) {
                        return full["STN"];
                    }
                },
                { bSortable: false,
                    mRender: function (data, type, full) {
                        return full["displayTestDate"];
                    }
                },
                { bSortable: false,
                    mRender: function (data, type, full) {
                        return full["Result"];
                    }
                },
                { bSortable: false,
                    mRender: function (data, type, full) {
                        //return "<a class=\"btn btn-app btn-app-small\"><i class=\"glyphicon glyphicon-trash\"></i></a>";
                        var isReadonly = "";
                        if (full["Order"] == "0") {
                            isReadonly = " readonly";
                        } else {
                            isReadonly = " checked readonly";
                        }
                        return "<input type = \"checkbox\" id=\"chk" + full["ParameterId"] + "\" " + isReadonly + " />";
                    }
                }
            ]
        });
    }

    else if (from == 'VA') {
        debugger;
        $('#dtlVisitAnthropometric').DataTable({
            "aaData": arrVisitAnthropometric,
            "bSort": false,
            "bPaginate": false,
            "bFilter": false,
            "bInfo": false,
            "autoWidth": false,
            "aoColumns": [
                { bSortable: false,
                    mRender: function (data, type, full) {
                        return full["displayDate"];
                    }
                },
                { bSortable: false,
                    mRender: function (data, type, full) {
                        return full["PS"];
                    }
                },
                { bSortable: false,
                    mRender: function (data, type, full) {
                        return full["VN"];
                    }
                },
                { bSortable: false,
                    mRender: function (data, type, full) {
                        return full["Height"];
                    }
                },
                { bSortable: false,
                    mRender: function (data, type, full) {
                        return full["Weight"];
                    }
                },
                { bSortable: false,
                    mRender: function (data, type, full) {
                        return full["MUAC"];
                    }
                },
                { bSortable: false,
                    mRender: function (data, type, full) {
                        return full["BPD"] + " / " + full["BPS"];
                    }
                }

            ]
        });
        }

        else if (from == 'PREG') {
        debugger;
        $('#dtlPresentPregnancy').DataTable({
            "aaData": arrPresentPregnancy,
            "bSort": false,
            "bPaginate": false,
            "bFilter": false,
            "bInfo": false,
            "autoWidth": false,
            "aoColumns": [
                { bSortable: false,
                    mRender: function (data, type, full) {
                        return full["M"];
                    }
                },
                { bSortable: false,
                    mRender: function (data, type, full) {
                        return full["Lie"];
                    }
                },
                { bSortable: false,
                    mRender: function (data, type, full) {
                        return full["FH"];
                    }
                },
                { bSortable: false,
                    mRender: function (data, type, full) {
                        return full["FM"];
                    }
                },
                { bSortable: false,
                    mRender: function (data, type, full) {
                        return full["P"];
                    }
                },
                { bSortable: false,
                    mRender: function (data, type, full) {
                        return full["FHR"];
                    }
                }

            ]
        });
    }
}

function GetProfileData() {
    activePage = getPageName() + '.aspx';
    var postData;
    $.ajax({
        type: "POST",
        url: activePage + "?data=getprofiledata",
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
                InitProfileControls(response);
            }
        },
        error: function (msg) {
        }
    });
}


function GetScreeningAndTestsData() {
    activePage = getPageName() + '.aspx';
    var postData;
    $.ajax({
        type: "POST",
        url: activePage + "?data=getscreeningtests",
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
                InitScreeningAndTestsControls(response);
            }
        },
        error: function (msg) {
        }
    });
}

function GetVisitDetailsData() {
    activePage = getPageName() + '.aspx';
    var postData;
    $.ajax({
        type: "POST",
        url: activePage + "?data=getvisitdetails",
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
                InitVisitDetailControls(response);
            }
        },
        error: function (msg) {
        }
    });
}