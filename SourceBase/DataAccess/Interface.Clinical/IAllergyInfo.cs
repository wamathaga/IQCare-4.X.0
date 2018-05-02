using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;


namespace Interface.Clinical
{
    public interface IAllergyInfo
    {
        int SaveAllergyInfo(int Id, int Ptn_Pk, string AllergyType, string Allergen, string otherAllergen, string severity, string typeReaction, int UserId, int DeleteFlag, string dataAllergy);
        DataSet GetAllAllergyData(int PatientId);
        int DeleteAllergyInfo(int Id, int @UserId);
    }
}
