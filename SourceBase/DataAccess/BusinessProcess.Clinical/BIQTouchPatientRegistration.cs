using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Interface.Clinical;
using DataAccess.Base;
using DataAccess.Entity;
using DataAccess.Common;
using Application.Common;
using System.Collections.Generic;

namespace BusinessProcess.Clinical
{
    public class BIQTouchPatientRegistration : ProcessBase, IIQTouchPatientRegistration
    {
        private static Object thisLock = new Object();
        #region "Constructor"
        public BIQTouchPatientRegistration()
        {
        }
        #endregion

        

        public DataSet GetRegistrationDetails(int PatientID, string LocationID)
        {
            ClsUtility.Init_Hashtable();
            ClsUtility.AddParameters("@patientid", SqlDbType.Int, PatientID.ToString());
            ClsUtility.AddParameters("@locationid", SqlDbType.Int, LocationID);
            ClsUtility.AddParameters("@Password", SqlDbType.VarChar, ApplicationAccess.DBSecurity.Replace("'",""));
            ClsObject RecordMgr = new ClsObject();
            DataSet regDT = (DataSet)RecordMgr.ReturnObject(ClsUtility.theParams, "pr_Clinical_IQTouchGetRegistration", ClsDBUtility.ObjectEnum.DataSet);
            return regDT;

        }

        public bool CheckPatientFolderNo(string FolderNo)
        {
            DataTable dt = ReturnDatatableQuery("select D9FolderNo from mst_Patient where D9FolderNo = '" + FolderNo + "'");
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        public DataTable ReturnDatatableQuery(string theQuery)
        {
            lock (this)
            {
                ClsObject theQB = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                return (DataTable)theQB.ReturnObject(ClsUtility.theParams, "pr_General_SQLTable_Parse", ClsDBUtility.ObjectEnum.DataTable);
            }
        }

        public DataTable GetCareGiverInfo(string PatientID)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsUtility.Init_Hashtable();

                //Patient info Params
                ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ClsUtility.AddParameters("@ptn_pk", SqlDbType.VarChar, PatientID);
                ClsObject RegMan = new ClsObject();
                RegMan.Connection = this.Connection;
                RegMan.Transaction = this.Transaction;
                DataTable getCGREcs = (DataTable)RegMan.ReturnObject(ClsUtility.theParams, "Pr_IQTouch_CareGiver_Get", ClsDBUtility.ObjectEnum.DataTable);

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);

                return getCGREcs;
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

        public DataTable GetCareGiverInfoFromVisit(string PatientID)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsUtility.Init_Hashtable();

                //Patient info Params
                ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ClsUtility.AddParameters("@ptn_pk", SqlDbType.VarChar, PatientID);
                ClsObject RegMan = new ClsObject();
                RegMan.Connection = this.Connection;
                RegMan.Transaction = this.Transaction;
                DataTable getCGREcs = (DataTable)RegMan.ReturnObject(ClsUtility.theParams, "Pr_IQTouch_CareGiver_FromVisit_Get", ClsDBUtility.ObjectEnum.DataTable);

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);

                return getCGREcs;
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

        public int SavePatientRecord(string LocationID, string FirstName, string LastName, string Sex, string DOB, string MiddleName, string D9FolderNo)
        {
            lock (thisLock)
            {
                try
                {
                    this.Connection = DataMgr.GetConnection();
                    this.Transaction = DataMgr.BeginTransaction(this.Connection);

                    ClsUtility.Init_Hashtable();

                    //Patient info Params
                    ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                    ClsUtility.AddParameters("@FirstName", SqlDbType.VarChar, FirstName);
                    ClsUtility.AddParameters("@LastName", SqlDbType.VarChar, LastName);
                    ClsUtility.AddParameters("@Sex", SqlDbType.Int, Sex);
                    ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID);
                    ClsUtility.AddParameters("@DOB", SqlDbType.VarChar, DOB);
                    ClsUtility.AddParameters("@MiddleName", SqlDbType.VarChar, MiddleName);
                    ClsUtility.AddParameters("@D9FolderNo", SqlDbType.VarChar, D9FolderNo);
                    ParameterDirection[] pd = { ParameterDirection.Output };
                    ClsUtility.AddDirectionParameter("@idNEW", SqlDbType.Int, ParameterDirection.Output);

                    ClsObject RegMan = new ClsObject();
                    RegMan.Connection = this.Connection;
                    RegMan.Transaction = this.Transaction;
                    int NewPatientID = (int)RegMan.ReturnObject(ClsUtility.theParams, "Pr_IQTouch_Patient_Add", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                    DataMgr.CommitTransaction(this.Transaction);
                    DataMgr.ReleaseConnection(this.Connection);

                    return NewPatientID;
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
        }

        public int SaveRegistrationDetails(objRegistration theReg, string PatientID, string LocationID, string UserID)
        {
            lock (this)
            {
                try
                {
                    //DataSet theDS;
                    this.Connection = DataMgr.GetConnection();
                    this.Transaction = DataMgr.BeginTransaction(this.Connection);

                    ClsUtility.Init_Hashtable();

                    //Patient Info params
                    ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID);
                    ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID);
                    ClsUtility.AddParameters("@FirstName", SqlDbType.VarChar, theReg.FirstName);
                    ClsUtility.AddParameters("@LastName", SqlDbType.VarChar, theReg.LastName);
                    ClsUtility.AddParameters("@RegistrationDate", SqlDbType.VarChar, theReg.RegistrationDate);
                    ClsUtility.AddParameters("@Sex", SqlDbType.Int, theReg.Sex.ToString());
                    ClsUtility.AddParameters("@DOB", SqlDbType.VarChar, theReg.DOB);
                    ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                    ClsUtility.AddParameters("@Address", SqlDbType.VarChar, theReg.Address);
                    ClsUtility.AddParameters("@Village", SqlDbType.VarChar, theReg.Suburb);
                    ClsUtility.AddParameters("@Town", SqlDbType.Int, theReg.SubDistrict);
                    ClsUtility.AddParameters("@DistrictName", SqlDbType.Int, theReg.District);
                    ClsUtility.AddParameters("@Phone", SqlDbType.VarChar, theReg.TelephoneNo);
                    ClsUtility.AddParameters("@ReferredFrom", SqlDbType.Int, theReg.EntryPoint.Split('|')[0]);
                    ClsUtility.AddParameters("@ReferredFromSpecify", SqlDbType.VarChar, theReg.OtherEntryPoint);

                    //Contact detail params
                    ClsUtility.AddParameters("@AddressComment", SqlDbType.VarChar, theReg.Addresscomments);
                    ClsUtility.AddParameters("@OtherAddress", SqlDbType.VarChar, theReg.PostalAddress);
                    ClsUtility.AddParameters("@OtherPOBoxNo", SqlDbType.VarChar, theReg.PostalCode);
                    ClsUtility.AddParameters("@GuardianName", SqlDbType.VarChar, theReg.CareGiverName);
                    ClsUtility.AddParameters("@Guardian_DOB", SqlDbType.VarChar, theReg.CareGiverDOB);
                    ClsUtility.AddParameters("@GuardianGender", SqlDbType.Int, theReg.CareGiverGender.ToString());
                    ClsUtility.AddParameters("@EmergContactRelation", SqlDbType.VarChar, theReg.CareGiverRelationship.ToString());
                    ClsUtility.AddParameters("@EmergContactRelationOther", SqlDbType.VarChar, theReg.OtherCareGiver);
                    ClsUtility.AddParameters("@EmergContactPhone", SqlDbType.VarChar, theReg.CareGiverTelephone);

                    //Mother's History params
                    ClsUtility.AddParameters("@MotherName", SqlDbType.VarChar, theReg.MotherName);

                    if (theReg.MotherAliveYN.ToString() != "")
                    {
                        ClsUtility.AddParameters("@MotherAliveYN", SqlDbType.Int, Convert.ToInt32(bool.Parse(theReg.MotherAliveYN.ToString().ToLower())).ToString());
                    }
                    ClsUtility.AddParameters("@MotherPMTCTdrugsYN", SqlDbType.Int, theReg.MotherPMTCTdrugsYN.ToString());
                    string MotherPMTCTDrugList = string.Empty;
                    foreach (var item in theReg.MotherPMTCTdrugs)
                    {
                        if (MotherPMTCTDrugList.Length > 0)
                            MotherPMTCTDrugList += "|";

                        MotherPMTCTDrugList += item.ToString();
                    }
                    ClsUtility.AddParameters("@MotherPMTCTdrugs", SqlDbType.VarChar, MotherPMTCTDrugList);
                    ClsUtility.AddParameters("@ChildPMTCTdrugsYN", SqlDbType.Int, theReg.ChildPMTCTdrugsYN.ToString());
                    string ChildPMTCTDrugList = string.Empty;
                    foreach (var item in theReg.ChildPMTCTdrugs)
                    {
                        if (ChildPMTCTDrugList.Length > 0)
                            ChildPMTCTDrugList += "|";

                        ChildPMTCTDrugList += item.ToString();
                    }
                    ClsUtility.AddParameters("@ChildPMTCTdrugs", SqlDbType.VarChar, ChildPMTCTDrugList);
                    ClsUtility.AddParameters("@MotherARTYN", SqlDbType.Int, theReg.MotherARTYN.ToString());
                    ClsUtility.AddParameters("@FeedingOption", SqlDbType.Int, theReg.FeedingOption.ToString());

                    //HIV Care params
                    ClsUtility.AddParameters("@HIVPosDateConfirmed", SqlDbType.VarChar, theReg.DateConfirmedHIVPositive);
                    ClsUtility.AddParameters("@HIVCareDateEnrolled", SqlDbType.VarChar, theReg.DateEnrolledHIVCare);
                    ClsUtility.AddParameters("@WHOStageAtEnrollment", SqlDbType.Int, theReg.WHOStageAtEnrollment.ToString());

                    //Transfer In params
                    ClsUtility.AddParameters("@TransferInDate", SqlDbType.VarChar, theReg.TransferInDate);
                    ClsUtility.AddParameters("@TransferFromDistrict", SqlDbType.Int, theReg.FromDistrict.ToString());
                    ClsUtility.AddParameters("@TransferFromFacility", SqlDbType.Int, theReg.Facility.ToString());
                    ClsUtility.AddParameters("@DateStartedFirstRegimen", SqlDbType.VarChar, theReg.DateStart);
                    string Regimens = string.Empty;
                    foreach (var item in theReg.Regimen)
                    {
                        if (Regimens.Length > 0)
                            Regimens += "|";

                        Regimens += item.ToString();
                    }
                    ClsUtility.AddParameters("@Regimen", SqlDbType.VarChar, theReg.RegimenAbbreviations);
                    ClsUtility.AddParameters("@RegimenIDs", SqlDbType.VarChar, Regimens);

                    ClsUtility.AddParameters("@Weight", SqlDbType.Decimal, theReg.Weight);
                    ClsUtility.AddParameters("@Height", SqlDbType.Decimal, theReg.Height);
                    ClsUtility.AddParameters("@Whostage", SqlDbType.Int, theReg.WHOStageAtTransfer.ToString());

                    ClsUtility.AddParameters("@PriorART", SqlDbType.VarChar, theReg.PriorART);
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());

                    ClsObject RegMan = new ClsObject();
                    RegMan.Connection = this.Connection;
                    RegMan.Transaction = this.Transaction;
                    int RecsAffected = (int)RegMan.ReturnObject(ClsUtility.theParams, "pr_Clinical_IQTouchSaveRegistration", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                    if (theReg.PriorART == "1")
                    {
                        int Purpose = 0;
                        foreach (var item in theReg.PriorARTRegimens)
                        {
                            Purpose++;
                            //Then Handle the regimens
                            ClsUtility.Init_Hashtable();
                            ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID);
                            ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID);
                            ClsUtility.AddParameters("@RegimenID", SqlDbType.Int, item.RegimenID.ToString());
                            ClsUtility.AddParameters("@Regimen", SqlDbType.VarChar, item.Regimen.ToString());
                            ClsUtility.AddParameters("@RegimenDate", SqlDbType.VarChar, item.PriorARTDateLastUsed);
                            ClsUtility.AddParameters("@PurposeID", SqlDbType.Int, Purpose.ToString());
                            ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                            ClsObject RegManPrior = new ClsObject();
                            RegManPrior.Connection = this.Connection;
                            RegManPrior.Transaction = this.Transaction;
                            int recsinserted = (int)RegManPrior.ReturnObject(ClsUtility.theParams, "pr_Clinical_IQTouchSaveRegistration_PriorART", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                        }
                    }

                    if (theReg.DrugAllergies.Count > 0)
                    {
                        foreach (var item in theReg.DrugAllergies)
                        {
                            ClsUtility.Init_Hashtable();
                            ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID);
                            ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID);
                            ClsUtility.AddParameters("@AllergenID", SqlDbType.Int, item.AllergenID.ToString());
                            ClsUtility.AddParameters("@AllergenName", SqlDbType.VarChar, item.Allergen);
                            ClsUtility.AddParameters("@ReactionType", SqlDbType.VarChar, item.TypeOfReaction);
                            ClsUtility.AddParameters("@AllergyDate", SqlDbType.VarChar, item.DateOfAllergy);
                            ClsUtility.AddParameters("@MedicalConditions", SqlDbType.VarChar, item.MedicalConditions);
                            ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                            ClsObject RegManDrugAllergy = new ClsObject();
                            RegManDrugAllergy.Connection = this.Connection;
                            RegManDrugAllergy.Transaction = this.Transaction;
                            int DrugsInserted = (int)RegManDrugAllergy.ReturnObject(ClsUtility.theParams, "pr_Clinical_IQTouchSaveRegistration_DrugAllergy", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                        }
                    }

                    DataMgr.CommitTransaction(this.Transaction);
                    DataMgr.ReleaseConnection(this.Connection);

                    return RecsAffected;
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
        }


