using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Base;
using Interface.Clinical;
using DataAccess.Common;
using System.Data;
using DataAccess.Entity;


namespace BusinessProcess.Clinical
{
    class BAdultIE : ProcessBase, IKNHAdultIE
    {
        ClsDBUtility oUtility = new ClsDBUtility();

        public DataTable GetKnhAdultIEData(BIQAdultIE adultIEFields)
        {
            oUtility.Init_Hashtable();

            oUtility.AddParameters("@Flag", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.Flag).ToString());
            oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.PtnPk).ToString());
            oUtility.AddParameters("@LocationId", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.LocationId).ToString());
            oUtility.AddParameters("@VisitPk", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.VisitPk).ToString());  // ID here Visit PK
            oUtility.AddParameters("@ID", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.ID).ToString());  // ID here Visit PK
            oUtility.AddParameters("@fieldName", SqlDbType.VarChar, ConverTotValue.NullToInt(adultIEFields.FieldName).ToString());  // ID here Visit PK

            ClsObject GetRecs = new ClsObject();
            DataTable regDT = (DataTable)GetRecs.ReturnObject(oUtility.theParams, "Pr_Clinical_GetKNHAdultIE", ClsDBUtility.ObjectEnum.DataTable);
            return regDT;
        }
        public DataTable GetPatientVisitIdAdultIE(int PatientId, int Visittype)
        {
            oUtility.Init_Hashtable();
            oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ConverTotValue.NullToInt(PatientId).ToString());
            oUtility.AddParameters("@VisitType", SqlDbType.Int, ConverTotValue.NullToInt(Visittype).ToString());  // ID here Visit PK

            ClsObject GetRecs = new ClsObject();
            DataTable regDT = (DataTable)GetRecs.ReturnObject(oUtility.theParams, "Pr_Clinical_GetPatientVisitIdAdultIE", ClsDBUtility.ObjectEnum.DataTable);
            return regDT;
        }

        public DataSet GetKnhAdultIEFormData(BIQAdultIE adultIEFields)
        {
            oUtility.Init_Hashtable();
            oUtility.AddParameters("@Flag", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.Flag).ToString());
            oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.PtnPk).ToString());
            oUtility.AddParameters("@LocationId", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.LocationId).ToString());
            oUtility.AddParameters("@VisitPk", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.VisitPk).ToString());  // ID here Visit PK
            oUtility.AddParameters("@ID", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.ID).ToString());  // ID here Visit PK
            oUtility.AddParameters("@fieldName", SqlDbType.VarChar, ConverTotValue.NullToInt(adultIEFields.FieldName).ToString());  // ID here Visit PK

            ClsObject GetRecs = new ClsObject();
            DataSet regDS = (DataSet)GetRecs.ReturnObject(oUtility.theParams, "Pr_Clinical_GetKNHAdultIE", ClsDBUtility.ObjectEnum.DataSet);
            return regDS;
        }
        public DataTable SaveAdultIE(List<BIQAdultIE> lstadultIEFields, StringBuilder strMultiselect)
        {
            ClsObject expressManagerTest = new ClsObject();
            int theRowAffected = 0;
            DataTable theDT = new DataTable();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                expressManagerTest.Connection = this.Connection;
                expressManagerTest.Transaction = this.Transaction;

                if (lstadultIEFields.Count > 0)
                {
                    foreach (var adultIEFields in lstadultIEFields)
                    {

                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@ID", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.ID).ToString());
                        oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.PtnPk).ToString());
                        oUtility.AddParameters("@VisitId", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.VisitPk).ToString());
                        oUtility.AddParameters("@LocationId", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.LocationId).ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.UserId).ToString());
                        oUtility.AddParameters("@Temperature", SqlDbType.Decimal, ConverTotValue.NullToInt(adultIEFields.Temperature).ToString());
                        oUtility.AddParameters("@RespirationRate", SqlDbType.Decimal, ConverTotValue.NullToInt(adultIEFields.RespirationRate).ToString());
                        oUtility.AddParameters("@HeartRate", SqlDbType.Decimal, ConverTotValue.NullToInt(adultIEFields.HeartRate).ToString());
                        oUtility.AddParameters("@SystolicBloodPressure", SqlDbType.Decimal, ConverTotValue.NullToInt(adultIEFields.SystolicBloodPressure).ToString());
                        oUtility.AddParameters("@DiastolicBloodPressure", SqlDbType.Decimal, ConverTotValue.NullToInt(adultIEFields.DiastolicBloodPressure).ToString());
                        oUtility.AddParameters("@Height", SqlDbType.Decimal, ConverTotValue.NullToInt(adultIEFields.Height).ToString());
                        oUtility.AddParameters("@Weight", SqlDbType.Decimal, ConverTotValue.NullToInt(adultIEFields.Weight).ToString());
                        oUtility.AddParameters("@HeadCircum", SqlDbType.Decimal, ConverTotValue.NullToInt(adultIEFields.txtheadcircumference).ToString());
                        oUtility.AddParameters("@DiagnosisConfirmed", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.DiagnosisConfirmed).ToString());
                        oUtility.AddParameters("@ConfirmHIVPosDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.ConfirmHIVPosDate).ToString());
                        oUtility.AddParameters("@ChildAccompaniedByCaregiver", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.ChildAccompaniedByCaregiver).ToString());
                        oUtility.AddParameters("@TreatmentSupporterRelationship", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.TreatmentSupporterRelationship).ToString());
                        oUtility.AddParameters("@HealthEducation", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.HealthEducation).ToString());
                        oUtility.AddParameters("@DisclosureStatus", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.DisclosureStatus).ToString());
                        oUtility.AddParameters("@Reasondisclosed", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.reasonnotdisclosed).ToString());

                        oUtility.AddParameters("@OtherDisclosurestatus", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.otherdisclosurestatus).ToString());
                        oUtility.AddParameters("@SchoolingStatus", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.SchoolingStatus).ToString());
                        //oUtility.AddParameters("@HighestLevelattained", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.Highestlevelattained).ToString());
                        oUtility.AddParameters("@HIVSupportgroup", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.HIVSupportgroup).ToString());
                        oUtility.AddParameters("@HIVSupportGroupMembership", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.supportgroupmembership).ToString());
                        oUtility.AddParameters("@PatientReferredFrom", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.PatientReferredFrom).ToString());
                        oUtility.AddParameters("@NursesComments", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.NursesComments).ToString());

                        oUtility.AddParameters("@PresentingComplaints", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.PresentingComplaints).ToString());
                        oUtility.AddParameters("@OtherPresentingcomplaints", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.otherspecifiedcomplaints).ToString());
                        oUtility.AddParameters("@AdditionalComplaints", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.Additionalcomplaints).ToString());
                        oUtility.AddParameters("@MedHistoryFP", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.MedHistoryFP).ToString());
                        oUtility.AddParameters("@MedHistoryLastFP", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.MedHistoryLastFP).ToString());

                        oUtility.AddParameters("@RespiratoryDiseaseName", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.RespiratoryDiseaseName).ToString());
                        oUtility.AddParameters("@RespiratoryDiseaseDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.RespiratoryDiseaseDate).ToString());
                        oUtility.AddParameters("@RespiratoryDiseaseTreatment", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.RespiratoryDiseaseTreatment).ToString());
                        oUtility.AddParameters("@CardiovascularDiseaseName", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.CardiovascularDiseaseName).ToString());
                        oUtility.AddParameters("@CardiovascularDiseaseDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.CardiovascularDiseaseDate).ToString());
                        oUtility.AddParameters("@CardiovascularDiseaseTreatment", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.CardiovascularDiseaseTreatment).ToString());
                        oUtility.AddParameters("@GastroIntestinalDiseaseName", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.GastroIntestinalDiseaseName).ToString());
                        oUtility.AddParameters("@GastroIntestinalDiseaseDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.GastroIntestinalDiseaseDate).ToString());
                        oUtility.AddParameters("@GastroIntestinalDiseaseTreatment", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.GastroIntestinalDiseaseTreatment).ToString());
                        oUtility.AddParameters("@NervousDiseaseName", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.NervousDiseaseName).ToString());
                        oUtility.AddParameters("@NervousDiseaseDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.NervousDiseaseDate).ToString());
                        oUtility.AddParameters("@NervousDiseaseTreatment", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.NervousDiseaseTreatment).ToString());
                        oUtility.AddParameters("@DermatologyDiseaseName", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.DermatologyDiseaseName).ToString());
                        oUtility.AddParameters("@DermatologyDiseaseDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.DermatologyDiseaseDate).ToString());
                        oUtility.AddParameters("@DermatologyDiseaseTreatment", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.DermatologyDiseaseTreatment).ToString());
                        oUtility.AddParameters("@MusculoskeletalDiseaseName", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.MusculoskeletalDiseaseName).ToString());
                        oUtility.AddParameters("@MusculoskeletalDiseaseDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.MusculoskeletalDiseaseDate).ToString());
                        oUtility.AddParameters("@MusculoskeletalDiseaseTreatment", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.MusculoskeletalDiseaseTreatment).ToString());
                        oUtility.AddParameters("@PsychiatricDiseaseName", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.PsychiatricDiseaseName).ToString());
                        oUtility.AddParameters("@PsychiatricDiseaseDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.PsychiatricDiseaseDate).ToString());
                        oUtility.AddParameters("@PsychiatricDiseaseTreatment", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.PsychiatricDiseaseTreatment).ToString());
                        oUtility.AddParameters("@HematologicalDiseaseName", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.HematologicalDiseaseName).ToString());
                        oUtility.AddParameters("@HematologicalDiseaseDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.HematologicalDiseaseDate).ToString());
                        oUtility.AddParameters("@HematologicalDiseaseTreatment", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.HematologicalDiseaseTreatment).ToString());
                        oUtility.AddParameters("@GenitalUrinaryDiseaseName", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.GenitalUrinaryDiseaseName).ToString());
                        oUtility.AddParameters("@GenitalUrinaryDiseaseDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.GenitalUrinaryDiseaseDate).ToString());
                        oUtility.AddParameters("@GenitalUrinaryDiseaseTreatment", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.GenitalUrinaryDiseaseTreatment).ToString());
                        oUtility.AddParameters("@OphthamologyDiseaseName", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.OphthamologyDiseaseName).ToString());
                        oUtility.AddParameters("@OphthamologyDiseaseDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.OphthamologyDiseaseDate).ToString());
                        oUtility.AddParameters("@OphthamologyDiseaseTreatment", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.OphthamologyDiseaseTreatment).ToString());
                        oUtility.AddParameters("@ENTDiseaseName", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.ENTDiseaseName).ToString());
                        oUtility.AddParameters("@ENTDiseaseDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.ENTDiseaseDate).ToString());
                        oUtility.AddParameters("@ENTDiseaseTreatment", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.ENTDiseaseTreatment).ToString());
                        oUtility.AddParameters("@OtherDiseaseName", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.OtherDiseaseName).ToString());
                        oUtility.AddParameters("@OtherDiseaseDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.OtherDiseaseDate).ToString());

                        oUtility.AddParameters("@LMPassessmentValid", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.LMPassessmentValid).ToString());

                        oUtility.AddParameters("@EDD", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.EDD).ToString());
                        oUtility.AddParameters("@OtherDiseaseTreatment", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.OtherDiseaseTreatment).ToString());
                        oUtility.AddParameters("@SchoolPerfomance", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.SchoolPerfomance).ToString());

                        oUtility.AddParameters("@TBAssessmentICF", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.TBAssessementICF).ToString());
                        oUtility.AddParameters("@TBFindings", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.TBFindings).ToString());
                        oUtility.AddParameters("@TBresultsAvailable", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.TBresultsAvailable).ToString());
                        oUtility.AddParameters("@SputumSmear", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.SputumSmear).ToString());
                        oUtility.AddParameters("@SputumSmearDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.SputumSmearDate).ToString());
                        oUtility.AddParameters("@ChestXRay", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.ChestXRay).ToString());
                        oUtility.AddParameters("@ChestXRayDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.ChestXRayDate).ToString());
                        oUtility.AddParameters("@TissueBiopsy", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.TissueBiopsy).ToString());
                        oUtility.AddParameters("@TissueBiopsyDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.TissueBiopsyDate).ToString());
                        oUtility.AddParameters("@CXR", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.CXR).ToString());
                        oUtility.AddParameters("@OtherCXR", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.OtherCXR).ToString());

                        oUtility.AddParameters("@TBType", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.TBType).ToString());
                        oUtility.AddParameters("@TBPatientType", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.TBPatientType).ToString());
                        oUtility.AddParameters("@TBPlan", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.TBPlan).ToString());
                        oUtility.AddParameters("@TBPlanOther", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.TBPlanOther).ToString());
                        oUtility.AddParameters("@TBRegimen", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.TBRegimen).ToString());
                        oUtility.AddParameters("@TBRegimenStartDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.TBRegimenStartDate).ToString());
                        oUtility.AddParameters("@TBRegimenEndDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.TBRegimenEndDate).ToString());
                        oUtility.AddParameters("@TBReason", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.TBRegimenother).ToString());
                        oUtility.AddParameters("@TBTreatmentOutcomes", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.TBTreatmentOutcomes).ToString());

                        oUtility.AddParameters("@NoTB", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.NoTBSign).ToString());
                        //oUtility.AddParameters("@pyridoxine", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.pyridoxine).ToString());
                        oUtility.AddParameters("@INHStartDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.INHStartDate).ToString());
                        oUtility.AddParameters("@INHEndDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.INHEndDate).ToString());
                        oUtility.AddParameters("@PyridoxineStartDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.PyridoxineStartDate).ToString());
                        oUtility.AddParameters("@PyridoxineEndDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.PyridoxineEndDate).ToString());
                        //oUtility.AddParameters("@AdherenceAssessed", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.adherenceassessed).ToString());
                        //oUtility.AddParameters("@DosesMissed", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.dosesmissed).ToString());
                        //oUtility.AddParameters("@Adherencereferred", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.adherencereferred).ToString());
                        //oUtility.AddParameters("@DiscontinuedIPT", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.DiscontinueIPT).ToString());
                        oUtility.AddParameters("@SuspectTB", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.SuspectTB).ToString());
                        oUtility.AddParameters("@StopINHDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.StopINHDate).ToString());
                        oUtility.AddParameters("@ContactsScreenedForTB", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.ContactsScreenedForTB).ToString());
                        oUtility.AddParameters("@TBNotScreenedSpecify", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.TBNotScreenedSpecify).ToString());
                        oUtility.AddParameters("@LongTermMedications", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.LongTermMedications).ToString());

                        ///oUtility.AddParameters("@Cotrimoxazole", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.Cotrimoxazole).ToString());
                        oUtility.AddParameters("@HormonalContraceptivesDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.HormonalContraceptivesDate).ToString());
                        oUtility.AddParameters("@AntihypertensivesDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.AntihypertensivesDate).ToString());
                        oUtility.AddParameters("@HypoglycemicsDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.HypoglycemicsDate).ToString());
                        oUtility.AddParameters("@FluconazoleDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.Fluconazole).ToString());
                        oUtility.AddParameters("@AnticonvulsantsDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.AnticonvulsantsDate).ToString());
                        oUtility.AddParameters("@OtherLongTermMedications", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.OtherLongTermMedications).ToString());
                        oUtility.AddParameters("@OtherCurrentLongTermMedications", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.OtherCurrentLongTermMedications).ToString());

                        //oUtility.AddParameters("@HIVRelatedTests", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.HIVRelatedTest).ToString());
                        oUtility.AddParameters("@HIVRelatedHistory", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.HIVRelatedHistory).ToString());
                        oUtility.AddParameters("@InitialCD4", SqlDbType.Decimal, ConverTotValue.NullToInt(adultIEFields.InitialCD4).ToString());
                        oUtility.AddParameters("@InitialCD4Percent", SqlDbType.Decimal, ConverTotValue.NullToInt(adultIEFields.InitialCD4Percent).ToString());
                        oUtility.AddParameters("@InitialCD4Date", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.InitialCD4Date).ToString());
                        oUtility.AddParameters("@HighestCD4Ever", SqlDbType.Decimal, ConverTotValue.NullToInt(adultIEFields.HighestCD4Ever).ToString());
                        oUtility.AddParameters("@HighestCD4Percent", SqlDbType.Decimal, ConverTotValue.NullToInt(adultIEFields.HighestCD4Percent).ToString());
                        oUtility.AddParameters("@HighestCD4EverDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.HighestCD4EverDate).ToString());
                        oUtility.AddParameters("@CD4atARTInitiation", SqlDbType.Decimal, ConverTotValue.NullToInt(adultIEFields.CD4atARTInitiation).ToString());
                        oUtility.AddParameters("@CD4AtARTInitiationPercent", SqlDbType.Decimal, ConverTotValue.NullToInt(adultIEFields.CD4AtARTInitiationPercent).ToString());
                        oUtility.AddParameters("@CD4atARTInitiationDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.CD4atARTInitiationDate).ToString());
                        oUtility.AddParameters("@MostRecentCD4", SqlDbType.Decimal, ConverTotValue.NullToInt(adultIEFields.MostRecentCD4).ToString());
                        oUtility.AddParameters("@MostRecentCD4Percent", SqlDbType.Decimal, ConverTotValue.NullToInt(adultIEFields.MostRecentCD4Percent).ToString());
                        oUtility.AddParameters("@MostRecentCD4Date", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.MostRecentCD4Date).ToString());
                        oUtility.AddParameters("@PreviousViralLoad", SqlDbType.Decimal, ConverTotValue.NullToInt(adultIEFields.PreviousViralLoad).ToString());
                        oUtility.AddParameters("@PreviousViralLoadDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.PreviousViralLoadDate).ToString());
                        oUtility.AddParameters("@OtherHIVRelatedHistory", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.OtherHIVRelatedHistory).ToString());

                        oUtility.AddParameters("@AnyARVExposure", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.ARVExposure).ToString());
                        oUtility.AddParameters("@PMTC1StartDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.PMTC1StartDate).ToString());
                        oUtility.AddParameters("@PMTC1Regimen", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.PMTC1Regimen).ToString());
                        oUtility.AddParameters("@PEP1Regimen", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.PEP1Regimen).ToString());
                        oUtility.AddParameters("@PEP1StartDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.PEP1StartDate).ToString());
                        oUtility.AddParameters("@HAART1Regimen", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.HAART1Regimen).ToString());
                        oUtility.AddParameters("@HAART1StartDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.HAART1StartDate).ToString());
                        oUtility.AddParameters("@ARVExposerdosesmissed", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.ARVExposerdosesmissed).ToString());
                        oUtility.AddParameters("@ARVExposerdelaydoses", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.ARVExposerdelaydoses).ToString());
                        oUtility.AddParameters("@Impression", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.Impression).ToString());
                        oUtility.AddParameters("@Diagnosis", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.Diagnosis).ToString());

                        //oUtility.AddParameters("@HIVRelatedOI", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.HIVRelatedOI).ToString()); Fields are handled in Multiselect
                        //oUtility.AddParameters("@NonHIVRelatedOI", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.NonHIVRelatedOI).ToString()); Fields are handled in Multiselect
                        //oUtility.AddParameters("@InitialEvaluation", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.InitialEvaluation).ToString());
                        oUtility.AddParameters("@WHOStageIConditions", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.WHOStageIConditions).ToString());
                        oUtility.AddParameters("@WHOStageIIConditions", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.WHOStageIIConditions).ToString());
                        oUtility.AddParameters("@WHOStageIIIConditions", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.WHOStageIIIConditions).ToString());
                        oUtility.AddParameters("@WHOStageIVConditions", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.WHOStageIVConditions).ToString());
                        oUtility.AddParameters("@InitiationWHOstage", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.InitiationWHOstage).ToString());
                        oUtility.AddParameters("@WHOStage", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.WHOStage).ToString());
                        oUtility.AddParameters("@WABStage", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.WABStage).ToString());
                        oUtility.AddParameters("@TannerStaging", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.TannerStaging).ToString());
                        oUtility.AddParameters("@Mernarche", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.Mernarche).ToString());
                        oUtility.AddParameters("@MernarcheDate", SqlDbType.DateTime, ConverTotValue.NullToInt(adultIEFields.Mernarchedate).ToString());


                        oUtility.AddParameters("@SpecifyAntibioticAllery", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.SpecifyAntibioticAllery).ToString());
                        oUtility.AddParameters("@OtherDrugAllergy", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.OtherDrugAllergy).ToString());
                        //oUtility.AddParameters("@SpecifyARVAllergy", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.ARVDrugAllergy).ToString());
                        //oUtility.AddParameters("@SulfaTMPDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.SulfaTMPDate).ToString());
                        //oUtility.AddParameters("@DrugAllergiesToxicitiesPaeds", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.DrugAllergiesToxicitiesPaeds).ToString());
                        oUtility.AddParameters("@ARVSideEffects", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.AnyARVSideEffects).ToString());

                        oUtility.AddParameters("@ShortTermEffects", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.ShortTermEffects).ToString());
                        //oUtility.AddParameters("@OtherShortTermEffects", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.OtherShortTermEffects).ToString()); //handled in multiselect
                        oUtility.AddParameters("@LongTermEffects", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.LongTermEffects).ToString());
                        //oUtility.AddParameters("@OtherLongtermEffects", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.OtherLongtermEffects).ToString()); //handled in multiselect
                        oUtility.AddParameters("@WorkUpPlan", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.WorkUpPlan).ToString());

                        oUtility.AddParameters("@LabEvaluation", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.LabEvaluation).ToString());
                        oUtility.AddParameters("@SpecifyLabEvaluation", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.LabEvaluationsub).ToString());
                        // oUtility.AddParameters("@Counselling", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.Counselling).ToString()); //handled in multiselect
                        // oUtility.AddParameters("@OtherCounselling", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.OtherCounselling).ToString()); //handled in multiselect

                        oUtility.AddParameters("@WardAdmission", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.WardAdmission).ToString());
                        oUtility.AddParameters("@ReferToSpecialistClinic", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.ReferToSpecialistClinic).ToString());
                        oUtility.AddParameters("@TransferOut", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.TransferOut).ToString());
                        oUtility.AddParameters("@ARTTreatmentPlan", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.ARTTreatmentPlan).ToString());
                        oUtility.AddParameters("@SwitchReason", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.SwitchReason).ToString());
                        oUtility.AddParameters("@StartART", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.StartART).ToString());
                        oUtility.AddParameters("@ARTEligibilityCriteria", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.ARTEligibilityCriteria).ToString());
                        oUtility.AddParameters("@OtherARTEligibilityCriteria", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.OtherARTEligibilityCriteria).ToString());

                        oUtility.AddParameters("@SubstituteRegimen", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.SubstituteRegimen).ToString());
                        oUtility.AddParameters("@NumberDrugsSubstituted", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.NumberDrugsSubstituted).ToString());
                        oUtility.AddParameters("@StopTreatment", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.StopTreatment).ToString());
                        oUtility.AddParameters("@StopTreatmentCodes", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.StopTreatmentCodes).ToString());
                        oUtility.AddParameters("@RegimenPrescribed", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.RegimenPrescribed).ToString());
                        oUtility.AddParameters("@OtherRegimenPrescribed", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.OtherRegimenPrescribed).ToString());
                        oUtility.AddParameters("@OIProphylaxis", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.OIProphylaxis).ToString());
                        oUtility.AddParameters("@ReasonCTXPrescribed", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.ReasonCoTrimoxPrescribed).ToString());
                        oUtility.AddParameters("@OtherTreatment", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.OtherTreatment).ToString());

                        oUtility.AddParameters("@SexualActiveness", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.SexualActiveness).ToString());
                        oUtility.AddParameters("@SexualOrientation", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.SexualOrientation).ToString());
                        oUtility.AddParameters("@HighRisk", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.HighRisk).ToString());
                        oUtility.AddParameters("@KnowSexualPartnerHIVStatus", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.KnowSexualPartnerHIVStatus).ToString());
                        oUtility.AddParameters("@ParnerHIVStatus", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.PartnerHIVStatus).ToString());

                        oUtility.AddParameters("@GivenPWPMessages", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.GivenPWPMessages).ToString());
                        oUtility.AddParameters("@SaferSexImportanceExplained", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.SaferSexImportanceExplained).ToString());
                        oUtility.AddParameters("@UnsafeSexImportanceExplained", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.UnsafeSexImportanceExplained).ToString());
                        oUtility.AddParameters("@LMPDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.LMPDate).ToString());
                        oUtility.AddParameters("@LMPNotaccessedReason", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.LMPNotaccessedReason).ToString());

                        oUtility.AddParameters("@PDTDone", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.PDTDone).ToString());
                        oUtility.AddParameters("@Pregnant", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.ClientPregnant).ToString());
                        oUtility.AddParameters("@PMTCTOffered", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.PMTCTOffered).ToString());
                        oUtility.AddParameters("@IntentionOfPregnancy", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.IntentionOfPregnancy).ToString());
                        oUtility.AddParameters("@DiscussedFertilityOptions", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.DiscussedFertilityOptions).ToString());
                        oUtility.AddParameters("@DiscussedDualContraception", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.DiscussedDualContraception).ToString());
                        oUtility.AddParameters("@CondomsIssued", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.CondomsIssued).ToString());
                        oUtility.AddParameters("@CondomNotIssued", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.ReasonCondomNotIssued).ToString());
                        oUtility.AddParameters("@STIScreened", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.STIScreened).ToString());
                        oUtility.AddParameters("@VaginalDischarge", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.VaginalDischarge).ToString());
                        oUtility.AddParameters("@UrethralDischarge", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.UrethralDischarge).ToString());
                        oUtility.AddParameters("@GenitalUlceration", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.GenitalUlceration).ToString());
                        oUtility.AddParameters("@STITreatmentPlan", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.STITreatmentPlan).ToString());
                        oUtility.AddParameters("@OnFP", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.OnFP).ToString());
                        oUtility.AddParameters("@FPMethod", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.FPMethod).ToString());
                        oUtility.AddParameters("@CervicalCancerScreened", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.CervicalCancerScreened).ToString());
                        oUtility.AddParameters("@CervicalCancerScreeningResults", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.CervicalCancerScreeningResults).ToString());
                        oUtility.AddParameters("@ReferredForCervicalCancerScreening", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.ReferredForCervicalCancerScreening).ToString());
                        oUtility.AddParameters("@HPVOffered", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.HPVOffered).ToString());
                        oUtility.AddParameters("@OfferedHPVVaccine", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.OfferedHPVVaccine).ToString());
                        oUtility.AddParameters("@HPVDoseDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.HPVDoseDate).ToString());
                        oUtility.AddParameters("@RefferedToFupF", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.RefferedToFupF).ToString());
                        oUtility.AddParameters("@SpecifyOtherRefferedTo", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.SpecifyOtherRefferedTo).ToString());
                        oUtility.AddParameters("@SignatureID", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.SignatureID).ToString());
                        oUtility.AddParameters("@VisitDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.VisitDate).ToString());
                        theDT = (DataTable)expressManagerTest.ReturnObjectNewImpl(oUtility.theParams, "Pr_Clinical_AddAdultIE", ClsDBUtility.ObjectEnum.DataTable);
                        StringBuilder Insertcbl = new StringBuilder();
                        oUtility.Init_Hashtable();
                        StringBuilder UpdateStrMultiselect = new StringBuilder();
                        if (Convert.ToInt32(adultIEFields.VisitPk) > 0)
                        {
                            UpdateStrMultiselect.Append("Delete from dtl_Multiselect_line where ptn_pk=" + adultIEFields.PtnPk + " and Visit_Pk=" + adultIEFields.VisitPk + " ");
                            UpdateStrMultiselect.Append(strMultiselect);
                            oUtility.AddParameters("@Insert", SqlDbType.VarChar, UpdateStrMultiselect.ToString());
                        }
                        else
                        {
                            oUtility.AddParameters("@Insert", SqlDbType.VarChar, strMultiselect.ToString());
                        }
                        theRowAffected = (int)expressManagerTest.ReturnObject(oUtility.theParams, "pr_Clinical_SaveCustomForm_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return theDT;
            }


            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);

            }

        }

        public DataTable SaveAdultIE_TriageTab(List<BIQAdultIE> lstadultIEFields, StringBuilder strMultiselect)
        {
            ClsObject expressManagerTest = new ClsObject();
            int theRowAffected = 0;
            DataTable theDT = new DataTable();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                expressManagerTest.Connection = this.Connection;
                expressManagerTest.Transaction = this.Transaction;

                if (lstadultIEFields.Count > 0)
                {
                    foreach (var adultIEFields in lstadultIEFields)
                    {

                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@ID", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.ID).ToString());
                        oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.PtnPk).ToString());
                        oUtility.AddParameters("@VisitId", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.VisitPk).ToString());
                        oUtility.AddParameters("@LocationId", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.LocationId).ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.UserId).ToString());
                        oUtility.AddParameters("@Temperature", SqlDbType.Decimal, ConverTotValue.NullToInt(adultIEFields.Temperature).ToString());
                        oUtility.AddParameters("@RespirationRate", SqlDbType.Decimal, ConverTotValue.NullToInt(adultIEFields.RespirationRate).ToString());
                        oUtility.AddParameters("@HeartRate", SqlDbType.Decimal, ConverTotValue.NullToInt(adultIEFields.HeartRate).ToString());
                        oUtility.AddParameters("@SystolicBloodPressure", SqlDbType.Decimal, ConverTotValue.NullToInt(adultIEFields.SystolicBloodPressure).ToString());
                        oUtility.AddParameters("@DiastolicBloodPressure", SqlDbType.Decimal, ConverTotValue.NullToInt(adultIEFields.DiastolicBloodPressure).ToString());
                        oUtility.AddParameters("@Height", SqlDbType.Decimal, ConverTotValue.NullToInt(adultIEFields.Height).ToString());
                        oUtility.AddParameters("@Weight", SqlDbType.Decimal, ConverTotValue.NullToInt(adultIEFields.Weight).ToString());
                        oUtility.AddParameters("@HeadCircum", SqlDbType.Decimal, ConverTotValue.NullToInt(adultIEFields.txtheadcircumference).ToString());
                        //oUtility.AddParameters("@WeightForAge", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.ddlweightforage).ToString());
                        //oUtility.AddParameters("@WeightForHeight", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.txtweightforheight).ToString());
                        oUtility.AddParameters("@DiagnosisConfirmed", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.DiagnosisConfirmed).ToString());
                        oUtility.AddParameters("@ConfirmHIVPosDate", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}", ConverTotValue.NullToDate(adultIEFields.ConfirmHIVPosDate).ToString()));
                        oUtility.AddParameters("@ChildAccompaniedByCaregiver", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.ChildAccompaniedByCaregiver).ToString());
                        oUtility.AddParameters("@TreatmentSupporterRelationship", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.TreatmentSupporterRelationship).ToString());
                        oUtility.AddParameters("@HealthEducation", SqlDbType.Bit, ConverTotValue.NullToBoolean(adultIEFields.HealthEducation).ToString());
                        oUtility.AddParameters("@DisclosureStatus", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.DisclosureStatus).ToString());
                        oUtility.AddParameters("@Reasondisclosed", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.reasonnotdisclosed).ToString());

                        oUtility.AddParameters("@OtherDisclosurestatus", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.otherdisclosurestatus).ToString());
                        oUtility.AddParameters("@SchoolingStatus", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.SchoolingStatus).ToString());
                        oUtility.AddParameters("@HighestLevelattained", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.Highestlevelattained).ToString());
                        oUtility.AddParameters("@HIVSupportgroup", SqlDbType.Bit, ConverTotValue.NullToBoolean(adultIEFields.HIVSupportgroup).ToString());
                        oUtility.AddParameters("@HIVSupportGroupMembership", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.supportgroupmembership).ToString());
                        oUtility.AddParameters("@PatientReferredFrom", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.PatientReferredFrom).ToString());
                        oUtility.AddParameters("@PatientReferredFromOthers", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.OtherPatientReferredFrom).ToString());
                        oUtility.AddParameters("@NursesComments", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.NursesComments).ToString());

                        oUtility.AddParameters("@ReferToSpecialistClinic", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.ReferToSpecialistClinic).ToString());

                        oUtility.AddParameters("@SpecifyOtherRefferedTo", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.SpecifyOtherRefferedTo).ToString());
                        oUtility.AddParameters("@SignatureID", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.SignatureID).ToString());
                        oUtility.AddParameters("@VisitDate", SqlDbType.VarChar, ConverTotValue.NullToDate(adultIEFields.VisitDate).ToString());
                        oUtility.AddParameters("@StartTime", SqlDbType.VarChar, DateTime.Now.ToString("dd-MMM-yyyy"));
                        oUtility.AddParameters("@FeatureID", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.AdultIEFeatureID).ToString());

                        theDT = (DataTable)expressManagerTest.ReturnObjectNewImpl(oUtility.theParams, "Pr_clinical_addadultie_triagetab", ClsDBUtility.ObjectEnum.DataTable);


                        oUtility.Init_Hashtable();

                        oUtility.AddParameters("@Insert", SqlDbType.VarChar, strMultiselect.ToString());

                        theRowAffected = (int)expressManagerTest.ReturnObject(oUtility.theParams, "pr_Clinical_SaveCustomForm_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return theDT;
            }


            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);

            }

        }
        public DataTable SaveAdultIE_CATab(List<BIQAdultIE> lstadultIEFields, StringBuilder strMultiselect)
        {
            ClsObject expressManagerTest = new ClsObject();
            int theRowAffected = 0;
            DataTable theDT = new DataTable();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                expressManagerTest.Connection = this.Connection;
                expressManagerTest.Transaction = this.Transaction;

                if (lstadultIEFields.Count > 0)
                {
                    foreach (var adultIEFields in lstadultIEFields)
                    {

                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@ID", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.ID).ToString());
                        oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.PtnPk).ToString());
                        oUtility.AddParameters("@VisitId", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.VisitPk).ToString());
                        oUtility.AddParameters("@LocationId", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.LocationId).ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.UserId).ToString());
                        oUtility.AddParameters("@OtherPresentingcomplaints", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.otherspecifiedcomplaints).ToString());
                        oUtility.AddParameters("@AdditionalComplaints", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.Additionalcomplaints).ToString());
                        oUtility.AddParameters("@OtherMedicalConditions", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.otherspecifiedcomplaints).ToString());
                        oUtility.AddParameters("@MedHistoryFP", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.MedHistoryFP).ToString());
                        oUtility.AddParameters("@MedHistoryLastFP", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.MedHistoryLastFP).ToString());
                        //oUtility.AddParameters("@SignatureID", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.SignatureID).ToString());
                        oUtility.AddParameters("@VisitDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.VisitDate).ToString());
                        oUtility.AddParameters("@StartTime", SqlDbType.DateTime, DateTime.Now.ToString());
                        oUtility.AddParameters("@FeatureID", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.AdultIEFeatureID).ToString());
                        theDT = (DataTable)expressManagerTest.ReturnObjectNewImpl(oUtility.theParams, "Pr_clinical_addadultie_CATab", ClsDBUtility.ObjectEnum.DataTable);

                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@Insert", SqlDbType.VarChar, strMultiselect.ToString());

                        theRowAffected = (int)expressManagerTest.ReturnObject(oUtility.theParams, "pr_Clinical_SaveCustomForm_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return theDT;
            }


            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);

            }

        }
        public DataTable SaveAdultIE_ExamTab(List<BIQAdultIE> lstadultIEFields, StringBuilder strMultiselect)
        {
            ClsObject expressManagerTest = new ClsObject();
            int theRowAffected = 0;
            DataTable theDT = new DataTable();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                expressManagerTest.Connection = this.Connection;
                expressManagerTest.Transaction = this.Transaction;

                if (lstadultIEFields.Count > 0)
                {
                    foreach (var adultIEFields in lstadultIEFields)
                    {

                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@ID", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.ID).ToString());
                        oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.PtnPk).ToString());
                        oUtility.AddParameters("@VisitId", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.VisitPk).ToString());
                        oUtility.AddParameters("@LocationId", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.LocationId).ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.UserId).ToString());

                        oUtility.AddParameters("@OtherLongTermMedications", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.OtherLongTermMedications).ToString());
                        oUtility.AddParameters("@OtherGeneralConditions", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.OtherGeneralConditions).ToString());
                        oUtility.AddParameters("@OtherAbdomenConditions", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.OtherAbdomenConditions).ToString());
                        oUtility.AddParameters("@OtherCardiovascularConditions", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.OtherCardiovascularConditions).ToString());
                        oUtility.AddParameters("@OtherOralCavityConditions", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.OtherOralCavityConditions).ToString());
                        oUtility.AddParameters("@OtherGenitourinaryConditions", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.OtherGenitourinaryConditions).ToString());
                        oUtility.AddParameters("@OtherCNSConditions", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.OtherCNSConditions).ToString());
                        oUtility.AddParameters("@OtherChestLungsConditions", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.OtherChestLungsConditions).ToString());
                        oUtility.AddParameters("@OtherSkinConditions", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.OtherSkinConditions).ToString());
                        oUtility.AddParameters("@OtherMedicalConditionNotes", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.Additionalmedicalconditionnotes).ToString());

                        oUtility.AddParameters("@InitialCD4", SqlDbType.Decimal, ConverTotValue.NullToInt(adultIEFields.InitialCD4).ToString());
                        oUtility.AddParameters("@InitialCD4Percent", SqlDbType.Decimal, ConverTotValue.NullToInt(adultIEFields.InitialCD4Percent).ToString());
                        if ((adultIEFields.InitialCD4Date != null) || (adultIEFields.InitialCD4Date.ToString() != ""))
                            oUtility.AddParameters("@InitialCD4Date", SqlDbType.DateTime, Convert.ToDateTime(adultIEFields.InitialCD4Date).Date.ToString("dd-MMM-yyyy"));
                        else
                            oUtility.AddParameters("@InitialCD4Date", SqlDbType.DateTime, String.Format("{0:dd-MMM-yyyy}", ConverTotValue.NullToDate(adultIEFields.InitialCD4Date).ToString()));
                        oUtility.AddParameters("@HighestCD4Ever", SqlDbType.Decimal, ConverTotValue.NullToInt(adultIEFields.HighestCD4Ever).ToString());
                        oUtility.AddParameters("@HighestCD4Percent", SqlDbType.Decimal, ConverTotValue.NullToInt(adultIEFields.HighestCD4Percent).ToString());
                        if ((adultIEFields.HighestCD4EverDate != null) || (adultIEFields.HighestCD4EverDate.ToString() != ""))
                            oUtility.AddParameters("@HighestCD4EverDate", SqlDbType.DateTime, Convert.ToDateTime(adultIEFields.HighestCD4EverDate).Date.ToString("dd-MMM-yyyy"));
                        else
                            oUtility.AddParameters("@HighestCD4EverDate", SqlDbType.DateTime, String.Format("{0:dd-MMM-yyyy}", ConverTotValue.NullToDate(adultIEFields.HighestCD4EverDate).ToString()));
                        oUtility.AddParameters("@CD4atARTInitiation", SqlDbType.Decimal, ConverTotValue.NullToInt(adultIEFields.CD4atARTInitiation).ToString());
                        oUtility.AddParameters("@CD4AtARTInitiationPercent", SqlDbType.Decimal, ConverTotValue.NullToInt(adultIEFields.CD4AtARTInitiationPercent).ToString());
                        if ((adultIEFields.CD4atARTInitiationDate != null) || (adultIEFields.CD4atARTInitiationDate.ToString() != ""))
                            oUtility.AddParameters("@CD4atARTInitiationDate", SqlDbType.DateTime, Convert.ToDateTime(adultIEFields.CD4atARTInitiationDate).Date.ToString("dd-MMM-yyyy"));
                        else
                            oUtility.AddParameters("@CD4atARTInitiationDate", SqlDbType.DateTime, String.Format("{0:dd-MMM-yyyy}", ConverTotValue.NullToDate(adultIEFields.CD4atARTInitiationDate).ToString()));

                        oUtility.AddParameters("@MostRecentCD4", SqlDbType.Decimal, ConverTotValue.NullToInt(adultIEFields.MostRecentCD4).ToString());
                        oUtility.AddParameters("@MostRecentCD4Percent", SqlDbType.Decimal, ConverTotValue.NullToInt(adultIEFields.MostRecentCD4Percent).ToString());
                        if ((adultIEFields.MostRecentCD4Date != null) || (adultIEFields.MostRecentCD4Date.ToString() != ""))
                            oUtility.AddParameters("@MostRecentCD4Date", SqlDbType.DateTime, Convert.ToDateTime(adultIEFields.MostRecentCD4Date).Date.ToString("dd-MMM-yyyy"));
                        else
                            oUtility.AddParameters("@MostRecentCD4Date", SqlDbType.DateTime, String.Format("{0:dd-MMM-yyyy}", ConverTotValue.NullToDate(adultIEFields.MostRecentCD4Date).ToString()));
                        //oUtility.AddParameters("@MostRecentCD4Date", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}", ConverTotValue.NullToDate(adultIEFields.MostRecentCD4Date).ToString()));
                        oUtility.AddParameters("@PreviousViralLoad", SqlDbType.Decimal, ConverTotValue.NullToInt(adultIEFields.PreviousViralLoad).ToString());
                        if ((adultIEFields.PreviousViralLoadDate != null) || (adultIEFields.PreviousViralLoadDate.ToString() != ""))
                            oUtility.AddParameters("@PreviousViralLoadDate", SqlDbType.DateTime, Convert.ToDateTime(adultIEFields.PreviousViralLoadDate).Date.ToString("dd-MMM-yyyy"));
                        else
                            oUtility.AddParameters("@PreviousViralLoadDate", SqlDbType.DateTime, String.Format("{0:dd-MMM-yyyy}", ConverTotValue.NullToDate(adultIEFields.PreviousViralLoadDate).ToString()));


                        oUtility.AddParameters("@OtherHIVRelatedHistory", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.OtherHIVRelatedHistory).ToString());
                        if ((adultIEFields.PMTC1StartDate != null) || (adultIEFields.PMTC1StartDate.ToString() != ""))
                            oUtility.AddParameters("@PMTC1StartDate", SqlDbType.DateTime, Convert.ToDateTime(adultIEFields.PMTC1StartDate).Date.ToString("dd-MMM-yyyy"));
                        else
                            oUtility.AddParameters("@PMTC1StartDate", SqlDbType.DateTime, String.Format("{0:dd-MMM-yyyy}", ConverTotValue.NullToDate(adultIEFields.PMTC1StartDate).ToString()));


                        oUtility.AddParameters("@PMTC1Regimen", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.PMTC1Regimen).ToString());
                        oUtility.AddParameters("@PEP1Regimen", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.PEP1Regimen).ToString());
                        if ((adultIEFields.PEP1StartDate != null) || (adultIEFields.PEP1StartDate.ToString() != ""))
                            oUtility.AddParameters("@PEP1StartDate", SqlDbType.DateTime, Convert.ToDateTime(adultIEFields.PEP1StartDate).Date.ToString("dd-MMM-yyyy"));
                        else
                            oUtility.AddParameters("@PEP1StartDate", SqlDbType.DateTime, String.Format("{0:dd-MMM-yyyy}", ConverTotValue.NullToDate(adultIEFields.PEP1StartDate).ToString()));


                        oUtility.AddParameters("@HAART1Regimen", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.HAART1Regimen).ToString());
                        if ((adultIEFields.HAART1StartDate != null) || (adultIEFields.HAART1StartDate.ToString() != ""))
                            oUtility.AddParameters("@HAART1StartDate", SqlDbType.DateTime, Convert.ToDateTime(adultIEFields.HAART1StartDate).Date.ToString("dd-MMM-yyyy"));
                        else
                            oUtility.AddParameters("@HAART1StartDate", SqlDbType.DateTime, String.Format("{0:dd-MMM-yyyy}", ConverTotValue.NullToDate(adultIEFields.HAART1StartDate).ToString()));


                        oUtility.AddParameters("@ARVExposerdosesmissed", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.ARVExposerdosesmissed).ToString());
                        oUtility.AddParameters("@ARVExposerdelaydoses", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.ARVExposerdelaydoses).ToString());
                        oUtility.AddParameters("@Impression", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.Impression).ToString());
                        oUtility.AddParameters("@HIVRelatedOI", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.HIVRelatedOI).ToString());

                        oUtility.AddParameters("@ProgressionInWHOstage", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.ProgressionInWHOstage).ToString());
                        oUtility.AddParameters("@SpecifyWHOprogression", SqlDbType.VarChar, ConverTotValue.NullToInt(adultIEFields.SpecifyWHOprogression).ToString());

                        oUtility.AddParameters("@NonHIVRelatedOI", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.NonHIVRelatedOI).ToString());
                        oUtility.AddParameters("@WHOStage", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.WHOStage).ToString());
                        oUtility.AddParameters("@WABStage", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.WABStage).ToString());
                        oUtility.AddParameters("@TannerStaging", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.TannerStaging).ToString());
                        oUtility.AddParameters("@Mernarche", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.Mernarche).ToString());
                        if ((adultIEFields.Mernarchedate != null) || (adultIEFields.Mernarchedate.ToString() != ""))
                            oUtility.AddParameters("@MernarcheDate", SqlDbType.DateTime, Convert.ToDateTime(adultIEFields.Mernarchedate).Date.ToString("dd-MMM-yyyy"));
                        else
                            oUtility.AddParameters("@MernarcheDate", SqlDbType.DateTime, String.Format("{0:dd-MMM-yyyy}", ConverTotValue.NullToDate(adultIEFields.Mernarchedate).ToString()));

                        oUtility.AddParameters("@StartTime", SqlDbType.DateTime, String.Format("{0:dd-MMM-yyyy hh:mm:ss}", DateTime.Now));
                        oUtility.AddParameters("@FeatureID", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.AdultIEFeatureID).ToString());
                        theDT = (DataTable)expressManagerTest.ReturnObjectNewImpl(oUtility.theParams, "Pr_clinical_addadultie_ExamTab", ClsDBUtility.ObjectEnum.DataTable);

                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@Insert", SqlDbType.VarChar, strMultiselect.ToString());

                        theRowAffected = (int)expressManagerTest.ReturnObject(oUtility.theParams, "pr_Clinical_SaveCustomForm_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return theDT;
            }


            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);

            }

        }
        public DataTable SaveAdultIE_MgtTab(List<BIQAdultIE> lstadultIEFields, StringBuilder strMultiselect)
        {
            ClsObject expressManagerTest = new ClsObject();
            int theRowAffected = 0;
            DataTable theDT = new DataTable();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                expressManagerTest.Connection = this.Connection;
                expressManagerTest.Transaction = this.Transaction;

                if (lstadultIEFields.Count > 0)
                {
                    foreach (var adultIEFields in lstadultIEFields)
                    {

                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@ID", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.ID).ToString());
                        oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.PtnPk).ToString());
                        oUtility.AddParameters("@VisitId", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.VisitPk).ToString());
                        oUtility.AddParameters("@LocationId", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.LocationId).ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.UserId).ToString());

                        oUtility.AddParameters("@OtherShortTermEffects", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.OtherShortTermEffects).ToString());
                        oUtility.AddParameters("@OtherLongtermEffects", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.OtherLongtermEffects).ToString());
                        oUtility.AddParameters("@WorkUpPlan", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.WorkUpPlan).ToString());
                        oUtility.AddParameters("@SpecifyLabEvaluation", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.OtherLabReview).ToString());

                        oUtility.AddParameters("@ARTTreatmentPlan", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.ARTTreatmentPlan).ToString());
                        oUtility.AddParameters("@OtherARTEligibilityCriteria", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.OtherARTEligibilityCriteria).ToString());
                        oUtility.AddParameters("@NumberDrugsSubstituted", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.NumberDrugsSubstituted).ToString());
                        oUtility.AddParameters("@SpecifyotherARTchangereason", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.OtherARTChangeCode).ToString());
                        oUtility.AddParameters("@OtherARTStopCode", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.OtherARTStopCode).ToString());
                        oUtility.AddParameters("@secondLineRegimenSwitch", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.SubstituteRegimen).ToString());
                        //oUtility.AddParameters("@OtherRegimenPrescribed", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.OtherRegimenPrescribed).ToString());
                        oUtility.AddParameters("@OIProphylaxis", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.OIProphylaxis).ToString());
                        oUtility.AddParameters("@ReasonCTXPrescribed", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.ReasonCoTrimoxPrescribed).ToString());
                        oUtility.AddParameters("@OtherTreatment", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.OtherTreatment).ToString());
                        oUtility.AddParameters("@Fluconazole", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.ReasonFluconazolePrescribed).ToString());
                        oUtility.AddParameters("@OtherOIProphylaxis", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.SexualOrientation).ToString());
                        oUtility.AddParameters("@signature", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.SignatureID).ToString());
                        oUtility.AddParameters("@VisitDate", SqlDbType.DateTime, String.Format("{0:dd-MMM-yyyy}", ConverTotValue.NullToDate(adultIEFields.VisitDate).ToString()));
                        oUtility.AddParameters("@StartTime", SqlDbType.DateTime, String.Format("{0:dd-MMM-yyyy hh:mm:ss}", DateTime.Now));
                        oUtility.AddParameters("@FeatureID", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.AdultIEFeatureID).ToString());
                        theDT = (DataTable)expressManagerTest.ReturnObjectNewImpl(oUtility.theParams, "Pr_clinical_addadultie_MgtTab", ClsDBUtility.ObjectEnum.DataTable);

                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@Insert", SqlDbType.VarChar, strMultiselect.ToString());
                        theRowAffected = (int)expressManagerTest.ReturnObject(oUtility.theParams, "pr_Clinical_SaveCustomForm_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return theDT;
            }


            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);

            }

        }
        public DataTable GetDataTable(string Mastertablename, string fieldname)
        {
            oUtility.Init_Hashtable();
            oUtility.AddParameters("@Mastertablename", SqlDbType.VarChar, Mastertablename.ToString());
            oUtility.AddParameters("@fieldname", SqlDbType.VarChar, fieldname.ToString());
            ClsObject GetRecs = new ClsObject();
            DataTable regDT = (DataTable)GetRecs.ReturnObject(oUtility.theParams, "Pr_Clinical_GetListDataTable", ClsDBUtility.ObjectEnum.DataTable);
            return regDT;
        }
    }
}
