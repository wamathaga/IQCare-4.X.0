<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" Inherits="frmReportCustomNew"
    MaintainScrollPositionOnPostback="true" EnableViewState="true" AutoEventWireup="true"
    EnableEventValidation="true" Title="Untitled Page" CodeBehind="frmReportCustomNew.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <script language="javascript" type="text/javascript">
        function GetRefresh() {
            document.forms[0].submit();
        }
        function fnChangeValue(id, vid) {
            var CtrlID = 'ctl00_IQCareContentPlaceHolder_' + id;
            var CtrlvID = 'ctl00_IQCareContentPlaceHolder_' + vid;
            if (document.getElementById(CtrlID).value == 'IS NULL' || document.getElementById(CtrlID).value == 'IS NOT NULL') {
                alert("Please change where conditon");
                document.getElementById(CtrlvID).value = 'Select';
                return false;
            }
            return true;
        }
        function fnChangeCondition(id, vid) {
            var CtrlID = 'ctl00_IQCareContentPlaceHolder_' + id;
            var CtrlvID = 'ctl00_IQCareContentPlaceHolder_' + vid;
            if (document.getElementById(CtrlID).value == 'IS NULL' || document.getElementById(CtrlID).value == 'IS NOT NULL') {

                document.getElementById(CtrlvID).value = '';
                return false;
            }
            return true;
        }
        function fnChangeText(id, vid) {
            var CtrlID = 'ctl00_IQCareContentPlaceHolder_' + id;
            var CtrlvID = 'ctl00_IQCareContentPlaceHolder_' + vid;
            if (document.getElementById(CtrlID).value == 'IS NULL' || document.getElementById(CtrlID).value == 'IS NOT NULL') {
                alert("Please change where conditon");
                document.getElementById(CtrlvID).value = '';
                return false;
            }
            return true;
        }
        function fnChangeDateValue(id, vid) {

            var CtrlID = 'ctl00_IQCareContentPlaceHolder_' + id;
            var CtrlvID = 'ctl00_IQCareContentPlaceHolder_' + vid;
            if (document.getElementById(CtrlID).value == 'IS NULL' || document.getElementById(CtrlID).value == 'IS NOT NULL') {
                alert("Please change where conditon");
                document.getElementById(CtrlvID).value = '';
                return false;
            }
            return true;
        }
        function fnValidate() {


            for (j = 1; j < 10; j++) {
                var controlCount = document.forms[0].elements.length;
                var cntrl = "";
                var ctrlValue = "";


                for (k = 2; k < 6; k++) {
                    cntrl = trim('ctl00_IQCareContentPlaceHolder_pnlSub' + j + 'Con' + k + 'Optr');
                    ctrlValue = trim('ctl00_IQCareContentPlaceHolder_pnlSub' + j + 'Con' + k + 'txtValue');
                    ctrlDate = trim('ctl00_IQCareContentPlaceHolder_pnlSub' + j + 'Con' + k + 'DateValue')
                    ctrldd = trim('ctl00_IQCareContentPlaceHolder_pnlSub' + j + 'Con' + k + 'ddlValue')
                    for (i = 0; i < controlCount; i++) {


                        var elm = document.forms[0].elements[i];
                        var ctrlID = elm.id;

                        if (ctrlID == cntrl) {
                            if (document.getElementById(cntrl).value != 'Select' && document.getElementById(cntrl).value != 'IS NULL' && document.getElementById(cntrl).value != 'IS NOT NULL') {
                                if (document.getElementById(ctrlValue) != null) {
                                    if (document.getElementById(ctrlValue).value == "") {
                                        alert('Pleas enter value in where condition');
                                        return false;
                                    }
                                }
                                if (document.getElementById(ctrlDate) != null) {
                                    if (document.getElementById(ctrlDate).value == "") {
                                        alert('Pleas enter Date in where condition');
                                        return false;
                                    }
                                }
                                if (document.getElementById(ctrldd) != null) {

                                    if (document.getElementById(ctrldd).value == "") {
                                        alert('Pleas  select a value in where condition');
                                        return false;
                                    }
                                }


                            }
                        }

                    }
                }
            }

            return true;
        }
        function trim(str) {
            while (str.substring(0, 1) == ' ') {
                str = str.substring(1, str.length);
            }
            while (str.substring(str.length - 1, str.length) == ' ') {
                str = str.substring(0, str.length - 1);
            }
            return str;
        } 
    </script>
    <div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">New Custom Report</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
	<div class="row" align="center">
         <br />           
        </div>
         <asp:UpdatePanel ID="up_customreport" runat="server">
                <ContentTemplate>
                <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <label class="control-label">
             <asp:RadioButton ID="rdoDynamicQuery" runat="server" Text="Dynamic Query" OnCheckedChanged="rdoDynamicQuery_CheckedChanged"
                                        GroupName="QueryType" ToolTip="Select option" AutoPostBack="true"></asp:RadioButton>
                                    <asp:RadioButton ID="rdoTSQL" runat="server" Text="Type SQL" OnCheckedChanged="rdoTSQL_CheckedChanged"
                                        GroupName="QueryType" ToolTip="Make your own query" AutoPostBack="true"></asp:RadioButton>
             </label>
             </div>
 </div>
  <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <label class="control-label">Step 1: Give your report an identity</label>
 </div>
 </div>
