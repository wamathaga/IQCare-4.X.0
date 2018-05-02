using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Interface.Clinical;
using DataAccess.Base;
using DataAccess.Entity;
using DataAccess.Common;
using Application.Common;

namespace BusinessProcess.Clinical
{
    class BKNHHEI : ProcessBase, IKNHHEI
    {
        public DataSet GetKNHPMTCTHEI(int patientID, int VisitID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientID", SqlDbType.Int, patientID.ToString());
                ClsUtility.AddParameters("@VisitId", SqlDbType.Int, VisitID.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "pr_KNH_GetPMTCTHEIPatientData", ClsDBUtility.ObjectEnum.DataSet);
            }

        }
        public int Save_Update_KNHHEI(int patientID, int VisitID, int LocationID, Hashtable ht, DataSet theDSchklist, int userID, int DataQualityFlag)
        {
            int retval = 0;
            DataSet theDS;
            ClsObject KNHPMTCTHEI = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                KNHPMTCTHEI.Connection = this.Connection;
                KNHPMTCTHEI.Transaction = this.Transaction;

                

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                ClsUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, VisitID.ToString());
                ClsUtility.AddParameters("@KNHHEIVisitDate", SqlDbType.VarChar, ht["KNHHEIVisitDate"].ToString());
                ClsUtility.AddParameters("@KNHHEIVisitType", SqlDbType.Int, ht["KNHHEIVisitType"].ToString());

                //Vital Sign
                ClsUtility.AddParameters("@KNHHEITemp", SqlDbType.Decimal, ht["KNHHEITemp"].ToString());
                ClsUtility.AddParameters("@KNHHEIRR", SqlDbType.Decimal, ht["KNHHEIRR"].ToString());
                ClsUtility.AddParameters("@KNHHEIHR", SqlDbType.Decimal, ht["KNHHEIHR"].ToString());
                ClsUtility.AddParameters("@KNHHEIHeight", SqlDbType.Decimal, ht["KNHHEIHeight"].ToString());
                ClsUtility.AddParameters("@KNHHEIWeight", SqlDbType.Decimal, ht["KNHHEIWeight"].ToString());
                ClsUtility.AddParameters("@KNHHEIBPSystolic", SqlDbType.Decimal, ht["KNHHEIBPSystolic"].ToString());
                ClsUtility.AddParameters("@KNHHEIBPDiastolic", SqlDbType.Decimal, ht["KNHHEIBPDiastolic"].ToString());
                ClsUtility.AddParameters("@KNHHEIHeadCircum", SqlDbType.Decimal, ht["KNHHEIHeadCircum"].ToString());
                ClsUtility.AddParameters("@KNHHEIWA", SqlDbType.Int, ht["KNHHEIWA"].ToString());
                ClsUtility.AddParameters("@KNHHEIWH", SqlDbType.Int, ht["KNHHEIWH"].ToString());
                ClsUtility.AddParameters("@KNHHEIBMIz", SqlDbType.Int, ht["KNHHEIBMIz"].ToString());
                ClsUtility.AddParameters("@KNHHEINurseComments", SqlDbType.VarChar, ht["KNHHEINurseComments"].ToString());
                ClsUtility.AddParameters("@KNHHEIReferToSpecialClinic", SqlDbType.VarChar, ht["KNHHEIReferToSpecialClinic"].ToString());
                ClsUtility.AddParameters("@KNHHEIReferToOther", SqlDbType.VarChar, ht["KNHHEIReferToOther"].ToString());

