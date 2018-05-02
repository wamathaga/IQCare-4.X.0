using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Entities.Common;

namespace Interface.Clinical
{
    public interface IKNHMEI
    {
        DataSet SaveUpdateKNHMEI_TriageTab(Hashtable theHT, DataSet theDS, String Tab);
        DataSet GetKNHMEI_Data(int PatientId, int VisitId);
        DataSet GetKNHMEIData_Autopopulate(int PatientId);
        DataSet GetKNHMEI_LabResult(int PatientId);
        int SaveKNHMEILabResult(DataTable theDT, int userId, int PatientId, int VisitId);
        DataSet GetKNHANC_Data(int VisitId);
        //DataSet SaveUpdateKNHMEI_HTCTab(Hashtable theHT, DataSet theDS);
        //DataSet SaveUpdateKNHMEI_ProfileTab(Hashtable theHT, DataSet theDS);
        //DataSet SaveUpdateKNHMEI_ClinicalReviewTab(Hashtable theHT, DataSet theDS);
        //DataSet SaveUpdateKNHMEI_PMTCTTab(Hashtable theHT, DataSet theDS);

        ANC GetANCData(int ptn_pk, int visitPK, int locationId);
        bool SaveUpdateProfile(ANC obj, int userId, int locationId, out int visitId);
        ANCScreeningTests GetANCScreeningTestsData(int ptn_pk, int locationId, int visitId);
        bool SaveUpdateScreeningTestsData(ANCScreeningTests obj, int userId, int locationId);
        bool SaveUpdateVisitDetailsData(ANCVisitDetails obj, int userId, int locationId);
        ANCVisitDetails GetANCVisitDetailsData(int ptn_pk, int locationId, int visitId);
    }
}
