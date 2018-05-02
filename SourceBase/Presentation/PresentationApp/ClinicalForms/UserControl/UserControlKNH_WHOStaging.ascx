<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControlKNH_WHOStaging.ascx.cs"
    Inherits="PresentationApp.ClinicalForms.UserControl.UserControlKNH_WHOStaging" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="UCact" Namespace="AjaxControlToolkit" %>
<script type="text/javascript">
    function RegisterJQuery() {
        $('#txtmenarchedate').datepicker({ autoclose: true }); 
        $(".datePicker").datepicker({ autoclose: true });
    }
    //Calling RegisterJQuery when document is ready (Page loaded first time)
    $(document).ready(RegisterJQuery);
    //Calling RegisterJQuery when the page is doing postback (asp.net)
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RegisterJQuery);
</script>
<script type="text/javascript">

    function rblSelectedValue2(val, divID, txtControlId) {
        debugger;
        var selectedvalue = val;
        var YN = "";
        if (selectedvalue == "1") {
            YN = "show";
        }
        else {
            //document.getElementById(txtControlId).value = '';
            YN = "hide";
        }
        rblSelShowHide(divID, YN);

    }
    function rblSelShowHide(theDiv, YN) {
        if (YN == "show") {
            if ($("#" + theDiv).is(':visible'))
            { $("#" + theDiv).hide(); }
            else
            { $("#" + theDiv).show(); }

        }
        if (YN == "hide") {
            $("#" + theDiv).hide();

        }
    }



    function ShowHideDiv(theDiv) {
        if ($("#" + theDiv).is(':visible'))
        { $("#" + theDiv).hide(); }
        else
        { $("#" + theDiv).show(); }
    }

    function SelectWHOStage() {
        //stage I
        var stageIChecked = false;
        var WHOGridI = document.getElementById("<%= gvWHO1.ClientID %>");
        var cell;

        if (WHOGridI.rows.length > 0) {
            for (i = 0; i < WHOGridI.rows.length; i++) {
                cell = WHOGridI.rows[i].cells[0];
                for (j = 0; j < cell.childNodes.length; j++) {
                    if (cell.childNodes[j].type == "checkbox") {
                        if (cell.childNodes[j].checked == true) {
                            stageIChecked = true;
                            //alert(stageIChecked);
                        }
                    }
                }
            }
        }

        //stageII
        var stageIIChecked = false;
        var WHOGridII = document.getElementById("<%= gvWHO2.ClientID %>");
        var cell2;

        if (WHOGridII.rows.length > 0) {
            for (i = 0; i < WHOGridII.rows.length; i++) {
                cell2 = WHOGridII.rows[i].cells[0];
                for (j = 0; j < cell2.childNodes.length; j++) {
                    if (cell2.childNodes[j].type == "checkbox") {
                        if (cell2.childNodes[j].checked == true) {
                            stageIIChecked = true;
                            //alert(stageIIChecked);
                        }
                    }
                }
            }
        }

        //stageIII
        var stageIIIChecked = false;
        var WHOGridIII = document.getElementById("<%= gvWHO3.ClientID %>");
        var cell3;

        if (WHOGridIII.rows.length > 0) {
            for (i = 0; i < WHOGridIII.rows.length; i++) {
                cell3 = WHOGridIII.rows[i].cells[0];
                for (j = 0; j < cell3.childNodes.length; j++) {
                    if (cell3.childNodes[j].type == "checkbox") {
                        if (cell3.childNodes[j].checked == true) {
                            stageIIIChecked = true;
                            //alert(stageIIIChecked);
                        }
                    }
                }
            }
        }


        //stageIV
        var stageIVChecked = false;
        var WHOGridIV = document.getElementById("<%= gvWHO4.ClientID %>");
        var cell4;

        if (WHOGridIV.rows.length > 0) {
            for (i = 0; i < WHOGridIV.rows.length; i++) {
                cell4 = WHOGridIV.rows[i].cells[0];
                for (j = 0; j < cell4.childNodes.length; j++) {
                    if (cell4.childNodes[j].type == "checkbox") {
                        if (cell4.childNodes[j].checked == true) {
                            stageIVChecked = true;
                            //alert(stageIVChecked);
                        }
                    }
                }
            }
        }

        //
        var sel = document.getElementById("<%= ddlwhostage1.ClientID %>");
        for (var i = 0; i < 4; i++) {
            sel.options[i].disabled = false;
        }

        if (stageIVChecked == true) {
            sel.selectedIndex = 4;
            for (var i = 0; i < 4; i++) {
                sel.options[i].disabled = "disabled";
            }
        }
        else if (stageIIIChecked == true) {
            sel.selectedIndex = 3;
            for (var i = 0; i < 3; i++) {
                sel.options[i].disabled = "disabled";
            }
        }
        else if (stageIIChecked == true) {
            for (var i = 0; i < 2; i++) {
                sel.options[i].disabled = "disabled";
            }
            sel.selectedIndex = 2;
        }
        else if (stageIChecked == true) {
            for (var i = 0; i < 1; i++) {
                sel.options[i].disabled = "disabled";
            }
            sel.selectedIndex = 1;
        }
        else {
            sel.selectedIndex = 0;
        }

    }

