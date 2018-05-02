using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Interface.Clinical
{
    public interface IKNHPeadraticFollowup
    {
        DataSet SaveUpdateKNHPeadraticFollowupData(Hashtable hashTable, DataTable dtMultiSelectValues, int DataQuality, int signature);
        DataSet SaveUpdateKNHPeadraticFollowupData_TriageTab(Hashtable hashTable, DataTable dtMultiSelectValues, int DataQuality, int signature, int UserId);
        DataSet SaveUpdateKNHPeadraticFollowupData_CATab(Hashtable hashTable, DataTable dtMultiSelectValues, int DataQuality, int signature, int UserId);
        DataSet SaveUpdateKNHPeadraticFollowupData_ExamTab(Hashtable hashTable, DataTable dtMultiSelectValues, int DataQuality, int signature, int UserId);
        DataSet SaveUpdateKNHPeadraticFollowupData_MgtTab(Hashtable hashTable, DataTable dtMultiSelectValues, int DataQuality, int signature, int UserId);
        DataSet GetKNHPeadtricFollowupDetails(int ptn_pk, int visitpk);
        DataSet GetKNHPeadtricFollowupAutoPopulating(int ptn_pk);
    }
}
