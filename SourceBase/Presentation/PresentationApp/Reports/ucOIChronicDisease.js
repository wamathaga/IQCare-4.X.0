var zscoreDetails;
var arrFacilityData = [];
var arrFacilityBaseData = [];
var arrCountryData = [];
var arrICD10 = [];

$(document).ready(function () {
    $.hivce.loader('show');
    arrChronicDisorders = [];
    $("#ddlCurrentWhoStage").select2();
    GetData();
});

function GetData() {

    var activePage = getPageName() + '.aspx';
    var postData;

    //    $.get(activePage + "?data=getdata").then(
    //        function () {
    //            alert("$.get succeeded");
    //        }, function () {
    //            alert("$.get failed!");
    //        }
    //    );
    //debugger;

    // Create a deferred object
    var request = $.ajax({
        type: "POST",
        url: activePage + "?data=getdata",
        //data: JSON.stringify(nodeId),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,
        cache: false
    });

    //    .done(function (html) {
    //        alert("success");
    //    }).fail(function () {
    //        alert("error");
    //    }).always(function () {
    //        alert("complete");
    //    })
    //debugger;


    request.then(function (response) {
        //alert("then");

        //console.log(response);
        //debugger;
        var responseSuccess = response.Success;
        if (responseSuccess != null) {
            if (responseSuccess == "false") {
                customAlert(errorMessage);
            }
        } else {
            InitControls(response);
        }
    });

    request.always(function () {
        //alert('koko');
        $.hivce.loader('hide');
    });


}

function InitControls(response) {
    window.scrollTo(0, 0);
    $('#dtlChronicDisorders').dataTable().fnDestroy();
    ReInitialisePCDatatable("CD");

    BindControls(response);

}

function ReInitialisePCDatatable(from) {
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

    $('#dtlICD10').DataTable({
        "aaData": arrICD10,
        "bSort": false,
        "bPaginate": false,
        "bFilter": false,
        "bInfo": false,
        "autoWidth": true,
        "destroy": true,
        "sSearch": false,
        "aoColumns": [
            { bSortable: false,
                mRender: function (data, type, full) {
                    return full["ICDId"];
                }
            },
            { bSortable: false,
                mRender: function (data, type, full) {
                    return full["ICDN"];
                }
            },
            { bSortable: false,
                mRender: function (data, type, full) {
                    return full["CHN"];
                }
            },
            { bSortable: false,
                mRender: function (data, type, full) {
                    return full["BC"] + " - " + full["BN"];
                }
            },
            { bSortable: false,
                mRender: function (data, type, full) {
                    return full["SBC"] + " - " + full["SBN"];
                }
            }
        ]
    });

}

