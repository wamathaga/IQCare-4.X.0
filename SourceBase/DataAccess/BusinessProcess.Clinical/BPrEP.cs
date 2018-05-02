using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using DataAccess.Base;
using Interface.Clinical;
using DataAccess.Common;
using System.Data;
using DataAccess.Entity;
using Entities.Common;
using Application.Common;
using HIVCE.Common;

namespace BusinessProcess.Clinical
{
    [Serializable()]
    public class BPrEP : ProcessBase, IPrEP
    {
        ClsDBUtility oUtility = null;

        public BPrEP()
        {

        }

        public PrEP GetPrEPInitiationData(int ptn_pk, int Visit_Id, int LocationID)
        {
            oUtility = new ClsDBUtility();
            oUtility.Init_Hashtable();

            oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ConverTotValue.NullToInt(ptn_pk).ToString());
            oUtility.AddParameters("@Visit_Id", SqlDbType.Int, ConverTotValue.NullToInt(Visit_Id).ToString());
            oUtility.AddParameters("@LocationID", SqlDbType.Int, ConverTotValue.NullToInt(LocationID).ToString());

            ClsObject GetRecs = new ClsObject();
            DataSet dsPrEP = (DataSet)GetRecs.ReturnObject(oUtility.theParams, "pr_Prep_GetPrEPInitiationData", ClsDBUtility.ObjectEnum.DataSet);

