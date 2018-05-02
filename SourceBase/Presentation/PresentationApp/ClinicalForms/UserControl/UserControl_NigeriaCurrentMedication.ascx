<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControl_NigeriaCurrentMedication.ascx.cs"
    Inherits="PresentationApp.ClinicalForms.UserControl.UserControl_NigeriaCurrentMedication" %>
    <script language="javascript" type="text/javascript">
        function ShowHideDiv(theDiv) {

            if ($("#" + theDiv).is(':visible')) {                
                $("#" + theDiv).hide();
                document.getElementById('ctl00_IQCareContentPlaceHolder_tabControl_TabClinicalHistory_UcPc_txtAdditionPresentingComplaints').value = '';
            }
            else
            {           
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

        function toggleCurrentPC(strcblcontrolId) {

            var GV = document.getElementById("<%= gvcurrentmedication.ClientID %>");
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
                        //var txt = GV.rows[i].getElementsByTagName('text');
                        //alert(inputList[0].Id);
                        if (lbl[0].innerText != "None") {
                            inputs[0].checked = false;
                            //var txtbx = GV.rows[i].cells[1].children[0];
                            //txtbx.disabled = true;
                            inputs[0].disabled = true;
                        }
                        else if (lbl[0].innerText == "None") {
                            //var txtbx = GV.rows[i].cells[1].children[0];
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
//                            var txtbx = GV.rows[i].cells[1].children[0];
//                            txtbx.disabled = true;
                        }
                        else {
//                            var txtbx = GV.rows[i].cells[1].children[0];
//                            txtbx.disabled = false;
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
//                            var txtbx = GV.rows[i].cells[1].children[0];
//                            txtbx.disabled = true;
//                            txtbx.value = '';
                        }
                        else {
//                            var txtbx = GV.rows[i].cells[1].children[0];
//                            txtbx.disabled = false;
                            //txtbx.value = '';
                            inputs[0].disabled = false;
                        }
                    }
                }
            }
        }
</script>
<div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
            <div id="divPresentingComplaints" style="width:98%">
                <asp:GridView ID="gvcurrentmedication" runat="server" AutoGenerateColumns="False"
                    Width="100%" GridLines="None" 
                    OnRowDataBound="gvcurrentmedication_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Current Medication" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                            <HeaderStyle ForeColor="Navy" />
                            <ItemTemplate>
                                <asp:Label ID="lblmedication" runat="server" Text='<%# Eval("ID")%>' Visible="false"></asp:Label>
                                <asp:CheckBox ID="Chkmedication" runat="server" Text='<%# Eval("NAME")%>' CssClass="checkbox-btn"/>
                            </ItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>                        
                    </Columns>
                </asp:GridView>
            </div>
             </div>
             
 </div>
 <div class="row" id="DivOtherComplaint" style="display: none;">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Other current medications:</label>
             </div>
             <div class="col-md-9 col-sm-12 col-xs-12 form-group">
            <asp:TextBox ID="txtOtherComplaints" runat="server" Width="99%" 
                                Rows="3" TextMode="MultiLine" CssClass="form-control" Style="resize:none;"></asp:TextBox>
             </div>
            
 </div>


