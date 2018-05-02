<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true" 
    CodeBehind="frmClinical_KNH_HEI.aspx.cs" Inherits="PresentationApp.ClinicalForms.frmClinical_KNH_HEI" %>

<%@ Register TagPrefix="UcVitalSign" TagName="Uc1" Src="~/ClinicalForms/UserControl/UserControlKNH_VitalSigns.ascx" %>
<%@ Register TagPrefix="UcHEIPrescomplaints" TagName="UcPC" Src="~/ClinicalForms/UserControl/UserControlKNHPresentingComplaints.ascx" %>
<%@ Register TagPrefix="UcNextAppointment" TagName="UcNxtAppt" Src="~/ClinicalForms/UserControl/UserControlKNH_NextAppointment.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="act" Namespace="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
<script type="text/javascript">
    function RegisterJQuery() {
        $('#txtVisitDate').datepicker({ autoclose: true });
        $('#txttestresultsgiven').datepicker({ autoclose: true });
        $('#txtresultmothergiven').datepicker({ autoclose: true });
        $('#txtDateImmunised').datepicker({ autoclose: true });
        $('#txtTreatmentDate').datepicker({ autoclose: true });

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

        function fnPlan() {
            var e = document.getElementById("<%=ddlPlan.ClientID%>");
            var strtext = e.options[e.selectedIndex].text;
            if (strtext == "Select") {
                hide('spnRegimen');
            }
            else {
                show('spnRegimen');
                var ddlRegimen = document.getElementById("<%=ddlRegimen.ClientID%>");
                if (strtext == "CTX" || strtext == "Nevirapine") {
                    ddlRegimen.value = 199;
                }
                else {
                    ddlRegimen.value = 0;
                }
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
        function fnreferredto() {
            var e = document.getElementById("<%=ddlReferred.ClientID%>");
            var strtext = e.options[e.selectedIndex].text;
            if (strtext == "Other") {
                show('spnReferredto');
            }
            else {
                hide('spnReferredto');
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
    </script>
    <div class="content-wrapper">
     
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
      <div class="box-header">
              <h3 class="box-title">HEI Form</h3>
            </div>
     <!-- /.box-header -->
            <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
            <div class="row">
            <br />
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-right">
            
             </div>
              <div class="col-md-2 col-sm-12 col-xs-12 form-group text-right">
             <label for="inputEmail3" class="control-label required">Visit date:</label>
             </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtVisitDate" clientidmode="Static"
                                                       onblur="DateFormat(this,this.value,event,false,3)" onkeyup="DateFormat(this,this.value,event,false,3);"
                            onfocus="javascript:vDateType='3'" maxlength="11" size="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>

            
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group text-right">
            <label for="inputEmail3" class="control-label required">Visit Type:</label>
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlVisitType" runat="server" OnSelectedIndexChanged="ddlVisitType_SelectedIndexChanged"
                            AutoPostBack="true" CssClass="form-control" Width="120">
                        </asp:DropDownList>
            </div>
             </div>
             <div class="row" style="margin-left:2px;">
             <act:TabContainer ID="tabControl" runat="server" CssClass="ajax__myTab" ActiveTabIndex="0" Width="100%" OnActiveTabChanged="tabControl_ActiveTabChanged" AutoPostBack="true">
                <act:TabPanel ID="TabPnlTriage" runat="server" HeaderText="Triage and Neonatal History">
                    <ContentTemplate>
                        <div class="border center formbg">
                            <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td>
                                        <asp:Panel ID="pnl1VitalSigns" runat="server" onclick="fnsetCollapseState();" CssClass="border center formbg"
                                            Style="padding: 6px">
                                            <h5 class="forms" align="left">
                                                <asp:ImageButton ID="ImgPC" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                <asp:Label ID="lblVitalSigns" runat="server" Text="Vital Signs"></asp:Label></h5>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="pnl2VitalSigns" runat="server">
                                            <div id="VitalSigns" style="display: none">
                                                <table cellspacing="6" cellpadding="0" width="100%" border="0">
                                                    <tr>
                                                        <td class="border pad5 whitebg" width="100%">
                                                            <UcVitalSign:Uc1 ID="idVitalSign" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                            <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td>
                                        <asp:Panel ID="pnl1NNHistory" runat="server" onclick="fnsetCollapseState();" CssClass="border center formbg"
                                            Style="padding: 6px">
                                            <h5 class="forms" align="left">
                                                <asp:ImageButton ID="imgNigeriaMedical" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                <asp:Label ID="lblNNHistory" runat="server" Text="Neo Natal History"></asp:Label></h5>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100%">
                                        <asp:Panel ID="pnl2NNHistory" runat="server" Style="overflow:hidden">
                                            <div id="NNHistory" style="display: none">
                                            <div class="row">
      <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Source of Referral:</label>
            </div>
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
               <asp:TextBox ID="txtSourceofReferral" runat="server" MaxLength="50" Columns="50" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Place of Delivery:</label>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
               <asp:DropDownList ID="ddlPlaceofDelivery" onchange="fnPlaceofdelivery();" runat="server" CssClass="form-control" Width="150">
                                                                        </asp:DropDownList>
            </div>

        </div>
         <div class="row">
            
             <span id="spnotherfacility" style="display: none">
              <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                            <label for="inputEmail3" class="control-label" id="Label26" runat="server">
                                                                                Other Facility:</label>
                                                                            
                                                                            </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtOtherFacility" runat="server" MaxLength="45" Columns="45" CssClass="form-control"></asp:TextBox>
             </div>
              </span>
            
            
            <span id="spanotherdelivery" style="display: none">
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                   <label for="inputEmail3" class="control-label" id="Label27" runat="server">
                                                                                Specify Other:</label>                                                         
                                                                            
            </div>
               <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                    <asp:TextBox ID="txtOtherDelivery" runat="server" MaxLength="45" Columns="45" CssClass="form-control"></asp:TextBox>                                                    
             </div>                                                          
                                                                        </span>
            
            
             </div>
              <div class="row">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label for="inputEmail3" class="control-label"><label id="Label5" runat="server" for="inputEmail3" class="control-label required">
                                                                            Mode of delivery:</label></label>
             </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <asp:DropDownList ID="ddlModeofDelivery" runat="server" Width="150" CssClass="form-control">
                                                                        </asp:DropDownList>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label id="Label6" runat="server" for="inputEmail3" class="control-label required">
                                                                            Birth Weight (kgs):</label>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <div style="width: 100%; margin: 0px auto;">
<div style="float: left"><asp:TextBox ID="txtBirthWeight" runat="server" MaxLength="3" Columns="4" CssClass="form-control" Width="80"></asp:TextBox></div>
<div style="float: left"> <span class="smallerlabel">kg</span></div>
</div>
            
                                                                       
            </div>
             </div>
            <div class="row">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label id="Label8" runat="server" for="inputEmail3" class="control-label required">
                                                                            ARV Prophylaxis:</label>
             </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <asp:DropDownList ID="ddlARVProphylaxis" CssClass="form-control" onchange="fnotherARVProphylaxis();" runat="server" Width="300">
                                                                        </asp:DropDownList>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label id="Label11" runat="server" for="inputEmail3" class="control-label required">
                                                                            Infant feeding option:</label>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlIfeedingoption" onchange="fnotherfeedingoption();" runat="server" CssClass="form-control" Width="250">
                                                                        </asp:DropDownList>
            </div>
             </div>
             <div class="row">
              <span id="spnotherARVProphy" style="display: none">
              <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label id="Label28" for="inputEmail3" class="control-label" style="margin-left: 51px;" runat="server">
                                                                                Specify Other:</label>
             </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <asp:TextBox ID="txtOtherARVProphylaxis" runat="server" MaxLength="25" Columns="25" CssClass="form-control"></asp:TextBox>
            </div>
                                                                           
                                                                        </span>
                                                                         <span id="spnotherfeedingoption" style="display: none">
                                                                          <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <label id="Label29" for="inputEmail3" class="control-label" style="margin-left: 10px;" runat="server">
                                                                                Specify Other:</label>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <asp:TextBox ID="txtOtherFeedingoption" runat="server" MaxLength="25" Columns="25" CssClass="form-control"></asp:TextBox>
            </div>
                                                                            
                                                                            
                                                                        </span>

             
           
             </div>
             <div class="row">
             <div style="margin-left:20px;">
            <table border="0" width="100%" >
                                                                <caption style="font-weight: bold; text-align: left;">
                                                                    Infant Available Labs</caption>
                                                                <tr>
                                                                <td style="margin-left:20px;">
                                                              
                                                                 <div class="row">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label id="Label9" runat="server" for="inputEmail3" class="control-label">
                                                                            Type of Test:</label>
             </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <asp:DropDownList ID="ddlTypeofTest" runat="server" CssClass="form-control" Width="300">
                                                                        </asp:DropDownList>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
           <label id="Label1" runat="server" for="inputEmail3" class="control-label">
                                                                            Results:</label>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group"> 
            <asp:DropDownList ID="ddlTestResults" runat="server" CssClass="form-control" Width="150">
                                                                        </asp:DropDownList>
            </div>
             </div>
                                                                
                                                                </td>
                                                                    
                                                                    
                                                                </tr>
                                                                <tr>
                                                               <td style="margin-left:20px;">
                                                                 <div class="row">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label id="Label7" runat="server" for="inputEmail3" class="control-label margin35">
                                                                            Date results given to guardian:</label>
             </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group"> 
            <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txttestresultsgiven" clientidmode="Static"
                                                       maxlength="11" size="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;" 
                                                       onblur="DateFormat(this,this.value,event,false,3)" onfocus="javascript:vDateType='3'" onkeyup="DateFormat(this,this.value,event,false,3);"/>
                                                </div>
                                            </div>

            
            

                                                                            
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label id="Label10" runat="server" for="inputEmail3" class="control-label margin50">
                                                                            Comments:</label>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <asp:TextBox ID="txtcomments" runat="server" Columns="30" MaxLength="30" CssClass="form-control" Width="250"></asp:TextBox>
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
                 <div id="divDrugAllergyMedicalAlr" class="grid" style="width: 95%;margin-right:1%">
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
                                                                                        BorderWidth="1px" CellPadding="0" CssClass="table table-bordered table-hover" GridLines="None" Height="25px"
                                                                                        OnRowDeleting="GrdNNHistory_RowDeleting" Width="98%">
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
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                            <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td colspan="2" align="left">
                                        <asp:Panel ID="pnl1MHistory" onclick="fnsetCollapseState();" CssClass="border center formbg"
                                            runat="server" Style="padding: 6px">
                                            <h5 class="forms" align="left">
                                                <asp:ImageButton ID="imgDAToxities" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                Maternal History</h5>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="left">
                                        <asp:Panel ID="pnl2MHistory" runat="server" Style="overflow:hidden;">
                                            <div id="MHistory" style="display: none">
                                            <div class="row">
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label id="Label15" runat="server" class="control-label required">
                                                                            State of mother:</label>
             </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <asp:DropDownList ID="ddlStateofMother" runat="server" Width="100" CssClass="form-control">
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
            <asp:DropDownList ID="ddlmothersANCFU" onchange="fnANCmotherfollowup();" runat="server" CssClass="form-control" Width="140">
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
              <div class="col-md-2 col-sm-12 col-xs-12 form-group"><label id="Label30" runat="server" class="control-label">
                                                                                Specify other Facility:</label>
            </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtmotherANCfollowup" runat="server" Columns="30" MaxLength="30" CssClass="form-control"></asp:TextBox>
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
            <div class="col-md-4 col-sm-12 col-xs-12 form-group"><input id="rdoARTEnrolYes" type="radio" value="Yes" runat="server" name="ARTEnrol" />
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
             <div class="row" style="margin-left:5px;">
             <table width="100%" border="0">
                                                                <caption style="font-weight: bold; text-align: left;">
                                                                    Mother Available Labs</caption>
                                                                <tr>
                                                                <td>
                                                                <div class="row">
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label id="Label17" runat="server" class="control-label">Type of Test</label>
             </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <asp:DropDownList ID="ddlTestDone" runat="server" CssClass="form-control" Width="150"></asp:DropDownList>
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group"> 
            <label id="Label18" style="margin-left: 29px" runat="server" class="control-label">Test results:</label>
            </div>
            <div class="col-md-5 col-sm-12 col-xs-12 form-group">
            <asp:TextBox ID="txtresultmother" runat="server" MaxLength="44" Columns="43" CssClass="form-control" Width="400"></asp:TextBox>
            </div>
             </div>
                                                                </td>
                                                                   
                                                                </tr>
                                                                <tr>
                                                                <td>
                                                               <div class="row">
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
              <label id="Label19" runat="server" class="control-label">Date results given:</label>
             </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtresultmothergiven" clientidmode="Static"
                                                       maxlength="11" size="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;" 
                                                       onblur="DateFormat(this,this.value,event,false,3)" onkeyup="DateFormat(this,this.value,event,false,3);" onfocus="javascript:vDateType='3'"/>
                                                </div>
                                            </div>                                                            
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            <label id="Label20" runat="server" class="control-label">Remarks:</label>
            </div>
            <div class="col-md-5 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="form-control" Width="400" Style="resize:none;"></asp:TextBox>
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
                            <act:CollapsiblePanelExtender ID="CPEVitalSigns" runat="server" SuppressPostBack="True"
                                ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnl2VitalSigns" CollapseControlID="pnl1VitalSigns"
                                ExpandControlID="pnl1VitalSigns" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                                ImageControlID="ImgPC" Enabled="True"></act:CollapsiblePanelExtender>
                            <act:CollapsiblePanelExtender ID="CPENNHistory" runat="server" SuppressPostBack="True"
                                ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnl2NNHistory" CollapseControlID="pnl1NNHistory"
                                ExpandControlID="pnl1NNHistory" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                                ImageControlID="imgNigeriaMedical" Enabled="True"></act:CollapsiblePanelExtender>
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
                    </ContentTemplate>
                </act:TabPanel>
                <act:TabPanel ID="TabClinicalHistory" runat="server" HeaderText="Clinical Review">
                    <ContentTemplate>
                        <div class="border center formbg">
                            <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td>
                                        <asp:Panel ID="pnl1IHistory" onclick="fnsetCollapseState();" CssClass="border center formbg"
                                            runat="server" Style="padding: 6px">
                                            <h5 class="forms" align="left">
                                                <asp:ImageButton ID="imghivrelhistory" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                <asp:Label ID="lblIHistory" runat="server" Text="Immunization and Vitamin A"></asp:Label></h5>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="pnl2IHistory" runat="server" Visible="True" Style="overflow:hidden;">
                                            <div id="IHistory" style="display: none">
                                            <div class="row">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label for="inputEmail3" class="control-label"><asp:Label ID="lblImmunisationPeriod" runat="server" Font-Bold="True" Text="Immunization Period:"></asp:Label></label>
             </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group"><asp:DropDownList ID="ddlImmunisationPeriod" runat="server" CssClass="form-control" Width="200">
                                                                        </asp:DropDownList>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group"><label for="inputEmail3" class="control-label">
            <asp:Label ID="lblImmunisationGiven" runat="server" Font-Bold="True" Text="Immunizations Given:"></asp:Label>
            </label>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <asp:DropDownList ID="ddImmunisationgiven" runat="server" CssClass="form-control" Width="200"></asp:DropDownList>
            </div>
             </div>
             <div class="row">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label for="inputEmail3" class="control-label">
             <asp:Label ID="lblDateImmunised" runat="server" Font-Bold="True" Text="Date Immunised:"></asp:Label></label>
             </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtDateImmunised" clientidmode="Static"
                                                       maxlength="11" size="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"
                                                       onblur="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'" onkeyup="DateFormat(this,this.value,event,false,'3')"/>
                                                </div>
                                            </div>

            
            
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group"><label for="inputEmail3" class="control-label">
            <asp:Label ID="Label23" runat="server" Font-Bold="True" Text="Has Vitamin A been given?:"></asp:Label></label>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <input id="rdoHasVitaminGivenYes" name="Vitamingiven" type="radio" runat="server" /></input><label>Yes</label>
                                                                        <input id="rdoHasVitaminGivenNo" name="Vitamingiven" type="radio" runat="server" /></input><label>No</label>
            </div>
             </div>
             <div class="row" align="center">
              <div id="div7" class="whitebg" align="center">
                                                                <asp:Button ID="btnAddImmunization" Text="Add" runat="server" OnClick="btnAddImmunization_Click"
                                                                    CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                                                                <label class="glyphicon glyphicon-open" style="margin-left: -3%; vertical-align: sub;
                                                                    color: #fff;">
                                                                </label>
                                                            </div>
             </div>
             <div class="row" align="center">
             <br />
             <div class="grid" id="div8" style="width: 97%;padding-right:1%">
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
                                                
                                            </div>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                            <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td>
                                        <asp:Panel ID="pnl1PComplaints" runat="server" onclick="fnsetCollapseState();" CssClass="border center formbg"
                                            Style="padding: 6px">
                                            <h5 class="forms" align="left">
                                                <asp:ImageButton ID="imgpriorart" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                <asp:Label ID="lblPComplaints" runat="server" Text="Presenting Complaints"></asp:Label></h5>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100%">
                                        <asp:Panel ID="pnl2PComplaints" runat="server" Visible="True" Style="overflow:hidden;">
                                            <div id="PComplaints" style="display: none">
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <UcHEIPrescomplaints:UcPC ID="UcHEIPcomplaints" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr class="center pad5 whitebg">
                                                        <td align="left" nowrap="noWrap">
                                                            <asp:Label ID="Label22" runat="server" Font-Bold="True" Text="TB Assesment:" CssClass="required"></asp:Label>
                                                            <div class="customdivbordermultiselect">
                                                                <asp:CheckBoxList ID="cblTBAssesment" runat="server">
                                                                </asp:CheckBoxList>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                            <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td colspan="2">
                                        <asp:Panel ID="Pnl1Examination" runat="server" onclick="fnsetCollapseState();" CssClass="border center formbg"
                                            Style="padding: 6px">
                                            <h5 class="forms" align="left">
                                                <asp:ImageButton ID="imgAdherence" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                <asp:Label ID="Label3" runat="server" Text="Examination, Milestones and Diagnosis"></asp:Label></h5>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" width="100%">
                                        <asp:Panel ID="Pnl2Examination" runat="server" Visible="True" Style="overflow:hidden;">
                                            <div id="Examination" style="display: none">
                                            <div class="row">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label for="inputEmail3" class="control-label"><asp:Label ID="Label21" runat="server" Font-Bold="True" Text="Examination:"></asp:Label></label>
             </div>
            
            <div class="col-md-8 col-sm-12 col-xs-12 form-group">
            <asp:TextBox ID="txtExamination" TextMode="MultiLine" runat="server" Width="99%" Style="resize:none;" CssClass="form-control"></asp:TextBox>
            </div>
             </div>
             <div class="row" style="margin-left:0px; background-color:Gray">
            <h4><asp:Label ID="Label31" runat="server" Font-Bold="True" Text="Milestones"></asp:Label></h3>
             </div>
             <div class="row">
             <br />
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label for="inputEmail3" class="control-label">
             <asp:Label ID="lblDuration" runat="server" Font-Bold="True" Text="Duration:" CssClass="required"></asp:Label></label>
             </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group"><asp:DropDownList ID="ddlDuration" runat="server" CssClass="form-control" Width="150"> </asp:DropDownList>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group"><label for="inputEmail3" class="control-label"><asp:Label ID="lblStatus" runat="server" Font-Bold="True" Text="Status:" CssClass="required"></asp:Label></label>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group"> <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" Width="150"></asp:DropDownList>
            </div>
             </div>
             <div class="row">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label for="inputEmail3" class="control-label"><asp:Label ID="lblComment" runat="server" Font-Bold="True" Text="Comments:"></asp:Label></label>
             </div>
            
            <div class="col-md-8 col-sm-12 col-xs-12 form-group">
            <asp:TextBox ID="txtComment" runat="server" Style="resize:none" TextMode="MultiLine" Width="81%" CssClass="form-control"></asp:TextBox>
            </div>
             </div>
              <div class="row" align="center">
             
              <div id="div4" class="whitebg" align="center">
                                                                            <asp:Button ID="btnAddMilestone" Text="Add" runat="server" OnClick="btnAddMilestone_Click"
                                                                                CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                                                                            <label class="glyphicon glyphicon-open" style="margin-left: -3%; vertical-align: sub;
                                                                                color: #fff;">
                                                                            </label>
                                                                        </div>
              </div>
              <div class="row" align="center">
               <br />
              <div class="grid" id="div5" style="width: 97%;padding-right:1%">
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
              <div class="row">
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label for="inputEmail3" class="control-label"><asp:Label ID="lblDiagnosis" runat="server" Font-Bold="True" Text="Diagnosis:"></asp:Label></label>
             </div>
           
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <div class="divborder checkbox" id="divdiagnosis">
                                                                            <asp:Panel ID="PnlDiagnosis" runat="server">
                                                                            </asp:Panel>
                                                                        </div>
            </div>
             </div>

                                                
                                            </div>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                            <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td align="left">
                                        <asp:Panel ID="Pnl1ManagementPlan" onclick="fnsetCollapseState();" CssClass="border center formbg"
                                            runat="server" Style="padding: 6px">
                                            <h5 class="forms" align="left">
                                                <asp:ImageButton ID="ImageButton2" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                Management Plan</h5>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="Pnl2ManagementPlan" runat="server" Visible="True" Style="overflow:hidden;">
                                            <div id="ManagementPlan" style="display: none">
                                            <div class="row">
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label for="inputEmail3" class="control-label"> <asp:Label ID="Label24" runat="server" Font-Bold="True" Text="Treatment:"></asp:Label></label>
             </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            <asp:DropDownList ID="ddlPlan" onchange="fnPlan();" runat="server" CssClass="form-control" Width="150"></asp:DropDownList>
            </div>
            <span id="spnRegimen" style="display: none">
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label for="inputEmail3" class="control-label"><asp:Label ID="Label25" runat="server" Font-Bold="True" Text="Plan:"></asp:Label></label>
             </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            <asp:DropDownList ID="ddlRegimen" runat="server" CssClass="form-control" Width="120"></asp:DropDownList>
            </div>
            </span>

            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            <label for="inputEmail3" class="control-label">
            
            <asp:Label ID="lblTreatment" runat="server" Font-Bold="True" Text="Date:"></asp:Label>
            </label>
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtTreatmentDate" clientidmode="Static"
                                                       maxlength="11" size="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;" 
                                                       onblur="DateFormat(this,this.value,event,false,3)" onkeyup="DateFormat(this,this.value,event,false,3);"
   onfocus="javascript:vDateType='3'"/>
                                                </div>
                                            </div>
                                                                
            </div>
             </div>
             <div class="row">
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label for="inputEmail3" class="control-label">Work plan detail</label>
             </div>
            
            <div class="col-md-10 col-sm-12 col-xs-12 form-group">
            <textarea id="txtAreaWorkPlan" cols="20" rows = "2" runat="server" placeholder="Enter workplan details"
                                                                    style="float:left; margin:5px; width:100%" class="form-control"></textarea>
            </div>
             </div>
              <div class="row" align="center">
              <div id="div10" class="whitebg" align="center">
                                                                <asp:Button ID="btnAddTB" Text="Add" runat="server" OnClick="btnAddTB_Click" CssClass="btn btn-primary"
                                                                    Height="30px" Width="8%" Style="text-align: left;" />
                                                                <label class="glyphicon glyphicon-open" style="margin-left: -3%; vertical-align: sub;
                                                                    color: #fff;">
                                                                </label>
                                                            </div>
              </div>
               <div class="row" align="center">
               <br />
               <div class="grid" id="div11" style="width: 97%;padding-right:1%">
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
                                                                                <div id="div12" class="GridView whitebg">
                                                                                    <asp:GridView Height="25px" ID="gvTB" runat="server" AutoGenerateColumns="False"
                                                                                        Width="100%" AllowSorting="True" BorderWidth="1px" GridLines="None" CssClass="table table-bordered table-hover"
                                                                                        CellPadding="0" OnRowDeleting="gvTB_RowDeleting">
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
                                                <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
                                                    <tr class="center pad5 whitebg">
                                                        <td style="width: 100%">
                                                            <table>
                                                                <tr>
                                                                    <td width="30%" align="left">
                                                                       
                                                                        
                                                                    </td>
                                                                    <td width="30%" align="left">
                                                                        
                                                                            
                                                                            
                                                                       
                                                                    </td>
                                                                    <td width="40%" align="center">
                                                                        
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan = "3">
                                                                    
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="border pad5 formbg" width="100%">
                                                            
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="pad5 formbg border" colspan="2">
                                                            
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                            <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td align="left">
                                        <asp:Panel ID="Pnl1RefAdminAppointment" onclick="fnsetCollapseState();" CssClass="border center formbg"
                                            runat="server" Style="padding: 6px">
                                            <h5 class="forms" align="left">
                                                <asp:ImageButton ID="ImageButton3" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                Referral, Admission and Appointments</h5>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="Pnl2RefAdminAppointment" runat="server" Visible="True" Style="overflow:hidden;">
                                            <div id="AdminAppointment" style="display: none">
                                             <div class="row">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label for="inputEmail3" class="control-label">Referred to?:</label>
             </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <asp:DropDownList ID="ddlReferred" onchange="fnreferredto();" runat="server" CssClass="form-control" Width="150"></asp:DropDownList>
            </div>
            <span id="spnReferredto" style="display: none">
            <div class="col-md-2 col-sm-12 col-xs-12 form-group"><label for="inputEmail3" class="control-label">Specify Other:</label>
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
            <asp:TextBox ID="txtOtherReferredto" runat="server" MaxLength="30" Columns="30" CssClass="form-control" Width="250"></asp:TextBox>
            </div>
            </span>
             </div>
              <div class="row">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label for="inputEmail3" class="control-label">Admit to ward?:</label>
             </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group"> <label>
                                                                            <input id="rdoadmittowardyes" type="radio" name="Ward" runat="server" />Yes</label>
                                                                        <label>
                                                                            <input id="rdoadmittowardno" type="radio" name="Ward" runat="server" />No</label>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group"><label for="inputEmail3" class="control-label"></label>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            </div>
             </div>
              <div class="row">
              <UcNextAppointment:UcNxtAppt ID="UserControlKNH_NextAppointment" runat="server" />
             </div>
                                                
                                            </div>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                            <act:CollapsiblePanelExtender ID="CPEIHistory" runat="server" SuppressPostBack="True"
                                ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnl2IHistory" CollapseControlID="pnl1IHistory"
                                ExpandControlID="pnl1IHistory" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                                ImageControlID="imghivrelhistory" Enabled="True"></act:CollapsiblePanelExtender>
                            <act:CollapsiblePanelExtender ID="CPEPComplaints" runat="server" SuppressPostBack="True"
                                ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnl2PComplaints" CollapseControlID="pnl1PComplaints"
                                ExpandControlID="pnl1PComplaints" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                                ImageControlID="imgpriorart" Enabled="True"></act:CollapsiblePanelExtender>
                            <act:CollapsiblePanelExtender ID="CPEExamination" runat="server" SuppressPostBack="True"
                                ExpandedImage="~/images/arrow-dn.gif" TargetControlID="Pnl2Examination" CollapseControlID="Pnl1Examination"
                                ExpandControlID="Pnl1Examination" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                                ImageControlID="imgAdherence" Enabled="True"></act:CollapsiblePanelExtender>
                            <%-- <act:CollapsiblePanelExtender ID="CPEMileStones" runat="server" SuppressPostBack="True"
                                ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnl2MileStones" CollapseControlID="pnl1MileStones"
                                ExpandControlID="pnl1MileStones" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                                ImageControlID="ImgARVSideEffect" Enabled="True"></act:CollapsiblePanelExtender>
                            <act:CollapsiblePanelExtender ID="CPEDisgonsis" runat="server" SuppressPostBack="True"
                                ExpandedImage="~/images/arrow-dn.gif" TargetControlID="Pnl2Diagnosis" CollapseControlID="Pnl1Diagnosis"
                                ExpandControlID="Pnl1Diagnosis" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                                ImageControlID="ImgARVSideEffect" Enabled="True"></act:CollapsiblePanelExtender>--%>
                            <act:CollapsiblePanelExtender ID="CPEManagmentPlan" runat="server" SuppressPostBack="True"
                                ExpandedImage="~/images/arrow-dn.gif" TargetControlID="Pnl2ManagementPlan" CollapseControlID="Pnl1ManagementPlan"
                                ExpandControlID="Pnl1ManagementPlan" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                                ImageControlID="ImgARVSideEffect" Enabled="True"></act:CollapsiblePanelExtender>
                            <act:CollapsiblePanelExtender ID="CPEREfAppointment" runat="server" SuppressPostBack="True"
                                ExpandedImage="~/images/arrow-dn.gif" TargetControlID="Pnl2RefAdminAppointment"
                                CollapseControlID="Pnl1RefAdminAppointment" ExpandControlID="Pnl1RefAdminAppointment"
                                CollapsedImage="~/images/arrow-up.gif" Collapsed="True" ImageControlID="ImgARVSideEffect"
                                Enabled="True"></act:CollapsiblePanelExtender>
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