                //Neonatal History
                ClsUtility.AddParameters("@KNHHEISrRefral", SqlDbType.VarChar, ht["KNHHEISrRefral"].ToString());
                ClsUtility.AddParameters("@KNHHEIPlDelivery", SqlDbType.Int, ht["KNHHEIPlDelivery"].ToString());
                ClsUtility.AddParameters("@KNHHEIPlDeliveryotherfacility", SqlDbType.VarChar, ht["KNHHEIPlDeliveryotherfacility"].ToString());
                ClsUtility.AddParameters("@KNHHEIPlDeliveryother", SqlDbType.VarChar, ht["KNHHEIPlDeliveryother"].ToString());
                ClsUtility.AddParameters("@KNHHEIMdDelivery", SqlDbType.Int, ht["KNHHEIMdDelivery"].ToString());
                ClsUtility.AddParameters("@KNHHEIBWeight", SqlDbType.Decimal, ht["KNHHEIBWeight"].ToString());
                ClsUtility.AddParameters("@KNHHEIARVProp", SqlDbType.Int, ht["KNHHEIARVProp"].ToString());
                ClsUtility.AddParameters("@KNHHEIARVPropOther", SqlDbType.VarChar, ht["KNHHEIARVPropOther"].ToString());
                ClsUtility.AddParameters("@KNHHEIIFeedoption", SqlDbType.Int, ht["KNHHEIIFeedoption"].ToString());
                ClsUtility.AddParameters("@KNHHEIIFeedoptionother", SqlDbType.VarChar, ht["KNHHEIIFeedoptionother"].ToString());

                //Maternal History
                ClsUtility.AddParameters("@KNHHEIStateofMother", SqlDbType.Int, ht["KNHHEIStateofMother"].ToString());
                ClsUtility.AddParameters("@KNHHEIMRegisthisclinic", SqlDbType.Int, ht["KNHHEIMRegisthisclinic"].ToString());
                ClsUtility.AddParameters("@KNHHEIPlMFollowup", SqlDbType.Int, ht["KNHHEIPlMFollowup"].ToString());
                ClsUtility.AddParameters("@KNHHEIPlMFollowupother", SqlDbType.VarChar, ht["KNHHEIPlMFollowupother"].ToString());
                ClsUtility.AddParameters("@KNHHEIMRecievedDrug", SqlDbType.Int, ht["KNHHEIMRecievedDrug"].ToString());
                ClsUtility.AddParameters("@KNHHEIOnARTEnrol", SqlDbType.Int, ht["KNHHEIOnARTEnrol"].ToString());

                //Immunization -- Saving to grid now.......
                //ClsUtility.AddParameters("@KNHHEIDateImmunised", SqlDbType.VarChar, ht["KNHHEIDateImmunised"].ToString());
                //ClsUtility.AddParameters("@KNHHEIPeriodImmunised", SqlDbType.Int, ht["KNHHEIPeriodImmunised"].ToString());
                //ClsUtility.AddParameters("@KNHHEIGivenImmunised", SqlDbType.Int, ht["KNHHEIGivenImmunised"].ToString());

                //Presenting Complaints 
                ClsUtility.AddParameters("@KNHHEIAdditionalComplaint", SqlDbType.VarChar, ht["KNHHEIAdditionalComplaint"].ToString());

                //Examination, Milestone and Diagnosis
                ClsUtility.AddParameters("@KNHHEIExamination", SqlDbType.VarChar, ht["KNHHEIExamination"].ToString());

                //ClsUtility.AddParameters("@KNHHEIMilestones", SqlDbType.Int, ht["KNHHEIMilestones"].ToString());
                //ClsUtility.AddParameters("@KNHHEIAssessmmentOutcome", SqlDbType.Int, ht["KNHHEIAssessmmentOutcome"].ToString());

                // Management Plan
                ClsUtility.AddParameters("@KNHHEIVitamgiven", SqlDbType.Int, ht["KNHHEIVitamgiven"].ToString());
                ClsUtility.AddParameters("@KNHHEIWorkPlan", SqlDbType.VarChar, ht["KNHHEIWorkPlan"].ToString());
                //ClsUtility.AddParameters("@KNHHEIPlan", SqlDbType.Int, ht["KNHHEIPlan"].ToString());
                //ClsUtility.AddParameters("@KNHHEIPlanRegimen", SqlDbType.Int, ht["KNHHEIPlanRegimen"].ToString());

