using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HIVCE.BusinessLayer;
using HIVCE.Common;
using System.Configuration;
using HIVCE.Common.Entities;
using Application.Presentation;
using System.Data;
using Interface.HIVCE;
using Interface.Service;
using Application.Common;
using Interface.Clinical;

namespace HIVCE.Presentation
{
    public partial class frm_GreenCard : System.Web.UI.Page
    {
        ICommonData oCommonData = (ICommonData)ObjectFactory.CreateInstance("BusinessProcess.Service.BCommonData,BusinessProcess.Service");

        protected void Page_Load(object sender, EventArgs e)
        {
            //Session["PatientInformation"]
            //Session["PatientSex"] = "Female";
            //Session["PatientAge"] = "12";
            int PatientId = 0;
            int visitPK = 0;
            int locationId = 0;
            int userId = 0;
            if (!IsPostBack)
            {
                if (ConfigurationManager.AppSettings["IsDevelopment"].ToString() == "false")
                {
                    if (Session["AppLocationId"] == null)
                    {
                        Response.Redirect("~/frmlogin.aspx", true);
                    }

                    if (PatientId == 0)
                    {
                        Response.Redirect("~/ClinicalForms/frmPatient_Home.aspx", true);
                    }
                }
                if (!object.Equals(Session["PatientId"], null))
                {
                    PatientId = Convert.ToInt32(Session["PatientId"]);
                    this.hidPID.Value = PatientId.ToString();
                }
                if (!object.Equals(Session["PatientVisitId"], null))
                {
                    if (!object.Equals(Request.QueryString["add"], null))
                    {
                        if (Request.QueryString["add"].ToString() == "0")
                        {
                            visitPK = 0;
                            Session["PatientVisitId"] = "0";
                        }
                    }
                    else
                    {
                        visitPK = Convert.ToInt32(Session["PatientVisitId"]);
                    }
                }
                else
                {
                    if (!object.Equals(Request.QueryString["add"], null))
                    {
                        if (Request.QueryString["add"].ToString() == "0")
                        {
                            visitPK = 0;
                        }
                    }
                }
                this.hidVId.Value = visitPK.ToString();

                if (!object.Equals(Session["AppLocationId"], null))
                {
                    locationId = Convert.ToInt32(Session["AppLocationId"]);
                }
                if (!object.Equals(Session["AppUserId"], null))
                {
                    userId = Convert.ToInt32(Session["AppUserId"]);
                }
                if (!object.Equals(Session["PatientSex"], null))
                {
                    this.hidGender.Value = Session["PatientSex"].ToString();
                }
                if (!object.Equals(Session["PatientAge"], null))
                {
                    this.hidDOB.Value = Session["PatientAge"].ToString();
                }
                if (!object.Equals(Session["patientageinyearmonth"], null))
                {
                    this.hidPAYM.Value = Session["patientageinyearmonth"].ToString();
                }


                if (!object.Equals(Request.QueryString["data"], null))
                {
                    string response = string.Empty;
                    if (Request.QueryString["data"].ToString() == "getdata")
                    {
                        response = GetClinicalEnounter(Convert.ToInt32(PatientId), visitPK, locationId);
                        SendResponse(response);
                    }
                    if (Request.QueryString["data"].ToString() == "getpcdata")
                    {
                        response = GetPresentingComplaints(Convert.ToInt32(PatientId), visitPK, locationId);
                        SendResponse(response);
                    }
                    if (Request.QueryString["data"].ToString() == "getAddtionalHx")
                    {
                        response = GetAddtionalHx(Convert.ToInt32(PatientId), visitPK, locationId);
                        SendResponse(response);
                    }
                    if (Request.QueryString["data"].ToString() == "getsystemicreview")
                    {
                        response = GetSystemicReview(Convert.ToInt32(PatientId), visitPK, locationId);
                        SendResponse(response);
                    }
                    if (Request.QueryString["data"].ToString() == "getscreeningdata")
                    {
                        response = GetScreeningData(Convert.ToInt32(PatientId), visitPK, locationId);
                        SendResponse(response);
                    }
                    if (Request.QueryString["data"].ToString() == "getmgtdata")
                    {
                        response = GetManagementData(Convert.ToInt32(PatientId), visitPK, locationId);
                        SendResponse(response);
                    }
                    if (Request.QueryString["data"].ToString() == "getzscore")
                    {
                        response = GetZScoreDetails(Convert.ToInt32(PatientId), visitPK, locationId);
                        SendResponse(response);
                    }

                    if (Request.QueryString["data"].ToString() == "savetriage")
                    {

                        System.IO.StreamReader sr = new System.IO.StreamReader(Request.InputStream);
                        string jsonString = "";
                        jsonString = sr.ReadToEnd();

                        response = SaveTriageData(jsonString, PatientId, visitPK, locationId, userId);
                        SendResponse(response);
                    }

                    if (Request.QueryString["data"].ToString() == "savepc")
                    {

                        System.IO.StreamReader sr = new System.IO.StreamReader(Request.InputStream);
                        string jsonString = "";
                        jsonString = sr.ReadToEnd();

                        response = SavePresentingComplaintsData(jsonString, PatientId, visitPK, locationId, userId);
                        SendResponse(response);
                    }
                    if (Request.QueryString["data"].ToString() == "saveadx")
                    {

                        System.IO.StreamReader sr = new System.IO.StreamReader(Request.InputStream);
                        string jsonString = "";
                        jsonString = sr.ReadToEnd();

                        response = SaveAdditionalHx(jsonString, PatientId, visitPK, locationId, userId);
                        SendResponse(response);
                    }

                    if (Request.QueryString["data"].ToString() == "savescreening")
                    {

                        System.IO.StreamReader sr = new System.IO.StreamReader(Request.InputStream);
                        string jsonString = "";
                        jsonString = sr.ReadToEnd();

                        response = SaveScreeningData(jsonString, PatientId, visitPK, locationId, userId);
                        SendResponse(response);
                    }

                    if (Request.QueryString["data"].ToString() == "savesystemicreview")
                    {

                        System.IO.StreamReader sr = new System.IO.StreamReader(Request.InputStream);
                        string jsonString = "";
                        jsonString = sr.ReadToEnd();

                        response = SaveSystemicReviewData(jsonString, PatientId, visitPK, locationId, userId);
                        SendResponse(response);
                    }

                    if (Request.QueryString["data"].ToString() == "savemanagementx")
                    {

                        System.IO.StreamReader sr = new System.IO.StreamReader(Request.InputStream);
                        string jsonString = "";
                        jsonString = sr.ReadToEnd();

                        response = SaveManagementxData(jsonString, PatientId, visitPK, locationId, userId);
                        SendResponse(response);
                    }

                }
            }
        }

