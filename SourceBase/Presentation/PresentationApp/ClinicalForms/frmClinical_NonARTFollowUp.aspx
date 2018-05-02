<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    Inherits="frmClinical_NonARTFollowUp" Title="Untitled Page" CodeBehind="frmClinical_NonARTFollowUp.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <script language="javascript" type="text/javascript">
        //For appointment
        function WindowPrint() {
            window.print();
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
        function addDays(id) {
            if (id >= 0) {
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
                    document.getElementById('<%=txtSpecifyDate.ClientID%>').value = "";
                    document.getElementById('<%=ddlAppReason.ClientID%>').value = 0;
                    // document.getElementById('<%=ddlPharmOrderedbyName.ClientID%>').value=0;
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
                    document.getElementById('<%=txtSpecifyDate.ClientID%>').value = dt2;
                    if (txtdate == "") {
                        document.getElementById('<%=txtSpecifyDate.ClientID%>').value = "";
                        document.getElementById('<%=ddlAppReason.ClientID%>').value = 0;
                        // document.getElementById('<%=lstappPeriod.ClientID%>').value = 0;
                    }
                }
                return;
            }
        }
        //code Name   
        function SendCD4() {
            var id = document.getElementById('<%=txtvisitDate.ClientID%>').value;
            if (id.length <= 0) {
                //			alert('Please enter visit Date');
                return true;
            }
            else {
                CallServer(id, "This is context from client");
                return true;
            }
        }
        function ReceiveServerData(args, context) {
            var a = args;
            var temp = new Array(); temp = a.split('zz');
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

        function parseXml(xml, flag) {
            var oXml = xml;
            var xml = $.parseXML(xml);
            $xml = $(xml);
            if (flag == 1) {
                //CD4 Values
                document.getElementById('<%=txtTestResults.ClientID%>').value = "";
                document.getElementById('<%=txtTestResultsDate.ClientID%>').value = "";
                if ($xml.find("ExistCD4").text() != 0) {
                    document.getElementById('<%=txtTestResults.ClientID%>').value = $xml.find("CD4TestResult").text()
                    formattedDate = $xml.find("LastCD4Date").text()
                    var yr = formattedDate.substr(7, 4);
                    var mm = formattedDate.substr(3, 3);
                    var dd = formattedDate.substr(0, 2);
                    formattedDate = dd + "-" + mm + "-" + yr;
                    document.getElementById('<%=txtTestResultsDate.ClientID%>').value = formattedDate;
                }
            }
            else if (flag == 2) {
                //Height 
                document.getElementById('<%=txtphysHeight.ClientID%>').value = "";
                if ($xml.find("Table").find("Column2").text() == 18) {
                    document.getElementById('<%=txtphysHeight.ClientID%>').value = $xml.find("Table").find("Column1").text();
                }
                if ($xml.find("Table1").find("Exist").text() == 1) {
                    var ans = true;
                    alert('You Cannot Create Pharmacy and Non-ART form for a Patient on same date');
                    if (ans == true) {
                        document.getElementById('<%=txtvisitDate.ClientID%>').value = "";
                        document.getElementById('<%=txtvisitDate.ClientID%>').focus();
                    }
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
                        document.getElementById('<%=txtLMP.ClientID%>').value = ""
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
    </script>
    <script type="text/javascript">
        function RegisterJQuery() {
            $('#txtvisitDate').datepicker({ autoclose: true });
            $('#txtTestResultsDate').datepicker({ autoclose: true });
            $('#txtLMP').datepicker({ autoclose: true });
            $('#txtEDDDate').datepicker({ autoclose: true });
            $('#txtDeliDate').datepicker({ autoclose: true });
            $('#txtSpecifyDate').datepicker({ autoclose: true });
            $('#txtpharmOrderedbyDate').datepicker({ autoclose: true });
            $('#txtpharmReportedbyDate').datepicker({ autoclose: true }); 
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
        <h3 class="box-title">Non-ART Follow-Up</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
         <div class="row">
         <br />
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label id="lblvdate" class="required">Visit Date:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtvisitDate" clientidmode="Static"
                                                        runat="server" data-date-format="dd-M-yyyy" style="width:110px;"/>
                                                        <input id="hdnVisitIDIE" type="hidden" value="0" runat="server" />
                                                </div>
                                            </div>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label>Last CD4 Count:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <div style="width: 100%; margin: 0px auto;">
<div style="float: left">
<asp:TextBox ID="txtTestResults" Width="50px" MaxLength="4" ReadOnly="true" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div style="float: left;margin-top:5px;"><span class="smallerlabel">c/mm<sup>3</sup></span></div>
</div>
             
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label>Date:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtTestResultsDate" clientidmode="Static"
                                                        runat="server" data-date-format="dd-M-yyyy" style="width:110px;"/>                                                         
                                                </div>
                                            </div>
             
             </div>
                     
    </div>
    <div class="row">
    <table width="100%" class="table-condensed">
                <tbody>
                    
                    <!-- if gender is female then-->
                    <tr>
                        <td id="tdPregnant" class="form" runat="server" colspan="2" align="left">
                        <div class="row">
                        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                        <label id="LMPdate" for="LMP">LMP:</label>
                        </div>
                         <div class="col-md-2 col-sm-12 col-xs-12 form-group">                         
                          <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtLMP" clientidmode="Static"
                                                        runat="server" data-date-format="dd-M-yyyy" style="width:110px;" maxlength="11" size="8" name="LMP"/>
                                                </div>
                                            </div>
                         </div>
                          <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                          <span id="rdopregnantyesno" style="display: inline">
                                <label id="PregnantVal">
                                    Pregnant:
                                </label>
                                <input id="rdopregnantYes" onmouseup="up(this);" onfocus="up(this);" onclick="down(this); show('spanEDD');"
                                    type="radio" value="Y" name="pregnant" runat="server" />
                                <label>
                                    Yes</label>
                                <input id="rdopregnantNo" onmouseup="up(this);" onfocus="up(this);" onclick="down(this); hide('spanEDD');"
                                    type="radio" value="N" name="pregnant" runat="server" /><label>
                                        No</label></span> 
                          </div>
                           <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                            <span id="spanEDD" style="display: none">
                            <div style="width: 100%; margin: 0px auto;">
                            <div style="float: left; margin-top:5px;"><label id="lblEDD" for="EDD">EDD:</label></div>
                            <div style="float: left;">
                            <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtEDDDate" clientidmode="Static"
                                                        runat="server" maxlength="11" size="11" data-date-format="dd-M-yyyy" style="width:110px;"/>
                                                </div>
                                            </div>

                           
                            </div>
                            </div>
                                            
                                            
                                           
                                            </span>
                           </div>
                        </div>
                             <div class="row">
                             <div id="spdelivery" style="display: none">
                             <div class="col-md-3 col-sm-12 col-xs-12 form-group" style="margin-left:10px;">
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
                                <label id="lblDelDate" for="DelDate">Delivered Date:</label>
                                </div>
                                     <div class="col-md-3 col-sm-12 col-xs-12 form-group"> 
                                     <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtDeliDate" clientidmode="Static"
                                                        runat="server" maxlength="11" size="11" data-date-format="dd-M-yyyy" style="width:200px;"/>
                                                </div>
                                            </div>

                                    
                                     </div>
                                    
                                    
                                    </span>
                            </div>
                             </div>
                           
                           
                           
                            
                        </td>
                    </tr>
                    <!-- end if gender is female -->
                </tbody>
            </table>
    </div>
    <div class="row box-header LeftMrgn">
             <h5 class="box-title col-xs-12 form-group">
            Presenting Complaints</h5>
            </div>
            <div class="row">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <label>Presenting Complaints:</label>
             </div>
            <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <input id="chkpresentingComplaintsNone" type="checkbox" checked="checked" value="None"
                                name="presentingComplaintsNone" runat="server" />
                            <span>None</span>
            </div>  
              <div class="col-md-8 col-sm-12 col-xs-12 form-group">
            <div>
                                <input id="chkpresentingComplaintsNonehidden" checked="checked" value="None" class="textstylehidden"
                                    type="checkbox" runat="server" />
                                <div id="presentingComplaintsShow" style="display: none" runat="server">
                                    <div class="checkbox checkbox-inline">
                                        <asp:CheckBoxList ID="cblPresentingComplaints" runat="server" RepeatLayout="Flow"
                                            Width="100%">
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>
             </div>          
             </div>
               <div class="row">
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label>TB Screening:</label>
             </div>
              <div class="col-md-3 col-sm-12 col-xs-12 form-group">
              <input id="rdoPerformed" onmouseup="up(this);" onfocus="up(this);" type="radio" value="show"
                                name="Symptom" runat="server" />
                            <span>Performed</span>
                            <input id="rdoNotDocumented" onmouseup="up(this);" onfocus="up(this);" type="radio"
                                value="Not Documented" name="Symptom" runat="server" />
                            <span>Not Documented</span>
             </div>
              <div class="col-md-6 col-sm-12 col-xs-12 form-group">
               <div class="checkbox checkbox-inline" id="TBSymptom" style="display: none">
                                <asp:CheckBoxList ID="cblTBScreen" RepeatLayout="Flow" Width="50%" runat="server">
                                </asp:CheckBoxList>
                            </div>
             </div>
             </div>
             <div class="row box-header LeftMrgn">
             <h5 class="box-title col-xs-12 form-group">
            Physical Exam</h5>
            </div>
            <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
 <div style="width: 100%; margin: 0px auto;">
<div style="float: left" class="supMrgn"><label id="TempVal">Temp:</label></div>
<div style="float: left">
<asp:TextBox ID="txtphysTemp" runat="server" MaxLength="4" Width="5%" CssClass="form-control" style="width:70px;"></asp:TextBox>
</div>
<div style="float: left" class="supMrgn"><span class="smallerlabel"> <label>C</label></span></div>
</div>
 </div>
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
 <div style="width: 100%; margin: 0px auto;">
<div style="float: left" class="supMrgn"><label>RR:</label></div>
<div style="float: left">
<asp:TextBox ID="txtphysRR" runat="server" MaxLength="4"  CssClass="form-control" style="width:70px;"></asp:TextBox>
</div>
<div style="float: left" class="supMrgn"><span class="smallerlabel"> <label>bpm</label></span></div>
</div>
 </div>
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
 <div style="width: 100%; margin: 0px auto;">
<div style="float: left" class="supMrgn"> <label>HR:&nbsp;&nbsp;</label></div>
<div style="float: left">

<asp:TextBox ID="txtphysHR" runat="server" MaxLength="4" CssClass="form-control" style="width:70px;"></asp:TextBox>
</div>
<div style="float: left" class="supMrgn"> <span class="smallerlabel"><label>bpm</label></span></div>
</div>
 </div>
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
  <div style="width: 100%; margin: 0px auto;">
<div style="float: left" class="supMrgn"><label>BP:</label></div>
<div style="float: left">
<asp:TextBox ID="txtphysBPSystolic" runat="server" MaxLength="4" CssClass="form-control" style="width:70px;"></asp:TextBox>
</div>
<div style="float: left" class="supMrgn"><b>/</b></div>
<div style="float: left">
<asp:TextBox ID="txtphysBPDiastolic" runat="server" MaxLength="4" CssClass="form-control" style="width:70px;"></asp:TextBox>
</div>
<div style="float: left" class="supMrgn"><span class="smallerlabel"><label>(mm/Hg)</label></span></div>
</div>
 </div>
 </div>
 <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
    <div style="width: 100%; margin: 0px auto;">
<div style="float: left"  class="supMrgn"><label id="Label1">HT:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label></div>
<div style="float: left">
<asp:TextBox ID="txtphysHeight" runat="server" MaxLength="4" CssClass="form-control" style="width:70px;"></asp:TextBox>
</div>
<div style="float: left" class="supMrgn"><span class="smallerlabel"><label>cm</label></span></div>
</div>
    </div>
    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
    <div style="width: 100%; margin: 0px auto;">
<div style="float: left" class="supMrgn"><label id="Label1">WT:</label></div>
<div style="float: left">
<asp:TextBox ID="txtphysWeight" runat="server" MaxLength="4" class="form-control" style="width:70px;"></asp:TextBox>
</div>
<div style="float: left" class="supMrgn"><span class="smallerlabel"><label>kg</label></span></div>
</div>
    </div>
    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
    <div style="width: 100%; margin: 0px auto;">
<div style="float: left" class="supMrgn"> <label>BMI:</label></div>
<div style="float: left">
<input id="txtanotherbmi" size="6" name="anotherbmi" runat="server" readonly="readonly" class="form-control" style="width:70px;" />
</div>
</div>
    </div>
    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
    <div style="width: 100%; margin: 0px auto;">
<div style="float: left" class="supMrgn"><label>Pain:</label></div>
<div style="float: left">
<select id="ddlPain" name="pain" runat="server" class="form-control" style="width:140px;">
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
 <div class="row box-header LeftMrgn">
             <h5 class="box-title col-xs-12 form-group">
            OIs or AIDS Defining Illnesses</h5>
            </div>
            <div class="row">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label>OIs or AIDS Defining Illnesses:</label>
             </div>
            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <div><input id="rdoHIVassocNone" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);  hide('assocSelected'); "
                                    type="radio" checked value="none" name="assocNone" runat="server" />
                                <span id="lblHIVassocNone">None</span>
                                <input id="PrevHIVassocNotDocumented" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);  hide('assocSelected');"
                                    type="radio" value="Not Documented" name="assocNone" runat="server" />
                                <span id="lblHIVassocNotdocumented">Not Documented</span>
                                <input id="rdoHIVassociate" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);toggle('assocSelected');"
                                    type="radio" value="show" name="assocNone" runat="server" />
                                <span id="lblHIVassociate">Enter Illnesses</span></div>
            </div> 
            <div class="col-md-5 col-sm-12 col-xs-12 form-group">
            <div id="assocSelected" style="display: None">
                                <div class="checkbox checkbox-inline chekcboxleft" style="width: 45%; vertical-align:top;">
                                    <table id="tblOIsAIDsleft" class="table-condensed" runat="server">
                                    </table>
                                </div>
                                <div class="checkbox checkbox-inline checkboxright" style="width: 45%; vertical-align:top;">
                                    <table id="tblOIsAIDsright" class="table-condensed" runat="server">
                                    </table>
                                </div>
                            </div>
            </div>             
             </div>
             <div class="row box-header LeftMrgn">
             <h5 class="box-title col-xs-12 form-group">
            Assessment</h5>
            </div>
            <div class="row">
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label id="AssessmentVal">Clinical Assessment:</label>
             </div>
            <div class="col-md-9 col-sm-12 col-xs-12 form-group">
            <div class="checkbox-inline" align="left">
                                            <asp:CheckBoxList ID="chkAssessment" runat="server" RepeatLayout="Table" RepeatColumns="4"
                                                RepeatDirection="Horizontal" Width="140%">
                                            </asp:CheckBoxList>
                                        </div>
            </div>            
             </div>
              <div class="row">
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label id="ARTAssessmentVal">ART Assessment:</label>
             </div>
            <div class="col-md-9 col-sm-12 col-xs-12 form-group">
            <input id="rdoartAssessmentID" type="radio" value="" name="artAssessmentID" runat="server"
                                onmouseup="up(this);" onfocus="up(this);" onclick="down(this);" /><span>Re-evaluate</span>
                            <input id="rdoartAssessmentID2" type="radio" value="" name="artAssessmentID" runat="server"
                                onmouseup="up(this);" onfocus="up(this);" onclick="down(this);" /><span>Prepare for
                                    ART</span>
            </div>            
             </div>
             <div class="row">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
              <label id="WHOSt">WHO Stage:</label>
             </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <asp:DropDownList ID="ddlphysWHOStage" runat="server" CssClass="form-control" Style="width:140px;"></asp:DropDownList>
            </div>  
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
              <label id="WABSt">WAB Stage:</label>
             </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <asp:DropDownList ID="ddlphysWABStage" runat="server" CssClass="form-control" Style="width:140px;">
                                <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                                <asp:ListItem Value="84">Working</asp:ListItem>
                                <asp:ListItem Value="85">Ambulatory</asp:ListItem>
                                <asp:ListItem Value="86">Bedridden</asp:ListItem>
                            </asp:DropDownList>
            </div>              
             </div>
             <div class="row box-header LeftMrgn">
             <h5 class="box-title col-xs-12 form-group">
            OI Prophylaxis</h5>
            </div>
            <div class="row">
             <div class="col-md-2 col-sm-12 col-xs-12 form-group"> 
              <label>Sulfa/TMX:</label>            
             </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            <div style="width: 100%; margin: 0px auto;">
<div style="float: left;margin-top:5px;"> <span>Dose:</span></div>
<div style="float: left">
<select id="lstpharmSulfaTMXDose" name="pharmSulfaTMXDose" runat="server" class="form-control" style="width:140px;">
                                <option value="" selected="selected">Select</option>
                                <option value="108">80/400mg</option>
                                <option value="109">160/800mg</option>
                                <option value="134">40/200mg/5ml</option>
                            </select></div>
</div>

            
                            
            </div>   
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">    
            <div style="width: 100%; margin: 0px auto;">
<div style="float: left;margin-top:5px;"><span>Frequency:</span></div>
<div style="float: left"><select id="lstpharmSulfaTMXFreq" name="pharmSulfaTMXFreq" runat="server" class="form-control" style="width:100px;">
                                <option value="" selected="selected">Select</option>
                                <option value="1">OD</option>
                                <option value="2">BD</option>
                                <option value="6">QID</option>
                            </select></div>

</div>         
             </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            <div style="width: 100%; margin: 0px auto;">
<div style="float: left;margin-top:5px;"><span>Duration:</span></div>
<div style="float: left">
<input id="txtpharmSulfaTMXDuration" maxlength="5" size="2" name="pharmSulfaTMXDuration" runat="server" class="form-control" style="width:70px;"/>
</div>

</div>  
            </div>   
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">   
            <div style="width: 100%; margin: 0px auto;">
<div style="float: left;margin-top:5px;"><span>Qty. Prescribed:</span></div>
<div style="float: left"><input id="txtpharmSulfaTMXQty" maxlength="5" size="2" name="pharmSulfaTMXQty" runat="server" class="form-control" style="width:70px;"/></div>
</div>            
             </div>

            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            <div style="width: 100%; margin: 0px auto;">
<div style="float: left;margin-top:5px;"><span>Qty. Dispensed:</span></div>
<div style="float: left">
<input id="txtpharmSulfaTMXDispensed" onblur="dispense(this.id);" maxlength="5" size="2" name="pharmSulfaTMXDispensed" runat="server" class="form-control" style="width:70px;"/></div>
</div>  
            </div>            
             </div>
               <div class="row">
               <div class="col-md-2 col-sm-12 col-xs-12 form-group"> 
             <label>Fluconazole:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group"> 
               <div style="width: 100%; margin: 0px auto;">
<div style="float: left;margin-top:5px;"><span>Dose:</span></div>
<div style="float: left">
<select id="lstpharmFluconazoleDose" name="pharmFluconazoleDose" runat="server" class="form-control" style="width:140px;">
                                <option value="" selected="selected">Select</option>
                                <option value="7">200 mg</option>
                                <option value="8">400 mg</option>
                                <option value="26">800 mg</option>
                            </select>
</div>
</div>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group"> 
                <div style="width: 100%; margin: 0px auto;">
<div style="float: left;margin-top:5px;"><span>Frequency:</span></div>
<div style="float: left"><select id="lstpharmFluconazoleFreq" name="pharmFluconazoleFreq" runat="server" class="form-control" style="width:100px;">
                                <option value="" selected="selected">Select</option>
                                <option value="1">OD</option>
                                <option value="2">BD</option>
                            </select></div>
</div>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group"> 
                <div style="width: 100%; margin: 0px auto;">
<div style="float: left;margin-top:5px;"><span>Duration:</span></div>
<div style="float: left"><input id="txtpharmFluconazoleDuration" maxlength="5" size="2" name="pharmFluconazoleDuration" runat="server" class="form-control" style="width:70px;" /></div>
</div>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group"> 
                <div style="width: 100%; margin: 0px auto;">
<div style="float: left;margin-top:5px;"><span>Qty. Prescribed:</span></div>
<div style="float: left"><input id="txtpharmFluconazoleQty" maxlength="5" size="2" name="pharmFluconazoleQty" runat="server" class="form-control" style="width:70px;"/></div>
</div>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group"> 
                <div style="width: 100%; margin: 0px auto;">
<div style="float: left;margin-top:5px;"><span>Qty. Dispensed:</span></div>
<div style="float: left"><input id="txtpharmFluconazoleDispensed" onblur="dispense(this.id);" maxlength="5" size="2" name="pharmFluconazoleDispensed" runat="server" class="form-control" style="width:70px;"/></div>
</div>
             </div>
               </div>
               <div class="row" align="center">
             <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <div align="center" id="divAddOI" runat="server">
                <asp:Button ID="OtherMedication" runat="server" Text="OI Treatment and Other Medications"
                    CssClass="btn btn-primary" Height="30" Width="28%" Style="text-align: left;"
                    OnClick="OtherMedication_Click" />
                <label class="glyphicon glyphicon-bed" style="margin-left: -3%; margin-right: 2%;
                    vertical-align: sub; color: #fff;">
                </label>
            </div>
             </div>
                    
             </div>
             <div class="row">
             <div class="col-md-12 col-sm-12 col-xs-12 form-group">
              <asp:Panel ID="PnlAddOtherMedication" runat="server" Width="100%" Wrap="true" HorizontalAlign="Left">
            </asp:Panel>
             </div>
                     
             </div>
             <div class="row box-header LeftMrgn">
             <h5 class="box-title col-xs-12 form-group">
            Clinical Notes</h5>
            </div>
             <div class="row">
             <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtClinicalNotes" CssClass="form-control" TextMode="MultiLine" Style="resize:none;" Width="99%" runat="server"></asp:TextBox>
             </div>
             </div>
             <div class="row box-header LeftMrgn">
             <h5 class="box-title col-xs-12 form-group">
            Appointment and Signature</h5>
            </div>
            <div class="row">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label>When is the patient's next appointment?</label>
             </div>
            <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <select id="lstappPeriod" name="appPeriod" runat="server" onchange="addDays(0)" class="form-control" style="width:105px;">
                                <option value="0" selected="selected">Select</option>
                                <option value="7">1 week</option>
                                <option value="14">2 weeks</option>
                                <option value="30">4 weeks</option>
                                <option value="60">2 months</option>
                                <option value="90">3 months</option>
                                <option value="180">6 months</option>
                            </select>
            </div> 
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label>Specify Date:</label>
            </div>  
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtSpecifyDate" clientidmode="Static"
                                                        runat="server" maxlength="11" size="10"  data-date-format="dd-M-yyyy" style="width:110px;"/>
                                                </div>
                                            </div>

             
            </div> 
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label>Appointment Reason:</label>
            </div>  
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlAppReason" runat="server" CssClass="form-control" Style="width:170px;"></asp:DropDownList>
            </div>           
             </div>
             <div class="row">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label id="OrderedBy">Ordered by:</label>
             </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <asp:DropDownList ID="ddlPharmOrderedbyName" runat="Server" CssClass="form-control" Width="300"></asp:DropDownList>
            </div>     
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label id="OrderedByDate" for="pharmOrderedbyDate">Ordered By Date:</label>
             </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtpharmOrderedbyDate" clientidmode="Static"
                                                        runat="server" maxlength="11" size="11" data-date-format="dd-M-yyyy" style="width:110px;"/>
                                                </div>
                                            </div>

            
            </div>            
             </div>
             <div class="row">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label id="DispensedBy">Dispensed by:</label>
             </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <asp:DropDownList ID="ddlPharmReportedbyName" runat="server" CssClass="form-control" Width="300"></asp:DropDownList>
            </div>     
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label id="DispensedByDate" for="pharmReportedbyDate">Dispensed by Date:</label>
             </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtpharmReportedbyDate" clientidmode="Static"
                                                        runat="server" maxlength="11" size="11" data-date-format="dd-M-yyyy" style="width:110px;"/>
                                                </div>
                                            </div>
            
            </div>            
             </div>
             <div class="row">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label id="Signature">Signature:</label>
             </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <select id="ddlPharmSignature" onchange="specifyChangeStop(this.id);" name="Signature"
                                runat="server" class="form-control" Style="width:200px;">
                                <option value="0" selected="selected">Select</option>
                                <option value="1">No Signature</option>
                                <option value="2">Patient's Signature</option>
                                <option value="3">Adherance counsellor signature</option>
                            </select>
            </div>
             <div id="Adherance_counsellor_signature" style="display: none">

             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label id="CounSign" class="required margin20" visible="true">Specify Counselor Name:</label>
             </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <asp:DropDownList ID="ddlCounselerName" runat="server" CssClass="form-control" Width="280"></asp:DropDownList>
            </div> </div>
                            <asp:Label ID="Label2" runat="server" Visible="false" Text="Label"></asp:Label>
                                 
                     
             </div>
             <div class="row" align="center">
             <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <asp:Panel ID="pnlCustomList" Visible="false" runat="server" Height="100%" Width="100%"
                                Wrap="true">
                            </asp:Panel>
             </div>
             </div>
             <div class="row" align="center">
             <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <asp:Button ID="btnSave" Text="Save" runat="server" OnClick="btnSave_Click" CssClass="btn btn-primary"
                                Width="8%" Height="30px" Style="text-align: left;" />
                            <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; margin-right: 2%;
                                vertical-align: sub; color: #fff;">
                            </label>
                            <asp:Button ID="btnQualityCheck" Text="Data Quality check" runat="server" OnClick="btnQualityCheck_Click"
                                CssClass="btn btn-primary" Width="17%" Height="30px" Style="text-align: left;" />
                            <label class="glyphicon glyphicon-floppy-saved" style="margin-left: -3%; margin-right: 2%;
                                vertical-align: sub; color: #fff;">
                            </label>
                            <asp:Button ID="btnBack" Text="Close" runat="server" OnClick="btnBack_Click" CssClass="btn btn-primary"
                                Width="8%" Height="30px" Style="text-align: left;" />
                            <label class="glyphicon glyphicon-remove-circle" style="margin-left: -3%; margin-right: 2%;
                                vertical-align: sub; color: #fff;">
                            </label>
                            <asp:Button ID="theBtn1" Text="Yes" CssClass="textstylehidden" runat="server" OnClick="theBtn1_Click"
                                Width="8%" Height="30px" Style="text-align: left;" />
                            <label id="lblOKClick" class="glyphicon glyphicon-ok-sign" runat="server" style="margin-left: -4%;
                                margin-right: 3%; vertical-align: middle; color: #fff; margin-top: .6%; visibility: hidden;">
                            </label>
                            <asp:Button ID="theBtn" Text="OK" CssClass="textstylehidden" runat="server" OnClick="theBtn_Click"
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
          <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>
     </div>
     </div>

    <div class="container-fluid" style="padding: 8px;">
        
        
        
        
        
        
        
        <script language="javascript" type="text/javascript">
            function GetControl() {
                document.forms[0].submit();
            } 
        </script>
        
        
    </div>
</asp:Content>
