<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    Inherits="frmClinical_InitialEvaluation" MaintainScrollPositionOnPostback="true"
    Title="Untitled Page" CodeBehind="frmClinical_InitialEvaluation.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <script language="javascript" type="text/javascript">
        function addDays() {
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
                document.getElementById('<%=ddappReason.ClientID%>').value = 0;
                document.getElementById('<%=ddinterviewer.ClientID%>').value = 0;
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
            document.getElementById('divwhoto').className = '';
            document.getElementById('divPresenting').className = '';
            document.getElementById('divdiseaseHistory').className = '';
            //document.getElementById('divhivassociated').className='';
            window.print();
            document.getElementById('divwhoto').className = 'checkbox';
            document.getElementById('divPresenting').className = 'checkbox';
            document.getElementById('divdiseaseHistory').className = 'divborder';
            //document.getElementById('divhivassociated').className='checkboxLeft';
        }
        function GetControl() {
            document.forms[0].submit();
        }

        function SenderPregnantLMP() {
            var id = document.getElementById('<%=txtvisitDate.ClientID%>').value;
            if (id.length <= 0) {
                return true;
            }
            else {
                CallPregnantLMPServer(id);
                return true;
            }
        }


        function RecievePregnantData(args, context) {
            var temp = new Array(); temp = args.split('ZZZ');
            if (window.ActiveXObject) {
                var objPreg = new ActiveXObject("MSXML2.DOMDocument");
                objPreg.loadXML(temp[1]);
                var dsRootPreg = objPreg.documentElement;
                if (dsRootPreg.text != '') {
                    if (dsRootPreg.childNodes(0).childNodes(0).firstChild.text == 1 && dsRootPreg.childNodes(0).childNodes(1).firstChild.text == 0) {
                        document.getElementById('<%=txtEDDDate.ClientID%>').value = "";
                        show('rdopregnantyesno');
                        hide('spdelivery');
                    }
                    else if (dsRootPreg.childNodes(0).childNodes(0).firstChild.text == 0 && dsRootPreg.childNodes(0).childNodes(1).firstChild.text == 0) {
                        show('rdopregnantyesno');
                        hide('spdelivery');
                    }
                    else if (dsRootPreg.childNodes(0).childNodes(0).firstChild.text == 1 && dsRootPreg.childNodes(0).childNodes(1).firstChild.text == 1) {
                        show('rdopregnantyesno');
                        hide('spdelivery');
                        document.getElementById('<%=txtEDDDate.ClientID%>').value = "";
                        document.getElementById('<%=txtLMPdate.ClientID%>').value = "";
                    }
                    else if (dsRootPreg.childNodes(0).childNodes(0).firstChild.text == 9 && dsRootPreg.childNodes(0).childNodes(1).firstChild.text == 0) {
                        show('rdopregnantyesno');
                        hide('spdelivery');
                    }
                    else if (dsRootPreg.childNodes(0).childNodes(0).firstChild.text == 1 && dsRootPreg.childNodes(0).childNodes(1).firstChild.text == 9) {
                        show('spdelivery');
                        hide('rdopregnantyesno');
                        if (dsRootPreg.childNodes(0).childNodes(3).firstChild.text != "") {
                            formattedDate = dsRootPreg.childNodes(0).childNodes(3).firstChild.text;
                            var yr = formattedDate.substr(7, 4);
                            var mm = formattedDate.substr(3, 3);
                            var dd = formattedDate.substr(0, 2);
                            formattedDate = dd + "-" + mm + "-" + yr;
                            document.getElementById('<%=txtEDDDate.ClientID%>').value = formattedDate;
                        }
                    }
                    else if (dsRootPreg.childNodes(0).childNodes(0).firstChild.text == 1 && dsRootPreg.childNodes(0).childNodes(1).firstChild.text == 0) {
                        show('spdelivery');
                        hide('rdopregnantyesno');
                        if (dsRootPreg.childNodes(0).childNodes(3).firstChild.text != "") {
                            formattedDate = dsRootPreg.childNodes(0).childNodes(3).firstChild.text;
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


        function SenderARTStatus(pID) {
            CallServer(pID, "This is context from client");
            return true;

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


        function ReceiveServerData(args, context) {
            if (window.ActiveXObject) {
                var obj = new ActiveXObject("MSXML2.DOMDocument");
                obj.loadXML(args);
                var dsRoot = obj.documentElement;
                if ((dsRoot.childNodes(0).childNodes(0).firstChild.text == 1) && (document.getElementById('<%=rdoprevARVExposureNone.ClientID%>').checked) == true) {
                    alert("None Cannot Selected as this patient is already in ART");
                }
                else { return true }
            }
        }
    
    </script>
    <style type="text/css">
    .LeftMrgn
    {
        margin-left:1px;
    }
    .supMrgn
    {
        padding-top:6px;
    }
    </style>
    <div class="content-wrapper">
    <asp:HiddenField ID="HdnPregDeli" runat="server" />
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">Initial Evaluation Form</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content--%>
         <div class="row">
         <br />
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label for="inputEmail3" class="control-label">
             <label id="Vdate" class="required right35">Visit Date:</label>
             </label>
             </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <div style="width: 100%; margin: 0px auto;">
<div style="float: left"><asp:TextBox ID="txtvisitDate" runat="server" Width="110" MaxLength="11" CssClass="form-control"></asp:TextBox></div>
<div style="float: left"><img onclick="w_displayDatePicker('<%= txtvisitDate.ClientID %>');" height="22" alt="Date Helper"
                                hspace="3" src="../images/cal_icon.gif" width="22" border="0" style="vertical-align: bottom;
                                margin-bottom: 2px;" /></div>
<div style="float: left"><span class="smallerlabel">(DD-MMM-YYYY)</span></div>
</div>
            
                            
                            
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group"><label for="inputEmail3" class="control-label">Date of HIV Diagnosis:</label>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <div class="row text-center">
<div style="width: 100%; margin: 0px auto;">
<div style="float: left">
<input id="txtHIVDiagnosisdate" maxlength="11" size="11" name="dateHIVDiagnosis" runat="server" style="width:110px;" class="form-control"/></div>
<div style="float: left"> <img onclick="w_displayDatePicker('<%= txtHIVDiagnosisdate.ClientID%>');" height="22"
                                alt="Date Helper" hspace="3" src="../images/cal_icon.gif" width="22" border="0"
                                style="vertical-align: bottom; margin-bottom: 2px;" /></div>
<div style="float: left"><span class="smallerlabel">(DD-MMM-YYYY)</span></div>
</div>
            
                           
                            

            </div>
             </div>


         </div>
         <div class="row">
         <div class="col-xs-6">
         <div class="row">
             <div class="col-md-5 col-sm-12 col-xs-12 form-group">
             <label for="inputEmail3" class="control-label">Diagnosis Verified:</label>
             </div>
            <div class="col-md-5 col-sm-12 col-xs-12 form-group"> 
            <input id="rdoHIVDiagnosisVerifiedYes" onmouseup="up(this);" onfocus="up(this);"
                                onclick="down(this);" type="radio" name="HIVDiagnosisVerified1" runat="server" />
                            <label>
                                Yes</label>
                            <input id="rdoHIVDiagnosisVerifiedNo" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);"
                                type="radio" name="HIVDiagnosisVerified1" runat="server" />
                            <label>
                                No</label>
            </div>
            
             </div>
         </div>
         <div class="col-xs-6">
         <div class="row">
             <div class="col-md-5 col-sm-12 col-xs-12 form-group">
             <label for="inputEmail3" class="control-label">Disclosed:</label>
             </div>
            <div class="col-md-5 col-sm-12 col-xs-12 form-group">
             <input id="rdoDisclosureYes" onmouseup="up(this);" onfocus="up(this);" onclick="show('showdisclosureName');"
                                type="radio" value="1" name="disclosure" runat="server" />&nbsp;
                            <label>
                                Yes</label>
                            <input id="rdoDisclosureNo" onmouseup="up(this);" onfocus="up(this);" type="radio"
                                value="0" name="disclosure" runat="server" />
                            <label>
                                No</label><br />
                            <div id="showdisclosureName" style="display: none">
                                <label>
                                    Who to:</label>
                                <div id="divwhoto" class="checkboxns">
                                    <table id="tblHIVdisclosure" width="100%" class="table-hover" runat="server">
                                    </table>
                                </div>
                            </div>
            </div>
            
             </div>
         </div>
         </div>
         <div class="row" id="tdPregnant" runat="server">
         <div class="row" >
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <label for="inputEmail3" class="control-label"> <label id="Label1" for="LMP" style="margin-left:15px;">LMP:</label></label>
             </div>
            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
            <div style="width: 100%; margin: 0px auto;">
<div style="float: left"> <input id="txtLMPdate" runat="server" maxlength="11" size="11" class="form-control" style="width:110px;" /></div>
<div style="float: left"><img id="imgLMP" onclick="w_displayDatePicker('<%=txtLMPdate.ClientID%>');" height="22"
                                    alt="Date Helper" hspace="3" src="../images/cal_icon.gif" width="22" border="0"
                                    style="vertical-align: bottom; margin-bottom: 2px;" /></div>
<div style="float: left"><span class="smallerlabel">(DD-MMM-YYYY)</span></div>
</div>
           
                                
                                
            </div>
            <div id="rdopregnantyesno" style="display: inline">
            <div class="col-md-1 col-sm-12 col-xs-12 form-group">
            <label for="inputEmail3" class="control-label"><label id="lblpregnanttmp">Pregnant:</label></label>
            </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <input id="rdopregnantYes" onmouseup="up(this);" onfocus="up(this);" onclick="down(this); show('spanEDD')"
                                    type="radio" value="1" name="pregnant" runat="server" />
                                <label>
                                    Yes</label>
                                <input id="rdopregnantNo" onmouseup="up(this);" onfocus="up(this);" onclick="down(this); hide('spanEDD')"
                                    type="radio" value="0" name="pregnant" runat="server" />
                                <label>
                                    No</label>
            </div>
            <span id="spanEDD" style="display: none">
            <div class="col-md-1 col-sm-12 col-xs-12 form-group"><label for="inputEmail3" class="control-label">
            <label id="lblEDD" for="EDD">EDD:</label></label>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <div style="width: 100%; margin: 0px auto;">
<div style="float: left"><input id="txtEDDDate" runat="server" maxlength="11" size="11" class="form-control" style="width:110px;" /></div>
<div style="float: left"><img id="imgEDD" onclick="w_displayDatePicker('<%=txtEDDDate.ClientID%>');" height="22"
                                        alt="Date Helper" hspace="3" src="../images/cal_icon.gif" width="22" border="0"
                                        style="vertical-align: sub;" /></div>
<div style="float: left"><span class="smallerlabel">(DD-MMM-YYYY)</span> </div>
</div>
            
                                    
                                    
            </div>
            </span>
            </div>
             </div>
             <div class="row" id="spdelivery" style="display: none">
             <div class="row">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label for="inputEmail3" class="control-label">
             <label id="lbldelivered">Delivered since last visit:</label></label>
             </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            <input id="rdoDeliveredYes" onmouseup="up(this);" onfocus="up(this);" onclick="down(this); show('spanDelDate'); hide('spanEDD')"
                                    type="radio" value="1" name="delivered" runat="server" />
                                <label id="lblDeliveredYes" runat="server">
                                    Yes</label>
                                <input id="rdoDeliveredNo" onmouseup="up(this);" onfocus="up(this);" onclick="down(this); show('spanEDD'); hide('spanDelDate')"
                                    type="radio" value="0" name="delivered" runat="server" />
                                <label id="lblDeliveredNo" runat="server">
                                    No</label>
            </div>
            <span id="spanDelDate" style="display: none">
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label for="inputEmail3" class="control-label">
              <label id="lblDelDate" for="DelDate">
                                        Delivered Date:</label>
             </label>
             </div>
            
            <div class="col-md-5 col-sm-12 col-xs-12 form-group">
            <div style="width: 100%; margin: 0px auto;">
<div style="float: left"><input id="txtDeliDate" runat="server" maxlength="11" size="11" class="form-control" style="width:110px;" /></div>
<div style="float: left"><img id="imgDelDate" onclick="w_displayDatePicker('<%=txtDeliDate.ClientID%>');"
                                        height="22" alt="Date Helper" hspace="3" src="../images/cal_icon.gif" width="22"
                                        border="0" style="vertical-align: bottom; margin-bottom: 2px;" /></div>
<div style="float: left"><span class="smallerlabel">(DD-MMM-YYYY)</span></div>
</div>
            
                                    
                                    
            </div>
            </span>
             </div>
             </div>
             </div>
             
             <div class="row box-header LeftMrgn" >
             <h5 class="box-title">
            Presenting Complaints</h5>
            </div>
            <div class="row">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label for="inputEmail3" class="control-label">Presenting Complaints:</label>
             </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <div style="width: 100%; margin: 0px auto;">
<div style="float: left"><input id="chkpresentingComplaintsNone" enableviewstate="true" type="checkbox" checked="checked"
            value="None" name="presentingComplaintsNone" runat="server" /></div>
<div style="float: left"><span class="smalllabel">None</span></div>
<div style="float: left"><input id="chkpresentingComplaintsNonehidden"
                                checked="checked" value="None" class="textstylehidden" type="checkbox" runat="server" /></div>
</div>
            

            

            </div>
            <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <div id="presentingComplaintsShow" enableviewstate="true" style="display: none" runat="server">
                                <div class="checkbox" id="divPresenting" nowrap="noWrap">
                                    <asp:CheckBoxList ID="cblPresentingComplaints" RepeatLayout="Flow" runat="server">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
            </div>
           
             </div>
             
             <div class="row">
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label for="inputEmail3" class="control-label">TB Screening:</label>
             </div>
            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
            <div style="width: 100%; margin: 0px auto;">
<div style="float: left"><input id="rdoPerformed" onmouseup="up(this);" onfocus="up(this);" onclick="toggle('divTBPerformed'); down(this); show('divTBPerformed');"
            type="radio" value="show" name="Symptom" runat="server" /></div>
<div style="float: left"> <span id="SpanTBPerformed" class="smallerlabel">Performed</span></div>
<div style="float: left"><input id="rdoNotDocumented" onmouseup="up(this);" onfocus="up(this);" onclick="toggle('divTBPerformed');down(this); hide('divTBPerformed');"
              type="radio" value="Not Documented" name="Symptom" runat="server" /></div>
<div style="float: left"><span id="SpanTBNotDocumented" class="smallerlabel">Not Documented</span></div>
</div>
  </div>
           
            <div class="col-md-6 col-sm-12 col-xs-12 form-group">
            <div class="divborder" id="divTBPerformed" nowrap="noWrap" style="display: none">
                                <asp:CheckBoxList ID="cblTBScreen" runat="server">
                                </asp:CheckBoxList>
                            </div>
            </div>
             </div>

             <div class="row box-header LeftMrgn">
             <h5 class="box-title">
            Medical History</h5>
            </div>
            <div class="row">
               <div class="col-xs-6"> 
               <div class="row">
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label for="inputEmail3" class="control-label">Disease History:</label>
             </div>
            <div class="col-md-8 col-sm-12 col-xs-12 form-group">
            <input class="margin20" id="rdoMedHistNone" onmouseup="up(this);" onfocus="up(this);"
                                type="radio" value="none" name="medicalHis" runat="server" />
                            <span class="smallerlabel">None</span>
                            <input class="margin20" id="rdoMedHistnotdocumented" onmouseup="up(this);" onfocus="up(this);"
                                type="radio" value="Not documented" name="medicalHis" runat="server" />
                            <span class="smallerlabel">Not Documented</span>
            </div>
           
             </div>
             <div class="row">
             <div class="col-md-8 col-sm-12 col-xs-12 form-group">
             <div id="divdiseaseHistory" nowrap="nowrap">
                                <asp:GridView ID="GrdMedHist" runat="server" Width="100%" AutoGenerateColumns="False"
                                    ShowHeader="False">
                                </asp:GridView>
                            </div>
             </div>
             </div>
               </div>
               <div class="col-xs-6"> 
               <%--Right--%>
               <div class="row">
               <label for="inputEmail3" class="control-label">Drug Allergies/Toxicities:</label>
               
               </div>
               <div class="row">
               <div class="col-md-2">
              <input id="rdoAllergynone" onmouseup="up(this);" onfocus="up(this);" type="radio"
                                value="none" name="noneAllergy" runat="server" />
                            <span id="lblAllergyNone" class="smallerlabel">None</span>
             </div>
             <div class="col-md-3">
             <input class="margin10" id="rdoAllergynotdocumented" onmouseup="up(this);" onfocus="up(this);"
                                type="radio" value="Not Documented" name="noneAllergy" runat="server" />
                            <span id="lblAllergyNotDocumented" class="smallerlabel">Not Documented</span>
             </div>
             <div class="col-md-3">
             <input class="margin10" id="chksulfaAllergy" onclick="SetCheckBoxes('aspnetForm','noneAllergy',false);"
                                type="checkbox" value="Yes" name="sulfaAllergy" runat="server" />
                            <span id="lblAllergySulfa" class="smallerlabel">Sulfa Drugs</span>
             </div>
             <div class="col-md-3">
             <input class="margin10" id="chkotherAllergy" type="checkbox" value="yes" name="otherAllergy"
                                runat="server" />
                            <span id="lblAllergyOther" class="smallerlabel">Other (specify):</span> 
             </div>
             <div class="col-md-4">
             <span id="otherAllergyName"
                                style="display: none">
                                <input class="inputsmall" id="txtotherAllergyName" maxlength="40" size="10" name="otherAllergyName"
                                    runat="server" />
                            </span>
             </div>
                       
               </div>
                <div class="row">
                Current Long Term Medications (select all that apply):
                </div>
                <div class="row">
                <div class="row">
                 <div class="col-md-3">
                 <label class="check" for="longTermMedsSulfa">
                                                <input id="chklongTermMedsSulfa" onclick="toggle('longTermMedsSulfaSelected');" type="checkbox"
                                                    name="longTermMedsSulfa" runat="server" />Sulfa/TMP</label>
                 </div>
                  <div class="col-md-8">
                  <div id="longTermMedsSulfaSelected" style="display: none">
                  <div style="width: 100%; margin: 0px auto;">
<div style="float: left"> <label class="margin15">
                                                    Describe:</label></div>
<div style="float: left"><input id="txtlongTermMedsSulfaDesc" maxlength="40" size="30" name="longTermMedsSulfaDesc"
                                                    runat="server" class="form-control" style="width:300px;"/></div>
</div>
                                               
                                                
                                            </div>
                 </div>
                </div>
                <div class="row">
                 <div class="col-md-3">
                 <label class="check" for="longTermMedsARV">
                                                <input id="chkLongTermTBMed" onclick="toggle('longTermMedsTBSelected');" type="checkbox"
                                                    name="LongTermTBMed" runat="server" />TB Rx (Specify)</label>
                 </div>
                  <div class="col-md-8">
                  <div id="longTermMedsTBSelected" style="display: none">
                  <div style="width: 100%; margin: 0px auto;">
<div style="float: left"><label class="margin15">Describe:</label></div>
<div style="float: left"><input id="txtLongTermTBMedDesc" maxlength="40" size="10" name="LongTermTBMedDesc"
                                                    runat="server" class="form-control" style="width:300px;"/></div>

</div>
<br />
<br />
<div style="width: 100%; margin: 0px auto;">
<div style="float: left">
<label class="margin10">Start Date:</label></div>
<div style="float: left">
<input id="txtLongTermTBStartDate" onkeyup="DateFormat(this,this.value,event,false,'4')" onfocus="javascript:vDateType='4'" maxlength="8" size="5" name="LongTermTBStartDate"
 runat="server" class="form-control" style="width:110px;" />
 </div>
 <div style="float: left">
 <span class="smallerlabel margin10">(MMM-YYYY)</span></div>

</div>
                                                
                                                
                                                
                                                
                                            </div>
                 </div>
                </div>
                <div class="row">
                 <div class="col-md-3">
                 <label class="check" for="longTermMedsOther1">
                                                <input id="chklongTermMedsOther1" onclick="toggle('longTermMedsOther1Selected');"
                                                    type="checkbox" value="Other1" name="longTermMedsOther1" runat="server" />
                                                Other (Specify)</label>
                 </div>
                  <div class="col-md-8">
                  <div id="longTermMedsOther1Selected" style="display: none">
                  <div style="width: 100%; margin: 0px auto;">
<div style="float: left"><label class="margin15">Describe:</label></div>
<div style="float: left">
<input id="txtlongTermMedsOther1Desc" maxlength="40" size="30" name="longTermMedsOther1Desc" runat="server" class="form-control" style="width:300px;"/></div>
</div>
                                                
                                                
                                            </div>
                 </div>
                </div>
                <div class="row">
                 <div class="col-md-3">
                  <label class="check" for="longTermMedsOther2">
                                                <input id="chklongTermMedsOther2" onclick="toggle('longTermMedsOther2Selected');"
                                                    type="checkbox" value="Other2" name="longTermMedsOther2" runat="server" />
                                                Other (Specify)</label>
                 </div>
                  <div class="col-md-8">
                  <div id="longTermMedsOther2Selected" style="display: none">
                  <div style="width: 100%; margin: 0px auto;">
<div style="float: left"><label class="margin15">Describe:</label></div>
<div style="float: left">
<input id="txtlongTermMedsOther2Desc" maxlength="40" size="30" name="longTermMedsOther2Desc"
                                                    runat="server" class="form-control" style="width:300px;"/>
</div>

</div>
                                                
                                                
                                            </div>
                 </div>
                </div>
                
                </div>
               </div>
            </div>
             <div class="row box-header LeftMrgn">
             <h5 class="box-title">
            HIV-Related History</h5>
            </div>

             <div class="row">
               <div class="col-xs-6">
               <%--left--%> 
               <div class="row">
               <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                <label>Lowest CD4:</label>
               </div>
               <div class="col-md-8 col-sm-12 col-xs-12 form-group">
                                           <input id="rdoprevLowestCD4none" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);"
                                type="radio" name="prevLowestCD4none" runat="server" />
                            <span class="smallerlabel">None</span>
                            <input id="rdoprevLowestCD4notdocumented" onmouseup="up(this);" onfocus="up(this);"
                                onclick="down(this);" type="radio" value="" name="prevLowestCD4none" runat="server" /><span
                                    class="smallerlabel">Not Documented</span>
               </div>
              </div>
              <div class="row">
              <div class="col-md-4 col-sm-12 col-xs-12 form-group">
              <div style="width: 100%; margin: 0px auto;">
<div style="float: left"><input id="txtprevLowestCD4" maxlength="4" size="3" name="prevLowestCD4"
                                runat="server" class="form-control" style="width:60px;"/></div>
<div style="float: left;" class="supMrgn"><span class="smallerlabel">c/mm<sup>3</sup></span></div>
<div style="float: left"><input id="txtprevLowestCD4Percent" maxlength="3" size="2" name="prevLowestCD4Percent"
                                runat="server" class="form-control" style="width:60px;"/></div>
<div style="float: left" class="supMrgn"><span class="smallerlabel">%</span></div>
</div>
              
                            
                            
                            

              </div>
              <div class="col-md-8 col-sm-12 col-xs-12 form-group">
              <div style="width: 100%; margin: 0px auto;">
<div style="float: left"><label class="margin10">
                                Date:</label></div>
<div style="float: left"><input id="txtprevLowestCD4Date" maxlength="11" size="8" name="prevLowestCD4Date"
                                runat="server" class="form-control" style="width:110px;" /></div>
<div style="float: left"><img onclick="w_displayDatePicker('<%= txtprevLowestCD4Date.ClientID%>');" height="22"
                                alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22" border="0"
                                style="vertical-align: top; margin-top: -2px;" /></div>
<div style="float: left"><span class="smallerlabel">(DD-MMM-YYYY)</span></div>
</div>
                           
              </div>
              </div>
              <div class="row">
              <div class="col-md-4 col-sm-12 col-xs-12 form-group">
              <label>CD4 Prior to Starting ARVs:</label>
              </div>
              <div class="col-md-8 col-sm-12 col-xs-12 form-group">
              <input id="rdopriorARVsCD4none" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);"
                                type="radio" value="" name="priorARVsCD4none" runat="server" />
                            <span class="smallerlabel">None</span>
                            <input id="rdopriorARVsCD4notdocumented" onmouseup="up(this);" onfocus="up(this);"
                                onclick="down(this);" type="radio" value="" name="priorARVsCD4none" runat="server" />
                            <span class="smallerlabel">Not Documented</span>
              </div>
              </div>
              <div class="row">
              <div class="col-md-4 col-sm-12 col-xs-12 form-group">
              <div style="width: 100%; margin: 0px auto;">
<div style="float: left"><input id="txtpriorARVsCD4" maxlength="4" size="3" name="priorARVsCD4"
                                runat="server" class="form-control" style="width:60px;"/></div>
<div style="float: left;" class="supMrgn"><span class="smallerlabel">c/mm<sup>3</sup></span></div>
<div style="float: left"><input id="txtpriorARVsCD4Percent" maxlength="3" size="2" name="priorARVsCD4Percent"
                                runat="server" class="form-control" style="width:60px;"/></div>
<div style="float: left" class="supMrgn"><span class="smallerlabel">%</span></div>
</div>
              
                            
                            
                            
              </div>
              <div class="col-md-8 col-sm-12 col-xs-12 form-group">
              <div style="width: 100%; margin: 0px auto;">
<div style="float: left"><label class="margin10">Date:</label></div>
<div style="float: left"><input id="txtpriorARVsCD4Date" maxlength="11" size="8" name="priorARVsCD4Date" runat="server" class="form-control" style="width:110px;"/></div>
<div style="float: left"><img onclick="w_displayDatePicker('<%= txtpriorARVsCD4Date.ClientID%>');" height="22"
                                alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22" border="0"
                                style="vertical-align: bottom; margin-bottom: 2px;" /></div>
<div style="float: left"><span class="smallerlabel">(DD-MMM-YYYY)</span></div>
</div>
              
                            
                            
                            
              </div>
              </div>
              <div class="row">
              <div class="col-md-4 col-sm-12 col-xs-12 form-group">
              <label>Most Recent CD4:</label>
              </div>
              <div class="col-md-8 col-sm-12 col-xs-12 form-group">
              <input id="rdomostRecentCD4none" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);"
                                type="radio" value="" name="mostRecentCD4none" runat="server" />
                            <span class="smallerlabel">None</span>
                            <input id="rdomostRecentCD4notdocumented" onmouseup="up(this);" onfocus="up(this);"
                                onclick="down(this);" type="radio" value="" name="mostRecentCD4none" runat="server" />
                            <span class="smallerlabel">Not Documented</span>
              </div>
              </div>

              <div class="row">
              <div class="col-md-4 col-sm-12 col-xs-12 form-group">
              <div style="width: 100%; margin: 0px auto;">
<div style="float: left"><input id="txtmostRecentCD4" maxlength="4" size="3" name="mostRecentCD4"
                                runat="server" class="form-control" style="width:60px;"/></div>
<div style="float: left;" class="supMrgn"><span class="smallerlabel">c/mm<sup>3</sup></span></div>
<div style="float: left"><input id="txtmostRecentCD4Percent" maxlength="3" size="2" name="mostRecentCD4Percent"
                                runat="server" class="form-control" style="width:60px;"/></div>
<div style="float: left" class="supMrgn"><span class="smallerlabel">%</span></div>
</div>

              
                            
                            
                            
              </div>
              <div class="col-md-8 col-sm-12 col-xs-12 form-group">
              <div style="width: 100%; margin: 0px auto;">
<div style="float: left"><label class="margin10">Date:</label></div>
<div style="float: left">
<input id="txtmostRecentCD4Date" maxlength="11" size="8" name="mostRecentCD4Date" runat="server" class="form-control" style="width:110px;"/></div>
<div style="float: left"><img onclick="w_displayDatePicker('<%=txtmostRecentCD4Date.ClientID%>');" height="22"
                                alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22" border="0"
                                style="vertical-align: bottom; margin-bottom: 2px;" /></div>
<div style="float: left"><span class="smallerlabel">(DD-MMM-YYYY)</span></div>
</div>
              
                            
                            
                            
              </div>
              </div>

              <div class="row">
              <div class="col-md-4 col-sm-12 col-xs-12 form-group">
              <label>Pre-therapy Viral Load:</label>
              </div>
              <div class="col-md-8 col-sm-12 col-xs-12 form-group">
              <input id="rdomostRecentViralLoadnone" onmouseup="up(this);" onfocus="up(this);"
                                onclick="down(this);" type="radio" value="" name="mostRecentViralLoadnone" runat="server" />
                            <span class="smallerlabel">None</span>
                            <input id="rdomostRecentViralLoadnotdocumented" onmouseup="up(this);" onfocus="up(this);"
                                onclick="down(this);" type="radio" value="" name="mostRecentViralLoadnone" runat="server" />
                            <span class="smallerlabel">Not Documented</span>
              </div>
              </div>
               <div class="row">
              <div class="col-md-4 col-sm-12 col-xs-12 form-group">
              <div style="width: 100%; margin: 0px auto;">
<div style="float: left">
<input id="txtmostRecentViralLoad" maxlength="9" size="11" name="mostRecentViralLoad"
runat="server" class="form-control" style="width:100px;"/></div>
<div style="float: left;" class="supMrgn"> <span class="smallerlabel">c/ml</span></div>
</div>

              
                           
              </div>
              <div class="col-md-8 col-sm-12 col-xs-12 form-group">
              <div style="width: 100%; margin: 0px auto;">
<div style="float: left"><label class="margin50">Date:</label></div>
<div style="float: left">
<input id="txtmostRecentViralLoadDate" maxlength="11" size="8" name="mostRecentViralLoadDate" runat="server" class="form-control" style="width:110px;"/></div>
<div style="float: left">
<img onclick="w_displayDatePicker('<%= txtmostRecentViralLoadDate.ClientID%>');"
                                height="22" alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22"
                                border="0" style="vertical-align: bottom; margin-bottom: 2px;" /></div>
<div style="float: left"><span class="smallerlabel">(DD-MMM-YYYY)</span></div>
</div>
              
                            
                            
                            

              </div>
              </div>
               <div class="row">
              <div class="col-md-3 col-sm-12 col-xs-12 form-group">
               <label>HIV-Associated Conditions:</label>
              </div>
              <div class="col-md-9 col-sm-12 col-xs-12 form-group">
              <input id="rdoHIVassocNone" onmouseup="up(this);" onfocus="up(this);" type="radio"
                                value="none" name="assocNone" runat="server" />
                            <span id="lblHIVassocNone" class="smallerlabel">None</span>
                            <input id="rdoPrevHIVassocNotDocumented" onmouseup="up(this);" onfocus="up(this);"
                                type="radio" value="Not Documented" name="assocNone" runat="server" />
                            <span id="lblHIVassocNotdocumented" class="smallerlabel">Not Documented</span>
                            <input id="rdoHIVassociate" onmouseup="up(this);" onfocus="up(this);" onclick="down(this); toggle('assocSelected');"
                                type="radio" value="show" name="assocNone" runat="server" />
                            <span id="lblHIVassociate" class="smallerlabel">Enter HIV-Associated Conditions</span>
              </div>
              </div>
              <div class="row">
              <div id="assocSelected" style="display: none;">
                                <div id="diventerillnessleft" class="checkboxleft" style="margin-left:20px;">
                                    <table id="tblHIVAIDSleft" class="table-hover" runat="server">
                                    </table>
                                </div>
                                <div class="checkboxright" id="diventerillnessright" style="margin-left:20px;">
                                    <table id="tblHIVAIDSright" class="table-hover" runat="server">
                                    </table>
                                </div>
                            </div>
              </div>

               </div>
               <div class="col-xs-6">
               <%--right--%> 
                <div class="row">
                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <label>ARV Exposure:</label>
                </div>
                <div class="col-md-9 col-sm-12 col-xs-12 form-group">
                 <input id="rdoprevARVExposureNone" onmouseup="up(this);" onfocus="up(this);" type="radio"
                                value="" name="prevARVExposure" runat="server" />
                            <span id="lblprevARVExposureNone" class="smallerlabel">None</span>
                            <input id="rdoprevARVExpnotdocumented" onmouseup="up(this);" onfocus="up(this);"
                                type="radio" value="" name="prevARVExposure" runat="server" />
                            <span id="lblprevARVExposurenotdocumented" class="smallerlabel">Not Documented</span>
                            <input id="rdopreviousARV" onmouseup="up(this);" onfocus="up(this);" onclick="show('prevexpdiv');"
                                type="radio" value="" name="prevARVExposure" runat="server" />
                            <span id="lblpreviousARV" class="smallerlabel">Enter ARV Exposure</span>
                </div>
                </div>
                <div id="prevexpdiv" style="display: none;">

                <div class="row">
                <div class="col-md-3 col-sm-12 col-xs-12 form-group"><label id="Cuart" class="required">
                                    Historical ART:</label></div>
                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                <div style="width: 100%; margin: 0px auto;">
<div style="float: left"><input id="txtcurrentART" size="16" name="currentART" readonly="readonly" runat="server" class="form-control" style="width:130px;" /></div>
<div style="float: left"><asp:Button ID="btnRegimen" runat="server" Text="..." OnClick="btnRegimen_Click"
                                    CssClass="btn btn-primary" Height="30px" /></div>
</div>


                
                
                </div>                
                <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                <div style="width: 100%; margin: 0px auto;">
<div style="float: left"><span class="smalllabel margin10">Start Date:</span></div>
<div style="float: left"><input id="txtcurrentARTDate" maxlength="8" size="5" onkeyup="DateFormat(this,this.value,event,false,'4')"
                                    onfocus="javascript:vDateType='4'" name="currentARTDate" placeholder="MMM-YYYY" runat="server" class="form-control" style="width:110px;"/></div>

</div>
                
                                
                </div>
                </div>
                <div class="row">
                
                <label>Previous ARV Exposure:</label>
                </div>
                 <div class="row">
                 <div class="col-md-4 col-sm-12 col-xs-12 form-group">

                 <input id="chkprevSDNVPNVP" onclick="toggle('prevSingleDoseNVPSelected');" value="Yes"
                                    type="checkbox" name="prevSingleDoseNVP" runat="server" class="margin50" />
                                <label id="lblNVP">
                                    Single Dose NVP</label>
                 </div>
                 <span id="prevSingleDoseNVPSelected" style="display: none">                 
                 <div class="col-md-4 col-sm-12 col-xs-12 form-group">

<div style="width: 100%; margin: 0px auto;">
<div style="float: left"><label>Date:</label></div>
<div style="float: left"><input id="txtprevSingleDoseNVPDate1" onkeyup="DateFormat(this,this.value,event,false,'4')"
                  onfocus="javascript:vDateType='4'" placeholder="MMM-YYYY" maxlength="8" size="5" name="prevSingleDoseNVPDate" runat="server" class="form-control" style="width:110px;"/></div>
</div>

                 
                 
                 </div>
                 
                 
                 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                 <div style="width: 100%; margin: 0px auto;">
<div style="float: left"><label class="right5">Date:</label></div>
<div style="float: left">
 <input id="txtprevSingleDoseNVPDate2" onkeyup="DateFormat(this,this.value,event,false,'4')"
                  onfocus="javascript:vDateType='4'" placeholder="MMM-YYYY" maxlength="8" size="5" name="prevSingleDoseNVPDate" runat="server" 
                  class="form-control" style="width:110px;"/>
</div>
</div>

                 
                </div>
                
                </span>
                </div>
                 <div class="row">      
                  <div class="col-md-4 col-sm-12 col-xs-12 form-group">          
               <input id="chkprevARVRegimen" onclick="toggle('prevARVReg');" type="checkbox" value="No"
                                        name="prevARVRegimen" class="margin50" runat="server" /><label id="lblprevARVRegimen">Previous
                                            Regimens</label>
                </div>
                </div>
                <div class="row">
                <div id="prevARVReg" style="display: none">
                <div class="row">
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label for="prevARVRegimen1Name">Regimen:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
              <input id="txtprevARVRegimen1Name" size="15" name="prevARVRegimen1Name" readonly="readonly" runat="server" class="form-control" style="width:130px;"/>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:Button ID="btnRegimen1" runat="server" Text="..." OnClick="btnRegimen1_Click" CssClass="btn btn-primary" />
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <span class="smalllabel margin10">Months on Rx*</span>
             </div>  
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <input id="txtprevARVRegimen1Months" maxlength="7" size="5" name="prevARVRegimen1Months" runat="server" class="form-control" style="width:100px;"/>
             </div>                      
            </div>
            <div class="row">
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
              <label for="prevARVRegimen2Name">Regimen:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <input id="txtprevARVRegimen2Name" size="15" name="prevARVRegimen2Name" readonly="readonly" runat="server" class="form-control" style="width:130px;"/>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            <asp:Button ID="btnRegimen2" runat="server" Text="..." OnClick="btnRegimen2_Click" CssClass="btn btn-primary" />
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <span class="smalllabel margin10">Months on Rx*</span>
             </div>   
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <input id="txtprevARVRegimen2Months" maxlength="7" size="5" name="prevARVRegimen2Months" runat="server" class="form-control" style="width:100px;"/>
             </div>                       
            </div>
            <div class="row">
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label for="prevOtherRegimen1Name"> Regimen:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            <input id="txtprevARVRegimen3Name" size="15" name="prevOtherRegimen1Name" readonly="readonly" runat="server" class="form-control" style="width:130px;"/>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:Button ID="btnRegimen3" runat="server" Text="..." OnClick="btnRegimen3_Click" CssClass="btn btn-primary"/>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <span class="smalllabel margin10">Months on Rx*</span>
             </div>     
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <input id="txtprevARVRegimen3Months" maxlength="7" size="5" name="prevOtherRegimen1Months" runat="server" class="form-control" style="width:100px;"/>
             </div>                     
            </div>
            <div class="row">
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label for="prevOtherRegimen2Name">Regimen:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <input id="txtprevARVRegimen4Name" size="15" name="prevOtherRegimen2Name" readonly="readonly" runat="server" class="form-control" style="width:130px;"/>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:Button ID="btnRegimen4" runat="server" Text="..." OnClick="btnRegimen4_Click" CssClass="btn btn-primary" />
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <span class="smalllabel margin10">Months on Rx*</span>
             </div> 
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <input id="txtprevARVRegimen4Months" maxlength="7" size="5" name="prevOtherRegimen2Months" runat="server" class="form-control" style="width:100px;"/>
             </div>                         
            </div>

                                    
                                </div>
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
<div style="float: left" class="supMrgn"><label id="TempVal">Temp:</label></div>
<div style="float: left"><input id="txtphysTemp" maxlength="4" size="4" name="physTemp" runat="server" class="form-control" style="width:70px;"/></div>
<div style="float: left" class="supMrgn"><span class="smallerlabel"> <label>C</label></span></div>
</div>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <div style="width: 100%; margin: 0px auto;">
<div style="float: left" class="supMrgn"><label>RR:</label></div>
<div style="float: left"><input id="txtphysRR" maxlength="4" size="4" name="physRR" runat="server" class="form-control" style="width:70px;"/></div>
<div style="float: left" class="supMrgn"><span class="smallerlabel"> <label>bpm</label></span></div>
</div>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <div style="width: 100%; margin: 0px auto;">
<div style="float: left" class="supMrgn"> <label>HR:&nbsp;&nbsp;</label></div>
<div style="float: left"><input id="txtphysHR" maxlength="3" size="4" name="physHR" runat="server" class="form-control" style="width:70px;"/></div>
<div style="float: left" class="supMrgn"> <span class="smallerlabel"><label>bpm</label></span></div>
</div>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <div style="width: 100%; margin: 0px auto;">
<div style="float: left" class="supMrgn"><label>BP:</label></div>
<div style="float: left"><input id="txtphysBPSystolic" maxlength="4" size="4" name="physBPSystolic" runat="server" class="form-control" style="width:70px;"/></div>
<div style="float: left" class="supMrgn"><b>/</b></div>
<div style="float: left"><input id="txtphysBPDiastolic" maxlength="4" size="4" name="physBPDiastolic" runat="server" class="form-control" style="width:70px;"/></div>
<div style="float: left" class="supMrgn"><span class="smallerlabel"><label>(mm/Hg)</label></span></div>
</div>
             </div>
                   
    </div>
    <div class="row">
    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
    <div style="width: 100%; margin: 0px auto;">
<div style="float: left"  class="supMrgn"><label id="lblHT">HT:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label></div>
<div style="float: left"><input id="txtphysHeight" maxlength="4" size="4" name="physHeight" runat="server" class="form-control" style="width:70px;"/></div>
<div style="float: left" class="supMrgn"><span class="smallerlabel"><label>cm</label></span></div>
</div>
    </div>
    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
    <div style="width: 100%; margin: 0px auto;">
<div style="float: left" class="supMrgn"><label id="lblWT">WT:</label></div>
<div style="float: left"><input id="txtphysWeight" maxlength="4" size="4" name="physWeight" runat="server" class="form-control" style="width:70px;"/></div>
<div style="float: left" class="supMrgn"><span class="smallerlabel"><label>kg</label></span></div>
</div>
    </div>
    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
    <div style="width: 100%; margin: 0px auto;">
<div style="float: left" class="supMrgn"> <label>BMI:</label></div>
<div style="float: left"><input id="txtanotherbmi" size="6" name="anotherbmi" runat="server" readonly="readonly" class="form-control" style="width:70px;"/></div>
</div>
</div>
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
 <div style="width: 100%; margin: 0px auto;">
<div style="float: left" class="supMrgn"><label>Pain:</label></div>
<div style="float: left"><select id="ddlPain" name="pain" runat="server" class="form-control" style="width:140px;">
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
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label id="lblWAB" class="right35">WAB Stage:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlphysWABStage" runat="server" CssClass="form-control" Style="width:140px;">
                                <asp:ListItem Value="0" Selected="true">Select</asp:ListItem>
                                <asp:ListItem Value="84">Working</asp:ListItem>
                                <asp:ListItem Value="85">Ambulatory</asp:ListItem>
                                <asp:ListItem Value="86">Bedridden</asp:ListItem>
                            </asp:DropDownList>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label id="lblWHO" class="right35">WHO Stage:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlWHOStage" runat="server" CssClass="form-control" Style="width:140px;">
                                <asp:ListItem Value="0" Selected="true">Select</asp:ListItem>
                                <asp:ListItem Value="155">N/A</asp:ListItem>
                                <asp:ListItem Value="87">I</asp:ListItem>
                                <asp:ListItem Value="88">II</asp:ListItem>
                                <asp:ListItem Value="89">III</asp:ListItem>
                                <asp:ListItem Value="90">IV</asp:ListItem>
                            </asp:DropDownList>
             
             </div>
                       
    </div>
    <div class="row box-header LeftMrgn">
             <h5 class="box-title">
            Assessment, Plan and Regimen</h5>
            </div>
    <div class="row">
    <div class="col-xs-6">
    <%--left--%>
    <div class="row">
    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label for="inputEmail3" class="control-label">Assessment:</label>
             </div>
             <div class="col-md-10 col-sm-12 col-xs-12 form-group">
            <input name="clinAssessmentInitialNone" id="rdoclinAssessment_Plan_RegimenNone" onmouseup="up(this);"
                                onfocus="up(this);" type="radio" value="No Illness Found" runat="server" />
                            <span id="lblAssessNone" class="smallerlabel">No Illness Found</span>
                            <input name="clinAssessmentInitial" id="chkclinAssessmentInitial1" onclick="SetCheckBoxes('aspnetForm','clinAssessmentInitialNone',false);"
                                type="checkbox" value="HIV-Related Illness" runat="server" />
                            <span id="lblHIVrelated" class="smallerlabel">HIV-Related Illness/OI</span>
                            <input name="clinAssessmentInitial" id="chkclinAssessmentInitial2" onclick="SetCheckBoxes('aspnetForm','clinAssessmentInitialNone',false);"
                                type="checkbox" value="Non-HIV-Related Illness" runat="server" />
                            <span id="lblHIVrelatedNon" class="smallerlabel">Non-HIV-Related Illness</span>
             </div>
    </div>
    <div class="row">
    <div class="col-md-10 col-sm-12 col-xs-12 form-group">
             <textarea name="clinAssessmentNotes" rows="5" cols="70" id="MulttxtclinAssessmentNotes" class="form-control" style="resize:none;width:400px;" runat="server"></textarea>
             </div>
    </div>
    </div>
    <div class="col-xs-6">
    <%--Right--%>
      <div class="row">
      <div class="col-md-1 col-sm-12 col-xs-12 form-group"><label>Plan:</label></div>
      <div class="col-md-11 col-sm-12 col-xs-12 form-group">
       <input class="margin10left" id="chkclinPlanInitial" type="checkbox" value="Lab evaluation/TB screen"
                                name="clinPlanInitial" runat="server" />
                            <span class="smallerlabel">Lab evaluation/TB screen</span>
                            <input id="chkclinPlanInitial2" type="checkbox" value="OI prophylaxis/treatment"
                                name="clinPlanInitial" runat="server" />
                            <span class="smallerlabel">OI prophylaxis/treatment</span>
                            <input id="chkclinPlanInitial3" type="checkbox" value="Treatment Preparation" name="clinPlanInitial"
                                runat="server" />
                            <span class="smallerlabel">Treatment Preparation</span>
                            <br />
                            <input id="chkclinPlanInitial4" type="checkbox" value="Other" name="clinPlanInitial"
                                runat="server" />
                            <span class="smallerlabel">Other</span>
      </div>
      </div>
      <div class="row">
      <div class="col-md-10 col-sm-12 col-xs-12 form-group">
      <textarea name="clinPlanNotes" rows="5" cols="60" id="MulttxtclinPlanNotes" runat="server" class="form-control" style="resize:none;width:400px;" ></textarea>
      </div>
      </div>
    </div>


 
             
             
                       
    </div>
   <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label for="inputEmail3" class="control-label">ARV Therapy:</label>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <select id="lstclinPlanIE" onchange="specifyChangeStop(this.id);" name="clinPlanIE"
                                runat="server" class="form-control" style="width:300px;">
                                <option value="0" selected="selected">Select</option>
                                <option value="94">Treatment not indicated now</option>
                                <option value="95">Continue current treatment</option>
                                <option value="96">Restart treatment</option>
                                <option value="97">Start new treatment(naive patient)</option>
                                <option value="98">Change regimen</option>
                                <option value="99">Stop treatment</option>
                            </select>
             </div>
             </div>
              <div class="row">
             <div id="arvTherapyChange" style="display: none">
             <div class="col-md-2 col-sm-12 col-xs-12 form-group" >
             <label for="inputEmail3" class="control-label required">Change Regimen Reason:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
              <asp:DropDownList ID="ddlTherapyChange" runat="server" CssClass="form-control" Style="width:250px;"></asp:DropDownList>
             </div>
             </div>
             <div id="otherarvTherapyChangeCode" style="display: none">
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
              <label class="required right45" for="arvTherapyChangeCodeOtherName">Specify:</label>
             </div>      
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <input id="txtarvTherapyChangeCodeOtherName" maxlength="20" size="10" name="arvTherapyChangeCodeOtherName" runat="server" class="form-control" Style="width:140px;"/>
             </div>  
             </div>    
             
                            </div>
                            <div class="row">
                            <div id="arvTherapyStop" style="display: none;">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label for="inputEmail3" class="control-label required">Stop Regimen Reason:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlTherapyStop" runat="server" CssClass="form-control" Style="width:250px;"></asp:DropDownList>
             </div>
              <div id="otherarvTherapyStopCode" style="display: none">
              
              <div class="col-md-1">
             <label for="inputEmail3" class="control-label required">Specify:</label>
             </div>
             <div class="col-md-6">
             <input id="txtarvTherapyStopCodeOtherName" maxlength="20" size="10" name="arvTherapyStopCodeOtherName" runat="server" class="form-control" style="width:300px;"/>
             </div>
             
             </div>
                                
                                
                               
                                    
                                    </div>
                            </div>

 <div class="row box-header LeftMrgn">
             <h5 class="box-title">
            Clinical Notes</h5>
            </div>

<div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
            <asp:TextBox ID="txtclinicalNotes" TextMode="MultiLine" Width="99%" Height="10%" runat="server" CssClass="form-control" Style="resize:none;"></asp:TextBox>
             </div>
             
</div>
<div class="row box-header LeftMrgn">
             <h5 class="box-title">
            Appointment and Signature</h5>
            </div>
<div class="row">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label for="inputEmail3" class="control-label">When is the patient's next appointment?</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <select id="lstappPeriod" name="appPeriod" runat="server" onchange="addDays()" class="form-control" style="width:110px;">
                                <option value="0" selected="selected">Select</option>
                                <option value="7">1 week</option>
                                <option value="14">2 weeks</option>
                                <option value="28">4 weeks</option>
                                <option value="58">2 months</option>
                                <option value="88">3 months</option>
                                <option value="180">6 months</option>
                            </select>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label for="inputEmail3" class="control-label">Appointment Reason:</label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
              <asp:DropDownList ID="ddappReason" runat="server" CssClass="form-control" style="width:300px;"> </asp:DropDownList>
             </div>
                       
    </div>
    <div class="row">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label for="inputEmail3" class="control-label">Specify Date:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <div style="width: 100%; margin: 0px auto;">
<div style="float: left"><input id="txtappDate" maxlength="11" size="11" name="appDate" runat="server" class="form-control" style="width:110px;" /></div>
<div style="float: left"><img onclick="w_displayDatePicker('<%=txtappDate.ClientID %>');" height="22" alt="Date Helper"
                                hspace="5" src="../images/cal_icon.gif" width="22" border="0" style="margin-left: 0px;
                                margin-bottom: 5px;" /></div>
<div style="float: left"><span class="smallerlabel" id="appDatespan">(DD-MMM-YYYY)</span></div>
</div>

             
                            
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label for="inputEmail3" class="control-label">Signature:</label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddinterviewer" runat="server" CssClass="form-control" style="width:300px;"></asp:DropDownList>
             </div>
                  
    </div>
     <div class="row">
      <asp:Panel ID="pnlCustomList" Visible="false" runat="server" Height="100%" Width="100%"
                                Wrap="true">
                            </asp:Panel>
     </div>
     <div class="row" align="center">
     <asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnsave_Click" CssClass="btn btn-primary"
                                Height="30px" Width="8%" style="text-align:left;" />
                            <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; margin-right:2%; vertical-align: sub;
                                color: #fff;">
                            </label>
                            <asp:Button ID="btncomplete" runat="server" Text="Data Quality Check" OnClick="btncomplete_Click"
                                CssClass="btn btn-primary" Height="30px" Width="17%" style="text-align:left;"/>
                            <label class="glyphicon glyphicon-floppy-saved" style="margin-left: -3%; margin-right:2%; vertical-align: sub;
                                color: #fff;">
                            </label>
                            <asp:Button ID="btnback" runat="server" Text="Close" OnClick="btnback_Click" CssClass="btn btn-primary"
                                Height="30px" Width="8%" style="text-align:left;"/>
                            <label class="glyphicon glyphicon-remove-circle" style="margin-left: -3%; margin-right:2%; vertical-align: sub;
                                color: #fff;">
                            </label>
                            <asp:Button ID="btnPrint" Text="Print" runat="server" OnClientClick="WindowPrint()"
                                CssClass="btn btn-primary" Height="30px" Width="8%" style="text-align:left;"/>
                            <label class="glyphicon glyphicon-print" style="margin-left: -3%; vertical-align: sub; color: #fff;">
                            </label>
     </div>
     <br />
    </div>
    </div>
      </div>
      </div>
      </div>
     
     
    

    </div>
</asp:Content>
