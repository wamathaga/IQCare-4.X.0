using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Interface.Clinical
{
    public interface IKNHMEI
    {
        DataSet SaveUpdateKNHMEI_TriageTab(Hashtable theHT, DataSet theDS, String Tab);
        DataSet GetKNHMEI_Data(int PatientId, int VisitId);
        DataSet GetKNHMEIData_Autopopulate(int PatientId);
        DataSet GetKNHMEI_LabResult(int PatientId);
        int SaveKNHMEILabResult(DataTable theDT, int userId, int PatientId, int VisitId);

        //DataSet SaveUpdateKNHMEI_HTCTab(Hashtable theHT, DataSet theDS);
        //DataSet SaveUpdateKNHMEI_ProfileTab(Hashtable theHT, DataSet theDS);
        //DataSet SaveUpdateKNHMEI_ClinicalReviewTab(Hashtable theHT, DataSet theDS);
        //DataSet SaveUpdateKNHMEI_PMTCTTab(Hashtable theHT, DataSet theDS);
    }
}
