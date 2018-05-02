<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true" Inherits="Pharmacy_frmPharmacyForm" Codebehind="frmPharmacyForm.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" TagPrefix="act" Namespace="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
<script type="text/javascript">
    function RegisterJQuery() {
        $('#txtDOB').datepicker({ autoclose: true });
        $('#txtpharmAppntDate').datepicker({ autoclose: true });
        $('#txtpharmOrderedbyDate').datepicker({ autoclose: true });
        $('#txtpharmReportedbyDate').datepicker({ autoclose: true });
    }
    //Calling RegisterJQuery when document is ready (Page loaded first time)
    $(document).ready(RegisterJQuery);
    //Calling RegisterJQuery when the page is doing postback (asp.net)
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RegisterJQuery);
</script>
    <script language="javascript" type="text/javascript">
        function CalculateDrugsPrescribed(txtMorning, txtMidday, txtEvening, txtNight, txtDuration, txtQtyPrescribed, valSyrup, valQtyUnitDisp) {
            var morning = document.getElementById(txtMorning).value;
            var midday = document.getElementById(txtMidday).value;
            var evening = document.getElementById(txtEvening).value;
            var night = document.getElementById(txtNight).value;
            var duration = document.getElementById(txtDuration).value;

            if (morning == "") morning = 0;
            if (midday == "") midday = 0;
            if (evening == "") evening = 0;
            if (night == "") night = 0;

            document.getElementById(txtMorning).value = morning;
            document.getElementById(txtMidday).value = midday;
            document.getElementById(txtEvening).value = evening;
            document.getElementById(txtNight).value = night;

            if ((morning + midday + evening + night) > 0 && duration != "") {
                if (valSyrup == "1") {
                    var qty = ((1 * morning + 1 * midday + 1 * evening + 1 * night) * duration) / valQtyUnitDisp;
                    document.getElementById(txtQtyPrescribed).value = Math.ceil(qty);
                }
                else {
                    var qty = duration * (1 * morning + 1 * midday + 1 * evening + 1 * night);
                    document.getElementById(txtQtyPrescribed).value = qty;
                }
            }
            else {
                document.getElementById(txtQtyPrescribed).value = "";
            }
        }

        function WindowPrint() {
            window.print();
        }

        function fnCheckUnCheck() {
            var chk = document.getElementById('<%=hidchkbox.ClientID %>').value;
            var chksplit = chk.split(',');
            if (document.getElementById('<%=ddlTreatment.ClientID %>').value == "222") {

                for (var i = 0; i < chksplit.length; i++) {
                    var cid = "ctl00_IQCareContentPlaceHolder_" + chksplit[i];
                    if (document.getElementById(cid) != null) {
                        document.getElementById(cid).disabled = true;
                    }
                }
            }
            else {
                for (var i = 0; i < chksplit.length; i++) {
                    var cid = "ctl00_IQCareContentPlaceHolder_" + chksplit[i];
                    if (document.getElementById(cid) != null) {
                        document.getElementById(cid).disabled = false;
                    }
                }
            }
            if (document.getElementById('<%=ddlTreatment.ClientID %>').value == "225") {
                document.getElementById('<%=pnlPedia.ClientID %>').disabled = true;



            }
            else {
                document.getElementById('<%=pnlPedia.ClientID %>').disabled = false;


            }
        }

        function Redirect(id, name, status) {

            if (name == "Add") {
                window.location.href = "../ClinicalForms/frmPatient_Home.aspx";
            }
            if (name == "Edit") {
                window.location.href = "../ClinicalForms/frmPatient_History.aspx";
            }
        }
        function CalculateTotalDailyDose(valsingleDose, valFrequency, valduration,valTotDose) {

            var vardose = document.getElementById(valsingleDose).value; // to get value of text box
            var selText = document.getElementById(valFrequency).options[document.getElementById(valFrequency).selectedIndex].text // to get selected text
            var varduration = document.getElementById(valduration).value;
            var result = Pharmacy_frmPharmacyForm.fnGetFrequencyMultiplier(selText).value;
            if (result != "0" && vardose!="" && valduration!="" ) {
                var TotalDose = vardose * varduration * result
                document.getElementById(valTotDose).value = TotalDose;
            }
            else {
                document.getElementById(valTotDose).value = "";
            }



        }


        function fnchecked(blnchecked, drugInstructions) {
            var drugid = blnchecked.substring(20);
            //alert(drugid);
            
            if (document.getElementById("ctl00_IQCareContentPlaceHolder_" + blnchecked).checked) {
                //document.getElementById("divPtnIns" + drugid).style.display = "";
                
                document.getElementById("ctl00_IQCareContentPlaceHolder_lblPtnInstructions" + drugid).style.display = "";
                document.getElementById("ctl00_IQCareContentPlaceHolder_txtPtnInstructions" + drugid).style.display = "";
                document.getElementById("ctl00_IQCareContentPlaceHolder_txtPtnInstructions" + drugid).value = drugInstructions;
            }
            else {
                
                //document.getElementById("ptnIns" + drugid).value = "";
                //document.getElementById("ctl00_IQCareContentPlaceHolder_ptnIns" + drugid).value = "";
                //document.getElementById("divPtnIns" + drugid).style.display = "none";
                document.getElementById("ctl00_IQCareContentPlaceHolder_lblPtnInstructions" + drugid).style.display = "none";
                document.getElementById("ctl00_IQCareContentPlaceHolder_txtPtnInstructions" + drugid).value = "";
                document.getElementById("ctl00_IQCareContentPlaceHolder_txtPtnInstructions" + drugid).style.display = "none";
            }
            
        }
 
    </script>
    <script type="text/javascript">
        function ace1_itemSelected(source, e) {
        var results = eval('('  + e.get_value() + ')');
            var index = source._selectIndex;
            if (index != -1) {
                //source.get_element().value = removeHTMLTags(source.get_completionList().childNodes[index]._value);
                var hdCustID = $get('<%= hdCustID.ClientID %>');
                hdCustID.value = results.DrugId;
            }
        }


        function onClientPopulated(sender, e) {
            var propertyPeople = sender.get_completionList().childNodes;
            for (var i = 0; i < propertyPeople.length; i++) {
                            var div = document.createElement("span");
                            var results = eval('(' + propertyPeople[i]._value + ')');
                            div.innerHTML = "<span style=' float:right; font-weight:bold;margin-right: 5px;'> " + results.AvlQty + "</span>";
                            //div.innerHTML = results.AvlQty;
                            propertyPeople[i].appendChild(div);
                            
                        }

            }

        
    </script>
    <style type="text/css" >
        .autoextender
        {
            font-family: Courier New, Arial, sans-serif;
            font-size: 11px;
            font-weight: 100;
            border: solid 1px #006699;
            line-height: 15px;
            padding: 0px;
            background-color: White;
            margin-left: 0px;
            width: 800px;
        }
        .autoextenderlist
        {
            cursor: pointer;
            color: black;
        }
        .autoextenderhighlight
        {
            color: White;
            background-color: #006699;
            cursor: pointer;
        }
        #divwidth
        {
            width: 800px !important;
            
        }
        #divwidth div
        {
            width: 800px !important;
        }
        .scrollable { width:100%; } 
    </style>

     <asp:UpdatePanel ID="Updatepanel" runat="server">
            <ContentTemplate>
    <div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">Pharmacy Form</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
         <div class="row">
         <br />
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Age:</label>
             </div>
             <div class="col-md-5 col-sm-12 col-xs-12 form-group">
             <div class="row">
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <div style="width: 100%; margin: 0px auto;">
<div style="float: left"><asp:TextBox ID="txtYr" runat="server" Width="80" CssClass="form-control"></asp:TextBox></div>
<div style="float: left">Yrs</div>
</div>
             
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <div style="width: 100%; margin: 0px auto;">
<div style="float: left"><asp:TextBox ID="txtMon" runat="server" Width="80" CssClass="form-control"></asp:TextBox></div>
<div style="float: left">Months</div>
</div>
             </div>
            
             </div>
             
             
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">DOB:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtDOB" clientidmode="Static"
                                                        runat="server" data-date-format="dd-M-yyyy" style="width:110px;"/>
                                                </div>
                                            </div>             
                                    <asp:HiddenField ID="hidchkbox" runat="server" />
             </div>                      
    </div>
    <div class="row">
     <div class="col-md-4 col-sm-12 col-xs-12 form-group">
     <div style="width: 100%; margin: 0px auto;">