        private string GetClinicalEnounter(int ptn_pk, int visitPK, int locationId)
        {
            string result = string.Empty;
            try
            {
                IClinicalEncounter clinicalencounter = (IClinicalEncounter)ObjectFactory.CreateInstance("HIVCE.BusinessLayer.BLClinicalEncounter, HIVCE.BusinessLayer");
                HIVCE.Common.Entities.ClinicalEncounter cl = clinicalencounter.GetData(ptn_pk, visitPK, locationId);
                result = SerializerUtil.ConverToJson<HIVCE.Common.Entities.ClinicalEncounter>(cl);
            }
            catch (Exception ex)
            {
                CLogger.WriteLog(ELogLevel.ERROR, "GetClinicalEnounter() exception: " + ex.ToString());
                ResponseType response = new ResponseType() { Success = EnumUtil.GetEnumDescription(Success.False) };
                result = SerializerUtil.ConverToJson<ResponseType>(response);
            }
            finally
            {

            }
            return result;
        }

        private string GetPresentingComplaints(int ptn_pk, int visitPK, int locationId)
        {
            string result = string.Empty;
            try
            {
                IClinicalEncounter clinicalencounter = (IClinicalEncounter)ObjectFactory.CreateInstance("HIVCE.BusinessLayer.BLClinicalEncounter, HIVCE.BusinessLayer");
                HIVCE.Common.Entities.PresentingComplaint obj = clinicalencounter.GetPresentingComplaints(ptn_pk, visitPK, locationId);
                result = SerializerUtil.ConverToJson<HIVCE.Common.Entities.PresentingComplaint>(obj);
                //result = bl.GetPresentingComplaints(ptn_pk, visitPK, locationId);
            }
            catch (Exception ex)
            {
                ///CLogger.WriteLog(ELogLevel.ERROR, "GetAnnouncements() exception: " + ex.ToString());
                ResponseType response = new ResponseType() { Success = EnumUtil.GetEnumDescription(Success.False) };
                result = SerializerUtil.ConverToJson<ResponseType>(response);
            }
            finally
            {

            }
            return result;
        }

