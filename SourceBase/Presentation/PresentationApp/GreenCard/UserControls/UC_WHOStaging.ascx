<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC_WHOStaging.ascx.cs"
    Inherits="PresentationApp.GreenCard.UserControls.UC_WHOStaging" %>

<script type="text/javascript">
    $(document).ready(function () {
        var arrL = $('#<%=gvWHO1.ClientID %>').find('input[id$="txtCurrentWho1Date"]');
        $(arrL).each(function () {            
            $(this).datepicker({
                autoclose: true
            });
        });
        var arrL = $('#<%=gvWHO1.ClientID %>').find('input[id$="txtCurrentWho1Date1"]');
        $(arrL).each(function () {
            $(this).datepicker({
                autoclose: true
            });
        });
        var arrL = $('#<%=gvWHO2.ClientID %>').find('input[id$="txtCurrentWho2Date"]');
        $(arrL).each(function () {
            $(this).datepicker({
                autoclose: true
            });
        });
        var arrL = $('#<%=gvWHO2.ClientID %>').find('input[id$="txtCurrentWho2Date1"]');
        $(arrL).each(function () {
            $(this).datepicker({
                autoclose: true
            });
        });
        var arrL = $('#<%=gvWHO3.ClientID %>').find('input[id$="txtCurrentWHO3Date"]');
        $(arrL).each(function () {
            $(this).datepicker({
                autoclose: true
            });
        });
        var arrL = $('#<%=gvWHO3.ClientID %>').find('input[id$="txtCurrentWho3Date1"]');
        $(arrL).each(function () {
            $(this).datepicker({
                autoclose: true
            });
        });
        var arrL = $('#<%=gvWHO4.ClientID %>').find('input[id$="txtCurrentWHO4Date"]');
        $(arrL).each(function () {
            $(this).datepicker({
                autoclose: true
            });
        });
        var arrL = $('#<%=gvWHO4.ClientID %>').find('input[id$="txtCurrentWho4Date1"]');
        $(arrL).each(function () {
            $(this).datepicker({
                autoclose: true
            });
        });
    });
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
<table class="table-condensed" width="100%">
<tr>
        <td colspan="2" class="border pad5 whitebg">
        <div class="box box-default box-solid">                                        
            <div class="box-header with-border" style="background-color:#eeeeee;">
				        <h3 class="box-title">
						            Staging
					        </h3>
                                <div class="box-tools pull-right">
                                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                                </button>
                                </div>
				                        
			        </div>
            <div class="box-body">           
                <table id="Table11" class=" center formbg" cellspacing="6" cellpadding="0" width="100%"
                    border="0">
                    <tr id="trnonadultfollowup" style="display: none;">
                        <td width="50%" class="border pad5 whitebg">
                            <table width="100%">
                                <tr>
                                    <td align="right" style="width: 55%;">
                                        <label>
                                            WHO Stage at initiation (Transfer in):</label>
                                    </td>
                                    <td align="left" class="data-control">
                                        <asp:DropDownList ID="ddlInitiationWHOstage" runat="server" Width="130px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="50%" class="border pad5 whitebg">
                            <table class="tbl-left">
                                <tr>
                                    <td align="right" class="data-lable">
                                        <label class="required">
                                            HIV associated conditions:</label>
                                    </td>
                                    <td align="left" class="data-control">
                                        <asp:DropDownList ID="ddlhivassociated" runat="server" Width="130px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td width="50%" class="border pad5 whitebg">
                            <table width="100%">
                                <tr>
                                    <td align="right" style="width: 55%;">
                                        <asp:Label ID="lblWHOStage" runat="server" CssClass="required" Font-Bold="True" Text="Current WHO Stage:"></asp:Label>
                                    </td>
                                    <td align="left" class="data-control">
                                        <asp:DropDownList ID="ddlwhostage1" runat="server" Width="130px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="50%" class="border pad5 whitebg">
                            <table class="tbl-left">
                                <tr>
                                    <td align="right" style="width: 40%;">
                                        <asp:Label ID="lblWABStage" runat="server" CssClass="required" Font-Bold="True" Text="WAB Stage:"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList Width="100%" runat="server" ID="ddlWABStage">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 1%;">
                                        <asp:HiddenField ID="hiddateshow" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td colspan="2">
                            <div id="divmernarcheshow">
                                
                                <table class="border pad6 whitebg" width="100%">
                                    <tr>
                                        <td style="width: 15%;" align="right" class="pad5">
                                            <label>
                                                Mernarche:
                                            </label>
                                        </td>
                                        <td style="width: 35%;" align="left">
                                            <input id="radbtnMernarcheyes" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);rblSelectedValue2(1,'divmenarchedatehshowhide','ctl00_IQCareContentPlaceHolder_tabControl_TabExamination_txtmenarchedate');"
                                                type="radio" name="labevaluationyes" runat="server" />
                                            <label>
                                                Yes</label>
                                            <input id="radbtnMernarcheno" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);rblSelectedValue2(0,'divmenarchedatehshowhide','ctl00_IQCareContentPlaceHolder_tabControl_TabExamination_txtmenarchedate');"
                                                type="radio" name="labevaluationyes" runat="server" />
                                            <label>
                                                No</label>
                                        </td>
                                        <td style="width: 35%;" align="left">
                                            <div id="divmenarchedatehshowhide" style="display: none;">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <label id="Label88">
                                                                Menarche Date:
                                                            </label>
                                                        </td>
                                                        <td>
                                                        <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                 </div>
                                                            <input id="txtmenarchedate" maxlength="11" size="11" name="VisitDate" runat="server" />
                                                       </div>     
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
                    <tr>
                        <td width="50%" class="border pad5 whitebg" colspan="2">
                            <div id="divtannerstaging">
                                <table class="tbl-left">
                                    <tr>
                                        <td align="right" class="data-lable">
                                            <label>
                                                Tanner Staging:</label>
                                        </td>
                                        <td align="left" class="data-control">
                                            <asp:DropDownList ID="ddltannerstaging" runat="server" AutoPostBack="false" Width="130px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr id="trPatFUstatus" style="display: none;">
                        <td width="50%" class="border pad5 whitebg" colspan="2">
                            <div id="div1">
                                <table class="tbl-left">
                                    <tr>
                                        <td align="right" class="data-lable">
                                            <label>
                                                Patient Follow Up Status:</label>
                                        </td>
                                        <td align="left" class="data-control">
                                            <asp:DropDownList ID="ddlPatFUstatus" runat="server" AutoPostBack="false" Width="130px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
           </div>
        </td>
    </tr>   
    <tr>
        <td colspan="2">
        <div class="box box-default collapsed-box box-solid">                                        
            <div class="box-header with-border" style="background-color:#eeeeee;">
				        <h3 class="box-title">
						            WHO Stage I
					        </h3>
                                <div class="box-tools pull-right">
                                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-plus"></i>
                                </button>
                                </div>
				                        
			        </div>
            <div class="box-body">
                <table width="100%" border="0" cellspacing="6" cellpadding="0">
                    <tbody>
                        <tr>
                            <td class="border center pad5 whitebg" style="width: 50%;" colspan="2">
                                <asp:Panel ID="PnlWHO1" runat="server" class="table-responsive  pre-scrollable box" Height="100px">
                                    <asp:GridView ID="gvWHO1" runat="server" AutoGenerateColumns="false" Width="800px"
                                        ShowFooter="false" GridLines="None">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Presenting WHO Stage I Conditions" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblwho1" runat="server" Text='<%# Eval("ID") %>' Visible="false"></asp:Label>
                                                    <asp:CheckBox ID="Chkwho1" runat="server" Text='<%# Eval("NAME") %>' onclick="SelectWHOStage()" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Current" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                 </div>
                                                    <input id="txtCurrentWho1Date" class="form-control" name="date" style="width:120px" data-date-format="dd-M-yyyy" runat="server" />
                                                   </div> 
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Historic" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                 </div>
                                                    <input id="txtCurrentWho1Date1" class="form-control" name="date" style="width:120px" data-date-format="dd-M-yyyy" runat="server" />
                                                  </div>  
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="2">
        <div class="box box-default collapsed-box box-solid">                                        
            <div class="box-header with-border" style="background-color:#eeeeee;">
				        <h3 class="box-title">
						            WHO Stage II
					        </h3>
                                <div class="box-tools pull-right">
                                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-plus"></i>
                                </button>
                                </div>
				                        
			        </div>
            <div class="box-body">           
                <table width="100%" border="0" cellspacing="6" cellpadding="0">
                    <tbody>
                        <tr>
                            <td class="border center pad5 whitebg" style="width: 50%;" colspan="2">
                                <asp:Panel ID="PnlWHO2" runat="server" class="table-responsive  pre-scrollable box" Height="200px">
                                    <asp:GridView ID="gvWHO2" runat="server" AutoGenerateColumns="false" Width="800px"
                                        ShowFooter="false" GridLines="None">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Presenting WHO Stage II Conditions" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblwho2" runat="server" Text='<%# Eval("ID") %>' Visible="false"></asp:Label>
                                                    <asp:CheckBox ID="Chkwho2" runat="server" Text='<%# Eval("NAME") %>' onclick="SelectWHOStage()" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Current" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                 </div>
                                                    <input id="txtCurrentWho2Date" class="form-control" style="width:120px" data-date-format="dd-M-yyyy" runat="server" />
                                                   </div> 
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Historic" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                 </div>
                                                    <input id="txtCurrentWho2Date1" class="form-control" style="width:120px" data-date-format="dd-M-yyyy" runat="server" />
                                                  </div>  
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
        </td>
    </tr>
    <tr>
        <td colspan="2" >
        <div class="box box-default collapsed-box box-solid">                                        
            <div class="box-header with-border" style="background-color:#eeeeee;">
				        <h3 class="box-title">
						            WHO Stage III
					        </h3>
                                <div class="box-tools pull-right">
                                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-plus"></i>
                                </button>
                                </div>
				                        
			        </div>
            <div class="box-body">             
                <table width="100%" border="0" cellspacing="6" cellpadding="0">
                    <tbody>
                        <tr>
                            <td class="border center pad5 whitebg" colspan="2">
                                <asp:Panel ID="PnlWHO3" runat="server" class="table-responsive  pre-scrollable box" Height="200px">
                                    <asp:GridView ID="gvWHO3" runat="server" AutoGenerateColumns="false" Width="800px"
                                        ShowFooter="false" BorderStyle="None" GridLines="None">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Presenting WHO Stage III Conditions" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWHO3" runat="server" Text='<%# Eval("ID") %>' Visible="false"></asp:Label>
                                                    <asp:CheckBox ID="ChkWHO3" runat="server" Text='<%# Eval("NAME") %>' onclick="SelectWHOStage()" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Current" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i> 
                                                    </div>
                                                    <input id="txtCurrentWHO3Date" class="form-control" style="width:120px" data-date-format="dd-M-yyyy" runat="server" />
                                                  </div>  
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Historic" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                 </div>
                                                    <input id="txtCurrentWho3Date1" class="form-control" style="width:120px" data-date-format="dd-M-yyyy" runat="server" />
                                                   </div> 
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
        </td>
    </tr>
    <tr>
        <td colspan="2" >
            <div class="box box-default collapsed-box box-solid">                                        
            <div class="box-header with-border" style="background-color:#eeeeee;">
				        <h3 class="box-title">
						            WHO Stage IV
					        </h3>
                                <div class="box-tools pull-right">
                                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-plus"></i>
                                </button>
                                </div>
				                        
			        </div>
            <div class="box-body">
           
                <table width="100%" border="0" cellspacing="6" cellpadding="0">
                    <tbody>
                        <tr>
                            <td class="border center pad5 whitebg" style="width: 50%;" colspan="2">
                                <asp:Panel ID="PnlWHO4" runat="server" class="table-responsive  pre-scrollable box" Height="200px">
                                    <asp:GridView ID="gvWHO4" runat="server" AutoGenerateColumns="false" Width="800px"
                                        ShowFooter="false" GridLines="None">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Presenting WHO Stage IV Conditions" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWHO4" runat="server" Text='<%# Eval("ID") %>' Visible="false"></asp:Label>
                                                    <asp:CheckBox ID="ChkWHO4" runat="server" Text='<%# Eval("NAME") %>' onclick="SelectWHOStage()" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Current" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                 </div>
                                                    <input id="txtCurrentWHO4Date" class="form-control" style="width:120px" data-date-format="dd-M-yyyy" runat="server" />
                                                   </div> 
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Historic" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                 </div>
                                                    <input id="txtCurrentWho4Date1" class="form-control" style="width:120px" data-date-format="dd-M-yyyy" runat="server" />
                                                   </div> 
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
        </td>
    </tr>
    
    
</table>
