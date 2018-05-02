using Entities.Common;

namespace Interface.Clinical
{
    public interface IHTSClientIntake
    {
        HTSClientIntake GetClientIntakeFormData(int ptn_pk, int locationID);
        bool SaveClientIntakeFormData(string obj, int patientID, int UserID);
    }
}
