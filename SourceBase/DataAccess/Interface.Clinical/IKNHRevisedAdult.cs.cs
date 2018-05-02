using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Interface.Clinical
{
    public interface IKNHRevisedAdult
    {
        DataTable GetDropdownFieldDetails(int featureId);
        DataSet SaveUpdateKNHRevisedAdultFollowupData(Hashtable hashTable, DataTable dtMultiSelectValues, int signature, int UserId);
        DataSet SaveUpdateKNHRevisedFollowupData_TriageTab(Hashtable hashTable, DataTable dtMultiSelectValues, int DataQuality, int signature, int UserId, string ModuleId);
        DataSet SaveUpdateKNHRevisedFollowupData_CATab(Hashtable hashTable, DataTable dtMultiSelectValues, int DataQuality, int signature, int UserId, string ModuleId);
        DataSet SaveUpdateKNHRevisedFollowupData_ExamTab(Hashtable hashTable, DataTable dtMultiSelectValues, int DataQuality, int signature, int UserId, string ModuleId);
        DataSet SaveUpdateKNHRevisedFollowupData_MgtTab(Hashtable hashTable, DataTable dtMultiSelectValues, int DataQuality, int signature, int UserId, string ModuleId);
        DataSet GetKNHRevisedAdultDetails(int ptn_pk, int visitpk);
    }
}
