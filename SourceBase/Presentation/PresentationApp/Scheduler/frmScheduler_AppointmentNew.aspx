<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    Inherits="frmScheduler_AppointmentNew" Title="Untitled Page" CodeBehind="frmScheduler_AppointmentNew.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <!--Modified 20June 2007 (6)-->
        <script language="javascript" type="text/javascript">
            var selDate = "";
            function CheckDate(vDateName) {                        
                var mYear = vDateName.value.substr(7, 4)
                if (mYear != '') {
                    if (mYear < 1930) {
                        alert("Selected year should be between 1930 and Current year. Reenter..");
                        vDateName.value = "";
                        vDateName.focus();
                    }
                    document.getElementById('txtAppDate').value = selDate;
                }     
            }
            function getFormattedDate(input) {
                var pattern = /(.*?)\/(.*?)\/(.*?)$/;
                var result = input.replace(pattern, function (match, p1, p2, p3) {
                    var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
                    return (p2 < 10 ? "0" + p2 : p2) + "-" + months[(p1 - 1)] + "-" + p3;
                });
                //alert(result);
                return result;
            }
            function noOfDays(days, newDate) {
                var daysValue = document.getElementById(days).value;
                //Bug ID 3279...added a new condition, if day value is less then 0.........
                if (daysValue < 0) {
                    alert("You can't book previous days appointment..");
                    document.getElementById(days).value = '';
                    document.getElementById(newDate).value = '';
                    document.getElementById(days).focus();
                    return;
                }
                // End Bug D 3279..........
                var someDate = new Date();
                var numberOfDaysToAdd = 0;
                if (daysValue == "") {
                    numberOfDaysToAdd = 0;
                }
                else {
                    numberOfDaysToAdd = parseInt(daysValue);
                }

                someDate.setDate(someDate.getDate() + numberOfDaysToAdd);

                var dd = someDate.getDate();
                var mm = someDate.getMonth() + 1;
                var y = someDate.getFullYear();

                //weekends
                var dayOfWeek = someDate.getDay();
                var isWeekend = (dayOfWeek == 6) || (dayOfWeek == 0);    // 6 = Saturday, 0 = Sunday
                if (isWeekend) {
                    var ans = confirm('Selected day is a weekend. Move date to following Monday?');
                    if (ans == true) {
                        if (dayOfWeek == 6) {
                            dd += 2;
                        }

                        if (dayOfWeek == 0) {
                            dd += 1;
                        }
                    }
                }
                var noofdaysinmonth = daysInMonth(mm, y);
                if (dd > noofdaysinmonth) {
                    dd = noofdaysinmonth - dd;
                    mm = mm + 1;
                }
                var someFormattedDate = new Date(mm + '/' + Math.abs(dd) + '/' + y);

                var curr_date = someFormattedDate.getDate();
                var curr_month = someFormattedDate.getMonth() + 1;
                var curr_year = someFormattedDate.getFullYear();
                var someFormattedDate1 = curr_month + "/" + curr_date + "/" + curr_year;
                document.getElementById(newDate).value = getFormattedDate(someFormattedDate1.toString());
                document.getElementById('txtAppDate').focus();
                selDate = document.getElementById('txtAppDate').value;
                //alert('selDate ' + selDate);  
            }
            function daysInMonth(month, year) {
                return new Date(year, month, 0).getDate();
            }
            function TEST() {
                //alert('selDate ' + selDate);
                if (selDate != '') {
                    document.getElementById('txtAppDate').value = selDate;
                    selDate = '';
                }
            }
        </script>
        <!--Modified 20June 2007 (6)-->
        <script type="text/javascript">
            function RegisterJQuery() {
                $('#txtAppDate').datepicker({ autoclose: true });
            }
            //Calling RegisterJQuery when document is ready (Page loaded first time)
            $(document).ready(RegisterJQuery);
            //Calling RegisterJQuery when the page is doing postback (asp.net)
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RegisterJQuery);
</script>
        <div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title" id="header" runat="server">New Appointment</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
	<div class="row" align="center">
         <br />           
        </div>
        <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group text-nowrap">
 <div class="row">
  <div class="col-md-4"><label class="control-label required">Appointment Date:</label></div>
    <div class="col-md-2"><asp:TextBox ID="txtNoOfDays" runat="server" Width="50px" CssClass="form-control"></asp:TextBox></div>
      <div class="col-md-1"> <label  class="control-label">Days</label></div>
 </div>
             
             
             </div>
                   
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtAppDate" clientidmode="Static"
                                                       maxlength="11" size="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;" onblur="CheckDate(this);TEST();"/>
                                                </div>
                                            </div>
             </div> 
               <div class="col-md-1 col-sm-12 col-xs-12 form-group text-right">
             <label  class="control-label required">Purpose:</label>
             </div>
               <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddAppPurpose" runat="server" AutoPostBack="false" Width="98%" CssClass="form-control"> </asp:DropDownList>
             </div>
               <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <label  class="control-label required">Provider:</label>
             </div>
               <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddAppProvider" runat="server" AutoPostBack="false"
                                Width="98%" CssClass="form-control">
                            </asp:DropDownList>
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
             </div>
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
<asp:Button ID="btnSubmit" runat="server" Text="Save" OnClick="btnSubmit_Click" Enabled="True" CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align:left;"/>
<label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
<asp:Button ID="btndelete" runat="server" Text="Delete" OnClick="btndelete_Click" Visible="false" CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align:left;"/>
<asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" Enabled="True" CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align:left;"/>
<label class="glyphicon glyphicon-step-backward" style="margin-left: -3%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
<asp:Button ID="theBtn" Text="OK" runat="server" OnClick="theBtn_Click" CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align:left;"/>
<label class="glyphicon glyphicon-ok" style="margin-left: -3%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
             </div>
             
 </div>
  <div class="row" align="center">
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
                                        <div class="mid" style="cursor: pointer; height: 280px; overflow: auto; border: 1px solid #666699;">
                                            <div id="div-gridview" class="GridView whitebg">
                                                <asp:GridView ID="grdSearchResult" AllowSorting="True" runat="server" CssClass="table table-bordered table-hover"
                                                    CellPadding="0" CellSpacing="0" AutoGenerateColumns="False" Width="100%" PageSize="1"
                                                    BorderWidth="0" GridLines="None" OnSorting="grdSearchResult_Sorting" OnRowDataBound="grdSearchResult_RowDataBound"
                                                    OnSelectedIndexChanging="grdSearchResult_SelectedIndexChanging">
                                                  
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
	 <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>
     </div>
     </div>
    
    <script language="javascript" type="text/javascript">
    </script>
</asp:Content>
