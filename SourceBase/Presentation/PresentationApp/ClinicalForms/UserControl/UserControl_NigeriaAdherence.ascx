<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControl_NigeriaAdherence.ascx.cs"
    Inherits="PresentationApp.ClinicalForms.UserControl.UserControl_NigeriaAdherence" %>
    <script type="text/javascript">
        function RegisterJQuery() {
            $('#txtdtIntrupptedDate').datepicker({ autoclose: true });
            $('#txtStopedReasonDate').datepicker({ autoclose: true });  
        }
        //Calling RegisterJQuery when document is ready (Page loaded first time)
        $(document).ready(RegisterJQuery);
        //Calling RegisterJQuery when the page is doing postback (asp.net)
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RegisterJQuery);
</script>    
<script language="javascript" type="text/javascript">
    function ShowHideDiv(theDiv) {

        if ($("#" + theDiv).is(':visible')) {
            $("#" + theDiv).hide();
            document.getElementById(<%=txtOtherdisclosed.ClientID %>).value = '';
        }
        else {
            $("#" + theDiv).show();
        }
    }
    $(document).ready(function () {
        $(".checkbox").on("change", function () {

            if ($(this).is(":checked")) {
                alert('a');
                $(".selectAll").prop('checked', false);
            }
        });
    });
    function rblSelectedValue(rbl, divID) {
        var selectedvalue = $("#" + rbl.id + " input:radio:checked").val();
        if (selectedvalue == "1") {
            YN = "show";
        }
        else {
            YN = "hide";
        }
        ShowHide(divID, YN);
    }

    function toggleDisclosedPC(strcblcontrolId) {

        var GV = document.getElementById("<%= gvdisclosed.ClientID %>");

        var inputList = GV.getElementsByTagName("input");
        var arrayOfCheckBoxLabels = GV.getElementsByTagName("label");

        if ((inputList[0].checked == true) && (arrayOfCheckBoxLabels[0].innerText == "No one")) {
            if (GV.rows.length > 0) {
                for (var i = 1; i < GV.rows.length; i++) {
                    var inputs = GV.rows[i].getElementsByTagName('input');
                    var lbl = GV.rows[i].getElementsByTagName('label');
                    if (lbl[0].innerText != "No one") {
                        inputs[0].checked = false;

                        inputs[0].disabled = true;
                    }
                    else if (lbl[0].innerText == "No one") {
                    }
                }
            }
        }
        else if ((inputList[0].checked == true) && (arrayOfCheckBoxLabels[0].innerText != "No one")) {

            if (GV.rows.length > 0) {
                for (var i = 1; i < GV.rows.length; i++) {
                    var inputs = GV.rows[i].getElementsByTagName('input');
                    var lbl = GV.rows[i].getElementsByTagName('label');
                    if (lbl[0].innerText == "No one") {
                        inputs[0].checked = false;

                    }
                    else {

                    }
                }
            }
        }
        else if ((inputList[0].checked == false) && (arrayOfCheckBoxLabels[0].innerText == "No one")) {
            if (GV.rows.length > 0) {
                for (var i = 1; i < GV.rows.length; i++) {
                    var inputs = GV.rows[i].getElementsByTagName('input');
                    var lbl = GV.rows[i].getElementsByTagName('label');

                    if (lbl[0].innerText == "No one") {

                    }
                    else {

                        inputs[0].disabled = false;
                    }
                }
            }
        }
    }
