<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    CodeBehind="frmPharmacy_ReferenceMaterials.aspx.cs" Inherits="PresentationApp.PharmacyDispense.frmPharmacy_ReferenceMaterials" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">

      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">Reference Materials</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
         <div class="row">
         <br />
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <li><a href="../ReferenceDocuments/Rapid%20Advice%20Booklet.pdf" target="_blank">Rapid
                Advice Booklet 2014</a></li>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <li><a href="../ReferenceDocuments/Guidelines%20for%20Antiretroviral%20Drug%20Therapy%20in%20Kenya.pdf"
                target="_blank">Guidelines for Antiretroviral Drug Therapy in Kenya</a></li>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
           <li><a href="../ReferenceDocuments/Kenya%20clinical%20National%20Manual%20for%20ART%20providers.pdf"
                target="_blank">Kenya Clinical National Manual for ART providers</a></li>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <li><a href="http://nascop.or.ke/online-library.php"
                target="_blank">NASCOP Programmes</a></li>
             </div> 
 </div>
	 <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>
     </div>

    
</asp:Content>
