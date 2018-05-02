var chkAchieved;
var arrImmunizationGiven = [];


function InitAdditionalHxControls(response) {

    window.scrollTo(0, 0);
    //Date picker
    $("#dtAssessed").datepicker({
        autoclose: true
    }).datepicker("setDate", "0");
    $("#dtTSDate").datepicker({
        autoclose: true
    }).datepicker("setDate", "0");

    $("#dtDateImmunized").datepicker({
        autoclose: true
    })//.datepicker("setDate", "0");
    $("#dtLastScreenedOn").datepicker({
        autoclose: true
    })//.datepicker("setDate", "0");
    $("#dtNextScreenedOn").datepicker({
        autoclose: true
    })//.datepicker("setDate", "0");
    $("#dtDHStartdate").datepicker({
        autoclose: true
    })//.datepicker("setDate", "0");
    $("#dtARTLDU").datepicker({
        autoclose: true
    })//.datepicker("setDate", "0");
    $("#dt1stLineDate").datepicker({
        autoclose: true
    })//.datepicker("setDate", "0");
    $("#dtDateOfAllergen").datepicker({
        autoclose: true
    })//.datepicker("setDate", "0");
    $("#dtLastHIVTestDate").datepicker({
        autoclose: true
    })//.datepicker("setDate", "0");
    $("#dtDHStartDate").datepicker({
        autoclose: true
    })//.datepicker("setDate", "0");



    $("#ddlMilestone").select2();
    $("#ddlStatus").select2();
    $("#ddlImmunizationPeriod").select2();
    $("#ddlImmunizationGiven").select2();
    $("#ddlMOrF").select2();
    $("#ddlPublicHair").select2();
    $("#ddlDrugList").select2();
    $("#ddlDHReason").select2();
    $("#ddlARTPurpose").select2();
    $("#ddlRegimen").select2();
    $("#ddlARTStartRegimen").select2();
    $("#ddlARTWHOStage").select2();
    $("#ddlAllergenType").select2();
    $("#ddlSeverity").select2();
    $("#ddlReactionType").select2();
    $("#ddlAllergen").select2();
    $("#ddlRegistered").select2();
    $("#ddlSex").select2();
    $("#ddlHIVStatus").select2();
    $("#ddlHIVCareStatus").select2();
    $("#ddlPartnerStatus").select2();
    $("#ddlPartnerGender").select2();
    $("#ddlSexualOrientation").select2();
    $("#ddlHighRiskBehaviour").select2();
    $("#ddlRelationshipType").select2();
    $("#ddlSexFM").select2();

    $("#chkAchieved").iCheck({
        checkboxClass: 'icheckbox_flat-blue',
        radioClass: 'iradio_flat-blue'
    });
    $('#chkAchieved').on('ifChecked', function (event) {
        chkStillActive = "true";
    });
    $('#chkAchieved').on('ifUnchecked', function (event) {
        chkStillActive = "false";
    });

    $('#dtlMilestones').dataTable().fnDestroy();
    ReInitialiseDatatable("ML");

    $('#dtlImmunizationHistory').dataTable().fnDestroy();
    ReInitialiseDatatable("IM");

    $('#dtlTannerStaging').dataTable().fnDestroy();
    ReInitialiseDatatable("TS");

    //var table = $("#dtlTannerStaging").DataTable();
    //table.rows.add(arrTannerStaging).draw().nodes();

    $('#dtlDrugList').dataTable().fnDestroy();
    ReInitialiseDatatable("LTM");

    $('#dtlPriorART').dataTable().fnDestroy();
    ReInitialiseDatatable("PART");

    $('#dtlDrugAllergies').dataTable().fnDestroy();
    ReInitialiseDatatable("DA");

    $('#dtlFMHIV').dataTable().fnDestroy();
    ReInitialiseDatatable("FM");

    $("#chkLongTermMedication").bootstrapSwitch('state', false);
    $("#chkAlternativeMedicine").bootstrapSwitch('state', false);
    $("#chkPriorART").bootstrapSwitch('state', false);
    $("#chkDrugAllergies").bootstrapSwitch('state', false);
    $("#chkHIVFamilyMember").bootstrapSwitch('state', false);

    $("#chkSexuallyActive6Months").bootstrapSwitch('state', true);

    $('input[type="checkbox"]').on('switchChange.bootstrapSwitch', function (event, state) {
        DisableDiv(this);
    });


    if (window.location.href.indexOf("GreenCard") > -1) {

        var divMS = "divMilestones";
        $("#" + divMS).css("visibility", "hidden");
        $("#" + divMS).css("display", "none");

        var divT = "divTanners";
        $("#" + divT).css("visibility", "hidden");
        $("#" + divT).css("display", "none");

        var divCLTM = "divCLTM";
        $("#" + divCLTM).css("visibility", "hidden");
        $("#" + divCLTM).css("display", "none");

        var divF = "divFamily";
        $("#" + divF).css("visibility", "hidden");
        $("#" + divF).css("display", "none");

        var divS = "divSexualHistory";
        $("#" + divS).css("visibility", "hidden");
        $("#" + divS).css("display", "none");

    }
    $("#btnSearchFamilyMember").prop('disabled', true);
    $("#ddlRegistered").select2().on('change', function (e) {
        var $this = $(this);
        if (jQuery.isEmptyObject($this) == false) {
            console.log($this.val());
            DisableFamilyPopupButton();
        }
    });


    BindAddHxCombo(response);

}

function DisableFamilyPopupButton() {
    if ($("#ddlRegistered").select2('data').length > 0) {
        var theID = $("#ddlRegistered").select2('data')[0].id;
        var theSelection = $("#ddlRegistered").select2('data')[0].text;
        if (theID == 0) {
            $("#btnSearchFamilyMember").prop('disabled', true);
        }
        else {
            $("#btnSearchFamilyMember").prop('disabled', false);
        }

    }
}

function OpenFamilyPopup() {

    //../frmFindAddPatient.aspx?FormName=FamilyInfo
    var NWin = window.open('../ClinicalForms/frmFamilyInformation.aspx?name=Add&ref=CE', '', 'height=800,width=800');
    if (window.focus) {
        //        NWin.onbeforeunload = function () {
        //            FamilyPopUpClosed();
        //        }
        NWin.focus();
    }
    return false;
}

function FamilyPopUpClosed() {
    debugger;
    //alert("FamilyPopUpClosed");
    $.hivce.loader('show');
    var postData;
    $.ajax({
        type: "POST",
        //url: "ClinicalEncounter.aspx?data=getAddtionalHx",
        url: activePage + "?data=getAddtionalHx",
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
                BindFamilyInfo(response);
            }

        },
        error: function (msg) {
        }
    });


}

function BindFamilyInfo(response) {
    arrHIVFamilyMember = [];
    $.each(response.FD, function (index, arrD) {
        if (jQuery.isEmptyObject(arrD) == false) {
            // parse JSON formatted date to javascript date object
            var displayDate, dtOSD, IsReg, vLHIVTD;
            if (arrD.LHIVTD !== null) {
                dtOSD = new Date(parseInt(arrD.LHIVTD.substr(6)));
                // format display date (e.g. 04/10/2012)
                displayDate = $.format.date(dtOSD, "dd-MMM-yyyy");
                vLHIVTD = arrD.LHIVTD;
            }
            else {
                dtOSD = "";
                displayDate = "";
                vLHIVTD = null;
            }
            if (arrD.IsRegistered == "Yes") {
                IsReg = 1;
            }
            else {
                IsReg = 0;
            }

            arrHIVFamilyMember.push({ id: arrD.id, RE: IsReg, RET: arrD.IsRegistered, FN: arrD.RFN, LN: arrD.RLN,
                Sex: arrD.SID,
                SexT: arrD.SDes,
                Age: arrD.AY,
                RT: arrD.RID,
                RTT: arrD.RDes,
                HS: arrD.HIVSID,
                HST: arrD.HIVSDes,
                LHTD: vLHIVTD,
                diaplayLHTD: displayDate,
                HCS: arrD.HIVCSID,
                HCST: arrD.HIVCSDes
                //,VId: arrD.VId
            });
        }
    });
    if (arrHIVFamilyMember.length > 0) {
        $("#chkHIVFamilyMember").bootstrapSwitch('state', true);
    }

    DrawDataTable("dtlFMHIV", arrHIVFamilyMember);
    $.hivce.loader('hide');

}