        private string GetAddtionalHx(int ptn_pk, int visitPK, int locationId)
        {
            string result = string.Empty;
            try
            {
                IClinicalEncounter clinicalencounter = (IClinicalEncounter)ObjectFactory.CreateInstance("HIVCE.BusinessLayer.BLClinicalEncounter, HIVCE.BusinessLayer");
                HIVCE.Common.Entities.AddtionalHx obj = clinicalencounter.GetAddtionalHx(ptn_pk, visitPK, locationId);

                result = SerializerUtil.ConverToJson<HIVCE.Common.Entities.AddtionalHx>(obj);
                //result = bl.GetAddtionalHx(ptn_pk, visitPK, locationId);
            }
            catch (Exception ex)
            {
                ///CLogger.WriteLog(ELogLevel.ERROR, "GetAnnouncements() exception: " + ex.ToString());
                ResponseType response = new ResponseType() { Success = EnumUtil.GetEnumDescription(Success.False) };
                result = SerializerUtil.ConverToJson<ResponseType>(response);
            }
            finally
            {

            }
            return result;
        }

        private string GetSystemicReview(int ptn_pk, int visitPK, int locationId)
        {
            string result = string.Empty;
            try
            {
                IQCareUtils theUtils = new IQCareUtils();
                //DataSet theDSXML = new DataSet();
                //theDSXML.ReadXml(MapPath("..\\XMLFiles\\AllMasters.con"));
                //DataView theCodeDV = new DataView(theDSXML.Tables["MST_CODE"]);
                DataView theCodeDV = new DataView(oCommonData.getAllMSTCode());
                theCodeDV.RowFilter = "DeleteFlag=0 and Name in ('WHOStageIConditions','WHOStageIIConditions','WHOStageIIICoditions','WHOStageIVConditions','InitiationWHOstage','GeneralConditions','SkinConditions','ENTConditions','ChestLungsConditions','CardiovascularConditions','AbdomenConditions','CNSConditions')";
                DataTable theCodeDT = (DataTable)theUtils.CreateTableFromDataView(theCodeDV);
                DataTable theDT = new DataTable();
                if (theCodeDT.Rows.Count > 0)
                {
                    List<CodeDeCodeTables> WhoStages = new List<CodeDeCodeTables>();
                    foreach (DataRow row in theCodeDT.Rows)
                    {

                        DataView theDV = new DataView(oCommonData.getMSTDecode(Convert.ToInt32(row["CodeId"])));
                        //theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and CodeID=" + row["CodeId"];

                        theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                        List<CodeDeCodeTables> WhoStages1 = new List<CodeDeCodeTables>();


                        WhoStages1 = (from dt in theDT.AsEnumerable()
                                      select new CodeDeCodeTables()
                                      {
                                          CodeId = Convert.ToInt32(row["CodeId"]),
                                          CodeName = Convert.ToString(row["Name"]),
                                          DeCodeId = dt.Field<int>("Id"),
                                          DeCodeName = dt.Field<string>("NAME")
                                      }).ToList();

                        WhoStages.AddRange(WhoStages1);
                    }

                    IClinicalEncounter clinicalencounter = (IClinicalEncounter)ObjectFactory.CreateInstance("HIVCE.BusinessLayer.BLClinicalEncounter, HIVCE.BusinessLayer");
                    HIVCE.Common.Entities.DBSystemicReviewData obj = clinicalencounter.GetSystemicReviewData(ptn_pk, visitPK, locationId);
                    obj.Collections = new List<CodeDeCodeTables>();
                    if (WhoStages.Count > 0)
                    {
                        obj.Collections = WhoStages;
                    }
                    result = SerializerUtil.ConverToJson<HIVCE.Common.Entities.DBSystemicReviewData>(obj);


                }
            }
            catch (Exception ex)
            {
                ///CLogger.WriteLog(ELogLevel.ERROR, "GetAnnouncements() exception: " + ex.ToString());
                ResponseType response = new ResponseType() { Success = EnumUtil.GetEnumDescription(Success.False) };
                result = SerializerUtil.ConverToJson<ResponseType>(response);
            }
            finally
            {

            }
            return result;
        }

