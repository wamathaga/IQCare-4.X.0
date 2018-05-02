using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Interface.Clinical
{
    public interface ICustomForm
    {
        DataSet Validate(string FormName, string Date, string PatientId);
        DataSet GetFormName(int ModuleId, int countryID);
        DataSet GetFieldName_and_Label(int FeatureId, int PatientId);
        DataSet GetFieldName_and_LabelPharmacy(int FeatureId, int PatientId);
        DataSet Common_GetSaveUpdate(string Insert);
        DataSet GetPmtctDecodeTable(string CodeID);
        DataSet SaveUpdate(String Insert, DataSet DS, int TabId);
        int DeleteForm(string FormName, int VisitID, int PatientId, int UserID);
        String GetSystemTime(int Format);
        int GetCustomFormSavedByUser(int FeatureId, int TabID);
    }

}