function ReInitialiseDatatable(from) {
    //debugger;
    if (from == 'ML') {
        arrMilestones = [];
        $('#dtlMilestones').DataTable({
            "aaData": arrMilestones,
            "bSort": false,
            "bPaginate": false,
            "bFilter": false,
            "bInfo": false,
            "autoWidth": true,
            "aoColumns": [
                { bSortable: false,
                    mRender: function (data, type, full) {
                        return full["MAT"];
                    }
                },
                 { bSortable: false,
                     mRender: function (data, type, full) {
                         return full["displayDate"];
                     }
                 },
                 { bSortable: false,
                     mRender: function (data, type, full) {
                         return full["IsAval"];
                     }
                 },
                 { bSortable: false,
                     mRender: function (data, type, full) {
                         return full["STA"];
                     }
                 },
                 { bSortable: false,
                     mRender: function (data, type, full) {
                         return full["CM"];
                     }
                 },
                 {
                     bSortable: false,
                     mRender: function (data, type, full) {
                         //return "<a class=\"btn btn-app btn-app-small\" onclick=\"DeleteADXDTRow('" + full["MA"] + "','ML');\"><i class=\"glyphicon glyphicon-trash\"></i></a>";
                         if (full !== undefined) {
                             if (full["VId"] == $("#hidVId").val() || full["VId"] == "0") {
                                 return "<a class=\"btn btn-app btn-app-small\" onclick=\"DeleteADXDTRow('" + full["MA"] + "','" + full["VId"] + "','ML');\"><i class=\"glyphicon glyphicon-trash\"></i></a>";
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
    else if (from == 'IM') {
        //debugger;
        arrImmunization = [];
        $('#dtlImmunizationHistory').DataTable({
            "aaData": arrImmunization,
            "bSort": false,
            "bPaginate": false,
            "bFilter": false,
            "bInfo": false,
            "autoWidth": true,
            "aoColumns": [
                { bSortable: false,
                    mRender: function (data, type, full) {
                        return full["IPT"];
                    }
                },
                { bSortable: false,
                    mRender: function (data, type, full) {
                        return full["IGT"];
                    }
                },
                { bSortable: false,
                    mRender: function (data, type, full) {
                        return full["displayDate"];
                    }
                },
                {
                    bSortable: false,
                    mRender: function (data, type, full) {
                        //return "<a class=\"btn btn-app btn-app-small\" onclick=\"DeleteADXDTRow('" + full["IP"] + "','IM');\"><i class=\"glyphicon glyphicon-trash\"></i></a>";
                        if (full !== undefined) {
                            if (full["VId"] == $("#hidVId").val() || full["VId"] == "0") {
                                return "<a class=\"btn btn-app btn-app-small\" onclick=\"DeleteADXDTRow('" + full["IP"] + "','" + full["VId"] + "','IM');\"><i class=\"glyphicon glyphicon-trash\"></i></a>";
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
    else if (from == 'TS') {
        arrTannerStaging = [];
        //debugger;
        $('#dtlTannerStaging').DataTable({
            "aaData": arrTannerStaging,
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
                            return full["BOG"];
                        }
                    },
                    { bSortable: false,
                        mRender: function (data, type, full) {
                            return full["PH"];
                        }
                    },
                    {
                        bSortable: false,
                        mRender: function (data, type, full) {
                            //return "<a class=\"btn btn-app btn-app-small\" onclick=\"DeleteADXDTRow('" + full["SD"] + "','TS');\"><i class=\"glyphicon glyphicon-trash\"></i></a>";
                            if (full !== undefined) {
                                if (full["VId"] == $("#hidVId").val() || full["VId"] == "0") {
                                    return "<a class=\"btn btn-app btn-app-small\" onclick=\"DeleteADXDTRow('" + full["SD"] + "','" + full["VId"] + "','TS');\"><i class=\"glyphicon glyphicon-trash\"></i></a>";
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
    else if (from == 'LTM') {
        arrLTM = [];
        $('#dtlDrugList').DataTable({
            "aaData": arrLTM,
            "bSort": false,
            "bPaginate": false,
            "bFilter": false,
            "bInfo": false,
            "autoWidth": false,
            "aoColumns": [
         { bSortable: false,
             mRender: function (data, type, full) {
                 return full["ValT"];
             }
         },
         { bSortable: false,
             mRender: function (data, type, full) {
                 return full["displayDate"];
             }
         },
         { bSortable: false,
             mRender: function (data, type, full) {
                 return full["NTT"];
             }
         },
         {
             bSortable: false,
             mRender: function (data, type, full) {
                 //return "<a class=\"btn btn-app btn-app-small\" onclick=\"DeleteADXDTRow('" + full["ValId"] + "','LTM');\"><i class=\"glyphicon glyphicon-trash\"></i></a>";
                 if (full !== undefined) {
                     if (full["VId"] == $("#hidVId").val() || full["VId"] == "0") {
                         return "<a class=\"btn btn-app btn-app-small\" onclick=\"DeleteADXDTRow('" + full["ValId"] + "','" + full["VId"] + "','LTM');\"><i class=\"glyphicon glyphicon-trash\"></i></a>";
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
    else if (from == 'PART') {
        arrPriorART = [];
        $('#dtlPriorART').DataTable({
            "aaData": arrPriorART,
            "bSort": false,
            "bPaginate": false,
            "bFilter": false,
            "bInfo": false,
            "autoWidth": false,
            "aoColumns": [
         { bSortable: false,
             mRender: function (data, type, full) {
                 return full["PT"];
             }
         },
         { bSortable: false,
             mRender: function (data, type, full) {
                 return full["RT"];
             }
         },
         { bSortable: false,
             mRender: function (data, type, full) {
                 return full["displayDate"];
             }
         },
         {
             bSortable: false,
             mRender: function (data, type, full) {
                 //return "<a class=\"btn btn-app btn-app-small\" onclick=\"DeleteADXDTRow('" + full["PID"] + "','PART');\"><i class=\"glyphicon glyphicon-trash\"></i></a>";
                 if (full !== undefined) {
                     if (full["VId"] == $("#hidVId").val() || full["VId"] == "0") {
                         return "<a class=\"btn btn-app btn-app-small\" onclick=\"DeleteADXDTRow('" + full["PID"] + "','" + full["VId"] + "','PART');\"><i class=\"glyphicon glyphicon-trash\"></i></a>";
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
                     return "<a class=\"btn btn-app btn-app-small\" onclick=\"DeleteADXDTRow('" + full["ATID"] + "',0,'DA');\"><i class=\"glyphicon glyphicon-trash\"></i></a>";
                 }
                 else {
                     return "<a class=\"btn btn-app btn-app-small\"><i class=\"glyphicon glyphicon-trash\"></i></a>";
                 }
             }
         }
    ]
        });
    }
    else if (from == 'FM') {
        arrHIVFamilyMember = [];
        $('#dtlFMHIV').DataTable({
            "aaData": arrHIVFamilyMember,
            "bSort": false,
            "bPaginate": true,
            "aLengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "All"]],
            "bFilter": false,
            "bInfo": false,
            "autoWidth": false,
            "searching": true,
            "aoColumns": [
         { bSortable: false,
             mRender: function (data, type, full) {
                 return full["RET"];
             }
         },
         { bSortable: false,
             mRender: function (data, type, full) {
                 return full["FN"];
             }
         },
         { bSortable: false,
             mRender: function (data, type, full) {
                 return full["LN"];
             }
         },
         { bSortable: false,
             mRender: function (data, type, full) {
                 return full["SexT"];
             }
         },
         { bSortable: false,
             mRender: function (data, type, full) {
                 return full["Age"];
             }
         },
         { bSortable: false,
             mRender: function (data, type, full) {
                 return full["RTT"];
             }
         },
         { bSortable: false,
             mRender: function (data, type, full) {
                 return full["HST"];
             }
         },
         { bSortable: false,
             mRender: function (data, type, full) {
                 return full["diaplayLHTD"];
             }
         },
         { bSortable: false,
             mRender: function (data, type, full) {
                 return full["HCST"];
             }
         },
         {
             bSortable: false,
             mRender: function (data, type, full) {
                 if (full !== undefined) {
                     return "<a class=\"btn btn-app btn-app-small\" onclick=\"DeleteADXDTRow('" + full["id"] + "~" + full["FN"] + "~" + full["LN"] + "~" + full["Sex"] + "~" + full["Age"] + "',0,'FM');\"><i class=\"glyphicon glyphicon-trash\"></i></a>";
                 }
                 else {
                     return "<a class=\"btn btn-app btn-app-small\"><i class=\"glyphicon glyphicon-trash\"></i></a>";
                 }
             }
         }
    ]
        });


    }
}


function BindAddHxCombo(response) {
    var data = [];
    drugAllergen = [];
    envAllergen = [];
    foodAllergen = [];
    $.each(response.MS, function (index, value) {
        data.push({ id: value.Id, text: value.NM });
    });

    $("#ddlMilestone").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlMilestone").select2("val", "0");
    $("#ddlMilestone").trigger('change.select2');

    data = [];
    $.each(response.IS, function (index, value) {
        data.push({ id: value.Id, text: value.NM });
    });

    $("#ddlStatus").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlStatus").select2("val", "0");
    $("#ddlStatus").trigger('change.select2');

    data = [];
    $.each(response.IP, function (index, value) {
        data.push({ id: value.Id, text: value.NM });
    });

    $("#ddlImmunizationPeriod").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlImmunizationPeriod").select2("val", "0");
    $("#ddlImmunizationPeriod").trigger('change.select2');

    data = [];
    $.each(response.IG, function (index, value) {
        data.push({ id: value.Id, text: value.NM });
    });
    arrImmunizationGiven = response.IG;

    $("#ddlImmunizationGiven").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlImmunizationGiven").select2("val", "0");
    $("#ddlImmunizationGiven").trigger('change.select2');

    data = [];
    $.each(response.LTML, function (index, value) {
        data.push({ id: value.Id, text: value.NM });
    });

    $("#ddlDrugList").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlDrugList").select2("val", "0");
    $("#ddlDrugList").trigger('change.select2');

    data = [];
    $.each(response.PARTP, function (index, value) {
        data.push({ id: value.Id, text: value.NM });
    });

    $("#ddlARTPurpose").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlARTPurpose").select2("val", "0");
    $("#ddlARTPurpose").trigger('change.select2');

    data = [];
    $.each(response.WHOS, function (index, value) {
        data.push({ id: value.Id, text: value.NM });
    });

    $("#ddlARTWHOStage").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlARTWHOStage").select2("val", "0");
    $("#ddlARTWHOStage").trigger('change.select2');

    data = [];
    $.each(response.Severity, function (index, value) {
        data.push({ id: value.Id, text: value.NM });
    });

    $("#ddlSeverity").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlSeverity").select2("val", "0");
    $("#ddlSeverity").trigger('change.select2');

    data = [];
    $.each(response.AT, function (index, value) {
        data.push({ id: value.Id, text: value.NM });
    });

    $("#ddlAllergenType").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlAllergenType").select2("val", "0");
    $("#ddlAllergenType").trigger('change.select2');

    $("#ddlAllergenType").select2().on('change', function () {
        BindAllergiesAllergen($(this).val());
    }).trigger('change');

    $.each(response.Allergen, function (index, value) {
        drugAllergen.push({ id: value.Id, text: value.NM });
    });
    $.each(response.ENVA, function (index, value) {
        envAllergen.push({ id: value.Id, text: value.NM });
    });
    $.each(response.FOODA, function (index, value) {
        foodAllergen.push({ id: value.Id, text: value.NM });
    });

    var selectList = $("#ddlAllergen");
    selectList.empty();
    $("#divtxtAller").css("visibility", "hidden");
    $("#divtxtAller").css("display", "none");
    $("#divddlAller").css("visibility", "visible");
    $("#divddlAller").css("display", "block");

    $("#ddlAllergen").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: drugAllergen
    });
    $("#ddlAllergen").select2("val", "0");
    $("#ddlAllergen").trigger('change.select2');

    data = [];
    $.each(response.HIVS, function (index, value) {
        data.push({ id: value.Id, text: value.NM });
    });

    $("#ddlHIVStatus").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlHIVStatus").select2("val", "0");
    $("#ddlHIVStatus").trigger('change.select2');

    data = [];
    $.each(response.Gender, function (index, value) {
        data.push({ id: value.Id, text: value.NM });
    });

    $("#ddlSex").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlSex").select2("val", "0");
    $("#ddlSex").trigger('change.select2');

    $("#ddlSexFM").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlSexFM").select2("val", "0");
    $("#ddlSexFM").trigger('change.select2');

    data = [];
    $.each(response.SO, function (index, value) {
        data.push({ id: value.Id, text: value.NM });
    });

    $("#ddlSexualOrientation").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlSexualOrientation").select2("val", "0");
    $("#ddlSexualOrientation").trigger('change.select2');



    data = [];
    $.each(response.PHIVS, function (index, value) {
        data.push({ id: value.Id, text: value.NM });
    });

    $("#ddlPartnerStatus").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlPartnerStatus").select2("val", "0");
    $("#ddlPartnerStatus").trigger('change.select2');


    data = [];
    $.each(response.HS, function (index, value) {
        data.push({ id: value.Id, text: value.NM });
    });

    $("#ddlHighRiskBehaviour").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlHighRiskBehaviour").select2("val", "0");
    $("#ddlHighRiskBehaviour").trigger('change.select2');


    data = [];
    //    $.each(response.Allergen, function (index, value) {
    //        data.push({ id: value.Id, text: value.NM });
    //    });



    data = $.grep(response.RC, function (e) { return e.CN == "RegimenCodes"; });
    var vdata = [];
    $.each(data, function (index, value) {
        vdata.push({ id: value.DId, text: value.DN });
    });

    $("#ddlRegimen").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        data: vdata
    });
    $("#ddlRegimen").select2("val", "0");
    $("#ddlRegimen").trigger('change.select2');


    $("#ddlARTStartRegimen").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlARTStartRegimen").select2("val", "0");
    $("#ddlARTStartRegimen").trigger('change.select2');

    data = [];
    $.each(response.HIVCS, function (index, value) {
        data.push({ id: value.Id, text: value.NM });
    });

    $("#ddlHIVCareStatus").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlHIVCareStatus").select2("val", "0");
    $("#ddlHIVCareStatus").trigger('change.select2');


    data = [];
    $.each(response.RT, function (index, value) {
        data.push({ id: value.Id, text: value.NM });
    });

    $("#ddlRelationshipType").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlRelationshipType").select2("val", "0");
    $("#ddlRelationshipType").trigger('change.select2');


    data = [];
    $.each(response.Gender, function (index, value) {
        data.push({ id: value.Id, text: value.NM });
    });

    $("#ddlPartnerGender").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlPartnerGender").select2("val", "0");
    $("#ddlPartnerGender").trigger('change.select2');


    var mofdata = [];
    $.each([1, 2, 3, 4], function (index, value) {
        mofdata.push({ id: value, text: value });
    });

    $("#ddlMOrF").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        minimumResultsForSearch: -1,
        allowClear: true,
        width: 'resolve',
        data: mofdata
    }).on('select2:select', function () {
        debugger;
        enableDisableDropdownItems($(this), true);
        $("#ddlMOrF").select2({
            allowClear: true
        });

    }).on('select2:unselect', function () {
        enableDisableDropdownItems($(this), false);
        $("#ddlMOrF").select2({
            allowClear: true
        });

    });

    $("#ddlMOrF").select2("val", "0");
    $("#ddlMOrF").trigger('change.select2');

    $("#ddlPublicHair").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        minimumResultsForSearch: -1,
        allowClear: true,
        width: 'resolve',
        data: mofdata
    }).on('select2:select', function () {
        debugger;
        enableDisableDropdownItems($(this), true);
        $("#ddlPublicHair").select2({
            allowClear: true
        });

    }).on('select2:unselect', function () {
        enableDisableDropdownItems($(this), false);
        $("#ddlPublicHair").select2({
            allowClear: true
        });

    });
    $("#ddlPublicHair").select2("val", "0");
    $("#ddlPublicHair").trigger('change.select2');

    data = [];
    $.each(response.LTMR, function (index, value) {
        data.push({ id: value.Id, text: value.NM });
    });

    $("#ddlDHReason").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlDHReason").select2("val", "0");
    $("#ddlDHReason").trigger('change.select2');
    data = [];

    $("#ddlDHReason").select2().on('change', function () {
        EnableDisableOtherLongTermMedication($(this).val());
    }).trigger('change');

    $("#ddlDrugList").select2().on('change', function () {
        EnableDisableDrugOtherLTM($(this).val());
    }).trigger('change');

    //    $("#ddlMOrF").select2().on('change', function () {
    //        enableDisableDropdownItems($(this));
    //    }).trigger('change');

    //    $("#ddlPublicHair").select2().on('change', function () {
    //        enableDisableDropdownItems($(this));
    //    }).trigger('change');

    BindADXControls(response);
}

function BindADXControls(response) {
    var data;
    arrMilestones = [];
    data = $.grep(response.MnI, function (e) { return e.Section == "Milestone"; });
    $.each(data, function (index, arrD) {
        if (jQuery.isEmptyObject(arrD) == false) {
            // parse JSON formatted date to javascript date object
            var dtOSD = new Date(parseInt(arrD.DT.substr(6)));

            // format display date (e.g. 04/10/2012)
            var displayDate = $.format.date(dtOSD, "dd-MMM-yyyy");
            var IsAval = "false";
            if (arrD.IsAchieved == 1) {
                IsAval = "true";
            }
            //debugger;
            arrMilestones.push({ MA: arrD.TTID, MAT: arrD.TT, DA: arrD.DT, displayDate: displayDate, IsA: arrD.IsAchieved, IsAval: IsAval, ST: arrD.RID, STA: arrD.Result, CM: arrD.CM, VId: arrD.VID });
        }
    });
    DrawDataTable("dtlMilestones", arrMilestones);

    arrImmunization = [];
    data = $.grep(response.MnI, function (e) { return e.Section == "Immunization"; });
    $.each(data, function (index, arrD) {
        if (jQuery.isEmptyObject(arrD) == false) {
            var vdata = $.grep(response.IP, function (e) { return e.Id == arrD.TTID; });
            if (jQuery.isEmptyObject(vdata) == false) {
                // parse JSON formatted date to javascript date object
                var dtOSD = new Date(parseInt(arrD.DT.substr(6)));

                // format display date (e.g. 04/10/2012)
                var displayDate = $.format.date(dtOSD, "dd-MMM-yyyy");
                //debugger;
                arrImmunization.push({ IP: arrD.TTID, IPT: arrD.TT, IG: arrD.RID, IGT: arrD.Result, DT: arrD.DT, displayDate: displayDate, VId: arrD.VID });

            }
        }
    });
    DrawDataTable("dtlImmunizationHistory", arrImmunization);

    arrTannerStaging = [];
    $.each(response.TS, function (index, arrD) {
        if (jQuery.isEmptyObject(arrD) == false) {
            // parse JSON formatted date to javascript date object
            var dtOSD = new Date(parseInt(arrD.SD.substr(6)));
            // format display date (e.g. 04/10/2012)
            var displayDate = $.format.date(dtOSD, "dd-MMM-yyyy");
            arrTannerStaging.push({ SD: arrD.SD, displayDate: displayDate, Gender: arrD.Gender, BOG: arrD.BOG, PH: arrD.PH, VId: arrD.VID });
        }
    });
    if (jQuery.isEmptyObject(response.TS) == false) {
        CheckDatenAssign(response.TS[0].LSO, "dtLastScreenedOn", false);
        CheckDatenAssign(response.TS[0].NSO, "dtNextScreenedOn", false);
    }
    DrawDataTable("dtlTannerStaging", arrTannerStaging);

    arrLTM = [];
    data = $.grep(response.LTMnHR, function (e) { return e.FN == "LongTermMedications"; });
    $.each(data, function (index, arrD) {
        if (jQuery.isEmptyObject(arrD) == false) {
            var vdata = $.grep(response.LTML, function (e) { return e.Id == arrD.ValId; });
            if (jQuery.isEmptyObject(vdata) == false) {
                //debugger;
                var vdata1 = $.grep(response.LTMR, function (e) { return e.Id == arrD.NF; });
                // parse JSON formatted date to javascript date object
                var dtOSD = new Date(parseInt(arrD.OSD.substr(6)));

                // format display date (e.g. 04/10/2012)
                var displayDate = $.format.date(dtOSD, "dd-MMM-yyyy");
                var reasonText = '';
                var isOther = false;
                var isDrugOtherText = false;
                var drugText = '';

                if (jQuery.isEmptyObject(arrD.NT) == false) {
                    if (arrD.NT.length > 0) {
                        reasonText = arrD.NT;
                        isOther = true;
                    }
                }
                else {
                    reasonText = vdata1[0].NM;
                    isOther = false;
                }
                if (jQuery.isEmptyObject(arrD.VNT) == false) {
                    if (arrD.VNT.length > 0) {
                        drugText = arrD.VNT;
                        isDrugOtherText = true;
                    }
                }
                else {
                    drugText = vdata[0].NM;
                    isDrugOtherText = false;
                }

                arrLTM.push({ FN: "LongTermMedications", ValId: vdata[0].Id, ValT: drugText, OSD: arrD.OSD, NT: vdata1[0].Id, NTT: reasonText, displayDate: displayDate, isOther: isOther, VId: arrD.VId, isDrugOtherText: isDrugOtherText });

            }
        }
    });

    if (arrLTM.length > 0) {
        $("#chkLongTermMedication").bootstrapSwitch('state', true);
    }
    DrawDataTable("dtlDrugList", arrLTM);


    arrPriorART = [];
    $.each(response.PART, function (index, arrD) {
        if (jQuery.isEmptyObject(arrD) == false) {
            var vdata = $.grep(response.PARTP, function (e) { return e.Id == arrD.PID; });
            if (jQuery.isEmptyObject(vdata) == false) {
                // parse JSON formatted date to javascript date object
                var dtOSD = new Date(parseInt(arrD.LDU.substr(6)));

                // format display date (e.g. 04/10/2012)
                var displayDate = $.format.date(dtOSD, "dd-MMM-yyyy");
                arrPriorART.push({ PID: arrD.PID, PT: vdata[0].NM, RId: arrD.RId, RT: arrD.Regimen, Regimen: arrD.Regimen, LDU: arrD.LDU, displayDate: displayDate, VId: arrD.VID });
            }
        }
    });
    if (arrPriorART.length > 0) {
        $("#chkPriorART").bootstrapSwitch('state', true);
    }

    DrawDataTable("dtlPriorART", arrPriorART);

    arrDrugAllergies = [];
    $.each(response.AllergiesD, function (index, arrD) {
        if (jQuery.isEmptyObject(arrD) == false) {
            // parse JSON formatted date to javascript date object
            var dtOSD = new Date(parseInt(arrD.DAller.substr(6)));

            // format display date (e.g. 04/10/2012)
            var displayDate = $.format.date(dtOSD, "dd-MMM-yyyy");
            var allerTID, allergenVal, allerDesc;

            if (arrD.ATD.trim() == "Other") {
                allergenVal = arrD.OAller;
            }
            else if (arrD.ATD.trim() == "Environmental") {
                var vdataENVA = $.grep(response.ENVA, function (e) { return e.Id == arrD.PID; });
                allerTID = arrD.Allergen;
                allergenVal = arrD.AllerDesc;
                allerDesc = arrD.AllerDesc;
            }
            else if (arrD.ATD.trim() == "Food") {
                var vdataFOODA = $.grep(response.FOODA, function (e) { return e.Id == arrD.PID; });
                allerTID = arrD.Allergen;
                allergenVal = arrD.AllerDesc;
                allerDesc = arrD.AllerDesc;
            }
            else {
                allergenVal = arrD.Allergen;
                allerDesc = arrD.AllerDesc;
                allerTID = "";
            }

            arrDrugAllergies.push({ ATID: arrD.ATID, ATD: arrD.ATD, Allergen: allergenVal, AllerDesc: allerDesc,
                AllerTID: allerTID,
                OAller: arrD.OAller,
                STID: arrD.STID,
                SDesc: arrD.SDesc,
                DAller: arrD.DAller,
                displayDAller: displayDate,
                TR: arrD.TR,
                Comment: arrD.Comment
                //,VId: arrD.VId
            });

        }
    });
    if (arrDrugAllergies.length > 0) {
        $("#chkDrugAllergies").bootstrapSwitch('state', true);
    }
    DrawDataTable("dtlDrugAllergies", arrDrugAllergies);

    arrHIVFamilyMember = [];
    $.each(response.FD, function (index, arrD) {
        if (jQuery.isEmptyObject(arrD) == false) {
            // parse JSON formatted date to javascript date object
            var displayDate, dtOSD, IsReg, vLHIVTD;
            if (arrD.LHIVTD !== null) {
                dtOSD = new Date(parseInt(arrD.LHIVTD.substr(6)));
                // format display date (e.g. 04/10/2012)
                displayDate = $.format.date(dtOSD, "dd-MMM-yyyy");
                vLHIVTD = arrD.LHIVTD;
            }
            else {
                dtOSD = "";
                displayDate = "";
                vLHIVTD = null;
            }
            if (arrD.IsRegistered == "Yes") {
                IsReg = 1;
            }
            else {
                IsReg = 0;
            }

            arrHIVFamilyMember.push({ id: arrD.id, RE: IsReg, RET: arrD.IsRegistered, FN: arrD.RFN, LN: arrD.RLN,
                Sex: arrD.SID,
                SexT: arrD.SDes,
                Age: arrD.AY,
                RT: arrD.RID,
                RTT: arrD.RDes,
                HS: arrD.HIVSID,
                HST: arrD.HIVSDes,
                LHTD: vLHIVTD,
                diaplayLHTD: displayDate,
                HCS: arrD.HIVCSID,
                HCST: arrD.HIVCSDes
                //,VId: arrD.VId
            });
        }
    });
    if (arrHIVFamilyMember.length > 0) {
        $("#chkHIVFamilyMember").bootstrapSwitch('state', true);
    }

    DrawDataTable("dtlFMHIV", arrHIVFamilyMember);

    $("#txtNeonatalNotes").val(response.Exami);

    if (response.RFHMed.trim().length > 0) {
        $("#chkAlternativeMedicine").bootstrapSwitch('state', true);
        $("#txtAlternativeMedicine").val(response.RFHMed);
    }

    //    if (jQuery.isEmptyObject(response.ARTS) == false) {
    //        var vdata = $.grep(response.Allergen, function (e) { return e.Id == response.ARTS[0].FLR; });
    //        if (jQuery.isEmptyObject(vdata) == false) {
    //            $("#ddlARTStartRegimen").select2().val(vdata[0].Id).trigger("change");
    //        }

    //        vdata = $.grep(response.WHOS, function (e) { return e.Id == response.ARTS[0].WS; });
    //        if (jQuery.isEmptyObject(vdata) == false) {
    //            $("#ddlARTWHOStage").select2().val(vdata[0].Id).trigger("change");
    //        }

    //        CheckDatenAssign(response.ARTS[0].FLRD, "dt1stLineDate", false);
    //        $("#txtASAFWeight").val(response.ARTS[0].WE);
    //        $("#txtASAFHeight").val(response.ARTS[0].HED);
    //        CalcualteBMI('hidPAYM', 'txtASAFWeight', 'txtASAFHeight', 'txtASAFBMI', 'lblASAFBMIClassification');
    //    }
    if (jQuery.isEmptyObject(response.SH) == false) {
        if (response.SH[0].SA6M == 1) {
            $("#chkSexuallyActive6Months").bootstrapSwitch('state', true);
        }
        $("#txtPartners").val(response.SH[0].P6M);

        $("#ddlPartnerStatus").select2().val(response.SH[0].HIVSSP).trigger("change");
        $("#ddlPartnerGender").select2().val(response.SH[0].PG).trigger("change");
        $("#ddlSexualOrientation").select2().val(response.SH[0].SO).trigger("change");
    }

    var vHRB = $.grep(response.LTMnHR, function (e) { return e.FN == "HighRisk"; });
    var selectedValues = new Array();
    $.each(vHRB, function (index, arrD) {
        if (jQuery.isEmptyObject(arrD) == false) {
            if (arrD.ValId !== 0) {
                selectedValues.push([arrD.ValId]);
            }
        }
    });

    $("#ddlHighRiskBehaviour").select2().val(selectedValues).trigger("change");


    var age = $("#hidDOB").val();
    var gender = $("#hidGender").val();

    if (age > 5) {
        $("#divG1").removeClass("box box-default box-solid").addClass("box box-default box-solid collapsed-box");
        $("#divG1I").removeClass("fa fa-minus").addClass("fa fa-plus");
    }

    if (age > 5) {
        $("#btnAddMilestones").prop('disabled', true);
        $("#btnAddImmunization").prop('disabled', true);
    }
    else {
        $("#btnAddMilestones").prop('disabled', false);
        $("#btnAddImmunization").prop('disabled', false);
    }

    if (age < 9 || age > 20) {
        $("#divTanners").removeClass("box box-default box-solid").addClass("box box-default box-solid collapsed-box");
        $("#divTannersI").removeClass("fa fa-minus").addClass("fa fa-plus");
        $("#btnAddTannerStaging").prop('disabled', true);
    }
    if (age < 12) {
        $("#divSexualHistory").css("visibility", "hidden");
        $("#divSexualHistory").css("display", "none");
    }

    var visitType = $("#ddlVisitType").select2("val");
    var visitTypeData = $("#ddlVisitType").select2('data')[0];


    if (visitType != null) {
        if (visitTypeData.text.toUpperCase().indexOf("INITIAL") == -1) {
            $("#divPriorARTSection").removeClass().addClass("box box-default box-solid collapsed-box");
            $("#divPriorARTI").removeClass("fa fa-minus").addClass("fa fa-plus");
        }
        else {
            $("#divPriorARTSection").removeClass().addClass("box box-default box-solid");
            $("#divPriorARTI").removeClass().addClass("fa fa-minus");
        }
    }


    $.hivce.loader('hide');
}


function DeleteADXDTRow(id, vid, from) {
    var arrUI = [];
    if (from == 'ML') {

        $.each(arrMilestones, function (i, el) {
            if (this.VId == $("#hidVId").val() || this.VId == 0) {
                if (this.MA == id) {
                    arrMilestones.splice(i, 1);
                }
            }
        });
        var table = $("#dtlMilestones").DataTable();
        table.rows().remove().draw();

        DrawDataTable("dtlMilestones", arrMilestones);
    }
    else if (from == 'IM') {

        $.each(arrImmunization, function (i, el) {
            if (this.VId == $("#hidVId").val() || this.VId == 0) {
                if (this.IP == id) {
                    arrImmunization.splice(i, 1);
                }
            }
        });
        var table = $("#dtlImmunizationHistory").DataTable();
        table.rows().remove().draw();

        DrawDataTable("dtlImmunizationHistory", arrImmunization);
    }
    else if (from == 'TS') {

        $.each(arrTannerStaging, function (i, el) {
            if (this.VId == $("#hidVId").val() || this.VId == 0) {
                if (this.SD == id) {
                    arrTannerStaging.splice(i, 1);
                }
            }
        });
        var table = $("#dtlTannerStaging").DataTable();
        table.rows().remove().draw();

        DrawDataTable("dtlTannerStaging", arrTannerStaging);
    }
    else if (from == 'LTM') {

        $.each(arrLTM, function (i, el) {
            if (this.VId == $("#hidVId").val() || this.VId == 0) {
                if (this.ValId == id) {
                    arrLTM.splice(i, 1);
                }
            }
        });
        var table = $("#dtlDrugList").DataTable();
        table.rows().remove().draw();

        DrawDataTable("dtlDrugList", arrLTM);
    }
    else if (from == 'PART') {

        $.each(arrPriorART, function (i, el) {
            if (this.VId == $("#hidVId").val() || this.VId == 0) {
                if (this.PID == id) {
                    arrPriorART.splice(i, 1);
                }
            }
        });
        var table = $("#dtlPriorART").DataTable();
        table.rows().remove().draw();

        DrawDataTable("dtlPriorART", arrPriorART);
    }
    else if (from == 'DA') {

        $.each(arrDrugAllergies, function (i, el) {
            if (this.ATID == id) {
                arrDrugAllergies.splice(i, 1);
            }
        });
        var table = $("#dtlDrugAllergies").DataTable();
        table.rows().remove().draw();

        DrawDataTable("dtlDrugAllergies", arrDrugAllergies);
    }
    else if (from == 'FM') {
        var arr = id.split('~');
        if (arr[0] == 0 || arr[0] == null || arr[0] == undefined) {
            $.each(arrHIVFamilyMember, function (i, el) {
                if (this.FN == arr[1]
            && this.LN == arr[2]
            && this.Sex == arr[3]
            && this.Age == arr[4]) {
                    arrHIVFamilyMember.splice(i, 1);
                }
            });
        }
        else {
            $.each(arrHIVFamilyMember, function (i, el) {
                if (this.id == arr[0]) {
                    arrHIVFamilyMember.splice(i, 1);
                }
            });

        }

        var table = $("#dtlFMHIV").DataTable();
        table.rows().remove().draw();

        DrawDataTable("dtlFMHIV", arrHIVFamilyMember);
    }

}

function EnableDisableDrugOtherLTM(val) {
    var ddlDrugList = $("#ddlDrugList").select2("val");
    if (ddlDrugList != null) {
        var data = $("#ddlDrugList").select2('data')[0];
        if (data.text.toLowerCase().indexOf("other") >= 0) {
            $("#txtDrugOther").prop("disabled", false);
        }
        else {
            $("#txtDrugOther").prop("disabled", true);
        }
    }
    else {
        $("#txtDrugOther").prop("disabled", true);
    }
}

function EnableDisableOtherLongTermMedication(val) {
    var ddlDrugList = $("#ddlDHReason").select2("val");
    if (ddlDrugList != null) {
        var data = $("#ddlDHReason").select2('data')[0];
        if (data.text.toLowerCase().indexOf("other") >= 0) {
            $("#txtOtherReason").prop("disabled", false);
        }
        else {
            $("#txtOtherReason").prop("disabled", true);
        }
    }
    else {
        $("#txtOtherReason").prop("disabled", true);
    }
}

function enableDisableDropdownItems(ctrl, IsSelect) {
    debugger;
    var selectedItem = false;
    var selevtedVal;

    $(ctrl).find("option:selected").each(function (i, selected) {
        if (jQuery.isEmptyObject(selected) == false) {
            selectedItem = true;
            selevtedVal = $(selected).val();
        }
    });

    if (selectedItem) {
        // loop trough all the selects for each selected option
        if (IsSelect) {
            $(ctrl).find("option").each(function (i, opt) {
                if (jQuery.isEmptyObject(opt) == false) {
                    //                //console.log($(opt).text());
                    if (selevtedVal > $(opt).val()) {
                        debugger;
                        opt.disabled = true;
                    }
                }
            });
        }
        else {
            $(ctrl).find("option").each(function (i, opt) {
                if (jQuery.isEmptyObject(opt) == false) {
                    //                //console.log($(opt).text());
                    debugger;
                    opt.disabled = false;
                }
            });
        }
        $(ctrl).select2();
    }
    else {
        $(ctrl).find("option").each(function (i, opt) {
            if (jQuery.isEmptyObject(opt) == false) {
                //                //console.log($(opt).text());
                debugger;
                opt.disabled = false;
            }
        });
        $(ctrl).select2();
    }
}

function BindAllergiesAllergen(val) {
    var data = [];
    data.push({ id: "", text: "" });
    var selectList = $("#ddlAllergen");
    selectList.empty();
    $("#txtAllergenOther").val("");


    $("#ddlAllergen").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve'
    });
    $("#ddlAllergen").select2("val", "0");
    $("#ddlAllergen").trigger('change.select2');

    if (val == 207 || val == null) {
        $("#divtxtAller").css("visibility", "hidden");
        $("#divtxtAller").css("display", "none");
        $("#divddlAller").css("visibility", "visible");
        $("#divddlAller").css("display", "block");

        $("#ddlAllergen").select2({
            placeholder: {
                id: '0', // the value of the option
                text: 'Select an option'
            },
            allowClear: true,
            width: 'resolve',
            data: drugAllergen
        });
        $("#ddlAllergen").select2("val", "0");
        $("#ddlAllergen").trigger('change.select2');
    }
    else if (val == 208) {
        $("#divtxtAller").css("visibility", "hidden");
        $("#divtxtAller").css("display", "none");
        $("#divddlAller").css("visibility", "visible");
        $("#divddlAller").css("display", "block");

        $("#ddlAllergen").select2({
            placeholder: {
                id: '0', // the value of the option
                text: 'Select an option'
            },
            allowClear: true,
            width: 'resolve',
            data: envAllergen
        });
        $("#ddlAllergen").select2("val", "0");
        $("#ddlAllergen").trigger('change.select2');
    }
    else if (val == 209) {
        $("#divtxtAller").css("visibility", "hidden");
        $("#divtxtAller").css("display", "none");
        $("#divddlAller").css("visibility", "visible");
        $("#divddlAller").css("display", "block");

        $("#ddlAllergen").select2({
            placeholder: {
                id: '0', // the value of the option
                text: 'Select an option'
            },
            allowClear: true,
            width: 'resolve',
            data: foodAllergen
        });
        $("#ddlAllergen").select2("val", "0");
        $("#ddlAllergen").trigger('change.select2');
    }
    else if (val == 211) {
        $("#divtxtAller").css("visibility", "visible");
        $("#divtxtAller").css("display", "block");
        $("#divddlAller").css("visibility", "hidden");
        $("#divddlAller").css("display", "none");

    }
    else {
        $("#divtxtAller").css("visibility", "hidden");
        $("#divtxtAller").css("display", "none");
        $("#divddlAller").css("visibility", "visible");
        $("#divddlAller").css("display", "block");

        $("#ddlAllergen").select2({
            placeholder: {
                id: '0', // the value of the option
                text: 'Select an option'
            },
            allowClear: true,
            width: 'resolve',
            data: drugAllergen
        });
        $("#ddlAllergen").select2("val", "0");
        $("#ddlAllergen").trigger('change.select2');
    }
}

function AddMilestones() {
    var ddlPC = $("#ddlMilestone").select2("val");
    var data = $("#ddlMilestone").select2('data')[0];
    var dtAssessed = GetDateData("dtAssessed", true);
    var ddlStatus = $("#ddlStatus").select2("val");
    var data1 = $("#ddlStatus").select2('data')[0];
    var txtComment = $("#txtComment").val();
    var vid = $("#hidVId").val();

    if (ddlPC == null) {
        customAlert("Please select milestone assessed from the list");
        return;
    }

    if ($("#dtAssessed").val().length == 0) {
        customAlert("Please enter date assessed");
        return;
    }
    if (ddlStatus == null) {
        customAlert("Please select status from the list");
        return;
    }

    //debugger;
    var chkData = false;
    $.each(arrMilestones, function (index, arrD) {
        if (jQuery.isEmptyObject(arrD) == false) {
            if (jQuery.isEmptyObject(arrD.DA) == false) {
                var dt = new Date(parseInt(arrD.DA.substr(6))).toString();
                dt = dt.substr(0, dt.indexOf('GMT'));
                da = convertToJSONDate(dt, true);
                var dtDA = new Date(parseInt(da.substr(6)));
                dtDA.setHours(0, 0, 0, 0);
                var dt = new Date(parseInt(dtAssessed.substr(6)));
                dt.setHours(0, 0, 0, 0);
                if (arrD.MA == data.id) {
                    if (dtDA.getTime() === dt.getTime()) {
                        chkData = true;
                    }
                }
            }
        }
    });

    if (chkData == false) {
        var IsA = 0;
        var IsAval = "false";
        if (chkStillActive == "true") {
            IsA = 1;
            IsAval = "true";
        }
        arrMilestones.push({ MA: data.id, MAT: data.text, DA: dtAssessed, displayDate: $("#dtAssessed").val(), IsA: IsA, IsAval: IsAval, ST: data1.id, STA: data1.text, CM: txtComment, VId: 0 });
    }
    else {
        customAlert("Already exits in the table.");
    }
    DrawDataTable("dtlMilestones", arrMilestones);
}

function AddImmunization() {
    //debugger;
    var ddlIP = $("#ddlImmunizationPeriod").select2("val");
    var data = $("#ddlImmunizationPeriod").select2('data')[0];
    var ddlIG = $("#ddlImmunizationGiven").select2("val");
    var data1 = $("#ddlImmunizationGiven").select2('data')[0];
    var dtDateImmunized = GetDateData("dtDateImmunized", true);
    var errorField = '';

    if (ddlIP == null) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Immunization period ';
    }

    if (ddlIG == null) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Immunization given ';
    }

    if ($("#dtDateImmunized").val().length == 0) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Immunization date ';
    }

    if (isNaN(errorField)) {
        customAlert(errorField + 'cannot be blank.');
        return;
    }

    var dataExists = false;
    $.each(arrImmunization, function (index, arrD) {
        if (jQuery.isEmptyObject(arrD.DT) == false) {
            var dt = new Date(parseInt(arrD.DT.substr(6))).toString();
            dt = dt.substr(0, dt.indexOf('GMT'));
            di = convertToJSONDate(dt, true);
            var dtDT = new Date(parseInt(di.substr(6)));
            dtDT.setHours(0, 0, 0, 0);
            var dt = new Date(parseInt(dtDateImmunized.substr(6)));
            dt.setHours(0, 0, 0, 0);
            if (arrD.IP == data.id) {
                if (dtDT.getTime() === dt.getTime()) {
                    dataExists = true;
                }
            }
        }
    });

    if (dataExists == false) {
        arrImmunization.push({ IP: data.id, IPT: data.text, IG: data1.id, IGT: data1.text, DT: dtDateImmunized, displayDate: $("#dtDateImmunized").val(), VId: 0 });
    }
    else {
        customAlert("Already exits in the table.");
    }

    DrawDataTable("dtlImmunizationHistory", arrImmunization);

}

function AddTannerStaging() {
    var ddlMOrF = $("#ddlMOrF").select2("val");
    var data = $("#ddlMOrF").select2('data')[0];
    var ddlPublicHair = $("#ddlPublicHair").select2("val");
    var data1 = $("#ddlPublicHair").select2('data')[0];
    var hidGender = $("#hidGender").val();
    var vid = $("#hidVId").val();
    var dtTSDate = GetDateData("dtTSDate", true);
    var errorField = '';

    if (ddlMOrF == null) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'F Breasts ';
    }

    if (ddlPublicHair == null) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Public hair ';
    }

    if ($("#dtTSDate").val().length == 0) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Tanners staging date ';
    }

    if (isNaN(errorField)) {
        customAlert(errorField + 'cannot be blank.');
        return;
    }
    //var chkData = $.grep(arrTannerStaging, function (e) { return e.SD == dtTSDate; });
    //debugger;
    var chkData = false;
    $.each(arrTannerStaging, function (index, arrD) {
        if (jQuery.isEmptyObject(arrD) == false) {
            if (jQuery.isEmptyObject(arrD.SD) == false) {
                var dt = new Date(parseInt(arrD.SD.substr(6))).toString();
                dt = dt.substr(0, dt.indexOf('GMT'));
                sd = convertToJSONDate(dt, true);
                var dtSD = new Date(parseInt(sd.substr(6)));
                dtSD.setHours(0, 0, 0, 0);
                var dt = new Date(parseInt(dtTSDate.substr(6)));
                dt.setHours(0, 0, 0, 0);
                //if (arrD.MA == data.id) {
                if (dtSD.getTime() === dt.getTime()) {
                    chkData = true;
                }
                //}
            }
            debugger;
            if (jQuery.isEmptyObject(arrD.BOG) == false) {
                var fbG = arrD.BOG.toString();
                var fbC = data.id.toString();
                if (fbG >= fbC) {
                    chkData = true;
                }
            }
            if (jQuery.isEmptyObject(arrD.PH) == false) {
                var phG = arrD.PH.toString();
                var phC = data1.id.toString();
                if (phG >= phC) {
                    chkData = true;
                }
            }

        }
    });

    if (chkData == false) {
        arrTannerStaging.push({ SD: dtTSDate, displayDate: $("#dtTSDate").val(), Gender: hidGender, BOG: data.id, PH: data1.id, VId: 0 });
    }
    else {
        customAlert("Already exits in the table.<br /> F Breasts and Public Hair should only be Incremental.");
    }

    DrawDataTable("dtlTannerStaging", arrTannerStaging);

}

function AddLTM() {
    var ddlDrugList = $("#ddlDrugList").select2("val");
    var data = $("#ddlDrugList").select2('data')[0];
    var ddlDHReason = $("#ddlDHReason").select2("val");
    var data1 = $("#ddlDHReason").select2('data')[0];
    var vid = $("#hidVId").val();
    var dtDHStartDate = GetDateData("dtDHStartDate", true);
    var errorField = '';

    if (ddlDrugList == null) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Drug ';
    }
    else {
        if (data.text.toLowerCase().indexOf("other") >= 0) {
            if ($("#txtDrugOther").val().length == 0) {
                if (errorField.length > 1) {
                    errorField += ', ';
                }
                errorField += 'Drug Other Reason ';
            }
        }
    }

    if (ddlDHReason == null) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Reason ';
    }
    else {
        if (data1.text.toLowerCase().indexOf("other") >= 0) {
            if ($("#txtOtherReason").val().length == 0) {
                if (errorField.length > 1) {
                    errorField += ', ';
                }
                errorField += 'Other Reason ';
            }
        }
    }

    if ($("#dtDHStartDate").val().length == 0) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Start date ';
    }

    if (isNaN(errorField)) {
        customAlert(errorField + 'cannot be blank.');
        return;
    }

    var chkData = $.grep(arrLTM, function (e) { return e.ValId == data.id });

    if (jQuery.isEmptyObject(chkData) == true) {
        var reasontext = '';
        var isOther = false;
        var reasontext = '';
        var isDrugOther = false;
        var drugText = ''
        if (data1.text.toLowerCase().indexOf("other") >= 0) {
            reasontext = $("#txtOtherReason").val();
            isOther = true;
        }
        else {
            reasontext = data1.text;
        }
        if (data.text.toLowerCase().indexOf("other") >= 0) {
            drugText = $("#txtDrugOther").val();
            isDrugOther = true;
        }
        else {
            drugText = data.text;
        }

        arrLTM.push({ FN: "LongTermMedications", ValId: data.id, ValT: drugText, OSD: dtDHStartDate, NT: data1.id, NTT: reasontext, displayDate: $("#dtDHStartDate").val(), isOther: isOther, VId: 0, isDrugOther: isDrugOther });
    }
    else {
        customAlert("Already exits in the table.");
    }
    DrawDataTable("dtlDrugList", arrLTM);

}

function AddPriorART() {
    var ddlARTPurpose = $("#ddlARTPurpose").select2("val");
    var data = $("#ddlARTPurpose").select2('data')[0];
    var ddlRegimen = $("#ddlRegimen").select2("val");
    var data1 = $("#ddlRegimen").select2('data')[0];
    var vid = $("#hidVId").val();
    var dtARTLDU = GetDateData("dtARTLDU", true);
    var errorField = '';

    if (ddlARTPurpose == null) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Prior ART purpose ';
    }

    if (ddlRegimen == null) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Regimen ';
    }

    if ($("#dtARTLDU").val().length == 0) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Last date used';
    }

    if (isNaN(errorField)) {
        customAlert(errorField + 'cannot be blank.');
        return;
    }

    var chkData = $.grep(arrPriorART, function (e) { return e.PID == data.id && e.RId == data1.id; });

    if (jQuery.isEmptyObject(chkData) == true) {

        arrPriorART.push({ PID: data.id, PT: data.text, RId: data1.id, RT: data1.text, Regimen: data1.text, LDU: dtARTLDU, displayDate: $("#dtARTLDU").val(), VId: 0 });
    }
    else {
        customAlert("Already exits in the table.");
    }
    DrawDataTable("dtlPriorART", arrPriorART);

}

function AddDrugAllergies() {
    var ddlAllergenType = $("#ddlAllergenType").select2("val");
    var data = $("#ddlAllergenType").select2('data')[0];
    var ddlAllergen = $("#ddlAllergen").select2("val");
    var data1 = $("#ddlAllergen").select2('data')[0];
    var ddlSeverity = $("#ddlSeverity").select2("val");
    var data2 = $("#ddlSeverity").select2('data')[0];
    var dtDateOfAllergen = GetDateData("dtDateOfAllergen", true);
    var txtAllergenOther = $("#txtAllergenOther").val();
    var txtTypeofReaction = $("#txtTypeofReaction").val();
    var txtAllergenComment = $("#txtAllergenComment").val();

    var errorField = '';
    var allergenVal = "";
    var allerTID = 0;

    if (ddlAllergenType == null) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Allergen Type ';
    }
    if (data != undefined) {
        if (data.text == "Other") {
            if (txtAllergenOther.length == 0) {
                if (errorField.length > 1) {
                    errorField += ', ';
                }
                errorField += 'Allergen ';
            }
        }
        else {
            if (ddlAllergen == null) {
                if (errorField.length > 1) {
                    errorField += ', ';
                }
                errorField += 'Allergen ';
            }
        }
    }
    else {
        if (ddlAllergen == null) {
            if (errorField.length > 1) {
                errorField += ', ';
            }
            errorField += 'Allergen ';
        }
    }
    if (data1 != undefined) {
        allerTID = data1.id;
    }
    if (ddlSeverity == null) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Severity ';
    }

    if ($("#dtDateOfAllergen").val().length == 0) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Date Of Allergen ';
    }

    if ($("#txtTypeofReaction").val().length == 0) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Reaction Type ';
    }

    if ($("#txtAllergenComment").val().length == 0) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Comment ';
    }

    if (isNaN(errorField)) {
        customAlert(errorField + 'cannot be blank.');
        return;
    }


    if (data.text == "Other") {
        allergenVal = txtAllergenOther;
    }
    else {
        allergenVal = data1.text;
    }

    var chkData = $.grep(arrDrugAllergies, function (e) { return e.ATID == data.id && e.AllerDesc == allergenVal; });

    if (jQuery.isEmptyObject(chkData) == true) {
        arrDrugAllergies.push({ ATID: data.id, ATD: data.text, Allergen: allergenVal, AllerDesc: allergenVal,
            AllerTID: allerTID,
            OAller: txtAllergenOther,
            STID: data2.id,
            SDesc: data2.text,
            DAller: dtDateOfAllergen,
            displayDAller: $("#dtDateOfAllergen").val(),
            TR: txtTypeofReaction,
            Comment: txtAllergenComment
            //,VId: 0
        });
    }
    else {
        customAlert("Already exits in the table.");
    }
    DrawDataTable("dtlDrugAllergies", arrDrugAllergies);

}

function AddFamilyMemberHIV() {
    var ddlRegistered = $("#ddlRegistered").select2("val");
    var data = $("#ddlRegistered").select2('data')[0];

    var ddlSex = $("#ddlSexFM").select2("val");
    var data1 = $("#ddlSexFM").select2('data')[0];

    var ddlRelationshipType = $("#ddlRelationshipType").select2("val");
    var data2 = $("#ddlRelationshipType").select2('data')[0];
    var ddlHIVStatus = $("#ddlHIVStatus").select2("val");
    var data3 = $("#ddlHIVStatus").select2('data')[0];
    var ddlHIVCareStatus = $("#ddlHIVCareStatus").select2("val");
    var data4 = $("#ddlHIVCareStatus").select2('data')[0];

    var txtFirstName = $("#txtFirstName").val();
    var txtLastName = $("#txtLastName").val();
    var txtAge = $("#txtAge").val();

    var dtLastHIVTestDate = GetDateData("dtLastHIVTestDate", true);

    var errorField = "";
    if (ddlRegistered == null) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Registered ';
    }

    if (ddlSex == null) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Sex ';
    }

    if (ddlRelationshipType == null) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Relationship Type ';
    }

    if (ddlHIVStatus == null) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'HIV Status ';
    }

    if (ddlHIVCareStatus == null) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'HIV Care Status ';
    }

    if ($("#txtFirstName").val().length == 0) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'First Name ';
    }

    if ($("#txtLastName").val().length == 0) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Last Name ';
    }
    if ($("#txtAge").val().length == 0) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Age ';
    }

    if (ddlHIVStatus != null && data3.text.toUpperCase() != "UNKNOWN") {
        if ($("#dtLastHIVTestDate").val().length == 0) {
            if (errorField.length > 1) {
                errorField += ', ';
            }
            errorField += 'Last HIV Test Date ';
        }
    }

    if (isNaN(errorField)) {
        customAlert(errorField + 'cannot be blank.');
        return;
    }

    var chkData = $.grep(arrHIVFamilyMember, function (e) { return e.FN == txtFirstName && e.LN == txtLastName && e.Age == txtAge; });

    if (jQuery.isEmptyObject(chkData) == true) {

        arrHIVFamilyMember.push({ RE: data.id, RET: data.text, FN: txtFirstName, LN: txtLastName,
            Sex: data1.id,
            SexT: data1.text,
            Age: txtAge,
            RT: data2.id,
            RTT: data2.text,
            HS: data3.id,
            HST: data3.text,
            LHTD: dtLastHIVTestDate,
            diaplayLHTD: $("#dtLastHIVTestDate").val(),
            HCS: data4.id,
            HCST: data4.text
            //,VId: 0
        });
    }
    else {
        customAlert("Already exits in the table.");
    }
    DrawDataTable("dtlFMHIV", arrHIVFamilyMember);

}

