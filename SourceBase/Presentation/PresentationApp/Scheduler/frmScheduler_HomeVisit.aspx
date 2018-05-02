<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    Inherits="frmScheduler_HomeVisit" Title="Untitled Page" CodeBehind="frmScheduler_HomeVisit.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
<script type="text/javascript">
    function RegisterJQuery() {
        $('#StartDate').datepicker({ autoclose: true });   
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
        <h3 class="box-title">Home Visit</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
          <!-- DAL: using tables for form layout. Note that there are classes on labels and td. For custom fields, 
            just use the 2 column layout, if there is an uneven number of cells, set last cell colspan="2" and align="center". Probably should talk through this -->
          <div class="row">
          <br />
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <label for="inputEmail3" class="control-label">
             <asp:Label ID="lblH2" runat="server"></asp:Label>
             </label>
             </div>
                              
    </div>
         <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">
             CHW/CHV/Nurse Team:
             </label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlCHW" runat="server" CssClass="form-control">
                            </asp:DropDownList>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label id="Vdate" class="required">*Home Visit Start Date:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="StartDate" clientidmode="Static"
                                                        runat="server" maxlength="11" size="10" name="StartDate" data-date-format="dd-M-yyyy" style="width:110px;"/>
                                                </div>
                                            </div>
            
             </div>                      
    </div>
    <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Alternative CHW/CHV/Nurse Team:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlAlternateCHW" runat="server" CssClass="form-control"></asp:DropDownList>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Number of Weeks:</label> 
             </div>
              <div class="col-md-3 col-sm-12 col-xs-12 form-group">
              <asp:TextBox ID="txtNumofWeeks" MaxLength="2" Width="70" runat="server" CssClass="form-control" OnTextChanged="txtNumofWeeks_change" AutoPostBack="true"></asp:TextBox>
              </div>
                                 
    </div>
    <div class="row">
    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Number of Visits per Week:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtVisitPerWeek" MaxLength="2" Width="70" runat="server" CssClass="form-control"></asp:TextBox>
             </div>   
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             
             </div>
                                
    </div>
    <div id="VisitPerWeekShow" style="display: none">
    <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Visits by Week:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label"> Week 1:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <select name="VisitsPerWeek" id="VisitsPerWeek1" runat="server" class="form-control" style="width:120px;">
                                        <option value="0">-Select-</option>
                                        <option value="1">1 day</option>
                                        <option value="2">2 days</option>
                                        <option value="3">3 days</option>
                                        <option value="4">4 days</option>
                                        <option value="5">5 days</option>
                                        <option value="6">6 days</option>
                                        <option value="7">7 days</option>
                                    </select>
             </div>
              <div id="VisitPerWeekShow2" style="display: none">
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label  class="control-label">Week 2:</label>
             </div>     
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <select name="VisitsPerWeek" id="VisitsPerWeek2" runat="server" class="form-control" style="width:120px;">
                                        <option value="0">-Select-</option>
                                        <option value="1">1 day</option>
                                        <option value="2">2 days</option>
                                        <option value="3">3 days</option>
                                        <option value="4">4 days</option>
                                        <option value="5">5 days</option>
                                        <option value="6">6 days</option>
                                        <option value="7">7 days</option>
                                    </select>
             </div>    
             </div>             
    </div>
    <div class="row">
    <div id="VisitPerWeekShow3" style="display: none">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label"></label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Week 3:</label>
             </div>
            
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <select name="VisitsPerWeek" id="VisitsPerWeek3" runat="server" class="form-control" style="width:120px;">
                                        <option value="0">-Select-</option>
                                        <option value="1">1 day</option>
                                        <option value="2">2 days</option>
                                        <option value="3">3 days</option>
                                        <option value="4">4 days</option>
                                        <option value="5">5 days</option>
                                        <option value="6">6 days</option>
                                        <option value="7">7 days</option>
                                    </select>
             </div>
              </div>
              <div id="VisitPerWeekShow4" style="display: none">
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label  class="control-label">Week 4:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <select name="VisitsPerWeek" id="VisitsPerWeek4" runat="server" class="form-control" style="width:120px;">
                                        <option value="0">-Select-</option>
                                        <option value="1">1 day</option>
                                        <option value="2">2 days</option>
                                        <option value="3">3 days</option>
                                        <option value="4">4 days</option>
                                        <option value="5">5 days</option>
                                        <option value="6">6 days</option>
                                        <option value="7">7 days</option>
                                    </select>
             </div> 
             </div>                   
    </div>
    </div>

    <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
            <asp:Panel ID="pnlCustomList" Visible="false" runat="server" Height="100%" Width="100%"
                                Wrap="true">
                            </asp:Panel>
             </div>
                              
    </div>
    <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click1" CssClass="btn btn-primary"
                                Width="8%" Height="30px" Style="text-align: left;" />
                            <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; margin-right: 2%;
                                vertical-align: sub; color: #fff;">
                            </label>
                            <asp:Button ID="btnComplete" runat="server" Text="Data Quality Check" OnClick="btnComplete_Click"
                                CssClass="btn btn-primary" Width="17%" Height="30px" Style="text-align: left;" />
                            <label class="glyphicon glyphicon-floppy-saved" style="margin-left: -3%; margin-right: 2%;
                                vertical-align: sub; color: #fff;">
                            </label>
                            <asp:Button ID="btnCancel" runat="server" Text="Close" OnClick="btnCancel_Click"
                                CssClass="btn btn-primary" Width="8%" Height="30px" Style="text-align: left;" />
                            <label class="glyphicon glyphicon-remove-circle" style="margin-left: -3%; margin-right: 2%;
                                vertical-align: sub; color: #fff;">
                            </label>
                            <asp:Button ID="theBtn" runat="server" CssClass="textstylehidden" OnClick="theBtn_Click"
                                Width="8%" Height="30px" Style="text-align: left;" />
                            <label id="lblOKClick" class="glyphicon glyphicon-ok-sign" runat="server" style="margin-left: -4%;
                                margin-right: 3%; vertical-align: middle; color: #fff; margin-top: .6%; visibility: hidden;">
                            </label>
                            <asp:Button ID="theBtn1" runat="server" CssClass="textstylehidden" OnClick="theBtn1_Click"
                                Width="8%" Height="30px" Style="text-align: left;" />
                            <label id="lblDQClick" class="glyphicon glyphicon-random" runat="server" style="margin-left: -4%;
                                margin-right: 3%; vertical-align: middle; color: #fff; margin-top: .6%; visibility: hidden;">
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
