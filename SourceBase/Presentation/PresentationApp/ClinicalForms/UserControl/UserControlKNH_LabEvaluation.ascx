<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControlKNH_LabEvaluation.ascx.cs" Inherits="PresentationApp.ClinicalForms.UserControl.UserControlKNH_LabEvaluation" %>

      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
      <%-- <div class="box-header">
        <h3 class="box-title">Heading	</h3>
       </div>--%>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
         <div class="row" align="center">
         <br />
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <asp:Button ID="btnLab" runat="server" Font-Bold="True" Text="Order Lab Tests" onclick="btnLab_Click" CssClass="btn btn-primary" 
             Height="30px" Width="15%" Style="text-align:left;" />
             <label class="fa fa-flask" style="margin-left: -3%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
             </div>
            
 </div>
 <div class="row" id="divlabdiagostic" style="display: block;">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Review of lab/other diagnostic test:</label>
             </div>
             <div class="col-md-9 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtlabdiagnosticinput" runat="server" Rows="4" TextMode="MultiLine" Width="99%" Height="100" CssClass="form-control" Style="resize:none;"></asp:TextBox>
             </div>
            
 </div>
	 <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>
     </div>


