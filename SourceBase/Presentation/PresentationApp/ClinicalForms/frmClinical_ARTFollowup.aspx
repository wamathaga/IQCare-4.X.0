<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    Inherits="frmClinical_ARTFollowup" Title="Untitled Page" CodeBehind="frmClinical_ARTFollowup.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <style type="text/css">
        .bottomRow
        {
            border-bottom: solid 1px #cccccc;
        }
        .LeftMrgn
        {
            margin-left: 5px;
        }
        .supMrgn
        {
            padding-top: 6px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function addDays(id) {

            txtdate = document.getElementById('<%= txtvisitDate.ClientID%>').value
            var yr1 = txtdate.substr(7, 4);
            var mm1 = txtdate.substr(3, 3);
            var dd1 = txtdate.substr(0, 2);
            var nmm1;
            switch (mm1.toLowerCase()) {
                case "jan": nmm1 = "01";
                    break;
                case "feb": nmm1 = "02";
                    break;
                case "mar": nmm1 = "03";
                    break;
                case "apr": nmm1 = "04";
                    break;
                case "may": nmm1 = "05";
                    break;
                case "jun": nmm1 = "06";
                    break;
                case "jul": nmm1 = "07";
                    break;
                case "aug": nmm1 = "08";
                    break;
                case "sep": nmm1 = "09";
                    break;
                case "oct": nmm1 = "10";
                    break;
                case "nov": nmm1 = "11";
                    break;
                case "dec": nmm1 = "12";
                    break;
            }
            dt1 = nmm1 + "/" + dd1 + "/" + yr1;
            dateParts = dt1.split('/');
            newDays = document.getElementById('<%= lstappPeriod.ClientID %>').value;
            if (newDays == 0) {
                document.getElementById('<%=txtappDate.ClientID%>').value = "";
                document.getElementById('<%=ddlAppReason.ClientID%>').value = 0;
                document.getElementById('<%=ddlCounsellorSignature.ClientID%>').value = 0;
            }
            else {
                year = Number(dateParts[2]);
                month = Number(dateParts[0]) - 1;
                day = Number(dateParts[1]) + parseInt(newDays);
                newDate = new Date(year, month, day);
                year = newDate.getFullYear();
                month = newDate.getMonth() + 1;
                month = (month < 10) ? '0' + month : month;
                day = newDate.getDate();
                day = (day < 10) ? '0' + day : day;

                formattedDate = month + '/' + day + '/' + year;
                var yr2 = formattedDate.substr(6, 4);
                var mm2 = formattedDate.substr(0, 2);
                var dd2 = formattedDate.substr(3, 2);
                var nmm2;
                switch (mm2) {
                    case "01": nmm2 = "Jan";
                        break;
                    case "02": nmm2 = "Feb";
                        break;
                    case "03": nmm2 = "Mar";
                        break;
                    case "04": nmm2 = "Apr";
                        break;
                    case "05": nmm2 = "May";
                        break;
                    case "06": nmm2 = "Jun";
                        break;
                    case "07": nmm2 = "Jul";
                        break;
                    case "08": nmm2 = "Aug";
                        break;
                    case "09": nmm2 = "Sep";
                        break;
                    case "10": nmm2 = "Oct";
                        break;
                    case "11": nmm2 = "Nov";
                        break;
                    case "12": nmm2 = "Dec";
                        break;
                }
                dt2 = dd2 + "-" + nmm2 + "-" + yr2;
                document.getElementById('<%=txtappDate.ClientID%>').value = dt2;
                if (txtdate == "") {
                    document.getElementById('<%=txtappDate.ClientID%>').value = "";
                }
            }
            return;

        }
        function WindowPrint() {
            document.getElementById('divReasonMissed').className = '';
            document.getElementById('divpreComplain').className = '';
            document.getElementById('divArvsideleft').className = '';
            document.getElementById('divARVsideright').className = '';
            document.getElementById('divTBScreen').className = '';
            document.getElementById('diventerillnessleft').className = '';
            document.getElementById('diventerillnessright').className = '';
            window.print();
            document.getElementById('divReasonMissed').className = 'divborder';
            document.getElementById('divpreComplain').className = 'checkbox';
            document.getElementById('divArvsideleft').className = 'checkboxleft';
            document.getElementById('divARVsideright').className = 'checkboxright';
            document.getElementById('diventerillnessleft').className = 'checkboxright';
            document.getElementById('diventerillnessright').className = 'checkboxleft';
        }

        function CalcualteBMI(txtBMI, txtWeight, txtHeight) {
            var weight = document.getElementById(txtWeight).value;
            var height = document.getElementById(txtHeight).value;
            if (weight == "" || height == "") {
                weight = 0;
                height = 0;
            }

            weight = parseFloat(weight);
            height = parseFloat(height);

            var BMI = weight / ((height / 100) * (height / 100));
            BMI = BMI.toFixed(2);
            document.getElementById(txtBMI).value = BMI;
        }

        function SendCodeName() {

            var id = document.getElementById('<%=txtvisitDate.ClientID%>').value;
            if (id.length <= 0) {
                return true;
            }
            else {
                CallServer(id, "This is context from client");
                return true;
            }

        }

        function parseXml(xml, flag) {

            var oXml = xml;
            var xml = $.parseXML(xml);
            $xml = $(xml);

            if (flag == 1) {
                //CD4 Values
                document.getElementById('<%=txtTestCD4Results.ClientID%>').value = "";
                document.getElementById('<%=txtTestResultsDate.ClientID%>').value = "";
                if ($xml.find("ExistCD4").text() != 0) {
                    document.getElementById('<%=txtTestCD4Results.ClientID%>').value = $xml.find("CD4TestResult").text()
                    formattedDate = $xml.find("LastCD4Date").text()
                    var yr = formattedDate.substr(7, 4);
                    var mm = formattedDate.substr(3, 3);
                    var dd = formattedDate.substr(0, 2);
                    formattedDate = dd + "-" + mm + "-" + yr;
                    document.getElementById('<%=txtTestResultsDate.ClientID%>').value = formattedDate;
                }
                //Viral Load Values 
                document.getElementById('<%=txtmostRecentViralLoad.ClientID%>').value = "";
                document.getElementById('<%=txtmostRecentViralLoadDate.ClientID%>').value = "";
                if ($xml.find("ExistViralLoad").text() != 0) {
                    document.getElementById('<%=txtmostRecentViralLoad.ClientID%>').value = $xml.find("ViralLoadTestResult").text();
                    formattedDate = $xml.find("LastViralLoadDate").text()
                    var yr = formattedDate.substr(7, 4);
                    var mm = formattedDate.substr(3, 3);
                    var dd = formattedDate.substr(0, 2);
                    formattedDate = dd + "-" + mm + "-" + yr;
                    document.getElementById('<%=txtmostRecentViralLoadDate.ClientID%>').value = formattedDate;
                }
                //Regimen Type 
                document.getElementById('<%=txtRegimenType.ClientID%>').value = "";
                document.getElementById('<%=txtPrescribedARVStartDate.ClientID%>').value = "";
                if ($xml.find("ExistRegimen").text() != 0) {
                    document.getElementById('<%=txtRegimenType.ClientID%>').value = $xml.find("RegimenType").text();
                    formattedDate = $xml.find("CurrentARTStartDate").text();
                    var yr = formattedDate.substr(7, 4);
                    var mm = formattedDate.substr(3, 3);
                    formattedDate = mm + "-" + yr;
                    document.getElementById('<%=txtPrescribedARVStartDate.ClientID%>').value = formattedDate;
                }
                //Prior to CD4
                document.getElementById('<%=txtpriorARVsCD4.ClientID%>').value = "";
                document.getElementById('<%=txtpriorARVsCD4Date.ClientID%>').value = "";
                document.getElementById('<%=hdnVisitIDIE.ClientID%>').value = "";
                if ($xml.find("Table4").find("Existflag").text() != 0) {
                    document.getElementById('<%=txtpriorARVsCD4.ClientID%>').value = $xml.find("Table4").find("PrevARVsCD4IE").text();
                    document.getElementById('<%=txtpriorARVsCD4Date.ClientID%>').value = $xml.find("Table4").find("PrevARVsCD4DateIE").text();
                    document.getElementById('<%=hdnVisitIDIE.ClientID%>').value = $xml.find("Table5").find("VisitIDIE").text();
                }
            }
            else if (flag == 2) {
                //Height 
                document.getElementById('<%=txtphysHeight.ClientID%>').value = "";
                if ($xml.find("Table").find("Column2").text() == 18) {
                    document.getElementById('<%=txtphysHeight.ClientID%>').value = $xml.find("Table").find("Column1").text();
                }
            }
            else if (flag == 3) {
                var objPreg;
                if (window.DOMParser) {
                    parser = new DOMParser();
                    objPreg = parser.parseFromString(oXml, "text/xml");
                }
                else // Internet Explorer
                {
                    objPreg = new ActiveXObject("MSXML2.DOMDocument");
                    objPreg.loadXML(oXml);
                }

                var dsRootPreg = objPreg.documentElement;
                if (dsRootPreg.text != '') {
                    if ($xml.find("Table").find("Pregnant").text() == 0 && $xml.find("Table").find("Delivered").text() == 1) {
                        document.getElementById('<%=txtEDDDate.ClientID%>').value = "";
                        show('rdopregnantyesno');
                        hide('spdelivery');
                    }
                    else if ($xml.find("Table").find("Pregnant").text() == 0 && $xml.find("Table").find("Delivered").text() == 0) {
                        document.getElementById('<%=txtEDDDate.ClientID%>').value = "";
                        show('rdopregnantyesno');
                        hide('spdelivery');
                    }
                    else if ($xml.find("Table").find("Pregnant").text() == 1 && $xml.find("Table").find("Delivered").text() == 1) {
                        show('rdopregnantyesno');
                        hide('spdelivery');
                        document.getElementById('<%=txtEDDDate.ClientID%>').value = ""
                        document.getElementById('<%=txtLMPdate.ClientID%>').value = ""
                    }
                    else if ($xml.find("Table").find("Pregnant").text() == 9 && $xml.find("Table").find("Delivered").text() == 0) {
                        show('rdopregnantyesno');
                        hide('spdelivery');
                    }
                    else if ($xml.find("Table").find("Pregnant").text() == 1 && $xml.find("Table").find("Delivered").text() == 9) {
                        show('spdelivery');
                        hide('rdopregnantyesno');
                        if ($xml.find("Table").find("EDD").text() != "") {
                            formattedDate = $xml.find("Table").find("EDD").text();
                            var yr = formattedDate.substr(7, 4);
                            var mm = formattedDate.substr(3, 3);
                            var dd = formattedDate.substr(0, 2);
                            formattedDate = dd + "-" + mm + "-" + yr;
                            document.getElementById('<%=txtEDDDate.ClientID%>').value = formattedDate;
                        }
                    }
                    else if ($xml.find("Table").find("Pregnant").text() == 1 && $xml.find("Table").find("Delivered").text() == 0) {
                        show('spdelivery');
                        hide('rdopregnantyesno');
                        if ($xml.find("Table").find("EDD").text() != "") {
                            formattedDate = $xml.find("Table").find("EDD").text();
                            var yr = formattedDate.substr(7, 4);
                            var mm = formattedDate.substr(3, 3);
                            var dd = formattedDate.substr(0, 2);
                            formattedDate = dd + "-" + mm + "-" + yr;
                            document.getElementById('<%=txtEDDDate.ClientID%>').value = formattedDate;
                        }

                    }
                }
            }

        }


        function ReceiveServerData(args, context) {
            var a = args;
            var temp = new Array(); temp = a.split('zzzz');
            var xmlhttp;
            if (window.XMLHttpRequest) {
                // code for IE7+, Firefox, Chrome, Opera, Safari
                xmlhttp = new XMLHttpRequest();
            }
            else {
                // code for IE6, IE5
                xmlhttp = new ActiveXObject("MSXML2.DOMDocument");
            }

            //if (window.ActiveXObject)
            if (xmlhttp != 'undefined') {

                var obj;
                if (window.DOMParser) {
                    parser = new DOMParser();
                    obj = parser.parseFromString(temp[0], "text/xml");
                }
                else // Internet Explorer
                {
                    obj = new ActiveXObject("MSXML2.DOMDocument");
                    obj.loadXML(temp[0]);
                }
                parseXml(temp[0], 1);
                parseXml(temp[1], 2);
                parseXml(temp[2], 3);
            }
        }

        function SenderPregnantLMP() {
            //   var id	= document.getElementById('<%=txtvisitDate.ClientID%>').value;
            //   if (id.length<=0)
            //    {
            //	return true;
            //	}
            //	else
            //	{
            //	CallPregnantLMPServer(id);
            //	return true;
            //	}
        }


        function RecievePregnantData(args, context) {
            //    var temp = new Array(); temp = args.split('zzzz');
            //    if (window.ActiveXObject)
            //    {
            //        var objPreg = new ActiveXObject("MSXML2.DOMDocument");
            //        objPreg.loadXML(temp[2]);
            //        var dsRootPreg=objPreg.documentElement;
            //        if (dsRootPreg.childNodes(0).childNodes(0).firstChild.text != 0);
            //        {   
            //            document.getElementById('<%=txtEDDDate.ClientID%>').value = "";
            //            show('rdopregnantyesno');
            //            hide('spdelivery');
            //        }
            //    }
        }
    </script>
    <script type="text/javascript">
        function RegisterJQuery() {
            $('#txtvisitDate').datepicker({ autoclose: true });
            $('#txtTestResultsDate').datepicker({ autoclose: true });
            $('#txtpriorARVsCD4Date').datepicker({ autoclose: true });
            $('#txtmostRecentViralLoadDate').datepicker({ autoclose: true });
            $('#txtPrescribedARVStartDate').datepicker({ autoclose: true });
            $('#txtLMPdate').datepicker({ autoclose: true });
            $('#txtEDDDate').datepicker({ autoclose: true });
            $('#txtDeliDate').datepicker({ autoclose: true });
            $('#txtInterruptedDate').datepicker({ autoclose: true });
            $('#txtstoppedDate').datepicker({ autoclose: true });
            $('#txtARTEndeddate').datepicker({ autoclose: true });
            $('#txtappDate').datepicker({ autoclose: true });
        }
        //Calling RegisterJQuery when document is ready (Page loaded first time)
        $(document).ready(RegisterJQuery);
        //Calling RegisterJQuery when the page is doing postback (asp.net)
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RegisterJQuery);
    </script>
    <div class="content-wrapper">
        <div class="box-body">
            <div class="row">
                <div class="col-xs-12">
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            <h3 class="box-title">
                                ART Follow-up Form</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body table-responsive no-padding" style="overflow: hidden; margin-left: 5px;">
                            <%--Main Content--%>
                            <div class="row bottomRow">
                                <br />
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label id="lblvdate" class="required">
                                        Visit Date:</label>
                                </div>
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <div class="form-group">
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <input type="text" class="form-control pull-left" id="txtvisitDate" clientidmode="Static"
                                                runat="server" data-date-format="dd-M-yyyy" style="width: 110px;" />
                                            <input id="hdnVisitIDIE" type="hidden" value="0" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label>
                                        Last CD4 Count:</label>
                                </div>
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <div style="width: 100%; margin: 0px auto;">
                                        <div style="float: left">
                                            <input id="txtTestCD4Results" style="width: 100px;" readonly="readonly" runat="server"
                                                size="4" class="form-control" /></div>
                                        <div style="float: left; margin-top: 5px;">
                                            <span class="smallerlabel">c/mm<sup>3</sup></span></div>
                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label>
                                        Date:</label>
                                </div>
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <div class="form-group">
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <input type="text" class="form-control pull-left" id="txtTestResultsDate" clientidmode="Static"
                                                runat="server" data-date-format="dd-M-yyyy" style="width: 110px;" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <br />
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <label style="margin-left: 5px;">
                                        CD4 Prior to Starting ARVs:</label></div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <div style="width: 100%; margin: 0px auto;">
                                        <div style="float: left">
                                            <asp:TextBox ID="txtpriorARVsCD4" runat="server" Width="100" MaxLength="4" CssClass="form-control"></asp:TextBox></div>
                                        <div style="float: left">
                                            <span class="smallerlabel">c/mm<sup>3</sup></span></div>
                                    </div>
                                </div>
                                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                    <label>
                                        Date:</label></div>
                                <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                    <div class="form-group">
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <input type="text" class="form-control pull-left" id="txtpriorARVsCD4Date" clientidmode="Static"
                                                runat="server" data-date-format="dd-M-yyyy" style="width: 110px;" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <label>
                                        Last Viral Load:</label></div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <div style="width: 100%; margin: 0px auto;">
                                        <div style="float: left">
                                            <input id="txtmostRecentViralLoad" style="width: 100px;" readonly="readonly" runat="server"
                                                size="4" class="form-control" /></div>
                                        <div style="float: left">
                                            <span class="smallerlabel">c/ml</span></div>
                                    </div>
                                </div>
                                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                    <label>
                                        Date:</label></div>
                                <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                    <div class="form-group">
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <input type="text" class="form-control pull-left" id="txtmostRecentViralLoadDate"
                                                clientidmode="Static" runat="server" data-date-format="dd-M-yyyy" style="width: 110px;" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label for="RegimenType">
                                        Regimen:</label>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <input id="txtRegimenType" readonly="readonly" runat="server" class="form-control" />
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <label>
                                        Current Regimen Began:</label>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <div class="form-group">
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <input type="text" class="form-control pull-left" id="txtPrescribedARVStartDate"
                                                clientidmode="Static" runat="server" data-date-format="dd-M-yyyy" style="width: 110px;"
                                                maxlength="11" size="8" readonly="readonly" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <table width="100%" class="table-condensed">
                                    <tbody>
                                        <!-- if gender is female then-->
                                        <tr>
                                            <td id="tdPregnant" class="form" runat="server">
                                                <div class="row">
                                                    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                                        <label for="LMP">
                                                            LMP:</label>
                                                    </div>
                                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                        <div class="form-group">
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <i class="fa fa-calendar"></i>
                                                                </div>
                                                                <input type="text" class="form-control pull-left" id="txtLMPdate" clientidmode="Static"
                                                                    runat="server" data-date-format="dd-M-yyyy" style="width: 110px;" maxlength="11"
                                                                    size="8" name="LMP" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                        <span id="rdopregnantyesno" style="display: inline">
                                                            <label id="lblpregnanttmp">
                                                                Pregnant:
                                                            </label>
                                                            <input id="rdopregnantYes" onmouseup="up(this);" onfocus="up(this);" onclick="down(this); show('spanEDD'); "
                                                                type="radio" value="Y" name="pregnant" runat="server" />
                                                            <label>
                                                                Yes</label>
                                                            <input id="rdopregnantNo" onmouseup="up(this);" onfocus="up(this);" onclick="down(this); hide('spanEDD');"
                                                                type="radio" value="N" name="pregnant" runat="server" />
                                                            <label>
                                                                No</label></span>
                                                    </div>
                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                        <span id="spanEDD" style="display: none">
                                                            <div style="width: 100%; margin: 0px auto;">
                                                                <div style="float: left; margin-top: 5px;">
                                                                    <label id="lblEDD" for="EDD">
                                                                        EDD:</label></div>
                                                                <div style="float: left">
                                                                    <div class="form-group">
                                                                        <div class="input-group date">
                                                                            <div class="input-group-addon">
                                                                                <i class="fa fa-calendar"></i>
                                                                            </div>
                                                                            <input type="text" class="form-control pull-left" id="txtEDDDate" clientidmode="Static"
                                                                                runat="server" maxlength="11" size="11" data-date-format="dd-M-yyyy" style="width: 110px;" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <span id="spdelivery" style="display: none">
                                                        <div class="col-md-3 col-sm-12 col-xs-12 form-group" style="margin-left: 10px;">
                                                            <label id="lbldelivered">
                                                                Delivered since last visit:
                                                            </label>
                                                        </div>
                                                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                            <input id="rdoDeliveredYes" onmouseup="up(this);" onfocus="up(this);" onclick="down(this); show('spanDelDate'); hide('spanEDD')"
                                                                type="radio" value="1" name="delivered" runat="server" />
                                                            <label>
                                                                Yes</label>
                                                            <input id="rdoDeliveredNo" onmouseup="up(this);" onfocus="up(this);" onclick="down(this); show('spanEDD'); hide('spanDelDate')"
                                                                type="radio" value="0" name="delivered" runat="server" />
                                                            <label>
                                                                No</label>
                                                        </div>
                                                        <span id="spanDelDate" style="display: none">
                                                            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                                <label id="lblDelDate" for="DelDate">
                                                                    Delivered Date:</label>
                                                            </div>
                                                            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                <div class="form-group">
                                                                    <div class="input-group date">
                                                                        <div class="input-group-addon">
                                                                            <i class="fa fa-calendar"></i>
                                                                        </div>
                                                                        <input type="text" class="form-control pull-left" id="txtDeliDate" clientidmode="Static"
                                                                            runat="server" maxlength="11" size="11" data-date-format="dd-M-yyyy" style="width: 200px;" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </span></span>
                                                </div>
                                            </td>
                                        </tr>
                                        <!-- end if gender is female -->
                                    </tbody>
                                </table>
                            </div>
                            <div class="row box-header LeftMrgn">
                                <h5 class="box-title">
                                    Adherence</h5>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <%--left--%>
                                    <div class="row">
                                        <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                            <label>
                                                Number of Doses Missed:</label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                            <label id="lbldosmissed">
                                                Last Week:</label>
                                        </div>
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                            <div id="MissedLastWeek" style="display: inline">
                                                <input type="text" id="txtMissedLastWeek" name="MissedLastWeek" size="2" maxlength="4"
                                                    runat="server" style="width: 100px;" class="form-control" />
                                            </div>
                                        </div>
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                            <input type="checkbox" id="chMissedLastWeeknone" name="MissedLastWeeknone" size="2"
                                                runat="server" />
                                            <span class="smalllabel">None</span>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                            <label>
                                                Last Month:</label>
                                        </div>
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                            <div id="MissedLastMonth" style="display: inline">
                                                <input type="text" id="txtMissedLastMonth" name="MissedLastMonth" size="2" maxlength="4"
                                                    runat="server" style="width: 100px;" class="form-control" />
                                            </div>
                                        </div>
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                            <input type="checkbox" id="chMissedLastMonthnone" name="MissedLastMonthnone" runat="server" />
                                            <span class="smalllabel">None</span>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                            <label>
                                                DOT:</label></div>
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                            <asp:TextBox ID="txtNumDOTPerWeek" runat="server" MaxLength="4" Width="100" CssClass="form-control"></asp:TextBox></div>
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                            <span class="smalllabel">times/week</span></div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                            <label>
                                                Home Visits:</label></div>
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                            <asp:TextBox ID="txtNumHomeVisitsPerWeek" runat="server" MaxLength="4" Width="100"
                                                CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                            <span class="smalllabel">times/week</span>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                            <label>
                                                Support Group:</label></div>
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                            <span>
                                                <input type="checkbox" id="ckSupportGroup" name="SupportGroup" runat="server" style="vertical-align: top;
                                                    margin-left: 6%;" /></span>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                                            <label>
                                                Rx was interrupted(Unintentional):</label></div>
                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                            <input type="radio" id="rdoInterrupted" name="intStop" value="interrupted" runat="server"
                                                onmouseup="up(this);" onfocus="up(this);" onclick="down(this); toggle('interruptedDate'); hide('stopDate');" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div id="interruptedDate" style="display: none; margin-left: 20%;">
                                            <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                                <label>
                                                    Date:</label></div>
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                <div class="form-group">
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <input type="text" class="form-control pull-left" id="txtInterruptedDate" clientidmode="Static"
                                                            runat="server" maxlength="11" size="8" data-date-format="dd-M-yyyy" style="width: 110px;" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                <div style="width: 100%; margin: 0px auto;">
                                                    <div style="float: left">
                                                        <input type="text" id="TxtInterruptedNumDays" name="InterruptedNumDays" size="2"
                                                            class="form-control" style="width: 50px" maxlength="3" runat="server" />
                                                    </div>
                                                    <div style="float: left">
                                                        <span class="smalllabel"># of days</span></div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                                            <label>
                                                Rx was stopped (intentional):</label></div>
                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                            <input type="radio" id="rdostopped" name="intStop" runat="server" onfocus="up(this);"
                                                onmouseup="up(this);" onclick="down(this); toggle('stopDate'); hide('interruptedDate');"
                                                value="stopped" style="margin-left: 6.5%;" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div id="stopDate" style="display: none;">
                                            <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                                <label>
                                                    Date:</label></div>
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                <div class="form-group">
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <input type="text" class="form-control pull-left" id="txtstoppedDate" clientidmode="Static"
                                                            runat="server" data-date-format="dd-M-yyyy" maxlength="11" size="8" style="width: 110px;" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                <div style="width: 100%; margin: 0px auto;">
                                                    <div style="float: left">
                                                        <input type="text" id="txtstoppedNumDays" name="stoppedNumDays" size="2" class="form-control"
                                                            style="width: 50px" maxlength="3" runat="server" /></div>
                                                    <div style="float: left">
                                                        <span class="smalllabel"># of days</span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                                            <label>
                                                Patient reports taking herbal medications:</label></div>
                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                            <input type="checkbox" id="ckHerbalMeds" name="HerbalMeds" runat="server" style="vertical-align: top;" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <%--right--%>
                                    <div class="row">
                                        <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                            <label id="lblreasonmissed">
                                                Reason Missed:</label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-10 col-sm-12 col-xs-12 form-group">
                                            <div class="divborder" id="divReasonMissed">
                                                <div>
                                                    <asp:CheckBoxList ID="cblAdheranceMissedReason" RepeatLayout="Flow" Width="0%" runat="server"
                                                        Style="vertical-align: baseline;">
                                                    </asp:CheckBoxList>
                                                    <label style="font-weight: bold" for="otherAdheranceMissedReason">
                                                        <span id="otherAdheranceMissedReason" style="display: none">Specify:
                                                            <input id="txtotherAdheranceMissedReason" size="10" name="otherAdheranceMissedReason"
                                                                runat="server" /></span></label>
                                                </div>
                                                <div>
                                                    <table id="tblReasonMissed" runat="server" class="table-condensed">
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row box-header LeftMrgn">
                                <h5 class="box-title">
                                    Presenting Complaints</h5>
                            </div>
                            <div class="row">
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label>
                                        Presenting Complaints:</label>
                                </div>
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <input id="chkpresentingComplaintsNone" type="checkbox" checked="checked" value="None"
                                        name="presentingComplaintsNone" runat="server" style="vertical-align: top;" />
                                    <span>None</span><input id="chkpresentingComplaintsNonehidden" checked="checked"
                                        value="None" class="textstylehidden" type="checkbox" runat="server" />
                                </div>
                                <div class="col-md-8 col-sm-12 col-xs-12 form-group">
                                    <div id="presentingComplaintsShow" style="display: none" runat="server">
                                        <div class="checkbox checkbox-inline" id="divpreComplain">
                                            <asp:CheckBoxList ID="cblPresentingComplaints" RepeatLayout="Flow" runat="server">
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label>
                                        TB Screening:</label>
                                </div>
                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                    <input id="rdoTBScreenPerformed" onmouseup="up(this);" onfocus="up(this);" onclick=" down(this); toggle('TBSCreenSelected');"
                                        type="radio" name="Sym" runat="server" />
                                    <span id="Span1">Performed</span>
                                    <input id="rdoTBScreenNotDocumented" onmouseup="up(this);" onfocus="up(this);" onclick="down(this); hide('TBSCreenSelected');"
                                        type="radio" value="Not Documented" name="Sym" runat="server" />
                                    <span id="Span2">Not Documented</span>
                                </div>
                                <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                                    <div id="TBSCreenSelected" style="display: none">
                                        <div class="checkbox checkbox-inline" id="divTBScreen">
                                            <asp:CheckBoxList ID="cblTBScreen" RepeatLayout="Flow" runat="server">
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row box-header LeftMrgn">
                                <h5 class="box-title">
                                    Physical Exam</h5>
                            </div>
                            <div class="row">
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <div style="width: 100%; margin: 0px auto;">
                                        <div style="float: left" class="supMrgn">
                                            <label id="TempVal">
                                                Temp:</label></div>
                                        <div style="float: left">
                                            <asp:TextBox ID="txtphysTemp" runat="server" MaxLength="4" Width="5%" CssClass="form-control"
                                                Style="width: 70px;"></asp:TextBox>
                                        </div>
                                        <div style="float: left" class="supMrgn">
                                            <span class="smallerlabel">
                                                <label>
                                                    C</label></span></div>
                                    </div>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <div style="width: 100%; margin: 0px auto;">
                                        <div style="float: left" class="supMrgn">
                                            <label>
                                                RR:</label></div>
                                        <div style="float: left">
                                            <asp:TextBox ID="txtphysRR" runat="server" MaxLength="4" CssClass="form-control"
                                                Style="width: 70px;"></asp:TextBox>
                                        </div>
                                        <div style="float: left" class="supMrgn">
                                            <span class="smallerlabel">
                                                <label>
                                                    bpm</label></span></div>
                                    </div>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <div style="width: 100%; margin: 0px auto;">
                                        <div style="float: left" class="supMrgn">
                                            <label>
                                                HR:&nbsp;&nbsp;</label></div>
                                        <div style="float: left">
                                            <asp:TextBox ID="txtphysHR" runat="server" MaxLength="4" CssClass="form-control"
                                                Style="width: 70px;"></asp:TextBox>
                                        </div>
                                        <div style="float: left" class="supMrgn">
                                            <span class="smallerlabel">
                                                <label>
                                                    bpm</label></span></div>
                                    </div>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <div style="width: 100%; margin: 0px auto;">
                                        <div style="float: left" class="supMrgn">
                                            <label>
                                                BP:</label></div>
                                        <div style="float: left">
                                            <asp:TextBox ID="txtphysBPSystolic" runat="server" MaxLength="4" CssClass="form-control"
                                                Style="width: 70px;"></asp:TextBox>
                                        </div>
                                        <div style="float: left" class="supMrgn">
                                            <b>/</b></div>
                                        <div style="float: left">
                                            <asp:TextBox ID="txtphysBPDiastolic" runat="server" MaxLength="4" CssClass="form-control"
                                                Style="width: 70px;"></asp:TextBox>
                                        </div>
                                        <div style="float: left" class="supMrgn">
                                            <span class="smallerlabel">
                                                <label>
                                                    (mm/Hg)</label></span></div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <div style="width: 100%; margin: 0px auto;">
                                        <div style="float: left" class="supMrgn">
                                            <label id="Label1">
                                                HT:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label></div>
                                        <div style="float: left">
                                            <asp:TextBox ID="txtphysHeight" runat="server" MaxLength="4" CssClass="form-control"
                                                Style="width: 70px;"></asp:TextBox>
                                        </div>
                                        <div style="float: left" class="supMrgn">
                                            <span class="smallerlabel">
                                                <label>
                                                    cm</label></span></div>
                                    </div>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <div style="width: 100%; margin: 0px auto;">
                                        <div style="float: left" class="supMrgn">
                                            <label id="Label2">
                                                WT:</label></div>
                                        <div style="float: left">
                                            <asp:TextBox ID="txtphysWeight" runat="server" MaxLength="4" class="form-control"
                                                Style="width: 70px;"></asp:TextBox>
                                        </div>
                                        <div style="float: left" class="supMrgn">
                                            <span class="smallerlabel">
                                                <label>
                                                    kg</label></span></div>
                                    </div>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <div style="width: 100%; margin: 0px auto;">
                                        <div style="float: left" class="supMrgn">
                                            <label>
                                                BMI:</label></div>
                                        <div style="float: left">
                                            <input id="txtanotherbmi" size="6" name="anotherbmi" runat="server" readonly="readonly"
                                                class="form-control" style="width: 70px;" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <div style="width: 100%; margin: 0px auto;">
                                        <div style="float: left" class="supMrgn">
                                            <label>
                                                Pain:</label></div>
                                        <div style="float: left">
                                            <select id="ddlPain" name="pain" runat="server" class="form-control" style="width: 140px;">
                                                <option value="0" selected="selected">Select</option>
                                                <option value="1">1</option>
                                                <option value="2">2</option>
                                                <option value="3">3</option>
                                                <option value="4">4</option>
                                                <option value="5">5</option>
                                                <option value="6">6</option>
                                                <option value="7">7</option>
                                                <option value="8">8</option>
                                                <option value="9">9</option>
                                                <option value="10">10</option>
                                            </select></div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label>
                                        ARV Side Effects:</label>
                                </div>
                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                    <input id="rdoARVSideEffectsNone" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);hide('sideEffectsSelected');"
                                        type="radio" value="none" name="SideEffects" runat="server" checked />
                                    <span id="lblARVNone">None</span>
                                    <input id="rdoARVSideEffectsNotDocumented" onmouseup="up(this);" onfocus="up(this);"
                                        onclick="down(this);hide('sideEffectsSelected');" type="radio" value="Not Documented"
                                        name="SideEffects" runat="server" />
                                    <span id="lblARVNotdocumented">Not Documented</span>
                                    <input id="rdoARVSideEffectsYes" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);toggle('sideEffectsSelected');"
                                        type="radio" value="show" name="SideEffects" runat="server" />
                                    <span id="lblARVSideEffect">Enter ARV Side Effects</span>
                                </div>
                                <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                                    <div id="sideEffectsSelected" style="display: none">
                                        <div class="checkboxleft" id="divArvsideleft" nowrap="noWrap">
                                            <asp:CheckBoxList ID="cblARVSideEffectleft" RepeatLayout="Flow" Width="100%" runat="server">
                                            </asp:CheckBoxList>
                                        </div>
                                        <div class="checkboxright" id="divARVsideright" nowrap="noWrap">
                                            <asp:CheckBoxList ID="cblARVSideEffectright" RepeatLayout="Flow" Width="100%" runat="server">
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label>
                                        OIs or AIDS Defining Illnesses:</label>
                                </div>
                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                    <input id="rdoOIsAIDsIllnessNone" onmouseup="up(this);" onfocus="up(this);" onclick="down(this); hide('pultb'); hide('assocSelected'); "
                                        type="radio" value="none" name="assocNone" runat="server" />
                                    <span id="lblOIAIDNone">None</span>
                                    <input id="rdoOIsAIDsIllnessNotDocumented" onmouseup="up(this);" onfocus="up(this);"
                                        onclick="down(this); hide('pultb'); hide('assocSelected');" type="radio" value="Not Documented"
                                        name="assocNone" runat="server" />
                                    <span id="lblOIAIDNotdocumented">Not Documented</span>
                                    <input id="rdoOIsAIDsIllnessYes" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);toggle('assocSelected');"
                                        type="radio" value="show" name="assocNone" runat="server" />
                                    <span id="lblOIAIDillness">Enter Illnesses</span>
                                </div>
                                <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                                    <div id="assocSelected" style="display: none">
                                        <table width="100%" class="table-condensed">
                                            <tbody>
                                                <tr>
                                                    <td style="width: 49%; vertical-align: top;">
                                                        <div class="checkboxright" id="diventerillnessleft" style="width: 96%">
                                                            <table id="tblOIsAIDsleft" class="table-condensed" width="100%" runat="server">
                                                            </table>
                                                        </div>
                                                    </td>
                                                    <td style="width: 49%; vertical-align: top;">
                                                        <div class="checkboxleft" id="diventerillnessright" style="width: 96%">
                                                            <table id="tblOIsAIDsright" class="table-condensed" width="100%" border="0" runat="server">
                                                            </table>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div class="row box-header LeftMrgn">
                                <h5 class="box-title">
                                    Assessment</h5>
                            </div>
                            <div class="row">
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label id="lblassessment">
                                        Clinical Assessment:</label>
                                </div>
                                <div class="col-md-8 col-sm-12 col-xs-12 form-group">
                                    <div id="assessment" class="checkbox-inline">
                                        <span>
                                            <asp:CheckBoxList ID="cblAssessment" runat="server" RepeatLayout="Table" RepeatDirection="Horizontal"
                                                Width="150%" RepeatColumns="4">
                                            </asp:CheckBoxList>
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <label id="lblWHO">
                                        WHO Stage:</label>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <asp:DropDownList ID="ddlWHOStage" runat="server" CssClass="form-control" Style="width: 140px;">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <label id="lblWAB">
                                        WAB Stage:</label>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <asp:DropDownList ID="ddlphysWABStage" runat="server" CssClass="form-control" Style="width: 140px;">
                                        <asp:ListItem Value="0" Selected="true">Select</asp:ListItem>
                                        <asp:ListItem Value="84">Working</asp:ListItem>
                                        <asp:ListItem Value="85">Ambulatory</asp:ListItem>
                                        <asp:ListItem Value="86">Bedridden</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row box-header LeftMrgn">
                                <h5 class="box-title">
                                    Plan</h5>
                            </div>
                            <div class="row">
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <label id="lblarvplan">
                                        ARV Therapy:</label>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <select id="lstclinPlanFU" onchange="specifyChangeStop(this.id);" name="clinPlanFU"
                                        runat="server" class="form-control" style="width: 300px;">
                                        <option value="0" selected="selected">Select</option>
                                        <option value="94">Treatment not indicated now</option>
                                        <option value="95">Continue current treatment</option>
                                        <option value="96">Restart treatment</option>
                                        <option value="97">Start new treatment</option>
                                        <option value="98">Change regimen</option>
                                        <option value="99">Stop treatment</option>
                                    </select>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                    <div id="arvTherapyChange" style="display: none">
                                        <div class="row">
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                <label class="required">
                                                    Change Regimen Reason:</label>
                                            </div>
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                <asp:DropDownList ID="ddlArvTherapyChangeCode" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                            <div id="otherarvTherapyChangeCode" style="display: none">
                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                    <label class="required" for="arvTherapyChangeCodeOtherName">
                                                        Specify:</label>
                                                </div>
                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                    <input id="txtarvTherapyChangeCodeOtherName" maxlength="20" size="10" class="form-control"
                                                        name="arvTherapyChangeCodeOtherName" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                    <div id="arvTherapyStop" style="display: none">
                                        <div class="row">
                                            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                <label id="lblrARTdate" class="required">
                                                    Date ART Ended</label>
                                            </div>
                                            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                <div class="form-group">
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <input type="text" class="form-control pull-left" id="txtARTEndeddate" clientidmode="Static"
                                                            runat="server" maxlength="11" size="10" data-date-format="dd-M-yyyy" style="width: 110px;" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                <label class="required">
                                                    Stop Regimen Reason:</label>
                                            </div>
                                            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                <asp:DropDownList ID="ddlArvTherapyStopCode" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                            <div id="otherarvTherapyStopCode" style="display: none">
                                                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                                    <label class="required right15" for="arvTherapyStopCodeOtherName">
                                                        Specify:</label>
                                                </div>
                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                    <input id="txtarvTherapyStopCodeOtherName" maxlength="20" size="10" name="arvTherapyStopCodeOtherName"
                                                        runat="server" class="form-control" style="width: 250px;" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                    <asp:Panel ID="pnlPanelNotes" runat="server" Visible="false">
                                        <tr>
                                            <td class="border pad5 whitebg formcenter" id="PlanNotesID" runat="server" colspan="2">
                                                <label id="lblNotes" style="vertical-align: top">
                                                    Notes</label>
                                                <textarea id="MulttxtclinPlanNotes" name="clinPlanNotes" rows="4" cols="160" runat="server"></textarea>
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                </div>
                            </div>
                            <div class="row box-header LeftMrgn">
                                <h5 class="box-title">
                                    Clinical Notes</h5>
                            </div>
                            <div class="row">
                                <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                    <asp:TextBox ID="txtClinicalNotes" TextMode="MultiLine" Width="99%" runat="server"
                                        CssClass="form-control" Style="resize: none;"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row box-header LeftMrgn">
                                <h5 class="box-title">
                                    Appointment and Signature</h5>
                            </div>
                            <div class="row">
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <label>
                                        When is the patient's next appointment?</label>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <select id="lstappPeriod" name="appPeriod" runat="server" onchange="addDays()" class="form-control"
                                        style="width: 110px;">
                                        <option value="0" selected="selected">Select</option>
                                        <option value="7">1 week</option>
                                        <option value="14">2 weeks</option>
                                        <option value="30">4 weeks</option>
                                        <option value="60">2 months</option>
                                        <option value="90">3 months</option>
                                        <option value="180">6 months</option>
                                    </select>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <label>
                                        Appointment Reason:</label>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <asp:DropDownList ID="ddlAppReason" runat="server" CssClass="form-control" Width="200">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <label>
                                        Specify Date:</label>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <div class="form-group">
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <input type="text" class="form-control pull-left" id="txtappDate" clientidmode="Static"
                                                runat="server" maxlength="11" size="8" data-date-format="dd-M-yyyy" style="width: 110px;" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <label>
                                        Signature:</label>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <asp:DropDownList ID="ddlCounsellorSignature" runat="server" CssClass="form-control"
                                        Width="300">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                    <asp:Panel ID="pnlCustomList" Visible="false" runat="server" Height="100%" Width="100%"
                                        Wrap="true">
                                    </asp:Panel>
                                </div>
                            </div>
                            <div class="row" align="center">
                                <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                    <asp:Button ID="btnsave" Text="Save" runat="server" OnClick="btnsave_Click" CssClass="btn btn-primary"
                                        Width="8%" Height="30px" Style="text-align: left;" />
                                    <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; margin-right: 2%;
                                        vertical-align: sub; color: #fff;">
                                    </label>
                                    <asp:Button ID="btndataquality" Text="Data Quality check" runat="server" OnClick="btndataquality_Click"
                                        CssClass="btn btn-primary" Width="17%" Height="30px" Style="text-align: left;" />
                                    <label class="glyphicon glyphicon-floppy-saved" style="margin-left: -3%; margin-right: 2%;
                                        vertical-align: sub; color: #fff;">
                                    </label>
                                    <asp:Button ID="btnclose" Text="Close" runat="server" OnClick="btnclose_Click" CssClass="btn btn-primary"
                                        Width="8%" Height="30px" Style="text-align: left;" />
                                    <label class="glyphicon glyphicon-remove-circle" style="margin-left: -3%; margin-right: 2%;
                                        vertical-align: sub; color: #fff;">
                                    </label>
                                    <asp:Button ID="btnOk" Text="OK" CssClass="btn btn-primary" runat="server" OnClick="theBtn_Click"
                                        Width="8%" Height="30px" Style="text-align: left;" />
                                    <label id="lblOKClick" class="glyphicon glyphicon-ok-sign" runat="server" style="margin-left: -4%;
                                        margin-right: 3%; vertical-align: middle; color: #fff; margin-top: .6%; visibility: hidden;">
                                    </label>
                                    <asp:Button ID="theBtnDQ" Text="DQ" CssClass="btn btn-primary" runat="server" OnClick="theBtnDQ_Click"
                                        Width="8%" Height="30px" Style="text-align: left;" />
                                    <label id="lblDQClick" class="glyphicon glyphicon-random" runat="server" style="margin-left: -4%;
                                        margin-right: 3%; vertical-align: middle; color: #fff; margin-top: .6%; visibility: hidden;">
                                    </label>
                                    <asp:Button ID="btnPrint" Text="Print" runat="server" OnClientClick="WindowPrint()"
                                        CssClass="btn btn-primary" Width="8%" Height="30px" Style="text-align: left;" />
                                    <label class="glyphicon glyphicon-print" style="margin-left: -3%; vertical-align: sub;
                                        color: #fff;">
                                    </label>
                                </div>
                            </div>
                            <%--Main end--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
