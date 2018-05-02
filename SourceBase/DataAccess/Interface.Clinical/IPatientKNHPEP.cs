using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace Interface.Clinical
{
    public interface IPatientKNHPEP
    {
        DataSet GetDetails();
        DataSet GetDetailsPaediatric_IE();
        DataSet GetKNHPEPDetails(int ptn_pk, int visitpk);
        DataSet SaveUpdateKNHPEPData(Hashtable hashTable, DataTable PreExistingMedicalConditions, DataTable ShortTermEffects, DataTable LongTermEffects, string tabname);
        //DataSet SaveUpdatePaediatric_IE(Hashtable hashTable, DataTable tblMultiselect, string tabname);
        DataSet GetPaediatric_IE(int ptn_pk, int visitpk);
        DataSet getVisitIdByPatient(int patient_Id);
        //Created By- Nidhi 
        //Desc- Created seprate store procedure for each tabs. prior to this, handdled by a single procedure
        DataSet SaveUpdatePaediatricIE_TriageTab(Hashtable hashTable, DataTable tblMultiselect);
        DataSet SaveUpdatePaediatricIE_ClinicalHistoryTab(Hashtable hashTable, DataTable tblMultiselect);
        DataSet SaveUpdatePaediatricIE_ExaminationTab(Hashtable hashTable, DataTable tblMultiselect);
        DataSet SaveUpdatePaediatricIE_ManagementTab(Hashtable hashTable, DataTable tblMultiselect);

        DataSet SaveUpdateKNHPEPCA(Hashtable hashTable, DataTable ShortTermEffects, DataTable LongTermEffects);
        DataSet SaveUpdateKNHPEPTriage(Hashtable hashTable, DataTable PreExistingMedicalConditions, DataTable referredTo);
        DataTable GetSignature(int featureId, int visit_pk);
        DataTable GetTabID(int featureId);
    }
}