        private string GetManagementData(int ptn_pk, int visitPK, int locationId)
        {
            string result = string.Empty;
            try
            {
                IQCareUtils theUtils = new IQCareUtils();
                DataSet theDSXML = new DataSet();
                theDSXML.ReadXml(MapPath("..\\XMLFiles\\AllMasters.con"));
                double age = Convert.ToDouble(Session["patientageinyearmonth"].ToString());
                List<CodeDeCodeTables> collections = new List<CodeDeCodeTables>();

                DataView theCodeDV = new DataView(theDSXML.Tables["MST_CODE"]);
                theCodeDV.RowFilter = "DeleteFlag=0 and Name in ('ShortTermEffects','LongTermEffects','RememberingMedications','KeyPopulation','ARV Eligibility')";
                DataTable theCodeDT = (DataTable)theUtils.CreateTableFromDataView(theCodeDV);
                DataTable theDT = new DataTable();
                if (theCodeDT.Rows.Count > 0)
                {
                    foreach (DataRow row in theCodeDT.Rows)
                    {

                        DataView theDV = new DataView(theDSXML.Tables["MST_DECODE"]);
                        theDV.RowFilter = "DeleteFlag=0 and CodeID=" + row["CodeId"] + "";

                        theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                        List<CodeDeCodeTables> collections1 = new List<CodeDeCodeTables>();


                        collections1 = (from dt in theDT.AsEnumerable()
                                        select new CodeDeCodeTables()
                                        {
                                            CodeId = dt.Field<int>("codeid"),
                                            CodeName = dt.Field<string>("CodeName"),
                                            DeCodeId = dt.Field<int>("Id"),
                                            DeCodeName = dt.Field<string>("NAME")
                                        }).ToList();

                        collections.AddRange(collections1);
                    }
                }

                IClinicalEncounter clinicalencounter = (IClinicalEncounter)ObjectFactory.CreateInstance("HIVCE.BusinessLayer.BLClinicalEncounter, HIVCE.BusinessLayer");
                HIVCE.Common.Entities.Management obj = clinicalencounter.GetManagement(ptn_pk, visitPK, locationId);

                if (obj.DropdownValues.Count > 0)
                {
                    if (collections.Count > 0)
                    {
                        obj.DropdownValues.AddRange(collections);
                    }
                }

                theCodeDV = new DataView(oCommonData.getAllEmployees());
                theCodeDT = (DataTable)theUtils.CreateTableFromDataView(theCodeDV);
                theDT = new DataTable();
                if (theCodeDT.Rows.Count > 0)
                {
                    foreach (DataRow row in theCodeDT.Rows)
                    {
                        UserDesignation ud = new UserDesignation();
                        ud.UserID = Convert.ToInt32(row["userid"].ToString());
                        ud.UserName = row["UserName"].ToString();
                        ud.Designation = row["Designation"].ToString();
                        ud.DeleteFlag = Convert.ToInt32(row["DeleteFlag"].ToString());

                        obj.Signature.Add(ud);
                    }
                }
                result = SerializerUtil.ConverToJson<HIVCE.Common.Entities.Management>(obj);
            }
            catch (Exception ex)
            {
                ///CLogger.WriteLog(ELogLevel.ERROR, "GetAnnouncements() exception: " + ex.ToString());
                ResponseType response = new ResponseType() { Success = EnumUtil.GetEnumDescription(Success.False) };
                result = SerializerUtil.ConverToJson<ResponseType>(response);
            }
            finally
            {

            }
            return result;
        }