</script>
 <style type="text/css">
.gdRowSpacing
{  
   height:45px;
}
</style>
<table class="table-condensed" width="100%">
    <tr>
        <td colspan="2">
            <div id="divprogressioninwhoshowhide" style="width: 101%; margin-left: -0.5%;">
                <table width="100%">
                    <tr id="TRProgressionWHO" runat="server">
                        <td colspan="2" class="border pad5 whitebg">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:Panel ID="PnlProgressioninWHO" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="ImgProgressioninWHO" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                    </td>
                                                    <td>
                                                        <h5 align="left" class="forms">
                                                            Progression in WHO - WAB Evaluation</h5>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                            <%--<table>
                            <tr>
                                <td>
                                    <h4 class="forms" align="left">
                                        Progression in WHO - WAB Evalaution
                                    </h4>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="RadioButtonList4" runat="server" RepeatDirection="Horizontal"
                                        OnClick="rblSelectedValue(this,'hideProgressioninWHOYN')">
                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="0" Selected="True"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>--%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="PnlProgressioninWHODetails" runat="server" Style="overflow:hidden;">
                            <div class="row">
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label for="inputEmail3" class="control-label">
             <label id="lblHas WHO Stage progressed-8888902" align="center"> Has WHO Stage progressed :</label>
             </label>
             </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            <asp:RadioButtonList ID="rdoProgressionInWHOstage" runat="server" RepeatDirection="Horizontal"
                                                                    OnClick="rblSelectedValue(this,'divspecifyprog')">
                                                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                                    <asp:ListItem Text="N/A" Value="2"></asp:ListItem>
                                                                </asp:RadioButtonList>
            </div>
            <div id="divspecifyprog" style="display: none;">
            <div class="col-md-3 col-sm-12 col-xs-12 form-group"><label for="inputEmail3" class="control-label">
            <label id="lblSpecify WHO progression-8888903" align="center">Specify WHO progression :</label></label>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <asp:TextBox runat="server" ID="txtSpecifyWHOprogression" CssClass="form-control" Width="180px"></asp:TextBox>
            </div>
            </div>
             </div>
                               
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="2" class="border pad5 whitebg">
            <table width="100%">
                <tr>
                    <td>
                        <asp:Panel ID="PnlWHOStageI" runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="ImgWHOStageI" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                    </td>
                                    <td>
                                        <h5 align="left" class="forms">
                                            WHO Stage I</h5>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <%--<table>
                <tr>
                    <td>
                        <h4 align="left">
                            WHO Stage I
                        </h4>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rblWHOStageI" runat="server" RepeatDirection="Horizontal"
                            OnClick="rblSelectedValue(this,'hideWHOStageIYN')">
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0" Selected="True"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>--%>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <%--<div id="hideWHOStageIYN" style="display: none;">--%>
            <asp:Panel ID="PnlWHOStageIDetails" runat="server" >
                <table width="100%" border="0" cellspacing="6" cellpadding="0">
                    <tbody>
                        <tr>
                            <td class="border center pad5 whitebg" style="width: 70%;" colspan="2">
                                <asp:Panel ID="PnlWHO1" runat="server" ScrollBars="Vertical" Height="200px" Style="overflow:hidden;">
                                 <div class="row" align="center">
                                    <asp:GridView ID="gvWHO1" runat="server" AutoGenerateColumns="false" Width="90%"
                                        ShowFooter="false" GridLines="None" CellPadding="10">
                                         
                                        <Columns>                                        
                                            <asp:TemplateField HeaderText="Presenting WHO Stage I Conditions" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="400" HeaderStyle-CssClass="control-label-grdHeader">
                                                
                                                <ItemTemplate>
                                                    <asp:Label ID="lblwho1" runat="server" Text='<%# Eval("ID") %>' Visible="false"></asp:Label>
                                                    <asp:CheckBox ID="Chkwho1" runat="server" Text='<%# Eval("NAME") %>' onclick="SelectWHOStage()" />
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Current" HeaderStyle-Width="300" HeaderStyle-CssClass="control-label-grdHeader" HeaderStyle-HorizontalAlign="Left"  ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left datePicker" id="txtCurrentWho1Date" clientidmode="Static"
                                                       maxlength="11" size="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>

                                                
                                                    
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Historic" HeaderStyle-Width="300"  HeaderStyle-CssClass="control-label-grdHeader" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left datePicker" id="txtCurrentWho1Date1" clientidmode="Static"
                                                       maxlength="11" size="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>  
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                     <RowStyle CssClass="gdRowSpacing"/>
                                        
                                    </asp:GridView>
                                    </div>
                                </asp:Panel>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td colspan="2" class="border pad5 whitebg">
            <table width="100%">
                <tr>
                    <td>
                        <asp:Panel ID="PnlWHOStageII" runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="ImgPnlWHOStageII" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                    </td>
                                    <td>
                                        <h5 align="left" class="forms">
                                            WHO Stage II</h5>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <%--<table>
                <tr>
                    <td>
                        <h4 class="forms" align="left">
                            WHO Stage II
                        </h4>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rblWHOStageII" runat="server" RepeatDirection="Horizontal"
                            OnClick="rblSelectedValue(this,'hideWHOStageIIYN')">
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0" Selected="True"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>--%>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Panel ID="PnlWHOStageIIDetails" runat="server">
                <table width="100%" border="0" cellspacing="6" cellpadding="0">
                    <tbody>
                        <tr>
                            <td class="border center pad5 whitebg" style="width: 50%;" colspan="2">
                                <asp:Panel ID="PnlWHO2" runat="server" ScrollBars="Vertical" Height="200px">
                                    <asp:GridView ID="gvWHO2" runat="server" AutoGenerateColumns="false" Width="90%"
                                        ShowFooter="false" GridLines="None">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Presenting WHO Stage II Conditions" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="control-label-grdHeader">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblwho2" runat="server" Text='<%# Eval("ID") %>' Visible="false"></asp:Label>
                                                    <asp:CheckBox ID="Chkwho2" runat="server" Text='<%# Eval("NAME") %>' onclick="SelectWHOStage()" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Current" HeaderStyle-CssClass="control-label-grdHeader" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left datePicker" id="txtCurrentWho2Date" clientidmode="Static"
                                                       maxlength="11" size="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
                                                                                            
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Historic" HeaderStyle-CssClass="control-label-grdHeader" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left datePicker" id="txtCurrentWho2Date1" clientidmode="Static"
                                                       maxlength="11" size="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
                                                
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="gdRowSpacing"/>
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td colspan="2" class="border pad5 whitebg">
            <table width="100%">
                <tr>
                    <td>
                        <asp:Panel ID="PnlWHOStageIII" runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="ImgWHOStageIII" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                    </td>
                                    <td>
                                        <h5 align="left" class="forms">
                                            WHO Stage III</h5>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <%--<table>
                <tr>
                    <td>
                        <h4 class="forms" align="left">
                            WHO Stage III
                        </h4>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rblWHOStageIII" runat="server" RepeatDirection="Horizontal"
                            OnClick="rblSelectedValue(this,'hideWHOStageIIIYN')">
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0" Selected="True"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>--%>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Panel ID="PnlWHOStageIIIDetails" runat="server">
                <table width="100%" border="0" cellspacing="6" cellpadding="0">
                    <tbody>
                        <tr>
                            <td class="border center pad5 whitebg" colspan="2">
                                <asp:Panel ID="PnlWHO3" runat="server" ScrollBars="Vertical" Height="200px">
                                    <asp:GridView ID="gvWHO3" runat="server" AutoGenerateColumns="false" Width="90%"
                                        ShowFooter="false" BorderStyle="None" GridLines="None">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Presenting WHO Stage III Conditions" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="control-label-grdHeader">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWHO3" runat="server" Text='<%# Eval("ID") %>' Visible="false"></asp:Label>
                                                    <asp:CheckBox ID="ChkWHO3" runat="server" Text='<%# Eval("NAME") %>' onclick="SelectWHOStage()" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Current" HeaderStyle-CssClass="control-label-grdHeader" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left datePicker" id="txtCurrentWHO3Date" clientidmode="Static"
                                                       maxlength="11" size="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div> 
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Historic" HeaderStyle-CssClass="control-label-grdHeader" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left datePicker" id="txtCurrentWho3Date1" clientidmode="Static"
                                                       maxlength="11" size="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div> 

                                                                                                   
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="gdRowSpacing"/>
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td colspan="2" class="border pad5 whitebg">
            <table width="100%">
                <tr>
                    <td>
                        <asp:Panel ID="PnlWHOStageIV" runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="ImgWHOStageIV" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                    </td>
                                    <td>
                                        <h5 align="left" class="forms">
                                            WHO Stage IV</h5>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <%--<table>
                <tr>
                    <td>
                        <h4 class="forms" align="left">
                            WHO Stage IV
                        </h4>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rblWHOStageIV" runat="server" RepeatDirection="Horizontal"
                            OnClick="rblSelectedValue(this,'hideWHOStageIVYN')">
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0" Selected="True"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>--%>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Panel ID="PnlWHOStageIVDetails" runat="server">
                <table width="100%" border="0" cellspacing="6" cellpadding="0">
                    <tbody>
                        <tr>
                            <td class="border center pad5 whitebg" style="width: 50%;" colspan="2">
                                <asp:Panel ID="PnlWHO4" runat="server" ScrollBars="Vertical" Height="200px">
                                    <asp:GridView ID="gvWHO4" runat="server" AutoGenerateColumns="false" Width="90%"
                                        ShowFooter="false" GridLines="None">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Presenting WHO Stage IV Conditions" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="control-label-grdHeader">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWHO4" runat="server" Text='<%# Eval("ID") %>' Visible="false"></asp:Label>
                                                    <asp:CheckBox ID="ChkWHO4" runat="server" Text='<%# Eval("NAME") %>' onclick="SelectWHOStage()" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Current" HeaderStyle-CssClass="control-label-grdHeader" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left datePicker" id="txtCurrentWHO4Date" clientidmode="Static"
                                                       maxlength="11" size="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>                                                
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Historic" HeaderStyle-CssClass="control-label-grdHeader" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left datePicker" id="txtCurrentWho4Date1" clientidmode="Static"
                                                       maxlength="11" size="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>       
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="gdRowSpacing"/>
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td colspan="2" class="border pad5 whitebg">
            <table width="100%">
                <tr>
                    <td>
                        <asp:Panel ID="pnlstagingYN" runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="Imgstaging" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                    </td>
                                    <td>
                                        <h5 align="left" class="forms">
                                            <asp:Literal ID="literalStaging" runat="server" Text="Staging"></asp:Literal>
                                        </h5>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Panel ID="pnlstaging" runat="server" Style="overflow:hidden;">
            <div class="row" id="trnonadultfollowup" style="display: none;">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label for="inputEmail3" class="control-label">WHO Stage at initiation (Transfer in):</label>
             </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <asp:DropDownList ID="ddlInitiationWHOstage" runat="server" Width="130px" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group"><label for="inputEmail3" class="control-label">HIV associated conditions:</label>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <asp:DropDownList ID="ddlhivassociated" runat="server" Width="130px" CssClass="form-control"></asp:DropDownList>
            </div>
             </div>
             <div class="row">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label for="inputEmail3" class="control-label">
              <asp:Label ID="lblWHOStage" runat="server" CssClass="control-label required" Text="Current WHO Stage:"></asp:Label></label>
             </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <asp:DropDownList ID="ddlwhostage1" runat="server" Width="130px" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group"><label for="inputEmail3" class="control-label">
            <asp:Label ID="lblWABStage" runat="server" CssClass="control-label required" Font-Bold="True" Text="WAB Stage:"></asp:Label></label>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList Width="100%" runat="server" ID="ddlWABStage" CssClass="form-control"></asp:DropDownList>
             <asp:HiddenField ID="hiddateshow" runat="server" />
            </div>
             </div>
             <div class="row" style="display: none">
             <div id="divmernarcheshow">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label for="inputEmail3" class="control-label">Mernarche:</label>
             </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <input id="radbtnMernarcheyes" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);rblSelectedValue2(1,'divmenarchedatehshowhide','ctl00_IQCareContentPlaceHolder_tabControl_TabExamination_txtmenarchedate');"
                                                type="radio" name="labevaluationyes" runat="server" />
                                            <label>
                                                Yes</label>
                                            <input id="radbtnMernarcheno" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);rblSelectedValue2(0,'divmenarchedatehshowhide','ctl00_IQCareContentPlaceHolder_tabControl_TabExamination_txtmenarchedate');"
                                                type="radio" name="labevaluationyes" runat="server" />
                                            <label>
                                                No</label>
            </div>
            <div id="divmenarchedatehshowhide" style="display: none;">
            <div class="col-md-3 col-sm-12 col-xs-12 form-group"><label for="inputEmail3" class="control-label">Menarche Date:</label>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtmenarchedate" clientidmode="Static"
                                                       maxlength="11" size="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;" 
                                                       onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"/>
                                                </div>
                                            </div>
            
                                                      
            </div> 
            </div>
             </div>
           
             </div>
             <div class="row">
             <div id="divtannerstaging">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label for="inputEmail3" class="control-label">Tanner Staging:</label>
             </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <asp:DropDownList ID="ddltannerstaging" runat="server" AutoPostBack="false" Width="130px" CssClass="form-control">
                                            </asp:DropDownList>
            </div>
            </div>
            <div id="trPatFUstatus" style="display: none;">
            <div class="col-md-3 col-sm-12 col-xs-12 form-group"><label for="inputEmail3" class="control-label">Patient Follow Up Status:</label>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <asp:DropDownList ID="ddlPatFUstatus" runat="server" AutoPostBack="false" Width="130px" CssClass="form-control"></asp:DropDownList>
            </div>
            </div>
             </div>
                
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <UCact:CollapsiblePanelExtender ID="CPPnlWHOStageI" runat="server" SuppressPostBack="true"
                ExpandedImage="~/images/arrow-dn.gif" TargetControlID="PnlWHOStageIDetails" CollapseControlID="PnlWHOStageI"
                ExpandControlID="PnlWHOStageI" CollapsedImage="~/Images/arrow-up.gif" Collapsed="true"
                ImageControlID="ImgWHOStageI"></UCact:CollapsiblePanelExtender>
            <UCact:CollapsiblePanelExtender ID="CPPnlWHOStageII" runat="server" SuppressPostBack="true"
                ExpandedImage="~/images/arrow-dn.gif" TargetControlID="PnlWHOStageIIDetails"
                CollapseControlID="PnlWHOStageII" ExpandControlID="PnlWHOStageII" CollapsedImage="~/Images/arrow-up.gif"
                Collapsed="true" ImageControlID="ImgWHOStageII"></UCact:CollapsiblePanelExtender>
            <UCact:CollapsiblePanelExtender ID="CPPnlWHOStageIII" runat="server" SuppressPostBack="true"
                ExpandedImage="~/images/arrow-dn.gif" TargetControlID="PnlWHOStageIIIDetails"
                CollapseControlID="PnlWHOStageIII" ExpandControlID="PnlWHOStageIII" CollapsedImage="~/Images/arrow-up.gif"
                Collapsed="true" ImageControlID="ImgWHOStageIDetails"></UCact:CollapsiblePanelExtender>
            <UCact:CollapsiblePanelExtender ID="CPPnlWHOStageIV" runat="server" SuppressPostBack="true"
                ExpandedImage="~/images/arrow-dn.gif" TargetControlID="PnlWHOStageIVDetails"
                CollapseControlID="PnlWHOStageIV" ExpandControlID="PnlWHOStageIV" CollapsedImage="~/Images/arrow-up.gif"
                Collapsed="true" ImageControlID="ImgWHOStageIV"></UCact:CollapsiblePanelExtender>
            <UCact:CollapsiblePanelExtender ID="CPPnlProgressioninWHO" runat="server" SuppressPostBack="true"
                ExpandedImage="~/images/arrow-dn.gif" TargetControlID="PnlProgressioninWHODetails"
                CollapseControlID="PnlProgressioninWHO" ExpandControlID="PnlProgressioninWHO"
                CollapsedImage="~/Images/arrow-up.gif" Collapsed="true" ImageControlID="ImgProgressioninWHO">
            </UCact:CollapsiblePanelExtender>
            <UCact:CollapsiblePanelExtender ID="CPpnlstaging" runat="server" SuppressPostBack="true"
                ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlstaging" CollapseControlID="pnlstagingYN"
                ExpandControlID="pnlstagingYN" CollapsedImage="~/Images/arrow-up.gif" Collapsed="true"
                ImageControlID="Imgstaging"></UCact:CollapsiblePanelExtender>
        </td>
    </tr>
</table>
