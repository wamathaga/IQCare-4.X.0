using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HIVCE.Common.Entities;

namespace Interface.HIVCE
{
    public interface IClinicalEncounter
    {
        ClinicalEncounter GetData(int ptn_pk, int visitPK, int locationId);
        PresentingComplaint GetPresentingComplaints(int ptn_pk, int visitPK, int locationId);
        AddtionalHx GetAddtionalHx(int ptn_pk, int visitPK, int locationId);
        Management GetManagement(int ptn_pk, int visitPK, int locationId);
        DBScreeningData GetScreeningData(int ptn_pk, int visitPK, int locationId);
        DBSystemicReviewData GetSystemicReviewData(int ptn_pk, int visitPK, int locationId);
        bool SaveUpdateTriage(Triage obj, int userId, out int visitId);
        bool SaveUpdateTSTriage(TSTriage obj, int userId, out int visitId);
        bool SaveUpdatePresentingComplaintsData(PresentingComplaint obj, int userId, int locationId);
        bool SaveUpdateAdditionalHxData(DBAdditionalHx obj, int userId, int locationId);
        bool SaveUpdateScreeningData(DBScreeningData obj, int userId, int locationId);
        bool SaveUpdateSystemicReviewData(DBSystemicReviewData obj, int userId, int locationId);
        bool SaveUpdateManagementxData(ManagementDB obj, int userId, int locationId);

        RefillEncounter GetRefillEncounter(int ptn_pk, int visitPK, int locationId);
        bool SaveRefillEncounterData(RefillEncounterDB obj, int userId, int locationId);

        OIChronicDiseaseData GetOIChronicDiseaseData(int ptn_pk, int locationId);

    }
}
