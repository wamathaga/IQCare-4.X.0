using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
using Entities.Common;

namespace Interface.Clinical
{
    public interface IPatientAdherence
    {
        PatientAdherenceCounseling GetEnhanceAdherenceCounsellingDataByPatient(int patientID, int locationID, int visitID, int sessionID);
        int SaveUpdateEnhanceAdherenceCounsellingSessions(PatientAdherenceCounseling obj, int userID);

        PatientAdherenceBarrier GetPatientAdherenceBarriers(int ptn_pk, int visitPK, int locationId);
        bool SaveUpdatePatientAdherenceBarriers(PatientAdherenceBarrier obj, int userId, out int visitId);
    }
}