            PrEP objPrEP = new PrEP();          
            //PrEPInitiation objPrEPIni = new PrEPInitiation();
            try
            {
                if (objPrEP != null)
                {
                    if (dsPrEP.Tables[0].Rows.Count > 0)
                    {
                        objPrEP.Purpose = new List<CodeDeCode>();
                        foreach (DataRow row in dsPrEP.Tables[0].Rows)
                        {
                            CodeDeCode ObjCodeDecode = new CodeDeCode();
                            ObjCodeDecode.CodeId = Convert.ToInt32(row["ID"]);
                            ObjCodeDecode.CodeName = Convert.ToString(row["Name"]);
                            objPrEP.Purpose.Add(ObjCodeDecode);
                        }
                    }
                    if (dsPrEP.Tables[1].Rows.Count > 0)
                    {
                        objPrEP.LabResult = new List<LabResult>();
                        foreach (DataRow row in dsPrEP.Tables[1].Rows)
                        {
                            LabResult ObjLabResult = new LabResult();
                            ObjLabResult.SubTestName = Convert.ToString(row["LabName"]);
                            ObjLabResult.ResultReportDate = Convert.ToString(row["ResultReportDate"]);
                            ObjLabResult.TestResults = Convert.ToDecimal(row["TestResults"]);
                            ObjLabResult.OrderTodayString = Convert.ToString(row["OrderToday"]);
                            ObjLabResult.LabTestID = Convert.ToInt32(row["LabTestID"]);
                            objPrEP.LabResult.Add(ObjLabResult);
                        }
                    }
                    if (dsPrEP.Tables[2].Rows.Count > 0)
                    {
                        objPrEP.StartPrEPNo = new List<CodeDeCode>();
                        foreach (DataRow row in dsPrEP.Tables[2].Rows)
                        {
                            CodeDeCode ObjCodeDecode = new CodeDeCode();
                            ObjCodeDecode.CodeId = Convert.ToInt32(row["ID"]);
                            ObjCodeDecode.CodeName = Convert.ToString(row["Name"]);
                            objPrEP.StartPrEPNo.Add(ObjCodeDecode);
                        }
                    }
                    if (dsPrEP.Tables[3].Rows.Count > 0)
                    {
                        objPrEP.PrEPAdherence = new List<CodeDeCode>();
                        foreach (DataRow row in dsPrEP.Tables[3].Rows)
                        {
                            CodeDeCode ObjCodeDecode = new CodeDeCode();
                            ObjCodeDecode.CodeId = Convert.ToInt32(row["ID"]);
                            ObjCodeDecode.CodeName = Convert.ToString(row["Name"]);
                            objPrEP.PrEPAdherence.Add(ObjCodeDecode);
                        }
                    }
                    if (dsPrEP.Tables[4].Rows.Count > 0)
                    {
                        objPrEP.WhyPoorFair = new List<CodeDeCode>();
                        foreach (DataRow row in dsPrEP.Tables[4].Rows)
                        {
                            CodeDeCode ObjCodeDecode = new CodeDeCode();
                            ObjCodeDecode.CodeId = Convert.ToInt32(row["ID"]);
                            ObjCodeDecode.CodeName = Convert.ToString(row["Name"]);
                            objPrEP.WhyPoorFair.Add(ObjCodeDecode);
                        }
                    }
                    if (dsPrEP.Tables[5].Rows.Count > 0)
                    {
                        objPrEP.PrEPStart = new List<CodeDeCode>();
                        foreach (DataRow row in dsPrEP.Tables[5].Rows)
                        {
                            CodeDeCode ObjCodeDecode = new CodeDeCode();
                            ObjCodeDecode.CodeId = Convert.ToInt32(row["ID"]);
                            ObjCodeDecode.CodeName = Convert.ToString(row["Name"]);
                            objPrEP.PrEPStart.Add(ObjCodeDecode);
                        }
                    }
                    if (dsPrEP.Tables[6].Rows.Count > 0)
                    {
                        objPrEP.RegimenCode = new List<CodeDeCode>();
                        foreach (DataRow row in dsPrEP.Tables[6].Rows)
                        {
                            CodeDeCode ObjCodeDecode = new CodeDeCode();
                            ObjCodeDecode.CodeId = Convert.ToInt32(row["ID"]);
                            ObjCodeDecode.CodeName = Convert.ToString(row["Name"]);
                            objPrEP.RegimenCode.Add(ObjCodeDecode);
                        }
                    }
                    if (dsPrEP.Tables[7].Rows.Count > 0)
                    {
                        objPrEP.RegimenReason = new List<CodeDeCode>();
                        foreach (DataRow row in dsPrEP.Tables[7].Rows)
                        {
                            CodeDeCode ObjCodeDecode = new CodeDeCode();
                            ObjCodeDecode.CodeId = Convert.ToInt32(row["ID"]);
                            ObjCodeDecode.CodeName = Convert.ToString(row["Name"]);
                            objPrEP.RegimenReason.Add(ObjCodeDecode);
                        }
                    }
                    if (dsPrEP.Tables[8].Rows.Count > 0)
                    {
                        objPrEP.AppointmentReason = new List<CodeDeCode>();
                        foreach (DataRow row in dsPrEP.Tables[8].Rows)
                        {
                            CodeDeCode ObjCodeDecode = new CodeDeCode();
                            ObjCodeDecode.CodeId = Convert.ToInt32(row["ID"]);
                            ObjCodeDecode.CodeName = Convert.ToString(row["Name"]);
                            objPrEP.AppointmentReason.Add(ObjCodeDecode);
                        }
                    }
                    if (dsPrEP.Tables[9].Rows.Count > 0)
                    {
                        objPrEP.objPrEPInition = new PrEPInitiation();
                        objPrEP.objPrEPInition.LabID = string.IsNullOrEmpty(dsPrEP.Tables[9].Rows[0]["Lab_Id"].ToString()) == false
                                   ? Convert.ToInt32(dsPrEP.Tables[9].Rows[0]["Lab_Id"]) : 0;
                        objPrEP.objPrEPInition.OrderToday = string.IsNullOrEmpty(dsPrEP.Tables[9].Rows[0]["OrderToday"].ToString()) == false
                                    ? Convert.ToBoolean(dsPrEP.Tables[9].Rows[0]["OrderToday"]) : false;
                        objPrEP.objPrEPInition.HIVTestResult = string.IsNullOrEmpty(dsPrEP.Tables[9].Rows[0]["HIVTestResult"].ToString()) == false
                            ? Convert.ToInt32(dsPrEP.Tables[9].Rows[0]["HIVTestResult"]) : 0;
                        objPrEP.objPrEPInition.IsSampleCollected = string.IsNullOrEmpty(dsPrEP.Tables[9].Rows[0]["IsSampleCollected"].ToString()) == false
                            ? Convert.ToBoolean(dsPrEP.Tables[9].Rows[0]["IsSampleCollected"]) : false;
                        objPrEP.objPrEPInition.ReferredToCCC = string.IsNullOrEmpty(dsPrEP.Tables[9].Rows[0]["ReferredToCCC"].ToString()) == false
                            ? Convert.ToBoolean(dsPrEP.Tables[9].Rows[0]["ReferredToCCC"]) : false;
                        objPrEP.objPrEPInition.PreviousPrep = string.IsNullOrEmpty(dsPrEP.Tables[9].Rows[0]["PreviousPrep"].ToString()) == false
                            ? Convert.ToBoolean(dsPrEP.Tables[9].Rows[0]["PreviousPrep"]) : false;
                        objPrEP.objPrEPInition.WillingToStartPrep = string.IsNullOrEmpty(dsPrEP.Tables[9].Rows[0]["WillingToStartPrep"].ToString()) == false
                            ? Convert.ToBoolean(dsPrEP.Tables[9].Rows[0]["WillingToStartPrep"]) : false;
                        objPrEP.objPrEPInition.IfWillingToStartPrepNo = string.IsNullOrEmpty(dsPrEP.Tables[9].Rows[0]["IfWillingToStartPrepNo"].ToString()) == false
                            ? Convert.ToInt32(dsPrEP.Tables[9].Rows[0]["IfWillingToStartPrepNo"]) : 0;
                        objPrEP.objPrEPInition.IfWillingToStartPrepYes = string.IsNullOrEmpty(dsPrEP.Tables[9].Rows[0]["IfWillingToStartPrepYes"].ToString()) == false
                            ? Convert.ToInt32(dsPrEP.Tables[9].Rows[0]["IfWillingToStartPrepYes"]) : 0;
                        objPrEP.objPrEPInition.PrepAdherence = string.IsNullOrEmpty(dsPrEP.Tables[9].Rows[0]["PrepAdherence"].ToString()) == false
                            ? Convert.ToInt32(dsPrEP.Tables[9].Rows[0]["PrepAdherence"]) : 0;
                        objPrEP.objPrEPInition.PillCount = string.IsNullOrEmpty(dsPrEP.Tables[9].Rows[0]["PillCount"].ToString()) == false
                           ? Convert.ToInt32(dsPrEP.Tables[9].Rows[0]["PillCount"]) : 0;
                        objPrEP.objPrEPInition.WhyPoorFairSingle = string.IsNullOrEmpty(dsPrEP.Tables[9].Rows[0]["WhyPoorFair"].ToString()) == false
                           ? Convert.ToInt32(dsPrEP.Tables[9].Rows[0]["WhyPoorFair"]) : 0;
                        objPrEP.objPrEPInition.PrepStatus = string.IsNullOrEmpty(dsPrEP.Tables[9].Rows[0]["PrepStatus"].ToString()) == false
                           ? Convert.ToInt32(dsPrEP.Tables[9].Rows[0]["PrepStatus"]) : 0;
                        objPrEP.objPrEPInition.PrepRegimen = string.IsNullOrEmpty(dsPrEP.Tables[9].Rows[0]["PrepRegimen"].ToString()) == false
                           ? Convert.ToInt32(dsPrEP.Tables[9].Rows[0]["PrepRegimen"]) : 0;
                        objPrEP.objPrEPInition.PrepRegimenDiscountinueReason = string.IsNullOrEmpty(dsPrEP.Tables[9].Rows[0]["PrepRegimenDiscountinueReason"].ToString()) == false
                           ? Convert.ToInt32(dsPrEP.Tables[9].Rows[0]["PrepRegimenDiscountinueReason"]) : 0;
                        objPrEP.objPrEPInition.CondomIssued = string.IsNullOrEmpty(dsPrEP.Tables[9].Rows[0]["CondomIssued"].ToString()) == false
                           ? Convert.ToBoolean(dsPrEP.Tables[9].Rows[0]["CondomIssued"]) : false;
                        objPrEP.objPrEPInition.AdherenceCounsellingDone = string.IsNullOrEmpty(dsPrEP.Tables[9].Rows[0]["AdherenceCounsellingDone"].ToString()) == false
                           ? Convert.ToBoolean(dsPrEP.Tables[9].Rows[0]["AdherenceCounsellingDone"]) : false;
                        objPrEP.objPrEPInition.PrescribedPrepToday = string.IsNullOrEmpty(dsPrEP.Tables[9].Rows[0]["PrescribedPrepToday"].ToString()) == false
                           ? Convert.ToBoolean(dsPrEP.Tables[9].Rows[0]["PrescribedPrepToday"]) : false;
                        objPrEP.objPrEPInition.WorkPlan = string.IsNullOrEmpty(dsPrEP.Tables[9].Rows[0]["WorkPlan"].ToString()) == false
                           ? Convert.ToString(dsPrEP.Tables[9].Rows[0]["WorkPlan"]) : "";
                        objPrEP.objPrEPInition.Visit_Id = string.IsNullOrEmpty(dsPrEP.Tables[9].Rows[0]["Visit_Id"].ToString()) == false
                           ? Convert.ToInt32(dsPrEP.Tables[9].Rows[0]["Visit_Id"]) : 0;
                        objPrEP.objPrEPInition.Ptn_pk = string.IsNullOrEmpty(dsPrEP.Tables[9].Rows[0]["Ptn_pk"].ToString()) == false
                           ? Convert.ToInt32(dsPrEP.Tables[9].Rows[0]["Ptn_pk"]) : 0;
                        objPrEP.objPrEPInition.LocationId = string.IsNullOrEmpty(dsPrEP.Tables[9].Rows[0]["LocationId"].ToString()) == false
                           ? Convert.ToInt32(dsPrEP.Tables[9].Rows[0]["LocationId"]) : 0;
                    }
                    if (dsPrEP.Tables[10].Rows.Count > 0)
                    {
                        objPrEP.NxtAPT = new List<Appointment>();
                        int id = 1;
                        foreach (DataRow row in dsPrEP.Tables[10].Rows)
                        {
                           Appointment app = new Appointment();
                            app.Id = id;
                            app.Ptn_pk = Convert.ToInt32(row["Ptn_Pk"].ToString());

                            app.LocationId = Convert.ToInt32(row["LocationId"].ToString());
                            app.Visit_Id = Convert.ToInt32(row["Visit_pk"].ToString());

                            app.Status = row["STATUS"].ToString();

                            app.AppointmentDate = string.IsNullOrEmpty(row["Appdate"].ToString()) == false ? Convert.ToDateTime(row["Appdate"].ToString()) : (DateTime?)null;
                            app.Purpose = string.IsNullOrEmpty(row["PurPoseID"].ToString()) == false
                                ? Convert.ToInt32(row["PurPoseID"].ToString())
                                : (int?)null;
                            app.PurposeName = row["Purpose"].ToString();
                            app.AppointmentType = string.IsNullOrEmpty(row["AppointmentType"].ToString()) == false
                                ? Convert.ToInt32(row["AppointmentType"].ToString())
                                : (int?)null;
                            app.ServiceArea = string.IsNullOrEmpty(row["ServiceArea"].ToString()) == false
                                ? Convert.ToInt32(row["ServiceArea"].ToString())
                                : (int?)null;
                            app.ServiceAreaName = row["ServiceAreaName"].ToString();
                            app.Description = row["Description"].ToString();

                            objPrEP.NxtAPT.Add(app);
                            id++;
                        }
                    }

                }

            }

