using Entities.Common;

namespace Interface.Clinical
{
    public interface IClinicalSummaryForm
    {
        ClinicalSummary GetClinicalSummaryData(int ptn_pk, int Visit_Id, int LocationID);
        int SaveUpdateClinicalSummaryData(ClinicalSummary obj, int userID);
    }
}
