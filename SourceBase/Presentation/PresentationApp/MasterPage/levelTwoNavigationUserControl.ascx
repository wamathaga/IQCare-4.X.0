<%@ Control Language="C#" AutoEventWireup="true" Inherits="MasterPage_levelTwoNavigationUserControl"
    CodeBehind="levelTwoNavigationUserControl.ascx.cs" %>
<%@ Register Src="~/ClinicalForms/UserControl/UserControlKNH_Extruder.ascx" TagName="UserControlKNH_Extruder"
    TagPrefix="ucExtruder" %>
<%@ Register Src="../ClinicalForms/UserControl/UserControl_Alerts.ascx" TagName="UserControl_Alerts"
    TagPrefix="uc1" %>
<style type="text/css">
    .MenuLevel2
    {
        font-size: 13px;
        font-weight: bold;
        text-align: center; /* set width if needed.*/
        width: 100%;
    }
    
    .MenuLevel2hover
    {
        color: #777;
        background-color: #E1E3E9;
        text-decoration: none;
        width: 100%;
    }
    
    .SubMenu
    {
        text-decoration: none;
        color: #777;
        background-color: #fff;
        padding-left: 15px;
        padding-right: 15px;
        padding-bottom: 15px;
        padding-top: 10px;
        border: 1px solid #eee;
        font-family: sans-serif;
        -webkit-text-size-adjust: 100%;
        -ms-text-size-adjust: 100%;
        font-size: 14px;
        border-radius: 5px;
        overflow: hidden;
    }
    
    .SubMenu a
    {
        color: #777;
    }
    
    .PharmacyDispensing
    {
        font-size: 12px;
        font-weight: bold;
        padding: 10px;
        color: #777;
        font-family: sans-serif;
        -webkit-text-size-adjust: 100%;
        -ms-text-size-adjust: 100%;
    }
    
    .PharmacyDispensinghover
    {
        color: #777;
        background-color: #E1E3E9;
        text-decoration: none;
        width: 100%;
    }
    
    .PharmacyDispensing a
    {
        color: #777;
    }
    .hrFor2ndMenu
    {
        display: block;
        height: 0px;
        border: 0;
        border-top: 1px solid #ccc;
        margin: 1em 0;
        padding: 0;
    }
