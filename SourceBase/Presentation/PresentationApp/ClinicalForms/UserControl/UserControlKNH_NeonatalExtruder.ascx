<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControlKNH_NeonatalExtruder.ascx.cs" Inherits="PresentationApp.ClinicalForms.UserControl.UserControlKNH_NeonatalExtruder" %>

<style type="text/css">
 .GridView tr th.narrow
{
    border: 1px solid #06c;
    color: White;
    padding: 0;    
}

.GridView tr td.narrow
{
    border: 1px solid #06c;
    color: #222;
    padding: 0;
}
.vScroll
{
    max-height: 500px;
    overflow-y: scroll;
}
</style>
<div style="height: 900px; background-color: White">
    <br />

<table width="100%" border="0">
<tr>
<td class="form">
<div class="GridView whitebg" style="cursor: pointer;">
    <div class="grid">
        <div class="rounded">
            <div class="top-outer">
                <div class="top-inner">
                    <div class="top center">
                        <h2>
                            Neonatal History</h2>
                    </div>
                </div>
            </div>
            <div class="mid-outer">
                <div class="mid-inner">
                    <div class="mid" style="height: auto; overflow: auto">
                        <div id="div-gridview" class="GridView whitebg vScroll">
                            <asp:GridView ID="grdNeonatal" runat="server" HeaderStyle-Wrap="true"
                                AutoGenerateColumns="False" Width="100%"  BorderWidth="0" 
                                GridLines="None" CssClass="table table-bordered table-hover" CellPadding="0" CellSpacing="0">
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>                               
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

</td>
</tr>
<tr>
<td class="form">
<div class="GridView whitebg" style="cursor: pointer;">
    <div class="grid">
        <div class="rounded">
            <div class="top-outer">
                <div class="top-inner">
                    <div class="top center">
                        <h2>
                            Maternal History</h2>
                    </div>
                </div>
            </div>
            <div class="mid-outer">
                <div class="mid-inner">
                    <div class="mid" style="height: auto; overflow:scroll">
                        <div id="div1" class="GridView whitebg vScroll">
                            <asp:GridView ID="grdMaternal" runat="server" 
                                AutoGenerateColumns="False" Width="100%"  BorderWidth="0"
                                GridLines="None" CssClass="table table-bordered table-hover" CellPadding="0" CellSpacing="0">
                                <HeaderStyle HorizontalAlign="Left" Font-Size="Small" Wrap="true"></HeaderStyle>                               
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

</td>
</tr>
<%--Add this row for display Milestones data on slider(Rahmat 09-Jan-2017)--%>
<tr>
<td class="form">
<div class="GridView whitebg" style="cursor: pointer;">
    <div class="grid">
        <div class="rounded">
            <div class="top-outer">
                <div class="top-inner">
                    <div class="top center">
                        <h2>
                            Milestone History</h2>
                    </div>
                </div>
            </div>
            <div class="mid-outer">
                <div class="mid-inner">
                    <div class="mid" style="height: auto; overflow:scroll">
                        <div id="div2" class="GridView whitebg vScroll">
                            <asp:GridView ID="gdrMilestone" runat="server" 
                                AutoGenerateColumns="False" Width="100%"  BorderWidth="0"
                                GridLines="None" CssClass="table table-bordered table-hover" CellPadding="0" CellSpacing="0">
                                <HeaderStyle HorizontalAlign="Left" Font-Size="Small" Wrap="true"></HeaderStyle>                              
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

</td>
</tr>
</table>
</div>