function SaveAddtionalHx() {
    var action = "saveadx";
    activePage = getPageName() + '.aspx';
    var dataADX = PrepareADXSaveData();
    var errorMsg = CheckADXBlankValues();
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
            data: JSON.stringify(dataADX),
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            async: true,
            cache: false,
            beforeSend: function () {
            },
            success: function (response) {
                //console.log(response);
                var responseSuccess = response.Success;
                if (responseSuccess == "true") {
                    GetAddtionalHxData();
                    customAlert("AdditionalHx tab " + dataSuccessMessage.toLowerCase());
                    GetNextPage("tab_4");
                }
                else {
                    if (responseSuccess == "false") {
                        customAlert(errorMessage);
                    }
                    else {
                        customAlert(responseSuccess);
                        //GetPresentingComplaintsData();
                    }
                }

                //$.eva.loader('hide');
            },
            error: function (msg) {
                //ShowErrorMessage(msg);
            }
        });
    }

}

function CheckADXBlankValues() {
    errorMsg = '';
    var errorField = '';


    var chkPriorART = GetSwitchValue("chkPriorART");

    if (chkPriorART == 1) {
        if (jQuery.isEmptyObject(arrPriorART) == true) {
            errorField += 'Prior ART ';
        }
    }


    //    if ($("#dt1stLineDate").val().length == 0) {
    //        if (errorField.length > 1) {
    //            errorField += ', ';
    //        }
    //        errorField += 'Start ART 1st Line Regimen Date ';
    //    }

    //    if (ddlCtrl == null) {
    //        errorField += 'Visit type ';
    //    }


    var age = $("#hidDOB").val();
    var visitType = $("#ddlVisitType").select2("val");
    var visitTypeData = $("#ddlVisitType").select2('data')[0];

    if (age < 5) {
        if (visitType != null) {
            if (visitTypeData.text.toUpperCase().indexOf("INITIAL") >= 0) {
                if (jQuery.isEmptyObject(arrMilestones) == true) {
                    if (errorField.length > 1) {
                        errorField += ', ';
                    }
                    errorField += 'Milestones ';
                }
                if (jQuery.isEmptyObject(arrImmunization) == true) {
                    if (errorField.length > 1) {
                        errorField += ', ';
                    }
                    errorField += 'Immunization History ';
                }
            }
        }
    }

    if (age > 9 && age < 20) {
        if (visitType != null) {
            if (visitTypeData.text.toUpperCase().indexOf("INITIAL") >= 0) {
                if (jQuery.isEmptyObject(arrTannerStaging) == true) {
                    if (errorField.length > 1) {
                        errorField += ', ';
                    }
                    errorField += 'Tanners Staging ';
                }
            }
        }
    }



    if (isNaN(errorField)) {
        errorMsg = errorField + 'cannot be blank.';
    }
    return errorMsg;
}

