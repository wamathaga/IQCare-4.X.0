<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    Inherits="ClinicalForms_frmFollowUpEducationCTC" EnableEventValidation="false"
    CodeBehind="frmFollowUpEducationCTC.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
<script type="text/javascript">
    function RegisterJQuery() {
        $('#txtvisitdate').datepicker({ autoclose: true });
    }
    //Calling RegisterJQuery when document is ready (Page loaded first time)
    $(document).ready(RegisterJQuery);
    //Calling RegisterJQuery when the page is doing postback (asp.net)
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RegisterJQuery);
</script>
    <script language="javascript" type="text/javascript">
        function ShowHide(id) {
            var ShowOtherConType = 'tdotherctopic';
            if (id.value == 2) {
                document.getElementById(ShowOtherConType).style.display = 'inline';
            }
            else {
                document.getElementById(ShowOtherConType).style.display = 'none';
            }
        }
        function WindowPrint() {
            window.print();
        }
        function Redirect(id) {
            window.location.href = "../ClinicalForms/frmPatient_Home.aspx";
        }
    </script>

    <div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">Follow-up Education</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
          <div class="row">
          <br />
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label for="inputEmail3" class="control-label">Counselling Type:</label>
             </div>
            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
              <asp:DropDownList ID="ddlcouncellingtype" runat="server" Width="98%" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlcouncellingtype_SelectedIndexChanged" class="form-control">
                            </asp:DropDownList>
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group"><label for="inputEmail3" class="control-label">Counselling Topic:</label>
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlcouncellingtopic" runat="server" AutoPostBack="true" Width="98%"
                                OnSelectedIndexChanged="ddlcouncellingtopic_SelectedIndexChanged"  class="form-control">
                            </asp:DropDownList>
            
            </div>
             </div>
             <div class="row">
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label for="inputEmail3" class="control-label">Visit Date:</label>
             </div>
            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
            <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtvisitdate" clientidmode="Static"
                                                       maxlength="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
            
            </div>
            <div id="tdotherctopic" runat="server" visible="false" class="border center whitebg">
            <div class="col-md-2 col-sm-12 col-xs-12 form-group"><label for="inputEmail3" class="control-label">Other Counselling Topic:</label>
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
            <asp:TextBox ID="txtotherctopic" runat="server" class="form-control" Width="99%"></asp:TextBox>
            </div>
            </div>
             </div>
             <div class="row">
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label for="inputEmail3" class="control-label">Comments:</label>
             </div>
            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
            <asp:TextBox ID="txtcomments" runat="server" TextMode="MultiLine" Height="60" Width="99%" class="form-control" Style ="resize:none"></asp:TextBox>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group"><label for="inputEmail3" class="control-label"></label>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            </div>
             </div>
             <div class="row col-sm-12 col-xs-12 form-group" align="center">
             <asp:Button ID="btnaddtopic" runat="server" Text="Add Topic" OnClick="btnadd_Click"
                                CssClass="btn btn-primary" Width="11%" Height="30px" Style="text-align: left;" />
                            <label class="glyphicon glyphicon-plus" style="margin-left: -2.8%; vertical-align: sub;
                                color: #fff;">
                            </label>
             </div>
             <div class="row col-sm-12 col-xs-12 form-group" align="center">
               <div class="gridview whitebg">
                                <div class="whitebg" style="height: 300px; overflow: auto">
                                    <asp:GridView ID="grdFollowupEducation" GridLines="None" CellPadding="0" CellSpacing="0"  runat="server" 
                                        OnRowDataBound="grdFollowupEducation_RowDataBound" AutoGenerateColumns="False"
                                        Width="100%" AllowSorting="True" OnSelectedIndexChanging="grdFollowupEducation_SelectedIndexChanging"
                                        OnSorting="grdFollowupEducation_Sorting" OnRowDeleting="grdFollowupEducation_RowDeleting" CssClass="table table-bordered table-hover">
                                       <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:GridView>
                                </div>
                            </div>
             </div>
             <div class="row" align="center">
              <div class="col-md-12 col-sm-12 col-xs-12 form-group">
              <asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnsave_Click" CssClass="btn btn-primary"
                                Height="30px" Width="8%" Style="text-align: left;" />
                            <label class="glyphicon glyphicon-floppy-disk" style="vertical-align: middle; margin-left: -3%;
                                color: #fff; margin-top: .75%; margin-right: 2%;">
                            </label>
                            <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CssClass="btn btn-primary"
                                Width="8%" Height="30px" Style="text-align: left;" />
                            <label class="glyphicon glyphicon-remove-circle" style="margin-left: -3%; margin-right: 2%;
                                vertical-align: sub; color: #fff;">
                            </label>
                            <asp:Button ID="btnPrint" Text="Print" runat="server" OnClientClick="WindowPrint()"
                                CssClass="btn btn-primary" Width="8%" Height="30px" Style="text-align: left;" />
                            <label class="glyphicon glyphicon-print" style="margin-left: -3%; vertical-align: sub;
                                color: #fff;">
                            </label>
                            </div>
             </div>
	 <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>
     </div>
     </div>

    
    
</asp:Content>
