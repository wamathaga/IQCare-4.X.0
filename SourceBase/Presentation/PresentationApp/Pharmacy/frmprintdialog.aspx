<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmprintdialog.aspx.cs" Inherits="PresentationApp.Pharmacy.frmprintdialog" %>
<%@ Register Src="usrctrlprintpharmacy.ascx" TagName="printdialogUserControl"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title></title>
     <style type="text/css">
     .container { width:450px; }
     </style>
    <script type="text/javascript">
        function doPrint() {
            var prtContent = document.getElementById('<%= pnlprintdialog.ClientID %>');
            var inputbutton = prtContent.getElementsByTagName("input");
            for (var i = 0; i < inputbutton.length; i++) {
                if (inputbutton[i].type.toString().toLowerCase() == "submit") {
                    document.getElementById(inputbutton[i].id).style.display = 'none';
                }
            }
            prtContent.border = 0; 
            var WinPrint = window.open('', '', 'left=100,top=100,width=1000,height=1000,toolbar=0,scrollbars=1,status=0,resizable=1');
            WinPrint.document.write(prtContent.outerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }

        function fnprint(pnl) {
            var printContent = document.getElementById(pnl);
            var inputbutton = printContent.getElementsByTagName("input");
            for (var i = 0; i < inputbutton.length; i++) {
                if (inputbutton[i].type.toString().toLowerCase() == "submit") {
                    document.getElementById(inputbutton[i].id).style.display='none';
                }
            }
            var windowUrl = 'about:blank';
            var uniqueName = new Date();
            var windowName = 'Print' + uniqueName.getTime();
            var printWindow = window.open(windowUrl, windowName, 'scrollbars=1,width=1050,height=800');
            printWindow.document.open();
            printWindow.document.write(printContent.innerHTML);
            printWindow.document.close();
            printWindow.focus();
            printWindow.print();
            printWindow.close();
        }
</script>
</head>
<body>

    <form id="form1" runat="server" method="post">
    <div id="divprintdialog"  style="padding: 50px;" class="container">
    <asp:Panel ID="pnlprintdialog" runat="server"></asp:Panel><br />
    <asp:Button ID="btnprint" runat="server" text="Print All Label" 
        onclick="btnprint_Click" />
    </div>
   
    </form>
</body>
</html>
