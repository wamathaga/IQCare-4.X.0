<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true" CodeBehind="frm_LaboratoryOrdered.aspx.cs" Inherits="PresentationApp.Laboratory.frm_LaboratoryOrdered" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
 
    <script language="javascript" type="text/javascript">
        function fnSetSession(url) {
            var result = frmFindAddPatient.SetPatientId_Session(url).value;
            window.location.href = result;
        }
        function fnSetSessionfamily(url) {
            var result = frmFindAddPatient.SetPatientIdFamily_Session(url).value;
            window.location.href = result;
        }

        function MakeStaticHeader(gridId, height, width, headerHeight, isFooter) {
            var tbl = document.getElementById(gridId);
            if (tbl) {
                var DivHR = document.getElementById('DivHeaderRow');
                var DivMC = document.getElementById('DivMainContent');
                var DivFR = document.getElementById('DivFooterRow');

                //*** Set divheaderRow Properties ****
                DivHR.style.height = headerHeight + 'px';
                DivHR.style.width = (parseInt(width) - 16) + 'px';
                DivHR.style.position = 'relative';
                DivHR.style.top = '0px';
                DivHR.style.zIndex = '10';
                DivHR.style.verticalAlign = 'top';

                //*** Set divMainContent Properties ****
                DivMC.style.width = width + 'px';
                DivMC.style.height = height + 'px';
                DivMC.style.position = 'relative';
                DivMC.style.top = -headerHeight + 'px';
                DivMC.style.zIndex = '1';

                //*** Set divFooterRow Properties ****
                DivFR.style.width = (parseInt(width) - 16) + 'px';
                DivFR.style.position = 'relative';
                DivFR.style.top = -headerHeight + 'px';
                DivFR.style.verticalAlign = 'top';
                DivFR.style.paddingtop = '2px';

                if (isFooter) {
                    var tblfr = tbl.cloneNode(true);
                    tblfr.removeChild(tblfr.getElementsByTagName('tbody')[0]);
                    var tblBody = document.createElement('tbody');
                    tblfr.style.width = '100%';
                    tblfr.cellSpacing = "0";

                    tblfr.border = "0px";
                    tblfr.rules = "none";
                    //*****In the case of Footer Row *******
                    tblBody.appendChild(tbl.rows[tbl.rows.length - 1]);
                    tblfr.appendChild(tblBody);
                    DivFR.appendChild(tblfr);
                }
                //****Copy Header in divHeaderRow****
                DivHR.appendChild(tbl.cloneNode(true));
            }
        }



        function OnScrollDiv(Scrollablediv) {

            //document.getElementById('DivHeaderRow').scrollLeft = Scrollablediv.scrollLeft;
            //document.getElementById('DivFooterRow').scrollLeft = Scrollablediv.scrollLeft;
        }
    </script>
    <div style="padding-top: 10px">
        <h1 class="nomargin" id="tHeading" runat="server" style="padding-left: 10px;">
            Laboratory Order Summary</h1>
        <div class="center" style="padding: 10px;">
            <div id="divshow" class="border formbg">
                <asp:UpdatePanel ID="up_patient" runat="server">
                    <ContentTemplate>
                        
                        <table class="center" cellspacing="6" cellpadding="0" width="100%" border="0">
                            <tbody>
                                <tr>
                                    <td class="center" align="center" style="padding-left: 10px; padding-right: 15px"
                                        colspan="2">
                                        <div class="grid">
                                            <div class="rounded">
                                                <%--<div class="top-outer">
                                                    <div class="top-inner">
                                                        <div class="top">
                                                            <h2>
                                                                Searched Result</h2>
                                                        </div>
                                                    </div>
                                                </div>--%>
                                                <div class="mid-outer">
                                                    <div class="mid-inner">
                                                        <div class="mid" style="cursor: pointer; height: 280px; overflow: auto;border: 1px solid #666699;">                                                           
                                                            <div id="div-gridview" class="whitebg" >
                                                               
                                                                <asp:GridView ID="grdSearchResult" runat="server" EnableViewState="False" Width="884"
                                                                    OnRowDataBound="grdSearchResult_RowDataBound" OnSorting="grdSearchResult_Sorting"
                                                                    PageSize="1" CssClass="table table-bordered table-hover" AutoGenerateColumns="False" CellPadding="0"
                                                                    CellSpacing="0" BorderWidth="0" GridLines="None" AllowSorting="True">

                                                                      <HeaderStyle CssClass="searchresultfixedheader" Height="20px"></HeaderStyle>
                                                                     <AlternatingRowStyle BackColor="White" BorderColor="Silver" />                      
                                                                </asp:GridView>
                                                            </div>
                                                            
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="bottom-outer">
                                                    <div class="bottom-inner">
                                                        <%--<div class="bottom">
                                                        </div>--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                       
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" align="center">
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnNewOrder" runat="server" Text=" New Order " 
                                                        onclick="btnNewOrder_Click" />
                                                </td>                                               
                                                <td>
                                                    <asp:Button ID="btnCancel" runat="server"  Text="Cancel" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>

                            </tbody>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnNewOrder"></asp:PostBackTrigger>
                        <asp:AsyncPostBackTrigger ControlID="grdSearchResult"></asp:AsyncPostBackTrigger>
                        <%--<asp:PostBackTrigger ControlID="btnAdd"></asp:PostBackTrigger>--%>
                        <asp:PostBackTrigger ControlID="btnCancel"></asp:PostBackTrigger>
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
