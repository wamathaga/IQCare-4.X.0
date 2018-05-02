<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControlNigeria_PhysicalExamination.ascx.cs"
    Inherits="PresentationApp.ClinicalForms.UserControl.UserControlNigeria_PhysicalExaminationascx" %>
<script type="text/javascript" language="javascript">
    function ShowHidePE(theDiv, YN, theFocus) {

        $(document).ready(function () {

            if (YN == "show") {
                $("#" + theDiv).slideDown();

            }
            if (YN == "hide") {
                $("#" + theDiv).slideUp();

            }

        });

    }
    function CheckBoxToggle(strcblcontrolId, strdivId, strfieldname) {
        var checkList = document.getElementById(strcblcontrolId);
        var checkBoxList = checkList.getElementsByTagName("input");
        var arrayOfCheckBoxLabels = checkList.getElementsByTagName("label");
        var checkBoxSelectedItems = new Array();

        for (var i = 0; i < checkBoxList.length; i++) {

            if (checkBoxList[i].checked) {
                if (arrayOfCheckBoxLabels[i].innerText == strfieldname) {
                    ShowHidePE(strdivId, "show");
                }
                else {
                    ShowHidePE(strdivId, "hide");
                }
            }
            else {
                ShowHidePE(strdivId, "hide");
            }
        }



    }
    function fnUncheckall(strcblcontrolId) {

        var checkList = document.getElementById(strcblcontrolId);
        var checkBoxList = checkList.getElementsByTagName("input");
        var arrayOfCheckBoxLabels = checkList.getElementsByTagName("label");
        var checkBoxSelectedItems = new Array();


        for (var i = 0; i < checkBoxList.length; i++) {
            if ($("label[for='" + checkBoxList[i].id + "']").text() != 'Normal') {
                //alert($("label[for='" + checkBoxList[i].id + "']").text());
                checkBoxList[i].checked = false;
            }

        }
    }
    function fnUncheckNormal(strcblcontrolId) {
        var checkList = document.getElementById(strcblcontrolId);
        var checkBoxList = checkList.getElementsByTagName("input");
        var arrayOfCheckBoxLabels = checkList.getElementsByTagName("label");
        var checkBoxSelectedItems = new Array();
        for (var i = 0; i < checkBoxList.length; i++) {
            if ($("label[for='" + checkBoxList[i].id + "']").text() == 'Normal') {
                checkBoxList[i].checked = false;
            }

        }

    }
    function CheckBoxToggleShowHidePE(val, divID, txt, txtControlId) {
        var checkList = document.getElementById(val);
        var checkBoxList = checkList.getElementsByTagName("input");
        var arrayOfCheckBoxLabels = checkList.getElementsByTagName("label");
        var checkBoxSelectedItems = new Array();
        var arrayOfCheckBoxLabelsnew;
        for (var i = 0; i < checkBoxList.length; i++) {
            if (checkBoxList[i].checked) {
                if ($("label[for='" + checkBoxList[i].id + "']").text() == txt) {
                    ShowHidePE(divID, "show");
                }

            }
            else {
                if ($("label[for='" + checkBoxList[i].id + "']").text() == txt) {
                    document.getElementById(txtControlId).value = '';
                    ShowHidePE(divID, "hide");
                }

            }
        }



    }




    function togglePhyExamPC(strcblcontrolId) {
        var GV = document.getElementById(strcblcontrolId);

        if (GV.children.length > 0) {
            for (var i = 0; i < GV.children.length; i++) {
                var inputs = GV.children[i].tagName;

                if (inputs == "INPUT" && GV.children[i].checked == true && GV.children[i].labels[0].innerText == "NSF") {
                    for (var j = 0; j < GV.children.length; j++) {
                        var inputsinner = GV.children[j].tagName;
                        if (inputsinner == "INPUT" && GV.children[j].labels[0].innerText != "NSF") {
                            GV.children[j].checked = false;
                            GV.children[j].disabled = true;
                        }
                    }

                }
                else if (inputs == "INPUT" && GV.children[i].checked == false && GV.children[i].labels[0].innerText == "NSF") {
                    for (var j = 0; j < GV.children.length; j++) {
                        var inputsinner = GV.children[j].tagName;
                        if (inputsinner == "INPUT" && GV.children[j].labels[0].innerText != "NSF") {
                            GV.children[j].disabled = false;
                        }
                    }

                }

                else if (inputs == "INPUT" && GV.children[i].checked == true && GV.children[i].labels[0].innerText != "NSF") {
                    for (var j = 0; j < GV.children.length; j++) {
                        var inputsinner = GV.children[j].tagName;
                        if (inputsinner == "INPUT" && GV.children[j].labels[0].innerText == "NSF") {
                            GV.children[j].checked = false;
                            GV.children[j].disabled = true;
                        }
                    }

                }

                else if (inputs == "INPUT" && GV.children[i].checked == false && GV.children[i].labels[0].innerText != "NSF") {
                    for (var j = 0; j < GV.children.length; j++) {
                        var inputsinner = GV.children[j].tagName;
                        if (inputsinner == "INPUT" && GV.children[j].labels[0].innerText == "NSF") {
                            GV.children[j].disabled = false;
                        }
                    }

                }


                //                if ((inputs[i].checked == true) && (lbl[i].innerText == "NSF")) {
                //                    for (var j = 1; j < GV.children.length; j++) {
                //                        var inputsinner = GV.children[j].getElementsByTagName('input');
                //                        var lblinner = GV.children[j].getElementsByTagName('label');
                //                        if (lblinner[j].innerText != "NSF") {
                //                            inputsinner[j].checked = false;
                //                            inputsinner[j].disabled = true;
                //                        }
                //                    }
                //                }
                //                else if ((inputs[i].checked == false) && (lbl[i].innerText == "NSF")) {
                //                    for (var k = 1; k < GV.children.length.length; k++) {
                //                        var inputsinnerouter = GV.children[i].getElementsByTagName('input');
                //                        var lblinnerouter = GV.children[i].getElementsByTagName('label');
                //                        if (lblinnerouter[k].innerText != "NSF") {
                //                            inputsinnerouter[k].checked = false;
                //                            inputsinnerouter[k].disabled = false;
                //                        }
                //                    }
                //                }
                //            }
                //        }
                //        else if ((inputList[0].checked == true) && (arrayOfCheckBoxLabels[0].innerText != "NSF")) {

                //            if (GV.childNodes.length > 0) {
                //                for (var i = 1; i < GV.childNodes.length; i++) {
                //                    var inputs = GV.childNodes[i].getElementsByTagName('input');
                //                    var lbl = GV.childNodes[i].getElementsByTagName('label');
                //                    if (arrayOfCheckBoxLabels[0].innerText == "NSF") {
                //                        inputs[0].checked = false;
                //                        
                //                    }                    
                //                }
                //            }
                //        }
                //        else if ((inputList[0].checked == false) && (arrayOfCheckBoxLabels[0].innerText == "NSF")) {
                //            if (GV.childNodes.length > 0) {
                //                for (var i = 1; i < GV.childNodes.length; i++) {
                //                    var inputs = GV.childNodes[i].getElementsByTagName('input');
                //                    var lbl = GV.childNodes[i].getElementsByTagName('label');
                //                    if (arrayOfCheckBoxLabels[0].innerText == "NSF") {
                //                        
                //                    }
                //                    else {                      
                //                                              
                //                        inputs[0].disabled = false;
                //                    }
                //                }
                //            }
                //        }

            }
        }
    }