            catch (Exception ex)
            {
                CLogger.WriteLog(ELogLevel.ERROR,
                    "BPrEP.GetPrEPInitiationData() Method:" + ex.ToString());
            }
            return objPrEP;
        }
        public int SaveUpdatePrEPInitiationData(PrEPInitiation obj, int userID, int visitId, int LocationID)
        {
            CLogger.WriteLog(ELogLevel.INFO, "BPrEP.SaveUpdatePrEPInitiationData() method called");
            ClsObject clsObjTP = new ClsObject();
            PrEP objMain = new PrEP();
            DataTable ReturnDT = new DataTable();
            int InitiationID = 0;
            try
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, obj.Ptn_pk.ToString());
                ClsUtility.AddParameters("@Visit_Id", SqlDbType.Int, visitId.ToString());
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, obj.LocationId.ToString());
                ClsUtility.AddParameters("@Lab_Id", SqlDbType.Int, obj.LabID.ToString());
                //ClsUtility.AddParameters("@OrderToday", SqlDbType.Bit, obj.OrderToday.ToString());
                ClsUtility.AddParameters("@OrderToday", SqlDbType.Bit, "1");
                ClsUtility.AddParameters("@HIVTestResult", SqlDbType.Int, obj.HIVTestResult.ToString());
                ClsUtility.AddParameters("@IsSampleCollected", SqlDbType.Bit, obj.IsSampleCollected.ToString());
                ClsUtility.AddParameters("@ReferredToCCC", SqlDbType.Bit, obj.ReferredToCCC.ToString());
                ClsUtility.AddParameters("@PreviousPrep", SqlDbType.Bit, obj.PreviousPrep.ToString());
                ClsUtility.AddParameters("@WillingToStartPrep", SqlDbType.Bit, obj.WillingToStartPrep.ToString());
                ClsUtility.AddParameters("@IfWillingToStartPrepNo", SqlDbType.Int, obj.IfWillingToStartPrepNo.ToString());
                ClsUtility.AddParameters("@IfWillingToStartPrepYes", SqlDbType.Int, obj.IfWillingToStartPrepYes.ToString());
                ClsUtility.AddParameters("@PrepAdherence", SqlDbType.Int, obj.PrepAdherence.ToString());
                ClsUtility.AddParameters("@PillCount", SqlDbType.Int, obj.PillCount.ToString());
                ClsUtility.AddParameters("@WhyPoorFair", SqlDbType.Int, obj.WhyPoorFairSingle.ToString());
                ClsUtility.AddParameters("@PrepStatus", SqlDbType.Int, obj.PrepStatus.ToString());
                ClsUtility.AddParameters("@PrepRegimen", SqlDbType.Int, obj.PrepRegimen.ToString());
                ClsUtility.AddParameters("@PrepRegimenDiscountinueReason", SqlDbType.Int, obj.PrepRegimenDiscountinueReason.ToString());
                ClsUtility.AddParameters("@CondomIssued", SqlDbType.Bit, obj.CondomIssued.ToString());
                ClsUtility.AddParameters("@AdherenceCounsellingDone", SqlDbType.Bit, obj.CondomIssued.ToString());
                ClsUtility.AddParameters("@PrescribedPrepToday", SqlDbType.Bit, obj.PrescribedPrepToday.ToString());
                ClsUtility.AddParameters("@WorkPlan", SqlDbType.VarChar, obj.WorkPlan.ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, userID.ToString());
                ClsUtility.AddParameters("@HepatitisBVaccine", SqlDbType.Bit, obj.HepatitisBVaccine.ToString());

                ReturnDT = (DataTable)clsObjTP.ReturnObject(ClsUtility.theParams, "Pr_PrEP_SaveUpdatePrEPInitiationData", ClsDBUtility.ObjectEnum.DataTable);
                if (ReturnDT != null && ReturnDT.Rows.Count > 0)
                {
                    InitiationID = Convert.ToInt32(ReturnDT.Rows[0]["InitiationID"]);
                }
               
                //
                foreach (LabResult mst in obj.LabResultInitiation)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@InitiationID", SqlDbType.Int, InitiationID.ToString());
                    ClsUtility.AddParameters("@LabID", SqlDbType.Int, mst.LabTestID.ToString());
                    ClsUtility.AddParameters("@OrderToday", SqlDbType.Bit, mst.OrderToday.ToString());
                    ReturnDT = (DataTable)clsObjTP.ReturnObject(ClsUtility.theParams, "Pr_PrEP_LabInvestigations", ClsDBUtility.ObjectEnum.DataTable);
                }
                if (obj.NextAppointment != null)
                {
                    foreach (Entities.Common.Appointment mst in obj.NextAppointment)
                    {
                        if (mst.Id == 0)
                        {
                            ClsUtility.Init_Hashtable();
                            ClsUtility.AddParameters("@PatientId", SqlDbType.Int, obj.Ptn_pk.ToString());
                            ClsUtility.AddParameters("@LocationId", SqlDbType.Int, LocationID.ToString());
                            ClsUtility.AddParameters("@AppDate", SqlDbType.DateTime, mst.AppointmentDate.HasValue == true ? mst.AppointmentDate.Value.ToString("yyyy-MM-dd hh:mm:ss").ToString() : DBNull.Value.ToString());
                            ClsUtility.AddParameters("@AppReasonId", SqlDbType.Int, mst.Purpose.ToString());
                            ClsUtility.AddParameters("@UserId", SqlDbType.Int, userID.ToString());
                            ClsUtility.AddParameters("@AppType", SqlDbType.Int, mst.AppointmentType.ToString());
                            ClsUtility.AddParameters("@ServiceArea", SqlDbType.Int, mst.ServiceArea.ToString());
                            ClsUtility.AddParameters("@Description", SqlDbType.VarChar, mst.Description.ToString());
                            ReturnDT = (DataTable)clsObjTP.ReturnObject(ClsUtility.theParams, "Pr_HIVCE_SaveAppointment", ClsDBUtility.ObjectEnum.DataTable);
                        }
                    }
                }
                return InitiationID;
            }
            catch (Exception ex)
            {
                CLogger.WriteLog(ELogLevel.ERROR, "BPrEP.SaveUpdatePrEPInitiationData() Method:" + ex.ToString());
                throw ex;
            }
            finally
            {
                clsObjTP = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }


        public PrEP GetPrEPTriageData(int ptn_pk, int visitPK, int locationId)
        {
            CLogger.WriteLog(ELogLevel.INFO, "BusinessProcessClinical.BPrEP.GetPrEPTriageData() method called");

            oUtility = new ClsDBUtility();
            oUtility.Init_Hashtable();

            oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ConverTotValue.NullToInt(ptn_pk).ToString());
            oUtility.AddParameters("@Visit_Id", SqlDbType.Int, ConverTotValue.NullToInt(visitPK).ToString());
            oUtility.AddParameters("@LocationID", SqlDbType.Int, ConverTotValue.NullToInt(locationId).ToString());

            ClsObject GetRecs = new ClsObject();
            DataSet data = (DataSet)GetRecs.ReturnObject(oUtility.theParams, "Pr_PrEP_GetTriageData", ClsDBUtility.ObjectEnum.DataSet);

            PrEP objPrEP = new PrEP();
            objPrEP.VisitTypes = new List<CodeDeCode>();
            objPrEP.Districts = new List<District>();
            objPrEP.FacilityList = new List<Facility>();
            objPrEP.OrdVisit = new OrdVisit();
            objPrEP.PatientVitals = new PatientVitals();
            objPrEP.UserDesignation = new List<UserDesignation>();
            //string result = string.Empty;

            try
            {
                /* Visit Types*/
                if (data.Tables[1].Rows.Count > 0)
                {
                    objPrEP.VisitTypes = (from dt in data.Tables[1].AsEnumerable()
                                          select new CodeDeCode()
                                          {
                                              CodeId = dt.Field<int>("codeid"),
                                              CodeName = dt.Field<string>("VisitType"),
                                              DeCodeId = dt.Field<int>("Id"),
                                              DeCodeName = dt.Field<string>("NAME")
                                          }).ToList();
                }

                /* User Designation*/
                if (data.Tables[7].Rows.Count > 0)
                {
                    objPrEP.UserDesignation = (from dt in data.Tables[7].AsEnumerable().OrderBy(o => o.Field<string>("UserName"))
                                               select new UserDesignation()
                                               {
                                                   UserID = dt.Field<int>("userid"),
                                                   UserName = dt.Field<string>("UserName"),
                                                   Designation = dt.Field<string>("Designation"),
                                                   DeleteFlag = dt.Field<int>("DeleteFlag")
                                               }).ToList();
                }

                /* District*/
                if (data.Tables[2].Rows.Count > 0)
                {
                    objPrEP.Districts = (from dt in data.Tables[2].AsEnumerable().OrderBy(o => o.Field<int?>("SRno"))
                                         select new District()
                                         {
                                             ID = dt.Field<int>("ID"),
                                             Name = dt.Field<string>("Name"),
                                             SrNo = dt.Field<int?>("SRno")
                                         }).OrderBy(o => o.SrNo).ToList();
                }

                /* Facility */
                if (data.Tables[3].Rows.Count > 0)
                {
                    objPrEP.FacilityList = (from dt in data.Tables[3].AsEnumerable().OrderBy(o => o.Field<int?>("SRno"))
                                            select new Facility()
                                            {
                                                ID = dt.Field<int>("ID"),
                                                Name = dt.Field<string>("Name"),
                                                SrNo = dt.Field<int?>("SRno"),
                                                MFLCode = dt.Field<Int32>("MFLCode").ToString(),
                                                CountryId = dt.Field<int?>("CountryId"),
                                                CountryName = dt.Field<string>("CountryName")
                                            }).OrderBy(o => o.SrNo).ToList();
                }

                /* Ord Visit */
                if (data.Tables[4].Rows.Count > 0)
                {
                    objPrEP.OrdVisit = new OrdVisit();
                    foreach (DataRow row in data.Tables[4].Rows)
                    {
                        objPrEP.OrdVisit.Visit_Id = Convert.ToInt32(row["Visit_Id"]);
                        objPrEP.OrdVisit.Ptn_Pk = Convert.ToInt32(row["Ptn_Pk"]);
                        objPrEP.OrdVisit.LocationId = Convert.ToInt32(row["LocationID"]);
                        objPrEP.OrdVisit.VisitDate = string.IsNullOrEmpty(row["visitdate"].ToString()) == false ? Convert.ToDateTime(row["visitdate"].ToString()) : (DateTime?)null;
                        objPrEP.OrdVisit.VisitTypeId = Convert.ToInt32(row["visittype"]);
                        objPrEP.OrdVisit.CreatedDate = string.IsNullOrEmpty(row["CreateDate"].ToString()) == false ? Convert.ToDateTime(row["CreateDate"].ToString()) : (DateTime?)null;
                        objPrEP.OrdVisit.UpdatedDate = string.IsNullOrEmpty(row["updatedate"].ToString()) == false ? Convert.ToDateTime(row["updatedate"].ToString()) : (DateTime?)null;
                        objPrEP.OrdVisit.TypeOfVisit = Convert.ToInt32(row["TypeOfVisit"]);
                        objPrEP.OrdVisit.Signature = Convert.ToInt32(row["Signature"]);
                    }
                }

                /* Patient Vitals */
                if (data.Tables[5].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = data.Tables[5];

                    objPrEP.PatientVitals = new PatientVitals();

                    objPrEP.PatientVitals.Visit_Id = Convert.ToInt32(dt.Rows[0]["visit_pk"].ToString());
                    objPrEP.PatientVitals.BPDiastolic = dt.Rows[0]["BPDiastolic"].ToString();
                    objPrEP.PatientVitals.BPSystolic = dt.Rows[0]["BPSystolic"].ToString();
                    objPrEP.PatientVitals.TEMP = dt.Rows[0]["TEMP"].ToString();
                    objPrEP.PatientVitals.RR = dt.Rows[0]["RR"].ToString();
                    objPrEP.PatientVitals.HR = dt.Rows[0]["HR"].ToString();
                    objPrEP.PatientVitals.HeadCircumference = dt.Rows[0]["Headcircumference"].ToString();
                    objPrEP.PatientVitals.Height = dt.Rows[0]["height"].ToString();
                    objPrEP.PatientVitals.Weight = dt.Rows[0]["weight"].ToString();
                    objPrEP.PatientVitals.MUAC = dt.Rows[0]["MUAC"].ToString();
                    objPrEP.PatientVitals.Weightforage = dt.Rows[0]["weightforage"].ToString();
                    objPrEP.PatientVitals.Weightforheight = dt.Rows[0]["weightforheight"].ToString();
                    objPrEP.PatientVitals.BMIz = dt.Rows[0]["BMIz"].ToString();
                    objPrEP.PatientVitals.NurseComments = dt.Rows[0]["NurseComments"].ToString();


                }

                /* Transfer In */
                if (data.Tables[6].Rows.Count > 0)
                {
                    objPrEP.EntryPoint = new HIVCare();
                    DataTable dt = new DataTable();
                    dt = data.Tables[6];

                    objPrEP.EntryPoint.Visit_Id = Convert.ToInt32(dt.Rows[0]["Visit_Id"].ToString());
                    objPrEP.EntryPoint.Ptn_pk = Convert.ToInt32(dt.Rows[0]["Ptn_Pk"].ToString());
                    objPrEP.EntryPoint.LocationId = Convert.ToInt32(dt.Rows[0]["LocationID"].ToString());
                    objPrEP.EntryPoint.HIVCareEnrollmentDate = Convert.ToDateTime(dt.Rows[0]["hivcareenrollmentdate"].ToString());
                    objPrEP.EntryPoint.DateHIVDiagnosis = string.IsNullOrEmpty(dt.Rows[0]["datehivdiagnosis"].ToString()) == false ? Convert.ToDateTime(dt.Rows[0]["datehivdiagnosis"].ToString()) : (DateTime?)null;
                    objPrEP.EntryPoint.ARTTransferInDate = string.IsNullOrEmpty(dt.Rows[0]["ARTTransferInDate"].ToString()) == false ? Convert.ToDateTime(dt.Rows[0]["ARTTransferInDate"].ToString()) : (DateTime?)null;
                    objPrEP.EntryPoint.ARTTransferInFrom = string.IsNullOrEmpty(dt.Rows[0]["ARTTransferInFrom"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["ARTTransferInFrom"].ToString()) : 0;
                    objPrEP.EntryPoint.District = string.IsNullOrEmpty(dt.Rows[0]["FromDistrict"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["FromDistrict"].ToString()) : 0;
                    objPrEP.EntryPoint.UserId = string.IsNullOrEmpty(dt.Rows[0]["UserId"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["UserId"].ToString()) : 0;
                    objPrEP.EntryPoint.ARTStartDate = string.IsNullOrEmpty(dt.Rows[0]["ARTStartDate"].ToString()) == false ? Convert.ToDateTime(dt.Rows[0]["ARTStartDate"].ToString()) : (DateTime?)null;
                    objPrEP.EntryPoint.TransferIn = string.IsNullOrEmpty(dt.Rows[0]["TransferIn"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["TransferIn"].ToString()) : 0;
                    objPrEP.EntryPoint.ConfirmHIVPosDate = string.IsNullOrEmpty(dt.Rows[0]["ConfirmHIVPosDate"].ToString()) == false ? Convert.ToDateTime(dt.Rows[0]["ConfirmHIVPosDate"].ToString()) : (DateTime?)null;
                    objPrEP.ReferredFrom = string.IsNullOrEmpty(dt.Rows[0]["ReferredFrom"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["ReferredFrom"].ToString()) : (int?)null;
                    objPrEP.ReferredFromOther = dt.Rows[0]["ReferredFromSpecify"].ToString();

                }

                if (data.Tables[12].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = data.Tables[12];
                    objPrEP.PatientVitals.SPO2 = dt.Rows[0]["SPO2"].ToString();
                }

                if (data.Tables[8].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = data.Tables[8];
                    objPrEP.ExistingInitialVisitId = string.IsNullOrEmpty(dt.Rows[0]["visit_id"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["visit_id"].ToString()) : 0;
                    objPrEP.ExistingInitialVisitDate = string.IsNullOrEmpty(dt.Rows[0]["VisitDate"].ToString()) == false ? Convert.ToDateTime(dt.Rows[0]["VisitDate"].ToString()) : (DateTime?)null;
                }

                if (data.Tables[9].Rows.Count > 0)
                {
                    objPrEP.AleardySavedTabs = (from dt in data.Tables[9].AsEnumerable()
                                                select new CodeDeCode()
                                                {
                                                    CodeId = dt.Field<int>("FeatureID"),
                                                    CodeName = dt.Field<string>("FeatureName"),
                                                    DeCodeId = dt.Field<int>("TabId"),
                                                    DeCodeName = dt.Field<string>("TabName")
                                                }).ToList();
                }

                /*Regimen*/
                if (data.Tables[10].Rows.Count > 0)
                {
                    objPrEP.Regimen = (from dt in data.Tables[10].AsEnumerable()
                                       select new MultiTableList()
                                       {
                                           Id = dt.Field<int>("RegimenId"),
                                           Name = dt.Field<string>("Regimen")
                                       }).ToList();
                }

                objPrEP.PatientReferred = new List<MultiTableList>();
                if (data.Tables[11].Rows.Count > 0)
                {
                    objPrEP.PatientReferred = (from dt in data.Tables[11].AsEnumerable().ToList()
                                               select new MultiTableList()
                                               {
                                                   Id = dt.Field<int>("id"),
                                                   Name = dt.Field<string>("NAME")
                                               }).ToList();
                }

                /* Key Population Details */
                if (data.Tables[13].Rows.Count > 0)
                {
                    //foreach (DataRow row in data.Tables[13].Rows)
                    //{
                    //    mgt.KeyPopulationList.Add(Convert.ToInt32(row["ID"].ToString()));
                    //}
                    objPrEP.KeyPopulationList = (from dt in data.Tables[13].AsEnumerable()
                                                 select new DBMultiSelectSelcted()
                                                 {
                                                     id = dt.Field<int>("ID")
                                                 }).ToList();
                }


                ///* Appointment Details */
                //if (data.Tables[19].Rows.Count > 0)
                //{
                //    DataTable dtRow = new DataTable();
                //    dtRow = data.Tables[19];

                //    objPrEP.AppDate = string.IsNullOrEmpty(dtRow.Rows[0]["AppDate"].ToString()) == false ? Convert.ToDateTime(dtRow.Rows[0]["AppDate"].ToString()) : (DateTime?)null;
                //    objPrEP.AppReason = string.IsNullOrEmpty(dtRow.Rows[0]["AppReason"].ToString()) == false ? dtRow.Rows[0]["AppReason"].ToString() : string.Empty;
                //}

                ///* PatientAdherence */
                //if (data.Tables[20].Rows.Count > 0)
                //{
                //    DataTable dtRow = new DataTable();
                //    dtRow = data.Tables[20];
                //    objPrEP.MgtSignature = string.IsNullOrEmpty(dtRow.Rows[0]["Signature"].ToString()) == false ? Convert.ToInt32(dtRow.Rows[0]["Signature"].ToString()) : 0;
                //}

                ///* Purpose */
                //if (data.Tables[21].Rows.Count > 0)
                //{
                //    List<CodeDeCodeTables> lst = new List<CodeDeCodeTables>();

                //    lst = (from dt in data.Tables[21].AsEnumerable()
                //           select new CodeDeCodeTables()
                //           {
                //               DeCodeId = dt.Field<int>("ID"),
                //               DeCodeName = dt.Field<string>("NAME"),
                //               CodeName = "PUR"
                //           }).ToList();

                //    objPrEP.Purpose.AddRange(lst);
                //}
            }
            catch (Exception ex)
            {
                CLogger.WriteLog(ELogLevel.ERROR, "BusinessProcessClinical.BPrEP.GetPrEPTriageData() Method:" + ex.ToString());
            }
            return objPrEP;
        }
        public int SaveUpdatePrEPTriage(PrEPTriage obj, int Visit_Id, int LocationID, int UserId)
        {
            CLogger.WriteLog(ELogLevel.INFO, "DBClinicalEncounter.SaveUpdateTriage() method called");
            bool flag = true;
            int visitId = 0;
            ClsObject clsObjTP = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                clsObjTP.Connection = this.Connection;
                clsObjTP.Transaction = this.Transaction;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, obj.Ptn_pk.ToString());
                ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, obj.Visit_Id.ToString());
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, obj.LocationId.ToString());
                ClsUtility.AddParameters("@Visitdate", SqlDbType.DateTime, obj.OrdVisit.VisitDate.Value.Date.ToString("yyyy-MM-dd hh:mm:ss"));
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserId.ToString());
                ClsUtility.AddParameters("@VisitTypeId", SqlDbType.Int, obj.OrdVisit.VisitTypeId.ToString());
                //   ClsUtility.AddParameters("@HIVSupportgroup", SqlDbType.VarChar, obj.InitialEvaluation.HIVSupportgroup.ToString());
                //  ClsUtility.AddParameters("@HIVSupportGroupMembership", SqlDbType.VarChar, obj.InitialEvaluation.HIVSupportGroupMembership.ToString());
                //    ClsUtility.AddParameters("@Menarche", SqlDbType.VarChar, obj.InitialEvaluation.Menarche.ToString());
                //   ClsUtility.AddParameters("@AccompaniedByCaregiver", SqlDbType.VarChar, obj.InitialEvaluation.AccompaniedByCaregiver.ToString());
                //   ClsUtility.AddParameters("@CaregiverRelationship", SqlDbType.VarChar, obj.InitialEvaluation.CaregiverRelationship.ToString());
                ClsUtility.AddParameters("@BPDiastolic", SqlDbType.Int, obj.PatientVitals.BPDiastolic.ToString());
                ClsUtility.AddParameters("@BPSystolic", SqlDbType.Int, obj.PatientVitals.BPSystolic.ToString());
                ClsUtility.AddParameters("@TEMP", SqlDbType.Float, string.IsNullOrEmpty(obj.PatientVitals.TEMP) != true ? obj.PatientVitals.TEMP.ToString() : "0");
                ClsUtility.AddParameters("@RR", SqlDbType.Int, obj.PatientVitals.RR.ToString());
                ClsUtility.AddParameters("@HR", SqlDbType.Int, obj.PatientVitals.HR.ToString());
                ClsUtility.AddParameters("@Headcircumference", SqlDbType.Float, string.IsNullOrEmpty(obj.PatientVitals.HeadCircumference) != true ? obj.PatientVitals.HeadCircumference.ToString() : "0");
                ClsUtility.AddParameters("@height", SqlDbType.Int, obj.PatientVitals.Height.ToString());
                ClsUtility.AddParameters("@weight", SqlDbType.Int, obj.PatientVitals.Weight.ToString());
                ClsUtility.AddParameters("@MUAC", SqlDbType.Int, obj.PatientVitals.MUAC.ToString());
                ClsUtility.AddParameters("@weightforage", SqlDbType.Int, obj.PatientVitals.Weightforage.ToString());
                ClsUtility.AddParameters("@weightforheight", SqlDbType.Int, obj.PatientVitals.Weightforheight.ToString());
                ClsUtility.AddParameters("@BMIz", SqlDbType.Int, obj.PatientVitals.BMIz.ToString());
                ClsUtility.AddParameters("@NurseComments", SqlDbType.VarChar, obj.PatientVitals.NurseComments.ToString());
                ClsUtility.AddParameters("@HIVCareEnrollmentDate", SqlDbType.DateTime, obj.OrdVisit.VisitDate.ToString());
                ClsUtility.AddParameters("@DateHIVDiagnosis", SqlDbType.DateTime, obj.HIVCare.DateHIVDiagnosis.ToString());
                ClsUtility.AddParameters("@HIVDiagnosisVerified", SqlDbType.Int, obj.HIVCare.HIVDiagnosisVerified.ToString());
                //ClsUtility.AddParameters("@HIVCareWhere", SqlDbType.VarChar, obj.HIVCare.HIVCareWhere.ToString());
                ClsUtility.AddParameters("@ARTTransferInDate", SqlDbType.DateTime, obj.HIVCare.ARTTransferInDate.ToString());
                ClsUtility.AddParameters("@ARTTransferInFrom", SqlDbType.Int, obj.HIVCare.ARTTransferInFrom.ToString());
                ClsUtility.AddParameters("@FromDistrict", SqlDbType.Int, obj.HIVCare.District.ToString());
                ClsUtility.AddParameters("@ARTStartDate", SqlDbType.DateTime, obj.HIVCare.ARTStartDate.ToString());
                ClsUtility.AddParameters("@TransferIn", SqlDbType.Int, obj.HIVCare.TransferIn.ToString());
                ClsUtility.AddParameters("@ConfirmHIVPosDate", SqlDbType.DateTime, obj.HIVCare.ConfirmHIVPosDate.ToString());
                ClsUtility.AddParameters("@Signature", SqlDbType.Int, obj.Signature.ToString());
                ClsUtility.AddParameters("@FormName", SqlDbType.VarChar, obj.FormName.ToString());
                ClsUtility.AddParameters("@Spo2", SqlDbType.VarChar, obj.PatientVitals.SPO2.ToString());

                ClsUtility.AddParameters("@MFLCode", SqlDbType.VarChar, obj.HIVCare.MFLCode.ToString());
                //   ClsUtility.AddParameters("@FirstlineRegimen", SqlDbType.VarChar, obj.HIVCare.ARTStart.FirstLineRegimen.ToString());
                //   ClsUtility.AddParameters("@CurrentRegimen", SqlDbType.VarChar, obj.HIVCare.ARTStart.CurrentRegimen.ToString());
                //    ClsUtility.AddParameters("@AnotherWeight", SqlDbType.VarChar, obj.HIVCare.ARTStart.Weight.ToString());
                //    ClsUtility.AddParameters("@AnotherHeight", SqlDbType.VarChar, obj.HIVCare.ARTStart.Height.ToString());
                //     ClsUtility.AddParameters("@AnotherMUAC", SqlDbType.VarChar, obj.HIVCare.ARTStart.MUAC.ToString());
                //      ClsUtility.AddParameters("@AnotherCD4Count", SqlDbType.VarChar, obj.HIVCare.ARTStart.CD4Count.ToString());
                //      ClsUtility.AddParameters("@BaselineViralLoad", SqlDbType.VarChar, obj.HIVCare.ARTStart.BaselineViralLoad.ToString());
                //     ClsUtility.AddParameters("@BaselineViralLoadDate", SqlDbType.DateTime, obj.HIVCare.ARTStart.BaselineViralLoadDate.ToString());
                //     ClsUtility.AddParameters("@AnotherWHOStage", SqlDbType.Int, obj.HIVCare.ARTStart.WhoStage.ToString());

                //      ClsUtility.AddParameters("@ReferredFrom", SqlDbType.Int, obj.ReferredFrom.ToString());
                //     ClsUtility.AddParameters("@ReferredFromOther", SqlDbType.VarChar, obj.ReferredFromOther.ToString());

                DataTable ReturnDT = new DataTable();

                ReturnDT = (DataTable)clsObjTP.ReturnObject(ClsUtility.theParams, "Pr_PrEP_SaveUpdatePrEPTriage", ClsDBUtility.ObjectEnum.DataTable);

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                visitId = (int)ReturnDT.Rows[0]["VisitId"];
                return visitId;
            }
            catch (Exception ex)
            {
                DataMgr.RollBackTransation(this.Transaction);
                flag = false;
                throw ex;

            }

            finally
            {

                clsObjTP = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        public PrEP GetPrEPAssessmentData(int ptn_pk, int visitPK, int locationId)
        {
            CLogger.WriteLog(ELogLevel.INFO, "BusinessProcessClinical.BPrEP.GetPrEPAssessmentData() method called");

            oUtility = new ClsDBUtility();
            oUtility.Init_Hashtable();

            oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ConverTotValue.NullToInt(ptn_pk).ToString());
            oUtility.AddParameters("@Visit_Id", SqlDbType.Int, ConverTotValue.NullToInt(visitPK).ToString());
            oUtility.AddParameters("@LocationID", SqlDbType.Int, ConverTotValue.NullToInt(locationId).ToString());

            ClsObject GetRecs = new ClsObject();
            DataSet data = (DataSet)GetRecs.ReturnObject(oUtility.theParams, "Pr_PrEP_GetAssessmentData", ClsDBUtility.ObjectEnum.DataSet);

            PrEP objPrEP = new PrEP();

            objPrEP.ChronicCondition = new List<CodeDeCode>();
            objPrEP.FamilyPlanningMethods = new List<CodeDeCode>();
            objPrEP.PregnancyStatus = new List<CodeDeCode>();
            //objPrEP.PresentingComplaints = new List<CodeDeCode>();
            objPrEP.ReasonNotFamilyPlanning = new List<CodeDeCode>();
            objPrEP.ReasonsForAmenorrohea = new List<CodeDeCode>();
            objPrEP.VDRL_RH = new List<CodeDeCode>();
            objPrEP.OBSnGYNHistory = new OBSnGYNHistory();
            //objPrEP.PCT = new List<MultiSelectTable>();
            objPrEP.CDT = new List<MultiSelectTable>();

            try
            {
                /* Chronic Condition*/
                if (data.Tables[0].Rows.Count > 0)
                {
                    objPrEP.ChronicCondition = (from dt in data.Tables[0].AsEnumerable()
                                           select new CodeDeCode()
                                           {
                                               DeCodeId = dt.Field<int>("Id"),
                                               DeCodeName = dt.Field<string>("NAME")
                                           }).ToList();
                }

                ///* Presenting Complaints*/
                //if (data.Tables[1].Rows.Count > 0)
                //{
                //    objPrEP.PresentingComplaints = (from dt in data.Tables[1].AsEnumerable()
                //                               select new CodeDeCode()
                //                               {
                //                                   DeCodeId = dt.Field<int>("Id"),
                //                                   DeCodeName = dt.Field<string>("NAME")
                //                               }).ToList();
                //}

                /* Pregnancy status */
                if (data.Tables[2].Rows.Count > 0)
                {
                    objPrEP.PregnancyStatus = (from dt in data.Tables[2].AsEnumerable()
                                          select new CodeDeCode()
                                          {
                                              DeCodeId = dt.Field<int>("Id"),
                                              DeCodeName = dt.Field<string>("NAME")
                                          }).ToList();
                }

                /* Family Planning Methods */
                if (data.Tables[3].Rows.Count > 0)
                {
                    objPrEP.FamilyPlanningMethods = (from dt in data.Tables[3].AsEnumerable()
                                                select new CodeDeCode()
                                                {
                                                    DeCodeId = dt.Field<int>("Id"),
                                                    DeCodeName = dt.Field<string>("NAME")
                                                }).ToList();
                }

                /* Reason Not on Family Planning */
                if (data.Tables[4].Rows.Count > 0)
                {
                    objPrEP.ReasonNotFamilyPlanning = (from dt in data.Tables[4].AsEnumerable()
                                                  select new CodeDeCode()
                                                  {
                                                      DeCodeId = dt.Field<int>("Id"),
                                                      DeCodeName = dt.Field<string>("NAME")
                                                  }).ToList();
                }

                /* Reasons For Amenorrohea */
                if (data.Tables[5].Rows.Count > 0)
                {
                    objPrEP.ReasonsForAmenorrohea = (from dt in data.Tables[5].AsEnumerable()
                                                select new CodeDeCode()
                                                {
                                                    DeCodeId = dt.Field<int>("Id"),
                                                    DeCodeName = dt.Field<string>("NAME")
                                                }).ToList();
                }

                /* VDRL and RH */
                if (data.Tables[12].Rows.Count > 0)
                {
                    objPrEP.VDRL_RH = (from dt in data.Tables[12].AsEnumerable()
                                  select new CodeDeCode()
                                  {
                                      DeCodeId = dt.Field<int>("Id"),
                                      DeCodeName = dt.Field<string>("NAME")
                                  }).ToList();
                }

                DataTable dtData = new DataTable();

                if (data.Tables[6].Rows.Count > 0)
                {
                    dtData = data.Tables[6];

                    objPrEP.OBSnGYNHistory.Ptn_pk = Convert.ToInt32(dtData.Rows[0]["Ptn_Pk"].ToString());
                    objPrEP.OBSnGYNHistory.LocationId = Convert.ToInt32(dtData.Rows[0]["LocationId"].ToString());
                    objPrEP.OBSnGYNHistory.Visit_Id = Convert.ToInt32(dtData.Rows[0]["Visit_Pk"].ToString());
                    if (dtData.Rows[0]["Menarche"].ToString().Length > 0)
                    {
                        objPrEP.OBSnGYNHistory.Menarche = Convert.ToInt32(dtData.Rows[0]["Menarche"]);
                    }
                    if (dtData.Rows[0]["HPVvaccine"].ToString().Length > 0)
                    {
                        objPrEP.OBSnGYNHistory.HPVvaccine = Convert.ToInt32(dtData.Rows[0]["HPVvaccine"]);
                    }
                    if (dtData.Rows[0]["CervicalCancerScreened"].ToString().Length > 0)
                    {
                        objPrEP.OBSnGYNHistory.CervicalCancerScreened = Convert.ToInt32(dtData.Rows[0]["CervicalCancerScreened"]);
                    }
                    if (dtData.Rows[0]["CervicalCancerScreenedDate"].ToString().Length > 0)
                    {
                        objPrEP.OBSnGYNHistory.CervicalCancerScreenedDate = Convert.ToDateTime(dtData.Rows[0]["CervicalCancerScreenedDate"].ToString());
                    }
                    objPrEP.OBSnGYNHistory.OtherPresentingComplaints = dtData.Rows[0]["OtherPresentingComplaints"].ToString();
                    objPrEP.OBSnGYNHistory.PreviousAdmissionDiagnosis = dtData.Rows[0]["PreviousAdmissionDiagnosis"].ToString();
                }
                if (data.Tables[7].Rows.Count > 0)
                {
                    dtData = data.Tables[7];
                    if (dtData.Rows[0]["LMP"].ToString().Length > 0)
                    {
                        objPrEP.OBSnGYNHistory.LMP = Convert.ToDateTime(dtData.Rows[0]["LMP"].ToString());
                    }
                    if (dtData.Rows[0]["Pregnant"].ToString().Length > 0)
                    {
                        objPrEP.OBSnGYNHistory.Pregnant = Convert.ToInt32(dtData.Rows[0]["Pregnant"].ToString());
                    }
                    if (dtData.Rows[0]["EDD"].ToString().Length > 0)
                    {
                        objPrEP.OBSnGYNHistory.EDD = Convert.ToDateTime(dtData.Rows[0]["EDD"].ToString());
                    }
                    if (dtData.Rows[0]["DateofDelivery"].ToString().Length > 0)
                    {
                        objPrEP.OBSnGYNHistory.DateofDelivery = Convert.ToDateTime(dtData.Rows[0]["DateofDelivery"].ToString());
                    }
                    if (dtData.Rows[0]["DateofInducedAbortion"].ToString().Length > 0)
                    {
                        objPrEP.OBSnGYNHistory.DateofInducedAbortion = Convert.ToDateTime(dtData.Rows[0]["DateofInducedAbortion"].ToString());
                    }
                    if (dtData.Rows[0]["DateofMiscarriage"].ToString().Length > 0)
                    {
                        objPrEP.OBSnGYNHistory.DateofMiscarriage = Convert.ToDateTime(dtData.Rows[0]["DateofMiscarriage"].ToString());
                    }
                    if (dtData.Rows[0]["Amenorrhoea"].ToString().Length > 0)
                    {
                        objPrEP.OBSnGYNHistory.Amenorrhoea = Convert.ToInt32(dtData.Rows[0]["Amenorrhoea"].ToString());
                    }
                }
                if (data.Tables[8].Rows.Count > 0)
                {
                    dtData = data.Tables[8];
                    objPrEP.OBSnGYNHistory.BreastStatus = Convert.ToInt32(dtData.Rows[0]["BreastStatus"]);
                }
                if (data.Tables[9].Rows.Count > 0)
                {
                    dtData = data.Tables[9];
                    if (dtData.Rows[0]["FamilyPlanningMethod"].ToString().Length > 0)
                    {
                        objPrEP.OBSnGYNHistory.FamilyPlanningMethod = Convert.ToInt32(dtData.Rows[0]["FamilyPlanningMethod"].ToString());
                    }
                    if (dtData.Rows[0]["FamilyPlanningStatus"].ToString().Length > 0)
                    {
                        objPrEP.OBSnGYNHistory.FamilyPlanningStatus = Convert.ToInt32(dtData.Rows[0]["FamilyPlanningStatus"].ToString());
                    }
                    if (dtData.Rows[0]["NoFamilyPlanning"].ToString().Length > 0)
                    {
                        objPrEP.OBSnGYNHistory.NoFamilyPlanning = Convert.ToInt32(dtData.Rows[0]["NoFamilyPlanning"].ToString());
                    }
                }
                if (data.Tables[10].Rows.Count > 0)
                {
                    dtData = data.Tables[10];
                    if (dtData.Rows[0]["Gravidae"].ToString().Length > 0)
                    {
                        objPrEP.OBSnGYNHistory.Gravidae = Convert.ToInt32(dtData.Rows[0]["Gravidae"].ToString());
                    }
                    if (dtData.Rows[0]["Parity"].ToString().Length > 0)
                    {
                        objPrEP.OBSnGYNHistory.Parity = Convert.ToInt32(dtData.Rows[0]["Parity"].ToString());
                    }
                }

                if (data.Tables[11].Rows.Count > 0)
                {

                    //DataTable selectedTable = data.Tables[11].AsEnumerable()
                    //        .Where(r => r.Field<string>("FieldName").ToLower() == "presentingcomplaints")
                    //        .CopyToDataTable();

                    DataView theCodeDV = new DataView(data.Tables[11]);
                    theCodeDV.RowFilter = "FieldName = 'presentingcomplaints'";
                    DataTable selectedTable = (DataTable)SerializerUtil.CreateTableFromDataView(theCodeDV);

                    if (selectedTable.Rows.Count > 0)
                    {
                        //objPrEP.PCT = (from dt in selectedTable.AsEnumerable()
                        //          select new MultiSelectTable()
                        //          {
                        //              Ptn_pk = dt.Field<int>("Ptn_pk"),
                        //              Visit_Pk = dt.Field<int>("Visit_Pk"),
                        //              ValueID = dt.Field<int>("ValueID"),
                        //              FieldName = dt.Field<string>("FieldName"),
                        //              OnSetDate = dt.Field<DateTime?>("OnSetDate"),
                        //              NumericField = dt.Field<int>("NumericField")
                        //          }).ToList();
                    }

                    //selectedTable = data.Tables[11].AsEnumerable()
                    //        .Where(r => r.Field<string>("FieldName").ToLower() == "chroniccondition")
                    //        .CopyToDataTable();

                    theCodeDV = new DataView(data.Tables[11]);
                    theCodeDV.RowFilter = "FieldName = 'chroniccondition'";
                    selectedTable = (DataTable)SerializerUtil.CreateTableFromDataView(theCodeDV);

                    if (selectedTable.Rows.Count > 0)
                    {
                        objPrEP.CDT = (from dt in selectedTable.AsEnumerable()
                                  select new MultiSelectTable()
                                  {
                                      Ptn_pk = dt.Field<int>("Ptn_pk"),
                                      Visit_Pk = dt.Field<int>("Visit_Pk"),
                                      ValueID = dt.Field<int>("ValueID"),
                                      FieldName = dt.Field<string>("FieldName"),
                                      OnSetDate = dt.Field<DateTime?>("OnSetDate"),
                                      NumericField = dt.Field<int>("NumericField")
                                  }).ToList();
                    }
                }


                if (data.Tables[13].Rows.Count > 0)
                {
                    DataView theCodeDV = new DataView(data.Tables[13]);
                    theCodeDV.RowFilter = "LabName = 'Rhesus'";
                    DataTable dt = (DataTable)SerializerUtil.CreateTableFromDataView(theCodeDV);
                    if (dt.Rows.Count > 0)
                    {
                        objPrEP.OBSnGYNHistory.RhesusTestResult = Convert.ToInt32(dt.Rows[0]["TestResultId"].ToString());
                        if (dt.Rows[0]["ReportedbyDate"].ToString().Length > 0)
                        {
                            objPrEP.OBSnGYNHistory.RhesusTestDate = Convert.ToDateTime(dt.Rows[0]["ReportedbyDate"].ToString());
                        }
                    }

                    theCodeDV = new DataView(data.Tables[13]);
                    theCodeDV.RowFilter = "LabName = 'HCT/Hb'";
                    dt = (DataTable)SerializerUtil.CreateTableFromDataView(theCodeDV);
                    if (dt.Rows.Count > 0)
                    {
                        objPrEP.OBSnGYNHistory.HbTestResult = (int)Convert.ToDecimal(dt.Rows[0]["TestResults"].ToString());
                        if (dt.Rows[0]["ReportedbyDate"].ToString().Length > 0)
                        {
                            objPrEP.OBSnGYNHistory.HbTestDate = Convert.ToDateTime(dt.Rows[0]["ReportedbyDate"].ToString());
                        }
                    }

                    theCodeDV = new DataView(data.Tables[13]);
                    theCodeDV.RowFilter = "LabName = 'Syphilis (RPR)'";
                    dt = (DataTable)SerializerUtil.CreateTableFromDataView(theCodeDV);
                    if (dt.Rows.Count > 0)
                    {
                        objPrEP.OBSnGYNHistory.VDRLTestResult = Convert.ToInt32(dt.Rows[0]["TestResultId"].ToString());
                        if (dt.Rows[0]["ReportedbyDate"].ToString().Length > 0)
                        {
                            objPrEP.OBSnGYNHistory.VDRLTestDate = Convert.ToDateTime(dt.Rows[0]["ReportedbyDate"].ToString());
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                CLogger.WriteLog(ELogLevel.ERROR, "BusinessProcessClinical.BPrEP.GetPrEPTriageData() Method:" + ex.ToString());
            }
            return objPrEP;
        }
    }




}