        public DataTable GetPatientRecord(int PatientID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientid", SqlDbType.Int, PatientID.ToString());
                ClsUtility.AddParameters("@Password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ClsObject RecordMgr = new ClsObject();
                return (DataTable)RecordMgr.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetPatientRecord_Futures", ClsDBUtility.ObjectEnum.DataTable);
            }

        }
        public DataTable theVisitIDDT(string patientid)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientid", SqlDbType.VarChar, patientid.ToString());
                ClsObject VisitIDMgr = new ClsObject();
                return (DataTable)VisitIDMgr.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetVisitIDEnrolment_Constella", ClsDBUtility.ObjectEnum.DataTable);
            }
        }
        public DataTable CheckDuplicateIdentifiervaule(string Columnname, string Columnvalue)
        {
            try
            {
                lock (this)
                {
                    ClsObject PatientHistory = new ClsObject();
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@Columnname", SqlDbType.Int, Columnname.ToString());
                    ClsUtility.AddParameters("@Columnvalue", SqlDbType.Int, Columnvalue.ToString());
                    return (DataTable)PatientHistory.ReturnObject(ClsUtility.theParams, "pr_Clinical_CheckDuplicateIdentifiervaule_Future", ClsDBUtility.ObjectEnum.DataTable);
                }

            }

            catch (Exception ex)
            {
                return null;
            }


        }
        public DataSet GetVisitDate_IELAB(int patientid, int LocationID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientid.ToString());
                ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                ClsObject VisitManager = new ClsObject();
                return (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetIELAB_VisitDate_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
        
        public DataSet GetAllDropDowns()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject PatientManager = new ClsObject();
                return (DataSet)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Admin_GetPatientEnrollmentDropDowns_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetAge(DateTime dob, DateTime regdate)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@dob", SqlDbType.DateTime, dob.ToString());
                ClsUtility.AddParameters("@regdate", SqlDbType.DateTime, regdate.ToString());
                ClsObject PatientManager = new ClsObject();
                return (DataSet)PatientManager.ReturnObject(ClsUtility.theParams, "pr_GetDataDiff", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetEnrolment(string CountryID, string PossitionID, string SatelliteID, string PatientClinicID, string enrolmentid, int deleteflag)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Countryid", SqlDbType.Int, CountryID.ToString());
                ClsUtility.AddParameters("@Posid", SqlDbType.Int, PossitionID.ToString());
                ClsUtility.AddParameters("@Satelliteid", SqlDbType.Int, SatelliteID.ToString());
                ClsUtility.AddParameters("@PatientClinicID", SqlDbType.VarChar, PatientClinicID.ToString());
                ClsUtility.AddParameters("@enrolmentid", SqlDbType.VarChar, enrolmentid.ToString());
                ClsUtility.AddParameters("@deleteflag", SqlDbType.Int, deleteflag.ToString());
                ClsObject PatientManager = new ClsObject();
                return (DataSet)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SelectEnrollment", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetPatientEnroll(string patientid, int VisitID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientid.ToString());
                ClsUtility.AddParameters("@visitID", SqlDbType.Int, VisitID.ToString());
                ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "Pr_Clinical_GetEnrollment_COnstella", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetPatientRegistration(int patientid, int VisitType)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientid.ToString());
                ClsUtility.AddParameters("@VisitType", SqlDbType.Int, VisitType.ToString());
                ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetPatientRegistration_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetPatientTechnicalAreaDetails(int patientid, string ModuleName, int ModuleID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientid.ToString());
                ClsUtility.AddParameters("@Modulename", SqlDbType.VarChar, ModuleName.ToString().TrimEnd());
                ClsUtility.AddParameters("@ModuleID", SqlDbType.Int, ModuleID.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "Pr_Clinical_GetPatientTechnicalAreaDetails_COnstella", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetModuleNames(int FacilityID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@facilityid", SqlDbType.Int, FacilityID.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectModulesByFacilityID_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetPatientSearchResults(int FId, string lastname, string middlename, string firstname, string enrollmentType, string enrollment, string gender, string dob, string status,int ModuleId,string FolderNo)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@FacilityId", SqlDbType.Int, FId.ToString());
                ClsUtility.AddParameters("@lastname", SqlDbType.VarChar, lastname.ToString());
                ClsUtility.AddParameters("@middlename", SqlDbType.VarChar, middlename.ToString());
                ClsUtility.AddParameters("@firstname", SqlDbType.VarChar, firstname.ToString());
                ClsUtility.AddParameters("@enrollmentType", SqlDbType.VarChar, enrollmentType.ToString());
                ClsUtility.AddParameters("@enrollmentid", SqlDbType.VarChar, enrollment.ToString());
                //ClsUtility.AddParameters("@hospitalno", SqlDbType.VarChar, hospitalno.ToString());
                ClsUtility.AddParameters("@gender", SqlDbType.VarChar, gender.ToString());
                //ClsUtility.AddParameters("@dobexact", SqlDbType.Int, dobexact.ToString());
                //ClsUtility.AddParameters("@dobestimate", SqlDbType.Int, dobestimate.ToString());
                ClsUtility.AddParameters("@dob", SqlDbType.VarChar, dob.ToString());
                ClsUtility.AddParameters("@status", SqlDbType.VarChar, status.ToString());
                ClsUtility.AddParameters("@ModuleId", SqlDbType.VarChar, ModuleId.ToString());
                ClsUtility.AddParameters("@FolderNo", SqlDbType.VarChar, FolderNo.ToString());
                ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "Pr_Clinical_GetPatientSearchresults_COnstella", ClsDBUtility.ObjectEnum.DataSet);
                //return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "Pr_Clinical_GetPatientSearchresults_Naveen", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetDuplicatePatientSearchResults(string lastname, string middlename, string firstname, string address, string phone)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@lastname", SqlDbType.VarChar, lastname.ToString());
                ClsUtility.AddParameters("@middlename", SqlDbType.VarChar, middlename.ToString());
                ClsUtility.AddParameters("@firstname", SqlDbType.VarChar, firstname.ToString());
                ClsUtility.AddParameters("@Address", SqlDbType.VarChar, address.ToString());
                ClsUtility.AddParameters("@Phone", SqlDbType.VarChar, phone.ToString());
                ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "Pr_Clinical_GetDuplicatePatientSearchresults_COnstella", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        #region "AidsRelief"
        public DataTable SaveNewRegistration(Hashtable ht, DataTable dtCaretype, DataTable dtARTsponsor, DataTable dt, DataTable dtBarrier, int dataquality, DataTable theCustomFieldData)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject PatientManager = new ClsObject();
                PatientManager.Connection = this.Connection;
                PatientManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientID", SqlDbType.VarChar, ht["PatientID"].ToString());
                ClsUtility.AddParameters("@FirstName", SqlDbType.VarChar, ht["FirstName"].ToString());
                ClsUtility.AddParameters("@LastName", SqlDbType.VarChar, ht["LastName"].ToString());
                ClsUtility.AddParameters("@LocationID", SqlDbType.Int, ht["LocationID"].ToString());
                ClsUtility.AddParameters("@SatelliteID", SqlDbType.VarChar, ht["SatelliteID"].ToString());
                ClsUtility.AddParameters("@CountryID", SqlDbType.VarChar, ht["CountryID"].ToString());
                ClsUtility.AddParameters("@PosID", SqlDbType.VarChar, ht["PosID"].ToString());
                ClsUtility.AddParameters("@PatientEnrollmentID", SqlDbType.VarChar, ht["PatientEnrollmentID"].ToString());
                ClsUtility.AddParameters("@PatientClinicID", SqlDbType.VarChar, ht["HospitalID"].ToString());
                ClsUtility.AddParameters("@ReferredFrom", SqlDbType.VarChar, ht["ReferredFrom"].ToString());
                ClsUtility.AddParameters("@ReferredFromSpecify", SqlDbType.VarChar, ht["ReferredFromSpecify"].ToString());
                ClsUtility.AddParameters("@RegistrationDate", SqlDbType.VarChar, ht["RegistrationDate"].ToString());
                ClsUtility.AddParameters("@Sex", SqlDbType.VarChar, ht["Gender"].ToString());
                ClsUtility.AddParameters("@dob", SqlDbType.VarChar, ht["DOB"].ToString());
                ClsUtility.AddParameters("@DobPrecision", SqlDbType.VarChar, ht["DOBPrecision"].ToString());
                ClsUtility.AddParameters("@TransferIn", SqlDbType.VarChar, ht["Transferin"].ToString());
                ClsUtility.AddParameters("@LPTFTransferFrom", SqlDbType.VarChar, ht["LPTFTransferfrom"].ToString());
                ClsUtility.AddParameters("@LocalCouncil", SqlDbType.VarChar, ht["LocalCouncil"].ToString());
                ClsUtility.AddParameters("@VillageName", SqlDbType.VarChar, ht["VillageName"].ToString());
                ClsUtility.AddParameters("@DistrictName", SqlDbType.VarChar, ht["DistrictName"].ToString());
                ClsUtility.AddParameters("@Province", SqlDbType.VarChar, ht["Province"].ToString());
                ClsUtility.AddParameters("@Address", SqlDbType.VarChar, ht["Address"].ToString());
                ClsUtility.AddParameters("@Phone", SqlDbType.VarChar, ht["Phone"].ToString());
                ClsUtility.AddParameters("@MaritalStatus", SqlDbType.VarChar, ht["MaritalStatus"].ToString());
                ClsUtility.AddParameters("@EducationLevel", SqlDbType.VarChar, ht["EducationLevel"].ToString());
                ClsUtility.AddParameters("@Literacy", SqlDbType.VarChar, ht["Literacy"].ToString());
                ClsUtility.AddParameters("@EmployeeID", SqlDbType.VarChar, ht["Interviewer"].ToString());
                ClsUtility.AddParameters("@Status", SqlDbType.VarChar, "0");
                ClsUtility.AddParameters("@StatusChangedDate", SqlDbType.VarChar, ht["HIVStatusChangedDate"].ToString());
                ClsUtility.AddParameters("@DataQuality", SqlDbType.Int, dataquality.ToString());
                ClsUtility.AddParameters("@GuardianName", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@GuardianInformation", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@EmergContactName", SqlDbType.VarChar, ht["EmergencyContactName"].ToString());
                ClsUtility.AddParameters("@EmergContactRelation", SqlDbType.VarChar, ht["EmergencyContactRelation"].ToString());
                ClsUtility.AddParameters("@EmergContactPhone", SqlDbType.VarChar, ht["EmergencyContactPhone"].ToString());
                ClsUtility.AddParameters("@EmergContactAddress", SqlDbType.VarChar, ht["EmergencyContactAddress"].ToString());
                ClsUtility.AddParameters("@EmergContactKnowsHIVStatus", SqlDbType.VarChar, ht["KnowHIVStatus"].ToString());
                ClsUtility.AddParameters("@DiscussStatus", SqlDbType.VarChar, ht["DiscussStatus"].ToString());
                ClsUtility.AddParameters("@PrevHIVCare", SqlDbType.VarChar, ht["PrevHIVCare"].ToString());
                ClsUtility.AddParameters("@PrevMedRecords", SqlDbType.VarChar, ht["PrevMedRecords"].ToString());
                ClsUtility.AddParameters("@PrevCareHomeBased", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@PrevCareVCT", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@PrevCareSTI", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@PrevCarePMTCT", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@PrevCareInPatient", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@PrevCareOther", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@PrevCareOtherSpecify", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@PrevART", SqlDbType.VarChar, ht["ArtSponsor"].ToString());
                ClsUtility.AddParameters("@PrevARTSSelfFinanced", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@PrevARTSGovtSponsored", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@PrevARTSUSGSponsered", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@PrevARTSMissionBased", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@PrevARTSThisFacility", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@PrevARTSOthers", SqlDbType.Int, "");
                ClsUtility.AddParameters("@PrevARTSOtherSpecs", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@EmploymentStatus", SqlDbType.VarChar, ht["EmploymentStatus"].ToString());
                ClsUtility.AddParameters("@Occupation", SqlDbType.VarChar, ht["Occupation"].ToString());
                ClsUtility.AddParameters("@MonthlyIncome", SqlDbType.VarChar, ht["MonthlyIncome"].ToString());
                ClsUtility.AddParameters("@NumChildren", SqlDbType.VarChar, ht["NumChildren"].ToString());
                ClsUtility.AddParameters("@NumPeopleHousehold", SqlDbType.VarChar, ht["NumPeopleHousehold"].ToString());
                ClsUtility.AddParameters("@DistanceTravelled", SqlDbType.VarChar, ht["DistanceTravelled"].ToString());
                ClsUtility.AddParameters("@TimeTravelled", SqlDbType.VarChar, ht["TimeTravelled"].ToString());
                ClsUtility.AddParameters("@TravelledUnits", SqlDbType.VarChar, ht["TimeTravelledUnits"].ToString());
                ClsUtility.AddParameters("@HIVStatus", SqlDbType.VarChar, ht["HIVStatus"].ToString());
                ClsUtility.AddParameters("@HIVStatus_Child", SqlDbType.VarChar, ht["KnowHIVChildStatus"].ToString());
                ClsUtility.AddParameters("@HIVDisclosure", SqlDbType.VarChar, ht["HIVDisclosure"].ToString());
                ClsUtility.AddParameters("@HIVDisclosureOther", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@NumHouseholdHIVTest", SqlDbType.VarChar, ht["NumHouseholdHIVTest"].ToString());
                ClsUtility.AddParameters("@NumHouseholdHIVPositive", SqlDbType.VarChar, ht["NumHouseholdHIVPositive"].ToString());
                ClsUtility.AddParameters("@NumHouseholdHIVDied", SqlDbType.VarChar, ht["NumHouseholdHIVDied"].ToString());
                ClsUtility.AddParameters("@SupportGroup", SqlDbType.VarChar, ht["SupportGroup"].ToString());
                ClsUtility.AddParameters("@SupportGroupName", SqlDbType.VarChar, ht["SupportGroupName"].ToString());
                ClsUtility.AddParameters("@ReferredFromVCT", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@ReferredFromOutpatient", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@ReferredFromOtherSource", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@ReferredFromPMTCT", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@ReferredFromTBOutpatient", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@ReferredFromInPatientWard", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@ReferredFromOtherFacility", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                ClsUtility.AddParameters("@Password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);

                DataTable dtp = (DataTable)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveEnrollment_Constella", ClsDBUtility.ObjectEnum.DataTable);

                //returning CareType;
                //////DataMgr.CommitTransaction(this.Transaction);
                //////DataMgr.ReleaseConnection(this.Connection);

                ////////returning CareType;
                //////this.Connection = DataMgr.GetConnection();
                //////this.Transaction = DataMgr.BeginTransaction(this.Connection);

                //ClsObject CareManager = new ClsObject();
                int intflag = 0;
                for (int i = 0; i < dtCaretype.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@patientid", SqlDbType.Int, dtp.Rows[0][0].ToString());
                    ClsUtility.AddParameters("@visitpk", SqlDbType.Int, dtp.Rows[0][1].ToString());
                    ClsUtility.AddParameters("@locationid", SqlDbType.Int, ht["LocationID"].ToString());
                    ClsUtility.AddParameters("@HIVAIDsCareID", SqlDbType.Int, dtCaretype.Rows[i]["HIVCareTypeID"].ToString());
                    ClsUtility.AddParameters("@HIVAIDsCareDesc", SqlDbType.VarChar, dtCaretype.Rows[i]["HIVCareTypeOther"].ToString());
                    ClsUtility.AddParameters("@userID", SqlDbType.Int, ht["UserID"].ToString());

                    if (intflag == 0)
                    {
                        int retval = (Int32)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveHIVAIDsCareType_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                    else if (intflag == 1)
                    {
                        int retval = (Int32)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveHIVAIDsCareType_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                //ClsObject ARTSponsorMgr = new ClsObject();
                for (int i = 0; i < dtARTsponsor.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@patientid", SqlDbType.Int, dtp.Rows[0][0].ToString());
                    ClsUtility.AddParameters("@visitpk", SqlDbType.Int, dtp.Rows[0][1].ToString());
                    ClsUtility.AddParameters("@locationid", SqlDbType.Int, ht["LocationID"].ToString());
                    ClsUtility.AddParameters("@ARTSponsorID", SqlDbType.Int, dtARTsponsor.Rows[i]["ARTsponsorID"].ToString());
                    ClsUtility.AddParameters("@ARTSponsorDesc", SqlDbType.VarChar, dtARTsponsor.Rows[i]["ARTSponsorOther"].ToString());
                    ClsUtility.AddParameters("@userID", SqlDbType.Int, ht["UserID"].ToString());

                    if (intflag == 0)
                    {
                        int retval = (Int32)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveARTSponsor_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                    else if (intflag == 1)
                    {
                        int retval = (Int32)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveARTSponsor_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                //HIV Disclosure Section
                //ClsObject DiscloseManager = new ClsObject();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@patientid", SqlDbType.Int, dtp.Rows[0][0].ToString());
                    ClsUtility.AddParameters("@visitpk", SqlDbType.Int, dtp.Rows[0][1].ToString());
                    ClsUtility.AddParameters("@locationid", SqlDbType.Int, ht["LocationID"].ToString());
                    ClsUtility.AddParameters("@disclosureid", SqlDbType.Int, dt.Rows[i]["DisclosureID"].ToString());
                    ClsUtility.AddParameters("@HIVDisclosureOther", SqlDbType.VarChar, dt.Rows[i]["DisclosureOther"].ToString());


                    if (intflag == 0)
                    {
                        int retval = (Int32)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveDiscloseEnrol_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                    else if (intflag == 1)
                    {
                        int retval = (Int32)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateDiscloseEnrol_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                //ClsObject BarrierManager = new ClsObject();
                for (int i = 0; i < dtBarrier.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@patientid", SqlDbType.Int, dtp.Rows[0][0].ToString());
                    ClsUtility.AddParameters("@visitpk", SqlDbType.Int, dtp.Rows[0][1].ToString());
                    ClsUtility.AddParameters("@locationid", SqlDbType.Int, ht["LocationID"].ToString());
                    ClsUtility.AddParameters("@barrierid", SqlDbType.Int, dtBarrier.Rows[i]["BarrierID"].ToString());
                    ClsUtility.AddParameters("@userID", SqlDbType.Int, ht["UserID"].ToString());

                    if (intflag == 0)
                    {
                        int retval = (Int32)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveBarrier_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                    else if (intflag == 1)
                    {
                        int retval = (Int32)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateSaveBarrier_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                //// Custom Fields //////////////
                ////////////PreSet Values Used/////////////////
                /// #99# --- Ptn_Pk
                /// #88# --- LocationId
                /// #77# --- Visit_Pk
                /// #66# --- Visit_Date
                /// #55# --- Ptn_Pharmacy_Pk
                /// #44# --- OrderedByDate
                /// #33# --- LabId
                /// #22# --- TrackingId
                /// #11# --- CareEndedId
                /// #00# --- HomeVisitId
                ///////////////////////////////////////////////

                //ClsObject theCustomManager = new ClsObject();
                for (Int32 i = 0; i < theCustomFieldData.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    string theQuery = theCustomFieldData.Rows[i]["Query"].ToString();
                    theQuery = theQuery.Replace("#99#", dtp.Rows[0][0].ToString());
                    theQuery = theQuery.Replace("#88#", ht["LocationID"].ToString());
                    theQuery = theQuery.Replace("#77#", dtp.Rows[0][1].ToString());
                    theQuery = theQuery.Replace("#66#", "'" + ht["RegistrationDate"].ToString() + "'");
                    ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int RowsAffected = (Int32)PatientManager.ReturnObject(ClsUtility.theParams, "pr_General_Dynamic_Insert", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }
                ////////////////////////////////


                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return dtp;
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
        public DataSet InsertUpdatePatientRegistration(Hashtable ht, DataTable dtCaretype, DataTable dtARTsponsor, DataTable dt, DataTable dtBarrier, int VisitID, int dataquality, DataTable theCustomFieldData)
        {
            try
            {
                int Rowsaffected;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject PatientManager = new ClsObject();
                PatientManager.Connection = this.Connection;
                PatientManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientID", SqlDbType.Int, ht["PatientID"].ToString());
                ClsUtility.AddParameters("@LocationID", SqlDbType.Int, ht["LocationID"].ToString());
                ClsUtility.AddParameters("@ReferredFrom", SqlDbType.VarChar, ht["ReferredFrom"].ToString());
                ClsUtility.AddParameters("@ReferredFromSpecify", SqlDbType.VarChar, ht["ReferredFromSpecify"].ToString());
                ClsUtility.AddParameters("@RegistrationDate", SqlDbType.VarChar, ht["RegistrationDate"].ToString());
                ClsUtility.AddParameters("@TransferIn", SqlDbType.VarChar, ht["Transferin"].ToString());
                ClsUtility.AddParameters("@LPTFTransferFrom", SqlDbType.VarChar, ht["LPTFTransferfrom"].ToString());
                ClsUtility.AddParameters("@EducationLevel", SqlDbType.VarChar, ht["EducationLevel"].ToString());
                ClsUtility.AddParameters("@Literacy", SqlDbType.VarChar, ht["Literacy"].ToString());
                ClsUtility.AddParameters("@EmployeeID", SqlDbType.VarChar, ht["Interviewer"].ToString());
                ClsUtility.AddParameters("@Status", SqlDbType.VarChar, "0");
                ClsUtility.AddParameters("@StatusChangedDate", SqlDbType.VarChar, ht["HIVStatusChangedDate"].ToString());
                ClsUtility.AddParameters("@DataQuality", SqlDbType.Int, dataquality.ToString());
                //ClsUtility.AddParameters("@GuardianName", SqlDbType.VarChar, "");
                //ClsUtility.AddParameters("@GuardianInformation", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@EmergContactKnowsHIVStatus", SqlDbType.VarChar, ht["KnowHIVStatus"].ToString());
                ClsUtility.AddParameters("@DiscussStatus", SqlDbType.VarChar, ht["DiscussStatus"].ToString());
                ClsUtility.AddParameters("@PrevHIVCare", SqlDbType.VarChar, ht["PrevHIVCare"].ToString());
                ClsUtility.AddParameters("@PrevMedRecords", SqlDbType.VarChar, ht["PrevMedRecords"].ToString());
                ClsUtility.AddParameters("@PrevCareHomeBased", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@PrevCareVCT", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@PrevCareSTI", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@PrevCarePMTCT", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@PrevCareInPatient", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@PrevCareOther", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@PrevCareOtherSpecify", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@PrevART", SqlDbType.VarChar, ht["ArtSponsor"].ToString());
                ClsUtility.AddParameters("@PrevARTSSelfFinanced", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@PrevARTSGovtSponsored", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@PrevARTSUSGSponsered", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@PrevARTSMissionBased", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@PrevARTSThisFacility", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@PrevARTSOthers", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@PrevARTSOtherSpecs", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@EmploymentStatus", SqlDbType.VarChar, ht["EmploymentStatus"].ToString());
                ClsUtility.AddParameters("@Occupation", SqlDbType.VarChar, ht["Occupation"].ToString());
                ClsUtility.AddParameters("@MonthlyIncome", SqlDbType.VarChar, ht["MonthlyIncome"].ToString());
                ClsUtility.AddParameters("@NumChildren", SqlDbType.VarChar, ht["NumChildren"].ToString());
                ClsUtility.AddParameters("@NumPeopleHousehold", SqlDbType.VarChar, ht["NumPeopleHousehold"].ToString());
                ClsUtility.AddParameters("@DistanceTravelled", SqlDbType.VarChar, ht["DistanceTravelled"].ToString());
                ClsUtility.AddParameters("@TimeTravelled", SqlDbType.VarChar, ht["TimeTravelled"].ToString());
                ClsUtility.AddParameters("@TravelledUnits", SqlDbType.VarChar, ht["TimeTravelledUnits"].ToString());
                ClsUtility.AddParameters("@HIVStatus", SqlDbType.VarChar, ht["HIVStatus"].ToString());
                ClsUtility.AddParameters("@HIVStatus_Child", SqlDbType.VarChar, ht["KnowHIVChildStatus"].ToString());
                ClsUtility.AddParameters("@HIVDisclosure", SqlDbType.VarChar, ht["HIVDisclosure"].ToString());
                ClsUtility.AddParameters("@HIVDisclosureOther", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@NumHouseholdHIVTest", SqlDbType.VarChar, ht["NumHouseholdHIVTest"].ToString());
                ClsUtility.AddParameters("@NumHouseholdHIVPositive", SqlDbType.VarChar, ht["NumHouseholdHIVPositive"].ToString());
                ClsUtility.AddParameters("@NumHouseholdHIVDied", SqlDbType.VarChar, ht["NumHouseholdHIVDied"].ToString());
                ClsUtility.AddParameters("@SupportGroup", SqlDbType.VarChar, ht["SupportGroup"].ToString());
                ClsUtility.AddParameters("@SupportGroupName", SqlDbType.VarChar, ht["SupportGroupName"].ToString());
                ClsUtility.AddParameters("@ReferredFromVCT", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@ReferredFromOutpatient", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@ReferredFromOtherSource", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@ReferredFromPMTCT", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@ReferredFromTBOutpatient", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@ReferredFromInPatientWard", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@ReferredFromOtherFacility", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                ClsUtility.AddParameters("@Password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                DataSet DSRowsReturned = (DataSet)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_InsertUpdateEnrollmentHIVCare_Constella", ClsDBUtility.ObjectEnum.DataSet);
                VisitID = Convert.ToInt32(DSRowsReturned.Tables[0].Rows[0]["VisitID"].ToString());

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientid", SqlDbType.Int, ht["PatientID"].ToString());
                ClsUtility.AddParameters("@visitpk", SqlDbType.Int, VisitID.ToString());
                DataTable DTtempcaretype = (DataTable)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_DeleteCareType_Constella", ClsDBUtility.ObjectEnum.DataTable);

                for (int i = 0; i < dtCaretype.Rows.Count; i++)
                {

                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@patientid", SqlDbType.Int, ht["PatientID"].ToString());
                    ClsUtility.AddParameters("@visitpk", SqlDbType.Int, VisitID.ToString());
                    ClsUtility.AddParameters("@locationid", SqlDbType.Int, ht["LocationID"].ToString());
                    ClsUtility.AddParameters("@HIVAIDsCareID", SqlDbType.Int, dtCaretype.Rows[i]["HIVCareTypeID"].ToString());
                    ClsUtility.AddParameters("@HIVAIDsCareDesc", SqlDbType.VarChar, dtCaretype.Rows[i]["HIVCareTypeOther"].ToString());
                    ClsUtility.AddParameters("@userID", SqlDbType.Int, ht["UserID"].ToString());
                    int retvalout = (Int32)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveHIVAIDsCareType_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }

                //returning ARTSponsor;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientid", SqlDbType.Int, ht["PatientID"].ToString());
                ClsUtility.AddParameters("@visitpk", SqlDbType.Int, VisitID.ToString());
                ClsUtility.AddParameters("@locationid", SqlDbType.Int, ht["LocationID"].ToString());
                DataTable DTtempartsponsor = (DataTable)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_DeleteARTSponsor_Constella", ClsDBUtility.ObjectEnum.DataTable);

                for (int i = 0; i < dtARTsponsor.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@patientid", SqlDbType.Int, ht["PatientID"].ToString());
                    ClsUtility.AddParameters("@visitpk", SqlDbType.Int, VisitID.ToString());
                    ClsUtility.AddParameters("@locationid", SqlDbType.Int, ht["LocationID"].ToString());
                    ClsUtility.AddParameters("@ARTSponsorID", SqlDbType.Int, dtARTsponsor.Rows[i]["ARTsponsorID"].ToString());
                    ClsUtility.AddParameters("@ARTSponsorDesc", SqlDbType.VarChar, dtARTsponsor.Rows[i]["ARTSponsorOther"].ToString());
                    ClsUtility.AddParameters("@userID", SqlDbType.Int, ht["UserID"].ToString());
                    int retvalout = (Int32)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveARTSponsor_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }

                //Disclosure
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientid", SqlDbType.Int, ht["PatientID"].ToString());
                ClsUtility.AddParameters("@visitpk", SqlDbType.Int, VisitID.ToString());
                ClsUtility.AddParameters("@locationid", SqlDbType.Int, ht["LocationID"].ToString());
                Rowsaffected = (int)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_DeleteDiscloseEnrol_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@patientid", SqlDbType.Int, ht["PatientID"].ToString());
                    ClsUtility.AddParameters("@visitpk", SqlDbType.Int, VisitID.ToString());
                    ClsUtility.AddParameters("@locationid", SqlDbType.Int, ht["LocationID"].ToString());
                    ClsUtility.AddParameters("@disclosureid", SqlDbType.Int, dt.Rows[i]["DisclosureID"].ToString());
                    ClsUtility.AddParameters("@HIVDisclosureOther", SqlDbType.VarChar, dt.Rows[i]["DisclosureOther"].ToString());
                    int retval = (Int32)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveDiscloseEnrol_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                }
                //returning Barrier Manager
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientid", SqlDbType.Int, ht["PatientID"].ToString());
                ClsUtility.AddParameters("@visitpk", SqlDbType.Int, VisitID.ToString());
                ClsUtility.AddParameters("@locationid", SqlDbType.Int, ht["LocationID"].ToString());
                DataTable DTtempbarrier = (DataTable)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_DeleteBarrier_Constella", ClsDBUtility.ObjectEnum.DataTable);
                for (int i = 0; i < dtBarrier.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@patientid", SqlDbType.Int, ht["PatientID"].ToString());
                    ClsUtility.AddParameters("@visitpk", SqlDbType.Int, VisitID.ToString());
                    ClsUtility.AddParameters("@locationid", SqlDbType.Int, ht["LocationID"].ToString());
                    ClsUtility.AddParameters("@barrierid", SqlDbType.Int, dtBarrier.Rows[i]["BarrierID"].ToString());
                    ClsUtility.AddParameters("@userID", SqlDbType.Int, ht["UserID"].ToString());
                    int retvalout = (Int32)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateSaveBarrier_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }

                //// Custom Fields //////////////
                ////////////PreSet Values Used/////////////////
                /// #99# --- Ptn_Pk
                /// #88# --- LocationId
                /// #77# --- Visit_Pk
                /// #66# --- Visit_Date
                /// #55# --- Ptn_Pharmacy_Pk
                /// #44# --- OrderedByDate
                /// #33# --- LabId
                /// #22# --- TrackingId
                /// #11# --- CareEndedId
                /// #00# --- HomeVisitId
                ///////////////////////////////////////////////
                for (Int32 i = 0; i < theCustomFieldData.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    string theQuery = theCustomFieldData.Rows[i]["Query"].ToString();
                    theQuery = theQuery.Replace("#99#", ht["PatientID"].ToString());
                    theQuery = theQuery.Replace("#88#", ht["LocationID"].ToString());
                    theQuery = theQuery.Replace("#77#", VisitID.ToString());
                    theQuery = theQuery.Replace("#66#", "'" + ht["RegistrationDate"].ToString() + "'");
                    ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int theRowsAffected = (Int32)PatientManager.ReturnObject(ClsUtility.theParams, "pr_General_Dynamic_Insert", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }
                //////////////////////////////
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return DSRowsReturned;
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
        public DataTable SaveNewRegistration(Hashtable ht, int dataquality)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject PatientManager = new ClsObject();
                PatientManager.Connection = this.Connection;
                PatientManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@FirstName", SqlDbType.VarChar, ht["FirstName"].ToString());
                ClsUtility.AddParameters("@MiddleName", SqlDbType.VarChar, ht["MiddleName"].ToString());
                ClsUtility.AddParameters("@LastName", SqlDbType.VarChar, ht["LastName"].ToString());
                ClsUtility.AddParameters("@LocationID", SqlDbType.VarChar, ht["LocationID"].ToString());
                ClsUtility.AddParameters("@RegistrationDate", SqlDbType.VarChar, ht["RegistrationDate"].ToString());
                ClsUtility.AddParameters("@Sex", SqlDbType.VarChar, ht["Gender"].ToString());
                ClsUtility.AddParameters("@dob", SqlDbType.VarChar, ht["DOB"].ToString());
                ClsUtility.AddParameters("@DobPrecision", SqlDbType.VarChar, ht["DOBPrecision"].ToString());
                ClsUtility.AddParameters("@LocalCouncil", SqlDbType.VarChar, ht["LocalCouncil"].ToString());
                ClsUtility.AddParameters("@VillageName", SqlDbType.VarChar, ht["VillageName"].ToString());
                ClsUtility.AddParameters("@DistrictName", SqlDbType.VarChar, ht["DistrictName"].ToString());
                ClsUtility.AddParameters("@Province", SqlDbType.VarChar, ht["Province"].ToString());
                ClsUtility.AddParameters("@Address", SqlDbType.VarChar, ht["Address"].ToString());
                ClsUtility.AddParameters("@Phone", SqlDbType.VarChar, ht["Phone"].ToString());
                ClsUtility.AddParameters("@MaritalStatus", SqlDbType.VarChar, ht["MaritalStatus"].ToString());
                ClsUtility.AddParameters("@DataQuality", SqlDbType.VarChar, dataquality.ToString());
                ClsUtility.AddParameters("@EmergContactName", SqlDbType.VarChar, ht["EmergencyContactName"].ToString());
                ClsUtility.AddParameters("@EmergContactRelation", SqlDbType.VarChar, ht["EmergencyContactRelation"].ToString());
                ClsUtility.AddParameters("@EmergContactPhone", SqlDbType.VarChar, ht["EmergencyContactPhone"].ToString());
                ClsUtility.AddParameters("@EmergContactAddress", SqlDbType.VarChar, ht["EmergencyContactAddress"].ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.VarChar, ht["UserID"].ToString());
                ClsUtility.AddParameters("@SatelliteID", SqlDbType.VarChar, ht["SatelliteID"].ToString());
                ClsUtility.AddParameters("@CountryID", SqlDbType.VarChar, ht["CountryID"].ToString());
                ClsUtility.AddParameters("@PosID", SqlDbType.VarChar, ht["PosID"].ToString());
                //ClsUtility.AddParameters("@PatientImage", SqlDbType.VarChar, ht["PatientImage"].ToString());
                ClsUtility.AddParameters("@Password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);

                DataTable dtp = (DataTable)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SavePatientRegistration_Constella", ClsDBUtility.ObjectEnum.DataTable);

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return dtp;
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
        public int UpdatePatientRegistration(Hashtable ht, int Ptn_Pk, int VisitID, int dataquality)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject PatientManager = new ClsObject();
                PatientManager.Connection = this.Connection;
                PatientManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientID", SqlDbType.Int, Ptn_Pk.ToString());
                ClsUtility.AddParameters("@VisitID", SqlDbType.Int, VisitID.ToString());
                ClsUtility.AddParameters("@FirstName", SqlDbType.VarChar, ht["FirstName"].ToString());
                ClsUtility.AddParameters("@MiddleName", SqlDbType.VarChar, ht["MiddleName"].ToString());
                ClsUtility.AddParameters("@LastName", SqlDbType.VarChar, ht["LastName"].ToString());
                ClsUtility.AddParameters("@RegistrationDate", SqlDbType.VarChar, ht["RegistrationDate"].ToString());
                ClsUtility.AddParameters("@Sex", SqlDbType.VarChar, ht["Gender"].ToString());
                ClsUtility.AddParameters("@dob", SqlDbType.VarChar, ht["DOB"].ToString());
                ClsUtility.AddParameters("@DobPrecision", SqlDbType.VarChar, ht["DOBPrecision"].ToString());
                ClsUtility.AddParameters("@LocalCouncil", SqlDbType.VarChar, ht["LocalCouncil"].ToString());
                ClsUtility.AddParameters("@VillageName", SqlDbType.VarChar, ht["VillageName"].ToString());
                ClsUtility.AddParameters("@DistrictName", SqlDbType.VarChar, ht["DistrictName"].ToString());
                ClsUtility.AddParameters("@Province", SqlDbType.VarChar, ht["Province"].ToString());
                ClsUtility.AddParameters("@Address", SqlDbType.VarChar, ht["Address"].ToString());
                ClsUtility.AddParameters("@Phone", SqlDbType.VarChar, ht["Phone"].ToString());
                ClsUtility.AddParameters("@MaritalStatus", SqlDbType.VarChar, ht["MaritalStatus"].ToString());
                ClsUtility.AddParameters("@DataQuality", SqlDbType.VarChar, dataquality.ToString());
                ClsUtility.AddParameters("@EmergContactName", SqlDbType.VarChar, ht["EmergencyContactName"].ToString());
                ClsUtility.AddParameters("@EmergContactRelation", SqlDbType.VarChar, ht["EmergencyContactRelation"].ToString());
                ClsUtility.AddParameters("@EmergContactPhone", SqlDbType.VarChar, ht["EmergencyContactPhone"].ToString());
                ClsUtility.AddParameters("@EmergContactAddress", SqlDbType.VarChar, ht["EmergencyContactAddress"].ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.VarChar, ht["UserID"].ToString());
                ClsUtility.AddParameters("@Password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                int RowsAffected = (Int32)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdatePatientRegistration_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                if (RowsAffected == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Updating Patient Enrolment record. Try Again..";
                    AppException.Create("#C1", theBL);
                }
                ////////DataMgr.CommitTransaction(this.Transaction);
                ////////DataMgr.ReleaseConnection(this.Connection);
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return Convert.ToInt32(RowsAffected);
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
        public DataTable SaveUpdateTechnicalArea(Hashtable ht, int VisitID)
        {
            DataSet theDS = new DataSet();
            try
            {
                this.Connection = DataMgr.GetConnection();
                ClsObject PatientManager = new ClsObject();
                PatientManager.Connection = this.Connection;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@flag", SqlDbType.VarChar, ht["flag"].ToString());
                ClsUtility.AddParameters("@Action", SqlDbType.VarChar, ht["Action"].ToString());
                ClsUtility.AddParameters("@PatientID", SqlDbType.VarChar, ht["PatientID"].ToString());
                ClsUtility.AddParameters("@RegistrationDate", SqlDbType.VarChar, ht["RegistrationDate"].ToString());
                ClsUtility.AddParameters("@ANCNumber", SqlDbType.VarChar, ht["ANCNumber"].ToString());
                ClsUtility.AddParameters("@PMTCTNumber", SqlDbType.VarChar, ht["PMTCTNumber"].ToString());
                ClsUtility.AddParameters("@Admission", SqlDbType.VarChar, ht["Admission"].ToString());
                ClsUtility.AddParameters("@OutpatientNumber", SqlDbType.VarChar, ht["OutpatientNumber"].ToString());
                ClsUtility.AddParameters("@CountryID", SqlDbType.VarChar, ht["CountryId"].ToString());
                ClsUtility.AddParameters("@POSID", SqlDbType.VarChar, ht["POSId"].ToString());
                ClsUtility.AddParameters("@SatelliteID", SqlDbType.VarChar, ht["SatelliteId"].ToString());
                ClsUtility.AddParameters("@PatientEnrollmentID", SqlDbType.VarChar, ht["PatientEnrollmentID"].ToString());
                ClsUtility.AddParameters("@PatientClinicID", SqlDbType.VarChar, ht["HospitalID"].ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                ClsUtility.AddParameters("@LocationID", SqlDbType.VarChar, ht["LocationID"].ToString());
                ClsUtility.AddParameters("@visitID", SqlDbType.Int, VisitID.ToString());
                ClsUtility.AddParameters("@VisitType", SqlDbType.Int, ht["VisitType"].ToString());

                theDS = (DataSet)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdateTechnicalArea_Constella", ClsDBUtility.ObjectEnum.DataSet);
                return theDS.Tables[0];
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

        #endregion
        #region "CTC"
        public DataSet theDropdown(string ID, string Flag)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ID", SqlDbType.VarChar, ID);
                ClsUtility.AddParameters("@Flag", SqlDbType.VarChar, Flag);
                ClsObject DDMgr = new ClsObject();
                return (DataSet)DDMgr.ReturnObject(ClsUtility.theParams, "pr_Clinical_DropDownCTC_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public DataTable SavePatientRegistrationCTC(Hashtable ht, int Flag, DataTable theCustomFieldData)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject PatientManagerCTC = new ClsObject();
                PatientManagerCTC.Connection = this.Connection;
                PatientManagerCTC.Transaction = this.Transaction;
                int theRowAffected = 0;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@LocationID", SqlDbType.VarChar, ht["LocationID"].ToString());
                ClsUtility.AddParameters("@CountryID", SqlDbType.VarChar, ht["CountryID"].ToString());
                ClsUtility.AddParameters("@PosID", SqlDbType.VarChar, ht["PosID"].ToString());
                ClsUtility.AddParameters("@SatelliteID", SqlDbType.VarChar, ht["SatelliteID"].ToString());
                ClsUtility.AddParameters("@PatientEnrolID", SqlDbType.VarChar, ht["EnrolmentID"].ToString());
                ClsUtility.AddParameters("@FileRefID", SqlDbType.VarChar, ht["FileReferenceID"].ToString());
                ClsUtility.AddParameters("@FirstName", SqlDbType.VarChar, ht["FirstName"].ToString());
                ClsUtility.AddParameters("@MiddleName", SqlDbType.VarChar, ht["MiddleName"].ToString());
                ClsUtility.AddParameters("@LastName", SqlDbType.VarChar, ht["LastName"].ToString());
                ClsUtility.AddParameters("@RegDate", SqlDbType.Int, ht["RegDate"].ToString());
                ClsUtility.AddParameters("@Gender", SqlDbType.VarChar, ht["Gender"].ToString());
                ClsUtility.AddParameters("@DOB", SqlDbType.VarChar, ht["DOB"].ToString());
                ClsUtility.AddParameters("@Status", SqlDbType.VarChar, "0");
                ClsUtility.AddParameters("@DOBPrecision", SqlDbType.VarChar, ht["DOBPrecision"].ToString());
                ClsUtility.AddParameters("@Maristatus", SqlDbType.VarChar, ht["Maristatus"].ToString());
                ClsUtility.AddParameters("@Phone", SqlDbType.VarChar, ht["Phone"].ToString());
                ClsUtility.AddParameters("@ConDetails", SqlDbType.VarChar, ht["ConDetails"].ToString());
                ClsUtility.AddParameters("@Region", SqlDbType.VarChar, ht["Region"].ToString());
                ClsUtility.AddParameters("@District", SqlDbType.VarChar, ht["District"].ToString());
                ClsUtility.AddParameters("@Division", SqlDbType.VarChar, ht["Division"].ToString());
                ClsUtility.AddParameters("@Ward", SqlDbType.VarChar, ht["Ward"].ToString());
                ClsUtility.AddParameters("@Village", SqlDbType.VarChar, ht["Village"].ToString());
                ClsUtility.AddParameters("@TCLLeader", SqlDbType.VarChar, ht["TCLLeader"].ToString());
                ClsUtility.AddParameters("@TCLContact", SqlDbType.VarChar, ht["TCLContact"].ToString());
                ClsUtility.AddParameters("@HHead", SqlDbType.VarChar, ht["HHead"].ToString());
                ClsUtility.AddParameters("@Hcontact", SqlDbType.VarChar, ht["Hcontact"].ToString());
                ClsUtility.AddParameters("@SupportName", SqlDbType.VarChar, ht["SupportName"].ToString());
                ClsUtility.AddParameters("@TsAddress", SqlDbType.VarChar, ht["TsAddress"].ToString());
                ClsUtility.AddParameters("@TsPhone", SqlDbType.VarChar, ht["TsPhone"].ToString());
                ClsUtility.AddParameters("@ComSOrganisation", SqlDbType.VarChar, ht["ComSOrganisation"].ToString());
                ClsUtility.AddParameters("@FirstHIVPosTestDate", SqlDbType.VarChar, ht["PosHivTest"].ToString());
                ClsUtility.AddParameters("@ConfirmHIVPosDate", SqlDbType.VarChar, ht["ConfirmHivPositive"].ToString());
                ClsUtility.AddParameters("@DrugAllery", SqlDbType.VarChar, ht["DrugAllery"].ToString());
                ClsUtility.AddParameters("@DataQuality", SqlDbType.Int, Flag.ToString());
                ClsUtility.AddParameters("@ReferredFrom", SqlDbType.VarChar, ht["ReferredFrom"].ToString());
                ClsUtility.AddParameters("@ReferredFromOther", SqlDbType.VarChar, ht["ReferredFromOther"].ToString());
                ClsUtility.AddParameters("@PriorExposure", SqlDbType.VarChar, ht["PriorExposure"].ToString());
                ClsUtility.AddParameters("@ArtStartDate", SqlDbType.VarChar, ht["ArtStartDate"].ToString());
                ClsUtility.AddParameters("@WhyEligible", SqlDbType.VarChar, ht["WhyEligible"].ToString());
                ClsUtility.AddParameters("@InitialRegCode", SqlDbType.VarChar, ht["InitialRegimenCode"].ToString());
                ClsUtility.AddParameters("@PrevARVRegimen", SqlDbType.VarChar, ht["InitialRegimenAbb"].ToString());
                ClsUtility.AddParameters("@WHOStage", SqlDbType.VarChar, ht["WHOStage"].ToString());
                ClsUtility.AddParameters("@FunStatus", SqlDbType.VarChar, ht["FunStatus"].ToString());
                ClsUtility.AddParameters("@Weight", SqlDbType.VarChar, ht["Weight"].ToString());
                ClsUtility.AddParameters("@CD4", SqlDbType.VarChar, ht["CD4"].ToString());
                ClsUtility.AddParameters("@PrevARVsCD4Percent", SqlDbType.VarChar, ht["CD4Percent"].ToString());
                ClsUtility.AddParameters("@TLC", SqlDbType.VarChar, ht["TLC"].ToString());
                ClsUtility.AddParameters("@TLCPercent", SqlDbType.VarChar, ht["TLCPercent"].ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                DataTable dtp = new DataTable();
                DataSet objDs = new DataSet();
                if (ht["Update"].ToString() == "1")
                {
                    ClsUtility.AddParameters("@PatientID", SqlDbType.VarChar, ht["ptn_pk"].ToString());
                    objDs = (DataSet)PatientManagerCTC.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateEnrollmentCTC_Constella", ClsDBUtility.ObjectEnum.DataSet);
                    dtp = objDs.Tables[0];
                }
                else
                {
                    ClsUtility.AddParameters("@PatientID", SqlDbType.VarChar, ht["ptn_pk"].ToString());
                    objDs = (DataSet)PatientManagerCTC.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveEnrollmentCTC_Constella", ClsDBUtility.ObjectEnum.DataSet);
                    dtp = objDs.Tables[0];
                }
                //// Custom Fields //////////////
                ////////////PreSet Values Used/////////////////
                /// #99# --- Ptn_Pk
                /// #88# --- LocationId
                /// #77# --- Visit_Pk
                /// #66# --- Visit_Date                
                ///////////////////////////////////////////////

                //ClsObject theCustomManager = new ClsObject();
                for (Int32 i = 0; i < theCustomFieldData.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    string theQuery = theCustomFieldData.Rows[i]["Query"].ToString();
                    theQuery = theQuery.Replace("#99#", objDs.Tables[0].Rows[0][0].ToString());
                    theQuery = theQuery.Replace("#88#", ht["LocationID"].ToString());
                    theQuery = theQuery.Replace("#77#", objDs.Tables[1].Rows[0][0].ToString());
                    theQuery = theQuery.Replace("#66#", "'" + ht["RegDate"].ToString() + "'");
                    ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int RowsAffected = (Int32)PatientManagerCTC.ReturnObject(ClsUtility.theParams, "pr_General_Dynamic_Insert", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }


                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return dtp;
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
        public DataSet GetPatientDetailsCTC(string patientId, string CountryID, string PosID, string SatelliteID, string PatientClinicID, int Existflag, int VisitID)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject PatientManagerCTC = new ClsObject();
                PatientManagerCTC.Connection = this.Connection;
                PatientManagerCTC.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientID", SqlDbType.VarChar, patientId);
                ClsUtility.AddParameters("@CountryID", SqlDbType.VarChar, CountryID);
                ClsUtility.AddParameters("@PosID", SqlDbType.VarChar, PosID);
                ClsUtility.AddParameters("@SatelliteID", SqlDbType.VarChar, SatelliteID);
                ClsUtility.AddParameters("@PatientClinicID", SqlDbType.VarChar, PatientClinicID.ToString());
                ClsUtility.AddParameters("@ExistFlag", SqlDbType.Int, Existflag.ToString());
                ClsUtility.AddParameters("@VisitID", SqlDbType.Int, "0");
                ClsUtility.AddParameters("@password", SqlDbType.Int, ApplicationAccess.DBSecurity);
                return (DataSet)PatientManagerCTC.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetEnrollmentCTC_Constella", ClsDBUtility.ObjectEnum.DataSet);

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
        }

        public DataSet GetDrugGenericCTC()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject CTCDrugManager = new ClsObject();
                return (DataSet)CTCDrugManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetDrugGenericPatientRegistrationCTC_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        #endregion


        #region "PMTCT"

        public DataTable Validate(string Argument, string Flag)
        {
            DataTable theDT = new DataTable();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsObject MgrValidatePMTCT = new ClsObject();
                MgrValidatePMTCT.Connection = this.Connection;
                MgrValidatePMTCT.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Argument", SqlDbType.VarChar, Argument.ToString());
                ClsUtility.AddParameters("@Flag", SqlDbType.Int, Flag.ToString());
                return theDT = (DataTable)MgrValidatePMTCT.ReturnObject(ClsUtility.theParams, "pr_Clinical_ValidateEnrollmentPMTCT_Constella", ClsDBUtility.ObjectEnum.DataTable);
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

        }

        public DataTable SavePatientRegistrationPMTCT(Hashtable ht, DataTable theCustomFieldData)
        {
            DataSet theDS = new DataSet();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsObject PatientManagerPMTCT = new ClsObject();
                PatientManagerPMTCT.Connection = this.Connection;
                PatientManagerPMTCT.Transaction = this.Transaction;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientID", SqlDbType.VarChar, ht["PatientID"].ToString());
                ClsUtility.AddParameters("@FirstName", SqlDbType.VarChar, ht["FName"].ToString());
                ClsUtility.AddParameters("@MiddleName", SqlDbType.VarChar, ht["MName"].ToString());
                ClsUtility.AddParameters("@LastName", SqlDbType.VarChar, ht["LName"].ToString());
                ClsUtility.AddParameters("@RegDate", SqlDbType.VarChar, ht["RegDate"].ToString());
                ClsUtility.AddParameters("@Sex", SqlDbType.VarChar, ht["Gender"].ToString());
                ClsUtility.AddParameters("@DOB", SqlDbType.VarChar, ht["DOB"].ToString());
                ClsUtility.AddParameters("@DOBPrecision", SqlDbType.VarChar, ht["DOBPrecision"].ToString());

                ClsUtility.AddParameters("@MStatus", SqlDbType.VarChar, ht["MStatus"].ToString());
                ClsUtility.AddParameters("@TransferIn", SqlDbType.VarChar, ht["TransferIn"].ToString());
                ClsUtility.AddParameters("@RefFrom", SqlDbType.VarChar, ht["RefFrom"].ToString());
                ClsUtility.AddParameters("@ANCNumber", SqlDbType.VarChar, ht["ANCNumber"].ToString());
                ClsUtility.AddParameters("@PMTCTNumber", SqlDbType.VarChar, ht["PMTCTNumber"].ToString());
                ClsUtility.AddParameters("@Admission", SqlDbType.VarChar, ht["Admission"].ToString());
                ClsUtility.AddParameters("@OutpatientNumber", SqlDbType.VarChar, ht["OutpatientNumber"].ToString());
                ClsUtility.AddParameters("@Address", SqlDbType.VarChar, ht["Address"].ToString());
                ClsUtility.AddParameters("@Village", SqlDbType.VarChar, ht["Village"].ToString());
                ClsUtility.AddParameters("@District", SqlDbType.VarChar, ht["District"].ToString());
                ClsUtility.AddParameters("@Phone", SqlDbType.VarChar, ht["Phone"].ToString());



                ClsUtility.AddParameters("@LocationID", SqlDbType.VarChar, ht["LocationID"].ToString());
                ClsUtility.AddParameters("@DataQuality", SqlDbType.VarChar, ht["DataQuality"].ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.VarChar, ht["UserID"].ToString());
                ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ClsUtility.AddParameters("@visittype", SqlDbType.VarChar, ht["VisitType"].ToString());

                if (ht["PatientID"].ToString() == "") //add mode only
                {
                    ClsUtility.AddParameters("@Status", SqlDbType.VarChar, "0");
                    ClsUtility.AddParameters("@CountryID", SqlDbType.VarChar, ht["CountryId"].ToString()); //only in insert mode,should not be updated in update mode
                    ClsUtility.AddParameters("@POSID", SqlDbType.VarChar, ht["POSId"].ToString());
                    ClsUtility.AddParameters("@SatelliteID", SqlDbType.VarChar, ht["SatelliteId"].ToString());
                }

                theDS = (DataSet)PatientManagerPMTCT.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveEnrollmentFrmPMTCT_Constella", ClsDBUtility.ObjectEnum.DataSet);

                DataTable dtp = new DataTable();
                dtp = theDS.Tables[0];
                //// Custom Fields //////////////
                ////////////PreSet Values Used/////////////////
                /// #99# --- Ptn_Pk
                /// #88# --- LocationId
                /// #77# --- Visit_Pk
                /// #66# --- Visit_Date                
                ///////////////////////////////////////////////

                //ClsObject theCustomManager = new ClsObject();
                for (Int32 i = 0; i < theCustomFieldData.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    string theQuery = theCustomFieldData.Rows[i]["Query"].ToString();
                    theQuery = theQuery.Replace("#99#", theDS.Tables[0].Rows[0][0].ToString());
                    theQuery = theQuery.Replace("#88#", ht["LocationID"].ToString());
                    theQuery = theQuery.Replace("#77#", theDS.Tables[0].Rows[0]["Visit_ID"].ToString());
                    theQuery = theQuery.Replace("#66#", "'" + ht["RegDate"].ToString() + "'");
                    ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int RowsAffected = (Int32)PatientManagerPMTCT.ReturnObject(ClsUtility.theParams, "pr_General_Dynamic_Insert", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return dtp;

                //            string theQuery = theCustomFieldData.Rows[i]["Query"].ToString();
                //            theQuery = theQuery.Replace("#99#", ht["PatientID"].ToString());
                //            theQuery = theQuery.Replace("#88#", ht["LocationID"].ToString());
                //            theQuery = theQuery.Replace("#77#", theDS.Tables[0].Rows[0]["Visit_ID"].ToString());
                //            theQuery = theQuery.Replace("#66#", "'" + ht["RegDate"].ToString() + "'");
                //            ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                //            int RowsAffected = (Int32)PatientManagerPMTCT.ReturnObject(ClsUtility.theParams, "pr_General_Dynamic_Insert", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                //        }

                //        DataMgr.CommitTransaction(this.Transaction);
                //        DataMgr.ReleaseConnection(this.Connection);
                //        return dtp;
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


        public DataSet GetPatientRegistrationPMTCT(int PatientId)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject PatientManagerCTC = new ClsObject();
                PatientManagerCTC.Connection = this.Connection;
                PatientManagerCTC.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientId.ToString());
                //ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientId.ToString());

                ClsUtility.AddParameters("@VisitID", SqlDbType.Int, "11");
                ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                return (DataSet)PatientManagerCTC.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetRegistrationPMTCT_Constella", ClsDBUtility.ObjectEnum.DataSet);

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


        }


        //public DataTable UpdatePatientRegistrationPMTCT(Hashtable ht, DataTable theCustomFieldData)
        //{
        //    DataSet theDS = new DataSet();
        //    try
        //    {
        //        this.Connection = DataMgr.GetConnection();
        //        this.Transaction = DataMgr.BeginTransaction(this.Connection);
        //        ClsObject PatientManagerPMTCT = new ClsObject();
        //        PatientManagerPMTCT.Connection = this.Connection;
        //        PatientManagerPMTCT.Transaction = this.Transaction;

        //        ClsUtility.Init_Hashtable();
        //        ClsUtility.AddParameters("@PatientID", SqlDbType.VarChar, ht["PatientID"].ToString());
        //        ClsUtility.AddParameters("@FirstName", SqlDbType.VarChar, ht["FName"].ToString());
        //        ClsUtility.AddParameters("@MiddleName", SqlDbType.VarChar, ht["MName"].ToString());
        //        ClsUtility.AddParameters("@LastName", SqlDbType.VarChar, ht["LName"].ToString());
        //        ClsUtility.AddParameters("@RegDate", SqlDbType.VarChar, ht["RegDate"].ToString());
        //        ClsUtility.AddParameters("@Sex", SqlDbType.VarChar, ht["Gender"].ToString());
        //        ClsUtility.AddParameters("@DOB", SqlDbType.VarChar, ht["DOB"].ToString());
        //        ClsUtility.AddParameters("@MStatus", SqlDbType.VarChar, ht["MStatus"].ToString());
        //        ClsUtility.AddParameters("@TransferIn", SqlDbType.VarChar, ht["TransferIn"].ToString());
        //        ClsUtility.AddParameters("@RefFrom", SqlDbType.VarChar, ht["RefFrom"].ToString());
        //        ClsUtility.AddParameters("@ANCNumber", SqlDbType.VarChar, ht["ANCNumber"].ToString());
        //        ClsUtility.AddParameters("@PMTCTNumber", SqlDbType.VarChar, ht["PMTCTNumber"].ToString());
        //        ClsUtility.AddParameters("@Admission", SqlDbType.VarChar, ht["Admission"].ToString());
        //        ClsUtility.AddParameters("@OutpatientNumber", SqlDbType.VarChar, ht["OutpatientNumber"].ToString());
        //        ClsUtility.AddParameters("@Address", SqlDbType.VarChar, ht["Address"].ToString());
        //        ClsUtility.AddParameters("@Village", SqlDbType.VarChar, ht["Village"].ToString());
        //        ClsUtility.AddParameters("@District", SqlDbType.VarChar, ht["District"].ToString());
        //        ClsUtility.AddParameters("@Phone", SqlDbType.VarChar, ht["Phone"].ToString());
        //        ClsUtility.AddParameters("@LocationID", SqlDbType.VarChar, ht["LocationID"].ToString());
        //        ClsUtility.AddParameters("@DataQuality", SqlDbType.VarChar, ht["DataQuality"].ToString());
        //        ClsUtility.AddParameters("@UserID", SqlDbType.VarChar, ht["UserID"].ToString());
        //        ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
        //        theDS = (DataSet)PatientManagerPMTCT.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateEnrollmentPMTCT_Constella", ClsDBUtility.ObjectEnum.DataSet);

        //        DataTable dtp = new DataTable();

        //        dtp = theDS.Tables[0];
        //        //// Custom Fields //////////////
        //        ////////////PreSet Values Used/////////////////
        //        /// #99# --- Ptn_Pk
        //        /// #88# --- LocationId
        //        /// #77# --- Visit_Pk
        //        /// #66# --- Visit_Date                
        //        ///////////////////////////////////////////////

        //        //ClsObject theCustomManager = new ClsObject();
        //        for (Int32 i = 0; i < theCustomFieldData.Rows.Count; i++)
        //        {
        //            ClsUtility.Init_Hashtable();
        //            string theQuery = theCustomFieldData.Rows[i]["Query"].ToString();
        //            theQuery = theQuery.Replace("#99#", ht["PatientID"].ToString());
        //            theQuery = theQuery.Replace("#88#", ht["LocationID"].ToString());
        //            theQuery = theQuery.Replace("#77#", theDS.Tables[0].Rows[0]["Visit_ID"].ToString());
        //            theQuery = theQuery.Replace("#66#", "'" + ht["RegDate"].ToString() + "'");
        //            ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
        //            int RowsAffected = (Int32)PatientManagerPMTCT.ReturnObject(ClsUtility.theParams, "pr_General_Dynamic_Insert", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
        //        }

        //        DataMgr.CommitTransaction(this.Transaction);
        //        DataMgr.ReleaseConnection(this.Connection);
        //        return dtp;
        //    }


        //    catch
        //    {

        //        DataMgr.RollBackTransation(this.Transaction);
        //        throw;
        //    }


        //    finally
        //    {
        //        if (this.Connection != null)
        //            DataMgr.ReleaseConnection(this.Connection);
        //    }

        //    return null;
        //}

        public DataSet GetChildDetail(int patientid, int LocationID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientid.ToString());
                ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ClsObject VisitManager = new ClsObject();
                return (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetChildDetail_Futures", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet SaveInfantInfo(Int64 PatientId, Int64 LocationID, Int64 VisitId, Int64 ParentId, Int64 UserID)
        {
            try
            {
                DataSet theDS;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptnpk", SqlDbType.BigInt, PatientId.ToString());
                ClsUtility.AddParameters("@VisitPk", SqlDbType.BigInt, VisitId.ToString());
                ClsUtility.AddParameters("@LocationId", SqlDbType.BigInt, LocationID.ToString());
                ClsUtility.AddParameters("@ParentID", SqlDbType.BigInt, ParentId.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.BigInt, UserID.ToString());
                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;
                VisitManager.Transaction = this.Transaction;
                theDS = (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveInfantInfo_Futures", ClsDBUtility.ObjectEnum.DataSet);

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);

                return theDS;
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

        public int DeleteInfantInfo(int PatientId, int UserID)
        {
            try
            {
                int theRowAffected = 0;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;
                VisitManager.Transaction = this.Transaction;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                theRowAffected = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_DeleteInfantInfo_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                if (theRowAffected == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Saving Custom Field. Try Again..";
                    AppException.Create("#C1", theMsg);

                }
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return theRowAffected;
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

        #endregion

        #region "ExposedInfant"
        public DataSet GetExposedInfantByParentId(int Ptn_Pk)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, Ptn_Pk.ToString());
                ClsObject VisitManager = new ClsObject();
                return (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetExposedInfantByParentId", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
        public int DeleteExposedInfantById(int Id)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Id", SqlDbType.Int, Id.ToString());
                ClsObject VisitManager = new ClsObject();
                int theRowAffected = 0;
                theRowAffected = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_DeleteExposedInfantById", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                return theRowAffected;
            }
        }
        public int SaveExposedInfant(int Id, int Ptn_Pk, int ExposedInfantId, string FirstName, string LastName, DateTime DOB, string FeedingPractice3mos,
            string CTX2mos, string HIVTestType, string HIVResult, string FinalStatus, DateTime? DeathDate, int UserID)
        {
            try
            {

                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;
                VisitManager.Transaction = this.Transaction;
                ClsUtility.Init_Hashtable();
                int theRowAffected = 0;
                ClsUtility.AddParameters("@Id", SqlDbType.Int, Id.ToString());
                ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, Ptn_Pk.ToString());
                ClsUtility.AddParameters("@ExposedInfantId", SqlDbType.Int, ExposedInfantId.ToString());
                ClsUtility.AddParameters("@FirstName", SqlDbType.VarChar, FirstName.ToString());
                ClsUtility.AddParameters("@LastName", SqlDbType.VarChar, LastName.ToString());
                ClsUtility.AddParameters("@DOB", SqlDbType.DateTime, DOB.ToString());
                ClsUtility.AddParameters("@FeedingPractice3mos", SqlDbType.VarChar, FeedingPractice3mos.ToString());
                ClsUtility.AddParameters("@CTX2mos", SqlDbType.VarChar, CTX2mos.ToString());
                ClsUtility.AddParameters("@HIVResult", SqlDbType.VarChar, HIVResult.ToString());
                ClsUtility.AddParameters("@HIVTestType", SqlDbType.VarChar, HIVTestType.ToString());
                ClsUtility.AddParameters("@FinalStatus", SqlDbType.VarChar, FinalStatus.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.VarChar, UserID.ToString());
                if (DeathDate != null)
                {
                    ClsUtility.AddParameters("@DeathDate", SqlDbType.VarChar, DeathDate == null ? null : DeathDate.ToString());
                }
                theRowAffected = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveExposedInfant", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                if (theRowAffected == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Saving Custom Field. Try Again..";
                    AppException.Create("#C1", theMsg);

                }
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return theRowAffected;
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
        #endregion
       
        #region "Technical Areas - Added Naveen -28-Oct-2010"
        public DataSet GetFieldNames(int ModuleID, int patientId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@moduleId", SqlDbType.Int, ModuleID.ToString());
                ClsUtility.AddParameters("@patientId", SqlDbType.Int, patientId.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "Pr_Clinical_GetModuleFieldNames_COnstella", ClsDBUtility.ObjectEnum.DataSet);
            }
        }


        #endregion
        public DataSet GetMaxAutoPopulateIdentifier(string columnname)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@columnname", SqlDbType.VarChar, columnname);
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetMaxAutopopulatIdentifier", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet CheckIdentity(string ExposedInfantId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ExposedInfantId", SqlDbType.Int, ExposedInfantId.ToString());
                ClsObject VisitManager = new ClsObject();
                return (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_CheckIdentityInfant", ClsDBUtility.ObjectEnum.DataSet);
            }
        }


        #region "Dynamic Registration"
        public DataSet GetFieldName_and_Label(int FeatureID, int PatientID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientId", SqlDbType.Int, PatientID.ToString());
                ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, FeatureID.ToString());
                ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ClsObject FieldMgr = new ClsObject();
                return (DataSet)FieldMgr.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetPatientRegistrationCustomFormFieldLabel_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }

        }

        public DataSet Common_GetSaveUpdateforCustomRegistrion(string Insert)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsObject PatientManager = new ClsObject();
                PatientManager.Connection = this.Connection;
                PatientManager.Transaction = this.Transaction;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Insert", SqlDbType.VarChar, Insert.ToString());
                ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                DataSet GetValue = (DataSet)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveCustomPatientRegistration_Constella", ClsDBUtility.ObjectEnum.DataSet);
                //if (RowsAffected == 0)
                //{
                //    MsgBuilder theBL = new MsgBuilder();
                //    theBL.DataElements["MessageText"] = "Error in Updating Patient Enrolment record. Try Again..";
                //    AppException.Create("#C1", theBL);
                //}
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return GetValue;
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

        #endregion

    }

    

}