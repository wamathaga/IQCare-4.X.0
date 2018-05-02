<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    Inherits="Reports_frmReportViewer" Title="Untitled Page" CodeBehind="frmReportViewer.aspx.cs" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <script language="javascript" type="text/javascript">

        function btn_Back() {
            //history.back(); 

        }
        function fnPrient() {
            window.open("..\\ExcelFiles\\PView.pdf", "mywindow", "status=1,toolbar=0");
            //window.print(); 
        }
    </script>
    <div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title" id="hBar" runat="server">
         Report
        	</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content--%>
          <div class="row text-center">
          <br />
            <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" CssClass="btn btn-primary"
                                Height="30px" Width="10%" Style="text-align: left;" />
                            <label class="glyphicon glyphicon-print" style="margin-left: -2.5%; margin-right: 2%;
                                vertical-align: sub; color: #fff;">
                            </label>
                             <asp:Button ID="btnExcel" runat="server" Text="Export to Excel" Visible="false" OnClick="btnExcel_Click"
                                CssClass="btn btn-primary" Height="30px" Width="12%" Style="text-align: left;" />
                            <label id="lblBtnExcel" runat = "server" class="glyphicon glyphicon-export" style="margin-left: -2.5%; margin-right: 2%;
                                vertical-align: sub; color: #fff;">
                            </label>
                            <br />
                            <br />
                            </div>
            <div class="row text-center" style="margin-left:15%;overflow: auto;">
           
                                <CR:CrystalReportViewer ID="crViewer" runat="server" AutoDataBind="true" HasCrystalLogo="False"
                                    GroupTreeStyle-ShowLines="false" EnableToolTips="true" HasDrillUpButton="False"
                                    HasSearchButton="False" HasToggleGroupTreeButton="False" HasZoomFactorList="False"
                                    EnableDatabaseLogonPrompt="true" HasExportButton="False" HasPrintButton="False"
                                    Height="50px" Width="350px" />
                                    <br />
                                  
                            </div>
            <div class="row text-center">
            <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn btn-primary"
                                Height="30px" Width="8%" Style="text-align: left;" />
                            <label class="glyphicon glyphicon-backward" style="margin-left: -3%; margin-right: 2%;
                                vertical-align: sub; color: #fff;">
                            </label>
                            <br />
            </div>
         </div>
      </div>
      </div>
      </div>
     </div>
     </div>

    
   
</asp:Content>
