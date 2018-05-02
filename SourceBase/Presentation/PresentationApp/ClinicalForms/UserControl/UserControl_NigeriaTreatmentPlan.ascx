<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControl_NigeriaTreatmentPlan.ascx.cs"
    Inherits="PresentationApp.ClinicalForms.UserControl.UserControl_NigeriaTreatmentPlan" %>
<%@ Register Src="~/ClinicalForms/UserControl/UserControlKNH_LabEvaluation.ascx"
    TagName="Uclabeval" TagPrefix="uc11" %>
<script type="text/javascript">

    function getSelectedtableValue(DivId, DDText, str, tableId) {
        var e = document.getElementById(DDText);
        var text = e.options[e.selectedIndex].innerHTML;
        var YN = "";


        if (text == str) {
            YN = "show";
        }
        else {
            YN = "hide";
        }

        
        hideChklistEligiblethrough('<%=chklistARTchangecode.ClientID %>');
        
        ShowHide(DivId, YN);
    }

    function hideChklistEligiblethrough(controlId) {
        var elementRef = document.getElementById(controlId);
        var inputElementArray = elementRef.getElementsByTagName('INPUT');
        for (var i = 0; i < inputElementArray.length; i++) {
            if (inputElementArray[i].type == 'checkbox')
                inputElementArray[i].checked = false; ;
        }
    }
    function CheckBoxHideUnhide(strcblcontrolId, strdivId, strfieldname) {
        //alert(strcblcontrolId);
        var checkList = document.getElementById(strcblcontrolId);
        var checkBoxList = checkList.getElementsByTagName("input");
        var arrayOfCheckBoxLabels = checkList.getElementsByTagName("label");
        var checkBoxSelectedItems = new Array();

        for (var i = 0; i < checkBoxList.length; i++) {

            if (checkBoxList[i].checked) {
                if (arrayOfCheckBoxLabels[i].innerHTML == strfieldname) {
                    ShowHide(strdivId, "show");
                }
                else {
                    ShowHide(strdivId, "hide");
                }
            }
            else {
                ShowHide(strdivId, "hide");
            }
        }

    }

    
</script>
<style type="text/css">
    .style2
    {
        width: 15%;
    }
</style>
<script language="javascript" type="text/javascript">
    
</script>
<div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
            <asp:CheckBox ID="ChkLabEvaluation" runat="server" Text="Lab Evaluation" onchange="ShowHideDiv('DivLabEval');" CssClass="checkbox-btn"/>
             </div>            
 </div>
 <div class="row" id="DivLabEval" style="display: none;">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <uc11:Uclabeval ID="UcLabEval" runat="server" />
 </div>
 </div>
 <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                <div id="divtreatment" style="margin-top: 10; margin-bottom: 10;">
                    <asp:GridView ID="gvtreatment" runat="server" AutoGenerateColumns="False" Width="100%"
                        GridLines="None" OnRowDataBound="gvtreatment_RowDataBound" ShowHeader="false">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lbltreatment" runat="server" Text='<%# Eval("ID")%>' Visible="false"></asp:Label>
                                    <asp:CheckBox ID="Chktreatment" runat="server" Text='<%# Eval("NAME")%>' CssClass="checkbox-btn"/>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>                            
                        </Columns>
                    </asp:GridView>
                </div>

 </div>
 </div>
 <div class="row" id="DivTreatmentOther" style="display: none;">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Specify other Referrals:</label>
             </div>
             <div class="col-md-9 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtOtherReferrals" runat="server" Width="99%" Rows="3" TextMode="MultiLine" CssClass="form-control" Style="resize:none:"></asp:TextBox>
             </div>
             
 </div>
 <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:CheckBox ID="chkregimen" runat="server" Text="Regimen" onchange="ShowHideDiv('DivPrescDrug');" CssClass="checkbox-btn"/>
 </div>
 </div>
 <div class="row" align="center" id="DivPrescDrug" style="display: none;">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <asp:Button ID="btnPrescribeDrugs" runat="server" Text="Prescribe Regimen" OnClick="btnPrescribeDrugs_Click" CssClass="btn btn-primary" Height="30"/>
             </div>
            
 </div>
 <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <div id="divenrollin">
                    <asp:GridView ID="gvenrollin" runat="server" AutoGenerateColumns="False" Width="100%"
                        GridLines="None" ShowHeader="false">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblenrollin" runat="server" Text='<%# Eval("ID")%>' Visible="false"></asp:Label>
                                    <asp:CheckBox ID="Chkenrollin" runat="server" Text='<%# Eval("NAME")%>' CssClass="checkbox-btn"/>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
             </div>
              
 </div>
 <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label required"><asp:Label ID="lblTreatmentplan" runat="server" Text="Treatment plan:"></asp:Label></label>
             </div>
             <div class="col-md-9 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlTreatmentplan" runat="server" OnSelectedIndexChanged="ddlTreatmentplan_SelectedIndexChanged" Width="90%" CssClass="form-control"></asp:DropDownList>
             </div>
             
 </div>
 <div class="row" id="divARTchangecode" style="display: none;">
 
 <div class="col-md-2 col-sm-12 col-xs-12 form-group text-nowrap">
             <label id="lblART change code-8888552" class="control-label">ART change code :</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group  text-nowrap">
             <div id="divchklistARTchangecode" enableviewstate="true" class="customdivbordermultiselect"
                                        runat="server">
                                        <asp:CheckBoxList ID="chklistARTchangecode" RepeatLayout="Flow" runat="server" CssClass="checkbox-btn">
                                        </asp:CheckBoxList>
                                    </div>
             </div>
             <div id="divSpecifyotherARTchangereason" style="display: none;">
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label id="lblSpecify other ART change reason-8888873" class="control-label">Specify other ART change reason:</label>           
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtSpecifyotherARTchangereason" runat="server" Width="99%" CssClass="form-control"></asp:TextBox>
             </div> 
             </div>
 
 </div>


