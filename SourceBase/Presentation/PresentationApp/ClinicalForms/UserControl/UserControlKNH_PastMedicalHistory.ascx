<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControlKNH_PastMedicalHistory.ascx.cs"
    Inherits="PresentationApp.ClinicalForms.UserControl.UserControlKNH_PresComplaints" %>
<%--<script language="javascript" type="text/javascript">
    function CheckBoxToggleShowHide1(val, divID, txt) {

        var checkList = document.getElementById(val);
        var checkBoxList = checkList.getElementsByTagName("input");
        var arrayOfCheckBoxLabels = checkList.getElementsByTagName("label");
        var checkBoxSelectedItems = new Array();

        for (var i = 0; i < checkBoxList.length; i++) {

            if (checkBoxList[i].checked) {
                if (arrayOfCheckBoxLabels[i].innerText == txt) {
                    ShowHide(divID, "show");
                }

            }
            else {
                if (arrayOfCheckBoxLabels[i].innerText == txt) {
                    ShowHide(divID, "hide");
                }
            }
        }



    }

</script>--%>
<table cellspacing='6' cellpadding='0' width='100%' border='0'>
    <tr>
        <td class='border center pad5 whitebg' colspan="2">
            <table width='100%'>
                <tr>
                    <td style='width: 18%' align='right'>
                        <asp:Label ID="lblMedicalcondition" runat="server" CssClass="required" Font-Bold="True"
                            Text="Medical condition:"></asp:Label>
                    </td>
                    <td style='width: 32%' align='left'>
                        <asp:RadioButtonList ID="rdoMedicalCondition" runat="server" RepeatDirection="Horizontal"
                            OnClick="rblSelectedValue(this,'divcblSpecificMedicalCondition')">
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td style='width: 50%'>
                        <div id="divcblSpecificMedicalCondition" style="display: none;">
                            <table width="100%">
                                <tr>
                                    <td align='left'>
                                        <label class="control-label" align='center' id='lblPre existing medical condition-8888899'>
                                            Pre existing medical condition :</label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style='width: 100%'>
                                        <div id="divSpecificMedicalCondition" enableviewstate="true" class="customdivbordermultiselect"
                                            runat="server">
                                            <asp:CheckBoxList ID="cblSpecificMedicalCondition" Width="100%" runat="server">
                                            </asp:CheckBoxList>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <div id="divothermedconditon" style="display: none;">
                                            <table width='100%'>
                                                <tr>
                                                    <td style='width: 55%' align='right'>
                                                        <label align='center' id='Label1' class="control-label">
                                                            Specify other medical condition :</label>
                                                    </td>
                                                    <td style='width: 50%' align='left'>
                                                        <asp:TextBox ID="txtothermedconditon" Width="180px" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class='border center pad5 whitebg' colspan="2">
            <table width="50%">
                <tr>
                    <td align='left'>
                        <label align='center' id='lblSurgical Conditions-8888596' class="control-label">
                            Surgical Conditions :</label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="divSurgicalConditions" enableviewstate="true" style="border-right: #666699 1px solid;
                            border-top: #666699 1px solid; border-left: #666699 1px solid; border-bottom: #666699 1px solid;
                            width: 100%; height: 100px; overflow: auto; text-align: left;" runat="server">
                            <asp:CheckBoxList ID="cblSurgicalConditions" onclick="CheckBoxToggleShortTerm();"
                                Width="100%" RepeatLayout="Flow" runat="server">
                            </asp:CheckBoxList>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <div id="divCurrentSurgicalConditionYN" style="display: none;">
                            <table width='100%'>
                                <tr>
                                    <td style='width: 55%' align='right'>
                                        <label align='center' id='lblSpecify current surgical condition-8888597' class="control-label">
                                            Specify current surgical condition :</label>
                                    </td>
                                    <td style='width: 50%' align='left'>
                                        <asp:TextBox ID="txtCurrentSurgicalCondition" Width="180px" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="divPreviousSurgicalCondition" style="display: none;">
                            <table width='100%'>
                                <tr>
                                    <td style='width: 55%' align='right'>
                                        <label align='center' id='lblSpecify previous surgical condition-8888598' class="control-label">
                                            Specify previous surgical condition :</label>
                                    </td>
                                    <td style='width: 50%' align='left'>
                                        <asp:TextBox ID="txtPreviousSurgicalCondition" Width="180px" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class='border center pad5 whitebg' colspan="2">
            <table width='100%'>
                <tr>
                    <td style='width: 50%' align='right'>
                        <label align='center' id='lblCurrent long term medications-8888900' class="control-label">
                            Current long term medications :</label>
                    </td>
                    <td style='width: 50%' align='left'>
                        <asp:RadioButtonList ID="rdoPreExistingMedicalConditionsFUP" runat="server" RepeatDirection="Horizontal"
                            OnClick="rblSelectedValue(this,'trAntihypertensives');rblSelectedValue(this,'trHypoglycemics');rblSelectedValue(this,'trothers');">
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="trAntihypertensives" style="display: none;">
        <td class='border center pad5 whitebg' style='width: 50%'>
            <%--<div id="divAntihypertensives" style="display: none;">--%>
            <table width='100%'>
                <tr>
                    <td align='left'>
                        <label align='center' id='lblAntihypertensives-8888426' class="control-label">
                            Antihypertensives :</label>
                    </td>
                </tr>
                <tr>
                    <td style='width: 60%' align='left'>
                        <asp:TextBox ID="txtAntihypertensives" Columns="20" Rows="3" Width="100%" TextMode="MultiLine"
                            runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <%--</div>--%>
        </td>
        <td class='border center pad5 whitebg' style='width: 50%'>
            <%--<div id="divAnticonvulsants" style="display: none;">--%>
            <table width='100%'>
                <tr>
                    <td align='left'>
                        <label align='center' id='lblAnticonvulsants-8888429' class="control-label">
                            Anticonvulsants :</label>
                    </td>
                </tr>
                <tr>
                    <td align='left'>
                        <asp:TextBox ID="txtAnticonvulsants" Columns="20" Rows="3" Width="100%" TextMode="MultiLine"
                            runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <%--</div>--%>
        </td>
    </tr>
    <tr id="trHypoglycemics" style="display: none;">
        <td class='border center pad5 whitebg' style='width: 50%'>
            <%--<div id="divHypoglycemics" style="display: none;">--%>
            <table width='100%'>
                <tr>
                    <td align='left'>
                        <label align='center' id='lblHypoglycemics-8888427' class="control-label">
                            Hypoglycemics :</label>
                    </td>
                </tr>
                <tr>
                    <td align='left'>
                        <asp:TextBox ID="txtHypoglycemics" Columns="20" Rows="3" Width="100%" TextMode="MultiLine"
                            runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <%--</div>--%>
        </td>
        <td class='border center pad5 whitebg' style='width: 50%'>
            <table width='100%'>
                <tr>
                    <td align='left'>
                        <label align='center' id='lblRadiotherapy/Chemotherapy -8888901' class="control-label">
                            Radiotherapy/Chemotherapy :</label>
                    </td>
                </tr>
                <tr>
                    <td align='left'>
                        <asp:TextBox ID="txtRadiotherapyChemotherapy" Columns="20" Rows="3" Width="100%"
                            TextMode="MultiLine" runat="server">
                        </asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="trothers" style="display: none;">
        <td class='border center pad5 whitebg' colspan="2">
            <table width='100%'>
                <tr>
                    <td align='left'>
                        <label align='center' id='Label2' class="control-label">
                            Others :</label>
                    </td>
                </tr>
                <tr>
                    <td align='left'>
                        <asp:TextBox ID="txtothers" Columns="20" Rows="3" Width="100%" TextMode="MultiLine"
                            runat="server">
                        </asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class='border center pad5 whitebg' colspan="2">
            <table width='100%'>
                <tr>
                    <td style='width: 30%' align='right'>
                        <label align='center' id='lblPreviously admitted since last clinic-8888599' class="control-label">
                            Previously admitted since last clinic :</label>
                    </td>
                    <td style='width: 20%' align='left'>
                        <asp:RadioButtonList ID="rdoPreviousAdmission" runat="server" RepeatDirection="Horizontal"
                            OnClick="rblSelectedValue(this,'divPreviousAdmissionDiagnosis');rblSelectedValue(this,'trPreviousAdmission');">
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td style='width: 50%'>
                        <div id="divPreviousAdmissionDiagnosis" style="display: none;">
                            <table width='100%'>
                                <tr>
                                    <td style='width: 40%' align='right'>
                                        <label align='center' id='lblDiagnosis-8888600' class="control-label">
                                            Diagnosis :</label>
                                    </td>
                                    <td style='width: 60%' align='left'>
                                        <asp:TextBox ID="txtPreviousAdmissionDiagnosis" Width="180px" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="trPreviousAdmission" style="display: none;">
        <td class='border center pad5 whitebg' style='width: 50%'>
            <%--<div id="divPreviousAdmissionStart" style="display: none;">--%>
            <table width='100%'>
                <tr>
                    <td style='width: 40%' align='right'>
                        <label align='center' id='lblAdmission start-8888601' class="control-label">
                            Admission start :</label>
                    </td>
                    <td style='width: 60%' align='left'>
                        <input id="txtPreviousAdmissionStart" maxlength="11" size="8" name="visitDate" runat="server" />
                        <img onclick="w_displayDatePicker('<%= txtPreviousAdmissionStart.ClientID%>');" height="22"
                            alt="Date Helper" hspace="5" src="../../images/cal_icon.gif" width="22" border="0"
                            style="vertical-align: bottom; margin-bottom: 2px;" /><span class="smallerlabel">(DD-MMM-YYYY)</span>
                        <input id="Hidden4" type="hidden" value="0" runat="server" />
                    </td>
                </tr>
            </table>
            <%--</div>--%>
        </td>
        <td class='border center pad5 whitebg' style='width: 50%'>
            <%--<div id="divPreviousAdmissionEnd" style="display: none;">--%>
            <table width='100%'>
                <tr>
                    <td style='width: 40%' align='right'>
                        <label align='center' id='lblAdmission end-8888602' class="control-label">
                            Admission end :</label>
                    </td>
                    <td style='width: 60%' align='left'>
                        <input id="txtPreviousAdmissionEnd" maxlength="11" size="8" name="visitDate" runat="server" />
                        <img onclick="w_displayDatePicker('<%= txtPreviousAdmissionEnd.ClientID%>');" height="22"
                            alt="Date Helper" hspace="5" src="../../images/cal_icon.gif" width="22" border="0"
                            style="vertical-align: bottom; margin-bottom: 2px;" /><span class="smallerlabel">(DD-MMM-YYYY)</span>
                        <input id="Hidden5" type="hidden" value="0" runat="server" />
                    </td>
                </tr>
            </table>
            <%--</div>--%>
        </td>
    </tr>
    <tr>
        <td class='border center pad5 whitebg' colspan="2">
            <table width='100%'>
                <tr>
                    <td style='width: 50%' align='right'>
                        <asp:Label ID="lblHIVAssociatedCond" runat="server" CssClass="required" Font-Bold="True"
                            Text="HIV associated conditions:"></asp:Label>
                    </td>
                    <td style='width: 50%' align='left'>
                        <asp:DropDownList runat="server" ID="ddlHIVAssociatedConditionsPeads" AutoPostBack="false">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
