using System;
using System.Collections.Generic;
using System.Text;
using System.Data;


namespace Interface.Pharmacy
{
   public interface IPediatric
    {
       DataSet GetPediatricFields(int PatientID);
       DataSet GetExistPaediatricDetails(int PatientID);
       int SaveUpdatePaediatricDetail(int patientID, int PharmacyID, int LocationID, int RegimenLine, string PharmacyNotes, DataTable theDT, DataSet theDrgMst, int OrderedBy, DateTime OrderedByDate, int DispensedBy, DateTime DispensedByDate, int Signature, int EmployeeID, int OrderType, int VisitType, int UserID, decimal Height, decimal Weight, int FDC, int ProgID, int ProviderID, DataTable theCustomFieldData, int PeriodTaken, int flag, int SCMFlag, DateTime AppntDate, int AppntReason);
       int SavePaediatricDetail(int patientID, DataTable theDT, DataSet theDrgMst, int OrderedBy, DateTime OrderedByDate, int DispensedBy, DateTime DispensedByDate, int Signature, int EmployeeID, int LocationID, int OrderType, int VisitType, int UserID, decimal Height, decimal Weight, int FDC, int ProgID, int ProviderID, DataTable theCustomFieldData, int PeriodTaken);
       int UpdatePaediatricDetail(int patientID, int LocationID, int PharmacyID, DataTable theDT, DataSet theDrgMst, int OrderedBy, int DispensedBy, int Signature, int EmployeeID, int OrderType, int UserID, decimal Height, decimal Weight, int FDC, int ProgID, int ProviderID, DateTime OrderedByDate, DateTime ReportedByDate, DataTable theCustomFieldData, int PeriodTaken);
       int DeletePediatricForms(string FormName, int OrderNo, int PatientId,int UserID);
       DataSet GetExistPharmacyForm(int PatientID, DateTime OrderedByDate);
       DataSet GetExistPharmacyFormDespensedbydate(int PatientID, DateTime DispensedByDate);
       DataSet GetPatientRecordformStatus(int PatientID);
       int IQTouchSaveUpdatePharmacy(List<IPharmacyFields> iPharmacyFields);
       DataSet IQTouchGetPharmacyDetails(int PatientID);
       DataSet SaveUpdate_CustomPharmacy(String Insert, DataSet DS, int UserId);
       DataSet GetPharmacyDetailforLabelPrint(int PatientId, int VisitId);
       DataSet GetPreDefinedDruglist();
       int SavePredefineList(string name, DataTable dt, int UserId);
       DataSet GetDrugGenericDetails(int PatientID);
       DataSet GetPatientContinueARVDetails(int PatientId);
   }
// This class is used for IQTouch pharmacy form
   [Serializable()]
   public class IPharmacyFields
   {
       public int Ptn_pk { get; set; }
       public int VisitID { get; set; }
       public int LocationID { get; set; }
       public int ptn_pharmacy_pk_old { get; set; }
       public int ptn_pharmacy_pk { get; set; }
       public int userid { get; set; }
       public int EmployeeId { get; set; }
       public int VisitType { get; set; }
       public int AppntReason { get; set; }
       public int TreatmentProgram { get; set; }
       public int PeriodTaken { get; set; }
       public int Drugprovider { get; set; }
       public decimal Weight { get; set; }
       public decimal Height { get; set; }
       public int RegimenLine { get; set; }
       //public DateTime PharmacyRefillDate { get; set; }
       public string PharmacyRefillDate { get; set; }
       public string PharmacyNotes { get; set; }
       public int OrderedBy { get; set; }
       //public DateTime OrderedByDate { get; set; }
       public string OrderedByDate { get; set; }
       public int DispensedBy { get; set; }
       //public DateTime DispensedByDate { get; set; }
       public string DispensedByDate { get; set; }
       public int flag { get; set; }
       public List<DrugDetails> Druginfo { get; set; }
   }
   [Serializable()]
   public class DrugDetails
   {

       public int Drug_Pk { get; set; }
       public int FrequencyID { get; set; }
       public int GenericId { get; set; }
       public decimal SingleDose { get; set; }
       public decimal Duration { get; set; }
       public int Prophylaxis { get; set; }
       public int BatchNo { get; set; }
       public decimal OrderedQuantity { get; set; }
       public decimal DispensedQuantity { get; set; }
       public int refill { get; set; }
       public DateTime RefillExpiration { get; set; }
       public int sellingprice { get; set; }
       public int billamount { get; set; }
   }
}