        private string GetScreeningData(int ptn_pk, int visitPK, int locationId)
        {
            string result = string.Empty;
            try
            {
                IQCareUtils theUtils = new IQCareUtils();
                double age = Convert.ToDouble(Session["patientageinyearmonth"].ToString());
                List<CodeDeCodeTables> collections = new List<CodeDeCodeTables>();
                DataSet theDSXML = new DataSet();
                theDSXML.ReadXml(MapPath("..\\XMLFiles\\AllMasters.con"));

                DataView theCodeDV = new DataView(oCommonData.getAllMSTCode());
                if (age <= 15)
                {
                    theCodeDV.RowFilter = "DeleteFlag=0 and Name in ('TBICFPaeds','TBFindings','IPT','CXR','TBRegimen','SputumSmear','GeneExpert','SputumDST','IPTAdherence','IPTContraindication','IPTDiscontinued')";
                }
                else
                {
                    theCodeDV.RowFilter = "DeleteFlag=0 and Name in ('TBAssessmentICF','TBFindings','IPT','CXR','TBRegimen','SputumSmear','GeneExpert','SputumDST','IPTAdherence','IPTContraindication','IPTDiscontinued')";
                }
                DataTable theCodeDT = (DataTable)theUtils.CreateTableFromDataView(theCodeDV);
                DataTable theDT = new DataTable();
                if (theCodeDT.Rows.Count > 0)
                {
                    foreach (DataRow row in theCodeDT.Rows)
                    {

                        //DataView theDV = new DataView(theDSXML.Tables["MST_DECODE"]);
                        DataView theDV = new DataView(oCommonData.getMSTDecode(Convert.ToInt32(row["CodeId"])));
                        theDV.RowFilter = "DeleteFlag=0 and CodeID=" + row["CodeId"] + " and Name Not in ('Transferred out')";

                        theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                        List<CodeDeCodeTables> collections1 = new List<CodeDeCodeTables>();


                        collections1 = (from dt in theDT.AsEnumerable()
                                        select new CodeDeCodeTables()
                                        {
                                            CodeId = Convert.ToInt32(row["CodeId"]),
                                            CodeName = Convert.ToString(row["Name"]),
                                            DeCodeId = dt.Field<int>("Id"),
                                            DeCodeName = dt.Field<string>("NAME")
                                        }).ToList();

                        collections.AddRange(collections1);
                    }
                }

                theCodeDV = new DataView(theDSXML.Tables["MST_BLUECODE"]);
                theCodeDV.RowFilter = "DeleteFlag=0 and Name in ('TB Status')";
                theCodeDT = (DataTable)theUtils.CreateTableFromDataView(theCodeDV);
                theDT = new DataTable();
                if (theCodeDT.Rows.Count > 0)
                {

                    foreach (DataRow row in theCodeDT.Rows)
                    {

                        DataView theDV = new DataView(theDSXML.Tables["MST_BLUEDECODE"]);
                        theDV.RowFilter = "DeleteFlag=0 and CodeID=" + row["CodeId"] + "";

                        theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                        List<CodeDeCodeTables> collections1 = new List<CodeDeCodeTables>();


                        collections1 = (from dt in theDT.AsEnumerable()
                                        select new CodeDeCodeTables()
                                        {
                                            CodeId = dt.Field<int>("codeid"),
                                            CodeName = "TBStatus",
                                            DeCodeId = dt.Field<int>("Id"),
                                            DeCodeName = dt.Field<string>("NAME")
                                        }).ToList();

                        collections.AddRange(collections1);
                    }
                }

                IClinicalEncounter clinicalencounter = (IClinicalEncounter)ObjectFactory.CreateInstance("HIVCE.BusinessLayer.BLClinicalEncounter, HIVCE.BusinessLayer");
                HIVCE.Common.Entities.DBScreeningData obj = clinicalencounter.GetScreeningData(ptn_pk, visitPK, locationId);

                obj.Collections = new List<CodeDeCodeTables>();

                if (collections.Count > 0)
                {
                    obj.Collections = collections;
                }
                result = SerializerUtil.ConverToJson<HIVCE.Common.Entities.DBScreeningData>(obj);


            }
            catch (Exception ex)
            {
                ///CLogger.WriteLog(ELogLevel.ERROR, "GetAnnouncements() exception: " + ex.ToString());
                ResponseType response = new ResponseType() { Success = EnumUtil.GetEnumDescription(Success.False) };
                result = SerializerUtil.ConverToJson<ResponseType>(response);
            }
            finally
            {

            }
            return result;
        }

