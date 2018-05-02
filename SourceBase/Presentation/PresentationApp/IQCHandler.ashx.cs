using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using Interface.Security;
using System.Data;
using Application.Presentation;
using BusinessProcess.Administration;
using BusinessProcess.Security;
using Interface.Security;
using System.Collections;
using Application.Common;

namespace PresentationApp
{
    /// <summary>
    /// Summary description for IQCHandler
    /// </summary>
    public class IQCHandler : IHttpHandler
    {
        string rUserID, rUserName, rPassword = string.Empty;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            context.Response.AppendHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
            context.Response.AppendHeader("Access-Control-Allow-Headers", "x-requested-with");

            string rRequestType = string.Empty;
            if (context.Request["RequestType"] != null)
                rRequestType = context.Request["RequestType"].ToString();

            if (context.Request["UserID"] != null)
                rUserID = context.Request["UserID"].ToString();

            if (context.Request["UserName"] != null)
                rUserName = context.Request["UserName"].ToString();

            if (context.Request["Password"] != null)
                rPassword = context.Request["Password"].ToString();

            if (rRequestType.Equals("FACLIST"))
                context.Response.Write(JSONHelper.ToJSON(GetFacilityList("","")));

            if (rRequestType.Equals("LGN"))
                context.Response.Write(JSONHelper.ToJSON(GetFacilityList("", "")));

            if (String.IsNullOrEmpty(rRequestType))
                context.Response.Write("Invalid request, Please try again.");

        }

        private string ValidateLogin(string rUserName, string rPwd, int rFacilityID)
        {
            string jsonString = string.Empty;
            try
            {
                IUser LoginManager = (IUser)ObjectFactory.CreateInstance("BusinessProcess.Security.BUser, BusinessProcess.Security");
                DataSet theDS = LoginManager.GetUserCredentials(rUserName, rFacilityID, 1);
               

                if (theDS.Tables.Count > 0)
                {
                    int FacilityExist = 1;

                    if (theDS.Tables[5].Rows.Count > 0)
                    {
                        DataView theDV = new DataView();
                        FacilityExist = 0;
                        foreach (DataRow theDR in theDS.Tables[5].Rows)
                        {
                            if (Convert.ToInt32(theDR["GroupId"]) > 1)
                            {
                                theDV = new DataView(theDS.Tables[1]);
                                theDV.RowFilter = "FacilityID= " + rFacilityID;
                                if (theDV.ToTable().Rows.Count > 0)
                                {
                                    FacilityExist = 1;
                                }
                            }
                            else if (Convert.ToInt32(theDR["GroupId"]) == 1)
                            {
                                FacilityExist = 1;
                            }
                        }
                    }

                    if (FacilityExist == 0)
                        jsonString = "AccessDenied";
                

                    if (theDS.Tables[1].Rows.Count == 0)
                        jsonString = "Invalid information";
                
                    Utility theUtil = new Utility();
                    if (theDS.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToString(theDS.Tables[0].Rows[0]["Password"]) != theUtil.Encrypt(rPassword.Trim().ToString()))
                            jsonString ="Password mismatch";
                    }
                } 
                
            }
            catch (Exception ex)
            {
                jsonString = "Error occured. Please try again later";
            }
            return jsonString;
        }

        private string GetFacilityList(string userID, string rFlagType)
        {
            IUser UserManager = (IUser)ObjectFactory.CreateInstance("BusinessProcess.Security.BUser,BusinessProcess.Security");
            DataTable theDT = UserManager.GetFacilityList();
            String jsonString = string.Empty;

            if (theDT != null && theDT.Rows.Count > 0)
            {
                if (theDT.Rows.Count > 0)
                    jsonString = DataTableToJSONWithJavaScriptSerializer(theDT);
            }
            return jsonString;
        }

        protected string DataTableToJSONWithJavaScriptSerializer(DataTable table)
        {
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in table.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in table.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            return jsSerializer.Serialize(parentRow);
        }
        
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        
    }

    public static class JSONHelper
    {
        public static string ToJSON(this object obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(obj);
        }

        public static string ToJSON(this object obj, int recursionDepth)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.RecursionLimit = recursionDepth;
            return serializer.Serialize(obj);
        }
    }
}