</script>
<div class="row">
 <div class="col-md-6">
 <%--left--%>
 <div class="row">
 <div class="col-md-12">
             <label class="control-label"><asp:Label ID="lblNigeriaPEGeneral" runat="server" Text="General appearance :"></asp:Label></label>
             </div>   
             </div>
             <div class="row"> 
 <div class="col-md-12 col-sm-12 col-xs-12 form-group"> 
             <div id="divNigeriaPEGeneral" enableviewstate="true" class="customdivbordermultiselect"
                                    runat="server">
                                    <asp:CheckBoxList ID="cblNigeriaPEGeneral" runat="server" Width="99%" RepeatLayout="Flow" CssClass="checkbox-btn">
                                    </asp:CheckBoxList>
                                </div>            
             </div>       
 </div>
 <div class="row" id="divNigeriaPEGeneralOther" style="display: none;">    
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <label id="lblOther general conditions-8888326" class="control-label">Other general conditions :</label>             
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtOtherNigeriaPEGeneral" ClientIDMode="Static" runat="server" Width="99%" Rows="3" TextMode="MultiLine" CssClass="form-control" Style="resize:none;"></asp:TextBox>
             </div>
             </div>
<div class="row">
 <div class="col-md-12 ">
             <label class="control-label"><asp:Label ID="lblNigeriaPEHeadEyeEnt" runat="server" Text="Head/Eye/ENT :"></asp:Label></label>
             </div>
 </div>
 <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <div id="divNigeriaPEHeadEyeEnt" enableviewstate="true" class="customdivbordermultiselect"
                                    runat="server">
                                    <asp:CheckBoxList ID="cblNigeriaPEHeadEyeEnt" Width="100%" RepeatLayout="Flow" runat="server" CssClass="checkbox-btn">
                                    </asp:CheckBoxList>
                                </div>
             </div>
 </div>
 <div class="row" id="divOtherNigeriaPEHeadEyeEnt" style="display: none;">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
 <label id="lblOther oral cavity conditions-8888332" class="control-label">Other HeadEyeEnt:</label>           
             </div>    
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">  
             <asp:TextBox ID="txtOtherNigeriaPEHeadEyeEnt" ClientIDMode="Static" runat="server"
                                                        Width="99%" Rows="3" TextMode="MultiLine" CssClass="form-control" Style="resize:none;"></asp:TextBox>           
             </div>       
 </div>
 <div class="row">
 <div class="col-md-12 ">
             <label class="control-label"><asp:Label ID="lblNigeriaPEBreast" runat="server" Text="Breast :"></asp:Label></label>
             </div>
 </div>
 <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
      <div id="divNigeriaPEBreast" enableviewstate="true" class="customdivbordermultiselect"
                                    runat="server">
                                    <asp:CheckBoxList ID="cblNigeriaPEBreast" Width="99%" RepeatLayout="Flow" CssClass="checkbox-btn" runat="server">
                                    </asp:CheckBoxList>
                                </div>       
             </div>
 </div>
 <div class="row" id="divOtherNigeriaPEBreast" style="display: none;">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
 <label id="lblOther CNS conditions-8888336" class="control-label">Other Breast :</label>          
             </div>    
             <div class="col-md-6 col-sm-12 col-xs-12 form-group"> 
             <asp:TextBox ID="txtOtherNigeriaPEBreast" ClientIDMode="Static" runat="server" Width="99%"
                                                        Rows="3" TextMode="MultiLine" CssClass="form-control" Style="resize:none:"></asp:TextBox>            
             </div>       
 </div>
 <div class="row">
 <div class="col-md-12 ">
             <label class="control-label"><asp:Label ID="lblNigeriaPERespiratory" runat="server" Text="Respiratory :"></asp:Label></label>
             </div>
 </div>
 <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
       <div id="divNigeriaPERespiratory" enableviewstate="true" class="customdivbordermultiselect"
                                    runat="server">
                                    <asp:CheckBoxList ID="cblNigeriaPERespiratory" Width="99%" RepeatLayout="Flow" runat="server" CssClass="checkbox-btn">
                                    </asp:CheckBoxList>
                                </div>      
             </div>
 </div>
 <div class="row" id="divOtherNigeriaPERespiratory" style="display: none;">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
 <label id="lblOther skin conditions-8888340" class="control-label">Other Respiratory :</label>             
             </div>    
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">     
             <asp:TextBox ID="txtOtherNigeriaPERespiratory" ClientIDMode="Static" runat="server"
                                                        Width="99%" Rows="3" TextMode="MultiLine" CssClass="form-control" Style="resize:none:"></asp:TextBox>        
             </div>       
 </div>
 <div class="row">
 <div class="col-md-12 ">
             <label class="control-label"><asp:Label ID="lblNigeriaPENeurological" runat="server" Text="Neurological :"></asp:Label></label>
             </div>
 </div>
 <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