<div class="row">
 <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <label class="control-label required"> *Report Title :</label>
             </div>
             <div class="col-md-5 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtTitle" runat="server" Width="98%" CssClass="form-control"></asp:TextBox>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Description :</label>
             </div>
             <div class="col-md-5 col-sm-12 col-xs-12 form-group">
              <asp:TextBox ID="txtDescription" runat="server" Width="98%" CssClass="form-control" TextMode="MultiLine" Style="resize:none;"></asp:TextBox>
             </div> 
 </div>
 <div class="row">
 <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <label class="control-label">*Category :</label>
             </div>
             <div class="col-md-5 col-sm-12 col-xs-12 form-group">
              <asp:DropDownList ID="ddCategory" runat="server" Width="98%" CssClass="form-control"></asp:DropDownList>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <div class="row">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
<input id="btnNewCategory" type="button" value="New Category" runat="server" class="btn btn-primary" style="text-align: left; height: 30px; width: 60%" />
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
 <input id="txtNewCategory" type="text" runat="server" style="visibility: hidden;" class="form-control"/>
             </div>
 </div>

             
           
             </div>
            
 </div>
 <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <label class="control-label">Step 2: Select your columns and column action</label>
 </div>
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Panel ID="pnlCustomRpt" runat="server" Width="100%" CssClass="whitebg"></asp:Panel>
             </div>
 </div>
  <div class="row" align="right">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Button ID="btnAddField" OnClick="btnAddField_Click" runat="server" Text="Add Field"
                                        CssClass="btn btn-primary" Height="30px" Width="11%" Style="text-align: left;" />
                                    <label class="glyphicon glyphicon-plus" style="margin-left: -3%; margin-right: 2%;
                                        vertical-align: sub; color: #fff; margin-top: 1%;">
                                    </label>
                                    <asp:Button ID="btnRemoveField" OnClick="btnRemoveField_Click" runat="server" Text="Remove Field"
                                        CssClass="btn btn-primary" Height="30px" Width="14%" Style="text-align: left;
                                        visibility: hidden" />
                                    <label id="lblbtnRemove" runat="server" class="glyphicon glyphicon-remove" style="margin-left: -3%;
                                        margin-right: 2%; vertical-align: sub; color: #fff; visibility: hidden; margin-top: 1%;">
                                    </label>
                                    <input style="visibility: hidden; width: 12px" id="hdnFld" type="text" name="hdnFld"
                                        runat="server" />
                                    <asp:HiddenField ID="hidMessage" runat="server"></asp:HiddenField>
                                    <asp:HiddenField ID="hidOrgMsg" runat="server"></asp:HiddenField>
             </div>
            
 </div>
  <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 Advanced Query Options:
             </div>
 <hr />   
 </div>
 <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Enter SQL Statement :</label>
             </div>
             <div class="col-md-10 col-sm-12 col-xs-12 form-group">
<asp:TextBox ID="txtSQLStatement" runat="server"  Width="98%" CssClass="form-control" TextMode="MultiLine" Height="150px" Style="resize:none;"></asp:TextBox>
                                                    <asp:HiddenField ID="hidReportid" runat="server"></asp:HiddenField>
             </div>
 </div>
  <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Button ID="btnSaveReport" OnClientClick="return fnValidate();" OnClick="btnSaveReport_Click"
                                        runat="server" Text="Save and Run Report" CssClass="btn btn-primary" Height="30px"
                                        Width="18%" Style="text-align: left;" />
                                    <label class="glyphicon glyphicon-saved" style="margin-left: -2.5%; margin-right: 2%;
                                        vertical-align: sub; color: #fff; margin-top: 1%;">
                                    </label>
                                    <asp:Button ID="Button2" runat="server" Text="Export Report Parameters" OnClick="Button2_Click"
                                        CssClass="btn btn-primary" Height="30px" Width="21%" Style="text-align: left;" />
                                    <label class="glyphicon glyphicon-new-window" style="margin-left: -2.5%; margin-right: 2%;
                                        vertical-align: sub; color: #fff; margin-top: 1%;">
                                    </label>
                                    <asp:Button ID="btnClose" OnClick="btnClose_Click" runat="server" Text="Close" CssClass="btn btn-primary"
                                        Height="30px" Width="8%" Style="text-align: left;" />
                                    <label class="glyphicon glyphicon-remove-circle" style="margin-left: -2.5%; margin-right: 2%;
                                        vertical-align: sub; color: #fff; margin-top: 1%;">
                                    </label>
             </div>
            
 </div>
                    
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSaveReport" />
                    <asp:PostBackTrigger ControlID="btnRemoveField" />
                    <asp:PostBackTrigger ControlID="btnClose" />
                    <asp:PostBackTrigger ControlID="Button2" />
                </Triggers>
            </asp:UpdatePanel>
	 <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>
     </div>
     </div>
           
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
                    //document.getElementById('divshow').disabled="disabled";



                }

                pageManager.add_endRequest(myEndRequestCallback);
                function myEndRequestCallback(sender, args) {
                    var hidbox = $get('<%=hidMessage.ClientID %>');
                    $get(uiId).disabled = false;
                    //document.getElementById('divshow').disabled="";
                    if (hidbox.value == "No") {
                        alert("No Record Found");
                    }
                    if (hidbox.value == "Title") {
                        var msg = document.getElementById('<%=hidOrgMsg.ClientID %>').value
                        alert(msg);
                        document.getElementById('<%=hidOrgMsg.ClientID %>').value = "";
                        hidbox.value = "";
                    }
                }
            </script>

</asp:Content>
