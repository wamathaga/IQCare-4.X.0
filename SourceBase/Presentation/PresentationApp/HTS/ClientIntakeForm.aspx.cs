using System;
using System.Data;
using Application.Common;
using Application.Presentation;
using Entities.Common;
using HIVCE.Common;
using Interface.Clinical;
using Interface.Service;

namespace PresentationApp.HTS
{
    public partial class ClientIntakeForm : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int PatientId = 0;
            int visitPK = 0;
            int locationId = 0;
            int userId = 0;
            string response = string.Empty;
            bool SaveResponse = false;

            if (!IsPostBack)
            {
                if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
                {
                    Response.Redirect("~/frmlogin.aspx", true);
                }

                if (!object.Equals(Session["PatientId"], null))
                {
                    PatientId = Convert.ToInt32(Session["PatientId"]);
                    if (PatientId == 0)
                        Response.Redirect("~/ClinicalForms/frmPatient_Home.aspx", true);

                    //this.hidPID.Value = PatientId.ToString();
                }
            }

            if (Request.QueryString["data"] == "GET" || Request.QueryString["data"] == "")
            {
                response = GetCIFData(Convert.ToInt32(PatientId));
                SendResponse(response);
            }

            if (Request.QueryString["data"] == "SAVE")
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(Request.InputStream);
                string jsonString = sr.ReadToEnd();

                response = SaveCIFData(jsonString, Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["UserID"]));
                SendResponse(response);
            }
        }

        private string GetCIFData(int PatientId)
        {
            string result = string.Empty;
            IQCareUtils theUtils = new IQCareUtils();
            ICommonData iComm = (ICommonData)ObjectFactory.CreateInstance("BusinessProcess.Service.BCommonData, BusinessProcess.Service");
            try
            {

                IHTSClientIntake clientIntake;
                clientIntake = (IHTSClientIntake)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BHTSClientIntake, BusinessProcess.Clinical");
                HTSClientIntake CI = clientIntake.GetClientIntakeFormData(PatientId, 0);

                DataView theCodeDV = new DataView(iComm.getAllProvinceByCountry(160));
                DataTable theCodeDT = theUtils.CreateTableFromDataView(theCodeDV);

                if (theCodeDT.Rows.Count > 0)
                {
                    foreach (DataRow row in theCodeDT.Rows)
                    {
                        States st = new States();
                        st.StateID = Convert.ToInt32(row["ID"].ToString());
                        st.StateName = row["Name"].ToString();
                        CI.AllStates.Add(st);
                    }
                }

                theCodeDV = new DataView(iComm.getAllDistrict());
                theCodeDT = theUtils.CreateTableFromDataView(theCodeDV);

                if (theCodeDT.Rows.Count > 0)
                {
                    foreach (DataRow row in theCodeDT.Rows)
                    {
                        LGAs lg = new LGAs();
                        lg.LgaID = Convert.ToInt32(row["ID"].ToString());
                        lg.LgaName = row["Name"].ToString();
                        CI.AllLGAs.Add(lg);
                    }
                }

                theCodeDV = new DataView(iComm.getAllEmployees());
                theCodeDT = theUtils.CreateTableFromDataView(theCodeDV);

                if (theCodeDT.Rows.Count > 0)
                {
                    foreach (DataRow row in theCodeDT.Rows)
                    {
                        UserDesignation ug = new UserDesignation();
                        ug.UserID = Convert.ToInt32(row["UserID"].ToString());
                        ug.UserName = row["UserName"].ToString();
                        CI.UserList.Add(ug);
                    }
                }

                result = SerializerUtil.ConverToJson<HTSClientIntake>(CI);
            }
            catch (Exception ex)
            {
                CLogger.WriteLog(ELogLevel.ERROR, "GetClientIntakeFormData() exception: " + ex.ToString());
                ResponseType response = new ResponseType() { Success = EnumUtil.GetEnumDescription(Success.False) };
                result = SerializerUtil.ConverToJson<ResponseType>(response);
            }
            return result;
        }

        private string SaveCIFData(string jsonString, int patientID, int userID)
        {
            bool responseStatus;
            ResponseType ObjResponse = new ResponseType();
            try
            {
                // HTSClientIntake CIform = SerializerUtil.ConverToObject<HTSClientIntake>(jsonString);
                IHTSClientIntake clientIntake;
                clientIntake = (IHTSClientIntake)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BHTSClientIntake, BusinessProcess.Clinical");
                responseStatus = clientIntake.SaveClientIntakeFormData(jsonString, patientID, userID);
                ObjResponse.Success = responseStatus.ToString();
            }
            catch (Exception ex)
            {
                CLogger.WriteLog(ELogLevel.ERROR, "GetClientIntakeFormData() exception: " + ex.ToString());
                ResponseType response = new ResponseType() { Success = EnumUtil.GetEnumDescription(Success.False) };
                responseStatus = Convert.ToBoolean(SerializerUtil.ConverToJson<ResponseType>(response.ToString()));
                ObjResponse.Success = responseStatus.ToString();
            }
            string result = SerializerUtil.ConverToJson<ResponseType>(ObjResponse);
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

