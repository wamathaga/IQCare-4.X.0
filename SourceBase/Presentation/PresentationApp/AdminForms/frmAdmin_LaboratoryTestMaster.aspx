<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="AdminLaboratoryTest_Master"
    Title="Untitled Page" Codebehind="frmAdmin_LaboratoryTestMaster.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <script language="javascript">
        function ShowHideBoundary() {

            var pnlBoundary = document.getElementById("<%= pnlBoundary.ClientID %>");
            var pnlBoundary2 = document.getElementById("<%= pnlBoundary2.ClientID %>");
            var dataType = document.getElementById("<%= ddlDataType.ClientID %>");
            var btnselectList = document.getElementById("<%= btnselectList.ClientID %>");

            if (dataType[dataType.selectedIndex].text == 'Numeric') {
                pnlBoundary.style.display = 'block';
                pnlBoundary2.style.display = 'block';
                btnselectList.style.visibility = "hidden";
            }
            else if (dataType[dataType.selectedIndex].text == 'Select List') {
                pnlBoundary.style.display = 'none';
                pnlBoundary2.style.display = 'none';
                btnselectList.style.visibility = "visible";
            }
            else {
                pnlBoundary.style.display = 'none';
                pnlBoundary2.style.display = 'none';
                btnselectList.style.visibility = "hidden";
            }

        }
        function selectwinopen() {
            window.open('frmAdmin_LaboratorySelectList.aspx?LabId=' + '<%= LabIdforselectList %>', 'Selection', 'toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=no,resize=no,width=370,height=310,scrollbars=yes');
        }
    </script>
    <%--<form id="adduser" method="post" runat="server">--%>
    
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title"><asp:Label ID="lblH2" runat="server" Text="Add/Edit LaboratoryTest"></asp:Label></h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
         <div class="row">
         <br />
 <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
             <label class="control-label">Laboratory Test :</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtLabName" runat="server" MaxLength="50" Width="99%" CssClass="form-control"></asp:TextBox>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
             <label class="control-label">Department :</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddDepartment" runat="server" CssClass="form-control" Width="99%"></asp:DropDownList>
             </div> 
              <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
             <label class="control-label">Data Type :</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <div class="row">
             <div class="col-md-10">
             <asp:DropDownList ID="ddlDataType" runat="server" CssClass="form-control" Width="99%">
                                <asp:ListItem Text="Text" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Numeric" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Select List" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Group" Value="3"></asp:ListItem>
                            </asp:DropDownList></div>
             <div class="col-md-1" style="margin-left:-31px;">
             <input type="button" id="btnselectList" name="btnselectList" onclick="javascript:selectwinopen();return false;"
                                runat="server" value="..." class="btn btn-primary" /></div>
             </div><asp:TextBox ID="txtSeq" Visible="false" runat="server">1</asp:TextBox>

             </div> 
              <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
             <label class="control-label">Unit</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddUnit" runat="server" Width="99%" CssClass="form-control">
                                            </asp:DropDownList>
             </div> 
              
 </div>
 <div class="row">
  <asp:Panel ID="pnlBoundary" runat="server">
 <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
             <label class="control-label">Lower Boundary :</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtLowerBoundary" runat="server" MaxLength="19" CssClass="form-control" Width="99%"></asp:TextBox>
             </div>
             </asp:Panel>
              <asp:Panel ID="pnlBoundary2" runat="server">
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
             <label class="control-label">Upper Boundary :</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtUpperBoundary" runat="server" MaxLength="19" CssClass="form-control" Width="99%"></asp:TextBox>
             </div> 
             </asp:Panel>
              <asp:Label runat="server" ID="lblStatus1">
              <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
             <label class="control-label">Status :</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddStatus" runat="server" CssClass="form-control" Width="99%">
                                    <asp:ListItem Text="Active" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="InActive" Value="1"></asp:ListItem>
                                </asp:DropDownList>
             </div> 
             </asp:Label>
              
              <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
             <label class="control-label"></label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             
             </div> 
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group text-nowrap">
             <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"  CssClass="btn btn-primary"
                                Height="30px" Width="8%" style="text-align:left;" />
                            <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; margin-right:2%; vertical-align: sub;
                                color: #fff;">
                            </label>
                            <asp:Button ID="btnExit" runat="server" Text="Close" OnClick="btnExit_Click" CssClass="btn btn-primary"
                                Height="30px" Width="8%" style="text-align:left;"/>
                            <label class="glyphicon glyphicon-remove-circle" style="margin-left: -3%; margin-right:2%; vertical-align: sub;
                                color: #fff;">
                            </label>
             </div>
            
 </div>
	 <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>
     </div>
    
    
    <script language="javascript" type="text/javascript">
        var pnlBoundary = document.getElementById("<%= pnlBoundary.ClientID %>");
        var pnlBoundary2 = document.getElementById("<%= pnlBoundary2.ClientID %>");
        var dataType = document.getElementById("<%= ddlDataType.ClientID %>");
        var btnselectList = document.getElementById("<%= btnselectList.ClientID %>");
        if (dataType[dataType.selectedIndex].text == 'Numeric') {
            pnlBoundary.style.display = 'block';
            pnlBoundary2.style.display = 'block';
            btnselectList.style.visibility = "hidden";
        }
        else if (dataType[dataType.selectedIndex].text == 'Select List') {
            pnlBoundary.style.display = 'none';
            pnlBoundary2.style.display = 'none';
            btnselectList.style.visibility = "visible";
        }
        else {
            pnlBoundary.style.display = 'none';
            pnlBoundary2.style.display = 'none';
            btnselectList.style.visibility = "hidden";
        }
    </script>
</asp:Content>
