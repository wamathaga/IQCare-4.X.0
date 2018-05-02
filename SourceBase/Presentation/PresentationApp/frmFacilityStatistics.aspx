<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    Inherits="frmFacilityHome" Title="Untitled Page" CodeBehind="frmFacilityStatistics.aspx.cs" %>

<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <!-- begin content area -->
    <%--<script language="javascript" type="text/javascript">
        function bgPmtct()
        {
            
            document.getElementById('<%=btnPMTCT.ClientID %>').style.backgroundColor ="White"; 
            document.getElementById('<%=btnART.ClientID %>').style.backgroundColor ="Silver";
            document.getElementById('<%=btnExposedChildren.ClientID %>').style.backgroundColor ="Silver";
           
        }
         function bgArt()
        {
            document.getElementById('<%=btnART.ClientID %>').style.backgroundColor ="White";
            document.getElementById('<%=btnPMTCT.ClientID %>').style.backgroundColor ="Silver"; 
            document.getElementById('<%=btnExposedChildren.ClientID %>').style.backgroundColor ="Silver";
        }
         function bgExposedChildren()
        {
            document.getElementById('<%=btnExposedChildren.ClientID %>').style.backgroundColor ="White";
            document.getElementById('<%=btnART.ClientID %>').style.backgroundColor ="Silver";
            document.getElementById('<%=btnPMTCT.ClientID %>').style.backgroundColor ="Silver"; 

        }
    </script>--%>
    <div>
        <h1 class="topmargin">
            Facility Statistics</h1>
        <table runat="server" cellspacing="0" cellpadding="0" style="width: 100%; padding-left: 10px;">
            <tr style="height: 35px;">
                <td colspan="3">
                    <div style="padding-left: 500px">
                        <asp:CheckBox ID="chkpreferred" Text="Preferred" runat="server" AutoPostBack="true"
                            OnCheckedChanged="chkpreferred_CheckedChanged" />
                        <label>
                            Facility/Satellite</label>
                        <asp:DropDownList ID="ddFacility" Width="221px" OnSelectedIndexChanged="ddFacility_SelectedIndexChanged"
                            AutoPostBack="true" runat="server" Height="16px">
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <table runat="server" width="100%" id="tblHIVCare" visible="false">
                        <tr>
                            <td>
                                <b>HIV Care </b>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <%--<telerik:radscriptmanager runat="server" id="RadScriptManager"/>--%>
                                <telerik:RadAjaxPanel ID="pnlnotification" runat="server"/>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 43%; height: 25px;">
                                <asp:LinkButton ID="lnkTotalActivePatients" runat="server" OnClick="hlTotalActivePatients_Click">Total Active Patients</asp:LinkButton>
                            </td>
                            <td class="blue">
                                <asp:Label ID="lblTotalActivePatients" CssClass="rightalign" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 43%; height: 25px;">
                                <asp:LinkButton ID="lnkActiveNonARTPatients" runat="server" OnClick="hlActiveNonARTPatients_Click">Active Non-ART Patients </asp:LinkButton>
                            </td>
                            <td class="blue">
                                <asp:Label ID="lblActiveNonARTPatients" CssClass="rightalign" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 43%; height: 25px;">
                                <asp:LinkButton ID="lnkActiveARTPatients" runat="server" OnClick="hlActiveARTPatient_Click">Active ART Patients </asp:LinkButton>
                            </td>
                            <td class="blue">
                                <asp:Label ID="lblActiveARTPatients" CssClass="rightalign" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 43%; height: 25px;">
                                <asp:LinkButton ID="hlLosttoFollowUp" runat="server" OnClick="hlLosttoFollowUp_Click">Lost to Follow up Patient list</asp:LinkButton>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 43%; height: 25px;">
                                <asp:LinkButton ID="hlartunknown" runat="server" OnClick="hlartunknown_Click">Due for Termination List:</asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 43%">
                            </td>
                            <td style="width: 60%; height: 25px;">
                                <asp:LinkButton ID="lnkmore" runat="server" OnClick="more_Click">more...</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <table runat="server" width="100%" id="tblPMTCTCare" visible="false">
                        <tr>
                            <td style="width: 43%; height: 25px;">
                                <b>PMTCT</b>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 43%; height: 25px;">
                                <asp:LinkButton ID="lnkCurrentMotherPMTCT" runat="server" OnClick="hllnkCurrentMotherPMTCT_Click">Current  Mothers in PMTCT</asp:LinkButton>
                            </td>
                            <td class="blue">
                                <asp:Label ID="lblCurrentMotherPMTCT" CssClass="rightalign" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 43%; height: 25px;">
                                <b>Current Number of Women on ARV Prophylaxis</b>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 43%; height: 25px;">
                                <asp:LinkButton ID="lnkANC" runat="server" OnClick="hllnkANC_Click">ANC</asp:LinkButton>
                            </td>
                            <td class="blue">
                                <asp:Label ID="lblANC" CssClass="rightalign" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 43%; height: 25px;">
                                <asp:LinkButton ID="lnkLD" runat="server" OnClick="hllnkLD_Click">L&D</asp:LinkButton>
                            </td>
                            <td>
                                <asp:Label ID="lblLD" CssClass="rightalign" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 43%; height: 25px;">
                                <asp:LinkButton ID="Postnatal" runat="server" OnClick="hllnkPostnatal_Click">Post Natal</asp:LinkButton>
                            </td>
                            <td class="blue">
                                <asp:Label ID="lblPostnatal" CssClass="rightalign" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 43%">
                            </td>
                            <td style="width: 60%; height: 25px;">
                                <asp:LinkButton ID="lnkmore1" runat="server" OnClick="hlmore1_Click">more...</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <table runat="server" width="100%" id="tblExpInfant" visible="false">
                        <tr>
                            <td style="width: 43%; height: 25px;">
                                <b>Exposed Infants</b>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 43%; height: 25px;">
                                <asp:LinkButton ID="lnkCurrentTotalExposedInfants" runat="server" OnClick="hllnkCurrentTotalExposedInfants_Click">Current Total Exposed Infants</asp:LinkButton>
                            </td>
                            <td class="blue">
                                <asp:Label ID="lblCurrentTotalExposedInfants" CssClass="rightalign" runat="server"
                                    Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 43%; height: 25px;">
                                <asp:LinkButton ID="lnkCurrentPMTCTInfants" runat="server" OnClick="hllnkCurrentPMTCTInfants_Click">Current PMTCT Infants</asp:LinkButton>
                            </td>
                            <td class="blue">
                                <asp:Label ID="lblCurrentPMTCTInfants" CssClass="rightalign" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 43%; height: 25px;">
                                <asp:LinkButton ID="lnkCurrentHIVCareInfants" runat="server" OnClick="hllnkCurrentHIVCareInfants_Click">Current HIV Care Infants</asp:LinkButton>
                            </td>
                            <td class="blue">
                                <asp:Label ID="lblCurrentHIVCareInfants" CssClass="rightalign" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 43%">
                                &nbsp;
                            </td>
                            <td style="width: 60%; height: 25px;">
                                <asp:LinkButton ID="lnkmore2" runat="server" OnClick="hlmore2_Click">more...</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <%--     <tr>
                    <td colspan="3">
                        <div style="background-color: Gray">
                            <input id="btnART" type="button" class="Tab" visible="false" runat="server" value="HIV Care" style="background-color:White"/>
                            <input id="btnPMTCT" type="button" class="Tab" visible="false" runat="server" value="PMTCT" />
                            <input id="btnExposedChildren" type="button" class="Tab" style="width:175px" visible="false" runat="server" value="Exposed Infants < 24 Months" />
                        </div>
                    </td>
                </tr>
                
                <tr >
                   <td colspan ="3">
                        <div id="trART" style=" width:100%" runat="server">
                        <table width ="100%">
                           <tbody>
                              <tr>
                                    <td align="center" valign="middle" style="width: 33%; height: 290px; border-top: #666699 1px solid;
                        border-right: #666699 1px solid; border-left: #666699 1px solid; border-bottom: #666699 1px solid;"
                        id="ART_1" >
                                <table class="bold" cellspacing="0" border="0" style="width: 90%">
                                    <tr>
                                        <td style="width:60%">
                                    <asp:LinkButton ID="lnkEverEnrolledPatients" runat="server" OnClick="hlEverEnrolledPatients_Click">Ever Enrolled Patients:</asp:LinkButton>
                                </td>
                                                                
                                        <td class="blue" align="right" style="width: 30%; height: 25px;">
                                    <asp:Label ID="lblTotalPatient" runat="server" Text="0"></asp:Label>
                                </td>
                                    </tr>
                                </table>
                        
                                <table class="bold" cellspacing="0" border="0" style="width: 90%">
                                    <tr>
                                        <td style="width:60%">
                                    <asp:LinkButton ID="lnkFemalesEnrolled" runat="server" OnClick="hlFemalesEnrolled_Click">Females Enrolled:</asp:LinkButton>
                                </td>                               
                                        <td class="blue" align="right" style="width: 30%; height: 25px;">
                                    <asp:Label ID="lblfemalepatient" runat="server" Text="0"></asp:Label>
                                </td>
                                    </tr>
                                </table>
                        
                                <table class="bold" cellspacing="0" border="0" style="width: 90%">
                                    <tr>
                                        <td style="width:60%">
                                    <asp:LinkButton ID="lnkMalesEnrolled" runat="server" OnClick="hlMalesEnrolled_Click">Males Enrolled:</asp:LinkButton>
                                </td>
                                
                                        <td class="blue" align="right" style="width: 30%; height: 25px">
                                    <asp:Label ID="lblmalepatient" runat="server" Text="0"></asp:Label>
                                </td>
                                    </tr>
                                    
                                    <tr>
                                        <td style="width:60%">
                                    <asp:LinkButton ID="lnkActivePatients" runat="server" OnClick="hlActivePatients_Click">Total Active Patients:</asp:LinkButton>
                                </td> 
                                                               
                                        <td class="blue" align="right" style="width: 30%; height: 25px;">
                                    <asp:Label ID="lblActivePatient" runat="server" Text="0"></asp:Label>
                                </td>
                                    </tr>
                                    
                                    <tr>
                                        <td style="width:60%">
                                        <asp:LinkButton ID="lnkActiveNonARTPatients" runat="server" OnClick="hlActiveNonARTPatients_Click">Active Non-ART Patients:</asp:LinkButton>
                                        </td> 
                                 
                                        <td class="blue" align="right" style="width: 30%; height: 25px;">
                                        <asp:Label ID="lblNonARTPatient" runat="server" Text="0"></asp:Label>
                                        </td>
                                    </tr>
                               </table>
                        
                                <table class="bold" cellspacing="0" border="0" style="width: 90%">
                                     <tr>
                                         <td style="width:60%">
                                    <asp:LinkButton ID="lnkActiveARTPatients" runat="server" OnClick="hlActiveARTPatients_Click">Active ART Patients:</asp:LinkButton>
                                </td>
                                         <td class="blue" align="right" style="width: 30%; height: 25px;">
                                    <asp:Label ID="lblArtPatients" runat="server" Text="0"></asp:Label>
                                </td>
                                    </tr>
                            
                                     <tr>
                                         <td style="width:60%">
                                    <asp:LinkButton ID="lnkARTMortality" runat="server" OnClick="hlARTMortality_Click">ART Mortality:</asp:LinkButton>
                                </td>
                                
                                         <td class="blue" align="right" style="width: 30%; height: 25px;">
                                    <asp:Label ID="lblARTMortality" runat="server" Text="0"></asp:Label>
                                </td>
                                     </tr> 
                                                                       
                                     <tr>
                                         <td style="width:60%;height: 25px;">
                                         <asp:LinkButton ID="hlLosttoFollowUp" runat="server" OnClick="hlLosttoFollowUp_Click">Lost to Follow up Patient list</asp:LinkButton>
                                         </td>
                                
                                      
                                    </tr>
                            
                                     <tr>
                                         <td style="width:60%;height: 25px;">
                                         <asp:LinkButton ID="hlartunknown" runat="server" OnClick="hlartunknown_Click">Due for Termination List:</asp:LinkButton>
                                         </td>
                                
                                        
                                    </tr>
                                </table>
                              </td>
                              
                                    <td align="center" valign="middle" style="width: 33%; height: 290px; border-right: #666699 1px solid;
                        border-top: #666699 1px solid; border-bottom: #666699 1px solid;"
                        id="ART_2">
                         <asp:Label ID="lblbd" runat="server" Font-Bold="True" Text="Non-ART Patient Breakdown by Age and Sex"></asp:Label><br />
                         <asp:Label ID="lblbdm" runat="server" Font-Bold="True" Text="Males"></asp:Label><br />
                                <table class="bold" cellspacing="0" border="0" style="width: 90%">
                                    <tr>
                                        <td style="width:60%">
                                    <asp:LinkButton ID="lnkNonARTMUpto2" runat="server" OnClick="hlNonARTMUpto2_Click">0-1 Years:</asp:LinkButton>
                                </td>
                                
                                        <td class="blue" align="right" width="10%" style="width: 30%; height: 25px">
                                        <asp:Label ID="lblNonARTMUpto2" runat="server" Text="0"></asp:Label>
                                        </td>
                                    </tr>
                            
                                    <tr>
                                         <td style="width:60%">
                                    <asp:LinkButton ID="lnkNonARTMUpto4" runat="server" OnClick="hlNonARTMUpto4_Click">2-4 Years:</asp:LinkButton>
                                </td>
                                
                                         <td class="blue" align="right" width="10%" style="width: 30%; height: 25px">
                                    <asp:Label ID="lblNonARTMUpto4" runat="server" Text="0"></asp:Label>
                                </td>
                                    </tr>
                            
                                    <tr>
                                        <td style="width:60%">
                                    <asp:LinkButton ID="lnkNonARTMUpto14" runat="server" OnClick="hlNonARTMUpto14_Click">5-14 Years:</asp:LinkButton>
                                </td>
                                
                                        <td class="blue" align="right" width="10%" style="width: 30%; height: 25px">
                                    <asp:Label ID="lblNonARTMUpto14" runat="server" Text="0"></asp:Label>
                                </td>
                                    </tr>
                            
                                    <tr>
                                        <td style="width:60%">
                                    <asp:LinkButton ID="lnkNonARTMAbove15" runat="server" OnClick="hlNonARTMAbove15_Click">15+ Years:</asp:LinkButton>
                                </td>
                                
                                        <td class="blue" align="right" width="10%" style="width: 30%; height: 25px">
                                    <asp:Label ID="lblNonARTMAbove15" runat="server" Text="0"></asp:Label>
                                </td>
                                    </tr>
                               </table>
                         <asp:Label ID="lblbdf" runat="server" Font-Bold="True" Text="Females"></asp:Label><br />
                                <table class="bold" cellspacing="0" border="0" style="width: 90%">
                            <tr>
                                <td style="width:60%">
                                    <asp:LinkButton ID="lnkNonARTFUpto2" runat="server" OnClick="hlNonARTFUpto2_Click">0-1 Years:</asp:LinkButton>
                                </td>
                                <td class="blue" align="right" width="10%" style="width: 30%; height: 25px">
                                    <asp:Label ID="lblNonARTFUpto2" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:60%">
                                    <asp:LinkButton ID="lnkNonARTFUpto4" runat="server" OnClick="hlNonARTFUpto4_Click">2-4 Years:</asp:LinkButton>
                                </td>
                                <td class="blue" align="right" width="10%" style="width: 30%; height: 25px">
                                    <asp:Label ID="lblNonARTFUpto4" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:60%">
                                    <asp:LinkButton ID="lnkNonARTFUpto14" runat="server" OnClick="hlNonARTFUpto14_Click">5-14 Years:</asp:LinkButton>
                                </td>
                                <td class="blue" align="right" width="10%" style="width: 30%; height: 25px">
                                    <asp:Label ID="lblNonARTFUpto14" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:60%">
                                    <asp:LinkButton ID="lnkNonARTFAbove15" runat="server" OnClick="hlNonARTFAbove15_Click">15+ Years:</asp:LinkButton>
                                </td>
                                <td class="blue" align="right" width="10%" style="width: 30%; height: 25px">
                                    <asp:Label ID="lblNonARTFAbove15" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    
                                    <td align="center" valign="middle" style="width: 34%; height: 290px; border-right: #666699 1px solid;
                        border-top: #666699 1px solid; border-bottom: #666699 1px solid;" id="ART_3">
                        <h3>
                        </h3>
                        
                            <asp:Label ID="Label6" runat="server" Font-Bold="True" Text="ART Patient Breakdown by Age and Sex"></asp:Label><br />
                            <asp:Label ID="Label10" runat="server" Font-Bold="True" Text="Males"></asp:Label><br />
                             <table class="bold" cellspacing="0" border="0" style="width: 90%">
                                 <tr>
                                    <td style="width:60%">
                                    <asp:LinkButton ID="lnkARTMUpto2" runat="server" OnClick="hlARTMUpto2_Click">0-1 Years:</asp:LinkButton>
                                </td>
                                
                                    <td class="blue" align="right" width="10%" style="width: 30%; height: 25px">
                                    <asp:Label ID="lblARTMUpto2" runat="server" Text="0"></asp:Label>
                                </td>
                                </tr>
                            
                                 <tr>
                                    <td style="width:60%">
                                    <asp:LinkButton ID="lnkARTMUpto4" runat="server" OnClick="hlARTMUpto4_Click">2-4 Years:</asp:LinkButton>
                                </td>
                                
                                    <td class="blue" align="right" width="10%" style="width: 30%; height: 25px">
                                    <asp:Label ID="lblARTMUpto4" runat="server" Text="0"></asp:Label>
                                </td>
                                </tr>
                            
                                 <tr>
                                    <td style="width:60%">
                                    <asp:LinkButton ID="lnkARTMUpto14" runat="server" OnClick="hlARTMUpto14_Click">5-14 Years:</asp:LinkButton>
                                </td>
                                
                                    <td class="blue" align="right" width="10%" style="width: 30%; height: 25px">
                                    <asp:Label ID="lblARTMUpto14" runat="server" Text="0"></asp:Label>
                                </td>
                               </tr>
                            
                                 <tr>
                                     <td style="width:60%">
                                    <asp:LinkButton ID="lnkARTMAbove15" runat="server" OnClick="hlARTMAbove15_Click">15+ Years:</asp:LinkButton>
                                </td>
                                
                                     <td class="blue" align="right" width="10%" style="width: 30%; height: 25px">
                                    <asp:Label ID="lblARTMAbove15" runat="server" Text="0"></asp:Label>
                                </td>
                               </tr>
                            </table>
                            <asp:Label ID="Label11" runat="server" Font-Bold="True" Text="Females"></asp:Label><br />
                             <table class="bold" cellspacing="0" border="0" style="width: 90%">
                                 <tr>
                                    <td style="width:60%">
                                    <asp:LinkButton ID="lnkARTFUpto2" runat="server" OnClick="hlARTFUpto2_Click">0-1 Years:</asp:LinkButton>
                                    </td>
                                
                                    <td class="blue" align="right" width="10%" style="width: 30%; height: 25px">
                                    <asp:Label ID="lblARTFUpto2" runat="server" Text="0"></asp:Label>
                                </td>
                                </tr>
                            
                                 <tr>
                                <td style="width:60%">
                                    <asp:LinkButton ID="lnkARTFUpto4" runat="server" OnClick="hlARTFUpto4_Click">2-4 Years:</asp:LinkButton>
                                </td>
                                <td class="blue" align="right" width="10%" style="width: 30%; height: 25px">
                                    <asp:Label ID="lblARTFUpto4" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>
                            
                                 <tr>
                                <td style="width:60%">
                                    <asp:LinkButton ID="lnkARTFUpto14" runat="server" OnClick="hlARTFUpto14_Click">5-14 Years:</asp:LinkButton>
                                </td>
                                <td class="blue" align="right" width="10%" style="width: 30%; height: 25px">
                                    <asp:Label ID="lblARTFUpto14" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>
                            
                                 <tr>
                                <td style="width:60%">
                                    <asp:LinkButton ID="lnkARTFAbove15" runat="server" OnClick="hlARTFAbove15_Click">15+ Years:</asp:LinkButton>
                                </td>
                                <td class="blue" align="right" width="10%" style="width: 30%; height: 25px">
                                    <asp:Label ID="lblARTFAbove15" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>
                            </table>                        
                                </td>
                              </tr>
                           </tbody> 
                        </table>  
                        </div>
                     
                        <div id="trPMTCT" style=" width:100%" runat="server">
                         <table width = "100%">
                           <tbody>
                             <tr>
                                <td align="center" valign="middle" style="width: 33%; height: 290px; border-top: #666699 1px solid;
                        border-right: #666699 1px solid; border-left: #666699 1px solid; border-bottom: #666699 1px solid;"
                        id="PMTCT_1">
                         <table class="bold" cellspacing="0" border="0" style="width: 90%">
                            <asp:Label ID="lblPMTCTEnroll" runat="server" Font-Bold="True" Text="PMTCT Enrolled"></asp:Label><br />
                            <tr>
                                <td style="width:75%">
                                    <asp:LinkButton ID="lnkMothersEverEnroll" runat="server" OnClick="hlMothersEverEnroll_Click">Cumulative Mothers Ever in PMTCT:</asp:LinkButton>
                                </td>
                                <td class="blue" align="right" style="height: 25px">
                                    <asp:Label ID="lblMothersEverEnroll" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>
                            
                            <tr>
                                <td style="width:80%">
                                    <asp:LinkButton ID="lnkCurrentMothers" runat="server" OnClick="hlCurrentMothers_Click">Current Mothers in PMTCT:</asp:LinkButton>
                                </td>
                                <td class="blue" align="right" style="height: 25px">
                                    <asp:Label ID="lblCurrentMothers" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>
                         </table><br />
                        
                         <table class="bold" cellspacing="0" border="0" style="width: 90%">
                            <asp:Label ID="lblCurrentWomenonARVPro" runat="server" Font-Bold="True" Text="Current Number Women on ARV Prophylaxis"></asp:Label><br />
                            <tr>
                                <td style="width:80%">
                                    <asp:LinkButton ID="lnkProANC" runat="server" OnClick="hlProANC_Click">ANC:</asp:LinkButton>
                                </td>
                                
                                <td class="blue" align="right" style="height: 25px">
                                    <asp:Label ID="lblProANC" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>
                            
                            <tr>
                                <td style="width:70%">
                                    <asp:LinkButton ID="lnkProLD" runat="server" OnClick="hlProLD_Click">L & D:</asp:LinkButton>
                                </td>
                                
                                <td class="blue" align="right" style="height: 25px">
                                    <asp:Label ID="lblProLD" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>
                            
                            <tr>
                                <td style="width:70%">
                                    <asp:LinkButton ID="lnkProPN" runat="server" OnClick="hlProPN_Click">Post Natal:</asp:LinkButton>
                                </td>
                                
                                <td class="blue" align="right" style="height: 25px">
                                    <asp:Label ID="lblProPN" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>
                        </table>
                                </td>
                    
                                <td id="PMTCT_2" align="center" valign="middle" style="height: 290px; border-right: solid 1px #666699; 
                        border-top: solid 1px #666699; border-bottom: solid 1px #666699;">
                        <asp:Label ID="lblHIVStatusDisCouple" runat="server" Font-Bold="True" Text="HIV Status and Discordant Couples"></asp:Label><br />
                        <table class="bold" cellspacing="0" border="0" style="width: 90%">
                            <tr>
                                <td colspan="3">
                                </td>
                                <td align="center" style="width:20%;height: 25px">
                                    <asp:Label ID="lblPartners" runat="server" Text="Partners"></asp:Label>
                                </td>
                                <td style="width:20%">
                                </td>
                            </tr>
                            
                            <tr>
                                <td colspan="2">
                                </td>
                                <td align="left" style="width:20%;height: 25px">
                                    <asp:Label ID="lblHIVPos" runat="server" Text="HIV+"></asp:Label>
                                </td>
                                <td align="left" style="width:20%;height: 25px">
                                    <asp:Label ID="lblHIVNeg" runat="server" Text="HIV-"></asp:Label>
                                </td>
                                <td align="left" style="width:20%;height: 25px">
                                    <asp:Label ID="lblUnknown" runat="server" Text="Unknown"></asp:Label>
                                </td>
                            </tr>
                            
                            <tr>
                                <td colspan="4" align="left">
                                <asp:Label ID="lblmothers" runat="server" Text="Mothers"></asp:Label>
                                </td>
                            </tr>
                            
                            <tr>
                                <td align="left" style="width:20%;height: 25px">
                                    <asp:Label ID="lblCurrentANC" runat="server" Text="Current ANC"></asp:Label>
                                </td>
                                <td align="left" style="width:20%;height: 25px">
                                    <asp:Label ID="lblCurrentANCMothers" runat="server" Text="0"></asp:Label>
                                </td>
                                <td colspan="3">
                                </td>
                            </tr>
                            
                            <tr>
                                <td align="left" style="width:20%;height: 25px">
                                    <asp:Label ID="lblCurrentANCHIVPosMothers" runat="server" Text="HIV+"></asp:Label>
                                </td>
                                <td align="left" style="height: 25px">
                                    <asp:Label ID="lblANCHIVPosMothers" runat="server" Text="0"></asp:Label>
                                </td>
                                <td align="left" style="width:20%">
                                    <asp:LinkButton ID="lnkPosMotherPosPartner" runat="server" OnClick="hlPosMotherPosPartner_Click" Text="0"></asp:LinkButton>
                                </td>
                                <td align="left" style="width:20%">
                                    <asp:LinkButton ID="lnkPosMotherNegPartner" runat="server" OnClick="hlPosMotherNegPartner_Click" Text="0"></asp:LinkButton>
                                </td>
                                <td align="left" style="width:20%">
                                    <asp:LinkButton ID="lnkPosMotherUnknownPartner" runat="server" OnClick="hlPosMotherUnknownPartner_Click" Text="0"></asp:LinkButton>
                                </td>
                            </tr>
                            
                            <tr>
                                <td align="left" style="width:20%;height: 25px">
                                    <asp:Label ID="lblCurrentHIVNegMothers" runat="server" Text="HIV-"></asp:Label>
                                </td>
                                <td align="left" style="height: 25px">
                                    <asp:Label ID="lblHIVNegMothers" runat="server" Text="0"></asp:Label>
                                </td>
                                <td align="left" style="width:20%">
                                    <asp:LinkButton ID="lnkNegMotherPosPartner" runat="server" OnClick="hlNegMotherPosPartner_Click" Text="0"></asp:LinkButton>
                                </td>
                                <td align="left" style="width:20%">
                                    <asp:LinkButton ID="lnkNegMotherNegPartner" runat="server" OnClick="hlNegMotherNegPartner_Click" Text="0"></asp:LinkButton>
                                </td>
                                <td align="left" style="width:20%">
                                    <asp:LinkButton ID="lnkNegMotherUnknownPartner" runat="server" OnClick="hlNegMotherUnknownPartner_Click" Text="0"></asp:LinkButton>
                                </td>
                            </tr>
                            
                        </table><br />
                                               
                        <table class="bold" cellspacing="0" border="0" style="width: 90%">
                            <tr>
                                <td  align="left" style="width:20%;height: 25px">
                                    <asp:Label ID="lblCurrentLD" runat="server" Text="Current L&D"></asp:Label>
                                </td>
                                <td align="left" style="width:20%;height: 25px">
                                    <asp:Label ID="lblCurrentLDMothers" runat="server" Text="0"></asp:Label>
                                </td>
                                <td colspan="3">
                                </td>
                            </tr>
                            
                            <tr>
                                <td align="left" style="width:20%;height: 25px">
                                    <asp:Label ID="lblLDHIVPos" runat="server" Text="HIV+"></asp:Label>
                                </td>
                                <td align="left" style="height: 25px">
                                    <asp:Label ID="lblLDHIVPosMothers" runat="server" Text="0"></asp:Label>
                                </td>
                                <td align="left" style="width:20%">
                                    <asp:LinkButton ID="lnkLDPosMotherPosPartner" runat="server" OnClick="hlLDPosMotherPosPartner_Click" Text="0"></asp:LinkButton>
                                </td>
                                <td align="left" style="width:20%">
                                    <asp:LinkButton ID="lnkLDPosMotherNegPartner" runat="server" OnClick="hlLDPosMotherNegPartner_Click" Text="0"></asp:LinkButton>
                                </td>
                                <td align="left" style="width:20%">
                                    <asp:LinkButton ID="lnkLDPosMotherUnknownPartner" runat="server" OnClick="hlLDPosMotherUnknownPartner_Click" Text="0"></asp:LinkButton>
                                </td>
                            </tr>
                            
                            <tr>
                                <td align="left" style="width:20%;height: 25px">
                                    <asp:Label ID="lblLDHIVNeg" runat="server" Text="HIV-"></asp:Label>
                                </td>
                                <td align="left" style="height: 25px">
                                    <asp:Label ID="lblLDHIVNegMothers" runat="server" Text="0"></asp:Label>
                                </td>
                                <td align="left" style="width:20%">
                                    <asp:LinkButton ID="lnkLDNegMotherPosPartner" runat="server" OnClick="hlLDNegMotherPosPartner_Click" Text="0"></asp:LinkButton>
                                </td>
                                <td align="left" style="width:20%">
                                    <asp:LinkButton ID="lnkLDNegMotherNegPartner" runat="server" OnClick="hlLDNegMotherNegPartner_Click" Text="0"></asp:LinkButton>
                                </td>
                                <td align="left" style="width:20%">
                                    <asp:LinkButton ID="lnkLDNegMotherUnknownPartner" runat="server" OnClick="hlLDNegMotherUnknownPartner_Click" Text="0"></asp:LinkButton>
                                </td>
                            </tr>
                            
                        </table><br />
                        
                        <table class="bold" cellspacing="0" border="0" style="width: 90%">
                            <tr>
                                <td align="left" style="width:20%;height: 25px">
                                    <asp:Label ID="lblCurrentPN" runat="server" Text="Current Post Natal"></asp:Label>
                                </td>
                                <td align="left" style="width:20%;height: 25px">
                                    <asp:Label ID="lblCurrentPNMothers" runat="server" Text="0"></asp:Label>
                                </td>
                                <td colspan="3">
                                </td>
                            </tr>
                            
                            <tr>
                                <td align="left" style="width:20%;height: 25px">
                                    <asp:Label ID="lblPNHIVPos" runat="server" Text="HIV+"></asp:Label>
                                </td>
                                <td align="left" style="height: 25px">
                                    <asp:Label ID="lblPNHIVPosMothers" runat="server" Text="0"></asp:Label>
                                </td>
                                <td align="left" style="width:20%">
                                    <asp:LinkButton ID="lnkPNPosMotherPosPartner" runat="server" OnClick="hlPNPosMotherPosPartner_Click" Text="0"></asp:LinkButton>
                                </td>
                                <td align="left" style="width:20%">
                                    <asp:LinkButton ID="lnkPNPosMotherNegPartner" runat="server" OnClick="hlPNPosMotherNegPartner_Click" Text="0"></asp:LinkButton>
                                </td>
                                <td align="left" style="width:20%">
                                    <asp:LinkButton ID="lnkPNPosMotherUnknownPartner" runat="server" OnClick="hlPNPosMotherUnknownPartner_Click" Text="0"></asp:LinkButton>
                                </td>
                            </tr>
                            
                            <tr>
                                <td align="left" style="width:20%;height: 25px">
                                    <asp:Label ID="lblPNHIVNeg" runat="server" Text="HIV-"></asp:Label>
                                </td>
                                <td align="left" style="height: 25px">
                                    <asp:Label ID="lblPNHIVNegMothers" runat="server" Text="0"></asp:Label>
                                </td>
                                <td align="left" style="width:20%">
                                    <asp:LinkButton ID="lnkPNNegMotherPosPartner" runat="server" OnClick="hlPNNegMotherPosPartner_Click" Text="0"></asp:LinkButton>
                                </td>
                                <td align="left" style="width:20%">
                                    <asp:LinkButton ID="lnkPNNegMotherNegPartner" runat="server" OnClick="hlPNNegMotherNegPartner_Click" Text="0"></asp:LinkButton>
                                </td>
                                <td align="left" style="width:20%">
                                    <asp:LinkButton ID="lnkPNNegMotherUnknownPartner" runat="server" OnClick="hlPNNegMotherUnknownPartner_Click" Text="0"></asp:LinkButton>
                                </td>
                            </tr>
                            
                        </table>
                                </td>
                             </tr>
                           </tbody>
                         </table>
                        </div>
                       
                        <div id="divExposedInfants" style=" width:100%" runat="server">
                         <table width = "100%">
                            <tbody>
                     <tr>
                       <td align="center" valign="middle" style="width: 30%; height: 290px; border-top: #666699 1px solid;
                        border-right: #666699 1px solid; border-left: #666699 1px solid; border-bottom: #666699 1px solid;"><br />
                        <table class="bold" cellspacing="0" border="0" style="width: 90%">
                            <tr>
                                <td style="width:80%">
                                    <asp:LinkButton ID="lnkExposedInfants" runat="server" OnClick="hlExposedInfants_Click">Cumulative Exposed Infants:</asp:LinkButton>
                                </td>
                                <td class="blue" align="right" style="height: 25px">
                                    <asp:Label ID="lblExposedInfants" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:80%">
                                    <asp:LinkButton ID="lnkCurrentExposedInfants" runat="server" OnClick="hlCurrentExposedInfants_Click">Current Total Exposed Infants:</asp:LinkButton>
                                </td>
                                <td class="blue" align="right" style="height: 25px">
                                    <asp:Label ID="lblCurrentExposedInfants" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>
                            
                            <tr>
                                <td style="width:80%">
                                    <asp:LinkButton ID="lnkCurrentPMTCTInfants" runat="server" OnClick="hlCurrentPMTCTInfants_Click">Current PMTCT Infants:</asp:LinkButton>
                                </td>
                                <td class="blue" align="right" style="height: 25px">
                                    <asp:Label ID="lblCurrentPMTCTInfants" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:80%">
                                    <asp:LinkButton ID="lnkCurrentHIVCareInfants" runat="server" OnClick="hlCurrentHIVCareInfants_Click">Current HIV Care Infants:</asp:LinkButton>
                                </td>
                                <td class="blue" align="right" style="height: 25px">
                                    <asp:Label ID="lblCurrentHIVCareInfants" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>
                        </table><br />
                        <asp:Label ID="lblProandTreatment" runat="server" Font-Bold="True" Text="ARV Prophylaxis and Treatment"></asp:Label><br /><br />
                        <table class="bold" cellspacing="0" border="0" style="width: 90%">
                        <tr>
                           <td style="width:80%">
                            <asp:LinkButton ID="lnkInfantsARVPro" runat="server" OnClick="hlInfantsARVPro_Click">Cumulative ARV Prophylaxis:</asp:LinkButton>
                           </td>
                                <td class="blue" align="right" style="height: 25px">
                                    <asp:Label ID="lblInfantsARVProphylaxis" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:80%">
                                    <asp:LinkButton ID="lnkInfantsCurrentProphy" runat="server" OnClick="hlInfantsCurrentProphylaxis_Click">Current ARV Prophylaxis:</asp:LinkButton>
                                </td>
                                <td class="blue" align="right" style="height: 25px">
                                    <asp:Label ID="lblInfantsCurrentProphylaxis" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:80%">
                                    <asp:LinkButton ID="lnkInfantsCumulativeARV" runat="server" OnClick="hlInfantsCumulativeARV_Click">Cumulative ARV Treatment:</asp:LinkButton>
                                </td>
                                <td class="blue" align="right" style="height: 25px">
                                    <asp:Label ID="lblInfantsCumulativeARV" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:80%">
                                    <asp:LinkButton ID="lnkInfantsCurrentARV" runat="server" OnClick="hlInfantsCurrentARV_Click">Current ARV Treatment:</asp:LinkButton>
                                </td>
                                <td class="blue" align="right" style="height: 25px">
                                    <asp:Label ID="lblInfantsCurrentARV" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>
                        </table><br />
                        <asp:Label ID="lblCotriProphy"  runat="server" Font-Bold="True" Text="Cotrimoxizole Prophylaxis"></asp:Label><br /><br />
                        <table class="bold" cellspacing="0" border="0" style="width: 90%">
                            <tr>
                           <td style="width:80%">
                            <asp:LinkButton ID="lnkContrimProCumulessthan2" runat="server" OnClick="hlContrimProCumulessthan2_Click">Cumulative Started < 2 Months :</asp:LinkButton>
                           </td>
                            <td class="blue" align="right" style="height: 25px">
                                <asp:Label ID="lblContrimProCumulessthan2" runat="server" Text="0"></asp:Label>
                            </td>
                         </tr>
                            <tr>
                                <td style="width:80%">
                                    <asp:LinkButton ID="lnkContrimProCurrentlessthan2" runat="server" OnClick="hlContrimProCurrentlessthan2_Click">Current Started < 2 Months :</asp:LinkButton>
                                </td>
                                <td class="blue" align="right" style="height: 25px">
                                    <asp:Label ID="lblContrimProCurrentlessthan2" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>
                            
                            <tr>
                                <td style="width:80%">
                                    <asp:LinkButton ID="lnkContrimProCumu2to24" runat="server" OnClick="hlContrimProCumu2to24_Click">Cumulative 2-24 Months :</asp:LinkButton>
                                </td>
                                <td class="blue" align="right" style="height: 25px">
                                    <asp:Label ID="lblContrimProCumu2to24" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:80%">
                                    <asp:LinkButton ID="lnkContrimProCurrent2to24" runat="server" OnClick="hlContrimProCurrent2to24_Click">Current 2-24 Months :</asp:LinkButton>
                                </td>
                                <td class="blue" align="right" style="height: 25px">
                                    <asp:Label ID="lblContrimProCurrent2to24" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                            <td colspan="2" style="height: 25px">
                            </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:LinkButton ID="lnkInfantsnotonContrim" runat="server" OnClick="hlInfantsnotonContrim_Click">Exposed Infants Not Yet on Cotrim:</asp:LinkButton>
                                </td>
                                
                            </tr>
                        </table><br />                                                         
                    </td>
                    
                       <td align="center" valign="middle" style="width: 70%; height: 290px; border-right: solid 1px #666699; 
                    border-top: solid 1px #666699; border-bottom: solid 1px #666699;">
                    <asp:Label ID="lblHIVStatusandfeedingoption" runat="server" Font-Bold="True" Text="HIV Status and Feeding Options"></asp:Label><br />
                        <table class="bold" cellspacing="0" border="0" style="width: 90%">
                            <tr>
                                 <td style="width:25%;height: 25px">
                                 </td>
                                 
                                 <td style="width:15%;height: 25px">                                   
                                 </td>
                                 
                                 <td align="left" style="width:15%;height: 25px">
                                 <asp:Label ID="lblEBF" runat="server" Text="EBF"></asp:Label>
                                 </td>
                         
                                 <td align="left" style="width:15%;height: 25px">
                                 <asp:Label ID="lblRF" runat="server" Text="RF"></asp:Label>
                                 </td> 
                         
                                 <td align="left" style="width:15%;height: 25px">
                                 <asp:Label ID="lblMF" runat="server" Text="MF"></asp:Label>
                                 </td>
                         
                                 <td align="left" style="width:15%;height: 25px">
                                 <asp:Label ID="lblOther" runat="server" Text="Other"></asp:Label>
                                 </td>                                 
                            </tr>
                        
                            <tr>
                                 <td style="width:25%;height: 25px">
                                 <asp:Label ID="lblPCRlessthan2" runat="server" Text="Age < 2 Months(PCR)"></asp:Label>
                                 </td>
                                 <td align="left" style="width:15%;height: 25px">
                                 <asp:Label ID="lblPCRLessthan2months" runat="server" Text="0"></asp:Label>
                                 </td>
                         
                                <td colspan="4">
                                </td>
                            </tr>
                       
                            <tr>
                                <td style="width:25%;height: 25px">
                                <asp:Label ID="lblPercentTested" runat="server" Text="Percent Tested"></asp:Label>
                                </td>                         
                                <td align="left" style="width:15%;height: 25px">
                                <asp:Label ID="lblPercentTestedResult" runat="server" Text="0"></asp:Label>
                                </td>
                                
                                <td colspan="4">
                                </td>
                            </tr>
                       
                            <tr>
                                 <td align="left" style="width:25%;height: 25px">
                                 <asp:Label ID="lblTestedResultHIVPos" runat="server" Text="HIV+"></asp:Label>
                                 </td>
                            
                                 <td align="left" style="width:15%;height: 25px">
                                 <asp:Label ID="lblTotalHIVPos" runat="server" Text="0"></asp:Label>                                
                                 </td>
                                                        
                                 <td align="left" style="width:15%;height: 25px">
                                 <asp:LinkButton ID="lnkHIVPosEBFlessthan2" runat="server" OnClick="hlHIVPosEBFlessthan2_Click" Text="0"></asp:LinkButton>
                                 </td>
                            
                                 <td align="left" style="width:15%;height: 25px">
                                 <asp:LinkButton ID="lnkHIVPosRFlessthan2" runat="server" OnClick="hlHIVPosRFlessthan2_Click" Text="0"></asp:LinkButton>
                                 </td>
                            
                                 <td align="left" style="width:15%;height: 25px">
                                 <asp:LinkButton ID="lnkHIVPosMFlessthan2" runat="server" OnClick="hlHIVPosMFlessthan2_Click" Text="0"></asp:LinkButton>
                                 </td>
                            
                                 <td align="left" style="width:15%;height: 25px">
                                 <asp:LinkButton ID="lnkHIVPosOtherlessthan2" runat="server" OnClick="hlHIVPosOtherlessthan2_Click" Text="0"></asp:LinkButton>
                                 </td>
                           </tr>
                          
                            <tr>
                                 <td align="left" style="width:25%;height: 25px">
                                 <asp:Label ID="lblPercentTestedHIVNeg" runat="server" Text="HIV-"></asp:Label>
                                 </td>
                            
                                 <td align="left" style="width:15%;height: 25px">
                                 <asp:Label ID="lblTotalHIVNeg" runat="server" Text="0"></asp:Label>
                                 </td> 
                                                       
                                 <td align="left" style="width:15%;height: 25px">
                                 <asp:LinkButton ID="lnkHIVNegEBFlessthan2" runat="server" OnClick="hlHIVNegEBFlessthan2_Click" Text="0"></asp:LinkButton>
                                 </td>
                            
                                 <td align="left" style="width:15%;height: 25px">
                                 <asp:LinkButton ID="lnkHIVNegRFlessthan2" runat="server" OnClick="hlHIVNegRFlessthan2_Click" Text="0"></asp:LinkButton>
                                 </td>
                            
                                 <td align="left" style="width:15%;height: 25px">
                                 <asp:LinkButton ID="lnkHIVNegMFlessthan2" runat="server" OnClick="hlHIVNegMFlessthan2_Click" Text="0"></asp:LinkButton>
                                 </td>
                            
                                 <td align="left" style="width:15%;height: 25px">
                                 <asp:LinkButton ID="lnkHIVNegOtherlessthan2" runat="server" OnClick="hlHIVNegOtherlessthan2_Click" Text="0"></asp:LinkButton>
                                 </td>
                          </tr> 
                                        
                            <tr>
                                 <td align="left" style="width:25%;height: 25px">
                                 <asp:Label ID="lblIndeterminate" runat="server" Text="Indeterminate"></asp:Label>
                                 </td>                                                        
                                 <td align="left" style="width:15%;height: 25px">
                                 <asp:Label ID="lblIndeterminateTested" runat="server" Text="0"></asp:Label>
                                 </td>
                                                        
                                 <td align="left" style="width:15%;height: 25px">
                                 <asp:LinkButton ID="lnkIndeterminateEBFlessthan2" runat="server" OnClick="hlIndeterminateEBFlessthan2_Click" Text="0"></asp:LinkButton>
                                 </td>
                            
                                 <td align="left" style="width:15%;height: 25px">
                                 <asp:LinkButton ID="lnkIndeterminateRFlessthan2" runat="server" OnClick="hlIndeterminateRFlessthan2_Click" Text="0"></asp:LinkButton>
                                  </td>
                            
                                 <td align="left" style="width:15%;height: 25px">
                                 <asp:LinkButton ID="lnkIndeterminateMFlessthan2" runat="server" OnClick="hlIndeterminateMFlessthan2_Click" Text="0"></asp:LinkButton>
                                 </td>
                            
                                 <td align="left" style="width:15%;height: 25px">
                                 <asp:LinkButton ID="lnkIndeterminateOtherlessthan2" runat="server" OnClick="hlIndeterminateOtherlessthan2_Click" Text="0"></asp:LinkButton>
                                 </td>
                           </tr> 
                                                                    
                        </table><br />
                                               
                        <table class="bold" cellspacing="0" border="0" style="width: 90%">
                           <tr>
                             <td style="width:25%;height: 25px">
                             <asp:Label ID="lblPCR2to12" runat="server" Text="Age 2-12 Months (PCR) "></asp:Label>
                             </td>
                             <td align="left" style="width:15%;height: 25px">
                             <asp:Label ID="lblPCR2to12months" runat="server" Text="0"></asp:Label>
                             </td>
                             
                             <td colspan="4">
                             </td>
                        </tr>
                        
                           <tr>
                             <td style="width:25%;height: 25px">
                             <asp:Label ID="lblPercentTested2to12" runat="server" Text="Percent Tested"></asp:Label>
                             </td>                             
                             <td align="left" style="width:15%;height: 25px">
                             <asp:Label ID="lblPercentTested2to12PCR" runat="server" Text="0"></asp:Label>
                             </td>
                             
                             <td colspan="4">
                             </td>
                          </tr>
                       
                           <tr>
                            <td align="left" style="width:25%;height: 25px">
                            <asp:Label ID="lblHIVPos2to12" runat="server" Text="HIV+"></asp:Label>
                            </td>                            
                            <td align="left" style="width:15%;height: 25px">
                            <asp:Label ID="lblTotalHIVPos2to12" runat="server" Text="0"></asp:Label>                                
                            </td> 
                                                       
                            <td align="left" style="width:15%;height: 25px">
                            <asp:LinkButton ID="lnkHIVPosEBF2to12" runat="server" OnClick="hlHIVPosEBF2to12_Click" Text="0"></asp:LinkButton>
                            </td>
                            
                            <td align="left" style="width:15%;height: 25px">
                            <asp:LinkButton ID="lnkHIVPosRF2to12" runat="server" OnClick="hlHIVPosRF2to12_Click" Text="0"></asp:LinkButton>
                            </td>
                            
                            <td align="left" style="width:15%;height: 25px">
                            <asp:LinkButton ID="lnkHIVPosMF2to12" runat="server" OnClick="hlHIVPosMF2to12_Click" Text="0"></asp:LinkButton>
                            </td>
                            
                            <td align="left" style="width:15%;height: 25px">
                            <asp:LinkButton ID="lnkHIVPosOther2to12" runat="server" OnClick="hlHIVPosOther2to12_Click" Text="0"></asp:LinkButton>
                            </td>
                            
                          </tr>
                          
                           <tr>
                             <td align="left" style="width:25%;height: 25px">
                             <asp:Label ID="lblHIVNeg2to12" runat="server" Text="HIV-"></asp:Label>
                             </td>                            
                             <td align="left" style="width:15%;height: 25px">
                             <asp:Label ID="lblTotalHIVNeg2to12" runat="server" Text="0"></asp:Label>                               
                             </td>
                                                        
                             <td align="left" style="width:15%;height: 25px">
                             <asp:LinkButton ID="lnkHIVNegEBF2to12" runat="server" OnClick="hlHIVNegEBF2to12_Click" Text="0"></asp:LinkButton>
                             </td>
                            
                             <td align="left" style="width:15%;height: 25px">
                             <asp:LinkButton ID="lnkHIVNegRF2to12" runat="server" OnClick="hlHIVNegRF2to12_Click" Text="0"></asp:LinkButton>
                             </td>
                            
                             <td align="left" style="width:15%;height: 25px">
                             <asp:LinkButton ID="lnkHIVNegMF2to12" runat="server" OnClick="hlHIVNegMF2to12_Click" Text="0"></asp:LinkButton>
                             </td>
                            
                             <td align="left" style="width:15%;height: 25px">
                             <asp:LinkButton ID="lnkHIVNegOther2to12" runat="server" OnClick="hlHIVNegOther2to12_Click" Text="0"></asp:LinkButton>
                            </td>                            
                          </tr> 
                                        
                           <tr>
                             <td align="left" style="width:25%;height: 25px">
                             <asp:Label ID="lblIndeterminate2to12" runat="server" Text="Indeterminate"></asp:Label>
                             </td>                            
                             <td align="left" style="width:15%;height: 25px">
                             <asp:Label ID="lblIndeterminateTested2to12" runat="server" Text="0"></asp:Label>                                
                             </td> 
                                                       
                             <td align="left" style="width:15%;height: 25px">
                             <asp:LinkButton ID="lnkIndeterminateEBF2to12" runat="server" OnClick="hlIndeterminateEBF2to12_Click" Text="0"></asp:LinkButton>
                             </td>
                            
                             <td align="left" style="width:15%;height: 25px">
                             <asp:LinkButton ID="lnkIndeterminateRF2to12" runat="server" OnClick="hlIndeterminateRF2to12_Click" Text="0"></asp:LinkButton>
                             </td>
                            
                             <td align="left" style="width:15%;height: 25px">
                             <asp:LinkButton ID="lnkIndeterminateMF2to12" runat="server" OnClick="hlIndeterminateMF2to12_Click" Text="0"></asp:LinkButton>
                             </td>
                            
                             <td align="left" style="width:15%;height: 25px">
                             <asp:LinkButton ID="lnkIndeterminateOther2to12" runat="server" OnClick="hlIndeterminateOther2to12_Click" Text="0"></asp:LinkButton>
                            </td>
                          </tr> 
                                                                    
                        </table><br />
                        
                        <table class="bold" cellspacing="0" border="0" style="width: 90%">
                         <tr>
                             <td style="width:25%;height: 25px">
                             <asp:Label ID="lbl18to24" runat="server" Text="Age 18-24 Months (Rapid Confirmatory)"></asp:Label>
                             </td>
                             <td align="left" style="width:15%;height: 25px">
                             <asp:Label ID="lbl18to24RConfirm" runat="server" Text="0"></asp:Label>
                             </td>
                             
                             <td colspan="4">
                             </td>
                        </tr>
                        
                         <tr>
                             <td style="width:25%;height: 25px">
                             <asp:Label ID="lblPercentTested18to24" runat="server" Text="Percent Tested"></asp:Label>
                             </td>
                             <td align="left" style="width:15%;height: 25px">
                             <asp:Label ID="lblPercentTested18to24months" runat="server" Text="0"></asp:Label>
                             </td>
                             <td colspan="4">
                             </td>
                       </tr>
                       
                         <tr>
                            <td align="left" style="width:25%;height: 25px">
                            <asp:Label ID="lblHIVPos18to24" runat="server" Text="HIV+"></asp:Label>
                            </td>                            
                            <td align="left" style="width:15%;height: 25px">
                            <asp:Label ID="lblTotalHIVPos18to24" runat="server" Text="0"></asp:Label>                                
                            </td> 
                                                       
                            <td align="left" style="width:15%;height: 25px">
                            <asp:LinkButton ID="lnkHIVPosEBF18to24" runat="server" OnClick="hlHIVPosEBF18to24_Click" Text="0"></asp:LinkButton>
                            </td>
                            
                            <td align="left" style="width:15%;height: 25px">
                            <asp:LinkButton ID="lnkHIVPosRF18to24" runat="server" OnClick="hlHIVPosRF18to24_Click" Text="0"></asp:LinkButton>
                            </td>
                            
                            <td align="left" style="width:15%;height: 25px">
                            <asp:LinkButton ID="lnkHIVPosMF18to24" runat="server" OnClick="hlHIVPosMF18to24_Click" Text="0"></asp:LinkButton>
                            </td>
                            
                            <td align="left" style="width:15%;height: 25px">
                            <asp:LinkButton ID="lnkHIVPosOther18to24" runat="server" OnClick="hlHIVPosOther18to24_Click" Text="0"></asp:LinkButton>
                            </td>
                          </tr>
                          
                         <tr>
                            <td align="left" style="width:25%;height: 25px">
                            <asp:Label ID="lblHIVNeg18to24" runat="server" Text="HIV-"></asp:Label>
                            </td>
                            
                            <td align="left" style="width:15%;height: 25px">
                            <asp:Label ID="lblTotalHIVNeg18to24" runat="server" Text="0"></asp:Label>                                
                            </td>
                                                        
                            <td align="left" style="width:15%;height: 25px">
                            <asp:LinkButton ID="lnkHIVNegEBF18to24" runat="server" OnClick="hlHIVNegEBF18to24_Click" Text="0"></asp:LinkButton>
                            </td>
                            
                            <td align="left" style="width:15%;height: 25px">
                            <asp:LinkButton ID="lnkHIVNegRF18to24" runat="server" OnClick="hlHIVNegRF18to24_Click" Text="0"></asp:LinkButton>
                            </td>
                            
                            <td align="left" style="width:15%;height: 25px">
                            <asp:LinkButton ID="lnkHIVNegMF18to24" runat="server" OnClick="hlHIVNegMF18to24_Click" Text="0"></asp:LinkButton>
                            </td>
                            
                            <td align="left" style="width:15%;height: 25px">
                            <asp:LinkButton ID="lnkHIVNegOther18to24" runat="server" OnClick="hlHIVNegOther18to24_Click" Text="0"></asp:LinkButton>
                            </td>
                          </tr> 
                                        
                         <tr>
                            <td align="left" style="width:25%;height: 25px">
                             <asp:Label ID="lblIndeterminate18to24" runat="server" Text="Indeterminate"></asp:Label>
                            </td>                            
                            <td align="left" style="width:15%;height: 25px">
                            <asp:Label ID="lblIndeterminateTested18to24" runat="server" Text="0"></asp:Label>                                
                            </td>
                                                        
                            <td align="left" style="width:15%;height: 25px">
                            <asp:LinkButton ID="lnkIndeterminateEBF18to24" runat="server" OnClick="hlIndeterminateEBF18to24_Click" Text="0"></asp:LinkButton>
                            </td>
                            
                            <td align="left" style="width:15%;height: 25px">
                            <asp:LinkButton ID="lnkIndeterminateRF18to24" runat="server" OnClick="hlIndeterminateRF18to24_Click" Text="0"></asp:LinkButton>
                            </td>
                            
                            <td align="left" style="width:15%;height: 25px">
                            <asp:LinkButton ID="lnkIndeterminateMF18to24" runat="server" OnClick="hlIndeterminateMF18to24_Click" Text="0"></asp:LinkButton>
                            </td>
                            
                            <td align="left" style="width:15%;height: 25px">
                            <asp:LinkButton ID="lnkIndeterminateOther18to24" runat="server" OnClick="hlIndeterminateOther18to24_Click" Text="0"></asp:LinkButton>
                            </td>
                          </tr>                                              
                        </table>
                    </td>                    
                 </tr>
                  </tbody>
                        </table>
                        </div>
                  </td>
               </tr>
                
                <tr>
                    <td style="height: 10px">
                    </td>
                </tr>
                
                <tr>
                    <td colspan="3">
                        <div id="divARTGraph" style=" width:100%" runat="server">
                            <table width="100%">
                                <tbody>
                                     <tr>
                                        <td class="pad18" align="center" valign="middle" width="30%" style="height: 100px;
                                         border-top: #666699 1px solid; border-left: #666699 1px solid; border-bottom: #666699 1px solid">
                                        <h3>Percent Males and Females Enrolled</h3>
                                        <asp:PlaceHolder ID="ChartHolder" runat="server"></asp:PlaceHolder>
                                        </td>
                                        <td class="pad18" align="center" valign="middle" width="30%" style="height: 100px;
                                         border-top: #666699 1px solid; border-bottom: #666699 1px solid">
                                        <h3>Percent ART and Non ART</h3>
                                        <asp:PlaceHolder ID="ChartHolder1" runat="server"></asp:PlaceHolder>
                                        </td>
                                        <td class="pad18" align="center" valign="middle" width="30%" style="height: 100px;
                                         border-right: #666699 1px solid; border-top: #666699 1px solid; border-bottom: #666699 1px solid">
                                         <h3>Percent ART by Age</h3>
                                         <asp:PlaceHolder ID="ChartHolder2" runat="server"></asp:PlaceHolder>
                                         </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </td>
                </tr>--%>
            <tr>
                <td colspan="3" align="left">
                    <asp:Panel ID="pnl_FacTexhAreas" runat="server" Width="100%">
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>
    <div id="divappoint" align="center" runat="server">
        <table class="center" width="100%">
            <tr>
                <td width="33%">
                    <a class="large" id="DirectScheduler" runat="server">Today's Appointments</a>
                </td>
                <td width="33%">
                </td>
                <td width="33%">
                    <a class="large" id="MissedScheduler" runat="server">Missed Appointments</a>
                </td>
            </tr>
        </table>
        <br />
    </div>
</asp:Content>
