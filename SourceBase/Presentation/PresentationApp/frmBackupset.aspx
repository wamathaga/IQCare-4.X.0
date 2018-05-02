<%@ Page Language="C#" AutoEventWireup="true" Inherits="frmBackupset" Codebehind="frmBackupset.aspx.cs" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Backup Setup</title>
    <%--<link rel="stylesheet" type="text/css" href="./style/styles.css" />--%>
    <link href="../Bootstrap/dist/css/AdminLTE.css" rel="stylesheet" type="text/css" />
    <link href="Style/bootstrap-3.3.6-dist/css/bootstrap.css" rel="stylesheet" type="text/css" /> 
    <link href="Style/bootstrap-3.3.6-dist/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="BackUpSet" runat="server" class="border" style="width: 600px; height: 398px;padding-left: 10px">
    <div style="width: 588px; height: 300px;">
    <div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">Backup Setup</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
         <div class="row">
         <br />
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <asp:ListBox ID="lstBackupFile" runat="server" Height="150px" Width="538px"></asp:ListBox>
             </div>
                                  
    </div>
         <div class="row text-center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Button ID="btnLoadBkp" runat="server" Text="Load Backup Set" OnClick="btnLoadBkp_Click" CssClass="btn btn-primary" Height="30px" Width="30%" Style="color:#fff; text-align:left;"/>
 <label class="glyphicon glyphicon-refresh" style="margin-left: -4%; vertical-align: sub; color: #fff;"></label>
 </div>
 </div>
 <div class="row text-center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <div class="gridviewbackup whitebg">
                                <asp:GridView ID="grdBackupset" runat="server" AllowSorting="false" AutoGenerateColumns="false"
                                    Width="100%" BackColor="white" CellSpacing="1" OnRowDataBound="grdBackupsetDataBound"
                                    OnSelectedIndexChanging="grdBackupset_SelectedIndexChanging" CssClass="table table-bordered table-hover">                                    
                                </asp:GridView>
                            </div>
 </div>
 </div>
       
   <div class="row text-center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CssClass="btn btn-primary" Height="30px" Width="15%" Style="color:#fff; text-align:left;"/>
 <label class="glyphicon glyphicon-remove" style="margin-left: -4%; vertical-align: sub; color: #fff;"></label>
 </div>
 </div>  
	 <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>
     </div>
     </div>



        
        
        <div class="center">
            <table cellpadding="18" width="100%" height="50%" border="0">
                <tbody>
                    <tr>
                        <td class="border center formbg" style="height: 180px; width: 560px">
                            
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        
    </div>
    </form>
</body>
</html>
