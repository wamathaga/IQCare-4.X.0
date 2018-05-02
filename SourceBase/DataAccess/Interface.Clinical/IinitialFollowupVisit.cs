using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Interface.Clinical
{
    public interface IinitialFollowupVisit
    {
        DataSet GetInitialFollowupVisitData(int patientID, int locationID);
        DataSet SaveUpdateInitialFollowupVisitData(Hashtable hashTable, DataSet dataSet, bool isUpdate, DataTable theCustomDataDT);
        int DeleteInitialFollowupVisitForm(string FormName, int OrderNo, int PatientId, int UserID);
        DataSet GetInitialFollowupVisitInfo(int patientID, int locationID, int visitID);
        DataSet GetExistInitialFollowupVisitbydate(int PatientID, string VisitdByDate, int locationID);
    }
}