function BindControls(response) {
    arrICDData = [];
    var collections = response.Collection;
    var mdata = $.grep(collections, function (e) { return e.CN == "ChronicCondition"; });
    $.each(response.CDT, function (index, arrD) {
        if (jQuery.isEmptyObject(arrD) == false) {
            var data = $.grep(mdata, function (e) { return e.DId == arrD.ValId; });
            if (jQuery.isEmptyObject(data) == false) {
                var displayDate = "";
                var osd = arrD.OSD;
                if (jQuery.isEmptyObject(arrD.OSD) == false) {
                    // parse JSON formatted date to javascript date object
                    var dtOSD = new Date(parseInt(arrD.OSD.substr(6)));
                    // format display date (e.g. 04/10/2012)
                    displayDate = $.format.date(dtOSD, "dd-MMM-yyyy");

                    /*var dt = new Date(parseInt(arrD.OSD.substr(6))).toString();
                    console.log(dt); // Mon Jul 16 2012 15:21:09 GMT+0530 (India Standard Time)
                    dt = dt.substr(0, dt.indexOf('GMT'));
                    console.log(dt); // Mon Jul 16 2012 15:21:09 
                    osd = convertToJSONDate(dt, true);*/
                }
                var IsA = 0;
                var IsAVal = "false";
                if (arrD.NF == "1") {
                    IsA = 1;
                    IsAVal = "true";
                }
                arrChronicDisorders.push({ PCID: arrD.ValId, PC: data[0].DN, OSD: arrD.OSD, IsActive: IsA, IsAVal: IsAVal, displayDate: displayDate, VId: arrD.VId });
            }
        }
    });
    BindDataTable(arrChronicDisorders, "divChronicDisorders", "chkChronicDisorders", "dtlChronicDisorders");



    var data = $.grep(response.WHOS, function (e) { return e.FN == "WHOStageIConditions"; });
    if (jQuery.isEmptyObject(data) == false) {

        $("#dtlStageI").dataTable().fnDestroy();

        $("#dtlStageI").DataTable({
            "aaData": data,
            "bSort": false,
            "bPaginate": false,
            "bFilter": false,
            "bInfo": false,
            "autoWidth": false,
            "aoColumns": [
                { bSortable: false,
                    mRender: function (data, type, full) {
                        return full["ValN"];
                    }
                },
                { bSortable: false,
                    mRender: function (data, type, full) {
                        var displayDate = "";
                        if (jQuery.isEmptyObject(full["DF1"]) == false) {
                            // parse JSON formatted date to javascript date object
                            var dtOSD = new Date(parseInt(full["DF1"].substr(6)));
                            // format display date (e.g. 04/10/2012)
                            displayDate = $.format.date(dtOSD, "dd-MMM-yyyy");
                        }
                        return displayDate;
                    }
                },
                {
                    bSortable: false,
                    mRender: function (data, type, full) {
                        var displayDate = "";
                        if (jQuery.isEmptyObject(full["DF2"]) == false) {
                            // parse JSON formatted date to javascript date object
                            var dtOSD = new Date(parseInt(full["DF2"].substr(6)));
                            // format display date (e.g. 04/10/2012)
                            displayDate = $.format.date(dtOSD, "dd-MMM-yyyy");
                        }
                        return displayDate;
                    }
                }
            ]
        });

    }

    data = $.grep(response.WHOS, function (e) { return e.FN == "WHOStageIIConditions"; });
    if (jQuery.isEmptyObject(data) == false) {

        $("#dtlStageII").dataTable().fnDestroy();

        $("#dtlStageII").DataTable({
            "aaData": data,
            "bSort": false,
            "bPaginate": false,
            "bFilter": false,
            "bInfo": false,
            "autoWidth": false,
            "aoColumns": [
                { bSortable: false,
                    mRender: function (data, type, full) {
                        return full["ValN"];
                    }
                },
                { bSortable: false,
                    mRender: function (data, type, full) {
                        var displayDate = "";
                        if (jQuery.isEmptyObject(full["DF1"]) == false) {
                            // parse JSON formatted date to javascript date object
                            var dtOSD = new Date(parseInt(full["DF1"].substr(6)));
                            // format display date (e.g. 04/10/2012)
                            displayDate = $.format.date(dtOSD, "dd-MMM-yyyy");
                        }
                        return displayDate;
                    }
                },
                {
                    bSortable: false,
                    mRender: function (data, type, full) {
                        var displayDate = "";
                        if (jQuery.isEmptyObject(full["DF2"]) == false) {
                            // parse JSON formatted date to javascript date object
                            var dtOSD = new Date(parseInt(full["DF2"].substr(6)));
                            // format display date (e.g. 04/10/2012)
                            displayDate = $.format.date(dtOSD, "dd-MMM-yyyy");
                        }
                        return displayDate;
                    }
                }
            ]
        });

    }

    data = $.grep(response.WHOS, function (e) { return e.FN == "WHOStageIIICoditions"; });
    if (jQuery.isEmptyObject(data) == false) {

        $("#dtlStageIII").dataTable().fnDestroy();

        $("#dtlStageIII").DataTable({
            "aaData": data,
            "bSort": false,
            "bPaginate": false,
            "bFilter": false,
            "bInfo": false,
            "autoWidth": false,
            "aoColumns": [
                { bSortable: false,
                    mRender: function (data, type, full) {
                        return full["ValN"];
                    }
                },
                { bSortable: false,
                    mRender: function (data, type, full) {
                        var displayDate = "";
                        if (jQuery.isEmptyObject(full["DF1"]) == false) {
                            // parse JSON formatted date to javascript date object
                            var dtOSD = new Date(parseInt(full["DF1"].substr(6)));
                            // format display date (e.g. 04/10/2012)
                            displayDate = $.format.date(dtOSD, "dd-MMM-yyyy");
                        }
                        return displayDate;
                    }
                },
                {
                    bSortable: false,
                    mRender: function (data, type, full) {
                        var displayDate = "";
                        if (jQuery.isEmptyObject(full["DF2"]) == false) {
                            // parse JSON formatted date to javascript date object
                            var dtOSD = new Date(parseInt(full["DF2"].substr(6)));
                            // format display date (e.g. 04/10/2012)
                            displayDate = $.format.date(dtOSD, "dd-MMM-yyyy");
                        }
                        return displayDate;
                    }
                }
            ]
        });

    }

    data = $.grep(response.WHOS, function (e) { return e.FN == "WHOStageIVConditions"; });
    if (jQuery.isEmptyObject(data) == false) {

        $("#dtlStageIV").dataTable().fnDestroy();

        $("#dtlStageIV").DataTable({
            "aaData": data,
            "bSort": false,
            "bPaginate": false,
            "bFilter": false,
            "bInfo": false,
            "autoWidth": false,
            "aoColumns": [
                { bSortable: false,
                    mRender: function (data, type, full) {
                        return full["ValN"];
                    }
                },
                { bSortable: false,
                    mRender: function (data, type, full) {
                        var displayDate = "";
                        if (jQuery.isEmptyObject(full["DF1"]) == false) {
                            // parse JSON formatted date to javascript date object
                            var dtOSD = new Date(parseInt(full["DF1"].substr(6)));
                            // format display date (e.g. 04/10/2012)
                            displayDate = $.format.date(dtOSD, "dd-MMM-yyyy");
                        }
                        return displayDate;
                    }
                },
                {
                    bSortable: false,
                    mRender: function (data, type, full) {
                        var displayDate = "";
                        if (jQuery.isEmptyObject(full["DF2"]) == false) {
                            // parse JSON formatted date to javascript date object
                            var dtOSD = new Date(parseInt(full["DF2"].substr(6)));
                            // format display date (e.g. 04/10/2012)
                            displayDate = $.format.date(dtOSD, "dd-MMM-yyyy");
                        }
                        return displayDate;
                    }
                }
            ]
        });

    }

    data = $.grep(collections, function (e) { return e.CN == "WHO Stage"; });
    var vdata = [];
    $.each(data, function (index, value) {
        vdata.push({ id: value.DId, text: value.DN });
    });
    arrCWHoStage = vdata;

    $("#ddlCurrentWhoStage").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        data: vdata
    });
    $("#ddlCurrentWhoStage").select2("val", "0");
    $("#ddlCurrentWhoStage").trigger('change.select2');

    $("#ddlCurrentWhoStage").select2().val(response.WS).trigger("change");

    //    AssignDataTableData("dtlStageI", response.WHOS, "WHOStageIConditions");
    //    AssignDataTableData("dtlStageII", response.WHOS, "WHOStageIIConditions");
    //    AssignDataTableData("dtlStageIII", response.WHOS, "WHOStageIIICoditions");
    //    AssignDataTableData("dtlStageIV", response.WHOS, "WHOStageIVConditions");


    if (jQuery.isEmptyObject(response.ICDGV) == false) {
        $.each(response.ICDGV, function (index, value) {
            if (jQuery.isEmptyObject(value) == false) {

                arrICD10.push({
                    CID: value.CID,
                    CHN: value.CHN,
                    BID: value.BID,
                    BC: value.BC,
                    BN: value.BN,
                    SBID: value.SBID,
                    SBC: value.SBC,
                    SBN: value.SBN,
                    ICDId: value.ICDId,
                    ICDC: value.ICDC,
                    ICDN: value.ICDN
                });
            }
        });

        DrawDataTable("dtlICD10", arrICD10);

    }


}

