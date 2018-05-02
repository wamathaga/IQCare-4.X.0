<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    Inherits="frmAddTechnicalArea" CodeBehind="frmAddTechnicalArea.aspx.cs" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">

    <script type="text/javascript">
        function RegisterJQuery() {
            $('#txtenrollmentDate').datepicker({ autoclose: true });
            $('#txtReEnrollmentDate').datepicker({ autoclose: true });
        }
        //Calling RegisterJQuery when document is ready (Page loaded first time)
        $(document).ready(RegisterJQuery);
        //Calling RegisterJQuery when the page is doing postback (asp.net)
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RegisterJQuery);
    </script>
    <script language="javascript" type="text/javascript">
        function fnChange() {

        }
        function fncheck() {
            //var id = document.getElementById('ctl00_facilityheaderfooter_ddlTecharea').value;
            var id = document.getElementById('ctl00_IQCareContentPlaceHolder_ddlTecharea').value;
            if (id == "0") {
                alert("Select Service");
                return false;
            }

            var ptype = document.getElementById('ddlPatientType').value;
            if (ptype == "0") {
                alert("Select Patient Type");
                return false;
            }
        }
    </script>
    <style>
        .outer
        {
            width: 100%; /* Firefox */
            display: -moz-box;
            -moz-box-pack: center;
            -moz-box-align: center; /* Safari and Chrome */
            display: -webkit-box;
            -webkit-box-pack: center;
            -webkit-box-align: center; /* W3C */
            display: box;
            box-pack: center;
            box-align: center;
        }
        .inner
        {
            width: 50%;
        }
    </style>
    <!-- Main content -->
    <section class="content">
      <!-- Default box -->
      <div>
        <div class="box-body">
                <div class="row outer">
        <div class="col-xs-9">
          <div class="box box-primary box-solid">
            <div class="box-header with-border">
              <h3 class="box-title" id="tHeading" runat="server">
                    Select Service</h3>
              <!-- /.box-tools -->
            </div>
            <!-- /.box-header -->
            <div class="box-body">
            <div class="row">
                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        Patient Name:</label>
                                    <asp:Label ID="lblname" runat="server" class="control-label" style="display:block;"></asp:Label>
                                </div>
                                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        Sex:</label>
                                   <asp:Label ID="lblsex" runat="server" class="control-label" style="display:block;"></asp:Label>
                                </div>
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        DOB:</label>
                                    <asp:Label ID="lbldob" runat="server" class="control-label" style="display:block;"></asp:Label>
                                </div>
                                <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        IQCare Reference Number:</label>
                                    <asp:Label ID="lblIQno" runat="server" class="control-label" style="display:block;"></asp:Label>
                                </div>
                                </div>
                                <div class="row">
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                &nbsp;
                                </div>
                                <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                <label class="control-label">
                                                    Service:</label>
                                                     <asp:DropDownList CssClass="form-control" ID="ddlTecharea" onchange="fnChange();" runat="server" Style="z-index: 2"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlTecharea_SelectedIndexChanged">
                                                </asp:DropDownList>
                                </div>
                                 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                 &nbsp;
                                </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                        &nbsp;
                                    </div>
                                    <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                        <label class="required" id="Label2" runat="server">
                                            Patient Type:</label>
                                            
                                        <asp:DropDownList CssClass="form-control" ID="ddlPatientType" ClientIDMode="Static" runat="server" Style="z-index: 2"
                                                          AutoPostBack="False" >
                                        
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                        &nbsp;
                                    </div>
                                </div>

                                <div class="row">
                                 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                &nbsp;
                                </div>
                                <div class="col-md-7 col-sm-12 col-xs-12 form-group">
                                <label for="pharmReportedbyDate" class="required" id="lblEnrollment" runat="server">
                                                    Enrollment Date:</label>

                                                    
                                            <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtenrollmentDate" clientidmode="Static" 
                                                        runat="server" data-date-format="dd-M-yyyy" style="width:200px;"/>
                                                </div>
                                            </div>
                                        

                                                    <%-- <input id="" onblur="DateFormat(this,this.value,event,false,'3')"
                                                    onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"
                                                    maxlength="11" size="11" name="pharmReportedbyDate" runat="server" />
                                                <img id="appDateimg2" height="22" runat="server" alt="Date Helper" hspace="5" src="images/cal_icon.gif"
                                                    width="22" border="0" name="appDateimg" visible="true" /><span class="smallerlabel"
                                                        id="appDatespan2">(DD-MMM-YYYY)</span>--%> &nbsp;
                                                <asp:Button ID="btnReEnollPatient" runat="server" Text="Re-Enroll Patient" OnClick="btnReEnollPatient_Click"
                                                    CssClass="btn btn-primary" Width="32.5%" Height="30px" Style="text-align: left;" />
                                                <span class="glyphicon glyphicon-check" style="vertical-align: sub; margin-left: -5%;
                                                    color: #fff"></span>
                                </div>
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                 &nbsp;
                                </div>
                                </div>
                                <table width="100%" border="0">
                                 <tr id="trReEnroll" runat="server">
                                 <td>
                                <div class="row">
                                 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                &nbsp;
                                </div>
                                <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                               
                               <label for="ReEnrollDate" class="required">
                                                    Re-Enrollment Date:</label>

                                                     <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtReEnrollmentDate" clientidmode="Static" 
                                                        runat="server" data-date-format="dd-M-yyyy" style="width:200px;"/>
                                                </div>
                                            </div>
                                                    <%--<input id="txtReEnrollmentDate" onblur="DateFormat(this,this.value,event,false,'3')"
                                                    onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"
                                                    maxlength="11" size="11" name="ReEnrollDate" runat="server" />
                                                <img id="imgDtReEnroll" height="22" runat="server" alt="Date Helper" hspace="5" src="images/cal_icon.gif"
                                                    width="22" border="0" name="appDateimg" visible="true" /><span class="smallerlabel"
                                                        id="Span1">(DD-MMM-YYYY)</span>--%>
                                                        
                                </div>
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                 &nbsp;
                                </div>
                                </div>
                                </td></tr>
                                                        </table>
                                <div class="row">
                                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                &nbsp;
                                </div>
                                <div class="col-md-10 col-sm-12 col-xs-12 form-group">
                                <asp:Panel ID="pnlIdentFields" runat="server" EnableViewState="true">
                                    </asp:Panel>
                                </div>
                                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                &nbsp;
                                </div>

                                </div>
            </div>
            <!-- /.box-body -->
            <!-- Button Footer -->
<div class="row no-print" align="center" style="padding-bottom:15px;">
    <div class="form-group">
        <div class="col-md-12" align="center">
            <asp:Button ID="btnSaveContinue" runat="server" Text="Save and Continue" OnClientClick="return fncheck();"
                                        OnClick="btnSaveContinue_Click" CssClass="btn btn-primary" Height="30px" Width="18%"
                                        Style="text-align: left;" />
                                    <label class="glyphicon glyphicon-floppy-disk" style="vertical-align: middle; margin-left: -3%;
                                        color: #fff; margin-top: .75%; margin-right: 2%;">
                                    </label>
                                    <asp:Button ID="btnContinue" runat="server" Text="Continue" OnClick="btnContinue_Click"
                                        CssClass="btn btn-primary" Height="30px" Width="11%" Style="text-align: left;" />
                                    <asp:Label ID="lblContinue" runat="server" class="glyphicon glyphicon-list" Style="margin-left: -3%;
                                        vertical-align: sub; color: #fff;">
                                    </asp:Label>
        </div>
    </div>
</div>
          </div>
          <!-- /.box -->
        </div>
      </div>
        </div>
        <!-- /.box-body -->
    </div>
    <!-- /.box -->
    
    </section>
</asp:Content>
