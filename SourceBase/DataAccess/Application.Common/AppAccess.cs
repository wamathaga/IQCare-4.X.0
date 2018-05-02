using System;

namespace Application.Common
{
    public class ApplicationAccess
    {
        public static int Enrollment = 1;
        public static int InitialEvaluation = 2;
        public static int AdultPharmacy = 3;
        public static int PaediatricPharmacy = 4;
        public static int Laboratory = 5;
        public static int NonARTFollowup = 6;
        public static int ARTFollowup = 7;
        public static int CareTracking = 8;
        public static int HomeVisit = 9;
        public static int FacilitySetup = 10;
        public static int UserAdministration = 11;
        public static int UserGroupAdministration = 12;
        //public static int CustomiseDropDown = 13;
        public static int HIVCarePatientProfile = 14;
        public static int PatientARVPickup = 15;
        public static int FacilityReports = 16;
        public static int DonorReports = 17;
        public static int DeleteForm = 46;
        public static int DeletePatient = 47;
        public static int CustomReports = 48;
        public static int ConfigureCustomFields = 49;
        public static int Schedular = 50;
        public static int SchedularAppointment = 45;
        public static int Transfer = 56;
        public static int FamilyInfo = 59;
        public static int OrderLabTest = 83;
        public static int PMTCTEnrollment = 117;
        public static int ChildEnrollment = 118;
        public static int Export = 228;
        public static int AuditTrail = 229;
        public static int QueryBuilderReports = 230;
        public static int Backupsetup = 232;
        public static int Backuprestore = 231;
        public static int HEIForm = 265;
        public static int MEIForm = 277;

        //form builder
        public static int FormBuilder = 119;
        public static int Upsize = 120;
        public static int DatabaseMigration = 121;
        public static int DatabaseMerge = 122;
        public static int ManageForms = 123;
        public static int ManageFields = 124;
        public static int PatientRegistration = 126;
        public static int ConfigureHomePages = 127;
        public static int ConfigureCareTermination = 128;
        public static int SpecialFormLinking = 129;
        public static int ManageTechnicalArea = 130;
        //form Project Management Module
        public static int Program = 131;
        public static int DonorName = 132;
        public static int Supplier = 133;
        public static int CostAllocationCategory = 134;
        public static int ItemType = 135;
        public static int LabTestLocation = 136;
        public static int ManufacturerDetail = 137;
        public static int PurchasingDispensingUnit = 138;
        public static int ReturnReason = 139;
        public static int AdjustmentReason = 140;
        public static int DonorProgramLinking = 141;
        public static int DrugType = 142;
        public static int Store = 143;
        public static int SupplierItem = 144;
        public static int ProgramItem = 145;
        public static int ItemTypeSubTypeLinking = 146;
        public static int StoreSourceDestinationLinking = 147;
        public static int StoreUserLinking = 148;
        public static int StoreItem = 149;
        public static int PatientVisitConfiguration = 151;
        public static int BudgetConfiguration = 152;
        public static int DebitNote = 153;
        public static int OpeningStock = 154;
        public static int AdjustStocklevel = 155;
        public static int PurchaseOrder = 156;
        public static int GoodReceiveNotes = 157;
        public static int DrugDispense = 158;
        public static int DisposeItem = 159;
        public static int BatchSummary = 160;
        public static int StockSummary = 161;
        public static int ExpiryReport = 162;
        public static int PriorARTHIVCare = 163;
        public static int ARTCare = 164;
        public static int HIVCareARTEncounter = 165;
        public static int InitialFollowupVisits = 167;
        public static int ARTHistory = 168;
        public static int KNHPaediatricInitialEvaluationForm = 174;
        public static int KNHAdultFollowupForm = 175;
        public static int KNHPaediatricFollowupForm = 176;
        public static int KNHAdultInitialEvaluationForm = 177;
        public static int KNHExpress = 219;
        public static int PASDPRegistrationForm = 207;
        public static int PASDPPharmacyform = 208;
        public static int PASDPInitialandFollowup = 209;
        public static int PASDPNonVisitClinicalNote = 210;
        public static int PASDPImmunisation = 217;
        public static int PASDPLabrotary = 218;
        public static int FBDBOperation = 266;
        public static int FBDataBaseSyncronisation = 267;
        public static int FBSCMConfiguration = 268;
        public static int FBManageCareEndedField = 269;
        public static int FBManageRegField = 270;
        public static int FBFieldAssociation = 271;
        public static int FBImportExportForm = 272;
        public static int RequistionVoucher = 273;
        public static int BudgetView = 274;
        //**************//
        //John Macharia start
        public static int ARVTherapy = 169;
        //John End
        public static int InterStoreTransfer = 170;
        public static int IssueVoucher = 171;

        public static int Allergy = 172;
        public static int KNHPaediatricInitialEvaulation = 174;
        public static int NAFDAC = 296;

        //****IQTools***////////
        public static int IQToolsReports = 225;

