using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;


namespace Interface.Clinical
{

    #region Registration Object
    [Serializable]
    public class objRegistration
    {
        public objRegistration()
        {
            MotherAliveYN = null;
            MotherPMTCTdrugsYN = -1;
            ChildPMTCTdrugsYN = -1;
            MotherPMTCTdrugs = new List<int>();
            ChildPMTCTdrugs = new List<int>();
            Regimen = new List<int>();
            PriorARTRegimens = new List<PriorARTRegimen>();
            DrugAllergies = new List<DrugAllergy>();
            MotherARTYN = 3;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DOB { get; set; }
        public int Sex { get; set; }
        public string RegistrationDate { get; set; }
        public string Address { get; set; }
        public string Suburb { get; set; }
        public string District { get; set; }
        public string SubDistrict { get; set; }
        public string TelephoneNo { get; set; }
        public string Addresscomments { get; set; }
        public string PostalAddress { get; set; }
        public string PostalCode { get; set; }
        public string EntryPoint { get; set; }
        public string OtherEntryPoint { get; set; }
        public string CareGiverName { get; set; }
        public string CareGiverDOB { get; set; }
        public decimal CareGiverAge { get; set; }
        public int CareGiverGender { get; set; }
        public int CareGiverRelationship { get; set; }
        public string OtherCareGiver { get; set; }
        public string CareGiverTelephone { get; set; }
        public string MotherName { get; set; }
        public bool? MotherAliveYN { get; set; }
        public int MotherPMTCTdrugsYN { get; set; }
        public List<int> MotherPMTCTdrugs { get; set; }
        public int ChildPMTCTdrugsYN { get; set; }
        public List<int> ChildPMTCTdrugs { get; set; }
        public int MotherARTYN { get; set; }
        public int FeedingOption { get; set; }
        public string DateConfirmedHIVPositive { get; set; }
        public string DateEnrolledHIVCare { get; set; }
        public int WHOStageAtEnrollment { get; set; }
        public string TransferInDate { get; set; }
        public int FromDistrict { get; set; }
        public string Facility { get; set; }
        public string DateStart { get; set; }
        public List<int> Regimen { get; set; }
        public string RegimenAbbreviations { get; set; }
        public string Weight { get; set; }
        public string Height { get; set; }
        public string BMI { get; set; }
        public int WHOStageAtTransfer { get; set; }
        public string PriorART { get; set; }
        public List<PriorARTRegimen> PriorARTRegimens { get; set; }
        public List<DrugAllergy> DrugAllergies { get; set; }

        [Serializable]
        public class PriorARTRegimen
        {
            public string Regimen { get; set; }
            public int RegimenID { get; set; }
            public string PriorARTDateLastUsed { get; set; }
        }

        [Serializable]
        public class DrugAllergy
        {
            public string Allergen { get; set; }
            public int AllergenID { get; set; }
            public string TypeOfReaction { get; set; }
            public string DateOfAllergy { get; set; }
            public string MedicalConditions { get; set; }
        }

    }
    #endregion

    public interface IIQTouchPatientRegistration
    {
        //IQCare Section
        DataTable GetPatientRecord(int PatientID);

        DataSet GetPatientEnroll(string patientid, int VisitID);
        DataSet GetPatientRegistration(int patientid, int VisitType);
        DataSet GetRegistrationDetails(int PatientID, string LocationID);
        bool CheckPatientFolderNo(string FolderNo);
        DataTable ReturnDatatableQuery(string theQuery);
        DataTable GetCareGiverInfo(string PatientID);
        DataTable GetCareGiverInfoFromVisit(string PatientID);
        int SavePatientRecord(string LocationID, string FirstName, string LastName, string Sex, string DOB, string MiddleName, string D9FolderNo);
        int SaveRegistrationDetails(objRegistration theReg, string PatientID, string LocationID, string UserID);