function PrepareADXSaveData() {
    var localdtformat = true;
    //alert(GetDateData("dtVisitDate", localdtformat));
    rowADXData = [];
    /*
    var ddlARTStartRegimen = $("#ddlARTStartRegimen").select2('data')[0]; // $("#ddlARTStartRegimen").select2("val");
    var ddlARTWHOStage = $("#ddlARTWHOStage").select2('data')[0]; // $("#ddlARTWHOStage").select2("val");
    var ddlPartnerStatus = $("#ddlPartnerStatus").select2('data')[0]; // $("#ddlPartnerStatus").select2("val");
    var ddlPartnerGender = $("#ddlPartnerGender").select2('data')[0]; // $("#ddlPartnerGender").select2("val");
    var ddlSexualOrientation = $("#ddlSexualOrientation").select2('data')[0]; // $("#ddlSexualOrientation").select2("val");
    var ddlHighRiskBehaviour = $("#ddlHighRiskBehaviour").select2('data'); //  $("#ddlHighRiskBehaviour").select2("val");
    */
    var ddlPartnerStatus = $("#ddlPartnerStatus").select2("val");
    if (ddlPartnerStatus == null) {
        ddlPartnerStatus = 0;
    }

    //    var ddlARTStartRegimen = $("#ddlARTStartRegimen").select2("val");
    //    if (ddlARTStartRegimen == null) {
    //        ddlARTStartRegimen = 0;
    //    }
    //    var ddlARTWHOStage = $("#ddlARTWHOStage").select2("val");
    //    if (ddlARTWHOStage == null) {
    //        ddlARTWHOStage = 0;
    //    }
    var ddlPartnerGender = $("#ddlPartnerGender").select2("val");
    if (ddlPartnerGender == null) {
        ddlPartnerGender = 0;
    }
    var ddlSexualOrientation = $("#ddlSexualOrientation").select2("val");
    if (ddlSexualOrientation == null) {
        ddlSexualOrientation = 0;
    }

    var txtNeonatalNotes = $("#txtNeonatalNotes").val();
    var txtAlternativeMedicine = $("#txtAlternativeMedicine").val();
    //var txtASAFWeight = $("#txtASAFWeight").val();
    //var txtASAFHeight = $("#txtASAFHeight").val();
    //var txtASAFBMI = $("#txtASAFBMI").val();
    var txtPartners = $("#txtPartners").val();

    rowADXData.push({ Ptn_Pk: 0,
        LId: 0,
        VId: 0,
        MH: GetMilestones(), //arrMilestones,
        IH: GetImmunizatio(), //arrImmunization,
        NHN: txtNeonatalNotes,
        TSLS: GetDateData("dtLastScreenedOn", true),
        TSNS: GetDateData("dtNextScreenedOn", true),
        TSH: GetTannerStaging(), //arrTannerStaging,
        CLTMF: GetSwitchValue("chkLongTermMedication"),
        CLTM: GetLongTermMedications(),
        PAMF: GetSwitchValue("chkAlternativeMedicine"),
        PAMN: txtAlternativeMedicine,
        PAF: GetSwitchValue("chkPriorART"),
        PAH: GetPriorART(), //arrPriorART,
        //ASAFRD: GetDateData("dt1stLineDate", true),
        //ASAFR: ddlARTStartRegimen,
        //ASAFW: txtASAFWeight,
        //ASAFH: txtASAFHeight,
        //ASAFBMI: txtASAFBMI,
        //ASAFWS: ddlARTWHOStage,
        AllerHF: GetSwitchValue("chkDrugAllergies"),
        AllerH: GetAllergies(), //arrDrugAllergies,
        HIVFMF: GetSwitchValue("chkHIVFamilyMember"),
        HIVFM: GetHIVFamilyMember(),
        SHF: GetSwitchValue("chkSexuallyActive6Months"),
        PNO: txtPartners,
        PS: ddlPartnerStatus,
        PG: ddlPartnerGender,
        SO: ddlSexualOrientation,
        HRB: GetHighRiskBehaviour()
    });

    return rowADXData[0];
}

