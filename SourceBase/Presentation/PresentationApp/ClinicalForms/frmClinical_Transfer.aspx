<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    Inherits="ClinicalForms_frmClinical_Transfer" CodeBehind="frmClinical_Transfer.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
<script type="text/javascript">
    function RegisterJQuery() {
        $('#txtTransferDate').datepicker({ autoclose: true });
    }
    //Calling RegisterJQuery when document is ready (Page loaded first time)
    $(document).ready(RegisterJQuery);
    //Calling RegisterJQuery when the page is doing postback (asp.net)
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RegisterJQuery);
</script>
    <script language="javascript" type="text/javascript">
        function WindowPrint() {
            window.print();
        }       
    </script>
    <div class="content-wrapper">
    <div class="box-body">
    <div class="row">
    <div class="col-xs-12">
     <div class="box box-primary box-solid">
      <div class="box-header">
        <h3 class="box-title">Patient Home</h3>
       </div>
       <!-- /.box-header -->
       <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
       <table width="100%" class="table-condensed">
                <tbody>
                    <tr id="tradd" runat="server">
                        <td class="pad5 whitebg border" style="width: 65%">
                            <table width="100%" class="table-condensed">
                                <tr>
                                    <td>
                                        <label id="lblLocation" for="Location">
                                            Location:
                                        </label>
                                    </td>
                                    <td>
                                        <span id="Current" class="patientInfo">Current</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLocationName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td>
                                        <span id="New" class="required">New</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddSatellite" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="pad5 whitebg border" style="width: 35%">                           
                                <div class="input-group date">               
                  <div style="width: 100%; margin: 0px auto;margin:10px;">
                  <div style="float: left;margin-top:5px;">Transfer Date:</div>
<div style="float: left">
<div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtTransferDate" clientidmode="Static"
                                                        runat="server" data-date-format="dd-M-yyyy" style="width:200px;"/>
                                                </div>
                                            </div>

</div>
</div> </div>
                                <%--New Date code End--%>
                            

                            
                            
                        </td>
                    </tr>
                    <tr id="tredit" runat="server">
                        <td class="pad5 whitebg border">
                            <label id="lblLocationEdit" for="Location">
                                Location: <span id="CurrentEdit" class="smallerlabel" style="font-weight: bold">Current
                                </span>
                                <asp:TextBox ID="txtLocationNameEdit" runat="server" CssClass="form-control"></asp:TextBox>
                            </label>
                            <span id="From" class="" style="font-weight: bold">From </span>
                            <asp:TextBox ID="txtFromSatellite" runat="server" CssClass="form-control"></asp:TextBox>
                            <span id="To" class="required" style="font-weight: bold">To </span>
                            <asp:DropDownList ID="ddSatelliteEdit" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </td>
                        <td class="pad5 whitebg border">
                            <label id="LblTransDate" class="required" for="TransferDate">
                                Transfer Date:</label>
                            <asp:TextBox ID="TxtTransDateEdit" MaxLength="11" runat="server" Width="30%" CssClass="form-control"></asp:TextBox>
                            <img onclick="w_displayDatePicker('<%= TxtTransDateEdit.ClientID %>');" alt="Date Helper"
                                height="22" hspace="3" src="../images/cal_icon.gif" width="22" border="0" />
                            <span class="smallerlabel">(DD-MMM-YYYY)</span>
                        </td>
                    </tr>
                    <tr>
                        <td class="center" colspan="2">
                            <div class="grid">
                                <div class="rounded">
                                    <div class="top-outer">
                                        <div class="top-inner">
                                            <div class="top">
                                                <h2>
                                                    Transfers</h2>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="mid-outer">
                                        <div class="mid-inner">
                                            <div class="mid" style="cursor: pointer; height: 280px; border: 1px solid #666699;">
                                                <div id="div-gridview" class="gridview whitebg">
                                                    <asp:GridView ID="GrdTransfer" runat="server" AutoGenerateColumns="False" AllowSorting="true"
                                                        Width="100%" PageIndex="1" HorizontalAlign="Left" BorderWidth="0" GridLines="None"
                                                        CssClass="table table-bordered table-hover" CellPadding="0" CellSpacing="0" BackColor="White" OnRowDataBound="GrdTransfer_RowDataBound"
                                                        OnSelectedIndexChanging="GrdTransfer_SelectedIndexChanging" OnSorting="GrdTransfer_Sorting">
                                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>                                                      
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="bottom-outer">
                                        <div class="bottom-inner">
                                            <div class="bottom">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="pad5 center" colspan="2">
                        <div class="row text-center">
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-primary"
                                Height="30px" Width="8%" Style="text-align: left;" />
                            <label class="glyphicon glyphicon-floppy-disk" style="vertical-align: middle; margin-left: -3%;
                                color: #fff; margin-top: .75%; margin-right: 2%;">
                            </label>
                            <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CssClass="btn btn-primary"
                                Height="30px" Width="8%" Style="text-align: left;" />
                            <label class="glyphicon glyphicon-remove-circle" style="margin-left: -3%; margin-right: 2%;
                                vertical-align: sub; color: #fff;">
                            </label>
                            <asp:Button ID="theBtn" Text="OK" runat="server" Height="30px" Width="8%" Style="text-align: left;" OnClick="theBtn_Click" CssClass="btn btn-primary" />
                            <label class="glyphicon glyphicon-ok" style="margin-left: -3%; vertical-align: sub;
                                color: #fff;">
                            </label>

                            <asp:Button ID="btnPrint" Text="Print" runat="server" OnClientClick="WindowPrint()"
                                CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;margin-left:25px;" />
                            <label class="glyphicon glyphicon-print" style="margin-left: -3%; vertical-align: sub;
                                color: #fff;">
                            </label>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
       </div>

     </div>
     </div>
    </div>
    </div>
    </div>
    
</asp:Content>
