<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControl_NigeriaMedicalHistory.ascx.cs"
    Inherits="PresentationApp.ClinicalForms.UserControl.UserControl_NigeriaMedicalHistory" %>
<style type="text/css">
    td
    { word-break: break-all;
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
    });

    function toggleMedicalPC(strcblcontrolId) {

        var GV = document.getElementById("<%= gvMedicalHistory.ClientID %>");
        //var GridView = strcblcontrolId.parentNode.parentNode;
        var inputList = GV.getElementsByTagName("input");
        var arrayOfCheckBoxLabels = GV.getElementsByTagName("label");
        //alert(inputList[0].Id);
        //alert(inputList[0].checked);
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
                        txtbx.disabled = false;
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
//                    inputs[0].checked = false;
//                    var txtbx = GV.rows[i].cells[1].children[0];
//                    txtbx.disabled = true;
//                    txtbx.value = '';
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
    function show_Medhide(controlID, status) {
        var s = document.getElementById(controlID);
        if (status == "notvisible") {
            s.style.display = "none";
        }
        else {
            //s.style.display = "block";
        }
    }
</script>
<div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
            <div id="divPresentingComplaints" class="divborder margin10" style="margin-top: 10;
                margin-bottom: 10;">
                <asp:GridView ID="gvMedicalHistory" runat="server" AutoGenerateColumns="False"
                    Width="100%" GridLines="None" 
                    OnRowDataBound="gvPresentingComplaints_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Complaint" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                            <HeaderStyle ForeColor="Navy" />
                            <ItemTemplate>
                                <asp:Label ID="lblMedical" runat="server" Text='<%# Eval("ID")%>' Visible="false"></asp:Label>
                                <asp:CheckBox ID="ChkMedical" runat="server" Text='<%# Eval("NAME")%>' />
                            </ItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Duration(Days)" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left">
                            <HeaderStyle ForeColor="Navy" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtMedical" runat="server" Width="150px" CssClass="form-control" > 
                                </asp:TextBox>                                
                            </ItemTemplate>

<ItemStyle HorizontalAlign="Left" Height="40"></ItemStyle>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
             </div>
            
 </div>
 <div class="row" id="DivtoggleMedicalPC" style="display: none;">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Specify other Presenting Complaints:</label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtOtherPresentingComplaints" runat="server" Width="99%" Rows="3" TextMode="MultiLine" CssClass="form-control" Style="resize:none;"></asp:TextBox>
             </div>
              
 </div>
 <div class="row">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Additional Comments:</label>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Past Medical History:</label>
             </div>             
 </div>
 <div class="row">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
            <asp:TextBox ID="txtAdditionalComplaints" runat="server" TextMode="MultiLine" Width="99%" Rows="5" CssClass="form-control" Style="resize:none:"></asp:TextBox>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtlastmedical" runat="server" Width="99%" Rows="5" TextMode="MultiLine" CssClass="form-control" Style="resize:none:"></asp:TextBox>
             </div>             
 </div>
 <div id="divshowMedicalHistory">
 <div class="row">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Relevant Family History:</label>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Hospitalisation:</label>
             </div>             
 </div>
 <div class="row">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
           <asp:TextBox ID="txtfamhistory" runat="server" TextMode="MultiLine" Width="99%" Rows="5" CssClass="form-control" Style="resize:none:"></asp:TextBox>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txthospitalization" runat="server" Width="99%" Rows="5" TextMode="MultiLine" CssClass="form-control" Style="resize:none:"></asp:TextBox>
             </div>             
 </div>
 </div>