function GetHighRiskBehaviour() {
    var ddlHighRiskBehaviour = $("#ddlHighRiskBehaviour").select2("val");
    var arrHRB = [];
    if (ddlHighRiskBehaviour !== null) {
        if (ddlHighRiskBehaviour.length > 0) {
            if (jQuery.isEmptyObject(ddlHighRiskBehaviour) == false) {
                $.each(ddlHighRiskBehaviour, function (index, arrD) {
                    if (jQuery.isEmptyObject(arrD) == false) {
                        arrHRB.push({ id: arrD });
                    }
                });
            }
        }
        else {
            arrHRB.push({ id: 0 });
        }
    }
    else {
        arrHRB.push({ id: 0 });
    }

    return arrHRB;
}

function GetMilestones() {
    var rowData2 = [];
    if (jQuery.isEmptyObject(arrMilestones) == false) {
        $.each(arrMilestones, function (index, arrD) {
            if (jQuery.isEmptyObject(arrD) == false) {
                if (arrD.VId == $("#hidVId").val() || arrD.VId == "0") {
                    rowData2.push({ MA: arrD.MA, MAT: arrD.MAT, DA: arrD.DA, displayDate: arrD.displayDate, IsA: arrD.IsA, IsAval: arrD.IsAval, ST: arrD.ST, STA: arrD.STA, CM: arrD.CM });
                }
            }
        });
    }
    return rowData2;
}

