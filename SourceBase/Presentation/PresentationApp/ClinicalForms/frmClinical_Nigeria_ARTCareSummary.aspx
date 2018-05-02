<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    CodeBehind="frmClinical_Nigeria_ARTCareSummary.aspx.cs" Inherits="PresentationApp.ClinicalForms.frmClinical_Nigeria_ARTCareSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
<script type="text/javascript">
    function RegisterJQuery() {
        $('#txtotherRegimendate').datepicker({ autoclose: true });
        $('#txtthisRegimendate').datepicker({ autoclose: true });  
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
        function GetControl() {
            document.forms[0].submit();
        }
    </script>
    <div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">ART Care Summary Form</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
         <div class="row box-header">
         <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <h5 class="box-title">Cohort</h5>
         </div>             
            </div>
            <div class="row">
 <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
             <label class="control-label">Cohort Month:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <input id="txtcohortmnth" size="10" name="CohortMonth" runat="server" readonly="readonly" style="width:99%" class="form-control"/>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
             <label class="control-label">Cohort Year:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <input id="txtcohortyear" size="10" name="CohortYear" runat="server" readonly="readonly" style="width:99%" class="form-control" />
             </div> 
 </div>
 <div class="row box-header">
         <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <h5 class="box-title">ART Start at Another Facility(Status at Start of ART)</h5>
         </div>             
            </div>
            <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label" id="lblotherregimen" runat="server">First ART Regimen:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group text-nowrap">
             <div class="row">
             <div class="col-md-7">
             <input id="txtotherregimen" size="16" name="otherregimen" readonly="readonly" runat="server" style="width:99%" class="form-control"/>
             </div>
              <div class="col-md-1" style="margin-left:-32px;">
              <asp:Button ID="btnotherRegimen" runat="server" Text="..." OnClick="btnTransRegimen_Click" CssClass="btn btn-primary" />
              </div>
             </div>
             
             
             </div>
            
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label  class="control-label" id="lblrARTdate" runat="server">Date ART Started:</label>
             </div> 
              <div class="col-md-2 col-sm-12 col-xs-12 form-group">
              <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtotherRegimendate" clientidmode="Static"
                                                       maxlength="11" size="10" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>             
             </div>
              <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
             <label  class="control-label">Clinical Stage:</label>
             </div>
              <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlotherFacilityClinicalStage" runat="server" Width="99%" CssClass="form-control"></asp:DropDownList>
             </div>
 </div>
  <div class="row">
  <div class="col-md-2 col-sm-12 col-xs-12 form-group">
  <div class="row">
              <div class="col-md-4">
              <label id="lblotherweight" runat="server" class="control-label">Weight:</label>
            </div>
            <div class="col-md-5">
            <input id="txtotherwght" size="6" maxlength="3" name="otherwght" runat="server" style="width:99%" class="form-control"/>            
            </div>
            <div class="col-md-2">
             <span class="smallerlabel">kg</span>
            </div>
            </div>
  </div>
  <div class="col-md-2 col-sm-12 col-xs-12 form-group">
  <div class="row">
              <div class="col-md-4">
              <label id="lblotherHeight" runat="server" class="control-label">Height:</label>
            </div>
             <div class="col-md-5">
             <input id="txtotherheight" size="6" maxlength="3" name="otherheight" runat="server" style="width:99%" class="form-control"/>             
            </div>
             <div class="col-md-2">
             <span class="smallerlabel">cm</span>
            </div>
            </div>
  </div>
  <div class="col-md-1 col-sm-12 col-xs-12 form-group">
   <label class="control-label">Function :</label>
  </div>
  <div class="col-md-2 col-sm-12 col-xs-12 form-group">
  <asp:DropDownList ID="ddlotherFunction" runat="server" Width="99%" CssClass="form-control"></asp:DropDownList>
  </div>
  <div class="col-md-1 col-sm-12 col-xs-12 form-group">
   <label class="control-label" id="lblotherCD4" runat="server">CD4 :</label>
  </div>
  <div class="col-md-1 col-sm-12 col-xs-12 form-group">
   <input id="txtotherCD4" size="6" name="otherCD4" runat="server" style="width:99%" class="form-control"/>
  </div>
  <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
   <label class="control-label" id="lblotherCD4Percent" runat="server">CD4 Percent :</label>
  </div>
  <div class="col-md-2 col-sm-12 col-xs-12 form-group">
  <input id="txtotherCD4Percent" size="6" name="othercd4percent" runat="server" style="width:99%" class="form-control"/>
  </div>

  </div>
  <div class="row box-header">
         <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <h5 class="box-title">ART Start at This Facility(Status of Start of ART)</h5>
         </div>             
            </div>
<div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label" id="Label1" runat="server">First ART Regimen:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group text-nowrap">
             <div class="row">
             <div class="col-md-7">
             <input id="txtthisregimen" size="16" name="thisregimen" readonly="readonly" runat="server" style="width:99%" class="form-control"/>
             
             </div>
              <div class="col-md-1" style="margin-left:-32px;">
              <asp:Button ID="btnthisRegimen" runat="server" Text="..." Enabled="False" CssClass="btn btn-primary" />
              </div>
             </div>
             
             
             </div>
            
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label  class="control-label" id="lblthisregi" runat="server">Date ART Started:</label>
             </div> 
              <div class="col-md-2 col-sm-12 col-xs-12 form-group">
              <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtthisRegimendate" clientidmode="Static"
                                                       maxlength="11" size="10" name="txtthisRegimendate" readonly="readonly" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>   
                                                   
             </div>
              <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
             <label  class="control-label">Clinical Stage:</label>
             </div>
              <div class="col-md-2 col-sm-12 col-xs-12 form-group">
              <asp:DropDownList ID="ddlthisfacilityClinicalStage" runat="server"  Width="99%" CssClass="form-control" Enabled="false"></asp:DropDownList>
             
             </div>
 </div>
 <div class="row">
  <div class="col-md-2 col-sm-12 col-xs-12 form-group">
  <div class="row">
              <div class="col-md-4">
              <label id="Label2" runat="server" class="control-label">Weight:</label>
            </div>
            <div class="col-md-5">
            <input id="txtthiswght" size="6" name="thisweight" runat="server" readonly="readonly" style="width:99%" class="form-control"/>             
            </div>
            <div class="col-md-2">
             <span class="smallerlabel">kg</span>
            </div>
            </div>
  </div>
  <div class="col-md-2 col-sm-12 col-xs-12 form-group">
  <div class="row">
              <div class="col-md-4">
              <label id="lblthisheight" runat="server" class="control-label">Height:</label>
            </div>
             <div class="col-md-5">
             <input id="txtthisheight" size="6" name="thisheight" runat="server" readonly="readonly" style="width:99%" class="form-control"/>                       
            </div>
             <div class="col-md-2">
             <span class="smallerlabel">cm</span>
            </div>
            </div>
  </div>
  <div class="col-md-1 col-sm-12 col-xs-12 form-group">
   <label class="control-label">Function :</label>
  </div>
  <div class="col-md-2 col-sm-12 col-xs-12 form-group">
  <asp:DropDownList ID="ddlthisFunction" runat="server" Enabled="false"  Width="99%" CssClass="form-control">
                            </asp:DropDownList>
  </div>
  <div class="col-md-1 col-sm-12 col-xs-12 form-group">
   <label class="control-label" id="lblthisCD4" runat="server">CD4 :</label>
  </div>
  <div class="col-md-1 col-sm-12 col-xs-12 form-group">
  <input id="txtthisCD4" size="6" name="thiscd4" runat="server" readonly="readonly" style="width:99%" class="form-control" />
  </div>
  <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
   <label class="control-label" id="lblthisCD4Percent" runat="server">CD4 Percent :</label>
  </div>
  <div class="col-md-2 col-sm-12 col-xs-12 form-group">
  <input id="txtthisCDPercent" size="6" name="thiscd4percent" runat="server" readonly="readonly" style="width:99%" class="form-control"/>
  </div>

  </div>
  <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <div class="grid">
                                <div class="rounded" style="width:98%">
                                    <div class="top-outer">
                                        <div class="top-inner">
                                            <div class="top">
                                                <h4>
                                                    <center style="color:#fff;">
                                                        Substitutions and Switches
                                                    </center>
                                                </h4>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="mid-outer">
                                        <div class="mid-inner">
                                            <div class="mid" style="height: 200px; overflow: auto">
                                                <div id="div-gridview" class="gridviewbackup whitebg">
                                                    <asp:GridView ID="grdSubsARVs" runat="server" AutoGenerateColumns="False" AllowSorting="true"
                                                        Width="100%" BorderColor="white" PageIndex="1" BorderWidth="0" GridLines="None"
                                                        CssClass="table table-bordered table-hover" CellPadding="0" CellSpacing="0">                                                      
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Date" DataField="ChangeDate" />
                                                            <asp:BoundField HeaderText="Regimen" DataField="regimentype" />
                                                            <asp:BoundField HeaderText="Line" DataField="RegimenLine" />
                                                            <asp:BoundField HeaderText="Why" DataField="ChangeReason" />
                                                        </Columns>
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
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
            <div class="grid" style="width:98%">
                                <div class="rounded">
                                    <div class="top-outer">
                                        <div class="top-inner">
                                            <div class="top">
                                                <h4>
                                                    <center style="color:#fff;">
                                                        ART Treatment Interruptions</center>
                                                </h4>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="mid-outer">
                                        <div class="mid-inner">
                                            <div class="mid" style="height: 200px; overflow: auto">
                                                <div id="div-gridview2" class="gridviewbackup whitebg">
                                                    <asp:GridView ID="grdInteruptions" runat="server" AutoGenerateColumns="False" AllowSorting="true"
                                                        Width="100%" BorderColor="white" PageIndex="1" BorderWidth="0" GridLines="None"
                                                        Height="20px" CssClass="table table-bordered table-hover" CellPadding="0" CellSpacing="0">
                                                       
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Stop/Lost" DataField="StopLost" />
                                                            <asp:BoundField HeaderText="Date" DataField="ARTEndDate" />
                                                            <asp:BoundField HeaderText="Why" DataField="Reason" />
                                                        </Columns>
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
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Button ID="btn_save" Text="Save" runat="server" OnClick="btn_save_Click" CssClass="btn btn-primary" Height="30px" Width="8%" style="text-align:left;" />
 <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -2%; margin-right:2%; vertical-align: sub; color: #fff;"></label>
 <asp:Button ID="DQ_Check" Text="Data Quality check" runat="server" OnClick="DQ_Check_Click" CssClass="btn btn-primary" Height="30px" Width="14%" style="text-align:left;" />
 <label class="glyphicon glyphicon-ok" style="margin-left: -2%; margin-right:2%; vertical-align: sub; color: #fff;"></label>
 <asp:Button ID="btn_close" Text="Close" runat="server" OnClick="btn_close_Click" CssClass="btn btn-primary" Height="30px" Width="8%" style="text-align:left;" />
 <label class="glyphicon glyphicon-remove-circle" style="margin-left: -2%; margin-right:2%; vertical-align: sub; color: #fff;"></label>
 <asp:Button ID="btn_print" Text="Print" runat="server" OnClientClick="WindowPrint()" CssClass="btn btn-primary" Height="30px" Width="8%" style="text-align:left;" />
 <label class="glyphicon glyphicon-print" style="margin-left: -2%; margin-right:2%; vertical-align: sub; color: #fff;"></label>

             </div>  
                   
 </div>
  </div>
	 <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>
     </div>

    <div style="padding-left: 8px; padding-right: 8px; width:100%;" class="container">
      
        <div class="border center formbg">
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td class="pad5 formbg border">
                            
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <br />
        <div class="border center formbg">
            <table cellspacing="6" cellpadding="0" width="100%" border="0"  class="table-condensed">
                <tbody>
                    <tr>
                        <td class="pad5 formbg border">
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="form pad5 center" colspan="2">
                            <br />
                            
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