<div style="float: left;margin-right:5px;"><label class="control-label">Weight:</label></div>
<div style="float: left"><asp:TextBox ID="txtWeight" MaxLength="5" runat="server" Width="80" CssClass="form-control"></asp:TextBox></div>
<div style="float: left">Kg<label class="smalllabel" id="dtwt" runat="server"></label></div>
</div>
     </div>
 
             
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
            <div style="width: 100%; margin: 0px auto;">
<div style="float: left;margin-right:5px;"><label class="control-label">Height:</label></div>
<div style="float: left"><asp:TextBox ID="txtHeight" MaxLength="4" runat="server" Width="80" CssClass="form-control"></asp:TextBox></div>
<div style="float: left">cm<label class="smalllabel" id="dtht" runat="server" ></label></div>
</div>
             </div>
             
            
              <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <div style="width: 100%; margin: 0px auto;">
<div style="float: left;margin-right:5px;"><label class="control-label">Body Surface Area:</label></div>
<div style="float: left">
<asp:TextBox  ID="txtBSA" runat="server" Width="80" CssClass="form-control" />
<asp:Label ID="lblBSA" runat="server" Text=""></asp:Label>
              </div>
<div style="float: left"><label class="patientInfo"></label>m<sup>2</sup></div>
</div>
             </div>

              
             
              
                                  
    </div>
    <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label required">Treatment Program:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlTreatment" runat="server" Style="z-index: 2" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlTreatment_SelectedIndexChanged" CssClass="form-control">
                                                </asp:DropDownList>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label required">Period Taken:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlPeriodTaken" runat="server" Style="z-index: 2" CssClass="form-control">
                                                </asp:DropDownList>
             </div>      
              <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label  class="control-label required">Drug Provider:</label>
             </div> 
              <div class="col-md-2 col-sm-12 col-xs-12 form-group" >
             <asp:DropDownList ID="ddlProvider" runat="server" Style="z-index: 2" CssClass="form-control" Width="160"></asp:DropDownList>
             </div>                 
    </div>
    <div class="row" id="trHIVsetFields" runat="server" visible="true">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label required">Regimen Line:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlregimenLine" runat="server" Style="z-index: 2" CssClass="form-control"></asp:DropDownList>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label required">Next Appointment Date:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtpharmAppntDate" clientidmode="Static"
                                                       maxlength="11" size="11" name="pharmAppointmentDate" runat="server" data-date-format="dd-M-yyyy" style="width:110px;"/>
                                                </div>
                                            </div>

            
             </div> 
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label  class="control-label required">Reason:</label>
             </div> 
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlAppntReason" runat="server" Style="z-index: 2" CssClass="form-control"></asp:DropDownList>
             </div>                      
    </div>
    <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">   
 <label class="control-label required">Select Drug:</label>         
 
             </div>
             <div class="col-md-10 col-sm-12 col-xs-12 form-group">            
             <asp:TextBox ID="txtautoDrugName" Width="400px" runat="server" AutoPostBack="true"
                                            AutoComplete="off" OnTextChanged="txtautoDrugName_TextChanged" Font-Names="Courier New" CssClass="form-control"></asp:TextBox>
                                        <div id="divwidth" >
                                        </div>
                                        
                                        <act:AutoCompleteExtender ServiceMethod="SearchDrugs" MinimumPrefixLength="2" CompletionInterval="30"
                                            EnableCaching="false" CompletionSetCount="10" TargetControlID="txtautoDrugName" BehaviorID="AutoCompleteEx"
                                            OnClientItemSelected="ace1_itemSelected" ID="AutoCompleteExtender1" runat="server" OnClientPopulated="onClientPopulated"
                                            FirstRowSelected="false" CompletionListCssClass="autoextender" CompletionListItemCssClass="autoextenderlist"
                                            CompletionListHighlightedItemCssClass="autoextenderhighlight" CompletionListElementID="divwidth">
                                           <Animations>
                                              <OnShow>
                                              <Sequence>
                                              <%-- Make the completion list transparent and then show it --%>
                                              <OpacityAction Opacity="0" />
                                              <HideAction Visible="true" />

                                              <%--Cache the original size of the completion list the first time
                                                the animation is played and then set it to zero --%>
                                              <ScriptAction Script="// Cache the size and setup the initial size
                                                                            var behavior = $find('AutoCompleteEx');
                                                                            if (!behavior._height) {
                                                                                var target = behavior.get_completionList();
                                                                                behavior._height = target.offsetHeight - 2;
                                                                                target.style.height = '0px';
                                                                            }" />
                                              <%-- Expand from 0px to the appropriate size while fading in --%>
                                              <Parallel Duration=".4">
                                              <FadeIn />
                                              <Length PropertyKey="height" StartValue="0" 
	                                            EndValueScript="$find('AutoCompleteEx')._height" />
                                              </Parallel>
                                              </Sequence>
                                              </OnShow>
                                              <OnHide>
                                              <%-- Collapse down to 0px and fade out --%>
                                              <Parallel Duration=".4">
                                              <FadeOut />
                                              <Length PropertyKey="height" StartValueScript="$find('AutoCompleteEx')._height" EndValue="0" />
                                              </Parallel>
                                              </OnHide>
                                              </Animations>
                                        </act:AutoCompleteExtender>
                                        <asp:HiddenField ID="hdCustID" runat="server" />
             </div>
                                
    </div>
    <div class="row" id="pnlARV" runat="server" visible="false">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <asp:Panel ID="pnlPedia" runat="server" Height="100%" Width="100%" Wrap="true">
                                        </asp:Panel>
             </div>
                              
    </div>
    <div class="row" id="pnlOI" runat="server" visible="false">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
           <asp:Panel ID="PnlOIARV" runat="server" Height="100%" Width="100%" Wrap="true">
                                        </asp:Panel>

                                        <script language="javascript" type="text/javascript">
                                            function GetControl() {
                                                document.forms[0].submit();
                                            }

                                            function CalcualteBSF(txtBSF, Weight, Height) {
                                                var YR1 = document.getElementById(Weight).value;
                                                var YR2 = document.getElementById(Height).value;
                                                if (YR1 == "" || YR2 == "") {
                                                    YR1 = 0;
                                                    YR2 = 0;
                                                }

                                                YR1 = parseInt(YR1);
                                                YR2 = parseInt(YR2);

                                                var BSF = Math.sqrt(YR1 * YR2 / 3600);
                                                BSF = BSF.toFixed(2);
                                                document.getElementById(txtBSF).value = BSF;
                                            }



                                        
                                    </script>
             </div>                               
    </div>
       <div class="row" id="pnlOther" runat="server" visible="false">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
            <asp:Panel ID="PnlOtMed" runat="server" Height="100%" Width="100%" Wrap="true">
                                        </asp:Panel>
             </div>                               
    </div>
       <div class="row" id="pnlTB" runat="server" visible="false">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
           <asp:Panel ID="pnlOtherTBMedicaton" runat="server" Height="100%" Width="100%" Wrap="true">
                                        </asp:Panel>
             </div>                               
    </div>
    <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <h4>Prescription Notes</h4>
             </div>                             
    </div>

    <div class="row">
 <div class="col-md-11 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtClinicalNotes" TextMode="MultiLine" CssClass="form-control" Width="100%" Height="80" runat="server" Style="resize:none;"></asp:TextBox>
             </div>                              
    </div>
    <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <h4>Approval and Signatures</h4>
             </div>
                             
    </div>
    <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label required">Prescribed by:</label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlPharmOrderedbyName" runat="Server" CssClass="form-control"></asp:DropDownList>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label required">Prescribed By Date:</label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtpharmOrderedbyDate" clientidmode="Static"
                                                       maxlength="11" size="11" name="pharmOrderedbyDate" runat="server" data-date-format="dd-M-yyyy" style="width:110px;"/>
                                                </div>
                                            </div>
             
             </div>                      
    </div>
    <div class="row" id="trDispense" runat="server">
    <asp:Panel runat="server" ID="pnlDispBy">

 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label id="lbldispensedby" class="required" runat="server">Dispensed by :</label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
            <asp:DropDownList ID="ddlPharmReportedbyName" runat="server" CssClass="form-control"></asp:DropDownList>
             </div>
             </asp:Panel>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
              <label id="lbldispensedbydate" class="required" runat="server" for="pharmReportedbyDate">Dispensed by Date:</label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtpharmReportedbyDate" clientidmode="Static"
                                                       maxlength="11" size="11" name="pharmReportedbyDate" runat="server" data-date-format="dd-M-yyyy" style="width:110px;"/>
                                                </div>
                                            </div>            
             </div>                      
    </div>
    <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label required">Signature:</label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
              <asp:DropDownList ID="ddlPharmSignature" runat="server" OnSelectedIndexChanged="ddlPharmSignature_SelectedIndexChanged" CssClass="form-control">
                                        <asp:ListItem Selected="True" Text="Select"></asp:ListItem>
                                        <asp:ListItem Text="No Signature"></asp:ListItem>
                                        <asp:ListItem Text="Patient's Signature"></asp:ListItem>
                                        <asp:ListItem Text="Adherance counsellor signature"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlCounselerName" runat="server" Visible="false" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlCounselerName_SelectedIndexChanged"  CssClass="form-control">
                                    </asp:DropDownList>

             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            <label id="lblCounselorName" class="margin20" visible="false" runat="server">
                                        Specify Counselor Name:
                                    </label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
              <asp:TextBox ID="txtCounselorName" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
              <asp:Label ID="Label2" runat="server" Visible="false" Text="Label"></asp:Label>
             </div>                      
    </div>
     <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
  <asp:Panel ID="pnlCustomList" Visible="false" runat="server" Height="100%" Width="100%"
                                        Wrap="true">
                                    </asp:Panel>
 </div>
 </div>
  <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnsave_Click" CssClass="btn btn-primary"  Width="8%" Height="30px"/>
  <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -2%; margin-right: 2%;
                                vertical-align: sub; color: #fff;"></label>
   <asp:Button ID="btncancel" runat="server" Text="Close" OnClick="btncancel_Click" Width="8%" Height="30px" CssClass="btn btn-primary"/>
    <label class="glyphicon glyphicon-remove" style="margin-left: -2%; margin-right: 2%;
                                vertical-align: sub; color: #fff;"></label>
                                    <asp:Button ID="btnOk" runat="server" CssClass="btn btn-primary" Text="OK" OnClick="btnOk_Click" Width="8%" Height="30px"/>
                                     <label class="glyphicon glyphicon-ok" style="margin-left: -2%; margin-right: 2%;
                                vertical-align: sub; color: #fff;"></label>

                                    <asp:Button ID="btnPrint" Text="Print Pharmacy Form" runat="server" CssClass="btn btn-primary" OnClientClick="WindowPrint()" Width="15%" Height="30px"/>
                                    <label class="glyphicon glyphicon-print" style="margin-left: -2%; margin-right: 2%;
                                vertical-align: sub; color: #fff;"></label>

                                    <asp:Button ID="btnPresPrint" Text="Print Prescription" runat="server" 
                                        onclick="btnPresPrint_Click" Width="13%" Height="30px" CssClass="btn btn-primary"/>
                                        <label class="glyphicon glyphicon-print" style="margin-left: -2%; margin-right: 2%;
                                vertical-align: sub; color: #fff;"></label>
                                    <asp:Button ID="btnPrintLabel" Text="Print Label" runat="server"  Visible="false"
                                        onclick="btnPrintLabel_Click" Width="8%" Height="30px" CssClass="btn btn-primary"/>
                                        <label class="glyphicon glyphicon-print" style="margin-left: -2%; margin-right: 2%;
                                vertical-align: sub; color: #fff;"></label>
 </div>
 </div>
	 <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>
     </div>
     </div>
     </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="txtautoDrugName"></asp:PostBackTrigger>
                <asp:PostBackTrigger ControlID="btnsave"></asp:PostBackTrigger>
                <asp:PostBackTrigger ControlID="btncancel"></asp:PostBackTrigger>
                <asp:PostBackTrigger ControlID="btnPrint"></asp:PostBackTrigger>
                <asp:PostBackTrigger ControlID="btnPresPrint"></asp:PostBackTrigger>
                <asp:PostBackTrigger ControlID="btnPrintLabel"></asp:PostBackTrigger>
            </Triggers>
        </asp:UpdatePanel>
    <%--    <form id="frmPharmacyForm" method="post" runat="server" title="Pharmacy Form">--%>
    
</asp:Content>
