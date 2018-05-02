<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmLaboratoryHistory.aspx.cs"
    Inherits="PresentationApp.Laboratory.frmLaboratoryHistory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        .GridPager a, .GridPager span
        {
            display: block;
            height: 15px;
            width: 15px;
            font-weight: bold;
            text-align: center;
            text-decoration: none;
        }
        .GridPager a
        {
            background-color: #f5f5f5;
            color: #969696;
            border: 1px solid #969696;
        }
        .GridPager span
        {
            background-color: #A1DCF2;
            color: #000;
            border: 1px solid #3AC0F2;
        }
        .rounded_corners
        {
            border: 1px solid #A1DCF2;
            -webkit-border-radius: 8px;
            -moz-border-radius: 8px;
            border-radius: 8px;
            overflow: hidden;
        }
        .rounded_corners td, .rounded_corners th
        {
            border: 1px solid #A1DCF2;
            font-family: Arial;
            font-size: 10pt;
            text-align: center;
        }
        .rounded_corners table table td
        {
            border-style: none;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $("[src*=plus]").live("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "images/minus.png");
        });
        $("[src*=minus]").live("click", function () {
            $(this).attr("src", "images/plus.png");
            $(this).closest("tr").next().remove();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server" style="vertical-align: center; horiz-align: center;">
    <div class="rounded_corners" style="overflow: auto;">
        <asp:GridView ID="gvLabHistory" runat="server" AutoGenerateColumns="false" AllowPaging="false"
            DataKeyNames="LabID" HeaderStyle-BackColor="#3c8dbc" HeaderStyle-ForeColor="White"
            RowStyle-BackColor="#e7e7e7" AlternatingRowStyle-BackColor="White" RowStyle-ForeColor="#3A3A3A"
            OnRowDataBound="OnRowDataBound" EmptyDataText="No Records Found.">
            <Columns>
                <asp:BoundField ItemStyle-Width="250px" DataField="LabNumber" HeaderText="Lab Number" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <img alt="" style="cursor: pointer" src="images/plus.png" />
                        <asp:GridView ID="gvLabResult" runat="server" AutoGenerateColumns="false" HeaderStyle-BackColor="#3c8dbc"
                            HeaderStyle-ForeColor="White" RowStyle-BackColor="#e7e7e7" AlternatingRowStyle-BackColor="White"
                            RowStyle-ForeColor="#3A3A3A">
                            <Columns>
                                <asp:BoundField ItemStyle-Width="150px" DataField="SubTestName" HeaderText="Test Name" />
                                <asp:BoundField ItemStyle-Width="150px" DataField="TestResults" HeaderText="Test Result" />
                                <asp:BoundField ItemStyle-Width="150px" DataField="Justification" HeaderText="Justification" />
                                <asp:BoundField ItemStyle-Width="250px" DataField="OrderedbyName" HeaderText="Ordered By" />
                                <asp:BoundField ItemStyle-Width="250px" DataField="ReportedbyName" HeaderText="Reported By" />
                            </Columns>
                        </asp:GridView>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle HorizontalAlign="Right" CssClass="GridPager" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
