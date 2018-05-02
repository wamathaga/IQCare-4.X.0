<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControlKNH_MedicalHistory.ascx.cs"
    Inherits="PresentationApp.ClinicalForms.UserControl.UserControlKNH_MedicalHistory" %>
<style type="text/css">
    td
    {
        word-break: break-all;
    }
    .tbl-right
    {
        width: 100%;
        border: none;
    }
    .data-control
    {
        width: 50%;
    }
    .data-lable
    {
        width: 50%;
    }
    .tbl-left
    {
        width: 100%;
        border: none;
    }
    .col-left
    {
        width: 350px;
    }
    .col-right
    {
        width: 350px;
    }
</style>
<table class="border center formbg" cellspacing="6" cellpadding="0" width="100%"
    border="0">
    <tr>
        <td colspan="2" class="border pad5 whitebg">
            <table>
                <tr>
                    <td>
                        <h4 align="left">
                            Respiratory System Medical History?
                        </h4>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rblRespiratory" runat="server" RepeatDirection="Horizontal"
                            OnClick="rblSelectedValue(this,'hide889YN')">
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <div id="hide889YN" style="display: none;">
                <table width="100%" class="formbg" cellspacing="6" cellpadding="0" border="0">
                    <tr>
                        <td width="50%" class="border pad5 whitebg">
                            <table class="tbl-right">
                                <tr>
                                    <td align="right" class="data-lable">
                                        <label class="control-label">
                                            Respiratory disease name:</label>
                                    </td>
                                    <td align="left" class="data-control">
                                        <asp:TextBox ID="txtRespiratoryDiseaseName" runat="server">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="50%" class="border pad5 whitebg">
                            <table class="tbl-left">
                                <tr>
                                    <td align="right" class="data-lable">
                                        <label class="control-label">
                                            Respiratory disease diagnosis &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;date:</label>
                                    </td>
                                    <td align="left" class="data-control">
                                        <input id="dtRespiratoryDiseaseDate" onblur="DateFormat(this,this.value,event,false,'3')"
                                            onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"
                                            maxlength="11" size="11" name="VisitDate" runat="server" />
                                        <img id="Img2" onclick="w_displayDatePicker('<%=dtRespiratoryDiseaseDate.ClientID%>');"
                                            height="22 " alt="Date Helper" hspace="5" src="../../images/cal_icon.gif" width="22"
                                            border="0" name="appDateimg" style="vertical-align: bottom; margin-bottom: 2px;" />
                                        <span class="smallerlabel" id="Span2">(DD-MMM-YYYY)</span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td width="50%" class="border pad5 whitebg">
                            <table class="tbl-right">
                                <tr>
                                    <td align="right" class="data-lable">
                                        <label class="control-label">
                                            Respiratory disease treatment:</label>
                                    </td>
                                    <td align="left" class="data-control">
                                        <asp:TextBox ID="txtRespiratoryDiseaseTreatment" runat="server">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="2" class="border pad5 whitebg">
            <table>
                <tr>
                    <td>
                        <h4 class="forms" align="left">
                            Cardiovascular System Medical History?
                        </h4>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rblCardiovascular" runat="server" RepeatDirection="Horizontal"
                            OnClick="rblSelectedValue(this,'hide890YN')">
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <div id="hide890YN" style="display: none;">
                <table width="100%" class="formbg" cellspacing="6" cellpadding="0" border="0">
                    <tr>
                        <td width="50%" class="border pad5 whitebg">
                            <table class="tbl-right">
                                <tr>
                                    <td align="right" class="data-lable">
                                        <label class="control-label">
                                            Cardiovascular disease name:</label>
                                    </td>
                                    <td align="left" class="data-control">
                                        <asp:TextBox ID="txtCardiovascularDiseaseName" runat="server">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="50%" class="border pad5 whitebg">
                            <table class="tbl-left">
                                <tr>
                                    <td align="right" class="data-lable">
                                        <label class="control-label">
                                            Cardiovascular disease diagnosis date:</label>
                                    </td>
                                    <td align="left" class="data-control">
                                        <input id="dtCardiovascularDiseaseDate" onblur="DateFormat(this,this.value,event,false,'3')"
                                            onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"
                                            maxlength="11" size="11" name="VisitDate" runat="server" />
                                        <img id="Img1" onclick="w_displayDatePicker('<%=dtCardiovascularDiseaseDate.ClientID%>');"
                                            height="22 " alt="Date Helper" hspace="5" src="../../images/cal_icon.gif" width="22"
                                            border="0" name="appDateimg" />
                                        <span class="smallerlabel" id="Span1">(DD-MMM-YYYY)</span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td width="50%" class="border pad5 whitebg">
                            <table class="tbl-right">
                                <tr>
                                    <td align="right" class="data-lable">
                                        <label class="control-label">
                                            Cardiovascular disease treatment:</label>
                                    </td>
                                    <td align="left" class="data-control">
                                        <asp:TextBox ID="txtCardiovascularDiseaseTreatment" runat="server">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="2" class="border pad5 whitebg">
            <table>
                <tr>
                    <td>
                        <h4 class="forms" align="left">
                            Gastro Intestinal System Medical History?
                        </h4>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rblGastroIntestinal" runat="server" RepeatDirection="Horizontal"
                            OnClick="rblSelectedValue(this,'hide894YN')">
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <div id="hide894YN" style="display: none;">
                <table width="100%" class="formbg" cellspacing="6" cellpadding="0" border="0">
                    <tr>
                        <td width="50%" class="border pad5 whitebg">
                            <table class="tbl-right">
                                <tr>
                                    <td align="right" class="data-lable">
                                        <label class="control-label">
                                            Gastro intestinal disease name:</label>
                                    </td>
                                    <td align="left" class="data-control">
                                        <asp:TextBox ID="txtGastroIntestinalDiseaseName" runat="server">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="50%" class="border pad5 whitebg">
                            <table class="tbl-left">
                                <tr>
                                    <td align="right" class="data-lable">
                                        <label class="control-label">
                                            Gastro intestinal disease diagnosis date:</label>
                                    </td>
                                    <td align="left" class="data-control">
                                        <input id="dtGastroIntestinalDiseaseDate" onblur="DateFormat(this,this.value,event,false,'3')"
                                            onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"
                                            maxlength="11" size="11" name="VisitDate" runat="server" />
                                        <img id="Img3" onclick="w_displayDatePicker('<%=dtGastroIntestinalDiseaseDate.ClientID%>');"
                                            height="22 " alt="Date Helper" hspace="5" src="../../images/cal_icon.gif" width="22"
                                            border="0" name="appDateimg" />
                                        <span class="smallerlabel" id="Span3">(DD-MMM-YYYY)</span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td width="50%" class="border pad5 whitebg">
                            <table class="tbl-right">
                                <tr>
                                    <td align="right" class="data-lable">
                                        <label class="control-label">
                                            Gastro intestinal disease treatment:</label>
                                    </td>
                                    <td align="left" class="data-control">
                                        <asp:TextBox ID="txtGastroIntestinalDiseaseTreatment" runat="server">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="2" class="border pad5 whitebg">
            <table>
                <tr>
                    <td>
                        <h4 class="forms" align="left">
                            Nervous System Medical History?
                        </h4>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rblNervousSystem" runat="server" RepeatDirection="Horizontal"
                            OnClick="rblSelectedValue(this,'hide897YN')">
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <div id="hide897YN" style="display: none;">
                <table width="100%" class="formbg" cellspacing="6" cellpadding="0" border="0">
                    <tr>
                        <td width="50%" class="border pad5 whitebg">
                            <table class="tbl-right">
                                <tr>
                                    <td align="right" class="data-lable">
                                        <label class="control-label">
                                            Nervous disease name:</label>
                                    </td>
                                    <td align="left" class="data-control">
                                        <asp:TextBox ID="txtNervousDiseaseName" runat="server">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="50%" class="border pad5 whitebg">
                            <table class="tbl-left">
                                <tr>
                                    <td align="right" class="data-lable">
                                        <label class="control-label">
                                            Nervous disease diagnosis date:</label>
                                    </td>
                                    <td align="left" class="data-control">
                                        <input id="dtNervousDiseaseDate" onblur="DateFormat(this,this.value,event,false,'3')"
                                            onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"
                                            maxlength="11" size="11" name="VisitDate" runat="server" />
                                        <img id="Img4" onclick="w_displayDatePicker('<%=dtNervousDiseaseDate.ClientID%>');"
                                            height="22 " alt="Date Helper" hspace="5" src="../../images/cal_icon.gif" width="22"
                                            border="0" name="appDateimg" />
                                        <span class="smallerlabel" id="Span4">(DD-MMM-YYYY)</span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td width="50%" class="border pad5 whitebg">
                            <table class="tbl-right">
                                <tr>
                                    <td align="right" class="data-lable">
                                        <label class="control-label">
                                            Nervous disease treatment:</label>
                                    </td>
                                    <td align="left" class="data-control">
                                        <asp:TextBox ID="txtNervousDiseaseTreatment" runat="server">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="2" class="border pad5 whitebg">
            <table>
                <tr>
                    <td>
                        <h4 class="forms" align="left">
                            Dermatology System Medical History?
                        </h4>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rblDermatology" runat="server" RepeatDirection="Horizontal"
                            OnClick="rblSelectedValue(this,'hide900YN')">
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <div id="hide900YN" style="display: none;">
                <table width="100%" class="formbg" cellspacing="6" cellpadding="0" border="0">
                    <tr>
                        <td width="50%" class="border pad5 whitebg">
                            <table class="tbl-right">
                                <tr>
                                    <td align="right" class="data-lable">
                                        <label class="control-label">
                                            Dermatology disease name:</label>
                                    </td>
                                    <td align="left" class="data-control">
                                        <asp:TextBox ID="txtDermatologyDiseaseName" runat="server">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="50%" class="border pad5 whitebg">
                            <table class="tbl-left">
                                <tr>
                                    <td align="right" class="data-lable">
                                        <label class="control-label">
                                            Dermatology disease diagnosis date:</label>
                                    </td>
                                    <td align="left" class="data-control">
                                        <input id="dtDermatologyDiseaseDate" onblur="DateFormat(this,this.value,event,false,'3')"
                                            onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"
                                            maxlength="11" size="11" name="VisitDate" runat="server" />
                                        <img id="Img5" onclick="w_displayDatePicker('<%=dtDermatologyDiseaseDate.ClientID%>');"
                                            height="22 " alt="Date Helper" hspace="5" src="../../images/cal_icon.gif" width="22"
                                            border="0" name="appDateimg" />
                                        <span class="smallerlabel" id="Span5">(DD-MMM-YYYY)</span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td width="50%" class="border pad5 whitebg">
                            <table class="tbl-right">
                                <tr>
                                    <td align="right" class="data-lable">
                                        <label class="control-label">
                                            Dermatology disease treatment:</label>
                                    </td>
                                    <td align="left" class="data-control">
                                        <asp:TextBox ID="txtDermatologyDiseaseTreatment" runat="server">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="2" class="border pad5 whitebg">
            <table>
                <tr>
                    <td>
                        <h4 class="forms" align="left">
                            Musculoskeletal System Medical History?
                        </h4>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rblMusculoskeletal" runat="server" RepeatDirection="Horizontal"
                            OnClick="rblSelectedValue(this,'hide901YN')">
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <div id="hide901YN" style="display: none;">
                <table width="100%" class="formbg" cellspacing="6" cellpadding="0" border="0">
                    <tr>
                        <td width="50%" class="border pad5 whitebg">
                            <table class="tbl-right">
                                <tr>
                                    <td align="right" class="data-lable">
                                        <label class="control-label">
                                            Musculoskeletal disease name:</label>
                                    </td>
                                    <td align="left" class="data-control">
                                        <asp:TextBox ID="txtMusculoskeletalDiseaseName" runat="server">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="50%" class="border pad5 whitebg">
                            <table class="tbl-left">
                                <tr>
                                    <td align="right" class="data-lable">
                                        <label class="control-label">
                                            Musculoskeletal disease diagnosis date:</label>
                                    </td>
                                    <td align="left" class="data-control">
                                        <input id="dtMusculoskeletalDiseaseDate" onblur="DateFormat(this,this.value,event,false,'3')"
                                            onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"
                                            maxlength="11" size="11" name="VisitDate" runat="server" />
                                        <img id="Img6" onclick="w_displayDatePicker('<%=dtMusculoskeletalDiseaseDate.ClientID%>');"
                                            height="22 " alt="Date Helper" hspace="5" src="../../images/cal_icon.gif" width="22"
                                            border="0" name="appDateimg" />
                                        <span class="smallerlabel" id="Span6">(DD-MMM-YYYY)</span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td width="50%" class="border pad5 whitebg">
                            <table class="tbl-right">
                                <tr>
                                    <td align="right" class="data-lable">
                                        <label class="control-label">
                                            Musculoskeletal disease treatment:</label>
                                    </td>
                                    <td align="left" class="data-control">
                                        <asp:TextBox ID="txtMusculoskeletalDiseaseTreatment" runat="server">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="2" class="border pad5 whitebg">
            <table>
                <tr>
                    <td>
                        <h4 class="forms" align="left">
                            Psychiatric System Medical History?
                        </h4>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rblPsychiatric" runat="server" RepeatDirection="Horizontal"
                            OnClick="rblSelectedValue(this,'hide902YN')">
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <div id="hide902YN" style="display: none;">
                <table width="100%" class="formbg" cellspacing="6" cellpadding="0" border="0">
                    <tr>
                        <td width="50%" class="border pad5 whitebg">
                            <table class="tbl-right">
                                <tr>
                                    <td align="right" class="data-lable">
                                        <label class="control-label">
                                            Psychiatric disease name:</label>
                                    </td>
                                    <td align="left" class="data-control">
                                        <asp:TextBox ID="txtPsychiatricDiseaseName" runat="server">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="50%" class="border pad5 whitebg">
                            <table class="tbl-left">
                                <tr>
                                    <td align="right" class="data-lable">
                                        <label class="control-label">
                                            Psychiatric disease diagnosis date:</label>
                                    </td>
                                    <td align="left" class="data-control">
                                        <input id="dtPsychiatricDiseaseDate" onblur="DateFormat(this,this.value,event,false,'3')"
                                            onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"
                                            maxlength="11" size="11" name="VisitDate" runat="server" />
                                        <img id="Img7" onclick="w_displayDatePicker('<%=dtPsychiatricDiseaseDate.ClientID%>');"
                                            height="22 " alt="Date Helper" hspace="5" src="../../images/cal_icon.gif" width="22"
                                            border="0" name="appDateimg" />
                                        <span class="smallerlabel" id="Span7">(DD-MMM-YYYY)</span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td width="50%" class="border pad5 whitebg">
                            <table class="tbl-right">
                                <tr>
                                    <td align="right" class="data-lable">
                                        <label class="control-label">
                                            Psychiatric disease treatment:</label>
                                    </td>
                                    <td align="left" class="data-control">
                                        <asp:TextBox ID="txtPsychiatricDiseaseTreatment" runat="server">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="2" class="border pad5 whitebg">
            <table>
                <tr>
                    <td>
                        <h4 class="forms" align="left">
                            Hematological System Medical History?
                        </h4>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rblHematological" runat="server" RepeatDirection="Horizontal"
                            OnClick="rblSelectedValue(this,'hide903YN')">
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <div id="hide903YN" style="display: none;">
                <table width="100%" class="formbg" cellspacing="6" cellpadding="0" border="0">
                    <tr>
                        <td width="50%" class="border pad5 whitebg">
                            <table class="tbl-right">
                                <tr>
                                    <td align="right" class="data-lable">
                                        <label class="control-label">
                                            Hematological disease name:</label>
                                    </td>
                                    <td align="left" class="data-control">
                                        <asp:TextBox ID="txtHematologicalDiseaseName" runat="server">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="50%" class="border pad5 whitebg">
                            <table class="tbl-left">
                                <tr>
                                    <td align="right" class="data-lable">
                                        <label class="control-label">
                                            Hematological disease diagnosis date:</label>
                                    </td>
                                    <td align="left" class="data-control">
                                        <input id="dtHematologicalDiseaseDate" onblur="DateFormat(this,this.value,event,false,'3')"
                                            onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"
                                            maxlength="11" size="11" name="VisitDate" runat="server" />
                                        <img id="Img8" onclick="w_displayDatePicker('<%=dtHematologicalDiseaseDate.ClientID%>');"
                                            height="22 " alt="Date Helper" hspace="5" src="../../images/cal_icon.gif" width="22"
                                            border="0" name="appDateimg" />
                                        <span class="smallerlabel" id="Span8">(DD-MMM-YYYY)</span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td width="50%" class="border pad5 whitebg">
                            <table class="tbl-right">
                                <tr>
                                    <td align="right" class="data-lable">
                                        <label class="control-label">
                                            Hematological disease treatment:</label>
                                    </td>
                                    <td align="left" class="data-control">
                                        <asp:TextBox ID="txtHematologicalDiseaseTreatment" runat="server">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="2" class="border pad5 whitebg">
            <table>
                <tr>
                    <td>
                        <h4 class="forms" align="left">
                            Genital Urinary System Medical History?
                        </h4>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rblGenitalUrinary" runat="server" RepeatDirection="Horizontal"
                            OnClick="rblSelectedValue(this,'hide904YN')">
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <div id="hide904YN" style="display: none;">
                <table width="100%" class="formbg" cellspacing="6" cellpadding="0" border="0">
                    <tr>
                        <td width="50%" class="border pad5 whitebg">
                            <table class="tbl-right">
                                <tr>
                                    <td align="right" class="data-lable">
                                        <label class="control-label">
                                            Genital Urinary disease name:</label>
                                    </td>
                                    <td align="left" class="data-control">
                                        <asp:TextBox ID="txtGenitalUrinaryDiseaseName" runat="server">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="50%" class="border pad5 whitebg">
                            <table class="tbl-left">
                                <tr>
                                    <td align="right" class="data-lable">
                                        <label class="control-label">
                                            Genital Urinary disease diagnosis date:</label>
                                    </td>
                                    <td align="left" class="data-control">
                                        <input id="dtGenitalUrinaryDiseaseDate" onblur="DateFormat(this,this.value,event,false,'3')"
                                            onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"
                                            maxlength="11" size="11" name="VisitDate" runat="server" />
                                        <img id="Img9" onclick="w_displayDatePicker('<%=dtGenitalUrinaryDiseaseDate.ClientID%>');"
                                            height="22 " alt="Date Helper" hspace="5" src="../../images/cal_icon.gif" width="22"
                                            border="0" name="appDateimg" />
                                        <span class="smallerlabel" id="Span9">(DD-MMM-YYYY)</span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td width="50%" class="border pad5 whitebg">
                            <table class="tbl-right">
                                <tr>
                                    <td align="right" class="data-lable">
                                        <label class="control-label">
                                            Genital Urinary disease treatment:</label>
                                    </td>
                                    <td align="left" class="data-control">
                                        <asp:TextBox ID="txtGenitalUrinaryDiseaseTreatment" runat="server">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="2" class="border pad5 whitebg">
            <table>
                <tr>
                    <td>
                        <h4 class="forms" align="left">
                            Ophthamology System Medical History?
                        </h4>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rblOphthamology" runat="server" RepeatDirection="Horizontal"
                            OnClick="rblSelectedValue(this,'hide905YN')">
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <div id="hide905YN" style="display: none;">
                <table width="100%" class="formbg" cellspacing="6" cellpadding="0" border="0">
                    <tr>
                        <td width="50%" class="border pad5 whitebg">
                            <table class="tbl-right">
                                <tr>
                                    <td align="right" class="data-lable">
                                        <label class="control-label">
                                            Ophthamology disease name:</label>
                                    </td>
                                    <td align="left" class="data-control">
                                        <asp:TextBox ID="txtOphthamologyDiseaseName" runat="server">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="50%" class="border pad5 whitebg">
                            <table class="tbl-left">
                                <tr>
                                    <td align="right" class="data-lable">
                                        <label class="control-label">
                                            Ophthamology disease diagnosis date:</label>
                                    </td>
                                    <td align="left" class="data-control">
                                        <input id="dtOphthamologyDiseaseDate" onblur="DateFormat(this,this.value,event,false,'3')"
                                            onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"
                                            maxlength="11" size="11" name="VisitDate" runat="server" />
                                        <img id="Img10" onclick="w_displayDatePicker('<%=dtOphthamologyDiseaseDate.ClientID%>');"
                                            height="22 " alt="Date Helper" hspace="5" src="../../images/cal_icon.gif" width="22"
                                            border="0" name="appDateimg" />
                                        <span class="smallerlabel" id="Span10">(DD-MMM-YYYY)</span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td width="50%" class="border pad5 whitebg">
                            <table class="tbl-right">
                                <tr>
                                    <td align="right" class="data-lable">
                                        <label class="control-label">
                                            Ophthamology disease treatment:</label>
                                    </td>
                                    <td align="left" class="data-control">
                                        <asp:TextBox ID="txtOphthamologyDiseaseTreatment" runat="server">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="2" class="border pad5 whitebg">
            <table>
                <tr>
                    <td>
                        <h4 class="forms" align="left">
                            ENT System Medical History?
                        </h4>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rblENT" runat="server" RepeatDirection="Horizontal" OnClick="rblSelectedValue(this,'div90666')">
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <div id="div90666" style="display: none;">
                <table width="100%" class="formbg" cellspacing="6" cellpadding="0" border="0">
                    <tr>
                        <td width="50%" class="border pad5 whitebg">
                            <table class="tbl-right">
                                <tr>
                                    <td align="right" class="data-lable">
                                        <label class="control-label">
                                            ENT disease name:</label>
                                    </td>
                                    <td align="left" class="data-control">
                                        <asp:TextBox ID="txtENTDiseaseName" runat="server">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="50%" class="border pad5 whitebg">
                            <table class="tbl-left">
                                <tr>
                                    <td align="right" class="data-lable">
                                        <label class="control-label">
                                            ENT disease diagnosis date:</label>
                                    </td>
                                    <td align="left" class="data-control">
                                        <input id="dtENTDiseaseDate" onblur="DateFormat(this,this.value,event,false,'3')"
                                            onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"
                                            maxlength="11" size="11" name="VisitDate" runat="server" />
                                        <img id="Img11" onclick="w_displayDatePicker('<%=dtENTDiseaseDate.ClientID%>');"
                                            height="22 " alt="Date Helper" hspace="5" src="../../images/cal_icon.gif" width="22"
                                            border="0" name="appDateimg" />
                                        <span class="smallerlabel" id="Span11">(DD-MMM-YYYY)</span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td width="50%" class="border pad5 whitebg">
                            <table class="tbl-right">
                                <tr>
                                    <td align="right" class="data-lable">
                                        <label class="control-label">
                                            ENT disease treatment:</label>
                                    </td>
                                    <td align="left" class="data-control">
                                        <asp:TextBox ID="txtENTDiseaseTreatment" runat="server">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