                //Referral, Admission and Appointment
                ClsUtility.AddParameters("@KNHHEIReferredto", SqlDbType.Int, ht["KNHHEIReferredto"].ToString());
                ClsUtility.AddParameters("@KNHHEIReferredtoother", SqlDbType.VarChar, ht["KNHHEIReferredtoother"].ToString());
                ClsUtility.AddParameters("@KNHHEIAdmittoward", SqlDbType.Int, ht["KNHHEIAdmittoward"].ToString());
                ClsUtility.AddParameters("@KNHHEITCA", SqlDbType.Int, ht["KNHHEITCA"].ToString());

                //Other Extar Field added By Rahmat as ON 16-Apr-2018
                ClsUtility.AddParameters("@Scheduled", SqlDbType.SmallInt, ht["Scheduled"].ToString());
                ClsUtility.AddParameters("@DurationARTstart", SqlDbType.Int, ht["DurationARTstart"].ToString());
                ClsUtility.AddParameters("@ReferredFrom", SqlDbType.Int, ht["ReferredFrom"].ToString());
                ClsUtility.AddParameters("@ReferredFromOther", SqlDbType.NVarChar, ht["ReferredFromOther"].ToString());
                ClsUtility.AddParameters("@SPO2", SqlDbType.Int, ht["SPO2"].ToString());
                ClsUtility.AddParameters("@AnyComplaints", SqlDbType.Bit, ht["AnyComplaints"].ToString());
                ClsUtility.AddParameters("@GeneralExamination", SqlDbType.Int, ht["GeneralExamination"].ToString());
                ClsUtility.AddParameters("@NeonatalHistoryNotes", SqlDbType.NVarChar, ht["NeonatalHistoryNotes"].ToString());
                ClsUtility.AddParameters("@TBFindings", SqlDbType.Bit, ht["TBFindings"].ToString());
                ClsUtility.AddParameters("@MUAC", SqlDbType.Int, ht["MUAC"].ToString());
                ClsUtility.AddParameters("@ReviewSystemComments", SqlDbType.NVarChar, ht["ReviewSystemComments"].ToString());

                theDS = (DataSet)KNHPMTCTHEI.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdateKNHHEI_Futures", ClsDBUtility.ObjectEnum.DataSet);
                ////////////////////////////////
                VisitID = Convert.ToInt32(theDS.Tables[0].Rows[0]["VisitId"]);
                retval = VisitID;

