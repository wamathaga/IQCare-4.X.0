<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControlKNH_DrugAllergies.ascx.cs"
    Inherits="PresentationApp.ClinicalForms.UserControl.UserControlKNH_DrugAllergies" %>
  
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
      <%-- <div class="box-header">
        <h3 class="box-title">Allergy Details</h3>
       </div>--%>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
         <div class="row" align="center">
         <br />
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <div class="GridView whitebg" style="cursor: pointer;width:99%">
                    <div class="grid">
                        <div class="rounded">
                            <div class="top-outer">
                                <div class="top-inner">
                                    <div class="top">
                                        <h2>
                                            Allergy Details</h2>
                                    </div>
                                </div>
                            </div>
                            <div class="mid-outer">
                                <div class="mid-inner">
                                    <div class="mid" style="height: 160px; overflow: auto; padding-bottom: 10px;">
                                        <div id="div-gridview" class="GridView whitebg">
                                            <asp:GridView ID="grdDrugAllergy" runat="server" AutoGenerateColumns="False" Width="100%"
                                                BorderWidth="0" GridLines="None" CssClass="table table-bordered table-hover" CellPadding="0" CellSpacing="0">                                              
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
                                  
    </div>
    <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Button ID="btnAllergies" runat="server" OnClick="btnAllergies_Click" Text="Add Allergies"
                    CssClass="btn btn-primary" Height="30px" Width="12%" Style="text-align: left;" />
                <label class="glyphicon glyphicon-plus-sign" style="margin-left: -3%; margin-right: 2%;
                    vertical-align: sub; color: #fff; margin-top: 1%;">
                </label>
 </div>
 </div>
	 <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>
     </div>
    
<div class="center formbg">
    <table cellspacing="6" cellpadding="0" width="100%" border="0">
        <tr>
            <td class="form">
                
            </td>
        </tr>
        <tr>
            <td class="form">
                
            </td>
        </tr>
    </table>
</div>