function BindDataTable(dtRows, disDiv, switchName, dtTable) {

    if (dtRows.length > 0) {
        $("#" + switchName).bootstrapSwitch('state', true);
        var ctrlVal = $("#" + switchName).bootstrapSwitch('state');
        $("#" + disDiv).removeAttr("style");
        if (ctrlVal) {
            $("#" + disDiv).css("visibility", "visible");
            $("#" + disDiv).css("display", "block");
        }
        else {
            $("#" + disDiv).css("visibility", "hidden");
            $("#" + disDiv).css("display", "none");
        }
        DrawDataTable(dtTable, dtRows);
    }
}

function AssignDataTableData(dataTableName, arrObject, arrFilterCriteria) {
    var dtData = $.grep(arrObject, function (e) { return e.FN == arrFilterCriteria; });
    var rows = $("#" + dataTableName).dataTable().fnGetNodes();
    for (var i = 0; i < rows.length; i++) {

        // Get HTML of 3rd column (for example)
        var col0 = $(rows[i]).find("td:eq(0)").html();
        var hidV = $(col0).filter("#hidRowId").val();
        var chkVal = $("#chkAchieved" + hidV).iCheck('update')[0].checked;
        var col2 = $(rows[i]).find("td:eq(2)").html();
        var col3 = $(rows[i]).find("td:eq(3)").html();

        var filterDate = $.grep(dtData, function (e) { return e.ValId == hidV; });
        if (jQuery.isEmptyObject(filterDate) == false) {
            $("#chkAchieved" + hidV).iCheck('check');
            CheckDatenAssign(filterDate[0].DF1, "dtCurrent" + hidV, false);
            CheckDatenAssign(filterDate[0].DF2, "dtHistory" + hidV, false);

        }
    }
}