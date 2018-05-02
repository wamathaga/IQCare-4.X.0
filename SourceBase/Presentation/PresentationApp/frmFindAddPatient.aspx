<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    Inherits="frmFindAddPatient" Title="Untitled Page" EnableViewState="true" CodeBehind="frmFindAddPatient.aspx.cs" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
<style type="text/css">
    .table-hover tbody tr:hover td, .table-hover tbody tr:hover th {
   cursor:pointer;
}
</style>
<script type="text/javascript">
    function RegisterJQuery() {
        $('#txtDOB').datepicker({ autoclose: true });
    }
    //Calling RegisterJQuery when document is ready (Page loaded first time)
    $(document).ready(RegisterJQuery);
    //Calling RegisterJQuery when the page is doing postback (asp.net)
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RegisterJQuery);
</script>
    <script language="javascript" type="text/javascript">
        function fnSetSession(url) {
            var result = frmFindAddPatient.SetPatientId_Session(url).value;
            window.location.href = result;
        }
        function fnSetSessionfamily(url) {
            var result = frmFindAddPatient.SetPatientIdFamily_Session(url).value;
            window.location.href = result;
        }

        function MakeStaticHeader(gridId, height, width, headerHeight, isFooter) {
            var tbl = document.getElementById(gridId);
            if (tbl) {
                var DivHR = document.getElementById('DivHeaderRow');
                var DivMC = document.getElementById('DivMainContent');
                var DivFR = document.getElementById('DivFooterRow');

                //*** Set divheaderRow Properties ****
                DivHR.style.height = headerHeight + 'px';
                DivHR.style.width = (parseInt(width) - 16) + 'px';
                DivHR.style.position = 'relative';
                DivHR.style.top = '0px';
                DivHR.style.zIndex = '10';
                DivHR.style.verticalAlign = 'top';

                //*** Set divMainContent Properties ****
                DivMC.style.width = width + 'px';
                DivMC.style.height = height + 'px';
                DivMC.style.position = 'relative';
                DivMC.style.top = -headerHeight + 'px';
                DivMC.style.zIndex = '1';

                //*** Set divFooterRow Properties ****
                DivFR.style.width = (parseInt(width) - 16) + 'px';
                DivFR.style.position = 'relative';
                DivFR.style.top = -headerHeight + 'px';
                DivFR.style.verticalAlign = 'top';
                DivFR.style.paddingtop = '2px';

                if (isFooter) {
                    var tblfr = tbl.cloneNode(true);
                    tblfr.removeChild(tblfr.getElementsByTagName('tbody')[0]);
                    var tblBody = document.createElement('tbody');
                    tblfr.style.width = '100%';
                    tblfr.cellSpacing = "0";

                    tblfr.border = "0px";
                    tblfr.rules = "none";
                    //*****In the case of Footer Row *******
                    tblBody.appendChild(tbl.rows[tbl.rows.length - 1]);
                    tblfr.appendChild(tblBody);
                    DivFR.appendChild(tblfr);
                }
                //****Copy Header in divHeaderRow****
                DivHR.appendChild(tbl.cloneNode(true));
            }
        }



        function OnScrollDiv(Scrollablediv) {

            //document.getElementById('DivHeaderRow').scrollLeft = Scrollablediv.scrollLeft;
            //document.getElementById('DivFooterRow').scrollLeft = Scrollablediv.scrollLeft;
        }
    </script>
    <div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title" id="tHeading" runat="server">Find/Add Patient</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
         <div id="divshow" class="border center formbg">
            <asp:UpdatePanel ID="up_patient" runat="server">
                <ContentTemplate>
                    <!-- Begin Stacey's code -->
                    <div class="row">
                    <br />
                   <div class="col-xs-7">
                   <%--left--%>
                   <div class="row">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Identifier:</label>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlIdentifier" runat="server" CssClass="form-control" Width="300" AutoPostBack="True" OnSelectedIndexChanged="ddlIdentifier_SelectedIndexChanged">
                                            </asp:DropDownList>
       </div>                                  
    </div>
    <div class="row">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
 <label id="lblidentificationno" class="control-label" runat="server">Identification Number:</label>
 </div>
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
 <asp:TextBox ID="txtidentificationno" runat="server" CssClass="form-control" Width="300"></asp:TextBox>
 </div>
 </div>
 <div class="row">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
  <label class="control-label">Facility/Satellite:</label>
 </div>
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
 <asp:DropDownList ID="ddFacility" runat="server" CssClass="form-control" Width="300"></asp:DropDownList>
 </div>
 </div>
 <div class="row">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
 <label class="control-label required">Service:</label>
 </div>
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
 <asp:DropDownList ID="ddlServices" runat="server" CssClass="form-control" Width="300"></asp:DropDownList>
 </div>
 </div>
                   </div>
                    <div class="col-xs-5">
                    <%--right--%>
                    <div class="row">
 <div class="col-md-5 col-sm-12 col-xs-12 form-group">
 <label for="lastname" class="control-label required">Last Name:</label>
 </div>
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
 <asp:TextBox ID="txtlastname" runat="server" CssClass="form-control" Width="250"></asp:TextBox>
 </div>
 </div>
 <div class="row">
 <div class="col-md-5 col-sm-12 col-xs-12 form-group">
 <label runat="server" id="lblmiddlename" for="middlename" class="control-label">Middle Name:</label>
 </div>
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
 <asp:TextBox ID="txtmiddlename" runat="server" CssClass="form-control" Width="250"></asp:TextBox>
 </div>
 </div>
 <div class="row">
 <div class="col-md-5 col-sm-12 col-xs-12 form-group">
 <label for="firstname" class="control-label">First Name:</label>
 </div>
  <div class="col-md-6 col-sm-12 col-xs-12 form-group">
  <asp:TextBox ID="txtfirstname" runat="server" CssClass="form-control" Width="250"></asp:TextBox>
 </div>
 </div>
 <div class="row" id="tdPatientOtherDetails" runat="server">
 <div class="row">
 <div class="col-md-5 col-sm-12 col-xs-12 form-group">
 <label for="DOB" class="control-label">Date of Birth:</label>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtDOB" clientidmode="Static"
                                                       maxlength="11" runat="server" data-date-format="dd-M-yyyy" style="width:110px;"/>
                                                </div>
                                            </div>

             
             </div>                                
    </div>
    <div class="row">
 <div class="col-md-5 col-sm-12 col-xs-12 form-group">
             <label for="Sex" class="control-label">Sex:</label>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddSex" runat="server" CssClass="form-control" Width="170">
                                                <asp:ListItem Selected="True" Value="">-Select-</asp:ListItem>
                                                <asp:ListItem Value="16">Male</asp:ListItem>
                                                <asp:ListItem Value="17">Female</asp:ListItem>
                                            </asp:DropDownList>
             </div>                                
    </div>
    <div class="row">
 <div class="col-md-5 col-sm-12 col-xs-12 form-group">
             <label for="Careendedstatus" class="control-label">Patient Status:</label>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddCareEndedStatus" runat="server" CssClass="form-control" Width="170">
                                                <asp:ListItem Selected="True" Value="">-Select-</asp:ListItem>
                                                <asp:ListItem Value="0">Active</asp:ListItem>
                                                <asp:ListItem Value="1">Care Ended</asp:ListItem>
                                            </asp:DropDownList>
             </div>                                
    </div>
 </div>

                   </div>
                    </div>                    
                    <!-- End Stacey's code -->
                    <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <asp:Button ID="btnView" runat="server" OnClick="btnView_Click" Text=" Find " CssClass="btn btn-primary"
                                                    Height="30px" Width="6%" Style="text-align: left;margin-right:20px;" />
                                                    <span class="glyphicon glyphicon-search"
                                                        style="margin-left: -4%; vertical-align: middle; color: #fff;margin-right:1%"></span>

            <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add Patient"
                                                    CssClass="btn btn-primary" Height="30px" Width="10%" Style="text-align: left;
                                                    margin-left: 5%;" /><span id="spanBtnAdd" runat="server" class="glyphicon glyphicon-plus"
                                                        style="margin-left: -3%; vertical-align: middle; color: #fff;margin-right:5%"></span>
            <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Back" CssClass="btn btn-primary"
                                                    Height="30px" Width="6%" Style="text-align: left;" />
                                                <span class="glyphicon glyphicon-backward" style="margin-left: -2%; vertical-align: middle;
                                                    color: #fff;margin-right:1%"></span>


             </div>
              </div>
              <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <div class="grid" style="padding-right:5px;">
                                        <div class="rounded">
                                            <div class="top-outer">
                                                <div class="top-inner">
                                                    <div class="top">
                                                        <h2>
                                                            Searched Result</h2>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="mid-outer">
                                                <div class="mid-inner">
                                                    <div class="mid" style="cursor: pointer; height: 420px; overflow: auto; border: 1px solid #666699;">
                                                        <%--<div style="overflow: hidden;" id="DivHeaderRow"> comment by deepak(New code for fixed header)
                                                        </div>--%>
                                                        <div id="div-gridview" class="whitebg">
                                                            <!-- Content Goes Here! -->
                                                            <asp:GridView ID="grdSearchResult" runat="server" EnableViewState="False" Width="100%"
                                                                OnRowDataBound="grdSearchResult_RowDataBound" OnSorting="grdSearchResult_Sorting"
                                                                PageSize="1" CssClass="table table-bordered table-hover" AutoGenerateColumns="False" CellPadding="0"
                                                                CellSpacing="0" BorderWidth="0" GridLines="None" AllowSorting="True">                                                               
                                                            </asp:GridView>
                                                        </div>
                                                        <%--<div id="DivFooterRow" style="overflow: hidden">
                                                        </div>--%>
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
 </div>
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
  <input id="rdoExact" class="textstylehidden" onmouseup="up(this);" onfocus="up(this);"
                                        onclick="down(this);" type="radio" value="0" name="dobPrecision" runat="Server" />
                                    <input id="rdoEstimated" class="textstylehidden" onmouseup="up(this);" onfocus="up(this);"
                                        onclick="down(this);" type="radio" value="1" name="dobPrecision" runat="Server" />
 </div>
 </div>

                    
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnView"></asp:PostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="grdSearchResult"></asp:AsyncPostBackTrigger>
                    <%--<asp:PostBackTrigger ControlID="btnAdd"></asp:PostBackTrigger>--%>
                    <asp:PostBackTrigger ControlID="btnCancel"></asp:PostBackTrigger>
                </Triggers>
            </asp:UpdatePanel>
        </div>
	 <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>
     </div>
     </div>

    
</asp:Content>