        private string SaveTriageData(string nodeJson, int ptn_pk, int visitPK, int locationId, int userId)
        {
            string result = string.Empty;
            ResponseType ObjResponse = new ResponseType();
            try
            {
                HIVCE.Common.Entities.Triage triage = SerializerUtil.ConverToObject<HIVCE.Common.Entities.Triage>(nodeJson);
                IClinicalEncounter clinicalencounter = (IClinicalEncounter)ObjectFactory.CreateInstance("HIVCE.BusinessLayer.BLClinicalEncounter, HIVCE.BusinessLayer");
                triage.Ptn_pk = ptn_pk;
                triage.Visit_Id = visitPK;
                triage.LocationId = locationId;

                //result = SerializerUtil.ConverToJson<Triage>(triage);
                bool flag = true;
                int visitId;
                flag = clinicalencounter.SaveUpdateTriage(triage, userId, out visitId);

                Session["PatientVisitId"] = visitId.ToString();
                this.hidVId.Value = visitId.ToString();

                if (flag)
                {
                    ObjResponse.Success = EnumUtil.GetEnumDescription(Success.True);
                }
                else
                {
                    ObjResponse.Success = EnumUtil.GetEnumDescription(Success.False);
                }

            }
            catch (Exception ex)
            {

                ObjResponse.Success = EnumUtil.GetEnumDescription(Success.False);
            }
            finally
            {
            }

            result = SerializerUtil.ConverToJson<ResponseType>(ObjResponse);
            return result;
        }

        private string SavePresentingComplaintsData(string nodeJson, int ptn_pk, int visitPK, int locationId, int userId)
        {
            string result = string.Empty;
            ResponseType ObjResponse = new ResponseType();
            try
            {
                HIVCE.Common.Entities.PresentingComplaint obj = SerializerUtil.ConverToObject<HIVCE.Common.Entities.PresentingComplaint>(nodeJson);
                IClinicalEncounter clinicalencounter = (IClinicalEncounter)ObjectFactory.CreateInstance("HIVCE.BusinessLayer.BLClinicalEncounter, HIVCE.BusinessLayer");
                obj.Ptn_pk = ptn_pk;
                obj.Visit_Id = visitPK;
                obj.LocationId = locationId;

                //result = SerializerUtil.ConverToJson<PresentingComplaint>(obj);

                bool flag = clinicalencounter.SaveUpdatePresentingComplaintsData(obj, userId, locationId);
                if (flag)
                {
                    ObjResponse.Success = EnumUtil.GetEnumDescription(Success.True);
                }
                else
                {
                    ObjResponse.Success = EnumUtil.GetEnumDescription(Success.False);
                }

            }
            catch (Exception ex)
            {

                ObjResponse.Success = EnumUtil.GetEnumDescription(Success.False);
            }
            finally
            {
            }

            result = SerializerUtil.ConverToJson<ResponseType>(ObjResponse);
            return result;
        }

        private string SaveAdditionalHx(string nodeJson, int ptn_pk, int visitPK, int locationId, int userId)
        {
            string result = string.Empty;
            ResponseType ObjResponse = new ResponseType();
            try
            {
                HIVCE.Common.Entities.DBAdditionalHx obj = SerializerUtil.ConverToObject<HIVCE.Common.Entities.DBAdditionalHx>(nodeJson);
                IClinicalEncounter clinicalencounter = (IClinicalEncounter)ObjectFactory.CreateInstance("HIVCE.BusinessLayer.BLClinicalEncounter, HIVCE.BusinessLayer");
                obj.Ptn_Pk = ptn_pk;
                obj.Visit_Id = visitPK;
                obj.LocationId = locationId;

                //result = SerializerUtil.ConverToJson<PresentingComplaint>(obj);

                bool flag = clinicalencounter.SaveUpdateAdditionalHxData(obj, userId, locationId);
                if (flag)
                {
                    ObjResponse.Success = EnumUtil.GetEnumDescription(Success.True);
                }
                else
                {
                    ObjResponse.Success = EnumUtil.GetEnumDescription(Success.False);
                }

            }
            catch (Exception ex)
            {

                ObjResponse.Success = EnumUtil.GetEnumDescription(Success.False);
            }
            finally
            {
            }

            result = SerializerUtil.ConverToJson<ResponseType>(ObjResponse);
            return result;
        }

