<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControlNigeria_WHOStaging.ascx.cs"
    Inherits="PresentationApp.ClinicalForms.UserControl.UserControlNigeria_WHOStaging" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="UCact" Namespace="AjaxControlToolkit" %>

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


<table class="border center formbg" cellspacing="6" cellpadding="0" width="100%"
    border="0">    
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
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <%--<div id="hideWHOStageIYN" style="display: none;">--%>
            <asp:Panel ID="PnlWHOStageIDetails" runat="server" Style="overflow:hidden;">
            <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
        <asp:Panel ID="PnlWHO1" runat="server" ScrollBars="Vertical" Height="150px">
                                    <asp:GridView ID="gvWHO1" runat="server" AutoGenerateColumns="false" Width="99%"
                                        ShowFooter="false" GridLines="None">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Presenting WHO Stage I Conditions" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="control-label">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblwho1" runat="server" Text='<%# Eval("ID") %>' Visible="false"></asp:Label>
                                                    <asp:CheckBox ID="Chkwho1" runat="server" Text='<%# Eval("NAME") %>' onclick="SelectWHOStage()" CssClass="checkbox-btn"/>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
             </div>
             
 </div>

                
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
            
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Panel ID="PnlWHOStageIIDetails" runat="server" Style="overflow:hidden;">
            <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
       <asp:Panel ID="PnlWHO2" runat="server" ScrollBars="Vertical" Height="150px">
                                    <asp:GridView ID="gvWHO2" runat="server" AutoGenerateColumns="false" Width="99%"
                                        ShowFooter="false" GridLines="None">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Presenting WHO Stage II Conditions" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="control-label">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblwho2" runat="server" Text='<%# Eval("ID") %>' Visible="false"></asp:Label>
                                                    <asp:CheckBox ID="Chkwho2" runat="server" Text='<%# Eval("NAME") %>' onclick="SelectWHOStage()" CssClass="checkbox-btn"/>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                            
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>    
            
 </div>
 </div>
                <table width="100%" border="0" cellspacing="6" cellpadding="0">
                    <tbody>
                        <tr>
                            <td class="border center pad5 whitebg" style="width: 50%;" colspan="2">
                                
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
            
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Panel ID="PnlWHOStageIIIDetails" runat="server" Style="overflow:hidden;">
            <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
           
            <asp:Panel ID="PnlWHO3" runat="server" ScrollBars="Vertical" Height="200px">
                                    <asp:GridView ID="gvWHO3" runat="server" AutoGenerateColumns="false" Width="99%"
                                        ShowFooter="false" BorderStyle="None" GridLines="None">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Presenting WHO Stage III Conditions" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="control-label">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWHO3" runat="server" Text='<%# Eval("ID") %>' Visible="false"></asp:Label>
                                                    <asp:CheckBox ID="ChkWHO3" runat="server" Text='<%# Eval("NAME") %>' onclick="SelectWHOStage()" CssClass="checkbox-btn"/>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                            
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
 </div>
 </div>
                
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
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Panel ID="PnlWHOStageIVDetails" runat="server" Style="overflow:hidden;">
            <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
<asp:Panel ID="PnlWHO4" runat="server" ScrollBars="Vertical" Height="200px">
                                    <asp:GridView ID="gvWHO4" runat="server" AutoGenerateColumns="false" Width="99%"
                                        ShowFooter="false" GridLines="None">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Presenting WHO Stage IV Conditions" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="control-label">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWHO4" runat="server" Text='<%# Eval("ID") %>' Visible="false"></asp:Label>
                                                    <asp:CheckBox ID="ChkWHO4" runat="server" Text='<%# Eval("NAME") %>' onclick="SelectWHOStage()" CssClass="checkbox-btn"/>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                          
                                            
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
            
 </div>
 </div>
                <table width="100%" border="0" cellspacing="6" cellpadding="0">
                    <tbody>
                        <tr>
                            <td class="border center pad5 whitebg" style="width: 50%;" colspan="2">
                                
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
             <label class="control-label">WHO Stage at initiation (Transfer in):</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlInitiationWHOstage" runat="server" Width="160" CssClass="form-control"></asp:DropDownList>
             </div> 
              <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label required">HIV associated conditions:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlhivassociated" runat="server" Width="160" CssClass="form-control"></asp:DropDownList>
             </div>
            
 </div>
 <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label required"><asp:Label ID="lblWHOStage" runat="server" Text="Current WHO Stage:"></asp:Label></label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
           <asp:DropDownList ID="ddlwhostage1" runat="server" Width="160" CssClass="form-control"></asp:DropDownList>
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
                ImageControlID="ImgWHOStageI">
            </UCact:CollapsiblePanelExtender>
            <UCact:CollapsiblePanelExtender ID="CPPnlWHOStageII" runat="server" SuppressPostBack="true"
                ExpandedImage="~/images/arrow-dn.gif" TargetControlID="PnlWHOStageIIDetails"
                CollapseControlID="PnlWHOStageII" ExpandControlID="PnlWHOStageII" CollapsedImage="~/Images/arrow-up.gif"
                Collapsed="true" ImageControlID="ImgWHOStageII">
            </UCact:CollapsiblePanelExtender>
            <UCact:CollapsiblePanelExtender ID="CPPnlWHOStageIII" runat="server" SuppressPostBack="true"
                ExpandedImage="~/images/arrow-dn.gif" TargetControlID="PnlWHOStageIIIDetails"
                CollapseControlID="PnlWHOStageIII" ExpandControlID="PnlWHOStageIII" CollapsedImage="~/Images/arrow-up.gif"
                Collapsed="true" ImageControlID="ImgWHOStageIDetails">
            </UCact:CollapsiblePanelExtender>
            <UCact:CollapsiblePanelExtender ID="CPPnlWHOStageIV" runat="server" SuppressPostBack="true"
                ExpandedImage="~/images/arrow-dn.gif" TargetControlID="PnlWHOStageIVDetails"
                CollapseControlID="PnlWHOStageIV" ExpandControlID="PnlWHOStageIV" CollapsedImage="~/Images/arrow-up.gif"
                Collapsed="true" ImageControlID="ImgWHOStageIV">
            </UCact:CollapsiblePanelExtender>            
            <UCact:CollapsiblePanelExtender ID="CPpnlstaging" runat="server" SuppressPostBack="true"
                ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlstaging" CollapseControlID="pnlstagingYN"
                ExpandControlID="pnlstagingYN" CollapsedImage="~/Images/arrow-up.gif" Collapsed="true"
                ImageControlID="Imgstaging">
            </UCact:CollapsiblePanelExtender>
        </td>
    </tr>
</table>
