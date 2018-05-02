using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Interface.HIVCE;
using HIVCE.DataAccess;
using Application.Common;
using HIVCE.Common.Entities;
using HIVCE.Common;
using DataAccess.Base;


namespace HIVCE.BusinessLayer
{
    [Serializable()]
    public class BLClinicalEncounter : ProcessBase, IClinicalEncounter
    {
        DBClinicalEncounter dbLayer;
        public BLClinicalEncounter()
        {

        }

        public ClinicalEncounter GetData(int ptn_pk, int visitPK, int locationId)
        {
            CLogger.WriteLog(ELogLevel.INFO, "BLClinicalEncounter.GetData() method called");
            ClinicalEncounter ce = new ClinicalEncounter();
            ce.VisitTypes = new List<CodeDeCodeTables>();
            ce.ContractRelations = new List<CodeDeCodeTables>();
            ce.Districts = new List<District>();
            ce.FacilityList = new List<Facility>();
            ce.OrdVisit = new OrdVisit();
            ce.PatientVitals = new PatientVitals();
            ce.HIVCare = new HIVCare();
            ce.UserDesignation = new List<UserDesignation>();
            //string result = string.Empty;

            try
            {
                dbLayer = new DBClinicalEncounter();
                DataSet data = dbLayer.GetData(ptn_pk, visitPK, locationId);

                /* Visit Types*/
                if (data.Tables[0].Rows.Count > 0)
                {
                    ce.VisitTypes = (from dt in data.Tables[0].AsEnumerable()
                                     select new CodeDeCodeTables()
                   {
                       CodeId = dt.Field<int>("codeid"),
                       CodeName = dt.Field<string>("VisitType"),
                       DeCodeId = dt.Field<int>("Id"),
                       DeCodeName = dt.Field<string>("NAME")
                   }).ToList();
                }

                /* Contract Relations*/
                if (data.Tables[1].Rows.Count > 0)
                {
                    ce.ContractRelations = (from dt in data.Tables[1].AsEnumerable()
                                            select new CodeDeCodeTables()
                                            {
                                                CodeId = dt.Field<int>("codeid"),
                                                CodeName = dt.Field<string>("ContactRelation"),
                                                DeCodeId = dt.Field<int>("Id"),
                                                DeCodeName = dt.Field<string>("NAME")
                                            }).ToList();
                }

                /* User Designation*/
                if (data.Tables[11].Rows.Count > 0)
                {
                    ce.UserDesignation = (from dt in data.Tables[11].AsEnumerable().OrderBy(o => o.Field<string>("UserName"))
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
                    ce.Districts = (from dt in data.Tables[2].AsEnumerable().OrderBy(o => o.Field<int?>("SRno"))
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
                    ce.FacilityList = (from dt in data.Tables[3].AsEnumerable().OrderBy(o => o.Field<int?>("SRno"))
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
                    ce.OrdVisit = new OrdVisit();
                    foreach (DataRow row in data.Tables[4].Rows)
                    {
                        ce.OrdVisit.Visit_Id = Convert.ToInt32(row["Visit_Id"]);
                        ce.OrdVisit.Ptn_Pk = Convert.ToInt32(row["Ptn_Pk"]);
                        ce.OrdVisit.LocationId = Convert.ToInt32(row["LocationID"]);
                        ce.OrdVisit.VisitDate = string.IsNullOrEmpty(row["visitdate"].ToString()) == false ? Convert.ToDateTime(row["visitdate"].ToString()) : (DateTime?)null;
                        ce.OrdVisit.VisitTypeId = Convert.ToInt32(row["visittype"]);
                        ce.OrdVisit.CreatedDate = string.IsNullOrEmpty(row["CreateDate"].ToString()) == false ? Convert.ToDateTime(row["CreateDate"].ToString()) : (DateTime?)null;
                        ce.OrdVisit.UpdatedDate = string.IsNullOrEmpty(row["updatedate"].ToString()) == false ? Convert.ToDateTime(row["updatedate"].ToString()) : (DateTime?)null;
                        ce.OrdVisit.TypeOfVisit = Convert.ToInt32(row["TypeOfVisit"]);
                        ce.OrdVisit.Signature = Convert.ToInt32(row["Signature"]);
                    }
                }

                /* Initial Evaluation */
                if (data.Tables[5].Rows.Count > 0)
                {
                    foreach (DataRow row in data.Tables[5].Rows)
                    {
                        ce.InitialEvaluation = new InitialEvaluation();
                        ce.InitialEvaluation.Id = Convert.ToInt32(row["Id"]);
                        ce.InitialEvaluation.Visit_Pk = Convert.ToInt32(row["Visit_Pk"]);
                        ce.InitialEvaluation.Ptn_Pk = Convert.ToInt32(row["Ptn_Pk"]);
                        ce.InitialEvaluation.LocationId = Convert.ToInt32(row["LocationId"]);
                        ce.InitialEvaluation.HIVSupportgroup = row["HIVSupportgroup"].ToString();
                        ce.InitialEvaluation.HIVSupportGroupMembership = row["HIVSupportGroupMembership"].ToString();
                        ce.InitialEvaluation.Menarche = row["Menarche"].ToString();
                        ce.InitialEvaluation.AccompaniedByCaregiver = row["AccompaniedByCaregiver"].ToString();
                        ce.InitialEvaluation.CaregiverRelationship = row["CaregiverRelationship"].ToString();
                    }
                }

                /* Patient Vitals */
                if (data.Tables[6].Rows.Count > 0)
                {
                    //ce.PatientVitals = (from dt in data.Tables[6].AsEnumerable()
                    //                    select new PatientVitals()
                    //                        {
                    //                            Visit_Id = dt.Field<int>("visit_pk"),
                    //                            TEMP = dt.Field<string>("TEMP").ToString(),
                    //                            RR = dt.Field<string>("RR").ToString(),
                    //                            HR = dt.Field<string>("HR").ToString(),
                    //                            HeadCircumference = dt.Field<string>("Headcircumference").ToString(),
                    //                            Height = dt.Field<string>("height").ToString(),
                    //                            Weight = dt.Field<string>("weight").ToString(),
                    //                            MUAC = dt.Field<string>("MUAC").ToString(),
                    //                            Weightforage = dt.Field<string>("weightforage").ToString(),
                    //                            Weightforheight = dt.Field<string>("weightforheight").ToString(),
                    //                            BMIz = dt.Field<string>("BMIz").ToString(),
                    //                            NurseComments = dt.Field<string>("NurseComments").ToString()
                    //                        }).FirstOrDefault();

                    DataTable dt = new DataTable();
                    dt = data.Tables[6];

                    ce.PatientVitals = new PatientVitals();

                    ce.PatientVitals.Visit_Id = Convert.ToInt32(dt.Rows[0]["visit_pk"].ToString());
                    ce.PatientVitals.BPDiastolic = dt.Rows[0]["BPDiastolic"].ToString();
                    ce.PatientVitals.BPSystolic = dt.Rows[0]["BPSystolic"].ToString();
                    ce.PatientVitals.TEMP = dt.Rows[0]["TEMP"].ToString();
                    ce.PatientVitals.RR = dt.Rows[0]["RR"].ToString();
                    ce.PatientVitals.HR = dt.Rows[0]["HR"].ToString();
                    ce.PatientVitals.HeadCircumference = dt.Rows[0]["Headcircumference"].ToString();
                    ce.PatientVitals.Height = dt.Rows[0]["height"].ToString();
                    ce.PatientVitals.Weight = dt.Rows[0]["weight"].ToString();
                    ce.PatientVitals.MUAC = dt.Rows[0]["MUAC"].ToString();
                    ce.PatientVitals.Weightforage = dt.Rows[0]["weightforage"].ToString();
                    ce.PatientVitals.Weightforheight = dt.Rows[0]["weightforheight"].ToString();
                    ce.PatientVitals.BMIz = dt.Rows[0]["BMIz"].ToString();
                    ce.PatientVitals.NurseComments = dt.Rows[0]["NurseComments"].ToString();


                }

                /* Patient Vitals */
                if (data.Tables[7].Rows.Count > 0)
                {
                    //ce.HIVCare = (from dt in data.Tables[7].AsEnumerable()
                    //              select new HIVCare()
                    //                    {
                    //                        Visit_Id = dt.Field<int>("Visit_Id"),
                    //                        Ptn_pk = dt.Field<int>("Ptn_Pk"),
                    //                        LocationId = dt.Field<int>("LocationID"),
                    //                        HIVCareEnrollmentDate = dt.Field<DateTime?>("HIVCareEnrollmentDate"),
                    //                        DateHIVDiagnosis = dt.Field<DateTime?>("DateHIVDiagnosis"),
                    //                        HIVDiagnosisVerified = dt.Field<int?>("HIVDiagnosisVerified"),
                    //                        HIVCareWhere = dt.Field<string>("HIVCareWhere"),
                    //                        ARTTransferInDate = dt.Field<DateTime?>("ARTTransferInDate"),
                    //                        ARTTransferInFrom = dt.Field<int>("ARTTransferInFrom"),
                    //                        District = dt.Field<int>("FromDistrict"),
                    //                        UserId = dt.Field<int>("UserId"),
                    //                        ARTStartDate = dt.Field<DateTime?>("ARTStartDate"),
                    //                        TransferIn = dt.Field<int?>("TransferIn"),
                    //                        ConfirmHIVPosDate = dt.Field<DateTime?>("ConfirmHIVPosDate")
                    //                    }).FirstOrDefault();

                    ce.HIVCare = new HIVCare();
                    DataTable dt = new DataTable();
                    dt = data.Tables[7];

                    ce.HIVCare.Visit_Id = Convert.ToInt32(dt.Rows[0]["Visit_Id"].ToString());
                    ce.HIVCare.Ptn_pk = Convert.ToInt32(dt.Rows[0]["Ptn_Pk"].ToString());
                    ce.HIVCare.LocationId = Convert.ToInt32(dt.Rows[0]["LocationID"].ToString());
                    ce.HIVCare.HIVCareEnrollmentDate = Convert.ToDateTime(dt.Rows[0]["HIVCareEnrollmentDate"].ToString());
                    ce.HIVCare.DateHIVDiagnosis = string.IsNullOrEmpty(dt.Rows[0]["DateHIVDiagnosis"].ToString()) == false ? Convert.ToDateTime(dt.Rows[0]["DateHIVDiagnosis"].ToString()) : (DateTime?)null;
                    ce.HIVCare.HIVDiagnosisVerified = string.IsNullOrEmpty(dt.Rows[0]["HIVDiagnosisVerified"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["HIVDiagnosisVerified"].ToString()) : 0;
                    //ce.HIVCare.HIVCareWhere = dt.Rows[0]["HIVCareWhere"].ToString();
                    ce.HIVCare.ARTTransferInDate = string.IsNullOrEmpty(dt.Rows[0]["ARTTransferInDate"].ToString()) == false ? Convert.ToDateTime(dt.Rows[0]["ARTTransferInDate"].ToString()) : (DateTime?)null;
                    ce.HIVCare.ARTTransferInFrom = string.IsNullOrEmpty(dt.Rows[0]["ARTTransferInFrom"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["ARTTransferInFrom"].ToString()) : 0;
                    ce.HIVCare.District = string.IsNullOrEmpty(dt.Rows[0]["FromDistrict"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["FromDistrict"].ToString()) : 0;
                    ce.HIVCare.UserId = string.IsNullOrEmpty(dt.Rows[0]["UserId"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["UserId"].ToString()) : 0;
                    ce.HIVCare.ARTStartDate = string.IsNullOrEmpty(dt.Rows[0]["ARTStartDate"].ToString()) == false ? Convert.ToDateTime(dt.Rows[0]["ARTStartDate"].ToString()) : (DateTime?)null;
                    ce.HIVCare.TransferIn = string.IsNullOrEmpty(dt.Rows[0]["TransferIn"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["TransferIn"].ToString()) : 0;
                    ce.HIVCare.ConfirmHIVPosDate = string.IsNullOrEmpty(dt.Rows[0]["ConfirmHIVPosDate"].ToString()) == false ? Convert.ToDateTime(dt.Rows[0]["ConfirmHIVPosDate"].ToString()) : (DateTime?)null;

                    ce.ReferredFrom = string.IsNullOrEmpty(dt.Rows[0]["ReferredFrom"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["ReferredFrom"].ToString()) : (int?)null;

                    ce.ReferredFromOther = dt.Rows[0]["ReferredFromSpecify"].ToString();

                }

                if (data.Tables[12].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = data.Tables[12];
                    ce.PatientVitals.SPO2 = dt.Rows[0]["SPO2"].ToString();
                }

                if (data.Tables[13].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = data.Tables[13];
                    ce.ExistingInitialVisitId = string.IsNullOrEmpty(dt.Rows[0]["visit_id"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["visit_id"].ToString()) : 0;
                    ce.ExistingInitialVisitDate = string.IsNullOrEmpty(dt.Rows[0]["VisitDate"].ToString()) == false ? Convert.ToDateTime(dt.Rows[0]["VisitDate"].ToString()) : (DateTime?)null;
                }

                if (data.Tables[14].Rows.Count > 0)
                {
                    ce.AleardySavedTabs = (from dt in data.Tables[14].AsEnumerable()
                                           select new CodeDeCodeTables()
                                           {
                                               CodeId = dt.Field<int>("FeatureID"),
                                               CodeName = dt.Field<string>("FeatureName"),
                                               DeCodeId = dt.Field<int>("TabId"),
                                               DeCodeName = dt.Field<string>("TabName")
                                           }).ToList();
                }

                if (data.Tables[15].Rows.Count > 0)
                {
                    ce.HIVCare.ARTStart = new ARTStart();
                    ARTStart ar = new ARTStart();
                    foreach (DataRow row in data.Tables[15].Rows)
                    {

                        ar.Ptn_Pk = Convert.ToInt32(row["Ptn_Pk"].ToString());
                        ar.LocationId = Convert.ToInt32(row["LocationId"].ToString());
                        ar.Visit_Id = Convert.ToInt32(row["Visit_pk"].ToString());
                        ar.FirstLineRegimenDate = Convert.ToDateTime(row["FirstLineRegStDate"].ToString());
                        ar.FirstLineRegimen = row["Firstlinereg"].ToString();
                        ar.Weight = row["weight"].ToString();
                        ar.Height = row["Height"].ToString();
                        ar.WhoStage = row["whostage"].ToString();
                        ar.CurrentRegimen = row["CurrentRegimen"].ToString();
                        ar.BaselineViralLoad = row["BaselineViralLoad"].ToString();
                        ar.BaselineViralLoadDate = string.IsNullOrEmpty(row["BaselineViralLoadDate"].ToString()) == false ? Convert.ToDateTime(row["BaselineViralLoadDate"].ToString()) : (DateTime?)null;

                        ar.CD4Count = row["cd4"].ToString();
                        ar.MUAC = string.IsNullOrEmpty(row["MUAC"].ToString()) == false ? Convert.ToInt32(row["MUAC"].ToString()) : (int?)null;


                    }
                    ce.HIVCare.ARTStart = ar;
                }
                /*Regimen*/
                if (data.Tables[16].Rows.Count > 0)
                {
                    ce.Regimen = (from dt in data.Tables[16].AsEnumerable()
                                  select new MultiTableList()
                                  {
                                      Id = dt.Field<int>("RegimenId"),
                                      Name = dt.Field<string>("Regimen")
                                  }).ToList();
                }

                if (data.Tables[17].Rows.Count > 0)
                {
                    ce.WHOStage = (from dt in data.Tables[17].AsEnumerable().Where(o => o.Field<string>("CName") == "WHO Stage").ToList()
                                   select new MultiTableList()
                                   {
                                       Id = dt.Field<int>("id"),
                                       Name = dt.Field<string>("NAME")
                                   }).ToList();
                }

                ce.PatientReferred = new List<MultiTableList>();
                if (data.Tables[18].Rows.Count > 0)
                {
                    ce.PatientReferred = (from dt in data.Tables[18].AsEnumerable().ToList()
                                          select new MultiTableList()
                                          {
                                              Id = dt.Field<int>("id"),
                                              Name = dt.Field<string>("NAME")
                                          }).ToList();
                }

                /* Appointment Details */
                if (data.Tables[19].Rows.Count > 0)
                {
                    DataTable dtRow = new DataTable();
                    dtRow = data.Tables[19];

                    ce.AppDate = string.IsNullOrEmpty(dtRow.Rows[0]["AppDate"].ToString()) == false ? Convert.ToDateTime(dtRow.Rows[0]["AppDate"].ToString()) : (DateTime?)null;
                    ce.AppReason = string.IsNullOrEmpty(dtRow.Rows[0]["AppReason"].ToString()) == false ? dtRow.Rows[0]["AppReason"].ToString() : string.Empty;
                }

                /* PatientAdherence */
                if (data.Tables[20].Rows.Count > 0)
                {
                    DataTable dtRow = new DataTable();
                    dtRow = data.Tables[20];
                    ce.MgtSignature = string.IsNullOrEmpty(dtRow.Rows[0]["Signature"].ToString()) == false ? Convert.ToInt32(dtRow.Rows[0]["Signature"].ToString()) : 0;
                }

                /* Purpose */
                if (data.Tables[21].Rows.Count > 0)
                {
                    List<CodeDeCodeTables> lst = new List<CodeDeCodeTables>();

                    lst = (from dt in data.Tables[21].AsEnumerable()
                           select new CodeDeCodeTables()
                           {
                               DeCodeId = dt.Field<int>("ID"),
                               DeCodeName = dt.Field<string>("NAME"),
                               CodeName = "PUR"
                           }).ToList();

                    ce.Purpose.AddRange(lst);
                }
            }
            catch (Exception ex)
            {
                CLogger.WriteLog(ELogLevel.ERROR, "BLClinicalEncounter.GetData() Method:" + ex.ToString());
            }
            return ce;
        }

        public PresentingComplaint GetPresentingComplaints(int ptn_pk, int visitPK, int locationId)
        {
            CLogger.WriteLog(ELogLevel.INFO, "BLClinicalEncounter.GetPresentingComplaints() method called");
            PresentingComplaint pc = new PresentingComplaint();
            pc.ChronicCondition = new List<CodeDeCodeTables>();
            pc.FamilyPlanningMethods = new List<CodeDeCodeTables>();
            pc.PregnancyStatus = new List<CodeDeCodeTables>();
            pc.PresentingComplaints = new List<CodeDeCodeTables>();
            pc.ReasonNotFamilyPlanning = new List<CodeDeCodeTables>();
            pc.ReasonsForAmenorrohea = new List<CodeDeCodeTables>();
            pc.VDRL_RH = new List<CodeDeCodeTables>();
            pc.OBSnGYNHistory = new OBSnGYNHistory();
            pc.PCT = new List<MultiSelectTable>();
            pc.CDT = new List<MultiSelectTable>();
            try
            {
                dbLayer = new DBClinicalEncounter();
                DataSet data = dbLayer.GetPresentingComplaints(ptn_pk, visitPK, locationId);

                /* Chronic Condition*/
                if (data.Tables[0].Rows.Count > 0)
                {
                    pc.ChronicCondition = (from dt in data.Tables[0].AsEnumerable()
                                           select new CodeDeCodeTables()
                                           {
                                               DeCodeId = dt.Field<int>("Id"),
                                               DeCodeName = dt.Field<string>("NAME")
                                           }).ToList();
                }

                /* Presenting Complaints*/
                if (data.Tables[1].Rows.Count > 0)
                {
                    pc.PresentingComplaints = (from dt in data.Tables[1].AsEnumerable()
                                               select new CodeDeCodeTables()
                                               {
                                                   DeCodeId = dt.Field<int>("Id"),
                                                   DeCodeName = dt.Field<string>("NAME")
                                               }).ToList();
                }

                /* Pregnancy status */
                if (data.Tables[2].Rows.Count > 0)
                {
                    pc.PregnancyStatus = (from dt in data.Tables[2].AsEnumerable()
                                          select new CodeDeCodeTables()
                                          {
                                              DeCodeId = dt.Field<int>("Id"),
                                              DeCodeName = dt.Field<string>("NAME")
                                          }).ToList();
                }

                /* Family Planning Methods */
                if (data.Tables[3].Rows.Count > 0)
                {
                    pc.FamilyPlanningMethods = (from dt in data.Tables[3].AsEnumerable()
                                                select new CodeDeCodeTables()
                                                {
                                                    DeCodeId = dt.Field<int>("Id"),
                                                    DeCodeName = dt.Field<string>("NAME")
                                                }).ToList();
                }

                /* Reason Not on Family Planning */
                if (data.Tables[4].Rows.Count > 0)
                {
                    pc.ReasonNotFamilyPlanning = (from dt in data.Tables[4].AsEnumerable()
                                                  select new CodeDeCodeTables()
                                                  {
                                                      DeCodeId = dt.Field<int>("Id"),
                                                      DeCodeName = dt.Field<string>("NAME")
                                                  }).ToList();
                }

                /* Reasons For Amenorrohea */
                if (data.Tables[5].Rows.Count > 0)
                {
                    pc.ReasonsForAmenorrohea = (from dt in data.Tables[5].AsEnumerable()
                                                select new CodeDeCodeTables()
                                                {
                                                    DeCodeId = dt.Field<int>("Id"),
                                                    DeCodeName = dt.Field<string>("NAME")
                                                }).ToList();
                }

                /* VDRL and RH */
                if (data.Tables[12].Rows.Count > 0)
                {
                    pc.VDRL_RH = (from dt in data.Tables[12].AsEnumerable()
                                  select new CodeDeCodeTables()
                                  {
                                      DeCodeId = dt.Field<int>("Id"),
                                      DeCodeName = dt.Field<string>("NAME")
                                  }).ToList();
                }

                DataTable dtData = new DataTable();

                if (data.Tables[6].Rows.Count > 0)
                {
                    dtData = data.Tables[6];

                    pc.OBSnGYNHistory.Ptn_pk = Convert.ToInt32(dtData.Rows[0]["Ptn_Pk"].ToString());
                    pc.OBSnGYNHistory.LocationId = Convert.ToInt32(dtData.Rows[0]["LocationId"].ToString());
                    pc.OBSnGYNHistory.Visit_Id = Convert.ToInt32(dtData.Rows[0]["Visit_Pk"].ToString());
                    if (dtData.Rows[0]["Menarche"].ToString().Length > 0)
                    {
                        pc.OBSnGYNHistory.Menarche = Convert.ToInt32(dtData.Rows[0]["Menarche"]);
                    }
                    if (dtData.Rows[0]["HPVvaccine"].ToString().Length > 0)
                    {
                        pc.OBSnGYNHistory.HPVvaccine = Convert.ToInt32(dtData.Rows[0]["HPVvaccine"]);
                    }
                    if (dtData.Rows[0]["CervicalCancerScreened"].ToString().Length > 0)
                    {
                        pc.OBSnGYNHistory.CervicalCancerScreened = Convert.ToInt32(dtData.Rows[0]["CervicalCancerScreened"]);
                    }
                    if (dtData.Rows[0]["CervicalCancerScreenedDate"].ToString().Length > 0)
                    {
                        pc.OBSnGYNHistory.CervicalCancerScreenedDate = Convert.ToDateTime(dtData.Rows[0]["CervicalCancerScreenedDate"].ToString());
                    }
                    pc.OBSnGYNHistory.OtherPresentingComplaints = dtData.Rows[0]["OtherPresentingComplaints"].ToString();
                    pc.OBSnGYNHistory.PreviousAdmissionDiagnosis = dtData.Rows[0]["PreviousAdmissionDiagnosis"].ToString();
                }
                if (data.Tables[7].Rows.Count > 0)
                {
                    dtData = data.Tables[7];
                    if (dtData.Rows[0]["LMP"].ToString().Length > 0)
                    {
                        pc.OBSnGYNHistory.LMP = Convert.ToDateTime(dtData.Rows[0]["LMP"].ToString());
                    }
                    if (dtData.Rows[0]["Pregnant"].ToString().Length > 0)
                    {
                        pc.OBSnGYNHistory.Pregnant = Convert.ToInt32(dtData.Rows[0]["Pregnant"].ToString());
                    }
                    if (dtData.Rows[0]["EDD"].ToString().Length > 0)
                    {
                        pc.OBSnGYNHistory.EDD = Convert.ToDateTime(dtData.Rows[0]["EDD"].ToString());
                    }
                    if (dtData.Rows[0]["DateofDelivery"].ToString().Length > 0)
                    {
                        pc.OBSnGYNHistory.DateofDelivery = Convert.ToDateTime(dtData.Rows[0]["DateofDelivery"].ToString());
                    }
                    if (dtData.Rows[0]["DateofInducedAbortion"].ToString().Length > 0)
                    {
                        pc.OBSnGYNHistory.DateofInducedAbortion = Convert.ToDateTime(dtData.Rows[0]["DateofInducedAbortion"].ToString());
                    }
                    if (dtData.Rows[0]["DateofMiscarriage"].ToString().Length > 0)
                    {
                        pc.OBSnGYNHistory.DateofMiscarriage = Convert.ToDateTime(dtData.Rows[0]["DateofMiscarriage"].ToString());
                    }
                    if (dtData.Rows[0]["Amenorrhoea"].ToString().Length > 0)
                    {
                        pc.OBSnGYNHistory.Amenorrhoea = Convert.ToInt32(dtData.Rows[0]["Amenorrhoea"].ToString());
                    }
                }
                if (data.Tables[8].Rows.Count > 0)
                {
                    dtData = data.Tables[8];
                    pc.OBSnGYNHistory.BreastStatus = Convert.ToInt32(dtData.Rows[0]["BreastStatus"]);
                }
                if (data.Tables[9].Rows.Count > 0)
                {
                    dtData = data.Tables[9];
                    if (dtData.Rows[0]["FamilyPlanningMethod"].ToString().Length > 0)
                    {
                        pc.OBSnGYNHistory.FamilyPlanningMethod = Convert.ToInt32(dtData.Rows[0]["FamilyPlanningMethod"].ToString());
                    }
                    if (dtData.Rows[0]["FamilyPlanningStatus"].ToString().Length > 0)
                    {
                        pc.OBSnGYNHistory.FamilyPlanningStatus = Convert.ToInt32(dtData.Rows[0]["FamilyPlanningStatus"].ToString());
                    }
                    if (dtData.Rows[0]["NoFamilyPlanning"].ToString().Length > 0)
                    {
                        pc.OBSnGYNHistory.NoFamilyPlanning = Convert.ToInt32(dtData.Rows[0]["NoFamilyPlanning"].ToString());
                    }
                }
                if (data.Tables[10].Rows.Count > 0)
                {
                    dtData = data.Tables[10];
                    if (dtData.Rows[0]["Gravidae"].ToString().Length > 0)
                    {
                        pc.OBSnGYNHistory.Gravidae = Convert.ToInt32(dtData.Rows[0]["Gravidae"].ToString());
                    }
                    if (dtData.Rows[0]["Parity"].ToString().Length > 0)
                    {
                        pc.OBSnGYNHistory.Parity = Convert.ToInt32(dtData.Rows[0]["Parity"].ToString());
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
                        pc.PCT = (from dt in selectedTable.AsEnumerable()
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

                    //selectedTable = data.Tables[11].AsEnumerable()
                    //        .Where(r => r.Field<string>("FieldName").ToLower() == "chroniccondition")
                    //        .CopyToDataTable();

                    theCodeDV = new DataView(data.Tables[11]);
                    theCodeDV.RowFilter = "FieldName = 'chroniccondition'";
                    selectedTable = (DataTable)SerializerUtil.CreateTableFromDataView(theCodeDV);

                    if (selectedTable.Rows.Count > 0)
                    {
                        pc.CDT = (from dt in selectedTable.AsEnumerable()
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
                        pc.OBSnGYNHistory.RhesusTestResult = Convert.ToInt32(dt.Rows[0]["TestResultId"].ToString());
                        if (dt.Rows[0]["ReportedbyDate"].ToString().Length > 0)
                        {
                            pc.OBSnGYNHistory.RhesusTestDate = Convert.ToDateTime(dt.Rows[0]["ReportedbyDate"].ToString());
                        }
                    }

                    theCodeDV = new DataView(data.Tables[13]);
                    theCodeDV.RowFilter = "LabName = 'HCT/Hb'";
                    dt = (DataTable)SerializerUtil.CreateTableFromDataView(theCodeDV);
                    if (dt.Rows.Count > 0)
                    {
                        pc.OBSnGYNHistory.HbTestResult = (int)Convert.ToDecimal(dt.Rows[0]["TestResults"].ToString());
                        if (dt.Rows[0]["ReportedbyDate"].ToString().Length > 0)
                        {
                            pc.OBSnGYNHistory.HbTestDate = Convert.ToDateTime(dt.Rows[0]["ReportedbyDate"].ToString());
                        }
                    }

                    theCodeDV = new DataView(data.Tables[13]);
                    theCodeDV.RowFilter = "LabName = 'Syphilis (RPR)'";
                    dt = (DataTable)SerializerUtil.CreateTableFromDataView(theCodeDV);
                    if (dt.Rows.Count > 0)
                    {
                        pc.OBSnGYNHistory.VDRLTestResult = Convert.ToInt32(dt.Rows[0]["TestResultId"].ToString());
                        if (dt.Rows[0]["ReportedbyDate"].ToString().Length > 0)
                        {
                            pc.OBSnGYNHistory.VDRLTestDate = Convert.ToDateTime(dt.Rows[0]["ReportedbyDate"].ToString());
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                CLogger.WriteLog(ELogLevel.ERROR, "BLClinicalEncounter.GetPresentingComplaints() Method:" + ex.ToString());
            }
            return pc;
        }

        public AddtionalHx GetAddtionalHx(int ptn_pk, int visitPK, int locationId)
        {
            CLogger.WriteLog(ELogLevel.INFO, "BLClinicalEncounter.GetAddtionalHx() method called");
            AddtionalHx pc = new AddtionalHx();

            try
            {
                dbLayer = new DBClinicalEncounter();
                DataSet data = dbLayer.GetAddtionalHx(ptn_pk, visitPK, locationId);

                pc.Ptn_pk = ptn_pk;
                pc.Visit_Id = visitPK;
                pc.LocationId = locationId;

                /* Milestone Assessed*/
                if (data.Tables[0].Rows.Count > 0)
                {
                    pc.MilestoneAssessed = (from dt in data.Tables[0].AsEnumerable()
                                            select new MultiTableList()
                                            {
                                                Id = dt.Field<int>("id"),
                                                Name = dt.Field<string>("NAME")
                                            }).ToList();
                }

                /* Immunisationperiod / Immunisationgiven*/
                if (data.Tables[1].Rows.Count > 0)
                {
                    pc.ImmunisationPeriod = new List<MultiTableList>();
                    pc.ImmunisationPeriod = (from dt in data.Tables[1].AsEnumerable().Where(o => o.Field<string>("CName") == "Immunisationperiod").ToList()
                                             select new MultiTableList()
                                             {
                                                 Id = dt.Field<int>("id"),
                                                 Name = dt.Field<string>("NAME")
                                             }).ToList();

                    pc.ImmunisationGiven = new List<MultiTableList>();
                    pc.ImmunisationGiven = (from dt in data.Tables[1].AsEnumerable().Where(o => o.Field<string>("CName") == "Immunisationgiven").ToList()
                                            select new MultiTableList()
                                            {
                                                Id = dt.Field<int>("id"),
                                                Name = dt.Field<string>("NAME")
                                            }).ToList();

                }

                /* ImmunisationStatus*/
                if (data.Tables[2].Rows.Count > 0)
                {
                    pc.ImmunisationStatus = (from dt in data.Tables[2].AsEnumerable()
                                             select new MultiTableList()
                                             {
                                                 Id = dt.Field<int>("id"),
                                                 Name = dt.Field<string>("NAME")
                                             }).ToList();
                }

                /* 
                 LongTermMedications, 
		            Prior ARt Purpose - Treatment Program
		            'WHO Stage'
		            Allergen - 'Environmental','Food'
		            'Severity'
		            AllergyType
		            HIV status
		            Gender
		            SexualOrientation
		            'PartnerHIVStatus'
		            'HighRisk'
                 */
                if (data.Tables[3].Rows.Count > 0)
                {
                    pc.LongTermMedicationsList = (from dt in data.Tables[3].AsEnumerable().Where(o => o.Field<string>("CName") == "LongTermMedications").ToList()
                                                  select new MultiTableList()
                                                  {
                                                      Id = dt.Field<int>("id"),
                                                      Name = dt.Field<string>("NAME")
                                                  }).ToList();

                    pc.Gender = (from dt in data.Tables[3].AsEnumerable().Where(o => o.Field<string>("CName") == "Sex").ToList()
                                 select new MultiTableList()
                                 {
                                     Id = dt.Field<int>("id"),
                                     Name = dt.Field<string>("NAME")
                                 }).ToList();

                    pc.HIVStatus = (from dt in data.Tables[3].AsEnumerable().Where(o => o.Field<string>("CName") == "HIVStatus").ToList()
                                    select new MultiTableList()
                                    {
                                        Id = dt.Field<int>("id"),
                                        Name = dt.Field<string>("NAME")
                                    }).ToList();

                    pc.WHOStage = (from dt in data.Tables[3].AsEnumerable().Where(o => o.Field<string>("CName") == "WHO Stage").ToList()
                                   select new MultiTableList()
                                   {
                                       Id = dt.Field<int>("id"),
                                       Name = dt.Field<string>("NAME")
                                   }).ToList();

                    pc.PriorARTPurpose = (from dt in data.Tables[3].AsEnumerable().Where(o => o.Field<string>("CName") == "Treatment Program").ToList()
                                          select new MultiTableList()
                                          {
                                              Id = dt.Field<int>("id"),
                                              Name = dt.Field<string>("NAME")
                                          }).ToList();

                    pc.environmentalAllergen = (from dt in data.Tables[3].AsEnumerable().Where(o => o.Field<string>("CName") == "Environmental").ToList()
                                                select new MultiTableList()
                                                {
                                                    Id = dt.Field<int>("id"),
                                                    Name = dt.Field<string>("NAME")
                                                }).ToList();

                    pc.foodAllergen = (from dt in data.Tables[3].AsEnumerable().Where(o => o.Field<string>("CName") == "Food").ToList()
                                       select new MultiTableList()
                                       {
                                           Id = dt.Field<int>("id"),
                                           Name = dt.Field<string>("NAME")
                                       }).ToList();

                    pc.Severity = (from dt in data.Tables[3].AsEnumerable().Where(o => o.Field<string>("CName") == "Severity").ToList()
                                   select new MultiTableList()
                                   {
                                       Id = dt.Field<int>("id"),
                                       Name = dt.Field<string>("NAME")
                                   }).ToList();

                    pc.PartnerHIVStatus = (from dt in data.Tables[3].AsEnumerable().Where(o => o.Field<string>("CName") == "PartnerHIVStatus").ToList()
                                           select new MultiTableList()
                                           {
                                               Id = dt.Field<int>("id"),
                                               Name = dt.Field<string>("NAME")
                                           }).ToList();

                    pc.PriorARTPurpose = (from dt in data.Tables[3].AsEnumerable().Where(o => o.Field<string>("CName") == "Treatment Program").ToList()
                                          select new MultiTableList()
                                          {
                                              Id = dt.Field<int>("id"),
                                              Name = dt.Field<string>("NAME")
                                          }).ToList();

                    pc.SexualOrientation = (from dt in data.Tables[3].AsEnumerable().Where(o => o.Field<string>("CName") == "SexualOrientation").ToList()
                                            select new MultiTableList()
                                            {
                                                Id = dt.Field<int>("id"),
                                                Name = dt.Field<string>("NAME")
                                            }).ToList();
                    pc.HighRisk = (from dt in data.Tables[3].AsEnumerable().Where(o => o.Field<string>("CName") == "HighRisk").ToList()
                                   select new MultiTableList()
                                   {
                                       Id = dt.Field<int>("id"),
                                       Name = dt.Field<string>("NAME")
                                   }).ToList();
                }

                /* AllergyType*/
                if (data.Tables[4].Rows.Count > 0)
                {
                    pc.AllergyType = (from dt in data.Tables[4].AsEnumerable()
                                      select new MultiTableList()
                                      {
                                          Id = dt.Field<int>("codeid"),
                                          Name = dt.Field<string>("NAME")
                                      }).ToList();
                }

                /*DrugRegimens */
                if (data.Tables[5].Rows.Count > 0)
                {
                    pc.DrugRegimens = (from dt in data.Tables[5].AsEnumerable()
                                       select new DrugRegimen()
                                       {
                                           DrugId = dt.Field<int>("DrugId"),
                                           DrugName = dt.Field<string>("DrugName"),
                                           DrugTypeId = dt.Field<int>("DrugTypeId"),
                                           DrugTypeName = dt.Field<string>("DrugTypeName"),
                                           GenericAbbrevation = dt.Field<string>("Generic Abbrevation")
                                           //ItemCode = dt.Field<int?>("Item Code")
                                       }).ToList();
                }



                /*Allergen*/
                if (data.Tables[6].Rows.Count > 0)
                {
                    pc.Allergen = (from dt in data.Tables[6].AsEnumerable()
                                   select new MultiTableList()
                                   {
                                       Id = dt.Field<int>("drug_pk"),
                                       Name = dt.Field<string>("drugname")
                                   }).ToList();
                }

                /*HivCareStatus*/
                if (data.Tables[7].Rows.Count > 0)
                {
                    pc.HivCareStatus = (from dt in data.Tables[7].AsEnumerable()
                                        select new MultiTableList()
                                        {
                                            Id = dt.Field<int>("id"),
                                            Name = dt.Field<string>("Name")
                                        }).ToList();
                }

                /*RelationshipType*/
                if (data.Tables[8].Rows.Count > 0)
                {
                    pc.RelationshipType = (from dt in data.Tables[8].AsEnumerable()
                                           select new MultiTableList()
                                           {
                                               Id = dt.Field<int>("id"),
                                               Name = dt.Field<string>("Name")
                                           }).ToList();
                }

                /*RelationshipType*/
                if (data.Tables[9].Rows.Count > 0)
                {
                    if (data.Tables[9].Rows[0]["LongTermMedications"].ToString().Length > 0)
                    {
                        pc.LongTermMedications = Convert.ToInt32(data.Tables[9].Rows[0]["LongTermMedications"].ToString());
                    }
                    if (data.Tables[9].Rows[0]["HerbalMeds"].ToString().Length > 0)
                    {
                        pc.LongTermMedications = Convert.ToInt32(data.Tables[9].Rows[0]["HerbalMeds"].ToString());
                    }
                    pc.Examinations = data.Tables[9].Rows[0]["Examinations"].ToString();
                    pc.ReasonForHerbalMedicine = data.Tables[9].Rows[0]["ReasonForHerbalMedicine"].ToString();
                }

                /*TannersStaging*/
                if (data.Tables[10].Rows.Count > 0)
                {
                    pc.TannersStaging = new List<TannersStaging>();

                    foreach (DataRow row in data.Tables[10].Rows)
                    {
                        TannersStaging ts = new TannersStaging();
                        ts.Id = Convert.ToInt32(row["id"].ToString());
                        ts.Ptn_Pk = Convert.ToInt32(row["Ptn_Pk"].ToString());
                        ts.LocationId = Convert.ToInt32(row["LocationId"].ToString());
                        ts.NextScreenedOn = string.IsNullOrEmpty(row["NextScreenedOn"].ToString()) == false ? Convert.ToDateTime(row["NextScreenedOn"].ToString()) : (DateTime?)null;
                        ts.LastScreenedOn = string.IsNullOrEmpty(row["LastScreenedOn"].ToString()) == false ? Convert.ToDateTime(row["LastScreenedOn"].ToString()) : (DateTime?)null;
                        ts.StagingDate = string.IsNullOrEmpty(row["StagingDate"].ToString()) == false ? Convert.ToDateTime(row["StagingDate"].ToString()) : (DateTime?)null;
                        ts.Gender = string.IsNullOrEmpty(row["Gender"].ToString()) == false ? Convert.ToInt32(row["Gender"].ToString()) : (int?)null;
                        ts.BreastsOrGenitals = Convert.ToInt32(row["BreastsOrGenitals"].ToString());
                        ts.PublicHair = Convert.ToInt32(row["PublicHair"].ToString());
                        ts.UserId = Convert.ToInt32(row["UserId"].ToString());
                        ts.DeleteFlag = Convert.ToInt32(row["DeleteFlag"].ToString());
                        ts.CreateDate = Convert.ToDateTime(row["CreateDate"].ToString());
                        ts.UpdateDate = Convert.ToDateTime(row["UpdateDate"].ToString());
                        ts.Visit_Pk = Convert.ToInt32(row["Visit_Pk"].ToString());

                        pc.TannersStaging.Add(ts);

                    }

                }

                /*PriorART*/
                if (data.Tables[11].Rows.Count > 0)
                {
                    pc.PriorART = new List<PriorART>();
                    foreach (DataRow row in data.Tables[11].Rows)
                    {
                        PriorART pArt = new PriorART();

                        pArt.Ptn_Pk = Convert.ToInt32(row["Ptn_Pk"].ToString());
                        pArt.LocationId = Convert.ToInt32(row["LocationId"].ToString());
                        pArt.Visit_Id = Convert.ToInt32(row["Visit_pk"].ToString());
                        pArt.PusposeId = string.IsNullOrEmpty(row["PurposeId"].ToString()) == false ? Convert.ToInt32(row["PurposeId"].ToString()) : (int?)null;
                        pArt.Regimen = row["Regimen"].ToString();
                        pArt.Regimenid = string.IsNullOrEmpty(row["Regimenid"].ToString()) == false ? Convert.ToInt32(row["Regimenid"].ToString()) : (int?)null;
                        pArt.LastDateUsed = string.IsNullOrEmpty(row["DateLastUsed"].ToString()) == false ? Convert.ToDateTime(row["DateLastUsed"].ToString()) : (DateTime?)null;
                        pArt.Userid = Convert.ToInt32(row["Userid"].ToString());
                        pArt.CreateDate = string.IsNullOrEmpty(row["CreateDate"].ToString()) == false ? Convert.ToDateTime(row["CreateDate"].ToString()) : (DateTime?)null;
                        pArt.UpdateDate = string.IsNullOrEmpty(row["UpdateDate"].ToString()) == false ? Convert.ToDateTime(row["UpdateDate"].ToString()) : (DateTime?)null;
                        pc.PriorART.Add(pArt);
                    }

                }

                /*ART Start in Another Facility   */
                //if (data.Tables[12].Rows.Count > 0)
                //{
                //    pc.ARTStart = new List<ARTStart>();
                //    foreach (DataRow row in data.Tables[12].Rows)
                //    {
                //        ARTStart ar = new ARTStart();
                //        ar.Ptn_Pk = Convert.ToInt32(row["Ptn_Pk"].ToString());
                //        ar.LocationId = Convert.ToInt32(row["LocationId"].ToString());
                //        ar.Visit_Id = Convert.ToInt32(row["Visit_pk"].ToString());
                //        ar.FirstLineRegimenDate = Convert.ToDateTime(row["FirstLineRegStDate"].ToString());
                //        ar.FirstLineRegimen = row["Firstlinereg"].ToString();
                //        ar.Weight = string.IsNullOrEmpty(row["weight"].ToString()) == false ? Convert.ToInt32(row["weight"].ToString()) : (int?)null;
                //        ar.Height = string.IsNullOrEmpty(row["Height"].ToString()) == false ? Convert.ToInt32(row["Height"].ToString()) : (int?)null;
                //        ar.WhoStage = string.IsNullOrEmpty(row["whostage"].ToString()) == false ? Convert.ToInt32(row["whostage"].ToString()) : (int?)null;
                //        pc.ARTStart.Add(ar);
                //    }
                //    //pc.ARTStart = (from dt in data.Tables[12].AsEnumerable()
                //    //               select new ARTStart()
                //    //               {
                //    //                   Ptn_Pk = dt.Field<int>("Ptn_Pk"),
                //    //                   LocationId = dt.Field<int>("LocationId"),
                //    //                   Visit_Id = dt.Field<int>("Visit_pk"),
                //    //                   FirstLineRegimenDate = dt.Field<DateTime?>("FirstLineRegStDate"),
                //    //                   FirstLineRegimen = dt.Field<string>("Firstlinereg"),
                //    //                   Weight = dt.Field<int?>("weight"),
                //    //                   Height = dt.Field<int?>("Height"),
                //    //                   WhoStage = dt.Field<int?>("whostage")
                //    //               }).ToList();
                //}

                /* Sexual History   */
                if (data.Tables[13].Rows.Count > 0)
                {
                    pc.SexualHistory = new List<PWPDetails>();
                    foreach (DataRow row in data.Tables[13].Rows)
                    {
                        PWPDetails pwp = new PWPDetails();
                        pwp.Id = Convert.ToInt32(row["ID"].ToString());
                        pwp.Ptn_Pk = Convert.ToInt32(row["Ptn_Pk"].ToString());
                        pwp.Visit_id = Convert.ToInt32(row["Visit_Pk"].ToString());
                        pwp.LocationID = Convert.ToInt32(row["LocationID"].ToString());
                        pwp.UserId = string.IsNullOrEmpty(row["UserID"].ToString()) == false ? Convert.ToInt32(row["UserID"].ToString()) : 0;
                        pwp.CreateDate = string.IsNullOrEmpty(row["CreateDate"].ToString()) == false ? Convert.ToDateTime(row["CreateDate"].ToString()) : (DateTime?)null;
                        pwp.UpdateDate = string.IsNullOrEmpty(row["UpdateDate"].ToString()) == false ? Convert.ToDateTime(row["UpdateDate"].ToString()) : (DateTime?)null;
                        pwp.SexuallyActiveLast6Months = string.IsNullOrEmpty(row["SexuallyActiveLast6Months"].ToString()) == false ? Convert.ToInt32(row["SexuallyActiveLast6Months"].ToString()) : 0;
                        pwp.SexualOrientation = string.IsNullOrEmpty(row["SexualOrientation"].ToString()) == false ? Convert.ToInt32(row["SexualOrientation"].ToString()) : 0;
                        pwp.HIVstatusOfsexualPartner = string.IsNullOrEmpty(row["HIVstatusOfsexualPartner"].ToString()) == false ? Convert.ToInt32(row["HIVstatusOfsexualPartner"].ToString()) : 0;
                        pwp.PartnersIn6Months = string.IsNullOrEmpty(row["PartnersIn6Months"].ToString()) == false ? Convert.ToInt32(row["PartnersIn6Months"].ToString()) : 0;
                        pwp.PartnersGender = string.IsNullOrEmpty(row["PartnersGender"].ToString()) == false ? Convert.ToInt32(row["PartnersGender"].ToString()) : 0;

                        pc.SexualHistory.Add(pwp);
                    }
                }


                /* 'Milestone' ,'Immunization' */
                if (data.Tables[14].Rows.Count > 0)
                {
                    pc.MilestoneImmunization = (from dt in data.Tables[14].AsEnumerable()
                                                select new GridData()
                                                {
                                                    Ptn_pk = dt.Field<int>("Ptn_pk"),
                                                    LocationID = dt.Field<int>("LocationID"),
                                                    Visit_pk = dt.Field<int>("Visit_pk"),
                                                    Section = dt.Field<string>("Section"),

                                                    TypeofTestId = dt.Field<int>("TypeofTestId"),
                                                    TypeofTest = dt.Field<string>("TypeofTest"),
                                                    ResultId = dt.Field<int>("ResultId"),
                                                    Result = dt.Field<string>("Result"),
                                                    Date = dt.Field<DateTime>("Date"),
                                                    Comments = dt.Field<string>("Comments"),
                                                    IsAchieved = dt.Field<int?>("IsAchieved")

                                                }).ToList();
                }

                /* 'LongTermMedications' ,'HighRisk' */
                if (data.Tables[15].Rows.Count > 0)
                {
                    pc.LTMnHR = (from dt in data.Tables[15].AsEnumerable()
                                 select new MultiSelectTable()
                                 {
                                     Ptn_pk = dt.Field<int>("Ptn_pk"),
                                     Visit_Pk = dt.Field<int>("Visit_Pk"),
                                     ValueID = dt.Field<int>("ValueID"),
                                     FieldName = dt.Field<string>("FieldName"),
                                     OnSetDate = dt.Field<DateTime?>("DateField1"),
                                     NumericField = dt.Field<int?>("NumericField"),
                                     Notes = dt.Field<string>("Other_Notes"),
                                     VNotes = dt.Field<string>("ValueIdOtherNotes")
                                 }).ToList();
                }

                /*Allergies   */
                if (data.Tables[16].Rows.Count > 0)
                {

                    pc.AllergiesDetail = new List<AllergiesDetail>();

                    foreach (DataRow row in data.Tables[16].Rows)
                    {
                        AllergiesDetail dt = new AllergiesDetail();
                        dt.Id = Convert.ToInt32(row["id"].ToString());
                        dt.Ptn_Pk = Convert.ToInt32(row["Ptn_Pk"].ToString());
                        dt.AllergyTypeDesc = row["AllergyTypeDesc"].ToString();
                        dt.AllergyTypeID = row["AllergyTypeID"].ToString();
                        dt.Allergen = row["Allergen"].ToString();
                        dt.AllergenDesc = row["AllergenDesc"].ToString();
                        dt.OtherAllergen = row["OtherAllergen"].ToString();
                        dt.TypeReaction = row["TypeReaction"].ToString();
                        dt.SeverityDesc = row["severityDesc"].ToString();
                        dt.SevrityTypeID = Convert.ToInt32(row["SevrityTypeID"].ToString());
                        dt.DateAllergy = string.IsNullOrEmpty(row["DateAllergy"].ToString()) == false ? Convert.ToDateTime(row["DateAllergy"].ToString()) : DateTime.Now;
                        dt.Comment = row["AllergiesComment"].ToString();
                        dt.UserID = Convert.ToInt32(row["UserID"].ToString());
                        dt.DeleteFlag = Convert.ToInt32(row["DeleteFlag"].ToString());
                        dt.CreateDate = Convert.ToDateTime(row["CreateDate"].ToString());
                        dt.UpdateDate = string.IsNullOrEmpty(row["UpdateDate"].ToString()) == false ? Convert.ToDateTime(row["UpdateDate"].ToString()) : DateTime.Now;
                        pc.AllergiesDetail.Add(dt);
                    }

                    //pc.AllergiesDetail = (from dt in data.Tables[13].AsEnumerable()
                    //                      select new AllergiesDetail()
                    //                      {
                    //                          Id = dt.Field<int>("id"),
                    //                          Ptn_Pk = dt.Field<int>("Ptn_Pk"),
                    //                          AllergyTypeDesc = dt.Field<string>("AllergyTypeDesc"),
                    //                          AllergyTypeID = dt.Field<string>("AllergyTypeID"),
                    //                          Allergen = dt.Field<string>("Allergen"),
                    //                          AllergenDesc = dt.Field<string>("AllergenDesc"),
                    //                          OtherAllergen = dt.Field<string>("OtherAllergen"),
                    //                          TypeReaction = dt.Field<string>("TypeReaction"),
                    //                          SeverityDesc = dt.Field<string>("severityDesc"),
                    //                          SevrityTypeID = dt.Field<int?>("SevrityTypeID"),
                    //                          DateAllergy = dt.Field<DateTime?>("DateAllergy"),
                    //                          Comment = dt.Field<string>("AllergiesComment"),
                    //                          UserID = dt.Field<int?>("UserID"),
                    //                          DeleteFlag = dt.Field<int?>("DeleteFlag"),
                    //                          CreateDate = dt.Field<DateTime?>("CreateDate"),
                    //                          UpdateDate = dt.Field<DateTime?>("UpdateDate")
                    //                      }).ToList();

                }


                /* Family History   */
                if (data.Tables[17].Rows.Count > 0)
                {
                    pc.FamilyDetails = new List<FamilyDetail>();
                    foreach (DataRow row in data.Tables[17].Rows)
                    {
                        FamilyDetail fd = new FamilyDetail();

                        fd.Id = Convert.ToInt32(row["Id"].ToString());
                        fd.Ptn_Pk = Convert.ToInt32(row["Ptn_Pk"].ToString());
                        fd.RFirstName = row["RFirstName"].ToString();
                        fd.RLastName = row["RLastName"].ToString();
                        fd.SexId = Convert.ToInt32(row["SexId"].ToString());
                        fd.SexDesc = row["SexDesc"].ToString();
                        fd.AgeYear = string.IsNullOrEmpty(row["AgeYear"].ToString()) == false ? Convert.ToInt32(row["AgeYear"].ToString()) : (int?)null;
                        fd.AgeMonth = string.IsNullOrEmpty(row["AgeMonth"].ToString()) == false ? Convert.ToInt32(row["AgeMonth"].ToString()) : (int?)null;
                        fd.RelationshipDate = string.IsNullOrEmpty(row["RelationshipDate"].ToString()) == false ? Convert.ToDateTime(row["RelationshipDate"].ToString()) : (DateTime?)null;
                        fd.RelationshipId = string.IsNullOrEmpty(row["RelationshipTypeID"].ToString()) == false ? Convert.ToInt32(row["RelationshipTypeID"].ToString()) : (int?)null;
                        fd.RelationshipDesc = row["RelationshipTypeDesc"].ToString();
                        fd.RegistrationNo = row["RegistrationNo"].ToString();
                        fd.FileNo = row["FileNo"].ToString();
                        fd.FileNo1 = row["FileNo1"].ToString();
                        fd.HIVStatusId = string.IsNullOrEmpty(row["HivStatusID"].ToString()) == false ? Convert.ToInt32(row["HivStatusID"].ToString()) : (int?)null;
                        fd.LastHIVTestDate = string.IsNullOrEmpty(row["LastHIVTestDate"].ToString()) == false ? Convert.ToDateTime(row["LastHIVTestDate"].ToString()) : (DateTime?)null;
                        fd.HIVStatusDesc = row["HivStatusDesc"].ToString();
                        fd.HIVCareStatusId = string.IsNullOrEmpty(row["HivCareStatusID"].ToString()) == false ? Convert.ToInt32(row["HivCareStatusID"].ToString()) : (int?)null;
                        fd.HIVCareStatusDesc = row["HivCareStatusDesc"].ToString();
                        fd.IsRegistered = row["Registered"].ToString();
                        fd.Referenceid = string.IsNullOrEmpty(row["ReferenceId"].ToString()) == false ? Convert.ToInt32(row["ReferenceId"].ToString()) : 0;

                        pc.FamilyDetails.Add(fd);
                    }
                }


                /* LongTermMedicationReason */
                if (data.Tables[18].Rows.Count > 0)
                {
                    pc.LongTermMedicationReason = (from dt in data.Tables[18].AsEnumerable()
                                                   select new MultiTableList()
                                           {
                                               Id = dt.Field<int>("Id"),
                                               Name = dt.Field<string>("NAME")
                                           }).ToList();
                }


                /* Regimen Codes */
                if (data.Tables[19].Rows.Count > 0)
                {
                    List<CodeDeCodeTables> lst = new List<CodeDeCodeTables>();

                    lst = (from dt in data.Tables[19].AsEnumerable()
                           select new CodeDeCodeTables()
                           {
                               DeCodeId = dt.Field<int>("RegimenID"),
                               DeCodeName = dt.Field<string>("RegimenValue"),
                               CodeName = "RegimenCodes"
                           }).ToList();
                    pc.RegimenCodes.AddRange(lst);

                }

                /* RegimenID */
                if (data.Tables[20].Rows.Count > 0)
                {
                    DataTable dtRow = new DataTable();
                    dtRow = data.Tables[20];

                    pc.RegimenCodeValue = string.IsNullOrEmpty(dtRow.Rows[0]["RegimenId"].ToString()) == false ? Convert.ToInt32(dtRow.Rows[0]["RegimenId"].ToString()) : 0;
                    //pc.RegimenVisitId = string.IsNullOrEmpty(dtRow.Rows[0]["Visit_Pk"].ToString()) == false ? Convert.ToInt32(dtRow.Rows[0]["Visit_Pk"].ToString()) : 0;
                }


            }
            catch (Exception ex)
            {
                CLogger.WriteLog(ELogLevel.ERROR, "BLClinicalEncounter.GetAddtionalHx() Method:" + ex.ToString());
            }
            return pc;
        }

        public DBScreeningData GetScreeningData(int ptn_pk, int visitPK, int locationId)
        {
            CLogger.WriteLog(ELogLevel.INFO, "BLClinicalEncounter.GetScreeningData() method called");
            DBScreeningData dataSC = new DBScreeningData();

            try
            {
                dbLayer = new DBClinicalEncounter();
                DataSet data = dbLayer.GetScreeningData(ptn_pk, visitPK, locationId);

                if (data.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in data.Tables[0].Rows)
                    {
                        dataSC.Ptn_Pk = Convert.ToInt32(row["Ptn_Pk"].ToString());
                        dataSC.LocationId = Convert.ToInt32(row["LocationId"].ToString());
                        dataSC.Visit_Id = Convert.ToInt32(row["Visit_pk"].ToString());

                        dataSC.TbFindings = string.IsNullOrEmpty(row["TBFindings"].ToString()) == false ? Convert.ToInt32(row["TBFindings"].ToString()) : 0;
                        dataSC.TBRegimen = string.IsNullOrEmpty(row["TBRegimen"].ToString()) == false ? Convert.ToInt32(row["TBRegimen"].ToString()) : 0;
                        dataSC.TBTreatment = string.IsNullOrEmpty(row["TBTreatment"].ToString()) == false ? Convert.ToInt32(row["TBTreatment"].ToString()) : 0;

                        dataSC.RxDuration = string.IsNullOrEmpty(row["TBRxDuration"].ToString()) == false ? Convert.ToInt32(row["TBRxDuration"].ToString()) : 0;
                        dataSC.SputumSmear = string.IsNullOrEmpty(row["SputumSmear"].ToString()) == false ? Convert.ToInt32(row["SputumSmear"].ToString()) : 0;
                        dataSC.GeneExpert = string.IsNullOrEmpty(row["GeneExpert"].ToString()) == false ? Convert.ToInt32(row["GeneExpert"].ToString()) : 0;

                        dataSC.SputumDST = string.IsNullOrEmpty(row["SputumDST"].ToString()) == false ? Convert.ToInt32(row["SputumDST"].ToString()) : 0;
                        dataSC.CXRResults = string.IsNullOrEmpty(row["CXRResults"].ToString()) == false ? Convert.ToInt32(row["CXRResults"].ToString()) : 0;
                        dataSC.IPTAdherence = string.IsNullOrEmpty(row["IPTAdherence"].ToString()) == false ? Convert.ToInt32(row["IPTAdherence"].ToString()) : 0;

                        dataSC.IPTContraindication = string.IsNullOrEmpty(row["IPTContraindication"].ToString()) == false ? Convert.ToInt32(row["IPTContraindication"].ToString()) : 0;
                        dataSC.IPTDiscontinued = string.IsNullOrEmpty(row["IPTDiscontinued"].ToString()) == false ? Convert.ToInt32(row["IPTDiscontinued"].ToString()) : 0;
                        dataSC.IsTBContactsScreened = string.IsNullOrEmpty(row["ContactsScreenedForTB"].ToString()) == false ? Convert.ToInt32(row["ContactsScreenedForTB"].ToString()) : 0;
                        dataSC.IPT = string.IsNullOrEmpty(row["IPT"].ToString()) == false ? Convert.ToInt32(row["IPT"].ToString()) : 0;
                        //dataSC.IPTName = string.IsNullOrEmpty(row["IPTName"].ToString()) == false ? row["IPTName"].ToString() : "";
                        dataSC.rbChestXray = string.IsNullOrEmpty(row["ChestXRay"].ToString()) == false ? Convert.ToInt32(row["ChestXRay"].ToString()) : 0;
                        dataSC.rbTissueBiopsy = string.IsNullOrEmpty(row["TissueBiopsy"].ToString()) == false ? Convert.ToInt32(row["TissueBiopsy"].ToString()) : 0;

                        dataSC.SputumSmearDate = string.IsNullOrEmpty(row["SputumSmearDate"].ToString()) == false ? Convert.ToDateTime(row["SputumSmearDate"].ToString()) : (DateTime?)null;
                        dataSC.GeneExpertDate = string.IsNullOrEmpty(row["GeneExpertDate"].ToString()) == false ? Convert.ToDateTime(row["GeneExpertDate"].ToString()) : (DateTime?)null;
                        dataSC.SputumDSTDate = string.IsNullOrEmpty(row["SputumDSTDate"].ToString()) == false ? Convert.ToDateTime(row["SputumDSTDate"].ToString()) : (DateTime?)null;
                        dataSC.ChestXrayDate = string.IsNullOrEmpty(row["ChestXRayDate"].ToString()) == false ? Convert.ToDateTime(row["ChestXRayDate"].ToString()) : (DateTime?)null;
                        dataSC.TissueBiopsyDate = string.IsNullOrEmpty(row["TissueBiopsyDate"].ToString()) == false ? Convert.ToDateTime(row["TissueBiopsyDate"].ToString()) : (DateTime?)null;
                        dataSC.TBStartDate = string.IsNullOrEmpty(row["TBRegimenStartDate"].ToString()) == false ? Convert.ToDateTime(row["TBRegimenStartDate"].ToString()) : (DateTime?)null;
                        dataSC.IPTStartDate = string.IsNullOrEmpty(row["INHStartDate"].ToString()) == false ? Convert.ToDateTime(row["INHStartDate"].ToString()) : (DateTime?)null;
                        dataSC.IPTEndDate = string.IsNullOrEmpty(row["INHEndDate"].ToString()) == false ? Convert.ToDateTime(row["INHEndDate"].ToString()) : (DateTime?)null;

                        dataSC.EligibleForIPT = string.IsNullOrEmpty(row["EligibleForIPT"].ToString()) == false ? Convert.ToInt32(row["EligibleForIPT"].ToString()) : 0;

                        dataSC.IPTWPYellowColoredUrine = string.IsNullOrEmpty(row["IPTWPYellowColoredUrine"].ToString()) == false ? Convert.ToInt32(row["IPTWPYellowColoredUrine"].ToString()) : 0;
                        dataSC.IPTWPNumbnessBurning = string.IsNullOrEmpty(row["IPTWPNumbnessBurning"].ToString()) == false ? Convert.ToInt32(row["IPTWPNumbnessBurning"].ToString()) : 0;
                        dataSC.IPTWPYellownessEyes = string.IsNullOrEmpty(row["IPTWPYellownessEyes"].ToString()) == false ? Convert.ToInt32(row["IPTWPYellownessEyes"].ToString()) : 0;
                        dataSC.IPTWPTenderness = string.IsNullOrEmpty(row["IPTWPTenderness"].ToString()) == false ? Convert.ToInt32(row["IPTWPTenderness"].ToString()) : 0;

                        dataSC.OtherReasonDeclinedIPT = string.IsNullOrEmpty(row["OtherReasonDeclinedIPT"].ToString()) == false ? row["OtherReasonDeclinedIPT"].ToString() : "";
                        dataSC.OtherReasonDiscontinuedIPT = string.IsNullOrEmpty(row["OtherReasonDiscontinuedIPT"].ToString()) == false ? row["OtherReasonDiscontinuedIPT"].ToString() : "";
                    }
                }

                if (data.Tables[1].Rows.Count > 0)
                {
                    dataSC.TBScreeningDisplay = (from dt in data.Tables[1].AsEnumerable()
                                                 select new MultiSelectTable()
                                                 {
                                                     Ptn_pk = dt.Field<int>("Ptn_pk"),
                                                     Visit_Pk = dt.Field<int>("Visit_Pk"),
                                                     ValueID = dt.Field<int>("ValueID"),
                                                     FieldName = dt.Field<string>("FieldName")
                                                 }).ToList();
                }


                dataSC.AlcoholDepressionScreening = new List<AlcoholDepressionScreeningObj>();
                BLAlcoholDepressionScreening blads = new BLAlcoholDepressionScreening();
                AlcoholDepressionScreeningObj adsObj = new AlcoholDepressionScreeningObj();
                adsObj = blads.GetAlcoholDepressionScreening(ptn_pk, visitPK);

                dataSC.AlcoholDepressionScreening.Add(adsObj);
            }
            catch (Exception ex)
            {
                CLogger.WriteLog(ELogLevel.ERROR, "BLClinicalEncounter.GetScreeningData() Method:" + ex.ToString());
            }
            return dataSC;
        }

        public DBSystemicReviewData GetSystemicReviewData(int ptn_pk, int visitPK, int locationId)
        {
            CLogger.WriteLog(ELogLevel.INFO, "BLClinicalEncounter.GetSystemicReviewData() method called");
            DBSystemicReviewData dataSC = new DBSystemicReviewData();

            try
            {
                dbLayer = new DBClinicalEncounter();
                DataSet data = dbLayer.GetSystemicReviewData(ptn_pk, visitPK, locationId);

                if (data.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in data.Tables[0].Rows)
                    {
                        dataSC.Ptn_Pk = Convert.ToInt32(row["Ptn_Pk"].ToString());
                        dataSC.LocationId = Convert.ToInt32(row["LocationId"].ToString());
                        dataSC.Visit_Id = Convert.ToInt32(row["Visit_pk"].ToString());

                        dataSC.NutritionalAssessment = string.IsNullOrEmpty(row["NutritionalProblem"].ToString()) == false ? Convert.ToInt32(row["NutritionalProblem"].ToString()) : 0;
                        dataSC.NutritionalAssessmentComments = string.IsNullOrEmpty(row["NutritionalProblem"].ToString()) == false ? row["Dischargenote"].ToString() : "";
                        dataSC.NutritionCounseling = string.IsNullOrEmpty(row["NutritionCounseling"].ToString()) == false ? Convert.ToInt32(row["NutritionCounseling"].ToString()) : 0;
                        dataSC.WHOStage = string.IsNullOrEmpty(row["whostage"].ToString()) == false ? Convert.ToInt32(row["whostage"].ToString()) : 0;
                        dataSC.ReviewSystemComments = string.IsNullOrEmpty(row["ReviewSystemComments"].ToString()) == false ? row["ReviewSystemComments"].ToString() : "";

                    }
                }

                if (data.Tables[1].Rows.Count > 0)
                {
                    dataSC.GeneralExamination = (from dt in data.Tables[1].AsEnumerable().Where(o => o.Field<string>("FieldName") == "GeneralConditions").ToList()
                                                 select new DBMultiSelectSelcted()
                                   {
                                       id = dt.Field<int>("ValueID")
                                   }).ToList();

                    dataSC.SkinExamination = (from dt in data.Tables[1].AsEnumerable().Where(o => o.Field<string>("FieldName") == "SkinConditions").ToList()
                                              select new DBMultiSelectSelcted()
                                              {
                                                  id = dt.Field<int>("ValueID")
                                              }).ToList();

                    dataSC.ENTExamination = (from dt in data.Tables[1].AsEnumerable().Where(o => o.Field<string>("FieldName") == "ENTConditions").ToList()
                                             select new DBMultiSelectSelcted()
                                             {
                                                 id = dt.Field<int>("ValueID")
                                             }).ToList();

                    dataSC.ChestExamination = (from dt in data.Tables[1].AsEnumerable().Where(o => o.Field<string>("FieldName") == "ChestLungsConditions").ToList()
                                               select new DBMultiSelectSelcted()
                                               {
                                                   id = dt.Field<int>("ValueID")
                                               }).ToList();

                    dataSC.CVSExamination = (from dt in data.Tables[1].AsEnumerable().Where(o => o.Field<string>("FieldName") == "CardiovascularConditions").ToList()
                                             select new DBMultiSelectSelcted()
                                             {
                                                 id = dt.Field<int>("ValueID")
                                             }).ToList();

                    dataSC.AbdomenExamination = (from dt in data.Tables[1].AsEnumerable().Where(o => o.Field<string>("FieldName") == "AbdomenConditions").ToList()
                                                 select new DBMultiSelectSelcted()
                                                 {
                                                     id = dt.Field<int>("ValueID")
                                                 }).ToList();

                    dataSC.CNSExamination = (from dt in data.Tables[1].AsEnumerable().Where(o => o.Field<string>("FieldName") == "CNSConditions").ToList()
                                             select new DBMultiSelectSelcted()
                                             {
                                                 id = dt.Field<int>("ValueID")
                                             }).ToList();

                    dataSC.GenitoUrinary = (from dt in data.Tables[1].AsEnumerable().Where(o => o.Field<string>("FieldName") == "GenitalUrinaryConditions").ToList()
                                            select new DBMultiSelectSelcted()
                                            {
                                                id = dt.Field<int>("ValueID")
                                            }).ToList();

                    var whoInClause = new string[] { "WHOStageIConditions", "WHOStageIIConditions", "WHOStageIIICoditions", "WHOStageIVConditions" };

                    dataSC.WHOStages = (from dt in data.Tables[1].AsEnumerable().Where(o => whoInClause.Contains(o.Field<string>("FieldName"))).ToList()
                                        select new WHOStage()
                                             {
                                                 ValueId = dt.Field<int>("ValueID"),
                                                 FieldName = dt.Field<string>("FieldName"),
                                                 DateField1 = dt.Field<DateTime?>("DateField1"),
                                                 DateField2 = dt.Field<DateTime?>("DateField2")
                                             }).ToList();

                }



            }
            catch (Exception ex)
            {
                CLogger.WriteLog(ELogLevel.ERROR, "BLClinicalEncounter.GetSystemicReviewData() Method:" + ex.ToString());
            }
            return dataSC;
        }

        public Management GetManagement(int ptn_pk, int visitPK, int locationId)
        {
            CLogger.WriteLog(ELogLevel.INFO, "BLClinicalEncounter.GetManagement() method called");
            Management mgt = new Management();

            try
            {
                dbLayer = new DBClinicalEncounter();
                DataSet data = dbLayer.GetManagement(ptn_pk, visitPK, locationId);
                mgt.DropdownValues = new List<CodeDeCodeTables>();

                mgt.Ptn_pk = Convert.ToInt32(ptn_pk.ToString());
                mgt.LocationId = Convert.ToInt32(locationId.ToString());
                mgt.Visit_Id = Convert.ToInt32(visitPK.ToString());

                /* CTX Adherence , ARV Drugs Adhere */
                if (data.Tables[0].Rows.Count > 0)
                {
                    List<CodeDeCodeTables> lst = new List<CodeDeCodeTables>();

                    lst = (from dt in data.Tables[0].AsEnumerable()
                           select new CodeDeCodeTables()
                                            {
                                                DeCodeId = dt.Field<int>("ID"),
                                                DeCodeName = dt.Field<string>("NAME"),
                                                CodeName = "Adherence"
                                            }).ToList();
                    mgt.DropdownValues.AddRange(lst);
                }

                /* Why Fair or Poor  */
                if (data.Tables[1].Rows.Count > 0)
                {
                    List<CodeDeCodeTables> lst = new List<CodeDeCodeTables>();

                    lst = (from dt in data.Tables[1].AsEnumerable()
                           select new CodeDeCodeTables()
                           {
                               DeCodeId = dt.Field<int>("ID"),
                               DeCodeName = dt.Field<string>("NAME"),
                               CodeName = "FairPoor"
                           }).ToList();
                    mgt.DropdownValues.AddRange(lst);
                }

                /* Substitution/Interruption */
                if (data.Tables[2].Rows.Count > 0)
                {
                    List<CodeDeCodeTables> lst = new List<CodeDeCodeTables>();

                    lst = (from dt in data.Tables[2].AsEnumerable()
                           select new CodeDeCodeTables()
                           {
                               DeCodeId = dt.Field<int>("ID"),
                               DeCodeName = dt.Field<string>("NAME"),
                               CodeName = "Substitution"
                           }).ToList();
                    mgt.DropdownValues.AddRange(lst);
                }
                /* Substitution/Interruption */
                if (data.Tables[3].Rows.Count > 0)
                {
                    if (data.Tables[3].Rows[0]["ART"] != DBNull.Value)
                    {
                        mgt.ArtStartinMonth = Convert.ToDateTime(data.Tables[3].Rows[0]["ART"]).Year * 12 + Convert.ToDateTime(data.Tables[3].Rows[0]["ART"]).Month;
                        mgt.ARTStart = Convert.ToDateTime(data.Tables[3].Rows[0]["ART"]);
                    }
                    else
                    {
                        mgt.ArtStartinMonth = 0;
                        mgt.ARTStart = null;
                    }
                    if (data.Tables[3].Rows[0]["REGIMEN"] != DBNull.Value)
                    {
                        mgt.ArtRegimeninMonth = Convert.ToDateTime(data.Tables[3].Rows[0]["REGIMEN"]).Year * 12 + Convert.ToDateTime(data.Tables[3].Rows[0]["REGIMEN"]).Month;
                        mgt.RegimenStart = Convert.ToDateTime(data.Tables[3].Rows[0]["REGIMEN"]);
                    }
                    else
                    {
                        mgt.ArtRegimeninMonth = 0;
                        mgt.ARTStart = null;
                    }
                }

                /* Therapy Change Codes / Therapy Stop Codes */
                if (data.Tables[4].Rows.Count > 0)
                {
                    List<CodeDeCodeTables> lst = new List<CodeDeCodeTables>();

                    lst = (from dt in data.Tables[4].AsEnumerable()
                           select new CodeDeCodeTables()
                           {
                               DeCodeId = dt.Field<int>("ID"),
                               DeCodeName = dt.Field<string>("NAME"),
                               CodeId = dt.Field<int>("Codeid"),
                               CodeName = dt.Field<string>("CodeName")
                           }).ToList();
                    mgt.DropdownValues.AddRange(lst);
                }

                /* Prevention with positives (PwP) */
                if (data.Tables[5].Rows.Count > 0)
                {
                    List<CodeDeCodeTables> lst = new List<CodeDeCodeTables>();

                    lst = (from dt in data.Tables[5].AsEnumerable()
                           select new CodeDeCodeTables()
                           {
                               DeCodeId = dt.Field<int>("ID"),
                               DeCodeName = dt.Field<string>("NAME"),
                               CodeName = "PWP"
                           }).ToList();
                    mgt.DropdownValues.AddRange(lst);
                }

                /* ICD 10  */
                if (data.Tables[6].Rows.Count > 0)
                {
                    List<ICD> lst = new List<ICD>();

                    lst = (from dt in data.Tables[6].AsEnumerable()
                           select new ICD()
                           {
                               ChapterId = dt.Field<int>("ChapterID"),
                               ChapterName = dt.Field<string>("ChapterName"),
                               BlockId = dt.Field<int>("BlockId"),
                               BlockCode = dt.Field<string>("BlockCode"),
                               BlockName = dt.Field<string>("BlockName"),
                               SubBlockId = dt.Field<int>("SubBlockId"),
                               SubBlockCode = dt.Field<string>("SubBlockCode"),
                               SubBlockName = dt.Field<string>("SubBlockName"),
                               ICDId = dt.Field<int>("ICDId"),
                               ICDCode = dt.Field<string>("ICDCode"),
                               ICDName = dt.Field<string>("ICDName")

                           }).ToList();
                    mgt.ICDDropdownValues.AddRange(lst);
                }

                /* Purpose */
                if (data.Tables[7].Rows.Count > 0)
                {
                    List<CodeDeCodeTables> lst = new List<CodeDeCodeTables>();

                    lst = (from dt in data.Tables[7].AsEnumerable()
                           select new CodeDeCodeTables()
                           {
                               DeCodeId = dt.Field<int>("ID"),
                               DeCodeName = dt.Field<string>("NAME"),
                               CodeName = "PUR"
                           }).ToList();

                    mgt.Purpose.AddRange(lst);
                }

                /* ARVSideEffects */
                if (data.Tables[8].Rows.Count > 0)
                {
                    mgt.ARVSideEffects = 0;
                    if (string.IsNullOrEmpty(data.Tables[8].Rows[0]["ARVSideEffects"].ToString()) == false)
                    {
                        if (Convert.ToBoolean(data.Tables[8].Rows[0]["ARVSideEffects"].ToString()))
                        {
                            mgt.ARVSideEffects = 1;
                        }
                    }
                    //mgt.ARVSideEffects = string.IsNullOrEmpty(data.Tables[8].Rows[0]["ARVSideEffects"].ToString()) == false ? (int)data.Tables[8].Rows[0]["ARVSideEffects"].ToString() : 0;
                    mgt.AreaReminders = string.IsNullOrEmpty(data.Tables[8].Rows[0]["WorkUpPlan"].ToString()) == false ? data.Tables[8].Rows[0]["WorkUpPlan"].ToString() : string.Empty;
                }

                /* PatientAdherence */
                if (data.Tables[9].Rows.Count > 0)
                {
                    DataTable dtRow = new DataTable();
                    dtRow = data.Tables[9];
                    mgt.PatientAdherence = new MAdherence();

                    mgt.PatientAdherence.PAM_Id = Convert.ToInt32(dtRow.Rows[0]["PAM_Id"].ToString());
                    mgt.PatientAdherence.Ptn_pk = ptn_pk;
                    mgt.PatientAdherence.Visit_Id = visitPK;

                    mgt.PatientAdherence.IsForgotMed = string.IsNullOrEmpty(dtRow.Rows[0]["ForgetMedicineSinceLastVisit"].ToString()) == false ? Convert.ToInt32(dtRow.Rows[0]["ForgetMedicineSinceLastVisit"].ToString()) : 0;
                    mgt.PatientAdherence.IsCarelessMed = string.IsNullOrEmpty(dtRow.Rows[0]["CarelessAboutTakingMedicine"].ToString()) == false ? Convert.ToInt32(dtRow.Rows[0]["CarelessAboutTakingMedicine"].ToString()) : 0;
                    mgt.PatientAdherence.IsWorseTakingMed = string.IsNullOrEmpty(dtRow.Rows[0]["FeelWorseStopTakingMedicine"].ToString()) == false ? Convert.ToInt32(dtRow.Rows[0]["FeelWorseStopTakingMedicine"].ToString()) : 0;
                    mgt.PatientAdherence.IsFeelBetterMed = string.IsNullOrEmpty(dtRow.Rows[0]["FeelBetterStopTakingMedicine"].ToString()) == false ? Convert.ToInt32(dtRow.Rows[0]["FeelBetterStopTakingMedicine"].ToString()) : 0;
                    mgt.PatientAdherence.MMAS4Score = string.IsNullOrEmpty(dtRow.Rows[0]["MMAS4_Score"].ToString()) == false ? dtRow.Rows[0]["MMAS4_Score"].ToString() : "";
                    mgt.PatientAdherence.MMAS4Rating = string.IsNullOrEmpty(dtRow.Rows[0]["MMAS4_AdherenceRating"].ToString()) == false ? dtRow.Rows[0]["MMAS4_AdherenceRating"].ToString() : "";
                    mgt.PatientAdherence.IsYesterdayMed = string.IsNullOrEmpty(dtRow.Rows[0]["TakeMedicineYesterday"].ToString()) == false ? Convert.ToInt32(dtRow.Rows[0]["TakeMedicineYesterday"].ToString()) : 0;
                    mgt.PatientAdherence.IsSymptomUnderControl = string.IsNullOrEmpty(dtRow.Rows[0]["SymptomsUnderControl_StopTakingMedicine"].ToString()) == false ? Convert.ToInt32(dtRow.Rows[0]["SymptomsUnderControl_StopTakingMedicine"].ToString()) : 0;
                    mgt.PatientAdherence.IsStickingTreatmentPlan = string.IsNullOrEmpty(dtRow.Rows[0]["UnderPresureStickingYourTreatmentPlan"].ToString()) == false ? Convert.ToInt32(dtRow.Rows[0]["UnderPresureStickingYourTreatmentPlan"].ToString()) : 0;
                    mgt.PatientAdherence.RememberingMedications = string.IsNullOrEmpty(dtRow.Rows[0]["RememberingMedications"].ToString()) == false ? Convert.ToInt32(dtRow.Rows[0]["RememberingMedications"].ToString()) : 0;
                    mgt.PatientAdherence.MMAS8Score = string.IsNullOrEmpty(dtRow.Rows[0]["MMAS8_Score"].ToString()) == false ? dtRow.Rows[0]["MMAS8_Score"].ToString() : "";
                    mgt.PatientAdherence.MMAS8Rating = string.IsNullOrEmpty(dtRow.Rows[0]["MMAS8_AdherenceRating"].ToString()) == false ? dtRow.Rows[0]["MMAS8_AdherenceRating"].ToString() : "";
                    mgt.PatientAdherence.MMAS8Suggestion = string.IsNullOrEmpty(dtRow.Rows[0]["ReferToCounselor"].ToString()) == false ? dtRow.Rows[0]["ReferToCounselor"].ToString() : "";
                    mgt.PatientAdherence.MGTSIG = string.IsNullOrEmpty(dtRow.Rows[0]["Signature"].ToString()) == false ? Convert.ToInt32(dtRow.Rows[0]["Signature"].ToString()) : 0;
                }

                /* CTX and Adherence With WHYPOORFAIR */
                if (data.Tables[10].Rows.Count > 0)
                {
                    DataTable dtRow = new DataTable();
                    dtRow = data.Tables[10];

                    mgt.CTXWhyPoorFair = string.IsNullOrEmpty(dtRow.Rows[0]["CTXWhyPoorFair"].ToString()) == false ? Convert.ToInt32(dtRow.Rows[0]["CTXWhyPoorFair"].ToString()) : 0;
                    mgt.CTXAdherence = string.IsNullOrEmpty(dtRow.Rows[0]["CTXAdherence"].ToString()) == false ? Convert.ToInt32(dtRow.Rows[0]["CTXAdherence"].ToString()) : 0;
                    mgt.ARVAdherence = string.IsNullOrEmpty(dtRow.Rows[0]["ARVAdherence"].ToString()) == false ? Convert.ToInt32(dtRow.Rows[0]["ARVAdherence"].ToString()) : 0;
                    mgt.ARVWhyPoorFair = string.IsNullOrEmpty(dtRow.Rows[0]["ARVWhyPoorFair"].ToString()) == false ? Convert.ToInt32(dtRow.Rows[0]["ARVWhyPoorFair"].ToString()) : 0;
                }

                /* Substitutions/Interruptions details */
                if (data.Tables[11].Rows.Count > 0)
                {
                    DataTable dtRow = new DataTable();
                    dtRow = data.Tables[11];

                    mgt.TherapyPlan = string.IsNullOrEmpty(dtRow.Rows[0]["TherapyPlan"].ToString()) == false ? Convert.ToInt32(dtRow.Rows[0]["TherapyPlan"].ToString()) : 0;
                    mgt.TherapyReasonCode = string.IsNullOrEmpty(dtRow.Rows[0]["TherapyReasonCode"].ToString()) == false ? Convert.ToInt32(dtRow.Rows[0]["TherapyReasonCode"].ToString()) : 0;
                    mgt.ARTChangeReasonDiscussbyMDT = string.IsNullOrEmpty(dtRow.Rows[0]["ChangeRegimenReasondiscussbyMDT"].ToString()) == false ? Convert.ToInt32(dtRow.Rows[0]["ChangeRegimenReasondiscussbyMDT"].ToString()) : 0;
                    mgt.ARTChangeReasonDiscussbyMDTComments = dtRow.Rows[0]["ChangeRegimenReasondiscussbyMDTComments"].ToString();
                    mgt.ARVEndDate = string.IsNullOrEmpty(dtRow.Rows[0]["DateARTEnded"].ToString()) == false ? Convert.ToDateTime(dtRow.Rows[0]["DateARTEnded"].ToString()) : (DateTime?)null;
                    mgt.EligibleThrough = string.IsNullOrEmpty(dtRow.Rows[0]["EligibleThrough"].ToString()) == false ? Convert.ToInt32(dtRow.Rows[0]["EligibleThrough"].ToString()) : 0;
                }

                /* PwP Details */
                if (data.Tables[12].Rows.Count > 0)
                {
                    DataTable dtRow = new DataTable();
                    dtRow = data.Tables[12];

                    mgt.ScreenedForSTI = string.IsNullOrEmpty(dtRow.Rows[0]["ScreenedForSTI"].ToString()) == false ? Convert.ToInt32(dtRow.Rows[0]["ScreenedForSTI"].ToString()) : 0;
                    mgt.PartnerNotify = string.IsNullOrEmpty(dtRow.Rows[0]["PartnerNotify"].ToString()) == false ? Convert.ToInt32(dtRow.Rows[0]["PartnerNotify"].ToString()) : 0;
                }

                /* Key Population Details */
                if (data.Tables[13].Rows.Count > 0)
                {
                    //foreach (DataRow row in data.Tables[13].Rows)
                    //{
                    //    mgt.KeyPopulationList.Add(Convert.ToInt32(row["ID"].ToString()));
                    //}
                    mgt.KeyPopulationList = (from dt in data.Tables[13].AsEnumerable()
                                             select new DBMultiSelectSelcted()
                                             {
                                                 id = dt.Field<int>("ID")
                                             }).ToList();
                }

                /* PWP List Details */
                if (data.Tables[14].Rows.Count > 0)
                {
                    //foreach (DataRow row in data.Tables[14].Rows)
                    //{
                    //    mgt.PWPList.Add(Convert.ToInt32(row["ID"].ToString()));
                    //}

                    mgt.PWPList = (from dt in data.Tables[14].AsEnumerable()
                                   select new DBMultiSelectSelcted()
                                   {
                                       id = dt.Field<int>("ID")
                                   }).ToList();
                }

                /* ICd GRID Data */
                if (data.Tables[15].Rows.Count > 0)
                {
                    mgt.ICDGridValues = new List<ICD>();

                    foreach (DataRow row in data.Tables[15].Rows)
                    {
                        if ((from dt in data.Tables[6].AsEnumerable()
                             where dt.Field<int>("ICDId") == Convert.ToInt32(row["ICDCodeID"].ToString())
                             select dt).Count() > 0)
                        {
                            ICD obj = (from dt in data.Tables[6].AsEnumerable()
                                       where dt.Field<int>("ICDId") == Convert.ToInt32(row["ICDCodeID"].ToString())
                                       select new ICD()
                                       {
                                           ChapterId = dt.Field<int>("ChapterID"),
                                           ChapterName = dt.Field<string>("ChapterName"),
                                           BlockId = dt.Field<int>("BlockId"),
                                           BlockCode = dt.Field<string>("BlockCode"),
                                           BlockName = dt.Field<string>("BlockName"),
                                           SubBlockId = dt.Field<int>("SubBlockId"),
                                           SubBlockCode = dt.Field<string>("SubBlockCode"),
                                           SubBlockName = dt.Field<string>("SubBlockName"),
                                           ICDId = dt.Field<int>("ICDId"),
                                           ICDCode = dt.Field<string>("ICDCode"),
                                           ICDName = dt.Field<string>("ICDName"),
                                           //Comment = row["Comments"].ToString()

                                       }).FirstOrDefault();

                            mgt.ICDGridValues.Add(obj);
                        }
                    }

                    mgt.Comment = data.Tables[15].Rows[0]["Comments"].ToString();

                }

                /* Referrals Details */
                if (data.Tables[16].Rows.Count > 0)
                {
                    DataTable dtRow = new DataTable();
                    dtRow = data.Tables[16];

                    mgt.PatientRefDesc = string.IsNullOrEmpty(dtRow.Rows[0]["PatientRefDesc"].ToString()) == false ? dtRow.Rows[0]["PatientRefDesc"].ToString() : string.Empty;
                }


                /* Appointment Details */
                if (data.Tables[17].Rows.Count > 0)
                {
                    DataTable dtRow = new DataTable();
                    dtRow = data.Tables[17];

                    mgt.AppDate = string.IsNullOrEmpty(dtRow.Rows[0]["AppDate"].ToString()) == false ? Convert.ToDateTime(dtRow.Rows[0]["AppDate"].ToString()) : (DateTime?)null;
                    mgt.AppReason = string.IsNullOrEmpty(dtRow.Rows[0]["AppReason"].ToString()) == false ? dtRow.Rows[0]["AppReason"].ToString() : string.Empty;
                }

                /* ShortTermEffects /LongTermEffects */
                if (data.Tables[18].Rows.Count > 0)
                {
                    mgt.ShortTermEffects = (from dt in data.Tables[18].AsEnumerable().Where(o => o.Field<string>("FieldName") == "ShortTermEffects").ToList()
                                            select new DBMultiSelectSelcted()
                                            {
                                                id = dt.Field<int>("ValueID")
                                            }).ToList();

                    mgt.LongTermEffects = (from dt in data.Tables[18].AsEnumerable().Where(o => o.Field<string>("FieldName") == "LongTermEffects").ToList()
                                           select new DBMultiSelectSelcted()
                                           {
                                               id = dt.Field<int>("ValueID")
                                           }).ToList();
                }

                /* Regimen Codes */
                if (data.Tables[19].Rows.Count > 0)
                {
                    List<CodeDeCodeTables> lst = new List<CodeDeCodeTables>();

                    lst = (from dt in data.Tables[19].AsEnumerable()
                           select new CodeDeCodeTables()
                           {
                               DeCodeId = dt.Field<int>("RegimenID"),
                               DeCodeName = dt.Field<string>("RegimenValue"),
                               CodeName = "RegimenCodes"
                           }).ToList();
                    mgt.RegimenCodes.AddRange(lst);

                }

                /* RegimenID */
                if (data.Tables[20].Rows.Count > 0)
                {
                    DataTable dtRow = new DataTable();
                    dtRow = data.Tables[20];

                    mgt.RegimenCodeValue = string.IsNullOrEmpty(dtRow.Rows[0]["RegimenId"].ToString()) == false ? Convert.ToInt32(dtRow.Rows[0]["RegimenId"].ToString()) : 0;
                    mgt.RegimenVisitId = string.IsNullOrEmpty(dtRow.Rows[0]["Visit_Pk"].ToString()) == false ? Convert.ToInt32(dtRow.Rows[0]["Visit_Pk"].ToString()) : 0;
                }

                /* Patient Classification */
                if (data.Tables[21].Rows.Count > 0)
                {
                    List<CodeDeCodeTables> lst = new List<CodeDeCodeTables>();

                    lst = (from dt in data.Tables[21].AsEnumerable()
                           select new CodeDeCodeTables()
                           {
                               DeCodeId = dt.Field<int>("ID"),
                               DeCodeName = dt.Field<string>("NAME"),
                               CodeName = "Classification"
                           }).ToList();
                    mgt.PatientClassifications.AddRange(lst);

                }

                /* ART Refill Model */
                if (data.Tables[22].Rows.Count > 0)
                {
                    List<CodeDeCodeTables> lst = new List<CodeDeCodeTables>();

                    lst = (from dt in data.Tables[22].AsEnumerable()
                           select new CodeDeCodeTables()
                           {
                               DeCodeId = dt.Field<int>("ID"),
                               DeCodeName = dt.Field<string>("NAME"),
                               CodeName = "ARTRM"
                           }).ToList();
                    mgt.ARTRefillModels.AddRange(lst);

                }

                /* RegimenID */
                if (data.Tables[23].Rows.Count > 0)
                {
                    DataTable dtRow = new DataTable();
                    dtRow = data.Tables[23];

                    mgt.PatientClassification = string.IsNullOrEmpty(dtRow.Rows[0]["PatientClassification"].ToString()) == false ? Convert.ToInt32(dtRow.Rows[0]["PatientClassification"].ToString()) : 0;
                    mgt.IsEnrolDifferenciatedCare = string.IsNullOrEmpty(dtRow.Rows[0]["IsEnrolDifferenciatedCare"].ToString()) == false ? Convert.ToInt32(dtRow.Rows[0]["IsEnrolDifferenciatedCare"]) : 0;
                    mgt.ARTRefillModel = string.IsNullOrEmpty(dtRow.Rows[0]["ARTRefillModel"].ToString()) == false ? Convert.ToInt32(dtRow.Rows[0]["ARTRefillModel"].ToString()) : 0;
                }

                /* Appointments*/
                if (data.Tables[24].Rows.Count > 0)
                {

                    mgt.Appointments = new List<Appointment>();

                    int id = 1;
                    foreach (DataRow row in data.Tables[24].Rows)
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

                        mgt.Appointments.Add(app);
                        id++;
                    }

                }


            }
            catch (Exception ex)
            {
                CLogger.WriteLog(ELogLevel.ERROR, "BLClinicalEncounter.GetManagement() Method:" + ex.ToString());
            }
            return mgt;
        }

        public bool SaveUpdateTriage(Triage obj, int userId, out int visitId)
        {
            CLogger.WriteLog(ELogLevel.INFO, "BLClinicalEncounter.SaveUpdateTriage() method called");
            bool flag = true;
            visitId = 0;
            try
            {
                //HIVCE.Common.Entities.Triage obj = SerializerUtil.ConverToObject<HIVCE.Common.Entities.Triage>(strObj);

                if (obj.Ptn_pk != 0)
                {
                    dbLayer = new DBClinicalEncounter();
                    visitId = dbLayer.SaveUpdateTriage(obj, userId);
                    if (visitId > 0)
                        flag = true;
                    else
                        flag = false;
                }
                else
                {
                    throw new Exception("Patient id can not be 0");
                }
            }
            catch (Exception ex)
            {
                flag = false;
                CLogger.WriteLog(ELogLevel.ERROR, "BLClinicalEncounter.SaveUpdateTriage() Method:" + ex.ToString());
                throw ex;
            }
            return flag;
        }

        public bool SaveUpdateTSTriage(TSTriage obj, int userId, out int visitId)
        {
            CLogger.WriteLog(ELogLevel.INFO, "BLClinicalEncounter.SaveUpdateTSTriage() method called");
            bool flag = true;
            visitId = 0;
            try
            {
                //HIVCE.Common.Entities.Triage obj = SerializerUtil.ConverToObject<HIVCE.Common.Entities.Triage>(strObj);

                if (obj.Ptn_pk != 0)
                {
                    dbLayer = new DBClinicalEncounter();
                    visitId = dbLayer.SaveUpdateTSTriage(obj, userId);
                    if (visitId > 0)
                        flag = true;
                    else
                        flag = false;
                }
                else
                {
                    throw new Exception("Patient id can not be 0");
                }
            }
            catch (Exception ex)
            {
                flag = false;
                CLogger.WriteLog(ELogLevel.ERROR, "BLClinicalEncounter.SaveUpdateTriage() Method:" + ex.ToString());
                throw ex;
            }
            return flag;
        }


        public bool SaveUpdatePresentingComplaintsData(PresentingComplaint obj, int userId, int locationId)
        {
            CLogger.WriteLog(ELogLevel.INFO, "BLClinicalEncounter.SaveUpdatePresentingComplaintsData() method called");
            bool flag = true;
            try
            {
                //HIVCE.Common.Entities.PresentingComplaint obj = SerializerUtil.ConverToObject<HIVCE.Common.Entities.PresentingComplaint>(strObj);
                if (obj.Ptn_pk != 0)
                {
                    dbLayer = new DBClinicalEncounter();
                    dbLayer.SaveUpdatePresentingComplaints(obj, userId, locationId);
                }
                else
                {
                    throw new Exception("Patient id can not be 0");
                }
            }
            catch (Exception ex)
            {
                flag = false;
                CLogger.WriteLog(ELogLevel.ERROR, "BLClinicalEncounter.SaveUpdatePresentingComplaintsData() Method:" + ex.ToString());
            }
            return flag;
        }

        public bool SaveUpdateAdditionalHxData(DBAdditionalHx obj, int userId, int locationId)
        {
            CLogger.WriteLog(ELogLevel.INFO, "BLClinicalEncounter.SaveUpdateAdditionalHxData() method called");
            bool flag = true;
            try
            {
                if (obj.Ptn_Pk != 0)
                {
                    dbLayer = new DBClinicalEncounter();
                    dbLayer.SaveUpdateAdditionalHx(obj, userId, locationId);
                }
                else
                {
                    throw new Exception("Patient id can not be 0");
                }
            }
            catch (Exception ex)
            {
                flag = false;
                CLogger.WriteLog(ELogLevel.ERROR, "BLClinicalEncounter.SaveUpdateAdditionalHxData() Method:" + ex.ToString());
            }
            return flag;
        }

        public bool SaveUpdateScreeningData(DBScreeningData obj, int userId, int locationId)
        {
            CLogger.WriteLog(ELogLevel.INFO, "BLClinicalEncounter.SaveUpdateAdditionalHxData() method called");
            bool flag = true;
            try
            {
                if (obj.Ptn_Pk != 0)
                {
                    dbLayer = new DBClinicalEncounter();
                    dbLayer.SaveUpdateScreeningData(obj, userId, locationId);
                }
                else
                {
                    throw new Exception("Patient id can not be 0");
                }
            }
            catch (Exception ex)
            {
                flag = false;
                CLogger.WriteLog(ELogLevel.ERROR, "BLClinicalEncounter.SaveUpdateAdditionalHxData() Method:" + ex.ToString());
            }
            return flag;
        }

        public bool SaveUpdateSystemicReviewData(DBSystemicReviewData obj, int userId, int locationId)
        {
            CLogger.WriteLog(ELogLevel.INFO, "BLClinicalEncounter.SaveUpdateSystemicRevieData() method called");
            bool flag = true;
            try
            {
                if (obj.Ptn_Pk != 0)
                {
                    dbLayer = new DBClinicalEncounter();
                    dbLayer.SaveUpdateSystemicReviewData(obj, userId, locationId);
                }
                else
                {
                    throw new Exception("Patient id can not be 0");
                }
            }
            catch (Exception ex)
            {
                flag = false;
                CLogger.WriteLog(ELogLevel.ERROR, "BLClinicalEncounter.SaveUpdateSystemicRevieData() Method:" + ex.ToString());
            }
            return flag;
        }

        public bool SaveUpdateManagementxData(ManagementDB obj, int userId, int locationId)
        {
            CLogger.WriteLog(ELogLevel.INFO, "BLClinicalEncounter.SaveUpdateManagementxData() method called");
            bool flag = true;
            try
            {
                if (obj.Ptn_pk != 0)
                {
                    dbLayer = new DBClinicalEncounter();
                    dbLayer.SaveUpdateManagementxData(obj, userId, locationId);
                }
                else
                {
                    throw new Exception("Patient id can not be 0");
                }
            }
            catch (Exception ex)
            {
                flag = false;
                CLogger.WriteLog(ELogLevel.ERROR, "BLClinicalEncounter.SaveUpdateManagementxData() Method:" + ex.ToString());
            }
            return flag;
        }

        public RefillEncounter GetRefillEncounter(int ptn_pk, int visitPK, int locationId)
        {
            CLogger.WriteLog(ELogLevel.INFO, "BLClinicalEncounter.GetData() method called");
            RefillEncounter ce = new RefillEncounter();
            ce.VisitTypes = new List<CodeDeCodeTables>();
            ce.OrdVisit = new OrdVisit();
            ce.PatientVitals = new PatientVitals();
            //string result = string.Empty;

            try
            {
                dbLayer = new DBClinicalEncounter();
                DataSet data = dbLayer.GetRefillEncounter(ptn_pk, visitPK, locationId);

                /* Visit Types*/
                if (data.Tables[0].Rows.Count > 0)
                {
                    ce.VisitTypes = (from dt in data.Tables[0].AsEnumerable()
                                     select new CodeDeCodeTables()
                                     {
                                         CodeId = dt.Field<int>("codeid"),
                                         CodeName = dt.Field<string>("VisitType"),
                                         DeCodeId = dt.Field<int>("Id"),
                                         DeCodeName = dt.Field<string>("NAME")
                                     }).ToList();
                }


                /* Ord Visit */
                if (data.Tables[1].Rows.Count > 0)
                {
                    ce.OrdVisit = new OrdVisit();
                    foreach (DataRow row in data.Tables[1].Rows)
                    {
                        ce.OrdVisit.Visit_Id = Convert.ToInt32(row["Visit_Id"]);
                        ce.OrdVisit.Ptn_Pk = Convert.ToInt32(row["Ptn_Pk"]);
                        ce.OrdVisit.LocationId = Convert.ToInt32(row["LocationID"]);
                        ce.OrdVisit.VisitDate = string.IsNullOrEmpty(row["visitdate"].ToString()) == false ? Convert.ToDateTime(row["visitdate"].ToString()) : (DateTime?)null;
                        ce.OrdVisit.VisitTypeId = Convert.ToInt32(row["visittype"]);
                        ce.OrdVisit.CreatedDate = string.IsNullOrEmpty(row["CreateDate"].ToString()) == false ? Convert.ToDateTime(row["CreateDate"].ToString()) : (DateTime?)null;
                        ce.OrdVisit.UpdatedDate = string.IsNullOrEmpty(row["updatedate"].ToString()) == false ? Convert.ToDateTime(row["updatedate"].ToString()) : (DateTime?)null;
                        ce.OrdVisit.TypeOfVisit = Convert.ToInt32(row["TypeOfVisit"]);
                        ce.OrdVisit.Signature = Convert.ToInt32(row["Signature"]);
                    }
                }

                /* Patient Vitals */
                if (data.Tables[2].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = data.Tables[2];

                    ce.PatientVitals = new PatientVitals();

                    ce.PatientVitals.Visit_Id = Convert.ToInt32(dt.Rows[0]["visit_pk"].ToString());
                    ce.PatientVitals.BPDiastolic = dt.Rows[0]["BPDiastolic"].ToString();
                    ce.PatientVitals.BPSystolic = dt.Rows[0]["BPSystolic"].ToString();
                    ce.PatientVitals.TEMP = dt.Rows[0]["TEMP"].ToString();
                    ce.PatientVitals.RR = dt.Rows[0]["RR"].ToString();
                    ce.PatientVitals.HR = dt.Rows[0]["HR"].ToString();
                    ce.PatientVitals.HeadCircumference = dt.Rows[0]["Headcircumference"].ToString();
                    ce.PatientVitals.Height = dt.Rows[0]["height"].ToString();
                    ce.PatientVitals.Weight = dt.Rows[0]["weight"].ToString();
                    ce.PatientVitals.MUAC = dt.Rows[0]["MUAC"].ToString();
                    ce.PatientVitals.Weightforage = dt.Rows[0]["weightforage"].ToString();
                    ce.PatientVitals.Weightforheight = dt.Rows[0]["weightforheight"].ToString();
                    ce.PatientVitals.BMIz = dt.Rows[0]["BMIz"].ToString();
                    ce.PatientVitals.NurseComments = dt.Rows[0]["NurseComments"].ToString();


                }


                if (data.Tables[3].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = data.Tables[3];
                    ce.PatientVitals.SPO2 = dt.Rows[0]["SPO2"].ToString();
                }


                /* Regimen Codes */
                if (data.Tables[4].Rows.Count > 0)
                {
                    List<CodeDeCodeTables> lst = new List<CodeDeCodeTables>();

                    lst = (from dt in data.Tables[4].AsEnumerable()
                           select new CodeDeCodeTables()
                           {
                               DeCodeId = dt.Field<int>("RegimenID"),
                               DeCodeName = dt.Field<string>("RegimenValue"),
                               CodeName = "RegimenCodes"
                           }).ToList();
                    ce.RegimenCodes.AddRange(lst);

                }

                /* Presenting Complaints*/
                if (data.Tables[5].Rows.Count > 0)
                {
                    ce.PresentingComplaints = (from dt in data.Tables[5].AsEnumerable()
                                               select new CodeDeCodeTables()
                                               {
                                                   DeCodeId = dt.Field<int>("Id"),
                                                   DeCodeName = dt.Field<string>("NAME")
                                               }).ToList();
                }

                /* Pregnancy status */
                if (data.Tables[6].Rows.Count > 0)
                {
                    ce.PregnancyStatus = (from dt in data.Tables[6].AsEnumerable()
                                          select new CodeDeCodeTables()
                                          {
                                              DeCodeId = dt.Field<int>("Id"),
                                              DeCodeName = dt.Field<string>("NAME")
                                          }).ToList();
                }

                /* Family Planning Methods */
                if (data.Tables[7].Rows.Count > 0)
                {
                    ce.FamilyPlanningMethods = (from dt in data.Tables[7].AsEnumerable()
                                                select new CodeDeCodeTables()
                                                {
                                                    DeCodeId = dt.Field<int>("Id"),
                                                    DeCodeName = dt.Field<string>("NAME")
                                                }).ToList();
                }

                /* Reason Not on Family Planning */
                if (data.Tables[8].Rows.Count > 0)
                {
                    ce.ReasonNotFamilyPlanning = (from dt in data.Tables[8].AsEnumerable()
                                                  select new CodeDeCodeTables()
                                                  {
                                                      DeCodeId = dt.Field<int>("Id"),
                                                      DeCodeName = dt.Field<string>("NAME")
                                                  }).ToList();
                }

                /* Prevention with positives (PwP) */
                if (data.Tables[9].Rows.Count > 0)
                {
                    List<CodeDeCodeTables> lst = new List<CodeDeCodeTables>();

                    lst = (from dt in data.Tables[9].AsEnumerable()
                           select new CodeDeCodeTables()
                           {
                               DeCodeId = dt.Field<int>("ID"),
                               DeCodeName = dt.Field<string>("NAME"),
                               CodeName = "PWP"
                           }).ToList();
                    ce.DropdownValues.AddRange(lst);
                }

                /* Purpose */
                if (data.Tables[10].Rows.Count > 0)
                {
                    List<CodeDeCodeTables> lst = new List<CodeDeCodeTables>();

                    lst = (from dt in data.Tables[10].AsEnumerable()
                           select new CodeDeCodeTables()
                           {
                               DeCodeId = dt.Field<int>("ID"),
                               DeCodeName = dt.Field<string>("NAME"),
                               CodeName = "PUR"
                           }).ToList();

                    ce.Purpose.AddRange(lst);
                }

                /* Appointments*/
                if (data.Tables[11].Rows.Count > 0)
                {

                    ce.Appointments = new List<Appointment>();

                    int id = 1;
                    foreach (DataRow row in data.Tables[11].Rows)
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

                        ce.Appointments.Add(app);
                        id++;
                    }

                }

                /* RegimenID */
                if (data.Tables[12].Rows.Count > 0)
                {
                    DataTable dtRow = new DataTable();
                    dtRow = data.Tables[12];

                    ce.RegimenCodeValue = string.IsNullOrEmpty(dtRow.Rows[0]["RegimenId"].ToString()) == false ? Convert.ToInt32(dtRow.Rows[0]["RegimenId"].ToString()) : 0;
                    ce.RegimenVisitId = string.IsNullOrEmpty(dtRow.Rows[0]["Visit_Pk"].ToString()) == false ? Convert.ToInt32(dtRow.Rows[0]["Visit_Pk"].ToString()) : 0;
                }

                /* PatientAdherence */
                if (data.Tables[13].Rows.Count > 0)
                {
                    DataTable dtRow = new DataTable();
                    dtRow = data.Tables[13];
                    ce.IsForgotMed = string.IsNullOrEmpty(dtRow.Rows[0]["ForgetMedicineSinceLastVisit"].ToString()) == false ? Convert.ToInt32(dtRow.Rows[0]["ForgetMedicineSinceLastVisit"].ToString()) : 0;
                    ce.Drugs = string.IsNullOrEmpty(dtRow.Rows[0]["Drugs"].ToString()) == false ? dtRow.Rows[0]["Drugs"].ToString() : "";
                    ce.NoMissedDoses = string.IsNullOrEmpty(dtRow.Rows[0]["NoMissedDoses"].ToString()) == false ? Convert.ToInt32(dtRow.Rows[0]["NoMissedDoses"].ToString()) : 0;
                }

                DataTable dtData = new DataTable();
                //
                if (data.Tables[14].Rows.Count > 0)
                {
                    dtData = data.Tables[14];
                    if (dtData.Rows[0]["LMP"].ToString().Length > 0)
                    {
                        ce.LMP = Convert.ToDateTime(dtData.Rows[0]["LMP"].ToString());
                    }
                    if (dtData.Rows[0]["Pregnant"].ToString().Length > 0)
                    {
                        ce.Pregnant = Convert.ToInt32(dtData.Rows[0]["Pregnant"].ToString());
                    }

                }

                if (data.Tables[15].Rows.Count > 0)
                {
                    dtData = data.Tables[15];
                    ce.BreastStatus = Convert.ToInt32(dtData.Rows[0]["BreastStatus"]);
                }
                if (data.Tables[16].Rows.Count > 0)
                {
                    dtData = data.Tables[16];
                    if (dtData.Rows[0]["FamilyPlanningMethod"].ToString().Length > 0)
                    {
                        ce.FamilyPlanningMethod = Convert.ToInt32(dtData.Rows[0]["FamilyPlanningMethod"].ToString());
                    }
                    if (dtData.Rows[0]["FamilyPlanningStatus"].ToString().Length > 0)
                    {
                        ce.FamilyPlanningStatus = Convert.ToInt32(dtData.Rows[0]["FamilyPlanningStatus"].ToString());
                    }
                    if (dtData.Rows[0]["NoFamilyPlanning"].ToString().Length > 0)
                    {
                        ce.NoFamilyPlanning = Convert.ToInt32(dtData.Rows[0]["NoFamilyPlanning"].ToString());
                    }
                }

                if (data.Tables[17].Rows.Count > 0)
                {

                    DataView theCodeDV = new DataView(data.Tables[17]);
                    theCodeDV.RowFilter = "FieldName = 'presentingcomplaints'";
                    DataTable selectedTable = (DataTable)SerializerUtil.CreateTableFromDataView(theCodeDV);

                    if (selectedTable.Rows.Count > 0)
                    {
                        ce.PCT = (from dt in selectedTable.AsEnumerable()
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

                /* PWP List Details */
                if (data.Tables[18].Rows.Count > 0)
                {
                    ce.PWPList = (from dt in data.Tables[18].AsEnumerable()
                                  select new DBMultiSelectSelcted()
                                  {
                                      id = dt.Field<int>("ID")
                                  }).ToList();
                }

                /* ARVSideEffects */
                if (data.Tables[19].Rows.Count > 0)
                {
                    ce.AreaReminders = string.IsNullOrEmpty(data.Tables[19].Rows[0]["WorkUpPlan"].ToString()) == false ? data.Tables[19].Rows[0]["WorkUpPlan"].ToString() : string.Empty;
                    ce.OtherPresentingComplaints = data.Tables[19].Rows[0]["OtherPresentingComplaints"].ToString();
                }


            }
            catch (Exception ex)
            {
                CLogger.WriteLog(ELogLevel.ERROR, "BLClinicalEncounter.GetData() Method:" + ex.ToString());
            }
            return ce;
        }

        public bool SaveRefillEncounterData(RefillEncounterDB obj, int userId, int locationId)
        {
            CLogger.WriteLog(ELogLevel.INFO, "BLClinicalEncounter.SaveRefillEncounterData() method called");
            bool flag = true;
            try
            {
                if (obj.Ptn_pk != 0)
                {
                    dbLayer = new DBClinicalEncounter();
                    dbLayer.SaveRefillEncounterData(obj, userId, locationId);
                }
                else
                {
                    throw new Exception("Patient id can not be 0");
                }
            }
            catch (Exception ex)
            {
                flag = false;
                CLogger.WriteLog(ELogLevel.ERROR, "BLClinicalEncounter.SaveRefillEncounterData() Method:" + ex.ToString());
                throw ex;
            }
            return flag;
        }


        public OIChronicDiseaseData GetOIChronicDiseaseData(int ptn_pk, int locationId)
        {
            CLogger.WriteLog(ELogLevel.INFO, "BLClinicalEncounter.GetOIChronicDiseaseData() method called");
            OIChronicDiseaseData oiChronicDisease = new OIChronicDiseaseData();
            oiChronicDisease.CDT = new List<MultiSelectTable>();
            oiChronicDisease.WHOStages = new List<WHOStage>();
            oiChronicDisease.ICDGridValues = new List<ICD>();
            try
            {
                dbLayer = new DBClinicalEncounter();
                DataSet data = dbLayer.GetOIChronicDiseaseData(ptn_pk, locationId);
                DataTable dtData = new DataTable();
                if (data.Tables[0].Rows.Count > 0)
                {
                    oiChronicDisease.CDT = (from dt in data.Tables[0].AsEnumerable()
                                            select new MultiSelectTable()
                                            {
                                                Ptn_pk = dt.Field<int>("Ptn_pk"),
                                                Visit_Pk = dt.Field<int>("Visit_Pk"),
                                                ValueID = dt.Field<int>("ValueID"),
                                                FieldName = dt.Field<string>("FieldName"),
                                                OnSetDate = dt.Field<DateTime?>("OnSetDate"),
                                                NumericField = dt.Field<int>("NumericField"),
                                                ValueName = dt.Field<string>("ValueName")
                                            }).ToList();

                }

                if (data.Tables[1].Rows.Count > 0)
                {

                    oiChronicDisease.WHOStages = (from dt in data.Tables[1].AsEnumerable()
                                                  select new WHOStage()
                                                  {
                                                      ValueId = dt.Field<int>("ValueID"),
                                                      FieldName = dt.Field<string>("FieldName"),
                                                      DateField1 = dt.Field<DateTime?>("DateField1"),
                                                      DateField2 = dt.Field<DateTime?>("DateField2"),
                                                      ValueName = dt.Field<string>("ValueName")
                                                  }).ToList();
                }

                if (data.Tables[2].Rows.Count > 0)
                {
                    dtData = data.Tables[2];
                    if (dtData.Rows[0]["whostage"].ToString().Length > 0)
                    {
                        oiChronicDisease.WHOStage = string.IsNullOrEmpty(dtData.Rows[0]["whostage"].ToString()) == false ? Convert.ToInt32(dtData.Rows[0]["whostage"].ToString()) : 0;
                    }
                    if (dtData.Rows[0]["ValueName"].ToString().Length > 0)
                    {
                        oiChronicDisease.WHOStageName = dtData.Rows[0]["ValueName"].ToString();
                    }
                }

                /* ICd GRID Data */
                if (data.Tables[3].Rows.Count > 0)
                {
                    oiChronicDisease.ICDGridValues = (from dt in data.Tables[3].AsEnumerable()
                                                      select new ICD()
                                                      {
                                                          ChapterId = dt.Field<int>("ChapterID"),
                                                          ChapterName = dt.Field<string>("ChapterName"),
                                                          BlockId = dt.Field<int>("BlockId"),
                                                          BlockCode = dt.Field<string>("BlockCode"),
                                                          BlockName = dt.Field<string>("BlockName"),
                                                          SubBlockId = dt.Field<int>("SubBlockId"),
                                                          SubBlockCode = dt.Field<string>("SubBlockCode"),
                                                          SubBlockName = dt.Field<string>("SubBlockName"),
                                                          ICDId = dt.Field<int>("ICDId"),
                                                          ICDCode = dt.Field<string>("ICDCode"),
                                                          ICDName = dt.Field<string>("ICDName")
                                                      }).ToList();
                }
                /* ICd GRID Data */
                if (data.Tables[4].Rows.Count > 0)
                {
                    oiChronicDisease.Collections = (from dt in data.Tables[4].AsEnumerable()
                                                    select new CodeDeCodeTables()
                        {
                            CodeId = dt.Field<int>("CodeId"),
                            CodeName = dt.Field<string>("CodeName"),
                            DeCodeId = dt.Field<int>("DeCodeId"),
                            DeCodeName = dt.Field<string>("DeCodeName")
                        }).ToList();
                }
            }
            catch (Exception ex)
            {
                CLogger.WriteLog(ELogLevel.ERROR, "BLClinicalEncounter.GetOIChronicDiseaseData() Method:" + ex.ToString());
            }
            return oiChronicDisease;
        }
    }
}
