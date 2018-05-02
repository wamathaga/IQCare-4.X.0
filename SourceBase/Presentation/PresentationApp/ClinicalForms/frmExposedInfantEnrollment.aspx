<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    EnableEventValidation="false" EnableViewState="true" Inherits="ClinicalForms_frmExposedInfantEnrollment"
    CodeBehind="frmExposedInfantEnrollment.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <%-- <form id="frmExposedInfantEnrollment" method="post" runat="server">--%>
   <script type="text/javascript">
       function RegisterJQuery() {
           $('#TxtDateOfBirth').datepicker({ autoclose: true });
           $('#TxtDeathDate').datepicker({ autoclose: true });           
       }
       //Calling RegisterJQuery when document is ready (Page loaded first time)
       $(document).ready(RegisterJQuery);
       //Calling RegisterJQuery when the page is doing postback (asp.net)
       Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RegisterJQuery);
</script>
        <script language="Javascript" type="text/javascript">

            function isNumberKey(evt) {
                var charCode = (evt.which) ? evt.which : event.keyCode
                if (charCode > 31 && (charCode < 48 || charCode > 57))
                    return false;

                return true;
            }

            function CalEnbleDisble() {

                document.getElementById("ctl00_IQCareContentPlaceHolder_spDeath").style.display = "block";
            }
            function spDeathCon(id) {
                //Change by RAHMAT(3-Mar-2017) because Rctl00_IQCareContentPlaceHolder_DDFinalesult line not read the value of text box when ClientIDMode is Static 
                var e = document.getElementById('<%=DDFinalResult.ClientID %>');
                document.getElementById('<%=TxtDeathDate.ClientID %>').value = '';
                var strVal = e.options[e.selectedIndex].value;
                if (strVal == "9") {
                    document.getElementById("ctl00_IQCareContentPlaceHolder_spDeath").style.display = "block";
                }
                else {
                    document.getElementById("ctl00_IQCareContentPlaceHolder_spDeath").style.display = "none";
                }
            }
        </script>
        <div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">Exposed Infant Follow up</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content--%>
          <div class="row">
          <br />
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label for="inputEmail3" class="control-label">Infant First Name:</label>
             </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="TxtFirstName" runat="server"  MaxLength="50" class="form-control" OnTextChanged="TxtFirstName_TextChanged"></asp:TextBox>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group"><label for="inputEmail3" class="control-label">Infant Last Name:</label>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <asp:TextBox ID="TxtLastName" runat="server" class="form-control" Width="98%"  MaxLength="50" OnTextChanged="TxtLastName_TextChanged"></asp:TextBox>
            </div>
             </div>
             <div class="row">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label for="inputEmail3" class="control-label">Unique ID:</label>
             </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <asp:TextBox ID="TxtInfantId" runat="server" class="form-control" MaxLength="10" onkeypress="return isNumberKey(event)"
                                    OnTextChanged="TxtInfantId_TextChanged"></asp:TextBox>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <label for="inputEmail3" class="control-label">Date of Birth:</label>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="TxtDateOfBirth" clientidmode="Static"
                                                       onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"
                                    onfocus="javascript:vDateType='3'" maxlength="11" size="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
            
            
                                </div>

            </div>
           
              <div class="row">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label for="inputEmail3" class="control-label">Infant Feeding Practice at 3 Months:</label>
             </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group"><asp:DropDownList ID="DDInfantFeedingPractice" runat="server" class="form-control">
                                </asp:DropDownList>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group"><label for="inputEmail3" class="control-label">CPT Started by 2 Months:</label>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <asp:CheckBox ID="ChkCPTStarted" runat="server" Checked="false" />
            </div>
             </div>
              <div class="row">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label for="inputEmail3" class="control-label">HIV Test Type:</label>
             </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group"><asp:DropDownList ID="DDHIVTestType" runat="server" class="form-control" Width="150">
                                </asp:DropDownList>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group"><label for="inputEmail3" class="control-label">Result</label>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group"> <asp:DropDownList ID="DDResult" runat="server" class="form-control" Width="150">
                                </asp:DropDownList>
            </div>
             </div>
              <div class="row">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label for="inputEmail3" class="control-label">Final Result:</label>
             </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <asp:DropDownList ID="DDFinalResult" runat="server" onchange="spDeathCon(this);" CssClass="form-control">
                                </asp:DropDownList>
            </div>
            <div id="spDeath" runat="server" style="display: none">
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <label for="inputEmail3" class="control-label">Death Date:</label>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="TxtDeathDate" clientidmode="Static"
                                                       maxlength="11" size="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
            
            </div>
            </div>
             </div>
              <div class="row col-sm-12 col-xs-12 form-group" align="center">
            <asp:Button ID="btnAdd" Text="Add Infant" runat="server" OnClick="btnAdd_Click1"
                                   CssClass="btn btn-primary" Height="30px" Width="11%" Style="text-align: left;" />
                                <label class="glyphicon glyphicon-plus" style="margin-left: -2.5%; margin-right: 2%;
                                    vertical-align: sub; color: #fff;">
                                </label>
             </div>
              <div class="row col-sm-12 col-xs-12 form-group" align="center">
              <div id="div-gridview" class="gridview whitebg">
                                    <div class="whitebg" style="height: 300px; overflow: auto">
                                        <asp:GridView ID="grdChildInfo" runat="server" EnableViewState="true" Width="100%"
                                            BorderColor="#666699" AutoGenerateColumns="False" CellSpacing="1" AllowSorting="True"
                                            OnRowDataBound="grdChildInfo_RowDataBound" OnSorting="grdChildInfo_Sorting" OnRowDeleting="grdChildInfo_RowDeleting"
                                            OnSelectedIndexChanging="grdChildInfo_SelectedIndexChanging">
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            <AlternatingRowStyle BackColor="White" BorderColor="Silver" />
                                            <Columns>
                                                <asp:BoundField HeaderText="First Name" DataField="FirstName" ItemStyle-HorizontalAlign="Center"
                                                    ReadOnly="true">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Last Name" DataField="LastName" ItemStyle-HorizontalAlign="Center"
                                                    ReadOnly="true">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Unique ID" DataField="ExposedInfantId" ItemStyle-HorizontalAlign="Center"
                                                    ReadOnly="true">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="DOB" DataField="DOB" ItemStyle-HorizontalAlign="Center"
                                                    ReadOnly="true" DataFormatString="{0:dd MMM yyyy}" HtmlEncode="false">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Infant Feeding" DataField="FeedingPractice3mos" ItemStyle-HorizontalAlign="Center"
                                                    ReadOnly="true">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="CPT Started" DataField="CTX2mos" ItemStyle-HorizontalAlign="Center"
                                                    ReadOnly="true">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="HIV Test Type" DataField="HIVTestType" ItemStyle-HorizontalAlign="Center"
                                                    ReadOnly="true">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="HIV Result" DataField="HIVResult" ItemStyle-HorizontalAlign="Center"
                                                    ReadOnly="true">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Final Result" DataField="FinalStatus" ItemStyle-HorizontalAlign="Center"
                                                    ReadOnly="true">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Death Date" DataField="DeathDate" ItemStyle-HorizontalAlign="Center"
                                                    ReadOnly="true" DataFormatString="{0:dd MMM yyyy}" HtmlEncode="false">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:CommandField ButtonType="Link" DeleteText="<img src='../Images/del.gif' alt='Delete this' border='0' />"
                                                    ShowDeleteButton="true" />
                                                <asp:BoundField DataField="HivResultId" Visible="false" />
                                                <asp:BoundField DataField="FeedID" Visible="false" />
                                                <asp:BoundField DataField="FinalStatusID" Visible="false" />
                                                <asp:BoundField DataField="HIVTestTypeID" Visible="false" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
              </div>
               <div class="row col-sm-12 col-xs-12 form-group" align="center">
             <asp:TextBox ID="txtSysDate" runat="server" CssClass="form-control" Width="14%"></asp:TextBox>
             </div>
              <div class="row col-sm-12 col-xs-12 form-group" align="center">
                <asp:Button ID="btnsave" Text="Save" runat="server" OnClick="btnsave_Click" CssClass="btn btn-primary"
                                    Height="30px" Width="8%" Style="text-align: left;"></asp:Button>
                                <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; margin-right: 2%;
                                    vertical-align: sub; color: #fff;">
                                </label>
                                <asp:Button ID="btnCancel" Text="Close" runat="server" OnClick="btnCancel_Click"
                                    CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                                <label class="glyphicon glyphicon-remove-circle" style="margin-left: -3%; margin-right: 2%;
                                    vertical-align: sub; color: #fff;">
                                </label>
              </div>
              <br />
         </div>
      </div>
      </div>
      </div>
     </div>
     </div>

</asp:Content>