        //Removed-21-10-2010
        //int UpdatePatientRegistration(Hashtable ht, DataTable dtCaretype, DataTable dtARTsponsor, DataTable dt, DataTable dtBarrier, int VisitID, int dataquality, DataTable theCustomFieldData);
        //DataTable SaveNewRegistration(Hashtable ht, DataTable dtCaretype, DataTable dtARTsponsor, DataTable dt, DataTable dtBarrier, int dataquality, DataTable theCustomFieldData, Int32 VisitId);
        
        /// ///////////////////////////////////////////////////////////////////////
       
        
        DataSet GetAllDropDowns();
        DataSet GetDuplicatePatientSearchResults(string lastname, string middlename, string firstname, string address, string phone);
        DataSet GetPatientSearchResults(int FId, string lastname, string middlename, string firstname, string enrollmentType, string enrollment, string gender, string dob, string status, int ModuleId, string FolderNo);
        DataSet GetEnrolment(string CountryID, string PossitionID, string SatelliteID, string PatientClinicID, string enrolmentid, int deleteflag);
        DataSet GetAge(DateTime dob, DateTime regdate);
        DataSet GetVisitDate_IELAB(int patientid, int LocationID);
        DataTable theVisitIDDT(string patientid);
        DataSet theDropdown(string ID, string Flag);
        //ExposedInfant Section
        DataSet GetExposedInfantByParentId(int Ptn_Pk);
        DataSet GetMaxAutoPopulateIdentifier(string columnname);
        int DeleteExposedInfantById(int Id);
        int SaveExposedInfant(int Id, int Ptn_Pk, int ExposedInfantId, string FirstName, string LastName, DateTime DOB, string FeedingPractice3mos,
            string CTX2mos, string HIVTestType, string HIVResult, string FinalStatus, DateTime? DeathDate, int UserID);

        //CTC Section
        DataTable SavePatientRegistrationCTC(Hashtable ht, int Flag, DataTable theCustomFieldData);
        DataSet GetPatientDetailsCTC(string patientId, string CountryID, string PosID, string SatelliteID, string PatientClinicID, int Existflag, int VisitID);
        DataSet GetDrugGenericCTC();
        //PMTCT Section
        DataTable SavePatientRegistrationPMTCT(Hashtable ht, DataTable theCustomFieldData);
        DataSet GetPatientRegistrationPMTCT(int patientId);
        //DataTable UpdatePatientRegistrationPMTCT(Hashtable ht, DataTable theCustomFieldData);
        DataTable Validate(string Argument, string Flag);
        DataSet GetChildDetail(int patientid, int LocationID);
        DataSet SaveInfantInfo(Int64 PatientId, Int64 LocationID, Int64 VisitId, Int64 ParentId, Int64 UserID);
        int DeleteInfantInfo(int PatientId, int UserID);
        //Added on 21-10-2010
        DataSet GetPatientTechnicalAreaDetails(int patientid, string ModuleName, int ModuleID);
        DataTable SaveNewRegistration(Hashtable ht, DataTable dtCaretype, DataTable dtARTsponsor, DataTable dt, DataTable dtBarrier, int dataquality, DataTable theCustomFieldData);
        DataSet InsertUpdatePatientRegistration(Hashtable ht, DataTable dtCaretype, DataTable dtARTsponsor, DataTable dt, DataTable dtBarrier, int VisitID, int dataquality, DataTable theCustomFieldData);
        DataTable SaveNewRegistration(Hashtable ht, int dataquality);
        int UpdatePatientRegistration(Hashtable ht, int Ptn_Pk, int VisitID, int dataquality);
        DataTable SaveUpdateTechnicalArea(Hashtable ht, int VisitID);
        DataSet GetFieldNames(int ModuleID,int patientId);
        DataSet GetModuleNames(int FacilityID);
        DataTable CheckDuplicateIdentifiervaule(string Columnname, string Columnvalue);
        DataSet GetFieldName_and_Label(int FeatureID, int PatientID);
        DataSet Common_GetSaveUpdateforCustomRegistrion(string Insert);
        //////////////////////////////////////////////////////////////////////////////////////////////////////
        DataSet CheckIdentity(string ExposedInfantId);
        
         
        
      }
}
