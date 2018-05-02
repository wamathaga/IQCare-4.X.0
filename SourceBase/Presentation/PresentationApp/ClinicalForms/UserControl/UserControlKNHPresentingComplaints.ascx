<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControlKNHPresentingComplaints.ascx.cs"
    Inherits="PresentationApp.ClinicalForms.UserControl.UserControlKNHPresentingComplaints" %>
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
<script language="javascript" type="text/javascript">
    function ShowHideDiv(theDiv) {
        if ($("#" + theDiv).is(':visible')) {
            $("#" + theDiv).hide();
            document.getElementById('ctl00_IQCareContentPlaceHolder_tabControl_TabClinicalHistory_UcPc_txtAdditionPresentingComplaints').value = '';
        }
        else
        { $("#" + theDiv).show(); }
    }
    $(document).ready(function () {
        $(".checkbox").on("change", function () {

            if ($(this).is(":checked")) {
                alert('a');
                $(".selectAll").prop('checked', false);
            }
        });

        var GV = document.getElementById("<%= gvPresentingComplaints.ClientID %>");
        var inputList = GV.getElementsByTagName("input");
        var arrayOfCheckBoxLabels = GV.getElementsByTagName("label");
        if (arrayOfCheckBoxLabels[0].innerText == "None") {
            if (GV.rows.length > 0) {
                var lbl = GV.rows[1].getElementsByTagName('label');
                var txt = GV.rows[1].getElementsByTagName('text');
                if (lbl[0].innerText == "None") {
                    lbl[0].style.marginLeft = "-2%";
                }
            }
        }
    });

    function togglePC(strcblcontrolId) {
        var GV = document.getElementById("<%= gvPresentingComplaints.ClientID %>");
        var inputList = GV.getElementsByTagName("input");
        var arrayOfCheckBoxLabels = GV.getElementsByTagName("label");
        if ((inputList[0].checked == true) && (arrayOfCheckBoxLabels[0].innerText == "None")) {
            if (GV.rows.length > 0) {
                for (var i = 1; i < GV.rows.length; i++) {
                    var inputs = GV.rows[i].getElementsByTagName('input');
                    var lbl = GV.rows[i].getElementsByTagName('label');
                    var txt = GV.rows[i].getElementsByTagName('text');
                    //alert(inputList[0].Id);
                    if (lbl[0].innerText != "None") {
                        inputs[0].checked = false;
                        var txtbx = GV.rows[i].cells[1].children[0];
                        txtbx.disabled = true;
                        inputs[0].disabled = true;
                      
                    }
                    else if (lbl[0].innerText == "None") {
                        var txtbx = GV.rows[i].cells[1].children[0];
                        //txtbx.disabled = false;
                       
                    }
                }
            }
        }
        else if ((inputList[0].checked == true) && (arrayOfCheckBoxLabels[0].innerText != "None")) {

            if (GV.rows.length > 0) {
                for (var i = 1; i < GV.rows.length; i++) {
                    var inputs = GV.rows[i].getElementsByTagName('input');
                    var lbl = GV.rows[i].getElementsByTagName('label');
                    if (lbl[0].innerText == "None") {
                        inputs[0].checked = false;
                        var txtbx = GV.rows[i].cells[1].children[0];
                        txtbx.disabled = true;
                    }
                    else {
                        var txtbx = GV.rows[i].cells[1].children[0];
                        txtbx.disabled = false;
                    }
                }
            }
        }
        else if ((inputList[0].checked == false) && (arrayOfCheckBoxLabels[0].innerText == "None")) {
            if (GV.rows.length > 0) {
                for (var i = 1; i < GV.rows.length; i++) {
                    var inputs = GV.rows[i].getElementsByTagName('input');
                    var lbl = GV.rows[i].getElementsByTagName('label');

                    if (lbl[0].innerText == "None") {
                        //inputs[0].checked = false;
                        var txtbx = GV.rows[i].cells[1].children[0];
                        txtbx.disabled = true;
                        txtbx.value = '';
                    }
                    else {
                        var txtbx = GV.rows[i].cells[1].children[0];
                        txtbx.disabled = false;
                        //txtbx.value = '';
                        inputs[0].disabled = false;
                    }
                }
            }
        }
    }
</script>
<div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <div id="divPresentingComplaints" class="divborder margin10" style="margin-top: 10;
                margin-bottom: 10;margin-left:15px;">
                <div class="row" align="center">
                <asp:GridView ID="gvPresentingComplaints" runat="server" AutoGenerateColumns="False"
                    Width="100%" GridLines="None" OnRowDataBound="gvPresentingComplaints_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Complaint" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                            <HeaderStyle ForeColor="Navy" />
                            <ItemTemplate>
                                <asp:Label ID="lblPresenting" runat="server" Text='<%# Eval("ID")%>' Visible="false"></asp:Label>
                                <asp:CheckBox ID="ChkPresenting" runat="server" Text='<%# Eval("NAME")%>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Duration(Days)" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left">
                            <HeaderStyle ForeColor="Navy" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtPresenting" runat="server" Width="150px" CssClass="form-control"> 
                                </asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Height="40"></ItemStyle>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                </div>
            </div>
             </div>
            
 </div>
 <div id="DivOther" style="display: none;">
             <div class="row">            
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label for="inputEmail3" class="control-label">
                                Specify other Presenting Complaints:</label>
             </div>
            <div class="col-md-9 col-sm-12 col-xs-12 form-group">
            <asp:TextBox ID="txtOtherPresentingComplaints" runat="server" Width="99%" Rows="3" Style="resize:none;" CssClass="form-control"
                                TextMode="MultiLine"></asp:TextBox>
            </div>
                
                </div>
            </div>
<div class="row">      
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <label for="txtAdditionalComplaints" class="control-label">
                Additional Complaints:</label>
             </div>
            <div class="col-md-9 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtAdditionalComplaints" runat="server" TextMode="MultiLine" Style="resize:none;" Width="99%"
                Rows="3" CssClass="form-control"></asp:TextBox>
            </div>
           
             </div>
