using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Interface.Clinical
{
    public interface INigeriaARTCareVisitation
    {
        #region "ART Card Visitation"
        DataSet GetNigeriaPatientARTCareVisitation(int patientID, int VisitID);
        int DeleteARTCareVisitationForm(string FormName, int OrderNo, int PatientId, int UserID);
        int Save_Update_ARTCareVisitation(int patientID, int VisitID, int LocationID, Hashtable ht, DataSet theDSchklist, int userID, int DataQualityFlag);
        #endregion
    }
}
