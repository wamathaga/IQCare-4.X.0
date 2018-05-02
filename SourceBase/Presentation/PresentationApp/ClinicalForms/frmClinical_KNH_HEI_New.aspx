<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    CodeBehind="frmClinical_KNH_HEI_New.aspx.cs" Inherits="PresentationApp.ClinicalForms.frmClinical_KNH_HEI_New" %>

<%--<%@ Register TagPrefix="UcVitalSign" TagName="Uc1" Src="~/ClinicalForms/UserControl/UserControlKNH_VitalSigns.ascx" %>
<%@ Register TagPrefix="UcHEIPrescomplaints" TagName="UcPC" Src="~/ClinicalForms/UserControl/UserControlKNHPresentingComplaints.ascx" %>--%>
<%@ Register TagPrefix="UcNextAppointment" TagName="UcNxtAppt" Src="~/ClinicalForms/UserControl/UserControlKNH_NextAppointment.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="act" Namespace="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
 <script src="<%=ResolveUrl("~/Incl/Common.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>"
        type="text/javascript"></script>
 <style type="text/css">
        .checkbox
        {
            padding-left: 20px;            
            
        }
        .checkbox label
        {
            display: inline-block;
            vertical-align: middle;
            /*position: relative;*/
            padding-left: -15px;
        }
        .checkbox label::before
        {
            content: "";
            display: inline-block;
            /*position: absolute;*/
            width: 17px;
            height: 17px;
            left: 0;
            margin-bottom:-3px;
            margin-left: -10px;
            border: 1px solid #cccccc;
            border-radius: 3px;
            background-color: #fff;
            -webkit-transition: border 0.15s ease-in-out, color 0.15s ease-in-out;
            -o-transition: border 0.15s ease-in-out, color 0.15s ease-in-out;
            transition: border 0.15s ease-in-out, color 0.15s ease-in-out;
        }
        .checkbox label::after
        {
            display: inline-block;
            position: absolute;
            width: 16px;
            height: 16px;
            left: 0;
            top: 0;
            margin-left: -20px;
            padding-left: 3px;
            padding-top: 1px;
            font-size: 11px;
            color: #555555;
        }
        .checkbox input[type="checkbox"]
        {
            opacity: 0;
            z-index: 1;
        }
        .checkbox input[type="checkbox"]:checked + label::after
        {
            font-family: "FontAwesome";
            content: "\f00c";
        }
         
        .checkbox-primary input[type="checkbox"]:checked + label::before
        {
            background-color: #337ab7;
            border-color: #337ab7;
        }
        .checkbox-primary input[type="checkbox"]:checked + label::after
        {
            color: #fff;
        }
    </style>
    <script type="text/javascript">
        function RegisterJQuery() {
            $('#txtVisitDate').datepicker({ autoclose: true });
            $('#txttestresultsgiven').datepicker({ autoclose: true });
            $('#txtresultmothergiven').datepicker({ autoclose: true });
            $('#txtDateImmunised').datepicker({ autoclose: true });
//            $('#txtTreatmentDate').datepicker({ autoclose: true });
            $('#txtDateAssessed').datepicker({ autoclose: true });
        }
        //Calling RegisterJQuery when document is ready (Page loaded first time)
        $(document).ready(RegisterJQuery);
        //Calling RegisterJQuery when the page is doing postback (asp.net)
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RegisterJQuery);
    </script>
    <script type="text/javascript" language="javascript">
        function fnPlaceofdelivery() {
            var e = document.getElementById("<%=ddlPlaceofDelivery.ClientID%>");
            var strtext = e.options[e.selectedIndex].text;            
            if (strtext == "Other facility") {
                show('spnotherfacility');
                hide('spanotherdelivery');
            }
            else if (strtext == "Other Specify") {
                hide('spnotherfacility');
                show('spanotherdelivery');
            }
            else {
                hide('spnotherfacility');
                hide('spanotherdelivery');
            }
        }
        function fnEnableDisableControl() {
            var e = document.getElementById("<%=ddlReferredFrom.ClientID%>");
            var myTxt = document.getElementById("<%=txtReferredFrom.ClientID%>");
            var strtext = e.options[e.selectedIndex].text;
            if (strtext == "Other Specify") {
                myTxt.disabled = false;
                myTxt.value = "";
            }
            else {
                myTxt.disabled = true;
                myTxt.value = "";
            }  
        }

        function fnotherARVProphylaxis() {
            var e = document.getElementById("<%=ddlARVProphylaxis.ClientID%>");
            var strtext = e.options[e.selectedIndex].text;
            if (strtext == "Other Specify") {
                show('spnotherARVProphy');
            }
            else {
                hide('spnotherARVProphy');
            }
        }

        function fnotherfeedingoption() {
            var e = document.getElementById("<%=ddlIfeedingoption.ClientID%>");
            var strtext = e.options[e.selectedIndex].text;
            if (strtext == "Other") {
                show('spnotherfeedingoption');
            }
            else {
                hide('spnotherfeedingoption');
            }
        }

        function fnANCmotherfollowup() {
            var e = document.getElementById("<%=ddlmothersANCFU.ClientID%>");
            var strtext = e.options[e.selectedIndex].text;
            if (strtext == "Other Facility") {
                show('spnANCFollowup');
            }
            else {
                hide('spnANCFollowup');
            }
        }
       
        function fnsetCollapseState() {
            var e = document.getElementById("<%=ddlVisitType.ClientID%>");
            var strtext = e.options[e.selectedIndex].text;
            if (strtext == "Select") {
                alert('Please select visit type');
            }
        }

        function changeTab() {
            var tabBehavior = $get('<%=tabControl.ClientID%>').control;
            //Set the Currently Visible Tab 
            tabBehavior.set_activeTabIndex(1);
            document.getElementById('<%=btnHIVHistorySave.ClientID%>').disabled = false;
            document.getElementById('<%=btncloseHIVHistory.ClientID%>').disabled = false;
        }

        function disable_enable_btnFind() {

            var radioYes = document.getElementById('<%=rdoMotherRegisYes.ClientID%>');
            var radioNo = document.getElementById('<%=rdoMotherRegisNo.ClientID%>');
            if (radioYes.checked == true || radioNo.checked == false) {
                document.getElementById('<%=btnFind.ClientID%>').style.visibility = 'visible';
                document.getElementById('<%=lblBtnFind.ClientID%>').style.visibility = 'visible';
            } else {
                document.getElementById('<%=btnFind.ClientID%>').style.visibility = 'hidden';
                document.getElementById('<%=lblBtnFind.ClientID%>').style.visibility = 'hidden';
            }
        }
        function disable_enable_Dropdown(urChkBox,urDiv) {
            var urChkBox1 = document.getElementById(urChkBox).checked;
            if (urChkBox1 == true) {
                document.getElementById(urDiv).style.display = "block";
            }
            else {              
                document.getElementById(urDiv).style.display = "none";
            }
        }
        function getCBValue(urChkBox,urHidenField) {
            var urChkBox1 = document.getElementById(urChkBox).checked;           
            if (urChkBox1 == true) {
                document.getElementById('<%=hfComplaints.ClientID%>').value = 1;
            }
            else {
                document.getElementById('<%=hfComplaints.ClientID%>').value = 0;
            }
        }
        function SetAnyComplaints() {
            //alert('T');
            if (document.getElementById('<%=hfComplaints.ClientID%>').value == "True") {
                document.getElementById('chkComplaints').checked = true;
            }
            else {
                document.getElementById('chkComplaints').checked = false;
            }
        }
        function GExaminationTrue(myChkBox,myDiv) {
            document.getElementById(myChkBox).checked = true;
            document.getElementById(myDiv).style.display = "block";          
        }
        function GExaminationFalse(myChkBox, myDiv) {
            document.getElementById(myChkBox).checked = false;
            document.getElementById(myDiv).style.display = "none";
        }
        function GExaminationBusinessRule() {
            var counter = 0;
            var CHK = "";
            var checkbox = "";
                    
            CHK = document.getElementById("<%=cblSkin.ClientID%>");
            checkbox = CHK.getElementsByTagName("input");            
            for (var i = 0; i < checkbox.length; i++) {
                if (checkbox[i].checked) {
                    counter++;
                }
            }
            if (counter > 0) {
                document.getElementById('chkSkin').checked = true;
                document.getElementById('divSkin').style.display = "block";   
            }
            else {
                document.getElementById('chkSkin').checked = false;
                document.getElementById('divSkin').style.display = "none";
            }
            //
            counter = 0;
            CHK = document.getElementById("<%=cblENT.ClientID%>");
            checkbox = CHK.getElementsByTagName("input");
            for (var i = 0; i < checkbox.length; i++) {
                if (checkbox[i].checked) {
                    counter++;
                }
            }
            if (counter > 0) {
                document.getElementById('chkENT').checked = true;
                document.getElementById('divENT').style.display = "block";
            }
            else {
                document.getElementById('chkENT').checked = false;
                document.getElementById('divENT').style.display = "none";
            }
            //
            counter = 0;
            CHK = document.getElementById("<%=cblChest.ClientID%>");
            checkbox = CHK.getElementsByTagName("input");
            for (var i = 0; i < checkbox.length; i++) {
                if (checkbox[i].checked) {
                    counter++;
                }
            }
            if (counter > 0) {
                document.getElementById('chkChest').checked = true;
                document.getElementById('divChest').style.display = "block";
            }
            else {
                document.getElementById('chkChest').checked = false;
                document.getElementById('divChest').style.display = "none";
            }
            //
            counter = 0;
            CHK = document.getElementById("<%=cblCVS.ClientID%>");
            checkbox = CHK.getElementsByTagName("input");
            for (var i = 0; i < checkbox.length; i++) {
                if (checkbox[i].checked) {
                    counter++;
                }
            }
            if (counter > 0) {
                document.getElementById('chkCVS').checked = true;
                document.getElementById('divCVS').style.display = "block";
            }
            else {
                document.getElementById('chkCVS').checked = false;
                document.getElementById('divCVS').style.display = "none";
            }
            //
            counter = 0;
            CHK = document.getElementById("<%=cblAbdomen.ClientID%>");
            checkbox = CHK.getElementsByTagName("input");
            for (var i = 0; i < checkbox.length; i++) {
                if (checkbox[i].checked) {
                    counter++;
                }
            }
            if (counter > 0) {
                document.getElementById('chkAbdomen').checked = true;
                document.getElementById('divAbdomen').style.display = "block";
            }
            else {
                document.getElementById('chkAbdomen').checked = false;
                document.getElementById('divAbdomen').style.display = "none";
            }
            //
            counter = 0;
            CHK = document.getElementById("<%=cblCNS.ClientID%>");
            checkbox = CHK.getElementsByTagName("input");
            for (var i = 0; i < checkbox.length; i++) {
                if (checkbox[i].checked) {
                    counter++;
                }
            }
            if (counter > 0) {
                document.getElementById('chkCNS').checked = true;
                document.getElementById('divCNS').style.display = "block";
            }
            else {
                document.getElementById('chkCNS').checked = false;
                document.getElementById('divCNS').style.display = "none";
            }
        }
    </script>
    <script type="text/javascript">

        function CalcualteBMI(age, txtWeight, txtHeight, txtBMI, lblBMIClassification) {
            var weight = document.getElementById(txtWeight).value;
            var height = document.getElementById(txtHeight).value;
            if (weight == "" || height == "") {
                weight = 0;
                height = 0;
                document.getElementById(txtBMI).value = "";
            }
            else {
                var BMI = weight / ((height / 100) * (height / 100));
                BMI = BMI.toFixed(2);
                document.getElementById(txtBMI).value = BMI;

                if (age > 15) {
                    if (BMI < 18.5) {
                        //underweight
                        document.getElementById(lblBMIClassification).innerHTML = 'UnderWeight';
                        document.getElementById(lblBMIClassification).style.color = "red";
                        document.getElementById(txtBMI).style.color = "red";
                        document.getElementById(lblBMIClassification).style.fontWeight = 'bold';
                    }

                    if (BMI >= 18.5 && BMI < 25) {
                        document.getElementById(lblBMIClassification).innerHTML = 'Normal';
                        document.getElementById(lblBMIClassification).style.color = "black";
                        document.getElementById(txtBMI).style.color = "black";
                    }

                    if (BMI > 25 && BMI < 30) {
                        //OverWeight
                        document.getElementById(lblBMIClassification).innerHTML = 'OverWeight';
                        document.getElementById(lblBMIClassification).style.color = "red";
                        document.getElementById(txtBMI).style.color = "red";
                        document.getElementById(lblBMIClassification).style.fontWeight = 'bold';
                    }

                    if (BMI >= 30) {
                        //Obese
                        document.getElementById(lblBMIClassification).innerHTML = 'Obese';
                        document.getElementById(lblBMIClassification).style.color = "red";
                        document.getElementById(txtBMI).style.color = "red";
                        document.getElementById(lblBMIClassification).style.fontWeight = 'bold';
                    }
                }
            }
        }

        function CalculateZScores() {

        }




        function HighLightAbnormalValuesAdults(txtTemp, txtRR, txtHR, txtBPSystolic, txtBPDiastolic) {
            //temperature
            var tempValue = document.getElementById(txtTemp).value;
            var temp = document.getElementById(txtTemp);
            if (tempValue < 36.1 || tempValue > 37.8) {
                temp.style.color = "red";
                temp.style.fontWeight = 'bold';
                //temp.style.fontSize = "large"
            }
            else {
                temp.style.color = "black";
                temp.style.fontWeight = 'normal';
            }

            //Respiratory Rate
            var RRValue = document.getElementById(txtRR).value;
            var RR = document.getElementById(txtRR);
            if (RRValue < 16 || RRValue > 18) {
                RR.style.color = "red";
                temp.style.fontWeight = 'bold';
                //temp.style.fontSize = "large"
            }
            else {
                RR.style.color = "black";
                temp.style.fontWeight = 'normal';
            }

            //Heart Rate
            var HRValue = document.getElementById(txtHR).value;
            var HR = document.getElementById(txtHR);
            if (HRValue < 60 || HRValue > 100) {
                HR.style.color = "red";
                temp.style.fontWeight = 'bold';
                //temp.style.fontSize = "large"
            }
            else {
                HR.style.color = "black";
                temp.style.fontWeight = 'normal';
            }

            //Systolic
            var SystolicValue = document.getElementById(txtBPSystolic).value;
            var Systolic = document.getElementById(txtBPSystolic);
            if (SystolicValue < 90 || SystolicValue > 120) {
                Systolic.style.color = "red";
                temp.style.fontWeight = 'bold';
                //temp.style.fontSize = "large"
            }
            else {
                Systolic.style.color = "black";
                temp.style.fontWeight = 'normal';
            }

            //Diastolic
            var DiastolicV = document.getElementById(txtBPDiastolic);
            if (DiastolicV != null) {
                var DiastolicValue = document.getElementById(txtBPDiastolic).value;
                var Diastolic = document.getElementById(txtBPDiastolic);
                if (DiastolicValue < 60 || DiastolicValue > 80) {
                    Diastolic.style.color = "red";
                    temp.style.fontWeight = 'bold';
                    //temp.style.fontSize = "large"
                }
                else {
                    Diastolic.style.color = "black";
                    temp.style.fontWeight = 'normal';
                }
            }
        }


        function HighLightAbnormalValuesPeads(age, txtTemp, txtRR, txtHR, txtBPSystolic, txtBPDiastolic) {
            //alert(age);
            //var age = '<%= Session["patientageinyearmonth"] %>';
            //temperature
            var tempValue = document.getElementById(txtTemp).value;
            var temp = document.getElementById(txtTemp);
            if (tempValue < 36.1 || tempValue > 37.8) {
                temp.style.color = "red";
                temp.style.fontWeight = 'bold';
                //temp.style.fontSize = "large"
            }
            else {
                temp.style.color = "black";
                temp.style.fontWeight = 'normal';
            }

            //Respiratory Rate
            var RRValue = document.getElementById(txtRR).value;
            var RR = document.getElementById(txtRR);
            if (age >= 0.0 || age <= 0.167) {
                if (RRValue < 35 || RRValue > 60) {
                    RR.style.color = "red";
                    RR.style.fontWeight = 'bold';
                    //temp.style.fontSize = "large"
                }
                else {
                    RR.style.color = "black";
                    RR.style.fontWeight = 'normal';
                }
            }

            if (age > 0.167 || age <= 0.99) {
                if (RRValue < 30 || RRValue > 50) {
                    RR.style.color = "red";
                    RR.style.fontWeight = 'bold';
                    //temp.style.fontSize = "large"
                }
                else {
                    RR.style.color = "black";
                    RR.style.fontWeight = 'normal';
                }
            }

            if (age > 0.99 || age <= 2.0) {
                if (RRValue < 24 || RRValue > 40) {
                    RR.style.color = "red";
                    RR.style.fontWeight = 'bold';
                    //temp.style.fontSize = "large"
                }
                else {
                    RR.style.color = "black";
                    RR.style.fontWeight = 'normal';
                }
            }

            if (age > 2.0 || age <= 5.0) {
                if (RRValue < 22 || RRValue > 34) {
                    RR.style.color = "red";
                    RR.style.fontWeight = 'bold';
                    //temp.style.fontSize = "large"
                }
                else {
                    RR.style.color = "black";
                    RR.style.fontWeight = 'normal';
                }
            }

            if (age > 5.0 || age <= 12.0) {
                if (RRValue < 18 || RRValue > 30) {
                    RR.style.color = "red";
                    RR.style.fontWeight = 'bold';
                    //temp.style.fontSize = "large"
                }
                else {
                    RR.style.color = "black";
                    RR.style.fontWeight = 'normal';
                }
            }

            if (age > 12.0) {
                if (RRValue < 12 || RRValue > 16) {
                    RR.style.color = "red";
                    RR.style.fontWeight = 'bold';
                    //temp.style.fontSize = "large"
                }
                else {
                    RR.style.color = "black";
                    RR.style.fontWeight = 'normal';
                }
            }


            //Heart Rate
            var HRValue = document.getElementById(txtHR).value;
            var HR = document.getElementById(txtHR);
            if (age >= 0 || age <= 0.167) {
                if (HRValue < 100 || HRValue > 160) {
                    HR.style.color = "red";
                    HR.style.fontWeight = 'bold';
                    //temp.style.fontSize = "large"
                }
                else {
                    HR.style.color = "black";
                    HR.style.fontWeight = 'normal';
                }
            }

            if (age > 0.167 || age <= 0.99) {
                if (HRValue < 100 || HRValue > 160) {
                    HR.style.color = "red";
                    HR.style.fontWeight = 'bold';
                    //temp.style.fontSize = "large"
                }
                else {
                    HR.style.color = "black";
                    HR.style.fontWeight = 'normal';
                }
            }

            if (age > 0.99 || age <= 2) {
                if (HRValue < 90 || HRValue > 150) {
                    HR.style.color = "red";
                    HR.style.fontWeight = 'bold';
                    //temp.style.fontSize = "large"
                }
                else {
                    HR.style.color = "black";
                    HR.style.fontWeight = 'normal';
                }
            }

            if (age > 2 || age <= 5) {
                if (HRValue < 80 || HRValue > 140) {
                    HR.style.color = "red";
                    HR.style.fontWeight = 'bold';
                    //temp.style.fontSize = "large"
                }
                else {
                    HR.style.color = "black";
                    HR.style.fontWeight = 'normal';
                }
            }

            if (age > 5.0 || age <= 12) {
                if (HRValue < 70 || HRValue > 120) {
                    HR.style.color = "red";
                    HR.style.fontWeight = 'bold';
                    //temp.style.fontSize = "large"
                }
                else {
                    HR.style.color = "black";
                    HR.style.fontWeight = 'normal';
                }
            }

            if (age > 12) {
                if (HRValue < 60 || HRValue > 100) {
                    HR.style.color = "red";
                    HR.style.fontWeight = 'bold';
                    //temp.style.fontSize = "large"
                }
                else {
                    HR.style.color = "black";
                    HR.style.fontWeight = 'normal';
                }
            }




            //Systolic
            var SystolicValue = document.getElementById(txtBPSystolic).value;
            var Systolic = document.getElementById(txtBPSystolic);
            if (age >= 0 || age <= 0.25) {
                if (SystolicValue < 65 || SystolicValue > 85) {
                    Systolic.style.color = "red";
                    Systolic.style.fontWeight = 'bold';
                    //temp.style.fontSize = "large"
                }
                else {
                    Systolic.style.color = "black";
                    Systolic.style.fontWeight = 'normal';
                }
            }

            if (age > 0.25 || age <= 0.5) {
                if (SystolicValue < 70 || SystolicValue > 90) {
                    Systolic.style.color = "red";
                    Systolic.style.fontWeight = 'bold';
                    //temp.style.fontSize = "large"
                }
                else {
                    Systolic.style.color = "black";
                    Systolic.style.fontWeight = 'normal';
                }
            }

            if (age > 0.5 || age <= 1) {
                if (SystolicValue < 80 || SystolicValue > 100) {
                    Systolic.style.color = "red";
                    Systolic.style.fontWeight = 'bold';
                    //temp.style.fontSize = "large"
                }
                else {
                    Systolic.style.color = "black";
                    Systolic.style.fontWeight = 'normal';
                }
            }

            if (age > 1 || age <= 3) {
                if (SystolicValue < 90 || SystolicValue > 105) {
                    Systolic.style.color = "red";
                    Systolic.style.fontWeight = 'bold';
                    //temp.style.fontSize = "large"
                }
                else {
                    Systolic.style.color = "black";
                    Systolic.style.fontWeight = 'normal';
                }
            }

            if (age > 3 || age <= 6) {
                if (SystolicValue < 95 || SystolicValue > 110) {
                    Systolic.style.color = "red";
                    Systolic.style.fontWeight = 'bold';
                    //temp.style.fontSize = "large"
                }
                else {
                    Systolic.style.color = "black";
                    Systolic.style.fontWeight = 'normal';
                }
            }

            if (age > 6 || age <= 12) {
                if (SystolicValue < 100 || SystolicValue > 120) {
                    Systolic.style.color = "red";
                    Systolic.style.fontWeight = 'bold';
                    //temp.style.fontSize = "large"
                }
                else {
                    Systolic.style.color = "black";
                    Systolic.style.fontWeight = 'normal';
                }
            }

            if (age > 12) {
                if (SystolicValue < 110 || SystolicValue > 135) {
                    Systolic.style.color = "red";
                    Systolic.style.fontWeight = 'bold';
                    //temp.style.fontSize = "large"
                }
                else {
                    Systolic.style.color = "black";
                    Systolic.style.fontWeight = 'normal';
                }
            }


            //Diastolic
            var DiastolicV = document.getElementById(txtBPDiastolic);
            if (DiastolicV != null) {
                var DiastolicValue = document.getElementById(txtBPDiastolic).value;
                var Diastolic = document.getElementById(txtBPDiastolic);
                if (age > 0 || age <= 0.25) {
                    if (DiastolicValue < 45 || DiastolicValue > 55) {
                        Diastolic.style.color = "red";
                        Diastolic.style.fontWeight = 'bold';
                        //temp.style.fontSize = "large"
                    }
                    else {
                        Diastolic.style.color = "black";
                        Diastolic.style.fontWeight = 'normal';
                    }
                }

                if (age > 0.25 || age <= 0.5) {
                    if (DiastolicValue < 50 || DiastolicValue > 65) {
                        Diastolic.style.color = "red";
                        Diastolic.style.fontWeight = 'bold';
                        //temp.style.fontSize = "large"
                    }
                    else {
                        Diastolic.style.color = "black";
                        Diastolic.style.fontWeight = 'normal';
                    }
                }

                if (age > 0.5 || age <= 1) {
                    if (DiastolicValue < 55 || DiastolicValue > 65) {
                        Diastolic.style.color = "red";
                        Diastolic.style.fontWeight = 'bold';
                        //temp.style.fontSize = "large"
                    }
                    else {
                        Diastolic.style.color = "black";
                        Diastolic.style.fontWeight = 'normal';
                    }
                }

                if (age > 1 || age <= 3) {
                    if (DiastolicValue < 55 || DiastolicValue > 70) {
                        Diastolic.style.color = "red";
                        Diastolic.style.fontWeight = 'bold';
                        //temp.style.fontSize = "large"
                    }
                    else {
                        Diastolic.style.color = "black";
                        Diastolic.style.fontWeight = 'normal';
                    }
                }

                if (age > 3 || age <= 6) {
                    if (DiastolicValue < 60 || DiastolicValue > 75) {
                        Diastolic.style.color = "red";
                        Diastolic.style.fontWeight = 'bold';
                        //temp.style.fontSize = "large"
                    }
                    else {
                        Diastolic.style.color = "black";
                        Diastolic.style.fontWeight = 'normal';
                    }
                }

                if (age > 6 || age <= 12) {
                    if (DiastolicValue < 60 || DiastolicValue > 75) {
                        Diastolic.style.color = "red";
                        Diastolic.style.fontWeight = 'bold';
                        //temp.style.fontSize = "large"
                    }
                    else {
                        Diastolic.style.color = "black";
                        Diastolic.style.fontWeight = 'normal';
                    }
                }

                if (age > 12) {
                    if (DiastolicValue < 65 || DiastolicValue > 85) {
                        Diastolic.style.color = "red";
                        Diastolic.style.fontWeight = 'bold';
                        //temp.style.fontSize = "large"
                    }
                    else {
                        Diastolic.style.color = "black";
                        Diastolic.style.fontWeight = 'normal';
                    }
                }

            }
        }

        function show_hide(controlID, status) {
            var s = document.getElementById(controlID);
            if (s != null) {
                if (status == "notvisible") {
                    s.style.display = "none";
                }
                else {
                    s.style.display = "block";
                }
            }
        }

        function TriageCheckBoxHideUnhideOtherSpecialistClinic(strcblcontrolId, txtControlId) {

            var checkList = document.getElementById(strcblcontrolId);
            var checkBoxList = checkList.getElementsByTagName("input");
            var arrayOfCheckBoxLabels = checkList.getElementsByTagName("label");
            var checkBoxSelectedItems = new Array();

            for (var i = 0; i < checkBoxList.length; i++) {

                if (checkBoxList[i].checked) {
                    if (arrayOfCheckBoxLabels[i].innerHTML == 'Other Specialist Clinic') {
                        //alert(arrayOfCheckBoxLabels[i].innerHTML + ' show ' + strcblcontrolId);
                        ShowHide('TriagedivReferToSpecialistClinic', "show");
                        return;
                    }
                    else {
                        //alert(arrayOfCheckBoxLabels[i].innerHTML + ' hide ' + strcblcontrolId);
                        document.getElementById(txtControlId).value = '';
                        ShowHide('TriagedivReferToSpecialistClinic', "hide");
                    }
                }
                else {
                    //alert(arrayOfCheckBoxLabels[i].innerHTML + ' hide ' + strcblcontrolId);
                    document.getElementById(txtControlId).value = '';
                    ShowHide('TriagedivReferToSpecialistClinic', "hide");
                }
            }
        }

        function TriageCheckBoxHideUnhideOtherReferral(strcblcontrolId, txtControlId) {

            var checkList = document.getElementById(strcblcontrolId);
            var checkBoxList = checkList.getElementsByTagName("input");
            var arrayOfCheckBoxLabels = checkList.getElementsByTagName("label");
            var checkBoxSelectedItems = new Array();

            for (var i = 0; i < checkBoxList.length; i++) {

                if (checkBoxList[i].checked) {
                    if (arrayOfCheckBoxLabels[i].innerHTML == 'Other (Specify)') {
                        //alert(arrayOfCheckBoxLabels[i].innerHTML + ' show ' + strcblcontrolId);
                        ShowHide('TriagedivReferToOther', "show");
                        return;
                    }
                    else {
                        //alert(arrayOfCheckBoxLabels[i].innerHTML + ' hide ' + strcblcontrolId);
                        document.getElementById(txtControlId).value = '';
                        ShowHide('TriagedivReferToOther', "hide");
                    }
                }
                else {
                    //alert(arrayOfCheckBoxLabels[i].innerHTML + ' hide ' + strcblcontrolId);
                    document.getElementById(txtControlId).value = '';
                    ShowHide('TriagedivReferToOther', "hide");
                }
            }
        }

        function ShowHide(theDiv, YN, theFocus) {

            $(document).ready(function () {

                if (YN == "show") {
                    //                    $("#" + theDiv).slideDown();
                    $("#" + theDiv).show();

                }
                if (YN == "hide") {
                    //                    $("#" + theDiv).slideUp();
                    $("#" + theDiv).hide();


                }

            });

        }

    </script>
      <script type="text/javascript">
          $(document).ready(function () { $("[name='switch-size']").bootstrapSwitch(); });
    </script>
    <div class="content-wrapper">
        <div class="box-body">
            <div class="row">
                <div class="col-xs-12">
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            <h3 class="box-title">
                                HEI Form</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body table-responsive no-padding" style="overflow: hidden; margin-left: 5px;">
                            <div class="row" style="margin-left: 2px;">
                                <br />
                                <act:TabContainer ID="tabControl" runat="server" CssClass="ajax__myTab" ActiveTabIndex="0"
                                    Width="100%" OnActiveTabChanged="tabControl_ActiveTabChanged" AutoPostBack="true">
                                    <act:TabPanel ID="TabPnlTriage" runat="server" HeaderText="Triage and Neonatal History">
                                        <ContentTemplate>
                                            <div style="margin-left: 8px;">
                                                <div class="row">
                                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group text-right">
                                                        <label for="inputEmail3" class="control-label required">
                                                            Visit date:</label>
                                                    </div>
                                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                        <div class="form-group">
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <i class="fa fa-calendar"></i>
                                                                </div>
                                                                <input type="text" class="form-control pull-left" id="txtVisitDate" clientidmode="Static"
                                                                    onblur="DateFormat(this,this.value,event,false,3)" onkeyup="DateFormat(this,this.value,event,false,3);"
                                                                    onfocus="javascript:vDateType='3'" maxlength="11" size="11" runat="server" data-date-format="dd-M-yyyy"
                                                                    style="width: 120px;" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-1 col-sm-12 col-xs-12 form-group text-right">
                                                        <label for="inputEmail3" class="control-label required">
                                                            Scheduled</label>
                                                    </div>
                                                    <div class="col-md-1 col-sm-12 col-xs-12 form-group text-left">
                                                        <label for="inputEmail3" class="control-label required">
                                                            <asp:CheckBox ID="cbScheduled" runat="server" Checked="true" />
                                                        </label>
                                                    </div>
                                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group text-right">
                                                        <label for="inputEmail3" class="control-label required">
                                                            Visit Type:</label>
                                                    </div>
                                                    <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                        <asp:DropDownList ID="ddlVisitType" runat="server" OnSelectedIndexChanged="ddlVisitType_SelectedIndexChanged"
                                                            AutoPostBack="true" CssClass="form-control" Width="95%">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                                    </div>
                                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                        <label for="inputEmail3" class="control-label">
                                                            Duration since ART start</label>
                                                    </div>
                                                    <div class="col-md-2 col-sm-1 col-xs-12 form-group">
                                                        <asp:TextBox ID="txtARTStart" runat="server" CssClass="form-control" Width="50%"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group text-left" style="margin-left: -100px;
                                                        padding-top: 8px;">
                                                        Months</div>
                                                </div>
                                                <div class="box box-primary">
                                                    <div class="box-header with-border">
                                                        <h3 class="box-title">
                                                            Visit Details</h3>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                            <label for="inputEmail3" class="control-label">
                                                                Referred From (Place of first diagnosis)</label>
                                                        </div>
                                                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                            <asp:DropDownList ID="ddlReferredFrom" runat="server" onchange="fnEnableDisableControl();" CssClass="form-control" Width="95%">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                         <asp:TextBox ID="txtReferredFrom" runat="server" Enabled="false" Width="99%" CssClass="form-control" placeholder="Referred From"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="box box-primary">
                                                    <div class="box-header with-border">
                                                        <h3 class="box-title">
                                                            Anthropometric Measurments</h3>
                                                    </div>
                                                    <div class="row">
                                                        <br />
                                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group text-right">
                                                            <label for="inputEmail3" class="control-label">
                                                                <asp:Label ID="lblWA" runat="server" Text="Height/Length(cm):"></asp:Label>
                                                               </label>
                                                               <asp:UpdatePanel ID="UpdatePanelBMIz" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Label ID="Label22" runat="server" Font-Bold="True"></asp:Label>
                                                        &nbsp;
                                                        <asp:Label ID="lblBMIzClassification" runat="server" Font-Bold="True"></asp:Label>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtHeight" EventName="TextChanged" />
                                                    </Triggers>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtWeight" EventName="TextChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtHeight" placeholder="Height/Length(cm)" OnTextChanged="txtHeight_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-1 col-sm-12 col-xs-12 form-group text-right">
                                                            <label for="inputEmail3" class="control-label">
                                                            <asp:Label ID="lblWH" runat="server" Text="Weight(Kg):"></asp:Label>
                                                                </label>
                                                                <asp:UpdatePanel ID="UpdatePanelWA" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Label ID="Label3" runat="server" Font-Bold="True"></asp:Label>
                                                        &nbsp;
                                                        <asp:Label ID="lblWAClassification" runat="server" Font-Bold="True"></asp:Label>
                                                        &nbsp;
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtHeight" EventName="TextChanged" />
                                                    </Triggers>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtWeight" EventName="TextChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtWeight" OnTextChanged="txtWeight_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-1 col-sm-12 col-xs-12 form-group text-right">
                                                            <label for="inputEmail3" class="control-label">
                                                                <asp:Label ID="lblBMIz" runat="server" Text="BMI: "></asp:Label>
                                                            </label>
                                                             
                                                        </div>
                                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtBMI" Enabled="false"></asp:TextBox>
                                                            <asp:Label ID="lblBMIClassification" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group text-right">
                                                            <label for="inputEmail3" class="control-label">
                                                                Head circumference(cm)::</label>
                                                                 
                                                        </div>
                                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtheadcircumference" Width="90%"></asp:TextBox>
                                                            <asp:UpdatePanel ID="UpdatePanelWH" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Label ID="Label21" runat="server" Font-Bold="True"></asp:Label>
                                                        &nbsp;
                                                        <asp:Label ID="lblWHClassification" runat="server" Font-Bold="True"></asp:Label>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtHeight" EventName="TextChanged" />
                                                    </Triggers>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtWeight" EventName="TextChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                                        </div>
                                                        <%-- <UcVitalSign:Uc1 ID="idVitalSign" runat="server" />--%>
                                                    </div>
                                                    
                                                    <div class="row">
                                                        <div class="col-md-6 col-sm-12 col-xs-12 form-group text-right">
                                                        <asp:Label ID="lblWALabel" runat="server" Text="Weight for age:" CssClass="control-label"></asp:Label>
                                                        </div>
                                                        <div class="col-md-6 col-sm-12 col-xs-12 form-group text-left">
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Label ID="Label24" runat="server" Font-Bold="True"></asp:Label>
                                                        &nbsp;
                                                        <asp:Label ID="Label25" runat="server" Font-Bold="True"></asp:Label>
                                                        &nbsp;
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtHeight" EventName="TextChanged" />
                                                    </Triggers>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtWeight" EventName="TextChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                                        </div>
                                                        </div>
                                                         <div class="row">
                                                         <div class="col-md-6 col-sm-12 col-xs-12 form-group text-right">
                                                         <asp:Label ID="lblWHLabel" runat="server" Text="Weight for height:" CssClass="control-label"></asp:Label>
                                                         </div>
                                                         <div class="col-md-6 col-sm-12 col-xs-12 form-group text-left">
                                                         <asp:Label ID="lblBMIzLabel" runat="server" CssClass="control-label" Text="BMI ZScore:"></asp:Label>

                                                         </div>
                                                         </div>
                                                        <div class="row">
                                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group text-right">
                                                            <label for="inputEmail3" class="control-label">
                                                                MUAC(cm):</label>
                                                        </div>
                                                        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtMUAC"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="box box-primary">
                                                    <div class="box-header with-border">
                                                        <h3 class="box-title">
                                                            Vital Signs</h3>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group text-right">
                                                            <label for="inputEmail3" class="control-label">
                                                                <asp:Label ID="lblBP" runat="server" CssClass="required" Font-Bold="True" Text="BP(mm/Hg):"></asp:Label>
                                                            </label>
                                                        </div>
                                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                            <div class="row">
                                                                <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                                    <asp:TextBox ID="txtBPSystolic" runat="server" Width="70px" CssClass="form-control"></asp:TextBox></div>
                                                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                                    /</div>
                                                                <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                                    <asp:TextBox ID="txtBPDiastolic" runat="server" Width="70px" CssClass="form-control"></asp:TextBox></div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group text-right">
                                                            <label for="inputEmail3" class="control-label">
                                                                <asp:Label ID="lblTemp" runat="server" Font-Bold="True" Text="Temperature(C):"></asp:Label>
                                                            </label>
                                                        </div>
                                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                            <asp:TextBox ID="txtTemp" runat="server" Width="90%" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group text-right">
                                                            <label for="inputEmail3" class="control-label">
                                                                Respiratory Rate(bpm):
                                                            </label>
                                                        </div>
                                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                            <asp:TextBox ID="txtRR" runat="server" Width="90%" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group text-right">
                                                            <label for="inputEmail3" class="control-label">
                                                                Heart Rate (bpm):
                                                            </label>
                                                        </div>
                                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                            <asp:TextBox ID="txtHR" runat="server" Width="80px" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-1 col-sm-12 col-xs-12 form-group text-right">
                                                            <label for="inputEmail3" class="control-label">
                                                                SPO2 (%):
                                                            </label>
                                                        </div>
                                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                            <asp:TextBox ID="txtSPO2" runat="server" Width="80px" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="row" id="trNursecomments" runat="server">
                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                            <label for="inputEmail3" class="control-label">
                                                                Nurse comments:
                                                            </label>
                                                            <asp:TextBox ID="txtnursescomments" TextMode="MultiLine" Width="99%" runat="server"
                                                                Rows="4" Style="resize: none" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="box box-primary11">
                                                    <div class="box-header with-border" style="background-color:#DCDCDC;">
                                                        <h3 class="box-title">
                                                            Birth, Feeding Options and ARV Prophylaxis
                                                        </h3>
                                                    </div>
                                                    <div class="row">
                                                        <br />
                                                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                            <label for="inputEmail3" class="control-label">
                                                                Place of Delivery:</label>
                                                        </div>
                                                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                            <asp:DropDownList ID="ddlPlaceofDelivery" onchange="fnPlaceofdelivery();" runat="server"
                                                                CssClass="form-control">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                                                        <div class="row">
                                                    <span id="spnotherfacility" style="display: none">
                                                        <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                                                            <label for="inputEmail3" class="control-label" id="Label2" runat="server">
                                                                Other Facility:</label>
                                                        </div>
                                                        <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                                                            <asp:TextBox ID="txtOtherFacility" runat="server" MaxLength="45" Columns="45" CssClass="form-control" Width="90%"></asp:TextBox>
                                                        </div>
                                                    </span><span id="spanotherdelivery" style="display: none">
                                                        <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                                                            <label for="inputEmail3" class="control-label" id="Label4" runat="server">
                                                                Specify Other:</label>
                                                        </div>
                                                        <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                                                            <asp:TextBox ID="txtOtherDelivery" runat="server" MaxLength="45" Columns="45" CssClass="form-control" Width="90%"></asp:TextBox>
                                                        </div>
                                                    </span>
                                                </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                
                                                <div class="row">
                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                        <label for="inputEmail3" class="control-label">
                                                            <label id="Label32" runat="server" for="inputEmail3" class="control-label required">
                                                                Mode of delivery:</label></label>
                                                    </div>
                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                        <asp:DropDownList ID="ddlModeofDelivery" runat="server" CssClass="form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                        <label id="Label33" runat="server" for="inputEmail3" class="control-label required">
                                                            Birth Weight (kgs):</label>
                                                    </div>
                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                        <div style="width: 100%; margin: 0px auto;">
                                                            <div style="float: left">
                                                                <asp:TextBox ID="txtBirthWeight" runat="server" MaxLength="3" Columns="4" CssClass="form-control"
                                                                    Width="80"></asp:TextBox></div>
                                                           
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                        <label id="Label34" runat="server" for="inputEmail3" class="control-label required">
                                                            ARV Prophylaxis:</label>
                                                    </div>
                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                        <asp:DropDownList ID="ddlARVProphylaxis" CssClass="form-control" onchange="fnotherARVProphylaxis();"
                                                            runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                        <label id="Label35" runat="server" for="inputEmail3" class="control-label required">
                                                            Infant feeding option:</label>
                                                    </div>
                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                        <asp:DropDownList ID="ddlIfeedingoption" onchange="fnotherfeedingoption();" runat="server"
                                                            CssClass="form-control" Width="90%">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <span id="Span3" style="display: none">
                                                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                            <label id="Label36" for="inputEmail3" class="control-label" style="margin-left: 51px;"
                                                                runat="server">
                                                                Specify Other:</label>
                                                        </div>
                                                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                            <asp:TextBox ID="txtOtherARVProphylaxis" runat="server" MaxLength="25" Columns="25"
                                                                CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </span><span id="Span4" style="display: none">
                                                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                            <label id="Label37" for="inputEmail3" class="control-label" style="margin-left: 10px;"
                                                                runat="server">
                                                                Specify Other:</label>
                                                        </div>
                                                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                            <asp:TextBox ID="txtOtherFeedingoption" runat="server" MaxLength="25" Columns="25"
                                                                CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </span>
                                                </div>
                                                <div class="row">
                                                    <div style="margin-left: 20px;">
                                                        <table border="0" width="100%">
                                                            <caption style="font-weight: bold; text-align: left;">
                                                                Infant Available Labs</caption>
                                                            <tr>
                                                                <td style="margin-left: 20px;">
                                                                    <div class="row">
                                                                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                            <label id="Label38" runat="server" for="inputEmail3" class="control-label">
                                                                                Type of Test:</label>
                                                                        </div>
                                                                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                            <asp:DropDownList ID="ddlTypeofTest" runat="server" CssClass="form-control" Width="90%">
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                            <label id="Label39" runat="server" for="inputEmail3" class="control-label">
                                                                                Results:</label>
                                                                        </div>
                                                                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                            <asp:DropDownList ID="ddlTestResults" runat="server" CssClass="form-control" Width="90%">
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="margin-left: 20px;">
                                                                    <div class="row">
                                                                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                            <label id="Label40" runat="server" for="inputEmail3" class="control-label margin35">
                                                                                Date results given to guardian:</label>
                                                                        </div>
                                                                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                            <div class="form-group">
                                                                                <div class="input-group date">
                                                                                    <div class="input-group-addon">
                                                                                        <i class="fa fa-calendar"></i>
                                                                                    </div>
                                                                                    <input type="text" class="form-control pull-left" id="txttestresultsgiven" clientidmode="Static"
                                                                                        maxlength="11" size="11" runat="server" data-date-format="dd-M-yyyy" style="width: 120px;"
                                                                                        onblur="DateFormat(this,this.value,event,false,3)" onfocus="javascript:vDateType='3'"
                                                                                        onkeyup="DateFormat(this,this.value,event,false,3);" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                            <label id="Label41" runat="server" for="inputEmail3" class="control-label margin50">
                                                                                Comments:</label>
                                                                        </div>
                                                                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                            <asp:TextBox ID="txtcomments" runat="server" Columns="30" MaxLength="30" CssClass="form-control"
                                                                                Width="90%"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                                <div class="row col-sm-12 col-xs-12 form-group" align="center">
                                                    <div id="divbtnPriorART" align="center" class="whitebg">
                                                        <asp:Button ID="btnAddNNatal" runat="server" OnClick="btnAddNNatal_Click" Text="Add"
                                                            CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                                                        <label class="glyphicon glyphicon-open" style="margin-left: -3%; vertical-align: sub;
                                                            color: #fff;">
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="row" align="center">
                                                    <div id="divDrugAllergyMedicalAlr" class="grid" style="width: 95%; margin-right: 1%">
                                                        <div class="rounded">
                                                            <div class="top-outer">
                                                                <div class="top-inner">
                                                                    <div class="top">
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="mid-outer">
                                                                <div class="mid-inner">
                                                                    <div class="mid" style="height: 100px; overflow: auto">
                                                                        <div id="div-gridview" class="GridView whitebg">
                                                                            <asp:GridView ID="GrdNNHistory" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                                BorderWidth="1px" CellPadding="0" CssClass="table table-bordered table-hover"
                                                                                GridLines="None" Height="25px" OnRowDeleting="GrdNNHistory_RowDeleting" Width="98%">
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="bottom-outer">
                                                                <div class="bottom-inner">
                                                                    <div class="bottom">
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="border center formbg">
                                                <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
                                                    <tr>
                                                        <td colspan="2" align="left">
                                                        <%--onclick="fnsetCollapseState();"--%>
                                                            <asp:Panel ID="pnl1MHistory"  CssClass="border center formbg"
                                                                runat="server" Style="padding: 6px">
                                                                <h5 class="forms" align="left">
                                                                    <asp:ImageButton ID="imgDAToxities" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                                    Maternal History</h5>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="left">
                                                            <asp:Panel ID="pnl2MHistory" runat="server" Style="overflow: hidden;">
                                                                <div id="MHistory" style="display: block">
                                                                    <div class="row">
                                                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                                            <label id="Label15" runat="server" class="control-label required">
                                                                                State of mother:</label>
                                                                        </div>
                                                                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                            <asp:DropDownList ID="ddlStateofMother" runat="server" Width="95%" CssClass="form-control">
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                            <label id="Label12" runat="server" class="control-label required">
                                                                                Mother Registered at this Clinic:</label>
                                                                        </div>
                                                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                                            <input id="rdoMotherRegisYes" type="radio" value="Yes" runat="server" name="MotherRegister"
                                                                                onclick="disable_enable_btnFind()" />
                                                                            <label for="rdoMotherRegisYes">
                                                                                Yes</label>
                                                                            <input id="rdoMotherRegisNo" runat="server" name="MotherRegister" type="radio" value="No"
                                                                                onclick="disable_enable_btnFind()" />
                                                                            <label for="rdoMotherRegisNo">
                                                                                No</label>
                                                                        </div>
                                                                        <div class="col-md-2">
                                                                            <asp:Button ID="btnFind" Text="Find Patient" runat="server" Width="85%" CssClass="btn btn-primary"
                                                                                Height="30px" Style="text-align: left; visibility: hidden;" OnClick="btnFind_Click" />
                                                                            <label id="lblBtnFind" runat="server" class="glyphicon glyphicon-search" style="vertical-align: sub;
                                                                                margin-left: -20%; color: #fff; visibility: hidden;">
                                                                            </label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                                            <label id="Label13" runat="server" class="control-label">
                                                                                Place of mothers ANC follow up:</label>
                                                                        </div>
                                                                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                            <asp:DropDownList ID="ddlmothersANCFU" onchange="fnANCmotherfollowup();" runat="server"
                                                                                CssClass="form-control" Width="95%">
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                            <label id="Label14" runat="server" class="control-label required">
                                                                                Mother received drugs for PMTCT:</label>
                                                                        </div>
                                                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                                            <input id="rdMotherRDrugYes" type="radio" runat="server" name="MotherRDrug" />
                                                                            <label for="rdMotherRDrugYes">
                                                                                Yes</label>
                                                                            <input id="rdMotherRDrugNo" runat="server" name="MotherRDrug" type="radio" />
                                                                            <label for="rdMotherRDrugNo">
                                                                                No</label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <span id="spnANCFollowup" style="display: none">
                                                                            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                                                <label id="Label30" runat="server" class="control-label">
                                                                                    Specify other Facility:</label>
                                                                            </div>
                                                                            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                <asp:TextBox ID="txtmotherANCfollowup" runat="server" Columns="30" MaxLength="30"
                                                                                    CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                        </span>
                                                                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                        </div>
                                                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                                            <label id="Label16" runat="server" class="control-label required">
                                                                                On ART at enrollment of infant:</label>
                                                                        </div>
                                                                        <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                                            <input id="rdoARTEnrolYes" type="radio" value="Yes" runat="server" name="ARTEnrol" />
                                                                            <label for="rdoARTEnrolYes">
                                                                                Yes</label>
                                                                            <input id="rdoARTEnrolNo" runat="server" name="ARTEnrol" type="radio" value="No" />
                                                                            <label for="rdoARTEnrolNo">
                                                                                No</label>
                                                                        </div>
                                                                        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                                                        </div>
                                                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                                        </div>
                                                                    </div>
                                                                    <div class="row" style="margin-left: 5px;">
                                                                        <table width="100%" border="0">
                                                                            <caption style="font-weight: bold; text-align: left;">
                                                                                Mother Available Labs</caption>
                                                                            <tr>
                                                                                <td>
                                                                                    <div class="row">
                                                                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                                                            <label id="Label17" runat="server" class="control-label">
                                                                                                Type of Test</label>
                                                                                        </div>
                                                                                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                            <asp:DropDownList ID="ddlTestDone" runat="server" CssClass="form-control" Width="150">
                                                                                            </asp:DropDownList>
                                                                                        </div>
                                                                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                                                            <label id="Label18"  runat="server" class="control-label">
                                                                                                Test results:</label>
                                                                                        </div>
                                                                                        <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                                                            <asp:TextBox ID="txtresultmother" runat="server" MaxLength="44" Columns="43" CssClass="form-control"
                                                                                                Width="400"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <div class="row">
                                                                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                                                            <label id="Label19" runat="server" class="control-label">
                                                                                                Date results given:</label>
                                                                                        </div>
                                                                                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                            <div class="form-group">
                                                                                                <div class="input-group date">
                                                                                                    <div class="input-group-addon">
                                                                                                        <i class="fa fa-calendar"></i>
                                                                                                    </div>
                                                                                                    <input type="text" class="form-control pull-left" id="txtresultmothergiven" clientidmode="Static"
                                                                                                        maxlength="11" size="11" runat="server" data-date-format="dd-M-yyyy" style="width: 120px;"
                                                                                                        onblur="DateFormat(this,this.value,event,false,3)" onkeyup="DateFormat(this,this.value,event,false,3);"
                                                                                                        onfocus="javascript:vDateType='3'" />
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                                                            <label id="Label20" runat="server" class="control-label">
                                                                                                Remarks:</label>
                                                                                        </div>
                                                                                        <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                                                            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="form-control"
                                                                                                Width="400" Style="resize: none;"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="border pad5 formbg" width="100%">
                                                                                    <div id="div1" class="whitebg" align="center">
                                                                                        <asp:Button ID="btnMMother" Text="Add" runat="server" OnClick="btnMMother_Click"
                                                                                            CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                                                                                        <label class="glyphicon glyphicon-open" style="margin-left: -3%; vertical-align: sub;
                                                                                            color: #fff;">
                                                                                        </label>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="pad5 formbg border" colspan="2">
                                                                                    <br />
                                                                                    <div class="grid" id="div2" style="width: 98%;">
                                                                                        <div class="rounded">
                                                                                            <div class="top-outer">
                                                                                                <div class="top-inner">
                                                                                                    <div class="top">
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="mid-outer">
                                                                                                <div class="mid-inner">
                                                                                                    <div class="mid" style="height: 100px; overflow: auto">
                                                                                                        <div id="div3" class="GridView whitebg">
                                                                                                            <asp:GridView Height="25px" ID="GrdMMHistory" runat="server" AutoGenerateColumns="False"
                                                                                                                Width="100%" AllowSorting="True" BorderWidth="1px" GridLines="None" CssClass="table table-bordered table-hover"
                                                                                                                CellPadding="0" OnRowDeleting="GrdMMHistory_RowDeleting">
                                                                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                                            </asp:GridView>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="bottom-outer">
                                                                                                <div class="bottom-inner">
                                                                                                    <div class="bottom">
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                              <%--  <act:CollapsiblePanelExtender ID="CPEVitalSigns" runat="server" SuppressPostBack="True"
                                                    ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnl2VitalSigns" CollapseControlID="pnl1VitalSigns"
                                                    ExpandControlID="pnl1VitalSigns" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                                                    ImageControlID="ImgPC" Enabled="True"></act:CollapsiblePanelExtender>
                                                <act:CollapsiblePanelExtender ID="CPENNHistory" runat="server" SuppressPostBack="True"
                                                    ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnl2NNHistory" CollapseControlID="pnl1NNHistory"
                                                    ExpandControlID="pnl1NNHistory" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                                                    ImageControlID="imgNigeriaMedical" Enabled="True"></act:CollapsiblePanelExtender>--%>
                                                <act:CollapsiblePanelExtender ID="CPEMHistory" runat="server" SuppressPostBack="True"
                                                    ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnl2MHistory" CollapseControlID="pnl1MHistory"
                                                    ExpandControlID="pnl1MHistory" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                                                    ImageControlID="imgDAToxities" Enabled="True"></act:CollapsiblePanelExtender>
                                            </div>
                                            <br />
                                            <div class="border center formbg">
                                                <table cellspacing="6" cellpadding="0" width="100%" border="0" id="Table4" runat="server">
                                                    <tr id="Tr2" runat="server" align="center">
                                                        <td id="Td2" runat="server" class="form">
                                                            <asp:Button ID="btnClinicalHistorySave" runat="server" Text="Save" OnClick="btnClinicalHistorySave_Click"
                                                                CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                                                            <asp:Label ID="lblbtnHIVHistorySave" CssClass="glyphicon glyphicon-floppy-disk" Style="margin-left: -3%;
                                                                margin-right: 2%; vertical-align: sub; color: #fff;" runat="server"></asp:Label>
                                                            <asp:Label ID="lblbtnDelete" CssClass="glyphicon glyphicon-floppy-remove" Style="margin-left: -3%;
                                                                margin-right: 2%; vertical-align: sub; color: #fff;" runat="server" Visible="false"></asp:Label>
                                                            <asp:Button ID="btncloseClinicalHist" runat="server" Text="Close" CssClass="btn btn-primary"
                                                                Height="30px" Width="8%" Style="text-align: left;" />
                                                            <label class="glyphicon glyphicon-remove-circle" style="margin-left: -3%; margin-right: 2%;
                                                                vertical-align: sub; color: #fff;">
                                                            </label>
                                                            <asp:Button ID="btnClinicalHistoryPrint" runat="server" Text="Print" CssClass="btn btn-primary"
                                                                Height="30px" Width="8%" Style="text-align: left;" />
                                                            <label class="glyphicon glyphicon-print" style="margin-left: -3%; vertical-align: sub;
                                                                color: #fff;">
                                                            </label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            </div>
                                        </ContentTemplate>
                                    </act:TabPanel>
                                    <act:TabPanel ID="TabClinicalHistory" runat="server" HeaderText="Clinical Review">
                                        <ContentTemplate>
                                        <div class="box box-primary">
                    <div class="box-header with-border">
            <h3 class="box-title">
                Presenting Complaints</h3>           
        </div>  
        <div class="row">
        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label id="Label1" runat="server" for="inputEmail3" class="control-label">
                                                                            Any Complaints :</label>
            </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <input id="chkComplaints" name="switch-size" type="checkbox" data-size="small" data-on-text="Yes"
                    data-off-text="No" clientmode="static" onchange="getCBValue('chkComplaints','hfComplaints')">
                 <asp:HiddenField ID="hfComplaints" runat="server" Value="0" />
                   
            </div>
        </div>  
        <div class="box-header with-border" style="background-color:#DCDCDC;">
        <h3 class="box-title">
            Review of Systems and Physical Exam</h3>
       <%-- <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                <i class="fa fa-minus"></i>
            </button>
        </div>--%>
        <!-- /.box-tools -->
    </div>
     <div class="box-body">
       <div class="box box-default box-solid">
         <div class="box-body">
         <div class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                        <div>
                            <div class="row">
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label requiredFieldColor">
                                        General Examination:</label>
                                </div>
                                <div class="col-md-10 col-sm-12 col-xs-12 form-group">
                                 <asp:DropDownList ID="ddlGeneralExamination" runat="server" CssClass="form-control" Width="95%"></asp:DropDownList>                                    
                                </div>
                            </div>
                            <!-- /.row -->
                            <div class="row">
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        Skin:</label>
                                </div>
                                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                <input id="chkSkin" name="switch-size" type="checkbox" data-size="small" data-on-text="Yes" data-off-text="No" 
                                onchange="disable_enable_Dropdown('chkSkin','divSkin')";>                               
                                </div>
                                 <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                 </div>
                                <div class="checkbox checkbox-primary col-md-8 col-sm-12 col-xs-12 form-group" id="divSkin" style="display: none;">
                                <asp:CheckBoxList ID="cblSkin" runat="server" RepeatDirection="Horizontal" RepeatColumns="8"
                                RepeatLayout="Flow"></asp:CheckBoxList>
                                 </div>
                            </div>
                            <!-- /.row -->
                            <div class="row">
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        ENT:</label>
                                </div>
                                <div class="col-md-1 col-sm-12 col-xs-12 form-group">                               
                                   <input id="chkENT" name="switch-size" type="checkbox" data-size="small" data-on-text="Yes" data-off-text="No" 
                                   onchange="disable_enable_Dropdown('chkENT','divENT')";>
                                </div>
                                  <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                 </div>
                                <div class="checkbox checkbox-primary col-md-8 col-sm-12 col-xs-12 form-group" id="divENT" style="display: none;">
                                <asp:CheckBoxList ID="cblENT" runat="server" RepeatDirection="Horizontal" RepeatColumns="9"
                                RepeatLayout="Flow"></asp:CheckBoxList>                                   
                                </div>
                            </div>
                            <!-- /.row -->
                            <div class="row">
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        Chest:</label>
                                </div>
                                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                 <input id="chkChest" name="switch-size" type="checkbox" data-size="small" data-on-text="Yes" data-off-text="No" 
                                 onchange="disable_enable_Dropdown('chkChest','divChest')";>                               
                                </div>
                                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                 </div>
                                <div class="checkbox checkbox-primary col-md-8 col-sm-12 col-xs-12 form-group" id="divChest" style="display: none;">
                                <asp:CheckBoxList ID="cblChest" runat="server" RepeatDirection="Horizontal" RepeatColumns="8"
                                RepeatLayout="Flow"></asp:CheckBoxList> 
                                </div>
                            </div>
                            <!-- /.row -->
                            <div class="row">
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        CVS:</label>
                                </div>
                                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                 <input id="chkCVS" name="switch-size" type="checkbox" data-size="small" data-on-text="Yes" data-off-text="No" 
                                 onchange="disable_enable_Dropdown('chkCVS','divCVS')";>                                
                                </div>
                                 <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                 </div>
                                <div class="checkbox checkbox-primary col-md-8 col-sm-12 col-xs-12 form-group" id="divCVS" style="display: none;">
                                <asp:CheckBoxList ID="cblCVS" runat="server" RepeatDirection="Horizontal" RepeatColumns="8"
                                RepeatLayout="Flow"></asp:CheckBoxList> 
                                </div>
                            </div>
                            <!-- /.row -->
                            <div class="row">
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        Abdomen:</label>
                                </div>
                                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                 <input id="chkAbdomen" name="switch-size" type="checkbox" data-size="small" data-on-text="Yes" data-off-text="No" 
                                 onchange="disable_enable_Dropdown('chkAbdomen','divAbdomen')";>                                
                                </div>
                                 <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                 </div>
                                <div class="checkbox checkbox-primary col-md-8 col-sm-12 col-xs-12 form-group" id="divAbdomen" style="display: none;">
                                <asp:CheckBoxList ID="cblAbdomen" runat="server" RepeatDirection="Horizontal" RepeatColumns="8"
                                RepeatLayout="Flow"></asp:CheckBoxList> 
                                </div>
                            </div>
                            <!-- /.row -->
                            <div class="row">
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        CNS:</label>
                                </div>
                                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                 <input id="chkCNS" name="switch-size" type="checkbox" data-size="small" data-on-text="Yes" data-off-text="No" 
                                 onchange="disable_enable_Dropdown('chkCNS','divCNS')";>                                
                                </div>
                                 <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                 </div>
                                <div class="checkbox checkbox-primary col-md-8 col-sm-12 col-xs-12 form-group" id="divCNS" style="display: none;">
                               <asp:CheckBoxList ID="cblCNS" runat="server" RepeatDirection="Horizontal" RepeatColumns="8"
                                RepeatLayout="Flow"></asp:CheckBoxList>                                  
                                </div>
                            </div>
                            <!-- /.row -->
                            
                            <!-- /.row -->
                            <!-- /.row -->
                            <div class="row">
                                <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        Review of System Comments :</label>
                                         <asp:TextBox ID="txtReviewSystemComments" TextMode="MultiLine" Width="99%" runat="server" maxlength="1000" 
                                                                Rows="4" Style="resize: none" CssClass="form-control" placeholder="Review of System Comments and Other Systemic Finding"></asp:TextBox>
                                    
                                </div>
                                <!-- /.box -->
                            </div>
                        </div>
                        <!-- /.panel -->
                    </div>
                    <!-- /.row -->
                </div>
         </div>
       </div>
     </div>
        <div class="box-header with-border" style="background-color:#DCDCDC;">
        <h3 class="box-title">
           Immunization History</h3>
        <%--<div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                <i class="fa fa-minus"></i>
            </button>
        </div>--%>
        <!-- /.box-tools -->
    </div>
    
           <div class="box-body">
           <div class="box box-default box-solid">
         <div class="box-body">
         <div class="row">
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label requiredFieldColor">
                                        <asp:Label ID="lblImmunisationPeriod" runat="server" Font-Bold="True" Text="Immunization Period:"></asp:Label></label>
                                </div>
                                <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                        <label for="inputEmail3" class="control-label">
                                        <asp:Label ID="lblImmunisationGiven" runat="server" Font-Bold="True" Text="Immunizations Given:"></asp:Label>
                                        </label>             
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                        <label for="inputEmail3" class="control-label requiredFieldColor">
                                        <asp:Label ID="lblDateImmunised" runat="server" Font-Bold="True" Text="Date Immunised:"></asp:Label>
                                        </label>             
                                </div>
                                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                                 
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <asp:DropDownList ID="ddlImmunisationPeriod" runat="server" CssClass="form-control"
                                                                                Width="95%">
                                                                            </asp:DropDownList>
                                </div>
                                <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                      <asp:DropDownList ID="ddImmunisationgiven" runat="server" CssClass="form-control"
                                                                                Width="95%">
                                                                            </asp:DropDownList>          
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <div class="form-group">
                                                                                <div class="input-group date">
                                                                                    <div class="input-group-addon">
                                                                                        <i class="fa fa-calendar"></i>
                                                                                    </div>
                                                                                    <input type="text" class="form-control pull-left" id="txtDateImmunised" clientidmode="Static"
                                                                                        maxlength="11" size="11" runat="server" data-date-format="dd-M-yyyy" style="width: 120px;"
                                                                                        onblur="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"
                                                                                        onkeyup="DateFormat(this,this.value,event,false,'3')" />
                                                                                </div>
                                                                            </div>            
                                </div>
                                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                      <div id="div7" align="left">
                                                                            <asp:Button ID="btnAddImmunization" Text="Add" runat="server" OnClick="btnAddImmunization_Click"
                                                                                CssClass="btn btn-primary" Height="30px"  Style="text-align: left;" />
                                                                            <label class="glyphicon glyphicon-open" style="margin-left: -3%; vertical-align: sub;
                                                                                color: #fff;">
                                                                            </label>
                                                                        </div>           
                                </div>
                            </div>
                            <div class="row" align="center">
                                                                        <br />
                                                                        <div class="grid" id="div8" style="width: 97%; padding-right: 1%">
                                                                            <div class="rounded">
                                                                                <div class="top-outer">
                                                                                    <div class="top-inner">
                                                                                        <div class="top">
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="mid-outer">
                                                                                    <div class="mid-inner">
                                                                                        <div class="mid" style="height: 100px; overflow: auto">
                                                                                            <div id="div9" class="GridView whitebg">
                                                                                                <asp:GridView Height="25px" ID="GrdImmunization" runat="server" AutoGenerateColumns="False"
                                                                                                    Width="100%" AllowSorting="True" BorderWidth="1px" GridLines="None" CssClass="table table-bordered table-hover"
                                                                                                    CellPadding="0" OnRowDeleting="GrdImmunization_RowDeleting">
                                                                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                                </asp:GridView>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="bottom-outer">
                                                                                    <div class="bottom-inner">
                                                                                        <div class="bottom">
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
<div class="row" style="padding-left: 1%">
 <label for="inputEmail3" class="control-label">Neonatal History Notes :</label>
 <asp:TextBox ID="txtNeonatalHistoryNotes" TextMode="MultiLine" Width="98%" runat="server" maxlength="1000" 
 Rows="4" Style="resize: none" CssClass="form-control" placeholder="Neonatal History Notes"></asp:TextBox>
 </div>
 <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                            <label for="inputEmail3" class="control-label">
                                                                                <asp:Label ID="Label23" runat="server" Font-Bold="True" Text="Has Vitamin A been given?:"></asp:Label></label>
                                                                        </div>
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                            <input id="rdoHasVitaminGivenYes" name="Vitamingiven" type="radio" runat="server" /></input><label>Yes</label>
                                                                            <input id="rdoHasVitaminGivenNo" name="Vitamingiven" type="radio" runat="server" /></input><label>No</label>
                                                                        </div>
 </div>
 
                                                                   
         </div>
         </div>
           </div>   
           <%--MileStone--%>
           <div class="box-header with-border" style="background-color:#DCDCDC;">
        <h3 class="box-title">
           Milestones</h3>
        <%--<div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                <i class="fa fa-minus"></i>
            </button>
        </div>--%>
        <!-- /.box-tools -->
    </div>
             <div class="box-body">
           <div class="box box-default box-solid">
         <div class="box-body">
         <div class="row">
          <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label id="Label5" runat="server" for="inputEmail3" class="control-label">
              Milestone Assessed</label>
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label id="Label6" runat="server" for="inputEmail3" class="control-label">
              Date Assessed</label>
            </div>
            <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <label id="Label7" runat="server" for="inputEmail3" class="control-label">
              Achieved</label>
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label id="Label8" runat="server" for="inputEmail3" class="control-label">
              <asp:Label ID="lblStatus" runat="server" Font-Bold="True" Text="Status:" CssClass="required"></asp:Label></label>
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label id="Label9" runat="server" for="inputEmail3" class="control-label">
              <asp:Label ID="lblComment" runat="server" Font-Bold="True" Text="Comments:"></asp:Label></label>
            </div>
            <div class="col-md-1 col-sm-12 col-xs-12 form-group">
           
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             
            </div>
         </div>
         <div class="row">
          <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            <asp:DropDownList ID="ddlMilestoneAssessed" runat="server" CssClass="form-control" Width="95%"></asp:DropDownList>
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
               <div class="form-group">
                                                                                <div class="input-group date">
                                                                                    <div class="input-group-addon">
                                                                                        <i class="fa fa-calendar"></i>
                                                                                    </div>
                                                                                    <input type="text" class="form-control pull-left" id="txtDateAssessed" clientidmode="Static"
                                                                                        maxlength="11" size="11" runat="server" data-date-format="dd-M-yyyy" style="width: 120px;"
                                                                                        onblur="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"
                                                                                        onkeyup="DateFormat(this,this.value,event,false,'3')" />
                                                                                </div>
                                                                            </div>   
            </div>
            <div class="col-md-1 col-sm-12 col-xs-12 form-group">
           <asp:CheckBox ID="chkAchieved" runat="server"/>
           </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
              <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" Width="95%"></asp:DropDownList>
              </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtComment" runat="server" Style="resize: none" TextMode="MultiLine"
              Width="100%" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-1 col-sm-12 col-xs-12 form-group">
           <div id="div4" class="whitebg" align="center">
                                                                            <asp:Button ID="btnAddMilestone" Text="Add" runat="server" OnClick="btnAddMilestone_Click"
                                                                                CssClass="btn btn-primary" Height="30px" Width="90%" Style="text-align: left;" />
                                                                            <label class="glyphicon glyphicon-open" style="margin-left: -3%; vertical-align: sub;
                                                                                color: #fff;">
                                                                            </label>
                                                                        </div>
            </div>
            <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                    <a class="btn btn-social-icon btn-adn btn-sm" style="margin-top: 1px;" onclick="window.open(this.href, 'myWindowName', 'width=800, height=600'); return false;"
                                        onkeypress="window.open(this.href, 'myWindowName', 'width=800, height=600'); return false;"
                                        href="<%= ResolveUrl("~/HIVCE/Milestones.pdf") %>"><i class="fa fa-adn"></i>
                                    </a>
                                </div>
         </div>
         <div class="row" align="center">
                                                                        <br />
                                                                        <div class="grid" id="div5" style="width: 97%; padding-right: 1%">
                                                                            <div class="rounded">
                                                                                <div class="top-outer">
                                                                                    <div class="top-inner">
                                                                                        <div class="top">
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="mid-outer">
                                                                                    <div class="mid-inner">
                                                                                        <div class="mid" style="height: 100px; overflow: auto">
                                                                                            <div id="div6" class="GridView whitebg">
                                                                                                <asp:GridView Height="25px" ID="gvMilestones" runat="server" AutoGenerateColumns="False"
                                                                                                    Width="100%" AllowSorting="True" BorderWidth="1px" GridLines="None" CssClass="table table-bordered table-hover"
                                                                                                    CellPadding="0" OnRowDeleting="gvMilestones_RowDeleting">
                                                                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                                </asp:GridView>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="bottom-outer">
                                                                                    <div class="bottom-inner">
                                                                                        <div class="bottom">
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
         </div>
         </div>
         </div>
          <%--TB Assessment--%>
          <div class="box-header with-border" style="background-color:#DCDCDC;">
        <h3 class="box-title">
           TB Assessment</h3>
       <%-- <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                <i class="fa fa-minus"></i>
            </button>
        </div>--%>
        <!-- /.box-tools -->
    </div>
      <div class="box-body">
           <div class="box box-default box-solid">
         <div class="box-body">
         <div class="row">
          <div class="col-md-8 col-sm-12 col-xs-12 form-group">
             <label id="Label10" runat="server" for="inputEmail3" class="control-label">
             TB Assessment (ICF) :</label>
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label id="Label11" runat="server" for="inputEmail3" class="control-label">
             TB Findings :</label>
            </div>
         </div>
         <div class="row">
          <div class="col-md-8 col-sm-12 col-xs-12 form-group">
             <div class="checkbox checkbox-primary customdivbordermultiselect">
              <asp:CheckBoxList ID="cblTBAssesment" runat="server" RepeatDirection="Horizontal" RepeatColumns="3" RepeatLayout="Flow"></asp:CheckBoxList>
              </div>
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
            <asp:DropDownList ID="ddlTBFindings" runat="server" Width="98%" CssClass="form-control" >
                    </asp:DropDownList>
            </div>
         </div>
         </div>
         </div>
         </div>
           <%--Reminder Work Plan--%>
           <div class="box-header with-border" style="background-color:#DCDCDC;">
        <h3 class="box-title">
           Reminders and workplan</h3>
        <%--<div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                <i class="fa fa-minus"></i>
            </button>
        </div>--%>
        <!-- /.box-tools -->
    </div>
     <div class="box-body">
           <div class="box box-default box-solid">
         <div class="box-body">
          <div class="row">
             <textarea id="txtAreaWorkPlan" cols="20" rows="2" runat="server" placeholder="Enter workplan details"
                                                                                style="float: left; margin-left: 13px; width: 97%; resize:none" class="form-control"></textarea>
          </div>
         </div>
         </div>
         </div>
  <div class="row">
   <UcNextAppointment:UcNxtAppt ID="UserControlKNH_NextAppointment" runat="server" />
        </div>      

        </div>

                                            
                                            <br />
                                            <div class="border center formbg">
                                                <table cellspacing="6" cellpadding="0" width="100%" border="0" id="Table1" runat="server">
                                                    <tr id="Tr4" runat="server" align="center">
                                                        <td id="Td4" runat="server" class="form">
                                                            <asp:Button ID="btnHIVHistorySave" runat="server" Text="Save" Enabled="false" OnClick="btnHIVHistorySave_Click"
                                                                CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                                                            <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; margin-right: 2%;
                                                                vertical-align: sub; color: #fff;">
                                                            </label>
                                                            <asp:Button ID="btncloseHIVHistory" runat="server" Text="Close" Enabled="false" OnClick="btncloseHIVHistory_Click"
                                                                CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                                                            <label class="glyphicon glyphicon-remove-circle" style="margin-left: -3%; margin-right: 2%;
                                                                vertical-align: sub; color: #fff;">
                                                            </label>
                                                            <asp:Button ID="btnHIVHistoryPrint" runat="server" OnClientClick="WindowPrint()"
                                                                Text="Print" CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                                                            <label class="glyphicon glyphicon-print" style="margin-left: -3%; vertical-align: sub;
                                                                color: #fff;">
                                                            </label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <br />
                                        </ContentTemplate>
                                    </act:TabPanel>
                                </act:TabContainer>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
