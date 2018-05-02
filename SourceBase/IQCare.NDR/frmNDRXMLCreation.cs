using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Windows.Forms;
using Interface.NDR;
using System.Xml.Schema;
using System.IO;
using Application.Presentation;
using Application.Common;
using System.Configuration;
using BusinessProcess.NDR;
using System.Threading.Tasks;
using System.Threading;

namespace IQCare.NDR
{
    public partial class frmNDRXMLCreation : Form
    {
        DataTable dtValues = new DataTable();

        DataSet data = new DataSet();

        public frmNDRXMLCreation()
        {
            InitializeComponent();
        }

        private void btnGenerateNDRXml_Click(object sender, EventArgs e)
        {
            try
            {
                string path = ConfigurationSettings.AppSettings["NDRXMLS"].ToString();
                DirectoryInfo fi = new DirectoryInfo(path);
                if (!fi.Exists)
                {
                    MessageBox.Show("NDR xml(s) path does not exits. Please change the NDR xml(s) path in config file.", "IQCare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                btnGenerateNDRXml.Enabled = false;
                // Set Minimum to 1 to represent the first file being copied.
                pBar1.Minimum = 1;
                // Set Maximum to the total number of files to copy.
                pBar1.Maximum = 1;
                // Set the initial value of the ProgressBar.
                pBar1.Value = 1;
                // Set the Step property to a value of 1 to represent each file being copied.
                pBar1.Step = 1;

                INDRGeneration ndrGeneration;
                DataSet ds = new DataSet();
                ndrGeneration = new BNDRGeneration();
                ds = ndrGeneration.GetPatientDetails(Convert.ToInt32(ConfigurationSettings.AppSettings["AppLocationId"].ToString()));
                GenerateXML(ds);
            }
            catch (System.IO.DirectoryNotFoundException exd)
            {
                MessageBox.Show(exd.Message.ToString(), "IQCare", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //CLogger.WriteLog(ELogLevel.ERROR, exd.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "IQCare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //CLogger.WriteLog(ELogLevel.ERROR, ex.ToString());
            }
        }

        private string GetConceptValue(DataTable dt, string code, string value)
        {
            string result = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(value))
                {
                    value = value.Replace("-", " ");

                    bool containsColumn = dt.AsEnumerable().Where(o => o.Field<String>("Value_Set_Code").ToUpper() == code.ToUpper()
                         && o.Field<String>("Code_description").Contains(value.Trim())).Any();

                    if (containsColumn)
                    {
                        DataRow query =
                            (from concept in dt.AsEnumerable()
                             where concept.Field<String>("Value_Set_Code").ToUpper() == code.ToUpper()
                             && concept.Field<String>("Code_description").Contains(value.Trim())
                             select concept).FirstOrDefault();

                        result = query.Field<string>("Code");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "IQCare.NDR.GetConceptValue", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //CLogger.WriteLog(ELogLevel.ERROR, ex.ToString());
            }
            return result;
        }

        public static bool IsValidXml(string xmlFilePath, string xsdFilePath)
        {
            var xdoc = XDocument.Load(xmlFilePath);
            var schemas = new XmlSchemaSet();
            schemas.Add("http://www.w3.org/2001/XMLSchema", xsdFilePath);

            try
            {
                xdoc.Validate(schemas, null);
            }
            catch (XmlSchemaValidationException)
            {
                return false;
            }

            return true;
        }

        public static bool IsValidXml_1(string xmlFilePath, string xsdFilePath)
        {
            Boolean result = true;

            /*var xdoc = XDocument.Load(xmlFilePath);
            var schemas = new XmlSchemaSet();
            schemas.Add("http://www.w3.org/2001/XMLSchema", xsdFilePath);

            
            xdoc.Validate(schemas, (sender, e) =>
            {
                result = false;
            });
            */
            XmlSchemaSet schemas = new XmlSchemaSet();
            schemas.Add("", xsdFilePath);

            XDocument custOrdDoc = XDocument.Load(xmlFilePath);
            bool errors = false;
            custOrdDoc.Validate(schemas, (o, e) =>
            {
                Console.WriteLine("{0}", e.Message);
                errors = true;
            });
            //Console.WriteLine("custOrdDoc {0}", errors ? "did not validate" : "validated");

            return errors;
        }

        public void GenerateXML(DataSet ds)
        {
            try
            {
                data = ds;
                dtValues = (DataTable)ds.Tables[0];
                DataTable dtPatients = new DataTable();
                dtPatients = (DataTable)ds.Tables[1];
                if (dtPatients.Rows.Count > 0)
                {
                    // Display the ProgressBar control.
                    pBar1.Visible = true;
                    // Set Minimum to 1 to represent the first file being copied.
                    pBar1.Minimum = 1;
                    // Set Maximum to the total number of files to copy.
                    pBar1.Maximum = dtPatients.Rows.Count;
                    // Set the initial value of the ProgressBar.
                    pBar1.Value = 1;
                    // Set the Step property to a value of 1 to represent each file being copied.
                    pBar1.Step = 1;

                    foreach (DataRow row in dtPatients.Rows)
                    {
                        string messageStatusCode = row["MessageStatusCode"].ToString();
                        XElement container = new XElement("Container",
                           new XElement(MessageHeader(messageStatusCode)),
                           new XElement(IndividualReport(dtValues, row)));
                        int patentId = Convert.ToInt32(row["Ptn_Pk"].ToString());
                        GenerateXMlFile(container, patentId);
                        // Perform the increment on the ProgressBar.
                        pBar1.PerformStep();

                        Thread.Sleep(4000);
                    }
                    MessageBox.Show("XML(s) files generated successfully.", "IQCare.NDR.GenerateXML", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Unable to process - No Patient(s) found", "IQCare.NDR.GenerateXML", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                btnGenerateNDRXml.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "IQCare.NDR.GenerateXML", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //CLogger.WriteLog(ELogLevel.ERROR, ex.ToString());
            }
        }

        private void GenerateXMlFile(XElement container, int patientid)
        {
            try
            {
                string fileName = "CCFN_" + ConfigurationSettings.AppSettings["AppLocationId"].ToString() + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + DateTime.Now.ToString("HHmmss") + ".xml";
                string path = ConfigurationSettings.AppSettings["NDRXMLS"].ToString();
                string xsdPath = ConfigurationSettings.AppSettings["XSDPATH"].ToString();
                container.Save(@path + fileName);
                bool errors = IsValidXml(@path + fileName, @xsdPath);

                if (!errors)
                {
                    FileInfo fi = new FileInfo(@path + fileName);
                    fi.Delete();

                    fileName = "CCFN_" + ConfigurationSettings.AppSettings["AppLocationId"].ToString() + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + DateTime.Now.ToString("HHmmss") + "_ERROR.xml";
                    container.Save(@path + fileName);

                }
                else
                {
                    INDRGeneration ndrGeneration;
                    DataSet ds = new DataSet();
                    ndrGeneration = new BNDRGeneration();
                    ndrGeneration.SavePatientDetails(patientid);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "IQCare.NDR.GenerateXMlFile", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //CLogger.WriteLog(ELogLevel.ERROR, ex.ToString());
            }

        }

        private XElement MessageHeader(string messageStatusCode)
        {
            DateTime dt = new DateTime();
            dt = DateTime.Now;
            Guid g = Guid.NewGuid();
            string strHostName = System.Net.Dns.GetHostName();
            XElement header =
            new XElement("MessageHeader",
                new XElement("MessageStatusCode", messageStatusCode),
                new XElement("MessageCreationDateTime", dt.ToString("yyyy-MM-ddTHH:mm:ss.ff")),
                new XElement("MessageSchemaVersion", "1.2"),
                new XElement("MessageUniqueID", g.ToString()),
                new XElement("MessageSendingOrganization",
                    new XElement("FacilityName", "Catholic Caritas Foundation of Nigeria"),
                    new XElement("FacilityID", "CCFN"),
                    new XElement("FacilityTypeCode", "IP")
                    )
            );
            return header;
        }

        private XElement IndividualReport(DataTable dtValues, DataRow row)
        {
            XElement individualreport = new XElement("IndividualReport");
            try
            {
                individualreport.Add(PatientDemographics(dtValues, row));
                individualreport.Add(Condition(dtValues, row));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "IQCare.NDR.IndividualReport", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //CLogger.WriteLog(ELogLevel.ERROR, ex.ToString());
            }
            return individualreport;
        }

        private XElement PatientDemographics(DataTable dtValues, DataRow row)
        {
            XElement patientdemographics = new XElement("PatientDemographics");
            try
            {
                patientdemographics.Add(new XElement("PatientIdentifier", row["PatientEnrollment"].ToString()));

                XElement treatmentfacility =
                   new XElement("TreatmentFacility",
                       new XElement("FacilityName", row["Facility-Satellite Name"].ToString()),
                       new XElement("FacilityID", row["LocationID"].ToString()),
                       new XElement("FacilityTypeCode", "FAC")
                   );
                patientdemographics.Add(treatmentfacility);

                XElement otherpatientidentifiers =
                    new XElement("OtherPatientIdentifiers",
                        new XElement("Identifier",
                            new XElement("IDNumber", row["IQCareReference"].ToString()),
                            new XElement("IDTypeCode", "IP")
                            )
                     );
                patientdemographics.Add(otherpatientidentifiers);

                DateTime DOB = DateTime.Parse(row["DateofBirth"].ToString());
                string sex = row["Sex"].ToString().ToUpper() == "MALE" ? "M" : "F";
                patientdemographics.Add(new XElement("PatientDateOfBirth", DOB.ToString("yyyy-MM-dd")));
                patientdemographics.Add(new XElement("PatientSexCode", sex));
                patientdemographics.Add(new XElement("PatientDeceasedIndicator", Convert.ToBoolean(row["CareEnded"].ToString())));
                patientdemographics.Add(new XElement("PatientPrimaryLanguageCode", "NIDB"));
                string educationVal = string.Empty;
                if (!DBNull.Value.Equals(row["EducationLevel"]))
                {
                    educationVal = GetConceptValue(dtValues, "EDUCATIONAL_LEVEL", row["EducationLevel"].ToString());
                    patientdemographics.Add(new XElement("PatientEducationLevelCode", educationVal.ToString()));
                }
                patientdemographics.Add(new XElement("PatientOccupationCode", "EMP"));
                string maritalstatus = string.Empty;
                if (!DBNull.Value.Equals(row["MaritalStatus"]))
                {
                    maritalstatus = GetConceptValue(dtValues, "MARITAL_STATUS", row["MaritalStatus"].ToString());
                    patientdemographics.Add(new XElement("PatientMaritalStatusCode", maritalstatus));
                }

                string nigeriaorigin = string.Empty;
                if (!DBNull.Value.Equals(row["Region"]))
                {
                    nigeriaorigin = GetConceptValue(dtValues, "STATES", row["Region"].ToString());
                    patientdemographics.Add(new XElement("StateOfNigeriaOriginCode", nigeriaorigin));
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "IQCare.NDR.PatientDemographics", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //CLogger.WriteLog(ELogLevel.ERROR, ex.ToString());
            }
            return patientdemographics;

        }

        private XElement Condition(DataTable dtValues, DataRow row)
        {
            XElement condition = new XElement("Condition");
            try
            {
                condition.Add(new XElement("ConditionCode", "86406008"));

                XElement programarea =
                   new XElement("ProgramArea",
                       new XElement("ProgramAreaCode", "HIV"));
                condition.Add(programarea);
                string lga = string.Empty;
                string province = string.Empty;
                if (!DBNull.Value.Equals(row["District"]))
                {
                    lga = GetConceptValue(dtValues, "LGA", row["District"].ToString());
                }
                if (!DBNull.Value.Equals(row["Province"]))
                {
                    province = GetConceptValue(dtValues, "STATES", row["Province"].ToString());
                }
                XElement patientaddress =
                  new XElement("PatientAddress",
                      new XElement("AddressTypeCode", "H"),
                      new XElement("WardVillage", row["Village"].ToString()),
                    //new XElement("Town", "HIV"),
                      new XElement("LGACode", lga),
                      new XElement("StateCode", province),
                      new XElement("CountryCode", "NGA"),
                      new XElement("PostalCode", row["PostalCode"].ToString())
                    //new XElement("OtherAddressInformation", row["PatientAddress"].ToString())
                      );
                condition.Add(patientaddress);

                condition.Add(GetCommonQuestions(dtValues, row));
                condition.Add(GetConditionSpecificQuestions(dtValues, row));
                condition.Add(GetEncounters(dtValues, row));
                GetLaboratoryReport(dtValues, row, condition);
                GetRegimen(dtValues, row, condition);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "IQCare.NDR.Condition", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //CLogger.WriteLog(ELogLevel.ERROR, ex.ToString());
            }
            return condition;

        }

        private XElement GetCommonQuestions(DataTable dtValues, DataRow row)
        {
            XElement commonquestions = new XElement("CommonQuestions");
            try
            {
                if (!DBNull.Value.Equals(row["ExistingHosp_Clinc"]))
                {
                    if (!string.IsNullOrEmpty(row["ExistingHosp_Clinc"].ToString()))
                    {
                        commonquestions.Add(new XElement("HospitalNumber", row["ExistingHosp_Clinc"].ToString()));
                    }
                }

                XElement diagnosisfacility =
                   new XElement("DiagnosisFacility",
                       new XElement("FacilityName", row["ReferredClinicFrom"].ToString()),
                       new XElement("FacilityID", row["ReferredClinicFromId"].ToString()),
                       new XElement("FacilityTypeCode", "FAC")
                       );
                commonquestions.Add(diagnosisfacility);
                DataTable commondata = (DataTable)data.Tables[2];
                int patientId = Convert.ToInt32(row["Ptn_Pk"].ToString());
                IEnumerable<DataRow> query =
                    (from common in commondata.AsEnumerable()
                     where common.Field<Int32>("Ptn_Pk") == patientId
                     select common);

                //commonquestions.Add(new XElement("DateOfFirstReport", ""));
                //commonquestions.Add(new XElement("DateOfLastReport", ""));

                string hivDate = string.Empty;
                string diedstatus = string.Empty;
                string pregnancystatus = string.Empty;

                foreach (DataRow qrow in query)
                {
                    if (!DBNull.Value.Equals(qrow["datehivdiagnosis"]))
                    {
                        hivDate = qrow["datehivdiagnosis"].ToString();
                    }
                    if (!DBNull.Value.Equals(qrow["DiedStatus"]))
                    {
                        diedstatus = qrow["DiedStatus"].ToString();
                    }
                    if (!DBNull.Value.Equals(qrow["PregnancyStatus"]))
                    {
                        pregnancystatus = qrow["PregnancyStatus"].ToString();
                    }
                }

                if (!String.IsNullOrEmpty(hivDate))
                {
                    DateTime DThivDate = DateTime.Parse(hivDate.ToString());
                    commonquestions.Add(new XElement("DiagnosisDate", DThivDate.ToString("yyyy-MM-dd")));
                }

                if (!String.IsNullOrEmpty(diedstatus))
                {
                    commonquestions.Add(new XElement("PatientDieFromThisIllness", diedstatus));
                }
                if (!String.IsNullOrEmpty(pregnancystatus))
                {
                    commonquestions.Add(new XElement("PatientPregnancyStatusCode", pregnancystatus));
                }
                //if (!String.IsNullOrEmpty(hivDate))
                //{
                //    commonquestions.Add(new XElement("EstimatedDeliveryDate", ""));
                //}
                if (!DBNull.Value.Equals(row["Age"]))
                {
                    commonquestions.Add(new XElement("PatientAge", row["Age"].ToString()));
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "IQCare.NDR.GetCommonQuestions", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //CLogger.WriteLog(ELogLevel.ERROR, ex.ToString());
            }
            return commonquestions;
        }

        private XElement GetEncounters(DataTable dtValues, DataRow row)
        {
            XElement encounters = new XElement("Encounters");
            try
            {
                int patientId = Convert.ToInt32(row["Ptn_Pk"].ToString());
                DataTable encounter = new DataTable();
                encounter = (DataTable)data.Tables[3];
                IEnumerable<DataRow> filterdata =
                    (from fdata in encounter.AsEnumerable()
                     where fdata.Field<Int32>("Ptn_Pk") == patientId
                     select fdata);

                List<DateTime> distinctDate =
                                (from fdata in filterdata.AsEnumerable()
                                 select
                                 Convert.ToDateTime(fdata.Field<DateTime>("VisitDate").ToString("yyyy-MM-dd"))
                                 ).Distinct().ToList();

                string VisitID = string.Empty;
                string VisitDate = string.Empty;
                string DurationOnArt = string.Empty;
                string Weight = string.Empty;
                string BloodPressure = string.Empty;
                string PatientFamilyPlanningCode = string.Empty;
                string PatientFamilyPlanningMethodCode = string.Empty;
                string FunctionalStatus = string.Empty;
                string WHOClinicalStage = string.Empty;
                string TBStatus = string.Empty;
                string ARVRegimen = string.Empty;
                string CotrimoxazoleDose = string.Empty;
                string INHDose = string.Empty;
                string CD4 = string.Empty;
                string CD4TestDate = string.Empty;
                string NextAppointmentDate = string.Empty;

                foreach (DateTime date in distinctDate)
                {

                    foreach (DataRow drow in filterdata.Where(o => Convert.ToDateTime(o.Field<DateTime>("VisitDate")) == date)
                        .OrderBy(o => o.Field<Int32>("Visit_Id")).ToList())
                    {
                        VisitID = drow["Visit_Id"].ToString();
                        DateTime VD = DateTime.Parse(drow["VisitDate"].ToString());
                        VisitDate = VD.ToString("yyyy-MM-dd");

                        if (!DBNull.Value.Equals(drow["DurationOnART"]))
                        {
                            DurationOnArt = drow["DurationOnART"].ToString();
                        }
                        if (!DBNull.Value.Equals(drow["Weight"]))
                        {
                            Weight = drow["Weight"].ToString();
                            Weight = Math.Round(Convert.ToDecimal(Weight)).ToString();
                        }
                        if (!DBNull.Value.Equals(drow["BloodPressure"]))
                        {
                            BloodPressure = drow["BloodPressure"].ToString();
                        }
                        if (!DBNull.Value.Equals(drow["PatientFamilyPlanningCode"]))
                        {
                            PatientFamilyPlanningCode = drow["PatientFamilyPlanningCode"].ToString();
                        }
                        if (!DBNull.Value.Equals(drow["PatientFamilyPlanningMethodCode"]))
                        {
                            PatientFamilyPlanningMethodCode = drow["PatientFamilyPlanningMethodCode"].ToString();
                        }
                        if (!DBNull.Value.Equals(drow["FunctionalStatus"]))
                        {
                            FunctionalStatus = drow["FunctionalStatus"].ToString();
                        }
                        if (!DBNull.Value.Equals(drow["WHOClinicalStage"]))
                        {
                            WHOClinicalStage = drow["WHOClinicalStage"].ToString();
                        }
                        if (!DBNull.Value.Equals(drow["TBStatus"]))
                        {
                            TBStatus = drow["TBStatus"].ToString();
                        }
                        if (!DBNull.Value.Equals(drow["ARVRegimen"]))
                        {
                            ARVRegimen = drow["ARVRegimen"].ToString();
                        }
                        if (!DBNull.Value.Equals(drow["CotrimoxazoleDose"]))
                        {
                            CotrimoxazoleDose = drow["CotrimoxazoleDose"].ToString();
                        }
                        if (!DBNull.Value.Equals(drow["INHDose"]))
                        {
                            INHDose = drow["INHDose"].ToString();
                        }
                        if (!DBNull.Value.Equals(drow["CD4"]))
                        {
                            CD4 = drow["CD4"].ToString();
                        }
                        //if (!DBNull.Value.Equals(drow["CD4TestDate"]))
                        //{
                        //    CD4TestDate = drow["CD4TestDate"].ToString();
                        //}
                        if (!DBNull.Value.Equals(drow["NextAppointmentDate"]))
                        {
                            NextAppointmentDate = drow["NextAppointmentDate"].ToString();
                        }

                    }

                    XElement hivencounter = new XElement("HIVEncounter");
                    hivencounter.Add(new XElement("VisitID", VisitID));
                    hivencounter.Add(new XElement("VisitDate", VisitDate));
                    if (!String.IsNullOrEmpty(DurationOnArt))
                    {
                        hivencounter.Add(new XElement("DurationOnArt", DurationOnArt));
                    }
                    if (!String.IsNullOrEmpty(Weight))
                    {
                        hivencounter.Add(new XElement("Weight", Weight));
                    }
                    if (!String.IsNullOrEmpty(BloodPressure))
                    {
                        hivencounter.Add(new XElement("BloodPressure", BloodPressure));
                    }
                    if (!String.IsNullOrEmpty(PatientFamilyPlanningCode))
                    {
                        hivencounter.Add(new XElement("PatientFamilyPlanningCode", PatientFamilyPlanningCode));
                    }
                    if (!String.IsNullOrEmpty(PatientFamilyPlanningMethodCode))
                    {
                        hivencounter.Add(new XElement("PatientFamilyPlanningMethodCode", PatientFamilyPlanningMethodCode));
                    }
                    if (!String.IsNullOrEmpty(FunctionalStatus))
                    {
                        hivencounter.Add(new XElement("FunctionalStatus", FunctionalStatus));
                    }
                    if (!String.IsNullOrEmpty(WHOClinicalStage))
                    {
                        hivencounter.Add(new XElement("WHOClinicalStage", WHOClinicalStage));
                    }
                    if (!String.IsNullOrEmpty(TBStatus))
                    {
                        hivencounter.Add(new XElement("TBStatus", TBStatus));
                    }
                    if (!String.IsNullOrEmpty(ARVRegimen))
                    {
                        string[] arv = ARVRegimen.ToString().Split(';');

                        hivencounter.Add(new XElement("ARVDrugRegimen",
                           new XElement("Code", arv[0].ToString()),
                           new XElement("CodeDescTxt", arv[1].ToString())
                       ));
                    }

                    if (!String.IsNullOrEmpty(CotrimoxazoleDose))
                    {

                        string[] cotrimoxazoledose = CotrimoxazoleDose.ToString().Split(';');
                        hivencounter.Add(new XElement("CotrimoxazoleDose",
                            new XElement("Code", cotrimoxazoledose[0].ToString()),
                            new XElement("CodeDescTxt", cotrimoxazoledose[1].ToString())
                            ));
                    }

                    if (!String.IsNullOrEmpty(INHDose))
                    {
                        string[] inh = INHDose.ToString().Split(';');
                        hivencounter.Add(new XElement("INHDose",
                            new XElement("Code", inh[0].ToString()),
                            new XElement("CodeDescTxt", inh[0].ToString())
                            ));

                    }
                    if (!String.IsNullOrEmpty(CD4))
                    {
                        string[] cd4 = CD4.ToString().Split(';');
                        hivencounter.Add(new XElement("CD4", cd4[0].ToString()));

                        DateTime cd4testdate = DateTime.Parse(cd4[1].ToString());
                        hivencounter.Add(new XElement("CD4TestDate", cd4testdate.ToString("yyyy-MM-dd")));
                    }
                    if (!String.IsNullOrEmpty(NextAppointmentDate))
                    {
                        DateTime nextappointmentdate = DateTime.Parse(NextAppointmentDate.ToString());
                        hivencounter.Add(new XElement("NextAppointmentDate", nextappointmentdate.ToString("yyyy-MM-dd")));
                    }
                    encounters.Add(hivencounter);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "IQCare.NDR.GetEncounters", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //CLogger.WriteLog(ELogLevel.ERROR, ex.ToString());
            }
            return encounters;
        }

        private XElement GetConditionSpecificQuestions(DataTable dtValues, DataRow drow)
        {
            XElement conditionspecificquestions = new XElement("ConditionSpecificQuestions");
            try
            {
                XElement hivquestions = new XElement("HIVQuestions");

                int patientId = Convert.ToInt32(drow["Ptn_Pk"].ToString());
                DataTable commonq1 = new DataTable();
                commonq1 = (DataTable)data.Tables[4];
                IEnumerable<DataRow> filterdata1 =
                    (from fdata in commonq1.AsEnumerable()
                     where fdata.Field<Int32>("Ptn_Pk") == patientId
                     select fdata);

                //DataTable commonq2 = new DataTable();
                //commonq2 = (DataTable)data.Tables[5];
                //IEnumerable<DataRow> filterdata2 =
                //    (from fdata in commonq2.AsEnumerable()
                //     where fdata.Field<Int32>("Ptn_Pk") == patientId
                //     select fdata);

                foreach (DataRow row in filterdata1)
                {
                    if (!DBNull.Value.Equals(row["PatientReferredFrom"]))
                    {
                        string patientreferredfrom = GetConceptValue(dtValues, "CARE_ENTRY_POINT", row["PatientReferredFrom"].ToString());
                        if (!string.IsNullOrEmpty(patientreferredfrom))
                        {
                            hivquestions.Add(new XElement("CareEntryPoint", patientreferredfrom));
                        }
                    }
                    else
                    {
                        //hivquestions.Add(new XElement("CareEntryPoint", ""));
                    }

                    if (!DBNull.Value.Equals(row["FirstHIVPosTestDate"]))
                    {
                        DateTime firsthivpostestdate = DateTime.Parse(row["FirstHIVPosTestDate"].ToString());
                        hivquestions.Add(new XElement("FirstConfirmedHIVTestDate", firsthivpostestdate.ToString("yyyy-MM-dd")));
                    }
                    else
                    {
                        //hivquestions.Add(new XElement("FirstConfirmedHIVTestDate", ""));
                    }
                    if (!DBNull.Value.Equals(row["FirstHIVTestMode"]))
                    {
                        string firsthivtestmode = GetConceptValue(dtValues, "HIV_TEST_TYPE", row["FirstHIVTestMode"].ToString());
                        hivquestions.Add(new XElement("FirstHIVTestMode", firsthivtestmode));
                    }
                    else
                    {
                        //hivquestions.Add(new XElement("FirstHIVTestMode", ""));
                    }

                    if (!DBNull.Value.Equals(row["WhereFirstHIVTest"]))
                    {
                        hivquestions.Add(new XElement("WhereFirstHIVTest", row["WhereFirstHIVTest"].ToString()));
                    }
                    else
                    {
                        //hivquestions.Add(new XElement("WhereFirstHIVTest", ""));
                    }
                    if (!DBNull.Value.Equals(row["PrevART"]))
                    {
                        string priorart = GetConceptValue(dtValues, "PRIOR_ART", row["PrevART"].ToString());
                        hivquestions.Add(new XElement("PriorArt", priorart.ToString()));
                    }
                    else
                    {
                        //hivquestions.Add(new XElement("PriorArt", ""));
                    }
                    if (!DBNull.Value.Equals(row["EligibleDate"]))
                    {
                        hivquestions.Add(new XElement("MedicallyEligibleDate", row["EligibleDate"].ToString()));
                    }
                    else
                    {
                        //hivquestions.Add(new XElement("MedicallyEligibleDate", ""));
                    }
                    if (!DBNull.Value.Equals(row["WhyEligible"]))
                    {
                        string whyeligible = GetConceptValue(dtValues, "WHY_ELIGIBLE", row["WhyEligible"].ToString());
                        hivquestions.Add(new XElement("ReasonMedicallyEligible", whyeligible.ToString()));
                    }
                    else
                    {
                        //hivquestions.Add(new XElement("ReasonMedicallyEligible", ""));
                    }
                    if (!DBNull.Value.Equals(row["InitialAdherenceCounselingCompletedDate"]))
                    {
                        DateTime initialadherencecounselingcompleteddate = DateTime.Parse(row["InitialAdherenceCounselingCompletedDate"].ToString());
                        hivquestions.Add(new XElement("InitialAdherenceCounselingCompletedDate", initialadherencecounselingcompleteddate.ToString("yyyy-MM-dd")));
                    }
                    else
                    {
                        //hivquestions.Add(new XElement("InitialAdherenceCounselingCompletedDate", ""));
                    }
                    if (!DBNull.Value.Equals(row["TransferredInDate"]))
                    {
                        DateTime transferredindate = DateTime.Parse(row["TransferredInDate"].ToString());
                        hivquestions.Add(new XElement("TransferredInDate", transferredindate.ToString("yyyy-MM-dd")));
                    }
                    else
                    {
                        // hivquestions.Add(new XElement("TransferredInDate", ""));
                    }
                    if (!DBNull.Value.Equals(row["TransferredInFrom"]))
                    {
                        string[] inh = row["TransferredInFrom"].ToString().Split(';');
                        hivquestions.Add(new XElement("FacilityReferredTo",
                            new XElement("FacilityName", inh[0].ToString()),
                            new XElement("FacilityID", inh[0].ToString()),
                            new XElement("FacilityTypeCode", inh[0].ToString())
                            ));

                    }
                    else
                    {
                        //hivquestions.Add(new XElement("FacilityReferredTo",
                        //        new XElement("FacilityName", ""),
                        //        new XElement("FacilityID", ""),
                        //        new XElement("FacilityTypeCode", "")
                        //        ));
                    }

                    if (!DBNull.Value.Equals(row["TransferredInFromPatId"]))
                    {
                        hivquestions.Add(new XElement("TransferredInFromPatId", row["TransferredInFromPatId"].ToString()));
                    }
                    else
                    {
                        //hivquestions.Add(new XElement("TransferredInFromPatId", ""));
                    }
                    if (!DBNull.Value.Equals(row["FirstARTRegimen"]))
                    {
                        string[] inh = row["FirstARTRegimen"].ToString().Split(';');
                        hivquestions.Add(new XElement("FirstARTRegimen",
                            new XElement("Code", inh[0].ToString()),
                            new XElement("CodeDescTxt", inh[0].ToString())
                            ));

                    }
                    else
                    {
                        //hivquestions.Add(new XElement("FirstARTRegimen",
                        //    new XElement("Code", ""),
                        //    new XElement("CodeDescTxt", "")
                        //    ));
                    }
                    if (!DBNull.Value.Equals(row["ARTStartDate"]))
                    {
                        DateTime artstartdate = DateTime.Parse(row["ARTStartDate"].ToString());
                        hivquestions.Add(new XElement("ARTStartDate", artstartdate.ToString("yyyy-MM-dd")));
                    }
                    else
                    {
                        //hivquestions.Add(new XElement("ARTStartDate", ""));
                    }
                    if (!DBNull.Value.Equals(row["WHOStage"]))
                    {
                        string whostage = GetConceptValue(dtValues, "WHO_STAGE", row["WHOStage"].ToString());
                        hivquestions.Add(new XElement("WHOClinicalStageARTStart", whostage.ToString()));
                    }
                    else
                    {
                        //hivquestions.Add(new XElement("WHOClinicalStageARTStart", ""));
                    }
                    if (!DBNull.Value.Equals(row["ARTWeight"]))
                    {
                        hivquestions.Add(new XElement("WeightAtARTStart", row["ARTWeight"].ToString()));
                    }
                    else
                    {
                        //hivquestions.Add(new XElement("WeightAtARTStart", ""));
                    }
                    if (!DBNull.Value.Equals(row["ChildHeightAtARTStart"]))
                    {
                        hivquestions.Add(new XElement("ChildHeightAtARTStart", row["ChildHeightAtARTStart"].ToString()));
                    }
                    else
                    {
                        //hivquestions.Add(new XElement("ChildHeightAtARTStart", ""));
                    }
                    if (!DBNull.Value.Equals(row["FunctionalStatus"]))
                    {
                        string functionalstatus = GetConceptValue(dtValues, "FUNCTIONAL_STATUS", row["FunctionalStatus"].ToString());
                        hivquestions.Add(new XElement("FunctionalStatusStartART", functionalstatus.ToString()));
                    }
                    else
                    {
                        //hivquestions.Add(new XElement("FunctionalStatusStartART", ""));
                    }
                    if (!DBNull.Value.Equals(row["CD4"]))
                    {
                        hivquestions.Add(new XElement("CD4AtStartOfART", row["CD4"].ToString()));
                    }
                    else
                    {
                        //hivquestions.Add(new XElement("CD4AtStartOfART", ""));
                    }
                    if (!DBNull.Value.Equals(row["PatientTransferredOut"]))
                    {
                        hivquestions.Add(new XElement("PatientTransferredOut", row["PatientTransferredOut"].ToString()));
                    }
                    else
                    {
                        // hivquestions.Add(new XElement("PatientTransferredOut", ""));
                    }
                    if (!DBNull.Value.Equals(row["TransferredOutStatus"]))
                    {
                        string transferredoutstatus = GetConceptValue(dtValues, "ART_STATUS", row["TransferredOutStatus"].ToString());
                        hivquestions.Add(new XElement("TransferredOutStatus", transferredoutstatus.ToString()));
                    }
                    else
                    {
                        // hivquestions.Add(new XElement("TransferredOutStatus", ""));
                    }
                    if (!DBNull.Value.Equals(row["TransferredOutDate"]))
                    {
                        DateTime transferredoutdate = DateTime.Parse(row["TransferredOutDate"].ToString());
                        hivquestions.Add(new XElement("TransferredOutDate", transferredoutdate.ToString("yyyy-MM-dd")));
                    }
                    else
                    {
                        // hivquestions.Add(new XElement("TransferredOutDate", ""));
                    }

                    if (!DBNull.Value.Equals(row["FacilityReferredTo"]))
                    {
                        string[] inh = row["FacilityReferredTo"].ToString().Split(';');
                        hivquestions.Add(new XElement("FacilityReferredTo",
                            new XElement("FacilityName", inh[0].ToString()),
                            new XElement("FacilityID", inh[0].ToString()),
                            new XElement("FacilityTypeCode", "FAC")
                            ));

                    }
                    else
                    {
                        //hivquestions.Add(new XElement("FacilityReferredTo",
                        //   new XElement("FacilityName", ""),
                        //   new XElement("FacilityID", ""),
                        //   new XElement("FacilityTypeCode", "")
                        //   ));
                    }
                    if (!DBNull.Value.Equals(row["PatientHasDied"]))
                    {
                        hivquestions.Add(new XElement("PatientHasDied", row["PatientHasDied"].ToString()));
                    }
                    else
                    {
                        //hivquestions.Add(new XElement("PatientHasDied", ""));
                    }
                    if (!DBNull.Value.Equals(row["StatusAtDeath"]))
                    {
                        string statusatdeath = GetConceptValue(dtValues, "ART_STATUS", row["StatusAtDeath"].ToString());
                        hivquestions.Add(new XElement("StatusAtDeath", statusatdeath.ToString()));
                    }
                    else
                    {
                        //hivquestions.Add(new XElement("StatusAtDeath", ""));
                    }
                    if (!DBNull.Value.Equals(row["DeathDate"]))
                    {
                        DateTime deathdate = DateTime.Parse(row["DeathDate"].ToString());
                        hivquestions.Add(new XElement("DeathDate", deathdate.ToString("yyyy-MM-dd")));
                    }
                    else
                    {
                        //hivquestions.Add(new XElement("DeathDate", ""));
                    }
                    if (!DBNull.Value.Equals(row["SourceOfDeathInformation"]))
                    {
                        hivquestions.Add(new XElement("SourceOfDeathInformation", row["SourceOfDeathInformation"].ToString()));
                    }
                    else
                    {
                        //hivquestions.Add(new XElement("SourceOfDeathInformation", ""));
                    }
                    if (!DBNull.Value.Equals(row["CauseOfDeathHIVRelated"]))
                    {
                        string causeofdeathhivrelated = GetConceptValue(dtValues, "YNU", row["CauseOfDeathHIVRelated"].ToString());
                        hivquestions.Add(new XElement("CauseOfDeathHIVRelated", causeofdeathhivrelated.ToString()));
                    }
                    else
                    {
                        //hivquestions.Add(new XElement("CauseOfDeathHIVRelated", ""));
                    }
                    if (!DBNull.Value.Equals(row["DrugAllergySpecify"]))
                    {
                        hivquestions.Add(new XElement("DrugAllergies", row["DrugAllergySpecify"].ToString()));
                    }
                    else
                    {
                        //hivquestions.Add(new XElement("DrugAllergies", ""));
                    }
                    if (!DBNull.Value.Equals(row["EnrolledInHIVCareDate"]))
                    {
                        DateTime enrolledinhivcaredate = DateTime.Parse(row["EnrolledInHIVCareDate"].ToString());
                        hivquestions.Add(new XElement("EnrolledInHIVCareDate", enrolledinhivcaredate.ToString("yyyy-MM-dd")));
                    }
                    else
                    {
                        //hivquestions.Add(new XElement("EnrolledInHIVCareDate", ""));
                    }
                    if (!DBNull.Value.Equals(row["InitialTBStatus"]))
                    {
                        string initialtbstatus = GetConceptValue(dtValues, "TB_STATUS", row["InitialTBStatus"].ToString());
                        hivquestions.Add(new XElement("InitialTBStatus", initialtbstatus.ToString()));
                    }
                    else
                    {
                        // hivquestions.Add(new XElement("InitialTBStatus", ""));
                    }
                }

                conditionspecificquestions.Add(hivquestions);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "IQCare.NDR.GetConditionSpecificQuestions", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //CLogger.WriteLog(ELogLevel.ERROR, ex.ToString());
            }
            return conditionspecificquestions;
        }

        private void GetLaboratoryReport(DataTable dtValues, DataRow row, XElement condition)
        {
            try
            {
                int patientId = Convert.ToInt32(row["Ptn_Pk"].ToString());
                DataTable report = new DataTable();
                report = (DataTable)data.Tables[5];
                var query =
                    (from fdata in report.AsEnumerable()
                     where fdata.Field<Int32>("Ptn_Pk") == patientId
                     select (new
                     {
                         VisitId = fdata.Field<Int32>("VisitId"),
                         VisitDate = fdata.Field<DateTime>("VisitDate"),
                         LaboratoryTestIdentifier = fdata.Field<string>("LaboratoryTestIdentifier"),
                         CollectionDate = fdata.Field<DateTime>("CollectionDate"),
                         BaselineRepeatCode = fdata.Field<string>("BaselineRepeatCode"),
                         ARTStatusCode = fdata.Field<string>("ARTStatusCode"),
                         Clinician = fdata.Field<string>("Clinician"),
                         ReportedBy = fdata.Field<string>("ReportedBy"),
                         CheckedBy = fdata.Field<string>("CheckedBy")
                     })).Distinct().ToList();

                foreach (var rowdata in query)
                {
                    XElement laboratoryreport = new XElement("LaboratoryReport");

                    laboratoryreport.Add(new XElement("VisitID", rowdata.VisitId));
                    DateTime VD = DateTime.Parse(rowdata.VisitDate.ToString());
                    laboratoryreport.Add(new XElement("VisitDate", VD.ToString("yyyy-MM-dd")));
                    laboratoryreport.Add(new XElement("LaboratoryTestIdentifier", rowdata.LaboratoryTestIdentifier));
                    DateTime CD = DateTime.Parse(rowdata.CollectionDate.ToString());
                    laboratoryreport.Add(new XElement("CollectionDate", CD.ToString("yyyy-MM-dd")));
                    laboratoryreport.Add(new XElement("BaselineRepeatCode", rowdata.BaselineRepeatCode));
                    laboratoryreport.Add(new XElement("ARTStatusCode", rowdata.ARTStatusCode));

                    var query1 =
                    (from fdata in report.AsEnumerable()
                     where fdata.Field<Int32>("Ptn_Pk") == patientId
                     && fdata.Field<Int32>("VisitId") == rowdata.VisitId
                     select (new
                     {
                         OrderedTestDate = fdata.Field<DateTime?>("OrderedTestDate"),
                         Code = fdata.Field<string>("Code"),
                         CodeDescTxt = fdata.Field<string>("CodeDescTxt"),
                         Value1 = fdata.Field<decimal?>("Value1"),
                         ResultedTestDate = fdata.Field<DateTime?>("ResultedTestDate")
                     })).Distinct().ToList();

                    foreach (var rowdata1 in query1)
                    {
                        XElement laboratoryorderandresult = new XElement("LaboratoryOrderAndResult");

                        DateTime OTD = DateTime.Parse(rowdata1.OrderedTestDate.ToString());
                        laboratoryorderandresult.Add(new XElement("OrderedTestDate", OTD.ToString("yyyy-MM-dd")));

                        XElement laboratoryresultedtest =
                            new XElement("LaboratoryResultedTest",
                                new XElement("Code", rowdata1.Code),
                                new XElement("CodeDescTxt", rowdata1.CodeDescTxt)
                                );

                        laboratoryorderandresult.Add(laboratoryresultedtest);

                        if (!string.IsNullOrEmpty(rowdata1.Value1.ToString()))
                        {
                            XElement laboratoryresult =
                                new XElement("LaboratoryResult",
                                    new XElement("AnswerNumeric",
                                        new XElement("Value1", rowdata1.Value1)
                               ));
                            laboratoryorderandresult.Add(laboratoryresult);
                        }

                        if (!string.IsNullOrEmpty(rowdata1.ResultedTestDate.ToString()))
                        {
                            DateTime RTD = DateTime.Parse(rowdata1.ResultedTestDate.ToString());
                            laboratoryorderandresult.Add(new XElement("ResultedTestDate", RTD.ToString("yyyy-MM-dd")));
                        }
                        //laboratoryorderandresult.Add(new XElement("OtherLaboratoryInformation", ""));

                        laboratoryreport.Add(laboratoryorderandresult);
                    }
                    if (!String.IsNullOrEmpty(rowdata.Clinician))
                    {
                        laboratoryreport.Add(new XElement("Clinician", rowdata.Clinician.Trim()));
                    }
                    if (!String.IsNullOrEmpty(rowdata.ReportedBy))
                    {
                        laboratoryreport.Add(new XElement("ReportedBy", rowdata.ReportedBy.Trim()));
                    }
                    if (!String.IsNullOrEmpty(rowdata.CheckedBy))
                    {
                        laboratoryreport.Add(new XElement("CheckedBy", rowdata.CheckedBy.Trim()));
                    }
                    condition.Add(laboratoryreport);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "IQCare.NDR.GetLaboratoryReport", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //CLogger.WriteLog(ELogLevel.ERROR, ex.ToString());
            }
        }

        private void GetRegimen(DataTable dtValues, DataRow row, XElement condition)
        {
            try
            {
                int patientId = Convert.ToInt32(row["Ptn_Pk"].ToString());
                DataTable report = new DataTable();
                report = (DataTable)data.Tables[6];
                IEnumerable<DataRow> filterdata =
                    (from fdata in report.AsEnumerable()
                     where fdata.Field<Int32>("Ptn_Pk") == patientId
                     select fdata);

                foreach (DataRow rowdata in filterdata)
                {
                    if (!DBNull.Value.Equals(rowdata["PrescribedRegimen"]))
                    {
                        XElement laboratoryreport = new XElement("Regimen");

                        laboratoryreport.Add(new XElement("VisitID", rowdata["VisitId"].ToString()));
                        DateTime VD = DateTime.Parse(rowdata["VisitDate"].ToString());
                        laboratoryreport.Add(new XElement("VisitDate", VD.ToString("yyyy-MM-dd")));
                        if (!DBNull.Value.Equals(rowdata["ReasonForRegimenSwitchSubs"]))
                        {
                            laboratoryreport.Add(new XElement("ReasonForRegimenSwitchSubs", rowdata["ReasonForRegimenSwitchSubs"].ToString()));
                        }

                        if (!DBNull.Value.Equals(rowdata["PrescribedRegimen"]))
                        {
                            string[] arv = rowdata["PrescribedRegimen"].ToString().Split(';');

                            laboratoryreport.Add(new XElement("PrescribedRegimen",
                               new XElement("Code", arv[0].ToString()),
                               new XElement("CodeDescTxt", arv[1].ToString())
                           ));

                        }
                        if (!DBNull.Value.Equals(rowdata["PrescribedRegimenTypeCode"]))
                        {
                            laboratoryreport.Add(new XElement("PrescribedRegimenTypeCode", rowdata["PrescribedRegimenTypeCode"].ToString()));
                        }
                        if (!DBNull.Value.Equals(rowdata["PrescribedRegimenLineCode"]))
                        {
                            laboratoryreport.Add(new XElement("PrescribedRegimenLineCode", rowdata["PrescribedRegimenLineCode"].ToString()));
                        }
                        if (!DBNull.Value.Equals(rowdata["PrescribedRegimenDuration"]))
                        {
                            string prescribedregimenduration = rowdata["PrescribedRegimenDuration"].ToString().Replace("-", "");
                            laboratoryreport.Add(new XElement("PrescribedRegimenDuration", prescribedregimenduration));
                        }
                        if (!DBNull.Value.Equals(rowdata["PrescribedRegimenDispensedDate"]))
                        {
                            DateTime VD1 = DateTime.Parse(rowdata["PrescribedRegimenDispensedDate"].ToString());
                            laboratoryreport.Add(new XElement("PrescribedRegimenDispensedDate", VD1.ToString("yyyy-MM-dd")));
                        }
                        if (!DBNull.Value.Equals(rowdata["DateRegimenStarted"]))
                        {
                            DateTime VD2 = DateTime.Parse(rowdata["DateRegimenStarted"].ToString());
                            laboratoryreport.Add(new XElement("DateRegimenStarted", VD2.ToString("yyyy-MM-dd")));
                        }
                        if (!DBNull.Value.Equals(rowdata["DateRegimenStartedDD"]))
                        {
                            laboratoryreport.Add(new XElement("DateRegimenStartedDD", rowdata["DateRegimenStartedDD"].ToString()));
                        }
                        if (!DBNull.Value.Equals(rowdata["DateRegimenStartedMM"]))
                        {
                            laboratoryreport.Add(new XElement("DateRegimenStartedMM", rowdata["DateRegimenStartedMM"].ToString()));
                        }
                        if (!DBNull.Value.Equals(rowdata["DateRegimenStartedYYYY"]))
                        {
                            laboratoryreport.Add(new XElement("DateRegimenStartedYYYY", rowdata["DateRegimenStartedYYYY"].ToString()));
                        }
                        if (!DBNull.Value.Equals(rowdata["DateRegimenEnded"]))
                        {
                            DateTime VD3 = DateTime.Parse(rowdata["DateRegimenEnded"].ToString());
                            laboratoryreport.Add(new XElement("DateRegimenEnded", VD3.ToString("yyyy-MM-dd")));
                        }
                        if (!DBNull.Value.Equals(rowdata["DateRegimenEndedDD"]))
                        {
                            laboratoryreport.Add(new XElement("DateRegimenEndedDD", rowdata["DateRegimenEndedDD"].ToString()));
                        }
                        if (!DBNull.Value.Equals(rowdata["DateRegimenEndedMM"]))
                        {
                            laboratoryreport.Add(new XElement("DateRegimenEndedMM", rowdata["DateRegimenEndedMM"].ToString()));
                        }
                        if (!DBNull.Value.Equals(rowdata["DateRegimenEndedYYYY"]))
                        {
                            laboratoryreport.Add(new XElement("DateRegimenEndedYYYY", rowdata["DateRegimenEndedYYYY"].ToString()));
                        }
                        if (!DBNull.Value.Equals(rowdata["PrescribedRegimenInitialIndicator"]))
                        {
                            laboratoryreport.Add(new XElement("PrescribedRegimenInitialIndicator", rowdata["PrescribedRegimenInitialIndicator"].ToString().ToLower()));
                        }
                        if (!DBNull.Value.Equals(rowdata["PrescribedRegimenCurrentIndicator"]))
                        {
                            laboratoryreport.Add(new XElement("PrescribedRegimenCurrentIndicator", rowdata["PrescribedRegimenCurrentIndicator"].ToString().ToLower()));
                        }
                        if (!DBNull.Value.Equals(rowdata["TypeOfPreviousExposureCode"]))
                        {
                            laboratoryreport.Add(new XElement("TypeOfPreviousExposureCode", rowdata["TypeOfPreviousExposureCode"].ToString()));
                        }
                        if (!DBNull.Value.Equals(rowdata["PoorAdherenceIndicator"]))
                        {
                            laboratoryreport.Add(new XElement("PoorAdherenceIndicator", rowdata["PoorAdherenceIndicator"].ToString().ToLower()));
                        }
                        if (!DBNull.Value.Equals(rowdata["ReasonForPoorAdherence"]))
                        {
                            laboratoryreport.Add(new XElement("ReasonForPoorAdherence", rowdata["ReasonForPoorAdherence"].ToString()));
                        }
                        if (!DBNull.Value.Equals(rowdata["ReasonRegimenEndedCode"]))
                        {
                            laboratoryreport.Add(new XElement("ReasonRegimenEndedCode", rowdata["ReasonRegimenEndedCode"].ToString()));
                        }
                        if (!DBNull.Value.Equals(rowdata["SubstitutionIndicator"]))
                        {
                            laboratoryreport.Add(new XElement("SubstitutionIndicator", rowdata["SubstitutionIndicator"].ToString().ToLower()));
                        }
                        if (!DBNull.Value.Equals(rowdata["SwitchIndicator"]))
                        {
                            laboratoryreport.Add(new XElement("SwitchIndicator", rowdata["SwitchIndicator"].ToString().ToLower()));
                        }
                        condition.Add(laboratoryreport);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "IQCare.NDR.GetLaboratoryReport", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //CLogger.WriteLog(ELogLevel.ERROR, ex.ToString());
            }
        }

    }
}