</script>
<div class="row">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">Participating in an adherence program:</label>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group text-left">
              <asp:RadioButtonList ID="rbladherenceYesNo" runat="server" RepeatDirection="Horizontal" CssClass="checkbox-btn">
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
             </div>
         
 </div>
 <div class="row">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label class="control-label" for="rblmissedarvYesNo">Missed ARV in the last 3 days:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:RadioButtonList ID="rblmissedarvYesNo" runat="server" onclick="rblSelectedValue(this,'DIVmissedarv');"
                            RepeatDirection="Horizontal" CssClass="checkbox-btn">
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
             </div>
             <div id="DIVmissedarv" style="display: none;">
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Reason missed:</label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
            <asp:DropDownList ID="ddlReasomMissed" runat="server" Width="99%" CssClass="form-control">
                            </asp:DropDownList>
             </div> 
             </div>
 </div>
 <div class="row">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label class="control-label" for="rblmissedarvYesNo">Treatment was Interrupted:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:RadioButtonList ID="rdoTreatmentIntrupted" runat="server" onclick="rblSelectedValue(this,'DIVInturptedReason');"
                            RepeatDirection="Horizontal" CssClass="checkbox-btn">
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
           
             </div> 
 </div>
 <div class="row" id="DIVInturptedReason" style="display: none;">
 <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Date:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtdtIntrupptedDate" clientidmode="Static"
                                                       maxlength="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;" 
                                                       onblur="DateFormat(this,this.value,event,false,'3')"
                                            onfocus="javascript:vDateType='3'" onkeyup="DateFormat(this,this.value,event,false,'3')"
                                            size="11"/>
                                                </div>
                                            </div>
                                            <img id="Img1" alt="Date Helper" border="0" height="22 " hspace="5" name="appDateimg"
                                            onclick="w_displayDatePicker('<%=txtdtIntrupptedDate.ClientID%>');" src="../Images/cal_icon.gif"
                                            style="vertical-align: text-bottom; visibility:hidden" width="22" />  
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Number of Days:</label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtintrpdays" MaxLength="3" runat="server" Width="70" CssClass="form-control"></asp:TextBox>
             </div> 
             <div class="col-md-2 col-sm-12 col-xs-12 form-group text-nowrap">
             <label  class="control-label">Reason Interrupted:</label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlreasonInterrupted" runat="server" Width="99%" CssClass="form-control">
                                        </asp:DropDownList>
             </div>
 </div>
 <div class="row">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Treatment was Stopped:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:RadioButtonList ID="rblstopped" runat="server" onclick="rblSelectedValue(this,'DIVStopedReason');"
                            RepeatDirection="Horizontal" CssClass="checkbox-btn">
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             
             </div> 
 </div>
 <div class="row" id="DIVStopedReason" style="display: none;">
 <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Date:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtStopedReasonDate" clientidmode="Static"
                                                       maxlength="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;" 
                                                       onblur="DateFormat(this,this.value,event,false,'3')"
                                onfocus="javascript:vDateType='3'" onkeyup="DateFormat(this,this.value,event,false,'3')"
                                size="11"/>
                                                </div>
                                            </div>            
                            <img id="Img2" alt="Date Helper" border="0" height="22 " hspace="5" name="appDateimg"
                                onclick="w_displayDatePicker('<%=txtStopedReasonDate.ClientID%>');" src="../Images/cal_icon.gif"
                                style="vertical-align: text-bottom; visibility:hidden;" width="22" />
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Number of Days:</label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtstoppeddays" MaxLength="3" runat="server" Width="70" CssClass="form-control"></asp:TextBox>
             </div> 
              <div class="col-md-2 col-sm-12 col-xs-12 form-group text-nowrap">
             <label class="control-label">Reason Interrupted:</label>
             </div>
              <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlStopedReason" runat="server" Width="99%" CssClass="form-control">
                            </asp:DropDownList>
             </div>
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <div id="divdisclosed" style="margin-top: 10; margin-bottom: 10;">
                <asp:GridView ID="gvdisclosed" runat="server" AutoGenerateColumns="False" Width="100%"
                    GridLines="None" OnRowDataBound="gvdisclosed_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Patient has disclosed status to:" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left">
                            <HeaderStyle ForeColor="Navy" />
                            <ItemTemplate>
                                <asp:Label ID="lbldisclosed" runat="server" Text='<%# Eval("ID")%>' Visible="false"></asp:Label>
                                <asp:CheckBox ID="Chkdisclosed" runat="server" Text='<%# Eval("NAME")%>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
             </div>
           
 </div>
 <div class="row" id="DivDiscloseOther" style="display: none;">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Specify other disclosed:</label>
             </div>
             <div class="col-md-9 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtOtherdisclosed" runat="server" Width="99%" Rows="3"
                                TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
             </div>
             
 </div>
 <div class="row">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label class="control-label">HIV Status can be discussed with:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
              <asp:TextBox ID="txthivdiscussed" runat="server" Width="200" CssClass="form-control" ></asp:TextBox>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Is the patient a member of as support group?:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group ">
             <asp:RadioButtonList ID="rblsupportgroup" runat="server" RepeatDirection="Horizontal" CssClass="checkbox-btn">
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
             </div> 
 </div>