        private string SaveScreeningData(string nodeJson, int ptn_pk, int visitPK, int locationId, int userId)
        {
            string result = string.Empty;
            ResponseType ObjResponse = new ResponseType();
            try
            {

                HIVCE.Common.Entities.DBScreeningData obj = SerializerUtil.ConverToObject<HIVCE.Common.Entities.DBScreeningData>(nodeJson);
                IClinicalEncounter clinicalencounter = (IClinicalEncounter)ObjectFactory.CreateInstance("HIVCE.BusinessLayer.BLClinicalEncounter, HIVCE.BusinessLayer");
                obj.Ptn_Pk = ptn_pk;
                obj.Visit_Id = visitPK;
                obj.LocationId = locationId;

                //result = SerializerUtil.ConverToJson<PresentingComplaint>(obj);

                bool flag = clinicalencounter.SaveUpdateScreeningData(obj, userId, locationId);
                if (flag)
                {
                    ObjResponse.Success = EnumUtil.GetEnumDescription(Success.True);
                }
                else
                {
                    ObjResponse.Success = EnumUtil.GetEnumDescription(Success.False);
                }
            }
            catch (Exception ex)
            {

                ObjResponse.Success = EnumUtil.GetEnumDescription(Success.False);
            }
            finally
            {
            }

            result = SerializerUtil.ConverToJson<ResponseType>(ObjResponse);
            return result;
        }

        private string SaveSystemicReviewData(string nodeJson, int ptn_pk, int visitPK, int locationId, int userId)
        {
            string result = string.Empty;
            ResponseType ObjResponse = new ResponseType();
            try
            {

                HIVCE.Common.Entities.DBSystemicReviewData obj = SerializerUtil.ConverToObject<HIVCE.Common.Entities.DBSystemicReviewData>(nodeJson);
                IClinicalEncounter clinicalencounter = (IClinicalEncounter)ObjectFactory.CreateInstance("HIVCE.BusinessLayer.BLClinicalEncounter, HIVCE.BusinessLayer");
                obj.Ptn_Pk = ptn_pk;
                obj.Visit_Id = visitPK;
                obj.LocationId = locationId;

                bool flag = clinicalencounter.SaveUpdateSystemicReviewData(obj, userId, locationId);
                if (flag)
                {
                    ObjResponse.Success = EnumUtil.GetEnumDescription(Success.True);
                }
                else
                {
                    ObjResponse.Success = EnumUtil.GetEnumDescription(Success.False);
                }
            }
            catch (Exception ex)
            {

                ObjResponse.Success = EnumUtil.GetEnumDescription(Success.False);
            }
            finally
            {
            }

            result = SerializerUtil.ConverToJson<ResponseType>(ObjResponse);
            return result;
        }

        private string SaveManagementxData(string nodeJson, int ptn_pk, int visitPK, int locationId, int userId)
        {
            string result = string.Empty;
            ResponseType ObjResponse = new ResponseType();
            try
            {

                HIVCE.Common.Entities.ManagementDB obj = SerializerUtil.ConverToObject<HIVCE.Common.Entities.ManagementDB>(nodeJson);
                IClinicalEncounter clinicalencounter = (IClinicalEncounter)ObjectFactory.CreateInstance("HIVCE.BusinessLayer.BLClinicalEncounter, HIVCE.BusinessLayer");
                obj.Ptn_pk = ptn_pk;
                obj.Visit_Id = visitPK;
                obj.LocationId = locationId;

                bool flag = clinicalencounter.SaveUpdateManagementxData(obj, userId, locationId);
                if (flag)
                {
                    ObjResponse.Success = EnumUtil.GetEnumDescription(Success.True);
                }
                else
                {
                    ObjResponse.Success = EnumUtil.GetEnumDescription(Success.False);
                }
            }
            catch (Exception ex)
            {

                ObjResponse.Success = EnumUtil.GetEnumDescription(Success.False);
            }
            finally
            {
            }

            result = SerializerUtil.ConverToJson<ResponseType>(ObjResponse);
            return result;
        }