</style>
<div runat="server" class="container-fluid" id="divmenu2">
    <script type="text/javascript">
        // set the destination textbox value with the ContactName
        function CallSuccess(res, destCtrl) {
            //var dest = document.getElementById(destCtrl);
            //dest.value = res;
        }

        // alert message on some failure
        function CallFailed(res, destCtrl) {
            alert(res.get_message());
        }

        function ClearSession() {

            MasterPage_levelTwoNavigationUserControl.SetPatientId_Session();
        }

        function fnSetformID(id) {
            MasterPage_levelTwoNavigationUserControl.SetDynamic_Session(id);


        }
        function openBluecard() {
            window.open('../Reports/frmPatientBlueCart.aspx?name=Add&PatientId=' + '<%#PatientId.ToString()%>' + '&ReportName=bluecart' + '&sts=0', 'bluecart', 'toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=no,resize=no,width=700,height=350,scrollbars=yes');

        }

        function openClinicalSummary() {
            window.open('../Reports/frmClinical_PatientSummary.aspx', 'popupwindow', 'toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=yes,resizable=no,width=950,height=650,scrollbars=yes');
        }

        function setFeatureID(id) {
            var menuTable = document.getElementById("<%#patientLevelMenu.ClientID%>");  //specify your menu id instead of Menu1
            if (menuTable != null) {
                var menuLinks = menuTable.getElementsByTagName("a");
                for (i = 0; i < menuLinks.length; i++) {
                    menuLinks[i].onclick = function () { return confirm("u sure to postback?"); }
                }
                setOnClickForNextLevelMenuItems(menuTable.nextSibling, id);
            }
        }
        function setOnClickForNextLevelMenuItems(currentMenuItemsContainer, id1) {

            var id = currentMenuItemsContainer.id;
            var len = id.length;
            if (id != null && typeof (id) != "undefined" && id.substring(0, parseInt(len) - 7) == "<%#patientLevelMenu.ClientID %>" && id.substring(parseInt(len) - 5, parseInt(len)) == "Items") {
                var subMenuLinks = currentMenuItemsContainer.getElementsByTagName("a");
                for (i = 0; i < subMenuLinks.length; i++) {
                    MasterPage_levelTwoNavigationUserControl.SetDynamic_Session(id1);
                }
                setOnClickForNextLevelMenuItems(currentMenuItemsContainer.nextSibling, id1);
            }
        }
        function openWaitingList() {//ope ning patients waiting list
            window.open("../ClinicalForms/frmPatientWaitingList.aspx", 'popupwindow', 'toolbars=no,location=no,directories=no,dependent=yes,top=150,left=150,maximize=yes,resizable=no,width=800,height=500,scrollbars=yes');
        }
        function openDrugHistory() {
            window.open('../PharmacyDispense/frmPharmacy_DrugHistory.aspx', '_blank', 'height=500,width=1100,scrollbars=yes');
        }
        function openLabHistory() {
            window.open('../Laboratory/frmLaboratoryHistory.aspx', '_blank', 'height=500,width=1100,scrollbars=yes');
        }
        //
        function PopupCenterL2(title, w, h) {
            // Fixes dual-screen position Most browsers Firefox            
            var dualScreenLeft = window.screenLeft != undefined ? window.screenLeft : screen.left;
            var dualScreenTop = window.screenTop != undefined ? window.screenTop : screen.top;

            width = window.innerWidth ? window.innerWidth : document.documentElement.clientWidth ? document.documentElement.clientWidth : screen.width;
            height = window.innerHeight ? window.innerHeight : document.documentElement.clientHeight ? document.documentElement.clientHeight : screen.height;

            var left = ((width / 2) - (w / 2)) + dualScreenLeft;
            var top = ((height / 2) - (h / 2)) + dualScreenTop;

            var myurl = "../ClinicalForms/frmNAFDAC.aspx";
            var newWindow = window.open(myurl, title, 'scrollbars=yes, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
            // Puts focus on the newWindow
            if (window.focus) {
                newWindow.focus();
            }
        }         
    </script>
</div>
<div class="navbar bg-default">
    <%--<div class="navbar-inner">--%>
    <div class="container-fluid" style="border-bottom: 1px solid #6CF; background-color: #F8F8F8;">
        <!-- .navbar-header -->
        <!-- Everything you want hidden at 940px or less, place within here -->
        <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
            <asp:Menu ID="patientLevelMenu" runat="server" OnMenuItemClick="patientLevelMenu_MenuItemClick1"
                Orientation="Horizontal" RenderingMode="Table" CssClass="nav navbar-nav text-muted MenuLevel2"
                StaticMenuStyle-CssClass="nav" StaticSelectedStyle-CssClass="active" DynamicMenuStyle-CssClass="dropdown-menu">
                <StaticMenuItemStyle Height="40px" />
                <DynamicMenuItemStyle CssClass="Menu" Height="25px" HorizontalPadding="5px" />
                <DynamicHoverStyle CssClass="MenuLevel2hover" />
                <DynamicMenuStyle CssClass="SubMenu" />
                <Items>
                    <asp:MenuItem Text="<i class='fa fa-pencil-square-o fa-lg text-muted' aria-hidden='true'></i> <span class='text-muted'>Registration</span>"
                        Selectable="True" Value="mnuRegistration" NavigateUrl="~/frmPatientCustomRegistration.aspx">
                    </asp:MenuItem>
                    <asp:MenuItem Text="<i class='fa fa-file-text-o fa-lg text-muted' aria-hidden='true'></i> <span class='text-muted'>Create New Form</span>"
                        Selectable="False" Value="mnuCreateNewForm">
                        <asp:MenuItem Text="Treatment Preparation" Value="mnuTreatmentPreparation" NavigateUrl="~/HIVCE/TreatmentPreparation.aspx?add=0">
                        </asp:MenuItem>
                        <asp:MenuItem Text="Alcohol Depression Screening" Value="mnuAlcoholDepressionScreening"
                            NavigateUrl="~/HIVCE/AlcoholDepressionScreening.aspx?add=0"></asp:MenuItem>
                        <asp:MenuItem Text="ART Readiness Assessment" Value="mnuARTReadinessAssessment" NavigateUrl="~/HIVCE/ARTReadinessAssessment.aspx?add=0">
                        </asp:MenuItem>
                        <asp:MenuItem Text="Adherence Barriers" Value="mnuAB" NavigateUrl="~/Adherence/AdherenceBarriers.aspx?add=0" />
                        <asp:MenuItem Text="Enhance Adherence Counselling" Value="mnuEAC" NavigateUrl="~/Adherence/EnhanceAdherence.aspx?add=0" />

                        <asp:MenuItem Text="PrEP" Value="mnuPrEP" NavigateUrl="~/PrEP/PrEP.aspx" />

                        <asp:MenuItem Text="Transition" Value="mnuTransition" NavigateUrl="~/HIVCE/Transition.aspx?add=0">
                        </asp:MenuItem>
                        <asp:MenuItem Text="<span class='hrFor2ndMenu'></span>"></asp:MenuItem>
                        <asp:MenuItem Text="Pharmacy" Value="mnuPharmacy" NavigateUrl="../PharmacyDispense/frmPharmacyDispense_PatientOrder.aspx" />
                        <asp:MenuItem Text="Order Labs" Value="mnuLabOrderDynm" />
                        <asp:MenuItem Text="<span class='hrFor2ndMenu'></span>"></asp:MenuItem>
                        <asp:MenuItem Text="DCC Adult Initial Evaluation" Value="mnuknhDCCadultIE" NavigateUrl="~/ClinicalForms/frmClinical_KNH_AdultIE.aspx" />
                        <asp:MenuItem Text="DCC Revised Adult Follow up Form" Value="mnuknhDCCadultFU" NavigateUrl="~/ClinicalForms/frmClinical_RevisedAdultfollowup.aspx" />
                        <asp:MenuItem Text="Nigeria Adult Initial Evaluation" Value="mnunigadultIE" NavigateUrl="~/ClinicalForms/frmClinical_Nigeria_AdultIE.aspx" />
                        <asp:MenuItem Text="Nigeria Paediatric Initial Evaluation" Value="mnunigPaedIE" NavigateUrl="~/ClinicalForms/frmClinical_Nigeria_PeadIE.aspx" />
                        <asp:MenuItem Text="Nigeria Initial Visit" Value="mnunigInitVisit" NavigateUrl="~/ClinicalForms/frmClinical_Nigeria_InitialVisit.aspx" />
                        <asp:MenuItem Text="PEP" Value="mnuknhpep" NavigateUrl="~/ClinicalForms/frmClinical_KNH_PEP.aspx" />
                        <asp:MenuItem Text="Adult Initial Evaluation" Value="mnuknhadultIE" NavigateUrl="~/ClinicalForms/frmClinical_KNH_AdultIE.aspx" />
                        <asp:MenuItem Text="Paediatric Initial Evaluation" Value="mnuknhPaediatricIE" NavigateUrl="~/ClinicalForms/frmClinical_KNH_Paediatric_IE.aspx" />
                        <asp:MenuItem Text="Initial Evaluation" Value="mnuInitEval" NavigateUrl="~/ClinicalForms/frmClinical_InitialEvaluation.aspx" />
                        <asp:MenuItem Text="ART Follow-up" Value="mnuFollowupART" NavigateUrl="~/ClinicalForms/frmClinical_ARTFollowup.aspx" />
                        <asp:MenuItem Text="Non-ART Follow-up" Value="mnuNonARTFollowUp" NavigateUrl="~/ClinicalForms/frmClinical_NonARTFollowUp.aspx" />
                        <asp:MenuItem Text="Lab Sample Collection & Results" Value="mnuLabTest" NavigateUrl="~/Laboratory/frm_LabTestResults.aspx" />
                        <asp:MenuItem Text="Home Visit" Value="mnuHomeVisit" NavigateUrl="~/Scheduler/frmScheduler_HomeVisit.aspx" />
                        <asp:MenuItem Text="Prior ART/HIV Care" Value="mnuPriorARTHIVCare" />
                        <asp:MenuItem Text="ART Card Visitation" Value="mnuARTCardVisitation" NavigateUrl="~/ClinicalForms/frmClinical_Nigeria_ARTCareVisitation.aspx" />
                        <asp:MenuItem Text="ART Card Summary" Value="mnuARTCardSummary" NavigateUrl="~/ClinicalForms/frmClinical_Nigeria_ARTCareSummary.aspx" />
                        <%--<asp:MenuItem Text="HEI Form" Value="mnuHEIForm" NavigateUrl="~/ClinicalForms/frmClinical_KNH_HEI.aspx" />--%>
                        <asp:MenuItem Text="HEI Form" Value="mnuHEIForm" NavigateUrl="~/ClinicalForms/frmClinical_KNH_HEI_New.aspx" />
                        <asp:MenuItem Text="ANC Postnatal Form" Value="mnuMEIForm" NavigateUrl="~/ClinicalForms/frmClinical_ANC.aspx" />
                        <asp:MenuItem Text="ART Care" Value="mnuARTCare"></asp:MenuItem>
                        <asp:MenuItem Text="HIV Care/ART Encounter" Value="mnuHIVCareARTEncounter"></asp:MenuItem>
                        <asp:MenuItem Text="ART History" Value="mnuARTHistory"></asp:MenuItem>
                        <asp:MenuItem Text="ART Therapy" Value="mnuARTTherapy"></asp:MenuItem>
                        <asp:MenuItem Text="Initial and Follow up Visits" Value="mnuARTVisit" />
                        <asp:MenuItem Text="Adult Follow up" Value="mnuRvsdAdult" />
                        <asp:MenuItem Text="Paediatric Follow up" Value="mnuPaediatricFollowup" />
                        <asp:MenuItem Text="Express" Value="mnuRvsdExpressForm" />
                        <asp:MenuItem NavigateUrl="~/ClinicalForms/frmPatientConsumables.aspx" Text="Consumables Issuance"
                            Value="mnuConsumablesIssuance" />
                        <asp:MenuItem Text="Clinical Encounter" Value="mnuHIVCE" NavigateUrl="~/HIVCE/ClinicalEncounter.aspx?add=0" />
                        <asp:MenuItem Text="Refill Encounter Form" Value="mnuREF" NavigateUrl="~/HIVCE/RefillEncounterForm.aspx?add=0" />
                    </asp:MenuItem>
                    <asp:MenuItem Text="<i class='fa fa-exchange text-muted fa-lg' aria-hidden='true'></i><span class='text-muted'>Additional Forms</span>"
                        Value="Additional Forms" Selectable="False">
                        <asp:MenuItem Text="Family Information" Value="mnuFamilyInformation"></asp:MenuItem>
                        <asp:MenuItem Text="Patient Classification" Value="mnuPatientClassification"></asp:MenuItem>
                        <asp:MenuItem Text="Follow-up Education" Value="mnuFollowupEducation"></asp:MenuItem>
                        <asp:MenuItem Text="Exposed Infant Follow-up" Value="mnuExposedInfant"></asp:MenuItem>
                        <asp:MenuItem Text="Allergy Information" Value="mnuAllergyInformation"></asp:MenuItem>
                        <asp:MenuItem Text="ADR Reporting" Value="mnuNAFDAC"></asp:MenuItem>
                        <asp:MenuItem Text="Free Text" Value="mnuFreeText"></asp:MenuItem>
                        <asp:MenuItem Text="Clinical Summary" Value="mnuClinicalSummaryAdd"></asp:MenuItem>
                        <asp:MenuItem Text="Client Intake Form" Value="mnuCIF" NavigateUrl="~/HTS/ClientIntakeForm.aspx" />
                    </asp:MenuItem>
                    <asp:MenuItem Text="<i class='fa fa-folder-open text-muted fa-lg' aria-hidden='true'></i> <span class='text-muted'>Existing Forms<span>"
                        Value="mnuExistingForms"></asp:MenuItem>
                    <asp:MenuItem Text="<i class='fa fa-times-circle text-muted fa-lg' aria-hidden='true'></i> <span class='text-muted'>Delete Form<span>"
                        Value="mnuClinicalDeleteForm"></asp:MenuItem>
                    <asp:MenuItem Text="<i class='fa fa-paperclip text-muted fa-lg' aria-hidden='true'></i> <span class='text-muted'>Patient Reports<span>"
                        Selectable="False">
                        <asp:MenuItem Text="Patient ARV Pick-up" Value="mnuDrugPickUp"></asp:MenuItem>
                        <asp:MenuItem Text="HIV Care Patient Profile" Value="mnuPatientProfile"></asp:MenuItem>
                        <asp:MenuItem Text="Patient Blue Card" Value="mnuPatientBlueCard" NavigateUrl="javascript:openBluecard();"
                            Target="_self"></asp:MenuItem>
                        <asp:MenuItem Text="Debit Note" Value="mnuDebitNote"></asp:MenuItem>
                        <%-- <asp:MenuItem Text="Patient Profile Summary" Value="mnuClinicalSummary" NavigateUrl="javascript:openClinicalSummary();"
                            Target="_self"></asp:MenuItem>--%>
                        <asp:MenuItem Text="Patient Profile Summary" Value="mnuClinicalSummary"></asp:MenuItem>
                        <asp:MenuItem Text="Drug History" Value="mnuDrugHistory" NavigateUrl="javascript:openDrugHistory();"
                            Target="_self"></asp:MenuItem>
                        <asp:MenuItem Text="Lab History" Value="mnuLabHistory" NavigateUrl="javascript:openLabHistory();"
                            Target="_self"></asp:MenuItem>
                        <asp:MenuItem Text="Clinical Encounter OI/Chronic Disease" Value="mnuCEPS" NavigateUrl="~/Reports/frmPatientCEOIChronicDisease.aspx">
                        </asp:MenuItem>
                    </asp:MenuItem>
                    <asp:MenuItem Text="<i class='fa fa-clock-o text-muted fa-lg' aria-hidden='true'></i> <span class='text-muted'>Appointments</span>"
                        Value="mnuScheduleAppointment"></asp:MenuItem>
                    <asp:MenuItem Text="<i class='glyphicon glyphicon-transfer text-muted fa-lg' aria-hidden='true'></i> <span class='text-muted'>Transfer</span>"
                        Value="mnuPatientTransfer" NavigateUrl="~/ClinicalForms/frmClinical_Transfer.aspx">
                    </asp:MenuItem>
                    <asp:MenuItem Text="<i class='fa fa-home text-muted fa-lg' aria-hidden='true'></i> <span class='text-muted'>Patient Home</span>"
                        Value="mnuPatientHome" NavigateUrl="~/ClinicalForms/frmPatient_Home.aspx"></asp:MenuItem>
                    <asp:MenuItem NavigateUrl="javascript:openWaitingList();" Text="<i class='fa fa-building-o text-muted fa-lg' aria-hidden='true'></i> <span class='text-muted'>Waiting List</span>"
                        Value="mnuWaitingList"></asp:MenuItem>
                </Items>
            </asp:Menu>
            <asp:Menu ID="PharmacyDispensingMenu" runat="server" Orientation="Horizontal" CssClass="nav navbar-nav text-muted PharmacyDispensing"
                ForeColor="Black" Width="50%" RenderingMode="Table" StaticEnableDefaultPopOutImage="False"
                Font-Bold="False" Visible="False" DynamicMenuStyle-CssClass="dropdown-menu">
                <StaticMenuItemStyle Height="40px" />
                <DynamicMenuItemStyle CssClass="PharmacyDispensing" Height="25px" HorizontalPadding="5px" />
                <DynamicHoverStyle CssClass="PharmacyDispensinghover" />
                <Items>
                    <asp:MenuItem Text="Dashboard" Value="Dashboard" NavigateUrl="~/PharmacyDispense/frmPharmacy_Dashboard.aspx">
                    </asp:MenuItem>
                    <asp:MenuItem Text="Dispense" Value="Dispense" NavigateUrl="~/PharmacyDispense/frmPharmacyDispense_FindPatient.aspx">
                    </asp:MenuItem>
                    <asp:MenuItem Text="Stock Summary" Value="StockSummaryWeb" NavigateUrl="~/PharmacyDispense/frmPharmacy_StockSummary.aspx">
                    </asp:MenuItem>
                    <asp:MenuItem Text="Stock Management" Value="StockManagement" NavigateUrl="~/PharmacyDispense/frmPharmacy_StockManagement.aspx">
                    </asp:MenuItem>
                    <asp:MenuItem NavigateUrl="~/PharmacyDispense/frmPharmacy_ReferenceMaterials.aspx"
                        Text="Reference Materials" Value="Reference Materials"></asp:MenuItem>
                    <asp:MenuItem NavigateUrl="javascript:PopupCenterL2('International Quality Care Patient Management and Monitoring System','1000','650');"
                        Text="ADR Reporting" Value="ADR Reporting"></asp:MenuItem>
                </Items>
            </asp:Menu>
        </div>
        <!-- .nav-collapse collapse -->
    </div>
    <!-- .container -->
    <%--</div><!-- .navbar-inner -->--%>
</div>
<%--<h4 class="margin" style="margin-left: 20px">
    <asp:Label ID="lblformname" runat="server" Text=""></asp:Label>
</h4>--%>
<asp:Label ID="lblpntStatus" CssClass="textstylehidden" runat="server" Visible="false"
    Text=""></asp:Label>
<div id="divPatientInfo123" class="contentpad">
    <asp:Panel ID="PanelPatiInfo" class="border center formbg" runat="server" Width="100%"
        Style="margin-left: 25px;">
        <div class="row" align="center">
            <div class="col-md-2 col-sm-12 col-xs-12 form-group text-nowrap">
                <label class="control-label">
                    Patient Name:</label>
                <asp:Label ID="lblpatientname" runat="server"></asp:Label>
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group align-right text-nowrap">
                <label class="control-label">
                    IQ Number:</label>
                <asp:Label ID="lblIQnumber" runat="server"></asp:Label>
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group align-right text-nowrap">
                <label class="control-label">
                    Age:</label>
                <asp:Label ID="lblAge" runat="server"></asp:Label>
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group align-right text-nowrap">
                <label class="control-label">
                    Gender:</label>
                <asp:Label ID="lblGender" runat="server"></asp:Label>
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group align-right text-nowrap" id="Tr1"
                runat="server">
                <asp:Panel ID="thePnlIdent" runat="server">
                </asp:Panel>
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group align-right text-nowrap" id="Tr2"
                runat="server">
                <asp:Panel ID="thePnlBill" runat="server">
                </asp:Panel>
            </div>
        </div>
    </asp:Panel>
    <uc1:UserControl_Alerts ID="UserControl_Alerts1" runat="server" />
</div>
