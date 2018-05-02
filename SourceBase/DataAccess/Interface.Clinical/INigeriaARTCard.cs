using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Interface.Clinical
{
    public interface INigeriaARTCard
    {
        DataSet SaveUpdateNigeriaAdultIEClinicalHistoryData(Hashtable hashTable, DataTable dtMultiSelectValues, int DataQuality, int signature);
        DataSet SaveUpdateNigeriaAdultIEHIVHistoryData(Hashtable hashTable, DataTable dtMultiSelectValues, int DataQuality, int signature, DataTable dtPriorART);
        DataSet SaveUpdateNigeriaAdultIEExaminationData(Hashtable hashTable, DataTable dtMultiSelectValues, int DataQuality, int signature);
        DataSet SaveUpdateNigeriaAdultIEManagementData(Hashtable hashTable, DataTable dtMultiSelectValues, int DataQuality, int signature);
        DataSet GetNigeriaAdultIEDetails(int ptn_pk, int visitpk);

        DataSet SaveUpdateNigeriaPaedIEClinicalHistoryData(Hashtable hashTable, DataTable dtMultiSelectValues, int DataQuality, int signature, DataTable dtPriorART);        
        DataSet SaveUpdateNigeriaPaedIEExaminationData(Hashtable hashTable, DataTable dtMultiSelectValues, int DataQuality, int signature);
        DataSet SaveUpdateNigeriaPaedIEManagementData(Hashtable hashTable, DataTable dtMultiSelectValues, int DataQuality, int signature);
        DataSet GetNigeriaPaedIEDetails(int ptn_pk, int visitpk);

        DataSet SaveUpdateInitialVisitData(Hashtable hashTable, int DataQuality, int signature);
        DataSet GetNigeriaInitialVisitDetails(int ptn_pk, int visitpk);
        DataTable GetNigeriaInitialVisitId(int ptn_pk);
        DataSet GetNigeriaPriorARTDetails(int ptn_pk, int visitpk);
    }
}
