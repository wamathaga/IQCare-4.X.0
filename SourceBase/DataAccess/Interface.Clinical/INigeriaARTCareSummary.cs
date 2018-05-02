using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Interface.Clinical
{
    public interface INigeriaARTCareSummary
    {
        int DeleteARTCareSummaryForm(string FormName, int OrderNo, int PatientId, int UserID);
        DataSet GetPatientARTCareSummary(int patientid, int LocationId);
        int SaveUpdatePatientARTCareSummary(int patientID, int VisitID, int LocationID, Hashtable ht, int userID, int DataQualityFlag);
    }
}
