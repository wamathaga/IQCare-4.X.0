using System;
using System.Data;
using Application.Common;
using Entities.Common;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using HIVCE.Common;
using Interface.Clinical;


namespace BusinessProcess.Clinical
{  
    public class BHTSClientIntake : ProcessBase, IHTSClientIntake
    {

        ClsDBUtility oUtility = new ClsDBUtility();

        public HTSClientIntake GetClientIntakeFormData(int ptn_pk, int locationID)
        {
           // oUtility = new ClsDBUtility();
            oUtility.Init_Hashtable();

            oUtility.AddParameters("@PatientID", SqlDbType.Int, ConverTotValue.NullToInt(ptn_pk).ToString());
            
            ClsObject GetRecs = new ClsObject();
            DataTable dtHTSCIF = (DataTable) GetRecs.ReturnObject(oUtility.theParams, "pr_GetHTSClientIntakeForPatient", ClsDBUtility.ObjectEnum.DataTable);

            HTSClientIntake objClientIntake = new HTSClientIntake();
            try
            {
                if (dtHTSCIF.Rows.Count > 0)
                {
                    objClientIntake.CIFID = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["CIFID"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["CIFID"].ToString())
                        : 0;
                    objClientIntake.PatientId = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["ptn_pk"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["ptn_pk"].ToString())
                        : 0;
                    objClientIntake.ReferFrom = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["ReferredFrom"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["ReferredFrom"])
                        : 0;
                    objClientIntake.TypeOfSession = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["TypeOfSession"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["TypeOfSession"])
                        : 0;
                    objClientIntake.Setting = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["Setting"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["Setting"].ToString())
                        : 0;
                    objClientIntake.IsClientIsIndex = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["IsClientIsIndex"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["IsClientIsIndex"])
                        : 0;
                    objClientIntake.IndexTestingType = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["IndexTestingType"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["IndexTestingType"])
                        : 0;
                    objClientIntake.IndexClientID = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["ClientIndexID"].ToString()) == false
                        ? dtHTSCIF.Rows[0]["ClientIndexID"].ToString()
                        :"";
                    objClientIntake.DateOfVisit = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["DateOfVisit"].ToString()) == false
                        ? Convert.ToDateTime(dtHTSCIF.Rows[0]["DateOfVisit"])
                        : (DateTime?)null;
                    objClientIntake.PatientState = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["StateOfResidence"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["StateOfResidence"])
                        : 0;
                    objClientIntake.PatientLga = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["LGAOfResidence"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["LGAOfResidence"])
                        : 0;

                    objClientIntake.PatientCode = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["PatientCode"].ToString()) == false
                        ? dtHTSCIF.Rows[0]["PatientCode"].ToString()
                        : String.Empty;
                    objClientIntake.FirstTimeVisit = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["FirstTimeVisit"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["FirstTimeVisit"])
                        : 0;
                    objClientIntake.PatientGender = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["PatientGender"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["PatientGender"])
                        : 0;
                    objClientIntake.PatientState = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["PatientState"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["PatientState"])
                        : 0;
                    objClientIntake.PatientLga = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["PatientLga"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["PatientLga"])
                        : 0;
                    objClientIntake.PatientMaritalStat = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["PatientMaritalStat"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["PatientMaritalStat"])
                        : 0;
                    objClientIntake.NoOfChildLessThan5 = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["NoOfChildLessThan5"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["NoOfChildLessThan5"])
                        : 0;
                    objClientIntake.NoOfWives = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["NoOfWives"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["NoOfWives"].ToString())
                        : 0;
                    objClientIntake.NoOfChildLessThan5 = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["NoOfChildLessThan5"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["NoOfChildLessThan5"])
                        : 0;
                    objClientIntake.PrevTestedHivNeg = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["PrevTestedHivNeg"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["PrevTestedHivNeg"])
                        : 0;objClientIntake.PrevTestedHivNeg = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["PrevTestedHivNeg"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["PrevTestedHivNeg"])
                        : 0;
                    objClientIntake.ClientPregnant = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["ClientPregnant"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["ClientPregnant"])
                        : 0;
                    objClientIntake.InformHivTransRoute = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["InformHivTransRoute"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["InformHivTransRoute"])
                        : 0;
                    objClientIntake.InformHivTransRiskFactor = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["InformRiskFactorForHIVTrans"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["InformRiskFactorForHIVTrans"])
                        : 0;
                    objClientIntake.InformHivTransMethod = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["InformHivTransMethod"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["InformHivTransMethod"])
                        : 0;
                    objClientIntake.InformHivPossiResults = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["InformHivPossiResults"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["InformHivPossiResults"])
                        : 0;
                    objClientIntake.InformConsentHIVTesting = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["InformConsentHIVTesting"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["InformConsentHIVTesting"])
                        : 0;
                    objClientIntake.EvrHadSex = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["EvrHadSex"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["EvrHadSex"])
                        : 0;
                    objClientIntake.BlodTransIn3Mnth = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["BlodTransIn3Mnth"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["BlodTransIn3Mnth"])
                        : 0;
                    objClientIntake.SexWithCasPartnr = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["SexWithCasPartnr"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["SexWithCasPartnr"])
                        : 0;
                    objClientIntake.SexWithRegPartnr = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["SexWithRegPartnr"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["SexWithRegPartnr"])
                        : 0;
                    objClientIntake.LastStiDone = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["LastStiDone"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["LastStiDone"])
                        : 0;
                    objClientIntake.OnceAMonthSex = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["OnceAMonthSex"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["OnceAMonthSex"])
                        : 0;
                    objClientIntake.CurrentCough = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["CurrentCough"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["CurrentCough"])
                        : 0;
                    objClientIntake.WeightLoss = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["WeightLoss"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["WeightLoss"])
                        : 0;
                    objClientIntake.Fever = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["Fever"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["Fever"])
                        : 0;
                    objClientIntake.NightSweats = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["NightSweats"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["NightSweats"])
                        : 0;
                    objClientIntake.FemaleVaginalDischarge = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["FemaleVaginalDischarge"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["FemaleVaginalDischarge"])
                        : 0;
                    objClientIntake.FemaleAbdominalPain = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["FemaleAbdominalPain"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["FemaleAbdominalPain"])
                        : 0;
                    objClientIntake.MaleUrethalDischarge = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["MaleUrethalDischarge"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["MaleUrethalDischarge"])
                        : 0;
                    objClientIntake.MaleScrotalSweling = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["MaleScrotalSweling"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["MaleScrotalSweling"])
                        : 0;
                    objClientIntake.GenitalSore = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["GenitalSore"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["GenitalSore"])
                        : 0;
                    objClientIntake.InitialHivTest = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["InitialHivTest"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["InitialHivTest"])
                        : 0;
                    objClientIntake.ConfirmatoryHivTest = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["ConfirmatoryHivTest"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["ConfirmatoryHivTest"])
                        : 0;
                    objClientIntake.SyphilisTest = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["SyphilisTest"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["SyphilisTest"])
                        : 0;
                    objClientIntake.HepatitisB = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["HepatitisB"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["HepatitisB"])
                        : 0;
                    objClientIntake.HepatitisC = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["HepatitisC"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["HepatitisC"])
                        : 0;
                    objClientIntake.HivTestResult = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["HivTestResult"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["HivTestResult"])
                        : 0;
                    objClientIntake.NotPrevTested = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["NotPrevTested"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["NotPrevTested"])
                        : 0;
                    objClientIntake.PrevTestNeg = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["PrevTestNeg"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["PrevTestNeg"])
                        : 0;
                    objClientIntake.PrevTestPosInHivCare = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["PrevTestPosInHivCare"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["PrevTestPosInHivCare"])
                        : 0;
                    objClientIntake.PrevTestPosNotInHivCare = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["PrevTestPosNotInHivCare"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["PrevTestPosNotInHivCare"])
                        : 0;
                    objClientIntake.HIVReqResFormSigned = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["HivReqResFormSigned"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["HivReqResFormSigned"])
                        : 0;
                    objClientIntake.HIVReqResFormWithCI = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["HivReqResFormWithCi"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["HivReqResFormWithCi"])
                        : 0;
                    objClientIntake.ClientRecHivTestResult = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["ClientRecHivTestResult"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["ClientRecHivTestResult"])
                        : 0;
                    objClientIntake.PostTestConslDone = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["PostTestConslDone"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["PostTestConslDone"])
                        : 0;
                    objClientIntake.RiskReducPlan = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["RiskReducPlan"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["RiskReducPlan"])
                        : 0;
                    objClientIntake.PostTestDiscloPlan = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["PostTestDiscloPlan"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["PostTestDiscloPlan"])
                        : 0;
                    objClientIntake.BringPartForHIVTest = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["BringPartForHivTest"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["BringPartForHivTest"])
                        : 0;
                    objClientIntake.BrngChildForHivTest = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["BrngChildForHivTest"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["BrngChildForHivTest"])
                        : 0;
                    objClientIntake.FPAndDualConcInfo = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["FPAndDualConcInfo"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["FPAndDualConcInfo"])
                        : 0;
                    objClientIntake.FPAndDualConcInfo = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["FPAndDualConcInfo"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["FPAndDualConcInfo"])
                        : 0;
                    objClientIntake.PrntUseCondomAsFPM = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["PrntUseCondomAsFPM"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["PrntUseCondomAsFPM"])
                        : 0;
                    objClientIntake.CondomUseDemo = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["CondomUseDemo"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["CondomUseDemo"])
                        : 0;
                    objClientIntake.CondomsProvided = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["CondomsProvided"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["CondomsProvided"])
                        : 0;
                    objClientIntake.ClientRefferedToOther = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["ClientRefferedToOther"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["ClientRefferedToOther"])
                        : 0;
                    objClientIntake.SyphilisTestResults = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["SyphilisTestResults"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["SyphilisTestResults"])
                        : 0;
                    objClientIntake.RequestedBy = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["RequestedBy"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["RequestedBy"])
                        : 0;
                    objClientIntake.TestedBy = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["TestedBy"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["TestedBy"])
                        : 0;
                    objClientIntake.CheckedBy = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["CheckedBy"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["CheckedBy"])
                        : 0;
                    objClientIntake.RequestedDate = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["RequestedDate"].ToString()) == false
                        ? Convert.ToDateTime(dtHTSCIF.Rows[0]["RequestedDate"].ToString())
                        :  (DateTime?)null;
                    objClientIntake.TestedDate = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["TestedDate"].ToString()) == false
                        ? Convert.ToDateTime(dtHTSCIF.Rows[0]["TestedDate"].ToString())
                        : (DateTime?)null; 
                    objClientIntake.CheckedDate = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["CheckedDate"].ToString()) == false
                        ? Convert.ToDateTime(dtHTSCIF.Rows[0]["CheckedDate"].ToString())
                        : (DateTime?)null;
                    objClientIntake.FirstTimeVisit = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["FirstTimeVisit"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["FirstTimeVisit"])
                        : 0;
                    objClientIntake.PartnerUseFPMethd = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["PartnerUseFPMethd"].ToString()) == false
                        ? Convert.ToInt32(dtHTSCIF.Rows[0]["PartnerUseFPMethd"])
                        : 0;
                    objClientIntake.Comments = string.IsNullOrEmpty(dtHTSCIF.Rows[0]["Comments"].ToString()) == false
                        ? dtHTSCIF.Rows[0]["Comments"].ToString()
                        : "";
                }
            }

            catch (Exception ex)
            {
                CLogger.WriteLog(ELogLevel.ERROR,
                    "BLTreatmentPreparation.GetTreatmentPreparation() Method:" + ex.ToString());
            }
            return objClientIntake;
        }

        public bool SaveClientIntakeFormData(string nodeJson, int patientID, int UserID)
        {
            //return true;
            CLogger.WriteLog(ELogLevel.INFO, "HTSClientIntakeForm SaveClientIntakeFormData() method called");
            bool flag = true;
            HTSClientIntake obj = SerializerUtil.ConverToObject<HTSClientIntake>(nodeJson);
            //DataTable dt = new DataTable();
            ClsObject clsObjTP = new ClsObject();
            try
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientID ", SqlDbType.Int, patientID.ToString());
                ClsUtility.AddParameters("@CIFID ", SqlDbType.Int, obj.CIFID.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                ClsUtility.AddParameters("@PatientCode", SqlDbType.VarChar, obj.PatientCode.ToString());
                ClsUtility.AddParameters("@FirstTimeVisit", SqlDbType.Int, obj.FirstTimeVisit.ToString());
                ClsUtility.AddParameters("@StateOfResidence ", SqlDbType.Int, obj.PatientState.ToString());
                ClsUtility.AddParameters("@LGAOfResidence ", SqlDbType.Int, obj.PatientLga.ToString());
                ClsUtility.AddParameters("@TypeOfSession ", SqlDbType.Int, obj.TypeOfSession.ToString());
                ClsUtility.AddParameters("@ClientIndexID ", SqlDbType.VarChar, obj.IndexClientID.ToString());
                ClsUtility.AddParameters("@IsClientIsIndex", SqlDbType.Int, obj.IsClientIsIndex.ToString());
                ClsUtility.AddParameters("@IndexTestingType", SqlDbType.Int, obj.IndexTestingType.ToString());
                ClsUtility.AddParameters("@PrevTestedHIVNeg", SqlDbType.Int, obj.PrevTestedHivNeg.ToString());
                ClsUtility.AddParameters("@ClientPreg", SqlDbType.Int, obj.ClientPregnant.ToString());

                ClsUtility.AddParameters("@InformHivTransRiskFactor", SqlDbType.Int, obj.InformHivTransRiskFactor.ToString());
                ClsUtility.AddParameters("@InformHivTransRoute", SqlDbType.Int, obj.InformHivTransRoute.ToString());
                ClsUtility.AddParameters("@InformHivTransMethod", SqlDbType.Int, obj.InformHivTransMethod.ToString());
                ClsUtility.AddParameters("@InformHivPossiResults", SqlDbType.Int, obj.InformHivPossiResults.ToString());
                ClsUtility.AddParameters("@InformConsentHIVTesting", SqlDbType.Int, obj.InformConsentHIVTesting.ToString());
                ClsUtility.AddParameters("@EvrHadSex", SqlDbType.Int, obj.EvrHadSex.ToString());
                ClsUtility.AddParameters("@BlodTransIn3Mnth", SqlDbType.Int, obj.BlodTransIn3Mnth.ToString());
                ClsUtility.AddParameters("@SexWithCasPartnr", SqlDbType.Int, obj.SexWithCasPartnr.ToString());
                ClsUtility.AddParameters("@SexWithRegPartnr", SqlDbType.Int, obj.SexWithRegPartnr.ToString());
                ClsUtility.AddParameters("@LastStiDone", SqlDbType.Int, obj.LastStiDone.ToString());
                ClsUtility.AddParameters("@OnceAMonthSex", SqlDbType.Int, obj.OnceAMonthSex.ToString());
                ClsUtility.AddParameters("@CurrentCough", SqlDbType.Int, obj.CurrentCough.ToString());
                ClsUtility.AddParameters("@WeightLoss", SqlDbType.Int, obj.WeightLoss.ToString());
                ClsUtility.AddParameters("@Fever", SqlDbType.Int, obj.Fever.ToString());
                ClsUtility.AddParameters("@NightSweats", SqlDbType.Int, obj.NightSweats.ToString());
                ClsUtility.AddParameters("@FemaleVaginalDischarge", SqlDbType.Int, obj.FemaleVaginalDischarge.ToString());
                ClsUtility.AddParameters("@FemaleAbdominalPain", SqlDbType.Int, obj.FemaleAbdominalPain.ToString());
                ClsUtility.AddParameters("@MaleUrethalDischarge", SqlDbType.Int, obj.MaleUrethalDischarge.ToString());
                ClsUtility.AddParameters("@MaleScrotalSweling", SqlDbType.Int, obj.MaleScrotalSweling.ToString());
                ClsUtility.AddParameters("@GenitalSore", SqlDbType.Int, obj.GenitalSore.ToString());
                ClsUtility.AddParameters("@InitialHivTest", SqlDbType.Int, obj.InitialHivTest.ToString());
                ClsUtility.AddParameters("@ConfirmatoryHivTest", SqlDbType.Int, obj.ConfirmatoryHivTest.ToString());
                ClsUtility.AddParameters("@SyphilisTest", SqlDbType.Int, obj.SyphilisTest.ToString());
                ClsUtility.AddParameters("@HepatitisB", SqlDbType.Int, obj.HepatitisB.ToString());
                ClsUtility.AddParameters("@HepatitisC", SqlDbType.Int, obj.HepatitisC.ToString());

                ClsUtility.AddParameters("@HivTestResult", SqlDbType.Int, obj.HivTestResult.ToString());
                ClsUtility.AddParameters("@NotPrevTested", SqlDbType.Int, obj.NotPrevTested.ToString());
                ClsUtility.AddParameters("@PrevTestNeg", SqlDbType.Int, obj.PrevTestNeg.ToString());
                ClsUtility.AddParameters("@PrevTestPosInHivCare", SqlDbType.Int, obj.PrevTestPosInHivCare.ToString());
                ClsUtility.AddParameters("@PrevTestPosNotInHivCare", SqlDbType.Int, obj.PrevTestPosNotInHivCare.ToString());
                ClsUtility.AddParameters("@HivReqResFormSigned", SqlDbType.Int, obj.HIVReqResFormSigned.ToString());
                ClsUtility.AddParameters("@HivReqResFormWithCi", SqlDbType.Int, obj.HIVReqResFormWithCI.ToString());
                ClsUtility.AddParameters("@ClientRecHivTestResult", SqlDbType.Int, obj.ClientRecHivTestResult.ToString());
                ClsUtility.AddParameters("@PostTestConslDone", SqlDbType.Int, obj.PostTestConslDone.ToString());
                ClsUtility.AddParameters("@RiskReducPlan", SqlDbType.Int, obj.RiskReducPlan.ToString());
                ClsUtility.AddParameters("@PostTestDiscloPlan", SqlDbType.Int, obj.PostTestDiscloPlan.ToString());
                ClsUtility.AddParameters("@BringPartForHivTest", SqlDbType.Int, obj.BringPartForHIVTest.ToString());
                ClsUtility.AddParameters("@BrngChildForHivTest", SqlDbType.Int, obj.BrngChildForHivTest.ToString());
                ClsUtility.AddParameters("@FPAndDualConcInfo", SqlDbType.Int, obj.FPAndDualConcInfo.ToString());
                ClsUtility.AddParameters("@PartnerUseFPMethd", SqlDbType.Int, obj.PartnerUseFPMethd.ToString());
                ClsUtility.AddParameters("@PrntUseCondomAsFPM", SqlDbType.Int, obj.PrntUseCondomAsFPM.ToString());
                ClsUtility.AddParameters("@CondomUseDemo", SqlDbType.Int, obj.CondomUseDemo.ToString());
                ClsUtility.AddParameters("@CondomsProvided", SqlDbType.Int, obj.CondomsProvided.ToString());
                ClsUtility.AddParameters("@ClientRefferedToOther", SqlDbType.Int, obj.ClientRefferedToOther.ToString());
                ClsUtility.AddParameters("@SyphilisTestResults", SqlDbType.Int, obj.SyphilisTestResults.ToString());
                ClsUtility.AddParameters("@RequestedBy", SqlDbType.Int, obj.RequestedBy.ToString());
                ClsUtility.AddParameters("@TestedBy", SqlDbType.Int, obj.TestedBy.ToString());
                ClsUtility.AddParameters("@CheckedBy", SqlDbType.Int, obj.CheckedBy.ToString());
                ClsUtility.AddParameters("@RequestedDate", SqlDbType.DateTime, obj.RequestedDate.ToString());
                ClsUtility.AddParameters("@TestedDate", SqlDbType.DateTime, obj.TestedDate.ToString());
                ClsUtility.AddParameters("@CheckedDate", SqlDbType.DateTime, obj.CheckedDate.ToString());
                ClsUtility.AddParameters("@ReferredFrom ", SqlDbType.Int, obj.ReferFrom.ToString());
                ClsUtility.AddParameters("@Setting ", SqlDbType.Int, obj.Setting.ToString());
                ClsUtility.AddParameters("@NoOfChildLessThan5 ", SqlDbType.Int, obj.NoOfChildLessThan5.ToString());
                ClsUtility.AddParameters("@NoOfWives", SqlDbType.Int, obj.NoOfWives.ToString());    
                ClsUtility.AddParameters("@DateOfVisit", SqlDbType.DateTime, obj.DateOfVisit.ToString());
                ClsUtility.AddParameters("@Comments", SqlDbType.VarChar, obj.Comments);
                ClsUtility.AddParameters("@PatientMaritalStat", SqlDbType.Int, obj.PatientMaritalStat.ToString());

                DataTable dt = (DataTable)clsObjTP.ReturnObject(ClsUtility.theParams, "pr_UpdateHTSClientIntakeForPatient", ClsDBUtility.ObjectEnum.DataTable);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
            return flag;
        }
    }
}
