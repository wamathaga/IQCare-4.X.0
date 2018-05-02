<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    Inherits="frmScheduler_Appointmenthistory" Title="Untitled Page" CodeBehind="frmScheduler_Appointmenthistory.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <%--<link type="text/css" href="../Style/_assets/css/grid.css" rel="stylesheet" />
<link type="text/css" href="../Style/_assets/css/round.css" rel="stylesheet" />--%>
    <%-- <form id="appointmenthistory" method="post" runat="server" enableviewstate="true" title="patientAppointmentHistory">--%>
    <div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">Appointment</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content--%>
         <table width="100%" class="table-condensed">
                <tr>
                    <td class="border pad5 whitebg" align="left">
                        <asp:Button ID="btnNewAppointment" runat="server" Text="New Appointment" Enabled="True"
                            OnClick="btnNewAppointment_Click" CssClass="btn btn-primary" Height="30px" Width="16%"
                            Style="text-align: left;" />
                        <span class="glyphicon glyphicon-new-window" style="margin-left: -2.5%; vertical-align:sub; color:#fff;"></span>
                    </td>
                </tr>
                <tr>
                    <td class="border pad5 whitebg" valign="top">
                        <div class="grid">
                            <div class="rounded">
                                <div class="top-outer">
                                    <div class="top-inner">
                                        <div class="top">
                                            <center style="color:#fff;">
                                                <h2>
                                                    Appointments</h2>
                                            </center>
                                        </div>
                                    </div>
                                </div>
                                <div class="mid-outer">
                                    <div class="mid-inner">
                                        <div class="mid" style="cursor: pointer; overflow: auto; border: 1px solid #666699;">
                                            <div id="div-gridview" class="GridView whitebg" style="height: 300px">
                                                <asp:GridView ID="grdSearchResult" AllowSorting="True" runat="server" BorderWidth="0"
                                                    GridLines="None" CssClass="table table-bordered table-hover" CellPadding="0" CellSpacing="0" AutoGenerateColumns="False"
                                                    Width="100%" PageSize="1" OnSorting="grdSearchResult_Sorting" OnRowDataBound="grdSearchResult_RowDataBound"
                                                    OnSelectedIndexChanging="grdSearchResult_SelectedIndexChanging">
                                                     <HeaderStyle CssClass="searchresultfixedheader" Height="20px" VerticalAlign="Middle"
                                                HorizontalAlign="Left"></HeaderStyle>
                                                    
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
                    <td class="pad5 center" align="center">
                        <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn btn-primary"
                            Height="30px" Width="8%" Style="text-align: left;" />
                        <span class="glyphicon glyphicon-backward" style="margin-left: -3%; vertical-align: middle;
                            color: #fff; "></span>
                    </td>
                </tr>
            </table>
         </div>
      </div>
      </div>
      </div>
     </div>
     </div>
    
</asp:Content>
