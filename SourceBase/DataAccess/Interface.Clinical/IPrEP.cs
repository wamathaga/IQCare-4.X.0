using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
using Entities.Common;

namespace Interface.Clinical
{
    public interface IPrEP
    {
        PrEP GetPrEPInitiationData(int ptn_pk, int Visit_Id, int LocationID);
        int SaveUpdatePrEPInitiationData(PrEPInitiation obj, int userID, int visitId, int LocationID);
        PrEP GetPrEPTriageData(int ptn_pk, int Visit_Id, int LocationID);
        int SaveUpdatePrEPTriage(PrEPTriage obj, int Visit_Id, int LocationID, int UserId);
        PrEP GetPrEPAssessmentData(int ptn_pk, int Visit_Id, int LocationID);
        //int SaveUpdatePrEPAssessment(PrEPTriage obj, int Visit_Id, int LocationID);
    }
}
