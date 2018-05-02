using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace Interface.Clinical
{
    public interface IKNHStaticForms
    {
        DataSet GetExistKNHStaticFormbydate(int PatientID, string VisitdByDate, int locationID, int Visittype);
        DataSet SaveUpdateExpressFormTriageTab(Hashtable theHT, DataTable dt, DataTable referredTo);
        DataSet GetExpressFormData(int ptn_pk, int visit_pk);
        DataSet SaveUpdateExpressFormClinicalAssessmentTab(Hashtable theHT, DataTable ARVShortTermEffects, DataTable ARVLongTermEffects, DataTable Eligiblethrough, DataTable ARTchangecode, DataTable ARTstopcode);
        DataSet SaveUpdateARVTherapy(Hashtable theHT);
        DataSet SaveUpdateTBScreening(Hashtable theHT,DataTable TBAssessment, DataTable IPTStopReason, DataTable ReviewCheckList, DataTable SignsOfHepatitis);
        DataSet GetTBScreeningFormData(int ptn_pk, int visit_pk);
        DataSet GetPwPFormData(int ptn_pk, int visit_pk);
        DataSet GetLastRegimenDispensed(int ptn_pk);
        DataSet useExpressFormRules(int ptn_pk);
        DataSet GetExtruderData(int ptn_pk);
        string GetSignature(string tabName, int visit_pk);
        DataSet SaveUpdatePwP(Hashtable theHT, DataTable HighRisk, DataTable TransitionPreparation, DataTable ReferredTo, DataTable Counselling);
        DataSet CheckIfPreviuosTabSaved(string tabName, int visit_pk);
        DataTable GetTabID(string tabName);
        DataSet CheckIfTabSaved(int tabID, int visit_pk);
        DataTable GetPatientFeatures(int Ptn_pk);
        DataSet GetExpressFormAutoPopulatingData(int ptn_pk);
        DataSet GetTBScreeningAutoPopulatingData(int ptn_pk);
        DataSet GetPEPFormAutoPopulatingData(int ptn_pk);
        DataSet GetPwPAutoPopulatingData(int ptn_pk);
        DataSet checkDuplicateVisit(string visitDate, int visitType, int ptnPk);
        DataSet GetLatestWHOStage(int ptn_pk);
        DataSet GetPatientDrugHistory(int ptn_pk);
        DataSet GetZScoreValues(int Ptn_pk, string gender, string height);
        DataSet GetAdultFollowUpFormAutoPopulatingData(int ptn_pk);
        DataSet GetAlerts(int PatientId);
    }
}