                //Diagnosis
                if (theDSchklist.Tables["dtD"] != null && theDSchklist.Tables["dtD"].Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDSchklist.Tables["dtD"].Rows)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                        ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, VisitID.ToString());
                        ClsUtility.AddParameters("@Id", SqlDbType.Int, theDR["DiagnosisID"].ToString());
                        ClsUtility.AddParameters("@CodeName", SqlDbType.VarChar, "DiagnosisPeads");
                        ClsUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, theDR["Diagnosis_Other"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, userID.ToString());
                        int temp = (int)KNHPMTCTHEI.ReturnObject(ClsUtility.theParams, "pr_KNHPMTCTHEI_SavecheckedlistItems", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                //Presenting Complaints
                if (theDSchklist.Tables["dtPC"] != null && theDSchklist.Tables["dtPC"].Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDSchklist.Tables["dtPC"].Rows)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                        ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, VisitID.ToString());
                        ClsUtility.AddParameters("@Id", SqlDbType.Int, theDR["PComplaintId"].ToString());
                        ClsUtility.AddParameters("@CodeName", SqlDbType.VarChar, "PresentingComplaints");
                        ClsUtility.AddParameters("@Numeric", SqlDbType.Int, theDR["Complaint_Other"].ToString());
                        ClsUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, theDR["Complaint_Other"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, userID.ToString());
                        int temp = (int)KNHPMTCTHEI.ReturnObject(ClsUtility.theParams, "pr_KNHPMTCTHEI_SavecheckedlistItems", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                //Vital Sign Referred To
                if (theDSchklist.Tables["dtVS_Rt"] != null && theDSchklist.Tables["dtVS_Rt"].Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDSchklist.Tables["dtVS_Rt"].Rows)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                        ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, VisitID.ToString());
                        ClsUtility.AddParameters("@Id", SqlDbType.Int, theDR["ID"].ToString());
                        ClsUtility.AddParameters("@CodeName", SqlDbType.VarChar, "VitalSign");
                        ClsUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, "");
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, userID.ToString());
                        int temp = (int)KNHPMTCTHEI.ReturnObject(ClsUtility.theParams, "pr_KNHPMTCTHEI_SavecheckedlistItems", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                //TB Assessment
                if (theDSchklist.Tables["dtTBA"] != null && theDSchklist.Tables["dtTBA"].Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDSchklist.Tables["dtTBA"].Rows)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                        ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, VisitID.ToString());
                        ClsUtility.AddParameters("@Id", SqlDbType.Int, theDR["ID"].ToString());
                        ClsUtility.AddParameters("@CodeName", SqlDbType.VarChar, "TBAssessment");
                        ClsUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, "");
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, userID.ToString());
                        int temp = (int)KNHPMTCTHEI.ReturnObject(ClsUtility.theParams, "pr_KNHPMTCTHEI_SavecheckedlistItems", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                //Neo Natal History
                if (theDSchklist.Tables["dtNeonatal"] != null && theDSchklist.Tables["dtNeonatal"].Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDSchklist.Tables["dtNeonatal"].Rows)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                        ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, VisitID.ToString());
                        ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                        ClsUtility.AddParameters("@Section", SqlDbType.VarChar, theDR["Section"].ToString());

                        ClsUtility.AddParameters("@TypeofTestId", SqlDbType.Int, theDR["TypeofTestId"].ToString());
                        ClsUtility.AddParameters("@TypeofTest", SqlDbType.VarChar, theDR["TypeofTest"].ToString());

                        ClsUtility.AddParameters("@ResultId", SqlDbType.Int, theDR["ResultId"].ToString());
                        ClsUtility.AddParameters("@Result", SqlDbType.VarChar, theDR["Result"].ToString());

                        ClsUtility.AddParameters("@Date", SqlDbType.VarChar, theDR["Date"].ToString());
                        ClsUtility.AddParameters("@Comments", SqlDbType.VarChar, theDR["Comments"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, userID.ToString());
                        int temp = (int)KNHPMTCTHEI.ReturnObject(ClsUtility.theParams, "pr_KNHPMTCTHEI_SaveGridViewData", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                //Maternal History
                if (theDSchklist.Tables["dtMaternal"] != null && theDSchklist.Tables["dtMaternal"].Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDSchklist.Tables["dtMaternal"].Rows)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                        ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, VisitID.ToString());
                        ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                        ClsUtility.AddParameters("@Section", SqlDbType.VarChar, theDR["Section"].ToString());

                        ClsUtility.AddParameters("@TypeofTestId", SqlDbType.Int, theDR["TypeofTestId"].ToString());
                        ClsUtility.AddParameters("@TypeofTest", SqlDbType.VarChar, theDR["TypeofTest"].ToString());

                        ClsUtility.AddParameters("@ResultId", SqlDbType.Int, theDR["ResultId"].ToString());
                        ClsUtility.AddParameters("@Result", SqlDbType.VarChar, theDR["Result"].ToString());

                        ClsUtility.AddParameters("@Date", SqlDbType.VarChar, theDR["Date"].ToString());
                        ClsUtility.AddParameters("@Comments", SqlDbType.VarChar, theDR["Comments"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, userID.ToString());
                        int temp = (int)KNHPMTCTHEI.ReturnObject(ClsUtility.theParams, "pr_KNHPMTCTHEI_SaveGridViewData", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                //Immunization
                if (theDSchklist.Tables["dtImmunization"] != null && theDSchklist.Tables["dtImmunization"].Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDSchklist.Tables["dtImmunization"].Rows)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                        ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, VisitID.ToString());
                        ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                        ClsUtility.AddParameters("@Section", SqlDbType.VarChar, theDR["Section"].ToString());

                        ClsUtility.AddParameters("@TypeofTestId", SqlDbType.Int, theDR["TypeofTestId"].ToString());
                        ClsUtility.AddParameters("@TypeofTest", SqlDbType.VarChar, theDR["TypeofTest"].ToString());

                        ClsUtility.AddParameters("@ResultId", SqlDbType.Int, theDR["ResultId"].ToString());
                        ClsUtility.AddParameters("@Result", SqlDbType.VarChar, theDR["Result"].ToString());

                        ClsUtility.AddParameters("@Date", SqlDbType.VarChar, theDR["Date"].ToString());
                        ClsUtility.AddParameters("@Comments", SqlDbType.VarChar, theDR["Comments"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, userID.ToString());
                        int temp = (int)KNHPMTCTHEI.ReturnObject(ClsUtility.theParams, "pr_KNHPMTCTHEI_SaveGridViewData", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                //Milestones
                if (theDSchklist.Tables["dtMilestone"] != null && theDSchklist.Tables["dtMilestone"].Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDSchklist.Tables["dtMilestone"].Rows)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                        ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, VisitID.ToString());
                        ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                        ClsUtility.AddParameters("@Section", SqlDbType.VarChar, theDR["Section"].ToString());

                        ClsUtility.AddParameters("@TypeofTestId", SqlDbType.Int, theDR["TypeofTestId"].ToString());
                        ClsUtility.AddParameters("@TypeofTest", SqlDbType.VarChar, theDR["TypeofTest"].ToString());

                        ClsUtility.AddParameters("@ResultId", SqlDbType.Int, theDR["ResultId"].ToString());
                        ClsUtility.AddParameters("@Result", SqlDbType.VarChar, theDR["Result"].ToString());

                        ClsUtility.AddParameters("@Date", SqlDbType.VarChar, theDR["Date"].ToString());
                        ClsUtility.AddParameters("@Comments", SqlDbType.VarChar, theDR["Comments"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, userID.ToString());
                        ClsUtility.AddParameters("@IsAchieved", SqlDbType.Int, theDR["Achieved"].ToString());
                        int temp = (int)KNHPMTCTHEI.ReturnObject(ClsUtility.theParams, "pr_KNHPMTCTHEI_SaveGridViewData", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                //TBAssessment
                if (theDSchklist.Tables["dtTBAssessment"] != null && theDSchklist.Tables["dtTBAssessment"].Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDSchklist.Tables["dtTBAssessment"].Rows)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                        ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, VisitID.ToString());
                        ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                        ClsUtility.AddParameters("@Section", SqlDbType.VarChar, theDR["Section"].ToString());

                        ClsUtility.AddParameters("@TypeofTestId", SqlDbType.Int, theDR["TypeofTestId"].ToString());
                        ClsUtility.AddParameters("@TypeofTest", SqlDbType.VarChar, theDR["TypeofTest"].ToString());

                        ClsUtility.AddParameters("@ResultId", SqlDbType.Int, theDR["ResultId"].ToString());
                        ClsUtility.AddParameters("@Result", SqlDbType.VarChar, theDR["Result"].ToString());

                        ClsUtility.AddParameters("@Date", SqlDbType.VarChar, theDR["Date"].ToString());
                        ClsUtility.AddParameters("@Comments", SqlDbType.VarChar, theDR["Comments"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, userID.ToString());
                        int temp = (int)KNHPMTCTHEI.ReturnObject(ClsUtility.theParams, "pr_KNHPMTCTHEI_SaveGridViewData", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                //SKIN
                if (theDSchklist.Tables["dtSkin"] != null && theDSchklist.Tables["dtSkin"].Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDSchklist.Tables["dtSkin"].Rows)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                        ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, VisitID.ToString());
                        ClsUtility.AddParameters("@Id", SqlDbType.Int, theDR["ID"].ToString());
                        ClsUtility.AddParameters("@CodeName", SqlDbType.VarChar, "SKIN");
                        ClsUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, "SKIN_OTHER");
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, userID.ToString());
                        int temp = (int)KNHPMTCTHEI.ReturnObject(ClsUtility.theParams, "pr_KNHPMTCTHEI_SavecheckedlistItems", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                //ENT
                if (theDSchklist.Tables["dtENT"] != null && theDSchklist.Tables["dtENT"].Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDSchklist.Tables["dtENT"].Rows)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                        ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, VisitID.ToString());
                        ClsUtility.AddParameters("@Id", SqlDbType.Int, theDR["ID"].ToString());
                        ClsUtility.AddParameters("@CodeName", SqlDbType.VarChar, "ENT");
                        ClsUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, "ENT_OTHER");
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, userID.ToString());
                        int temp = (int)KNHPMTCTHEI.ReturnObject(ClsUtility.theParams, "pr_KNHPMTCTHEI_SavecheckedlistItems", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                //CHEST
                if (theDSchklist.Tables["dtChest"] != null && theDSchklist.Tables["dtChest"].Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDSchklist.Tables["dtChest"].Rows)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                        ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, VisitID.ToString());
                        ClsUtility.AddParameters("@Id", SqlDbType.Int, theDR["ID"].ToString());
                        ClsUtility.AddParameters("@CodeName", SqlDbType.VarChar, "CHEST");
                        ClsUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, "CHEST_OTHER");
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, userID.ToString());
                        int temp = (int)KNHPMTCTHEI.ReturnObject(ClsUtility.theParams, "pr_KNHPMTCTHEI_SavecheckedlistItems", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                //CVS
                if (theDSchklist.Tables["dtCVS"] != null && theDSchklist.Tables["dtCVS"].Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDSchklist.Tables["dtCVS"].Rows)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                        ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, VisitID.ToString());
                        ClsUtility.AddParameters("@Id", SqlDbType.Int, theDR["ID"].ToString());
                        ClsUtility.AddParameters("@CodeName", SqlDbType.VarChar, "CVS");
                        ClsUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, "CVS_OTHER");
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, userID.ToString());
                        int temp = (int)KNHPMTCTHEI.ReturnObject(ClsUtility.theParams, "pr_KNHPMTCTHEI_SavecheckedlistItems", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                //Abdomen
                if (theDSchklist.Tables["dtAbdomen"] != null && theDSchklist.Tables["dtAbdomen"].Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDSchklist.Tables["dtAbdomen"].Rows)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                        ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, VisitID.ToString());
                        ClsUtility.AddParameters("@Id", SqlDbType.Int, theDR["ID"].ToString());
                        ClsUtility.AddParameters("@CodeName", SqlDbType.VarChar, "Abdomen");
                        ClsUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, "Abdomen_OTHER");
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, userID.ToString());
                        int temp = (int)KNHPMTCTHEI.ReturnObject(ClsUtility.theParams, "pr_KNHPMTCTHEI_SavecheckedlistItems", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                //CNS
                if (theDSchklist.Tables["dtCNS"] != null && theDSchklist.Tables["dtCNS"].Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDSchklist.Tables["dtCNS"].Rows)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                        ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, VisitID.ToString());
                        ClsUtility.AddParameters("@Id", SqlDbType.Int, theDR["ID"].ToString());
                        ClsUtility.AddParameters("@CodeName", SqlDbType.VarChar, "CNS");
                        ClsUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, "CNS_OTHER");
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, userID.ToString());
                        int temp = (int)KNHPMTCTHEI.ReturnObject(ClsUtility.theParams, "pr_KNHPMTCTHEI_SavecheckedlistItems", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
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
            return retval;
        }
    }
}