<div id="divNigeriaPENeurological" enableviewstate="true" class="customdivbordermultiselect"
                                    runat="server">
                                    <asp:CheckBoxList ID="cblNigeriaPENeurological" Width="99%" RepeatLayout="Flow"
                                        runat="server" CssClass="checkbox-btn">
                                    </asp:CheckBoxList>
                                </div>
             </div>
 </div>
 <div class="row" id="divOtherNigeriaPENeurological" style="display: none;">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
 <label id="Label2" class="control-label">Other Neurological :</label>
            
             </div>    
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">      
                 <asp:TextBox ID="txtOtherNigeriaPENeurological" ClientIDMode="Static" runat="server"
                                                        Width="99%" Rows="3" TextMode="MultiLine" CssClass="form-control" Style="resize:none:"></asp:TextBox>   
             </div>       
 </div>

 </div>
 <div class="col-md-6">
 <%--right--%>
 <div class="row">
 <div class="col-md-12">
             <label class="control-label"><asp:Label ID="lblNigeriaPESkin" runat="server" Text="Skin :"></asp:Label></label>
             </div> 
             </div>
<div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <div id="divNigeriaPESkin" enableviewstate="true" class="customdivbordermultiselect"
                                    runat="server">
                                    <asp:CheckBoxList ID="cblNigeriaPESkin" Width="100%" RepeatLayout="Flow" runat="server" CssClass="checkbox-btn">
                                    </asp:CheckBoxList>
                                </div>
 </div>
 </div>
 <div class="row" id="divOtherNigeriaPESkin" style="display: none;">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
 <label id="lblOther cardiovascular condition-8888330" class="control-label">Other Skin condition :</label>
           
             </div>    
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">   
                <asp:TextBox ID="txtOtherNigeriaPESkin" ClientIDMode="Static" runat="server" Width="99%"
                                                        Rows="3" TextMode="MultiLine" CssClass="form-control" Style="resize:none;"></asp:TextBox>       
             </div>       
 </div>     
 <div class="row">
 <div class="col-md-12 ">
             <label class="control-label"><asp:Label ID="lblNigeriaPECardiovascular" runat="server" Text="Cardiovascular :"></asp:Label></label>
             </div>
 </div>
 <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <div id="divNigeriaPECardiovascular" enableviewstate="true" class="customdivbordermultiselect"
                                    runat="server">
                                    <asp:CheckBoxList ID="cblNigeriaPECardiovascular" Width="99%" RepeatLayout="Flow"
                                        runat="server" CssClass="checkbox-btn">
                                    </asp:CheckBoxList>
                                </div>
             </div>
 </div>
 <div class="row" id="divOtherNigeriaPECardiovascular" style="display: none;">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
  <label id="lblOther genital urinary conditions-8888334" class="control-label">Other Cardiovascular :</label>           
             </div>    
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">      
             <asp:TextBox ID="txtOtherNigeriaPECardiovascular" ClientIDMode="Static" runat="server"
                                                        Width="99%" Rows="3" TextMode="MultiLine" CssClass="form-control" Style="resize:none:"></asp:TextBox>       
             </div>       
 </div>
 <div class="row">
 <div class="col-md-12 ">
             <label class="control-label"><asp:Label ID="lblNigeriaPEGenitalia" runat="server" Text="Genitalia :"></asp:Label></label>
             </div>
 </div>
 <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <div id="divNigeriaPEGenitalia" enableviewstate="true" class="customdivbordermultiselect"
                                    runat="server">
                                    <asp:CheckBoxList ID="cblNigeriaPEGenitalia" Width="99%" CssClass="checkbox-btn" RepeatLayout="Flow" runat="server">
                                    </asp:CheckBoxList>
                                </div>
             </div>
 </div>
 <div class="row" id="divOtherNigeriaPEGenitalia" style="display: none;">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
 <label id="lblOther chest lungs conditions-8888338" class="control-label">Other Genitalia :</label>             
             </div>    
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">   
             <asp:TextBox ID="txtOtherNigeriaPEGenitalia" ClientIDMode="Static" runat="server"
                                                        Width="99%" Rows="3" TextMode="MultiLine" CssClass="form-control" Style="resize:none:"></asp:TextBox>          
             </div>       
 </div>
 <div class="row">
 <div class="col-md-12 ">
             <label class="control-label"><asp:Label ID="lblNigeriaPEGastrointestinal" runat="server" Text="Gastrointestinal :"></asp:Label></label>
             </div>
 </div>
 <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
  <div id="divNigeriaPEGastrointestinal" enableviewstate="true" class="customdivbordermultiselect"
                                    runat="server">
                                    <asp:CheckBoxList ID="cblNigeriaPEGastrointestinal" onclick="CheckBoxToggleShortTerm();"
                                        Width="99%" RepeatLayout="Flow" runat="server" CssClass="checkbox-btn">
                                    </asp:CheckBoxList>
                                </div>           
             </div>
 </div>
 <div class="row" id="divOtherNigeriaPEGastrointestinal" style="display: none;">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
 <label id="lblOther abdomen conditions-8888328" class="control-label">Other Gastrointestinal:</label>             
             </div>    
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">         
             <asp:TextBox ID="txtOtherNigeriaPEGastrointestinal" ClientIDMode="Static" runat="server"
                                                        Width="99%" Rows="3" TextMode="MultiLine" CssClass="form-control" Style="resize:none:"></asp:TextBox>    
             </div>       
 </div>
 <div class="row">
 <div class="col-md-12 ">
             <label class="control-label"><asp:Label ID="lblNigeriaPEMentalstatus" runat="server" Text="Mental status :"></asp:Label></label>
             </div>
 </div>
 <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
   <div id="divNigeriaPEMentalstatus" enableviewstate="true" class="customdivbordermultiselect"
                                    runat="server">
                                    <asp:CheckBoxList ID="cblNigeriaPEMentalstatus" Width="100%" RepeatLayout="Flow"
                                        runat="server" CssClass="checkbox-btn">
                                    </asp:CheckBoxList>
                                </div>          
             </div>
 </div>
 <div class="row" id="divOtherNigeriaPEMentalstatus" style="display: none;">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
 <label id="Label4" class="control-label">Other Gastrointestinal:</label>             
             </div>    
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">   
             <asp:TextBox ID="txtOtherNigeriaPEMentalstatus" ClientIDMode="Static" runat="server"
                                                        Width="99%" Rows="3" TextMode="MultiLine" CssClass="form-control" Style="resize:none:"></asp:TextBox>          
             </div>       
 </div>     
 </div>
 
 </div>
 <div class="row">
 <div class="col-md-12">
 <label id="lblAdditional medical condition notes-8888564" class="control-label">Additional and detailed findings :</label>
             </div>             
 </div>
 <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
<asp:TextBox ID="txtOtherAdditionaldetailedfindings" Columns="20" Rows="3" Width="99%"
                                    TextMode="MultiLine" runat="server" CssClass="form-control" Style="resize:none:"></asp:TextBox>
             </div>
            
 </div>