function GetLongTermMedications() {
    var rowDataLTM = [];
    if (jQuery.isEmptyObject(arrLTM) == false) {
        $.each(arrLTM, function (index, arrD) {
            debugger;
            if (jQuery.isEmptyObject(arrD) == false) {
                //arrLTM.push
                var reasontext = '';
                if (arrD.isOther) {
                    reasontext = arrD.NTT;
                }
                var drugtext = '';
                if (arrD.isDrugOther) {
                    drugtext = arrD.ValT;
                }
                if (arrD.VId == $("#hidVId").val() || arrD.VId == "0") {
                    rowDataLTM.push({ FN: arrD.FN, ValId: arrD.ValId, ValT: arrD.ValT, OSD: arrD.OSD, NT: arrD.NT, NTT: reasontext, displayDate: arrD.displayDate, VIDON: drugtext });
                }
            }
        });
    }
    return rowDataLTM;
}

function GetHIVFamilyMember() {
    var rowDataHIVFM = [];
    if (jQuery.isEmptyObject(arrHIVFamilyMember) == false) {
        $.each(arrHIVFamilyMember, function (index, arrD) {
            if (jQuery.isEmptyObject(arrD) == false) {
                if (arrD.LHTD !== null) {
                    rowDataHIVFM.push({ id: arrD.id, RE: arrD.RE, RET: arrD.RET, FN: arrD.FN, LN: arrD.LN,
                        Sex: arrD.Sex,
                        SexT: arrD.SexT,
                        Age: arrD.Age,
                        RT: arrD.RT,
                        RTT: arrD.RTT,
                        HS: arrD.HS,
                        HST: arrD.HST,
                        LHTD: arrD.LHTD,
                        diaplayLHTD: arrD.diaplayLHTD,
                        HCS: arrD.HCS,
                        HCST: arrD.HCST
                    });
                }
                else {
                    rowDataHIVFM.push({ id: arrD.id, RE: arrD.RE, RET: arrD.RET, FN: arrD.FN, LN: arrD.LN,
                        Sex: arrD.Sex,
                        SexT: arrD.SexT,
                        Age: arrD.Age,
                        RT: arrD.RT,
                        RTT: arrD.RTT,
                        HS: arrD.HS,
                        HST: arrD.HST,
                        LHTD: arrD.LHTD,
                        diaplayLHTD: arrD.diaplayLHTD,
                        HCS: arrD.HCS,
                        HCST: arrD.HCST
                    });
                }
            }
        });
    }
    return rowDataHIVFM;
}