        private string GetZScoreDetails(int ptn_pk, int visitPK, int locationId)
        {
            string result = string.Empty;
            try
            {
                IClinicalEncounter clinicalencounter = (IClinicalEncounter)ObjectFactory.CreateInstance("HIVCE.BusinessLayer.BLClinicalEncounter, HIVCE.BusinessLayer");
                HIVCE.Common.Entities.ClinicalEncounter cl = clinicalencounter.GetData(ptn_pk, visitPK, locationId);
                DataSet ZScoreDS = new DataSet();
                IKNHStaticForms KNHS = (IKNHStaticForms)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHStaticForms, BusinessProcess.Clinical");
                ZScoreDS = KNHS.GetZScoreValues(ptn_pk, Session["PatientSex"].ToString(), cl.PatientVitals.Height);

                ZScoreDetails zs = new ZScoreDetails();
                zs.WFA = new ZScore();
                zs.WFH = new ZScore();
                zs.BMIz = new ZScore();
                if (ZScoreDS.Tables[0].Rows.Count > 0)
                {
                    DataColumnCollection columns = ZScoreDS.Tables[0].Columns;
                    if (columns.Contains("L"))
                    {
                        zs.WFA.L = Convert.ToDouble(ZScoreDS.Tables[0].Rows[0]["L"].ToString());
                        zs.WFA.M = Convert.ToDouble(ZScoreDS.Tables[0].Rows[0]["M"].ToString());
                        zs.WFA.S = Convert.ToDouble(ZScoreDS.Tables[0].Rows[0]["S"].ToString());
                    }
                }

                if (ZScoreDS.Tables[1].Rows.Count > 0)
                {
                    DataColumnCollection columns = ZScoreDS.Tables[1].Columns;
                    if (columns.Contains("L"))
                    {
                        zs.WFH.L = Convert.ToDouble(ZScoreDS.Tables[1].Rows[0]["L"].ToString());
                        zs.WFH.M = Convert.ToDouble(ZScoreDS.Tables[1].Rows[0]["M"].ToString());
                        zs.WFH.S = Convert.ToDouble(ZScoreDS.Tables[1].Rows[0]["S"].ToString());
                    }
                }

                if (ZScoreDS.Tables[2].Rows.Count > 0)
                {
                    DataColumnCollection columns = ZScoreDS.Tables[2].Columns;
                    if (columns.Contains("L"))
                    {
                        zs.BMIz.L = Convert.ToDouble(ZScoreDS.Tables[2].Rows[0]["L"].ToString());
                        zs.BMIz.M = Convert.ToDouble(ZScoreDS.Tables[2].Rows[0]["M"].ToString());
                        zs.BMIz.S = Convert.ToDouble(ZScoreDS.Tables[2].Rows[0]["S"].ToString());
                    }
                }

                ZScoreDS.Dispose();

                result = SerializerUtil.ConverToJson<HIVCE.Common.Entities.ZScoreDetails>(zs);
            }
            catch (Exception ex)
            {
                CLogger.WriteLog(ELogLevel.ERROR, "GetZScoreDetails() exception: " + ex.ToString());
                ResponseType response = new ResponseType() { Success = EnumUtil.GetEnumDescription(Success.False) };
                result = SerializerUtil.ConverToJson<ResponseType>(response);
            }
            finally
            {

            }
            return result;
        }


        private void SendResponse(string data)
        {
            Response.Clear();
            Response.ContentType = "application/json";
            Response.AddHeader("Content-type", "text/json");
            Response.AddHeader("Content-type", "application/json");
            Response.Write(data);
            Response.End();
        }
    }
}