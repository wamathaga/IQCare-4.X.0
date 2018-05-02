<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    CodeBehind="frmClinical_Nigeria_InitialVisit.aspx.cs" Inherits="PresentationApp.ClinicalForms.frmClinical_Nigeria_InitialVisit" %>

<%@ Register TagPrefix="UcDrugAllergies" TagName="Uc6" Src="~/ClinicalForms/UserControl/UserControlKNH_DrugAllergies.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
<script type="text/javascript">
    function RegisterJQuery() {
        $('#txtConfHIVdate').datepicker({ autoclose: true });
        $('#txtMedElligDate').datepicker({ autoclose: true });
        $('#txtIntAdhCounsling').datepicker({ autoclose: true });
        $('#txtTransferedIn').datepicker({ autoclose: true });
    }
    //Calling RegisterJQuery when document is ready (Page loaded first time)
    $(document).ready(RegisterJQuery);
    //Calling RegisterJQuery when the page is doing postback (asp.net)
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RegisterJQuery);
</script>
<script language="javascript" type="text/javascript">
    function ShowHide(theDiv, YN, theFocus) {
        $(document).ready(function () {

            if (YN == "show") {
                //                    $("#" + theDiv).slideDown();
                $("#" + theDiv).show();

            }
            if (YN == "hide") {
                //                    $("#" + theDiv).slideUp();
                $("#" + theDiv).hide();


            }

        });

    }
    function WindowPrint() {
        window.print();
    }
    //DropDown list
    function getSelectedValue(DivId, DDText, str) {
        var e = document.getElementById(DDText);
        var text = e.options[e.selectedIndex].innerText;
        var YN = "";
        if (text == str) {
            YN = "show";
        }
        else {
            YN = "hide";
        }
        ShowHide(DivId, YN);
    }
        </script>
        <div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">Nigeria Initial Visit</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
         <div class="row box-header">
         <br />
         <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <h5 class="box-title">Clinical Status</h5>
         </div>             
            </div>
         <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label" id="lblFP" runat="server">Care Entry Point:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlCareEntryPoint" runat="server" CssClass="form-control" Width="99%"></asp:DropDownList>
             </div>
              <div id="divCareEntryPointOther" style="display: none;">
             <div class="col-md-2 col-sm-12 col-xs-12 form-group text-nowrap">
             <label id="Label6" runat="server" class="control-label">Other (Specify):</label>             
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
              <asp:TextBox ID="txtCareEntryOther" runat="server" CssClass="form-control" Width="99%"></asp:TextBox>
             </div> 
             </div>
              <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label"> Date of Confirmed HIV Test:</label>
             </div> 
              <div class="col-md-2 col-sm-12 col-xs-12 form-group">
              <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtConfHIVdate" clientidmode="Static"
                                                       maxlength="11" size="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
              
             </div> 
 </div>
 <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
 <label id="lblTBStatus" runat="server" class="control-label">Mode of HIV Test:</label>            
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            <asp:DropDownList ID="ddlModeHIVTest" runat="server" CssClass="form-control" Width="99%"></asp:DropDownList>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Where:</label>           
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            <asp:TextBox ID="txtmodetestwhere" runat="server" CssClass="form-control" Width="99%"></asp:TextBox>
             </div> 
              <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
              <label class="control-label">Prior ART:</label>             
             </div> 
              <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <asp:DropDownList ID="ddlPriorART" runat="server" CssClass="form-control" Width="99%"></asp:DropDownList>
             </div> 
 </div>
  <div class="row box-header">       
         <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <h5 class="box-title">ART Commencement</h5>
         </div>             
            </div>
            <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Date Medically Elligible:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtMedElligDate" clientidmode="Static"
                                                       maxlength="11" size="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
            
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
             <label class="control-label">Why Elligible:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <asp:DropDownList ID="ddlwhyelligible" runat="server" CssClass="form-control" Width="99%"></asp:DropDownList>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label  class="control-label">Date Initial Adherence Counseling Completed :</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtIntAdhCounsling" clientidmode="Static"
                                                       maxlength="11" size="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
            
             </div> 
 </div>
 <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Date Transferred In:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtTransferedIn" clientidmode="Static"
                                                       maxlength="11" size="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
            
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group text-nowrap">
             <label class="control-label">Facility Transferred From:</label>
             </div>
             <div class="col-md-5 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlFacTransfFrom" runat="server" CssClass="form-control" Width="99%"></asp:DropDownList>
             </div> 
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             
             </div>
 </div>
  <div class="row box-header">         
         <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <h5 id="H3" class="box-title">Drug Allergies</h5>
         </div>             
            </div>
            <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <UcDrugAllergies:Uc6 ID="UCDrugAllergies" runat="server" />
             </div>
             
 </div>
  <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-primary"
                            Height="30px" Width="8%" style="text-align:left;" />
                            <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; margin-right:2%; vertical-align: sub; color: #fff;"></label>
                        <asp:Button ID="btndataquality" Text="Data Quality check" runat="server" CssClass="btn btn-primary"
                            Height="30px" Width="14%" style="text-align:left;" OnClick="btndataquality_Click" />
                            <label class="glyphicon glyphicon-ok" style="margin-left: -3%; margin-right:2%; vertical-align: sub; color: #fff;"></label>
                        <asp:Button ID="btnclose" runat="server" Text="Close" CssClass="btn btn-primary"
                            Height="30px" Width="8%" style="text-align:left;" OnClick="btnclose_Click" />
                            <label class="glyphicon glyphicon-remove-circle" style="margin-left: -3%; margin-right:2%; vertical-align: sub; color: #fff;"></label>
                        <asp:Button ID="btnPrint" runat="server" OnClientClick="WindowPrint()" CssClass="btn btn-primary"
                           Height="30px" Width="8%" style="text-align:left;" Text="Print" OnClick="btnPrint_Click" />
                           <label class="glyphicon glyphicon-print" style="margin-left: -3%; vertical-align: sub; color: #fff;"></label>
 </div>
 </div>
	 <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>
     </div>
     </div>
    <d
        <div class="border center formbg">
            <br />
            <h4 class="forms" align="left">
                </h4>
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td class="border pad5 whitebg" align="center" width="100%">
                            
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <br />
        <div class="border center formbg">
            <table cellspacing="6" cellpadding="0" width="100%" border="0" id="Table4" runat="server"
                class="table-condensed">
                <tr id="Tr2" runat="server" align="center">
                    <td id="Td3" runat="server" class="form">
                        
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
