using System.Data;


namespace Interface.Clinical
{
    public interface IAllergyInfo
    {
        int SaveAllergyInfo(int Id, int Ptn_Pk, string AllergyType, string Allergen, string otherAllergen, string severity, string typeReaction, int UserId, int DeleteFlag, string dataAllergy);
        DataSet GetAllAllergyData(int PatientId);
        int DeleteAllergyInfo(int Id, int @UserId);

        DataSet GetNAFDACDataByPatient(int PatientId);
        int SaveNAFDACData(int ptn_Pk, string description, string dateReactionStart, string dateReactionStop, string outcomeReaction, string admitDuetoADR, string prolongedADR,
            string durationOfAdmission, string reactionTreatment, int signature, int drugPK, int suspectedDrug, string indicationForUse, string dosage, string route,
            string dateStarted, string dateStopped, int concomitantMedicine, string reasonForUse, int userId, int deleteFlag, string nafdacNo, int genericID, int batchID, string outcomeReactionDesc);

        int DeleteNAFDACDataByDrug(int Id, int @UserId);
    }
}