function GetImmunizatio() {
    var rowDataIM = [];
    if (jQuery.isEmptyObject(arrImmunization) == false) {
        $.each(arrImmunization, function (index, arrD) {
            if (jQuery.isEmptyObject(arrD) == false) {
                if (arrD.VId == $("#hidVId").val() || arrD.VId == "0") {
                    rowDataIM.push({ IP: arrD.IP, IPT: arrD.IPT, IG: arrD.IG, IGT: arrD.IGT, DT: arrD.DT, displayDate: arrD.displayDate, VId: arrD.VId });
                }
            }
        });
    }
    return rowDataIM;
}

function GetAllergies() {
    var rowDataAllergy = [];
    if (jQuery.isEmptyObject(arrDrugAllergies) == false) {
        $.each(arrDrugAllergies, function (index, arrD) {
            if (jQuery.isEmptyObject(arrD) == false) {

                rowDataAllergy.push({ ATID: arrD.ATID, ATD: arrD.ATD, Allergen: arrD.Allergen,
                    AllerDesc: arrD.AllerDesc, AllerTID: arrD.AllerTID, OAller: arrD.OAller,
                    STID: arrD.STID,
                    SDesc: arrD.SDesc,
                    DAller: arrD.DAller,
                    displayDAller: arrD.displayDAller,
                    TR: arrD.TR,
                    Comment: arrD.Comment
                    //,VId: arrD.VId
                });
            }
        });

    }
    return rowDataAllergy;
}

function GetPriorART() {
    var rowDataPART = [];
    if (jQuery.isEmptyObject(arrPriorART) == false) {
        $.each(arrPriorART, function (index, arrD) {
            if (jQuery.isEmptyObject(arrD) == false) {
                if (arrD.VId == $("#hidVId").val() || arrD.VId == "0") {
                    rowDataPART.push({ PID: arrD.PID, PT: arrD.PT, RId: arrD.RId, RT: arrD.RT, Regimen: arrD.Regimen, LDU: arrD.LDU, displayDate: arrD.displayDate });
                }
            }
        });
    }
    return rowDataPART;
}

function GetTannerStaging() {
    var rowDataTS = [];
    if (jQuery.isEmptyObject(arrTannerStaging) == false) {
        $.each(arrTannerStaging, function (index, arrD) {
            if (jQuery.isEmptyObject(arrD) == false) {
                if (arrD.VId == $("#hidVId").val() || arrD.VId == "0") {
                    rowDataTS.push({ SD: arrD.SD, displayDate: arrD.displayDate, Gender: arrD.Gender, BOG: arrD.BOG, PH: arrD.PH });
                }
            }
        });
    }
    return rowDataTS;
}