        // for billing 
        public static int Billing = 236;
        public static int BillingConfiguration = 237;
        public static int BillingReports = 238;
        public static int BillingReversal = 239;
        public static int Consumables = 240;
        public static int BillingType = 235;

        /// The billing cheque payment

        public static int BillingChequePayment = 241;
        /// The billing insurance payment        
        public static int BillingInsurancePayment = 242;
        /// <summary>
        /// The billing payment type
        /// </summary>
        public static int BillingReceivePayment = 243;
        /// <summary>
        /// The billing payment type
        /// </summary>
        public static int BillingWriteOffBill = 246;

        //patients Ward

        public static int NewWardAdmission = 250;
        public static int UpdateWardAdmission = 251;
        public static int DischargePatient = 252;
        public static int Wards = 253;

        #region "PharmacyDispense"

        public static int Dashboard = 254;
        public static int Dispense = 255;
        public static int StockSummaryWeb = 256;
        public static int StockManagement = 257;

        #endregion
        #region  "Waiting List"
        public static int WaitingList = 258;

        #endregion

        #region "CTC"

        public static int FollowupEducation = 71;
        public static int Pharmacy = 72;
        public static int PatientClassification = 67;
        public static int PatientRecord = 73;

        #endregion

        #region "PASDP"
        public static int RegistrationForm = 207;
        public static int Pharmacyform = 208;
        public static int InitialandFollowup = 209;
        public static int NonVisitClinicalNote = 210;
        public static int PatientVisit = 211;
        public static int PatientSummary = 212;
        public static int PatientLabHistory = 213;
        #endregion
        #region "Nigeria ART Care"
        public static int NigeriaAdultIE = 260;
        public static int NigeriaARTCareVisitation = 261;
        public static int NigeriaPaediatricIE = 262;
        public static int NigeriaARTCareSummary = 263;
        public static int NigeriaInitialVisit = 264;
        #endregion

        #region "CustomizeList"
        public class CustomizeMasterList
        {
            public static int AdherenceCodes = 18;
            public static int ARVSideEffects = 19;
            public static int BarrierToCare = 20;
            public static int District = 21;
            public static int Drugs = 22;
            public static int EducationLevel = 23;
            public static int EmergencyContactRelationship = 24;
            public static int Designation = 25;
            public static int Employee = 26;
            public static int EmploymentStatus = 27;
            public static int Laboratory = 28;
            public static int MartialStatus = 29;
            public static int Occupation = 30;
            public static int OIAIDSDefiningIllness = 31;
            public static int PresentingComplaints = 32;
            public static int Province = 33;
            public static int TherapyChangeCode = 34;
            public static int Village = 35;
            public static int PatientReferred = 36;
            //public static int PharmacyUnits = 37;
            public static int DrugFrequency = 37;
            public static int LaboratoryUnit = 53;
            public static int PatientRelationType = 57;
            public static int ARVAdherenceReasonPoor = 81;

            #region CTC
            public static int Region = 60;
            public static int Divison = 62;
            public static int Ward = 64;
            public static int CTC_PatientClassification = 67;
            public static int CTC_ARVAdherenceReason = 65;
            public static int CTC_AIDSDefiningEvents = 68;
            public static int CTC_PatientReferredTo = 82;
            public static int CTC_Regimen = 70;
            #endregion

            #region PASDP
            public static int PASDPDistrict = 178;
            public static int SubDistrict = 179;
            public static int Suburbs = 180;
            public static int EntryPoint = 181;
            public static int Relationship = 182;
            public static int DrugStatus = 183;
            public static int VisitType = 184;
            public static int DevelopmentalScreening = 185;
            public static int TannerStage = 186;
            public static int FamilyPlanningMethod = 187;
            public static int FormsoftreatmentTBTreatment = 188;
            public static int DiagnosisMode = 189;
            public static int PhysicalFindings = 190;
            public static int AdverseEventSeverityGrade = 191;
            public static int WhypoorfairARVAdherence = 192;
            public static int AdverseEventcategory = 193;
            public static int AdverseEventName = 194;
            public static int TBSensitivityandResistance = 195;
            public static int SuspectedDeathReason = 196;
            public static int LosttoFollowupReason = 197;
            public static int ExitReason = 198;
            public static int ReferredTo = 199;
            public static int Levelofdisclosure = 200;
            public static int NutritionalSupport = 201;
            public static int NutritionalProblem = 202;
            public static int FeedingOption = 203;
            public static int StopRegimenReason = 204;
            public static int Changeregimenreason = 205;
            public static int Immunisation = 206;






            #endregion


        }
        #endregion

        #region HIVCE
        public static int ClinicalEncounter = 295;
        #endregion

        #region Adherence
        public static int AdherenceBarriers = 299;
        public static int EnhanceAdherenceCounselling = 298;
        #endregion

        public static string DBSecurity = "'ttwbvXWpqb5WOLfLrBgisw=='";
    }

    public class FunctionAccess
    {
        public static int View = 1;
        public static int Update = 2;
        public static int Delete = 3;
        public static int Add = 4;
        public static int Print = 5;
    }
}
