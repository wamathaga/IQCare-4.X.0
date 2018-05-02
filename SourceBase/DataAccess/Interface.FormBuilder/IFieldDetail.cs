using System;
using System.Data;
using System.Collections;

namespace Interface.FormBuilder
{
    public interface IFieldDetail 
    {
      DataSet GetDrugType();
      DataSet GetBusinessRule();
      DataSet GetBusinessDrugList(int fieldID);
      //DataSet GetConditionalformInfo(int featureid);
      DataSet GetCustomFields(string strFieldName, int iModuleId, int flag);
      DataSet GetCustomFields(string strFieldName, int iModuleId, int flag ,int isgridview);
      int ResetCustomFieldRules(int fieldID, int flag, int predefine, string FieldName);
      DataSet CheckPredefineField(int fieldID);
      DataSet CheckCustomFields(int fieldID);
      int DeleteCustomField(int fieldID, int flag);
      DataSet GetDuplicateCustomFields(int id, string fieldName, int ModuleId, int flag);
      int SaveUpdateCusomtField(int ID, string FieldName, int ControlID, int DeleteFlag, int UserID, int CareEnd, int flag,
          string SelectList, DataTable business, int Predefined, int SystemID, DataTable dtconditionalFields, DataTable dtICD10Fields, DataSet dsFormVersionFields, int FacilityId);
      DataSet GetConditionalFieldslist(Int32 Codeid, int FID, int flag);
      DataSet GetConditionalFieldsDetails(Int32 ConfieldID, Int32 CareEndconFlag);
      //int SaveConditionalFields(DataTable dtconditionalFields);
      int SaveModDeCode(DataTable dtModDeCode);
      DataSet GetICDList();
      DataSet GetICD10Values(int FieldId);

    }
}
