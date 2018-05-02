using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HIVCE.Common;
using HIVCE.Common.Entities;
using Application.Presentation;
using System.Data;
using System.Configuration;
using HIVCE.BusinessLayer;
using Interface.HIVCE;

namespace HIVCE.Presentation
{
    public partial class AlcoholDepressionScreening : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int PatientId = 0;
            int visitPK = 0;
            if (!IsPostBack)
            {
                if (ConfigurationManager.AppSettings["IsDevelopment"].ToString() == "false")
                {
                    if (Session["AppLocation"] == null)
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
                }
                if (!object.Equals(Session["PatientVisitId"], null))
                {
                    visitPK = Convert.ToInt32(Session["PatientVisitId"]);
                }

                if (!object.Equals(Request.QueryString["add"], null))
                {
                    if (Request.QueryString["add"].ToString() == "0")
                    {
                        visitPK = 0;
                        Session.Remove("PatientVisitId");
                    }
                }
                if (!object.Equals(Session["PatientAge"], null))
                {
                    this.hidDOB.Value = Session["PatientAge"].ToString();
                }
                if (!object.Equals(Request.QueryString["data"], null))
                {
                    string response = string.Empty;
                    if (!object.Equals(Request.QueryString["data"], null))
                    {

                        if (Request.QueryString["data"].ToString() == "getdata")
                        {
                            response = GetDisclosurestatus();
                            SendResponse(response);
                        }
                    }
                    if (!object.Equals(Request.QueryString["data"], null))
                    {

                        if (Request.QueryString["data"].ToString() == "getadsdata")
                        {
                            response = GetAlcoholDepressionScreening(Convert.ToInt32(PatientId), visitPK);
                            SendResponse(response);
                        }
                    }

                    if (Request.QueryString["data"].ToString() == "save")
                    {

                        System.IO.StreamReader sr = new System.IO.StreamReader(Request.InputStream);
                        string jsonString = "";
                        jsonString = sr.ReadToEnd();

                        response = SaveData(jsonString, PatientId, visitPK);
                        SendResponse(response);
                    }
                }
            }
        }

        private string GetDisclosurestatus()
        {
            string result = string.Empty;
            try
            {
                IQCareUtils theUtils = new IQCareUtils();
                DataSet theDSXML = new DataSet();
                theDSXML.ReadXml(MapPath("..\\XMLFiles\\AllMasters.con"));
                DataView theCodeDV = new DataView(theDSXML.Tables["MST_CODE"]);
                theCodeDV.RowFilter = "DeleteFlag=0 and Name in ('DisclosureStatus')";
                DataTable theCodeDT = (DataTable)theUtils.CreateTableFromDataView(theCodeDV);
                DataTable theDT = new DataTable();

                List<CodeDeCodeTables> Disclosures = new List<CodeDeCodeTables>();
                List<CodeDeCodeTables> Disclosures1 = new List<CodeDeCodeTables>();
                if (theCodeDT.Rows.Count > 0)
                {
                    foreach (DataRow row in theCodeDT.Rows)
                    {

                        DataView theDV = new DataView(theDSXML.Tables["MST_DECODE"]);
                        theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and CodeID=" + row["CodeId"];

                        theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);

                        Disclosures1 = new List<CodeDeCodeTables>();

                        Disclosures1 = (from dt in theDT.AsEnumerable()
                                      select new CodeDeCodeTables()
                                      {
                                          CodeId = dt.Field<int>("codeid"),
                                          CodeName = dt.Field<string>("CodeName"),
                                          DeCodeId = dt.Field<int>("Id"),
                                          DeCodeName = dt.Field<string>("NAME")
                                      }).ToList();

                        Disclosures.AddRange(Disclosures1);
                    }

                    
                }
                if (theDSXML.Tables["Mst_PMTCTDecode"] != null)
                {

                    DataView theDVDecode = new DataView(theDSXML.Tables["Mst_PMTCTDecode"]);
                    theDVDecode.RowFilter = "CodeName='DisclosedHIVStatusTo' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                    theDVDecode.Sort = "SRNo";
                    if (theDVDecode.Table != null)
                    {
                        DataTable theDTCode = new DataTable();
                        theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                        Disclosures1 = new List<CodeDeCodeTables>();


                        Disclosures1 = (from dt in theDTCode.AsEnumerable()
                                        select new CodeDeCodeTables()
                                        {
                                            CodeId = dt.Field<int>("codeid"),
                                            CodeName = dt.Field<string>("CodeName"),
                                            DeCodeId = dt.Field<int>("Id"),
                                            DeCodeName = dt.Field<string>("NAME")
                                        }).ToList();

                        Disclosures.AddRange(Disclosures1);
                    }
                }

                result = SerializerUtil.ConverToJson<List<CodeDeCodeTables>>(Disclosures);
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

        private string GetAlcoholDepressionScreening(int ptn_pk, int visitPK)
        {
            string result = string.Empty;
            try
            {
                IAlcoholDepressionScreening ads= (IAlcoholDepressionScreening)ObjectFactory.CreateInstance("HIVCE.BusinessLayer.BLAlcoholDepressionScreening, HIVCE.BusinessLayer");
                HIVCE.Common.Entities.AlcoholDepressionScreeningObj objTP = ads.GetAlcoholDepressionScreening(ptn_pk, visitPK);
                result = SerializerUtil.ConverToJson<AlcoholDepressionScreeningObj>(objTP);
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

        private string SaveData(string nodeJson, int ptn_pk, int visitPK)
        {
            string result = string.Empty;
            ResponseType ObjResponse = new ResponseType();
            try
            {
                HIVCE.Common.Entities.AlcoholDepressionScreeningObj adcObj = SerializerUtil.ConverToObject<HIVCE.Common.Entities.AlcoholDepressionScreeningObj>(nodeJson);
                IAlcoholDepressionScreening blObj = (IAlcoholDepressionScreening)ObjectFactory.CreateInstance("HIVCE.BusinessLayer.BLAlcoholDepressionScreening, HIVCE.BusinessLayer");
                adcObj.Ptn_pk = ptn_pk;
                adcObj.Visit_Id = visitPK;

                bool flag = blObj.SaveUpdateAlcoholDepressionScreening(adcObj);
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