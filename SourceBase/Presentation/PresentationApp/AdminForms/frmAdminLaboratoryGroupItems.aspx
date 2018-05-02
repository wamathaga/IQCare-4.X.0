<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true" CodeBehind="frmAdminLaboratoryGroupItems.aspx.cs" Inherits="PresentationApp.AdminForms.frmAdminLaboratoryGroupItems" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
<div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">Laboratory Group Items >> <asp:Label ID="lblH2" runat="server" Text="Lab Group Name"></asp:Label></h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
	<div class="row" align="center">
         <br />           
        </div>
        <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Laboratory Test :</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtLabName" runat="server" MaxLength="50" Width="99%" CssClass="form-control"></asp:TextBox>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Department :</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddDepartment" runat="server" Width="99%" CssClass="form-control">
                            </asp:DropDownList>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Status :</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddStatus" runat="server" Width="99%" CssClass="form-control">
                                    <asp:ListItem Text="Active" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="InActive" Value="1"></asp:ListItem>
                                </asp:DropDownList>
             </div> 
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
              <div id="div-gridview" class="div-gridview whitebg" style="overflow: auto; height: 400px;padding-right:5px;">
                <asp:GridView ID="grdLabs" runat="server" AutoGenerateColumns="False" Width="100%"
                    AllowSorting="True" ShowHeaderWhenEmpty="True" CssClass="table table-bordered table-hover" >
                   
                    <Columns>
                     <asp:TemplateField Visible="False">
                         
                            <ItemTemplate>
                                <div>
                                    <asp:Label ID="lblLabTestID" runat="server" AutoPostBack="false" Text='<%#Bind("LabTestID")%>'>
                                    </asp:Label></div>
                            </ItemTemplate>                           
                           
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Lab Test Name">                            
                            <ItemTemplate>
                                <div id="grdchk">
                                    <asp:CheckBox ID="chkLabTest" runat="server" AutoPostBack="false" Checked='<%#Convert.ToBoolean(Eval("member")) %>' Text='<%#Bind("LabName")%>'>
                                    </asp:CheckBox></div>
                            </ItemTemplate>
                            
                          
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Department">
                            <ItemTemplate>
                                <asp:Label ID="lblDepartment" runat="server" Text='<%#Bind("LabDepartmentName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                           <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" Text='<%#Bind("Status")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                      
                    </Columns>
                </asp:GridView>
            </div>
             </div>
             
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
  <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"
                            CssClass="btn btn-primary" Height="30px" Width="8%" 
                                Style="text-align: left;"  />
                            <label class="glyphicon glyphicon-open" style="margin-left: -3%; vertical-align: sub;
                                color: #fff;margin-right: 2%; ">
                            </label>
                            <asp:Button ID="btnExit" runat="server" Text="Close" OnClick="btnExit_Click"
                             CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                            <label class="glyphicon glyphicon-remove-circle" style="margin-left: -3%; margin-right: 2%;
                                vertical-align: sub; color: #fff;">
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
