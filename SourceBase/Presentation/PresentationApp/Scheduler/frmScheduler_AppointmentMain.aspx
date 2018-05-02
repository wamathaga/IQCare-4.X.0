<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    Inherits="frmScheduler_AppointmentMain" Title="Untitled Page" CodeBehind="frmScheduler_AppointmentMain.aspx.cs" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <%-- <link type="text/css" href="../Style/_assets/css/grid.css" rel="stylesheet" />
    <link type="text/css" href="../Style/_assets/css/round.css" rel="stylesheet" />--%>
    <%--<form id="appointment" method="post" runat="server" enableviewstate="true" title="patientAppointment">--%>
    <script type="text/javascript">
        function RegisterJQuery() {
            $('#txtFrom').datepicker({ autoclose: true });
            $('#txtTo').datepicker({ autoclose: true });
        }
        //Calling RegisterJQuery when document is ready (Page loaded first time)
        $(document).ready(RegisterJQuery);
        //Calling RegisterJQuery when the page is doing postback (asp.net)
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RegisterJQuery);
</script>
    <script language="javascript" type="text/javascript">

        //Modified 20June 2007 (5)
        function CheckDate(vDateName) {

            var mYear = vDateName.value.substr(7, 4)
            if (mYear != '') {
                if (mYear < 1930) {
                    alert("Selected year should be between 1930 and Current year. Reenter..");
                    vDateName.value = "";
                    vDateName.focus();

                }
            }
        }
        function fnredirect(url) {

            window.location.href = url;
        }
        //Modified 20June 2007 (5)
        function ResetDateAndGrid(YearVal, MonthVal, DayVal, previousFormName) {
            var BYearVal = YearVal;
            if (MonthVal = 1) {
                BYearVal = BYearVal - 1;
            }
            var ddlStatus = document.getElementById('<%=ddAppointmentStatus.ClientID %>')[document.getElementById('<%=ddAppointmentStatus.ClientID %>').selectedIndex].value;
            var fromDate = document.getElementById('<%= txtFrom.ClientID %>');
            var toDate = document.getElementById('<%= txtTo.ClientID %>');
            var grid = document.getElementById('<%=grdSearchResult.ClientID %>');
            
            if (DayVal < 10) {
                DayVal = "0" + DayVal;
            }
            var months = new Array(13);
            var currDate = new Date();

            months[0] = "Jan";
            months[1] = "Feb";
            months[2] = "Mar";
            months[3] = "Apr";
            months[4] = "May";
            months[5] = "Jun";
            months[6] = "Jul";
            months[7] = "Aug";
            months[8] = "Sep";
            months[9] = "Oct";
            months[10] = "Nov";
            months[11] = "Dec";

            MonthVal = MonthVal - 1;
            currDate = DayVal + "-" + months[MonthVal] + "-" + YearVal;
            if ((previousFormName == 'FacilityHomePending') && (ddlStatus == 12)) {

                //fromDate.value = currDate;
                //toDate.value = currDate;
                fromDate.value = "";
                toDate.value = "";

            }
            else if (ddlStatus == 15) {
                //Calculate the date of 1 month previous to current date

                newDate = new Date(YearVal, MonthVal, DayVal);

                var mnth = newDate.getMonth();
                var yr = newDate.getYear();

                var day = newDate.getDate();
                var daydiff = 0;

                if (DayVal > 30) {
                    day = DayVal - 30;
                }
                else {
                    daydiff = 30 - DayVal;
                    if (daydiff == 0) {
                        day = 1;
                    }
                    else {
                        if (mnth == 0) {
                            mnth = 11;
                            yr = yr - 1;
                        }
                        else {
                            mnth = mnth - 1;
                        }
                        daysinmonth = 32 - new Date(yr, mnth, 32).getDate();
                        day = daysinmonth - daydiff;
                    }
                }
                var monthname = months[mnth];
                var dt = new Date();

                if (day < 10) {
                    day = "0" + day;
                }

                dt = day + "-" + monthname + "-" + BYearVal;
                //fromDate.value = dt;
                //toDate.value = currDate;
                fromDate.value = "";
                toDate.value = "";

            }
            else {
                fromDate.value = " ";
                toDate.value = " ";
            }
            //grid.innerText = "";
        } 
              
        </script>
        <div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">Appointments</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
          <asp:UpdatePanel ID="up_appointment" runat="server">
                    <ContentTemplate>                    
                    <div class="row">
                    <br />
                  <div class="col-md-6 col-sm-12 col-xs-12 form-group text-right">
                    <asp:Button ID="btnExcel" runat="server"  OnClick="btnExcel_Click" Text="Export to Excel" Height="30px" Width="25%" CssClass="btn btn-primary" Style="color:White;text-align:left;"/>
                    <i class="fa fa-file-excel-o" style="margin-left: -4%; margin-right: 2%; vertical-align: sub; color: #fff; margin-top: 1%;"></i>
                    <%--<label class="glyphicon glyphicon-export" style="margin-left: -4%; margin-right: 2%; vertical-align: sub; color: #fff; margin-top: 1%;"></label>--%>
                </div>
    <div class="col-md-6 col-sm-12 col-xs-12 form-group">
    <asp:Button ID="btnNewAppointment" runat="server" OnClick="btnNewAppointment_Click1" Text="New Appointment" Height="30px" Width="25%" CssClass="btn btn-primary" Style="color:White;text-align:left;"/>
    <i class="glyphicon glyphicon-new-window" style="margin-left: -4%; margin-right: 2%; vertical-align: sub; color: #fff; margin-top: 1%;"></i>
    
    </div>
                </div>
                <div class="row">
                <asp:UpdateProgress runat="server" ID="updateProgress" AssociatedUpdatePanelID="up_appointment">
                                                    <ProgressTemplate>
                                                        Searching....<asp:Image runat="server" ID="imggif" ImageUrl="~/Images/loading.gif"
                                                            ImageAlign="AbsMiddle" />
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                </div>
                <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">Appointment Status:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddAppointmentStatus" runat="server" Width="140px" CssClass="form-control"></asp:DropDownList>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">Date Range From:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtFrom" clientidmode="Static"
                                                        maxlength="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>             
             </div> 
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">To:</label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtTo" clientidmode="Static"
                                                        maxlength="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
             </div>                       
    </div>
    <div class="row text-center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
  <asp:Button ID="btnSubmit" runat="server" Text="View" OnClick="btnSubmit_Click" Height="30px" Width="7%" CssClass="btn btn-primary" Style="color:White;text-align:left;"/>
  <label class="glyphicon glyphicon-eye-open" style="margin-left: -3%; margin-right: 2%; vertical-align: sub; color: #fff; margin-top: 1%;"></label>
<asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" Height="30px" Width="7%" CssClass="btn btn-primary" Style="color:White;text-align:left;"/>
<label class="glyphicon glyphicon-step-backward" style="margin-left: -3%; margin-right: 2%; vertical-align: sub; color: #fff; margin-top: 1%;"></label>
                                    <asp:HiddenField ID="hidappointment" runat="server" />
 </div>
 </div>
 <div class="row text-center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <div class="grid">
                                            <div class="rounded">
                                                <div class="top-outer">
                                                    <div class="top-inner">
                                                        <div class="top">
                                                            <h2>
                                                                Appointments</h2>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="mid-outer">
                                                    <div class="mid-inner">
                                                        <div class="mid" style="height: 300px; overflow: auto">
                                                            <div id="div-gridview" class="GridView whitebg">
                                                                <asp:GridView ID="grdSearchResult" AllowSorting="True" runat="server" CssClass="table table-bordered table-hover"
                                                                    CellPadding="0" CellSpacing="0" Width="100%" PageSize="1" AutoGenerateColumns="false"
                                                                    OnSorting="grdSearchResult_Sorting" BorderWidth="0" GridLines="None" OnRowDataBound="grdSearchResult_RowDataBound">
                                                                   <%-- <HeaderStyle HorizontalAlign="Left"></HeaderStyle>--%>
                                                                    <%--<AlternatingRowStyle BackColor="White" BorderColor="White" />--%>
                                                                    <%--<RowStyle CssClass="row" />--%>
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
 </div>
 </div>
                        
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSubmit" />
                        <asp:PostBackTrigger ControlID="btnBack" />
                        <asp:PostBackTrigger ControlID="btnExcel" />
                    </Triggers>
                </asp:UpdatePanel>
                <script language="javascript" type="text/javascript">
                    if (typeof (Sys) !== 'undefined')
                        Sys.Application.notifyScriptLoaded();
                    var pageManager = Sys.WebForms.PageRequestManager.getInstance();
                    var uiId = '';
                    pageManager.add_beginRequest(myBeginRequestCallback);
                    function myBeginRequestCallback(sender, args) {
                        var postbackElem = args.get_postBackElement();
                        uiId = postbackElem.id;
                        postbackElem.disabled = true;
                        document.getElementById('divApp').disabled = "disabled";

                    }
                    pageManager.add_endRequest(myEndRequestCallback);

                    function myEndRequestCallback(sender, args) {
                        var hidbox = $get('<%=hidappointment.ClientID %>');
                        document.getElementById('divApp').disabled = "";
                        if (hidbox.value == "No") {

                            document.getElementById('<%=txtFrom.ClientID %>').value = "";
                            document.getElementById('<%=txtTo.ClientID %>').value = "";
                            document.getElementById('<%=ddAppointmentStatus.ClientID %>').selectedIndex = 0;
                            alert("No Record Found");
                        }
                    }
                </script>
	 <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>
     </div>
     </div>

    <div style="padding-top: 20px;">
        
        <h1 class="nomargin" style="padding-left: 10px;">
            </h1>
        <div class="center" style="padding: 5px;">
            <div id="divApp" class="border center formbg">
               
            </div>
        </div>
        <br />
    </div>
</asp:Content>
